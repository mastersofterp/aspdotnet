//=================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : Academic
// PAGE NAME     : StudentMessFeedBackAns.aspx
// CREATION DATE : 25-04-2018
//=================================================================================
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
using System.Data.SqlClient;
public partial class ACADEMIC_HostelStudentAttendance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
    RoomAllotmentController objRAC = new RoomAllotmentController();


    #region Page Load
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                if (Session["usertype"].ToString() == "2")
                {

                    FillLabel();
                    if (hdhostteler.Value == "True")
                    {
                      string curdate= DateTime.Now.ToString("dd/MM/yyyy");
                      string count = objCommon.LookUp("ACD_HOSTEL_ONLINE_ATTENDANCE", "studidno=count(STUDID)", "studid="+ Session["idno"]+ " and CONVERT(nvarchar,ATT_DATE,103)=CONVERT(nvarchar,'" + curdate + "',103)");
                      if (Convert.ToInt16(count) > 0)
                      {
                          lblMsg.Visible = true;
                          lblMsg.Text = "Attendance Already Done";
                          lblMsg.ForeColor = System.Drawing.Color.Red;
                          pnlatt.Visible = false;
                      }
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Only for Hostteller Student";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        pnlatt.Visible = false;
                    }
                }
                else
                {
                    pnlSearch.Visible = true;
                    pnlStudInfo.Visible = false;
                }

            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentMessFeedBackAns.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentMessFeedBackAns.aspx");
        }
    }
    #endregion

    #region Private Methods
    private void FillLabel()
    {
        try
        {
            //Check for Activity On/Off

            Course objCourse = new Course();
            CourseController objCC = new CourseController();
            SqlDataReader dr = null;

            if (Session["usertype"].ToString() == "2")
            {
                dr = objCC.GetShemeSemesterByUser(Convert.ToInt32(Session["idno"]));
            }
            else
            {
                dr = objCC.GetShemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
            }

            if (dr != null)
            {
                if (dr.Read())
                {
                    int sessionno = 0;
                    lblregno.Text = dr["regno"] == null ? string.Empty : dr["regno"].ToString();
                    lblName.Text = dr["regno"] == null ? string.Empty : dr["regno"].ToString();
                    lblName.Text = lblName.Text  +"-"+ dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                   
                    lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();

                    sessionno = Convert.ToInt16(Session["currentsession"].ToString());

                    lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME ", " SESSIONNO = " + sessionno);

                    lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                    lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                    //lblSection.Text = dr["SECTIONNAME"] == null ? string.Empty : dr["SECTIONNAME"].ToString();
                    //lblSection.ToolTip = dr["SECTIONNO"] == null ? string.Empty : dr["SECTIONNO"].ToString();

                    //lblRegNo.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
                    lblhostel.Text = dr["HOSTELNAME"] == null ? string.Empty : dr["HOSTELNAME"].ToString();
                    lblhostel.ToolTip = dr["HOSTEL_NO"] == null ? string.Empty : dr["HOSTEL_NO"].ToString();
                    ViewState["ROOM_NO"] = dr["ROOM_NO"] == null ? string.Empty : dr["ROOM_NO"].ToString();
                    ViewState["HOSTEL_SESSION_NO"] = dr["HOSTEL_SESSION_NO"] == null ? string.Empty : dr["HOSTEL_SESSION_NO"].ToString();

                    string photo = objCommon.LookUp("ACD_STUD_PHOTO", "PHOTO", "IDNO = " + dr["IDNO"].ToString());
                    if (photo != string.Empty)
                    {
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                    }
                    else
                        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
                    hdhostteler.Value = dr["HOSTELER"] == null ? string.Empty : dr["HOSTELER"].ToString();
                }
            }

            if (dr != null) dr.Close();
            pnlStudInfo.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillLabel-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //private int FillAnswers()
    //{
    //    objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
    //    objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
    //    objSEB.Date = DateTime.Now;
    //    objSEB.CollegeCode = Session["colcode"].ToString();
    //    objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
    //    objSEB.FB_Status = true;
    //    objSEB.ACTYVITY_NO = Convert.ToInt16(lblactivity.Text);
    //    objSEB.Semesterno = Convert.ToInt16(lblSemester.ToolTip);
    //    objSEB.HBNO = Convert.ToInt16(lblhostel.ToolTip);
    //    objSEB.UA_NO = 0;
    //    int ret = objSFBC.AddStudentFeedBackAns(objSEB);
    //    return ret;
    //}
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string att_status = string.Empty;
            string remark = string.Empty;
            string hbno = string.Empty;
            string hostel_sessionno = string.Empty;
            string roomno = string.Empty;
            string Ipaddress = string.Empty;
            int college_code = Convert.ToInt16(Session["colcode"]);
            if (Rdbattendance.SelectedValue == "")
            {
                objCommon.DisplayUserMessage(updDetails, "Question must be select", this.Page);
                return;
            }

            if (Rdbattendance.SelectedValue == "2")
            {
                if (txtreason.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(updDetails, "Please enter reason", this.Page);
                    txtreason.Focus();
                    return;
                }
            }
            if (Session["usertype"].ToString() == "2")
            {
                if (Rdbattendance.SelectedValue == "1")
                {
                    att_status = "P";
                }
                else
                {
                    att_status = "A";
                    remark = txtreason.Text;
                }
                string idno = lblName.ToolTip;
               
                hbno = lblhostel.ToolTip;
                roomno = ViewState["ROOM_NO"].ToString();
                hostel_sessionno = ViewState["HOSTEL_SESSION_NO"].ToString();
                Ipaddress = Request.ServerVariables["REMOTE_HOST"];
                int output = objRAC.HostelAttendanceInsert(hostel_sessionno, hbno, roomno, idno, idno, att_status, remark, college_code, Ipaddress);
                if (output != -99)
                {
                    objCommon.DisplayUserMessage(updDetails, "Your Attendance Save Successfully.", this.Page);
                    pnlatt.Visible = false;
                    lblMsg.Visible = true;
                    lblMsg.Text = "Your Attendance Save Successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                    objCommon.DisplayUserMessage(updDetails, "Transaction Failed!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Rdbattendance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rdbattendance.SelectedValue == "2")
        {
            tdreason.Visible = true;
            txtreason.Visible = true;
        }
        else
        {
            tdreason.Visible = false;
            txtreason.Visible = false;
            txtreason.Text = "";
        }
    }
}
