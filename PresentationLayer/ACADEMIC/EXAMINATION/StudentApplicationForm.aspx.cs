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
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Mail;

public partial class ACADEMIC_EXAMINATION_StudentApplicationForm : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                    if (Session["usertype"].ToString() == "2")
                {
                    divstudentsection.Visible=true;
                    divadminsection.Visible = false;                    
                    this.ShowDetails();                 
                    btnSubmit.Visible = true;
                    btnRemoveList.Visible = true;
                    getpreviousreciepts();
                }
                    else if (Session["usertype"].ToString() == "1")
                {
                    divstudentsection.Visible = false;                   
                    divadminsection.Visible = true;
                    btnSubmit.Visible = false;
                    btnRemoveList.Visible = false;
                    objCommon.FillDropDownList(ddlApplicationForAdmin, "ACD_APPLICATION_MASTER", "APNO", "APNAME", "APNO>0", "APNO ASC");
                }
            }
        }  
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentApplicationForm.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentApplicationForm.aspx");
        }
    }
    #region studentlogin
    private void ShowDetails()
    {
        int idno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (Session["usertype"].ToString() == "1")
        {
            idno = Convert.ToInt32(ViewState["idno"]);         
        }
        try
        {
            if (idno > 0)
            {
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString();
                         lblBranch.Text= dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();                      
                        lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        ViewState["COLLEGE_ID"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        ViewState["IDNO"] = lblName.ToolTip;                   
                        lblsession.Text = Session["sessionname"].ToString() == null ? string.Empty : Session["sessionname"].ToString();
                        objCommon.FillDropDownList(ddlApplication, "ACD_APPLICATION_MASTER", " APNO", "APNAME", " APNO>0", "APNO ASC");
                        DataSet ds = objCommon.FillDropDown("ACD_APPLICATION_MASTER", "APNAME", "AMOUNT", "APNO>0", "APNO ASC");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblappname1.Text = ds.Tables[0].Rows[0]["APNAME"].ToString();
                            lblappamount1.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                            lblappname2.Text = ds.Tables[0].Rows[1]["APNAME"].ToString();
                            lblappamount2.Text = ds.Tables[0].Rows[1]["AMOUNT"].ToString();
                            lblappname3.Text = ds.Tables[0].Rows[2]["APNAME"].ToString();
                            lblappamount3.Text = ds.Tables[0].Rows[2]["AMOUNT"].ToString();
                            lblappname4.Text = ds.Tables[0].Rows[3]["APNAME"].ToString();
                            lblappamount4.Text = ds.Tables[0].Rows[3]["AMOUNT"].ToString();
                            lblappname5.Text = ds.Tables[0].Rows[4]["APNAME"].ToString();
                            lblappamount5.Text = ds.Tables[0].Rows[4]["AMOUNT"].ToString();
                        }                       
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void getpreviousreciepts()
    {
        divreciept.Visible = true;
        DataSet dsreciept = objSC.GetStudentRecieptInformation(Convert.ToInt32(Session["IDNO"]));
        if (dsreciept.Tables[0].Rows.Count > 0)
        {
            lvrecieptinfo.DataSource = dsreciept;
            lvrecieptinfo.DataBind();
            foreach (ListViewItem item in lvrecieptinfo.Items)
            {
                Label lblstuappstatus = item.FindControl("lblstuapprstatus") as Label;
                 Label lblapaystatus = item.FindControl("lblpaystatus") as Label;
                 ImageButton btnprint = item.FindControl("btnPrintReceipt") as ImageButton;

                 if (lblapaystatus.Text.ToUpper() == "PENDING")
                {
                    lblapaystatus.Style.Add("color", "BLUE");
                    btnprint.Enabled = true;
                }
                 else if (lblapaystatus.Text.ToUpper() == "COMPLETE")                     
                 {
                     lblapaystatus.Style.Add("color", "GREEN");
                     btnprint.Enabled = false;
                 }

                 else
                 {
                     lblapaystatus.Style.Add("color", "RED");
                     btnprint.Enabled = false;

                 }
                 if (lblstuappstatus.Text.ToUpper() == "APPROVED")
                 {
                    
                     lblstuappstatus.Style.Add("color", "GREEN");
                 }
                 else if (lblstuappstatus.Text.ToUpper() == "PENDING")
                 {
                    
                     lblstuappstatus.Style.Add("color", "BLUE");
                 }
                 else
                 {
                   
                     lblstuappstatus.Style.Add("color", "RED");
                 }
            }
        }
        else
        {
            lvrecieptinfo.DataSource = null;
            lvrecieptinfo.DataBind();

        }

    }
    private void GetStudentApplication()
    {
        DataSet dsstudname = objCommon.FillDropDown("ACD_STUDENT A  LEFT OUTER JOIN ACD_STU_ADDRESS B ON(A.IDNO=B.IDNO)", "distinct A.IDNO, A.STUDNAME,A.FATHERNAME,A.MOTHERNAME", "B.PADDRESS", "A.IDNO=" + Convert.ToInt32(Session["idno"]), "A.IDNO");
        DataSet ds = objCommon.FillDropDown("ACD_APPLICATION_TRANS A INNER JOIN ACD_DCR B ON(A.DCR_NO=B.DCR_NO) INNER JOIN ACD_STUDENT C ON(A.IDNO=C.IDNO)", "A.CORR_STUDNAME,A.CORR_FATHERNAME,A.CORR_MOTHERNAME", "(CASE WHEN A.APPROVE_STATUS=0 THEN 'PENDING'  WHEN A.APPROVE_STATUS=1 THEN 'APPROVED' WHEN A.APPROVE_STATUS=2 THEN 'REJECTED' ELSE 'NA' END) AS APPROVE_STATUS,A.REASON,C.STUDNAME,C.FATHERNAME,C.MOTHERNAME,A.NOOFCOPIES", "A.APNO=" + Convert.ToInt32(ddlApplication.SelectedValue) + "AND A.IDNO=" + Convert.ToInt32(Session["IDNO"]) + "AND B.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND B.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip), "");
        divReason.Visible = true;
        txtReason.Text = ds.Tables[0].Rows[0]["REASON"] == null ? string.Empty : ds.Tables[0].Rows[0]["REASON"].ToString();
        txtReason.Enabled = false;
        btnSubmit.Visible = false;
        divstatus.Visible = true;
        divstatus.Visible = true;
        if (ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString().ToUpper() == "APPROVED")
        {
            lblstatus.Text = "APPROVED";
            lblstatus.Style.Add("color", "GREEN");
        }
        else if (ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString().ToUpper() == "PENDING")
        {
          //  objCommon.DisplayMessage(this.updApplication, "Your " + ddlApplication.SelectedItem.Text + " Payment Done Successfully! Contact Examination Section With Chalan Reciept", this.Page);
            lblstatus.Text = "PENDING";
            lblstatus.Style.Add("color", "BLUE");
        }
        else
        {
            //objCommon.DisplayMessage(this.updApplication, "Your " + ddlApplication.SelectedItem.Text + " Payment Done Successfully!! But Your Application Rejected", this.Page);
            lblstatus.Text = "REJECTED";
            lblstatus.Style.Add("color", "RED");
        }
        if (Convert.ToInt32(ddlApplication.SelectedValue) == 1)
        {
            divnamecorrection.Visible = true;
            divaddress.Visible = false;
            lvnamecorrection.DataSource = ds;
            lvnamecorrection.DataBind();
            foreach (ListViewItem item in lvnamecorrection.Items)
            {
                TextBox txtcorrectname = item.FindControl("txtcorrectedname") as TextBox;
                TextBox txtcorrectedfatname = item.FindControl("txtcorrectedfatname") as TextBox;
                TextBox txtcorrectedmotname = item.FindControl("txtcorrectedmotname") as TextBox;
                txtcorrectname.Enabled = false;
                txtcorrectedfatname.Enabled = false;
                txtcorrectedmotname.Enabled = false;
            }
        }
        else if (Convert.ToInt32(ddlApplication.SelectedValue) == 3 || Convert.ToInt32(ddlApplication.SelectedValue) == 5)
        {
            txtmailadd.Text = dsstudname.Tables[0].Rows[0]["PADDRESS"] == null ? string.Empty : dsstudname.Tables[0].Rows[0]["PADDRESS"].ToString();
            txtmailadd.Enabled = false;
            divnamecorrection.Visible = false;
            divaddress.Visible = true;
            if (Convert.ToInt32(ddlApplication.SelectedValue) == 3)
            {
                divnoofcopies.Visible = true;
                 txtnoofcopies.Text = ds.Tables[0].Rows[0]["NOOFCOPIES"] == null ? string.Empty : ds.Tables[0].Rows[0]["NOOFCOPIES"].ToString();
                 txtnoofcopies.Enabled = false;
            }
            else if (Convert.ToInt32(ddlApplication.SelectedValue) == 5)
            {
                divnoofcopies.Visible = false;
            }
        }
        else
        {
            divnamecorrection.Visible = false;
        }           
    }
    protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpreviousreciepts();
        int AppId = Convert.ToInt32(ddlApplication.SelectedValue);               
            string application = objCommon.LookUp("ACD_APPLICATION_TRANS A INNER JOIN ACD_DCR B ON(A.DCR_NO=B.DCR_NO)", "COUNT(A.IDNO)", "A.APNO=" + Convert.ToInt32(ddlApplication.SelectedValue) + "AND A.IDNO=" + Convert.ToInt32(Session["IDNO"]) + "AND B.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND B.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip));
            if (Convert.ToInt32(application) > 0)
            {
                string chalanstatus = objCommon.LookUp("ACD_APPLICATION_TRANS A INNER JOIN ACD_DCR B ON(A.DCR_NO=B.DCR_NO)", "COUNT(A.IDNO)", "A.APNO=" + Convert.ToInt32(ddlApplication.SelectedValue) + "AND A.IDNO=" + Convert.ToInt32(Session["IDNO"]) + "AND B.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND B.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND B.RECON=0 AND B.CAN=1");
                GetStudentApplication();
                if (Convert.ToInt32(chalanstatus) > 0)
                {
                    objCommon.DisplayUserMessage(updApplication, "You Have Already Applied for " + ddlApplication.SelectedItem.Text + " !! But Challan Was Not Reconciled  Please Reconcile Your Challan", this.Page);
                    btnSubmit.Visible = false;                   
                }
                else
                {
                    objCommon.DisplayMessage(this.updApplication, "Your " + ddlApplication.SelectedItem.Text + " Payment Done Successfully! Please Print Application and Contact Examination Section!!", this.Page);
                }               
            }
       else
        {
            DataSet dsstudname = objCommon.FillDropDown("ACD_STUDENT A  LEFT OUTER JOIN ACD_STU_ADDRESS B ON(A.IDNO=B.IDNO)", "distinct A.IDNO, A.STUDNAME,A.FATHERNAME,A.MOTHERNAME", "B.PADDRESS", "A.IDNO=" + Convert.ToInt32(Session["idno"]), "A.IDNO");
            if (AppId == 1)
            {
                DataTable dt = new DataTable();
                dt = dsstudname.Tables[0];          
                if (!dt.Columns.Contains("CORR_STUDNAME"))
                {
                    dt.Columns.Add("CORR_STUDNAME", typeof(string));
                }

                if (!dt.Columns.Contains("CORR_FATHERNAME"))
                {
                    dt.Columns.Add("CORR_FATHERNAME", typeof(string));
                }
                if (!dt.Columns.Contains("CORR_MOTHERNAME"))
                {
                    dt.Columns.Add("CORR_MOTHERNAME", typeof(string));
                }
                divnamecorrection.Visible = true;
                lvnamecorrection.DataSource = dt;
                lvnamecorrection.DataBind();
                divaddress.Visible = false;

            }
            else if (AppId == 3 || AppId == 5)
            {
                txtmailadd.Text = dsstudname.Tables[0].Rows[0]["PADDRESS"] == null ? string.Empty : dsstudname.Tables[0].Rows[0]["PADDRESS"].ToString();
                divnamecorrection.Visible = false;
                divaddress.Visible = true;
                if (AppId == 3)
                {
                   // txtnoofcopies.Text = dsstudname.Tables[0].Rows[0]["NOOFCOPIES"] == null ? string.Empty : dsstudname.Tables[0].Rows[0]["NOOFCOPIES"].ToString();
                    divnoofcopies.Visible = true;
                }
                else if (AppId == 5)
                {
                    divnoofcopies.Visible = false;
                }                                                       
            }
            
            else
            {
                divnamecorrection.Visible = false;
                divaddress.Visible = false;
               
            }
            divReason.Visible = true;
            txtReason.Enabled = true;
            txtReason.Text = string.Empty;
            btnSubmit.Visible = true;
            divstatus.Visible = false;
        }
        }         
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try 
        {
            string COM_CODE = string.Empty;
            int AppId = Convert.ToInt32(ddlApplication.SelectedValue);
            string Reason = txtReason.Text.Trim().ToString();
            string mailaddress = string.Empty;
            int noofcopies = 0;
            if (AppId == 3 || AppId ==5)
            {
            mailaddress = txtmailadd.Text.Trim().ToString() == null ? string.Empty : txtmailadd.Text.Trim().ToString();
            if (AppId == 3)
            {
                string noof = string.Empty;
                noof = txtnoofcopies.Text.Trim().ToString();
                if (noof != string.Empty)
                {
                    noofcopies = Convert.ToInt32(noof);
                } 
            }
              
            }
             int AppStatus = 0;
             if (AppId == 1)
             {                 
                 COM_CODE = "NCF";
             }
             else if (AppId == 2)
             {               
                 COM_CODE = "DGCF";
             }
             else if (AppId == 3)
             {               
                 COM_CODE = "OTF";
             }
             else if (AppId == 4)
             {                
                 COM_CODE = "CGCF";
             }
             else if (AppId == 5)
             {               
                 COM_CODE = "PDF";
             }
             ViewState["COMCODE"] = COM_CODE;
             if (AppId == 1)
             {
                 foreach (ListViewItem item in lvnamecorrection.Items)
                 {
                     string txtcorrectedname = (item.FindControl("txtcorrectedname") as TextBox).Text;
                     string txtcorrectedfatname = (item.FindControl("txtcorrectedfatname") as TextBox).Text;
                     string txtcorrectedmotname = (item.FindControl("txtcorrectedmotname") as TextBox).Text;
                     CustomStatus cs = (CustomStatus)objSC.AddStudentApplication(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["currentsession"]), COM_CODE, AppId, Reason, AppStatus, Convert.ToString(txtcorrectedname), Convert.ToString(txtcorrectedfatname), Convert.ToString(txtcorrectedmotname), mailaddress, noofcopies);
                     if (cs.Equals(CustomStatus.RecordSaved))

                     {
                         objCommon.DisplayMessage(this.updApplication, "Your " + ddlApplication.SelectedItem.Text+ " Application is Recieved, Please Print The Challan!!", this.Page);
                        // radiolist.Visible = true;                      
                         BtnPrntChalan.Visible = true;
                         btnSubmit.Visible = false;
                         ddlApplication.Enabled = false;
                         txtReason.Enabled = false;                                     
                     }
                     else
                     {
                         objCommon.DisplayMessage(this.updApplication, " Error occured While Saving You Application Details Please Contact Admin!!", this.Page);
                     }
                 }
             }
             else
             {
              
                 CustomStatus cs = (CustomStatus)objSC.AddStudentApplication(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["currentsession"]), COM_CODE, AppId, Reason, AppStatus, "", "", "", mailaddress, noofcopies);
                 if (cs.Equals(CustomStatus.RecordSaved))
                 {
                     objCommon.DisplayMessage(this.updApplication, "Your " + ddlApplication.SelectedItem.Text + " Application is Recieved Successfully, Please Print The Challan!!", this.Page);
                     //radiolist.Visible = true;
                     BtnPrntChalan.Visible = true;
                     btnSubmit.Visible = false;
                     ddlApplication.Enabled = false;
                     txtReason.Enabled = false;                     
                 }
                 else
                 {
                     objCommon.DisplayMessage(this.updApplication, " Error occured While Saving You Application Details Please Contact Admin!!", this.Page);
                 }
             }                               
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void radiolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radiolist.SelectedValue == "1")
        {

            BtnOnlinePay.Visible = true;
            BtnPrntChalan.Visible = false;

        }
        else
        {

            BtnPrntChalan.Visible = true;
            BtnOnlinePay.Visible = false;

        }
    }
    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        lblOrderID.Text = Convert.ToString(Convert.ToInt32(ViewState["IDNO"]) + Convert.ToInt32(Session["currentsession"]) + Convert.ToString(lblSemester.ToolTip) + ir);
    }
    //  get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", 1, "EF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];             
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
    private void SubmitPaymentDetails()
     {
        int semester = 0;
        semester = Convert.ToInt32(lblSemester.ToolTip);
        ActivityController objActController = new ActivityController();
        int APNO = Convert.ToInt32(ddlApplication.SelectedValue);
        CreateCustomerRef();
        //   GetNewReceiptNo();
        string amount = objCommon.LookUp("ACD_APPLICATION_MASTER", "ISNULL(REPLACE(AMOUNT,'.00',''),0)", "APNO=" + APNO);
       
        int officaltrans = 0;
        string NameCorrectionFee = "0";
        string DuplicategradeCardFee = "0";
        string OfficailTranscriptFee = "0";
        string ConsolidatedgcswFee = "0";
        string ProvisionalDegreeFee = "0";
        string COM_CODE = string.Empty;
        if (APNO == 1)
        {
            NameCorrectionFee = amount;                                        
        }
        else if (APNO == 2)
        {
            DuplicategradeCardFee = amount;      
        }
        else if (APNO == 3)
        {
            officaltrans = Convert.ToInt32(txtnoofcopies.Text) * Convert.ToInt32(amount);
            OfficailTranscriptFee = officaltrans.ToString();
            amount = OfficailTranscriptFee;
            //officaltrans = Convert.ToInt32(txtnoofcopies.Text) * AMT;
            //OfficailTranscriptFee = officaltrans.ToString();
            //amount = OfficailTranscriptFee;
        }
        else if (APNO == 4)
        {
            ConsolidatedgcswFee = amount;        
        }
        else if (APNO == 5)
        {
            ProvisionalDegreeFee = amount;
        }
        else
        {
            objCommon.DisplayUserMessage(updApplication, "Failed To Continue Please Contact Admin!!", this.Page);
        }
        if (radiolist.SelectedValue != "")
        {
            if (radiolist.SelectedValue == "1")
            {
                int result = 0;

                result = feeController.Insert_Payment_StudentApplicationForm(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["currentsession"]), semester, Convert.ToString(lblOrderID.Text), 1, APNO, "0", "0", "0", "0", "0", "0", NameCorrectionFee, DuplicategradeCardFee, OfficailTranscriptFee, ConsolidatedgcswFee, ProvisionalDegreeFee, "0", amount, ViewState["COMCODE"].ToString());
               
                if (result > 0)
                {

                }
                else
                {
                    objCommon.DisplayUserMessage(updApplication, "Failed To Continue Please Contact Admin!!", this.Page);
                    return;
                }
            }
            else if (radiolist.SelectedValue == "2")
            {
                int result = 0;
                result = feeController.Insert_Payment_StudentApplicationForm(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["currentsession"]), semester, Convert.ToString(lblOrderID.Text), 2, APNO, "0", "0", "0", "0", "0", "0", NameCorrectionFee, DuplicategradeCardFee, OfficailTranscriptFee, ConsolidatedgcswFee, ProvisionalDegreeFee, "0", amount, ViewState["COMCODE"].ToString());


                if (result > 0)
                {
                }
                else
                {
                    objCommon.DisplayUserMessage(updApplication, "Failed To Continue Please Contact Admin!!", this.Page);
                    return;
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updApplication, "Please Select Payment Option!", this.Page);
            // rdbPayOption.Focus();
        }
    }
    protected void BtnPrntChalan_Click(object sender, EventArgs e)
    {
        SubmitPaymentDetails();
        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_APPLICATION_TRANS B ON(A.DCR_NO=B.DCR_NO)", "MAX(A.DCR_NO)", "A.IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND A.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + " AND  A.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND RECIEPT_CODE='EF' " + "AND B.APNO=" + ddlApplication.SelectedValue));
       
        ShowReport1("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1", 1);
        this.Clear();
    }
    // -----This report is for printing challan for Student Application Form------
    private void ShowReport1(string rptName, int dcrNo, int studentNo, string copyNo, int param)
    {
        try
        {            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=examReceipt_StudentLogin";
            url += "&path=~,Reports,Academic," + rptName;
            if (param == 1)
            {
                url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            }
            else
            {
                url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_PREV_STATUS=1,@P_SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip);
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updApplication, this.updApplication.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {

        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }
    //protected void btnreprintchalan_Click(object sender, EventArgs e)
    //{
    //    int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_APPLICATION_TRANS B ON(A.DCR_NO=B.DCR_NO)", "MAX(A.DCR_NO)", "A.IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND A.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + " AND  A.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND RECIEPT_CODE='EF'" + "AND B.APNO=" + ddlApplication.SelectedValue));
    //    ShowReport1("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1", 1);
    //}



    protected void BtnOnlinePay_Click(object sender, EventArgs e)
    {

    }

    #endregion studentlogin
    #region adminlogin
    private void BindListViewForAdmin()
    {
        int AppIdAdmin = Convert.ToInt32(ddlApplicationForAdmin.SelectedValue);
        try
        {
            if (ddlApplicationForAdmin.SelectedIndex > 0)
            {
                DataSet ds = objSC.GetStudentApplicationsForModfication(AppIdAdmin);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudentApplication.DataSource = ds;
                    lvStudentApplication.DataBind();

                    foreach (ListViewItem item in lvStudentApplication.Items)
                    {
                        Label lblactinestatus = item.FindControl("lblapprstatus") as Label;
                        if (lblactinestatus.Text.ToUpper() == "APPROVED")
                        {
                            lblactinestatus.Style.Add("color", "GREEN");
                        }
                        else if (lblactinestatus.Text.ToUpper() == "PENDING")
                        {
                            lblactinestatus.Style.Add("color", "BLUE");
                        }
                        else
                        {
                            lblactinestatus.Style.Add("color", "RED");
                        }
                    }
                }
                else
                {
                    lvStudentApplication.DataSource = null;
                    lvStudentApplication.DataBind();
                    objCommon.DisplayMessage(this.updApplication, " No Applications Found For :" + ddlApplicationForAdmin.SelectedItem.Text + " !!", this.Page);                  
                }
            }
            else
            {
                lvStudentApplication.DataSource = null;
                lvStudentApplication.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ElectionNominationForm.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlApplicationForAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListViewForAdmin();
        divstudentList.Visible = true;
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        
        int idno = 0;
        string regno = string.Empty;
        int apidno = 0;
        LinkButton btn = sender as LinkButton;
        int dcrno = Convert.ToInt32(btn.CommandArgument.ToString());

        string hdfapid = (btn.Parent.FindControl("hdnapid") as HiddenField).Value;
        string hdfidno = (btn.Parent.FindControl("hdnidno") as HiddenField).Value;
        string hdfregno = (btn.Parent.FindControl("hdnregno") as HiddenField).Value;
        idno = Convert.ToInt32(hdfidno);
        regno = Convert.ToString(hdfregno);
         apidno = Convert.ToInt32(hdfapid);
        ViewState["idno"] = idno;
        ViewState["REGNO"] = regno;
        ViewState["APPIDNO"] = apidno;
        this.ShowDetails();
        int APPIDADMIN = Convert.ToInt32(ddlApplicationForAdmin.SelectedValue);
        divadminsection.Visible = false;
        divstudentsection.Visible = true;
        divapprove.Visible = true;
        DataSet dsAdmin = objCommon.FillDropDown("ACD_APPLICATION_TRANS A INNER JOIN ACD_APPLICATION_MASTER B ON (A.APNO=B.APNO) INNER JOIN ACD_DCR C ON(A.DCR_NO=C.DCR_NO) INNER JOIN ACD_STUDENT D ON(A.IDNO=D.IDNO) LEFT OUTER JOIN ACD_STU_ADDRESS E ON(D.IDNO=E.IDNO) ", "B.APNAME,D.STUDNAME,D.FATHERNAME,D.MOTHERNAME,A.CORR_STUDNAME,A.CORR_FATHERNAME,A.CORR_MOTHERNAME,A.NOOFCOPIES,E.PADDRESS", "(CASE WHEN A.APPROVE_STATUS=0 THEN 'PENDING'  WHEN A.APPROVE_STATUS=1 THEN 'APPROVED' WHEN A.APPROVE_STATUS=2 THEN 'REJECTED' ELSE 'NA' END) AS APPROVE_STATUS,A.REASON,A.REMARK", "A.APNO=" + Convert.ToInt32(ddlApplicationForAdmin.SelectedValue) + "AND A.IDNO=" + Convert.ToInt32(idno) + " AND C.DCR_NO=" + dcrno, "A.APP_TRANS_ID DESC");
        ddlApplication.SelectedItem.Text = dsAdmin.Tables[0].Rows[0]["APNAME"] == null ? "0" : dsAdmin.Tables[0].Rows[0]["APNAME"].ToString();
        txtReason.Text = dsAdmin.Tables[0].Rows[0]["REASON"] == null ? string.Empty : dsAdmin.Tables[0].Rows[0]["REASON"].ToString();
        txtRemark.Text = dsAdmin.Tables[0].Rows[0]["REMARK"] == null ? string.Empty : dsAdmin.Tables[0].Rows[0]["REMARK"].ToString();
        txtmailadd.Text = dsAdmin.Tables[0].Rows[0]["PADDRESS"] == null ? string.Empty : dsAdmin.Tables[0].Rows[0]["PADDRESS"].ToString();
        if (APPIDADMIN == 1)
        {
            divnamecorrection.Visible = true;
            lvnamecorrection.DataSource = dsAdmin;
            lvnamecorrection.DataBind();
          

        }
        else if (APPIDADMIN == 3)
        {
            txtnoofcopies.Text = dsAdmin.Tables[0].Rows[0]["NOOFCOPIES"] == null ? string.Empty : dsAdmin.Tables[0].Rows[0]["NOOFCOPIES"].ToString();
            txtnoofcopies.Enabled = false;
            txtmailadd.Enabled = false;
            divnamecorrection.Visible = false;
            divnoofcopies.Visible = true;
            divaddress.Visible = true;


        }
        else if (APPIDADMIN == 5)
        {
            divnamecorrection.Visible = false;
            divaddress.Visible = true;
            divnoofcopies.Visible = false;
            txtmailadd.Enabled = false;
        }
        else
        {
            divnamecorrection.Visible = false;
        }


        divstatus.Visible = true;
        if (dsAdmin.Tables[0].Rows[0]["APPROVE_STATUS"].ToString().ToUpper() == "APPROVED")
        {
            lblstatus.Text = "APPROVED";
            lblstatus.Style.Add("color", "GREEN");
        }
        else if (dsAdmin.Tables[0].Rows[0]["APPROVE_STATUS"].ToString().ToUpper() == "PENDING")
        {
            lblstatus.Text = "PENDING";
            lblstatus.Style.Add("color", "BLUE");
        }
        else
        {
            lblstatus.Text = "REJECTED";
            lblstatus.Style.Add("color", "RED");
        }
        divReason.Visible = true;
        divremark.Visible = true;
        ddlApplication.Enabled = false;
        txtReason.Enabled = false;
        
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {

        string Remark = txtRemark.Text.Trim().ToString();
        int approve_status = 1;
        string chalanstatus = objCommon.LookUp("ACD_APPLICATION_TRANS A INNER JOIN ACD_DCR B ON(A.DCR_NO=B.DCR_NO)", "COUNT(1)", "A.APNO=" + Convert.ToInt32(ddlApplicationForAdmin.SelectedValue) + "AND A.IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND B.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND B.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND B.RECON=0 AND B.CAN = 1 ");
        if (Convert.ToInt32(chalanstatus) > 0)
        {
            objCommon.DisplayMessage(updApplication, " You Cannot Approve this Application because This Student Chalan Was Not Reconciled", this.Page);
        }
        else
        {
        CustomStatus cs = (CustomStatus)objSC.StudentApplicationFormApprove(Convert.ToInt32(ViewState["APPIDNO"]), Convert.ToInt32(ViewState["idno"]), approve_status,Remark, Convert.ToInt32(Session["userno"]));
        if (cs.Equals(CustomStatus.RecordSaved))
        {

            User_AccController objUC = new User_AccController();
            objCommon.DisplayMessage(this.updApplication, " Student Application Approved Successfully", this.Page);
            divstudentsection.Visible = false;
            divadminsection.Visible = true;
            this.BindListViewForAdmin();


            string useremail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + ViewState["REGNO"].ToString() + "' and UA_NAME IS NOT NULL");
            string usermobile = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_NAME='" + ViewState["REGNO"].ToString() + "' and UA_NAME IS NOT NULL");
            if (useremail == null && useremail.ToString() == "")
            {
             
                objCommon.DisplayMessage(updApplication,"Sorry , Student email id not registered in system !!", this.Page);
                return;

            }
           
           
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            if (dsconfig != null)
            {
                string emaili1 = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string pwd1 = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();


                mail.From = new MailAddress(emaili1);
                string MailFrom = emaili1;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emaili1, pwd1);
                SmtpServer.EnableSsl = true;
                string aa = string.Empty;
                mail.Subject = "Your Application Status:";
                mail.To.Clear();
                mail.To.Add(useremail);
                mail.IsBodyHtml = true;
                mail.Body = "Your " + ddlApplicationForAdmin.SelectedItem.Text + " Application Approved Succesfully";
                SmtpServer.Send(mail);
                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                {
                    if (usermobile != null)
                    {
                        if (usermobile.ToString() != "")
                        {
                            StudentAttendanceController dsStudentDetails = new StudentAttendanceController();
                            dsStudentDetails.SENDMSG("Your " + ddlApplicationForAdmin.SelectedItem.Text + " Application Approved Succesfully" + "", usermobile);
                        }
                       

                    }

                  
                }
                else
                {
                    objCommon.DisplayMessage(this.updApplication,"Student Email is not send successfully", this.Page);
                }


            }
        }

        else
        {
            objCommon.DisplayMessage(this.updApplication, " Error occured While Approving Student Application Form ", this.Page);
        }
        }
    }
    protected void btnreject_Click(object sender, EventArgs e)
    {

        string Remark = txtRemark.Text.Trim().ToString();
        int approve_status = 2;
        string chalanstatus = objCommon.LookUp("ACD_APPLICATION_TRANS A INNER JOIN ACD_DCR B ON(A.DCR_NO=B.DCR_NO)", "COUNT(1)", "A.APNO=" + Convert.ToInt32(ddlApplicationForAdmin.SelectedValue) + "AND A.IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND B.SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND B.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND B.RECON=0 AND B.CAN = 1");
        if (Convert.ToInt32(chalanstatus) > 0)
        {
            objCommon.DisplayMessage(updApplication, " You Cannot Reject this Application because This Student Chalan Was Not Reconciled", this.Page);
        }
        else
        {


            CustomStatus cs = (CustomStatus)objSC.StudentApplicationFormApprove(Convert.ToInt32(ViewState["APPIDNO"]), Convert.ToInt32(ViewState["idno"]), approve_status, Remark, Convert.ToInt32(Session["userno"]));
        if (cs.Equals(CustomStatus.RecordSaved))
        {

            objCommon.DisplayMessage(this.updApplication, " Student Application Rejected Successfully", this.Page);
            divstudentsection.Visible = false;
            divadminsection.Visible = true;
            this.BindListViewForAdmin();

        }
        else
        {
            objCommon.DisplayMessage(this.updApplication, " Error occured While Rejecting Student Application Form ", this.Page);
        }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divstudentsection.Visible = false;
        divadminsection.Visible = true;
    }
    #endregion adminlogin

    private void Clear()
    {
        divnamecorrection.Visible = false;
        btnSubmit.Visible = true;
        btnRemoveList.Visible = true;
        BtnPrntChalan.Visible = false;
        divReason.Visible = false;
        ddlApplication.SelectedValue = "0";
        ddlApplication.Enabled = true;
        divstatus.Visible = false;
        getpreviousreciepts();
        divaddress.Visible = false;
        divnoofcopies.Visible = false;
    }
    protected void btnRemoveList_Click(object sender, EventArgs e)
    {

        this.Clear();
       
    }
    private void GetCorrectedName()
    {
        divnamecorrection.Visible = true;
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = Convert.ToInt32(ViewState["idno"]);         
        }
        DataSet dsstudname = objCommon.FillDropDown("ACD_STUDENT A LEFT OUTER JOIN ACD_APPLICATION_TRANS B ON(A.IDNO=B.IDNO)", "A.STUDNAME,A.FATHERNAME,A.MOTHERNAME", "B.CORR_STUDNAME,B.CORR_FATHERNAME,B.CORR_MOTHERNAME", "A.IDNO=" + idno, "A.IDNO");
        lvnamecorrection.DataSource = dsstudname;
        lvnamecorrection.DataBind();
        
    }
    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnprint = sender as ImageButton;
        int DCR_NO = Convert.ToInt32(btnprint.CommandArgument.ToString());
        ShowReport1("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1", 1);  
    }
    protected void btnprintapplication_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnprintApp = sender as ImageButton;
        int APPTRANSID = Convert.ToInt32(btnprintApp.CommandArgument.ToString());
        int apno=0;
        int dcrno=0;
         string hdfapno = (btnprintApp.Parent.FindControl("hdnapno") as HiddenField).Value;
         string hdfdcrno = (btnprintApp.Parent.FindControl("hdnstudcrno") as HiddenField).Value;
        apno=Convert.ToInt32(hdfapno);
        dcrno = Convert.ToInt32(hdfdcrno);
        // Name Correction in the Grade Card
        if (apno == 1)
        {
            ShowAppReport("NameCorrectionGradeCard", "rptnamecorrgradecard.rpt", Convert.ToInt32(Session["idno"]), APPTRANSID, dcrno);
        }
            //Issue of Duplicate Grade Card
        else if (apno == 2)
        { 
            ShowAppReport("DuplicateGradeCard", "rptduplicategradecard.rpt", Convert.ToInt32(Session["idno"]), APPTRANSID, dcrno);
        
        }
            //Issue of official Transcript
        else if (apno == 3)
        {
            ShowAppReport("OfficialTranscript", "rptofficialtranscript.rpt", Convert.ToInt32(Session["idno"]), APPTRANSID, dcrno);
            
        }
            //Issue of Consolidated Grade Card Semester Wise
        else if (apno == 4)
        {
            ShowAppReport("ConsolidatedGradeCardSemWiseReport", "rptconsgradecardsemwise.rpt", Convert.ToInt32(Session["idno"]), APPTRANSID, dcrno);
            
        }
            //Provisional Degree Certificate
        else if (apno == 5)
        {
            ShowAppReport("ConsolidatedGradeCardSemWiseReport", "rptprovisionaldegreecertificate.rpt", Convert.ToInt32(Session["idno"]), APPTRANSID, dcrno);
            
        }
    }
    private void ShowAppReport(string reportTitle, string rptFileName, int idno, int APPTRANSID, int dcrno)
    {     
    try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;          
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_APP_TRANS_ID=" + APPTRANSID + ",@P_DCR_NO=" + dcrno;
          
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updApplication, this.updApplication.GetType(), "controlJSScript", sb.ToString(), true);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
           }
}