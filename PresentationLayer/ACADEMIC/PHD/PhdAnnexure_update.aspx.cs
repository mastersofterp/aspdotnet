using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_PhdAnnexure_update : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    PhdController objPhdC = new PhdController();
    string ua_dept = string.Empty;
    string UANO = string.Empty;

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
                 //  this.CheckPageAuthorization();

                    
                    //nodgc.Visible = false;
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
                        DivDrops.Visible = true;
                        ShowStudentDetails();
                        Div2.Visible = false;
                        divAdmBatch.Visible = false;
                        PnlSP.Visible = false;
                        divchairman.Visible = false;
                        pnlApprove.Visible = true;
                        REPORTAPPROVE();
                        CheckBox1.Visible = false; // Added By Vipul Tichakule on dated 20-11-2023 as per TicketNo -


                    }
                    else if (ViewState["usertype"].ToString() == "1" )
                    {
                        txtRemark.Enabled = true;
                        DivDrops.Visible = true;
                        PnlSP.Visible = true;
                        btnReject.Visible = false;
                        btnApprove.Visible = false;
                        expertrow.Visible = false;
                        secondsupervisor.Visible = false;
                        Div2.Visible = false;
                        updEdit.Visible = true;
                        divAdmBatch.Visible = false;
                        divCriteria.Visible = true;
                        divchairman.Visible = true;
                        pnlApprove.Visible = false;
                    }
                    else 
                    {
                        txtRemark.Enabled = true;
                        DivDrops.Visible = true;
                        PnlSP.Visible = true;
                        btnReject.Visible = false;
                        btnApprove.Visible = false;
                        expertrow.Visible = false;
                        secondsupervisor.Visible = false;
                        Div2.Visible = false;
                        updEdit.Visible = true;
                        divAdmBatch.Visible = true;
                        divCriteria.Visible = false;
                        divchairman.Visible = true;
                        pnlApprove.Visible = false;
                    }
                    //else if (ViewState["usertype"].ToString() == "4")
                    //{
                    //    txtRemark.Enabled = true;
                    //    DivDrops.Visible = true;
                    //    PnlSP.Visible = true;
                    //    btnReject.Visible = true;
                    //    btnApprove.Visible = true;
                    //    expertrow.Visible = false;
                    //    secondsupervisor.Visible = false;
                    //    ControlActivityOFF();
                    //}

                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
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
    public void ControlActivityOFF_STUDENT()
    {
        string STATUS = objCommon.LookUp("ACD_PHD_ALLOTED_SUPERVISOR", "ISNULL(DEAN_APPROVE,0)", "IDNO=" + Convert.ToInt32(Session["idno"]));
        if (STATUS == "1")
        {
            txtResearch.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = CheckBox1.Enabled = Div2.Visible = false;
            ddlCommittee.Enabled = ddlDGCSupervisor.Enabled = CheckBox3.Enabled = ddlMember.Enabled = ddlJointSupervisor.Enabled = CheckBox2.Enabled = ddlMember1.Enabled = false;
            ddlInstFac.Enabled = CheckBox4.Enabled = ddlMember2.Enabled = ddlJointSupervisorSecond.Enabled = CheckBox6.Enabled = ddlMember5.Enabled = ddlDRC.Enabled = false;
            CheckBox5.Enabled = ddlMember3.Enabled = ddlDRCChairman.Enabled =btnApprove.Enabled= false;
        }
        else
        {
            txtResearch.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = CheckBox1.Enabled =  true;
            ddlCommittee.Enabled = ddlDGCSupervisor.Enabled = CheckBox3.Enabled = ddlMember.Enabled = ddlJointSupervisor.Enabled = CheckBox2.Enabled = ddlMember1.Enabled = true;
            ddlInstFac.Enabled = CheckBox4.Enabled = ddlMember2.Enabled = ddlJointSupervisorSecond.Enabled = CheckBox6.Enabled = ddlMember5.Enabled = ddlDRC.Enabled = true;
            CheckBox5.Enabled = ddlMember3.Enabled = ddlDRCChairman.Enabled = btnApprove.Enabled = true;
        }
    }
    public void ControlActivityOFF()
    {
       
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure_update.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure_update.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO"); //added on 27/03/23

            objCommon.FillDropDownList(ddlSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA_PHD", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
            objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            objCommon.FillDropDownList(ddlInstFac, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            objCommon.FillDropDownList(ddlJointSupervisorSecond, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            objCommon.FillDropDownList(ddlCommittee, "ACD_PHD_COMMITTEE", "COMMITTEE_ID", "COMMITTEE_NAME", "isnull(ACTIVESTATUS,0)=1 and isnull(COMMITTEE_STATUS,0)=1 ", "COMMITTEE_ID");
            objCommon.FillDropDownList(ddlDRC, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void REPORTAPPROVE()
    {
        try
        {
            string SP_Name2 = "PKG_PHD_GET_ALLOTED_SUPERVISOR_APPROVAL";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "" ;
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
    #endregion

    #region Dynamic Search

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
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

                        if (tablename == "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO= CDB.BRANCHNO)" || tablename == "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(D.DEGREENO= CDB.DEGREENO)")
                        {

                            objCommon.FillDropDownList(ddlDropdown, tablename, "DISTINCT " + column1, column2, "UGPGOT=3", column1);
                        }
                        else
                        {

                            objCommon.FillDropDownList(ddlDropdown, tablename, "DISTINCT " + column1, column2, ""+column1+">0", column1);
                        }

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
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "1")
        {
            DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
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
        ShowStudentDetails();
        divmain.Visible = true;
        DivSutLog.Visible = true;
        updEdit.Visible = false;
        Panellistview.Visible = false;
    }
    #endregion

    #region Supervisior
    private void ShowFacuilty()
    {
        try
        {

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ShowFacuilty()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
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
        if (dtr != null)
        {
            if (dtr.Read())
            {
                //if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "2")
                //{
                    ControlActivityOFF_STUDENT();
               // }

                lblidno.Text = dtr["IDNO"].ToString();
                lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
                lbladmbatch.Text = dtr["ADMBATCHNAME"].ToString();
                lblnames.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblDepartment.Text = dtr["BRANCHNAME"].ToString();
                lblModeOfStudy.Text = dtr["PHD_MODE"] == null ? string.Empty : dtr["PHD_MODE"].ToString();
                lblEmailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                lblSession.Text = dtr["ADMISSION_SESSION_NAME"] == null ? string.Empty : dtr["ADMISSION_SESSION_NAME"].ToString();
                lblMobileNo.Text = dtr["STUDENTMOBILE"].ToString();
           
                string SEMESTERNO = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
                int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + Convert.ToInt16(SEMESTERNO) + "AND ISNULL(CANCEL,0)=0"));
                if (count > 0)
                {
                    ddlStatusCat.SelectedValue = "1";
                }
                else
                {
                    ddlStatusCat.SelectedValue = "2";
                }
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
                if (dtr["PHDSTATUS"] == null)
                {
                    partfull.Text = "";

                }
                //-----------------------------------------------//
                if (dtr["PHDSTATUS"].ToString() == "1")
                {
                    partfull.Text = "Fulltime";

                }
                if (dtr["PHDSTATUS"].ToString() == "2")
                {
                    partfull.Text = "Parttime";

                }
                lblname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lbldate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                if (dtr["DEAN_APPROVE"].ToString().Equals("1"))
                {
                    lblRejectStatus.Text = dtr["Status"].ToString();
                    lblRejectStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblRejectStatus.Text = dtr["Status"].ToString();
                    lblRejectStatus.ForeColor = System.Drawing.Color.Red;
                }

               // ddlStatusCat.SelectedValue = dtr["COURSEWORK_STATUS"].ToString();
                if (dtr["SUPERVISOR_EXT_UANO"].ToString() == "0")
                {
                    CheckBox1.Checked = false;
                    objCommon.FillDropDownList(ddlSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                   // ddlSupervisor.SelectedValue = dtr["SUPERVISOR_UANO"].ToString();
                    ddlSupervisor.SelectedValue = dtr["SUPERVISOR_UANO"].ToString() == "0" ? "0" : dtr["SUPERVISOR_UANO"].ToString();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                    if (Convert.ToInt32(ddlSupervisor.SelectedValue) > 0)
                    {
                        ddlSupervisor.Enabled = false;
                        CheckBox1.Enabled=false;       
                    }
                    else
                    {
                        ddlSupervisor.Enabled = true;
                        CheckBox1.Enabled = true;   
                    }
                    }
                    supervisor();
                }
                else
                {
                    CheckBox1.Checked = true;
                    objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                    ddlSupervisor.SelectedValue = dtr["SUPERVISOR_EXT_UANO"].ToString();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        if (Convert.ToInt32(ddlSupervisor.SelectedValue) > 0)
                        {
                            ddlSupervisor.Enabled = false;
                            CheckBox1.Enabled = false;
                        }
                        else
                        {
                            ddlSupervisor.Enabled = true;
                            CheckBox1.Enabled = true;
                        }
                    }
                    supervisor();
                }
                ddlSupervisorrole.SelectedValue = dtr["SUPERROLE"].ToString();
                Role();
                txtResearch.Text = dtr["RESEARCH"].ToString();
               //ddlNdgc.SelectedValue = dtr["NOOFDGC"].ToString();
                txtRemark.Text = dtr["REMARK"].ToString();
                if (ViewState["usertype"].ToString() == "2")
                {
                    if (txtResearch.Text =="")
                    {
                        txtResearch.Enabled = true;
                    }
                    else
                    {
                        txtResearch.Enabled = false;
                    }
                }
                ddlCommittee.SelectedValue = dtr["COMMITTENO"].ToString();
                if (dtr["SUPERVISOR_EXT_UANO"].ToString() == "0")
                {
                    objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 AND COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                }
                else
                {
                    objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 AND COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                }
                ddlMember.SelectedValue = dtr["SUPERVISOR_DISGNNO"].ToString();

                if (dtr["JOINTSUPERVISOR1_EXT_UANO"].ToString() == "0")
                {
                    CheckBox2.Checked = false;
                    objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                    objCommon.FillDropDownList(ddlMember1, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlJointSupervisor.SelectedValue = dtr["JOINTSUPERVISOR1_UANO"].ToString() == "0" ? "0" : dtr["JOINTSUPERVISOR1_UANO"].ToString();
                    ddlMember1.SelectedValue = dtr["JOINSUPERVISOR1_DISGNNO"].ToString() == "0" ? "0" : dtr["JOINSUPERVISOR1_DISGNNO"].ToString();
                }
                else
                {
                    CheckBox2.Checked = true;
                    objCommon.FillDropDownList(ddlJointSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                    objCommon.FillDropDownList(ddlMember1, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlMember1.SelectedValue = dtr["JOINSUPERVISOR1_DISGNNO"].ToString() == "0" ? "0" : dtr["JOINSUPERVISOR1_DISGNNO"].ToString();
                    ddlJointSupervisor.SelectedValue = dtr["JOINTSUPERVISOR1_EXT_UANO"].ToString() == "0" ? "0" : dtr["JOINTSUPERVISOR1_EXT_UANO"].ToString();
                }
                if (dtr["INSTITUTEFACULTY_EXT_UANO"].ToString() == "0")
                {
                    CheckBox4.Checked = false;
                    //objCommon.FillDropDownList(ddlInstFac, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                    objCommon.FillDropDownList(ddlInstFac ,"USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                    objCommon.FillDropDownList(ddlMember2, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(),"PD.DESIG_ID");
                    ddlMember2.SelectedValue=dtr["INSTITUTEFAC_DISGNNO"].ToString()== "0" ? "0" : dtr["INSTITUTEFAC_DISGNNO"].ToString();
                    ddlInstFac.SelectedValue=dtr["INSTITUTEFACULTY_UANO"].ToString()== "0" ? "0" : dtr["INSTITUTEFACULTY_UANO"].ToString();
                }
                else
                {
                    CheckBox4.Checked = true;
                    objCommon.FillDropDownList(ddlInstFac, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                    objCommon.FillDropDownList(ddlMember2, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlMember2.SelectedValue=dtr["INSTITUTEFAC_DISGNNO"].ToString()== "0" ? "0" : dtr["INSTITUTEFAC_DISGNNO"].ToString();
                    ddlInstFac.SelectedValue=dtr["INSTITUTEFACULTY_EXT_UANO"].ToString()== "0" ? "0" : dtr["INSTITUTEFACULTY_EXT_UANO"].ToString();                  
                }
                if (dtr["JOINTSUPERVISOR2_EXT_UANO"].ToString() == "0")
                {
                    CheckBox6.Checked = false;
                    objCommon.FillDropDownList(ddlJointSupervisorSecond, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                    objCommon.FillDropDownList(ddlMember5, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlMember5.SelectedValue=dtr["JOINSUPERVISOR2_DISGNNO"].ToString()== "0" ? "0" : dtr["JOINSUPERVISOR2_DISGNNO"].ToString();
                    ddlJointSupervisorSecond.SelectedValue=dtr["JOINTSUPERVISOR2_UANO"].ToString()== "0" ? "0" : dtr["JOINTSUPERVISOR2_UANO"].ToString();
                }
                else
                {
                    CheckBox6.Checked = true;
                    objCommon.FillDropDownList(ddlJointSupervisorSecond, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                    objCommon.FillDropDownList(ddlMember5, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" +  dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlMember5.SelectedValue=dtr["JOINSUPERVISOR2_DISGNNO"].ToString()== "0" ? "0" : dtr["JOINSUPERVISOR2_DISGNNO"].ToString();
                    ddlJointSupervisorSecond.SelectedValue=dtr["JOINTSUPERVISOR2_EXT_UANO"].ToString()== "0" ? "0" : dtr["JOINTSUPERVISOR2_EXT_UANO"].ToString();
                }
                if (dtr["DRC_EXT_UANO"].ToString() == "0")
                {
                    CheckBox5.Checked = false;
                    objCommon.FillDropDownList(ddlDRC, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                    objCommon.FillDropDownList(ddlMember3, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlMember3.SelectedValue = dtr["DRC_DISGNNO"].ToString() == "0" ? "0" : dtr["DRC_DISGNNO"].ToString();
                    ddlDRC.SelectedValue = dtr["DRC_UANO"].ToString() == "0" ? "0" : dtr["DRC_UANO"].ToString();
                }
                else
                {
                    CheckBox5.Checked = true;
                    objCommon.FillDropDownList(ddlDRC, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                    objCommon.FillDropDownList(ddlMember3, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + dtr["COMMITTENO"].ToString(), "PD.DESIG_ID");
                    ddlMember3.SelectedValue = dtr["DRC_DISGNNO"].ToString() == "0" ? "0" : dtr["DRC_DISGNNO"].ToString();
                    ddlDRC.SelectedValue = dtr["DRC_EXT_UANO"].ToString() == "0" ? "0" : dtr["DRC_EXT_UANO"].ToString();
                }
                ddlDRCChairman.SelectedValue = dtr["DRCCHAIRMAN_UANO"].ToString() == "0" ? "0" : dtr["DRCCHAIRMAN_UANO"].ToString();
            }
        }
    }
    public void supervisor()
    {
        if (ddlSupervisor.SelectedIndex > 0)
        {
            if (CheckBox1.Checked)
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO=" + ddlSupervisor.SelectedValue, "NAME");
                CheckBox3.Checked = true;
                ddlDGCSupervisor.Focus();
               // ddlDGCSupervisor.SelectedIndex = 1;
                ddlDGCSupervisor.SelectedValue = ddlSupervisor.SelectedValue;
                ddlDGCSupervisor.Enabled = false;
                CheckBox3.Enabled = false;

            }

            else
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 and ua_no=" + ddlSupervisor.SelectedValue, "ua_fullname");
                // objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO)", "UA_NO", "UA_FULLNAME", "DESIGNATIONNO IN (1,3) and ua_no=" + ddlSupervisor.SelectedValue, "ua_fullname");
                ddlDGCSupervisor.Focus();
               // ddlDGCSupervisor.SelectedIndex = 1;
                ddlDGCSupervisor.SelectedValue = ddlSupervisor.SelectedValue;
                ddlDGCSupervisor.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox3.Checked = false;
            }
        }
        else
        {
            ddlDGCSupervisor.SelectedIndex = 0;
            ddlDGCSupervisor.Focus();
            if (CheckBox1.Checked)
            {
                CheckBox3.Enabled = false;
                CheckBox3.Checked = true;
            }
            else
            {
                CheckBox3.Enabled = false;
                CheckBox3.Checked = false;
            }
        }
       // ddlSupervisorrole.SelectedIndex = 0;
    }
    public void Role()
    {
        if (ddlSupervisorrole.SelectedValue == "S")
        {
            expertrow.Visible = false;
            secondsupervisor.Visible = false;
        }
        else
            if (ddlSupervisorrole.SelectedValue == "J")
            {
                expertrow.Visible = true;

            }
            else if (ddlSupervisorrole.SelectedValue == "T")
            {
                expertrow.Visible = true;
                secondsupervisor.Visible = true;
            }
    }

    protected void ddlSupervisorrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        Role();
    }
    protected void ddlNdgc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
            CheckBox3.Checked = true;
            ddlDGCSupervisor.SelectedIndex = 0;
            ddlDGCSupervisor.Focus();
            if (CheckBox3.Checked)
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 AND COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            }

            else
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            }
        }

        else
        {
            objCommon.FillDropDownList(ddlSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            CheckBox3.Checked = false;
            ddlDGCSupervisor.SelectedIndex = 0;
            ddlDGCSupervisor.Focus();
            if (CheckBox3.Checked)
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 AND COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            }

            else
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
                objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            }
        }
    }
    protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupervisor.SelectedIndex > 0)
        {
            if (CheckBox1.Checked)
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO=" + ddlSupervisor.SelectedValue, "NAME");
                CheckBox3.Checked = true;
                ddlDGCSupervisor.Focus();
                ddlDGCSupervisor.SelectedIndex = 1;
                ddlDGCSupervisor.Enabled = false;
                CheckBox3.Enabled = false;
                

            }

            else
            {
                objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 and ua_no=" + ddlSupervisor.SelectedValue, "ua_fullname");
               // objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO)", "UA_NO", "UA_FULLNAME", "DESIGNATIONNO IN (1,3) and ua_no=" + ddlSupervisor.SelectedValue, "ua_fullname");
                ddlDGCSupervisor.Focus();
                ddlDGCSupervisor.SelectedIndex = 1;
                ddlDGCSupervisor.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox3.Checked = false;
            }
            FillDropDownNotIN();
        }
        else
        {
            ddlDGCSupervisor.SelectedIndex = 0;
            ddlDGCSupervisor.Focus();
            if (CheckBox1.Checked)
            {
                CheckBox3.Enabled = false;
                CheckBox3.Checked = true;
            }
            else
            {
                CheckBox3.Enabled = false;
                CheckBox3.Checked = false;
            }
            FillDropDownNotIN();
        }
        //ddlSupervisorrole.SelectedIndex = 0;
    }

    #region DDL
    protected void txtSecondSupervisorOutside_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlMember5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlJointSupervisorSecond_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDownNotIN();
    }
    protected void ddlMember1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddlJointSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDownNotIN();
    }
    protected void ddlDRCChairman_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDownNotIN();
    }
    protected void ddlInstFac_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDownNotIN();
    }
    protected void ddlDRC_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDownNotIN();
    }
    protected void ddlMember3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Text = string.Empty;
        divCriteria.Visible = false;
        divpanel.Visible = false;
    }

    #endregion

    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked)
        {
            objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
            objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 AND COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }

        else
        {
            objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0", "ua_fullname");
            objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        string NOTIN = string.Empty;
        if (CheckBox2.Checked)
        {
            objCommon.FillDropDownList(ddlJointSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
            objCommon.FillDropDownList(ddlMember1, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }

        else
        {
            NotIn();
            NOTIN = Session[UANO].ToString();
            objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
            objCommon.FillDropDownList(ddlMember1, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            Session[UANO] = null;
        }
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        string NOTIN = string.Empty;
        if (CheckBox4.Checked)
        {
            objCommon.FillDropDownList(ddlInstFac, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
            objCommon.FillDropDownList(ddlMember2, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }

        else
        {
            NotIn();
            NOTIN = Session[UANO].ToString();
            objCommon.FillDropDownList(ddlInstFac, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
            objCommon.FillDropDownList(ddlMember2, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            Session[UANO] = null;
        }
    }
    protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
    {
        string NOTIN = string.Empty;
        if (CheckBox6.Checked)
        {
            objCommon.FillDropDownList(ddlJointSupervisorSecond, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
            objCommon.FillDropDownList(ddlMember5, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }

        else
        {
            NotIn();
            NOTIN = Session[UANO].ToString();
            objCommon.FillDropDownList(ddlJointSupervisorSecond, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
            objCommon.FillDropDownList(ddlMember5, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            Session[UANO] = null;
        }

    }
    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCommittee.SelectedIndex > 0)
        {
            if (CheckBox3.Checked)
            {
                // objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
                objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 AND COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            }

            else
            {
              //  objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO)", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND DESIGNATIONNO IN (1,3)", "ua_fullname");
                objCommon.FillDropDownList(ddlMember, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            }
            objCommon.FillDropDownList(ddlMember1, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            objCommon.FillDropDownList(ddlMember2, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            objCommon.FillDropDownList(ddlMember5, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            objCommon.FillDropDownList(ddlMember3, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }
        else
        {
            ddlCommittee.SelectedIndex = 0;

        }

    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        string NOTIN = string.Empty;
        if (CheckBox5.Checked)
        {
            objCommon.FillDropDownList(ddlDRC, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
            objCommon.FillDropDownList(ddlMember3, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=1 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
        }

        else
        {
            NotIn();
            NOTIN = Session[UANO].ToString();
            objCommon.FillDropDownList(ddlDRC, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
            objCommon.FillDropDownList(ddlMember3, "ACD_COMMITTEE_MAPPING cm inner join ACD_PHDCOMMITTEE_DESIGNATION PD ON(CM.DESIG_ID=PD.DESIG_ID)", "PD.DESIG_ID", "PD.DESIGNATION", "PD.EXTERNALSTATUS=0 and COMMITTEE_ID=" + ddlCommittee.SelectedValue, "PD.DESIG_ID");
            Session[UANO] = null;
        }
    }

    private void NotIn()
    {
        if (ddlSupervisor.SelectedIndex > 0)
        {
            if (CheckBox1.Checked)
            {
            }
            else
            {
                UANO += ddlSupervisor.SelectedValue + ",";
            }
        }
        if (ddlDGCSupervisor.SelectedIndex > 0)
        {
            if (CheckBox3.Checked)
            {
            }
            else
            {
                UANO += ddlDGCSupervisor.SelectedValue + ",";
            }
        }
        if (ddlJointSupervisor.SelectedIndex > 0)
        {
            if (CheckBox2.Checked)
            {
            }
            else
            {
                UANO += ddlJointSupervisor.SelectedValue + ",";
            }
        }
        if (ddlInstFac.SelectedIndex > 0)
        {
            if (CheckBox4.Checked)
            {
            }
            else
            {
                UANO += ddlInstFac.SelectedValue + ",";
            }
        }
        if (ddlJointSupervisorSecond.SelectedIndex > 0)
        {
            if (CheckBox6.Checked)
            {
            }
            else
            {
                UANO += ddlJointSupervisorSecond.SelectedValue + ",";
            }
        }
        if (ddlDRC.SelectedIndex > 0)
        {
            if (CheckBox5.Checked)
            {
            }
            else
            {
                UANO += ddlDRC.SelectedValue + ",";
            }
        }
        if (ddlDRCChairman.SelectedIndex > 0)
        {
            UANO += ddlDRCChairman.SelectedValue;
        }
        if (UANO == "")
        {
            UANO = "0";
        }
        Session[UANO] = UANO.TrimEnd(',');
    }

    private void FillDropDownNotIN()
    {
        string NOTIN = string.Empty;
        NotIn();
        NOTIN = Session[UANO].ToString();
        if (ddlJointSupervisor.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN +")", "ua_fullname");
        }
        if (ddlJointSupervisorSecond.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlJointSupervisorSecond, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
        }
        if (ddlInstFac.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlInstFac, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
        }
        if (ddlDRC.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlDRC, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
        }
        if (ddlDRCChairman.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "UA_NO", "UA_FULLNAME + ' - '+EmployeeId+ ' - '+ (CASE WHEN DESIGNATIONNO = 1 THEN '(S)'  WHEN DESIGNATIONNO =2 THEN '(D)' WHEN DESIGNATIONNO = 3 THEN '(SD)' END)", "UA_NO>0 AND UA_NO NOT IN (" + NOTIN + ")", "ua_fullname");
        }
        Session[UANO] = null;

    }
    #endregion

    #region submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        //if (ua_type == "3" && ua_dec == "1" || ua_type == "2" && ua_dec == "0")
        //{
            SubmitData();
        //}
        //else
        //{

        //}

        //if (ua_type == "3" || ua_type == "1")
        //{

           // SubmitData();
       // }

    }
    private void SubmitData()
    {
        try
        {

            //added by Vipul Tichakule on date 07-11-2023
            if (ddlSupervisor.SelectedValue == ddlJointSupervisor.SelectedValue || ddlSupervisor.SelectedValue == ddlJointSupervisorSecond.SelectedValue || ddlSupervisor.SelectedValue == ddlDRC.SelectedValue || ddlSupervisor.SelectedValue == ddlDRCChairman.SelectedValue)
            {
                objCommon.DisplayMessage("Multiple faculty with the same name are not allowed!!!", this.Page);
                return;
            }
            if (ddlJointSupervisor.SelectedValue == ddlSupervisor.SelectedValue || ddlJointSupervisor.SelectedValue == ddlJointSupervisorSecond.SelectedValue || ddlJointSupervisor.SelectedValue == ddlDRC.SelectedValue || ddlJointSupervisor.SelectedValue == ddlDRCChairman.SelectedValue)
            {
                objCommon.DisplayMessage("Multiple faculty with the same name are not allowed!!!", this.Page);
                return;
            }
            if (ddlJointSupervisorSecond.SelectedValue == ddlSupervisor.SelectedValue || ddlJointSupervisorSecond.SelectedValue == ddlJointSupervisor.SelectedValue || ddlJointSupervisorSecond.SelectedValue == ddlDRC.SelectedValue || ddlJointSupervisorSecond.SelectedValue == ddlDRCChairman.SelectedValue)
            {

                objCommon.DisplayMessage("Multiple faculty with the same name are not allowed!!!", this.Page);
                return;
            }
            if (ddlDRC.SelectedValue == ddlSupervisor.SelectedValue || ddlDRC.SelectedValue == ddlJointSupervisorSecond.SelectedValue || ddlDRC.SelectedValue == ddlJointSupervisor.SelectedValue || ddlDRC.SelectedValue == ddlDRCChairman.SelectedValue)
            {
                objCommon.DisplayMessage("Multiple faculty with the same name are not allowed!!!", this.Page);
                return;
            }
            if (ddlDRCChairman.SelectedValue == ddlSupervisor.SelectedValue || ddlDRCChairman.SelectedValue == ddlJointSupervisor.SelectedValue || ddlDRCChairman.SelectedValue == ddlJointSupervisorSecond.SelectedValue || ddlDRCChairman.SelectedValue == ddlDRC.SelectedValue)
            {
                objCommon.DisplayMessage("Multiple faculty with the same name are not allowed!!!", this.Page);
                return;
            }

            PhdController objPhdC = new PhdController();

            Phd objS = new Phd();
            objS.IdNo = Convert.ToInt32(Session["idno"]);
            objS.SuperRole = ddlSupervisorrole.SelectedValue;
            objS.Research = txtResearch.Text;
            int NOFSP = Convert.ToInt32(ddlNdgc.SelectedValue);
            int commiteeno = Convert.ToInt32(ddlCommittee.SelectedValue);
            objS.SupervisormemberNo = Convert.ToInt32(ddlMember.SelectedValue);
            objS.JoinsupervisormemberNo = Convert.ToInt32(ddlMember1.SelectedValue);
            objS.InstitutefacmemberNo = Convert.ToInt32(ddlMember2.SelectedValue);
            objS.Secondjoinsupervisormemberno = Convert.ToInt32(ddlMember5.SelectedValue);
            objS.DrcmemberNo = Convert.ToInt32(ddlMember3.SelectedValue);
            if (CheckBox1.Checked)
            {
                objS.SupervisorStatus = Convert.ToInt32(ddlSupervisor.SelectedValue);
                objS.SupervisorNo = 0;
            }
            else
            {

                objS.SupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
                objS.SupervisorStatus = 0;
            }
            if (CheckBox2.Checked)
            {
                objS.JoinsupervisorStatus = Convert.ToInt32(ddlJointSupervisor.SelectedValue);
                objS.JoinsupervisorNo = 0;
            }
            else
            {

                objS.JoinsupervisorNo = Convert.ToInt32(ddlJointSupervisor.SelectedValue);
                objS.JoinsupervisorStatus = 0;
            }
            if (CheckBox4.Checked)
            {
                objS.InstitutefacultyStatus = Convert.ToInt32(ddlInstFac.SelectedValue);
                objS.InstitutefacultyNo = 0;
            }
            else
            {

                objS.InstitutefacultyNo = Convert.ToInt32(ddlInstFac.SelectedValue);
                objS.InstitutefacultyStatus = 0;
            }
            if (CheckBox6.Checked)
            {
                objS.Secondjoinsupervisorstatus = Convert.ToInt32(ddlJointSupervisorSecond.SelectedValue);
                objS.Secondjoinsupervisorno = 0;
            }
            else
            {

                objS.Secondjoinsupervisorno = Convert.ToInt32(ddlJointSupervisorSecond.SelectedValue);
                objS.Secondjoinsupervisorstatus = 0;
            }
            if (CheckBox5.Checked)
            {
                objS.Drcstatus = Convert.ToInt32(ddlDRC.SelectedValue);
                objS.DrcNo = 0;
            }
            else
            {

                objS.DrcNo = Convert.ToInt32(ddlDRC.SelectedValue);
                objS.Drcstatus = 0;
            }
            objS.DrcChairNo = Convert.ToInt32(ddlDRCChairman.SelectedValue);

            string output = objPhdC.UpdateSupervisorPHDStudent(objS, NOFSP, commiteeno);
            if (output != "2")
            {
                objCommon.DisplayMessage("Student Information Insert Successfully!!", this.Page);
                this.ShowStudentDetails();
            }
            else
            {
                if (output != "1")
                {
                    objCommon.DisplayMessage("Student Information Update Successfully!!", this.Page);

                    this.ShowStudentDetails();
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SubmitData()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Approve and Reject By Dean

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        PhdController objPhdC = new PhdController();
        Phd objS = new Phd();
        //if (ViewState["usertype"].ToString() == "4")
        //{
                objS.IdNo = Convert.ToInt32(Session["idno"]);
             
                string output = objPhdC.ApprovedSupervisor(objS);
                if (output != "-99")
                {
                    objCommon.DisplayMessage("Supervisor Approve Successfully!!", this.Page);
                    ShowStudentDetails();
                    btnSubmit.Enabled = false;
                }          
        //}
        //else
        //{
        //    objCommon.DisplayMessage("You are not Authorized for Approve Supervisor!!", this.Page);
        //}
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        PhdController objPhdC = new PhdController();
        Phd objS = new Phd();
        //if (ViewState["usertype"].ToString() == "4" )
        //{
            if (txtRemark.Text != "")
            {
                objS.IdNo = Convert.ToInt32(Session["idno"]);
                string Remark = txtRemark.Text;
                string output = objPhdC.RejectSupervisor(objS, Remark);
                if (output != "-99")
                {
                    objCommon.DisplayMessage("Supervisor Rejected Successfully!!", this.Page);
                     ShowStudentDetails();
                    
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Insert Remark for Cancellation!!", this.Page);
            }
        //}
        //else
        //{
        //    objCommon.DisplayMessage("You are not Authorized for Reject Supervisor!!", this.Page);
        //}

    }
      #endregion 

    #region cancel

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion
  
}