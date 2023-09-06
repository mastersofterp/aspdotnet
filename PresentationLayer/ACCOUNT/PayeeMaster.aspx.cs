//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYEE MASTER PAGE FOR CHEQUE PRINTING MODULE                                                     
// CREATION DATE : 28-04-2010                                               
// CREATED BY    : JITENDRA M. CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Reflection;
using System.Data.SqlClient;
using IITMS.NITPRM;
using System.IO;

public partial class PayeeMaster : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountTransactionController oacc = new AccountTransactionController();
    PayeeMasterClass obj = new PayeeMasterClass();
    public string back=string.Empty;
    GridView gv = new GridView();
    public string[] para;
    protected void Page_PreInit(object sender, EventArgs e)
    {
       if (Request.QueryString["obj"] != null)
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
        else
        {

            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtPayee.Focus();

            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                //  Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    objCommon.DisplayMessage("Select company/cash book.", this);

                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {

                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();


                    //PopulateDropDown();
                    PopulateListBox();

                    ViewState["action"] = "add";
                }
            }
            if (Request.QueryString["obj"] != null)
            {
                para = Request.QueryString["obj"].ToString().Trim().Split(',');
                string PartyNo = para[0];
              
                string PartyName = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "ACC_CODE='" + PartyNo + "'");
                double Balance = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "BALANCE", "ACC_CODE='" + PartyNo + "'"));
                if (Balance < 0)
                {
                    lblCurBal.Text = (-1 * Balance).ToString();
                    txtmd.Text = "Cr";
                }
                else
                {
                    lblCurBal.Text = (Balance).ToString();
                    txtmd.Text = "Dr";
                }

                txtAcc.Text = PartyName + "*" + PartyNo;
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Remove("obj");
            }
            //Page Authorization
            if (Request.QueryString["obj"] == null)
                CheckPageAuthorization();
            Filldropdown();
            
        }
      
        
    }
    public void Filldropdown()
    {

     objCommon.FillDropDownList(ddlBank,"ACC_BANK_DETAIL", "BANKNO", "BANKNAME", "", "");
     objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "");
    }
    protected void ddlPayeeNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        Boolean PayeeNature = Convert.ToBoolean(objCommon.LookUp("ACC_PAYEE_NATURE_MASTER", "isnull(IS_LEDGER_MANDATORY,0) as IS_LEDGER_MANDATORY", "NATURE_ID=" + (ddlPayeeNature.SelectedValue)));

        if (PayeeNature == true)
        {
            txtAcc.Enabled = true;

        }
        else
        {
            txtAcc.Enabled = false;
        }
    }
    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMergeLedger(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetMergeData(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }
    private void PopulateListBox()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PAYEE", "IDNO", "UPPER(PARTYNAME) AS PARTYNAME", "IDNO > 0", "IDNO");// "PARTY_NAME");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstBankName.Items.Clear();
                    lstBankName.DataTextField = "PARTYNAME";
                    lstBankName.DataValueField = "IDNO";
                    lstBankName.DataSource = ds.Tables[0];
                    lstBankName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountMaster.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Boolean PayeeNature =Convert.ToBoolean(objCommon.LookUp("ACC_PAYEE_NATURE_MASTER", "isnull(IS_LEDGER_MANDATORY,0) as IS_LEDGER_MANDATORY", "NATURE_ID=" + (ddlPayeeNature.SelectedValue)));

        if (PayeeNature == true)
        {
            if (txtAcc.Text != string.Empty)
            {
            }
            else
            {

                objCommon.DisplayUserMessage(UPDLedger, "Please Select Ledger", this);
                return;
            }
            
        }
         // string Val = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NO='" + txtAcc.Text.Split('*') + "'");


            if (ViewState["id"] == null)
            {
                if (txtAcc.Text != string.Empty)
                {
                    hdnOpartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'");
                    int partno = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'"));

                    //AccountTransactionController oacc = new AccountTransactionController();
                    //IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass obj = new IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass();

                    string checkparty = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PAYEE", "PARTY_NO", "PARTY_NO=" + partno);


                    if (checkparty.ToString() == "" || checkparty.ToString() != partno.ToString())
                    {
                        obj.IDNO = 0;
                        obj.PARTYNAME = txtPayee.Text.ToString().Trim();
                        if (txtAccountCode.Text == string.Empty)
                            obj.ACCNO = "0";
                        else
                            obj.ACCNO = txtAccountCode.Text.ToString().Trim();
                        obj.IFSC = txtIfsc.Text.ToString().Trim();
                        obj.BRANCH = txtBranch.Text.ToString().Trim();

                        obj.ADDRESS = txtAddress.Text.ToString().Trim();
                        obj.PARTY_NO = Convert.ToInt32(hdnOpartyManual.Value);
                        obj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue);
                        obj.NATURE_ID = Convert.ToInt32(ddlPayeeNature.SelectedValue);
                        obj.PAN_NUMBER = txtPanNo.Text.Trim();
                        obj.CONTACT_NO = txtContactNo.Text.ToString().Trim();
                        obj.EMAIL_ID = txtEmail.Text.ToString().Trim();
                        if (chkstatus.Checked == true)
                        {
                            obj.CAN = 1;
                        }
                        else
                        {
                            obj.CAN = 0;
                        }

                        int res = oacc.AddPayeeDetails(obj, Session["comp_code"].ToString().Trim());
                        if (res == 1)
                        {
                            objCommon.DisplayMessage(UPDLedger, "Payee Data Created Sucessfully", this);
                            ClearAll();
                            PopulateListBox();
                            txtPayee.Focus();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UPDLedger, "Ledger Already Exist", this);
                        return;
                    }
                }
                else
                {
                    hdnOpartyManual.Value = "0"; // objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'");
                    int partno = 0; // Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'"));

                    //AccountTransactionController oacc = new AccountTransactionController();
                    //IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass obj = new IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass();

                    obj.IDNO = 0;
                    obj.PARTYNAME = txtPayee.Text.ToString().Trim();
                    if (txtAccountCode.Text == string.Empty)
                        obj.ACCNO = "0";
                    else
                        obj.ACCNO = txtAccountCode.Text.ToString().Trim();
                    obj.IFSC = txtIfsc.Text.ToString().Trim();
                    obj.BRANCH = txtBranch.Text.ToString().Trim();

                    obj.ADDRESS = txtAddress.Text.ToString().Trim();
                    obj.PARTY_NO = 0; // Convert.ToInt32(hdnOpartyManual.Value);
                    obj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue);
                    obj.NATURE_ID = Convert.ToInt32(ddlPayeeNature.SelectedValue);
                    obj.PAN_NUMBER = txtPanNo.Text.Trim();
                    obj.CONTACT_NO = txtContactNo.Text.ToString().Trim();
                    obj.EMAIL_ID = txtEmail.Text.ToString().Trim();
                    if (chkstatus.Checked == true)
                    {
                        obj.CAN = 1;
                    }
                    else
                    {
                        obj.CAN = 0;
                    }

                    int res = oacc.AddPayeeDetails(obj, Session["comp_code"].ToString().Trim());
                    if (res == 1)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Payee Data Created Sucessfully", this);
                        ClearAll();
                        PopulateListBox();
                        txtPayee.Focus();
                        return;
                    }
                }
           
        }
       
        else
        {
            if (ViewState["id"].ToString() == "")
            {


                //AccountTransactionController oacc = new AccountTransactionController();
                //IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass obj = new IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass();
                obj.IDNO = 0;
                obj.PARTYNAME = txtPayee.Text.ToString().Trim();
                if (txtAccountCode.Text == string.Empty)
                    obj.ACCNO = "0";
                else
                    obj.ACCNO = txtAccountCode.Text.ToString().Trim();

                obj.ADDRESS = txtAddress.Text.ToString().Trim();
                obj.IFSC = txtIfsc.Text.ToString().Trim();
                obj.BRANCH = txtBranch.Text.ToString().Trim();

                obj.ADDRESS = txtAddress.Text.ToString().Trim();
                obj.PARTY_NO = Convert.ToInt32(hdnOpartyManual.Value);
                obj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue);
                obj.NATURE_ID = Convert.ToInt32(ddlPayeeNature.SelectedValue);
                obj.PAN_NUMBER = txtPanNo.Text.Trim();
                obj.CONTACT_NO = txtContactNo.Text.ToString().Trim();
                obj.EMAIL_ID = txtEmail.Text.ToString().Trim();
                if (chkstatus.Checked == true)
                {
                    obj.CAN = 1;
                }
                else
                {
                    obj.CAN = 0;
                }
                int res = oacc.AddPayeeDetails(obj, Session["comp_code"].ToString().Trim());
                if (res == 1)
                {
                    objCommon.DisplayMessage(UPDLedger, "Payee Data Created Sucessfully", this);
                    ClearAll();
                    PopulateListBox();
                    txtPayee.Focus();
                    return;
                }

            }
            else
            {
                //updating the account

                //AccountTransactionController oacc = new AccountTransactionController();
                //IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass obj = new IITMS.UAIMS.BusinessLayer.BusinessEntities.PayeeMasterClass();
                obj.IDNO = Convert.ToInt32(ViewState["id"].ToString());
                obj.PARTYNAME = txtPayee.Text.ToString().Trim();
                obj.ACCNO = txtAccountCode.Text.ToString().Trim();
                obj.ADDRESS = txtAddress.Text.ToString().Trim();
                obj.IFSC = txtIfsc.Text.ToString().Trim();
                obj.BRANCH = txtBranch.Text.ToString().Trim();
                obj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue);

                obj.NATURE_ID = Convert.ToInt32(ddlPayeeNature.SelectedValue);
                obj.PARTY_NO = Convert.ToInt32(hdnOpartyManual.Value);
                obj.PAN_NUMBER = txtPanNo.Text.Trim();
                obj.CONTACT_NO = txtContactNo.Text.ToString().Trim();
                obj.EMAIL_ID = txtEmail.Text.ToString().Trim();
                if (chkstatus.Checked == true)
                {
                    obj.CAN = 1;
                }
                else
                {
                    obj.CAN = 0;
                }
                int res = oacc.UpdatePayeeDetails(obj, Session["comp_code"].ToString().Trim());
                if (res == 1)
                {
                    objCommon.DisplayMessage(UPDLedger, "Payee Data updated Sucessfully", this);
                    ClearAll();
                    PopulateListBox();
                    txtPayee.Focus();
                    return;
                }


            }


        }
        

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
      txtPayee.Text = "";
      txtAddress.Text = "";
      txtAccountCode.Text = "";
      chkstatus.Checked = false;
      ViewState["id"] = "";
      txtPayee.Focus();
      txtBranch.Text = string.Empty;
      txtIfsc.Text = string.Empty;
      txtAcc.Text = string.Empty;
      ddlBank.SelectedIndex = 0;
      ddlPayeeNature.SelectedIndex = 0;
      txtPanNo.Text = string.Empty;
      txtContactNo.Text = string.Empty;
      txtEmail.Text = string.Empty;
    }

    protected void lstBankName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //int partyId = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "ACC_NO", "PARTY_NAME="+txtAcc.Text));

            //very important 
            string id = Request.Form[lstBankName.UniqueID].ToString();

            if (id != "" | id != string.Empty)
            {
                ClearAll();
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();

                //Show Details 
                PartyController objPC = new PartyController();
                string code_year = Session["comp_code"].ToString().Trim();// +"_" + Session["fin_yr"].ToString();

                DataSet dscnt = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PAYEE A LEFT JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY B ON A.PARTY_NO=B.PARTY_NO LEFT JOIN ACC_BANK_DETAIL C ON A.BANKNO=C.BANKNO", "A.*", "B.PARTY_NAME,C.BANKNAME,B.ACC_CODE", "IDNO = '" + Convert.ToString(id.ToString()).Trim().ToUpper() + "' ", string.Empty);

                if (dscnt != null)
                {
                    if (dscnt.Tables[0].Rows.Count > 0)
                    {
                        int o = 0;
                        for (o = 0; o < dscnt.Tables[0].Rows.Count; o++)
                        {
                            txtAccountCode.Text = dscnt.Tables[0].Rows[o]["ACCNO"].ToString().Trim();
                            txtPayee.Text = dscnt.Tables[0].Rows[o]["PARTYNAME"].ToString().Trim();
                            txtAddress.Text = dscnt.Tables[0].Rows[o]["ADDRESS"].ToString().Trim();
                            txtBranch.Text = dscnt.Tables[0].Rows[o]["BRANCH"].ToString().Trim();
                            txtIfsc.Text = dscnt.Tables[0].Rows[o]["IFSC_CODE"].ToString().Trim();

                            //if (ddlBank.SelectedItem.Text == string.Empty)
                            //{
                            //    ddlBank.SelectedValue = "0";

                            //}
                            ddlBank.SelectedValue = dscnt.Tables[0].Rows[o]["BANKNO"].ToString();

                            if (dscnt.Tables[0].Rows[o]["PARTY_NAME"].ToString().Trim() != string.Empty && dscnt.Tables[0].Rows[o]["ACC_CODE"].ToString().Trim() != string.Empty)
                            {
                              txtAcc.Text = dscnt.Tables[0].Rows[o]["PARTY_NAME"].ToString().Trim() + "*" + dscnt.Tables[0].Rows[o]["ACC_CODE"].ToString().Trim();
                            }
                            if (dscnt.Tables[0].Rows[o]["PAN_NO"].ToString().Trim() != string.Empty)
                            {
                                txtPanNo.Text = dscnt.Tables[0].Rows[o]["PAN_NO"].ToString().Trim();
                            }
                            if (dscnt.Tables[0].Rows[o]["NATURE_ID"].ToString().Trim() != string.Empty)
                            {
                                ddlPayeeNature.SelectedValue = dscnt.Tables[0].Rows[o]["NATURE_ID"].ToString().Trim();

                            }

                            txtEmail.Text = dscnt.Tables[0].Rows[o]["EMAIL_ID"].ToString().Trim();
                            txtContactNo.Text = dscnt.Tables[0].Rows[o]["CONTACT_NO"].ToString().Trim();

                            if (dscnt.Tables[0].Rows[o]["CAN"].ToString().Trim() == "0")
                            {
                                chkstatus.Checked = false;

                            }
                            else
                            {
                                chkstatus.Checked = true;
                            }
                        
                                               
                        }

                                            
                    
                    }
                 
                
                
                
                }
                
                



            }
            else
            {
                ViewState["action"] = "add";
                ViewState["id"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountMaster.lstBankName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }



    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PAYEE", "IDNO", "PARTYNAME", "PARTYNAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "PARTYNAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstBankName.DataTextField = "PARTYNAME";
                lstBankName.DataValueField = "IDNO";
                lstBankName.DataSource = ds.Tables[0];
                lstBankName.DataBind();

            }
        }

        txtSearch.Focus();

    }

    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int partyId = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE=" + txtAcc.Text.Split('*')[1].ToString()));
            double Balance = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "BALANCE", "PARTY_NO=" + partyId));
            if (Balance > 0)
                lblCurBal.Text = Balance.ToString() + " Dr";
            else
                lblCurBal.Text = Balance.ToString() + " Cr";
        }
        catch (Exception)
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ExportPayeeDetails();
    }
    private void AddExcelHeader(GridView gv)
    {
        try
        {
            string CollegeName = objCommon.LookUp("Reff", "CollegeName", "College_Code='" + Session["colcode"].ToString() + "'");

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header1Cell = new TableCell();

            Header1Cell.Text = CollegeName;
            Header1Cell.ColumnSpan = 9;
            Header1Cell.Font.Size = 13;
            Header1Cell.Font.Bold = true;
            Header1Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow1.Cells.Add(Header1Cell);
            gv.Controls[0].Controls.AddAt(0, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header2Cell = new TableCell();

            Header2Cell.Text = "Report : Payee Master Details";
            Header2Cell.ColumnSpan = 9;
            Header2Cell.Font.Size = 12;
            Header2Cell.Font.Bold = true;
            Header2Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(Header2Cell);
            gv.Controls[0].Controls.AddAt(1, HeaderGridRow2);

            gv.HeaderRow.Visible = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ExportPayeeDetails()
    {
        DataSet dsfee = oacc.GetPayeeDetailsInExcel(Session["comp_code"].ToString().Trim());
        if (dsfee.Tables.Count > 0)
        {
            gv.DataSource = dsfee.Tables[0];
            gv.DataBind();
            AddExcelHeader(gv);
            string attachment = "attachment; filename=" + "PayeeDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.UPDLedger,"No Data Found.", this.Page);
        }

    }
}
