using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.EnterpriseServices;
using System.Collections;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_POSTADMISSION_ADMPUnlockStudentDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPUnlockStudentDetails objSD = new ADMPUnlockStudentDetails();
    ADMPUnlockStudentDetailsController objUC = new ADMPUnlockStudentDetailsController();

    string degreenos = string.Empty;
    string branchnos = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["UserNo"] = null;
            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //if (Convert.ToInt32(Session["OrgId"].ToString()) == 15)
                //{
                //    pnlPortal.Visible = true;
                //    pnlErp.Visible = false;
                //    BindDropDownList();
                //}
                //else
                //{
                    pnlErp.Visible = true;
                    pnlPortal.Visible = false;
                    BindErpDropDownList();
                    CheckPageAuthorization();
                //}
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ADMPUnlockStudentDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ADMPUnlockStudentDetails.aspx");
        }
    }

    #region Portal
    private void BindDropDownList()
    {
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS=1", "BATCHNO DESC");
    }

    public class TabName
    {
        int tabid = 0;
        string tabtextname = string.Empty;

        public TabName(int tabid, string tabtextname)
        {
            this.tabid = tabid;
            this.tabtextname = tabtextname;
        }
        public int TabId
        {
            get { return tabid; }
            set { tabid = value; }
        }

        public string TabTextName
        {
            get { return tabtextname; }
            set { tabtextname = value; }
        }
    }

    private void BindTabUG()
    {
        List<TabName> list = new List<TabName>();
        list.Add(new TabName(1, "Personal Details"));
        list.Add(new TabName(2, "Address Details"));
        list.Add(new TabName(3, "Photo Signature Details"));
        list.Add(new TabName(4, "Qualification Details - X-Class"));
        list.Add(new TabName(5, "Qualification Details - Plus Two Level"));
        list.Add(new TabName(9, "Work Experience"));
        list.Add(new TabName(10, "Upload Documents"));
        list.Add(new TabName(11, "Reservation Details"));

        lstbxAllowProcess.Items.Clear();
        lstbxAllowProcess.DataSource = list;
        lstbxAllowProcess.DataTextField = "TabTextName";
        lstbxAllowProcess.DataValueField = "TabId";
        lstbxAllowProcess.DataBind();
    }

    private void BindTabPG()
    {
        List<TabName> list = new List<TabName>();
        list.Add(new TabName(1, "Personal Details"));
        list.Add(new TabName(2, "Address Details"));
        list.Add(new TabName(3, "Photo Signature Details"));
        list.Add(new TabName(4, "Qualification Details - X-Class"));
        list.Add(new TabName(5, "Qualification Details - Plus Two Level"));
        list.Add(new TabName(6, "Qualification Details - Graduation"));
        list.Add(new TabName(7, "Qualification Details - Post Graduation"));
        list.Add(new TabName(8, "Qualification Details - Other Qualification"));
        list.Add(new TabName(9, "Work Experience"));
        list.Add(new TabName(10, "Upload Documents"));
        list.Add(new TabName(11, "Reservation Details"));

        lstbxAllowProcess.Items.Clear();
        lstbxAllowProcess.DataSource = list;
        lstbxAllowProcess.DataTextField = "TabTextName";
        lstbxAllowProcess.DataValueField = "TabId";
        lstbxAllowProcess.DataBind();
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlApplicantType.SelectedIndex = 0;
            ddlDegreeProgram.Items.Clear();
            ddlDegreeProgram.Items.Insert(0, new ListItem("Please Select", "0"));
            lstbxAllowProcess.ClearSelection();
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            divAllowProcess.Visible = false;
            lvStudnetDetails.DataSource = null;
            lvStudnetDetails.DataBind();
            divStudnetDetails.Visible = false;
            btnsubmit.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.ddlAdmBatch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlApplicantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegreeProgram.Items.Clear();
            ddlDegreeProgram.Items.Insert(0, new ListItem("Please Select", "0"));
            lstbxAllowProcess.ClearSelection();
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            divAllowProcess.Visible = false;
            lvStudnetDetails.DataSource = null;
            lvStudnetDetails.DataBind();
            divStudnetDetails.Visible = false;
            btnsubmit.Visible = false;
            //if (ddlApplicantType.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlDegreeProgram, "VW_ACD_COLLEGE_DEGREE_BRANCH", "(Convert(varchar,DEGREENO) + ',' + Convert(varchar,BRANCHNO))PROGRAM_ID", "(DEGREE_CODE +'-'+ LONGNAME)PROGRAM", "DEGREETYPEID=" + Convert.ToInt32(ddlApplicantType.SelectedValue) + "", "");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.ddlApplicantType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rboApplicationNo.Checked == true)
    //        {
    //            objSD.ApplicationId = (txtSearch.Text);
    //            objSD.FirstName = "";
    //        }
    //        if (rboName.Checked == true)
    //        {
    //            objSD.ApplicationId = "";
    //            objSD.FirstName = (txtSearch.Text);
    //        }
    //        DataSet ds = objUC.SearchUnlockStudentDetails(objSD);
    //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //          //  lblStudnetName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ADMPUnlockStudentDetails.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    private void SelectedDegreeProgram()
    {

        string[] DegreeBranch = ddlDegreeProgram.SelectedValue.Split(',');
        if (ddlApplicantType.SelectedIndex > 0)
        {
            if (!string.IsNullOrEmpty(degreenos))
                degreenos = DegreeBranch[0];
            else
                degreenos = DegreeBranch[0];

            if (!string.IsNullOrEmpty(branchnos))
                branchnos = DegreeBranch[1];
            else
                branchnos = DegreeBranch[1];
        }

        degreenos = degreenos.Trim();
        branchnos = branchnos.Trim();
    }

    private void BindStudnetDetails()
    {
        try
        {
            objSD.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objSD.UGPGOT = Convert.ToInt32(ddlApplicantType.SelectedValue);
            //    SelectedDegreeProgram();
            objSD.CollegeId = 0;
            objSD.DegreeNo = 0;//Convert.ToInt32(degreenos.ToString());
            objSD.BranchNo = 0;// Convert.ToInt32(branchnos.ToString());
            DataSet dsDetails = objUC.GetUnlockStudentDetails(objSD);
            if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
            {
                lvStudnetDetails.DataSource = dsDetails.Tables[0];
                lvStudnetDetails.DataBind();
                divStudnetDetails.Visible = true;
                divAllowProcess.Visible = true;
                btnsubmit.Visible = true;
            }
            else
            {
                lvStudnetDetails.DataSource = null;
                lvStudnetDetails.DataBind();
                divStudnetDetails.Visible = false;
                divAllowProcess.Visible = false;
                btnsubmit.Visible = false;
                objCommon.DisplayMessage(this.updunlock, "Record not found", Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.BindStudnetDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
                clear();
                if (ddlApplicantType.SelectedValue == "1")
                {
                    BindTabUG();
                }
                else if (ddlApplicantType.SelectedValue == "2")
                {
                    BindTabPG();
                }
                BindStudnetDetails();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string userno = string.Empty;
            foreach (ListViewDataItem lvDetails in lvStudnetDetails.Items)
            {
                CheckBox chkstud = lvDetails.FindControl("chkStud") as CheckBox;
                HiddenField hdUserno = lvDetails.FindControl("hdnUserno") as HiddenField;
                if (chkstud.Checked == true)
                {
                    count++;
                    userno += hdUserno.Value.ToString() + ",";
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updunlock, "Please select at least one student", Page);
                return;
            }

            DateTime CurrentDateTime = Convert.ToDateTime(DateTime.Now);
            string CurrentTime = CurrentDateTime.ToString("H:mm");

            //  txtStartEndDateTime.Text.ToString();
            string[] SplitDate = txtStartEndDateTime.Text.Split(' ');
            string StartDate = SplitDate[0];
            string STime = SplitDate[1];
            string SAMPM = SplitDate[2];

            string EndDate = SplitDate[4];
            string ETime = SplitDate[5];
            string EAMPM = SplitDate[6];

            string StartTime = STime + ' ' + SAMPM;
            string EndTime = ETime + ' ' + EAMPM;

            DateTime SDate = Convert.ToDateTime(StartDate + ' ' + StartTime);
            DateTime EDate = Convert.ToDateTime(EndDate + ' ' + EndTime);
            //DateTime sTime = Convert.ToDateTime(StartTime);
            //DateTime eTime = Convert.ToDateTime(EndTime);
            string t1 = SDate.ToString("H:mm");
            string t2 = EDate.ToString("H:mm");

            if (Convert.ToDateTime(SDate.ToString("yyyy-MM-dd")) < Convert.ToDateTime(CurrentDateTime.ToString("yyyy-MM-dd")))
            {
                objCommon.DisplayMessage(this.updunlock, "Start Date should not be less than Current Date", Page);
                return;
            }
            if (Convert.ToDateTime(SDate.ToString("yyyy-MM-dd")) == Convert.ToDateTime(CurrentDateTime.ToString("yyyy-MM-dd")))
            {
                if (Convert.ToDateTime(t1) < Convert.ToDateTime(CurrentTime))
                {
                    objCommon.DisplayMessage(this.updunlock, "Start Time should not be less than Current Time", Page);
                    return;
                }
                if (Convert.ToDateTime(t1) == Convert.ToDateTime(CurrentTime))
                {
                    objCommon.DisplayMessage(this.updunlock, "Start Time should not be equal to Current Time", Page);
                    return;
                }
            }
            if (Convert.ToDateTime(SDate.ToString("yyyy-MM-dd")) == Convert.ToDateTime(EDate.ToString("yyyy-MM-dd")))
            {
                if (Convert.ToDateTime(t2) < Convert.ToDateTime(CurrentTime))
                {
                    objCommon.DisplayMessage(this.updunlock, "End Time should not be less than Current Time", Page);
                    return;
                }
                if (Convert.ToDateTime(t2) < Convert.ToDateTime(t1))
                {
                    objCommon.DisplayMessage(this.updunlock, "End Time should not be less than Start Time", Page);
                    return;
                }
                if (Convert.ToDateTime(t1) == Convert.ToDateTime(t2))
                {
                    objCommon.DisplayMessage(this.updunlock, "Start Time should not be equal to End Time", Page);
                    return;
                }
            }



            objSD.Userno = userno.ToString();
            objSD.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            //  SelectedDegreeProgram();
            objSD.DegreeNo = 0;// Convert.ToInt32(degreenos.ToString());
            objSD.BranchNo = 0;// Convert.ToInt32(branchnos.ToString());
            string allowprocess = string.Empty;
            foreach (ListItem item in lstbxAllowProcess.Items)
            {
                if (item.Selected == true)
                {
                    allowprocess += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(allowprocess))
            {
                allowprocess = allowprocess.Substring(0, allowprocess.Length - 1);
            }
            else
            {
                allowprocess = "0";
            }
            objSD.AllowProcess = allowprocess.ToString();
            objSD.StartDate = Convert.ToDateTime(StartDate);//Convert.ToDateTime(txtStartDate.Text);
            objSD.EndDate = Convert.ToDateTime(EndDate);//Convert.ToDateTime(txtEndDate.Text);
            objSD.UaNo = Convert.ToInt32(Session["userno"].ToString());
            objSD.StartTime = StartTime.ToString();//txtStartTime.Text; 
            objSD.EndTime = EndTime.ToString();// txtEndTime.Text; 
            objSD.AllowProcessFrom = 'P';
            int retstatus = objUC.InsUpdUnlockStudentDetails(objSD);
            if (retstatus == 1)
            {
                objCommon.DisplayMessage(this.updunlock, "Record saved successfully", Page);
                ClearAfterSubmit();
                BindStudnetDetails();
                return;
            }
            else if (retstatus == 2)
            {
                objCommon.DisplayMessage(this.updunlock, "Record updated successfully", Page);
                ClearAfterSubmit();
                BindStudnetDetails();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updunlock, "Error", Page);

                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.btnsubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearAfterSubmit()
    {
        lstbxAllowProcess.ClearSelection();
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtStartEndDateTime.Text = string.Empty;
    }

    private void cancel()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlApplicantType.SelectedIndex = 0;
        ddlDegreeProgram.Items.Clear();
        ddlDegreeProgram.Items.Insert(0, new ListItem("Please Select", "0"));
        lvStudnetDetails.DataSource = null;
        lvStudnetDetails.DataBind();
        divStudnetDetails.Visible = false;
        lstbxAllowProcess.ClearSelection();
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        divAllowProcess.Visible = false;
        btnsubmit.Visible = false;
        txtStartTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtStartEndDateTime.Text = string.Empty;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        cancel();
    }

    private void clear()
    {
        lstbxAllowProcess.ClearSelection();
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        divAllowProcess.Visible = false;
        lvStudnetDetails.DataSource = null;
        lvStudnetDetails.DataBind();
    }
    #endregion

    #region ERP
    private void BindErpDropDownList()
    {
        objCommon.FillDropDownList(ddlErpAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS=1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlErpInstituteName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        BindErpAllowProcess();
    }

    private void BindErpAllowProcess()
    {
        List<TabName> list = new List<TabName>();
        list.Add(new TabName(1, "Personal Details"));
        list.Add(new TabName(2, "Address Details"));
        list.Add(new TabName(3, "Document Upload"));
        list.Add(new TabName(4, "Qualification Details"));
        list.Add(new TabName(5, "Covid Information"));
        list.Add(new TabName(6, "Other Information"));
        lstbxErpAllowProcess.Items.Clear();
        lstbxErpAllowProcess.DataSource = list;
        lstbxErpAllowProcess.DataTextField = "TabTextName";
        lstbxErpAllowProcess.DataValueField = "TabId";
        lstbxErpAllowProcess.DataBind();
    }

    protected void btnErpShow_Click(object sender, EventArgs e)
    {
        try
        {
            Erpclear();
            BindErpStudnetDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.btnErpShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindErpStudnetDetails()
    {
        try
        {
            objSD.BatchNo = Convert.ToInt32(ddlErpAdmBatch.SelectedValue);
            objSD.UGPGOT = 0;
            objSD.CollegeId = Convert.ToInt32(ddlErpInstituteName.SelectedValue);
            objSD.DegreeNo = Convert.ToInt32(ddlErpDegree.SelectedValue);
            objSD.BranchNo = Convert.ToInt32(ddlErpBranch.SelectedValue);

            DataSet dsDetails = objUC.GetUnlockStudentDetails(objSD);
            if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
            {
                lvErpUnlockDetails.DataSource = dsDetails.Tables[0];
                lvErpUnlockDetails.DataBind();
                divErpUnlockDetails.Visible = true;
                divErpAllowProcess.Visible = true;
                btnErpSubmit.Visible = true;
            }
            else
            {
                lvErpUnlockDetails.DataSource = null;
                lvErpUnlockDetails.DataBind();
                divErpUnlockDetails.Visible = false;
                divErpAllowProcess.Visible = false;
                btnErpSubmit.Visible = false;
                objCommon.DisplayMessage(this.updunlock, "Record not found", Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.BindErpStudnetDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnErpSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string idno = string.Empty;
            foreach (ListViewDataItem lvDetails in lvErpUnlockDetails.Items)
            {
                CheckBox chkstud = lvDetails.FindControl("chkStud") as CheckBox;
                HiddenField hdIdno = lvDetails.FindControl("hdnIdno") as HiddenField;
                if (chkstud.Checked == true)
                {
                    count++;
                    idno += hdIdno.Value.ToString() + ",";
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updunlock, "Please select at least one student", Page);
                return;
            }

            DateTime CurrentDateTime = Convert.ToDateTime(DateTime.Now);
            string CurrentTime = CurrentDateTime.ToString("H:mm");

            //  txtStartEndDateTime.Text.ToString();
            string[] SplitDate = txtErpSEDateTime.Text.Split(' ');
            string StartDate = SplitDate[0];
            string STime = SplitDate[1];
            string SAMPM = SplitDate[2];

            string EndDate = SplitDate[4];
            string ETime = SplitDate[5];
            string EAMPM = SplitDate[6];

            string StartTime = STime + ' ' + SAMPM;
            string EndTime = ETime + ' ' + EAMPM;

            DateTime SDate = Convert.ToDateTime(StartDate + ' ' + StartTime);
            DateTime EDate = Convert.ToDateTime(EndDate + ' ' + EndTime);
 
            string t1 = SDate.ToString("H:mm");
            string t2 = EDate.ToString("H:mm");

            if (Convert.ToDateTime(SDate.ToString("yyyy-MM-dd")) < Convert.ToDateTime(CurrentDateTime.ToString("yyyy-MM-dd")))
            {
                objCommon.DisplayMessage(this.updunlock, "Start Date should not be less than Current Date", Page);
                return;
            }
            if (Convert.ToDateTime(SDate.ToString("yyyy-MM-dd")) == Convert.ToDateTime(CurrentDateTime.ToString("yyyy-MM-dd")))
            {
                if (Convert.ToDateTime(t1) < Convert.ToDateTime(CurrentTime))
                {
                    objCommon.DisplayMessage(this.updunlock, "Start Time should not be less than Current Time", Page);
                    return;
                }
                if (Convert.ToDateTime(t1) == Convert.ToDateTime(CurrentTime))
                {
                    objCommon.DisplayMessage(this.updunlock, "Start Time should not be equal to Current Time", Page);
                    return;
                }
            }
            if (Convert.ToDateTime(SDate.ToString("yyyy-MM-dd")) == Convert.ToDateTime(EDate.ToString("yyyy-MM-dd")))
            {
                if (Convert.ToDateTime(t2) < Convert.ToDateTime(CurrentTime))
                {
                    objCommon.DisplayMessage(this.updunlock, "End Time should not be less than Current Time", Page);
                    return;
                }
                if (Convert.ToDateTime(t2) < Convert.ToDateTime(t1))
                {
                    objCommon.DisplayMessage(this.updunlock, "End Time should not be less than Start Time", Page);
                    return;
                }
                if (Convert.ToDateTime(t1) == Convert.ToDateTime(t2))
                {
                    objCommon.DisplayMessage(this.updunlock, "Start Time should not be equal to End Time", Page);
                    return;
                }
            }



            objSD.Userno = idno.ToString();
            objSD.BatchNo = Convert.ToInt32(ddlErpAdmBatch.SelectedValue);
            objSD.DegreeNo = 0;
            objSD.BranchNo = 0;
            string allowprocess = string.Empty;
            foreach (ListItem item in lstbxErpAllowProcess.Items)
            {
                if (item.Selected == true)
                {
                    allowprocess += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(allowprocess))
            {
                allowprocess = allowprocess.Substring(0, allowprocess.Length - 1);
            }
            else
            {
                allowprocess = "0";
            }
            objSD.AllowProcess = allowprocess.ToString();
            objSD.StartDate = Convert.ToDateTime(StartDate);
            objSD.EndDate = Convert.ToDateTime(EndDate);
            objSD.UaNo = Convert.ToInt32(Session["userno"].ToString());
            objSD.StartTime = StartTime.ToString();
            objSD.EndTime = EndTime.ToString();
            objSD.AllowProcessFrom = 'E';
            int retstatus = objUC.InsUpdUnlockStudentDetails(objSD);
            if (retstatus == 1)
            {
                objCommon.DisplayMessage(this.updunlock, "Record saved successfully", Page);
                ClearErpAfterSubmit();
                BindErpStudnetDetails();
                return;
            }
            else if (retstatus == 2)
            {
                objCommon.DisplayMessage(this.updunlock, "Record updated successfully", Page);
                ClearErpAfterSubmit();
                BindErpStudnetDetails();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updunlock, "Error", Page);

                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.btnsubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearErpAfterSubmit()
    {
        lstbxErpAllowProcess.ClearSelection();
        txtErpSEDateTime.Text = string.Empty;
    }

    private void cancelErp()
    {
        ddlErpAdmBatch.SelectedIndex = 0;
        ddlErpInstituteName.SelectedIndex = 0;
        ddlErpDegree.Items.Clear();
        ddlErpDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlErpBranch.Items.Clear();
        ddlErpBranch.Items.Insert(0, new ListItem("Please Select", "0"));
        lvErpUnlockDetails.DataSource = null;
        lvErpUnlockDetails.DataBind();
        divErpUnlockDetails.Visible = false;
        divErpAllowProcess.Visible = false;
        lstbxErpAllowProcess.ClearSelection();
        btnErpSubmit.Visible = false;
        txtErpSEDateTime.Text = string.Empty;

    }

    private void Erpclear()
    {
        lstbxErpAllowProcess.ClearSelection();
        txtErpSEDateTime.Text = string.Empty;
    }  

    protected void btnErpClear_Click(object sender, EventArgs e)
    {
        cancelErp();
    }  

    protected void ddlErpInstituteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlErpInstituteName.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlErpDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlErpInstituteName.SelectedValue + "", "DEGREENO");
            }
            else
            {
                ddlErpDegree.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.ddlErpInstituteName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    
    }

    protected void ddlErpDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlErpDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlErpBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlErpInstituteName.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlErpDegree.SelectedValue), "A.LONGNAME");
            }
            else
            {
                ddlErpBranch.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPUnlockStudentDetails.ddlErpDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    #endregion
}