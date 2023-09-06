//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ChangeInMasterFile.ASPX                                                    
// CREATION DATE : 25-JULY-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
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


public partial class PayRoll_Pay_ChangeInMasterFile : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();
    ChangeInMasterFileController ObjChangeMstFile = new ChangeInMasterFileController();
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

        // CheckRef();

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
                pnlSelect.Visible = true;
                pnlMonthlyChanges.Visible = false;

                FillPayHead(Convert.ToInt32(Session["userno"].ToString()));
                FillStaff();
                FillDepartment();
                FillPayHead();
            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty; 
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_ChangeInMasterFile.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ChangeInMasterFile.aspx");
        }
    }

    private void BindListViewList(string payHead,int staffNo, string payRule,string Dept,int collegeNo)
    {
        try
        {
            //if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            //{
                string orderby;

                if (ddlorderby.SelectedValue == "0")
                {
                    orderby = "IDNO";
                }
                else if (ddlorderby.SelectedValue == "2")
                {
                    orderby = "SEQ_NO";
                }
                else if (ddlorderby.SelectedValue == "3")
                {
                    orderby = "PFILENO";
                }
                else if (ddlorderby.SelectedValue == "4")
                {
                    orderby = "FNAME";
                }
                else
                {
                    if (ddlorderby.SelectedValue == "1")
                        orderby = "IDNO";
                    else
                        orderby = "SEQ_NO";

                }


                pnlMonthlyChanges.Visible = true;
                DataSet ds = ObjChangeMstFile.GetEmployeesForAmountDeduction(payHead, staffNo, orderby, payRule, Dept, collegeNo, Convert.ToInt32(ddlSubPayHead.SelectedValue),Convert.ToInt32(ddlEmployeeType.SelectedValue));
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }

                lvMonthlyChanges.DataSource = ds;
                lvMonthlyChanges.DataBind();
           // }

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
            
            foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
            {
                TextBox txt = lvitem.FindControl("txtDays") as TextBox;

                if (ddlSubPayHead.SelectedValue != "0" && (txt.Text != "0.00" || txt.Text != "0"))
                {
                    bool isMainHead = Convert.ToBoolean(objCommon.LookUp("PAYROLL_PAY_SUBPAYHEAD", "ISNULL(MAINHEADSTATUS,0)", "SUBHEADNO =" + Convert.ToInt32(ddlSubPayHead.SelectedValue) + ""));

                    CustomStatus cs1 = (CustomStatus)ObjChangeMstFile.AddSubPayheadAmount(Convert.ToInt32(txt.ToolTip), ddlPayhead.SelectedValue.ToString(), ddlPayhead.SelectedItem.Text, Convert.ToDecimal(txt.Text), Convert.ToInt32(ddlSubPayHead.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue), isMainHead);
                    count = 1;
                }
                else
                {

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayHeadAmount(ddlPayhead.SelectedValue, Convert.ToDecimal(txt.Text), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
                }
            }

            if (count == 1)
            {   
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage("Record Updated Successfully", this);
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

        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            txt.Text = string.Empty;
        }
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlPayhead.SelectedIndex = 0;
        ddlEmployeeType.SelectedIndex = 0;
        ddlorderby.SelectedIndex = 0;
    }

    protected void FillPayHead(int uaNo)
    {
        try
        {   
            PayHeadPrivilegesController objPayHead =new PayHeadPrivilegesController();
            DataSet ds = null;
            ds = objPayHead.EditPayHeadUserEarnings(uaNo);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPayhead.DataSource = ds;
                ddlPayhead.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlPayhead.DataTextField = ds.Tables[0].Columns[2].ToString();
                ddlPayhead.DataBind();
                ddlPayhead.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
 
    protected void FillStaff()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff,"PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDeptmon, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPTNO ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
       lblmsg.Text = string.Empty;
       lblerror.Text = string.Empty;
       //int value = Convert.ToInt32(ddlpayruleselect.SelectedValue);

       int subheadnocnt = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAY_SUBPAYHEAD", "COUNT(1)", "PAYHEAD = '" + ddlPayhead.SelectedValue + "' AND PAYHEAD LIKE '%I%'"));

       if (subheadnocnt > 0 && ddlSubPayHead.SelectedValue != "0")
       {
           BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue));
       }
       else if (subheadnocnt == 0 && ddlSubPayHead.SelectedValue == "0")
       {
           BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue));
       }

       else
       {
          // objCommon.DisplayMessage("Please select sub pay head", this);
           BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue));

       }

       
       //to display employee count in footer
       txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChanges.Items.Count);

       //Used in javascript to display payhead desc
       hidPayhead.Value = ddlPayhead.SelectedItem.ToString();
       
       //display the total amount of payhead in footer 
       txtPayheadName.Text = "Total Amount of " + ddlPayhead.SelectedItem.ToString() + " = ";
       this.TotalPayheadAmount();
    }


    protected void TotalPayheadAmount()
    {
        decimal totalPayheadAmount=0;

        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            totalPayheadAmount = totalPayheadAmount + Convert.ToDecimal(txt.Text);            
        }

        txtAmount.Text=totalPayheadAmount.ToString();
      
    }

    private void ShowMessage(string message)
    {   
        if (message != string.Empty)
        {
            message = message.Replace("'", "\'");
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void FillPayHead()
    {
        try
        {
            // objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYFULL", "TYPE='C'", "SRNO");
            objCommon.FillDropDownList(ddlpayruleselect, "payroll_rule", "PAYRULE", "PAYRULE", "", "RULENO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlCollege.SelectedIndex = 0;
        //ddlStaff.SelectedIndex = 0;
       // ddlDeptmon.SelectedIndex = 0;
       // ddlorderby.SelectedIndex = 0;
       // ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;

        objCommon.FillDropDownList(ddlSubPayHead, "PAYROLL_PAY_SUBPAYHEAD", "SUBHEADNO", "FULLNAME", "PAYHEAD='" + ddlPayhead.SelectedValue + "' AND PAYHEAD LIKE '%I%' ", "SUBHEADNO");

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlStaff.SelectedIndex = 0;
        //ddlDeptmon.SelectedIndex = 0;
       // ddlorderby.SelectedIndex = 0;
        //ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDeptmon.SelectedIndex = 0;
        //ddlorderby.SelectedIndex = 0;
        //ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlDeptmon_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddlorderby.SelectedIndex = 0;
       // ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlpayruleselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlSubPayHead_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddlCollege.SelectedIndex = 0;
       // ddlStaff.SelectedIndex = 0;
       // ddlDeptmon.SelectedIndex = 0;
        // ddlorderby.SelectedIndex = 0;
      //  ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
}
