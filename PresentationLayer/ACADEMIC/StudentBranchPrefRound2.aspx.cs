using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_StudentBranchPrefRound2 : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();

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
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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

                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                objCommon.FillDropDownList(ddlRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO>0", "ADMROUNDNO");
                ddlAdmBatch.Items.RemoveAt(0);
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO IN(1,3,4)", "DEGREENO DESC");

                btnRCTRound.Enabled = false;
                btnAllot.Enabled = false;
            }
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentBranchPrefRound2.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=StudentBranchPrefRound2.aspx");
        }
    }

    private void BindSeatcount()
    {
        DataSet dsseatcount = objSC.getseatcount(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue), Convert.ToInt32(rdCatAllot.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));

        if (dsseatcount.Tables[0].Rows != null)
        {
            lvSeat.Visible = true;
            lstviewForSVDET.Visible = false;
            lvSeat.DataSource = dsseatcount.Tables[0];
            lvSeat.DataBind();

            lvStudents.Visible = false;
            pnlStudents.Visible = false;
        }
        else
        {
            objCommon.DisplayMessage("No Data Found", this.Page);
            lvSeat.Visible = false;
            lvSeat.DataSource = null;
            lvSeat.DataBind();
        }
    }
    

    private void ListOperations(ListView list, DataTable dt)
    {
        foreach (ListViewDataItem item in list.Items)
        {
            CheckBox cbHead = list.FindControl("cbHead") as CheckBox;
            CheckBox chkAllot = item.FindControl("chkAllot") as CheckBox;
            string lblCCode = (item.FindControl("lblCCode") as Label).ToolTip;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (lblCCode == dt.Rows[i]["USERNAME"].ToString())
                {


                    chkAllot.Checked = true;
                    cbHead.Checked = true;

                    //chkRegister.Checked = true;
                    //cbHeadReg.Checked = true;

                    if (dt.Rows[i]["ALLOTED"].ToString() == "1")
                    {
                        chkAllot.Checked = true;
                        cbHead.Checked = true;
                        chkAllot.BackColor = System.Drawing.Color.SeaGreen;

                    }
                    else
                    {
                        chkAllot.Checked = false;
                        cbHead.Checked = false;

                    }
                }
            }
            //cbHead.Enabled = false;
            //chkAllot.Enabled = false;
        }
    }

    private void Export()
    {
        string attachment = "attachment; filename=" + "Verification.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        int sessionNo = 0;
        sessionNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);

        DataSet dsfee = objSC.GetBranchAllotementList_JEEENSVDET(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    private void BindSeatcount_ForSVDET()
    {
        DataSet dsseatcount = objSC.getseatcount_ForSVDET(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));

        if (dsseatcount.Tables[0].Rows != null)
        {
            lstviewForSVDET.Visible = true;
            lvSeat.Visible = false;
            lstviewForSVDET.DataSource = dsseatcount.Tables[0];
            lstviewForSVDET.DataBind();

            lvStudents.Visible = false;
            pnlStudents.Visible = false;

        }
        else
        {
            objCommon.DisplayMessage("No Data Found", this.Page);
            lstviewForSVDET.Visible = false;
            lstviewForSVDET.DataSource = null;
            lstviewForSVDET.DataBind();
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADDMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_QUALIFYNO=" + Convert.ToInt32(ddlEntrance.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEntrance, "ACD_ENTRE_DEGREE ED INNER JOIN ACD_QUALEXM Q ON(ED.QUALIFYNO=Q.QUALIFYNO)", "ED.QUALIFYNO", "Q.QUALIEXMNAME", "ED.DEGREENO=" + ddlDegree.SelectedValue, "QUALIFYNO DESC");
            lvStudents.Visible = false;
            pnlStudents.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvSeat.DataSource = null;
            lvSeat.DataBind();
            lvSeat.Visible = false;
        }
        else
        {
            lvSeat.DataSource = null;
            lvSeat.DataBind();
            objCommon.DisplayMessage("No Data Found", this.Page);
            lvStudents.Visible = false;
            pnlStudents.Visible = false;
            ddlEntrance.Items.Clear();
            ddlEntrance.Items.Insert(0, "Please Select");

        }
    }


    protected void ddlEntrance_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSeat.DataSource = null;
        lvSeat.DataBind();
        lstviewForSVDET.DataSource = null;
        lstviewForSVDET.DataBind();
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        ddlRound.SelectedValue = "0";


    }




    protected void rdCatAllot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlEntrance.SelectedValue == "105")
            {
                BindSeatcount_ForSVDET();
                rdCatAllot.SelectedValue = "2";

            }
            else if (ddlEntrance.SelectedIndex > 0)
            {
                BindSeatcount();
            }
            else
            {
                lvSeat.DataSource = null;
                lvSeat.DataBind();
                lstviewForSVDET.DataSource = null;
                lstviewForSVDET.DataBind();
                lvSeat.Visible = false;
                lstviewForSVDET.Visible = false;
                objCommon.DisplayMessage("No Data Found", this.Page);
                lvStudents.Visible = false;
                pnlStudents.Visible = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefRound2.ddlRound_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedItem.Text != string.Empty)
            {
                if (ddlDegree.SelectedIndex > 0)
                {
                    if (ddlEntrance.SelectedIndex > 0)
                    {
                        this.ShowReport("", "rptBranchallotmentlist.rpt");
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Select Entrance Exam Name.", this.Page);
                        ddlEntrance.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Degree.", this.Page);
                    ddlDegree.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmBatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefRound2.btnpdf_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnexport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedItem.Text != string.Empty)
            {
                if (ddlDegree.SelectedIndex > 0)
                {
                    if (ddlEntrance.SelectedIndex > 0)
                    {
                        if (ddlRound.SelectedIndex > 0)
                        {
                            this.Export();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Select Round.", this.Page);
                            ddlRound.Focus();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Select Entrance Exam Name.", this.Page);
                        ddlEntrance.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Degree.", this.Page);
                    ddlDegree.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmBatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefRound2.btnexport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnAllot_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRound.SelectedIndex > 0)
            {
                string usernos = string.Empty;
                int IdExists = 0;
                int count = 0;
                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkAllot") as CheckBox;
                    if (chkBox.Checked == true && chkBox.Enabled == true)
                    {
                        usernos += chkBox.ToolTip + "$";
                    }
                    count++;

                    if (chkBox.Enabled == false)
                    {
                        IdExists++;
                    }
                }
                if (string.IsNullOrEmpty(usernos) && count != IdExists)
                {
                    objCommon.DisplayMessage("Please Select Atleast One Student.", this.Page);
                    return;
                }
                else if (count == IdExists)
                {
                    objCommon.DisplayMessage("Branches Are Already Alloted To The Students For Selected Degree " + ddlDegree.SelectedItem.Text + " Of Entrance Exam " + ddlEntrance.SelectedItem.Text + "", this.Page);
                    return;
                }
                else
                {
                    
                    // by sumit on 26/05/2017

                    int ret = 0;

                    if (ddlEntrance.SelectedValue == "105")
                    {
                        ret = objSC.AllotStudentBranch_ForSVDET(Convert.ToInt32(ddlAdmBatch.SelectedValue), usernos, ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
                        BindSeatcount_ForSVDET();

                    }
                    else
                    {
                        ret = objSC.AllotStudentBranch_ForBranchPREF(Convert.ToInt32(ddlAdmBatch.SelectedValue), usernos, ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue), Convert.ToInt32(rdCatAllot.SelectedValue));

                        BindSeatcount();
                    }


                    btnAllot.Enabled = false;
                    pnlStudents.Visible = false;
                    if (ret == Convert.ToInt32(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage("Branch Allotment Done Successfully !", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Failed To Allot Branch!", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Round!", this.Page);
                ddlRound.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefRound2.btnAllot_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            if (rdCatAllot.SelectedValue == "")
            {
                objCommon.DisplayMessage("Please Select Allotment Category.", this.Page);
                return;
            }
            else
            {
                this.rdCatAllot_SelectedIndexChanged(sender, e);
            }
            if (ddlAdmBatch.SelectedItem.Text != string.Empty)
            {
                if (ddlDegree.SelectedIndex > 0)
                {
                    if (ddlEntrance.SelectedIndex > 0)
                    {
                        
                        //for third round-JEEEnSVDET


                        DataSet ds = objSC.GetEligibleStudents_ForRountTwo_SVDETnJEEE(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue), Convert.ToInt32(rdCatAllot.SelectedValue));

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            pnlStudents.Visible = true;
                            btnAllot.Enabled = true;
                            lvStudents.DataSource = ds.Tables[0];
                            lvStudents.DataBind();
                            lvStudents.Visible = true;

                            //code commented for checked checkbox bydefault when alloted status is 1


                            foreach (ListViewDataItem lvItem in lvStudents.Items)
                            {
                                HiddenField lblidNo = lvItem.FindControl("hidIdNo") as HiddenField;
                                CheckBox chkb = lvItem.FindControl("chkAllot") as CheckBox;
                                if (lblidNo.Value == "1")
                                {
                                    chkb.Checked = true;
                                    chkb.Enabled = false;
                                }
                            }
                            //ListOperations(lvStudents, ds.Tables[0]);

                            int chklock = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCHPREF_LOG_ADM", "COUNT(1)", "ISNULL(LOCK,0)=1 AND SESSIONNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND  QUALIFYNO=" + Convert.ToInt32(ddlEntrance.SelectedValue) + "AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue)));
                            if (chklock > 0)
                            {
                                btnRCTRound.Enabled = false;
                                btnAllot.Enabled = false;
                            }
                            else
                            {
                                btnRCTRound.Enabled = true;
                                btnAllot.Enabled = true;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Record not found", this.Page);
                            lvStudents.DataSource = null;
                            lvStudents.DataBind();
                            lvStudents.Visible = false;
                        }



                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Select Entrance Exam Name.", this.Page);
                        ddlEntrance.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Degree.", this.Page);
                    ddlDegree.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmBatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefRound2.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnRCTRound_Click(object sender, EventArgs e)
    {
        int ret = 0;
        if (ddlRound.SelectedIndex > 0)
        {
            ret = objSC.ResetCounselingRound(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));


            if (ddlEntrance.SelectedValue == "105")
            {
                BindSeatcount_ForSVDET();
            }
            else
            {
                BindSeatcount();
            }


            if (ret == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updLists, "Branch Counseling Reset Successfully !", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(updLists, "Failed To Reset Counseling !", this.Page);
            }



        }

    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        int ret = 0;

        ret = objSC.LockBranchCounseling(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
        if (ret == Convert.ToInt32(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(updLists, "Branch Allotment Lock Successfully !", this.Page);
            btnRCTRound.Enabled = false;
            btnAllot.Enabled = false;
            return;
        }
        else
        {
            objCommon.DisplayMessage(updLists, "Failed To Lock  Branch Allotment!", this.Page);
            btnRCTRound.Enabled = true;
            btnAllot.Enabled = true;
            return;
        }

    }

    protected void btnCloseRound_Click(object sender, EventArgs e)
    {
        int ret = 0;

        ret = objSC.CloseBranchCounseling(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
        if (ret == Convert.ToInt32(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(updLists, "Branch Counseling Close Successfully !", this.Page);
            btnRCTRound.Enabled = false;
            btnAllot.Enabled = false;
            return;
        }
        else
        {
            objCommon.DisplayMessage(updLists, "Failed To Close  Branch Counseling!", this.Page);
            btnRCTRound.Enabled = true;
            btnAllot.Enabled = true;
            return;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string reportTitle = string.Empty;
            string rptFileName = string.Empty;

            this.SetReportFileAndTitle(ref reportTitle, ref rptFileName);

            if (ddlAdmBatch.SelectedItem.Text != string.Empty)
            {
                if (ddlDegree.SelectedIndex > 0)
                {
                    if (ddlEntrance.SelectedIndex > 0)
                    {
                        if (ddlRound.SelectedIndex > 0)
                        {
                            this.ShowReportStatus(reportTitle, rptFileName);
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Select Round.", this.Page);
                            ddlRound.Focus();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Select Entrance Exam Name.", this.Page);
                        ddlEntrance.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Degree.", this.Page);
                    ddlDegree.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmBatch.Focus();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefRound2.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void SetReportFileAndTitle(ref string reportTitle, ref string rptFileName)
    {
        if (ddlEntrance.SelectedIndex == 1)
        {
            reportTitle = "Svdet_Report";
            rptFileName = "StudentBranchCounselingStatusSvet.rpt";
        }
        else if (ddlEntrance.SelectedIndex == 2)
        {
            reportTitle = "Jee_Report";
            rptFileName = "StudentBranchCounselingStatusJee.rpt";
        }
    }

    private void ShowReportStatus(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_QUALIFYNO=" + Convert.ToInt32(ddlEntrance.SelectedValue) + ",@P_ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue) + ",@P_ALLOTCAT=" + Convert.ToInt32(rdCatAllot.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",AdmBatch=" + ddlAdmBatch.SelectedItem;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReportStatus() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlRound_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSeat.DataSource = null;
        lvSeat.DataBind();
        lstviewForSVDET.DataSource = null;
        lstviewForSVDET.DataBind();
        lvSeat.Visible = false;
        lstviewForSVDET.Visible = false;
        lvStudents.Visible = false;
        pnlStudents.Visible = false;

    }
}