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
                //this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
         if (txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Student Roll No.", this.Page);
                return;
            }

            string count = objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "ROLLNO='" + txtRollNo.Text.Trim() + "'");


            if (Convert.ToInt32(count) > 0)
            {
                int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim()+"'"));

                string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));

                if (Convert.ToInt32(demandno) == 0)
                {
                    this.ShowDetails();
                }
                else
                {
                    this.ShowDetailsAfterConfirm();
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "This is wrong Roll No.", this.Page);
                tblStudent.Visible = false;
            }
       // this.ClearControls_DemandDraftDetails();
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();

        string studentIDs = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'");

        if (rblStatus.SelectedIndex == 0)
        {

            if (rblPaymentStatus.SelectedIndex == 0)
            {
                string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));
                if (dcrNo != string.Empty && studentIDs != string.Empty)
                {
                    // if (txtPayType.Text.Trim() == "D")
                    //{
                    //    DemandModificationController dmController = new DemandModificationController();
                    //    string amount = txtDDAmount.Text;
                    //    string bankname = ddlBank.SelectedItem.Text;
                    //    string bankno = ddlBank.SelectedValue;
                    //    string date = txtDDDate.Text.Trim();
                    //    string city = txtDDCity.Text.Trim();
                    //    string ddno = txtDDNo.Text.Trim();
                    //    string collegecode = Session["colcode"].ToString(); ;

                    //    //string output = dmController.InserDDData(Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs),ddno,Convert.ToDateTime(date),Convert.ToInt32(bankno),bankname, amount,city,collegecode);
                    //    int output = dmController.InserDDData(Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs),ddno,Convert.ToDateTime(date),Convert.ToInt32(bankno),bankname, amount,city,collegecode);

                    //}
                    feeController.ReconcileDataForPro(Convert.ToInt32(studentIDs), Convert.ToInt32(Session["currentsession"]));
                    //ShowReport("Admission_Slip_Report", "AdmissionSlipForBtech.rpt", studentIDs);
                    objCommon.DisplayMessage(UpdatePanel1, "This student Admission Successfully", this.Page);

                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please paid the Fees", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Choose the Payment Status YES for Reconcilation", this.Page);
            }
        }

        else
        {
            CustomStatus cs = (CustomStatus)objSC.DeleteCourseRegistered(Convert.ToInt32(studentIDs), Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(lblSemester.ToolTip));
            
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayMessage("Provisional Admission Deleted Successfully", this.Page);
            else
                objCommon.DisplayMessage("Error in deleting record...", this.Page);
        }
    }

    //private void ShowReport(string reportTitle, string rptFileName, string idno)
    //{
    //    try
    //    {
    //        string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(idno));
    //        string admbatchno = objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + Convert.ToInt32(idno));

    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_BRANCHNO=" + Convert.ToInt32(branchno) + ",@P_ADMBATCH=" + Convert.ToInt32(admbatchno) + ",@Year=1";
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

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
        //txtDDNo.Text = string.Empty;
        //txtDDAmount.Text = string.Empty;
        //txtDDCity.Text = string.Empty;
        //txtDDDate.Text = string.Empty;
        //ddlBank.SelectedIndex = 0;
    }

    private void ShowDetails()
    {
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();
       
        try
        {
                   int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'"));
                    DataTableReader dtr = objSC.GetProStudentDetails(idno);

                    if (dtr != null)
                    {
                        tblStudent.Visible = true;
                        if (dtr.Read())
                        {
                            lblTempNo.Text = dtr["IDNO"].ToString();
                            string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                            lblBranch.Text = branchname;
                            lblBranch.ToolTip = dtr["branchno"].ToString();
                            lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                            lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                            lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                            lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                            string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                            lblSemester.Text = semester;
                            lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
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
                            city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                            lblPCity.Text = city;
                            string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + dtr["DEGREENO"].ToString());
                            lblDegree.Text = degree;
                            string paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENAME", "PAYTYPENO=" + dtr["PTYPE"].ToString());
                            lblPaymenttype.Text = paytype;
                            lblPaymenttype.ToolTip = dtr["PTYPE"].ToString();
                            
                            int degreeno = Convert.ToInt32(dtr["DEGREENO"]);

                            trPayStatus.Visible = false;
                            trPrint.Visible = true;
                            trSubmit.Visible = false;
                            trStatus.Visible = true;
                            trMsg.Visible = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "This is not a Provisional Admission Student.", this.Page);
                            tblStudent.Visible = false;
                        }
                       
                    }
              
               
        }
        catch (Exception ex)
        {
            
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ProvisionalAdmissionConfirmation.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetailsAfterConfirm()
    {
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();

        try
        {
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'"));
            DataTableReader dtr = objSC.GetProStudentDetails(idno);

            if (dtr != null)
            {
                tblStudent.Visible = true;
                if (dtr.Read())
                {
                    lblTempNo.Text = dtr["IDNO"].ToString();
                    string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                    lblBranch.Text = branchname;
                    lblBranch.ToolTip = dtr["branchno"].ToString();
                    lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                    lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                    lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                    lblSemester.Text = semester;
                    lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
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
                    city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                    lblPCity.Text = city;
                    string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + dtr["DEGREENO"].ToString());
                    lblDegree.Text = degree;
                    string paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENAME", "PAYTYPENO=" + dtr["PTYPE"].ToString());
                    lblPaymenttype.Text = paytype;
                    lblPaymenttype.ToolTip = dtr["PTYPE"].ToString();
                    
                    int degreeno = Convert.ToInt32(dtr["DEGREENO"]);
                    trPayStatus.Visible = true;
                    trPrint.Visible = false;
                    trSubmit.Visible = true;
                    trStatus.Visible = false;
                    trMsg.Visible = true;
                    
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "This is not a Provisional Admission Student.", this.Page);
                    tblStudent.Visible = false;
                }
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ProvisionalAdmissionConfirmation.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblStatus.SelectedIndex == 1)
        {
            trSubmit.Visible = true;
            trPrint.Visible = false;
        }
        else
        {
            trSubmit.Visible = false;
            trPrint.Visible = true;
        }
    }
    protected void btnPrintChallan_Click(object sender, EventArgs e)
    {
        try
        {

            //Create Demand and Print the Challan..
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = this.GetDemandCriteria();
            int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);
            string studentIDs = lblTempNo.Text.Trim();

            bool overwriteDemand = true;

            

            string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + studentIDs.ToString() + " AND SEMESTERNO=" + selectSemesterNo);
            if (Convert.ToInt32(demandno) <= 0)
            {
                string response = dmController.CreateDemandForStudents(studentIDs, demandCriteria, selectSemesterNo, overwriteDemand);
                
               
            }

            //Create DCR and print Challan
            string receiptno = this.GetNewReceiptNo();
            FeeDemand dcr = this.GetDcrCriteria();
            string dcritem = dmController.CreateDcrForStudents(studentIDs, dcr, selectSemesterNo, overwriteDemand, receiptno);


            //Print Challan..

            //this.ShowReport("FeeCollectionReceiptForCourseRegister.rpt", Convert.ToInt32(studentIDs), "1");

            string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + selectSemesterNo);

            if (dcrNo != string.Empty && studentIDs != string.Empty)
            {
                this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Academic_ProvisionalAdmissionConfirmation.btnPrintChallan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            demandCriteria.SemesterNo = int.Parse(lblSemester.ToolTip);
            demandCriteria.PaymentTypeNo = int.Parse(lblPaymenttype.ToolTip);

            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Academic_ProvisionalAdmissionConfirmation.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }


    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
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
                objCommon.ShowError(Page, "Academic_Academic_ProvisionalAdmissionConfirmation.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }



    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = int.Parse(lblSemester.ToolTip);
            dcrCriteria.PaymentTypeNo = int.Parse(lblPaymenttype.ToolTip);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.ExcessAmount = 0;
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Academic_ProvisionalAdmissionConfirmation.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
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
                objCommon.ShowError(Page, "Academic_ProvisionalAdmissionConfirmation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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
      
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

}
