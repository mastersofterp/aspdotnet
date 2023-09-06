using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class ACCOUNT_MainConfiguration : System.Web.UI.Page
{
    AccountPassingAuthority objPA = new AccountPassingAuthority();
    AccountPassingAuthorityController objPAController = new AccountPassingAuthorityController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

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

                objCommon.FillDropDownList(ddlAO, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)", "UA_FULLNAME");
                objCommon.FillDropDownList(ddlFO, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)", "UA_FULLNAME");
                //1=ADMIN
                //3=FACULTY
                //4=DEAN/REGISTRAR
                //5=NON TEACHING 
                //8=HOD

                int AO = Convert.ToInt32(objCommon.LookUp("ACC_MAIN_CONFIGURATION", "ISNULL(AO,0)", ""));
                int FO = Convert.ToInt32(objCommon.LookUp("ACC_MAIN_CONFIGURATION", "ISNULL(F0,0)", ""));

                if (AO > 0)
                {
                    ddlAO.SelectedValue = AO.ToString();
                }
                if (FO > 0)
                {
                    ddlFO.SelectedValue = FO.ToString();
                }
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

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ao = Convert.ToInt32(ddlAO.SelectedValue);
            int fo = Convert.ToInt32(ddlFO.SelectedValue);

            CustomStatus cs = (CustomStatus)objPAController.UpdateMainConfiguration(ao,fo);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                MessageBox("Record Updated Successfully");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_MainConfiguration.btnSubmit_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}