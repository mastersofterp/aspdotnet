//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD STUDENT PROGRESS                                                     
// CREATION DATE : 20-MARCH-2013                                                          
// CREATED BY    : Dipali Nanore                          
// MODIFIED DATE :                 
// ADDED BY      :                                  
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_PhdStudProgress : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Sessionprogress = string.Empty;
    PhdController objEntityMethodClass = new PhdController();
    FeeCollectionController feeController = new FeeCollectionController();
    static int Result1 = 0;
    Label ReceptNo = new Label();
    HiddenField hdfSem = new HiddenField();

    StudentController objSC = new StudentController();


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
                //CheckPageAuthorization(); 

                //Set the Page Title
                string page="982";
                //Request.QueryString["pageno"] = page.ToString();
                //Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                //FillDropDown();
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                ViewState["Receiptcode"] = "TF";
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));  //--- change ua_idno by  ua_no 

                Sessionprogress = Session["currentsession"].ToString();  // commented by abdul 27072015
                if (Request.QueryString["pageno"] == null)  // added by abdul 27072015
                    {
                    objCommon.DisplayMessage("Unable to find page number!!", this.Page);
                    return;
                    }
                  page = Request.QueryString["pageno"].ToString();  // added by abdul 27072015
                if (page == "")  // added by abdul 27072015
                    {
                    objCommon.DisplayMessage("Unable to find page number!!", this.Page);  // added by abdul 27072015
                    return;
                    }
                //Sessionprogress = objCommon.LookUp("ACTIVITY_MASTER AM INNER JOIN [SESSION_ACTIVITY] SA ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "MAX(SA.SESSION_NO)", "PAGE_LINK like '%" + page + "%' AND STARTED=1 AND AM.ACTIVITY_CODE = 'PROGRESS'");
                if (Sessionprogress != "")
                    {
                    ViewState["Sessionprogress"] = Convert.ToInt32(Sessionprogress);
                    }
                else
                    {
                    ViewState["Sessionprogress"] = Sessionprogress = objCommon.LookUp("ACTIVITY_MASTER AM INNER JOIN [SESSION_ACTIVITY] SA ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)", "PAGE_LINK like '%" + page + "%' ");
                    }

                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR PROGRESS REPORT
                    CheckActivity();

                    //pnlId.Visible = false;
                    dvRemark.Visible = false;
                    pnlId.Visible = false;
                    pnlmainbody.Visible = true;
                    updEdit.Visible = false;
                    divbody.Visible = true;
                    //imgCalDateOfBirth.Visible = false;

                    //few validations
                    PhdStudProgressValidation();




                    ShowStudentDetails();

                    //ShowSignDetails();
                    ViewState["action"] = "edit";

                    //Added on 26/11/2015
                    divCountCheck.Visible = true;
                    DivDGC.Visible = false;
                }
                else
                {
                    //CheckActivity();
                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "3")
                    {
                        //slide.Visible = false;
                        pnlmainbody.Visible = false;
                        DivDGC.Visible = false;
                        pnlId.Visible = false;
                        updEdit.Visible = true;
                        pnlmainbody.Visible = false;
                        divbody.Visible = false;
                        
                    }
                    if (ua_type_fac == "6" || ua_type_fac == "4")
                    {
                        //pnlDoc.Enabled = false;
                        pnlId.Enabled = false;
                        btnReport.Visible = false;
                        updEdit.Visible = true;
                        pnlmainbody.Visible = false;
                        divbody.Visible = false;
                       // slide.Visible = false;
                        
                    }
                    pnlId.Visible = false;
                    lblRegNo.Enabled = true;
                    btnReport.Visible = false;
                    updEdit.Visible = true;
                    pnlmainbody.Visible = false;
                    divbody.Visible = false;
                   // slide.Visible = false;
                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        //ShowSignDetails();
                    }

                    divCountCheck.Visible = false;  // Added on 26/11/2015

                    //added on 30072018
                    if (ua_type_fac == "1" || ua_type_fac == "4")
                    {
                        if (Convert.ToString(Session["userpreviewid"]) != string.Empty)
                        {
                            updEdit.Visible = true;
                            pnlmainbody.Visible = false;
                            pnlId.Visible = false;
                            ViewState["action"] = "edit";
                            ViewState["Sessionprogress"] = Session["previewsession"];
                            ShowStudentDetails();
                            pnlId.Visible=false;
                            divbody.Visible = false;
                            // btnReport.Visible = false;
                            pnlmainbody.Visible = false;
                        }
                    }
                }
            }
        }
        else
        {
            //Sessionprogress = Session["currentsession"].ToString();
            //ViewState["Sessionprogress"] = Convert.ToInt32(Sessionprogress) - 1;
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                   // txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
        pnlmainbody.Visible = false;

        // BindProgress();
    }

    private void CheckActivity()
    {
        string sessionno = "0";
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        //sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSIONNO > 0 AND FLOCK=0 AND ODD_EVEN=2 AND EXAMTYPE=3");
        sessionno = ViewState["Sessionprogress"].ToString();

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnDisplay.Visible = true;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnDisplay.Visible = false;
            }

        }
        else
        {
            // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            objCommon.DisplayMessage("this Activity has been Stopped. So You Can View Details and Download Certificate .", this.Page);
            pnDisplay.Visible = false;
            if (ViewState["usertype"].ToString() == "2")
            {
                // === add to find annexure A register or not  ==>  added by dipali on 23072018 
                string ActiveIdno = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "IDNO", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"].ToString())));
                if (ActiveIdno != string.Empty)
                {
                    pnDisplay.Visible = true;
                    CheckControlOff();
                }
            }
            else
            {
                pnDisplay.Visible = true;
                CheckControlOff();
            }

        }
        dtr.Close();
    }


    /// <summary>
    /// only for student 13/08/2021
    /// </summary>
    public void PhdStudProgressValidation()
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            DataSet dsdetails = objSC.GetPhdstudProgressValidation(Convert.ToInt32(ViewState["Sessionprogress"]), Convert.ToInt32(Session["idno"]));
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                if (dsdetails.Tables[0].Rows[0]["PHD_STUDENT_RESULT_COUNT"].ToString() == "0")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'Academic/Comprehensive_Stud_Report.aspx'"));
                    //76
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('You are not Eligible for Progress Report Submission because Your Previous Semester Progress Report Not Submitted Yet!!');window.location ='Comprehensive_Stud_Report.aspx?pageno=" + pageno + "';",
true);

                    // return;
                }

                if (dsdetails.Tables[0].Rows[0]["PHD_STUDENT_RESULT_DRC_REMARK_COUNT"].ToString() == "0")
                {
                    //objCommon.DisplayMessage("You Can Not Approved This Student. Because Student Not Paid The Fees", this.Page);
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'Academic/Comprehensive_Stud_Report.aspx'"));
                    //76
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('You are not Eligible for Progress Report Submission because Your Previous Semester Progress Report is Pending!!');window.location ='Comprehensive_Stud_Report.aspx?pageno=" + pageno + "';",
true);

                    //return;
                }

                if (dsdetails.Tables[0].Rows[0]["DCR_WITH_RECON_COUNT"].ToString() == "0")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'Academic/Comprehensive_Stud_Report.aspx'"));
                    //76
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('You are not Eligible for Progress Report Submission because Your Previous Semester Fees is Not Paid Yet!!');window.location ='Comprehensive_Stud_Report.aspx?pageno=" + pageno + "';",
true);
                    //return;
                }

            }

        }
    }




    public void CheckControlOff()
    {
        txtReserchTopic.Enabled = txtDescription.Enabled = txtRemark.Enabled = ddlGrade.Enabled = txtComments.Enabled = false;
        btnSubmit.Visible = false;
    }

    private void ChangeControlStatus(bool status)
    {

        foreach (Control c in Page.Controls)
            foreach (Control ctrl in c.Controls)

                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = status;

                else if (ctrl is Button)

                    ((Button)ctrl).Enabled = status;

                else if (ctrl is RadioButton)

                    ((RadioButton)ctrl).Enabled = status;

                else if (ctrl is ImageButton)

                    ((ImageButton)ctrl).Enabled = status;

                else if (ctrl is CheckBox)

                    ((CheckBox)ctrl).Enabled = status;

                else if (ctrl is DropDownList)

                    ((DropDownList)ctrl).Enabled = status;

                else if (ctrl is HyperLink)

                    ((HyperLink)ctrl).Enabled = status;

    }

    private void ShowStudentDetails()
    {
       // CheckActivity();
        //Sessionprogress = Session["currentsession"].ToString();
        //ViewState["Sessionprogress"] = Convert.ToInt32(Sessionprogress) - 1;
    int sessionno = 2;
    ViewState["Sessionprogress"] = sessionno;
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            // dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            dtr = objSC.GetStudentPHDProgressDetails(Convert.ToInt32(Session["idno"]));
        }
        else if (Convert.ToString(Session["userpreviewid"]) != string.Empty)
        {
            dtr = objSC.GetStudentPHDProgressDetails(Convert.ToInt32(Session["userpreviewid"]));
        }
        else
        {
        dtr = objSC.GetStudentPHDProgressDetailsSupervisor(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ViewState["Sessionprogress"].ToString()));
            pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                //txtIDNo.Text = dtr["IDNO"].ToString();
                //txtIDNo.ToolTip = dtr["REGNO"].ToString();
                lblRegNo.ToolTip = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ENROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ENROLLNO"].ToString();
                lblRegNo.Text = dtr["IDNO"].ToString();

                lblRollNo.Text = dtr["ROLLNO"].ToString();
                //txtRegNo.Enabled = false;
                Sessionprogress = ViewState["Sessionprogress"].ToString();
                int Sessionno = Convert.ToInt32(Sessionprogress);
                string sessionname = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO = " + Sessionno);
                //string sessionyear = objCommon.LookUp("ACD_SESSION_MASTER", "(case when odd_even = 1 then substring(SESSION_NAME,0,5) else substring(SESSION_NAME,6,5) end)SESSION_NAME", "SESSIONNO = " + Sessionno);
                lblsession.Text = sessionname;
                lblsession.ToolTip = Sessionno.ToString();
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
                if (lblStatus.Text == "2")
                {
                    lblStatus.Text = "PART TIME";
                }
                else
                {
                    lblStatus.Text = "FULL TIME";
                }
                lblSupervisor.Text = dtr["PHDSUPERVISORNAME"] == null ? string.Empty : dtr["PHDSUPERVISORNAME"].ToString();
                lblSupervisor.ToolTip = dtr["PHDSUPERVISORNO"] == null ? string.Empty : dtr["PHDSUPERVISORNO"].ToString();
                //lblCoSupervisor.Text = dtr["PHDCOSUPERVISORNAME"] == null ? string.Empty : dtr["PHDCOSUPERVISORNAME"].ToString();
                //lblCoSupervisor.ToolTip = dtr["PHDCOSUPERVISORNO1"] == null ? string.Empty : dtr["PHDCOSUPERVISORNO1"].ToString();
                lblCredits.Text = dtr["CREDITS"] == null ? string.Empty : dtr["CREDITS"].ToString();
                // txtReserchTopic.Text = dtr["TOPICS"] == null ? string.Empty : dtr["TOPICS"].ToString();
                string Research = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "TOPICS", "IDNO = " + dtr["IDNO"].ToString() + " AND SESSIONNO =" + (Convert.ToInt32(Sessionprogress)));
                txtReserchTopic.Text = Research.ToString();
                //txtDescription.Text = dtr["WORKDONE"] == null ? string.Empty : dtr["WORKDONE"].ToString();
                string Description = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "WORKDONE", "IDNO = " + dtr["IDNO"].ToString() + " AND SESSIONNO =" + (Convert.ToInt32(Sessionprogress)));
                txtDescription.Text = Description.ToString();


                //Added by Sneha Doble on 23/09/2020
                DataSet ds1 = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO,ISNULL(SECONDJOINTSUPERVISORNO,0)SECONDJOINTSUPERVISORNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text), "IDNO");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string supervisor = ds1.Tables[0].Rows[0]["SUPERVISORNO"].ToString();
                    string jointsupervisor = ds1.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString();
                    string instituefaculty = ds1.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString();
                    string drc = ds1.Tables[0].Rows[0]["DRCNO"].ToString();
                    string drcchairman = ds1.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString();
                    string secondsupervisor = ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORNO"].ToString();

                    if (Session["userno"].ToString() == supervisor)
                    {
                        divremark.Visible = true;
                        DivDGC.Visible = false;
                        txtRemark.Text = dtr["REMARK"] == null ? string.Empty : dtr["REMARK"].ToString();
                    }
                    else if (Session["userno"].ToString() == jointsupervisor && dtr["SUPERROLE"].ToString() == "S")
                    {
                        divremark.Visible = true;
                        DivDGC.Visible = false;
                        divgradeaw.Visible = false;
                        txtRemark.Text = dtr["REMARKCOSUPER"] == null ? string.Empty : dtr["REMARKCOSUPER"].ToString();
                    }
                    else if (Session["userno"].ToString() == jointsupervisor && (dtr["SUPERROLE"].ToString() == "J" || dtr["SUPERROLE"].ToString() == "T"))
                    {
                        divremark.Visible = true;
                        DivDGC.Visible = false;
                        divgradeaw.Visible = false;
                        txtRemark.Text = dtr["REMARKCOSUPER"] == null ? string.Empty : dtr["REMARKCOSUPER"].ToString();
                    }
                    else if (Session["userno"].ToString() == instituefaculty)
                    {
                        divremark.Visible = false;
                        DivDGC.Visible = true;
                        lbldgc.Text = "Institute faculty";
                        txtComments.Text = dtr["REMARKINSTITUTE"] == null ? string.Empty : dtr["REMARKINSTITUTE"].ToString();
                    }
                    else if (Session["userno"].ToString() == drc)
                    {
                        divremark.Visible = false;
                        DivDGC.Visible = true;
                        lbldgc.Text = "DRC Nominee";
                        txtComments.Text = dtr["REMARKDGC"] == null ? string.Empty : dtr["REMARKDGC"].ToString();
                    }
                    else if (Session["userno"].ToString() == secondsupervisor)
                    {
                        divremark.Visible = true;
                        DivDGC.Visible = false;
                        txtRemark.Text = dtr["REMARKSECCOSUPER"] == null ? string.Empty : dtr["REMARKSECCOSUPER"].ToString();
                    }

                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                    if (remarksup != "")
                    {
                        string jointsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                        if (jointsuperremark != "")
                        {
                            string remarkins = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                            if (remarkins != "")
                            {
                                string remarkdgc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                if (remarkdgc != "")
                                {
                                    if (Session["userno"].ToString() == drcchairman)
                                    {
                                        divremark.Visible = false;
                                        DivDGC.Visible = true;
                                        lbldgc.Text = "DRC ChairPerson";
                                        txtComments.Text = dtr["REMARKDRCCHAIRMAN"] == null ? string.Empty : dtr["REMARKDRCCHAIRMAN"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }


                //txtRemark.Text = dtr["REMARK"] == null ? string.Empty : dtr["REMARK"].ToString();
                ddlGrade.SelectedValue = dtr["GRADE"] == null ? "0" : dtr["GRADE"].ToString();
                lblgradestatus.Text = dtr["GRADESTATUS"] == null ? string.Empty : dtr["GRADESTATUS"].ToString();

                //added extra sup 20092017
                // lbljointsupervisior1.Text = dtr["SECONDSUPERNAME"] == null ? string.Empty : dtr["SECONDSUPERNAME"].ToString();
                // lbljointsupervisior1.ToolTip = dtr["SECONDJOINTSUPERVISORNO"] == null ? string.Empty : dtr["SECONDJOINTSUPERVISORNO"].ToString();

                //---------------SEMESTER NO CONDITION -- added by dipali 
                lblsemesterno.ToolTip = dtr["SEMESTERNO"] == null ? string.Empty : dtr["SEMESTERNO"].ToString();
                lblsemesterno.Text = dtr["SEM"] == null ? string.Empty : dtr["SEM"].ToString();
                //--------------------------------------------------------------------------------------   

                if (dtr["SUPERROLE"].ToString() == "S")
                {
                    lblCo.Text = "Expert from Department :";
                }
                else
                {
                    lblCo.Text = "Co-Supervisor :";
                }
                DataSet ds = objCommon.FillDropDown("ACD_PHD_STUDENT_RESULT", "isnull(SUPERVISORNO,0)SUPERVISORNO,isnull(JOINTSUPERVISORNO,0)JOINTSUPERVISORNO ,REMARKCOSUPER", "isnull(DRCNO,0)DRCNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + " AND SESSIONNO =" + (Convert.ToInt32(Sessionprogress)), "IDNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int supervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                    int jointsupervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
                    //int instituefaculty = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
                    int drc = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
                    //int dcrchair = Convert.ToInt32(dtr["DRCCHAIRMANNO"]);
                    int uano = Convert.ToInt32(Session["userno"].ToString());

                    if (uano == jointsupervisor)
                    {
                        if (dtr["SUPERROLE"].ToString() == "S")
                        {
                            lblname.Text = "Experts";
                        }
                        else
                        {
                            lblname.Text = "Joint supervisor";
                        }
                        txtRemark.Text = (ds.Tables[0].Rows[0]["REMARKCOSUPER"].ToString());
                    }
                    // ADDED FOR EXTRA SUPERVISOR   ----------------------------20092017
                    if (dtr["SUPERROLE"].ToString() == "T")
                    { secoundsupervisor.Visible = true; }
                    else { secoundsupervisor.Visible = false; }
                    // END
                    if (uano == supervisor)
                    {
                        lblname.Text = "supervisor";
                    }

                }
                //check the supervisor remark status if  status (remarkstatus) is 1 then show the report button

                int count = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "count(*)", "IDNO=" + Convert.ToInt32(dtr["IDNO"])));
                if (count > 0)
                {
                    //int remarkstatus = Convert.ToInt32(dtr["REMARKSTATUS"]);
                    string rstatus = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSTATUS", "SESSIONNO=" + (Convert.ToInt32(Sessionprogress)) + " AND IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string rstatusco = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "SESSIONNO=" + (Convert.ToInt32(Sessionprogress)) + " AND IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string rstatusins = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "SESSIONNO=" + (Convert.ToInt32(Sessionprogress)) + " AND IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string rstatusdgc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "SESSIONNO=" + (Convert.ToInt32(Sessionprogress)) + " AND IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string rstatusdean = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "DEANSTATUS", "SESSIONNO=" + (Convert.ToInt32(Sessionprogress)) + " AND IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string rstatusdrcchairman = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDRCCHAIRMAN", "SESSIONNO=" + (Convert.ToInt32(Sessionprogress)) + " AND IDNO =" + Convert.ToInt32(dtr["IDNO"]));

                    string institute = objCommon.LookUp("ACD_PHD_DGC", "OUTINSTITUTE", "IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string joint = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string drc = objCommon.LookUp("ACD_PHD_DGC", "OUTNOMINEE", "IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    string noofdgc = objCommon.LookUp("ACD_PHD_DGC", "NOOFDGC", "IDNO =" + Convert.ToInt32(dtr["IDNO"]));
                    if (joint != "")
                    {
                        rstatusco = "Outside";
                    }
                    if (institute != "")
                    {
                        rstatusins = "Outside";
                    }
                    if (drc != "")
                    {
                        rstatusdgc = "Outside";
                    }
                    if (noofdgc == "3")
                    {
                        rstatusco = "Outside";
                    }
                    if (rstatus == "1" && rstatusco != "" && rstatusins != "" && rstatusdgc != "" && rstatusdrcchairman != "" && rstatusdean == "1")
                    {
                        btnReport.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        btnReport.Visible = false;
                        btnSubmit.Visible = true;
                    }
                }

                if (ViewState["usertype"].ToString() == "4")
                {
                    string Recon = objCommon.LookUp("ACD_DCR", "isnull(RECON,0)RECON", "IDNO =" + Convert.ToInt32(dtr["IDNO"]) + " AND SEMESTERNO =" + (Convert.ToInt32(lblsemesterno.ToolTip.ToString()) + 1) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["Sessionprogress"]) + " AND  RECIEPT_CODE= 'TF'");
                    if (Recon != "")
                    {
                        if (Convert.ToBoolean(Recon) == false)
                        {
                            objCommon.DisplayMessage("You Can Not Approved This Student. Because Student Not Paid The Fees", this.Page);
                            btnSubmit.Enabled = false;
                            return;
                        }
                    }
                }
                this.BindProgress();
            }
            else
            {
                objCommon.DisplayMessage("This Student Not Filled Progress Report", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("This Student Not Filled Progress Report", this.Page);
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    private void ClearControl()
    {
        txtIDNo.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
        lblFatherName.Text = string.Empty;
        lblStudName.Text = string.Empty;
        lblDateOfJoining.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblStatus.Text = string.Empty;
        lblSupervisor.Text = string.Empty;
        lblCredits.Text = string.Empty;
        //lblCoSupervisor.Text = string.Empty;
        txtReserchTopic.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlGrade.SelectedIndex = 0;
        Session["qualifyTbl"] = null;
        lvlprogrss.DataSource = null;
        lvlprogrss.Visible = false;
        txtRemark.Text = string.Empty;
        txtComments.Text = string.Empty;
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }
        //Response.Redirect(url + "&id=" + lnk.CommandArgument);
        Session["idno"] = Convert.ToInt32(lnk.CommandArgument);
        ShowStudentDetails();
        pnlmainbody.Visible = true;
           
    }

    private void bindlist(string category, string searchtext)
    {
        //int dept = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
        //if (dept > 0)
        //{
        //    string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "DEGREENO=6 AND DEPTNO=" + dept);
        //    StudentController objSC = new StudentController();
        //    DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvStudent.DataSource = ds;
        //        lvStudent.DataBind();
        //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        //    }
        //    else
        //        lblNoRecords.Text = "Total Records : 0";
        //}
        //else
        //{
        string branchno = "0";

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        //Response.Redirect(Request.Url.ToString());
    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //string semester = objCommon.LookUp("ACD_STUDENT_RESULT", "distinct SEMESTERNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + " AND SESSIONNO=" + (Convert.ToInt32(ViewState["Sessionprogress"])));

    //        int ProStatus = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()))); //add on 18/01/2021
    //        //string semester = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SEMESTERNO) SEMESTERNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + "AND ISNULL(PREV_STATUS,0)=0");
    //       // string pass = objCommon.LookUp("ACD_TRRESULT", "RESULT", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + " AND SEMESTERNO=" + semester);
    //        // aDDED BY SAMAD
    //        string pass = "P";
    //        StudentController objSC = new StudentController();
    //        Student objS = new Student();
    //        string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
    //        string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

    //        if (ua_type == "1" || ua_type == "2" || (ua_type == "3" && ua_dec == "0" || ua_type == "4"))
    //        {
    //            objS.IdNo = Convert.ToInt32(txtIDNo.Text);
    //            objS.EnrollNo = lblEnrollNo.Text.Trim();
    //            objS.RegNo = lblRegNo.Text.Trim();
    //            objS.RollNo = lblRegNo.Text.Trim();
    //            if (!lblStudName.Text.Trim().Equals(string.Empty)) objS.StudName = lblStudName.Text.Trim();
    //            if (!lblFatherName.Text.Trim().Equals(string.Empty)) objS.FatherName = lblFatherName.Text.Trim();
    //            if (!lblDateOfJoining.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(lblDateOfJoining.Text.Trim());
    //            objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
    //            objS.PhdSupervisorNo = Convert.ToInt32(lblSupervisor.ToolTip);
    //            // objS.PhdCoSupervisorNo1 = Convert.ToInt32(lblCoSupervisor.ToolTip);
    //            DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text), "IDNO");
    //            objS.DrcNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
    //            if (!lblCredits.Text.Trim().Equals(string.Empty)) objS.Credits = Convert.ToInt32(lblCredits.Text);
    //            if (!txtReserchTopic.Text.Trim().Equals(string.Empty)) objS.Topics = txtReserchTopic.Text.Trim();
    //            if (!txtDescription.Text.Trim().Equals(string.Empty)) objS.Workdone = txtDescription.Text.Trim();
    //            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //            // if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
    //            objS.Grade = Convert.ToInt32(ddlGrade.SelectedValue);
    //            objS.Sessionno = (Convert.ToInt32(ViewState["Sessionprogress"]));
    //            objS.CollegeCode = Session["colcode"].ToString();
    //            //UANO SAVE
    //            if (ua_type == "1" || ua_type == "3")
    //            {
    //                objS.Uano = Convert.ToInt32(Session["userno"]);
    //            }
    //            else
    //            {
    //                objS.Uano = 0;
    //            }
    //            int Sessionno = (Convert.ToInt32(ViewState["Sessionprogress"]));
    //            // UPDATE THE ONLY PHD STUDENT DATA THROUGH STUDENT
    //            if (ua_type == "1" || ua_type == "2")
    //            {
    //                if (pass == "P")
    //                {
    //                    string output = objSC.UpdatePHDStudentResult(objS);
    //                    if (output != "-99")
    //                    {
    //                        Session["qualifyTbl"] = null;
    //                        objCommon.DisplayMessage("Student Information Updated Successfully!!", this.Page);
    //                        this.ShowStudentDetails();
    //                    }

    //                    //to create dcr temporrary
    //                    if (Session["userno"].ToString() == "25167")
    //                    {
    //                        //dcr change dc chairman to supervisor
    //                        hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
    //                        if (hdfSem.Value == "2" || hdfSem.Value == "4" || hdfSem.Value == "6" || hdfSem.Value == "8" || hdfSem.Value == "10" || hdfSem.Value == "12" || hdfSem.Value == "14")
    //                        {
    //                            this.DemandAndDcr();
    //                        }
    //                        this.ShowStudentDetails();
    //                        this.BindProgress();
    //                        return;
    //                    }


    //                    //PHD SESSION WISE FEES  add on 18/01/2021
    //                    if (Session["userno"].ToString() == "25167" && Sessionno == 94)
    //                    {
    //                        //dcr change dc chairman to supervisor
    //                        hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
    //                        if (hdfSem.Value == "3")
    //                        {
    //                            this.DemandAndDcr();
    //                        }
    //                        if (hdfSem.Value == "5" || hdfSem.Value == "7" || hdfSem.Value == "9" || hdfSem.Value == "11" || hdfSem.Value == "13" || hdfSem.Value == "15")
    //                        {
    //                            if (ProStatus == 3 || ProStatus == 7)
    //                            {
    //                                this.DemandAndDcr();
    //                            }
    //                        }
    //                        this.ShowStudentDetails();
    //                        this.BindProgress();
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage("Your Result is Not Passed!!", this.Page);
    //                }
    //            }
    //            DataSet ds1 = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,ISNULL(DRCCHAIRMANNO,0)DRCCHAIRMANNO,ISNULL(SECONDJOINTSUPERVISORNO,0)SECONDJOINTSUPERVISORNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text), "IDNO");
    //            if (ds1.Tables[0].Rows.Count > 0)
    //            {
    //                int supervisor = Convert.ToInt32(ds1.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
    //                int jointsupervisor = Convert.ToInt32(ds1.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
    //                int instituefaculty = Convert.ToInt32(ds1.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
    //                int drc = Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCNO"].ToString());
    //                int drcchairman = Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString());
    //                int secondsupervisor = Convert.ToInt32(ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORNO"].ToString());

    //                int uano = Convert.ToInt32(Session["userno"].ToString());

    //                // check the supervisor login
    //                if ((ua_type == "3" && ua_dec == "0") || ua_type == "4")
    //                {
    //                    //Check the Status of the student

    //                    int chkstudentstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
    //                    objS.Grade = Convert.ToInt32(ddlGrade.SelectedValue);

    //                    if (chkstudentstatus > 0)
    //                    {
    //                        if (uano == supervisor || uano == jointsupervisor || uano == secondsupervisor)
    //                        {
    //                            if (txtRemark.Text == string.Empty)
    //                            {
    //                                objCommon.DisplayMessage("Please Enter Student Remark", this.Page);
    //                                return;
    //                            }
    //                        }
    //                        //if (uano == drc || uano == instituefaculty || uano == drcchairman)
    //                        if (uano == drc || uano == instituefaculty)
    //                        {
    //                            if (txtComments.Text == string.Empty)
    //                            {
    //                                objCommon.DisplayMessage("Please Enter Comments", this.Page);
    //                                return;
    //                            }
    //                        }
    //                        if (ddlGrade.SelectedValue == "-1")
    //                        {
    //                            objCommon.DisplayMessage("Please Select Grade", this.Page);
    //                        }
    //                        else
    //                        {
    //                            if (uano == supervisor)
    //                            {
    //                                string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                if (remarksup == "")
    //                                {
    //                                    // if (!txtRemark.Text.Trim().Equals(string.Empty))
    //                                    objS.Remark = txtRemark.Text.Trim();
    //                                    string output1 = objSC.UpdateSupervisorRemarkStatus(objS);

    //                                    if (output1 != "-99")
    //                                    {
    //                                        //to update stud details from supervisor
    //                                        string output = objSC.UpdatePHDStudentResult(objS);

    //                                        Session["qualifyTbl"] = null;
    //                                        objCommon.DisplayMessage("Information Updated Successfully By Supervisor!!", this.Page);

    //                                        //dcr change dc chairman to supervisor
    //                                        hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
    //                                        if (hdfSem.Value == "2" || hdfSem.Value == "4" || hdfSem.Value == "6" || hdfSem.Value == "8" || hdfSem.Value == "10" || hdfSem.Value == "12" || hdfSem.Value == "14")
    //                                        {
    //                                            this.DemandAndDcr();
    //                                        }

    //                                        this.ShowStudentDetails();
    //                                        this.BindProgress();

    //                                        //PHD SESSION WISE FEES  add on 18/01/2021
    //                                        if (Sessionno == 94)
    //                                        {
    //                                            //dcr change dc chairman to supervisor
    //                                            hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
    //                                            if (hdfSem.Value == "3")
    //                                            {
    //                                                this.DemandAndDcr();
    //                                            }
    //                                            if (hdfSem.Value == "5" || hdfSem.Value == "7" || hdfSem.Value == "9" || hdfSem.Value == "11" || hdfSem.Value == "13" || hdfSem.Value == "15")
    //                                            {
    //                                                if (ProStatus == 3 || ProStatus == 7)
    //                                                {
    //                                                    this.DemandAndDcr();
    //                                                }
    //                                            }
    //                                            this.ShowStudentDetails();
    //                                            this.BindProgress();
    //                                            return;
    //                                        }
    //                                    }

    //                                }
    //                                else
    //                                {
    //                                    //objCommon.DisplayMessage("Remarks of Superviser on the work done by the candidate is Already Given.", this.Page);
    //                                }
    //                            }

    //                            if (uano == jointsupervisor)
    //                            {
    //                                string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                if (remarksup != "")
    //                                {
    //                                    string remarkjsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                    if (remarkjsuper == "")
    //                                    {

    //                                        if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //                                        string output1 = objSC.UpdateSupervisorRemarkStatusCo(objS);
    //                                        if (output1 != "-99")
    //                                        {
    //                                            Session["qualifyTbl"] = null;
    //                                            objCommon.DisplayMessage("Information Updated Successfully By Joint Supervisor/Expert in Department!!", this.Page);
    //                                            this.ShowStudentDetails();
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        //objCommon.DisplayMessage("Remarks by Joint Superviser/Expert on the work done by the candidate is Already Given.", this.Page);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
    //                                }
    //                            }
    //                            ///////////////////// added for exta supervisor /////////////////////////////
    //                            if (uano == secondsupervisor)
    //                            {
    //                                string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                if (remarksup != "")
    //                                {
    //                                    string jointsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                    if (jointsuperremark != "")
    //                                    {
    //                                        string jointsecondsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));

    //                                        if (jointsecondsuperremark == "")
    //                                        {
    //                                            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //                                            string output1 = objSC.UpdateSupervisorRemarkStatusSecondCo(objS);
    //                                            if (output1 != "-99")
    //                                            {
    //                                                Session["qualifyTbl"] = null;
    //                                                objCommon.DisplayMessage("Information Updated Successfully By Second Joint Supervisor!!", this.Page);
    //                                                this.ShowStudentDetails();
    //                                                this.BindProgress();
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            // objCommon.DisplayMessage("Remarks by Second Joint Superviser on the work done by the candidate is Already Given.", this.Page);
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        objCommon.DisplayMessage("Please Forward application to first Joint Supervisor.!!", this.Page);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to first Supervisor.!!", this.Page);
    //                                }
    //                            }
    //                            /////////////////////////////////////////////////////////////////////////////
    //                            if (uano == instituefaculty)
    //                            {
    //                                #region comment
    //                                ///////////////////// added for extra supervisor //////////////////////////////
    //                                //if (secondsupervisor > 0)
    //                                //{                                       
    //                                //    //string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                //    //if (remarksecondsuper != string.Empty)
    //                                //    //{
    //                                //    //    // second supervisor confirm this application
    //                                //    //}
    //                                //    //else
    //                                //    //{
    //                                //    //    objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
    //                                //    //}
    //                                //}
    //                                #endregion

    //                                string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                string noofdgc = objCommon.LookUp("ACD_PHD_DGC", "NOOFDGC", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
    //                                string joint = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));

    //                                if (noofdgc == "3")
    //                                {
    //                                    remarksup = "outside";
    //                                }
    //                                if (joint != "")
    //                                {
    //                                    remarksup = "outside";
    //                                }
    //                                //**** commented by vaibhav m on date 11012017
    //                                if (remarksup != "")
    //                                {
    //                                    string remarkinstitudefac = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                    if (remarkinstitudefac == "")
    //                                    {
    //                                        if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //                                        if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
    //                                        //if (txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = "Satisfatory";
    //                                        string output1 = objSC.UpdateRemarkdgcCom(objS, txtComments.Text, "I");
    //                                        if (output1 != "-99")
    //                                        {
    //                                            Session["qualifyTbl"] = null;
    //                                            objCommon.DisplayMessage("Information Updated Successfully By Institute Faculty!!", this.Page);
    //                                            this.ShowStudentDetails();
    //                                            this.BindProgress();

    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        //objCommon.DisplayMessage("Institude Faculty Comments are Already Given.", this.Page);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
    //                                }
    //                            }
    //                            if (uano == drc)
    //                            {
    //                                /////////////////// added for extra supervisor //////////////////////////////
    //                                //if (secondsupervisor > 0)
    //                                //{
    //                                //    string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                //    if (remarksecondsuper != string.Empty)
    //                                //    {
    //                                //        // second supervisor confirm this application
    //                                //    }
    //                                //    else
    //                                //    {
    //                                //        objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
    //                                //    }
    //                                //}
    //                                /////////////////////////////////////////////////////////////////////////////

    //                                //**** commented by vaibhav m on date 11012017

    //                                string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));

    //                                if (remarksup != "")
    //                                {
    //                                    string remarkdrc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                    if (remarkdrc == "")
    //                                    {
    //                                        if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //                                        if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
    //                                        string output1 = objSC.UpdateRemarkdgcCom(objS, txtComments.Text, "D");
    //                                        if (output1 != "-99")
    //                                        {
    //                                            Session["qualifyTbl"] = null;
    //                                            objCommon.DisplayMessage("Information Updated Successfully By DRC Nominee!!", this.Page);
    //                                            this.ShowStudentDetails();
    //                                            this.BindProgress();
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        //objCommon.DisplayMessage("DRC Nominee Comments are Already Given.", this.Page);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
    //                                }
    //                                //**** upto here
    //                            }
    //                            //----add   drcchairman  -----  condition -- 16042019
    //                            if (uano == drcchairman)
    //                            {
    //                                /////////////////// added for extra supervisor //////////////////////////////
    //                                //if (secondsupervisor > 0)
    //                                //{
    //                                //    string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                //    if (remarksecondsuper != string.Empty)
    //                                //    {
    //                                //        // second supervisor confirm this application
    //                                //    }
    //                                //    else
    //                                //    {
    //                                //        objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
    //                                //    }
    //                                //}
    //                                /////////////////////////////////////////////////////////////////////////////
    //                                string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                if (remarksup != "")
    //                                {
    //                                    string jointsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                    if (jointsuperremark != "")
    //                                    {
    //                                        string remarkins = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                        if (remarkins != "")
    //                                        {
    //                                            string remarkdgc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                            if (remarkdgc != "")
    //                                            {
    //                                                //string drcnominee = objCommon.LookUp("ACD_PHD_DGC", "OUTNOMINEE", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
    //                                                //if (drcnominee != "")
    //                                                //{
    //                                                //    remarkdgc = "outside";
    //                                                //}
    //                                                //if (remarkjoint == "")
    //                                                //{
    //                                                //    objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
    //                                                //    return;
    //                                                //}
    //                                                //else if (remarkins == "")
    //                                                //{
    //                                                //    objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
    //                                                //    return;
    //                                                //}
    //                                                //else if (remarkdgc != "")
    //                                                //{
    //                                                string remarkdgcchairman = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDRCCHAIRMAN", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                                if (remarkdgcchairman == "")
    //                                                {
    //                                                    if (txtRemark.Text == string.Empty)
    //                                                    {
    //                                                        objS.Remark = "OK";
    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //                                                    }

    //                                                    if (txtComments.Text == string.Empty)
    //                                                    {
    //                                                        objS.Remark = "OK";
    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
    //                                                    }

    //                                                    string output1 = objSC.UpdateRemarkdgcCom(objS, txtComments.Text, "DR");
    //                                                    if (output1 != "-99")
    //                                                    {
    //                                                        Session["qualifyTbl"] = null;
    //                                                        objCommon.DisplayMessage("Drc Chairman Information Updated Successfully. Now, Student is Eligible for Fees Payment!!", this.Page);
    //                                                        this.ShowStudentDetails();
    //                                                        this.BindProgress();
    //                                                    }
    //                                                }
    //                                                else
    //                                                {
    //                                                    objCommon.DisplayMessage("DRC ChairPerson Comments are Already Given.", this.Page);
    //                                                }
    //                                            }
    //                                            else
    //                                            {
    //                                                objCommon.DisplayMessage("Please Forward application to DGC Nominee first!!", this.Page);
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            objCommon.DisplayMessage("Please Forward application to Institute Faculty first!!", this.Page);
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        objCommon.DisplayMessage("Please Forward application to Joint Supervisor first!!", this.Page);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
    //                                }

    //                            }
    //                            //  ---  drcchairman end  


    //                            if (ua_type == "4")
    //                            {
    //                                /////////////////// added for extra supervisor //////////////////////////////
    //                                //if (secondsupervisor > 0)
    //                                //{
    //                                //    string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                //    if (remarksecondsuper != string.Empty)
    //                                //    {
    //                                //        // second supervisor confirm this application
    //                                //    }
    //                                //    else
    //                                //    {
    //                                //        objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
    //                                //    }
    //                                //}
    //                                /////////////////////////////////////////////////////////////////////////////
    //                                string remarkjoint = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                string remarkins = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                string remarkdgc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
    //                                string drcnominee = objCommon.LookUp("ACD_PHD_DGC", "OUTNOMINEE", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));

    //                                string remarkdrc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDRCCHAIRMAN", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));

    //                                if (drcnominee != "")
    //                                {
    //                                    remarkdgc = "outside";
    //                                }
    //                                if (remarkjoint == "")
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
    //                                    return;
    //                                }
    //                                else if (remarkins == "")
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
    //                                    return;
    //                                }
    //                                else if (remarkdgc == "")
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to DGC Nominee first!!", this.Page);
    //                                    return;
    //                                }
    //                                else if (remarkdrc != "" && remarkdgc != "" && remarkins != "")
    //                                {
    //                                    if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
    //                                    //if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
    //                                    string output1 = objSC.UpdateRemarkdgcCom(objS, "", "DE");
    //                                    if (output1 != "-99")
    //                                    {
    //                                        Session["qualifyTbl"] = null;
    //                                        objCommon.DisplayMessage("Information Updated Successfully By Dean!!", this.Page);
    //                                        this.ShowStudentDetails();
    //                                        this.BindProgress();
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    objCommon.DisplayMessage("Please Forward application to DGC Member first!!", this.Page);
    //                                }

    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        objCommon.DisplayMessage("Student not forword the application to Supervisor", this.Page);
    //                        return;
    //                    }
    //                }
    //            }
    //        }

    //        #region Commented by Sneha
    //        //this.BindProgress();
    //        //Check the Status of the Supervisor if status yes to permission for the HODs

    //        //int chksuperstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
    //        //if (chksuperstatus > 0)
    //        //{
    //        //    int chkSupervisorStatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "SUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));

    //        //    if (chkSupervisorStatus == 1)
    //        //    {
    //        //        if (ua_type == "3" && ua_dec == "1")
    //        //        {
    //        //            objS.IdNo = Convert.ToInt32(txtIDNo.Text);
    //        //            string output = objSC.UpdateHODStatus(objS);
    //        //            if (output != "-99")
    //        //            {
    //        //                objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
    //        //            }
    //        //        }
    //        //    }
    //        //    else
    //        //    {
    //        //        objCommon.DisplayMessage("Supervisor not forword this student application", this.Page);
    //        //        return;
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    objCommon.DisplayMessage("Cannot the Assign the DGC members", this.Page);
    //        //    return;
    //        //}

    //        //Update the DRC status by login DGC and admin.

    //        //if (ua_type == "6" || ua_type == "1" || ua_type == "4")
    //        //{
    //        //    objS.IdNo = Convert.ToInt32(txtIDNo.Text);
    //        //    string output = objSC.UpdateDRCStatus(objS);
    //        //    if (output != "-99")
    //        //    {
    //        //        objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
    //        //        ShowReport("PHDAnnexureConfirm", "rptAnnexureConfirmation.rpt");
    //        //    }
    //        //}

    //        #endregion
    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //        this.ClearControl();
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //string semester = objCommon.LookUp("ACD_STUDENT_RESULT", "distinct SEMESTERNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + " AND SESSIONNO=" + (Convert.ToInt32(ViewState["Sessionprogress"])));
        txtIDNo.Text = "8592";
            int ProStatus = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()))); //add on 18/01/2021
            string semester = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SEMESTERNO) SEMESTERNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + "AND ISNULL(PREV_STATUS,0)=0");
            string pass = "";// objCommon.LookUp("ACD_TRRESULT", "RESULT", "IDNO=" + Convert.ToInt32(txtIDNo.Text) + " AND SEMESTERno=" + 7);
            // aDDED BY SAMAD
            pass = "P";
            StudentController objSC = new StudentController();
            Student objS = new Student();
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            if (ua_type == "1" || ua_type == "2" || (ua_type == "3" && ua_dec == "0" || ua_type == "4"))
            {
                //objS.IdNo = Convert.ToInt32(txtIDNo.Text);
                //objS.EnrollNo = lblEnrollNo.Text.Trim();
                //objS.RegNo = lblRegNo.Text.Trim();
                //objS.RollNo = lblRegNo.Text.Trim();
                //if (!lblStudName.Text.Trim().Equals(string.Empty)) objS.StudName = lblStudName.Text.Trim();
                //if (!lblFatherName.Text.Trim().Equals(string.Empty)) objS.FatherName = lblFatherName.Text.Trim();
                //if (!lblDateOfJoining.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(lblDateOfJoining.Text.Trim());
                //objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
                //objS.PhdSupervisorNo = Convert.ToInt32(lblSupervisor.ToolTip);
                //// objS.PhdCoSupervisorNo1 = Convert.ToInt32(lblCoSupervisor.ToolTip);
                //DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text), "IDNO");
                //objS.DrcNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
                //if (!lblCredits.Text.Trim().Equals(string.Empty)) objS.Credits = Convert.ToInt32(lblCredits.Text);
                //if (!txtReserchTopic.Text.Trim().Equals(string.Empty)) objS.Topics = txtReserchTopic.Text.Trim();
                //if (!txtDescription.Text.Trim().Equals(string.Empty)) objS.Workdone = txtDescription.Text.Trim();
                //if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                // if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
               


                if (ua_type == "2")
                {
                    objS.Grade = Convert.ToInt32(-1);
                }
                else
                {
                    objS.Grade = Convert.ToInt32(ddlGrade.SelectedValue);
                }

                objS.SessionNo = (Convert.ToInt32(ViewState["Sessionprogress"]));
                objS.CollegeCode = Session["colcode"].ToString();
                //UANO SAVE
                if (ua_type == "1" || ua_type == "3")
                {
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    objS.Uano = 0;
                }
                int Sessionno = (Convert.ToInt32(ViewState["Sessionprogress"]));
                // UPDATE THE ONLY PHD STUDENT DATA THROUGH STUDENT
                if (ua_type == "1" || ua_type == "2")
                {
                    if (pass == "P")
                    {
                        string output = objSC.UpdatePHDStudentResult(objS);
                        if (output != "-99")
                        {
                            Session["qualifyTbl"] = null;
                            objCommon.DisplayMessage("Student Information Updated Successfully!!", this.Page);
                            this.ShowStudentDetails();
                        }

                        //to create dcr temporrary
                        if (Session["userno"].ToString() == "25167")
                        {
                            //dcr change dc chairman to supervisor
                            hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
                            if (hdfSem.Value == "2" || hdfSem.Value == "4" || hdfSem.Value == "6" || hdfSem.Value == "8" || hdfSem.Value == "10" || hdfSem.Value == "12" || hdfSem.Value == "14")
                            {
                                this.DemandAndDcr();
                            }
                            this.ShowStudentDetails();
                            this.BindProgress();
                            return;
                        }


                        //PHD SESSION WISE FEES  add on 18/01/2021
                        if (Session["userno"].ToString() == "25167")
                        {
                            //dcr change dc chairman to supervisor
                            hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
                            if (hdfSem.Value == "3")
                            {
                                this.DemandAndDcr();
                            }
                            if (hdfSem.Value == "5" || hdfSem.Value == "7" || hdfSem.Value == "9" || hdfSem.Value == "11" || hdfSem.Value == "13" || hdfSem.Value == "15")
                            {
                                //if (ProStatus == 3 || ProStatus == 7)
                                //{
                                    this.DemandAndDcr();
                                //}
                            }
                            this.ShowStudentDetails();
                            this.BindProgress();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Your Result is Not Passed!!", this.Page);
                    }
                }
                DataSet ds1 = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO,ISNULL(SECONDJOINTSUPERVISORNO,0)SECONDJOINTSUPERVISORNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text), "IDNO");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    int supervisor = Convert.ToInt32(ds1.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                    int jointsupervisor = Convert.ToInt32(ds1.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
                    int instituefaculty = Convert.ToInt32(ds1.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
                    int drc = Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCNO"].ToString());
                    int drcchairman = Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString());
                    int secondsupervisor = Convert.ToInt32(ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORNO"].ToString());

                    int uano = Convert.ToInt32(Session["userno"].ToString());

                    // check the supervisor login
                    if ((ua_type == "3" && ua_dec == "0") || ua_type == "4")
                    {
                        //Check the Status of the student

                        int chkstudentstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
                        objS.Grade = Convert.ToInt32(ddlGrade.SelectedValue);

                        if (chkstudentstatus > 0)
                        {
                            if (uano == supervisor || uano == jointsupervisor || uano == secondsupervisor)
                            {
                                if (txtRemark.Text == string.Empty)
                                {
                                    objCommon.DisplayMessage("Please Enter Student Remark", this.Page);
                                    return;
                                }
                            }
                            //if (uano == drc || uano == instituefaculty || uano == drcchairman)
                            if (uano == drc || uano == instituefaculty)
                            {
                                if (txtComments.Text == string.Empty)
                                {
                                    objCommon.DisplayMessage("Please Enter Comments", this.Page);
                                    return;
                                }
                            }
                            if (ddlGrade.SelectedValue == "-1")
                            {
                                objCommon.DisplayMessage("Please Select Grade", this.Page);
                            }
                            else
                            {
                                if (uano == supervisor)
                                {
                                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    if (remarksup == "")
                                    {
                                        // if (!txtRemark.Text.Trim().Equals(string.Empty))
                                        objS.Remark = txtRemark.Text.Trim();
                                        string output1 = objSC.UpdateSupervisorRemarkStatus(objS);

                                        if (output1 != "-99")
                                        {
                                            //to update stud details from supervisor
                                            string output = objSC.UpdatePHDStudentResult(objS);

                                            Session["qualifyTbl"] = null;
                                            objCommon.DisplayMessage("Information Updated Successfully By Supervisor!!", this.Page);

                                            //dcr change dc chairman to supervisor
                                            hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
                                            if (hdfSem.Value == "2" || hdfSem.Value == "4" || hdfSem.Value == "6" || hdfSem.Value == "8" || hdfSem.Value == "10" || hdfSem.Value == "12" || hdfSem.Value == "14")
                                            {
                                                this.DemandAndDcr();
                                            }

                                            this.ShowStudentDetails();
                                            this.BindProgress();

                                            //PHD SESSION WISE FEES  add on 18/01/2021
                                            //if (Sessionno == 94)
                                            //{
                                                //dcr change dc chairman to supervisor
                                                hdfSem.Value = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "MAX(SEMESTERNO)", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
                                                if (hdfSem.Value == "3")
                                                {
                                                    this.DemandAndDcr();
                                                }
                                                if (hdfSem.Value == "5" || hdfSem.Value == "7" || hdfSem.Value == "9" || hdfSem.Value == "11" || hdfSem.Value == "13" || hdfSem.Value == "15")
                                                {
                                                    //if (ProStatus == 3 || ProStatus == 7)
                                                    //{
                                                        this.DemandAndDcr();
                                                    //}
                                                }
                                                this.ShowStudentDetails();
                                                this.BindProgress();
                                                return;
                                           // }
                                        }

                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Remarks of Superviser on the work done by the candidate is Already Given.", this.Page);
                                    }
                                }

                                if (uano == jointsupervisor)
                                {
                                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    if (remarksup != "")
                                    {
                                        string remarkjsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                        if (remarkjsuper == "")
                                        {

                                            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                                            string output1 = objSC.UpdateSupervisorRemarkStatusCo(objS);
                                            if (output1 != "-99")
                                            {
                                                Session["qualifyTbl"] = null;
                                                objCommon.DisplayMessage("Information Updated Successfully By Joint Supervisor/Expert in Department!!", this.Page);
                                                this.ShowStudentDetails();
                                            }
                                        }
                                        else
                                        {
                                            //objCommon.DisplayMessage("Remarks by Joint Superviser/Expert on the work done by the candidate is Already Given.", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
                                    }
                                }
                                ///////////////////// added for exta supervisor /////////////////////////////
                                if (uano == secondsupervisor)
                                {
                                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    if (remarksup != "")
                                    {
                                        string jointsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                        if (jointsuperremark != "")
                                        {
                                            string jointsecondsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));

                                            if (jointsecondsuperremark == "")
                                            {
                                                if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                                                string output1 = objSC.UpdateSupervisorRemarkStatusSecondCo(objS);
                                                if (output1 != "-99")
                                                {
                                                    Session["qualifyTbl"] = null;
                                                    objCommon.DisplayMessage("Information Updated Successfully By Second Joint Supervisor!!", this.Page);
                                                    this.ShowStudentDetails();
                                                    this.BindProgress();
                                                }
                                            }
                                            else
                                            {
                                                // objCommon.DisplayMessage("Remarks by Second Joint Superviser on the work done by the candidate is Already Given.", this.Page);
                                            }
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage("Please Forward application to first Joint Supervisor.!!", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Please Forward application to first Supervisor.!!", this.Page);
                                    }
                                }
                                /////////////////////////////////////////////////////////////////////////////
                                if (uano == instituefaculty)
                                {
                                    #region comment
                                    ///////////////////// added for extra supervisor //////////////////////////////
                                    //if (secondsupervisor > 0)
                                    //{                                       
                                    //    //string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    //    //if (remarksecondsuper != string.Empty)
                                    //    //{
                                    //    //    // second supervisor confirm this application
                                    //    //}
                                    //    //else
                                    //    //{
                                    //    //    objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
                                    //    //}
                                    //}
                                    #endregion

                                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    string noofdgc = objCommon.LookUp("ACD_PHD_DGC", "NOOFDGC", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
                                    string joint = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));

                                    if (noofdgc == "3")
                                    {
                                        remarksup = "outside";
                                    }
                                    if (joint != "")
                                    {
                                        remarksup = "outside";
                                    }
                                    //**** commented by vaibhav m on date 11012017
                                    if (remarksup != "")
                                    {
                                        string remarkinstitudefac = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                        if (remarkinstitudefac == "")
                                        {
                                            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                                            if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
                                            //if (txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = "Satisfatory";
                                            string output1 = objSC.UpdateRemarkdgcCom(objS, txtComments.Text, "I");
                                            if (output1 != "-99")
                                            {
                                                Session["qualifyTbl"] = null;
                                                objCommon.DisplayMessage("Information Updated Successfully By Institute Faculty!!", this.Page);
                                                this.ShowStudentDetails();
                                                this.BindProgress();

                                            }
                                        }
                                        else
                                        {
                                            //objCommon.DisplayMessage("Institude Faculty Comments are Already Given.", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
                                    }
                                }
                                if (uano == drc)
                                {
                                    /////////////////// added for extra supervisor //////////////////////////////
                                    //if (secondsupervisor > 0)
                                    //{
                                    //    string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    //    if (remarksecondsuper != string.Empty)
                                    //    {
                                    //        // second supervisor confirm this application
                                    //    }
                                    //    else
                                    //    {
                                    //        objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
                                    //    }
                                    //}
                                    /////////////////////////////////////////////////////////////////////////////

                                    //**** commented by vaibhav m on date 11012017

                                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));

                                    if (remarksup != "")
                                    {
                                        string remarkdrc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                        if (remarkdrc == "")
                                        {
                                            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                                            if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
                                            string output1 = objSC.UpdateRemarkdgcCom(objS, txtComments.Text, "D");
                                            if (output1 != "-99")
                                            {
                                                Session["qualifyTbl"] = null;
                                                objCommon.DisplayMessage("Information Updated Successfully By DRC Nominee!!", this.Page);
                                                this.ShowStudentDetails();
                                                this.BindProgress();
                                            }
                                        }
                                        else
                                        {
                                            //objCommon.DisplayMessage("DRC Nominee Comments are Already Given.", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
                                    }
                                    //**** upto here
                                }
                                //----add   drcchairman  -----  condition -- 16042019
                                if (uano == drcchairman)
                                {
                                    /////////////////// added for extra supervisor //////////////////////////////
                                    //if (secondsupervisor > 0)
                                    //{
                                    //    string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    //    if (remarksecondsuper != string.Empty)
                                    //    {
                                    //        // second supervisor confirm this application
                                    //    }
                                    //    else
                                    //    {
                                    //        objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
                                    //    }
                                    //}
                                    /////////////////////////////////////////////////////////////////////////////
                                    string remarksup = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    if (remarksup != "")
                                    {
                                        string jointsuperremark = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                        if (jointsuperremark != "")
                                        {
                                            string remarkins = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                            if (remarkins != "")
                                            {
                                                string remarkdgc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                                if (remarkdgc != "")
                                                {
                                                    //string drcnominee = objCommon.LookUp("ACD_PHD_DGC", "OUTNOMINEE", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));
                                                    //if (drcnominee != "")
                                                    //{
                                                    //    remarkdgc = "outside";
                                                    //}
                                                    //if (remarkjoint == "")
                                                    //{
                                                    //    objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
                                                    //    return;
                                                    //}
                                                    //else if (remarkins == "")
                                                    //{
                                                    //    objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
                                                    //    return;
                                                    //}
                                                    //else if (remarkdgc != "")
                                                    //{
                                                    string remarkdgcchairman = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDRCCHAIRMAN", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                                    if (remarkdgcchairman == "")
                                                    {
                                                        if (txtRemark.Text == string.Empty)
                                                        {
                                                            objS.Remark = "OK";
                                                        }
                                                        else
                                                        {
                                                            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                                                        }

                                                        if (txtComments.Text == string.Empty)
                                                        {
                                                            objS.Remark = "OK";
                                                        }
                                                        else
                                                        {
                                                            if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
                                                        }

                                                        string output1 = objSC.UpdateRemarkdgcCom(objS, txtComments.Text, "DR");
                                                        if (output1 != "-99")
                                                        {
                                                            Session["qualifyTbl"] = null;
                                                            objCommon.DisplayMessage("Drc Chairman Information Updated Successfully. Now, Student is Eligible for Fees Payment!!", this.Page);
                                                            this.ShowStudentDetails();
                                                            this.BindProgress();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        objCommon.DisplayMessage("DRC ChairPerson Comments are Already Given.", this.Page);
                                                    }
                                                }
                                                else
                                                {
                                                    objCommon.DisplayMessage("Please Forward application to DGC Nominee first!!", this.Page);
                                                }
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage("Please Forward application to Institute Faculty first!!", this.Page);
                                            }
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage("Please Forward application to Joint Supervisor first!!", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Please Forward application to Supervisor first!!", this.Page);
                                    }

                                }
                                //  ---  drcchairman end  


                                if (ua_type == "4")
                                {
                                    /////////////////// added for extra supervisor //////////////////////////////
                                    //if (secondsupervisor > 0)
                                    //{
                                    //    string remarksecondsuper = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKSECCOSUPER", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    //    if (remarksecondsuper != string.Empty)
                                    //    {
                                    //        // second supervisor confirm this application
                                    //    }
                                    //    else
                                    //    {
                                    //        objCommon.DisplayMessage("First supervisor not yet confirm this application!!", this.Page);
                                    //    }
                                    //}
                                    /////////////////////////////////////////////////////////////////////////////
                                    string remarkjoint = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARK", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    string remarkins = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKINSTITUTE", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    string remarkdgc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDGC", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));
                                    string drcnominee = objCommon.LookUp("ACD_PHD_DGC", "OUTNOMINEE", "IDNO =" + Convert.ToInt32(txtIDNo.Text.Trim()));

                                    string remarkdrc = objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "REMARKDRCCHAIRMAN", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND SESSIONNO =" + (Convert.ToInt32(ViewState["Sessionprogress"])));

                                    if (drcnominee != "")
                                    {
                                        remarkdgc = "outside";
                                    }
                                    if (remarkjoint == "")
                                    {
                                        objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
                                        return;
                                    }
                                    else if (remarkins == "")
                                    {
                                        objCommon.DisplayMessage("Please Forward application to Institue faculty first!!", this.Page);
                                        return;
                                    }
                                    else if (remarkdgc == "")
                                    {
                                        objCommon.DisplayMessage("Please Forward application to DGC Nominee first!!", this.Page);
                                        return;
                                    }
                                    else if (remarkdrc != "" && remarkdgc != "" && remarkins != "")
                                    {
                                        if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                                        //if (!txtComments.Text.Trim().Equals(string.Empty)) objS.Remark = txtComments.Text.Trim();
                                        string output1 = objSC.UpdateRemarkdgcCom(objS, "", "DE");
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                            objCommon.DisplayMessage("Information Updated Successfully By Dean!!", this.Page);
                                            this.ShowStudentDetails();
                                            this.BindProgress();
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Please Forward application to DGC Member first!!", this.Page);
                                    }

                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Student not forword the application to Supervisor", this.Page);
                            return;
                        }
                    }
                }
            }

            #region Commented by Sneha
            //this.BindProgress();
            //Check the Status of the Supervisor if status yes to permission for the HODs

            //int chksuperstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
            //if (chksuperstatus > 0)
            //{
            //    int chkSupervisorStatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "SUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));

            //    if (chkSupervisorStatus == 1)
            //    {
            //        if (ua_type == "3" && ua_dec == "1")
            //        {
            //            objS.IdNo = Convert.ToInt32(txtIDNo.Text);
            //            string output = objSC.UpdateHODStatus(objS);
            //            if (output != "-99")
            //            {
            //                objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage("Supervisor not forword this student application", this.Page);
            //        return;
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Cannot the Assign the DGC members", this.Page);
            //    return;
            //}

            //Update the DRC status by login DGC and admin.

            //if (ua_type == "6" || ua_type == "1" || ua_type == "4")
            //{
            //    objS.IdNo = Convert.ToInt32(txtIDNo.Text);
            //    string output = objSC.UpdateDRCStatus(objS);
            //    if (output != "-99")
            //    {
            //        objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
            //        ShowReport("PHDAnnexureConfirm", "rptAnnexureConfirmation.rpt");
            //    }
            //}

            #endregion
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text) + ",@P_SESSIONNO=" + (Convert.ToInt32(ViewState["Sessionprogress"]));
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QUALIFYNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("PhdProgressReport", "rptPhdProgressReportstudent.rpt");
    }

    protected void btnProgrApply_Click(object sender, EventArgs e)
    {
        ShowReportApplystudent("PhdProgressReport", "Phd_Stud_ApplyProgress.rpt");
    }

    private void ShowReportApplystudent(string reportTitle, string rptFileName)
    {
        try
        {
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            if (ua_type == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO= 0";
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region DCR and Demand-- 01122017

    public void DemandAndDcr()
    {
        ReceptNo.Text = this.GetNewReceiptNo();
        if (ReceptNo.Text != string.Empty)
        {
            if (this.AddDemand() == 1)
            {
                if (this.AddDCR() == 1)
                {
                    Result1 = 1;
                }
            }
        }

    }

    public int AddDCR()
    {
        try
        {
            FeeDemand objEntityClass = new FeeDemand();
            objEntityClass.SessionNo = Convert.ToInt32(lblsession.ToolTip.ToString());
            objEntityClass.StudentId = Convert.ToInt32(lblRegNo.ToolTip.ToString());
            objEntityClass.EnrollmentNo = lblEnrollNo.Text == string.Empty ? "" : lblEnrollNo.Text.ToString().Trim();
            objEntityClass.ReceiptTypeCode = ViewState["Receiptcode"].ToString() == string.Empty ? "" : ViewState["Receiptcode"].ToString().Trim();
            objEntityClass.SemesterNo = Convert.ToInt32(hdfSem.Value.ToString());
            objEntityClass.PaymentTypeNo = 1;
            objEntityClass.Remark = ReceptNo.Text == string.Empty ? "" : ReceptNo.Text.ToString().Trim();
            objEntityClass.CounterNo = Convert.ToInt32(ViewState["CounterNo"].ToString() == string.Empty ? "" : ViewState["CounterNo"].ToString().Trim());
            objEntityClass.UserNo = 1;

            int result = objEntityMethodClass.InsertPhdProgressDCR(objEntityClass);

            if (result == 1)
            {
                return result;
            }
            else
            {
                objCommon.DisplayMessage("Error In Saving, Please Try Again", this.Page);
                return result;
            }
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
            return 0;
        }
    }

    public int AddDemand()
    {
        try
        {
            FeeDemand objEntityClass = new FeeDemand();
            objEntityClass.SessionNo = Convert.ToInt32(lblsession.ToolTip.ToString());
            objEntityClass.StudentId = Convert.ToInt32(lblRegNo.ToolTip.ToString());
            objEntityClass.EnrollmentNo = lblEnrollNo.Text == string.Empty ? "" : lblEnrollNo.Text.ToString().Trim();
            objEntityClass.ReceiptTypeCode = ViewState["Receiptcode"].ToString() == string.Empty ? "" : ViewState["Receiptcode"].ToString().Trim();
            objEntityClass.SemesterNo = Convert.ToInt32(hdfSem.Value.ToString());
            objEntityClass.PaymentTypeNo = 1;
            objEntityClass.CounterNo = Convert.ToInt32(ViewState["CounterNo"].ToString() == string.Empty ? "" : ViewState["CounterNo"].ToString().Trim());
            objEntityClass.FeeCatNo = 1;
            int result = objEntityMethodClass.InsertPhdProgressDemand(objEntityClass);

            if (result == 1)
            {
                //  objEntityMethodClass.DisplayMessage("You are Demand Create successfully", this.Page);
                return result;

            }
            else
            {
                objCommon.DisplayMessage("Error In Saving, Please Try Again", this.Page);
                return result;
            }
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
            return 0;
        }
    }

    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), ViewState["Receiptcode"].ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    #endregion

    //Added by Sneha Doble on 23/09/2020.
    private void BindProgress()
    {
        try
        {
            DataSet ds = objEntityMethodClass.GetFacultyRemarkforphdProgress(Convert.ToInt32(lblsession.ToolTip), Convert.ToInt32(lblRegNo.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlprogrss.DataSource = ds;
                lvlprogrss.DataBind();
                lvlprogrss.Visible = true;
            }
            else
            {
                lvlprogrss.DataSource = null;
                lvlprogrss.DataBind();
                lvlprogrss.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_PhdStudProgress.BindProgress() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            //ddlSearch.SelectedIndex = 2;
            if (ddlSearch.SelectedIndex > 0)
                {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
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


                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
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
        //// Panel1.Visible = true;
        // lblNoRecords.Visible = true;
        // //divbranch.Attributes.Add("style", "display:none");
        // //divSemester.Attributes.Add("style", "display:none");
        // //divtxt.Attributes.Add("style", "display:none");
        // string value = string.Empty;
        // if (ddlDropdown.SelectedIndex > 0)
        // {
        //     value = ddlDropdown.SelectedValue;
        // }
        // else
        // {
        //     value = txtSearch.Text;
        // }

        // //ddlSearch.ClearSelection();

        // bindlist(ddlSearch.SelectedItem.Text, value);
        // ddlDropdown.ClearSelection();
        // txtSearch.Text = string.Empty;
        //// div_Studentdetail.Visible = false;
        // //divMSG.Visible = false;
        // //btnPayment.Visible = false;
        //// btnReciept.Visible = false;
        //// divPreviousReceipts.Visible = false;
        // //if (value == "BRANCH")
        // //{
        // //    divbranch.Attributes.Add("style", "display:block");

        // //}
        // //else if (value == "SEM")
        // //{
        // //    divSemester.Attributes.Add("style", "display:block");
        // //}
        // //else
        // //{
        // //    divtxt.Attributes.Add("style", "display:block");
        // //}

        // //ShowDetails();
        // Panel3.Visible = true;

        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            {
            value = ddlDropdown.SelectedValue;
            }
        else
            {
            value = txtSearch.Text;
            }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
       // bindlist(arg[0], arg[1]);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;

        }
    protected void btnClose_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());

        }
}



