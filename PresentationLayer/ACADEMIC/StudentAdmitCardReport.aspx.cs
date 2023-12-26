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



public partial class ACADEMIC_StudentAdmitCardReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
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
                    // CheckPageAuthorization();

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
                    int Check = Convert.ToInt32(Session["usertype"]);
                    if (Check == 1)
                    {
                        divRemark.Visible = true;
                    }
                }

                #region Only for PRMITR
                if (Convert.ToInt32(Session["OrgId"]) == 10)
                {
                    btnAttendance.Visible = true;
                }
                else
                {
                    btnAttendance.Visible = false;
                }
                #endregion


            }
            // lblSession.Text = Convert.ToString(Session["sessionname"]);

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }




    protected void PopulateDropDownList()
    {
        try
        {

            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");  //Added by Amey on 12122022


            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

            //  objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

            // FILL DROPDOWN ADMISSION BATCH

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds;
            int sessionno = Convert.ToInt32(Session["currentsession"]);
            int OrgID = Convert.ToInt32(Session["OrgId"]);

            //ds = studCont.GetStudentListForAdmitCard(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue)); // commented on 06-03-2020 by Vaishali
            // prev_status = 0;
            //ds = studCont.GetStudentListForAdmitCard(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlExamname.SelectedValue), OrgID, Convert.ToInt32(ddlSection.SelectedValue)); // added on 06-03-2020 by Vaishali

            ds = studCont.GetStudentListForAdmitCard(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlExamname.SelectedValue), OrgID, Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ViewState["schemeno"])); // added on 13122022 by AMEY


            //ds = studCont.GetStudentListForAdmitCard(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlExamname.SelectedValue), OrgID, Convert.ToInt32(ddlSection.SelectedValue)); // added on 13122022


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //dv = new DataView(ds.Tables[0], "ADMBATCH=" + ddlAdmbatch.SelectedValue, "REGNO", DataViewRowState.OriginalRows);

                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                lvStudentRecords.Visible = true;

                //hftot.Value = ds.Count.ToString();
                btnPrintReport.Enabled = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(updtime, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlColg.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlExamname.SelectedIndex = 0;

            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lvStudentRecords.Visible = false;

            txtRemark.Text = "";
            txtDateofissue.Text = "";

            ddlColg.Focus();

            Session["listIdCard"] = null;
            //Response.Redirect(Request.Url.ToString());
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
            // added on 06-03-2020 by Vaishali
            string examno = objCommon.LookUp("ACD_EXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "EXAMNO = " + ddlExamname.SelectedValue + "");
            //if (rbRegEx.SelectedIndex == 0)
            //{
            //    prev_status = 0;
            //}
            //else
            //{
            //    prev_status = 1;
            //}ddlSemester
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            string date1 = Convert.ToDateTime(txtDateofissue.Text).ToString("dd/MM/yyyy");

            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)//Added by GAurav 02_12_2022
            {

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 9 || Convert.ToInt32(Session["OrgId"]) == 7)
            {
                //url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(ddlColg.SelectedValue) + ",@P_IDNO=" + param + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_EXAM_DATE=" + date1 + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue);
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 8)//MIT AOE
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FULL_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);


                // @P_COLLEGE_CODE=63,@P_IDNO=1453.,@P_DEGREENO=1,@P_BRANCHNO=2,@P_SEMESTERNO=3,@P_SESSIONNO=81,@P_USER_FUll_NAME=Mastersoft%20Super%20Admin,@P_EXAMNO=88,@=P_COLLEGE_ID=1,@P_SECTIONNO=0

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 2)//Crescent   
            {
               url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM_DATE=" + date1;
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 16) //Maher
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 19) //PCEN  Added By Injamam 20_10_2023   
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20) //PJLCOE   Added By Injamam 30_11_2023   
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 21) //TGPCET  Added By Injamam 29_11_2023 
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 18) //HITS  Added By Injamam 29_11_2023 
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 5) //JECRC  Added By Injamam 29_11_2023 
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_EXAMNO=" + Convert.ToInt32(ddlExamname.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterStartupScript(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);
            //ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
                    // if (studentIds.Length > 0)
                    // {
                    // studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim() + ".";
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        txtDateofissue.Text = string.Empty;
        txtRemark.Text = string.Empty;
        // ddlExamname.SelectedIndex = 1;
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            // ddlAdmbatch.SelectedIndex = 0;
            //  ddlAdmbatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlSemester.SelectedIndex = 0;
        txtDateofissue.Text = string.Empty;
        txtRemark.Text = string.Empty;
        //ddlExamname.SelectedIndex = 1;
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND SESSIONNO =" + ddlSession.SelectedValue, "A.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

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

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT WITH (NOLOCK)", "*", "", "REGNO='" + regno + "'", "REGNO");
        //if (rbRegEx.SelectedIndex == 0)
        //{
        //    prev_status = 0;
        //}
        //else
        //{
        //    prev_status = 1;
        //}
        // int sessionno = Convert.ToInt32(Session["currentsession"]);
        // string dateOfIssue = txtDateofissue.Text.ToString();
        // DataSet ds1 = objQrC.GetDetailsForAdmitCard(sessionno, Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), prev_status, Convert.ToInt16(idno), dateOfIssue);
        DataSet ds1 = objQrC.GetDetailsForAdmitCard(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), prev_status, Convert.ToInt16(idno));

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


                //   COMMENTED BY PRAFULL ON DT-26-06-2023 AS PER DISCUSSION 

                foreach (ListViewDataItem lvItem in lvStudentRecords.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkReport") as CheckBox;
                    CheckBox chk1 = lvStudentRecords.Controls[0].FindControl("chkIdentityCard") as CheckBox;


                    if (chk1.Checked == true)
                    {
                        studentIds = chkBox.ToolTip;
                        string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip) + ""));
                        // GenerateQrCode((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip), RegNo);
                        ids = "0";
                        int OrgID = Convert.ToInt32(Session["OrgId"]);

                        if (Convert.ToInt32(Session["OrgId"]) == 6) //RCPIPER added by SHUBHAM ON 21/12/2023
                        {
                            int id = Convert.ToInt32((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip));
                            GenerateQrCode_RCPIPER(id);         //RCPIPER added by SHUBHAM ON 21/12/2023
                        }
                        // int count=Convert.ToInt32( objCommon.LookUp("ACD_ADMITCARD_LOG", "couNt(1)","IDNO="+Convert.ToInt32(studentIds) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                        //int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), studentIds + '.', ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), txtRemark.Text, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToDateTime(txtDateofissue.Text), OrgID, Convert.ToInt32(ddlSection.SelectedValue));

                        //int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), studentIds + '.', ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), txtRemark.Text, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToDateTime(txtDateofissue.Text), OrgID, Convert.ToInt32(ddlSection.SelectedValue));



                        //if (chkg == 2)
                        //{
                        //    objCommon.DisplayMessage("Admit Card Successfully Generated", this.Page);
                        //}


                    }
                    else if (chkBox.Checked == true)
                    {
                        studentIds = chkBox.ToolTip;
                        string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip) + ""));
                        // GenerateQrCode((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip), RegNo);

                        int OrgID = Convert.ToInt32(Session["OrgId"]);
                        if (Convert.ToInt32(Session["OrgId"]) == 6) //RCPIPER added by SHUBHAM ON 21/12/2023
                        {
                            int id = Convert.ToInt32((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip));
                            GenerateQrCode_RCPIPER(id);       //RCPIPER added by SHUBHAM ON 21/12/2023
                        }

                        // int count=Convert.ToInt32( objCommon.LookUp("ACD_ADMITCARD_LOG", "couNt(1)","IDNO="+Convert.ToInt32(studentIds) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                        //int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), studentIds + '.', ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), txtRemark.Text, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToDateTime(txtDateofissue.Text), OrgID, Convert.ToInt32(ddlSection.SelectedValue));

                        //int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), studentIds + '.', ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), txtRemark.Text, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToDateTime(txtDateofissue.Text), OrgID, Convert.ToInt32(ddlSection.SelectedValue));



                        //if (chkg == 2)
                        //{
                        //    objCommon.DisplayMessage("Admit Card Successfully Generated", this.Page);
                        //}
                    }
                    else
                    {

                    }

                }
                if (Convert.ToInt32(Session["OrgId"]) == 8) // for MIT pune
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_MIT.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 9)// for ATLAS
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_atlas.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 3)//for CPUKOTA
                {
                    //ShowReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), examno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPU.rpt");
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPU.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 4) //for CPUH
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPUH.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 7)  //for RAJAGIRI
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Rajagiri.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 1) //RCPIT
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_RCPIT.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2) //CRESCENT
                {
                     ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CRESCENT.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6) //RCPIPER added by gaurav 28_02_2023
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_RCPIPER.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 16)  //Maher by Injamam 
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Maher.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 19)  //PCEN   Added By Injamam 20_10_2023
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_PCEN.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 20)  //PJLCOE   Added By Injamam 30_11_2023
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_PJLCEN.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 18) //HITS  Added By Injamam 29_11_2023 
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_HITS.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 21) //TGPCET Added By Injamam 29_11_2023 
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_TGPCET.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 5) //JECRC Added By Injamam 29_11_2023 
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_JECRC.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 22) //ADCET Added by Tejas 22_12_2023
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_ADCET.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 15) //DAIICT Added by Tejas 22_12_2023
                {
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket_DAIICT.rpt");
                }
                else
                {
                    //ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");  
                    ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt"); //JECRC & RCPIPER
                    //ShowReport(ids, "Student_Admit_Card_Report", "DemoReport.rpt"); 
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        txtDateofissue.Text = string.Empty;
        txtRemark.Text = string.Empty;
        //  ddlExamname.SelectedIndex = 1;

        if (ddlColg.SelectedIndex > 0)
        {
            //int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND SCHEMENO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])));
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlColg.SelectedValue));  //Added on 14122022 

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }

            //DataSet ds12 = objCommon.FillDropDown("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COLLEGE_ID", "COSCHNO=" + ddlColg.SelectedValue, "COLLEGE_ID");

            //int clg_id = Convert.ToInt32(ds12.Tables[0].Rows[0]["COLLEGE_ID"]);
            //ViewState["clg_id"] = Convert.ToInt32(ds12.Tables[0].Rows[0]["COLLEGE_ID"]);


            //int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND COL_SCHEME_NAME=" + "'" + ddlColg.SelectedItem.Text + "'" + "AND SCHEMENO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])));

            //(ViewState["schemenoss"]) = schemeno;

            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "a.DEGREENO");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_EXAM_DATE ED INNER JOIN ACD_SESSION_MASTER SM ON (ED.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_SCHEME SC ON (ED.SCHEMENO=SC.SCHEMENO)", "DISTINCT ED.SESSIONNO", "SESSION_PNAME", "ED.COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue), "SESSIONNO DESC");
            //ddlDegree.Focus();

            ddlSession.Focus();
        }
        else
        {
            ddlColg.Focus();

            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";

            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSection.SelectedItem.Value = "0";

            ddlExamname.Items.Clear();
            ddlExamname.Items.Add("Please Select");
            ddlExamname.SelectedItem.Value = "0";
        }
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

                        //if (rbRegEx.SelectedIndex == 0)
                        //{
                        //    prev_status = 0;
                        //}
                        //else
                        //{
                        //    prev_status = 1;
                        //}

                        //Parameter to Report Document
                        //================================
                        //Extract Parameters From queryString
                        customReport.SetParameterValue("@P_IDNO", hdfuserno.Value);
                        customReport.SetParameterValue("@P_SESSIONNO", Convert.ToInt32(ddlSession.SelectedValue));
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


                        DataSet ds = objCommon.FillDropDown("REFF WITH (NOLOCK)", "EMAILSVCID", "EMAILSVCPWD", string.Empty, string.Empty);
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
                            mailBody.AppendFormat("Dear " + objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_FULLNAME", "UA_IDNO=" + hdfuserno.Value));
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            //mailBody.AppendFormat("<p>Your Admit Card is Generated.</p>");
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                            string sessionnoname = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSION_NAME", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue));
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
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "AdmitCard.btnSendSMS_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }

        }
    }

    protected void btnEligibleStudReport_Click(object sender, EventArgs e)
    {
        ShowReportForEligible(Convert.ToInt32(ddlSession.SelectedValue), "Eligible_Student_NoDues_Report", "rptEligibleStudentListForNoDuesStatus.rpt");
    }

    private void ShowReportForEligible(int sessionno, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + sessionno;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        txtDateofissue.Text = string.Empty;
        txtRemark.Text = string.Empty;

        //objCommon.FillDropDownList(ddlExamname, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO= " + ddlDegree.SelectedValue, "EXAMNAME");

        //   int schemeno=Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND SCHEMENO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])));

        //(ViewState["schemenoss"])=schemeno;

        if (ddlSemester.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlExamname, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", "DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.Schemeno=" + ViewState["schemeno"], "EXAMNAME");

            //ddlExamname.SelectedIndex = 1;

            txtDateofissue.Focus();
        }
        else
        {
            ddlSemester.Focus();

            ddlExamname.Items.Clear();
            ddlExamname.Items.Add("Please Select");
            ddlExamname.SelectedItem.Value = "0";
        }


        if (Convert.ToInt32(Session["OrgId"]) == 9)
        {
            //objCommon.FillDropDownList(ddlSection, "ACD_EXAM_DATE ED INNER JOIN ACD_SECTION SEC ON (ED.SECTIONNO=SEC.SECTIONNO) INNER JOIN ACD_SCHEME SC ON (ED.SCHEMENO=SC.SCHEMENO)", " DISTINCT ED.SECTIONNO", "SEC.SECTIONNAME", " ED.SECTIONNO>0", "SEC.SECTIONNAME");
            //objCommon.FillDropDownList(ddlSection, "ACD_EXAM_DATE ED INNER JOIN ACD_SECTION SEC ON (ED.SECTIONNO=SEC.SECTIONNO) INNER JOIN ACD_SCHEME SC ON (ED.SCHEMENO=SC.SCHEMENO)", "DISTINCT ED.SECTIONNO", "SEC.SECTIONNAME", "ED.SESSIONNO=" + ddlSession.SelectedValue + "AND ED.DEGREENO=" + ddlDegree.SelectedValue + "AND ED.BRANCHNO=" + ddlBranch.SelectedValue + "AND ED.SEMESTERNO=" + ddlSemester.SelectedValue, "SEC.SECTIONNAME");  //Added by Amey on 14122022

            objCommon.FillDropDownList(ddlSection, "ACD_EXAM_DATE ED INNER JOIN ACD_SECTION SEC ON (ED.SECTIONNO=SEC.SECTIONNO) INNER JOIN ACD_SCHEME SC ON (ED.SCHEMENO=SC.SCHEMENO)", "DISTINCT ED.SECTIONNO", "SEC.SECTIONNAME", "ED.SESSIONNO=" + ddlSession.SelectedValue + "AND ED.SCHEMENO=" + ViewState["schemeno"] + "AND ED.SEMESTERNO=" + ddlSemester.SelectedValue + "AND ED.SECTIONNO>0", "SEC.SECTIONNAME");  //Added by Amey on 15122022
        }
        else
        {
            //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", " DISTINCT SECTIONNO", "SECTIONNAME", " SECTIONNO>0", "SECTIONNAME");
            objCommon.FillDropDownList(ddlSection, "ACD_EXAM_DATE ED INNER JOIN ACD_SECTION SEC ON (ED.SECTIONNO=SEC.SECTIONNO) INNER JOIN ACD_SCHEME SC ON (ED.SCHEMENO=SC.SCHEMENO)", "DISTINCT ED.SECTIONNO", "SEC.SECTIONNAME", "ED.SESSIONNO=" + ddlSession.SelectedValue + "AND ED.SCHEMENO=" + ViewState["schemeno"] + "AND ED.SEMESTERNO=" + ddlSemester.SelectedValue + "AND ED.SECTIONNO>0", "SEC.SECTIONNAME");  //Added by Amey on 15122022
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        txtDateofissue.Text = string.Empty;
        txtRemark.Text = string.Empty;

        objCommon.FillDropDownList(ddlExamname, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", "DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.Schemeno=" + ViewState["schemeno"], "EXAMNAME");
    }


    protected void ddlExamname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;

        btnShow.Focus();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.SelectedIndex = 0;
        txtDateofissue.Text = string.Empty;
        txtRemark.Text = string.Empty;
        //ddlExamname.SelectedIndex = 1;
        if (ddlSession.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND SESSIONNO =" + ddlSession.SelectedValue, "A.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSession.Focus();

            //ddlSession.Items.Clear();
            //ddlSession.Items.Add(new ListItem("Please Select", "0"));

            //ddlSemester.Items.Clear();
            //ddlSemester.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";

            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSection.SelectedItem.Value = "0";

            ddlExamname.Items.Clear();
            ddlExamname.Items.Add("Please Select");
            ddlExamname.SelectedItem.Value = "0";

        }

        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }




    //This Method Generate QR-CODE FOR RCPIPER & also  save image in ACD_HALLTICKET_QRCODE Table & QR-Code Files Folder. Added by Shubham on 24/12/23
    private void GenerateQrCode_RCPIPER(int idno)
    {
        try
        {
            string SP_Name = "PKG_GENERATEQR_CODE_DETAILS_FOR_HALLTICKET";
            string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO,@P_COLLEGE_ID";
            string Call_Values = "" + idno + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ViewState["college_id"]);
            DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds1.Tables[0].Rows[0];

                // Check if the required columns exist
                if (row.Table.Columns.Contains("REGNO") && row.Table.Columns.Contains("STUDNAME") &&
                    row.Table.Columns.Contains("DEGREENAME") && row.Table.Columns.Contains("LONGNAME") &&
                    row.Table.Columns.Contains("YEARNAME") && row.Table.Columns.Contains("SEMESTERNAME") &&
                    row.Table.Columns.Contains("SCHEMENAME"))
                {
                    string Qrtext = "PRN:" + ds1.Tables[0].Rows[0]["REGNO"].ToString().Trim() +
                                    "; Stud Name: " + ds1.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() +
                                    "; Degree: " + ds1.Tables[0].Rows[0]["DEGREENAME"] +
                                    "; Branch: " + ds1.Tables[0].Rows[0]["LONGNAME"] +
                                    "; C_Year: " + ds1.Tables[0].Rows[0]["YEARNAME"] +
                                    "; Sem: " + ds1.Tables[0].Rows[0]["SEMESTERNAME"] +
                                    "; Scheme: " + ds1.Tables[0].Rows[0]["SCHEMENAME"] +
                                     "";

                    Session["qr"] = Qrtext.ToString();

                    if (Session["qr"] != null && Session["qr"] is string)
                    {
                        QRCodeEncoder encoder = new QRCodeEncoder();
                        encoder.QRCodeVersion = 10;

                        //ak
                        string newStr = Session["qr"].ToString();
                        //

                        Bitmap img = encoder.Encode(newStr);
                        img.Save(Server.MapPath("~\\img.Jpeg"));
                        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

                        byte[] QR_IMAGE = ViewState["File"] as byte[];
                        long ret = objQrC.AddUpdateQrCodeHallTicket(Convert.ToInt16(idno), QR_IMAGE, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue));

                    }
                }
                else
                {
                    // Handle the case where the required columns are missing
                }
            }
            else
            {

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentAdmitCardReport.GenerateQrCode_RCPIPER() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

}