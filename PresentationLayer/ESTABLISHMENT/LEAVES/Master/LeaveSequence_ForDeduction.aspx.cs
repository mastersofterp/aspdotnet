//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : LeaveSequence_ForDeduction.aspx                                                     
// CREATION DATE : 23 Nov 2012
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
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_LeaveSequence_ForDeduction : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    LeavesController objLeaveSeq = new LeavesController();

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
                  //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                FillStaffType();               
                FillCollege();
                BindListViewLeaveSeq();

                btnAdd.Visible = true;
                btnShowReport.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                //CheckPageAuthorization();
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
                Response.Redirect("~/notauthorized.aspx?page=leaves.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=leaves.aspx");
        }
    }
    private void FillCollege()
    {
        //Added by Saket Singh on 14-dec-2016
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");        
        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }


    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
            objCommon.FillDropDownList(ddlAppoint, "PAYROLL_APPOINT", "APPOINTNO", "APPOINT", "APPOINTNO>0", "APPOINTNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListViewLeaveSeq()
    {
        try
        {
            Leaves objLeaves = new Leaves(); //Added by Saket Singh on 14-dec-2016
            objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue); //Added by Saket Singh on 14-dec-2016
            DataSet ds = objLeaveSeq.GetAllLeaveSeq(objLeaves);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                dpPager.Visible = false;
            }
            else
            {
                //btnShowReport.Visible = true;
                dpPager.Visible = true;
            }
            lvLeaveSeq.DataSource = ds;
            lvLeaveSeq.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain  
        BindListViewLeaveSeq();
    }

    private void Clear()
    {

        ddllv1.SelectedIndex = 0;
        ddllv2.SelectedIndex = 0;
        ddllv3.SelectedIndex = 0;
        ddllv4.SelectedIndex = 0;
        ddllv5.SelectedIndex = 0;
        ddllv2.Enabled = false;
        ddllv3.Enabled = false;
        ddllv4.Enabled = false;
        ddllv5.Enabled = false;
        txtPAPath.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        ddlAppoint.SelectedIndex = 0;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ddllv2.Enabled = false;
        ddllv3.Enabled = false;
        ddllv4.Enabled = false;
        ddllv5.Enabled = false;
        ViewState["action"] = "add";

        btnAdd.Visible = false;
        btnShowReport.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }

    private void FillLeave()
    {
        try
        {
            objCommon.FillDropDownList(ddllv1, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", "", "LEAVE_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;

        btnAdd.Visible = true;
        btnShowReport.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Leaves objLeaves = new Leaves();
            objLeaves.STNO = Convert.ToInt32(ddlStaff.SelectedValue);
            objLeaves.LEAVE01 = Convert.ToInt32(ddllv1.SelectedValue);
            objLeaves.LEAVE02 = Convert.ToInt32(ddllv2.SelectedValue);
            objLeaves.LEAVE03 = Convert.ToInt32(ddllv3.SelectedValue);
            objLeaves.LEAVE04 = Convert.ToInt32(ddllv4.SelectedValue);
            objLeaves.LEAVE05 = Convert.ToInt32(ddllv5.SelectedValue);
            objLeaves.LEAVESEQ = txtPAPath.Text;
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            //objLeaves .PERIOD = Convert.ToInt32(Session ["Period"]);
            objLeaves.PERIOD = 3;
            objLeaves.APPOINT_NO = Convert.ToInt32(ddlAppoint.SelectedValue);
            objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int chkLvSeqExist = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SEQUENCE_FOR_DEDUCTION", "COUNT(*)", "STNO=" + ddlStaff.SelectedValue + " AND APPOINTNO=" + ddlAppoint.SelectedValue + " AND COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue) + ""));

                    if (chkLvSeqExist == 0)
                    {
                        CustomStatus cs = (CustomStatus)objLeaveSeq.AddLeaveSeq(objLeaves);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();

                            btnAdd.Visible = true;
                            btnShowReport.Visible = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            objCommon.DisplayMessage("Record Saved Successfully", this);
                            BindListViewLeaveSeq();

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Record Already Exists For Same Staff Type and Appointment Type!", this);
                    }
                }
                else
                {
                    if (ViewState["LSNO"] != null)
                    {
                        objLeaves.LSNO = Convert.ToInt32(ViewState["LSNO"].ToString());
                        CustomStatus CS = (CustomStatus)objLeaveSeq.UpdateLeaveSeq(objLeaves);
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                            btnAdd.Visible = true;
                            btnShowReport.Visible = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;
                            objCommon.DisplayMessage("Record Updated Successfully", this);
                        }
                    }
                }
            }
            BindListViewLeaveSeq();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillLeave();
            ImageButton btnEdit = sender as ImageButton;
            int LSNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(LSNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            btnAdd.Visible = false;
            btnShowReport.Visible = false;
            btnSave.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int LSNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objLeaveSeq.DeleteLeaveSeq(LSNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 LSNO)
    {
        DataSet ds = null;
        try
        {
            ds = objLeaveSeq.GetSingleLeaveSeq(LSNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["LSNO"] = LSNO;
                ddlStaff.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                ddlAppoint.SelectedValue = ds.Tables[0].Rows[0]["APPOINTNO"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                //
                txtPAPath.Text = ds.Tables[0].Rows[0]["LEAVESEQ"].ToString();
                //ddllv1.SelectedValue = 40.ToString();
                ddllv1.SelectedValue = ds.Tables[0].Rows[0]["LEAVE01"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["LEAVE02"].ToString().Trim().Equals("0")))
                {
                    ddllv2.SelectedValue = ds.Tables[0].Rows[0]["LEAVE02"].ToString();
                    this.EnableDisable(2);
                    ddllv2.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["LEAVE03"].ToString().Trim().Equals("0")))
                {
                    ddllv3.SelectedValue = ds.Tables[0].Rows[0]["LEAVE03"].ToString();
                    this.EnableDisable(3);
                    ddllv3.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["LEAVE04"].ToString().Trim().Equals("0")))
                {
                    ddllv4.SelectedValue = ds.Tables[0].Rows[0]["LEAVE04"].ToString();
                    this.EnableDisable(4);
                    ddllv4.Enabled = true;

                }
                if (!(ds.Tables[0].Rows[0]["LEAVE05"].ToString().Trim().Equals("0")))
                {
                    ddllv5.SelectedValue = ds.Tables[0].Rows[0]["LEAVE05"].ToString();
                    this.EnableDisable(5);
                    ddllv5.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddllv1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddllv1_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void ddllv2_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);
            //int Period = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE","PERIOD","LNO="+ddllv2.SelectedValue));
            //Session["Period"] = Period.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddllv2_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddllv3_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddllv3_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddllv4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddllv4_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void EnableDisable(int index)
    {

        switch (index)
        {
            case 1:
                if (ddllv1.SelectedIndex == 0)
                {
                    ddllv2.SelectedIndex = 0;
                    ddllv2.Enabled = false;
                    ddllv3.SelectedIndex = 0;
                    ddllv3.Enabled = false;
                    ddllv4.SelectedIndex = 0;
                    ddllv4.Enabled = false;
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;
                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + ") AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddllv2, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv2, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");

                }
                else
                {

                    ddllv2.Enabled = true;
                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + ")"; 
                    //objCommon.FillDropDownList(ddllv2, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv2, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");
                    ddllv3.SelectedIndex = 0;
                    ddllv3.Enabled = false;
                    ddllv4.SelectedIndex = 0;
                    ddllv4.Enabled = false;
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;
                    txtPAPath.Text = ddllv1.SelectedItem.ToString();
                }

                break;

            case 2:
                if (ddllv2.SelectedIndex == 0)
                {
                    ddllv3.SelectedIndex = 0;
                    ddllv3.Enabled = false;
                    ddllv4.SelectedIndex = 0;
                    ddllv4.Enabled = false;
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;
                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + ") ";
                    // objCommon.FillDropDownList(ddllv3, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv3, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");

                }
                else
                {
                    ddllv3.Enabled = true;
                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + ") "; 
                    // objCommon.FillDropDownList(ddllv3, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv3, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");
                    ddllv4.SelectedIndex = 0;
                    ddllv4.Enabled = false;
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;

                    txtPAPath.Text = ddllv1.SelectedItem.ToString() + "->" + ddllv2.SelectedItem.ToString();
                }
                break;
            case 3:


                if (ddllv3.SelectedIndex == 0)
                {
                    ddllv4.SelectedIndex = 0;
                    ddllv4.Enabled = false;
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;

                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddllv4, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv4, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");
                }
                else
                {
                    ddllv4.Enabled = true;
                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + ")"; 
                    //objCommon.FillDropDownList(ddllv4, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv4, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;
                    txtPAPath.Text = ddllv1.SelectedItem.ToString() + "->" + ddllv2.SelectedItem.ToString() + "->" + ddllv3.SelectedItem.ToString();
                }

                break;
            case 4:

                if (ddllv4.SelectedIndex == 0)
                {
                    ddllv5.SelectedIndex = 0;
                    ddllv5.Enabled = false;

                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + "," + ddllv4.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + "," + ddllv4.SelectedValue + ")"; 
                    //objCommon.FillDropDownList(ddllv5, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv5, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");
                }
                else
                {
                    ddllv5.Enabled = true;
                    //string swhere = " LNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + "," + ddllv4.SelectedValue + ")AND STNO=" + ddlStaff.SelectedValue + "";
                    string swhere = " LVNO NOT IN (" + ddllv1.SelectedValue + "," + ddllv2.SelectedValue + "," + ddllv3.SelectedValue + "," + ddllv4.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddllv5, "PAYROLL_LEAVE", "LNO", " LEAVE +'---'+''+ Convert(NVARCHAR(20),(CASE PERIOD WHEN 1 THEN 'JUL-DEC' WHEN 2 THEN 'JAN-JUN' ELSE 'YEARLY' END)) +''", "STNO=" + ddlStaff.SelectedValue, "LEAVE");
                    objCommon.FillDropDownList(ddllv5, "PAYROLL_LEAVE_NAME", "LVNO", " LEAVE_NAME", swhere, "LEAVE_NAME");
                    txtPAPath.Text = ddllv1.SelectedItem.ToString() + "->" + ddllv2.SelectedItem.ToString() + "->" + ddllv3.SelectedItem.ToString() + "->" + ddllv4.SelectedItem.ToString();
                }

                break;
            case 5:
                if (!(ddllv4.SelectedIndex == 0))
                {
                    txtPAPath.Text = ddllv1.SelectedItem.ToString() + "->" + ddllv2.SelectedItem.ToString() + "->" + ddllv3.SelectedItem.ToString() + "->" + ddllv4.SelectedItem.ToString() + "->" + ddllv5.SelectedItem.ToString();
                }
                break;

        }

    }

    protected void ddllv5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddllv5_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLeave();
    }
}
