using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exam_Hall_Ticket : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPExamHallTicketController objexam = new ADMPExamHallTicketController();

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
            // Check User Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["colcode"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
                ddlAdmissionBatch.SelectedIndex = 0;
                btnPrint.Enabled = false;
                btnAdmitCard.Visible = false;
                btnPrint.Visible = false;
                // this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
               // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID");
                //objCommon.FillDropDownList(ddlCode, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "", "BRANCH_CODE");

            }
        }
    }

    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedIndex > 0)
            {
               // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT="+ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            
            ddlDegree.Items.Insert(0, new ListItem("Please Select Degree","0"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();

            lstProgram.Items.Clear();
            pnlCount.Visible = false;
            ddlExamSchedule.Items.Clear();
            pnllistView.Visible = false;
            lvSchedule.DataSource = null;
            lvSchedule.DataBind();
            ddlExamSchedule.Enabled = false;
            btnAdmitCard.Visible = false;
            btnPrint.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Exam_Hall_Ticket.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdmitCard.Visible = false;
        btnPrint.Visible = false;
        lstProgram.Items.Clear();
        pnlCount.Visible = false;
        ddlExamSchedule.Items.Clear();
        pnllistView.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();

        ddlExamSchedule.Enabled = false;
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objexam.GetBranch(Degree);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }


    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            btnAdmitCard.Visible = false;
            btnPrint.Visible = false;
            pnlCount.Visible = false;  
            pnllistView.Visible = false;
            int DegreeId=Convert.ToInt32(ddlDegree.SelectedValue);
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            string branchno = string.Empty;
            
            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();
            //string Branch=ddlProgramType.SelectedValue;
            ds = objexam.GetSChedule(DegreeId, branchno,ADMBATCH);

            ddlExamSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlExamSchedule.DataSource = ds;
                ddlExamSchedule.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlExamSchedule.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlExamSchedule.DataBind();
                ddlExamSchedule.Items.Insert(0, new ListItem("Select Schedule", "0"));
                ddlExamSchedule.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            
             if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Branch/Program.", this.Page);
                return;
            }

             btnAdmitCard.Visible = true;
             btnPrint.Visible = true;

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);

            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = objexam.GetAdmitCardStudent(ADMBATCH, ProgramType, DegreeNo, branchno);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllistView.Visible = true;
                ddlExamSchedule.Enabled = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                //lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                //lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lvSchedule.DataBind();
                pnlCount.Visible = true;
                txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString("0000");
                txtGeneratedAdmitCard.Text = ds.Tables[0].AsEnumerable().Where(c => c["STATUS"].ToString() == "Generated").ToList().Count.ToString();
                txtNotGeneratedAdmitCard.Text = ds.Tables[0].AsEnumerable().Where(c => c["STATUS"].ToString() == "Not Generated").ToList().Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(upExamTimeTable, "No Record Found.", this.Page);
                btnAdmitCard.Visible = false;
                btnPrint.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnAdmitCard_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Branch/Program.", this.Page);
                return;
            }

            if ( Convert.ToInt32(ddlExamSchedule.SelectedValue) == 0)
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Exam Schedule.", this.Page);
                return;
            }
            string ipaddress = string.Empty;
           
            string rollno = string.Empty;
            int chkCount = 0;
            int updCount = 0;
            
            ipaddress = Request.ServerVariables["REMOTE_HOST"];
          
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int userNo = 0;
            string rollNo = string.Empty;
            bool IsReschedule = false;

            foreach (ListViewDataItem lvItem in lvSchedule.Items)
            {

                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;                
                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    userNo = Convert.ToInt32(hdnUserNo.Value);
                }
              
                if (chkBox.Checked && chkBox.Enabled == true)
                {
                    //rollno = hdfRollno.Value;
                    chkCount++;
                    CustomStatus cs = (CustomStatus)objexam.UpdateStatusFor_AdmitCard(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), userNo, ipaddress, ua_no, ddlExamSchedule.SelectedItem.Text, Convert.ToInt32(ddlExamSchedule.SelectedValue), IsReschedule);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        updCount++;
                    }
                }
            }
            if (chkCount == 0)
            {
                //objCommon.DisplayMessage(this.Page, "Please Select At Least One Student.", this.Page);
                objCommon.DisplayMessage(upExamTimeTable, "Please Select At Least One Student.", this.Page);
                btnShow_Click(sender,e);
                return;
            }
            if (chkCount > 0 && chkCount == updCount)
            {
                //objCommon.DisplayMessage("Admit Card Generated Successfully for Selected Students.", this.Page);
                objCommon.DisplayMessage(upExamTimeTable, "Admit Card Generated Successfully for Selected Students.", this.Page);
                btnShow_Click(sender, e);
                //this.BindData();
            }
         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upExamTimeTable, "Please Select Branch/Program.", this.Page);
                return;
            }

            if (Convert.ToInt32(ddlExamSchedule.SelectedValue) == 0)
            {

                objCommon.DisplayMessage(upExamTimeTable, "Please Select Exam Schedule.", this.Page);
                closeWindow("Student Admit Card Report", "rptAdmitCard.rpt");
                return;


            }

            else
            {
                string studentIds = string.Empty;
                string ipaddress = string.Empty;

                string branchno = string.Empty;

                foreach (ListItem items in lstProgram.Items)
                {
                    if (items.Selected == true)
                    {
                        branchno += items.Value + '$';
                        //activitynames += items.Text + ',';
                    }
                }

                //branchno.TrimEnd(',').TrimEnd();
                branchno = branchno.TrimEnd('$').Trim();

                ShowReportNew("Student Admit Card Report", "rptAdmitCard.rpt", branchno);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.btnPrint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void closeWindow(string reportTitle, string rptFileName)
    {

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_EXAMSCHEDULE=" +ddlExamSchedule.SelectedItem.Text + ",@P_BRANCHNO=" + branchno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_EXAMSCHEDULE=" + ddlExamSchedule.SelectedValue + ",@P_BRANCHNO=" + branchno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + branchno;
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.close('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }

    private void ShowReportNew(string reportTitle, string rptFileName,string branchno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_EXAMSCHEDULE=" +ddlExamSchedule.SelectedItem.Text + ",@P_BRANCHNO=" + branchno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_EXAMSCHEDULE=" + ddlExamSchedule.SelectedValue + ",@P_BRANCHNO=" + branchno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + branchno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlExamSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPrint.Enabled = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnAdmitCard.Visible = false;
        btnPrint.Visible = false;
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH DESC");        
        clear();

    }

    protected void clear()
    {
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        lstProgram.Items.Clear();
        pnlCount.Visible = false;
        ddlExamSchedule.Items.Clear();
        pnllistView.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
    }
}