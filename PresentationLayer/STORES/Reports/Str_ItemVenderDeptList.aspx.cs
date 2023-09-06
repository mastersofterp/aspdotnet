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

public partial class STORES_Reports_Str_ItemVenderDeptList : System.Web.UI.Page
{

    Common objCommon;
    Masters objMasters;


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
           objCommon = new Common();

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!Page.IsPostBack)
        {
            CheckPageAuthorization();  
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
            else
            {
                objCommon = new Common();
               // objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
            }
            trgrp.Visible = false;
            drpoDowntr.Visible = false;
            trrblVendor.Visible = false;
            trCateg.Visible = false;
            trrblDept.Visible = false;
            trMainDept.Visible = false;

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
        //Response.Redirect("~/Stores/Reports/Str_ItemVenderDeptList.aspx");
        Response.Redirect(Request.RawUrl);  
    }

    //to show report click on report button.
    protected void btnreport_Click(object sender, EventArgs e)
    {
       
            if (ddlType.SelectedValue == "1")
            {
                ShowReport("ItemList", "Str_ItemList.rpt");
            }
            else if (ddlType.SelectedValue == "2")
            {
                ShowReport("VendorList", "Str_VendorList.rpt");
            }
            else if (ddlType.SelectedValue == "3")
            {
                ShowReport("DepartmentList", "Str_DepartmentList.rpt");
            }
     
        else
        {
           objCommon.DisplayMessage(updpanel, "Please Select Group.", this);
            return;
        }
    }

    //Generate the report
    private void ShowReport(string reportTitle, string rptFileName)
    {


        try
        {
            string Script = string.Empty;
            int Deptno;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            if (ddlType.SelectedValue == "1")
            {
                if (rblGroup.SelectedValue == "3")
                {
                    url += "&param=@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMGROUPNAME=" + ddlItem.SelectedItem.Text;
                }
                else
                    url += "&param=@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMGROUPNO=" + ddlItem.SelectedValue;
            }
            else if (ddlType.SelectedValue == "2")
            {
                url += "&param=@P_CATEGORYWISE=" + rblAllParticularVendor.SelectedValue + ",@P_CATEGORYNO=" + ddlCategory.SelectedValue;
            }
            else if (ddlType.SelectedValue == "3")
            {
                int padeptno = 0;
                if (rblDeptWise.SelectedValue == "1")
                {
                    padeptno = 1;
                }
                else
                    padeptno = 0;
                if (ddlDept.SelectedValue == "") Deptno = 0;
                else Deptno = Convert.ToInt32(ddlDept.SelectedValue);
                url += "&param=@P_MAINDEPTWISE=" + rblDeptWise.SelectedValue + ",@P_DEPTNO=" + Deptno + ",@P_PADEPT=" + padeptno;
            }
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {

            objCommon = new Common();
           // objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
        }

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
            trgrp.Visible = true;
            drpoDowntr.Visible = true;
            trrblVendor.Visible = false;
            trCateg.Visible = false;
            trrblDept.Visible = false;
            trMainDept.Visible = false;
            this.FillDropDown();
        }
        else if (ddlType.SelectedValue == "2")
        {
            trrblVendor.Visible = true;
            trgrp.Visible = false;
            drpoDowntr.Visible = false;
            trrblDept.Visible = false;
            trMainDept.Visible = false;
            //trCateg.Visible = true;
            this.FillDropDown();
        }
        else if (ddlType.SelectedValue == "3")
        {
            trgrp.Visible = false;
            drpoDowntr.Visible = false;
            trrblVendor.Visible = false;
            trCateg.Visible = false;
            trrblDept.Visible = true;
            //trMainDept.Visible = true;
            this.FillDropDown();
        }
    }

    public void FillDropDown()
    {
        if (ddlType.SelectedValue == "1")
        {
            if (rblGroup.SelectedValue == "1")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
            else if (rblGroup.SelectedValue == "2")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "", "MISGNAME");
        }
        else if (ddlType.SelectedValue == "2")
        {
            //if (rblAllParticularVendor.SelectedValue == "1")
            //{
                objCommon.FillDropDownList(ddlCategory, "STORE_PARTY_CATEGORY", "PCNO", "PCNAME", "", "PCNAME");
           // }
        }
        else if (ddlType.SelectedValue == "3")
        {
            if (rblDeptWise.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "MDNO>0", "MDNAME");
            }
        }

    }
    protected void rblGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillDropDown();
    }
    protected void rblAllParticularVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAllParticularVendor.SelectedValue == "1")
            trCateg.Visible = true;
        else
            trCateg.Visible = false;
        this.FillDropDown();

    }
    protected void rblDeptWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblDeptWise.SelectedValue == "1")
            trMainDept.Visible = true;
        else
            trMainDept.Visible = false;
        this.FillDropDown();
    }
}
