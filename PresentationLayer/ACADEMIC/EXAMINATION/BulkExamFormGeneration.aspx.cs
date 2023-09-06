//======================================================================================
// PROJECT NAME  : UAIMS [ARKA JAIN UNIVERSITY]                                                          
// MODULE NAME   : ACADEMIC/EXAMINATION                                                             
// PAGE NAME     : BULK EXAM FORM GENERATION                                    
// CREATION DATE : 16-10-2019
// CREATED BY    : DEELIP KARE                                     
// MODIFIED DATE :                                                              
// MODIFIED DESC :                                                                  
//======================================================================================
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_EXAMINATION_BulkExamFormGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FetchDataController studCont = new FetchDataController();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();
    DemandModificationController objDem = new DemandModificationController();
    string bindListStatus = string.Empty;
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
                     // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //if (CheckActivity())
                    //{
                    //    divBody.Visible = true;
                    //}
                    //else
                    //{
                    //    divBody.Visible = false;
                    //}
                    populateDropDown();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                bindListStatus = "NoRender";
            }
            else
            {
                bindListStatus = "Render";
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
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
    private void populateDropDown()
    {


        string deptno = string.Empty;
        if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
            deptno = "0";
        else
            deptno = Session["userdeptno"].ToString();

        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");


        objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        //ddlSession.SelectedIndex = 1;
        ////mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        //ddlSession.Focus();
    }
    protected void BindListView(string str)
    {
        try
        {
            DataSet ds;
            // Get List of Student.....
            // ds = studCont.GetStudentListForRegistrationSlip(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));



            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            //ds = studCont.GetStudentDetailsForExamForm(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));

            //int type = Convert.ToInt32(rdlist.SelectedValue);
            int type = Convert.ToInt32(rdlist.SelectedValue);
            //if (rdlist.SelectedValue == "2") {  type = "0,1"; }
            ds = studCont.GetStudentDetailsForExamForm(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), type);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                btnGenerateExamForm.Enabled = true;
                lvStudentRecords.Visible = true;
               // dpPager.Visible = false ;
                chkprint.Enabled = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
                foreach (ListViewDataItem item in lvStudentRecords.Items)
                {
                    Label lblStatus = item.FindControl("lblStatus") as Label;
                    CheckBox chkBox = item.FindControl("chkreport") as CheckBox;
                    int lblGenerate = Convert.ToInt32(lblStatus.ToolTip);

                    if (lblGenerate > 0)
                    {
                        chkBox.Enabled = false;
                        chkBox.Checked = true;
                    }
                    lblGenerate = 0;
                }
            }
            else
            {
                if (str == "NoRender")
                {

                }
                else
                {
                    objCommon.DisplayMessage(updtime, "Record Not Found!!", this.Page);
                    lvStudentRecords.DataSource = null;
                    lvStudentRecords.DataBind();
                   // dpPager.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSem.SelectedIndex = -1;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
       // dpPager.Visible = false;
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = -1;
        ddlSem.SelectedIndex = -1;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        //dpPager.Visible = false;
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ddlBranch.SelectedValue + "AND DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO");
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.SelectedIndex = -1;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        //dpPager.Visible = false;
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER SE ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ddlScheme.SelectedValue, "SE.SEMESTERNO");
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        //dpPager.Visible = false;
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        //Bind the Student List....


        if (rdlist.SelectedValue == String.Empty)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>alert(\"Please select your option for radio button list.\");</script>", false);
            objCommon.DisplayMessage(this, "Please select at least one Type", this.Page);
            return;
        }
        this.BindListView("render");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlColg.SelectedIndex = -1;
        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSem.SelectedIndex = -1;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();



         if (ddlSession.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            ddlSem.Focus();
            

        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            

        }
        ddlSem.SelectedIndex = 0;
       

    }




     private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            demandCriteria.SessionNo = Convert.ToInt16(ddlSession.SelectedValue);
            demandCriteria.ReceiptTypeCode = "EF";
            demandCriteria.BranchNo = (ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["branchno"]) : 0);
            demandCriteria.SemesterNo = (ddlSem.SelectedIndex > 0 ? Int32.Parse(ddlSem.SelectedValue) : 0);
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
    protected void btnGenerateExamForm_Click(object sender, EventArgs e)
    {
        //bool IsTrue = false;
        //int Chekcount = 0;
        //int IsStandardFeescount = 0;
        string PayTypeNames = string.Empty;
        int selectSemesterNo = Int32.Parse(ddlSem.SelectedValue);
        FeeDemand demandCriteria = this.GetDemandCriteria();
        string ids = GetStudentIDs();

        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
        objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
        objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
        objSR.IPADDRESS = Session["ipAddress"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        objSR.COLLEGE_CODE = Session["colcode"].ToString();

        //foreach (ListViewDataItem lvItem in lvStudentRecords.Items)
        //{
            //CheckBox chkBox = lvItem.FindControl("chkReport") as CheckBox;

            //HiddenField hdSemno = lvItem.FindControl("hdfSem") as HiddenField;
            //HiddenField hdPtypeno = lvItem.FindControl("hdfPtype") as HiddenField;
            //HiddenField hdBatch = lvItem.FindControl("hdfAdmbatch") as HiddenField;
            //Label lblPayType = lvItem.FindControl("lblPayType") as Label;

            //if (chkBox.Checked == true && chkBox.Enabled == true)
            //{



                //int ret = objSReg.AddExamRegiSubjects_Bulk(objSR, ids);
                //if (ret == 1)
                //{ 
                    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    int type = Convert.ToInt32(rdlist.SelectedValue);
                    int sem = Convert.ToInt32(ddlSem.SelectedValue);
                    int uano = Convert.ToInt32(Session["userno"].ToString());
                    int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                    string ip = Session["ipAddress"].ToString();
                    string response = objDem.CreateDemandForExamFeesBulk(ids, sessionno, sem, type, uano, schemeno, ip);
                    if (response == "1")
                    {

                        int ret = objSReg.AddExamRegiSubjects_Bulk(objSR, ids);

                        if (ret == 1)
                        {

                            this.BindListView("render");
                            ShowReport(ids, "BulkExamForm", "rptBulkExamination_Form.rpt");
                            objCommon.DisplayMessage("Provisional Exam Registration Done and Exam Fees Demand Created Successfully.", this.Page);
                        }
                        else 
                        {
                           // objCommon.DisplayMessage("Error! in saving record.", this.Page);
                        
                        }
                    }
                    else
                    {
                        //objCommon.DisplayMessage("Error!.", this.Page);
                    }
                //}
                //else
                //{
                //    objCommon.DisplayMessage("Error! in saving record.", this.Page);
                //}




            //}
            //else
            //{
            //   // objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            //}
        //}


            //if (chkBox.Checked == true && chkBox.Enabled == true)
            //{
            //    IsTrue = true;
            //    Chekcount++;
            //    int standardFeesCount = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*) AS CNT",
            //    "DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
            //    " AND BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
            //    " AND BATCHNO=" + Convert.ToInt32(hdBatch.Value) +
            //     " AND PAYTYPENO=" + Convert.ToInt32(hdPtypeno.Value) +
            //    " AND RECIEPT_CODE='EF'"));
            //    if (standardFeesCount > 0)
            //    {
            //        IsStandardFeescount++;
            //    }
            //    else
            //    {
            //        if (PayTypeNames.Contains(lblPayType.Text))
            //        {
            //        }
            //        else
            //        {
            //            if (PayTypeNames == string.Empty)
            //            {
            //                PayTypeNames += lblPayType.Text;
            //            }
            //            else
            //            {
            //                PayTypeNames = PayTypeNames + "," + lblPayType.Text;
            //            }
            //        }
            //    }
            //}

        //}
        //if (IsTrue)
        //{
        //    if (Chekcount != IsStandardFeescount)
        //    {
        //    //    objCommon.DisplayMessage("Standard Fees Defination not found to get Exam Fees amount.Please Define Standard Fees For Payment Type " + PayTypeNames, this.Page);
        //    //    return;
        //    }
        //    else
        //    {
        //        //int ret = objSReg.AddExamRegiSubjects_Bulk(objSR, ids);
        //        //if (ret == 1)
        //        //{ //string studentIDs,int sessionno,int sem, int type,int uano,int schemeno
        //        //    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //        //    int type = Convert.ToInt32(rdlist.SelectedValue);
        //        //    int sem =Convert.ToInt32(ddlSem.SelectedValue);
        //        //    int uano =Convert.ToInt32(Session["userno"].ToString());
        //        //    int schemeno=Convert.ToInt32(ViewState["schemeno"]);
        //        //    string  ip= Session["ipAddress"].ToString();
        //        //    string response = objDem.CreateDemandForExamFeesBulk(ids, sessionno, sem,type,uano,schemeno,ip);
        //        //    if (response == "1")
        //        //    {
        //        //        this.BindListView("render");
        //        //        ShowReport(ids, "BulkExamForm", "rptBulkExamination_Form.rpt");
        //        //        objCommon.DisplayMessage("Provisional Exam Registration Done and Exam Fees Demand Created Successfully.", this.Page);
        //        //    }
        //        //    else
        //        //    {
        //        //        objCommon.DisplayMessage("Error!.", this.Page);
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    objCommon.DisplayMessage("Error! in saving record.", this.Page);
        //        //}
        //    }
        //}
        //else
        //{
        //    //objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
        //}
    }

    private void ShowReport(string idnos, string reportTitle, string rptFileName)
    {
        int sessionno = 0;
       int type = Convert.ToInt32(rdlist.SelectedValue);
        sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //    int idno =Convert.ToInt32( hidIdNo.Text);

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idnos + ",@P_SESSIONNO=" + sessionno + ",@P_SEM=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_TYPE=" + type ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked && (item.FindControl("chkReport") as CheckBox).Enabled == true)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lvStudentRecords.DataSource = null;
    //    lvStudentRecords.DataBind();
    //}

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //this.BindListView(bindListStatus);
    }
    protected void chkprint_CheckedChanged(object sender, EventArgs e)
    {
        if (chkprint.Checked)
        {
            btnPrint.Enabled = true;
            btnGenerateExamForm.Enabled = false;
            lvStudentRecords.Visible = true;
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                Label lblStatus = item.FindControl("lblStatus") as Label;
                CheckBox chkBox = item.FindControl("chkreport") as CheckBox;
                int lblGenerate = Convert.ToInt32(lblStatus.ToolTip);

                if (lblGenerate > 0)
                {
                    chkBox.Enabled = true;
                    chkBox.Checked = false;
                }
                else
                {
                    chkBox.Enabled = false;
                }
                lblGenerate = 0;
            }
        }
        else
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                CheckBox chkreport = item.FindControl("chkreport") as CheckBox;
                Label lblStatus = item.FindControl("lblStatus") as Label;
                if (lblStatus.Text == "Generated")
                {
                    chkreport.Enabled = false;
                    chkreport.Checked = true;
                }
                else
                {
                    chkreport.Enabled = true;
                }
            }
            btnPrint.Enabled = false;
            btnGenerateExamForm.Enabled = true;
        }
    }

    private string GetStudentIDsPrint()
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string ids = GetStudentIDsPrint();
        ShowReport(ids, "BulkExamForm", "rptBulkExamination_Form.rpt");
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {


        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {
            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");


                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
                //ddlSession.SelectedIndex = 1;
               
                ddlSession.Focus();

             

            }


             lvStudentRecords.DataSource = null;
             lvStudentRecords.DataBind();
            ddlSem.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
        }
        ddlSem.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        



    }
    protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvStudentRecords.DataSource = null;
        //lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;
        //if (rdlist.SelectedValue == "2")
        //{
        //    foreach (ListViewDataItem item in lvStudentRecords.Items)
        //    {
        //        CheckBox chkreport = item.FindControl("chkreport") as CheckBox;
        //        Label lblStatus = item.FindControl("lblStatus") as Label;
                
        //            chkreport.Enabled = false;
        //            chkreport.Checked = true;
               
        //    }
        //    //btnPrint.Enabled = false;
        //    //btnGenerateExamForm.Enabled = true;
        
        //}
    }
}

  
   