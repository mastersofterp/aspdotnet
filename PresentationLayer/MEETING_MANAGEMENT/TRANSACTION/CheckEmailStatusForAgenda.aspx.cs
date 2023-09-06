//=======================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : Meeting Management
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 09-JUN-2017
// DESCRIPTION   : USED TO CHECK RESPONSE ON SENDING EMAIL FOR AGENDA 
//========================================================================
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


public partial class MEETING_MANAGEMENT_TRANSACTION_CheckEmailStatusForAgenda : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();
    static int ptype;
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
                    ViewState["action"] = "add";
                    FillCollege();
                }
                            
            }
            else
            {
                //msgcomp.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_CheckEmailStatusForAgenda.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This function is used to check page authorisation.
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


    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
    }


    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    { 

        btnSubmit.Enabled = true;
        DataSet DS_MEETINCODE = objCommon.FillDropDown("TBL_MM_AGENDA", "distinct MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "");
        if (DS_MEETINCODE.Tables.Count > 0)
        {
            if (DS_MEETINCODE.Tables[0].Rows.Count > 0)
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.Items.Add("Please Select");
                ddlpremeeting.SelectedItem.Value = "0";
                ddlpremeeting.DataTextField = "MEETING_CODE";
                ddlpremeeting.DataValueField = "MEETING_CODE";
                ddlpremeeting.DataSource = DS_MEETINCODE.Tables[0];
                ddlpremeeting.DataBind();
                ddlpremeeting.SelectedIndex = 0;
            }
            else
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.DataSource = null;
                ddlpremeeting.DataBind();
            }
        }
    }



    // This function is used to bind the list.
    private void BindlistView()
    {
        try
        {
            
            //DataSet ds = objCommon.FillDropDown("TBL_MM_EMAIL_CONFIRMATION EC INNER JOIN TBL_MM_COMITEE C ON (EC.COMMITTEEID = C.ID)  INNER JOIN TBL_MM_MENBERDETAILS MD ON (EC.MEMBER_USERID = MD.USERID)", "ROW_NUMBER() OVER(ORDER BY EC.ECID ASC) AS Row, EC.ECID, C.NAME, EC.MEETING_CODE, MD.FNAME +' '+ (case MD.MNAME when '-' then '' else MD.MNAME end) +' '+ MD.LNAME AS MEMBER_NAME", "(CASE EC.RECEIVE_STATUS WHEN 1 THEN 'Received' ELSE 'Not Received' END) AS RECEIVE_STATUS, RECEIVED_DATE", "EC.COMMITTEEID =" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND EC.MEETING_CODE ='" + ddlpremeeting.SelectedItem.Text + "'", "EC.ECID");
            DataSet ds = objCommon.FillDropDown("TBL_MM_EMAIL_CONFIRMATION EC INNER JOIN TBL_MM_COMITEE C ON (EC.COMMITTEEID = C.ID) INNER JOIN TBL_MM_MENBERDETAILS MD ON (EC.FK_MEMBER = MD.PK_CMEMBER)", "ROW_NUMBER() OVER(ORDER BY EC.ECID ASC) AS Row, EC.ECID, C.NAME, EC.MEETING_CODE, MD.FNAME +' '+ (case MD.MNAME when '-' then '' else MD.MNAME end) +' '+ MD.LNAME AS MEMBER_NAME", "(CASE EC.RECEIVE_STATUS WHEN 1 THEN 'Received' ELSE 'Not Received' END) AS RECEIVE_STATUS, RECEIVED_DATE", "EC.COMMITTEEID =" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND EC.MEETING_CODE ='" + ddlpremeeting.SelectedItem.Text + "'", "EC.ECID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvReceivedList.DataSource = ds;
                lvReceivedList.DataBind();
                lvReceivedList.Visible = true;
            }
            else
            {
                lvReceivedList.DataSource = null;
                lvReceivedList.DataBind();
                lvReceivedList.Visible = false ;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_CheckEmailStatusForAgenda.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //objMM.DESIGNAME = Convert.ToString(txtDesigname.Text);

            //if (ViewState["action"] != null)
            //{
            //    if (txtDesigname.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage(this.updActivity, "Please Enter Data", this.Page);
            //        return;
            //    }
            //    else
            //    {
            //        if (ViewState["action"].ToString().Equals("add"))
            //        {
            //            DataSet ds = objCommon.FillDropDown("tbl_mm_CommiteeDesig", "PK_COMMITEEDES", "DESIGNATION", "STATUS = 0 AND DESIGNATION='" + objMM.DESIGNAME + "'", "");
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
            //                txtDesigname.Text = string.Empty;
            //                return;
            //            }
            //            else
            //            {
            //                objMM.PK_COMMITEEDES = 0;
            //                CustomStatus cs = (CustomStatus)objMC.AddUpdate_Designation(objMM);
            //                if (cs.Equals(CustomStatus.RecordSaved))
            //                {
            //                    BindlistView();
            //                    ViewState["action"] = "add";
            //                    objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
            //                    Clear();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            objMM.PK_COMMITEEDES = Convert.ToInt32(ViewState["DesigNo"]);
            //            // DataSet ds = objCommon.FillDropDown(" TBL_MM_COMITEE", "*", "", "NAME ='" + objMM.NAME + "' AND CODE='" + objMM.CODE + "' ", "");
            //            DataSet ds = objCommon.FillDropDown("tbl_mm_CommiteeDesig", "PK_COMMITEEDES", "DESIGNATION", "STATUS = 0 AND DESIGNATION ='" + objMM.DESIGNAME + "' AND PK_COMMITEEDES !=" + Convert.ToInt32(ViewState["DesigNo"]), "");
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
            //                txtDesigname.Text = string.Empty;
            //                return;
            //            }
            //            else
            //            {
            //                CustomStatus cs = (CustomStatus)objMC.AddUpdate_Designation(objMM);
            //                if (cs.Equals(CustomStatus.RecordSaved))
            //                {
            //                    BindlistView();
            //                    ViewState["action"] = "add";
            //                    objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
            //                    Clear();
            //                }
            //            }
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_CheckEmailStatusForAgenda.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    // This function is used to clear the controls.
    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlCommitee.SelectedIndex = 0;
        ddlpremeeting.Items.Clear();
        ddlpremeeting.Items.Add("Please Select");
        ddlpremeeting.SelectedItem.Value = "0";
        lvReceivedList.DataSource = null;
        lvReceivedList.DataBind();
        lvReceivedList.Visible = false;
    }

    protected void ddlpremeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindlistView();
    }

    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (rdbCommitteeType.SelectedValue == "U")
            //{
            //    trCollegeName.Visible = false;
            //    ddlCommitee.SelectedIndex = 0;
            //    ddlpremeeting.SelectedIndex = 0;
            //    ddlCollege.SelectedIndex = 0;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND COLLEGE_NO = 0", "NAME");
            //}
            //else
            //{
            //    trCollegeName.Visible = true;
            //    ddlCommitee.SelectedIndex = 0;
            //    ddlpremeeting.SelectedIndex = 0;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_CheckEmailStatusForAgenda.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}