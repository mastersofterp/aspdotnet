//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BRANCH MASTER                                                        
// CREATION DATE : 14-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
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



public partial class Academic_Masters_Branch : System.Web.UI.UserControl
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
            }
            // degreename
            PopulateDropDownList();
            BindListView();
            ViewState["action"] = "add";
        }
        
        divMsg.InnerHtml = string.Empty;
        
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList( ddlDegreeName, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
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
            int branchno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(branchno);
            ViewState["action"] = "edit";

         }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchEntry.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BranchController objBC = new BranchController();
            Branch objBranch = new Branch();

            objBranch.ShortName = txtShortName.Text.Trim();
            objBranch.LongName = txtLongName.Text.Trim();
            objBranch.BranchNameInHindi = txtBranchName.Text.Trim();
            if (!txtIntake1.Text.Trim().Equals(string.Empty)) objBranch.Intake1 = Convert.ToInt32(txtIntake1.Text.Trim());
            if (!txtIntake2.Text.Trim().Equals(string.Empty)) objBranch.Intake2 = Convert.ToInt32(txtIntake2.Text.Trim());
            if (!txtIntake3.Text.Trim().Equals(string.Empty)) objBranch.Intake3 = Convert.ToInt32(txtIntake3.Text.Trim());
            if (!txtIntake4.Text.Trim().Equals(string.Empty)) objBranch.Intake4 = Convert.ToInt32(txtIntake4.Text.Trim());
            if (!txtIntake5.Text.Trim().Equals(string.Empty)) objBranch.Intake5 = Convert.ToInt32(txtIntake5.Text.Trim());
            objBranch.Duration = txtDuration.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(txtDuration.Text.Trim());
            objBranch.Code = txtCode.Text.Trim();
            objBranch.Ugpgpf = ddlEducation.SelectedItem.Text.Trim();
            objBranch.DegreeNo = Convert.ToInt32(ddlDegreeName.SelectedValue);
            objBranch.DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            objBranch.CollegeCode = Session["colcode"].ToString();
           
            //Check whether to add or update
           if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Branch Type
                  CustomStatus cs = (CustomStatus)objBC.AddBranchType(objBranch);
                  if (cs.Equals(CustomStatus.RecordSaved))
                  {
                      BindListView();
                      Clear();
                      objCommon.DisplayMessage( "Record Saved Succesfully", this.Page);
                  }
                  else
                  {
                       objCommon.DisplayMessage( "Record already exist", this.Page);
                  }
                }
                else
                {
                    //Edit
                    if (ViewState["branchno"] != null)
                    {
                        objBranch.BranchNo = Convert.ToInt32(ViewState["branchno"].ToString());

                        CustomStatus cs = (CustomStatus)objBC.UpdateBranchType(objBranch);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListView();
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage("Record Updated Succesfully", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage("Record already exist", this.Page);
                        }
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
       }
    }

    private void ShowDetail(int branchno)
    {
        BranchController objBC = new BranchController();
        SqlDataReader dr = objBC.GetBranchType(branchno);

        //Show Complaint type Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["branchno"] = branchno.ToString();
                ddlDegreeName.Text = dr["DEGREENO"] == DBNull.Value ? string.Empty : dr["DEGREENO"].ToString();
                ddlDept.Text = dr["DEPTNO"] == DBNull.Value ? string.Empty : dr["DEPTNO"].ToString();
                txtShortName.Text = dr["SHORTNAME"] == DBNull.Value ? string.Empty : dr["SHORTNAME"].ToString();
                txtLongName.Text = dr["LONGNAME"] == DBNull.Value ? string.Empty : dr["LONGNAME"].ToString();
                txtBranchName.Text = dr["BRANCHNM_HINDI"] == DBNull.Value ? string.Empty : dr["BRANCHNM_HINDI"].ToString();
                txtIntake1.Text = dr["INTAKE1"] == DBNull.Value ? string.Empty : dr["INTAKE1"].ToString();
                txtIntake2.Text = dr["INTAKE2"] == DBNull.Value ? string.Empty : dr["INTAKE2"].ToString();
                txtIntake3.Text = dr["INTAKE3"] == DBNull.Value ? string.Empty : dr["INTAKE3"].ToString();
                txtIntake4.Text = dr["INTAKE4"] == DBNull.Value ? string.Empty : dr["INTAKE4"].ToString();
                txtIntake5.Text = dr["INTAKE5"] == DBNull.Value ? string.Empty : dr["INTAKE5"].ToString();
                txtDuration.Text = dr["DURATION"] == DBNull.Value ? string.Empty : dr["DURATION"].ToString();
                txtCode.Text = dr["CODE"] == DBNull.Value ? string.Empty : dr["CODE"].ToString();
                ddlEducation.Text = dr["UGPGPF"] == DBNull.Value ? string.Empty : dr["UGPGPF"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        ddlDegreeName.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        txtShortName.Text = string.Empty;
        txtLongName.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        txtIntake1.Text = string.Empty;
        txtIntake2.Text = string.Empty;
        txtIntake3.Text = string.Empty;
        txtIntake4.Text = string.Empty;
        txtIntake5.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtCode.Text = string.Empty;
        ddlEducation.SelectedIndex = -1;
        //lblMsg.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            BranchController objBC = new BranchController();
            DataSet ds = objBC.GetAllBranchType();
             lvBranch.DataSource = ds;
             lvBranch.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpBranch_PreRender(object sender, EventArgs e)
    {
       //BindListView();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("BranchMaster", "rptBranchMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString()+",@P_DEGREENO="+ddlDegreeName.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
