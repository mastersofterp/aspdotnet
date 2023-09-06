//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SUDENT STRENGTH REPORT AND CATEGORY STRENGTH REPORT                             
// CREATION DATE : 27-JULY-2009                                                          
// CREATED BY    : MANGESH BARMATE                                                  
// MODIFIED DATE : 03-01-2022                                                                     
// MODIFIED DESC : Added dynamic header and label                                                                     
//======================================================================================

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
public partial class Academic_StudentLocalAddressLabel : System.Web.UI.Page
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
        try
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
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
                //Fill DropDown List
                PopulateDropDownList();
            }
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
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
        }
    }

    //Fill DropdownList
    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN BATCH
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "DEGREENAME");
            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            // FILL DROPDOWN COLLEGE            
            objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_DEGREENO=" + ddlDegree.SelectedValue 
                + ",@P_BRANCHNO=" + ddlBranch.SelectedValue 
                + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue 
                + ",@P_NAME=" + (rdoFatherName.Checked ? "2" : "1") 
                + ",@P_ADDRESS=" + (rdoLocalAddress.Checked ? "1" : "2")
                + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_COLLEGEID=" + Convert.ToInt32(ViewState["college_id"]);

            
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updRoom, this.updRoom.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("STUDENT_ADDRESS_INFO", "StudentLocalAddress.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            rdoStudentName.Checked = true;
            rdoLocalAddress.Checked = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FILL DROPDOWN BRANCH
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND DEGREENO ="+ ddlDegree.SelectedValue, "BRANCHNO");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.LONGNAME");
    }

    protected void ddlClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClg.SelectedValue != "0")
        {
            ViewState["college_id"] = Convert.ToInt32(ddlClg.SelectedValue).ToString();
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D ON(CD.DEGREENO=D.DEGREENO)", " DISTINCT CD.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue) + "AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "D.DEGREENAME");
        }
        else
        {
            ddlSemester.SelectedIndex = 0;

        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "SM.SEMESTERNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue, "SM.SEMESTERNAME");
        }
        else
        {
            ddlSemester.SelectedIndex = 0;

        }
    }
}
