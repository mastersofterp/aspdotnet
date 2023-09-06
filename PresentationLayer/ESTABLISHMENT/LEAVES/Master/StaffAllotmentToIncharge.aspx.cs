//======================================================================================
// PROJECT NAME  : UIAMS
// MODULE NAME   : 
// PAGE NAME     : StaffAllotmentToIncharge.aspx                                                    
// CREATION DATE : 
// CREATED BY    :                                
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;
using IITMS.UAIMS;

public partial class ESTABLISHMENT_LEAVES_Master_StaffAllotmentToIncharge : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    // LeaveInchargeEntity objSE = new LeaveInchargeEntity();
    // LeaveInchargeController objSC = new LeaveInchargeController();
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
                CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillCollege();
                //FillIncharge();
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
       // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);


            //objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "I.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND INCHARGEEMPLOYEEIDNO>0", "E.FNAME");

        }
    }
    private void FillIncharge()
    {
        //DataSet ds = objSC.GetInchargeList(Convert.ToInt32(ddlCollege.SelectedValue));
        objSE.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        DataSet ds = objSC.GetInchargeList(objSE);

        ddlIncharge.Items.Clear();
        ddlIncharge.Items.Add("Please Select");
        ddlIncharge.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlIncharge.DataSource = ds;
            ddlIncharge.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlIncharge.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlIncharge.DataBind();
            ddlIncharge.SelectedIndex = 0;
        }
        // objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "I.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND INCHARGEEMPLOYEEIDNO>0", "E.FNAME");
        int empidno = Convert.ToInt32(Session["idno"]);
        ds = objCommon.FillDropDown("PAYROLL_SHIFT_INCHARGEMASTER", "*", "", "INCHARGEEMPLOYEEIDNO=" + empidno + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "INCHARGEEMPLOYEEIDNO=" + empidno + "", "E.FNAME");
            ListItem removeItem = ddlIncharge.Items.FindByValue("0");
            ddlIncharge.Items.Remove(removeItem);
            //BindListView();
        }
        BindListView();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "I.COLLEGE_NO ="+ Convert.ToInt32(ddlCollege.SelectedValue) +" AND INCHARGEEMPLOYEEIDNO>0", "E.FNAME");
        objSE.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        DataSet ds = objSC.GetInchargeList(objSE);

        ddlIncharge.Items.Clear();
        ddlIncharge.Items.Add("Please Select");
        ddlIncharge.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlIncharge.DataSource = ds;
            ddlIncharge.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlIncharge.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlIncharge.DataBind();
            ddlIncharge.SelectedIndex = 0;
        }
        lvIncharge.DataSource = null;
        lvIncharge.DataBind();
        pnlIncharge.Visible = false;

        lvView.DataSource = null;
        lvView.DataBind();
        pnlView.Visible = false;
        btnTempRemove.Visible = btnPerRemove.Visible = btnSave.Visible = false;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = ddlIncharge.SelectedIndex = 0;
        lvIncharge.DataSource = null;
        lvIncharge.DataBind();
        pnlIncharge.Visible = false;
        lvView.DataSource = null;
        lvView.DataBind();
        pnlView.Visible = false;
        btnSave.Visible = btnTempRemove.Visible = btnPerRemove.Visible = false;
        FillCollege();
        FillIncharge();
        // btnSave.Visible = btnTempRemove.Visible = btnPerRemove.Visible = false;
        //BindListView();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Shift_Mgt", "Estb_StaffAllotmentToInchargeReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,SHIFT," + rptFileName;

            //@P_INCHARGEEMPLOYEEIDNO,@P_COLLEGE_NO
            url += "&param=@P_INCHARGEEMPLOYEEIDNO=" + Convert.ToInt32(ddlIncharge.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            // url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_PERIOD=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_LEAVENO=" + Convert.ToInt32(ddlLeaveName.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowReport->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("INCHARGEEMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("EMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COLLEGE_NO", typeof(int)));

            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            int inchrgeID = Convert.ToInt32(ddlIncharge.SelectedValue);
            objSE.COLLEGE_NO = collegeno;
            objSE.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);
            int count = 0;
            foreach (ListViewDataItem rptitem in lvIncharge.Items)
            {
                CheckBox chk = rptitem.FindControl("chkIncharge") as CheckBox;

                if (chk.Checked)
                {
                    count = 1;
                    CheckBox txt = rptitem.FindControl("chkIncharge") as CheckBox;
                    DataRow dr = dt.NewRow();

                    dr["INCHARGEEMPLOYEEIDNO"] = inchrgeID;
                    dr["EMPLOYEEIDNO"] = chk.ToolTip;
                    dr["COLLEGE_NO"] = Convert.ToInt32(collegeno);
                    dt.Rows.Add(dr);
                }
            }
            if (count != 1)
            {
                objCommon.DisplayMessage("Please Select Atleast One Employee", this.Page); return;
            }
            CustomStatus cs = (CustomStatus)objSC.InsertUpdateInchargeDetailMaster(dt, objSE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                //FillCollege();
                //FillIncharge();
                objCommon.DisplayMessage("Record Saved Successfully!", this.Page);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_StaffAllotmentToInchare.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnTempRemove_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("INCHARGEEMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("EMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COLLEGE_NO", typeof(int)));

            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            int inchrgeID = Convert.ToInt32(ddlIncharge.SelectedValue);
            objSE.COLLEGE_NO = collegeno;
            objSE.IsTempRemove = true;

            objSE.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);
            int count = 0; int temp_avail = 0;
            foreach (ListViewDataItem rptitem in lvView.Items)
            {
                CheckBox chk = rptitem.FindControl("chkStaff") as CheckBox;

                if (chk.Checked)
                {
                    count = 1;
                    CheckBox txt = rptitem.FindControl("chkStaff") as CheckBox;
                    DataRow dr = dt.NewRow();
                    // lblTempAvailable
                    Label lblTempAvailable = rptitem.FindControl("lblTempAvailable") as Label;
                    if (lblTempAvailable.Text.ToString().Trim() != "Y".ToString().Trim())
                    {
                        dr["INCHARGEEMPLOYEEIDNO"] = inchrgeID;
                        dr["EMPLOYEEIDNO"] = chk.ToolTip;
                        dr["COLLEGE_NO"] = Convert.ToInt32(collegeno);
                        dt.Rows.Add(dr);
                    }
                    if (lblTempAvailable.Text.ToString().Trim() == "Y".ToString().Trim())
                    {
                        temp_avail = 1;
                    }
                }
            }
            if (count != 1)
            {
                MessageBox("Please Select Atleast One record for Temp/Per. remove");
                return;
            }
            if (dt.Rows.Count == 0 && temp_avail == 1)
            {
                MessageBox("For Temp. Available Employee, please Click On Permanent Remove");
                return;
            }
            CustomStatus cs = (CustomStatus)objSC.TempPermanentRemoveInchargeDetails(dt, objSE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                //if (dt.Rows.Count != 0 && temp_avail == 1)
                //{
                //    MessageBox("For Temp. Available Employee, please Submit for Permanent Remove");
                //    return;
                //}
                // objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
                string str = "Record Saved Successfully!";
                //str=str +" For Temp. Available Employee, please Submit for Permanent Remove";
                objCommon.DisplayMessage(str, this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_StaffAllotmentToInchare.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPerRemove_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("INCHARGEEMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("EMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COLLEGE_NO", typeof(int)));

            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            int inchrgeID = Convert.ToInt32(ddlIncharge.SelectedValue);
            objSE.COLLEGE_NO = collegeno;
            objSE.IsPermanentRemove = true;

            objSE.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);
            int count = 0;
            foreach (ListViewDataItem rptitem in lvView.Items)
            {
                CheckBox chk = rptitem.FindControl("chkStaff") as CheckBox;

                if (chk.Checked)
                {
                    count = 1;
                    CheckBox txt = rptitem.FindControl("chkStaff") as CheckBox;
                    DataRow dr = dt.NewRow();

                    dr["INCHARGEEMPLOYEEIDNO"] = inchrgeID;
                    dr["EMPLOYEEIDNO"] = chk.ToolTip;
                    dr["COLLEGE_NO"] = Convert.ToInt32(collegeno);
                    dt.Rows.Add(dr);
                }
            }
            CustomStatus cs = (CustomStatus)objSC.TempPermanentRemoveInchargeDetails(dt, objSE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                //FillCollege();
                //FillIncharge();
                objCommon.DisplayMessage("Record Saved Successfully!", this.Page);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_StaffAllotmentToInchare.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListView()
    {
        try
        {
            if (ddlIncharge.SelectedValue.ToString() != "0")
            {
                objSE.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);
                objSE.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                DataSet ds = objSC.GetInchargeDetailsEmployees(objSE);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvIncharge.DataSource = ds.Tables[0];
                    lvIncharge.DataBind();
                    pnlIncharge.Visible = true;
                    //btnTempRemove.Visible = btnPerRemove.Visible = true;
                    btnSave.Visible = true;
                }
                else
                {
                    lvIncharge.DataSource = null;
                    lvIncharge.DataBind();
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
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lvView.DataSource = ds.Tables[1];
                    lvView.DataBind();
                    pnlView.Visible = true;
                    btnTempRemove.Visible = btnPerRemove.Visible = true;

                }
                else
                {
                    lvView.DataSource = null;
                    lvView.DataBind();
                    pnlView.Visible = false;
                    btnTempRemove.Visible = btnPerRemove.Visible = false;
                }
            }
            else
            {
                lvIncharge.DataSource = null;
                lvIncharge.DataBind();
                //pnlIncharge.Visible = false;

                lvView.DataSource = null;
                lvView.DataBind();
                // pnlView.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_StaffAllotmentToInchare.ddlIncharge_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlIncharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_StaffAllotmentToInchare.ddlIncharge_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}