using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_UpdateExamNo : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //fill dropdown
                PopulateDropDown();
                //get ip address
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
            divMsg.InnerHtml = string.Empty;
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UpdateExamNo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UpdateExamNo.aspx");
        }
    }

    #endregion

    #region Form Events

    // clear all Items
    private void clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
    }
    #endregion

    #region Private Methods

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetStudentsExamNo_RollNo(Convert.ToInt32(ddlAdmBatch.SelectedValue),Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
           // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
      
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    // Bind Branch  
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlClgname.SelectedValue, "A.LONGNAME");

    }

    // Show selected student 
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "distinct A.STUDNAME", "A.ENROLLNO,A.REGNO,A.IDNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlSemester.SelectedValue +"  AND A.ADMCAN = 0 ", "A.IDNO");
            if (dsStudent.Tables[0].Rows.Count > 0 && dsStudent != null && dsStudent.Tables.Count > 0)
            {
                //  DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO", "A.STUDNAME", "A.IDNO,A.ENROLLNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " AND A.DEGREENO =  " + ddlDegree.SelectedValue + " AND A.BRANCHNO =  " + ddlBranch.SelectedValue + "AND B.COLLEGE_ID =  " + ddlClgname.SelectedValue + " AND ADMCAN = 0 ", "IDNO");
                lvStudents.Visible = true;
                lvStudents.DataSource = dsStudent.Tables[0];
                lvStudents.DataBind();
                hdnTot.Value = dsStudent.Tables[0].Rows.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(updStudent,"Student not Available for this Selection", this.Page);
                lvStudents.Visible = false;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@UserName=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGEID=" + Convert.ToInt32(ddlClgname.SelectedValue) +",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",IPADDRESS=" + ViewState["ipAddress"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    // registration No. Alllot Report
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowReport("RollNo_Allotment_Report", "rptExamNoAllotment.rpt");
            ShowReport("RollNo_Allotment_Report", "rptRollNoAllotment.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnGenerate_Click1(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            string idnochk = string.Empty;
            string regno = string.Empty;
            string enrollnos2 = string.Empty;
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                
                CheckBox idno = ((lvItem.FindControl("cbRow") as CheckBox));
                if (idno.Checked == true)
                {
                    idnochk = "1";

                    TextBox enrollnos = lvItem.FindControl("txtRollNo") as TextBox;
                    string enroll = objCommon.LookUp("ACD_STUDENT", "regno", "regno='" + enrollnos.Text + "'");

                    if (enrollnos.Text != string.Empty && enrollnos.Text != "" && enroll != enrollnos.Text.ToString())
                    {
                        regno += ((lvItem.FindControl("hfdIdno")) as HiddenField).Value + "$";
                        enrollnos2 += (lvItem.FindControl("txtRollNo") as TextBox).Text + "$";
                    }
                    else
                    {
                        objCommon.DisplayMessage(updStudent,"Registration  No. is allready allocated.", this.Page);
                        return;
                    }
                }
                
            }
            if (idnochk == string.Empty)
            {
                objCommon.DisplayMessage(updStudent,"Please select atleast Single Student to Modify Exam No.", this.Page);

            }
            //if (regno != "")
            //{
            //    if (regno.Substring(regno.Length - 1) == "")
            //        regno = regno.Substring(0, regno.Length - 1);
            //}


            if (regno.Length <= 0 && enrollnos2.Length <= 0)
            {
                objCommon.DisplayMessage(updStudent,"Please Select Student/Registration No.", this.Page);
                return;
            }

            if (objSC.UpdateStudentRegNumber(regno, enrollnos2) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                clear();
                objCommon.DisplayMessage(updStudent,"Exam No. Alloted Successfully!!!", this.Page);
            }
            else
                objCommon.DisplayMessage(updStudent,"Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_UpdateRegistrationNo.btnGenerate_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlClgname.SelectedValue + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
    }
    // Bind Semester 
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue, "SM.SEMESTERNO");
    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        clear();
    }
}