using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STORES_Reports_str_Store_ItemExpWarr : System.Web.UI.Page
{
    Common objCommon = new Common();
  
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
                //if (Session["userno"] == null || Session["username"] == null ||
                //    Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    //Session["MDNO"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));//Added by Vijay 03-06-2020

                    ViewState["butAction"] = "add";
                    //Session["dtitems"] = null;
                    Filldropdown();

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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlItem.SelectedIndex = 0;
    }

    protected void Filldropdown()
    {
        objCommon.FillDropDownList(ddlItem,"STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NO");
    }

  
    protected void btnRpt_Click(object sender, EventArgs e)
    {

          //if (rdbtnExpItem.Checked == true)
          //  {
          //      ShowReport("ExpiryItem", "str_ExpItemReport.rpt");
          //  }
          //  if (rdbtnWarrItem.Checked == true)
          //  {
          //       ShowReport("WarrantyItem", "str_ItemWarranty.rpt");
          //  }
        if (rblGroup.SelectedValue == "1")
        {
                 ShowReport("ExpiryItem", "str_ExpItemReport.rpt");
        }
        else
        {
            ShowReport("WarrantyItem", "str_ItemWarranty.rpt");
        }
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlItem.SelectedIndex = 0;

    }

    //Generate the report
    private void ShowReport(string reportTitle, string rptFileName)
    {

      //if(rdbtnExpItem.value)

      //  int Item = 0;
      //  if (rdbtnExpItem.Checked == true)
      //  {
      //      Item = 1;
      //  }

      //  if (rdbtnWarrItem.Checked == true)
      //  {
      //      Item = 2;
      //  }
        int Type = 0;
        if (rblGroup.SelectedValue == "1")
        {
            Type = 1;
        }
        else
        {
            Type = 2;
        }

        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ReportName=" + reportTitle + ",@p_Type=" + Convert.ToInt32(Item) + ",@p_ITEM_NO=" + ddlItem.SelectedValue + ",@P_FROM_DATE=" + Convert.ToDateTime(Fdate).ToString("dd-MMM-yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(Ldate).ToString("dd-MMM-yyyy") +,";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ReportName=" + reportTitle + ",@p_Type=" + Type + ",@p_ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
            //url += "&param=@p_Type=" + Type + ",@p_ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured.ToString(), this);
        }

    }
    protected void rblGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlItem.SelectedIndex = 0;
    }
}