//===============================================//
// MODULE NAME   : RFC ERP Portal (RFC Common Code)
// PAGE NAME     : Estimate Certificate 
// CREATION DATE : 26-07-2023 
// CREATED BY    : Jay Takalkhede 
// Modified BY   : Jay Takalkhede
// Modified Date : 24-08-2023
// Version :- 1) RFC.Feature.Major.1 (26-07-2023) 2) RFC.Enhancement.Major.2 (24-08-2023) 3) RFC.Enhancement.Major.2 (01-09-2023)
//===============================================//


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_REPORTS_Estimate_Certificate_Master : System.Web.UI.Page
{
    #region Page Action
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();
    CertificateMasterController objcerMasterController = new CertificateMasterController();
    bool IsDataPresent = false;

   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    #endregion

    #region PageLoad
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ACTIVESTATUS=1", "DEGREENO");
            objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKCODE", "BANKNO>0 AND ACTIVESTATUS=1", "BANKNO");

            //Fill Dropdown admbatch
            objCommon.FillDropDownList(ddldegreerc, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ACTIVESTATUS=1", "DEGREENO");
            objCommon.FillDropDownList(ddlAdmyear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1  ", "ACADEMIC_YEAR_NAME DESC");
            objCommon.FillDropDownList(ddlAcadYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1 ", "ACADEMIC_YEAR_NAME DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method
                    PopulateDropDown();

                    if (Convert.ToInt32(Session["OrgId"]) == 1)
                    {
                        dvdegree.Visible = true;

                    }
                    else
                    {
                        dvdegree.Visible = false;
                    }
                  
                }
              
            }
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
                Response.Redirect("~/notauthorized.aspx?page=Estimate_Certificate_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Estimate_Certificate_Master.aspx");
        }
    }

    public void HiddenItemBank()
    {
        if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            DivBank.Visible = true;
            txtACNO.Visible = true;
            ddlBank.Visible = true;
            txtIFSC.Visible = true;
            txttypeofAC.Visible = true;
        }
        else
        {
            DivBank.Visible = false;
            txtACNO.Visible = false;
            ddlBank.Visible = false;
            txtIFSC.Visible = false;
            txttypeofAC.Visible = false;
        }
    }
    #endregion

    #region GenrateCertificate

    #region Bind 
    protected void btnShowData1_Click(object sender, EventArgs e)
    {
        try
        {
            BindList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BindList()
    {
        try
        {
            DataSet ds;
            //DataSet dsissueCert;
            //DataSet ds;
            int Admyear = 0;
            int branchNo = 0;
            int semesterNo = 0;
            int degreeNo = 0;
            //string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ADMCAN=0 AND REGNO='" + txtSearch_Enrollno_LC.Text + "'");
            //ViewState["idno"] = idNo;
            Admyear = Convert.ToInt32(ddlAdmyear.SelectedValue);
            branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            
            ds = objcerMasterController.GetStudentListForEBC(Admyear, branchNo, semesterNo, degreeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds.Tables[0];
                lvStudentRecords.DataBind();
                HiddenItemBank();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "No student data found", this);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();

            }
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
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }

    #endregion Bind

    #region Show Report
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //int PRINT=0;
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this.updpnlExam2, "Please Select only one Student for issuing Certificate", this);
            HiddenItemBank();
            return;
        }
        else if(count>1)
         {
                objCommon.DisplayMessage(this.updpnlExam2, "Please Select only one Student for issuing Certificate", this);
                HiddenItemBank();
                return;
         }
        else
        {
            string PRINT = objCommon.LookUp("ACD_CERT_TRAN", "COUNT(PRINT_CERTIFICATE)", "CERT_NO=30 AND IDNO='" + GetStudentIDs() + "'");
            if (PRINT == "0")
            {
                objCommon.DisplayMessage(this.updpnlExam2, "Please Confirm Student for issuing Certificate", this);
                HiddenItemBank();
                return;
            }
            else
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1 )
                {                  
                    ShowReport(GetStudentIDs(), "ESTIMATE_Certificate", "rptEstimateCertificate_RCPITEC.rpt");
                    BindList();
                    btnPrint.Enabled = false;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    ShowReport(GetStudentIDs(), "ESTIMATE_Certificate", "rptEstimateCertificate_RCPIPER_NEW.rpt");
                    BindList();
                    btnPrint.Enabled = false;
                }
                else
                {
                    //RFC.Enhancement.Major.2 (24-08-2023) Changes done as per degree for RCpit Client(Tkt No.47196)
                    ShowReport(GetStudentIDs(), "ESTIMATE_Certificate", "rptEstimateCertificate.rpt");
                    BindList();
                    btnPrint.Enabled = false;
                }
            }
        }
    }
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            CertificateMasterController objcertMasterController = new CertificateMasterController();
            CertificateMaster objcertMaster = new CertificateMaster();
               int Backno = 0;
              string Banckname =string.Empty;
              string TypeofAcc =string.Empty;
              string AccNo     =string.Empty;
              string Ifsc = string.Empty;
              if (Convert.ToInt32(Session["OrgId"]) == 6)
              {//RFC.Enhancement.Major.2 (01-09-2023)
               Backno       = Convert.ToInt32(ddlBank.SelectedValue);
               Banckname = ddlBank.SelectedItem.Text;
               TypeofAcc = txttypeofAC.Text;
               AccNo     = txtACNO.Text;
               Ifsc      = txtIFSC.Text.ToUpper();
              }
            string year=objCommon.LookUp("ACD_STUDENT", "YEAR", "IDNO=" + param);
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 6)
            {//RFC.Enhancement.Major.2 (01-09-2023)
                url += "&param=@P_IDNO=" + param + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_ACDYEAR=" + Convert.ToInt32(ddlAdmyear.SelectedValue) + ",YEAR=" + year.ToString() + ",BankName=" + Banckname + ",Accno=" + AccNo + ",IFSCCODE=" + Ifsc + ",ToBank=" + TypeofAcc;
            }
            else
            {
                url += "&param=@P_IDNO=" + param + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_ACDYEAR=" + Convert.ToInt32(ddlAdmyear.SelectedValue) + ",YEAR=" + year.ToString();
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
            foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
                cbRow.Checked = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Show Report

    #region DDL
    protected void btnCancel_LC_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        if (ddlDegree.SelectedIndex > 0)
        {
            if (ddlDegree.SelectedValue == "3")
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", " ACTIVESTATUS=1 AND SEMESTERNO BETWEEN 1 AND 4 ", "SEMESTERNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNAME");
            }
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "B.ACTIVESTATUS=1 AND CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
            ddlSemester.SelectedIndex = -1;
        }
        else
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlSemester.SelectedIndex = -1;
            ddlBranch.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select Degree!", this.Page);
            ddlDegree.Focus();
        }
    }

    protected void ddlAdmyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        if (ddlAdmyear.SelectedIndex > 0)
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlSemester.SelectedIndex = -1;
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
        }
        else
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlSemester.SelectedIndex = -1;
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
        }
    }
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done (Tkt No.47196)
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        if (ddlBranch.SelectedIndex > 0)
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlSemester.SelectedIndex = -1;
        }
        else
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlSemester.SelectedIndex = -1;
        }
    }
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done (Tkt No.47196)
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        if (ddlSemester.SelectedIndex > 0)
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
        }
        else
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
        }
    }
    #endregion

    #region Confirm
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        int count1 = 0;
        int count = 0;
        int count_Submit = 0;
        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this.updpnlExam2, "Please Select only one Student for confirm and issuing Certificate", this);
            HiddenItemBank();
            return;
        }
        else if (count > 1)
        {
            objCommon.DisplayMessage(this.updpnlExam2, "Please Select only one Student for confirm and issuing Certificate", this);
            HiddenItemBank();
            return;
        }
        else
        {
            CertificateMasterController objcertMasterController = new CertificateMasterController();
            CertificateMaster objcertMaster = new CertificateMaster();
            try
            {
                foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
                    if (cbRow.Checked == true)
                    {

                        count1++;
                        objcertMaster.IpAddress = ViewState["ipAddress"].ToString();
                        objcertMaster.UaNO = Convert.ToInt32(Session["userno"]);
                        objcertMaster.CollegeCode = Session["colcode"].ToString();
                        HiddenField hfRow = (dataitem.FindControl("hidIdNo")) as HiddenField;
                        //string hb=hfRow.Value;
                        objcertMaster.IdNo = Convert.ToInt32(hfRow.Value);
                        objcertMaster.IssueStatus = 1;
                        TextBox txtRemark = (dataitem.FindControl("txtRemark")) as TextBox;
                        objcertMaster.Remark = txtRemark.Text;
                        objcertMaster.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                        int ORGID = Convert.ToInt32(Session["OrgId"]);
                        if (Convert.ToInt32(Session["OrgId"]) == 6) //RFC.Enhancement.Major.2 (01-09-2023)
                        {
                            int Backno       = Convert.ToInt32(ddlBank.SelectedValue);
                            string TypeofAcc = txttypeofAC.Text;
                            string AccNo     = txtACNO.Text;
                            string Ifsc      = txtIFSC.Text;
                            int mode = 0;
                            DataSet ds = objcerMasterController.AddBankEstimateCertificate(objcertMaster, Backno, TypeofAcc, AccNo, Ifsc, mode);//RFC.Enhancement.Major.2 (01-09-2023)
                        }
                        CustomStatus cs = (CustomStatus)objcertMasterController.AddEstimateCertificate(objcertMaster, ORGID);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            count_Submit++;
                            btnPrint.Enabled = true;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updpnlExam2, "Error !!!", this);
                        }

                    }
                }
                foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
                {
                    TextBox txtRemark = (dataitem.FindControl("txtRemark")) as TextBox;
                    txtRemark.Text = string.Empty;
                }
                if (count1 == count_Submit)
                {
                    objCommon.DisplayMessage(this.updpnlExam2, "Process Done Successfully !!!", this);
                    HiddenItemBank();
                    //BindList();
                    return;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
    #endregion

    #endregion

    #region ManageHead
    #region GridviewgrdInstitute
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        // dr["RowNumber"] = 1; dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("4th_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["Particular"] = string.Empty;
        dr["1st_Year"] = string.Empty;
        dr["2nd_Year"] = string.Empty;
        dr["3rd_Year"] = string.Empty;
        dr["4th_Year"] = string.Empty;
        dr["PARNO"] = 0;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        grdInstitute.DataSource = dt;
        grdInstitute.DataBind();
    }

    private void SetInitialRow1()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        // dr["RowNumber"] = 1; dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("4th_Year", typeof(string)));
        //  dt.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["Particular"] = string.Empty;
        dr["1st_Year"] = string.Empty;
        dr["2nd_Year"] = string.Empty;
        dr["3rd_Year"] = string.Empty;
        dr["4th_Year"] = string.Empty;
        // dr["PARNO"] = 0;
        //        dt.Rows.Add(dt);
        //dr["INSTALMENT_NO"] = 1;
        //dr["DUE_DATE"] = string.Empty;
        //dr["INSTALL_AMOUNT"] = string.Empty;
        //dr["RECON"] = 0;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        // ViewState["CurrentTable"] = dt;

        grdInstitute.DataSource = dt;
        grdInstitute.DataBind();

    }
    private void AddNewRowToGrid1()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("4th_Year", typeof(string)));
                //    dtNewTable.Columns.Add(new DataColumn("PARNO", typeof(int)));
                drCurrentRow = dtNewTable.NewRow();
                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Particular"] = string.Empty;
                drCurrentRow["1st_Year"] = string.Empty;
                drCurrentRow["2nd_Year"] = string.Empty;
                drCurrentRow["3rd_Year"] = string.Empty;
                drCurrentRow["4th_Year"] = string.Empty;
                //  drCurrentRow["PARNO"] = 0;
                dtNewTable.Rows.Add(drCurrentRow);

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txtHead = (TextBox)grdInstitute.Rows[rowIndex].Cells[1].FindControl("txtHead");
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txt1styear");
                    TextBox txt2ndyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[3].FindControl("txt2ndyear");
                    TextBox txt3rdyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[4].FindControl("txt3rdyear");
                    TextBox txt4thyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[5].FindControl("txt4thyear");
                    //   HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[6].FindControl("hdnId");

                    if (txt1styear.Text.Trim() != string.Empty && txtHead.Text.Trim() != string.Empty && txt2ndyear.Text.Trim() != string.Empty && txt3rdyear.Text.Trim() != string.Empty && txt4thyear.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Particular"] = txtHead.Text;
                        drCurrentRow["1st_Year"] = txt1styear.Text;
                        drCurrentRow["2nd_Year"] = txt2ndyear.Text;
                        drCurrentRow["3rd_Year"] = txt3rdyear.Text;
                        drCurrentRow["4th_Year"] = txt4thyear.Text;
                        //   drCurrentRow["PARNO"] = hdn.Value;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }
                ViewState["CurrentTable"] = dtNewTable;
                grdInstitute.DataSource = dtNewTable;
                grdInstitute.DataBind();

                SetBindPreviousData();
            }
            else
            {
                //objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtHead = (TextBox)grdInstitute.Rows[rowIndex].Cells[1].FindControl("txtHead");
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txt1styear");
                    TextBox txt2ndyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[3].FindControl("txt2ndyear");
                    TextBox txt3rdyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[4].FindControl("txt3rdyear");
                    TextBox txt4thyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[5].FindControl("txt4thyear");
                    //  HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[6].FindControl("hdnId");

                    txtHead.Text = dt.Rows[i]["Particular"].ToString();
                    txt1styear.Text = dt.Rows[i]["1st_Year"].ToString();
                    txt2ndyear.Text = dt.Rows[i]["2nd_Year"].ToString();
                    txt3rdyear.Text = dt.Rows[i]["3rd_Year"].ToString();
                    txt4thyear.Text = dt.Rows[i]["4th_Year"].ToString();
                    // hdn.Value = dt.Rows[i]["PARNO"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    private void SetBindPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtHead = (TextBox)grdInstitute.Rows[rowIndex].Cells[1].FindControl("txtHead");
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txt1styear");
                    TextBox txt2ndyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[3].FindControl("txt2ndyear");
                    TextBox txt3rdyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[4].FindControl("txt3rdyear");
                    TextBox txt4thyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[5].FindControl("txt4thyear");

                    txtHead.Text = dt.Rows[i]["Particular"].ToString();
                    txt1styear.Text = dt.Rows[i]["1st_Year"].ToString();
                    txt2ndyear.Text = dt.Rows[i]["2nd_Year"].ToString();
                    txt3rdyear.Text = dt.Rows[i]["3rd_Year"].ToString();
                    txt4thyear.Text = dt.Rows[i]["4th_Year"].ToString();
                    AddNewRowToGrid1();
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("4th_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("PARNO", typeof(int)));

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txtHead = (TextBox)grdInstitute.Rows[rowIndex].Cells[1].FindControl("txtHead");
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txt1styear");
                    TextBox txt2ndyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[3].FindControl("txt2ndyear");
                    TextBox txt3rdyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[4].FindControl("txt3rdyear");
                    TextBox txt4thyear = (TextBox)grdInstitute.Rows[rowIndex].Cells[5].FindControl("txt4thyear");
                    HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[6].FindControl("hdnId");

                    if (txt1styear.Text.Trim() != string.Empty && txtHead.Text.Trim() != string.Empty && txt2ndyear.Text.Trim() != string.Empty && txt3rdyear.Text.Trim() != string.Empty && txt4thyear.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Particular"] = txtHead.Text;
                        drCurrentRow["1st_Year"] = txt1styear.Text;
                        drCurrentRow["2nd_Year"] = txt2ndyear.Text;
                        drCurrentRow["3rd_Year"] = txt3rdyear.Text;
                        drCurrentRow["4th_Year"] = txt4thyear.Text;
                        // drCurrentRow["PARNO"] = hdn.Value;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }
                drCurrentRow = dtNewTable.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Particular"] = string.Empty;
                drCurrentRow["1st_Year"] = string.Empty;
                drCurrentRow["2nd_Year"] = string.Empty;
                drCurrentRow["3rd_Year"] = string.Empty;
                drCurrentRow["4th_Year"] = string.Empty;
                drCurrentRow["PARNO"] = 0;

                dtNewTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtNewTable;
                grdInstitute.DataSource = dtNewTable;
                grdInstitute.DataBind();

                SetBindPreviousData();
            }
            else
            {
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void grdInstitute_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int parno = Convert.ToInt32(e.CommandArgument);
            if (parno == 0)
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowID = gvRow.RowIndex;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 1)
                    {
                        if (gvRow.RowIndex <= dt.Rows.Count)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }
                    //Store the current data in ViewState for future reference
                    ViewState["CurrentTable"] = dt;
                    //Re bind the GridView for the updated data
                    grdInstitute.DataSource = dt;
                    grdInstitute.DataBind();

                    //Set Previous Data on Postbacks
                    SetPreviousData();
                }
            }
            else 
            {

                //====== Getting connection string defined in the web.config file. Pointed to the database we want to use.
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

                //======= Delete Query.
                string cmdText = "DELETE FROM ACD_ESTIMATE_CERT_PARTICULARS WHERE parno=@parno";

                //====== Providning information to SQL command object about which query to 
                //====== execute and from where to get database connection information.
                SqlCommand cmd = new SqlCommand(cmdText, con);

                //===== Adding parameters/Values.
                cmd.Parameters.AddWithValue("@parno", parno);

                //===== To check current state of the connection object. If it is closed open the connection
                //===== to execute the insert query.
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //===== Execute Query.
                cmd.ExecuteNonQuery();

                //===== close the connection.
                con.Close();

                //===== Bind data to FormView so that FormView will display updated data.
                //bindAllEmployees();
                BindGridviewgrd();
            }
        }
    }
    #endregion

    #region GridviewgrdHostel

    private void SetInitialRowH()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        // dr["RowNumber"] = 1; dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("4th_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr1 = dt1.NewRow();
        // dr["RowNumber"] = 1;
        dr1["Particular"] = string.Empty;
        dr1["1st_Year"] = string.Empty;
        dr1["2nd_Year"] = string.Empty;
        dr1["3rd_Year"] = string.Empty;
        dr1["4th_Year"] = string.Empty;
        dr1["PARNO"] = 0;

        dt1.Rows.Add(dr1);

        //Store the DataTable in ViewState
        ViewState["CurrentTableH"] = dt1;

        grdHostel.DataSource = dt1;
        grdHostel.DataBind();
    }
    private void SetInitialRowH1()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        // dr["RowNumber"] = 1; dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("4th_Year", typeof(string)));
        dr1 = dt1.NewRow();
        // dr["RowNumber"] = 1;
        dr1["Particular"] = string.Empty;
        dr1["1st_Year"] = string.Empty;
        dr1["2nd_Year"] = string.Empty;
        dr1["3rd_Year"] = string.Empty;
        dr1["4th_Year"] = string.Empty;

        dt1.Rows.Add(dr1);

        //Store the DataTable in ViewState
        // ViewState["CurrentTableH"] = dt1;

        grdHostel.DataSource = dt1;
        grdHostel.DataBind();

    }
    private void AddNewRowToGridH1()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTableH"];
            DataRow drCurrentRow1 = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                DataTable dtNewTable1 = new DataTable();
                dtNewTable1.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("4th_Year", typeof(string)));
                drCurrentRow1 = dtNewTable1.NewRow();
                drCurrentRow1["RowNumber"] = 1;
                drCurrentRow1["Particular"] = string.Empty;
                drCurrentRow1["1st_Year"] = string.Empty;
                drCurrentRow1["2nd_Year"] = string.Empty;
                drCurrentRow1["3rd_Year"] = string.Empty;
                drCurrentRow1["4th_Year"] = string.Empty;
                dtNewTable1.Rows.Add(drCurrentRow1);

                for (int i = 0; i < dtCurrentTable1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txthHead = (TextBox)grdHostel.Rows[rowIndex].Cells[1].FindControl("txthHead");
                    TextBox txth1styear = (TextBox)grdHostel.Rows[rowIndex].Cells[2].FindControl("txth1styear");
                    TextBox txth2ndyear = (TextBox)grdHostel.Rows[rowIndex].Cells[3].FindControl("txth2ndyear");
                    TextBox txth3rdyear = (TextBox)grdHostel.Rows[rowIndex].Cells[4].FindControl("txth3rdyear");
                    TextBox txth4thyear = (TextBox)grdHostel.Rows[rowIndex].Cells[5].FindControl("txth4thyear");

                    if (txth1styear.Text.Trim() != string.Empty && txthHead.Text.Trim() != string.Empty && txth2ndyear.Text.Trim() != string.Empty && txth3rdyear.Text.Trim() != string.Empty && txth4thyear.Text.Trim() != string.Empty)
                    {
                        drCurrentRow1 = dtNewTable1.NewRow();

                        drCurrentRow1["RowNumber"] = i + 1;
                        drCurrentRow1["Particular"] = txthHead.Text;
                        drCurrentRow1["1st_Year"] = txth1styear.Text;
                        drCurrentRow1["2nd_Year"] = txth2ndyear.Text;
                        drCurrentRow1["3rd_Year"] = txth3rdyear.Text;
                        drCurrentRow1["4th_Year"] = txth4thyear.Text;

                        rowIndex++;
                        dtNewTable1.Rows.Add(drCurrentRow1);
                    }
                    else
                    {
                        return;
                    }
                }
                ViewState["CurrentTableH"] = dtNewTable1;
                grdHostel.DataSource = dtNewTable1;
                grdHostel.DataBind();

                SetBindPreviousDataH();
            }
            else
            {
                //objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    private void SetPreviousDataH()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["CurrentTableH"];
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    TextBox txthHead = (TextBox)grdHostel.Rows[rowIndex].Cells[1].FindControl("txthHead");
                    TextBox txth1styear = (TextBox)grdHostel.Rows[rowIndex].Cells[2].FindControl("txth1styear");
                    TextBox txth2ndyear = (TextBox)grdHostel.Rows[rowIndex].Cells[3].FindControl("txth2ndyear");
                    TextBox txth3rdyear = (TextBox)grdHostel.Rows[rowIndex].Cells[4].FindControl("txth3rdyear");
                    TextBox txth4thyear = (TextBox)grdHostel.Rows[rowIndex].Cells[5].FindControl("txth4thyear");

                    txthHead.Text = dt1.Rows[i]["Particular"].ToString();
                    txth1styear.Text = dt1.Rows[i]["1st_Year"].ToString();
                    txth2ndyear.Text = dt1.Rows[i]["2nd_Year"].ToString();
                    txth3rdyear.Text = dt1.Rows[i]["3rd_Year"].ToString();
                    txth4thyear.Text = dt1.Rows[i]["4th_Year"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    private void SetBindPreviousDataH()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["CurrentTableH"];
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    TextBox txthHead = (TextBox)grdHostel.Rows[rowIndex].Cells[1].FindControl("txthHead");
                    TextBox txth1styear = (TextBox)grdHostel.Rows[rowIndex].Cells[2].FindControl("txth1styear");
                    TextBox txth2ndyear = (TextBox)grdHostel.Rows[rowIndex].Cells[3].FindControl("txth2ndyear");
                    TextBox txth3rdyear = (TextBox)grdHostel.Rows[rowIndex].Cells[4].FindControl("txth3rdyear");
                    TextBox txth4thyear = (TextBox)grdHostel.Rows[rowIndex].Cells[5].FindControl("txth4thyear");

                    txthHead.Text = dt1.Rows[i]["Particular"].ToString();
                    txth1styear.Text = dt1.Rows[i]["1st_Year"].ToString();
                    txth2ndyear.Text = dt1.Rows[i]["2nd_Year"].ToString();
                    txth3rdyear.Text = dt1.Rows[i]["3rd_Year"].ToString();
                    txth4thyear.Text = dt1.Rows[i]["4th_Year"].ToString();
                    AddNewRowToGridH1();
                    rowIndex++;
                }
            }
        }
    }
    private void AddNewRowToGridH()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTableH"];
            DataRow drCurrentRow1 = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                DataTable dtNewTable1 = new DataTable();
                dtNewTable1.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("3rd_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("4th_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("PARNO", typeof(int)));
                for (int i = 0; i < dtCurrentTable1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txthHead = (TextBox)grdHostel.Rows[rowIndex].Cells[1].FindControl("txthHead");
                    TextBox txth1styear = (TextBox)grdHostel.Rows[rowIndex].Cells[2].FindControl("txth1styear");
                    TextBox txth2ndyear = (TextBox)grdHostel.Rows[rowIndex].Cells[3].FindControl("txth2ndyear");
                    TextBox txth3rdyear = (TextBox)grdHostel.Rows[rowIndex].Cells[4].FindControl("txth3rdyear");
                    TextBox txth4thyear = (TextBox)grdHostel.Rows[rowIndex].Cells[5].FindControl("txth4thyear");

                    if (txth1styear.Text.Trim() != string.Empty && txthHead.Text.Trim() != string.Empty && txth2ndyear.Text.Trim() != string.Empty && txth3rdyear.Text.Trim() != string.Empty && txth4thyear.Text.Trim() != string.Empty)
                    {
                        drCurrentRow1 = dtNewTable1.NewRow();

                        drCurrentRow1["RowNumber"] = i + 1;
                        drCurrentRow1["Particular"] = txthHead.Text;
                        drCurrentRow1["1st_Year"] = txth1styear.Text;
                        drCurrentRow1["2nd_Year"] = txth2ndyear.Text;
                        drCurrentRow1["3rd_Year"] = txth3rdyear.Text;
                        drCurrentRow1["4th_Year"] = txth4thyear.Text;

                        rowIndex++;
                        dtNewTable1.Rows.Add(drCurrentRow1);
                    }
                    else
                    {
                        return;
                    }
                }
                drCurrentRow1 = dtNewTable1.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow1["Particular"] = string.Empty;
                drCurrentRow1["1st_Year"] = string.Empty;
                drCurrentRow1["2nd_Year"] = string.Empty;
                drCurrentRow1["3rd_Year"] = string.Empty;
                drCurrentRow1["4th_Year"] = string.Empty;
                drCurrentRow1["PARNO"] = 0;

                dtNewTable1.Rows.Add(drCurrentRow1);
                ViewState["CurrentTableH"] = dtNewTable1;
                grdHostel.DataSource = dtNewTable1;
                grdHostel.DataBind();

                SetBindPreviousDataH();
            }
            else
            {
                //objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    protected void btnhAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGridH();
    }

    protected void grdHostel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            //--- Get primarry key value of the selected record.
            int parno = Convert.ToInt32(e.CommandArgument);
            if (parno == 0)
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowID = gvRow.RowIndex;
                if (ViewState["CurrentTableH"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTableH"];
                    if (dt.Rows.Count > 1)
                    {
                        if (gvRow.RowIndex <= dt.Rows.Count)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }
                    //Store the current data in ViewState for future reference
                    ViewState["CurrentTableH"] = dt;
                    //Re bind the GridView for the updated data
                    grdHostel.DataSource = dt;
                    grdHostel.DataBind();

                    //Set Previous Data on Postbacks
                    SetPreviousDataH();
                }
            }
            else
            {

                //====== Getting connection string defined in the web.config file. Pointed to the database we want to use.
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

                //======= Delete Query.
                string cmdText = "DELETE FROM ACD_ESTIMATE_CERT_PARTICULARS WHERE parno=@parno";

                //====== Providning information to SQL command object about which query to 
                //====== execute and from where to get database connection information.
                SqlCommand cmd = new SqlCommand(cmdText, con);

                //===== Adding parameters/Values.
                cmd.Parameters.AddWithValue("@parno", parno);

                //===== To check current state of the connection object. If it is closed open the connection
                //===== to execute the insert query.
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //===== Execute Query.
                cmd.ExecuteNonQuery();

                //===== close the connection.
                con.Close();

                //===== Bind data to FormView so that FormView will display updated data.
                //bindAllEmployees();
                BindGridviewgrd();
            }
        }
    }

    #endregion

    #region BindGridviewgrd
    private void BindGridviewgrd()
    {

        try
         {

         DataSet dslist = objcerMasterController.GetParticularinfo(Convert.ToInt32(ddlAcadYear.SelectedValue), rbdExpenditure.SelectedValue, rbdstudentGender.SelectedValue, Convert.ToInt32(ddldegreerc.SelectedValue));

       if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = dslist.Tables[0];
                if (rbdExpenditure.SelectedValue == "A")
                {
                    SetInitialRow();
                    ViewState["CurrentTable"] = dt1;
                    pnlStudInstitute.Visible = true;
                    pnlStudHostel.Visible = false;
                    grdInstitute.Visible = true;
                    grdInstitute.DataSource = dslist;
                    grdInstitute.DataBind();
                    int cc=grdInstitute.Rows.Count;
                }
                else
                {
                    SetInitialRowH();
                    ViewState["CurrentTableH"] = dt1;
                    pnlStudHostel.Visible = true;
                    pnlStudInstitute.Visible = false;
                    grdHostel.Visible = true;
                    grdHostel.DataSource = dslist;
                    grdHostel.DataBind();
                    int cc=grdHostel.Rows.Count;
                }

            }
            else
            {
                if (rbdExpenditure.SelectedValue == "A")
                {
                    SetInitialRow();
                    pnlStudInstitute.Visible = true;
                    pnlStudHostel.Visible = false;
                    grdInstitute.Visible = true;
                }
                else
                {
                    SetInitialRowH();
                    pnlStudHostel.Visible = true;
                    pnlStudInstitute.Visible = false;
                    grdHostel.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.btnConfirmHead_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        }   

    #endregion

    #region Submit
    private void SubmitDatarowInstitute_mca(GridViewRow dRow)
    {
        int count = 0;

        TextBox txtHead_mca = dRow.FindControl("txtHead_mca") as TextBox;
        TextBox txt1styearmca = dRow.FindControl("txt1styearmca") as TextBox;
        TextBox txt2ndyearmca = dRow.FindControl("txt2ndyearmca") as TextBox;
        decimal thirdAmount = Convert.ToDecimal(null);
        decimal fourthAmount = Convert.ToDecimal(null);
        string Head = txtHead_mca.Text;

        decimal firstAmount = txt1styearmca.Text == string.Empty ? 0 : Convert.ToDecimal(txt1styearmca.Text);
        decimal SecondAmount = txt2ndyearmca.Text == string.Empty ? 0 : Convert.ToDecimal(txt2ndyearmca.Text);


        string EXPE_CODE = rbdExpenditure.SelectedValue;
        int AcdYear = Convert.ToInt32(ddlAcadYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddldegreerc.SelectedValue);
        string IpAddress = ViewState["ipAddress"].ToString();
        int UaNO = Convert.ToInt32(Session["userno"]);
        int INSERT = Convert.ToInt32(ViewState["COUNT"]);
        string gender = rbdstudentGender.SelectedValue;
        CustomStatus cs = (CustomStatus)objcerMasterController.InsertExpe(Head, firstAmount, SecondAmount, thirdAmount, fourthAmount, EXPE_CODE, AcdYear, Convert.ToInt32(Session["OrgId"]), IpAddress, UaNO, INSERT, gender, degreeno);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            count++;
            BindGridviewgrd_mca();
        }
        ViewState["Count"] = count;
    }
    private void SubmitDatarowHostel_mca(GridViewRow dRow)
    {
        int count = 0;
        TextBox txthHead_hostelmca = dRow.FindControl("txthHead_hostelmca") as TextBox;
        TextBox txth1styear_mca = dRow.FindControl("txth1styear_mca") as TextBox;
        TextBox txth2ndyear_mca = dRow.FindControl("txth2ndyear_mca") as TextBox;


        string Head = txthHead_hostelmca.Text;
        decimal firstAmount = txth1styear_mca.Text == string.Empty ? 0 : Convert.ToDecimal(txth1styear_mca.Text);
        decimal SecondAmount = txth2ndyear_mca.Text == string.Empty ? 0 : Convert.ToDecimal(txth2ndyear_mca.Text);
        decimal thirdAmount = Convert.ToDecimal(null);
        decimal fourthAmount = Convert.ToDecimal(null);
        string EXPE_CODE = rbdExpenditure.SelectedValue;
        int AcdYear = Convert.ToInt32(ddlAcadYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddldegreerc.SelectedValue);
        string IpAddress = ViewState["ipAddress"].ToString();
        int UaNO = Convert.ToInt32(Session["userno"]);
        int INSERT = Convert.ToInt32(ViewState["COUNT1"]);
        string gender = rbdstudentGender.SelectedValue;
        CustomStatus cs = (CustomStatus)objcerMasterController.InsertExpe(Head, firstAmount, SecondAmount, thirdAmount, fourthAmount, EXPE_CODE, AcdYear, Convert.ToInt32(Session["OrgId"]), IpAddress, UaNO, INSERT, gender, degreeno);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            count++;
            BindGridviewgrd_mca();
            // objCommon.DisplayMessage(UpdatePanel1, "Data Saved Successfully", this.Page);
        }
        ViewState["Count1"] = count;
        //else if (cs.Equals(CustomStatus.RecordExist))
        //{
        //    BindGridviewgrd();
        //    objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Hostel ", this.Page);
        //}
    }
    protected void btnConfirmHead_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                //int rowIndex=0;
                int COUNT = 0;
                if (ddlAcadYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.Page, "Select Academic Year ", this.Page);
                }
                else
                {
                    if (ddldegreerc.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Select Degree ", this.Page);
                    }
                    else if (ddldegreerc.SelectedValue == "1")
                    {

                        if (rbdExpenditure.SelectedValue == "")
                        {
                            objCommon.DisplayMessage(this.Page, "Select Approximate Expenditure", this.Page);
                        }
                        else
                        {
                            if (rbdExpenditure.SelectedValue == "A")
                            {
                                foreach (GridViewRow item in grdInstitute.Rows)
                                {
                                    ViewState["COUNT"] = COUNT++;
                                    SubmitDatarowInstitute(item);
                                    BindGridviewgrd();
                                }
                                if (Convert.ToInt32(ViewState["Count"]) > 0)
                                {
                                    objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                                    return;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Institute", this.Page);
                                    return;
                                }
                            }

                            else
                            {
                                // int count=0;
                                foreach (GridViewRow item in grdHostel.Rows)
                                {
                                    ViewState["COUNT1"] = COUNT++;
                                    SubmitDatarowHostel(item);
                                    BindGridviewgrd();
                                }
                                if (Convert.ToInt32(ViewState["Count1"]) > 0)
                                {
                                    objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                                    return;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Hostel", this.Page);
                                    return;
                                }
                            }
                        }
                    }
                    else if (ddldegreerc.SelectedValue == "4")
                    {

                        if (rbdExpenditure.SelectedValue == "")
                        {
                            objCommon.DisplayMessage(this.Page, "Select Approximate Expenditure", this.Page);
                        }
                        else
                        {
                            if (rbdExpenditure.SelectedValue == "A")
                            {
                                foreach (GridViewRow item in grdInstitute_mca.Rows)
                                {
                                    ViewState["COUNT"] = COUNT++;
                                    SubmitDatarowInstitute_mca(item);
                                    BindGridviewgrd_mca();
                                }
                                if (Convert.ToInt32(ViewState["Count"]) > 0)
                                {
                                    objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                                    return;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Institute", this.Page);
                                    return;
                                }
                            }

                            else
                            {
                                // int count=0;
                                foreach (GridViewRow item in grdHostel_mca.Rows)
                                {
                                    ViewState["COUNT1"] = COUNT++;
                                    SubmitDatarowHostel_mca(item);
                                    BindGridviewgrd_mca();
                                }
                                if (Convert.ToInt32(ViewState["Count1"]) > 0)
                                {
                                    objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                                    return;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Hostel", this.Page);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                int COUNT = 0;
                if (ddlAcadYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.Page, "Select Academic Year ", this.Page);
                }
                else
                {
                    if (rbdExpenditure.SelectedValue == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Select Approximate Expenditure", this.Page);
                    }
                    else
                    {
                        if (rbdExpenditure.SelectedValue == "A")
                        {
                            foreach (GridViewRow item in grdInstitute.Rows)
                            {
                                ViewState["COUNT"] = COUNT++;
                                SubmitDatarowInstitute(item);
                                BindGridviewgrd();
                            }
                            if (Convert.ToInt32(ViewState["Count"]) > 0)
                            {
                                objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                                return;
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Institute", this.Page);
                                return;
                            }
                        }

                        else
                        {
                            // int count=0;
                            foreach (GridViewRow item in grdHostel.Rows)
                            {
                                ViewState["COUNT1"] = COUNT++;
                                SubmitDatarowHostel(item);
                                BindGridviewgrd();
                            }
                            if (Convert.ToInt32(ViewState["Count1"]) > 0)
                            {
                                objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                                return;
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Hostel", this.Page);
                                return;
                            }
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.btnConfirmHead_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void SubmitDatarowInstitute(GridViewRow dRow)
    {
        int count=0;
       
        TextBox txtHead = dRow.FindControl("txtHead") as TextBox;
        TextBox txt1styear = dRow.FindControl("txt1styear") as TextBox;
        TextBox txt2ndyear = dRow.FindControl("txt2ndyear") as TextBox;
        TextBox txt3rdyear = dRow.FindControl("txt3rdyear") as TextBox;
        TextBox txt4thyear = dRow.FindControl("txt4thyear") as TextBox;
        string Head = txtHead.Text;

        decimal firstAmount = txt1styear.Text == string.Empty ? 0 : Convert.ToDecimal(txt1styear.Text);
        decimal SecondAmount = txt2ndyear.Text == string.Empty ? 0 : Convert.ToDecimal(txt2ndyear.Text);
        decimal thirdAmount = txt3rdyear.Text == string.Empty ? 0 : Convert.ToDecimal(txt3rdyear.Text);
        decimal fourthAmount = txt4thyear.Text == string.Empty ? 0 : Convert.ToDecimal(txt4thyear.Text);

        string EXPE_CODE = rbdExpenditure.SelectedValue;
        int AcdYear=Convert.ToInt32(ddlAcadYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddldegreerc.SelectedValue);
        string IpAddress = ViewState["ipAddress"].ToString();
        int UaNO = Convert.ToInt32(Session["userno"]);
        int INSERT = Convert.ToInt32(ViewState["COUNT"]);
        string gender = rbdstudentGender.SelectedValue;
        CustomStatus cs = (CustomStatus)objcerMasterController.InsertExpe(Head, firstAmount, SecondAmount, thirdAmount, fourthAmount, EXPE_CODE, AcdYear, Convert.ToInt32(Session["OrgId"]), IpAddress, UaNO, INSERT, gender,degreeno);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            count++;
            BindGridviewgrd();
        }
        ViewState["Count"] = count;
    }
    private void SubmitDatarowHostel(GridViewRow dRow)
    {
        int count=0;
        TextBox txthHead = dRow.FindControl("txthHead") as TextBox;
        TextBox txth1styear = dRow.FindControl("txth1styear") as TextBox;
        TextBox txth2ndyear = dRow.FindControl("txth2ndyear") as TextBox;
        TextBox txth3rdyear = dRow.FindControl("txth3rdyear") as TextBox;
        TextBox txth4thyear = dRow.FindControl("txth4thyear") as TextBox;

        string Head = txthHead.Text;
        decimal firstAmount = txth1styear.Text == string.Empty ? 0 : Convert.ToDecimal(txth1styear.Text);
        decimal SecondAmount = txth2ndyear.Text == string.Empty ? 0 : Convert.ToDecimal(txth2ndyear.Text);
        decimal thirdAmount = txth3rdyear.Text == string.Empty ? 0 : Convert.ToDecimal(txth3rdyear.Text);
        decimal fourthAmount = txth4thyear.Text == string.Empty ? 0 : Convert.ToDecimal(txth4thyear.Text);
        string EXPE_CODE = rbdExpenditure.SelectedValue;
        int AcdYear=Convert.ToInt32(ddlAcadYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddldegreerc.SelectedValue);
        string IpAddress = ViewState["ipAddress"].ToString();
        int UaNO = Convert.ToInt32(Session["userno"]);
        int INSERT = Convert.ToInt32(ViewState["COUNT1"]);
        string gender = rbdstudentGender.SelectedValue;
        CustomStatus cs = (CustomStatus)objcerMasterController.InsertExpe(Head, firstAmount, SecondAmount, thirdAmount, fourthAmount, EXPE_CODE, AcdYear, Convert.ToInt32(Session["OrgId"]), IpAddress, UaNO, INSERT, gender, degreeno);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            count++;
            BindGridviewgrd();
            // objCommon.DisplayMessage(UpdatePanel1, "Data Saved Successfully", this.Page);
        }
        ViewState["Count1"] = count;
        //else if (cs.Equals(CustomStatus.RecordExist))
        //{
        //    BindGridviewgrd();
        //    objCommon.DisplayMessage(this.Page, "Please Add Approximate Expenditure for Hostel ", this.Page);
        //}
    }
    protected void btnCancelHead_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Submit

    //added by pooja for mca degree on date 14-aug-2023
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done as per degree for RCpit Client(Tkt No.47196)

    #region GridviewgrdInstitute_mca
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done as per degree for RCpit Client(Tkt No.47196)
    private void SetInitialRow_mca()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        // dr["RowNumber"] = 1; dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("2nd_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["Particular"] = string.Empty;
        dr["1st_Year"] = string.Empty;
        dr["2nd_Year"] = string.Empty;
        dr["PARNO"] = 0;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        grdInstitute_mca.DataSource = dt;
        grdInstitute_mca.DataBind();
    }

    private void SetInitialRow1_mca()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

        dr = dt.NewRow();

        dr["Particular"] = string.Empty;
        dr["1st_Year"] = string.Empty;
        dr["2nd_Year"] = string.Empty;

        dt.Rows.Add(dr);

        grdInstitute_mca.DataSource = dt;
        grdInstitute_mca.DataBind();

    }

    private void AddNewRowToGrid1_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

                drCurrentRow = dtNewTable.NewRow();
                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Particular"] = string.Empty;
                drCurrentRow["1st_Year"] = string.Empty;
                drCurrentRow["2nd_Year"] = string.Empty;

                dtNewTable.Rows.Add(drCurrentRow);

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txtHead_mca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[1].FindControl("txtHead_mca");
                    TextBox txt1styearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[2].FindControl("txt1styearmca");
                    TextBox txt2ndyearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[3].FindControl("txt2ndyearmca");


                    if (txt2ndyearmca.Text.Trim() != string.Empty && txtHead_mca.Text.Trim() != string.Empty && txt2ndyearmca.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Particular"] = txtHead_mca.Text;
                        drCurrentRow["1st_Year"] = txt1styearmca.Text;
                        drCurrentRow["2nd_Year"] = txt2ndyearmca.Text;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }
                ViewState["CurrentTable"] = dtNewTable;
                grdInstitute_mca.DataSource = dtNewTable;
                grdInstitute_mca.DataBind();

                SetBindPreviousData_mca();
            }
            else
            {
                //objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void SetPreviousData_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtHead_mca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[1].FindControl("txtHead_mca");
                    TextBox txt1styearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[2].FindControl("txt1styearmca");
                    TextBox txt2ndyearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[3].FindControl("txt2ndyearmca");


                    txtHead_mca.Text = dt.Rows[i]["Particular"].ToString();
                    txt1styearmca.Text = dt.Rows[i]["1st_Year"].ToString();
                    txt2ndyearmca.Text = dt.Rows[i]["2nd_Year"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetBindPreviousData_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtHead_mca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[1].FindControl("txtHead_mca");
                    TextBox txt1styearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[2].FindControl("txt1styearmca");
                    TextBox txt2ndyearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[3].FindControl("txt2ndyearmca");


                    txtHead_mca.Text = dt.Rows[i]["Particular"].ToString();
                    txt1styearmca.Text = dt.Rows[i]["1st_Year"].ToString();
                    txt2ndyearmca.Text = dt.Rows[i]["2nd_Year"].ToString();

                    AddNewRowToGrid1_mca();
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToGrid_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

                dtNewTable.Columns.Add(new DataColumn("PARNO", typeof(int)));

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txtHead_mca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[1].FindControl("txtHead_mca");
                    TextBox txt1styearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[2].FindControl("txt1styearmca");
                    TextBox txt2ndyearmca = (TextBox)grdInstitute_mca.Rows[rowIndex].Cells[3].FindControl("txt2ndyearmca");

                   /// HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[6].FindControl("hdnId");

                    if (txt1styearmca.Text.Trim() != string.Empty && txtHead_mca.Text.Trim() != string.Empty && txt2ndyearmca.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Particular"] = txtHead_mca.Text;
                        drCurrentRow["1st_Year"] = txt1styearmca.Text;
                        drCurrentRow["2nd_Year"] = txt2ndyearmca.Text;


                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }
                drCurrentRow = dtNewTable.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Particular"] = string.Empty;
                drCurrentRow["1st_Year"] = string.Empty;
                drCurrentRow["2nd_Year"] = string.Empty;

                drCurrentRow["PARNO"] = 0;

                dtNewTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtNewTable;
                grdInstitute_mca.DataSource = dtNewTable;
                grdInstitute_mca.DataBind();

                SetBindPreviousData_mca();
            }
            else
            {
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    protected void btnAddmca_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid_mca();
    }

    protected void grdInstitute_mca_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int parno = Convert.ToInt32(e.CommandArgument);
            if (parno == 0)
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowID = gvRow.RowIndex;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 1)
                    {
                        if (gvRow.RowIndex <= dt.Rows.Count)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }
                    //Store the current data in ViewState for future reference
                    ViewState["CurrentTable"] = dt;
                    //Re bind the GridView for the updated data
                    grdInstitute_mca.DataSource = dt;
                    grdInstitute_mca.DataBind();

                    //Set Previous Data on Postbacks
                    SetPreviousData_mca();
                }
            }
            else
            {

                //====== Getting connection string defined in the web.config file. Pointed to the database we want to use.
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

                //======= Delete Query.
                string cmdText = "DELETE FROM ACD_ESTIMATE_CERT_PARTICULARS WHERE parno=@parno";

                //====== Providning information to SQL command object about which query to 
                //====== execute and from where to get database connection information.
                SqlCommand cmd = new SqlCommand(cmdText, con);

                //===== Adding parameters/Values.
                cmd.Parameters.AddWithValue("@parno", parno);

                //===== To check current state of the connection object. If it is closed open the connection
                //===== to execute the insert query.
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //===== Execute Query.
                cmd.ExecuteNonQuery();

                //===== close the connection.
                con.Close();

                //===== Bind data to FormView so that FormView will display updated data.
                //bindAllEmployees();
                BindGridviewgrd_mca();
            }
        }
    }

    #endregion

    #region GridviewgrdHostel_mca
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done as per degree for RCpit Client(Tkt No.47196)
    private void SetInitialRowH_mca()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        dt1.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

        dt1.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr1 = dt1.NewRow();
        dr1["Particular"] = string.Empty;
        dr1["1st_Year"] = string.Empty;
        dr1["2nd_Year"] = string.Empty;
        dr1["PARNO"] = 0;

        dt1.Rows.Add(dr1);
        ViewState["CurrentTableH"] = dt1;

        grdHostel_mca.DataSource = dt1;
        grdHostel_mca.DataBind();
    }
    private void SetInitialRowH1_mca()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        dt1.Columns.Add(new DataColumn("Particular", typeof(string)));
        dt1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
        dt1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

        dr1 = dt1.NewRow();

        dr1["Particular"] = string.Empty;
        dr1["1st_Year"] = string.Empty;
        dr1["2nd_Year"] = string.Empty;

        dt1.Rows.Add(dr1);
        grdHostel_mca.DataSource = dt1;
        grdHostel_mca.DataBind();

    }
    private void AddNewRowToGridH1_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTableH"];
            DataRow drCurrentRow1 = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                DataTable dtNewTable1 = new DataTable();
                dtNewTable1.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

                drCurrentRow1 = dtNewTable1.NewRow();
                drCurrentRow1["RowNumber"] = 1;
                drCurrentRow1["Particular"] = string.Empty;
                drCurrentRow1["1st_Year"] = string.Empty;
                drCurrentRow1["2nd_Year"] = string.Empty;

                dtNewTable1.Rows.Add(drCurrentRow1);

                for (int i = 0; i < dtCurrentTable1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txthHead_hostelmca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[1].FindControl("txthHead_hostelmca");
                    TextBox txth1styear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[2].FindControl("txth1styear_mca");
                    TextBox txth2ndyear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[3].FindControl("txth2ndyear_mca");


                    if (txth1styear_mca.Text.Trim() != string.Empty && txthHead_hostelmca.Text.Trim() != string.Empty && txth2ndyear_mca.Text.Trim() != string.Empty)
                    {
                        drCurrentRow1 = dtNewTable1.NewRow();

                        drCurrentRow1["RowNumber"] = i + 1;
                        drCurrentRow1["Particular"] = txthHead_hostelmca.Text;
                        drCurrentRow1["1st_Year"] = txth1styear_mca.Text;
                        drCurrentRow1["2nd_Year"] = txth2ndyear_mca.Text;


                        rowIndex++;
                        dtNewTable1.Rows.Add(drCurrentRow1);
                    }
                    else
                    {
                        return;
                    }
                }
                ViewState["CurrentTableH"] = dtNewTable1;
                grdHostel_mca.DataSource = dtNewTable1;
                grdHostel_mca.DataBind();

                SetBindPreviousDataH_mca();
            }
            else
            {
                //objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    private void SetPreviousDataH_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["CurrentTableH"];
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    TextBox txthHead_hostelmca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[1].FindControl("txthHead_hostelmca");
                    TextBox txth1styear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[2].FindControl("txth1styear_mca");
                    TextBox txth2ndyear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[3].FindControl("txth2ndyear_mca");


                    txthHead_hostelmca.Text = dt1.Rows[i]["Particular"].ToString();
                    txth1styear_mca.Text = dt1.Rows[i]["1st_Year"].ToString();
                    txth2ndyear_mca.Text = dt1.Rows[i]["2nd_Year"].ToString();

                    rowIndex++;
                }
            }
        }
    }
    private void SetBindPreviousDataH_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["CurrentTableH"];
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    TextBox txthHead_hostelmca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[1].FindControl("txthHead_hostelmca");
                    TextBox txth1styear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[2].FindControl("txth1styear_mca");
                    TextBox txth2ndyear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[3].FindControl("txth2ndyear_mca");

                    txthHead_hostelmca.Text = dt1.Rows[i]["Particular"].ToString();
                    txth1styear_mca.Text = dt1.Rows[i]["1st_Year"].ToString();
                    txth2ndyear_mca.Text = dt1.Rows[i]["2nd_Year"].ToString();

                    AddNewRowToGridH1_mca();
                    rowIndex++;
                }
            }
        }
    }
    private void AddNewRowToGridH_mca()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableH"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTableH"];
            DataRow drCurrentRow1 = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                DataTable dtNewTable1 = new DataTable();
                dtNewTable1.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("Particular", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("1st_Year", typeof(string)));
                dtNewTable1.Columns.Add(new DataColumn("2nd_Year", typeof(string)));

                dtNewTable1.Columns.Add(new DataColumn("PARNO", typeof(int)));
                for (int i = 0; i < dtCurrentTable1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txthHead_hostelmca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[1].FindControl("txthHead_hostelmca");
                    TextBox txth1styear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[2].FindControl("txth1styear_mca");
                    TextBox txth2ndyear_mca = (TextBox)grdHostel_mca.Rows[rowIndex].Cells[3].FindControl("txth2ndyear_mca");

                    if (txth1styear_mca.Text.Trim() != string.Empty && txthHead_hostelmca.Text.Trim() != string.Empty && txth2ndyear_mca.Text.Trim() != string.Empty)
                    {
                        drCurrentRow1 = dtNewTable1.NewRow();

                        drCurrentRow1["RowNumber"] = i + 1;
                        drCurrentRow1["Particular"] = txthHead_hostelmca.Text;
                        drCurrentRow1["1st_Year"] = txth1styear_mca.Text;
                        drCurrentRow1["2nd_Year"] = txth2ndyear_mca.Text;


                        rowIndex++;
                        dtNewTable1.Rows.Add(drCurrentRow1);
                    }
                    else
                    {
                        return;
                    }
                }
                drCurrentRow1 = dtNewTable1.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow1["Particular"] = string.Empty;
                drCurrentRow1["1st_Year"] = string.Empty;
                drCurrentRow1["2nd_Year"] = string.Empty;

                drCurrentRow1["PARNO"] = 0;

                dtNewTable1.Rows.Add(drCurrentRow1);
                ViewState["CurrentTableH"] = dtNewTable1;
                grdHostel_mca.DataSource = dtNewTable1;
                grdHostel_mca.DataBind();

                SetBindPreviousDataH_mca();
            }
            else
            {
                //objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    protected void btnhAdd_mca_Click(object sender, EventArgs e)
    {
        AddNewRowToGridH_mca();

    }
    protected void grdHostel_mca_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            //--- Get primarry key value of the selected record.
            int parno = Convert.ToInt32(e.CommandArgument);
            if (parno == 0)
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowID = gvRow.RowIndex;
                if (ViewState["CurrentTableH"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTableH"];
                    if (dt.Rows.Count > 1)
                    {
                        if (gvRow.RowIndex <= dt.Rows.Count)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }
                    //Store the current data in ViewState for future reference
                    ViewState["CurrentTableH"] = dt;
                    //Re bind the GridView for the updated data
                    grdHostel_mca.DataSource = dt;
                    grdHostel_mca.DataBind();

                    //Set Previous Data on Postbacks
                    SetPreviousDataH_mca();
                }
            }
            else
            {

                //====== Getting connection string defined in the web.config file. Pointed to the database we want to use.
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

                //======= Delete Query.
                string cmdText = "DELETE FROM ACD_ESTIMATE_CERT_PARTICULARS WHERE parno=@parno";

                //====== Providning information to SQL command object about which query to 
                //====== execute and from where to get database connection information.
                SqlCommand cmd = new SqlCommand(cmdText, con);

                //===== Adding parameters/Values.
                cmd.Parameters.AddWithValue("@parno", parno);

                //===== To check current state of the connection object. If it is closed open the connection
                //===== to execute the insert query.
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //===== Execute Query.
                cmd.ExecuteNonQuery();

                //===== close the connection.
                con.Close();

                //===== Bind data to FormView so that FormView will display updated data.
                //bindAllEmployees();
                BindGridviewgrd_mca();
            }
        }
    }


    #endregion

    #region BindGridviewgrd_mca
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done as per degree for RCpit Client(Tkt No.47196)
    private void BindGridviewgrd_mca()
    {

        try
        {
         DataSet dslist = objcerMasterController.GetParticularinfo(Convert.ToInt32(ddlAcadYear.SelectedValue), rbdExpenditure.SelectedValue, rbdstudentGender.SelectedValue, Convert.ToInt32(ddldegreerc.SelectedValue));
              
            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = dslist.Tables[0];
                if (rbdExpenditure.SelectedValue == "A")
                {
                    SetInitialRow_mca();
                    ViewState["CurrentTable"] = dt1;
                    pnlStudInstitute_mca.Visible = true;
                    pnlStudHostel_mca.Visible = false;
                    grdInstitute_mca.Visible = true;
                    grdInstitute_mca.DataSource = dslist;
                    grdInstitute_mca.DataBind();
                    int cc=grdInstitute_mca.Rows.Count;
                }
                else if (rbdExpenditure.SelectedValue == "H")
                {
                    SetInitialRowH_mca();
                    ViewState["CurrentTableH"] = dt1;
                    pnlStudHostel_mca.Visible = true;
                    pnlStudInstitute_mca.Visible = false;
                    grdHostel_mca.Visible = true;
                    grdHostel_mca.DataSource = dslist;
                    grdHostel_mca.DataBind();
                    int cc=grdHostel_mca.Rows.Count;
                }

            }
            else
            {
                if (rbdExpenditure.SelectedValue == "A")
                {
                    SetInitialRow_mca();
                    pnlStudInstitute_mca.Visible = true;
                    pnlStudHostel_mca.Visible = false;
                    grdInstitute_mca.Visible = true;
                }
                else if (rbdExpenditure.SelectedValue == "H")
                {
                    SetInitialRowH_mca();
                    pnlStudInstitute_mca.Visible = false;
                    pnlStudHostel_mca.Visible = true;
                    grdHostel_mca.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.btnConfirmHead_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }
    #endregion

    #region DDL
    //RFC.Enhancement.Major.2 (24-08-2023) Changes done as per degree for RCpit Client(Tkt No.47196)
    protected void rbdstudentGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rbdExpenditure.SelectedValue == "A")
                {
                    if (ddldegreerc.SelectedValue == "1")
                    {
                        rbdExpenditure.SelectedIndex = -1;
                        divgender.Visible = false;
                        rbdstudentGender.SelectedIndex = -1;
                    }
                    else if (ddldegreerc.SelectedValue == "4")
                    {

                        rbdExpenditure.SelectedIndex = -1;
                        divgender.Visible = false;
                        rbdstudentGender.SelectedIndex = -1;
                    }
                }
                else if (rbdExpenditure.SelectedValue == "H")
                {
                    if (ddldegreerc.SelectedValue == "1")
                    {
                        //rbdExpenditure.SelectedIndex = -1;
                        //rbdstudentGender.SelectedIndex = -1;
                        divgender.Visible = true;
                        BindGridviewgrd();
                        Panelgrid.Visible = true;
                        pnlStudInstitute.Visible = false;
                        pnlStudHostel.Visible = true;
                        Panelgrid_mca.Visible = false;
                        pnlStudInstitute_mca.Visible = false;
                        pnlStudHostel_mca.Visible = false;
                    }
                    else if (ddldegreerc.SelectedValue == "4")
                    {

                        //rbdExpenditure.SelectedIndex = -1;
                        //rbdstudentGender.SelectedIndex = -1;
                        divgender.Visible = true;

                        BindGridviewgrd_mca();
                        Panelgrid.Visible = false;
                        pnlStudInstitute.Visible = false;
                        pnlStudHostel.Visible = false;
                        Panelgrid_mca.Visible = true;
                        pnlStudInstitute_mca.Visible = false;
                        pnlStudHostel_mca.Visible = true;

                    }
                }
            }
            else
            {
                BindGridviewgrd();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.btnConfirmHead_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rbdExpenditure_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {

                if (ddlAcadYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Select Academic Year", this.Page);
                    rbdExpenditure.SelectedIndex = -1;
                }
                else
                {


                    if (ddldegreerc.SelectedValue == "1")
                    {

                        if (rbdExpenditure.SelectedValue == "A")
                        {

                            divgender.Visible = false;
                            rbdstudentGender.SelectedIndex = -1;
                            pnlStudHostel.Visible = false;
                            BindGridviewgrd();
                            Panelgrid.Visible = true;
                            pnlStudInstitute.Visible = true;
                            pnlStudHostel.Visible = false;
                            Panelgrid_mca.Visible = false;
                            pnlStudInstitute_mca.Visible = false;
                            pnlStudHostel_mca.Visible = false;

                        }
                        else if (rbdExpenditure.SelectedValue == "H")
                        {

                            rbdstudentGender.SelectedIndex = -1;
                            divgender.Visible = true;
                            pnlStudInstitute.Visible = false;


                        }
                    }
                    else if (ddldegreerc.SelectedValue == "4")
                    {

                        if (rbdExpenditure.SelectedValue == "A")
                        {

                            divgender.Visible = false;
                            rbdstudentGender.SelectedIndex = -1;
                            BindGridviewgrd_mca();
                            Panelgrid.Visible = false;
                            pnlStudInstitute.Visible = false;
                            pnlStudHostel.Visible = false;
                            Panelgrid_mca.Visible = true;
                            pnlStudInstitute_mca.Visible = true;
                            pnlStudHostel_mca.Visible = false;

                        }
                        else if (rbdExpenditure.SelectedValue == "H")
                        {

                            rbdstudentGender.SelectedIndex = -1;
                            divgender.Visible = true;
                            pnlStudInstitute_mca.Visible = false;                  
                        }
                    }

                }
            }
            else
            {
                if (ddlAcadYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Select Academic Year", this.Page);
                    rbdExpenditure.SelectedIndex = -1;
                }
                else
                {
                    if (rbdExpenditure.SelectedValue == "A")
                    {

                        divgender.Visible = false;
                        rbdstudentGender.SelectedIndex = -1;
                        pnlStudHostel.Visible = false;
                        BindGridviewgrd();
                        Panelgrid.Visible = true;
                        pnlStudInstitute.Visible = true;
                        pnlStudHostel.Visible = false;
                        Panelgrid_mca.Visible = false;
                        pnlStudInstitute_mca.Visible = false;
                        pnlStudHostel_mca.Visible = false;
                    }
                    else if (rbdExpenditure.SelectedValue == "H")
                    {
                        rbdstudentGender.SelectedIndex = -1;
                        divgender.Visible = true;
                        pnlStudInstitute.Visible = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.btnConfirmHead_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddldegreerc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldegreerc.SelectedIndex > 0)
        {
            if (rbdExpenditure.SelectedValue == "A")
            {
                if (ddldegreerc.SelectedValue == "1")
                {
                    rbdExpenditure.SelectedIndex = -1;
                    divgender.Visible = false;
                    rbdstudentGender.SelectedIndex = -1;
                    pnlStudInstitute.Visible = true;
                    pnlStudInstitute_mca.Visible = false;
                }
                else if (ddldegreerc.SelectedValue == "4")
                {

                    rbdExpenditure.SelectedIndex = -1;
                    divgender.Visible = false;
                    rbdstudentGender.SelectedIndex = -1;
                    pnlStudInstitute.Visible = false;
                    pnlStudInstitute_mca.Visible = true;
                }
            }
            else if (rbdExpenditure.SelectedValue == "H")
            {
                if (ddldegreerc.SelectedValue == "1")
                {
                    rbdExpenditure.SelectedIndex = -1;
                    rbdstudentGender.SelectedIndex = -1;
                    divgender.Visible = true;
                    pnlStudInstitute.Visible = false;
                    pnlStudHostel.Visible = true;
                    pnlStudHostel_mca.Visible = false;
                }
                else if (ddldegreerc.SelectedValue == "4")
                {

                    rbdExpenditure.SelectedIndex = -1;
                    rbdstudentGender.SelectedIndex = -1;
                    divgender.Visible = true;
                    pnlStudInstitute_mca.Visible = false;
                    pnlStudHostel.Visible = false;
                    pnlStudHostel_mca.Visible = true;
                }
            }
        }
        else
        {
            rbdExpenditure.SelectedIndex = -1;
            divgender.Visible = false;
            rbdstudentGender.SelectedIndex = -1;
            pnlStudInstitute.Visible = false;
            pnlStudInstitute_mca.Visible = false;
            divgender.Visible = false;
            pnlStudInstitute.Visible = false;
            pnlStudHostel.Visible = false;
            pnlStudHostel_mca.Visible = false;
        }

    }
    protected void ddlAcadYear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlAcadYear.SelectedIndex > 0)
        {
            ddldegreerc.SelectedIndex = 0;
            rbdExpenditure.SelectedIndex = -1;
            divgender.Visible = false;
            rbdstudentGender.SelectedIndex = -1;
            pnlStudInstitute.Visible = false;
            pnlStudInstitute_mca.Visible = false;
            divgender.Visible = false;
            pnlStudInstitute.Visible = false;
            pnlStudHostel.Visible = false;
            pnlStudHostel_mca.Visible = false;

        }
        else
        {
            ddldegreerc.SelectedIndex = 0;
            rbdExpenditure.SelectedIndex = -1;
            divgender.Visible = false;
            rbdstudentGender.SelectedIndex = -1;
            pnlStudInstitute.Visible = false;
            pnlStudInstitute_mca.Visible = false;
            divgender.Visible = false;
            pnlStudInstitute.Visible = false;
            pnlStudHostel.Visible = false;
            pnlStudHostel_mca.Visible = false;
        }
    }
    #endregion

    #endregion

}



