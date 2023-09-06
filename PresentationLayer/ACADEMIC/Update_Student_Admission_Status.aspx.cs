using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Update_Student_Admission_Status : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();

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
                    // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                int utyp = Convert.ToInt32(Session["usertype"].ToString());
                divMsg.InnerHtml = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
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
                Response.Redirect("~/notauthorized.aspx?page=Update_Student_Admission_Status.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Update_Student_Admission_Status.aspx.aspx");
        }
    }

    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            if (ddlAdmissionstatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage(Page, "Please select Admission Status", this.Page);
                return;
            }
            else 
            {
                objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
                objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
                objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
                objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = Session["ipAddress"].ToString().Trim();
                int AdmStatusId = Convert.ToInt32(ddlAdmissionstatus.SelectedValue);
                foreach (ListViewDataItem itm in lvStudentRecords.Items)
                {
                    CheckBox chk = itm.FindControl("chkAdmStatus") as CheckBox;
                    HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;
                    id = objS.IdNo;

                    if (chk.Checked.Equals(true) && chk.Enabled.Equals(true))
                    {

                        objS.IdNo = Convert.ToInt32(hdnf.Value);
                        flag = true;
                        if (flag.Equals(true))
                        {
                            output = studCont.AddAdmissionStatusForStudent(objS, AdmStatusId);
                        }

                        //BindListView();
                    }
                    if (chk.Checked)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(Page, "Please select at least one student", this.Page);
                    return;
                }
                else if (flag.Equals(true))
                {
                    objCommon.DisplayMessage(Page, "Student Status Updated Successfully", this.Page);
                    BindListView();
                }
            }
           
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void PopulateDropDownList()
    {

        try
        {
 
            objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0 AND ActiveStatus=1", "college_id");

            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");

            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");

            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");

            objCommon.FillDropDownList(ddlAdmissionstatus, "ACD_STUDENT_ADMISSION_STATUS", "STUDENT_ADMISSION_STATUS_ID", "STUDENT_ADMISSION_STATUS_DESCRIPTION", "", "");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
     
    protected void BindListView()
    {
        try
        {
            DataSet ds;
            ds = studCont.GetStudentListForAdmissionStatus(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlAcdYear.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divAdmissionStatus.Visible = true;
                btnsubmit1.Visible = true;
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();

                foreach (ListViewDataItem item in lvStudentRecords.Items)
                {


                }

                lvStudentRecords.Visible = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
                //objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label - 
            }

            else
            {
                objCommon.DisplayMessage(updtime, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        ddlDegree.Focus();
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
        ddlBranch.Focus();
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        ddlDegree.Focus();
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        ddlDegree.Focus();
        divAdmissionStatus.Visible = false;
        btnsubmit1.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
}