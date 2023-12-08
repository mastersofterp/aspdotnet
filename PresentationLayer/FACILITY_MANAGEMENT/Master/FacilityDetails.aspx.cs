//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:16-04-2018
//PURPOSE: TO DEFINE FACILITY DETAILS
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

public partial class FacilityDetails : System.Web.UI.Page
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
                pnlList.Visible = true;
                pnlbutton.Visible = false;
              //  CheckPageAuthorization();
                //FillMinorFacility();
                BindFacilityDetail();
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
    //protected void FillMinorFacility()
    //{
    //    try
    //    {
    //        objCommon.FillListBox(lstMinorFacility, "Facility_Minor", "MinFacilityNo", "MinFacilityName", "isnull(IsActive,0)=1", "MinFacilityName");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_attendanceProcess.FillMinorFacility-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iresult = 0;
            objFM.CenFacilityName = txtFacilityName.Text;
            objFM.CenFacilityDetail = txtDetail.Text;
            objFM.Remark = txtRemark.Text;
            objFM.IsActive = true;
            objFM.CreatedBy = Convert.ToInt32(Session["userno"]);
            objFM.IPAddress = Session["ipAddress"].ToString();
            objFM.MacAddress = Convert.ToString(Session["macAddress"]);
            objFM.CollegeCode = Convert.ToInt32(Session["colcode"]);
            DataTable dt = new DataTable();
            dt.Columns.Add("MinFacilityNo");

            //for (int i = 0; i < lstMinorFacility.Items.Count; i++)
            //{
            //    if (lstMinorFacility.Items[i].Selected)
            //    {
            //DataRow dr = dt.NewRow();
            //dr["MinFacilityNo"] = lstMinorFacility.Items[i].Value;
            //dt.Rows.Add(dr);
            //dt.AcceptChanges();
            //    }
            //}
            int count = 0;
            foreach (RepeaterItem item in rptMinorList.Items)
            {
                CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
                if (chkSelect.Checked == true)
                {
                    count = 1;
                    DataRow dr = dt.NewRow();
                    dr["MinFacilityNo"] = chkSelect.ToolTip;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();

                }

            }
            if (count == 0)
            {
                MessageBox("Please Select Atleast One Minor Facility");
                return;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString() == "add")
                {
                    objFM.CenFacilityNo = 0;

                    iresult = Convert.ToInt32(objFC.AddUpdateCentraFacilityDetail(objFM, dt));
                    if (iresult <= 0)
                    {
                        MessageBox("Record Alreasy Exists");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                        BindFacilityDetail();
                    }
                    if (iresult >= 1)
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                        BindFacilityDetail();
                    }
                }
                else
                {
                    objFM.CenFacilityNo = Convert.ToInt32(ViewState["CenFacilityNo"]);


                    iresult = Convert.ToInt32(objFC.AddUpdateCentraFacilityDetail(objFM, dt));
                    if (iresult <= 0)
                    {
                        MessageBox("Record Alreasy Exists");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                        BindFacilityDetail();
                    }
                    if (iresult >= 1)
                    {
                        MessageBox("Record Updated Successfully");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                        BindFacilityDetail();
                    }
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
                    BindFacilityDetail();
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
            int MinFacilityNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(MinFacilityNo);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlbutton.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowDetails(Int32 FacilityDetailNo)
    {
        DataSet ds = null;
        try
        {
            objFM.CenFacilityNo = FacilityDetailNo;
            ds = objFC.GetCentraFacilityDetailByNo(objFM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CenFacilityNo"] = FacilityDetailNo;
                txtFacilityName.Text = ds.Tables[0].Rows[0]["CenFacilityName"].ToString();
                txtDetail.Text = ds.Tables[0].Rows[0]["CenFacilityDetail"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
               
              

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                rptMinorList.DataSource = ds.Tables[1];
                rptMinorList.DataBind();
                for (int i = 0; i < rptMinorList.Items.Count;i++ )
                {
                  
                    CheckBox chkSelect = rptMinorList.Items[i].FindControl("chkSelect") as CheckBox;
                    string CHECK_STATUS = ds.Tables[1].Rows[i]["CHECK_STATUS"].ToString().Trim();
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
        txtFacilityName.Text=txtDetail.Text=txtRemark.Text = string.Empty;
        pnlbutton.Visible = false;
        pnlList.Visible = true;
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
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlbutton.Visible = true;
        ViewState["action"] = "add";
    }
    protected void BindFacilityDetail()
    {
        try
        {
            DataSet ds = objFC.GetCentraFacilityDetail();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //btnShowReport.Visible = false;
                //dpPager.Visible = false;
                lvFacilityDetail.DataSource = null;
                lvFacilityDetail.DataBind();
            }
            else
            {
                //btnShowReport.Visible = true;
              //  dpPager.Visible = true;

                lvFacilityDetail.DataSource = ds;
                lvFacilityDetail.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {

                rptMinorList.DataSource = ds.Tables[1];
                rptMinorList.DataBind();
            }
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
    //    BindFacilityDetail();
    //}

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("FacilityStatus", "Facility_Detail.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
          
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FACILITY_MANAGEMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FACILITY_MANAGEMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);




        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Status_Report.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
}

