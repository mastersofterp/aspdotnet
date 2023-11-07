//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EXAMINATION                                                             
// PAGE NAME     : CODING DECODING GENERATION                                           
// CREATION DATE : 16-APRIL-2019                                                          
// CREATED BY    : ROHIT KUMAR TIWARI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_DecodingGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objStudent = new ExamController();
    //ConnectionString
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
        divMsg.InnerHtml = string.Empty;
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

                //lblPreSession.Text = Session["currentsession"].ToString();

                DataTableReader dtr = objCommon.FillDropDown("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO = " + Session["currentsession"].ToString(), string.Empty).CreateDataReader();
                if (dtr.Read())
                {
                    lblPreSession.Text = dtr["SESSION_NAME"].ToString();
                    lblPreSession.ToolTip = dtr["SESSIONNO"].ToString();
                }
                dtr.Close();
                ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                PopulateDropDown();
                btnGenNo.Enabled = false;
                btnLock.Enabled = false;
                btnReport.Enabled = false;

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                //check type of number enabled in Exam Configue
                var decode = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "isnull(DECODE_NUMBER,0)", "");
                int decodeNum = Convert.ToInt32(decode);
                var seat = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "isnull(SEAT_NUMBER,0)", "");
                int seatnum = Convert.ToInt32(seat);
                var barcode = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "isnull(BARCODE,0)", "");
                int barcodenum = Convert.ToInt32(barcode);

                if (decodeNum == 1 && seatnum != 1 && barcodenum != 1)
                {
                    ddlNumType.Items.Clear();
                    //ddlNumType.Items.Insert(0, "Please Select");
                    ddlNumType.Items.Insert(0, new ListItem("Decode Number", "1"));
                    ddlNumType.SelectedValue = "1";
                    hdfNumtypeStat.Value = "1";
                    spanDecodebtn.Visible = true;
                    ddlNumType.Enabled = false;
                    btnGenNo.Enabled = true;
                    divtxtNumberSeries.Visible = false;
                }
                else if (seatnum == 1 && decodeNum != 1 && barcodenum != 1)
                {
                    ddlNumType.Items.Clear();
                    ddlNumType.Items.Insert(0, new ListItem("Seat Number", "2"));
                    ddlNumType.SelectedValue = "2";
                    hdfNumtypeStat.Value = "2";
                    spanDecodebtn.Visible = true;
                    ddlNumType.Enabled = false;
                    btnGenNo.Enabled = true;
                    //divtxtNumberSeries.Visible = true;
                    btnReport.Visible = false;

                    btnReport.Text = "PRINT SEAT NUMBER";
                }
                else if (barcodenum == 1 && seatnum != 1 && decodeNum != 1)
                {
                    ddlNumType.Items.Clear();
                    //ddlNumType.Items.Insert(0, "Please Select");
                    ddlNumType.Items.Insert(0, new ListItem("Barcode Number", "3"));
                    ddlNumType.SelectedValue = "3";
                    hdfNumtypeStat.Value = "3";
                    spanDecodebtn.Visible = true;
                    ddlNumType.Enabled = false;
                    btnGenNo.Enabled = true;
                    divtxtNumberSeries.Visible = false;

                    btnReport.Text = "PRINT BARCODE NUMBER";
                }
                else if (decodeNum == 1 && seatnum == 1 && barcodenum != 1)
                {
                    ddlNumType.Items.Insert(1, new ListItem("Decode Number", "1"));
                    ddlNumType.Items.Insert(2, new ListItem("Seat Number", "2"));
                    //ddlNumType.Items.Insert(3, new ListItem("Barcode Number", "3"));
                    ddlNumType.Enabled = true;
                    hdfNumtypeStat.Value = "2";
                    btnGenNo.Enabled = false;
                    divtxtNumberSeries.Visible = false;
                }
                else if (decodeNum == 1 && seatnum != 1 && barcodenum == 1)
                {
                    ddlNumType.Items.Insert(1, new ListItem("Decode Number", "1"));
                    //ddlNumType.Items.Insert(2, new ListItem("Seat Number", "2"));
                    ddlNumType.Items.Insert(2, new ListItem("Barcode Number", "3"));
                    ddlNumType.Enabled = true;
                    hdfNumtypeStat.Value = "2";
                    btnGenNo.Enabled = false;
                    divtxtNumberSeries.Visible = false;
                }
                else if (decodeNum != 1 && seatnum == 1 && barcodenum == 1)
                {
                    //ddlNumType.Items.Insert(1, new ListItem("Decode Number", "1"));
                    ddlNumType.Items.Insert(1, new ListItem("Seat Number", "2"));
                    ddlNumType.Items.Insert(2, new ListItem("Barcode Number", "3"));
                    ddlNumType.Enabled = true;
                    hdfNumtypeStat.Value = "2";
                    btnGenNo.Enabled = false;
                    divtxtNumberSeries.Visible = false;
                }
                else if (decodeNum == 1 && seatnum == 1 && barcodenum == 1)
                {
                    ddlNumType.Items.Insert(1, new ListItem("Decode Number", "1"));
                    ddlNumType.Items.Insert(2, new ListItem("Seat Number", "2"));
                    ddlNumType.Items.Insert(3, new ListItem("Barcode Number", "3"));
                    ddlNumType.Enabled = true;
                    hdfNumtypeStat.Value = "2";
                    btnGenNo.Enabled = false;
                    divtxtNumberSeries.Visible = false;
                }
                else
                {
                    ddlNumType.Items.Clear();
                    ddlNumType.Items.Insert(0, new ListItem("Please Select", "0"));
                    ddlNumType.Items.Insert(1, new ListItem("Decode Number", "1"));
                    ddlNumType.Items.Insert(2, new ListItem("Seat Number", "2"));
                    ddlNumType.Items.Insert(3, new ListItem("Barcode Number", "3"));
                }
                //SetFocus(ddlClgScheme);
                ddlClgScheme.Focus();
            }
        }
        //ddlClgScheme.Focus();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            //    Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            //}
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"].ToString() + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            //Fill Dropdown Session.
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //Fill Dropdown Scheme
            ddlClgScheme.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DecodingGeneration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnGenNo_Click(object sender, EventArgs e)
    {
        int NumType = Convert.ToInt32(ddlNumType.SelectedValue);
        if (NumType == 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Select Type of Number To Generate", this);
            return;
        }
        try
        {
            string recordCount = objCommon.LookUp("ACD_LOG_RANDOMNO WITH (NOLOCK)", "COUNT(*)", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND BRANCHNO = " + ViewState["branchno"].ToString());
            string BarcoderecordCount = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(BARCODE_NO)", "SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ViewState["schemeno"] + " AND SEMESTERNO = " + ddlsemester.SelectedValue);

            if (recordCount != "0" && NumType == 1)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Decode Number Already Exits For Selected Course.", this);
                return;
            }
            else if (recordCount != "0" && NumType == 3)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Barcode Number Already Exits For This Selection.", this);
                return;
            }

            if (recordCount == "0" && NumType == 1)
            {
                #region NumType == 1 // Decode Number

                if (ddlCourse.SelectedIndex <= 0)
                {
                    DataSet CoursenoDs = objCommon.FillDropDown(" ACD_STUDENT_RESULT", "DISTINCT COURSENO", "(CCODE +' - '+ COURSENAME)COURSENAME,SUBID", " SESSIONNO=" + ddlSession.SelectedValue + "AND SEMESTERNO=" + ddlsemester.SelectedValue + " AND EXAM_REGISTERED=1 AND CANCEL=0 AND SCHEMENO=" + ViewState["schemeno"].ToString(), "SUBID");

                    CustomStatus mulCs = CustomStatus.Others;
                    for (int i = 0; i <= CoursenoDs.Tables[0].Rows.Count - 1; i++)
                    {
                        mulCs = (CustomStatus)objStudent.GenerateDecodeNumber(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"].ToString()), Convert.ToInt32(CoursenoDs.Tables[0].Rows[i]["COURSENO"].ToString()), Convert.ToInt32(ddlDigits.SelectedValue), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());

                    }
                    if (mulCs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Decode Number Generated Successfully", this);
                        BindListView();
                        //Clear();

                    }
                    else if (mulCs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Seat Number Generate Successfully", this);
                        BindListView();
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Deocde/Seat Number Generation Process Not Done", this);
                        BindListView();
                    }

                }
                else
                {
                    CustomStatus cs = (CustomStatus)objStudent.GenerateDecodeNumber(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"].ToString()), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlDigits.SelectedValue), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Decode Number Generated Successfully", this);
                        BindListView();
                        //Clear();

                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Deocde Generation Process Not Done", this);
                        BindListView();
                    }
                }

                #endregion
            }
            else if (NumType == 2)
            {
                #region NumType == 2 // Seat Number

                //false number
                //if (ddlCourse.SelectedIndex <= 0)
                //{
                //    objCommon.DisplayMessage(UpdatePanel1, "Please Select Course", Page);
                //    return;
                //}
                //else if (txtNumSeries.Text == string.Empty)
                //{
                //    objCommon.DisplayMessage(UpdatePanel1, "Please Enter False Number Series", Page);
                //    return;
                //}

                int numSeries;

                try
                {
                    numSeries = Convert.ToInt32(txtNumSeries.Text);
                }
                catch
                {
                    numSeries = 0;
                }

                //Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"].ToString()), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlDigits.SelectedValue), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString())

                //CustomStatus ret = (CustomStatus)objStudent.GenerateFalseNumber(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), numSeries, Convert.ToInt32(ViewState["branchno"].ToString()), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString()); // Commented By Sagar Mankar On Date 06112023 For PCEN

                CustomStatus ret = (CustomStatus)objStudent.GenerateSeatNumber(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), numSeries, Convert.ToInt32(ViewState["branchno"].ToString()), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString()); // Added By Sagar Mankar On Date 06112023 For PCEN

                if (ret.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Seat Number Generated Successfully", this);
                    txtNumSeries.Text = string.Empty;
                    BindListView();
                    //Clear();

                }
                else if (ret.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Seat Number Already Exists", this);
                }
                else if (ret.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Lock Absent Student Entry", this);
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Seat Number Generation Process Not Done", this);
                    BindListView();
                }

                #endregion
            }
            else if (NumType == 3)
            {
                #region NumType == 3 // Barcode Number

                if (ddlCourse.SelectedIndex <= 0)
                {
                    DataSet CoursenoDs = objCommon.FillDropDown(" ACD_STUDENT_RESULT", "DISTINCT COURSENO", "(CCODE +' - '+ COURSENAME)COURSENAME,SUBID", " SESSIONNO=" + ddlSession.SelectedValue + "AND SEMESTERNO=" + ddlsemester.SelectedValue + " AND EXAM_REGISTERED=1 AND CANCEL=0 AND SCHEMENO=" + ViewState["schemeno"].ToString(), "SUBID");

                    CustomStatus mulCs = CustomStatus.Others;
                    //for (int i = 0; i <= CoursenoDs.Tables[0].Rows.Count - 1; i++)
                    //{
                    //    mulCs = (CustomStatus)objStudent.GenerateBarcodeNumber(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"].ToString()), Convert.ToInt32(CoursenoDs.Tables[0].Rows[i]["COURSENO"].ToString()), Convert.ToInt32(ddlDigits.SelectedValue), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());

                    //}
                    //for (int i = 0; i <= CoursenoDs.Tables[0].Rows.Count - 1; i++)
                    //{
                    mulCs = (CustomStatus)objStudent.GenerateBarcodeNumber(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));

                    //}
                    if (mulCs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Barcode Number Generated Successfully", this);
                        BindListView();
                        //Clear();

                    }
                    else if (mulCs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Seat Number Generate Successfully", this);
                        BindListView();
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Deocde/Seat Number Generation Process Not Done", this);
                        BindListView();
                    }

                }
                else
                {
                    CustomStatus cs = (CustomStatus)objStudent.GenerateBarcodeNumber(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Barcode Number Generated Successfully", this);
                        BindListView();
                        //Clear();

                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Barcode Generation Process Not Done", this);
                        BindListView();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Decode Number Already Exits For Selected Course", this);
                lvClear();
                gvClear();
                Clear();
            }

                #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DecodingGeneration.btnGenNo_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        lvClear();
        gvClear();
        Clear();

        ddlClgScheme.Focus();
    }

    private void lvClear()
    {
        lvDecodeNo.DataSource = null;
        lvDecodeNo.DataBind();
        lvDecodeNo.Visible = false;
        pnlLst.Visible = false;

    }
    private void gvClear()
    {
        rptDecodeStat.Visible = false;
        gvDecode.DataSource = null;
        gvDecode.DataBind();
    }
    private void Clear()
    {
        ddlClgScheme.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlNumType.SelectedIndex = 0;
        // btnGenNo.Enabled = false;
        pStatics.Visible = false;
        rptDecodeStat.Visible = false;
        ddlNumType.Enabled = true;
        txtNumSeries.Text = string.Empty;
        //spanDecodebtn.Visible = false;
        //ddlDigits.SelectedIndex = 0;
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        if (ddlClgScheme.SelectedIndex <= 0 || ddlSession.SelectedIndex <= 0 || ddlsemester.SelectedIndex <= 0 || ddlCourse.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Select College & Scheme/Session/Semester/Course for Locking", this.Page);
            return;
        }

        int lck = 1;    //lock = true
        CustomStatus cs = (CustomStatus)objStudent.UpdateLockDecodeNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), lck);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(UpdatePanel1, "Decoded Numbers Locked for Selected Course!!", this);
            //Clear();
            //lvClear();
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Failed to Lock Decode Number!!", this);
        }


        BindListView();
    }

    private void BindListView()
    {
        DataSet ds = null;

        if (ddlCourse.SelectedIndex > 0)
        {
            if (ddlNumType.SelectedValue == "1")
            {
                // if (ViewState["branchno"].ToString() != "99")
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "(SR.CCODE +' - '+ SR.COURSENAME)COURSENAME, SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.DECODENO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and SR.EXAM_REGISTERED=1 AND EXTERMARK IS NULL AND ISNULL(SR.DETAIND,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND C.COURSENO = " + ddlCourse.SelectedValue + "AND SR.SUBID=1 AND BRANCHNO=" + ViewState["branchno"].ToString(), "SR.REGNO");
            }
            else if (ddlNumType.SelectedValue == "2")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "(SR.CCODE +' - '+ SR.COURSENAME)COURSENAME, SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.BARCODE_NO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and SR.EXAM_REGISTERED=1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND C.COURSENO = " + ddlCourse.SelectedValue + "AND BRANCHNO=" + ViewState["branchno"].ToString(), "SR.REGNO");
            }
            else if (ddlNumType.SelectedValue == "3")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "(SR.CCODE +' - '+ SR.COURSENAME)COURSENAME, SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.BARCODE_NO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and SR.EXAM_REGISTERED=1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND C.COURSENO = " + ddlCourse.SelectedValue + "AND BRANCHNO=" + ViewState["branchno"].ToString(), "SR.BARCODE_NO");
            }
        }
        else
        {
            if (ddlNumType.SelectedValue == "1")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "(SR.CCODE +' - '+ SR.COURSENAME)COURSENAME, SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.DECODENO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and SR.EXAM_REGISTERED=1 AND EXTERMARK IS NULL AND ISNULL(SR.DETAIND,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SUBID=1 AND BRANCHNO=" + ViewState["branchno"].ToString(), "SR.REGNO");
            }
            else if (ddlNumType.SelectedValue == "2")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "(SR.CCODE +' - '+ SR.COURSENAME)COURSENAME, SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.BARCODE_NO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and SR.EXAM_REGISTERED=1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND BRANCHNO=" + ViewState["branchno"].ToString(), "SR.REGNO");
            }
            else if (ddlNumType.SelectedValue == "3")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "(SR.CCODE +' - '+ SR.COURSENAME)COURSENAME, SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.BARCODE_NO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and SR.EXAM_REGISTERED=1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND BRANCHNO=" + ViewState["branchno"].ToString(), "SR.BARCODE_NO");
            }

            //Label lblHead = (Label)lvDecodeNo.Items[0].FindControl("lblHead");
            ////Label lblHead = lvDecodeNo.FindControl("lblHead") as Label;
            //lblHead.Text = ddlNumType.SelectedItem.ToString();

            ////Label lblhead = (Label)lvDecodeNo.FindControl("lblHead") as Label;
            //Label lblhead = (Label)lvDecodeNo.FindControl("lblHead");
            //lblhead.Text = "no"; //ddlNumType.SelectedItem.Text;
        }
        //else
        //    ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO", "SR.IDNO", "C.COURSE_NAME ,SR.REGNO,SR.SESSIONNO,SR.SEATNO,SR.DECODENO,SR.EXTERMARK", "isnull(cancel,0)=0 and EXAM_REGISTERED=1 AND ISNULL(DETAIND,0)=0  AND SESSIONNO = " + ddlSession.SelectedValue + " AND C.COURSENO = " + ddlCourse.SelectedValue + "AND BRANCHNO=" + ViewState["branchno"].ToString() + "AND SR.SEATNO IS NOT NULL ", "right(sr.seatno,3)");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDecodeNo.DataSource = ds;
                lvDecodeNo.DataBind();

                Label lblHead = lvDecodeNo.FindControl("lblHead") as Label;
                lblHead.Text = ddlNumType.SelectedItem.ToString();

                for (int i = 0; i < lvDecodeNo.Items.Count; i++)
                {
                    if (ddlNumType.SelectedValue == "1")
                    {
                        Label lblCode = (Label)lvDecodeNo.Items[i].FindControl("lblCode");
                        lblCode.Text = ds.Tables[0].Rows[i]["DECODENO"].ToString();
                    }
                    else if (ddlNumType.SelectedValue == "2")
                    {
                        Label lblCode = (Label)lvDecodeNo.Items[i].FindControl("lblCode");
                        lblCode.Text = ds.Tables[0].Rows[i]["SEATNO"].ToString();
                    }
                    else if (ddlNumType.SelectedValue == "3")
                    {
                        Label lblCode = (Label)lvDecodeNo.Items[i].FindControl("lblCode");
                        lblCode.Text = ds.Tables[0].Rows[i]["BARCODE_NO"].ToString();
                    }
                }

                lvDecodeNo.Visible = true;
                pnlLst.Visible = true;
                btnLock.Enabled = true;
            }
            else
                objCommon.DisplayMessage(UpdatePanel1, "No Records", this.Page);
            btnLock.Enabled = false;
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "No Records", this.Page);
            btnLock.Enabled = false;
        }
        ChkLockStat();

    }
    private void ChkLockStat()
    {
        string cnt = string.Empty;

        cnt = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SR.IDNO = S.IDNO)", "DISTINCT (CASE WHEN DECODENOLOCK IS NULL THEN 0 ELSE DECODENOLOCK END) AS DECODENOLOCK", "SR.EXAM_REGISTERED = 1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND isnull(cancel,0)=0 and SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());

        if (cnt == "1")
            btnLock.Enabled = false;
        else
            btnLock.Enabled = true;

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvClear();
        gvClear();
        //ddlNumType.SelectedIndex = 0;
        // btnGenNo.Enabled = false;
        pStatics.Visible = false;

        if (ddlCourse.SelectedIndex <= 0)
        {
            ChkLockStat();
        }

        //Get Total & Absent Students..

        //if (ViewState["branchno"].ToString() == "99")    //No Branch..
        //{
        //    abs = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK)", "COUNT(*)", "(EXTERMARK = -1 OR EXTERMARK = -2.00) AND EXAM_REGISTERED = 1 AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 and SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue);
        //    tot = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK)", "COUNT(*)", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 and  COURSENO = " + ddlCourse.SelectedValue);
        //}
        //else
        //{

        // btnGenNo.Enabled = true;
        //btnLock.Enabled = true;
        btnReport.Enabled = true;
        btnShow.Focus();
    }

    protected void lvDecodeNo_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //if ((e.Item.FindControl("hdfAB") as HiddenField).Value == "-1")
        //{
        //    (e.Item.FindControl("lblABP") as Label).Text = "<span style='color:Red;font-weight:bold'>ABSENT</span>";
        //}

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            var status = e.Item.FindControl("lblABP") as Label;

            string a = status.Text;
            if (a == "-1.00")
            {
                status.Text = "ABSENT";
                status.ForeColor = System.Drawing.Color.Red;
                //status.Font = new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Bold);
                //status.ForeColor = System.Drawing.FontStyle.Bold;
            }
            else
            {
                status.Text = "";
            }

        }
    }



    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvClear();
        gvClear();
        ddlsemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;

        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + ViewState["schemeno"].ToString(), "S.SEMESTERNO");

                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                ddlsemester.Focus();
            }
            else
            {
                ddlsemester.Items.Clear();
                ddlsemester.Items.Add("Please Select");
                ddlsemester.SelectedIndex = 0;

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedIndex = 0;

                ddlSession.Focus();
            }

            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
            pnlLst.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DecodingGeneration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "B.LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue, "A.BRANCHNO");

            }
            else
            {
                ddlBranch.SelectedIndex = 0;
                ddlBranch.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
            pnlLst.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNumType.SelectedValue == "1")
            {
                ShowReport("DecodedNumber_Report", "rptDecodeNumber.rpt");
            }
            else if (ddlNumType.SelectedValue == "3")
            {
                ShowReport("BarcodeNumber_Report", "rptBarcodeNumber.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DecodingGeneration.btnReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string ccode = objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));

        try
        {
            if (ddlNumType.SelectedValue == "1")
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"].ToString()) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_CCODE=" + ccode.ToString() + ",@P_COURSENO=" + ddlCourse.SelectedValue;
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
            else if (ddlNumType.SelectedValue == "3")
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlsemester.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue;
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DecodingGeneration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlClgScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlNumType.SelectedIndex = 0;
        // btnGenNo.Enabled = false;
        lvClear();
        gvClear();

        try
        {
            if (ddlClgScheme.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgScheme.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // FILL DROPDOWN  ddlSession_SelectedIndexChanged
                }

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ISNULL (IS_ACTIVE,0)= 1", "SESSIONNO DESC");

                ddlSession.SelectedIndex = 0;
                ddlSession.Focus();

                //ddlSession.Items.Clear();
                //ddlSession.Items.Add("Please Select");
                //ddlSession.SelectedIndex = 0;

                ddlsemester.Items.Clear();
                ddlsemester.Items.Add("Please Select");
                ddlsemester.SelectedIndex = 0;

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedIndex = 0;
            }
            else
            {
                ddlSession.Items.Clear();
                ddlSession.Items.Add("Please Select");
                ddlSession.SelectedIndex = 0;

                ddlsemester.Items.Clear();
                ddlsemester.Items.Add("Please Select");
                ddlsemester.SelectedIndex = 0;

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedIndex = 0;

                ddlClgScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Exam_MarkConversion.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvClear();
        ddlCourse.SelectedIndex = 0;
        // ddlNumType.SelectedIndex = 0;
        gvClear();

        if (ddlsemester.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlCourse, " ACD_STUDENT_RESULT", "DISTINCT COURSENO", "(CCODE +' - '+ COURSENAME)COURSENAME,SUBID", " SESSIONNO=" + ddlSession.SelectedValue + "AND SEMESTERNO=" + ddlsemester.SelectedValue + " AND EXAM_REGISTERED=1 AND isnull(CANCEL,0)=0 AND SUBID=1 AND SCHEMENO=" + ViewState["schemeno"].ToString(), "SUBID"); //Commented By Sagar Mankar On Date 10102023 For BARCODE
            objCommon.FillDropDownList(ddlCourse, " ACD_STUDENT_RESULT", "DISTINCT COURSENO", "(CCODE +' - '+ COURSENAME)COURSENAME,SUBID", " SESSIONNO=" + ddlSession.SelectedValue + "AND SEMESTERNO=" + ddlsemester.SelectedValue + " AND EXAM_REGISTERED=1 AND isnull(CANCEL,0)=0 AND SCHEMENO=" + ViewState["schemeno"].ToString(), "COURSENO"); // Added By Sagar Mankar On Date 10102023 For BARCODE

            // objCommon.FillListBox(lstCourse, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON S.IDNO=SR.IDNO", "DISTINCT COURSENO", "CCODE +' - '+ COURSENAME", "SESSIONNO=" + ddlSession.SelectedValue + "AND BRANCHNO=" + ViewState["branchno"].ToString() + " AND DEGREENO=" + ViewState["degreeno"].ToString() + " AND SR.SCHEMENO=" + ViewState["schemeno"].ToString(), "COURSENO");
            if (ddlNumType.SelectedValue == "1" || ddlNumType.SelectedValue == "3")
            {
                spanDecodebtn.Visible = true;
                btnReport.Visible = true;
            }
            else
            {
                spanDecodebtn.Visible = true;
                btnReport.Visible = false;
            }

            ddlCourse.Focus();
        }
        else
        {
            pStatics.Visible = false;

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("Please Select");
            ddlCourse.SelectedIndex = 0;

            ddlsemester.Focus();
        }
    }

    protected void ddlNumType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNumType.SelectedIndex > 0 && ddlsemester.SelectedIndex > 0)
            btnGenNo.Enabled = true;
        else
            btnGenNo.Enabled = false;
        this.GetStatus();

        if (ddlNumType.SelectedValue == "2")
        {
            //divtxtNumberSeries.Visible = true;
            spanDecodebtn.Visible = true;
            btnReport.Visible = false;
        }
        else if (ddlNumType.SelectedValue == "1")
        {
            spanDecodebtn.Visible = true;
            divtxtNumberSeries.Visible = false;

            btnReport.Text = "PRINT DECODE NUMBER";
        }
        else if (ddlNumType.SelectedValue == "3")
        {
            spanDecodebtn.Visible = true;
            divtxtNumberSeries.Visible = false;
            btnReport.Visible = true;

            btnReport.Text = "PRINT BARCODE NUMBER";
        }
        else
        {
            divtxtNumberSeries.Visible = false;
            spanDecodebtn.Visible = true;
        }


    }
    private void GetStatus()
    {
        if (ddlNumType.SelectedValue == "1")
        {
            string tot = string.Empty;
            string abs = string.Empty;
            string totDec = string.Empty;
            string noDec = string.Empty;
            if (ddlCourse.SelectedIndex > 0)
            {
                pStatics.Visible = true;

                abs = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "(EXTERMARK = -1 OR EXTERMARK = -2) AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 and EXAM_REGISTERED = 1 AND isnull(EXTERMARK,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                tot = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + "AND isnull(EXTERMARK,0)=0 AND ISNULL(CANCEL,0)=0 AND ISNULL(DETAIND,0)=0 AND SR.SUBID=1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND  COURSENO = " + ddlCourse.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());
                //  }

                totDec = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + "AND DECODENO IS NOT NULL AND isnull(EXTERMARK,0)=0 AND ISNULL(CANCEL,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND ISNULL(DETAIND,0)=0 AND  COURSENO = " + ddlCourse.SelectedValue + "AND SR.SUBID=1  AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                noDec = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + "AND DECODENO IS NULL AND isnull(EXTERMARK,0)=0 AND ISNULL(CANCEL,0)=0 AND ISNULL(DETAIND,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND COURSENO = " + ddlCourse.SelectedValue + "AND SR.SUBID=1  AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                lblTot.Text = "Total Students : " + tot;
                lblAb.Text = "<span style='color:Red'>Absent Students : " + abs + "</span>";
                lblHDec.Text = "Decode Generated  : " + totDec;
                lblNDec.Text = "Decode Not Generated : " + noDec;
            }
            else
            {
                pStatics.Visible = true;
                abs = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "(EXTERMARK = -1 OR EXTERMARK = -2)AND SR.SUBID=1  AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 AND isnull(EXTERMARK,0)=0 and EXAM_REGISTERED = 1 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + "AND SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                tot = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SR.SUBID=1  AND SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND isnull(EXTERMARK,0)=0 AND ISNULL(DETAIND,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND  SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                totDec = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SR.SUBID=1 AND SESSIONNO = " + ddlSession.SelectedValue + "AND DECODENO IS NOT NULL AND isnull(EXTERMARK,0)=0 AND ISNULL(CANCEL,0)=0 AND ISNULL(DETAIND,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND  SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                noDec = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SR.SUBID=1  AND SESSIONNO = " + ddlSession.SelectedValue + "AND DECODENO IS NULL AND isnull(EXTERMARK,0)=0 AND ISNULL(CANCEL,0)=0 AND ISNULL(DETAIND,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"].ToString() + " AND  SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND S.BRANCHNO = " + ViewState["branchno"].ToString());

                lblTot.Text = "Total Students : " + tot;
                lblAb.Text = "<span style='color:Red'>Absent Students : " + abs + "</span>";
                lblHDec.Text = "Decode Generated  : " + totDec;
                lblNDec.Text = "Decode Not Generated : " + noDec;
            }
        }
        else
        {
            pStatics.Visible = false;
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        rptDecodeStat.Visible = false;

        this.BindListView();
        this.GetStatus();

        if (ddlNumType.SelectedValue == "1" || ddlNumType.SelectedValue == "3")
        {
            btnReport.Enabled = true;
        }
        else
        {
            btnReport.Visible = false;
        }
    }


    protected void btnStatDecode_Click(object sender, EventArgs e)
    {
        lvClear();
        //ddlNumType.SelectedIndex = 0;
        ViewState["genStatus"] = "GENERATED";
        GetGridSDB();
    }

    //to get Session Degree Branch for grid view
    private void GetGridSDB()
    {
        DataSet statDs = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON(SR.SESSIONNO=SM.SESSIONNO)INNER JOIN ACD_SCHEME SC ON(SR.SCHEMENO=SC.SCHEMENO)INNER JOIN ACD_DEGREE D ON(SC.DEGREENO= D.DEGREENO)INNER JOIN ACD_BRANCH BR ON(SC.BRANCHNO=BR.BRANCHNO)", "TOP 1 SM.SESSION_PNAME", "D.DEGREENAME,SR.SCHEMENO,BR.LONGNAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + " AND  SR.SCHEMENO=" + ViewState["schemeno"].ToString(), "");
        if (statDs.Tables.Count > 0)
        {
            if (statDs.Tables[0].Rows.Count > 0)
            {
                rptDecodeStat.Visible = true;
                gvDecode.DataSource = statDs;
                gvDecode.DataBind();
            }
            else
            {
                rptDecodeStat.Visible = false;
                gvDecode.DataSource = null;
                gvDecode.DataBind();
            }
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "No Record", this.Page);
            return;
        }
    }

    #region old
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNumType.SelectedIndex = -1;
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, " ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON S.IDNO=SR.IDNO", "DISTINCT COURSENO", "CCODE +' - '+ COURSENAME", " SESSIONNO=" + ddlSession.SelectedValue + "AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue, "COURSENO");

            }
            else
            {
                ddlDegree.SelectedIndex = 0;
                ddlDegree.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
            pnlLst.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO=" + ddlBranch.SelectedValue, "SCHEMENO");
            }
            else
            {
                ddlScheme.SelectedIndex = 0;
                ddlScheme.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    //to get Decode status Course wise

    private DataSet GetDecodeStatus(string Status)
    {
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@COMMAND_TYPE";
        string sp_call = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + Status;
        string sp_name = "PKG_ACD_GET_DECODE_STATUS";
        DataSet dsCourse = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
        return dsCourse;
    }

    private DataSet GetBarcodeStatus(string Status)
    {
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@COMMAND_TYPE";
        string sp_call = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + ddlsemester.SelectedValue + "," + Status;
        string sp_name = "PKG_ACD_GET_BARCODE_STATUS";
        DataSet dsCourse = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
        return dsCourse;
    }

    private DataSet GetSeatStatus(string Status)
    {
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@COMMAND_TYPE";
        string sp_call = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + ddlsemester.SelectedValue + "," + Status;
        string sp_name = "PKG_ACD_GET_SEAT_STATUS";
        DataSet dsCourse = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
        return dsCourse;
    }

    protected void gvDecode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvChildGrid = (GridView)e.Row.FindControl("gvChildGrid");

            HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;


            // DataSet dsCourse = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER SM ON(SR.SEMESTERNO=SM.SEMESTERNO) INNER JOIN ACD_COURSE C ON(SR.COURSENO = C.COURSENO)", "SM.SEMESTERNAME", "SM.SEMESTERNAME,C.COURSE_NAME,COUNT(*)STATUS", " SR.SESSIONNO=211 AND SR.SCHEMENO=30 AND SR.DECODENO IS NULL GROUP BY SM.SEMESTERNAME,C.COURSE_NAME", "");

            //string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@COMMAND_TYPE";
            //string sp_call = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + "GENERATED";
            //string sp_name = "PKG_ACD_GET_DECODE_STATUS";

            string genStat = ViewState["genStatus"].ToString();
            DataSet dsCourse = null;

            if (ddlNumType.SelectedValue == "1")
            {
                dsCourse = GetDecodeStatus(genStat);
            }
            else if (ddlNumType.SelectedValue == "2")
            {
                dsCourse = GetSeatStatus(genStat);
            }
            else if (ddlNumType.SelectedValue == "3")
            {
                dsCourse = GetBarcodeStatus(genStat);
            }

            if (dsCourse.Tables.Count > 0)
            {
                if (dsCourse.Tables[0].Rows.Count > 0)
                {
                    gvChildGrid.DataSource = dsCourse;
                    gvChildGrid.DataBind();
                }
                else
                {

                    gvChildGrid.DataSource = null;
                    gvChildGrid.DataBind();
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "No Records", this.Page);
                return;
            }
        }
    }
    protected void btnNotGenDe_Click(object sender, EventArgs e)
    {
        lvClear();
        //ddlNumType.SelectedIndex = 0;
        ViewState["genStatus"] = "NOTGENERATED";
        GetGridSDB();

    }
}