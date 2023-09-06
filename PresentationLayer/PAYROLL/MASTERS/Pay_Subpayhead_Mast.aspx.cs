//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYROLL                                                         
// PAGE NAME     : Pay_Subpayhead_Mast.aspx                               
// CREATION DATE : 01-MAY-2009                                                     
// CREATED BY    : KIRAN GVS                                                
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Masters_Pay_Subpayhead_Mast : System.Web.UI.Page
{
    Common objCommon = new Common();
    PayController objPay = new PayController();

    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // Populate DropDownList
                PopulateDropdown();

                //Bind the ListView with sub payheads
                BindListViewSubPayHeads();

                ViewState["action"] = "add";

                //Set Report Parameters
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Payroll" + "," + "CrystalReport.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
    }


    private void PopulateDropdown()
    {
        try
        {

            objCommon.FillDropDownList(ddlMainPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYFULL", "PAYSHORT IS NOT NULL and PAYSHORT<>'' and PAYSHORT <> '-' AND TYPE ='E' ", "SRNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.PopulateDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListViewSubPayHeads()
    {
        try
        {
            DataSet ds = objPay.GetAllSubPayHead();
            lvSubPayhead.DataSource = ds;
            lvSubPayhead.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.BindListViewSubPayHeads-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=qualificationmas.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckPurpose();

                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objPay.AddSubPayHead(ddlMainPayhead.SelectedValue, txtshortpayhead.Text, txtFullName.Text, ChkBookAdj(), Session["colcode"].ToString(), chkMainHead.Checked);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindListViewSubPayHeads();
                            Clear();
                            lblStatus.Text = "Record successfully saved";
                        }
                        else
                            lblStatus.Text = "Error";
                    }
                }
                else
                {
                    //Edit Quarter
                    if (ViewState["ibno"] != null)
                    {
                        bool result = CheckPurpose();

                        if (result == true)
                        {
                            //objCommon.DisplayMessage("Record Already Exist", this);
                            MessageBox("Record Already Exist");
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objPay.UpdateSubPayHead(Convert.ToInt32(ViewState["ibno"]), ddlMainPayhead.SelectedValue, txtshortpayhead.Text, txtFullName.Text, ChkBookAdj(), chkMainHead.Checked);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                BindListViewSubPayHeads();
                                Clear();
                                lblStatus.Text = "Record successfully updated";
                            }
                            else
                                lblStatus.Text = "Error";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlMainPayhead.SelectedIndex = 0;
        txtshortpayhead.Text = string.Empty;
        txtFullName.Text = string.Empty;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
        chkBookAbj.Checked = false;

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewSubPayHeads();
    }
    private int ChkBookAdj()
    {
        int value = 0;

        if (chkBookAbj.Checked == true)
        {
            value = 1;
        }
        else
        {
            value = 0;
        }

        return value;
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["ibno"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblPayhead = lst.FindControl("lblPayhead") as Label;
            Label lblSname = lst.FindControl("lblSname") as Label;
            Label lblFname = lst.FindControl("lblFname") as Label;
            //Label lblBookAdj = lst.FindControl("lblBookAdj") as Label;
            Label lblMainHead = lst.FindControl("lblMainHead") as Label;

            if (lblMainHead.Text == "Yes")
            {
                chkMainHead.Checked = true;
            }
            else
            {
                chkMainHead.Checked = false;
            }

            ddlMainPayhead.SelectedValue = lblPayhead.Text;
            txtshortpayhead.Text = lblSname.Text;
            txtFullName.Text = lblFname.Text;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
        dsPURPOSE = objCommon.FillDropDown("PAYROLL_PAY_SUBPAYHEAD", "*", "", "FULLNAME='" + txtFullName.Text + "' OR SHORTNAME='" + txtshortpayhead.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
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
            //if (ddlPayhead.SelectedIndex > 0)
            //{
            //    payheadid = Convert.ToInt32(ddlPayhead.SelectedValue);
            //}
            //else
            //{
            //    payheadid = 0;
            //}

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

            ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);
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
