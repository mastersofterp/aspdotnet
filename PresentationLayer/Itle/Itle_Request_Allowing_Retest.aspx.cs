//=====================================================
// PROJECT NAME  : UAIMS [GEC]                                                          
// MODULE NAME   : ITLE                                                            
// PAGE NAME     : REQUEST FOR ALLOWING RETEST                                    
// CREATION DATE : 30- MAY-2014                                                          
// CREATED BY    : SWAPNIL BAWANKAR
//====================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Itle_Itle_Request_Allowing_Retest : System.Web.UI.Page
{
    Common objCommon = new Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITLE_RequestAllowrestest objallot = new ITLE_RequestAllowrestest();
    Request_Allow_Retest objReq = new Request_Allow_Retest();

    #region Page Load

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
                // lblSession.Text = Session["sessionname"].ToString();
                // lblSession.ToolTip = Session["currentsession"].ToString();

                FillDropdown();
                //btnallowrequest.Visible = false;
                //btnMedicalreport.Visible = false;
            }
            //BindListView();
            divMsg.InnerHtml = string.Empty;

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
                Response.Redirect("~/notauthorized.aspx?page=Itle_Request_Allowing_Retest.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Request_Allowing_Retest.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {

            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");
               // objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A INNER JOIN ACD_SESSION_MASTER B ON(A.SESSIONNO =B.SESSIONNO)  ", " DISTINCT A.SESSIONNO", "B.SESSION_NAME", "A.SESSIONNO>0 AND EXAMTYPE IN (1,3) and IDNO=" + Convert.ToInt32(Session["userno"]), "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) inner join ACD_STUDENT_RESULT S on (A.SESSIONNO=S.SESSIONNO)", "DISTINCT A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "A.SESSIONNO>0 AND EXAMTYPE IN (1,3) AND IDNO=" + Convert.ToInt32(Session["idno"]), "A.SESSIONNO ");
           
               // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");
            }
            else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
            {

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

            }
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void BindListView()
    {
        try
        {
            //CourseController objCourse = new CourseController();

            DataSet ds = null;

            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int idno = Convert.ToInt32(Session["idno"]);
            int usertype = Convert.ToInt32(Session["usertype"]);

            if (Convert.ToInt32(Session["usertype"]) == 2)
                ds = objCourse.getDataAllowforRetest(sessionno, idno, usertype);
            //ds = objCourse.getDataAllowforRetest(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["usertype"]));
           // ds = objCourse.GetCourseByUaNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]));
            
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                trSession.Visible = true;
                DivCourseList.Visible = true;
                lblSession.Text = ddlSession.SelectedItem.Text.Trim();
                ViewState["stuid"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_selectCourse.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            HiddenField hdnLECTURE;
            HiddenField hdnTHEORY;
            HiddenField hdnPRACTICAL;
            HiddenField hdnSTUD_IDNO;
            HiddenField hdnSTUD_NAME;
            HiddenField hdnROLLNO;
            HiddenField hdnBRANCHNO;
            HiddenField hdnYEAR;
            HiddenField hdnCREDITS;
            Label lblSubname;
            Label lblSubjecttype;
            CheckBox chkCourseName;
            DropDownList ddlTest;

            //*****VALIDATION ADDED BY: M. REHBAR SHEIKH ON 26-08-2019***** 
            int chkCounter = 0;
            int dropdownNotSelected = 0;
            // bool dropdownNotSelected = false;
            foreach (ListViewDataItem item in lvCourse.Items)
            {
                CheckBox chkcoursename = (CheckBox)item.FindControl("chkCourseName");
                DropDownList ddltest = (DropDownList)item.FindControl("ddlTest");

                if (chkcoursename.Enabled)
                {
                    if (chkcoursename.Checked)
                    {
                        chkCounter++;
                    }
                }

                if (ddltest.Enabled)
                {
                    if (ddltest.SelectedIndex != 0)
                    {
                        dropdownNotSelected++;
                    }
                }
            }

            if (chkCounter == 0 || dropdownNotSelected == 0)
            {
                objCommon.DisplayMessage(this.updRequest, "Please Select Course Name and Test Name.", this.Page);
                return;
            }
            //*****************************************************************

            foreach (ListViewDataItem item in lvCourse.Items)
            {
                lblSubname = (Label)item.FindControl("lblSubname");
                lblSubjecttype = (Label)item.FindControl("lblSubjecttype");
                chkCourseName = (CheckBox)item.FindControl("chkCourseName");
                ddlTest = (DropDownList)item.FindControl("ddlTest");
                hdnLECTURE = (HiddenField)item.FindControl("hdnLECTURE");

                hdnTHEORY = (HiddenField)item.FindControl("hdnTHEORY"); ;
                hdnPRACTICAL = (HiddenField)item.FindControl("hdnPRACTICAL");
                hdnSTUD_IDNO = (HiddenField)item.FindControl("hdnSTUD_IDNO");
                hdnSTUD_NAME = (HiddenField)item.FindControl("hdnSTUD_NAME");
                hdnROLLNO = (HiddenField)item.FindControl("hdnROLLNO");
                hdnBRANCHNO = (HiddenField)item.FindControl("hdnBRANCHNO");
                hdnYEAR = (HiddenField)item.FindControl("hdnYEAR");
                hdnCREDITS = (HiddenField)item.FindControl("hdnCREDITS");

                if (chkCourseName.Checked.Equals(true))
                {
                    objallot.CourseName += lblSubname.Text + ",";
                    objallot.Courseno += lblSubname.ToolTip + ",";
                    objallot.Subid += lblSubjecttype.ToolTip + ",";
                    objallot.Testno += ddlTest.SelectedValue + ",";
                    //  objallot.Lecture        += (hdnLECTURE.Value) +",";
                    //  objallot.Practicle     += hdnPRACTICAL.Value + ",";
                    //  objallot.Theory        += hdnTHEORY.Value +",";
                    objallot.Stud_idno += hdnSTUD_IDNO.Value + ",";
                    objallot.Roll_no += hdnROLLNO.Value + ",";
                    objallot.BatchName += hdnYEAR.Value + ",";
                    //  objallot.Credits       += hdnCREDITS.Value +",";
                    objallot.Branchno += hdnBRANCHNO.Value + ",";
                    objallot.StudName += hdnSTUD_NAME.Value + ",";

                }
            }
            objallot.Sessionno = Convert.ToInt16(ddlSession.SelectedValue);

            objallot.Ua_no = Convert.ToInt16(Session["userno"].ToString());

            CustomStatus cs = (CustomStatus)objReq.Insert_ALlow_Retest(objallot);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updRequest, "Record Submitted Successfully!", this.Page);
                btnallowrequest.Visible = true;
                btnMedicalreport.Visible = true;
                Uncheck();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updRequest, "Request is already sent for this Test!", this.Page);
                btnallowrequest.Visible = true;
                btnMedicalreport.Visible = true;
                Uncheck();
            }
            else
            {
                objCommon.DisplayMessage(this.updRequest, "Sorry! Transaction Fail", this.Page);

            }

            // objallot.Lecture = Convert.ToInt16(hdnLECTURE.Value);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Request_Allowing_Retest.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            Label lblSubname;
            DropDownList ddlTest;
            CheckBox chkTest;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Display the e-mail address in italics.
                lblSubname = (Label)e.Item.FindControl("lblSubname");
                ddlTest = (DropDownList)e.Item.FindControl("ddlTest");
                chkTest = (CheckBox)e.Item.FindControl("chkCourseName");
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", " COURSENO=" + Convert.ToInt16(lblSubname.ToolTip), "TESTNO");

                if (ddlTest.Items.Count > 1)
                {
                    ddlTest.Enabled = true;
                    chkTest.Enabled = true;
                    chkTest.BorderColor = System.Drawing.Color.Red;
                }
                else
                {
                    ddlTest.Enabled = false;
                    chkTest.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Itle_Request_Allowing_Retest.lvCourse_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            //string Script = string.Empty;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,ITLE," + rptFileName;
            //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",SESSION_NAME=" + Session["SESSION_NAME"] + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",COURSE_NAME=" + Session["ICourseName"] + ",@P_COLLEGE_CODE=" + Session["colcode"];
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            string Script = string.Empty;
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_STUDIDNO=" + Convert.ToInt16(ViewState["stuid"].ToString());

            url += "&param=@P_STUDIDNO=" + Convert.ToInt16(ViewState["stuid"].ToString());
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updRequest, this.updRequest.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Request_Allowing_Retest.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //medical certificate  
    protected void btnMedicalreport_Click(object sender, EventArgs e)
    {
        ShowReport("Medical Certificate", "Itle_MedicalCertificate_Report.rpt");
    }

    //allow certifiacte 
    protected void btnallowrequest_Click(object sender, EventArgs e)
    {
        ShowReport("Medical Certificate", "Itle_Request_Alternative_Examination_Report.rpt");
    }

    //for refreshing ListView     
    protected void Uncheck()
    {
        foreach (ListViewDataItem lsvdata in lvCourse.Items)
        {
            CheckBox chkitem = lsvdata.FindControl("chkCourseName") as CheckBox;
            chkitem.Checked = false;

            DropDownList ddlTest = lsvdata.FindControl("ddlTest") as DropDownList;
            ddlTest.SelectedValue = "0";

        }
    }

    //Added by Saket Singh on 12-09-2017
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;

        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                ds = objCommon.FillDropDown("ACD_SESSION_MASTER", " Top 1 SESSION_NAME", "SESSIONNO", "SESSIONNO>0 AND SESSIONNO=" + ddlSession.SelectedValue, "SESSIONNO DESC");
                //ds = objCommon.FillDropDown("ACD_SESSION_MASTER", " Top 1 SESSION_NAME", "SESSIONNO", "SESSIONNO>0 and FLOCK =1", "SESSIONNO DESC");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                    BindListView();
                    Session["SessionNo"] = Convert.ToInt32(ddlSession.SelectedValue);
                }
            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    #endregion
}
