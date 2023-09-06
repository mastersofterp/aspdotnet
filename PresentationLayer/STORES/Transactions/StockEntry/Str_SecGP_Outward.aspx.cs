
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                                       
// CREATION DATE : 05-08-2021                                                 
// CREATED BY    : GOPAL ANTHATI                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class STORES_Transactions_StockEntry_Str_SecGP_Outward : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StrSecurityPassEnt objSecEnt = new StrSecurityPassEnt();
    StrSecurityPassCon objSecCon = new StrSecurityPassCon();

    DataTable dtItemTable = null;
    DataRow datarow = null;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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

                }
            }
            ViewState["Action"] = "Add";
            FillDropDownList();

            BindListView();

        }
        //
        divMsg.InnerText = string.Empty;
    }

    private void FillDropDownList()
    {
        if (rdlList.SelectedValue == "1")
        {
            if (ViewState["Action"].ToString() == "Edit")
                objCommon.FillDropDownList(ddlGatePass, "STORE_ITEM_REPAIR_MAIN", "IR_ID", "GP_NUMBER", "", "GP_NUMBER DESC");
            else
                objCommon.FillDropDownList(ddlGatePass, "STORE_ITEM_REPAIR_MAIN", "IR_ID", "GP_NUMBER", "IR_ID NOT IN (SELECT IR_ID FROM STORE_SEC_PASS_OUTWARD)", "GP_NUMBER DESC");
        }
        else
        {
            objCommon.FillDropDownList(ddlGatePass, "STORE_ITEM_REPAIR_MAIN", "IR_ID", "GP_NUMBER", "", "GP_NUMBER DESC");
        }
    }
    private void BindListView()
    {

        DataSet ds = objSecCon.GetSecGPOutwardItemList();   //modified by shabina 24-09-2021


        // DataSet ds = objCommon.FillDropDown("STORE_SEC_PASS_OUTWARD A INNER JOIN STORE_ITEM_REPAIR_TRAN C ON (A.IR_ID=C.IR_ID) INNER JOIN STORE_ITEM_REPAIR_MAIN B ON (A.IR_ID=B.IR_ID)", "DISTINCT SP_OW_ID,REG_SLIP_NO,OUT_DATE", "VEHICLE_NO,B.GP_NUMBER", "C.IS_RET_SEC IS NULL", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvOutwardGP.DataSource = ds.Tables[0];
            lvOutwardGP.DataBind();
            divListOutwardGP.Visible = true;
            pnlAddNew.Visible = true;
            divEmptyLabel.Visible = false;
        }
        else
        {
            lvOutwardGP.DataSource = null;
            lvOutwardGP.DataBind();
            divListOutwardGP.Visible = false;
            // pnlAddNew.Visible = false;
            divEmptyLabel.Visible = true;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {

        ClearAll();
        divOutwardGP.Visible = true;
        divAddNew.Visible = false;
        divListOutwardGP.Visible = false;
        divButtons.Visible = true;
        pnlAddNew.Visible = false;
        divTranSlipNo.Visible = false;

        divReceiveFields.Visible = false;
        btnAddNew2.Visible = false;
        btnCancel.Visible = true;

        //ddlGatePass.Enabled = false;
        txtVehicleNo.Enabled = true;
        txtVehicleNo.Text = "";
        txtOutDate.Enabled = true;
        ceOutDate.Enabled = true;
        txtOutDate.Text = "";
        txtOutTime.Enabled = true;
        txtOutTime.Text = "";
        lvDsrItem.Enabled = false;


        FillDropDownList();

        ddlGatePass.Enabled = true;
        ddlGatePass.Enabled = true;
        divlvDsr.Visible = false;



    }
    protected void ddlGatePass_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds = objSecCon.GetItemListByGPNum(Convert.ToInt32(ddlGatePass.SelectedValue));
        //DataSet ds = objCommon.FillDropDown("STORE_ITEM_REPAIR_TRAN A INNER JOIN STORE_ITEM_REPAIR_MAIN B ON (A.IR_ID=B.IR_ID) INNER JOIN PAYROLL_EMPMAS C ON (B.TO_EMPLOYEE=C.IDNO)", "DSR_NUMBER,COMPLAINT_NATURE", "TITLE+isnull(fname,'')+' '+isnull(mname,'')+' '+isnull(fname,'') as NAME ", "A.IR_ID=" + ddlGatePass.SelectedValue, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDsrItem.DataSource = ds.Tables[0];
            lvDsrItem.DataBind();
            divlvDsr.Visible = true;
            txtCarriedEmp.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
            divEmp.Visible = true;
        }
        else
        {
            lvDsrItem.DataSource = ds.Tables[0];
            lvDsrItem.DataBind();
            divlvDsr.Visible = false;
            txtCarriedEmp.Text = string.Empty;
            divEmp.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string IrTranid = string.Empty;
        foreach (ListViewItem lv in lvDsrItem.Items)
        {
            CheckBox chkDsrselect = lv.FindControl("chkDsrselect") as CheckBox;
            if (chkDsrselect.Checked)
            {
                string irtranid = chkDsrselect.ToolTip;
                IrTranid += irtranid + ',';
            }
        }

        if (IrTranid == string.Empty)
        {
            MessageBox("Please Select Atleast One Item.");
            return;
        }

        if (txtReceiveDate.Text != "")
        {
            if (Convert.ToDateTime(txtReceiveDate.Text) < Convert.ToDateTime(txtOutDate.Text))
            {
                MessageBox("Received Date Should Be Greater Than Or Equal To Out Date.");
                return;
            }
            objSecEnt.RECEIVED_DATE = Convert.ToDateTime(txtReceiveDate.Text);
        }
        if (txtReceiveTime.Text != "")
        {
            DateTime t1 = DateTime.Parse(txtOutTime.Text);
            DateTime t2 = DateTime.Parse(txtReceiveTime.Text);

            if (t1.TimeOfDay > t2.TimeOfDay)
            {
                MessageBox("Received Time Should Be Greater Than Out Time");
                return;
            }
            else
            {
                objSecEnt.RECEIVED_TIME = Convert.ToDateTime(txtReceiveTime.Text).ToString("hh:mm tt");
            }
            
        }


        if (IrTranid != string.Empty)
        {
            IrTranid = IrTranid.Substring(0, IrTranid.Length - 1);
        }
        objSecEnt.IRTRANID = IrTranid;

        objSecEnt.REG_SLIP_NO = txtRegSlipNo.Text;
        objSecEnt.GP_NUMBER = Convert.ToInt32(ddlGatePass.SelectedValue);
        objSecEnt.OUT_DATE = Convert.ToDateTime(txtOutDate.Text);
        objSecEnt.OUT_TIME = Convert.ToDateTime(txtOutTime.Text).ToString("hh:mm tt");



        objSecEnt.VEHICLE_NO = txtVehicleNo.Text;

        objSecEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
        objSecEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);
        objSecEnt.TRAN_TYPE = Convert.ToInt32(rdlList.SelectedValue);

        if (ViewState["Action"].ToString() == "Add")
        {
            objSecEnt.SPID = 0;
            GetOWRegSlipNo();
            CustomStatus cs = (CustomStatus)objSecCon.InsUpdateSecPassOutward(objSecEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved & Reg.Slip Number Generated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        else
        {
            objSecEnt.SP_OW_ID = Convert.ToInt32(ViewState["SP_OW_ID"]);
            objSecEnt.REG_SLIP_NO = txtRegSlipNo.Text;
            CustomStatus cs = (CustomStatus)objSecCon.InsUpdateSecPassOutward(objSecEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Updated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        //ClearAll();
        divTranSlipNo.Visible = true;
        btnSubmit.Enabled = false;
        btnCancel.Visible = false;
        btnAddNew2.Visible = true;

    }
    private void GetOWRegSlipNo()
    {
        DataSet ds = objSecCon.GenerateOWRegSlipNo();
        txtRegSlipNo.Text = ds.Tables[0].Rows[0]["REG_SLIP_NO"].ToString();
        objSecEnt.REG_SLIP_NO = txtRegSlipNo.Text;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {

        //  txtRegSlipNo.Text = string.Empty;
        //txtVehicleNo.Text = string.Empty;
        txtCarriedEmp.Text = string.Empty;

        divEmp.Visible = false;
        ViewState["Action"] = "Add";
        divButtons.Visible = true;
        btnSubmit.Enabled = true;

        if (rdlList.SelectedValue == "1")
        {
            divTranSlipNo.Visible = false;
            lvDsrItem.DataSource = null;
            lvDsrItem.DataBind();
            divlvDsr.Visible = false;
            ddlGatePass.SelectedIndex = 0;
            txtOutDate.Text = string.Empty;
            txtOutTime.Text = string.Empty;
            txtVehicleNo.Text = "";
        }
        else
        {
            foreach (ListViewItem lv in lvDsrItem.Items)
            {
                CheckBox chkDsrselect = lv.FindControl("chkDsrselect") as CheckBox;
                chkDsrselect.Checked = false;
            }

            txtReceiveDate.Text = string.Empty;
            txtReceiveTime.Text = string.Empty;
        }



    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //if (rdlList.SelectedValue == "2")
        //{
        //    divAddNew.Visible = false;
        //}
        if (rdlList.SelectedValue == "1")
        {
            divAddNew.Visible = true;
            btnAddNew.Visible = true;
        }
        else
        {

            btnAddNew.Visible = false;
        }
        ClearAll();
        BindListView();
        // divListOutwardGP.Visible = true;
        divOutwardGP.Visible = false;
        FillDropDownList();
        divButtons.Visible = false;
        pnlAddNew.Visible = true;
        divRadioList.Visible = true;
        divAddNew.Visible = true;

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Img = sender as ImageButton;
        int SP_OW_ID = Convert.ToInt32(Img.CommandArgument);
        int Count = Convert.ToInt32(objCommon.LookUp("STORE_SEC_PASS_OUTWARD A INNER JOIN STORE_ITEM_REPAIR_TRAN B ON (A.IR_ID=B.IR_ID)", "COUNT(*)", "IS_RET_SEC = 'Y' AND SP_OW_ID=" + SP_OW_ID));

        if (Count > 0)
        {
            MessageBox("Some Items Received Against This Gate Pass Number,So You Can Not Modify");
            return;
        }
        ViewState["SP_OW_ID"] = SP_OW_ID;
        DataSet ds = objSecCon.GetItemRepairDetails(SP_OW_ID);
        ClearAll();
        divAddNew.Visible = false;
        divOutwardGP.Visible = true;
        pnlAddNew.Visible = false;
        divButtons.Visible = true;
        divReceiveFields.Visible = false;

        ddlGatePass.Enabled = true;
        txtVehicleNo.Enabled = true;
        txtOutDate.Enabled = true;
        ceOutDate.Enabled = true;
        txtOutTime.Enabled = true;
        lvDsrItem.Enabled = false;
        ddlGatePass.Enabled = false;

        //FillDropDownList();
        ViewState["Action"] = "Edit";
        btnAddNew2.Visible = false;
        btnCancel.Visible = false;


        //ddlGatePass.DataSource = ds.Tables[0];
        //ddlGatePass.DataTextField = ds.Tables[0].Columns["GP_NUMBER"].ToString();
        //ddlGatePass.DataValueField = ds.Tables[0].Columns["IR_ID"].ToString();

        //ddlGatePass.DataBind();

        FillDropDownList();


        txtRegSlipNo.Text = ds.Tables[0].Rows[0]["REG_SLIP_NO"].ToString();
        divTranSlipNo.Visible = true;
        ddlGatePass.SelectedValue = ds.Tables[0].Rows[0]["IR_ID"].ToString();
        txtVehicleNo.Text = ds.Tables[0].Rows[0]["VEHICLE_NO"].ToString();
        txtOutDate.Text = ds.Tables[0].Rows[0]["OUT_DATE"].ToString();
        txtOutTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OUT_TIME"]).ToString("hh:mm tt");
        txtCarriedEmp.Text = ds.Tables[0].Rows[0]["EMP_NAME"].ToString();
        divEmp.Visible = true;
        if (ds.Tables[1].Rows.Count > 0)
        {
            lvDsrItem.DataSource = ds.Tables[1];
            lvDsrItem.DataBind();
            divlvDsr.Visible = true;
        }
        else
        {
            lvDsrItem.DataSource = null;
            lvDsrItem.DataBind();
            divlvDsr.Visible = false;
        }
        //ddlGatePass.SELE
    }

    protected void rdlList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        if (rdlList.SelectedValue == "2")
        {
            divAddNew.Visible = false;
            thSelAction.Visible = true;
            thEditAction.Visible = false;
        }
        else
        {
            btnAddNew.Visible = true;
            divAddNew.Visible = true;
            thSelAction.Visible = false;
            thEditAction.Visible = true;
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button Img = sender as Button;
        int SP_OW_ID = Convert.ToInt32(Img.CommandArgument);

        ViewState["SP_OW_ID"] = SP_OW_ID;
        DataSet ds = objSecCon.GetItemRepairDetails(SP_OW_ID);
        ClearAll();
        divAddNew.Visible = false;
        divOutwardGP.Visible = true;
        divListOutwardGP.Visible = false;
        divButtons.Visible = true;
        pnlAddNew.Visible = false;
        divReceiveFields.Visible = true;
        btnAddNew2.Visible = false;

        //vishwas
        // ddlGatePass.Enabled = false;
        ddlGatePass.Enabled = true;
        txtVehicleNo.Enabled = false;
        txtOutDate.Enabled = false;
        ceOutDate.Enabled = false;
        txtOutTime.Enabled = false;
        lvDsrItem.Enabled = true;

        //FillDropDownList();
        ViewState["Action"] = "Edit";

        txtRegSlipNo.Text = ds.Tables[0].Rows[0]["REG_SLIP_NO"].ToString();
        divTranSlipNo.Visible = true;

        FillDropDownList();

        ddlGatePass.SelectedValue = ds.Tables[0].Rows[0]["IR_ID"].ToString();
        ddlGatePass.Enabled = false;

        txtVehicleNo.Text = ds.Tables[0].Rows[0]["VEHICLE_NO"].ToString();
        txtOutDate.Text = ds.Tables[0].Rows[0]["OUT_DATE"].ToString();
        txtOutTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OUT_TIME"]).ToString("hh:mm tt");
        //if (ds.Tables[0].Rows[0]["RECEIVED_DATE"].ToString() != "")
        //    txtReceiveDate.Text = ds.Tables[0].Rows[0]["RECEIVED_DATE"].ToString();
        //if (ds.Tables[0].Rows[0]["RECEIVED_TIME"].ToString() != "")
        //    txtReceiveTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["RECEIVED_TIME"]).ToString("hh:mm tt");
        txtCarriedEmp.Text = ds.Tables[0].Rows[0]["EMP_NAME"].ToString();

        if (ds.Tables[1].Rows.Count > 0)
        {
            lvDsrItem.DataSource = ds.Tables[1];
            lvDsrItem.DataBind();
            divlvDsr.Visible = true;
        }
        else
        {
            lvDsrItem.DataSource = null;
            lvDsrItem.DataBind();
            divlvDsr.Visible = false;
        }
    }
}