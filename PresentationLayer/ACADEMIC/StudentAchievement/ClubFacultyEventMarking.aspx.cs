//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Club Faculty Student Event Marking                 
// CREATION DATE : 29-SEP-2022                                                      
// CREATED BY    : NIKHIL SHENDE 
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//=============================================================
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
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using ClosedXML.Excel;

public partial class ACADEMIC_StudentAchievement_ClubFacultyStudentEventMarking : System.Web.UI.Page
{
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ClubFacultyStudentEventMarkingController objcsem = new ClubFacultyStudentEventMarkingController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDown();
            ActiveRegisteredList.Visible = false;
            //BindListView();

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int Ua_No;
                    Ua_No = Convert.ToInt32(Session["userno"]);
                    //BindListView();
                }
                else
                {
                    objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);

                    div1.Visible = false;
                    return;

                    //Page.Title = Session["coll_name"].ToString();

                    //PageId = Request.QueryString["pageno"];
                }
            }
        }
        //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        //ViewState["CREATE_EVENT_ID"] = null;
        //ViewState[""]

    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Event_Participation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Event_Participation.aspx");
        }
    }

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlclubtype, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO > 0 AND ACTIVESTATUS = 1", "CLUB_ACTIVITY_NO ASC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void BindListView()
    {
        try
        {
            ////DataSet ds = objcsem.RgisteredStudentListforFacluty(Convert.ToInt32(ddlclubtype.SelectedValue), ddlEventCategory.SelectedItem.Text);

            DataSet ds = objcsem.RgisteredStudentListforFaclutyByFacultyNO(Convert.ToInt32(ddlclubtype.SelectedValue), ddlEventCategory.SelectedItem.Text,Convert.ToInt32(Session["userno"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvcStudentActiveDetails.DataSource = ds;
                lvcStudentActiveDetails.DataBind();
                ActiveRegisteredList.Visible = true;
                //pnlclub.Visible = true;
            }
                 else if (ds.Tables[0].Rows.Count <= 0)
            {
                objCommon.DisplayMessage(this.updclubfact, "Record Not Found for selected criteria. ", this.Page);
                ActiveRegisteredList.Visible = false;
            }
            else
            {
                lvcStudentActiveDetails.DataSource = null;
                lvcStudentActiveDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BindListView()" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void BindListView()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_ACTIVE_STUDENT_ACTIVITY AM INNER JOIN ACD_STUDENT AC ON (AM.IDNO=AC.IDNO) LEFT JOIN ACD_CLUB_FACLUTY_STUDENT_EVENT_MARKING EM ON(EM.IDNO=AM.IDNO)  INNER JOIN ACD_BRANCH AB ON(AB.BRANCHNO=AC.BRANCHNO) INNER JOIN ACD_DEGREE AD ON(AD.DEGREENO=AC.DEGREENO) INNER JOIN ACD_ACHIEVEMENT_CREATE_EVENT CE ON(AM.CREATE_EVENT_ID=CE.CREATE_EVENT_ID) CROSS APPLY  (SELECT *  FROM [DBO].[SPLIT](AM.CLUBNO,',')) B INNER JOIN CLUB_MASTER CM ON(B.VALUE=CM.CLUB_ACTIVITY_NO) ", "DISTINCT AC.IDNO, AC.REGNO,AC.STUDNAME AS STUDENT_NAME", "AB.LONGNAME AS BRANCH_NAME,AD.DEGREENAME AS DEGREE_NAME,CE.CREATE_EVENT_ID,EVENT_ID,AM.CLUBNO,CE.EVENT_TITLE", "AM.CLUBNO=" + Convert.ToInt32(ddlclubtype.SelectedValue) + " AND CE.EVENT_TITLE='" + ddlEventCategory.SelectedValue + "'", "REGNO DESC");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvcStudentActiveDetails.DataSource = ds;
    //            lvcStudentActiveDetails.DataBind();
    //            ActiveRegisteredList.Visible = true;
    //            //pnlclub.Visible = true;
    //        }
    //        else if (ds.Tables[0].Rows.Count <= 0)
    //        {
    //            objCommon.DisplayMessage(this.updclubfact, "Record Not Found for selected criteria. ", this.Page);
    //            ActiveRegisteredList.Visible = false;
    //        }
    //        else
    //        {
    //            lvcStudentActiveDetails.DataSource = null;
    //            lvcStudentActiveDetails.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void ClearData()
    {
        ddlclubtype.SelectedIndex = 0;
        ddlEventCategory.SelectedIndex = 0;
        lvcStudentActiveDetails.DataSource = null;
        lvcStudentActiveDetails.DataBind();
        ActiveRegisteredList.Visible = false;
    }

    protected void btnShowActivityDetails_Click(object sender, EventArgs e)
    {
        //int CREATE_EVENT_ID = Convert.ToInt32(objCommon.LookUp("ACD_ACHIEVEMENT_CREATE_EVENT", "ENDDATE", "CREATE_EVENT_ID=" + Convert.ToInt32(ViewState["CREATE_EVENT_ID"]) + " AND ENDDATE>1"));

        ////int CREATE_EVENT_ID = Convert.ToInt32(objCommon.LookUp("ACD_ACHIEVEMENT_CREATE_EVENT", "ENDDATE", "CREATE_EVENT_ID='" + Convert.ToInt32(ViewState["CREATE_EVENT_ID"]+ &&)));
        //if (CREATE_EVENT_ID > 0)
        //{
            BindListView();
        //}

        //else
        //{
        //    BindListView();
        //}
     
    }
    protected void btnCancelActivityDetails_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void lvcStudentActiveDetails_DataBound(object sender, EventArgs e)
    {

    }
    protected void btnSaveStudentActivitydetails_Click(object sender, EventArgs e)
    {
        if (lvcStudentActiveDetails.Items.Count > 0)
        {
            foreach (ListViewDataItem item in lvcStudentActiveDetails.Items)
            {
                HiddenField StudIdno = item.FindControl("hdfIdno") as HiddenField;
                HiddenField StudRegNo = item.FindControl("hdnRegno") as HiddenField;
                HiddenField CreateEventID = item.FindControl("hdnCreateeventId") as HiddenField;
                Label RegisteredNo = item.FindControl("lblRegno") as Label;
                Label StudentName = item.FindControl("lblStudentName") as Label;
                Label BranchName = item.FindControl("lblBranchName") as Label;
                Label DegreeName = item.FindControl("lblDegreeName") as Label;
                DropDownList EventId = item.FindControl("ddlEvent") as DropDownList;
                //HiddenField hdnEventMarkingId = item.FindControl("hdnEventMarkingId") as HiddenField;

                int studid = Convert.ToInt32(StudIdno.Value);
                int eventid = Convert.ToInt32(EventId.SelectedValue);
                //int RegNo = Convert.ToInt32(StudRegNo.Value);//removed from Procedures and controller
                int create_event_id = Convert.ToInt32(CreateEventID.Value);
                string StudetName = StudentName.Text;
                //int EventMarkingId =Convert.ToInt32(hdnEventMarkingId.Value);
                int Ua_No;
                Ua_No = Convert.ToInt32(Session["userno"]);

                if (eventid == 0)
                {
                    objCommon.DisplayMessage(this, "Please Select Participation Type", this.Page);
                    return;
                }
                ////CustomStatus cs = (CustomStatus)objcsem.InsertStudentActivityDetails(studid, RegNo, eventid, create_event_id, Ua_No, Ua_No, Convert.ToInt32(ddlclubtype.SelectedValue), ddlEventCategory.SelectedItem.Text);//, 
                CustomStatus cs = (CustomStatus)objcsem.InsertStudentActivityDetails(studid, eventid, create_event_id, Ua_No, Ua_No, Convert.ToInt32(ddlclubtype.SelectedValue), ddlEventCategory.SelectedItem.Text);//, 

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    //objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                    //ClearData();
                    //EventId.SelectedIndex = 0;
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this, " Record Updated Successfully", this.Page);
                    //ClearData();
                }
                else
                {
                    objCommon.DisplayMessage(this, " Record Already Exist", this.Page);
                    //ClearData();
                }
            }
            objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
            //ClearData();
        }
        else
        {

        }
    }
    protected void btnCancelStudentActivitydetails_Click(object sender, EventArgs e)
    {
        //ClearData();
        //ddlclubtype.SelectedIndex = 0;
        //ddlEventCategory.SelectedIndex = 0;
    }

    protected void ddlclubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlclubtype.SelectedIndex == 0 && ddlEventCategory.SelectedIndex == 0)
        {
            ClearData();
        }
        if (ddlclubtype.SelectedIndex == 0)
        {
            ClearData();
        }
        if (ddlEventCategory.SelectedIndex == 0)
        {
            lvcStudentActiveDetails.DataSource = null;
            lvcStudentActiveDetails.DataBind();
            ActiveRegisteredList.Visible = false;
        }
        objCommon.FillDropDownList(ddlEventCategory, "ACD_ACTIVE_STUDENT_ACTIVITY AB  INNER JOIN ACD_ACHIEVEMENT_CREATE_EVENT DB ON(DB.CREATE_EVENT_ID=AB.CREATE_EVENT_ID)", "DISTINCT DB.CREATE_EVENT_ID", " DB.EVENT_TITLE", "AB.CLUBNO=" + Convert.ToInt32(ddlclubtype.SelectedValue), "DB.EVENT_TITLE ASC");
        //BindListView();
    }

    protected void ddlEventCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEventCategory.SelectedIndex == 0)
        {
            ClearData();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DataGrid Gr = new DataGrid();
        DataSet ds = new DataSet();
        ds = objcsem.ActivityStudnetEvenMarkingReport();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=ClubFacultyStudentEventMarkingReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
    }


    public DataSet GetClubStudentListDeatils()
    {
        DataSet ds = null;

        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[0];

            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CLUB_REGISTERED_STUDENT_LIST_REPORT", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllSubModuleDetails-> " + ex.ToString());
        }

        return ds;
    }

    public DataSet GetClubActivityRegistration()
    {
        DataSet ds = null;

        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[0];

            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CLUB_ACTIVITY_REGISTRATION_REPORT", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllSubModuleDetails-> " + ex.ToString());
        }

        return ds;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        // DataSet ds = OBJCLUB.GetClubStudentListDeatils();
        try
        {
            DataSet ds = GetClubStudentListDeatils();

            ds.Tables[0].TableName = "Detailed Reports";
            ds.Tables[1].TableName = "Summary Reports";

            if (ds.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        wb.Worksheets.Add(dt);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Club_Registered_Student_List.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Available To Export !!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void btnExportR_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = GetClubActivityRegistration();

            ds.Tables[0].TableName = "Detailed Reports";
            ds.Tables[1].TableName = "Summary Reports";

            if (ds.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        wb.Worksheets.Add(dt);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Club_Activity_Registration_Report.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Available To Export !!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
  
}

