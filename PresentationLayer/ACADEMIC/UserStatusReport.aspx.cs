using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class Academic_UserStatusReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\INCLUDES\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\INCLUDES\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\INCLUDES\modalbox.js"));
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddladmbatch, "acd_admbatch", "BATCHNO", "BATCHNAME", "BATCHNO >0", "BATCHNO DESC");
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    //this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    //this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    //this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "");



                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        ViewState["ReceiptType"] = "AF";
                        ViewState["idno"] = Request.QueryString["id"].ToString();
                        //this.DisplayInformation(Convert.ToInt32(Request.QueryString["id"].ToString()));

                        divStudInfo.Visible = true;

                    }
                }

            }

            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntry.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntry.aspx");
        }
    }

    #endregion

    #region Search Student



    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Response.Redirect(url + "&id=" + lnk.CommandArgument);
    }


    #endregion

    #region Displaying Student's and His Existing Collection Information
    private void DisplayAllCount()
    {
        try
        {
            RefundController refundController = new RefundController();
            DataSet ds = refundController.GetActiveStudentCount(Convert.ToInt32(ddladmbatch.SelectedValue));
            int ST_TOT_COUNT = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvStudentCount.DataSource = ds;
                lvStudentCount.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentCount);//Set label -
                divStudentCount.Visible = true;
                //lvStudentCount.Items[0].FindControl("btnRefund").Focus();
                ST_TOT_COUNT = Convert.ToInt32(ds.Tables[0].Rows[0]["ST_TOT_COUNT"].ToString());

            }
            else
            {
                lvStudentCount.DataSource = null;
                lvStudentCount.DataBind();
                divStudentCount.Visible = false;
                objCommon.DisplayMessage(updBatch, "No record found .", this);

            }
            lblStudents.Text = "Total Students : " + ST_TOT_COUNT;
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    #endregion



    #region Refresh or Reload Page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }

    }
    #endregion

    protected void ddladmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        if (ddladmbatch.SelectedIndex > 0)
            DisplayAllCount();

    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
            int degree = Convert.ToInt32(sec_batch[0].ToString());
            int branchno = Convert.ToInt32(sec_batch[1].ToString());
            int ST_TOT_COUNT = 0;
            RefundController refundController = new RefundController();
            DataSet ds = refundController.GetActiveStudentDetail(Convert.ToInt32(ddladmbatch.SelectedValue), degree, branchno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentDetail.DataSource = ds;
                lvStudentDetail.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentDetail);//Set label -
                lvStudentDetail.Visible = true;
                divstudentdetail.Visible = true;
                divStudentCount.Visible = false;
                ST_TOT_COUNT = Convert.ToInt32(ds.Tables[0].Rows[0]["ST_TOT_COUNT"].ToString());
                lblStudents.Text = "Total Students : " + ST_TOT_COUNT;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkbtnstudentdetail_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int idno = Convert.ToInt32(lnk.CommandArgument.ToString());
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}