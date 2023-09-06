//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : HOSTEL STUDENT BONAFIDE CERTIFICATE                                  
// CREATION DATE : 29-DEC-2010                                                          
// CREATED BY    : GAURAV SONI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class Academic_HostelStudentBonafideCertificate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Masters objM = new Masters();
    string studentIds = string.Empty;
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                   
                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelStudentBonafideCertificate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelStudentBonafideCertificate.aspx");
        }
    }
    #endregion

    #region Form Actions
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BonafideController bonCont = new BonafideController();
            DataSet ds;
            int hostelSessionNo = 0;
            int admBatchNo = 0;
            int hostelno = 0;
            //if (Session["listStudentHostelBonafideCert"] == null || ((DataTable)Session["listStudentHostelBonafideCert"] == null))
            //{
                hostelSessionNo = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                admBatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                 hostelno = Convert.ToInt32(ddlHostelNo.SelectedValue);
            
                //ds = studCont.GetStudentSearchForHostelBonafideCert(hostelSessionNo, admBatchNo);

                 ds = bonCont.GetStudentSearchForHostelBonafideCert(hostelSessionNo, hostelno);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudentRecords.DataSource = ds.Tables[0];
                        lvStudentRecords.DataBind();
                        //hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                        foreach (ListViewDataItem item in lvStudentRecords.Items)
                        {
                            DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                            objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_CERTIFICATE_REMARK", "CRNO", "CERT_REMARK", "CRNO>0", "");
                        }
                    }
                    else
                    {
                         ShowMessage("Sorry,Student Record Not found.");
                       // objCommon.DisplayMessage("Sorry,Student Record Not found.", this.Page);
                        return;
                    }
                    btnPrintReport.Visible = true;
              
              //
                //Session["listStudentHostelBonafideCert"] = ds.Tables[0];
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController studCont = new StudentController();
            DataTable dt;
            if (Session["listStudentHostelBonafideCert"] != null && (DataTable)Session["listStudentHostelBonafideCert"] != null)
            {
                dt = (DataTable)Session["listStudentHostelBonafideCert"];

                string searchText = txtSearchText.Text.Trim();
                string searchBy = ("enrollmentno");
                DataTableReader dtr = studCont.RetrieveStudentDetails(searchText,searchBy).Tables[0].CreateDataReader();
                if (dtr != null && dtr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["NAME"] = dtr["NAME"];
                    dr["ENROLLMENTNO"] = dtr["ENROLLMENTNO"];
                    dr["SHORTNAME"] = dtr["SHORTNAME"];
                    dr["YEARNAME"] = dtr["YEARNAME"];
                    dr["ROOM_NO"] = dtr["ROOM_NO"];
                    dr["ISSUECOUNT"] = dtr["ISSUECOUNT"];
                    dt.Rows.Add(dr);
                    Session["listStudentHostelBonafideCert"] = dt;
                    lvStudentRecords.DataSource = dt;
                    lvStudentRecords.DataBind();
                    lblEnrollmentNo.Text = string.Empty;
                }
                else
                {
                    lblEnrollmentNo.Text = "Enrollment No. does not match.";
                }
                dtr.Close();
            }
            else
            {
                string searchText = txtSearchText.Text.Trim();
                string searchBy = ("enrollmentno");
                DataTableReader dtr = studCont.RetrieveStudentDetails(searchText, searchBy).Tables[0].CreateDataReader();
                dt = this.GetDataTable();
                DataRow dr = dt.NewRow();
                if (dtr.Read())
                {
                    dr["IDNO"] = dtr["IDNO"];
                    dr["NAME"] = dtr["NAME"];
                    dr["ENROLLMENTNO"] = dtr["ENROLLMENTNO"];
                    dr["SHORTNAME"] = dtr["SHORTNAME"];
                    dr["YEARNAME"] = dtr["YEARNAME"];
                    //dr["ROOM_NO"] = dtr["ROOM_NO"];
                    //dr["ISSUECOUNT"] = dtr["ISSUECOUNT"];
                    dt.Rows.Add(dr);
                    Session["listStudentHostelBonafideCert"] = dt;
                    lvStudentRecords.DataSource = dt;
                    lvStudentRecords.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.btnAdd_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        BonafideController objBC = new BonafideController();
        Bonafide objBonafide = new Bonafide();
        int count = 0;
        try

        {
            if (rdoBonafide.Checked == true)
            {

                {
                    foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
                    {
                        //Get Student Details from lvStudent
                        CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                        DropDownList ddlremark = dataitem.FindControl("ddlRemark") as DropDownList;
                        if (cbRow.Checked == true)
                        {
                            count++;
                            if (ddlremark.SelectedValue == "0")
                            {
                                objCommon.DisplayMessage("Please Select Remark for selected student.", this.Page);
                                txtTotStud.Text = count.ToString();
                                return;
                            }
                            else
                            {
                                Label lblIDNo = (dataitem.FindControl("lblIDNo")) as Label;
                                objBonafide.Idno = Convert.ToInt32(lblIDNo.Text);
                                ViewState["studentId"] = Convert.ToInt32(lblIDNo.Text);
                                Label lblRoomNo = (dataitem.FindControl("lblRoom")) as Label;
                                objBonafide.Room_No = Convert.ToInt32(lblRoomNo.ToolTip.ToString());
                                objBonafide.Adm_Batch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                                objBonafide.Issue_Date = DateTime.Now;
                                objBonafide.Issuer_Name = Session["username"].ToString();
                                objBonafide.Session_No = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                                objBonafide.College_Code = Session["colcode"].ToString();
                                int crno = Convert.ToInt32(ddlremark.SelectedValue);
                                // ADD Single Student
                                CustomStatus cs = (CustomStatus)objBC.AddBonafideCertificate(objBonafide, crno);
                            }
                        }
                    }
                    ShowReport(GetStudentIDs(), "Hostel_Student_Bonafide_Certificate", "HostelStudentBonafideCertificate.rpt");
                    txtTotStud.Text = count.ToString();
                }
            }
            else
            {
                if (rdoHostel.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
                    {
                        //Get Student Details from lvStudent
                        CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                        DropDownList ddlremark = dataitem.FindControl("ddlRemark") as DropDownList;
                        if (cbRow.Checked == true)
                        {
                            count++;
                            if (ddlremark.SelectedValue == "0")
                            {
                                objCommon.DisplayMessage("Please Select Remark for Selected Student.", this.Page);
                                txtTotStud.Text = count.ToString();
                                return;
                            }
                            else
                            {
                                Label lblIDNo = (dataitem.FindControl("lblIDNo")) as Label;
                                objBonafide.Idno = Convert.ToInt32(lblIDNo.Text);
                                ViewState["studentId"] = Convert.ToInt32(lblIDNo.Text);
                                Label lblRoom = (dataitem.FindControl("lblRoom")) as Label;
                                objBonafide.Room_No = Convert.ToInt32(lblRoom.ToolTip.ToString());
                                objBonafide.Adm_Batch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                                objBonafide.Issue_Date = DateTime.Now;
                                objBonafide.Issuer_Name = Session["username"].ToString();
                                objBonafide.Session_No = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                                objBonafide.College_Code = Session["colcode"].ToString();
                                int crno = Convert.ToInt32(ddlremark.SelectedValue);
                                // ADD Single Student
                                CustomStatus cs = (CustomStatus)objBC.AddResidenceCertificate(objBonafide, crno);
                            }
                        }
                    }
                    ShowReport(GetStudentIDs(), "Hostel Residence Certificate", "rptHostelResidenceCertificate.rpt");
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
            //Session["listStudentHostelBonafideCert"] = null;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Private Methods
    protected void PopulateDropDownList()
    {
        try
        {
            //FILL DROPDOWN HOSTEL SESSION NO.
            objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            if (Session["usertype"].ToString() == "1")
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
            else
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
        
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private DataTable GetDataTable()
    {
        DataTable objStudentIdCard = new DataTable();
        objStudentIdCard.Columns.Add(new DataColumn("IDNO", typeof(int)));
        objStudentIdCard.Columns.Add(new DataColumn("NAME", typeof(string)));
        objStudentIdCard.Columns.Add(new DataColumn("ENROLLMENTNO", typeof(string)));
        objStudentIdCard.Columns.Add(new DataColumn("SHORTNAME", typeof(string)));
        objStudentIdCard.Columns.Add(new DataColumn("YEARNAME", typeof(string)));
        //objStudentIdCard.Columns.Add(new DataColumn("ROOM_NO", typeof(int)));
        //objStudentIdCard.Columns.Add(new DataColumn("ISSUECOUNT", typeof(int)));
        return objStudentIdCard;
    }

    private void ShowReport(string studentIds, string reportTitle, string rptFileName)
    {
        try
        {
            if (studentIds.Length == 0)
            {
                objCommon.DisplayMessage("Please select atleast one students to generate Certificate.", this.Page);
                return;
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Hostel")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Hostel," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",username=" + Session["userfullname"].ToString() + ",@P_IDNO=" + studentIds.ToString() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_HostelStudentBonafideCertificate.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("cbRow") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentBonafideCertificate.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }
    #endregion
    //protected void btnAddRemark_Click(object sender, EventArgs e)
    //{
    //    int output = -99;
    //    output = objM.AddMaster("ACD_HOSTEL_CERTIFICATE_REMARK", "CRNO,CERT_REMARK,COLLEGE_CODE", "CRNO", "'" + txtAddRemark.Text.Trim() + "',8");
    //    if (output != -99)
    //    {
    //        lblRemark.Text = "Record Saved Successfully";
    //        txtAddRemark.Text = string.Empty;
    //        //Fill Remark
    //        foreach (ListViewDataItem item in lvStudentRecords.Items)
    //        {
    //            DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
    //            if (ddlRemark.SelectedIndex == 0)
    //                objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_CERTIFICATE_REMARK", "CRNO", "CERT_REMARK", "CRNO>0", "");
    //        }
    //    }
    //    else
    //        lblRemark.Text = "Transaction Failed!!";
    //}
}