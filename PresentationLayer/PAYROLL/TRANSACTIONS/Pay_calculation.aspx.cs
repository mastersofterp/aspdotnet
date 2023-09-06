//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayRoll_Pay_calculation.ASPX                                                    
// CREATION DATE : 22-AUG-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class PayRoll_Pay_calculation : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    SalaryProcessController objSalController = new SalaryProcessController();

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

                FillStaffDropdown();
                cetxtStartDate.SelectedDate = System.DateTime.Today;
                string txt;
                txt = System.DateTime.Today.ToString("MM/yyyy");
                butSalaryProcess.Text = "Salary Process For" + " " + txt + " " + " Month ";
                trspace.Visible = false;
                trselection.Visible = false;
                //UpdateProgress1.Visible = false;
            }
        }
        else 
        {
            cetxtStartDate.SelectedDate = null;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_calculation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_calculation.aspx");
        }
    }

    protected void FillStaffDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {   
         
        butSalaryProcess.Text = "Salary Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper()+"."+ " " + "Month ";
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {   
         //string monYear;
         //int checkSalaryProcess;
         //monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM") + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
         //checkSalaryProcess = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + monYear + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue)));
        //int count = this.checkSalaryProcess();
        //UpdateProgress1.Visible = true;
        lblerror.Text = string.Empty;
        if (checkSalaryProcess() > 0)
         {
             //checkSalaryProcess= CheckSalaryProcess(txtMonthYear, Convert.ToInt32(ddlStaff.SelectedValue));
             if (ddlStaff.SelectedIndex == 0)
             {
                 trspace.Visible = false;
                 trselection.Visible = false;
                 radSelectedEmployees.Checked = false;
                 radAllEmployees.Checked = true;
                 tremployee.Visible = false;
             }
             else
             {
                
                 trspace.Visible = true;
                 trselection.Visible = true;

                 if (radSelectedEmployees.Checked)
                 {
                     FillEmployee();
                 }

             }
         }
         else
         {
             trspace.Visible = false;
             trselection.Visible = false;          
         }
    }

    protected void FillEmployee()
    {
        try
        {
            //objCommon.FillListBox(lstEmployee,"PAYROLL_EMPMAS","IDNO", "'[' + convert(nvarchar(50),idno) +']'+ ' ' + isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + ddlStaff.SelectedValue +" AND COLLEGE_NO=" + ddlCollege.SelectedValue , "IDNO");
            lstEmployee.Items.Clear();
            if (ddlorderby.SelectedValue == "1")
            {            
                objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", "'[' + convert(nvarchar(50),idno) +']'+ ' ' + isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + ddlStaff.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "IDNO");
            }
            else if (ddlorderby.SelectedValue == "2")
            {
                objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", "'[' + convert(nvarchar(50),PFILENO) +']'+ ' ' + isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + ddlStaff.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "PFILENO");
            }
            else if (ddlorderby.SelectedValue == "3")
            {
                objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", "'[' + convert(nvarchar(50),SEQ_NO) +']'+ ' ' + isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + ddlStaff.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "SEQ_NO");
            }
            else if (ddlorderby.SelectedValue == "4")
            {
                objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", " isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + ddlStaff.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", "'[' + convert(nvarchar(50),idno) +']'+ ' ' + isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + ddlStaff.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "IDNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void radSelectedEmployees_CheckedChanged(object sender, EventArgs e)
    {
        tremployee.Visible = true;
        tr_OrderBy.Visible = true;
        FillEmployee();
    }

    protected void radAllEmployees_CheckedChanged(object sender, EventArgs e)
    {
        tremployee.Visible = false;
        tr_OrderBy.Visible = false;
    }

    protected void butSalaryProcess_Click(object sender, EventArgs e)
    {
        string status;        
        lblerror.Text = ""; 
        //int count=this.checkSalaryProcess();
        if (checkSalaryProcess() > 0)        {
            
            if (radSelectedEmployees.Checked)
            {
                string employeelist = string.Empty;
                for (int i = 0; i < lstEmployee.Items.Count; i++)
                {
                    if (lstEmployee.Items[i].Selected)
                    {
                        employeelist += lstEmployee.Items[i].Value + ",";
                    }
                }
                employeelist = employeelist.Substring(0, employeelist.Length - 1);
            
                status = objSalController.PayRollCalculation(employeelist, Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlStaff.SelectedValue), txtMonthYear.Text, Session["colcode"].ToString(),Convert.ToInt32(ddlCollege.SelectedValue));

                if (status != null || status != "" || status != string.Empty)
                {  
                    //lblerror.Text = status;
                    objCommon.DisplayMessage(UpdatePanel1, status, this);
                }
            }
            else
            {
                status = objSalController.PayRollCalculation("0", Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlStaff.SelectedValue), txtMonthYear.Text, Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue));
                if (status != null || status != "" || status != string.Empty)
                {
                    //lblerror.Text = status;
                    objCommon.DisplayMessage(UpdatePanel1, status, this);
                }
            }
        }
        else
        {
            status = objSalController.PayRollCalculation("0", Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlStaff.SelectedValue), txtMonthYear.Text, Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue));
            if (status != null || status != "" || status != string.Empty)
            {
                //lblerror.Text = status;
                objCommon.DisplayMessage(UpdatePanel1, status, this);
            }
        }
        
        System.Threading.Thread.Sleep(1000);

    }

    protected int checkSalaryProcess()
    {
        string monYear;
        int count; 
        monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
        count   = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + monYear + "' AND COLLEGE_NO="+ Convert.ToInt32(ddlCollege.SelectedValue)+" AND staffno=" + Convert.ToInt32(ddlStaff.SelectedValue)));
        return count;
    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
}
