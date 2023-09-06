using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STORES_Masters_Str_Tax_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
               this.BindListViewField();
                ViewState["action"] = "add";

            }
            //Set Report Parameters
           // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Field_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StoreMaster objStrMst = new StoreMaster();
            objStrMst.Tax_Name = txtTaxName.Text;
            objStrMst.Tax_Code = txtTaxCode.Text;

            if (txtTaxPer.Text == "")
            {
                objStrMst.Tax_Per = 0;
            }
            else {
                objStrMst.Tax_Per = Convert.ToDecimal(txtTaxPer.Text);
            }



            if (rblIsPercentage.SelectedValue == "Y")                           // Gayatri Rode 22-02-2022
            {
                objStrMst.Tax_Per = Convert.ToDecimal(txtTaxPer.Text);
            }
            else
            {
                objStrMst.Tax_Per = 0;
            }
            


            objStrMst.Tax_SerialNo = txtsrno.Text == "" ? 0 : Convert.ToInt32(txtsrno.Text);
            objStrMst.College_Code = Session["colcode"].ToString();
            if (rblIsPercentage.SelectedValue == "Y")
            {
                objStrMst.Is_Per = 1;
            }
            else {
                objStrMst.Is_Per = 0;
            }
            if (rbIsStateTax.SelectedValue == "Y")
            {
                objStrMst.Is_State_Tax = 1;
            }
            if (rbIsStateTax.SelectedValue == "N")
            {
                objStrMst.Is_State_Tax = 0;
            }
            if (chkCalOnBasicAmt.Checked)
            {
                objStrMst.Cal_Basic_Ammount = true;
            }
            else
            {
                objStrMst.Cal_Basic_Ammount = false;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objStrMst.Tax_Id = 0;
                    int duplicateCkeck = 0;
                    if (rblIsPercentage.SelectedValue == "Y")
                    {
                         duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_TAX_MASTER", "count(*)", "TAX_NAME ='" + txtTaxName.Text + "' and TAX_PER="+Convert.ToDecimal(txtTaxPer.Text)+" and TAX_code='"+txtTaxCode.Text+"'"));
                    }
                    else
                    {
                        duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_TAX_MASTER", "count(*)", "TAX_NAME ='" + txtTaxName.Text + "' and TAX_code='" + txtTaxCode.Text + "' and IS_STATE_TAX ='1'")); 
                    }
                 
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddUpdTaxMasterField(objStrMst);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            objCommon.DisplayMessage(updpnlMain, "Record Saved Successfully", this);
                             this.BindListViewField();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                    }
                }

                else
                {
                    if (ViewState["TaxNo"] != null)
                    {
                        objStrMst.Tax_Id = Convert.ToInt32(ViewState["TaxNo"].ToString());
                        //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_TAX_MASTER", "count(*)", "TAX_NAME='" + txtTaxName.Text + "' and IS_STATE_TAX ='1' and TAXID !=" + ViewState["TaxNo"]));
                        int duplicateCkeck = 0;
                        if (rblIsPercentage.SelectedValue == "Y")
                        {
                            duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_TAX_MASTER", "count(*)", "TAX_NAME ='" + txtTaxName.Text + "' and TAX_PER=" + Convert.ToDecimal(txtTaxPer.Text) + " and TAX_code='"+txtTaxCode.Text+"'"));
                        }
                        else
                        {
                            duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_TAX_MASTER", "count(*)", "TAX_NAME ='" + txtTaxName.Text + "' and IS_PER =0"));
                        }
                        if (duplicateCkeck == 0)
                        {

                          
                            CustomStatus csupd = (CustomStatus)objStrMaster.AddUpdTaxMasterField(objStrMst);
                            if (csupd.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                objCommon.DisplayMessage(updpnlMain, "Record Updated Successfully", this);
                                 this.BindListViewField();
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                        }
                    }
                }
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation"))
            {
                objCommon.DisplayMessage(updpnlMain, "Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
        Clear();
       
    }

    public void Clear()
    {
        txtTaxName.Text = string.Empty;
        txtTaxCode.Text = string.Empty;
        txtsrno.Text = string.Empty;
        txtTaxPer.Text = string.Empty;
        rblIsPercentage.SelectedValue = "N";
        rbIsStateTax.SelectedValue="Y";
        chkCalOnBasicAmt.Checked = false;
        TaxPer.Visible = false;
       
        ViewState["action"] = "add";
    }
    protected void rblIsPercentage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblIsPercentage.SelectedValue == "Y")
        {
            TaxPer.Visible = true;
        }
        if (rblIsPercentage.SelectedValue == "N")
        {
            TaxPer.Visible = false;
        }
    }

    private void BindListViewField()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("STORE_TAX_MASTER", "TAXID,TAX_CODE,TAX_NAME,TAX_PER", " (case IS_STATE_TAX When 1 then 'Yes' else 'No' End) as IS_STATE_TAX,(case CAL_ON_BASIC When 1 then 'Yes' else 'No' End) as CAL_ON_BASIC", "", "TAXID DESC");
           // if (ds.Tables[0].Rows[0]["TAX_NAME"].ToString())
            lvField.DataSource = ds;
            lvField.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Tax_Master.BindListViewField-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewField();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["TaxNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            
            ShowEditDetailsField(Convert.ToInt32(ViewState["TaxNo"].ToString()));

          

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsField(int taxid)
    {
        DataSet ds = null;

        try
        {
            //ds = objStrMaster.GetSingleRecordField(TaxNo);
            ds = objCommon.FillDropDown("STORE_TAX_MASTER", "TAXID,TAX_CODE,TAX_NAME,IS_PER,TAX_PER", "IS_STATE_TAX,CAL_ON_BASIC,TAX_SRNO", "TAXID=" + taxid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtTaxName.Text = ds.Tables[0].Rows[0]["TAX_NAME"].ToString();
                txtTaxCode.Text = ds.Tables[0].Rows[0]["TAX_CODE"].ToString();
           txtsrno.Text = ds.Tables[0].Rows[0]["TAX_SRNO"].ToString();
           txtTaxPer.Text = ds.Tables[0].Rows[0]["TAX_PER"].ToString();

           if (ds.Tables[0].Rows[0]["IS_PER"].ToString() == "1")
           {
               rblIsPercentage.SelectedValue = "Y";
               TaxPer.Visible = true;
           }

           if (ds.Tables[0].Rows[0]["IS_PER"].ToString() == "0")
           {
               rblIsPercentage.SelectedValue = "N";
               TaxPer.Visible = false;
           }

           if (ds.Tables[0].Rows[0]["CAL_ON_BASIC"].ToString() == "True")
                    chkCalOnBasicAmt.Checked = true;
           if (ds.Tables[0].Rows[0]["CAL_ON_BASIC"].ToString() == "False")
               chkCalOnBasicAmt.Checked = false;

           if (ds.Tables[0].Rows[0]["IS_STATE_TAX"].ToString() == "1")
               rbIsStateTax.SelectedValue = "Y";
           if (ds.Tables[0].Rows[0]["IS_STATE_TAX"].ToString() == "0")
               rbIsStateTax.SelectedValue = "N";

           

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.ShowEditDetailsField-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }
    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        ShowTaxMasterReport("TaxMasterReport", "Tax_Master_GetAllField_Report.rpt");
    }

    public void ShowTaxMasterReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("store")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
           


            //url += "&path=~,Reports,Stores," + rptFileName;

          
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
           

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ReportName=" + reportTitle + ",@P_VENDORNO=" + vendorno + ",@P_VENDORWISE=" + rblSelectAllVendor.SelectedValue + ",@P_FDATE=" + Convert.ToDateTime(Fdate).ToString("dd-MMM-yyyy") + ",@P_TDATE=" + Convert.ToDateTime(Ldate).ToString("dd-MMM-yyyy") + ",@P_MDNO=" + Session["MDNO"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Tax_Master.ShowTaxMasterReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}