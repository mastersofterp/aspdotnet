//============================================
//CREATED BY: SWATI GHATE
//CREATED DATE: 23-12-2014
//PURPOSE: To Store & Maintain Detention Entry for EL 
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

public partial class ESTABLISHMENT_LEAVES_Transactions_DetentionEntryforEL : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
   
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
                Session["usertype"] == null || Session["userfullname"] == null)
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;

                CheckPageAuthorization();
                FillDropDown();

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
                Response.Redirect("~/notauthorized.aspx?page=DetentionEntryforEL.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DetentionEntryforEL.aspx");
        }
    }
    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");

            objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO <> 0", "SUBDEPTNO");
            //To fill Year
            int chkYear = DateTime.Now.Year;
            ddlYear.Items.Add((chkYear-1).ToString());
            ddlYear.Items.Add(chkYear.ToString());
            ddlYear.Items.Add((chkYear + 1).ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewList(int staffNo,int Deptno,int Year)
    {
        try
        {
            Leaves objLeave = new Leaves();
            if (!(Convert.ToInt32(ddlStaffType.SelectedIndex) == 0))
            {
                objLeave.STNO = staffNo;               
                objLeave.DEPTNO = Deptno;
                objLeave.YEAR = Year;

                 int cnt = Convert.ToInt32(objCommon.LookUp("PAYROLL_DETENTION_EL", "COUNT(*)", "STAFFNO=" + ddlStaffType.SelectedValue+" AND ELCREDIT_YEAR="+Year));
                  DataSet ds=null;
                if (cnt == 0)
                {
                    ds = objApp.GetEmployeesForDetentionEL(objLeave);
                }
                else
                {
                    
                    ViewState["ACTION"] = "EDIT";
                   
                    ds = objApp.GetEmployeesForDetentionELUpdate(objLeave);
           
                }

            
                lvInfo.DataSource = ds;
                lvInfo.DataBind();
                lvInfo.Visible = true;
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

            string monyear = string.Empty;
        

            //monyear = mon + year;

            foreach (ListViewDataItem lvitem in lvInfo.Items)
            {
                TextBox txt = lvitem.FindControl("txtDays") as TextBox;
                //CustomStatus cs1 = (CustomStatus)ObjChangeMstFile.AddSubPayheadAmount(Convert.ToInt32(txt.ToolTip), monyear, ddlPayhead.SelectedValue.ToString(), Convert.ToInt32(ddlSubPayhead.SelectedValue), Convert.ToDecimal(txt.Text), Convert.ToInt32(ddlStaff.SelectedValue));

               
                
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

        
        ddlStaffType.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        lvInfo.Visible = false;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        btnSave.Visible = false; btnCancel.Visible = false;
    }

    

   

    protected void btnShow_Click(object sender, EventArgs e)
    {
        btnCancel.Visible = true;
        btnSave.Visible = true;
       lblmsg.Text = string.Empty;
       lblerror.Text = string.Empty;
       BindListViewList(Convert.ToInt32(ddlStaffType.SelectedValue),Convert.ToInt32(ddlDept.SelectedValue),Convert.ToInt32(ddlYear.SelectedValue));
        //DataSet ds=objCommon.FillDropDown("

       
    }
    protected void TotalPayheadAmount()
    {
        decimal totalPayheadAmount = 0;

        foreach (ListViewDataItem lvitem in lvInfo.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            totalPayheadAmount = totalPayheadAmount + Convert.ToDecimal(txt.Text);
        }


    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            message = message.Replace("'", "\'");
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    /// <summary>
    /// Used to Save deltention entry for EL leave by deleting previous records with specific conditions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDetentionEntryForEL();
    }
    protected void SaveDetentionEntryForEL()
    {
        Leaves objLeave = new Leaves();

        int checkcount = 0;
        int instCount = 0;
        string selectedIDs = string.Empty;

        int staffTypeno = Convert.ToInt32(ddlStaffType.SelectedValue);
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        //====================
        string lvname="EL";
        int leaveno =Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_NAME", "LVNO", "Leave_ShortName='" + lvname + "'"));

        int period = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "PERIOD", "LEAVENO=" + leaveno + " AND STNO=" + staffTypeno));
        //=======================
        objLeave.STNO = staffTypeno;
        objLeave.YEAR = year;
        objLeave.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
        objLeave.PERIOD = period;
        CustomStatus cs1 = (CustomStatus)objApp.DeleteLeaveDetentionEntryForEL(objLeave);


        foreach (ListViewDataItem lvItem in lvInfo.Items)
        {

            TextBox txtfirstdays = lvItem.FindControl("txtfirstDays") as TextBox;
            TextBox txtNoofDays = lvItem.FindControl("txtDays") as TextBox;
            //TextBox txtVacationDays = lvItem.FindControl("txtvacationdays") as TextBox;
            //TextBox txtServiceEl = lvItem.FindControl("txtServicEl") as TextBox;
            TextBox txtTotalEl = lvItem.FindControl("txtTotalEL") as TextBox;
            Label lblYear = lvItem.FindControl("lblYear") as Label;
            //txtreason
            TextBox txtreason = lvItem.FindControl("txtreason") as TextBox;
            int idno = Convert.ToInt32(txtNoofDays.ToolTip);

            int deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + idno));
            int designo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDESIGNO", "IDNO=" + idno));

            objLeave.EMPNO = idno;
            //objLeave.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeave.DEPTNO = deptno;
            objLeave.SUBDESIGNO = designo;
            //objLeave.ELFIRSTDAY = Convert.ToDouble(txtfirstdays.Text);
            objLeave.ELNOOFDAYS = Convert.ToDouble(txtNoofDays.Text);
            //objLeave.ELVACATIONDAYS = Convert.ToDouble(txtVacationDays.Text);
            //objLeave.SERVICEEL = Convert.ToDouble(txtServiceEl.Text);
            objLeave.ELTOCREDIT = Convert.ToDouble(txtTotalEl.Text);
            objLeave.ELYEAR = Convert.ToString(lblYear.Text);
            objLeave.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objLeave.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
            objLeave.REASON = txtreason.Text;
            CustomStatus cs = (CustomStatus)objApp.AddDetentionEntryForEL(objLeave);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Record Saved Successfully", this);
            }


        }
        if (checkcount == 0)
        {
            // MessageBox("Please Select Atleast One Employee");
            return;
        }


        // selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
        // string idno = selectedIDs.Replace('$', ',');


        if (instCount == 1)
        {
            //MessageBox("Record saved successfully");
        }
        //BindLateComersList();

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        lvInfo.Visible = false;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        btnSave.Visible = false; btnCancel.Visible = false;
        lvInfo.Visible = false;
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStaffType.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        lvInfo.Visible = false;
        btnSave.Visible = false; btnCancel.Visible = false;
    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlYear.SelectedIndex = 0;
        lvInfo.Visible = false;
        btnSave.Visible = false; btnCancel.Visible = false;
    }
}
