using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_StudentNameUpdation : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //fill dropdown
                PopulateDropDown();
                //get ip address
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -
            divMsg.InnerHtml = string.Empty;
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
                Response.Redirect("~/notauthorized.aspx?page=UpdateRegistrationNo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UpdateRegistrationNo.aspx");
        }
    }

    #endregion

    #region Form Events

    // clear all Items
    private void clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    #endregion

    #region Private Methods

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetStudentsRegNo_RollNo(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion



    // bind Degree 
    // protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    // {
    //objCommon.FillDropDownList(ddlDegree, "ACD_STUDENT A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO)", "DISTINCT(B.DEGREENO)", "B.DEGREENAME", "A.ADMBATCH =" + ddlAdmBatch.SelectedValue, "DEGREENAME");
    // }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=A.DEGREENO)", "DISTINCT(B.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID =" + ddlClgname.SelectedValue + "AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    // Bind Branch  
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID =" + ddlClgname.SelectedValue + " AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    // Show selected student 
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.DEGREENO=B.DEGREENO AND A.BRANCHNO=B.BRANCHNO", "distinct A.STUDNAME", "A.IDNO,A.REGNO,STUDENTMOBILE,SEX,FATHERNAME,FATHERMOBILE,MOTHERNAME,MOTHERMOBILE,EMAILID,FATHER_EMAIL,EMAILID_INS,MOTHER_EMAIL", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " AND A.DEGREENO =  " + ddlDegree.SelectedValue + " AND A.BRANCHNO =  " + ddlBranch.SelectedValue + "AND A.COLLEGE_ID =  " + ddlClgname.SelectedValue + " AND ISNULL(ADMCAN,0) = 0 and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue), "IDNO");
            if (dsStudent.Tables[0].Rows.Count > 0 && dsStudent != null && dsStudent.Tables.Count > 0)
            {
                lvStudents.Visible = true;

                lvStudents.DataSource = dsStudent.Tables[0];
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
                hdnTot.Value = dsStudent.Tables[0].Rows.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(updpnlStudent, "Student not Available for this Selection", this.Page);
                lvStudents.Visible = false;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // Generate Registration No.

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@UserName=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",IPADDRESS=" + ViewState["ipAddress"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    // registration No. Alllot Report
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("RollNo_Allotment_Report", "rptRollNoAllotment.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        StudentController objs = new StudentController();
        Button btnUpdate = sender as Button;
        int s = int.Parse(btnUpdate.CommandArgument);
        ListViewItem item = lvStudents.Items[s];
        TextBox txtName = (TextBox)item.FindControl("txtName");
        TextBox txtStudEmail = (TextBox)item.FindControl("txtStudEmail");
        TextBox txtStudMobile = (TextBox)item.FindControl("txtStudMobile");
        TextBox txtStudIndusEmail = (TextBox)item.FindControl("txtStudIndusEmail");
        RadioButtonList rdgender = (RadioButtonList)item.FindControl("rdgender");
        //txtStudIndusEmail.Visible = false;
        TextBox txtFMob = (TextBox)item.FindControl("txtFMob");
        TextBox txtFName = (TextBox)item.FindControl("txtFName");
        TextBox txtFEmail = (TextBox)item.FindControl("txtFEmail");
        TextBox txtMName = (TextBox)item.FindControl("txtMName");
        TextBox txtMMob = (TextBox)item.FindControl("txtMMob");
        TextBox txtMEmail = (TextBox)item.FindControl("txtMEmail");

        string name = txtName.Text.ToString();
        string StudEmail = txtStudEmail.Text.ToString();
        string StudMobile = txtStudMobile.Text.ToString();
        string StuGender = rdgender.SelectedValue.ToString();
        string Fname = txtFName.Text.ToString();
        string Mname = txtMName.Text.ToString();
        string StudIndusEmail = "";
        string FMob = txtFMob.Text.ToString();
        string FEmail = txtFEmail.Text.ToString();
        string MMob = txtMMob.Text.ToString();
        string MEmail = txtMEmail.Text.ToString();

        ////int cs = objs.UpdateStudentname(Convert.ToInt32(btnUpdate.ToolTip), name);
        int cs = objs.UpdateStudentname(Convert.ToInt32(btnUpdate.ToolTip), name, StudEmail, StudMobile, StudIndusEmail, StuGender, Fname, FMob, FEmail, Mname, MMob, MEmail);
        if (cs == 2)
        {
            objCommon.DisplayMessage(updpnlStudent, "Student Data Updated Successfully", this.Page);
            btnShow_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(updpnlStudent, "Server Error!", this.Page);
        }

    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue, "SM.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}
