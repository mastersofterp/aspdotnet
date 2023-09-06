using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class PAYROLL_TRANSACTIONS_Pay_DailyWages : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillDropdown();
                pnlIncrement.Visible = false;
                pnlSelect.Visible = true;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_DailyWages.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_DailyWages.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {
            string staffDailyWages =Convert.ToString(objCommon.LookUp("PAYROLL_PAY_REF", "ISNULL(STAFF_FOR_CA,0)", ""));
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO IN(" + staffDailyWages + ")", "STAFFNO");
            objCommon.FillDropDownList(ddlIncMonth, "PAYROLL_MONFILE", "MCODE", "MNAME", "", "MCODE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    private void BindListViewList(int collegeNo, int staffNo, int month)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedValue) == 0 && Convert.ToInt32(ddlIncMonth.SelectedValue) == 0))
            {
                pnlIncrement.Visible = true;
                DataSet ds = objpay.GetDailyWagesEmployees(collegeNo, staffNo, month);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnCancel.Visible = true;
                    btnSave.Visible = true;

                }
                lvIncrement.DataSource = ds;
                lvIncrement.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListViewValidation();
    }

    protected void ddlIncMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListViewValidation();
    }

    protected void BindListViewValidation()
    {
            lblerror.Text = string.Empty;
            BindListViewList(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlIncMonth.SelectedValue));

    }
    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvIncrement.Items)
            {
                TextBox txtFixAmt = lvitem.FindControl("txtfixamt") as TextBox;
                //TextBox txtInc = lvitem.FindControl("txtInc") as TextBox;
                //TextBox txtIncDate = lvitem.FindControl("txtIncDate") as TextBox;
                TextBox txtBasic = lvitem.FindControl("txtBasic") as TextBox;
                TextBox txtDailyAmt = lvitem.FindControl("txtfixamt") as TextBox;
                CustomStatus cs = (CustomStatus)objpay.UpdateDailyWagesBasic(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToDouble(txtFixAmt.ToolTip), Convert.ToDouble(txtBasic.Text), Convert.ToDouble(txtDailyAmt.Text));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }
            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlStaff.SelectedIndex = 0;
        //ddlIncMonth.SelectedIndex = 0;      
        //pnlIncrement.Visible = false;
        //lblerror.Text = string.Empty;
        //lblmsg.Text = string.Empty;

        Response.Redirect(Request.Url.ToString());

    }
}
