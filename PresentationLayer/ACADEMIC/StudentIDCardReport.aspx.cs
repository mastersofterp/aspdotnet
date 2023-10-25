using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using System.Data;
using System.Data.OleDb;
using System.Data.Common;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;


public partial class ACADEMIC_StudentIDCardReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                    ShowDetails();
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btnUpload);
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btnShow);
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022

                int orgid = Convert.ToInt32(Session["OrgId"]);

                if (orgid == 1 || orgid == 8 || orgid == 9 || orgid == 6)
                {
                    btnbackReport.Visible = false;
                }



                if (Session["OrgId"].ToString() == "2")
                {
                    btnFrontBackIdentityCard.Visible = true;
                    btnPrintReport.Visible = false;
                    btnbackReport.Visible = false;
                }

                if (Session["OrgId"].ToString() == "16")
                {
                    rfvDegree.Visible = true;
                    fieldman.Visible = true;
                }
                else
                {
                    rfvDegree.Visible = false;
                    fieldman.Visible = false;
                }

            }
            else
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue, "A.SEMESTERNO");
            }

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
            // FILL DROPDOWN SCHEME TYPE
            //objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");           

            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");

            // FILL DROPDOWN BATCH
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "DEGREENAME");

            // FILL DROPDOWN COLLEGE
            objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue , "A.SEMESTERNO");
            //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", " CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CD.BRANCHNO");


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

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            int orgid = Convert.ToInt32(Session["OrgId"]);






            if (orgid == 9)
            {
                SaveQRCODE();
            }

            string ids = GetStudentIDs();

            if (!string.IsNullOrEmpty(ids))
            {

                if (orgid == 1)
                {

                    if (chkIDCard.Checked == false)
                    {
                        // ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardFront.rpt");
                        //ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardFront_Indus_Clgwise.rpt");

                        ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardFrontNew_RCPIT.rpt");
                    }
                    else
                        // StudentIDCardFrontNew
                        ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardFrontNew.rpt");
                }
                if (orgid == 2)
                {
                    ShowReport_Crescent(ids, "Student_ID_Card_Report", "StudentIDCardNewFrontCresent.rpt");
                }
                else if (orgid == 8)
                {
                    ShowReport_ATLAS(ids, "Student_ID_Card_Report", "Student_Identity_Card_MIT.rpt");
                }
                else if (orgid == 9)
                {
                    if (ddlClg.SelectedValue == "1")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "StudentIDCardATLAS_ISDI.rpt");
                    }
                    else if (ddlClg.SelectedValue == "2")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "StudentIDCardATLAS_ISME.rpt");
                    }
                    else if (ddlClg.SelectedValue == "3")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "StudentIDCardATLAS_SMC.rpt");
                    }
                    else if (ddlClg.SelectedValue == "4")
                    {
                        //ShowReport_ATLAS(ids,"Student_ID_Card_Report", "");
                    }
                    else if (ddlClg.SelectedValue == "5")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "StudentIDCardATLAS_INSOFE.rpt");
                    }
                    else if (ddlClg.SelectedValue == "6")
                    {
                        //ShowReport_ATLAS(ids,"Student_ID_Card_Report", "");
                    }
                    else if (ddlClg.SelectedValue == "7")
                    {
                        //ShowReport_ATLAS(ids,"Student_ID_Card_Report", "");
                    }
                    else if (ddlClg.SelectedValue == "8")
                    {
                        //ShowReport_ATLAS(ids,"Student_ID_Card_Report", "");
                    }
                }
                else if (orgid == 16)
                {

                    string ua_section = objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (A.DEGREENO = CDB.DEGREENO AND A.BRANCHNO=CDB.BRANCHNO) INNER JOIN  ACD_UA_SECTION UAS ON (CDB.UGPGOT = UAS.UA_SECTION)", "DISTINCT UAS.UA_SECTIONNAME", "CDB.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue));
                    if (ua_section == "PG")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "Student_Identity_Card_MAHER_PG.rpt");
                    }
                    else if (ua_section == "UG")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "Student_Identity_Card_MAHER_UG.rpt");
                    }
                }


                 //ADDED BY POOJA 0N DATE 15-06-2023
                else if (orgid == 6)
                {
                  

                    string ua_section = objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (A.DEGREENO = CDB.DEGREENO AND A.BRANCHNO=CDB.BRANCHNO) INNER JOIN  ACD_UA_SECTION UAS ON (CDB.UGPGOT = UAS.UA_SECTION)", "DISTINCT UAS.UA_SECTIONNAME", "CDB.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue));
                    if (ua_section == "PG")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "Student_Identity_Card_PG_RCPIPER.rpt");
                    }
                    else if (ua_section == "UG")
                    {
                        ShowReport_ATLAS(ids, "Student_ID_Card_Report", "Student_Identity_Card_UG_RCPIPER.rpt");
                    }
                }

                  //
                else if (orgid == 5)
                {
                    ShowReport_ATLAS(ids, "Student_ID_Card_Report", "Student_Identity_Card_JECRC.rpt");
                }

                else if (orgid == 3)
                {
                    ShowReport_ATLAS(ids, "Student_ID_Card_Report", "rptStudentIdentityCard_CPUK.rpt");
                }

                else if (orgid == 4)
                {
                    ShowReport_ATLAS(ids, "Student_ID_Card_Report", "rptStudentIdentityCard_CPUH.rpt");
                }

                else if (orgid == 18)
                {
                    ShowReport_ATLAS(ids, "Student_ID_Card_Report", "rptStudentIdentityCard_HITS.rpt");
                }
                else
                {
                    // StudentIDCardFrontNew
                    ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardFrontNew.rpt");
                }
            }
            else
                objCommon.DisplayMessage(this.updStudent, "Please Select Students!", this.Page);
        }
        catch (Exception ex)
        {
            throw;
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

    private void GenerateQrCode(string idno)
    {
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "REGNO", "STUDNAME,CONVERT(VARCHAR(50),DOB,106 )AS DOB,STUDENTMOBILE", "IDNO='" + idno + "' AND ISNULL(ADMCAN,0)=0  AND  ISNULL(CAN,0) = 0", "REGNO");

        //DataSet ds1 = objQrC.GetStudentDataForGradeCardPC(Convert.ToInt16(ddlAdmbatch.SelectedValue), Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlCollege.SelectedValue), Convert.ToInt16(ddlDegree.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), declaredDate, dateOfIssue, Convert.ToInt16(idno));
        //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";
        string Qrtext = "PRN=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() +
                        ";STUDENT NAME: " + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() +
                        ";DOB: " + ds.Tables[0].Rows[0]["DOB"].ToString().Trim() +
                        ";EMERGENCY NO : " + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString().Trim() + "";

        Session["qr"] = Qrtext.ToString();

        QRCodeEncoder encoder = new QRCodeEncoder();
        encoder.QRCodeVersion = 10;
        Bitmap img = encoder.Encode(Session["qr"].ToString());
        //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
        img.Save(Server.MapPath("~\\img.Jpeg"));
        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

        //img.Save(Server.MapPath("~\\img.Jpeg"));
        byte[] QR_IMAGE = ViewState["File"] as byte[];
        long ret = objQrC.AddQrCodeForStudentIDCard(Convert.ToInt32(idno), QR_IMAGE);
    }

    protected void BindListView()
    {
        try
        {
            StudentController studCont = new StudentController();
            DataSet ds;
            ds = studCont.GetStudentListForIdentityCard(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (txtRangeTo.Text == string.Empty && txtRangeFrom.Text == string.Empty)
                {
                    lvStudentRecords.DataSource = ds;
                    lvStudentRecords.DataBind();
                }
                else
                {
                    int rangeTo = Convert.ToInt32(txtRangeTo.Text);
                    int rangeFrom = Convert.ToInt32(txtRangeFrom.Text);

                    if (rangeFrom > rangeTo)
                    {
                        objCommon.DisplayMessage(this.updStudent, "Please Select Valid Range!!", this.Page);
                        return;
                    }
                    DataTable xdata = (from r in ds.Tables[0].AsEnumerable() where Convert.ToInt32(r["SRNO"]) <= rangeTo && Convert.ToInt32(r["SRNO"]) >= rangeFrom select r).CopyToDataTable();
                    lvStudentRecords.DataSource = xdata;
                    lvStudentRecords.DataBind();
                }
            
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label -
                hftot.Value = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
                objCommon.DisplayMessage(this.updStudent, "Record Not Found!!", this.Page);
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
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportDefault(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",academicsession=" + (DateTime.Today.Year).ToString() + "-" + (DateTime.Today.Year + 1).ToString() + ",@P_IDNO=" + param + " ,@P_OrganizationId=" + Convert.ToInt32(Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport_Crescent(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + " ,@P_OrganizationId=" + Convert.ToInt32(Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void ShowReport_ATLAS(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + param + " ,@P_OrganizationId=" + Convert.ToInt32(Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
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
                        studentIds += "$";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    //GenerateQrCode(studentIds);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }

    private void SaveQRCODE()
    {
        string studentId = string.Empty;

        foreach (ListViewDataItem item in lvStudentRecords.Items)
        {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
            {
                studentId = (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                GenerateQrCode(studentId);
            }
        }
    }




    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + ddlDegree.SelectedValue + "AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CD.BRANCHNO");
            ddlBranch.Focus();
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


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND COLLEGE_ID=" + ddlClg.SelectedValue, "A.SEMESTERNO");
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

    private void ShowDetails()
    {
        try
        {
            SqlDataReader dr = objCommon.GetCommonDetails();

            if (dr != null)
            {
                if (dr.Read())
                {
                    //imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    // imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=" + dr["Errors"].ToString() + "&type=registrar";
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // Upload the Registrar Sign to below code..
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            ReferenceController objEc = new ReferenceController();
            Reference objRef = new Reference();

            if (fuRegistrarSign.HasFile)
            {
                objRef.CollegeLogo = objCommon.GetImageData(fuRegistrarSign);
            }
            else
            {
                System.IO.FileStream ff = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/logo.gif"), System.IO.FileMode.Open);
                int ImageSize = (int)ff.Length;
                byte[] ImageContent = new byte[ff.Length];
                ff.Read(ImageContent, 0, ImageSize);
                ff.Close();
                ff.Dispose();
                objRef.CollegeLogo = ImageContent;
            }



            //if (ViewState["action"] != null)
            //{
            //Edit Reference
            CustomStatus cs = (CustomStatus)objEc.UpdateRegistrarSign(objRef);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updStudent, "Registrat Sign Updated Successfully!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updStudent, "Error!!", this.Page);
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnbackReport_Click(object sender, EventArgs e)
    {
        try
        {
            int orgid = Convert.ToInt32(Session["OrgId"]);
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                if (orgid == 2)
                {
                    ShowReport_Crescent(ids, "Student_ID_Card_Report", "StudentIDCardNewBackCresent.rpt");
                }
                else
                {
                    if (chkIDCard.Checked == false)
                    {
                        //ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardBack.rpt");
                        ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardBack_Indus_Clgwise_crese.rpt");
                    }
                    else
                        ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardBackNew.rpt");
                }
            }
            else
                objCommon.DisplayMessage(this.updStudent, "Please Select Students!", this.Page);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();

        if (ddlClg.SelectedValue != "0")
        {
            int orgid = Convert.ToInt32(Session["OrgId"]);
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "2")
            {
                btnbackReport.Visible = false;
            }

            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D ON(CD.DEGREENO=D.DEGREENO)", " DISTINCT CD.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue) + "AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "D.DEGREENAME");
        }
        else
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void btnFrontBackIdentityCard_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                ShowReport_Crescent(ids, "Student_ID_Card_Report", "StudentIDCardNewFrontCresent.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlClg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
}
