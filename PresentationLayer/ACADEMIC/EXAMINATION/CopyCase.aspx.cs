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
using System.Data;

public partial class ACADEMIC_EXAMINATION_CopyCase : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Student_Acd objStudent = new Student_Acd();
    StudentController objSC = new StudentController();

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

                    ViewState["action"] = "add";
            }
            PopulateDropDownList();

        }

        txtStudent.Focus();
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CopyCase.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CopyCase.aspx");
        }
    }

    // Populate DropDown List  
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlPunishment, "ACD_PUNISHMENT_EXAM WITH (NOLOCK)", "PUNISH_EXAMNO", "PUNISH_DESC", "PUNISH_EXAMNO > -1", "PUNISH_EXAMNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CopyCaseNew.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Show The Copy Case Details on Listview
    private void BindListView()
    {
        try
        {
            DataSet ds = objSC.GetCopyCaseData(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCopyCase.DataSource = ds;
                lvCopyCase.DataBind();
                ListHead.Visible = true;
            }
            else
            {
                lvCopyCase.DataSource = null;
                lvCopyCase.DataBind();
                ListHead.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CopyCaseNew.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // Edit Copy Case Record 
    protected void btnEditCopy_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int copyno = int.Parse(btnEdit.CommandArgument);
            ShowDetail(copyno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CopyCaseNew.btnEditCopy_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Show details of WithHeld Entry
    private void ShowDetail(int copyno)
    {
        SqlDataReader dr = objSC.GetCopyCaseDetailsbyId(copyno);
        //int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtStudent.Text + "' AND REGNO !='')"));
        //Show WithHeld Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["COPYNO"] = copyno.ToString();
                ddlRegistered.SelectedValue = dr["COURSENO"] == DBNull.Value ? "0" : dr["COURSENO"].ToString();
                ddlPunishment.SelectedValue = dr["PUNISH_EXAMNO"] == DBNull.Value ? "0" : dr["PUNISH_EXAMNO"].ToString();
                txtPunishment.Text = dr["PUNISHMENT"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            // int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtStudent.Text+"' AND REGNO !='')"));
            int idno = objSC.GetStudentIDByRegNo(txtStudent.Text.Trim());
            if (idno == 0)
            {
                lblDegreeName.Text = string.Empty;
                lblMsgD.Text = "Student Not Found!!";
                ddlSession.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                txtStudent.Text = string.Empty;
                lblSemester.Text = string.Empty;
                lblScheme.Text = string.Empty;
                lvCopyCase.DataSource = null;
                pnlStudInfo.Visible = false;
                ListHead.Visible = false;
                return;
            }
            else
            {
                ViewState["idno"] = idno;
                pnlStudInfo.Visible = true;
                ListHead.Visible = true;
                lblMsgD.Text = "";
                //bh1.Visible = true;
                //Show Student Data
                DataTableReader dtr = objSC.GetCopyCaseStudentDetails(idno, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));


                if (dtr.Read())
                {
                    lblDegreeName.Text = dtr["DEGREENAME"].ToString();
                    lblDegreeName.ToolTip = dtr["DEGREENO"].ToString();
                    lblBranch.Text = dtr["BRANCHNAME"].ToString();
                    //lblClass.Text = dtr["ROLL_NO"].ToString();
                    // lblSeatNo.Text = dtr["SEATNO"].ToString();
                    lblStudent.Text = dtr["STUDNAME"].ToString();
                    lblSemester.Text = dtr["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
                    lblScheme.Text = dtr["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dtr["SCHEMENO"].ToString();
                    //lblSection.Text = dtr["SECTIONNAME"].ToString();
                    //lblSection.ToolTip = dtr["SECTIONNO"].ToString();
                    txtStudent.ToolTip = dtr["IDNO"].ToString();
                    lblMsg.Text = string.Empty;
                    BindListView();
                }
                else
                {
                    ListHead.Visible = false;
                    pnlStudInfo.Visible = false;
                    lvCopyCase.DataSource = null;
                    lvCopyCase.DataBind();
                    objCommon.DisplayMessage(this.UpdatePanel1, "No Record Found For session : " + ddlSession.SelectedItem.Text + " Sem : " + ddlSem.SelectedItem.Text + " having Reg no. " + txtStudent.Text, this.Page);
                }
                dtr.Close();
            }
            objCommon.FillDropDownList(ddlRegistered, "ACD_STUDENT_RESULT SR WITH (NOLOCK),ACD_COURSE CS WITH (NOLOCK)", "SR.COURSENO", "CS.CCODE+'-'+CS.COURSE_NAME", "CS.COURSENO > 0 AND SR.COURSENO=CS.COURSENO AND SR.SEMESTERNO=CS.SEMESTERNO AND SR.CCODE=CS.CCODE AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.IDNO=" + Convert.ToInt32(idno), "CS.CCODE");
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CopyCaseNew.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objStudent.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        objStudent.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
        objStudent.DegreeNo = Convert.ToInt32(lblDegreeName.ToolTip.Trim());

        //if (lblSeatNo.Text == "")
        //{
        //    objStudent.Seatno = 0;
        //}
        //else
        //{
        //    objStudent.Seatno = Convert.ToInt32(lblSeatNo.Text.Trim());
        //}
        objStudent.StudName = lblStudent.Text.Trim();
        objStudent.SemesterNo = Convert.ToInt32(lblSemester.ToolTip.Trim());
        objStudent.SchemeNo = Convert.ToInt32(lblScheme.ToolTip.Trim());
        //objStudent.SectionNo = Convert.ToInt32(lblSection.ToolTip.Trim());
        objStudent.CourseName = ddlRegistered.SelectedItem.Text;
        objStudent.CourseNo = Convert.ToInt32(ddlRegistered.SelectedValue);
        objStudent.CCODE = objCommon.LookUp("acd_course", "ccode", "courseno=" + Convert.ToInt32(ddlRegistered.SelectedValue));
        objStudent.Punishment = txtPunishment.Text.Trim();
        objStudent.Punish_ExamNo = Convert.ToInt32(ddlPunishment.SelectedValue);
        objStudent.Allow_sessionno = Convert.ToInt32(ddlSession.SelectedValue) + Convert.ToInt32(ddlPunishment.SelectedValue);
        objStudent.UA_No = Convert.ToInt32(Session["userno"]);
        objStudent.Date = System.DateTime.Now.Date;

        if (ViewState["action"] != null)
        {
            //Add
            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (ViewState["COPYNO"] != null)
                {
                    objStudent.Copyno = Convert.ToInt32(ViewState["COPYNO"].ToString());

                    CustomStatus cs = (CustomStatus)objSC.UpadteCopyCaseStatus(objStudent);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Update successfully", this.Page);
                        BindListView();
                        ViewState["action"] = "add";
                        Clearall();

                    }
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objSC.AddCopyCase(objStudent);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "Record Save successfully", this.Page);
                    BindListView();
                    ViewState["action"] = "add";
                    Clearall();
                }

            }

        }
    }

    //Clear All Details
    private void Clearall()
    {
        ddlPunishment.SelectedIndex = 0;
        ddlRegistered.SelectedIndex = 0;
        txtPunishment.Text = string.Empty;
        ListHead.Visible = true;
    }
    private void ClearallControls()
    {
        ddlPunishment.SelectedIndex = 0;
        ddlRegistered.SelectedIndex = 0;
        txtPunishment.Text = string.Empty;
        lvCopyCase.DataSource = null;
        lvCopyCase.DataBind();
        ListHead.Visible = false;
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ListHead.Visible = false;
        Response.Redirect(Request.Url.ToString());
    }
 
}
