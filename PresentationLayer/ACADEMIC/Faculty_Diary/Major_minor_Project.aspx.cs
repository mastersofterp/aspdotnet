//=================================================================================
// PROJECT NAME  : RFC Common Code                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : Major_minor_Project.aspx.cs                                          
// CREATION DATE : 05/01/2024                                                
// CREATED BY    : Vipul Tichakule                             
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_Major_minor_Project : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Session objSession = new Session();
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();

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
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
             
                //if (Session["usertype"].ToString() == "3")
                //{
                //        //  ddlSession.Items.Clear();
                   
                         
                // }

              objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SESSION_MASTER C ON (CT.SESSIONNO=C.SESSIONNO)", "DISTINCT C.SESSIONNO", "C.SESSION_NAME", "C.SESSIONNO > 0 AND ISNULL(C.IS_ACTIVE,0)=1 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "C.SESSIONNO DESC");
              objCommon.FillDropDownList(ddlsessionNew, "ACD_SESSION_MASTER SM INNER JOIN  ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (SM.SESSIONNO = S.SESSIONNO) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SM.SESSION_NAME", "S.SESSIONNO > 0 AND ISNULL(SM.IS_ACTIVE,0)=1 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "S.SESSIONNO DESC");
                ddlSession.Focus();
                BindListView();

                divProject.Visible = false;
                divNewProject.Visible = false;

                ddlsessionNew.Enabled = true;
                ddlCollegeNew.Enabled = true;
                ddlDegreeNew.Enabled = true;
                ddlBranchNew.Enabled = true;
              
            }

        }

    }

    protected void BindListView()
    {
        DataSet ds =objTeachingPlanController.GetProjectTitleData();
        if (ds != null)
        {
            lvProject.DataSource = ds;
            lvProject.DataBind();

        }
        else
        {
            objCommon.DisplayMessage(this.UpdProjectMs, "Record not found", this.Page);  
        }
    }

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtProject.Text != string.Empty)
        {
            int clgcode = Convert.ToInt32(objCommon.LookUp("Reff", "College_code", "College_code >0"));
            objSession.ProjectName = txtProject.Text;

            if (hfdActive.Value == "true")
            {
                objSession.IsActive = true;
            }
            else
            {
                objSession.IsActive = false;
            }

            if (btnSubmit.Text.ToString().Equals("Submit"))
            {
                CustomStatus cs = (CustomStatus)objTeachingPlanController.InsertPeojectTitle(objSession, clgcode);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record Inerted Successfully", this.Page);
                    BindListView();
                    ClearControl();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record not inserted", this.Page);
                }
            }
            else
            {
                CustomStatus cs = (CustomStatus)objTeachingPlanController.UpdateProjectTitle(objSession, Convert.ToInt32(ViewState["ID"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record update Successfully", this.Page);
                    btnSubmit.Text = "Submit";
                    ClearControl();
                    BindListView();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record not update ", this.Page);
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.UpdProjectMs, "Please Enter Name Of Major / Minor Project", this.Page);
        }
        

    }
    protected void btn_editt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = int.Parse(btnEdit.CommandArgument);
            ViewState["ID"] = ID;
            ShowDetail(ID);
            ViewState["action"] = "edit";
           btnSubmit.Text= "Update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void ShowDetail(int ID)
    {
        SqlDataReader dr = objTeachingPlanController.GetProjectTitleData(ID);   
        if (dr != null)
        {
            if (dr.Read())
            {
                if (dr["PROJECT_TITLE"] == null | dr["PROJECT_TITLE"].ToString().Equals(""))
                    txtProject.Text = string.Empty;
                else
                    txtProject.Text = dr["PROJECT_TITLE"].ToString();

                if (dr["IS_ACTIVE"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }    
            }
            dr.Close();
        }      
    }

    protected void btnCancell_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    protected void ClearControl()
    {
        txtProject.Text = string.Empty;
        btnSubmit.Text = "Submit";
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_MASTER CM ON (CT.COLLEGE_ID=CM.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "CM.COLLEGE_NAME");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, " ACD_STUDENT S INNER JOIN  USER_ACC A ON (A.UA_NO = S.FAC_ADVISOR) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "A.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "D.DEGREENAME");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_STUDENT S INNER JOIN  USER_ACC A ON (A.UA_NO = S.FAC_ADVISOR) INNER JOIN ACD_BRANCH D ON (D.BRANCHNO=S.BRANCHNO)", "DISTINCT D.BRANCHNO", "D.LONGNAME", "A.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "D.LONGNAME");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlProject, "ACD_FD_PROJECT_TITLE_MASTER", "ID","Project_Title" ,"id > 0", "");
        }
    }

    protected void BindStudentListView()
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
        int degree = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);

        DataSet ds = objTeachingPlanController.BindStudData(sessionno, collegeid, degree, branchno);
        if (ds != null)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lvStudent.Visible = true;
            divProject.Visible = true;
            btnShow.Text = "Submit";

            DataSet dsCheck = objCommon.FillDropDown("ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT", "IDNO", "STUDNAME", "IDNO>0", "IDNO");
            Session["check"] = dsCheck;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chkstud = item.FindControl("ChkBox") as CheckBox;
                Label IDno = item.FindControl("lblIDno") as Label;

                for (int i = 0; i < dsCheck.Tables[0].Rows.Count; i++)
                {
                    if (IDno.Text == dsCheck.Tables[0].Rows[i]["IDNO"].ToString())
                    {

                        chkstud.Enabled = false;
                    }
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updStudent, "Record not found ", this.Page);
        }
    }


    protected void btnSubmitt_Click(object sender, EventArgs e)
    {       
        


        if (btnShow.Text.ToString().Equals("Show"))
        {

            BindStudentListView();
        }
        else
        {
            bool msg = false;
            int checkbox = 0;
            bool check = false;
            int result = 0;
            bool Exist = false;
           
            if (ddlProject.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updStudent, "Please select project", this.Page);
                return;
            }
            int Srno = 0;
            string Groupid =objCommon.LookUp("ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT", "MAX(SRNO)", "IDNO>0");
           
            if (Groupid != "")
            {
                Srno = Convert.ToInt32(Groupid);
                Srno = Srno + 1;
            }
            else {
                Srno = 1;
            }


            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chkstud = item.FindControl("ChkBox") as CheckBox;
                Label StudName = item.FindControl("lblName") as Label;
                Label Degreeno = item.FindControl("lblDegree") as Label;
                Label Branchno = item.FindControl("lblBranch") as Label;
                Label Session = item.FindControl("lblSession") as Label;
                Label IDno = item.FindControl("lblIDno") as Label;
                ViewState["checkbox"] = chkstud.Checked;
               objSession.ProjectName=ddlProject.SelectedItem.Text;

                


               if (chkstud.Checked == false && checkbox == 0)
                {
                        //objCommon.DisplayMessage(this.updStudent, "Please select checkbox", this.Page);
                    check = true;
                      
                }
               else if(chkstud.Checked == true)
                {
                    checkbox++;
                    check = false ;
                    CustomStatus cs = (CustomStatus)objTeachingPlanController.InsertProjectTitleData(objSession, Convert.ToInt32(IDno.Text), Convert.ToInt32(Session.Text), Convert.ToInt32(Degreeno.ToolTip), Convert.ToInt32(Branchno.ToolTip), Convert.ToInt32(ddlProject.SelectedValue), Srno);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        msg = true;
                        // objCommon.DisplayMessage(this.updStudent, "Record inserted succesfully", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Exist = true ;
                    }
                    else
                    {
                        msg = false;
                    }

                }

             
            }
            if (check == true)
            {
                objCommon.DisplayMessage(this.updStudent, "Please select checkbox", this.Page);
                return;
            }

            if (msg == true)
            {
                objCommon.DisplayMessage(this.updStudent, "Record inserted successfully", this.Page);             
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chkstud = item.FindControl("ChkBox") as CheckBox;
                    if (chkstud.Checked)
                    {
                        chkstud.Checked = false;
                    }
                }

                BindStudentListView();
               // ClearControll();
               
            }
            else if (Exist == true)
            {
                objCommon.DisplayMessage(this.UpdProjectMs, "Record Already Exist", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "Record not inserted", this.Page);
            }

        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControll();
        lvStudent.Visible = false;
        divProject.Visible = false;
        btnShow.Text = "Show";
    }

    protected void ClearControll()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlProject.SelectedIndex = 0;

        ddlsessionNew.SelectedIndex = 0;
        ddlCollegeNew.SelectedIndex = 0;
        ddlDegreeNew.SelectedIndex = 0;
        ddlDegreeNew.SelectedIndex = 0;
        ddlBranchNew.SelectedIndex = 0;
        ddlProejctNew.SelectedIndex = 0;
    }

    protected void ddlsessionNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsessionNew.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCollegeNew, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (CM.COLLEGE_ID = S.COLLEGE_ID) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.SESSIONNO=" + Convert.ToInt32(ddlsessionNew.SelectedValue), "CM.COLLEGE_NAME");
        }
    }
    protected void ddlCollegeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeNew.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegreeNew, " ACD_DEGREE D INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (D.DEGREENO= S.DEGREENO) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue), "D.DEGREENAME");
        }
    }
    protected void ddlDegreeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
          if (ddlDegreeNew.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranchNew, " ACD_BRANCH B INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegreeNew.SelectedValue), "B.LONGNAME");
        }
    }
    protected void ddlBranchNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranchNew.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlProejctNew, "ACD_FD_PROJECT_TITLE_MASTER", "ID", "Project_Title", "id > 0", "");
        }
    }



    protected void btnSubmitNew_Click(object sender, EventArgs e)
    {

        if (btnSubmitNew.Text.ToString().Equals("Show"))
        {
            
            BindProjectAssignStudentData();
            lvStudentAssign.Visible = true;
            divNewProject.Visible = true;
        }
        else if (btnSubmitNew.Text.ToString().Equals("Update"))
        {
            if (ddlProejctNew.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updEditAssignStudent, "Please select project", this.Page);
                return;
            }  

            int StudentId = Convert.ToInt32(ViewState["IDNO"].ToString());
            objSession.ProjectName=ddlProject.SelectedItem.Text;
            CustomStatus cs = (CustomStatus)objTeachingPlanController.UpdateAssignProjectData(objSession,StudentId,Convert.ToInt32(ddlProejctNew.SelectedValue));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(this.updEditAssignStudent, "Record update successfully", this.Page);
                BindProjectAssignStudentData();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this.updEditAssignStudent, "Record Already Exist", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updEditAssignStudent, "Record not update", this.Page);
            }
        }

    }

    protected void BindProjectAssignStudentData()
    { 
        
        int sessionno = Convert.ToInt32(ddlsessionNew.SelectedValue);
        int collegeid = Convert.ToInt32(ddlCollegeNew.SelectedValue);
        int degree = Convert.ToInt32(ddlDegreeNew.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranchNew.SelectedValue);

        DataSet ds = objTeachingPlanController.BindStudentAssignprojectData(sessionno, collegeid, degree, branchno);
        if (ds != null)
        {
            lvStudentAssign.DataSource = ds;
            lvStudentAssign.DataBind();
            divNewProject.Visible = true;
            //btnSubmitNew.Text = "Show";
            ClearControll();
        }
        else
        {
            objCommon.DisplayMessage(this.updEditAssignStudent, "Record not found ", this.Page);
        }
    }

    protected void btnCancelNew_Click(object sender, EventArgs e)
    {
        lvStudentAssign.Visible = false;
        divNewProject.Visible = false;

        ddlsessionNew.Enabled = true;
        ddlCollegeNew.Enabled = true;
        ddlDegreeNew.Enabled = true;
        ddlBranchNew.Enabled = true;

        btnSubmitNew.Text = "Show";
        ClearControll();
    }



    protected void btn_EditAssignStudent_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
          
            ImageButton btnEdit = sender as ImageButton;
            int studentId= int.Parse(btnEdit.CommandArgument);
            ViewState["IDNO"] = studentId;
            EditProjectAssignStudent(studentId);
            ViewState["action"] = "edit";
            btnSubmitNew.Text = "Update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void EditProjectAssignStudent(int studentId)
    {

        SqlDataReader dr = objTeachingPlanController.EditAssignDataOfStudent(studentId);
        if (dr != null)
        {
            if (dr.Read())
            {
                ddlsessionNew.SelectedValue = dr["SESSIONNO"].ToString();          
                ddlCollegeNew.SelectedValue = dr["COLLEGE_ID"].ToString();
                ddlDegreeNew.SelectedValue = dr["DEGREENO"].ToString();
                ddlBranchNew.SelectedValue = dr["BRANCHNO"].ToString();
                ddlProejctNew.SelectedValue = dr["PROJECT_ID"].ToString();

                ddlsessionNew.Enabled = false;
                ddlCollegeNew.Enabled = false;
                ddlDegreeNew.Enabled = false;
                ddlBranchNew.Enabled = false;

              
            }
            dr.Close();
        }      
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Records_Of_Major_Minor_Project", "Major_MinorProject_Details.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
        string title = objCommon.LookUp("ACD_FD_PROJECT_TITLE_MASTER", "PROJECT_TITLE", "id>0");
        if (title != null)
        {
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + clgcode; // ViewState["college_id"].ToString();
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updEditAssignStudent, this.updEditAssignStudent.GetType(), "controlJSScript", sb.ToString(), true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updEditAssignStudent, "Record not found ", this.Page);
        }
    }

}