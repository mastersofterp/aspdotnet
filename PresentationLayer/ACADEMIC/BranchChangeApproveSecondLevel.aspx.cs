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
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using SendGrid;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_BranchChangeApproveSecondLevel : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    FeeDemand demandCriteria = new FeeDemand();
    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    //lblSession.Text = Session["sessionname"].ToString();
                    PopulateDropDown();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                // btnPrint.Enabled = false;
                btnSubmit.Enabled = false;
            }

            ddlBranch.Attributes.Add("onChange", "return ShowConfirm(this);");
        }

        divMsg.InnerHtml = string.Empty;

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        divMsg.InnerHtml = string.Empty;
        rdWithFee.Checked = false;
        rdWithoutFee.Checked = false;
        rdWithFee.Enabled = true;
        rdWithoutFee.Enabled = true;
        lblNote.Visible = false;
        //ddlBranch.Enabled = true;  //COmmeted on 2020 Nov 24
        lblCurrentfeess.Text = "0";
        lblPaidFees.Text = "0";
        lblNewBranchFee.Text = "0";
        lblExcessAmt.Text = "0";
        ViewState["IS_DEMAND_CREATED"] = null;
        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please Enter Proper Reg. No.";
            txtStudent.Focus();
            return;
        }
        lblMsg.Text = string.Empty;

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";
            string category = "";
            string admcan = "0";
            string FirstLevelApprove = "0";
            decimal paidfees = 0;
            idno = objCommon.LookUp("acd_student", "idno", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");

            if (idno == string.Empty)
            {
                lblName.Text = string.Empty;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Student Not Found!!";
                this.ClearControls();
                btnSubmit.Enabled = false;
                return;
            }
            if (txtStudent.Text.Trim() != string.Empty)
            {
                admcan = objCommon.LookUp("acd_student", "isnull(admcan,0)", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
                if (admcan == "True")
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Entered student admission has been cancelled!", this.Page);
                    txtStudent.Text = "";
                    txtStudent.Focus();
                    return;
                }

                FirstLevelApprove = objCommon.LookUp("ACD_BRCHANGE", "IS_FIRST_LEVEL_APPROVE", "IDNO=" + idno);

                if (String.IsNullOrEmpty(FirstLevelApprove))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);
                    return; 
                }

                if (!String.IsNullOrEmpty(FirstLevelApprove))
                {
                    if (FirstLevelApprove.ToLower() == "false")
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);
                        return; 
                    }
                }

                //if (admcan == "True")
                //{
                //    objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);                    
                //    return;
                //}

                if (txtStudent.Text.Contains("("))
                {
                    if (txtStudent.Text.Contains("["))
                    {
                        char[] ct = { '(' };
                        string[] cat = txtStudent.Text.Trim().Split(ct);
                        //idno value
                        category = cat[1].Replace(")", "");
                        cat = category.Split('[');
                        category = cat[0].Replace("]", "");
                        char[] sp = { '[' };
                        string[] data = txtStudent.Text.Trim().Split(sp);
                        //idno value
                        idno = data[1].Replace("]", "");
                        ViewState["idno"] = Convert.ToInt32(idno);
                    }

                }
            }
            string degreeNo = objCommon.LookUp("acd_student", "degreeno", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
            ViewState["idno"] = idno.ToString();


            imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            DataTableReader dtr = objSRegist.GetStudentDetailsNew(Convert.ToInt32(idno));

            if (dtr.Read())
            {
                Session["idno"] = dtr["idno"].ToString();
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString(); ;
                lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                ViewState["OldRollNo"] = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                lblRollNo.Text = ViewState["OldRollNo"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                lblDegree.Text = dtr["DEGREENAME"].ToString();
                lblDegree.ToolTip = dtr["DEGREENO"].ToString();

                lblColg.Text = dtr["COLLEGE_NAME"].ToString();
                lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();
                
                ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
                ViewState["degreeNo"] = dtr["DEGREENO"].ToString();
                 
                ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
                ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                ViewState["BranchNoOld"] = dtr["BRANCHNO"].ToString();

                ViewState["NEWDEGREENO"] = dtr["NEW_DEGREENO"].ToString(); 
                ViewState["NEWBRANCHNO"] = dtr["NEWBRANCHNO"].ToString();
                ViewState["BRCH_NO"] = dtr["BRCHNO"].ToString();
              


                ViewState["PTYPENO"] = dtr["PTYPE"].ToString();
                lblRequestRemark.Text = dtr["REQUEST_REMARK"].ToString();
                lblAcademicRemark.Text = dtr["ACADEMIC_REMARK"].ToString();
                lblApprovedRemark.Text = dtr["APPROVE_REMARK"].ToString();
                ViewState["FileName"] = dtr["FILE_NAME"].ToString();
                ViewState["PREVIEW"] = dtr["PREVIEW"].ToString();
                string filename=dtr["FILE_NAME"].ToString();
                if (!String.IsNullOrEmpty(filename))
                {
                  //  btnView.Visible = true;
                }
                else
                {
                  //  btnView.Visible = false;
                }
                btnSubmit.Enabled = true;
                divdata.Visible = true;
                //}

                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["idno"].ToString() + "&type=student";
                // Commented by Rita M........
                //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", "B.BRANCHNO", "B.LONGNAME", "DB.COLLEGE_ID=" + Convert.ToInt32(dtr["COLLEGE_ID"]) + " AND DB.DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"]) + " AND DB.BRANCHNO<>" + Convert.ToInt32(dtr["BRANCHNO"]), "LONGNAME");

                //Added by Rita M...............
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]), "A.DEGREENO ASC");


                lblCurrentfeess.Text = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

                lblPaidFees.Text = objCommon.LookUp("ACD_DCR", "SUM(ISNULL(TOTAL_AMT,0))", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

                if (lblPaidFees.Text.Equals(string.Empty))
                {
                    lblPaidFees.Text = Convert.ToString(paidfees);
                }


                int isBranchChanged = Convert.ToInt32(objCommon.LookUp("ACD_BRCHANGE", "ISNULL(COUNT(IDNO),0)COUNT", "REGNO='" + txtStudent.Text.Trim() + "'"));

                if (isBranchChanged >= 2)
                {
                    pnlBranchChange.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    rdWithFee.Enabled = false;
                    rdWithoutFee.Enabled = false;
                    // ShowMessage("Branch change already done for filtered student.");
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "ALERT: Programme/Branch change already done for filtered student.You Cannot Change Branch More than two times.";
                    return;
                }
                else if (isBranchChanged >= 1)
                {
                    pnlBranchChange.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    rdWithFee.Enabled = true;
                    rdWithoutFee.Enabled = true;
                    // ShowMessage("Branch change already done for filtered student.");
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "ALERT: Programme/Branch change already done for filtered student.";
                    return;
                }

            }
            else
            {
                //lblRegNo.Text = string.Empty;
                btnSubmit.Enabled = false;
            }
            dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {

           
            objCommon.FillDropDownList(ddlStudentList, "ACD_STUDENT S INNER JOIN ACD_BRCHANGE BR ON S.IDNO = BR.IDNO", "DISTINCT S.IDNO", "(CASE WHEN S.REGNO IS NOT NULL THEN S.STUDNAME+'('+ISNULL(S.REGNO,'')+')' ELSE S.STUDNAME END)STUDENT_NAME", "ISNULL(IS_SECOND_LEVEL_APPROVE,0) != 1 AND ISNULL(IS_FIRST_LEVEL_APPROVE,0) = 1  ", "IDNO");
            objCommon.FillDropDownList(ddlNewCollege,"ACD_COLLEGE_MASTER","COLLEGE_ID","COLLEGE_NAME","COLLEGE_ID>0","");
            //session
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND BRANCHNO<>99", "LONGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStudentDetails(int idno)
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataTableReader dtr = objSRegist.GetStudentDetails(idno);

            if (dtr.Read())
            {
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString();
                lblRollNo.Text = dtr["ROLLNO"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            }
            dtr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReport("ACADEMIC_BranchChange", "AdmissionSlip.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_IDNO=" + Session["idno"] + ",@P_ADMBATCH=" + ViewState["lblAdmbatch"].ToString() + ",@Year=" + ViewState["lblYear"].ToString() + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1"));
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //**********LOGIC ADDED BY: M. REHBAR SHEIKH ON 11-07-2019 | SAVE FOR NEW BRANCH***********
        if (!rdWithoutFee.Checked && !rdWithFee.Checked)
        {
            ShowMessage("Programme/Branch change with fees collection Or without fees collection? Please specify with radio button selection.");
            //ddlBranch.SelectedIndex = 0;
            return;
        }

        if (ddlBranch.SelectedIndex == 0)
        {
            ShowMessage("Please select new Programme/Branch selection.");
            ddlBranch.Focus();
            return;
        }

        if (txtRemark.Text == string.Empty)
        {
            ShowMessage("Please enter some remarks.");
            txtRemark.Focus();
            return;
        }
        string NewRollNo = "", OldRollNo = "";
        decimal paidfees = 0, excessamts = 0;
        StudentController objSController = new StudentController();
        BranchController objBranch = new BranchController();
        Student objStudent = new Student();
        Common objcommon = new Common();
        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO=" + Session["idno"].ToString()));                // + txtStudent.Text.Trim()));

        if (Convert.ToString(ViewState["New_RollNo"]).Equals(string.Empty))
        {
            NewRollNo = "";
        }
        else
        {
            NewRollNo = ViewState["New_RollNo"].ToString();
        }


        if (Convert.ToString(ViewState["OldRollNo"]).Equals(string.Empty))
        {
            OldRollNo = "";
        }
        else
        {
            OldRollNo = ViewState["OldRollNo"].ToString();
        }

        StudentRegistration objSRegist = new StudentRegistration();
        DataTableReader dtr = objSRegist.GetStudentDetails(IDNO);

        if (dtr.Read())
        {
            lblName.Text = dtr["STUDNAME"].ToString();
            lblRegNo.Text = dtr["REGNO"].ToString();
            lblBranch.Text = dtr["LONGNAME"].ToString();
            lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
            lblDegree.Text = dtr["DEGREENAME"].ToString();
            lblDegree.ToolTip = dtr["DEGREENO"].ToString();
            lblColg.Text = dtr["COLLEGE_NAME"].ToString();
            lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();
        }
        dtr.Close();

        objStudent.IdNo = Convert.ToInt32(IDNO);
        objStudent.BranchNo = Convert.ToInt32(lblBranch.ToolTip); // old branch no
        objStudent.StudName = lblName.Text;
        objStudent.NewBranchNo = Convert.ToInt32(ddlBranch.SelectedValue); // new branch no
        //Added by Rita M.....
        objStudent.DegreeNo = Convert.ToInt32(lblDegree.ToolTip);// old degree
        objStudent.NewDegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);//new degree....
        objStudent.College_ID = Convert.ToInt32(lblColg.ToolTip); //Deepali

        objStudent.Remark = txtRemark.Text.Trim();
        objStudent.CollegeCode = Session["colcode"].ToString();
        objStudent.RegNo = txtStudent.Text.Trim();
        int userno = Convert.ToInt32(Session["userno"]);

        if (lblPaidFees.Text.Equals(string.Empty))
        {
            paidfees = 0;
        }
        else
        {
            paidfees = Convert.ToDecimal(lblPaidFees.Text);
        }

        if (lblExcessAmt.Text.Equals(string.Empty))
        {
            excessamts = 0;
        }
        else
        {
            excessamts = Convert.ToDecimal(lblExcessAmt.Text);
        }

        //if (!rdWithoutFee.Checked && !rdWithFee.Checked)

        int IsWithFeePaid = 0;
        if (rdWithoutFee.Checked)
        {
            IsWithFeePaid = 0;
        }
        else if (rdWithFee.Checked)
        {
            IsWithFeePaid = 1;
        }

        //Demand Update

        FeeDemand feeDemand = new FeeDemand();

        feeDemand.StudentId = Convert.ToInt32(IDNO);
        feeDemand.StudentName = lblName.Text;
        feeDemand.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue); // new branch no
        feeDemand.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);//new degree....
        feeDemand.EnrollmentNo = txtStudent.Text.Trim();
        feeDemand.PaymentTypeNo = Convert.ToInt32(ViewState["PTYPENO"]);

        //feeDemand.SessionNo = Convert.ToInt32(ViewState["SessionNo_RollB"]);
        feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]);

        feeDemand.ReceiptTypeCode = "TF";
        feeDemand.UserNo = Convert.ToInt32(Session["userno"]);

        feeDemand.AdmBatchNo = Convert.ToInt32(ViewState["batchNo"]);
        feeDemand.SemesterNo = Convert.ToInt32(ViewState["semesterNo"]);
        feeDemand.CollegeCode = Session["colcode"].ToString();
        feeDemand.College_ID = Convert.ToInt32(lblColg.ToolTip);


        if (lvFeeItems.Items.Count > 0)
        {
            SubmitDemand(feeDemand, Convert.ToInt32(ViewState["PTYPENO"]));
        }
        //

        //CustomStatus cs = (CustomStatus)objBranch.ChangeBranch_NewStudent(objStudent, NewRollNo, OldRollNo, userno, paidfees, excessamts);
        CustomStatus cs = (CustomStatus)objBranch.ChangeBranch_FinalApprove(objStudent, NewRollNo, OldRollNo, userno, paidfees, excessamts, Convert.ToInt32(ViewState["BRCH_NO"]), IsWithFeePaid, Convert.ToInt32(ViewState["semesterNo"]));
        


        DemandModificationController dmController = new DemandModificationController();
        FeeDemand demandCriteria = new FeeDemand();

        DataSet newFees = objCommon.FillDropDown("ACD_DEMAND", "*", string.Empty, "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(lblBranch.ToolTip) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "", string.Empty);
        if (newFees != null && newFees.Tables[0].Rows.Count > 0)
        {
            demandCriteria.SessionNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
            demandCriteria.ReceiptTypeCode = newFees.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            demandCriteria.BranchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["BRANCHNO"]);
            demandCriteria.SemesterNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
            demandCriteria.PaymentTypeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["PAYTYPENO"]);
            demandCriteria.UserNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["UA_NO"]);
            demandCriteria.CollegeCode = newFees.Tables[0].Rows[0]["COLLEGE_CODE"].ToString();
            //Commented by Rita M.....
            demandCriteria.DegreeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
            //demandCriteria.NewDegreeNo = newdegreeno;
            demandCriteria.AdmBatchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCHNO"]);

        }

        string college = "";
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            if (rdWithoutFee.Checked)
            {
                string response2 = dmController.CancelDemandForSelectedStudent_ByBranchChange_WithoutFees(Convert.ToInt32(Session["idno"]), demandCriteria);
            }
            // btnPrint.Enabled = true;
            string regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
             college=objcommon.LookUp("ACD_COLLEGE_MASTER CM INNER JOIN ACD_STUDENT S ON CM.COLLEEGE_ID=S.COLLEGE_ID","COLLEGE_NAME","IDNO="+Convert.ToInt32(Session["idno"]));
             objCommon.DisplayMessage(UpdatePanel1, "Programme/Branch changed successfully.", this.Page);//New Registration Number is: " + regno
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Programme/Branch Change Process Completed Successfully ";// +regno;
          //  TransferToEmail(lblName.Text, regno, ddlDegree.SelectedItem.Text, lblBranch.Text, ddlBranch.SelectedItem.Text, txtRemark.Text, college);
            divdata.Visible = false;
            divFeeItems.Visible = false;
            divDemandButton.Visible = false; //new added on 2020 Nov 24
            ClearControls();
            btnShow.Enabled = true;

        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            if (rdWithoutFee.Checked)
            {
                string response2 = dmController.CancelDemandForSelectedStudent_ByBranchChange_WithoutFees(Convert.ToInt32(Session["idno"]), demandCriteria);
            }
            // btnPrint.Enabled = true;
            //objCommon.DisplayMessage(UpdatePanel1, "Branch changed successfully. And Student with new generated Roll No. already exist.", this.Page);
            string regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));

            objCommon.DisplayMessage(UpdatePanel1, "Programme/Branch changed successfully.", this.Page);
            //New Registration Number is: " + regno
            lblMsg.ForeColor = System.Drawing.Color.Green;
            //lblMsg.Text = "Branch changed successfully. And Student with new generated Roll No. already exist.";
            lblMsg.Text = "Programme/Branch Change Process Completed Successfully. ";//New Registration Number is: + regno;
            //TransferToEmail(lblName.Text, regno, ddlDegree.SelectedItem.Text, lblBranch.Text, ddlBranch.SelectedItem.Text, txtRemark.Text, college);
            divdata.Visible = false;
            divFeeItems.Visible = false;
            divDemandButton.Visible = false; //new added on 2020 Nov 24
            btnShow.Enabled = true;
            ClearControls();
            txtStudent.Focus();
        }
        else
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Error...";
            divdata.Visible = false;
            // btnPrint.Enabled = false;
        }
    }

    private void ClearControls()
    {
        lblName.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblRollNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        txtStudent.Text = string.Empty;
        txtNewRegNo.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        lblNewBranchFee.Text = "0";
        lblPaidFees.Text = "0";
        lblExcessAmt.Text = "0";
        lblCurrentfeess.Text = "0";
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        lblNote.Visible = false;
        //btnPrint.Enabled = false;
        //lblMsg.Text = string.Empty;
        //imgEmpPhoto.Dispose();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //if (ViewState["IS_DEMAND_CREATED"] != null)
        //{
        //    if (rdWithFee.Checked)
        //    {
        //        if (ViewState["IS_DEMAND_CREATED"].ToString() == "YES")
        //        {
        //            DemandModificationController dmController = new DemandModificationController();
        //            FeeDemand demandCriteria2 = new FeeDemand();

        //            demandCriteria2.SessionNo = Convert.ToInt32(ViewState["SessionNo_RollB"]);
        //            demandCriteria2.ReceiptTypeCode = ViewState["ReceiptTypeCode_RollB"].ToString();
        //            demandCriteria2.BranchNo = Convert.ToInt32(ViewState["BranchNo_RollB"]);
        //            demandCriteria2.SemesterNo = Convert.ToInt32(ViewState["SemesterNo_RollB"]);
        //            demandCriteria2.PaymentTypeNo = Convert.ToInt32(ViewState["PaymentTypeNo_RollB"]);
        //            demandCriteria2.UserNo = Convert.ToInt32(ViewState["UserNo_RollB"]);
        //            demandCriteria2.CollegeCode = ViewState["CollegeCode_RollB"].ToString();
        //            demandCriteria2.DegreeNo = Convert.ToInt32(ViewState["DegreeNo_RollB"]);
        //            //  demandCriteria2.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        //            demandCriteria2.AdmBatchNo = Convert.ToInt32(ViewState["AdmBatchNo_RollB"]);

        //            string total = "0";
        //            total = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BranchNo_RollB"]) + "AND DEGREENO=" + Convert.ToInt32(ViewState["DegreeNo_RollB"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["SemesterNo_RollB"]) + "");

        //            decimal paidFee = 0;
        //            decimal olddemandAmt = 0;
        //            decimal excessAmt = 0;

        //            paidFee = Convert.ToDecimal(lblPaidFees.Text);
        //            olddemandAmt = Convert.ToDecimal(total);

        //            if (paidFee > olddemandAmt)
        //            {
        //                excessAmt = paidFee - olddemandAmt;
        //                lblExcessAmt.Text = excessAmt.ToString();
        //            }

        //            string response2 = dmController.RollBackDemandForSelectedStudent_ByBranchChange(Convert.ToInt32(Session["idno"]), demandCriteria2, Convert.ToInt32(ViewState["semesterNo"]), false, Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(lblBranch.ToolTip), Convert.ToInt32(ViewState["NewBranchNo_RollB"]), excessAmt, Convert.ToInt32(ddlDegree.SelectedValue));
        //            if (response2 == "2")
        //            {
        //                objCommon.DisplayMessage(this.UpdatePanel1, "Demand Sucessfully RollBack.", this.Page);
        //                lblMsg.ForeColor = System.Drawing.Color.Green;
        //                lblMsg.Text = "Demand Sucessfully RollBack.";
        //                ViewState["IS_DEMAND_CREATED"] = null;

        //                pnlBranchChange.Visible = false;
        //                btnSubmit.Visible = false;
        //                btnCancel.Visible = false;
        //                rdWithFee.Checked = false;
        //                rdWithoutFee.Checked = false;
        //                rdWithFee.Enabled = true;
        //                rdWithoutFee.Enabled = true;
        //                lblNote.Visible = false;
        //                btnShow.Enabled = true;
        //                ddlBranch.Enabled = true;
        //                lblNewBranchFee.Text = "0";
        //                lblExcessAmt.Text = "0";
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    Response.Redirect(Request.Url.ToString());
        //}
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //**********LOGIC ADDED BY: M. REHBAR SHEIKH ON 11-07-2019 | CREATE NEW DEMAND FOR NEW BRANCH***********
        int colgId = 0;
        int Demand_Count = 0;
        bool Demand_Overwrite = false;
        if (!rdWithoutFee.Checked && !rdWithFee.Checked)
        {
            ShowMessage("Programme/Branch change with fees collection Or without fees collection? Please specify with radio button selection.");
            ddlBranch.SelectedIndex = 0;
            return;
        }
        ViewState["IS_DEMAND_CREATED"] = null;
        ViewState["New_RollNo"] = null;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        int branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        int newdegreeno = Convert.ToInt32(ddlDegree.SelectedValue);

        StudentController objsc = new StudentController();

        if (lblRollNo.Text.Equals(string.Empty))
        {
            ViewState["New_RollNo"] = null;
        }
        else
        {
            //Commented by Deepali on 15/09/2020
            //txtNewRegNo.Text = objsc.GetNewRollNoForBranchChange(Convert.ToInt32(ViewState["batchNo"]), Convert.ToInt32(ViewState["degreeNo"]), Convert.ToInt32(lblBranch.ToolTip), Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(branchNo), 1, Convert.ToInt32(Session["idno"]));
            //ViewState["New_RollNo"] = txtNewRegNo.Text;
        }


        int isDemand = 0;
        // isDemand = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(ISNULL(IDNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + ""));
        isDemand = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(ISNULL(IDNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + ""));

        if (ddlBranch.SelectedValue == "0")
        {
            txtNewRegNo.Text = string.Empty;
        }

      
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = new FeeDemand();

            //DataSet newFees = objCommon.FillDropDown("ACD_DEMAND", "*", string.Empty, "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(lblBranch.ToolTip) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "", string.Empty);
            DataSet newFees = objCommon.FillDropDown("ACD_STUDENT", "STUDNAME,PTYPE,SEMESTERNO,ADMBATCH", "", "IDNO = " + Session["idno"].ToString(), "");
            if (newFees != null && newFees.Tables[0].Rows.Count > 0)
            {
                //demandCriteria.SessionNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
                demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
                demandCriteria.ReceiptTypeCode = "TF";
                demandCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);  //New Branch
                demandCriteria.SemesterNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
                demandCriteria.PaymentTypeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["PTYPE"]);
                demandCriteria.UserNo = Convert.ToInt32(Session["userno"]);
                demandCriteria.CollegeCode = Session["colcode"].ToString();
                demandCriteria.College_ID = Convert.ToInt32(ddlNewCollege.SelectedValue);//NEW COLLEGE Added by Dileep kare on 05012021 client requirement fresh desk ticket number
                //Commented by Rita M.....
                //demandCriteria.DegreeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
                demandCriteria.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);  // New Degree
                //demandCriteria.NewDegreeNo = newdegreeno;
                demandCriteria.AdmBatchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCH"]);

                //New Parameters on 2020 Nov 24
                demandCriteria.StudentId = Convert.ToInt32(Session["idno"]);
                demandCriteria.StudentName = newFees.Tables[0].Rows[0]["STUDNAME"].ToString();
                //demandCriteria.EnrollmentNo = newFees.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString();
                demandCriteria.EnrollmentNo = "";
                //New Parameters on 2020 Nov 24 END

                //FOR ROLLBACK
                //ViewState["SessionNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
                ViewState["SessionNo_RollB"] = Convert.ToInt32(Session["currentsession"]);
                //ViewState["ReceiptTypeCode_RollB"] = newFees.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                ViewState["ReceiptTypeCode_RollB"] = "";
                ViewState["BranchNo_RollB"] = Convert.ToInt32(lblBranch.ToolTip);
                ViewState["NewBranchNo_RollB"] = branchNo;
                ViewState["SemesterNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
                ViewState["PaymentTypeNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["PTYPE"]);
                ViewState["UserNo_RollB"] = Convert.ToInt32(Session["userno"]);
                ViewState["CollegeCode_RollB"] = Session["colcode"].ToString();
                //ViewState["DegreeNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
                 
                // ViewState["DegreeNo_RollB"] = degreeno;
                ViewState["AdmBatchNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCH"]);

                string receipt = string.Empty;
                DataSet dsReceiptCode = objCommon.FillDropDown("ACD_DEMAND", "RECIEPT_CODE", "IDNO", "IDNO=" + Session["idno"].ToString() + "AND SEMESTERNO=" + demandCriteria.SemesterNo + "AND ADMBATCHNO=" + demandCriteria.AdmBatchNo + "AND PAYTYPENO=" + ViewState["PaymentTypeNo_RollB"].ToString() + "AND ISNULL(CAN,0)=0", "");
                if (dsReceiptCode.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsReceiptCode.Tables[0].Rows.Count; i++)
                    {
                        int standardFeesCount = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*) AS CNT",
                    "SEMESTER" + demandCriteria.SemesterNo + "<>0" +
                    "AND DEGREENO=" + Convert.ToInt32(demandCriteria.DegreeNo) +
                    " AND BRANCHNO=" + Convert.ToInt32(demandCriteria.BranchNo) +
                    " AND BATCHNO=" + Convert.ToInt32(demandCriteria.AdmBatchNo) +
                     " AND PAYTYPENO=" + Convert.ToInt32(ViewState["PaymentTypeNo_RollB"].ToString()) +
                     "AND COLLEGE_ID="+Convert.ToInt32(demandCriteria.College_ID)+
                    " AND RECIEPT_CODE='" + dsReceiptCode.Tables[0].Rows[i]["RECIEPT_CODE"].ToString() + "'"));
                        if (standardFeesCount == 0)
                        {
                            if (receipt == string.Empty)
                            {
                                receipt += objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_TITLE", "RECIEPT_CODE='" + dsReceiptCode.Tables[0].Rows[i]["RECIEPT_CODE"].ToString()+"'");
                            }
                            else
                            {
                                receipt += "," + objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_TITLE", "RECIEPT_CODE='" + dsReceiptCode.Tables[0].Rows[i]["RECIEPT_CODE"].ToString()+"'");
                            }
                        }
                    }
                    if (receipt != string.Empty)
                    {
                        objCommon.DisplayMessage(this.Page, "Standard Fees is not defined for new selected Programme/Branch and Degree for : " + receipt + " Receipts.Kindly Create Standard Fees Before submit", this.Page);
                        btnSubmit.Enabled = false;
                        return;
                    }
                }

            }
           
            //Commented by Rita M....
            //string response = dmController.CreateDemandForSelectedStudents_ByBranchChange(Session["idno"].ToString(), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), false, Convert.ToInt32(ViewState["COLLEGE_ID"]));
            //Added by Rita M.....

            colgId = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "COLLEGE_ID", " BRANCHNO=" + Convert.ToInt32(branchNo) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ""));

            // START ********************ADDED BY ROHIT KUAMR TIWARI ON DATE 26-JULY -2019 *****************************
            Demand_Count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1) DEMAND_COUNT", " SESSIONNO=" + demandCriteria.SessionNo + "  AND RECIEPT_CODE='" + demandCriteria.ReceiptTypeCode + "'  AND BRANCHNO = " + demandCriteria.BranchNo + "    AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + demandCriteria.SemesterNo + " AND PAYTYPENO = " + demandCriteria.PaymentTypeNo + " AND UA_NO=" + demandCriteria.UserNo + "  AND IDNO =" + Convert.ToInt32(Session["idno"]) + ""));
            if (Demand_Count > 0)
            {
                Demand_Overwrite = true;
            }
            else
            {
                Demand_Overwrite = false;
            }
            // END **************

            //string response = dmController.CreateDemandForSelectedStudents_ByBranchChange(Session["idno"].ToString(), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), Demand_Overwrite, Convert.ToInt32(colgId), Convert.ToInt32(ddlDegree.SelectedValue));    
            int response = dmController.CreateDemandForSelectedStudents_ByBranchChangeNew(Session["idno"].ToString(), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), Demand_Overwrite, Convert.ToInt32(demandCriteria.College_ID), Convert.ToInt32(ddlDegree.SelectedValue));
            if (rdWithFee.Checked)
            {
            //bool res = feeController.CreateNewDemand(demandCriteria, demandCriteria.PaymentTypeNo);

            if (response != -99)
            {
                if (response < 0)
                {
                    //ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                    objCommon.DisplayMessage(UpdatePanel1, "Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.", this.Page);
                    return; 
                }
                else if (response > 0)
                {
                   // Label lblDmno=

                    ViewState["BranchChangeDemandID"] = response;
                    lblNewBranchFee.Visible = true;
                    //Commented by rita M.....
                    //lblNewBranchFee.Text = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + "AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "")).ToString();

                    //Added by Rita M.....
                    lblNewBranchFee.Text = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + "AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "")).ToString();

                    decimal paidFee = 0;
                    decimal newFee = 0;
                    decimal excessAmt = 0;

                    paidFee = Convert.ToDecimal(lblPaidFees.Text);
                    newFee = Convert.ToDecimal(lblNewBranchFee.Text);

                    if (paidFee > newFee)
                    {
                        excessAmt = paidFee - newFee;
                        lblExcessAmt.Text = excessAmt.ToString();
                    }

                    string response2 = dmController.CancelDemandForSelectedStudent_ByBranchChange(Convert.ToInt32(Session["idno"]), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), false, Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(lblBranch.ToolTip), branchNo, Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(newdegreeno), excessAmt);
                    if (response > 0 && response2 == "2")                    
                    {
                        ViewState["IS_DEMAND_CREATED"] = "YES";
                        rdWithFee.Enabled = false;
                        rdWithoutFee.Enabled = false;
                        lblNote.Visible = true;
                        btnShow.Enabled = false;
                        ddlBranch.Enabled = false;
                        ShowMessage("Demand sucessfully created. Please click on submit for change Programme/Branch.");
                    }
                }                 
            }
            else
                ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
        }
    }


    //private void ShowReport(FeeDemand demandRpt, string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=" + this.GetReportParameters(demandRpt) + (rdoDetailedReport.Checked ? (",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue) : "");
    //        divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_DCR_ReportUI.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void rdWithoutFee_CheckedChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        //ddlBranch.Enabled = true;  //Commeted on 2020 Nov 24
        divDemandButton.Visible = true;
        //
        ddlNewCollege.SelectedValue = ViewState["NEW_COLLEGE_ID"].ToString();

        ddlDegree.SelectedValue = ViewState["NEWDEGREENO"].ToString();
        Bind_Branch();
        ddlBranch.SelectedValue = ViewState["NEWBRANCHNO"].ToString();
        ddlBranch_SelectedIndexChanged(null,null);
        CreateNewDemandWithoughFeePaid();
        //
    }

    protected void rdWithFee_CheckedChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnSubmit.Enabled = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        // ddlBranch.Enabled = true; //Commeted on 2020 Nov 24
        divDemandButton.Visible = true;
        //
        ddlNewCollege.SelectedValue = ViewState["NEW_COLLEGE_ID"].ToString();

        ddlDegree.SelectedValue = ViewState["NEWDEGREENO"].ToString();
        Bind_Branch();
        ddlBranch.SelectedValue = ViewState["NEWBRANCHNO"].ToString();
       
        ddlBranch_SelectedIndexChanged(null, null);
        //
    }
    
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND A.BRANCHNO !=" + ViewState["BranchNoOld"].ToString(), "A.LONGNAME");
        }
        catch (Exception ex)
        {

        }
    }

    void Bind_Branch()
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["NEWDEGREENO"].ToString(), "A.LONGNAME");
        }
        catch (Exception ex)
        {

        }
    }

    DataSet ds = null;

    void BindFeeItems()
    {
        int status = 0;
        int studId = Convert.ToInt32(ViewState["idno"]);
        int semesterNo = Convert.ToInt32(ViewState["semesterNo"]);
       // string dm = objCommon.LookUp("ACD_DEMAND", "DM_NO", "SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND  RECIEPT_CODE='TF' AND BRANCHNO=" + ViewState["NEWBRANCHNO"].ToString() + "AND DEGREENO =" + ViewState["NEWDEGREENO"].ToString() + "AND SEMESTERNO= " +Convert.ToInt32(semesterNo) + "AND IDNO=" + Convert.ToInt32(studId));
       //ds = feeController.GetFeeItems_Data(Convert.ToInt32(Session["currentsession"]), studId, semesterNo , 'TF', 0, 1, Convert.ToInt32(ViewState["PaymentTypeNo"]), ref status);
       //ds = feeController.GetFeeItems_Data(Convert.ToInt32(Session["currentsession"]), studId, semesterNo, "TF", 0, 1, Convert.ToInt32(ViewState["PTYPENO"]), ref status);
         //demandCriteria.ReceiptTypeCode
        DemandModificationController dmController = new DemandModificationController();
        DataSet ds = dmController.GetDemand(Convert.ToInt32(ViewState["BranchChangeDemandID"]), Convert.ToInt32(ViewState["idno"]));

        if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvFeeItems.DataSource = ds;
            lvFeeItems.DataBind();

            string RecieptCode = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            if (RecieptCode == "TF" || RecieptCode == "EF" || RecieptCode == "HF" || RecieptCode == "BCA" || RecieptCode == "MBA" || RecieptCode == "PG" || RecieptCode == "EVF" ||
                RecieptCode == "PGF" || RecieptCode == "BMF" || RecieptCode == "BHE" || RecieptCode == "PDF" || RecieptCode == "UNG" || RecieptCode == "TOF" || RecieptCode == "AF" ||
                RecieptCode == "SVA" || RecieptCode == "JUM" || RecieptCode == "PC" || RecieptCode == "TPF")
            {
                /// Show remark for current fee demand
                //txtRemark.Text = ds.Tables[0].Rows[0]["PARTICULAR"].ToString();
                //txtFeeBalance.Text = ds.Tables[0].Rows[0]["EXCESS_AMT"].ToString();

                /// Set FeeCatNo from datasource
                ViewState["FeeCatNo"] = ds.Tables[0].Rows[0]["FEE_CAT_NO"].ToString();

                /// Show total fee amount to be paid by the student in total amount textbox.
                /// This total fee amount can be changed by user according to the student's current 
                /// payment amount (i.e. student can do part payment of Fee also).
                //  txtTotalAmount.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                double totalamt = Convert.ToDouble(this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString());
                if (totalamt < 0)
                {
                    txtTotalAmountShow.Text = "0.00";
                }
                else
                {
                    txtTotalAmountShow.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                }
                // lblamtpaid.Text = objCommon.LookUp("acd_demand", "TOTAL_AMT", "IDNO=" + studId + " and SEMESTERNO=" + semesterNo + " and sessionno="+Convert.ToInt32(Session["currentsession"])+" and paytypeno="+Convert.ToInt16(ViewState["PaymentTypeNo"])+" and  RECIEPT_CODE='" + GetViewStateItem("ReceiptType")+"'");
                lblamtpaid.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                //txtTotalAmount.Text = txtTotalAmountShow.Text;
                // txtTotalFeeAmount.Text = txtTotalAmount.Text;
                txtTotalFeeAmount.Text = txtTotalAmountShow.Text;
            }
        }

    }

    protected void lnkDemandNew_Click(object sender, EventArgs e)
     {
        BindFeeItems();
        divFeeItems.Visible = true;
        divDemandButton.Visible = false;
    }

    private double GetTotalFeeDemandAmount(DataTable dt)
    {
        double totalFeeAmt = 0.00;
        try
        {
            foreach (DataRow dr in dt.Rows)
                totalFeeAmt += ((dr["AMOUNT"].ToString().Trim() != string.Empty) ? Convert.ToDouble(dr["AMOUNT"].ToString()) : 0.00);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetTotalFeeDemandAmount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return totalFeeAmt;
    }

    ////////////////////////////////////////////////////////////////////////

    public void SubmitDemand(FeeDemand feeDemand, int paymentTypeNoOld)
    {
        DailyCollectionRegister dcr = this.Bind_FeeCollectionData();

        feeController.CreateNewDemandFromBranchChange(feeDemand, paymentTypeNoOld, ref dcr);
    }

    private DailyCollectionRegister Bind_FeeCollectionData()
    {
        DailyCollectionRegister dcr = new DailyCollectionRegister();
        //int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO=" + Session["idno"].ToString()));
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME", "IDNO = " + Session["idno"].ToString(),"");
            if (ds.Tables[0].Rows.Count > 0)
            {
                dcr.StudentId = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"]);
                dcr.EnrollmentNo = ds.Tables[0].Rows[0]["REGNO"].ToString();
                dcr.StudentName = ds.Tables[0].Rows[0]["STUDNAME"].ToString();

                dcr.BranchNo = Convert.ToInt32(ViewState["NEWBRANCHNO"]);
                dcr.BranchName = lblBranch.Text;
                //dcr.YearNo = (GetViewStateItem("YearNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("YearNo")) : 0;
                dcr.DegreeNo = Convert.ToInt32(ViewState["NEWDEGREENO"]);
                dcr.SemesterNo = Convert.ToInt32(ViewState["semesterNo"]);
            }
            
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
            //dcr.Currency = ((ddlCurrency.SelectedIndex > 0 && ddlCurrency.SelectedValue != string.Empty) ? Int32.Parse(ddlCurrency.SelectedValue) : 1);
            dcr.FeeHeadAmounts = this.GetFeeItems();
            dcr.TotalAmount = (txtTotalFeeAmount.Text.Trim() != string.Empty) ? Convert.ToDouble(txtTotalFeeAmount.Text) : 0.00;

            dcr.ReceiptTypeCode = "TF";
            //dcr.ReceiptDate = ((txtReceiptDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtReceiptDate.Text) : DateTime.MinValue);
            dcr.PaymentType = Convert.ToString(ViewState["PTYPENO"]);

            dcr.IsDeleted = false;
            dcr.CompanyCode = string.Empty;
            dcr.RpEntry = string.Empty;
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.PrintDate = DateTime.Today;
            dcr.Remark = txtRemark.Text.Trim();
            dcr.ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
            dcr.CollegeCode = Session["colcode"].ToString();


            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                string fee_head = string.Empty;//***************
                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************
                string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();

                if (fee_head == "LATE FEE")
                {
                    if (feeAmt != null && feeAmt != string.Empty)
                    {
                        dcr.Late_fee = Convert.ToDouble(feeAmt);

                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.Bind_FeeCollectionData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        return dcr;
    }

    private FeeHeadAmounts GetFeeItems()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                int feeHeadNo = 0;
                double feeAmount = 0.00;

                string fee_head = string.Empty;//***************
                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************

                if (fee_head != "LATE FEE")//*****************
                {
                    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
                    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                        feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                    if (feeAmt != null && feeAmt != string.Empty)
                        feeAmount = Convert.ToDouble(feeAmt);

                    feeHeadAmts[feeHeadNo - 1] = feeAmount;
                }
            }
             
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }

    private void CreateNewDemandWithoughFeePaid()
    {
        int colgId = 0;
        int Demand_Count = 0;
        Boolean Demand_Overwrite = false;
        DemandModificationController dmController = new DemandModificationController();
       

        //DataSet newFees = objCommon.FillDropDown("ACD_DEMAND", "*", string.Empty, "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(lblBranch.ToolTip) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "", string.Empty);

         

        colgId = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "COLLEGE_ID", " BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ""));

        //
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT", "STUDNAME,PTYPE,SEMESTERNO,ADMBATCH", "", "IDNO = " + Session["idno"].ToString(), "");
        if (dsStudent != null && dsStudent.Tables[0].Rows.Count > 0)
        {
            demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);  // New Degree
            demandCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);  //New Branch
            demandCriteria.SemesterNo = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
            demandCriteria.PaymentTypeNo = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"]);
            demandCriteria.UserNo = Convert.ToInt32(Session["userno"]);
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.College_ID = colgId;
            demandCriteria.AdmBatchNo = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"]);
            demandCriteria.StudentId = Convert.ToInt32(Session["idno"]);
            demandCriteria.StudentName = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
            demandCriteria.EnrollmentNo = "";
        }

        Demand_Count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1) DEMAND_COUNT", " SESSIONNO=" + demandCriteria.SessionNo + "  AND RECIEPT_CODE='" + demandCriteria.ReceiptTypeCode + "'  AND BRANCHNO = " + demandCriteria.BranchNo + "    AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + demandCriteria.SemesterNo + " AND PAYTYPENO = " + demandCriteria.PaymentTypeNo + " AND UA_NO=" + demandCriteria.UserNo + "  AND IDNO =" + Convert.ToInt32(Session["idno"]) + ""));
        if (Demand_Count > 0)
        {
            Demand_Overwrite = true;
        }
        else
        {
            Demand_Overwrite = false;
        }
        int response = dmController.CreateDemandForSelectedStudents_ByBranchChangeNew(Session["idno"].ToString(), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), Demand_Overwrite, Convert.ToInt32(colgId), Convert.ToInt32(ddlDegree.SelectedValue));
        if (response != -99)
        {
            if (response < 0)
            {
                ShowMessage("Unable to create demand for following students.\\nStudent Name.: " + demandCriteria.StudentName + "\\nStandard fees is not defined for fees criteria applicable to these students.");
            }
            else if (response > 0)
            {
                ViewState["BranchChangeDemandID"] = response;
                //Label lbldemno = e.Item.FindControl("lblDemand") as Label;
                lblNewBranchFee.Visible = true;
                lblNewBranchFee.Text = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "")).ToString();
            }
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ExportinExcelBranchChange();
    }
    private void ExportinExcelBranchChange()
    {
        BranchController objBrn = new BranchController();
        string attachment = "attachment; filename=" + "BranchChangeExcel.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = objBrn.GetBranchChangeFirstLevel();

        DataGrid dg = new DataGrid();

        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }
    protected void ddlStudentList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        divMsg.InnerHtml = string.Empty;
        rdWithFee.Checked = false;
        rdWithoutFee.Checked = false;
        rdWithFee.Enabled = true;
        rdWithoutFee.Enabled = true;
        lblNote.Visible = false;
        //ddlBranch.Enabled = true;  //COmmeted on 2020 Nov 24
        lblCurrentfeess.Text = "0";
        lblPaidFees.Text = "0";
        lblNewBranchFee.Text = "0";
        lblExcessAmt.Text = "0";
        ViewState["IS_DEMAND_CREATED"] = null;
        //if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        //{
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    lblMsg.Text = "Please Enter Proper Reg. No.";
        //    txtStudent.Focus();
        //    return;
        //}
        lblMsg.Text = string.Empty;

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";
            string category = "";
            string admcan = "0";
            string FirstLevelApprove = "0";
            decimal paidfees = 0;
            //idno = objCommon.LookUp("acd_student", "idno", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
            idno = ddlStudentList.SelectedValue;
            if (idno == string.Empty)
            {
                lblName.Text = string.Empty;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Student Not Found!!";
                this.ClearControls();
                btnSubmit.Enabled = false;
                return;
            }
            if (txtStudent.Text.Trim() != string.Empty)
            {
                admcan = objCommon.LookUp("acd_student", "isnull(admcan,0)", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
                if (admcan == "True")
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Entered student admission has been cancelled!", this.Page);
                    txtStudent.Text = "";
                    txtStudent.Focus();
                    return;
                }

                FirstLevelApprove = objCommon.LookUp("ACD_BRCHANGE", "IS_FIRST_LEVEL_APPROVE", "IDNO=" + idno);

                if (String.IsNullOrEmpty(FirstLevelApprove))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);
                    return;
                }

                if (!String.IsNullOrEmpty(FirstLevelApprove))
                {
                    if (FirstLevelApprove.ToLower() == "false")
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);
                        return;
                    }
                }

                //if (admcan == "True")
                //{
                //    objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);                    
                //    return;
                //}

                if (txtStudent.Text.Contains("("))
                {
                    if (txtStudent.Text.Contains("["))
                    {
                        char[] ct = { '(' };
                        string[] cat = txtStudent.Text.Trim().Split(ct);
                        //idno value
                        category = cat[1].Replace(")", "");
                        cat = category.Split('[');
                        category = cat[0].Replace("]", "");
                        char[] sp = { '[' };
                        string[] data = txtStudent.Text.Trim().Split(sp);
                        //idno value
                        idno = data[1].Replace("]", "");
                        ViewState["idno"] = Convert.ToInt32(idno);
                    }

                }
            }
            string degreeNo = objCommon.LookUp("acd_student", "degreeno", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
            ViewState["idno"] = idno.ToString();


            imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            DataTableReader dtr = objSRegist.GetStudentDetailsNew(Convert.ToInt32(idno));

            if (dtr.Read())
            {
                Session["idno"] = dtr["idno"].ToString();
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString(); ;
                lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                ViewState["OldRollNo"] = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                lblRollNo.Text = ViewState["OldRollNo"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                lblDegree.Text = dtr["DEGREENAME"].ToString();
                lblDegree.ToolTip = dtr["DEGREENO"].ToString();

                lblColg.Text = dtr["COLLEGE_NAME"].ToString();
                lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();

                ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
                ViewState["degreeNo"] = dtr["DEGREENO"].ToString();

                ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
                ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                ViewState["BranchNoOld"] = dtr["BRANCHNO"].ToString();

                ViewState["NEWDEGREENO"] = dtr["NEW_DEGREENO"].ToString();
                ViewState["NEWBRANCHNO"] = dtr["NEWBRANCHNO"].ToString();
                ViewState["BRCH_NO"] = dtr["BRCHNO"].ToString();
                ViewState["NEW_COLLEGE_ID"] = dtr["NEW_COLLEGE_ID"].ToString();

                ViewState["PTYPENO"] = dtr["PTYPE"].ToString();
                lblRequestRemark.Text = dtr["REQUEST_REMARK"].ToString();
                lblAcademicRemark.Text = dtr["ACADEMIC_REMARK"].ToString();
                lblApprovedRemark.Text = dtr["APPROVE_REMARK"].ToString();

                ViewState["FileName"] = dtr["FILE_NAME"].ToString();
                ViewState["PREVIEW"] = dtr["PREVIEW"].ToString();
                string filename = dtr["FILE_NAME"].ToString();
                if (!String.IsNullOrEmpty(filename))
                {
                    lnkView.Visible = true;
                    lblView.Visible = false;
                }
                else
                {
                    lblView.Visible = true;
                    lblView.Text = "Preview not Available";
                    lnkView.Visible = false;
                }

                btnSubmit.Enabled = true;
                divdata.Visible = true;
                //}

                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["idno"].ToString() + "&type=student";
                // Commented by Rita M........
                //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", "B.BRANCHNO", "B.LONGNAME", "DB.COLLEGE_ID=" + Convert.ToInt32(dtr["COLLEGE_ID"]) + " AND DB.DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"]) + " AND DB.BRANCHNO<>" + Convert.ToInt32(dtr["BRANCHNO"]), "LONGNAME");

                //Added by Rita M...............
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]), "A.DEGREENO ASC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["NEW_COLLEGE_ID"].ToString()), "A.DEGREENO ASC");
                
                lblCurrentfeess.Text = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

                lblPaidFees.Text = objCommon.LookUp("ACD_DCR", "SUM(ISNULL(TOTAL_AMT,0))", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

                if (lblPaidFees.Text.Equals(string.Empty))
                {
                    lblPaidFees.Text = Convert.ToString(paidfees);
                }


                int isBranchChanged = Convert.ToInt32(objCommon.LookUp("ACD_BRCHANGE", "ISNULL(COUNT(IDNO),0)COUNT", "REGNO='" + txtStudent.Text.Trim() + "'"));

                if (isBranchChanged >= 2)
                {
                    pnlBranchChange.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    rdWithFee.Enabled = false;
                    rdWithoutFee.Enabled = false;
                    // ShowMessage("Branch change already done for filtered student.");
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "ALERT: Programme/Branch change already done for filtered student.You Cannot Change Branch More than two times.";
                    return;
                }
                else if (isBranchChanged >= 1)
                {
                    pnlBranchChange.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    rdWithFee.Enabled = true;
                    rdWithoutFee.Enabled = true;
                    // ShowMessage("Branch change already done for filtered student.");
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "ALERT: Programme/Branch change already done for filtered student.";
                    return;
                }

            }
            else
            {
                //lblRegNo.Text = string.Empty;
                btnSubmit.Enabled = false;
            }
            dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void lnkView_Click(object sender, EventArgs e)
    {
        string path = ViewState["PREVIEW"].ToString();
        iframeView.Attributes.Add("src", path);

        mpeViewDocument.Show();
    }
    public void TransferToEmail(string studname, string Regno, string degree, string oldbranch, string newbranch, string remark, string College)
    {
        try
        {
            int ret = 0;
            //  string Session = ddlSession.SelectedItem.Text;
            // string sem = ddlSem.SelectedItem.Text;//kare.dileep@mastersofterp.co.in
            string useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=4");
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();



                msg.From = new MailAddress(fromAddress, "MAKAUT - Programme/Branch Change");
                msg.To.Add(new MailAddress(useremail));



                msg.Subject = "Regarding Programme/Branch Change Approval";
                //FOR MANISH : ERR: AT HTML TAGS :
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                const string EmailTemplate = "<html><body>" +
                                            "<div align=\"center\">" +
                                            "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                             "<tr>" +
                                             "<td>" + "</tr>" +
                                             "<tr>" +
                                            "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                            "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b><br/></td>" +
                                            "</tr>" +
                                            "</table>" +
                                            "</div>" +
                                            "</body></html>";
                StringBuilder mailBody = new StringBuilder();
                //  mailBody.AppendFormat("<h1>Greetings !!</h1>");
                mailBody.AppendFormat("Dear Sir/Madam <b>" + "" + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Below Student has opted for a Programme/Branch change that required your approval with Comments.");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>Student Details </b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant New Reg. No. : </b> " + Regno + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Name : </b>" + studname + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicantion Date :</b> " + DateTime.Now.ToString("dd/MM/yyyy") + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> College Name  : </b>" + lblColg.Text + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Department  : </b>" + degree + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Currenct Programme/Branch : </b>" + oldbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> New Programme/Branch  : </b>" + newbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>New College Name  : </b>" + College + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                //  mailBody.AppendFormat("<b>Your new Login Password is </b>");
                mailBody.AppendFormat("<b>Comments :" + remark + " </b>");
                mailBody.AppendFormat("<br />");
                string Mailbody = mailBody.ToString();
                string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                msg.IsBodyHtml = true;
                msg.Body = nMailbody;


                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);


                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };




                smtp.Send(msg);

                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                {
                    ret = 1;
                    //    objCommon.DisplayMessage(updSession, "Email Sent Successfully.", this.Page);
                    //Storing the details of sent email
                }

            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvFeeItems_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
      //  Label lbldemno = (e.Item.FindControl("lbldemand") as Label);
       // lbldemno.ToolTip.ToString();

       

    }
}