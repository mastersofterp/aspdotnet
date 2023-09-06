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

public partial class ACADEMIC_SingleStudentScholershipAdjustment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    DailyCollectionRegister dcr = new DailyCollectionRegister();
    int prev_status;
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
                PopulateDropDown();
                PopulateDropDownList();
                // Set the Page Title
                //Page.Title = Session["coll_name"].ToString();

                // Load Page Help
                //Page.Title = Session["coll_name"].ToString();

                // Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                ///objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
                ddlSearch.SelectedIndex = 0;
     
               // ViewState["ReceiptType"] = "TF";
                divSingleStudDetail.Visible = false;
                txtToDate.Text = DateTime.Today.ToShortDateString();
                //ddlReceipttype.SelectedValue = "TF";

                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                {
                    //divSchltype.Visible = false;
                    //divSemester.Visible = false;

                    //ddlScholorshipType.SelectedValue = "1";

                    ddlReceiptSing.SelectedIndex = 1;
                }
                else
                {
                    //divSemester.Visible = true;
                   // lblYearMandatory.Visible = true;
                    ddlReceiptSing.SelectedIndex = 1;
                }

                //this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }
    #region Tab 1 Single Student Scholarship Adjustment

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlReceiptSing, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO=1", "RECIEPT_TITLE");
            objCommon.FillDropDownList(ddlBankName, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int semesterno ,int schlid)
    {
        try
        {     

            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
            {
                ddlReceiptSing.SelectedIndex = 1;
            }
            else
            {
                ddlReceiptSing.SelectedIndex = 1;
            }

            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + "");

            if (Convert.ToInt32(ViewState["idno"]) > 0)
            {
                DataSet dsStudent = studCont.GetStudentDetails_For_ScholarshipAdjustment(Convert.ToInt32(ViewState["idno"]), semesterno, schlid);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {

                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        
                        lblDegrreno.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();

                        //lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();

                        int Branchno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString());
                        ViewState["Branchno"] = Branchno;

                        lblDegrreno.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        lblSingCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        lblSingCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        lblYear.Text = dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString();
                        lblYear.ToolTip = dsStudent.Tables[0].Rows[0]["YEAR"].ToString();
                        lblschlType.Text = dsStudent.Tables[0].Rows[0]["SCHOLORSHIPNAME"].ToString();
                        lblschlType.ToolTip = dsStudent.Tables[0].Rows[0]["SCHOLARSHIP_ID"].ToString();
                        lblAcademicYear.Text = dsStudent.Tables[0].Rows[0]["ACADEMIC_YEAR_NAME"].ToString();
                        lblAcademicYear.ToolTip = dsStudent.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
                        lblSemsterName.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemsterName.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                        BindListView();
                        //GridView GV = new GridView();                                                   
                        //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";                       
                    }
                    else
                    {
                        //divCourses.Visible = false;
                        objCommon.DisplayMessage(updSingleStud, "Registration Details not found for this session!", this.Page);
                    }
                }
                else
                {
                    //divCourses.Visible = false;
                    objCommon.DisplayMessage(updSingleStud, "Registration Details not found for this session!", this.Page);
                }
            }

            //}
            //else
            //{
            //    //divCourses.Visible = false;
            //    objCommon.DisplayMessage(updFee, "No Record Found!!!", this.Page);
            //    // Panel1.Visible = false;
            //}


        }
        catch (Exception ex)
        {
            throw;
        }
    }




    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //divdata.Visible = false;
            //ClearControls();

            if (ddlSearch.SelectedIndex > 0)
            {
                txtStudent.Text = string.Empty;
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtStudent.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        //lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtStudent.Visible = true;
                        pnlDropdown.Visible = false;
                    }
                }
            }
            else
            {
                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
           LinkButton lnk = sender as LinkButton;     
          //LinkButton btn = (LinkButton) sender;
           ListViewDataItem item = lnk.NamingContainer as ListViewDataItem;
          LinkButton lnkbutton = item.FindControl("lnkId") as LinkButton;

          int schlid = Convert.ToInt32(lnkbutton.ToolTip);
          int Semesterno = Convert.ToInt32(lnkbutton.CommandName);


        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        ViewState["idno"] = Session["stuinfoidno"].ToString();

        // divdata.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        //updEdit.Visible = false;
        // pnlstudetails.Visible = true;
        //divtypeofStudent.Visible = true;
        // divSearchInfo.Visible = true;
        btnSubmitForSingleStu.Visible = true;
        btnSubmitForSingleStu.Enabled = true;
        btnCancel1.Visible = true;
        //btnShowForSingle.Visible = true;
        pnlbody.Visible = false;
        //ShowStudentDetails();

        divSingleStudDetail.Visible = true;
        ShowDetails(Semesterno, schlid);
        //objCommon.FillDropDownList(ddlScholorshipType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0", "SCHOLORSHIPTYPENO");
        // ShowDetails(Convert.ToInt32(lnk.CommandArgument));
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Search Criteria ", this.Page);
            pnlDropdown.Visible = false;
            return;
        }
        else if (txtStudent.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.Page, "Please Enter Search String ", this.Page);
            return;
        }


        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            value = ddlDropdown.SelectedValue;
        else
            value = txtStudent.Text;

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtStudent.Text = string.Empty;
        //  divdata.Visible = false;
    }

    private void PopulateDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
            ddlSearch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsSingleAdjustment(searchtext, category);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
           
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.DisplayMessage(this.Page, "Record Not Found,Kindly Check for  Scholership allotment is Creates or Not.", this.Page);
            return;
        }
    }

 
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void btnSubmitForSingleStu_Click(object sender, EventArgs e)
    {
        this.SaveScholarshipAdjustment();
    }

    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;
        try
        {
            ViewState["ReceiptType"] = ddlReceiptSing.SelectedValue;

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

    private void SaveScholarshipAdjustment()
    {
        int id = 0;
        int count = 0;
        int output = 0;
        bool flag = false;
        GetNewReceiptNo();
        TextBox txtAdjAmt;

        foreach (ListViewDataItem itm in lvStudentRecords.Items)
        {
            CheckBox chk = itm.FindControl("chkReport") as CheckBox;
            if (chk.Checked == true && chk.Enabled == true)       
            {
                TextBox txtSemesterAmount = itm.FindControl("txtSemesterAmount") as TextBox;

                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;
                Label lblname = itm.FindControl("lblname") as Label;
                Label lblamt = itm.FindControl("lblschamt") as Label;
                Label lblExcessAmount = itm.FindControl("lblRemainingAmt") as Label;
                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                {
                    txtAdjAmt = itm.FindControl("txtAdjAmount") as TextBox;
                }
                else
                {
                    txtAdjAmt = itm.FindControl("txtAdjustAmount") as TextBox;
                }
                //TextBox txtAdjAmt = itm.FindControl("txtAdjAmount") as TextBox;
                Label lblBranch = itm.FindControl("lblDDLData") as Label;
                HiddenField hdfBranchno = itm.FindControl("hdfBranchno") as HiddenField;
                HiddenField hdfdegreeno = itm.FindControl("hdfDegree") as HiddenField;
                CheckBox chkRpt = (CheckBox)itm.FindControl("chkReport");
                dcr.StudentId = Convert.ToInt32(chkRpt.ToolTip.ToString());

                TextBox txtDDNumber = itm.FindControl("txtDDNumber") as TextBox;
               
                dcr.StudentName = lblname.Text;
                dcr.UserNo = Int32.Parse(Session["userno"].ToString());
                dcr.CounterNo = Convert.ToInt32(ViewState["CounterNo"].ToString());
                dcr.ReceiptTypeCode = ddlReceiptSing.SelectedValue;


                if (txtAdjAmt.Text == "")
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Enter Scholarship Paid Amount", this.Page);
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
            
                dcr.DegreeNo = Convert.ToInt32(hdfdegreeno.Value);
                dcr.BranchNo = Convert.ToInt32(hdfBranchno.Value);
                dcr.YearNo =  Convert.ToInt32(lblYear.ToolTip);;
                dcr.SemesterNo = Convert.ToInt32(lblSemsterName.ToolTip);
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


                int academicYearID = Convert.ToInt32(lblAcademicYear.ToolTip);


                if (count == 0)
                {
                    //objCommon.ConfirmMessage(this.updtime1, "Are you sure to adjust scholarship for selected students?", this.Page);
                }
                int ScholarshipId = Convert.ToInt32(lblschlType.ToolTip);

                output = feeController.Insert_Scholarship_Adjustment_Single_Student(ref dcr, f4, bankid, transactionid, academicYearID, ScholarshipId);
                count++;

                string ScholorshipNO = (objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "ISNULL(SCHLSHPNO,0)", "IDNO=" + dcr.StudentId + "AND SEMESTERNO=" + dcr.SemesterNo + "AND SCHOLARSHIP_ID=" + ScholarshipId));

                feeController.Update_Shcolorship_Status_Single_Student(ScholorshipNO, f4, int.Parse(Session["userno"].ToString()), ViewState["ipAddress"].ToString());

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
            objCommon.DisplayUserMessage(this.Page, "Scholarship Adjusted Successfully.", this.Page);
    }


    protected void BindListView()
    {
        try
        {
            DataSet ds;
            int sessionno = Convert.ToInt32(Session["currentsession"]);

            if (Convert.ToInt32(ViewState["idno"]) > 0)
            {
    
            }
           
            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }

            ds = studCont.GetStudentScholershipAdjustmentForSingleStudent(Convert.ToInt32(lblDegrreno.ToolTip), Convert.ToInt32(ViewState["Branchno"]), Convert.ToInt32(lblYear.ToolTip), Convert.ToInt32(lblSemsterName.ToolTip), prev_status, Convert.ToInt32(lblAcademicYear.ToolTip), Convert.ToInt32(lblSingCollege.ToolTip), Convert.ToInt32(lblschlType.ToolTip), Convert.ToInt32(lblAdmBatch.ToolTip), Convert.ToInt32(ViewState["idno"]));

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
                    if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                    {
                        TextBox txtAdjAmt = itm.FindControl("txtAdjAmount") as TextBox;
                        txtAdjAmt.Visible = true;
                    }
                    else
                    {
                        TextBox txtAdjAmt = itm.FindControl("txtAdjustAmount") as TextBox;
                        txtAdjAmt.Visible = true;
                    }
                    string count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + hdnf.Value + "  AND BRANCHNO= " + hdfBranchno.Value + "  AND DEGREENO=" + hdfdegreeno.Value + "AND SEMESTERNO=" + Convert.ToInt32(lblamt.ToolTip) + " AND SCHOLARSHIP_ID=" + Convert.ToInt32(lblschlType.ToolTip) + " AND  PAY_MODE_CODE='SA'  AND RECON=1 AND CAN=0");

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
                           
                            btnSubmitForSingleStu.Enabled = false;
                            chk.Checked = true;
                            chk.Enabled = false;
                             
                        }
                        else
                        {
                            //btnSubmitForSingleStu.Visible = true;
                            //btnSubmitForSingleStu.Enabled = true;
                            chk.Checked = false;
                            chk.Enabled = true;
                             
                        }
                    }                  
                }
            }

            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found,Kindly Check for Demand is Creates or Not.", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSemesterSing_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion
 
   
}