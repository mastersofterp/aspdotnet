//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Activity Registraton                 
// CREATION DATE : 20-SEP-2022                                                      
// CREATED BY    : NIKHIL SHENDE 
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//=============================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;
using System.IO;

public partial class ACADEMIC_StudentAchievement_Activity_Registration : System.Web.UI.Page
{

    Common objCommon = new Common();
    Activity_RegistrationController objar = new Activity_RegistrationController();
    string PageId;
    //string Page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    int idno;
                    idno = Convert.ToInt32(Session["idno"]);
                    int ua_no = Convert.ToInt32(Session["usertype"]);
                    BindListView(idno);
                    BindListViewForActiveList();
                    //divRegActList.Visible = false;
                }
                else
                {
                    objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);
                    divactivity.Visible = false;
                    return;
                }
            }
        }
    }

    protected void BindListView(int idno)
    {
        try
        {
            DataSet ds = objar.ActivityRegistrationListView(Convert.ToInt32(Session["idno"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlEventNoDataMsg.Visible = false;
                pnlEvent.Visible = true;
                lvActivityList.DataSource = ds.Tables[0];
                lvActivityList.DataBind();
            }
            else
            {
                pnlEventNoDataMsg.Visible = true;
                pnlEvent.Visible = false;
                lvActivityList.DataSource = null;
                lvActivityList.DataBind();
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


    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        int count = 0;
        if (lvActivityList.Items.Count > 0)
        {
            foreach (ListViewDataItem item in lvActivityList.Items)
            {

                //CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                CheckBox lblcheck = item.FindControl("chkAccept") as CheckBox;
                HiddenField hdnCreateEventId = item.FindControl("hdnCreateEventId") as HiddenField;
                HiddenField hdnClubNo = item.FindControl("hdnClubNo") as HiddenField;
                Label lblClubName = item.FindControl("lblClubName") as Label;
                Label lblActivitName = item.FindControl("lblActivitName") as Label;
                Label lblActivityTitle = item.FindControl("lblActivityTitle") as Label;
                //Label lblSdate = item.FindControl("lblSdate") as Label;
                //Label lblEdate = item.FindControl("lblEdate") as Label;
                //Label lblTime = item.FindControl("lblTime") as Label;
                Label lblRegistrationDate = item.FindControl("lblRegistrationDate") as Label;

                if (lblcheck.Checked == true)
                {
                    count++;
                    //if (lblActivitName == lblActivityTitle)
                    //{

                    //    objCommon.DisplayMessage(this, "You Can Not Add Same Event ", this.Page);
                    //    return;

                    //}
                }

                if (lblcheck.Checked == true)
                {
                    string clubname = lblClubName.Text;
                    int ClubNo = Convert.ToInt32(hdnClubNo.Value);
                    int CreateEvent_Id = Convert.ToInt32(hdnCreateEventId.Value);

                    string ActivityName = lblActivitName.Text;
                    //string ActivityTitle = lblActivityTitle.Text;
                    string RegistarionDate = lblRegistrationDate.Text;
                    //DateTime StartDate = Convert.ToDateTime(lblSdate.Text);
                    //string startDate = lblSdate.Text;
                    //DateTime EndDate = Convert.ToDateTime(lblEdate.Text);
                    //string endDate = lblEdate.Text;
                    //string Time = lblTime.Text;
                    //DateTime myDateTime = new DateTime();
                    //string Date1 = Convert.ToDateTime(lblRegistrationDate.Text).ToString("dd/MM/yyyy");
                    DateTime RegistrationDate = Convert.ToDateTime(lblRegistrationDate.Text);



                    DateTime CurrentDate = DateTime.Now;

                    //if (CurrentDate > RegistrationDate)
                    //{
                    //    ClearData();
                    //    objCommon.DisplayMessage(this.pnlEvent, clubname += "- Registration Last Date is Already Over !!", this.Page);
                    //    return;
                    //}


                    int Idno = Convert.ToInt32(Session["idno"]);
                    int ua_no = Convert.ToInt32(Session["usertype"]);

                    string create_id = string.Empty;
                    string RgDate = string.Empty;
                    create_id = objCommon.LookUp("ACD_ACTIVE_STUDENT_ACTIVITY", "CREATE_EVENT_ID", "IDNO=" + Idno + "AND CREATE_EVENT_ID=" + CreateEvent_Id);

                    if (create_id != string.Empty)
                    {
                        int CrtId = Convert.ToInt32(create_id);
                        if (CrtId == CreateEvent_Id)
                        {
                            //objCommon.DisplayMessage(this.Page,clubname+= "- You Are Already Registered This Event for Another Club", this.Page);
                            objCommon.DisplayMessage(this.Page, ActivityName += "- This Event You Are Already Registered for Another Club !!", this.Page);
                            ClearData();
                            return;
                        }
                    }

                    //CustomStatus cs = (CustomStatus)objar.InsertActiveActivityData(Idno, ClubNo, CreateEvent_Id, RegistrationDate, ua_no, 1);
                    int INS_STATUS = objar.InsertActiveActivityData(Idno, ClubNo, CreateEvent_Id, RegistrationDate, ua_no, 1);
                    CustomStatus cs = (CustomStatus)INS_STATUS;
                    if (INS_STATUS == -1001)
                    {
                        objCommon.DisplayMessage(this, "Registration Capacity is Full.", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                        //BindListViewForActiveList();
                        //ClearData();
                        //BindListView(Idno);
                        divRegActList.Visible = true;
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        //objCommon.DisplayMessage(this, "You can not applied same event at same day", this.Page);
                        objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                        BindListViewForActiveList();
                        ClearData();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                        BindListViewForActiveList();
                        ClearData();
                    }
                    //}
                }
            }

            //objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
            int IIdno = Convert.ToInt32(Session["idno"]);
            BindListViewForActiveList();

            ClearData();
            BindListView(IIdno);

            if (lvActivityList.Items.Count <= 0)
            {
                objCommon.DisplayMessage("Please Select Atleast one Event", this.Page);
                return;
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Atleast one Event", this.Page);
                return;
            }
        }
        //objCommon.DisplayMessage(this, "Please Select Atleast one Event", this.Page);
    }

    protected void BindListViewForActiveList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ACTIVE_STUDENT_ACTIVITY AC INNER JOIN ACD_ACHIEVEMENT_CREATE_EVENT CE ON(AC.CREATE_EVENT_ID=CE.CREATE_EVENT_ID) INNER JOIN ACD_CLUB_MASTER CM ON(AC.CLUBNO= CM.CLUB_ACTIVITY_NO) INNER JOIN ACD_CLUB_ACTIVITY_MASTER ACM ON(CE.ACTIVITY_TYPE=ACM.ACTIVITYID) ", "ACT_ID", "CM.CLUB_ACTIVITY_TYPE AS CLUB_NAME,ACM.ACTIVITY_NAME AS ACTIVITY_TYPE, CE.EVENT_TITLE AS ACTIVITY_TITLE,CE.STDATE AS ACTIVITY_START_DATE,CE.ENDDATE AS ACTIVITY_END_DATE, CE.ACTIVITY_TIME, AC.REGISTRATION_DATE AS REGISTRATION_LAST_DATE, AC.CREATED_ON AS REGISTERED_DATE", "Idno=" + Convert.ToInt32(Session["idno"]), "ACT_ID DESC");

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlRegstlstNoDataMsg.Visible = false;
                pnlRegstlst.Visible = true;
                ListView2.DataSource = ds.Tables[0];
                ListView2.DataBind();
            }
            else
            {
                pnlRegstlstNoDataMsg.Visible = true;
                pnlRegstlst.Visible = false;
                ListView2.DataSource = null;
                ListView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BindListViewForActiveList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        ClearData();
        //divRegActList.Visible = false;
    }

    protected void ClearData()
    {
        foreach (ListViewDataItem item in lvActivityList.Items)
        {
            CheckBox check = item.FindControl("chkAccept") as CheckBox;
            check.Checked = false;
        }
    }
    protected void btnDeleteActvity_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnDelete = sender as ImageButton;
            //LinkButton btnEdit = sender as LinkButton;
            ////int ActID = Convert.ToInt32(btnDelete.CommandArgument);

            string[] commandArgs = btnDelete.CommandArgument.ToString().Split(new char[] { ',' });
            int ActID = Convert.ToInt32(commandArgs[0]);
            string Act_StartDate = commandArgs[1];

            if (!string.IsNullOrEmpty(Act_StartDate) && (Act_StartDate.Contains("/") || Act_StartDate.Contains("-")))
            {

                DateTime dtStartDate = DateTime.Parse(Act_StartDate);
                DateTime dtRegDate = DateTime.Today;

                if (dtRegDate > dtStartDate)
                {
                    objCommon.DisplayMessage(this, "Can Not Deleted Back Dated Activities.", this.Page);
                    return;
                }

            }

            ViewState["Edit"] = ActID;
            ViewState["Action"] = "Delete";
            //ShowDetail(ActivityID);

            int idno;
            idno = Convert.ToInt32(Session["idno"]);
            CustomStatus cs = (CustomStatus)objar.DeleteActivityRegistration(idno, ActID);

            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(this.Page, "Record Deleted Successfully", this.Page);
                BindListView(idno);
                BindListViewForActiveList();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this.Page, "Record Deleted Failed", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Something Went Wrong", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_Registration.btnDeleteActvity_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}