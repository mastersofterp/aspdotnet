using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Data;
public partial class SemesterRegistration_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                //CheckPageAuthorization();s
                //Set the Page Titles
                Page.Title = Session["coll_name"].ToString();

                //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", , "COLLEGE_ID");

                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ")", "C.COLLEGE_ID");

               
                

                //objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY A INNER JOIN ACTIVITY_MASTER B ON A.ACTIVITY_NO = B.ACTIVITY_NO  INNER JOIN ACD_SESSION_MASTER C ON SESSION_NO = SESSIONNO", "SESSIONNO", "SESSION_NAME", "PAGE_LINK LIKE '%" + ViewState["pageno"] + "%' AND STARTED = 1 and CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)", "");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER ", "SESSIONNO", "SESSION_NAME", "", "SESSIONNO");
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    

                // PopulateDropDown();
                //get ip address
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            divMsg.InnerHtml = string.Empty;
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gv = new GridView();
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            DataSet ds = objSC.GetSemesterRegistrationDetails(sessionno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();

                string Attachment = "Attachment; filename=" + "SemesterRegistrationReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.HeaderStyle.Font.Bold = true;
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_RefundReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
        {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "COLLEGE_ID=" + ddlcollege.SelectedValue, "SESSIONNO DESC");
        }
}