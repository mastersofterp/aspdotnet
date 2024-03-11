//===============================================//
// MODULE NAME   : RFC ERP Portal (RFC Common Code)
// PAGE NAME     : Estimate Certificate for DAIICT
// CREATION DATE : 29-02-2024
// CREATED BY    : Sakshi Makwana
// Modified BY   : 
// Modified Date : 
// Version :- 
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

public partial class ACADEMIC_REPORTS_Estimate_Certificate_Master_RFCC : System.Web.UI.Page
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
            objCommon.FillDropDownList(ddlAdmyear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_NAME DESC");
            //Fill Dropdown admbatch
            objCommon.FillDropDownList(ddldegreerc, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ACTIVESTATUS=1", "DEGREENO");
            objCommon.FillDropDownList(ddlAcadYear,"ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1 ", "ACADEMIC_YEAR_NAME DESC");
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
                    //this.CheckPageAuthorization();

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
                Response.Redirect("~/notauthorized.aspx?page=Estimate_Certificate_Master_DAIICT.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Estimate_Certificate_Master_DAIICT.aspx");
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
            return;
        }
        else if (count > 1)
        {
            objCommon.DisplayMessage(this.updpnlExam2, "Please Select only one Student for issuing Certificate", this);
            return;
        }
        else
        {
            string PRINT = objCommon.LookUp("ACD_CERT_TRAN", "COUNT(PRINT_CERTIFICATE)", "CERT_NO=30 AND IDNO='" + GetStudentIDs() + "'");
            if (PRINT == "0")
            {
                objCommon.DisplayMessage(this.updpnlExam2, "Please Confirm Student for issuing Certificate", this);
                //HiddenItemBank();
                return;
            }
            else
            {

                ShowReport(GetStudentIDs(), "ESTIMATE_Certificate", "rptEstimateCertificate_Daiict.rpt");
                BindList();
            }
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            CertificateMasterController objcertMasterController = new CertificateMasterController();
            CertificateMaster objcertMaster = new CertificateMaster();
            string admbatch = objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + param);
            string year = objCommon.LookUp("ACD_STUDENT", "YEAR", "IDNO=" + param);

            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + param + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) +  ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue);
         
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

    protected void ddlAdmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudInstitute.Visible = false;
        pnlStudInstitute.Visible = false;
    }

    protected void ddldegreerc_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridviewgrd();
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

            return;
        }
        else if (count > 1)
        {
            objCommon.DisplayMessage(this.updpnlExam2, "Please Select only one Student for confirm and issuing Certificate", this);

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
                        //if (Convert.ToInt32(Session["OrgId"]) == 6) //RFC.Enhancement.Major.2 (01-09-2023)
                        //{
                        //    int Backno = Convert.ToInt32(ddlBank.SelectedValue);
                        //    string TypeofAcc = txttypeofAC.Text;
                        //    string AccNo = txtACNO.Text;
                        //    string Ifsc = txtIFSC.Text;
                        //    int mode = 0;
                        //    DataSet ds = objcerMasterController.AddBankEstimateCertificate(objcertMaster, Backno, TypeofAcc, AccNo, Ifsc, mode);
                        //}
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
        dt.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["Particular"] = string.Empty;
        dr["1st_Year"] = string.Empty;
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
        //  dt.Columns.Add(new DataColumn("PARNO", typeof(int)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["Particular"] = string.Empty;
        dr["1st_Year"] = string.Empty;
      
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
              
                //    dtNewTable.Columns.Add(new DataColumn("PARNO", typeof(int)));
                drCurrentRow = dtNewTable.NewRow();
                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Particular"] = string.Empty;
                drCurrentRow["1st_Year"] = string.Empty;
               
                //  drCurrentRow["PARNO"] = 0;
                dtNewTable.Rows.Add(drCurrentRow);

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txtHead = (TextBox)grdInstitute.Rows[rowIndex].Cells[1].FindControl("txtHead");
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txtamount");
                    //   HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[6].FindControl("hdnId");

                    if (txt1styear.Text.Trim() != string.Empty && txtHead.Text.Trim() != string.Empty )
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Particular"] = txtHead.Text;
                        drCurrentRow["1st_Year"] = txt1styear.Text;
                    
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
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txtamount");
               
                    //  HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[6].FindControl("hdnId");

                    txtHead.Text = dt.Rows[i]["Particular"].ToString();
                    txt1styear.Text = dt.Rows[i]["1st_Year"].ToString();
                
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
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txtamount");
         

                    txtHead.Text = dt.Rows[i]["Particular"].ToString();
                    txt1styear.Text = dt.Rows[i]["1st_Year"].ToString();
                  
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
                dtNewTable.Columns.Add(new DataColumn("PARNO", typeof(int)));

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox txtHead = (TextBox)grdInstitute.Rows[rowIndex].Cells[1].FindControl("txtHead");
                    TextBox txt1styear = (TextBox)grdInstitute.Rows[rowIndex].Cells[2].FindControl("txtamount");
                    HiddenField hdn = (HiddenField)grdInstitute.Rows[rowIndex].Cells[3].FindControl("hdnId");

                    if (txt1styear.Text.Trim() != string.Empty && txtHead.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Particular"] = txtHead.Text;
                        drCurrentRow["1st_Year"] = txt1styear.Text;
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

    #region BindGridviewgrd

    private void BindGridviewgrd()
    {

        try
         {

             DataSet dslist = objcerMasterController.GetParticularinfoForSem(Convert.ToInt32(ddlAcadYear.SelectedValue), Convert.ToInt32(ddldegreerc.SelectedValue));

            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = dslist.Tables[0];
             
                    SetInitialRow();
                    ViewState["CurrentTable"] = dt1;
                    Panelgrid.Visible = true;
                    pnlStudInstitute.Visible = true;
                    grdInstitute.Visible = true;
                    grdInstitute.DataSource = dslist;
                    grdInstitute.DataBind();
                    int cc=grdInstitute.Rows.Count;
            }
            else
            {
                    SetInitialRow();
                    Panelgrid.Visible = true;
                    pnlStudInstitute.Visible = true;

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

    private void SubmitDatarowInstitute(GridViewRow dRow)
    {
        int count = 0;

        TextBox txtHead_mca = dRow.FindControl("txtHead") as TextBox;
        TextBox txt1styearmca = dRow.FindControl("txtamount") as TextBox;
        decimal thirdAmount = Convert.ToDecimal(null);
        decimal fourthAmount = Convert.ToDecimal(null);
        string Head = txtHead_mca.Text;

        decimal firstAmount = txt1styearmca.Text == string.Empty ? 0 : Convert.ToDecimal(txt1styearmca.Text);
       

        int AcdYear = Convert.ToInt32(ddlAcadYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddldegreerc.SelectedValue);
        string IpAddress = ViewState["ipAddress"].ToString();
        int UaNO = Convert.ToInt32(Session["userno"]);
        int INSERT = Convert.ToInt32(ViewState["COUNT"]);

        int cs = objcerMasterController.InsertExpeForSem(Head, firstAmount, AcdYear, Convert.ToInt32(Session["OrgId"]), IpAddress, UaNO, INSERT, degreeno);
        if (cs==1)
        {
            count++;

        }
        ViewState["Count"] = count;
    }

    protected void btnConfirmHead_Click(object sender, EventArgs e)
    {
        try
        {
           
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
                    else 
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
                    }
                 
                              
            }
        catch(Exception ex)
        {

        }
    }
           
  
    protected void btnCancelHead_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion Submit

    #endregion

}