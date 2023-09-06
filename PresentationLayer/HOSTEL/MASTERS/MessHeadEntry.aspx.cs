//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : Mess Head Entry                                            
// CREATION DATE : 20-01-20013                                                          
// CREATED BY    : Pawan Mourya                                    
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class HOSTEL_MASTERS_MessHeadEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    MessMasterController objMC = new MessMasterController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();
                    BindListViewMessHead();
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
            
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_MessHeadEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MessHeadEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MessHeadEntry.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvHead in lvMessHead.Items)
            {
                Label txtHead = lvHead.FindControl("txtHead") as Label;
                TextBox txtLName = lvHead.FindControl("txtLName") as TextBox;
                TextBox txtSName = lvHead.FindControl("txtSName") as TextBox;

                CustomStatus cs = (CustomStatus)objMC.UpdateMessHead(Convert.ToInt32((txtHead.ToolTip)), (txtSName.Text), (txtLName.Text));
                if (cs.Equals(CustomStatus.RecordUpdated))
                    count++;
            }
            BindListViewMessHead();
            if (count == lvMessHead.Items.Count)
                lblmsg.Text = "Record Saved Successfully";

            else
                lblerror.Text = "Error...";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "MessHeadEntry.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void BindListViewMessHead()
    {
        try
        {
            //FeesHeadController objFHC = new FeesHeadController();
            DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_MESS_HEAD", "MESS_HEAD_NO", "MESS_HEAD,MESS_SHORTNAME,MESS_LONGNAME", "MESS_HEAD_NO>0 and MESS_HEAD_NO<13", "MESS_HEAD_NO");
            if (ds.Tables[0].Rows.Count <= 0)
            {
                pnlfees.Visible = false;
            }
            else
            {
                pnlfees.Visible = true;
                lvMessHead.DataSource = ds;
                lvMessHead.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "MessHeadEntry.BindListViewFeesHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
