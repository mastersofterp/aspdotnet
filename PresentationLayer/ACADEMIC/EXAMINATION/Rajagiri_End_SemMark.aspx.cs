using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Rajagiri_End_SemMark : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarkE = new MarksEntryController();
  //  StudentRegist objSR = new StudentRegist();
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
        divMsg.InnerHtml = string.Empty;
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDown();  
            }
            hdnUserno.Value = Convert.ToString(Session["userno"]);
            hdfIpAdd.Value = Convert.ToString(Session["ipAddress"].ToString());
            
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Rajagiri_End_SemMark.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Rajagiri_End_SemMark.aspx");
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            //College Name 
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");

            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));

            ddlsemester.Items.Clear();
            ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_EndSemExamMarkEntry.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvclear();
        DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
        {
            ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            hdfschemno.Value = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            if (ddlcollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            }

        }
        ddlSession.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        }
        lvclear();
        ddlsemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"] + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            ddlCourse.Focus();
        }
        lvclear();
        ddlCourse.SelectedIndex = 0;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvclear();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.Show();
    }
    #region Add Auto Increment Column
    public void AddAutoIncrementColumn(DataTable dt)
    {
        DataColumn column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.AutoIncrement = true;
        column.AutoIncrementSeed = 0;
        column.AutoIncrementStep = 1;
        dt.Columns.Add(column);
        int index = 0;
        foreach (DataRow row in dt.Rows)
        {
            row.SetField(column, ++index);
        }
    }
    #endregion End Auto Increment Column
    protected void Show()
    {
        //To show the Details of Score 2 on tool tip
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tooltip", "toottipScoreTwo();", true);

        string p_para = "@P_SCHEMENO,@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO";
        string p_call = "" + ViewState["schemeno"].ToString() + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue);
        string p_name = "ACD_GET_STUD_END_SEM_RAJAGIRI";

        DataSet ds = objCommon.DynamicSPCall_Select(p_name, p_para, p_call);
      //  DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN acd_student_test_mark_29122022 STM ON (SR.IDNO=STM.IDNO)", "DISTINCT SR.REGNO", "SR.IDNO,SR.SEATNO,STM.EXTERMARK1,STM.EXTERMARK2,STM.EXTERMARK3,isnull(STM.LOCKEM1,0)LOCKEM1,isnull(STM.LOCKEM2,0)LOCKEM2,isnull(STM.LOCKEM3,0)LOCKEM3", "SR.EXAM_REGISTERED=1 AND STM.SCHEME_NO=" + ViewState["schemeno"].ToString() + "AND STM.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND STM.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "AND STM.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SR.REGNO");
       
        var maxMark = objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO='" + Convert.ToInt32(ddlCourse.SelectedValue) + "' AND SCHEMENO='" + Convert.ToInt32(ViewState["schemeno"].ToString()) + "' ");
        lbltxtmax.Visible = true;
        lblmax.Visible = true;
        lblmax.Text = maxMark;
        hdfmaxMark.Value = maxMark;
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            DataTable dtDetails = ds.Tables[0];
            AddAutoIncrementColumn(dtDetails);
            lvScore.DataSource = dtDetails;
            lvScore.DataBind();
            lvScore.Visible = true;
            
            string str = string.Empty;

            #region for creating temp data table to excute EXTER2_CHECK mode (it has nothing to do with data)
            string tempData = "[{\"IDNO\":1458,\"EXTERMARKS\":38.00}]";
            var unQuotedString = tempData.TrimStart('"').TrimEnd('"');

            List<EndSemMark> data = JsonConvert.DeserializeObject<List<EndSemMark>>(unQuotedString);

            string json = JsonConvert.SerializeObject(data);
            DataTable dtTemp = JsonConvert.DeserializeObject<DataTable>(json);
            #endregion

            int ret = objMarkE.SaveScoreEntry(Convert.ToInt32(hdnUserno.Value),Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), "EXTER2_CHECK", dtTemp,"::1");


          
                var lockS1 = ds.Tables[0].Rows[0]["LOCKEM1"].ToString();
                var lockS2 = ds.Tables[0].Rows[0]["LOCKEM2"].ToString();
                var lockS3 = ds.Tables[0].Rows[0]["LOCKEM3"].ToString();
            
                if ((lockS1.Equals("True")) && (lockS2.Equals("False")) && (lockS3.Equals("False")))
                {
                    //$('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', false);
                    //disab
                    str = "$(document).ready(function(){$('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', false);$('#ctl00_ContentPlaceHolder1_btnLock').prop('disabled', false);$('#ctl00_ContentPlaceHolder1_btnfinal').hide();$('#tblScore tbody tr').each(function () { $(this).find('#txtscore1').prop('disabled', true); if ($(this).find('#txtscore1').val()==902 || $(this).find('#txtscore1').val()==903){$(this).find('#txtscore2').prop('disabled', true);}else {$(this).find('#txtscore2').prop('disabled', false);}})}) ";
                }
                else if ((lockS1.Equals("True")) && (lockS2.Equals("True")) && (lockS3.Equals("False")) && (ret.Equals(51)))
                {
                    str = "$(document).ready(function(){$('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', false);$('#ctl00_ContentPlaceHolder1_btnLock').prop('disabled', false);$('#ctl00_ContentPlaceHolder1_btnfinal').hide();$('#tblScore tbody tr').each(function () { $(this).find('#txtscore1').prop('disabled', true); $(this).find('#txtscore2').prop('disabled', true);$(this).find('#txtscore3').prop('disabled', false);var s1 = $(this).find('#txtscore1').val();if(s1==902 || s1==903){$(this).find('#txtscore3').val(s1);$(this).find('#txtscore3').prop('disabled', true);}})}) ";
                }
                else if ((lockS1.Equals("True")) && (lockS2.Equals("True")) && (lockS3.Equals("False")) && (ret.Equals(50)))
                {
                    str = "$(document).ready(function(){ $('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', true);$('#ctl00_ContentPlaceHolder1_btnLock').prop('disabled', true);$('#ctl00_ContentPlaceHolder1_btnfinal').show();$('#tblScore tbody tr').each(function () { $(this).find('#txtscore1').prop('disabled', true); $(this).find('#txtscore2').prop('disabled', true);$(this).find('#txtscore3').prop('disabled', true);})}) ";
                }
                else if ((lockS1.Equals("True")) && (lockS2.Equals("True")) && (lockS3.Equals("True")))
                {
                    str = "$(document).ready(function(){ $('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', true);$('#ctl00_ContentPlaceHolder1_btnLock').prop('disabled', true);$('#ctl00_ContentPlaceHolder1_btnfinal').show();$('#tblScore tbody tr').each(function () { $(this).find('#txtscore1').prop('disabled', true); $(this).find('#txtscore2').prop('disabled', true);$(this).find('#txtscore3').prop('disabled', true);})}) ";
                }
                else
                {
                    str = "$(document).ready(function(){ $('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', false);$('#ctl00_ContentPlaceHolder1_btnLock').prop('disabled', false);$('#ctl00_ContentPlaceHolder1_btnfinal').hide();$('#tblScore tbody tr').each(function () { $(this).find('#txtscore1').prop('disabled', false); $(this).find('#txtscore2').prop('disabled', true);$(this).find('#txtscore3').prop('disabled', true); })}) ";

            }
                string gradeCount = objCommon.LookUp("ACD_STUDENT_RESULT", "count(grade)Grade", "SCHEMENO=" + ViewState["schemeno"].ToString()+ " and SESSIONNO=" + ddlSession.SelectedValue + " and SEMESTERNO=" + ddlsemester.SelectedValue + " and COURSENO=" + ddlCourse.SelectedValue);
            
            if (Convert.ToInt32(Session["usertype"].ToString()) == 1 && gradeCount == "0" && lockS1=="True")
            {
                //$('#ctl00_ContentPlaceHolder1_btnUnLock').click(function () { unlockScore(); });
                string strUnlock = "$('#ctl00_ContentPlaceHolder1_btnUnLock').show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "unlock", "" + strUnlock + "", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str + "", true);
        }
        else
        {
            objCommon.DisplayMessage("No Record", this.Page);
            lvScore.DataSource = null;
            lvScore.DataBind();
            lvScore.Visible = false;
        }

    }
    protected void clear()
    {
        ddlcollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;

        lvScore.DataSource = null;
        lvScore.DataBind();
        lvScore.Visible = false;
        lbltxtmax.Visible = false;
        lblmax.Visible = false;
    }
    protected void lvclear()
    {
        lvScore.DataSource = null;
        lvScore.DataBind();
        lvScore.Visible = false;
        lbltxtmax.Visible = false;
        lblmax.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveMarks(int userno, string datatable, int schemeno, int sessionno, int semesterno, int courseno, string mode, string ipaddress)
    {
        object optval = 0;

        MarksEntryController objMarkE = new MarksEntryController();

        var unQuotedString = datatable.TrimStart('"').TrimEnd('"');
      
        List<EndSemMark> data = JsonConvert.DeserializeObject<List<EndSemMark>>(unQuotedString);

        string json = JsonConvert.SerializeObject(data);
        DataTable dtScore = JsonConvert.DeserializeObject<DataTable>(json);
        //"EXTER1_MARK_ENTRY"
        try
        {
            optval = objMarkE.SaveScoreEntry(userno, schemeno, sessionno, semesterno, courseno, mode, dtScore, ipaddress);
        }
        catch (Exception ex)
        { 
        
        }
        //string myJsonString = (new JavaScriptSerializer()).Serialize(optval);
        return JsonConvert.SerializeObject(optval);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
      //  @P_SCHEMENO INT,@P_SESSIONNO INT,@P_SEMESTERNO INT,@P_COURSENO INT
        if (ddlcollege.SelectedIndex <= 0 || ddlSession.SelectedIndex <= 0 || ddlsemester.SelectedIndex <= 0 || ddlCourse.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage("Select College & Scheme=> Session=> Semester=> Course", this.Page);
            return;
        }
        this.ShowReport("pdf", "StudentScoreReport_EndSem.rpt");
        this.Show();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlsemester.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue+",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {
            throw;
        }
    }

}