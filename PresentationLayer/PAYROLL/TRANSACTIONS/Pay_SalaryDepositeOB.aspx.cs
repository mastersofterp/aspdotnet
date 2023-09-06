//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_SalaryDepositeOB.ASPX                                                    
// CREATION DATE : 26-MARCH-2016                                                        
// CREATED BY    : SURAJ CHOUDHARI
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

public partial class PAYROLL_TRANSACTIONS_Pay_SalaryDepositeOB : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();
    PayController objPC = new PayController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                pnlAttendance.Visible = false;
                
                FillDropdown();
                
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Salary_Remark.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Salary_Remark.aspx");
        }
    }
    

    private void BindListViewList()
    {

        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {

                pnlAttendance.Visible = true;
                DataSet ds = objPC.GetSalOBOfEmployee(Convert.ToInt32(ddlStaff.SelectedValue),0,Convert.ToInt32(ddlCollege.SelectedValue));
                if (ds.Tables[0].Rows.Count <= 0)
                {

                   
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {

                    //  lblcount.Text = ds.Tables[0].Rows.Count.ToString();

                    
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }

                lvAttendance.DataSource = ds;
                lvAttendance.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvAttendance.Items)
            {
                TextBox txt = lvitem.FindControl("txtDays") as TextBox;
                CustomStatus cs = (CustomStatus)objPC.UpdateSALOB(Convert.ToDecimal(txt.Text), Convert.ToDateTime(txtOBDate.Text), Convert.ToInt32(txt.ToolTip));                                                                          
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }

            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);

            }

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        foreach (ListViewDataItem lvitem in lvAttendance.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            txt.Text = string.Empty;
        }
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlAttendance.Visible = false;

    }

    protected void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkIdno_CheckedChanged(object sender, EventArgs e)
    {
        
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void chkAbsent_CheckedChanged(object sender, EventArgs e)
    {
        
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }




    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListViewList();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlAttendance.Visible = false;
        ddlStaff.SelectedIndex = 0;
        txtOBDate.Text = string.Empty;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlAttendance.Visible = false;
        txtOBDate.Text = string.Empty;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
}
