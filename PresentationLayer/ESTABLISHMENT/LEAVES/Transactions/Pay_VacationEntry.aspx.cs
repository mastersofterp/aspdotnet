//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : AssignShift.aspx                                                   
// CREATION DATE : 5 july 2012
// CREATED BY    : Mrunal Bansod                                       
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;



public partial class ESTABLISHMENT_LEAVES_Transactions_Pay_VacationEntry : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objShift = new LeavesController();
    Shifts objEshifts = new Shifts();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                CheckPageAuthorization();
                FillCollege();
                FillDepartment();
                FillStaffType();

            }
        }
        divMsg.InnerHtml = string.Empty;

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }     
    private void FillDepartment()
    {
        try
        {
            // objCommon.FillDropDownList(ddlShift, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        
        ddlDept.SelectedIndex = ddlStafftype.SelectedIndex = ddlCollege.SelectedIndex = 0;
        ViewState["selectedDates"] = null;
        pnlAlloted.Visible = pnlList.Visible = false;

        lvEmpList.DataSource = null;
        //lvEmpList.DataBind();

        lvView.DataSource = null;
        //lvView.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int checkcount = 0;
            int instCount = 0;
            string selectedIDs = string.Empty;
            DataTable dtAppRecord = new DataTable();
            dtAppRecord.Columns.Add("IDNO");
            dtAppRecord.Columns.Add("FROM_DATE");
            dtAppRecord.Columns.Add("TO_DATE");

            foreach (ListViewDataItem lvItem in lvEmpList.Items)
            {
                CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
                string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
                TextBox txtToDate = lvItem.FindControl("txtToDt") as TextBox;
                TextBox txtFromDate = lvItem.FindControl("txtFromDt") as TextBox;


                if (chk.Checked == true)
                {
                    checkcount += 1;
                    selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
                    DataRow dr = dtAppRecord.NewRow();
                    dr["IDNO"] = Convert.ToInt32(chk.ToolTip);
                    dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
                    dr["TO_DATE"] = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");


                    dtAppRecord.Rows.Add(dr);
                    dtAppRecord.AcceptChanges();
                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Employee");
                return;
            }
            //objEshifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objEshifts.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            CustomStatus cs = (CustomStatus)objShift.AddVacation(dtAppRecord, objEshifts);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record saved successfully");
                BindListViewVacation();
                Clear();
            }
         //   BindListViewEmployees();
           //

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void BindListViewVacation()
    {
        try
        {
            DataSet ds = objShift.GetVacationInfo(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue));
            if (ds.Tables[1].Rows.Count > 0)
            {
                lvView.DataSource = ds.Tables[1];
                lvView.DataBind();
               // pnlAlloted.Visible = true;
                lvView.Visible = true;
            }
            else
            {
                lvView.DataSource = null;
                lvView.DataBind();
                //pnlAlloted.Visible = false;

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpList.DataSource = ds.Tables[0];
                lvEmpList.DataBind();
                lvEmpList.Visible = true;
                pnlList.Visible = true;
            }
            else
            {
                lvEmpList.DataSource = null;
                lvEmpList.DataBind();
                // pnlList.Visible = false;
            }

          

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transaction.BindListViewVacation -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Holidays_Entry", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }


    //protected void rblEmp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblEmp.SelectedValue == "0")
    //    {
    //        trdept.Visible = false;
    //        BindListViewEmployees(Convert.ToInt32(rblEmp.SelectedValue), 0);
    //    }
    //    else
    //    {
    //        trdept.Visible = true;
    //        //BindListViewEmployees(Convert.ToInt32(rblEmp.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue));
    //    }
    //}

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_master", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 Vacno = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objShift.DeleteVacationEntry(Vacno);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                MessageBox("Record is Deleted Successfully");
                //Clear();
                BindListViewVacation();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindListViewVacation();
      

    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewVacation();
       

    }
}

