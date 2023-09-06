using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.IO;

public partial class ACADEMIC_EXAMINATION_BulkDetaintionEntry : System.Web.UI.Page
{
    ExamController excol = new ExamController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "4") || (Session["usertype"].ToString() == "1"))
                {
                    //check availibility
                    CheckActivity();//[23-09-2016]
                    this.FillDropdown();
                }
                else
                {
                    objCommon.DisplayMessage(this.updDetained, "You are not authorized to view this page!!", this.Page);
                }
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            ddlDegree.Focus();
        }
    }


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            //Term.
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
           // objCommon.FillDropDownList(ddlSessionCancel, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //Degree Name
            if (Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1")
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            else
                objCommon.FillDropDownList(ddlDegree, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND DEPTNO =  " + Session["userdeptno"].ToString(), "D.DEGREENO");

            if (Session["usertype"].ToString() == "3" && (Session["username"].ToString().ToUpper() == "HODCHEMISTRY" || Convert.ToString(Session["username"]) == "HOD_PHY"))
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO = 1", "DEGREENO");
            }
           // objCommon.FillDropDownList(ddlDegreeCancel, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            ddlSession.SelectedIndex = -1;
            ddlSession.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH BR ON (CD.BRANCHNO=BR.BRANCHNO)", "CD.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
                ddlBranch.Focus();
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        sessionno = Session["currentsession"].ToString();
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                updDetained.Visible = false;

            }
            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                updDetained.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            updDetained.Visible = false;
        }
        dtr.Close();
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Branch!", this.Page);
                    ddlBranch.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Scheme!", this.Page);
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShowStudentlist_Click(object sender, EventArgs e)
    {
          GetDetainStudList();
    }


    private void GetDetainStudList()
    {
        try
        {

            DataTable dt = new DataTable();

            DataSet ds = excol.GetAttendanceData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
            if (ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
            }
            else
            {
                dt = ds.Tables[0];
                if (!dt.Columns.Contains("PERCENTAGE1"))
                {
                    dt.Columns.Add("PERCENTAGE1", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS1"))
                {
                    dt.Columns.Add("STATUS1", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE2"))
                {
                    dt.Columns.Add("PERCENTAGE2", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS2"))
                {
                    dt.Columns.Add("STATUS2", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE3"))
                {
                    dt.Columns.Add("PERCENTAGE3", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS3"))
                {
                    dt.Columns.Add("STATUS3", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE4"))
                {
                    dt.Columns.Add("PERCENTAGE4", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS4"))
                {
                    dt.Columns.Add("STATUS4", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE5"))
                {
                    dt.Columns.Add("PERCENTAGE5", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS5"))
                {
                    dt.Columns.Add("STATUS5", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE6"))
                {
                    dt.Columns.Add("PERCENTAGE6", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS6"))
                {
                    dt.Columns.Add("STATUS6", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE7"))
                {
                    dt.Columns.Add("PERCENTAGE7", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS7"))
                {
                    dt.Columns.Add("STATUS7", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE8"))
                {
                    dt.Columns.Add("PERCENTAGE8", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS8"))
                {
                    dt.Columns.Add("STATUS8", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE9"))
                {
                    dt.Columns.Add("PERCENTAGE9", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS9"))
                {
                    dt.Columns.Add("STATUS9", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE10"))
                {
                    dt.Columns.Add("PERCENTAGE10", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS10"))
                {
                    dt.Columns.Add("STATUS10", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE11"))
                {
                    dt.Columns.Add("PERCENTAGE11", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS11"))
                {
                    dt.Columns.Add("STATUS11", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE12"))
                {
                    dt.Columns.Add("PERCENTAGE12", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS12"))
                {
                    dt.Columns.Add("STATUS12", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE13"))
                {
                    dt.Columns.Add("PERCENTAGE13", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS13"))
                {
                    dt.Columns.Add("STATUS13", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE14"))
                {
                    dt.Columns.Add("PERCENTAGE14", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS14"))
                {
                    dt.Columns.Add("STATUS14", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE15"))
                {
                    dt.Columns.Add("PERCENTAGE15", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS15"))
                {
                    dt.Columns.Add("STATUS15", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE16"))
                {
                    dt.Columns.Add("PERCENTAGE16", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS16"))
                {
                    dt.Columns.Add("STATUS16", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE17"))
                {
                    dt.Columns.Add("PERCENTAGE17", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS17"))
                {
                    dt.Columns.Add("STATUS17", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE18"))
                {
                    dt.Columns.Add("PERCENTAGE18", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS18"))
                {
                    dt.Columns.Add("STATUS18", typeof(string));
                }
                if (!dt.Columns.Contains("PERCENTAGE19"))
                {
                    dt.Columns.Add("PERCENTAGE19", typeof(string));
                }
                if (!dt.Columns.Contains("STATUS19"))
                {
                    dt.Columns.Add("STATUS19", typeof(string));
                }
               
                if (!dt.Columns.Contains("FINAL_COURSE_CODE1"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE1", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND1"))
                {
                    dt.Columns.Add("DETAIND1", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE2"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE2", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND2"))
                {
                    dt.Columns.Add("DETAIND2", typeof(string));
                }

                if (!dt.Columns.Contains("FINAL_COURSE_CODE3"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE3", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND3"))
                {
                    dt.Columns.Add("DETAIND3", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE4"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE4", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND4"))
                {
                    dt.Columns.Add("DETAIND4", typeof(string));
                }

                if (!dt.Columns.Contains("FINAL_COURSE_CODE5"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE5", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND5"))
                {
                    dt.Columns.Add("DETAIND5", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE6"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE6", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND6"))
                {
                    dt.Columns.Add("DETAIND6", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE7"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE7", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND7"))
                {
                    dt.Columns.Add("DETAIND7", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE8"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE8", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND8"))
                {
                    dt.Columns.Add("DETAIND8", typeof(string));
                }

                if (!dt.Columns.Contains("FINAL_COURSE_CODE9"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE9", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND9"))
                {
                    dt.Columns.Add("DETAIND9", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE10"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE10", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND10"))
                {
                    dt.Columns.Add("DETAIND10", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE11"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE11", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND11"))
                {
                    dt.Columns.Add("DETAIND11", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE12"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE12", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND12"))
                {
                    dt.Columns.Add("DETAIND12", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE13"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE13", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND13"))
                {
                    dt.Columns.Add("DETAIND13", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE14"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE14", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND14"))
                {
                    dt.Columns.Add("DETAIND14", typeof(string));
                }

                if (!dt.Columns.Contains("FINAL_COURSE_CODE15"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE15", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND15"))
                {
                    dt.Columns.Add("DETAIND15", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE16"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE16", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND16"))
                {
                    dt.Columns.Add("DETAIND16", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE17"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE17", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND17"))
                {
                    dt.Columns.Add("DETAIND17", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE18"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE18", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND18"))
                {
                    dt.Columns.Add("DETAIND18", typeof(string));
                }
                if (!dt.Columns.Contains("FINAL_COURSE_CODE19"))
                {
                    dt.Columns.Add("FINAL_COURSE_CODE19", typeof(string));
                }
                if (!dt.Columns.Contains("DETAIND19"))
                {
                    dt.Columns.Add("DETAIND19", typeof(string));
                }




                lvCourse.DataSource = dt;
                lvCourse.DataBind();

                foreach (ListViewDataItem item in lvCourse.Items)
                {
                    CheckBox chk = item.FindControl("lblcode1") as CheckBox;
                    CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
                    CheckBox CheckBox2 = item.FindControl("CheckBox2") as CheckBox;
                    CheckBox CheckBox3 = item.FindControl("CheckBox3") as CheckBox;
                    CheckBox CheckBox4 = item.FindControl("CheckBox4") as CheckBox;
                    CheckBox CheckBox5 = item.FindControl("CheckBox5") as CheckBox;
                    CheckBox CheckBox6 = item.FindControl("CheckBox6") as CheckBox;
                    CheckBox CheckBox7 = item.FindControl("CheckBox7") as CheckBox;
                    CheckBox CheckBox8 = item.FindControl("CheckBox8") as CheckBox;
                    CheckBox CheckBox9 = item.FindControl("CheckBox9") as CheckBox;
                    CheckBox CheckBox10 = item.FindControl("CheckBox10") as CheckBox;
                    CheckBox CheckBox11 = item.FindControl("CheckBox11") as CheckBox;
                    CheckBox CheckBox12 = item.FindControl("CheckBox12") as CheckBox;
                    CheckBox CheckBox13 = item.FindControl("CheckBox13") as CheckBox;
                    CheckBox CheckBox14 = item.FindControl("CheckBox14") as CheckBox;
                    CheckBox CheckBox15 = item.FindControl("CheckBox15") as CheckBox;
                    CheckBox CheckBox16 = item.FindControl("CheckBox16") as CheckBox;
                    CheckBox CheckBox17 = item.FindControl("CheckBox17") as CheckBox;
                    CheckBox CheckBox18 = item.FindControl("CheckBox18") as CheckBox;
                    Label chlbl1 = item.FindControl("chlbl1") as Label;
                    Label chlbl2 = item.FindControl("chlbl2") as Label;
                    Label chlbl3 = item.FindControl("chlbl3") as Label;
                    Label chlbl4 = item.FindControl("chlbl4") as Label;
                    Label chlbl5 = item.FindControl("chlbl5") as Label;
                    Label chlbl6 = item.FindControl("chlbl6") as Label;
                    Label chlbl7 = item.FindControl("chlbl7") as Label;
                    Label chlbl8 = item.FindControl("chlbl8") as Label;
                    Label chlbl9 = item.FindControl("chlbl9") as Label;
                    Label chlbl10 = item.FindControl("chlbl10") as Label;
                    Label chlbl11 = item.FindControl("chlbl11") as Label;
                    Label chlbl12 = item.FindControl("chlbl12") as Label;
                    Label chlbl13 = item.FindControl("chlbl13") as Label;
                    Label chlbl14 = item.FindControl("chlbl14") as Label;
                    Label chlbl15 = item.FindControl("chlbl15") as Label;
                    Label chlbl16 = item.FindControl("chlbl16") as Label;
                    Label chlbl17 = item.FindControl("chlbl17") as Label;
                    Label chlbl18 = item.FindControl("chlbl18") as Label;
                    Label chlbl19 = item.FindControl("chlbl19") as Label;

                    if (chlbl1.Text == "")
                    {
                        chlbl1.Text = "0.00";
                    }
                    if (chlbl2.Text == "")
                    {
                        chlbl2.Text = "0.00";
                    }
                    if (chlbl3.Text == "")
                    {
                        chlbl3.Text = "0.00";
                    }
                    if (chlbl4.Text == "")
                    {
                        chlbl4.Text = "0.00";
                    }
                    if (chlbl5.Text == "")
                    {
                        chlbl5.Text = "0.00";
                    }
                    if (chlbl6.Text == "")
                    {
                        chlbl6.Text = "0.00";
                    }
                    if (chlbl7.Text == "")
                    {
                        chlbl7.Text = "0.00";
                    }
                    if (chlbl8.Text == "")
                    {
                        chlbl8.Text = "0.00";
                    }
                    if (chlbl9.Text == "")
                    {
                        chlbl9.Text = "0.00";
                    }
                    if (chlbl10.Text == "")
                    {
                        chlbl10.Text = "0.00";
                    }
                    if (chlbl11.Text == "")
                    {
                        chlbl11.Text = "0.00";
                    }
                    if (chlbl12.Text == "")
                    {
                        chlbl12.Text = "0.00";
                    }
                    if (chlbl13.Text == "")
                    {
                        chlbl13.Text = "0.00";
                    }
                    if (chlbl14.Text == "")
                    {
                        chlbl14.Text = "0.00";
                    }
                    if (chlbl15.Text == "")
                    {
                        chlbl15.Text = "0.00";
                    }
                    if (chlbl16.Text == "")
                    {
                        chlbl16.Text = "0.00";
                    }
                    if (chlbl17.Text == "")
                    {
                        chlbl17.Text = "0.00";
                    }
                    if (chlbl18.Text == "")
                    {
                        chlbl18.Text = "0.00";
                    }
                    if (chlbl19.Text == "")
                    {
                        chlbl19.Text = "0.00";
                    }
                    if (Convert.ToDouble(chlbl1.Text) < 85.00 || chlbl1.ToolTip == "CIE-FAIL")
                    {
                        chk.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        chk.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl2.Text) < 85.00 || chlbl2.ToolTip == "CIE-FAIL")
                    {
                        CheckBox1.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox1.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl3.Text) < 85.00 || chlbl3.ToolTip == "CIE-FAIL")
                    {
                        CheckBox2.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox2.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl4.Text) < 85.00 || chlbl4.ToolTip == "CIE-FAIL")
                    {
                        CheckBox3.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox3.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl5.Text) < 85.00 || chlbl5.ToolTip == "CIE-FAIL")
                    {
                        CheckBox4.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox4.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl6.Text) < 85.00 || chlbl6.ToolTip == "CIE-FAIL")
                    {
                        CheckBox5.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox5.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl7.Text) < 85.00 || chlbl7.ToolTip == "CIE-FAIL")
                    {
                        CheckBox6.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox6.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl8.Text) < 85.00 || chlbl8.ToolTip == "CIE-FAIL")
                    {
                        CheckBox7.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox7.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl9.Text) < 85.00 || chlbl9.ToolTip == "CIE-FAIL")
                    {
                        CheckBox8.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox8.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl10.Text) < 85.00 || chlbl10.ToolTip == "CIE-FAIL")
                    {
                        CheckBox9.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox9.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl11.Text) < 85.00 || chlbl11.ToolTip == "CIE-FAIL")
                    {
                        CheckBox10.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox10.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl12.Text) < 85.00 || chlbl12.ToolTip == "CIE-FAIL")
                    {
                        CheckBox11.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox11.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl13.Text) < 85.00 || chlbl13.ToolTip == "CIE-FAIL")
                    {
                        CheckBox12.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox12.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl14.Text) < 85.00 || chlbl14.ToolTip == "CIE-FAIL")
                    {
                        CheckBox13.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox13.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl15.Text) < 85.00 || chlbl15.ToolTip == "CIE-FAIL")
                    {
                        CheckBox14.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox14.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl16.Text) < 85.00 || chlbl16.ToolTip == "CIE-FAIL")
                    {
                        CheckBox15.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox15.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl17.Text) < 85.00 || chlbl17.ToolTip == "CIE-FAIL")
                    {
                        CheckBox16.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox16.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl18.Text) < 85.00 || chlbl18.ToolTip == "CIE-FAIL")
                    {
                        CheckBox17.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox17.ForeColor = System.Drawing.Color.Green;
                    }
                    if (Convert.ToDouble(chlbl19.Text) < 85.00 || chlbl19.ToolTip == "CIE-FAIL")
                    {
                        CheckBox18.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        CheckBox18.ForeColor = System.Drawing.Color.Green;
                    }




                    if (chk.Text == "" )
                    {
                        chk.Visible = false;
                    }
                    if (chk.ToolTip == "1")
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                           
                    }
                    
                    if (CheckBox1.Text == "")
                    {
                        CheckBox1.Visible = false;
                    }
                    if (CheckBox1.ToolTip == "1")
                    {
                        CheckBox1.Checked = true;
                        CheckBox1.Enabled = false;

                    }
                    
                    if (CheckBox2.Text == "")
                    {
                        CheckBox2.Visible = false;
                      
                    }
                    if (CheckBox2.ToolTip == "1")
                    {
                        CheckBox2.Checked = true;
                        CheckBox2.Enabled = false;
                    }
                    if (CheckBox3.Text == "")
                    {
                        CheckBox3.Visible = false;

                    }
                    if (CheckBox3.ToolTip == "1")
                    {
                        CheckBox3.Checked = true;
                        CheckBox3.Enabled = false;
                    }
                    if (CheckBox4.Text == "")
                    {
                        CheckBox4.Visible = false;
                    }
                    if (CheckBox4.ToolTip == "1")
                    {
                        CheckBox4.Checked = true;
                        CheckBox4.Enabled = false;

                    }
                    if (CheckBox5.Text == "")
                    {
                        CheckBox5.Visible = false;
                    }
                    if (CheckBox5.ToolTip == "1")
                    {
                        CheckBox5.Checked = true;
                        CheckBox5.Enabled = false;
                    }
                    if (CheckBox6.Text == "")
                    {
                        CheckBox6.Visible = false;
                    }
                    if (CheckBox6.ToolTip == "1")
                    {
                        CheckBox6.Checked = true;
                        CheckBox6.Enabled = false;
                    }
                    if (CheckBox7.Text == "")
                    {
                        CheckBox7.Visible = false;
                    }
                    if (CheckBox7.ToolTip == "1")
                    {
                        CheckBox7.Checked = true;
                        CheckBox7.Enabled = false;
                    }

                    if (CheckBox8.Text == "")
                    {
                        CheckBox8.Visible = false;
                    }
                    if (CheckBox8.ToolTip == "1")
                    {
                        CheckBox8.Checked = true;
                        CheckBox8.Enabled = false;
                    }
                    if (CheckBox9.Text == "")
                    {
                        CheckBox9.Visible = false;
                    }
                    if (CheckBox9.ToolTip == "1")
                    {
                        CheckBox9.Checked = true;
                        CheckBox9.Enabled = false;

                    }
                    if (CheckBox10.Text == "")
                    {
                        CheckBox10.Visible = false;
                    }
                    if (CheckBox10.ToolTip == "1")
                    {
                        CheckBox10.Checked = true;
                        CheckBox10.Enabled = false;

                    }
                    if (CheckBox11.Text == "")
                    {
                        CheckBox11.Visible = false;
                    }
                    if (CheckBox11.ToolTip == "1")
                    {
                        CheckBox11.Checked = true;
                        CheckBox11.Enabled = false;

                    }
                    if (CheckBox12.Text == "")
                    {
                        CheckBox12.Visible = false;
                      
                    }
                    if (CheckBox12.ToolTip == "1")
                    {
                        CheckBox12.Checked = true;
                        CheckBox12.Enabled = false;

                    }
                    if (CheckBox13.Text == "")
                    {
                        CheckBox13.Visible = false;
                    }
                    if (CheckBox13.ToolTip == "1")
                    {
                        CheckBox13.Checked = true;
                        CheckBox13.Enabled = false;
                    }
                    if (CheckBox14.Text == "")
                    {
                        CheckBox14.Visible = false;
                    }
                    if (CheckBox14.ToolTip == "1")
                    {
                        CheckBox14.Checked = true;
                        CheckBox14.Enabled = false;
                    }
                    if (CheckBox15.Text == "")
                    {
                        CheckBox15.Visible = false;
                    }
                    if (CheckBox15.ToolTip == "1")
                    {
                        CheckBox15.Checked = true;
                        CheckBox15.Enabled = false;
                    }
                    if (CheckBox16.Text == "")
                    {
                        CheckBox16.Visible = false;
                    }
                    if (CheckBox16.ToolTip == "1")
                    {
                        CheckBox16.Checked = true;
                        CheckBox16.Enabled = false;
                    }
                    if (CheckBox17.Text == "")
                    {
                        CheckBox17.Visible = false;
                    }
                    if (CheckBox17.ToolTip == "1")
                    {
                        CheckBox17.Checked = true;
                        CheckBox17.Enabled = false;
                    }
                    if (CheckBox18.Text == "")
                    {
                        CheckBox18.Visible = false;
                    }
                    if (CheckBox18.ToolTip == "1")
                    {
                        CheckBox18.Checked = true;
                        CheckBox18.Enabled = false;
                    }


                }

            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AcademicCalenderMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                //objCommon.DisplayMessage(updBarcode, "Server UnAvailable", this.Page);
            }
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int uano = Convert.ToInt32(Session["userno"].ToString());
        string ipaddress = ViewState["ipAddress"].ToString();
        if (lvCourse.Items.Count > 0)
        {

            foreach (ListViewDataItem item in lvCourse.Items)
            {

                string ExistCourses = string.Empty;
                string ExistCourses1 = string.Empty;
                HiddenField hdnval = item.FindControl("hdnenroll") as HiddenField;
                CheckBox chk = item.FindControl("lblcode1") as CheckBox;
                CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
                CheckBox CheckBox2 = item.FindControl("CheckBox2") as CheckBox;
                CheckBox CheckBox3 = item.FindControl("CheckBox3") as CheckBox;
                CheckBox CheckBox4 = item.FindControl("CheckBox4") as CheckBox;
                CheckBox CheckBox5 = item.FindControl("CheckBox5") as CheckBox;
                CheckBox CheckBox6 = item.FindControl("CheckBox6") as CheckBox;
                CheckBox CheckBox7 = item.FindControl("CheckBox7") as CheckBox;
                CheckBox CheckBox8 = item.FindControl("CheckBox8") as CheckBox;
                CheckBox CheckBox9 = item.FindControl("CheckBox9") as CheckBox;
                CheckBox CheckBox10 = item.FindControl("CheckBox10") as CheckBox;
                CheckBox CheckBox11 = item.FindControl("CheckBox11") as CheckBox;
                CheckBox CheckBox12 = item.FindControl("CheckBox12") as CheckBox;
                CheckBox CheckBox13 = item.FindControl("CheckBox13") as CheckBox;
                CheckBox CheckBox14 = item.FindControl("CheckBox14") as CheckBox;
                CheckBox CheckBox15 = item.FindControl("CheckBox15") as CheckBox;
                CheckBox CheckBox16 = item.FindControl("CheckBox16") as CheckBox;

                if (chk.Checked == true)
                {
                    ExistCourses += ((item.FindControl("lblcode1")) as CheckBox).Text + "$";
                }

                if (CheckBox1.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox1")) as CheckBox).Text + "$";
                }
                if (CheckBox2.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox2")) as CheckBox).Text + "$";
                }
                if (CheckBox3.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox3")) as CheckBox).Text + "$";
                }

                if (CheckBox4.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox4")) as CheckBox).Text + "$";
                }

                if (CheckBox5.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox5")) as CheckBox).Text + "$";
                }

                if (CheckBox6.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox6")) as CheckBox).Text + "$";
                }

                if (CheckBox7.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox7")) as CheckBox).Text + "$";
                }

                if (CheckBox8.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox8")) as CheckBox).Text + "$";
                }
                if (CheckBox9.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox9")) as CheckBox).Text + "$";
                }
                if (CheckBox10.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox10")) as CheckBox).Text + "$";
                }
                if (CheckBox11.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox11")) as CheckBox).Text + "$";
                                    }
                if (CheckBox12.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox12")) as CheckBox).Text + "$";
                }
                if (CheckBox13.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox13")) as CheckBox).Text + "$";

                }
                if (CheckBox14.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox14")) as CheckBox).Text + "$";

                }
                if (CheckBox15.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox15")) as CheckBox).Text + "$";
                 }

                if (CheckBox16.Checked == true)
                {
                    ExistCourses += ((item.FindControl("CheckBox16")) as CheckBox).Text + "$";

                }

                ExistCourses = ExistCourses.TrimEnd('$');


                DataSet ds = excol.UpdateDetailBulkDe(hdnval.Value, ExistCourses, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), uano, ipaddress);


            }

            objCommon.DisplayMessage(this.updDetained, "The Selected Courses For Selected Students Are Detained Successfully!", this.Page);
            GetDetainStudList();
        }
        else
        {
            objCommon.DisplayMessage(this.updDetained,"Please Select Students!", this.Page);
            lvCourse.DataSource = null;
            lvCourse.DataBind();
        }
    }
    private void ShowReportinxcel(string exporttype, string rptFileName)
    {
        try
        {

            DataSet ds = null;

            ds = excol.GetStudendetaintion(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
            ds.Tables[0].Columns.Remove("IDNO");
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DetainedList.xls");

            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();




        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentAdmissionStaticalReport.btnAdmCountinExcel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


        
    }

    private void ShowReportAdmStatistcs(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" +Convert.ToInt32(ddlcollege.SelectedValue) +",@P_DEGREENO="+Convert.ToInt32(ddlDegree.SelectedValue) +",@P_SEMESTERNO="+Convert.ToInt32(ddlSem.SelectedValue)+ ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetained, this.updDetained.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Session", this.Page);
            }
            else if (ddlcollege.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Instute / College Name", this.Page);
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Degree", this.Page);
            }
            else if (ddlBranch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Branch", this.Page);
            }
            else
            {
                ShowReportinxcel("xls", "AdmissionStatisticsCount.rpt");
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentAdmissionStaticalReport.btnAdmCountinExcel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Session", this.Page);
            }
            else if (ddlcollege.SelectedValue=="0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Instute / College Name", this.Page);
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Degree", this.Page);
            }
            else if (ddlBranch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select Branch", this.Page);
            }
            else
            {
                ShowReportAdmStatistcs("Admission_Statistics_Report", "AdmissionStatisticsCount.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentsStaticalReport.btnAdmReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.SelectedValue = "0";
            ddlSession.SelectedValue = "0";
            ddlSem.SelectedValue = "0";
            ddlDegree.SelectedValue = "0";
            ddlScheme.SelectedValue = "0";
            ddlcollege.SelectedValue = "0";
            lvCourse.DataSource = null;
            lvCourse.DataBind();

        }
        catch 
        { 
        
        }
    }
}
