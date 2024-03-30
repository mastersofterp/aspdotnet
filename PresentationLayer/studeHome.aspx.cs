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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Collections.Generic;
using System.Web.Services;
using System.IO;
using System.Text;
public partial class StudeHome : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    ExamController objExamController = new ExamController();
    NewsController objNc = new NewsController();
    TPController objTpcontroller = new TPController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                    //CheckPageAuthorization();

                    //if (Request.QueryString["i"] == "1")
                    //{
                    //    Anchor_Click(sender, e) ;
                    //}

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    //BindListViewTask();               // Pending

                    // DataSet dsLastLoginTime = objCommon.FillDropDown("LogFile", "TOP(1) LEFT(FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt'), Charindex(' ', FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt')) - 1)AA", "null", "UA_NAME='" + Session["username"].ToString() + "'", "LOGINTIME desc");
                    //  DataSet dsLastLoginForm = objCommon.FillDropDown("LogFile", "TOP(1) right(FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt'), Charindex(' ', FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt')) - 4) as AA", "null", "UA_NAME='" + Session["username"].ToString() + "'", "LOGINTIME desc");

                    //lblLastLoginTime.Text = dsLastLoginTime.Tables[0].Rows[0]["AA"].ToString();
                    //lblLastLoginForm.Text = dsLastLoginForm.Tables[0].Rows[0]["AA"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    // Show_ExamTT();//PRASHANTG-TN56760--220324
                    //  Show_Notice();//PRASHANTG-TN56760--220324
                    //  Show_TodaysTT();//PRASHANTG-TN56760--240324
                    // Show_placement();//PRASHANTG-TN56760--240324
                  
                    //PRASHANTG-TN56760--240324
                    DataSet ds = new DataSet();
                    ds = objCommon.FillDropDown("ACD_MODULE_CONFIG", "ISNULL(STUD_DASH_OUTSTANING,0) 'STUD_DASH_OUTSTANING',ISNULL(OUTSTANDING_FEECOLLECTION,0) 'OUTSTANDING_FEECOLLECTION'", "OUTSTANDING_MESSAGE,ISNULL(DISPLAY_STUD_LOGIN_DASHBOARD,0) 'DISPLAY_STUD_LOGIN_DASHBOARD'", "", "");
                    //int Outstanding = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(STUD_DASH_OUTSTANING,0)",""));
                    //PRASHANTG-TN56760--240324
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["STUD_DASH_OUTSTANING"].ToString() == "1")
                            {
                                divoutstanding.Visible = true;
                                string spName = "PKG_ACD_TOTAL_OUTSTANDING_FEES";
                                string spParameters = "@P_IDNO";
                                string spValue = "" + Convert.ToInt32(Session["idno"].ToString());
                                DataSet dsoutstaning = null;

                                dsoutstaning = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                                if (dsoutstaning != null)
                                {
                                    if (dsoutstaning.Tables.Count > 0 && dsoutstaning.Tables[0] != null && dsoutstaning.Tables[0].Rows.Count > 0)
                                    {
                                        // lblLastLoginTime.Text = dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"].ToString();//PRASHANTG-TN56760--240324

                                        if (Convert.ToInt32(dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"]) > 0)
                                        {
                                            divoutstanding.Visible = true;
                                            lblLastLoginTime.Text = dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"].ToString();
                                            string Outstanding_Message = ds.Tables[0].Rows[0]["OUTSTANDING_MESSAGE"].ToString();
                                            var amt = dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"].ToString();
                                            var msg = Outstanding_Message.Replace(@"[Amount]", amt.ToString() + " Rs.");
                                            objCommon.DisplayMessage(msg, this.Page);
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript7", "AddClassTobtnoutfees();", true);
                                        }
                                        else { lblLastLoginTime.Text = "0.00"; }
                                    }
                                    if (dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"].ToString() == "0.00")
                                    {
                                        btnoutfees.Visible = false;
                                        btnpayonline.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                divoutstanding.Visible = false;
                            }

                            // int DashboardOnOff = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(DISPLAY_STUD_LOGIN_DASHBOARD,0)", ""));
                            //PRASHANTG-TN56760--240324
                            if (ds.Tables[0].Rows[0]["DISPLAY_STUD_LOGIN_DASHBOARD"].ToString() == "1")
                            {
                                pnlMarquee.Visible = true;
                            }
                            else
                            {
                                pnlMarquee.Visible = false;
                            }
                        }
                    }
                    //code commented by PRASHANTG-TN56760--240324
                    // Added by Gopal Mandaogade 04102023 Ticket #46419
                    /* DataSet ds1 = objCommon.FillDropDown("ACD_MODULE_CONFIG", "ISNULL(OUTSTANDING_FEECOLLECTION,0) OUTSTANDING_FEECOLLECTION", "OUTSTANDING_MESSAGE", "", "");
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["OUTSTANDING_FEECOLLECTION"]) == 1)
                    {
                        string Outstanding_Message = ds.Tables[0].Rows[0]["OUTSTANDING_MESSAGE"].ToString();
                        string spName = "PKG_ACD_TOTAL_OUTSTANDING_FEES";
                        string spParameters = "@P_IDNO";
                        string spValue = "" + Convert.ToInt32(Session["idno"].ToString());
                        DataSet dsoutstaning = null;

                        dsoutstaning = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                        if (dsoutstaning.Tables.Count > 0 && dsoutstaning != null && dsoutstaning.Tables[0] != null && dsoutstaning.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"]) > 0)
                            {
                                divoutstanding.Visible = true;
                                lblLastLoginTime.Text = dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"].ToString();
                                var amt = dsoutstaning.Tables[0].Rows[0]["TOTAL_OUTSTANDING"].ToString();
                                var msg = Outstanding_Message.Replace(@"[Amount]", amt.ToString() + " Rs.");
                                objCommon.DisplayMessage(msg, this.Page);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript7", "AddClassTobtnoutfees();", true);
                            }
                        }

                    }*/
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "studehome.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //PRASHANTG-TN56760--220324
    protected void btnLoadAttend_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsAttendance = new DataSet();
            int idno = Convert.ToInt32(Session["idno"].ToString());
            dsAttendance = objSC.RetrieveStudentAttendanceDetailsForDashboard(idno);

            StringBuilder sb = new StringBuilder();

            if (dsAttendance != null)
            {
                if (dsAttendance.Tables.Count > 0)
                {
                    for (int i = 0; i < dsAttendance.Tables[0].Rows.Count; i++)
                    {
                        string origTitle = dsAttendance.Tables[0].Rows[i]["CCODE"] + "- " + dsAttendance.Tables[0].Rows[i]["COURSE_NAME"] + " (SEC -" + dsAttendance.Tables[0].Rows[i]["SECTIONNAME"] + ")";
                        sb.Append("<tr>");
                        sb.Append("<td class='text-center' data-container='body' data-original-title='" + origTitle + "' data-toggle='tooltip'>" + dsAttendance.Tables[0].Rows[i]["CCODE"] + "</td>");
                        sb.Append("<td class='text-center'>" + dsAttendance.Tables[0].Rows[i]["ATT"] + "</td>");
                        sb.Append("<td class='text-center'>" + dsAttendance.Tables[0].Rows[i]["ATT_PER"] + "</td>");
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    sb.Append("<tr style='text-align:center; font-size:15px; font-weigth:bold' class='info'><td colspan='3'>No records to display..</td></tr>");
                }
            }
            else
            {
                sb.Append("<tr style='text-align:center; font-size:15px; font-weigth:bold' class='info'><td colspan='3'>No records to display..</td></tr>");
            }

            tbodyAtten.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.btnLoadAttend_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //PRASHANTG-TN56760--220324
    protected void btnLoadQA_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();

            int UserTypeId = Convert.ToInt32(Session["userno"]);
            ds = objSC.GetQuickAccessForStudentDashboard(UserTypeId);//test -5730

            StringBuilder sb = new StringBuilder();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string nav = ds.Tables[0].Rows[i]["AL_URL"] == null ? "#" : ds.Tables[0].Rows[i]["AL_URL"].ToString() + "?pageno=" + ds.Tables[0].Rows[i]["AL_NO"].ToString();
                        sb.Append("<li class='list-group-item'><a id='A1' href='" + nav + "'  runat='server'  target='_blank'><i class='fa fa-star'></i>" + ds.Tables[0].Rows[i]["AL_LINK"] + "</a></li>");
                    }
                }
                else
                {
                    sb.Append("<li class='list-group-item text-center info' style='text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;'>No records to display.. </li>");
                }
            }
            else
            {
                sb.Append("<li class='list-group-item text-center info' style='text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;'>No records to display.. </li>");
            }
            ulQuickAccess.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.btnLoadQA_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //PRASHANTG-TN56760--220324
    protected void btnLoadTask_Click(object sender, EventArgs e)
    {
        try
        {
            int ua_no = Convert.ToInt32(Session["userno"]);
            int ua_type = Convert.ToInt32(Session["usertype"]);

            DataSet ds = objSC.GetTaskForStudentDashboard(ua_type, ua_no);
            StringBuilder sb = new StringBuilder();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string nav = ds.Tables[0].Rows[i]["AL_URL"] == null ? "#" : ds.Tables[0].Rows[i]["AL_URL"].ToString() + "?pageno=" + ds.Tables[0].Rows[i]["AL_NO"].ToString();
                        sb.Append("<li class='list-group-item'><a id='A1' href='" + nav + "'  runat='server'  target='_blank'><i class='fa fa-star'></i>" + ds.Tables[0].Rows[i]["AL_LINK"] + "</a></li>");
                    }
                }
                else
                {
                    sb.Append("<li class='list-group-item text-center info' style='text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;'>No records to display.. </li>");
                }
            }
            else
            {
                sb.Append("<li class='list-group-item text-center info' style='text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;'>No records to display.. </li>");
            }
            ulTasks.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.btnLoadTask_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //PRASHANTG-TN56760--220324
    protected void btnActNotice_Click(object sender, EventArgs e)
    {
        try
        {
            Show_Notice();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.btnActNotice_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //PRASHANTG-TN56760--240324
    protected void btnTT_Click(object sender, EventArgs e)
    {
        try
        {
            Show_TodaysTT();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.btnTT_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //PRASHANTG-TN56760--240324
    protected void btnClassTT_Click(object sender, EventArgs e)
    {
        DataSet dsTimeTable = new DataSet();
        int idno = Convert.ToInt32(Session["idno"].ToString());
        dsTimeTable = objSC.RetrieveStudentTimeTableDetails(idno);
        StringBuilder html = new StringBuilder();

        if (dsTimeTable != null)
        {
            if (dsTimeTable.Tables[0] != null && dsTimeTable.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsTimeTable.Tables[0].Rows.Count; i++)
                {

                    html.Append("<tr>");
                    html.Append("<td class='text-center' style='width:14%'>" + dsTimeTable.Tables[0].Rows[i]["Slot"].ToString() + "</td>");
                    if (dsTimeTable.Tables[0].Rows[i]["Monday"].ToString() != "-")
                    {
                        var arrLec1 = dsTimeTable.Tables[0].Rows[i]["Monday"].ToString();
                        html.Append("<td class='text-center' data-container='body'  data-original-title='" + arrLec1[1] + "-" + arrLec1[0] + " (SEC-" + arrLec1[2] + ")' data-toggle='tooltip'>" + arrLec1 + "</td>");
                    }
                    else
                    {
                        html.Append("<td class='text-center'>" + dsTimeTable.Tables[0].Rows[i]["Monday"].ToString() + "</td>");
                    }

                    if (dsTimeTable.Tables[0].Rows[i]["Tuesday"].ToString() != "-")
                    {
                        var arrLec2 = dsTimeTable.Tables[0].Rows[i]["Tuesday"].ToString();

                        html.Append("<td class='text-center' data-container='body'  data-original-title='" + arrLec2[1] + " - " + arrLec2[0] + " (SEC-" + arrLec2[2] + ")' data-toggle='tooltip'>" + arrLec2 + "</td>");
                    }
                    else
                    {
                        html.Append("<td class='text-center'>" + dsTimeTable.Tables[0].Rows[i]["Tuesday"].ToString() + "</td>");
                    }

                    if (dsTimeTable.Tables[0].Rows[i]["Wednesday"].ToString() != "-")
                    {
                        var arrLec3 = dsTimeTable.Tables[0].Rows[i]["Wednesday"].ToString();
                       html.Append("<td class='text-center' data-container='body'  data-original-title='" + arrLec3[1] + " - " + arrLec3[0] + " (SEC-" + arrLec3[2] + ")' data-toggle='tooltip'>" + arrLec3 + "</td>");
                    }
                    else
                    {
                        html.Append("<td class='text-center'>" + dsTimeTable.Tables[0].Rows[i]["Wednesday"].ToString() + "</td>");
                    }

                    if (dsTimeTable.Tables[0].Rows[i]["Thursday"].ToString() != "-")
                    {
                        var arrLec4 = dsTimeTable.Tables[0].Rows[i]["Thursday"].ToString();
                       html.Append("<td class='text-center' data-container='body'  data-original-title='" + arrLec4[1] + " - " + arrLec4[0] + " (SEC-" + arrLec4[2] + ")' data-toggle='tooltip'>" + arrLec4 + "</td>");
                    }
                    else
                    {
                        html.Append("<td class='text-center'>" + dsTimeTable.Tables[0].Rows[i]["Thursday"].ToString() + "</td>");
                    }

                    if (dsTimeTable.Tables[0].Rows[i]["Friday"].ToString() != "-")
                    {
                        var arrLec5 = dsTimeTable.Tables[0].Rows[i]["Friday"].ToString();
                        html.Append("<td class='text-center' data-container='body'  data-original-title='" + arrLec5[1] + " - " + arrLec5[0] + " (SEC-" + arrLec5[2] + ")' data-toggle='tooltip'>" + arrLec5 + "</td>");
                    }
                    else
                    {
                        html.Append("<td class='text-center'>" + dsTimeTable.Tables[0].Rows[i]["Friday"].ToString() + "</td>");
                    }

                    if (dsTimeTable.Tables[0].Rows[i]["Saturday"].ToString() != "-")
                    {
                        var arrLec6 = dsTimeTable.Tables[0].Rows[i]["Saturday"].ToString().Split('-');
                        if (arrLec6[0] == "undefined") { arrLec6[0] = ""; }
                        html.Append("<td class='text-center' data-container='body'  data-original-title='" + arrLec6[1] + "-" + arrLec6[0] + " (SEC-" + arrLec6[2] + ")' data-toggle='tooltip'>" + arrLec6 + "</td>");
                    }
                    else
                    {
                        html.Append("<td class='text-center'>" + dsTimeTable.Tables[0].Rows[i]["Saturday"].ToString() + "</td>");
                    }
                }
            }
            else
            {
                html.Append("<tr style='text-align:center; font-size:15px; font-weigth:bold' class='info'><td colspan='8'>No records to display..</td></tr>");
            }
        }
        else
        {
            html.Append("<tr style='text-align:center; font-size:15px; font-weigth:bold' class='info'><td colspan='8'>No records to display..</td></tr>");
        }
    }
    //PRASHANTG-TN56760--240324
    protected void btnExamTT_Click(object sender, EventArgs e)
    {
        Show_ExamTT();
    }
    //PRASHANTG-TN56760--240324
    protected void btnPlacement_Click(object sender, EventArgs e)
    {
        Show_placement();
    }

    public void Show_TodaysTT()
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);

            DataSet dsTimeTable = objSC.GetTodaysTimeTableForStudent(idno);
            if (dsTimeTable != null)
            {
                if (dsTimeTable.Tables.Count > 0 && dsTimeTable != null && dsTimeTable.Tables[0] != null && dsTimeTable.Tables[0].Rows.Count > 0)
                {
                    lvTodaysTT.DataSource = dsTimeTable;
                    lvTodaysTT.DataBind();
                }
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.Show_TodaysTT() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void Show_Notice()
    {
        try
        {
            DataSet ds = objNc.GetUserTypeWiseNewsstudent(Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["idno"]));
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvActiveNotice.DataSource = ds.Tables[0];
                    lvActiveNotice.DataBind();
                }
                if (ds.Tables.Count > 0 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    lvExpNotice.DataSource = ds.Tables[1];
                    lvExpNotice.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.Show_Notice() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void Show_ExamTT()
    {
        try
        {
            DataSet ds = objExamController.GetStudentExamTimeTable(Convert.ToInt32(Session["currentsession"]), 0, Convert.ToInt32(Session["idno"]));
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvExamTT.DataSource = ds;
                    lvExamTT.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.Show_ExamTT() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //------start 14-12-2023--Juned--Show Placement Data
    public void Show_placement()
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                DataSet ds = objTpcontroller.GetPlacement(Convert.ToInt32(Session["userno"]));//3531
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        LvPlacement.DataSource = ds.Tables[0];
                        LvPlacement.DataBind();
                        divplacement.Visible = true;
                    }
                }
            }
            else
            {
                divplacement.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.Show_placement() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //------End 14-12-2023--Juned--Show Placement Data

    protected void GetFileNamePathEventForActiveNotice(object sender, CommandEventArgs e)
    {
        string filename = e.CommandArgument.ToString();
        GetFileNamePath(filename);
    }

    protected void GetFileNamePathEventForExpiredNotice(object sender, CommandEventArgs e)
    {
        string filename = e.CommandArgument.ToString();
        GetFileNamePath(filename);
    }


    protected string GetFileNamePath(object filename)
    {
        string Url = string.Empty;
        string fileUrl = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = filename.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {
                return "";
            }
            else
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;

                fileUrl = string.Format(ResolveUrl("~/DownloadImg/" + ImageName));


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                if (CheckIFExits(ImageName) == true)
                {
                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    string script = "window.open('" + fileUrl + "', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script, true);
                    return string.Format(ResolveUrl("~/DownloadImg/" + ImageName));
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.GetFileNamePath() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
        return string.Empty;
    }

    #region BlobStorage
    public bool CheckIFExits(string FileName)
    {
        bool retIfExists = false;

        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                retIfExists = true;
            });
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.CheckIFExits() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retIfExists;
    }
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
          

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.DeleteIFExits() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion BlobStorage

    protected string checkFile(string filename)
    {
        string filepath = Server.MapPath("~//UPLOAD_FILES//NOTICE_DOCUMENT/");
        string returnPath = "UPLOAD_FILES/NOTICE_DOCUMENT/";
        string returnFilename = "";

        FileInfo myfile = new FileInfo(filepath + filename);
        if (myfile.Exists)
        {
            if (filename.Contains(' '))
            {
                returnFilename = System.Web.HttpUtility.UrlPathEncode(filename);
            }
            else
            {
                returnFilename = filename;
            }
            return "~/upload_files/notice_document/" + returnFilename.ToString();
        }
        else
        {
            return "";
        }
    }

    //public string GetFileNamePath(object filename)
    //{
    //    string filepath = Server.MapPath("~//UPLOAD_FILES//NOTICE_DOCUMENT/");

    //    FileInfo myfile = new FileInfo(filepath + filename);
    //    if (myfile.Exists)
    //    {
    //        return "~/upload_files/notice_document/" + filename.ToString();
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}

    public string GetFileName(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return filename.ToString();
        else
            return "None";
    }
    
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studeHome.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studeHome.aspx");
        }
    }
    
    //private void BindListViewTask()
    //{
    //    try
    //    {
    //        string host = Request.Url.Host;
    //        string scheme = Request.Url.Scheme;
    //        int portno = Request.Url.Port;

    //        DataSet dsTasks = objSC.RetrieveStudentTaskDetails(Convert.ToInt32(Session["idno"].ToString()));

    //        hftot.Value = dsTasks.Tables[0].Rows.Count.ToString();

    //        lblTaskCount.Text = dsTasks.Tables[0].Rows.Count.ToString("00");

    //        if (dsTasks.Tables[0].Rows.Count > 0)
    //        {
    //            Repeater1.DataSource = dsTasks;
    //            Repeater1.DataBind();
    //        }

    //        foreach (RepeaterItem item in Repeater1.Items)
    //        {
    //            LinkButton lbtnli = (LinkButton)item.FindControl("lbtnli");
    //            string cmmd = lbtnli.CommandArgument.ToString();
    //            int alno = Convert.ToInt32(lbtnli.CommandName);

    //            int visited = objCommon.LookUp("ACD_LINK_STATUS", "1", "UANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ALNO=" + alno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_LINK_STATUS", "1", "UANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ALNO=" + alno));
    //            if (visited == 1)
    //            {
    //                HtmlGenericControl x = (HtmlGenericControl)item.FindControl("spancount");
    //                x.Attributes["class"] = "";
    //            }
    //            else
    //            {
    //                HtmlGenericControl x = (HtmlGenericControl)item.FindControl("spancount");
    //                x.Attributes["class"] = "ncount";
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void Anchor_Click(object sender, EventArgs e)
    //{
    //    string host = Request.Url.Host;
    //    string scheme = Request.Url.Scheme;
    //    int portno = Request.Url.Port;

    //    foreach (RepeaterItem item in Repeater1.Items)
    //    {
    //        LinkButton lbtnli = (LinkButton)item.FindControl("lbtnli");
    //        string cmmd=lbtnli.CommandArgument.ToString();
    //    }     
    //}

    //protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    string host = Request.Url.Host;
    //    string scheme = Request.Url.Scheme;
    //    int portno = Request.Url.Port;
    //    string alurl = Convert.ToString(e.CommandArgument);
    //    int alno = Convert.ToInt32(e.CommandName);
    //    int result = 0;
    //    int uano = Convert.ToInt32(Session["userno"].ToString());

    //    result = objSC.AddLinkStatus(uano, alno);

    //    BindListViewTask();

    //    foreach (RepeaterItem item in Repeater1.Items)
    //    {
    //        LinkButton lbtnli = (LinkButton)item.FindControl("lbtnli");
    //        string cmmd = lbtnli.CommandArgument.ToString();

    //        int visited = objCommon.LookUp("ACD_LINK_STATUS", "1", "UANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ALNO=" + alno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_LINK_STATUS", "1", "UANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ALNO=" + alno));
    //        if (visited == 1)
    //        {
    //            HtmlGenericControl x = (HtmlGenericControl)e.Item.FindControl("spancount");
    //            x.Attributes["class"] = "";
    //        }
    //        else
    //        {
    //            HtmlGenericControl x = (HtmlGenericControl)e.Item.FindControl("spancount");
    //            x.Attributes["class"] = "ncount";
    //        }
    //    }

    //    if (host == "localhost")
    //    {
    //        if (alurl.Contains("Masters/masters"))
    //        {
    //            Response.Redirect("~/" + alurl + "&pageno=" + alno);
    //        }
    //        else
    //        {
    //            // Response.Redirect(Request.RawUrl); 
    //            string url = "~/" + alurl + "?pageno=" + alno;
    //            Response.Redirect("~/" + alurl + "?pageno=" + alno);
    //        }
    //    }
    //    else
    //    {
    //        if (alurl.Contains("Masters/masters"))
    //        {
    //            Response.Redirect(scheme + "://" + host + "/" + alurl + "&pageno=" + alno);
    //        }
    //        else
    //        {
    //            Response.Redirect(scheme + "://" + host + "/" + alurl + "?pageno=" + alno);
    //        }
    //    }    
    //}

    
    /***************************************** ADDED ON 02-04-2020 **********************************************/
    //Not in use //prashantg-290324
    [WebMethod]
    public static StudentHomeModel.StudAttendance ShowAttendance()
    {
        StudeHome a = new StudeHome();
        StudentHomeModel.StudAttendance StudAttnData = a.BindAttendance();
        return StudAttnData;
    }
    private StudentHomeModel.StudAttendance BindAttendance()
    {
        StudentHomeModel.StudAttendance objstudAttData = new StudentHomeModel.StudAttendance();
        List<StudentHomeModel.StudentAttendance> objAttList = new List<StudentHomeModel.StudentAttendance>();
        try
        {
            DataSet dsAttendance = new DataSet();
          int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + Convert.ToInt32(Session["idno"] + ""))) == '0' ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + Convert.ToInt32(Session["idno"] + "")));
            int sessionno = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SES ON (SES.SESSIONNO=SR.SESSIONNO)", "MAX(SR.SESSIONNO)", "SR.IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(SES.FLOCK,0)=1 AND SES.COLLEGE_ID=" + college_id + "") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SES ON (SES.SESSIONNO=SR.SESSIONNO)", "MAX(SR.SESSIONNO)", "SR.IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(SES.FLOCK,0)=1 AND SES.COLLEGE_ID=" + college_id + ""));
            Session["sessionno"] = sessionno;
            int schemeno = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT SCHEMENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT SCHEMENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            Session["schemeno"] = schemeno;
            int semesterno = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(Session["sessionno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno"].ToString())));
            Session["semesterno"] = semesterno;
            int idno = Convert.ToInt32(Session["idno"].ToString());
            // Added on 07-04-2020
            int sectionNo = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SectionNo)", "IDNO=" + idno + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SectionNo)", "IDNO=" + idno + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno));
            // Added on 07-04-2020
            dsAttendance = objSC.RetrieveStudentAttendanceDetailsForDashboard(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), idno, sectionNo);
                

            if (dsAttendance != null && dsAttendance.Tables[0] != null && dsAttendance.Tables[0].Rows.Count > 0)
            {
                objAttList = (from DataRow dr in dsAttendance.Tables[0].Rows
                              select new StudentHomeModel.StudentAttendance
                              {
                                  CourseName = dr["COURSE_NAME"].ToString(),
                                  Attendance = dr["ATT"].ToString(),
                                  AttendancePerc = dr["ATT_PER"].ToString(),
                                  CourseCode = dr["CCODE"].ToString(),
                                  SectionName = dr["SECTIONNAME"].ToString()
                              }).ToList();
            }
            else
            {
                objstudAttData = null;
            }
            objstudAttData.AttendList = objAttList;

            //objstudAttData.AttendancePercent = dsAttendance.Tables[0].Rows[0]["PER"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.BindAttendance() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objstudAttData;
    }
    
    //[WebMethod]
    //public static StudentHomeModel.GetNoticeData ShowNoticeAnnData()
    //{
    //    StudeHome a = new StudeHome();
    //    StudentHomeModel.GetNoticeData NoticeAnnData = a.BindListViewNotice();
    //    return NoticeAnnData;
    //}
    //private StudentHomeModel.GetNoticeData BindListViewNotice()
    //{
    //    StudentHomeModel.GetNoticeData objGetNoticeAnnouncementData = new StudentHomeModel.GetNoticeData();
    //    try
    //    {
    //        List<StudentHomeModel.StudentNotice> NoticeList = new List<StudentHomeModel.StudentNotice>(); 

    //        NewsController objNc = new NewsController();
    //        DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NOTICE");

    //        DataSet dsGetAnnouncementCount = objSC.GetAnnouncementCount(Convert.ToInt32(Session["idno"].ToString()));
    //        DataSet dsGetAssignmentCount = objSC.GetAssignmentCount(Convert.ToInt32(Session["idno"].ToString()));
    //        if (dsGetAnnouncementCount != null)
    //        {
    //            if (dsNews != null && dsNews.Tables[0] != null && dsNews.Tables[0].Rows.Count > 0)
    //            {
    //                NoticeList = (from DataRow dr in dsNews.Tables[0].Rows
    //                              select new StudentHomeModel.StudentNotice
    //                              {
    //                                  Link = dr["LINK"].ToString(),
    //                                  Title = dr["TITLE"].ToString(),
    //                                  NewsDescription = dr["NEWSDESC"].ToString()
    //                              }).ToList();
    //            }
    //            else
    //            {
    //                NoticeList = null;
    //            }
    //            objGetNoticeAnnouncementData.NoticeList = NoticeList;
    //            objGetNoticeAnnouncementData.Announcement = Convert.ToInt32(dsGetAnnouncementCount.Tables[0].Rows[0]["ANNOUNCEMENT"]).ToString("00");
    //            objGetNoticeAnnouncementData.Assignment = Convert.ToInt32(dsGetAssignmentCount.Tables[0].Rows[0]["ASSIGNMENT"]).ToString("00");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    return objGetNoticeAnnouncementData;
    //}

    [WebMethod]
    public static List<StudentHomeModel.StudentNews> ShowNewsData()
    {
        StudeHome a = new StudeHome();
        List<StudentHomeModel.StudentNews> NewsData = a.BindListViewNews();
        return NewsData;
    }
    private List<StudentHomeModel.StudentNews> BindListViewNews()
    {
        List<StudentHomeModel.StudentNews> newsList = new List<StudentHomeModel.StudentNews>();
        try
        {
            //DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NEWS");
            DataSet dsNews = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (dsNews != null && dsNews.Tables[0] != null && dsNews.Tables[0].Rows.Count > 0)
            {
                newsList = (from DataRow dr in dsNews.Tables[0].Rows
                            select new StudentHomeModel.StudentNews
                            {
                                Day = dr["DD"].ToString(),
                                Month = dr["MM"].ToString(),
                                //Link = dr["LINK"].ToString(),
                                //Link = checkFile("upload_files/notice_document/" + dr["FILENAME"].ToString()),
                                Link = checkFile(dr["FILENAME"].ToString()),
                                Title = dr["TITLE"].ToString(),
                                NewsDesc = dr["NEWSDESC"].ToString()
                            }).ToList();
            }
            else
            {
                newsList = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return newsList;
    }

    [WebMethod]
    public static List<StudentHomeModel.StudentNews> ShowExpiredNewsData()
    {
        StudeHome a = new StudeHome();
        List<StudentHomeModel.StudentNews> ExpiredNewsData = a.BindListViewExpiredNews();
        return ExpiredNewsData;
    }
    private List<StudentHomeModel.StudentNews> BindListViewExpiredNews()
    {
        List<StudentHomeModel.StudentNews> ExpirednewsList = new List<StudentHomeModel.StudentNews>();
        try
        {
            NewsController objNc = new NewsController();
            //DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NEWS");
            DataSet dsNews = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (dsNews != null && dsNews.Tables[1] != null && dsNews.Tables[1].Rows.Count > 0)
            {
                ExpirednewsList = (from DataRow dr in dsNews.Tables[1].Rows
                                   select new StudentHomeModel.StudentNews
                                   {
                                       Day = dr["DD"].ToString(),
                                       Month = dr["MM"].ToString(),
                                       //Link = dr["LINK"].ToString(),
                                       Link = checkFile(dr["FILENAME"].ToString()),
                                       Title = dr["TITLE"].ToString(),
                                       NewsDesc = dr["NEWSDESC"].ToString()
                                   }).ToList();
            }
            else
            {
                ExpirednewsList = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.BindListViewExpiredNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return ExpirednewsList;
    }
    /****** Bind Body of Time Table ************/
    //not in use prashantg-290324
    [WebMethod]
    public static List<StudentHomeModel.StudentTimeTable> ShowStudTimeTableData()
    {
        StudeHome a = new StudeHome();
        List<StudentHomeModel.StudentTimeTable> studTTData = a.BindTimeTable();
        return studTTData;
    }
    private List<StudentHomeModel.StudentTimeTable> BindTimeTable()
    {
        List<StudentHomeModel.StudentTimeTable> objTTList = new List<StudentHomeModel.StudentTimeTable>();
        try
        {
            DataSet dsTimeTable = new DataSet();
            int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + Convert.ToInt32(Session["idno"] + ""))) == '0' ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + Convert.ToInt32(Session["idno"] + "")));
            Session["sessionno"] = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SES ON (SES.SESSIONNO=SR.SESSIONNO)", "MAX(SR.SESSIONNO)", "SR.IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(SES.FLOCK,0)=1 AND COLLEGE_ID=" + college_id + "") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SES ON (SES.SESSIONNO=SR.SESSIONNO)", "MAX(SR.SESSIONNO)", "SR.IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(SES.FLOCK,0)=1 AND COLLEGE_ID=" + college_id + ""));
            Session["schemeno"] = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT SCHEMENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT SCHEMENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            Session["semesterno"] = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(Session["sessionno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno"].ToString())));
            if (Session["sessionno"].ToString() == null)
            {
                Response.Redirect("~/default.aspx");
            }
            int sessionno = Convert.ToInt32(Session["sessionno"].ToString());
            int schemeno = Convert.ToInt32(Session["schemeno"].ToString());
            int semesterno = Convert.ToInt32(Session["semesterno"].ToString());
            int idno = Convert.ToInt32(Session["idno"].ToString());
            // Added on 07-04-2020
            int sectionNo = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SectionNo)", "IDNO=" + idno + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SectionNo)", "IDNO=" + idno + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno));
            // Added on 07-04-2020
            dsTimeTable = objSC.RetrieveStudentTimeTableDetails(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), idno, Convert.ToInt32(sectionNo));
            if (dsTimeTable != null)
            {
                if (dsTimeTable.Tables[0] != null && dsTimeTable.Tables[0].Rows.Count > 0)
                {
                    objTTList = (from DataRow dr in dsTimeTable.Tables[0].Rows
                                 select new StudentHomeModel.StudentTimeTable
                                 {
                                     //
                                     Monday = dr[0].ToString(),
                                     Tuesday = dr[1].ToString(),
                                     Wednesday = dr[2].ToString(),
                                     Thursday = dr[3].ToString(),
                                     Friday = dr[4].ToString(),
                                     Saturday = dr[5].ToString(),
                                     Slot = dr[6].ToString(),
                                     //Sunday = dr[6].ToString()
                                 }).ToList();
                }
                else
                {
                    objTTList = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.BindTimeTable-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return objTTList;
    }
    /****** Bind Body of Time Table ************/

    /****** Bind  Assignment and Announcement ************/
    //[WebMethod]
    //public static StudentHomeModel.GetNoticeData ShowNAnnAssData()
    //{
    //    StudeHome a = new StudeHome();
    //    StudentHomeModel.GetNoticeData AssignAnnouncmentData = a.BindAssignAnnouncment();
    //    return AssignAnnouncmentData;
    //}
    //private StudentHomeModel.GetNoticeData BindAssignAnnouncment()
    //{
    //    StudentHomeModel.GetNoticeData objGetNoticeAnnouncementData = new StudentHomeModel.GetNoticeData();
    //    try
    //    {
    //        List<StudentHomeModel.StudentNotice> NoticeList = new List<StudentHomeModel.StudentNotice>();

    //        NewsController objNc = new NewsController();


    //        DataSet dsGetAnnouncementCount = objSC.GetAnnouncementAssignmentCount(Convert.ToInt32(Session["idno"].ToString()));

    //        if (dsGetAnnouncementCount != null)
    //        {

    //            // objGetNoticeAnnouncementData.NoticeList = NoticeList;
    //            objGetNoticeAnnouncementData.Announcement = Convert.ToInt32(dsGetAnnouncementCount.Tables[1].Rows[0]["ANNOUNCEMENT"]).ToString("00");
    //            objGetNoticeAnnouncementData.Assignment = Convert.ToInt32(dsGetAnnouncementCount.Tables[0].Rows[0]["ASSIGNMENT"]).ToString("00");
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) != true)
    //            objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    return objGetNoticeAnnouncementData;
    //}
    /****** Bind  Assignment and Announcement ************/

    /******************* FOR QUICK ACCESS *********************/
    [WebMethod]
    public static List<StudentHomeModel.StudQuickAccess> ShowQuickAccessData()
    {
        StudeHome a = new StudeHome();
        List<StudentHomeModel.StudQuickAccess> QuickAccess = a.GetQuickAccessData();
        return QuickAccess;
    }
    private List<StudentHomeModel.StudQuickAccess> GetQuickAccessData()
    {
        List<StudentHomeModel.StudQuickAccess> objQA = new List<StudentHomeModel.StudQuickAccess>();
        try
        {
            DataSet ds = new DataSet();

            int UserTypeId = Convert.ToInt32(Session["userno"]);
            ds = objSC.GetQuickAccessForStudentDashboard(UserTypeId);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objQA = (from DataRow dr in ds.Tables[0].Rows
                             select new StudentHomeModel.StudQuickAccess
                             {
                                 PageNo = Convert.ToInt32(dr[0].ToString()),
                                 LinkName = dr[1].ToString(),
                                 Link = dr[2].ToString()
                             }).ToList();
                }
                else
                {
                    objQA = null;
                }
            }
            else
            {
                objQA = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studeHome.aspx.GetQuickAccessData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return objQA;
    }
    /******************* FOR QUICK ACCESS *********************/

    /********************FOR STUDENT TASKS**************************/
    [WebMethod]
    public static List<StudentHomeModel.StudentTask> ShowStudTasks()
    {
        StudeHome a = new StudeHome();
        List<StudentHomeModel.StudentTask> objETList = a.BindListViewTask();
        return objETList;
    }

    private List<StudentHomeModel.StudentTask> BindListViewTask()
    {
        List<StudentHomeModel.StudentTask> TaskList = new List<StudentHomeModel.StudentTask>();
        try
        {

            int ua_no = Convert.ToInt32(Session["userno"]);
            int ua_type = Convert.ToInt32(Session["usertype"]);

            DataSet dsTasks = objSC.GetTaskForStudentDashboard(ua_type, ua_no);
            if (dsTasks != null && dsTasks.Tables[0] != null && dsTasks.Tables[0].Rows.Count > 0)
            {
                TaskList = (from DataRow dr in dsTasks.Tables[0].Rows
                            select new StudentHomeModel.StudentTask
                            {

                                PageNo = Convert.ToInt32(dr[0].ToString()),
                                LinkName = dr[1].ToString(),
                                Link = dr[2].ToString()
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "studehome.aspx.BindListViewTask-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return TaskList;
    }

    /********************FOR STUDENT TASKS**************************/
    /***************************************** ADDED ON 02-04-2020 **********************************************/
    //-------------------Student Attendance Percentage-------------------------------//

    [WebMethod]
    public static string ShowAttPer()
    {
        StudeHome a = new StudeHome();
        string LblPer = a.BindStudentAttedencePer();
        return LblPer;

    }

    private string BindStudentAttedencePer()
    {
      
            DataSet ds = null;
            string AttPer = "0.00";
            try
            {
                int userno = Convert.ToInt32(Session["idno"]);

                if (userno > 0)
                {
                    ds = objSC.GetStudentAttPerDashboard(userno);
                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        AttPer = ds.Tables[0].Rows[0]["PER"].ToString();
                    }
                    else
                    {
                        AttPer = null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "studehome.aspx.BindStudentAttedencePer-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }

        return AttPer;
    }

    protected void btnpayonline_Click(object sender, EventArgs e)
    {
        string Pagename = "Online Payment";
        int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "TOP 1 AL_No", "AL_Link = '" + Convert.ToString(Pagename) + "'"));
        Response.Redirect("~/Academic/OnlinePayment.aspx?pageno=" + pageno);
    }

    protected void btnoutfees_Click(object sender, EventArgs e)
    {
        string Pagename = "Online Payment";
        int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "TOP 1 AL_No", "AL_Link = '" + Convert.ToString(Pagename) + "'"));
        Response.Redirect("~/Academic/OnlinePayment.aspx?pageno=" + pageno);

    }

    

}