
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Data.SqlClient;

public partial class ACADEMIC_BulkOD : System.Web.UI.Page
{
    //Initialize controllers and variables
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int idno = 0;
    int hno = 0;
    int rhno = 0;
    int RegNo = 0;
    

    #region PageLoad
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //page_load
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
               // this.CheckPageAuthorization();
                Blob_Storage();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //populate dropdown
                this.PopulateDropDown();
                this.FillDropdown();
                //this.BindListView();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


                buttonsfunctionality();

            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 30/01/2022
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 30/01/2022

        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }



    public void buttonsfunctionality()
    {
        //to show the buttons according to user type
        //3 - For Faculty  &   8- for HOD
        if (Session["usertype"].ToString() == "3")
        {
            btnSubmit.Enabled = false;
            btnSubmit.Visible = true;

            btnODApprove.Visible = false;
            btnODReject.Visible = false;
            btnODCancel.Visible = false;
        }
        if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "1")
        {
            btnSubmit.Enabled = false;
            btnSubmit.Visible = false;

            btnODApprove.Visible = false;
            btnODReject.Visible = false;
            btnODCancel.Visible = false;
        }
    }
    private void Blob_Storage()
    {

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updODReport, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updODReport, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
    }
    //to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
        }
    }

    //function to load dropdownlist
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (B.BRANCHNO=CDB.BRANCHNO)", "DISTINCT B.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ViewState["degreeno"]), "BRANCHNO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            AcademinDashboardController objADEController = new AcademinDashboardController();
            DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
            ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSession1.DataSource = ds;
                ddlSession1.DataValueField = "SESSIONNO";
                ddlSession1.DataTextField = "COLLEGE_SESSION";
                ddlSession1.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion PageLoad

    #region dropdown
    //load degree and clear all fields
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 ", "D.DEGREENO");
        }
        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "SM.SEMESTERNO");

        ddlSemester.Focus();
        //pnlStudents.Visible = false;
        lvStudent.Visible = false;
        ddlDegree.SelectedIndex = 0;
        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        chkSlots.Items.Clear();
        chkSlots.Items.Clear();
        ddlOdType.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lblSlots1.Visible = false;
        lblSlots2.Visible = false;
        lblSlotCompulsary.Visible = false;
        chkCheckAll.Visible = false;

        buttonsfunctionality();
    }

    //load branch and clear all fields
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uano = Session["userno"].ToString();
        string uatype = Session["usertype"].ToString();
        string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]), "A.LONGNAME");

            //  DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]), "A.LONGNAME");
            string BranchNos = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
            }
            DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            //on faculty login to get only those dept which is related to logged in faculty
            objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
            ddlBranch.Focus();


            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            // ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));


            txtEventDetail.Text = string.Empty;
            ddlLeaveName.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            chkSlots.Items.Clear();
            chkSlots.Items.Clear();
            chkCheckAll.Enabled = false;
            ddlOdType.SelectedIndex = 0;
            //pnlStudents.Visible = false;
            lvStudent.Visible = false;
            ddlBranch.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lblSlots1.Visible = false;
            lblSlots2.Visible = false;
            lblSlotCompulsary.Visible = false;
            chkCheckAll.Visible = false;

            buttonsfunctionality();

        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            //ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }

    }

    //load scheme and clear all fields
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND BRANCHNO = " + Convert.ToInt32(ViewState["branchno"]), "SCHEMENO");
            ddlScheme.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            //  ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            // ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }


        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;

        chkSlots.Items.Clear();
        chkSlots.Items.Clear();
        chkCheckAll.Enabled = false;
        ddlOdType.SelectedIndex = 0;
        //pnlStudents.Visible = false;
        lvStudent.Visible = false;
        ddlScheme.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lblSlots1.Visible = false;
        lblSlots2.Visible = false;
        lblSlotCompulsary.Visible = false;
        chkCheckAll.Visible = false;

        buttonsfunctionality();

    }


    //load semester and clear all fields
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                ddlSemester.Items.Clear();
                ddlSemester.Focus();
                // ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                // ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }

            txtEventDetail.Text = string.Empty;
            ddlLeaveName.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            chkSlots.Items.Clear();
            chkSlots.Items.Clear();
            chkCheckAll.Enabled = false;
            ddlOdType.SelectedIndex = 0;
            //pnlStudents.Visible = false;
            lvStudent.Visible = false;
            ddlSection.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lblSlots1.Visible = false;
            lblSlots2.Visible = false;
            lblSlotCompulsary.Visible = false;
            chkCheckAll.Visible = false;

            buttonsfunctionality();

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //load section and clear all fields
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            //if (ddlBranch.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
            //{
            //    objCommon.DisplayMessage("Please Select Branch/Scheme", this.Page);
            //    return;
            //}
            //else
            //{
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT ST INNER JOIN  ACD_SECTION  S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO>0 AND ST.Schemeno = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ST.Semesterno= " + ddlSemester.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
            //}
            ddlSection.Focus();
            txtEventDetail.Text = string.Empty;
            ddlLeaveName.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            chkSlots.Items.Clear();
            chkSlots.Items.Clear();
            chkCheckAll.Enabled = false;
            ddlOdType.SelectedIndex = 0;
            //pnlStudents.Visible = false;
            lvStudent.Visible = false;
            ddlSection.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lblSlots1.Visible = false;
            lblSlots2.Visible = false;
            lblSlotCompulsary.Visible = false;
            chkCheckAll.Visible = false;

            buttonsfunctionality();

        }
    }

    //load OD TYPE and Leave Type and clear all fields
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlOdType, "ACD_ODTYPE", "ODID", "OD_NAME", "ODID>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "ODID");
        objCommon.FillDropDownList(ddlLeaveName, "acd_specialleavetype", "specialleavetypeno", "specialleavetype", "specialleavetypeno>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "specialleavetypeno");

        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;

        chkSlots.Items.Clear();
        chkSlots.Items.Clear();
        chkCheckAll.Enabled = false;
        ddlOdType.SelectedIndex = 0;
        //pnlStudents.Visible = false;
        lvStudent.Visible = false;

        // ddlSection.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lblSlots1.Visible = false;
        lblSlots2.Visible = false;
        lblSlotCompulsary.Visible = false;
        chkCheckAll.Visible = false;

        buttonsfunctionality();
        ddlOdType.Focus();
    }



    //Get date according to Special and Normal OD
    protected void ddlOdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOdType.SelectedValue == "1")//Normal OD
        {
            txtToDate.Enabled = false;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkSlots.Items.Clear();
        }
        else //Special  OD
        {
            txtToDate.Enabled = true;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkSlots.Items.Clear();
        }

        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        chkSlots.Items.Clear();
        chkSlots.Items.Clear();
        chkCheckAll.Enabled = false;
        //pnlStudents.Visible = false;      
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lblSlots1.Visible = false;
        lblSlots2.Visible = false;
        lblSlotCompulsary.Visible = false;
        chkCheckAll.Visible = false;
        buttonsfunctionality();
        txtFromDate.Focus();
    }

    #endregion dowpdown

    #region User Defined Methods
    //to load listview and get students list
    private void BindListViewWithOD()
    {
        try
        {
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                DataSet ds = null;
                //for hod login and admin login
                if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "1")
                {
                    //to get all students + all entries which are already applied leave with their details
                    ds = objAttC.GetAppliedStudentsForOD(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]),
                        Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),
                        Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                }
                //for faculty login
                else if (Session["usertype"].ToString() == "3")
                {
                    //to get all students + all entries which are already applied leave with their details
                    ds = objAttC.GetStudentsWithODEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]),
                        Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),
                        Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(Session["userno"].ToString()));
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        //pnlStudents.Visible = true;
                        lvStudent.Visible = true;
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
                        //buttonsfunctionality();
                        btnSubmit.Enabled = true;
                        btnSubmit.Visible = true;
                        btnODApprove.Visible = true;
                        btnODReject.Visible = true;
                        btnODCancel.Visible = true;

                        // hftot.Value = ds.Tables[0].Rows.Count.ToString();

                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                            String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;
                            Label lblStatus = item.FindControl("lblStatus") as Label;
                            Label lblRollno = item.FindControl("lblRollno") as Label;
                            HiddenField hfCountOfIDNO = item.FindControl("hfCountOfIDNO") as HiddenField;


                            if (lblStatus.Text == "PENDING")
                            {
                                chkBox.Checked = true;

                                //to show the buttons if list containing pending leaves
                                //  buttonsfunctionality();



                            }


                            if (lblStatus.Text == "APPROVED")
                            {
                                chkBox.Checked = true;
                                chkBox.Enabled = false;
                            }

                            if (lblStatus.Text == "REJECTED")
                            {
                                chkBox.Checked = true;
                                chkBox.Enabled = false;
                            }


                            if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "1")
                            {
                                btnSubmit.Enabled = true;
                                btnSubmit.Visible = true;

                                btnODApprove.Visible = true;
                                btnODReject.Visible = true;
                                btnODCancel.Visible = true;
                                if ((hfCountOfIDNO.Value) != "")
                                {
                                    if (Convert.ToInt32(hfCountOfIDNO.Value) >= 63)
                                    {
                                        lblRollno.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }

                            //to show the buttons
                            if (Session["usertype"].ToString() == "3")
                            {
                                btnSubmit.Enabled = true;
                                btnSubmit.Visible = true;

                                btnODApprove.Visible = true;
                                btnODReject.Visible = true;
                                btnODCancel.Visible = true;

                                if ((hfCountOfIDNO.Value) != "")
                                {
                                    if (Convert.ToInt32(hfCountOfIDNO.Value) >= 63)
                                    {
                                        lblRollno.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }



                            lblReg = string.Empty;
                        }




                        //to check which slots already exist and check that slot as true
                        DataSet dsSlots = null;
                        dsSlots = objAttC.GetAllSlots(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]),
                            Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),
                            Convert.ToDateTime(txtFromDate.Text));
                        if (dsSlots.Tables[0].Rows.Count > 0)
                        {
                            //to check slots
                            foreach (ListItem item in chkSlots.Items)
                            {
                                for (int i = 0; i < dsSlots.Tables[0].Rows.Count; i++)
                                {
                                    if (dsSlots.Tables[0].Rows[i]["SLOTNO"].ToString() == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }


                        //txtEventDetail.Text = ds.Tables[0].Rows[0]["ACADEMIC_HOLIDAY_DETAIL"].ToString();

                        //ddlLeaveName.SelectedValue = ds.Tables[0].Rows[0]["LEAVENO"].ToString();

                        //if (ds.Tables[0].Rows[0]["ENDDATE"].ToString() != "")
                        //{
                        //txtToDate.Text = ds.Tables[0].Rows[0]["ENDDATE"].ToString();
                        //}


                        btnSubmit.Enabled = true;
                        btnSubmit.Visible = true;

                        btnODApprove.Visible = true;
                        btnODReject.Visible = true;
                        btnODCancel.Visible = true;

                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();

                        buttonsfunctionality();
                        lvStudent.Visible = false;
                        //pnlStudents.Visible = false;

                        // ODFields.Visible = false;
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    // pnlStudents.Visible = false;
                    lvStudent.Visible = false;
                    buttonsfunctionality();
                }
                ddlLeaveName.Focus();
            }
            else
            {
                txtToDate.Focus();
            }
        }
        catch
        {
            throw;
        }
    }


    #endregion

    private void ClearControls()
    {
        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;

        chkSlots.Items.Clear();
        chkSlots.Items.Clear();
        chkCheckAll.Enabled = false;
        ddlOdType.SelectedIndex = 0;

        //pnlStudents.Visible = false;
        lvStudent.Visible = false;
        //  ddlSession.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        //ddlSection.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;

        lvStudent.DataSource = null;
        lvStudent.DataBind();

        lblSlots1.Visible = false;
        lblSlots2.Visible = false;
        lblSlotCompulsary.Visible = false;
        chkCheckAll.Visible = false;
        buttonsfunctionality();

    }
    #region transaction


    //to get idno for od apply only because only "approval_status = - " will be added in list
    // - it means od leave not applied yet
    private string GetIDNO()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
            Label lblStatus = item.FindControl("lblStatus") as Label;
            //to check only new leave apply
            if (cbRow.Checked && lblStatus.Text == "-")
            {
                if (retIDNO.Length == 0) retIDNO = cbRow.ToolTip.ToString();
                else
                    retIDNO += "," + cbRow.ToolTip.ToString();
            }
        }

        if (retIDNO.Equals(""))
        {
            idno = 0;
            return "0";
        }
        else
        {
            idno = 1;
            return retIDNO;
        }
    }

    //to get regno for od apply only because only "approval_status = - " will be added in list
    // - it means od leave not applied yet
    private string GetREGNO()
    {
        string retRegNo = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
            Label lblRollno = item.FindControl("lblRollno") as Label;
            Label lblStatus = item.FindControl("lblStatus") as Label;
            //to check only new leave apply
            if (cbRow.Checked && lblStatus.Text == "-")
            {
                if (retRegNo.Length == 0) retRegNo = lblRollno.ToolTip.ToString();
                else
                    retRegNo += "," + lblRollno.ToolTip.ToString();
            }
        }

        if (retRegNo.Equals(""))
        {
            RegNo = 0;
            return "0";
        }
        else
        {
            RegNo = 1;
            return retRegNo;
        }
    }


    //od apply
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //bulk student leave apply
        try
        {
            string slotno = string.Empty, idnos = string.Empty, regnos = string.Empty;
            int odType = 1;
            if (txtFromDate.Text != string.Empty)
            {
                hiddenfieldfromDt.Value = txtFromDate.Text;
                if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(hiddenfieldfromDt.Value)))
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select Date in Proper Range", this.Page);
                    txtToDate.Focus();
                    //this.BindListViewWithOD();
                    return;
                }
                if ((Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text)))
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select Date in Proper Range", this.Page);
                    txtToDate.Focus();
                    //this.BindListViewWithOD();
                    return;
                }
                for (int i = 0; i < chkSlots.Items.Count; i++)
                {
                    if (chkSlots.Items[i].Selected == true)
                        slotno += chkSlots.Items[i].Value + ',';
                }

                if (!string.IsNullOrEmpty(slotno))
                    slotno = slotno.Substring(0, slotno.Length - 1);

                if (ddlOdType.SelectedValue == "1")//if NORMAL OD then slots selection mandatory. 
                {
                    if (slotno == string.Empty)
                    {
                        objCommon.DisplayMessage(updBulkReg, "Please select atleast one slot!", this.Page);
                        // this.BindListViewWithOD();
                        return;
                    }
                }

                idnos = GetIDNO();
                if (idnos == "0")
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select At least one Student which are not Pending!!", this.Page);
                    //this.BindListViewWithOD();
                    return;
                }

                regnos = GetREGNO();
                if (regnos == "0")
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select At least one Student!!", this.Page);
                    // this.BindListViewWithOD();
                    return;
                }

                objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                objAttModel.LeaveStartDate = Convert.ToDateTime(txtFromDate.Text);
                if (ddlOdType.SelectedValue == "2")
                    objAttModel.LeaveEndDate = Convert.ToDateTime(txtToDate.Text);
                else
                    objAttModel.LeaveEndDate = Convert.ToDateTime(txtFromDate.Text);

                objAttModel.College_code = Session["colcode"].ToString();
                objAttModel.Event_Detail = txtEventDetail.Text;
                objAttModel.LEAVENO = Convert.ToInt32(ddlLeaveName.SelectedValue);
                objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);
                odType = Convert.ToInt32(ddlOdType.SelectedValue);

                //Apply od
                CustomStatus cs = (CustomStatus)objAttC.AddBulkODLeaveDetails(objAttModel, idnos, regnos, slotno, odType);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updBulkReg, "OD Leave Applied Successfully!", this.Page);
                    ClearControls();
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayMessage(updBulkReg, "Transaction Failed", this.Page);
                }

            }
            else
            {
                objCommon.DisplayMessage(updBulkReg, "Please Enter Date", this.Page);
                return;
            }

            this.BindListViewWithOD();
        }
        catch (Exception ex)
        {
            throw;
        }



    }

    //to clear whole page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //pnlStudents.Visible = false;
        lvStudent.Visible = false;
    }

    protected void clear()
    {
        //txtTotStud.Text = "0";
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        // ddlStatus.SelectedIndex = 0;
        //  ddlSchemeType.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        //pnlStudents.Visible = false;
        lvStudent.Visible = false;
    }



    //for colorful status and checkbox
    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem item = (ListViewDataItem)e.Item;
            string status = (string)DataBinder.Eval(item.DataItem, "STATUS");
            var label = (e.Item.FindControl("lblStatus") as Label);
            label.Font.Bold = true;
            if (status == "PENDING")
            {
                (e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.SandyBrown;
                //(e.Item.FindControl("cbRow") as CheckBox).BackColor = System.Drawing.Color.SandyBrown;

            }
            else if (status == "APPROVED")
            {
                (e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Green;
                (e.Item.FindControl("cbRow") as CheckBox).BackColor = System.Drawing.Color.Green;
            }
            else if (status == "REJECTED")
            {
                (e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
                (e.Item.FindControl("cbRow") as CheckBox).BackColor = System.Drawing.Color.Red;
            }
        }
    }





    #endregion transaction
    //to get slots alloted for selected date and load students according to that
    public void GetSlotsAndLoadStudents()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlSemester.SelectedIndex > 0 && ddlSection.SelectedIndex > 0)
            {
                if (ddlOdType.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select OD Type First", this.Page);
                    ddlOdType.Focus();
                    return;
                }
                else
                {
                    //string idnos = GetIDNO();
                    //if (idnos == "0")
                    //{
                    //    objCommon.DisplayMessage(updBulkReg, "Please Select At least one Student!!", this.Page);
                    //    return;
                    //}
                    //else
                    //{
                    DataSet ds = null;
                    //to get all slots alloted for selected date
                    ds = objAttC.GetSelectedDateSlotsOfBulkStudents(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text));
                    if (ddlOdType.SelectedValue == "1")//Normal OD
                    {
                        txtToDate.Text = txtFromDate.Text;
                        txtToDate.Enabled = false;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            chkSlots.DataSource = ds.Tables[0];
                            chkSlots.DataTextField = "SLOTTIME";
                            chkSlots.DataValueField = "SLOTNO";
                            chkSlots.DataBind();
                            chkCheckAll.Visible = true;
                            chkCheckAll.Enabled = true;
                            chkSlots.Visible = true;
                            lblSlots1.Visible = true;
                            lblSlots2.Visible = true;
                            lblSlotCompulsary.Visible = true;

                            //updBulkReg.Update(); 
                        }
                        else
                        {
                            objCommon.DisplayMessage(updBulkReg, "No Slots available for selected date.", this);
                            chkSlots.DataSource = null;
                            chkSlots.DataBind();
                            chkSlots.Visible = false;
                            chkCheckAll.Visible = false;
                            chkCheckAll.Enabled = false;
                            lblSlots1.Visible = false;
                            lblSlots2.Visible = false;

                            lblSlotCompulsary.Visible = false;
                        }
                    }
                    else //Special  OD
                    {
                        txtToDate.Enabled = true;
                        // txtToDate.Text = string.Empty;
                        chkCheckAll.Enabled = false;
                        lblSlots1.Visible = false;
                        lblSlots2.Visible = false;
                        lblSlotCompulsary.Visible = false;
                    }


                }
            }
            else
            {
                objCommon.DisplayMessage(updBulkReg, "Please Select Details First", this.Page);
                ddlSession.Focus();
                return;
            }
        }
        catch
        {
            throw;
        }
    }


    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetSlotsAndLoadStudents();
            //to load students with selected date and all students with their status
            BindListViewWithOD();
        }
        catch
        {
            throw;
        }
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //to load students with selected date and all students with their status
            BindListViewWithOD();
        }
        catch
        {
            throw;
        }
    }


    //to check all checkbox as checked of slot
    protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCheckAll.Checked == true)
        {
            for (int i = 0; i < chkSlots.Items.Count; i++)
            {
                chkSlots.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < chkSlots.Items.Count; i++)
            {
                chkSlots.Items[i].Selected = false;
            }
        }
    }


    //to get holiday no having Approval_status as Pending
    private string getholidaynoforodapproval()
    {
        try
        {
            string retHNO = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
                Label lblStatus = item.FindControl("lblStatus") as Label;
                HiddenField hfCountOfIDNO = item.FindControl("hfCountOfIDNO") as HiddenField; //from db count
                HiddenField hfHolidayno = item.FindControl("hfHolidayno") as HiddenField;
                HiddenField hfODTYPE = item.FindControl("hfODTYPE") as HiddenField;
                HiddenField hfSelectedSlotCnt = item.FindControl("hfSelectedSlotCnt") as HiddenField;//to get current selected count
                //for hod not limit for od approval
                if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "1")
                {
                    if (cbRow.Checked && lblStatus.Text == "PENDING")
                    {
                        if (retHNO.Length == 0) retHNO = hfHolidayno.Value.ToString();
                        else
                            retHNO += "," + hfHolidayno.Value.ToString();
                    }
                }

                //for faculty 63 limit for od approval
                if (Session["usertype"].ToString() == "3")
                {
                    if (hfCountOfIDNO.Value == "")
                    {
                        hfCountOfIDNO.Value = "0";
                    }

                    int abc = 0;
                    abc = (Convert.ToInt32(hfCountOfIDNO.Value) + Convert.ToInt32(hfSelectedSlotCnt.Value));

                    if ((cbRow.Checked == true) && (lblStatus.Text == "PENDING") && ((Convert.ToInt32(hfCountOfIDNO.Value) + Convert.ToInt32(hfSelectedSlotCnt.Value)) < 63) && (Convert.ToInt32(hfODTYPE.Value) == 1))
                    {
                        if (retHNO.Length == 0) retHNO = hfHolidayno.Value.ToString();
                        else
                            retHNO += "," + hfHolidayno.Value.ToString();
                    }
                }


            }
            if (retHNO.Equals(""))
            {
                hno = 0;
                return "0";
            }
            else
            {
                hno = 1;
                return retHNO;
            }
        }
        catch { return null; }
    }


    //od leave approve from here
    protected void btnODApprove_Click(object sender, EventArgs e)
    {
        //bulk student leave approval
        try
        {
            string holidaynosod = "";

            holidaynosod = getholidaynoforodapproval();
            if (holidaynosod == "0")
            {
                if (Session["usertype"].ToString() == "3")
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select At least one Pending Student Or Approval Limit Exceeded Or Special OD Can not Approve !!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Select At least one Pending Student Or Approval Limit Exceeded !!", this.Page);
                }

                return;
            }

            #region to check attendance new //for remove attendance condition dated on 09-03-2023 Ticket 36479,39896,39960,40064,40376,40407
            //int status = 0, attCount = 0;
            //string slotnos = string.Empty;
            ////============================================================//
            //for (int i = 0; i < chkSlots.Items.Count; i++)
            //{
            //    if (chkSlots.Items[i].Selected == true)
            //        slotnos += chkSlots.Items[i].Value + ',';

            //}
            //slotnos = slotnos.TrimEnd(',');

            //foreach (ListViewDataItem item in lvStudent.Items)
            //{
            //    CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
            //    Label lblStatus = item.FindControl("lblStatus") as Label;
            //    //HiddenField hfHolidayno = item.FindControl("hfHolidayno") as HiddenField;
            //    if (cbRow.Checked == true && lblStatus.Text == "PENDING")
            //    {
            //        //check for Attendance is available for the date and status is Present
            //        DataSet ds = null;
            //        string slotnames = string.Empty;
            //        string studentNames = string.Empty;
            //        string fdate = Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("yyyy/MM/dd");
            //        string toDate = Convert.ToDateTime(txtToDate.Text.ToString()).ToString("yyyy/MM/dd");
            //        if (ddlOdType.SelectedValue.ToString() == "1")//if NORMAL OD then slots selection mandatory. 
            //        {
            //            if (slotnos == string.Empty)
            //            {
            //                objCommon.DisplayMessage(updBulkReg, "Please select atleast one slot!", this.Page);
            //                return;
            //            }
            //            else
            //            {
            //                attCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO", "COUNT(*)", "SA.STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE) AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0 AND SLOTNO IN(" + slotnos + ")"));
            //                if (attCount > 0)
            //                {
            //                    ds = objCommon.FillDropDown("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO INNER JOIN ACD_TIME_SLOT S ON S.SLOTNO=A.SLOTNO INNER JOIN ACD_STUDENT ST ON ST.IDNO=SA.STUDIDS", "DISTINCT A.SLOTNO,TIMEFROM+' - '+TIMETO AS SLOTTIME,ST.STUDNAME", "", "SA.STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE) AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0 AND A.SLOTNO IN(" + slotnos + ")", "");

            //                    foreach (DataRow dr in ds.Tables[0].Rows)
            //                    {
            //                        slotnames = slotnames + dr["SLOTTIME"].ToString() + ',';
            //                        studentNames = studentNames + dr["STUDNAME"].ToString() + ',';
            //                    }

            //                    slotnames = slotnames.TrimEnd(',');
            //                    studentNames = studentNames.TrimEnd(',');
            //                }
            //            }
            //        }
            //        else // IF ODTYPE IS SPECIAL OD THEN CHECK ATT. MARKED OR NOT BETWEEN START DATE & END DATE..
            //        {
            //            attCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO", "COUNT(*)", "SA.STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE)AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0"));
            //        }
            //        if (attCount > 0)
            //        {
            //            if (ddlOdType.SelectedValue.ToString() == "1")//if NORMAL OD then slots selection mandatory. 
            //            {
            //                // lblmessageShow.Text = "Attendance for the selected slots <b>" + slotnames + "</b> and For Selected Students <b>" + studentNames + "</b> has marked as Present ! Please mark as Absent for Approval.";
            //                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
            //                objCommon.DisplayMessage(this, "Attendance for the selected OD date has marked as Present ! Please mark as Absent for Approval.", this);
            //            }
            //            else
            //            {
            //                ds = objCommon.FillDropDown("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO INNER JOIN ACD_STUDENT ST ON ST.IDNO=SA.STUDIDS", "DISTINCT ST.STUDNAME", "", "SA.STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE) AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0", "");

            //                foreach (DataRow dr in ds.Tables[0].Rows)
            //                {
            //                    //slotnames = slotnames + dr["SLOTTIME"].ToString() + ',';
            //                    studentNames = studentNames + dr["STUDNAME"].ToString() + ',';
            //                }

            //                // slotnames = slotnames.TrimEnd(',');
            //                studentNames = studentNames.TrimEnd(',');

            //                // lblmessageShow.Text = "Attendance for Selected OD Students <b>" + studentNames + "</b> date has marked as Present ! Please mark as Absent for Approval.";
            //                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
            //                objCommon.DisplayMessage(this, "Attendance for the selected OD date has marked as Present ! Please mark as Absent for Approval.", this);
            //            }
            //            return;
            //        }
            //    }

            //}
            #endregion

            #region to check attendance commented section
            //int status = 0, attCount = 0;
            //string slotnos = string.Empty;
            ////============================================================//
            //for (int i = 0; i < chkSlots.Items.Count; i++)
            //{
            //    if (chkSlots.Items[i].Selected == true)
            //        slotnos += chkSlots.Items[i].Value + ',';

            //}
            //slotnos = slotnos.TrimEnd(',');

            //foreach (ListViewDataItem item in lvStudent.Items)
            //{
            //    CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
            //    //Label lblStatus = item.FindControl("lblStatus") as Label;
            //    //HiddenField hfHolidayno = item.FindControl("hfHolidayno") as HiddenField;
            //    if (cbRow.Checked == true)
            //    {
            //        //check for Attendance is available for the date and status is Present
            //        DataSet ds = null;
            //        string slotnames = string.Empty;
            //        string studentNames = string.Empty;
            //        string fdate = Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("yyyy/MM/dd");
            //        string toDate = Convert.ToDateTime(txtToDate.Text.ToString()).ToString("yyyy/MM/dd");
            //        if (ddlOdType.SelectedValue.ToString() == "1")//if NORMAL OD then slots selection mandatory. 
            //        {
            //            if (slotnos == string.Empty)
            //            {
            //                objCommon.DisplayMessage(updBulkReg, "Please select atleast one slot!", this.Page);
            //                return;
            //            }
            //            else
            //            {
            //                attCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO", "COUNT(*)", "STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE) AND ATT_STATUS=1 AND SLOTNO IN(" + slotnos + ")"));
            //                if (attCount > 0)
            //                {
            //                    ds = objCommon.FillDropDown("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO INNER JOIN ACD_ACADEMIC_TT_SLOT S ON S.SLOTNO=A.SLOTNO INNER JOIN ACD_STUDENT ST ON ST.IDNO=SA.STUDIDS", "A.SLOTNO,TIMEFROM+' - '+TIMETO AS SLOTTIME,ST.STUDNAME", "", "STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE)AND ATT_STATUS=1 AND A.SLOTNO IN(" + slotnos + ")", "");

            //                    foreach (DataRow dr in ds.Tables[0].Rows)
            //                    {
            //                        slotnames = slotnames + dr["SLOTTIME"].ToString() + ',';
            //                        studentNames = studentNames + dr["STUDNAME"].ToString() + ',';
            //                    }

            //                    slotnames = slotnames.TrimEnd(',');
            //                    studentNames = studentNames.TrimEnd(',');
            //                }
            //            }
            //        }
            //        else // IF ODTYPE IS SPECIAL OD THEN CHECK ATT. MARKED OR NOT BETWEEN START DATE & END DATE..
            //        {
            //            attCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO", "COUNT(*)", "STUDIDS=" + cbRow.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE)AND ATT_STATUS=1"));
            //        }
            //        if (attCount > 0)
            //        {
            //            if (ddlOdType.SelectedValue.ToString() == "1")//if NORMAL OD then slots selection mandatory. 
            //            {
            //                lblmessageShow.Text = "Attendance for the selected slots <b>" + slotnames + "</b> and For Selected Students <b>" + studentNames + "</b> has marked as Present ! Please mark as Absent for Approval.";
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
            //            }
            //            else
            //            {
            //                lblmessageShow.Text = "Attendance for Selected OD Students <b>" + studentNames + "</b> date has marked as Present ! Please mark as Absent for Approval.";
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
            //                //objCommon.DisplayMessage(this, "Attendance for the selected OD date has marked as Present ! Please mark as Absent for Approval.", this);
            //            }
            //            return;
            //        }
            //    }

            //}
            #endregion

            objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);
            // Approved = 1
            //OD Approval
            CustomStatus cs = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, holidaynosod, 1, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updBulkReg, "OD Leave Approved Successfully!", this.Page);
                ClearControls();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updBulkReg, "Transaction Failed", this.Page);
            }

            this.BindListViewWithOD();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //to get holiday no having Approval_status as Pending
    private string getholidaynoforodrejection()
    {
        string retrHNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
            Label lblStatus = item.FindControl("lblStatus") as Label;

            HiddenField hfHolidayno = item.FindControl("hfHolidayno") as HiddenField;
            if (cbRow.Checked && lblStatus.Text == "PENDING")
            {
                if (retrHNO.Length == 0) retrHNO = hfHolidayno.Value.ToString();
                else
                    retrHNO += "," + hfHolidayno.Value.ToString();
            }
        }
        if (retrHNO.Equals(""))
        {
            rhno = 0;
            return "0";
        }
        else
        {
            rhno = 1;
            return retrHNO;
        }
    }
    //od leave reject from here
    protected void btnODReject_Click(object sender, EventArgs e)
    {
        //bulk student leave Reject
        try
        {
            string holidaynosod = "";

            holidaynosod = getholidaynoforodrejection();
            if (holidaynosod == "0")
            {
                objCommon.DisplayMessage(updBulkReg, "Please Select At least one Pending Student!!", this.Page);
                return;
            }

            objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);

            // Rejected = 2

            //OD Rejected
            CustomStatus cs = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, holidaynosod, 2, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updBulkReg, "OD Leave Rejected Successfully!", this.Page);
                ClearControls();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updBulkReg, "Transaction Failed", this.Page);
            }

            this.BindListViewWithOD();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //od leave cancel from here
    protected void btnODCancel_Click(object sender, EventArgs e)
    {
        //bulk student leave cancel
        try
        {
            string holidaynosod = "";

            holidaynosod = getholidaynoforodrejection();
            if (holidaynosod == "0")
            {
                objCommon.DisplayMessage(updBulkReg, "Please Select At least one Pending Student!!", this.Page);
                return;
            }

            objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);

            // cancel = 1

            //OD cancel
            CustomStatus cs = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, holidaynosod, 0, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(updBulkReg, "OD Leave Cancelled Successfully!", this.Page);
                ClearControls();

            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updBulkReg, "Transaction Failed", this.Page);
            }


            this.BindListViewWithOD();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                }
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession1.Items.Count > 0)
        {
            DataSet ds;
            string sessionno = string.Empty;

            for (int k = 0; k < ddlSession1.Items.Count; k++)
            {
                if (ddlSession1.Items[k].Selected == true)
                    sessionno += ddlSession1.Items[k].Value + "$";
            }
            string SP_Name2 = "PKG_GET_APPLIED_STUDENTS_FOR_OD_EXCEL_REPORT";
            string SP_Parameters2 = "@P_SESSION";
            string Call_Values2 = "" + sessionno.TrimEnd('$') + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            DataGrid dg = new DataGrid();
            if (dsStudList != null && dsStudList.Tables.Count > 0 && dsStudList.Tables[0].Rows.Count > 0)
            {
                //  ddlSession1.ClearSelection();
                string attachment = "attachment ; filename=APPLIED_STUDENTS_FOR_OD.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dg.DataSource = dsStudList.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updODReport, "Record Not Found", this.Page);
                ddlSession1.ClearSelection();
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updODReport, "Please Select College & Session", this.Page);
            return;
        }


    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    protected void imgbtnpfPrevDoc2_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updODReport, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updODReport, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        string directoryName = "~/ODAPPLYDOCUMENT" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {
            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string doc = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var Document = doc;
        string extension = Path.GetExtension(doc.ToString());
        if (doc == null || doc == "")
        {
        }
        else
        {
            if (extension == ".pdf")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                //Response.Flush();
                Response.End();
            }
        }
    }
    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN SESSION_ACTIVITY A ON A.SESSION_NO=S.SESSIONNO ", " Distinct S.SESSIONNO", "S.SESSION_NAME", "S.SESSIONNO>0 AND STARTED=1", "S.SESSIONNO DESC");
            objCommon.FillDropDownList(ddlLeaveNamesingle, "acd_specialleavetype", "specialleavetypeno", "specialleavetype", "specialleavetypeno>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "specialleavetypeno");

            //objCommon.FillDropDownList(ddlSessionBulk, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND EXAMTYPE=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlLeaveTypeBulk, "acd_specialleavetype", "specialleavetypeno", "specialleavetype", "specialleavetypeno>0", "specialleavetypeno");
            //Added by Nikhil L. on 29/01/2022
            objCommon.FillDropDownList(ddlCollegesingle, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_NAME");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowDetails()
    {
        try
        {
            if (txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this, "Please Enter Student Registration No./PRN No.", this.Page);
                txtRollNo.Focus();
                return;
            }

            //string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "' OR ENROLLNO = '" + txtRollNo.Text.Trim() + "'AND ISNULL(ADMCAN,0)=0");
            ViewState["idno"] = idno;
            string sessionno = ddlSessionsingle.SelectedValue;

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage(this.updSingle, "Student with Univ. Reg. No. OR Adm. No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                //divCourses.Visible = false;
                //divNote.Visible = true;
                return;
            }

            string facAdvisor = objCommon.LookUp("ACD_STUDENT", "ISNULL(FAC_ADVISOR,0)", "REGNO = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "'");


            if ((string.IsNullOrEmpty(facAdvisor) || facAdvisor != Session["userno"].ToString()) && Session["usertype"].ToString() == "3")
            {
                objCommon.DisplayMessage(this.updSingle, "You are not faculty Advisor of selected Univ. Reg  No. OR Adm. No." + txtRollNo.Text.Trim() + "!", this.Page);
                txtRollNo.Text = string.Empty;
                txtRollNo.Focus();
                //divNote.Visible = true;
                this.ClearControls();
                return;
            }

            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
            string degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno);
            string deptno = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "BRANCHNO=" + branchno + " AND DEGREENO=" + degreeno);
            //string hoddeptno = Session["userdeptno"].ToString();

            //if (Convert.ToInt32(deptno) == hoddeptno || hoddeptno == 0)
            //{

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,(CASE WHEN S.FATHERNAME = '' THEN '--' WHEN S.FATHERNAME IS NULL THEN '--' ELSE FATHERNAME END)FATHERNAME,(CASE WHEN S.MOTHERNAME = '' THEN '--' WHEN S.MOTHERNAME IS NULL THEN '--' ELSE MOTHERNAME END)MOTHERNAME,S.REGNO,S.ROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,ISNULL(S.PHYSICALLY_HANDICAPPED,0) AS PH", "S.IDNO = " + idno, string.Empty);

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    SingleStudOD.Visible = true;
                    btnSubmit.Enabled = true;
                    btnReport.Enabled = true;
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "COUNT(IDNO) ", "IDNO=" + idno + " AND PHOTO IS NOT NULL"));
                    if (count > 0)
                    {
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                    }
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    //ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                    //Payment Type..
                    //ddlPayType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();

                    //physically hadicapped
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString() == "False" ? "No" : "Yes";
                    objCommon.FillDropDownList(ddlSessionsingle, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER S ON (R.SESSIONNO = S.SESSIONNO)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "IDNO =" + idno + " AND SEMESTERNO=" + lblSemester.ToolTip + "", "S.SESSIONNO");
                    tblInfo.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updSingle, "Student with Reg  No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    return;
                }

                try
                {
                    string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno);
                    objCommon.FillDropDownList(ddlSessionsingle, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(college_id), "SESSIONNO DESC");
                    ddlSessionsingle.Focus();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowDetails(int Holidayno)
    {
        try
        {
            string _slotNos = string.Empty;
            SqlDataReader dr = objAttC.GetSingleAcademicLeave(Holidayno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtRollNo.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();

                    txtEventDetailSingle.Text = dr["ACADEMIC_HOLIDAY_DETAIL"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_DETAIL"].ToString();
                    ddlLeaveNamesingle.SelectedValue = dr["ACADEMIC_LEAVE_NO"] == null ? string.Empty : dr["ACADEMIC_LEAVE_NO"].ToString();
                    lblName.ToolTip = dr["IDNO"].ToString();
                    if (dr["ODTYPE"] == null | dr["ODTYPE"].ToString().Equals(""))
                        ddlOdTypesingle.SelectedIndex = 0;
                    else
                        ddlOdTypesingle.SelectedValue = dr["ODTYPE"].ToString();

                    txtFromDatesingle.Text = dr["ACADEMIC_HOLIDAY_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtToDatesingle.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    if (dr["FILENAME"] == null)
                    {
                        lblFileName.Visible = false;
                    }
                    else
                    {
                        lblFileName.Visible = false;
                        lblFileName.Text = dr["FILENAME"].ToString();
                    }

                    if (!string.IsNullOrEmpty(txtFromDatesingle.Text))
                    {
                        this.txtFromDatesingle_TextChanged(new object(), new EventArgs());
                    }
                    if (dr["SESSIONNO"] == null | dr["SESSIONNO"].ToString().Equals(""))
                        ddlSessionsingle.SelectedIndex = 0;
                    else
                        ddlSessionsingle.SelectedValue = dr["SESSIONNO"].ToString();


                    if (chkSlotssingle.Items.Count > 0)
                    {
                        _slotNos = dr["SLOTNO"].ToString();
                        string[] values = _slotNos.Split(',');
                        foreach (string a in values)
                        {
                            for (int i = 0; i < chkSlotssingle.Items.Count; i++)
                            {
                                if (chkSlotssingle.Items[i].Value == a)
                                    chkSlotssingle.Items[i].Selected = true;
                            }
                        }
                    }
                    else
                    {
                        chkSlotssingle.Items.Clear();
                        chkCheckAllsingle.Enabled = false;
                        lblSlot.Visible = false;
                    }

                }
            }
            ShowDetails();
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ClearControlssingle();
        ShowDetails();
        lvExamday.Visible = false;
    }
    protected void ddlSessionsingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlOdTypesingle, "ACD_ODTYPE", "ODID", "OD_NAME", "ODID>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "ODID");
        BindListView();
    }
    protected void ddlCollegesingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollegesingle.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSessionsingle, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegesingle.SelectedValue), "SESSIONNO DESC");
                ddlSessionsingle.Focus();

            }
            else
            {
                ddlCollegesingle.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlOdTypesingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOdTypesingle.SelectedValue == "1")
        {
            txtToDatesingle.Enabled = false;
            CalendarExtender4.Enabled = false;
            txtToDatesingle.Text = string.Empty;
            txtFromDatesingle.Text = string.Empty;
            chkSlotssingle.Items.Clear();
        }
        else
        {
            txtToDatesingle.Enabled = true;
            CalendarExtender4.Enabled = true;
            txtToDatesingle.Text = string.Empty;
            txtFromDatesingle.Text = string.Empty;
            chkSlotssingle.Items.Clear();
        }
    }
    private void ClearControlssingle()
    {
        ddlSessionsingle.SelectedIndex = 0;
        txtEventDetailSingle.Text = string.Empty;
        ddlLeaveNamesingle.SelectedIndex = 0;
        txtFromDatesingle.Text = string.Empty;
        txtToDatesingle.Text = string.Empty;
        ViewState["action"] = null;
        ddlOdTypesingle.SelectedIndex = 0;
        ddlCollegesingle.SelectedIndex = 0;
        //txtRollNo.Text = string.Empty;
        chkSlotssingle.Items.Clear();
        chkCheckAllsingle.Enabled = false;
        lblSlot.Visible = false;
    }
    protected void btnCancelsingle_Click(object sender, EventArgs e)
    {
        //ClearControlssingle();
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtEventDetailSingle.Text = string.Empty;
            ddlLeaveNamesingle.SelectedIndex = 0;
            txtFromDatesingle.Text = string.Empty;
            txtToDatesingle.Text = string.Empty;
            ddlOdTypesingle.SelectedIndex = 0;
            chkSlotssingle.Items.Clear();
            chkCheckAllsingle.Visible = false;
            chkSlotssingle.Visible = false;
            lblSlot.Visible = false;
            ImageButton btnEdit = sender as ImageButton;
            int holidayno = int.Parse(btnEdit.CommandArgument);
            ViewState["holidayno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(holidayno);

            ddlSessionsingle.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SessionController objsessionn = new SessionController();
            ImageButton btnDelete = sender as ImageButton;
            int Sessionno = int.Parse(btnDelete.CommandArgument);

            //Delete 
            //CustomStatus cs = (CustomStatus)objsessionn.DeleteAcademicLeave(Convert.ToInt32(btnDelete.ToolTip));
            objCommon.DisplayMessage(this.updSingle, "Holiday Entry Deleted Successfully !!", this.Page);
            this.BindListView();
            this.ClearControlssingle();
            return;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void imgbtnpfPrevDoc_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSingle, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updSingle, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        string directoryName = "~/ODAPPLYDOCUMENT" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string doc = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var Document = doc;
        string extension = Path.GetExtension(doc.ToString());
        if (doc == null || doc == "")
        {
        }
        else
        {
            if (extension == ".pdf")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                //Response.Flush();
                Response.End();
            }
        }
    }
    protected void lvExamday_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem item = (ListViewDataItem)e.Item;
            string status = (string)DataBinder.Eval(item.DataItem, "APPROVAL_STATUS");

            if (status == "PENDING")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.SandyBrown;
                (e.Item.FindControl("chkOd") as CheckBox).Enabled = true;
            }
            else if (status == "APPROVED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Green;
                (e.Item.FindControl("chkOd") as CheckBox).Enabled = false;
            }
            else if (status == "REJECTED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Red;
                (e.Item.FindControl("chkOd") as CheckBox).Enabled = false;
            }
            else if (status == "CANCELLED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Black;
                (e.Item.FindControl("chkOd") as CheckBox).Enabled = false;
            }

        }
    }
    private void BindListView()
    {
        try
        {
            string sessiutype = Session["usertype"].ToString();
            string sessuno = Session["userno"].ToString();
            DataSet ds = objAttC.GetSingleOdList(Convert.ToInt32(ddlSessionsingle.SelectedValue), ViewState["idno"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvExamday.DataSource = ds;
                lvExamday.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvExamday);
                lvExamday.Visible = true;
            }
            else
            {
                lvExamday.DataSource = null;
                lvExamday.DataBind();
                lvExamday.Visible = false;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvExamday.Items)
                {
                    ImageButton btnEdit = item.FindControl("btnEdit") as ImageButton;
                    Label lblStatus = item.FindControl("lblAStatus") as Label;

                    if (lblStatus.Text == "PENDING")
                    {
                        btnEdit.Enabled = true;
                        btnEdit.ImageUrl = "~/Images/edit.png";
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        if (lblStatus.Text == "APPROVED")
                        {
                            btnEdit.ImageUrl = "~/images/check1.jpg";
                            //btnEdit.ImageUrl.wi="50px";
                            btnEdit.Width = Unit.Pixel(15);
                        }
                        if (lblStatus.Text == "REJECTED")
                        {
                            btnEdit.ImageUrl = "~/Images/delete.png";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void chkCheckAllsingle_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCheckAllsingle.Checked == true)
        {
            for (int i = 0; i < chkSlotssingle.Items.Count; i++)
            {
                chkSlotssingle.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < chkSlotssingle.Items.Count; i++)
            {
                chkSlotssingle.Items[i].Selected = false;
            }
        }
    }
    public void LoadSlots()
    {
        if (txtFromDatesingle.Text != "" && ddlSessionsingle.SelectedValue != null)
        {
            DataSet ds = null;
            int idno = lblName.ToolTip != string.Empty ? Convert.ToInt32(lblName.ToolTip) : 0;
            ds = objAttC.GetSelectedDateSlots(Convert.ToInt32(ddlSessionsingle.SelectedValue), idno, Convert.ToDateTime(txtFromDatesingle.Text));

            if (ddlOdTypesingle.SelectedValue == "1")//Normal OD
            {
                txtToDatesingle.Text = txtFromDatesingle.Text;
                txtToDatesingle.Enabled = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkSlotssingle.DataSource = ds;
                    chkSlotssingle.DataTextField = "SLOTTIME";
                    chkSlotssingle.DataValueField = "SLOTNO";
                    chkSlotssingle.DataBind();
                    chkSlotssingle.Visible = true;
                    chkCheckAllsingle.Visible = true;
                    chkCheckAllsingle.Enabled = true;
                    lblSlot.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updSingle, "No Slots available for selected date.", this);
                    chkSlotssingle.DataSource = null;
                    chkSlotssingle.DataBind();
                    chkSlotssingle.Visible = false;
                    chkCheckAllsingle.Visible = false;
                    chkCheckAllsingle.Enabled = false;
                    lblSlot.Visible = false;
                }
            }
            else //Special  OD
            {
                txtToDatesingle.Enabled = true;
                // txtToDate.Text = string.Empty;
                chkCheckAllsingle.Enabled = false;
            }
        }
    }
    protected void txtFromDatesingle_TextChanged(object sender, EventArgs e)
    {
        try
        {
            LoadSlots();
        }
        catch
        {
            throw;
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        //bulk student leave Reject
        try
        {
            string singleholidaynosod = "";

            singleholidaynosod = singleholidaynoforodrejection();
            if (singleholidaynosod == "0")
            {
                objCommon.DisplayMessage(this.updSingle, "Please Select At least one Pending Student!!", this.Page);
                return;
            }

            objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);
            // Rejected = 2

            //OD Rejected
            CustomStatus cs = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, singleholidaynosod, 2, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updSingle, "OD Leave Rejected Successfully!", this.Page);
                //ClearControlssingle();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this.updSingle, "Transaction Failed", this.Page);
            }

            BindListView();
            ClearControlssingle();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private string singleholidaynoforodrejection()
    {
        string retrOD = string.Empty;
        foreach (ListViewDataItem item in lvExamday.Items)
        {
            CheckBox chkOd = item.FindControl("chkOd") as CheckBox;
            Label lblStatus = item.FindControl("lblAStatus") as Label;

            HiddenField hidTtno = item.FindControl("hidTtno") as HiddenField;
            if (chkOd.Checked && lblStatus.Text == "PENDING")
            {
                if (retrOD.Length == 0) retrOD = hidTtno.Value.ToString();
                else
                    retrOD += "," + hidTtno.Value.ToString();
            }
        }
        if (retrOD.Equals(""))
        {
            rhno = 0;
            return "0";
        }
        else
        {
            rhno = 1;
            return retrOD;
        }
    }
    protected void btnCancelODsingle_Click(object sender, EventArgs e)
    {
        try
        {
            string singleholidaynosod = "";

            singleholidaynosod = singleholidaynoforodrejection();
            if (singleholidaynosod == "0")
            {
                objCommon.DisplayMessage(this.updSingle, "Please Select At least one Pending Student!!", this.Page);
                return;
            }

            objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);
            // cancel = 1

            //OD cancel
            CustomStatus cs = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, singleholidaynosod, 0, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updSingle, "OD Leave Cancelled Successfully!", this.Page);
                //ClearControlssingle();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this.updSingle, "Transaction Failed", this.Page);
            }

            BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            int Academic_Session = 0;

            Academic_Session = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_LEAVE_MASTER", "COUNT(*)", "isnull(cancel,0)=0 and idno=" + Convert.ToInt32(ViewState["idno"].ToString()) + " AND ISNULL(APPROVAL_STATUS,0)<>2 AND ( ( CONVERT(DATE,'" + Convert.ToDateTime(txtFromDatesingle.Text.Trim()).ToString("dd/MM/yyyy") + "',103) between CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) and CONVERT(DATE,ACADEMIC_HOLIDAY_ENDDATE,103) ) OR  (CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) between CONVERT(DATE,'" + Convert.ToDateTime(txtFromDatesingle.Text.Trim()).ToString("dd/MM/yyyy") + "',103)  and CONVERT(DATE,'" + Convert.ToDateTime(txtToDatesingle.Text.Trim()).ToString("dd/MM/yyyy") + "',103) ) OR  (CONVERT(DATE,ACADEMIC_HOLIDAY_ENDDATE,103) between CONVERT(DATE,'" + Convert.ToDateTime(txtFromDatesingle.Text.Trim()).ToString("dd/MM/yyyy") + "',103)  and CONVERT(DATE,'" + Convert.ToDateTime(txtToDatesingle.Text.Trim()).ToString("dd/MM/yyyy") + "',103) )  )"));

            if (Academic_Session > 0)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return flag;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            int flagapprove = 0;
            string flagholiday = singleholidaynoforodrejection();
            if (flagholiday == "0")
            {
                flagapprove = 1;
            }
            if (flagapprove == 1)
            {
                //if (ddlCollegesingle.SelectedValue == "0")
                //{
                //    objCommon.DisplayMessage(this.updSingle, "Please Select College.", this.Page);
                //    ddlCollegesingle.Focus();
                //}
                if (ddlSessionsingle.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updSingle, "Please Select Session.", this.Page);
                    ddlSessionsingle.Focus();
                }
                else if (ddlOdTypesingle.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updSingle, "Please Select OD Type.", this.Page);
                    ddlOdTypesingle.Focus();
                }
                else if (ddlLeaveNamesingle.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updSingle, "Please Select Leave Type.", this.Page);
                    ddlLeaveNamesingle.Focus();
                }
                else if (txtEventDetailSingle.Text == "0")
                {
                    objCommon.DisplayMessage(this.updSingle, "Please Enter Reason.", this.Page);
                    txtEventDetailSingle.Focus();
                }
                else
                {
                    string slotno = string.Empty, idnos = string.Empty, regnos = string.Empty;
                    int odType = 1;
                    if (txtFromDatesingle.Text != string.Empty)
                    {
                        hiddenfieldfromDtsingle.Value = txtFromDatesingle.Text;
                        if ((Convert.ToDateTime(txtFromDatesingle.Text) < Convert.ToDateTime(hiddenfieldfromDtsingle.Value)) | (Convert.ToDateTime(txtFromDatesingle.Text) > Convert.ToDateTime(hiddenfieldfromDtsingle.Value)))
                        {
                            objCommon.DisplayMessage(this.updSingle, "Please Select Date in Proper Range", this.Page);
                            txtToDatesingle.Focus();
                            return;
                        }
                        if ((Convert.ToDateTime(txtFromDatesingle.Text) > Convert.ToDateTime(txtToDatesingle.Text)))
                        {
                            objCommon.DisplayMessage(this.updSingle, "Please Select Date in Proper Range", this.Page);
                            txtToDatesingle.Focus();
                            return;
                        }
                        for (int i = 0; i < chkSlotssingle.Items.Count; i++)
                        {
                            if (chkSlotssingle.Items[i].Selected == true)
                                slotno += chkSlotssingle.Items[i].Value + ',';
                        }

                        if (!string.IsNullOrEmpty(slotno))
                            slotno = slotno.Substring(0, slotno.Length - 1);

                        if (ddlOdTypesingle.SelectedValue == "1")//if NORMAL OD then slots selection mandatory. 
                        {
                            if (slotno == string.Empty)
                            {
                                objCommon.DisplayMessage(this.updSingle, "Please select atleast one slot!", this.Page);
                                //return;
                            }
                        }

                        objAttModel.Sessionno = Convert.ToInt32(ddlSessionsingle.SelectedValue);
                        objAttModel.LeaveStartDate = Convert.ToDateTime(txtFromDatesingle.Text);
                        if (ddlOdTypesingle.SelectedValue == "2")
                            objAttModel.LeaveEndDate = Convert.ToDateTime(txtToDatesingle.Text);
                        else
                            objAttModel.LeaveEndDate = Convert.ToDateTime(txtFromDatesingle.Text);

                        objAttModel.College_code = Session["colcode"].ToString();
                        objAttModel.Event_Detail = txtEventDetailSingle.Text;
                        objAttModel.LEAVENO = Convert.ToInt32(ddlLeaveNamesingle.SelectedValue);
                        objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);
                        odType = Convert.ToInt32(ddlOdTypesingle.SelectedValue);
                        idnos = ViewState["idno"].ToString();

                        if (CheckDuplicateEntry() == true)
                        {
                            objCommon.DisplayMessage(this.updSingle, "Entry For This Date Already Done!", this.Page);
                            return;
                        }
                        //Apply OD

                        CustomStatus cs = (CustomStatus)objAttC.AddBulkODLeaveDetails(objAttModel, idnos, txtRollNo.Text, slotno, odType);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //objCommon.DisplayMessage(updSingle, "OD Leave Applied and Approved Successfully!", this.Page);

                        }
                        else if (cs.Equals(CustomStatus.TransactionFailed))
                        {
                            objCommon.DisplayMessage(this.updSingle, "Transaction Failed", this.Page);
                        }

                        //For OD Approve

                        string holidaynosod = objCommon.LookUp("ACD_ACADEMIC_LEAVE_MASTER", "MAX(HOLIDAY_NO)", "IDNO=" + idnos + "");
                        // Approved = 1

                        CustomStatus csapprove = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, holidaynosod, 1, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
                        if (csapprove.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.updSingle, "OD Leave Applied and Approved Successfully!", this.Page);
                        }
                        else if (csapprove.Equals(CustomStatus.TransactionFailed))
                        {
                            objCommon.DisplayMessage(this.updSingle, "Transaction Failed", this.Page);
                        }

                        BindListView();
                        ClearControlssingle();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updSingle, "Please Enter Date", this.Page);
                        return;
                    }
                }
            }
            else
            {
                string singleholidaynosod = "";

                singleholidaynosod = singleholidaynoforodrejection();
                if (singleholidaynosod == "0")
                {
                    objCommon.DisplayMessage(this.updSingle, "Please Select At least one Pending Student!!", this.Page);
                    return;
                }

                objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);

                CustomStatus cs = (CustomStatus)objAttC.ApproveORRejectBulkODLeaveDetails(objAttModel, singleholidaynosod, 1, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updSingle, "OD Leave Approved Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayMessage(this.updSingle, "Transaction Failed", this.Page);
                }
                flagapprove = 0;
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitShowCancel_Click(object sender, EventArgs e)
    {
        ClearControlscancel();
        ShowDetailsCancel();
        lvExamdaycancel.Visible = false;
    }
    private void ClearControlscancel()
    {
        ddlSessionsinglecan.SelectedIndex = 0;
    }
    private void ShowDetailsCancel()
    {
        try
        {
            if (txtRollNoCancel.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this, "Please Enter Student Registration No./PRN No.", this.Page);
                txtRollNoCancel.Focus();
                return;
            }

            //string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNoCancel.Text.Trim() + "' OR ENROLLNO = '" + txtRollNoCancel.Text.Trim() + "'AND ISNULL(ADMCAN,0)=0");
            ViewState["idno"] = idno;
            string sessionno = ddlSessionsinglecan.SelectedValue;

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage(this.updODCancel, "Student with Univ. Reg. No. OR Adm. No." + txtRollNoCancel.Text.Trim() + " Not Exists!", this.Page);
                //divCourses.Visible = false;
                //divNote.Visible = true;
                return;
            }

            string facAdvisor = objCommon.LookUp("ACD_STUDENT", "ISNULL(FAC_ADVISOR,0)", "REGNO = '" + txtRollNoCancel.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "'");


            if ((string.IsNullOrEmpty(facAdvisor) || facAdvisor != Session["userno"].ToString()) && Session["usertype"].ToString() == "3")
            {
                objCommon.DisplayMessage(this.updODCancel, "You are not faculty Advisor of selected Univ. Reg  No. OR Adm. No." + txtRollNoCancel.Text.Trim() + "!", this.Page);
                txtRollNoCancel.Text = string.Empty;
                txtRollNoCancel.Focus();
                //divNote.Visible = true;
                this.ClearControlsCancel();
                return;
            }

            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
            string degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno);
            string deptno = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "BRANCHNO=" + branchno + " AND DEGREENO=" + degreeno);
            //string hoddeptno = Session["userdeptno"].ToString();

            //if (Convert.ToInt32(deptno) == hoddeptno || hoddeptno == 0)
            //{

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,(CASE WHEN S.FATHERNAME = '' THEN '--' WHEN S.FATHERNAME IS NULL THEN '--' ELSE FATHERNAME END)FATHERNAME,(CASE WHEN S.MOTHERNAME = '' THEN '--' WHEN S.MOTHERNAME IS NULL THEN '--' ELSE MOTHERNAME END)MOTHERNAME,S.REGNO,S.ROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,ISNULL(S.PHYSICALLY_HANDICAPPED,0) AS PH", "S.IDNO = " + idno, string.Empty);

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "COUNT(IDNO) ", "IDNO=" + idno + " AND PHOTO IS NOT NULL"));
                    if (count > 0)
                    {
                        imgPhotocan.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";
                    }
                    else
                    {
                        imgPhotocan.ImageUrl = "~/IMAGES/nophoto.jpg";
                    }
                    //Show Student Details..
                    lblNamecan.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblNamecan.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                    lblFatherNamecan.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherNamecan.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    lblEnrollNocan.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranchcan.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranchcan.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblSchemecan.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblSchemecan.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemestercan.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemestercan.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatchcan.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatchcan.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                    //physically hadicapped
                    lblPHcan.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString() == "False" ? "No" : "Yes";
                    objCommon.FillDropDownList(ddlSessionsinglecan, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER S ON (R.SESSIONNO = S.SESSIONNO)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "IDNO =" + idno + " AND SEMESTERNO=" + lblSemestercan.ToolTip + "", "S.SESSIONNO");
                    tblInfoCancel.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updODCancel, "Student with Reg  No." + txtRollNoCancel.Text.Trim() + " Not Exists!", this.Page);
                    return;
                }

                try
                {
                    string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno);
                    objCommon.FillDropDownList(ddlSessionsinglecan, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(college_id), "SESSIONNO DESC");
                    ddlSessionsinglecan.Focus();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControlsCancel()
    {
        buttonsfunctionality();
    }
    protected void ddlSessionsinglecan_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewCancel();
    }
    private void BindListViewCancel()
    {
        try
        {
            string sessiutype = Session["usertype"].ToString();
            string sessuno = Session["userno"].ToString();
            DataSet ds = objAttC.GetOdListForCancel(Convert.ToInt32(ddlSessionsinglecan.SelectedValue), ViewState["idno"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvExamdaycancel.DataSource = ds;
                lvExamdaycancel.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvExamdaycancel);
                lvExamdaycancel.Visible = true;
            }
            else
            {
                lvExamdaycancel.DataSource = null;
                lvExamdaycancel.DataBind();
                lvExamdaycancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancelsinglecan_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lvExamdaycancel_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem item = (ListViewDataItem)e.Item;
            string status = (string)DataBinder.Eval(item.DataItem, "APPROVAL_STATUS");
            var label = (e.Item.FindControl("lblStatuscan") as Label);
            label.Font.Bold = true;
            if (status == "PENDING")
            {
                (e.Item.FindControl("lblStatuscan") as Label).ForeColor = System.Drawing.Color.SandyBrown;

            }
            else if (status == "APPROVED")
            {
                (e.Item.FindControl("lblStatuscan") as Label).ForeColor = System.Drawing.Color.Green;
            }
        }
    }
    protected void btnCancelOd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnCancelOd = sender as ImageButton;
            int holidayno = int.Parse(btnCancelOd.CommandArgument);

            CustomStatus cs = (CustomStatus)objAttC.CancelODLeaveDetails(holidayno, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updBulkReg, "OD Leave Cancel Successfully!", this.Page);
                //ClearControlsCancel();
                BindListViewCancel();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updBulkReg, "Transaction Failed", this.Page);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
