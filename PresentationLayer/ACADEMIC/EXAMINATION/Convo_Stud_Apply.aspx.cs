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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
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
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Convo_Stud_Apply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();
    CourseController objCC = new CourseController();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentRegistration objSRegist = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();
    bool IsDataPresent = false;

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //TO SET THE MASTERPAGE
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CHECK SESSION

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            //else
            //{
            //    //PAGE AUTHORIZATION
            //    // this.CheckPageAuthorization();

            //    //SET THE PAGE TITLE
            //    this.Page.Title = Session["coll_name"].ToString();

            //    //LOAD PAGE HELP
            //    if (Request.QueryString["pageno"] != null)
            //    {
            //        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            //    }

            //}
            else
            {
                //PAGE AUTHORIZATION
                //this.CheckPageAuthorization();
                if (CheckActivity() == false)
                {
                    Session["getuserno"] = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect script", "alert('Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.!'); window.location = '" + Page.ResolveUrl("~/default.aspx") + "'", true);
                    return;
                }

                ShowDetails();
                checkDefinefee();
            }
        }
    }
    #endregion
    #region ShowDetails
    //Student Basic details
    private void ShowDetails()
    {
        try
        {
            StudentController objSC = new StudentController();
            int uano = Convert.ToInt32(Session["idno"]);
           // int uano = 5095;

            if (uano > 0)
            {
                string SP_Name = "PKG_ACD_STUDENTS_DEGREE_COMPLETE_CONVOCATION";
                string SP_Parameters = "@P_IDNO";
                string Call_Values = "" + uano + "";
                DataSet dsStudent = null;
                dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    lblregno.Text = dsStudent.Tables[0].Rows[0]["Regno"].ToString();
                    ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["Regno"].ToString();
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    ViewState["STUDNAME"] = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblMobile.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    ViewState["MOBILENO"] = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    lblEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                    ViewState["EMAILID"] = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString(); 
                    lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREE"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["BRANCH"].ToString();
                    ViewState["SHORTNAME"] = dsStudent.Tables[0].Rows[0]["BRANCH"].ToString();
                    lblAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                    lblClassObtained.Text = dsStudent.Tables[0].Rows[0]["CLASS OBTAINED"].ToString();
                    lblsem.Text = dsStudent.Tables[0].Rows[0]["SEMESTER"].ToString();
                    //lblFees.Text = dsStudent.Tables[0].Rows[0]["FEE"].ToString();
                    //Session["Amt"] = dsStudent.Tables[0].Rows[0]["FEE"].ToString();
                }
                else 
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Student Details Not found!!!", this.Page);
            return;
        }
    }
    #endregion


    private bool CheckActivity()
    {
        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            bool ret = true;
            string sessionno = string.Empty;
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE IN('CONVO','Convo')");
            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "MAX(SA.SESSION_NO)", "am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%' AND SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            ViewState["sessionno"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno),2, Convert.ToInt32(Request.QueryString["pageno"].ToString()));
            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    ret = false;
                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    ret = false;   // Temp Comment 
                }
            }
            else
            {
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
            }
            dtr.Close();
            return ret;
        }
        else
        {
            bool ret = true;
            string sessionno = string.Empty;

            //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "MAX(SA.SESSION_NO)", "am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            ViewState["sessionno"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    ret = false;
                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    ret = false;   // Temp Comment 
                }
            }
            else
            {
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
            }
            dtr.Close();
            return ret;

        }
    }

    #region report
    private void ShowReportConv(string reportTitle, string rptFileName)
    {
        try
        {

            string orderid = objCommon.LookUp("ACD_CONVOCATION_PAYEMENT_LOG", "ORDERID", "UA_NO=" + Convert.ToInt32(ViewState["UA_NO"]) + "AND ISNULL(RECON,0) = 1");
            
            if (orderid != "")
            {
                string url = "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=56,@P_ORDER_ID=" + orderid; 
                string Script = string.Empty;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Student Details Not found", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
            }

            ////To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnPay_Click(object sender, EventArgs e)
    {
        //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_CONVOCATION_PAYEMENT_LOG", "COUNT(1) PAY_COUNT", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND RECEIPT_CODE = 'CON'"));
        int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionno"]) + " AND RECIEPT_CODE = 'CON' AND RECON = 1 AND ISNULL(CAN,0)=0"));
        if (ifPaidAlready > 0)
        {
            objCommon.DisplayMessage(this.Page, "Convocation Fee paid already. Can not proceed with the transaction !!.", this.Page);
            btnPay.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
            return;
        }

        DemandGenerate();

        //// FOR CPU KOTA
        if (Convert.ToInt32(Session["OrgId"]) == 3)      //CashfreePayment Gateway  
        {
           
            RazorPaymentGateway();
        }
        else
        {
            
        }
        //RazorPaymentGateway();
       
    }

    private void checkDefinefee() 
    {
        try 
        {
            double Chk = Convert.ToDouble(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION T INNER JOIN ACD_EXAM_TYPE E WITH (NOLOCK) ON (T.FEETYPE = E.EXAM_TYPENO)", "ISNULL(T.FEE,0) AS FEE", "T.COLLEGE_ID =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + " AND T.SESSIONNO = " + Convert.ToInt32(ViewState["sessionno"]) + " AND T.SEMESTERNO = " + Convert.ToInt32(ViewState["SEMESTERNO"]) + " AND T.DEGREENO = " + Convert.ToInt32(ViewState["DEGREENO"]) + " AND E.EXAM_TYPE LIKE '%Convocation%' AND E.ACTIVESTATUS = 1 and isnull(CANCEL,0) = 0"));
            if (Chk == 0)
            {
                objCommon.DisplayMessage(this.Page, "Convocation Fee Not Define. Can not proceed with the transaction !!.", this.Page);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect script", "alert('Convocation Fee has been paid already. Can't proceed with the transaction !'); window.location = '" + Page.ResolveUrl("~/Convocation_Details.aspx") + "'", true);
                btnPay.Visible = false;
                btnReciept.Visible = false;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                return;
            }
            else 
            {
                lblFees.Text = Chk.ToString();
                Session["Amt"] = Chk.ToString();
            }

        }catch(Exception ex)
        {
        }
    }

    #region CREATE DEMAND
    private void DemandGenerate() 
    {
        try 
        {

            StudentController objSC = new StudentController();
            CreateStudentPayOrderId();
            DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
            string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
            //objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
            objSR.IDNO = Convert.ToInt32(Session["idno"]);
            objSR.REGNO = ViewState["REGNO"].ToString();
            objSR.SCHEMENO = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString());
            int SEMESTERNO = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            int user = Convert.ToInt32(Session["usertype"]);
            string Amt = Session["Amt"].ToString();

            objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionno"]);
           
            //create Demand

            CustomStatus cs = (CustomStatus)objSRegist.AddConvocationRegistrationDetails(objSR, Amt, ViewState["OrderId"].ToString(), SEMESTERNO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //objCommon.DisplayMessage(this.Page, "Convocation Successfully !!.", this.Page);
                ////objCommon.DisplayMessage(" Successfully!!", this.Page);
                //ShowDetails();
                //return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Getting Error !!.", this.Page);
                //objCommon.DisplayMessage("Getting Error!!", this.Page);
                return;
            }
        }catch(Exception ex)
        {
        }
    }
    #endregion

    private void CreateStudentPayOrderId()
    {
        ViewState["OrderId"] = null;
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        //string Orderid = Convert.ToString((Convert.ToInt32(Session["IDNO"].ToString())) + (Convert.ToString(ViewState["Branch"].ToString())) + (Convert.ToString(ViewState["Semester"].ToString())) + ir);
        string Orderid = Convert.ToString((Convert.ToInt32(Session["IDNO"].ToString())) + (Convert.ToString(10)) + (Convert.ToString(2)) + ir);


        ViewState["OrderId"] = Orderid;
        Session["Order_id"] = Orderid;
    }

    #region "Online Payment"
    protected void RazorPaymentGateway()
    {
        
        try
        {
            Session["studName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
            Session["studPhone"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;   
            Session["studEmail"] = ViewState["EMAILID"].ToString();  // lblMailId.Text;
            Session["ReceiptType"] = "CON";
            Session["idno"] = Session["idno"].ToString();
            Session["homelink"] = "Convo_Stud_Apply.aspx";
            Session["regno"] = ViewState["REGNO"].ToString(); // lblRegno.Text;
            Session["payStudName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
            Session["paymobileno"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
            Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
            Session["Branchname"] = ViewState["SHORTNAME"].ToString(); //lblBranchName.Text;
            Session["IPADDRESS"] = Session["ipAddress"].ToString();
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            // Session["studAmt"] = Session["Amt"].ToString();
            Session["studAmt"] = 1;
            Session["ReceiptType"] = "CON";
            Session["paysession"] = ViewState["sessionno"].ToString();
            Session["paysemester"] = ViewState["SEMESTERNO"].ToString();

            int activityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME like '%Convocation%'"));
            Session["payactivityno"] = activityno;
            DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 1, activityno);
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
        catch (Exception ex)
        {
            throw;
        }

    }
    #endregion
}