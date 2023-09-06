//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : CANCEL ROOM ALLOTMENT                                                
// CREATION DATE : 26-NOV-2010                                                          
// CREATED BY    : GAURAV SONI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


using System.Collections;
using System.Configuration;

using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;
using HostelBusinessLogicLayer.BusinessLogic;

public partial class Hostel_ChangeRoomAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    RoomAllotmentController raController = new RoomAllotmentController();
    RoomAllotment objRM = new RoomAllotment();
    HostelFeeCollectionController objFee = new HostelFeeCollectionController();

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
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session

                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    
                    //Fill Session Dropdown
                    objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1 AND OrganizationId=" + Session["OrgId"] + "", "HOSTEL_SESSION_NO DESC");
                    objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0 AND OrganizationId=" + Session["OrgId"] + "", "HOSTEL_NAME");
                
                    ddlSession.SelectedIndex = 1;

                    // Commented by Saurabh L on 28/06/2023 Purpose: Demand update option in change Hostel Room Allotment given to All client
                    //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
                    //{
                    //    divUpdateDemand.Visible = true;
                    //}
                    //else
                    //{
                    //    divUpdateDemand.Visible = false;
                    //}
                   
                    btnShow_Click(sender, e);
                }
            }
            else
            {

                if (Page.Request.Params["__EVENTTARGET"] != null)
                {
                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                    {
                        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                        bindlist(arg[0], arg[1]);
                    }

                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                    {
                        txtSearch.Text = string.Empty;
                        lblNoRecords.Text = string.Empty;
                        // Added By Sonali Bhor on date 17/02/2023 to clear data of modal on cancel
                        rbRegNo.Checked = true;
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                    }
                }
            }
            if (Request.QueryString["id"] != null)
            {
                txtEnrollNo.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
                //if (Convert.ToInt32(Request.QueryString["id"].ToString()) == 0)
                //FillInformation();
                this.DisplayInformation(Convert.ToInt32(Request.QueryString["id"].ToString()));
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Hostel_CancelRoomAllotment.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Hostel_CancelRoomAllotment.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string studentId = string.Empty;

        try
        {
            if (Request.QueryString["id"] != null)
            {
                studentId = Request.QueryString["id"].ToString();
            }

            FeeCollectionController feeController = new FeeCollectionController();
            studentId = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_STUDENT S ON (S.IDNO=H.RESIDENT_NO)", "H.RESIDENT_NO", "H.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND RESIDENT_NO ='" + studentId + "'");
            if (studentId != "")
            {
                this.DisplayInformation(Convert.ToInt32(studentId));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
    private void DisplayInformation(int residentNo)
    {
        try
        {
            DataSet ds = null;
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            /// Display student's personal and academic data in 
            /// student information section
            FeeCollectionController feeController = new FeeCollectionController();
            ds = feeController.GetStudentInfoById(residentNo, OrganizationId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                /// show student information
                this.PopulateStudentInfoSection(dr);
            }
            else
            {
                //ShowMessage("Unable to retrieve student's record.");
               objCommon.DisplayMessage(this.Page, "Unable to retrieve student's record.", this.Page);
                return;
            }

            // Save resident no. for further transaction
            ViewState["ResidentNo"] = residentNo;

            /// Show room allotment details (if exists) for the student
               // session no added to get current 
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            ds = raController.GetRoomAllotmentInfoByResidentNo(residentNo, OrganizationId, sessionno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
              
             
                btnCancel.Visible = true;
                /// save room allotment no to be used while saving the record
                //ViewState["RoomAllotmentNo"] = ds.Tables[0].Rows[0]["ROOM_ALLOTMENT_NO"].ToString();

                ViewState["OldroomNo"] = ds.Tables[0].Rows[0]["ROOM_NO"].ToString();
                ViewState["OldhostelNo"] = ds.Tables[0].Rows[0]["HBNO"].ToString();
                ViewState["HOSTEL_NAME"] = ds.Tables[0].Rows[0]["HOSTEL_NAME"].ToString();
                ViewState["BLOCK_NAME"] = ds.Tables[0].Rows[0]["BLOCK_NAME"].ToString();
                ViewState["ROOM_NAME"] = ds.Tables[0].Rows[0]["ROOM_NAME"].ToString();
                lbloldhostel.Text = ds.Tables[0].Rows[0]["HOSTEL_NAME"].ToString();
               lblroomname.Text = ds.Tables[0].Rows[0]["ROOM_NAME"].ToString();

               //lvAllotmentDetails.DataSource = ds;
               //lvAllotmentDetails.DataBind();
              
            }
            else
            {
                btnsubmit.Visible = false;
                btnCancel.Visible = false;
                //this.ShowMessage("No room allotment found for this student.");
                objCommon.DisplayMessage(this.updEdit, "No room allotment found for this student in current session.", this.Page);           
                btnCancel.Visible = true;
                btnCancel.Text = "Back";
                // added by Sonali for not allowing room change without having previous room allotment on 22/02/2023
                ddlHostelNo.Enabled = false;
                ddlRoomNo.Enabled = false;
            }

            /// Hide search student div and student list so that
            /// once a student's data is populated, user should not select
            /// other student to show information, unless and until user 
            /// clicks the cancel button.
            //divStudentSearch.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.DisplayStudentInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            /// Bind data with labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            txtEnrollNo.Text  = dr["REGNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();

            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the record.
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();

            divStudInfo.Visible = true;
            divshifthostel.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["pageno"] != null)
        {
            Response.Redirect("ChangeHostelRoomAllotment.aspx?pageno=" + Request.QueryString["pageno"]);
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "'" + message + "'", "Showalert();", true);
        }
    }
    protected void ddlHostelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            lblcapacity.Text = "";
           
            ddlRoomNo.SelectedIndex = 0;
            btnsubmit.Visible = false;
            objCommon.FillDropDownList(ddlRoomType, "ACD_HOSTEL_ROOMTYPE_MASTER", "DISTINCT TYPE_NO	", "ROOMTYPE_NAME", " HOSTEL_NO=" + ddlHostelNo.SelectedValue + " AND OrganizationId=" + Session["OrgId"], "TYPE_NO");
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    //FUNCTION OF SEARCH POP UP
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Response.Redirect(url + "&id=" + lnk.CommandArgument);
    }

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetails(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string  capacity = string.Empty ;
        string allotedseat = string.Empty;
        int availableseat = 0;
        capacity = objCommon.LookUp("ACD_HOSTEL_ROOM", "ISNULL(CAPACITY,0)CAPACITY", "ROOM_NO=" + ddlRoomNo.SelectedValue + " AND HBNO=" + ddlHostelNo.SelectedValue + " AND OrganizationId=" + Session["OrgId"]);
        allotedseat = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT ", " COUNT(ROOM_NO)", "ISNULL(CAN,0)=0 AND HOSTEL_SESSION_NO =" + ddlSession.SelectedValue + " AND  ROOM_NO=" + ddlRoomNo.SelectedValue + " AND HBNO=" + ddlHostelNo.SelectedValue +   " AND OrganizationId=" + Session["OrgId"] + "  GROUP BY ROOM_NO") ;
        if (allotedseat == "")
        {
            allotedseat = "0";
        }
        if (capacity == "")
        {
            capacity = "0";
        }
        availableseat = Convert.ToInt32(capacity) - Convert.ToInt32(allotedseat);
        lblcapacity.Text = "Total Seats :  " + capacity + "&nbsp;         Available Seats : " + availableseat;
        if (availableseat < 1)
        {
            lblmsg.Text = "Their is no vacancy in this room, all seats are alloted";
            btnsubmit.Visible = false;
        }
        else
        {
            lblmsg.Text = "";
            btnsubmit.Visible = true;
        }
        lblmsg.Attributes.Add("style", "background-color:Red;");
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            int UpdateDemand = 0;

            if (chkUpdateDemand.Checked == true)
            {
                UpdateDemand = 1;
            }

            string studentIDs = Convert.ToString(Request.QueryString["id"]);

            int studentId = Convert.ToInt32(Request.QueryString["id"]);

            int oldroomno = Convert.ToInt32(ViewState["OldroomNo"]);
            int oldhostelno = Convert.ToInt32(ViewState["OldhostelNo"]);

            int newroomno = Convert.ToInt32(ddlRoomNo.SelectedValue);
            int newhostelno = Convert.ToInt32(ddlHostelNo.SelectedValue);

            bool overwrite = false;

            int sesionno = Convert.ToInt32(ddlSession.SelectedValue);
            string remarks ="Room allotment has been changed successfully from "+  ViewState["HOSTEL_NAME"] + "   ( " + ViewState["ROOM_NAME"]  + " ) "  ;
            remarks = remarks + " to " + ddlHostelNo.SelectedItem.Text + " ( " + ddlRoomNo.SelectedItem.Text + " ) ";

            if (UpdateDemand == 1)
            {
                int ret = objFee.InsertHostelDemandLog(studentId, sesionno, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(Session["OrgId"]));

                string response = objFee.CreateHostelFeeDemand(studentIDs, "HF", Session["usertype"].ToString(), Convert.ToInt32(ddlSession.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ViewState["SemesterNo"]), overwrite, Convert.ToInt32(ViewState["SemesterNo"]), 1, newroomno);

            }
           
            if (raController.ChangeRoomAllotment(studentId, sesionno, oldroomno, newroomno, oldhostelno, newhostelno, remarks))
            {
                btnShow_Click(sender, e);
                Clear();
                lblmsg.Text = remarks;
                btnsubmit.Visible = false;
                lblmsg.Attributes.Add("style", "background-color:Green;");
            }
        
        }
    }
    private void Clear()
    {        
        ddlHostelNo.SelectedIndex = 0;
        ddlRoomNo.SelectedIndex = 0;
        ddlRoomType.SelectedIndex = 0;
        chkUpdateDemand.Checked = false;
        lblcapacity.Text = "";
        lblmsg.Text = "";
    }

    protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblcapacity.Text = "";

        ddlRoomNo.SelectedIndex = 0;
        btnsubmit.Visible = false;
        objCommon.FillDropDownList(ddlRoomNo, "ACD_HOSTEL_ROOM HR INNER JOIN ACD_HOSTEL H ON H.HOSTEL_NO=HR.HBNO INNER JOIN ACD_HOSTEL_ROOMTYPE_MASTER RM ON RM.TYPE_NO = HR.ROOM_TYPE", "DISTINCT ROOM_NO", "ROOM_NAME", " H.HOSTEL_NO=" + ddlHostelNo.SelectedValue + " AND HR.ROOM_TYPE=" + ddlRoomType.SelectedValue + " AND HR.OrganizationId=" + Session["OrgId"] , "ROOM_NAME");
    }
}