//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITCalculation.aspx                                                  
// CREATION DATE : 13-April-2011                                                      
// CREATED BY    : Ankit Agrawal                                                     
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


public partial class Pay_ITCalculation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    ITProcessController objITController = new ITProcessController();

    string UsrStatus = string.Empty;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    //Checking logon Status and redirection to Login Page(Default.aspx) if user is not logged in
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //function to fill dropdownlists
                //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
                //objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
                ddlStaff.Visible = true;
                //ddlStaff.SelectedIndex = 0;
                ddlCalculationBy.SelectedIndex = 0;
                trcalulationby.Visible = false;
                DateTime fdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITFDATE", ""));
                txtFromDate.Text = fdate.ToString();
                DateTime tdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITTDATE", ""));
                txtToDate.Text = tdate.ToString();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITCalculation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITCalculation.aspx");
        }
    }

    protected void rblCalculationBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblCalculationBy.SelectedIndex == 0)
        {
            //lblCalculationBy.Text = "Select Staff";
            trcalulationby.Visible = false;
            chkStaffWise.Visible = false;
            chkStaffWise.Checked = false;
        }
        else if (rblCalculationBy.SelectedIndex == 1)
        {
            lblCalculationBy.Text = "Select Employee";
            trcalulationby.Visible = true;
            ddlCalculationBy.Visible = true;
            //chkStaffWise.Visible = false;
            //rowStaffWise.Visible = false;
            //ddlStaff.SelectedIndex = 0;
        }
        FillDropdown();
    }

    //Function to fill dropdownlist for stafftype
    protected void FillDropdown()
    {
       
        if (rblCalculationBy.SelectedIndex == 0)
            //objCommon.FillDropDownList(ddlCalculationBy, "PAYROLL_COLLEGE", "COLLEGENO", "COLLEGENAME", "COLLEGENO>0", "COLLEGENO");
            ddlCalculationBy.Visible = false;
       else if (rblCalculationBy.SelectedIndex == 1)
            //objCommon.FillDropDownList(ddlCalculationBy, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),PFILENO) +']')", "IDNO>0 AND STAFFNO="+ddlStaff.SelectedValue, "IDNO");
            ddlCalculationBy.Visible = true;
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Pay_ITCalculation.aspx");
        if (ddlCollege.SelectedIndex > 0)
        {
            ddlCollege.SelectedIndex = 0;
        }
        else
        {
            ddlCollege.SelectedIndex = 0;
        }
        if (ddlStaff.SelectedIndex > 0)
        {
            ddlStaff.SelectedIndex = 0;
        }
        else
        {
            ddlStaff.SelectedIndex = 0;
        }
        if (ddlCalculationBy.SelectedIndex > 0)
        { 
           ddlCalculationBy.SelectedIndex =0;
        }
        else
        {
           ddlCalculationBy.SelectedIndex = 0;
        }
    }
    protected void chkStaffWise_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkStaffWise.Checked == true)
        //{
        //    rowStaffWise.Visible = true;
        //}
        //else
        //{
        //    rowStaffWise.Visible = false;
        //}
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {

        int status = 0;
        string fromDate=string.Empty;
        string toDate=string.Empty;
        int collegeNo=0;
        int staffNo=0;
        int idNo=0;
        int proposedSal=0;
        int userIdno=Convert.ToInt32(Session["idno"].ToString());
        fromDate = txtFromDate.Text.ToString().Trim();
        toDate = txtToDate.Text.ToString().Trim();
        if (rblCalculationBy.SelectedIndex == 0)
        {
            idNo = 0;
            //collegeNo = Convert.ToInt32(ddlCalculationBy.SelectedValue);
            //collegeNo = 0;
            collegeNo =Convert.ToInt32(ddlCollege.SelectedValue);
            staffNo = Convert.ToInt32(ddlStaff.SelectedValue);
            //staffNo = chkStaffWise.Checked == true ? Convert.ToInt32(ddlStaff.SelectedValue) : 0;
        }
        else
        {
            staffNo = 0;
            collegeNo = 0;
            idNo = Convert.ToInt32(ddlCalculationBy.SelectedValue);
        }
        proposedSal = chkProposedSalary.Checked == true ? 1 : 0;
        status = objITController.ITCalculation(fromDate,toDate,collegeNo,staffNo,idNo,proposedSal,userIdno);
        if (status != 0)
        {
            string msg = string.Empty;
            msg = status == 1 ? "INCOME TAX CALCULATION PROCESS SUCCESSFUL!!!" : "CALCULATION FAILED!!!";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        }
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCalculationBy, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),PFILENO) +']')", "IDNO>0 AND COLLEGE_NO="+ddlCollege.SelectedValue+" AND STAFFNO=" + ddlStaff.SelectedValue, "IDNO");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
    }
}
