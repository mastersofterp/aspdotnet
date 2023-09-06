using System;
using System.Collections.Generic;
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

public partial class TRAININGANDPLACEMENT_Masters_TPWorkArea : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
    
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }                    
                    ViewState["action"] = "add";
                    BindListViewWorkArea();

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPWorkArea.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPWorkArea.aspx"); 
        }
    }
    private void BindListViewWorkArea()
    {
        try
        {
            DataSet ds = objTP.Getworkarea(0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvWorkarea.DataSource = ds;
                lvWorkarea.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.BindListViewWorkArea -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {            
            string colcode = Convert.ToString(Session["colcode"]);
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objTP.AddWorkArea(Convert.ToString(txtWorkarea.Text.Trim()), colcode);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        
                        BindListViewWorkArea();
                        ViewState["action"] = "add";
                        //Response.Redirect(Request.Url.ToString());
                        objCommon.DisplayMessage(updActivity,"Record Saved Successfully.",this.Page);
                        Clear();
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(updActivity, "Record Already Exists.", this.Page);
                        //Clear();
                    }

                }
                else
                {
                    if (ViewState["WORKAREANO"] != null)
                    {
                          int WNO= Convert.ToInt32(ViewState["WORKAREANO"].ToString());

                        CustomStatus cs = (CustomStatus)objTP.UpdateWorkArea(WNO,Convert.ToString(txtWorkarea.Text.Trim()));    
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewWorkArea();
                            ViewState["action"] = "add";                            
                            //Response.Redirect(Request.Url.ToString());
                            objCommon.DisplayMessage(updActivity, "Record Updated Successfully.", this.Page);
                            Clear();
                        }

                        // ADDED BY SUMIT-- 21092019//
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(updActivity, "Record Already Exists.", this.Page);
                            //Clear();
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear()
    {
        txtWorkarea.Text = string.Empty;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int WORKAREANO = int.Parse(btnEdit.CommandArgument);
            ViewState["WORKAREANO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(WORKAREANO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int WORKAREANO)
    {
        try
        {
            DataSet ds = objTP.Getworkarea(WORKAREANO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtWorkarea.Text = ds.Tables[0].Rows[0]["WORKAREANAME"].ToString();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


}
