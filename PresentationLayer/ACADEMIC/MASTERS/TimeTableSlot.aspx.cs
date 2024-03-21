//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : SLOT MASTER FORM                                                     
// CREATION DATE : 02-APRIL-2010                                                        
// CREATED BY    : NIRAJ D. PHALKE                                                      
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

public partial class ACADEMIC_MASTERS_TimeTableSlot : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();

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
                    // this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form mode equals to -1(New Mode).
                    ViewState["slotno"] = "0";



                    ViewState["action"] = "add";
                }
            }

            this.LoadSlot();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // Added By Gaurav S 12_01_2022
            DateTime TimeFrom = Convert.ToDateTime(txtTimeFrom.Text);
            DateTime TimeTo = Convert.ToDateTime(txtTimeTo.Text);
            int courseslot = (hfdCourseSlot.Value.ToString()).ToLower() == "true" ? 1 : 0;
            if (TimeFrom > TimeTo)
            {
                if (courseslot == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCourseSlot(true);", true);
                }
                objCommon.DisplayMessage("Time Must Be Greater Than To Time", this.Page);
                return;
            }

            //string chkexist = objCommon.LookUp("ACD_EXAM_TT_SLOT", "count(1)", "SLOTNAME='" + txtSlotName.Text.ToString().Trim() + "' AND TIMEFROM ='" + txtTimeFrom.Text.ToString().Trim() + "'AND TIMETO ='" + txtTimeTo.Text.ToString().Trim() + "'");
            //if ((chkexist != null || chkexist != string.Empty) && chkexist != "0")
            //{
            //    if (courseslot == 1)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCourseSlot(true);", true);
            //    }
            //    objCommon.DisplayMessage("Slot Name and Times already exist", this.Page);
            //    return;
            //}

            if (ViewState["slotno"].ToString() != string.Empty && ViewState["slotno"].ToString() == "0")
            {
                string chkexist = objCommon.LookUp("ACD_EXAM_TT_SLOT", "count(1)", "SLOTNAME='" + txtSlotName.Text.ToString().Trim() + "' AND TIMEFROM ='" + txtTimeFrom.Text.ToString().Trim() + "'AND TIMETO ='" + txtTimeTo.Text.ToString().Trim() + "'");
                if ((chkexist != null || chkexist != string.Empty) && chkexist != "0")
                {
                    if (courseslot == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCourseSlot(true);", true);
                    }
                    objCommon.DisplayMessage("Slot Name and Times already exist", this.Page);
                    return;
                }
                if (txtTimeFrom.Text == txtTimeTo.Text)
                {
                    if (courseslot == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCourseSlot(true);", true);
                    }
                    objCommon.DisplayMessage("From time and To time is not equal", this.Page);
                }
                else
                {
                    //CustomStatus cs = (CustomStatus)objExamController.AddExamSlot(txtSlotName.Text.Trim(), txtTimeFrom.Text.Trim(), txtTimeTo.Text.Trim(), Session["colcode"].ToString());
                    CustomStatus cs = (CustomStatus)objExamController.AddExamSlot(txtSlotName.Text.Trim(), txtTimeFrom.Text.Trim(), txtTimeTo.Text.Trim(), Session["colcode"].ToString(), courseslot);
                    if (!cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage("Slot Added Successfully!", this.Page);
                        this.LoadSlot();
                        this.Clear();
                    }

                    else
                        objCommon.DisplayMessage("Error Adding Slot!", this.Page);
                }
            }
            else
            {
                string chkexist = objCommon.LookUp("ACD_EXAM_TT_SLOT", "count(1)", "SLOTNAME='" + txtSlotName.Text.ToString().Trim() + "' AND TIMEFROM ='" + txtTimeFrom.Text.ToString().Trim() + "'AND TIMETO ='" + txtTimeTo.Text.ToString().Trim() + "' AND ISNULL(COURSE_SLOT,0)=" + courseslot);
                if ((chkexist != null || chkexist != string.Empty) && chkexist != "0")
                {
                    if (courseslot == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCourseSlot(true);", true);
                    }
                    objCommon.DisplayMessage("Record already exist", this.Page);
                    return;
                }
                if (txtTimeFrom.Text == txtTimeTo.Text)
                {
                    objCommon.DisplayMessage("From time and To time is not equal", this.Page);
                }
                else
                {
                    //CustomStatus cs = (CustomStatus)objExamController.UpdateExamSlot(Convert.ToInt32(ViewState["slotno"]), txtSlotName.Text.Trim(), txtTimeFrom.Text.Trim(), txtTimeTo.Text.Trim());
                    CustomStatus cs = (CustomStatus)objExamController.UpdateExamSlot(Convert.ToInt32(ViewState["slotno"]), txtSlotName.Text.Trim(), txtTimeFrom.Text.Trim(), txtTimeTo.Text.Trim(), courseslot);
                    if (!cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage("Slot Updated Successfully!", this.Page);
                        this.LoadSlot();
                        this.Clear();
                    }
                    else
                        objCommon.DisplayMessage("Error Updating Slot!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;

            DataSet ds = objExamController.GetSingleExamSlot(int.Parse(btnEditRecord.CommandArgument));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ViewState["slotno"] = dr["SLOTNO"].ToString();

                txtSlotName.Text = dr["SLOTNAME"].ToString();
                txtTimeFrom.Text = dr["TIMEFROM"].ToString();
                txtTimeTo.Text = dr["TIMETO"].ToString();

                if (dr["COURSE_SLOT"].ToString() == "1")
                {
                    hfdCourseSlot.Value = "true";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCourseSlot(true);", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    #region User-Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TimeTableSlot.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TimeTableSlot.aspx");
        }
    }

    private void LoadSlot()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME,TIMEFROM,TIMETO,(CASE CAST(EXAMTYPE AS NVARCHAR(5)) WHEN 1 THEN 'MID SEM TIME TABLE' WHEN 2 THEN 'END SEM TIME TABLE' ELSE '' END) EXAM_TYPE,COURSE_SLOT", "SLOTNO>0", "SLOTNO DESC");
            if (ds != null && ds.Tables.Count > 0)
            {
                lvSlot.DataSource = ds;
                lvSlot.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.LoadSlot() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void Clear()
    {
        txtSlotName.Text = string.Empty;
        txtTimeFrom.Text = string.Empty;
        txtTimeTo.Text = string.Empty;
        ViewState["slotno"] = "0";
        hfdCourseSlot.Value = "";
    }
    #endregion
}
