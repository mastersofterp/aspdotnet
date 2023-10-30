//====================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 18-FEB-2017
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================

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

public partial class Health_Transaction_DirectMedicineIssue : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HealthTransactionController objHelTranTransaction = new HealthTransactionController();
    HealthTransactions objHelTran = new HealthTransactions();
    DirectMIssueController objDirectCon = new DirectMIssueController();
    DirectMIssue objD = new DirectMIssue();

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
                    lblEmployeeCode.Visible = false;
                    ViewState["action"] = "add";

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
                            lblEmployeeCode.Visible = true;
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
                            lblEmployeeCode.Visible = false;
                            trPatientHist.Visible = true;
                        }
                        if (Request.QueryString["Name"] == "Other")
                        {
                            hfPatientName.Value = id.ToString();
                            lblPatientCat.Text = string.Empty;
                            trDependent.Visible = false;
                            lblEmp.Visible = false;
                            lblDot.Visible = false;
                            lblEmployeeCode.Visible = false;
                            FillOtherPatientInfo(id);
                            trPatientHist.Visible = true;
                        }

                        btnNewP.Visible = true;
                        pnlMedicineGrid.Visible = false;
                    }
                    else
                    {
                        //btnPrescription.Visible = true;
                        //btnCancel.Visible = true;
                        btnNewP.Visible = true;
                        //btnSubmit.Visible = true;

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
                        ItemsName.Add(sdr["ITEM_NO"].ToString() + "---------*" + sdr["ITEM_NAME"].ToString());
                    }
                }
                conn.Close();
                return ItemsName;

            }
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDoseName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DNO, DNAME AS DNAME from HEALTH_DOSAGEMASTER where DNAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["DNO"].ToString() + "---------*" + sdr["DNAME"].ToString());
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
    }

    private void FillEmployeeInfo(int idno)
    {
        try
        {
            HelMasterController objHelMaster = new HelMasterController();
            DataSet ds = objHelMaster.GetPatientDetailsByPID(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblEmployeeCode.Text = ds.Tables[0].Rows[0]["PFILENO"].ToString();
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
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                txtAge.Focus();
                lblPatientCat.Text = "E";
                this.BindListView(Convert.ToInt32(hfPatientName.Value), lblPatientCat.Text);
                if (Convert.ToInt32(Session["usertype"]) == 5)
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

    private void FillStudentInfo(int idno)
    {
        try
        {
            HelMasterController objHelMaster = new HelMasterController();
            DataSet ds = objHelMaster.GetStudentInfo(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

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
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                txtAge.Focus();
                lblPatientCat.Text = "S";
                this.BindListView(Convert.ToInt32(hfPatientName.Value), lblPatientCat.Text);
                if (Convert.ToInt32(Session["usertype"]) == 5)
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

    private void FillOtherPatientInfo(int idno)
    {
        try
        {

            trOtherCategory.Visible = false;
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
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                txtAge.Focus();
                lblPatientCat.Text = "O";
                this.BindListView(Convert.ToInt32(hfPatientName.Value), lblPatientCat.Text);
                if (Convert.ToInt32(Session["usertype"]) == 5)
                {
                    btnPrescription.Visible = true;
                }
            }

            //// for fisrt time entry of Other Category Patient.
            //if (txtPatientName.Text.Trim() == "Emergency Case")
            //{
            //    txtAge.Text = string.Empty;
            //    trOtherCategory.Visible = true;
            //}
            //else
            //{

            //}
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

    protected void btnAddPrescription_OnClick(object sender, EventArgs e)
    {
        try
        {
            int unique = 0;

            DataSet ds = objHelTranTransaction.GetInsufficientStockDetails(Convert.ToInt32(hfItemName.Value));

            if (Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(ds.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString()) && rdbYes.Checked == true)
            {
                objCommon.DisplayMessage(this.updOpdTransaction, "No sufficient stock", this.Page);
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
                    if (ddlDosage.SelectedValue == "0")
                    {
                        dr["DOSES_ID"] = "";
                    }
                    else
                    {
                        dr["DOSES_ID"] = ddlDosage.SelectedItem.Text;
                    }
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

                    if (ddlDosage.SelectedValue == "0")
                    {
                        dr["DOSES_ID"] = "";
                    }
                    else
                    {
                        dr["DOSES_ID"] = ddlDosage.SelectedItem.Text;
                    }

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

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        try
        {

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

            objHelTran.IP_ADDRESS = "";
            objHelTran.MAC_ADDRESS = "";
            objHelTran.COLLEGE_CODE = Session["colcode"].ToString();

            objHelTran.OPDNO = Convert.ToInt32(ViewState["opdID"]);

            if (ViewState["action"].ToString() == "add")
            {
                foreach (GridViewRow row in lvMedicineissue.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        TextBox txtItemname = (TextBox)row.FindControl("txtItemname"); // item name
                        TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");  // direct issue qty
                        TextBox txtAvilQty = (TextBox)row.FindControl("txtAvailQty");// Available qty
                        TextBox txtQtyMain = (TextBox)row.FindControl("txtQtyMain"); // Issued Qty

                        if (txtItemname.Text == string.Empty)
                        {
                            objCommon.DisplayMessage(this.updOpdTransaction, "Medicine name should not be blank.", this.Page);
                            return;
                        }
                        if (txtissue.Text == string.Empty)
                        {
                            objCommon.DisplayMessage(this.updOpdTransaction, "Issue Quantity should not be blank.", this.Page);
                            return;
                        }
                        int issueQty = Convert.ToInt32(txtissue.Text);
                        int avaiQty = Convert.ToInt32(txtAvilQty.Text);
                        int IssuedQty = Convert.ToInt32(txtQtyMain.Text); // mainQty

                        if (issueQty > avaiQty)
                        {
                            objCommon.DisplayMessage(this.updOpdTransaction, "Issue Quantity should not be greater than Available Quantity.", this.Page);
                            return;
                        }
                    }
                }

                DataTable MedicineTbl = new DataTable("MediTbl");

                MedicineTbl.Columns.Add("ADMNO", typeof(int));
                MedicineTbl.Columns.Add("ITEM_NAME", typeof(string));
                MedicineTbl.Columns.Add("INO", typeof(int));
                MedicineTbl.Columns.Add("QTY", typeof(int));
                MedicineTbl.Columns.Add("DOSESNO", typeof(int));
                MedicineTbl.Columns.Add("QTY_ISSUE", typeof(int));
                MedicineTbl.Columns.Add("AVAILABLE_QTY", typeof(int));
                MedicineTbl.Columns.Add("ISSUE_STATUS", typeof(int));

                DataRow dr = null;
                foreach (GridViewRow row in lvMedicineissue.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label srNo = (Label)row.FindControl("lblSrno");
                        TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                        Label ino = (Label)row.FindControl("lblino");
                        TextBox qty = (TextBox)row.FindControl("txtQtyMain");
                        Label dno = (Label)row.FindControl("lblDno");
                        TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
                        TextBox txtAvailQty = (TextBox)row.FindControl("txtAvailQty");
                        CheckBox chkrow = (CheckBox)row.FindControl("chkIssue");

                        dr = MedicineTbl.NewRow();
                        dr["ADMNO"] = srNo.Text;
                        dr["ITEM_NAME"] = txtItemname.Text;
                        dr["INO"] = ino.Text;
                        dr["QTY"] = qty.Text;
                        dr["DOSESNO"] = dno.Text;
                        dr["QTY_ISSUE"] = txtissue.Text;
                        dr["AVAILABLE_QTY"] = txtAvailQty.Text;
                        dr["ISSUE_STATUS"] = 1;
                        MedicineTbl.Rows.Add(dr);
                    }
                }

                objHelTran.MEDICINE_PRES = MedicineTbl;                                  // CustomStatus cs = (CustomStatus)objHelTranTransaction.AddPrescriptionBasedOPD(objHelTran);

                if (Session["Carta"] != null)
                {
                    DataTable dt = (DataTable)Session["Carta"];
                    if (dt.Rows.Count != 0)
                    {
                        CustomStatus cs1 = (CustomStatus.TransactionFailed);
                        int count = Convert.ToInt32(SecondGrid.Rows.Count);

                        if (hfPatientName.Value == string.Empty)
                        {
                            objD.PID = 0;
                        }
                        else
                        {
                            objD.PID = Convert.ToInt32(hfPatientName.Value);
                        }

                        cs1 = (CustomStatus)objHelTranTransaction.AddHelPrescriptionBasedMedicine(objHelTran);

                        if (cs1.Equals(CustomStatus.RecordSaved))  // cs.Equals(CustomStatus.RecordSaved) &&
                        {
                            objCommon.DisplayMessage(updOpdTransaction, "Record Saved Successfully.", this);
                            this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                        }
                    }
                    //else if (dt.Rows.Count == 0 && cs.Equals(CustomStatus.RecordSaved))
                    //{
                    //    objCommon.DisplayMessage(updOpdTransaction, "Prescriptions are Not Added & OPD Record Saved Successfully", this);
                    //}
                }
                if (Session["Carta"] == null)
                {
                    objCommon.DisplayMessage(updOpdTransaction, "Record Saved Successfully.", this);
                    this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());

                }
                BindData();
            }

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
        //hfPatientName.Value = null;
        Session["Carta"] = null;
        this.ClearControls();
        this.ClearPresc();
        //trDependent.Visible = false;       
        pnlMedicineGrid.Visible = false;
        trDetails.Visible = false;
        btnNewP.Visible = true;
        btnSubmit.Visible = false;
    }

    protected void btnConfirm_OnClick(object sender, EventArgs e)
    {
        DataTable dt;
        dt = ((DataTable)Session["Carta"]);
        this.BindListView_Second(dt);
        //btnSubmit.Visible = true;       
        pnlSGrid.Visible = true;
        trPatientHist.Visible = false;
        btnSubmitNew.Visible = true;
        btnCancel.Visible = true;
    }

    protected void btnPrescription_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (hfPatientName.Value == "")
            {
                objCommon.DisplayMessage(updOpdTransaction, "Please Select Patient Name.", this);
                return;
            }
            else
            {
                this.BindData();
                trDetails.Visible = false;
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                btnNewP.Visible = false;
                pnlSGrid.Visible = false;
                btnSubmitNew.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objCommon.DisplayUserMessage(updOpdTransaction, "Health_Transaction_PatientDetails.btnPrescription_OnClick()-->" + ex.Message + " " + ex.StackTrace, this);

            else
                objCommon.DisplayUserMessage(updOpdTransaction, "Server Unavailable.", this);
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
        ShowReport("Prescription", "rptPrescriptionDetails.rpt", sender);
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
            ScriptManager.RegisterClientScriptBlock(updOpdTransaction, updOpdTransaction.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
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
                // trPatientHist.Visible = false;
            }

            //DataSet dsRec = objDirectCon.GetDirectIssueHistory(OPDID, PatientCat);
            // if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
            // {
            //     gridDirectMIssue.DataSource = dsRec.Tables[0];
            //     gridDirectMIssue.DataBind();
            //    // trDirectIssueHistory.Visible = true;
            // }
            // else
            // {
            //     gridDirectMIssue.DataSource = null;
            //     gridDirectMIssue.DataBind();

            // }
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
        txtWeight.Text = string.Empty;
        txtBP.Text = string.Empty;
        txtTemprature.Text = string.Empty;
        txtPulseRate.Text = string.Empty;
        txtRespiration.Text = string.Empty;
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
        Session["RecTbl"] = null;
        ViewState["SRNO"] = null;
        pnlSGrid.Visible = false;
        //this.ModalPopupExtender2.Hide();
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        Session["RecContentTbl"] = null;
        ViewState["opdID"] = null;
        ViewState["action"] = "add";
        btnSubmitNew.Visible = false;
        Session["Carta"] = null;
        hfPatientName.Value = null;
    }

    private void ShowReport(string reportTitle, string rptFileName, object sender)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            int OPDID = int.Parse(btnPrint.CommandArgument);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_OPDID=" + OPDID;
            ScriptManager.RegisterClientScriptBlock(updOpdTransaction, updOpdTransaction.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
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
                SecDep.Visible = true;
                ThiDep.Visible = true;
                objCommon.FillDropDownList(ddlDependent, "PAYROLL_SB_FAMILYINFO", "FNNO", "RELATION + '- '+ MEMNAME AS MEMNAME", "IDNO=" + hfPatientName.Value + "AND RELATION <> 'SELF'", "FNNO");
                txtAge.Text = string.Empty;
                txtWeight.Text = string.Empty;
            }
            else  // employee
            {
                FirstDep.Visible = false;
                SecDep.Visible = false;
                ThiDep.Visible = false;
                ddlDependent.SelectedIndex = 0;
                txtAge.Text = string.Empty;
                txtWeight.Text = string.Empty;
                FillEmployeeInfo(Convert.ToInt32(hfPatientName.Value));
                pnlSGrid.Visible = false;

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
                // DataSet ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "FNNO", "DATEDIFF(YEAR,  DOB, GETDATE()) AS AGE", "FNNO=" + ddlDependent.SelectedValue, "");
                DataSet ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "FNNO", "DBO.AGE_LENGTH(DOB) AS AGE", "FNNO=" + ddlDependent.SelectedValue, "");
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
            objCommon.DisplayMessage(updOpdTransaction, "Please save prescription details and then close.", this);
        }
        else
        {
            //this.ModalPopupExtender2.Hide();
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('hide');", true);
        }
    }

    // This method is used to create data table for test title.
    private DataTable CreateTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("TITLENO", typeof(int)));
        dt.Columns.Add(new DataColumn("TEST_TITLE", typeof(string)));
        dt.Columns.Add(new DataColumn("OBSERNO", typeof(int)));
        return dt;
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        GridViewRow item = (GridViewRow)(sender as Control).NamingContainer;
        ImageButton btnDetails = (ImageButton)item.FindControl("btnDetails");

        int opdID = int.Parse(btnDetails.CommandArgument);
        ViewState["opdID"] = int.Parse(btnDetails.CommandArgument);
        ShowDetails(opdID);
        btnPrescription.Visible = true;
        pnlSGrid.Enabled = false;
        btnConfirm.Visible = true;
        btnCancelP.Visible = true;
        //trDetails.Visible = true;
        btnNewP.Visible = false;
        btnCancel.Visible = true;
    }

    private void ShowDetails(int opdID)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("HEALTH_PATIENT_DETAILS", "*", "", "OPDID=" + opdID, "OPDID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                // txtPatientName.Text =  ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["PID"].ToString();
                lblEmployeeCode.Text = ds.Tables[0].Rows[0]["PATIENT_CODE"].ToString();
                if (lblEmployeeCode.Text == "D")
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
                //txtComplaints.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                //txtFindings.Text = ds.Tables[0].Rows[0]["FINDING"].ToString();
                //txtBP.Text = ds.Tables[0].Rows[0]["BP"].ToString();
                //txtTemprature.Text = ds.Tables[0].Rows[0]["TEMP"].ToString();
                //txtPulseRate.Text = ds.Tables[0].Rows[0]["PULSE"].ToString();
                //txtRespiration.Text = ds.Tables[0].Rows[0]["RESP"].ToString();


                DataSet dsRec = objDirectCon.GetPreviousPrescriptionForIssue(opdID);//objCommon.FillDropDown("HEALTH_PRESC P INNER JOIN HEALTH_DOSAGEMASTER D ON (P.DOSES = D.DNO)", "ADMNO AS [UNIQUE], INO, ITEMNAME + ' ' + (CASE WHEN STATUS_DEL = 1 THEN '(Suspended)' WHEN (STATUS_DEL = 0 AND ISSUE_STATUS = 1) THEN '(Issued)' ELSE '' END) AS ITEMNAME, D.DNAME AS DOSES_ID , NOOFDAYS, DOSES AS DOSES, QTY, SPINST", "PRESCRIPTION_STATUS as PRESCRIPTION_STATUS_ID , (CASE PRESCRIPTION_STATUS WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) as PRESCRIPTION_STATUS", "OPDNO=" + opdID, "OPDNO");
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    SecondGrid.DataSource = dsRec.Tables[0];
                    SecondGrid.DataBind();
                    pnlSGrid.Visible = true;
                    btnPrescription.Visible = true;
                    Session["Carta"] = dsRec.Tables[0];
                    gvPrisc.DataSource = dsRec.Tables[0];
                    gvPrisc.DataBind();
                }
                else
                {
                    btnPrescription.Visible = false;
                    objCommon.DisplayMessage(updOpdTransaction, "No record found for current Selection", this.Page);
                    return;
                }
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

    protected void btnSearchOnForm_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPatientName.Text != "")
            {
                DataSet ds = objDirectCon.GetPatientInDirectIssue(txtPatientName.Text, Convert.ToInt32(rdbSearchList.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                }

                if (rdbSearchList.SelectedValue == "0")   // Search by Employee Code
                {
                    lblPatientCat.Text = string.Empty;
                    FillEmployeeInfo(Convert.ToInt32(hfPatientName.Value));
                    trDependent.Visible = true;
                    lblEmp.Visible = true;
                    lblDot.Visible = true;
                    lblEmployeeCode.Visible = true;
                    trPatientHist.Visible = true;
                }
                else if (rdbSearchList.SelectedValue == "1")   // Search by Student Reg No.
                {
                    lblPatientCat.Text = string.Empty;
                    lblEmployeeCode.Text = string.Empty;
                    trDependent.Visible = false;
                    FillStudentInfo(Convert.ToInt32(hfPatientName.Value));
                    lblEmp.Visible = false;
                    lblDot.Visible = false;
                    lblEmployeeCode.Visible = false;
                    trPatientHist.Visible = true;
                }
                btnNewP.Visible = true;
                pnlMedicineGrid.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this.updOpdTransaction, "Please Enter Either Employee Code OR Student REG. NO.", this.Page);
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

    private void BindData()
    {
        try
        {
            DataSet ds = objDirectCon.GetPatientDetailsForDirectIssue(Convert.ToInt32(ViewState["opdID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvMedicineissue.DataSource = ds;    // ViewState["opdID"] = ds.Tables[0].Rows[0]["OPDNO"].ToString(); pnlMedicineGrid
                lvMedicineissue.DataBind();
                pnlMedicineGrid.Visible = true;
                DataTable firstTable = ds.Tables[0];
                ViewState["CurrentTable"] = firstTable;

                foreach (GridViewRow row in lvMedicineissue.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                        TextBox qty = (TextBox)row.FindControl("txtQtyMain");
                        TextBox dno = (TextBox)row.FindControl("txtDoses");
                        CheckBox chkrow = (CheckBox)row.FindControl("chkIssue");
                        TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
                        //LinkButton lnkR = (LinkButton)row.FindControl("lnkRemove");

                        txtItemname.Enabled = false;
                        qty.Enabled = false;
                        dno.Enabled = false;
                        txtissue.Enabled = true;
                        //lnkR.Visible = true;
                        //Button btnAdd = lvMedicineissue.FooterRow.FindControl("btnAdd") as Button;
                        // btnAdd.Visible = true;
                        // btnsave.Enabled = true;                       

                    }
                }
            }
            else
            {

                pnlMedicineGrid.Visible = false;
                //btnsave.Visible = false;
                //btnclear.Visible = false;
                lvMedicineissue.DataSource = null;
                lvMedicineissue.DataBind();
                DataTable firstTable = ds.Tables[0];
                ViewState["CurrentTable"] = firstTable;
                objCommon.DisplayMessage(updOpdTransaction, "No record found for current Selection", this.Page);
                return;

            }
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_StockMaintenance_InvoiceEntry->btnSave_Click" + ex.Message);
        }
    }

    protected void txtItemname_TextChanged(object sender, EventArgs e)
    {
        TextBox txtitem = sender as TextBox;
        GridViewRow item = (GridViewRow)(sender as Control).NamingContainer;

        GridViewRow row = (GridViewRow)txtitem.NamingContainer;
        int index = row.RowIndex;

        TextBox txtItemname = (TextBox)item.FindControl("txtItemname");
        Label lblino = (Label)item.FindControl("lblino");
        //LinkButton lnkRemove = (LinkButton)item.FindControl("lnkRemove");
        TextBox txtAvailQuantity = (TextBox)item.FindControl("txtAvailQty");

        string txtValue = ((TextBox)lvMedicineissue.Rows[index].FindControl("txtItemname")).Text;
        string[] Value = txtValue.Split('-');
        string[] ITEM_NAME = Value[9].Split('*');

        lblino.Text = Value[0].ToString();
        //lnkRemove.CommandArgument = Value[0].ToString();
        txtItemname.Text = ITEM_NAME[1].ToString();
        txtItemname.Focus();

        DataSet ds = objDirectCon.GetTotal(Convert.ToInt32(lblino.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtAvailQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString(); //bal_qty.ToString();
        }
    }

    protected void txtDoses_TextChanged(object sender, EventArgs e)
    {
        TextBox txtDoses = sender as TextBox;
        GridViewRow doses = (GridViewRow)(sender as Control).NamingContainer;

        GridViewRow row = (GridViewRow)txtDoses.NamingContainer;
        int index = row.RowIndex;

        TextBox txtDosesName = (TextBox)doses.FindControl("txtDoses");
        Label lblDno = (Label)doses.FindControl("lblDno");

        string txtValue = ((TextBox)lvMedicineissue.Rows[index].FindControl("txtDoses")).Text;

        string[] Value = txtValue.Split('*');
        txtDoses.Text = Value[1].ToString();

        string[] DOSES_NAME = Value[0].Split('-');

        lblDno.Text = DOSES_NAME[0].ToString();
        txtDoses.Focus();
    }

    protected void AddNewCustomer(object sender, EventArgs e)
    {
        try
        {
            AddNewRowToGrid();
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->AddNewCustomer" + ex.Message);
        }
    }

    private void AddNewRowToGrid()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtItemname = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[1].FindControl("txtItemname");
                        Label lblino = (Label)lvMedicineissue.Rows[rowIndex].Cells[1].FindControl("lblino");
                        TextBox txtDoses = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[2].FindControl("txtDoses");
                        Label lbldNO = (Label)lvMedicineissue.Rows[rowIndex].Cells[2].FindControl("lblDno");
                        TextBox txtQtyMain = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[3].FindControl("txtQtyMain");
                        TextBox txtIssueQty = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[4].FindControl("txtIssueQty");
                        TextBox txtAvailQty = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[5].FindControl("txtAvailQty");
                        //LinkButton lnkR = (LinkButton)lvMedicineissue.Rows[rowIndex].Cells[6].FindControl("lnkRemove");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ITEMNAME"] = txtItemname.Text;
                        dtCurrentTable.Rows[i - 1]["INO"] = lblino.Text;
                        dtCurrentTable.Rows[i - 1]["QTY_ISSUE"] = txtQtyMain.Text;
                        dtCurrentTable.Rows[i - 1]["DIRECT_ISSUE"] = txtIssueQty.Text;
                        dtCurrentTable.Rows[i - 1]["DNAME"] = txtDoses.Text;
                        dtCurrentTable.Rows[i - 1]["DOSES"] = lbldNO.Text;

                        if (txtIssueQty.Text != string.Empty)
                        {
                            dtCurrentTable.Rows[i - 1]["QTY_ISSUE"] = txtQtyMain.Text;  //txtIssueQty.Text;
                        }
                        else
                        {
                            dtCurrentTable.Rows[i - 1]["QTY_ISSUE"] = 0;
                        }
                        dtCurrentTable.Rows[i - 1]["ITEM_MAX_QTY"] = txtAvailQty.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvMedicineissue.DataSource = dtCurrentTable;
                    lvMedicineissue.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->AddNewCustomer" + ex.Message);
        }
    }

    protected void DeleteCustomer(object sender, EventArgs e)
    {
        try
        {

            //LinkButton lnkRemove = (LinkButton)sender;
            //int ino = Convert.ToInt32(lnkRemove.CommandArgument);

            // DataTable dt = (DataTable)ViewState["CurrentTable"]; 
            DataTable MedicineTbl = new DataTable("MediTbl");
            MedicineTbl.Columns.Add("ITEMNAME", typeof(string));
            MedicineTbl.Columns.Add("INO", typeof(int));
            MedicineTbl.Columns.Add("QTY", typeof(int));
            MedicineTbl.Columns.Add("DNAME", typeof(string));
            MedicineTbl.Columns.Add("DOSES", typeof(int));
            MedicineTbl.Columns.Add("QTY_ISSUE", typeof(int));
            MedicineTbl.Columns.Add("ITEM_MAX_QTY", typeof(int));
            MedicineTbl.Columns.Add("ISSUE_STATUS", typeof(int));
            MedicineTbl.Columns.Add("ITEM_NO", typeof(int));

            DataRow dr = null;
            foreach (GridViewRow row in lvMedicineissue.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                    Label itemno = (Label)row.FindControl("lblino");
                    TextBox qty = (TextBox)row.FindControl("txtQtyMain");
                    Label dno = (Label)row.FindControl("lblDno");
                    TextBox txtDoses = (TextBox)row.FindControl("txtDoses");
                    TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
                    TextBox txtAvailQty = (TextBox)row.FindControl("txtAvailQty");
                    CheckBox chkrow = (CheckBox)row.FindControl("chkIssue");

                    dr = MedicineTbl.NewRow();
                    dr["ITEMNAME"] = txtItemname.Text;
                    dr["INO"] = itemno.Text;
                    dr["QTY"] = qty.Text;
                    dr["DNAME"] = txtDoses.Text;
                    dr["DOSES"] = dno.Text;
                    if (txtissue.Text != string.Empty)
                    {
                        dr["QTY_ISSUE"] = txtissue.Text;
                    }
                    else
                    {
                        dr["QTY_ISSUE"] = 0;
                    }
                    dr["ITEM_MAX_QTY"] = txtAvailQty.Text;
                    dr["ITEM_NO"] = itemno.Text;
                    if (chkrow.Checked == true)
                    {
                        dr["ISSUE_STATUS"] = "1";
                    }
                    else
                    {
                        dr["ISSUE_STATUS"] = "0";
                    }

                    MedicineTbl.Rows.Add(dr);
                }
            }

            //CustomStatus cs = (CustomStatus)objHelTranTransaction.DeleteHealthMedicineIsuue(ino, Convert.ToInt32(ViewState["opdno"]));
            //if (cs.Equals(CustomStatus.RecordDeleted))
            //{
            //    objCommon.DisplayMessage(this.updOpdTransaction, "Record Deleted Successfully", this.Page);

            //    //MedicineTbl.Rows.Remove(this.GetEditableDatarow(MedicineTbl, lnkRemove.CommandArgument));

            //    ViewState["CurrentTable"] = MedicineTbl;
            //    lvMedicineissue.DataSource = MedicineTbl;
            //    lvMedicineissue.DataBind();
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updOpdTransaction, "errro in deleting data", this.Page);
            //}
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->DeleteCustomer" + ex.Message);
        }
    }

    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["INO"].ToString() == value)
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

    protected void EditCustomer(object sender, GridViewEditEventArgs e)
    {
        lvMedicineissue.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lvMedicineissue.EditIndex = -1;
        BindData();
    }

    protected void btnNewP_OnClick(object sender, EventArgs e)
    {
        try
        {
            //if (hfPatientName.Value == "")
            //{
                lvPatientHist.Visible = false;
                lvMedicineissue.Visible = false;
                btnConfirm.Visible = true;
                btnCancelP.Visible = true;
                btnSubmitNew.Visible = true;
                btnSubmit.Visible = false;
                //this.ModalPopupExtender2.Show();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#divPrescription').modal('show');", true);
                // this.ModalPopupExtender2.Hide();
                //objCommon.DisplayMessage(updOpdTransaction, "Please Select Patient Name.", this);
                // return;
            //}

        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->DeleteCustomer" + ex.Message);
        }
    }

    protected void btnSubmitNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfPatientName.Value == string.Empty)
            {
                string pidOtherUser = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "(ISNULL(MAX(PID),0))+1", "PATIENT_CODE='O'");
                objD.PID = Convert.ToInt32(pidOtherUser);
                lblPatientCat.Text = "O";
            }
            else
            {
                string PatientName = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "PATIENT_NAME", "PID =" + hfPatientName.Value);
                if (PatientName == "Emergency Case")
                {
                    string pidOtherUser = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "(ISNULL(MAX(PID),0))+1", "PATIENT_CODE='O'");
                    objD.PID = Convert.ToInt32(pidOtherUser);
                    lblPatientCat.Text = "O";
                }
                else
                {
                    objD.PID = Convert.ToInt32(hfPatientName.Value);
                }
            }

            ViewState["PID"] = objD.PID;
            objD.COMPLAINT = txtComplaints.Text == string.Empty ? "" : txtComplaints.Text;
            objD.FINDING = txtFindings.Text == string.Empty ? "" : txtFindings.Text;
            objD.WEIGHT = txtWeight.Text == string.Empty ? "" : txtWeight.Text;
            objD.AGE = txtAge.Text == string.Empty ? string.Empty : txtAge.Text;
            if (rdbFemale.Checked == true)
            {
                objD.SEX = 'F';
            }
            else if (rdbMale.Checked == true)
            {
                objD.SEX = 'M';
            }
            objD.BP = txtBP.Text;
            if (txtTemprature.Text != "")
            {
                objD.TEMP = Convert.ToDecimal(txtTemprature.Text);
            }
            else
            {
                objD.TEMP = Convert.ToDecimal(0);
            }
            objD.PULSE = txtPulseRate.Text;
            objD.RESP = txtRespiration.Text;
            objD.IP_ADDRESS = "";
            objD.MAC_ADDRESS = "";
            objD.COLLEGE_CODE = Session["colcode"].ToString();

           // string[] pNAME = txtPatientName.Text.Split('*');
            string[] pNAME = txtPatientName.Text.Split('-');
            string[] dNAME = ddlDependent.SelectedItem.Text.Split('-');
            if (lblPatientCat.Text == "E")
            {
                if (rdbPCList.SelectedValue == "0")
                {
                    objD.DEPENDENTID = 0;
                    objD.PATIENT_CODE = 'E';
<<<<<<< HEAD

                    objD.PATIENT_NAME = pNAME[1];

=======
>>>>>>> UAT_TO_MAIN_2023-10-30/06-30PM
                    //objD.PATIENT_NAME = pNAME[1];
                    if (pNAME.Length == 1)
                    {
                        objD.PATIENT_NAME = pNAME[0];
                    }
                    else
                    {
                        objD.PATIENT_NAME = pNAME[1];
                    }
                }
                else
                {
                    objD.DEPENDENTID = Convert.ToInt32(ddlDependent.SelectedValue);
                    objD.PATIENT_CODE = 'D';
                    objD.PATIENT_NAME = dNAME[1];
                }
            }
            else if (lblPatientCat.Text == "S")
            {
                objD.PATIENT_CODE = 'S';
                objD.PATIENT_NAME = pNAME[0];
            }
            else
            {
                objD.PATIENT_CODE = 'O';
                objD.REFERENCE_BY = txtReference.Text;
                objD.PATIENT_NAME = pNAME[0];
            }

            ViewState["PATIENT_CODE"] = objD.PATIENT_CODE;

            string DopdNo = objCommon.LookUp("HEALTH_DIRECT_ISSUE_PATIENT_DETAILS", "(ISNULL(MAX(DOPDID),0))+1", string.Empty);
            objD.DOPDNO = Convert.ToInt32(DopdNo);

            if (ViewState["action"].ToString() == "add")
            {

                //foreach (GridViewRow row in lvMedicineissue.Rows)
                //{
                //    if (row.RowType == DataControlRowType.DataRow)
                //    {
                //        TextBox txtItemname = (TextBox)row.FindControl("txtItemname"); // item name
                //        TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");  // direct issue qty
                //        TextBox txtAvilQty = (TextBox)row.FindControl("txtAvailQty");// Available qty
                //        TextBox txtQtyMain = (TextBox)row.FindControl("txtQtyMain"); // Issued Qty

                //        if (txtItemname.Text == string.Empty)
                //        {
                //            objCommon.DisplayMessage(this.updOpdTransaction, "Medicine name should not be blank.", this.Page);
                //            return;
                //        }
                //        if (txtissue.Text == string.Empty)
                //        {
                //            objCommon.DisplayMessage(this.updOpdTransaction, "Issue Quantity should not be blank.", this.Page);
                //            return;
                //        }
                //        int issueQty = Convert.ToInt32(txtissue.Text);
                //        int avaiQty = Convert.ToInt32(txtAvilQty.Text);
                //        int IssuedQty = Convert.ToInt32(txtQtyMain.Text); // mainQty

                //        if (issueQty > avaiQty)
                //        {
                //            objCommon.DisplayMessage(this.updOpdTransaction, "Issue Quantity should not be greater than Available Quantity.", this.Page);
                //            return;
                //        }
                //    }
                //}


                DataTable dtMedicine = new DataTable("MediTbl");

                dtMedicine.Columns.Add("ADMNO", typeof(int));
                dtMedicine.Columns.Add("INO", typeof(int));
                dtMedicine.Columns.Add("ITEMNAME", typeof(string));
                dtMedicine.Columns.Add("QTY", typeof(string));
                dtMedicine.Columns.Add("DOSES", typeof(string));
                dtMedicine.Columns.Add("NOOFDAYS", typeof(int));
                dtMedicine.Columns.Add("SPINST", typeof(string));
                dtMedicine.Columns.Add("PRESCRIPTION_STATUS", typeof(string));

                DataRow dr = null;

                foreach (GridViewRow row in SecondGrid.Rows)
                {
                    HiddenField hdnsrno = (HiddenField)row.FindControl("hdnSRNO");
                    HiddenField hdnIno = (HiddenField)row.FindControl("hdnINO");
                    HiddenField hdnDno = (HiddenField)row.FindControl("hdnDosesID");
                    HiddenField hdnPno = (HiddenField)row.FindControl("hdnPStatusID");

                    dr = dtMedicine.NewRow();
                    dr["ADMNO"] = Convert.ToInt32(hdnsrno.Value);
                    dr["INO"] = Convert.ToInt32(hdnIno.Value);
                    dr["ITEMNAME"] = row.Cells[2].Text;
                    dr["QTY"] = row.Cells[6].Text;
                    dr["DOSES"] = Convert.ToInt32(hdnDno.Value);
                    dr["NOOFDAYS"] = row.Cells[3].Text.Replace("&nbsp;", "0");
                    dr["SPINST"] = row.Cells[7].Text.Replace("&nbsp;", "");
                    dr["PRESCRIPTION_STATUS"] = Convert.ToInt32(hdnPno.Value);

                    dtMedicine.Rows.Add(dr);
                }

                objD.MEDICINE_PRES = dtMedicine;

                CustomStatus cs = (CustomStatus)objDirectCon.AddDirectMedicineIssue(objD);

                if (Session["Carta"] != null)
                {
                    DataTable dt = (DataTable)Session["Carta"];
                    if (dt.Rows.Count != 0)
                    {
                        CustomStatus cs1 = (CustomStatus.TransactionFailed);
                        int count = Convert.ToInt32(SecondGrid.Rows.Count);

                        if (hfPatientName.Value == string.Empty)
                        {
                            objD.PID = 0;
                        }
                        else
                        {
                            objD.PID = Convert.ToInt32(hfPatientName.Value);
                        }

                        cs1 = (CustomStatus)objDirectCon.AddDirectPrescription(objD);

                        if (cs.Equals(CustomStatus.RecordSaved) && cs1.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(updOpdTransaction, "Record Saved Successfully.", this);
                            this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                        }
                    }
                    else if (dt.Rows.Count == 0 && cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updOpdTransaction, "Prescriptions are Not Added & OPD Record Saved Successfully", this);
                    }
                }
                if (Session["Carta"] == null)
                {
                    objCommon.DisplayMessage(updOpdTransaction, "Record Saved Successfully.", this);
                    this.BindListView(Convert.ToInt32(ViewState["PID"]), ViewState["PATIENT_CODE"].ToString());
                }
            }
            ClearControls();
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->DeleteCustomer" + ex.Message);
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