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
using System.Data.SqlClient;
using System.Data;
using DotNetIntegrationKit;

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Text;
using System.Net;

public partial class ACADEMIC_OnlinePayment_StudentLogin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    int idno = 0; decimal TotalSum = 0; String tspl_txn_id = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        ShowStudentDetails();
                        GetPreviousReceipt();
                        objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0 AND RCPTTYPENO IN(1,2)", "RECIEPT_CODE");//RCPTTYPENO
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for only Student Login!!", this.Page);
                    }
                }
                TRAmount.Visible = false;
            } divMsg.InnerHtml = string.Empty;
            btnSubmit.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                idno = Convert.ToInt32(Session["idno"]);
                string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
                DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    DataRow dr = dsStudent.Tables[0].Rows[0];
                    //lblStudName.Text = dr["STUDNAME"].ToString();

                    string fullName = dr["STUDNAME"].ToString();
                    string[] names = fullName.Split(' ');
                    string name = names.First();
                    string lasName = names.Last();
                    lblStudName.Text = fullName;
                    lblStudLastName.Text = lasName;

                    lblSex.Text = dr["SEX"].ToString();
                    //lblRegNo.Text = dr["REGNO"].ToString();
                    // lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
                    lblRegNo.Text = dr["REGNO"].ToString();
                    lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
                    lblDegree.Text = dr["DEGREENAME"].ToString();
                    lblBranchs.Text = dr["BRANCH_NAME"].ToString();
                    lblYear.Text = dr["YEARNAME"].ToString();
                    lblSemester.Text = dr["SEMESTERNAME"].ToString();
                    lblBatch.Text = dr["BATCHNAME"].ToString();
                    lblMobileNo.Text = dr["STUDENTMOBILE"].ToString();
                    lblEmailID.Text = dr["EMAILID"].ToString();
                    ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                    ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                    ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString();
                    ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString();
                    lblCollege.Text = dr["COLLEGENAME"].ToString();
                    hdnCollege.Value = dr["COLLEGE_CODE"].ToString();
                    ViewState["COLLEGE_ID"] = hdnCollege.Value;
                }
                else
                {
                    objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for Student Login!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        // lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToInt32(ViewState["SESSIONNO"]) + Convert.ToInt32(ViewState["semesterno"]) + ir);
        lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ddlSemester.SelectedValue) + ir);
    }

    private void Clear()
    {
        //ViewState["Amount"] = null;
        //TotalSum = 0;
        btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; 
        //TRSPayOption.Visible = false; 
        lblStatus.Visible = false;
        ddlReceiptType.SelectedIndex = 0; 
        //rdbPayOption.SelectedIndex = -1; 
        btnReport.Visible = false; TRNote.Visible = false;
        lvStudentFees.DataSource = null;
        lvStudentFees.DataBind();

    }

    private void CheckPageAuthorization()
    {
        // Request.QueryString["pageno"] = "ACADEMIC/EXAMINATION/ExamRegistration_New.aspx ?pageno=1801";
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
        }
    }

    private void GetSessionValues()
    {
        Session["FirstName"] = lblStudName.Text;
        Session["LastName"] = lblStudLastName.Text;
        Session["RegNo"] = lblRegNo.Text;
        Session["MobileNo"] = lblMobileNo.Text;
        Session["EMAILID"] = lblEmailID.Text;
        Session["OrderID"] = lblOrderID.Text;
    }

    private void SubmitData()
    {
        try
        {
            CreateCustomerRef();
            GetSessionValues();

            if (rdbPayOption.SelectedValue != "")
            {
                if (rdbPayOption.SelectedValue == "1") // Online payment 
                {
                    int result = 0;
                    int DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIEPT_CODE='TF'"));
                    if (DM_NO > 0)
                    {
                        if (rdbPayServiceType.SelectedValue == "1") // Atom Gateway
                        {
                            //result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(lblOrderID.Text), Convert.ToInt32(rdbPayOption.SelectedValue), Convert.ToInt32(rdbPayServiceType.SelectedValue));

                            //if (result > 0)
                            //{
                            //    SendRequest(Convert.ToInt32(1), Convert.ToDouble(ViewState["Amount"]));
                            //}
                            //else
                            //{
                            //    objCommon.DisplayMessage(updBulkReg, "Failed To Done Online Payment.", this.Page);
                            //    return;
                            //}

                        }
                        else if (rdbPayServiceType.SelectedValue == "2") // PAYTM Gateway
                        {
                            //result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(lblOrderID.Text), Convert.ToInt32(rdbPayOption.SelectedValue), Convert.ToInt32(rdbPayServiceType.SelectedValue));
                            //if (result > 0)
                            //{

                            //    this.SendToPayTm(lblOrderID.Text);
                            //}
                            //else
                            //{
                            //    objCommon.DisplayMessage(updBulkReg, "Failed To Done Online Payment.", this.Page);
                            //    return;
                            //}
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updBulkReg, "Demand not created.", this.Page);
                    }
                }
                else if (rdbPayOption.SelectedValue == "2") // By Challan
                {
                    int result = 0;
                    int DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND RECIEPT_CODE='TF'"));
                    //result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(lblOrderID.Text), Convert.ToInt32(rdbPayOption.SelectedValue), 0);//, Convert.ToString(ViewState["Amount"])//, APTRANSACTIONID


                    //if (result > 0)
                    //{
                    //    btnReport.Enabled = true;
                    //}
                    //else
                    //{
                    //    lblStatus.Visible = true;
                    //    objCommon.DisplayUserMessage(updBulkReg, "Failed To Done Payment.", this.Page);
                    //    return;
                    //}
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updBulkReg, "Please Select Payment Option!", this.Page);
                rdbPayOption.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  
    private void GetPreviousReceipt()
    {
        DataSet ds = feeController.GetPaidReceiptsInfoByStudId_FORPAYMENT(Convert.ToInt32(Session["idno"]));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvPaidReceipts.DataSource = ds;
            lvPaidReceipts.DataBind();
        }
        else
        {
            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
        }
    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReceiptType.SelectedIndex > 0)
        {
            lblStatus.Visible = false;
            //rdbPayOption.SelectedIndex = -1;
            ddlSemester.SelectedValue = "0";
            if (ddlSemester.SelectedIndex > 0)
            {

            }
            else
            {
                btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; 
                //TRSPayOption.Visible = false; 
                TRNote.Visible = false;
                //objCommon.DisplayUserMessage(updBulkReg, "Please Select Semester.", this.Page);
                ddlSemester.Focus();
                lblStatus.Visible = false;
            }
        }

        else
        {
            btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; 
            //TRSPayOption.Visible = false; 
            TRNote.Visible = false;
            objCommon.DisplayUserMessage(updBulkReg, "Please Select Receipt Type.", this.Page);
            ddlReceiptType.Focus();
            lblStatus.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvStudentFees_PreRender(object sender, EventArgs e)
    {
        Label lbltotal = this.lvStudentFees.FindControl("lbltotal") as Label;
        ViewState["Amount"] = TotalSum.ToString();
        lbltotal.Text = TotalSum.ToString();
    }

    protected void lvStudentFees_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            TotalSum += Convert.ToDecimal(lblAmount.Text);
        }
    }

 
    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            btnReport.Visible = false;
           // ddlSemester_SelectedIndexChanged(new object(), new EventArgs());
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=FeeCollectionReceipt_StudentLogin";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + rptName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        /// 

        //string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        //return param;


        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        
        SubmitData();//Session["currentsession"]

        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND RECIEPT_CODE='TF'"));

        if (ViewState["COLLEGE_ID"] == "11" || ViewState["COLLEGE_ID"] == "12" || ViewState["COLLEGE_ID"] == "13")
        {
            this.ShowReport("FeeCollectionReceipt-SVIM.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
        else if (ViewState["COLLEGE_ID"] == "10")
        {
            this.ShowReport("FeeCollectionReceipt-SVITS.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
        else
        {
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
    }

    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;

        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());

        string recipt_code = Convert.ToString(objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "DCR_NO = " + DCR_NO + ""));
        if (recipt_code == "TF")
        {
            if (ViewState["COLLEGE_ID"] == "11" || ViewState["COLLEGE_ID"] == "12" || ViewState["COLLEGE_ID"] == "13")
            {
                this.ShowReport("FeeCollectionReceipt-SVIM.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
            }
            else if (ViewState["COLLEGE_ID"] == "10")
            {
                this.ShowReport("FeeCollectionReceipt-SVITS.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
            }
            else
            {
                this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
            }
        }
    }

    protected void lvPaidReceipts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            switch (e.Item.ItemType.ToString())
            {

            };
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string count = objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND  SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND RECIEPT_CODE='TF'");

            if (Convert.ToInt32(count)>0)
            {
                objCommon.DisplayUserMessage(updBulkReg, "You Have Already Generated Challan For This Amount. Reprint From Following List.", this.Page);
                return;
            }
            int status = 0;
            if (ddlReceiptType.SelectedIndex > 0)
            {
                if (ddlSemester.SelectedIndex > 0)
                {
                    string session = objCommon.LookUp("ACD_DEMAND", "isnull(SESSIONNO,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "AND RECIEPT_CODE='" + Convert.ToString(ddlReceiptType.SelectedValue) +"'");
                    if (session == "0" || session=="")
                    {
                        objCommon.DisplayUserMessage(updBulkReg, "Demand not defined for " + ddlSemester.SelectedValue +"th semester. Please contact to exam section.", this.Page);
                        return;
                    }
                    DataSet ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
                    if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //btnSubmit.Visible = true;
                        //btnSubmit.Enabled = true;
                        btnReport.Visible = true;
                        btnReport.Enabled = true;
                        btnCancel.Visible = true; pnlStudentsFees.Visible = true;
                        TRSPayOption.Visible = false;
                        TRNote.Visible = true;
                        
                        lvStudentFees.DataSource = ds;
                        lvStudentFees.DataBind();
                       
                    }
                    else
                    {
                        btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; TRNote.Visible = false; TRSPayOption.Visible = false;
                        btnReport.Visible = false; TRSGatewayType.Visible = false;
                        objCommon.DisplayUserMessage(updBulkReg, "Demand Not Created.", this.Page);
                        btnSubmit.Enabled = false;
                        lvStudentFees.DataSource = null;
                        lvStudentFees.DataBind();
                        lblStatus.Visible = false;
                    }
                   
                }
                else
                {
                    btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; 
                     
                    TRNote.Visible = false;
                    objCommon.DisplayUserMessage(updBulkReg, "Please Select Semester.", this.Page);
                    ddlReceiptType.Focus();
                    lblStatus.Visible = false;
                }

            }
            else
            {
                btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; 
                 
                TRNote.Visible = false;
                objCommon.DisplayUserMessage(updBulkReg, "Please Select Receipt Type.", this.Page);
                ddlReceiptType.Focus();
                lblStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ddlReceiptType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public static string encryptFile(string textToencrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.ECB;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length; 
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToencrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText,0,plainText.Length));
    }

    private void SendTransaction()
    {
        //string key = "1234567891234567";
        //string key = "1300642210105005";
        string merchant_id = "";
        string submerchant_id = ""; string submerchant_id1 = "";
        DataSet ds = null;
        ds = feeController.Get_COLLEGE_PAYMENTDATA(Convert.ToInt32(hdnCollege.Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            merchant_id = ds.Tables[0].Rows[0]["ICID"].ToString();
        }
        else
        {
            objCommon.DisplayUserMessage(updBulkReg, "Error occurred while fetching college details!", this.Page);
            return;
        }
        string txnrefno = string.Empty; string txnrefno1 = string.Empty;
        string amt = string.Empty;
        string amt1 = string.Empty;
        if ((ViewState["Amount"] != null || ViewState["Amount"] != "") && (Session["idno"] != null || Session["idno"] != ""))
        {
            amt1 = encryptFile(ViewState["Amount"].ToString(), ds.Tables[0].Rows[0]["AESKey"].ToString());//******************UNCOMMENT THIS LINE FOR LIVE SERVER*******************************
            //amt1 = encryptFile("1", ds.Tables[0].Rows[0]["AESKey"].ToString());      
            amt = ViewState["Amount"].ToString();      //******************FOR TESTING ONLY*******************************
            submerchant_id1 = encryptFile(Session["idno"].ToString().Trim(), ds.Tables[0].Rows[0]["AESKey"].ToString());
            submerchant_id = Session["idno"].ToString().Trim();
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
        string mandatory_fields="";
        ////string mandatory_fields1 = "";//****************

        string paymode = encryptFile("9", ds.Tables[0].Rows[0]["AESKey"].ToString());
        //string return_url = encryptFile("HTTP://LOCALHOST:27007/PRESENTATIONLAYER/RESPONSE.ASPX", key);
        string return_url = encryptFile("https://indusuni.mastersofterp.in/response.aspx", ds.Tables[0].Rows[0]["AESKey"].ToString());
        ////string return_url = encryptFile("http://localhost:27007/presentationlayer/response.aspx", ds.Tables[0].Rows[0]["AESKey"].ToString());

        ////string return_url1 = "https://indusuni.mastersofterp.in/response.aspx";//************
       
        txnrefno1 = encryptFile(lblOrderID.Text, ds.Tables[0].Rows[0]["AESKey"].ToString());//**************
        txnrefno = lblOrderID.Text;//**************
        mandatory_fields = encryptFile((txnrefno + "|" + submerchant_id + "|" + amt + "|" + lblRegNo.Text.Trim()), ds.Tables[0].Rows[0]["AESKey"].ToString());

        ////mandatory_fields1 = txnrefno + "|" + submerchant_id + "|" + amt + "|" + lblRegNo.Text.Trim();
         
        string url = string.Empty;
        ////string plain_url = string.Empty;
        ////plain_url = "https://eazypay.icicibank.com/EazyPG?merchantid=" + merchant_id + ""
        ////              + "&mandatory fields=" + mandatory_fields1
        ////              + "&optional fields="
        ////              + "&returnurl=" + return_url1
        ////              + "&Reference No=" + txnrefno  //*********
        ////              + "&submerchantid=" + submerchant_id
        ////              + "&transaction amount=" + amt
        ////              + "&paymode=9";

        if (txnrefno != string.Empty && submerchant_id != string.Empty && amt != string.Empty && lblRegNo.Text != string.Empty && paymode != string.Empty)
        {
        ////https://eazypay.icicibank.com/EazyPG?merchantid=131016&mandatory fields=12|34|1==&optional fields=&returnurl=HTTPS://INDUSUNI.MASTERSOFTERP.IN/RESPONSE.ASPX&Reference No=12&submerchantid=34&transaction amount=1==&paymode=9

            url = "https://eazypay.icicibank.com/EazyPG?merchantid=" + merchant_id + ""
                      + "&mandatory fields=" + mandatory_fields
                      + "&optional fields="
                      + "&returnurl=" + return_url
                      + "&Reference No=" + txnrefno1  //*********
                      + "&submerchantid=" + submerchant_id1
                      + "&transaction amount=" + amt1
                      + "&paymode=" + paymode;
            
            Response.Redirect(url, false);
        }
        else
        {
            objCommon.DisplayUserMessage(updBulkReg, "Can not proceed due to insufficient data!", this.Page);
            return;
        }   
    }

    //added by akhilesh on [2018-02-20]
    protected void rdbPayOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        int output = 0;
        if (rdbPayOption.SelectedValue == "1")
        {
            if (Convert.ToDecimal(ViewState["Amount"]) <= 0)
            {
                objCommon.DisplayUserMessage(updBulkReg, "Amount To Be Pay, Cannot Be Zero Or Less Than Zero.", this.Page);
               
                btnReport.Visible = false;
                btnSubmit.Visible = false; lblStatus.Visible = false;
                TRSGatewayType.Visible = false;

            }
            else
            {
                //TRSCardType.Visible = false;
                btnReport.Visible = false;
                btnSubmit.Visible = true; lblStatus.Visible = false;
                TRSGatewayType.Visible = true;

                output = feeController.GetFeeItems_Data_ISEXISTS(Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), "O", Convert.ToInt32(ViewState["batchno"]));//, ref status);
                if (output == 1)
                {
                    //TRSCardType.Visible = false;
                    btnReport.Visible = false;
                    btnSubmit.Visible = false;
                    lblStatus.Visible = false;
                    TRSGatewayType.Visible = false;
                }
                else
                {
                    //TRSCardType.Visible = false;
                    btnReport.Visible = false;
                    btnSubmit.Visible = true;
                    TRSGatewayType.Visible = true;
                    lblStatus.Visible = false;
                    TRSGatewayType.Visible = true;
                }

            }
            ddlSemester_SelectedIndexChanged(new object(), new EventArgs());


        }
        else if (rdbPayOption.SelectedValue == "2")
        {
            if (Convert.ToDecimal(ViewState["Amount"]) <= 0)
            {
                objCommon.DisplayUserMessage(updBulkReg, "Amount To Be Pay, Cannot Be Zero Or Less Than Zero.", this.Page);
                btnReport.Visible = false;
                //TRSCardType.Visible = false;
                btnSubmit.Visible = false;
                btnReport.Enabled = true;
                lblStatus.Visible = false;
            }
            else
            {
                lblStatus.Visible = false;
                btnReport.Visible = true;
                //TRSCardType.Visible = false;
                btnSubmit.Visible = false;
                btnReport.Enabled = true;

                output = feeController.GetFeeItems_Data_ISEXISTS(Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), "OF", Convert.ToInt32(ViewState["batchno"]));//, ref status);
                if (output == 2)
                {
                    btnReport.Visible = false;
                    //TRSCardType.Visible = false;
                    btnSubmit.Visible = false;
                    btnReport.Enabled = true;

                    lblStatus.Visible = true;
                    lblStatus.Text = "You Have Already Generated Challan For This Amount. Reprint From Following List";
                    lblStatus.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    lblStatus.Visible = false;
                    btnReport.Visible = true;
                    //TRSCardType.Visible = false;
                    btnSubmit.Visible = false;
                    btnReport.Enabled = true;
                    TRSGatewayType.Visible = false;
                }
            }
            ddlSemester_SelectedIndexChanged(new object(), new EventArgs());
            btnSubmit.Visible = false;
        }
        else
        {
        }
    }

    //methods for ATOM Payment Services dated on[2017-04-28]
    public void SendRequest(int feeid, double amount)
    {
        string loginId = string.Empty;
        string pwd = string.Empty;
        string transactionType = string.Empty;
        string ProductId = string.Empty;
        string Currency = string.Empty;
        string txnsCamt = string.Empty;
        string ClientCode = string.Empty;
        string txnid = string.Empty;
        string CusAcc = string.Empty;
        string ReturnUrl = string.Empty;
        double Amount = 0.00;
        string udf1 = string.Empty;
        string udf2 = string.Empty;
        string udf3 = string.Empty;
        string udf9 = string.Empty;
        loginId = "29077";
        pwd = "SHRI@123";
        transactionType = "NBFundTransfer";
        ProductId = "VAISHNAV";
        Currency = "INR";
        txnsCamt = "0";// Convert.ToString(lblTotalFee.Text)
        //ClientCode = "29";
        byte[] byt = System.Text.Encoding.UTF8.GetBytes(lblRegNo.Text);
        ClientCode = Convert.ToBase64String(byt);
        txnid = Convert.ToString(lblOrderID.Text);
        CusAcc = "1234567890";
        ReturnUrl = "https://svvverp.mastersofterp.in/Atom_Response.aspx";
        //ReturnUrl = "http://localhost:50629/PresentationLayer/Atom_Response.aspx";
        Amount = Convert.ToDouble(ViewState["Amount"].ToString());
        udf9 = lblRegNo.Text;
        udf1 = lblStudName.Text + " " + lblStudLastName.Text;
        udf2 = lblEmailID.Text;
        udf3 = lblMobileNo.Text;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://payment.atomtech.in/paynetz/epi/fts");

        request.Method = "POST";

        //------localhost:2999/Atom_Request.aspx
        request.ContentType = "application/x-www-form-urlencoded";
        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; CK={CVxk71YSfgiE6+6P6ftT7lWzblrdvMbRqavYf/6OcMIH8wfE6iK7TNkcwFAsxeChX7qRAlQhvPWso3KI6Jthvnvls9scl+OnAEhsgv+tuvs=}; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        string postData = "login=" + loginId + "&pass=" + pwd + "&ttype=" + transactionType + "&prodid=" + ProductId + "&amt=" + Amount.ToString("0.00") + "&txncurr=" + Currency + "&txnscamt=" + txnsCamt + "&clientcode=" + ClientCode + "&txnid=" + txnid + "&date=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "&custacc=" + CusAcc + "&ru=" + ReturnUrl + "&udf1=" + udf1 + "&udf2=" + udf2 + "&udf3=" + udf3 + "&udf9=" + udf9 + "";

        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        request.ContentType = "application/x-www-form-urlencoded";

        request.ContentLength = byteArray.Length;
        request.AllowAutoRedirect = true;

        request.Proxy.Credentials = CredentialCache.DefaultCredentials;

        Stream dataStream = request.GetRequestStream();

        dataStream.Write(byteArray, 0, byteArray.Length);

        dataStream.Close();

        WebResponse response = request.GetResponse();

        XmlDocument objXML = new XmlDocument();

        dataStream = response.GetResponseStream();

        objXML.Load(dataStream);

        string TxnId = objXML.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[2].InnerText;

        string Token = objXML.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[3].InnerText;
        string txnData = "ttype=NBFundTransfer&txnStage=1&tempTxnId=" + TxnId + "&token=" + Token;

        dataStream.Close();
        response.Close();
        Response.Redirect("https://payment.atomtech.in/paynetz/epi/fts?" + txnData);
    }

    //methods for PAYTM Payment Services dated on[2017-06-28]
    public void SendToPayTm(String orderid)
    {
        //double fees = Convert.ToDouble(ViewState["Amount"].ToString());
        //Random random = new Random();

        ////String merchantKey = "M!2KX8VkntfEa0aq";TEST
        //String merchantKey = "APTkxDEhoztkfML3";
        //Dictionary<string, string> parameters = new Dictionary<string, string>();
        ////parameters.Add("MID", "ShriVa05680355152351");TEST
        //parameters.Add("MID", "ShriVa60032798403946");
        //parameters.Add("CHANNEL_ID", "WEB");
        ////parameters.Add("INDUSTRY_TYPE_ID", "Retail");TEST
        //parameters.Add("INDUSTRY_TYPE_ID", "PrivateEducation");
        ////parameters.Add("WEBSITE", "WEB_STAGING");TEST
        //parameters.Add("WEBSITE", "ShriVaiWEB");
        //parameters.Add("EMAIL", lblEmailID.Text);
        //parameters.Add("MOBILE_NO", lblMobileNo.Text);
        ////parameters.Add("CUST_ID", (Session["idno"].ToString()));
        //parameters.Add("CUST_ID", lblRegNo.Text);
        ////byte[] byt = System.Text.Encoding.UTF8.GetBytes(lblRegNo.Text);
        ////byte[] byt = System.Text.Encoding.UTF8.GetBytes(lblStudName.Text);

        ////parameters.Add("CUST_ID", (Convert.ToBase64String(byt)));

        //parameters.Add("ORDER_ID", orderid.ToString());
        ////  parameters.Add("TXN_AMOUNT", "1.00"); //lblAmount.Text
        //parameters.Add("TXN_AMOUNT", ViewState["Amount"].ToString());
        //parameters.Add("THEME", "merchant3");
        ////parameters.Add("CALLBACK_URL", "http://localhost:50629/PresentationLayer/PaytmResponse.aspx");
        //parameters.Add("CALLBACK_URL", "https://svvverp.mastersofterp.in//PaytmResponse.aspx");
        ////string paytmURL = "https://pguat.paytm.com/oltp-web/processTransaction?orderid=" + orderid;
        //string paytmURL = "https://secure.paytm.in/oltp-web/processTransaction?orderid=" + orderid;

        //string checksum = CheckSum.generateCheckSum(merchantKey, parameters);

        //string outputHTML = "<html>";
        //outputHTML += "<head>";
        //outputHTML += "<title>Merchant Check Out Page</title>";
        //outputHTML += "</head>";
        //outputHTML += "<body>";
        //outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
        //outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
        //outputHTML += "<table border='1'>";
        //outputHTML += "<tbody>";
        //foreach (string key in parameters.Keys)
        //{
        //    outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
        //}
        //outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
        //outputHTML += "</tbody>";
        //outputHTML += "</table>";
        //outputHTML += "<script type='text/javascript'>";
        //outputHTML += "document.f1.submit();";
        //outputHTML += "</script>";
        //outputHTML += "</form>";
        //outputHTML += "</body>";
        //outputHTML += "</html>";
        //Response.Write(outputHTML);
    }
    
}