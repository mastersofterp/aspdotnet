using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;   
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_BulkScholershipUpdate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    QrCodeController objQrC = new QrCodeController();
    int prev_status;

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
        try
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

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //ShowDetails();
                }

                if (rdoSingleStudent.Checked)
                {
                    pnlSingleStud.Visible = true;
                    objCommon.FillDropDownList(ddlSessionSingleStud, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                    // ddlSessionSingleStud
                }
                else
                {
                    pnlBulkStud.Visible = true;

                }


                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6") // For RCPIT and RCPIPER)
                {
                    ddlScholorshipType.SelectedValue = "1";
                    ddlScholarShipsType.SelectedValue = "1";
                    divschmode.Visible = false;
                    divSchlWise.Visible = false;
                    divSemester.Visible = false;
                    divddlSchlWiseBulk.Visible = false;
                    divddlSort.Visible = false;
                }
                else
                {
                    divschmode.Visible = true;
                    ddlSchMode.SelectedValue = "2";
                    divSchlWise.Visible = true;
                    divSemester.Visible = true;
                    divddlSchlWiseBulk.Visible = true;                            
                    lblYearMandatory.Visible = true;
                    divddlSort.Visible = true;
                }

            }

            // lblSession.Text = Convert.ToString(Session["sessionname"]);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkScholershipUpdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkScholershipUpdate.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN SCHEME TYPEbtnShow_Click
            //objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            // FILL DROPDOWN BATCH
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlScholorshipType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0 AND ACTIVESTATUS=1", "SCHOLORSHIPTYPENO");
            //objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0", "college_id");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //  objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 ", "SEMESTERNO");
            objCommon.FillDropDownList(ddlYear,"ACD_YEAR","YEAR","YEARNAME","YEAR>0","YEAR");
            objCommon.FillDropDownList(ddlScholarShipsType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0  AND ACTIVESTATUS=1 ", "SCHOLORSHIPTYPENO");  // added on 2020 feb 11
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
            // FILL DROPDOWN ADMISSION BATCH
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        try
        {
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
            {
                this.BindListView();
            }
            else
            {
                if (ddlYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Year", this.Page);
                    return;
                }
                else
                {
                    this.BindListView();
                }
            }
                          
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds;
            int Sort = 0 ;
            int sessionno = Convert.ToInt32(Session["currentsession"]);

            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }
            int ScholarshipMode = Convert.ToInt32(ddlSchMode.SelectedValue);
            


            int AmtPercent = txtschAmt.Text == string.Empty ? 0 : Convert.ToInt32(txtschAmt.Text);

            Sort = Convert.ToInt32(ddlSort.SelectedValue);

            //ds = studCont.GetStudentListForAdmitCardNoDues(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            //ds = studCont.GetStudentScholershipDetails(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            if (ddlSchlWiseBulk.SelectedValue == "1")
            {
                ds = studCont.GetStudentScholershipDetailsSemWise(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlScholarShipsType.SelectedValue), ScholarshipMode, AmtPercent,Sort);
            }
            else
            {
                ds = studCont.GetStudentScholershipDetails(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlScholarShipsType.SelectedValue), ScholarshipMode, AmtPercent, Sort);
            }


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divNote.Visible = true;
                divSchltype.Visible = true;
                //ddlScholarShipsType.SelectedIndex = 2;
                Panel1.Visible = true;
                lvStudentRecords.Visible = true;
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
                hftot.Value = lvStudentRecords.Items.Count.ToString();

                foreach (ListViewDataItem itm in lvStudentRecords.Items)
                {
                    CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                    TextBox txtSchAmt = itm.FindControl("txtSemesterAmount") as TextBox;
                    HiddenField hdidno = itm.FindControl("hidIdNo") as HiddenField;
                    HiddenField hdfBranchno = itm.FindControl("hdfBranchno") as HiddenField;
                    Label lblDegreeno = itm.FindControl("lblschamt") as Label;
                   
                    //Label lblDDLData = (Label)itm.FindControl("lblDDLData");
                    //DropDownList ddl = (DropDownList)itm.FindControl("ddlScholarShipsType");
                    
                    //objCommon.FillDropDownList(ddlScholorshipType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0", "SCHOLORSHIPTYPENO");  // added on 2020 feb 11
                   
                    //ddlScholorshipType.SelectedIndex =2;
                    //ddl.SelectedValue = lblDDLData.Text;
                   // ddl.SelectedIndex = 2;


                    //Above Commented as per dissussion with Manoj Shanti Sir 

                    //string count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + hdidno.Value + "  AND BRANCHNO= " + hdfBranchno.Value + "  AND DEGREENO=" + lblDegreeno.ToolTip + "AND SEMESTERNO=" + lblDegreeno.Text + " AND  PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0");
                    //if (count != "0")
                    //{
                    //    txtSchAmt.Enabled = false;
                    //    ddl.Enabled = false;
                    //    chk.Checked = true;
                    //    chk.Enabled = false;
                    //}
                    //else
                    //{
                    //    txtSchAmt.Enabled = true;
                    //    ddl.Enabled = true;
                    //    chk.Checked = false;
                    //    chk.Enabled = true;
                    //}
                    

                    string count = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "COUNT(1)", "IDNO=" + hdidno.Value + "  AND BRANCHNO= " + hdfBranchno.Value + "  AND DEGREENO=" + lblDegreeno.ToolTip + "AND  YEAR=" + lblDegreeno.Text + " AND  SCHOLARSHIP_ID=" + Convert.ToInt32(ddlScholarShipsType.SelectedValue));

                    if (count != "0")
                    {
                        txtSchAmt.Enabled = false;
                        //ddl.Enabled = false;
                        chk.Checked = true;
                        chk.Enabled = false;
                    }
                    else
                    {
                        if (Session["OrgId"].ToString() =="1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                        {
                            divamount.Visible = false;
                        }
                        else
                        {
                            if (ddlSchMode.SelectedValue == "2")
                            {
                                divamount.Visible = true;
                                txtSchAmt.Text = txtAmountsch.Text;
                            }
                            else if (ddlSchMode.SelectedValue == "2")
                            {
                                divamount.Visible = false;
                            }
                        }
                        txtSchAmt.Enabled = true;
                        //ddl.Enabled = true;
                        chk.Checked = false;
                        chk.Enabled = true;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updtime, " No Demand Found!!", this.Page);
              
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
                divNote.Visible = false;
                
                //ddlScholarShipsType.SelectedIndex = 0;
                //divSchltype.Visible = false;
               

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            Session["listIdCard"] = null;
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + ddlAdmBatch.SelectedValue + ",@P_PREV_STATUS=" + prev_status + ",@P_USER_FUll_NAME=" + Session["userfullname"];
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_PREV_STATUS=" + prev_status + ",@P_DATEOFISSUE=" + txtDateofissue.Text.ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
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
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            // ddlAdmbatch.SelectedIndex = 0;
            // ddlAdmbatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
       
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {

            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 ", "SEMESTERNO");
            ddlSemester.Focus();

        }
        else
        {
            //ddlBranch.Items.Clear();
            //ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
       
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

    //This Method Generate QR-CODE & also  save image in ACD_STUD_PHOTO Table & QR-Code Files Folder.

    private void GenerateQrCode(string idno, string regno)
    {

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "*", "", "REGNO='" + regno + "'", "REGNO");
        if (rbRegEx.SelectedIndex == 0)
        {
            prev_status = 0;
        }
        else
        {
            prev_status = 1;
        }
        // int sessionno = Convert.ToInt32(Session["currentsession"]);
        // string dateOfIssue = txtDateofissue.Text.ToString();
        // DataSet ds1 = objQrC.GetDetailsForAdmitCard(sessionno, Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), prev_status, Convert.ToInt16(idno), dateOfIssue);
        DataSet ds1 = objQrC.GetDetailsForAdmitCard(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), prev_status, Convert.ToInt16(idno));

        //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";
        string Qrtext = "RollNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";Degree=" +
                                 ds1.Tables[0].Rows[0]["DEGREENAME"].ToString().Trim() + ";Semester=" +
                                ds1.Tables[0].Rows[0]["SEMESTER"].ToString().Trim() + "";



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

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                string studentIds = string.Empty;
                foreach (ListViewDataItem lvItem in lvStudentRecords.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkReport") as CheckBox;
                    if (chkBox.Checked == true)
                    {
                        studentIds += chkBox.ToolTip + ",";
                        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip) + ""));
                        //  GenerateQrCode((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip), RegNo);
                    }
                }

                int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), studentIds, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]));
                //ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");  
                ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");
                //ShowReport(ids, "Student_Admit_Card_Report", "DemoReport.rpt"); 

            }
            else
            {
                objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
        
    }
    //sending from mail aayushi gupta

    protected void btnSendEmail_Click1(object sender, EventArgs e)
    {
        {
            try
            {
                DataSet d = new DataSet();
                string studentIds = string.Empty;
                ReportDocument customReport = new ReportDocument();
                //DataSet ds = objCommon.FillDropDown("Reff", "EMAILSVCID", "EMAILSVCPWD",string.Empty,string.Empty);
                foreach (ListViewDataItem item in lvStudentRecords.Items)
                {
                    CheckBox chk = item.FindControl("chkReport") as CheckBox;
                    HiddenField hdfuserno = item.FindControl("hidIdNo") as HiddenField;
                    HiddenField hdfAppli = item.FindControl("hdfAppliid") as HiddenField;
                    HiddenField Hdfemail = item.FindControl("Hdfemail") as HiddenField;

                    if (chk.Checked == true && chk.Enabled == true)
                    {
                        studentIds += hdfuserno.Value + "$";

                        string reportPath = Server.MapPath(@"~,Reports,Academic,rptBulkExamRegslip.rpt".Replace(",", "\\"));
                        customReport.Load(reportPath);

                        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                        ConnectionInfo crConnectionInfo = new ConnectionInfo();
                        Tables CrTables;

                        crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
                        crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                        crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                        crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                        CrTables = customReport.Database.Tables;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                        {
                            crtableLogoninfo = CrTable.LogOnInfo;
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                            CrTable.ApplyLogOnInfo(crtableLogoninfo);
                        }

                        if (rbRegEx.SelectedIndex == 0)
                        {
                            prev_status = 0;
                        }
                        else
                        {
                            prev_status = 1;
                        }

                        //Parameter to Report Document
                        //================================
                        //Extract Parameters From queryString
                        customReport.SetParameterValue("@P_IDNO", hdfuserno.Value);
                        customReport.SetParameterValue("@P_SESSIONNO", Convert.ToInt32(ddlAdmBatch.SelectedValue));
                        customReport.SetParameterValue("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedValue));
                        customReport.SetParameterValue("@P_BRANCHNO", Convert.ToInt32(ddlBranch.SelectedValue));
                        customReport.SetParameterValue("@P_SEMESTERNO", Convert.ToInt32(ddlSemester.SelectedValue));
                        customReport.SetParameterValue("@P_PREV_STATUS", Convert.ToInt32(prev_status));
                        customReport.SetParameterValue("@P_USER_FUll_NAME", Session["userfullname"]);
                        customReport.SetParameterValue("@P_COLLEGE_CODE", 0);

                        string path = Server.MapPath("~/AdmitCardMail\\");
                        if (!(Directory.Exists(path)))
                            Directory.CreateDirectory(path);
                        customReport.ExportToDisk(ExportFormatType.PortableDocFormat, path + hdfAppli.Value + ".pdf");


                        DataSet ds = objCommon.FillDropDown("Reff", "EMAILSVCID", "EMAILSVCPWD", string.Empty, string.Empty);
                        var fromAddress = ds.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                        //const string fromPassword = "MUadmission2016";
                        string fromPassword = ds.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                        if (Hdfemail.Value == "")
                        {
                            objCommon.DisplayMessage("Kindly check Email Id .", this.Page);
                        }
                        else
                        {
                            string EmailTemplate = "<html><body>" +
                                                "<div align=\"center\">" +
                                                "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                                 "<tr>" +
                                                 "<td>" + "</tr>" +
                                                 "<tr>" +
                                                "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><B>Regards,<br/><br/><b>Controller Of Examination <br/>Indus University</td>" +
                                                "</tr>" +
                                                "</table>" +
                                                "</div>" +
                                                "</body></html>";
                            StringBuilder mailBody = new StringBuilder();
                            mailBody.AppendFormat("<h1>Greetings !!</h1>");
                            mailBody.AppendFormat("Dear " + objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_IDNO=" + hdfuserno.Value));
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            //mailBody.AppendFormat("<p>Your Admit Card is Generated.</p>");
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                            string sessionnoname = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue));
                            mailBody.AppendFormat("<p>Please find the following attachment for the Hall Ticket of END-SEM Examination <b>" + sessionnoname + "</b> .</p>");
                            mailBody.AppendFormat("<br />");
                            string Mailbody = mailBody.ToString();
                            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                            //msg.From = new MailAddress(HttpUtility.HtmlEncode(sendersemailid));
                            msg.From = new MailAddress(HttpUtility.HtmlEncode(fromAddress));
                            msg.To.Add(Hdfemail.Value);
                            msg.Body = nMailbody;
                            msg.Attachments.Add(new Attachment(path + hdfAppli.Value + ".pdf"));
                            msg.IsBodyHtml = true;
                            msg.Subject = "Hall Ticket For " + sessionnoname;
                            SmtpClient smt = new SmtpClient();
                            smt.Host = "smtp.gmail.com";
                            smt.Port = 587;
                            smt.UseDefaultCredentials = true;
                            smt.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);

                            smt.EnableSsl = true;
                            // smt.Send(msg);
                            //SmtpClient smt = new SmtpClient("smtp.gmail.com");
                            //smt.Port = 587;
                            //smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromAddress), HttpUtility.HtmlEncode(fromPassword));
                            //smt.EnableSsl = true;
                            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                            smt.Send(msg);
                            objCommon.DisplayMessage(Page, "Mail Sent Successfully For Selected Student(s)!!", this);
                            //  objCommon.DisplayMessage("Mail Sent Successfully !!", this.Page);
                            //string script = "<script>alert('Mail Sent Successfully')</script>";
                            //ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
                            msg.Attachments.Dispose();
                            // BindListView();
                        }
                        if (File.Exists(path + hdfAppli.Value + ".pdf"))
                        {
                            File.Delete(path + hdfAppli.Value + ".pdf");
                        }

                    }

                }
                if (studentIds.Equals(""))
                {
                    objCommon.DisplayMessage("Please Select at least one Student!", this.Page);

                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        int ScholarshipMode = 0;
        int Percentage = 0;

        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            //objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            //objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();
            objS.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            //objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.StudType = Convert.ToInt32(rbRegEx.SelectedValue.ToString());
            objS.ScholershipTypeNo = Convert.ToInt32(ddlScholarShipsType.SelectedValue);

            if (objS.ScholershipTypeNo == 0)
            {
                objCommon.DisplayMessage(updtime, "Please Select Scholarship Type", this.Page);
                return;
            }

            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                if (chk.Checked == true && chk.Enabled == true)
                {
                    TextBox txtSemesterAmount = itm.FindControl("txtSemesterAmount") as TextBox;
                   // DropDownList ddlScholarShipsType = itm.FindControl("ddlScholarShipsType") as DropDownList;
                    if (txtSemesterAmount.Text == string.Empty) //|| ddlScholarShipsType.SelectedValue == "0")  
                    {
                        //objCommon.DisplayMessage(updtime, "Please Enter Scholarship Amount/Scholarship Type", this.Page);
                        objCommon.DisplayMessage(updtime, "Please Enter Scholarship Amount", this.Page);
                        //ddlScholarShipsType.Focus();
                        txtSemesterAmount.Focus();
                        return;
                    }
                }
            }

            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                if (chk.Checked == true && chk.Enabled == true)
                {
                    TextBox txtSemesterAmount = itm.FindControl("txtSemesterAmount") as TextBox;
                    //DropDownList ddlScholarShipsType = itm.FindControl("ddlScholarShipsType") as DropDownList; //Commented as per dissussion with Manoj Shanti Sir 
                    HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;
                    objS.IdNo = Convert.ToInt32(hdnf.Value);
                    objS.Amount = txtSemesterAmount.Text;
                    //objS.ScholershipTypeNo = Convert.ToInt32(ddlScholarShipsType.SelectedValue);    //Commented as per dissussion with Manoj Shanti Sir 
                    HiddenField hdnfdegreeno = itm.FindControl("hdfdegreeno") as HiddenField;
                    HiddenField hdnfbranchno = itm.FindControl("hdfBranchno") as HiddenField;
                    //HiddenField hdnfadmnbatch = itm.FindControl("hdfadmbatch") as HiddenField;

                    HiddenField hdfAcademicYearId = itm.FindControl("hdfAcademicYearId") as HiddenField;
                    HiddenField hdfAdmissionBatch = itm.FindControl("hfdAdmbatch") as HiddenField;
                    Label lblschamt =itm.FindControl("lblschamt") as Label;

                    objS.DegreeNo = Convert.ToInt32(hdnfdegreeno.Value);
                    objS.BranchNo = Convert.ToInt32(hdnfbranchno.Value);

                    objS.AdmBatch = Convert.ToInt32(hdfAdmissionBatch.Value);
                    objS.Year = Convert.ToInt32(lblschamt.Text);

                    int yearID = Convert.ToInt32(hdfAcademicYearId.Value);

                    if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                    {
                        ScholarshipMode = 0;
                        Percentage = 0;
                    }
                    else
                    { 
                        ScholarshipMode = Convert.ToInt32(ddlSchMode.SelectedValue);

                        if (ScholarshipMode == 1)
                        {
                            Percentage = Convert.ToInt32(txtschAmt.Text);
                        }
                        else
                        {
                            Percentage = 0;
                        }
                    }

                    
                    
                    //if (ddlScholarShipsType.SelectedIndex > 0 && txtSemesterAmount.Text != string.Empty)

                    if ( txtSemesterAmount.Text != string.Empty)
                    {
                        if(ddlSchlWiseBulk.SelectedValue == "1")
                        {
                            output = studCont.UpdateBulkStudentScholarshipDetailsSemWise(objS, yearID, ScholarshipMode, Percentage);
                            count++;
                        }
                        else
                        {
                            output = studCont.UpdateBulkStudentScholarshipDetails(objS, yearID, ScholarshipMode, Percentage);
                            count++;
                        }
                        
                    }
                    else
                    {
                        //if (ddlScholarShipsType.SelectedIndex == 0)
                        //{
                        //    objCommon.DisplayMessage(updtime, "Please select Scholarship Type", this.Page);
                        //}
                        //else if (txtSemesterAmount.Text == string.Empty)
                        //{
                        //    objCommon.DisplayMessage(updtime, "Please Enter Scholarship Amount", this.Page);
                        //}
                    }

                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (count > 0)
            {
                objCommon.DisplayMessage(updtime, "Record Updated Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvStudentRecords_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                HiddenField hdfn = e.Item.FindControl("hidIdNo") as HiddenField;
                CheckBox chkdis = e.Item.FindControl("chkReport") as CheckBox;
                //Label lblDDLData = e.Item.FindControl("lblDDLData") as Label;


                //New Code  2020 feb 11

                //Commented as per dissussion with Manoj Shanti Sir 

                //DropDownList ddlScholarShipsType = e.Item.FindControl("ddlScholarShipsType") as DropDownList;


                //if (ViewState["SCHOLORSHIPTYPE"] != null)
                //{
                //    ddlScholarShipsType.DataSource = (DataTable)ViewState["SCHOLORSHIPTYPE"];
                //    ddlScholarShipsType.DataTextField = "SCHOLORSHIPNAME";
                //    ddlScholarShipsType.DataValueField = "SCHOLORSHIPTYPENO";
                //    ddlScholarShipsType.DataBind();
                //}

                //ddlScholarShipsType.SelectedValue = lblDDLData.Text;


                int idno = Convert.ToInt32(hdfn.Value);

                string exist1 = objCommon.LookUp("ACD_NODUES_STATUS", "COUNT(1)", "IDNO='" + idno + "'");

                if (Convert.ToInt32(exist1) > 0)
                {
                    if (dr["NODUES_STATUS"].Equals(1))
                    {
                        ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                        chkdis.InputAttributes.Add("disabled", "true");
                        ((RadioButton)e.Item.FindControl("rdoYes")).Enabled = true;
                        ((RadioButton)e.Item.FindControl("rdoYes")).Checked = true;
                        ((RadioButton)e.Item.FindControl("rdoNo")).Enabled = true;
                    }
                    else if (dr["NODUES_STATUS"].Equals(0))
                    {
                        ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                        chkdis.InputAttributes.Add("disabled", "true");
                        ((RadioButton)e.Item.FindControl("rdoYes")).Enabled = true;
                        ((RadioButton)e.Item.FindControl("rdoNo")).Enabled = true;
                        ((RadioButton)e.Item.FindControl("rdoNo")).Checked = true;
                    }
                    else
                    {
                        //((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                        //((RadioButton)e.Item.FindControl("rdoYes")).Enabled = false;
                        //((RadioButton)e.Item.FindControl("rdoNo")).Enabled = false;                   
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        ddlScholarShipsType.SelectedIndex = 0;
        divSchltype.Visible = false;
        
    }

    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
       
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
    }
    protected void rbRegEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        ddlScholarShipsType.SelectedIndex = 0;
        divSchltype.Visible = false;                                               
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void rdoSingleStudent_CheckedChanged(object sender, EventArgs e)
    {
        pnlSingleStud.Visible = true;
        Panel1.Visible = false;
        pnlBulkStud.Visible = false;
        ClearForBulkStudent();
        divNote.Visible = false;
        ddlScholarShipsType.SelectedIndex = 0;
        divSchltype.Visible = false;
         

    }
    protected void rdoBulkStudent_CheckedChanged(object sender, EventArgs e)
    {
        pnlSingleStud.Visible = false;
        pnlBulkStud.Visible = true;
        Panel1.Visible = true;
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        divSchltype.Visible = true;
        ClearForSingleStudent();


        //New code 
        DataSet dsData = objCommon.FillDropDown("ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0", "SCHOLORSHIPTYPENO");  // added on 2020 feb 11
        ViewState["SCHOLORSHIPTYPE"] = dsData.Tables[0];

    }


    public void ClearForBulkStudent()
    {
        ddlAcdYear.SelectedIndex=0;
        ddlYear.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;
        Panel1.Visible = false;
        divNote.Visible = false;
        ddlScholarShipsType.SelectedIndex = 0;
        divSchltype.Visible = false;  

    }



    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlScholarShipsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(ddlScholarShipsType.SelectedValue == "0")
        //{
        //    lvStudentRecords.DataSource = null;
        //    lvStudentRecords.DataBind();
        //}
    }

    protected void ddlSchMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            if (ddlSchMode.SelectedValue == "1")
            {
                divAmt.Visible = true;
                divamount.Visible = false;
            }
            else
            {
                divAmt.Visible = false;
                divamount.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkScholershipUpdate.ddlSchMode_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

   
 
  // For Single Student Code

    #region SingleStudent

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        divCourses.Visible = true;
        ddlScholorshipType.SelectedIndex = 0;
        ddlSchlModeSingle.SelectedIndex = 0;
        ddlSchlWise.SelectedIndex = 0;
        divamtSingl.Visible = false;

        //btnShow.Visible = true;
        if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
        {
            divModeSingl.Visible = false;
            divShowPerButton.Visible = false;
        }
        else 
        {
            divModeSingl.Visible = true;             
            ddlSchlModeSingle.SelectedValue = "2";
            divShowPerButton.Visible = false;
        }
        ShowDetails();
    }

    private void ShowDetails()
    {
        int idno = 0;
        int No_Dues_Status = 0;

        StudentController objSC = new StudentController();
        try
        {
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            ViewState["idno"] = idno;
            int sessionno = Convert.ToInt32(ddlSessionSingleStud.SelectedValue);
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16(idno) + "");

            if (Enrollno.Equals(txtEnrollno.Text))
            {
                Panel1.Visible = true;
                if (idno > 0)
                {
                    //DataSet dsStudent = objSC.GetStudentDetails_No_Dues(idno, sessionno);
                    DataSet dsStudent = objSC.GetStudentDetails_For_Scholarship(idno, sessionno);

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
                            lblDegrreno.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                            lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                            lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                            lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            lblSingCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                            lblSingCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            lblYear.Text = dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString();
                            lblYear.ToolTip = dsStudent.Tables[0].Rows[0]["YEAR"].ToString();
                            //GridView GV = new GridView();                                                   
                            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                          
                            BindSemesters(Convert.ToInt16(idno));
                            

                        }
                        else
                        {
                            divCourses.Visible = false;
                            objCommon.DisplayMessage(updtime, "Registration Details not found for this session!", this.Page);
                        }
                    }
                    else
                    {
                        divCourses.Visible = false;
                        objCommon.DisplayMessage(updtime, "Registration Details not found for this session!", this.Page);
                    }
                }
            }
            else
            {
                divCourses.Visible = false;
                objCommon.DisplayMessage(updtime, "No Record Found!!!", this.Page);
                // Panel1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkScholershipUpdate.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  

    public void BindSemesters(int idno)
    {
        int TotalRow = 0;

        int Scholarshipid = Convert.ToInt32(ddlScholorshipType.SelectedValue);
        int ScholashipMode= Convert.ToInt32(ddlSchlModeSingle.SelectedValue);
        int AmtPercent =  txtPerAmountSingStud.Text == string.Empty ? 0 : Convert.ToInt32(txtPerAmountSingStud.Text);
        int SchlWise = ddlSchlWise.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlSchlWise.SelectedValue);
        TotalRow = studCont.GetTotalSemesterCount(idno, SchlWise);
        DataSet dsData = studCont.GetSingleStudentScholarshipData(idno, Scholarshipid, ScholashipMode, AmtPercent);
        // DataSet dsData = objCommon.FillDropDown("ACD_STUDENT A LEFT JOIN ACD_STUDENT_SCHOLERSHIP B ON A.IDNO = B.IDNO", "B.SEMESTERNO", "B.SCHL_AMOUNT,B.SCHOLARSHIP_ID", "A.IDNO = " + idno, "");
        ViewState["StudSemData"] = dsData.Tables[0];
        if (dsData.Tables[0].Rows.Count > 0)
        {
           // ddlScholorshipType.SelectedValue = "0";
            string sem = dsData.Tables[0].Rows[0]["SCHOLARSHIP_ID"].ToString();
            if (!String.IsNullOrEmpty(sem))
            {
                ddlScholorshipType.SelectedValue = sem;
            }
        }
        CreateSemesterGrid(TotalRow);
    }

    public void CreateSemesterGrid(int TotalRow)
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("SemesterNo", typeof(string)));//
        dt.Columns.Add(new DataColumn("AcadYear", typeof(string)));
        dt.Columns.Add(new DataColumn("SemesterAmt", typeof(string)));//
        //


        int cnt = 1;
        while (cnt <= TotalRow)
        {
            dr = dt.NewRow();
            dr["RowNumber"] = "" + cnt;
            dr["SemesterNo"] = cnt;
            dr["AcadYear"] = "";
            dr["SemesterAmt"] = "";

            dt.Rows.Add(dr);
            cnt++;
        }

        gvSemesters.DataSource = dt;
        gvSemesters.DataBind();
    }

    protected void gvSemesters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int Scholarshipid = Convert.ToInt32(ddlScholorshipType.SelectedValue);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblSemestersNo");
                TextBox txt = (TextBox)e.Row.FindControl("txtSemestersAmount");
                DropDownList ddlAcadYear = (DropDownList)e.Row.FindControl("ddlAcadYear");
                DataTable dtdata = (DataTable)ViewState["StudSemData"];
                ImageButton btndelete = (ImageButton)e.Row.FindControl("btnDelete");
                Label lblTxt = (Label)e.Row.FindControl("lblTxt");
                ImageButton btnDeleteAllotment = (ImageButton)e.Row.FindControl("btnDeleteAllotment");
                Label lblTxt2 = (Label)e.Row.FindControl("lblTxt2");
                // ViewState["amount_textbox"] = txt;
                objCommon.FillDropDownList(ddlAcadYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
                
                if (dtdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtdata.Rows)
                    {
                        string amt = dr["SCHL_AMOUNT"].ToString();
                     
                        string semno = string.Empty;
                        string count = string.Empty;
                        string dcrno = string.Empty;
                        string yearsem = string.Empty;
                        string acdYear = string.Empty;
                        string allotCount = string.Empty;
                        string Schlshipno = string.Empty;

                        if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
                        {
                            semno = dr["YEAR"].ToString();
                            count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND YEAR=" + semno + " AND PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0");
                            dcrno = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND YEAR=" + semno + " AND PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0 ");

                            //acdYear = (objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "COUNT(1)", "IDNO=" + ViewState["idno"] + "AND BRANCHNO=" + lblBranch.ToolTip + "AND DEGREENO=" + lblDegrreno.ToolTip + "AND YEAR=" + semno + "AND SCHL_AMOUNT=" + amt));
                            //if (acdYear == "1")
                            //{
                            //    ddlAcadYear.SelectedValue = dr["ACADEMIC_YEAR_ID"].ToString();
                            //}
                        }
                        else
                        {
                            if (ddlSchlWise.SelectedValue == "2")
                            {
                                semno = dr["YEAR"].ToString();
                                yearsem = dr["SEMESTERNO"].ToString();
                                count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + yearsem + " AND SCHOLARSHIP_ID=" + Scholarshipid + " AND PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0");
                                dcrno = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + yearsem + " AND SCHOLARSHIP_ID=" + Scholarshipid + " AND PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0 ");                           
                            }
                            else
                            {
                                semno = dr["SEMESTERNO"].ToString();
                                count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + semno + " AND SCHOLARSHIP_ID=" + Scholarshipid + " AND PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0");
                                dcrno = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + semno + " AND SCHOLARSHIP_ID=" + Scholarshipid + " AND PAY_MODE_CODE='SA' AND RECON=1 AND CAN=0 ");                          
                            }                        
                        }

                        if (ddlSchlWise.SelectedValue == "2")
                        {
                            allotCount = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "COUNT(1)", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + yearsem + " AND SCHOLARSHIP_ID=" + Scholarshipid);
                            Schlshipno = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "SCHLSHPNO", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + yearsem + " AND SCHOLARSHIP_ID=" + Scholarshipid);
                        }
                        else
                        {
                            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
                            {
                                allotCount = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "COUNT(1)", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND YEAR=" + semno + " AND SCHOLARSHIP_ID=" + Scholarshipid);
                                Schlshipno = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "SCHLSHPNO", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND YEAR=" + semno + " AND SCHOLARSHIP_ID=" + Scholarshipid);
                            }
                            else
                            {
                                allotCount = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "COUNT(1)", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + semno + " AND SCHOLARSHIP_ID=" + Scholarshipid);
                                Schlshipno = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "SCHLSHPNO", "IDNO=" + ViewState["idno"] + " AND BRANCHNO=" + lblBranch.ToolTip + " AND DEGREENO=" + lblDegrreno.ToolTip + " AND SEMESTERNO=" + semno + " AND SCHOLARSHIP_ID=" + Scholarshipid);
                            }
                        }
                        


                        //if (count != "0")
                        //{
                        //    if (lbl.Text == semno)
                        //    {
                        //        //txt.Enabled = false;
                        //        btndelete.Visible = true;
                        //        lblTxt.Visible = false;
                        //        btndelete.ToolTip = dcrno;
                        //        btndelete.Enabled = true;
                        //        lblTxt2.Visible = false;
                        //        btnDeleteAllotment.Visible = true;
                        //        btnDeleteAllotment.Enabled = false;
                        //    }
                        //}
                        //else
                        //{
                        //    if (lbl.Text == semno)
                        //    {
                        //        txt.Enabled = true;
                        //    }

                        //}

                        if (lbl.Text == semno)
                        {
                            txt.Text = amt.ToString();
                            ddlAcadYear.SelectedValue = dr["ACADEMIC_YEAR_ID"].ToString();

                            //lblTxt.ToolTip = txt.ToString();
                            //ViewState["semesterno"] = semno;

                        }
                        //else
                        //{
                        //    btndelete.Enabled = false;
                        //}
                        //if (txt.Enabled == true)
                        //{
                        //    //btndelete.Enabled = false;
                        //    btndelete.Visible = false;
                        //    lblTxt.Visible = true;
                        //}

                        if (count != "0")
                        {
                            if (lbl.Text == semno)
                            {

                                lblTxt.Visible = false;
                                lblTxt2.Visible = false;
                                txt.Enabled = false;
                                btndelete.Visible = true;                             
                                btndelete.ToolTip = dcrno;
                                btndelete.Enabled = true;
                               
                                btnDeleteAllotment.Visible = true;
                                btnDeleteAllotment.Enabled = false;
                            }
                            else
                            {
                                txt.Enabled = true;
                                lblTxt2.Visible = true;
                                btnDeleteAllotment.Visible = false;
                            }
                           
                        }
                        else
                        {
                            if(allotCount != "0" && amt != string.Empty)
                            {
                                if (lbl.Text == semno)
                                {
                                    lblTxt2.Visible = false;
                                    btnDeleteAllotment.ToolTip = Schlshipno;
                                    btnDeleteAllotment.Visible = true;
                                }   
                            }
                            else
                            {
                                lblTxt2.Visible = true;
                                btnDeleteAllotment.Visible = false;
                            }

                        }                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkScholershipUpdate.gvSemesters_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitForSingleStu_Click(object sender, EventArgs e)
    {
        if (ddlScholorshipType.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updtime, "Please Select Scholorship Type", this.Page);
            return;
        }

        if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
        {

        }
        else
        {
            if (ddlSchlWise.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updtime, "Please Select Scholorship Category", this.Page);
                return;
            }
        }
       

        int InsertCount = 0;
        int UpdateCount = 0;
        int id = 0;
        int count = 0;

        bool flag = false;
        int SCHLMODE = 0;
        int Percentage = 0;
        try
        {
            objS.DegreeNo = Convert.ToInt32(lblDegrreno.ToolTip);
            objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();
            objS.IdNo = Convert.ToInt32(lblName.ToolTip);
            objS.AdmBatch = Convert.ToInt32(lblAdmBatch.ToolTip);
            objS.ScholershipTypeNo = Convert.ToInt32(ddlScholorshipType.SelectedValue);

            //Adding to table

            //DataTable dtData = new DataTable();
            //DataRow drData = null;
            //dtData.Columns.Add(new DataColumn("SemesterNo", typeof(string)));
            //dtData.Columns.Add(new DataColumn("SemesterAmount", typeof(string)));//           

            //
            int totalRow = gvSemesters.Rows.Count;
            int counter = 0;

            foreach (GridViewRow row in gvSemesters.Rows)
            {
                string output = "";
                Label lblSemestersNo = (Label)row.FindControl("lblSemestersNo");
                TextBox txtSemestersAmount = (TextBox)row.FindControl("txtSemestersAmount");
                DropDownList ddlAcadYear = (DropDownList)row.FindControl("ddlAcadYear");
                objS.SemesterNo = Convert.ToInt32(lblSemestersNo.Text);

                if (txtSemestersAmount.Text != string.Empty && ddlAcadYear.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updtime, "Please Select Academic Year", this.Page);
                    return;
                }

                if (txtSemestersAmount.Text == "")
                {
                    objS.Amount = null;
                }
                else
                {
                    objS.Amount = txtSemestersAmount.Text;
                }


                //drData = dtData.NewRow();
                //drData["SemesterNo"] = lblSemestersNo.Text;
                //drData["SemesterAmount"] = txtSemesters.Text;
                //dtData.Rows.Add(drData);


                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
                {
                    SCHLMODE = 0;
                    Percentage = 0;
                }
                else
                {
                    SCHLMODE = Convert.ToInt32(ddlSchlModeSingle.SelectedValue);

                    if (SCHLMODE == 1)
                    {
                        Percentage = Convert.ToInt32(txtPerAmountSingStud.Text);
                    }
                    else
                    {
                        Percentage = 0;
                    }
                }


                if (objS.Amount != null)
                {
                    if (ddlSchlWise.SelectedValue == "1")
                    {
                        if (txtSemestersAmount.Enabled == true)
                        {
                            output = studCont.InsertStudentScholershipDetailsSemWise(objS, Convert.ToInt32(ddlAcadYear.SelectedValue), SCHLMODE, Percentage);
                        }
                    }
                    else
                    {
                        if (txtSemestersAmount.Enabled == true)
                        {
                            output = studCont.InsertStudentScholershipDetails(objS, Convert.ToInt32(ddlAcadYear.SelectedValue), SCHLMODE, Percentage);
                        }
                    }
                                    
                }
                else if (objS.Amount == null)
                {
                    counter++;
                }
                if (output == "1")
                {
                    InsertCount++;
                }
                else if (output == "2")
                {
                    UpdateCount++;
                }
            }

            //output = studCont.InsertStudentScholershipDetails(objS, dtData);

            if (InsertCount > 0 )
            {
                objCommon.DisplayMessage(updtime, "Records Saved Successfully", this.Page);
                this.BindSemesters(Convert.ToInt16(objS.IdNo));
            }
            else if (UpdateCount > 0)
            {
                objCommon.DisplayMessage(updtime, "Records Updated Successfully", this.Page);
                this.BindSemesters(Convert.ToInt16(objS.IdNo));
            }
            else if (totalRow == counter)
            {
                objCommon.DisplayMessage(updtime, "Please Enter amount", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updtime, "Some Error Occured", this.Page);
            }


            //if (flag.Equals(true))
            //{
            //    output = studCont.AddNoDuesStatusForAdmitCard(objS);
            //    count++;
            //}
            //if (count == 0)
            //{
            //    objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            //}
            //else if (flag.Equals(true))
            //{
            //    objCommon.DisplayMessage(updtime, "Student No-Dues Status Alloted Successfully", this.Page);
            //    txtEnrollno.Text = "";
            //    ddlSessionSingleStud.SelectedIndex = 0;
            //    pnlSingleStud.Visible = true;
            //    pnlBulkStud.Visible = false;
            //    divCourses.Visible = false;
            //    //lvStudentRecords.DataSource = null;
            //    //lvStudentRecords.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkScholershipUpdate.btnSubmitForSingleStu_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ClearForSingleStudent()
    {
        ddlSessionSingleStud.SelectedIndex = 0;
        txtEnrollno.Text = "";
        divCourses.Visible = false;
        pnlSingleStud.Visible = false;
        // Panel1.Visible = false;

    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            objS.DegreeNo = Convert.ToInt32(lblDegrreno.ToolTip);
            objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            objS.IdNo = Convert.ToInt32(lblName.ToolTip);
            //string SemesterNo = objCommon.LookUp("ACD_STUDENT S LEFT JOIN ACD_STUDENT_SCHOLERSHIP SS ON S.IDNO = SS.IDNO AND S.SEMESTERNO = SS.SEMESTERNO", "S.SEMESTERNO", "S.IDNO=" + ViewState["idno"]);
            objS.AdmBatch = Convert.ToInt32(lblAdmBatch.ToolTip);
            objS.ScholershipTypeNo = Convert.ToInt32(ddlScholorshipType.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();

            ImageButton btnDeleteAmount = sender as ImageButton;

            int dcrNo = Convert.ToInt32(btnDeleteAmount.ToolTip);

            string SemesterNo = objCommon.LookUp("ACD_DCR", "SEMESTERNO", "IDNO=" + ViewState["idno"] + " AND DCR_NO=" + dcrNo);
            objS.SemesterNo = Convert.ToInt32(SemesterNo);
            if (dcrNo > 0)
            {
                retStatus = studCont.DeleteScholarshipAdjustment(objS, dcrNo);
                if (retStatus == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updtime, "Scholarship Adjustment Deleted Successfully!", this.Page);
                    BindSemesters(Convert.ToInt16(objS.IdNo));
                }
                else
                {
                    objCommon.DisplayMessage(this.updtime, "Error occurred!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkScholershipUpdate.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlScholorshipType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idno = 0;

        if (ddlScholorshipType.SelectedIndex == 0)
        {
            txtPerAmountSingStud.Text = string.Empty;
            ddlSchlModeSingle.SelectedIndex = 0;
            divamtSingl.Visible = false;
            divShowPerButton.Visible = false;
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            this.BindSemesters(Convert.ToInt16(idno));
        }
        else
        {
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")  // For RCPIT and RCPIPER)
            {
                idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
                ViewState["idno"] = idno;
                this.BindSemesters(Convert.ToInt16(idno));
            }
            else
            {
                ddlSchlModeSingle.SelectedIndex = 0;
                idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
                ViewState["idno"] = idno;
                this.BindSemesters(Convert.ToInt16(idno));
            }
           
        }
    }

    protected void btnShowSchMode_Click(object sender, EventArgs e)
    {
        if(txtPerAmountSingStud.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.updtime, "Please Enter Percentage", this.Page);
            return;
        }
        else
        {
             int idno = 0;
             idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
             this.BindSemesters(Convert.ToInt16(idno));
        }    
    }


    protected void btnDeleteAllotment_Click(object sender, ImageClickEventArgs e)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            objS.DegreeNo = Convert.ToInt32(lblDegrreno.ToolTip);
            objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            objS.IdNo = Convert.ToInt32(lblName.ToolTip);
            //string SemesterNo = objCommon.LookUp("ACD_STUDENT S LEFT JOIN ACD_STUDENT_SCHOLERSHIP SS ON S.IDNO = SS.IDNO AND S.SEMESTERNO = SS.SEMESTERNO", "S.SEMESTERNO", "S.IDNO=" + ViewState["idno"]);

            objS.AdmBatch = Convert.ToInt32(lblAdmBatch.ToolTip);
            objS.ScholershipTypeNo = Convert.ToInt32(ddlScholorshipType.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();
            ImageButton btnDeleteAllotment = sender as ImageButton;          
            int Schlshipno = Convert.ToInt32(btnDeleteAllotment.ToolTip);
            string SemesterNo = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "SEMESTERNO", "IDNO=" + ViewState["idno"] + " AND SCHLSHPNO=" + Schlshipno);
            objS.SemesterNo = Convert.ToInt32(SemesterNo);

            if (Schlshipno > 0)
            {
                retStatus = studCont.DeleteScholarshipAllotment(objS, Schlshipno);
                if (retStatus == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updtime, "Scholarship Allotment Deleted Successfully!", this.Page);

                    BindSemesters(Convert.ToInt16(objS.IdNo));
                }
                else
                {
                    objCommon.DisplayMessage(this.updtime, "Error occurred!", this.Page);
                }
            }           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkScholershipUpdate.btnDeleteAllotment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSchlModeSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchlModeSingle.SelectedValue == "1")
        {
            divamtSingl.Visible = true;
            divShowPerButton.Visible = true;
            
      
        }
        else if (ddlSchlModeSingle.SelectedValue == "2")
        {
            txtPerAmountSingStud.Text = string.Empty;
            divamtSingl.Visible = false;
            divShowPerButton.Visible = false;          
            int idno = 0;
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            this.BindSemesters(Convert.ToInt16(idno));
        }
        else
        {
            txtPerAmountSingStud.Text = string.Empty;
            ddlSchlModeSingle.SelectedIndex = 0;
            divamtSingl.Visible = false;
            divShowPerButton.Visible = false;
            int idno = 0;
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            this.BindSemesters(Convert.ToInt16(idno));
        }
    }


    protected void ddlSchlWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchlWise.SelectedValue == "1")
        {
            int idno = 0;
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            this.BindSemesters(Convert.ToInt16(idno));
        }

        if (ddlSchlWise.SelectedValue == "2")
        {
            int idno = 0;
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            this.BindSemesters(Convert.ToInt16(idno));
        }
     
    }

    

    #endregion




   
}