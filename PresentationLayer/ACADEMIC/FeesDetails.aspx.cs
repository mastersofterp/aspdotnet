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
using System.Data;

public partial class ACADEMIC_FeesRefundReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFeeCollectionController = new FeeCollectionController();

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
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
                    this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "");

                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        int studentId = int.Parse(Request.QueryString["id"].ToString()); this.DisplayInformation(studentId);
                    }
                }
                txtEnrollNo.Focus();
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by btnSearch then call search method.
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                        this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();
            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 2)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                        case "Name":
                            objSearch.StudentName = paramValue;
                            break;
                        case "SRNO":
                            objSearch.Srno = paramValue;
                            break;
                        case "DegreeNo":
                            objSearch.DegreeNo = int.Parse(paramValue);
                            break;
                        case "BranchNo":
                            objSearch.BranchNo = int.Parse(paramValue);
                            break;
                        case "YearNo":
                            objSearch.YearNo = int.Parse(paramValue);
                            break;
                        case "SemNo":
                            objSearch.SemesterNo = int.Parse(paramValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.GetStudents(objSearch);
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesRefundReport.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesRefundReport.aspx");
        }
    }

    private void DisplayInformation(int studentId)
    {
        try
        {
            DataSet ds = objFeeCollectionController.GET_FEESDETAILS_IDNOWISE(studentId, Convert.ToInt32(Session["usertype"]));
            decimal sum = 0;
            if (ds.Tables.Count > 0 && ds != null)
            {
                   
                    lvFeesDetails.DataSource = ds;
                    lvFeesDetails.DataBind();
                    foreach (ListViewItem item in lvFeesDetails.Items)
                    {
                        Label lblpaid = item.FindControl("lblTotPaid") as Label;
                        Label lblRptCode = item.FindControl("lblRecieptCode") as Label;
                        if (lblRptCode.Text == "Refund fee")
                        {
                            sum -= Convert.ToDecimal(lblpaid.Text);
                        }
                        else
                        {
                            sum += Convert.ToDecimal(lblpaid.Text);
                        }
                       
                    }
                    lblPaidAmt.Text = sum.ToString();
                    if (Convert.ToDouble(lblPaidAmt.Text) > 0)
                    {
                        divtotamt.Visible = true;
                    }
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.PopulateStudentInfoSection(dr);
            }
            else
            {
                lvFeesDetails.DataSource = null;
                lvFeesDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.DisplayInformation() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            // Bind data with labels
            ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
            Session["idno"] = dr["IDNO"].ToString();
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["GENDER"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH"].ToString();
            lblYear.Text = dr["YEAR"].ToString();
            lblSemester.Text = dr["CURR_SEMESTERNO"].ToString();
            lblBatch.Text = dr["ADMBATCH"].ToString();
            lblFatherName.Text = dr["FATHERNAME"].ToString();
            lblMobileNo.Text = dr["MOBILENO"].ToString();
            divstudinfo.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtEnrollNo.Text.Trim() + "'"));
                if (studentId > 0)
                {
                    this.DisplayInformation(studentId);
                    divstudinfo.Visible = true;
                }
                else
                    objCommon.DisplayMessage(updEdit, "No student found having USN no.: ", this.Page);
                // objCommon.DisplayMessage("No student found having USN no.: " + txtEnrollNo.Text.Trim())
               // ShowMessage("No student found having USN no.: " + txtEnrollNo.Text.Trim());
            }
            else
            {
                //ShowMessage("Please enter USN number to show fees details.");
                objCommon.DisplayMessage("No student found having USN no.: ", this.Page);
                ShowMessage("No student found having USN no.: " + txtEnrollNo.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }
    

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

    //for report
    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            //btnReport.Visible = false;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fees_Details";
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

    private void ShowReport_All(string rptName, int Idno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fees_Details";
            url += "&path=~,Reports,Academic,rpt_GetFeesDetails_IDNOWise.rpt";
            url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_USERTYPE=" + Convert.ToInt32(Session["usertype"]);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fees_Details','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.ShowReport_All() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport_All("rpt_GetFeesDetails_IDNOWise.rpt", Convert.ToInt32(Session["idno"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Response.Redirect(url + "&id=" + lnk.CommandArgument);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    
    protected void ImgPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;
        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        
        //if (ViewState["COLLEGE_ID"].ToString() == "11" || ViewState["COLLEGE_ID"].ToString() == "12" || ViewState["COLLEGE_ID"].ToString() == "13")
        //{
        //    string PayModeCode = objCommon.LookUp("ACD_DCR", "PAY_MODE_CODE", "DCR_NO =" + DCR_NO);
        //    if (PayModeCode.ToUpper() == "C")
        //    {               
        //        this.ShowReport("FeeCollectionReceipt-SVIM.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        //    }
        //    else
        //    {               
        //        this.ShowReport("FeeCollectionReceipt-SVIM_BankChallan.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        //    }
            
        //}
        //else if (ViewState["COLLEGE_ID"].ToString() == "10")
        //{
        //    this.ShowReport("FeeCollectionReceipt-SVITS.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        //}
        //else
        //{
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        //}
     
    }
 
}