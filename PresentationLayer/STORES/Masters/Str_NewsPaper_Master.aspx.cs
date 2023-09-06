//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_NewsPaper_Master.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class Stores_Masters_Str_NewsPaper_Master : System.Web.UI.Page
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
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.BindListViewNews();
                ViewState["action"] = "add";
                this.FillCity();
               
            }
            //Set  Report Parameters
            objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "NewsPaper_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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


    private void BindListViewNews()
    {
        try
        {
            DataSet ds = null; //objStrMaster.GetAllNewPaper();
            ds = objCommon.FillDropDown("STORE_NEWSPAPER A   INNER JOIN STORE_CITY B ON (A.CITYNO=B.CITYNO)", "NPNAME,A.CITYNO,B.CITY ", "NPNO", "", "");
            lvNewsPaper.DataSource = ds;
            lvNewsPaper.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_NewsPaper_Master.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_NEWSPAPER", " count(*)", "cityno= " + Convert.ToInt32(ddlcity.SelectedValue) + "and npname='" + Convert.ToString(txtNewsPaperName.Text.ToLower()) + "'"));

                    if (duplicateCkeck == 0)
                    {

                        CustomStatus cs = (CustomStatus)objStrMaster.AddNewsPaper(txtNewsPaperName.Text, Convert.ToInt32(ddlcity.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                            this.BindListViewNews();
                        }
                    }
                    else 
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Record Already exist", this);
                    }
                }
                else
                {
                    if (ViewState["npNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_NEWSPAPER", " count(*)", "cityno=" + Convert.ToInt32(ddlcity.SelectedValue) + "and npname= '" + Convert.ToString(txtNewsPaperName.Text) + "' and npNo <> " + Convert.ToInt32(ViewState["npNo"].ToString())));

                         if (duplicateCkeck == 0)
                         {

                             CustomStatus cs = (CustomStatus)objStrMaster.UpdateNewsPaper(txtNewsPaperName.Text, Convert.ToInt32(ddlcity.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ViewState["npNo"].ToString()), Session["userfullname"].ToString());
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {
                                 ViewState["action"] = "add";
                                 this.Clear();
                                 objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
                                 this.BindListViewNews();
                                 this.BindListViewNews();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(UpdatePanel1, "Record Already exist", this);
                         }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation"))
            {
                objCommon.DisplayMessage( "Record Already Exist",Page );
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_NewsPaper_Master.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["npNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsNewsPaper(Convert.ToInt32(ViewState["npNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_NewsPaper_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowEditDetailsNewsPaper(int npNo)
    {
        DataSet ds = null;

        try
        {
            //ds = objStrMaster.GetSingleNewPaper(npNo);
            ds = objCommon.FillDropDown("STORE_NEWSPAPER A INNER JOIN STORE_CITY B ON(A.CITYNO=B.CITYNO)", "NPNAME,A.CITYNO,B.CITY ", "NPNO", "NPNO=" + npNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                txtNewsPaperName.Text = ds.Tables[0].Rows[0]["NPNAME"].ToString();
                
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_NewsPaper_Master.ShowEditDetailsNewsPaper-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //Clear();
    }

    protected void Clear()
    {
        ddlcity.SelectedValue = "0";
        txtNewsPaperName.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewNews();
    }

    protected void FillCity()
    {
        try
        {   
            objCommon.FillDropDownList(ddlcity, "store_city", "cityno", "city", "", "city");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_NewsPaper_Master.FillCity-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
}
