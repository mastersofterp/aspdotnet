using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Linq;


public partial class Dispatch_Transactions_IO_InwardDispatch : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();

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
                    //pnlAdd.Visible = false;
                    PopulateDropdown();
                    ViewState["action"] = null;
                    BindListViewInward();
                    objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                    objCommon.FillDropDownList(ddlDesig, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "");
                    objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO = B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null", "U.UA_FULLNAME");
                    objCommon.FillDropDownList(ddlUType, "User_Rights", "USERTYPEID", "USERDESC", "USERTYPEID IN (3,5)", "USERTYPEID");
                    objCommon.FillDropDownList(ddlHODdept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                    txtReceivedDT.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;
                    lblCheque.Text = "Chq.No";
                }              
            }
            else
            {
               // trmsg.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.Page_Load --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=IO_InwardDispatch.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_InwardDispatch.aspx");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "POSTTYPENO > 0", "POSTTYPENAME");
            objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "ACTIVESTATUS > 0", "POSTTYPENAME");    // gayatri rode 08-04-2022
            objCommon.FillDropDownList(ddlCity, "acd_city", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlOutRefNo, "ADMN_IO_TRAN", "IOTRANNO", "CENTRALREFERENCENO", "IOTYPE='O' AND CENTRALREFERENCENO<>''", "CENTRALREFERENCENO desc");
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");
            objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.PopulateDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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

    private void BindListViewInward()
    {
        try
        {
            DataSet ds = objIOtranc.GetAllInward(0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvInward.DataSource = ds;
                lvInward.DataBind();
            }
        }
        catch (Exception ex)
        {
            if
                (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.BindListViewInward -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IOTRAN objIOtran = new IOTRAN();
        try
        {
            if (ddlDepartment.SelectedIndex > 0 || ddlUser.SelectedIndex > 0)
            {
                if (ddlUser.SelectedIndex <= 0 || ddlDepartment.SelectedIndex <= 0)
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Select Department and User.", this.Page);
                    return;
                }
            }

            objIOtran.IOTYPE = 'I'; 
            objIOtran.CENTRALRECSENTDT = Convert.ToDateTime(txtReceivedDT.Text);
            objIOtran.CENTRALENTRYDT = System.DateTime.Now;
            objIOtran.CENTRALREFERENCENO = txtDispRefno.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtDispRefno.Text.Trim().ToString();
            objIOtran.OUTREFERENCENO = txtReferenceNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtReferenceNo.Text.Trim().ToString();
            objIOtran.IOFROM = txtFrom.Text.ToString().Trim();
            objIOtran.TOUSER = ddlUser.SelectedItem.Text.Equals("Please Select") ? string.Empty : ddlUser.SelectedItem.Text;  // txtToUser.Text.ToString().Trim();
            objIOtran.ADDRESS = txtAddress.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtAddress.Text.Trim().ToString();
            objIOtran.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            objIOtran.PINCODE = txtPIN.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtPIN.Text.Trim().ToString();
            //objIOtran.LETTERTYPENO = Convert.ToInt32(ddlLetterType.SelectedValue);
            objIOtran.SUBJECT = txtSubject.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtSubject.Text.Trim().ToString();
            objIOtran.POSTTYPENO = Convert.ToInt32(ddlPostType.SelectedValue);
            //objIOtran.CENTRALIONO = txtDispInwardNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtDispInwardNo.Text.Trim().ToString();
            
            objIOtran.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objIOtran.CREATOR = (Session["userno"].ToString());
            objIOtran.CREATED_DATE = System.DateTime.Now;

            objIOtran.ADDLINE = txtAddLine.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtAddLine.Text.Trim().ToString();
            objIOtran.STATENO = Convert.ToInt32(ddlState.SelectedValue);
            objIOtran.COUNTRYNO = Convert.ToInt32(ddlCountry.SelectedValue);
            if (rdbUL.SelectedValue == "4")
            {
                objIOtran.TODEPT = Convert.ToInt32(ddlHODdept.SelectedValue);
            }
            else
            {
                objIOtran.TODEPT = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            objIOtran.DESIGNO = Convert.ToInt32(ddlDesig.SelectedValue);
            objIOtran.IN_TO_USER = Convert.ToInt32(ddlUser.SelectedValue);
            objIOtran.RFID = txtRFID.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtRFID.Text.Trim().ToString();
            objIOtran.PEON = txtPeon.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtPeon.Text.Trim().ToString();
            //if (rdbCheque.SelectedValue == "")
            //{
            //    objIOtran.CHEUQE_ID = 0;
            //}
            //else
            //{
            //    objIOtran.CHEUQE_ID = Convert.ToInt32(rdbCheque.SelectedValue);
            //}
            //objIOtran.CHQDDNO = txtDDNo.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtDDNo.Text.Trim().ToString();
            //objIOtran.CHEQDT = txtDDdate.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDDdate.Text);
            //objIOtran.CHEQAMT = txtDDAmt.Text.Trim().ToString().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtDDAmt.Text);
            //objIOtran.BANKNAME = txtbank.Text.Trim().ToString().Equals(string.Empty) ? string.Empty : txtbank.Text.Trim().ToString();

            DataTable dt;
            dt = (DataTable)Session["RecTbl"];
            objIOtran.CHEQUE_DETAILS_TABLE = dt;

            objIOtran.USERTYPE = Convert.ToInt32(ddlUType.SelectedValue);
            if(rdbUL.SelectedValue =="0")
            {
                objIOtran.USERFLAG = 'P';   // Principal
            }
            else if(rdbUL.SelectedValue =="1")
            {
                objIOtran.USERFLAG = 'S';   // Secretary
            }
            else if(rdbUL.SelectedValue =="2")
            {
                objIOtran.USERFLAG = 'C';   // Chairman
            }
            else if (rdbUL.SelectedValue == "4")
            {
                objIOtran.USERFLAG = 'H';  // HOD
            }
            else if (rdbUL.SelectedValue == "5")
            {
                objIOtran.USERFLAG = 'V';  // SVCE
            }
            else
            {
                objIOtran.USERFLAG = 'O';  // staff
            }
            

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    //=============Shaikh Juned 04-07-22--Start

                   // DataSet ds = objMaster.AllMasters(objMaster._tablename, calnames[1] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' AND " + calnames[2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "'", objMaster.captions[0, 2]);
                    DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN", "OUTREFERENCENO", "SUBJECT", "OUTREFERENCENO= '" + Convert.ToString(txtReferenceNo.Text) + "' ", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayMessage(this, "Reference Number Is already exist", this.Page);
                        return;
                    }

                    //==========end==

                    CustomStatus cs = (CustomStatus)objIOtranc.AddInwardEntry(objIOtran);
                    if (Convert.ToInt32(cs) != -99)
                    {
                        Clear_Controls();
                        ViewState["action"] = null;
                        BindListViewInward();
                        divAdd.Visible = false;
                        divUser.Visible = false;
                        //divDD.Visible = false;
                        pnlList.Visible = true;
                        divAddNew.Visible = true;
                        divBtnPanel.Visible = false;
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["IOTRANNO"] != null)
                    {
                        objIOtran.IOTRANNO = Convert.ToInt32(ViewState["IOTRANNO"].ToString());
                        ////=============Shaikh Juned 04-07-22--Start

                        //// DataSet ds = objMaster.AllMasters(objMaster._tablename, calnames[1] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' AND " + calnames[2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "'", objMaster.captions[0, 2]);
                        //DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN", "OUTREFERENCENO", "SUBJECT", "OUTREFERENCENO= '" + Convert.ToString(txtReferenceNo.Text) + "' ", "");
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    objCommon.DisplayMessage(this, "Reference Number Is already exist", this.Page);
                        //    return;
                        //}

                        ////==========end==
                        CustomStatus cs = (CustomStatus)objIOtranc.UpdateInwardEntry(objIOtran);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear_Controls();
                            BindListViewInward();
                            ViewState["action"] = null;
                            divAdd.Visible = false;
                            divUser.Visible = false;
                            //divDD.Visible = false;
                            pnlList.Visible = true;
                            divAddNew.Visible = true;
                            divBtnPanel.Visible = false;
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                        }
                    }
                }
            }
            //Response.Redirect("Movement.aspx");               
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Controls();
            // Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());

        divAdd.Visible = false;
        divUser.Visible = false;
        //divDD.Visible = false;
        pnlList.Visible = true;
        divAddNew.Visible = true;
        divBtnPanel.Visible = false;
        rdbUL.SelectedIndex = 0;
        divUserDetails.Visible = false;
        ddlUType.SelectedIndex = 0;
        divHOD.Visible = false;
        ddlHODdept.SelectedIndex = 0;

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //trmsg.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int IOTRANNO = int.Parse(btnEdit.CommandArgument);
            ViewState["IOTRANNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(IOTRANNO);
            divAdd.Visible = true;
            divUser.Visible = true;
            //divDD.Visible = true;
            pnlList.Visible = false;
            divAddNew.Visible = false;
            divBtnPanel.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvInward_Bound(object sender, ListViewItemEventArgs e)
    {
        ImageButton img = ((ListViewDataItem)e.Item).FindControl("btnEdit") as ImageButton;

        Label hf = e.Item.FindControl("lblStatus") as Label;
        if (hf.Text == "RECEIVED")
        {
            img.Enabled = false;
            img.ToolTip = "Non Editable";
        }
        else
        {
            img.Enabled = true;

        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //trmsg.Visible = true;
            Clear_Controls();
            DataSet ds = objIOtranc.GetRefernceNo("I");
            txtDispRefno.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();
            divAdd.Visible = true;
            divUser.Visible = true;
            //divDD.Visible = true;
            pnlList.Visible = false;
            divAddNew.Visible = false;
            divBtnPanel.Visible = true;
            ViewState["action"] = "add";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnAdd_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear_Controls()
    {
        txtAddress.Text = string.Empty;
        txtbank.Text = string.Empty;      
        ddlPostType.SelectedIndex = 0;      
        txtDDAmt.Text = string.Empty;
        txtReceivedDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtReferenceNo.Text = string.Empty;
        txtFrom.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        txtPIN.Text = string.Empty;
        txtSubject.Text = string.Empty;        
        txtDDdate.Text = string.Empty;
        txtDDNo.Text = string.Empty;
        txtAddLine.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlOutRefNo.SelectedIndex = 0;                               //  GAYATRI RODE 13-01-2021
        ddlDesig.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null", "U.UA_FULLNAME");
        ddlUser.SelectedIndex = 0;
        txtRFID.Text = string.Empty;
        txtPeon.Text = string.Empty;
        ViewState["IOTRANNO"] = null;
        ViewState["action"] = "add";
        Session["RecTbl"] = null;
        ViewState["SRNO"] = null;
        lvChequeDetails.DataSource = null;
        lvChequeDetails.DataBind();
        lblCheque.Text = "Chq.No";

        rdbUL.SelectedIndex = 0;
        divUserDetails.Visible = false;
        ddlUType.SelectedIndex = 0;
        divHOD.Visible = false;
        ddlHODdept.SelectedIndex = 0;
    }

  

    private void ShowDetails(int IOTRANNO)
    {
        try
        {
            DataSet ds = objIOtranc.GetInwardByInwardNo(IOTRANNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPostType.SelectedValue = ds.Tables[0].Rows[0]["POSTTYPENO"].ToString();                //ddlLetterType.SelectedValue = ds.Tables[0].Rows[0]["LETTERTYPENO"].ToString();
                txtReceivedDT.Text = ds.Tables[0].Rows[0]["CENTRALRECSENTDT"].ToString();
                txtReferenceNo.Text = ds.Tables[0].Rows[0]["OUTREFERENCENO"].ToString();
                txtFrom.Text = ds.Tables[0].Rows[0]["IOFROM"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtPIN.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                txtToUser.Text = ds.Tables[0].Rows[0]["TOUSER"].ToString();                //ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["TOUSER"].ToString();
                txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                txtDispRefno.Text = ds.Tables[0].Rows[0]["CENTRALREFERENCENO"].ToString();                //txtDispInwardNo.Text = ds.Tables[0].Rows[0]["CENTRALIONO"].ToString();
                txtDDNo.Text = ds.Tables[0].Rows[0]["CHQDDNO"].ToString();
                txtDDAmt.Text = ds.Tables[0].Rows[0]["CHEQAMT"].ToString();
                txtDDdate.Text = ds.Tables[0].Rows[0]["CHEQDT"].ToString();
                txtbank.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtAddLine.Text = ds.Tables[0].Rows[0]["ADDRESS_LINE"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATE"].ToString();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                txtPeon.Text = ds.Tables[0].Rows[0]["PEON"].ToString();
 
                if (ds.Tables[0].Rows[0]["USER_TYPE_FLAG"].ToString() == "H")
                {
                    ddlHODdept.SelectedValue = ds.Tables[0].Rows[0]["TODEPT"].ToString();
                }
                else
                {
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["TODEPT"].ToString();
                }
                
                objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null and U.UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "U.UA_FULLNAME");
                ddlUser.SelectedValue = ds.Tables[0].Rows[0]["IN_TO_USER"].ToString();
                ddlDesig.SelectedValue = ds.Tables[0].Rows[0]["DESIGNO"].ToString();
                txtRFID.Text = ds.Tables[0].Rows[0]["RFID_NUMBER"].ToString();

                ddlUType.SelectedValue = ds.Tables[0].Rows[0]["USER_TYPE"].ToString();                             

                if (ds.Tables[0].Rows[0]["USER_TYPE_FLAG"].ToString() == "P")
                {
                    rdbUL.SelectedValue = "0";
                    divUserDetails.Visible = false;
                    divHOD.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["USER_TYPE_FLAG"].ToString() == "S")
                {
                    rdbUL.SelectedValue = "1";
                    divUserDetails.Visible = false;
                    divHOD.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["USER_TYPE_FLAG"].ToString() == "C")
                {
                    rdbUL.SelectedValue = "2";
                    divUserDetails.Visible = false;
                    divHOD.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["USER_TYPE_FLAG"].ToString() == "H")
                {
                    rdbUL.SelectedValue = "4";
                    divUserDetails.Visible = false;
                    divHOD.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["USER_TYPE_FLAG"].ToString() == "V")
                {
                    rdbUL.SelectedValue = "5";
                    divUserDetails.Visible = false;
                    divHOD.Visible = false;
                }
                else
                {
                    rdbUL.SelectedValue = "3";
                    divUserDetails.Visible = true;
                    divHOD.Visible = false;
                }



                DataSet dsCheq = objIOtranc.GetChequeDetails(IOTRANNO);
                if (dsCheq.Tables[0].Rows.Count > 0)
                {
                    lvChequeDetails.DataSource = dsCheq;
                    lvChequeDetails.DataBind();
                    lvChequeDetails.Visible = true;

                    Session["RecTbl"] = dsCheq.Tables[0];
                }
                else
                {
                    lvChequeDetails.DataSource = null;
                    lvChequeDetails.DataBind();
                    lvChequeDetails.Visible = false;
                    Session["RecTbl"] = null;
                }

                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void dpLett_PreRender(object sender, EventArgs e)
    {
        BindListViewInward();
    }
    protected void ddlDepartment_SelectedIndexChange(object sender, EventArgs e)
    {
        try
        {
            if (ddlUType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null and U.UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "AND U.UA_TYPE=" + Convert.ToInt32(ddlUType.SelectedValue), "U.UA_FULLNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null and U.UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "U.UA_FULLNAME");
            }
            ddlUser.Focus();
            txtRFID.Text = string.Empty;
            ddlDesig.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if
                (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.BindUser -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlUser_SelectedIndexChange(object sender, EventArgs e)
    {
        try
        {
            //string desigNo = objCommon.LookUp("PAYROLL_EMPMAS","SUBDESIGNO","IDNO=" + ddlUser.SelectedValue);
            //if (desigNo != "")
            //{
            //    ddlDesig.SelectedValue = desigNo;
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updActivity, "Please select designation.", this.Page);
            //}

            DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDESIGNO", "PFILENO", "IDNO=" + ddlUser.SelectedValue, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDesig.SelectedValue = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();
                //txtRFID.Text = ds.Tables[0].Rows[0]["RFIDNO"].ToString();
                txtRFID.Text = ds.Tables[0].Rows[0]["PFILENO"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please select designation.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if
                (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.BindUser -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            //trmsg.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int IOTRANNO = int.Parse(btnEdit.CommandArgument);
            ViewState["IOTRANNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(IOTRANNO);
            divAdd.Visible = true;
            divUser.Visible = true;
            //divDD.Visible = true;
            pnlList.Visible = false;
            divAddNew.Visible = false;
            divBtnPanel.Visible = true;
            txtReferenceNo.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    #region Cheque Details

    private DataTable CreateTable()
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

            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                int maxVal = 0;
                DataTable dt = (DataTable)Session["RecTbl"];
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
                Session["RecTbl"] = dt;
                lvChequeDetails.DataSource = dt;
                lvChequeDetails.DataBind();
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                DataTable dt = this.CreateTable();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["CHEQUE_TYPE"] = Convert.ToInt32(rdbCheque.SelectedValue);
                dr["CHEQUE_NO"] = txtDDNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDNo.Text.Trim());
                dr["CHEQUE_AMOUNT"] = txtDDAmt.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDAmt.Text.Trim());
                dr["CHEQUE_DATE"] = txtDDdate.Text.Trim() == null ? string.Empty : Convert.ToString(txtDDdate.Text.Trim());
                dr["CHEQUE_BANK"] = txtbank.Text.Trim() == null ? string.Empty : Convert.ToString(txtbank.Text.Trim());

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvChequeDetails.DataSource = dt;
                lvChequeDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ClearRec()
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

    protected void btnEditRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = ((DataTable)Session["RecTbl"]);
                ViewState["EDIT_SRNO"] = btnEditRec.CommandArgument;
                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);
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
                Session["RecTbl"] = dt;
                lvChequeDetails.DataSource = dt;
                lvChequeDetails.DataBind();
                ViewState["actionContent"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
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
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

  
   
    #endregion


    // To get the details on the selection of radiobuttons.
    protected void rdbUL_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
          if (rdbUL.SelectedValue == "3")
          {
              divUserDetails.Visible = true;
              ddlUType.SelectedIndex = 0;
              ddlDepartment.SelectedIndex = 0;
              ddlUser.SelectedIndex = 0;
              txtRFID.Text = string.Empty;
              ddlDesig.SelectedIndex = 0;
              divHOD.Visible = false;
          }
          else if (rdbUL.SelectedValue == "4")
          {
              divHOD.Visible = true;
              divUserDetails.Visible = false;
          }
          else
          {
              divUserDetails.Visible = false;
              divHOD.Visible = false;
          }
      }
      catch (Exception ex)
      {
          if (Convert.ToBoolean(Session["error"]) == true)
              objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.rdbUL_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
          else
              objUCommon.ShowError(Page, "Server UnAvailable");
      }

    }

    // To get the list of user on the selection of user type.
    protected void ddlUType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null and U.UA_TYPE=" + Convert.ToInt32(ddlUType.SelectedValue) + " AND U.UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "U.UA_FULLNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null and U.UA_TYPE=" + Convert.ToInt32(ddlUType.SelectedValue), "U.UA_FULLNAME");
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.ddlUType_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}