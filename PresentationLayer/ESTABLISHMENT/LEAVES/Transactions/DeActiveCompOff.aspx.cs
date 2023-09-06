using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public partial class ESTABLISHMENT_LEAVES_Transactions_DeActiveCompOff : System.Web.UI.Page
{
    Common objCommon = new Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                BindListViewCompOffList();
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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    protected void BindListViewCompOffList()
    {
        try
        {
            DataSet ds = objApp.GetCreditedCompOff();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCompoff.DataSource = ds.Tables[0];
                lvCompoff.DataBind();
                lvCompoff.Visible = true;

            }
            else
            {
                lvCompoff.DataSource = null;
                lvCompoff.DataBind();
                lvCompoff.Visible = false;
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.BindListViewLeaveapplStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnLeaveCancel_Click(object sender, EventArgs e)
    {
       
        Button btnLeaveCancel = sender as Button;
        int ENO = int.Parse(btnLeaveCancel.CommandName);
        int IDNO = int.Parse(btnLeaveCancel.CommandArgument);
        ViewState["ENO"] = ENO;
        ViewState["IDNO"] = IDNO;

        CustomStatus cs = (CustomStatus)objApp.CompOffCancel(ENO, IDNO);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Deactivated Successfully");
            Clear();
            BindListViewCompOffList();
          
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void Clear()
    {
        ViewState["ENO"] = null;
        ViewState["IDNO"] = null;
    }
}