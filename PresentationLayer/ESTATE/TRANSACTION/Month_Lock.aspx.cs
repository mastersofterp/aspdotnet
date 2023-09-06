using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ESTATE_Transaction_Month_Lock : System.Web.UI.Page
{

    Monthlybilllock_controller objbillLock = new Monthlybilllock_controller();
    Common objCommon = new Common();
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
                //chkmonthlock.Checked = false;
                // chkmonthlockwater.Checked = false;
                BindlistView();
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    //this is used to process electicity bill
    protected void btnProcessBill_Click(object sender, EventArgs e)
    {        
        try
        {
            string NameId = "0";
            string eDate = Convert.ToDateTime(txtselectdate.Text).ToString("yyyy-MM-dd");
            CustomStatus cs = (CustomStatus)objbillLock.ElectricityBillProcess(eDate, NameId);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updReport, "Electricity Bill Process Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updReport, "Electricity Bill Not Process.", this.Page);
            }            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //this is used for month lock.........
    protected void btnmonthlock_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (!string.IsNullOrEmpty(txtselectdate.Text)) // && !string.IsNullOrEmpty(txtDueDate.Text) && !string.IsNullOrEmpty(txtLastDate.Text))
            {
                string chkStatus = string.Empty;
                //string chkwater = string.Empty;
                if (chkmonthlock.Checked.Equals(true)) // && chkmonthlockwater.Checked.Equals(true))
                {
                    chkStatus = "L";
                    //chkwater = "L";
                }
                else if (chkmonthlock.Checked.Equals(true)) // && chkmonthlockwater.Checked.Equals(false))
                {
                    chkStatus = "L";
                    //chkwater = "A";
                }
                else if (chkmonthlock.Checked.Equals(false)) // && chkmonthlockwater.Checked.Equals(true))
                {
                    chkStatus = "A";
                    // chkwater = "L";
                }
                else if (chkunlockenergy.Checked.Equals(true)) //&& chkmonthunlockwater.Checked.Equals(true))
                {
                    chkStatus = "A";
                    // chkwater = "A";
                }
                //else 
                //    chkStatus = "A";
                //    chkwater = "A";

               // string DueDate = Convert.ToDateTime(txtDueDate.Text).ToString("yyyy-MM-dd");
                //  string LastDate = Convert.ToDateTime(txtLastDate.Text).ToString("yyyy-MM-dd");  
                DateTime DueDate;
                if (txtDueDate.Text != string.Empty)
                {
                     DueDate = Convert.ToDateTime(txtDueDate.Text);
                }
                else
                {
                     DueDate = DateTime.MinValue;
                }
                DateTime LastDate;
                if (txtLastDate.Text != string.Empty)
                {
                     LastDate = Convert.ToDateTime(txtLastDate.Text);
                }
                else
                {
                     LastDate = DateTime.MinValue;
                }

                DateTime SelectDate;
                if (txtselectdate.Text != string.Empty)
                {
                    SelectDate = Convert.ToDateTime(txtselectdate.Text);
                }
                else
                {
                    SelectDate = DateTime.MinValue;
                }


                CustomStatus cs = (CustomStatus)objbillLock.Monthly_billLock(SelectDate, DueDate, LastDate, chkStatus);//, chkwater);

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (chkmonthlock.Checked == true)
                    {
                        objCommon.DisplayMessage(this.updReport, "Month Bill Entry Is Successfully Lock.", this.Page);
                        btnProcessBill.Enabled = true;
                    }
                    else if (chkunlockenergy.Checked == true)
                    {
                        objCommon.DisplayMessage(this.updReport, "Month Bill Entry Is Successfully UnLock.", this.Page);
                        btnProcessBill.Enabled = false;
                    }
                    BindlistView();
                }
                else
                {
                    objCommon.DisplayMessage(this.updReport, "Sorry!Unable to Lock.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updReport, "Please Select Month, Due Date And Last Date.", this.Page);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    // This method is used to bind the list of application of user.
    private void BindlistView()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_LOCK_UNLOCK_STATUS", "IDNO, (CASE MONTH WHEN 1 THEN 'JANUARY' WHEN 2 THEN 'FEBRUARY' WHEN 3 THEN 'MARCH' WHEN 4 THEN 'APRIL' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUNE' WHEN 7 THEN 'JULY' WHEN 8 THEN 'AUGUST' WHEN 9 THEN 'SEPTEMBER' WHEN 10 THEN 'OCTOBER' WHEN 11 THEN 'NOVEMBER' ELSE 'DECEMBER' END) + ', ' + CAST(YEAR as varchar(20))  as MONTH", "LOCKING_DATE,DUE_DATE, LAST_DATE, (CASE STATUS WHEN 'L' THEN 'LOCK' ELSE 'UNLOCK' END) AS STATUS", "", "IDNO DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLock.DataSource = ds;
                lvLock.DataBind();
                lvLock.Visible = true;
            }
            else
            {
                lvLock.DataSource = null;
                lvLock.DataBind();
                lvLock.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtselectdate.Text = string.Empty;
        chkmonthlock.Checked = false;
        //chkmonthlockwater.Checked = false;
        //chkmonthunlockwater.Checked = false;
        chkunlockenergy.Checked = false;
        txtDueDate.Text = string.Empty;
        txtLastDate.Text = string.Empty;
        ViewState["IDNO"] = null; 
        btnProcessBill.Enabled = false;
    }

    protected void txtselectdate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_ENERGY_METER_READING", "*", " ", "YEAR(MONTH_R)='" + Convert.ToDateTime(txtselectdate.Text).Year + "' and month(MONTH_R)='" + Convert.ToDateTime(txtselectdate.Text).Month + "'", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["E_status"].ToString().Trim() == "L")
            {
                chkmonthlock.Checked = true;
                chkunlockenergy.Checked = false;
                btnProcessBill.Enabled = true;
            }
            else
            {
                chkunlockenergy.Checked = true;
                chkmonthlock.Checked = false;
                btnProcessBill.Enabled = false;
            }
            txtDueDate.Text = ds.Tables[0].Rows[0]["DUE_DATE"].ToString();
            txtLastDate.Text = ds.Tables[0].Rows[0]["LAST_DATE"].ToString();
        }

        //DataSet ds1 = null;
        //ds1 = objCommon.FillDropDown("EST_WATER_METER_READING", "*", " ", "YEAR(MONTH_R)='" + Convert.ToDateTime(txtselectdate.Text).Year + "' and month(MONTH_R)='" + Convert.ToDateTime(txtselectdate.Text).Month + "'", "");

        //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
        //{
        //    if (ds1.Tables[0].Rows[0]["E_status"].ToString().Trim() == "L")
        //    {
        //        chkmonthlockwater.Checked = true;
        //    }
        //    else
        //    {
        //        chkmonthunlockwater.Checked = true;
        //    }
        //}
    }

    protected void chkmonthlock_CheckedChanged(object sender, EventArgs e)
    {
        if (chkmonthlock.Checked == true)
        {
            chkunlockenergy.Checked = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int IDNO = int.Parse(btnEdit.CommandArgument);
            ViewState["IDNO"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(IDNO);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // This method is used to show the details of the selected record.
    private void ShowDetails(int IDNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("EST_LOCK_UNLOCK_STATUS", "*", "", "IDNO=" + IDNO, "");
          
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtselectdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LOCKING_DATE"]).ToString("MM/yyyy");
                    txtDueDate.Text = ds.Tables[0].Rows[0]["DUE_DATE"].ToString();
                    txtLastDate.Text = ds.Tables[0].Rows[0]["LAST_DATE"].ToString();
                    if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "L")
                    {
                        chkmonthlock.Checked = true;
                        chkunlockenergy.Checked = false;
                        btnProcessBill.Enabled = true;
                    }
                    else
                    {
                        chkunlockenergy.Checked = true;
                        chkmonthlock.Checked = false;
                        btnProcessBill.Enabled = false;
                    }
                }    
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void chkunlockenergy_CheckedChanged(object sender, EventArgs e)
    {
        if (chkunlockenergy.Checked == true)
        {
            chkmonthlock.Checked = false;
        }
    }
  
}
