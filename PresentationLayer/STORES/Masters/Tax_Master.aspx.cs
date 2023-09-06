//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Tax_Master.aspx                                                  
// CREATION DATE : 02-March-2010                                                       
// CREATED BY    : Chaitanya C. Bhure                                                         
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
using System.Data.SqlClient;

public partial class Stores_Masters_Tax_Master : System.Web.UI.Page
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
                this.BindListViewTax();
                ViewState["action"] = "add";
                //Set  Report Parameters
                objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Tax_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            }
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
                    CustomStatus cs = (CustomStatus)objStrMaster.AddTax(txtTaxName.Text, Convert.ToDouble(txtPercent.Text), Session["colcode"].ToString(), Session["userfullname"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        this.Clear();
                        BindListViewTax();
                        objCommon.DisplayMessage(updpnlMain, "Record Save Succesfully", this);
                    }
                }
                else
                {
                    if (ViewState["taxno"] != null)
                    {
                        CustomStatus csupd = (CustomStatus)objStrMaster.UpdateTax(Convert.ToInt32(ViewState["taxno"].ToString()), txtTaxName.Text, Convert.ToDouble(txtPercent.Text), Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (csupd.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewTax();
                            objCommon.DisplayMessage(updpnlMain, "Record UPdated Succesfully", this);
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_TAX_MASTER.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewTax()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllTax ();
            lvTaxMaster  .DataSource = ds;
            lvTaxMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Tax_Master.BindListViewTax-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsTAX(int TAXNO)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordTax(TAXNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtTaxName.Text = ds.Tables[0].Rows[0]["TAXNAME"].ToString();
                txtPercent.Text = ds.Tables[0].Rows[0]["TAXPER"].ToString();
            
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Tax_Master.ShowEditDetailsTAX-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
           ds.Clear();
            ds.Dispose();
        }

    }
    protected void lvTaxMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewTax();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["taxno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsTAX(Convert.ToInt32(ViewState["taxno"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Tax_Master_btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear()
    {

        txtTaxName.Text = string.Empty;
        txtPercent.Text = string.Empty;
        ViewState["action"] = "add";
    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
        this.Clear();
    }
}
