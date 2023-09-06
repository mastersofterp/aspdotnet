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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ITLE_ITLEProcessPendingResult : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestResultController TRC = new ITestResultController();
    ITLETestController ObjTest = new ITLETestController();
    string _nitmn_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                Page.Title = Session["coll_name"].ToString();

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                FillDropdown();
            }
        }

    }
    private void FillDropdown()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACD_ITESTMASTER", "TESTNO", "TESTNAME", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND TEST_TYPE='O'", "TESTNO");


            ddlTest.Items.Clear();
            ddlTest.Items.Add("Please Select");
            ddlTest.SelectedItem.Value = "0";

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTest.DataSource = ds;
                ddlTest.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlTest.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlTest.DataBind();
                ddlTest.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {


        }
    }
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
    //        }
    //        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
    //    }
    //}

    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
            //pnllvCourseList.Visible = true;
        }
        catch (Exception ex)
        {


        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = ObjTest.GetPendingStudent(Convert.ToInt32(ddlTest.SelectedValue));
            if (ds != null)
            {
                lvRewsult.DataSource = ds;
                lvRewsult.DataBind();
            }
            else
            {
                lvRewsult.DataSource = null;
                lvRewsult.DataBind();
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    protected void btnProcessResult_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnProcessResult = sender as LinkButton;
            int idno = Convert.ToInt32(int.Parse(btnProcessResult.CommandArgument));
            int tdno = Convert.ToInt32(int.Parse(btnProcessResult.ToolTip));
            decimal totmarks;
            decimal correctMarks;
            int correctAns;
            if (Convert.ToInt32(ddlTest.SelectedValue) != 0)
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);
                string sql = "DELETE FROM ACD_ITEST_RESULT WHERE testno=" + Convert.ToInt32(ddlTest.SelectedValue) + " AND IDNO=" + idno + " AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]);
                objSqlHelper.ExecuteNonQuery(sql);
            }
            DataSet ds = ObjTest.GetCorrectAnswer(Convert.ToInt32(ddlTest.SelectedValue), idno, Convert.ToInt32(Session["ICourseNo"]));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                correctAns = Convert.ToInt32(ds.Tables[0].Rows[0]["COR_ANS_COUNT"]);
                totmarks = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalQuestion"]);
                correctMarks = Convert.ToDecimal(correctAns);

                int cs = Convert.ToInt32(ObjTest.AddIETestResult(Convert.ToInt32(tdno), Convert.ToInt32(totmarks), correctAns, totmarks, correctMarks, Convert.ToInt32(1), Convert.ToInt32(Session["ICourseNo"])));
                if (cs != -99)
                {

                    objCommon.DisplayMessage("Result Processed Sucessfully", this.Page);
                    BindListView();
                }


            }

        }
        catch (Exception ex)
        {


        }
    }
}