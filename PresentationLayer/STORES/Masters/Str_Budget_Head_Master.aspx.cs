
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : STR_Budget_HeadNAME_MASTER.aspx                                                
// CREATION DATE : 05-march-2010                                                        
// CREATED BY    : chaitanya bhure                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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

public partial class Stores_Masters_STR_Budget_HeadNAME_MASTER : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindListViewBudgetNames();
                ViewState["action"] = "add";
                

            }
            //Set  Report Parameters
            objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "BudgetName_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }


    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_BUDGET_HEAD", " count(*)",  " BHNAME='" + Convert.ToString(txtBudName.Text) + "'"));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddBudgetHead_Name(txtBudName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            objCommon.DisplayMessage(updpnlMain, "Record Save Succesfully", this);
                            this.BindListViewBudgetNames();
                        }
                    }
                    else 
                    {
                        objCommon.DisplayMessage(updpnlMain,"Record Already Exist",this);
                    }
                }
                else
                {
                    if (ViewState["bhno"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_BUDGET_HEAD", " count(*)", " BHNAME='" + Convert.ToString(txtBudName.Text) + "' and bhno <> " + Convert.ToInt32(ViewState["bhno"].ToString())));
                         if (duplicateCkeck == 0)
                         {
                             CustomStatus csupd = (CustomStatus)objStrMaster.UpdateBudgetHead_Name(Convert.ToInt32(ViewState["bhno"].ToString()), txtBudName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                             if (csupd.Equals(CustomStatus.RecordUpdated))
                             {
                                 ViewState["action"] = "add";
                                 this.Clear();
                                 objCommon.DisplayMessage(updpnlMain, "Record Update Succesfully", this);
                                 this.BindListViewBudgetNames();
                             }
                        }
                         else 
                         {
                             objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                         }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_STR_Budget_HeadNAME_MASTER.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //this.Clear();
    }
    protected void btnshowrpt_Click(object sender, EventArgs e)
    {

    }

    protected void lvbudname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Clear()
    {
        txtBudName.Text = string.Empty;
        ViewState["action"] = "add";
    }
   void BindListViewBudgetNames()
    {
        
        try
        {
           

            DataSet ds = objStrMaster.GetAllBudgetHead_Name();
            lvbudname .DataSource = ds;
            lvbudname .DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_NewsPaper_Master.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   protected void btnEdit_Click(object sender, ImageClickEventArgs e)
   {
       try
       {
           ImageButton btnEdit = sender as ImageButton;
           ViewState["bhno"] = int.Parse(btnEdit.CommandArgument);
           ViewState["action"] = "edit";
           ShowEditDetailsBudgetName(Convert.ToInt32(ViewState["bhno"].ToString()));
       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Stores_Masters_STR_Budget_HeadNAME_MASTER.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server UnAvailable");
       }
   }
   private void ShowEditDetailsBudgetName(int bhno)
   {
       DataSet ds = null;

       try
       {
           ds = objStrMaster.GetSingleRecordBudgetHead_Name (bhno );
           if (ds.Tables[0].Rows.Count > 0)
           {
                   txtBudName.Text = ds.Tables[0].Rows[0]["BHNAME"].ToString();

           }
       }
       catch (Exception ex)
       {

           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_HeadName_Master.ShowEditDetailsBudget-> " + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server UnAvailable");

       }
       finally
       {
           ds.Clear();
           ds.Dispose();
       }

   }
   protected void lvbudname_PreRender(object sender, EventArgs e)
   {
       BindListViewBudgetNames();
   }
   protected void dpPager_PreRender(object sender, EventArgs e)
   {
       BindListViewBudgetNames();
   }
}
