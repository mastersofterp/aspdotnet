//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : SECTION ALLOTMENT                                                     
// CREATION DATE : 27-Sept-2010                                                          
// CREATED BY    : NIRAJ D. PHALKE                                 
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



public partial class Academic_AttFineEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
            }
            //Populate the Drop Down Lists
            PopulateDropDown();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "");
            objCommon.FillDropDownList(ddlAttPer, "ATT_WIEGHTAGE", "DISTINCT ATT_WT_NO", "ATTENDANCE", "ATT_WT_NO > 0", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SessionCreate.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlSession.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SCHEMENO = S.SCHEMENO)", "DISTINCT S.SCHEMENO", "SCHEMENAME", "S.SCHEMENO >0 AND CT.SESSIONNO=" + ddlSession.SelectedValue, "S.SCHEMENAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SCHEMENO = S.SCHEMENO)", "DISTINCT S.SCHEMENO", "SCHEMENAME", "S.SCHEMENO >0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND CT.SESSIONNO=" + ddlSession.SelectedValue, "S.SCHEMENAME");
                }
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.SelectedIndex = 0;

            }
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            pnlStud.Visible = false;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlAttPer.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlSem, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER S ON (CT.SEMESTERNO = S.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "S.SEMESTERNAME", "CT.SCHEMENO = " + ddlScheme.SelectedValue + "AND CT.SESSIONNO=" + ddlSession.SelectedValue, "CT.SEMESTERNO");//AND SR.PREV_STATUS = 0
                }
                else
                {
                    objCommon.FillDropDownList(ddlSem, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER S ON (CT.SEMESTERNO = S.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "S.SEMESTERNAME", "CT.SCHEMENO = " + ddlScheme.SelectedValue + "AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND CT.SESSIONNO=" + ddlSession.SelectedValue, "CT.SEMESTERNO");//AND SR.PREV_STATUS = 0
                }
            }
            else
            {
                ddlSem.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            pnlStud.Visible = false;
            ddlSem.SelectedIndex = 0;
            ddlAttPer.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    private void BindListView()
    {
        try
        {
            string perCriteria = "";
            if (ddlAttPer.SelectedIndex > 0)
            {
                perCriteria = (ddlAttPer.SelectedItem.Text).ToString();
            }
            else
            {
                perCriteria = "";
            }
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetStudentsForFineEntry(Convert.ToInt32(ddlSession.SelectedValue),
                Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue),
                (perCriteria).ToString());

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudList.DataSource = ds;
                lvStudList.DataBind();
                pnlStud.Visible = true;
            }
            else
            {
                lvStudList.DataSource = null;
                lvStudList.DataBind();
                pnlStud.Visible = false;
                objCommon.DisplayMessage(this.updSession, "No Data Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //ImageButton ibutton = (ImageButton)((ListViewItem)cb.Parent).FindControl("imgStartDate");
        TextBox txtFine = (TextBox)((ListViewItem)cb.Parent).FindControl("txtFine");

        if (cb.Checked)
        {
            txtFine.Enabled = true;
            txtFine.Focus();
        }
        else
        {
            txtFine.Text = "";
            txtFine.Enabled = false;
        }
    }
    private string GetStudentIDs1()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudList.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked && (item.FindControl("chkStudent") as CheckBox).Enabled == true)
                {
                    studentIds += (item.FindControl("chkStudent") as CheckBox).ToolTip + ",";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }
    public void GetSelectNo(out string msg, out string FineAmtIdno)
    {
        FineAmtIdno = string.Empty;
        msg = string.Empty;
        
        try
        {
            foreach (ListViewDataItem item in lvStudList.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked && (item.FindControl("chkStudent") as CheckBox).Enabled == true)
                {
                    if (((item.FindControl("txtFine") as TextBox).Text.Trim() == string.Empty))
                    {
                        CheckBox chkBox = item.FindControl("chkStudent") as CheckBox;
                        FineAmtIdno += chkBox.ToolTip + ",";
                        msg = "Please Enter Fine Amount.";
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {

            demandCriteria.SessionNo = Convert.ToInt16(ddlSession.SelectedValue);
            demandCriteria.ReceiptTypeCode = "OF";
            //demandCriteria.BranchNo = (ddlBranch.SelectedIndex > 0 ? Int32.Parse(ddlBranch.SelectedValue) : 0);
            //demandCriteria.SemesterNo = (ddlSem.SelectedIndex > 0 ? Int32.Parse(ddlSem.SelectedValue) : 0);
            demandCriteria.PaymentTypeNo = 1;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CreateDemand.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DemandModificationController dmController = new DemandModificationController();
        FeeDemand demandCriteria = this.GetDemandCriteria();
        int idd = 0;
        string mssg = string.Empty;

        string FineAmtIds = string.Empty;
        string FineAmt = string.Empty;
        string message = string.Empty;
        int sem = 0;
        try
        {

            string ids = GetStudentIDs1();
            if (ids == string.Empty)
            {
                objCommon.DisplayUserMessage(updSession, "Please Select Student ", this.Page);
                return;
            }
            else
            {

                GetSelectNo(out mssg, out FineAmtIds);
                message = mssg;

                if (FineAmtIds != string.Empty)
                {
                    objCommon.DisplayUserMessage(updSession, message, this.Page);
                    return;
                }
                else
                {
                    foreach (ListViewDataItem item in lvStudList.Items)
                    {
                        if ((item.FindControl("chkStudent") as CheckBox).Checked && (item.FindControl("chkStudent") as CheckBox).Enabled == true)
                        {
                            idd = Convert.ToInt32((item.FindControl("chkStudent") as CheckBox).ToolTip);
                            FineAmt = (item.FindControl("txtFine") as TextBox).Text;
                            sem = Convert.ToInt32((item.FindControl("lblStudname") as Label).ToolTip);
                            string response = dmController.CreateDemandForAttendnaceFine(idd, demandCriteria, FineAmt, sem);
                            if (response == "1")
                            {
                            }
                            else
                            {
                                objCommon.DisplayUserMessage(updSession, "Error !", this.Page);
                            }
                        }
                    }
                    objCommon.DisplayUserMessage(updSession, "Demand Created Successfully for Attendance Fine !", this.Page);
                    //BindListView();
                }
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlAttPer_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        pnlStud.Visible = false;
    }
    protected void cbHead_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        foreach (ListViewDataItem item in lvStudList.Items)
        {
            TextBox txtFine = item.FindControl("txtFine") as TextBox;
            if (cb.Checked)
            {
                txtFine.Enabled = true;
            }
            else
            {
                txtFine.Text = "";
            }
        }

    }
}
