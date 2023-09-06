//============================================
//CREATED BY: SHRIKANT BHARNE
//CREATED DATE: 17-03-2016
//PURPOSE: TO FILL DIRECT COM-OFF APPLICATION AND CREDIT DIRECTLY WITHOUT REQUEST.
//============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;


public partial class ESTABLISHMENT_LEAVES_Transactions_DirectComOff : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLeaveMaster = new Leaves();

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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //  FillUser();               

                ViewState["action"] = "add";

                FillCollege();
                FillDept();
            }
            //
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
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}

        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }

    }

    private void FillDept()
    {
        objCommon.FillDropDownList(ddldeprtment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
    }

    protected void ddldeprtment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddldeprtment.SelectedValue) > 0)
            {
                FillUser();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void FillUser()
    {
        try
        {
            DataSet ds = null;
            //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "ENAME");
            //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.SUBDEPTNO =" + Convert.ToInt32(ddldeprtment.SelectedValue), "ENAME");
            if (ddldeprtment.SelectedIndex > 0)
            {
                BindListViewEmployees(0, Convert.ToInt32(ddldeprtment.SelectedValue), 0, 4);
                pnlList.Visible = true;
            }
            else
            {
                lvEmpList.DataSource = null;
                lvEmpList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        //ddlEmp.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddldeprtment.SelectedIndex = 0;
        txtReason.Text = txtWDate.Text = string.Empty;
        pnlList.Visible = false;
        lvEmpList.DataBind();
        lvEmpList.DataSource = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlEmp.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddldeprtment.SelectedIndex = 0;

        txtWDate.Text = txtReason.Text = txtdays.Text = string.Empty;
        pnlList.Visible = false;
        lvEmpList.DataBind();
        lvEmpList.DataSource = null;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int DPTNO;
            string Reason;
            int idno = 0;
            int cs = 0;
            //int COMPOFF_VALID_MONTH = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "ISNULL(COMPOFF_VALID_MONTH,1)", ""));

            // DateTime EXPIRY_DATE = Convert.ToDateTime(ViewState["EXPIRY_DATE"]);
            double NO_OF_DAYS = Convert.ToDouble(txtdays.Text);
            //int idno = Convert.ToInt32(Session["idno"]);

            int checkcount = 0;
            int instCount = 0;
            string selectedIDs = string.Empty;
            foreach (ListViewDataItem lvItem in lvEmpList.Items)
            {
                CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
                string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";

                if (chk.Checked && chk != null)
                {
                    checkcount += 1;
                    idno = Convert.ToInt32(chk.ToolTip);

                    if (ddldeprtment.SelectedIndex > 0)
                    {
                        DPTNO = Convert.ToInt32(ddldeprtment.SelectedValue);
                    }
                    else
                    {
                        DPTNO = 0;
                    }

                    if (txtReason.Text != string.Empty)
                    {
                        Reason = txtReason.Text;
                    }
                    else
                    {
                        Reason = string.Empty;
                    }
                    if (NO_OF_DAYS > 1)
                    {
                        MessageBox("Only 1 Days Can Credit");
                        return;
                    }
                    if (NO_OF_DAYS <= 0)
                    {
                        MessageBox("Not allow to Credit 0 Days");
                        return;
                    }


                    cs = objApp.DirectAddCompOff(idno, Convert.ToDateTime(txtWDate.Text), txtReason.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), 'C', DPTNO, NO_OF_DAYS);

                }

            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Employee");
                return;
            }
            if (checkcount > 0)
            {
                MessageBox("Record Saved Successfully");
                ViewState["action"] = null;
                Clear();

            }           

        }
        catch (Exception ex)
        {

        }

    }


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }
    protected void txtWDate_TextChanged(object sender, EventArgs e)
    {
        DateTime Test;
        if (DateTime.TryParseExact(txtWDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (Convert.ToDateTime(txtWDate.Text) > System.DateTime.Now)
            {
                MessageBox("Date Of Working Should Not be greater Than Current Date");
                txtWDate.Text = string.Empty;
                return;
            }
        }
        else
        {
            // MessageBox("Date Of Birth Not a Correct foramt");
            txtWDate.Text = string.Empty;
        }
    }


    protected void BindListViewEmployees(int collegeno, int deptno, int StNo, int tranno)
    {
        try
        {
            if (ddldeprtment.SelectedIndex >= 0)
            {
                DataSet ds = objApp.RetrieveAllEmployee(collegeno, deptno, StNo, tranno);
                lvEmpList.DataSource = ds.Tables[0];
                lvEmpList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}