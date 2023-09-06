using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;



public partial class ACADEMIC_ClubActivityRegistrationReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //StudentController objSC = new StudentController();
    ClubController OBJCLUB = new ClubController();
    string PageId;
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
            //FillDropDown();
            //BindListView();

            // objCommon.FillDropDownList(ddlInstMultiCheck, "CLUB_ACTIVITY_MASTER", "CLUB_NO", "TYPE_OF_ACTIVITY", "CLUB_NO>0", "CLUB_NO");
            //objCommon.FillListBox(ddlsubuser, "CLUB_ACTIVITY_MASTER", "CLUB_NO", "TYPE_OF_ACTIVITY", "CLUB_NO>0", "CLUB_NO");

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //populateDropDown();


                if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 1)
                {
                    pnlMain.Visible = true;
                    //if (CheckActivity())
                    //{
                    //    pnlMain.Visible = true;
                    //    ShowStudentDetails();
                    //}
                    //else
                    //{
                    //    pnlMain.Visible = false;
                    //}
                    //BindListView();

                }
                else
                {
                  //  objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);
                    pnlMain.Visible = false;
                    //div3.Visible = false;
                    //lvPraticipation.Visible = false;

                   // return;

                    Page.Title = Session["coll_name"].ToString();

                    PageId = Request.QueryString["pageno"];
                   
                }
               // objCommon.FillListBox(ddlsubuser, "CLUB_MASTER", "CLUB_NO", "CLUB_ACTIVITY_TYPE", "CLUB_NO>0", "CLUB_NO");
                populateDropDown();
                    
            }
        }



        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

    }
    //}

    private void populateDropDown()
    {
        objCommon.FillDropDownList(ddlclub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
        //objCommon.FillDropDownList(ddlCollege, "acd_college_scheme_mapping", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO > 0", "COSCHNO DESC");
        objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");

       
         //DataSet ds = OBJCLUB.clubCollegesession(1,Session["college_nos"].ToString());

         //if (ds.Tables.Count > 0)
         //{
         //    if (ds.Tables[0].Rows.Count > 0)
         //    {

         //        //ddlSession.Items.Clear();
         //        ddlSession.Items.Add("Please Select");
         //        //ddlSession.SelectedItem.Value = "0";
         //        ddlSession.DataSource = ds;
         //        ddlSession.DataValueField = ds.Tables[0].Columns["SESSIONNO"].ToString();
         //        ddlSession.DataTextField = ds.Tables[0].Columns["COLLEGE_SESSION"].ToString();
         //        ddlSession.DataBind();
         //       // ddlSession.SelectedIndex = 0;
         //    }
         //    else
         //    {
         //        ddlSession.Items.Clear();
         //        ddlSession.Items.Add("Please Select");
         //        //ddlSession.SelectedItem.Value = "0";
         //    }
         //}
        
    
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {

        try
        {
           
            GridView gv = new GridView();
            int clubno = Convert.ToInt32(ddlclub.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            DataSet ds = OBJCLUB.GetClubRegistrationDetailsReport(clubno,collegeid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();

                string Attachment = "Attachment; filename=" + "ClubRegistrationReport.xls";
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
                objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
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
        ddlclub.SelectedIndex = 0;
    }
}