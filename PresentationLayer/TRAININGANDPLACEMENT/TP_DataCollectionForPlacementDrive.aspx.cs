using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;

public partial class TRAININGANDPLACEMENT_TP_DataCollectionForPlacementDrive : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    StudentController objStud = new StudentController();
    Student objstudent = new Student();
    TPTraining objTPTrn = new TPTraining();

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", " ACTIVESTATUS=1", "");
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "ACTIVESTATUS=1", "");
                    objCommon.FillDropDownList(ddlSemester, " ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "ACTIVESTATUS=1", "");
                    objCommon.FillDropDownList(ddlAdmBatch, " ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS=1", "");
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    // string lastroundcount = objCommon.LookUp("ACD_TP_COMPSCHEDULE", "photo", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                    Session["EmailFileAttachemnt"] = null;
                    ViewState["folderPath"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void View_Click(object sender, EventArgs e)
    {
        objTPTrn.Degree =Convert.ToInt32( ddlDegree.SelectedValue);
        objTPTrn.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
        objTPTrn.Semester = Convert.ToInt32(ddlSemester.SelectedValue);
        if (TextBox1.Text != string.Empty)
        {
            objTPTrn.CGPA = Convert.ToDecimal(TextBox1.Text);
        }
        else
        {
            objTPTrn.CGPA = Convert.ToDecimal(0.00);
        }
        if (txtBacklog.Text!= string.Empty)
        {
        objTPTrn.NoOfAttempt = Convert.ToInt32(txtBacklog.Text);
        }
        else
        {
            objTPTrn.NoOfAttempt=0;
        }
        if (txtAttempt.Text != string.Empty)
        {
            objTPTrn.Attempt = Convert.ToInt32(txtAttempt.Text);
        }
        else
        {
            objTPTrn.Attempt = 0;
        }
        objTPTrn.ADMBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);

       // CustomStatus cs = (CustomStatus)objCompany.GetDataForPlacementDrive(objTPTrn);
        DataSet ds = objCompany.GetDataForPlacementDrive(objTPTrn);
        lvStudentPlacementDetail.DataSource = ds;
        lvStudentPlacementDetail.DataBind();
        lvStudentPlacementDetail.Visible = true;
        ViewState["ExcelData"] = ds;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
        ddlDegree.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        TextBox1.Text = string.Empty;
        txtBacklog.Text = string.Empty;
        txtAttempt.Text = string.Empty;
        ddlAdmBatch.SelectedValue = "0";
        lvStudentPlacementDetail.DataSource = null;
        lvStudentPlacementDetail.DataBind();

    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        if (this.lvStudentPlacementDetail.Items.Count == 0)
        {
            objCommon.DisplayMessage("Students Data Is Not Available !", this.Page);
            return;
        }
        GridView gvStudData = new GridView();
        gvStudData.DataSource = ViewState["ExcelData"];
        gvStudData.DataBind();
        string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
        string attachment = "attachment; filename=STUDENT_PLACEMENT_DATA.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Response.Write(FinalHead);
        gvStudData.RenderControl(htw);
        //string a = sw.ToString().Replace("_", " ");
        Response.Write(sw.ToString());
        Response.End();
    }
}