using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
public partial class ACADEMIC_RedoCourseregisteredlist_report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    //ConnectionString
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
                PopulateDropDown();
            }
            divMsg.InnerHtml = string.Empty;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 22/12/2021
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 22/12/2021
        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RedoCourseregisteredlist_report.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RedoCourseregisteredlist_report.aspx.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Fill Dropdown Session 
            string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
            DataSet dsCollegeSession = objCC.GetCollegeSession(1, college_IDs);
            ddlCollege.Items.Clear();
            //ddlCollege.Items.Add("Please Select");
            ddlCollege.DataSource = dsCollegeSession;
            ddlCollege.DataValueField = "SESSIONNO";
            ddlCollege.DataTextField = "COLLEGE_SESSION";
            ddlCollege.DataBind();
          //  rdbReport.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (ddlCollege.Items.Count > 0)
        {
            DataSet ds;
            string sessionno = string.Empty;  //Convert.ToInt32(ddlCollege.SelectedValue);

            for (int k = 0; k < ddlCollege.Items.Count; k++)
            {
                if (ddlCollege.Items[k].Selected == true)
                    sessionno += ddlCollege.Items[k].Value + "$";
            }
            ds = objCC.GetredoRegistrationDetailsBySession(sessionno.TrimEnd('$'));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView gv = new GridView();
                gv.DataSource = ds;
                gv.DataBind();
                string attachment = "attachment ; filename=REDO_COURSE_REGISTRATION_LIST_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl_details, "Record Not Found", this.Page);
                return;
            }
           
        }
        else
        {
            objCommon.DisplayMessage(this.updpnl_details, "Please Select Session", this.Page);
            return;

        }
        
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}