//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:18-04-2018
//PURPOSE: TO APPROVE FACILITY BY DEPARTMENT HOD
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

public partial class FacilityApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
   
    FacilityEntity objFM = new FacilityEntity();
    FacilityController objFC = new FacilityController();

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
                //pnlList.Visible = true;
                pnlbutton.Visible = false;
              //  CheckPageAuthorization();

                int uano = Convert.ToInt32(Session["userno"]);
                ViewState["uano"] = uano;

             
                BindFacilityApplication();

                
            }

        }
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
  
    private void BindUserDetail(int idno)
    {
        //int idno=0;
        DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DES ON(DES.SUBDESIGNO=E.SUBDESIGNO)","E.FNAME+' '+E.MNAME+' '+E.LNAME AS NAME,D.SUBDEPT","E.PFILENO,DES.SUBDESIG","E.IDNO="+idno+"","");
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
            objFM.UANO = Convert.ToInt32(ViewState["uano"]);
            DataSet ds = objFC.GetPendingApplication(objFM);
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

                lvApplication.DataSource = ds.Tables[0];
                lvApplication.DataBind();
                lvApplication.Visible = true;
            }
            if (ds.Tables[1].Rows.Count <= 0)
            {
                //btnShowReport.Visible = false;
                //dpPager.Visible = false;
                rptApplicationStatus.DataSource = null;
                rptApplicationStatus.DataBind();
                rptApplicationStatus.Visible = true;
            }
            else
            {
                //btnShowReport.Visible = true;
                //  dpPager.Visible = true;

                rptApplicationStatus.DataSource = ds.Tables[1];
                rptApplicationStatus.DataBind();
                rptApplicationStatus.Visible = true;
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
            int iresult = 0;
           
          
            if (ViewState["action"] != null)
            {
                
                    objFM.ApplicationNo = Convert.ToInt32(ViewState["ApplicationNo"]);
                    objFM.UANO = Convert.ToInt32(ViewState["uano"]);
                    objFM.STATUS = Convert.ToChar(ddlStatus.SelectedValue.ToString());
                    objFM.Remark = txtApprovalRemark.Text;
                    objFM.IsActive = true;
                    objFM.IsActive = true;
                    objFM.CreatedBy = Convert.ToInt32(Session["userno"]);
                    objFM.IPAddress = Session["ipAddress"].ToString();
                    objFM.MacAddress = Convert.ToString(Session["macAddress"]);
                    objFM.CollegeCode = Convert.ToInt32(Session["colcode"]);
                    iresult = Convert.ToInt32(objFC.AddUpdateFacilityApproval(objFM));                    
                    if (iresult >= 1)
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false;                      
                        ViewState["action"] = null;
                        Clear();
                        BindFacilityApplication();
                    }
                
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
            string AppStatus = btnEdit.AlternateText;
            ViewState["App_Status"] = AppStatus;
            if (AppStatus == "CANCELLED")
            {
                MessageBox("User cancelled this application ");
                return;
            }
            ShowDetails(ApplicationNo);
            
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlbutton.Visible = true;
            //pnlList.Visible = false;
            lvApplication.Visible = rptApplicationStatus.Visible = false;
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
                txtFacilityDetailName.Text = ds.Tables[0].Rows[0]["CenFacilityName"].ToString(); ;
                txtDetail.Text = ds.Tables[0].Rows[0]["CenFacilityDetail"].ToString();               
                txtAppRemark.Text = ds.Tables[0].Rows[0]["APP_REMARK"].ToString();
                txtApplicationDate.Text = ds.Tables[0].Rows[0]["ApplicationDateAPR"].ToString();
                txtFromDt.Text = ds.Tables[0].Rows[0]["FromDateAPR"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["ToDateAPR"].ToString();               
                txtLevel.Text = ds.Tables[0].Rows[0]["PriorityLevel_Status"].ToString();

                txtDept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                txtCode.Text = ds.Tables[0].Rows[0]["PFILENO"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

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
    private void Clear()
    {
        txtFromDt.Text = txtToDt.Text=txtApplicationDate.Text=txtAppRemark.Text=txtDetail.Text = txtLevel.Text =txtDetail.Text=txtFacilityDetailName.Text=txtApprovalRemark.Text = string.Empty;
        ddlStatus.SelectedValue = "0";
        pnlbutton.Visible = false;
        //pnlList.Visible = true;
      
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
        //pnlList.Visible = true;
        BindFacilityApplication();
    }
 
   
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain            
    //    BindFacilityApplicationByNo();
    //}

   
}

