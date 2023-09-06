//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : Leave_Period.aspx                                                   
// CREATION DATE : 15-06-2016
// CREATED BY    : SWATI GHATE                                  
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
using IITMS.UAIMS;
public partial class ESTABLISHMENT_LEAVES_Master_Leave_Period : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLC = new LeavesController();
    Leaves objLM = new Leaves();

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
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                fillPeriod();
                BindListView();

                btnAdd.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                CheckPageAuthorization();
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=Leave_Period.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Leave_Period.aspx");
        }
    }
    private void fillPeriod()
    {
        //select MNAME,TDAY from payroll_monfile
        objCommon.FillDropDownList(ddlPeriodFrom, "payroll_monfile", "MCODE", "MNAME", "", "MCODE");
        objCommon.FillDropDownList(ddlPeriodTo, "payroll_monfile", "MCODE", "MNAME", "", "MCODE");
    }
    protected void BindListView()
    {
        try
        {
            //SELECT PERIOD,PERIOD_NAME,PERIOD_FROM,PERIOD_TO FROM PAYROLL_LEAVE_PERIOD

           // DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_PERIOD L", "PERIOD,PERIOD_NAME", "PERIOD_FROM,PERIOD_TO", "", "PERIOD_NAME");

            DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_PERIOD L", "PERIOD,PERIOD_NAME", "DATENAME(month, DATEADD(month, PERIOD_FROM-1, CAST('2008-01-01' AS datetime))) as PERIOD_FROM,DATENAME(month, DATEADD(month, PERIOD_TO-1, CAST('2008-01-01' AS datetime))) as PERIOD_TO ", "", "PERIOD_NAME");

            if (ds.Tables[0].Rows.Count <= 0)
            {

                dpPager.Visible = false;
            }
            else
            {

                dpPager.Visible = true;
            }
            lvPeriod.DataSource = ds;
            lvPeriod.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListView();
    }

    private void Clear()
    {
        txtPeriodName.Text = string.Empty;
        ddlPeriodFrom.SelectedIndex = 0;
        ddlPeriodTo.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["PERIOD"] = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";

        btnAdd.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }


    public bool CheckPeriod()
    {
        bool result = false;
        DataSet dsPeriod = new DataSet();
        //SELECT PERIOD,PERIOD_NAME,PERIOD_FROM,PERIOD_TO FROM PAYROLL_LEAVE_PERIOD
        dsPeriod = objCommon.FillDropDown("PAYROLL_LEAVE_PERIOD", "*", "", "PERIOD_NAME='" + txtPeriodName.Text + "'", "");
        if (dsPeriod.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            bool result = CheckPeriod();
            objLM.PERIOD_NAME = txtPeriodName.Text.ToString().Trim().ToUpper();
            objLM.PERIOD_FROM = Convert.ToInt32(ddlPeriodFrom.SelectedValue);
            objLM.PERIOD_TO = Convert.ToInt32(ddlPeriodTo.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        Clear();
                        return;
                    }
                    objLM.PERIOD = 0;
                    CustomStatus cs = (CustomStatus)objLC.AddUpdatePeriod(objLM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListView();
                        ViewState["action"] = null;
                        Clear();

                        btnAdd.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnBack.Visible = false;
                    }
                }
                else
                {
                    if (ViewState["PERIOD"] != null)
                    {
                        objLM.PERIOD = Convert.ToInt32(ViewState["PERIOD"].ToString());
                        if (result == true)
                        {
                            DataSet ds = new DataSet();
                            //ds = objCommon.FillDropDown("PAYROLL_OD_PURPOSE", "*", "", "PURPOSENO=" + ViewState["PERIOD"].ToString().Trim() + "", "");                           
                            //if (txtPeriodName.Text.ToUpper() != ds.Tables[0].Rows[0]["PERIOD_NAME"].ToString().Trim().ToUpper())
                            ds = objCommon.FillDropDown("PAYROLL_LEAVE_PERIOD", "*", "", "PERIOD=" + ViewState["PERIOD"].ToString().Trim() + "", "");
                            if (txtPeriodName.Text.ToUpper() != ds.Tables[0].Rows[0]["PERIOD_NAME"].ToString().Trim().ToUpper())
                            {
                                //objCommon.DisplayMessage("Record Already Exist", this);
                                MessageBox("Record Already Exist");
                                txtPeriodName.Text = string.Empty;
                                txtPeriodName.Focus();
                                return;
                            }
                        }
                        CustomStatus cs = (CustomStatus)objLC.AddUpdatePeriod(objLM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListView();
                            ViewState["action"] = null;
                            Clear();

                            btnAdd.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        Clear();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        txtPeriodName.Text = string.Empty;
        ddlPeriodFrom.SelectedIndex = 0;
        ddlPeriodTo.SelectedIndex = 0;

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;

        btnAdd.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["PERIOD"] = int.Parse(btnEdit.CommandArgument);

            DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_PERIOD L", "PERIOD,PERIOD_NAME", "PERIOD_FROM,PERIOD_TO", "PERIOD=" + int.Parse(btnEdit.CommandArgument) + "", "PERIOD_NAME");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPeriodName.Text = ds.Tables[0].Rows[0]["PERIOD_NAME"].ToString();
                ddlPeriodFrom.SelectedValue = ds.Tables[0].Rows[0]["PERIOD_FROM"].ToString();
                ddlPeriodTo.SelectedValue = ds.Tables[0].Rows[0]["PERIOD_TO"].ToString();
            }
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnAdd.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }



}
