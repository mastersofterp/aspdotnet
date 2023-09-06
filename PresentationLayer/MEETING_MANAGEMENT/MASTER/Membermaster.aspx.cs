//======================================================================
// MODIFY BY      : MRUNAL SINGH                              
// MODIFYING DATE : 01-12-2014                                          
// DESCRIPTION    : TO TAKE THE FACULTIES FROM PAYROLL AND TRY TO 
//                  GET THE IDNO OF FACULTY.(SMS, EMAIL)                   
// MODIFYING DATE : 16-02-2015                                          
// DESCRIPTION    : ADD DEPTNO OF THE USER.      
//======================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class MEETING_MANAGEMENT_MASTERS_Membermaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingEntity objmas = new MeetingEntity();
    MM_CONTROLLER ojccon = new MM_CONTROLLER();
    public static string flag = "ADD";

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

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //  ViewState["action"] = "add";
                    flag = "ADD";
                }
                // This method is used to get the list of members departmentwise.
                //FillCollege();
                //objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "", "FNAME"); // STAFFNO in (1,2,5) AND

                FillMember();
                bindlist();
                FillDropDown();
            }
            txtTitle.Enabled = true;
            txtFName.Enabled = true;
            txtMName.Enabled = true;
            txtLName.Enabled = true;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Membermaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillMember()
    {
        objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "P.PSTATUS='Y'", "FNAME");
    }
    private void FillCollege()
    {
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");


        //if (Convert.ToInt16(Session["usertype"]) != 1 && Convert.ToInt16(Session["usertype"]) != 8)
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }

    public void FillDropDown()
    {

        objCommon.FillDropDownList(ddlCityTemp, "ACD_CITY", "CITY AS CITYA", "CITY AS CITYB", "CITYNO > 0", "CITY");
        objCommon.FillDropDownList(ddlStateTemp, "ACD_STATE", "STATENAME AS STATENAMEA", "STATENAME AS STATENAMEB", "STATENO > 0", "STATENAME");
        objCommon.FillDropDownList(ddlCountryTemp, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");

        objCommon.FillDropDownList(ddlCityPerm, "ACD_CITY", "CITY AS CITYA", "CITY AS CITYB", "CITYNO > 0", "CITY");
        objCommon.FillDropDownList(ddlStatePerm, "ACD_STATE", "STATENAME AS STATENAMEA", "STATENAME AS STATENAMEB", "STATENO > 0", "STATENAME");
        objCommon.FillDropDownList(ddlCountryPerm, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
        objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }

    // This method is used to check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    // This method is used to bind the list of Member departmentwise.
    public void bindlist()
    {
        DataSet ds = null;
        lvMRBills.DataSource = null;
        lvMRBills.DataBind();
        if (rdblistMemberType.SelectedValue == "1")
        {
             ds = objCommon.FillDropDown("TBL_MM_MENBERDETAILS MD LEFT OUTER JOIN TBL_MM_RELETIONMASTER RM ON(MD.PK_CMEMBER=RM.FK_MEMBER)", " DISTINCT PK_CMEMBER, MD.USERID", "(TITLE+' '+FNAME+' '+MNAME+ ' '+LNAME)AS NAME, (CASE FLAG WHEN 'E' THEN 'External' ELSE 'Internal' END) AS MEMBER_TYPE,(CASE isnull(MD.MEMBER_TYPE,1) WHEN 1 THEN 'Employee' ELSE 'Student' END) as EMPSTUD_TYPE", "MD.MEMBER_TYPE=1", "PK_CMEMBER");
        }
        else if (rdblistMemberType.SelectedValue == "2")
        {
            ds = objCommon.FillDropDown("TBL_MM_MENBERDETAILS MD LEFT OUTER JOIN TBL_MM_RELETIONMASTER RM ON(MD.PK_CMEMBER=RM.FK_MEMBER)", " DISTINCT PK_CMEMBER, MD.USERID", "(TITLE+' '+FNAME+' '+MNAME+ ' '+LNAME)AS NAME, (CASE FLAG WHEN 'E' THEN 'External' ELSE 'Internal' END) AS MEMBER_TYPE,(CASE isnull(MD.MEMBER_TYPE,1) WHEN 1 THEN 'Employee' ELSE 'Student' END) as EMPSTUD_TYPE", "MD.MEMBER_TYPE=2", "PK_CMEMBER");
        }
        else
        {
            ds = objCommon.FillDropDown("TBL_MM_MENBERDETAILS MD LEFT OUTER JOIN TBL_MM_RELETIONMASTER RM ON(MD.PK_CMEMBER=RM.FK_MEMBER)", " DISTINCT PK_CMEMBER, MD.USERID", "(TITLE+' '+FNAME+' '+MNAME+ ' '+LNAME)AS NAME, (CASE FLAG WHEN 'E' THEN 'External' ELSE 'Internal' END) AS MEMBER_TYPE,(CASE isnull(MD.MEMBER_TYPE,1) WHEN 1 THEN 'Employee' ELSE 'Student' END) as EMPSTUD_TYPE", "", "PK_CMEMBER");
        }
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvMRBills.DataSource = ds;
                lvMRBills.DataBind();
                lvMRBills.Visible = true;
            }
            else
            {
                lvMRBills.DataSource = null;
                lvMRBills.DataBind();
                lvMRBills.Visible = false;
            }
        }
    }

    // This method is used to clear the controls.
    public void clear()
    {
       
        txtTitle.Text = string.Empty;
        txtFName.Text = string.Empty;
        txtMName.Text = string.Empty;
        txtLName.Text = string.Empty;
        txtRepresentation.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtConstituency.Text = string.Empty;
        txtAddOfficial.Text = string.Empty;
        txtAddPerm.Text = string.Empty;
        txtoffEmail.Text = string.Empty;
        txtperemail.Text = string.Empty;
        txtPhoneOffc.Text = string.Empty;
        txtPhonePerm.Text = string.Empty;
        txtPinOffci.Text = string.Empty;
        txtPinPerm.Text = string.Empty;
        Chksame.Checked = false;
        //ddlMember.SelectedValue = "";
        txtTitle.Enabled = true;
        txtFName.Enabled = true;
        txtMName.Enabled = true;
        txtLName.Enabled = true;
        txtDOB.Text = string.Empty;
        txtProfession.Text = string.Empty;
        txtAcadQuali.Text = string.Empty;
        txtPAN.Text = string.Empty;
        txtFrmDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        txtAddLinePerm.Text = string.Empty;
        txtAddLineOfficial.Text = string.Empty;
        ddlCityPerm.SelectedIndex = 0;
        ddlCityTemp.SelectedIndex = 0;
        ddlStatePerm.SelectedIndex = 0;
        ddlStateTemp.SelectedIndex = 0;
        ddlCountryPerm.SelectedIndex = 0;
        ddlCountryTemp.SelectedIndex = 0;
        txtMobilePerm.Text = string.Empty;
        txtMobileTemp.Text = string.Empty;
        //ddlCollege.SelectedIndex = 0;
        txtJoiningDate.Text = string.Empty;
        ViewState["PK_CMEMBER"] = null;
        imgEmpPhoto.ImageUrl = "";
        flag = "ADD";
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        if(rdblistMemberType.SelectedValue != "2")
        {
            rdblistMemberType.SelectedValue = "1";
        }
       // rdblistMemberType.SelectedValue = "1";
        ddlMember.SelectedIndex = 0;
        spanAddress.Visible = true;
        imgEmpPhoto.ImageUrl = "~/Images/nophoto.jpg"; //Shaikh Juned 25-06-2022
    }

    // This method is used to get the details of particular record.
    public void showdetails(int PKID)
    {
        int Usernumber = 0;
        DataSet ds = objCommon.FillDropDown("TBL_MM_MENBERDETAILS", "DISTINCT TITLE, FNAME, MNAME, LNAME, REPRESENTATION, DESIGNATION, CONSTITUENCY, MEMBER_JOINING_DATE, COLLEGENO, P_ADDRESS, P_CITY, P_STATE, P_PHONE, P_EMAIL, P_PIN , T_CITY, T_ADDRESS,T_STATE,T_PIN,T_EMAIL", "T_PHONE, USERID, AUDITDATE, IS_SAME_ADDRESS, DEPTNO, P_ADDRESSLINE, P_COUNTRY, P_MOBILE, T_ADDRESSLINE, T_COUNTRY,T_MOBILE, DOB,PROFESSION,ACAD_QUALI,PAN_NO,FROM_DATE,TO_DATE,USERID,MEMBER_TYPE as MEMBER_TYPE", "PK_CMEMBER='" + PKID + "'", "");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                imgEmpPhoto.ImageUrl = "";

                Usernumber = Convert.ToInt32(ds.Tables[0].Rows[0]["USERID"].ToString());
                txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                txtFName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                txtMName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                txtLName.Text = ds.Tables[0].Rows[0]["LNAME"].ToString();
                txtRepresentation.Text = ds.Tables[0].Rows[0]["Representation"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();
                txtConstituency.Text = ds.Tables[0].Rows[0]["CONSTITUENCY"].ToString();
                txtAddOfficial.Text = ds.Tables[0].Rows[0]["T_ADDRESS"].ToString();
                txtAddPerm.Text = ds.Tables[0].Rows[0]["P_ADDRESS"].ToString();
                txtoffEmail.Text = ds.Tables[0].Rows[0]["T_EMAIL"].ToString();
                txtperemail.Text = ds.Tables[0].Rows[0]["P_EMAIL"].ToString();
                txtPhoneOffc.Text = ds.Tables[0].Rows[0]["T_PHONE"].ToString();
                txtPhonePerm.Text = ds.Tables[0].Rows[0]["P_PHONE"].ToString();
                txtPinOffci.Text = ds.Tables[0].Rows[0]["T_PIN"].ToString();
                txtPinPerm.Text = ds.Tables[0].Rows[0]["P_PIN"].ToString();

                txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                txtProfession.Text = ds.Tables[0].Rows[0]["PROFESSION"].ToString();
                txtAcadQuali.Text = ds.Tables[0].Rows[0]["ACAD_QUALI"].ToString();
                txtPAN.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                txtAddLineOfficial.Text = ds.Tables[0].Rows[0]["T_ADDRESSLINE"].ToString();
                txtAddLinePerm.Text = ds.Tables[0].Rows[0]["P_ADDRESSLINE"].ToString();

                //if (ds.Tables[0].Rows[0]["T_CITY"].ToString() == "")
                //{
                //    ddlCityTemp.SelectedIndex = 0;
                //}
                //else
                //{
                //    ddlCityTemp.SelectedIndex = ds.Tables[0].Rows[0]["T_CITY"].ToString();
                //}


                if (ds.Tables[0].Rows[0]["T_CITY"].ToString() == "")
                {
                    ddlCityTemp.SelectedIndex = 0;
                    
                }
                else
                {
                    ddlCityTemp.SelectedValue = ds.Tables[0].Rows[0]["T_CITY"].ToString();
                }


                if (ds.Tables[0].Rows[0]["P_CITY"].ToString() == "")
                {
                    ddlCityPerm.SelectedIndex = 0;
                }
                else
                {
                    ddlCityPerm.SelectedValue = ds.Tables[0].Rows[0]["P_CITY"].ToString();
                }


                if (ds.Tables[0].Rows[0]["T_STATE"].ToString() == "")
                {
                    ddlStateTemp.SelectedIndex = 0;
                }
                else
                {
                    ddlStateTemp.SelectedValue = ds.Tables[0].Rows[0]["T_STATE"].ToString();
                }


                if (ds.Tables[0].Rows[0]["P_STATE"].ToString() == "")
                {
                    ddlStatePerm.SelectedIndex = 0;
                }
                else
                {
                    ddlStatePerm.SelectedValue = ds.Tables[0].Rows[0]["P_STATE"].ToString();
                }

                txtMobileTemp.Text = ds.Tables[0].Rows[0]["T_MOBILE"].ToString();
                txtMobilePerm.Text = ds.Tables[0].Rows[0]["P_MOBILE"].ToString();

                ddlCountryTemp.SelectedValue = ds.Tables[0].Rows[0]["T_COUNTRY"].ToString();
                ddlCountryPerm.SelectedValue = ds.Tables[0].Rows[0]["P_COUNTRY"].ToString();

                txtJoiningDate.Text = ds.Tables[0].Rows[0]["MEMBER_JOINING_DATE"].ToString();

                FillCollege();

                //ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGENO"].ToString();
                // objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "P.PSTATUS='Y' AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "FNAME");
                rdblistMemberType.SelectedValue = ds.Tables[0].Rows[0]["MEMBER_TYPE"].ToString();
                if (rdblistMemberType.SelectedValue == "2")
                {
                    spanAddress.Visible = false;
                    divStudPanel.Visible = true;
                    divShow.Visible = true;
                    objCommon.FillDropDownList(ddlMember, "ACD_STUDENT", "IDNO", "(STUDNAME+' '+ISNULL(STUDLASTNAME,'')) AS FULLNAME", "ISNULL(ADMCAN,0)=0 ", "STUDNAME");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                    objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "A.LONGNAME", "B.DEGREENO > 0 AND A.BRANCHNO>0", "A.BRANCHNO");
                }
                else
                {
                    spanAddress.Visible = true;
                    divStudPanel.Visible = false;
                    divShow.Visible = false;
                    objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "P.PSTATUS='Y'", "FNAME");
                    
                }               
               
                // ddlMember.SelectedValue = ds.Tables[0].Rows[0]["USERID"].ToString();
                ddlMember.SelectedValue = ds.Tables[0].Rows[0]["USERID"].ToString();
                string BranchNo = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_BRANCH  B ON (S.BRANCHNO=B.BRANCHNO)", "S.BRANCHNO", "IDNO=" + ddlMember.SelectedValue + "");
                string DegreNo = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE  D ON (S.DEGREENO=D.DEGREENO)", "S.DEGREENO", "IDNO=" + ddlMember.SelectedValue + "");
                string SemNo = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_SEMESTER  SE ON (S.SEMESTERNO=SE.SEMESTERNO)", "S.SEMESTERNO", "IDNO=" + ddlMember.SelectedValue + "");
                if (rdblistMemberType.SelectedValue == "2")
                {
                    ddlBranch.SelectedValue = BranchNo;
                }
                ddlDegree.SelectedValue = DegreNo;
                ddlsemester.SelectedValue = SemNo;
               // imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + ds.Tables[0].Rows[0]["USERID"].ToString() + "&type=emp";
                imgEmpPhoto.ImageUrl = "~/Images/nophoto.jpg"; //Shaikh Juned 1-07-2022

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_SAME_ADDRESS"].ToString()))
                {
                    Chksame.Checked = true;
                }
                else
                {
                    Chksame.Checked = false;
                }

               
            }
            if (Usernumber == 0)
            {
                txtTitle.Enabled = true;
                txtFName.Enabled = true;
                txtMName.Enabled = true;
                txtLName.Enabled = true;
            }
            else
            {
                //txtTitle.Enabled = false;
                //txtFName.Enabled = false;
                //txtMName.Enabled = false;
                //txtLName.Enabled = false;
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            flag = "UPDATE";
            ImageButton btnEdit = sender as ImageButton;
            int MRSRNO = int.Parse(btnEdit.CommandArgument);
            ViewState["PK_CMEMBER"] = int.Parse(btnEdit.CommandArgument);
            objmas.PKID = MRSRNO;
            showdetails(MRSRNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Membermaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        spanAddress.Visible = true;
    }

    private bool checkMemberExist()
    {
        bool retVal = false;
        DataSet ds = null;
        if (ddlMember.SelectedIndex != 0)
        {
            ds = objCommon.FillDropDown("TBL_MM_MENBERDETAILS", "*", "", "USERID =" + ddlMember.SelectedValue, "PK_CMEMBER");
        }

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = true;
                ViewState["PK_CMEMBER"] = ds.Tables[0].Rows[0]["PK_CMEMBER"].ToString();
                flag = "UPDATE";
            }
        }
        return retVal;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (rdblistMemberType.SelectedValue == "1")
        {
            if (txtAddOfficial.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Address Line 1.');", true);
                return;
            }
        }
        if (txtPAN.Text.Length != 10 && txtPAN.Text!="")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Pan Number Is Invalid.. Please Enter Ten Digit Pan Number.');", true);
            return;
        }
        if (txtMobileTemp.Text.Length != 10)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mobile Number Is Invalid.. Please Enter Ten Digit Mobile Number.');", true);
            return;
        }
        if (txtPinOffci.Text != "")
        {
            if (txtPinOffci.Text.Length != 6)//---------------05/05/2023
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Pin Code Is Invalid.. Please Enter Six Digit Pin Code.');", true);
                return;
            }
        }
        if (txtPinPerm.Text != "")
        {
            if (txtPinPerm.Text.Length != 6)//---------------05/05/2023
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Pin Code Is Invalid.. Please Enter Six Digit Pin Code.');", true);
                return;
            }
        }
        if (txtMobilePerm.Text != "")
        {
            if (txtMobilePerm.Text.Length != 10)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mobile Number Is Invalid.. Please Enter Ten Digit Mobile Number.');", true);
                return;
            }
        }
        
        //if (!txtFrmDt.Text.Equals(string.Empty))
        //{
        //    if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
        //        txtFrmDt.Focus();
        //        return;
        //    }
        //}

        //int result;

        //if (!txtDOB.Text.Equals(string.Empty))
        //{
        //    if (Convert.ToDateTime(txtDOB.Text) < System.DateTime.Now)
        //    {
        //        objmas.DOB = Convert.ToDateTime(txtDOB.Text.Trim());
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(this.updActivity, "Date of Birth should be smaller than Current date.", this.Page);
        //        txtDOB.Focus();
        //        return;
        //    }
        //}
        //else
        //{
        //    objmas.DOB = DateTime.MinValue;
        //}

        //if (ddlMember.SelectedIndex == 0 && txtMName.Text == string.Empty && txtLName.Text == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Middle Name & Last Name.');", true);
        //    return;
        //}

        //if (flag == "ADD")
        //{
        //    if (checkMemberExist())
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Member Already Exist.');", true);
        //        return;

        //    }
        //    else
        //    {
        //        objmas.PKID = 0;
        //        objmas.QTYPE = 1;
        //        result = ojccon.AddUpdate_MEMBER_Details(objmas);
        //    }
        //}
        //if (flag == "UPDATE")
        //{
        //    objmas.PKID = Convert.ToInt32(ViewState["PK_CMEMBER"].ToString());
        //    objmas.QTYPE = 2;
        //     result = ojccon.AddUpdate_MEMBER_Details(objmas);
        //}

        //flag = "ADD";
        //clear();
        //if (result > 0)
        //{
        //    bindlist();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error In Record Saving.');", true);
        //}


        objmas.TITLE = txtTitle.Text.Trim();
        objmas.FNAME = txtFName.Text.Trim();
        objmas.MNAME = txtMName.Text.Trim();
        objmas.LNAME = txtLName.Text.Trim();

        objmas.PROFESSION = txtProfession.Text.Trim();
        objmas.ACAD_QUALI = txtAcadQuali.Text.Trim();
        objmas.PAN_NO = txtPAN.Text.Trim();

        if (txtFrmDt.Text != string.Empty)
        {
            objmas.FROM_DATE = Convert.ToDateTime(txtFrmDt.Text.Trim());
        }
        else
        {
            objmas.FROM_DATE = DateTime.MinValue; //Convert.ToDateTime(DBNull.Value);
        }
        // objmas.FROM_DATE = Convert.ToDateTime(txtFrmDt.Text.Trim());
        //objmas.TO_DATE = Convert.ToDateTime(txtToDt.Text.Trim());

        if (txtToDt.Text != string.Empty)
        {
            objmas.TO_DATE = Convert.ToDateTime(txtToDt.Text.Trim());
        }
        else
        {
            objmas.TO_DATE = DateTime.MinValue; // Convert.ToDateTime(DBNull.Value);
        }
        objmas.Representation = txtRepresentation.Text.Trim();
        objmas.designation = txtDesignation.Text.Trim();
        objmas.CONSTITUENCY = txtConstituency.Text.Trim();

        objmas.T_ADDRESS = txtAddOfficial.Text.Trim();
        objmas.T_ADDLINE = txtAddLineOfficial.Text.Trim();
        objmas.T_CITY = ddlCityTemp.SelectedIndex == 0 ? string.Empty : ddlCityTemp.SelectedItem.Text;
        objmas.T_STATE = ddlStateTemp.SelectedIndex == 0 ? string.Empty : ddlStateTemp.SelectedItem.Text;
        objmas.T_PIN = txtPinOffci.Text.Trim();
        objmas.T_COUNTRY = ddlCountryTemp.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlCountryTemp.SelectedValue);
        objmas.T_PHONE = txtPhoneOffc.Text.Trim();
        objmas.T_MOBILE = txtMobileTemp.Text.Trim();
        objmas.T_EMAIL = txtoffEmail.Text.Trim();

        objmas.P_ADDRESS = txtAddPerm.Text.Trim();
        objmas.P_ADDLINE = txtAddLinePerm.Text.Trim();
        objmas.P_CITY = ddlCityPerm.SelectedIndex == 0 ? string.Empty : ddlCityPerm.SelectedItem.Text;
        objmas.P_STATE = ddlStatePerm.SelectedIndex == 0 ? string.Empty : ddlStatePerm.SelectedItem.Text;
        objmas.P_PIN = txtPinPerm.Text.Trim();
        objmas.P_COUNTRY = ddlCountryPerm.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlCountryPerm.SelectedValue);
        objmas.P_PHONE = txtPhonePerm.Text.Trim();
        objmas.P_MOBILE = txtMobilePerm.Text.Trim();
        objmas.P_EMAIL = txtperemail.Text.Trim();

        if (txtJoiningDate.Text != string.Empty)
        {
            objmas.JOINING_DATE = Convert.ToDateTime(txtJoiningDate.Text.Trim());
        }
        else
        {
            objmas.JOINING_DATE = DateTime.MinValue;
        }


        objmas.DEPTNO = Convert.ToInt32(Session["userEmpDeptno"]);
        if (Chksame.Checked)
        {
            objmas.IS_SAME_ADDRESS = Chksame.Checked;
        }
        else
        {
        }

        if (ddlMember.SelectedValue == null || ddlMember.SelectedValue == "")
        {
            objmas.USERID = Convert.ToInt32(0);
        }
        else
        {
            objmas.USERID = Convert.ToInt32(ddlMember.SelectedValue);
        }

        objmas.MEMBER_TYPE = Convert.ToInt32(rdblistMemberType.SelectedValue);// Either 1: means employee OR 2 : means student 


        if (ViewState["PK_CMEMBER"] == null)
        {
            if (checkMemberExist())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Member Already Exist.');", true);
                clear();
                return;
            }
            else
            {
                objmas.PKID = 0;
                objmas.QTYPE = 1;
                ojccon.AddUpdate_MEMBER_Details(objmas);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                clear();
                bindlist();
            }
        }
        else
        {
            objmas.PKID = Convert.ToInt32(ViewState["PK_CMEMBER"].ToString());
            objmas.QTYPE = 2;
            ojccon.AddUpdate_MEMBER_Details(objmas);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
            clear();
            bindlist();
        }






    }


    protected void Chksame_CheckedChanged(object sender, EventArgs e)
    {
        if (Chksame.Checked == true)
        {
            txtAddPerm.Text = txtAddOfficial.Text;
            txtAddLinePerm.Text = txtAddLineOfficial.Text;
            ddlCityPerm.SelectedValue = ddlCityTemp.SelectedValue;
            txtperemail.Text = txtoffEmail.Text;
            txtPhonePerm.Text = txtPhoneOffc.Text;
            txtPinPerm.Text = txtPinOffci.Text;
            ddlStatePerm.SelectedValue = ddlStateTemp.SelectedValue;
            ddlCountryPerm.SelectedValue = ddlCountryTemp.SelectedValue;
            txtMobilePerm.Text = txtMobileTemp.Text;

            if (ddlMember.SelectedIndex == 0)
            {
                txtTitle.Enabled = true;
                txtFName.Enabled = true;
                txtMName.Enabled = true;
                txtLName.Enabled = true;
            }
            else
            {
                //txtTitle.Enabled = false;
                //txtFName.Enabled = false;
                //txtMName.Enabled = false;
                //txtLName.Enabled = false;
            }
        }
        else
        {
            txtAddPerm.Text = string.Empty;
            txtperemail.Text = string.Empty;
            txtPhonePerm.Text = string.Empty;
            txtPinPerm.Text = string.Empty;
            txtAddLinePerm.Text = string.Empty;
            ddlCityPerm.SelectedIndex = 0;
            ddlStatePerm.SelectedIndex = 0;
            ddlCountryPerm.SelectedIndex = 0;
            txtMobilePerm.Text = string.Empty;

            if (ddlMember.SelectedIndex == 0)
            {
                txtTitle.Enabled = true;
                txtFName.Enabled = true;
                txtMName.Enabled = true;
                txtLName.Enabled = true;
            }
            else
            {
                //txtTitle.Enabled = false;
                //txtFName.Enabled = false;
                //txtMName.Enabled = false;
                //txtLName.Enabled = false;
            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void lnkMemberDetailsbtn_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        ViewState["PK_CMEMBER"] = lnk.CommandArgument;
        ShowReportMemberDetail("pdf", "MemberDetailsByMemberId.rpt");
    }

    private void ShowReportMemberDetail(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=MemberDetails.pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;

            url += "&param=@P_MEMBER_ID=" + Convert.ToString(ViewState["PK_CMEMBER"]);




            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Membermaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember.SelectedIndex > 0)
        {
            DataSet ds = null;
            if (rdblistMemberType.SelectedValue == "1")
            {
                ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDESIG D ON (E.SUBDESIGNO = D.SUBDESIGNO)", "E.*", "D.SUBDESIG", "E.IDNO=" + ddlMember.SelectedValue + "", "");
                //imgEmpPhoto.ImageUrl = "~/Images/nophoto.jpg"; //Shaikh Juned 01-07-2022
            }
            else
            {
                ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO, REGNO, ENROLLNO, STUDNAME AS FNAME, STUDLASTNAME AS LNAME, FATHERNAME AS MNAME, ADDHARCARDNO AS NUNIQUEID", "STUDENTMOBILE, DOB, EMAILID, '' AS TITLE, '' as SUBDESIG , '' as PAN_NO, '' as DOJ, '' as RESADD1, '' as TOWNADD1, STUDENTMOBILE as PHONENO, EMAILID ", "IDNO=" + ddlMember.SelectedValue, "");
            }

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                    txtFName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                    txtMName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                    txtLName.Text = ds.Tables[0].Rows[0]["LNAME"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    txtAdharNo.Text = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    if (rdblistMemberType.SelectedValue == "1")
                    {
                      
                            imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + ds.Tables[0].Rows[0]["idno"].ToString() + "&type=emp"; 
                       
                    }
                    else
                    {
                        imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + ds.Tables[0].Rows[0]["idno"].ToString() + "&type=STUDENT";
                    }
                    //txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                    //txtFName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                    //txtMName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                    //txtLName.Text = ds.Tables[0].Rows[0]["LNAME"].ToString();
                    //txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    //txtProfession.Text = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    txtDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                    //txtAcadQuali = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    //txtFrmDt = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    //txtToDt.Text = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    txtPAN.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                    //txtRepresentation.Text = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    //txtDesignation.Text = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                    txtJoiningDate.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    txtAddOfficial.Text = ds.Tables[0].Rows[0]["RESADD1"].ToString();
                    txtAddLineOfficial.Text = ds.Tables[0].Rows[0]["TOWNADD1"].ToString();
                    //ddlCityPerm.SelectedIndex=
                    txtMobileTemp.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                    txtoffEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                    //txtTitle.Enabled = false;
                    //txtFName.Enabled = false;
                    //txtMName.Enabled = false;
                    //txtLName.Enabled = false;
                    //txtDOB.Enabled = false;
                    //txtAdharNo.Enabled = false;
                }
            }
        }
        else if (ddlMember.SelectedIndex == 0)
        {
            imgEmpPhoto.ImageUrl = "~/Images/nophoto.jpg"; //Shaikh Juned 25-06-2022
            txtTitle.Text = string.Empty;
            txtFName.Text = string.Empty;
            txtMName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtAdharNo.Text = string.Empty;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "P.PSTATUS='Y' AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "FNAME"); // E.STAFFNO in (1,2,5) AND 
        //objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "P.PSTATUS='Y'", "FNAME"); // E.STAFFNO in (1,2,5) AND 
    }
    protected void rdblistMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            clear();
            if (rdblistMemberType.SelectedValue == "1")
            {
                spanAddress.Visible = true;
                imgEmpPhoto.ImageUrl = "~/Images/nophoto.jpg"; //Shaikh Juned 25-06-2022
                divStudPanel.Visible = false;
                divShow.Visible = false;
                objCommon.FillDropDownList(ddlMember, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(FNAME, '')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS FULLNAME", "P.PSTATUS='Y'", "FNAME");
            }
            else
            {
                spanAddress.Visible = true;
                imgEmpPhoto.ImageUrl = "~/Images/nophoto.jpg";
                divStudPanel.Visible = true;
                divShow.Visible = true;
                objCommon.FillDropDownList(ddlMember, "ACD_STUDENT", "IDNO", "(STUDNAME+' '+ISNULL(STUDLASTNAME,'')) AS FULLNAME", "ISNULL(ADMCAN,0)=0 ", "STUDNAME");
                //back.Visible = true;
            }
            bindlist();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Membermaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "A.LONGNAME", "B.DEGREENO=" + ddlDegree.SelectedValue + " AND A.BRANCHNO>0", "A.BRANCHNO");

        //int deptno= objCommon.LookUp("USER_ACC","")
        DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN (SELECT VALUE FROM DBO.SPLIT('" + Session["userdeptno"].ToString() + "',','))", "A.LONGNAME");
        string BranchNos = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
        }
        DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
        //on faculty login to get only those dept which is related to logged in faculty
        objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
        ddlBranch.Focus();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlMember, "ACD_STUDENT", "IDNO", "STUDNAME AS FULLNAME", "ISNULL(ADMCAN,0)=0 ", "STUDNAME");
            objCommon.FillDropDownList(ddlMember, "ACD_STUDENT", "IDNO", "(STUDNAME+' '+ISNULL(STUDLASTNAME,'')) STUDNAME", "(BRANCHNO=" + ddlBranch.SelectedValue + " OR " + ddlBranch.SelectedValue + "=0) AND (DEGREENO=" + ddlDegree.SelectedValue + " OR " + ddlDegree.SelectedValue + "= 0) AND ISNULL(ADMCAN,0)=0 AND (SEMESTERNO=" + ddlsemester.SelectedValue + " OR " + ddlsemester.SelectedValue + "= 0)", "REGNO, STUDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Membermaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void back_Click(object sender, EventArgs e)
    {
        clear();
        rdblistMemberType.SelectedValue = "1";
        divStudPanel.Visible = false;
        divShow.Visible = false;
    }
}
