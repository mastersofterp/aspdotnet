//======================================================================================
// PROJECT NAME  : UIAMS
// MODULE NAME   : PAYROLL
// PAGE NAME     : LeaveInchargeMaster.aspx                                                    
// CREATION DATE : 06-10-2016
// CREATED BY    : Suraj Choudhari                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;

public partial class ESTABLISHMENT_LEAVES_Master_ShiftInchargeMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //LeaveInchargeEntity objSE = new LeaveInchargeEntity();
    //LeaveInchargeController objSC = new LeaveInchargeController();
    ShiftInchargeEntity objSE = new ShiftInchargeEntity();
    ShiftInchargeController objSC = new ShiftInchargeController();
    string UsrStatus = string.Empty;

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               // CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillCollege();
                FillStaffTypeDepartment();
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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);

        }
    }
    private void FillStaffTypeDepartment()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_LeaveCancellationReport.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillDepartment()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_LeaveCancellationReport.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindEmployeeList()
    {
        try
        {
            objSE.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSE.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objSE.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
            DataSet ds = objSC.GetInchargeEmployees(objSE);
            if (ds.Tables[0].Rows.Count >= 0)
            {
                lvIncharge.DataSource = ds;
                lvIncharge.DataBind();
                pnlIncharge.Visible = true;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    CheckBox chk = lvIncharge.Items[i].FindControl("chkIncharge") as CheckBox;

                    Boolean checktrue = Convert.ToBoolean(ds.Tables[0].Rows[i]["CHECKED"].ToString());

                    if (checktrue)
                        chk.Checked = true;
                    else
                        chk.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_LeaveInchargeMaster.ddlCollege_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // FillStaffTypeDepartment();
            if (ddlStaffType.SelectedIndex != 0)
            {
                BindEmployeeList();
            }
            else
            {
                lvIncharge.DataSource = null;
                lvIncharge.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_LeaveInchargeMaster.ddlCollege_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillStaffTypeDepartment();


            if (ddlStaffType.SelectedIndex != 0)
            {
                BindEmployeeList();
            }
            else
            {
                lvIncharge.DataSource = null;
                lvIncharge.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_LeaveInchargeMaster.ddlCollege_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlStaffType.SelectedIndex != 0 && ddlCollege.SelectedIndex >= 0)
            {
                BindEmployeeList();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_LeaveInchargeMaster.ddlCollege_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = ddlStaffType.SelectedIndex = ddlDept.SelectedIndex = 0;
        lvIncharge.DataSource = null;
        lvIncharge.DataBind();
        pnlIncharge.Visible = false;
        txtWardNo.Text = string.Empty;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("INCHARGEEMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COLLEGE_NO", typeof(int)));
            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            objSE.COLLEGE_NO = collegeno;
            objSE.WARD_NO = txtWardNo.Text.ToString().Trim();
            objSE.Created_By = Convert.ToInt32(Session["userno"]);
            int count = 0;
            foreach (ListViewDataItem rptitem in lvIncharge.Items)
            {
                CheckBox chk = rptitem.FindControl("chkIncharge") as CheckBox;

                if (chk.Checked)
                {
                    count = 1;
                    CheckBox txt = rptitem.FindControl("chkIncharge") as CheckBox;
                    DataRow dr = dt.NewRow();

                    dr["INCHARGEEMPLOYEEIDNO"] = chk.ToolTip;
                    dr["COLLEGE_NO"] = Convert.ToInt32(collegeno);
                    dt.Rows.Add(dr);
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage("Please Select Atleast One Employee!", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objSC.InsertUpdateInchargeMaster(dt, objSE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindEmployeeList();
                objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
                // ViewState["lblSHIFTNO"] = null;
                // Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("INCHARGEEMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COLLEGE_NO", typeof(int)));
            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            objSE.COLLEGE_NO = collegeno;
            objSE.WARD_NO = txtWardNo.Text.ToString().Trim();
            objSE.Created_By = Convert.ToInt32(Session["userno"]);

            int count = 0;
            foreach (ListViewDataItem rptitem in lvIncharge.Items)
            {
                CheckBox chk = rptitem.FindControl("chkIncharge") as CheckBox;

                if (chk.Checked)
                {
                    count = 1;
                    CheckBox txt = rptitem.FindControl("chkIncharge") as CheckBox;
                    DataRow dr = dt.NewRow();

                    dr["INCHARGEEMPLOYEEIDNO"] = chk.ToolTip;
                    dr["COLLEGE_NO"] = Convert.ToInt32(collegeno);
                    dt.Rows.Add(dr);
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage("Please Select Atleast One Employee!", this.Page);
                return;
            }
            //CustomStatus cs = (CustomStatus)objSC.InsertUpdateInchargeMaster(dt, objSE);
            CustomStatus cs = (CustomStatus)objSC.RemoveUpdateInchargeMaster(dt, objSE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindEmployeeList();
                objCommon.DisplayMessage("Record Removed Successfully!", this.Page);
                // ViewState["lblSHIFTNO"] = null;
                // Clear();
            }
        }
        catch (Exception ex)
        {

        }

    }
}