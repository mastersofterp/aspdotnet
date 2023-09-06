//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Earn_Dedu_Rule.ASPX                                                    
// CREATION DATE : 11-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
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

public partial class PayRoll_Pay_Earn_Dedu_Rule : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();

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
            btnSave.Visible = false;
            btnBack.Visible = false;
            btnCancel.Visible = false;

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                // string s = Session["userfullname"].ToString();
                //  string c = Session["colcode"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillRule();
                FillPayHead();
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                List.Visible = true;
                BindListViewCalPay();
                ddlTo.Visible = false;
                ddlFrom.Visible = false;

                //Set Report Parameters
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Payroll" + "," + "Pay_EarningsAndDeducations.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            //string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_COLLEGENO=" + ddlCollege.SelectedValue + "," + "@P_PAYRULE=" + ddlpayruleselect.SelectedValue.ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, UpdatePanel1.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Earn_Dedu_Rule.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Earn_Dedu_Rule.aspx");
        }
    }

    private void BindListViewCalPay()
    {
        try
        {
            string payruletxt = string.Empty;
            if (ddlpayruleselect.SelectedValue == "0")
                payruletxt = "0";
            else
                payruletxt = Convert.ToString(ddlpayruleselect.SelectedItem.Text);

            int collegeNo = 0;
            collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);

            DataSet ds = objpay.GetAllCalPay(payruletxt, collegeNo);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
                dpPager.Visible = true;
            }
            lvCalPay.DataSource = ds;
            lvCalPay.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.BindListViewCalPay-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnBack.Visible = true;
        btnCancel.Visible = true;
        btnAdd.Visible = false;
        btnShowReport.Visible = false;
        if (ddlCollege.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Please select College", this.Page);
        }
        else
        {
            Clear();
            enabledisable(null);
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            List.Visible = false;
            ViewState["action"] = "add";
        }
    }

    private void Clear()
    {
        ddlPayhead.SelectedIndex = 0;
        ddlRule.SelectedIndex = 0;
        ddlFrom.SelectedIndex = 0;
        ddlTo.SelectedIndex = 0;
        txtEffectFromDate.Text = null;
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        txtmaxlimit.Text = string.Empty;
        txtminlimit.Text = string.Empty;
        txtper.Text = string.Empty;
        txtfixamount.Text = string.Empty;
        CalendarExtender1.SelectedDate = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            btnShowReport.Visible = false;
            btnAdd.Visible = false;
            btnSave.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int calno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(calno);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            List.Visible = false;
            ViewState["College_No"] = Convert.ToInt32(ddlCollege.SelectedValue);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void ShowDetails(Int32 calno)
    {

        DataSet ds = null;
        try
        {
            ds = objpay.GetCalPay(calno);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["calno"] = calno.ToString();
                string ruleName = ds.Tables[0].Rows[0]["PAYRULE"].ToString();
                Session["ruleName"] = ruleName;
                enabledisable(ds.Tables[0].Rows[0]["PAYHEAD"].ToString());
                if (ViewState["formula"] != null)
                {
                    if (ViewState["formula"].ToString() == "SCALE")
                    {
                        ddlFrom.Text = ds.Tables[0].Rows[0]["B_MIN"].ToString();
                        ddlTo.Text = ds.Tables[0].Rows[0]["B_MAX"].ToString();
                    }
                }
                else
                {
                    txtFrom.Text = ds.Tables[0].Rows[0]["B_MIN"].ToString();
                    txtTo.Text = ds.Tables[0].Rows[0]["B_MAX"].ToString();
                }
                txtper.Text = ds.Tables[0].Rows[0]["PER"].ToString();
                txtmaxlimit.Text = ds.Tables[0].Rows[0]["MAX"].ToString();
                txtminlimit.Text = ds.Tables[0].Rows[0]["MIN"].ToString();
                ddlPayhead.Text = ds.Tables[0].Rows[0]["PAYHEAD"].ToString();
                ddlRule.Text = ds.Tables[0].Rows[0]["PAYRULE"].ToString();
                txtfixamount.Text = ds.Tables[0].Rows[0]["FIX"].ToString();
                CalendarExtender1.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FDT"].ToString());
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
  {

        try
        {
            btnSave.Visible = false;
            btnBack.Visible = false;
            btnCancel.Visible = false;
            Payroll objcalpay = new Payroll();
            objcalpay.Payrule = ddlRule.SelectedValue;
            objcalpay.Payhead = ddlPayhead.SelectedValue;

            objcalpay.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);

            if (ViewState["formula"] != null)
            {
                if (ViewState["formula"].ToString() == "SCALE")
                {
                    objcalpay.Bmin = Convert.ToDouble(ddlFrom.SelectedValue);
                    objcalpay.Bmax = Convert.ToDouble(ddlTo.SelectedValue);
                }
            }
            else
            {
                objcalpay.Bmin = Convert.ToDouble(txtFrom.Text);
                objcalpay.Bmax = Convert.ToDouble(txtTo.Text);
            }
            objcalpay.Min = Convert.ToDouble(txtminlimit.Text);
            objcalpay.Max = Convert.ToDouble(txtmaxlimit.Text);
            objcalpay.Per = Convert.ToDouble(txtper.Text);
            objcalpay.Fix = Convert.ToDouble(txtfixamount.Text);
            objcalpay.Fdt = Convert.ToDateTime(txtEffectFromDate.Text);

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {


                    //Add New Help
                    CustomStatus cs = (CustomStatus)objpay.AddCalPay(objcalpay);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        pnlAdd.Visible = false;
                        List.Visible = true;
                        btnAdd.Visible = true;
                        btnBack.Visible = true;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                        //   objCommon.DisplayMessage("Record saved successfully!", this.Page);
                        Showmessage("Record saved successfully!");
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        //objCommon.DisplayMessage("Record Already Exist!", this.Page);
                        Showmessage("Record Already Exist!");
                        btnSave.Visible = true;
                        btnBack.Visible = true;
                        btnCancel.Visible = true;
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage("Record not saved!", this.Page);
                        btnSave.Visible = true;
                        btnBack.Visible = true;
                        btnCancel.Visible = true;
                        return;
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["calno"] != null)
                    {
                        objcalpay.Calno = Convert.ToInt32(ViewState["calno"].ToString());
                        CustomStatus cs = (CustomStatus)objpay.UpdateCalPay(objcalpay);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            pnlAdd.Visible = false;
                            btnAdd.Visible = true;
                            pnlList.Visible = true;
                            List.Visible = true;
                           // btnBack.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                            //objCommon.DisplayMessage("Record Updated successfully!", this.Page);
                            Showmessage("Record Updated successfully!");
                        }


                        else
                        {
                            //objCommon.DisplayMessage("Record Already Existed!", this.Page);
                            //Response.Write("Data inserted successfully");
                            Showmessage("Record Already Existed!");

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        Clear();

    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int calno = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objpay.DeleteCalPay(calno);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        enabledisable(ddlPayhead.SelectedValue);
    }

    private void enabledisable(string payhead)
    {
        try
        {
            string formula = objCommon.LookUp("PAYROLL_PAYHEAD", "FORMULA", "PAYHEAD='" + payhead + "'");

            ViewState["formula"] = formula.ToString();
            if (formula == "SCALE")
            {
                txtFrom.Visible = false;
                txtTo.Visible = false;
                ddlTo.Visible = true;
                ddlFrom.Visible = true;
                FillSCALE();
            }
            else
            {
                ViewState["formula"] = null;
                txtFrom.Visible = true;
                txtTo.Visible = true;
                txtFrom.Text = string.Empty;
                txtEffectFromDate.Visible = true;
                txtTo.Text = string.Empty;
                ddlTo.Visible = false;
                ddlFrom.Visible = false;
                ddlFrom.SelectedIndex = 0;
                ddlTo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.enabledisable-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        btnAdd.Visible = true;
        btnShowReport.Visible = true;
        List.Visible = true;
        btnSave.Visible = false;
        btnBack.Visible = false;
        btnCancel.Visible = false;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListViewCalPay();
    }

    private void FillRule()
    {
        try
        {
            objCommon.FillDropDownList(ddlRule, "PAYROLL_RULE", "PAYRULE", "PAYRULE", "", "RULENO");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillRule-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillPayHead()
    {
        try
        {
            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYFULL", "TYPE='C'", "SRNO");
            objCommon.FillDropDownList(ddlpayruleselect, "PAYROLL_RULE", "PAYRULE", "PAYRULE", "", "RULENO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillSCALE()
    {
        try
        {
            string ruleName = ddlRule.SelectedValue;
            int ruleno;
            if (ruleName == "0")
            {
                ruleName = Convert.ToString(Session["ruleName"]);
                ruleno = Convert.ToInt32(objCommon.LookUp("PAYROLL_RULE", "RULENO", "PAYRULE='" + ruleName + "'"));
            }
            else
            {
                ruleno = Convert.ToInt32(objCommon.LookUp("PAYROLL_RULE", "RULENO", "PAYRULE='" + ruleName + "'"));
            }

            objCommon.FillDropDownList(ddlFrom, "PAYROLL_SCALE", "SCALERANGE", "SCALE", "RULENO=" + ruleno, "SCALENO");
            objCommon.FillDropDownList(ddlTo, "PAYROLL_SCALE", "SCALERANGE", "SCALE", "RULENO=" + ruleno, "SCALENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillSCALE-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlpayruleselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewCalPay();
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpayruleselect.SelectedIndex = 0;
        BindListViewCalPay();
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {


    }
    protected void btnShowReport_Click1(object sender, EventArgs e)
    {

        ShowReport("Pay_Earn_deduction", "Pay_EarningsAndDeducations.rpt");
    }
}
