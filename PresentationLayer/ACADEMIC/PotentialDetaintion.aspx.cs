
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : POTENTIAL DETAINTION
// CREATION DATE : 21/01/2021                                               
// CREATED BY    : SAFAL GUPTA                                         
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_PotentialDetaintion : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int prov_detain = 0;
    int final_detain = 0;
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                   
                    

                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
               
            }
            Perticularsub.Visible = false;
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {

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
                Response.Redirect("~/notauthorized.aspx?page=studentPreRegist.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentPreRegist.aspx");
        }
    }
    

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvCourses.DataSource = null;
        lvCourses.DataBind();
        lblshow.Visible = false;
        pnlStudents.Visible = false;

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));


        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //string deptno = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));
                //ViewState["deptno"] = Convert.ToInt32(deptno);
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }

    }

    private void ClearControls()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        lvCourses.DataSource = null;
        lvCourses.DataBind();
        lblshow.Visible = false;
        pnlStudents.Visible = false;

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));


        if (ddlSession.SelectedIndex > 0)
        {
            //ddlSession.Focus();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT CT ON S.SEMESTERNO=CT.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "", "S.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }
        btnSubmit.Visible = false;
        lvCourses.DataSource = null;
        lvCourses.DataBind();
        lblshow.Visible = false ;
        pnlStudents.Visible = false;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourses.DataSource = null;
        lvCourses.DataBind();
        btnSubmit.Visible = false;
        btnCancel.Visible = true;
        lblshow.Visible = false;
        pnlStudents.Visible = false;

        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));

        if (Session["usertype"].ToString() == "1")
        {
            //ddlSemester.Focus();
            // SELECT DISTINCT S.COURSENO,(S.CCODE +'-'+S.COURSENAME)COURSENAME  FROM ACD_STUDENT_RESULT S INNER JOIN ACD_COURSE_TEACHER CT ON S.COURSENO=CT.COURSENO WHERE CT.SESSIONNO=54 AND CT.UA_NO=402 AND CT.SCHEMENO=1 AND CT.SEMESTERNO=3
            objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT S INNER JOIN ACD_COURSE CT ON S.COURSENO=CT.COURSENO", "DISTINCT S.COURSENO", "(S.CCODE +'-'+S.COURSENAME)COURSENAME ", "S.PREV_STATUS=0 AND ISNULL(S.REGISTERED,0)=1 AND S.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "  AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "", "S.COURSENO");
            ddlCourse.Focus();
        }
        else
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT S INNER JOIN ACD_COURSE_TEACHER CT ON S.COURSENO=CT.COURSENO", "DISTINCT S.COURSENO", "(S.CCODE +'-'+S.COURSENAME)COURSENAME ", "S.PREV_STATUS=0 AND ISNULL(S.REGISTERED,0)=1 AND S.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (CT.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " OR CT.ADTEACHER=" + Convert.ToInt32(Session["userno"].ToString()) + ") AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "", "S.COURSENO");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SHODATA();
    }


    public void SHODATA()
    {
        try
        {
           //code for get exam-registerd students 
            StudentRegistration objSR = new StudentRegistration();
            DataSet ds = null;
            if (rblSelection.SelectedValue != "1" && rblSelection.SelectedValue != "2")
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Select any one Detention Type.", this);
                return;
            }

            if (rblSelection.SelectedValue == "2")
            {
                
                ds = objSR.GetRegisteredStudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlCourse.SelectedValue),2,string.Empty,0);
            }
            else
            {
                if (ddlCourse.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Select Course.", this);
                    return;
                }
                if (txtPercentage.Text == "")
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Enter Percentage.", this);
                    return;
                }
                ds = objSR.GetRegisteredStudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlCourse.SelectedValue),1,ddlOperator.SelectedItem.Text,Convert.ToDecimal(txtPercentage.Text));
            }
                if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvCourses.DataSource = ds;
                    lvCourses.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourses);//Set label - 
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    lblshow.Visible = true;
                    pnlStudents.Visible = true;
                }
                else
                {
                    lvCourses.DataSource = null;
                    lvCourses.DataBind();
                    btnSubmit.Visible = false;
                    btnCancel.Visible = true;
                    lblshow.Visible = false;
                    pnlStudents.Visible = false;
                    objCommon.DisplayMessage(UpdatePanel1, "Record Not Found !!", this);
                }
            }
            else
            {
                lvCourses.DataSource = null;
                lvCourses.DataBind();
                pnlStudents.Visible = false;
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        int count = 0;
        try
        {
            StudentRegist objSR = new StudentRegist();
            StudentRegistration objSReg = new StudentRegistration();
            string ccode = string.Empty;
            string prov_detend = string.Empty;
            string idnos = string.Empty;
            CustomStatus cs = 0;

            foreach (ListViewDataItem item in lvCourses.Items)
            {

                CheckBox chk = item.FindControl("chkAccept") as CheckBox;

                if (chk.Checked)
                {
                    prov_detend += "1,";
                    idnos += chk.ToolTip + ",";
                    count++;
                }
                else
                //if (chk.Checked==false)
                {
                    //prov_detend += "0,";
                    //idnos += chk.ToolTip + ",";
                    //count++;
                }


            }
            objSR.PROV_DETEND = prov_detend;
            objSR.IDNOS = idnos.ToString();

            string idnoTrim = string.Empty;
            string[] arr = idnos.Split(',');
            string regno = string.Empty;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                regno += objCommon.LookUp("acd_student", "regno", "idno in (" + arr[i] + ")");
                regno += ",";
            }


            if (count <= 0)
            {
                objCommon.DisplayMessage("Please Select at least one Student for Provisional Detention", this.Page);
                return;
            }
            string REGNOs = regno.TrimEnd(',');
            ViewState["regnos"] = REGNOs;
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
            objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
            objSR.COURSENO = Convert.ToInt32(ddlCourse.SelectedValue);
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString ();
            int orgid = Convert.ToInt32(Session["OrgId"]);
            
            if (rblSelection.SelectedValue == "2")
            {
                 cs = (CustomStatus)objSReg.AddDetendinfo(objSR, orgid, 2);
            }
            else
            {
                 cs = (CustomStatus)objSReg.AddCourseDetendinfo(objSR, orgid, 1);
            }
           
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(UpdatePanel1, "Provisional Detention Entry Done Successfully ...", this);
                SHODATA();
                // ClearControls();
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Error in Saving Record..", this);
                return;
            }

            //SendMailtoHOD();

        }
            
        catch (Exception ex)
        {
            throw;
        }
    }

   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //  DataSet ds = objStudentController.Get_Detent_StudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=REPORT";
            url += "&path=~,Reports,Academic," + "rptProvisionalDetention_Report.rpt";
            //        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_COURSENO="+Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void SendMailtoHOD()
    {
        StudentAttendanceController STT = new StudentAttendanceController();
        string idno = string.Empty;
        string emailId = string.Empty;
        string emailId1 = string.Empty;
        string message = string.Empty;
        string Subject = string.Empty;
        string Ua_No = string.Empty;
        try
        {
            string session_name = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
            string coursename = objCommon.LookUp("acd_course", "CCODE + '-' +COURSE_NAME", "courseno=" + ddlCourse.SelectedValue);
            Ua_No = Session["userno"].ToString();
            Subject = "Provisional Detaintion Student List of " + coursename + " for session " + session_name;
            string ua_fullname = objCommon.LookUp("USER_ACC", "ua_fullname", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            string Ua_Deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            string ua_no_hod = objCommon.LookUp("USER_ACC", "ua_no", "ua_dec=1 and Ua_Deptno =" + Convert.ToInt32(Ua_Deptno));
            emailId = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO=" + ua_no_hod);
            emailId1 = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO=" + Ua_No);
            message = "Respected Sir/Mam,<br/>Student(s) with Enrollment No(s) :" + ViewState["regnos"] + " are provisionaly detaind for session " + session_name + " in " + coursename + "<br/><br/><br/>Regards<br/>" + ua_fullname;
            objCommon.sendEmail(message, emailId, Subject);
            objCommon.sendEmail(message, emailId1, Subject);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void lvCourses_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        CheckBox chk = e.Item.FindControl("chkAccept") as CheckBox;
        Label changeClr = e.Item.FindControl("lblProv") as Label;

        if (changeClr.ToolTip == "1")
        {
            chk.Enabled = false;

            chk.BackColor = System.Drawing.Color.Green;
            chk.Checked = false;
            changeClr.Text = "YES";
            changeClr.Style.Add("color", "Green");
        }
        else
        {
            chk.Enabled = true;
            changeClr.Text = "NO";
            changeClr.Style.Add("color", "Red");

        }
    }

    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvCourses.DataSource = null;
        lvCourses.DataBind();
        lblshow.Visible = false;
        pnlStudents.Visible = false;
        ddlCourse.SelectedIndex = 0;

        if (rblSelection.SelectedValue == "1")
        {
            divCourse.Visible = true;
            ddlCourse.SelectedIndex = 0;
            Perticularsub.Visible = true;
        }
        else
        {
            divCourse.Visible = false;
            ddlCourse.SelectedIndex = 0;
            Perticularsub.Visible = false;
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvCourses.DataSource = null;
        lvCourses.DataBind();
        lblshow.Visible = false;
        pnlStudents.Visible = false;
    }
}



