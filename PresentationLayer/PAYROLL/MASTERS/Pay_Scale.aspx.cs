//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Scale.aspx                                                  
// CREATION DATE : 06-May-2009                                                        
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

public partial class PayRoll_Pay_Scale : System.Web.UI.Page
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
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        btnAdd.Visible = true;
      //  btnShowReport.Visible = true;
        btnshowScaleReport.Visible = true;
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
              //  CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Binging listview with scale details after adding and editing the record
                BindListViewScale();
                //Filling the Dropdown of Rules
                FillRule();
                FillPayHead();
                // BindListViewCalPay();
                //Disabled add panal
                pnlAdd.Visible = false;
                //Enabled listview panal
                pnlList.Visible = true;
                List.Visible = true;
                //Set Report Parameters
                objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Payroll" + "," + "Pay_Scale.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");

            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Scale.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Scale.aspx");
        }
    }

    private void BindListViewScale()
    {
        try
        {

            string payruletxt = string.Empty;
            if (ddlpayruleselect.SelectedValue == "0")
                payruletxt = "0";
            else
                payruletxt = Convert.ToString(ddlpayruleselect.SelectedItem.Text);

            DataSet ds = objpay.GetAllScale(payruletxt);
            if (ds.Tables[0].Rows.Count <= 0)
            {
              //  btnShowReport.Visible = false;
                btnshowScaleReport.Visible = true;
                dpPager.Visible = true;
            }
            else
            {

               // btnShowReport.Visible = true;
                btnshowScaleReport.Visible = true;
                dpPager.Visible = true;
            }
            lvScale.DataSource = ds;
            lvScale.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.BindListViewScale-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlpayruleselect.SelectedIndex > 0)
        {
            Clear();
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;
            btnAdd.Visible = false;
          //  btnShowReport.Visible = false;
            btnshowScaleReport.Visible = false;
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            List.Visible = false;
            ViewState["action"] = "add";
            pnlbtn.Visible = true;
            // Added on 09-01-2023
            ddlRule.SelectedIndex = ddlpayruleselect.SelectedIndex;
        }
        else
        {
            MessageBox("Select Rule ");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            //btnAdd.Visible = true;
            //btnShowReport.Visible = true;
            //btnAdd.Visible = true;
            //btnSave.Visible = false;
            //btnCancel.Visible = false;
            //btnBack.Visible = false;
            Payroll objscale = new Payroll();
            if (!(txtBasic1.Text.Equals(string.Empty))) objscale.B1 = Convert.ToInt32(txtBasic1.Text.ToString().Trim());
            if (!(txtIncrement1.Text.Equals(string.Empty))) objscale.I1 = Convert.ToInt32(txtIncrement1.Text.ToString().Trim());
            if (!(txtBasic2.Text.Equals(string.Empty))) objscale.B2 = Convert.ToInt32(txtBasic2.Text.ToString().Trim());
            if (!(txtIncrement2.Text.Equals(string.Empty))) objscale.I2 = Convert.ToInt32(txtIncrement2.Text.ToString().Trim());
            if (!(txtBasic3.Text.Equals(string.Empty))) objscale.B3 = Convert.ToInt32(txtBasic3.Text.ToString().Trim());
            if (!(txtIncrement3.Text.Equals(string.Empty))) objscale.I3 = Convert.ToInt32(txtIncrement3.Text.ToString().Trim());
            if (!(txtBasic4.Text.Equals(string.Empty))) objscale.B4 = Convert.ToInt32(txtBasic4.Text.ToString().Trim());
            if (!(txtIncrement4.Text.Equals(string.Empty))) objscale.I4 = Convert.ToInt32(txtIncrement4.Text.ToString().Trim());
            if (!(txtBasic5.Text.Equals(string.Empty))) objscale.B5 = Convert.ToInt32(txtBasic5.Text.ToString().Trim());
            if (!(txtIncrement5.Text.Equals(string.Empty))) objscale.I5 = Convert.ToInt32(txtIncrement5.Text.ToString().Trim());

            if (!(txtBasic6.Text.Equals(string.Empty))) objscale.B6 = Convert.ToInt32(txtBasic6.Text.ToString().Trim());
            if (!(txtIncrement6.Text.Equals(string.Empty))) objscale.I6 = Convert.ToInt32(txtIncrement6.Text.ToString().Trim());

            if (!(txtBasic7.Text.Equals(string.Empty))) objscale.B7 = Convert.ToInt32(txtBasic7.Text.ToString().Trim());
            if (!(txtIncrement7.Text.Equals(string.Empty))) objscale.I7 = Convert.ToInt32(txtIncrement7.Text.ToString().Trim());

            if (!(txtBasic8.Text.Equals(string.Empty))) objscale.B8 = Convert.ToInt32(txtBasic8.Text.ToString().Trim());

            if (!(txtIncrement8.Text.Equals(string.Empty))) objscale.I8 = Convert.ToInt32(txtIncrement8.Text.ToString().Trim());

            if (!(txtBasic9.Text.Equals(string.Empty))) objscale.B9 = Convert.ToInt32(txtBasic9.Text.ToString().Trim());
            if(!(txtIncrement9.Text.Equals(string.Empty))) objscale.I9=Convert.ToInt32(txtIncrement9.Text.ToString().Trim());
            if (!(txtBasic10.Text.Equals(string.Empty))) objscale.B10 = Convert.ToInt32(txtBasic10.Text.ToString().Trim());
            if (!(txtIncrement10.Text.Equals(string.Empty))) objscale.I10 = Convert.ToInt32(txtIncrement10.Text.ToString().Trim());
            if (!(txtBasic11.Text.Equals(string.Empty))) objscale.B11 = Convert.ToInt32(txtBasic11.Text.ToString().Trim());
            if (!(txtScale.Text.Equals(string.Empty))) objscale.Scale = txtScale.Text;
            if (txtscalerange.Text == "")
            {
                objCommon.DisplayUserMessage(updmain, "Please Enter Scale No !", this.Page);
                return;
            }
            else
            {
                objscale.ScaleRange = Convert.ToDouble(txtscalerange.Text);
            }
            if (!(txtGradePay.Text.Equals(string.Empty))) objscale.GradePay = Convert.ToDouble(txtGradePay.Text.ToString().Trim());
            objscale.RuleNo = Convert.ToInt32(ddlRule.SelectedValue);
            objscale.CollegeCode = Session["colcode"].ToString();
            if (txtshortScalename.Text == "")
            {
                objCommon.DisplayUserMessage(updmain, "Please Enter Scale Short Name!", this.Page);
                return;
            }
            else
            {
                objscale.PAYSHORTNAME = txtshortScalename.Text.Trim();
            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Record
                    CustomStatus cs = (CustomStatus)objpay.AddScale(objscale);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(updmain, "Record saved successfully!", this.Page);
                        pnlAdd.Visible = false;
                        btnBack.Visible = true;
                        pnlList.Visible = true;
                        List.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                    else if(cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayUserMessage(updmain, "Record Already Existed!", this.Page);
                    }

                }
                else
                {
                    //Edit Record
                    if (ViewState["scaleno"] != null)
                    {
                        objscale.ScaleNo = Convert.ToInt32(ViewState["scaleno"].ToString());
                        CustomStatus cs = (CustomStatus)objpay.UpdateScale(objscale);

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            // objCommon.DisplayMessage("Record Updated successfully!", this.Page);
                            objCommon.DisplayUserMessage(updmain, "Record Updated successfully!", this.Page);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            List.Visible = true;
                            ViewState["action"] = null;
                            ViewState["scaleno"] = null;
                            Clear();
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(updmain, "Record Already Existed!", this.Page);
                        }
                    }
                }
            }
            BindListViewScale();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
        btnAdd.Visible = false;
       // btnShowReport.Visible = false;
        btnshowScaleReport.Visible = false;
        try
        {
            pnlbtn.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int scaleno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(scaleno);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetails(int scaleno)
    {
        DataSet ds = null;
        try
        {
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            List.Visible = false;
            ds = objpay.GetScale(scaleno);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["scaleno"] = scaleno.ToString();
                txtBasic1.Text = ds.Tables[0].Rows[0]["B1"].ToString();
                txtIncrement1.Text = ds.Tables[0].Rows[0]["I1"].ToString();
                txtBasic2.Text = ds.Tables[0].Rows[0]["B2"].ToString();
                txtIncrement2.Text = ds.Tables[0].Rows[0]["I2"].ToString();
                txtBasic3.Text = ds.Tables[0].Rows[0]["B3"].ToString();
                txtIncrement3.Text = ds.Tables[0].Rows[0]["I3"].ToString();
                txtBasic4.Text = ds.Tables[0].Rows[0]["B4"].ToString();
                txtIncrement4.Text = ds.Tables[0].Rows[0]["I4"].ToString();
                txtBasic5.Text = ds.Tables[0].Rows[0]["B5"].ToString();
                txtIncrement5.Text = ds.Tables[0].Rows[0]["I5"].ToString();

                txtBasic6.Text = ds.Tables[0].Rows[0]["B6"].ToString();
                txtIncrement6.Text = ds.Tables[0].Rows[0]["I6"].ToString();

                txtBasic7.Text = ds.Tables[0].Rows[0]["B7"].ToString();
                txtIncrement7.Text = ds.Tables[0].Rows[0]["I7"].ToString();

                txtBasic8.Text = ds.Tables[0].Rows[0]["B8"].ToString();

                txtIncrement8.Text=ds.Tables[0].Rows[0]["I8"].ToString();
                txtBasic9.Text=ds.Tables[0].Rows[0]["B9"].ToString();
                txtIncrement9.Text=ds.Tables[0].Rows[0]["I9"].ToString();
                txtBasic10.Text=ds.Tables[0].Rows[0]["B10"].ToString();
                txtIncrement10.Text=ds.Tables[0].Rows[0]["I10"].ToString();
                txtBasic11.Text = ds.Tables[0].Rows[0]["B11"].ToString();
                txtGradePay.Text = ds.Tables[0].Rows[0]["GRADEPAY"].ToString();
                ddlRule.Text = ds.Tables[0].Rows[0]["RULENO"].ToString();
                txtscalerange.Text = ds.Tables[0].Rows[0]["SCALERANGE"].ToString();
                txtScale.Text = ds.Tables[0].Rows[0]["SCALE"].ToString();
                txtshortScalename.Text = ds.Tables[0].Rows[0]["ShortScaleName"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CreateUser.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
        btnAdd.Visible = false;
        //btnShowReport.Visible = false;
        btnshowScaleReport.Visible = false;
        Clear();
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Clear();
        btnAdd.Visible = true;
      //  btnShowReport.Visible = true;
        btnshowScaleReport.Visible = true;
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        List.Visible = true;
        ViewState["action"] = null;
    }

    private void Clear()
    {
        txtBasic1.Text = string.Empty;
        txtIncrement1.Text = string.Empty;
        txtBasic2.Text = string.Empty;
        txtIncrement2.Text = string.Empty;
        txtBasic3.Text = string.Empty;
        txtIncrement3.Text = string.Empty;
        txtBasic4.Text = string.Empty;
        txtIncrement4.Text = string.Empty;
        txtBasic5.Text = string.Empty;
        txtIncrement5.Text = string.Empty;
        txtBasic6.Text = string.Empty;
        txtIncrement6.Text = string.Empty;
        txtBasic7.Text = string.Empty;
        txtIncrement7.Text = string.Empty;
        txtBasic8.Text = string.Empty;
        txtGradePay.Text = string.Empty;
        ddlRule.SelectedIndex = 0;
        txtscalerange.Text = string.Empty;
        txtScale.Text = string.Empty;
        txtIncrement8.Text = string.Empty;
        txtBasic9.Text = string.Empty;
        txtIncrement9.Text = string.Empty;
        txtBasic10.Text = string.Empty;
        txtIncrement10.Text = string.Empty;
        txtBasic11.Text = string.Empty;
        txtshortScalename.Text = string.Empty;
     
    }

    private void FillRule()
    {
        try
        {

            objCommon.FillDropDownList(ddlRule, "PAYROLL_RULE", "RULENO", "PAYRULE", "ACTIVESTATUS = 1", "RULENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.FillRule-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain
        BindListViewScale();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int ITRULEID;
            string ITRULENAME;
            if(ddlpayruleselect.SelectedIndex > 0)
            {
                ITRULENAME=ddlpayruleselect.SelectedItem.Text;
            }
            else
            {
                ITRULENAME = "";
            }

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitleForEmployeePayScale=" + reportTitle;
            //url += "&pathForEmployeePayScale=~,Reports,Payroll," + rptFileName;
            ////@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",
            //url += "&paramForEmployeePayScale=username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,PayRoll," + rptFileName;
            //url += "&param=@username=" + Session["username"].ToString().ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //ScriptManager.RegisterClientScriptBlock(updmain, updmain.GetType(), "Message", " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);

            //ScriptManager.RegisterClientScriptBlock(updmain, updmain.GetType(), "Message", " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
           // url += "&param=@username=" + Session["username"].ToString().ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PAYRULE=" + ITRULEID;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@Username=" + Session["username"].ToString().ToString() + ",@P_PAYRULE=" + ITRULENAME;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updmain, this.updmain.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "pay_Scale.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShowReport_Click(object sender, EventArgs e)
    {

        try
        {
            ShowReport("ScaleReport", "Pay_Scale.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Scale.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void FillPayHead()
    {
        try
        {
            // objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYFULL", "TYPE='C'", "SRNO");
            objCommon.FillDropDownList(ddlpayruleselect, "PAYROLL_RULE", "PAYRULE", "PAYRULE", "ACTIVESTATUS=1", "RULENO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnshowScaleReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("ScaleReport", "Pay_Scale.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Scale.btnshowScaleReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void ShowScaleReport(string reportTitle, string rptFileName)
    {
        try
        {
          //  int ITRULEID;
            string ITRULENAME;
            if (ddlpayruleselect.SelectedIndex > 0)
            {
                ITRULENAME =ddlpayruleselect.SelectedItem.Text;
            }
            else
            {
                ITRULENAME = "";
            }
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            // url += "&param=@username=" + Session["username"].ToString().ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PAYRULE=" + ITRULEID;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@Username=" + Session["username"].ToString().ToString() + ",@P_PAYRULE=" + ITRULENAME;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updmain, this.updmain.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {

        }

    }

}



