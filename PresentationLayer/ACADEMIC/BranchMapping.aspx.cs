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
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_BranchEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController  objBC = new MappingController();
    Branch objBranch = new Branch();

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
           // BindListView();
            ViewState["action"] = "add";
        }

        divMsg.InnerHtml = string.Empty;
        
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlBranchName, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "LONGNAME");
            objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlSection, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
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
        Response.Redirect(Request.Url.ToString());
    }
   
    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);
        int output = objBC.deletebranchRecord(srno);
        BindListView();
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(updGradeEntry, " Record Deleted Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(updGradeEntry, " Information Is Not Deleted ", this.Page);
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
            objBranch.BranchNo = Convert.ToInt32(ddlBranchName.SelectedValue);
            if (!txtIntake1.Text.Trim().Equals(string.Empty)) objBranch.Intake1 = Convert.ToInt32(txtIntake1.Text.Trim());
            if (!txtIntake2.Text.Trim().Equals(string.Empty)) objBranch.Intake2 = Convert.ToInt32(txtIntake2.Text.Trim());
            if (!txtIntake3.Text.Trim().Equals(string.Empty)) objBranch.Intake3 = Convert.ToInt32(txtIntake3.Text.Trim());
            if (!txtIntake4.Text.Trim().Equals(string.Empty)) objBranch.Intake4 = Convert.ToInt32(txtIntake4.Text.Trim());
            if (!txtIntake5.Text.Trim().Equals(string.Empty)) objBranch.Intake5 = Convert.ToInt32(txtIntake5.Text.Trim());
            objBranch.Duration = txtDuration.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(txtDuration.Text.Trim());
            objBranch.Code = txtCode.Text.Trim();
            objBranch.Ugpgot = Convert.ToInt32(ddlSection.SelectedValue);
            objBranch.DegreeNo = Convert.ToInt32(ddlDegreeName.SelectedValue);
            objBranch.DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            //objBranch.CollegeCode = "33"; //Commented by Irfan Shaikh on 20190413
            objBranch.CollegeCode = Session["colcode"].ToString();  //Added by Irfan Shaikh on 20190413
            objBranch.BranchCode = txtBrCode.Text.Trim().ToString();//Added by Irfan Shaikh on 20190413
            objBranch.CollegeID = Convert.ToInt32(ddlCollegeName.SelectedValue);

            if (chkEng.Checked == true)
            objBranch.EnggStatus = 1;
            else
            objBranch.EnggStatus = 0;

            objBranch.SCHOOL_COLLEGE_CODE = txtSchoolInstituteCode.Text;
            objBranch.AICTE_NONAICTE = Convert.ToInt32(ddlSchoolInstituteType.SelectedValue);

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
                        objCommon.DisplayMessage(updGradeEntry, "Record saved successfully", this.Page); 
                     //   lvBranch.DataSource = null;
                      //  lvBranch.DataBind();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updGradeEntry, "Record Already Exists", this.Page);
                    }
                }
                else if (ViewState["action"].ToString().Equals("Edit"))
                {
                    CustomStatus cs = (CustomStatus)objBC.EditbranchRecord(Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(txtDuration.Text), txtCode.Text, objBranch);
                     if (cs.Equals(CustomStatus.RecordUpdated))
                     {
                         BindListView();
                         Clear();
                         objCommon.DisplayMessage(updGradeEntry, "Record updated successfully", this.Page);
                        // lvBranch.DataSource = null;
                         lvBranch.DataBind();
                         ViewState["action"] = null; //Added by Irfan Shaikh on 20190413
                     }
                     else
                     {
                         objCommon.DisplayMessage(updGradeEntry, "Error...", this.Page);
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

    private void Clear()
    {
        ddlDegreeName.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlCollegeName.SelectedIndex = 0;
        ddlBranchName.SelectedIndex = 0;
        txtIntake1.Text = string.Empty;
        txtIntake2.Text = string.Empty;
        txtIntake3.Text = string.Empty;
        txtIntake4.Text = string.Empty;
        txtIntake5.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtCode.Text = string.Empty;
        txtBrCode.Text = string.Empty; //Added by Irfan Shaikh on 20190413        
        ddlSection.SelectedIndex = 0;
        ddlDegreeName.Enabled = true;
        ddlDept.Enabled = true;
        ddlCollegeName.Enabled = true;
        ddlBranchName.Enabled = true;
        txtSchoolInstituteCode.Text = string.Empty;
        ddlSchoolInstituteType.SelectedIndex = 0;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objBC.GetAllBranchType(Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlDegreeName.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBranch.Visible = true;
                lvBranch.DataSource = ds;
                lvBranch.DataBind();
            }
            else
            {
                lvBranch.Visible = false;
                lvBranch.DataSource = null;
                lvBranch.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlCollegeName.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updGradeEntry, "Please select College", this.Page);
            return;
        }
        //if(ddlDegreeName.SelectedValue == "0")
        //{
        //    objCommon.DisplayMessage(updGradeEntry, "Please select Degree", this.Page);
        //    return;
        //}
       // ShowReport("BranchMaster", "rptBranch_mapping.rpt");
        ShowReport("BranchMaster", "rptBranch_mapping_new.rpt");
        
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + "15" + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegreeName.SelectedValue);// +",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue); //Commented by Irfan Shaikh on 20190413
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(15) + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegreeName.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue) +",@P_COLLEGE_CODE =" + Convert.ToInt32(ddlCollegeName.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updGradeEntry, this.updGradeEntry.GetType(), "controlJSScript", sb.ToString(), true);
        }
        
        catch (Exception ex)
             {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
         BindListView();
        objCommon.FillDropDownList(ddlDegreeName, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEGREENO");
        objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ","D.DEPTNO" ,"D.DEPTNAME" ,"D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEPTNAME");

        if (ddlCollegeName.SelectedIndex == 0) // ADDED BY SUMIT-- 07-NOV-2019
        {
            ddlBranchName.SelectedIndex = 0;
        }
        
    }

    protected void ddlDegreeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;

        ViewState["action"] = "Edit";
        int srno = int.Parse(btnEdit.CommandArgument);
        ViewState["srno"] = srno;
        DataSet chkds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH", "*", "DEGREENO", "CDBNO=" + srno, string.Empty);
        if (chkds.Tables[0].Rows.Count > 0)
        {
            ddlCollegeName.SelectedValue = chkds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            ddlDegreeName.SelectedValue = chkds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ddlDept.SelectedValue = chkds.Tables[0].Rows[0]["DEPTNO"].ToString();
            ddlBranchName.SelectedValue = chkds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            txtCode.Text= chkds.Tables[0].Rows[0]["CODE"].ToString().Trim();
            txtBrCode.Text = chkds.Tables[0].Rows[0]["BRANCH_CODE"].ToString().Trim(); ////Added by Irfan Shaikh on 20190413
            txtDuration.Text=chkds.Tables[0].Rows[0]["DURATION"].ToString();
            //ddlEducation.SelectedValue = chkds.Tables[0].Rows[0]["branchname"].ToString();
            txtIntake1.Text = chkds.Tables[0].Rows[0]["INTAKE1"].ToString();
            txtIntake2.Text = chkds.Tables[0].Rows[0]["INTAKE2"].ToString();
            txtIntake3.Text = chkds.Tables[0].Rows[0]["INTAKE3"].ToString();
            txtIntake4.Text = chkds.Tables[0].Rows[0]["INTAKE4"].ToString();
            txtIntake5.Text = chkds.Tables[0].Rows[0]["INTAKE5"].ToString();
            ddlSection.SelectedValue = chkds.Tables[0].Rows[0]["UGPGOT"].ToString().Trim();
            ddlCollegeName.Enabled = false;
            ddlDegreeName.Enabled = false;

            //Added Mahesh Malve On Dated 16-02-2021
            txtSchoolInstituteCode.Text = Convert.ToString(chkds.Tables[0].Rows[0]["SCHOOL_COLLEGE_CODE"]);
            ddlSchoolInstituteType.SelectedValue = Convert.ToString(chkds.Tables[0].Rows[0]["AICTE_NONAICTE"]) == string.Empty ? "0" : chkds.Tables[0].Rows[0]["AICTE_NONAICTE"].ToString();

            if (chkds.Tables[0].Rows[0]["ENG_STATUS"].ToString() == "1")
            {
                chkEng.Checked = true;
            }
            else
            {
                chkEng.Checked = false;
            }
            //ddlDept.Enabled = false;
            //ddlBranchName.Enabled = false;
            //ddlEducation.Enabled = false;
        }
    }
}
