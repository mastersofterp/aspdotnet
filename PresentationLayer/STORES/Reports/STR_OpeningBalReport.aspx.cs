//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_openingbalreport.aspx.cs                                                 
// CREATION DATE : 28-May-2014                                                        
// CREATED BY    : VINOD ANDHALE                                                        
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class STORES_Reports_STR_OpeningBalReport : System.Web.UI.Page
{
    Common objCommon=new Common();
    Masters objMasters;

    //UserAuthorizationController objucc;
    string UsrStatus = string.Empty;


    //Check Logon Status and Redirect To Login Page(Default.aspx) if not logged in
    protected void Page_Load(object sender, EventArgs e)
    {
        //For displaying user friendly messages
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

        //Check Session
        if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
       
        if (!Page.IsPostBack)
        {
            CheckPageAuthorization();
            DataSet ds = new DataSet();
           
            ViewState["action"] = "add";
            Session["butAction"] = "add";

            if (Session["userno"] != null && Session["usertype"].ToString() != "1")
            {
                Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            }
            else if (Session["userno"] != null && Session["usertype"].ToString() == "1")
            {
                int mdno = 0;
                Session["strdeptcode"] = mdno.ToString();
            }
           

            drpoDowntr.Visible = false;

            this.FillItemsGroup();
           
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

    //reload the page.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Stores/Report/Str_OpeningBalReport.aspx");
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    //to show report click on report button.
    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (rblGroup.SelectedValue == "3" && rblSelectAllItem.SelectedValue == "0")
            ShowReport("OpeningBalance", "Str_OpeningBalRptAllItem.rpt");
        else
            ShowReport("OpeningBalance", "Str_OpeningBalRpt.rpt");
          
    }

    //fill dropdownlist on the basis of selected group of item
    protected void FillItemsGroup()
    {
        try
        {
            //objCommon.FillDropDownList(ddlItem , "Store_Item", "item_no", "item_NAME", "", "");
            if(rblGroup .SelectedValue == "1")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
            else if(rblGroup .SelectedValue =="2")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "", "MISGNAME");
            else if(rblGroup .SelectedValue =="3")
                objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
          

        }
        catch (Exception ex)
        {
            objCommon = new Common();
          objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }
    }


    protected void rblGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        this.FillItemsGroup();
    }

    //Generate the report
    private void ShowReport(string reportTitle, string rptFileName)
     {
        int itemno = 0;
        if (rblSelectAllItem.SelectedValue == "0")
        {
            itemno  = 0;
        }
        else if (rblSelectAllItem.SelectedValue == "1")
        {
            itemno  = Convert.ToInt32(ddlItem.SelectedValue);
        }
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMWISE=" + rblSelectAllItem.SelectedValue + ",@P_ITEMNO=" + itemno;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {

            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }

    }

  
    //dropdownlist will visible or not 
    protected void rblSelectAllItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelectAllItem.SelectedValue == "0")
            drpoDowntr.Visible = false;
        else if (rblSelectAllItem.SelectedValue == "1")
            drpoDowntr.Visible = true;
    }
    protected void rdoItemGroupList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}