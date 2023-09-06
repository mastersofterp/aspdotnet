using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class QuickLinks : System.Web.UI.Page
{
    CustomStatus cs = new CustomStatus();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
                }
                Session["pageno"] = Request.QueryString["pageno"];
            }
            else if (Session["pageno"].ToString() != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Session["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
            }
        }
        catch { }
    }

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
                String query = Request.QueryString["q"];
                BindListView();             
            }
           
        }
    }

    public void BindListView()
    {
        try
        {
            Access_LinkController objACLink = new Access_LinkController();
            DataSet ds = objACLink.GetUserAccAssignLinks(Convert.ToInt32(Session["userno"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvALinks.DataSource = ds;
                lvALinks.DataBind();
                //dpPager.Visible = true;
            }
            else
            {
                lvALinks.DataSource = null;
                lvALinks.DataBind();
                //dpPager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Access_LinkController objACLink = new Access_LinkController();

            string QL_AL_NO = string.Empty;
            string QL_AL_NAME = string.Empty;
            int ua_no = Convert.ToInt32(Session["userno"]);
            foreach (ListViewDataItem lvitem in lvALinks.Items)
            {
                CheckBox chk = lvitem.FindControl("chkSelect") as CheckBox;
                Label lbl = lvitem.FindControl("lblPage") as Label;
                if (chk.Checked == true)
                {
                    QL_AL_NO += chk.ToolTip + ",";
                    QL_AL_NAME += lbl.ToolTip + ",";
                }
            }

            //validation
            if (QL_AL_NO.Length <= 0)
            {
                objCommon.DisplayMessage(this.updatepanel, "Please Select atleast one Link", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objACLink.AddUserQLinks(ua_no, QL_AL_NO.TrimEnd(','), QL_AL_NAME.TrimEnd(','));

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updatepanel, "User Quick Links Created Successfully!!!", this.Page);
                Response.Redirect(Request.Url.ToString());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "assign_link.btnAssign_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}