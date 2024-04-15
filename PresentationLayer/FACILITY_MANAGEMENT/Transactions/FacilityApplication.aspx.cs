//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:17-04-2018
//PURPOSE: TO APPLY FACILITY BY DEPARTMENT HOD
//==============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using System.Globalization;

public partial class FacilityApplication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    FacilityEntity objFM = new FacilityEntity();
    FacilityController objFC = new FacilityController();
    Status obstatus = new Status();
    //NotificationController objNF = new NotificationController();
    Notification NF = new Notification();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtApplicationDate.Text = System.DateTime.Now.ToShortDateString();
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                pnlbutton.Visible = false;
                // CheckPageAuthorization();

                //Convert.ToInt32(Session["idno"])
                if (Session["idno"] != null && Convert.ToInt32(Session["idno"]) != 0)
                {
                    int idno = Convert.ToInt32(Session["idno"]);
                    ViewState["idno"] = idno;
                    BindUserDetail(idno);
                    pnlList.Visible = true;
                    FillCentralizeFacilityList();
                    BindFacilityApplication();

                }
                else
                {
                    MessageBox("Sorry! Valid Employee Login Not Found!");
                    pnlList.Visible = false;
                    return;
                }


            }

        }
        txtApplicationDate.Text = System.DateTime.Now.ToShortDateString();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MinorFacility.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MinorFacility.aspx");
        }
    }
    private void FillCentralizeFacilityList()
    {
        objCommon.FillDropDownList(ddlFacilityName, "Facility_CentralizeDetail", "CentralizeDetailNo", "CenFacilityName", " isnull(IsActive,0)=1", "CenFacilityName");
    }
    private void BindUserDetail(int idno)
    {
        //int idno=0;
        DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DES ON(DES.SUBDESIGNO=E.SUBDESIGNO)", "E.FNAME+' '+E.MNAME+' '+E.LNAME AS NAME,D.SUBDEPT", "E.PFILENO,DES.SUBDESIG", "E.IDNO=" + idno + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtDept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
            txtDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
            txtCode.Text = ds.Tables[0].Rows[0]["PFILENO"].ToString();
            txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
        }
    }
    protected void BindFacilityApplication()
    {
        try
        {
            objFM.IDNO = Convert.ToInt32(ViewState["idno"]);
            DataSet ds = objFC.GetFacilityApplication(objFM);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //btnShowReport.Visible = false;
                //dpPager.Visible = false;
                lvApplication.DataSource = null;
                lvApplication.DataBind();
                lvApplication.Visible = true;
            }
            else
            {
                //btnShowReport.Visible = true;
                //  dpPager.Visible = true;

                lvApplication.DataSource = ds;
                lvApplication.DataBind();
                lvApplication.Visible = true;
            }
            //if (ds.Tables[1].Rows.Count > 0)
            //{

            //    rptMinorFacilityList.DataSource = ds.Tables[1];
            //    rptMinorFacilityList.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FACILITY_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FACILITY_MANAGEMENT," + rptFileName;
            url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            DateTime date = DateTime.Now;
            DateTime Pdate = date.AddDays(-1);
            if (Convert.ToDateTime(txtFromDt.Text) < Pdate)
            {
                MessageBox("You can not apply for back date!!");
                return;
            }
            if (IsValid)
            {
                int result = checkApplication();
                if (result == 1)
                {
                    MessageBox(ddlFacilityName.SelectedItem.Text + "  is already booked on this time and date");
                    ClearControls();
                    return;
                }
                else
                {
                    ConvertDateTime();
                    int iresult = 0;
                    objFM.CenFacilityNo = Convert.ToInt32(ddlFacilityName.SelectedValue);
                    objFM.ApplicationDate = Convert.ToDateTime(txtApplicationDate.Text);
                    objFM.FromDate = Convert.ToDateTime(Session["FromDate"]);
                    objFM.ToDate = Convert.ToDateTime(Session["ToDate"]);
                    objFM.PriorityLevel = Convert.ToChar(ddlLevel.SelectedValue);
                    objFM.IDNO = Convert.ToInt32(ViewState["idno"]);
                    //---------------------- Changed by Vijay Andoju on 07-02-20202----------------------

                    objFM.Remark = txtAppRemark.Text; //Edited on  07-02-20202
                    //---------------------- Changed by Vijay Andoju on 07-02-20202----------------------
                    objFM.IsActive = true;
                    objFM.CreatedBy = Convert.ToInt32(Session["userno"]);
                    objFM.IPAddress = Session["ipAddress"].ToString();
                    objFM.MacAddress = Convert.ToString(Session["macAddress"]);
                    objFM.CollegeCode = Convert.ToInt32(Session["colcode"]);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MinFacilityNo");
                    dt.Columns.Add("IsExtraMinor");

                    if (chkIsCancel.Checked == true)
                    {
                        objFM.IsCancel = true;
                    }
                    else
                    {
                        objFM.IsCancel = false;
                    }
                    objFM.Reason = txtReason.Text;
                    int count = 0;
                    foreach (RepeaterItem item in rptMinorFacilityList.Items)
                    {
                        CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
                        if (chkSelect.Checked == true)
                        {
                            count = 1;
                            DataRow dr = dt.NewRow();
                            dr["MinFacilityNo"] = chkSelect.ToolTip;
                            dr["IsExtraMinor"] = false;
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                    int countExtra = 0;
                    foreach (RepeaterItem item in rptExtraMinorList.Items)
                    {
                        CheckBox chkSelectExtra = item.FindControl("chkSelectExtra") as CheckBox;
                        if (chkSelectExtra.Checked == true)
                        {
                            countExtra = 1;
                            DataRow dr = dt.NewRow();
                            dr["MinFacilityNo"] = chkSelectExtra.ToolTip;
                            dr["IsExtraMinor"] = true;
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                        }

                    }
                    if (count == 0 && countExtra == 0)
                    {
                        MessageBox("Please Select Atleast One Minor Facility");
                        return;
                    }

                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString() == "add")
                        {
                            objFM.ApplicationNo = 0;

                            iresult = Convert.ToInt32(objFC.AddUpdateFacilityApplication(objFM, dt));
                            if (iresult <= 0)
                            {
                                MessageBox("Record Already Exists");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                                BindFacilityApplication();
                            }
                            if (iresult >= 1)
                            {
                                MessageBox("Record Saved Successfully");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                                BindFacilityApplication();
                                //SendNotification();
                            }
                        }
                        else
                        {
                            objFM.ApplicationNo = Convert.ToInt32(ViewState["ApplicationNo"]);
                            if (chkIsCancel.Checked == true)
                            {
                                objFM.IsCancel = true;
                            }
                            else
                            {
                                objFM.IsCancel = false;
                            }
                            objFM.Reason = txtReason.Text;

                            iresult = Convert.ToInt32(objFC.AddUpdateFacilityApplication(objFM, dt));
                            if (iresult <= 0)
                            {
                                MessageBox("Record Already Exists");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                                BindFacilityApplication();

                            }
                            if (iresult >= 1)
                            {
                                MessageBox("Record Updated Successfully");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                                BindFacilityApplication();
                                //SendNotification();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox("To Date is Invalid (Enter dd/mm/yyyy Format)");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int CenFacilityNo = int.Parse(btnDelete.CommandArgument);
            DataSet ds = objCommon.FillDropDown("Facility_Detail", "*", "", "MinFacilityNo=" + CenFacilityNo + " and ISNULL(IsActive,0)=1 ", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox("Record Already In Used. Not Allow To Delete");
            }
            else
            {
                CustomStatus cs = (CustomStatus)objFC.DeleteFacilityDetail(objFM);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    ViewState["action"] = null;
                    //BindApplicationByUser();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ApplicationNo = int.Parse(btnEdit.CommandArgument);
            ViewState["ApplicationNo"] = ApplicationNo.ToString();
            string Status = (btnEdit.AlternateText);
            ViewState["Status"] = Status.ToString();
            if (Status.ToString() == "CANCEL")
            {
                MessageBox("Cancelled Application Cannot Modify");
                return;
            }

            if (Status.ToString() == "REJECTED")
            {
                MessageBox("Rejected Application Cannot Modify");
                return;

            }
            if (Status.ToString() == "APPROVED")
            {
                MessageBox("Approved Application Cannot Modify");
                return;
            }

            ShowDetails(ApplicationNo);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlbutton.Visible = true;
            pnlList.Visible = false;
            lvApplication.Visible = false;
            Panel4.Visible = true;
            Panel2.Visible = true;


            divCancel1.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowDetails(Int32 ApplicationNo)
    {
        DataSet ds = null;
        try
        {
            objFM.ApplicationNo = ApplicationNo;
            ds = objFC.GetFacilityApplicationByNo(objFM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ApplicationNo"] = ApplicationNo;
                ddlFacilityName.SelectedValue = ds.Tables[0].Rows[0]["CentralizeDetailNo"].ToString();
                txtDetail.Text = ds.Tables[0].Rows[0]["CenFacilityDetail"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["FD_REMARK"].ToString();
                txtAppRemark.Text = ds.Tables[0].Rows[0]["APP_REMARK"].ToString();
                txtApplicationDate.Text = ds.Tables[0].Rows[0]["ApplicationDate"].ToString();
                txtFromDt.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["ToDate"].ToString();
                txtFromTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"].ToString()).ToString("hh:mm tt");
                txtToTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"].ToString()).ToString("hh:mm tt");
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["PriorityLevel"].ToString();

                //CancelReason,IsCancel
                txtReason.Text = ds.Tables[0].Rows[0]["CancelReason"].ToString();
                Boolean IsCancel = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCancel"].ToString());
                if (IsCancel)
                {
                    chkIsCancel.Checked = true;
                }
                else
                {
                    chkIsCancel.Checked = false;
                }
            }



            if (ds.Tables[2].Rows.Count > 0)
            {
                rptMinorFacilityList.DataSource = ds.Tables[2];
                rptMinorFacilityList.DataBind();
                for (int i = 0; i < rptMinorFacilityList.Items.Count; i++)
                {

                    CheckBox chkSelect = rptMinorFacilityList.Items[i].FindControl("chkSelect") as CheckBox;
                    string CHECK_STATUS = ds.Tables[2].Rows[i]["CHECK_STATUS"].ToString().Trim();
                    if (CHECK_STATUS == "CHECKED")
                    {
                        chkSelect.Checked = true;
                    }
                    else
                    {
                        chkSelect.Checked = false;
                    }
                }
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                rptExtraMinorList.DataSource = ds.Tables[3];
                rptExtraMinorList.DataBind();
                for (int i = 0; i < rptExtraMinorList.Items.Count; i++)
                {

                    CheckBox chkSelectExtra = rptExtraMinorList.Items[i].FindControl("chkSelectExtra") as CheckBox;
                    string CHECK_STATUS = ds.Tables[3].Rows[i]["CHECK_STATUS"].ToString().Trim();
                    if (CHECK_STATUS == "CHECKED")
                    {
                        chkSelectExtra.Checked = true;
                    }
                    else
                    {
                        chkSelectExtra.Checked = false;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ClearControls()
    {
        txtFromDt.Text = txtToDt.Text = txtApplicationDate.Text = txtAppRemark.Text = txtDetail.Text = txtRemark.Text = txtDetail.Text = txtToTime.Text = txtFromTime.Text = txtRemark.Text = string.Empty;
        ddlFacilityName.SelectedIndex = 0;
        //ddlLevel.SelectedValue = "0";
        ddlLevel.SelectedIndex = 0;
        txtApplicationDate.Text = System.DateTime.Now.ToShortDateString();
        Panel2.Visible = false;
        Panel4.Visible = false;

    }
    private void Clear()
    {
        txtFromDt.Text = txtToDt.Text = txtApplicationDate.Text = txtAppRemark.Text = txtDetail.Text = txtRemark.Text = txtDetail.Text = txtToTime.Text = txtFromTime.Text = txtRemark.Text = string.Empty;
        pnlbutton.Visible = false;
        pnlList.Visible = true;
        Panel2.Visible = false;
        Panel4.Visible = false;
        ddlFacilityName.SelectedIndex = 0;
        //ddlLevel.SelectedValue = "0";
        ddlLevel.SelectedIndex = 0;
        txtApplicationDate.Text = System.DateTime.Now.ToShortDateString();

        // FillCentralizeFacilityList();
        // BindFacilityApplication();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false; pnlbutton.Visible = false;
        pnlList.Visible = true;
        divCancel1.Visible = false;
        BindFacilityApplication();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        divCancel1.Visible = false;
        pnlbutton.Visible = true;
        ViewState["action"] = "add";
        lvApplication.Visible = false;
        txtApplicationDate.Text = System.DateTime.Now.ToShortDateString();
    }
    protected void BindFacilityApplicationByNo()
    {
        try
        {
            objFM.CenFacilityNo = Convert.ToInt32(ddlFacilityName.SelectedValue);
            DataSet ds = objFC.GetCentraFacilityDetailByNo(objFM);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtDetail.Text = ds.Tables[0].Rows[0]["CenFacilityDetail"].ToString();
            }
            if (ds.Tables[2].Rows.Count <= 0)
            {
                //btnShowReport.Visible = false;
                //dpPager.Visible = false;
                rptMinorFacilityList.DataSource = null;
                rptMinorFacilityList.DataBind();
                Panel2.Visible = false;
                rptMinorFacilityList.Visible = false;
            }
            else
            {
                //btnShowReport.Visible = true;
                //  dpPager.Visible = true;

                rptMinorFacilityList.DataSource = ds.Tables[2];
                rptMinorFacilityList.DataBind();
                Panel2.Visible = true;
                rptMinorFacilityList.Visible = true;
            }
            if (ds.Tables[3].Rows.Count > 0)
            {

                rptExtraMinorList.DataSource = ds.Tables[3];
                rptExtraMinorList.DataBind();
                Panel4.Visible = true;
                rptExtraMinorList.Visible = true;
            }
            else
            {

                rptExtraMinorList.DataSource = null;
                rptExtraMinorList.DataBind();
                Panel4.Visible = false;
                rptExtraMinorList.Visible = false;
            }
            // lvApplication.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain            
    //    BindFacilityApplicationByNo();
    //}

    protected void ddlFacilityName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFacilityName.SelectedValue == "0")
        {
            txtDetail.Text = string.Empty;
            txtRemark.Text = string.Empty;
            Panel2.Visible = false;
            rptMinorFacilityList.Visible = false;
            Panel4.Visible = false;
            rptExtraMinorList.Visible = false;

        }
        else
        {
            BindFacilityApplicationByNo();
        }
    }

    //protected void txtToDt_TextChanged(object sender, EventArgs e)
    //{
    //    if (ddlFacilityName.SelectedIndex > 0)
    //    {
    //       // CheckApplicationExists();
    //        txtToTime.Focus();

    //    }
    //    if (Convert.ToDateTime(txtFromDt.Text) > Convert.ToDateTime(txtToDt.Text))
    //    {
    //        txtFromDt.Text = string.Empty;

    //        objCommon.DisplayUserMessage(updPanel, "From Date should be less than or equal to To Date.", this);

    //    }

    //}

    private void CheckApplicationExists()
    {
        int ret = 0;
        try
        {
            if (ddlFacilityName.SelectedIndex > 0)
            {
                objFM.FromDate = Convert.ToDateTime(txtFromDt.Text);
                objFM.ToDate = Convert.ToDateTime(txtToDt.Text);
                if (ViewState["ApplicationNo"] != null)
                {
                    objFM.ApplicationNo = Convert.ToInt32(ViewState["ApplicationNo"]);
                }
                else
                {
                    objFM.ApplicationNo = 0;
                }
                objFM.CenFacilityNo = Convert.ToInt32(ddlFacilityName.SelectedValue);
                objFM.IDNO = Convert.ToInt32(ViewState["idno"]);
                DataSet ds = objFC.CheckApplicationExists(objFM);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string IsExists = ds.Tables[0].Rows[0]["IsExists"].ToString();
                    if (IsExists == "Y")
                    {

                        objCommon.DisplayUserMessage(updPanel, "Application Already Exists For Selected Facility. Do you want to Continue!", this);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void ConvertDateTime()
    {
        DateTime fromdate, todate, to_time, from_time;
        Session["ToDate"] = null;
        Session["FromDate"] = null;
        from_time = Convert.ToDateTime(txtFromTime.Text);
        fromdate = Convert.ToDateTime(txtFromDt.Text);
        Session["FromDate"] = fromdate.ToString("yyyy-MM-dd") + from_time.ToString(" HH:mm:ss");
        to_time = Convert.ToDateTime(txtToTime.Text);
        todate = Convert.ToDateTime(txtToDt.Text);
        Session["ToDate"] = todate.ToString("yyyy-MM-dd") + to_time.ToString(" HH:mm:ss");

    }
    private int checkApplication()
    {
        int result = 0;
        try
        {
            ConvertDateTime();
            objFM.CenFacilityNo = Convert.ToInt32(ddlFacilityName.SelectedValue);
            objFM.FromDate = Convert.ToDateTime(Session["FromDate"]);
            objFM.ToDate = Convert.ToDateTime(Session["ToDate"]);
            result = objFC.ChkApplicationDateTime(objFM);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return result;
    }

    protected void SendNotification()
    {
        try
        {

            DataSet ds1 = objCommon.FillDropDown("USER_ACC U left JOIN PAYROLL_EMPMAS E ON (U.UA_IDNO=E.IDNO)", "U.UA_DEPTNO, U.UA_NO", "UA_TYPE", "U.UA_NO IN(SELECT FACILITY_MGT_AUTHORITY_UANO FROM REFF)", "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                //E.SUBDEPTNO
                NF.Degreeno = 0;
                NF.Semesterno = 0;
                NF.Branchno = 0;
                //if (Auth_ua_no != 0)
                if (Convert.ToInt32(ds1.Tables[0].Rows[0]["UA_NO"].ToString()) != 0)
                {
                    DataSet ds = objCommon.FillDropDown("TBL_ANDROID_NOTIFICATION", "*", "", "NOTIFICATIONID=15", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NF.NotificationID = Convert.ToInt32(ds.Tables[0].Rows[0]["NOTIFICATIONID"].ToString());
                        NF.Title = ds.Tables[0].Rows[0]["TITLE"].ToString();
                        NF.UANO = Convert.ToInt32(Session["userno"].ToString());
                        NF.UA_Type = Convert.ToInt32(ds1.Tables[0].Rows[0]["UA_TYPE"].ToString());
                        NF.Deptno = Convert.ToInt32(ds1.Tables[0].Rows[0]["UA_DEPTNO"].ToString());
                        NF.UserNo = Convert.ToString(ds1.Tables[0].Rows[0]["UA_NO"].ToString());
                        NF.Details = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                    }

                }
                //if (Auth_uatype == 2)
                if (Convert.ToInt32(ds1.Tables[0].Rows[0]["UA_TYPE"].ToString()) == 2)
                {
                    DataSet ds = objCommon.FillDropDown("USER_ACC INNER JOIN ACD_STUDENT S ON(U.UA_IDNO=S.IDNO INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_SEMESTER SEM ON(S.SEMESTERNO=SEM.SEMESTERNO))  ", "*", "D.DEGREENO,B.BRANCHNO,SEM.SEMESTERNO", "UA_TYPE" + ds1.Tables[0].Rows[0]["UA_TYPE"].ToString(), "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NF.Degreeno = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
                        NF.Semesterno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                        NF.Branchno = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString());
                    }

                }

                obstatus = objFC.SubmitNotificationDetails(NF);
                // MessageBox("Notification send successfully");
                objCommon.DisplayMessage(this.updPanel, obstatus.Message.ToString(), this.Page);
            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "Facility_Management_MinorFacility.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
        }

    }
    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        if (txtFromDt.Text != string.Empty)
        {
            if (IsValidDateFormat(txtFromDt.Text, "dd/MM/yyyy") && IsValidDateFormat(txtToDt.Text, "dd/MM/yyyy"))
            {
                if (Convert.ToDateTime(txtFromDt.Text) > Convert.ToDateTime(txtToDt.Text))
                {
                    MessageBox("To date should be greater than or equal to From date");
                    txtToDt.Text = string.Empty;
                }
            }
        }
    }

    static bool IsValidDateFormat(string date, string format)
    {
        DateTime parsedDate;
        return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
    }
}

