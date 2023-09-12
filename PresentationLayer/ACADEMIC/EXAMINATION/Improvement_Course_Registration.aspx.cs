using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_EXAMINATION_Improvement_Course_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    Student_Acd objSA = new Student_Acd();

    bool IsNotActivitySem = false;
    bool flag = true;

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
                ////Page Authorization
                // this.CheckPageAuthorization();
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 and college_id=" + ddlclgname.SelectedValue); // ADD BY ROSHAN PANNASE 31-12-2015 FOR COURSE REGISTER IN START SESSION ONELY.

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //File Dropdown Box


                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;
                Session["payactivityno"] = "14";

                if (ViewState["usertype"].ToString() == "2")
                {
                    if (CheckActivity())
                    {
                        txtEnrollno.Text = string.Empty;
                        btnSearch.Visible = false;
                        btnCancel.Visible = false;
                        divCourses.Visible = true;
                        pnlSearch.Visible = false;
                        this.ShowDetails();
                        int b = 0;
                        //btnSubmit.Visible = true;
                        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        {
                            CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                            if (chkacc.Checked == true)
                            {
                                b++;
                                chkacc.Enabled = false;
                            }
                        }

                        btnSubmit.Enabled = (lvFailCourse.Items.Count == b) ? false : true;
                        btnPrintRegSlip.Visible = true;
                        btnPrintRegSlip.Enabled = true;
                        //btnPrintRegSlip.Visible = (lvFailCourse.Items.Count == b) ? true : false;
                        //btnPrintRegSlip.Enabled = (lvFailCourse.Items.Count == b) ? true : false;
                    }
                    //btnPrintRegSlip.Enabled = true;
                }
                else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
                    pnlSearch.Visible = true;

                //btnSubmit.Visible = false;
                btnStudSubmit.Visible = false;
                //btnPrintRegSlip.Visible = true;
                string IPADDRESS = string.Empty;
                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];

            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter,
                                                          System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];
            //User_IPAddress = IP_Array[0];
        }
        return User_IPAddress;
    }
    //private bool CheckActivity()
    //{
    //    //bool ret = true;
    //    //DataSet ds = objSReg.Get_Student_Details_for_Course_Registration(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToInt32(Session["usertype"]), 2);
    //    //ViewState["DataSet_Student_Details"] = ds;
    //    //if (ds.Tables[0].Rows.Count > 0)
    //    //{
    //    //    ViewState["ACTIVITY_NO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSE_REG_CONFIG_NO"]);

    //    //    if (ds.Tables[0].Rows[0]["STARTED"].ToString().ToLower().Equals("false"))
    //    //    {
    //    //        objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //    //        ret = false;
    //    //    }

    //    //    if (ds.Tables[0].Rows[0]["PRE_REQ_ACT"].ToString().ToLower().Equals("false"))
    //    //    {
    //    //        objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //    //        ret = false;
    //    //    }

    //    //    ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
    //    //    ViewState["SessionNo"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"]);
    //    //    ViewState["sessionnonew"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"]);
    //    //    ViewState["IsPaymetApplicable"] = ds.Tables[0].Rows[0]["PAYMENT_APPLICABLE_FOR_SEM_WISE"].ToString();
    //    //}
    //    //else
    //    //{
    //    //    divenroll.Visible = false;
    //    //    btnSearch.Visible = false;
    //    //    btnCancel.Visible = false;
    //    //    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //    //    ret = false;
    //    //}
    //    //return ret;

    //    #region commented data on 31.07.2023 by Shailendra K.
    //    if (Convert.ToInt32(ViewState["usertype"]) == 2)
    //    {
    //        bool ret = true;
    //        string sessionno = string.Empty;
    //        int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]) + ""));
    //        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND COLLEGE_IDS=" + college_id + " AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
    //        //sessionno = Session["currentsession"].ToString();
    //        ViewState["SessionNo"] = sessionno;
    //        ViewState["sessionnonew"] = sessionno;
    //        ActivityController objActController = new ActivityController();
    //        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

    //        if (dtr.Read())
    //        {
    //            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

    //            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //            {
    //                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //                //dvMain.Visible = false;
    //                ret = false;

    //            }

    //            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //            {
    //                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //                //dvMain.Visible = false;
    //                ret = false;
    //            }

    //        }
    //        else
    //        {

    //            divenroll.Visible = false;
    //            btnSearch.Visible = false;
    //            btnCancel.Visible = false;
    //            // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //            // dvMain.Visible = false;
    //            ret = false;
    //        }
    //        dtr.Close();
    //        return ret;
    //    }
    //    else
    //    {
    //        bool ret = true;
    //        string sessionno = string.Empty;

    //        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
    //        //sessionno = Session["currentsession"].ToString();
    //        ViewState["SessionNo"] = sessionno;
    //        ViewState["sessionnonew"] = sessionno;
    //        ActivityController objActController = new ActivityController();
    //        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

    //        if (dtr.Read())
    //        {
    //            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

    //            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //            {
    //                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //                //dvMain.Visible = false;
    //                ret = false;

    //            }

    //            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //            {
    //                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //                //dvMain.Visible = false;
    //                ret = false;
    //            }

    //        }
    //        else
    //        {
    //            // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //            // dvMain.Visible = false;
    //            ret = false;
    //        }
    //        dtr.Close();
    //        return ret;

    //    }
    //    #endregion
    //}

    private bool CheckActivity()
    {
        bool ret = true;
        DataSet ds = objSReg.Get_Student_Details_for_Course_Registration(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToInt32(Session["usertype"]), 3);
        ViewState["DataSet_Student_Details"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSE_REG_CONFIG_NO"]);

            if (ds.Tables[0].Rows[0]["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            if (ds.Tables[0].Rows[0]["PRE_REQ_ACT"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }

            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["SessionNo"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"]);
            ViewState["sessionnonew"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"]);
            ViewState["IsPaymetApplicable"] = ds.Tables[0].Rows[0]["PAYMENT_APPLICABLE_FOR_SEM_WISE"].ToString();
        }
        else
        {
            divenroll.Visible = false;
            btnSearch.Visible = false;
            btnCancel.Visible = false;
            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
            ret = false;
        }
        return ret;

        #region commented data on 31.07.2023 by Shailendra K.
        //if (Convert.ToInt32(ViewState["usertype"]) == 2)
        //{
        //    bool ret = true;
        //    string sessionno = string.Empty;
        //    int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]) + ""));
        //    sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND COLLEGE_IDS=" + college_id + " AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
        //    //sessionno = Session["currentsession"].ToString();
        //    ViewState["sessionno"] = sessionno;
        //    ActivityController objActController = new ActivityController();
        //    DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        //    if (dtr.Read())
        //    {
        //        ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

        //        if (dtr["STARTED"].ToString().ToLower().Equals("false"))
        //        {
        //            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
        //            //dvMain.Visible = false;
        //            ret = false;

        //        }

        //        //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
        //        if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
        //        {
        //            objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
        //            //dvMain.Visible = false;
        //            ret = false;
        //        }

        //    }
        //    else
        //    {

        //        divenroll.Visible = false;
        //        btnSearch.Visible = false;
        //        btnCancel.Visible = false;
        //        // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
        //        // dvMain.Visible = false;
        //        ret = false;
        //    }
        //    dtr.Close();
        //    return ret;
        //}
        //else
        //{
        //    bool ret = true;
        //    string sessionno = string.Empty;

        //    sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
        //    //sessionno = Session["currentsession"].ToString();
        //    ViewState["sessionno"] = sessionno;
        //    ActivityController objActController = new ActivityController();
        //    DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        //    if (dtr.Read())
        //    {
        //        ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

        //        if (dtr["STARTED"].ToString().ToLower().Equals("false"))
        //        {
        //            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
        //            //dvMain.Visible = false;
        //            ret = false;

        //        }

        //        //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
        //        if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
        //        {
        //            objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
        //            //dvMain.Visible = false;
        //            ret = false;
        //        }

        //    }
        //    else
        //    {
        //        // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
        //        // dvMain.Visible = false;
        //        ret = false;
        //    }
        //    dtr.Close();
        //    return ret;

        //}
        #endregion
    }
    private void FillDropdown()
    {

        objCommon.FillDropDownList(ddlclgname, "ACD_COLLEGE_MASTER SM", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "SM.COLLEGE_ID DESC");
        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + ViewState["ACTIVITY_NO"], "");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //ViewState["degreenos"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //ViewState["branchnos"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }
        //commented on 21042022
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        //ddlSession.SelectedIndex = 1;
        //mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        ddlSession.Focus();
    }
    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;
        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }
    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "SEF";
            dcrCriteria.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            dcrCriteria.PaymentTypeNo = 1;
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.GetDcrCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }
    public string getIPAddress()
    {
        string direction;
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        WebResponse response = request.GetResponse();
        StreamReader stream = new StreamReader(response.GetResponseStream());
        direction = stream.ReadToEnd();
        stream.Close();
        response.Close(); //Search for the ip in the html
        int first = direction.IndexOf("Address: ") + 9;
        int last = direction.LastIndexOf("</b");
        direction = direction.Substring(first, last - first);
        return direction.ToString();
    }
    private bool CheckConnection()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create("http://www.google.com/") as HttpWebRequest;
            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
        catch (Exception)
        {
            return false;
        }
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
    private void ShowDetails()
    {
        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();
        lvFailInaggre.DataSource = null;
        lvFailInaggre.DataBind();
        btnSubmit.Visible = false;
        btnPrintRegSlip.Visible = false;
        //btnReport.Visible = false;
        int idno = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0) IDNO", "REGNO='" + txtEnrollno.Text.Trim() + "' "));
        }

        try
        {
            if (idno > 0)
            {
                divCourses.Visible = true;
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        ViewState["StudentMobile"] = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        ViewState["StudentEmail"] = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        ViewState["collegeId"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

                        decimal lblcgpa = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["CGPA"]);
                        lblCGPA.Text = lblcgpa.ToString("0.00");

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        lblBacklogFine.Text = "0";
                        lblTotalFee.Text = "0";
                        lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + Convert.ToInt32(ViewState["SessionNo"].ToString()));

                        int UGPGOT = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["UGPGOT"]);
                        decimal CGPA = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["CGPA"]);

                        if ((UGPGOT == 1 && CGPA < 5) || (UGPGOT == 2 && CGPA < 6))
                        {
                            bindcourses();
                        }
                        else
                        {
                            objCommon.DisplayMessage("You are not eligible for Improvement Registration", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;
                        flag = false;
                        ddlclgname.ClearSelection();
                        ddlSession.ClearSelection();
                        txtEnrollno.Text = "";
                        ddlSession.Enabled = true;
                        txtEnrollno.Enabled = true;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;
                    flag = false;

                    ddlclgname.ClearSelection();
                    ddlSession.ClearSelection();
                    txtEnrollno.Text = "";

                    ddlSession.Enabled = true;
                    txtEnrollno.Enabled = true;
                }
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                divCourses.Visible = false;
                flag = false;

                ddlclgname.ClearSelection();
                ddlSession.ClearSelection();
                txtEnrollno.Text = "";

                ddlSession.Enabled = true;
                txtEnrollno.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        //int sessionno = 0;
        //if (Session["usertype"].ToString() == "2")
        //{
        //    // sessionno = Convert.ToInt32(Session["currentsession"]);
        //    sessionno = Convert.ToInt32(ViewState["SessionNo"]);
        //}
        //else
        //{
        //    sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //}

        //int idno = Convert.ToInt32(lblName.ToolTip);
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEM=" + Convert.ToInt32(ddlBackLogSem.SelectedValue);

        //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    divMsg.InnerHtml += " </script>";
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}

        int sessionno = Convert.ToInt32(ViewState["SessionNo"]);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEM=" + Convert.ToInt32(lblSemester.ToolTip);

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + ViewState["collegeId"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Event
    protected void btnReport_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                return;
            }

        }
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT(SCHEMENO)", "IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
        int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));
        ShowReport("ExamRegistrationSlip", "rptExamRegslipNit.rpt");
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        int idno = 0;
        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

        if (!string.IsNullOrEmpty(REGNO))
            idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
        else
        {
            objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
            ddlSession.Enabled = true;
            txtEnrollno.Enabled = true;
            ddlclgname.ClearSelection();
            ddlSession.ClearSelection();
            txtEnrollno.Text = string.Empty;
            divCourses.Visible = false;
            return;
        }

        Session["idno"] = idno;
        if (!CheckActivity())
            return;
        btnShow.Visible = false;
        ddlSession.Enabled = false;
        ShowDetails();

        if (flag.Equals(false))
        {
            objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
            divCourses.Visible = false;
        }
    }

    //private bool CheckActivityCollege(int cid)
    //{
    //    bool ret = true;
    //    string sessionno = string.Empty;

    //    sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + cid + " UNION ALL SELECT 0 AS SESSION_NO");
    //    //sessionno = Session["currentsession"].ToString();
    //    ViewState["sessionnonew"] = sessionno;
    //    ActivityController objActController = new ActivityController();
    //    DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

    //    if (dtr.Read())
    //    {
    //        ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

    //        if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //        {
    //            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //            //dvMain.Visible = false;
    //            ret = false;

    //        }

    //        //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        {
    //            objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //            //dvMain.Visible = false;
    //            ret = false;
    //        }

    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //        txtEnrollno.Text = string.Empty;
    //        // dvMain.Visible = false;
    //        ret = false;
    //    }
    //    dtr.Close();
    //    return ret;
    //}


    protected void btnShow_Click1(object sender, EventArgs e)
    {
        //Fail subjects List
        int idno = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        // int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        StudentController objSC = new StudentController();
        DataSet dsFailSubjects;
        // DataSet dsDetainedStudent = null;
        if (ViewState["usertype"].ToString() != "1" && ViewState["usertype"].ToString() != "3" && ViewState["usertype"].ToString() != "7")
        {
            idno = Convert.ToInt32(Session["idno"]);
            //int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + semesterno + " AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));

            int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "  AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));
            if (accept > 0)
            {
                lvFailCourse.Enabled = false;
                btnSubmit.Enabled = false;
                btnPrintRegSlip.Enabled = false;
            }
            else
            {
                lvFailCourse.Enabled = true;
                btnSubmit.Enabled = true;
                btnPrintRegSlip.Enabled = false;
            }
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                return;
            }
        }

        //int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + semesterno + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        if (exreg > 0)
        {
            btnReport.Visible = true;
            objCommon.DisplayMessage("Selected Semester Exam Registration Already Done", this.Page);
            lvFailCourse.Enabled = true;
            btnSubmit.Enabled = false;
            btnPrintRegSlip.Enabled = false;
        }
        else
        {
            btnReport.Visible = false;

        }


        // dsFailSubjects = objSC.GetStudentFailExamSubjects(idno, sessionno, semesterno);
        int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
        //dsFailSubjects = objSC.GetStudentFailExamSubjectSNew(idno, sessionno, semesterno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);

        dsFailSubjects = objSC.GetStudentFailExamSubjectSNew_sem_RedoCourse(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);
        if (dsFailSubjects.Tables[0].Rows.Count > 0)
        {
            lvFailCourse.DataSource = dsFailSubjects;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = true;
            divCourses.Visible = true;
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            divCourses.Visible = true;
            objCommon.DisplayMessage("No Courses found for this semester...!!!", this.Page);
        }


        if (dsFailSubjects != null && dsFailSubjects.Tables.Count > 0)
        {
            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {


                int sum = 0;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == false)
                    {
                        btnPrintRegSlip.Enabled = false;
                    }
                    else
                        btnPrintRegSlip.Enabled = false;

                    //string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "  AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");


                    string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");

                    if (count == "0")
                    {
                        if (chk.Checked == true)
                            sum++;
                        chk.Enabled = true;
                    }
                    else
                    {
                        if (Session["usertype"].ToString() == "2")
                        {
                            chk.Enabled = false;
                            btnPrintRegSlip.Enabled = true;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            chk.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                }

                if (sum.Equals(Convert.ToInt32(1)))
                {
                    lblBacklogFine.Text = "200";
                    hdnBacklogFine.Value = "200";
                    lblTotalFee.Text = "200";
                    hdnTotalFee.Value = "200";
                    objCommon.DisplayMessage("Backlog Registration done for this semester...!!!", this.Page);
                }
                else if (sum.Equals(Convert.ToInt32(2)))
                {
                    lblBacklogFine.Text = "400";
                    hdnBacklogFine.Value = "400";
                    lblTotalFee.Text = "400";
                    hdnTotalFee.Value = "400";
                    objCommon.DisplayMessage("Backlog Registration done for this semester...!!!", this.Page);
                }
                else if (sum.Equals(Convert.ToInt32(0)))
                {
                    lblBacklogFine.Text = "0";
                    hdnBacklogFine.Value = "0";
                    lblTotalFee.Text = "0";
                    hdnTotalFee.Value = "0";
                }
                else
                {
                    lblBacklogFine.Text = "500";
                    hdnBacklogFine.Value = "500";
                    lblTotalFee.Text = "500";
                    hdnTotalFee.Value = "500";
                }
            }
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            divCourses.Visible = true;
            objCommon.DisplayMessage("No Courses found for this semester...!!!", this.Page);
        }
        btnSubmit.Visible = true;
        btnPrintRegSlip.Visible = false;

        if (ViewState["usertype"].ToString() == "2")
        {
            //btnPrintRegSlip.Enabled = true;
        }
    }


    protected void bindcourses()
    {
        int idno = 0;
        int sessionno = Convert.ToInt32(ViewState["sessionnonew"]);
        StudentController objSC = new StudentController();
        DataSet dsFailSubjects;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ViewState["sessionnonew"]) + "  AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));
            if (accept > 0)
            {
                lvFailCourse.Enabled = false;
                btnSubmit.Enabled = false;
                btnPrintRegSlip.Enabled = false;
            }
            else
            {
                lvFailCourse.Enabled = true;
                btnSubmit.Enabled = true;
                btnPrintRegSlip.Enabled = false;
            }
        }
        else
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0) IDNO", "REGNO='" + txtEnrollno.Text.Trim() + "' "));

        if (idno == null || idno == 0)
            objCommon.DisplayMessage("No Record Found...", this.Page);
        else
        {
            //int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            //int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));

            dsFailSubjects = objSC.GetStudentImprovementRegisteredCourses(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(hdfDegreeno.Value), Convert.ToInt32(lblBranch.ToolTip), Convert.ToInt32(lblSemester.ToolTip));
            ViewState["FailSubjects"] = dsFailSubjects;

            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                ddlBackLogSem.Items.Clear();
                ddlBackLogSem.Items.Clear();
                ddlBackLogSem.Items.Add("Please Select");
                ddlBackLogSem.SelectedItem.Value = "0";
                DataView view = new DataView(dsFailSubjects.Tables[0]);
                DataTable distinctValues = new DataTable();
                distinctValues = view.ToTable(true, "SEMESTER", "SEMESTERNAME");
                ddlBackLogSem.DataSource = distinctValues;
                ddlBackLogSem.DataValueField = distinctValues.Columns[0].ToString();
                ddlBackLogSem.DataTextField = distinctValues.Columns[1].ToString();
                ddlBackLogSem.DataBind();
                ddlBackLogSem.SelectedIndex = 0;
            }

            //DataSet dsFailSubjects = (DataSet)ViewState["FailSubjects"];

            if (dsFailSubjects.Tables[1].Rows.Count > 0)
            {
                //DataTable dt = dsFailSubjects.Tables[0].Select("SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip)).CopyToDataTable();
                //lvFailCourse.DataSource = dt;
                lvFailCourse.DataSource = dsFailSubjects.Tables[1];
                lvFailCourse.DataBind();
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                btnSubmit.Visible = (Convert.ToInt16(Session["usertype"].ToString()) == 2) ? false : true;
                btnStudSubmit.Visible = (Convert.ToInt16(Session["usertype"].ToString()) != 2) ? false : true;
                pnlFailCourse.Visible = true;

                btnPrintRegSlip.Visible = true;
                btnSubmit.Visible = true;

                int a = lvFailCourse.Items.Count;
                int b = 0;

                if (Session["usertype"].ToString() == "2")
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        //if (chk.Enabled == false)
                        chk.Enabled = chk.Checked ? false : true;
                        b++;
                    }
                }

                if (ViewState["usertype"].ToString() == "2")
                {
                    //btnSubmit.Enabled = (a == b) ? false : true;
                    //btnStudSubmit.Enabled = (a == b) ? false : true;
                }
                else
                {

                }

                //StudentController objSC = new StudentController();
                //DataSet dc = objSC.GetOfferedCourseForRegistration(Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToInt32(ViewState["SessionNo"]));
                //if (dc.Tables[0].Rows.Count > 0)
                //{
                //    lvOfferedCourse.DataSource = dc;
                //    lvOfferedCourse.DataBind();
                //}
                //else
                //{
                //    lvOfferedCourse.DataSource = null;
                //    lvOfferedCourse.DataBind();
                //}

                //DataSet dc = objSC.GetOfferedCourseForRegistration(Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToInt32(ViewState["SessionNo"]));
                if (dsFailSubjects.Tables[0].Rows.Count > 0)
                {
                    lvOfferedCourse.DataSource = dsFailSubjects.Tables[0];
                    lvOfferedCourse.DataBind();
                }
                else
                {
                    lvOfferedCourse.DataSource = null;
                    lvOfferedCourse.DataBind();
                }

            }
            else
            {
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                divCourses.Visible = true;
                objCommon.DisplayMessage("No Courses found...!!!", this.Page);
                btnSubmit.Visible = false;
                btnStudSubmit.Visible = false;
                pnlFailCourse.Visible = false;
                pnlOfferedCourse.Visible = false;
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveEntry();

        #region Commented by Shailendra K. on dated 01.08.2023
        //int count_Backlog_idno = 0;
        //StudentRegistration objSRegist = new StudentRegistration();
        //StudentRegist objSR = new StudentRegist();
        //int idno = 0;
        //string REGNO = string.Empty;
        //if (ViewState["usertype"].ToString() == "2")
        //{
        //    idno = Convert.ToInt32(Session["idno"]);
        //}
        //else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        //{
        //    //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

        //    REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "'");

        //    if (REGNO != null && REGNO != string.Empty && REGNO != "")
        //        idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
        //    else
        //    {
        //        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
        //        return;
        //    }
        //}
        ////string schemeno = objCommon.LookUp("ACD_STUDENT","SCHEMENO", "IDNO=" + idno);
        ////string schemeno = objCommon.LookUp("(SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE  IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND PASSFAIL = 'FAIL IN AGGREGATE' UNION ALL SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE IDNO =" + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND RESULT = 'F')A", "SCHEMENO", "");


        //string schemeno = objCommon.LookUp("(SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE  IDNO = " + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND PASSFAIL = 'FAIL IN AGGREGATE' UNION ALL SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE IDNO =" + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND RESULT = 'F')A", "SCHEMENO", "");



        ////string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);

        ////objSR.SESSIONNO = Convert.ToInt32(Session["currentsession"]);
        ////objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);

        //objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionnonew"]);
        //objSR.IDNO = idno;
        ////Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).Text);
        //objSR.REGNO = REGNO;
        //objSR.ROLLNO = txtEnrollno.Text;
        ////objSR.SCHEMENO = Convert.ToInt32(schemeno);
        //objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
        ////objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
        ////objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        //objSR.IPADDRESS = Session["ipAddress"].ToString(); ;// ----ViewState["ipAddress"].ToString();
        //objSR.COLLEGE_CODE = Session["colcode"].ToString();
        ////objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        //objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        //objSR.COURSENOS = string.Empty;
        //objSR.SEMESTERNOS = string.Empty;
        //int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
        //int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
        //int status = 0;
        //int dstatus = 0;
        //int cntcourse = 0;
        /////////////////
        //objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        //objSA.DegreeNo = degreenos;
        //objSA.BranchNo = branchnos;
        //objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
        ////objSA.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        //objSA.IpAddress = ViewState["ipAddress"].ToString();

        //objSR.EXAM_REGISTERED = 0;

        //objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
        //objSR.TotalFee = objSR.Backlogfees;
        //////////////////

        //int Count = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "count(*)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND PREV_STATUS=1 AND ISNULL(CANCEL,0)=0"));

        //int YearBack = Convert.ToInt16(0);
        ////For Backlog Exam Registration Count
        ////string BacklogExamCount = objCommon.LookUp("Reff", "BacklogExam_Count", "");
        //string BacklogExamCount = "5";

        //int A = lvFailCourse.Items.Count;
        //if (lvFailCourse.Items.Count > 0)
        //{
        //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //    {
        //        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
        //        if (chk.Enabled == true)
        //            cntcourse++;
        //    }
        //}
        //if (cntcourse == 0)
        //{
        //    objCommon.DisplayMessage("Please Select Atleast one Course..!!", this.Page);
        //    return;
        //}
        //else
        //{
        //    if (lvFailCourse.Items.Count > 0)
        //    {
        //        //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "  AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1"));
        //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //        {
        //            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
        //            if (chk.Checked == true && chk.Enabled == true)
        //                status++;
        //        }
        //    }
        //    else
        //    {
        //        status = -1;
        //    }

        //    if (status > 0)
        //    {

        //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //        {
        //            //Get Student Details from lvStudent
        //            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
        //            if (cbRow.Checked == true && cbRow.Enabled == true)
        //            {
        //                objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
        //                objSR.SEMESTERNOS += ((dataitem.FindControl("lblsemester")) as Label).ToolTip + ",";
        //            }
        //        }
        //        objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
        //        objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd();

        //        //objSR.SEMESTERNOS = "3";
        //        //int count = 0;
        //        int ret = 0;
        //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //        {
        //            if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && (dataitem.FindControl("chkAccept") as CheckBox).Enabled == true)
        //            {
        //                objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
        //                ret = objSReg.AddExamRegisteredBacklaogRedoCourseReg(objSR);
        //            }

        //            //int ret = objSReg.AddExamRegisteredBacklaog(objSR);                    
        //        }
        //        //   int paymenttypenoOld = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
        //        // if (count > 0)
        //        // {

        //        //}
        //        if (ret == 1)
        //        // if (cs == CustomStatus.RecordSaved) ///Commented by Rita M.
        //        {
        //            //  objCommon.DisplayMessage("Student Backlog Exam Registration Done Successfully!!", this.Page);

        //            objCommon.DisplayMessage("Redo Course Registration Done Successfully!!!", this.Page);

        //            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //            {
        //                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
        //                {

        //                    CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
        //                    chkacc.Enabled = false;
        //                    //objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";

        //                    //// objSR.SEMESTERNOS = objSR.SEMESTERNOS + (dataitem.FindControl("lblsemester") as Label).ToolTip + "$";
        //                    ////string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
        //                    //objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
        //                    //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
        //                }
        //            }

        //            int a = lvFailCourse.Items.Count;
        //            int b = 0;
        //            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //            {

        //                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
        //                if (chk.Enabled == false)
        //                    b++;

        //            }

        //            btnSubmit.Enabled = (a == b) ? false : true;
        //        }

        //    }
        //    #region Backlog Exam Fees Related & Create Demand Not in Use


        //    #endregion
        //    else
        //    {
        //        objCommon.DisplayMessage("Please Select atleast one course", this.Page);
        //        return;
        //    }

        //}

        #endregion
    }
    #endregion

    protected void ddlBackLogSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBackLogSem.SelectedIndex == 0)
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            btnSubmit.Visible = false;
            btnReport.Visible = false;
            btnShow.Visible = false;
            btnPrintRegSlip.Visible = false;
        }
        else
        {
            DataSet dsFailSubjects = (DataSet)ViewState["FailSubjects"];

            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsFailSubjects.Tables[0].Select("SEMESTERNO =" + Convert.ToInt32(ddlBackLogSem.SelectedValue)).CopyToDataTable();
                lvFailCourse.DataSource = dt;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                btnSubmit.Visible = (Convert.ToInt16(Session["usertype"].ToString()) == 2) ? false : true;
                btnStudSubmit.Visible = (Convert.ToInt16(Session["usertype"].ToString()) != 2) ? false : true;
                pnlFailCourse.Visible = true;

                int a = lvFailCourse.Items.Count;
                int b = 0;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Enabled == false)
                        b++;
                }

                btnSubmit.Enabled = (a == b) ? false : true;
                btnStudSubmit.Enabled = (a == b) ? false : true;

                StudentController objSC = new StudentController();
                //DataSet dc = objSC.GetOfferedCourseForRegistration(Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToInt32(ViewState["SessionNo"]));
                //if (dc.Tables[0].Rows.Count > 0)
                //{
                //    lvOfferedCourse.DataSource = dc;
                //    lvOfferedCourse.DataBind();
                //}
                //else
                //{
                //    lvOfferedCourse.DataSource = null;
                //    lvOfferedCourse.DataBind();
                //}

                //DataSet dc = objSC.GetOfferedCourseForRegistration(Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToInt32(ViewState["SessionNo"]));
                if (dt.Rows.Count > 0)
                {
                    lvOfferedCourse.DataSource = dt;
                    lvOfferedCourse.DataBind();
                }
                else
                {
                    lvOfferedCourse.DataSource = null;
                    lvOfferedCourse.DataBind();
                }

            }
            else
            {
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                divCourses.Visible = true;
                objCommon.DisplayMessage("No Courses found...!!!", this.Page);
                btnSubmit.Visible = false;
                btnStudSubmit.Visible = false;
                pnlFailCourse.Visible = true;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();
        int admbatch = Convert.ToInt32(objCommon.LookUp("acd_student", "ADMBATCH", "IDNO=" + Convert.ToInt32(lblName.ToolTip)));
        int paymenttypeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO=" + Convert.ToInt32(lblName.ToolTip)));

        try
        {
            demandCriteria.StudentId = Convert.ToInt32(lblName.ToolTip);
            demandCriteria.StudentName = lblName.Text;
            demandCriteria.SessionNo = Convert.ToInt32(ViewState["SessionNo"]);
            demandCriteria.ReceiptTypeCode = "RRF";
            demandCriteria.BranchNo = Convert.ToInt16(lblBranch.ToolTip);
            demandCriteria.DegreeNo = Convert.ToInt16(hdfDegreeno.Value); ;
            demandCriteria.SemesterNo = Convert.ToInt16(ddlBackLogSem.SelectedValue);
            demandCriteria.AdmBatchNo = admbatch;
            demandCriteria.PaymentTypeNo = Convert.ToInt16(Session["payactivityno"]); // paymenttypeno;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.Remark = "Redo Course Registration Fees";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        //ShowReport("BacklogExamRegistrationSlip", "rptBacklogExamination_Form.rpt");
        ShowReport("ImprovementExamRegistrationSlip", "rptCourseRegSlip.rpt");
    }

    protected void ddlclgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlclgname.SelectedValue), "SESSIONNO desc");
        ddlSession.Enabled = true;
        txtEnrollno.Enabled = true;
        txtEnrollno.Text = string.Empty;
        divCourses.Visible = false;
        pnlFailCourse.Visible = false;
    }

    protected void btnStudSubmit_Click(object sender, EventArgs e)
    {
        //if (ViewState["usertype"].ToString() == "2")
        //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#thselect').hide();$('td:nth-child(1)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thselect').hide();$('td:nth-child(1)').hide();});", true);

        try
        {
            SaveEntry();
            //btnStudSubmit.Visible = true;
            btnPayment.Visible = false;
            if (!string.IsNullOrEmpty(ViewState["IsPaymetApplicable"].ToString()) && ViewState["IsPaymetApplicable"].ToString().ToLower().Equals("true"))
            {
                int payStatus = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO =" + lblSemester.ToolTip + " AND RECIEPT_CODE='RRF'"));
                if (payStatus > 0)
                {
                    objCommon.DisplayMessage("Redo Course Registration payment already done.", this.Page);
                    return;
                }

                decimal fees = 0;
                decimal total = 0;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true)
                    {
                        fees = Convert.ToDecimal(objCommon.LookUp("ACD_REDO_REG_FEE_DEFINITION", "ISNULL(FEES,0)FEES", ""));
                        total = total + fees;
                    }
                }

                ViewState["TotalFees"] = total;
                FeeDemand demandCriteria = this.GetDemandCriteria();
                //Create demand for Redo Course Registration
                CustomStatus cs = (CustomStatus)feeController.CreateDemandForRedoCourseReg(demandCriteria, lblEnrollNo.Text.Trim(), total);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    btnStudSubmit.Visible = false;
                    btnSubmit.Visible = false;
                    btnPayment.Visible = (Convert.ToInt16(Session["OrgId"]) == 2) ? true : false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.btnStudSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        try
        {
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            string PaymentMode = "REDO FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = ViewState["TotalFees"];
            Session["studName"] = lblName.Text;
            Session["studPhone"] = ViewState["StudentMobile"];
            Session["studEmail"] = ViewState["StudentEmail"];

            Session["ReceiptType"] = "RRF";
            Session["idno"] = Convert.ToInt32(Session["idno"]);

            //Session["paysemester"] =
            Session["homelink"] = "OnlinePayment.aspx";
            Session["regno"] = lblEnrollNo.Text;
            Session["payStudName"] = lblName.Text;
            Session["paymobileno"] = ViewState["StudentMobile"];
            Session["Installmentno"] = "0";
            Session["paysemester"] = ViewState["semesterno"];
            Session["paysession"] = ViewState["SessionNo"];

            DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RedoCourseRegistration.btnPayment_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void SaveEntry()
    {
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;
        string REGNO = string.Empty;
        DataSet ds = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO,REGNO", "DEGREENO,BRANCHNO", "IDNO=" + idno, "");
            if (ds.Tables[0].Rows.Count > 0)
                REGNO = ds.Tables[0].Rows[0]["REGNO"].ToString();
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "'");
            ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO,REGNO", "DEGREENO,BRANCHNO", "REGNO='" + txtEnrollno.Text.Trim() + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                REGNO = ds.Tables[0].Rows[0]["REGNO"].ToString();
                idno = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString());
            }
        }

        string schemeno = objCommon.LookUp("(SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE  IDNO = " + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND PASSFAIL = 'FAIL IN AGGREGATE' UNION ALL SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE IDNO =" + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND RESULT = 'F')A", "SCHEMENO", "");

        objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionnonew"]);
        objSR.IDNO = idno;
        objSR.REGNO = REGNO;
        objSR.ROLLNO = txtEnrollno.Text;
        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
        objSR.IPADDRESS = Session["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        objSR.COURSENOS = string.Empty;
        objSR.SEMESTERNOS = string.Empty;

        int status = 0;
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.DegreeNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
        objSA.BranchNo = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString());
        objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
        objSA.IpAddress = ViewState["ipAddress"].ToString();
        objSR.EXAM_REGISTERED = 0;
        objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
        objSR.TotalFee = objSR.Backlogfees;

        int Count = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "count(*)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND PREV_STATUS=1 AND ISNULL(CANCEL,0)=0"));

        int YearBack = Convert.ToInt16(0);

        if (lvFailCourse.Items.Count > 0)
        {
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                //if (chk.Checked == true && chk.Enabled == true)
                if (chk.Checked == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
                    objSR.SEMESTERNOS += ((dataitem.FindControl("lblsemester")) as Label).ToolTip + ",";
                    status++;
                }

            }
        }

        if (status > 0)
        {
            //objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
            //objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd();
            int redoImprovementCourseRegFlag = Convert.ToInt16(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(ALLOW_CURRENT_SEM_FOR_REDO_IMPROVEMENT_CRS_REG,0)ALLOW_CURRENT_SEM_FOR_REDO_IMPROVEMENT_CRS_REG", ""));
            ViewState["redoImprovementCourseRegFlag"] = redoImprovementCourseRegFlag;
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd();

            objSR.SEMESTERNOS = redoImprovementCourseRegFlag == 1 ? lblSemester.ToolTip : objSR.SEMESTERNOS.TrimEnd(',');
            int ret = 0;

            ret = objSReg.AddExamRegisteredImprovementCourseReg(objSR);
            if (ret == 1)
            {
                objCommon.DisplayMessage("Improvement Course Registration Done Successfully!!!", this.Page);
                //int b = 0;
                //foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                //{
                //    CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                //    if (chkacc.Checked)
                //    {
                //        chkacc.Enabled = false;
                //        b++;
                //    }
                //}

                //btnSubmit.Enabled = (lvFailCourse.Items.Count == b) ? false : true;

                bindcourses();
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select atleast one course", this.Page);
            return;
        }
    }
}