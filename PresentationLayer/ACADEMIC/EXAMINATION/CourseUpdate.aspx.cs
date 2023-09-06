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
using System.Net;
using System.IO;
using Dns = System.Net.Dns;
using AddressFamily = System.Net.Sockets.AddressFamily;
public partial class ACADEMIC_EXAMINATION_CourseUpdate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
    string ipaddress = string.Empty;
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

                //Populate all the DropDownLists
                FillDropDown();
                //ViewState["ipAddress"] = GetUserIPAddress();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                ViewState["ipAddress"] = ip.AddressList[0].ToString();
                //string ipaddress = GetUserIPAddress();
                //ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                //ipaddress = Session["ipAddress"].ToString();
            }
            divMsg.InnerHtml = string.Empty;
        }

    }
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter,
                                                          System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];

        }
        return User_IPAddress;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseUpdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseUpdate.aspx");
        }
    }
    public void FillDropDown()
    {
        //fill semester
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        btnSubmit.Enabled = false;
        btnCancel.Enabled = false;
    }
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlStatus = e.Item.FindControl("ddlStatus") as DropDownList;
        DropDownList ddlSession = e.Item.FindControl("ddlSession") as DropDownList;
        DropDownList ddlSemes = e.Item.FindControl("ddlsempro") as DropDownList;
        Label lblCourse = e.Item.FindControl("lblcourse") as Label;
        //Label lbl1 = e.Item.FindControl("Label1") as Label;
        //Label lbl2 = e.Item.FindControl("Label2") as Label;
        //Label lbl3 = e.Item.FindControl("Label3") as Label;
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO");
        objCommon.FillDropDownList(ddlSemes, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        //string session = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DBO.FN_DESC('SESSIONNAME',SESSIONNO)SESSION", "ROLL_NO ='" + txtRollNo.Text + "' AND COURSENO =" + Convert.ToInt32(lblCourse.ToolTip));
        //ddlSession.SelectedItem.Text = session;
        int session =Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "SESSIONNO", "ROLL_NO ='" + txtRollNo.Text + "' AND COURSENO =" + Convert.ToInt32(lblCourse.ToolTip)));
        ddlSession.SelectedValue = session.ToString(); ;
        int status = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "PREV_STATUS", "ROLL_NO ='" + txtRollNo.Text + "' AND COURSENO =" + Convert.ToInt32(lblCourse.ToolTip)));
        ddlStatus.SelectedValue = status.ToString();
        //string semester = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNO", "ROLL_NO ='" + txtRollNo.Text + "' AND COURSENO =" + Convert.ToInt32(lblCourse.ToolTip));
        //ddlSemes.SelectedItem.Text = semester;
        int semester = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "SEMESTERNO", "ROLL_NO ='" + txtRollNo.Text + "' AND COURSENO =" + Convert.ToInt32(lblCourse.ToolTip)));
        ddlSemes.SelectedValue = semester.ToString();
        //lbl1.ForeColor = System.Drawing.Color.Red;
        //lbl2.ForeColor = System.Drawing.Color.Red;
        // lbl3.ForeColor = System.Drawing.Color.Red;

        //lbl2.ForeColor = System.Drawing.Color.Red;
        // lbl3.ForeColor = System.Drawing.Color.Red;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CourseController objCourse = new CourseController();
        foreach (ListViewDataItem lv in lvStudents.Items)
        {
            Label lblCourse = lv.FindControl("lblcourse") as Label;
            DropDownList ddlStatus = lv.FindControl("ddlStatus") as DropDownList;
            DropDownList ddlSession = lv.FindControl("ddlSession") as DropDownList;
            DropDownList ddlSemes = lv.FindControl("ddlsempro") as DropDownList;
            //HiddenField oldstatus = lv.FindControl("hdfstatus") as HiddenField;
            //HiddenField oldsession = lv.FindControl("hdfsession") as HiddenField;
            //HiddenField oldsem = lv.FindControl("hdfsempro") as HiddenField;
            Label lbloldstatus = lv.FindControl("lblstatus") as Label;
            Label lbloldsession = lv.FindControl("lblsession") as Label;
            Label lbloldsemester = lv.FindControl("lblsemestr") as Label;
            Label lbloldgrade = lv.FindControl("lblOldgrade") as Label;
            TextBox txtnewgrade = lv.FindControl("txtNewGrade") as TextBox;
            int UANO = Convert.ToInt32(Session["userno"]);
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text + "' AND ROLLNO IS NOT NULL"));
            string oldstat = Convert.ToString(lbloldstatus.Text);
            int oldsess = Convert.ToInt32(lbloldsession.Text);
            int oldsemester = Convert.ToInt32(lbloldsemester.Text);
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO");
            //objCommon.FillDropDownList(ddlSemes, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            //if (ddlStatus.SelectedIndex > 0 && ddlSession.SelectedIndex > 0 && ddlSemes.SelectedIndex > 0)
            //{ 
            int a = objCourse.UpdateCoursedetails(txtRollNo.Text, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemes.SelectedValue), UANO, IDNO, ViewState["ipAddress"].ToString(), oldsemester, oldsess, oldstat, lbloldgrade.Text, txtnewgrade.Text);
            if (a == 1)
            {
                objCommon.DisplayMessage("Result Saved Successfully!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage("Record Not Saved !!", this.Page);
            }
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Please Select Status,Session & Semester", this.Page);
            //}
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtRollNo.Text != "")
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                DataSet ds = objCourse.GetCourseDetails(txtRollNo.Text, Convert.ToInt32(ddlSemester.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlStud.Visible = true;
                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();
                    btnSubmit.Enabled = true;
                    btnCancel.Enabled = true;
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Semester!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Insert Rollno!!", this.Page);
        }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        //MarksEntryController objMarkEntryContr = new MarksEntryController();
        //foreach (ListViewDataItem lv in lvStudents.Items)
        //{
        //    Label lblCourse = lv.FindControl("lblcourse") as Label;
        //    DropDownList ddlStatus = lv.FindControl("ddlStatus") as DropDownList;
        //    DropDownList ddlSession = lv.FindControl("ddlSession") as DropDownList;
        //    DropDownList ddlSemes = lv.FindControl("ddlsempro") as DropDownList;
        //    //HiddenField oldstatus = lv.FindControl("hdfstatus") as HiddenField;
        //    //HiddenField oldsession = lv.FindControl("hdfsession") as HiddenField;
        //    //HiddenField oldsem = lv.FindControl("hdfsempro") as HiddenField;
        //    Label lbloldstatus = lv.FindControl("lblstatus") as Label;
        //    Label lbloldsession = lv.FindControl("lblsession") as Label;
        //    Label lbloldsemester = lv.FindControl("lblsemestr") as Label;
        //    Label lbloldgrade = lv.FindControl("lblOldgrade") as Label;
        //    TextBox txtnewgrade = lv.FindControl("txtNewGrade") as TextBox;
        //    int UANO = Convert.ToInt32(Session["userno"]);
        //    int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text + "' AND ROLLNO IS NOT NULL"));
        //    string oldstat = Convert.ToString(lbloldstatus.Text);
        //    int oldsess = Convert.ToInt32(lbloldsession.Text);
        //    int oldsemester = Convert.ToInt32(lbloldsemester.Text);

        //    if (txtnewgrade.Text != "")
        //    {
        //        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //        int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DISTINCT SCHEMENO", "IDNO=" + IDNO + " AND SEMESTERNO =" + ddlSemes.SelectedValue));

        //        int semesterno = Convert.ToInt32(ddlSemes.SelectedValue);

        //        //string idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO ='" + txtEnrollno.Text + "' AND ENROLLNO IS NOT NULL"));
        //        string ipAddress = Convert.ToString(ViewState["ipAddress"]);

        //        string ret2 = objMarkEntryContr.MarkEntryResultProcStudent(Sessionno, schemeno, semesterno, ipAddress, IDNO);
        //        if (ret2 == "1")
        //        {
        //            objCommon.DisplayMessage("Result Processed  Successfully", this.Page);

        //        }
        //        else
        //        {
        //            objCommon.DisplayMessage("Error in Result Processing", this.Page);
        //        }
        //    }
        //}
    }
}

