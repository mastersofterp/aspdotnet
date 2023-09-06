//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE                                                     
// CREATION DATE : 30-JAN-2012                                                          
// CREATED BY    : ASHISH DHAKATE                             
// MODIFIED DATE :                 
// ADDED BY      :   Dipali Nanore                                
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


public partial class Academic_PhdAnnexure : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int chkdrcstatus = 0;
    string ua_dept = string.Empty;
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
        //********************************
        string Sessionexam = string.Empty;
        //********************************

        ////imgPhoto.ImageUrl = "~/images/nophoto.jpg";
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
                Page.Title = Session["coll_name"].ToString();

                //**************************************
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;
                Sessionexam = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'AnnexA' AND STARTED=1");

                if (Sessionexam != "")
                {
                    ViewState["Sessionexam"] = Sessionexam;
                }
                else
                {
                    ViewState["Sessionexam"] = Session["currentsession"].ToString();
                }
                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "3")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    CheckActivity();
                }
                //**************************************

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                FillDropDown();
                
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                //string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

                ViewState["usertype"] = ua_type;
                ViewState["dec"] = ua_dec;
                if (ViewState["usertype"].ToString() == "2")
                {
                    //pnlId.Visible = false;
                    //DivGenInfo.Visible = true;
                    //DivDrops.Visible = true;
                    //imgCalDateOfBirth.Visible = false;
                    ShowStudentDetails();
                    updEdit.Visible = false;
                    divmain.Visible = false;
                    //nodgc.Visible = false;
                    ////txtRemark.Enabled = false;
                    ////divremark.Visible = true;
                    ////ShowSignDetails();
                    //ViewState["action"] = "edit";
                    //hlink.Visible = false;


                }
                else
                {
                    txtRemark.Enabled = true;
                    nodgc.Visible = true;
                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "6" || ua_type_fac == "4")
                    {
                        pnlDoc.Enabled = false;
                       // pnlId.Enabled = false;
                        ddlCoSupervisor.Enabled = false;
                        ddlSupervisor.Enabled = false;
                        btnReport.Visible = false;
                        btnReject.Visible = true;
                        txtRemark.Enabled = true;
                        btnApply.Visible = true;
                        btnJoint.Visible = true;
                        ddlDRCChairman.Enabled = false;
                        ddlMember4.Enabled = false;
                        hlink.Visible = true;
                    }

                    pnlId.Visible = true;
                    DivDrops.Visible = true;
                    //licredits.Visible = true;
                    //liStatusCategory.Visible = true;
                    //liSupervisorrole.Visible = true;
                    //liSupervisor.Visible = true;
                    //lijointSupervisor.Visible = true;
                    //liDGCMember.Visible = true;
                    //txtRegNo.Enabled = true;
                    // txtEnrollno.Enabled = true;
                    btnReport.Visible = false;
                    //  btnReject.Enabled = false;

                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        //ShowSignDetails();
                    }

                    //added on 30072018 -- for link bulk page 
                    if (ua_type_fac == "1" || ua_type_fac == "4")
                    {
                        //if (Request.QueryString["id"] == null)
                        //{
                        //    if (Convert.ToString(Session["userpreviewidA"]) != null)
                        //    {
                        //        pnlId.Visible = true;
                        //        ViewState["action"] = "edit";
                        //        ShowStudentDetails();

                        //        txtTotCredits.Enabled = ddlAdmBatch.Enabled = ddlStatus.Enabled = txtResearch.Enabled = txtStudentName.Enabled = txtDateOfJoining.Enabled = false;
                        //        txtFatherName.Enabled = ddlStatusCat.Enabled = ddlDepatment.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = false;
                        //        // btnReport.Visible = false;
                        //    }
                        //}
                    }
                }

            }

        }
        else
        {

            //if (Page.Request.Params["__EVENTTARGET"] != null)
            //{
            //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnSearch"))
            //    {
            //        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
            //       // bindlist(arg[0], arg[1]);
            //    }

            //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
            //    {
            //        txtSearch.Text = string.Empty;
            //        lvStudent.DataSource = null;
            //        lvStudent.DataBind();
            //        lblNoRecords.Text = string.Empty;
            //    }
            //}
        }
        divMsg.InnerHtml = string.Empty;
        ddlNdgc.Enabled = false;

        
    }

    //**********************
    private void CheckActivity()
    {
        string sessionno = string.Empty;

        sessionno = ViewState["Sessionexam"].ToString();

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin!!", this.Page);
                pnDisplay.Visible = false;

            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnDisplay.Visible = false;

                if (ViewState["usertype"].ToString() == "3")
                {
                    pnDisplay.Visible = true;
                    btnReject.Visible = false;
                    btnReject.Enabled = false;
                    ddlDGCSupervisor.Enabled = ddlJointSupervisor.Enabled = ddlInstFac.Enabled = ddlDRC.Enabled = ddlDRCChairman.Enabled = ddlJointSupervisor.Enabled = false;
                    ddlCoSupervisor.Enabled = ddlCoSupervisor.Enabled = ddlInstFac.Enabled = ddlJointSupervisorSecond.Enabled = false;
                    ControlActivityOFF();
                }
                if (ViewState["usertype"].ToString() == "2")
                {
                    // === add to find annexure A register or not  ==>  added by dipali on 23072018 
                    string ActiveIdno = objCommon.LookUp("ACD_PHD_DGC", "IDNO", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"].ToString())));
                    if (ActiveIdno != string.Empty)
                    {
                        pnDisplay.Visible = true;
                        ControlActivityOFF();
                    }
                }
            }
        }
        else
        {
            // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            objCommon.DisplayMessage("this Activity has been Stopped. So You Can View Details and Download Certificate .", this.Page);
            pnDisplay.Visible = false;

            if (ViewState["usertype"].ToString() == "3")
            {
                pnDisplay.Visible = true;
                ddlDGCSupervisor.Enabled = ddlJointSupervisor.Enabled = ddlInstFac.Enabled = ddlDRC.Enabled = ddlDRCChairman.Enabled = ddlJointSupervisor.Enabled = false;
                ddlCoSupervisor.Enabled = ddlCoSupervisor.Enabled = ddlInstFac.Enabled = ddlJointSupervisorSecond.Enabled = false;
                ControlActivityOFF();
            }
            if (ViewState["usertype"].ToString() == "2")
            {
                // === add to find annexure A register or not  ==>  added by dipali on 23072018 
                string ActiveIdno = objCommon.LookUp("ACD_PHD_DGC", "IDNO", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"].ToString())));
                if (ActiveIdno != string.Empty)
                {
                    pnDisplay.Visible = true;
                    ControlActivityOFF();
                }
            }
        }

        dtr.Close();
    }
    //**********************
    //==== When activity off display only report for supervisior and student=====================//   added by dipali on 23072018 
    public void ControlActivityOFF()
    {
        txtTotCredits.Enabled = ddlAdmBatch.Enabled = ddlStatus.Enabled = txtResearch.Enabled = txtStudentName.Enabled = txtDateOfJoining.Enabled = false;
        txtFatherName.Enabled = ddlStatusCat.Enabled = ddlDepatment.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = false;
        btnReject.Enabled = false;
    }


    private void FillDropDown()
    {
        try
        {
            string ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            objCommon.FillDropDownList(ddlDepatment, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO=6", "BRANCHNO");
            //objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
            //objCommon.FillDropDownList(ddlCoSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");

            //objCommon.FillDropDownList(ddlDGCSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
            //objCommon.FillDropDownList(ddlJointSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
            //objCommon.FillDropDownList(ddlInstFac, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
            //objCommon.FillDropDownList(ddlDRC, "ACD_DRC_MEMBER", "DRCNO", "DRCNAME", "DRCNO>0", "DRCNO");
            //objCommon.FillDropDownList(ddlSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (UA_DEPTNO = "+ua_dept +" OR "+ua_dept+" = 0)", "UA_NO");
            objCommon.FillDropDownList(ddlSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5)", "ua_fullname");
            objCommon.FillDropDownList(ddlCoSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) ", "ua_fullname");

            objCommon.FillDropDownList(ddlDGCSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) ", "ua_fullname");
            objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) ", "ua_fullname");
            // ADDED FOR EXTRA SUPURVISOR
            objCommon.FillDropDownList(ddlJointSupervisorSecond, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) ", "ua_fullname");
            objCommon.FillDropDownList(ddlInstFac, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) ", "ua_fullname");
            objCommon.FillDropDownList(ddlDRC, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5)", "ua_fullname");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>10", "BATCHNO");
            objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) ", "ua_fullname");
            //objCommon.FillDropDownList(ddlStatusCat, "ACD_PHDSTATUS_CATEGORY", "PHDSTATAUSCATEGORYNO", "PHDSTATAUSCATEGRYNAME", "PHDSTATAUSCATEGORYNO > 0", "PHDSTATAUSCATEGORYNO");
            ddlDRCChairman.SelectedIndex = 1;
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            pnlDoc.Visible = false;
            trDGC.Visible = false;
            trdrc.Visible = false;
            trdrc1.Visible = false;
            divdrc.Visible = false;
            hlink.Visible = false;
            //ddlSupervisor.Enabled = false;
            //ddlCoSupervisor.Enabled = false;
        }
        else
        {
            if (ViewState["usertype"].ToString() == "3" && ViewState["dec"].ToString() == "1")
            {
                trDGC.Visible = false;
                pnlDoc.Visible = false;
                trdrc.Visible = false;
                trdrc1.Visible = false;
                divdrc.Visible = false;
                nodgc.Visible = false;
                string BRno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
                string deptno = objCommon.LookUp("ACD_BRANCH", "DEPTNO", "BRANCHNO=" + Convert.ToInt32(BRno));
                if (ua_dept != deptno)
                {
                    btnSubmit.Enabled = false;
                    divhod.Visible = true;
                }
                hlink.Visible = false;
            }
            else
            {
                trDGC.Visible = true;
            }
            if (Request.QueryString["id"] != null)
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
            else
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
            //pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtSearch.Text = dtr["IDNO"].ToString();
                lblidno.Text = dtr["IDNO"].ToString(); 
                //txtSearch.ToolTip = dtr["REGNO"].ToString();
                txtRegNo.ToolTip = dtr["IDNO"].ToString();
                txtEnrollno.ToolTip = dtr["ENROLLNO"].ToString();
                txtEnrollno.Text = dtr["ENROLLNO"].ToString();
                lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
                txtRegNo.Text = dtr["IDNO"].ToString();
                //txtRegNo.Enabled = false;
                //txtStudentName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                //txtFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                //txtDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");

                lblnames.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");

                ddlDepatment.SelectedValue = "0";//dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
                ddlStatus.SelectedValue = dtr["PHDSTATUS"] == null ? "0" : dtr["PHDSTATUS"].ToString();
                ddlStatus.Enabled = false;


               // ddlDepatment.SelectedValue = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
               //lblDepartment.Text = ddlDepatment.SelectedItem.Text;
                ddlStatus.SelectedValue = dtr["PHDSTATUS"] == null ? "0" : dtr["PHDSTATUS"].ToString();
                lblstatussup.Text = ddlStatus.SelectedItem.Text;
                if (dtr["PHDSTATUS"] == null)
                {
                    partfull.Text = "";

                }
                if (dtr["PHDSTATUS"].ToString() == "1")
                {
                    partfull.Text = "Fulltime";

                }
                if (dtr["PHDSTATUS"].ToString() == "2")
                {
                    partfull.Text = "Parttime";

                }

                ddlAdmBatch.SelectedValue = "0";//dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
                lbladmbatch.Text = "1";// ddlAdmBatch.SelectedItem.Text;
                if (dtr["PHDSTATUS"] == null)
                {
                    partfull.Text = "";

                }
                if (dtr["PHDSTATUS"].ToString() == "1")
                {
                    partfull.Text = "Fulltime";

                }
                if (dtr["PHDSTATUS"].ToString() == "2")
                {
                    partfull.Text = "Parttime";

                }

                ddlAdmBatch.SelectedValue = "0"; //dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
                lbladmbatch.Text = ddlAdmBatch.SelectedItem.Text;
                txtTotCredits.Text = "4";//dtr["CREDITS"].ToString();
             
                if (dtr["JOINSUPERVISORMEMBERNO"].ToString() == "4")
                {
                    txtOutside1.Visible = true;
                    txtOutside.Visible = false;
                    txtOutside1.Text = dtr["PHDCOSUPERVISORNAME"].ToString() == "" ? "" : dtr["PHDCOSUPERVISORNAME"].ToString();
                }
                if (dtr["INSTITUTEFACMEMBERNO"].ToString() == "4")
                {
                    txtOutsideInsti.Visible = true;
                    txtOutsideInsti.SelectedItem.Text = dtr["INSTITUTEFACULTYNAME"].ToString() == "" ? "" : dtr["INSTITUTEFACULTYNAME"].ToString();
                }
                if (dtr["DRCMEMBERNO"].ToString() == "4")
                {
                    txtOutsideDRC.Visible = true;
                    txtOutsideDRC.SelectedItem.Text = dtr["DRCNAME"].ToString() == "" ? "" : dtr["DRCNAME"].ToString();
                }
                //if (dtr["DRCCHAIRMEMBERNO"].ToString() == "4")
                //{
                //    txtOutsideDrcCh.Visible = true;
                //    txtOutsideDrcCh.Text = dtr["DRCCHAIRNAME"].ToString() == "" ? "" : dtr["DRCCHAIRNAME"].ToString();
                //}
                string deptno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "BRANCHNO=" + Convert.ToInt32(ddlDepatment.SelectedValue));
                //if (deptno == "19")
                //{
                //    objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND  (DRCSTATUS='D' OR DRCSTATUS='SD') AND UA_NO=75", "UA_NO");

                //}
                //else if (deptno == "15")
                //{
                //    objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND  (DRCSTATUS='D' OR DRCSTATUS='SD') AND UA_NO=09", "UA_NO");

                //}
                //change remove id 14 -- 6sep
                // else 
                if (deptno == "07")
                {
                    objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) AND  (DRCSTATUS='D' OR DRCSTATUS='SD') AND UA_NO=19", "UA_FULLNAME");
                }
                else
                {
                    //objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE IN (3,5) AND (UA_DEPTNO = " + deptno + " OR " + deptno + " = 0) )", "UA_FULLNAME");

                }
                //ddlDRCChairman.SelectedIndex = 1;
                //for jointsupervisor,institute faculty,drc
                //DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO", "IDNO=" + Convert.ToInt32(txtSearch.Text), "IDNO");
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    int supervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                //    int jointsupervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
                //    int instituefaculty = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
                //    int drc = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
                //    int dcrchair = Convert.ToInt32(dtr["DRCCHAIRMANNO"]);
                //    int uano = Convert.ToInt32(Session["userno"].ToString());

                //    if (ds.Tables[0].Rows[0]["SUPERROLE"].ToString() == "S")
                //    {
                //        lblJoint.Text = "Expert in the field from Department:";
                //        lblJointSupevisor.Text = string.Empty;
                //        Jointid.Visible = false;
                //    }
                //    else
                //    {
                //        lblJoint.Text = "Joint-Supervisor *(s)(if any) :";
                //        string joint = objCommon.LookUp("ACD_PHD_DGC", "DBO.FN_DESC('UANAME',JOINTSUPERVISORNO)JOINT", "IDNO=" + Convert.ToInt32(txtSearch.Text));
                //        if (joint != "")
                //        {
                //            lblJointSupevisor.Text = joint.ToString();
                //            Jointid.Visible = true;
                //        }
                //    }
                //    if (uano == jointsupervisor || uano == instituefaculty || uano == drc || uano == dcrchair)
                //    {
                //        btnSubmit.Text = "Confirm";
                //        ddlCoSupervisor.Enabled = false;
                //        ddlSupervisor.Enabled = false;
                //        ddlSupervisorrole.Enabled = false;
                //        txtTotCredits.Enabled = false;
                //        ddlStatus.Enabled = false;
                //        ddlStatusCat.Enabled = false;
                //        txtResearch.Enabled = false;
                //        pnlDoc.Enabled = false;
                //        btnReject.Visible = true;
                //        txtRemark.Enabled = true;
                //        ddlMember4.Enabled = false;
                //        ddlDRCChairman.Enabled = false;

                //    }
                //    if (uano == supervisor)
                //    {
                //        ddlSupervisorrole.Enabled = false;
                //        ddlSupervisor.Enabled = false;
                //        ddlDGCSupervisor.Enabled = false;
                //        string superstatus = objCommon.LookUp("ACD_PHD_DGC", "DRCCHAIRMANNO", "IDNO=" + Convert.ToInt32(txtSearch.Text));
                //        if (superstatus == "0" || superstatus == "")
                //        {
                //            btnReject.Visible = true;
                //            pnlDoc.Enabled = true;
                //            txtRemark.Enabled = false;
                //        }
                //        else
                //        {
                //            btnReject.Visible = true;
                //            pnlDoc.Enabled = false;
                //            txtRemark.Enabled = true;
                //        }

                //    }
                //    if (ds.Tables[0].Rows[0]["SUPERROLE"].ToString() == "T")
                //        ddlNdgc.SelectedValue = "5";                      
                //    else
                //        ddlNdgc.SelectedValue = ds.Tables[0].Rows[0]["NOOFDGC"].ToString() == "0" ? "4" : ds.Tables[0].Rows[0]["NOOFDGC"].ToString();

                //    if (ds.Tables[0].Rows[0]["NOOFDGC"].ToString() == "3")
                //    {
                //        expertrow.Visible = false;
                //    }
                //    else
                //    {
                //        expertrow.Visible = true;
                //    }
                //}
                ////if (Convert.ToInt32(ddlStatus.SelectedValue) > 0)
                ////{
                ////    objCommon.FillDropDownList(ddlStatusCat, "ACD_PHDSTATUS_CATEGORY", "PHDSTATAUSCATEGORYNO", "PHDSTATAUSCATEGRYNAME", "PHDSTATAUSCAT=" + Convert.ToInt32(ddlStatus.SelectedValue), "PHDSTATAUSCATEGORYNO");
                ////}
                //ddlStatus.SelectedValue = dtr["FULLPART"] == null ? "0" : dtr["FULLPART"].ToString();
                //lblstatussup.Text = ddlStatus.SelectedItem.Text;
                //ddlSupervisorrole.SelectedValue = dtr["SUPERROLE"] == null ? "0" : dtr["SUPERROLE"].ToString();

                //// ADDED FOR EXTRA SUPERVISOR
                //if (ddlSupervisorrole.SelectedValue == "T")
                //{ secondsupervisor.Visible = true; }
                //else { secondsupervisor.Visible = false; }
                //// END
                //ddlStatusCat.SelectedValue = "1"; //dtr["PHDSTATUSCAT"] == null ? "0" : dtr["PHDSTATUSCAT"].ToString();
                //ddlSupervisor.SelectedValue = dtr["PHDSUPERVISORNO"] == null ? "0" : dtr["PHDSUPERVISORNO"].ToString();
                //ddlCoSupervisor.SelectedValue = dtr["PHDCOSUPERVISORNO1"] == null ? "0" : dtr["PHDCOSUPERVISORNO1"].ToString();
                lblname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lbldate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                ////ADD THE SOME VALUES 

                //ddlDGCSupervisor.SelectedValue ="1"; //dtr["SUPERVISORNO"] == null ? "0" : dtr["SUPERVISORNO"].ToString();
                //ddlMember.SelectedValue = dtr["SUPERVISORMEMBERNO"] == null ? "0" : dtr["SUPERVISORMEMBERNO"].ToString();

               // ddlJointSupervisor.SelectedValue ="2";// dtr["JOINTSUPERVISORNO"] == null ? "0" : dtr["JOINTSUPERVISORNO"].ToString();
                //ddlMember1.SelectedValue = dtr["JOINSUPERVISORMEMBERNO"] == null ? "0" : dtr["JOINSUPERVISORMEMBERNO"].ToString();
                //ddlMember5.SelectedValue = dtr["SECONDJOINSUPERVISORMEMBERNO"] == null ? "0" : dtr["SECONDJOINSUPERVISORMEMBERNO"].ToString();
                //if (ddlMember1.SelectedValue == "4")
                //{
                //    txtOutside1.Visible = false;
                //    txtOutside.Visible = true;
                //    //txtOutside1.Text = dtr["OUTMEMBER"] == null ? "0" : dtr["OUTMEMBER"].ToString();
                //    objCommon.FillDropDownList(txtOutside, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
                //    txtOutside.SelectedValue = dtr["JOINTOUTMEMBER"] == null ? "0" : dtr["JOINTOUTMEMBER"].ToString();
                //    ddlJointSupervisor.Enabled = false;
                //}
                //else
                //{
                //    txtOutside1.Visible = false;
                //    txtOutside.Visible = false;
                //    ddlJointSupervisor.Enabled = true;
                //}

                ////added by neha 28/01/2020
                //ddlMember5.SelectedValue = dtr["SECONDJOINSUPERVISORMEMBERNO"] == null ? "0" : dtr["SECONDJOINSUPERVISORMEMBERNO"].ToString();
                //if (ddlMember5.SelectedValue == "4")
                //{
                //    txtSecondSupervisor1.Visible = false;
                //    txtSecondSupervisorOutside.Visible = true;
                //    objCommon.FillDropDownList(txtSecondSupervisorOutside, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
                //    txtSecondSupervisorOutside.SelectedValue = dtr["SECONDJOINTOUTMEMBER"] == null ? "0" : dtr["SECONDJOINTOUTMEMBER"].ToString();
                //    ddlJointSupervisorSecond.Enabled = false;
                //}
                //else
                //{
                //    txtSecondSupervisor1.Visible = false;
                //    txtSecondSupervisorOutside.Visible = false;
                //    ddlJointSupervisorSecond.Enabled = true;
                //}



                //ddlInstFac.SelectedValue = dtr["INSTITUTEFACULTYNO"] == null ? "0" : dtr["INSTITUTEFACULTYNO"].ToString();
                //ddlMember2.SelectedValue = dtr["INSTITUTEFACMEMBERNO"] == null ? "0" : dtr["INSTITUTEFACMEMBERNO"].ToString();

                //ddlDRC.SelectedValue = dtr["DRCNO"] == null ? "0" : dtr["DRCNO"].ToString();
                //ddlMember3.SelectedValue = dtr["DRCMEMBERNO"] == null ? "0" : dtr["DRCMEMBERNO"].ToString();

                //if (ViewState["usertype"].ToString() != "2")
                //{
                //    ddlDRCChairman.SelectedValue = dtr["DRCCHAIRMANNO"] == null ? "0" : dtr["DRCCHAIRMANNO"].ToString();
                //    ddlMember4.SelectedValue = dtr["DRCCHAIRMEMBERNO"] == null ? "0" : dtr["DRCCHAIRMEMBERNO"].ToString();
                //}
                ////chkdrcstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "DRCNO=" + Convert.ToInt32(ddlDRC.SelectedValue)));
                ////if (chkdrcstatus > 0)
                ////{
                //if (ViewState["usertype"].ToString() == "4")
                //{
                //    btnReject.Visible = true;
                //    hlink.Visible = true;
                //}
                ////}
                txtResearch.Text = "MACHINE LEARNING";//dtr["RESEARCH"].ToString();
                ////check the dean status if dean status (drcstatus) is 1 then show the report button


                //int count = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "IDNO=" + Convert.ToInt32(dtr["IDNO"])));
                //if (count > 0)
                //{
                //    //int drcstatus = Convert.ToInt32(dtr["DRCSTATUS"]);
                //    int deanstatus = Convert.ToInt32(dtr["DEAN_STATUS"]);
                //    int dcrstatus = Convert.ToInt32(dtr["DRCSTATUS"]);
                //    int superstatus = Convert.ToInt32(dtr["SUPERVISORSTATUS"]);
                //    int jointsupervisor = Convert.ToInt32(dtr["JOINTSUPERVISORSTATUS"]);
                //    int institutefac = Convert.ToInt32(dtr["INSTITUTEFACULTYSTATUS"]);
                //    int dcrchairstatus = Convert.ToInt32(dtr["DRCCHAIRMANSTATUS"]);
                //    string remark = dtr["REJECTREMARK"].ToString();
                //    // ADDED FOR EXTRA SUPERVISOR

                //    int secondsupervisorno = 0;
                //    int secondsupervisormemberno = 0;
                //    int secondsupervisorstatus = 0;

                //    if (dtr["SECONDJOINTSUPERVISORNO"] != DBNull.Value)
                //    {
                //        secondsupervisorno = Convert.ToInt32(dtr["SECONDJOINTSUPERVISORNO"]);
                //        ddlJointSupervisorSecond.SelectedValue = secondsupervisorno.ToString();
                //    }
                //    if (dtr["SECONDJOINSUPERVISORMEMBERNO"] != DBNull.Value)
                //    {
                //        secondsupervisormemberno = (Convert.ToInt32(dtr["SECONDJOINSUPERVISORMEMBERNO"]) as int?) ?? 0;
                //        ddlMember5.SelectedValue = secondsupervisormemberno.ToString();
                //    }
                //    if (dtr["SECONDJOINTSUPERVISORSTATUS"] != DBNull.Value)
                //    {
                //        secondsupervisorstatus = (Convert.ToInt32(dtr["SECONDJOINTSUPERVISORSTATUS"]) as int?) ?? 0;
                //    }

                //    if (ddlSupervisorrole.SelectedValue == "T")
                //    {
                //        if (deanstatus == 1 && dcrstatus == 1 && superstatus == 1 && jointsupervisor == 1 && institutefac == 1 && dcrchairstatus == 1 && secondsupervisorstatus == 1)
                //        {
                //            btnReport.Visible = true;
                //            btnReject.Visible = false;
                //            ddlSupervisor.Enabled = false;
                //            ddlJointSupervisor.Enabled = false;
                //            ddlCoSupervisor.Enabled = false;
                //            btnSubmit.Enabled = false;
                //            btnSubmit.Visible = false;
                //            divremark.Visible = false;
                //            divConfirm.Visible = true;
                //        }
                //        else if (deanstatus == 0 && dcrstatus == 0 && superstatus == 0 && jointsupervisor == 0 && institutefac == 0 && dcrchairstatus == 0 && remark != "" && secondsupervisorstatus == 0)
                //        {
                //            btnReport.Visible = false;
                //            btnReject.Visible = true;
                //            divremark.Visible = true;
                //            divConfirm.Visible = false;

                //        }
                //        else
                //        {
                //            btnReject.Visible = true;
                //            btnReport.Visible = false;
                //            divremark.Visible = false;
                //        }
                //    }
                //    // end  
                //    else
                //    {
                //        if (deanstatus == 1 && dcrstatus == 1 && superstatus == 1 && jointsupervisor == 1 && institutefac == 1 && dcrchairstatus == 1)
                //        {
                //            btnReport.Visible = true;
                //            btnReject.Visible = false;
                //            ddlSupervisor.Enabled = false;
                //            ddlJointSupervisor.Enabled = false;
                //            ddlCoSupervisor.Enabled = false;
                //            btnSubmit.Enabled = false;
                //            btnSubmit.Visible = false;
                //            divremark.Visible = false;
                //            divConfirm.Visible = true;
                //        }
                //        else if (deanstatus == 0 && dcrstatus == 0 && superstatus == 0 && jointsupervisor == 0 && institutefac == 0 && dcrchairstatus == 0 && remark != "")
                //        {
                //            btnReport.Visible = false;
                //            btnReject.Visible = true;
                //            divremark.Visible = true;
                //            divConfirm.Visible = false;

                //        }
                //        else
                //        {
                //            btnReject.Visible = true;
                //            btnReport.Visible = false;
                //            divremark.Visible = false;

                //            ddlStatusCat.Enabled = false;
                //            txtTotCredits.Enabled = false;
                //            txtResearch.Enabled = false;
                //            ddlSupervisor.Enabled = false;
                //            ddlSupervisorrole.Enabled = false;
                //            ddlJointSupervisor.Enabled = true;
                //            ddlCoSupervisor.Enabled = false;
                //        }
                //    }
               // }



                //divmain.Visible = true;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure.aspx");
        }
    }

    private void ClearControl()
    {
        txtSearch.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtEnrollno.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        Session["qualifyTbl"] = null;
    }

    private void SubmitData()
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        try
        {
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            if (ua_type == "1" || ua_type == "2" || ua_type == "3" || ua_type == "4")
            {
                if (txtTotCredits.Text == string.Empty)
                {
                    txtTotCredits.Text = "0";
                }

                objS.IdNo = Convert.ToInt32(Session["idno"]);
                objS.EnrollNo = txtEnrollno.Text.Trim();
                objS.RegNo = txtRegNo.Text.Trim();
                objS.RollNo = txtRegNo.Text.Trim();
                //if (!txtStudentName.Text.Trim().Equals(string.Empty)) objS.StudName = txtStudentName.Text.Trim();
                //if (!txtFatherName.Text.Trim().Equals(string.Empty)) objS.FatherName = txtFatherName.Text.Trim();
                //if (!txtDateOfJoining.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime (txtDateOfJoining.Text.Trim());

                if (!lblnames.Text.Trim().Equals(string.Empty)) objS.StudName = lblnames.Text.Trim();
                if (!lblfathername.Text.Trim().Equals(string.Empty)) objS.FatherName = lblfathername.Text.Trim();
                if (!lbljoiningdate.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(lbljoiningdate.Text.Trim());

                objS.BranchNo = 1;// Convert.ToInt32(ddlDepatment.SelectedValue);
                objS.PhdSupervisorNo = 1;//Convert.ToInt32(ddlSupervisor.SelectedValue);
                objS.PhdCoSupervisorNo1 = Convert.ToInt32(ddlCoSupervisor.SelectedValue);
                objS.SupervisorNo = Convert.ToInt32(ddlDGCSupervisor.SelectedValue);
                objS.SupervisorNo = 1;//Convert.ToInt32(ddlSupervisor.SelectedValue);
                objS.SupervisormemberNo = Convert.ToInt32(ddlMember.SelectedValue);
                // objS.JoinsupervisorNo = Convert.ToInt32(ddlCoSupervisor.SelectedValue);
                objS.JoinsupervisorNo = Convert.ToInt32(ddlJointSupervisor.SelectedValue);
                objS.JoinsupervisormemberNo = Convert.ToInt32(ddlMember1.SelectedValue);

                // ADDED FOR EXTRA SUPERVISOR
                objS.Secondjoinsupervisorno = Convert.ToInt32(ddlJointSupervisorSecond.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlJointSupervisorSecond.SelectedValue);
                objS.Secondjoinsupervisormemberno = Convert.ToInt32(ddlMember5.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlMember5.SelectedValue);

                if (ua_type == "3")
                {
                    objS.InstitutefacultyNo = Convert.ToInt32(ddlInstFac.SelectedValue);//== 0 ? 0 : Convert.ToInt32(ddlInstFac.SelectedValue);
                    objS.InstitutefacmemberNo = Convert.ToInt32(ddlMember2.SelectedValue);//== 0 ? 0 : Convert.ToInt32(ddlMember2.SelectedValue);
                    objS.DrcNo = Convert.ToInt32(ddlDRC.SelectedValue); // == 0 ? 0 : Convert.ToInt32(ddlDRC.SelectedValue);
                    objS.DrcmemberNo = Convert.ToInt32(ddlMember3.SelectedValue); // == 0 ? 0 : Convert.ToInt32(ddlMember3.SelectedValue);
                    objS.DrcChairNo = Convert.ToInt32(ddlDRCChairman.SelectedValue); //== 0 ? 0 : Convert.ToInt32(ddlDRCChairman.SelectedValue);
                    objS.DrcChairmemberNo = Convert.ToInt32(ddlMember4.SelectedValue); // == 0 ? 0 : Convert.ToInt32(ddlMember4.SelectedValue);
                }
                objS.AdmDate = Convert.ToDateTime("2021-11-27 12:13:17.377");//Convert.ToDateTime(lbljoiningdate.Text);
                objS.AdmBatch = 1;// Convert.ToInt32(ddlAdmBatch.SelectedValue);
                objS.Phdstatuscat = Convert.ToInt32(ddlStatusCat.SelectedValue);
                objS.Credits = Convert.ToInt32(txtTotCredits.Text.Trim());
                objS.CollegeCode = Session["colcode"].ToString();

                objS.PhdStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                objS.SuperRole = ddlSupervisorrole.SelectedValue;
                objS.Research = txtResearch.Text;
                //UANO SAVE
                if (ua_type == "1" || ua_type == "3")
                {
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    objS.Uano = 0;
                }
                // UPDATE THE ONLY PHD STUDENT DATA THROUGH STUDENT
                if (ua_type == "1" || ua_type == "2")
                {
                    // add phd admission cancel condition  on 17042018 
                    //DataSet dsjoin = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO", "SUM(CASE WHEN SUPERROLE = 'J' THEN 1 ELSE 0 END)J,SUM(CASE WHEN SUPERROLE = 'S' THEN 1 ELSE 0 END)S", "SUPERVISORNO =" + ddlSupervisor.SelectedValue + " GROUP BY SUPERVISORNO", "");
                    //  DataSet dsjoin = objCommon.FillDropDown("(SELECT SUPERVISORNO,SUM(CASE WHEN SUPERROLE='J' THEN 1 ELSE 0 END)JOINTLY,SUM(CASE WHEN SUPERROLE='S' THEN 1 ELSE 0 END)SINGLY ,ADMCANCEL	FROM ACD_PHD_DGC where ISNULL(ADMCANCEL ,0)=0  GROUP BY SUPERVISORNO ,ADMCANCEL)A LEFT OUTER JOIN (SELECT JOINTSUPERVISORNO,SUM(CASE WHEN SUPERROLE='J' THEN 1 ELSE 0 END)JOINTLY ,SUM(CASE WHEN SUPERROLE='S' THEN 1 ELSE 0 END)SINGLY , ADMCANCEL FROM ACD_PHD_DGC where ISNULL(ADMCANCEL ,0)=0  GROUP BY JOINTSUPERVISORNO,ADMCANCEL)B ON(A.SUPERVISORNO =B.JOINTSUPERVISORNO)", "SUPERVISORNO,DBO.FN_DESC('UANAME',A.SUPERVISORNO)SUPERVISOR", "(ISNULL(A.JOINTLY,0)+ISNULL(B.JOINTLY,0))J,A.SINGLY AS S", "SUPERVISORNO =" + ddlSupervisor.SelectedValue + " AND ISNULL(A.ADMCANCEL,0) = 0 ", "");

                    DataSet dsjoin = objSC.GetJointlySinglyCount(Convert.ToInt32(ddlSupervisor.SelectedValue));

                    if (dsjoin.Tables[0].Rows.Count == 0)
                    {
                        if (ddlSupervisorrole.SelectedIndex > 0)
                        {
                            string output = objSC.UpdatePHDStudent(objS);
                            if (output != "-99")
                            {
                                Session["qualifyTbl"] = null;
                                objCommon.DisplayMessage("Student Information Update Successfully!!", this.Page);
                                ControlActivityOFF();

                                this.ShowStudentDetails();

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Select Supervisor role !!", this.Page);
                        }
                    }
                    else
                    {  //20188

                        // (a) 5  0
                        // (b) 4  2
                        // (c) 3  4
                        // (d) 2  6
                        // (e) 1  7
                        // (f) 0  8

                        string singly, jointly = string.Empty;

                        singly = dsjoin.Tables[0].Rows[0]["SINGLY"].ToString();
                        jointly = dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString();

                        if (ddlSupervisorrole.SelectedItem.Text == "Singly")
                        {
                            singly = (Convert.ToInt32(singly) + 1).ToString();
                        }
                        else if (ddlSupervisorrole.SelectedItem.Text == "Jointly" || ddlSupervisorrole.SelectedItem.Text == "Multiple")
                        {
                            jointly = (Convert.ToInt32(jointly) + 1).ToString();
                        }

                        int flag = 0;

                        if (Convert.ToInt32(singly) <= 5 && Convert.ToInt32(jointly) == 0)
                        {
                            flag = 0;
                        }
                        else if (Convert.ToInt32(singly) <= 4 && Convert.ToInt32(jointly) <= 2)
                        {

                            flag = 0;
                        }
                        else if (Convert.ToInt32(singly) <= 3 && Convert.ToInt32(jointly) <= 4)
                        {
                            flag = 0;

                        }

                        else if (Convert.ToInt32(singly) <= 2 && Convert.ToInt32(jointly) <= 6)
                        {
                            flag = 0;
                        }

                        else if (Convert.ToInt32(singly) <= 1 && Convert.ToInt32(jointly) <= 7)
                        {
                            flag = 0;
                        }
                        else if (Convert.ToInt32(singly) == 0 && Convert.ToInt32(jointly) <= 8)
                        {
                            flag = 0;
                        }
                        else
                        {
                            flag = 1;
                        }

                        if (flag == 0)
                        {
                            string output = objSC.UpdatePHDStudent(objS);
                            if (output != "-99")
                            {
                                Session["qualifyTbl"] = null;
                                objCommon.DisplayMessage("Student Information Updated Successfully!!", this.Page);
                                ControlActivityOFF();
                                this.ShowStudentDetails();
                            }
                            else
                            {
                                objCommon.DisplayMessage("Error Occured while Updating Record!!", this.Page);
                            }
                        }
                        else
                        {
                            //objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                            int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/PhdAnnexure.aspx'"));

                            ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('Supervisor limit exceed!!');window.location ='PhdAnnexure.aspx?pageno=" + pageno + "';",
true);

                            ddlJointSupervisor.SelectedIndex = 0;
                            // Response.Redirect(Request.Url.ToString());
                        }


                        #region Commented
                        //if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "5" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "0")
                        //{
                        //    if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "4" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "2")
                        //    {
                        //        if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "3" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "4")
                        //        {
                        //            if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "2" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "6")
                        //            {
                        //                if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "1" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "7")
                        //                {
                        //                    if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "0" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "8")
                        //                    {
                        //                        if (Convert.ToInt32(dsjoin.Tables[0].Rows[0]["SINGLY"].ToString()) <= 5 && Convert.ToInt32(dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString()) <= 8)
                        //                        {

                        //                            string output = objSC.UpdatePHDStudent(objS);
                        //                            if (output != "-99")
                        //                            {
                        //                                Session["qualifyTbl"] = null;
                        //                                objCommon.DisplayMessage("Student Information Updated Successfully!!", this.Page);
                        //                                ControlActivityOFF();
                        //                                this.ShowStudentDetails();
                        //                            }
                        //                            else
                        //                            {
                        //                                objCommon.DisplayMessage("Error Occured while Updating Record!!", this.Page);
                        //                            }

                        //                        }
                        //                        else
                        //                        {
                        //                            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //                        }
                        //                    }
                        //                    else
                        //                    {
                        //                        objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //                }
                        //            }
                        //            else
                        //            {
                        //                objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //            }
                        //        }
                        //        else
                        //        {
                        //            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //        }
                        //    }
                        //    else
                        //    {
                        //        objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //    }
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);

                        //}
                        #endregion
                    }
                }

                // ADDED FOR EXTRA SUPERVISOR
                DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,ISNULL(DRCCHAIRMANNO,0)DRCCHAIRMANNO,ISNULL(SECONDJOINTSUPERVISORNO,0)SECONDJOINTSUPERVISORNO", "IDNO=" + Convert.ToInt32(txtSearch.Text), "IDNO");

                int supervisor = Convert.ToInt32(ds.Tables[0].Rows.Count >= 0 ? ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString() : "0");
                int jointsupervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
                int instituefaculty = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
                int drc = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
                int drcchair = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString());


                // ADDED FOR EXTRA SUPERVISOR
                int secondjointsupervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["SECONDJOINTSUPERVISORNO"].ToString());

                int uano = Convert.ToInt32(Session["userno"].ToString());
                if (btnSubmit.Text == "Submit")
                {
                    if (uano == supervisor)
                    {
                        // check the supervisor login
                        if (ua_type == "3" && ua_dec == "0")
                        {
                            //Check the Status of the student
                            //if (ddlDRC.SelectedIndex > 0)
                            //{
                            //    chkdrcstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "DRCNO=" + Convert.ToInt32(ddlDRC.SelectedValue)));

                            //    if (chkdrcstatus == 0)
                            //    {
                            int chkSupervisorStatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "SUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtSearch.Text.Trim())));
                            if (chkSupervisorStatus == 1)
                            {
                                if (ddlSupervisorrole.SelectedIndex > 0)
                                {
                                    //int Rolecount = 0;
                                    //Rolecount = Convert.ToInt32(objSC.GetSupervisorJSRoleCount(objS));
                                    //if (Rolecount > 0)
                                    //{


                                    //if (chkstudentstatus > 0)
                                    //{
                                    if (ddlMember1.SelectedValue == "4")
                                    {
                                        string outsidemem = txtOutside.SelectedItem.Text;
                                        // string output1 = objSC.UpdateDGCSupervisorOutside(objS, txtOutside1.Text.ToString(), "J");
                                        string output1 = objSC.UpdateDGCSupervisorOutside(objS, txtOutside.SelectedItem.Text, "J", Convert.ToInt32(txtOutside.SelectedValue.ToString())); // NEHA 27/01/2020
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                        }
                                    }

                                    //27/01/2020 NEHA
                                    if (ddlMember5.SelectedValue == "4")
                                    {
                                        string outsidemem5 = txtSecondSupervisorOutside.Text;
                                        string output1 = objSC.UpdateDGCSupervisorOutside(objS, txtSecondSupervisorOutside.SelectedItem.Text, "T", Convert.ToInt32(txtSecondSupervisorOutside.SelectedValue.ToString()));
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                        }
                                    }


                                    if (ddlMember2.SelectedValue == "4")
                                    {
                                        string outsidemem1 = txtOutsideInsti.SelectedItem.Text;
                                        string output1 = objSC.UpdateDGCSupervisorOutside(objS, outsidemem1, "I", 0);
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                        }
                                    }
                                    if (ddlMember3.SelectedValue == "4")
                                    {
                                        string outsidemem2 = txtOutsideDRC.SelectedItem.Text;
                                        string output1 = objSC.UpdateDGCSupervisorOutside(objS, outsidemem2, "D", 0);
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                        }
                                    }
                                    if (ddlMember4.SelectedValue == "4")
                                    {
                                        string outsidemem3 = txtOutsideDrcCh.Text;
                                        string output1 = objSC.UpdateDGCSupervisorOutside(objS, outsidemem3, "DR", 0);
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                        }
                                    }
                                    if (ddlNdgc.SelectedValue == "3")
                                    {
                                        string output1 = objSC.UpdateDGCSupervisor(objS, "U");
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                            objCommon.DisplayMessage("Information Updated Successfully!!", this.Page);
                                            this.ShowStudentDetails();
                                        }
                                    }
                                    //added for extra supervisor

                                    if (ddlNdgc.SelectedValue == "5")
                                    {
                                        string output1 = objSC.UpdateDGCSupervisor(objS, "T");
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                            objCommon.DisplayMessage("Information Updated Successfully!!", this.Page);
                                            this.ShowStudentDetails();
                                        }
                                    }

                                    else
                                    {
                                        string output1 = objSC.UpdateDGCSupervisor(objS, "F");
                                        if (output1 != "-99")
                                        {
                                            Session["qualifyTbl"] = null;
                                            objCommon.DisplayMessage("Information Updated Successfully!!", this.Page);
                                            this.ShowStudentDetails();
                                        }
                                    }
                                    //}
                                    //else
                                    //{
                                    //    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                                    //}

                                }
                                else
                                {
                                    objCommon.DisplayMessage("Please Select Supervisor role !!", this.Page);
                                }


                            }
                            else
                            {
                                //objCommon.DisplayMessage("Student not forword the application to Supervisor", this.Page);
                                objCommon.DisplayMessage("HOD has not forwarded the application to Supervisor", this.Page);
                                return;
                            }
                        }
                    }
                }

                //for hod
                //confirmed the assigned supervisor and  can change.  status yes to permission for assigned supervisor.

                int chksuperstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "IDNO=" + Convert.ToInt32(txtSearch.Text.Trim())));
                if (chksuperstatus > 0)
                {
                    //int chkSupervisorStatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "SUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtSearch.Text.Trim())));

                    //if (chkSupervisorStatus == 1)
                    //{
                    if (ua_type == "3" && ua_dec == "1")
                    {
                        objS.IdNo = Convert.ToInt32(txtSearch.Text);
                        objS.PhdSupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
                        objS.PhdCoSupervisorNo1 = Convert.ToInt32(ddlCoSupervisor.SelectedValue);
                        string output = objSC.UpdateHODStatus(objS);
                        if (output != "-99")
                        {
                            objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
                        }
                    }
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage("Supervisor not forword this student application", this.Page);
                    //    return;
                    //}
                }
                else
                {
                    objCommon.DisplayMessage("Cannot the Assign the DGC members", this.Page);
                    return;
                }

                DataSet dscheck = objCommon.FillDropDown("ACD_PHD_DGC", "ISNULL(SUPERVISORSTATUS,0)SUPERVISORSTATUS,ISNULL(JOINTSUPERVISORSTATUS,0)JOINTSUPERVISORSTATUS", "ISNULL(INSTITUTEFACULTYSTATUS,0)INSTITUTEFACULTYSTATUS,ISNULL(DRCSTATUS,0)DRCSTATUS,ISNULL(DRCCHAIRMANSTATUS,0)DRCCHAIRMANNO,ISNULL(SECONDJOINTSUPERVISORSTATUS,0)SECONDJOINTSUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtSearch.Text), "IDNO");

                string jointlyrole = objCommon.LookUp("ACD_PHD_DGC", "SUPERROLE", "IDNO=" + Convert.ToInt32(txtSearch.Text));

                //joint supervisor login
                if (uano == jointsupervisor)
                {
                    objS.IdNo = Convert.ToInt32(txtSearch.Text);
                    string output = objSC.UpdateDRCStatus(objS, "J");
                    if (output != "-99")
                    {
                        if (jointlyrole == "S")
                        {
                            objCommon.DisplayMessage("Status Updated by Experts Successfully!!", this.Page);

                        }
                        else
                        {
                            objCommon.DisplayMessage("Status Updated by Joint Supervisor Successfully!!", this.Page);
                        }
                    }
                }
                // added for extra supervisor
                if (jointlyrole == "T")
                {

                    //second joint supervisor login
                    if (uano == secondjointsupervisor)
                    {
                        if (dscheck.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString() == "1")
                        {
                            objS.IdNo = Convert.ToInt32(txtSearch.Text);
                            string output = objSC.UpdateDRCStatus(objS, "T");
                            if (output != "-99")
                            {
                                objCommon.DisplayMessage("Status Updated by Second Joint Supervisor Successfully!!", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Approved first from First Joint Supervisor login!!", this.Page);
                        }

                    }
                    //instistute faculty login
                    if (uano == instituefaculty)
                    {
                        if (dscheck.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString() == "1")    // comment as per ravi sir
                        {
                            objS.IdNo = Convert.ToInt32(txtSearch.Text);
                            string output = objSC.UpdateDRCStatus(objS, "I");
                            if (output != "-99")
                            {
                                objCommon.DisplayMessage("Status Updated by Institute Faculty Successfully!!", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Approved first from Second Joint Supervisor login!!", this.Page);
                        }
                    }

                }
                // END
                else
                {
                    //instistute faculty login
                    if (uano == instituefaculty)
                    {
                        //if (dscheck.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString() == "1")  // comment because any dgc member login and confirm -- 23072018
                        //{
                        objS.IdNo = Convert.ToInt32(txtSearch.Text);
                        string output = objSC.UpdateDRCStatus(objS, "I");
                        if (output != "-99")
                        {
                            objCommon.DisplayMessage("Status Updated by Institute Faculty Successfully!!", this.Page);
                        }
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage("Please Approved first from Joint Supervisor login!!", this.Page);
                        //}
                    }
                }


                // for drc chairman
                //if (ua_type == "6")
                //{
                if (uano == drc)
                {
                    //if (dscheck.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString() == "1")
                    //{
                    objS.IdNo = Convert.ToInt32(txtSearch.Text);
                    string output = objSC.UpdateDRCStatus(objS, "D");
                    if (output != "-99")
                    {
                        objCommon.DisplayMessage("Status Updated by DRC Nominee Successfully!!", this.Page);
                    }
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage("Please Approved first from Institute Faculty login!!", this.Page);
                    //}
                }
                if (uano == drcchair)
                {

                    DataSet ds1 = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRMANSTATUS", "IDNO=" + Convert.ToInt32(txtSearch.Text), "IDNO");
                    int sup = Convert.ToInt32(ds1.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString());

                    int joint = ds1.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString());
                    int institue = ds1.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString());
                    int dr = ds1.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCSTATUS"].ToString());
                    if (sup == 1 && joint == 1 && institue == 1 && dr == 1)
                    {
                        objS.IdNo = Convert.ToInt32(txtSearch.Text);
                        string output = objSC.UpdateDRCStatus(objS, "DC");
                        if (output != "-99")
                        {
                            objCommon.DisplayMessage("Status Updated by DRC Chairman Successfully!!", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Approve Supervisor first from DGC member!!", this.Page);
                    }
                }

                //for dean

                if (ua_type == "1" || ua_type == "4")
                {
                    DataSet ds2 = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRMANSTATUS,ISNULL(SECONDJOINTSUPERVISORSTATUS,0)SECONDJOINTSUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtSearch.Text), "IDNO");
                    int sup = Convert.ToInt32(ds2.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString());

                    int joint = Convert.ToInt32(ds2.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString());
                    int institue = ds2.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds2.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString());
                    int dr = ds2.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds2.Tables[0].Rows[0]["DRCSTATUS"].ToString());
                    int drch = ds2.Tables[0].Rows[0]["DRCCHAIRMANSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds2.Tables[0].Rows[0]["DRCCHAIRMANSTATUS"].ToString());
                    int secondsup = ds2.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds2.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString());
                    // added for extra supervisor
                    if (ddlSupervisorrole.SelectedValue == "T")
                    {
                        if (sup == 1 && joint == 1 && institue == 1 && dr == 1 && drch == 1 && secondsup == 1)
                        {
                            objS.IdNo = Convert.ToInt32(txtSearch.Text);
                            string output = objSC.UpdateDeanStatus(objS);
                            if (output != "-99")
                            {
                                objCommon.DisplayMessage("Status Updated by Dean Successfully!!", this.Page);
                                if (ddlNdgc.SelectedValue == "4")
                                {
                                    ShowReport("PHDAnnexureConfirm", "rptAnnexureConfirmation.rpt");
                                }
                                else if (ddlNdgc.SelectedValue == "3")
                                {
                                    ShowReport("PHDAnnexureConfirm3", "rptAnnexureConfirmation3member.rpt");
                                }
                                else
                                {
                                    ShowReport("PHDAnnexureConfirm5", "rptAnnexureConfirmation5member.rpt");
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Approve Supervisor first from DGC member & DRC chairman!!", this.Page);
                        }
                    }
                    else
                    {
                        if (sup == 1 && joint == 1 && institue == 1 && dr == 1 && drch == 1)
                        {
                            objS.IdNo = Convert.ToInt32(txtSearch.Text);
                            string output = objSC.UpdateDeanStatus(objS);
                            if (output != "-99")
                            {
                                objCommon.DisplayMessage("Status Updated by Dean Successfully!!", this.Page);
                                if (ddlNdgc.SelectedValue == "4")
                                {
                                    ShowReport("PHDAnnexureConfirm", "rptAnnexureConfirmation.rpt");
                                }
                                else
                                {
                                    ShowReport("PHDAnnexureConfirm3", "rptAnnexureConfirmation3member.rpt");
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Approve Supervisor first from DGC member & DRC chairman!!", this.Page);
                        }
                    }
                }
            }
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

    //protected void lnkId_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnk = sender as LinkButton;
    //    string url = string.Empty;
    //    if (Request.Url.ToString().IndexOf("&id=") > 0)
    //    {
    //        url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
    //    }
    //    else
    //    {
    //        url = Request.Url.ToString();
    //    }

    //    Response.Redirect(url + "&id=" + lnk.CommandArgument);

    //}

    //private void bindlist(string category, string searchtext)
    //{

    //    //int dept = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

    //    //if (dept > 0)
    //    //{
    //    //    string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "DEGREENO=6 AND DEPTNO=" + dept);


    //    //    StudentController objSC = new StudentController();
    //    //    DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

    //    //    if (ds.Tables[0].Rows.Count > 0)
    //    //    {
    //    //        lvStudent.DataSource = ds;
    //    //        lvStudent.DataBind();
    //    //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
    //    //    }
    //    //    else
    //    //        lblNoRecords.Text = "Total Records : 0";
    //    //}
    //    //else
    //    //{
    //    string branchno = "0";

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
    //    //}
    //}

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panellistview.Visible = true;
           // divReceiptType.Visible = false;
          //  divStudSemester.Visible = false;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        if (ua_type == "3" && ua_dec == "1" || ua_type == "2" && ua_dec == "0") { }
        else
        {
            if (ddlSupervisorrole.SelectedValue == "0")
            { objCommon.DisplayMessage("Please select supervisor role jointly/singly/multiple.", this.Page); return; }
            if (ddlSupervisorrole.SelectedValue == "J" || ddlSupervisorrole.SelectedValue == "S")
            {
                if (ddlMember.SelectedValue == "0" || ddlMember1.SelectedValue == "0" || ddlMember2.SelectedValue == "0" || ddlMember3.SelectedValue == "0") { objCommon.DisplayMessage("Please select Doctoral guidance commitee member role.", this.Page); return; }
            }

            if (ddlSupervisorrole.SelectedValue == "T")
            {
                if (ddlMember.SelectedValue == "0" || ddlMember1.SelectedValue == "0" || ddlMember2.SelectedValue == "0" || ddlMember3.SelectedValue == "0" || ddlMember5.SelectedValue == "0") { objCommon.DisplayMessage("Please select Doctoral guidance commitee member role.", this.Page); return; }
            }

            if (ddlMember1.SelectedValue == "4")
            { if (ddlJointSupervisor.SelectedValue != "0" || txtOutside1.Text == "") { objCommon.DisplayMessage("Please check you have fill joint supervisor properly.", this.Page); return; } }
            if (ddlMember2.SelectedValue == "4")
            { if (ddlInstFac.SelectedValue != "0" || txtOutsideInsti.SelectedItem.Text == "") { objCommon.DisplayMessage("Please check you have fill institute faculty expert properly.", this.Page); return; } }




            if (ddlMember3.SelectedValue == "4")
            { if (ddlDRC.SelectedValue != "0" || txtOutsideDRC.SelectedItem.Text == "") { objCommon.DisplayMessage("Please check you have fill DRC nominee properly.", this.Page); return; } }


            //27/01/2020 NEHA
            if (ddlMember5.SelectedValue == "4")
            { if (ddlJointSupervisorSecond.SelectedValue != "0" || txtSecondSupervisorOutside.SelectedItem.Text == "") { objCommon.DisplayMessage("Please check you have fill second joint supervisor properly.", this.Page); return; } }


        }


        //StudentController objSC = new StudentController();
        //Student objS = new Student();
        //string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        //string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        //if (ddlJointSupervisor.SelectedValue != "0" && ddlInstFac.SelectedValue != "0" && ddlDRC.SelectedValue != "0")
        //{
        //    SubmitData();
        //}



        if ((ddlJointSupervisor.SelectedValue != "0" && ddlInstFac.SelectedValue != "0" && ddlDRC.SelectedValue != "0") || ua_type == "4")
        {
            SubmitData();
        }
        //27/01/2020 NEHA
        else if (ua_type == "3" && ua_dec == "0" && ddlJointSupervisor.SelectedValue == "0" && ddlMember1.SelectedValue == "4" && txtOutside1.Text != string.Empty || ddlInstFac.SelectedValue == "0" && ddlMember2.SelectedValue == "4" && txtOutsideInsti.SelectedItem.Text != string.Empty || ddlDRC.SelectedValue == "0"
            && ddlMember3.SelectedValue == "4" && txtOutsideDRC.SelectedItem.Text != string.Empty && ddlMember5.SelectedValue == "4" && txtSecondSupervisorOutside.SelectedItem.Text != string.Empty)
        {
            SubmitData();
        }
        else if (ua_type == "3" && ua_dec == "1" || ua_type == "2" && ua_dec == "0")
        {
            SubmitData();
        }
        else
        {
            objCommon.DisplayMessage("Please Fill DGC All Information !!", this.Page);
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtSearch.Text);
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
        if (ddlNdgc.SelectedValue == "4")
        {
            ShowReport("PHDAnnexureConfirm", "rptAnnexureConfirmation.rpt");
        }
        else if (ddlNdgc.SelectedValue == "3")
        {
            ShowReport("PHDAnnexureConfirm3", "rptAnnexureConfirmation3member.rpt");
        }
        else
        {
            ShowReport("PHDAnnexureConfirm5", "rptAnnexureConfirmation5member.rpt");
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {

        StudentController objSC = new StudentController();
        Student objS = new Student();
        if (ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "3")
        {
            if (txtRemark.Text != "")
            {
                objS.IdNo = Convert.ToInt32(txtSearch.Text);
                objS.PhdSupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
                objS.PhdCoSupervisorNo1 = Convert.ToInt32(ddlCoSupervisor.SelectedValue);
                string Remark = txtRemark.Text;
                string output = objSC.RejectSupervisorStatus(objS, Remark);
                if (output != "-99")
                {
                    objCommon.DisplayMessage("Supervisor Rejected Successfully!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Insert Remark for Cancellation!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized for Reject Supervisor!!", this.Page);
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        ShowReportApplystudent("PHDApplystudent", "Phd_Stud_Apply.rpt");
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
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtSearch.Text);
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

    protected void ddlMember1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember1.SelectedValue == "4")
        {
            // --add outside member -- list -- 08012019
            txtOutside.Visible = true;
            txtOutside1.Visible = false;
            objCommon.FillDropDownList(txtOutside, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            ddlJointSupervisor.SelectedValue = "0";

            ddlJointSupervisor.Enabled = false;
        }
        else
        {
            //   objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            txtOutside.Visible = false;
            ddlJointSupervisor.Enabled = true;
        }
    }

    protected void btnJoint_Click(object sender, EventArgs e)
    {
        ShowReportjointly("NoofScholarspersupervisor", "rptPhDJntlysngly.rpt");
    }

    private void ShowReportjointly(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SUPERVISIORNO= 0";
            //   url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupervisor.SelectedIndex > 0)
        {


            objCommon.FillDropDownList(ddlDGCSupervisor, "user_Acc", "ua_no", "ua_fullname", "ua_no=" + ddlSupervisor.SelectedValue, "");
            ddlDGCSupervisor.Focus();
        }
        else
        {
            ddlSupervisorrole.SelectedIndex = 0;
            ddlSupervisorrole.Focus();
        }
        ddlSupervisorrole.SelectedIndex = 0;
    }

    protected void ddlSupervisorrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSupervisorrole.SelectedValue == "S")
        //{
        //    lblJoint.Text = "Expert in the field from Department :";
        //}
        //else
        //{
        //    lblJoint.Text = "Joint-Supervisor *(s)(if any) :";
        //}

        StudentController objSC = new StudentController();
        if (ddlSupervisorrole.SelectedValue == "J" || ddlSupervisorrole.SelectedValue == "T" || ddlSupervisorrole.SelectedValue == "S")
        {
            //    DataSet dsjoin = objSC.GetJointlySinglyCount(Convert.ToInt32(ddlSupervisor.SelectedValue));

            //    if (dsjoin.Tables[0].Rows.Count == 0)
            //    {

            //    }
            //    else
            //    {
            //        string singly, jointly = string.Empty;

            //        singly = dsjoin.Tables[0].Rows[0]["SINGLY"].ToString();
            //        jointly = dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString();

            //        if (ddlSupervisorrole.SelectedItem.Text == "Singly")
            //        {
            //            singly = (Convert.ToInt32(singly) + 1).ToString();
            //        }
            //        else if (ddlSupervisorrole.SelectedItem.Text == "Jointly" || ddlSupervisorrole.SelectedItem.Text == "Multiple")
            //        {
            //            jointly = (Convert.ToInt32(jointly) + 1).ToString();
            //        }

            //        int flag = 0;

            //        if (Convert.ToInt32(singly) <= 5 && Convert.ToInt32(jointly) == 0)
            //        {
            //            flag = 0;
            //        }
            //        else if (Convert.ToInt32(singly) <= 4 && Convert.ToInt32(jointly) <= 2)
            //        {

            //            flag = 0;
            //        }
            //        else if (Convert.ToInt32(singly) <= 3 && Convert.ToInt32(jointly) <= 4)
            //        {
            //            flag = 0;

            //        }

            //        else if (Convert.ToInt32(singly) <= 2 && Convert.ToInt32(jointly) <= 6)
            //        {
            //            flag = 0;
            //        }

            //        else if (Convert.ToInt32(singly) <= 1 && Convert.ToInt32(jointly) <= 7)
            //        {
            //            flag = 0;
            //        }
            //        else if (Convert.ToInt32(singly) == 0 && Convert.ToInt32(jointly) <= 8)
            //        {
            //            flag = 0;
            //        }
            //        else
            //        {
            //            flag = 1;
            //        }

            //        if (flag == 0)
            //        {

            //        }
            //        else
            //        {
            //            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
            //            ddlJointSupervisor.SelectedIndex = 0;
            //        }
            //    }
            //}
            //// added for extra supervisor
            //if (ddlSupervisorrole.SelectedValue == "S")
            //{
            //    lblJoint.Text = "Expert in the field from Department :";
            //    secondsupervisor.Visible = false;
            //}
            //else if (ddlSupervisorrole.SelectedValue == "J")
            //{
            //    lblJoint.Text = "Joint-Supervisor *(s)(if any) :";
            //    secondsupervisor.Visible = false;
            //}
            //else
            //{
            //    lblJoint.Text = "Joint-Supervisor *(s)(if any) :";
            //    secondsupervisor.Visible = true;
            //    expertrow.Visible = true;
            //    ddlNdgc.SelectedValue = "5";
            //}


        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStatusCat, "ACD_PHDSTATUS_CATEGORY", "PHDSTATAUSCATEGORYNO", "PHDSTATAUSCATEGRYNAME", "PHDSTATAUSCAT=" + Convert.ToInt32(ddlStatus.SelectedValue), "PHDSTATAUSCATEGORYNO");

    }

    protected void ddlNdgc_SelectedIndexChanged(object sender, EventArgs e)
    {
        // added for extra supervisor
        if (ddlSupervisorrole.SelectedValue != "T")
        {
            if (ddlNdgc.SelectedValue == "3")
            {
                ddlJointSupervisor.ClearSelection();
                ddlMember1.ClearSelection();
                expertrow.Visible = false;
            }
            else
            {
                expertrow.Visible = true;
            }
        }
        else
        {
            ddlNdgc.SelectedValue = "5";
        }
    }

    protected void ddlMember2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember2.SelectedValue == "4")
        {
            txtOutsideInsti.Visible = true;      //   -- add outside member 
            objCommon.FillDropDownList(txtOutsideInsti, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            ddlInstFac.SelectedValue = "0";
            ddlInstFac.Enabled = false;
        }
        else
        {
            // objCommon.FillDropDownList(ddlInstFac, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            txtOutsideInsti.Visible = false;
            ddlInstFac.Enabled = true;
        }
    }

    protected void ddlMember3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember3.SelectedValue == "4")
        {
            objCommon.FillDropDownList(txtOutsideDRC, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            txtOutsideDRC.Visible = true;
            ddlDRC.SelectedValue = "0";
            ddlDRC.Enabled = false;
        }
        else
        {
            //  objCommon.FillDropDownList(ddlDRC, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            txtOutsideDRC.Visible = false;
            ddlDRC.Enabled = true;
        }
    }

    protected void ddlMember4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember4.SelectedValue == "4")
        {
            txtOutsideDrcCh.Visible = true;
            // objCommon.FillDropDownList(txtOutsideDrcCh, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            ddlDRCChairman.SelectedValue = "0";
            ddlDRCChairman.Enabled = false;
        }
        else
        {
            // objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='D' OR DRCSTATUS='SD')", "ua_fullname");
            txtOutsideDrcCh.Visible = false;
            //ddlDRCChairman.SelectedValue = "0";
            ddlDRCChairman.Enabled = true;
        }
    }

    protected void txtOutside_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOutside1.Text = txtOutside.SelectedItem.Text;
    }

    //  added by dipali on 22102019 for  Singly jointly count 
    protected void ddlJointSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        if (ddlSupervisorrole.SelectedValue == "J" || ddlSupervisorrole.SelectedValue == "T")
        {
            DataSet dsjoin = objSC.GetJointlySinglyCount(Convert.ToInt32(ddlJointSupervisor.SelectedValue));

            if (dsjoin.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                string singly, jointly = string.Empty;

                singly = dsjoin.Tables[0].Rows[0]["SINGLY"].ToString();
                jointly = dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString();

                if (ddlSupervisorrole.SelectedItem.Text == "Singly")
                {
                    singly = (Convert.ToInt32(singly) + 1).ToString();
                }
                else if (ddlSupervisorrole.SelectedItem.Text == "Jointly" || ddlSupervisorrole.SelectedItem.Text == "Multiple")
                {
                    jointly = (Convert.ToInt32(jointly) + 1).ToString();
                }

                int flag = 0;

                if (Convert.ToInt32(singly) <= 5 && Convert.ToInt32(jointly) == 0)
                {
                    flag = 0;
                }
                else if (Convert.ToInt32(singly) <= 4 && Convert.ToInt32(jointly) <= 2)
                {

                    flag = 0;
                }
                else if (Convert.ToInt32(singly) <= 3 && Convert.ToInt32(jointly) <= 4)
                {
                    flag = 0;

                }

                else if (Convert.ToInt32(singly) <= 2 && Convert.ToInt32(jointly) <= 6)
                {
                    flag = 0;
                }

                else if (Convert.ToInt32(singly) <= 1 && Convert.ToInt32(jointly) <= 7)
                {
                    flag = 0;
                }
                else if (Convert.ToInt32(singly) == 0 && Convert.ToInt32(jointly) <= 8)
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                }

                if (flag == 0)
                {

                }
                else
                {
                    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                    ddlJointSupervisor.SelectedIndex = 0;
                }


                #region Commented
                //if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "5" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "0")
                //{
                //    if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "4" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "2")
                //    {
                //        if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "3" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "4")
                //        {
                //            if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "2" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "6")
                //            {
                //                if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "1" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "7")
                //                {
                //                  if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "0" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "8")
                //                   {
                //                     if (Convert.ToInt32(dsjoin.Tables[0].Rows[0]["SINGLY"].ToString()) <= 5 && Convert.ToInt32(dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString()) <= 8)
                //                        {
                //                            //  ---  eligible for singly jointly 
                //                        }
                //                        else
                //                        {
                //                            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                            ddlJointSupervisor.SelectedIndex = 0;
                //                        }
                //                   }
                //                   else
                //                   {
                //                        objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                        ddlJointSupervisor.SelectedIndex = 0;
                //                   }
                //                }
                //                else
                //                {
                //                    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                    ddlJointSupervisor.SelectedIndex = 0;
                //                }
                //            }
                //            else
                //            {
                //                objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                ddlJointSupervisor.SelectedIndex = 0;
                //            }
                //        }
                //        else
                //        {
                //            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //            ddlJointSupervisor.SelectedIndex = 0;
                //        }
                //    }
                //    else
                //    {
                //        objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //        ddlJointSupervisor.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //    ddlJointSupervisor.SelectedIndex = 0;
                //}
                #endregion
            }

        }
    }

    protected void ddlJointSupervisorSecond_SelectedIndexChanged(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        if (ddlSupervisorrole.SelectedValue == "T")
        {
            DataSet dsjoin = objSC.GetJointlySinglyCount(Convert.ToInt32(ddlJointSupervisorSecond.SelectedValue));

            if (dsjoin.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                string singly, jointly = string.Empty;

                singly = dsjoin.Tables[0].Rows[0]["SINGLY"].ToString();
                jointly = dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString();

                if (ddlSupervisorrole.SelectedItem.Text == "Singly")
                {
                    singly = (Convert.ToInt32(singly) + 1).ToString();
                }
                else if (ddlSupervisorrole.SelectedItem.Text == "Jointly" || ddlSupervisorrole.SelectedItem.Text == "Multiple")
                {
                    jointly = (Convert.ToInt32(jointly) + 1).ToString();
                }

                int flag = 0;

                if (Convert.ToInt32(singly) <= 5 && Convert.ToInt32(jointly) == 0)
                {
                    flag = 0;
                }
                else if (Convert.ToInt32(singly) <= 4 && Convert.ToInt32(jointly) <= 2)
                {

                    flag = 0;
                }
                else if (Convert.ToInt32(singly) <= 3 && Convert.ToInt32(jointly) <= 4)
                {
                    flag = 0;

                }

                else if (Convert.ToInt32(singly) <= 2 && Convert.ToInt32(jointly) <= 6)
                {
                    flag = 0;
                }

                else if (Convert.ToInt32(singly) <= 1 && Convert.ToInt32(jointly) <= 7)
                {
                    flag = 0;
                }
                else if (Convert.ToInt32(singly) == 0 && Convert.ToInt32(jointly) <= 8)
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                }

                if (flag == 0)
                {

                }
                else
                {
                    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                    ddlJointSupervisorSecond.SelectedIndex = 0;
                }

                #region Commented
                //if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "5" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "0")
                //{
                //    if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "4" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "2")
                //    {
                //        if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "3" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "4")
                //        {
                //            if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "2" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "6")
                //            {
                //                if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "1" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "7")
                //                {
                //                    if (dsjoin.Tables[0].Rows[0]["SINGLY"].ToString() != "0" || dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString() != "8")
                //                    {
                //                        if (Convert.ToInt32(dsjoin.Tables[0].Rows[0]["SINGLY"].ToString()) <= 5 && Convert.ToInt32(dsjoin.Tables[0].Rows[0]["JOINTLY"].ToString()) <= 8)
                //                        {
                //                            //  ---  eligible for singly jointly 
                //                        }
                //                        else
                //                        {
                //                            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                            ddlJointSupervisorSecond.SelectedIndex = 0;

                //                        }
                //                    }
                //                    else
                //                    {
                //                        objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                        ddlJointSupervisorSecond.SelectedIndex = 0;
                //                    }
                //                }
                //                else
                //                {
                //                    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                    ddlJointSupervisorSecond.SelectedIndex = 0;
                //                }
                //            }
                //            else
                //            {
                //                objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //                ddlJointSupervisorSecond.SelectedIndex = 0;
                //            }
                //        }
                //        else
                //        {
                //            objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //            ddlJointSupervisorSecond.SelectedIndex = 0;
                //        }
                //    }
                //    else
                //    {
                //        objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //        ddlJointSupervisorSecond.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    objCommon.DisplayMessage("Supervisor limit exceed!!", this.Page);
                //    ddlJointSupervisorSecond.SelectedIndex = 0;
                //}
                #endregion
            }

        }
    }

    protected void ddlMember5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember5.SelectedValue == "4")
        {
            // --add outside member -- list -- 08012019
            txtSecondSupervisorOutside.Visible = true;

            objCommon.FillDropDownList(txtSecondSupervisorOutside, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            ddlJointSupervisorSecond.SelectedValue = "0";

            ddlJointSupervisorSecond.Enabled = false;
        }
        else
        {
            //   objCommon.FillDropDownList(ddlJointSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            txtSecondSupervisorOutside.Visible = false;
            ddlJointSupervisorSecond.Enabled = true;
        }


    }

    protected void txtSecondSupervisorOutside_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSecondSupervisor1.Text = txtSecondSupervisorOutside.SelectedItem.Text;
    }

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    if (txtSearchs.Text == string.Empty)
    //    {
    //        //DivGeneralinfo.Visible = false;
    //        objCommon.DisplayMessage(this.UpdateProgress1, "Please Enter IDNO", this.Page);
    //        return;
    //    }
    //    else
    //    {
    //        ShowStudentDetails();
    //    }
    //}
    //protected void btncancelid_Click(object sender, EventArgs e)
    //{

    //}

    //private void ShowStudentDetailsSuper()
    //{
    //    StudentController objSC = new StudentController();
    //    DataTableReader dtr = null;
    //    if (ViewState["usertype"].ToString() == "2")
    //    {
    //        dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
    //        pnlDoc.Visible = false;
    //        trDGC.Visible = false;
    //        trdrc.Visible = false;
    //        trdrc1.Visible = false;
    //        divdrc.Visible = false;
    //        hlink.Visible = false;
    //    }
    //    else
    //    {
    //        if (ViewState["usertype"].ToString() == "3" && ViewState["dec"].ToString() == "1")
    //        {
    //            trDGC.Visible = false;
    //            pnlDoc.Visible = false;
    //            trdrc.Visible = false;
    //            trdrc1.Visible = false;
    //            divdrc.Visible = false;
    //            nodgc.Visible = false;
    //            string BRno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
    //            string deptno = objCommon.LookUp("ACD_BRANCH", "DEPTNO", "BRANCHNO=" + Convert.ToInt32(BRno));
    //            if (ua_dept != deptno)
    //            {
    //                btnSubmit.Enabled = false;
    //                divhod.Visible = true;
    //            }
    //            hlink.Visible = false;
    //        }
    //        else
    //        {
    //            trDGC.Visible = true;
    //        }
    //        if (Request.QueryString["id"] != null)
    //        {
    //            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
    //        }
    //        else
    //        {
    //            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["userpreviewidA"]));
    //        }
    //    }
    //    if (dtr != null)
    //    {
    //        if (dtr.Read())
    //        {
    //            DivGeneralinfo.Visible = true;
    //            lblidno.Text = dtr["IDNO"].ToString();
    //            lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
    //            lblname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
    //            lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
    //            lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
    //            ddlDepatment.SelectedValue = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
    //            lblDepartment.Text = ddlDepatment.SelectedItem.Text;
    //            ddlStatus.SelectedValue = dtr["PHDSTATUS"] == null ? "0" : dtr["PHDSTATUS"].ToString();
    //            lblstatussup.Text = ddlStatus.SelectedItem.Text;
    //            if (dtr["PHDSTATUS"] == null)
    //            {
    //                partfull.Text = "";

    //            }
    //            if (dtr["PHDSTATUS"].ToString() == "1")
    //            {
    //                partfull.Text = "Fulltime";

    //            }
    //            if (dtr["PHDSTATUS"].ToString() == "2")
    //            {
    //                partfull.Text = "Parttime";

    //            }

    //            ddlAdmBatch.SelectedValue = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
    //            lbladmbatch.Text = ddlAdmBatch.SelectedItem.Text;
    //            lblcredit.Text = dtr["CREDITS"].ToString();
    //            if (dtr["JOINSUPERVISORMEMBERNO"].ToString() == "4")
    //            {
    //                txtOutside1.Visible = true;
    //                txtOutside.Visible = false;
    //                txtOutside1.Text = dtr["PHDCOSUPERVISORNAME"].ToString() == "" ? "" : dtr["PHDCOSUPERVISORNAME"].ToString();
    //            }
    //            if (dtr["INSTITUTEFACMEMBERNO"].ToString() == "4")
    //            {
    //                txtOutsideInsti.Visible = true;
    //                txtOutsideInsti.SelectedItem.Text = dtr["INSTITUTEFACULTYNAME"].ToString() == "" ? "" : dtr["INSTITUTEFACULTYNAME"].ToString();
    //            }
    //            if (dtr["DRCMEMBERNO"].ToString() == "4")
    //            {
    //                txtOutsideDRC.Visible = true;
    //                txtOutsideDRC.SelectedItem.Text = dtr["DRCNAME"].ToString() == "" ? "" : dtr["DRCNAME"].ToString();
    //            }
    //            if (dtr["DRCCHAIRMEMBERNO"].ToString() == "4")
    //            {
    //                txtOutsideDrcCh.Visible = true;
    //                txtOutsideDrcCh.Text = dtr["DRCCHAIRNAME"].ToString() == "" ? "" : dtr["DRCCHAIRNAME"].ToString();
    //            }
    //            string deptno = objCommon.LookUp("ACD_BRANCH", "DEPTNO", "BRANCHNO=" + Convert.ToInt32(ddlDepatment.SelectedValue));
               
    //            if (deptno == "07")
    //            {
    //                objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND  (DRCSTATUS='D' OR DRCSTATUS='SD') AND UA_NO=19", "UA_FULLNAME");
    //            }
    //            else
    //            {
    //                objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (UA_DEPTNO = " + deptno + " OR " + deptno + " = 0) AND (DRCSTATUS='D' OR DRCSTATUS='SD')", "UA_FULLNAME");

    //            }
    //            //ddlDRCChairman.SelectedIndex = 1;
    //            //for jointsupervisor,institute faculty,drc
    //            DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO, isnull(DRCCHAIRMANNO,0)DRCCHAIRMANNO,SUPERROLE,ISNULL(NOOFDGC,0)NOOFDGC", "IDNO=" + Convert.ToInt32(txtSearch.Text), "IDNO");
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                int supervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
    //                int jointsupervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
    //                int instituefaculty = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
    //                int drc = Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
    //                int dcrchair = Convert.ToInt32(dtr["DRCCHAIRMANNO"]);
    //                int uano = Convert.ToInt32(Session["userno"].ToString());

    //                if (ds.Tables[0].Rows[0]["SUPERROLE"].ToString() == "S")
    //                {
    //                    lblJoint.Text = "Expert in the field from Department:";
    //                    lblJointSupevisor.Text = string.Empty;
    //                    Jointid.Visible = false;
    //                }
    //                else
    //                {
    //                    lblJoint.Text = "Joint-Supervisor *(s)(if any) :";
    //                    string joint = objCommon.LookUp("ACD_PHD_DGC", "DBO.FN_DESC('UANAME',JOINTSUPERVISORNO)JOINT", "IDNO=" + Convert.ToInt32(txtSearch.Text));
    //                    if (joint != "")
    //                    {
    //                        lblJointSupevisor.Text = joint.ToString();
    //                        Jointid.Visible = true;
    //                    }
    //                }
    //                if (uano == jointsupervisor || uano == instituefaculty || uano == drc || uano == dcrchair)
    //                {
    //                    btnSubmit.Text = "Confirm";
    //                    ddlCoSupervisor.Enabled = false;
    //                    ddlSupervisor.Enabled = false;
    //                    ddlSupervisorrole.Enabled = false;
    //                    txtTotCredits.Enabled = false;
    //                    ddlStatus.Enabled = false;
    //                    ddlStatusCat.Enabled = false;
    //                    txtResearch.Enabled = false;
    //                    pnlDoc.Enabled = false;
    //                    btnReject.Visible = true;
    //                    txtRemark.Enabled = true;
    //                    ddlMember4.Enabled = false;
    //                    ddlDRCChairman.Enabled = false;

    //                }
    //                if (uano == supervisor)
    //                {
    //                    ddlSupervisorrole.Enabled = false;
    //                    ddlSupervisor.Enabled = false;
    //                    ddlDGCSupervisor.Enabled = false;
    //                    string superstatus = objCommon.LookUp("ACD_PHD_DGC", "DRCCHAIRMANNO", "IDNO=" + Convert.ToInt32(txtSearch.Text));
    //                    if (superstatus == "0" || superstatus == "")
    //                    {
    //                        btnReject.Visible = true;
    //                        pnlDoc.Enabled = true;
    //                        txtRemark.Enabled = false;
    //                    }
    //                    else
    //                    {
    //                        btnReject.Visible = true;
    //                        pnlDoc.Enabled = false;
    //                        txtRemark.Enabled = true;
    //                    }

    //                }
    //                if (ds.Tables[0].Rows[0]["SUPERROLE"].ToString() == "T")
    //                    lblnodgc.Text = "5";
    //                else
    //                    ddlNdgc.SelectedValue = ds.Tables[0].Rows[0]["NOOFDGC"].ToString() == "0" ? "4" : ds.Tables[0].Rows[0]["NOOFDGC"].ToString();
    //                lblnodgc.Text = ddlNdgc.SelectedItem.Text;

    //                if (ds.Tables[0].Rows[0]["NOOFDGC"].ToString() == "3")
    //                {
    //                    expertrow.Visible = false;
    //                }
    //                else
    //                {
    //                    expertrow.Visible = true;
    //                }
    //            }

    //            ddlStatus.SelectedValue = dtr["FULLPART"] == null ? "0" : dtr["FULLPART"].ToString();
    //            lblstatussup.Text = ddlStatus.SelectedItem.Text;

    //            ddlSupervisorrole.SelectedValue = dtr["SUPERROLE"] == null ? "0" : dtr["SUPERROLE"].ToString();
    //            lblSupervisorrole.Text = ddlSupervisorrole.SelectedItem.Text;
    //            // ADDED FOR EXTRA SUPERVISOR
    //            if (ddlSupervisorrole.SelectedValue == "T")
    //            { secondsupervisor.Visible = true; }
    //            else { secondsupervisor.Visible = false; }
    //            // END
    //            ddlStatusCat.SelectedValue = dtr["PHDSTATUSCAT"] == null ? "0" : dtr["PHDSTATUSCAT"].ToString();
    //            lblStatusCategory.Text = ddlStatusCat.SelectedItem.Text;

    //            ddlSupervisor.SelectedValue = dtr["PHDSUPERVISORNO"] == null ? "0" : dtr["PHDSUPERVISORNO"].ToString();
    //            lblSupervisor.Text = ddlSupervisor.SelectedItem.Text;

    //            ddlCoSupervisor.SelectedValue = dtr["PHDCOSUPERVISORNO1"] == null ? "0" : dtr["PHDCOSUPERVISORNO1"].ToString();
    //            lbljointSupervisor.Text = ddlCoSupervisor.SelectedItem.Text;

    //            lblname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
    //            lbldate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
    //            //ADD THE SOME VALUES 

    //            ddlDGCSupervisor.SelectedValue = dtr["SUPERVISORNO"] == null ? "0" : dtr["SUPERVISORNO"].ToString();
    //            ddlMember.SelectedValue = dtr["SUPERVISORMEMBERNO"] == null ? "0" : dtr["SUPERVISORMEMBERNO"].ToString();

    //            ddlJointSupervisor.SelectedValue = dtr["JOINTSUPERVISORNO"] == null ? "0" : dtr["JOINTSUPERVISORNO"].ToString();
    //            ddlMember1.SelectedValue = dtr["JOINSUPERVISORMEMBERNO"] == null ? "0" : dtr["JOINSUPERVISORMEMBERNO"].ToString();
    //            ddlMember5.SelectedValue = dtr["SECONDJOINSUPERVISORMEMBERNO"] == null ? "0" : dtr["SECONDJOINSUPERVISORMEMBERNO"].ToString();
    //            if (ddlMember1.SelectedValue == "4")
    //            {
    //                txtOutside1.Visible = false;
    //                txtOutside.Visible = true;
    //                //txtOutside1.Text = dtr["OUTMEMBER"] == null ? "0" : dtr["OUTMEMBER"].ToString();
    //                objCommon.FillDropDownList(txtOutside, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
    //                txtOutside.SelectedValue = dtr["JOINTOUTMEMBER"] == null ? "0" : dtr["JOINTOUTMEMBER"].ToString();
    //                ddlJointSupervisor.Enabled = false;
    //            }
    //            else
    //            {
    //                txtOutside1.Visible = false;
    //                txtOutside.Visible = false;
    //                ddlJointSupervisor.Enabled = true;
    //            }

    //            //added by neha 28/01/2020
    //            ddlMember5.SelectedValue = dtr["SECONDJOINSUPERVISORMEMBERNO"] == null ? "0" : dtr["SECONDJOINSUPERVISORMEMBERNO"].ToString();
    //            if (ddlMember5.SelectedValue == "4")
    //            {
    //                txtSecondSupervisor1.Visible = false;
    //                txtSecondSupervisorOutside.Visible = true;
    //                objCommon.FillDropDownList(txtSecondSupervisorOutside, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
    //                txtSecondSupervisorOutside.SelectedValue = dtr["SECONDJOINTOUTMEMBER"] == null ? "0" : dtr["SECONDJOINTOUTMEMBER"].ToString();
    //                ddlJointSupervisorSecond.Enabled = false;
    //            }
    //            else
    //            {
    //                txtSecondSupervisor1.Visible = false;
    //                txtSecondSupervisorOutside.Visible = false;
    //                ddlJointSupervisorSecond.Enabled = true;
    //            }



    //            ddlInstFac.SelectedValue = dtr["INSTITUTEFACULTYNO"] == null ? "0" : dtr["INSTITUTEFACULTYNO"].ToString();
    //            ddlMember2.SelectedValue = dtr["INSTITUTEFACMEMBERNO"] == null ? "0" : dtr["INSTITUTEFACMEMBERNO"].ToString();

    //            ddlDRC.SelectedValue = dtr["DRCNO"] == null ? "0" : dtr["DRCNO"].ToString();
    //            ddlMember3.SelectedValue = dtr["DRCMEMBERNO"] == null ? "0" : dtr["DRCMEMBERNO"].ToString();

    //            if (ViewState["usertype"].ToString() != "2")
    //            {
    //                ddlDRCChairman.SelectedValue = dtr["DRCCHAIRMANNO"] == null ? "0" : dtr["DRCCHAIRMANNO"].ToString();
    //                ddlMember4.SelectedValue = dtr["DRCCHAIRMEMBERNO"] == null ? "0" : dtr["DRCCHAIRMEMBERNO"].ToString();
    //            }
    //            //chkdrcstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "DRCNO=" + Convert.ToInt32(ddlDRC.SelectedValue)));
    //            //if (chkdrcstatus > 0)
    //            //{
    //            if (ViewState["usertype"].ToString() == "4")
    //            {
    //                btnReject.Visible = true;
    //                hlink.Visible = true;
    //            }
    //            //}
    //            txtResearch.Text = dtr["RESEARCH"].ToString();
    //            //check the dean status if dean status (drcstatus) is 1 then show the report button


    //            int count = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "IDNO=" + Convert.ToInt32(dtr["IDNO"])));
    //            if (count > 0)
    //            {
    //                //int drcstatus = Convert.ToInt32(dtr["DRCSTATUS"]);
    //                int deanstatus = Convert.ToInt32(dtr["DEAN_STATUS"]);
    //                int dcrstatus = Convert.ToInt32(dtr["DRCSTATUS"]);
    //                int superstatus = Convert.ToInt32(dtr["SUPERVISORSTATUS"]);
    //                int jointsupervisor = Convert.ToInt32(dtr["JOINTSUPERVISORSTATUS"]);
    //                int institutefac = Convert.ToInt32(dtr["INSTITUTEFACULTYSTATUS"]);
    //                int dcrchairstatus = Convert.ToInt32(dtr["DRCCHAIRMANSTATUS"]);
    //                string remark = dtr["REJECTREMARK"].ToString();
    //                // ADDED FOR EXTRA SUPERVISOR

    //                int secondsupervisorno = 0;
    //                int secondsupervisormemberno = 0;
    //                int secondsupervisorstatus = 0;

    //                if (dtr["SECONDJOINTSUPERVISORNO"] != DBNull.Value)
    //                {
    //                    secondsupervisorno = Convert.ToInt32(dtr["SECONDJOINTSUPERVISORNO"]);
    //                    ddlJointSupervisorSecond.SelectedValue = secondsupervisorno.ToString();
    //                }
    //                if (dtr["SECONDJOINSUPERVISORMEMBERNO"] != DBNull.Value)
    //                {
    //                    secondsupervisormemberno = (Convert.ToInt32(dtr["SECONDJOINSUPERVISORMEMBERNO"]) as int?) ?? 0;
    //                    ddlMember5.SelectedValue = secondsupervisormemberno.ToString();
    //                }
    //                if (dtr["SECONDJOINTSUPERVISORSTATUS"] != DBNull.Value)
    //                {
    //                    secondsupervisorstatus = (Convert.ToInt32(dtr["SECONDJOINTSUPERVISORSTATUS"]) as int?) ?? 0;
    //                }

    //                if (ddlSupervisorrole.SelectedValue == "T")
    //                {
    //                    if (deanstatus == 1 && dcrstatus == 1 && superstatus == 1 && jointsupervisor == 1 && institutefac == 1 && dcrchairstatus == 1 && secondsupervisorstatus == 1)
    //                    {
    //                        btnReport.Visible = true;
    //                        btnReject.Visible = false;
    //                        ddlSupervisor.Enabled = false;
    //                        ddlJointSupervisor.Enabled = false;
    //                        ddlCoSupervisor.Enabled = false;
    //                        btnSubmit.Enabled = false;
    //                        btnSubmit.Visible = false;
    //                        divremark.Visible = false;
    //                        divConfirm.Visible = true;
    //                    }
    //                    else if (deanstatus == 0 && dcrstatus == 0 && superstatus == 0 && jointsupervisor == 0 && institutefac == 0 && dcrchairstatus == 0 && remark != "" && secondsupervisorstatus == 0)
    //                    {
    //                        btnReport.Visible = false;
    //                        btnReject.Visible = true;
    //                        divremark.Visible = true;
    //                        divConfirm.Visible = false;

    //                    }
    //                    else
    //                    {
    //                        btnReject.Visible = true;
    //                        btnReport.Visible = false;
    //                        divremark.Visible = false;
    //                    }
    //                }
    //                // end  
    //                else
    //                {
    //                    if (deanstatus == 1 && dcrstatus == 1 && superstatus == 1 && jointsupervisor == 1 && institutefac == 1 && dcrchairstatus == 1)
    //                    {
    //                        btnReport.Visible = true;
    //                        btnReject.Visible = false;
    //                        ddlSupervisor.Enabled = false;
    //                        ddlJointSupervisor.Enabled = false;
    //                        ddlCoSupervisor.Enabled = false;
    //                        btnSubmit.Enabled = false;
    //                        btnSubmit.Visible = false;
    //                        divremark.Visible = false;
    //                        divConfirm.Visible = true;
    //                    }
    //                    else if (deanstatus == 0 && dcrstatus == 0 && superstatus == 0 && jointsupervisor == 0 && institutefac == 0 && dcrchairstatus == 0 && remark != "")
    //                    {
    //                        btnReport.Visible = false;
    //                        btnReject.Visible = true;
    //                        divremark.Visible = true;
    //                        divConfirm.Visible = false;

    //                    }
    //                    else
    //                    {
    //                        btnReject.Visible = true;
    //                        btnReport.Visible = false;
    //                        divremark.Visible = false;

    //                        ddlStatusCat.Enabled = false;
    //                        txtTotCredits.Enabled = false;
    //                        txtResearch.Enabled = false;
    //                        ddlSupervisor.Enabled = false;
    //                        ddlSupervisorrole.Enabled = false;
    //                        ddlJointSupervisor.Enabled = false;
    //                        ddlCoSupervisor.Enabled = false;
    //                    }
    //                }
    //            }

    //        }
    //    }
    //}

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
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        //updEdit.Visible = true;
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        //string url = string.Empty;
        //if (Request.Url.ToString().IndexOf("&id=") > 0)
        //    url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        //else
        //    url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();

        //if (lnk.CommandArgument == null)
        //{
        //    string number = Session["StudId"].ToString();
        //    Session["stuinfoidno"] = Convert.ToInt32 (number);
        //}
        //else
        //{
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        Session["idno"] = Session["stuinfoidno"].ToString();
       
        //DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

        //Server.Transfer("PersonalDetails.aspx", false);
       // DisplayInformation(Convert.ToInt32(Session["stuinfoidno"]));
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        divmain.Visible = true;
        
        ShowStudentDetails();
        divmain.Visible = true;
        DivSutLog.Visible=true;
        divGeneralInfo.Visible = true;
        updEdit.Visible = false;
        Panellistview.Visible = false;
       
    }
}



