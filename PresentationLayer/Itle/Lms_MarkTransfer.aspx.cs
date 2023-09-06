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
using System.Linq;
using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Lms_MarkTransfer : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    AssignmentController objAM = new AssignmentController();
    Assignment objAssign = new Assignment();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                //Page Authorization
                //Page Authorization
                if (Session["Page"] == null)
                {
                   // CheckPageAuthorization();
                    Session["Page"] = 1;
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                FillDropdown();
               // GETEXAMTYPE();
                Session["RecTblExamMap"] = null;
              //  FILLDROPDOWN();
            }
        }

        // Used to get maximum size of file attachment
        txtSyllabus.Text = Session["ICourseName"].ToString();
        txtSyllabus.Enabled = false;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_SyllabusMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_SyllabusMaster.aspx");
        }
    }

    protected void FillDropdown()
    {
       objCommon.FillDropDownList(ddlexam, "ITLE_EXAM_MAPPING", "distinct SR_NO", "SUBEXAM_NAME,SUBEXAM_NO", "", "");

        //DataSet ds = objCommon.FillDropDown("ITLE_EXAM_MAPPING", "distinct SR_NO", "SUBEXAM_NAME,SUBEXAM_NO", "", "");

        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    ddlexam.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        //}
      
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        int SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
        int COURSENO = Convert.ToInt32(Session["ICourseNo"]);

        DataSet ds = objAM.GetExamMappingStudentList(SESSIONNO, COURSENO);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvstudentlist.DataSource = ds;
            lvstudentlist.DataBind();
        }

       
    }
    protected void btntransfer_Click(object sender, EventArgs e)
    {

        try
        {

            int ret = 0;

            int SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            int COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            int Exam_Id = Convert.ToInt32(ddlexam.SelectedValue);

            ret = objAM.TransferAssignmentMark(SESSIONNO, COURSENO, Exam_Id);


            ddlexam.SelectedIndex = 0;
            lvstudentlist.Visible = false;

            if (ret == 1)
            {
                objCommon.DisplayMessage("Assignment Mark Transfer Successful.", this);


            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}