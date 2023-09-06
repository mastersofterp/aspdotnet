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

public partial class Itle_ITLE_Ans_Sheet : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    ITestMasterController objTC = new ITestMasterController();
    ITestMaster objTM = new ITestMaster();

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

        try
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

                    lblSession.Text = Session["SESSION_NAME"].ToString();
                    lblSession.ToolTip = Session["SessionNo"].ToString();
                    lblSession.ForeColor = System.Drawing.Color.Green;
                    lblCorseName.ForeColor = System.Drawing.Color.Green;
                    lblCorseName.Text = Session["ICourseName"].ToString();
                    //PopulateDropdown();

                }
                FillDropdown();
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Ans_Sheet.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ITLE_Ans_Sheet.aspxx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ITLE_Ans_Sheet.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            if (rbtTestType.SelectedValue == "O")
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND TEST_TYPE='O'", "TESTNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND TEST_TYPE='D'", "TESTNO");

            }


        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ITLE_Ans_Sheet.FillDropdown-> " + ex.Message + " " + ex.StackTrace);

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
            url += "&path=~,Reports,Itle," + rptFileName;
            url += "&param=@P_IDNO=" + ddlStudent.SelectedValue + ",@P_TESTNO=" + ddlTest.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString());
            //url += "&param=username=" + Session["username"].ToString() + ",IP_ADDRESS=" + Session["ipAddress"].ToString() + ",MAC_ADDRESS=" + Session["macAddress"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_IDNO=" + ddlStudent.SelectedValue + ",STUDNAME=" + ddlStudent.SelectedItem.Text + ",TESTNAME=" + ddlTest.SelectedItem.Text + ",@P_TESTNO=" + ddlTest.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //int USERID ;

            //objTM.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            //objTM.TOPICNAME = txtTopicName.Text;     
            if (ddlStudent.SelectedIndex > 0 & ddlTest.SelectedIndex > 0)
            {
                objTM.IDNO = Convert.ToInt32(ddlStudent.SelectedValue);
                objTM.TEST_NO = Convert.ToInt32(ddlTest.SelectedValue);
                if (Session["Test_Type"].ToString() == "O")
                {
                    ShowReport("Itle_Student_Result", "Itle_Student_Result_Photocopy.rpt");
                }
                else
                {
                    ShowReport("Itle_Student_Result", "Itle_Descriptive_Result_Photocopy.rpt");

                }
            }
            else
            {
                objCommon.DisplayUserMessage(updPanel1, "Please select Test and Student !", this.Page);
                
            }



        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Ans_Sheet.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void Hdn1_ValueChanged(object sender, EventArgs e)
    {

        //DataSet ds = objCommon.FillDropDown("LIB_AUTHOR", "*", "", "AUTHORNO='" + Hdn1.Value.ToString().Trim() + "'", "AUTHORNAME");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    txtaname.Text = ds.Tables[0].Rows[0]["AUTHORNAME"].ToString().Trim();
        //    Hdn1.Value = ds.Tables[0].Rows[0]["AUTHORNO"].ToString().Trim();
        //}

        //      string changeScript = "&lt;script language='javascript'> function SomeValueChanged() {" +
        //"document.getElementById('MonitorChangeControl').Text = 'Some values may have been changed.'; }</script>";
        // Add the JavaScript code to the page.
        //if (!ClientScript.IsClientScriptBlockRegistered("SomeValueChanged"))
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "SomeValueChanged", changeScript);
        //}
    }

    //protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlTest, "ITLE_TESTMASTER TM JOIN ITLE_RESULTCOPY RC ON(TM.TESTNO=RC.TESTNO)", "DISTINCT TM.TESTNO", "TM.TESTNAME", "RC.UA_NO=" + Convert.ToInt32(ddlStudent.SelectedValue) , "TM.TESTNAME");
    //}

    protected void btnShowpg_Click(object sender, EventArgs e)
    {
        objTM.IDNO = Convert.ToInt32(ddlStudent.SelectedValue);
        objTM.TEST_NO = Convert.ToInt32(ddlTest.SelectedValue);
        Response.Redirect("ITLE_Result_Copy.aspx?id=" + objTM.IDNO + "&testno=" + objTM.TEST_NO);
    }

    protected void ddlTest_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {

            Session["Test_Type"] = objCommon.LookUp("ACD_ITESTMASTER", "TEST_TYPE", "TESTNO=" + ddlTest.SelectedValue);

            objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S JOIN ACD_ITEST_RESULT TR ON (S.IDNO=TR.IDNO) JOIN ACD_STUDENT_RESULT SR ON (SR.IDNO=TR.IDNO)", "DISTINCT S.IDNO", "S.STUDNAME", "SR.SESSIONNO=" + Session["SessionNo"] + "AND TR.COURSENO=" + Session["ICourseNo"] + "AND TR.TESTNO=" + ddlTest.SelectedValue, "IDNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Ans_Sheet.ddlTest_SelectedIndexChanged1-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void rbtTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtTestType.SelectedValue == "O")
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND TEST_TYPE='O'", "TESTNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND TEST_TYPE='D'", "TESTNO");

            }


        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ITLE_Ans_Sheet.rbtTestType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);

        }
    }

    #endregion
}

