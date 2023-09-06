//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITChallanEntry.aspx                                                  
// CREATION DATE : 14-April-2011                                                      
// CREATED BY    : Ankit Agrawal                                                     
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class Pay_ITChallanEntry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    ITChallanEntry objChalan;
    string UsrStatus = string.Empty;
    string CHIDNO = string.Empty;
    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //Checking logon Status and redirection to Login Page(Default.aspx) if user is not logged in
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
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //function to fill dropdownlists
                FillDropdown();
                ddlCollege.SelectedIndex = 0;
                ddlSuplOrderNo.SelectedIndex = 0;
                ddlCollegeNo.SelectedIndex = 0;
                Clear();
                ViewState["action"] = "add";

                string dt = DateTime.Now.ToString("MMM");
                string YR = DateTime.Now.Year.ToString();
                string monyear = dt + "" + YR;
                txtMonYear.Text = monyear;
                DateTime chdate = DateTime.Now;
                txtChDate.Text = chdate.ToShortDateString();
                txtDepDate.Text = DateTime.Now.ToShortDateString();
                txtTaxDedDate.Text = DateTime.Now.ToShortDateString();
            }
        }
    }
    #endregion

    #region Actions

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITChallanEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITChallanEntry.aspx");
        }
    }

    //Retrive Data on click event of image button
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ChID = int.Parse(btnEdit.CommandArgument);
            ShowDetails(ChID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //Response.Redirect(Request.Url.ToString());
    }

    protected void chkSuppBill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSuppBill.Checked == true)
        {
            rowSupplOrder.Visible = true;
            rfvSuplOrderNo.Enabled = true;
        }
        else
        {
            rowSupplOrder.Visible = false;
            rfvSuplOrderNo.Enabled = false;
        }
    }

    //Deletes the challan entry Delete Button Click
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnDel = sender as ImageButton;
            int ChIdNo = int.Parse(btnDel.CommandArgument);
            DeleteEntry(ChIdNo);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    //Bind the ListView with Domain 
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddUpdate();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {


        GetIDNoForPrint();
        if (CHIDNO != string.Empty)
            ShowReport("Form24Q", "Pay_IT_Form24Q.rpt");



    }

    protected void txtMonYear_TextChanged(object sender, EventArgs e)
    {
        BindListView();
    }
    #endregion

    #region Methods

    //Function to fill dropdownlist for college type,stafftype
    protected void FillDropdown()
    {
        objCommon.FillDropDownList(ddlCollege, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        objCommon.FillDropDownList(ddlSuplOrderNo, "PAYROLL_SUPPLIMENTARY_BILL", "ORDNO", "ORDNO AS ORDERNO", "SUPLTRXID>0", "SUPLTRXID DESC");
        objCommon.FillDropDownList(ddlCollegeNo, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
    }



    private void ShowDetails(int ChIDNO)
    {
        DataSet ds;
        DataTableReader dtr;
        ds = objITMas.GetChallanEntry(ChIDNO, string.Empty);
        dtr = ds.CreateDataReader();
        ViewState["lblChIDNO"] = ChIDNO;
        if (dtr.Read())
        {
            txtMonYear.Text = dtr["MON"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["MON"].ToString().Trim().ToUpper();
            txtChDate.Text = dtr["CHALANDT"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["CHALANDT"].ToString().Trim().ToUpper();
            if (dtr["SUPL"].ToString().Trim().ToUpper() == "1")
            {
                chkSuppBill.Checked = true;
                rowSupplOrder.Visible = true;
                rfvSuplOrderNo.Enabled = true;
                ddlSuplOrderNo.SelectedValue = dtr["ORDNO"].ToString().Trim().ToUpper();
            }
            else
            {
                chkSuppBill.Checked = false;
                rowSupplOrder.Visible = false;
                rfvSuplOrderNo.Enabled = false;
            }
            ddlCollege.SelectedValue = dtr["STAFFNO"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["STAFFNO"].ToString().Trim().ToUpper();
            ddlCollegeNo.SelectedValue = dtr["COLLEGE_NO"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["COLLEGE_NO"].ToString().Trim().ToUpper();
            txtChallanNo.Text = dtr["CHALANNO"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["CHALANNO"].ToString().Trim().ToUpper();
            txtChequeDDNo.Text = dtr["CHQDDNO"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["CHQDDNO"].ToString().Trim().ToUpper();
            txtTaxDeposited.Text = dtr["CHAMT"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["CHAMT"].ToString().Trim().ToUpper();
            txtSurcharge.Text = dtr["CHSCHARGE"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["CHSCHARGE"].ToString().Trim().ToUpper();
            txtEducationCess.Text = dtr["CHEDUCESS"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["CHEDUCESS"].ToString().Trim().ToUpper();
            txtInterest.Text = dtr["INTEREST"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["INTEREST"].ToString().Trim().ToUpper();
            txtOthers.Text = dtr["OTHERS"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["OTHERS"].ToString().Trim().ToUpper();
            txtBSRCode.Text = dtr["BSRNO"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["BSRNO"].ToString().Trim().ToUpper();
            txtTaxDedDate.Text = dtr["SALDT"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["SALDT"].ToString().Trim().ToUpper();
            txtDepDate.Text = dtr["DEPODT"].ToString().Trim().ToUpper() == null ? string.Empty : dtr["DEPODT"].ToString().Trim().ToUpper();
        }
    }

    private void BindListView()
    {
        DataSet ds;
        if (txtMonYear.Text.Trim() != string.Empty)
        {
            ds = objITMas.GetChallanEntry(0, txtMonYear.Text.Trim());
            lvChallan.DataSource = ds;
            lvChallan.DataBind();
        }
    }

    private void DeleteEntry(int ChIdNo)
    {
        CustomStatus cs = (CustomStatus)objITMas.DeleteChalanEntry(ChIdNo);
        if (cs.Equals(CustomStatus.RecordDeleted))
        {
            //objCommon.DisplayMessage(this.updpanel, "Record Deleted Successfully!", this.Page);
            MessageBox("Record Deleted Successfully!");
            ViewState["action"] = "add";
        }

    }

    private void Clear()
    {
        txtBSRCode.Text = "";
        txtChallanNo.Text = "";
        txtChDate.Text = "";
        txtChequeDDNo.Text = "";
        txtDepDate.Text = "";
        txtEducationCess.Text = "";
        txtInterest.Text = "";
        txtMonYear.Text = "";
        txtOthers.Text = "";
        txtSurcharge.Text = "";
        txtTaxDedDate.Text = "";
        txtTaxDeposited.Text = "";
        chkSuppBill.Checked = false;
        ddlCollege.SelectedIndex = 0;
        ddlCollegeNo.SelectedIndex = 0;
        ddlSuplOrderNo.SelectedIndex = 0;
        rowSupplOrder.Visible = false;
        rfvSuplOrderNo.Enabled = false;
        //BindListView();
        ViewState["action"] = "add";
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {

        try
        {

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_Month=" + txtMonYear.Text.Trim().ToUpper() + ",@P_CHIDNO=" + CHIDNO + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void AddUpdate()
    {
        //Add

        objChalan = new ITChallanEntry();
        objChalan.BSRCODE = txtBSRCode.Text.ToString().Trim().ToUpper();
        objChalan.CHDATE = Convert.ToDateTime(txtChDate.Text.ToString().Trim().ToUpper());
        objChalan.CHNO = txtChallanNo.Text.ToString().Trim().ToUpper();
        objChalan.CHQDDNO = txtChequeDDNo.Text.ToString().Trim().ToUpper();
        objChalan.COLLEGECODE = Session["colcode"].ToString().ToUpper();

        if (!txtTaxDedDate.Text.Trim().Equals(string.Empty))
        {
            objChalan.DEDUDATE = Convert.ToDateTime(txtTaxDedDate.Text);
        }
        else
        {
            MessageBox("Please Select Tax Deduction Date");
            txtTaxDedDate.Focus();
            return;
        }



        if (!txtDepDate.Text.Trim().Equals(string.Empty))
        {
            objChalan.DEPODATE = Convert.ToDateTime(txtDepDate.Text);
        }
        else
        {
            MessageBox("Please Select Tax Deposite Date");
            txtDepDate.Focus();
            return;
        }

        if (!txtEducationCess.Text.Trim().Equals(string.Empty))
            objChalan.EDUCESS = Convert.ToDecimal(txtEducationCess.Text);
        else
            objChalan.EDUCESS = 0;
        if (!txtInterest.Text.Trim().Equals(string.Empty))
            objChalan.INTEREST = Convert.ToDecimal(txtInterest.Text.ToString().Trim().ToUpper());
        else
            objChalan.INTEREST = 0;
        objChalan.MONYEAR = txtMonYear.Text.ToString().Trim().ToUpper();
        if (!txtOthers.Text.Trim().Equals(string.Empty))
            objChalan.OTHERS = Convert.ToDecimal(txtOthers.Text);
        else
            objChalan.OTHERS = 0;
        objChalan.STAFF = ddlCollege.SelectedIndex == 0 ? string.Empty : ddlCollege.SelectedValue.ToString().Trim().ToUpper();
        objChalan.COLLEGENO = Convert.ToInt32(ddlCollegeNo.SelectedValue);
        objChalan.SUPORDERNO = ddlSuplOrderNo.SelectedIndex == 0 ? string.Empty : ddlSuplOrderNo.SelectedValue.ToString().Trim().ToUpper();
        if (!txtSurcharge.Text.Trim().Equals(string.Empty))
            objChalan.SURCHARGE = Convert.ToDecimal(txtSurcharge.Text);
        else
            objChalan.SURCHARGE = 0;
        if (!txtTaxDeposited.Text.Trim().Equals(string.Empty))
            objChalan.TAXDEPO = Convert.ToDecimal(txtTaxDeposited.Text);
        else
            objChalan.TAXDEPO = 0;
        if (ViewState["action"].ToString().Equals("add"))
        {

            objChalan.CHIDNO = 0;
            CustomStatus cs = (CustomStatus)objITMas.AddUpdateChallanEntry(objChalan);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Record Saved Successfully!", this.Page);

                this.BindListView();
                Clear();
            }

        }
        //edit
        else
        {
            if (ViewState["lblChIDNO"] != null)
            {

                objChalan.CHIDNO = Convert.ToInt32(ViewState["lblChIDNO"].ToString().Trim());
                CustomStatus cs = (CustomStatus)objITMas.AddUpdateChallanEntry(objChalan);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Record Updated Successfully!", this.Page);
                    ViewState["action"] = "add";

                    this.BindListView();
                    Clear();
                }

            }
        }
    }

    private void GetIDNoForPrint()
    {
        CHIDNO = string.Empty;
        int chkcount = 0;
        string chidno = string.Empty;
        foreach (ListViewDataItem lvItem in lvChallan.Items)
        {
            CheckBox chk = lvItem.FindControl("chkPrint") as CheckBox;
            if (chk.Checked == true)
            {
                CHIDNO = CHIDNO + chk.ToolTip.ToString().Trim() + "$";
                chkcount += 1;
                chidno = chk.ToolTip;
            }
        }
        if (chkcount == 1)
        {
            CHIDNO = chidno;
        }
        if (CHIDNO == string.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Please Select Atleast One Challan Entry');", true);
        }
    }

    //to populate message Box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    #endregion


    protected void ddlCollegeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string payhead = objCommon.LookUp("PAYROLL_PAY_REF", "IT_FIELD", "").ToString();
        string mon = txtMonYear.Text;
        int stno = Convert.ToInt32(ddlCollege.SelectedValue);
        int collegeno = Convert.ToInt32(ddlCollegeNo.SelectedValue);
        this.GetTaxDeposited(payhead, mon, stno, collegeno);
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        string payhead = objCommon.LookUp("PAYROLL_PAY_REF", "IT_FIELD", "").ToString();
        string mon = txtMonYear.Text;
        int stno = Convert.ToInt32(ddlCollege.SelectedValue);
        int collegeno = Convert.ToInt32(ddlCollegeNo.SelectedValue);
        this.GetTaxDeposited(payhead, mon, stno, collegeno);

    }


    public void GetTaxDeposited(string payhead, string mon, int stno, int collegeno)
    {
        DataSet ds;
        ds = objITMas.GetTaxDeposited(payhead, mon, stno, collegeno);
        txtTaxDeposited.Text = ds.Tables[0].Rows[0]["TAXDEPOSITED"].ToString();

    }
}
