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
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_AttendanceConfig : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();

    #region PageLoad

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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            //Populate the Drop Down Lists
            PopulateDropDown();
            BindListView();
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 29/01/2022
            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 29/01/2022
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendanceConfig.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendanceConfig.aspx");
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString() == "1" || Session["dec"].ToString()== "1")
            {
                objCommon.FillDropDownList(ddlSchemeType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
                objCommon.FillListBox(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 and ActiveStatus=1", "SEMESTERNO");
                PopulateDropDownList();

                if (Session["usertype"].ToString() != "1")
                {
                    // divDepartment.Visible = true;
                    rfvDepartment.Visible = true;
                    PopulateDropDownList();
                }
                else
                {
                    divDepartment.Visible = false;
                    rfvDepartment.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void PopulateDropDownList()
    {
        try
        {
            AcademinDashboardController objADEController = new AcademinDashboardController();
            DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
            ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                // ddlSession.SelectedValue = "";
                ddlSession.DataSource = ds;
                ddlSession.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSession.DataTextField = ds.Tables[0].Columns[4].ToString();
                ddlSession.DataBind();
                //ddlSession.SelectedIndex = 0;
            }
            //ddlSession.Items.Clear();
            //ddlSession.Items.Add("Please Select");
            //ddlSession.SelectedItem.Value = "0";




            //if (ds.Tables.Count > 0)
            //    {
            //    if (ds.Tables[0].Rows.Count > 0)
            //        {
            //        ddlDegree.DataTextField = "DEGREENAME";
            //        ddlDegree.DataValueField = "DEGREENO";
            //        ddlDegree.ToolTip = "DEGREENO";
            //        ddlDegree.DataSource = ds.Tables[0];
            //        ddlDegree.DataBind();
            //        }

            //    }

            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC"); //ISNULL(FLOCK,0)=1
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objAttC.GetAllAttendanceConfig(Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["usertype"]), Session["college_nos"].ToString());
            lvAttConfig.DataSource = ds;
            lvAttConfig.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAttConfig);//Set label -
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    DataSet ds = objAttC.GetSemesterDurationwise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));

        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Add("Please Select");
        //    ddlSemester.SelectedItem.Value = "0";
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddlSemester.DataSource = ds;
        //        ddlSemester.DataValueField = ds.Tables[0].Columns[0].ToString();
        //        ddlSemester.DataTextField = ds.Tables[0].Columns[1].ToString();
        //        ddlSemester.DataBind();
        //        ddlSemester.SelectedIndex = 0;
        //    }
        //}
        //catch { }
    }
    #endregion

    #region Transaction

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int srno = 0;
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
                {
                    objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                    return;
                }
                else
                {
                    //Set all properties


                    string Sessionnos = "";
                    foreach (ListItem items in ddlSession.Items)
                    {
                        if (items.Selected == true)
                        {
                            //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            Sessionnos += items.Value + ',';
                        }
                    }
                    Sessionnos = Sessionnos.Remove(Sessionnos.Length - 1);
                    ViewState["SessionNos"] = Sessionnos;
                    string Degreenos = "";
                    foreach (ListItem items in ddlDegree.Items)
                    {
                        if (items.Selected == true)
                        {
                            //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            Degreenos += items.Value + ',';
                        }
                    }
                    Degreenos = Degreenos.Remove(Degreenos.Length - 1);

                    string Semesternnos = "";
                    foreach (ListItem items in ddlSemester.Items)
                    {
                        if (items.Selected == true)
                        {
                            //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            Semesternnos += items.Value + ',';
                        }
                    }
                    Semesternnos = Semesternnos.Remove(Semesternnos.Length - 1);



                    AcdAttendanceController acdatt = new AcdAttendanceController();
                    DataSet dsCollegeids = acdatt.getselectedcollegewisecollegeid(ViewState["SessionNos"].ToString());

                    string CollegeIds = string.Empty;
                    //DataSet dscollege = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT(COLLEGE_ID)", "", "SESSIONNO IN (" + ViewState["SessionNos"].ToString() + ")", "COLLEGE_ID");
                    if (dsCollegeids.Tables.Count > 0)
                    {
                        if (dsCollegeids.Tables[0].Rows.Count > 0)
                        {
                            CollegeIds = dsCollegeids.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        }
                        //dsCollegeids.Tables[0].Columns[1].ToString();

                    }


                    //objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    //objAttModel.Degree_No = Convert.ToInt32(ddlDegree.SelectedValue);
                    //objAttModel.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                    int _schemeType = Convert.ToInt32(ddlSchemeType.SelectedValue);
                    objAttModel.AttendanceStartDate = Convert.ToDateTime(txtStartDate.Text);
                    objAttModel.AttendanceEndDate = Convert.ToDateTime(txtEndDate.Text);
                    objAttModel.AttendanceLockDay = Convert.ToInt32(txtAttLockDay.Text);
                    //objAttModel.AttendanceLockHrs = txtAttLockHrs.Text;
                    //Added by Rishabh on 29/10/2021
                    if (hfdSms.Value == "true")
                    {
                        objAttModel.SMSFacility = true;
                    }
                    else
                    {
                        objAttModel.SMSFacility = false;
                    }

                    if (hfdEmail.Value == "true")
                    {
                        objAttModel.EmailFacility = true;
                    }
                    else
                    {
                        objAttModel.EmailFacility = false;
                    }

                    if (hfdCourse.Value == "true")
                    {
                        objAttModel.CRegStatus = true;
                    }
                    else
                    {
                        objAttModel.CRegStatus = false;
                    }

                    if (hfdTeaching.Value == "true")
                    {
                        objAttModel.TeachingPlan = true;
                    }
                    else
                    {
                        objAttModel.TeachingPlan = false;
                    }

                    if (hfdActive.Value == "true")
                    {
                        objAttModel.ActiveStatus = true;
                    }
                    else
                    {
                        objAttModel.ActiveStatus = false;
                    }
                    //End
                    objAttModel.College_code = Session["colcode"].ToString();

                    if (ViewState["srno"] != null)
                        srno = Convert.ToInt32(ViewState["srno"]);

                    //Check for add or edit
                    if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                    {
                        //Edit and Update
                        CustomStatus cs = (CustomStatus)objAttC.UpdateAttConfiguration(objAttModel, srno, Sessionnos, CollegeIds, Degreenos, _schemeType, Semesternnos, Convert.ToInt32(Session["OrgId"]));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ClearControls();
                            objCommon.DisplayMessage(this, "Configuration Updated Successfully", this.Page);
                            this.BindListView();
                        }
                    }
                    else
                    {
                        //Add New
                        CustomStatus cs = (CustomStatus)objAttC.AddAttendanceConfig(objAttModel, Sessionnos, CollegeIds, Degreenos, _schemeType, Semesternnos, Convert.ToInt32(Session["OrgId"]));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ClearControls();
                            objCommon.DisplayMessage(this, "Configuration Added Successfully", this.Page);
                            this.BindListView();
                        }
                        else if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this, "Configuration Already Exists !!", this.Page);
                            ClearControls();
                        }
                        else if (cs.Equals(CustomStatus.TransactionFailed))
                        {
                            objCommon.DisplayMessage(this, "Transaction Failed", this.Page);
                            ClearControls();
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Enter Start and End Date", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int srno = int.Parse(btnEdit.CommandArgument);
            ViewState["srno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";

            this.ShowDetails(srno);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int srno)
    {
        try
        {
            ddlSession.Items.Clear();
            ddlDegree.Items.Clear();
            char delimiterChars = ',';
            char delimiter = ',';
            //SessionController objSS = new SessionController();
            SqlDataReader dr = objAttC.GetSingleConfiguration(srno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    //ddlSchoolInstitute.SelectedValue = dr["COLLEGE_ID"] == DBNull.Value ? "0" : dr["COLLEGE_ID"].ToString();



                    //objCommon.FillListBox(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue, "D.DEGREENO");
                    //objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlSchoolInstitute.SelectedValue), "SESSIONNO DESC");



                    string ddlSessionno = dr["SESSIONNO"] == DBNull.Value ? "0" : dr["SESSIONNO"].ToString();

                    AcademinDashboardController objADEController = new AcademinDashboardController();
                    DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
                    //   ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        // ddlSession.SelectedValue = "";
                        ddlSession.DataSource = ds;
                        ddlSession.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlSession.DataTextField = ds.Tables[0].Columns[4].ToString();
                        ddlSession.DataBind();
                        // ddlSession.SelectedIndex = 0;
                    }


                    string[] utype = ddlSessionno.Split(delimiterChars);
                    string[] DeptTypes = ddlSessionno.Split(delimiter);






                    for (int j = 0; j < utype.Length; j++)
                    {
                        for (int i = 0; i < ddlSession.Items.Count; i++)
                        {
                            if (utype[j] == ddlSession.Items[i].Value)
                            {
                                ddlSession.Items[i].Selected = true;
                            }
                        }
                    }




                    string CollegeID = objCommon.LookUp("ACD_SESSION_MASTER ", "COLLEGE_ID", "SESSIONNO=" + ddlSessionno);



                    string degreeno = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();




                    objCommon.FillListBox(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "COLLEGE_ID=" + CollegeID, "D.DEGREENO");
                    string[] dtype = degreeno.Split(delimiterChars);
                    string[] degreetypes = degreeno.Split(delimiter);

                    for (int k = 0; k < dtype.Length; k++)
                    {
                        for (int m = 0; m < ddlDegree.Items.Count; m++)
                        {
                            if (dtype[k] == ddlDegree.Items[m].Value)
                            {
                                ddlDegree.Items[m].Selected = true;
                            }
                        }
                    }

                    // ddlSession.SelectedValue = dr["SESSIONNO"] == DBNull.Value ? "0" : dr["SESSIONNO"].ToString();
                    // ddlDegree.SelectedValue = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();



                    ddlSemester.SelectedValue = dr["SEMESTERNO"] == DBNull.Value ? "0" : dr["SEMESTERNO"].ToString();
                    ddlSchemeType.SelectedValue = dr["SCHEMETYPE"] == DBNull.Value ? "0" : dr["SCHEMETYPE"].ToString();
                    txtStartDate.Text = dr["START_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["START_DATE"].ToString()).ToString("dd/MM/yyyy");
                    txtEndDate.Text = dr["END_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["END_DATE"].ToString()).ToString("dd/MM/yyyy");
                    txtAttLockDay.Text = dr["LOCK_ATT_DAYS"] == null ? string.Empty : dr["LOCK_ATT_DAYS"].ToString();

                    //txtAttLockHrs.Text = dr["LOCK_ATT_HOURS"] == null ? string.Empty : dr["LOCK_ATT_HOURS"].ToString();
                    ViewState["sms"] = dr["SMS_FACILITY"].ToString();
                    if (dr["SMS_FACILITY"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatSms(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src2", "SetStatSms(false);", true);
                        //hfdSms.Value = "0";
                    }

                    if (dr["EMAIL_FACILITY"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src3", "SetStatEmail(true);", true);
                        //ScriptManager.RegisterClientScriptBlock(updpnl, updpnl.GetType(), "script3", "SetStatEmail(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src4", "SetStatEmail(false);", true);
                        // ScriptManager.RegisterClientScriptBlock(updpnl, updpnl.GetType(), "script4", "SetStatEmail(false);", true);
                    }

                    if (dr["CREG_STATUS"].ToString() == "Before")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src5", "SetStatCourse(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src5", "SetStatCourse(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "src6", "SetStatCourse(false);", true);
                        // ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src6", "SetStatCourse(false);", true);
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Src", "SetStatCourse()", true);
                    }

                    if (dr["TEACHING_PLAN"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "SetStatTeaching(true);", true);
                        // ScriptManager.RegisterClientScriptBlock(updpnl, this.GetType(), "script7", "SetStatTeaching(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src8", "SetStatTeaching(false);", true);
                        // ScriptManager.RegisterClientScriptBlock(updpnl, this.GetType(), "script8", "SetStatTeaching(false);", true);
                    }

                    if (dr["ACTIVE"].ToString() == "Active")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.updpnl, GetType(), "script", "SetStatActive(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9", "SetStatActive(true);", true);
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.updpnl, GetType(), "script", "SetStatActive(false);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript10", "SetStatActive(false);", true);
                    }

                }
            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ClearControls()
    {
        ddlSession.ClearSelection();
        ddlDegree.ClearSelection();
        ddlSemester.ClearSelection();
        ddlSchemeType.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtAttLockDay.Text = string.Empty;
        //txtAttLockHrs.Text = string.Empty;
        //rdoSMSNo.Checked = true;
        //rdoEmailNo.Checked = true;
        //rblCRegAfter.Checked = true;
        ViewState["action"] = null;
        ddlSchoolInstitute.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clgID = ViewState["college_id"].ToString();
        string clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
        if (ddlSchoolInstitute.SelectedIndex > 0)
        {
            ddlDepartment.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;

            //objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID="+Convert.ToInt32(ddlSchoolInstitute.SelectedValue), "SESSIONNO DESC");
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "ACTIVESTATUS=1 AND DEPTNO>0 and DEPTNO IN( " + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
            }
            else
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.ACTIVESTATUS=1 AND D.DEPTNO>0 and COLLEGE_ID=" + clgID + "", "DEPTNAME ASC");
                objCommon.FillListBox(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "D.ACTIVESTATUS=1 AND COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue, "D.DEGREENO");
            }
        }
        else
        {
            ddlDepartment.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;

            ddlSemester.SelectedIndex = 0;
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        int Deptno = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        if (ddlDepartment.SelectedIndex > 0)
        {
            //ddlDegree.SelectedIndex = 0;
            //ddlSemester.SelectedIndex = 0;
            objCommon.FillListBox(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.ACTIVESTATUS=1 AND D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["CollegeId"]), "D.DEGREENO");
        }
        else
        {
            //ddlDegree.SelectedIndex = 0;
            //ddlSemester.SelectedIndex = 0;
        }
        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
    }


    #endregion
    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string clgID = ddlSession.SelectedIndex > 0 ? ddlSession.SelectedValue : "0";
    //    //if (ddlSession.SelectedIndex > 0)
    //    {
    //        //ddlDepartment.SelectedIndex = 0;
    //        //ddlDegree.SelectedIndex = 0;
    //        //ddlSemester.SelectedIndex = 0;

    //        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlSchoolInstitute.SelectedValue), "SESSIONNO DESC");
    //        if (Session["usertype"].ToString() != "1")
    //        {
    //            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "ACTIVESTATUS=1 AND DEPTNO>0 and DEPTNO IN( " + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
    //        }
    //        else
    //        {
    //            //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.ACTIVESTATUS=1 AND D.DEPTNO>0 and COLLEGE_ID=" + clgID + "", "DEPTNAME ASC");




    //        string Sessionn = ddlSession.SelectedValue;
    //        string[] subs = Sessionn.Split(',');
    //        ViewState["SessionNos"] = Convert.ToInt32(subs[0]);
    //        ViewState["CollegeIds"] = Convert.ToInt32(subs[1]);

    //        objCommon.FillListBox(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "D.ACTIVESTATUS=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["CollegeIds"]), "D.DEGREENO");
    //        }
    //    }
    //    //else
    //    //{
    //    //    ddlDepartment.SelectedIndex = 0;
    //    //    ddlDegree.SelectedIndex = 0;

    //    //    ddlSemester.SelectedIndex = 0;
    //    //}
    //}

    protected void ddlSession_SelectedIndexChanged1(object sender, EventArgs e)
    {

        //ddlDegree.ClearSelection();
        ddlDegree.Items.Clear();

        string Sessionn = "";


        foreach (ListItem items in ddlSession.Items)
        {
            if (items.Selected == true)
            {
                //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                Sessionn += items.Value + ',';
            }
        }
        if (Sessionn != "")
        {
            Sessionn = Sessionn.Remove(Sessionn.Length - 1);
            ViewState["SessionNos"] = Sessionn;
        }


        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillListBox(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.ACTIVESTATUS=1 AND D.DEGREENO>0 AND  COLLEGE_ID=" + Convert.ToInt32(ViewState["CollegeId"]), "D.DEGREENO");
            // objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT AD INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (AD.DEPTNO = CDB.DEPTNO) INNER JOIN ACD_SESSION_MASTER SM ON(SM.COLLEGE_ID = CDB.COLLEGE_ID)", "DISTINCT AD.DEPTNO", "DEPTNAME", "AD.ACTIVESTATUS=1 AND AD.DEPTNO>0 and SESSIONNO IN( " + ViewState["SessionNos"].ToString() + ")", "DEPTNAME ASC");
            ddlDegree.Focus();
        }
        else
        {
            AcdAttendanceController acdatt = new AcdAttendanceController();
            DataSet dsCollegeids = acdatt.getselectedcollegewisecollegeid(ViewState["SessionNos"].ToString());
            string collegeidnos = string.Empty;
            //DataSet dscollege = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT(COLLEGE_ID)", "", "SESSIONNO IN (" + ViewState["SessionNos"].ToString() + ")", "COLLEGE_ID");
            if (dsCollegeids.Tables.Count > 0)
            {
                if (dsCollegeids.Tables[0].Rows.Count > 0)
                {
                    collegeidnos = dsCollegeids.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                }
                //dsCollegeids.Tables[0].Columns[1].ToString();

            }

            ddlDegree.Items.Clear();
            DataSet ds = objCommon.FillDropDown("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "COLLEGE_ID IN (" + collegeidnos + ")", "D.DEGREENO");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.ToolTip = "DEGREENO";
                    ddlDegree.DataSource = ds.Tables[0];
                    ddlDegree.DataBind();
                    // ddlDegree.SelectedIndex = 0;
                }

            }

            else
            {
                ViewState["SessionNos"] = 0;
            }
        }
        if (Sessionn == "")
        {
            ddlDegree.Items.Clear();
        }
    }

}