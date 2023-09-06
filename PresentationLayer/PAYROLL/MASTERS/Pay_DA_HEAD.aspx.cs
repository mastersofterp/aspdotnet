//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_DA_HEAD                                                  
// CREATION DATE : 13-Apr-2011
// CREATED BY    : Purva  Raut                                          
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_DA_HEAD : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpCreateController objStatus = new EmpCreateController();
    PayMaster objPay = new PayMaster();
    string UsrStatus = string.Empty;
    int OrganizationId;
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
                ViewState["action"] = "add";
                BindListView();
                OrganizationId = Convert.ToInt32(Session["OrgId"]);
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_DA_HEAD.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_DA_HEAD.aspx");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objStatus.GetDAHeadDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvheaddescription.DataSource = ds;
                lvheaddescription.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtdaheaddescription.Text == "")
        {
            MessageBox("Head Description is Mandatory");
        }
        else
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int DAHEADID = 0;
                    int collegeno = 0;
                    CustomStatus cs = (CustomStatus)objStatus.InsertDAHEADMaster(DAHEADID, txtdaheaddescription.Text.Trim(), collegeno, Convert.ToInt32(Session["userno"]),Convert.ToInt32(Session["OrgId"]));
                    if(cs.Equals(CustomStatus.RecordSaved))
                    {
               
                        MessageBox("Record Saved Successfully");
                        Clear();
                        BindListView();
                    }
                    else if(cs.Equals(CustomStatus.RecordExist))
                    {
                        MessageBox("Record is Already Exists");
                    }
                    else if(cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                    }
                }
                else if(ViewState["action"].ToString().Equals("edit"))               
                {
                    int collegeno = 0;
                    int DAHEADID = Convert.ToInt32(ViewState["lblFNO"]);
                    CustomStatus cs = (CustomStatus)objStatus.UpdateDAHEADMaste(DAHEADID, txtdaheaddescription.Text.Trim(), collegeno, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["OrgId"]));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
        }
    }

    public void Clear()
    {
        txtdaheaddescription.Text = "";
        ViewState["action"] = "add";
        ViewState["lblCNO"] = null;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblFNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblDAHeadDescription = lst.FindControl("lblDAHeadDescription") as Label;
            txtdaheaddescription.Text = lblDAHeadDescription.Text.ToString();
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void DataPager1_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
}