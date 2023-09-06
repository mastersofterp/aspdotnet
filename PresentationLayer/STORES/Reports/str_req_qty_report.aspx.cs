//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : str_req_qty_report.aspx                                               
// CREATION DATE : 23-DEC-2014                                                        
// CREATED BY    : NITIN MESHRAM                                                 
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class STORES_Reports_str_req_qty_report : System.Web.UI.Page
{
    Common objCommon = new Common();
    Masters objMasters = new Masters();


    string UsrStatus = string.Empty;
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
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
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
                 //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["butAction"] = "add";
                //Session["dtitems"] = null;

            }

            drpoDowntr.Visible = false;
            this.FillVendor();
            txtFrmDt.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
            txtTodt.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

    //to generate the Goods receipt register report
    protected void btnGRR_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
        {
            objCommon.DisplayMessage(this.Page,"To Date Should Be Greater Than Or Equal To From Date.",this.Page);
            return;
        }
        ShowReport("Required Item", "str_req_qty.rpt");
    }


    //fill dropdownlist with vendor name
    protected void FillVendor()
    {
        try
        {
            objCommon.FillDropDownList(ddlVendor, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");

        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured.ToString(), this);
        }
    }


    //Generate the report
    private void ShowReport(string reportTitle, string rptFileName)
    {

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFrmDt.Text)));
        Fdate = Fdate.Substring(0, 10);
        string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
        Ldate = Ldate.Substring(0, 10);

        int vendorno = 0;
        if (rblSelectAllVendor.SelectedValue == "0")
        {
            vendorno = 0;
        }
        else if (rblSelectAllVendor.SelectedValue == "1")
        {
            vendorno = Convert.ToInt32(ddlVendor.SelectedValue);
        }
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISNO=" + vendorno + ",@P_FROMDATE=" + Convert.ToDateTime(Fdate).ToString("dd-MMM-yyyy") + ",@P_TODATE=" + Convert.ToDateTime(Ldate).ToString("dd-MMM-yyyy") + ",@P_MDNO=" + Convert.ToInt32(Session["strdeptcode"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured.ToString(), this);
        }

    }

    //to generate the stores register report 
    protected void btnStrReg_Click(object sender, EventArgs e)
    {
        ShowReport("StoresRegister", "Str_StoresRegister.rpt");
    }


    protected void rblSelectAllVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelectAllVendor.SelectedValue == "1")
        {
            drpoDowntr.Visible = true;
        }
        else
        {
            drpoDowntr.Visible = false;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFrmDt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        rblSelectAllVendor.SelectedValue = "0";
        drpoDowntr.Visible = false;

    }
    protected void btnInwardreport_Click(object sender, EventArgs e)
    {

        ShowReport("InwardReport", "Str_InwardBirla.rpt");

    }
}
