
//======================================================================================
// PROJECT NAME  : RFC CODE                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : MAINTAIN LOG SEND BULK EMAIL SMS                                          
// CREATION DATE : 22-01-2024                                                         
// CREATED BY    : SAKSHI MAKWANA                                 
// MODIFIED DATE :                                        
// MODIFIED DESC :                                         
//======================================================================================


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
using System.IO;
using ClosedXML.Excel;


public partial class ACADEMIC_BulkEmailLog_Excel : System.Web.UI.Page
{
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
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
                //Page Authorization
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
             DataSet ds = objCommon.FillDropDown("ACD_BULK_EMAIL_SMS_WHATSAPP_LOG", "Distinct ACTIVITY_NAME", "ACTIVITY_NAME", "Org_id=" + Convert.ToInt32(Session["OrgId"]) + " ", "");
             ddlActivity.Items.Add(new ListItem("Please Select", "0"));
             ddlActivity.DataSource = ds;
             ddlActivity.DataTextField = "ACTIVITY_NAME";
             ddlActivity.DataBind();
         
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlActivity.SelectedValue == "0" || ddlActivity.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(this, "Please Select Activity ", this.Page);
            return;
        }
        else
        {
            string Activity = ddlActivity.SelectedItem.Text;
            DataSet ds = GetLogDetail(Activity, Convert.ToInt32(Session["OrgId"]));
            GridView GV = new GridView();
            //**************************************************************************************
            //GetAttendanceForAll(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector)

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                // string sem = "AllSemester";


                string attachment = "attachment; filename=EmailSMS_Log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                //Response.Flush();
                Response.End();
            }
            else
            {
                objCommon.DisplayUserMessage(this, "No Record Found.", this.Page);
            }

        }
    }


    //Added By Sakshi M on 22-01-2024
    public DataSet GetLogDetail(string Activity, int org)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@P_ACTIVITY", Activity);
            objParams[1] = new SqlParameter("@P_ORG", org);


            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EMAIL_SMS_LOG_DETAIL", objParams);
        }
        catch (Exception ex)
        {

            throw new IITMSException("Get_Students_FacultyDiary->" + ex.ToString());
        }
        return ds;
    }
}

