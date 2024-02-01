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

public partial class ACADEMIC_ReAdmission_Branch_Payment_Type_Change : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();
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
                   //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //Populate all the DropDownLists
                    //FillDropDown();
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
                FillDropDown();
                hdfOrganization.Value = Session["OrgId"].ToString();
                //Search Pannel Dropdown Added by Swapnil
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                ddlSearch.SelectedIndex = 0;
                ddlSearch_SelectedIndexChanged(sender, e);

                //End Search Pannel Dropdown
            }
            else
            {
                //int count = 0;
                //if (Page.Request.Params["__EVENTTARGET"] != null)
                //{
                //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                //    {
                //        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                //        bindlist(arg[0], arg[1]);
                //    }
                //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnclose"))
                //    {
                //       // divSemester.Attributes.Add("style", "display:none");
                //        divtxt.Attributes.Add("style", "display:none");
                //       // divbranch.Attributes.Add("style", "display:none");
                //    }

                //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                //    {
                //        // lblMsg.Text = string.Empty;

                //    }
                //}
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    #region SearchPannel
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        //pnltextbox.Visible = false;
                        divtxt.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        //divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                //  pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
            divtxt.Visible = false;
        }
        else
        {
            value = txtSearch.Text;
            divtxt.Visible = true;
        }

        //ddlSearch.ClearSelection();

        if (ddlSearch.SelectedValue != "0" && ddlSearch.SelectedValue != "")
        {
            if (ddlSearch.SelectedItem.Text == "IDNO" && txtSearch.Text == "")
            {
                objCommon.DisplayMessage(this, "Please Enter Search Value", this.Page);
                lblNoRecords.Text = "Total Records : 0";
            }
            else
            {
                bindlist(ddlSearch.SelectedItem.Text, value);
                ddlDropdown.ClearSelection();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select Search Criteria", this.Page);
        }
        txtSearch.Text = string.Empty;
        //if (value == "BRANCH")
        //{
        //    divbranch.Attributes.Add("style", "display:block");

        //}
        //else if (value == "SEM")
        //{
        //    divSemester.Attributes.Add("style", "display:block");
        //}
        //else
        //{
        //    divtxt.Attributes.Add("style", "display:block");
        //}

        //ShowDetails();
        this.clearcotrollers();
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

            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;

            this.student_details(idno);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region SelectedIndexChanged
    protected void ddlNewCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            chkHeadChange.Checked = false;
            objCommon.FillDropDownList(ddlNewDegree, "ACD_DEGREE D  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB  WITH (NOLOCK) ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "D.ACTIVESTATUS=1  AND COLLEGE_ID=" + ddlNewCollege.SelectedValue, "D.DEGREENO");
            this.Controlls_Visible();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlNewDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            chkHeadChange.Checked = false;
            if (rblSelection.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlNewBranch, "ACD_BRANCH B  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB  WITH (NOLOCK) ON (B.BRANCHNO=CDB.BRANCHNO)", "DISTINCT B.BRANCHNO", "LONGNAME", "B.ACTIVESTATUS=1  AND COLLEGE_ID=" + ddlNewCollege.SelectedValue + " AND DEGREENO=" + ddlNewDegree.SelectedValue, "B.BRANCHNO");
            }
            else if (rblSelection.SelectedValue == "2" || rblWithBranch.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlNewBranch, "ACD_BRANCH B  WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB  WITH (NOLOCK) ON (B.BRANCHNO=CDB.BRANCHNO)", "DISTINCT B.BRANCHNO", "LONGNAME", "B.ACTIVESTATUS=1  AND COLLEGE_ID=" + ddlNewCollege.SelectedValue + " AND DEGREENO=" + ddlNewDegree.SelectedValue + " AND B.BRANCHNO NOT IN(" + lblBranch.ToolTip + ")", "B.BRANCHNO");
            }
            this.Controlls_Visible();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlNewBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkHeadChange.Checked = false;
        if (rblSelection.SelectedValue == "2")
        {
            this.Create_Demands();
            ddlNewBranch.Enabled = false;
            ddlNewDegree.Enabled = false;
            ddlNewCollege.Enabled = false;

        }
        this.Controlls_Visible();
    }
    protected void ddlNewPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkHeadChange.Checked = false;
        this.Create_Demands();
        ddlNewBranch.Enabled = false;
        ddlNewDegree.Enabled = false;
        ddlNewCollege.Enabled = false;
        ddlNewPaymentType.Enabled = false;
    }
    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkHeadChange.Checked = false;       
        ddlNewPaymentType.SelectedIndex = -1;
        ddlNewCollege.SelectedIndex = -1;
        ddlNewDegree.SelectedIndex = -1;
        ddlNewBranch.SelectedIndex = -1;
        ddlNewBranch.Enabled = true;
        ddlNewDegree.Enabled = true;
        ddlNewCollege.Enabled = true;
        ddlNewPaymentType.Enabled = true;
        rblWithBranch.SelectedIndex = -1;
        divHeadchange.Visible = false;
        this.Controlls_Visible();
        divregno.Visible = false;
        if (rblSelection.SelectedValue == "1")
        {
            divregno.Visible = false;
            objCommon.FillDropDownList(ddlNewPaymentType, "ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
        }
        else if (rblSelection.SelectedValue == "3")
        {
            divregno.Visible = false;
            objCommon.FillDropDownList(ddlNewPaymentType, "ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND PAYTYPENO NOT IN (" + lblPaymentType.ToolTip + ")", "PAYTYPENO");
        }
        else if (rblSelection.SelectedValue == "4")
        {
            divregno.Visible = true;
            btnReport.Visible = false;
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");

        }
    }
    protected void rblWithBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkHeadChange.Checked = false;
        ddlNewPaymentType.SelectedIndex = -1;
        ddlNewCollege.SelectedIndex = -1;
        ddlNewDegree.SelectedIndex = -1;
        ddlNewBranch.SelectedIndex = -1;
        ddlNewBranch.Enabled = true;
        ddlNewCollege.Enabled = true;
        ddlNewDegree.Enabled = true;
        ddlNewPaymentType.Enabled = true;
        divHeadchange.Visible = false;
        //this.Controlls_Visible();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "rblWithBranch_change();", true);
    }
    #endregion

    #region button_ClickEvents
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FeeDemand demandCriteria = new FeeDemand();
        Student objs = new Student();
        int idno = Convert.ToInt32(ViewState["idno"]);
        objs.IdNo = idno;
        objs.AdmBatch = Convert.ToInt32(hdfAdmBatch.Value);
        objs.NewCollege_ID = Convert.ToInt32(ddlNewCollege.SelectedValue);
        objs.NewDegreeNo = Convert.ToInt32(ddlNewDegree.SelectedValue);
        objs.NewBranchNo = Convert.ToInt32(ddlNewBranch.SelectedValue);
        objs.NewPayTypeNO = rblSelection.SelectedValue == "2" ? Convert.ToInt32(hdfPaymentType.Value) : Convert.ToInt32(ddlNewPaymentType.SelectedValue);
        objs.SemesterNo = Convert.ToInt32(lblSemester.ToolTip);
        objs.Uano = Convert.ToInt32(Session["userno"]);
        objs.SessionNo = Convert.ToInt16(Session["currentsession"]);
        objs.ReceiptNo = "TF";

        objs.College_ID = Convert.ToInt32(lblCollege.ToolTip);
        objs.DegreeNo = Convert.ToInt32(lblDegree.ToolTip);
        objs.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
        objs.PayTypeNO = Convert.ToInt32(lblPaymentType.ToolTip);
        decimal Paid_Fees = Convert.ToDecimal(lblPaidAmount.Text);
        decimal excess = 0;
        //demandCriteria.ReceiptTypeCode = ddlReceiptType.SelectedValue;
        objs.CollegeCode = Session["colcode"].ToString();
        int organizationid = Convert.ToInt32(Session["OrgId"]);
        objs.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objs.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        objs.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        string ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        string remark = string.Empty;
        int chkregno = 0;
        if (chkRegno.Checked == true)
            {
            chkregno = 1;
            }
        else
            {
            chkregno = 0;
            }
        //int with_branch=Convert.ToInt32(rblWithBranch.SelectedValue);
        if (rblWithBranch.SelectedValue == "2")
        {
            objs.NewBranchNo = Convert.ToInt32(lblBranch.ToolTip);
            objs.NewDegreeNo = Convert.ToInt32(lblDegree.ToolTip);
            objs.NewCollege_ID = Convert.ToInt32(lblCollege.ToolTip);
        }
        else if (rblSelection.SelectedValue == "4")
        {

        CustomStatus cs = (CustomStatus)admCanController.UpdateReadmissionInSameBatch(idno, objs, organizationid, chkregno,ipaddress);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);              
                this.clearcotrollers();
                divregno.Visible = false;
                divSelection.Visible = true;
                btnReport.Visible = false;
                divStudInfo.Attributes.Add("style", "display:block");
                return;
            }
        }
        else
        if (!admission_change_status(idno, objs.NewCollege_ID, objs.NewDegreeNo, objs.NewBranchNo, objs.SemesterNo, objs.NewPayTypeNO))
        {
            this.clearcotrollers();
            return;
        }

        if (rblSelection.SelectedValue == "1")
        {

            if (admCanController.ReAdmissionStudent(idno, objs, remark, ipaddress, organizationid))
            {
                objCommon.DisplayMessage(this.Page, "Re-Admission done successfully.", this.Page);
                //  this.clearcotrollers();
                this.Controlls_Visible();
                divNewDemandAMounts.Visible = false;
                divSelection.Visible = true;
                divStudInfo.Attributes.Add("style", "display:block");
            }
            //string ret = admCanController.CreateDemandReadmission(idno, objs, false);
            if (chkHeadChange.Checked && organizationid == 1)
            {
                this.Save_FeeCollectionData(idno);
            }

        }
        else if (rblSelection.SelectedValue == "2")
        {
            CustomStatus cs = (CustomStatus)admCanController.Ins_ChangeBranch(objs, Paid_Fees, excess, organizationid, ipaddress);
            //  admCanController.CreateDemandBranchChange(idno, objs, false, organizationid);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Branch change done successfully.", this.Page);
                this.Controlls_Visible();
                divNewDemandAMounts.Visible = false;

            }
        }
        else if (rblSelection.SelectedValue == "3")
        {
            if (rblWithBranch.SelectedValue == "2")
            {
                objs.NewBranchNo = Convert.ToInt32(lblBranch.ToolTip);
                objs.NewDegreeNo = Convert.ToInt32(lblDegree.ToolTip);
                objs.NewCollege_ID = Convert.ToInt32(lblCollege.ToolTip);
            }
            int is_with_branch = Convert.ToInt32(rblWithBranch.SelectedValue) > 0 ? Convert.ToInt32(rblWithBranch.SelectedValue) : 0;
            //admCanController.CreateDemandPaymentTypeModification(idno, objs, false, organizationid);
            CustomStatus cs = (CustomStatus)admCanController.Ins_PaymentTypeModification(objs, organizationid, ipaddress, is_with_branch);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                this.clearcotrollers();
                objCommon.DisplayMessage(this.Page, "Payment type modification done successfully.", this.Page);
                this.Controlls_Visible();
                divNewDemandAMounts.Visible = false;
                divSelection.Visible = true;
                divStudInfo.Attributes.Add("style", "display:block");
            }

            if (chkHeadChange.Checked && organizationid == 1)
            {
                this.Save_FeeCollectionData(idno);
            }   

        }
       


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        DailyCollectionRegister dcr = new DailyCollectionRegister();
        this.ShowReport_ForCash("FeeCollectionReceiptAdjustment.rpt", Convert.ToInt32(ViewState["dcrno"].ToString()), Convert.ToInt32(ViewState["idno"]), "1");
        this.Controlls_Visible();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    #region user_definedfuncationsss
    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlNewCollege, "ACD_COLLEGE_MASTER  WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "ACTIVESTATUS=1 AND ORGANIZATIONID=" + Session["OrgId"], "COLLEGE_ID");

        }
        catch
        {
            throw;
        }
    }
    private void student_details(int idno)
    {
        ViewState["idno"] = idno;
        int Organizationid = Convert.ToInt32(Session["OrgId"]);
        DataSet ds_student = admCanController.Get_Student_Details_Readmit_Branch_Payment_type(idno, Organizationid);
        if (ds_student != null && ds_student.Tables.Count > 0 && ds_student.Tables[0].Rows.Count > 0)
        {
            lblEnroll.Text = ds_student.Tables[0].Rows[0]["ENROLLNO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["ENROLLNO"].ToString() : string.Empty;
            lblRegno.Text = ds_student.Tables[0].Rows[0]["REGNO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["REGNO"].ToString() : string.Empty;
            lblRollno.Text = ds_student.Tables[0].Rows[0]["ROLLNO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["ROLLNO"].ToString() : string.Empty;
            lblStudname.Text = ds_student.Tables[0].Rows[0]["STUDNAME"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["STUDNAME"].ToString() : string.Empty;
            lblCollege.Text = ds_student.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() : string.Empty;
            lblCollege.ToolTip = ds_student.Tables[0].Rows[0]["COLLEGE_ID"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["COLLEGE_ID"].ToString() : "0";
            lblDegree.Text = ds_student.Tables[0].Rows[0]["DEGREENAME"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["DEGREENAME"].ToString() : string.Empty;
            lblDegree.ToolTip = ds_student.Tables[0].Rows[0]["DEGREENO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["DEGREENO"].ToString() : "0";
            lblBranch.Text = ds_student.Tables[0].Rows[0]["LONGNAME"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["LONGNAME"].ToString() : string.Empty;
            lblBranch.ToolTip = ds_student.Tables[0].Rows[0]["BRANCHNO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["BRANCHNO"].ToString() : string.Empty;
            hdfbranch.Value = ds_student.Tables[0].Rows[0]["BRANCHNO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["BRANCHNO"].ToString() : string.Empty;
            lblPaymentType.Text = ds_student.Tables[0].Rows[0]["PAYTYPENAME"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["PAYTYPENAME"].ToString() : string.Empty;
            lblPaymentType.ToolTip = ds_student.Tables[0].Rows[0]["PTYPE"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["PTYPE"].ToString() : "0";
            hdfPaymentType.Value = ds_student.Tables[0].Rows[0]["PTYPE"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["PTYPE"].ToString() : "0";
            lblSemester.Text = ds_student.Tables[0].Rows[0]["SEMESTER"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["SEMESTER"].ToString() : string.Empty;
            lblSemester.ToolTip = ds_student.Tables[0].Rows[0]["SEMESTERNO"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["SEMESTERNO"].ToString() : string.Empty;
            lblAppliedAmount.Text = ds_student.Tables[0].Rows[0]["TOTAL_APPLIED_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_APPLIED_AMOUNT"].ToString() : string.Empty;
            lblPaidAmount.Text = ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() : string.Empty;
            hdfPaidAmount.Value = ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() : string.Empty;
            lbllvTotal_Amount.Text = ds_student.Tables[0].Rows[0]["TOTAL_APPLIED_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_APPLIED_AMOUNT"].ToString() : "0.00";
            lbllvPaid_Amount.Text = ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() : "0.00";
            lbllvChngetotal_amount.Text = ds_student.Tables[0].Rows[0]["TOTAL_APPLIED_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_APPLIED_AMOUNT"].ToString() : "0.00";
            lbllvchangepaid_amount.Text = ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["TOTAL_PAID_AMOUNT"].ToString() : "0.00";
            hdfAdmBatch.Value = ds_student.Tables[0].Rows[0]["ADMBATCH"].ToString() != string.Empty ? ds_student.Tables[0].Rows[0]["ADMBATCH"].ToString() : "0";

            ViewState["total_amount"] = lblAppliedAmount.Text;
            ViewState["paid_amount"] = lblPaidAmount.Text;


            divStudInfo.Attributes.Add("style", "display:block");
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            divSelection.Visible = true;
            lblNoRecords.Visible = false;
            //divNewCollege.Attributes.Add("style", "display:block");
            //divNewDegree.Attributes.Add("style", "display:block");
            //divNewBranch.Attributes.Add("style", "display:block");
            //divNewPaymentype.Attributes.Add("style", "display:block");


        }
        else
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "No data found.", this.Page);
            divSelection.Visible = false;

        }
        this.Controlls_Visible();
    }
    private void Controlls_Visible()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "rblSelection_change();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "rblWithBranch_change();", true);
    }
    private FeeHeadAmounts GetFeeItems()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in lvFeeItemsChanges.Items)
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

                    string feeAmt = ((TextBox)item.FindControl("txtChangeHeadAmount")).Text.Trim();
                    if (feeAmt != null && feeAmt != string.Empty)
                        feeAmount = Convert.ToDouble(feeAmt);

                    feeHeadAmts[feeHeadNo - 1] = feeAmount;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }
    private void Save_FeeCollectionData(int idno)
    {
        DailyCollectionRegister dcr = new DailyCollectionRegister();
        try
        {
            dcr.StudentId = idno;
            dcr.EnrollmentNo = lblEnroll.Text;
            dcr.StudentName = lblStudname.Text;
            dcr.BranchNo = rblWithBranch.SelectedValue == "1" ? Convert.ToInt32(ddlNewBranch.SelectedValue) : Convert.ToInt32(lblBranch.ToolTip);
            dcr.BranchName = lblBranch.Text;
            dcr.DegreeNo = rblWithBranch.SelectedValue == "1" ? Convert.ToInt32(ddlNewDegree.SelectedValue) : Convert.ToInt32(lblDegree.ToolTip);
            dcr.SemesterNo = Convert.ToInt32(lblSemester.ToolTip);
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
            dcr.Currency = 1;
            dcr.FeeHeadAmounts = this.GetFeeItems();
            dcr.CashAmount = (lblPaidAmount.Text.Trim() != string.Empty) ? Convert.ToDouble(lblPaidAmount.Text) : 0.00;
            dcr.ReceiptTypeCode = "TF";
            dcr.IsDeleted = false;
            dcr.CompanyCode = string.Empty;
            dcr.RpEntry = string.Empty;
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.PrintDate = DateTime.Today;
            dcr.CollegeCode = Session["colcode"].ToString();

            foreach (ListViewDataItem item in lvFeeItemsChanges.Items)
            {
                string fee_head = string.Empty;//***************
                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************
                string feeAmt = ((TextBox)item.FindControl("txtChangeHeadAmount")).Text.Trim();

                if (fee_head == "LATE FEE")
                {
                    if (feeAmt != null && feeAmt != string.Empty)
                    {
                        dcr.Late_fee = Convert.ToDouble(feeAmt);

                    }
                }
            }
            int dcrno = admCanController.SaveFeeCollection_Transaction(ref dcr);
            if (dcrno > 0)
            {
                btnReport.Enabled = true;
            }
            else
            {
                btnReport.Enabled = false;
            }

            ViewState["dcrno"] = dcrno;

        }
        catch (Exception ex)
        {
            throw;
        }
        //  return dcr;
    }
    private void clearcotrollers()
    {
        divStudInfo.Attributes.Add("style", "display:none");
        divFeeItems.Attributes.Add("style", "display:none");
        divHeadchange.Attributes.Add("style", "display:none");
        divNewBranch.Attributes.Add("style", "display:none");
        divNewCollege.Attributes.Add("style", "display:none");
        divNewDegree.Attributes.Add("style", "display:none");
        divNewPaymentype.Attributes.Add("style", "display:none");
        divSelection.Visible = false;
        rblWithBranch.Attributes.Add("style", "display:none;margin-left: 230px;");
        divButtons.Attributes.Add("style", "display:none;text-align:center");
        ddlNewBranch.SelectedIndex = -1;
        ddlNewCollege.SelectedIndex = -1;
        ddlNewDegree.SelectedIndex = -1;
        ddlNewPaymentType.SelectedIndex = -1;
        rblSelection.SelectedIndex = -1;
        rblWithBranch.SelectedIndex = -1;
        chkHeadChange.Checked = false;
        lvFeeItems.DataSource = null;
        lvFeeItems.DataBind();
        lvFeeItemsChanges.DataSource = null;
        lvFeeItemsChanges.DataBind();
        divNewDemandAMounts.Visible = false;
    }
    private bool admission_change_status(int idno, int college_id, int degreeno, int branchno, int semesterno, int ptype)
    {
        DataSet ds = admCanController.Get_Admission_Activity_status(idno, college_id, degreeno, branchno, semesterno, ptype);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ADMISSION_STATUS"].ToString() == "1" && rblSelection.SelectedValue != "1")
            {
                objCommon.DisplayMessage(this.Page, "Selected student admission was cancelled.", this.Page);
                return false;
            }

            if (rblSelection.SelectedValue == "1" && ds.Tables[0].Rows[0]["READMISSION_STATUS"].ToString() == "1")
            {
                objCommon.DisplayMessage(this.Page, "Selected student re-admission already done.", this.Page);
                return false;
            }
            else if (rblSelection.SelectedValue == "2" && ds.Tables[0].Rows[0]["BRANCH_STATUS"].ToString() == "1")
            {
                objCommon.DisplayMessage(this.Page, "Selected student branch change already done.", this.Page);
                return false;
            }
            else if (rblSelection.SelectedValue == "3" && ds.Tables[0].Rows[0]["PTYPE_STATUS"].ToString() == "1")
            {
                if (rblWithBranch.SelectedValue == "1" && ds.Tables[0].Rows[0]["PTYPE_STATUS"].ToString() == "1" && ds.Tables[0].Rows[0]["BRANCH_STATUS"].ToString() == "1")
                {
                    objCommon.DisplayMessage(this.Page, "Selected student payment type & branch change already done.", this.Page);
                    return false;
                }
                else if (rblWithBranch.SelectedValue == "2" && ds.Tables[0].Rows[0]["PTYPE_STATUS"].ToString() == "1")
                {
                    objCommon.DisplayMessage(this.Page, "Selected student payment type modification already done.", this.Page);
                    return false;
                }
            }
        }

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows[0]["STANDARD_FEE_STATUS"].ToString() == "0" || ds.Tables[1].Rows[0]["STANDARD_FEE_STATUS"].ToString() == "")
            {
                objCommon.DisplayMessage(this.Page, "Standard fees not found for this selection.", this.Page);
                return false;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Standard fees not found for this selection.", this.Page);
            return false;
        }
        return true;
    }
    private void ShowReport_ForCash(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, copyNo) + ",username=" + Session["username"].ToString();
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt";
        return param;

    }
    private void Create_Demands()
    {
        FeeDemand demandCriteria = new FeeDemand();
        Student objs = new Student();
        int idno = Convert.ToInt32(ViewState["idno"]);
        objs.IdNo = idno;
        objs.AdmBatch = Convert.ToInt32(hdfAdmBatch.Value);
        objs.NewCollege_ID = Convert.ToInt32(ddlNewCollege.SelectedValue);
        objs.NewDegreeNo = Convert.ToInt32(ddlNewDegree.SelectedValue);
        objs.NewBranchNo = Convert.ToInt32(ddlNewBranch.SelectedValue);
        objs.NewPayTypeNO = rblSelection.SelectedValue == "2" ? Convert.ToInt32(hdfPaymentType.Value) : Convert.ToInt32(ddlNewPaymentType.SelectedValue);
        objs.SemesterNo = Convert.ToInt32(lblSemester.ToolTip);
        objs.Uano = Convert.ToInt32(Session["userno"]);
        objs.SessionNo = Convert.ToInt16(Session["currentsession"]);
        objs.ReceiptNo = "TF";

        objs.College_ID = Convert.ToInt32(lblCollege.ToolTip);
        objs.DegreeNo = Convert.ToInt32(lblDegree.ToolTip);
        objs.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
        objs.PayTypeNO = Convert.ToInt32(lblPaymentType.ToolTip);
        decimal Paid_Fees = Convert.ToDecimal(lblPaidAmount.Text);
        decimal excess = 0;
        //demandCriteria.ReceiptTypeCode = ddlReceiptType.SelectedValue;
        objs.CollegeCode = Session["colcode"].ToString();
        int organizationid = Convert.ToInt32(Session["OrgId"]);

        string ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        string remark = string.Empty;
        //int with_branch=Convert.ToInt32(rblWithBranch.SelectedValue);
        if (rblWithBranch.SelectedValue == "2")
        {
            objs.NewBranchNo = Convert.ToInt32(lblBranch.ToolTip);
            objs.NewDegreeNo = Convert.ToInt32(lblDegree.ToolTip);
            objs.NewCollege_ID = Convert.ToInt32(lblCollege.ToolTip);
        }

        if (!admission_change_status(idno, objs.NewCollege_ID, objs.NewDegreeNo, objs.NewBranchNo, objs.SemesterNo, objs.NewPayTypeNO))
        {
            this.clearcotrollers();
            return;
        }

        if (rblSelection.SelectedValue == "1")
        {
            string ret = admCanController.CreateDemandReadmission(idno, objs, false);
        }
        else if (rblSelection.SelectedValue == "2")
        {
            admCanController.CreateDemandBranchChange(idno, objs, false, organizationid);
        }
        else if (rblSelection.SelectedValue == "3")
        {
            if (rblWithBranch.SelectedValue == "2")
            {
                objs.NewBranchNo = Convert.ToInt32(lblBranch.ToolTip);
                objs.NewDegreeNo = Convert.ToInt32(lblDegree.ToolTip);
                objs.NewCollege_ID = Convert.ToInt32(lblCollege.ToolTip);
            }

            admCanController.CreateDemandPaymentTypeModification(idno, objs, false, organizationid);
        }

        int status = 0;
        DataSet ds_payment_details = admCanController.Get_Payment_Details(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(idno), Convert.ToInt32(lblSemester.ToolTip), "TF", 0, 1, Convert.ToInt32(lblPaymentType.ToolTip), ref status);
        //(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(idno), Convert.ToInt32(lblSemester.ToolTip), "TF", 0, 1, Convert.ToInt16(lblPaymentType.ToolTip), ref 0);
        if (ds_payment_details != null && ds_payment_details.Tables.Count > 0 && ds_payment_details.Tables[0].Rows.Count > 0)
        {
            lvFeeItems.DataSource = ds_payment_details;
            lvFeeItems.DataBind();
            divFeeItems.Visible = true;

            lvFeeItemsChanges.DataSource = ds_payment_details;
            lvFeeItemsChanges.DataBind();
        }
        divHeadchange.Visible = true;
        lblNewTotal_Amount.Text = lbllvTotal_Amount.Text;
        lblNewPaid_Amount.Text = lbllvPaid_Amount.Text;
        divNewDemandAMounts.Visible = true;
        divNewDemandAMounts.Attributes.Add("style", "display:block");
        this.Controlls_Visible();
    }
    #endregion
}