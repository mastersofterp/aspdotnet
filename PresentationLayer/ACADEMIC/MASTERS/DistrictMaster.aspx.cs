//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DISTRICT MASTER
// CREATION DATE : 11-AUG-2019
// CREATED BY    : SWAPNIL PRACHAND
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_MASTERS_DistrictMaster : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objMapping = new MappingController();
    string coll_code = string.Empty;
    int state = 0;
    string district = string.Empty;

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
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME ASC");
            }
             BindListView();
            ViewState["action"] = "add";
            //trQualLevel.Visible = false;
            //lblQuaLevel.Visible = false;
            //ddlQualification.Visible = false;
           // trQualLevel.Style.Add("display", "none");
        }
        divMsg.InnerHtml = string.Empty;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DistrictMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DistrictMaster.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            
            DataSet ds = objMapping.GetAllDistrict();
            lvDistrict.DataSource = ds;
            lvDistrict.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_QualifyMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try 
        {
            int districtno = 0;
           int flag = 0;
            coll_code = Session["colcode"].ToString();
            state = Convert.ToInt32(ddlState.SelectedValue);
            district = txtDistrict.Text.Trim();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                     flag = 1;
                     CustomStatus cs = (CustomStatus)objMapping.AddDistrict(districtno,district, state, coll_code, flag);
                     if (cs.Equals(CustomStatus.RecordSaved))
                     {
                         ViewState["action"] = "add";
                         Clear();
                         objCommon.DisplayMessage(this.updDistrict, "Record Saved Successfully!", this.Page);
                     }
                     else
                     {
                         ViewState["action"] = "add";
                         Clear();
                         objCommon.DisplayMessage(this.updDistrict, "Record Already Exist !", this.Page);
                     }
                   
                }
                else
                {
                    //Edit
                    if (ViewState["districtno"] != null)
                    {
                        

                        districtno = Convert.ToInt32(ViewState["districtno"].ToString());

                        CustomStatus cs = (CustomStatus)objMapping.AddDistrict(districtno,district, state, coll_code, flag);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updDistrict, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updDistrict, "Record Already Exist !", this.Page);
                        }

                    }
                }
               
            }
            
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_DistrictMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void Clear()
    {
        ddlState.SelectedIndex = 0;
        txtDistrict.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("DistrictMaster", "rptDistrictMaster.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +  ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updDistrict, this.updDistrict.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int districtno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(districtno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_DistrictMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        ViewState["action"] = "edit";
        

    }


    private void ShowDetail(int districtno)
    {

        SqlDataReader dr = objMapping.GetDistrictByNo(districtno);
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["districtno"] = districtno.ToString();
                txtDistrict.Text = dr["DISTRICTNAME"] == null ? string.Empty : dr["DISTRICTNAME"].ToString();

                ddlState.SelectedValue = dr["STATENO"] == null ? string.Empty : dr["STATENO"].ToString();
                
            }
        }
        if (dr != null) dr.Close();
    }
}