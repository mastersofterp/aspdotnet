using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;

public partial class ACADEMIC_StudentAchievement_StudentAchievementReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventParticipationController OBJEPC = new EventParticipationController();
    EventParticipationEntity objEPE = new EventParticipationEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDown();
        }

    }
    public void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID>0", "ACADMIC_YEAR_ID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentAchievementReport.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, System.EventArgs e)
    {
        DataGrid Gr = new DataGrid();
        DataSet ds = new DataSet();
        int EVENT_PARTICIPATION_ID = Convert.ToInt32(ddlAcademicYear.SelectedValue);
        ds = OBJEPC.StudentAchievementReport(EVENT_PARTICIPATION_ID);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=CreateEventReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
                ClearData();
            }
            else
            {
               
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                ClearData();
            }

        }
    }

    protected void btnReportMoocs_Click(object sender, System.EventArgs e)
    {
        MoocsCertificationController objMCC = new MoocsCertificationController();
        DataGrid Gr = new DataGrid();
        DataSet ds = new DataSet();
        int MOOCD_CERTIFICATION_ID = Convert.ToInt32(ddlAcademicYear.SelectedValue);
        ds = objMCC.MoocsCertificationReport(MOOCD_CERTIFICATION_ID);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=MoocsReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
                ClearData();
                
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                ClearData();
            }

        }
    }
        public void ClearData()
         {
            ddlAcademicYear.SelectedIndex = 0;
         }
    }
