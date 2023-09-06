//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Leave Mgt.                   
// CREATION DATE : 21-April-2015                                                          
// CREATED BY    : Swati Ghate                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using IITMS.SQLServer.SQLDAL;

using System.Globalization;

public partial class Transfer_Attendance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLC = new LeavesController();
    //ConnectionStrings
    // string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;

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
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization


                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
                    int prevyr = System.DateTime.Today.AddYears(-1).Year;
                    int month = System.DateTime.Today.Month;
                    int year = System.DateTime.Today.Year;
                    string frmdt = null;
                    if (month == 1)
                    {
                        //frmdt = "25" + "/" + "12" + "/" + prevyr.ToString();
                        frmdt="01" + "/" + month + "/" + year.ToString();
                    }
                    else
                    {
                        //frmdt = "25" + "/" + prevmonth.ToString() + "/" + year.ToString();
                        frmdt = "01" + "/" + month + "/" + year.ToString();
                    }


                    //string todt = "26" + "/" + month.ToString() + "/" + year.ToString();
                    string todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();

                    txtFromDate.Text = frmdt;
                    txtToDate.Text = todt;
                    FillCollege();
                    CheckPageAuthorization();
                }

                //Focus on From Date Textbox
                txtFromDate.Focus();


            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Transfer_Attendance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Transfer_Attendance.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            pnlView.Visible = false;
            lvEmpList.Visible = false;
            DateTime dt1 = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime dt2 = Convert.ToDateTime(txtToDate.Text.Trim());
            TimeSpan ts = dt1.Subtract(dt2);
            // int diffMonth = Math.Abs((dt2.Year - dt1.Year) * 12 + dt1.Month - dt2.Month);
            divMsg.InnerHtml = string.Empty;
            int diffDay = Math.Abs(dt2.Month - dt1.Month); //+ dt1.Month - dt2.Month)
            if (diffDay > 1)
            {
                ShowMessage("Date difference Should not more than 1 Month.");
                return;
            }
            else
            {
                int ret = Convert.ToInt32(objLC.CheckAttendance(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text)));
                if (ret == 1)
                {
                    //DataSet ds = objLC.GetAttendanceRecord(0, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlCollege.SelectedValue), "T");
                    DataSet ds = objLC.GetAttendanceRecord(Convert.ToInt32(ddlstafftype.SelectedValue), Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(ddlCollege.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvEmpList.DataSource = ds;
                        lvEmpList.DataBind();
                        pnlView.Visible = true;
                        lvEmpList.Visible = true;
                        btnUpdate.Visible = true;
                        btnEdit.Visible = true;
                        // //btnReport.Visible = true;

                    }
                    else
                    {
                        lvEmpList.DataSource = null;
                        lvEmpList.DataBind();
                        pnlView.Visible = false;
                        lvEmpList.Visible = false;

                    }
                }
                else if (ret == 0)
                {
                    objCommon.DisplayMessage("Sorry! Please Process Attendance", this);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnEdit.Enabled = false;
            btnUpdate.Enabled = true;
            if (lvEmpList.Items.Count > 0)
            {
                for (int i = 0; i < lvEmpList.Items.Count; i++)
                {
                    TextBox txtleaveUpdated = lvEmpList.Items[i].FindControl("txtleaveUpdated") as TextBox;
                    txtleaveUpdated.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Transfer_ATtendance.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int ret = 0;

        //====================================================================
        int checkcount = 0;
        int instCount = 0;
        string selectedIDs = string.Empty;
        int idno = 0;
        foreach (ListViewDataItem lvItem in lvEmpList.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
          

            TextBox txtleave = lvItem.FindControl("txtleave") as TextBox;

            TextBox txtleaveUpdated = lvItem.FindControl("txtleaveUpdated") as TextBox;
            Label lblidno = lvItem.FindControl("lblidno") as Label;
           
            checkcount += 1;           
            idno = Convert.ToInt32(lblidno.Text);
            double leaveUpdated = Convert.ToDouble(txtleaveUpdated.Text);
            ret = Convert.ToInt32(objLC.UpdateAttendance(Convert.ToInt32(idno), leaveUpdated, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlCollege.SelectedValue)));

            //}
        }

        //======================================================================
        if (ret == Convert.ToInt32(CustomStatus.RecordUpdated))
        {

            objCommon.DisplayMessage("Attendance Record Transfer Successfully", this);
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
            btnEdit.Visible = false;
            btnUpdate.Visible = false;
            pnlView.Visible = false;
            //btnReport.Visible = true;
            //btnReport.Enabled = true;
            //foreach (ListViewDataItem lvItem in lvEmpList.Items)
            //{

            //    CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            //    TextBox txtleave = lvItem.FindControl("txtleave") as TextBox;

            //    TextBox txtleaveUpdated = lvItem.FindControl("txtleaveUpdated") as TextBox;
            //    txtleaveUpdated.Enabled = false;
            //    btnEdit.Enabled = true;
            //    btnUpdate.Enabled = false;
            //    //chk.Checked = false;
            //}
        }

    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromDate.Text);
                DtTo = Convert.ToDateTime(txtToDate.Text);
                if (DtTo < DtFrom)
                {
                    MessageBox("To Date Should be Greater than  or equal to From Date");
                    txtToDate.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            MessageBox("Please Enter Valid Date");
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
    }
}
