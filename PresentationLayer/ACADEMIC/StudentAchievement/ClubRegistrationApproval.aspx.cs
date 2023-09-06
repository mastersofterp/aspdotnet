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

public partial class ClubRegistrationApproval : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objstudent = new Student();
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
            
            //objCommon.FillListBox(ddlsubuser, "CLUB_ACTIVITY_MASTER", "CLUB_NO", "TYPE_OF_ACTIVITY", "CLUB_NO>0", "CLUB_NO");

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //populateDropDown();


                if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 8)
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
                
               // populateDropDown();
                fillapproveDropDown();
            }
        }


        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

    }

    private void populateDropDown()
    {
       // objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");

      //  objCommon.FillDropDownList(ddlCollege, "acd_college_scheme_mapping", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO > 0", "COSCHNO DESC");
        if (Convert.ToInt32(Session["usertype"]) == 1)
        {
            objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");

            objCommon.FillDropDownList(ddlclub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
        }
        //else if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 8)
        //{
        //    objCommon.FillDropDownList(ddlclub, "ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "app_no", "CLUB_ACTIVITY_TYPE", "app_no>0", "app_no");
        //    objCommon.FillDropDownList(ddlCollege, "ACD_CLUB_AUTHORITY_APPROVAL_MASTER s INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=COLLEGE_NO)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");
        //}
      
    }

    private void fillapproveDropDown()
    {


        string APPROVAL_1_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "isnull (APPROVAL_1_UANO,0)APPROVAL_1_UANO", "app_no>0 and APPROVAL_1_UANO=" + Convert.ToInt32(Session["userno"]));
        string APPROVAL_2_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "isnull(APPROVAL_2_UANO,0)APPROVAL_2_UANO", "app_no>0 and APPROVAL_2_UANO=" + Convert.ToInt32(Session["userno"]));
           

            if (Session["userno"].Equals(APPROVAL_1_UANO))
            {

               
                objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");
                //objCommon.FillDropDownList(ddlclub, "ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0", "CLUB_ACTIVITY_NO");
                objCommon.FillDropDownList(ddlclub, "ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 and APPROVAL_1_UANO=" + Convert.ToInt32(Session["userno"]), "CLUB_ACTIVITY_NO ");

               

            }
            else if (Session["userno"].Equals(APPROVAL_2_UANO))
            {
              
                objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");
               // objCommon.FillDropDownList(ddlclub, "ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0", "CLUB_ACTIVITY_NO");
                objCommon.FillDropDownList(ddlclub, "ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 and APPROVAL_2_UANO=" + Convert.ToInt32(Session["userno"]), "CLUB_ACTIVITY_NO ");



            }
       // }
    }

    private void BindListViewapprovedstud(int status)
    {
        int userNo = Convert.ToInt32(Session["userno"].ToString());
        int userType = Convert.ToInt32(Session["usertype"].ToString());
        int clubactivityno = Convert.ToInt32(ddlclub.SelectedValue);
          // int clubactivityno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER","CLUB_ACTIVITY_NO",""));
        try
        {
            DataSet dsColleges = objSC.GetCollegesByUser_For_Club(Convert.ToInt32(Session["userno"]));
            string college = string.Empty;
            int actual_College = Convert.ToInt32(ddlCollege.SelectedValue);
          
         
           string APPROVAL_1_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER","APPROVAL_1_UANO","CLUB_ACTIVITY_NO="+ clubactivityno+" AND APPROVAL_1_UANO=" + userNo + " AND APPROVAL_1_UA_TYPE=" + userType + "");
            
            ViewState["APPROVAL_1_UANO"] = APPROVAL_1_UANO;
            string APPROVAL_2_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", " DISTINCT APPROVAL_2_UANO", "CLUB_ACTIVITY_NO="+ clubactivityno+"  AND APPROVAL_2_UANO=" + userNo + " AND APPROVAL_2_UA_TYPE=" + userType + " ");
            ViewState["APPROVAL_2_UANO"] = APPROVAL_2_UANO;
            string APPROVAL_3_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", " DISTINCT APPROVAL_3_UANO", "CLUB_ACTIVITY_NO="+ clubactivityno+" AND APPROVAL_3_UANO=" + userNo + " AND APPROVAL_3_UA_TYPE=" + userType + "");
            ViewState["APPROVAL_3_UANO"] = APPROVAL_3_UANO;
            string APPROVAL_4_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "DISTINCT APPROVAL_4_UANO", "CLUB_ACTIVITY_NO=" + clubactivityno + "  AND APPROVAL_4_UANO=" + userNo + " AND APPROVAL_4_UA_TYPE=" + userType + " ");
            ViewState["APPROVAL_4_UANO"] = APPROVAL_4_UANO;
            string APPROVAL_5_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "DISTINCT APPROVAL_5_UANO", "CLUB_ACTIVITY_NO=" + clubactivityno + "  AND APPROVAL_5_UANO=" + userNo + " AND APPROVAL_5_UA_TYPE=" + userType + " ");
            ViewState["APPROVAL_5_UANO"] = APPROVAL_5_UANO;
            
         
            DataSet ds = objSC.GetApprovedStudListCLUB(objstudent, Convert.ToInt32(Session["userno"].ToString()), actual_College, userType, clubactivityno, clubactivityno);
            if (ds.Tables.Count == 0)
            {
                objCommon.DisplayMessage(this.Page, "Records Not Found.", this.Page);
                return;
            }
           
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvclubRegapprove.DataSource = ds;
                lvclubRegapprove.DataBind();
                lvclubRegapprove.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvclubRegapprove);//Set label -
     

               
                
                foreach (ListViewDataItem item in lvclubRegapprove.Items)
                {
                    HiddenField hfidno = (item.FindControl("hfidno") as HiddenField);
                    HiddenField hdnCumulativeNo = (item.FindControl("hdnCumulativeNo") as HiddenField);
                    HiddenField hdclub = item.FindControl("hdclubno") as HiddenField;
                    CheckBox appstatus = item.FindControl("chkapprove") as CheckBox;
                    Label lblstatus = item.FindControl("lbltype") as Label;
                  
                    string Approval_1 = objCommon.LookUp("ACD_CLUB_ACTIVITY_REGISTRATION", "APPROVAL_1", "IDNO=" + hfidno.Value + "AND CLUBACTIVITY_TYPE=" + hdnCumulativeNo.Value + "AND CLUB_NO=" + hdclub.Value);
                    string Approval_2 = objCommon.LookUp("ACD_CLUB_ACTIVITY_REGISTRATION", "APPROVAL_2", "IDNO=" + hfidno.Value + "AND CLUBACTIVITY_TYPE=" + hdnCumulativeNo.Value + "AND CLUB_NO=" + hdclub.Value);
                    string approve = lblstatus.ToolTip;

                 
                    if (Session["userno"].Equals(APPROVAL_1_UANO))
                    {

                        if (Approval_1 == "1")
                        {
                         
                            appstatus.Enabled = false;
                            appstatus.Checked = true;

                        }
                    }
                    else if (Session["userno"].Equals(APPROVAL_2_UANO))
                    {

                        if (Approval_1 == "1" && Approval_2=="1")
                        {

                           
                            appstatus.Enabled = false;
                            appstatus.Checked = true;

                        } 
                    }
                    
            

                }
                lvclubRegapprove.Visible = true;
               
            }
        
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);
        
                lvclubRegapprove.Visible = false;
               
                return;
            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnapprove_Click(object sender, EventArgs e)
 {
        try
        {
              int count = 0;
              int userNo = Convert.ToInt32(Session["userno"].ToString());
             int userType = Convert.ToInt32(Session["usertype"].ToString());
           //  string APPROVAL_1_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "isnull (APPROVAL_1_UANO,0)APPROVAL_1_UANO", "app_no>0 and APPROVAL_1_UANO=" + Convert.ToInt32(Session["userno"]));
           //  string APPROVAL_2_UANO = objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "isnull(APPROVAL_2_UANO,0)APPROVAL_2_UANO", "app_no>0 and APPROVAL_2_UANO=" + Convert.ToInt32(Session["userno"]));

            Boolean IsRecordUpdated = false;
            foreach (ListViewDataItem item in lvclubRegapprove.Items)
            {
                CheckBox chk = item.FindControl("chkapprove") as CheckBox;
                Label lbclub = item.FindControl("lblname") as Label;
                HiddenField hdCumulativeNo = item.FindControl("hdnCumulativeNo") as HiddenField;
                HiddenField hdclub = item.FindControl("hdclubno") as HiddenField;



                //if (Session["userno"].Equals(APPROVAL_1_UANO))
                //{

                //    int clubactivityno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "app_no>0 and APPROVAL_1_UANO=" + Convert.ToInt32(Session["userno"]) + " and  club=" + Convert.ToInt32(ddlclub.SelectedValue)));
                //}
                //else if (Session["userno"].Equals(APPROVAL_2_UANO))
                //{

                //    int clubactivityno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "app_no>0 and APPROVAL_2_UANO=" + Convert.ToInt32(Session["userno"]) + " and club=" + Convert.ToInt32(ddlclub.SelectedValue)));

                //}
                HiddenField hdclubactivityno = item.FindControl("hdclubactivityno") as HiddenField;
                //int clubactivityno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER","CLUB_ACTIVITY_NO","app_no>0 and APPROVAL_1_UANO=" + Convert.ToInt32(Session["userno"])));

               // int clubactivityno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "app_no>0 and APPROVAL_1_UANO=" + Convert.ToInt32(Session["userno"])));
              // int clubactivityno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_AUTHORITY_APPROVAL_MASTER", "CLUB_ACTIVITY_NO", "app_no>0 and APPROVAL_1_UANO and APPROVAL_2_UANO"));

                
                //HiddenField hdclubactivityno = item.FindControl("CLUB_ACTIVITY_NO") as HiddenField;
                int id = Convert.ToInt32(chk.ToolTip.ToString());
                int stat = 0, type = 0, club = 0, clubactivityno=0;
                if (chk.Checked == true && chk.Enabled == true)
                {

                    IsRecordUpdated = true;
                    stat = 1;
                    type = Convert.ToInt32(hdCumulativeNo.Value);
                    club = Convert.ToInt32(hdclub.Value);
                    clubactivityno = Convert.ToInt32(hdclubactivityno.Value);
                    CustomStatus cs = (CustomStatus)OBJCLUB.Club_Registration_approve_students(id, userNo, userType, stat, type, club, clubactivityno);
                    //CustomStatus cs = (CustomStatus)OBJCLUB.Club_Registration_approve_students(id, userNo, userType, stat, type, club);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        count++;
                    }

                }
            }

            if (count < 1 && IsRecordUpdated)
            {
                objCommon.DisplayMessage(this.Page, "No Records Updated..!!", this.Page);
                return;
            }
            else if (count < 1)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student..!!", this.Page);
            }
            else if (count > 0)
            {
                objCommon.DisplayMessage(this.Page, "Student Approved Successfully.", this.Page);
                BindListViewapprovedstud(-1);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_PHDVerifiactionPortal.btnApprove_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListViewapprovedstud(-1);
        
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView gv = new GridView();
          
            int club = Convert.ToInt32(ddlclub.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            DataSet ds = OBJCLUB.GetClubRegistrationApprovalStudentReport(club,collegeid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();

                string Attachment = "Attachment; filename=" + "ClubRegistrationApprovalReport.xls";
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
}