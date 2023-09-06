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
using System.Transactions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Itle_ITLE_Bulk_stud_log_creation : System.Web.UI.Page
{
    string Message = string.Empty;
    string UsrStatus = string.Empty;
    Common objCommon = new Common();
    //UserAuthorizationController objucc;
    AssignmentController objEC = new AssignmentController();

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

            fillSession();
            ViewState["EDIT"] = "";
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                

                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                
            }
        }
    }

    //get the access permission for the user
    //private string GetUserRight()
    //{
    //    objucc = new UserAuthorizationController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
    //    DataTable dtrights = objucc.GetMenuStatusUserwise(Convert.ToInt32(Session["idno"]), "Examination/" + objCommon.GetCurrentPageName());
    //    if (dtrights != null)
    //    {
    //        if (dtrights.Rows.Count > 0)
    //        {
    //            UsrStatus = dtrights.Rows[0]["STATUS"].ToString().Trim();
    //            return UsrStatus;

    //        }
    //        else
    //        {
    //            objCommon.DisplayUserMessage(updpanel, "Invalid Page Request!", this);
    //            return "INVALID";
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayUserMessage(updpanel, "Invalid Page Request!", this);
    //        return "INVALID";
    //    }

    //}

    /// <summary>
    /// filling session dropdownlist from cache if alredy loaded else fill from dataset
    /// </summary>
    protected void fillSession()
    {


            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                objCommon.FillDropDownList(Ddlsession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");
            }
            else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
            {

                objCommon.FillDropDownList(Ddlsession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

            }
        //DataSet dsSession = new DataSet();
        //dsSession = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO");
        //Ddlsession.Items.Clear();
        //Ddlsession.Items.Add("Please Select");
        //Ddlsession.SelectedItem.Value = "0";
        //Ddlsession.DataTextField = "SESSION_NAME";
        //Ddlsession.DataValueField = "SESSIONNO";
        //Ddlsession.DataSource = dsSession.Tables[0];
        //Ddlsession.DataBind();
        //Ddlsession.SelectedIndex = 0;
    }

    /// <summary>
    /// Getting Basic course Name from cache if filled alredy otherwise from database
    /// </summary>
    protected void getMainCourse()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        //DataSet ds = new DataSet();
        //ds = objCommon.FillDropDown("ACD_COURSE A INNER JOIN ACD_STUDENT_RESULT B ON A.COURSENO=B.COURSENO", "DISTINCT B.COURSENO", "A.COURSE_NAME", "B.SESSIONNO=" + Ddlsession.SelectedValue, "B.COURSENO");
        //if (ds != null && ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlMainCourse.Items.Clear();
        //    ddlMainCourse.Items.Add("Please Select");
        //    ddlMainCourse.SelectedItem.Value = "0";
        //    ddlMainCourse.DataTextField = "COURSE_NAME";
        //    ddlMainCourse.DataValueField = "COURSENO";
        //    //DataView dv = new DataView(ds.Tables[0], "CSRNO=" + ddlBasicCourse.SelectedValue, "", DataViewRowState.OriginalRows);
        //    ddlMainCourse.DataSource = ds.Tables[0];
        //    ddlMainCourse.DataBind();
        //    ddlMainCourse.SelectedIndex = 0;
        //}
    }

    //public void GetSubjectName()
    //{
    //    objCommon.FillDropDownList(ddlSubject, "Student_Result", "DISTINCT SDSRNO", "SUBJECTNAME", "COURSESRNO=" + ddlMainCourse.SelectedValue + " AND SESSIONNO =" + Ddlsession.SelectedValue, "SDSRNO");

    //    if (ddlSubject.Items.Count < 2)
    //    {
    //        objCommon.DisplayUserMessage(updpanel, "Subjects Not Available", this);
    //    }
    //    ddlSubject.Focus();
    //}

    //public void GetTeacher()
    //{
    //    DataSet ds = new DataSet();      
    //    ds = objCommon.FillDropDown("CCMS_LOGIN", "LOGINID", "USERFULLNAME", "UA_TYPE=" + 3, "USERFULLNAME");
    //    ddlTeacher.Items.Clear();
    //    ddlTeacher.Items.Add("Select");
    //    ddlTeacher.SelectedItem.Value = "0";
    //    ddlTeacher.DataTextField = "USERFULLNAME";
    //    ddlTeacher.DataValueField = "LOGINID";
    //    ddlTeacher.DataSource = ds;
    //    ddlTeacher.DataBind();
    //    ddlTeacher.SelectedIndex = 0;
    //}

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "LONGNAME");
                
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_courseAllot.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_courseAllot.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Ddlsession.SelectedIndex > 0)
        {
            getMainCourse();
        }

    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_Semester SC ON SR.SEmesterNO = SC.SEmesterNO", "DISTINCT SR.SEMESTERNO", "SC.SEMESTERNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue, "SR.SEMESTERNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindStudent();
            bindPages();
            //if (ddlBranch.SelectedValue == "99")
            //    //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON S.SECTIONNO = SC.SECTIONNO", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSem.SelectedValue + " AND SC.SECTIONNO > 0", "SECTIONNAME");
            //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
            //else
            //    //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON S.SECTIONNO = SC.SECTIONNO", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.BRANCHNO =" + ddlBranch.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSem.SelectedValue, "SECTIONNAME");
            //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
            //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue + " AND C.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");

            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void bindSubject()
    //{
    //    DataSet ds = objEC.GetSubjectTeacherDivisionWise(Convert.ToInt32(Ddlsession.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), Convert.ToInt32(ddlMainCourse.SelectedValue),ddlDivision.SelectedValue);
    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        pnlSubjectData.Visible = true;
    //        lstSubject.DataSource = ds.Tables[0];
    //        lstSubject.DataBind();

    //        hdnSession.Value=Ddlsession.SelectedValue;
    //        hdnCourse.Value=ddlMainCourse.SelectedValue;
    //        hdnDivision.Value=ddlDivision.SelectedValue;
    //        hdnSubjectType.Value=ddlSubjectType.SelectedValue;
    //        hdnSubject.Value=ddlSubject.SelectedValue;


    //    }
    //    else
    //    {
    //        pnlSubjectData.Visible = false;
    //    }
    //}

    private void bindStudent()
    {
        DataSet ds = objEC.GetStudentTeacherDivisionWise(Convert.ToInt32(Ddlsession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlStudGrid.Visible = true;
            LsvStudList.DataSource = ds.Tables[0];
            LsvStudList.DataBind();
        }
        else
        {
            pnlStudGrid.Visible = false;
        }
    }

    private void bindPages()
    {
        /////PASS MODULE ID OF ITLE
        DataSet ds = objEC.GetMenuItems(6);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlPages.Visible = true;
            lvPages.DataSource = ds.Tables[0];
            lvPages.DataBind();
        }
        else
        {
            pnlPages.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //string uRight = GetUserRight();
            //string TwoCharAdd = uRight.Substring(0, 2).ToString();
            //string TwoCharMod = uRight.Substring(2, 2).ToString();

            //if (uRight == "NANMNR")
            //{
            //    objCommon.DisplayMessage(updpanel, Common.Message.NoRights, this);
            //    return;
            //}
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromDays(1);
            TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress, transactionOptions, EnterpriseServicesInteropOption.Full);
            using (transactionScope)
            {
                if (Ddlsession.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage("Select the Session", this);
                    return;
                }
                if (ddlDegree.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage("Select the Degree", this);
                    return;
                }


                long ret = 0;

                if (ViewState["EDIT"].ToString() == "Edit")
                {
                    // ret = objEC.BulkStudLogin(Convert.ToInt32(Ddlsession.SelectedValue), Convert.ToInt32(ddlMainCourse.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue));

                }
                else
                {
                    string idno = string.Empty;
                    string pwd = "";
                    int i = 0;

                    foreach (ListViewItem items in LsvStudList.Items)
                    {
                        CheckBox chk = items.FindControl("chkSelect") as CheckBox;
                        Label lblRollNum = items.FindControl("lblRollNo") as Label;

                        if (chk.Checked == true)
                        {
                            pwd = pwd + "," + Common.EncryptPassword(lblRollNum.Text);
                            idno += lblRollNum.Text + ",";
                            i += 1;
                        }
                    }

                    //pwd = pwd.Remove(0, 1);
                    idno = idno.TrimEnd(',');

                    if (i == 0)
                    {
                        objCommon.DisplayMessage("Select the Students to allot the Teacher", this);
                        return;
                    }
                    /////////////////////////////
                    ////for page authorization
                    string modid = string.Empty;
                    //string pwd = "";
                    int j = 0;

                    foreach (ListViewItem items in lvPages.Items)
                    {
                        CheckBox chk = items.FindControl("chkSelect") as CheckBox;
                        Label lblpgnm = items.FindControl("lblPageName") as Label;

                        if (chk.Checked == true)
                        {
                            // pwd = pwd + "," + Common.EncryptPassword(lblRollNum.ToolTip);
                            modid += lblpgnm.ToolTip + ",";
                            j += 1;
                        }
                    }

                    //pwd = pwd.Remove(0, 1);
                    modid = modid.TrimEnd(',');

                    if (j == 0)
                    {
                        objCommon.DisplayMessage("Select the pages to allot", this);
                        return;
                    }
                    ///////

                    //string db = Session["DataBase"].ToString();
                    int moduleid = Convert.ToInt32(Session["ModuleId"]);
                    
                    ret = objEC.BulkStudLogin(idno, pwd, modid);
                }


                if (ret <= 0)
                {
                    if (Message.ToString().Trim() == "")
                    {
                        //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Alert);
                    }
                    else
                    {
                        //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                    }
                }
                else if (ret == 2)
                {
                    objCommon.DisplayMessage(UpdBulkLogin, "Logins Are Allready Created", this.Page);
                    //objCommon.DisplayMessage("Logins Are Allready Created", this);
                }

                else
                {
                    //System.Data.DataTable dt1 = objCommon.GetAllMenusCache();
                    //if ((dt1 != null) && (dt1.Rows.Count > 0))
                    //{
                    //    HttpRuntime.Cache.Insert("MenuMaster" + Session["DataBase"].ToString().Trim(), dt1, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

                    //}
                    objCommon.DisplayMessage(UpdBulkLogin, "Logins Are Successfully Created", this.Page);

                    //System.Windows.Forms.MessageBox.Show("Logins Are Successfully Created");
                    //objCommon.DisplayMessage("Logins Are Successfully Created", this);
                    //objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Saved, Common.MessageType.Success);

                    //bindStudent();
                    //bindSubject();
                    //ViewState["EDIT"] = "";
                    //transactionScope.Complete();

                }
            }
            Clear();
            pnlStudGrid.Visible = false;
            pnlPages.Visible = false;
        }
        catch (Exception ee)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    private void Clear()
    {
        Ddlsession.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlBranch.Items.Clear();
        ddlScheme.Items.Clear();
        ddlSem.Items.Clear();

        hdnSession.Value = "";
        hdnCourse.Value = "";
        hdnDivision.Value = "";
        hdnSubjectType.Value = "";
        hdnSubject.Value = "";
        hdnTeacher.Value = "";

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        

        pnlStudGrid.Visible = false;      
        LsvStudList.DataSource = null;
        LsvStudList.DataBind();

        pnlPages.Visible = false;
        lvPages.DataSource = null;
        lvPages.DataBind();

        ViewState["EDIT"] = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {


    }
}
