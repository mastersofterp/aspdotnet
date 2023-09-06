using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_ChallanReconciliationByStudent : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ChalanReconciliationController crController = new ChalanReconciliationController();
    FeeCollectionController feeController = new FeeCollectionController();
    #region Page Events

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PopulateDropDown();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (Session["usertype"].ToString().Equals("2"))     //Only student
                    {
                        divEnrollment.Visible = false;

                    }
                    else
                    {
                        divEnrollment.Visible = true;

                    }

                }
            }
            else
            {
                // Clear message div
                divMsg1.InnerHtml = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void PopulateDropDown()
    {
        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "RECIEPT_TITLE");
        this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg1.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ChalanReconciliation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ChallanReconciliationByStudent.aspx");
        }
    }
    #endregion


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divMSG.Visible = false;
            divFeeDetails.Visible = false;
            int IDNO = 0;
            if (divEnrollment.Visible == false)
            {
                IDNO = Convert.ToInt32(Session["idno"] == string.Empty ? "0" : Session["idno"].ToString());
                ViewState["StudId"] = IDNO;
            }
            else
            {
                IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollmentNo.Text + "'") == "" ? "0" : objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollmentNo.Text + "'"));
                if (IDNO == 0)
                {
                    divMSG.Visible = true;
                    lblmsg.Attributes.Add("style", "color:red");
                    lblmsg.Text = "Record not found!!";
                    return;
                }
                ViewState["StudId"] = IDNO;

            }

            if (ddlSemester.SelectedIndex > 0)
            {

                DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'TF' THEN 'Admission Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);
                if (dsDueFee.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDueFee.Tables[0].Rows.Count; i++)
                    {
                        if (dsDueFee.Tables[0].Rows[i]["DRTOTAL_AMT"].ToString() != dsDueFee.Tables[0].Rows[i]["DTOTAL_AMT"].ToString())
                        {
                            objCommon.DisplayMessage(this.Page, " Please Generate Challan for " + dsDueFee.Tables[0].Rows[i]["FEE_TITLE"].ToString() + " Semester " + dsDueFee.Tables[0].Rows[i]["SEMESTERNAME"].ToString(), this.Page);
                            return;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {

                }
                SemesterWiseFees();
            }

            //DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            //bool result1 = crController.Generate_Receipt_By_Student(Convert.ToInt32(ViewState["StudId"]), Convert.ToInt32(ddlSemester.SelectedValue), ddlReceiptType.SelectedValue, dcr);
            //ViewState["Dcrno"] = dcr.DcrNo.ToString();
            //if (result1 == true && Convert.ToInt32(ViewState["Dcrno"]) == 0)
            //{
            //    objCommon.DisplayMessage("Challan Reconciliation Already Done.", this.Page);
            //    ddlSemester.SelectedIndex = 0;
            //}
            //else
            //{
            //    string ReceiptType = ddlReceiptType.SelectedValue;
            //    int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

            //    DataSet ds = crController.GetDemand(Convert.ToInt32(ViewState["StudId"]), ReceiptType, SemesterNo);
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        lvFeeItems.DataSource = ds;
            //        lvFeeItems.DataBind();
            //        divFeeDetails.Visible = true;
            //        //btnPrint.Visible = true;
            //        btnReceipt.Visible = true;
            //        //To show Total Amount
            //        divMsg1.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmount(); </script>";
            //    }
            //    else
            //        objCommon.DisplayMessage("No fee demand found for this student.", this.Page);
            //}

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void SemesterWiseFees()
    {
        DataSet ds = null;
        int IDNO = Convert.ToInt32(ViewState["StudId"].ToString());
        ds = feeController.GetStudentFeesforOnlinePayment(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlSemester.SelectedValue), IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            decimal latefee = Convert.ToDecimal(ds.Tables[0].Rows[0]["LATE_FEE"].ToString());

            decimal total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AMOUNTS"));

            total = total + latefee;

            ViewState["Sessionno"] = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();

            if (total > 0)
            {
                string ReceiptType = ddlReceiptType.SelectedValue;
                int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

                ds = crController.GetDemand(Convert.ToInt32(ViewState["StudId"]), ReceiptType, SemesterNo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvFeeItems.DataSource = ds;
                    lvFeeItems.DataBind();
                    divFeeDetails.Visible = true;
                    //btnPrint.Visible = true;
                    btnReceipt.Visible = true;
                    divMSG.Visible = false;
                    //To show Total Amount
                    divMsg1.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmount(); </script>";
                }
            }
            else
            {
                // objCommon.DisplayMessage("Fees already paid for selected semester.", this.Page);
                divMSG.Visible = true;
                divFeeDetails.Visible = false;
                //  btnReciept.Visible = true;
                lblmsg.Attributes.Add("style", "color:green");
                lblmsg.Text = "Fees already paid for selected semester.";
                return;
            }
        }
        else
        {
            // objCommon.DisplayMessage("", this.Page);
            divFeeDetails.Visible = false;
            divMSG.Visible = true;
            lblmsg.Attributes.Add("style", "color:red");
            lblmsg.Text = "Fees demand not Created for Selected Semester, Please Contact Account Section.";
            return;
        }
    }

    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            DailyCollectionRegister dcr = this.Bind_FeeCollectionData();

            if (Session["userno"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            int result1 = crController.Generate_Receipt_By_Student(Convert.ToInt32(ViewState["StudId"]), Convert.ToInt32(ViewState["Sessionno"]), Convert.ToInt32(ddlSemester.SelectedValue), 1, ddlReceiptType.SelectedValue);
            ViewState["DcrNo"] = result1;
            if (result1 != -1 && Convert.ToInt32(ViewState["DcrNo"]) > 0)
            {
                divMsg1.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmount(); </script>";
                this.ShowReport("FeeCollectionReceipt_Student.rpt", Convert.ToInt32(ViewState["DcrNo"]), Convert.ToInt32(ViewState["StudentID"]), "1");
                //objCommon.DisplayMessage("Receipt Generated Successfully.", this.Page);

            }
            else
            {
                //objCommon.DisplayMessage("Receipt can not be Generated for this Demand.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, copyNo);
            divMsg1.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg1.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg1.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + Convert.ToInt32(ViewState["DcrNo"]) + "*MainRpt,@P_IDNO=" + ViewState["StudId"] + "*MainRpt,CopyNo=" + copyNo + "*MainRpt";
        return param;

    }

    private DailyCollectionRegister Bind_FeeCollectionData()
    {
        /// Bind transaction related data from various controls.
        DailyCollectionRegister dcr = new DailyCollectionRegister();
        try
        {
            dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
            dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());

            dcr.FeeHeadAmounts = this.GetFeeItems();
            DemandDrafts[] dds = null;
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.CollegeCode = Session["colcode"].ToString();
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.TotalAmount = this.GetTotalDemandAmt();
        }
        catch (Exception ex)
        {
            throw;
        }
        return dcr;
    }
    private double GetTotalDemandAmt()
    {
        double totalAmt = 0.00;
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                string amt = (item.FindControl("txtFeeItemAmount") as TextBox).Text.Trim();
                if (amt != null && amt != string.Empty)
                    totalAmt += double.Parse(amt);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return totalAmt;
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

                string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
                if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                    feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                if (feeAmt != null && feeAmt != string.Empty)
                    feeAmount = Convert.ToDouble(feeAmt);

                feeHeadAmts[feeHeadNo - 1] = feeAmount;
            }


        }
        catch (Exception ex)
        {
            throw;
        }
        return feeHeadAmts;
    }


}