//================================================
//MODIFIED BY   : MRUNAL SINGH
//MODIFIED DATE : 06-12-2014
//DESCRIPTION   : TO SAVE ADDRESS OF THE MULTIPLE RECEIVERS FOR DEPARTMENT DISPATCH OUTWARD
//================================================

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;  
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Dispatch_Transactions_IO_OutwardDeptEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();
    IOTranController objIO = new IOTranController();
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
                    //pnlList.Visible = true;
                   // pnlAdd.Visible = false;
                    PopulateDropdown();
                    lvTo.DataSource = null;
                    lvTo.DataBind();
                    ViewState["action"] = null;
                    BindListViewOutward();
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;
                    ViewState["IOTRANNO"] = null;

                    txtSentDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    hdnDate.Value = System.DateTime.Now.ToString("dd/MM/yyyy");
                }               
            }          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDeptEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDeptEntry.aspx");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
           // objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "POSTTYPENO > 0", "POSTTYPENAME");
            objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "ACTIVESTATUS > 0", "POSTTYPENAME");  //08-03-2022 GAYATRI  RODE
            objCommon.FillDropDownList(ddlCarrier, "ADMN_IO_CARRIER", "CARRIERNO", "CARRIERNAME", "STATUS=0", "CARRIERNAME");
            objCommon.FillDropDownList(ddlCity, "acd_city", "CITYNO", "CITY", "CITYNO > 0", "CITY");
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");
            objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.PopulateDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListViewOutward()
    {
        try
        {
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_NO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
            DataSet ds = objIOtranc.GetAllOutward(deptno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IvOutward.DataSource = ds;
                IvOutward.DataBind();

            }
            else
            {
                IvOutward.DataSource = null;
                IvOutward.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.BindListViewOutward -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpLett_PreRender(object sender, EventArgs e)
    {
        BindListViewOutward();
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
            //--- Receiver Multiple Detail By MRUNAL
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
                    {
                        remark = dr["REMARK"].ToString();
                    }
                    else
                    {
                        remark += "," + dr["REMARK"].ToString();
                    }
                    if (address.Trim().Equals(string.Empty))
                    {
                        address = dr["MULTIPLE_ADDRESS"].ToString();
                    }
                    else
                    {
                        address += "," + dr["MULTIPLE_ADDRESS"].ToString();
                    }
                    if (cityno.Trim().Equals(string.Empty))
                    {
                        cityno = dr["CITYNO"].ToString();
                    }
                    else
                    {
                        cityno += "," + dr["CITYNO"].ToString();
                    }
                    if (citynumber.Trim().Equals(string.Empty))
                    {
                        citynumber = dr["CITYNumber"].ToString();
                    }
                    else
                    {
                        citynumber += "," + dr["CITYNumber"].ToString();
                    }
                    if (pinno.Trim().Equals(string.Empty))
                    {
                        pinno = dr["PINNO"].ToString();
                    }
                    else
                    {
                        pinno += "," + dr["PINNO"].ToString();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please Enter Receiver Details.", this.Page);
                return;
            }
            DataSet ds = objIOtranc.GetRefernceNo("O");
            objIOtran.CENTRALREFERENCENO = ds.Tables[0].Rows[0]["REFNO"].ToString();
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_EMPDEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

            objIOtran.IOTYPE = 'O';
            objIOtran.DEPTRECSENTDT = Convert.ToDateTime(txtSentDT.Text);

            objIOtran.IOTO = to1;
            //objIOtran.ADDRESS = txtAddress.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtAddress.Text.Trim().ToString();
            //objIOtran.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            //objIOtran.PINCODE = txtPIN.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtPIN.Text.Trim().ToString();
            objIOtran.SUBJECT = txtSubject.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtSubject.Text.Trim().ToString();
            objIOtran.DEPTREFERENCENO = txtRefNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtRefNo.Text.Trim().ToString();
            objIOtran.CHQDDNO = txtDDNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtDDNo.Text.Trim().ToString();
            objIOtran.CHEQDT = txtDDdate.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDDdate.Text);
            objIOtran.CHEQAMT = txtDDAmt.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtDDAmt.Text);
            objIOtran.BANKNAME = txtbank.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtbank.Text.Trim().ToString();

            objIOtran.TRACKING_NO = txtTrackNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtTrackNo.Text.Trim().ToString();  //24/06/2022
            objIOtran.FROMDEPT = deptno;

            objIOtran.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objIOtran.CREATOR = (Session["userno"].ToString());
            objIOtran.CREATED_DATE = System.DateTime.Now;

            objIOtran.POSTTYPENO = Convert.ToInt32(ddlPostType.SelectedValue);
            objCM.carrierNo = Convert.ToInt32(ddlCarrier.SelectedValue);
            objCM.letterCategory = Convert.ToInt32(ddlLCat.SelectedValue);

            objIOtran.RFID = lblRFID.Text;
            if (rdbCheque.SelectedValue == "")
            {
                objIOtran.CHEUQE_ID = 0;
            }
            else
            {
                objIOtran.CHEUQE_ID = Convert.ToInt32(rdbCheque.SelectedValue);
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objIOtranc.AddOutwardEntry(objIOtran, objCM, to, remark, address, citynumber, pinno, addLine, state, country, contactno);
                    if (Convert.ToInt32(cs) != -99)
                    {
                        //Clear_Controls();
                        //ViewState["action"] = "add";
                        //Session["RecTbl"] = null;
                        //BindListViewOutward();
                        //pnlList.Visible = true;

                        //// pnlAdd.Visible = false;
                        //pnlRpt.Visible = true;
                        //objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);




                        Clear_Controls();                                                          // 19/01/2021
                        ViewState["action"] = "add";
                        Session["RecTbl"] = null;
                      
                        divpnlRpt.Visible = true;
                        divAddNew.Visible = true;
                        pnlRpt.Visible = true;
                        divListview.Visible = true;
                        divList.Visible = true;
                        BindListViewOutward();
                        divPanel.Visible = false;
                        divpnlTo.Visible = false;
                        divAddTo.Visible = false;
                        divList.Visible = false;
                        divFrom.Visible = false;
                        divDispDetails.Visible = false;
                        divSubmit.Visible = false;
                        divNote.Visible = false;
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                        

                    }
                }
                else
                {
                    if (ViewState["IOTRANNO"] != null)
                    {
                        objIOtran.IOTRANNO = Convert.ToInt32(ViewState["IOTRANNO"].ToString());

                        CustomStatus cs = (CustomStatus)objIOtranc.UpdateOutwardEntry(objIOtran, objCM, to, remark, address, citynumber, pinno, addLine, state, country, contactno);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear_Controls();
                           // BindListViewOutward();
                            ViewState["action"] = "add";
                            Session["RecTbl"] = null;
                            //pnlList.Visible = true;                   
                            //pnlRpt.Visible = true;


                            divpnlRpt.Visible = true;
                            divAddNew.Visible = true;
                            pnlRpt.Visible = true;
                            divListview.Visible = true;
                            divList.Visible = true;
                            BindListViewOutward();
                            divPanel.Visible = false;
                            divpnlTo.Visible = false;
                            divAddTo.Visible = false;
                            divList.Visible = false;
                            divFrom.Visible = false;
                            divDispDetails.Visible = false;
                            divSubmit.Visible = false;
                            divNote.Visible = false;
                           
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void Clear_Controls()
    {
        txtSentDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtTo.Text = string.Empty;
        //txtAddress.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        txtPIN.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtRefNo.Text = string.Empty;
        txtbank.Text = string.Empty;
        txtDDAmt.Text = string.Empty;
        txtDDdate.Text = string.Empty;
        txtDDNo.Text = string.Empty;
        lvTo.DataSource = null;
        lvTo.DataBind();
        ViewState["action"] = "add";
        ddlCarrier.SelectedIndex = 0;
        ddlPostType.SelectedIndex = 0;
        ddlLCat.SelectedIndex = 0;
        ViewState["IOTRANNO"] = null;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int IOTRANNO = int.Parse(btnEdit.CommandArgument);


          //28/01/2022 All user Can not Edit Accept Record Only Admin Can Do 
            if (Convert.ToInt32(Session["userno"]) != 1)
            {
                DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN T inner join ADMN_IO_CC_TRAN C on (T.IOTRANNO=C.IOTRANNO)", "T.IOTRANNO", "T.ACCEPT_REJECT", "T.IOTRANNO=" + IOTRANNO, "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string s = ds.Tables[0].Rows[0]["ACCEPT_REJECT"].ToString();
                    if (s == "A")
                    {
                        objCommon.DisplayMessage(this.updActivity, "Accepted Record Can Not Be Modify.", this.Page);
                        return;
                    }
                    //else if (s == "R")
                    //{
                    //    objCommon.DisplayMessage(this.updActivity, "Rejected Record Can Not Be Modify.", this.Page);               
                    //    return;
                    //}
                }
            }
                    
            ViewState["IOTRANNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(IOTRANNO);       
            divpnlRpt.Visible = false;
            divAddNew.Visible = false;
            divListview.Visible = false;

            divPanel.Visible = true;
            divpnlTo.Visible = true;
            divAddTo.Visible = true;
            divList.Visible = true;
            divFrom.Visible = true; 
            divDispDetails.Visible = true;
            divSubmit.Visible = true;
            divNote.Visible = true; 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objIOtranc.GetRefernceNo("O");
            txtcentralrefno.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();
            DataSet dsInfo = objIOtranc.GetFromInfo(Convert.ToInt32(Session["idno"]));
            if (dsInfo.Tables[0].Rows.Count > 0)
            {
                lblRFID.Text = dsInfo.Tables[0].Rows[0]["PFILENO"].ToString();
                lblEmpName.Text = dsInfo.Tables[0].Rows[0]["NAME"].ToString();
                lblDesig.Text = dsInfo.Tables[0].Rows[0]["SUBDESIG"].ToString();
                lblDept.Text = dsInfo.Tables[0].Rows[0]["SUBDEPT"].ToString();
                lblMobile.Text = dsInfo.Tables[0].Rows[0]["PHONENO"].ToString();
            }

           
            lblAddress.Text = objCommon.LookUp("reff", "college_address", string.Empty);
            pnlRpt.Visible = false;
            Clear_Controls();          
            ViewState["action"] = "add";
            Session["RecTbl"] = null;
            Panel2.Visible = false;
            btnSubmit.Enabled = true;

            divpnlRpt.Visible = false;
            divAddNew.Visible = false;
            divListview.Visible = false;

            divPanel.Visible = true;   
            divpnlTo.Visible = true;    
            divAddTo.Visible = true;   
            divList.Visible = true;   
            divFrom.Visible = true;
            divDispDetails.Visible = true;   
            divSubmit.Visible = true;
            divNote.Visible = true; 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnAdd_Click -> " + ex.Message + " " + ex.StackTrace);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int IOTRANNO)
    {
        try
        {
            int status;
            DataSet ds = objIOtranc.GetOutwardByOutwardNo(IOTRANNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSentDT.Text = ds.Tables[0].Rows[0]["DEPTRECSENTDT"].ToString();
                //txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                //ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();               
                //txtPIN.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();               
                txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                txtRefNo.Text = ds.Tables[0].Rows[0]["DEPTREFERENCENO"].ToString();
                txtDDNo.Text = ds.Tables[0].Rows[0]["CHQDDNO"].ToString();
                txtDDAmt.Text = ds.Tables[0].Rows[0]["CHEQAMT"].ToString();
                txtDDdate.Text = ds.Tables[0].Rows[0]["CHEQDT"].ToString();
                txtbank.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtcentralrefno.Text = ds.Tables[0].Rows[0]["CENTRALREFERENCENO"].ToString();
                ddlPostType.SelectedValue = ds.Tables[0].Rows[0]["POSTTYPENO"].ToString();
                ddlCarrier.SelectedValue = ds.Tables[0].Rows[0]["CARRIERNO"].ToString();
                ddlLCat.SelectedValue = ds.Tables[0].Rows[0]["LETTER_CAT"].ToString();

                txtTrackNo.Text = ds.Tables[0].Rows[0]["TRACKING_NO"].ToString();

                if (ds.Tables[0].Rows[0]["CHEUQE_ID"].ToString() != "0")
                {
                    rdbCheque.SelectedValue = ds.Tables[0].Rows[0]["CHEUQE_ID"].ToString();
                }

                status = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());

                DataSet dsRec = objIOtranc.GetRecieverOutwardDispatchByIotranNo(IOTRANNO);
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    lvTo.DataSource = dsRec.Tables[0];
                    lvTo.DataBind();
                    Session["RecTbl"] = dsRec.Tables[0];
                    ViewState["SRNO"] = Convert.ToInt32(dsRec.Tables[0].Rows.Count);
                }
                DataSet dsInfo = objIOtranc.GetFromInfo(Convert.ToInt32(Session["idno"]));
                if (dsInfo.Tables[0].Rows.Count > 0)
                {
                    lblRFID.Text = dsInfo.Tables[0].Rows[0]["PFILENO"].ToString();
                    lblEmpName.Text = dsInfo.Tables[0].Rows[0]["NAME"].ToString();
                    lblDesig.Text = dsInfo.Tables[0].Rows[0]["SUBDESIG"].ToString();
                    lblDept.Text = dsInfo.Tables[0].Rows[0]["SUBDEPT"].ToString();
                    lblMobile.Text = dsInfo.Tables[0].Rows[0]["PHONENO"].ToString();
                }
                //string coladdress
                lblAddress.Text = objCommon.LookUp("reff", "college_address", string.Empty);

                if (status == 1)
                {

                    Panel1.Enabled = false;
                    pnlTo.Enabled = false;
                    pnlDD.Enabled = false;
                    Panel2.Enabled = false;
                    Panel2.Visible = true;
                    btnSubmit.Enabled = false;

                    txtSendDetails.Text = ds.Tables[0].Rows[0]["CENTRALRECSENTDT"].ToString();
                    txtRef.Text = ds.Tables[0].Rows[0]["CENTRALREFERENCENO"].ToString();
                    txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                    ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["UNIT"].ToString();
                    txtScost.Text = ds.Tables[0].Rows[0]["COST"].ToString();
                    txtnperson.Text = ds.Tables[0].Rows[0]["NO_OF_PERSON"].ToString();
                    txtExtraCost.Text = ds.Tables[0].Rows[0]["EXTRACOST"].ToString();
                    txtCost.Text = ds.Tables[0].Rows[0]["TOTAL_COST"].ToString();
                }
                else
                {
                    Panel1.Enabled = true;
                    pnlTo.Enabled = true;
                    pnlDD.Enabled = true;
                    Panel2.Visible = false;
                    btnSubmit.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_NO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

            DataSet dsdate = objIOtranc.GetAllOutwardDatewise(Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd").Trim(), Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd").Trim(), deptno);
           

            

            if (dsdate.Tables[0].Rows.Count > 0)
            {
                IvOutward.DataSource = dsdate;
                IvOutward.DataBind();
            }
            else
            {
                IvOutward.DataSource = null;
                IvOutward.DataBind();
                objCommon.DisplayMessage(this.updActivity, "Record Not Found...!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_NO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("DISPATCH")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,DISPATCH," + rptFileName;
            url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@P_DEPTNO=" + deptno + ",@P_USERNO=0,@P_POSTTYPE=0,@P_CARRIERNO=0,@P_LETTERCAT=0,@P_CHEQUE=0,username=" + Session["userfullname"].ToString() + ",@P_TYPE=0";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updActivity, updActivity.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_OutwardDeptEntry.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnBack_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                dr["CITYNO"] = ddlCity.SelectedItem == null ? string.Empty : Convert.ToString(ddlCity.SelectedItem);
                dr["PINNO"] = txtPIN.Text.Trim() == "" ? "0" : Convert.ToString(txtPIN.Text.Trim());
                dr["CITYNumber"] = Convert.ToInt32(ddlCity.SelectedValue);
                dr["ADDLINE"] = txtAddLine.Text.Trim() == null ? string.Empty : Convert.ToString(txtAddLine.Text.Trim()).Replace(',', ' ');
                dr["STATE"] = ddlState.SelectedItem == null ? string.Empty : Convert.ToString(ddlState.SelectedItem);
                dr["STATENO"] = Convert.ToInt32(ddlState.SelectedValue);
                dr["COUNTRY"] = ddlCountry.SelectedIndex == 0 ? string.Empty : Convert.ToString(ddlCountry.SelectedItem);//Updated by Vijay Andoju on 07-MAR-2020
               // dr["COUNTRY"] = ddlCountry.SelectedItem == 0 ? string.Empty : Convert.ToString(ddlCountry.SelectedItem);//OLD
                dr["COUNTRYNO"] = Convert.ToInt32(ddlCountry.SelectedValue);
                dr["CONTACTNO"] = txtContNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtContNo.Text.Trim()).Replace(',', ' ');

                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
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
                dr["CITYNO"] = ddlCity.SelectedItem == null ? string.Empty : Convert.ToString(ddlCity.SelectedItem);
                dr["PINNO"] = txtPIN.Text.Trim() == "" ? string.Empty : Convert.ToString(txtPIN.Text.Trim());
                dr["CITYNumber"] = Convert.ToInt32(ddlCity.SelectedValue);
                dr["ADDLINE"] = txtAddLine.Text.Trim() == null ? string.Empty : Convert.ToString(txtAddLine.Text.Trim()).Replace(',', ' ');
                dr["STATE"] = ddlState.SelectedItem == null ? string.Empty : Convert.ToString(ddlState.SelectedItem);
                dr["STATENO"] = Convert.ToInt32(ddlState.SelectedValue);
                dr["COUNTRY"] = ddlCountry.SelectedIndex == 0 ? string.Empty : Convert.ToString(ddlCountry.SelectedItem);//Updated by Vijay Andoju on 07-MAR-2020
                // dr["COUNTRY"] = ddlCountry.SelectedItem == 0 ? string.Empty : Convert.ToString(ddlCountry.SelectedItem);//OLD
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int IOTRANNO = Convert.ToInt32(btnDelete.CommandArgument);
            //28/01/2022
            //DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN T inner join ADMN_IO_CC_TRAN C on (T.IOTRANNO=C.IOTRANNO)", "T.IOTRANNO", "T.ACCEPT_REJECT", "T.IOTRANNO=" + IOTRANNO, "");

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string s = ds.Tables[0].Rows[0]["ACCEPT_REJECT"].ToString();
            //    if (s == "A")
            //    {
            //        objCommon.DisplayMessage(this.updActivity, "Approved Record Can Not Be Modify.", this.Page);
            //        return;
            //    }
            //    //else if (s == "R")
            //    //{
            //    //    objCommon.DisplayMessage(this.updActivity, "Rejected Record Can Not Be Modify.", this.Page);               
            //    //    return;
            //    //}
            //}


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
    protected void ClearRec()
    {
        txtTo.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtMulAddress.Text = string.Empty;
        txtPIN.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        txtAddLine.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtContNo.Text = string.Empty;
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        int deptno = Convert.ToInt32(Session["userno"]);

        DataSet ds = objIO.VerifyOutwardRecordDept(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(deptno), 0, 0, 0, 0, 0, 0);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ShowReport("Dispatch", "OutwardRegister.rpt");
        }
        else
        {

            objCommon.DisplayMessage(this.updActivity, "Record Not Found.", this.Page);
            return;
        }
    }

    protected void btnDeleteRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int IOTRANNO = int.Parse(btnDelete.CommandArgument);
            ViewState["IOTRANNO"] = int.Parse(btnDelete.CommandArgument);
            //28/01/2022 All user Can not Delete Accept Record Only Admin Can Do 
            if (Convert.ToInt32(Session["userno"]) != 1)
            {
                DataSet dss = objCommon.FillDropDown("ADMN_IO_TRAN T inner join ADMN_IO_CC_TRAN C on (T.IOTRANNO=C.IOTRANNO)", "T.IOTRANNO", "T.ACCEPT_REJECT", "T.IOTRANNO=" + IOTRANNO, "");

                if (dss.Tables[0].Rows.Count > 0)
                {
                    string s = dss.Tables[0].Rows[0]["ACCEPT_REJECT"].ToString();
                    if (s == "A")
                    {
                        objCommon.DisplayMessage(this.updActivity, "Accepted Record Can Not Be Delete.", this.Page);
                        return;
                    }
                    //else if (s == "R")
                    //{
                    //    objCommon.DisplayMessage(this.updActivity, "Rejected Record Can Not Be Modify.", this.Page);               
                    //    return;
                    //}
                }
            }


            DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN", "*", "", "STATUS =1 AND IOTRANNO=" + IOTRANNO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This outward can not be deleted, it is already dispatched.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objIOtranc.DeleteDeptOutward(IOTRANNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear_Controls();
                    BindListViewOutward();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully.');", true);
                }
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

  
}