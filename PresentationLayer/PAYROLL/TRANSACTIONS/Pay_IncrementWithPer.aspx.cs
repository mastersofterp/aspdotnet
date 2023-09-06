//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Increment.ASPX                                                    
// CREATION DATE : 11-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_Pay_IncrementWithPer : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
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
            Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Increment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Increment.aspx");
        }
    }

    private void BindListViewList(int staffNo, int incMonth, int collegeNo, int subdeptno)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedValue) == 0 && Convert.ToInt32(ddlIncMonth.SelectedValue) == 0))
            {
                pnlIncrement.Visible = true;
                DataSet ds = objpay.GetIncrementEmployeesper(staffNo, incMonth, collegeNo, subdeptno);
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

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvIncrement.Items)
            {
                TextBox txtOldBasic = lvitem.FindControl("txtOldBasic") as TextBox;
                TextBox txtInc = lvitem.FindControl("txtInc") as TextBox;
                TextBox txtIncDate = lvitem.FindControl("txtIncDate") as TextBox;
                TextBox txtBasic = lvitem.FindControl("txtNewBasic") as TextBox;
                TextBox txtIncper = lvitem.FindControl("txtIncper") as TextBox;
                CustomStatus cs = (CustomStatus)objpay.UpdateIncermentper(Convert.ToInt32(txtOldBasic.ToolTip), Convert.ToInt32(txtOldBasic.Text), Convert.ToInt32(txtBasic.Text), Convert.ToChar(txtInc.Text), Convert.ToDateTime(txtIncDate.Text), Convert.ToDecimal(txtIncper.Text));
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

    protected void FillDropdown()
    {
        try
        {
            // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlIncMonth, "PAYROLL_MONFILE", "MCODE", "MNAME", "", "MCODE");
            objCommon.FillDropDownList(ddldepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPTNO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
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
        if (Convert.ToInt32(ddlStaff.SelectedIndex) == 0 && Convert.ToInt32(ddlIncMonth.SelectedIndex) == 0)
        {
            pnlIncrement.Visible = false;
        }
        else if (Convert.ToInt32(ddlStaff.SelectedIndex) == 0 && Convert.ToInt32(ddlIncMonth.SelectedIndex) > 0)
        {
            //lblerror.Text = "Please Select Staff";
            objCommon.DisplayMessage(UpdatePanel1, "Please select Staff", this);
            BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlIncMonth.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
        }
        //else if (Convert.ToInt32(ddlStaff.SelectedIndex) > 0 && Convert.ToInt32(ddlIncMonth.SelectedIndex) == 0)
        //{
        //    //lblerror.Text = "Please Select Increment Month";
        //    objCommon.DisplayMessage(UpdatePanel1, "Please select Increment Month", this);

        //}
        else
        {
            lblerror.Text = string.Empty;
            BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlIncMonth.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
        }
    }

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    lblerror.Text = string.Empty;
    //    BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlIncMonth.SelectedValue));
    //}
    //protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlIncMonth_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlIncMonth.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));

    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlIncMonth.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
    }
}