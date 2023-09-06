//========================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS AND EVENT MANAGEMENT
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 28-APR-2017
//DESCRIPTION   : THIS FORM IS USED TO CREATE EVENT APPROVAL FOR EVENTS.    
//=========================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class Sports_Transaction_EventApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();   

    EventApprovalEnt objEA = new EventApprovalEnt();
    EventApprovalCon objEACon = new EventApprovalCon();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
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
                //Page Authorization
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               
                pnlEdit.Visible = false;
                pnllist.Visible = true;
                int usernock = Convert.ToInt32(Session["userno"]);
                BindPendingEvents();

                //btnHidePanel.Visible = false;
                //trfrmto.Visible = false;
                //trbutshow.Visible = false;
                //txtFromdt.Text = System.DateTime.Now.ToString();
                //txtTodt.Text = System.DateTime.Now.ToString();
                ViewState["ModifyEvent"] = "add";
            }
        }

    }

    // This method is used to check the page authority.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    protected void BindPendingEvents()
    {
        try
        {

            DataSet ds = objEACon.GetEventsPendingList(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventPendingList.DataSource = ds;
                lvEventPendingList.DataBind();
                lvEventPendingList.Visible = true;
            }
            else
            {
                lvEventPendingList.DataSource = null;
                lvEventPendingList.DataBind();
                lvEventPendingList.Visible = false;
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventApproval.BindPendingEvents ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        txtfrmdt.Text = string.Empty;
        txttodate.Text = string.Empty;
        //Modified by Saahil Trivedi 08-02-2022
        divAuthority.Visible = false;
        divButtons.Visible = false;
    }

    private void clear_lblvalue()
    {
        lblEvent.Text = string.Empty;
        lblFromdt.Text = string.Empty;
        lblTodt.Text = string.Empty;
        lblOrganizingTeam.Text = string.Empty;
        lblCollege.Text = string.Empty;
        //txtRemarks.Text = string.Empty;       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //clear();
        ddlSelect.SelectedValue = "A";
        txtRemarks.Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlEdit.Visible = false;
        pnllist.Visible = true;
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
        divButtons.Visible = false;
        divAuthority.Visible = false;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
           
            objEA.PSID = Convert.ToInt32(ViewState["PSID"].ToString());
            objEA.UA_NO = Convert.ToInt32(Session["userno"]);
            objEA.Status = ddlSelect.SelectedValue.ToString();
            objEA.Remarks = txtRemarks.Text.ToString();


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (ViewState["ModifyEvent"].ToString().Equals("edit"))
                    {
                        CustomStatus cs = (CustomStatus)objEACon.UpdateApprovalAuthority(objEA);


                         objEA.PSID  = Convert.ToInt32(Session["PSID"]);
                         objEA.FROMDT = Convert.ToDateTime(txtfrmdt.Text);
                         objEA.TODT = Convert.ToDateTime(txttodate.Text);

                        CustomStatus lcs = (CustomStatus)objEACon.UpdatePlanSchedule(objEA);

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false;
                            pnlEdit.Visible = false;
                            pnllist.Visible = true;
                            ViewState["action"] = null;
                            clear_lblvalue();
                            clear();
                            BindPendingEvents();
                        }
                        ViewState["action"] = null;
                        ViewState["ModifyEvent"] = null;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objEACon.UpdateApprovalAuthority(objEA);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (ddlSelect.SelectedValue=="A")
                            {
                            MessageBox("Record Approved Successfully");
                            }
                            else{
                                MessageBox("Record Reject Successfully");
                            }
                            pnllist.Visible = true;
                            pnlAdd.Visible = false;
                            ViewState["action"] = null;
                            clear_lblvalue();
                            clear();
                            BindPendingEvents();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventApproval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void SendSMS(int letrno)
    {
        //Leaves objLM = new Leaves();      
        //string url = "http://smsnmms.co.in/sms.aspx";
        //string uid = string.Empty; string pass = string.Empty; string mobno = string.Empty; string message, Mobile_no = string.Empty;

        //DataSet dsReff = objCommon.FillDropDown("REFF", "SMSSVCID", "SMSSVCPWD", "", "");
        //if (dsReff.Tables[0].Rows.Count > 0)
        //{
        //    uid = dsReff.Tables[0].Rows[0]["SMSSVCID"].ToString();
        //    pass = dsReff.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
        //}
        //objLM.LETRNO = letrno;
        //DataSet ds = objApp.GetSMSInformation(objLM);

        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    string leavestatus = ds.Tables[0].Rows[0]["LeaveStatus"].ToString();
        //    string name = ds.Tables[0].Rows[0]["name"].ToString();
        //    string leavename = ds.Tables[0].Rows[0]["Leave_Name"].ToString();
        //    double tot_days = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
        //    string Joindt = ds.Tables[0].Rows[0]["Joindt"].ToString();
        //    string PHONENO = ds.Tables[0].Rows[0]["PHONENO"].ToString();           
        //}
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int PSID = int.Parse(btnApproval.CommandArgument);
            Session["PSID"] = PSID;
            ShowDetails(PSID);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            ViewState["action"] = "edit";
            divAdd.Visible = true;
           divButtons.Visible = true;
            divAuthority.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventApproval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int psid = int.Parse(btnApproval.CommandArgument);
            Session["psid"] = psid;
            divAdd.Visible = true;
            divEdit.Visible = true;
            divAuthority.Visible = true;
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            pnlEdit.Visible = true;
            //Modified by Saahil Trivedi 08-02-2022
            divButtons.Visible = true;
            ShowDetails(psid);          

            ViewState["action"] = "edit";
            ViewState["ModifyEvent"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventApproval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    private void ShowDetails(Int32 psid)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objEACon.GetEventDetail(psid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PSID"] = psid;
                lblEvent.Text = ds.Tables[0].Rows[0]["EVENTNAME"].ToString();
                lblFromdt.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                lblTodt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                lblOrganizingTeam.Text = ds.Tables[0].Rows[0]["TEAMNAME"].ToString();
                lblCollege.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                int EventId = Convert.ToInt32(ds.Tables[0].Rows[0]["EVENTID"]);

                ViewState["EVENTID"] = EventId.ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
            }

            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventApproval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
      
    }

    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
       // pnlODStatus.Visible = false;
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        //trfrmto.Visible = false;
       // trbutshow.Visible = false;
    }



  


  


    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

}