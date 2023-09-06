/* Author      : Zubair Ahmad
   Create Date : 06/05/2014
   Description : This form is used to Export(Save) Questions from 
                 Question Bank(DataBase) into Excel file.

*/
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
//using System.Transactions;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

public partial class Itle_Itle_Export_QuestionBank : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    IQuestionbankController objQues = new IQuestionbankController();

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

        try
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

                    lblSession.Text = Session["SESSION_NAME"].ToString();
                    lblSession.ToolTip = Session["SessionNo"].ToString();
                    lblSession.ForeColor = System.Drawing.Color.Green;
                    lblCorseName.ForeColor = System.Drawing.Color.Green;
                    lblCorseName.Text = Session["ICourseName"].ToString();
                    //PopulateDropdown();

                }
                FillDropdown();
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Export_QuestionBank.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #region "General"
    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT, ACD_COURSE C", "DISTINCT CT.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) AS COURSENAME", "CT.COURSENO = C.COURSENO  AND UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND CT.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "CT.COURSENO");
            //objCommon.FillDropDownList(ddlStudent, "ITLE_TESTRESULT TR, STUDENT_ADMISSION SA", "distinct TR.IDNO", "SA.FNAME + ' ' + SA.MNAME + ' ' + SA.LNAME as STUDNAME", "TR.IDNO=SA.STUDSRNO AND TR.IDNO>0", "STUDNAME");


        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "Itle_Export_QuestionBank.FillDropdown-> " + ex.Message + " " + ex.StackTrace);

        }
    }
   
    #endregion

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_Export_QuestionBank.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Export_QuestionBank.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
   
    protected void ddlCourse_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlTopic, "ACD_IQUESTIONBANK", "TOPIC AS TOPIC_ID", "(TOPIC +'       '+ '            (' + CONVERT(NVARCHAR,COUNT(QUESTIONNO))+' Ques.)') AS TOPIC_NAME", "COURSENO=" + ddlCourse.SelectedValue + "AND QUESTION_TYPE='O' GROUP BY TOPIC", "TOPIC_NAME");

        }
         catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Export_QuestionBank.ddlCourse_SelectedIndexChanged1-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void rbnQuestionOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbnQuestionOption.SelectedValue == "T")
        {
            trTopicName.Visible = true;
        }
        else 
        {
            trTopicName.Visible = false;
        }

    }

    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objQues.GetAllQuestionFromQB(Convert.ToInt32(ddlCourse.SelectedValue), ddlTopic.SelectedItem.Value.ToString());

            
            grdExportQuestions.DataSource = ds;
            grdExportQuestions.DataBind();


            int count = grdExportQuestions.Rows.Count;

            //for (int i = 1; i <= count; i++)
            //{
            //    e.Row.Cells[i].Text = Server.HtmlEncode(e.Row.Cells[i].Text);
            //    //cell.Text = Server.HtmlDecode(cell.Text);
            //}


            if (ds.Tables[0].Rows.Count > 0)
            pnlReport.Visible = true;
            trExcelButton.Visible = true;
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(updPanel1, "Itle_Export_QuestionBank.btnShowGrid_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    // This Method is aslso used in the process of exporting questions into excel file
    public override void VerifyRenderingInServerForm(Control control)
    {


    }

    protected void imgbutExporttoexcel_Click(object sender, EventArgs e)
    {
        string filename = string.Empty;
        string ContentType = string.Empty;
        filename = "QUESTION.xls";
        ContentType = "ms-excel";
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + ContentType;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grdExportQuestions.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
   
    // USED TO HIDE HTML TAGS FROM GRID VIEW CONTROL
    protected void grdExportQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int count = grdExportQuestions.Rows.Count;

       
            foreach (TableCell tc in e.Row.Cells)
            {
                //e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
                tc.Text = Server.HtmlDecode(tc.Text);
            }
        
    }
}
