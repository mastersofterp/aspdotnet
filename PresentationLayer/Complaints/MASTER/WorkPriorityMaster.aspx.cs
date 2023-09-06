//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : PRIORITY WORK MASTER                                                       
// CREATION DATE : 10-OCT-2017                                                        
// CREATED BY    : MRUNAL SINGH                                                   
// MODIFIED DATE :
// MODIFIED DESC :
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

public partial class Complaints_MASTER_WorkPriorityMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Complaint objCEnt = new Complaint();
    ComplaintController objCCon = new ComplaintController();

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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindListView();
                ViewState["action"] = "add";
            
            }
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_PRIORITY_WORK", "PWID", "PWNAME", "", "PWID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPriorityWork.DataSource = ds;
                lvPriorityWork.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_WorkPriorityMaster.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
   


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int pwId = int.Parse(btnEdit.CommandArgument);
            ViewState["PWID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetail(pwId);
            ViewState["action"] = "edit";          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_WorkPriorityMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int pwId)
    {
        DataSet ds = objCommon.FillDropDown("COMPLAINT_PRIORITY_WORK", "PWID", "PWNAME", "PWID = " + pwId, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["PWID"] = ds.Tables[0].Rows[0]["PWID"].ToString();
            txtWorkPriority.Text = ds.Tables[0].Rows[0]["PWNAME"].ToString();      
        }       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCEnt.PWID = 0;
            objCEnt.PWNAME = txtWorkPriority.Text.Trim();
           
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objCCon.AddUpdatePriorityWork(objCEnt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        BindListView();                       
                        objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["PWID"] != null)
                    {
                        objCEnt.PWID = Convert.ToInt32(ViewState["PWID"].ToString());

                        CustomStatus cs = (CustomStatus)objCCon.AddUpdatePriorityWork(objCEnt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            Clear();
                            BindListView();  
                            objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully.", this.Page);
                        }
                    }
                }
            }     
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_WorkPriorityMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
       

    //Clear Control 
    private void Clear()
    {
        txtWorkPriority.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["PWID"] = null;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }
}