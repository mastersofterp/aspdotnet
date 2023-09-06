//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITRules.ASPX                                                    
// CREATION DATE : 29/03/2011
// CREATED BY    :                                       
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



public partial class PAY_ITRules : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    ITRule objIT = new ITRule();
    string UsrStatus = string.Empty;

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
                ViewState["action"] = "add";
                BindListView();
                BindITRuleYearDropdown();

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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITRules.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITRules.aspx");
        }
    }
    public void BindITRuleYearDropdown()
    {
       // objCommon.FillDropDownList(ddlITRule, "PAYROLL_ITRULE", "IT_RULE_ID", "IT_RULE_NAME", "IT_RULE_ID > 0", "IT_RULE_ID");

        objCommon.FillDropDownList(ddlITRule, "PAYROLL_ITRULE", "IT_RULE_ID", "IT_RULE_NAME", "IT_RULE_ID > 0 and IsActive =1", "IT_RULE_ID");
    }

    //Save Rule
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AddUpdate();
    }
    protected void AddUpdate()
    {
        ITRule objIT = new ITRule();

        bool result = checkITRule();
        try
        {
            //Add/Update5
          
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                //Add
                {
                    if (result == true)
                    {
                        objCommon.DisplayMessage(this.updpanel, "Data already Exist", this.Page);
                        txtMinLimit.Focus();
                        return;
                    }
                    if (rblschemetype.SelectedValue == "")
                    {
                        objCommon.DisplayMessage(this.updpanel, "Please Select Scheme Type", this.Page);
                        return;
                    }
                    if (rblschemetype.SelectedValue == "Old Scheme")
                    {
                        objIT.SchemeType = 0;
                    }
                    else if (rblschemetype.SelectedValue == "New Scheme")
                    {
                        objIT.SchemeType = 1;
                    }
                        objIT.ITNO = 0;
                        objIT.MINLIMIT = Convert.ToDecimal(txtMinLimit.Text.Trim());
                        objIT.MAXLIMIT = Convert.ToDecimal(txtMaxLimit.Text.Trim());
                        objIT.FIXAMT = Convert.ToDecimal(txtFixAmount.Text.Trim());
                        objIT.PERCENTAGE = Convert.ToDecimal(txtPercentage.Text.Trim());
                        objIT.COLLEGE_CODE = Session["colcode"].ToString();
                        objIT.STATUS =Convert.ToInt32(ddlITFor.SelectedValue);
                        objIT.ITRULEID = Convert.ToInt32(ddlITRule.SelectedValue);
                        CustomStatus cs = (CustomStatus)objITMas.AddUpdateITRule(objIT);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindListView();
                            Clear();
                            objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                        }
                        else
                            objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
                else
                //Update
                {
                    if (Session["lblITNO"] != null)
                    {
                        ViewState["action"] = "add";

                        if (result == true)
                        {
                            string sNo = Session["lblITNO"].ToString().Trim();
                            DataSet ds = new DataSet();
                            ds = objCommon.FillDropDown("PAYROLL_ITAXCAL", "*", "", "ITNO=" + Convert.ToInt32(sNo), "");

                            //*****AS REQUIRED BY TESTER TO NOT CHECK (DATA ALREADY EXIST) ON MIN & MAX LIMIT. COMMENTED BY: M. REHBAR SHEIKH ON 23-08-2019*****
                            //if (txtMinLimit.Text.Trim() == ds.Tables[0].Rows[0]["F_RANGE"].ToString().Trim() && txtMaxLimit.Text.Trim() == ds.Tables[0].Rows[0]["T_RANGE"].ToString().Trim())
                            //{
                            //    objCommon.DisplayMessage(this.updpanel, "Data already Exist", this.Page);
                            //    txtMinLimit.Focus();
                            //    ViewState["action"] = "edit";
                            //    return;
                            //}
                            //***************************************************************************
                        }
                        if (rblschemetype.SelectedValue == "")
                        {
                            objCommon.DisplayMessage(this.updpanel, "Please Select Scheme Type", this.Page);
                            return;
                        }
                        if (rblschemetype.SelectedValue == "Old Scheme")
                        {
                            objIT.SchemeType = 0;
                        }
                        else if (rblschemetype.SelectedValue == "New Scheme")
                        {
                            objIT.SchemeType = 1;
                        }
                            objIT.ITNO = Convert.ToInt32(Session["lblITNO"].ToString().Trim());
                            objIT.MINLIMIT = Convert.ToDecimal(txtMinLimit.Text.Trim());
                            objIT.MAXLIMIT = Convert.ToDecimal(txtMaxLimit.Text.Trim());
                            objIT.FIXAMT = Convert.ToDecimal(txtFixAmount.Text.Trim());
                            objIT.PERCENTAGE = Convert.ToDecimal(txtPercentage.Text.Trim());
                            objIT.COLLEGE_CODE = Session["colcode"].ToString();
                            objIT.STATUS = Convert.ToInt32(ddlITFor.SelectedValue);
                            objIT.ITRULEID = Convert.ToInt32(ddlITRule.SelectedValue);
                            CustomStatus cs = (CustomStatus)objITMas.AddUpdateITRule(objIT);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                BindListView();
                                Clear();
                                objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                            }
                            else
                                objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                        
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    //Get Details of Rules To be Edited on textbox
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Session["lblITNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblMINLIMIT = lst.FindControl("lblMinAmount") as Label;
            Label lblMAXLIMIT = lst.FindControl("lblMaxAmount") as Label;
            Label lblFIXAMT = lst.FindControl("lblFixAmount") as Label;
            Label lblPER = lst.FindControl("lblPercentage") as Label;
            Label lblITFor = lst.FindControl("lblstatus") as Label;
            Label lblITRuleId = lst.FindControl("lblITRuleId") as Label;
            Label lblscheme = lst.FindControl("lblschemetype") as Label;
            txtMinLimit.Text = lblMINLIMIT.Text.Trim();
            txtMaxLimit.Text = lblMAXLIMIT.Text.Trim();
            txtFixAmount.Text = lblFIXAMT.Text.Trim();
            txtPercentage.Text = lblPER.Text.Trim();
            ddlITFor.SelectedValue = lblITFor.Text.Trim();
            ddlITRule.SelectedValue = lblITRuleId.Text.Trim();

            if (lblscheme.Text == "Old Scheme")
            {
                rblschemetype.SelectedValue = "Old Scheme";
            }
            else if (lblscheme.Text == "New Scheme")
            {
                rblschemetype.SelectedValue = "New Scheme";
            }

            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    //Check user rights and generate the report
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("ITRules", "Pay_ITRules.rpt");
    }

    //Fetch Already Defined Rules From The Database
    private void BindListView()
    {
        try
        {
            DataSet ds= objITMas.GetITRules();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvITRule.DataSource = ds;
                lvITRule.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Check If the Rule being added, already exists or a new one
    public bool checkITRule()
    {
        bool result = false;
        DataSet ds = new DataSet();

        int ITRULEID=Convert.ToInt32(ddlITRule.SelectedValue);
        int scheme = Convert.ToInt32(objCommon.LookUp("PAYROLL_ITRULE", "SchemeType", "IT_RULE_ID=" + ITRULEID));

        ds = objCommon.FillDropDown("PAYROLL_ITAXCAL", "*", "", "F_RANGE=" + txtMinLimit.Text.Trim() + " AND T_RANGE=" + txtMaxLimit.Text.Trim() + " AND STATUS=" + ddlITFor.SelectedValue + " AND SchemeType=" + scheme + " AND IT_RULE_ID=" + ITRULEID, "ITNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;
        }
        return result;
    }

   

    //Function to generate Report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle;
            //+",@P_Session=" + Session["Session"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString() + ",@P_WorkingDate=" + Session["WorkingDate"].ToString().Trim()

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtMinLimit.Text = "";
        txtMaxLimit.Text = "";
        txtFixAmount.Text = "";
        txtPercentage.Text = "";
        ViewState["action"] = "add";
        ddlITFor.SelectedIndex = 0;
        ddlITRule.SelectedIndex = 0;
        rblschemetype.SelectedIndex = -1;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlITRule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ITRULEID=Convert.ToInt32(ddlITRule.SelectedValue);
        int scheme = Convert.ToInt32(objCommon.LookUp("PAYROLL_ITRULE", "SchemeType", "IT_RULE_ID=" + ITRULEID));
        if(scheme  == 0)
        {
            rblschemetype.SelectedValue = "Old Scheme";
        }
        else 
        {
            rblschemetype.SelectedValue  = "New Scheme";
        }
    }
    protected void rblschemetype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
