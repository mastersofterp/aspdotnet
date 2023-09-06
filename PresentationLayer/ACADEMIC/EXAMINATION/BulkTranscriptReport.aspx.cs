using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_EXAMINATION_BulkTranscriptReport : System.Web.UI.Page
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
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");
            }
            

        }
    else
     divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkTranscriptReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkTranscriptReport.aspx");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
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

    private void GenerateQrCode(int Idno)
    {
        //foreach (ListViewDataItem item in lvStudentRecords.Items)
        //{
        //    string Serial_Number = string.Empty;
        //    CheckBox chk = item.FindControl("chkReport") as CheckBox;
        //    int Idno = Convert.ToInt32(chk.ToolTip);
        //    if (chk.Checked)
        //    {
                if (Idno > 0)
                {
                    string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO='" + Idno + "' AND ISNULL(ADMCAN,0)=0");

                    DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON S.DEGREENO=D.DEGREENO INNER JOIN ACD_BRANCH B ON S.BRANCHNO=B.BRANCHNO", "DEGREENAME,LONGNAME BRANCH,*", "", "REGNO='" + RegNo + "'", "REGNO");

                    //string BranchName = objCommon.LookUp("ACD_BRANCH","SHORTNAME","BRANCHNO="+ ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Trim()+"");
                    //SELECT @V_DURATION = DURATION FROM ACD_COLLEGE_DEGREE_BRANCH WHERE COLLEGE_ID = @V_COLLEGE_ID AND DEGREENO = @V_DEGREENO AND BRANCHNO = @V_BRANCHNO
                    int Duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO=" + ds.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND BRANCHNO=" + ds.Tables[0].Rows[0]["BRANCHNO"].ToString()));
                    int finalSemester = Duration * 2;
                    DataSet ds1 = objQrC.GetStudentResultData(Idno);
                    //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";

                    string Qrtext = "Student Name:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Enrollment No.:" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; Degree:" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString().Trim() + "; Branch:" + ds.Tables[0].Rows[0]["BRANCH"].ToString().Trim() + "; CGPA:" +
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
                    long ret = objQrC.AddUpdateQrCode(Idno, QR_IMAGE);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlExam, " Details not found Please check Admission Status.", this);
                }
            
    }

    private void GenerateTranscriptNumber(int idno)
    {

        objQrC.GenerationofTranscriptnumber(idno, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
                
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //Bind the Student List....
            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BulkTranscriptReport.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds;
            // Get List of Student.....
            ds = objSC.GetStudentListForBulkTanscriptReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                lvStudentRecords.Visible = true;
                btnTranscriptReport.Visible = true;
               //hftot.Value = lvStudentRecords.Items.Count.ToString();
            }

            else
            {
                objCommon.DisplayMessage(updpnlExam, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
                btnTranscriptReport.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BulkTranscriptReport.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowTranscript(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param;
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_RESULT=" + "pass" + ",@P_SPEC=" + "BE" + ",@P_SEMESTERNO=" + 0 + ",@DateofIssue=" + "";
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
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BulkTranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        // ShowAllResult("AllResult", "rpt_StudentAllResultUGGreater2013-2014.rpt");
        ShowAllResult(GetStudentIDs(),"AllResult", "rpt_StudentAllResult.rpt");

    }

    private void ShowAllResult(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +  ",@P_IDNO=" + ViewState["idno"].ToString()+",@P_SEMESTERNO="+0+"";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param;

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
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BulkTranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnTranscriptReport_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
        {

            CheckBox chk = dataitem.FindControl("chkReport") as CheckBox;
            if (chk.Checked == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(updpnlExam, "Please select atleast one Student.!!", this.Page);
            return;
        }
        foreach (ListViewDataItem item in lvStudentRecords.Items)
        {
            string Serial_Number = string.Empty;
            CheckBox chk = item.FindControl("chkReport") as CheckBox;
            int Idno = Convert.ToInt32(chk.ToolTip);
            if (chk.Checked)
            {
                if (Idno > 0)
                {
                    GenerateQrCode(Idno);
                    GenerateTranscriptNumber(Idno);
                    //this.ShowTranscript(Idno,"Transcript Report", "rptTranscriptNew.rpt");
                }
            }
        }

        this.ShowTranscript(GetStudentIDs(), "Bulk Transcript Report", "rptTranscriptBulk.rpt");
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += "$";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BulkTranscriptReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}