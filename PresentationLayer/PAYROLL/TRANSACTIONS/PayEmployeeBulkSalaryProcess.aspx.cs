//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayEmployeeBulkSalaryProcess.aspx                                                  
// CREATION DATE : 14-01-2022                                                     
// CREATED BY    : PURVA RAUT                                                      
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

public partial class PAYROLL_TRANSACTIONS_PayEmployeeBulkSalaryProcess : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    SalaryProcessController objSalController = new SalaryProcessController();
    public  int Collegeno=0,Staffno=0;
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
                //CheckPageAuthorization();
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

               // butSalaryProcess.Text = "Salary Process For" + " " + txt + " " + " Month ";
                butSalaryProcess.Visible = false;
            }
        }
        else
        {
            cetxtStartDate.SelectedDate = null;
        }
    }
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=PayEmployeeBulkSalaryProcess.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=PayEmployeeBulkSalaryProcess.aspx");
    //    }
    //}
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
                objUCommon.ShowError(Page, "PayRoll_PayEmployeeBulkSalaryProcess.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {

        butSalaryProcess.Text = "Salary Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (txtMonthYear.Text == "")
        {
            MessageBox("Please Enter Month Year");
            return;
        }
        else
        {
            string monthyear = txtMonthYear.Text.Trim();
            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            int Staffno = Convert.ToInt32(ddlStaff.SelectedValue);
            ds = objSalController.GetPayEmployeeBulkSalaryProcessData(collegeno, monthyear, Staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LstBulkSalaryProcess.DataSource = ds;
                LstBulkSalaryProcess.DataBind();
            }
            else
            {
                MessageBox("Data Not Found");
                return;
            }
            string txt;
            txt = System.DateTime.Today.ToString("MM/yyyy");
            butSalaryProcess.Text = "Salary Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
            butSalaryProcess.Visible = true;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void butSalaryProcess_Click(object sender, EventArgs e)
    {
       
        string status = string.Empty;
        string CollegNO = string.Empty;
        string StaffNo = string.Empty;
        lblerror.Text = "";
        DataSet ds=new DataSet();
        int CS = 0;
        DataTable DTBulkSalaryProcess = new DataTable("DTBulkSalaryProcess");

        DTBulkSalaryProcess.Columns.Add(new DataColumn("College", typeof(string)));
        DTBulkSalaryProcess.Columns.Add(new DataColumn("Staff", typeof(string)));
        DTBulkSalaryProcess.Columns.Add(new DataColumn("Status", typeof(string)));
        DTBulkSalaryProcess.Columns.Add(new DataColumn("Remark", typeof(string)));
        DataRow dr = null;
       // dr = DTBulkSalaryProcess.NewRow();
        try
        {
            int count = 0;
            foreach (ListViewDataItem lvitem in LstBulkSalaryProcess.Items)
            {
                string employeelist = string.Empty;
                employeelist= "0";
                HiddenField hdnCollegeno = lvitem.FindControl("hdncollegeid") as HiddenField;
                HiddenField hdnstaffno = lvitem.FindControl("hdnstaffid") as HiddenField;
                Label lblCollegeName = lvitem.FindControl("lblcollegename") as Label;
                Label lblstaffname = lvitem.FindControl("lblstaffname") as Label;
                Label lblsalarystatus = lvitem.FindControl("lblsalarystatus") as Label;
                if(Convert.ToString(hdnCollegeno.Value) == "")
                {
                    //Collegeno = Convert.ToInt32(hdnCollegeno.Value);
                    Collegeno = Convert.ToInt32(hdnCollegeno.Value);
                }
                else
                {
                      Collegeno = Convert.ToInt32(hdnCollegeno.Value);
                }
                if (Convert.ToString(hdnstaffno.Value) == "")
                {
                    //Collegeno = Convert.ToInt32(hdnCollegeno.Value);
                    Staffno = Convert.ToInt32(hdnstaffno.Value);
                }
                else
                {
                    Staffno = Convert.ToInt32(hdnstaffno.Value);
                }
                 CheckBox chkbox = lvitem.FindControl("chkidno") as CheckBox;
                   if (chkbox.Checked == true)
                   {
                       try
                       {
                           status = objSalController.PayRollBulkCalculation("0", Convert.ToInt32(Session["idno"].ToString()), Staffno, txtMonthYear.Text, Session["colcode"].ToString(), Collegeno);
                          // objSalController.PayRollCalculation("0", Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlStaff.SelectedValue), txtMonthYear.Text, Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue));
                           if (status != null || status != "" || status != string.Empty)
                           {
                               // logic here
                               count = 1;
                               dr = DTBulkSalaryProcess.NewRow();
                               dr["College"] = lblCollegeName.Text.Trim();
                               dr["Staff"] = lblstaffname.Text.Trim();
                               dr["Status"] = lblsalarystatus.Text.Trim();
                               dr["Remark"] = status;
                               DTBulkSalaryProcess.Rows.Add(dr);
                           }
                           else
                           {
                               MessageBox("Error!!!!!");
                               return;
                           }                        
                       }
                       catch (Exception ex)
                       {
                           if (status == "")
                           {
                               dr = DTBulkSalaryProcess.NewRow();
                               dr["College"] = lblCollegeName.Text.Trim();
                               dr["Staff"] = lblstaffname.Text.Trim();
                               dr["Status"] = lblsalarystatus.Text.Trim();
                               dr["Remark"] = "Error";
                               DTBulkSalaryProcess.Rows.Add(dr);
                               //ds.Tables.Add(DTBulkSalaryProcess);
                               //lstBulkSalaryProcess1.DataSource = ds.Tables[0];
                               //lstBulkSalaryProcess1.DataBind();
                           }
                           
                       }
                       finally
                       {
                           
                       }
                   }
                   System.Threading.Thread.Sleep(1000);
            }
            if (count == 1)
            {
                ds.Tables.Add(DTBulkSalaryProcess);
                lstBulkSalaryProcess1.DataSource = ds.Tables[0];
                lstBulkSalaryProcess1.DataBind();
                MessageBox("Salary Process Successfully");
                return;
            }
            else if(status == "")
            {
                ds.Tables.Add(DTBulkSalaryProcess);
                lstBulkSalaryProcess1.DataSource = ds.Tables[0];
                lstBulkSalaryProcess1.DataBind();
                MessageBox("Please Select Atleast Record");
                return;
            }
          
        }
        catch (Exception ex)
        {

        }
       
    }
    protected void LstBulkSalaryProcess_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HiddenField hdnsalaryprocessstatus = (HiddenField)e.Item.FindControl("hdnstatus");
            Label lblsalarystatus = (Label)e.Item.FindControl("lblsalarystatus");

            if (Convert.ToBoolean(hdnsalaryprocessstatus.Value) == true)
            {
                lblsalarystatus.ForeColor= System.Drawing.Color.Green;
            }
            else
            {
                lblsalarystatus.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
}