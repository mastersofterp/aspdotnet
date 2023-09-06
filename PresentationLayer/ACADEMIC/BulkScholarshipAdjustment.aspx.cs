using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_BulkScholarshipAdjustment : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    QrCodeController objQrC = new QrCodeController();
    int prev_status;
    DailyCollectionRegister dcr = new DailyCollectionRegister();
    FeeCollectionController feeController = new FeeCollectionController();

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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    txtToDate.Text = DateTime.Today.ToShortDateString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //ShowDetails();
                }

                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                {
                    //divSchltype.Visible = false;
                    ddlScholarShipsType.SelectedValue = "1";
                    ddlReceipt.SelectedIndex = 1;
                }
                else
                {
                    lblYearMandatory.Visible = true;
                    ddlReceipt.SelectedIndex = 1;
                }

                //if (rdoSingleStudent.Checked)
                //{
                //    pnlSingleStud.Visible = true;
                //    objCommon.FillDropDownList(ddlSessionSingleStud, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                //    // ddlSessionSingleStud
                //}
                //else
                //{
                //    pnlBulkStud.Visible = true;
                //}
            }
            // lblSession.Text = Convert.ToString(Session["sessionname"]);

            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            throw;
        }
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
    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN SCHEME TYPE
            //objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            // FILL DROPDOWN BATCH
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH AB INNER JOIN ACD_STUDENT_SCHOLERSHIP SS ON(AB.BATCHNO = SS.ADMBATCH)", "DISTINCT AB.BATCHNO", "AB.BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlReceipt, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO=1", "RECIEPT_TITLE");
            //objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0", "college_id");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlBankName, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
            //  objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");

            objCommon.FillDropDownList(ddlScholarShipsType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0  AND ACTIVESTATUS=1 ", "SCHOLORSHIPTYPENO");  // added on 2020 feb 11

            txtDateofissue.Text = DateTime.Today.ToShortDateString();

            // FILL DROPDOWN ADMISSION BATCH

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BindListView()
    {
        try
        {
            DataSet ds;
            int sessionno = Convert.ToInt32(Session["currentsession"]);

            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }
            //ds = studCont.GetStudentListForAdmitCardNoDues(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            //ds = studCont.GetStudentScholershipAdjustment(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            string dd = ddlScholarShipsType.SelectedValue;
            int ScholarshipId = Convert.ToInt32(ddlScholarShipsType.SelectedValue); 
            
           // ds = studCont.GetStudentScholershipAdjustment(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            ds = studCont.GetStudentScholershipAdjustment(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), ScholarshipId);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
                lvStudentRecords.Visible = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
                foreach (ListViewDataItem itm in lvStudentRecords.Items)
                {
                    CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                    HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;
                    Label lblname = itm.FindControl("lblname") as Label;
                    Label lblamt = itm.FindControl("lblschamt") as Label;
                    Label lblschajmt = itm.FindControl("lblschajmt") as Label;
                    Label lblBranch = itm.FindControl("lblDDLData") as Label;
                    Label lblRemainingAmt = itm.FindControl("lblRemainingAmt") as Label;
                    Label txtAdjAmount = itm.FindControl("lblschajmt") as Label;
                    HiddenField hdfBranchno = itm.FindControl("hdfBranchno") as HiddenField;
                    HiddenField hdfdegreeno = itm.FindControl("hdfDegree") as HiddenField;

                    string count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + hdnf.Value + "  AND BRANCHNO= " + hdfBranchno.Value + "  AND DEGREENO=" + hdfdegreeno.Value + "AND SEMESTERNO=" + Convert.ToInt32(lblamt.ToolTip) + " AND  SCHOLARSHIP_ID=" + ScholarshipId + " AND  PAY_MODE_CODE='SA'  AND RECON=1 AND CAN=0");


                   if (count != "0")
                   {
                       Decimal DCRTOTAL = 0;

                       DataSet da = studCont.GetDCRTotalAmountForStudentScholershipAdjustment(Convert.ToInt32(hdnf.Value), Convert.ToInt32(lblamt.ToolTip), Convert.ToInt32(lblBranch.Text));

                       if (da.Tables[0].Rows.Count > 0)
                       {
                           DCRTOTAL = Convert.ToDecimal(da.Tables[0].Rows[0]["TOTAL_AMT"].ToString());
                       }

                       if (DCRTOTAL >= Convert.ToDecimal(lblamt.Text) && da.Tables[0].Rows.Count > 0)
                       {
                           chk.Checked = true;
                           chk.Enabled = false;
                       }
                       else
                       {
                           chk.Checked = false;
                           chk.Enabled = true;
                       }
                   }                       
                    //if (count != "0")
                    //{
                    //    chk.Checked = true; 
                    //    chk.Enabled = false;
                    //}
                    //else
                    //{
                    //    chk.Checked = false;
                    //    chk.Enabled = true;
                    //}
                    //Label lblDDLData = (Label)itm.FindControl("lblDDLData");

                    //if (lblamt.Text.Trim().Equals(lblschajmt.Text))
                    //{
                    //    chk.Checked = true;
                    //    chk.Enabled = false;

                    //}
                    //else if (txtAdjAmount.Text.Trim().Equals(string.Empty))
                    //{
                    //    chk.Checked = false;
                    //    chk.Enabled = true;
                    //}               
                }
            }

            else
            {
                objCommon.DisplayMessage(updtime1, "Record Not Found,Kindly Check for Demand is Creates or Not.", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
            {
                this.BindListView();
                btnSubmit.Enabled = true;
            }
            else
            {
                if (ddlYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Year", this.Page);
                    return;
                }
                else
                {
                    this.BindListView();
                    btnSubmit.Enabled = true;
                }
               
            }
           
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO) INNER JOIN ACD_STUDENT_SCHOLERSHIP SS ON(B.DEGREENO=SS.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_STUDENT_SCHOLERSHIP SS ON (A.BRANCHNO=SS.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            // ddlAdmbatch.SelectedIndex = 0;
            // ddlAdmbatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 ", "SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            //ddlBranch.Items.Clear();
            //ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }

        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;
        try
        {
            //DataSet ds = feeController.GetNewReceiptData(GetViewStateItem("PaymentMode"), Int32.Parse(Session["userno"].ToString()), ViewState["ReceiptType"].ToString());
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    //String reciptCode;
            //    //reciptCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECEIPT_CODE","");
            //    String FeesSessionStartDate;
            //    FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");

            //    DataRow dr = ds.Tables[0].Rows[0];
            //    dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
            //    //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
            //    //receiptNo = dr["PRINTNAME"].ToString() + "/" + GetViewStateItem("PaymentMode") + "/" + Session["FeesSessionStartDate"].ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString();
            //    receiptNo = dr["PRINTNAME"].ToString() + "/" + GetViewStateItem("PaymentMode") + "/" + ViewState["ReceiptType"].ToString() + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
            //    // save counter no in hidden field to be used while saving the record
            //    ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            //}
            //int a = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "DCR_NO>0")) + 1;
            //receiptNo = "SVVV/B-Tech/C/" + a;

            //int a = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL( MAX(DCR_NO),0)", "DCR_NO>0"));

            //int a = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL( MAX(DCR_NO),0)", string.Empty));

            //int RecNo = a + 1;

            //receiptNo = "SVVV/" + RecNo;

            ViewState["ReceiptType"] = ddlReceipt.SelectedValue;

            DataSet ds = feeController.GetNewReceiptData("C", Int32.Parse(Session["userno"].ToString()), ViewState["ReceiptType"].ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //String reciptCode;
                //reciptCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECEIPT_CODE","");
                String FeesSessionStartDate;
                FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");

                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                //receiptNo = dr["PRINTNAME"].ToString() + "/" + GetViewStateItem("PaymentMode") + "/" + Session["FeesSessionStartDate"].ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString();
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "SA" + "/" + ViewState["ReceiptType"].ToString() + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return receiptNo;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //objCommon.ConfirmMessage(this.updtime1, "Are you sure you Bulk Scholarship Adjustment?", this.Page);
        this.SaveScholarshipAdjustment();
        ddlBankName.SelectedIndex = 0;
        txtToDate.Text = string.Empty;
        txtRemark.Text = string.Empty;


    }

    private void SaveScholarshipAdjustment()
    {
        int id = 0;
        int count = 0;
        int output = 0;
        bool flag = false;
        GetNewReceiptNo();

        foreach (ListViewDataItem itm in lvStudentRecords.Items)
        {
            CheckBox chk = itm.FindControl("chkReport") as CheckBox;
            // if (chk.Checked == true && chk.Enabled == true)
            if (chk.Checked == true && chk.Enabled == true )
            {
                TextBox txtSemesterAmount = itm.FindControl("txtSemesterAmount") as TextBox;
                //DropDownList ddlScholarShipsType = itm.FindControl("ddlScholarShipsType") as DropDownList;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;
                Label lblname = itm.FindControl("lblname") as Label;
                Label lblamt = itm.FindControl("lblschamt") as Label;
                Label lblExcessAmount = itm.FindControl("lblRemainingAmt") as Label;
                TextBox txtAdjAmt = itm.FindControl("txtAdjAmount") as TextBox;
                Label lblBranch = itm.FindControl("lblDDLData") as Label;
                HiddenField hdfBranchno = itm.FindControl("hdfBranchno") as HiddenField;
                HiddenField hdfdegreeno = itm.FindControl("hdfDegree") as HiddenField;
                HiddenField hfdyearname = itm.FindControl("hfdyearname") as HiddenField;

                CheckBox chkRpt = (CheckBox)itm.FindControl("chkReport");
                dcr.StudentId = Convert.ToInt32(chkRpt.ToolTip.ToString());
                TextBox txtDDNumber = itm.FindControl("txtDDNumber") as TextBox;
                //dcr.StudentId = Convert.ToInt32(hdnf.Value);
                dcr.StudentName = lblname.Text;
                //double f4 = Convert.ToDouble(lblamt.Text);

                dcr.UserNo = Int32.Parse(Session["userno"].ToString());
                dcr.CounterNo = Convert.ToInt32(ViewState["CounterNo"].ToString());
                dcr.ReceiptTypeCode = ddlReceipt.SelectedValue;


                if (txtAdjAmt.Text == "")
                {
                    objCommon.DisplayUserMessage(updtime1, "Please Enter Scholarship Paid Amount", this.Page);
                    return;
                }

                if (txtToDate.Text.Trim().Equals(string.Empty))
                {
                    dcr.ReceiptDate = System.DateTime.Now;
                }
                else
                {
                    dcr.ReceiptDate = Convert.ToDateTime(txtToDate.Text);
                }
                //dcr.ReceiptDate = Convert.ToDateTime(txtToDate.Text);                    
                dcr.DegreeNo = Convert.ToInt32(hdfdegreeno.Value);
                dcr.BranchNo = Convert.ToInt32(hdfBranchno.Value);
                dcr.YearNo = Convert.ToInt32(hfdyearname.Value);
                dcr.SemesterNo = Convert.ToInt32(lblamt.ToolTip);
                dcr.EnrollmentNo = lblname.ToolTip;
                dcr.BranchName = lblBranch.ToolTip;
                dcr.PaymentModeCode = "SA";
                dcr.PaymentType = "SA";
                dcr.Currency = 1;
                dcr.DcrNo = 1;
                dcr.Remark = txtRemark.Text;
                double f4 = Convert.ToDouble(txtAdjAmt.Text);
                int bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                string transactionid = txtDDNumber.Text;


                int academicYearID = Convert.ToInt32(ddlAcdYear.SelectedValue);


                if (count == 0)
                {
                    //objCommon.ConfirmMessage(this.updtime1, "Are you sure to adjust scholarship for selected students?", this.Page);
                }

                int ScholarshipId = Convert.ToInt32(ddlScholarShipsType.SelectedValue);
                
              
                output = feeController.InsertScholarshipAdjustment(ref dcr, f4, bankid, transactionid, academicYearID, ScholarshipId);
                count++;

                string ScholorshipNO = (objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "ISNULL(SCHLSHPNO,0)", "IDNO=" + dcr.StudentId + "AND SEMESTERNO=" + dcr.SemesterNo + "AND SCHOLARSHIP_ID=" + ScholarshipId));
                feeController.UpdateShcolorshipStatus(ScholorshipNO, f4, int.Parse(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
                this.BindListView();
            }
        }
        if (count == 0)
        {
            //objCommon.DisplayUserMessage(updtime1,"Please Select Atleast one Student",this.Page);

            //return;
        }
        //  objCommon.DisplayMessage(updtime1, "Adjustment Successfully Done.",Page);
        else
            objCommon.DisplayUserMessage(updtime1, "Scholarship Adjusted Successfully.", this.Page);
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlAcdYear.SelectedIndex = -1;
        //ddlAdmBatch.SelectedIndex = -1;
        //ddlBranch.SelectedIndex = -1;
        //ddlDegree.SelectedIndex = -1;
        //ddlColg.SelectedIndex = -1;
        //ddlSemester.SelectedIndex = -1;
        //ddlYear.SelectedIndex = -1;
        //ddlAdmBatch.SelectedIndex = -1;
        //lvStudentRecords.DataSource = null;
        //lvStudentRecords.DataBind();
        //btnSubmit.Enabled = false;
        Response.Redirect(Request.Url.ToString());

        //  Response.Redirect("~/Academic/BulkScholarshipAdjustment.aspx", false);
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        btnSubmit.Enabled = false;
    }

    protected void ddlScholarShipsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        btnSubmit.Enabled = false;
    }
    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        btnSubmit.Enabled = false;
    }
    protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        btnSubmit.Enabled = false;
    }
   
}