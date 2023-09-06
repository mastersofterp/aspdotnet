using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using IITMS.UAIMS;
using mastersofterp_MAKAUAT;
public partial class ADMINISTRATION_AndroidMenuConfig : System.Web.UI.Page
{
    ConfigController objStud = new ConfigController();
    Common objCommon = new Common();
   
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
                   // this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method


                }
                BindListview();
            }
            ViewState["ipaddress"] = Request.ServerVariables["REMOTE_ADDR"];
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindListview()
    {
        DataSet ds = objStud.BindAndroidMenuConfig();
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            lvDataDisplay.DataSource = ds;
            lvDataDisplay.DataBind();
            lvDataDisplay.Visible = true;
        }
        else
        {
            lvDataDisplay.DataSource = null;
            lvDataDisplay.DataBind();
            lvDataDisplay.Visible = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int MenuId = 0;
        int IsOn = 0;
        string IsChecked = string.Empty;
        string menuid = string.Empty;
        foreach (ListViewDataItem item in this.lvDataDisplay.Items)
        {
            if (item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chkIson = item.FindControl("chkStatus") as CheckBox;
                HiddenField hf = item.FindControl("hfId") as HiddenField;
                if (chkIson.Checked)
                {
                    IsOn = 1;

                    IsChecked += IsOn + ",";
                   
                    MenuId = Convert.ToInt32(hf.Value);
                    menuid += MenuId + ",";
                    //objStud.AndroidMenuConfigUpdateCheckStatus(MenuId, IsOn);
                }

                if(chkIson.Checked!=true)
                {
                    IsOn = 0;
                    IsChecked += IsOn + ",";
                    MenuId = Convert.ToInt32(hf.Value);
                    menuid += MenuId + ",";
                    //objStud.AndroidMenuConfigUpdateCheckStatus(MenuId, IsOn);
                }
            }
        }
        int uano = Convert.ToInt32(Session["userno"].ToString());
        IsChecked = IsChecked.Substring(0, IsChecked.Length - 1);
        menuid = menuid.Substring(0, menuid.Length - 1);
        int a = objStud.AndroidMenuConfigUpdateCheckStatus(IsChecked, menuid, uano, ViewState["ipaddress"].ToString());
        if (a == 1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated Successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error')", true);
        }
        BindListview();
    }

    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindListview();
    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
        {
            popup.Visible = false;
            BindListview();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);

        }
        else
            objCommon.DisplayMessage("Password does not match!", this.Page);
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            Response.Redirect("~/principalHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "5")
        {
            Response.Redirect("~/homeNonFaculty.aspx", false);
        }
        else
        {
            Response.Redirect("~/home.aspx", false);
        }
    }
}