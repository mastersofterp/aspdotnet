using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_DetentionLeaveConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objDetLV = new LeavesController();

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
                CheckPageAuthorization();
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;

                BindListViewDetLVConfig();
                FillStaffType();
                FillLeaveName();
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=DetentionLeaveConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DetentionLeaveConfiguration.aspx");
        }
    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_DetentionLeaveConfiguration.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillLeaveName()
    {
        try
        {
            objCommon.FillDropDownList(ddlleaveshrtname, "Payroll_Leave_Name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_DetentionLeaveConfiguration.FillLeaveName ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void Clear()
    {
        ddlStafftype.SelectedIndex = 0;
        ddlleaveshrtname.SelectedIndex = 0;
        ViewState["HTNO"] = null;
        ViewState["action"] = "add";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;

        ViewState["action"] = "add";
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;

        btnAdd.Visible = false;
        //btnShowReport.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        ddlStafftype.SelectedIndex = 0;
        ddlleaveshrtname.SelectedIndex = 0;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;

        btnAdd.Visible = true;
        //btnShowReport.Visible = true;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = CheckPurpose();
            Leaves objLeaves = new Leaves();
            int DLNO = 0;
            objLeaves.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            objLeaves.LEAVENO = Convert.ToInt32(ddlleaveshrtname.SelectedValue);
            objLeaves.UANO = Convert.ToInt32(Session["userno"]);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        MessageBox("Record Already Exist");
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objDetLV.AddUpdateDetLVConfig(objLeaves, DLNO);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewDetLVConfig();
                            ViewState["action"] = null;
                            Clear();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            btnAdd.Visible = true;
                            //btnShowReport.Visible = true;
                        }
                    }
                }
                else
                {
                    if (ViewState["DLNO"] != null)
                    {
                        if (result == true)
                        {
                            MessageBox("Record Already Exist");
                            return;
                        }
                        else
                        {
                            DLNO = Convert.ToInt32(ViewState["DLNO"].ToString());
                            CustomStatus cs = (CustomStatus)objDetLV.AddUpdateDetLVConfig(objLeaves, DLNO);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Updated Successfully");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                BindListViewDetLVConfig();
                                ViewState["action"] = null;
                                Clear();
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                btnBack.Visible = false;

                                btnAdd.Visible = true;
                                //btnShowReport.Visible = true;
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_DetentionLeaveConfiguration.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAY_DETENTION_LEAVE_CONFIG", "*", "", "(STNO='" + Convert.ToInt32(ddlStafftype.SelectedValue) + "' AND LEAVENO='" + Convert.ToInt32(ddlleaveshrtname.SelectedValue) + "') OR STNO='" + Convert.ToInt32(ddlStafftype.SelectedValue) + "' AND DLNO != '" + Convert.ToInt32(ViewState["DLNO"]) + "' ", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    protected void BindListViewDetLVConfig()
    {
        try
        {
            int DLNO = 0;
            DataSet ds = objDetLV.GetDetentionLeaveConfig(DLNO);
            //if (ds.Tables[0].Rows.Count <= 0)
            //{
            //    btnShowReport.Visible = false;
            //    //  dpPager.Visible = false;
            //}
            //else
            //{
            //    btnShowReport.Visible = true;
            //    // dpPager.Visible = true;
            //}
            lvDetLeave.DataSource = ds;
            lvDetLeave.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_DetentionLeaveConfiguration.BindListViewDetLVConfig ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int DLNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(DLNO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

            btnAdd.Visible = false;
            //btnShowReport.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetails(Int32 DLNO)
    {
        DataSet ds = null;
        try
        {
            ds = objDetLV.GetDetentionLeaveConfig(DLNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["DLNO"] = DLNO;
                ddlStafftype.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                ddlleaveshrtname.SelectedValue = ds.Tables[0].Rows[0]["LEAVENO"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}