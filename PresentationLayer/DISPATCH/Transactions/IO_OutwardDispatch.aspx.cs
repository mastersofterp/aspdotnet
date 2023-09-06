//================================================
//MODIFIED BY   : MRUNAL SINGH
//MODIFIED DATE : 05-12-2014
//DESCRIPTION   : TO SAVE ADDRESS OF THE MULTIPLE 
//                RECEIVERS FOR DISPATCH OUTWARD,
//================================================
using System;
using System.Collections.Generic;
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Dispatch_Transactions_IO_OutwardDispatch : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();
    CarrierMaster objCM = new CarrierMaster();
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
                   this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropdown();
                    ddlUnit.SelectedIndex = 0;
                    ViewState["action"] = null;
                    lvTo.DataSource = null;
                    lvTo.DataBind();
                    BindListViewOutwardDispatch();
                    txtDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;
                    txtSentDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    Session["RecChequeTbl"] = null;
                    ViewState["SRNO_CHEQUE"] = 0;
                    lblCheque.Text = "Chq.No";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
           // objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "POSTTYPENO > 0", "POSTTYPENAME");
            objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "ACTIVESTATUS > 0", "POSTTYPENAME");      //gayatri rode 08-03-2022
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITY");
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");
            objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlCarrier, "ADMN_IO_CARRIER", "CARRIERNO", "CARRIERNAME", "STATUS=0", "CARRIERNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.PopulateDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListViewOutwardDispatch()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                ds = objIOtranc.GetAllOutwardDispatch(0);
            }
            else
            {
                ds = objIOtranc.GetAllOutwardDispatch(Convert.ToInt32(Session["userno"]));
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                IvOutwardDispatch.DataSource = ds;
                IvOutwardDispatch.DataBind();
                IvOutwardDispatch.Visible = true;
            }
            else
            {
                IvOutwardDispatch.DataSource = null;
                IvOutwardDispatch.DataBind();
                IvOutwardDispatch.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.BindListViewOutwardDispatch -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (lvTo.Items.Count == 0)
        {
            objCommon.DisplayMessage(this.updActivity, "Please Add Receiver Details.", this.Page);
            return;
        }
        IOTRAN objIOtran = new IOTRAN();
        try
        {
            string to1 = string.Empty;
            string to = string.Empty;
            string remark = string.Empty;
            string address = string.Empty;
            string addLine = string.Empty;
            string state = string.Empty;
            string country = string.Empty;
            string cityno = string.Empty;
            string citynumber = string.Empty;
            string pinno = string.Empty;
            string contactno = string.Empty;

            DataTable dt;
            if (ViewState["action"].ToString().Equals("add"))
            {
                if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
                {
                    dt = (DataTable)Session["RecTbl"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (to.Trim().Equals(string.Empty))
                        {
                            to = dr["IOTO"].ToString();
                            to1 = dr["IOTO"].ToString();
                            addLine = dr["ADDLINE"].ToString();
                            state = dr["STATENO"].ToString();
                            country = dr["COUNTRYNO"].ToString();
                            contactno = dr["CONTACTNO"].ToString();
                        }
                        else
                        {
                            to += "," + dr["IOTO"].ToString();
                            addLine += "," + dr["ADDLINE"].ToString();
                            state += "," + dr["STATENO"].ToString();
                            country += "," + dr["COUNTRYNO"].ToString();
                            contactno += "," + dr["CONTACTNO"].ToString();
                        }

                        if (remark.Trim().Equals(string.Empty))

                            remark = dr["REMARK"].ToString();
                        else
                            remark += "," + dr["REMARK"].ToString();

                        if (address.Trim().Equals(string.Empty))
                            address = dr["MULTIPLE_ADDRESS"].ToString();
                        else
                            address += "," + dr["MULTIPLE_ADDRESS"].ToString();
                        if (cityno.Trim().Equals(string.Empty))
                            cityno = dr["CITYNO"].ToString();

                        else
                            cityno += "," + dr["CITYNO"].ToString();
                        if (citynumber.Trim().Equals(string.Empty))
                            citynumber = dr["CITYNumber"].ToString();

                        else
                            citynumber += "," + dr["CITYNumber"].ToString();
                        if (pinno.Trim().Equals(string.Empty))
                            pinno = dr["PINNO"].ToString();
                        else
                            pinno += "," + dr["PINNO"].ToString();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Enter Receiver Detail.", this.Page);
                    return;
                }
            }
            else
            { 
            




            }
            objIOtran.IOTYPE = 'O';
            objIOtran.FROMDEPT = Convert.ToInt32(ddlDepartment.SelectedValue);
            objIOtran.DEPTRECSENTDT = Convert.ToDateTime(txtDT.Text);
            objIOtran.POSTTYPENO = Convert.ToInt32(ddlPostType.SelectedValue);

            objIOtran.IOTO = to1;
            //objIOtran.ADDRESS = txtMulAddress.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtMulAddress.Text.Trim().ToString();
            // objIOtran.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            // objIOtran.PINCODE = txtPIN.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtPIN.Text.Trim().ToString();
            objIOtran.SUBJECT = txtSubject.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtSubject.Text.Trim().ToString();
            objIOtran.DEPTREFERENCENO = txtRefNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtRefNo.Text.Trim().ToString();

            //objIOtran.CHQDDNO = txtDDNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtDDNo.Text.Trim().ToString();
            //objIOtran.CHEQDT = txtDDdate.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDDdate.Text);
            //objIOtran.CHEQAMT = txtDDAmt.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtDDAmt.Text);
            //objIOtran.BANKNAME = txtbank.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtbank.Text.Trim().ToString();
            objIOtran.CENTRALRECSENTDT = Convert.ToDateTime(txtSentDT.Text);

            objIOtran.CENTRALREFERENCENO = txtcentralrefno.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtcentralrefno.Text.Trim().ToString();
            objIOtran.WEIGHT = txtWeight.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtWeight.Text);

            objIOtran.COST = txtScost.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtScost.Text);
            objIOtran.EXTRACOST = txtExtraCost.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtExtraCost.Text);
            objIOtran.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objIOtran.CREATED_DATE = System.DateTime.Now;
            objCM.carrierNo = Convert.ToInt32(ddlCarrier.SelectedValue);
            objIOtran.TRACKING_NO = txtTrackNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtTrackNo.Text.Trim().ToString();
            objIOtran.TOTAL_COST = txtCost.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtCost.Text);
            objIOtran.NO_OF_PERSONS = txtnperson.Text.Trim().ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(txtnperson.Text);
            objIOtran.UNIT = Convert.ToInt32(ddlUnit.SelectedValue);
            if (rdbCheque.SelectedValue == "")
            {
                objIOtran.CHEUQE_ID = 0;
            }
            else
            {
                objIOtran.CHEUQE_ID = Convert.ToInt32(rdbCheque.SelectedValue);
            }

            DataTable dtCheque;
            dtCheque = (DataTable)Session["RecChequeTbl"];
            objIOtran.CHEQUE_DETAILS_TABLE = dtCheque;
            

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    objIOtran.CREATOR = (Session["userno"].ToString());
                    CustomStatus cs = (CustomStatus)objIOtranc.AddOutwardDispatch(objIOtran, objCM, to, remark, address, citynumber, pinno, addLine, state, country, contactno);
                    if (Convert.ToInt32(cs) != -99)
                    {
                        Clear_Controls();
                        lvTo.Visible = false;    //25/01/2022
                        ViewState["action"] = null;
                        BindListViewOutwardDispatch();
                        objCommon.DisplayUserMessage(updActivity, "Record Saved Successfully.", this);
                    }
                }
                else
                {
                    if (ViewState["IOTRANNO"] != null)
                    {
                        DateTime Dt = Convert.ToDateTime(ViewState["DT"]);
                        objIOtran.IOTRANNO = Convert.ToInt32(ViewState["IOTRANNO"].ToString());
                        objIOtran.CREATOR = (ViewState["CREATOR"].ToString());
                        CustomStatus cs = (CustomStatus)objIOtranc.UpdateOutwardDispatch(objIOtran, objCM, to, remark, Dt, address, citynumber, pinno, addLine, state, country, contactno);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear_Controls();
                            lvTo.Visible = false;    //25/01/2022
                            BindListViewOutwardDispatch();
                            ViewState["action"] = null;
                            Session["RecTbl"] = null;
                            objCommon.DisplayUserMessage(updActivity, "Record Updated Successfully.", this);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Clear_Controls()
    {
        ddlDepartment.SelectedIndex = 0;
        txtContNo.Text = string.Empty;
        txtDT.Text = System.DateTime.Now.ToString();
        ddlPostType.SelectedIndex = 0;
        txtTo.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtMulAddress.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtRefNo.Text = string.Empty;
        //txtcentralrefno.Text = string.Empty;
        txtCost.Text = string.Empty;
        txtExtraCost.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtbank.Text = string.Empty;

        txtDDAmt.Text = string.Empty;
        txtDDdate.Text = string.Empty;
        txtDDNo.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        txtPIN.Text = string.Empty;

        txtAddLine.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;

        ddlCarrier.SelectedIndex = 0;
        ddlLCat.SelectedIndex = 0;
        txtTrackNo.Text = string.Empty;
        txtScost.Text = string.Empty;
        ViewState["IOTRANNO"] = null;
        ViewState["action"] = "edit";
        txtSentDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        DataSet ds = objIOtranc.GetRefernceNo("O");
        txtcentralrefno.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();

        Session["RecTbl"] = null;
        ViewState["SRNO"] = null;
        lvChequeDetails.DataSource = null;
        lvChequeDetails.DataBind();
        txtnperson.Text = string.Empty;

    }

    protected void ClearRec()
    {
        txtTo.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtMulAddress.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        txtPIN.Text = string.Empty;
        txtAddLine.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtContNo.Text = string.Empty;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            int IOTRANNO = int.Parse(btnEdit.CommandArgument);
            ViewState["IOTRANNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            int Creator = Convert.ToInt32(objCommon.LookUp("ADMN_IO_CC_TRAN", "CREATOR", "IOTRANNO=" + IOTRANNO));
            ViewState["CREATOR"] = Creator;
            DateTime Dt = Convert.ToDateTime(objCommon.LookUp("ADMN_IO_TRAN", "ISNULL(DEPTENTRYDT, DEPTRECSENTDT)", "IOTRANNO=" + IOTRANNO));
            ViewState["DT"] = Dt;
            this.ShowDetails(IOTRANNO);

            divPanel.Visible = true;
            divpnlTo.Visible = true;
            divAddTo.Visible = true;
            divList.Visible = true;
            //divDD.Visible = true;
            divDispDetails.Visible = true;
            divSubmit.Visible = true;
            divListview.Visible = false;
            divAddNew.Visible = false;
           // txtTrackNo.Enabled = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Controls();
            DataSet ds = objIOtranc.GetRefernceNo("O");
            txtcentralrefno.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();

            ViewState["action"] = "add";
            Session["RecTbl"] = null;
            ViewState["SRNO"] = 1;
            lvTo.DataSource = null;
            lvTo.DataBind();
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");

            divPanel.Visible = true;
            divpnlTo.Visible = true;
            divAddTo.Visible = true;
            divList.Visible = true;
            //divDD.Visible = true;
            divDispDetails.Visible = true;
            divSubmit.Visible = true;
           
            divListview.Visible = false;
            divAddNew.Visible = false;
            txtTrackNo.Enabled = true;


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnAdd_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void rdbCheque_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbCheque.SelectedValue == "1")
        {
            lblCheque.Text = "Chq.No.";
            txtDDNo.Focus();
        }
        else
        {
            lblCheque.Text = "DD.No.";
            txtDDNo.Focus();
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        //pnlAdd.Visible = false;
        //pnlList.Visible = true;

        divPanel.Visible = false;
        divpnlTo.Visible = false;
        divAddTo.Visible = false;
        divList.Visible = false;
       // divDD.Visible = false;
        divDispDetails.Visible = false;
        divSubmit.Visible = false;

        divListview.Visible = true;
        divAddNew.Visible = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lvTo.Visible = false;
            Clear_Controls();
            return;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int IOTRANNO)
    {
        try
        {
            int status;
            DataSet ds = objIOtranc.GetOutwardDispatchByOutwardNo(IOTRANNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcentralrefno.Text = ds.Tables[0].Rows[0]["CENTRALREFERENCENO"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["FROMDEPT"].ToString();
                txtDT.Text = ds.Tables[0].Rows[0]["DEPTRECSENTDT"].ToString();
                //txtMulAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                //ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                //txtPIN.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                txtRefNo.Text = ds.Tables[0].Rows[0]["DEPTREFERENCENO"].ToString();
                txtDDNo.Text = ds.Tables[0].Rows[0]["CHQDDNO"].ToString();
                txtDDAmt.Text = ds.Tables[0].Rows[0]["CHEQAMT"].ToString();
                txtDDdate.Text = ds.Tables[0].Rows[0]["CHEQDT"].ToString();
                txtbank.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtSentDT.Text = ds.Tables[0].Rows[0]["CENTRALRECSENTDT"].ToString();
                txtScost.Text = ds.Tables[0].Rows[0]["COST"].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                txtExtraCost.Text = ds.Tables[0].Rows[0]["EXTRACOST"].ToString();
                ddlPostType.SelectedValue = ds.Tables[0].Rows[0]["POSTTYPENO"].ToString();
                ddlLCat.SelectedValue = ds.Tables[0].Rows[0]["LETTER_CAT"].ToString();
                txtTrackNo.Text = ds.Tables[0].Rows[0]["TRACKING_NO"].ToString();
                ddlCarrier.SelectedValue = ds.Tables[0].Rows[0]["CARRIERNO"].ToString();
                txtnperson.Text = ds.Tables[0].Rows[0]["NO_OF_PERSON"].ToString();
                txtCost.Text = ds.Tables[0].Rows[0]["TOTAL_COST"].ToString();
                status = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                txtTrackNo.Text = ds.Tables[0].Rows[0]["TRACKING_NO"].ToString().Equals(string.Empty) ? string.Empty : ds.Tables[0].Rows[0]["TRACKING_NO"].ToString();      //24/06/2022       //24/06/2022

                if (ds.Tables[0].Rows[0]["CHEUQE_ID"].ToString() != "0")
                {
                    rdbCheque.SelectedValue = ds.Tables[0].Rows[0]["CHEUQE_ID"].ToString();
                }
                DataSet dsRec = objIOtranc.GetRecieverOutwardDispatchByIotranNo(IOTRANNO);
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    lvTo.DataSource = dsRec.Tables[0];
                    lvTo.DataBind();
                    Session["RecTbl"] = dsRec.Tables[0];
                    ViewState["SRNO"] = Convert.ToInt32(dsRec.Tables[0].Rows.Count);
                }

                DataSet dsCheq = objIOtranc.GetChequeDetails(IOTRANNO);
                if (dsCheq.Tables[0].Rows.Count > 0)
                {
                    lvChequeDetails.DataSource = dsCheq;
                    lvChequeDetails.DataBind();
                    lvChequeDetails.Visible = true;

                    Session["RecChequeTbl"] = dsCheq.Tables[0];
                }
                else
                {
                    lvChequeDetails.DataSource = null;
                    lvChequeDetails.DataBind();
                    lvChequeDetails.Visible = false;
                    Session["RecChequeTbl"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtWeight_TextChanged(object sender, EventArgs e)   
    {
        if (!(txtWeight.Text.Trim().Equals(string.Empty)))
        {

            // txtScost.Text = objCommon.LookUp("ADMN_IO_WEIGHT", "COST", " '" + txtWeight.Text.Trim() + "' between  WEIGHTFROM and WEIGHTTO and POSTTYPENO='" + Convert.ToInt32(ddlPostType.SelectedValue) + "' and unit='" + Convert.ToString(ddlUnit.SelectedItem) + "'");

            // BY MRUNAL SINGH ON 06-12-2014
            txtScost.Text = objCommon.LookUp("ADMN_IO_WEIGHT", "COST", " '" + txtWeight.Text.Trim() + "' between  WEIGHTFROM and WEIGHTTO and POSTTYPENO='" + Convert.ToInt32(ddlPostType.SelectedValue) + "' and unit='" + Convert.ToString(ddlUnit.SelectedValue) + "'");

            double scost = txtScost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtScost.Text);
            if (scost == 0)
            {
                objCommon.DisplayUserMessage(updActivity, "Weight not defined/Please Select Post Type.", this);
                txtWeight.Text = string.Empty;
            }
            int nper = txtnperson.Text.Trim().Equals(string.Empty) ? 1 : Int32.Parse(txtnperson.Text);
            double cost = (scost * nper);
            txtCost.Text = cost.ToString();
            txtnperson.Focus();
        }
    }

    protected void txtnperson_TextChanged(object sender, EventArgs e)
    {
        if (!(txtnperson.Text.Trim().Equals(string.Empty)))
        {
            //txtScost.Text = objCommon.LookUp("ADMN_IO_WEIGHT", "COST", " '" + txtWeight.Text.Trim() + "' between  WEIGHTFROM and WEIGHTTO and POSTTYPENO='" + Convert.ToInt32(ddlPostType.SelectedValue) + "' and unit='" + Convert.ToString(ddlUnit.SelectedValue) + "'");

            //double scost = txtScost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtScost.Text);
            //int nper = txtnperson.Text.Trim().Equals(string.Empty) ? 1 : Int32.Parse(txtnperson.Text);
            //double cost = (scost * nper);
            //txtCost.Text = cost.ToString();
            //txtExtraCost.Text = string.Empty;        // gayatri rode 19/01/2021
            //txtExtraCost.Focus();


            txtScost.Text = objCommon.LookUp("ADMN_IO_WEIGHT", "COST", " '" + txtWeight.Text.Trim() + "' between  WEIGHTFROM and WEIGHTTO and POSTTYPENO='" + Convert.ToInt32(ddlPostType.SelectedValue) + "' and unit='" + Convert.ToString(ddlUnit.SelectedValue) + "'");

            double scost = txtScost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtScost.Text);
            int nper = txtnperson.Text.Trim().Equals(string.Empty) ? 1 : Int32.Parse(txtnperson.Text);
            double Ecost = txtExtraCost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtExtraCost.Text);
            double cost = (scost * nper);
            cost = (scost * nper) + Ecost;
            txtCost.Text = cost.ToString();



        }

    }

    protected void txtExtraCost_TextChanged(object sender, EventArgs e)
    {

        if (!(txtExtraCost.Text.Trim().Equals(string.Empty)))
        {
            double scost = txtScost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtScost.Text);
            int nper = txtnperson.Text.Trim().Equals(string.Empty) ? 1 : Int32.Parse(txtnperson.Text);
            double Ecost = txtExtraCost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtExtraCost.Text);
            double cost = (scost * nper) + Ecost;
            txtCost.Text = cost.ToString();
        }
    }
    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (!(txtWeight.Text == string.Empty))
        {
            txtScost.Text = objCommon.LookUp("ADMN_IO_WEIGHT", "COST", " ( WEIGHTFROM <=" + txtWeight.Text.Trim() + "  and WEIGHTTO  >=" + txtWeight.Text.Trim() + ")and UNIT='" + Convert.ToString(ddlUnit.SelectedValue) + "' and POSTTYPENO=" + Convert.ToInt32(ddlPostType.SelectedValue));
            double scost = txtScost.Text.Trim().Equals(string.Empty) ? 0 : double.Parse(txtScost.Text);
            int nper = txtnperson.Text.Trim().Equals(string.Empty) ? 1 : Int32.Parse(txtnperson.Text);
            double cost = scost;
            txtCost.Text = cost.ToString();
        }

    }
    private DataTable CreateTabel()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("IOTO", typeof(string)));
        dtRe.Columns.Add(new DataColumn("REMARK", typeof(string)));
        dtRe.Columns.Add(new DataColumn("MULTIPLE_ADDRESS", typeof(string)));
        dtRe.Columns.Add(new DataColumn("CITYNO", typeof(string)));
        dtRe.Columns.Add(new DataColumn("PINNO", typeof(string)));
        dtRe.Columns.Add(new DataColumn("CITYNumber", typeof(int)));
        dtRe.Columns.Add(new DataColumn("ADDLINE", typeof(string)));
        dtRe.Columns.Add(new DataColumn("STATE", typeof(string)));
        dtRe.Columns.Add(new DataColumn("STATENO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("COUNTRY", typeof(string)));
        dtRe.Columns.Add(new DataColumn("COUNTRYNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("CONTACTNO", typeof(string)));
        return dtRe;
    }
    protected void btnAddTo_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["IOTO"] = txtTo.Text.Trim() == null ? string.Empty : Convert.ToString(txtTo.Text.Trim()).Replace(',', ' ');
                dr["REMARK"] = txtRemarks.Text.Trim() == null ? string.Empty : Convert.ToString(txtRemarks.Text.Trim()).Replace(',', ' ');
                dr["MULTIPLE_ADDRESS"] = txtMulAddress.Text.Trim() == null ? string.Empty : Convert.ToString(txtMulAddress.Text.Trim()).Replace(',', ' ');

                dr["CITYNO"] = ddlCity.SelectedItem.Text == "Please Select" ? string.Empty : Convert.ToString(ddlCity.SelectedItem.Text);

                dr["PINNO"] = txtPIN.Text.Trim() == null ? string.Empty : Convert.ToString(txtPIN.Text.Trim());
                dr["CITYNumber"] = Convert.ToInt32(ddlCity.SelectedValue);
                dr["ADDLINE"] = txtAddLine.Text.Trim() == null ? string.Empty : Convert.ToString(txtAddLine.Text.Trim()).Replace(',', ' ');

                dr["STATE"] = ddlState.SelectedItem.Text == "Please Select" ? string.Empty : Convert.ToString(ddlState.SelectedItem.Text);

                dr["STATENO"] = Convert.ToInt32(ddlState.SelectedValue);

                dr["COUNTRY"] = ddlCountry.SelectedItem.Text == "Please Select" ? string.Empty : Convert.ToString(ddlCountry.SelectedItem.Text);

                dr["COUNTRYNO"] = Convert.ToInt32(ddlCountry.SelectedValue);
                dr["CONTACTNO"] = txtContNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtContNo.Text.Trim()).Replace(',', ' ');

                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
                lvTo.Visible = true;    //25/01/2022
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["IOTO"] = txtTo.Text.Trim() == null ? string.Empty : Convert.ToString(txtTo.Text.Trim()).Replace(',', ' ');
                dr["REMARK"] = txtRemarks.Text.Trim() == null ? string.Empty : Convert.ToString(txtRemarks.Text.Trim()).Replace(',', ' ');
                dr["MULTIPLE_ADDRESS"] = txtMulAddress.Text.Trim() == null ? string.Empty : Convert.ToString(txtMulAddress.Text.Trim()).Replace(',', ' ');
                dr["CITYNO"] = ddlCity.SelectedItem.Text == "Please Select" ? string.Empty : Convert.ToString(ddlCity.SelectedItem.Text);
                //dr["CITYNO"] = ddlCity.SelectedItem == null ? string.Empty : Convert.ToString(ddlCity.SelectedItem);
                dr["PINNO"] = txtPIN.Text.Trim() == null ? string.Empty : Convert.ToString(txtPIN.Text.Trim());
                dr["CITYNumber"] = Convert.ToInt32(ddlCity.SelectedValue);
                dr["ADDLINE"] = txtAddLine.Text.Trim() == null ? string.Empty : Convert.ToString(txtAddLine.Text.Trim()).Replace(',', ' ');
                dr["STATE"] = ddlState.SelectedItem.Text == "Please Select" ? string.Empty : Convert.ToString(ddlState.SelectedItem.Text);
                dr["STATENO"] = Convert.ToInt32(ddlState.SelectedValue);
                dr["COUNTRY"] = ddlCountry.SelectedItem.Text == "Please Select" ? string.Empty : Convert.ToString(ddlCountry.SelectedItem.Text);
                dr["COUNTRYNO"] = Convert.ToInt32(ddlCountry.SelectedValue);
                dr["CONTACTNO"] = txtContNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtContNo.Text.Trim()).Replace(',', ' ');

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnAddTo_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEditRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = ((DataTable)Session["RecTbl"]);

                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);

                txtTo.Text = dr["IOTO"].ToString();
                txtRemarks.Text = dr["REMARK"].ToString();
                txtMulAddress.Text = dr["MULTIPLE_ADDRESS"].ToString();
                ddlCity.SelectedValue = dr["CITYNumber"].ToString();
                txtPIN.Text = dr["PINNO"].ToString();
                txtAddLine.Text = dr["ADDLINE"].ToString();
                ddlState.SelectedValue = dr["STATENO"].ToString();
                ddlCountry.SelectedValue = dr["COUNTRYNO"].ToString();
                txtContNo.Text = dr["CONTACTNO"].ToString();

                dt.Rows.Remove(dr);
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
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
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    protected void ddlPostType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtScost.Text = "";
        txtCost.Text = "";
        txtWeight.Text = "";
        txtnperson.Text = "";
        txtExtraCost.Text = "";

    }

    protected void btnDeleteRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int IOTRANNO = int.Parse(btnDelete.CommandArgument);
            ViewState["IOTRANNO"] = int.Parse(btnDelete.CommandArgument);

            CustomStatus cs = (CustomStatus)objIOtranc.DeleteDeptOutward(IOTRANNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear_Controls();
                BindListViewOutwardDispatch();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted.');", true);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Cheque Details

    private DataTable CreateChequeTable()
    {
        DataTable dtCheque = new DataTable();
        dtCheque.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtCheque.Columns.Add(new DataColumn("CHEQUE_TYPE", typeof(int)));
        dtCheque.Columns.Add(new DataColumn("CHEQUE_NO", typeof(int)));
        dtCheque.Columns.Add(new DataColumn("CHEQUE_AMOUNT", typeof(double)));
        dtCheque.Columns.Add(new DataColumn("CHEQUE_DATE", typeof(DateTime)));
        dtCheque.Columns.Add(new DataColumn("CHEQUE_BANK", typeof(string)));
        return dtCheque;
    }

    protected void btnAddCheque_Click(object sender, EventArgs e)
    {
        try
        {
            lvChequeDetails.Visible = true;

            if (Session["RecChequeTbl"] != null && ((DataTable)Session["RecChequeTbl"]) != null)
            {
                int maxVal = 0;
                DataTable dt = (DataTable)Session["RecChequeTbl"];
                DataRow dr = dt.NewRow();

                if (dr != null)
                {
                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                }
                if (ViewState["EDIT_SRNO"] != null)
                {
                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_SRNO"]);
                }
                else
                {
                    dr["SRNO"] = maxVal + 1;
                }
                dr["CHEQUE_TYPE"] = Convert.ToInt32(rdbCheque.SelectedValue);
                dr["CHEQUE_NO"] = txtDDNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDNo.Text.Trim());
                dr["CHEQUE_AMOUNT"] = txtDDAmt.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDAmt.Text.Trim());
                dr["CHEQUE_DATE"] = txtDDdate.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDdate.Text.Trim());
                dr["CHEQUE_BANK"] = txtbank.Text.Trim() == null ? string.Empty : Convert.ToString(txtbank.Text.Trim());


                dt.Rows.Add(dr);
                Session["RecChequeTbl"] = dt;
                lvChequeDetails.DataSource = dt;
                lvChequeDetails.DataBind();
                ClearChequeRec();
                ViewState["SRNO_CHEQUE"] = Convert.ToInt32(ViewState["SRNO_CHEQUE"]) + 1;
            }
            else
            {
                DataTable dt = this.CreateChequeTable();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO_CHEQUE"]) + 1;
                dr["CHEQUE_TYPE"] = Convert.ToInt32(rdbCheque.SelectedValue);
                dr["CHEQUE_NO"] = txtDDNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDNo.Text.Trim());
                dr["CHEQUE_AMOUNT"] = txtDDAmt.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDAmt.Text.Trim());
                dr["CHEQUE_DATE"] = txtDDdate.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDdate.Text.Trim());
                dr["CHEQUE_BANK"] = txtbank.Text.Trim() == null ? string.Empty : Convert.ToString(txtbank.Text.Trim());

                ViewState["SRNO_CHEQUE"] = Convert.ToInt32(ViewState["SRNO_CHEQUE"]) + 1;
                dt.Rows.Add(dr);
                ClearChequeRec();
                Session["RecChequeTbl"] = dt;
                lvChequeDetails.DataSource = dt;
                lvChequeDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ClearChequeRec()
    {
        rdbCheque.SelectedIndex = 0;
        lblCheque.Text = "Chq.No";
        txtDDNo.Text = string.Empty;
        txtDDAmt.Text = string.Empty;
        txtDDdate.Text = string.Empty;
        txtbank.Text = string.Empty;
        ViewState["actionContent"] = null;
        ViewState["EDIT_SRNO"] = null;
    }

    protected void btnEditChequeRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditChequeRec = sender as ImageButton;
            DataTable dt;
            if (Session["RecChequeTbl"] != null && ((DataTable)Session["RecChequeTbl"]) != null)
            {
                dt = ((DataTable)Session["RecChequeTbl"]);
                ViewState["EDIT_SRNO"] = btnEditChequeRec.CommandArgument;
                DataRow dr = this.GetEditableChequeDatarow(dt, btnEditChequeRec.CommandArgument);
                txtDDNo.Text = dr["CHEQUE_NO"].ToString();
                txtDDAmt.Text = dr["CHEQUE_AMOUNT"].ToString();
                txtDDdate.Text = Convert.ToDateTime(dr["CHEQUE_DATE"]).ToString("dd/MM/yyyy");
                txtbank.Text = dr["CHEQUE_BANK"].ToString();
                rdbCheque.SelectedValue = dr["CHEQUE_TYPE"].ToString();
                if (rdbCheque.SelectedValue == "1")
                {
                    lblCheque.Text = "Chq.No.";
                }
                else
                {
                    lblCheque.Text = "DD.No.";
                }

                dt.Rows.Remove(dr);
                Session["RecChequeTbl"] = dt;
                lvChequeDetails.DataSource = dt;
                lvChequeDetails.DataBind();
                ViewState["actionContent"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableChequeDatarow(DataTable dt, string value)
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
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }



    #endregion
}