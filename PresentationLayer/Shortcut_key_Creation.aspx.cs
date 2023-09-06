//=================================================================================
// PROJECT NAME  : MAKAUT_CODE                                                          
// MODULE NAME   : TO CREATE SHORTCUT KEY FOR LINK                                 
// CREATION DATE : 26-Octomber-2021                                                   
// CREATED BY    : PRANITA A. HIRADKAR                                                 
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

using System.Data.SqlClient;
using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class Shortcut_key_Creation : System.Web.UI.Page
{
    CustomStatus cs = new CustomStatus();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Access_LinkController objLink = new Access_LinkController();
    Access_Link objAL = new Access_Link();

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
                //Page Authorization
                 CheckPageAuthorization();

                objCommon.FillDropDownList(ddlModule, "ACCESS_LINK  L INNER JOIN ACC_SECTION C ON (C.AS_NO = L.AL_ASNO)", "DISTINCT L.AL_ASNO", "C.AS_TITLE", "L.AL_ASNO > 0", "L.AL_ASNO");
                pnlList.Visible = true;
                pnlAdd.Visible = false;
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
        }
        
        //   string status =Session["status"].ToString();
        //if (status=="1")
        //{
        //    objCommon.DisplayMessage(this.Page,"ppp",this.Page);
        //}
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Shortcut_key_Creation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Shortcut_key_Creation.aspx");
        }
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int al_asno = Convert.ToInt32(ddlModule.SelectedValue);
            lvALinks.Visible = true;
            DataSet ds = objLink.GetDomaindata(al_asno); 
            if (ds != null && ds.Tables.Count > 0)
            {
                lvALinks.DataSource = ds;
             //   ViewState["DDL_Data"] = ds;
                lvALinks.DataBind();
                foreach (ListViewItem item in lvALinks.Items)
                {
                    Label lblactinestatus = item.FindControl("lblactinestatus") as Label;
                    if (lblactinestatus.Text == "1")
                    {
                        lblactinestatus.Text = "Active";
                        lblactinestatus.Style.Add("color", "Green");
                    }
                    else
                    {
                        lblactinestatus.Text = "DeActive";
                        lblactinestatus.Style.Add("color", "Red");
                    }
                }
            }
            pnlList.Visible = true;
            Panel1.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Shortcut_key_Creation.ddlModule_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlAdd.Visible = true;
            Panel1.Visible = false;
            pnlList.Visible = false;
            ImageButton btnEdit = sender as ImageButton;
            int al_no = int.Parse(btnEdit.CommandArgument);
            ViewState["ID"] = al_no;
            ShowDetail(al_no);
            //string pageurl = Request.Url.ToString() + "&action=edit&al_no=" + al_no.ToString();
            //Response.Redirect(pageurl);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Shortcut_key_Creation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
        ddlModule.SelectedIndex = 0;
        pnlAdd.Visible = false;
        Panel1.Visible = false;
        pnlList.Visible = true;
    }

    private void ShowDetail(int al_no)
    {
        try
        {
            SqlDataReader dr = objLink.GetSingleRecord(al_no);
            //Show Detail
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtLinkTitle.Text = dr["al_link"] == null ? "" : dr["al_link"].ToString();
                    txtLinkUrl.Text = dr["al_url"] == null ? "" : dr["al_url"].ToString();
                    txtShortcutkey.Text = dr["SHORTCUT_KEY"] == null ? "" : dr["SHORTCUT_KEY"].ToString();
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Shortcut_key_Creation.ShowDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        try
        {
            ddlModule.SelectedIndex = 0;
            objAL.Al_No = Convert.ToInt32(ViewState["ID"]);
            objAL.Al_Link = txtLinkTitle.Text.Trim();
            objAL.Al_Url = txtLinkUrl.Text.Trim();
            objAL.Shortcut_key = txtShortcutkey.Text.Trim();
          
                CustomStatus cs = (CustomStatus)objLink.UpdateAccessLinkShortcutKey(objAL);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(updlink, "Link Shortcut Key Updated Successfully", this.Page);
                }
                else if(cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayUserMessage(updlink, "Link Shortcut Key Already Exist", this.Page);
                }
                else
                lblStatus.Text = "Error..";

                pnlList.Visible = true;
                Panel1.Visible = true;
                pnlAdd.Visible = false;
                Panel1.Visible = false;
              }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Shortcut_key_Creation.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        }
}