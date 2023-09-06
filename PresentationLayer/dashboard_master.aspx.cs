
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;


public partial class dashboard_master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    int flag;
    //ConnectionStrings
    private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();
                //User_AccController objUACC = new User_AccController();
                //DataSet ds = objUACC.getdashboarddetails();

                objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", string.Empty, "SESSIONNO DESC");
                bindlist();
            }
        }
    }


    private void bindlist()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_MASTER A INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO=B.SESSIONNO", "ID", "NAME,STATUS,A.SESSIONNO,SESSION_PNAME", "ID>0", "NAME");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvdashboard.DataSource = ds;
                    lvdashboard.DataBind();
                }
                else
                {
                    lvdashboard.DataSource = null;
                    lvdashboard.DataBind();
                }
            }
            else
            {
                lvdashboard.DataSource = null;
                lvdashboard.DataBind();
            }
        }
        catch (Exception ex)
        { 
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool status = false;
            int sessionno = Convert.ToInt32(ddlsession.SelectedValue);
            string idno = txtdashboardname.ToolTip;
            if (hfdActive.Value == "true")
            {
                status = true;
            }
            else
            {
                status = false;
            }
            if (idno == "")
            {

                User_AccController objUACC = new User_AccController();
                string dashboardname = txtdashboardname.Text;
               
                int ret = objUACC.InsertDashboarddetails(dashboardname,sessionno, status);

                if (ret == 1)
                {
                    objCommon.DisplayMessage(this.upd1, "Record Inserted Successfully", this.Page);
                    txtdashboardname.Text = string.Empty;
                    bindlist();
                }
                else
                {
                    objCommon.DisplayMessage(this.upd1, "Failed to Insert", this.Page);
                }
            }
            else
            {
                User_AccController objUACC = new User_AccController();
                
                string dashboardname = txtdashboardname.Text;
                idno = txtdashboardname.ToolTip;
                //if (chkstatus.Checked)
                //{
                //    status = 1;
                //}
                //else
                //{
                //    status = 0;
                //}
                int ret = objUACC.UpdateDashboarddetails(idno, dashboardname, status, sessionno);
                if (ret == 1)
                {
                    objCommon.DisplayMessage(this.upd1, "Record Updated Successfully", this.Page);
                    txtdashboardname.Text = string.Empty;
                    ddlsession.SelectedValue = "0";
                    //chkstatus.Checked = false;
                    bindlist();
                   
                }
                else
                {
                    objCommon.DisplayMessage(this.upd1, "Failed to Update", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void Clear()
    {
        txtdashboardname.Text = string.Empty;
        ddlsession.SelectedValue = "0";
        //chkstatus.Checked = false;
        //lvdashboard.DataSource = null;
        //lvdashboard.DataBind();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
        }

    }
    protected void lvdashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        Label lbl = e.Item.FindControl("lblstatus") as Label;
        if (lbl.Text == "True")
        {
            lbl.Text = "Active";
            lbl.Style.Add("color", "Green");
            //cnt_registered++;
        }
        else
        {
            lbl.Text = "In-Active";
            lbl.Style.Add("color", "Red");
            //cnt_pending++;
        }


    }

    private void ShowDetail(int idno)
    {
        User_AccController objACC = new User_AccController();
        DataTableReader dtr;

        dtr = objACC.GetUserBydashidno(idno);
        if (ViewState["action"] == "edit".ToString())
        {
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    txtdashboardname.Text = dtr["NAME"] == DBNull.Value ? string.Empty : dtr["NAME"].ToString();

                    txtdashboardname.ToolTip = dtr["ID"] == DBNull.Value ? string.Empty : dtr["ID"].ToString();
                    ddlsession.SelectedValue = dtr["SESSIONNO"] == DBNull.Value ? string.Empty : dtr["SESSIONNO"].ToString();

                    //chkstatus.Checked = Convert.ToInt32(dtr["STATUS"]) == 1 ? true : false;
                    if (dtr["STATUS"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                    }
                }
            }
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            flag = 1;
            ImageButton btnEdit = sender as ImageButton;
            int idno = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetail(idno);

        }
        catch (Exception ex)
        {

        }
    }
}