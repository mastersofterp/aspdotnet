//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : DRC MASTER                             
// CREATION DATE : 28-FEB-2012                                                         
// CREATED BY    : ASHISH DHAKATE   
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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


public partial class ACADEMIC_MASTERS_DRCMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");
            }
            
            BindListView();
            ViewState["action"] = "add";
        }
       
        //divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SupervisorMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SupervisorMaster.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            PStaffController objBC = new PStaffController();
            PStaff objSupr = new PStaff();

            objSupr.DRCName = txtDRCName.Text.Trim();
            objSupr.DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            objSupr.CollegeCode = Session["colcode"].ToString();

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Phd Supervisoor
                    CustomStatus cs = (CustomStatus)objBC.AddDRCDetail(objSupr);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updBatch, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
                        //Label1.Text = "Record already exist";
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["drcno"] != null)
                    {
                        objSupr.DRCNo = Convert.ToInt32(ViewState["drcno"].ToString());

                        CustomStatus cs = (CustomStatus)objBC.UpdateDRCName(objSupr);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updBatch, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
                            //Label1.Text = "Record already exist";
                        }
                    }
                }

                BindListView();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_SupervisorMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int drcno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(drcno);
            string status = objCommon.LookUp("ACD_DRC_MEMBER", "DRCNO", "DRCNO=" + drcno);
            
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_SupervisorMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int drcno)
    {


        PStaffController objExam = new PStaffController();
        SqlDataReader dr = objExam.GetDRCNo(drcno);
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["drcno"] = drcno.ToString();
                txtDRCName.Text = dr["DRCNAME"] == null ? string.Empty : dr["DRCNAME"].ToString();
                ddlDept.Text = dr["DEPTNO"] == null ? string.Empty : dr["DEPTNO"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        ddlDept.SelectedIndex = 0;
        txtDRCName.Text = string.Empty;
        ddlType.SelectedIndex = 0;
        //Label1.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            PStaffController objBC = new PStaffController();
            DataSet ds = objBC.GetAllDRCName();
            lvDRCName.DataSource = ds;
            lvDRCName.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_SupervisorMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("DRCMaster", "rptDRCList.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_SupervisorMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}
