
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
using System.Net;
using System.IO;
using Dns = System.Net.Dns;
using AddressFamily = System.Net.Sockets.AddressFamily;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_ExamRegistration : System.Web.UI.Page
{

    #region Class
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();

    ExamController ExamC = new ExamController();
    Student_Acd objSA = new Student_Acd();
    bool IsNotActivitySem = false;
    bool flag = true;
    #endregion

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
                ////Page Authorization
                this.CheckPageAuthorization();
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 and college_id="+ddlclgname.SelectedValue); // ADD BY ROSHAN PANNASE 31-12-2015 FOR COURSE REGISTER IN START SESSION ONELY.

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //File Dropdown Box
               

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;


              
                    if (CheckActivity())
                    {
                        FillDropdown();
                        if (ViewState["usertype"].ToString() == "2")
                        {

                            int cid = 0;
                            int idno = 0;

                            idno = Convert.ToInt32(Session["idno"]);
                            cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

                            if (CheckActivityCollege(cid))
                            {
                                //CHECK ACTIVITY FOR EXAM REGISTRATION
                                //CheckActivity();
                                txtEnrollno.Text = string.Empty;
                                btnSearch.Visible = false;
                                btnCancel.Visible = false;
                                divCourses.Visible = true;
                                pnlSearch.Visible = false;
                                this.ShowDetails();



                                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                                {
                                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                                    {

                                        CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                                        chkacc.Enabled = false;

                                    }
                                }

                                int a = lvFailCourse.Items.Count;
                                int b = 0;
                                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                                {

                                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                                    if (chk.Enabled == false)
                                        b++;

                                }
                                if (a == b)
                                {
                                    btnSubmit.Enabled = false;
                                }
                                else
                                {
                                    btnSubmit.Enabled = true;
                                }

                                // BindStudentDetails();
                            }
                        }
                        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
                        {
                            //divNote.Visible = false;
                            pnlSearch.Visible = true;
                        }
                        else
                        {
                            //pnlStart.Visible = false;
                        }
                    }
               
                else
                {

                }
                
                btnSubmit.Visible = false;
                btnPrintRegSlip.Visible = false;
                //for ip address behind proxy
                //string ipadd = string.Empty;
                //string strHostName = Dns.GetHostName();
                //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                ////Label2.Visible = false;
                ////Label1.Visible = false;
                //string host = Dns.GetHostName();
                //IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                //string clientMachineName;
                //clientMachineName = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);



                //IPADDRESS = ip.AddressList[1].ToString();

                //IPADDRESS = objCommon.LookUp("sys.dm_exec_connections","CLIENT_NET_ADDRESS","SESSION_ID = @@SPID");

                //Label1.Text = Request.ServerVariables["REMOTE_USER"];
                //ipadd = Convert.ToString(ipEntry.AddressList[1]);
                // ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];
               // ViewState["ipAddress"] = "14.139.110.183"; //Request.ServerVariables["REMOTE_ADDR"];

                // string User_IPAddress = GetUserIPAddress();

                //User_IPAddress = IP_Array[0];

                //clientip = getIPAddress();
                //ViewState["ipAddress"] = clientip;
                //ViewState["ipAddress"] = IPADDRESS;
                //Label2.Text = System.Web.HttpContext.Current.Request.UserHostAddress; ;
            }
            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }

        divMsg.InnerHtml = string.Empty;
    }
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            //or
            //Client_IPAddress = Request.UserHostAddress;
            //or
            //User_IPAddress = Request.ServerVariables["REMOTE_HOST"];
        }
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
    private bool CheckActivity()
    {
        if (Convert.ToInt32(ViewState["usertype"]) == 2)
        {
            bool ret = true;
            string sessionno = string.Empty;

            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            ViewState["sessionno"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;

                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }

            }
            else
            {

                divenroll.Visible = false;
                btnSearch.Visible = false;
                btnCancel.Visible = false;
                // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
            }
            dtr.Close();
            return ret;
        }
        else
        {
            bool ret = true;
            string sessionno = string.Empty;

            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            ViewState["sessionno"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;

                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }

            }
            else
            {
                // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
            }
            dtr.Close();
            return ret;
        
        }
    }
    private void FillDropdown()
    {

       objCommon.FillDropDownList(ddlclgname, "ACD_COLLEGE_MASTER SM", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "SM.COLLEGE_ID DESC");
        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + ViewState["ACTIVITY_NO"], "");
        if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
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
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDcrCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
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

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                ddlSession.Enabled = true;
                txtEnrollno.Enabled = true;
                ddlclgname.ClearSelection();
                ddlSession.ClearSelection();
                txtEnrollno.Text = string.Empty;

                return;
                
            }
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
                        if (ViewState["semesternos"].ToString().Contains(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()))
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
                            //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                            hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                            hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                            lblBacklogFine.Text = "0";
                            lblTotalFee.Text = "0";

                            bindcourses();

                            //  objCommon.FillDropDownList(ddlBackLogSem, "(SELECT IDNO,SEMESTERNO  FROM ACD_TRRESULT T WHERE  IDNO = " + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND PASSFAIL = 'FAIL IN AGGREGATE' UNION ALL SELECT IDNO,SEMESTERNO FROM ACD_TRRESULT T WHERE IDNO =" + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND RESULT = 'F')A", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "", "SEMESTERNO");                       
                            // objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO > 0 AND PREV_STATUS=1 AND IDNO = " + idno, "SEMESTERNO DESC");

                            //int examtype = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO =" + sessionno));

                            ////if (examtype.Equals(1))
                            ////{
                            //    int Count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "count(*)", "(SEMESTERNO IN (SELECT * FROM UF_CSVTOTABLE('" + ViewState["semesternos"].ToString() + "'))) and  idno =" + idno + "  AND SEMESTERNO > 0  AND  ISNULL(GDPOINT,0)=0 AND SESSIONNO < " + sessionno));

                            //    if (Count > 0)
                            //    {

                            //objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT_HIST", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "(SEMESTERNO IN (SELECT * FROM UF_CSVTOTABLE('" + ViewState["semesternos"].ToString() + "'))) and  idno =" + idno + "  AND SEMESTERNO > 0  AND ISNULL(GDPOINT,0)=0 AND SESSIONNO < " + sessionno, "SEMESTERNO");
                            //    }
                            //    else
                            //    {
                            //        objCommon.DisplayMessage("No Backlog Found for this Student.!!", this.Page);
                            //    }
                            ////}
                            //else if (examtype.Equals(2))
                            //{
                            //objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT_HIST", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO > 0 AND IDNO = " + idno, "SEMESTERNO DESC");
                            //    // objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "(SEMESTERNO IN (SELECT * FROM UF_CSVTOTABLE('" + ViewState["semesternos"].ToString() + "'))) AND IDNO = " + idno, "SEMESTERNO DESC");
                            //}
                        }
                        else
                        {
                            IsNotActivitySem = true;
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
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
           // sessionno = Convert.ToInt32(Session["currentsession"]);
            sessionno = Convert.ToInt32(ViewState["sessionno"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEM=" + Convert.ToInt32(ddlBackLogSem.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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
        //divNote.Visible = false;
        //divCourses.Visible = true;

        int idno=0;
        int clgid=0;

        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

        if (REGNO != null && REGNO != string.Empty && REGNO != "")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
        }
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


        
       //idno = feeController.GetStudentIdByEnrollmentNo(REGNO);

       clgid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

       if (CheckActivityCollege(clgid))
       {
           btnShow.Visible = false;
          // txtEnrollno.Enabled = false;
           ddlSession.Enabled = false;
           ShowDetails();
           if (IsNotActivitySem == true)
           {
               objCommon.DisplayMessage("Activity Is Not Started For This Semester Student.", this.Page);
               divCourses.Visible = false;
               return;
           }
           else
           {
               if (flag.Equals(false))
               {
                   objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                   divCourses.Visible = false;
               }
               else
               {
                   // divCourses.Visible = true;
               }
           }
           //bindcourses();


           foreach (ListViewDataItem dataitem in lvFailCourse.Items)
           {
               if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
               {

                   CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                   chkacc.Enabled = false;

               }
           }

           int a = lvFailCourse.Items.Count;
           int b = 0;
           foreach (ListViewDataItem dataitem in lvFailCourse.Items)
           {

               CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
               if (chk.Enabled == false)
                   b++;

           }
           if (a == b)
           {
               btnSubmit.Enabled = false;
           }
           else
           {
               btnSubmit.Enabled = true;
           }

       }

       else
       { }
    }
    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;  

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + cid + " UNION ALL SELECT 0 AS SESSION_NO");
        //sessionno = Session["currentsession"].ToString();
        ViewState["sessionnonew"] = sessionno;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                //dvMain.Visible = false;
                ret = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                //dvMain.Visible = false;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            txtEnrollno.Text=string.Empty;
            // dvMain.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }
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
                               
        dsFailSubjects = objSC.GetStudentFailExamSubjectSNew_sem(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);
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
       // int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        StudentController objSC = new StudentController();
        DataSet dsFailSubjects;
       // DataSet dsDetainedStudent = null;
        if (ViewState["usertype"].ToString() != "1" && ViewState["usertype"].ToString() != "3" && ViewState["usertype"].ToString() != "7")
        {
            idno = Convert.ToInt32(Session["idno"]);
            //int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + semesterno + " AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));

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

        //int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        //if (exreg > 0)
        //{

     
        //    btnReport.Visible = true;
        //    objCommon.DisplayMessage("Selected Semester Exam Registration Already Done", this.Page);
        //    lvFailCourse.Enabled = true;
        //   // btnSubmit.Enabled = false;
        //    btnPrintRegSlip.Enabled = false;


          



        //}
        //else
        //{
        //    btnReport.Visible = false;
            
        //}
        if (idno == null || idno == 0)
        {
            objCommon.DisplayMessage("No Record Found...", this.Page);

        }
        else
        {
            // dsFailSubjects = objSC.GetStudentFailExamSubjects(idno, sessionno, semesterno);
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            //dsFailSubjects = objSC.GetStudentFailExamSubjectSNew(idno, sessionno, semesterno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);

            dsFailSubjects = objSC.GetStudentFailExamSubjectSNew_sem(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);
            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                lvFailCourse.DataSource = dsFailSubjects;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                btnSubmit.Visible = true;
                pnlFailCourse.Visible = true;

            }
            else
            {
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                divCourses.Visible = true;
                objCommon.DisplayMessage("No Courses found...!!!", this.Page);
                btnSubmit.Visible = false;
                pnlFailCourse.Visible = true;
            }


           
        }
       




        //if (dsFailSubjects != null && dsFailSubjects.Tables.Count > 0)
        //{
        //    if (dsFailSubjects.Tables[0].Rows.Count > 0)
        //    {


        //        int sum = 0;
        //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //        {
        //            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
        //            if (chk.Checked == false)
        //            {
        //                btnPrintRegSlip.Enabled = false;
        //            }
        //            else
        //                btnPrintRegSlip.Enabled = false;

        //            //string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "  AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");


        //            string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");

        //            if (count == "0")
        //            {
        //                if (chk.Checked == true)
        //                    sum++;
        //                chk.Enabled = true;
        //            }
        //            else
        //            {
        //                if (Session["usertype"].ToString() == "2")
        //                {
        //                    chk.Enabled = false;
        //                    btnPrintRegSlip.Enabled = true;
        //                    btnSubmit.Enabled = false;
        //                }
        //                else
        //                {
        //                    chk.Enabled = true;
        //                    btnSubmit.Enabled = true;
        //                }
        //            }
        //        }

        //        //if (sum.Equals(Convert.ToInt32(1)))
        //        //{
        //        //    lblBacklogFine.Text = "200";
        //        //    hdnBacklogFine.Value = "200";
        //        //    lblTotalFee.Text = "200";
        //        //    hdnTotalFee.Value = "200";
        //        //    objCommon.DisplayMessage("Backlog Registration done for this semester...!!!", this.Page);
        //        //}
        //        //else if (sum.Equals(Convert.ToInt32(2)))
        //        //{
        //        //    lblBacklogFine.Text = "400";
        //        //    hdnBacklogFine.Value = "400";
        //        //    lblTotalFee.Text = "400";
        //        //    hdnTotalFee.Value = "400";
        //        //    objCommon.DisplayMessage("Backlog Registration done for this semester...!!!", this.Page);
        //        //}
        //        //else if (sum.Equals(Convert.ToInt32(0)))
        //        //{
        //        //    lblBacklogFine.Text = "0";
        //        //    hdnBacklogFine.Value = "0";
        //        //    lblTotalFee.Text = "0";
        //        //    hdnTotalFee.Value = "0";
        //        //}
        //        //else
        //        //{
        //        //    lblBacklogFine.Text = "500";
        //        //    hdnBacklogFine.Value = "500";
        //        //    lblTotalFee.Text = "500";
        //        //    hdnTotalFee.Value = "500";
        //        //}
        //    }
        //}
        //else
        //{
        //    lvFailCourse.DataSource = null;
        //    lvFailCourse.DataBind();
        //    lvFailCourse.Visible = false;
        //    divCourses.Visible = true;
        //    objCommon.DisplayMessage("No Courses found for this semester...!!!", this.Page);
        //}
        //btnSubmit.Visible = true;
        //btnPrintRegSlip.Visible = false;

        if (ViewState["usertype"].ToString() == "2")
        {
           //btnPrintRegSlip.Enabled = true;
        }                         
    }
    
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int count_Backlog_idno = 0;
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "'");

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
        //string schemeno = objCommon.LookUp("ACD_STUDENT","SCHEMENO", "IDNO=" + idno);
        //string schemeno = objCommon.LookUp("(SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE  IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND PASSFAIL = 'FAIL IN AGGREGATE' UNION ALL SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE IDNO =" + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND RESULT = 'F')A", "SCHEMENO", "");


        string schemeno = objCommon.LookUp("(SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE  IDNO = " + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND PASSFAIL = 'FAIL IN AGGREGATE' UNION ALL SELECT TOP(1) SCHEMENO FROM ACD_TRRESULT T WHERE IDNO =" + idno + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO) AND RESULT = 'F')A", "SCHEMENO", "");



        string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);

        //objSR.SESSIONNO = Convert.ToInt32(Session["currentsession"]);
        //objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);

        objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionnonew"]);
        objSR.IDNO = idno;
        //Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).Text);
        objSR.REGNO = Regno;
        objSR.ROLLNO = txtEnrollno.Text;
        //objSR.SCHEMENO = Convert.ToInt32(schemeno);
        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
        //objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
        //objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        objSR.IPADDRESS = Session["ipAddress"].ToString(); ;// ----ViewState["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        //objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        objSR.COURSENOS = string.Empty;
        objSR.SEMESTERNOS = string.Empty;
        int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
        int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
        int status = 0;
        int dstatus = 0;
        int cntcourse = 0;
        ///////////////
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.DegreeNo = degreenos;
        objSA.BranchNo = branchnos;
        objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
        //objSA.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        objSA.IpAddress = ViewState["ipAddress"].ToString();

        objSR.EXAM_REGISTERED = 0;

        objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
        objSR.TotalFee = objSR.Backlogfees;
        ////////////////

        int Count = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "count(*)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND PREV_STATUS=1 AND ISNULL(CANCEL,0)=0"));

        int YearBack = Convert.ToInt16(0);
        //For Backlog Exam Registration Count
        //string BacklogExamCount = objCommon.LookUp("Reff", "BacklogExam_Count", "");
        string BacklogExamCount = "5";
       
        int A = lvFailCourse.Items.Count;
        if (lvFailCourse.Items.Count > 0)
        {
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Enabled == true)
                    cntcourse++;
            }
        }
        if (cntcourse == 0)
        {
            objCommon.DisplayMessage("Please Select Atleast one Course..!!", this.Page);
            return;
        }
        else
        { 
       
        if (lvFailCourse.Items.Count > 0)
        {
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "  AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1"));
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true && chk.Enabled == true)
                    status++;
            }
        }
        else
        {
            status = -1;
        }

        string CourseSemAmt = string.Empty;
        if (status > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
                    objSR.SEMESTERNOS += ((dataitem.FindControl("lblsemester")) as Label).ToolTip + ",";
                    CourseSemAmt += ((dataitem.FindControl("lblAmt")) as Label).ToolTip + ",";
                }
            }
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd(',');
            objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd(',');
            CourseSemAmt = CourseSemAmt.TrimEnd(',');

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && (dataitem.FindControl("chkAccept") as CheckBox).Enabled == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";

                    // objSR.SEMESTERNOS = objSR.SEMESTERNOS + (dataitem.FindControl("lblsemester") as Label).ToolTip + "$";
                    //string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
                    //  objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
                    //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
                }
            }

       //     FeeDemand demandCriteria = this.GetDemandCriteria();
            //int ret = objSReg.AddExamRegisteredBacklaog(objSR);
            int ret = objSReg.AddExamRegisteredBacklaog(objSR);


            int ret_LOG = ExamC.AddExamRegisteredBacklaog_All_LOG(objSR, objSR.SEMESTERNOS);  // THIS IS USED TO INSERT THE BACKLOG COURSES DATA LOG AS PER TKNO-45071 ON DT 26062023


            int retdemand = objSReg.AddExamRegistrationDetails_Backlog(objSR, Convert.ToString(CourseSemAmt));
         //   int paymenttypenoOld = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));

            if (ret == 1 && retdemand ==1)
            // if (cs == CustomStatus.RecordSaved) ///Commented by Rita M.
            {
                //  objCommon.DisplayMessage("Student Backlog Exam Registration Done Successfully!!", this.Page);

                objCommon.DisplayMessage("Backlog Exam Registration Done Successfully!!!", this.Page);

                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {

                        CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                        chkacc.Enabled = false;
                        //objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";

                        //// objSR.SEMESTERNOS = objSR.SEMESTERNOS + (dataitem.FindControl("lblsemester") as Label).ToolTip + "$";
                        ////string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
                        //objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
                        //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
                    }
                }

                int a = lvFailCourse.Items.Count;
                int b = 0;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {

                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Enabled == false)
                        b++;

                }
                if (a == b)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }

            }

        }
        #region Backlog Exam Fees Related & Create Demand Not in Use

        //        int semno = Convert.ToInt32(lblSemester.ToolTip);
        //            decimal total = 0;
        //            //if ((semno == 1 && schemetype == 1) || (semno == 2 && schemetype == 1) || (semno == 3 && schemetype == 1) || (semno == 4 && schemetype == 1))
        //            //{
        //            //    total = objSR.CourseFee + objSR.CommanFee + objSR.LateFine + objSR.Backlogfees;
        //            //}
        //            //else
        //            //{
        //               // count_Backlog_idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "count(isnull(IDNO,0))IDNO", "IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PREV_STATUS=1 "));
        //               // count_Regular_idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "count(isnull(IDNO,0))IDNO", "IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (PREV_STATUS=0 OR  PREV_STATUS is null)"));

         //                //// For Regular And Backlog Student  
        //                //if (count_Backlog_idno > 0 && count_Regular_idno > 0)
        //                //{
        //                //    total = amount + objSR.LateFine + objSR.Backlogfees + objSR.CourseFee;
        //                //}
        //                //// For Backlog Student  
        //                //else if (count_Backlog_idno > 0)
        //                //{
        //                    total = objSR.Backlogfees;
        //            //    }

         //            //    // For Regular Student
        //            //    else
        //            //    {
        //            //        total = amount + objSR.LateFine + objSR.CourseFee;
        //            //    }
        //            //}

         //       //Create demand for Backlog Student...........
        //            feeController.CreateDemandForExaminationForBacklog(demandCriteria, paymenttypenoOld, lblEnrollNo.Text.Trim(), total);
        //            if ((Session["usertype"].ToString().Equals("2"))) //student
        //            {
        //                objCommon.DisplayMessage("Provisional exam registration successfull. Print registration slip.", this.Page);

         //                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //                {
        //                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
        //                    string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "  AND SESSIONNO=" +  Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");
        //                    if (count == "0")
        //                    {
        //                        chk.Enabled = true;
        //                    }
        //                    else
        //                    {
        //                        if (Session["usertype"].ToString() == "2")
        //                        {
        //                            chk.Enabled = false;
        //                            btnPrintRegSlip.Enabled = true;
        //                            btnSubmit.Enabled = false;
        //                        }
        //                        else
        //                        {
        //                            chk.Enabled = true;
        //                        }
        //                        btnPrintRegSlip.Visible = true;
        //                        btnPrintRegSlip.Enabled = true;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                objCommon.DisplayMessage("Provisional Backlog Exam Registration Done and Exam Fees Demand Created Successfully.", this.Page);

         //                btnPrintRegSlip.Enabled = true;

         //                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //                {
        //                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
        //                    {

         //                        CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
        //                        chkacc.Enabled = false;
        //                        //objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";

         //                        //// objSR.SEMESTERNOS = objSR.SEMESTERNOS + (dataitem.FindControl("lblsemester") as Label).ToolTip + "$";
        //                        ////string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
        //                        //objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
        //                        //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
        //                    }
        //                }



         //            }
        //            //btnSubmit.Visible = false;
        //           // btnPrintRegSlip.Enabled = true;
        //            //txtRollNo.Enabled = true;
        //            // ShowReport("ExamRegistrationSlip", "rptExamRegForm.rpt");
        //        }
        //}
        #endregion
        else
        {
            objCommon.DisplayMessage("Please Select atleast one course", this.Page);
            return;
        }
            //COUNT THE HOW MANY SUBJECTS
            //status = dstatus + status + 1;
            //if (status > 0)
            //{

            //    int branchno = Convert.ToInt32(lblBranch.ToolTip);
            //    int admbatch = Convert.ToInt32(lblAdmBatch.ToolTip);
            //    int degreeno = Convert.ToInt32(hdfDegreeno.Value);
            //    int categoryno = Convert.ToInt32(hdfCategory.Value);
            //    int SubCount;
            //    if (categoryno == 0)
            //    {
            //        categoryno = 4;
            //    }
            //    //= Convert.ToInt32(lvFailCourse.Items.Count.ToString());
            //    //if (status > 3)
            //    if (status > 4)
            //    {
            //        SubCount = 2;
            //    }
            //    else
            //    {
            //        SubCount = 1;
            //    }
            //    int semesterno = Convert.ToInt32(lblSemester.ToolTip);
            //    //double ExamAmt = Convert.ToDouble(objCommon.LookUp("ACD_EXAM_FEES", "AMOUNT", "DEGREENO='" + degreeno + "' AND CATEGORYNO='" + categoryno + "' AND SUB_LIMIT_NO='" + SubCount + "' AND SESSIONNO=" + objSR.SESSIONNO));
            //    //int studentIDs = Convert.ToInt32(Session["idno"].ToString());
            //    int studentIDs = idno;
            //    double ExamAmt = 200;
            //    bool overwriteDemand = true;

            //    string receiptno = this.GetNewReceiptNo();
            //    FeeDemand dcr = this.GetDcrCriteria();

            //    ///new code for late fees 03/04/2013
            //    DateTime today = DateTime.Now.Date;
            //    DateTime LastExamdate = Convert.ToDateTime(objCommon.LookUp("REFF", "Exam_Last_Date", "")).Date;
            //    double CalLateExmAmt;

            //    int day = Convert.ToInt32((today - LastExamdate).TotalDays);
            //    if (day > 0)
            //    {
            //        //This code for the not count the sunday and saturday.
            //        int holidayCount = 0;

            //        for (DateTime dt = LastExamdate; dt < today; dt = dt.AddDays(1.0))
            //        {
            //            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
            //            {
            //                holidayCount++;
            //            }
            //        }

            //        day = day - holidayCount;
            //        double LateExamAmt = Convert.ToDouble(objCommon.LookUp("REFF", "Exam_Late_Fee_Amt", ""));
            //        CalLateExmAmt = LateExamAmt * day;
            //    }
            //    else
            //    {
            //        CalLateExmAmt = 0;
            //    }
            //    /////

            //    string dcritem = dmController.CreateDcrForBacklogStudents(studentIDs, dcr, 1, overwriteDemand, receiptno, ExamAmt, CalLateExmAmt);

            //    string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + semesterno + " AND SESSIONNO=" + objSR.SESSIONNO);


            //    //FeeDemand demandCriteria = this.GetDemandCriteria();
            //    //string studentIDs = Session["idno"].ToString();
            //    //bool overwriteDemand = true;

            //    //string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + studentIDs.ToString() + " AND SEMESTERNO=" + semesterno + " AND SESSIONNO=" + objSR.SESSIONNO);

            //    //if (Convert.ToInt32(demandno) <= 0)
            //    //{
            //    //    string response = dmController.CreateDemandForStudents(studentIDs, demandCriteria, semesterno, overwriteDemand);
            //    //}


            //}

            //else
            //{
            //    objCommon.DisplayMessage("Please Select atleast one course selected in course list", this.Page);
            //}
        }
    }
    #endregion
    #region ddl
    protected void ddlBackLogSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBackLogSem.SelectedItem.Value == "0")
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
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
        }
        
    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();
        int branchno = Convert.ToInt32(objCommon.LookUp("acd_student", "branchno", "regno='" + lblEnrollNo.Text + "'"));
        int degreeno = Convert.ToInt32(objCommon.LookUp("acd_student", "degreeno", "regno='" + lblEnrollNo.Text + "'"));
      //  int semesterno = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", "regno='" + lblEnrollNo.Text + "'"));
        int admbatch = Convert.ToInt32(objCommon.LookUp("acd_student", "ADMBATCH", "regno='" + lblEnrollNo.Text + "'"));
        int paymenttypeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
        //lblEnrollNo


        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            demandCriteria.StudentId = Convert.ToInt32(idno);
            demandCriteria.StudentName = lblName.Text;
            //demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.SessionNo = Convert.ToInt32(ViewState["sessionno"]);
            demandCriteria.ReceiptTypeCode = "EF";
            demandCriteria.BranchNo = branchno;
            demandCriteria.DegreeNo = degreeno;
            demandCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            demandCriteria.AdmBatchNo = admbatch;
            demandCriteria.PaymentTypeNo = paymenttypeno;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.Remark = "BackLog Fees";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EaxmRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("BacklogExamRegistrationSlip", "rptBacklogExamination_Form.rpt");
    }


    //private void ShowDetails()
    //{

    //    lvFailCourse.DataSource = null;
    //    lvFailCourse.DataBind();

    //    int idno = 0;
    //    int sessionno = Convert.ToInt32(Session["currentsession"]);
    //    StudentController objSC = new StudentController();

    //    // Commented below code because we added drop dwon list for session

    //    string Session_Name = string.Empty;
    //    Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
    //    lblsession.Text = Convert.ToString(Session_Name);

    //    string Session_No = string.Empty;
    //    Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
    //    Session["SESSIONNO"] = Convert.ToInt32(Session_No);

    //    if (ViewState["usertype"].ToString() == "2")
    //    {
    //        idno = Convert.ToInt32(Session["idno"]);
    //    }
    //    else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || Session["usertype"].ToString() == "12")
    //    {
    //        idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
    //    }
    //    try
    //    {
    //        if (idno > 0)
    //        {
    //            DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

    //            if (dsStudent != null && dsStudent.Tables.Count > 0)
    //            {
    //                if (dsStudent.Tables[0].Rows.Count > 0)
    //                {
    //                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
    //                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

    //                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
    //                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

    //                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
    //                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
    //                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
    //                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
    //                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
    //                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
    //                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
    //                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
    //                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
    //                    //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
    //                    hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
    //                    hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
    //                    lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
    //                    hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
    //                    int admbatchno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"]);
    //                    // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
    //                    ViewState["IDNO"] = lblName.ToolTip;

    //                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
    //                    DataSet dasem;
    //                    // dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL'  AND SEMESTERNO NOT IN (SELECT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO <=" + lblSemester.ToolTip + " AND  IDNO  =" + ViewState["IDNO"] + "   AND SEMESTERNO > 0  AND  PASSFAIL = 'PASS' )", "SEMESTERNO");
    //                    dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL' ", "SEMESTERNO");
    //                    ddlBackLogSem.Items.Clear();
    //                    ddlBackLogSem.Items.Add("Please Select");
    //                    ddlBackLogSem.SelectedItem.Value = "0";

    //                    int oddeven = 0;
    //                    if (Convert.ToInt32(lblSemester.ToolTip) == 1 || Convert.ToInt32(lblSemester.ToolTip) == 3 || Convert.ToInt32(lblSemester.ToolTip) == 5 || Convert.ToInt32(lblSemester.ToolTip) == 7 || Convert.ToInt32(lblSemester.ToolTip) == 9)
    //                    {
    //                        oddeven = 1;
    //                    }
    //                    else if (Convert.ToInt32(lblSemester.ToolTip) == 2 || Convert.ToInt32(lblSemester.ToolTip) == 4 || Convert.ToInt32(lblSemester.ToolTip) == 6 || Convert.ToInt32(lblSemester.ToolTip) == 8 || Convert.ToInt32(lblSemester.ToolTip) == 10)
    //                    {
    //                        oddeven = 2;
    //                    }
    //                    if (dasem != null && dasem.Tables[0].Rows.Count > 0)
    //                    {
    //                        if (hdfDegreeno.Value == "1")//For B.Arch Student Can Apply for oddbacklog to oddcurrent and evenbacklog to even semesters only others can apply all semesters
    //                        {
    //                            //  DataSet dasem;
    //                            // dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL'  AND SEMESTERNO NOT IN (SELECT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO <=" + lblSemester.ToolTip + " AND  IDNO  =" + ViewState["IDNO"] + "   AND SEMESTERNO > 0  AND  PASSFAIL = 'PASS' )", "SEMESTERNO");
    //                            //ddlBackLogSem.Items.Clear();
    //                            //ddlBackLogSem.Items.Add("Please Select");// if ( ViewState["addeven"] !=null && Convert.ToInt32(ViewState["addeven"]) == 2)
    //                            if (oddeven == 2)
    //                            {
    //                                //if ((Convert.ToInt32(ddlSession.SelectedValue)) % 2 == 0)
    //                                //{
    //                                for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
    //                                {
    //                                    if (Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 == 0)
    //                                    {
    //                                        ListItem newlist = new ListItem(dasem.Tables[0].Rows[i]["SEMESTER"].ToString(), dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString());
    //                                        ddlBackLogSem.Items.Add(newlist);
    //                                    }
    //                                }
    //                                //}
    //                            }
    //                            else if (oddeven == 1)  // if ( ViewState["addeven"] !=null && Convert.ToInt32(ViewState["addeven"]) == 1)
    //                            {
    //                                for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
    //                                {
    //                                    if (Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 != 0)
    //                                    {
    //                                        ListItem newlist = new ListItem(dasem.Tables[0].Rows[i]["SEMESTER"].ToString(), dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString());
    //                                        ddlBackLogSem.Items.Add(newlist);

    //                                    }
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (dasem != null && dasem.Tables[0].Rows.Count > 0)
    //                            {
    //                                ddlBackLogSem.DataSource = dasem;
    //                                ddlBackLogSem.DataValueField = dasem.Tables[0].Columns[0].ToString();
    //                                ddlBackLogSem.DataTextField = dasem.Tables[0].Columns[1].ToString();
    //                                ddlBackLogSem.DataBind();
    //                                ddlBackLogSem.SelectedIndex = 0;
    //                            }
    //                            // objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL'  AND SEMESTERNO NOT IN (SELECT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO <=" + lblSemester.ToolTip + " AND  IDNO  =" + ViewState["IDNO"] + "   AND SEMESTERNO > 0  AND  PASSFAIL = 'PASS' )", "SEMESTERNO");

    //                        }
    //                    }                     
    //                }
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}




    protected void ddlclgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlclgname.SelectedValue), "SESSIONNO desc");
        ddlSession.Enabled = true;
        txtEnrollno.Enabled = true;
        txtEnrollno.Text = string.Empty;
        divCourses.Visible = false;
        pnlFailCourse.Visible = false;

    }
}