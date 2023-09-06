//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Leave Mgt.                   
// CREATION DATE : 21-April-2015                                                          
// CREATED BY    : Swati Ghate                                                  
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

using IITMS.SQLServer.SQLDAL;

public partial class Comp_Off_Leave_ListNew : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    //ConnectionStrings
   // string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                  

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
                    int prevyr = System.DateTime.Today.AddYears(-1).Year;
                    int month = System.DateTime.Today.Month;
                    int year = System.DateTime.Today.Year;
                   
                    ShowDetails();

                }

               //Focus on From Date Textbox
              
             
                
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowDetails()
    {
        try
        {
            DataSet ds = objLeave.GetCompoffEmployeeList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpList.Visible = true;
                lvEmpList.DataSource = ds;
                lvEmpList.DataBind();
               // btnSave.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("No Employee for Comp-Off Leave...", this.Page);
                lvEmpList.DataSource = null;
                lvEmpList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comp_Off_Leave_List.ShowDetails ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    


    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
   
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnEdit.Enabled = false;
            btnUpdate.Enabled = true;
            if (lvEmpList.Items.Count > 0)
            {
                for (int i = 0; i < lvEmpList.Items.Count; i++)
                {
                    TextBox txtleave = lvEmpList.Items[i].FindControl("txtleave") as TextBox;
                    txtleave.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int ret = 0;
        if (lvEmpList.Items.Count > 0)
        {
            for (int i = 0; i < lvEmpList.Items.Count; i++)
            {
                TextBox txtleave = lvEmpList.Items[i].FindControl("txtleave") as TextBox;
                 Label lblidno = lvEmpList.Items[i].FindControl("lblidno") as Label;
                //txtleave.Enabled = true;
                 //ret=Convert.ToInt32( objLC.UpdateAttendance(Convert.ToInt32(lblidno.Text), Convert.ToDouble(txtleave.Text),Convert.ToDateTime(txtFromDate.Text),Convert.ToDateTime(txtToDate.Text)));
                                          
            }
        }
        if (ret==Convert.ToInt32(CustomStatus.RecordUpdated))
        {
            
            objCommon.DisplayMessage("Attendance Record Transfer Sucessfully", this);
            //btnReport.Visible = true;
            //btnReport.Enabled = true;
        }
        
    } 
   
 
}
