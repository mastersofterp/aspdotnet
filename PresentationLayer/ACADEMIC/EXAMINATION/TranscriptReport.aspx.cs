//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Comprehensive Student Report
// CREATION DATE : 
// CREATED BY    : Manish 
// MODIFIED BY   : 
// MODIFIED DATE : 13/04/2016
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

using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;

public partial class ACADEMIC_EXAMINATION_TranscriptReportUG : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();

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
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
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

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;
            }

            divSession.Visible = false;

            imgPhoto.ImageUrl = "~/images/nophoto.jpg";

        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtEnrollmentSearch.Text = string.Empty;

                }
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string RegNo = txtEnrollmentSearch.Text.ToString();
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + RegNo + "' AND ISNULL(ADMCAN,0)=0");
        if (!string.IsNullOrEmpty(idno))
        {
            ShowDetails();
            if (Convert.ToInt32(Session["OrgId"]) != 9)// Atlas Added by gaurav 27_10_2022
            {
                GenerateQrCode();
            }
            btntranscripwithoutheader.Visible = false;
            btnTranscriptWithHeader.Visible = true;
            btnTranscriptWithFormat.Visible = true;


            // btnConsolgradecard.Visible = true;
            // btnConsolegradewithoutheader.Visible = true;
            #region Atlas
            if (Convert.ToInt32(Session["OrgId"]) == 9)// Atlas Added by gaurav 27_10_2022
            {
                btnReport.Visible = true;
                txtSpecilization.Visible = false;
                txtresult.Visible = false;
                lbSpecilization.Visible = false;
                lbResult.Visible = false;
                lblDateofIssue.Visible = false;
                txtDateofIssue.Visible = false;
                divdate.Visible = false;

            }
            #endregion
            #region Crescent
            else if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                btnTranscriptWithHeader.Visible = true;
                btntranscripwithoutheader.Visible = false;
                btnReport.Visible = false;
                txtSpecilization.Visible = false;
                txtresult.Visible = false;
                lbSpecilization.Visible = false;
                lbResult.Visible = false;
                lblDateofIssue.Visible = false;
                txtDateofIssue.Visible = false;
                divdate.Visible = false;

                //if (Convert.ToInt32(Session["OrgId"]) == 2)
                //{
                //    divSession.Visible = false;
                //}
                //else
                //{
                divSession.Visible = true;

                objCommon.FillDropDownList(ddlSession, "ACD_TRRESULT ASR INNER JOIN ACD_SESSION_MASTER ASM ON (ASR.SESSIONNO=ASM.SESSIONNO)", "DISTINCT ASM.SESSIONNO", "ASM.SESSION_NAME", "ASM.SESSIONNO > 0 AND ASR.REGNO='" + txtEnrollmentSearch.Text + "'", "ASM.SESSION_NAME DESC");
                //}

            }
            #endregion
            else
            {
                btnReport.Visible = true;
                txtSpecilization.Visible = true;
                txtresult.Visible = true;
                lbSpecilization.Visible = true;
                lbResult.Visible = true;
                lblDateofIssue.Visible = true;
                txtDateofIssue.Visible = true;
                divdate.Visible = true;
            }

        }

        else
        {
            btntranscripwithoutheader.Visible = false;
            btnTranscriptWithHeader.Visible = false;
            //btnConsolgradecard.Visible = false;
            // btnConsolegradewithoutheader.Visible = false;
            btnReport.Visible = false;
            txtSpecilization.Visible = false;
            txtresult.Visible = false;
            lbSpecilization.Visible = false;
            lbResult.Visible = false;
            objCommon.DisplayMessage(this.updpnlExam, txtEnrollmentSearch.Text.Trim() + " details not found Please check Admission Status.", this);
            Clear();
        }

    }

    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }

    private void GenerateQrCode()
    {
        string RegNo = txtEnrollmentSearch.Text.ToString();
        string idno = objCommon.LookUp("ACD_STUDENT", "idno", "REGNO='" + RegNo + "' AND ISNULL(ADMCAN,0)=0");
        if (!string.IsNullOrEmpty(idno))
        {
            int Idno = Convert.ToInt32(idno);

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON S.DEGREENO=D.DEGREENO INNER JOIN ACD_BRANCH B ON S.BRANCHNO=B.BRANCHNO", "DEGREENAME,LONGNAME BRANCH,*", "", "REGNO='" + RegNo + "'", "REGNO");

            //string BranchName = objCommon.LookUp("ACD_BRANCH","SHORTNAME","BRANCHNO="+ ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Trim()+"");
            //SELECT @V_DURATION = DURATION FROM ACD_COLLEGE_DEGREE_BRANCH WHERE COLLEGE_ID = @V_COLLEGE_ID AND DEGREENO = @V_DEGREENO AND BRANCHNO = @V_BRANCHNO
            int Duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO=" + ds.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND BRANCHNO=" + ds.Tables[0].Rows[0]["BRANCHNO"].ToString()));
            int finalSemester = Duration * 2;
            DataSet ds1 = objQrC.GetStudentResultData(Idno);
            //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";

            string Qrtext = "Student Name:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Enrollment No.:" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; Degree:" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString().Trim() + "; Branch:" + ds.Tables[0].Rows[0]["BRANCH"].ToString().Trim(); //+ "; CGPA:" +
            // ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS1"].ToString().Trim() + "; S2=" +
            // ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS2"].ToString().Trim() + "; S3=" +
            ds1.Tables[0].Rows[0]["CGPA"].ToString().Trim();
            //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS4"].ToString().Trim() + "; S5=" +
            //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS5"].ToString().Trim() + "; S6=" +
            //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS6"].ToString().Trim() + "; S7=" +
            //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS7"].ToString().Trim() + "; S8=" +
            //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS8"].ToString().Trim() + "; S9=" +
            //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS9"].ToString().Trim() + ";";
            Session["qr"] = Qrtext.ToString();

            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeVersion = 10;
            Bitmap img = encoder.Encode(Session["qr"].ToString());
            //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
            //img.Save(Server.MapPath("~\\img.Jpeg"));
            //img.Save(Server.MapPath("~\\img.Jpeg"));

            string imagepath = Server.MapPath("~/") + @"img.Jpeg";
            img.Save(imagepath);

            ViewState["File"] = imageToByteArray(imagepath);
            byte[] QR_IMAGE = ViewState["File"] as byte[];
            //long ret = objQrC.AddUpdateQrCode(Idno, QR_IMAGE);
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlExam, txtEnrollmentSearch.Text.Trim() + " details not found Please check Admission Status.", this);
        }
    }
    private void ShowDetails()
    {
        Clear();
        int idno;
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();

        idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());

        ViewState["idno"] = idno;
        try
        {
            if (idno > 0)
            {
                DataTableReader dtr = objSC.GetStudentDetails(idno);

                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        // int PCITY=Convert.ToInt32(dtr["PCITY"]);

                        lblRegNo.Text = dtr["REGNO"].ToString();
                        string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                        lblBranch.Text = branchname;
                        lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                        lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                        string caste = objCommon.LookUp("ACD_CASTE", "CASTE", "CASTENO=" + dtr["CASTE"].ToString());
                        lblCaste.Text = caste;
                        string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["CATEGORYNO"].ToString());
                        lblCategory.Text = category;
                        string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                        lblReligion.Text = religion;
                        string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                        lblNationality.Text = nation;
                        lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                        //string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());

                        string PCITY = dtr["PCITY"] == DBNull.Value ? "0" : dtr["PCITY"].ToString();

                        string Pcity = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + PCITY);
                        lblCity.Text = Pcity;

                        // lblCity.Text = dtr["CITY"] == null ? "0" : dtr["CITY"].ToString();

                        lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                        lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                        lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                        //lblEnrollNo.Text = dtr["ROLLNO"] == null ? string.Empty : dtr["ROLLNO"].ToString();
                        string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                        lblSemester.Text = semester;
                        string LCITY = dtr["LCITY"] == DBNull.Value ? "0" : dtr["LCITY"].ToString();
                        string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + LCITY);
                        lblPCity.Text = city;
                        //  lblCity.Text = dtr["CITY"] == null ? "0" : dtr["CITY"].ToString();
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENT";
                        ViewState["Degreeno"] = dtr["DEGREENO"].ToString();
                        ViewState["Branchno"] = dtr["BRANCHNO"].ToString();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, txtEnrollmentSearch.Text.Trim() + " details not found Please check Admission Status.", this);
                lblSemester.Text = string.Empty;

                //ShowMessage("No student found having Reg no.: " + txtEnrollmentSearch.Text.Trim());
                //lblEnrollNo.Text = string.Empty;

            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GenerateTranscriptNumber()
    {
        objQrC.GenerationofTranscriptnumber(Convert.ToInt32(ViewState["idno"].ToString()), Convert.ToInt32(ViewState["Degreeno"].ToString()), Convert.ToInt32(ViewState["Branchno"].ToString()));
    }
    private void Clear()
    {
        lblRegNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblName.Text = string.Empty;
        lblMName.Text = string.Empty;
        lblDOB.Text = string.Empty;
        lblCaste.Text = string.Empty;
        lblCategory.Text = string.Empty;
        lblReligion.Text = string.Empty;
        lblNationality.Text = string.Empty;
        lblLAdd.Text = string.Empty;
        lblLLNo.Text = string.Empty;
        lblMobNo.Text = string.Empty;
        lblPAdd.Text = string.Empty;
        imgPhoto.ImageUrl = null;
        lblSemester.Text = string.Empty;
        lblCity.Text = string.Empty;
        lblPCity.Text = string.Empty;

    }
    private void ShowTranscript(string reportTitle, string rptFileName)
    {
        try
        {
            DateTime dateofissue = DateTime.MinValue;
            string dateofIssue = txtDateofIssue.Text == "" ? string.Empty : (txtDateofIssue.Text);
            if (dateofIssue == string.Empty)
            {
                dateofIssue = "";
            }
            else
            // DateTime dateofissue = Convert.ToDateTime(txtDateofIssue.Text);
            {

                if (txtDateofIssue.Text != "")
                {
                    dateofissue = Convert.ToDateTime(txtDateofIssue.Text);
                    txtDateofIssue.Text = dateofissue.ToString("dd-MMM-yyyy");
                    // dateofissue =Convert.ToDateTime( txtDateofIssue.Text);
                }
                else
                {
                    DateTime? dtt = null;
                    dateofissue = DateTime.MinValue;
                }
            }


            string spec = txtSpecilization.Text;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (dateofIssue == string.Empty)
            {

                if (Convert.ToInt32(Session["OrgId"]) == 9)// Atlas Added by gaurav 27_10_2022
                {

                    int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + ViewState["idno"]));
                    url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + ViewState["idno"].ToString() + ",@P_RESULT=" + txtresult.Text + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0;

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + ViewState["idno"]));//added on 01052023
                    url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + ViewState["idno"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue;
                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"].ToString() + ",@P_RESULT=" + txtresult.Text + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0;//+ ""

                }
            }
            else
            {

                if (Convert.ToInt32(Session["OrgId"]) == 2)
                {

                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"].ToString();

                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"].ToString() + ",@P_RESULT=" + txtresult.Text + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0 + ",@DateofIssue=" + txtDateofIssue.Text;
                }

            }
            //,@P_REPORT_STATUS=" + txtReportStatus.Text + "";

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnTranscript_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32 ((ViewState["idno"])) >0)
    //    {
    //        //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
    //        this.ShowTranscript("Transcript", "rptTranscripReport.rpt");
    //    }
    //}
    protected void btnReport_Click(object sender, EventArgs e)
    {
        // ShowAllResult("AllResult", "rpt_StudentAllResultUGGreater2013-2014.rpt");

        if (Convert.ToInt32(Session["OrgId"]) == 9) // Atlas Added by gaurav 27_10_2022
        {
            ShowAllResult("AllResult", "rpt_StudentAllResult_Atlas.rpt");
        }
        else
        {
            //ShowAllResult("AllResult", "rpt_StudentAllResult.rpt"); // Commented By Sagar Mankar On Date 04072023 For CPUKOTA
            ShowAllResult("AllResult", "rpt_StudentAllResult_CC.rpt"); // Added By Sagar Mankar On Date 04072023 For CPUKOTA
        }
    }

    private void ShowAllResult(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +  ",@P_IDNO=" + ViewState["idno"].ToString()+",@P_SEMESTERNO="+0+"";

            if (Convert.ToInt32(Session["OrgId"]) == 9)// Atlas Added by gaurav 27_10_2022
            {

                int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + ViewState["idno"]));
                // url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + ViewState["idno"].ToString() + ",@P_RESULT=" + txtresult.Text + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0;
                url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + ViewState["idno"].ToString();

            }
            else
            {


                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"].ToString();
            }
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnTranscriptBackReport_Click(object sender, EventArgs e)
    //{
    //    int Admbatch = Convert.ToInt16( objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO="+ Convert.ToInt16( ViewState["idno"].ToString()) +""));

    //    if (Admbatch < 5)
    //    {
    //        this.ShowTranscriptBack("Transcript_Back_Report", "rptTranscriptBackReportLessADMFive.rpt");
    //    }
    //    else
    //    {
    //        this.ShowTranscriptBack("Transcript_Back_Report", "rptTranscriptBackReportGreaterADMFive.rpt");
    //    }
    //}


    //private void ShowTranscriptBack(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_IDNO=" + ViewState["idno"].ToString()+"";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void btnTranscriptWithHeader_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32((ViewState["idno"])) > 0)
        {
            //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
            //this.ShowTranscript("Transcript Report UG", "rptTranscripReportWithHeaderUG.rpt");
            GenerateTranscriptNumber();
            if (Convert.ToInt32(Session["OrgId"]) == 9)// Atlas Added by gaurav 21_10_2022
            {

                this.ShowTranscript("Transcript Report", "rptTranscriptNew_atlas.rpt");
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 2)
            {

                this.ShowTranscript("Transcript Report", "rptGradeCardReportPG_Trans.rpt");

            }
            else
            {
                //this.ShowTranscript("Transcript Report", "rptTranscriptNew.rpt"); -- Commented By Sagar Mankar On Date 04072023 For CPUKOTA
                this.ShowTranscript("Transcript Report", "rptTranscriptNew_CC.rpt"); // Added By Sagar Mankar On Date 04072023 For CPUKOTA
            }

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btntranscripwithoutheader_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32((ViewState["idno"])) > 0)
        {
            //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
            //this.ShowTranscript("Transcript Report UG", "rptTranscripReportWithHeaderUG.rpt");
            this.ShowTranscript("Transcript Report", "rptTranscriptNewwithoutheader.rpt");
        }
    }
    protected void btnConsolgradecard_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32((ViewState["idno"])) > 0)
        {
            //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
            //this.ShowTranscript("Transcript Report UG", "rptTranscripReportWithHeaderUG.rpt");
            this.ShowTranscript("Transcript Report", "rptgradecardNew.rpt");
        }

    }
    protected void btnConsolegradewithoutheader_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32((ViewState["idno"])) > 0)
        {
            //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
            //this.ShowTranscript("Transcript Report UG", "rptTranscripReportWithHeaderUG.rpt");
            this.ShowTranscript("Transcript Report", "rptGradeCardNewwithoutheader.rpt");
        }

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnTranscriptWithFormat_Click(object sender, EventArgs e)  // Added for Tejas Thakre 11_09_2023 for Common Code
    {
        int SCHEMENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SCHEMENO", "IDNO=" + ViewState["idno"]));
        String GRADEMARKS = Convert.ToString(objCommon.LookUp("ACD_SCHEME", "GRADEMARKS", "SCHEMENO=" + SCHEMENO));
        #region CPUKOTA Added on 30102023
        if (Convert.ToInt32(Session["OrgId"]) == 3)
        {
            if (GRADEMARKS == "M") //"M" Means Marks Pattern and  "G" Means Grade Pattern
            {
                ShowTranscriptReportMarks("AllTranscript", "rpt_Transcript_Report_MarkCPUK.rpt");
            }
            else
            {
                ShowTranscriptReport("AllTranscript", "rpt_Transcript_Report_CPUK.rpt");
            }

        }
        #endregion
        else
        {
            ShowTranscriptReport("AllTranscript", "rpt_Transcript_Report.rpt");
        }
    }

    private void ShowTranscriptReportMarks(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +  ",@P_IDNO=" + ViewState["idno"].ToString()+",@P_SEMESTERNO="+0+"";




            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + ViewState["idno"]));
            url += "&param=@P_IDNO=" + ViewState["idno"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowTranscriptReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +  ",@P_IDNO=" + ViewState["idno"].ToString()+",@P_SEMESTERNO="+0+"";

           

            
             int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + ViewState["idno"]));
             url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + ViewState["idno"].ToString();
           
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}

