//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Payroll
// PAGE NAME     : Pay_Salary_Remark.ASPX                                                    
// CREATION DATE : 17-NOV-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_Pay_Salary_Remark : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objPayroll = new PayController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null )
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                this.FillDropDown();
                butSubmit.Visible = false;
                butCancel.Visible = false;
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

    private void BindListViewList(int staffNo, string monYear, int collegeNo)
    {
        try
        {
            DataSet ds = objPayroll.GetEmployeesForRemark(staffNo, monYear,collegeNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                butSubmit.Visible = true;
                butCancel.Visible = true;
            }
            else
            {
                butSubmit.Visible = false;
                butCancel.Visible = false;
            }

            lvSalaryRemark.DataSource = ds;
            lvSalaryRemark.DataBind();
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Salary_Remark.BindListViewList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ShowEmployeesForCommonRemark_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Select MonthYear", this);
        }
        else
        {
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), ddlMonthYear.SelectedItem.Text,Convert.ToInt32(ddlCollege.SelectedValue));
            pnlSalaryRemark.Visible = true;
            butSubmit.Visible = true;
        }
    }
    
    protected void butSubmitCommonRemark_Click(object sender, EventArgs e)
    {
        try
        {
            //CustomStatus cs = (CustomStatus)objPayroll.UpdateMonthRemark(txtMonthRemark.Text, ddlMonthYear.SelectedItem.Text,ddlCollege.SelectedValue);
            CustomStatus cs = (CustomStatus)UpdateMonthRemarkNew(txtMonthRemark.Text, ddlMonthYear.SelectedItem.Text, Convert.ToInt32(ddlCollege.SelectedValue));

            objCommon.DisplayMessage(UpdatePanel1,"Record Updated Successfully", this);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Salary_Remark.butSubmitCommonRemark_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public int UpdateMonthRemarkNew(string monRemark, string monYear, int collegeNo)
    {
        int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            //UpdateDaysal
            SqlParameter[] sqlParams = new SqlParameter[]
                        {    
                             new SqlParameter("@P_MONYEAR",monYear),                            
                             new SqlParameter("@P_MONTHREMARK",monRemark),
                             new SqlParameter("@P_COLLEGENO",collegeNo)
                          
                        };
            if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_MONTH_REMARK", sqlParams, false) != null)
                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateMonthRemarkNew()-> " + ex.ToString());
        }
        return retStatus;
    }

   
    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem lvitem in lvSalaryRemark.Items)
            {
                HiddenField hididno = lvitem.FindControl("hididno") as HiddenField;
                TextBox txtlvCommonRemark = lvitem.FindControl("txtlvCommonRemark") as TextBox;
                TextBox txtlvRemark = lvitem.FindControl("txtlvRemark") as TextBox;
                CustomStatus cs = (CustomStatus)objPayroll.UpdateSalaryRemark(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(hididno.Value), txtlvCommonRemark.Text,txtlvRemark.Text, ddlMonthYear.SelectedItem.Text);
            }
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue),ddlMonthYear.SelectedItem.Text, Convert.ToInt32(ddlCollege.SelectedValue));
            objCommon.DisplayMessage(UpdatePanel1,"Record Updated Successfully", this);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Salary_Remark.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillDropDown()
    {
        try 
        {
            objCommon.FillDropDownList(ddlStaff, "payroll_staff", "staffno", "staff", "staffno>0", "staffno");
            objCommon.FillDropDownList(ddlMonthYear, "payroll_salfile A", "distinct monyear", "monyear", "sallock=0", "A.monyear");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Salary_Remark.FillSchemeSemester()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());      
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStaff.SelectedIndex = 0;
        pnlSalaryRemark.Visible = false;
        butSubmit.Visible = false;
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSalaryRemark.Visible = false;
        butSubmit.Visible = false;
    }
    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        pnlSalaryRemark.Visible = false;
        butSubmit.Visible = false;
    }
}
