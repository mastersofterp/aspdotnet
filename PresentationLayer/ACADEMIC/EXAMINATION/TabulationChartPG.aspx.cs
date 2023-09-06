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
using IITMS.SQLServer.SQLDAL;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ACADEMIC_EXAMINATION_TabulationChartPG : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //TO SET THE MASTERPAGE
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CHECK SESSION
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();

                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropdownList();
            }
        }

    }
    #endregion

    #region Other Events
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                ClearAllDropDowns();
                ClearPanel();
                ddlDegree.SelectedIndex = 0;
                //ddlBranch.Items.Clear();
                //ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAllDropDowns();
            ClearPanel();
            ddlBranch.Items.Clear();
            // ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                //ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ClearPanel();
            ClearAllDropDowns();
            if (ddlBranch.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedValue == "99")
                //    objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", " SCHEMETYPE = 1 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "schemename");
                //else
                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                //ddlScheme.Focus();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT_HIST SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlStuType.Items.Clear();
            ddlStuType.Items.Add(new ListItem("Regular", "0"));
            ddlStuType.Items.Add(new ListItem("EX", "1"));
            ShowDate();
            
            Bindlist();

            //if (ddlBranch.SelectedValue == "99")
            //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SC.SECTIONNO > 0 AND SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
            //else
            //      objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SC.SECTIONNO > 0 AND SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Click Events
    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    SummaryResultSheet("SummaryResultSheet", "rptSummaryResultSheet.rpt"); 
    //}
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    #region User Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
        }
    }

    private void ShowDate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_TRRESULT TR INNER JOIN ACD_STUDENT S ON(TR.IDNO = S.IDNO)", "RESULT_DECLARED_DATE", " ", "TR.SESSIONNO=" + ddlSession.SelectedValue + " and S.COLLEGE_ID=" + ddlCollege.SelectedValue + " and S.DEGREENO =" + ddlDegree.SelectedValue + "and S.BRANCHNO=" + ddlBranch.SelectedValue + " and TR.SEMESTERNO=" + ddlSemester.SelectedValue + " and S.ADMBATCH=" + ddlAdmbatch.SelectedValue + "", "");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDeclareDate.Text = ds.Tables[0].Rows[0]["RESULT_DECLARED_DATE"].ToString();
            }
            else
            {
                txtDeclareDate.Text = string.Empty;
            }
            if (txtDeclareDate.Text != string.Empty)
            {
                lblmsg.Text = "For this crieteria result already declared on mentioned date";
            }
        }
    }

    private void FillDropdownList()
    {
        //Fill Dropdown session 
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO desc"); //--AND FLOCK = 1
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
    }

    private void ClearAllDropDowns()
    {
        //ddlScheme.Items.Clear();
        //ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
    }
    private void ClearPanel()
    {
        //pnlStudents.Visible = false;
        //lvCourse.Visible = false;
        //divMsg.InnerHtml = string.Empty;
        //btnSave.Visible = false;
    }

    private void ShowReport(string reportTitle, string rptFileName, string idno)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString() + ",@P_DEGREE=" + ddlDegree.SelectedItem.Text;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

    }

    private void Bindlist()
    {
        DataSet ds = null;
        if (ddlSession.SelectedIndex > 0 && ddlSemester.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
        {
            //Regular

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R INNER JOIN ACD_STUDENT T ON (T.IDNO = R.IDNO)  INNER JOIN ACD_SCHEME S ON (S.SCHEMENO = R.SCHEMENO) ", "DISTINCT R.IDNO", "DBO.FN_DESC('NAME',R.IDNO)STUDNAME,R.REGNO", "R.IDNO <> 0 AND R.SESSIONNO =" + ddlSession.SelectedValue + " AND R.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue + " AND R.PREV_STATUS=" + ddlStuType.SelectedValue + " and (T.CAN=0 OR T.CAN IS NULL)  AND R.EXAM_REGISTERED=1 and (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + ddlCollege.SelectedValue + " or T.COLLEGE_ID= 0 ) AND T.ADMBATCH=" + ddlAdmbatch.SelectedValue + "", "R.REGNO");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    pnlStudent.Visible = true;
                }
                else
                    if (ddlCollege.SelectedIndex == 0)
                    {
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        pnlStudent.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnlExam, "No Record Found!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        pnlStudent.Visible = false;
                    }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Error!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlStudent.Visible = false;
            }
        }
    }
    private string GetIDNO()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            if (chk.Checked)
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "$" + lblStudname.ToolTip.ToString();
            }
        }
        if (retIDNO.Equals("")) return "0";
        else return retIDNO;
        //return retIDNO;
    }
    #endregion

    protected void btnGrade_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = GetIDNO();
            if (idno == "")
            {
                objCommon.DisplayMessage(updpnlExam, "Please Select At least one Student!!", this.Page);
                return;
            }
            else
            {
                int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                //          B.tech                                  M.Tech.(App. Geology)           B. Arch.                        MCA                                 M. tech
                if ((degreeno == 1 && semesterno == 8) || (degreeno == 2 && semesterno == 4) || (degreeno == 3 && semesterno == 10) || (degreeno == 4 && semesterno == 6) || (degreeno == 5 && semesterno == 4))
                    ShowReport("Grade_Card_Report", "rptTabulationRegistarMrk.rpt", idno == "" ? "0" : idno);
                else
                    ShowReport("Grade_Card_Report_Semester", "rptGradeCard_Semester.rpt", idno == "" ? "0" : idno);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.btnGrade_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport_GradeCard(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowGradeCardFinal(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + "";
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnGradeCard_Click1(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
            if (chk.Checked)
            {
                GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
            }
        }
        if (ddlSemester.SelectedValue == "6")
        {
            this.ShowGradeCardFinal("Grade_Card_Final", "rptGradeCardReportFinalPG.rpt");
        }
        else
        {
            this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG.rpt");
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

    private void GenerateQrCode(string idno, string regno, string studname)
    {
        string declaredDate = string.Empty;
        string dateOfIssue = string.Empty;
        declaredDate = txtDeclareDate.Text;
        dateOfIssue = txtDateOfIssue.Text;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "*", "", "REGNO='" + regno + "'", "REGNO");

        DataSet ds1 = objQrC.GetStudentDataForGradeCardPG(Convert.ToInt16(ddlAdmbatch.SelectedValue), Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlCollege.SelectedValue), Convert.ToInt16(ddlDegree.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), declaredDate, dateOfIssue, Convert.ToInt16(idno));
        //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";
        string Qrtext = "RollNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() +
                        "; StudName: " + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() +
                        "; Year of Exam : " + ds1.Tables[0].Rows[0]["MONTH"].ToString().Trim() + ", " + ds1.Tables[0].Rows[0]["EXAMYEAR"].ToString().Trim() +
                        "; Result : " + ds1.Tables[0].Rows[0]["PASSFAIL"].ToString().Trim() + 
                        "; SGPA : " + ds1.Tables[0].Rows[0]["SGPA"] + "";
            
        //"RollNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + 
        //"; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + 
        //"; RegNo=" + ds1.Tables[0].Rows[0]["ENROLLNO"].ToString().Trim() + 
        //"; Result=" + ds1.Tables[0].Rows[0]["PASSFAIL"].ToString().Trim() + 
        //"; SGPA=" + ds1.Tables[0].Rows[0]["SGPA"].ToString().Trim() + "";

        Session["qr"] = Qrtext.ToString();

        QRCodeEncoder encoder = new QRCodeEncoder();
        encoder.QRCodeVersion = 10;
        Bitmap img = encoder.Encode(Session["qr"].ToString());
        //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
        img.Save(Server.MapPath("~\\img.Jpeg"));
        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

        //img.Save(Server.MapPath("~\\img.Jpeg"));
        byte[] QR_IMAGE = ViewState["File"] as byte[];
        long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds.Tables[0].Rows[0]["IDNO"].ToString().Trim()), QR_IMAGE);
    }

    private void ShowGradeCard(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + "";
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowGradeCardWithoutHeader(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + " ,@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_IDNO=" + GetIDNO() + "";
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btntabulation_Click(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedValue == "6")
            ShowTR("ResultSheet", "rptTabulationBtech.rpt");
        else
            ShowTR("ResultSheet", "rptTabulationPG.rpt");
    }

    private void ShowTR(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnGradeCardHeader_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
            if (chk.Checked)
            {

                GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
            }
        }
        this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportWithoutHeaderPG.rpt");
    }

    private void ShowStudentfailedList(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlStuType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearPanel();
        Bindlist();
    }

    private void ShowGradeCardBack(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "D.DEGREENO");
            ddlDegree.Focus();
        }
    }

    private void ShowPassedStudents(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowStudentResultList(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_STUDTYPE=" + ddlStuType.SelectedValue;
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Export(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + "Student Result List";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_STUDTYPE=" + ddlStuType.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnPassedStud_Click(object sender, EventArgs e)
    {
        this.ShowPassedStudents("Passed Students", "PassedStudentsReport.rpt");
    }

    protected void btnNotificationPdf_Click(object sender, EventArgs e)
    {
        this.ShowStudentResultList("Student Result List", "rptStudentResultList.rpt");
    }

    protected void btnNotificationWord_Click(object sender, EventArgs e)
    {
        this.Export("doc", "rptStudentResultList.rpt");
    }
}