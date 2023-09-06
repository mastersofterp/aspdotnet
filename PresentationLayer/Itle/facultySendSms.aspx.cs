using System;
using System. Collections;
using System. Configuration;
using System. Data;
using System. Linq;
using System. Web;
using System. Web. Security;
using System. Web. UI;
using System. Web. UI. HtmlControls;
using System. Web. UI. WebControls;
using System. Web. UI. WebControls. WebParts;
using System. Xml. Linq;
using IITMS;
using IITMS. UAIMS;
using IITMS. UAIMS. BusinessLayer. BusinessEntities;
using IITMS. UAIMS. BusinessLayer. BusinessLogic;
using IITMS. UAIMS. BusinessLogicLayer. BusinessLogic;
using IITMS.NITPRM;

public partial class facultySendSms : System. Web. UI. Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    IMailController objMail = new IMailController();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page. IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response. Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page. Title = Session["coll_name"]. ToString();
                // lblSession.Text = Session["SESSION_NAME"].ToString();
                // lblSession.ToolTip = Session["SessionNo"].ToString();
                //Load Page Help

                if (Request. QueryString["pageno"] != null)
                {
                    //lblHelp. Text = objCommon. GetPageHelp(int. Parse(Request. QueryString["pageno"]. ToString()));
                }
                fldtemplate. Visible = false;
                
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
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

            //objCommon. FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
            //ds = objCommon. FillDropDown("ACD_SESSION_MASTER", " Top 1 SESSION_NAME", "SESSIONNO", "SESSIONNO>0 AND FLOCK=1", "SESSIONNO DESC");
            //if (ds != null && ds. Tables[0]. Rows. Count > 0)
            //{
            //    ddlSession. SelectedValue = ds. Tables[0]. Rows[0]["SESSIONNO"]. ToString();
            //   // BindListView();

            //    Session["SessionNo"] = Convert. ToInt32(ddlSession. SelectedValue);
            //    Session["SESSION_NAME"] = ddlSession. SelectedItem. Text;
            //}


        }
        catch (Exception ex)
        {
            objUCommon. ShowError(Page, "selectCourse.FillDropdown-> " + ex. Message + " " + ex. StackTrace);
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request. QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response. Redirect("~/notauthorized.aspx?page=selectCourse.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response. Redirect("~/notauthorized.aspx?page=selectCourse.aspx");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            if (ddlSession. SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlMainCourse, "ACD_COURSE A INNER JOIN ACD_STUDENT_RESULT B ON A.COURSENO=B.COURSENO", "DISTINCT B.COURSENO", "A.COURSE_NAME", "B.SESSIONNO=" + ddlSession.SelectedValue, "B.COURSENO");
                // BindListView();
                // Session["SessionNo"] = Convert. ToInt32(ddlSession. SelectedValue);
                //Session["SESSION_NAME"] = ddlSession. SelectedItem. Text;
                //trSession. Visible = true;
              
                if (Convert. ToInt32(Session["usertype"]) == 3 || (Convert. ToInt32(Session["usertype"]) == 5))
                {
                    ds = objCourse. GetCourseByUaNo(Convert. ToInt32(ddlSession. SelectedValue), Convert. ToInt32(Session["userno"]), Convert. ToInt32(Session["usertype"]));
                  
                    if (ds != null && ds. Tables[0]. Rows. Count > 0)
                    {
                        ddlCourse. DataSource = ds .Tables[0];
                        ddlCourse. DataValueField = "courseno";
                        ddlCourse.DataTextField   = "coursename";
                        ddlCourse. DataBind();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            objUCommon. ShowError(Page, "facultySendSms.FillDropdown-> " + ex. Message + " " + ex. StackTrace);
        }
    }


    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblstudmobile;
        if (e. Item. ItemType == ListViewItemType. DataItem)
        {
            // Display the e-mail address in italics.
            lblstudmobile = (Label)e. Item. FindControl("lblstudmobile");
           
            if (string.IsNullOrEmpty(lblstudmobile.Text))
            {
                lblstudmobile. Text = "Not Available";
                lblstudmobile.ForeColor = System. Drawing. Color. Red;
            }
        }
    }





    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
           // ds = objCommon. FillDropDown("acd_student_result a inner join  ACD_STUDENT b on(a.regno = b.REGNO)", "b.studname,b.studentmobile", "a.IDNO,a.regno", "a.SESSIONNO =" + Convert. ToInt16(ddlSession. SelectedValue)+"and a.COURSENO="+Convert.ToInt16(ddlCourse.SelectedValue), "a.IDNO");

           // ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO) INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)", "DISTINCT ISNULL(S.STUDNAME,'') AS  STUDNAME,SEC.SECTIONNAME,(CASE WHEN S.STUDENTMOBILE IS NULL THEN 'N/A' ELSE S.STUDENTMOBILE END ) AS STUDENTMOBILE,(CASE WHEN S.EMAILID IS NULL THEN 'N/A' ELSE S.EMAILID END ) AS  EMAILID,S.REGNO", "S.IDNO", "R.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "  AND  (R.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR R.UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + "OR R.UA_NO_TUTR=" + Convert.ToInt32(Session["userno"]) + "OR R.AD_TEACHER_TH IN (SELECT VALUE FROM STRING_SPLIT ('" + Convert.ToInt32(Session["userno"]) + "',',')) OR  R.AD_TEACHER_PR IN (SELECT VALUE FROM STRING_SPLIT ('" + Convert.ToInt32(Session["userno"]) + "',','))) AND R.SESSIONNO='" + Convert.ToInt32(Session["SessionNo"]) + "' AND  (R.CANCEL IS NULL OR R.CANCEL = 0)", "REGNO ASC");

             ds = objCourse.GetStudentByCourse(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Session["userno"]),Convert.ToInt32(ddlCourse.SelectedValue));

            if (ds != null && ds. Tables[0]. Rows. Count > 0)
            {

                lvStudents. DataSource = ds;
                lvStudents. DataBind();
                fldtemplate.Visible = true;
                lvStudents. Visible = true;
                
            }

            else
            {
                lvStudents. DataSource = null;
                lvStudents. DataBind();
                lvStudents. Visible = true;
                fldtemplate. Visible = false;
            }
         
        
        }
        catch (Exception ex)
        {
            objUCommon. ShowError(Page, "facultySendSms.FillDropdown-> " + ex. Message + " " + ex. StackTrace);
        }
    }

    //send sms 
    protected void btnSendSms_Click(object sender, EventArgs e)
     {
        try
        {
            string mobileNo = string. Empty;
            if (string. IsNullOrEmpty(txtDesiTemp.Text))
            {
                 objCommon. DisplayMessage(this.updpnl, "PLease Insert Template Message!", this);
                return ;
            }
            if (txtDesiTemp. Text. Length < 133)
            {

                foreach (ListViewDataItem item in lvStudents. Items)
                {
                    Label lblstudmobile = item. FindControl("lblstudmobile") as Label;
                    CheckBox chk = item. FindControl("chkSelectMail") as CheckBox;

                    if (chk. Checked. Equals(true))
                    {

                       
                         if (mobileNo.Equals(string.Empty))
                         {
                             mobileNo += lblstudmobile.Text;
                         }
                        //if (!string.Equals(lblstudmobile.Text, "Not Available"))
                        else
                        {
                            mobileNo += lblstudmobile.Text + ",";

                        }
                       
                    }
                }

                CustomStatus cs = (CustomStatus)objMail. SendSmsByFaculty(txtDesiTemp. Text, mobileNo);

                if (cs. Equals(CustomStatus. RecordSaved))
                {
                     objCommon. DisplayMessage(this.updpnl, "Data Saved Successfully!", this.Page);
                }
                else
                {
                     objCommon. DisplayMessage(this.updpnl, "Sorry! Try again..", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

            objUCommon. ShowError(Page, "facultySendSms.FillDropdown-> " + ex. Message + " " + ex. StackTrace);
     
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        lvStudents. DataSource = null;
        lvStudents. DataBind();
        lvStudents. Visible = true;
        fldtemplate. Visible = false;
        ddlSession. ClearSelection();
        ddlCourse. ClearSelection();
        txtDesiTemp. Text = string. Empty;

    }
}
