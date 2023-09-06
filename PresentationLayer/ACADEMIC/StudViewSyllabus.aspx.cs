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
using IITMS.UAIMS;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_StudViewSyllabus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    StudentController objSC = new StudentController();
    // CourseControlleritle objCourse = new CourseControlleritle();
    //string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    string PageId;

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                //if (Session["ICourseNo"] == null)
                //    Response.Redirect("selectCourse.aspx");

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    //if (CheckActivity())
                    //{
                    div2.Visible = true;
                    DivSyllabusList.Visible = true;
                    BindListView();
                    //}
                    // else
                    // {

                    // }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "you are not authorized to view this page.!!", this.Page);
                    div2.Visible = false;
                    div.Visible = false;
                    DivSyllabusList.Visible = false;
                    //divCourses.Visible = false;
                    return;
                    //Page Authorization
                    //   CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PageId = Request.QueryString["pageno"];

                    // temprary provision for current session using session variable [by defaullt value set 1 in db]
                    //lblSession.Text = Session["SESSION_NAME"].ToString();
                    //lblSession.ToolTip = Session["SessionNo"].ToString();
                    //lblCourseName.Text = Session["ICourseName"].ToString();

                    //lblSession.ForeColor = System.Drawing.Color.Green;
                    //lblCourseName.ForeColor = System.Drawing.Color.Green;
                    //BindTreeView();



                    // Used to insert id,date and courseno in Log_History table
                    // int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
                }
            }
        }
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudViewSyllabus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudViewSyllabus.aspx");
        }
    }

    private void BindListView()
    {
        // ISyllabus objSub = new ISyllabus();
        StudentController objSC = new StudentController();

        //objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
        //objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

        //objSub.UA_NO = Convert.ToInt32(Session["userno"]);
        // DataSet ds = objSC.GetSyllabusByUaNo(objSub);
        DataSet ds = objSC.GetUploadSyllabusByUaNo(Convert.ToInt32(Session["idno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvSyllabus.DataSource = ds;
            lvSyllabus.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSyllabus);//Set label - 
            DivSyllabusList.Visible = true;
        }
        else
        {
            lvSyllabus.DataSource = null;
            lvSyllabus.DataBind();
            DivSyllabusList.Visible = false;
            lblmsg.Text = "No uploaded syllabus found.";
        }
    }

    //private void BindTreeView()
    //{
    //    try
    //    {
    //        ISyllabus objSub = new ISyllabus();
    //        ISyllabusController objSC = new ISyllabusController();

    //        //objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
    //        //objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

    //        Fill_TreeViewSyllabus(objSub); 
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //private void Fill_TreeViewSyllabus(ISyllabus objSub)
    //{
    //    try
    //    {
    //        string lnksection = string.Empty;
    //        string lnkunit = string.Empty;
    //        TreeNode xx = null;
    //        TreeNode yy = null;
    //        TreeNode zz = null;
    //        TreeNode tt = null;


    //        DataTableReader dtr = objSC.GetSyllabusViewBySession(objSub);
    //        while (dtr.Read())
    //        {
    //            if (dtr["section"].ToString().Trim() != string.Empty && dtr["section"].ToString().Trim() != lnksection)                
    //            {
    //                xx = new TreeNode();
    //                xx.Text = dtr["section"].ToString();
    //               // tvSyllabus.Nodes.Add(xx);
    //                if (dtr["unit_name"].ToString().Trim() != string.Empty && dtr["unit_name"].ToString().Trim() != lnkunit)
    //                {
    //                    yy = new TreeNode();
    //                    yy.Text = dtr["unit_name"].ToString();
    //                    xx.ChildNodes.Add(yy);
    //                    if (dtr["topic_name"].ToString().Trim() != string.Empty)
    //                    {
    //                        zz = new TreeNode();
    //                        zz.Text = dtr["topic_name"].ToString();
    //                      //  zz.ToolTip = dtr["sub_no"].ToString();
    //                        yy.ChildNodes.Add(zz);
    //                    }
    //                }                   
    //            }
    //            else
    //            {
    //                if (dtr["section"].ToString().Trim() == string.Empty)
    //                {
    //                    xx = new TreeNode();
    //                    xx.Text = "-";
    //                    //tvSyllabus.Nodes.Add(xx);
    //                }
    //                if (dtr["unit_name"].ToString().Trim() != string.Empty && dtr["unit_name"].ToString().Trim() != lnkunit)
    //                {
    //                    yy = new TreeNode();
    //                    yy.Text = dtr["unit_name"].ToString();
    //                    xx.ChildNodes.Add(yy);
    //                    if (dtr["topic_name"].ToString().Trim() != string.Empty)
    //                    {
    //                        zz = new TreeNode();
    //                        zz.Text = dtr["topic_name"].ToString();
    //                      //  zz.ToolTip = dtr["sub_no"].ToString();
    //                        yy.ChildNodes.Add(zz);
    //                    }
    //                }
    //                else
    //                {
    //                    //yy = new TreeNode();
    //                    //yy.Text = dtr["unit_name"].ToString();
    //                    //xx.ChildNodes.Add(yy);
    //                    if (dtr["topic_name"].ToString().Trim() != string.Empty)
    //                    {
    //                        zz = new TreeNode();
    //                        zz.Text = dtr["topic_name"].ToString();
    //                       // zz.ToolTip = dtr["sub_no"].ToString();
    //                        yy.ChildNodes.Add(zz);
    //                    }
    //                }                
    //            }
    //            if (dtr["section"] != null & dtr["section"].ToString() != string.Empty)
    //                lnksection = dtr["section"].ToString();
    //            else
    //                lnksection = "-";
    //            if (dtr["unit_name"] != null & dtr["unit_name"].ToString() != string.Empty)
    //                lnkunit = dtr["unit_name"].ToString();
    //        }            
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Session["colcode"]; // ",COURSE_NAME=" + Session["ICourseName"] +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Page Events

    protected void btnViewSyllabus_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Itle_Syllabus", "Itle_View_Syllabus.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;
        string Filename = lnkbtn.ToolTip;
        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string file_path = MapPath("~/UploadSyllabus/");
        //string filePath = file_path + "UploadSyllabus\\" + "SYLLABUS_" + Convert.ToInt32(an_no) + System.IO.Path.GetExtension(fileName);
        string filePath = file_path + Filename;//+ System.IO.Path.GetExtension(fileName);
        Response.Redirect("DownloadAttachment.aspx?file=" + filePath + "&filename=" + fileName);

        //string filePath = file_path + "Itle/upload_files/SYLLABUS/" + "SYLLABUS_" + Convert.ToInt32(an_no);

        //HttpContext.Current.Response.ContentType = "Text/Doc";
        //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        //HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        //HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    #endregion

    #region Public Methods

    public string GetFileName(object filename, object announcemetnno)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/Syllabus/Syllabus_" + Convert.ToInt32(announcemetnno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/Syllabus_/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        if (status.ToString().Equals("Expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    #endregion

}
