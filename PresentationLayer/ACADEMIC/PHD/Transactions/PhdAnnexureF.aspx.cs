//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE-F                                            
// CREATION DATE : 26 jun 2018                                                   
// CREATED BY    : Dipali Nanore                         
// MODIFIED DATE :                 
// ADDED BY      :                                  
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Drawing;


public partial class Academic_StudentInfoEntry : System.Web.UI.Page
{
    PhdController objEntityMethodClass = new PhdController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    static int Result1 = 0;
    Label ReceptNo = new Label();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    static bool chkvalidation = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //imgPhoto.ImageUrl = "~/images/nophoto.jpg";
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
               // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                //FillDropDown();
                ViewState["Receiptcode"] = "PHF";
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));  //--- change ua_idno by  ua_no 

                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");

                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    //pnlId.Visible = false;
                    pnDisplay.Visible = true;
                    ShowStudentDetails();
                    BindStudents();
                    ViewState["action"] = "edit";

                    if (txtThesistitle.Text == string.Empty)
                    {
                        if (chkvalidation == false)
                        {
                            txtThesistitle.Enabled = true;
                            txtThesistitleHindi.Enabled = true;
                            Btnsubmit.Visible = false;
                            btnReport.Visible = false;
                            btnpayment.Visible = false;

                        }
                        else
                        {
                            txtThesistitle.Enabled = true;
                            txtThesistitleHindi.Enabled = true;
                            Btnsubmit.Visible = true;
                            btnReport.Visible = false;
                            btnpayment.Visible = false;
                        }
                    }
                    else
                    {
                        txtThesistitle.Enabled = false;
                        txtThesistitleHindi.Enabled = false;
                        Btnsubmit.Visible = false;
                        btnReport.Visible = true;
                        btnpayment.Visible = true;
                    }
                }
                else
                {

                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "3")
                    {                        //pnlDoc.Enabled = false;
                        //pnlId.Enabled = false;
                    }
                   // pnlId.Visible = true;
                    lblRegNo.Enabled = true;
                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        BindStudents();
                        //ShowSignDetails();
                    }

                    txtThesistitle.Enabled = false;
                    txtThesistitleHindi.Enabled = false;
                    ddlDescipline.Enabled = false;
                    if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "4")
                    {
                        pnDisplay.Visible = false;
                        txtThesistitle.Enabled = true;
                        txtThesistitleHindi.Enabled = true;
                        ddlDescipline.Enabled = true;
                        Btnsubmit.Visible = true;
                        btnReport.Visible = true;
                        btnpayment.Visible = false;
                    }
                }
            }
        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
    }


    protected void BindStudents()
    {
        try
        {
            chkvalidation = true;
            if (ViewState["idno"].ToString() != null)
            {
                // DataSet ds = objEntityMethodClass.GETStudProgressRptDetails(Convert.ToInt32(lblRegNo.ToolTip.ToString()));
                DataSet ds = objEntityMethodClass.GETStudProgressRptDetails(Convert.ToInt32(ViewState["idno"].ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvphddetails.DataSource = ds.Tables[0];
                    lvphddetails.DataBind();

                    foreach (ListViewDataItem dataRow in lvphddetails.Items)
                    {
                        Label lblsup = dataRow.FindControl("lblSup") as Label;
                        Label lbljointsup = dataRow.FindControl("lbljointsup") as Label;
                        Label lblinsfaculty = dataRow.FindControl("lblinsfac") as Label;
                        Label lbldrc = dataRow.FindControl("lbldrcstatus") as Label;
                        Label lbldrcchairman = dataRow.FindControl("lbldrcchairman") as Label;
                        Label lbldean = dataRow.FindControl("lbldean") as Label;

                        if (ViewState["usertype"].ToString() == "2")
                        {
                            //if (lblsup.Text.ToString().ToUpper().Contains("PENDING") || lbljointsup.Text.ToString().ToUpper().Contains("PENDING") || lblinsfaculty.Text.ToString().ToUpper().Contains("PENDING") || lbldrc.Text.ToString().ToUpper().Contains("PENDING") || lbldrcchairman.Text.ToString().ToUpper().Contains("PENDING") || lbldean.Text.ToString().ToUpper().Contains("PENDING"))
                                if (lbldean.ToolTip.ToString().ToUpper().Contains("PENDING"))
                            {
                                chkvalidation = false;
                            }
                           
                        }

                    }

                    if (chkvalidation == false)
                    {
                        objCommon.DisplayMessage("First Get Approval From all Your Pending Progress Report.!!", this.Page);
                        Btnsubmit.Visible = false;
                        btnReport.Visible = false;
                        btnpayment.Visible = false;
                        return;
                    }
                    else
                    {
                        Btnsubmit.Visible = true;
                        btnReport.Visible = true;
                        btnpayment.Visible = true;
                    }


                }
                else
                {
                    lvphddetails.DataSource = null;
                    lvphddetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PhdAnnexureF.BindStudents() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void bindlist(string category, string searchtext)
    {


        string branchno = "0";

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";

    }

    private void ChangeControlStatus(bool status)
    {

        foreach (Control c in Page.Controls)
            foreach (Control ctrl in c.Controls)

                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = status;

                else if (ctrl is Button)

                    ((Button)ctrl).Enabled = status;

                else if (ctrl is RadioButton)

                    ((RadioButton)ctrl).Enabled = status;

                else if (ctrl is ImageButton)

                    ((ImageButton)ctrl).Enabled = status;

                else if (ctrl is CheckBox)

                    ((CheckBox)ctrl).Enabled = status;

                else if (ctrl is DropDownList)

                    ((DropDownList)ctrl).Enabled = status;

                else if (ctrl is HyperLink)

                    ((HyperLink)ctrl).Enabled = status;
    }

    private void ShowStudentDetails()
    {
        try
        {
        string count = objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + "AND STATUS=1");
        string FSTATUS = objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + " AND ANNEXURE_F_STATUS=1");                  
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        ViewState["idno"] = 0;

        if (ViewState["usertype"].ToString() == "2")
        {
            if (Convert.ToInt32(count) > 0)
            {
                // modify  specialization by dipali 04072018
                //objCommon.FillDropDownList(ddlDescipline, "(select  DEPTNO , DEPTNAME  from ACD_DEPARTMENT where DEPTNO>0 Union all select  CODE DEPTNO ,SHORTNAME DEPTNAME from  ACD_SPECIALIZATION where DEGREENO=5 and SPECIALNO>0 )a ", "ROW_NUMBER()over (order by  DEPTNO)DEPTNO", "DEPTNAME", "DEPTNO>0 ", "DEPTNO");
                //added by neha 17March21
                objCommon.FillDropDownList(ddlDescipline, "ACD_DISCIPLINE", "DISCNO", "DISCNAME", "DISCNO>0 AND ISNULL(DISC_ACTIVE_STATUS,0)=1", "DISCNAME");

                dtr = objSC.GetStudentPHDDetailsAnnexureF(Convert.ToInt32(Session["stuinfoidno"]));
                ViewState["idno"] = Session["idno"].ToString();
                if (Convert.ToInt32(FSTATUS) > 0)
                {
                    btnpayment.Visible = true;
                }                     
            }
            else
            {
                objCommon.DisplayMessage(this.updEdit,"PhD Annexure B is Incomplete or Not Approved !!", this.Page);
                btnReport.Enabled = false; // Added on 05/12/2015
                return;
            }
        }
        else
        {
            // modify  specialization by dipali 04072018
            //objCommon.FillDropDownList(ddlDescipline, "(select  DEPTNO , DEPTNAME  from ACD_DEPARTMENT where DEPTNO>0 Union all select  CODE DEPTNO ,SHORTNAME DEPTNAME from  ACD_SPECIALIZATION where DEGREENO=5 and SPECIALNO>0 )a ", "ROW_NUMBER()over (order by  DEPTNO)DEPTNO", "DEPTNAME", "DEPTNO>0 ", "DEPTNO");

            //added by neha 17March21
            objCommon.FillDropDownList(ddlDescipline, "ACD_DISCIPLINE", "DISCNO", "DISCNAME", "DISCNO>0 AND ISNULL(DISC_ACTIVE_STATUS,0)=1", "DISCNAME");
                       
            //objCommon.FillDropDownList(ddlDescipline, "(Select distinct b.Deptno,deptname  from ACD_DEPARTMENT D INNER JOIN ACD_BRANCH B ON D.DEPTNO=B.DEPTNO and ISNULL(b.degreeno,0)!= 5 UNION SELECT BRANCHNO Deptno, SHORTNAME+'-'+SPECIALIZATION DEPTNAME  FROM ACD_BRANCH WHERE SPECIALIZATION IS NOT NULL )A", " ROW_NUMBER() OVER(ORDER BY DEPTNO)DEPTNO", "DEPTNAME", "DEPTNO>0 ", "DEPTNAME");
            dtr = objSC.GetStudentPHDDetailsAnnexureF(Convert.ToInt32(Session["stuinfoidno"].ToString()));
            ViewState["idno"] = Session["stuinfoidno"].ToString();
            pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                 //txtIDNo.Text = dtr["IDNO"].ToString();
                //txtIDNo.ToolTip = dtr["REGNO"].ToString();
                lblRegNo.ToolTip = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ENROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ENROLLNO"].ToString();
                lblRegNo.Text = dtr["ROLLNO"].ToString();
                //txtRegNo.Enabled = false;
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
                if (lblStatus.Text == "2")
                {
                    lblStatus.Text = "PART TIME";
                }
                else
                {
                    lblStatus.Text = "FULL TIME";
                }

                lblSupervisor.Text = dtr["PHDSUPERVISORNAME"] == null ? string.Empty : dtr["PHDSUPERVISORNAME"].ToString();
                lblSupervisor.ToolTip = dtr["PHDSUPERVISORNO"] == null ? string.Empty : dtr["PHDSUPERVISORNO"].ToString();
                txtThesistitle.Text = dtr["THESIS_TITLE"] == null ? string.Empty : dtr["THESIS_TITLE"].ToString();
                txtThesistitleHindi.Text = dtr["THESIS_TITLE_HINDI"] == null ? string.Empty : dtr["THESIS_TITLE_HINDI"].ToString();
                
                if (txtThesistitle.Text == string.Empty)
                {
                    ddlDescipline.Items.Add(new ListItem("Please Select", "0"));
                    ddlDescipline.SelectedValue = "0";
                }
                else
                {
                    //ddlDescipline.SelectedItem.Text = dtr["DISCIPLINE"].ToString();

                    string abc = dtr["DISCIPLINE"].ToString();
                    int discno = Convert.ToInt32(objCommon.LookUp("ACD_DISCIPLINE", "DISCNO", "DISCNAME = '" + dtr["DISCIPLINE"].ToString() + "' "));
                    ddlDescipline.SelectedValue = discno.ToString();
                }
                
                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1")
                {
                    if (txtThesistitle.Text == "" || txtThesistitle.Text == string.Empty)
                    {
                        txtThesistitle.Enabled = true;
                        ddlDescipline.Enabled = true;
                        Btnsubmit.Visible = true;
                        btnReport.Visible = false;
                    }
                    else
                    {
                        txtThesistitle.Enabled = false;
                        ddlDescipline.Enabled = false;
                        Btnsubmit.Visible = false;
                        btnReport.Visible = true;
                    }
                }
                else
                {
                    txtThesistitle.Enabled = false;
                    ddlDescipline.Enabled = false;
                    Btnsubmit.Visible = false;
                    btnReport.Enabled = true;
                }
                if (ViewState["usertype"].ToString() == "1")
                {
                    Btnsubmit.Visible = true;
                    btnReport.Visible = true;
                }
                //  btnReport.Enabled = true; // Added on 05/12/2015
            }
        }
        }
            catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PhdAnnexureF.ShowStudentDetails() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhdAnnexureF.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdAnnexureF.aspx");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }
        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        ShowStudentDetails();
        updEdit.Visible = false;
        pnDisplay.Visible = true;

        //Response.Redirect(url + "&id=" + lnk.CommandArgument);

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexureF.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        ShowReport("PhdAnnexureFReport", "rptAnnexureF.rpt");
    }

    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        try{
        StudentController objSC = new StudentController();
        Student objS = new Student();
        objS.IdNo = Convert.ToInt32(lblRegNo.ToolTip.ToString());
        objS.ThesisTitle = Server.HtmlDecode(txtThesistitle.Text.Trim());
        objS.Descipline = ddlDescipline.SelectedItem.Text;
        objS.ThesisTitleHindi = Server.HtmlDecode(txtThesistitleHindi.Text.Trim());
        string output = objSC.UpdatePHDStudentAnnexure_F(objS);
        if (output != "-99")
        {
            Session["qualifyTbl"] = null;
            objCommon.DisplayMessage(this,"Student Information Updated Successfully!!", this.Page);
            DemandAndDcr();
            this.ShowStudentDetails();

            txtThesistitle.Enabled = false;
            txtThesistitleHindi.Enabled = false;
            ddlDescipline.Enabled = false;
            btnReport.Visible = true;
            btnpayment.Visible = true;
            if (ViewState["usertype"].ToString() == "1")
            {
                btnpayment.Visible = false;
            }
        }
        else
        {
            Session["qualifyTbl"] = null; 
            objCommon.DisplayMessage(this,"PhD Annexure B is Incomplete or Not Approved !!", this.Page);           
            this.ShowStudentDetails();
            txtThesistitle.Enabled = true;
            txtThesistitleHindi.Enabled = true;
            ddlDescipline.Enabled = true;
            btnReport.Visible = false;
            btnpayment.Visible = false;
        }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PhdAnnexureF.Btnsubmit_Click() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnpayment_Click(object sender, EventArgs e)
    {
        string RollNO = lblRegNo.Text.ToString().Trim();
        string EnrollNo = lblEnrollNo.Text.ToString().Trim();
        //Response.Redirect("http://localhost:55628/PresentationLayer/Academic/PaymentHome.aspx?pageno=2472");
        Response.Redirect("https://mis.nitrr.ac.in/ACADEMIC/PaymentHome.aspx?pageno=2472");
    }

    #region DCR and Demand-- 01122017
    public void DemandAndDcr()
    {
        ReceptNo.Text = this.GetNewReceiptNo();
        if (ReceptNo.Text != string.Empty)
        {
            if (this.AddDemand() == 1)
            {
                if (this.AddDCR() == 1)
                {
                    Result1 = 1;
                }
            }
        }

    }

    public int AddDCR()
    {
        try
        {

            FeeDemand objEntityClass = new FeeDemand();
            objEntityClass.SessionNo = 1;
            objEntityClass.StudentId = Convert.ToInt32(lblRegNo.ToolTip.ToString());
            objEntityClass.EnrollmentNo = lblEnrollNo.Text == string.Empty ? "" : lblEnrollNo.Text.ToString().Trim();
            objEntityClass.ReceiptTypeCode = ViewState["Receiptcode"].ToString() == string.Empty ? "" : ViewState["Receiptcode"].ToString().Trim();
            objEntityClass.SemesterNo  = 8;
            objEntityClass.PaymentTypeNo = 1;
            objEntityClass.Remark = ReceptNo.Text == string.Empty ? "" : ReceptNo.Text.ToString().Trim();
             objEntityClass.CounterNo = Convert.ToInt32(ViewState["CounterNo"].ToString() == string.Empty ? "" : ViewState["CounterNo"].ToString().Trim());
             objEntityClass.UserNo = 1;

            int result = objEntityMethodClass.InsertPhdThesisDCR(objEntityClass);

            if (result == 1)
            {
                 return result;
            }
            else
            {
                objCommon.DisplayMessage("Error In Saving, Please Try Again", this.Page);
                return result;
            }
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
            return 0;
        }
    }

    public int AddDemand()
    {
        try
        {
            FeeDemand objEntityClass = new FeeDemand();
            objEntityClass.SessionNo = 1;
            objEntityClass.StudentId = Convert.ToInt32(lblRegNo.ToolTip.ToString());
            objEntityClass.EnrollmentNo = lblEnrollNo.Text == string.Empty ? "" : lblEnrollNo.Text.ToString().Trim();
            objEntityClass.ReceiptTypeCode = ViewState["Receiptcode"].ToString() == string.Empty ? "" : ViewState["Receiptcode"].ToString().Trim();
            objEntityClass.SemesterNo = 8;
            objEntityClass.PaymentTypeNo = 1;
            objEntityClass.CounterNo =Convert.ToInt32( ViewState["CounterNo"].ToString() == string.Empty ? "" : ViewState["CounterNo"].ToString().Trim());
            objEntityClass.FeeCatNo = 1;
            int result = objEntityMethodClass.InsertPhdThesisDemand(objEntityClass );

            if (result == 1)
            {
                //  objEntityMethodClass.DisplayMessage("You are Demand Create successfully", this.Page);
                return result;

            }
            else
            {
                objCommon.DisplayMessage("Error In Saving, Please Try Again", this.Page);
                return result;
            }
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
            return 0;
        }
    }

    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "PHF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
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

    #endregion

    protected void lblphdReportdownload_Click(object sender, EventArgs e)
    {       
    }


    protected void btnsearch_Click(object sender, EventArgs e)
        {

        }
    protected void btnClose_Click(object sender, EventArgs e)
        {

        }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            //Panel3.Visible = false;
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
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;


                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
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

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

                }
            }
        catch
            {
            throw;
            }
        txtSearch.Text = string.Empty;
        }
}



