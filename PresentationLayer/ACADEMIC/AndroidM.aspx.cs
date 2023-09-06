using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using mastersofterp_MAKAUAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_AndroidM : System.Web.UI.Page
{
    ConfigController objStud = new ConfigController();
    Common objCommon = new Common();
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                   BindListview();
                   BindListviewAPIDetails();
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AndroidM.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=AndroidM.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AndroidM.aspx");
        }
    }
    private void BindListview()
    {
        DataSet ds = null;
        ds = objStud.BindAndroidMenuConfig();
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

                if (chkIson.Checked != true)
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
        string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        int a = objStud.AndroidMenuConfigUpdateCheckStatus(IsChecked, menuid, uano, IPADDRESS.ToString());
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
    protected void btnSubmitApiDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string APPID = string.Empty;
            string SERVER_KEY = string.Empty;
            string APP_APIURL = string.Empty;
            int id = 0;

            APPID = txtAapiId.Text.ToString();
            SERVER_KEY = txtServerKey.Text.ToString();
            APP_APIURL = txtApiUrl.Text.ToString();



            id = (btnSubmitApiDetails.Text == "Update") ? Convert.ToInt32(Session["idApp"]) : 0;
           
           
            int a = objStud.AddInsertUpdateAppDetails(APPID, SERVER_KEY, APP_APIURL, id);
            if (a == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Added Successfully.')", true);
                Session["idApp"] = string.Empty;
                btnSubmitApiDetails.Text = "Submit";
            }
            if (a == 2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully.')", true);
                Session["idApp"] = string.Empty;
                btnSubmitApiDetails.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error')", true);
                Session["idApp"] = string.Empty;
                btnSubmitApiDetails.Text = "Submit";
            }
            BindListviewAPIDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AndroidM.btnSubmitApiDetails_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void BindListviewAPIDetails()
    {
        try 
        {
            DataSet ds = null;
            ds = objStud.GetDataOfApiDetails();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvAppDetails.DataSource = ds;
                lvAppDetails.DataBind();
                lvAppDetails.Visible = true;
            }
            else
            {
                lvAppDetails.DataSource = null;
                lvAppDetails.DataBind();
                lvAppDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AndroidM.BindListviewAPIDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
    protected void btnCancelApiDetails_Click(object sender, EventArgs e)
    {
        txtAapiId.Text=string.Empty;
        txtServerKey.Text=string.Empty;
        txtApiUrl.Text = string.Empty;

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int id = int.Parse(btnEdit.CommandArgument);
            Session["idApp"] = id;
            btnSubmitApiDetails.Text = "Update";
            DataSet ds = null;
            ds = objStud.GetDataOfApiDetailsEdit(id);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtAapiId.Text = ds.Tables[0].Rows[0]["FIREBASE_APP_ID"].ToString(); ;
                txtServerKey.Text = ds.Tables[0].Rows[0]["FIREBASE_SERVER_KEY"].ToString(); ;
                txtApiUrl.Text = ds.Tables[0].Rows[0]["APP_API_URL"].ToString(); ;
            }
            BindListviewAPIDetails();
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AndroidM.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
          
       
    }
}