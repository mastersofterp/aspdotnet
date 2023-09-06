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


public partial class STORES_Reports_Str_Reorder_Level : System.Web.UI.Page
{
    Common objCommon = new Common();
    Masters objMasters = new Masters();

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

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //  objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());

        }
        else
        {
            Response.Redirect("~/Default.aspx");
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
                //int mdno = 0;
                //Session["strdeptcode"] = mdno.ToString();
            }
            else
            {
                objCommon = new Common();
                //objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
            }

            drpoDowntr.Visible = false;
            chkReorderLevel.Text = "All Reorder Level";
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
        ViewState["action"] = "add";
        Session["butAction"] = "add";
        drpoDowntr.Visible = false;
        rblGroup.SelectedValue = "1";
        rblSelectAllItem.SelectedValue = "0";
        chkReorderLevel.Checked = true;
        chkReorderLevel.Text = "All Reorder Level";
        this.FillItemsGroup();
    }

    //to show report click on report button.
    protected void btnreport_Click(object sender, EventArgs e)
    {

        ShowReport("ReorderLevelOfItems", "Str_Reorder_Level.rpt");

    }

    //fill dropdownlist on the basis of selected group of item
    protected void FillItemsGroup()
    {
        try
        {
            if (rblGroup.SelectedValue == "1")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
            else if (rblGroup.SelectedValue == "2")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "", "MISGNAME");
            else if (rblGroup.SelectedValue == "3")
                objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");


        }
        catch (Exception ex)
        {
            objCommon = new Common();
            // objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
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
        int intchkReorder = 0;
        if (rblSelectAllItem.SelectedValue == "0")
        {
            itemno = 0;
        }
        else if (rblSelectAllItem.SelectedValue == "1")
        {
            itemno = Convert.ToInt32(ddlItem.SelectedValue);
        }
        if (chkReorderLevel.Checked == true)
        {
            intchkReorder = 0;
        }
        else if (chkReorderLevel.Checked == false)
        {
            intchkReorder = 1;
        }
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            // url += "&param=@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMWISE=" + rblSelectAllItem.SelectedValue + ",@P_ITEMNO=" + itemno;
            url += "&param=@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMWISE=" + rblSelectAllItem.SelectedValue + ",@P_ITEMNO=" + itemno + ",@P_CHKREORDER=" + intchkReorder;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            objCommon = new Common();
            // objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
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

    protected void chkReorderLevel_CheckedChanged(object sender, EventArgs e)
    {
        if (chkReorderLevel.Checked == true)
        {
            chkReorderLevel.Text = "All Reorder Level";
        }
        else
        {
            chkReorderLevel.Text = "Below Reorder Level";
        }
    }


}