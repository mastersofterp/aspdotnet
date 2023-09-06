//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYROLL                                                         
// PAGE NAME     : TO CREATE QUALIFICATION MASTERS                                 
// CREATION DATE : 01-MAY-2009                                                     
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Masters_qualificationMas : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "Server UnAvailable");
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
                // Populate DropDownList
                PopulateDropdown();
                //Bind the ListView with Qualification
                BindListViewQualification();

                ViewState["action"] = "add";

                //Set Report Parameters
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Masters" + "," + "rptQualification.rpt&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=qualificationMas.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=qualificationMas.aspx");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", string.Empty, "QUALILEVELNO");
            ddlQType.DataSource = ds;
            ddlQType.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlQType.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlQType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewQualification()
    {
        try
        {
            Masters objMasters = new Masters();
            DataSet dsQualification = objMasters.AllQualifications();

            if (dsQualification.Tables[0].Rows.Count > 0)
            {
                lvQualification.DataSource = dsQualification;
                lvQualification.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.BindListViewQualification-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlQType.SelectedIndex = 0;
        txtQualification.Text = string.Empty;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {
                Masters objMasters = new Masters();

                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objMasters.AddQualification(txtQualification.Text, Convert.ToInt32(ddlQType.SelectedValue), Session["colcode"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewQualification();
                        Clear();
                        lblStatus.Text = "Record Successfully Added";
                    }
                    else
                        lblStatus.Text = "Error";
                }
                else
                {
                    //Edit Qualification
                    if (ViewState["qualno"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objMasters.UpdateQualification(Convert.ToInt32(ViewState["qualno"]), txtQualification.Text, Convert.ToInt32(ddlQType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewQualification();
                            Clear();
                            lblStatus.Text = "Record Successfully Updated";
                        }
                        else
                            lblStatus.Text = "Error";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            //int qualno = int.Parse(btnEdit.CommandArgument);
            ViewState["qualno"] = int.Parse(btnEdit.CommandArgument);

            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblQLevelNo = lst.FindControl("lblQLevelNo") as Label;
            Label lblQuali = lst.FindControl("lblQuali") as Label;

            ddlQType.SelectedValue = lblQLevelNo.Text;
            txtQualification.Text = lblQuali.Text;
            ViewState["action"] = "edit";
            lblStatus.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewQualification();
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("Qualification Report", "rptQualification.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            //string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, UpdatePanel1.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}