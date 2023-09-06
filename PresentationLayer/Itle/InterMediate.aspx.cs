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
using IITMS.NITPRM;


public partial class Itle_InterMediate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IAnnouncementController objAC = new IAnnouncementController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    public string sMarquee = string.Empty;
    public string fMarquee = string.Empty;
    public string Message = string.Empty;
   

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
        string UserName = string.Empty;
        string IDNO = string.Empty;
        int UA_NO;

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

               
                if (Session["usertype"].ToString().Equals("2"))
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["idno"].ToString() + "&type=STUDENT";
                }
                else if (Session["usertype"].ToString().Equals("3"))
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=EMP";

                }
                else
                {
                    imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";

                }


                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblUserName.Text = Session["userfullname"].ToString();
                lblUserName.ForeColor = System.Drawing.Color.Green;
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                Label1.ForeColor = System.Drawing.Color.Blue;               
            }

            ShowDetail();
            
        }


        UA_NO = Convert.ToInt32(Session["userno"].ToString());

        UserName = Session["userfullname"].ToString();


        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            //string[] STUDENT_ID = UserName.Split('@');
            //IDNO = STUDENT_ID[0].ToString();

            IDNO = (Session["idno"]).ToString();

            fMarquee = objAC.ScrollingFacultyNews(Request.ApplicationPath, IDNO);
            //lblFacultyAnnounce.Text = objNC.ScrollingFacultyNews(Request.ApplicationPath, IDNO);
            //sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
        }


        else if (Convert.ToInt32(Session["usertype"]) == 3)
        {



            pnlFaculty.Visible = false;
            //sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("workpage.aspx");  
    }


    // USED TO SHOW USERS INFORMATIONS
    private void ShowDetail()
    {
        try
        {
            string IDNO=string.Empty;
            CourseControlleritle objAC = new CourseControlleritle();


            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                //string[] STUDENT_ID = UserName.Split('@');
                //IDNO = STUDENT_ID[0].ToString();

                IDNO = (Session["idno"]).ToString();
                              
           }


            else if (Convert.ToInt32(Session["usertype"]) == 3)
            {

                IDNO = (Session["userno"]).ToString();
               
            }

            DataTableReader dtr = objAC.GetStudInfo(IDNO,Convert.ToInt32(Session["SessionNo"]));

            //Show Announcement Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblCountry.Text = dtr["COUNTRY"] == null ? "" : dtr["COUNTRY"].ToString();
                    lblCity.Text = dtr["CITY"] == null ? "" : dtr["CITY"].ToString();
                    lblRollNo.Text = dtr["ROLLNO"] == null ? "" : dtr["ROLLNO"].ToString();

                    lblFatherName.Text = dtr["FATHERNAME"] == null ? "" : dtr["FATHERNAME"].ToString();
                    lblMobileNo.Text = dtr["STUDENTMOBILE"] == null ? "" : dtr["STUDENTMOBILE"].ToString();
                    lblEmail.Text = dtr["EMAILID"] == null ? "" : dtr["EMAILID"].ToString();
                    lblCourseProfile.Text = dtr["COURSENAME"] == null ? "" : dtr["COURSENAME"].ToString();

                    //lblDivision.Text = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    //lblCurrdate.Text = dtr["STARTDATE"] == null ? "" : Convert.ToDateTime(dtr["STARTDATE"].ToString()).ToString("dd-MMM-yyyy");
                                        

                }

                while (dtr.Read())
                {
                    lblCourseProfile.Text += dtr["COURSENAME"].ToString() + ",";
                }


               

            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Itle_AnnouncementMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
}
