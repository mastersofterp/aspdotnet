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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.Generic;

public partial class Itle_Student_Result_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TestResult objResult = new TestResult();
    int flag;

    #region Page Load

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               
                
                
                FillDropdown();

            }
        }
    }

    #endregion

    #region Commented Codes

    //protected void rbnStudentName_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckRadioButton();
    //}
    //protected void rbnStudentID_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckRadioButton();
    //}

    //Procedure for Checking radion buttond
    //public void CheckRadioButton()
    //{
    //    if (rbnStudentID.Checked == true)
    //    {
    //        txtStudent_Id.Enabled = true;
    //        txtStudent_Name.Enabled = false;
    //        txtStudent_Name.Text = string.Empty;
    //        lblID_Required.Text = string.Empty;
    //        lblName_Required.Text = string.Empty;

    //    }

    //    else
    //    {
    //        txtStudent_Name.Enabled = true;
    //        txtStudent_Id.Enabled = false;
    //        txtStudent_Id.Text = string.Empty;
    //        lblID_Required.Text = string.Empty;
    //        lblName_Required.Text = string.Empty;
    //    }


    //}

    // 

    //protected void btnShowReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReport("Itle_Student_Result", "Itle_Student_Result.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
    //    }
    //}

    #endregion

    protected void FillDropdown()
    {
        objCommon.FillDropDownList(ddlname, "ACD_STUDENT A INNER JOIN  acd_student_result S ON (A.IDNO = S.IDNO)", "A.STUDNAME", "STUDNAME", "S.COURSENO =" + Session["ICourseNo"], "A.STUDNAME");
        
    }
    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_Result_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_Result_Report.aspx");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_STUDID=" + ViewState["COUNTID"]; //hdn1.Value.Trim(); 
            //url += "&param=username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_STUDID=" + hdn1.Value.Trim() + ""; //  + ",COURSENAME=" + Session["ICourseName"].ToString() 
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void ShowReport1(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",COURSENAME=" + Session["ICourseName"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void btnAllStudentReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport1("Itle_Student_Result_For_All_Student", "Itle_Student_Result_All_Student.rpt");
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        { 
           // if (txtStudent_Id.Text != "" || txtStudent_Name.Text != "")
           if (txtStudent_Id.Text != "" || ddlname.SelectedItem != null)
            {
                 //int countId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "count(idno)", "ROLLNO<>'' AND ROLLNO IS NOT NULL AND ROLLNO='" + txtStudent_Id.Text.Trim() + "'"));
                if (txtStudent_Id.Text != "")
                {
                    int countId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "distinct IDNO", "REGNO='" + txtStudent_Id.Text.Trim() + "'"));
                    ViewState["COUNTID"] = countId;
                    if (countId > 0)
                    {
                        ShowReport("Itle_Student_Result", "Itle_Student_Result.rpt");
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.Page, "Roll No.is not valid.", this.Page);
                        return;
                    }
                }
                    else
                {
                    int countId1 = Convert.ToInt32(objCommon.LookUp(" ACD_STUDENT A INNER JOIN  acd_student_result S ON (A.IDNO = S.IDNO)", "DISTINCT A.IDNO", "A.STUDNAME='" + ddlname.SelectedItem.Text + "' AND S.COURSENO =" + Session["ICourseNo"] + ""));
                    ViewState["COUNTID"] = countId1;

                    if (countId1 > 0)
                    {
                        ShowReport("Itle_Student_Result", "Itle_Student_Result.rpt");
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.Page, "Name.is not valid.", this.Page);
                        return;
                    }
                }
           }

                else
                {
                    // objCommon.DisplayUserMessage(UpdatePanel1, "Please enter student Id or Name !", this.Page);
                    ShowMessage("Please enter student Id or Name !");
                    return;
                }

                }
               
               

      

        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.btnSubmit_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }


    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void rbtnSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnSearch.SelectedValue == "N")
            {
                txtStudent_Id.Enabled = false;
                //txtStudent_Name.Enabled = true;
                ddlname.Enabled = true;
            }
            else
            {
                txtStudent_Id.Enabled = true;
                //txtStudent_Name.Enabled = false;
                ddlname.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

}