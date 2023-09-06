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
using IITMS.NITPRM;

public partial class Itle_CreateEmailGroup : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    IMailController objMail = new IMailController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Menu mm = (Menu)Master.FindControl("mainMenu");
            mm.Visible = false;
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                // lblSession.Text = Session["SESSION_NAME"].ToString();
                // lblSession.ToolTip = Session["SessionNo"].ToString();
                //Load Page Help

                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                fldtemplate.Visible = false;

            }
            //BindListView();
            FillDropdown();
        }


    }

    private void FillDropdown()
    {
        DataSet ds = null;
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");
            }
            else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
            {

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

            }
            ds = objCommon.FillDropDown("ITLE_EMAIL_GROUP MG LEFT JOIN ACD_COURSE C ON (C.COURSENO=MG.COURSENO)", "DISTINCT MG.EGROUPNO", "MG.GROUPNAME,C.COURSE_NAME,MG.COURSENO,MG.SESSIONNO", "FACULTY_UANO =" + Session["userno"], "EGROUPNO DESC");
            lvGroupList.DataSource = ds;
            lvGroupList.DataBind();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=selectCourse.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=selectCourse.aspx");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                FillCourse();
                //objCommon.FillDropDownList(ddlMainCourse, "ACD_COURSE A INNER JOIN ACD_STUDENT_RESULT B ON A.COURSENO=B.COURSENO", "DISTINCT B.COURSENO", "A.COURSE_NAME", "B.SESSIONNO=" + ddlSession.SelectedValue, "B.COURSENO");
                // BindListView();
                // Session["SessionNo"] = Convert. ToInt32(ddlSession. SelectedValue);
                //Session["SESSION_NAME"] = ddlSession. SelectedItem. Text;
                //trSession. Visible = true;


            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "facultySendSms.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void FillCourse()
    {
        DataSet ds = null;
        if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
        {
            ds = objCourse.GetCourseByUaNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("Please Select");
            ddlCourse.SelectedItem.Value = "0";
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourse.DataSource = ds.Tables[0];
                ddlCourse.DataValueField = "courseno";
                ddlCourse.DataTextField = "coursename";
                ddlCourse.DataBind();
            }
        }
    }

    //protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    Label lblstudmobile;
    //    if (e. Item. ItemType == ListViewItemType. DataItem)
    //    {
    //        // Display the e-mail address in italics.
    //        lblstudmobile = (Label)e. Item. FindControl("lblstudmobile");

    //        if (string.IsNullOrEmpty(lblstudmobile.Text))
    //        {
    //            lblstudmobile. Text = "Not Available";
    //            lblstudmobile.ForeColor = System. Drawing. Color. Red;
    //        }
    //    }
    //}

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            //ds = objCommon. FillDropDown("acd_student_result a inner join  ACD_STUDENT b on(a.regno = b.REGNO)", "b.studname,b.studentmobile", "a.IDNO,a.regno", "a.SESSIONNO =" + Convert. ToInt16(ddlSession. SelectedValue)+"and a.COURSENO="+Convert.ToInt16(ddlCourse.SelectedValue), "a.IDNO");
            ds = objCommon.FillDropDown("ACD_STUDENT S  INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO) INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)INNER JOIN USER_ACC UA ON(R.IDNO=ua.UA_IDNO)", "DISTINCT ISNULL(S.STUDNAME,'')+'  '+ISNULL(S.STUDLASTNAME,'') AS  STUDNAME,(CASE WHEN S.STUDENTMOBILE IS NULL THEN 'N/A' ELSE S.STUDENTMOBILE END ) AS STUDENTMOBILE", "UA.UA_NO,ISNULL(R.REGNO,'') REGNO", "R.SESSIONNO =" + Convert.ToInt16(ddlSession.SelectedValue) + "AND (R.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR R.UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + "OR R.UA_NO_TUTR=" + Convert.ToInt32(Session["userno"]) + ") AND R.COURSENO=" + Convert.ToInt16(ddlCourse.SelectedValue) + "AND UA.UA_FIRSTLOG=" + 1 + "", "STUDNAME ASC");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lvStudents.Visible = true;
                fldtemplate.Visible = true;
            }

            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = true;
                fldtemplate.Visible = false;

                objCommon.DisplayMessage("Please Select the Student from the List", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "facultySendSms.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
        }
    }


    protected void btnreset_Click(object sender, EventArgs e)
    {

        ClearControls();
    }

    private void ClearControls()
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = true;
        fldtemplate.Visible = false;
        ddlSession.ClearSelection();
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add("Please Select");
        ddlCourse.SelectedItem.Value = "0";
        txtGroupName.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void btnCreateGroup_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = string.Empty;

            //Get the id's of the student to whom add in Mail Group
            foreach (ListViewDataItem dti in lvStudents.Items)
            {
                CheckBox chkSel = dti.FindControl("chkSelectMail") as CheckBox;

                if (chkSel.Checked)
                {
                    if (idno.Equals(string.Empty))
                        idno = chkSel.ToolTip;
                    else
                        idno = idno + "," + chkSel.ToolTip;
                }
            }

            if (idno.Length == 0)
            {
                objCommon.DisplayMessage(updpnl, "Please Select the Student from the List", this.Page);
                return;
            }
            else
            {
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {

                    CustomStatus cs = (CustomStatus)objMail.InsertEmailGroup(Convert.ToInt32(ViewState["GROUP_NO"]), Convert.ToInt32(Session["userno"].ToString()), idno, txtGroupName.Text.Trim(), Session["colcode"].ToString(), System.DateTime.Now, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["SessionNo"]));
                    FillDropdown();
                    objCommon.DisplayMessage(updpnl, "Group Updated Successfully", this.Page);
                    ClearControls();

                }
                else
                {
                    string groupName = objCommon.LookUp("ITLE_EMAIL_GROUP", "GROUPNAME", "GROUPNAME='" + txtGroupName.Text.Trim() + "'");
                    if (groupName == txtGroupName.Text)
                    {
                        objCommon.DisplayMessage(updpnl, "Group Name is Already Exist", this.Page);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objMail.InsertEmailGroup(0, Convert.ToInt32(Session["userno"].ToString()), idno, txtGroupName.Text.Trim(), Session["colcode"].ToString(), System.DateTime.Now, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["SessionNo"]));
                    FillDropdown();
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updpnl, "Group Created Successfully", this.Page);

                    }

                }
                ClearControls();


            }

        }


        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "CreateEmailGroup.btnCreateGroup_Click-> " + ex.Message + " " + ex.StackTrace);
        }
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        DataSet dsuano = null;
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int GROUP_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["GROUP_NO"] = GROUP_NO;
            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN  ACD_STUDENT B ON(A.REGNO = B.REGNO) INNER JOIN USER_ACC UA ON(A.IDNO=ua.UA_IDNO)", "B.STUDNAME", "UA.UA_NO,A.REGNO", "A.COURSENO=" + btnEdit.AlternateText, "A.REGNO");
            lvStudents.DataSource = ds;
            lvStudents.DataBind();
            lvStudents.Visible = true;
            fldtemplate.Visible = true;
            ViewState["action"] = "edit";

            dsuano = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN  ACD_STUDENT B ON(A.REGNO = B.REGNO) INNER JOIN USER_ACC UA ON(A.IDNO=ua.UA_IDNO) INNER JOIN ITLE_EMAIL_GROUP MG ON (UA.UA_NO=MG.STUD_UANO)", "DISTINCT B.STUDNAME,MG.SESSIONNO", "UA.UA_NO,A.REGNO,MG.COURSENO,MG.GROUPNAME", "MG.EGROUPNO =" + GROUP_NO + "AND MG.FACULTY_UANO=" + Convert.ToInt16(Session["userno"]), "A.REGNO");

            ddlSession.SelectedValue = dsuano.Tables[0].Rows[0]["SESSIONNO"].ToString();
            FillCourse();
            ddlCourse.SelectedValue = dsuano.Tables[0].Rows[0]["COURSENO"].ToString();
            txtGroupName.Text = dsuano.Tables[0].Rows[0]["GROUPNAME"].ToString();
            if (dsuano != null && dsuano.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < dsuano.Tables[0].Rows.Count; i++)
                {
                    foreach (ListViewDataItem lsvdata in lvStudents.Items)
                    {

                        CheckBox chkitem = lsvdata.FindControl("chkSelectMail") as CheckBox;

                        //LinkButton lnkResendSms = lsvdata.FindControl("lnkResendSms") as LinkButton;

                        if (chkitem.ToolTip.Equals(dsuano.Tables[0].Rows[i]["UA_NO"].ToString()))
                        {

                            chkitem.Checked = true;
                            //lnkResendSms.Text = Convert.ToInt32(ds.Tables[0].Rows[i]["SMS_STATUS"]) == 1 ? "" : "Resend SMS".ToString();

                        }


                    }
                }
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Dispose();
        }
    }

    private void ClearControl()
    {
        ddlSession.SelectedValue = "0";
        ddlCourse.SelectedValue = "0";

    }



    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int groupno = int.Parse(btnDel.CommandArgument);

            objCommon.DeleteClientTableRow("ITLE_EMAIL_GROUP", "EGROUPNO=" + groupno);

            objCommon.DisplayMessage(updpnl, "Group Deleted Successfully", this.Page);
            FillDropdown();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CreateEmailGroup.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
