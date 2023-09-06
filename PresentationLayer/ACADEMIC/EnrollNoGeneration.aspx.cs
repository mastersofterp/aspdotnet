//======================================================================================
// PROJECT NAME   : UAIMS [GHREC]                                                         
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : Registration No. Generation
// CREATION DATE  : 06-JULY-2011                                                          
// CREATED BY     : NIRAJ D. PHALKE                                                   
// MODIFIED DATE  : 01-05-2019                                                                     
// MODIFIED DESC  : M. REHBAR SHEIKH                                                                     
//======================================================================================

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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class ACADEMIC_EnrollNoGeneration : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentRegistration objRegistration = new StudentRegistration();
    ModuleConfigController objMConfig = new ModuleConfigController();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        }

                    this.FillDropdown();
                    //LblHear.Text = "PRN NUMBER GENERATION";
                    if (radRegNoGen.Checked)
                        {
                        radRegNoGen_CheckedChanged(sender, e);
                        }
                    btnReport.Enabled = false;
                    //ddlAdmBatch.SelectedIndex = 0;
                    CheckConfig(sender, e);
                    this.Load();
                    //radRollNoGen_CheckedChanged(sender, e);
                    }
                }
            // this.BindListView();
            }
        catch
            {
            throw;
            }
        }
    private void CheckConfig(object sender, EventArgs e)
        {
        try
            {
            DataSet ds = objMConfig.GetModuleConfigData();
            if (ds.Tables != null)
                {
                if (ds.Tables[0].Rows.Count > 0)
                    {

                    if (ds.Tables[0].Rows[0]["ALLOW_ENROLLNO"].ToString() == "1")// && ds.Tables[0].Rows[0]["ALLOW_ROLLNO"].ToString() == "0" && ds.Tables[0].Rows[0]["ALLOW_ENROLLNO"].ToString() == "0")
                        {
                        radRollNoGen.Visible = false;
                        radRegNoGen.Visible = false;
                        }
                    if (ds.Tables[0].Rows[0]["ALLOW_ROLLNO"].ToString() == "1")// && ds.Tables[0].Rows[0]["ALLOW_ROLLNO"].ToString() == "0" && ds.Tables[0].Rows[0]["ALLOW_ENROLLNO"].ToString() == "0")
                        {
                        radRollNoGen.Visible = true;
                        radRegNoGen.Checked = false;
                        radRollNoGen.Checked = true;
                        radRollNoGen_CheckedChanged(sender, e);
                        }
                    else
                        {
                        radRollNoGen.Visible = false;
                        }
                    if (ds.Tables[0].Rows[0]["ALLOW_REGNO"].ToString() == "1")// && ds.Tables[0].Rows[0]["ALLOW_ROLLNO"].ToString() == "0" && ds.Tables[0].Rows[0]["ALLOW_ENROLLNO"].ToString() == "0")
                        {
                        pnlShow.Visible = true;
                        //radRollNoGen.Visible = false;
                        radRegNoGen.Visible = true;
                        radRegNoGen.Checked = true;
                        radRollNoGen.Checked = false;
                        radRegNoGen_CheckedChanged(sender, e);
                        if (Convert.ToInt32(Session["OrgId"]) == 1) // RCPIT
                            {
                            radRegNoGen.Text = "PRN Number Generation";
                            btnGenerateRR.Visible = false;
                            btnGenRegNo.Visible = true;
                            DivYear.Visible = true;//added by Dileep Kare on 13.04.2022 as per testing bug.
                            DivSem.Visible = false;//added by Dileep Kare on 13.04.2022 as per testing bug.
                            }
                        if (Convert.ToInt32(Session["OrgId"]) == 2 || Convert.ToInt32(Session["OrgId"]) == 16) // Crescent
                            {
                            radRegNoGen.Text = "RR Number Generation";
                            btnGenerateRR.Visible = true;
                            btnGenRegNo.Visible = false;
                            DivYear.Visible = false;//added by Dileep Kare on 13.04.2022 as per testing bug.
                            DivSem.Visible = true;  //added by Dileep Kare on 13.04.2022 as per testing bug.
                            }
                        Divsection.Visible = false;
                        //DivYear.Visible = false;//commented by Dileep Kare on 13.04.2022 as per testing bug.
                        //DivSem.Visible = true;  //commented by Dileep Kare on 13.04.2022 as per testing bug.
                        }
                    else
                        {
                        radRegNoGen.Visible = false;
                        }
                    }
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.CheckConfig-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    private void CheckPageAuthorization()
        {
        if (Request.QueryString["pageno"] != null)
            {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                Response.Redirect("~/notauthorized.aspx?page=RegistraionNoGeneration.aspx");
                }
            }
        else
            {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RegistraionNoGeneration.aspx");
            }
        }

    private void FillDropdown()
        {
        try
            {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlidtype, "ACD_IDTYPE WITH (NOLOCK)", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0 AND ISNULL(ACTIVESTATUS,0)=1", "IDTYPENO");
            //added by prafull on date 281021
            objCommon.FillDropDownList(ddlyear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0", "YEAR");
            objCommon.FillDropDownList(ddlsort, "ACD_REGNO_GENRATION", "ID", "NAME", "ID>0", "ID");
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_RegistraionNoGeneration.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }

    private void BindListView()
        {
        DataSet dsStudent = null;

        //commented by Prafull 11112021 
        if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 1)
            {
            if (radRollNoGen.Checked) // Class roll no. generation
                {
                //Comment by Mahesh on Dated 21-01-2021 Due to not required IDType in Makaut project.
                //int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));
                //int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
                //int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));


                //int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));


                int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0 AND A.CAN = 0", "A.REGNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvlrollno.DataSource = dsStudent.Tables[0];
                    lvlrollno.DataBind();
                    lvlrollno.Visible = true;



                    if (StudCountflag != RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else if (StudCountflag == RollCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = true;

                        }
                    }
                else
                    {
                    lvlrollno.DataSource = null;
                    lvlrollno.DataBind();
                    lvlrollno.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenerateRoll.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            if (radRegNoGen.Checked)// Regno. generation
                {
                //added by prafull
                //int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));


                int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));




                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue +   "AND A.ADMCAN = 0 AND A.CAN = 0", " A.STUDNAME");

                if (ddlsort.SelectedIndex == 1)
                    {
                    dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0 AND A.CAN = 0", " A.STUDNAME");
                    }
                else
                    {
                    dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0 AND A.CAN = 0", " A.MERITNO");
                    }
                //int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));
                //int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
                //int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue +  "  AND A.ADMCAN = 0 AND A.CAN = 0", "A.REGNO");

                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvStudents.DataSource = dsStudent.Tables[0];
                    lvStudents.DataBind();
                    lvStudents.Visible = true;


                    if (StudCountflag != RegCountflag)
                        {
                        //objCommon.DisplayMessage(this.UpdatePanel1, "Registration No. should be generated first for all the student(s).", this.Page);
                        btnGenRegNo.Enabled = true;
                        btnGenRegNo.Visible = true;
                        return;
                        }
                    else if (StudCountflag == RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                        btnGenRegNo.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenRegNo.Visible = true;
                        btnGenRegNo.Enabled = true;
                        }
                    }
                else
                    {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    lvStudents.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenRegNo.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            }

        else if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 16)
            {
            if (radRollNoGen.Checked) // Class roll no. generation
                {
                int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0 AND A.CAN = 0", "A.REGNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvlrollno.DataSource = dsStudent.Tables[0];
                    lvlrollno.DataBind();
                    lvlrollno.Visible = true;

                    if (StudCountflag != RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else if (StudCountflag == RollCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = true;

                        }
                    }
                else
                    {
                    lvlrollno.DataSource = null;
                    lvlrollno.DataBind();
                    lvlrollno.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenerateRoll.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            if (radRegNoGen.Checked)// Regno. generation
                {
                //added by rohit on 24_05_2023

                int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                if (ddlsort.SelectedIndex == 1 && ddlgender.SelectedIndex == 1)
                    {
                    string Gender = string.Empty;
                    if (rdbgender.SelectedValue == "1")
                        {
                        Gender = "M";

                        }
                    else if (rdbgender.SelectedValue == "2")
                        {
                        Gender = "F";
                        }
                    else
                        {
                        Gender = "0";
                        }

                    dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.REGNO,IDTYPE,A.ROLLNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND (A.SEMESTERNO=" + ddlsemester.SelectedValue + "  OR 0=0) AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0 AND (A.SEX='" + Gender + "' OR '" + Gender + "'='0')", "A.REGNO");//AND ISNULL(A.REGNO,'')=''
                    }

                else
                    {
                    dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0 AND A.CAN = 0", " A.MERITNO");
                    }

                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvStudList.DataSource = dsStudent.Tables[0];
                    lvStudList.DataBind();
                    lvStudList.Visible = true;

                    if (StudCountflag != RegCountflag)
                        {
                        //objCommon.DisplayMessage(this.UpdatePanel1, "Registration No. should be generated first for all the student(s).", this.Page);
                        btnGenRegNo.Enabled = false;
                        btnGenRegNo.Visible = false;
                        btnGenerateRR.Enabled = true;

                        return;
                        }
                    else if (StudCountflag == RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                        btnGenRegNo.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenRegNo.Visible = true;
                        btnGenRegNo.Enabled = true;
                        }
                    }
                else
                    {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    lvStudents.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenRegNo.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            }

        else if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 7)
            {
            btnGenRegNo.Visible = false;
            btnGenerateRR.Visible = true;
            btnGenerateRR.Enabled = true;

            if (radRollNoGen.Checked) // Class roll no. generation
                {
                btnGenerateRR.Visible = false;
                int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0 AND A.CAN = 0", "A.REGNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvlrollno.DataSource = dsStudent.Tables[0];
                    lvlrollno.DataBind();
                    lvlrollno.Visible = true;



                    if (StudCountflag != RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else if (StudCountflag == RollCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = true;

                        }
                    }
                else
                    {
                    lvlrollno.DataSource = null;
                    lvlrollno.DataBind();
                    lvlrollno.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenerateRoll.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            if (radRegNoGen.Checked)// Regno. generation
                {
                int RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                int StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                if (ddlsort.SelectedIndex == 1)
                    {

                    dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0 AND A.CAN = 0", " A.STUDNAME");
                    }
                else
                    {
                    dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0 AND A.CAN = 0", " A.MERITNO");
                    }

                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvStudents.DataSource = dsStudent.Tables[0];
                    lvStudents.DataBind();
                    lvStudents.Visible = true;


                    if (StudCountflag != RegCountflag)
                        {

                        return;
                        }
                    else if (StudCountflag == RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                        btnGenRegNo.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenRegNo.Visible = true;
                        btnGenRegNo.Enabled = true;
                        }
                    }
                else
                    {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    lvStudents.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenRegNo.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            }

        else if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 15)
            {

            btnGenRegNo.Visible = false;
            btnGenerateRR.Visible = true;
            btnGenerateRR.Enabled = true;
            int RegCountflag=0;
            int StudCountflag=0;
            if (radRollNoGen.Checked) // Class roll no. generation
                {


                btnGenerateRR.Visible = false;
                 RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                 StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 "));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));


                dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue),Convert.ToInt32(ddlyear.SelectedValue));

                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0", "A.REGNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvlrollno.DataSource = dsStudent.Tables[0];
                    lvlrollno.DataBind();
                    lvlrollno.Visible = true;
                    //if (StudCountflag != RegCountflag)
                    //    {
                    //    objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                    //    btnGenerateRoll.Visible = true;
                    //    btnGenerateRoll.Enabled = false;
                    //    return;
                    //    }
                    //else 
                    if (StudCountflag == RollCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = true;

                        }
                    }
                else
                    {
                    lvlrollno.DataSource = null;
                    lvlrollno.DataBind();
                    lvlrollno.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenerateRoll.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            if (radRegNoGen.Checked)// Regno. generation
                {
                 RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                 StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                if (ddlsort.SelectedIndex == 1)
                    {

                    dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                    //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.STUDNAME");
                    }
                }
                else
                    {
                    dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                    //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.MERITNO");
                    }

                    if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                        lvStudents.DataSource = dsStudent.Tables[0];
                        lvStudents.DataBind();
                        lvStudents.Visible = true;


                        if (StudCountflag != RegCountflag)
                            {

                            return;
                            }
                        else if (StudCountflag == RegCountflag)
                            {
                            objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                            btnGenRegNo.Enabled = false;
                            return;
                            }
                        else
                            {
                            btnGenRegNo.Visible = true;
                            btnGenRegNo.Enabled = true;
                            }
                        }
                    else
                        {
                        lvStudents.DataSource = null;
                        lvStudents.DataBind();
                        lvStudents.Visible = false;
                        objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                        btnGenRegNo.Visible = false;
                        btnReport.Enabled = false;
                        }
                    }

        else if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 18)
            {
            btnGenRegNo.Visible = false;
            btnGenerateRR.Visible = true;
            btnGenerateRR.Enabled = true;
            int RegCountflag = 0;
            int StudCountflag = 0;
            if (radRollNoGen.Checked) // Class roll no. generation
                {


                btnGenerateRR.Visible = false;
                RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 "));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));


                dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));

                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0", "A.REGNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvlrollno.DataSource = dsStudent.Tables[0];
                    lvlrollno.DataBind();
                    lvlrollno.Visible = true;
                    //if (StudCountflag != RegCountflag)
                    //    {
                    //    objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                    //    btnGenerateRoll.Visible = true;
                    //    btnGenerateRoll.Enabled = false;
                    //    return;
                    //    }
                    //else 
                    if (StudCountflag == RollCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = true;

                        }
                    }
                else
                    {
                    lvlrollno.DataSource = null;
                    lvlrollno.DataBind();
                    lvlrollno.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenerateRoll.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            if (radRegNoGen.Checked)// Regno. generation
                {
                RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                if (ddlsort.SelectedIndex == 1)
                    {

                    dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                    //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.STUDNAME");
                    }
                }
            else
                {
                dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.MERITNO");
                }

            if (dsStudent.Tables[0].Rows.Count > 0)
                {
                lvStudents.DataSource = dsStudent.Tables[0];
                lvStudents.DataBind();
                lvStudents.Visible = true;


                if (StudCountflag != RegCountflag)
                    {

                    return;
                    }
                else if (StudCountflag == RegCountflag)
                    {
                    objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                    btnGenRegNo.Enabled = false;
                    btnGenerateRR.Enabled = false;
                    
                    return;
                    }
                else
                    {
                    btnGenRegNo.Visible = true;
                    btnGenRegNo.Enabled = true;
                    }
                }
            else
                {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = false;
                objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                btnGenRegNo.Visible = false;
                btnReport.Enabled = false;
                }
            }

        else if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 11)
            {
            btnGenRegNo.Visible = false;
            btnGenerateRR.Visible = true;
            btnGenerateRR.Enabled = true;
            int RegCountflag = 0;
            int StudCountflag = 0;
            if (radRollNoGen.Checked) // Class roll no. generation
                {


                btnGenerateRR.Visible = false;
                RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 "));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));


                dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));

                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0", "A.REGNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvlrollno.DataSource = dsStudent.Tables[0];
                    lvlrollno.DataBind();
                    lvlrollno.Visible = true;
                    //if (StudCountflag != RegCountflag)
                    //    {
                    //    objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                    //    btnGenerateRoll.Visible = true;
                    //    btnGenerateRoll.Enabled = false;
                    //    return;
                    //    }
                    //else 
                    if (StudCountflag == RollCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                        btnGenerateRoll.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenerateRoll.Visible = true;
                        btnGenerateRoll.Enabled = true;

                        }
                    }
                else
                    {
                    lvlrollno.DataSource = null;
                    lvlrollno.DataBind();
                    lvlrollno.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenerateRoll.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            if (radRegNoGen.Checked)// Regno. generation
                {
                RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0"));

                int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                if (ddlsort.SelectedIndex == 1)
                    {

                    dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                    //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.STUDNAME");
                    }
                }
            else
                {
                dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.MERITNO");
                }

            if (dsStudent.Tables[0].Rows.Count > 0)
                {
                lvStudents.DataSource = dsStudent.Tables[0];
                lvStudents.DataBind();
                lvStudents.Visible = true;


                if (StudCountflag != RegCountflag)
                    {

                    return;
                    }
                else if (StudCountflag == RegCountflag)
                    {
                    objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                    btnGenRegNo.Enabled = false;
                    btnGenerateRR.Enabled = false;

                    return;
                    }
                else
                    {
                    btnGenRegNo.Visible = true;
                    btnGenRegNo.Enabled = true;
                    }
                }
            else
                {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = false;
                objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                btnGenRegNo.Visible = false;
                btnReport.Enabled = false;
                }
            }



                
            else
                {
                btnGenRegNo.Visible = false;
                btnGenerateRR.Visible = true;
                btnGenerateRR.Enabled = true;
                int RegCountflag = 0;
                int StudCountflag = 0;
                if (radRollNoGen.Checked) // Class roll no. generation
                    {


                    btnGenerateRR.Visible = false;
                    RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                    StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 "));

                    int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));


                    dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));

                    //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + " AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "  and A.SECTIONNO=" + ddlSection.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + " AND A.ADMCAN = 0", "A.REGNO");
                    if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                        lvlrollno.DataSource = dsStudent.Tables[0];
                        lvlrollno.DataBind();
                        lvlrollno.Visible = true;
                        //if (StudCountflag != RegCountflag)
                        //    {
                        //    objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number should be generated first for all the student(s).", this.Page);
                        //    btnGenerateRoll.Visible = true;
                        //    btnGenerateRoll.Enabled = false;
                        //    return;
                        //    }
                        //else 
                        if (StudCountflag == RollCountflag)
                            {
                            objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. is already generated.", this.Page);
                            btnGenerateRoll.Enabled = false;
                            return;
                            }
                        else
                            {
                            btnGenerateRoll.Visible = true;
                            btnGenerateRoll.Enabled = true;

                            }
                        }
                    else
                        {
                        lvlrollno.DataSource = null;
                        lvlrollno.DataBind();
                        lvlrollno.Visible = false;
                        objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                        btnGenerateRoll.Visible = false;
                        btnReport.Enabled = false;
                        }
                    }
                if (radRegNoGen.Checked)// Regno. generation
                    {
                    RegCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

                    StudCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND ISNULL(A.ADMCAN,0) = 0"));

                    int RollCountflag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));

                    if (ddlsort.SelectedIndex == 1)
                        {

                        dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                        //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.STUDNAME");
                        }
                    }
                else
                    {
                    dsStudent = GetStudentsDAIICT(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
                    //dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", " DISTINCT A.STUDNAME", "A.IDNO,A.REGNO,IDTYPE,A.ROLLNO,A.MERITNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + " AND IDTYPE = " + ddlidtype.SelectedValue + "AND A.ADMCAN = 0", " A.MERITNO");
                    }

                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    lvStudents.DataSource = dsStudent.Tables[0];
                    lvStudents.DataBind();
                    lvStudents.Visible = true;


                    if (StudCountflag != RegCountflag)
                        {

                        return;
                        }
                    else if (StudCountflag == RegCountflag)
                        {
                        objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number is already generated.", this.Page);
                        btnGenRegNo.Enabled = false;
                        return;
                        }
                    else
                        {
                        btnGenRegNo.Visible = true;
                        btnGenRegNo.Enabled = true;
                        }
                    }
                else
                    {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    lvStudents.Visible = false;
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found for selected criteria.", this.Page);
                    btnGenRegNo.Visible = false;
                    btnReport.Enabled = false;
                    }
                }
            }
        

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        //btnGenerateRoll.Visible = false;
        btnReport.Enabled = false;
        ddlBranch.ClearSelection();
        ddlidtype.ClearSelection();
        if (ddlDegree.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 and B.DEGREENO=" + ddlDegree.SelectedValue + " ", "A.LONGNAME");
            }
        else
            {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please select degree", this.Page);
            }
        }

    protected void btnClear0_Click(object sender, EventArgs e)
        {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        ddlBranch.SelectedIndex = 0;
        }
        
    protected void btnShow_Click(object sender, EventArgs e)
        {
        this.BindListView();
        btnReport.Enabled = true;
        }

    //commented by prafull muke



    protected void btnClear0_Click1(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }

    protected void btnReport_Click(object sender, EventArgs e)
        {
        ShowReport("enrollnoreport", "rptStudentEnrollNoGeneration.rpt");
        }

    private void ShowReport(string reportTitle, string rptFileName)
        {
        try
            {
            int flag = 0;
            int idtype = 0;

            //commented by Prafull 

            //if (radRollNoGen.Checked) // Class roll no. generation
            //{
            //    flag = 2;
            //    idtype = 1;

            //}
            //else
            //if (radRegNoGen.Checked) 
            //{
            //    flag =1;
            //    idtype = 0;
            //}
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlClgname.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue);

            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);

            url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_IDTYPE=" + idtype + ",@P_FLAG=" + flag + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }

    protected void ddlClgname_SelectedIndexChanged1(object sender, EventArgs e)
        {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        //btnGenerateRoll.Visible = false;
        btnReport.Enabled = false;
        ddlidtype.ClearSelection();
        ddlDegree.ClearSelection();
        ////ddlBranch.ClearSelection();
        ddlBranch.ClearSelection();
        if (ddlClgname.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK), ACD_COLLEGE_DEGREE C WITH (NOLOCK)", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.COLLEGE_ID=" + ddlClgname.SelectedValue + "", "DEGREENO");
            }
        else
            {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please select school/college name", this.Page);
            }
        }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            ddlClgname.ClearSelection();
            ddlDegree.ClearSelection();
            ////ddlDegree.Items.Insert(0, "Please Select");
            ddlBranch.ClearSelection();
            ddlidtype.ClearSelection();
            ////ddlBranch.Items.Insert(0, "Please Select");
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            //btnGenerateRoll.Enabled = false;

            lvStudList.DataSource = null;
            lvStudList.DataBind();

            btnReport.Enabled = false;
            if (ddlAdmBatch.SelectedIndex == 0)
                {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please select admission batch", this.Page);
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ddlAdmBatch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudList.DataSource = null;
            lvStudList.DataBind();

            lvStudents.Visible = false;
            //btnGenerateRoll.Enabled = false;
            btnReport.Enabled = false;
            ddlidtype.ClearSelection();
            if (ddlBranch.SelectedIndex == 0)
                {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please select Programme/Branch", this.Page);
                }
            else
                {
                objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_SEMESTER B WITH (NOLOCK) ON A.SEMESTERNO=b.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "A.SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)", "A.SEMESTERNO");
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ddlAdmBatch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }

    protected void Load()
        {
        pnlShow.Visible = true;
        buttonSection.Visible = true;
        //btnGenRegNo.Visible = true;
        // btnGenerateRoll.Visible = false;

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;

        ddlClgname.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlidtype.SelectedIndex = 0;
        }

    protected void radRollNoGen_CheckedChanged(object sender, EventArgs e)
        {

        //lblyear.Visible = false;
        //ddlyear.Visible = false;
        lvStudList.DataBind();
        lvStudList.DataSource = null;
        lvStudList.Visible = false;
        lvlrollno.DataBind();
        lvlrollno.DataSource = null;
        lvlrollno.Visible = false;
        pnlShow.Visible = true;
        buttonSection.Visible = true;

        btnGenerateRoll.Visible = true;

        btnGenerateRR.Visible = false;
        btnGenRegNo.Visible = false;
        radRollNoGen.Visible = true;

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;

        ddlClgname.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlidtype.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        Divsection.Visible = true;
        DivSem.Visible = true;
        DivYear.Visible = false;
        //LblHear.Text = "ROLL NUMBER GENERATION";
        }

    protected void btnGenRegNo_Click(object sender, EventArgs e)
        {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        bool flag1 = false;

        int flag;
        int flag2;
        int flag3;
        //int flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue +  "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO is null or A.REGNO='')"));


        if (ddlsort.SelectedIndex == 1)
            {
            flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
            flag2 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));
            flag3 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

            }
        else
            {

            flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
            flag2 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "and A.MERITNO=" + ddlsort.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));
            flag3 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "and A.MERITNO=" + ddlsort.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));


            }

        //added by prafull
        //int flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "and A.MERITNO=" + ddlsort.SelectedValue + "AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
        //int flag2 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "and A.MERITNO=" + ddlsort.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));
        //int flag3 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO INNER JOIN ACD_YEAR Y  ON A.YEAR=Y.YEAR", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "and A.YEAR=" + ddlyear.SelectedValue + "and A.MERITNO=" + ddlsort.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));



        //int flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue +  "AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
        //int flag2 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));
        //int flag3 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));



        if (flag != flag3)
            {
            //added by parfull
            //retStatus = objRegistration.GenereateEnrollmentNo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue),1);
            if (Session["OrgId"].ToString() == "7") //RAJAGIRI PRN Generation
                {

                retStatus = objRegistration.GenereateRRNoForRajagiri(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));

                }
            
            else
                {

                retStatus = objRegistration.GenereateEnrollmentNo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue));
                }
            if (retStatus == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number Generated Successfully!", this.Page);
                }
            else if (retStatus == Convert.ToInt32(CustomStatus.TransactionFailed))
                {
                objCommon.DisplayMessage(this.UpdatePanel1, "Error occurred!", this.Page);
                }
            }
        else if (flag == flag3)
            {
            flag1 = true;
            objCommon.DisplayMessage(this.UpdatePanel1, "PRN Number Already Generated!", this.Page);
            }

        this.BindListView();
        if (flag1 == true)
            {
            //btnGenerateRoll.Enabled = false;
            }
        }

    protected void ddlidtype_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (Convert.ToInt32(ddlidtype.SelectedValue) > 0)
            {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudents.Visible = true;
            }
        }

    protected void radRegNoGen_CheckedChanged(object sender, EventArgs e)
        {
        pnlShow.Visible = true;
        buttonSection.Visible = true;
        if (Convert.ToInt32(Session["OrgId"]) == 1) // RCPIT
            {
            btnGenRegNo.Visible = true;
            }
        if (Convert.ToInt32(Session["OrgId"]) == 2 || Convert.ToInt32(Session["OrgId"]) == 16) // Crescent
            {
            btnGenerateRR.Visible = true;
            }
        lvStudList.DataBind();
        lvStudList.DataSource = null;
        lvStudList.Visible = false;
        lvlrollno.DataBind();
        lvlrollno.DataSource = null;
        lvlrollno.Visible = false;

        //radRollNoGen.Visible = false;
        btnGenerateRoll.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        ddlClgname.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlidtype.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        Divsection.Visible = false;
        DivSem.Visible = false;
        DivYear.Visible = true;
        //LblHear.Text = "PRN NUMBER GENERATION";
        }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
        {
        lvStudList.DataSource = null;
        lvStudList.DataBind();

        if (ddlsemester.SelectedIndex > 0)
            {
            this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION WITH (NOLOCK)", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            ddlSection.Focus();
            }
        else
            {
            ddlSection.SelectedIndex = 0;
            ddlsemester.Focus();
            }
        }

    protected void btnGenerateRR_Click(object sender, EventArgs e)
        {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        string Gender = "0";
        if (rdbgender.SelectedValue == "1")
            {
            Gender = "M";
            }
        else if (rdbgender.SelectedValue == "2")
            {
            Gender = "F";
            }
        else
            {
            Gender = "0";
            }
        if (Session["OrgId"].ToString() != "6") // This condition not for RC piper
            {

            if (Session["OrgId"].ToString() == "7") //RAJAGIRI PRN Generation
                {
                retStatus = objRegistration.GenereateRRNoForRajagiri(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));
                }
            else if (Session["OrgId"].ToString() == "15")
                {
                retStatus = objRegistration.GenereateRRNoForDaiict(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));
                btnGenerateRR.Enabled = false;
                }
            else if (Session["OrgId"].ToString() == "18")
                {
                retStatus = objRegistration.GenereateRRNoForHITS(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));
                btnGenerateRR.Enabled = false;
                }
            else if (Session["OrgId"].ToString() == "11")
                {
                retStatus = objRegistration.GenereateRRNoForPRMCEM(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));
                btnGenerateRR.Enabled = false;
                }
            else if (Session["OrgId"].ToString() == "16")
                {
                retStatus = objRegistration.GenereateRRNoForMaher(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));
                btnGenerateRR.Enabled = false;
                }
            else
                {

                retStatus = objRegistration.GenereateRRNo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Gender.ToString());
                }
            }
        else
            {
            //RC Piper PRN Generation
            retStatus = objRegistration.GenereateRRNoForRcpiper(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue));
            }

        if (retStatus == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
            objCommon.DisplayMessage(this.UpdatePanel1, "RR Number Generated Successfully!", this.Page);

            if (Session["OrgId"].ToString() == "16")
                {
                btnGenerateRR.Enabled = false;
                }
            }
        else if (retStatus == Convert.ToInt32(CustomStatus.TransactionFailed))
            {
            objCommon.DisplayMessage(this.UpdatePanel1, "Error occurred!", this.Page);
            }
        this.BindListView();
        }
    protected void btnGenerateRoll_Click(object sender, EventArgs e)
        {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        bool flag1 = false;
        //int flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "count(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO is null or A.REGNO='')"));
        int flag = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "COUNT(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "AND A.SECTIONNO=" + ddlSection.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0"));
        int flag2 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "COUNT(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "AND A.SECTIONNO=" + ddlSection.SelectedValue + "AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.ROLLNO  is not null and A.ROLLNO <>'')"));
        int flag3 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "COUNT(1)", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + " and A.COLLEGE_ID=" + ddlClgname.SelectedValue + " and A.DEGREENO=" + ddlDegree.SelectedValue + " and A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.SEMESTERNO=" + ddlsemester.SelectedValue + "AND A.SECTIONNO=" + ddlSection.SelectedValue + " AND A.IDTYPE=" + ddlidtype.SelectedValue + "  AND ISNULL(A.ADMCAN,0) = 0 AND ISNULL(A.CAN,0) = 0 AND (A.REGNO  is not null and A.REGNO <>'')"));

        if (flag == flag3)
            {
            //retStatus = objRegistration.GenereateEnrollmentNo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlidtype.SelectedValue), 2);


            if (Session["OrgId"].ToString() == "7")
                {
                 retStatus = objRegistration.GenereateRollNo_Rajagiri(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue));
                }
            if (Session["OrgId"].ToString() == "15")
                {
                retStatus = objRegistration.GenereateRRNoForDaiict(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue),Convert.ToInt32(ddlyear.SelectedValue),Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue));
                }
            else
                {
            retStatus = objRegistration.GenereateRollNo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlidtype.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlsort.SelectedValue));
                }

            if (retStatus == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. Generated Successfully!", this.Page);
                }
            else if (retStatus == Convert.ToInt32(CustomStatus.TransactionFailed))
                {
                objCommon.DisplayMessage(this.UpdatePanel1, "Error occurred!", this.Page);
                }
            }
        else if (flag == flag2)
            {
            flag1 = true;
            objCommon.DisplayMessage(this.UpdatePanel1, "Roll No. Already Generated!", this.Page);
            }

        this.BindListView();
        if (flag1 == true)
            {
            btnGenerateRoll.Enabled = false;
            }
        }

    protected void ddlgender_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlgender.SelectedIndex > 0)
            {
            divgender.Visible = true;
            }
        else
            {
            divgender.Visible = false;
            rdbgender.ClearSelection();
            }
        }
    //protected void rdbgender_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //    string  Gender=string.Empty;
    //    if (rdbgender.SelectedValue == "1")
    //        {
    //        Gender = "M";

    //        }
    //    else
    //        {
    //        Gender = "F";
    //        }

    //    }

    public DataSet GetStudentsGenderWise(int Batch, int CollegeID, int Degreeno, int BranchNo, int Semester, int Section, int Idtype, string Gender)
        {
        DataSet ds = null;
        try
            {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objparams = new SqlParameter[8];
            objparams[0] = new SqlParameter("@P_ADMBATCH", Batch);
            objparams[1] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
            objparams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
            objparams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
            objparams[4] = new SqlParameter("@P_SEMESTERNO", Semester);
            objparams[5] = new SqlParameter("@P_SECTIONNO", Section);
            objparams[6] = new SqlParameter("@P_IDTYPE", Idtype);
            objparams[7] = new SqlParameter("@P_GENDER", Gender);
            ds = objsqlhelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_DETAILS_BY_GENDER", objparams);

            }
        catch (Exception ex)
            {
            throw new IITMSException("IITMS.NITPRM.BUSINESSLAYER.BUSINESSLOGIC.STUDENTCONTROLLER.GETSESSIONWISEMARKDETAILS -->" + ex.Message + " " + ex.StackTrace);
            }
        return ds;
        }

    public DataSet GetStudentsDAIICT(int Batch, int CollegeID, int Degreeno, int BranchNo, int Semester, int Section, int Idtype,int Year)
        {
        DataSet ds = null;
        try
            {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objparams = new SqlParameter[8];
            objparams[0] = new SqlParameter("@P_BATCHID", Batch);
            objparams[1] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
            objparams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
            objparams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
            objparams[4] = new SqlParameter("@P_SEMESTERNO", Semester);
            objparams[5] = new SqlParameter("@P_SECTIONNO", Section);
            objparams[6] = new SqlParameter("@P_IDTYPE", Idtype);
            objparams[7] = new SqlParameter("@P_YEARNO", Year);
            ds = objsqlhelper.ExecuteDataSetSP("PKG_GET_STUDENT_DETAILS_REGNO_DAIICT", objparams);

            }
        catch (Exception ex)
            {
            throw new IITMSException("IITMS.NITPRM.BUSINESSLAYER.BUSINESSLOGIC.STUDENTCONTROLLER.GETSESSIONWISEMARKDETAILS -->" + ex.Message + " " + ex.StackTrace);
            }
        return ds;
        }



    }
