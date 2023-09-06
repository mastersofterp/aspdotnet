//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Head_PrivilegeToUser.ASPX                                                    
// CREATION DATE : 24-JULY-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
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

public partial class EMPIDCARD : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    PayHeadPrivilegesController objPayHeadPrivilege = new PayHeadPrivilegesController();
    string selectedIDs = string.Empty;

    int OrganizationId;
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
                FillDropDown();
                OrganizationId = Convert.ToInt32(Session["OrgId"]);
                //pnlButton.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?EMPIDCARD.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EMPIDCARD.aspx");
        }
    }

    private void BindListViewPayHead(int staffNo)
    {
        try
        {
            string orderby=string.Empty;
            if (ddlOrderBy.SelectedValue == "-1")
            {
              orderby= "IDNO";
            }
            else if (ddlOrderBy.SelectedValue == "0")
            {
                orderby = Convert.ToString(ddlOrderBy.SelectedItem.Text);
            }
            else
            {
                orderby = Convert.ToString(ddlOrderBy.SelectedItem.Text);
            }
            DataSet ds = objPayHeadPrivilege.GetEmpForIdentityCard(staffNo,Convert.ToInt32(ddlDepartment.SelectedValue),Convert.ToInt32(ddlCollege.SelectedValue), orderby);
            //DataSet dsEdit = objPayHeadPrivilege.EditPayHeadUser(staffNo);

            if (ds.Tables[0].Rows.Count <= 0)
                pnlPayhead.Visible = false;
            else
            {
                pnlPayhead.Visible = true;
                lvPayhead.DataSource = ds;
                lvPayhead.DataBind();
               
            
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.BindListViewPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void BindListViewUser(int staffNo)
    //{
    //    try
    //    {
    //        DataSet ds = objPayHeadPrivilege.GetPayHeadUser(staffNo);
    //        if (ds.Tables[0].Rows.Count <= 0)
    //            PnlUserHead.Visible = true;
    //        else
    //        {
    //            PnlUserHead.Visible = true;
    //            lvUser.DataSource = ds;
    //            lvUser.DataBind();
    //            ds.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.BindListViewAppoint-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void FillDropDown()
    {
        OrganizationId = Convert.ToInt32(Session["OrgId"]);
           
            try
            {
                objCommon.FillDropDownList(ddlStaffName, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
                objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
                objCommon.FillDropDownList(ddlIDCardType, "PayReportConfiguration", "FormatID", "IDCardType", "IdType in (1,2) and OrganizationId=" + OrganizationId, "FormatID");
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.FillUser-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
       
    }

    protected int GetLinkNo()
    {   
        int linkno=0;

        try
        {
           linkno = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAY_REF", "isnull(link_no,0)", ""));
        }
        catch (Exception ex)
        {   
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.GetLinkNo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

        return linkno; 
    }

    protected void ddlStaffName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
             OrganizationId = Convert.ToInt32(Session["OrgId"]);
            if (ddlStaffName.SelectedIndex > 0)
            {
                //if (ddlDepartment.SelectedIndex != 0)
                //{
                int staffNo = Convert.ToInt32(ddlStaffName.SelectedValue);
                BindListViewPayHead(staffNo);
                objCommon.FillDropDownList(ddlIDCardType, "PayReportConfiguration", "FormatID", "IDCardType", "StaffNo = " + Convert.ToInt32(ddlStaffName.SelectedValue) + " and OrganizationId=" + OrganizationId, "FormatID");
                // + "or StaffNo = 0

                //}
            }
            else
            {
                objCommon.FillDropDownList(ddlIDCardType, "PayReportConfiguration", "FormatID", "IDCardType", "IdType in (1,2) and OrganizationId=" + OrganizationId, "FormatID");
           
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;

       

        if (!(ddlStaffName.SelectedValue == "0"))
        {
            int staffNo = Convert.ToInt32(ddlStaffName.SelectedValue);
            pnlPayhead.Visible = true;
            //PnlUserHead.Visible = true;
            pnlButton.Visible = true;
            BindListViewPayHead(staffNo);
            //BindListViewUser(staffNo);
        }
        else
        {
            pnlPayhead.Visible = false;
            // PnlUserHead.Visible = false;
           // pnlButton.Visible = false;
        }
    }

    //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //}

    protected void btnBothSide_Click(object sender, EventArgs e)
    {

         OrganizationId = Convert.ToInt32(Session["OrgId"]);
        foreach (ListViewDataItem lvItem in lvPayhead.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            if (chk.Checked == true)
            {
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
            }
           
        }
        if (selectedIDs  != "")
        {
              selectedIDs = selectedIDs.Remove(selectedIDs.Length - 1);
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Select Atleast One Employee!!!", this);
            return;
        }
        string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "FormatID=" + Convert.ToInt32(ddlIDCardType.SelectedValue)+" and OrganizationId="+OrganizationId);
        //if(ddlStaffName .SelectedIndex ==1)
        //ShowReport("EmployeeICards", "Pay_EmpIDCardBothSide.rpt");  
        //ShowReportIdCard("EmployeeICards", "rptEmpIDCardBackNew.rpt"); // old code
        if (ReportName == "")
        {
            ShowReportIdCard("EmployeeICards", "EmployeeIdCard.rpt"); 
            
        }
        else
        {
           ShowReportIdCard("EmployeeICards", ReportName); 
           // ShowReportIdCard("EmployeeICards", "EmployeeIdCard.rpt"); 
        }
    }
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvItem in lvPayhead.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            if (chk.Checked == true)
            {
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
            }

        }

        selectedIDs = selectedIDs.Remove(selectedIDs.Length - 1);

        string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "FormatID=" + Convert.ToInt32(ddlIDCardType.SelectedValue));
        //if(ddlStaffName .SelectedIndex ==1)
        //ShowReport("EmployeeICards", "Pay_EmpIDCardBothSide.rpt");  
        //ShowReportIdCard("EmployeeICards", "rptEmpIDCardBackNew.rpt"); // old code
      
        ShowReportIdCard("EmployeeICards", ReportName); 


       // ShowReportIdCard("EmployeeICards", "rptEmpIDCardBackNew.rpt");
       
      
            //if(ddlStaffName .SelectedIndex ==1)
                //ShowReport("EmployeeICardsNonTeaching", "Pay_EmpIDSingleCol_New_Actual.rpt");
    }

    protected void btnBackSide_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvItem in lvPayhead.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            if (chk.Checked == true)
            {
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
            }
        }
        selectedIDs = selectedIDs.Remove(selectedIDs.Length - 1);

        ShowReportIdCard("EmployeeICards", "rptEmpIDCardBackNew.rpt");
      
               // ShowReport("EmployeeICardSingleBack", "Pay_IDcardSingleColBack_RuiaNew.rpt");
       
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlStaffName.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlOrderBy.SelectedIndex = 0;
        pnlPayhead.Visible = false;
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        ddlIDCardType.SelectedIndex = 0;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            
            CustomStatus cs = (CustomStatus)objPayHeadPrivilege.DeleteUser(Convert.ToInt32(ddlStaffName.SelectedValue));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                //lblmsg.Text = "Record Deleted Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Deleted Successfully",this);
                BindListViewPayHead(Convert.ToInt32(ddlStaffName.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

   private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string prePrinted = string.Empty;
            string photo = string.Empty;
            string color = string.Empty;
            string singleCol = string.Empty;

                color = "1";
           
                prePrinted = "0";
           
                singleCol = "0";
            
                photo = "1";
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("EMPIDCARD.aspx")));
            //url += "../../../Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,PAYROLL," + rptFileName;
            ////url += "&param=@P_IDNO=" + selectedIDs + ",@P_PrePrinted=" + prePrinted + ",@P_Color=" + color + ",@P_Photo=" + photo + ",@P_WorkingDate=" + Session["WorkingDate"].ToString().Trim() + ",@P_UserId=" + Session["useridname"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString() + ",@P_Session=" + Session["Session"].ToString();
            //url += "&param=@P_IDNO=" + selectedIDs + ",@P_Color=" + color + ",@P_Photo=" + photo ;

            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;


                url += "&path=~,Reports,Payroll," + rptFileName;

                //if(ViewState["action"].Equals("salaryCertificate"))
                url += "&param=@P_IDNO=" + selectedIDs + ",@P_Color=" + color + ",@P_Photo=" + photo;
                //+ "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
                //else
                //    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();

                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NoReport, Common.MessageType.Error);
        }
    }

   private void ShowReportIdCard(string reportTitle, string rptFileName)
   {
       try
       {
           string Script = string.Empty;
           string prePrinted = string.Empty;

           string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
           url += "Reports/CommonReport.aspx?";
           url += "pagetitle=" + reportTitle;   

           url += "&path=~,Reports,Payroll," + rptFileName;
           url += "&param=@P_IDNO=" + selectedIDs + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

           Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


       }
       catch (Exception ex)
       {
           //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NoReport, Common.MessageType.Error);
       }
   }


   protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
   {
       try
       {
           lblmsg.Text = string.Empty;
           lblerror.Text = string.Empty;

           if (!(ddlStaffName.SelectedValue == "0"))
           {

               int staffNo = Convert.ToInt32(ddlStaffName.SelectedValue);
               pnlPayhead.Visible = true;
               //PnlUserHead.Visible = true;
               pnlButton.Visible = true;
               BindListViewPayHead(staffNo);
               //BindListViewUser(staffNo);
           }
           else
           {
               pnlPayhead.Visible = false;
               // PnlUserHead.Visible = false;
              // pnlButton.Visible = false;
           }
       }
       catch (Exception ex)
       { 
       
       }
   }

   protected void ddlIDCardType_SelectedIndexChanged(object sender, EventArgs e)
   {

   }
}
