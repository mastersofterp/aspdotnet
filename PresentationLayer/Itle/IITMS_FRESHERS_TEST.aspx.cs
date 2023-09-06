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
public partial class Itle_IITMS_FRESHERS_TEST : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IAnnouncementController objAC = new IAnnouncementController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
        string UserName = string.Empty;
        string IDNO = string.Empty;
        int UA_NO;

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


                Page.Title = Session["coll_name"].ToString();
               
                Label1.ForeColor = System.Drawing.Color.Blue;
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
        }


        UA_NO = Convert.ToInt32(Session["userno"].ToString());

        UserName = Session["userfullname"].ToString();
        lblLine.ForeColor = System.Drawing.Color.Red;

        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            //string[] STUDENT_ID = UserName.Split('@');
            //IDNO = STUDENT_ID[0].ToString();

            IDNO = (Session["idno"]).ToString();

           
        }
       
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ITestResultController objTest = new ITestResultController();

        Session["USERNAME"] = txtUserName.Text.Trim();
        Session["QUALIFICATION"] = txtQualification.Text.Trim();
        Session["EMAIL"] = txtEmail.Text.Trim();
        Session["ADDRESS"] = txtAddress.Text.Trim();
        Session["MOBILE_NO"] = txtMobileNo.Text.Trim();

        long cs = objTest.AddFresherInfo(Convert.ToInt32(Session["idno"]),Session["USERNAME"].ToString(), Session["QUALIFICATION"].ToString(), Session["EMAIL"].ToString(), Session["ADDRESS"].ToString(), Session["MOBILE_NO"].ToString());

        Response.Redirect("StudTest.aspx");

    }
}
