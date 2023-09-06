
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
public partial class VEHICLE_MAINTENANCE_Report_RegisterReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM objVM = new VM();
    VMController objVMC = new VMController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    FillDropDown();
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;
                  
                  
                   
                  
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Servicing.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlvehical, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0", "VIDNO");
        objCommon.FillDropDownList(ddlworkshop, "VEHICLE_WORKSHOP", "WSNO", "WORKSHOP_NAME", "WSNO>0", "WORKSHOP_NAME");
        //VEHICLE_SERVICEMAS
        //BILLNO
        objCommon.FillDropDownList(ddlbillno, "VEHICLE_SERVICEMAS", "BILLNO", "BILLNO", "WSNO>0", "BILLNO");
       
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Vehical Management", "reptServiceReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
       
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=ServiceReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_VIDNO="+ddlvehical.SelectedValue+",@P_WSNO="+ ddlworkshop.SelectedValue +",@P_BILLNO="+ ddlbillno.SelectedValue ; //+",@P_TTID="+ ddlTripType.SelectedValue +",@P_PASSENGER="+txtPassenger.Text.Trim();



            if (txtfrmDate.Text.Trim() != string.Empty && txttoDate.Text.Trim() != string.Empty)
            {
                url += ",@P_FROMDATE=" + txtfrmDate.Text + ",@P_TODATE=" + txttoDate.Text;
            }
            else
            {
                url += ",@P_FROMDATE=null,@P_TODATE=null";
            }
            


            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void chkvehical_CheckedChanged(object sender, EventArgs e)
    {
        if (chkvehical.Checked == true)
        {
            ddlvehical.Enabled = true;

        }
        else
        {
            ddlvehical.Enabled = false;
        }

    }
    protected void chkworkshop_CheckedChanged(object sender, EventArgs e)
    {
        if (chkworkshop.Checked == true)
        {
            ddlworkshop.Enabled = true;

        }
        else
        {
            ddlworkshop.Enabled = false;
        }
    }
    protected void chkbill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbill.Checked == true)
        {
            ddlbillno.Enabled = true;

        }
        else
        {
            ddlbillno.Enabled = false;
        }

    }
    protected void chkdate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdate.Checked == true)
        {
            txtfrmDate.Enabled = true;
                txttoDate.Enabled=true;
        }
        else
        {
            txtfrmDate.Enabled = false;
            txttoDate.Enabled = false;

         }
    }
}
