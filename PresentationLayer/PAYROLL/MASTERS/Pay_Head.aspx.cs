//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Head.ASPX                                                    
// CREATION DATE : 05-May-2009                                                        
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

public partial class PayRoll_Pay_Head : System.Web.UI.Page
{
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
            btnSub.Visible = false;
            btnBack.Visible = false;
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

                pnlSelect.Visible = true;

                pnlupdate.Visible = false;

                //Enabled listview panal
                if (!(Convert.ToInt32(ddlPayhead.SelectedIndex) == 0))
                {
                    BindListViewList(Convert.ToInt32(ddlPayhead.SelectedValue.ToString()));

                }
                else
                {
                    pnlList.Visible = false;
                }

            //    objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Payroll" + "," + "Pay_Payhead.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Head.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=Pay_Head.aspx");
        }
    }

    private void BindListViewList(Int32 idhead)
    {
        try
        {
            if (!(Convert.ToInt32(ddlPayhead.SelectedIndex) == 0))
            {
                pnlList.Visible = true;
                DataSet ds = objpay.GetIdHead(idhead);
                lvPayhead.DataSource = ds;
                lvPayhead.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head.ddlRMDept_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(Convert.ToInt32(ddlPayhead.SelectedIndex) == 0))
        {
            BindListViewList(Convert.ToInt32(ddlPayhead.SelectedValue.ToString()));

        }
        else
        {
            pnlList.Visible = false;
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int srno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(srno);
            pnlupdate.Visible = true;
            pnlSelect.Visible = false;
            pnlList.Visible = false;
            btnSub.Visible = true;
            btnBack.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int srno)
    {
        DataSet ds = null;
        try
        {
            ds = objpay.GetRetPayHead(srno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["srno"] = srno.ToString();
                txtPaycode.Text = ds.Tables[0].Rows[0]["PAYHEAD"].ToString();
                txtPayhead.Text = ds.Tables[0].Rows[0]["PAYSHORT"].ToString();
                if (!(ds.Tables[0].Rows[0]["TYPE"].ToString() == null || ds.Tables[0].Rows[0]["TYPE"].ToString() == "" || ds.Tables[0].Rows[0]["TYPE"].ToString() == string.Empty)) ddltype.Text = ds.Tables[0].Rows[0]["TYPE"].ToString();
                if (!(ds.Tables[0].Rows[0]["CAL_ON"].ToString() == null || ds.Tables[0].Rows[0]["CAL_ON"].ToString() == "" || ds.Tables[0].Rows[0]["CAL_ON"].ToString() == string.Empty)) ddlcal.Text = ds.Tables[0].Rows[0]["CAL_ON"].ToString();
                txtFullname.Text = ds.Tables[0].Rows[0]["PAYFULL"].ToString();
                txtFormula.Text = ds.Tables[0].Rows[0]["FORMULA"].ToString();
                enabledisable(ds.Tables[0].Rows[0]["TYPE"].ToString());
                chkperlock.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Isclearamount"].ToString());
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            btnSub.Visible = false;
            btnBack.Visible = false;
            Payroll objpayhead = new Payroll();
            objpayhead.Srno = Convert.ToInt32(ViewState["srno"].ToString());
            objpayhead.PayShort = txtPayhead.Text.Trim();
            objpayhead.PayFull = txtFullname.Text.Trim();
            if (!(ddlcal.SelectedValue.ToString() == "-1")) objpayhead.CalOn = ddlcal.SelectedValue;
            if (!(ddltype.SelectedValue.ToString() == "-1")) objpayhead.Type = ddltype.SelectedValue;
            objpayhead.Formula = txtFormula.Text;
            objpayhead.Isclearamount = chkperlock.Checked;
            CustomStatus cs = (CustomStatus)objpay.UpdatePayHead(objpayhead);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                pnlupdate.Visible = false;
                pnlSelect.Visible = true;
                pnlList.Visible = true;
                Clear();

            }
            if (!(Convert.ToInt32(ddlPayhead.SelectedIndex) == 0))
            {
                BindListViewList(Convert.ToInt32(ddlPayhead.SelectedValue.ToString()));

            }
            else
            {
                pnlList.Visible = false;
            }

            Showmessage("Record Updated Successfully.");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head.btnSub_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlupdate.Visible = false;
        pnlSelect.Visible = true;
        pnlList.Visible = true;
        btnBack.Visible = false;
        btnSub.Visible = false;
    }

    private void Clear()
    {
        txtPaycode.Text = null;
        txtPayhead.Text = null;
        ddltype.SelectedIndex = 0;
        ddlcal.SelectedIndex = 0;
        txtFullname.Text = null;
        txtFormula.Text = null;
        chkperlock.Checked = false;
        //txtseqno.Text = null;

    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        enabledisable(ddltype.SelectedValue);
    }

    private void enabledisable(string type)
    {
        try
        {
            if (type == "E")
            {
                calrow.Visible = false;
                txtFormula.Text = string.Empty;
                ddlcal.SelectedIndex = 0;
                formularow.Visible = false;
            }
            if(type == "-1") // updation done in 22092023
            {
                calrow.Visible = false;
                txtFormula.Text = string.Empty;
                ddlcal.SelectedIndex = 0;
                formularow.Visible = false;
            }
            if(type  == "C")
            {
                calrow.Visible = true;
                formularow.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head.enabledisable-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlcal_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFormula.Text = ddlcal.Text;
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Pay Head Report", "Pay_Payhead.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Scale.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int payheadid;
            if (ddlPayhead.SelectedIndex > 0)
            {
                payheadid = Convert.ToInt32(ddlPayhead.SelectedValue);
            }
            else
            {
                payheadid = 0;
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@Username=" + Session["username"].ToString().ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "pay_Scale.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}
