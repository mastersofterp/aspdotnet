//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ADMISSION SESSION
// CREATION DATE : 22-Mar-2023                                                          
// CREATED BY    : NEHAL                                                                    
//======================================================================================

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_PHD_PhD_Admission_Session : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhd = new PhdController();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                BindListView();
            }
            //BindListView();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhD_Admission_Session.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhD_Admission_Session.aspx");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objPhd.GetAllPhdSession(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvSession.DataSource = ds;
                lvSession.DataBind();
            }
            else
            {
                pnlSession.Visible = false;
                lvSession.DataSource = null;
                lvSession.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int sessionid = int.Parse(btnEdit.CommandArgument);
        Session["sessionid"] = int.Parse(btnEdit.CommandArgument);
        ViewState["edit"] = "edit";

        this.ShowDetails(sessionid);
        txtName.Focus();
    }
    private void ShowDetails(int sessionid)
    {
        try
        {
            DataSet ds = objPhd.GetAllPhdSession(sessionid);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                string IS_ACTIVE = ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["SESSION_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["SESSION_STDATE"].ToString()).ToString("dd/MM/yyyy");
                txtToDate.Text = ds.Tables[0].Rows[0]["SESSION_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["SESSION_ENDDATE"].ToString()).ToString("dd/MM/yyyy");

                if (IS_ACTIVE == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
            }
            if (ds != null) ;

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != string.Empty)
            {
                hdnDate.Value = txtFromDate.Text;
                if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hdnDate.Value)) | (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(hdnDate.Value)))
                {
                    objCommon.DisplayMessage(UPDROLE, "Please Select Date in Proper Range", this.Page);
                    txtToDate.Focus();
                    return;
                }
                if ((Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text)))
                {
                    objCommon.DisplayMessage(UPDROLE, "End Date should be greater than Start Date", this.Page);
                    txtToDate.Focus();
                    return;
                }
                else
                {
                    string Sessionname = txtName.Text.Trim();
                    bool IsActive;

                    if (hfdActive.Value == "true")
                    {
                        IsActive = true;
                    }
                    else
                    {
                        IsActive = false;
                    }
                    DateTime Session_SDate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Session_EDate = Convert.ToDateTime(txtToDate.Text);
                    //Check for add or edit
                    if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
                    {
                        //Edit 
                        int sessionid = Convert.ToInt32(Session["sessionid"]);
                        CustomStatus cs = (CustomStatus)objPhd.Add_Phd_Admission_Session(sessionid, Sessionname, Session_SDate, Session_EDate, IsActive);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            txtName.Text = string.Empty;
                            txtFromDate.Text = string.Empty;
                            txtToDate.Text = string.Empty;
                            BindListView();
                            Session["action"] = null;
                            objCommon.DisplayMessage(this.UPDROLE, "Record Updated sucessfully", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                        }
                    }

                    else
                    {
                        //Add New
                        int sessionid = 0;
                        CustomStatus cs = (CustomStatus)objPhd.Add_Phd_Admission_Session(sessionid, Sessionname, Session_SDate, Session_EDate, IsActive);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.UPDROLE, "Record Added sucessfully", this.Page);
                            txtName.Text = string.Empty;
                            txtFromDate.Text = string.Empty;
                            txtToDate.Text = string.Empty;
                            BindListView();
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(UPDROLE, "Please Enter Date", this.Page);
                return;
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}