using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Data;
using BusinessLogicLayer.BusinessEntities.Academic;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

public partial class Semester_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SemesterRegistration objsem = new SemesterRegistration();
    FeeCollectionController objFee = new FeeCollectionController();
    int degreeno = 0;
    int college_id = 0;
    int Installmentcount = 0;

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
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        //string ky = "Your Information in the Student Information Section needs to be updated before proceeding to Semester/ Trimester Admission. \\n \\n Link path --> Academic > Student Related > Student Information";
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Popup", "alert('" + ky + "')", true);


                        //ViewState["idno"] = Session["idno"].ToString();
                        //GetSemesterActivity();
                        //int CheckActivity = 1;
                        // if (CheckActivity == 1)
                        //int CheckActivity = 1;///this has been temporary added need to revert.
                        if (CheckActivity())
                        {
                            Page.Title = Session["coll_name"].ToString();
                            // objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY A INNER JOIN ACD_SESSION_MASTER B ON (A.SESSION_NO = B.SESSIONNO)", "TOP 1 B.SESSIONNO", "B.SESSION_NAME", "ACTIVITY_NO = 1 AND CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)", "B.SESSIONNO DESC");//original


                            ShowStudentDetails(Convert.ToInt32(Session["idno"]));
                            objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY A INNER JOIN ACTIVITY_MASTER B ON A.ACTIVITY_NO = B.ACTIVITY_NO  INNER JOIN ACD_SESSION_MASTER C ON SESSION_NO = SESSIONNO", "SESSIONNO", "SESSION_NAME", "PAGE_LINK LIKE '%" + Convert.ToString(Request.QueryString["pageno"].ToString()) + "%' AND STARTED = 1 and CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112) AND " + Convert.ToInt32(Session["COLLEGE_ID"]) + " IN(SELECT VALUE FROM DBO.SPLIT(A.COLLEGE_IDS,',') WHERE VALUE <>'')", "");
                            ddlSession.SelectedIndex = 1;
                            objCommon.FillDropDownList(ddlbank, "acd_bank", "BANKNO", "BANKNAME", "ACTIVESTATUS=1", "BANKNO");
                            objCommon.FillDropDownList(ddlcheque, "acd_bank", "BANKNO", "BANKNAME", "ACTIVESTATUS=1", "BANKNO");
                            objCommon.FillDropDownList(ddlbanknft, "acd_bank", "BANKNO", "BANKNAME", "ACTIVESTATUS=1", "BANKNO");
                            objCommon.FillDropDownList(ddlChallanBank, "acd_bank", "BANKNO", "BANKNAME", "ACTIVESTATUS=1", "BANKNO");

                            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT A inner join ACD_SEMESTER sem ON(A.SEMESTERNO=SE.SEMESTERNO)", "A.IDNO", "(A.SEMESTERNO,0))+1 as SEMESTERNO", "IDNO>0", "IDNO");

                            int SemAdmWithPayment = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(SEM_ADM_WITH_PAYMENT,0) as SEM_ADM_WITH_PAYMENT", ""));
                            int COUNT = Convert.ToInt16(objCommon.LookUp("ACD_SEMESTER_REGISTRATION", "COUNT(IDNO)", "IDNO=" + Session["idno"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                            objCommon.FillDropDownList(ddlmode, "ACD_SEM_ADM_PAY_CONFIG", "PAYMODENO", "PAYMENTMODE", "ISNULL(ACTIVE_STATUS,0)=1", "PAYMODENO");
                            if (SemAdmWithPayment == 0)
                            {
                                btnPayment.Visible = false;
                                dvmode.Visible = false;
                                dd_details.Visible = false;
                                cheque_details.Visible = false;
                                NEFT_RTGS_details.Visible = false;
                                ChallanDetails.Visible = false;
                                divamount.Visible = false;
                                pnlfeedetails.Visible = false;
                                pnlpaymentdetails.Visible = false;
                                divnote.Visible = false;
                                pnlInstallment.Visible = false;
                                pnldiscountScholarship.Visible = false;
                                //pnlScholarship.Visible = false;
                                divInstallmentPayment.Visible = false;
                                lvStudFeeDetails.Visible = false;
                                btnSemAdmWithoutPayment.Visible = (COUNT > 0) ? false : true;
                                if (COUNT > 0)
                                    objCommon.DisplayMessage(this.Page, "Semester Admission Already Done ", this.Page);
                            }
                            else
                            {
                                btnSemAdmWithoutPayment.Visible = false;
                                //objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY A INNER JOIN ACD_SESSION_MASTER B ON (A.SESSION_NO = B.SESSIONNO)", "TOP 1 B.SESSIONNO", "B.SESSION_NAME", "ACTIVITY_NO = 31 AND CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)", "B.SESSIONNO DESC");//original
                                int Studinfomandatory = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(STUD_INFO_MANDATE,0) as STUD_INFO_MANDATE", ""));
                                int FinalSUbmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "count(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(FINAL_SUBMIT,0) = 1"));

                                if (Studinfomandatory == 0)
                                {
                                    btnPayment.Visible = false;
                                    dvmode.Visible = false;
                                    dd_details.Visible = false;
                                    cheque_details.Visible = false;
                                    NEFT_RTGS_details.Visible = false;
                                    ChallanDetails.Visible = false;
                                    //installdetails.Visible = false;
                                    BindListView();
                                    if (Convert.ToInt32(Session["Installment"]) == 0)
                                    {
                                        getScholarshipDiscountDetails(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), "TF");
                                    }
                                    else
                                    {
                                        BindInstallmentDetailslv();
                                    }
                                    BindListViewStudfeedetails();
                                    if (Session["OrgId"].ToString().Equals("8"))
                                    {
                                        //rdopayment.SelectedValue = "1";
                                        tablefeedetails.Visible = true;
                                    }
                                    else
                                    {

                                        tablefeedetails.Visible = false;
                                    }
                                }
                                else if (Studinfomandatory == 1)
                                {
                                    if (FinalSUbmit == 0)
                                    {
                                        //string ky1 = "Your Information in the Student Information Section needs to be updated before proceeding to Semester/ Trimester Admission. \\n \\n Link path --> Academic > Student Related > Student Information";
                                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Popup", "alert('" + ky1 + "')", true);
                                        objCommon.DisplayMessage("Your Information in the Student Information Section needs to be updated before proceeding to Semester/ Trimester Admission. \\n \\n Link path --> Academic > Student Related > Student Information", this.Page);
                                        pnlMain.Visible = false;
                                        return;
                                    }
                                    else if (FinalSUbmit == 1)
                                    {
                                        btnPayment.Visible = false;
                                        dvmode.Visible = false;
                                        dd_details.Visible = false;
                                        cheque_details.Visible = false;
                                        NEFT_RTGS_details.Visible = false;
                                        ChallanDetails.Visible = false;
                                        //installdetails.Visible = false;
                                        BindListView();
                                        if (Convert.ToInt32(Session["Installment"]) == 0)
                                        {
                                            getScholarshipDiscountDetails(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), "TF");
                                        }
                                        else
                                        {
                                            BindInstallmentDetailslv();
                                        }
                                        BindListViewStudfeedetails();


                                        if (Session["OrgId"].ToString().Equals("8"))
                                        {
                                            //rdopayment.SelectedValue = "1";
                                            tablefeedetails.Visible = true;
                                        }
                                    }
                                }
                            }
                        }

                        int onlinepaybtn = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(STUD_ADM_PAYMENT_BTN,0) as STUD_ADM_PAYMENT_BTN", ""));
                        rdonpayment.Visible = (onlinepaybtn == 1) ? true : false;

                        int Offlinepaybtn = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(SEM_ADM_OFF_PAY_BTN,0) as STUD_ADM_PAYMENT_BTN", ""));
                        rdoffpayment.Visible = (Offlinepaybtn == 1) ? true : false;
                    }
                    else
                    {
                        objCommon.DisplayMessage("This Page is only for students.", this.Page);
                        pnlMain.Visible = false;
                    }
                }
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool CheckActivity()
    {
        //bool ret = true;
        //string sessionno = string.Empty;
        ////sessionno = Convert.ToInt32(objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "ISNULL(SA.SESSION_NO,0)SESSION_NO", "AM.ACTIVITY_CODE = 'Sem Reg'"));
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY A INNER JOIN ACTIVITY_MASTER B ON A.ACTIVITY_NO = B.ACTIVITY_NO  INNER JOIN ACD_SESSION_MASTER C ON SESSION_NO = SESSIONNO", "SESSIONNO", "PAGE_LINK LIKE '%" + ViewState["pageno"] + "%' AND STARTED = 1 and CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)");
        ////objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY A INNER JOIN ACTIVITY_MASTER B ON A.ACTIVITY_NO = B.ACTIVITY_NO  INNER JOIN ACD_SESSION_MASTER C ON SESSION_NO = SESSIONNO", "SESSIONNO", "SESSION_NAME", "PAGE_LINK LIKE '%" + ViewState["pageno"] + "%' AND STARTED = 1 and CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)", "");


        //ActivityController objActController = new ActivityController();
        //DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        bool ret = true;
        string sessionno = string.Empty;
        string degreeno = string.Empty;
        string branchno = string.Empty;
        string semesterno = string.Empty;
        string college_id = string.Empty;

        ActivityController objActController = new ActivityController();
        DataSet ds = objActController.GetSessionNoForActivity(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
        //sessionno = Convert.ToInt32(objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "ISNULL(SA.SESSION_NO,0)SESSION_NO", "AM.ACTIVITY_CODE = 'Sem Reg'"));
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY A INNER JOIN ACTIVITY_MASTER B ON A.ACTIVITY_NO = B.ACTIVITY_NO  INNER JOIN ACD_SESSION_MASTER C ON SESSION_NO = SESSIONNO", "SESSIONNO", "PAGE_LINK LIKE '%" + ViewState["pageno"] + "%' AND STARTED = 1 and CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)");
        //objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY A INNER JOIN ACTIVITY_MASTER B ON A.ACTIVITY_NO = B.ACTIVITY_NO  INNER JOIN ACD_SESSION_MASTER C ON SESSION_NO = SESSIONNO", "SESSIONNO", "SESSION_NAME", "PAGE_LINK LIKE '%" + ViewState["pageno"] + "%' AND STARTED = 1 and CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112)", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sessionno = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            college_id = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            string groupcount = Convert.ToString(objCommon.LookUp("ACD_BRANCH_SPECIALIZATION_GROUP_LIMIT_MAPPING", "ISNULL(MAX_GROUP_LIMIT,0)", "SRNO>0 AND COLLEGE_ID =" + college_id + " AND  DEGREENO=" + degreeno + " AND BRANCHNO=" + branchno));
            Session["MAX_GROUP_LIMIT"] = groupcount;
        }

        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), degreeno, branchno, semesterno);

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                // dvMain.Visible = false;
                pnlMain.Visible = false;
                ret = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                //dvMain.Visible = false;
                pnlMain.Visible = false;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            //dvMain.Visible = false;
            pnlMain.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    protected void GetSemesterActivity()
    {
        StudentController objSC = new StudentController();
        DataSet dsActivity = null;
        string alno = ViewState["pageno"].ToString();
        int pageno = 0;
        if (alno == string.Empty)
        {

        }
        else
        {
            pageno = Convert.ToInt32(alno);
        }

        dsActivity = objSC.CheckActivityON(pageno);

        {
            if (Convert.ToInt32(dsActivity.Tables[0].Rows[0][0]) == 0)
            {
                pnlMain.Visible = false;
                // pnlsub.Visible = false;
                // lvApplypost.Visible = false;
                objCommon.DisplayMessage("Activity has been stopped for Semester Admission.", this.Page);
                return;
            }
            else
            {
                //pnlMain.Visible = true;
                // pnlsub.Visible = true;

                int COUNT = Convert.ToInt16(objCommon.LookUp("ACD_SEMESTER_REGISTRATION", "COUNT(IDNO)", "IDNO=" + Session["idno"]));
                Session["RegCount"] = COUNT;
                if (COUNT != 0)
                {
                    divstuddetails.Visible = true;
                    previousreceipt.Visible = true;
                    dd_details.Visible = false;
                    divregisterfor.Visible = false;
                    feedetails.Visible = false;
                    divregfor.Visible = false;
                    pnlpaymentdetails.Visible = false;
                    // divnote.Visible = false; //temporary visible false need to revert
                }
                else
                {
                    divnote.Visible = false;
                }
            }
        }

    }

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();

            //DataSet ds = objSC.GetFeesheadDetail(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue));
            DataSet ds = objSC.GetFeesheadDetailSemesterAdmission(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                lvfeehead.DataSource = ds;
                lvfeehead.DataBind();
                lvfeehead.Visible = true;
                Decimal TotalPrice = Convert.ToDecimal(dt.Compute("SUM(SEMESTER)", string.Empty));
                //lbltotalamount.Text = ds.Tables[0].Rows[0]["TOTAL"].ToString();
                //Session["amount"] = ds.Tables[0].Rows[0]["TOTAL"].ToString();
                lbltotalamount.Text = TotalPrice.ToString();
                Session["amount"] = TotalPrice.ToString();
            }
            else
            {
                divamount.Visible = false;

                objCommon.DisplayMessage("Standard Fees is not Defined so that you are not Eligible to do Semester Registration.", this.Page);
                pnlMain.Visible = false;
                //return;
                //pnlApplypost.Visible = false;
                //lvApplypost.DataSource = null;
                //lvApplypost.DataBind();
                //
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ApplyForPost.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }


    private void BindListViewStudfeedetails()
    {
        try
        {
            StudentController objSC = new StudentController();

            DataSet ds = objSC.GetStudFeedetails(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudFeeDetails.DataSource = ds;
                lvStudFeeDetails.DataBind();
                lvStudFeeDetails.Visible = true;
                //lbltotalamount.Text = ds.Tables[0].Rows[0]["TOTAL"].ToString();
                string ConfStatus = ds.Tables[0].Rows[0]["APPROVED STATUS"].ToString();
                if (ConfStatus == "PENDING")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#idStatus').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#idStatus').hide();$('td:nth-child(10)').hide();});", true); //For Hide Status Column for 
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#recieptno').hide();$('td:nth-child(2)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#recieptno').hide();$('td:nth-child(2)').hide();});", true);//For Hide Status Column for   
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#recieptno').hide();$('#ctl00_ContentPlaceHolder1_lvStudFeeDetails_ctrl0_tdRecipetNo').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#recieptno').hide();$('#ctl00_ContentPlaceHolder1_lvStudFeeDetails_ctrl0_tdRecipetNo').hide();});", true);//For Hide Status Column for           
                }
                if (ConfStatus == "APPROVED")
                {
                    //s divnote.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "$('#idremark').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#idremark').hide();$('td:nth-child(11)').hide();});", true); //For Hide Status Column for 
                }
            }
            else
            {
                //pnlApplypost.Visible = false;
                //lvApplypost.DataSource = null;
                //lvApplypost.DataBind();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ApplyForPost.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void ShowStudentDetails(int idno)
    {
        StudentController objSC = new StudentController();
        DataSet ds = null;

        try
        {
            int NoofInstallment = 0;
            int NoofEntriesindcr = 0;
            int Mode = 0;
            int CheckSemAfterPromotion = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(SEM_PROM_AFTER_SEM_ADM,0) as SEM_PROM_AFTER_SEM_ADM", ""));
            if (CheckSemAfterPromotion == 1)
            {
                Mode = 2;
                ddlSession.Enabled = false;
                ddlSemester.Enabled = false;
            }
            else
            {
                Mode = 1;
            }

            ds = objSC.GetStudentDetailsSemesteradmission(idno, Mode);
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["idno"] = idno;
                    lblStudentID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                    lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblDegree.Text = ds.Tables[0].Rows[0]["PROGRAMME"].ToString();
                    lblStudentCampus.Text = ds.Tables[0].Rows[0]["SCHEME"].ToString();
                    lblStudentCampus.ToolTip = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblYear.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSAStatus.Text = ds.Tables[0].Rows[0]["SASTATUS"].ToString();
                    Session["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    ddlSemester.Items.Clear();
                    ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                    ddlSemester.DataValueField = "SEMESTERNO";
                    ddlSemester.DataTextField = "SEMESTERNAME";

                    ddlSemester.DataSource = ds.Tables[1];
                    ddlSemester.DataBind();
                    Session["MOBILE"] = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    Session["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    Session["Branchname"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                    Session["YEARNO"] = ds.Tables[0].Rows[0]["YEARNO"].ToString();
                    Session["STUD_SCHEMENO"] = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    Session["demandno"] = ds.Tables[0].Rows[0]["DM_NO"].ToString();
                    int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                    string receipttype = "TF";
                    if (ds.Tables[0].Rows[0]["SASTATUS"].ToString() == "Done")
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            lvSpecializationGroup.Visible = true;
                            lvSpecializationGroup.DataSource = ds.Tables[2];
                            lvSpecializationGroup.DataBind();
                        }
                        divnewnote.Visible = true;
                    }

                    Installmentcount = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO =" + idno + " AND SEMESTERNO =" + Convert.ToInt32(Semesterno) + " AND RECIPTCODE = '" + Convert.ToString(receipttype) + "'"));
                    if (Installmentcount > 0)
                    {
                        int CheckGroupCount = Convert.ToInt32(objCommon.LookUp("ACD_VALUEADDED_COURSE", "Count(1)", " SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SCHEMENO = " + Convert.ToInt32(Session["STUD_SCHEMENO"])));
                        if (CheckGroupCount > 0)
                        {
                            ds = objFee.BindValueAddedGroups(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["STUD_SCHEMENO"]));

                            //   ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                // ddlSession.SelectedValue = "";
                                ddlgroups.DataSource = ds;
                                ddlgroups.DataValueField = ds.Tables[0].Columns[0].ToString();
                                ddlgroups.DataTextField = ds.Tables[0].Columns[1].ToString();
                                ddlgroups.DataBind();
                                // ddlSession.SelectedIndex = 0;
                            }

                            //objCommon.FillListBox(ddlgroups, "ACD_GROUP_MASTER_SPECIALIZATION A INNER JOIN ACD_VALUEADDED_COURSE B ON (A.GROUPID=B.GROUPID) ", "DISTINCT A.GROUPID", "GROUP_NAME", "ACTIVE_STATUS=1 and SEMESTERNO=" + ddlSemester.SelectedValue + "COSCHNO=" + Convert.ToInt32(Session["STUD_SCHEMENO"]), "A.GROUPID");
                            divgroups.Visible = true;
                            ViewState["GroupCount"] = 1;
                            Session["HASGROUP"] = 1;
                            int countmax = 0;
                            if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                            {
                                lblSpecializationGroup.Text = "Select Specialization Groups(Any )";
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Scs", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                                return;
                            }
                            else
                            {
                                countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                                lblSpecializationGroup.Text = "Select Specialization Groups(Any " + countmax + ")";
                            }
                        }
                        else
                        {
                            divgroups.Visible = false;
                        }
                        Session["Installment"] = Installmentcount;
                        divInstallmentPayment.Visible = true;
                        divregfor.Visible = true;
                        pnlfeedetails.Visible = true;
                        feedetails.Visible = true;

                        NoofInstallment = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));

                        NoofEntriesindcr = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CAN,0)=0 AND ISNULL(RECON,0)=1 AND RECIEPT_CODE = 'TF' AND PAY_MODE_CODE NOT IN('CO','SA') AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));



                        ds = objFee.GetStudentInstallmentDetailsSemesterreg(idno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(receipttype), Convert.ToInt32(ddlSession.SelectedValue));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            lblTotalAmountinst.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                            lblTotalInstallment.Text = ds.Tables[0].Rows[0]["TOTAL_INSTALMENT"].ToString();
                            string InstallmentAmount = ds.Tables[0].Rows[0]["TOTAL_DUE_AMOUNT"].ToString();
                            int DEMANDNO = Convert.ToInt32(ds.Tables[0].Rows[0]["DM_NO"].ToString());
                            Session["demandno"] = DEMANDNO;
                            ViewState["InstallAmount"] = InstallmentAmount;
                                     
                            lvInstallment.DataSource = ds;
                            lvInstallment.DataBind();
                            pnlinstallmentdetails.Visible = true;
                            divInstallmentPayment.Visible = true;

                            int isdemandcreated = 0;
                            isdemandcreated = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                            if (isdemandcreated > 0)
                            {

                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Demand is not Create Please Create Demand First.", this.Page);
                                return;
                            }
                        }
                    }
                    else
                    {
                        pnlinstallmentdetails.Visible = false;
                        getScholarshipDiscountDetails(idno, Semesterno, receipttype);
                    }

                    int COUNT = Convert.ToInt16(objCommon.LookUp("ACD_SEMESTER_REGISTRATION", "COUNT(IDNO)", "IDNO=" + Session["idno"] + " AND SEMESTERNO=" + Semesterno));
                    Session["RegCount"] = COUNT;

                    if (COUNT != 0 && Installmentcount == 0)
                    {
                        divstuddetails.Visible = true;
                        previousreceipt.Visible = true;
                        dd_details.Visible = false;
                        divregisterfor.Visible = false;
                        feedetails.Visible = false;
                        divpayment.Visible = false;
                        divnote.Visible = true;
                        divregfor.Visible = false;
                        pnlpaymentdetails.Visible = true;//Temporary Visible true Need not revert Visible as false.
                        pnlInstallment.Visible = false;
                        pnlpaymentdetails.Visible = false;
                        // divnote.Visible = false; //temporary make visible false need to revert
                        
                    }
                    else if (COUNT != 0 && Installmentcount != 0)
                    {
                        divstuddetails.Visible = true;
                        previousreceipt.Visible = true;
                        dd_details.Visible = false;
                        divregisterfor.Visible = false;
                        feedetails.Visible = false;
                        divpayment.Visible = false;
                        divnote.Visible = true;
                        divregfor.Visible = false;
                        pnlpaymentdetails.Visible = true;
                        if (Convert.ToInt32(Session["Installment"]) == 0)
                        {
                            getScholarshipDiscountDetails(idno, Semesterno, receipttype);
                        }
                        if (NoofInstallment == NoofEntriesindcr)
                        {
                            pnlpaymentdetails.Visible = false;
                            divnote.Visible = true;
                        }
                    }
                    else if (Installmentcount != 0 && COUNT == 0)
                    {
                        //getScholarshipDiscountDetails(idno, Semesterno, receipttype);
                        divstuddetails.Visible = true;
                        previousreceipt.Visible = true;
                        dd_details.Visible = false;
                        divregisterfor.Visible = true;
                        feedetails.Visible = false;
                        divpayment.Visible = true;
                        divnote.Visible = false;
                        divregfor.Visible = true;
                        pnlpaymentdetails.Visible = true;
                        pnlfeedetails.Visible = false;
                        int dcrdetails = 0;
                        dcrdetails = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND RECIEPT_CODE='TF' AND PAY_MODE_CODE NOT IN('CO','SA')"));
                        if (dcrdetails > 0)
                        {
                            objCommon.DisplayMessage(this.Page, "Payment has been found for semester Registration please click on submit Button for Semester Registration.", this.Page);
                            divsemregwithoutpayment.Visible = true;
                            btnsubmit.Visible = true;
                            pnlpaymentdetails.Visible = false;
                            pnlfeedetails.Visible = false;
                        }
                       
                    }
                    else
                    {
                        divnote.Visible = false;
                        pnlfeedetails.Visible = true;
                        pnlpaymentdetails.Visible = true;
                        divregfor.Visible = true;
                        pnlfeedetails.Visible = true;
                        feedetails.Visible = true;

                        //Added by nehal on 07062023
                        int dcrdetails = 0;
                        dcrdetails = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND RECIEPT_CODE='TF' AND PAY_MODE_CODE NOT IN('CO','SA')"));
                        if (dcrdetails > 0)
                        {
                            objCommon.DisplayMessage(this.Page, "Payment has been found for semester Registration please click on submit Button for Semester Registration.", this.Page);
                            divsemregwithoutpayment.Visible = true;
                            btnsubmit.Visible = true;
                            pnlpaymentdetails.Visible = false;
                            pnlfeedetails.Visible = false;
                            BindListViewStudfeedetails();
                        }

                        int CheckGroupCount = Convert.ToInt32(objCommon.LookUp("ACD_VALUEADDED_COURSE", "Count(1)", " SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SCHEMENO = " + Convert.ToInt32(Session["STUD_SCHEMENO"])));
                        if (CheckGroupCount > 0)
                        {
                            ds = objFee.BindValueAddedGroups(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["STUD_SCHEMENO"]));
                            //   ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                // ddlSession.SelectedValue = "";
                                ddlgroups.DataSource = ds;
                                ddlgroups.DataValueField = ds.Tables[0].Columns[0].ToString();
                                ddlgroups.DataTextField = ds.Tables[0].Columns[1].ToString();
                                ddlgroups.DataBind();
                                // ddlSession.SelectedIndex = 0;
                            }
                            //objCommon.FillListBox(ddlgroups, "ACD_GROUP_MASTER_SPECIALIZATION A INNER JOIN ACD_VALUEADDED_COURSE B ON (A.GROUPID=B.GROUPID) ", "DISTINCT A.GROUPID", "GROUP_NAME", "ACTIVE_STATUS=1 and SEMESTERNO=" + ddlSemester.SelectedValue + " AND COSCHNO=" + Convert.ToInt32(Session["STUD_SCHEMENO"]), "A.GROUPID");
                            divgroups.Visible = true;
                            ViewState["GroupCount"] = 1;
                            Session["HASGROUP"] = 1;
                            int countmax = 0;
                            if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                            {
                                lblSpecializationGroup.Text = "Select Specialization Groups(Any )";
                                //objCommon.DisplayMessage(this.Page, "Please Configure Branch Specialization Group Mapping", this.Page);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                                return;
                            }
                            else
                            {
                                countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                                lblSpecializationGroup.Text = "Select Specialization Groups(Any " + countmax + ")";
                            }
                        }
                        else
                        {
                            divgroups.Visible = false;
                        }

                        if (Convert.ToInt32(Session["Installment"]) == 0)
                        {
                            getScholarshipDiscountDetails(idno, Semesterno, receipttype);
                        }

                        //if (Scholarship > 0)
                        //{
                        //    Session["Scholarship_Flag"] = 1;
                        //    pnlScholarship.Visible = true;
                        //    pnlfeedetails.Visible = false;
                        //    feedetails.Visible = false;

                        //    ds = objFee.GetStudentScholarshipDetailsSemesterreg(idno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(receipttype));
                        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        //    {
                        //        Session["Scholarship_Net_Amount"] = ds.Tables[0].Rows[0]["PAYABLE_AMT"].ToString();
                        //        lvScholarship.DataSource = ds;
                        //        lvScholarship.DataBind();
                        //        lvScholarship.Visible = true;
                        //    }
                        //}
                        //else
                        //    {
                        //        scholarshipdetails.Visible = true;
                        //        pnlfeedetails.Visible = false;
                        //        feedetails.Visible = false;
                        //    }
                        //int SemAdmWithPayment = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(SEM_ADM_WITH_PAYMENT,0) as SEM_ADM_WITH_PAYMENT", ""));
                        //if (SemAdmWithPayment != 0)
                        //    BindListView();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Data Not Found.", this.Page);
                    return;
                }
                // string receipttype = "TF";

                // int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

                //count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO =" + idno + " AND SEMESTERNO =" + Convert.ToInt32(Semesterno) + " AND RECIPTCODE = '" + Convert.ToString(receipttype) + "'"));
                //if (count > 0)
                //    {
                //    Session["Installment"] = count;
                //    divInstallmentPayment.Visible = true;
                //    //  divDirectPayment.Visible = false;


                //    ds = objFee.GetStudentInstallmentDetails(idno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(receipttype));
                //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //        {
                //        // lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                //        //lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                //        //lblRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
                //        //lblCollegeName.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                //        //lblDegreeName.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                //        //lblBranchName.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                //        //lblMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                //        lblTotalAmountinst.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                //        //lblEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //        lblTotalInstallment.Text = ds.Tables[0].Rows[0]["TOTAL_INSTALMENT"].ToString();

                //        lvInstallment.DataSource = ds;
                //        lvInstallment.DataBind();
                //        }
                //    }
            }
            //else
            //    {
            //    //divnote.Visible = false;
            //    }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ApplyForPost.ShowStudentDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void getScholarshipDiscountDetails(int idno, int Semesterno, string receipttype)
    {
        DataSet ds = null;
        int DISCOUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR DCR INNER JOIN ACD_FEES_DISCOUNT D ON (D.IDNO=DCR.IDNO AND D.RECIPTCODE = DCR.RECIEPT_CODE AND DCR.SEMESTERNO=D.SEMESTERNO)", "ISNULL(count(*),0)", "DCR.IDNO =" + idno + " AND DCR.RECIEPT_CODE='TF' AND DCR.PAY_MODE_CODE = 'CO' AND ISNULL(DCR.CAN,0)=0 AND ISNULL(DCR.RECON,0)=1 AND DCR.SEMESTERNO =" + Convert.ToInt32(Semesterno)));
        int Scholarship = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP S INNER JOIN ACD_DCR DCR ON (S.IDNO = DCR.IDNO AND S.SEMESTERNO = DCR.SEMESTERNO)", "ISNULL(count(*),0)", "S.IDNO = " + idno + " AND DCR.RECIEPT_CODE='TF' AND DCR.PAY_MODE_CODE='SA' AND ISNULL(DCR.CAN,0)=0 AND ISNULL(DCR.RECON,0)=1 AND DCR.SEMESTERNO =" + Convert.ToInt32(Semesterno)));

        if (DISCOUNT > 0 || Scholarship > 0)
        {
            Session["DiscountScholarship_Flag"] = 1;
            pnldiscountScholarship.Visible = true;
            pnlfeedetails.Visible = false;
            feedetails.Visible = false;

            ds = objFee.GetStudentScholarshipDiscountDetailsSemesterReg(idno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(receipttype), Convert.ToInt32(ddlSession.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvDiscountScholarship.DataSource = ds;
                lvDiscountScholarship.DataBind();
                lvDiscountScholarship.Visible = true;
                var strExpr = "HEAD = 'Net Payable Fee'";
                ds.Tables[0].DefaultView.RowFilter = strExpr;
                DataTable dt = (ds.Tables[0].DefaultView).ToTable();
                Session["DiscountScholarship_Net_Amount"] = dt.Rows[0]["ADJUSTMENT_AMOUNT"].ToString();
            }
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        int status1 = 0;
        int Currency = 1;
        string amount = string.Empty;
        amount = Convert.ToString(hdfAmount.Value);
        try
        {

            Button btnPayNow = sender as Button;
            int installno = (btnPayNow.CommandArgument != string.Empty ? int.Parse(btnPayNow.CommandArgument) : 0);
            //double Amount = Convert.ToDouble((btnPayNow.CommandName != string.Empty ? Double.Parse(btnPayNow.CommandName) : 0));
            double Amount = Convert.ToDouble((btnPayNow.CommandName != string.Empty ? Double.Parse(btnPayNow.CommandName) : 0));
            int demandno = (btnPayNow.ToolTip != string.Empty ? int.Parse(btnPayNow.ToolTip) : 0);
            HiddenField hdfIdno = sender as HiddenField;
            int Idno = Convert.ToInt32(Session["stuinfoidno"]);

            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            // DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = Amount;
            ViewState["studAmt"] = Amount;//hdnTotalCashAmt.Value;

            int session = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "SESSION_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND INSTALL_NO=" + Convert.ToInt32(installno)));

            Session["paysession"] = session;
            //int uano = Convert.ToInt32(Session["userno"]);
            if (Session["OrgId"].ToString() == "6")
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }
            if (Session["OrgId"].ToString() == "8")
            {
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }

            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl);
                    //Response.Redirect("http://localhost:55403/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/RazorPayOnlinePaymentRequest.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        try
        {
            int online = 0;
            int ofline = 0;
            objsem.IdNo = Convert.ToInt32(Session["idno"]);
            objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);
            if (rdonpayment.Checked == true)
            {
                objsem.paymentMode = 1;
            }
            else if (rdoffpayment.Checked == true)
            {
                objsem.paymentMode = 2;
            }

            //objsem.paymentMode = Convert.ToInt32(rdopayment.SelectedValue);
            objsem.OfflineMode = Convert.ToInt32(ddlmode.SelectedValue);
            objsem.BankName = ddlbank.SelectedItem.Text;
            objsem.BranchName = txtddbranch.Text;
            //objsem = Convert.ToInt32(TextBox2.Text);
            //objsem.chequeNo = Convert.ToInt32(DropDownList4.SelectedValue);
            objsem.DDNo = Convert.ToInt32(txtddno.Text);

            // objsem.DDNumber = Convert.ToInt32(txtddno.Text);
            //objsem.Total_Amt = Convert.ToInt32(TextBox10.Text);
            //objsem.Total_Amt=Convert.ToInt32(38350000);
            objsem.Total_Amt = Convert.ToDecimal(lbltotalamount.Text);
            //Convert.ToDecimal(txttotdd.Text);
            objsem.Date_of_Issue = txtdatedd.Text.ToString();

            objsem.CREATED_BY = Convert.ToInt32(Session["usertype"].ToString());

            objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
            objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");

            //int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            int installment = 0;
            int installmentno = 0;
            string Groups = "";
            if (Convert.ToInt32(Session["Installment"]) > 0)
            {
                installment = 1;
                objsem.Total_Amt = Convert.ToDecimal(ViewState["Total_Amount"]);
                installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
            }
            else
            {
                installment = 0;
            }
            if (Convert.ToInt32(Session["DiscountScholarship_Flag"]) == 1)
            {
                objsem.Total_Amt = Convert.ToDecimal(Session["DiscountScholarship_Net_Amount"]);
            }
            else
            {
                Session["DiscountScholarship_Flag"] = 0;
            }

            //if (Convert.ToInt32(Session["Discount_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Discount_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Discount_Flag"] = 0;
            //}

            //if (Convert.ToInt32(Session["Scholarship_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Scholarship_Flag"] = 0;
            //}

            if (ViewState["GroupCount"] != null)
            {
                if (ViewState["GroupCount"].Equals(1))
                {
                    foreach (ListItem items in ddlgroups.Items)
                    {
                        if (items.Selected == true)
                        {
                            Groups += items.Value + ',';
                        }
                    }

                    if (Groups != "")
                    {
                        int countmax = 0;
                        if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                        {
                            ddlgroups.ClearSelection();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                            return;
                        }
                        else
                        {
                            countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                        }
                        //Groups = Groups;
                        Groups = Groups.Substring(0, Groups.Length - 1);
                        int Count = 0;
                        string Program = Groups;
                        string[] subs = Program.Split(',');

                        Count = subs.Count();

                        if (Count != countmax)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                            ddlgroups.ClearSelection();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Groups.", this.Page);
                        return;
                    }
                }
            }

            int UANO = Convert.ToInt32(Session["userno"]);

            CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistration(objsem, installment, installmentno, Groups, Convert.ToInt32(Session["STUD_SCHEMENO"]), UANO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
                BindListViewStudfeedetails();
                ClearControls();
                dd_details.Visible = false;
                divpayment.Visible = false;
                divnote.Visible = true;
                lvfeehead.Visible = false;
                divamount.Visible = false;
                BindInstallmentDetailslv();
                ddlgroups.Enabled = false;
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
                Button1.Visible = false;
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Semester_Registration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        try
        {
            objsem.IdNo = Convert.ToInt32(Session["idno"]);
            objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);
            if (rdonpayment.Checked == true)
            {
                objsem.paymentMode = 1;
            }
            else if (rdoffpayment.Checked == true)
            {
                objsem.paymentMode = 2;
            }
            //
            //objsem.paymentMode = Convert.ToInt32(rdopayment.SelectedValue);
            objsem.OfflineMode = Convert.ToInt32(ddlmode.SelectedValue);
            objsem.BankName = ddlcheque.SelectedItem.Text;
            objsem.BranchName = txtbranchcheque.Text;
            objsem.chequeNo = Convert.ToInt32(txtchno.Text);
            // objsem.CheckNumber = Convert.ToInt32(txtchno.Text);
            // objsem.Total_Amt = Convert.ToInt32(38350000);
            objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");//(txtdatepaynft.Text.ToString().Trim());
            objsem.Total_Amt = Convert.ToDecimal(lbltotalamount.Text);
            //Convert.ToDecimal(txttotalchq.Text);
            objsem.Date_of_Issue = txtdatechq.Text.ToString();
            objsem.CREATED_BY = 1;
            objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];

            int installment = 0;
            int installmentno = 0;
            string Groups = "";
            if (Convert.ToInt32(Session["Installment"]) > 0)
            {
                installment = 1;
                objsem.Total_Amt = Convert.ToDecimal(ViewState["Total_Amount"]);
                installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
            }
            else
            {
                installment = 0;
            }
            if (Convert.ToInt32(Session["DiscountScholarship_Flag"]) == 1)
            {
                objsem.Total_Amt = Convert.ToDecimal(Session["DiscountScholarship_Net_Amount"]);
            }
            else
            {
                Session["DiscountScholarship_Flag"] = 0;
            }
            //if (Convert.ToInt32(Session["Discount_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Discount_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Discount_Flag"] = 0;
            //}

            //if (Convert.ToInt32(Session["Scholarship_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Scholarship_Flag"] = 0;
            //}

            if (ViewState["GroupCount"] != null)
            {
                if (ViewState["GroupCount"].Equals(1))
                {

                    foreach (ListItem items in ddlgroups.Items)
                    {
                        if (items.Selected == true)
                        {

                            Groups += items.Value + ',';
                        }
                    }

                    if (Groups != "")
                    {
                        int countmax = 0;
                        if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                        {
                            ddlgroups.ClearSelection();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                            return;
                        }
                        else
                        {
                            countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                        }
                        //Groups = Groups;
                        Groups = Groups.Substring(0, Groups.Length - 1);
                        int Count = 0;
                        string Program = Groups;
                        string[] subs = Program.Split(',');


                        Count = subs.Count();

                        if (Count != countmax)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                            ddlgroups.ClearSelection();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                        return;
                    }
                }
            }
            int UANO = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistration(objsem, installment, installmentno, Groups, Convert.ToInt32(Session["STUD_SCHEMENO"]), UANO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
                ClearControlschequedetails();
                BindListViewStudfeedetails();
                lvfeehead.Visible = false;
                cheque_details.Visible = false;
                divpayment.Visible = false;
                divnote.Visible = true;
                divamount.Visible = false;
                BindInstallmentDetailslv();
                ddlgroups.Enabled = false;

                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exist", this.Page);
                Button2.Visible = false;
                return;
                //Label1.Text = "Record already exist";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryForm.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        try
        {
            objsem.IdNo = Convert.ToInt32(Session["idno"]);
            objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);
            if (rdonpayment.Checked == true)
            {
                objsem.paymentMode = 1;
            }
            else if (rdoffpayment.Checked == true)
            {
                objsem.paymentMode = 2;
            }
            //objsem.paymentMode = Convert.ToInt32(rdopayment.SelectedValue);
            objsem.OfflineMode = Convert.ToInt32(ddlmode.SelectedValue);
            objsem.BankName = ddlbanknft.SelectedItem.Text;
            objsem.BranchName = txtbranchnft.Text;
            //objsem.Total_Amt = Convert.ToInt32(38350000);  
            objsem.Total_Amt = Convert.ToDecimal(lbltotalamount.Text);
            //Convert.ToDecimal(txttotalnft.Text);
            objsem.TransactionId = txttransid.Text.ToString();
            objsem.Date_of_Issue = txtdatechq.Text.ToString();
            objsem.Date_of_Payment = (txtdatepaynft.Text.ToString().Trim());

            string IDNO = (objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0) as IDNO", "IDNO=" + Convert.ToInt32(Session["idno"])));

            string path = Server.MapPath("~/UploadSlip/");
            objsem.Filename = System.IO.Path.GetExtension(Fuslip.PostedFile.FileName);
            if (Fuslip.HasFile)
            {
                //objsem.Filename = System.IO.Path.GetExtension(Fuslip.PostedFile.FileName);
                int fileSize = Fuslip.PostedFile.ContentLength;
                //string ext = System.IO.Path.GetExtension(fuResume.FileName).ToLower();
                string extension = Path.GetExtension(Fuslip.PostedFile.FileName);
                objsem.Filename = System.IO.Path.GetExtension(Fuslip.PostedFile.FileName);

                if (extension.Contains(".pdf") || extension.Contains(".png") || extension.Contains(".jpeg"))
                {

                    if (fileSize < 200000)
                    {

                        path = Server.MapPath("~/UploadSlip/");

                        //Check whether Directory (Folder) exists.
                        if (!Directory.Exists(path))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(path);
                        }

                        string Datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //if (!(Directory.Exists(MapPath("~/PresentationLayer/UploadSliP"))))
                        //    Directory.CreateDirectory(path);         
                        string fileName = Fuslip.PostedFile.FileName.Trim();
                        fileName = fileName.Remove(fileName.LastIndexOf("."));
                        string ext = System.IO.Path.GetExtension(Fuslip.FileName);
                        fileName = IDNO + '_' + Datetime + "_Payslip" + ext;

                        //if (File.Exists((path + fileName).ToString()))
                        //    // File.Delete((path + fileName).ToString());

                        Fuslip.SaveAs(path + fileName);
                        objsem.Filename = Fuslip.PostedFile.FileName;
                        // string filePath,fileName;
                        path = Fuslip.PostedFile.FileName; // file name with path.
                        objsem.Filename = Fuslip.FileName;

                        objsem.Filename = fileName;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload File Size Upto 200KB Only.", this.Page);
                        return;
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Only pdf or jpeg or png Files are Allowed.", this.Page);
                    return;
                }
            }
            //objsem.Filename = string.Empty;
            objsem.CREATED_BY = 1;
            int installment = 0;
            int installmentno = 0;
            if (Convert.ToInt32(Session["Installment"]) > 0)
            {
                installment = 1;
                objsem.Total_Amt = Convert.ToDecimal(ViewState["Total_Amount"]);
                installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
            }
            else
            {
                installment = 0;
            }
            if (Convert.ToInt32(Session["DiscountScholarship_Flag"]) == 1)
            {
                objsem.Total_Amt = Convert.ToDecimal(Session["DiscountScholarship_Net_Amount"]);
            }
            else
            {
                Session["DiscountScholarship_Flag"] = 0;
            }

            //if (Convert.ToInt32(Session["Discount_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Discount_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Discount_Flag"] = 0;
            //}

            //if (Convert.ToInt32(Session["Scholarship_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Scholarship_Flag"] = 0;
            //}

            objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
            string Groups = "";

            if (ViewState["GroupCount"] != null)
            {
                if (ViewState["GroupCount"].Equals(1))
                {

                    foreach (ListItem items in ddlgroups.Items)
                    {
                        if (items.Selected == true)
                        {

                            Groups += items.Value + ',';
                        }
                    }

                    if (Groups != "")
                    {
                        int countmax = 0;
                        if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                        {
                            ddlgroups.ClearSelection();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                            return;
                        }
                        else
                        {
                            countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                        }
                        //Groups = Groups;
                        Groups = Groups.Substring(0, Groups.Length - 1);
                        int Count = 0;
                        string Program = Groups;
                        string[] subs = Program.Split(',');


                        Count = subs.Count();

                        if (Count != countmax)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);

                            ddlgroups.ClearSelection();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                        return;
                    }
                }
            }
            int UANO = Convert.ToInt32(Session["userno"]);

            CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistration(objsem, installment, installmentno, Groups, Convert.ToInt32(Session["STUD_SCHEMENO"]), UANO);
            //CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistrationNEFTDetails(objsem);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
                NEFT_RTGS_details.Visible = false;
                ClearControlsNEFTRTGSDetails();
                BindListViewStudfeedetails();
                lvfeehead.Visible = false;
                ddlmode.SelectedIndex = 0;
                divpayment.Visible = false;
                divnote.Visible = true;
                divamount.Visible = false;
                BindInstallmentDetailslv();
                ddlgroups.Enabled = false;
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
                Button3.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryForm.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }
    }


    private void ClearControls()
    {
        ddlmode.SelectedIndex = 0;
        ddlbank.SelectedIndex = 0;
        txtddbranch.Text = string.Empty;
        txtddno.Text = string.Empty;
        //txttotdd.Text = string.Empty;
        txtdatedd.Text = string.Empty;
        dd_details.Visible = false;
    }

    private void ClearControlschequedetails()
    {
        ddlcheque.SelectedIndex = 0;
        txtbranchcheque.Text = string.Empty;
        txtchno.Text = string.Empty;
        txtdatechq.Text = string.Empty;
        txttotalchq.Text = string.Empty;

    }
    private void ClearControlsNEFTRTGSDetails()
    {
        ddlbanknft.SelectedIndex = 0;
        txtbranchnft.Text = string.Empty;
        txttransid.Text = string.Empty;
        txtdatepaynft.Text = string.Empty;
        txttotalnft.Text = string.Empty;
    }


    private void ClearControlsChallanDetails()
    {
        ddlChallanBank.SelectedIndex = 0;
        txtChallanBranch.Text = string.Empty;
        txtChallanNo.Text = string.Empty;
        txtdatepaychallan.Text = string.Empty;
         
    }
    //protected void rdopayment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdopayment.SelectedValue == "1")
    //    {
    //        btnPayment.Visible = true;
    //        dvmode.Visible = false;
    //        dd_details.Visible = false;
    //        cheque_details.Visible = false;
    //        NEFT_RTGS_details.Visible = false;
    //    }
    //    if (rdopayment.SelectedValue == "2")
    //    {
    //        btnPayment.Visible = false;
    //        dvmode.Visible = true;
    //        dd_details.Visible = false;
    //        cheque_details.Visible = false;
    //        NEFT_RTGS_details.Visible = false;
    //    }
    //}

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "1")
        {
            dd_details.Visible = true;
            cheque_details.Visible = false;
            NEFT_RTGS_details.Visible = false;
            ChallanDetails.Visible = false;
            this.ShowDDDetails(Convert.ToInt32(ddlmode.SelectedValue));
        }
        if (ddlmode.SelectedValue == "2")
        {
            cheque_details.Visible = true;
            dd_details.Visible = false;
            NEFT_RTGS_details.Visible = false;
            ChallanDetails.Visible = false;
            this.ShowChequeDetails(Convert.ToInt32(ddlmode.SelectedValue));
        }
        if (ddlmode.SelectedValue == "3")
        {
            NEFT_RTGS_details.Visible = true;
            ChallanDetails.Visible = false;
            dd_details.Visible = false;
            cheque_details.Visible = false;
            this.ShowNEFTRTGSDetails(Convert.ToInt32(ddlmode.SelectedValue));
        }

        if (ddlmode.SelectedValue == "4")
        {
            ChallanDetails.Visible = true;
            NEFT_RTGS_details.Visible = false;           
            dd_details.Visible = false;
            cheque_details.Visible = false;
            this.ShowChallanDetails(Convert.ToInt32(ddlmode.SelectedValue));
        }

    }

    private void ShowDDDetails(int PaymodeNo)
    {

        DataSet ds = objFee.GetDetailsOfSemesterAdmissionPaymentConfiguration(PaymodeNo);

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblDDAccountName.Text = ds.Tables[0].Rows[0]["ACC_HOLDER_NAME"].ToString();
                lblBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                lblDDpayableat.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
               
            }
        }
    }
    private void ShowChequeDetails(int PaymodeNo)
    {
        DataSet ds = objFee.GetDetailsOfSemesterAdmissionPaymentConfiguration(PaymodeNo);

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblChequeInfavOf.Text = ds.Tables[0].Rows[0]["ACC_HOLDER_NAME"].ToString();
                lblChequeBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                lblChequepayableat.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();

            }
        }
    }
    private void ShowNEFTRTGSDetails(int PaymodeNo)
    {
        DataSet ds = objFee.GetDetailsOfSemesterAdmissionPaymentConfiguration(PaymodeNo);

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAccountHolderName.Text = ds.Tables[0].Rows[0]["ACC_HOLDER_NAME"].ToString();

                lblBankNameRTGS.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();

                lblAccountNo.Text = ds.Tables[0].Rows[0]["ACCOUNT_NO"].ToString();

                lblIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC_CODE"].ToString();

                lblBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();

            }
        }
    }

    private void ShowChallanDetails(int PaymodeNo)
    {
        DataSet ds = objFee.GetDetailsOfSemesterAdmissionPaymentConfiguration(PaymodeNo);

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblChallanAccName.Text = ds.Tables[0].Rows[0]["ACC_HOLDER_NAME"].ToString();

                lblChallanBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();

                lblChallanAccNo.Text = ds.Tables[0].Rows[0]["ACCOUNT_NO"].ToString();

                lblChallanIfsc.Text = ds.Tables[0].Rows[0]["IFSC_CODE"].ToString();

                lblChallanBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();

                lblChallanFileName.Text = ds.Tables[0].Rows[0]["CHALLAN_FILE_NAME"].ToString();
                lblChallanFileName.Visible = false;
            }
        }
    }
    protected void txtdatedd_TextChanged(object sender, EventArgs e)
    {
        if (txtdatedd.Text != "" && Convert.ToDateTime(txtdatedd.Text) > DateTime.Today)
        {
            objCommon.DisplayMessage(this.Page, "Future Date Is Not Acceptable.", this.Page);
            txtdatedd.Text = string.Empty;
        }
    }
    protected void txtdatechq_TextChanged(object sender, EventArgs e)
    {
        if (txtdatechq.Text != "" && Convert.ToDateTime(txtdatechq.Text) > DateTime.Today)
        {
            objCommon.DisplayMessage(this.Page, "Future Date Is Not Acceptable.", this.Page);
            txtdatechq.Text = string.Empty;
        }
    }

    protected void txtdatepaynft_TextChanged(object sender, EventArgs e)
    {
        if (txtdatepaynft.Text != "" && Convert.ToDateTime(txtdatepaynft.Text) > DateTime.Today)
        {
            objCommon.DisplayMessage(this.Page, "Future Date Is Not Acceptable.", this.Page);
            txtdatepaynft.Text = string.Empty;
        }

    }
    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        int StudId = Convert.ToInt32(Session["idno"]);
        ImageButton btnPrint = sender as ImageButton;
        Session["DCRNO"] = int.Parse(btnPrint.CommandArgument);
        int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        if (Session["OrgId"].ToString() == "8")
        {
            ShowReport("Semester Registration", "FeeCollectionReceiptForCash_MIT.rpt");

        }
        else if (Session["OrgId"].ToString() == "9")
        {
            ShowReport_ATLAS("Semester Registration", "FeeCollectionReceiptForCash_ATLAS.rpt");
        }
        else if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4"))
        {
            this.ShowReport_ForCash("FeeCollectionReceiptForCash_cpukota.rpt", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CAN_REC"]));
        }
        else
        {
            this.ShowReport_ForCash("FeeCollectionReceiptForCash.rpt", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CAN_REC"]));
        }

        //FeeCollectionReceiptForCash_ATLAS.rpt

        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "$('#idremark').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#idremark').hide();$('td:nth-child(11)').hide();});", true);
        //this.ShowReport("SemesterRegistrationMIT.rpt", Convert.ToInt32(StudId), Convert.ToInt32(SemesterNo),(DcrNO.ToString()));
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"] + ",@P_DCRNO=" + Session["DCRNO"];
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //private void ShowReport_ForCash(string rptName, string UA_FULLNAME)
    //    {
    //    try
    //        {
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=Fee_Collection_Receipt";
    //        url += "&path=~,Reports,Academic," + rptName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"] + ",@P_DCRNO=" + Session["DCRNO"] + "," + "@P_UA_NAME=" + Session["username"].ToString();
    //        divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
    //        divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
    //        }
    //    catch (Exception ex)
    //        {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //        }
    //    }ss
    private void ShowReport_ATLAS(string reportTitle, string rptFileName)
    {
        try
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"] + ",@P_DCRNO=" + Session["DCRNO"];
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReport_ForCash(string rptFileName, string UA_FULLNAME, int Cancel)
    {
        try
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + rptFileName;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"] + ",@P_DCRNO=" + Session["DCRNO"] + "," + "@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        try
        {
            string Groups = "";
            if (ViewState["GroupCount"] != null)
            {
                if (ViewState["GroupCount"].Equals(1))
                {
                    foreach (ListItem items in ddlgroups.Items)
                    {
                        if (items.Selected == true)
                        {
                            Groups += items.Value + ',';
                        }
                    }
                    if (Groups != "")
                    {
                        int countmax = 0;
                        if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                        {
                            ddlgroups.ClearSelection();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                            return;
                        }
                        else
                        {
                            countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                        }
                        //Groups = Groups;
                        Groups = Groups.Substring(0, Groups.Length - 1);
                        Session["Groupids"] = Groups;
                        int Count = 0;
                        string Program = Groups;
                        string[] subs = Program.Split(',');
                        Count = subs.Count();

                        if (Count != countmax)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                            ddlgroups.ClearSelection();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                        return;
                    }
                }
            }
            ViewState["btnflag"] = "sendpay";
            Panel1_ModalPopupExtender.Show();
            //string Groups = "";
            //if (ViewState["GroupCount"] != null)
            //{

            //    if (ViewState["GroupCount"].Equals(1))
            //    {
            //        foreach (ListItem items in ddlgroups.Items)
            //        {
            //            if (items.Selected == true)
            //            {

            //                Groups += items.Value + ',';


            //            }

            //        }
            //        if (Groups != "")
            //        {
            //            //Groups = Groups;
            //            Groups = Groups.Substring(0, Groups.Length - 1);
            //            Session["Groupids"] = Groups;
            //            int Count = 0;
            //            string Program = Groups;
            //            string[] subs = Program.Split(',');
            //            Count = subs.Count();

            //            if (Count != 2)
            //            {
            //                objCommon.DisplayMessage(this.Page, "Please Select Two Groups Only.", this.Page);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
            //            return;
            //        }
            //    }
            //}
            //// }

            //int semesterreg = 1;
            //Session["SemRegflag"] = semesterreg;
            //int status1 = 0;
            //int Currency = 1;
            //int installmentno = 0;
            //int installment = 0;
            ////  = "1";
            //string activityname = "Semester Admission Fee";

            //Session["payactivityno"] = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME='" + activityname + "'"));

            //Session["paymode"] = "1";
            //string amount = string.Empty;
            ////amount = Convert.ToString(lbltotalamount.Text);
            //amount = Convert.ToString(Session["amount"].ToString());
            //Button btnPayNow = sender as Button;

            //double Amount = Convert.ToDouble((amount != string.Empty ? Double.Parse(amount) : 0));
            //HiddenField hdfIdno = sender as HiddenField;
            //int Idno = Convert.ToInt32(Session["idno"]);
            //Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            //int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            //DailyCollectionRegister dcr = new DailyCollectionRegister();
            //string PaymentMode = "Admission Fees";
            //Session["PaymentMode"] = PaymentMode;

            //objsem.paymentMode = 1;
            //string orderid = "0";

            //int InstallmentCount = 0;
            //InstallmentCount = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "Count(ISNULL (IDNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"])));

            //if (InstallmentCount > 0)
            //{
            //    Session["studAmt"] = ViewState["InstallAmount"];
            //    ViewState["studAmt"] = ViewState["InstallAmount"];
            //    installment = 1;
            //    installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
            //    Session["Installmentno"] = installment;
            //}
            //else
            //{

            //    Session["studAmt"] = Amount;
            //    ViewState["studAmt"] = Amount;
            //}

            //if (Convert.ToInt32(Session["Discount_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Discount_Net_Amount"]);
            //    Session["studAmt"] = Convert.ToDecimal(Session["Discount_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Discount_Flag"] = 0;
            //}


            //if (Convert.ToInt32(Session["Scholarship_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
            //    Session["studAmt"] = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Scholarship_Flag"] = 0;
            //}

            //dcr.TotalAmount = Convert.ToDouble(Amount);
            //Session["studName"] = lblStudName.Text;
            //Session["studPhone"] = Session["MOBILE"];
            //Session["studEmail"] = Session["EMAILID"];

            //Session["ReceiptType"] = "TF";
            //Session["idno"] = Idno;

            //Session["paysemester"] = ddlSemester.SelectedValue;
            //Session["homelink"] = "OnlinePayment.aspx";
            //Session["regno"] = lblStudentID.Text;
            //Session["payStudName"] = lblStudName.Text;
            //Session["SESSIONNO"] = ddlSession.SelectedValue;
            //Session["SemadmSessionno"] = ddlSession.SelectedValue;

            //if (rdonpayment.Checked == true)
            //{
            //    Session["paymode"] = 1;
            //}
            //else if (rdoffpayment.Checked == true)
            //{
            //    Session["paymode"] = 2;
            //}
            //// Session["paymode"] = rdopayment.SelectedValue;

            //int session = Convert.ToInt32(ddlSession.SelectedValue);

            //Session["paysession"] = session;

            //FeeCollectionController objFee = new FeeCollectionController();

            //if (Session["OrgId"].ToString() == "6")
            //{
            //    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            //}
            //if (Session["OrgId"].ToString() == "8")
            //{
            //    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            //}
            ////**********************************End by Nikhil L.********************************************//

            //DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);

            //if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            //{
            //    if (ds1.Tables[0].Rows.Count > 1)
            //    {

            //    }
            //    else
            //    {
            //        Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
            //        string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            //        Response.Redirect(RequestUrl);
            //    }
            //}

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rdonpayment_CheckedChanged(object sender, EventArgs e)
    {
        if (Session["amount"].ToString() == "0.00")
        {
            objCommon.DisplayMessage(this.Page, "Please Define Standard Fees.", this.Page);
            return;
        }
        else
        {
            btnPayment.Visible = true;
            dvmode.Visible = false;
            dd_details.Visible = false;
            cheque_details.Visible = false;
            NEFT_RTGS_details.Visible = false;
            ChallanDetails.Visible = false;
            rdoffpayment.Checked = false;
        }
    }
    protected void rdoffpayment_CheckedChanged(object sender, EventArgs e)
    {
        if (Session["amount"].ToString() == "0.00")
        {
            objCommon.DisplayMessage(this.Page, "Please Define Standard Fees.", this.Page);
            return;
        }
        else
        {
            btnPayment.Visible = false;
            dvmode.Visible = true;
            dd_details.Visible = false;
            cheque_details.Visible = false;
            NEFT_RTGS_details.Visible = false;
            ChallanDetails.Visible = false;
            rdonpayment.Checked = false;

            if (Convert.ToInt32(Session["Installment"]) > 0)
            {
                objCommon.FillDropDownList(ddlinstallment, "ACD_FEES_INSTALLMENT", "top 1 INSTALMENT_NO", "CONCAT(INSTALMENT_NO,' - ' ,INSTALL_AMOUNT, ' - ' ,DUE_DATE) as Installment", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "INSTALL_NO");
                objCommon.FillDropDownList(ddlchequeinstallment, "ACD_FEES_INSTALLMENT", "top 1 INSTALMENT_NO", "CONCAT(INSTALMENT_NO,' - ' ,INSTALL_AMOUNT, ' - ' ,DUE_DATE) as Installment", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "INSTALL_NO");
                objCommon.FillDropDownList(ddlneftinstallment, "ACD_FEES_INSTALLMENT", "top 1 INSTALMENT_NO", "CONCAT(INSTALMENT_NO,' - ' ,INSTALL_AMOUNT, ' - ' ,DUE_DATE) as Installment", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "INSTALL_NO");
                ddinstallment.Visible = true;
                chequeinstallment.Visible = true;
                neftinstallment.Visible = true;
            }
        }
    }

    //private DailyCollectionRegister Bind_FeeCollectionData()
    //    {
    //    /// Bind transaction related data from various controls.
    //    DailyCollectionRegister dcr = new DailyCollectionRegister();
    //    try
    //        {

    //        //dcr.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
    //        dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
    //        int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
    //        ////if (examType == 1)
    //        ////{
    //        dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
    //        ////}
    //        ////else
    //        ////{
    //        ////    dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString()) + 1;
    //        ////}

    //        dcr.FeeHeadAmounts = this.GetFeeItems();




    //        DemandDrafts[] dds = null;



    //        // dcr.ReceiptNo = txtReceiptNo.Text.Trim();

    //        //int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(REC_NO)", "REC_NO=" + txtReceiptNo.Text));
    //        //if (count == 1)
    //        //{
    //        //    objCommon.DisplayMessage("Receipt No Already Exists", this.Page); 
    //        //}
    //        //else
    //        //{
    //        //    dcr.ReceiptNo = txtReceiptNo.Text.Trim();
    //        //}


    //        dcr.ChallanDate = DateTime.Today;

    //        /// This status is used to mark/flag unpaid/not received bank chalans.
    //        /// Default is false. if unpaid then will be marked as true.
    //        dcr.IsDeleted = false;
    //        dcr.CompanyCode = string.Empty;
    //        dcr.RpEntry = string.Empty;
    //        dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
    //        dcr.PrintDate = DateTime.Today;
    //        dcr.CollegeCode = Session["colcode"].ToString();            //this is add to excess amount maintain. date: 10/04/2012
    //        // check the status of configuration page

    //        //string chkConfig = objCommon.LookUp("ACD_CONFIG", "STATUS", "CONFIGNO=1");
    //        //if(chkConfig == "Y")
    //        //{
    //        //dcr.ExcessAmount = Convert.ToDouble(txtTotalAmount.Text) - Convert.ToDouble(txtTotalFeeAmount.Text);


    //        //*****************
    //        foreach (ListViewDataItem item in lvFeeItems.Items)
    //            {
    //            string fee_head = string.Empty;//***************
    //            fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************
    //            string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();

    //            if (fee_head == "LATE FEE")
    //                {
    //                if (feeAmt != null && feeAmt != string.Empty)
    //                    {
    //                    dcr.Late_fee = Convert.ToDouble(feeAmt);

    //                    }
    //                }
    //            }
    //        //*****************

    //        //}
    //        //else
    //        //{
    //        //    dcr.ExcessAmount = 0.00;
    //        //    objCommon.DisplayMessage("Excess amount cannot maintain. Beacause not maintain the Uaims Configuration status", this.Page);
    //        //}
    //        }
    //    catch (Exception ex)
    //        {
    //        throw;
    //        }
    //    return dcr;
    //    }
    //private FeeHeadAmounts GetFeeItems()
    //    {
    //    FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
    //    try
    //        {
    //        foreach (ListViewDataItem item in lvFeeItems.Items)
    //            {
    //            int feeHeadNo = 0;
    //            double feeAmount = 0.00;

    //            string fee_head = string.Empty;//***************
    //            fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************
    //            CheckBox chkAmount = item.FindControl("chkFee") as CheckBox;

    //            if (chkAmount.Checked)//*****************
    //                {
    //                string feeHeadSrNo = chkAmount.ToolTip;
    //                if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
    //                    feeHeadNo = Convert.ToInt32(feeHeadSrNo);
    //                string feeAmt = ((HiddenField)item.FindControl("hidFeeItemAmount")).Value;
    //                // string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
    //                if (feeAmt != null && feeAmt != string.Empty)
    //                    feeAmount = Convert.ToDouble(feeAmt);

    //                feeHeadAmts[feeHeadNo - 1] = feeAmount;
    //                }
    //            }


    //        ////foreach (ListViewDataItem item in lvFeeItems.Items)
    //        ////{
    //        ////    int feeHeadNo = 0;
    //        ////    double feeAmount = 0.00;

    //        ////    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
    //        ////    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
    //        ////        feeHeadNo = Convert.ToInt32(feeHeadSrNo);

    //        ////    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
    //        ////    if (feeAmt != null && feeAmt != string.Empty)
    //        ////        feeAmount = Convert.ToDouble(feeAmt);

    //        ////    feeHeadAmts[feeHeadNo - 1] = feeAmount;
    //        ////}

    //        //foreach (ListViewDataItem item in lvFeeItems.Items)
    //        //{
    //        //    int feeHeadNo = 0;
    //        //    double feeAmount = 0.00;
    //        //    string fee_head = string.Empty;//***************
    //        //    fee_head = ((Label)item.FindControl("FEE_LONGNAME")).Text;//*****************

    //        //    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
    //        //    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
    //        //    if(fee_head != "LATE FEE")//*****************
    //        //    {
    //        //        ////string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
    //        //        if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
    //        //            feeHeadNo = Convert.ToInt32(feeHeadSrNo);

    //        //        ////string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
    //        //        if (feeAmt != null && feeAmt != string.Empty)
    //        //            feeAmount = Convert.ToDouble(feeAmt);

    //        //        feeHeadAmts[feeHeadNo - 1] = feeAmount;
    //        //    }

    //        //    //*****************
    //        //    if(fee_head == "LATE FEE")
    //        //    {
    //        //        if (feeAmt != null && feeAmt != string.Empty)
    //        //        {
    //        //            feeAmount = Convert.ToDouble(feeAmt);
    //        //            late_fee = feeAmount;
    //        //        }
    //        //    }
    //        //    //*****************
    //        //}
    //        }
    //    catch (Exception ex)
    //        {
    //        throw;
    //        }
    //    return feeHeadAmts;
    //    }

    protected void btnPrintReceipt_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (btnPrint.CommandArgument != string.Empty)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_crescent.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_MIT.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
                else if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4"))
                {
                    this.ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_cpukota.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
                else
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void ShowReportPrevious(string reportTitle, string rptFileName, int dcrNo, int studentNo, string Username)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() +
              "," + this.GetReportParameters(dcrNo, studentNo, "2");

            //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt";
        return param;
    }
    private string GetInstallmnentReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + ",@P_IDNO=" + studentNo.ToString();
        return param;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlinstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlinstallment.SelectedIndex > 0)
        {

            string total_amount = (ddlinstallment.SelectedItem.Text).Split('-')[1];
            int installmentno = Convert.ToInt32(ddlinstallment.SelectedValue);
            ViewState["Total_Amount"] = total_amount;
            ViewState["INSTALLMENT_NO"] = installmentno;
        }
    }
    protected void ddlchequeinstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlchequeinstallment.SelectedIndex > 0)
        {
            string total_amount = (ddlchequeinstallment.SelectedItem.Text).Split('-')[1];
            int installmentno = Convert.ToInt32(ddlchequeinstallment.SelectedValue);
            ViewState["Total_Amount"] = total_amount;
            ViewState["INSTALLMENT_NO"] = installmentno;
        }
    }
    protected void ddlneftinstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlneftinstallment.SelectedIndex > 0)
        {
            string total_amount = (ddlneftinstallment.SelectedItem.Text).Split('-')[1];
            int installmentno = Convert.ToInt32(ddlneftinstallment.SelectedValue);
            ViewState["Total_Amount"] = total_amount;
            ViewState["INSTALLMENT_NO"] = installmentno;
        }
    }
    public void BindInstallmentDetailslv()
    {
        StudentController objSC = new StudentController();
        DataSet ds = null;
        string receipttype = "TF";
        ds = objFee.GetStudentInstallmentDetailsSemesterreg(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(receipttype), Convert.ToInt32(ddlSession.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Session["Installemt_Flag"] = 1;
            lblTotalAmountinst.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
            lblTotalInstallment.Text = ds.Tables[0].Rows[0]["TOTAL_INSTALMENT"].ToString();
            string InstallmentAmount = ds.Tables[0].Rows[0]["TOTAL_DUE_AMOUNT"].ToString();
            ViewState["InstallAmount"] = InstallmentAmount;
            lvInstallment.DataSource = ds;
            lvInstallment.DataBind();
            pnlinstallmentdetails.Visible = true;
            divInstallmentPayment.Visible = true;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string Groups = "";
        if (ViewState["GroupCount"] != null)
        {
            if (ViewState["GroupCount"].Equals(1))
            {
                foreach (ListItem items in ddlgroups.Items)
                {
                    if (items.Selected == true)
                    {
                        Groups += items.Value + ',';
                    }
                }
                if (Groups != "")
                {
                    int countmax = 0;
                    if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                    {
                        ddlgroups.ClearSelection();
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                        return;
                    }
                    else
                    {
                        countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                    }

                    //Groups = Groups;
                    Groups = Groups.Substring(0, Groups.Length - 1);
                    Session["Groupids"] = Groups;
                    int Count = 0;
                    string Program = Groups;
                    string[] subs = Program.Split(',');
                    Count = subs.Count();

                    if (Count != countmax)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                        ddlgroups.ClearSelection();
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                    return;
                }
            }
        }
        ViewState["btnflag"] = "sendwopaysem";
        Panel1_ModalPopupExtender.Show();

        //int online = 0;
        //int ofline = 0;
        //objsem.IdNo = Convert.ToInt32(Session["idno"]);
        //objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        //objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);

        //objsem.paymentMode = 1;

        ////objsem.paymentMode = Convert.ToInt32(rdopayment.SelectedValue);
        //objsem.OfflineMode = Convert.ToInt32(0);
        //objsem.BankName = ddlbank.SelectedItem.Text;
        //objsem.BranchName = txtddbranch.Text;
        ////objsem = Convert.ToInt32(TextBox2.Text);
        ////objsem.chequeNo = Convert.ToInt32(DropDownList4.SelectedValue);
        //// objsem.DDNo = Convert.ToInt32(txtddno.Text);
        ////
        //// objsem.DDNumber = Convert.ToInt32(txtddno.Text);
        ////objsem.Total_Amt = Convert.ToInt32(TextBox10.Text);
        ////objsem.Total_Amt=Convert.ToInt32(38350000);
        //objsem.Total_Amt = Convert.ToDecimal(lbltotalamount.Text);
        ////Convert.ToDecimal(txttotdd.Text);
        //objsem.Date_of_Issue = txtdatedd.Text.ToString();

        //objsem.CREATED_BY = Convert.ToInt32(Session["usertype"].ToString());

        //objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
        //objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");

        ////int OrganizationId = Convert.ToInt32(Session["OrgId"]);
        //int installment = 0;
        //int installmentno = 0;
        //if (Convert.ToInt32(Session["Installment"]) > 0)
        //{
        //    installment = 1;
        //    objsem.Total_Amt = Convert.ToDecimal(ViewState["Total_Amount"]);
        //    installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
        //}
        //else
        //{
        //    installment = 0;
        //}
        //string orderid = "0";
        //string Groups = "";

        //if (ViewState["GroupCount"] != null)
        //{
        //    if (ViewState["GroupCount"] == "1")
        //    {
        //        foreach (ListItem items in ddlgroups.Items)
        //        {
        //            if (items.Selected == true)
        //            {

        //                Groups += items.Value + ',';
        //            }
        //        }

        //        if (Groups != "")
        //        {
        //            //Groups = Groups;
        //            Groups = Groups.Substring(0, Groups.Length - 1);
        //            Session["Groupids"] = Groups;
        //            int Count = 0;
        //            string Program = Groups;
        //            string[] subs = Program.Split(',');
        //            Count = subs.Count();


        //            if (Count != 2)
        //            {
        //                objCommon.DisplayMessage(this.Page, "You can Select Only Two Group.", this.Page);
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
        //            return;
        //        }
        //    }
        //}

        //StudentController objSC = new StudentController();
        //CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistrationOnline(objsem, orderid, installment, installmentno, ViewState["GroupCount"].ToString());
        //if (cs.Equals(CustomStatus.RecordSaved))
        //{
        //    objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
        //    ShowStudentDetails(objsem.IdNo);
        //    BindListViewStudfeedetails();
        //    ClearControls();
        //    dd_details.Visible = false;
        //    divpayment.Visible = false;
        //    divnote.Visible = true;
        //    lvfeehead.Visible = false;
        //    divamount.Visible = false;
        //    divsemregwithoutpayment.Visible = false;
        //    pnlpaymentdetails.Visible = false;
        //    pnlfeedetails.Visible = false;
        //    previousreceipt.Visible = true;
        //    BindInstallmentDetailslv();
        //    return;
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
        //    Button1.Visible = false;
        //    return;
        //}
    }
    protected void ddlgroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Groups = "";

        foreach (ListItem items in ddlgroups.Items)
        {
            if (items.Selected == true)
            {
                //strSplitAry = ddlSchedule.SelectedItem.Text.Trim() .Split(separator, StringSplitOptions.RemoveEmptyEntries);
                Groups += items.Value + ',';
            }
        }

        if (Groups != "")
        {
            // divSpeccourse.Visible = true;
            //  pnlSpecGroup.Visible = true;
        }
        else
        {

            // divSpeccourse.Visible = false;
            //  pnlSpecGroup.Visible = false;
        }
    }

    protected void SemAdmWithoutPayment_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["btnflag"] = "sendwopay";
            Panel1_ModalPopupExtender.Show();

            //StudentController objSC = new StudentController();
            //objsem.IdNo = Convert.ToInt32(Session["idno"]);
            //objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            //objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);

            //objsem.paymentMode = 0; // Semester Admission wothout payemnt 
            //objsem.OfflineMode = 0;
            //objsem.BankName = string.Empty;
            //objsem.BranchName = string.Empty;
            //objsem.DDNo = 0;
            //objsem.Total_Amt = 0;
            //objsem.Date_of_Issue = string.Empty;
            //objsem.CREATED_BY = Convert.ToInt32(Session["usertype"].ToString());

            //objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
            //objsem.Date_of_Payment = string.Empty;
            //int installment = 0;
            //int installmentno = 0;
            //string Groups = "";
            //if (ViewState["GroupCount"] != null)
            //{
            //    if (ViewState["GroupCount"].Equals(1))
            //    {
            //        foreach (ListItem items in ddlgroups.Items)
            //        {
            //            if (items.Selected == true)
            //                Groups += items.Value + ',';
            //        }

            //        if (Groups != "")
            //        {
            //            Groups = Groups.TrimEnd(',');
            //            int Count = 0;
            //            string Program = Groups;
            //            string[] subs = Program.Split(',');
            //            Count = subs.Count();
            //            if (Count != 2)
            //            {
            //                objCommon.DisplayMessage(this.Page, "Please Select Only two Groups.", this.Page);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            objCommon.DisplayMessage(this.Page, "Please Select Groups.", this.Page);
            //            return;
            //        }
            //    }
            //}

            //int UANO = Convert.ToInt32(Session["userno"]);
            //CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistration(objsem, installment, installmentno, Groups, Convert.ToInt32(Session["STUD_SCHEMENO"]), UANO);
            //if (cs.Equals(CustomStatus.RecordSaved))
            //{
            //    objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
            //    ShowStudentDetails(objsem.IdNo);
            //    BindListViewStudfeedetails();
            //    ClearControls();
            //    dd_details.Visible = false;
            //    divpayment.Visible = false;
            //    divnote.Visible = true;
            //    lvfeehead.Visible = false;
            //    divamount.Visible = false;
            //    BindInstallmentDetailslv();
            //    ddlgroups.Enabled = false;
            //    divnote.Visible = false;
            //    lvStudFeeDetails.Visible = false;
            //    btnSemAdmWithoutPayment.Visible = false;
            //    return;
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
            //    Button1.Visible = false;
            //    return;
            //}

        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Semester_Registration.SemAdmWithoutPayment_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
            ////this.ClearControl();
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkAgree.Checked == false)
            {
                Showmessage("Please check terms and conditions");
                Panel1_ModalPopupExtender.Show();

                return;
            }
            else
            {
                if (ViewState["btnflag"].ToString() == "sendpay")
                {
                    string Groups = "";
                    if (ViewState["GroupCount"] != null)
                    {
                        if (ViewState["GroupCount"].Equals(1))
                        {
                            foreach (ListItem items in ddlgroups.Items)
                            {
                                if (items.Selected == true)
                                {
                                    Groups += items.Value + ',';
                                }
                            }
                            if (Groups != "")
                            {
                                int countmax = 0;
                                if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                                {
                                    ddlgroups.ClearSelection();
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                                    return;
                                }
                                else
                                {
                                    countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                                }
                                //Groups = Groups;
                                Groups = Groups.Substring(0, Groups.Length - 1);
                                Session["Groupids"] = Groups;
                                int Count = 0;
                                string Program = Groups;
                                string[] subs = Program.Split(',');
                                Count = subs.Count();

                                if (Count != countmax)
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                                    ddlgroups.ClearSelection();
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                                return;
                            }
                        }
                    }
                    // }

                    int semesterreg = 1;
                    Session["SemRegflag"] = semesterreg;
                    int status1 = 0;
                    int Currency = 1;
                    int installmentno = 0;
                    int installment = 0;
                    //  = "1";
                    string activityname = "Semester Admission Fee";

                    Session["payactivityno"] = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME='" + activityname + "'"));

                    Session["paymode"] = "1";
                    string amount = string.Empty;
                    //amount = Convert.ToString(lbltotalamount.Text);
                    amount = Convert.ToString(Session["amount"].ToString());
                    Button btnPayNow = sender as Button;

                    double Amount = Convert.ToDouble((amount != string.Empty ? Double.Parse(amount) : 0));
                    HiddenField hdfIdno = sender as HiddenField;
                    int Idno = Convert.ToInt32(Session["idno"]);
                    Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                    int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                    DailyCollectionRegister dcr = new DailyCollectionRegister();
                    string PaymentMode = "Admission Fees";
                    Session["PaymentMode"] = PaymentMode;

                    objsem.paymentMode = 1;
                    string orderid = "0";

                    int InstallmentCount = 0;
                    //InstallmentCount = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "Count(ISNULL (IDNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    InstallmentCount = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO =" + Idno + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = 'TF'"));

                    if (InstallmentCount > 0)
                    {
                        Session["studAmt"] = ViewState["InstallAmount"];
                        ViewState["studAmt"] = ViewState["InstallAmount"];
                        installment = 1;
                        installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
                        Session["Installmentno"] = installment;
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["DiscountScholarship_Flag"]) == 1)
                        {
                            objsem.Total_Amt = Convert.ToDecimal(Session["DiscountScholarship_Net_Amount"]);
                            Session["studAmt"] = Convert.ToDecimal(Session["DiscountScholarship_Net_Amount"]);
                        }
                        else
                        {
                            Session["DiscountScholarship_Flag"] = 0;
                            Session["studAmt"] = Amount;
                            ViewState["studAmt"] = Amount;
                        }
                    }

                    

                    //if (Convert.ToInt32(Session["Discount_Flag"]) == 1)
                    //{
                    //    objsem.Total_Amt = Convert.ToDecimal(Session["Discount_Net_Amount"]);
                    //    Session["studAmt"] = Convert.ToDecimal(Session["Discount_Net_Amount"]);
                    //}
                    //else
                    //{
                    //    Session["Discount_Flag"] = 0;
                    //}

                    //if (Convert.ToInt32(Session["Scholarship_Flag"]) == 1)
                    //{
                    //    objsem.Total_Amt = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
                    //    Session["studAmt"] = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
                    //}
                    //else
                    //{
                    //    Session["Scholarship_Flag"] = 0;
                    //}

                    dcr.TotalAmount = Convert.ToDouble(Amount);
                    Session["studName"] = lblStudName.Text;
                    Session["studPhone"] = Session["MOBILE"];
                    Session["studEmail"] = Session["EMAILID"];

                    Session["ReceiptType"] = "TF";
                    Session["idno"] = Idno;

                    Session["paysemester"] = ddlSemester.SelectedValue;
                    Session["homelink"] = "OnlinePayment.aspx";
                    Session["regno"] = lblStudentID.Text;
                    Session["payStudName"] = lblStudName.Text;
                    Session["SESSIONNO"] = ddlSession.SelectedValue;
                    Session["SemadmSessionno"] = ddlSession.SelectedValue;

                    if (rdonpayment.Checked == true)
                    {
                        Session["paymode"] = 1;
                    }
                    else if (rdoffpayment.Checked == true)
                    {
                        Session["paymode"] = 2;
                    }
                    // Session["paymode"] = rdopayment.SelectedValue;

                    int session = Convert.ToInt32(ddlSession.SelectedValue);

                    Session["paysession"] = session;

                    FeeCollectionController objFee = new FeeCollectionController();

                    if (Session["OrgId"].ToString() == "6")
                    {
                        degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    }
                    if (Session["OrgId"].ToString() == "8")
                    {
                        college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    }
                    //**********************************End by Nikhil L.********************************************//

                    DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);

                    if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 1)
                        {

                        }
                        else
                        {
                            Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                            string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                            Response.Redirect(RequestUrl);
                        }
                    }
                }
                //added by nehal on 07062023
                else if (ViewState["btnflag"].ToString() == "sendwopaysem")
                {
                    int online = 0;
                    int ofline = 0;
                    objsem.IdNo = Convert.ToInt32(Session["idno"]);
                    objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);

                    //added by nehal on 07062023
                    objsem.paymentMode = 0;
                    //end

                    //objsem.paymentMode = Convert.ToInt32(rdopayment.SelectedValue);
                    objsem.OfflineMode = Convert.ToInt32(0);
                    objsem.BankName = ddlbank.SelectedItem.Text;
                    objsem.BranchName = txtddbranch.Text;
                    //objsem = Convert.ToInt32(TextBox2.Text);
                    //objsem.chequeNo = Convert.ToInt32(DropDownList4.SelectedValue);
                    // objsem.DDNo = Convert.ToInt32(txtddno.Text);
                    //
                    // objsem.DDNumber = Convert.ToInt32(txtddno.Text);
                    //objsem.Total_Amt = Convert.ToInt32(TextBox10.Text);
                    //objsem.Total_Amt=Convert.ToInt32(38350000);
                    objsem.Total_Amt = Convert.ToDecimal(lbltotalamount.Text);
                    //Convert.ToDecimal(txttotdd.Text);
                    objsem.Date_of_Issue = txtdatedd.Text.ToString();

                    objsem.CREATED_BY = Convert.ToInt32(Session["usertype"].ToString());

                    objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                    objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");

                    //int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                    int installment = 0;
                    int installmentno = 0;
                    if (Convert.ToInt32(Session["Installment"]) > 0)
                    {
                        installment = 1;
                        objsem.Total_Amt = Convert.ToDecimal(ViewState["Total_Amount"]);
                        installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
                    }
                    else
                    {
                        installment = 0;
                    }
                    string orderid = "0";
                    string Groups = "";

                    if (ViewState["GroupCount"] != null)
                    {
                        if (ViewState["GroupCount"] == "1")
                        {
                            foreach (ListItem items in ddlgroups.Items)
                            {
                                if (items.Selected == true)
                                {

                                    Groups += items.Value + ',';
                                }
                            }

                            if (Groups != "")
                            {
                                int countmax = 0;
                                if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                                {
                                    ddlgroups.ClearSelection();
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                                    return;
                                }
                                else
                                {
                                    countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                                }
                                //Groups = Groups;
                                Groups = Groups.Substring(0, Groups.Length - 1);
                                Session["Groupids"] = Groups;
                                int Count = 0;
                                string Program = Groups;
                                string[] subs = Program.Split(',');
                                Count = subs.Count();


                                if (Count != countmax)
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                                    ddlgroups.ClearSelection();
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                                return;
                            }
                        }
                    }
                    else
                    {
                        ViewState["GroupCount"] = "0";
                    
                    }

                    StudentController objSC = new StudentController();
                    CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistrationOnline(objsem, orderid, installment, installmentno, ViewState["GroupCount"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
                        ShowStudentDetails(objsem.IdNo);
                        BindListViewStudfeedetails();
                        ClearControls();
                        dd_details.Visible = false;
                        divpayment.Visible = false;
                        divnote.Visible = true;
                        lvfeehead.Visible = false;
                        divamount.Visible = false;
                        divsemregwithoutpayment.Visible = false;
                        pnlpaymentdetails.Visible = false;
                        pnlfeedetails.Visible = false;
                        previousreceipt.Visible = true;
                        BindInstallmentDetailslv();
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
                        Button1.Visible = false;
                        return;
                    }
                }
                //end
                else
                {
                    StudentController objSC = new StudentController();
                    objsem.IdNo = Convert.ToInt32(Session["idno"]);
                    objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);

                    objsem.paymentMode = 0; // Semester Admission wothout payemnt 
                    objsem.OfflineMode = 0;
                    objsem.BankName = string.Empty;
                    objsem.BranchName = string.Empty;
                    objsem.DDNo = 0;
                    objsem.Total_Amt = 0;
                    objsem.Date_of_Issue = string.Empty;
                    objsem.CREATED_BY = Convert.ToInt32(Session["usertype"].ToString());

                    objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                    objsem.Date_of_Payment = string.Empty;
                    int installment = 0;
                    int installmentno = 0;
                    string Groups = "";
                    if (ViewState["GroupCount"] != null)
                    {
                        if (ViewState["GroupCount"].Equals(1))
                        {
                            foreach (ListItem items in ddlgroups.Items)
                            {
                                if (items.Selected == true)
                                    Groups += items.Value + ',';
                            }

                            if (Groups != "")
                            {
                                int countmax = 0;
                                if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                                {
                                    ddlgroups.ClearSelection();
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                                    return;
                                }
                                else
                                {
                                    countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                                }
                                Groups = Groups.TrimEnd(',');
                                int Count = 0;
                                string Program = Groups;
                                string[] subs = Program.Split(',');
                                Count = subs.Count();
                                if (Count != countmax)
                                {
                                    objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);
                                    ddlgroups.ClearSelection();
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Please Select Groups.", this.Page);
                                return;
                            }
                        }
                    }

                    int UANO = Convert.ToInt32(Session["userno"]);
                    CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistration(objsem, installment, installmentno, Groups, Convert.ToInt32(Session["STUD_SCHEMENO"]), UANO);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
                        ShowStudentDetails(objsem.IdNo);
                        BindListViewStudfeedetails();
                        ClearControls();
                        dd_details.Visible = false;
                        divpayment.Visible = false;
                        divnote.Visible = true;
                        lvfeehead.Visible = false;
                        divamount.Visible = false;
                        BindInstallmentDetailslv();
                        ddlgroups.Enabled = false;
                        divnote.Visible = false;
                        lvStudFeeDetails.Visible = false;
                        btnSemAdmWithoutPayment.Visible = false;
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
                        Button1.Visible = false;
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Hide();
    }
    protected void txtdatepaychallan_TextChanged(object sender, EventArgs e)
    {
        if (txtdatepaychallan.Text != "" && Convert.ToDateTime(txtdatepaychallan.Text) > DateTime.Today)
        {
            objCommon.DisplayMessage(this.Page, "Future Date Is Not Acceptable.", this.Page);
            txtdatepaynft.Text = string.Empty;
        }
    }
    protected void btnSubmitChallan_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        try
        {
            objsem.IdNo = Convert.ToInt32(Session["idno"]);
            objsem.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objsem.SemesterNO = Convert.ToInt32(ddlSemester.SelectedValue);
            if (rdonpayment.Checked == true)
            {
                objsem.paymentMode = 1;
            }
            else if (rdoffpayment.Checked == true)
            {
                objsem.paymentMode = 2;
            }
            //objsem.paymentMode = Convert.ToInt32(rdopayment.SelectedValue);
            objsem.OfflineMode = Convert.ToInt32(ddlmode.SelectedValue);
            objsem.BankName = ddlChallanBank.SelectedItem.Text;
            objsem.BranchName = txtChallanBranch.Text;
            //objsem.Total_Amt = Convert.ToInt32(38350000);  
            objsem.Total_Amt = Convert.ToDecimal(lbltotalamount.Text);
            //Convert.ToDecimal(txttotalnft.Text);
            objsem.TransactionId = txtChallanNo.Text.ToString();
            objsem.Date_of_Issue = txtdatepaychallan.Text.ToString();
            objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy"); //(txtdatepaychallan.Text.ToString().Trim());

            string IDNO = (objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0) as IDNO", "IDNO=" + Convert.ToInt32(Session["idno"])));

            string path = Server.MapPath("~/UploadChallan/");
            objsem.Filename = System.IO.Path.GetExtension(FuChallan.PostedFile.FileName);
            if (FuChallan.HasFile)
            {
                //objsem.Filename = System.IO.Path.GetExtension(Fuslip.PostedFile.FileName);
                int fileSize = FuChallan.PostedFile.ContentLength;
                //string ext = System.IO.Path.GetExtension(fuResume.FileName).ToLower();
                string extension = Path.GetExtension(FuChallan.PostedFile.FileName);
                objsem.Filename = System.IO.Path.GetExtension(FuChallan.PostedFile.FileName);

                if (extension.Contains(".pdf") || extension.Contains(".png") || extension.Contains(".jpeg"))
                {

                    if (fileSize < 200000)
                    {

                        path = Server.MapPath("~/UploadChallan/");

                        //Check whether Directory (Folder) exists.
                        if (!Directory.Exists(path))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(path);
                        }

                        string Datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //if (!(Directory.Exists(MapPath("~/PresentationLayer/UploadSliP"))))
                        //    Directory.CreateDirectory(path);         
                        string fileName = FuChallan.PostedFile.FileName.Trim();
                        fileName = fileName.Remove(fileName.LastIndexOf("."));
                        string ext = System.IO.Path.GetExtension(FuChallan.FileName);
                        fileName = IDNO + '_' + Datetime + "_Challan" + ext;

                        //if (File.Exists((path + fileName).ToString()))
                        //    // File.Delete((path + fileName).ToString());

                        Fuslip.SaveAs(path + fileName);
                        objsem.Filename = FuChallan.PostedFile.FileName;
                        // string filePath,fileName;
                        path = FuChallan.PostedFile.FileName; // file name with path.
                        objsem.Filename = FuChallan.FileName;

                        objsem.Filename = fileName;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload File Size Upto 200KB Only.", this.Page);
                        return;
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Only pdf or jpeg or png Files are Allowed.", this.Page);
                    return;
                }
            }
            //objsem.Filename = string.Empty;
            objsem.CREATED_BY = 1;
            int installment = 0;
            int installmentno = 0;
            if (Convert.ToInt32(Session["Installment"]) > 0)
            {
                installment = 1;
                objsem.Total_Amt = Convert.ToDecimal(ViewState["Total_Amount"]);
                installmentno = Convert.ToInt32(ViewState["INSTALLMENT_NO"]);
            }
            else
            {
                installment = 0;
            }
            if (Convert.ToInt32(Session["DiscountScholarship_Flag"]) == 1)
            {
                objsem.Total_Amt = Convert.ToDecimal(Session["DiscountScholarship_Net_Amount"]);
            }
            else
            {
                Session["DiscountScholarship_Flag"] = 0;
            }

            //if (Convert.ToInt32(Session["Discount_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Discount_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Discount_Flag"] = 0;
            //}

            //if (Convert.ToInt32(Session["Scholarship_Flag"]) == 1)
            //{
            //    objsem.Total_Amt = Convert.ToDecimal(Session["Scholarship_Net_Amount"]);
            //}
            //else
            //{
            //    Session["Scholarship_Flag"] = 0;
            //}

            objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
            string Groups = "";

            if (ViewState["GroupCount"] != null)
            {
                if (ViewState["GroupCount"].Equals(1))
                {

                    foreach (ListItem items in ddlgroups.Items)
                    {
                        if (items.Selected == true)
                        {

                            Groups += items.Value + ',';
                        }
                    }

                    if (Groups != "")
                    {
                        int countmax = 0;
                        if (Session["MAX_GROUP_LIMIT"].ToString() == null || Session["MAX_GROUP_LIMIT"].ToString() == "")
                        {
                            ddlgroups.ClearSelection();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Specialization configuration not done. Please contact coordinator.');", true);
                            return;
                        }
                        else
                        {
                            countmax = Convert.ToInt32(Session["MAX_GROUP_LIMIT"].ToString());
                        }
                        //Groups = Groups;
                        Groups = Groups.Substring(0, Groups.Length - 1);
                        int Count = 0;
                        string Program = Groups;
                        string[] subs = Program.Split(',');


                        Count = subs.Count();

                        if (Count != countmax)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select " + countmax + " Groups Only.", this.Page);

                            ddlgroups.ClearSelection();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                        return;
                    }
                }
            }
            int UANO = Convert.ToInt32(Session["userno"]);

            CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistration(objsem, installment, installmentno, Groups, Convert.ToInt32(Session["STUD_SCHEMENO"]), UANO);
            //CustomStatus cs = (CustomStatus)objSC.AddSemesterRegistrationNEFTDetails(objsem);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Semester Admission Done Successfully.", this.Page);
                NEFT_RTGS_details.Visible = false;
                ClearControlsChallanDetails();
                BindListViewStudfeedetails();
                lvfeehead.Visible = false;
                ddlmode.SelectedIndex = 0;
                divpayment.Visible = false;
                divnote.Visible = true;
                divamount.Visible = false;
                BindInstallmentDetailslv();
                ddlgroups.Enabled = false;
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
                btnSubmitChallan.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryForm.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }
    }
    protected void btnDownloadChallan_Click(object sender, EventArgs e)
    {
        try
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;

            string blob_ContainerName = "";
            string blob_ConStr = "";
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"] != null)
                {
                    blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
                    blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            //string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            string FileName = lblChallanFileName.Text;
            string directoryName = "~/CHALLANDOCUMENT" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string doc = FileName;
            var Document = doc;
            string extension = Path.GetExtension(doc.ToString());
            if (doc == null || doc == "")
            {
                objCommon.DisplayMessage(this.Page, "Challan Not Found !", this.Page);
                return;
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
        catch (Exception ex)
        {
        }
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
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

}

