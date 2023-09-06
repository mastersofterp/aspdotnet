//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 15-FEB-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

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

using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Web.Caching;
using System.IO;
using System.Drawing;
using System.Configuration;
using IITMS.NITPRM;


public partial class Health_Transaction_PatientDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HealthTransactionController objHelTranTransaction = new HealthTransactionController();
    HealthTransactions objHelTran = new HealthTransactions();

    #region Page Events
    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["userno"],Session["username"],   
    /// </summary>
    /// 
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

                    objCommon.FillDropDownList(ddlDosage, "HEALTH_DOSAGEMASTER", "DNO", "DNAME", "DNO<>0", "DNAME");
                    objCommon.FillDropDownList(ddlBloodGrp, "HEALTH_BLOODGROUP", "BLOODGRNO", "BLOODGR", "BLOODGRNO<>0", "BLOODGR");
                    txtEnterDate.Text = Convert.ToString(System.DateTime.Now);
                    txtEnterTime.Text = DateTime.Now.ToString("hh:mm:ss tt");      //Convert.ToString(System.DateTime.Now.ToLongTimeString());  //DateTime.Now.ToString("hh:mm:ss tt")        
                    Session["Carta"] = null;
                    Session["RecTbl"] = null;
                    Session["RecContentTbl"] = null;
                    ViewState["SRNO"] = null;
                    trPatientHist.Visible = false;
                    this.ClearControls();
                    this.ClearPresc();

                    ViewState["PCAT"] = null;
                    lblEmployeeCode.Text = string.Empty;
                    lblEmp.Visible = false;
                    lblDot.Visible = false;
                    ViewState["action"] = "add";

                    if (Convert.ToInt32(Session["usertype"]) == 1)  // admin
                    {
                        objCommon.FillDropDownList(ddlDoctor, "HEALTH_DOCTORMASTER D LEFT JOIN PAYROLL_EMPMAS E ON (D.EMP_IDNO = E.IDNO)", "D.DRID", "isnull(E.PFILENO,'') + ' - ' +D.DRNAME AS DRNAME ", "D.STATUS='Y'", "D.DRID");
                        ddlDoctor.AppendDataBoundItems = true;
                    }
                    else if (Convert.ToInt32(Session["usertype"]) == 5) // doctor
                    {
                        objCommon.FillDropDownList(ddlDoctor, "HEALTH_DOCTORMASTER D LEFT JOIN PAYROLL_EMPMAS E ON (D.EMP_IDNO = E.IDNO)", "D.DRID", "isnull(E.PFILENO,'')+ ' - ' +D.DRNAME AS DRNAME ", "D.EMP_IDNO=" + Convert.ToInt32(Session["idno"]), "D.DRID");

                        DataSet ds = objCommon.FillDropDown("HEALTH_DOCTORMASTER D LEFT JOIN PAYROLL_EMPMAS E ON (D.EMP_IDNO = E.IDNO)", "D.DRID", "isnull(E.PFILENO,'')+ ' - ' +D.DRNAME AS DRNAME ", "D.EMP_IDNO=" + Convert.ToInt32(Session["idno"]), "D.DRID");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDoctor.SelectedValue = ds.Tables[0].Rows[0]["DRID"].ToString();
                            // btnPrescription.Visible = true;
                        }
                    }
                    objCommon.FillDropDownList(ddlTitle, "HEALTH_TEST_TITLE", "TITLENO", "TITLE", "", "TITLENO");

                    if (Request.QueryString["id"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        string Name = Request.QueryString["Name"];
                        if (Request.QueryString["Name"] == "EmployeeCode" || Request.QueryString["Name"] == "EmployeeName")
                        {
                            hfPatientName.Value = string.Empty;
                            lblPatientCat.Text = string.Empty;
                            FillEmployeeInfo(id);
                            trDependent.Visible = true;
                            lblEmp.Visible = true;
                            lblDot.Visible = true;
                            trPatientHist.Visible = true;
                        }
                        else
                        {
                            trDependent.Visible = false;
                        }
                        if (Request.QueryString["Name"] == "StudentName" || Request.QueryString["Name"] == "StudentRegNo")
                        {
                            hfPatientName.Value = string.Empty;
                            lblPatientCat.Text = string.Empty;
                            trDependent.Visible = false;
                            FillStudentInfo(id);
                            lblEmp.Visible = false;
                            lblDot.Visible = false;
                            trPatientHist.Visible = true;
                        }
                        if (Request.QueryString["Name"] == "Other")
                        {
                            hfPatientName.Value = id.ToString();
                            lblPatientCat.Text = string.Empty;
                            trDependent.Visible = false;
                            lblEmp.Visible = false;
                            lblDot.Visible = false;
                            FillOtherPatientInfo(id);
                            trPatientHist.Visible = true;
                        }
                    }
                    else
                    {
                        btnPrescription.Visible = true;
                        //this.ModalPopupExtender2.Hide();
                        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
                    }
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
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                // cmd.CommandText = "select INO, cast(INO as nvarchar) + '------------*' +  INAME AS INAME from HEALTH_ITEMMASTER where " + "INAME like @SearchText + '%'";
                cmd.CommandText = "select ITEM_NO, ITEM_NAME AS ITEM_NAME from HEALTH_ITEM where ITEM_NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["ITEM_NO"].ToString() + "-------------*" + sdr["ITEM_NAME"].ToString());
                    }
                }
                conn.Close();
                return ItemsName;

            }
        }
    }


    /// <summary>
    /// Page_PreInit event calls SetMasterPage() method.   
    /// </summary>
    /// 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
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
            // divdemo2.visible = true;
            if (dt.Rows.Count > 0)
            {
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
            else
            {
                MessageBox("Record Not Found.");
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divdemo2').modal('show');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
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
        //Response.Redirect(url + "&id=" + lnk.CommandArgument + "&Name=" + ViewState["PCAT"].ToString() + "&serno=" + lnk.CommandName);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "Close();", true);
    }

    private void FillEmployeeInfo(int idno)
    {
        try
        {
            HelMasterController objHelMaster = new HelMasterController();
            DataSet ds = objHelMaster.GetPatientDetailsByPID(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                imgEmpPhoto.ImageUrl = "";
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblEmployeeCode.Text = ds.Tables[0].Rows[0]["PFILENO"].ToString();
                ddlBloodGrp.SelectedValue = ds.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                string Gender = ds.Tables[0].Rows[0]["SEX"].ToString();
                if (Gender.Trim() == "M")
                {
                    rdbMale.Checked = true;
                    rdbFemale.Checked = false;
                }
                else //if ((ds.Tables[0].Rows[0]["SEX"].ToString() == "F"))
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;
                }
                txtAge.Text = ds.Tables[0].Rows[0]["AGE"].ToString();
                txtHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                // txtBP.Focus();
                txtPatientName.Focus();
                lblPatientCat.Text = "E";
                this.BindListView(Convert.ToInt32(hfPatientName.Value), lblPatientCat.Text);
                if (Convert.ToInt32(Session["usertype"]) == 5) // doctor
                {
                    btnPrescription.Visible = true;
                }

                imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + ds.Tables[0].Rows[0]["IDNO"].ToString() + "&type=emp";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.FillEmployeeInfo -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillStudentInfo(int idno)
    {
        try
        {
            HelMasterController objHelMaster = new HelMasterController();
            DataSet ds = objHelMaster.GetStudentInfo(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                imgEmpPhoto.ImageUrl = "";
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                ddlBloodGrp.SelectedValue = ds.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                //lblEmployeeCode.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                if ((ds.Tables[0].Rows[0]["SEX"].ToString() == "M"))
                {
                    rdbMale.Checked = true;
                    rdbFemale.Checked = false;
                }
                else if ((ds.Tables[0].Rows[0]["SEX"].ToString() == "F"))
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;
                }
                txtAge.Text = ds.Tables[0].Rows[0]["AGE"].ToString();
                txtHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                //txtBP.Focus();
                txtPatientName.Focus();
                lblPatientCat.Text = "S";
                this.BindListView(Convert.ToInt32(hfPatientName.Value), lblPatientCat.Text);
                if (Convert.ToInt32(Session["usertype"]) == 5) // doctor
                {
                    btnPrescription.Visible = true;
                }

                imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + ds.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.FillEmployeeInfo -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
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
                if ((ds.Tables[0].Rows[0]["SEX"].ToString() == "M"))
                {
                    rdbMale.Checked = true;
                    rdbFemale.Checked = false;
                }
                else if ((ds.Tables[0].Rows[0]["SEX"].ToString() == "F"))
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;
                }
                txtAge.Text = ds.Tables[0].Rows[0]["AGE"].ToString();
                txtHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                //txtBP.Focus();
                txtPatientName.Focus();
                lblPatientCat.Text = "O";
                this.BindListView(Convert.ToInt32(hfPatientName.Value), lblPatientCat.Text);
                if (Convert.ToInt32(Session["usertype"]) == 5) // doctor
                {
                    btnPrescription.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.FillEmployeeInfo -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Actions

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        try
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            objHelTran.RPID = 0; // Convert.ToInt32(objCommon.LookUp("HEALTH_PATIENTMASTER", "RPID", "PID='" + hfPatientName.Value + "'"));
            if (hfPatientName.Value == string.Empty)
            {
                string pidOtherUser = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "(ISNULL(MAX(PID),0))+1", "PATIENT_CODE='O'");
                objHelTran.PID = Convert.ToInt32(pidOtherUser);
                lblPatientCat.Text = "O";
            }
            else
            {
                objHelTran.PID = Convert.ToInt32(hfPatientName.Value);
            }

            ViewState["PID"] = objHelTran.PID;

            objHelTran.DRID = Convert.ToInt32(ddlDoctor.SelectedValue);
            objHelTran.OPDDATE = Convert.ToDateTime(txtEnterDate.Text);
            objHelTran.OPDTIME = Convert.ToDateTime(txtEnterTime.Text);
            objHelTran.COMPLAINT = txtComplaints.Text == string.Empty ? "" : txtComplaints.Text;
            objHelTran.CHIEF_COMPLAINT = txtChiefComplaints.Text == string.Empty ? "" : txtChiefComplaints.Text;
            objHelTran.FINDING = txtFindings.Text == string.Empty ? "" : txtFindings.Text;  // Present history 
            objHelTran.PAST_HISTORY = txtPastHistory.Text == string.Empty ? "" : txtPastHistory.Text;
            objHelTran.FAMILY_HISTORY = txtFamilyHistory.Text == string.Empty ? "" : txtFamilyHistory.Text;
            objHelTran.CHRONIC_DIESEASE = txtChronicDiesease.Text == string.Empty ? "" : txtChronicDiesease.Text;
            objHelTran.DIAGNOSIS = txtDiagnosis.Text == string.Empty ? "" : txtDiagnosis.Text;
            objHelTran.SURGICAL_PROCEDURE = txtSurgicalProc.Text == string.Empty ? "" : txtSurgicalProc.Text;
            objHelTran.INSTRUCTION = txtInstructions.Text == string.Empty ? "" : txtInstructions.Text;
            objHelTran.REFERRED_TO = txtReferred.Text == string.Empty ? "" : txtReferred.Text;
            objHelTran.ADMITTED_STATUS = Convert.ToInt32(rdbAdmittedStatus.SelectedValue);


            objHelTran.REMARK = "--";
            objHelTran.HEIGHT = txtHeight.Text;
            objHelTran.WEIGHT = txtWeight.Text == string.Empty ? "" : txtWeight.Text;
            // objHelTran.AGE = Convert.ToInt32(txtAge.Text);
            objHelTran.AGE = txtAge.Text == string.Empty ? "" : txtAge.Text;
            objHelTran.BLOOD_GROUP = Convert.ToInt32(ddlBloodGrp.SelectedValue);
            if (rdbFemale.Checked == true)
            {
                objHelTran.SEX = 'F';
            }
            else if (rdbMale.Checked == true)
            {
                objHelTran.SEX = 'M';
            }
            objHelTran.BP = txtBP.Text;
            if (txtTemprature.Text != "")
            {
                objHelTran.TEMP = Convert.ToDecimal(txtTemprature.Text);
            }
            else
            {
                objHelTran.TEMP = Convert.ToDecimal(0);
            }
            objHelTran.PULSE = txtPulseRate.Text;
            objHelTran.RESP = txtRespiration.Text;
            objHelTran.RANDOM_SUGAR_LEVEL = txtRandomSugar.Text;
            objHelTran.IP_ADDRESS = IP;
            objHelTran.MAC_ADDRESS = "";
            objHelTran.COLLEGE_CODE = Session["colcode"].ToString();

            string[] pNAME = txtPatientName.Text.Split('*');
            string[] dNAME = ddlDependent.SelectedItem.Text.Split('-');
            if (lblPatientCat.Text == "E")
            {
                if (rdbPCList.SelectedValue == "0")
                {
                    objHelTran.DEPENDENTID = 0;
                    objHelTran.PATIENT_CODE = 'E';
                    //objHelTran.PATIENT_NAME = pNAME[1];
                    if (pNAME.Length == 1)
                    {
                        objHelTran.PATIENT_NAME = pNAME[0];
                    }
                    else
                    {
                        objHelTran.PATIENT_NAME = pNAME[1];
                    }
                }
                else
                {
                    objHelTran.DEPENDENTID = Convert.ToInt32(ddlDependent.SelectedValue);
                    objHelTran.PATIENT_CODE = 'D';
                    objHelTran.PATIENT_NAME = dNAME[1];
                }
                //objHelTran.PATIENT_NAME = pNAME[1];
            }
            else if (lblPatientCat.Text == "S")
            {
                objHelTran.PATIENT_CODE = 'S';
                objHelTran.PATIENT_NAME = pNAME[1];
            }
            else
            {
                objHelTran.PATIENT_CODE = 'O';
                objHelTran.REFERENCE_BY = txtReference.Text;
                objHelTran.PATIENT_NAME = pNAME[0];
            }

            ViewState["PATIENT_CODE"] = objHelTran.PATIENT_CODE;

            string opdNo = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "(ISNULL(MAX(OPDID),0))+1", string.Empty);
            objHelTran.OPDNO = Convert.ToInt32(opdNo);


            if (chkLab.Checked == true)
            {
                if (lvTest.Items.Count > 0)
                {
                    objHelTran.TEST_GIVEN = 1;
                }
                else
                {
                    //objCommon.DisplayMessage(this.updOpdTransaction, "Please Select Laboratory Test.", this.Page);
                    MessageBox("Please Select Laboratory Test.");
                    return;
                }
            }
            else
            {
                objHelTran.TEST_GIVEN = 0;
            }
            if (chkCerti.Checked == true)
            {
                objHelTran.ISSUE_CERTIFICATES = 1;
            }
            else
            {
                objHelTran.ISSUE_CERTIFICATES = 0;
            }

            DataTable dtTEST;
            dtTEST = (DataTable)Session["RecTbl"];
            objHelTran.TEST_TITLE = dtTEST;

            DataTable dtCONTENT;
            dtCONTENT = (DataTable)Session["RecContentTbl"];
            objHelTran.TITLE_CONTENTS = dtCONTENT;





            if (Session["Carta"] != null)
            {
                DataTable dt = (DataTable)Session["Carta"];
                if (dt.Rows.Count != 0)
                {
                    CustomStatus cs1 = (CustomStatus.TransactionFailed);
                    int count = Convert.ToInt32(SecondGrid.Rows.Count);

                    int rowscount = SecondGrid.Rows.Count;
                    int columnscount = SecondGrid.Columns.Count;

                    DataTable dtMedicine = new DataTable();

                    dtMedicine.Columns.Add("ADMNO", typeof(int));
                    dtMedicine.Columns.Add("INO", typeof(int));
                    dtMedicine.Columns.Add("ITEMNAME", typeof(string));
                    dtMedicine.Columns.Add("QTY", typeof(string));
                    dtMedicine.Columns.Add("DOSES", typeof(string));
                    dtMedicine.Columns.Add("NOOFDAYS", typeof(int));
                    dtMedicine.Columns.Add("SPINST", typeof(string));
                    dtMedicine.Columns.Add("PRESCRIPTION_STATUS", typeof(string));

                    DataRow dr;
                    foreach (GridViewRow row in SecondGrid.Rows)
                    {
                        HiddenField hdnsrno = (HiddenField)row.FindControl("hdnSRNO");
                        HiddenField hdnIno = (HiddenField)row.FindControl("hdnINO");
                        HiddenField hdnDno = (HiddenField)row.FindControl("hdnDosesID");
                        HiddenField hdnPno = (HiddenField)row.FindControl("hdnPStatusID");

                        dr = dtMedicine.NewRow();
                        dr["ADMNO"] = Convert.ToInt32(hdnsrno.Value); //Convert.ToInt32(((HiddenField)SecondGrid.Rows[i].Cells[8].FindControl("hdnSRNO")).Value);
                        dr["INO"] = Convert.ToInt32(hdnIno.Value); // Convert.ToInt32(((HiddenField)SecondGrid.Rows[i].Cells[1].FindControl("hdnINO")).Value);
                        dr["ITEMNAME"] = row.Cells[2].Text; //SecondGrid.Rows[i].Cells[2].Text;
                        dr["QTY"] = row.Cells[6].Text;  //Convert.ToString(SecondGrid.Rows[i].Cells[6].Text);
                        dr["DOSES"] = Convert.ToInt32(hdnDno.Value); //((HiddenField)SecondGrid.Rows[i].Cells[5].FindControl("hdnDosesID")).Value;
                        dr["NOOFDAYS"] = row.Cells[3].Text;  //Convert.ToInt32(SecondGrid.Rows[i].Cells[3].Text);
                        dr["SPINST"] = row.Cells[7].Text.Replace("&nbsp;", ""); //SecondGrid.Rows[i].Cells[7].Text.Replace("&nbsp;", "");
                        dr["PRESCRIPTION_STATUS"] = Convert.ToInt32(hdnPno.Value); // Convert.ToInt32(((HiddenField)SecondGrid.Rows[i].Cells[10].FindControl("hdnPStatusID")).Value);

                        dtMedicine.Rows.Add(dr);
                    }

                    objHelTran.MEDICINE_PRES = dtMedicine;
                }
            }

            if (ViewState["action"].ToString() == "add")
            {
                CustomStatus cs = (CustomStatus)objHelTranTransaction.AddHelOPDINSERT(objHelTran);

                if (Session["Carta"] != null)
                {
                    DataTable dt = (DataTable)Session["Carta"];
                    if (dt.Rows.Count != 0)
                    {
                        CustomStatus cs1 = (CustomStatus.TransactionFailed);
                        int count = Convert.ToInt32(SecondGrid.Rows.Count);
                        cs1 = (CustomStatus)objHelTranTransaction.AddHelPrescriptionInsert(objHelTran);
                        if (cs.Equals(CustomStatus.RecordSaved) && cs1.Equals(CustomStatus.RecordSaved))
                        {
                            this.DeleteCart();
                            //objCommon.DisplayMessage(updOpdTransaction, "Record Saved Successfully.", this);
                            MessageBox("Record Save Successfully.");
                            this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                        }
                    }
                    else if (dt.Rows.Count == 0 && cs.Equals(CustomStatus.RecordSaved))
                    {
                        //objCommon.DisplayMessage(updOpdTransaction, "Prescriptions are Not Added & OPD Record Saved Successfully", this);
                        MessageBox("Prescriptions are Not Added & OPD Record Saved Successfully");
                    }
                }
                if (Session["Carta"] == null)
                {
                    //objCommon.DisplayMessage(updOpdTransaction, "Record Saved Successfully.", this);
                    MessageBox("Record Save Successfully.");
                    this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                }
            }
            else // when edit existing record
            {
                objHelTran.OPDNO = Convert.ToInt32(ViewState["opdID"]);
                CustomStatus cs = (CustomStatus)objHelTranTransaction.AddHelOPDUPDATE(objHelTran);

                if (Session["Carta"] != null)
                {
                    DataTable dt = (DataTable)Session["Carta"];
                    //if (dt.Rows.Count != 0)
                    //{
                    CustomStatus cs1 = (CustomStatus.TransactionFailed);
                    int count = Convert.ToInt32(SecondGrid.Rows.Count);
                    cs1 = (CustomStatus)objHelTranTransaction.AddHelPrescriptionInsert(objHelTran);

                    if (cs.Equals(CustomStatus.RecordSaved) && cs1.Equals(CustomStatus.RecordSaved))
                    {
                        this.DeleteCart();
                        //objCommon.DisplayMessage(updOpdTransaction, "Record Updated Successfully.", this);
                        MessageBox("Record Updated Successfully.");
                        this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                    }
                    //}
                    //else if (dt.Rows.Count == 0 && cs.Equals(CustomStatus.RecordSaved))
                    //{
                    //    objCommon.DisplayMessage(updOpdTransaction, "Prescriptions are Not Added & OPD Record Saved Successfully", this);
                    //}
                }
                if (Session["Carta"] == null)
                {
                    //objCommon.DisplayMessage(updOpdTransaction, "Record Save Successfully.", this);
                    MessageBox("Record Save Successfully.");
                    this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                }
            }

            this.ClearPresc();
            ClearControls();
            this.ClearTest();
            ViewState["action"] = "add";
            //this.ModalPopupExtender2.Hide();
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        hfDoctorsName.Value = null;
        hfPatientName.Value = null;
        Session["Carta"] = null;
        this.ClearControls();
        this.ClearPresc();
        this.ClearTest();
    }
    protected void btnConfirm_OnClick(object sender, EventArgs e)
    {
        DataTable dt;
        dt = ((DataTable)Session["Carta"]);
        this.BindListView_Second(dt);
        //this.ModalPopupExtender2.Hide();
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        pnlSGrid.Visible = true;
    }

    protected void btnPrescription_OnClick(object sender, EventArgs e)
    {

        try
        {
            if (hfPatientName.Value == "")
            {
                //this.ModalPopupExtender2.Hide();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
                //objCommon.DisplayMessage(updOpdTransaction, "Please Select Doctor.", this);
                MessageBox("Please Select Doctor.");
                return;
            }
            if (ddlDoctor.SelectedIndex == 0)
            {
                //this.ModalPopupExtender2.Hide();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
                //objCommon.DisplayMessage(updOpdTransaction, "Please Select Doctor.", this);
                MessageBox("Please Select Doctor.");
                return;
            }

            if (ddlDoctor.SelectedIndex > 0)
            {
                this.ClearPresc();
                //ModalPopupExtender2.TargetControlID = "btnPrescription";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('show');", true);
            }
            else
            {
               // objCommon.DisplayMessage(updOpdTransaction, "Please Select Patient and Doctor.", this);
                MessageBox("Please Select Patient and Doctor.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objCommon.DisplayUserMessage(Page,"Health_Transaction_PatientDetails.btnPrescription_OnClick()-->" + ex.Message + " " + ex.StackTrace, this);

            else

                objCommon.DisplayUserMessage(Page, "Server Unavailable.", this);
        }
    }

    protected void btnAddPrescription_OnClick(object sender, EventArgs e)
    {
        try
        {
            int unique = 0;

            DataSet ds = objHelTranTransaction.GetInsufficientStockDetails(Convert.ToInt32(hfItemName.Value));

            if (Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(ds.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString()) && rdbYes.Checked == true)
            {
                //objCommon.DisplayMessage(this.updOpdTransaction, "No sufficient stock", this.Page);
                MessageBox("No sufficient stock");
                //lblMsg.Text = "No sufficient stock";                   
                //this.ModalPopupExtender2.Show();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('show');", true);
                return;
            }
            else if (rdbNo.Checked == true && Convert.ToInt32(txtQuantity.Text) < Convert.ToInt32(ds.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString()) || rdbYes.Checked == true && Convert.ToInt32(txtQuantity.Text) < Convert.ToInt32(ds.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString()))
            {

                if (gvPrisc.Rows.Count > 0)
                {
                    btnConfirm.Visible = true;
                    btnCancelP.Visible = true;
                }
                else
                {
                    btnConfirm.Visible = true;
                    btnCancelP.Visible = true;
                }

                DataTable dt;
                if (Session["Carta"] != null && ((DataTable)Session["Carta"]) != null)
                {

                    dt = ((DataTable)Session["Carta"]);
                    DataRow dr = dt.NewRow();
                    if (dr != null)
                    {
                        unique = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UNIQUE"]));
                    }
                    dr["UNIQUE"] = unique + 1;
                    dr["ITEMNAME"] = txtItemName.Text;
                    dr["INO"] = hfItemName.Value;
                    dr["QTY"] = txtQuantity.Text;
                    dr["DOSES_ID"] = ddlDosage.SelectedItem.Text;
                    dr["DOSES"] = ddlDosage.SelectedValue;
                    dr["SPINST"] = txtSpecialInstructions.Text;
                    dr["NOOFDAYS"] = txtNumberofDays.Text;
                    if (rdbYes.Checked == true)
                    {
                        dr["PRESCRIPTION_STATUS"] = "Yes";
                        dr["PRESCRIPTION_STATUS_ID"] = 1;

                    }
                    else
                    {
                        dr["PRESCRIPTION_STATUS"] = "No";
                        dr["PRESCRIPTION_STATUS_ID"] = 0;
                    }
                    dt.Rows.Add(dr);
                    Session["Carta"] = dt;
                    this.BindListView_Details(dt);
                }
                else
                {
                    dt = this.GetDataTable();
                    DataRow dr = dt.NewRow();
                    dr["UNIQUE"] = unique + 1;
                    dr["ITEMNAME"] = txtItemName.Text;
                    dr["INO"] = hfItemName.Value;
                    dr["QTY"] = txtQuantity.Text;
                    dr["DOSES_ID"] = ddlDosage.SelectedItem.Text;
                    dr["DOSES"] = ddlDosage.SelectedValue;
                    dr["SPINST"] = txtSpecialInstructions.Text;
                    dr["NOOFDAYS"] = txtNumberofDays.Text;
                    if (rdbYes.Checked == true)
                    {
                        dr["PRESCRIPTION_STATUS"] = "Yes";
                        dr["PRESCRIPTION_STATUS_ID"] = 1;
                    }
                    else
                    {
                        dr["PRESCRIPTION_STATUS"] = "No";
                        dr["PRESCRIPTION_STATUS_ID"] = 0;
                    }
                    dt.Rows.Add(dr);
                    Session.Add("Carta", dt);
                    this.BindListView_Details(dt);
                }
                this.ClearPresc();
                txtItemName.Focus();
            }
            else
            {

                if (gvPrisc.Rows.Count > 0)
                {
                    btnConfirm.Visible = true;
                    btnCancelP.Visible = true;
                }
                else
                {
                    btnConfirm.Visible = true;
                    btnCancelP.Visible = true;
                }

                DataTable dt;
                if (Session["Carta"] != null && ((DataTable)Session["Carta"]) != null)
                {

                    dt = ((DataTable)Session["Carta"]);
                    DataRow dr = dt.NewRow();
                    if (dr != null)
                    {
                        unique = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UNIQUE"]));
                    }
                    dr["UNIQUE"] = unique + 1;
                    dr["ITEMNAME"] = txtItemName.Text;
                    dr["INO"] = hfItemName.Value;
                    dr["QTY"] = txtQuantity.Text;
                    dr["DOSES_ID"] = ddlDosage.SelectedItem.Text;
                    dr["DOSES"] = ddlDosage.SelectedValue;
                    dr["SPINST"] = txtSpecialInstructions.Text;
                    dr["NOOFDAYS"] = txtNumberofDays.Text;
                    if (rdbYes.Checked == true)
                    {
                        dr["PRESCRIPTION_STATUS"] = "Yes";
                        dr["PRESCRIPTION_STATUS_ID"] = 1;

                    }
                    else
                    {
                        dr["PRESCRIPTION_STATUS"] = "No";
                        dr["PRESCRIPTION_STATUS_ID"] = 0;
                    }
                    dt.Rows.Add(dr);
                    Session["Carta"] = dt;
                    this.BindListView_Details(dt);
                }
                else
                {
                    dt = this.GetDataTable();
                    DataRow dr = dt.NewRow();
                    dr["UNIQUE"] = unique + 1;
                    dr["ITEMNAME"] = txtItemName.Text;
                    dr["INO"] = hfItemName.Value;
                    dr["QTY"] = txtQuantity.Text;
                    dr["DOSES_ID"] = ddlDosage.SelectedItem.Text;
                    dr["DOSES"] = ddlDosage.SelectedValue;
                    dr["SPINST"] = txtSpecialInstructions.Text;
                    dr["NOOFDAYS"] = txtNumberofDays.Text;
                    if (rdbYes.Checked == true)
                    {
                        dr["PRESCRIPTION_STATUS"] = "Yes";
                        dr["PRESCRIPTION_STATUS_ID"] = 1;
                    }
                    else
                    {
                        dr["PRESCRIPTION_STATUS"] = "No";
                        dr["PRESCRIPTION_STATUS_ID"] = 0;
                    }
                    dt.Rows.Add(dr);
                    Session.Add("Carta", dt);
                    this.BindListView_Details(dt);
                }
                this.ClearPresc();
                txtItemName.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnAddPrescription_OnClick -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            DataTable dt;
            if (Session["Carta"] != null && ((DataTable)Session["Carta"]) != null)
            {
                dt = ((DataTable)Session["Carta"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["Carta"] = dt;
                this.BindListView_Details(dt);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnDelete_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string IP = Request.ServerVariables["REMOTE_HOST"];
        // string MAC_ADDRESS = ""; 
        // ShowReport("Prescription", "rpt_Prescription.rpt", IP, MAC_ADDRESS, sender);  
        ShowReport("Prescription", "rptPrescriptionDetails.rpt", sender); //, IP, MAC_ADDRESS, sender);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnReport = sender as Button;
            ViewState["OBSERNO"] = int.Parse(btnReport.CommandArgument);
            ShowReport("TestObservation", "rptTestObservationReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.btnReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_LaboratoryTest_TestObservation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// CheckPageAuthorization() method checks whether the user is authorised to access this Page    
    /// </summary>
    /// 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }


    private void CalculateBMI()
    {


    }


    /// <summary>
    /// BindListView() method fetches all the records from HEL_DOCTORMASTER table with the help of GetDoctorByDRID() method.    
    /// </summary>
    /// 
    private void BindListView(int OPDID, string PatientCat)
    {
        try
        {
            DataSet ds = objHelTranTransaction.GetPatientDetailsByOPDID(OPDID, PatientCat);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvPatientHist.DataSource = ds.Tables[0];
                lvPatientHist.DataBind();
                trPatientHist.Visible = true;
            }
            else
            {
                lvPatientHist.DataSource = null;
                lvPatientHist.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListView_Details(DataTable dt)
    {
        try
        {
            gvPrisc.DataSource = dt;
            gvPrisc.DataBind();
            //this.ModalPopupExtender2.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('show');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.BindListView_Details() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void BindListView_Second(DataTable dt)
    {
        try
        {
            SecondGrid.DataSource = dt;
            SecondGrid.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.BindListView_Second() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ClearPresc()
    {
        txtItemName.Text = string.Empty;
        hfItemName.Value = null;
        txtNumberofDays.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        ddlDosage.SelectedIndex = 0;
        rdbYes.Checked = true;
        rdbNo.Checked = false;
        txtSpecialInstructions.Text = string.Empty;

    }

    private void ClearTest()
    {
        chkLab.Checked = false;
        trTest.Visible = false;
        lvTest.DataSource = null;
        lvTest.DataBind();
        lvTest.Visible = false;


    }

    private void DeleteCart()
    {
        DataTable dt = (DataTable)Session["Carta"];
        if (dt == null)
        {

        }
        else
        {
            dt.Rows.Clear();
        }
        this.BindListView_Details(dt);
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["UNIQUE"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("UNIQUE", typeof(string)));
        dt.Columns.Add(new DataColumn("OPDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEMNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("INO", typeof(int)));
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("PRESCDT", typeof(string)));
        dt.Columns.Add(new DataColumn("PRESCTIME", typeof(string)));
        dt.Columns.Add(new DataColumn("DOSES_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("DOSES", typeof(int)));
        dt.Columns.Add(new DataColumn("SPINST", typeof(string)));
        dt.Columns.Add(new DataColumn("PID", typeof(int)));
        dt.Columns.Add(new DataColumn("NOOFDAYS", typeof(string)));
        dt.Columns.Add(new DataColumn("PRESCRIPTION_STATUS", typeof(string)));
        dt.Columns.Add(new DataColumn("PRESCRIPTION_STATUS_ID", typeof(int)));
        return dt;
    }

    private void ClearControls()
    {
        txtComplaints.Text = string.Empty;
        txtFindings.Text = string.Empty;
        txtDiagnosis.Text = string.Empty;
        txtInstructions.Text = string.Empty;
        txtHeight.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtBP.Text = string.Empty;
        txtTemprature.Text = string.Empty;
        txtPulseRate.Text = string.Empty;
        txtRespiration.Text = string.Empty;
        txtRandomSugar.Text = string.Empty;
        if (Convert.ToInt32(Session["idno"]) == 0)
        {
            ddlDoctor.SelectedIndex = 0;
        }

        txtAge.Text = string.Empty;
        gvPrisc.DataSource = null;
        gvPrisc.DataBind();
        SecondGrid.DataSource = null;
        SecondGrid.DataBind();
        FirstDep.Visible = false;
        SecDep.Visible = false;
        ThiDep.Visible = false;
        ddlDependent.SelectedIndex = 0;
        rdbPCList.SelectedValue = "0";
        txtReference.Text = string.Empty;
        txtPatientName.Text = string.Empty;
        hfPatientName.Value = string.Empty;
        lblEmployeeCode.Text = string.Empty;
        Session["RecTbl"] = null;
        ViewState["SRNO"] = null;
        ddlBloodGrp.SelectedIndex = 0;
        pnlSGrid.Visible = false;
        //this.ModalPopupExtender2.Hide();
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        ViewState["action"] = "add";

        txtChiefComplaints.Text = string.Empty;
        txtPastHistory.Text = string.Empty;
        txtFamilyHistory.Text = string.Empty;
        txtChronicDiesease.Text = string.Empty;
        txtSurgicalProc.Text = string.Empty;
        txtReferred.Text = string.Empty;
        rdbAdmittedStatus.Text = string.Empty;
        imgEmpPhoto.ImageUrl = "";


    }

    private void ShowReport(string reportTitle, string rptFileName, object sender) //, string IPAddress, string MacAddress, object sender)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            int OPDID = int.Parse(btnPrint.CommandArgument);
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HEALTH")));
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_OPDID=" + OPDID; // +",username=" + Session["userfullname"].ToString() + ",IP_ADDRESS=" + IPAddress + ",MAC_ADDRESS=" + MacAddress;
            //ScriptManager.RegisterClientScriptBlock(updOpdTransaction, updOpdTransaction.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void rdbPCList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbPCList.SelectedValue == "1") // dependent
            {
                FirstDep.Visible = true;
                SecDep.Visible = false;
                ThiDep.Visible = true;
                objCommon.FillDropDownList(ddlDependent, "PAYROLL_SB_FAMILYINFO", "FNNO", "RELATION + '- '+ MEMNAME AS MEMNAME", "IDNO=" + hfPatientName.Value + "AND RELATION <> 'SELF'", "FNNO");
                txtAge.Text = string.Empty;
                txtHeight.Text = string.Empty;
                txtWeight.Text = string.Empty;
            }
            else  // employee
            {
                FirstDep.Visible = false;
                SecDep.Visible = false;
                ThiDep.Visible = false;
                ddlDependent.SelectedIndex = 0;
                txtAge.Text = string.Empty;
                txtHeight.Text = string.Empty;
                txtWeight.Text = string.Empty;
                FillEmployeeInfo(Convert.ToInt32(hfPatientName.Value));

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.rdbPCList_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlDependent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDependent.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "FNNO", "DATEDIFF(YEAR,  DOB, GETDATE()) AS AGE", "FNNO=" + ddlDependent.SelectedValue, "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAge.Text = ds.Tables[0].Rows[0]["AGE"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.ddlDependent_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDoctor.SelectedIndex > 0 && txtPatientName.Text != string.Empty)
            {
                btnPrescription.Visible = true;
                txtComplaints.Focus();
                btnSubmit.Visible = true;
            }
            else
            {
                btnPrescription.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnDelete_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCancelP_Click(object sender, EventArgs e)
    {
        ClearPresc();
        //this.ModalPopupExtender2.Show();
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('show');", true);
        gvPrisc.DataSource = null;
        gvPrisc.DataBind();
        Session["Carta"] = null;
    }


    #endregion

    protected void btnCloseP_Click(object sender, EventArgs e)
    {
        if (gvPrisc.Rows.Count > 0)
        {
            //this.ModalPopupExtender2.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('show');", true);
            //objCommon.DisplayMessage(updOpdTransaction, "Please save prescription details and then close.", this);
            MessageBox("Please save prescription details and then close.");
        }
        else
        {
            // this.ModalPopupExtender2.Hide();
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        }
    }

    protected void chkLab_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkLab.Checked == true)
            {
                trTest.Visible = true;
                ddlTitle.Focus();
            }
            else
            {
                trTest.Visible = false;
                lvTest.DataSource = null;
                lvTest.DataBind();
                lvTest.Visible = false;
                Session["RecTbl"] = null;
                ViewState["SRNO"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Transaction_PatientDetails.chkLab_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    // This method is used to create data table.
    private DataTable CreateTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("TITLENO", typeof(int)));
        dt.Columns.Add(new DataColumn("TEST_TITLE", typeof(string)));
        dt.Columns.Add(new DataColumn("OBSERNO", typeof(int)));
        return dt;
    }

    // This action button is used to add laboratory test
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bool chkBListChecked = false;
            foreach (ListItem chkItem in chkBList.Items)
            {
                if (chkItem.Selected)
                {
                    chkBListChecked = true;
                    break;
                }
            }

            if (chkBListChecked == true)
            {
                TestContentTable();
                lvTest.Visible = true;
                lvPanel.Visible = true;

                if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
                {

                    int maxVal = 0;
                    DataTable dt = (DataTable)Session["RecTbl"];
                    DataRow dr = dt.NewRow();
                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                    }
                    dr["SRNO"] = maxVal + 1;
                    if (CheckDuplicateTitle(dt, ddlTitle.SelectedItem.Text.Trim()))
                    {
                        //objCommon.DisplayMessage(this.updOpdTransaction, "This test already exist.", this.Page);
                        MessageBox("This test already exist.");
                        return;
                    }
                    //  dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dr["TITLENO"] = ddlTitle.SelectedValue;
                    dr["TEST_TITLE"] = ddlTitle.SelectedItem.Text;
                    dr["OBSERNO"] = 0;
                    dt.Rows.Add(dr);
                    Session["RecTbl"] = dt;
                    lvTest.DataSource = dt;
                    lvTest.DataBind();
                    lvTest.Visible = true;
                    ddlTitle.SelectedIndex = 0;
                    chkBList.Visible = false;

                    chkTest.Visible = false;
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                }
                else
                {
                    DataTable dt = this.CreateTabel();
                    DataRow dr = dt.NewRow();
                    dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dr["TITLENO"] = ddlTitle.SelectedValue;
                    dr["TEST_TITLE"] = ddlTitle.SelectedItem.Text;
                    dr["OBSERNO"] = 0;
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dt.Rows.Add(dr);
                    ddlTitle.SelectedIndex = 0;
                    chkBList.Visible = false;

                    chkTest.Visible = false;
                    Session["RecTbl"] = dt;
                    lvTest.DataSource = dt;
                    lvTest.DataBind();
                }
            }
            else
            {
                //objCommon.DisplayMessage(this.updOpdTransaction, "Please Select Test Contents.", this.Page);
                MessageBox("Please Select Test Contents.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckDuplicateTitle(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TEST_TITLE"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.CheckDuplicateStopName() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void btnDeleteRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];

                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));

                Session["RecTbl"] = dt;
                lvTest.DataSource = dt;
                lvTest.DataBind();
                lvTest.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnDeleteRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        //ListView item = (ListView)(sender as Control).NamingContainer;
        ImageButton btnDetails = sender as ImageButton;

        int opdID = int.Parse(btnDetails.CommandArgument);
        ViewState["opdID"] = int.Parse(btnDetails.CommandArgument);
        ShowDetails(opdID);
        ViewState["action"] = "edit";
        btnSubmit.Visible = false;
        //btnPrescription.Visible = false;       
        btnCancelP.Visible = true;
    }

    private void ShowDetails(int opdID)
    {
        try
        {

            DataSet ds = null;
            ds = objCommon.FillDropDown("HEALTH_PATIENT_DETAILS", "*", "", "OPDID=" + opdID, "OPDID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //txtPatientName.Text = ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["PID"].ToString();
                //lblEmployeeCode.Text = ds.Tables[0].Rows[0]["PATIENT_CODE"].ToString();
                lblPatientCat.Text = ds.Tables[0].Rows[0]["PATIENT_CODE"].ToString();
                if (lblPatientCat.Text == "D")
                {
                    rdbPCList.SelectedValue = "1";
                    objCommon.FillDropDownList(ddlDependent, "PAYROLL_SB_FAMILYINFO", "FNNO", "RELATION + '- '+ MEMNAME AS MEMNAME", "IDNO=" + hfPatientName.Value + "AND RELATION <> 'SELF' AND MEMFOR=2", "FNNO");
                    FirstDep.Visible = true;
                    SecDep.Visible = true;
                    ThiDep.Visible = true;
                }
                else
                {
                    rdbPCList.SelectedValue = "0";
                    FirstDep.Visible = false;
                    SecDep.Visible = false;
                    ThiDep.Visible = false;
                }
                ddlDependent.SelectedValue = ds.Tables[0].Rows[0]["DEPENDENT_ID"].ToString();
                txtAge.Text = ds.Tables[0].Rows[0]["AGE"].ToString();
                txtHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();

                if (ds.Tables[0].Rows[0]["SEX"].ToString() == "M")
                {
                    rdbMale.Checked = true;
                    rdbFemale.Checked = false;
                }
                else
                {
                    rdbFemale.Checked = true;
                    rdbMale.Checked = false;
                }

                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                txtReference.Text = ds.Tables[0].Rows[0]["REFERENCE_BY"].ToString();
                // objCommon.FillDropDownList(ddlDoctor, "HEALTH_DOCTORMASTER D LEFT JOIN PAYROLL_EMPMAS E ON (D.EMP_IDNO = E.IDNO)", "D.DRID", "E.PFILENO+ ' - ' +D.DRNAME AS DRNAME", "", "D.DRID");

                ddlDoctor.SelectedValue = ds.Tables[0].Rows[0]["DRID"].ToString();
                txtComplaints.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                txtFindings.Text = ds.Tables[0].Rows[0]["FINDING"].ToString();
                txtDiagnosis.Text = ds.Tables[0].Rows[0]["DIAGNOSIS"].ToString();
                txtInstructions.Text = ds.Tables[0].Rows[0]["INSTRUCTION"].ToString();
                txtBP.Text = ds.Tables[0].Rows[0]["BP"].ToString();
                txtTemprature.Text = ds.Tables[0].Rows[0]["TEMP"].ToString();
                txtPulseRate.Text = ds.Tables[0].Rows[0]["PULSE"].ToString();
                txtRespiration.Text = ds.Tables[0].Rows[0]["RESP"].ToString();
                ddlBloodGrp.SelectedValue = ds.Tables[0].Rows[0]["BLOOD_GROUP"].ToString();

                txtChiefComplaints.Text = ds.Tables[0].Rows[0]["CHIEF_COMPLAINT"].ToString();
                txtPastHistory.Text = ds.Tables[0].Rows[0]["PAST_HISTORY"].ToString();
                txtFamilyHistory.Text = ds.Tables[0].Rows[0]["FAMILY_HISTORY"].ToString();
                txtChronicDiesease.Text = ds.Tables[0].Rows[0]["CHRONIC_DIESEASE"].ToString();
                txtSurgicalProc.Text = ds.Tables[0].Rows[0]["SURGICAL_PROCEDURE"].ToString();
                txtReferred.Text = ds.Tables[0].Rows[0]["REFERRED_TO"].ToString();
                rdbAdmittedStatus.SelectedValue = ds.Tables[0].Rows[0]["ADMITTED_STATUS"].ToString();
                txtRandomSugar.Text = ds.Tables[0].Rows[0]["RANDOM_SUGAR_LEVEL"].ToString();


                if (ds.Tables[0].Rows[0]["TEST_GIVEN"].ToString() == "1")
                {
                    chkLab.Checked = true;

                    DataSet dsTest = objCommon.FillDropDown("HEALTH_PATIENT_LAB_TEST LT INNER JOIN HEALTH_TEST_TITLE TT ON (LT.TITLENO = TT.TITLENO) LEFT JOIN HEALTH_TEST_OBSERVATION O ON (LT.OPDID = O.OPDID AND LT.TITLENO = O.TITLENO)", "LT.SRNO, LT.TITLENO, TT.TITLE AS TEST_TITLE", "ISNULL(O.OBSERNO,0) AS OBSERNO", "LT.OPDID=" + opdID, "LT.OPDID");
                    if (Convert.ToInt32(dsTest.Tables[0].Rows.Count) > 0)
                    {
                        lvTest.DataSource = dsTest;
                        lvTest.DataBind();
                        if (dsTest.Tables[0].Rows.Count > 0)
                        {
                            Session["RecTbl"] = dsTest.Tables[0];
                        }

                        trTest.Visible = true;
                        trChkBox.Visible = true;
                        lvTest.Visible = true;
                        lvPanel.Visible = true;
                        //btnAdd.Enabled = false;
                        lvPanel.Enabled = true;
                    }

                    DataSet dsContent = objCommon.FillDropDown("HEALTH_PATIENT_TEST_CONTENT TC  INNER JOIN HEALTH_TEST_CONTENT C ON (TC.CONTENTNO = C.CONTENTNO)", "TC.TITLENO", "TC.CONTENTNO", "TC.OPDID = " + opdID, "TC.TITLENO, TC.CONTENTNO"); //C.CONTENT_NAME
                    if (Convert.ToInt32(dsContent.Tables[0].Rows.Count) > 0)
                    {
                        Session["RecContentTbl"] = dsContent.Tables[0];

                        //chkBList.DataSource = dsContent;
                        //chkBList.DataTextField = "CONTENT_NAME"; //dsContent.Tables[0].Rows[0]["CONTENT_NAME"].ToString();
                        //chkBList.DataValueField = "CONTENTNO";//dsContent.Tables[0].Rows[0]["CONTENTNO"].ToString();
                        //chkBList.DataBind();
                        //chkBList.Visible = true;
                        //chkBList.Enabled = false;
                    }
                }
                else
                {
                    chkLab.Checked = false;
                    btnAdd.Enabled = true;
                    trTest.Visible = false;

                    //trChkBox.Visible = false;     

                    chkTest.Visible = false;
                    chkBList.Visible = false;


                    lvPanel.Visible = false;
                    lvTest.DataSource = null;
                    lvTest.DataBind();
                    Session["RecTbl"] = null;
                }

                DataSet dsRec = objCommon.FillDropDown("HEALTH_PRESC P INNER JOIN HEALTH_DOSAGEMASTER D ON (P.DOSES = D.DNO)", "ADMNO AS [UNIQUE], INO, ITEMNAME + ' ' + (CASE WHEN STATUS_DEL = 1 THEN '(Suspended)' WHEN (STATUS_DEL = 0 AND ISSUE_STATUS = 1) THEN '(Issued)' ELSE '' END) AS ITEMNAME, D.DNAME AS DOSES_ID , NOOFDAYS, DOSES AS DOSES, QTY, SPINST", "PRESCRIPTION_STATUS as PRESCRIPTION_STATUS_ID , (CASE PRESCRIPTION_STATUS WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) as PRESCRIPTION_STATUS", "OPDNO=" + opdID, "OPDNO");
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    SecondGrid.DataSource = dsRec.Tables[0];
                    SecondGrid.DataBind();
                    pnlSGrid.Visible = true;

                    Session["Carta"] = dsRec.Tables[0];
                    gvPrisc.DataSource = dsRec.Tables[0];
                    gvPrisc.DataBind();
                }
                else
                {
                    SecondGrid.DataSource = null;
                    SecondGrid.DataBind();
                    pnlSGrid.Visible = false;

                    Session["Carta"] = null;
                    gvPrisc.DataSource = null;
                    gvPrisc.DataBind();
                }
            }

            DataSet dsIssue = objCommon.FillDropDown("HEALTH_PRESC P INNER JOIN HEALTH_DOSAGEMASTER D ON (P.DOSES = D.DNO)", "PRESCNO", "ISSUE_STATUS", "ISSUE_STATUS = 1 AND OPDNO=" + opdID, "OPDNO");
            if (Convert.ToInt32(dsIssue.Tables[0].Rows.Count) > 0)
            {
                btnSubmit.Visible = false;
                btnPrescription.Visible = false;
                pnlSGrid.Enabled = false;

                ddlTitle.Enabled = false;
                chkLab.Enabled = false;


                chkTest.Visible = false;
                chkBList.Visible = false;


                for (int i = 0; i < lvTest.Items.Count; i++)
                {
                    Button btnReport = lvTest.Items[i].FindControl("btnReport") as Button;
                    ImageButton btnDel = lvTest.Items[i].FindControl("btnDelete") as ImageButton;
                    btnReport.Enabled = true;
                    btnDel.Enabled = false;
                }
            }
            else
            {
                btnSubmit.Visible = true;
                btnPrescription.Visible = true;
                pnlSGrid.Enabled = true;
                Panel1.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCanceModal_Click(object sender, EventArgs e)
    {
        rbPatient.SelectedValue = "EmployeeCode";

        DataTable dt = null;
        lvEmp.DataSource = dt;
        lvEmp.DataBind();
    }

    protected void chkTest_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTest.Checked == true)
            {
                foreach (ListItem chkitem in chkBList.Items)
                {
                    chkitem.Selected = true;
                }
            }
            else
            {
                foreach (ListItem chkitem in chkBList.Items)
                {
                    chkitem.Selected = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.chkB_CheckedChanged -> " + ex.Message + " " + ex.StackTrace);
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
                DataSet ds = objHelTranTransaction.GetTestContentDetails(Convert.ToInt32(ddlTitle.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    chkBList.DataSource = ds;
                    chkBList.DataTextField = "CONTENT_NAME";
                    chkBList.DataValueField = "CONTENTNO";
                    chkBList.DataBind();
                    chkBList.Visible = true;
                    chkBList.Enabled = true;
                    btnAdd.Visible = true;
                    chkTest.Visible = true;
                    chkTest.Checked = false;
                }
                else
                {
                    chkBList.Visible = false;
                    btnAdd.Visible = false;
                    chkTest.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.ddlTitle_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearSearch()
    {
        txtComplaints.Text = string.Empty;
        txtFindings.Text = string.Empty;
        txtDiagnosis.Text = string.Empty;
        txtInstructions.Text = string.Empty;
        txtHeight.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtBP.Text = string.Empty;
        txtTemprature.Text = string.Empty;
        txtPulseRate.Text = string.Empty;
        txtRespiration.Text = string.Empty;
        if (Convert.ToInt32(Session["idno"]) == 0)
        {
            ddlDoctor.SelectedIndex = 0;
        }

        FirstDep.Visible = false;
        SecDep.Visible = false;
        ThiDep.Visible = false;
        ddlDependent.SelectedIndex = 0;
        txtReference.Text = string.Empty;
        //lblEmployeeCode.Text = string.Empty;
        Session["RecTbl"] = null;
        ViewState["SRNO"] = null;

        //this.ModalPopupExtender2.Hide();
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        Session["RecContentTbl"] = null;
        ViewState["opdID"] = null;
        ViewState["action"] = "add";
        pnlSGrid.Visible = false;
    }


    protected void btnSearchOnForm_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPatientName.Text != "")
            {
                DataSet ds = objHelTranTransaction.GetPatientIDByCodeRegNo(txtPatientName.Text, Convert.ToInt32(rdbSearchList.SelectedValue));
                //if (ds.Tables[0].Rows.Count != null)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();


                    if (rdbSearchList.SelectedValue == "0")  // Search by Employee Code
                    {
                        lblPatientCat.Text = string.Empty;
                        FillEmployeeInfo(Convert.ToInt32(hfPatientName.Value));
                        trDependent.Visible = true;
                        lblEmp.Visible = true;
                        lblDot.Visible = true;
                        trPatientHist.Visible = true;
                    }
                    else if (rdbSearchList.SelectedValue == "1") // Search by Student Reg No.
                    {
                        lblPatientCat.Text = string.Empty;
                        lblEmployeeCode.Text = string.Empty;
                        trDependent.Visible = false;
                        FillStudentInfo(Convert.ToInt32(hfPatientName.Value));
                        lblEmp.Visible = false;
                        lblDot.Visible = false;
                        trPatientHist.Visible = true;
                    }
                    this.ClearSearch();
                    this.ClearTest();
                    this.ClearPresc();
                }
                else
                {
                    //objCommon.DisplayMessage(this.updOpdTransaction, "No Record Found.", this.Page);
                    MessageBox("No Record Found.");
                    return;
                }
            }
            else
            {
                //objCommon.DisplayMessage(this.updOpdTransaction, "Please Enter Either Employee Code OR Student REG. NO.", this.Page);
                MessageBox("Please Enter Either Employee Code OR Student REG. NO.");
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnSearchOnForm_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to create data table for test content as per selected test.
    private DataTable CreateContentTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("TITLENO", typeof(int)));
        dt.Columns.Add(new DataColumn("CONTENTNO", typeof(int)));
        return dt;
    }

    private void TestContentTable()
    {
        foreach (ListItem item in chkBList.Items)
        {
            if (item.Selected == true)
            {
                if (Session["RecContentTbl"] != null && ((DataTable)Session["RecContentTbl"]) != null)
                {

                    DataTable dtContent = (DataTable)Session["RecContentTbl"];
                    DataRow dr = dtContent.NewRow();

                    dr["TITLENO"] = ddlTitle.SelectedValue;
                    dr["CONTENTNO"] = item.Value;
                    dtContent.Rows.Add(dr);
                    Session["RecContentTbl"] = dtContent;
                }
                else
                {
                    DataTable dtContent = this.CreateContentTabel();
                    DataRow dr = dtContent.NewRow();
                    dr["TITLENO"] = ddlTitle.SelectedValue;
                    dr["CONTENTNO"] = item.Value;
                    dtContent.Rows.Add(dr);
                    Session["RecContentTbl"] = dtContent;
                }
            }
        }
    }

    //protected void txtWeight_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Health_Transaction_PatientDetails.txtWeight_TextChanged -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}


    protected void btnBMIndex_Click(object sender, EventArgs e)
    {
        try
        {
            double BMI = 0.0;
            double HeightInMeters = Convert.ToDouble(txtHeight.Text) / 100;
            BMI = Math.Round((Convert.ToDouble(txtWeight.Text) / (HeightInMeters * HeightInMeters)), 2);
            lblBMI.Text = BMI.ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.btnBMIndex_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    //protected void FillTitle()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO <> 0", "CITY");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.FillCity-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
}