//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Purchase_Order_Preparartion.aspx                                      
// CREATION DATE : 22-march-2010                                                    
// CREATED BY    : chaitanya Bhure                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Linq;

public partial class Stores_Transactions_Quotation_Str_purchase_Order_Preparation : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Purchase_Order_Controller objstrPO = new Str_Purchase_Order_Controller();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();

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
        btnRpt.Visible = false;
        //selected_tab.Value = Request.Form[selected_tab.UniqueID];
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
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ddlInd.Items.Clear();
                this.BindQuotation();
                BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                objCommon.FillDropDownList(ddltendor, "Store_tender", "TNO", "TENDERNO", "FLAG='R'", "TNO DESC");
                objCommon.FillDropDownList(ddlInd, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "TQSTATUS='D' AND INDNO NOT IN (SELECT INDENTNO FROM STORE_PORDER WHERE ISTYPE='D')", "INDTRNO DESC");
                // objCommon.FillDropDownList(ddlIndPP, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "TQSTATUS='P'", "INDTRNO DESC");
                //objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "'['+CONVERT(NVARCHAR(20),PCODE)+'] '+ISNULL(PNAME,'')+''", "", "PCNO DESC");
                objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "ISNULL(PNAME,'')+''", "", "PNAME");
                // Tabs.ActiveTabIndex = 0;
                // hdnValue.Value = "";
                // objCommon.ReportPopUp(btncmpitem, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Single_Item_Cmp_Report.rpt&param=@UserName=" + Session["userfullname"].ToString() + "," + "@P_QUOTNO=" + lstQtNo.SelectedValue + "," + "@P_ITEM_NO=" + lstItem.SelectedValue, "UAIMS");
                ViewState["OthInfo"] = null;
                ViewState["TaxTable"] = null;
                ViewState["Action"] = "add";
            }
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    void BindPOByDept(int mdno)
    {
        DataSet ds = objstrPO.GetAllPOByDepartment(mdno);
        lstPO.DataSource = ds.Tables[0];
        lstPO.DataTextField = "REFNO";
        lstPO.DataValueField = "PORDNO";
        lstPO.DataBind();

    }

    void BindPOTrackByDept(int mdno)
    {
        ddlPOTrack.Items.Clear();
        ddlPOTrack.Items.Add(new ListItem("Please Select", "0", true));
        DataSet ds = objstrPO.GetAllPOByDepartment(mdno);

        ddlPOTrack.DataSource = ds.Tables[0];
        ddlPOTrack.DataTextField = "REFNO";
        ddlPOTrack.DataValueField = "PORDNO";
        ddlPOTrack.DataBind();


    }

    void BindUnLockPOByDept(int Mdno)
    {
        DataSet ds = objstrPO.GetAllUnlockPOByDepartment(Mdno);
        lstPOForLock.DataSource = ds.Tables[0];
        lstPOForLock.DataTextField = "REFNO";
        lstPOForLock.DataValueField = "PORDNO";
        lstPOForLock.DataBind();
    }

    void BindQuotation()
    {
        DataSet ds = objstrPO.GetQuotationByDepartment(Convert.ToInt32(Session["strdeptcode"].ToString()));
        lstQtNo.DataSource = ds.Tables[0];
        lstQtNo.DataTextField = "REFNO";
        lstQtNo.DataValueField = "QUOTNO";
        lstQtNo.DataBind();
    }

    void BindVendorByQuotation(string Quotno)
    {
        DataSet ds = objstrPO.GetVendorByQuotation(Quotno);
        lstVendor.DataSource = ds.Tables[0];
        lstVendor.DataTextField = "PNAME";
        lstVendor.DataValueField = "PNO";
        lstVendor.DataBind();
    }

    void BindItemsRecommandToVendor(string Quotno, int pno)
    {
        DataSet ds = objstrPO.GetItemsRecommandToVendor(Quotno, pno);
        grdPOitems.DataSource = ds.Tables[0];
        grdPOitems.DataBind();
        ViewState["OthInfo"] = ds.Tables[0];
        if (ds.Tables[1].Rows.Count > 0)
        {
            ViewState["TaxTable"] = ds.Tables[1];
        }
        else
        {
            ViewState["Taxtable"] = null;
        }
    }

    void BindFieldsRecommandToVendor(string Quotno, int pno)
    {
        DataSet ds = objstrPO.GetFieldsRecommandToVendor(Quotno, pno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdPartyFields.DataSource = ds.Tables[0];
            grdPartyFields.DataBind();
            divExtraCharges.Visible = true;
        }
        else
        {
            grdPartyFields.DataSource = null;
            grdPartyFields.DataBind();
            divExtraCharges.Visible = false;
        }
    }

    void BindItemsRecommandForTender(int tenderno)
    {
        DataSet ds = objstrPO.GetTenderItemDetails(tenderno);
        grdItemTender.DataSource = ds.Tables[0];
        grdItemTender.DataBind();

        ViewState["OthInfo"] = ds.Tables[0];
        if (ds.Tables[1].Rows.Count > 0)
        {
            ViewState["TaxTable"] = ds.Tables[1];
        }
        else
        {
            ViewState["Taxtable"] = null;
        }
    }

    void BindVendorForTender(int tenderno)
    {
        DataSet ds = objstrPO.GetTenderDetails(tenderno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblVcode.Text = ds.Tables[0].Rows[0]["VENDORCODE"].ToString();
            lblVname.Text = ds.Tables[0].Rows[0]["VENDORNAME"].ToString();
            lblVcontact.Text = ds.Tables[0].Rows[0]["VENDOR_CONTACT"].ToString();
            lblVaddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
            hdnTvno.Value = ds.Tables[0].Rows[0]["TVNO"].ToString();
        }
    }

    void BindFieldsToTender(int Tenderno)
    {
        DataSet ds = objstrPO.GetTenderFields(Tenderno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdPartyFields.DataSource = ds.Tables[0];
            grdPartyFields.DataBind();
            divExtraCharges.Visible = true;
        }
        else
        {
            grdPartyFields.DataSource = null;
            grdPartyFields.DataBind();
            divExtraCharges.Visible = false;
        }
    }

    void BindItemsForDPO(int indentno)
    {
        DataSet ds = objstrPO.GetDPOItemDetails(Convert.ToInt32(ddlInd.SelectedValue));
        lvItem.DataSource = ds.Tables[0];
        lvItem.DataBind();
        lvItem.Visible = true;
        hdnrowcount.Value = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[1].Rows.Count > 0)
        {
            ViewState["TaxTable"] = ds.Tables[1];
            hdnListCount.Value = ds.Tables[1].Rows.Count.ToString();
            ViewState["Action"] = "edit";
            hdnOthEdit.Value = "1";
        }
        else
        {
            ViewState["TaxTable"] = null;
        }
    }

    void ShowDetailPO(int PODRDNO)
    {
        DataSet ds = objstrPO.GetSinglePONO(PODRDNO);
        DataRow dr = ds.Tables[0].Rows[0];
        txtRefNo.Text = dr["REFNO"].ToString().Split('/')[3];
        txtSdate.Text = Convert.ToDateTime(dr["SDATE"]).ToString("dd/MM/yyyy");
        txtDtSend.Text = Convert.ToDateTime(dr["TDATE"]).ToString("dd/MM/yyyy");
        txtSub.Text = dr["SUBJECT"].ToString();
        txtFooter.Text = dr["FOOTER1"].ToString();
        txtTerm.Text = dr["TERM1"].ToString();
        txtTecClar.Text = dr["TECHCLAR"].ToString();
        txtRemark1.Text = dr["REMARK"].ToString();
        txtCifChrge.Text = dr["CIFCHARGE"].ToString();
        txtCifSpec.Text = dr["CIFCHARGETEXT"].ToString();
        txtEncl.Text = dr["ENCL"].ToString();
        txtCopyto.Text = dr["COPYTO"].ToString();
        txtAccHead.Text = dr["HA"].ToString();
        if (dr["AGREEMENT"].ToString() == "1")
            chkAgreement.Checked = true;
        if (dr["CST"].ToString() == "1")
            chkCSTExempt.Checked = true;
        if (dr["DEL"].ToString() == "1")
            chkdelete.Checked = true;
        if (dr["ED"].ToString() == "1")
            chkEDexempt.Checked = true;
        if (dr["RELISHED"].ToString() == "1")
            chkLC.Checked = true;
        if (dr["PENDING"].ToString() == "1")
            chkPending.Checked = true;
        if (dr["RELIES"].ToString() == "1")
            chkRelies.Checked = true;
        if (dr["SDPER"].ToString() == "1")
            chkSD.Checked = true;
        if (dr["SIGN"].ToString().Trim() == "Director")
        {
            rdDirector.Checked = true;
        }
        else
        {
            rdforDirector.Checked = true;
        }
        BindQuotation();

        lstQtNo.SelectedValue = dr["QUOTNO"].ToString();
        lstVendor.DataSource = objstrPO.GetVendorByQuotation(lstQtNo.SelectedValue).Tables[1];
        lstVendor.DataTextField = "PNAME";
        lstVendor.DataValueField = "PNO";
        lstVendor.DataBind();
        lstVendor.SelectedValue = dr["PNO"].ToString();
        //lstVendor.Enabled = false;
        lstQtNo.Enabled = false;
    }

    void GeneratePorderNo()
    {
        DataSet ds = new DataSet();
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ds = objstrPO.GenratePno(mdno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (radSelType.SelectedValue == "0")
            {
                txtRefNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["POREDERNO"].ToString());
            }
            else if (radSelType.SelectedValue == "1")
            {
                txtRefTender.Text = Convert.ToString(ds.Tables[0].Rows[0]["POREDERNO"].ToString());
            }
            else if (radSelType.SelectedValue == "4")
            {
                txtRefTender.Text = Convert.ToString(ds.Tables[0].Rows[0]["POREDERNO"].ToString());
            }

            else
            {
                txtRefDPO.Text = Convert.ToString(ds.Tables[0].Rows[0]["POREDERNO"].ToString());
            }
        }
    }

    void Clear()
    {
        lstQtNo.Enabled = true;
        ViewState["action"] = "add";
        txtAccHead.Text = string.Empty;
        //txtCifChrge.Text = string.Empty;
        txtCifChrge.Text = "0.00";
        txtCifSpec.Text = string.Empty;
        txtCopyto.Text = string.Empty;
        txtDtSend.Text = string.Empty;
        txtEncl.Text = string.Empty;
        txtFooter.Text = string.Empty;
        //txtItemSpec.Text = string.Empty;
        txtRefNo.Text = string.Empty;
        txtRemark1.Text = string.Empty;
        txtSdate.Text = string.Empty;
        txtSub.Text = string.Empty;
        txtTecClar.Text = string.Empty;
        txtTerm.Text = string.Empty;
        //txtRemark2.Text = string.Empty;
        chkAgreement.Checked = false;
        chkCSTExempt.Checked = false;
        chkdelete.Checked = false;
        chkEDexempt.Checked = false;
        chkLC.Checked = false;
        chkPending.Checked = false;
        chkRelies.Checked = false;
        chkSD.Checked = false;
        //rdDirector.Checked = false;
        rdforDirector.Checked = false;
        BindVendorByQuotation(lstQtNo.SelectedValue);
        grdPOitems.DataSource = null;
        grdPOitems.DataBind();

        txtDeliveryAt.Text = string.Empty;
        txtDeliverySchedule.Text = string.Empty;
        txtHandlingCharges.Text = string.Empty;
        txtTransportCharges.Text = string.Empty;
        txtModeofDespatch.Text = string.Empty;
        rdlInsured.SelectedValue = "2";
        objCommon.FillDropDownList(ddlInd, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "TQSTATUS='D' AND INDNO NOT IN (SELECT INDENTNO FROM STORE_PORDER WHERE ISTYPE='D')", "INDTRNO DESC");
    }

    void clearTenderControl()
    {
        lblVaddress.Text = string.Empty;
        lblVcode.Text = string.Empty;
        lblVcontact.Text = string.Empty;
        lblVname.Text = string.Empty;
        txtDtsendTender.Text = string.Empty;
        txtRefTender.Text = string.Empty;
        txtSdateTender.Text = string.Empty;
        ddltendor.SelectedIndex = 0;
        txtSubjectTender.Text = string.Empty;
        grdItemTender.DataSource = null;
        grdItemTender.DataBind();
    }

    void clearDPOControl()
    {
        ddlVendor.SelectedIndex = 0;
        ddlInd.SelectedIndex = 0;
        lblcontact.Text = string.Empty;
        lblAdd.Text = string.Empty;
        txtRefDPO.Text = string.Empty;
        txtDtSendDPO.Text = string.Empty;
        txtSdateDPO.Text = string.Empty;
        txtSubjectDPO.Text = string.Empty;
        lvItem.DataSource = null;
        lvItem.DataBind();
        lvItem.Visible = false;
        txtFooter.Text = string.Empty;
        txtRemark1.Text = string.Empty;
    }

    //void showpanel(int selection)
    //{
    //    switch (selection)
    //    {
    //        case 0:
    //            {
    //                TabPanel1.Enabled = true;
    //                TabPanel2.Enabled = true;
    //                TabPanel3.Enabled = true;
    //                break;
    //            }
    //        case 1:
    //            {
    //                TabPanel7.Enabled = true;
    //                TabPanel2.Enabled = true;
    //                TabPanel3.Enabled = true;
    //                ddltendor.Items.Clear();
    //                objCommon.FillDropDownList(ddltendor, "Store_tender", "TNO", "TENDERNO", "FLAG='R' AND TENDERNO like '%OP%'", "TNO DESC");
    //                break;
    //            }
    //        case 2:
    //            {
    //                TabPanel6.Enabled = true;
    //                TabPanel2.Enabled = true;
    //                TabPanel3.Enabled = true;
    //                ddlInd.Items.Clear();
    //                objCommon.FillDropDownList(ddlInd, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "TQSTATUS='D'", "INDTRNO DESC");
    //                break;
    //            }
    //        case 3:
    //            {
    //                TabPanel6.Enabled = true;
    //                TabPanel2.Enabled = true;
    //                TabPanel3.Enabled = true;
    //                //ddlInd.Enabled = false;
    //                ddlInd.Items.Clear();
    //                objCommon.FillDropDownList(ddlInd, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "TQSTATUS='P'", "INDTRNO DESC");
    //                break;
    //            }
    //        case 4:
    //            {
    //                TabPanel7.Enabled = true;
    //                TabPanel2.Enabled = true;
    //                TabPanel3.Enabled = true;
    //                ddltendor.Items.Clear();
    //                objCommon.FillDropDownList(ddltendor, "Store_tender", "TNO", "TENDERNO", "FLAG='R' AND TENDERNO like '%LT%'", "TNO DESC");

    //                break;
    //            }
    //        default:
    //            {
    //                TabPanel6.Enabled = false;
    //                TabPanel7.Enabled = false;
    //                TabPanel1.Enabled = false;
    //                break;
    //            }
    //    }



    //}

    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
        //string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        //string message = string.Format(prompt, Message);
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }
    //Save Po.
    int SavePO(string Colcode)
    {
        Str_Purchase_Order objPO = new Str_Purchase_Order();
        objPO.QUOTNO = lstQtNo.SelectedValue;
        objPO.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
        objPO.PNO = Convert.ToInt32(lstVendor.SelectedValue);
        objPO.REFNO = txtRefNo.Text;

        if (txtSdate.Text != string.Empty)
        {
            objPO.SDATE = Convert.ToDateTime(txtSdate.Text);
        }
        else
        {
            objPO.SDATE = DateTime.MinValue;
        }

        if (txtDtSend.Text != string.Empty)
        {
            objPO.TDATE = Convert.ToDateTime(txtDtSend.Text);
        }
        else
        {
            objPO.TDATE = DateTime.MinValue;
        }

        objPO.TERM1 = txtTerm.Text;
        objPO.SUBJECT = txtSub.Text;
        objPO.REMARK = txtRemark1.Text;
        objPO.TECHCLAR = txtTecClar.Text;
        objPO.CIFCHARGE = Convert.ToDouble(txtCifChrge.Text);
        objPO.CIFCHARGETEXT = txtCifSpec.Text;
        objPO.ENCL = txtEncl.Text;
        objPO.COPYTO = txtCopyto.Text;
        objPO.HA = txtAccHead.Text;
        objPO.SDPER = chkSD.Checked ? '1' : '0';
        objPO.AGREEMENT = chkAgreement.Checked ? '1' : '0';
        objPO.RELISHED = chkLC.Checked ? '1' : '0';
        objPO.RELIES = chkRelies.Checked ? '1' : '0';
        objPO.SIGN = rdDirector.Checked ? rdDirector.Text : rdforDirector.Text;
        objPO.ED = chkEDexempt.Checked ? '1' : '0';
        objPO.CST = chkCSTExempt.Checked ? '1' : '0';
        objPO.FLAG = '0';
        objPO.FLAGINV = '0';
        objPO.FINAL = '0';
        objPO.PENDING = chkPending.Checked ? '1' : '0';
        objPO.DEL = chkdelete.Checked ? '1' : '0';
        objPO.PINO = 0;
        objPO.FOOTER1 = txtFooter.Text;
        objPO.ISTYPE = 'Q';
        objPO.BANKGTY = chkBankGty.Checked ? '1' : '0';
        objPO.AMOUNT = Convert.ToDouble(txtAmount.Text);
        objPO.BANK_REMARK = txtBankRemark.Text;



        string ItemNos = "";
        for (int j = 0; j < grdPOitems.Rows.Count; j++)
        {
            ImageButton btn = (ImageButton)grdPOitems.Rows[j].FindControl("btnAddTax");
            ItemNos += btn.CommandArgument + ',';
        }
        ItemNos = ItemNos.Substring(0, ItemNos.Length - 1);


        int i = objstrPO.SavePO(objPO, Session["colcode"].ToString(), ItemNos);
        return i;
        // DisplayMessage("Purchase Order Save Successfuly");
    }
    //Update Po.
    int UpdatePO(String colcode)
    {
        Str_Purchase_Order objPO = new Str_Purchase_Order();
        objPO.PORDNO = Convert.ToInt32(lstPOForLock.SelectedValue);
        objPO.QUOTNO = lstQtNo.SelectedValue;
        objPO.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
        objPO.PNO = Convert.ToInt32(lstVendor.SelectedValue);
        objPO.REFNO = txtRefNo.Text;
        objPO.SDATE = Convert.ToDateTime(txtSdate.Text);
        objPO.TDATE = Convert.ToDateTime(txtDtSend.Text);
        objPO.TERM1 = txtTerm.Text;
        objPO.SUBJECT = txtSub.Text;
        objPO.REMARK = txtRemark1.Text;
        objPO.TECHCLAR = txtTecClar.Text;
        objPO.CIFCHARGE = Convert.ToDouble(txtCifChrge.Text);
        objPO.CIFCHARGETEXT = txtCifSpec.Text;
        objPO.ENCL = txtEncl.Text;
        objPO.COPYTO = txtCopyto.Text;
        objPO.HA = txtAccHead.Text;
        objPO.SDPER = chkSD.Checked ? '1' : '0';
        objPO.AGREEMENT = chkAgreement.Checked ? '1' : '0';
        objPO.RELISHED = chkLC.Checked ? '1' : '0';
        objPO.RELIES = chkRelies.Checked ? '1' : '0';
        objPO.SIGN = rdDirector.Checked ? rdDirector.Text : rdforDirector.Text;
        objPO.ED = chkEDexempt.Checked ? '1' : '0';
        objPO.CST = chkCSTExempt.Checked ? '1' : '0';
        objPO.FLAG = '0';
        objPO.FLAGINV = '0';
        objPO.FINAL = '0';
        objPO.PENDING = chkPending.Checked ? '1' : '0';
        objPO.DEL = chkdelete.Checked ? '1' : '0';
        objPO.PINO = 0;
        objPO.FOOTER1 = txtFooter.Text;
        objPO.ISTYPE = 'Q';
        objPO.BANKGTY = chkBankGty.Checked ? '1' : '0';
        objPO.AMOUNT = Convert.ToDouble(txtAmount.Text);
        objPO.BANK_REMARK = txtBankRemark.Text;


        int i = objstrPO.UpdatePO(objPO, Session["colcode"].ToString());
        return i;
        // DisplayMessage("Purchase Order Save Successfuly");

    }

    int SaveTenderPO(string Colcode)
    {
        Str_Purchase_Order objPO = new Str_Purchase_Order();
        objPO.TENDERNO = Convert.ToString(ddltendor.SelectedItem);
        objPO.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
        objPO.PNO = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_RECOMMAND", "TVNO", "TENDERNO='" + Convert.ToInt32(ddltendor.SelectedValue) + "'"));
        objPO.REFNO = txtRefTender.Text;
        objPO.SDATE = Convert.ToDateTime(txtSdateTender.Text);
        objPO.TDATE = Convert.ToDateTime(txtDtsendTender.Text);
        objPO.TERM1 = txtTerm.Text;
        objPO.SUBJECT = txtSubjectTender.Text;
        objPO.REMARK = txtRemark1.Text;
        objPO.TECHCLAR = txtTecClar.Text;
        objPO.CIFCHARGE = Convert.ToDouble(txtCifChrge.Text);
        objPO.CIFCHARGETEXT = txtCifSpec.Text;
        objPO.ENCL = txtEncl.Text;
        objPO.COPYTO = txtCopyto.Text;
        objPO.HA = txtAccHead.Text;
        objPO.SDPER = chkSD.Checked ? '1' : '0';
        objPO.AGREEMENT = chkAgreement.Checked ? '1' : '0';
        objPO.RELISHED = chkLC.Checked ? '1' : '0';
        objPO.RELIES = chkRelies.Checked ? '1' : '0';
        objPO.SIGN = rdDirector.Checked ? rdDirector.Text : rdforDirector.Text;
        objPO.ED = chkEDexempt.Checked ? '1' : '0';
        objPO.CST = chkCSTExempt.Checked ? '1' : '0';
        objPO.FLAG = '0';
        objPO.FLAGINV = '0';
        objPO.FINAL = '0';
        objPO.PENDING = chkPending.Checked ? '1' : '0';
        objPO.DEL = chkdelete.Checked ? '1' : '0';
        objPO.PINO = 0;
        objPO.FOOTER1 = txtFooter.Text;
        if (radSelType.SelectedValue == "1")
        {
            objPO.ISTYPE = 'T';
        }
        else
        {
            objPO.ISTYPE = 'L';
        }
        objPO.BANKGTY = chkBankGty.Checked ? '1' : '0';
        objPO.AMOUNT = Convert.ToDouble(txtAmount.Text);
        objPO.BANK_REMARK = txtBankRemark.Text;

        int i = objstrPO.SaveTenderPO(objPO, Session["colcode"].ToString());
        return i;
        // DisplayMessage("Purchase Order Save Successfuly");
    }

    int UpdateTenderPO(String colcode)
    {
        Str_Purchase_Order objPO = new Str_Purchase_Order();
        objPO.PORDNO = Convert.ToInt32(lstPOForLock.SelectedValue);
        objPO.QUOTNO = lstQtNo.SelectedValue;
        objPO.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
        objPO.PNO = Convert.ToInt32(lstVendor.SelectedValue);
        objPO.REFNO = txtRefNo.Text;
        objPO.SDATE = Convert.ToDateTime(txtSdate.Text);
        objPO.TDATE = Convert.ToDateTime(txtDtSend.Text);
        objPO.TERM1 = txtTerm.Text;
        objPO.SUBJECT = txtSub.Text;
        objPO.REMARK = txtRemark1.Text;
        objPO.TECHCLAR = txtTecClar.Text;
        objPO.CIFCHARGE = Convert.ToDouble(txtCifChrge.Text);
        objPO.CIFCHARGETEXT = txtCifSpec.Text;
        objPO.ENCL = txtEncl.Text;
        objPO.COPYTO = txtCopyto.Text;
        objPO.HA = txtAccHead.Text;
        objPO.SDPER = chkSD.Checked ? '1' : '0';
        objPO.AGREEMENT = chkAgreement.Checked ? '1' : '0';
        objPO.RELISHED = chkLC.Checked ? '1' : '0';
        objPO.RELIES = chkRelies.Checked ? '1' : '0';
        objPO.SIGN = rdDirector.Checked ? rdDirector.Text : rdforDirector.Text;
        objPO.ED = chkEDexempt.Checked ? '1' : '0';
        objPO.CST = chkCSTExempt.Checked ? '1' : '0';
        objPO.FLAG = '0';
        objPO.FLAGINV = '0';
        objPO.FINAL = '0';
        objPO.PENDING = chkPending.Checked ? '1' : '0';
        objPO.DEL = chkdelete.Checked ? '1' : '0';
        objPO.PINO = 0;
        objPO.FOOTER1 = txtFooter.Text;
        if (radSelType.SelectedIndex == 1)
        {
            objPO.ISTYPE = 'T';
        }
        else
        {
            objPO.ISTYPE = 'L';
        }
        objPO.BANKGTY = chkBankGty.Checked ? '1' : '0';
        objPO.AMOUNT = Convert.ToDouble(txtAmount.Text);
        objPO.BANK_REMARK = txtBankRemark.Text;

        int i = objstrPO.UpdateTenderPO(objPO, Session["colcode"].ToString());
        return i;
        // DisplayMessage("Purchase Order Save Successfuly");

    }

    int SaveDPOPO(string Colcode)
    {
        if (ddlVendor.SelectedIndex > 0)
        {
            Str_Purchase_Order objPO = new Str_Purchase_Order();

            AddItemTable();

            objPO.INDENTNO = Convert.ToString(ddlInd.SelectedItem);
            objPO.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
            objPO.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
            objPO.REFNO = txtRefDPO.Text;
            objPO.SDATE = Convert.ToDateTime(txtSdateDPO.Text);
            objPO.TDATE = Convert.ToDateTime(txtDtSendDPO.Text);
            objPO.TERM1 = txtTerm.Text;
            objPO.SUBJECT = txtSubjectDPO.Text;
            objPO.REMARK = txtRemark1.Text;
            objPO.TECHCLAR = txtTecClar.Text;
            objPO.CIFCHARGE = Convert.ToDouble(txtCifChrge.Text);
            objPO.CIFCHARGETEXT = txtCifSpec.Text;
            objPO.ENCL = txtEncl.Text;
            objPO.COPYTO = txtCopyto.Text;
            objPO.HA = txtAccHead.Text;
            objPO.SDPER = chkSD.Checked ? '1' : '0';
            objPO.AGREEMENT = chkAgreement.Checked ? '1' : '0';
            objPO.RELISHED = chkLC.Checked ? '1' : '0';
            objPO.RELIES = chkRelies.Checked ? '1' : '0';
            objPO.SIGN = rdDirector.Checked ? rdDirector.Text : rdforDirector.Text;
            objPO.ED = chkEDexempt.Checked ? '1' : '0';
            objPO.CST = chkCSTExempt.Checked ? '1' : '0';
            objPO.FLAG = '0';
            objPO.FLAGINV = '0';
            objPO.FINAL = '0';
            objPO.PENDING = chkPending.Checked ? '1' : '0';
            objPO.DEL = chkdelete.Checked ? '1' : '0';
            objPO.PINO = 0;
            objPO.FOOTER1 = txtFooter.Text;
            if (radSelType.SelectedValue == "2")
            {
                objPO.ISTYPE = 'D';
            }
            else
            {
                objPO.ISTYPE = 'P';
            }
            objPO.BANKGTY = chkBankGty.Checked ? '1' : '0';
            objPO.AMOUNT = Convert.ToDouble(txtAmount.Text);
            objPO.BANK_REMARK = txtBankRemark.Text;


            objPO.DPO_ITEM_TBL = dtItemTable;
            objPO.DPO_TAX_TBL = ViewState["TaxTable"] as DataTable;

            int i = objstrPO.SaveDPOPO(objPO, Session["colcode"].ToString());
            return i;
        }
        else
        {
            DisplayMessage("Please Select Vendor Name");
            return (99);
        }
        // DisplayMessage("Purchase Order Save Successfuly");
    }

    //Update Po.
    int UpdateDPOPO(String colcode)
    {
        Str_Purchase_Order objPO = new Str_Purchase_Order();
        objPO.PORDNO = Convert.ToInt32(lstPOForLock.SelectedValue);
        objPO.INDENTNO = ddlInd.SelectedItem.ToString();
        objPO.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
        objPO.PNO = Convert.ToInt32(ddltendor.SelectedValue);
        objPO.REFNO = txtRefDPO.Text;
        objPO.SDATE = Convert.ToDateTime(txtSdateDPO.Text);
        objPO.TDATE = Convert.ToDateTime(txtDtSendDPO.Text);
        objPO.TERM1 = txtTerm.Text;
        objPO.SUBJECT = txtSubjectDPO.Text;
        objPO.REMARK = txtRemark1.Text;
        objPO.TECHCLAR = txtTecClar.Text;
        objPO.CIFCHARGE = Convert.ToDouble(txtCifChrge.Text);
        objPO.CIFCHARGETEXT = txtCifSpec.Text;
        objPO.ENCL = txtEncl.Text;
        objPO.COPYTO = txtCopyto.Text;
        objPO.HA = txtAccHead.Text;
        objPO.SDPER = chkSD.Checked ? '1' : '0';
        objPO.AGREEMENT = chkAgreement.Checked ? '1' : '0';
        objPO.RELISHED = chkLC.Checked ? '1' : '0';
        objPO.RELIES = chkRelies.Checked ? '1' : '0';
        objPO.SIGN = rdDirector.Checked ? rdDirector.Text : rdforDirector.Text;
        objPO.ED = chkEDexempt.Checked ? '1' : '0';
        objPO.CST = chkCSTExempt.Checked ? '1' : '0';
        objPO.FLAG = '0';
        objPO.FLAGINV = '0';
        objPO.FINAL = '0';
        objPO.PENDING = chkPending.Checked ? '1' : '0';
        objPO.DEL = chkdelete.Checked ? '1' : '0';
        objPO.PINO = 0;
        objPO.FOOTER1 = txtFooter.Text;
        // objPO.ISTYPE = 'T';
        if (radSelType.SelectedValue == "2")
        {
            objPO.ISTYPE = 'D';
        }
        else
        {
            objPO.ISTYPE = 'P';
        }
        objPO.BANKGTY = chkBankGty.Checked ? '1' : '0';
        objPO.AMOUNT = Convert.ToDouble(txtAmount.Text);
        objPO.BANK_REMARK = txtBankRemark.Text;

        objPO.HANDLING_CHARG = txtHandlingCharges.Text == "" ? 0.0 : Convert.ToDouble(txtHandlingCharges.Text);
        objPO.TRANSPORT_CHARG = txtTransportCharges.Text == "" ? 0.0 : Convert.ToDouble(txtTransportCharges.Text);
        objPO.INSURED = Convert.ToInt32(rdlInsured.SelectedValue);
        objPO.DELIVERY_AT = txtDeliveryAt.Text;
        objPO.DELIVERY_SCHEDULE = txtDeliverySchedule.Text;
        objPO.MODE_OF_DESPATCH = txtModeofDespatch.Text;
        int i = objstrPO.UpdateDPOPO(objPO, Session["colcode"].ToString());
        return i;
        // DisplayMessage("Purchase Order Save Successfuly");
    }

    protected void lstQtNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVendorByQuotation(lstQtNo.SelectedValue);
    }

    protected void lstVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstVendor.SelectedValue != "" || lstVendor.SelectedValue != "-1")
        {
            BindItemsRecommandToVendor(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
            //BindFieldsRecommandToVendor(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
            this.GeneratePorderNo();
            grdPOitems.Visible = true;
        }

    }

    protected void ddltendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemsRecommandForTender(Convert.ToInt32(ddltendor.SelectedValue));
        BindVendorForTender(Convert.ToInt32(ddltendor.SelectedValue));
        //BindFieldsToTender(Convert.ToInt32(ddltendor.SelectedValue));
        this.GeneratePorderNo();
    }

    protected void ddlInd_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindItemsForDPO(Convert.ToInt32(ddlInd.SelectedValue));

    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objstrPO.GetVendorDetails(Convert.ToInt32(ddlVendor.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblAdd.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();   //25//02/2022
            lblcontact.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();  //25//02/2022
            // lblVaddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();            //25//02/2022
            //lblVcontact.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();             //25//02/2022
            this.GeneratePorderNo();
        }
    }

    protected void lstPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objstrPO.GetSinglePONO(Convert.ToInt32(lstPO.SelectedValue));
        if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() != "D")
        {
            //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Str_Purchase_order_Report.rpt&param=@P_PORDNO=" + Convert.ToInt32(lstPO.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
        }
        else
        {
            //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "str_porder_dpurchase.rpt&param=@P_PORDNO=" + Convert.ToInt32(lstPO.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (radSelType.SelectedValue == "2")                                                              //Gayatri rode 24-02-2022
            {
                foreach (ListViewItem itemRow in lvItem.Items)
                {
                    TextBox lblRate = itemRow.FindControl("lblRate") as TextBox;
                    TextBox lblBillAmt = itemRow.FindControl("lblBillAmt") as TextBox;
                    Label lblItemName = itemRow.FindControl("lblItemName") as Label;

                    if (lblRate.Text == "" || Convert.ToDouble(lblRate.Text) < 1)
                    {
                        DisplayMessage("Please Enter Valid Rate For Item  :  " + lblItemName.Text);
                        //DisplayMessage("Please Enter Valid Rate For Item :");
                        return;
                    }
                    if (lblBillAmt.Text == "" || Convert.ToDouble(lblBillAmt.Text) < 1)
                    {
                        DisplayMessage("Bill Amount Should Be Greater Than Zero For Item : " + lblItemName.Text);
                        // DisplayMessage("Bill Amount Should Be Greater Than Zero For Item : " );
                        return;
                    }
                }

            } 




            if (lstVendor.SelectedIndex >= 0 || ddltendor.SelectedIndex > 0 || ddlInd.SelectedIndex > 0)
            {
                if (ViewState["action"].Equals("add"))
                {
                    if (radSelType.SelectedValue == "0")
                    {
                        if (lstQtNo.SelectedIndex == 0)
                        {
                            DisplayMessage("Please Select Quotation From List");
                            return;
                        }
                        if (lstVendor.SelectedIndex == 0)
                        {
                            DisplayMessage("Please Select Vendor From List");
                            return;
                        }
                        if (txtSdate.Text == "" || txtSdate.Text == string.Empty)
                        {
                            DisplayMessage("Please Enter Supply Before Date.");
                            return;
                        }
                        if (txtDtSend.Text == "" || txtDtSend.Text == string.Empty)
                        {
                            DisplayMessage("Please Enter Date Of Sending.");
                            return;
                        }
                        if (Convert.ToDateTime(txtSdate.Text) < Convert.ToDateTime(txtDtSend.Text))
                        {
                            DisplayMessage("Supply Before Date Should Be Greater Than Or Equal To Date Of Sending.");
                            return;
                        }
                        CustomStatus cs = (CustomStatus)SavePO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            DisplayMessage("PO Saved Successfully");
                            Clear();
                            BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            //BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            //BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                    else if (radSelType.SelectedValue == "1")
                    {
                        if (ddltendor.SelectedIndex == 0)
                        {
                            DisplayMessage("Please Select Tender From List.");
                            return;
                        }
                        if (txtSdateTender.Text == "" || txtSdateTender.Text == string.Empty)
                        {
                            DisplayMessage("Please Enter Supply Before Date.");
                            return;
                        }
                        if (txtDtsendTender.Text == "" || txtDtsendTender.Text == string.Empty)
                        {
                            DisplayMessage("Please Enter Date Of Sending.");
                            return;
                        }
                        if (Convert.ToDateTime(txtSdateTender.Text) < Convert.ToDateTime(txtDtsendTender.Text))
                        {
                            DisplayMessage("Supply Before Date Should Be Greater Than Or Equal To Date Of Sending.");
                            return;
                        }
                        CustomStatus cs = (CustomStatus)SaveTenderPO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            DisplayMessage("PO Saved Successfully");
                            Clear();
                            clearTenderControl();
                            BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            //BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                    else if (radSelType.SelectedValue == "4")
                    {
                        CustomStatus cs = (CustomStatus)SaveTenderPO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            DisplayMessage("PO Saved Successfully");
                            Clear();
                            clearTenderControl();
                            //BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                    else
                    {
                        if (ddlInd.SelectedIndex == 0)
                        {
                            DisplayMessage("Please Select Indent Number");
                            return;
                        }
                        if (ddlVendor.SelectedIndex == 0)
                        {
                            DisplayMessage("Please Select Vendor Name");
                            return;
                        }

                        if (txtSdateDPO.Text == "")
                        {
                            DisplayMessage("Please Enter Supply Before Date.");
                            return;
                        }
                        if (txtDtSendDPO.Text == "")
                        {
                            DisplayMessage("Please Enter Date Of Sending.");
                            return;
                        }
                        if (Convert.ToDateTime(txtSdateDPO.Text) < Convert.ToDateTime(txtDtSendDPO.Text))
                        {
                            DisplayMessage("Supply Before Date Should Be Greater Than Or Equal To Date Of Sending.");
                            return;
                        }

                        if (lvItem.Items.Count > 0)
                        {

                        }
                        else
                        {
                            DisplayMessage("Please Enter Item Details.");
                            return;
                        }


                        CustomStatus cs = (CustomStatus)SaveDPOPO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            //if (txtSdateDPO.Text == string.Empty)
                            DisplayMessage("PO Saved Successfully");
                            Clear();
                            clearDPOControl();
                            BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                }
                else
                {
                    if (radSelType.SelectedValue == "0")
                    {
                        CustomStatus cs = (CustomStatus)UpdatePO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            DisplayMessage("PO Updated Successfully");
                            Clear();
                            BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            //BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                    else if (radSelType.SelectedValue == "1")
                    {
                        CustomStatus cs = (CustomStatus)UpdateTenderPO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            DisplayMessage("PO Updated Successfully");
                            Clear();
                            BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                    else if (radSelType.SelectedValue == "4")
                    {
                        CustomStatus cs = (CustomStatus)UpdateTenderPO(Session["colcode"].ToString());
                        if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved)
                        {
                            DisplayMessage("PO Updated Successfully");
                            Clear();
                            BindPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            //BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                            // BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
                        }
                    }
                }

                BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));    //16/05/2022
            }
            else
            {
                if (radSelType.SelectedValue == "0")
                {
                    DisplayMessage("Select Proper Vendor");
                    //Tabs.ActiveTabIndex = 0;
                }
                else if (radSelType.SelectedValue == "1")
                {
                    DisplayMessage("Please Select Tendor First");
                    //Tabs.ActiveTabIndex = 0;
                }
                else if (radSelType.SelectedValue == "2")
                {
                    DisplayMessage("Please Select Indent Number");
                    return;
                }
            }

         //   BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));    16/05/2022
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "IITMS.NITPRM.PRESENTATIONLAYER->btnSave.click()" + ex.Message);
        }
    }

    protected void btnRpt_Click(object sender, EventArgs e)
    {
        if (lstPO.SelectedValue == "")
        {
            DisplayMessage("Please Select Purchase Order");
        }
        else
        {
            DataSet ds = objstrPO.GetSinglePONO(Convert.ToInt32(lstPO.SelectedValue));
            if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() != "D")
            {
                ShowReport("PO_REPORT", "Str_Purchase_order_Report.rpt");
                //ShowReport("PO_REPORT", "Str_Purchase_order_Report_New.rpt");
            }
            else
            {
                ShowReport("PO_REPORT", "str_porder_dpurchase.rpt");

            }
        }
    }

    protected void btnQuoRept_Click(object sender, EventArgs e)
    {
        if (lstPO.SelectedValue == "")
        {
            DisplayMessage("Please Select Atleast one Purchase Order");
        }
        else
        {
            DataSet ds = objstrPO.GetSinglePONO(Convert.ToInt32(lstPO.SelectedValue));
            if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() == "D")
            {
                ShowReport("PO_REPORT", "str_porder_dpurchase.rpt");


            }
            else if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() == "P")
            {
                ShowReport("PO_REPORT", "str_porder_dpurchase.rpt");
            }
            else
            {
                //ShowReport("PO_REPORT", "Str_Purchase_order_Report.rpt");
                //ShowReport("PO_REPORT", "Str_Purchase_order_Report_New.rpt");
                ShowReport("PO_REPORT", "Str_Purchase_order_Report_Latest.rpt");
            }
        }

    }

    //To Show Purchase Order report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("STORES")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_PORDNO=" + Convert.ToInt32(lstPO.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;

        try
        {
            int PO_Approval = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "PO_APPROVAL", ""));
            if (lstPOForLock.SelectedValue == "")
            {
                DisplayMessage("Please Select Atleast one Purchase Order.");
            }
            else if (PO_Approval == 0)
            {
                ViewState["APP_PNO"] = Convert.ToInt32(lstPOForLock.SelectedValue);
                CustomStatus cs = (CustomStatus)objstrPO.UpdatePOLock(Convert.ToInt32(lstPOForLock.SelectedValue));
                //if (cs == CustomStatus.RecordUpdated || cs == CustomStatus.RecordSaved )
                //{
                DisplayMessage("Purchase Order Locked Successfully.");
                //}
                BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));

            }
            CustomStatus CS = (CustomStatus)objstrPO.DeleteExistingEntry(Convert.ToInt32(lstPOForLock.SelectedValue));
            grdAppQuot.DataSource = null;
            grdAppQuot.DataBind();
            ViewState["APP_PNO"] = Convert.ToInt32(lstPOForLock.SelectedValue);
            int SerialNo = Convert.ToInt32(objCommon.LookUp("STORE_PURCHASE_ORDER_APPROVAL", "ISNULL(MAX(SR_NO),0)+1", "PNO = " + Convert.ToInt32(ViewState["APP_PNO"].ToString())));
            txtAppSrno.Text = SerialNo.ToString();
            //FillPurchaseApproval();
            objCommon.FillDropDownList(ddlAppUser, "user_acc a inner join store_passing_authority b on (a.UA_NO = b.UA_NO) ", "a.UA_NO", "a.UA_FULLNAME", "", "");
            int PoApproval = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "PO_APPROVAL", ""));
            if (PoApproval == 1)
            {
                this.MdlPOPopup.Show();
                divPOPopup.Visible = true;
                divOthPopup.Visible = false;
                divTaxPopup.Visible = false;
            }
            else
            {
                this.MdlPOPopup.Hide();
            }

        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "Str_purchase_Order_Preparation-->btnLock()->" + ex.Message);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "edit";
        ShowDetailPO(Convert.ToInt32(lstPOForLock.SelectedValue));
        // BindFieldsRecommandToVendor(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
        //Tabs.ActiveTabIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MdlPOPopup.Hide();
        Clear();
        this.MdlPOPopup.Hide();
    }

    protected void btnShowPanel_Click(object sender, EventArgs e)
    {
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;
        ViewState["TaxTable"] = null;
        ViewState["ItemNo"] = null;
        ViewState["OthInfo"] = null;

        if (radSelType.SelectedValue == "0")
        {
            id1.Visible = true;
            id4.Visible = true;
            id5.Visible = true;
            //id6.Visible = true;
            //id7.Visible = false;
            id8.Visible = true;
            id2.Visible = false;
            id3.Visible = false;

        }
        if (radSelType.SelectedValue == "1")
        {
            id1.Visible = true;
            id2.Visible = true;
            id5.Visible = true;
            //id6.Visible = true;
            //id7.Visible = false;
            id8.Visible = true;
            id4.Visible = false;
            id3.Visible = false;
        }
        if (radSelType.SelectedValue == "2")
        {
            id1.Visible = true;
            id2.Visible = false;
            id5.Visible = true;
            // id6.Visible = true;
            //id7.Visible = false;
            id8.Visible = true;
            id4.Visible = false;
            id3.Visible = true;
        }
        if (radSelType.SelectedValue == "3")
        {
            id1.Visible = true;
            id2.Visible = false;
            id5.Visible = true;
            //id6.Visible = true;
            //id7.Visible = false;
            id8.Visible = true;
            id4.Visible = false;
            id3.Visible = true;
        }
        if (radSelType.SelectedValue == "4")
        {
            id1.Visible = true;
            id2.Visible = true;
            id5.Visible = true;
            //id6.Visible = true;
            //id7.Visible = false;
            id8.Visible = true;
            id4.Visible = false;
            id3.Visible = false;
        }
        Clear();
        clearTenderControl();
        clearDPOControl();
        //showpanel(5); //to use default panel show
        //showpanel(Convert.ToInt32(radSelType.SelectedValue));
    }

 
    void ShowMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
    }

    protected void btnAppCancel_Click(object sender, EventArgs e)
    {
        //txtAppSrno.Text = string.Empty;
        ddlAppUser.SelectedValue = "0";
        objCommon.FillDropDownList(ddlAppUser, "user_acc a inner join store_passing_authority b on (a.UA_NO = b.UA_NO) ", "a.UA_NO", "a.UA_FULLNAME", "", "");
        MdlPOPopup.Show();
        divPOPopup.Visible = true;
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
    }

    protected void btnCreateApp_Click(object sender, EventArgs e)
    {
        int ret = 0;
        DataSet MS = new DataSet();
        string strApprovalName = string.Empty;
        DataSet ds = objstrPO.GetApprovalInfo(Convert.ToInt32(ViewState["APP_PNO"]));
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (strApprovalName != "")
            {
                strApprovalName = strApprovalName + " /";
            }
            strApprovalName = strApprovalName + dr["UA_FULLNAME"];
        }
        if (strApprovalName != "")
        {
            //Response.Write("<script>alert('Authority Created and Purchase Order Locked Successfully');</script>");            
            ret = objstrPO.UpdatePOApprovalInfo(Convert.ToInt32(ViewState["APP_PNO"]), strApprovalName); //temp comment's
            BindUnLockPOByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));
            ShowMessage("Authority Created and Purchase Order Locked Successfully");
            BindPOTrackByDept(Convert.ToInt32(Session["strdeptcode"].ToString()));

            //GetConfiguration();
            //if (objConfigEnty.SendSms == true)
            //{
            //    MS = objstrPO.POPPATHEmailSMSINFO(Convert.ToInt32(ViewState["APP_PNO"]));
            //    if (MS.Tables[0].Rows.Count > 0)
            //    {
            //        //
            //        if (MS.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
            //            SendSMSMsg(objConfigEnty.SMSURL, objConfigEnty.SMSUserName, objConfigEnty.SMSPassword, MS.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear Sir, This Purchase Number " + MS.Tables[0].Rows[0]["REFNO"] + "  is waiting for your Approval,Kindly Log In and Approve the same.");
            //    }
            //}

            //if (objConfigEnty.SendEmail == true)
            //{
            //    MS = objstrPO.POPPATHEmailSMSINFO(Convert.ToInt32(ViewState["APP_PNO"]));
            //    if (MS.Tables[0].Rows.Count > 0)
            //    {
            //        if (MS.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
            //            SendEmailMsg(MS.Tables[0].Rows[0]["UA_EMAIL"].ToString(), "Mail About Purchase Approval", "This Purchase Number " + MS.Tables[0].Rows[0]["REFNO"] + " " + Environment.NewLine + " is waiting for your Approval,Kindly Log In and Approve the same.");
            //    }
            //}

            ViewState["APP_PNO"] = null;
            grdAppQuot.DataSource = null;
            grdAppQuot.DataBind();
            this.MdlPOPopup.Hide();

        }
        else
        {
            ShowMessage("Please Add Purchase Authority");
            this.MdlPOPopup.Show();
            divPOPopup.Visible = true;
            divOthPopup.Visible = false;
            divTaxPopup.Visible = false;
        }
    }

    private void FillPurchaseApproval()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("STORE_PURCHASE_ORDER_APPROVAL a inner join user_acc b on (a.UA_NO=b.UA_NO)", "a.APP_NO,a.SR_NO", "a.UA_NO,b.UA_FULLNAME", "a.PNO=" + ViewState["APP_PNO"].ToString(), "a.SR_NO");
            grdAppQuot.DataSource = ds;
            grdAppQuot.DataBind();
            ds.Dispose();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAppSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            if (ViewState["@P_APP_NO"] == null)
            {
                ret = objstrPO.AddUpdatePOApprovalAuthority(0, Convert.ToInt32(ViewState["APP_PNO"]), Convert.ToInt32(txtAppSrno.Text), Convert.ToInt32(ddlAppUser.SelectedValue), Convert.ToInt32(Session["colcode"].ToString()));
                ddlAppUser.SelectedValue = "0";
                ViewState["@P_APP_NO"] = null;
                MdlPOPopup.Show();
                divPOPopup.Visible = true;
                divOthPopup.Visible = false;
                divTaxPopup.Visible = false;
            }
            else
            {
                ret = objstrPO.AddUpdatePOApprovalAuthority(Convert.ToInt32(ViewState["@P_APP_NO"]), Convert.ToInt32(ViewState["APP_PNO"]), Convert.ToInt32(txtAppSrno.Text), Convert.ToInt32(ddlAppUser.SelectedValue), Convert.ToInt32(Session["colcode"].ToString()));
                ddlAppUser.SelectedValue = "0";
                ViewState["@P_APP_NO"] = null;
                MdlPOPopup.Show();
                divPOPopup.Visible = true;
                divOthPopup.Visible = false;
                divTaxPopup.Visible = false;
            }

            if (ret == 1)
            {
                ShowMessage("Record Saved Successfully");
                int SerialNo = Convert.ToInt32(objCommon.LookUp("STORE_PURCHASE_ORDER_APPROVAL", "ISNULL(MAX(SR_NO),0)+1", "PNO = " + Convert.ToInt32(ViewState["APP_PNO"].ToString())));
                txtAppSrno.Text = SerialNo.ToString();
                FillPurchaseApproval();
            }
            else if (ret == 2)
            {
                ShowMessage("Record Updated Successfully");
                int SerialNo = Convert.ToInt32(objCommon.LookUp("STORE_PURCHASE_ORDER_APPROVAL", "ISNULL(MAX(SR_NO),0)+1", "PNO = " + Convert.ToInt32(ViewState["APP_PNO"].ToString())));
                txtAppSrno.Text = SerialNo.ToString();
                FillPurchaseApproval();
                ViewState["@P_APP_NO"] = null;
            }
            else if (ret == 3)
            {
                ShowMessage("Record Already Exist");
            }
            else if (ret < 0)
            {
                ShowMessage("Transaction Failed");
            }

        }
        catch (Exception ex)
        {
            ShowMessage("Transaction Failed");
        }
    }

    protected void ImgAppEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int APP_NO = Convert.ToInt32(btnEdit.CommandArgument);
        DataSet ds = objCommon.FillDropDown("STORE_PURCHASE_ORDER_APPROVAL", "SR_NO", "UA_NO", "APP_NO=" + APP_NO, "");
        objCommon.FillDropDownList(ddlAppUser, "user_acc a inner join store_passing_authority b on (a.UA_NO = b.UA_NO) ", "a.UA_NO", "a.UA_FULLNAME", "", "");
        txtAppSrno.Text = ds.Tables[0].Rows[0]["SR_NO"].ToString();
        ddlAppUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
        ViewState["@P_APP_NO"] = APP_NO;
        MdlPOPopup.Show();
        divPOPopup.Visible = true;
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
    }

    protected void ddlPOTrack_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divPOT1.Visible = true;
            divPOT2.Visible = true;
            divPOT4.Visible = true;
            DataSet ds = null;
            ds = objstrPO.GetPOApprovalTrackDetails(Convert.ToInt32(ddlPOTrack.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {

                // lblPODate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["REQ_DATE"]).ToString("dd-MM-yyyy");
                lblReqNo.Text = ds.Tables[0].Rows[0]["REQ_NO"].ToString();
                lblQuotNo.Text = ds.Tables[0].Rows[0]["QUOTNO"].ToString();
                lblSupplier.Text = ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblNetTotal.Text = ds.Tables[2].Rows[0]["NET_AMT"].ToString();


                if (ds.Tables[1].Rows.Count > 0)
                {
                    lvAuthority.DataSource = ds.Tables[1];
                    lvAuthority.DataBind();
                    pnlAuthhority.Visible = true;
                }
                else
                {
                    lvAuthority.DataSource = null;
                    lvAuthority.DataBind();
                    pnlAuthhority.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Transaction Failed");
        }

    }

    // it is used to get PO report in SVCE format.
    protected void btnPOReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstPO.SelectedValue == "")
            {
                DisplayMessage("Please Select Atleast One Purchase Order");
            }
            else
            {
                DataSet ds = objstrPO.GetSinglePONO(Convert.ToInt32(lstPO.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowPOSvceReport("Purchase Order", "Str_PO_Report_SVCE.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Transaction Failed");
        }
    }

    //To Show Purchase Order report
    private void ShowPOSvceReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("STORES")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_PORDNO=" + Convert.ToInt32(lstPO.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    // to get PO approval report   
    protected void btnPOApprovalRpt_Click1(object sender, EventArgs e)
    {
        try
        {
            if (lstPO.SelectedValue == "")
            {
                DisplayMessage("Please Select Atleast One Purchase Order");
            }
            else
            {
                DataSet ds = objstrPO.GetSinglePONO(Convert.ToInt32(lstPO.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowPOSvceReport("Purchase Order", "Str_PO_Approval_Report.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Transaction Failed");
        }
    }



    //#region DPO Item Insert
    protected void btnAddTax_Click(object sender, EventArgs e)
    {
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;

        ViewState["ItemNo"] = null;
        ImageButton btn = sender as ImageButton;
        ViewState["ItemNo"] = Convert.ToInt32(btn.CommandArgument);

        if (lstVendor.SelectedValue != "")//For Quotation PO
        {
            if (ViewState["TaxTable"] != null)
            {
                DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
                DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
                if (foundRow.Length > 0)
                {
                    DataTable dt = foundRow.CopyToDataTable();
                    lvTax.DataSource = dt;
                    lvTax.DataBind();
                    this.MdlPOPopup.Show();
                    divOthPopup.Visible = false;
                    divTaxPopup.Visible = true;
                    divPOPopup.Visible = false;

                    if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
                        btnSaveTax.Visible = true;
                    else
                        btnSaveTax.Visible = false;

                    lvTax.Enabled = false;
                    decimal TotTaxAmt = 0;
                    foreach (ListViewItem i in lvTax.Items)
                    {
                        TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                        TotTaxAmt += Convert.ToDecimal(lblTaxAmount.Text);
                    }
                    txtTotTaxAmt.Text = TotTaxAmt.ToString("00.00");

                }
                else
                {
                    ShowMessage("No Taxes Are Applicable For This Item.");
                    return;
                }
            }
            else
            {
                ShowMessage("No Taxes Are Applicable For This Item.");
                return;
            }
        }//For DPO
        else
        {
            if (ViewState["TaxTable"] != null)
            {
                DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
                DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
                if (foundRow.Length > 0)
                {
                    DataTable dt = foundRow.CopyToDataTable();
                    lvTax.DataSource = dt;
                    lvTax.DataBind();
                    hdnListCount.Value = dt.Rows.Count.ToString();
                    this.MdlPOPopup.Show();
                    divOthPopup.Visible = false;
                    divTaxPopup.Visible = true;
                    divPOPopup.Visible = false;

                    if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
                    {
                        btnSaveTax.Visible = true;
                        lvTax.Enabled = true;
                    }
                    else
                        btnSaveTax.Visible = false;
                    lvTax.Enabled = true;
                    //ViewState["TaxEdit"]="edit";
                }
                else
                {
                    GetDefaultTaxes();
                }
            }
            else
            {
                GetDefaultTaxes();
            }
            decimal TotTaxAmt = 0;
            foreach (ListViewItem i in lvTax.Items)
            {
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                TotTaxAmt += Convert.ToDecimal(lblTaxAmount.Text);
            }
            txtTotTaxAmt.Text = TotTaxAmt.ToString("00.00");
        }



    }
    private void GetDefaultTaxes()
    {
        DataSet ds = null;
        int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO=" + ddlVendor.SelectedValue));
        int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
        if (VendorState == CollegeState)
        {
            ds = objstrPO.GetTaxesForDPO(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
        }
        else
        {
            ds = objstrPO.GetTaxesForDPO(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTax.DataSource = ds.Tables[0];
            lvTax.DataBind();
            hdnListCount.Value = ds.Tables[0].Rows.Count.ToString();
            this.MdlPOPopup.Show();
            divOthPopup.Visible = false;
            divTaxPopup.Visible = true;
            divPOPopup.Visible = false;
            lvTax.Enabled = true; //09/03/2022
            txtTotTaxAmt.Text = ds.Tables[1].Rows[0]["TOT_TAX_AMT"].ToString();
        }
        else
        {
            lvTax.DataSource = null;
            lvTax.DataBind();
            this.MdlPOPopup.Hide();
            ShowMessage("No Taxes Are Applicable For This Item.");
        }
    }
    protected void btnAddOthInfo_Click(object sender, ImageClickEventArgs e)
    {
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;

        ImageButton btn = sender as ImageButton;
        int ItemNo = Convert.ToInt32(btn.CommandArgument);

        if (lstVendor.SelectedValue != "")//For Quotation PO
        {
            DataTable dtTaxdup = (DataTable)ViewState["OthInfo"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ItemNo);

            if (foundRow.Length > 0)
            {
                DataTable newtable = foundRow.CopyToDataTable();
                txtItemRemarkOth.Text = newtable.Rows[0]["ITEM_REMARK"].ToString();
                txtQualityQtySpec.Text = newtable.Rows[0]["QUALITY_QTY_SPEC"].ToString();
                txtTechSpec.Text = newtable.Rows[0]["TECH_SPEC"].ToString();
                txtItemRemarkOth.Enabled = false;
                txtQualityQtySpec.Enabled = false;
                txtTechSpec.Enabled = false;
            }
            else
            {
                ShowMessage("No other Information Available For This Item.");
                return;
            }

        }
        else//For DPO
        {
            if (ViewState["Action"].ToString() == "edit" && hdnOthEdit.Value == "0")
            {
                DataSet ds = objCommon.FillDropDown("STORE_PARTYITEMENTRY", "ITEM_REMARK,TECH_SPEC", "QUALITY_QTY_SPEC", "INDENTNO='" + ddlInd.SelectedValue + "' AND ITEM_NO=" + ItemNo, "");
                txtItemRemarkOth.Text = ds.Tables[0].Rows[0]["ITEM_REMARK"].ToString();
                txtQualityQtySpec.Text = ds.Tables[0].Rows[0]["QUALITY_QTY_SPEC"].ToString();
                txtTechSpec.Text = ds.Tables[0].Rows[0]["TECH_SPEC"].ToString();
            }
        }
        this.MdlPOPopup.Show();
        divOthPopup.Visible = true;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;
        if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
        {
            btnSaveOthInfo.Visible = true;
            lvTax.Enabled = true;
            txtItemRemarkOth.Enabled = true;
            txtQualityQtySpec.Enabled = true;
            txtTechSpec.Enabled = true;
        }
        else
            btnSaveOthInfo.Visible = false;

    }

    protected void btnSaveTax_Click(object sender, EventArgs e)
    {
        //if (ViewState["TaxEdit"] == null)
        // {
        if (ViewState["TaxTable"] != null && ((DataTable)ViewState["TaxTable"]) != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                foreach (DataRow drow in foundRow)
                    dtTaxdup.Rows.Remove(drow);
            }
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;
                int maxVal = 0;
                DataTable dtTax = (DataTable)ViewState["TaxTable"];
                DataRow dtRow = null;
                dtRow = dtTax.NewRow();
                if (dtRow != null)
                {
                    maxVal = Convert.ToInt32(dtTax.AsEnumerable().Max(row => row["TAX_SRNO"]));
                }
                dtRow["TAX_SRNO"] = maxVal + 1;
                dtRow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                dtRow["TAXID"] = hdnTaxId.Value;
                dtRow["TAX_NAME"] = lblTaxName.Text;
                dtRow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(dtRow);
                ViewState["TaxTable"] = dtTax;
            }
        }
        else
        {
            DataTable dtTax = this.CreateTaxTable();
            DataRow dtRow = null;
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;
                dtRow = dtTax.NewRow();

                dtRow["TAX_SRNO"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtRow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                dtRow["TAXID"] = hdnTaxId.Value;
                dtRow["TAX_NAME"] = lblTaxName.Text;
                dtRow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(dtRow);
                ViewState["TaxTable"] = dtTax;
            }
        }
        // }
        // else
        // {
        // }
        txtTotTaxAmt.Text = string.Empty;
//-------------------------------------------------02/05/2022------------------------------
        //foreach (ListViewItem i in lvItem.Items)
        //{
        //    TextBox lblRate = i.FindControl("lblRate") as TextBox;
        //    TextBox lblDiscPer = i.FindControl("lblDiscPer") as TextBox;
        //    TextBox lblDiscAmt = i.FindControl("lblDiscAmt") as TextBox;
        //    lblRate.Enabled = false;
        //    lblDiscPer.Enabled = false;
        //    lblDiscAmt.Enabled = false;

        //}

        //-------------------------------------------------02/05/2022------------------------------

    }

    private DataTable CreateTaxTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("TAX_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(int)));
        dt.Columns.Add(new DataColumn("TAX_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TAX_AMOUNT", typeof(decimal)));
        return dt;
    }

    protected void btnSaveOthInfo_Click(object sender, EventArgs e)
    {
        this.MdlPOPopup.Hide();
        txtItemRemarkOth.Text = string.Empty;
        txtQualityQtySpec.Text = string.Empty;
        txtTechSpec.Text = string.Empty;

    }
    private DataTable CreateItemTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DISC_PER", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DISC_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TAXABLE_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TAX_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("BILL_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ITEM_REMARK", typeof(string)));
        dt.Columns.Add(new DataColumn("PORDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("IS_TAX", typeof(int)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        return dt;
    }
    private void AddItemTable()
    {
        dtItemTable = this.CreateItemTable();

        datarow = null;
        foreach (ListViewItem i in lvItem.Items)
        {
            HiddenField hdnItemSrNo = i.FindControl("hdnItemSrNo") as HiddenField;
            HiddenField hdnItemno = i.FindControl("hdnItemno") as HiddenField;
            Label lblItemName = i.FindControl("lblItemName") as Label;
            Label lblItemQty = i.FindControl("lblItemQty") as Label;
            TextBox lblRate = i.FindControl("lblRate") as TextBox;
            TextBox lblDiscPer = i.FindControl("lblDiscPer") as TextBox;
            TextBox lblDiscAmt = i.FindControl("lblDiscAmt") as TextBox;
            TextBox lblTaxableAmt = i.FindControl("lblTaxableAmt") as TextBox;
            TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
            TextBox lblBillAmt = i.FindControl("lblBillAmt") as TextBox;
            HiddenField hdnPordno = i.FindControl("hdnPordno") as HiddenField;
            HiddenField hdnIsTax = i.FindControl("hdnIsTax") as HiddenField;
            HiddenField hdnTechSpec = i.FindControl("hdnTechSpec") as HiddenField;
            HiddenField hdnQualityQtySpec = i.FindControl("hdnQualityQtySpec") as HiddenField;
            HiddenField hdnOthItemRemark = i.FindControl("hdnOthItemRemark") as HiddenField;

            datarow = dtItemTable.NewRow();
            datarow["ITEM_SRNO"] = hdnItemSrNo.Value;
            datarow["ITEM_NO"] = hdnItemno.Value;
            datarow["ITEM_NAME"] = lblItemName.Text;
            datarow["QTY"] = lblItemQty.Text == "" ? "0" : lblItemQty.Text;
            datarow["RATE"] = lblRate.Text == "" ? "0" : lblRate.Text;
            datarow["DISC_PER"] = lblDiscPer.Text == "" ? "0" : lblDiscPer.Text;
            datarow["DISC_AMT"] = lblDiscAmt.Text == "" ? "0" : lblDiscAmt.Text;
            datarow["TAXABLE_AMT"] = lblTaxableAmt.Text == "" ? "0" : lblTaxableAmt.Text;
            datarow["TAX_AMT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
            datarow["BILL_AMT"] = lblBillAmt.Text == "" ? "0" : lblBillAmt.Text;
            datarow["ITEM_REMARK"] = hdnOthItemRemark.Value;
            datarow["PORDNO"] = hdnPordno.Value;
            datarow["IS_TAX"] = hdnIsTax.Value;
            datarow["TECH_SPEC"] = hdnTechSpec.Value;
            datarow["QUALITY_QTY_SPEC"] = hdnOthItemRemark.Value;

            dtItemTable.Rows.Add(datarow);

        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.GetEditableDatarowFromTOG -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    private void CalItemCount()
    {
        double ItemQtyCount = 0.0;
        for (int i = 0; i < dtItemTable.Rows.Count; i++)
        {
            ItemQtyCount += Convert.ToDouble(dtItemTable.Rows[i]["QTY"].ToString());
        }
        //divItemCount.Visible = true;
        lblItemCount.Text = dtItemTable.Rows.Count.ToString();
        lblItemQtyCount.Text = ItemQtyCount.ToString();
        hdnrowcount.Value = dtItemTable.Rows.Count.ToString();
    }
    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            //if (Session["dtItem"] != null && ((DataTable)Session["dtItem"]) != null)
            //{
            AddItemTable();
            dtItemTable.Rows.Remove(this.GetEditableDatarow(dtItemTable, btnDelete.CommandArgument));
            for (int i = 0; i < dtItemTable.Rows.Count; i++)
            {
                dtItemTable.Rows[i]["ITEM_SRNO"] = i + 1;
            }
            Session["dtItem"] = dtItemTable;
            lvItem.DataSource = dtItemTable;
            lvItem.DataBind();
            lvItem.Visible = true;
            CalItemCount();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnDeleteRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //#endregion


    #region Tender

    protected void btnAddTenderTax_Click(object sender, ImageClickEventArgs e)
    {
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;

        lvTax.Enabled = false;
        ViewState["ItemNo"] = null;
        ImageButton btn = sender as ImageButton;
        ViewState["ItemNo"] = Convert.ToInt32(btn.CommandArgument);

        if (ViewState["TaxTable"] != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                DataTable dt = foundRow.CopyToDataTable();
                lvTax.DataSource = dt;
                lvTax.DataBind();
                hdnListCount.Value = dt.Rows.Count.ToString();
                this.MdlPOPopup.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;

                if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
                    btnSaveTax.Visible = true;
                else
                    btnSaveTax.Visible = false;
                //ViewState["TaxEdit"]="edit";
                CalTotTax();
            }
            else
            {
                BindTaxesForTender();
            }

        }
        else
        {
            DisplayMessage("No Taxes Are Added For This Item.");
            return;
            //BindTaxesForTender();
        }

    }

    private void BindTaxesForTender()
    {
        DataSet ds = null;
        int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO IN (SELECT PNO FROM STORE_TENDER_PARTY WHERE TVNO=" + hdnTvno.Value + ")"));
        int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
        if (VendorState == CollegeState)
        {
            ds = objstrPO.GetTaxesForTender(ddltendor.SelectedValue, Convert.ToInt32(hdnTvno.Value), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
        }
        else
        {
            ds = objstrPO.GetTaxesForTender(ddltendor.SelectedValue, Convert.ToInt32(hdnTvno.Value), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
        }
        if (ViewState["TaxTable"] != null)
        {
            DataTable dtTaxAdd = (DataTable)ViewState["TaxTable"];
            if (ds.Tables[0].Rows.Count > 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    int maxVal = 0;

                    DataRow datarow = null;
                    datarow = dtTaxAdd.NewRow();
                    if (datarow != null)
                    {
                        maxVal = Convert.ToInt32(dtTaxAdd.AsEnumerable().Max(row => row["TAX_SRNO"]));
                    }
                    datarow["TAX_SRNO"] = ds.Tables[0].Rows[i]["TAX_SRNO"].ToString();//maxVal + 1;
                    datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                    datarow["TAXID"] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                    datarow["TAX_NAME"] = ds.Tables[0].Rows[i]["TAX_NAME"].ToString();
                    datarow["TAX_AMOUNT"] = ds.Tables[0].Rows[i]["TAX_AMOUNT"].ToString();
                    ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                    dtTaxAdd.Rows.Add(datarow);
                }
                ViewState["TaxTable"] = dtTaxAdd;
                DataRow[] foundRow = dtTaxAdd.Select("ITEM_NO=" + ViewState["ItemNo"]);
                if (foundRow.Length > 0)
                {
                    DataTable dtTax = foundRow.CopyToDataTable();
                    lvTax.DataSource = dtTax;
                    lvTax.DataBind();
                    hdnListCount.Value = dtTax.Rows.Count.ToString();
                    ViewState["action"] = "edit";
                    hdnOthEdit.Value = "1";

                    this.MdlPOPopup.Show();
                    divOthPopup.Visible = false;
                    divTaxPopup.Visible = true;
                    if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
                        btnSaveTax.Visible = true;
                    else
                        btnSaveTax.Visible = false;
                }
            }
            else
            {
                lvTax.DataSource = null;
                lvTax.DataBind();
                this.MdlPOPopup.Hide();
                DisplayMessage("No Taxes Are Applicable For This Item.");
                return;
            }
            // AddTaxTable(dtTaxdup);

        }
        else if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtTax = (DataTable)ds.Tables[0];
            DataRow[] foundRow = dtTax.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                DataTable dtTaxAdd = foundRow.CopyToDataTable();
                lvTax.DataSource = dtTaxAdd;
                lvTax.DataBind();
                hdnListCount.Value = dtTaxAdd.Rows.Count.ToString();
                ViewState["action"] = "edit";
                hdnOthEdit.Value = "1";
                this.MdlPOPopup.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
                    btnSaveTax.Visible = true;
                else
                    btnSaveTax.Visible = false;
                CalTotTax();
            }
        }
        else
        {
            lvTax.DataSource = null;
            lvTax.DataBind();
            this.MdlPOPopup.Hide();
            DisplayMessage("No Taxes Are Applicable For This Item.");
            return;
        }

    }

    private void CalTotTax()
    {
        decimal TotTaxAmt = 0;
        foreach (ListViewItem i in lvTax.Items)
        {
            TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
            TotTaxAmt += Convert.ToDecimal(lblTaxAmount.Text);
        }
        txtTotTaxAmt.Text = TotTaxAmt.ToString("00.00");
    }

    protected void btnAddTenderOthInfo_Click(object sender, ImageClickEventArgs e)
    {
        divOthPopup.Visible = false;
        divTaxPopup.Visible = false;
        divPOPopup.Visible = false;

        txtItemRemarkOth.Enabled = false;
        txtQualityQtySpec.Enabled = false;
        txtTechSpec.Enabled = false;

        this.MdlPOPopup.Show();
        divOthPopup.Visible = true;
        divTaxPopup.Visible = false;
        //if (ViewState["action"].ToString() == "edit" && hdnOthEdit.Value == "1")
        //{
        if (radSelType.SelectedValue == "2" || radSelType.SelectedValue == "2")
            btnSaveOthInfo.Visible = true;
        else
            btnSaveOthInfo.Visible = false;
        ImageButton btn = sender as ImageButton;
        int ItemNo = Convert.ToInt32(btn.CommandArgument);
        string Tenderno = objCommon.LookUp("STORE_TENDER", "TENDERNO", "TNO=" + ddltendor.SelectedValue);
        DataSet ds = objCommon.FillDropDown("STORE_TENDER_ITEM", "ITEM_REMARK,TECH_SPEC", "QUALITY_QTY_SPEC", "TENDERNO='" + Tenderno + "' AND TVNO=" + Convert.ToInt32(hdnTvno.Value) + " AND ITEM_NO=" + ItemNo, "");
        if (ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            txtItemRemarkOth.Text = ds.Tables[0].Rows[0]["ITEM_REMARK"].ToString();
            txtQualityQtySpec.Text = ds.Tables[0].Rows[0]["QUALITY_QTY_SPEC"].ToString();
            txtTechSpec.Text = ds.Tables[0].Rows[0]["TECH_SPEC"].ToString();

        }
        ///}
    }

    #endregion

}
