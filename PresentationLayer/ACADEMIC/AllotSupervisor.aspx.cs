//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE-B                                                  
// CREATION DATE : 25-APRIL-2013                                                          
// CREATED BY    : ASHISH DHAKATE                             
// MODIFIED DATE :                 
// ADDED BY      :                                  
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_StudentInfoEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
        //imgPhoto.ImageUrl = "~/images/nophoto.jpg";
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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

                //Populate all the DropDownLists
                PopulateDropDownList();
                
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                

                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "2")
                {
                    pnlId.Visible = false;
                    
                    ShowStudentDetails();
                    ViewState["action"] = "edit";
                    
                }
                else
                {

                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "3")
                    {
                        //pnlDoc.Enabled = false;
                        pnlId.Enabled = false;
                        
                    }

                    pnlId.Visible = true;
                    lblRegNo.Enabled = true;
       
                   
                    if (Request.QueryString["id"] != null)
                    {
                       
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        //ShowSignDetails();
                       
                    }
                }
              
            }
            
        }
        else
        {

            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
    }

    protected void PopulateDropDownList()
    {
        //fill dropdown Supervisor
        this.objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "", "SUPERVISORNAME");
        this.objCommon.FillDropDownList(ddlCoSupevisor1, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "", "SUPERVISORNAME");
        this.objCommon.FillDropDownList(ddlCoSupevisor2, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "", "SUPERVISORNAME");
    }

    private void ChangeControlStatus(bool status)
    {

        foreach (Control c in Page.Controls)
            foreach (Control ctrl in c.Controls)

                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = status;

                else if (ctrl is Button)

                    ((Button)ctrl).Enabled = status;

                else if (ctrl is RadioButton)

                    ((RadioButton)ctrl).Enabled = status;

                else if (ctrl is ImageButton)

                    ((ImageButton)ctrl).Enabled = status;

                else if (ctrl is CheckBox)

                    ((CheckBox)ctrl).Enabled = status;

                else if (ctrl is DropDownList)

                    ((DropDownList)ctrl).Enabled = status;

                else if (ctrl is HyperLink)

                    ((HyperLink)ctrl).Enabled = status;

    }

    private void ShowStudentDetails()
    {

        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
           
        }
        else
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
            pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtIDNo.Text = dtr["IDNO"].ToString();
                //txtIDNo.ToolTip = dtr["REGNO"].ToString();
                lblRegNo.ToolTip = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ROLLNO"].ToString();
                lblRegNo.Text = dtr["IDNO"].ToString();
                //txtRegNo.Enabled = false;
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
                if (lblStatus.Text == "2")
                {
                    lblStatus.Text = "PART TIME";
                }
                else
                {
                    lblStatus.Text = "FULL TIME";
                }
                
                //lblSupervisor.Text = dtr["PHDSUPERVISORNAME"] == null ? string.Empty : dtr["PHDSUPERVISORNAME"].ToString();
                //lblSupervisor.ToolTip = dtr["PHDSUPERVISORNO"] == null ? string.Empty : dtr["PHDSUPERVISORNO"].ToString();
               

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
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }

        Response.Redirect(url + "&id=" + lnk.CommandArgument);


    }

    private void bindlist(string category, string searchtext)
    {

        int dept = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

        if (dept > 0)
        {
            string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "DEGREENO=3 AND DEPTNO=" + dept);


            StudentController objSC = new StudentController();
            DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
                lblNoRecords.Text = "Total Records : 0";
        }
        else
        {
            string branchno = "0";

            StudentController objSC = new StudentController();
            DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
                lblNoRecords.Text = "Total Records : 0";
        }
    }

  

    

  

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    protected void btnReport_Click1(object sender, EventArgs e)
    {
        ShowReport("PhdProgressReport", "rptAnnexureC.rpt");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StudentController objSC = new StudentController();
        Student objS = new Student();

        try
        {
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            int supervisor = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT","PHDSUPERVISORNO","iDNO="+ txtIDNo.Text.Trim()));

            if (supervisor > 0)
            {
            if (ua_type == "1" || ua_type == "2" || (ua_type == "3" && ua_dec == "0"))
            {

                objS.IdNo = Convert.ToInt32(txtIDNo.Text);
               
               
                objS.PhdSupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
                objS.PhdCoSupervisorNo1 = Convert.ToInt32(ddlCoSupevisor1.SelectedValue);
                objS.PhdCoSupervisorNo2  = Convert.ToInt32(ddlCoSupevisor2.SelectedValue);
                objS.TypeSupervisorNo  = Convert.ToInt32(ddlSupervisorType.SelectedValue);
                objS.TypeCoSupervisorNo1 = Convert.ToInt32(ddlCoSupervisorType1.SelectedValue);
                objS.TypeCoSupervisorNo2 = Convert.ToInt32(ddlCoSupervisorType1.SelectedValue);
                objS.CollegeCode = Session["colcode"].ToString();

                //UANO SAVE
                if (ua_type == "1" || ua_type == "3")
                {
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    objS.Uano = 0;
                }

                // UPDATE THE ONLY PHD STUDENT DATA THROUGH STUDENT
                if (ua_type == "1" || ua_type == "2")
                {
                    string output = objSC.UpdateStudentSupervisorInfo(objS);
                    if (output != "-99")
                    {
                        Session["qualifyTbl"] = null;
                        objCommon.DisplayMessage("Student Information Updated Successfully!!", this.Page);
                        this.ShowStudentDetails();
                    }
                }
            }
            }
            else
            {
                objCommon.DisplayMessage("Already allot the Supervisor", this.Page);
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }

    }

    protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSupervisorType.Text = objCommon.LookUp("ACD_PHD_SUPERVISOR", "TYPENAME", "SUPERVISORNO=" + ddlSupervisor.SelectedValue);
        ddlSupervisorType.Focus();
    }

    protected void ddlCoSupevisor1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCoSupervisorType1.Text = objCommon.LookUp("ACD_PHD_SUPERVISOR", "TYPENAME", "SUPERVISORNO=" + ddlCoSupevisor1.SelectedValue);
        ddlCoSupervisorType1.Focus();
    }

    protected void ddlCoSupevisor2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCoSupervisorType2.Text = objCommon.LookUp("ACD_PHD_SUPERVISOR", "TYPENAME", "SUPERVISORNO=" + ddlCoSupevisor2.SelectedValue);
        ddlCoSupervisorType2.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        this.ClearControl();
    }
    private void ClearControl()
    {
        txtIDNo.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
        lblStudName.Text = string.Empty;
        lblFatherName.Text = string.Empty;
        lblDateOfJoining.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblStatus.Text = string.Empty;
        ddlSupervisor.SelectedIndex = 0;
        ddlCoSupevisor1.SelectedIndex = 0;
        ddlCoSupevisor2.SelectedIndex = 0;
        ddlSupervisorType.SelectedIndex = 0;
        ddlCoSupervisorType1.SelectedIndex = 0;
        ddlCoSupervisorType2.SelectedIndex = 0;
        
      
    }
}

   

