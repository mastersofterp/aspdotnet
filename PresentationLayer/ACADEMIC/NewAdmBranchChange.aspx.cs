

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using SendGrid;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using ClosedXML.Excel;


public partial class ACADEMIC_NewAdmBranchChange : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        if (!Page.IsPostBack)
        {
            pnltextbox.Visible = false;
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    //lblSession.Text = Session["sessionname"].ToString();
                    PopulateDropDown();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                // btnPrint.Enabled = false;
                btnSubmit.Enabled = false;

                search();
            }

            ddlBranch.Attributes.Add("onChange", "return ShowConfirm(this);");
        }

        divMsg.InnerHtml = string.Empty;
        divParmenter.Visible = false;
        divAdmBatch.Visible = false;
        divAcdYear.Visible = false;
        btnexcelBranchChange.Visible = false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        #region commented code by shailendra K on dated 06.08.2022

        //pnlBranchChange.Visible = false;
        //btnSubmit.Visible = false;
        //btnCancel.Visible = false;
        //divMsg.InnerHtml = string.Empty;
        //rdWithFee.Checked = false;
        //rdWithoutFee.Checked = false;
        //rdWithFee.Enabled = true;
        //rdWithoutFee.Enabled = true;
        //lblNote.Visible = false;
        //ddlBranch.Enabled = true;
        //lblCurrentfeess.Text = "0";
        //lblPaidFees.Text = "0";
        //lblNewBranchFee.Text = "0";
        //lblExcessAmt.Text = "0";
        //ViewState["IS_DEMAND_CREATED"] = null;
        //if (string.IsNullOrEmpty(txtStudent.Text.Trim()))    // == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        //{
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    lblMsg.Text = "Please Enter Proper Reg. No.";
        //    txtStudent.Focus();
        //    return;
        //}
        //lblMsg.Text = string.Empty;

        //try
        //{
        //    StudentRegistration objSRegist = new StudentRegistration();
        //    StudentController objSC = new StudentController();
        //    string idno = "0";
        //    // string category = "";
        //    string admcan = "0";
        //    decimal paidfees = 0;

        //    DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(txtStudent.Text.Trim(), ddlSearch.SelectedItem.Text);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        idno = ds.Tables[0].Rows[0]["IDNO"].ToString();

        //    // idno = objCommon.LookUp("acd_student", "idno", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
        //    //objCommon.LookUp("acd_student", "isnull(admcan,0)", "IDNO='" + idno + "' AND CAN=0 AND ADMCAN=0");

        //    if (idno == string.Empty)
        //    {
        //        lblName.Text = string.Empty;
        //        lblMsg.ForeColor = System.Drawing.Color.Red;
        //        lblMsg.Text = "Student Not Found!!";
        //        this.ClearControls();
        //        btnSubmit.Enabled = false;
        //        return;
        //    }

        //    if (idno != string.Empty)
        //    {
        //        admcan = ds.Tables[0].Rows[0]["ADMCANCEL"].ToString();
        //        if (admcan == "CANCELLED")
        //        {
        //            objCommon.DisplayMessage(this.Page, "Entered student admission has been cancelled!", this.Page);
        //            txtStudent.Text = "";
        //            txtStudent.Focus();
        //            return;
        //        }

        //        int RecExists = Convert.ToInt32(objCommon.LookUp("ACD_BRCHANGE", "COUNT(IDNO)", "IDNO = (SELECT IDNO FROM ACD_STUDENT WHERE IDNO = '" + idno + "')"));
        //        if (RecExists > 0)
        //        {
        //            objCommon.DisplayMessage(this.Page, "Already Requested for Branch Change.", this.Page);
        //            return;
        //        }

        //        ViewState["idno"] = idno.ToString();
        //        imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
        //        DataTableReader dtr = objSRegist.GetStudentDetails(Convert.ToInt32(idno));

        //        if (dtr.Read())
        //        {
        //            Session["idno"] = dtr["idno"].ToString();
        //            lblName.Text = dtr["STUDNAME"].ToString();
        //            lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString(); ;
        //            lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
        //            ViewState["OldRollNo"] = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
        //            lblRollNo.Text = ViewState["OldRollNo"].ToString();
        //            lblBranch.Text = dtr["LONGNAME"].ToString();
        //            lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
        //            lblDegree.Text = dtr["DEGREENAME"].ToString();
        //            lblDegree.ToolTip = dtr["DEGREENO"].ToString();

        //            lblColg.Text = dtr["COLLEGE_NAME"].ToString();
        //            lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();

        //            ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
        //            ViewState["degreeNo"] = dtr["DEGREENO"].ToString();

        //            ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
        //            ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
        //            ViewState["BranchNoOld"] = dtr["BRANCHNO"].ToString();

        //            btnSubmit.Enabled = true;
        //            divdata.Visible = true;

        //            //New added on 2020Nov20
        //            pnlBranchChange.Visible = false;
        //            btnSubmit.Visible = true;
        //            btnCancel.Visible = true;
        //            lblNote.Visible = false;
        //            lblMsg.Text = string.Empty;
        //            ddlBranch.Enabled = true;

        //            imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["idno"].ToString() + "&type=student";

        //            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "ISNULL(ActiveStatus,0)= 1 AND COLLEGE_ID > 0", "COLLEGE_ID");
        //            lblCurrentfeess.Text = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");
        //            lblPaidFees.Text = objCommon.LookUp("ACD_DCR", "SUM(ISNULL(TOTAL_AMT,0))", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

        //            if (lblPaidFees.Text.Equals(string.Empty))
        //                lblPaidFees.Text = Convert.ToString(paidfees);

        //            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND A.BRANCHNO !=" + ViewState["BranchNoOld"].ToString(), "A.LONGNAME");
        //        }
        //        else
        //        {
        //            //lblRegNo.Text = string.Empty;
        //            btnSubmit.Enabled = false;
        //        }
        //        dtr.Close();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}

        #endregion

        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            value = ddlDropdown.SelectedValue;
        else
            value = txtStudent.Text;

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtStudent.Text = string.Empty;
        divdata.Visible = false;
    }

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
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
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
            ddlSearch.SelectedIndex = 1;
            txtStudent.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void ShowStudentDetails(int idno)
    //{
    //    try
    //    {
    //        StudentRegistration objSRegist = new StudentRegistration();
    //        DataTableReader dtr = objSRegist.GetStudentDetails(idno);

    //        if (dtr.Read())
    //        {
    //            lblName.Text = dtr["STUDNAME"].ToString();
    //            lblRegNo.Text = dtr["REGNO"].ToString();
    //            lblRollNo.Text = dtr["ROLLNO"].ToString();
    //            lblBranch.Text = dtr["LONGNAME"].ToString();
    //            lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
    //            imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
    //        }
    //        dtr.Close();

    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReport("ACADEMIC_BranchChange", "AdmissionSlip.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_IDNO=" + Session["idno"] + ",@P_ADMBATCH=" + ViewState["lblAdmbatch"].ToString() + ",@Year=" + ViewState["lblYear"].ToString() + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1"));
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    public byte[] GetImageDataForDocumentation(FileUpload fu)
    {
        if (fu.HasFile)
        {
            int ImageSize = fu.PostedFile.ContentLength;
            Stream ImageStream = fu.PostedFile.InputStream as Stream;
            byte[] ImageContent = new byte[ImageSize];
            int intStatus = ImageStream.Read(ImageContent, 0, ImageSize);
            //ImageStream.Close();
            // ImageStream.Dispose();
            return ImageContent;
        }
        else
        {
            FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
            int ImageSize = (int)ff.Length;
            byte[] ImageContent = new byte[ff.Length];
            ff.Read(ImageContent, 0, ImageSize);
            ff.Close();
            ff.Dispose();
            return ImageContent;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //**********LOGIC ADDED BY: M. REHBAR SHEIKH ON 11-07-2019 | SAVE FOR NEW BRANCH***********
        //if (!rdWithoutFee.Checked && !rdWithFee.Checked)//&& !rdBranchChange.Checked)
        //{
        //    ShowMessage("Branch change with fees collection Or without fees collection? Please specify with radio button selection.");
        //    return;
        //}

        if (ddlBranch.SelectedIndex == 0)
        {
            ShowMessage("Please select new branch selection.");
            ddlBranch.Focus();
            return;
        }

        if (txtRemark.Text == string.Empty)
        {
            ShowMessage("Please enter some remarks.");
            txtRemark.Focus();
            return;
        }
        string NewRollNo = "", OldRollNo = "";
        decimal paidfees = 0, excessamts = 0;
        StudentController objSController = new StudentController();
        BranchController objBranch = new BranchController();
        Student objStudent = new Student();
        Common objcommon = new Common();

        if (Convert.ToString(ViewState["New_RollNo"]).Equals(string.Empty))
            NewRollNo = "";
        else
            NewRollNo = ViewState["New_RollNo"].ToString();


        if (string.IsNullOrEmpty(ViewState["OldRollNo"].ToString()))
            OldRollNo = "";
        else
            OldRollNo = ViewState["OldRollNo"].ToString();

        StudentRegistration objSRegist = new StudentRegistration();
        DataTableReader dtr = objSRegist.GetStudentDetails(Convert.ToInt32(Session["idno"].ToString()));

        if (dtr.Read())
        {
            lblName.Text = dtr["STUDNAME"].ToString();
            lblRegNo.Text = dtr["REGNO"].ToString();
            lblBranch.Text = dtr["LONGNAME"].ToString();
            lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
            lblDegree.Text = dtr["DEGREENAME"].ToString();
            lblDegree.ToolTip = dtr["DEGREENO"].ToString();
            lblColg.Text = dtr["COLLEGE_NAME"].ToString();
            lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();
        }
        dtr.Close();

        objStudent.IdNo = Convert.ToInt32(Session["idno"].ToString());
        objStudent.BranchNo = Convert.ToInt32(lblBranch.ToolTip); // old branch no
        objStudent.StudName = lblName.Text;
        objStudent.NewBranchNo = Convert.ToInt32(ddlBranch.SelectedValue); // new branch no
        //Added by Rita M.....
        objStudent.DegreeNo = Convert.ToInt32(lblDegree.ToolTip);// old degree
        objStudent.NewDegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);//new degree....
        objStudent.College_ID = Convert.ToInt32(lblDegree.ToolTip);
        objStudent.NewCollege_ID = Convert.ToInt32(ddlCollege.SelectedValue); //new College....
        objStudent.Remark = txtRemark.Text.Trim();
        objStudent.CollegeCode = Session["colcode"].ToString();
        objStudent.RegNo = txtStudent.Text.Trim();
        int userno = Convert.ToInt32(Session["userno"]);

        if (lblPaidFees.Text.Equals(string.Empty))
            paidfees = 0;
        else
            paidfees = Convert.ToDecimal(lblPaidFees.Text);

        if (lblExcessAmt.Text.Equals(string.Empty))
            excessamts = 0;
        else
            excessamts = Convert.ToDecimal(lblExcessAmt.Text);

        int Checkbox = 0;
        if (chkRegno.Checked == true)
        {
            Checkbox = 1;
        }
        else
        {
            Checkbox = 0;
        }

        CustomStatus cs = (CustomStatus)objBranch.ChangeBranch_NewStudent(objStudent, NewRollNo, OldRollNo, userno, paidfees, excessamts, Checkbox);


        DemandModificationController dmController = new DemandModificationController();
        FeeDemand demandCriteria = new FeeDemand();

        DataSet newFees = objCommon.FillDropDown("ACD_DEMAND", "*", string.Empty, "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(lblBranch.ToolTip) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + " AND RECIEPT_CODE='TF'", string.Empty);
        if (newFees != null && newFees.Tables[0].Rows.Count > 0)
        {
            demandCriteria.SessionNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
            demandCriteria.ReceiptTypeCode = newFees.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            demandCriteria.BranchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["BRANCHNO"]);
            demandCriteria.SemesterNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
            demandCriteria.PaymentTypeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["PAYTYPENO"]);
            demandCriteria.UserNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["UA_NO"]);
            demandCriteria.CollegeCode = newFees.Tables[0].Rows[0]["COLLEGE_CODE"].ToString();
            //Commented by Rita M.....
            demandCriteria.DegreeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
            demandCriteria.AdmBatchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCHNO"]);
        }

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            //if (rdWithoutFee.Checked)
            //{
                string response2 = dmController.CancelDemandForSelectedStudent_ByBranchChange_WithoutFees(Convert.ToInt32(Session["idno"]), demandCriteria);
            //}
            objCommon.DisplayMessage(UpdatePanel1, "Branch changed successfully.", this.Page);
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Branch Change Process Completed Successfully!";
            divdata.Visible = false;
            ClearControls();
            btnShow.Enabled = true;
        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            //if (rdWithoutFee.Checked)
            //{
                string response2 = dmController.CancelDemandForSelectedStudent_ByBranchChange_WithoutFees(Convert.ToInt32(Session["idno"]), demandCriteria);
            //}
            objCommon.DisplayMessage(UpdatePanel1, "Branch changed successfully. And Student with new generated Roll No. already exist.", this.Page);
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Branch changed successfully. And Student with new generated Roll No. already exist.";
            divdata.Visible = false;
            btnShow.Enabled = true;
            ClearControls();
            ddlSearch.Focus();
        }
        else
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Error...";
            divdata.Visible = false;
        }
    }

    private void ClearControls()
    {
        lblName.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblRollNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        txtStudent.Text = string.Empty;
        txtNewRegNo.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        lblNewBranchFee.Text = "0";
        lblPaidFees.Text = "0";
        lblExcessAmt.Text = "0";
        lblCurrentfeess.Text = "0";
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        lblNote.Visible = false;
        lblDegree.Text = string.Empty;
        lblColg.Text = string.Empty;
        chkRegno.Checked = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //**********LOGIC ADDED BY: M. REHBAR SHEIKH ON 11-07-2019 | CREATE NEW DEMAND FOR NEW BRANCH***********

        Student objS = new Student();
        StudentController objSC = new StudentController();

        int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + Convert.ToInt32(Session["idno"])));
        int PAYTYPE = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO=" + Convert.ToInt32(Session["idno"])));

        DataSet dsstandardfees = objSC.GetStandardFeesDetails(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), "TF", Convert.ToInt32(admbatch), Convert.ToInt32(PAYTYPE), Convert.ToInt32(Session["OrgId"]));
        int Count = 0;
        if (dsstandardfees.Tables[0].Rows.Count > 0)
        {
            Count = Convert.ToInt32(dsstandardfees.Tables[0].Rows[0]["COUNT"].ToString());
            //objCommon.DisplayMessage(this.Page, "Standard Fees is Not Defined For Selected ", this.Page);
        }
        if (Count == 0)
        {
            objCommon.DisplayMessage(this.Page, "Standard Fees is Not Defined Please Define Standard Fees First.", this.Page);
            return;
        }

        int colgId = 0;
        int Demand_Count = 0;
        bool Demand_Overwrite = false;
        //if (!rdWithoutFee.Checked && !rdWithFee.Checked)// && !rdBranchChange.Checked)
        //{
        //    ShowMessage("Branch change with fees collection Or without fees collection? Please specify with radio button selection.");
        //    ddlBranch.SelectedIndex = 0;
        //    return;
        //}
        ViewState["IS_DEMAND_CREATED"] = null;
        ViewState["New_RollNo"] = null;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        int branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        int newdegreeno = Convert.ToInt32(ddlDegree.SelectedValue);

        StudentController objsc = new StudentController();

        if (lblRollNo.Text.Equals(string.Empty))
        {
            ViewState["New_RollNo"] = null;
        }
        else
        {
            //Commented by Deepali on 15/09/2020
            //txtNewRegNo.Text = objsc.GetNewRollNoForBranchChange(Convert.ToInt32(ViewState["batchNo"]), Convert.ToInt32(ViewState["degreeNo"]), Convert.ToInt32(lblBranch.ToolTip), Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(branchNo), 1, Convert.ToInt32(Session["idno"]));
            //ViewState["New_RollNo"] = txtNewRegNo.Text;
        }


        int isDemand = 0;
        isDemand = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(ISNULL(IDNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeNo"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + ""));

        if (ddlBranch.SelectedValue == "0")
            txtNewRegNo.Text = string.Empty;

        //if (rdWithFee.Checked)
        //{
        DemandModificationController dmController = new DemandModificationController();
        FeeDemand demandCriteria = new FeeDemand();

        DataSet newFees = objCommon.FillDropDown("ACD_DEMAND", "*", string.Empty, "IDNO=" + Convert.ToInt32(Session["idno"]) +" AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "", string.Empty);
        if (newFees != null && newFees.Tables[0].Rows.Count > 0)
        {
            demandCriteria.SessionNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
            demandCriteria.ReceiptTypeCode = newFees.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            demandCriteria.BranchNo = branchNo;
            demandCriteria.SemesterNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
            demandCriteria.PaymentTypeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["PAYTYPENO"]);
            demandCriteria.UserNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["UA_NO"]);
            demandCriteria.CollegeCode = newFees.Tables[0].Rows[0]["COLLEGE_CODE"].ToString();
            //Commented by Rita M.....
            demandCriteria.DegreeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
            //demandCriteria.NewDegreeNo = newdegreeno;
            demandCriteria.AdmBatchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCHNO"]);

            //FOR ROLLBACK
            ViewState["SessionNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
            ViewState["ReceiptTypeCode_RollB"] = newFees.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            ViewState["BranchNo_RollB"] = Convert.ToInt32(lblBranch.ToolTip);
            ViewState["NewBranchNo_RollB"] = branchNo;
            ViewState["SemesterNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
            ViewState["PaymentTypeNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["PAYTYPENO"]);
            ViewState["UserNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["UA_NO"]);
            ViewState["CollegeCode_RollB"] = newFees.Tables[0].Rows[0]["COLLEGE_CODE"].ToString();
            ViewState["DegreeNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
            // ViewState["DegreeNo_RollB"] = degreeno;
            ViewState["AdmBatchNo_RollB"] = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCHNO"]);

        }
        colgId = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "COLLEGE_ID", " BRANCHNO=" + Convert.ToInt32(branchNo) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ""));

        // START ********************ADDED BY ROHIT KUAMR TIWARI ON DATE 26-JULY -2019 *****************************
        Demand_Count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1) DEMAND_COUNT", " SESSIONNO=" + demandCriteria.SessionNo + "  AND RECIEPT_CODE='" + demandCriteria.ReceiptTypeCode + "'  AND BRANCHNO = " + demandCriteria.BranchNo + "    AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + demandCriteria.SemesterNo + " AND PAYTYPENO = " + demandCriteria.PaymentTypeNo + " AND UA_NO=" + demandCriteria.UserNo + "  AND IDNO =" + Convert.ToInt32(Session["idno"]) + ""));
        Demand_Overwrite = Demand_Count > 0 ? true : false;

        // END **************

        string response = dmController.CreateDemandForSelectedStudents_ByBranchChange(Session["idno"].ToString(), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), Demand_Overwrite, Convert.ToInt32(colgId), Convert.ToInt32(ddlDegree.SelectedValue));
        if (response != "-99")
        {
            if (response.Length > 2)
                ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
            else if (response == "1")
            {
                lblNewBranchFee.Visible = true;
                //lblNewBranchFee.Text = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + "AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "")).ToString();

                lblNewBranchFee.Text = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND BRANCHNO=" + Convert.ToInt32(branchNo) + "AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"]) + "")).ToString();


                decimal paidFee = 0;
                decimal newFee = 0;
                decimal excessAmt = 0;

                paidFee = Convert.ToDecimal(lblPaidFees.Text);
                newFee = Convert.ToDecimal(lblNewBranchFee.Text);

                if (paidFee > newFee)
                {
                    excessAmt = paidFee - newFee;
                    lblExcessAmt.Text = excessAmt.ToString();
                }

                string response2 = dmController.CancelDemandForSelectedStudent_ByBranchChange(Convert.ToInt32(Session["idno"]), demandCriteria, Convert.ToInt32(ViewState["semesterNo"]), false, Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(lblBranch.ToolTip), branchNo, Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(newdegreeno), excessAmt);
                if (response == "1" && response2 == "2")
                {
                    ViewState["IS_DEMAND_CREATED"] = "YES";
                    rdWithFee.Enabled = false;
                    rdWithoutFee.Enabled = false;
                    lblNote.Visible = true;
                    btnShow.Enabled = false;
                    ddlBranch.Enabled = false;
                    ShowMessage("Demand sucessfully created. Please click on submit for change branch.");
                }
            }
        }
        else
            ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
        // }
    }
    protected void rdWithoutFee_CheckedChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        ddlBranch.Enabled = true;
        ddlDegree.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        divnewbranchfee.Visible = dvCurrentBranchFees.Visible = dvNewRollNo.Visible = dvPaidFees.Visible = true;
    }

    protected void rdWithFee_CheckedChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        ddlBranch.Enabled = true;
        ddlDegree.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        divnewbranchfee.Visible = dvCurrentBranchFees.Visible = dvNewRollNo.Visible = dvPaidFees.Visible = true;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string College_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Session["idno"].ToString());
            string Degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Session["idno"].ToString());
            if (College_id == ddlCollege.SelectedValue && Degreeno==ddlDegree.SelectedValue)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND A.BRANCHNO !=" + ViewState["BranchNoOld"].ToString() + "AND B.COLLEGE_ID =" + ddlCollege.SelectedValue, "A.LONGNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.COLLEGE_ID =" + ddlCollege.SelectedValue, "A.LONGNAME");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void TransferToEmail(string studname, string Regno, string oldbranch, string newbranch, string remark)
    {
        try
        {
            int ret = 0;
            string useremail = "";
            if (lblColg.ToolTip == "1" && lblBranch.ToolTip == "8")
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=6");
            else if (lblColg.ToolTip == "2")
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=7");
            else if (lblColg.ToolTip == "4")
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=8");
            else
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=1");

            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                msg.From = new MailAddress(fromAddress, "ABBS - Branch Change");
                msg.To.Add(new MailAddress(useremail));
                msg.Subject = "Regarding Branch Change Approval";
                //FOR MANISH : ERR: AT HTML TAGS :
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                const string EmailTemplate = "<html><body>" +
                                         "<div align=\"center\">" +
                                         "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                          "<tr>" +
                                          "<td>" + "</tr>" +
                                          "<tr>" +
                                         "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                         "</tr>" +
                                         "<tr>" +
                                         "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b><br/></td>" +
                                         "</tr>" +
                                         "</table>" +
                                         "</div>" +
                                         "</body></html>";
                StringBuilder mailBody = new StringBuilder();
                //  mailBody.AppendFormat("<h1>Greetings !!</h1>");
                mailBody.AppendFormat("Dear Sir/Madam <b>" + "" + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Below Student has opted for a Program change that required your approval with Comments.");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>Student Details </b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Reg. No. : </b> " + Regno + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Name : </b>" + studname + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicantion Date :</b> " + DateTime.Now.ToString("dd/MM/yyyy") + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> College Name  : </b>" + lblColg.Text + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Department  : </b>" + lblDegree.Text + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Currenct Program : </b>" + oldbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> New Program  : </b>" + newbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>New College Name  : </b>" + lblColg.Text + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                //  mailBody.AppendFormat("<b>Your new Login Password is </b>");
                mailBody.AppendFormat("<b>Comments :" + remark + " </b>");
                mailBody.AppendFormat("<br />");

                string Mailbody = mailBody.ToString();
                string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                msg.IsBodyHtml = true;
                msg.Body = nMailbody;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
                smtp.Send(msg);

                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                    ret = 1;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Session["usertype"].ToString() != "1")
        //{
        //    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
        //}
        //else
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
    }
    //protected void rdBranchChange_CheckedChanged(object sender, EventArgs e)
    //{
    //    pnlBranchChange.Visible = true;
    //    divnewbranchfee.Visible = dvCurrentBranchFees.Visible = dvNewRollNo.Visible = dvPaidFees.Visible = false;
    //}
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divdata.Visible = false;
            ClearControls();
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

        divdata.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        ShowDetails(Convert.ToInt32(lnk.CommandArgument));

        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        ddlBranch.Enabled = true;
        ddlDegree.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        divnewbranchfee.Visible = dvCurrentBranchFees.Visible = dvNewRollNo.Visible = dvPaidFees.Visible = true;
    }

    private void ShowDetails(int idno = 0)
    {
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        divMsg.InnerHtml = string.Empty;
        rdWithFee.Checked = false;
        rdWithoutFee.Checked = false;
        rdWithFee.Enabled = true;
        //rdWithoutFee.Enabled = true;
        lblNote.Visible = false;
        ddlBranch.Enabled = true;
        lblCurrentfeess.Text = "0";
        lblPaidFees.Text = "0";
        lblNewBranchFee.Text = "0";
        lblExcessAmt.Text = "0";
        ViewState["IS_DEMAND_CREATED"] = null;
        pnlLV.Visible = false;
        try
        {
            //idno = Convert.ToInt32(Session["idno"]);
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string admcan = "0";
            decimal paidfees = 0;

            if (idno > 0)
            {
                admcan = objCommon.LookUp("acd_student", "isnull(admcan,0)", "idno='" + idno + "' AND CAN=0 AND ADMCAN=0");
                if (admcan == "1")
                {
                    objCommon.DisplayMessage(this.Page, "Entered student admission has been cancelled!", this.Page);
                    txtStudent.Text = "";
                    txtStudent.Focus();
                    return;
                }

                int RecExists = Convert.ToInt32(objCommon.LookUp("ACD_BRCHANGE", "COUNT(IDNO)", "IDNO = (SELECT IDNO FROM ACD_STUDENT WHERE IDNO = '" + idno + "')"));
                //if (RecExists > 0)
                //{
                //    objCommon.DisplayMessage(this.Page, "Already Requested for Branch Change.", this.Page);
                //    divdata.Visible = false;
                //    lblMsg.Text = string.Empty;
                //    return;
                //}

                ViewState["idno"] = idno.ToString();
                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
                DataTableReader dtr = objSRegist.GetStudentDetails(idno);

                if (dtr.Read())
                {
                    Session["idno"] = dtr["idno"].ToString();
                    lblName.Text = dtr["STUDNAME"].ToString();
                    lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString(); ;
                    lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                    ViewState["OldRollNo"] = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                    lblRollNo.Text = ViewState["OldRollNo"].ToString();
                    lblBranch.Text = dtr["LONGNAME"].ToString();
                    lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                    lblDegree.Text = dtr["DEGREENAME"].ToString();
                    lblDegree.ToolTip = dtr["DEGREENO"].ToString();

                    lblColg.Text = dtr["COLLEGE_NAME"].ToString();
                    lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();

                    ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
                    ViewState["degreeNo"] = dtr["DEGREENO"].ToString();

                    ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
                    ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                    ViewState["BranchNoOld"] = dtr["BRANCHNO"].ToString();

                    btnSubmit.Enabled = true;
                    divdata.Visible = true;

                    //New added on 2020Nov20
                    pnlBranchChange.Visible = false;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    lblNote.Visible = false;
                    lblMsg.Text = string.Empty;
                    ddlBranch.Enabled = true;

                    imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["idno"].ToString() + "&type=student";

                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "ISNULL(ActiveStatus,0)= 1 AND COLLEGE_ID > 0", "COLLEGE_ID");
                    lblCurrentfeess.Text = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");
                    lblPaidFees.Text = objCommon.LookUp("ACD_DCR", "SUM(ISNULL(TOTAL_AMT,0))", "IDNO=" + Convert.ToInt32(idno) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

                    if (lblPaidFees.Text.Equals(string.Empty))
                        lblPaidFees.Text = Convert.ToString(paidfees);

                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND A.BRANCHNO !=" + ViewState["BranchNoOld"].ToString(), "A.LONGNAME");
                }
                else
                    btnSubmit.Enabled = false;

                dtr.Close();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //protected void chkRegno_CheckedChanged(object sender, EventArgs e)
    //    {
    //    DemandModificationController dmController = new DemandModificationController();
    //    FeeDemand demandCriteria = new FeeDemand();
    //    int Idno=0;
    //    int OrgID = 0;
    //    string Paymodecode=string.Empty;
    //    string Paytype=string.Empty;
    //    string Receiptcode=string.Empty;
    //    int Semesterno=0;
    //    if (chkRegno.Checked == true)
    //        {

    //        Idno = Convert.ToInt32(Session["stuinfoidno"]);
    //        OrgID = Convert.ToInt32(Session["OrgId"]);
    //        Paymodecode = "C";
    //        Paytype = "C";
    //        Receiptcode = "TF";

    //        string Semester = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Session["IDNO"].ToString());
    //        CustomStatus cs = (CustomStatus)dmController.CreateRegNoonBranchchange(Idno, OrgID, Paymodecode, Paytype, Receiptcode, Convert.ToInt32(Semester));
    //        if (cs.Equals(CustomStatus.RecordUpdated))
    //            {

    //            objCommon.DisplayMessage(UpdatePanel1, "Registration Number Generate successfully.", this.Page);
    //            ShowDetails();
    //            }


    //        }
    //    }
    public void search()
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

    //added by nehal on 04/04/23
    protected void lnkNote_Click(object sender, EventArgs e)
    {
        divParmenter.Visible = true;
        btnexcelBranchChange.Visible = true;
    }
    protected void rdoAdmBatch_CheckedChanged(object sender, EventArgs e)
    {
        divParmenter.Visible = true;
        divAdmBatch.Visible = true;
        divAcdYear.Visible = false;
        btnexcelBranchChange.Visible = true;
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
    }
    protected void rdoAcdYear_CheckedChanged(object sender, EventArgs e)
    {
        divParmenter.Visible = true;
        divAdmBatch.Visible = false;
        divAcdYear.Visible = true;
        btnexcelBranchChange.Visible = true;
        objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
    }
    protected void btnexcelBranchChange_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        if (rdoAdmBatch.Checked == true || rdoAcdYear.Checked == true)
        {
            divParmenter.Visible = true;
            btnexcelBranchChange.Visible = true;
            int AcdYear = Convert.ToInt32(ddlAcdYear.SelectedValue);
            int AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);

            if (rdoAdmBatch.Checked == true)
            {
                AcdYear = 0;
                ddlAcdYear.SelectedValue = "0";
                if (ddlAdmBatch.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(updEdit, "Please Select Admission Batch.", this.Page);
                    ddlAcdYear.Focus();
                    return;
                }
                else
                {
                    DataSet ds = objSC.GetBranchChangeReportExcel(AcdYear, AdmBatch);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        ds.Tables[0].TableName = "Outstanding Report RecieptWise";
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            foreach (System.Data.DataTable dt in ds.Tables)
                            {
                                //Add System.Data.DataTable as Worksheet.
                                if (dt != null && dt.Rows.Count > 0)
                                    wb.Worksheets.Add(dt);
                            }
                            //Export the Excel file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename= Branch Change Report.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updEdit, "No Record Found", this.Page);
                        return;
                    }
                }

            }
            else if (rdoAcdYear.Checked == true)
            {
                AdmBatch = 0;
                ddlAdmBatch.SelectedValue = "0";
                if (ddlAcdYear.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(updEdit, "Please Select Academic Year.", this.Page);
                    ddlAcdYear.Focus();
                    return;
                }
                else
                {
                    DataSet ds = objSC.GetBranchChangeReportExcel(AcdYear, AdmBatch);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        ds.Tables[0].TableName = "Outstanding Report RecieptWise";
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            foreach (System.Data.DataTable dt in ds.Tables)
                            {
                                //Add System.Data.DataTable as Worksheet.
                                if (dt != null && dt.Rows.Count > 0)
                                    wb.Worksheets.Add(dt);
                            }
                            //Export the Excel file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename= Branch Change Report.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updEdit, "No Record Found", this.Page);
                        return;
                    }
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updEdit, "Please Select Adm Batch or Academic Year.", this.Page);
            divParmenter.Visible = true;
            return;
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divParmenter.Visible = true;
        divAdmBatch.Visible = true;
        divAcdYear.Visible = false;
        btnexcelBranchChange.Visible = true;
    }
    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        divParmenter.Visible = true;
        divAdmBatch.Visible = false;
        divAcdYear.Visible = true;
        btnexcelBranchChange.Visible = true;
    }
}
