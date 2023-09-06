//=============================================================================
//MODIFIED BY  : MRUNAL SINGH
//MODIFIED DATE : 25-12-2014
//DESCRIPTION   : SAVE AND UPDATE IN TIME & OUT TIME IN DATABASE
//=============================================================================
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

public partial class VEHICLE_MAINTENANCE_Transaction_Servicing : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM objVM = new VM();
    VMController objVMC = new VMController();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // txtBllAmt.Text = "0.00";
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
                    ViewState["action"] = "add";
                    FillDropDown();
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;
                    BindList();
                    pnlAddHis.Visible = true;
                    pnlList.Visible = true;
                    pnlPersInfo.Visible = false;
                    pnlTo.Visible = false;
                    trW.Visible = true;
                    pnlButton.Visible = false;
                }
               
                hdnDate.Value = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
            if (lvTo.Items.Count > 0)
            {

            }
            else
            {
                lvTo.DataSource = null;
                lvTo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
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
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddl, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "VIDNO");
        objCommon.FillDropDownList(ddlWorkshp, "VEHICLE_WORKSHOP", "WSNO", "WORKSHOP_NAME", "WSNO>0", "WORKSHOP_NAME");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int no = int.Parse(btnDelete.CommandArgument);
        objVM.SIDNO = Convert.ToInt32(no);
        CustomStatus CS = (CustomStatus)objVMC.DeleteServiceMasterBySIDNO(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Sucessfully", this.Page);
            BindList();
        }
    }
    private void clear()
    {
        ddl.SelectedIndex = 0;
        lvTo.DataSource = null;
        lvTo.DataBind();
        ddlWorkshp.SelectedIndex = 0;
        Session["RecTbl"] = null;
        txtBillno.Text = string.Empty;
        txtBllAmt.Text = string.Empty;
        txtBllDt.Text = string.Empty;
        txtBnkName.Text = string.Empty;
        txtChqDt.Text = string.Empty;
        txtChqNo.Text = string.Empty;
        txtINDt.Text = string.Empty;
        txtINTime.Text = string.Empty;
        txtItmAmt.Text = string.Empty;
        txtNxtVstDt.Text = string.Empty;
        txtOUTDt.Text = string.Empty;
        txtOuttym.Text = string.Empty;
        txtPaidDt.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtItem.Text = string.Empty;
        txtItmAmt.Text = string.Empty;
        txtWrkOrdrNo.Text = string.Empty;
        trW.Visible = false;
        //pnlTo.Visible = false;
        ViewState["IDNO"] = null;
        ViewState["action"] = "add";

        txtTransactioNo.Text = string.Empty;
        txtTranDate.Text = string.Empty;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {       
        ImageButton imgBtn = sender as ImageButton;
        int no = int.Parse(imgBtn.CommandArgument);
        ViewState["IDNO"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(no);
        pnlAddHis.Visible = false;
        pnlButton.Visible = true;
        pnlItemList.Visible = true;

    }
    private void ShowDetails(int NO)
    {
        try
        {
            objVM.SIDNO = Convert.ToInt32(NO);
            DataSet ds = objVMC.GetServiceMasterByIDNO(objVM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlAddHis.Visible = false;
                pnlList.Visible = false;
                pnlPersInfo.Visible = true;
                pnlTo.Visible = true;
                pnlItemList.Visible = true;
                rdbtnMode.SelectedValue = ds.Tables[0].Rows[0]["PAIDBY"].ToString();
                if (ds.Tables[0].Rows[0]["PAIDBY"].ToString().Trim().Equals("C"))
                {
                    trChewue.Visible = false;
                    trbank.Visible = false;
                    divOTransfer.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["PAIDBY"].ToString().Trim().Equals("O"))
                {
                    divOTransfer.Visible = true;
                    trChewue.Visible = false;
                    trbank.Visible = false;
                    txtTransactioNo.Text = ds.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                    txtTranDate.Text = ds.Tables[0].Rows[0]["TRANSACTION_DATE"].ToString();
                }
                else
                {
                    trbank.Visible = true;
                    trChewue.Visible = true;
                    txtBnkName.Text = ds.Tables[0].Rows[0]["CHQBANK"].ToString();
                    txtChqDt.Text = ds.Tables[0].Rows[0]["CHQDT"].ToString();
                    txtChqNo.Text = ds.Tables[0].Rows[0]["CHQNO"].ToString();
                }

                txtBillno.Text = ds.Tables[0].Rows[0]["BILLNO"].ToString();
                txtBllAmt.Text = ds.Tables[0].Rows[0]["BILLAMT"].ToString();
                txtBllDt.Text = ds.Tables[0].Rows[0]["BILLDT"].ToString();
                txtINDt.Text = ds.Tables[0].Rows[0]["WSINDT"].ToString();
                if (ds.Tables[0].Rows[0]["WSINTIME"].ToString() != "")
                {
                    // txtINTime.Text = ds.Tables[0].Rows[0]["WSINTIME"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["WSINTIME"]).ToString();//.ToString("HH:mm:ss");

                    txtINTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WSINTIME"]).ToString("hh:mm tt");

                }
                txtNxtVstDt.Text = ds.Tables[0].Rows[0]["NEXTDT"].ToString();
                txtOUTDt.Text = ds.Tables[0].Rows[0]["WSOUTDT"].ToString();

                if (ds.Tables[0].Rows[0]["WSOUTIME"].ToString() != "")
                {
                    // txtOuttym.Text = ds.Tables[0].Rows[0]["WSOUTIME"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["WSOUTIME"]).ToString();//.ToString("HH:mm:ss");

                    txtOuttym.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WSOUTIME"]).ToString("hh:mm tt");
                }
                txtPaidDt.Text = ds.Tables[0].Rows[0]["PAIDT"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                txtWrkOrdrNo.Text = ds.Tables[0].Rows[0]["ORDNO"].ToString();
                ddlWorkshp.SelectedValue = ds.Tables[0].Rows[0]["WSNO"].ToString();
                ddl.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                int ddlno = Convert.ToInt32(ddlWorkshp.SelectedValue);
                BindListWrkshp(ddlno);
                DataSet dss = objVMC.GetServiceMasterItem(objVM);
                if (Convert.ToInt32(dss.Tables[0].Rows.Count) > 0)
                {
                    lvTo.DataSource = dss;
                    lvTo.DataBind();
                    Session["RecTbl"] = dss.Tables[0];
                    ViewState["SRNO"] = Convert.ToInt32(dss.Tables[0].Rows.Count);
                }
                else
                {
                    lvTo.DataSource = null;
                    lvTo.DataBind();

                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.ShowDetails -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtINDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtINDt.Text), Convert.ToDateTime(txtOUTDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('OUT Date Should Be Greater Than IN Date.');", true);
                    txtINDt.Focus();
                    return;
                }
            }

            if (!txtNxtVstDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtOUTDt.Text), Convert.ToDateTime(txtNxtVstDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Next Visit Date should be greater than OUT DATE.');", true);
                    txtINDt.Focus();
                    return;
                }
            }



            string Item = string.Empty;
            string Qty = string.Empty;
            string Amt = string.Empty;

            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = (DataTable)Session["RecTbl"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (Item.Trim().Equals(string.Empty))

                        Item = dr["ITEM"].ToString();
                    else
                        Item += "," + dr["ITEM"].ToString();


                    if (Qty.Trim().Equals(string.Empty))

                        Qty = dr["QTY"].ToString();
                    else
                        Qty += "," + dr["QTY"].ToString();

                    if (Amt.Trim().Equals(string.Empty))
                        Amt = dr["AMT"].ToString();
                    else
                        Amt += "," + dr["AMT"].ToString();
                }
            }

            objVM.ITEM = Convert.ToString(Item);
            objVM.VIDNO = Convert.ToInt32(ddl.SelectedValue);
            objVM.WSNO = Convert.ToInt32(ddlWorkshp.SelectedValue);
            objVM.SBILLNO = txtBillno.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtBillno.Text.Trim());
            if (txtBllAmt.Text == string.Empty)
            {
                objVM.SBILLAMT = 0.00m;
            }
            else
            {
                objVM.SBILLAMT = Convert.ToDecimal(txtBllAmt.Text);
            }

            if (txtPaidDt.Text == string.Empty)
            {
                objVM.SPAIDDT = DateTime.MinValue;
            }
            else
            {
                objVM.SPAIDDT = Convert.ToDateTime(txtPaidDt.Text);
            }

            if (rdbtnMode.SelectedValue == "B")
            {
                objVM.SCHQNO = txtChqNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtChqNo.Text.Trim());
                objVM.SCHQBANK = txtBnkName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtBnkName.Text.Trim());
                objVM.SCHQDT = Convert.ToDateTime(txtChqDt.Text);
            }
            else if (rdbtnMode.SelectedValue == "O")
            {
                objVM.TRANSCATIONNO = txtTransactioNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtTransactioNo.Text.Trim());
                objVM.TRANSFERDT = Convert.ToDateTime(txtTranDate.Text);
            }
            else
            {               
                objVM.SCHQNO = "0";
                objVM.SCHQBANK = "";                
            }
            objVM.WSINDT = Convert.ToDateTime(txtINDt.Text);
            objVM.WSOUTDT = Convert.ToDateTime(txtOUTDt.Text);

            objVM.WSINTIME = Convert.ToDateTime(Convert.ToDateTime(txtINTime.Text).ToString("HH:mm tt"));
            objVM.WSOUTTIME = Convert.ToDateTime(Convert.ToDateTime(txtOuttym.Text).ToString("HH:mm tt"));

            objVM.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text.Trim());
            objVM.SPAIDBY = Convert.ToChar(rdbtnMode.SelectedValue);
            if (txtBllDt.Text == string.Empty)
            {
                objVM.BILLDT = DateTime.MinValue;
            }
            else
            {
                objVM.BILLDT = Convert.ToDateTime(txtBllDt.Text);
            }

            objVM.NEXTDT = Convert.ToDateTime(txtNxtVstDt.Text);
            objVM.ORDNO = txtWrkOrdrNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtWrkOrdrNo.Text.Trim());

            


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    //--======start===Shaikh Juned 26-08-2022

                    DataSet ds = objCommon.FillDropDown("VEHICLE_SERVICEMAS", "BILLNO", "ORDNO", "BILLNO='" + Convert.ToString(txtBillno.Text) + "'", ""); //or ORDNO='" + Convert.ToString(txtWrkOrdrNo.Text) + "'
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string BILLNO = dr["BILLNO"].ToString();
                            if (BILLNO == txtBillno.Text)
                            {
                                objCommon.DisplayMessage(this.Page, "Bill No Is Already Exist.", this.Page);
                                return;
                            }
                        }
                    }


                    DataSet ds1 = objCommon.FillDropDown("VEHICLE_SERVICEMAS", "BILLNO", "ORDNO", "ORDNO='" + Convert.ToString(txtWrkOrdrNo.Text) + "' ", ""); //or ORDNO='" + Convert.ToString(txtWrkOrdrNo.Text) + "'
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds1.Tables[0].Rows)
                        {
                            string ORDNO = dr["ORDNO"].ToString();
                            if (ORDNO == txtWrkOrdrNo.Text)
                            {
                                objCommon.DisplayMessage(this.Page, "Order No Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }
                    //---========end=====

                    CustomStatus cs = (CustomStatus)objVMC.AddServiceMaster(objVM, Amt, Qty);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                        Showmessage("Record Save Successfully.");
                        clear();
                        ViewState["action"] = "add";
                        BindList();
                        pnlPersInfo.Visible = false;
                        pnlAddHis.Visible = true;
                        pnlButton.Visible = false;
                        pnlList.Visible = true;
                    }
                }
                else
                {
                    if (ViewState["IDNO"] != null)
                    {
                        objVM.SIDNO = Convert.ToInt32(ViewState["IDNO"].ToString());
                        //--======start===Shaikh Juned 26-08-2022

                        DataSet ds = objCommon.FillDropDown("VEHICLE_SERVICEMAS", "BILLNO", "ORDNO", "BILLNO='" + Convert.ToString(txtBillno.Text) + "' and SIDNO != '" + Convert.ToInt32(objVM.SIDNO) + "'", ""); //or ORDNO='" + Convert.ToString(txtWrkOrdrNo.Text) + "'
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string BILLNO = dr["BILLNO"].ToString();
                                if (BILLNO == txtBillno.Text)
                                {
                                    objCommon.DisplayMessage(this.Page, "Bill No Is Already Exist.", this.Page);
                                    return;
                                }
                            }
                        }


                        DataSet ds1 = objCommon.FillDropDown("VEHICLE_SERVICEMAS", "BILLNO", "ORDNO", "ORDNO='" + Convert.ToString(txtWrkOrdrNo.Text) + "' and SIDNO != '" + Convert.ToInt32(objVM.SIDNO) + "'", ""); //or ORDNO='" + Convert.ToString(txtWrkOrdrNo.Text) + "'
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                string ORDNO = dr["ORDNO"].ToString();
                                if (ORDNO == txtWrkOrdrNo.Text)
                                {
                                    objCommon.DisplayMessage(this.Page, "Order No Is Already Exist.", this.Page);
                                    return;
                                }

                            }
                        }
                        //---========end=====


                        CustomStatus cs = (CustomStatus)objVMC.UpdServiceMaster(objVM, Amt, Qty);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                          
                            clear();
                           // objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                            Showmessage("Record Updated Successfully.");
                            ViewState["action"] = "add";
                            BindList();
                            pnlPersInfo.Visible = false;
                            pnlAddHis.Visible = true;
                            pnlButton.Visible = false;
                            pnlList.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindList()
    {
        try
        {
            DataSet ds = objVMC.GetServiceMasterAll();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDesg.DataSource = ds;
                lvDesg.DataBind();
            }
            else
            {
                lvDesg.DataSource = null;
                lvDesg.DataBind();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.BindList -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        clear();       
        trW.Visible = false;
        pnlAddHis.Visible = false;
        pnlList.Visible = false;
        pnlPersInfo.Visible = true;
        pnlTo.Visible = true;
        lvTo.DataSource = null;
        lvTo.DataBind();
        pnlButton.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAddHis.Visible = true;
        pnlList.Visible = true;
        pnlPersInfo.Visible = false;
        lvTo.DataSource = null;
        lvTo.DataBind();
        pnlTo.Visible = false;
        pnlButton.Visible = false;

    }
    private DataTable CreateTabel()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("ITEM", typeof(string)));
        dtRe.Columns.Add(new DataColumn("QTY", typeof(string)));
        dtRe.Columns.Add(new DataColumn("AMT", typeof(decimal)));
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
                dr["ITEM"] = txtItem.Text.Trim() == null ? string.Empty : Convert.ToString(txtItem.Text.Trim());
                dr["QTY"] = txtQty.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtQty.Text.Trim());
                dr["AMT"] = Convert.ToDecimal(txtItmAmt.Text);


                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                double Amount = 0.00;

                foreach (DataRow ddr in dt.Rows)
                {
                    Amount = Amount + Convert.ToDouble(ddr["AMT"]);
                }

                txtBllAmt.Text = Amount.ToString("0.00");

                ClearRec();
            }
            else
            {

                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["ITEM"] = txtItem.Text.Trim() == null ? string.Empty : Convert.ToString(txtItem.Text.Trim());
                dr["QTY"] = Convert.ToInt32(txtQty.Text);
                dr["AMT"] = Convert.ToDecimal(txtItmAmt.Text);

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);

                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
                double Amount = 0.00;
                foreach (DataRow ddr in dt.Rows)
                {
                    Amount = Amount + Convert.ToDouble(ddr["AMT"]);
                }
                txtBllAmt.Text = Amount.ToString("0.00");
                ClearRec();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.btnAddTo_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearRec()
    {
        txtItem.Text = string.Empty;
        txtItmAmt.Text = string.Empty;
        txtQty.Text = string.Empty;
    }
    protected void btnDeletel_Click(object sender, EventArgs e)
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

                double Amount = 0.00;
                foreach (DataRow ddr in dt.Rows)
                {
                    Amount = Amount + Convert.ToDouble(ddr["AMT"]);
                }
                txtBllAmt.Text = Amount.ToString("0.00");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
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

                txtItem.Text = dr["ITEM"].ToString();
                txtQty.Text = dr["QTY"].ToString();
                txtItmAmt.Text = dr["AMT"].ToString();
                dt.Rows.Remove(dr);
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
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
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    protected void ddlWorkshp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int ddlno = Convert.ToInt32(ddlWorkshp.SelectedValue);
            BindListWrkshp(ddlno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.ddlWorkshp_SelectedIndexChanged -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListWrkshp(int ddlno)
    {
        try
        {

            string add = objCommon.LookUp("VEHICLE_WORKSHOP", "ADD1", "WSNO=" + ddlno);
            trW.Visible = true;
            lblWrkshpDtl.Text = add;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.BindListWrkshp -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void rdbtnMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbtnMode.SelectedValue.Equals("B"))
        {
            trbank.Visible = true;
            trChewue.Visible = true;
            divOTransfer.Visible = false;
        }
        else if (rdbtnMode.SelectedValue.Equals("O"))
        {
            divOTransfer.Visible = true;
           // txtTranDate.Visible = true;
           // txtTransactioNo.Visible = true;
           // TransferDate.Visible = true;
           // TransactionNo.Visible = true;


            trbank.Visible = false;
            trChewue.Visible = false;

        }
        else 
        {
            trbank.Visible = false;
            trChewue.Visible = false;
            divOTransfer.Visible = false;

        }
    }
    protected void txtOUTDt_TextChanged(object sender, EventArgs e)
    {

    }
}
