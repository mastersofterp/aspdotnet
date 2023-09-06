//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Laboratory Test)     
// CREATION DATE : 13-APR-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Web.UI.HtmlControls;


public partial class Health_LaboratoryTest_TestObservation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HealthTransactionController objHelTranTransaction = new HealthTransactionController();
    LabMaster objLab = new LabMaster();
    LabController objLabController = new LabController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
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
                   
                   // objCommon.FillDropDownList(ddlTitle, "HEALTH_TEST_TITLE", "TITLENO", "TITLE", "", "TITLENO");
                    ViewState["action"] = "add";
                    BindlistView();

                    if (Request.QueryString["id"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        string Name = Request.QueryString["Name"];
                        if (Request.QueryString["Name"] == "EmployeeCode" || Request.QueryString["Name"] == "EmployeeName")
                        {
                            hfPatientName.Value = id.ToString();
                            FillEmployeeInfo(id);
                        }
                        if (Request.QueryString["Name"] == "StudentName" || Request.QueryString["Name"] == "StudentRegNo")
                        {
                            hfPatientName.Value = id.ToString();
                            FillStudentInfo(id);
                        }
                        if (Request.QueryString["Name"] == "Other")
                        {
                            hfPatientName.Value = id.ToString();
                            FillOtherPatientInfo(id);
                        }
                    }
                    txtSampleDt.Text = Convert.ToString(System.DateTime.Now);
                    txtDueDt.Text = Convert.ToString(System.DateTime.Now);
                }
            }
            else
            {
                if (Page.Request.Params["__EVENTTARGET"] != null)
                {
                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                    {
                        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                        bindlist(arg[0], arg[1]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEmployeeInfo(int idno)
    {
        try
        {           
            DataSet ds = objLabController.GetPatientDetailsByPID(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
            }
            objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE IN ('E','D') AND TEST_GIVEN=1 AND PID=" + idno, "OPDID DESC");

            if (ddlVisitDate.Items.Count <= 1)
            {
                ddlTitle.Enabled = false;
            }
            else
            {
                ddlTitle.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.FillEmployeeInfo -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

    private void FillStudentInfo(int idno)
    {
        try
        {           
            DataSet ds = objLabController.GetStudentInfo(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();   
                txtPatientName.Focus();                
            }
            objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE ='S' AND TEST_GIVEN=1 AND PID=" + idno, "OPDID DESC");
            if (ddlVisitDate.Items.Count <= 1)
            {
                ddlTitle.Enabled = false;
            }
            else
            {
                ddlTitle.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.FillStudentInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillOtherPatientInfo(int idno)
    {
        try
        {
            HelMasterController objHelMaster = new HelMasterController();
            DataSet ds = objHelMaster.GetOtherPatientInfo(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                txtPatientName.Text = ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["PID"].ToString();    
                txtPatientName.Focus();               
            }
            objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE ='O' AND TEST_GIVEN=1 AND PID=" + idno, "OPDID DESC");
            if (ddlVisitDate.Items.Count <= 1)
            {
                ddlTitle.Enabled = false;
            }
            else
            {
                ddlTitle.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void UnBindList()
    {
        try
        {
            DataTable dt = null;
            lvEmp.DataSource = dt;
            lvEmp.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestTitle.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void bindlist(string category, string searchtext)
    {
        try
        {
            ViewState["PCAT"] = category;
            DataTable dt = objHelTranTransaction.RetrievePatientDetails(searchtext, category);
            lvEmp.DataSource = dt;
            lvEmp.DataBind();
            if (category == "StudentName" || category == "StudentRegNo")
            {
                HtmlTableRow trHeader = ((HtmlTableRow)lvEmp.FindControl("trHeader"));
                trHeader.Cells[1].InnerText = "RegNo";
                trHeader.Cells[2].InnerText = "Branch";
                trHeader.Cells[3].InnerText = "Degree";
            }
            else
            {
                HtmlTableRow trHeader = ((HtmlTableRow)lvEmp.FindControl("trHeader"));
                trHeader.Cells[1].InnerText = "Employee Code";
                trHeader.Cells[2].InnerText = " Department";
                trHeader.Cells[3].InnerText = "Designation";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();
        Response.Redirect(url + "&id=" + lnk.CommandArgument + "&Name=" + ViewState["PCAT"].ToString());
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {            
            objLab.PATIENT_ID = Convert.ToInt32(hfPatientName.Value);            
            objLab.OPDID = Convert.ToInt32(ddlVisitDate.SelectedValue);
            objLab.TITLENO = Convert.ToInt32(ddlTitle.SelectedValue);
            objLab.TEST_SAMPLE_DT = Convert.ToDateTime(txtSampleDt.Text);
            objLab.TEST_DUE_DT = Convert.ToDateTime(txtDueDt.Text);
            objLab.COMMON_REMARK = txtCRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtCRemark.Text.Trim());
            objLab.COLLEGE_CODE = Session["colcode"].ToString();
           

            DataTable ObservationTbl = new DataTable("obserTbl");
            ObservationTbl.Columns.Add("CONTENTNO", typeof(int));
            ObservationTbl.Columns.Add("SRNO", typeof(int));
            ObservationTbl.Columns.Add("PATIENT_VALUE", typeof(string));
            ObservationTbl.Columns.Add("REMARK", typeof(string));

            DataRow dr = null;
            foreach (ListViewItem i in lvContent.Items)
            {                
                HiddenField HdnContentNo = (HiddenField)i.FindControl("hdnContentNo");
                Label LblSRNO = (Label)i.FindControl("lblSrNo");
                TextBox txtPvalue = (TextBox)i.FindControl("txtPatientValue");
                TextBox txtRemark = (TextBox)i.FindControl("txtRemark");

                //if (txtPvalue.Text.ToString() != string.Empty)
                //{
                    dr = ObservationTbl.NewRow();
                    dr["CONTENTNO"] = HdnContentNo.Value;
                    dr["SRNO"] = LblSRNO.Text;
                    dr["PATIENT_VALUE"] = txtPvalue.Text;
                    dr["REMARK"] = txtRemark.Text;

                    ObservationTbl.Rows.Add(dr);
                //}   
            }
            objLab.OBSERVATION_TRAN = ObservationTbl;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objLabController.AddUpdateObservation(objLab);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                        BindlistView();
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);                        
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {                       
                        ViewState["action"] = "add";
                        Clear();
                        BindlistView();
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["OBSERNO"] != null)
                    {
                       objLab.OBSERNO = Convert.ToInt32(ViewState["OBSERNO"].ToString());
                        CustomStatus cs = (CustomStatus)objLabController.AddUpdateObservation(objLab);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Clear();
                            BindlistView();
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {                           
                            ViewState["action"] = "add";
                            BindlistView();
                            Clear();
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);                           
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }    
    
    private void Clear()
    {
        ddlVisitDate.SelectedIndex = 0;
        ddlTitle.SelectedIndex = 0;
        txtSampleDt.Text = Convert.ToString(System.DateTime.Now);
        txtDueDt.Text = Convert.ToString(System.DateTime.Now);
        lblRefBy.Text = string.Empty;
        ViewState["OBSERNO"] = null;
        ViewState["action"] = "add";
        lvContent.DataSource = null;
        lvContent.DataBind();
        lvContent.Visible = false;
        txtCRemark.Text = string.Empty;
        txtPatientName.Text = string.Empty;
        ddlTitle.Enabled = true;
    }

   

    
    protected void ddlVisitDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string DoctorName = string.Empty;
            if (ddlVisitDate.SelectedIndex > 0)
            {
                lvContent.Visible = false;
                objCommon.FillDropDownList(ddlTitle, "HEALTH_PATIENT_LAB_TEST PL INNER JOIN HEALTH_TEST_TITLE TT ON  (PL.TITLENO = TT.TITLENO)", "PL.TITLENO", "TT.TITLE", "PL.OPDID=" + ddlVisitDate.SelectedValue, "PL.TITLENO");
                DoctorName = objCommon.LookUp("HEALTH_PATIENT_DETAILS P INNER JOIN HEALTH_DOCTORMASTER D ON (P.DRID = D.DRID)", "D.DRNAME", "P.OPDID=" + ddlVisitDate.SelectedValue);
                lblRefBy.Text = DoctorName;
                DataSet ds = objCommon.FillDropDown("HEALTH_PATIENT_DETAILS", "PATIENT_CODE", "DEPENDENT_ID", "OPDID=" + Convert.ToInt32(ddlVisitDate.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int DependentID = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPENDENT_ID"].ToString());
                    if (DependentID != 0)
                    {
                        DataSet dsD = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "FNNO", "RELATION + ' - ' + MEMNAME AS NAME", "FNNO=" + DependentID, "");
                        if (dsD.Tables[0].Rows.Count > 0)
                        {
                            lblDeptName.Text = dsD.Tables[0].Rows[0]["NAME"].ToString();
                            trDependent.Visible = true;
                        }
                        else
                        {
                            trDependent.Visible = false;
                        }
                    }
                    else
                    {
                        trDependent.Visible = false;
                    }
                }
              
                ddlVisitDate.Focus();
                //txtSampleDt.Focus();
            }
            else
            {
                lblRefBy.Text = string.Empty;
                DoctorName = string.Empty;
                txtSampleDt.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.ddlVisitDate_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTitle.SelectedIndex > 0)
            {
                DataSet dsRec = objLabController.GetTestContentToObserve(Convert.ToInt32(ddlTitle.SelectedValue), Convert.ToInt32(ddlVisitDate.SelectedValue));
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    lvContent.DataSource = dsRec.Tables[0];
                    lvContent.DataBind();
                    ddlTitle.Focus();
                    lvContent.Visible = true;
                }
                else
                {
                    lvContent.DataSource = null;
                    lvContent.DataBind();
                    lvContent.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Title.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.ddlTitle_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindlistView()
    {
        try
        {           
            DataSet ds = objCommon.FillDropDown("HEALTH_TEST_OBSERVATION O INNER JOIN HEALTH_TEST_TITLE T ON (O.TITLENO = T.TITLENO)", "O.OBSERNO, O.TEST_SAMPLE_DT, O.TEST_DUE_DT, O.COMMON_REMARK, O.PATIENT_NAME", "T.TITLE", "" , "O.OBSERNO DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvObservation.DataSource = ds;
                lvObservation.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int OBSERNO = int.Parse(btnEdit.CommandArgument);
            ViewState["OBSERNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(OBSERNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int OBSERNO)
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("HEALTH_TEST_OBSERVATION O INNER JOIN PAYROLL_EMPMAS E ON (E.IDNO = O.PATIENT_ID)", "O.OBSERNO, O.TEST_SAMPLE_DT, O.TEST_DUE_DT, O.COMMON_REMARK, O.TITLENO, O.OPDID", "E.IDNO, ISNULL(E.TITLE,'')+' '+ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'')  AS PATIENT_NAME", "O.OBSERNO=" + OBSERNO, "O.OBSERNO");
            DataSet ds = objCommon.FillDropDown("HEALTH_TEST_OBSERVATION O", "O.OBSERNO, O.TEST_SAMPLE_DT, O.TEST_DUE_DT, O.COMMON_REMARK, O.TITLENO, O.OPDID", "O.PATIENT_ID, O.PATIENT_NAME, O.PATIENT_CODE", "O.OBSERNO=" + OBSERNO, "O.OBSERNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSampleDt.Text = ds.Tables[0].Rows[0]["TEST_SAMPLE_DT"].ToString();
                txtDueDt.Text = ds.Tables[0].Rows[0]["TEST_DUE_DT"].ToString();
                txtCRemark.Text = ds.Tables[0].Rows[0]["COMMON_REMARK"].ToString();
                txtPatientName.Text = ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
              
                int IDNO = Convert.ToInt32(ds.Tables[0].Rows[0]["PATIENT_ID"].ToString());
                string P_CODE = ds.Tables[0].Rows[0]["PATIENT_CODE"].ToString();
                if (P_CODE == "E")
                {
                    P_CODE = "'E','D'";
                }
                else if (P_CODE == "S")
                {
                    P_CODE = "'S'";
                }
                else if (P_CODE == "O")
                {
                    P_CODE = "'O'";
                }
                hfPatientName.Value = IDNO.ToString();
                objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PID=" + IDNO + " AND PATIENT_CODE IN (" + P_CODE + ")", "OPDID DESC");
                ddlVisitDate.SelectedValue = ds.Tables[0].Rows[0]["OPDID"].ToString();

                objCommon.FillDropDownList(ddlTitle, "HEALTH_PATIENT_LAB_TEST PL INNER JOIN HEALTH_TEST_TITLE TT ON  (PL.TITLENO = TT.TITLENO)", "PL.TITLENO", "TT.TITLE", "PL.OPDID=" + ddlVisitDate.SelectedValue, "PL.TITLENO");

                ddlTitle.SelectedValue = ds.Tables[0].Rows[0]["TITLENO"].ToString();

                DataSet dsRec = objLabController.GetTestObservation(OBSERNO);
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    lvContent.DataSource = dsRec.Tables[0];
                    lvContent.DataBind();
                    lvContent.Visible = true;
                }
                else
                {
                    lvContent.DataSource = null;
                    lvContent.DataBind();
                    lvContent.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindlistView();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["OBSERNO"] = int.Parse(btnPrint.CommandArgument);
            ShowReport("TestObservation", "rptTestObservationReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_OBSERNO=" + Convert.ToInt32(ViewState["OBSERNO"]);
            ScriptManager.RegisterClientScriptBlock(updActivity, updActivity.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCanceModal_Click(object sender, EventArgs e)
    {
        rbPatient.SelectedValue = "EmployeeCode";

        DataTable dt = null;
        lvEmp.DataSource = dt;
        lvEmp.DataBind();
    }
   
}