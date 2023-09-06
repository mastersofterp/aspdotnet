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

public partial class ACADEMIC_CheckStudentInfo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentRegistration objSReg = new StudentRegistration();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
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
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                tblStudent.Visible = false;
                this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.ShowDetails();
        this.ClearControls_DemandDraftDetails();
        txtPayType.Text = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string studentIDs=txtTempNo.Text.Trim();

        if (rblPaymentStatus.SelectedIndex == 0)
        {
            
            string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs));
            if (dcrNo != string.Empty && studentIDs != string.Empty)
            {
                if (txtPayType.Text.Trim() == "D" || txtPayType.Text.Trim() == "I")
            {
                DemandModificationController dmController = new DemandModificationController();
                string amount = txtDDAmount.Text;
                string bankname = ddlBank.SelectedItem.Text;
                string bankno = ddlBank.SelectedValue;
                string date = txtDDDate.Text.Trim();
                string city = txtDDCity.Text.Trim();
                string ddno = txtDDNo.Text.Trim();
                string collegecode = Session["colcode"].ToString();
                int sessionno = Convert.ToInt32(Session["currentsession"]);
                int semesterno = 1;
                string paytype = txtPayType.Text.Trim();

                //string output = dmController.InserDDData(Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs),ddno,Convert.ToDateTime(date),Convert.ToInt32(bankno),bankname, amount,city,collegecode);
                int output = dmController.InserDDDataSemPramote(Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), ddno, Convert.ToDateTime(date), Convert.ToInt32(bankno), bankname, amount, city, collegecode, sessionno,semesterno, paytype, "");
                    
            }
                // check for the only Cash
                if (txtPayType.Text.Trim() == "C")
                {
                    feeController.UpdateReconcileDate(Convert.ToInt32(studentIDs), Convert.ToDateTime(txtCashDate.Text.Trim()));
                }

                feeController.ReconcileData(Convert.ToInt32(studentIDs));
                ShowReport("Admission_Slip_Report", "AdmissionSlipForBtech.rpt", studentIDs);
                //objCommon.DisplayMessage(UpdatePanel1,"Chalan Reconcile this student", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1,"Please paid the Fees", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Choose the Payment Status YES for Reconcilation", this.Page);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string idno)
    {
        try
        {
            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(idno));
            string admbatchno = objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + Convert.ToInt32(idno));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_BRANCHNO=" + Convert.ToInt32(branchno) + ",@P_ADMBATCH=" + Convert.ToInt32(admbatchno) + ",@Year=1";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

  


    #region Displaying Demand Draft Details

    //protected void btnSaveDD_Info_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt;
    //        if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //        {
    //            dt = ((DataTable)Session["DD_Info"]);
    //            DataRow dr = dt.NewRow();
    //            dr["DD_NO"] = txtDDNo.Text.Trim();
    //            dr["DD_DT"] = txtDDDate.Text.Trim();
    //            dr["DD_CITY"] = txtDDCity.Text.Trim();
    //            dr["DD_BANK_NO"] = ddlBank.SelectedValue;
    //            dr["DD_BANK"] = ddlBank.SelectedItem.Text;
    //            dr["DD_AMOUNT"] = txtDDAmount.Text.Trim();
    //            dt.Rows.Add(dr);
    //            Session["DD_Info"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);

    //            // add the two DD for the same semester add the previous DD amount and current DD amount// 25/05/2012
    //            // txtTotalAmount.Text = (Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtDDAmount.Text.Trim())).ToString();

    //        }
    //        else
    //        {
    //            dt = this.GetDemandDraftDataTable();
    //            DataRow dr = dt.NewRow();
    //            dr["DD_NO"] = txtDDNo.Text.Trim();
    //            dr["DD_DT"] = txtDDDate.Text.Trim();
    //            dr["DD_CITY"] = txtDDCity.Text.Trim();
    //            dr["DD_BANK_NO"] = ddlBank.SelectedValue;
    //            dr["DD_BANK"] = ddlBank.SelectedItem.Text;
    //            dr["DD_AMOUNT"] = txtDDAmount.Text.Trim();
    //            dt.Rows.Add(dr);
    //            Session.Add("DD_Info", dt);
    //            this.BindListView_DemandDraftDetails(dt);

    //            //Enter the DD amount then add the DD amount to total amount textbox //add code 25/05/2012

    //            //txtTotalAmount.Text = txtDDAmount.Text.Trim();

    //        }
    //        this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> UpdateCash_DD_Amount();  </script> ";
    //        this.ClearControls_DemandDraftDetails();
    //        // txtTotalAmount.Focus();
    //        //btnSaveDD_Info.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnSaveDD_Info_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }

    //}

    //protected void btnEditDDInfo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEdit = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //        {
    //            dt = ((DataTable)Session["DD_Info"]);
    //            DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
    //            txtDDNo.Text = dr["DD_NO"].ToString();
    //            txtDDDate.Text = dr["DD_DT"].ToString();
    //            txtDDCity.Text = dr["DD_CITY"].ToString();
    //            ddlBank.SelectedValue = dr["DD_BANK_NO"].ToString();
    //            txtDDAmount.Text = dr["DD_AMOUNT"].ToString();
    //            dt.Rows.Remove(dr);
    //            Session["DD_Info"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);

    //            // for Edit the data to maintain the Total amount // 25/04/2012
    //            //txtTotalAmount.Text = (Convert.ToDouble(txtTotalAmount.Text.Trim()) - Convert.ToDouble(txtDDAmount.Text.Trim())).ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnEditDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void btnDeleteDDInfo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelete = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //        {
    //            dt = ((DataTable)Session["DD_Info"]);
    //            dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
    //            Session["DD_Info"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);

    //            // This code add for delete the DD amount to duduct the amount for total amount.// 26/04/2012
    //            if (dt.Rows.Count != 0)
    //            {
    //                string ddAmt = dt.Rows[0]["DD_AMOUNT"].ToString();
    //                //txtTotalAmount.Text = "0";
    //                //txtTotalDDAmount.Text = "0";
    //            }
    //            //else
    //            //{
    //            //    string ddAmt = dt.Rows[0]["DD_AMOUNT"].ToString();
    //            //    txtTotalAmount.Text = ddAmt;

    //            //}
    //            //txtTotalAmount.Focus();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private void BindListView_DemandDraftDetails(DataTable dt)
    //{
    //    try
    //    {
    //        divDDDetails.Style["display"] = "block";
    //        //divFeeItems.Style["display"] = "block";
    //        //lvDemandDraftDetails.DataSource = dt;
    //        //lvDemandDraftDetails.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private DataTable GetDemandDraftDataTable()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add(new DataColumn("DD_NO", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DD_DT", typeof(DateTime)));
    //    dt.Columns.Add(new DataColumn("DD_CITY", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DD_BANK_NO", typeof(int)));
    //    dt.Columns.Add(new DataColumn("DD_BANK", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DD_AMOUNT", typeof(double)));

    //    return dt;
    //}

    //private DataRow GetEditableDataRow(DataTable dt, string value)
    //{
    //    DataRow dataRow = null;
    //    try
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            if (dr["DD_NO"].ToString() == value)
    //            {
    //                dataRow = dr;
    //                break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return dataRow;
    //}

    
    #endregion

    private void ClearControls_DemandDraftDetails()
    {
        txtDDNo.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDCity.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtCashDate.Text = string.Empty;
    }

    protected void btnModifyPType_Click(object sender, EventArgs e)
    {
        try
        {

            objSReg.UpdateBranchCategory(lblTempNo.Text.Trim(), ddlBranch.SelectedValue, ddlAdmcategory.SelectedValue);
            objCommon.DisplayMessage("Updated Successfully!", this.Page);
            this.ShowDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.btnModifyPType_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetails()
    {
        StudentController objSC = new StudentController();
        DataSet dsFees;
        //dsDocuments;
        FeeCollectionController feeController = new FeeCollectionController();
        //int idno = feeController.GetStudentIdByEnrollmentNo(txtTempNo.Text.Trim());
        int idno = Convert.ToInt32(txtTempNo.Text.Trim());
        string count = objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "IDNO=" + idno);

        try
        {
            if (Convert.ToInt32(count) > 0)
            {

                string dcrNo = objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + idno);

                if (Convert.ToInt32(dcrNo) > 0)
                {

                    DataTableReader dtr = objSC.GetStudentDetailsForCheck(idno);

                    if (dtr != null)
                    {
                        tblStudent.Visible = true;
                        if (dtr.Read())
                        {
                            lblTempNo.Text = dtr["IDNO"].ToString();
                            string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                            lblBranch.Text = branchname;
                            lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                            lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                            lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                            //string caste = objCommon.LookUp("ACD_CASTE", "CASTE", "CASTENO=" + dtr["CASTE"].ToString());
                            //lblCaste.Text = caste;
                            string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["ADMCATEGORYNO"].ToString());
                            lblCategory.Text = category;
                            string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                            lblReligion.Text = religion;
                            string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                            lblNationality.Text = nation;
                            lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                            string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                            lblCity.Text = city;
                            lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                            lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                            lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                            lblRemark.Text = dtr["REMARK"] == null ? string.Empty : dtr["REMARK"].ToString();
                            city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                            lblPCity.Text = city;
                            string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + dtr["DEGREENO"].ToString());
                            lblDegree.Text = degree;
                            string paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENAME", "PAYTYPENO=" + dtr["PTYPE"].ToString());
                            lblPaymenttype.Text = paytype;
                            //BRANCH AND CATEGORY
                            int degreeno = Convert.ToInt32(dtr["DEGREENO"]);
                            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + degreeno, "BRANCHNO");
                            objCommon.FillDropDownList(ddlAdmcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORYNO");

                            ddlAdmcategory.SelectedValue = dtr["ADMCATEGORYNO"].ToString();
                            ddlBranch.SelectedValue = dtr["branchno"].ToString();

                            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";

                            //Students Fees Details
                            dsFees = objSC.RetrieveStudentFeesDetails(idno);
                            if (dsFees.Tables[0].Rows.Count > 0)
                            {
                                lvFees.DataSource = dsFees;
                                lvFees.DataBind();
                            }
                            else
                            {
                                lvFees.DataSource = null;
                                lvFees.DataBind();
                            }

                            //STUDENT Document list
                            //string documentno = objCommon.LookUp("ACD_STUDENT_DOC_LIST", "DOCUMENTNO", "IDNO=" + idno);

                            //dsDocuments = objSC.GetStudentsDocuments(idno, documentno);
                            //if (dsDocuments.Tables[0].Rows.Count > 0)
                            //{
                            //    lvDocumentList.DataSource = dsDocuments;
                            //    lvDocumentList.DataBind();


                            //}
                            //else
                            //{
                            //    lvDocumentList.DataSource = null;
                            //    lvDocumentList.DataBind();

                            //}

                        }
                    }

                    string chk_recon= objCommon.LookUp("ACD_DCR","RECON","IDNO="+ idno);
                    if (chk_recon == "True")
                    {
                        trButtons.Visible = false;
                        trmsg.Visible = false;
                        trpayStatus.Visible = false;
                        trpayType.Visible = false;
                        trSlip.Visible = true;
                    }
                    else
                    {
                        trButtons.Visible = true;
                        trmsg.Visible = true;
                        trpayStatus.Visible = true;
                        trpayType.Visible = true;
                        trSlip.Visible = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please paid the Fees. ", this.Page);
                    tblStudent.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "No student found having Temp. no. ", this.Page);
                tblStudent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnChallan_Click(object sender, EventArgs e)
    {
        string studentIDs = txtTempNo.Text.Trim();


        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=1 AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));

        if (dcrNo != string.Empty && studentIDs != string.Empty)
        {
            this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
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
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        //string param = "@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //param += ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        //param += ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        //param += ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        //return param;
        string collegeCode = "8";

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + collegeCode + "";
        return param;
    }

    protected void btnAdmissionSlip_Click(object sender, EventArgs e)
    {
        string studentIDs = txtTempNo.Text.Trim();
        ShowReport("Admission_Slip_Report", "AdmissionSlipForBtech.rpt", studentIDs);
    }
}
