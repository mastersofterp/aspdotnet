using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_PhdStudProgress : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    PhdController objPhdC = new PhdController();
    bool flag = false;

    string ua_dept = string.Empty;


    #region Page Load
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    if (ViewState["action"] == null)
                        ViewState["action"] = "add";
                    string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ViewState["usertype"] = ua_type;
                    ViewState["dec"] = ua_dec;
                    FillDropDown();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        updEdit.Visible = false;
                        divmain.Visible = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = false;
                        pnlRemark.Visible = false;
                        Panel1.Visible = false;
                        divAdmBatch.Visible = false;
                        // PnlAppRemark.Visible = false;
                        ShowStudentDetails();

                    }
                    else if
                        (ViewState["usertype"].ToString() == "1")
                    {
                        ShowStudentDetails();
                        btnSave.Visible = false;
                        btnCancel.Visible = true;
                        btnApprove.Visible = true;
                        pnlRemark.Visible = true;
                        updEdit.Visible = true;
                        divAdmBatch.Visible = false;
                        divCriteria.Visible = true;
                        ControlActivityOFF();
                    }

                    else
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = true;
                        btnApprove.Visible = true;
                        pnlRemark.Visible = true;
                        updEdit.Visible = true;
                        divAdmBatch.Visible = true;
                        divCriteria.Visible = false;
                        // ShowStudentDetails();
                        ControlActivityOFF();
                    }

                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        ControlActivityOFF();
                    }

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

            }
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
                Response.Redirect("~/notauthorized.aspx?page=PhdStudProgress.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdStudProgress.aspx");
        }
    }
    public void ControlActivityOFF()
    {
        // txtResearch.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = CheckBox1.Enabled = false;
        txtReserchTopic.Enabled = txtWorkDone.Enabled = false;
    }
    public void ControlActivityON()
    {
        // txtResearch.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = CheckBox1.Enabled = false;
        txtReserchTopic.Enabled = txtWorkDone.Enabled = true;
    }
    private void FillDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA_PHD", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO"); //added on 27/03/23
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region DYNAMIC SEARCH

    private void bindlist(string category, string searchtext)
    {
        //added on 27/03/23

        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "1")
        {
            DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Panellistview.Visible = true;
                lvStudent.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblNoRecords.Text = "Total Records : 0";
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
        }
        else
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());

            DataSet ds = objPhdC.RetrieveStudentDetailsPHDforFaculty(uano, Convert.ToInt32(ddlAdmBatch.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                Panellistview.Visible = true;
                lvStudent.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblNoRecords.Text = "Total Records : 0";
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        Session["idno"] = Session["stuinfoidno"].ToString();
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        divmain.Visible = true;

        divmain.Visible = true;
        DivSutLog.Visible = true;
        updEdit.Visible = false;
        Panellistview.Visible = false;
        ShowStudentDetails();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }
        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        if (ViewState["usertype"].ToString() == "1")
        {
            divCriteria.Visible = true;
            divpanel.Visible = true;
        }
        else
        {
            divCriteria.Visible = false;
            divpanel.Visible = false;
        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails_Phd(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;


                        objCommon.FillDropDownList(ddlDropdown, tablename, "DISTINCT " + column1, column2, "UGPGOT=3", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
        txtSearch.Text = string.Empty;
    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            //  this.objCommon.FillDropDownList(ddlsem, "ACD_SEMESTER A INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=A.SEMESTERNO) ", "S.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO > 0 AND S.IDNO =" + Convert.ToInt32(Session["idno"]), "A.SEMESTERNO");
        }
        else
        {
            if (Request.QueryString["id"] != null)
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));

            }
            else
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));

            }
        }
        this.objCommon.FillDropDownList(ddlsem, "ACD_SEMESTER A INNER JOIN ACD_STUDENT S ON(S.SEMESTERNO=A.SEMESTERNO) ", "S.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO > 0 AND S.IDNO =" + Convert.ToInt32(Session["idno"]), "A.SEMESTERNO");
        if (dtr != null)
        {
            if (dtr.Read())
            {
                string AllotSup = objCommon.LookUp("ACD_PHD_ALLOTED_SUPERVISOR", "count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]));
                string scheme = objCommon.LookUp("acd_student", "isnull(schemeno,0)", "IDNO=" + Convert.ToInt32(Session["idno"]));
                string deanStatus = objCommon.LookUp("ACD_PHD_ALLOTED_SUPERVISOR", "DEAN_APPROVE", "IDNO=" + Convert.ToInt32(Session["idno"]));
                if (AllotSup == "0")
                {

                    objCommon.DisplayMessage(updEdit, "Supervisor Allotment Not Done", this.Page);
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        updEdit.Visible = false;
                    }
                    else
                    {
                        updEdit.Visible = true;
                    }
                    divmain.Visible = false;
                    ddlSearch.SelectedIndex = 0;
                    return;
                }
                else
                {
                    if (deanStatus == "0")
                    {
                        objCommon.DisplayMessage(updEdit, "Dean Approval Not Done", this.Page);
                        if (ViewState["usertype"].ToString() == "2")
                        {
                            updEdit.Visible = false;
                        }
                        else
                        {
                            updEdit.Visible = true;
                        }
                        divmain.Visible = false;
                        ddlSearch.SelectedIndex = 0;
                        return;
                    }
                    else
                    {
                        if (scheme == "0")
                        {
                            objCommon.DisplayMessage(updEdit, "Scheme Allotment Not Done", this.Page);
                            if (ViewState["usertype"].ToString() == "2")
                            {
                                updEdit.Visible = false;
                            }
                            else
                            {
                                updEdit.Visible = true;
                            }
                            divmain.Visible = false;
                            ddlSearch.SelectedIndex = 0;
                            return;
                        }
                        else
                        {
                            lblidno.Text = dtr["IDNO"].ToString();
                            lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
                            lbladmbatch.Text = dtr["ADMBATCHNAME"].ToString();
                            lblnames.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                            lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                            lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                            lblDepartment.Text = dtr["BRANCHNAME"].ToString();
                            ViewState["SCHEMENO"] = dtr["SCHEMENO"].ToString();
                            ViewState["DEGREENO"] = dtr["DEGREENO"].ToString();
                            lblScheme.Text = dtr["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dtr["SCHEMENO"].ToString();
                            lblDepartment.ToolTip = dtr["BRANCHNO"].ToString();
                            lbladmbatch.Text = dtr["ADMBATCHNAME"].ToString();
                            if (dtr["PHDSTATUS"] == null)
                            {
                                lblstatussup.Text = "";

                            }
                            if (dtr["PHDSTATUS"].ToString() == "1")
                            {
                                lblstatussup.Text = "Full Time";

                            }
                            if (dtr["PHDSTATUS"].ToString() == "2")
                            {
                                lblstatussup.Text = "Part Time";

                            }
                            if (dtr["SUPERROLE"].ToString() == "S")
                            {
                                lblSuperRole.Text = "Singly";
                            }
                            else
                                if (dtr["SUPERROLE"].ToString() == "J")
                                {
                                    lblSuperRole.Text = "Jointly";
                                }
                                else if (dtr["SUPERROLE"].ToString() == "T")
                                {
                                    lblSuperRole.Text = "Multiple";
                                }
                            lblNDM.Text = dtr["NOOFDGC"].ToString();
                            lblAoR.Text = dtr["RESEARCH"].ToString();
                        }
                    }
                }

            }
        }
    }

    private void REPORTAPPROVE()
    {
        try
        {
            string SP_Name2 = "PKG_PHD_GET_SUPERVISOR_APPROVAL";
            string SP_Parameters2 = "@P_IDNO , @P_SEMESTERNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "," +
                                 Convert.ToInt32(ddlsem.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvApprove.DataSource = ds;
                lvApprove.DataBind();
                lvApprove.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ProgressReport()
    {
        try
        {

            DataSet ds = null;
            if (ViewState["usertype"].ToString() == "2")
            {
                ds = objPhdC.GetPROGRESSReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlsem.SelectedValue), Convert.ToInt32(Session["userno"]));
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    ds = objPhdC.GetPROGRESSReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlsem.SelectedValue), Convert.ToInt32(Session["userno"]));

                }
                else
                {
                    ds = objPhdC.GetPROGRESSReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlsem.SelectedValue), Convert.ToInt32(Session["userno"]));

                }
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtReserchTopic.Text = ds.Tables[0].Rows[0]["RESEARCH_TOPIC"].ToString();
                txtWorkDone.Text = ds.Tables[0].Rows[0]["WORK_DESC"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["WORK_REMARK"].ToString().Trim() == "" ? string.Empty : ds.Tables[0].Rows[0]["WORK_REMARK"].ToString().Trim();
                ddlGreade.SelectedValue = ds.Tables[0].Rows[0]["GREAD_AWARDED"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["GREAD_AWARDED"].ToString().Trim();
                txtComment.Text = ds.Tables[0].Rows[0]["COMMENTS"].ToString().Trim() == "" ? string.Empty : ds.Tables[0].Rows[0]["COMMENTS"].ToString().Trim();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region SUBMIT AND APPROVE

    protected void ddlsem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsem.SelectedIndex > 0)
        {
          DataSet  ds = objPhdC.GetPROGRESSReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlsem.SelectedValue), Convert.ToInt32(Session["userno"]));
          if (ds.Tables[0].Rows.Count > 0)
          {
              ProgressReport();
              REPORTAPPROVE();
              btnApprove.Enabled = true;
          }
          else
          {
              objCommon.DisplayMessage(this, "Progress Report Not submit By Student For Selected Semester", this.Page);
              btnApprove.Enabled = false;
             
          }
        }
        else
        {
            txtWorkDone.Text = string.Empty;
            txtReserchTopic.Text = string.Empty;
            ddlGreade.SelectedIndex = 0;
            txtRemark.Text = string.Empty;
            txtComment.Text = string.Empty;
            lvApprove.Visible = false;
            lvApprove.DataSource = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int IDNO = Convert.ToInt32(Session["idno"]);
            int ADMBATCH = Convert.ToInt32(objCommon.LookUp("acd_student", "admbatch", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int DEGREENO = Convert.ToInt32(ViewState["DEGREENO"].ToString());
            int BRANCHNO = Convert.ToInt32(lblDepartment.ToolTip);
            int SEMESTERNO = Convert.ToInt32(ddlsem.SelectedValue);
            int TOTAL_NO_CREDITS = 16;
            string ROLL_NO = objCommon.LookUp("acd_student", "ROLLNO", "IDNO=" + IDNO);
            string RESEARCH_TOPIC = txtReserchTopic.Text;
            string WORK_DESC = txtWorkDone.Text;
            //string WORK_REMARK = txtRemark.Text;
            //string COMMENTS = txtComment.Text;
            //int GREAD_AWARDED = Convert.ToInt32(ddlGreade.SelectedValue);
            int REGISTERED_BY = Convert.ToInt32(Session["userno"]);
            string IPADDRESS = Session["ipAddress"].ToString();
            int COLLEGE_ID = Convert.ToInt32(objCommon.LookUp("acd_student", "college_id", "IDNO=" + Convert.ToInt32(Session["idno"])));

            string SEMESTERNO1 = objCommon.LookUp("ACD_PHD_PROGRESS_REPORT", "count(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "and SEMESTERNO=" + Convert.ToInt32(ddlsem.SelectedValue));
            if (Convert.ToInt32(SEMESTERNO1)>0)
            {

                ViewState["action"] = "edit";
            }
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //CustomStatus cs = (CustomStatus)objPhdC.InsertProgressReport(IDNO, ADMBATCH, DEGREENO, BRANCHNO, SEMESTERNO, TOTAL_NO_CREDITS, ROLL_NO, RESEARCH_TOPIC, WORK_DESC, WORK_REMARK, COMMENTS, GREAD_AWARDED, REGISTERED_BY, IPADDRESS, COLLEGE_ID);
                CustomStatus cs = (CustomStatus)objPhdC.InsertProgressReport(IDNO, ADMBATCH, DEGREENO, BRANCHNO, SEMESTERNO, TOTAL_NO_CREDITS, ROLL_NO, RESEARCH_TOPIC, WORK_DESC, REGISTERED_BY, IPADDRESS, COLLEGE_ID);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                }
            }
            else
            {

                //CustomStatus cs = (CustomStatus)objPhdC.InsertProgressReport(IDNO, ADMBATCH, DEGREENO, BRANCHNO, SEMESTERNO, TOTAL_NO_CREDITS, ROLL_NO, RESEARCH_TOPIC, WORK_DESC, WORK_REMARK, COMMENTS, GREAD_AWARDED, REGISTERED_BY, IPADDRESS, COLLEGE_ID);
                CustomStatus cs = (CustomStatus)objPhdC.InsertProgressReport(IDNO, ADMBATCH, DEGREENO, BRANCHNO, SEMESTERNO, TOTAL_NO_CREDITS, ROLL_NO, RESEARCH_TOPIC, WORK_DESC, REGISTERED_BY, IPADDRESS, COLLEGE_ID);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    Response.Redirect(Request.Url.ToString());

                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int SUPERVISOR_UANO = 0;
            int SUPERVISOR_EXT_UANO = 0;
            int JOINTSUPERVISOR1_UANO = 0;
            int JOINTSUPERVISOR1_EXT_UANO = 0;
            int INSTITUTEFACULTY_UANO = 0;
            int INSTITUTEFACULTY_EXT_UANO = 0;
            int JOINTSUPERVISOR2_UANO = 0;
            int JOINTSUPERVISOR2_EXT_UANO = 0;
            int DRC_UANO = 0;
            int DRC_EXT_UANO = 0;
            int DRCCHAIRMAN_UANO = 0;
            string SUPERROLE = string.Empty;
            string role = string.Empty;
            int ext_status = 0;
            int IDNO = Convert.ToInt32(Session["idno"]);
            int ADMBATCH = Convert.ToInt32(objCommon.LookUp("acd_student", "admbatch", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int DEGREENO = Convert.ToInt32(ViewState["DEGREENO"].ToString());
            int BRANCHNO = Convert.ToInt32(lblDepartment.ToolTip);
            int SEMESTERNO = Convert.ToInt32(ddlsem.SelectedValue);
            int COLLEGE_ID = Convert.ToInt32(objCommon.LookUp("acd_student", "college_id", "IDNO=" + Convert.ToInt32(Session["idno"])));
            //string SUPERROLE = objCommon.LookUp("ACD_PHD_ALLOTED_SUPERVISOR", "SUPERROLE", "IDNO=" + Convert.ToInt32(Session["idno"]));
            int UANO = Convert.ToInt32(Session["userno"]);
            string WORK_REMARK = txtRemark.Text;
            string COMMENTS = txtComment.Text;
            int GREAD_AWARDED = Convert.ToInt32(ddlGreade.SelectedValue);

            string SP_Name2 = "PKG_PHD_GET_SUPERVISOR_DETAILS";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SUPERROLE = ds.Tables[0].Rows[0]["SUPERROLE"].ToString();
                SUPERVISOR_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISOR_UANO"].ToString());
                SUPERVISOR_EXT_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISOR_EXT_UANO"].ToString());
                JOINTSUPERVISOR1_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISOR1_UANO"].ToString());
                JOINTSUPERVISOR1_EXT_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISOR1_EXT_UANO"].ToString());
                INSTITUTEFACULTY_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTY_UANO"].ToString());
                INSTITUTEFACULTY_EXT_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTY_EXT_UANO"].ToString());
                JOINTSUPERVISOR2_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISOR2_UANO"].ToString());
                JOINTSUPERVISOR2_EXT_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISOR2_EXT_UANO"].ToString());
                DRC_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["DRC_UANO"].ToString());
                DRC_EXT_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["DRC_EXT_UANO"].ToString());
                DRCCHAIRMAN_UANO = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCCHAIRMAN_UANO"].ToString());
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Supervisor Allotment Not Done", this.Page);
                return;
            }

            if (SUPERVISOR_UANO == UANO)
            {
                role = "SUPERVISOR";
                ext_status = 0;
            }
            else if (SUPERVISOR_EXT_UANO == UANO)
            {
                role = "SUPERVISOR";
                ext_status = 1;
            }
            else if (JOINTSUPERVISOR1_UANO == UANO)
            {
                role = "JOINTSUPERVISOR1";
                ext_status = 0;
            }
            else if (JOINTSUPERVISOR1_EXT_UANO == UANO)
            {
                role = "JOINTSUPERVISOR1";
                ext_status = 1;
            }
            else if (INSTITUTEFACULTY_UANO == UANO)
            {
                role = "INSTITUTEFACULTY";
                ext_status = 0;
            }
            else if (INSTITUTEFACULTY_EXT_UANO == UANO)
            {
                role = "INSTITUTEFACULTY";
                ext_status = 1;
            }
            else if (JOINTSUPERVISOR2_UANO == UANO)
            {
                role = "JOINTSUPERVISOR2";
                ext_status = 0;
            }
            else if (JOINTSUPERVISOR2_EXT_UANO == UANO)
            {
                role = "JOINTSUPERVISOR2";
                ext_status = 1;
            }
            else if (DRC_UANO == UANO)
            {
                role = "DRC";
                ext_status = 0;
            }
            else if (DRC_EXT_UANO == UANO)
            {
                role = "DRC";
                ext_status = 1;
            }
            else if (DRCCHAIRMAN_UANO == UANO)
            {
                role = "DRCCHAIRMAN";
                ext_status = 0;
            }
            if (txtReserchTopic.Text == string.Empty || txtWorkDone.Text == string.Empty)
            {
                objCommon.DisplayMessage(updEdit, "Student Information is Incomplete", this.Page);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objPhdC.ApproveProgressReport(IDNO, ADMBATCH, DEGREENO, BRANCHNO, SEMESTERNO, COLLEGE_ID, SUPERROLE, UANO, role, ext_status, WORK_REMARK, COMMENTS, GREAD_AWARDED);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Student Progress Report Approve sucessfully", this.Page);
                }
                REPORTAPPROVE();

            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    #endregion

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Text = string.Empty;
        divCriteria.Visible = false;
        divpanel.Visible = false;
    }
}