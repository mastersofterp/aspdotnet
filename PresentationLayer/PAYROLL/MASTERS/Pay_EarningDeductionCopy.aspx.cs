//======================================================================================
// PROJECT NAME  : RFC_CODE                                                                
// MODULE NAME   : PAYROLL
// PAGE NAME     : Pay_EarningDeductionCopy.ASPX                                                    
// CREATION DATE : 21-09-2022                                                        
// CREATED BY    : Prashant Wankar                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class PAYROLL_MASTERS_Pay_EarningDeductionCopy : System.Web.UI.Page
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
            divshow.Visible = false;

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
                // string s = Session["userfullname"].ToString();
                //  string c = Session["colcode"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillRule();

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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Earn_Dedu_Rule.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Earn_Dedu_Rule.aspx");
        }
    }
    private void FillRule()
    {
        try
        {
            objCommon.FillDropDownList(ddlPayRuleFrom, "PAYROLL_RULE", "PAYRULE", "PAYRULE", "", "RULENO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillRule-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowOption()
    {
        try
        {
            objCommon.FillDropDownList(ddlRuleCopy, "PAYROLL_RULE", "PAYRULE", "PAYRULE", "", "RULENO");
            objCommon.FillDropDownList(ddlCollegeCopy, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID !=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.BindListViewCalPay-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewCalPay()
    {
        try
        {
            string payruletxt = string.Empty;
            if (ddlPayRuleFrom.SelectedValue == "0")
                payruletxt = "0";
            else
                payruletxt = Convert.ToString(ddlPayRuleFrom.SelectedItem.Text);

            int collegeNo = 0;
            collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);

            DataSet ds = objpay.GetAllCalPay(payruletxt, collegeNo);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvCalPay.DataSource = null;
                lvCalPay.DataBind();
                divshow.Visible = false;
            }
            else
            {
                lvCalPay.DataSource = ds;
                lvCalPay.DataBind();

                divshow.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.BindListViewCalPay-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindListViewCalPay();
        ShowOption();
        lvCopyRule.DataSource = null;
        lvCopyRule.DataBind();
        lvCopyRule.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divshow.Visible = false;
        lvCopyRule.DataSource = null;
        lvCopyRule.DataBind();
        ddlCollege.SelectedIndex = 0;
        ddlPayRuleFrom.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (ListViewDataItem itm in lvCalPay.Items)
            {
                CheckBox chk = itm.FindControl("ChkAppointment") as CheckBox;
                Label lblreg = itm.FindControl("lblreg") as Label;

                if (chk.Checked == true)
                {
                    count++;
                    CustomStatus cs = (CustomStatus)objpay.SaveEarningDeduction(Convert.ToInt32(chk.ToolTip), Convert.ToInt32(ddlCollegeCopy.SelectedValue), Convert.ToString(ddlRuleCopy.SelectedItem.Text));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        MessageBox("Earning & Deduction Rule Copy Successfully");
                       // divshow.Visible = false;
                        BindExistingRule();
                    }
                }
            }

            if (count == 0)
            {
                MessageBox("Please select atleast single rule");
                
            }
           // BindExistingRule();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {

        

    }

    private void BindExistingRule()
    {
        try
        {
            string payruletxt = string.Empty;
            if (ddlRuleCopy.SelectedValue == "0")
                payruletxt = "0";
            else
                payruletxt = Convert.ToString(ddlRuleCopy.SelectedItem.Text);

            int collegeNo = 0;
            collegeNo = Convert.ToInt32(ddlCollegeCopy.SelectedValue);

            DataSet ds = objpay.GetAllCalPay(payruletxt, collegeNo);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvCopyRule.DataSource = null;
                lvCopyRule.DataBind();
                lvCopyRule.Visible = false;
            }
            else
            {
                lvCopyRule.DataSource = ds;
                lvCopyRule.DataBind();
                lvCopyRule.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.BindListViewCalPay-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
      
    }
    protected void ddlCollegeCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExistingRule();
    }
    protected void ddlRuleCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExistingRule();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        divshow.Visible = false;
        lvCopyRule.DataSource = null;
        lvCopyRule.DataBind();
        lvCopyRule.Visible = false;
    }
    protected void ddlPayRuleFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        divshow.Visible = false;
        lvCopyRule.DataSource = null;
        lvCopyRule.DataBind();
        lvCopyRule.Visible = false;
    }
}