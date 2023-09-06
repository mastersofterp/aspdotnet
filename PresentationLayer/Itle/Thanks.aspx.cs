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
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class Itle_Thanks : System.Web.UI.Page
{
    Common objCommon = new Common();
    public void Page_Load(object sender, EventArgs e)
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

                lblUrname.Text = Convert.ToString(Session["USERFULLNAME"].ToString());

                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();


            }
            //lblThanx.Text = Convert.ToString(Session["USERFULLNAME"].ToString() + " Thank You For Appearing In Test");
            //DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + Convert.ToInt32(Test_No), "TESTNO");
            // DataSet ds = objCommon.FillDropDown("dbo.ITLE_TESTRESULT", "*", "CORRECTMARKS", "max(TESTNO)>0", "TESTNO");
            //string Testno = objCommon.LookUp("dbo.ITLE_TESTRESULT", "max(TESTNO)", "TESTNO>0");
            //DataSet ds = objCommon.FillDropDown("dbo.ITLE_TESTRESULT", "CORRECTMARKS","TOTALMARKS","TESTNO="+Testno, "TESTNO");
            //string Totalmark = ds.Tables[0].Rows[0]["TOTALMARKS"].ToString();
            //string CorrectMark = ds.Tables[0].Rows[0]["CORRECTMARKS"].ToString();
            //lblStudentMessage.Text = " Thanks for Attending the Test.And Your Score is " + CorrectMark + "Out of" + Totalmark;
        }
        divCollegename.InnerText = objCommon.LookUp("REFF", "COLLEGENAME", "");
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Session["TOTSCORE"] = 0;
        Session["TOTALMARKS"] = 0;
        Session["NOCORRANS"] = 0;
        Session["TOTANSQUE"] = 0;
        Session["Test_No_OBJ"] = string.Empty;
        Session["COURSENO_OBJ"] = string.Empty;
        Session["CURRTESTENDTIME"] = string.Empty;
        Session["TDNO_OBJ"] = string.Empty;
        Session["CurQuesIndex"] = string.Empty;
        Session["CurQuesNo_OBJ"] = string.Empty;
        Session["NextQuesNo"] = string.Empty;
        Session["PrevQuesNo"] = string.Empty;
        Session["TOTQUES"] = string.Empty;
        Session["ANSWER_TYPE"] = string.Empty;
        Response.Redirect("~/Itle/StudTest.aspx?pageno=1476");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            //int USERID ;

            //objTM.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            //objTM.TOPICNAME = txtTopicName.Text;     
            //if (ddlStudent.SelectedIndex > 0 & ddlTest.SelectedIndex > 0)
            //{
            //    objTM.IDNO = Convert.ToInt32(ddlStudent.SelectedValue);
            //    objTM.TEST_NO = Convert.ToInt32(ddlTest.SelectedValue);
            if (Session["TestType"].ToString() == "O")
            {

                ShowReport("Itle_Student_Result", "Itle_Student_Result_Photocopy.rpt");
            }
            else
            {
                ShowReport("Itle_Student_Result", "Itle_Descriptive_Result_Photocopy.rpt");
            }
            //}



        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["..error"]) == true)
                objCommon.ShowError(Page, "ITLE_Ans_Sheet.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Itle," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",IP_ADDRESS=" + Session["ipAddress"].ToString() + ",MAC_ADDRESS=" + Session["macAddress"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_IDNO=" + Session["idno"] + ",STUDNAME=" + Session["USERFULLNAME"].ToString() + ",TESTNAME=" + Session["TestName"].ToString() + ",@P_TESTNO=" + Convert.ToInt32(Session["Test_No"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Ans_Sheet.ShowReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
