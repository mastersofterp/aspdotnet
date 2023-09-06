//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : TO CREATE BulkPrintRegistrationSlip.aspx PAGE                                             
// CREATION DATE : 13-May-20019
// CREATED BY    : Rita A. Munde                              
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Web;
using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_BulkPrintRegistrationSlip : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    int prev_status;

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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }

            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN COLLEGE
            objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0", "college_id");
            // objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            // FILL DROPDOWN YEAR
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            // ddlYear.SelectedValue = "1";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //Bind the Student List....
            this.BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds;
            // Get List of Student.....
            ds = studCont.GetStudentListForRegistrationSlip(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                lvStudentRecords.Visible = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
            }

            else
            {
                objCommon.DisplayMessage(updtime, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            Session["listIdCard"] = null;
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            if (ddlColg.SelectedIndex > 0)
            {
                ViewState["college_id"] = Convert.ToInt32(ddlColg.SelectedValue).ToString();
            }
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_IDNO=" + param;
            
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_PREV_STATUS=" + prev_status + ",@P_DATEOFISSUE=" + txtDateofissue.Text.ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    //Get List of Student separated by dot(.).........
    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                string studentIds = string.Empty;
                foreach (ListViewDataItem lvItem in lvStudentRecords.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkReport") as CheckBox;
                    if (chkBox.Checked == true)
                    {
                        studentIds += chkBox.ToolTip + ",";
                        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip) + ""));

                    }
                }

                // int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), studentIds, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]));

                // ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");

                ShowReport(ids, "Admission_Slip_Report", "rptStudentRegistrationSlip.rpt");

            }
            else
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
                // objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();

            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;

                DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport");
                DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller");


                objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;

                if (chk.Checked.Equals(true) && chk.Enabled.Equals(true))
                {
                    if (Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) > 0)
                    {
                        objS.Transportation = 1;
                        objS.TransportSts = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                    }

                    if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > 0)
                    {
                        objS.Hosteler = Convert.ToInt32(1);
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Hosteler = Convert.ToInt32(0);
                        objS.HostelSts = 0;
                        flag = true;
                    }
                    BindListView();
                    // objCommon.DisplayMessage(this.updtime, "Please Select No Dues Status.", this.Page);

                    if (flag.Equals(true))
                    {
                        //Add  and update transport & hosteller status
                        output = studCont.AddTransportStatusForStudent(objS);
                    }
                }
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student Status Alloted Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void rbRegEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    public void ClearForBulkStudent()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;
        Panel1.Visible = false;
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP 1(SEMESTERNO)", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {

        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();


            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;

                DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport");
                DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller");


                objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;

                if (chk.Checked.Equals(true) && chk.Enabled.Equals(true))
                {
                    objS.Lock = 1;
                    objS.UnLock = 0;
                    if (Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) > 0)
                    {
                        objS.Transportation = 1;
                        objS.TransportSts = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                    }

                    if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > 0)
                    {
                        objS.Hosteler = Convert.ToInt32(1);
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Hosteler = Convert.ToInt32(0);
                        objS.HostelSts = 0;
                        flag = true;
                    }
                    BindListView();
                    if (flag.Equals(true))
                    {
                        output = studCont.AddLockTransportStatusForStudent(objS);
                    }
                }
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student Status Lock Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();


            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;

                DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport");
                DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller");


                objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;

                if (chk.Checked.Equals(true) && chk.Enabled.Equals(false))
                {
                    objS.Lock = 0;
                    objS.UnLock = 1;
                    if (Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) > 0)
                    {
                        objS.Transportation = 1;
                        objS.TransportSts = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                    }

                    if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > 0)
                    {
                        objS.Hosteler = Convert.ToInt32(1);
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Hosteler = Convert.ToInt32(0);
                        objS.HostelSts = 0;
                        flag = true;
                    }
                    BindListView();

                    if (flag.Equals(true))
                    {
                        //This method is used for lock the transportation status......
                        output = studCont.AddLockTransportStatusForStudent(objS);
                    }
                }
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student Status Unlock Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}