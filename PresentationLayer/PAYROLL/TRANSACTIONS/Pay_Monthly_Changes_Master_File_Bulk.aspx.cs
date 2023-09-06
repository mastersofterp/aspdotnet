//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ChangeInMasterFile.ASPX                                                    
// CREATION DATE : 25-JULY-2019                                                        
// CREATED BY    : PRASHANT WANKAR                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text;

using System.Collections;


public partial class PAYROLL_TRANSACTIONS_Pay_Monthly_Changes_Master_File_Bulk : System.Web.UI.Page
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
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

    private void BindListViewList(string payHead, int staffNo, string payRule, string Dept, int collegeNo, int subdeptno)
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

            if (ddlHeadType.SelectedValue == "I")
            {
                pnlMonthlyChanges.Visible = true;
                DivDeduction.Visible = false;
                DivIncome.Visible = true;

                //( int Staff, string order, string PayRule, int collegeNo, int empTypeno, int subdeptno,string HeadType)
                DataSet ds = ObjChangeMstFile.GetEmployeesForAmountDeductionWithMultipleHead(staffNo, orderby, payRule, collegeNo, Convert.ToInt32(ddlEmployeeType.SelectedValue), subdeptno, "I", Convert.ToInt32(Session["userno"].ToString()));
                if (ds.Tables[0].Rows.Count <= 0)
                {

                    divbutton.Visible = false;

                }
                else
                {

                    divbutton.Visible = true;

                }
                //added by vijay andoju for add empty rowfro summ of head



                lvMonthlyChanges.DataSource = ds;
                lvMonthlyChanges.DataBind();

                SumofAllHeads(ds.Tables[0],"I");



                //EARNING HEADS
                Label lblh1 = lvMonthlyChanges.FindControl("lblEH1") as Label; lblh1.Text = ds.Tables[0].Rows[0]["HEADEARNING1"].ToString();
                Label lblh2 = lvMonthlyChanges.FindControl("lblEH2") as Label; lblh2.Text = ds.Tables[0].Rows[0]["HEADEARNING2"].ToString();
                Label lblh3 = lvMonthlyChanges.FindControl("lblEH3") as Label; lblh3.Text = ds.Tables[0].Rows[0]["HEADEARNING3"].ToString();
                Label lblh4 = lvMonthlyChanges.FindControl("lblEH4") as Label; lblh4.Text = ds.Tables[0].Rows[0]["HEADEARNING4"].ToString();
                Label lblh5 = lvMonthlyChanges.FindControl("lblEH5") as Label; lblh5.Text = ds.Tables[0].Rows[0]["HEADEARNING5"].ToString();
                Label lblh6 = lvMonthlyChanges.FindControl("lblEH6") as Label; lblh6.Text = ds.Tables[0].Rows[0]["HEADEARNING6"].ToString();
                Label lblh7 = lvMonthlyChanges.FindControl("lblEH7") as Label; lblh7.Text = ds.Tables[0].Rows[0]["HEADEARNING7"].ToString();
                Label lblh8 = lvMonthlyChanges.FindControl("lblEH8") as Label; lblh8.Text = ds.Tables[0].Rows[0]["HEADEARNING8"].ToString();
                Label lblh9 = lvMonthlyChanges.FindControl("lblEH9") as Label; lblh9.Text = ds.Tables[0].Rows[0]["HEADEARNING9"].ToString();
                Label lblh10 = lvMonthlyChanges.FindControl("lblEH10") as Label; lblh10.Text = ds.Tables[0].Rows[0]["HEADEARNING10"].ToString();
                Label lblh11 = lvMonthlyChanges.FindControl("lblEH11") as Label; lblh11.Text = ds.Tables[0].Rows[0]["HEADEARNING11"].ToString();
                Label lblh12 = lvMonthlyChanges.FindControl("lblEH12") as Label; lblh12.Text = ds.Tables[0].Rows[0]["HEADEARNING12"].ToString();
                Label lblh13 = lvMonthlyChanges.FindControl("lblEH13") as Label; lblh13.Text = ds.Tables[0].Rows[0]["HEADEARNING13"].ToString();
                Label lblh14 = lvMonthlyChanges.FindControl("lblEH14") as Label; lblh14.Text = ds.Tables[0].Rows[0]["HEADEARNING14"].ToString();
                Label lblh15 = lvMonthlyChanges.FindControl("lblEH15") as Label; lblh15.Text = ds.Tables[0].Rows[0]["HEADEARNING15"].ToString();

                Label lbblh1 = lvSumIncome.FindControl("lblEH1") as Label; lbblh1.Text = ds.Tables[0].Rows[0]["HEADEARNING1"].ToString();
                Label lbblh2 = lvSumIncome.FindControl("lblEH2") as Label; lbblh2.Text = ds.Tables[0].Rows[0]["HEADEARNING2"].ToString();
                Label lbblh3 = lvSumIncome.FindControl("lblEH3") as Label; lbblh3.Text = ds.Tables[0].Rows[0]["HEADEARNING3"].ToString();
                Label lbblh4 = lvSumIncome.FindControl("lblEH4") as Label; lbblh4.Text = ds.Tables[0].Rows[0]["HEADEARNING4"].ToString();
                Label lbblh5 = lvSumIncome.FindControl("lblEH5") as Label; lbblh5.Text = ds.Tables[0].Rows[0]["HEADEARNING5"].ToString();
                Label lbblh6 = lvSumIncome.FindControl("lblEH6") as Label; lbblh6.Text = ds.Tables[0].Rows[0]["HEADEARNING6"].ToString();
                Label lbblh7 = lvSumIncome.FindControl("lblEH7") as Label; lbblh7.Text = ds.Tables[0].Rows[0]["HEADEARNING7"].ToString();
                Label lbblh8 = lvSumIncome.FindControl("lblEH8") as Label; lbblh8.Text = ds.Tables[0].Rows[0]["HEADEARNING8"].ToString();
                Label lbblh9 = lvSumIncome.FindControl("lblEH9") as Label; lbblh9.Text = ds.Tables[0].Rows[0]["HEADEARNING9"].ToString();
                Label lbblh10 = lvSumIncome.FindControl("lblEH10") as Label; lbblh10.Text = ds.Tables[0].Rows[0]["HEADEARNING10"].ToString();
                Label lbblh11 = lvSumIncome.FindControl("lblEH11") as Label; lbblh11.Text = ds.Tables[0].Rows[0]["HEADEARNING11"].ToString();
                Label lbblh12 = lvSumIncome.FindControl("lblEH12") as Label; lbblh12.Text = ds.Tables[0].Rows[0]["HEADEARNING12"].ToString();
                Label lbblh13 = lvSumIncome.FindControl("lblEH13") as Label; lbblh13.Text = ds.Tables[0].Rows[0]["HEADEARNING13"].ToString();
                Label lbblh14 = lvSumIncome.FindControl("lblEH14") as Label; lbblh14.Text = ds.Tables[0].Rows[0]["HEADEARNING14"].ToString();
                Label lbblh15 = lvSumIncome.FindControl("lblEH15") as Label; lbblh15.Text = ds.Tables[0].Rows[0]["HEADEARNING15"].ToString();

            }
            else
            {
                pnlMonthlyChanges.Visible = true;
                DivDeduction.Visible = true;
                DivIncome.Visible = false;

                DataSet ds = ObjChangeMstFile.GetEmployeesForAmountDeductionWithMultipleHead(staffNo, orderby, payRule, collegeNo, Convert.ToInt32(ddlEmployeeType.SelectedValue), subdeptno, "D", Convert.ToInt32(Session["userno"].ToString()));

                if (ds.Tables[0].Rows.Count <= 0)
                {

                    divbutton.Visible = false;

                }
                else
                {

                    divbutton.Visible = true;


                }

               

                lvMonthlyChangesDeduction.DataSource = ds;
                lvMonthlyChangesDeduction.DataBind();

                SumofAllHeads(ds.Tables[0], "D");

               

                //DEDUCTION HEADS
                Label lblh16 = lvMonthlyChangesDeduction.FindControl("lblDH1") as Label; lblh16.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND1"].ToString();
                Label lblh17 = lvMonthlyChangesDeduction.FindControl("lblDH2") as Label; lblh17.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND2"].ToString();
                Label lblh18 = lvMonthlyChangesDeduction.FindControl("lblDH3") as Label; lblh18.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND3"].ToString();
                Label lblh19 = lvMonthlyChangesDeduction.FindControl("lblDH4") as Label; lblh19.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND4"].ToString();
                Label lblh20 = lvMonthlyChangesDeduction.FindControl("lblDH5") as Label; lblh20.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND5"].ToString();
                Label lblh21 = lvMonthlyChangesDeduction.FindControl("lblDH6") as Label; lblh21.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND6"].ToString();
                Label lblh22 = lvMonthlyChangesDeduction.FindControl("lblDH7") as Label; lblh22.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND7"].ToString();
                Label lblh23 = lvMonthlyChangesDeduction.FindControl("lblDH8") as Label; lblh23.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND8"].ToString();
                Label lblh24 = lvMonthlyChangesDeduction.FindControl("lblDH9") as Label; lblh24.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND9"].ToString();
                Label lblh25 = lvMonthlyChangesDeduction.FindControl("lblDH10") as Label; lblh25.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND10"].ToString();
                Label lblh26 = lvMonthlyChangesDeduction.FindControl("lblDH11") as Label; lblh26.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND11"].ToString();
                Label lblh27 = lvMonthlyChangesDeduction.FindControl("lblDH12") as Label; lblh27.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND12"].ToString();
                Label lblh28 = lvMonthlyChangesDeduction.FindControl("lblDH13") as Label; lblh28.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND13"].ToString();
                Label lblh29 = lvMonthlyChangesDeduction.FindControl("lblDH14") as Label; lblh29.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND14"].ToString();
                Label lblh30 = lvMonthlyChangesDeduction.FindControl("lblDH15") as Label; lblh30.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND15"].ToString();
                Label lblh31 = lvMonthlyChangesDeduction.FindControl("lblDH16") as Label; lblh31.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND16"].ToString();
                Label lblh32 = lvMonthlyChangesDeduction.FindControl("lblDH17") as Label; lblh32.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND17"].ToString();
                Label lblh33 = lvMonthlyChangesDeduction.FindControl("lblDH18") as Label; lblh33.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND18"].ToString();
                Label lblh34 = lvMonthlyChangesDeduction.FindControl("lblDH19") as Label; lblh34.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND19"].ToString();
                Label lblh35 = lvMonthlyChangesDeduction.FindControl("lblDH20") as Label; lblh35.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND20"].ToString();
                Label lblh36 = lvMonthlyChangesDeduction.FindControl("lblDH21") as Label; lblh36.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND21"].ToString();
                Label lblh37 = lvMonthlyChangesDeduction.FindControl("lblDH22") as Label; lblh37.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND22"].ToString();
                Label lblh38 = lvMonthlyChangesDeduction.FindControl("lblDH23") as Label; lblh38.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND23"].ToString();
                Label lblh39 = lvMonthlyChangesDeduction.FindControl("lblDH24") as Label; lblh39.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND24"].ToString();
                Label lblh40 = lvMonthlyChangesDeduction.FindControl("lblDH25") as Label; lblh40.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND25"].ToString();
                Label lblh41 = lvMonthlyChangesDeduction.FindControl("lblDH26") as Label; lblh41.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND26"].ToString();
                Label lblh42 = lvMonthlyChangesDeduction.FindControl("lblDH27") as Label; lblh42.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND27"].ToString();
                Label lblh43 = lvMonthlyChangesDeduction.FindControl("lblDH28") as Label; lblh43.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND28"].ToString();
                Label lblh44 = lvMonthlyChangesDeduction.FindControl("lblDH29") as Label; lblh44.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND29"].ToString();
                Label lblh45 = lvMonthlyChangesDeduction.FindControl("lblDH30") as Label; lblh45.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND30"].ToString();



                Label lblsh16 = lvSumDiduction.FindControl("lblDH1") as Label; lblsh16.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND1"].ToString();
                Label lblsh17 = lvSumDiduction.FindControl("lblDH2") as Label; lblsh17.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND2"].ToString();
                Label lblsh18 = lvSumDiduction.FindControl("lblDH3") as Label; lblsh18.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND3"].ToString();
                Label lblsh19 = lvSumDiduction.FindControl("lblDH4") as Label; lblsh19.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND4"].ToString();
                Label lblsh20 = lvSumDiduction.FindControl("lblDH5") as Label; lblsh20.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND5"].ToString();
                Label lblsh21 = lvSumDiduction.FindControl("lblDH6") as Label; lblsh21.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND6"].ToString();
                Label lblsh22 = lvSumDiduction.FindControl("lblDH7") as Label; lblsh22.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND7"].ToString();
                Label lblsh23 = lvSumDiduction.FindControl("lblDH8") as Label; lblsh23.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND8"].ToString();
                Label lblsh24 = lvSumDiduction.FindControl("lblDH9") as Label; lblsh24.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND9"].ToString();
                Label lblsh25 = lvSumDiduction.FindControl("lblDH10") as Label; lblsh25.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND10"].ToString();
                Label lblsh26 = lvSumDiduction.FindControl("lblDH11") as Label; lblsh26.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND11"].ToString();
                Label lblsh27 = lvSumDiduction.FindControl("lblDH12") as Label; lblsh27.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND12"].ToString();
                Label lblsh28 = lvSumDiduction.FindControl("lblDH13") as Label; lblsh28.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND13"].ToString();
                Label lblsh29 = lvSumDiduction.FindControl("lblDH14") as Label; lblsh29.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND14"].ToString();
                Label lblsh30 = lvSumDiduction.FindControl("lblDH15") as Label; lblsh30.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND15"].ToString();
                Label lblsh31 = lvSumDiduction.FindControl("lblDH16") as Label; lblsh31.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND16"].ToString();
                Label lblsh32 = lvSumDiduction.FindControl("lblDH17") as Label; lblsh32.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND17"].ToString();
                Label lblsh33 = lvSumDiduction.FindControl("lblDH18") as Label; lblsh33.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND18"].ToString();
                Label lblsh34 = lvSumDiduction.FindControl("lblDH19") as Label; lblsh34.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND19"].ToString();
                Label lblsh35 = lvSumDiduction.FindControl("lblDH20") as Label; lblsh35.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND20"].ToString();
                Label lblsh36 = lvSumDiduction.FindControl("lblDH21") as Label; lblsh36.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND21"].ToString();
                Label lblsh37 = lvSumDiduction.FindControl("lblDH22") as Label; lblsh37.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND22"].ToString();
                Label lblsh38 = lvSumDiduction.FindControl("lblDH23") as Label; lblsh38.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND23"].ToString();
                Label lblsh39 = lvSumDiduction.FindControl("lblDH24") as Label; lblsh39.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND24"].ToString();
                Label lblsh40 = lvSumDiduction.FindControl("lblDH25") as Label; lblsh40.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND25"].ToString();
                Label lblsh41 = lvSumDiduction.FindControl("lblDH26") as Label; lblsh41.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND26"].ToString();
                Label lblsh42 = lvSumDiduction.FindControl("lblDH27") as Label; lblsh42.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND27"].ToString();
                Label lblsh43 = lvSumDiduction.FindControl("lbblDH28") as Label; lblsh43.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND28"].ToString();
                Label lblsh44 = lvSumDiduction.FindControl("lblDH29") as Label; lblsh44.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND29"].ToString();
                Label lblsh45 = lvSumDiduction.FindControl("lblDH30") as Label; lblsh45.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND30"].ToString();
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

    private void SumofAllHeads(DataTable dataTable,string Type)
    {
        DataTable dt = dataTable;
        DataRow dtrow = dt.NewRow();
        dt.Rows.InsertAt(dtrow, dt.Rows.Count);

        lvSumDiduction.Visible = false;

        lvSumIncome.Visible = false;

        DataRow[] dtfind = dt.Select("Name is null");
        dt = dtfind.CopyToDataTable();
        if (Type == "I")
        {
            lvSumIncome.DataSource = dt;
            lvSumIncome.DataBind();
            lvSumIncome.Visible = true;
                //added by vijay andoju for sumofheadof lvMonthlyChanges
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SumOfHead()", true);
        }
        if (Type == "D")
        {
            lvSumDiduction.DataSource = dt;
            lvSumDiduction.DataBind();
            lvSumDiduction.Visible = true;

            //added by vijay andoju for sumofheadof lvMonthlyChanges
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SumOfHeadDeduct()", true);
        }
    }

    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearGrid()
    {
        lvMonthlyChanges.DataSource = null;
        lvMonthlyChanges.DataBind();

        lvMonthlyChangesDeduction.DataSource = null;
        lvMonthlyChangesDeduction.DataBind();

        pnlMonthlyChanges.Visible = false;
        DivDeduction.Visible = false;
        DivIncome.Visible = false;
        divbutton.Visible = false;


    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;



            if (ddlHeadType.SelectedValue == "I")
            {
                int C1 = 0;
                foreach (ListViewItem i in lvMonthlyChanges.Items)
                {
                    CheckBox chkIno = (CheckBox)i.FindControl("chkIno");
                    if (chkIno.Checked == true)
                    {
                        C1 = C1 + 1;
                    }
                }
                if (C1 == 0)
                {
                    ShowGrid();
                    objCommon.DisplayMessage("Please select atleast one employee from list !", this);
                    return;
                }


                DataTable PayrollPaymasTbl = new DataTable("PayrollPaymasTbl");
                PayrollPaymasTbl.Columns.Add("IDNO", typeof(int));
                PayrollPaymasTbl.Columns.Add("I1", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I2", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I3", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I4", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I5", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I6", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I7", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I8", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I9", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I10", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I11", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I12", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I13", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I14", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("I15", typeof(decimal));


                DataRow dr = null;
                StringBuilder Strbuild = new StringBuilder();
                //For Issue insert

                foreach (ListViewItem i in lvMonthlyChanges.Items)
                {
                    CheckBox chkIno = (CheckBox)i.FindControl("chkIno");

                    Label lblI1 = i.FindControl("lblI1") as Label;
                    Label lblI2 = i.FindControl("lblI2") as Label;
                    Label lblI3 = i.FindControl("lblI3") as Label;
                    Label lblI4 = i.FindControl("lblI4") as Label;
                    Label lblI5 = i.FindControl("lblI5") as Label;
                    Label lblI6 = i.FindControl("lblI6") as Label;
                    Label lblI7 = i.FindControl("lblI7") as Label;
                    Label lblI8 = i.FindControl("lblI8") as Label;
                    Label lblI9 = i.FindControl("lblI9") as Label;
                    Label lblI10 = i.FindControl("lblI10") as Label;
                    Label lblI11 = i.FindControl("lblI11") as Label;
                    Label lblI12 = i.FindControl("lblI12") as Label;
                    Label lblI13 = i.FindControl("lblI13") as Label;
                    Label lblI14 = i.FindControl("lblI14") as Label;
                    Label lblI15 = i.FindControl("lblI15") as Label;


                    Label lblCk1 = i.FindControl("lblCk1") as Label;
                    Label lblCk2 = i.FindControl("lblCk2") as Label;
                    Label lblCk3 = i.FindControl("lblCk3") as Label;
                    Label lblCk4 = i.FindControl("lblCk4") as Label;
                    Label lblCk5 = i.FindControl("lblCk5") as Label;
                    Label lblCk6 = i.FindControl("lblCk6") as Label;
                    Label lblCk7 = i.FindControl("lblCk7") as Label;
                    Label lblCk8 = i.FindControl("lblCk8") as Label;
                    Label lblCk9 = i.FindControl("lblCk9") as Label;
                    Label lblCk10 = i.FindControl("lblCk10") as Label;
                    Label lblCk11 = i.FindControl("lblCk11") as Label;
                    Label lblCk12 = i.FindControl("lblCk12") as Label;
                    Label lblCk13 = i.FindControl("lblCk13") as Label;
                    Label lblCk14 = i.FindControl("lblCk14") as Label;
                    Label lblCk15 = i.FindControl("lblCk15") as Label;

                    TextBox txtI1 = i.FindControl("txtI1") as TextBox;
                    TextBox txtI2 = i.FindControl("txtI2") as TextBox;
                    TextBox txtI3 = i.FindControl("txtI3") as TextBox;
                    TextBox txtI4 = i.FindControl("txtI4") as TextBox;
                    TextBox txtI5 = i.FindControl("txtI5") as TextBox;
                    TextBox txtI6 = i.FindControl("txtI6") as TextBox;
                    TextBox txtI7 = i.FindControl("txtI7") as TextBox;
                    TextBox txtI8 = i.FindControl("txtI8") as TextBox;
                    TextBox txtI9 = i.FindControl("txtI9") as TextBox;
                    TextBox txtI10 = i.FindControl("txtI10") as TextBox;
                    TextBox txtI11 = i.FindControl("txtI11") as TextBox;
                    TextBox txtI12 = i.FindControl("txtI12") as TextBox;
                    TextBox txtI13 = i.FindControl("txtI13") as TextBox;
                    TextBox txtI14 = i.FindControl("txtI14") as TextBox;
                    TextBox txtI15 = i.FindControl("txtI15") as TextBox;

                    if (chkIno.Checked == true)
                    {
                        dr = PayrollPaymasTbl.NewRow();

                        dr["IDNO"] = chkIno.ToolTip;

                        if (lblI1.Text == "False" || lblCk1.Text == "0") { dr["I1"] = -1; } else { dr["I1"] = txtI1.Text.Trim(); }
                        if (lblI2.Text == "False" || lblCk2.Text == "0") { dr["I2"] = -1; } else { dr["I2"] = txtI2.Text.Trim(); }
                        if (lblI3.Text == "False" || lblCk3.Text == "0") { dr["I3"] = -1; } else { dr["I3"] = txtI3.Text.Trim(); }
                        if (lblI4.Text == "False" || lblCk4.Text == "0") { dr["I4"] = -1; } else { dr["I4"] = txtI4.Text.Trim(); }
                        if (lblI5.Text == "False" || lblCk5.Text == "0") { dr["I5"] = -1; } else { dr["I5"] = txtI5.Text.Trim(); }
                        if (lblI6.Text == "False" || lblCk6.Text == "0") { dr["I6"] = -1; } else { dr["I6"] = txtI6.Text.Trim(); }
                        if (lblI7.Text == "False" || lblCk7.Text == "0") { dr["I7"] = -1; } else { dr["I7"] = txtI7.Text.Trim(); }
                        if (lblI8.Text == "False" || lblCk8.Text == "0") { dr["I8"] = -1; } else { dr["I8"] = txtI8.Text.Trim(); }
                        if (lblI9.Text == "False" || lblCk9.Text == "0") { dr["I9"] = -1; } else { dr["I9"] = txtI9.Text.Trim(); }
                        if (lblI10.Text == "False" || lblCk10.Text == "0") { dr["I10"] = -1; } else { dr["I10"] = txtI10.Text.Trim(); }
                        if (lblI11.Text == "False" || lblCk11.Text == "0") { dr["I11"] = -1; } else { dr["I11"] = txtI11.Text.Trim(); }
                        if (lblI12.Text == "False" || lblCk12.Text == "0") { dr["I12"] = -1; } else { dr["I12"] = txtI12.Text.Trim(); }
                        if (lblI13.Text == "False" || lblCk13.Text == "0") { dr["I13"] = -1; } else { dr["I13"] = txtI13.Text.Trim(); }
                        if (lblI14.Text == "False" || lblCk14.Text == "0") { dr["I14"] = -1; } else { dr["I14"] = txtI14.Text.Trim(); }
                        if (lblI15.Text == "False" || lblCk15.Text == "0") { dr["I15"] = -1; } else { dr["I15"] = txtI15.Text.Trim(); }


                        PayrollPaymasTbl.Rows.Add(dr);
                    }
                }

                CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayHeadAmountIncome_Bulk(PayrollPaymasTbl);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }

                if (count == 1)
                {
                    objCommon.DisplayMessage("Record Updated Successfully", this);
                    count = 0;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SumOfHead()", true);


                }



            }
            else
            {
                int C1 = 0;
                foreach (ListViewItem i in lvMonthlyChangesDeduction.Items)
                {
                    CheckBox chkIno = (CheckBox)i.FindControl("chkIno");
                    if (chkIno.Checked == true)
                    {
                        C1 = C1 + 1;
                    }
                }
                if (C1 == 0)
                {
                    ShowGrid();
                    objCommon.DisplayMessage("Please select atleast one employee from list !", this);
                    return;
                }



                DataTable PayrollPaymasTbl = new DataTable("PayrollPaymasTbl");
                PayrollPaymasTbl.Columns.Add("IDNO", typeof(int));
                PayrollPaymasTbl.Columns.Add("D1", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D2", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D3", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D4", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D5", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D6", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D7", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D8", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D9", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D10", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D11", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D12", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D13", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D14", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D15", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D16", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D17", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D18", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D19", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D20", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D21", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D22", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D23", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D24", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D25", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D26", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D27", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D28", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D29", typeof(decimal));
                PayrollPaymasTbl.Columns.Add("D30", typeof(decimal));


                DataRow dr = null;
                StringBuilder Strbuild = new StringBuilder();
                //For Issue insert

                foreach (ListViewItem i in lvMonthlyChangesDeduction.Items)
                {
                    CheckBox chkIno = (CheckBox)i.FindControl("chkIno");

                    Label lblD1 = i.FindControl("lblD1") as Label;
                    Label lblD2 = i.FindControl("lblD2") as Label;
                    Label lblD3 = i.FindControl("lblD3") as Label;
                    Label lblD4 = i.FindControl("lblD4") as Label;
                    Label lblD5 = i.FindControl("lblD5") as Label;
                    Label lblD6 = i.FindControl("lblD6") as Label;
                    Label lblD7 = i.FindControl("lblD7") as Label;
                    Label lblD8 = i.FindControl("lblD8") as Label;
                    Label lblD9 = i.FindControl("lblD9") as Label;
                    Label lblD10 = i.FindControl("lblD10") as Label;
                    Label lblD11 = i.FindControl("lblD11") as Label;
                    Label lblD12 = i.FindControl("lblD12") as Label;
                    Label lblD13 = i.FindControl("lblD13") as Label;
                    Label lblD14 = i.FindControl("lblD14") as Label;
                    Label lblD15 = i.FindControl("lblD15") as Label;
                    Label lblD16 = i.FindControl("lblD16") as Label;
                    Label lblD17 = i.FindControl("lblD17") as Label;
                    Label lblD18 = i.FindControl("lblD18") as Label;
                    Label lblD19 = i.FindControl("lblD19") as Label;
                    Label lblD20 = i.FindControl("lblD20") as Label;
                    Label lblD21 = i.FindControl("lblD21") as Label;
                    Label lblD22 = i.FindControl("lblD22") as Label;
                    Label lblD23 = i.FindControl("lblD23") as Label;
                    Label lblD24 = i.FindControl("lblD24") as Label;
                    Label lblD25 = i.FindControl("lblD25") as Label;
                    Label lblD26 = i.FindControl("lblD26") as Label;
                    Label lblD27 = i.FindControl("lblD27") as Label;
                    Label lblD28 = i.FindControl("lblD28") as Label;
                    Label lblD29 = i.FindControl("lblD29") as Label;
                    Label lblD30 = i.FindControl("lblD30") as Label;



                    Label lblCkD1 = i.FindControl("lblCkD1") as Label;
                    Label lblCkD2 = i.FindControl("lblCkD2") as Label;
                    Label lblCkD3 = i.FindControl("lblCkD3") as Label;
                    Label lblCkD4 = i.FindControl("lblCkD4") as Label;
                    Label lblCkD5 = i.FindControl("lblCkD5") as Label;
                    Label lblCkD6 = i.FindControl("lblCkD6") as Label;
                    Label lblCkD7 = i.FindControl("lblCkD7") as Label;
                    Label lblCkD8 = i.FindControl("lblCkD8") as Label;
                    Label lblCkD9 = i.FindControl("lblCkD9") as Label;
                    Label lblCkD10 = i.FindControl("lblCkD10") as Label;
                    Label lblCkD11 = i.FindControl("lblCkD11") as Label;
                    Label lblCkD12 = i.FindControl("lblCkD12") as Label;
                    Label lblCkD13 = i.FindControl("lblCkD13") as Label;
                    Label lblCkD14 = i.FindControl("lblCkD14") as Label;
                    Label lblCkD15 = i.FindControl("lblCkD15") as Label;
                    Label lblCkD16 = i.FindControl("lblCkD16") as Label;
                    Label lblCkD17 = i.FindControl("lblCkD17") as Label;
                    Label lblCkD18 = i.FindControl("lblCkD18") as Label;
                    Label lblCkD19 = i.FindControl("lblCkD19") as Label;
                    Label lblCkD20 = i.FindControl("lblCkD20") as Label;
                    Label lblCkD21 = i.FindControl("lblCkD21") as Label;
                    Label lblCkD22 = i.FindControl("lblCkD22") as Label;
                    Label lblCkD23 = i.FindControl("lblCkD23") as Label;
                    Label lblCkD24 = i.FindControl("lblCkD24") as Label;
                    Label lblCkD25 = i.FindControl("lblCkD25") as Label;
                    Label lblCkD26 = i.FindControl("lblCkD26") as Label;
                    Label lblCkD27 = i.FindControl("lblCkD27") as Label;
                    Label lblCkD28 = i.FindControl("lblCkD28") as Label;
                    Label lblCkD29 = i.FindControl("lblCkD29") as Label;
                    Label lblCkD30 = i.FindControl("lblCkD30") as Label;


                    TextBox txtD1 = i.FindControl("txtD1") as TextBox;
                    TextBox txtD2 = i.FindControl("txtD2") as TextBox;
                    TextBox txtD3 = i.FindControl("txtD3") as TextBox;
                    TextBox txtD4 = i.FindControl("txtD4") as TextBox;
                    TextBox txtD5 = i.FindControl("txtD5") as TextBox;
                    TextBox txtD6 = i.FindControl("txtD6") as TextBox;
                    TextBox txtD7 = i.FindControl("txtD7") as TextBox;
                    TextBox txtD8 = i.FindControl("txtD8") as TextBox;
                    TextBox txtD9 = i.FindControl("txtD9") as TextBox;
                    TextBox txtD10 = i.FindControl("txtD10") as TextBox;
                    TextBox txtD11 = i.FindControl("txtD11") as TextBox;
                    TextBox txtD12 = i.FindControl("txtD12") as TextBox;
                    TextBox txtD13 = i.FindControl("txtD13") as TextBox;
                    TextBox txtD14 = i.FindControl("txtD14") as TextBox;
                    TextBox txtD15 = i.FindControl("txtD15") as TextBox;
                    TextBox txtD16 = i.FindControl("txtD16") as TextBox;
                    TextBox txtD17 = i.FindControl("txtD17") as TextBox;
                    TextBox txtD18 = i.FindControl("txtD18") as TextBox;
                    TextBox txtD19 = i.FindControl("txtD19") as TextBox;
                    TextBox txtD20 = i.FindControl("txtD20") as TextBox;
                    TextBox txtD21 = i.FindControl("txtD21") as TextBox;
                    TextBox txtD22 = i.FindControl("txtD22") as TextBox;
                    TextBox txtD23 = i.FindControl("txtD23") as TextBox;
                    TextBox txtD24 = i.FindControl("txtD24") as TextBox;
                    TextBox txtD25 = i.FindControl("txtD25") as TextBox;
                    TextBox txtD26 = i.FindControl("txtD26") as TextBox;
                    TextBox txtD27 = i.FindControl("txtD27") as TextBox;
                    TextBox txtD28 = i.FindControl("txtD28") as TextBox;
                    TextBox txtD29 = i.FindControl("txtD29") as TextBox;
                    TextBox txtD30 = i.FindControl("txtD30") as TextBox;


                    if (chkIno.Checked == true)
                    {
                        dr = PayrollPaymasTbl.NewRow();

                        dr["IDNO"] = chkIno.ToolTip;

                        if (lblD1.Text == "False" || lblCkD1.Text == "0") { dr["D1"] = -1; } else { dr["D1"] = txtD1.Text.Trim(); }
                        if (lblD2.Text == "False" || lblCkD2.Text == "0") { dr["D2"] = -1; } else { dr["D2"] = txtD2.Text.Trim(); }
                        if (lblD3.Text == "False" || lblCkD3.Text == "0") { dr["D3"] = -1; } else { dr["D3"] = txtD3.Text.Trim(); }
                        if (lblD4.Text == "False" || lblCkD4.Text == "0") { dr["D4"] = -1; } else { dr["D4"] = txtD4.Text.Trim(); }
                        if (lblD5.Text == "False" || lblCkD5.Text == "0") { dr["D5"] = -1; } else { dr["D5"] = txtD5.Text.Trim(); }
                        if (lblD6.Text == "False" || lblCkD6.Text == "0") { dr["D6"] = -1; } else { dr["D6"] = txtD6.Text.Trim(); }
                        if (lblD7.Text == "False" || lblCkD7.Text == "0") { dr["D7"] = -1; } else { dr["D7"] = txtD7.Text.Trim(); }
                        if (lblD8.Text == "False" || lblCkD8.Text == "0") { dr["D8"] = -1; } else { dr["D8"] = txtD8.Text.Trim(); }
                        if (lblD9.Text == "False" || lblCkD9.Text == "0") { dr["D9"] = -1; } else { dr["D9"] = txtD9.Text.Trim(); }
                        if (lblD10.Text == "False" || lblCkD10.Text == "0") { dr["D10"] = -1; } else { dr["D10"] = txtD10.Text.Trim(); }
                        if (lblD11.Text == "False" || lblCkD11.Text == "0") { dr["D11"] = -1; } else { dr["D11"] = txtD11.Text.Trim(); }
                        if (lblD12.Text == "False" || lblCkD12.Text == "0") { dr["D12"] = -1; } else { dr["D12"] = txtD12.Text.Trim(); }
                        if (lblD13.Text == "False" || lblCkD13.Text == "0") { dr["D13"] = -1; } else { dr["D13"] = txtD13.Text.Trim(); }
                        if (lblD14.Text == "False" || lblCkD14.Text == "0") { dr["D14"] = -1; } else { dr["D14"] = txtD14.Text.Trim(); }
                        if (lblD15.Text == "False" || lblCkD15.Text == "0") { dr["D15"] = -1; } else { dr["D15"] = txtD15.Text.Trim(); }
                        if (lblD16.Text == "False" || lblCkD16.Text == "0") { dr["D16"] = -1; } else { dr["D16"] = txtD16.Text.Trim(); }
                        if (lblD17.Text == "False" || lblCkD17.Text == "0") { dr["D17"] = -1; } else { dr["D17"] = txtD17.Text.Trim(); }
                        if (lblD18.Text == "False" || lblCkD18.Text == "0") { dr["D18"] = -1; } else { dr["D18"] = txtD18.Text.Trim(); }
                        if (lblD19.Text == "False" || lblCkD19.Text == "0") { dr["D19"] = -1; } else { dr["D19"] = txtD19.Text.Trim(); }
                        if (lblD20.Text == "False" || lblCkD20.Text == "0") { dr["D20"] = -1; } else { dr["D20"] = txtD20.Text.Trim(); }
                        if (lblD21.Text == "False" || lblCkD21.Text == "0") { dr["D21"] = -1; } else { dr["D21"] = txtD21.Text.Trim(); }
                        if (lblD22.Text == "False" || lblCkD22.Text == "0") { dr["D22"] = -1; } else { dr["D22"] = txtD22.Text.Trim(); }
                        if (lblD23.Text == "False" || lblCkD23.Text == "0") { dr["D23"] = -1; } else { dr["D23"] = txtD23.Text.Trim(); }
                        if (lblD24.Text == "False" || lblCkD24.Text == "0") { dr["D24"] = -1; } else { dr["D24"] = txtD24.Text.Trim(); }
                        if (lblD25.Text == "False" || lblCkD25.Text == "0") { dr["D25"] = -1; } else { dr["D25"] = txtD25.Text.Trim(); }
                        if (lblD26.Text == "False" || lblCkD26.Text == "0") { dr["D26"] = -1; } else { dr["D26"] = txtD26.Text.Trim(); }
                        if (lblD27.Text == "False" || lblCkD27.Text == "0") { dr["D27"] = -1; } else { dr["D27"] = txtD27.Text.Trim(); }
                        if (lblD28.Text == "False" || lblCkD28.Text == "0") { dr["D28"] = -1; } else { dr["D28"] = txtD28.Text.Trim(); }
                        if (lblD29.Text == "False" || lblCkD29.Text == "0") { dr["D29"] = -1; } else { dr["D29"] = txtD29.Text.Trim(); }
                        if (lblD30.Text == "False" || lblCkD30.Text == "0") { dr["D30"] = -1; } else { dr["D30"] = txtD30.Text.Trim(); }


                        PayrollPaymasTbl.Rows.Add(dr);
                    }
                }

                CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayHeadAmountDeduction_Bulk(PayrollPaymasTbl);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }

                if (count == 1)
                {
                    objCommon.DisplayMessage("Record Updated Successfully", this);
                    count = 0;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SumOfHeadDeduct()", true);
                }


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

        //foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        //{
        //    TextBox txt = lvitem.FindControl("txtDays") as TextBox;
        //    txt.Text = string.Empty;
        //}
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlPayhead.SelectedIndex = 0;
        ddlEmployeeType.SelectedIndex = 0;
        ddlorderby.SelectedIndex = 0;
        ddlCollege.SelectedValue = "0";
        ddlDeptmon.SelectedValue = "0";

    }

    protected void FillPayHead(int uaNo)
    {
        try
        {
            PayHeadPrivilegesController objPayHead = new PayHeadPrivilegesController();
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
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
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
            objCommon.FillDropDownList(ddlDeptmon, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPT ");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowGrid()
    {
        int subheadnocnt = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAY_SUBPAYHEAD", "COUNT(1)", "PAYHEAD = '" + ddlPayhead.SelectedValue + "' AND PAYHEAD LIKE '%I%'"));

        if (subheadnocnt > 0 && ddlSubPayHead.SelectedValue != "0")
        {
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDeptmon.SelectedValue));
        }
        else if (subheadnocnt == 0 && ddlSubPayHead.SelectedValue == "0")
        {
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDeptmon.SelectedValue));
        }

        else
        {
            objCommon.DisplayMessage("Please select sub pay head", this);
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
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDeptmon.SelectedValue));
        }
        else if (subheadnocnt == 0 && ddlSubPayHead.SelectedValue == "0")
        {
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), ddlpayruleselect.SelectedValue, ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDeptmon.SelectedValue));
        }

        else
        {
            objCommon.DisplayMessage("Please select sub pay head", this);
        }

        if (ddlHeadType.SelectedValue == "I")
        {
            //to display employee count in footer
            txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChanges.Items.Count);
        }
        else
        {
            txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChangesDeduction.Items.Count);
        }

        //Used in javascript to display payhead desc
        hidPayhead.Value = ddlPayhead.SelectedItem.ToString();

        //display the total amount of payhead in footer 
        txtPayheadName.Text = "Total Amount of " + ddlPayhead.SelectedItem.ToString() + " = ";
        // this.TotalPayheadAmount();
    }

    protected void TotalPayheadAmount()
    {
        decimal totalPayheadAmount = 0;

        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            totalPayheadAmount = totalPayheadAmount + Convert.ToDecimal(txt.Text);
        }

        txtAmount.Text = totalPayheadAmount.ToString();

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

    protected void LvMonthlyChangesDeduction_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            var lblD1 = e.Item.FindControl("lblD1") as Label;
            var lblD2 = e.Item.FindControl("lblD2") as Label;
            var lblD3 = e.Item.FindControl("lblD3") as Label;
            var lblD4 = e.Item.FindControl("lblD4") as Label;
            var lblD5 = e.Item.FindControl("lblD5") as Label;
            var lblD6 = e.Item.FindControl("lblD6") as Label;
            var lblD7 = e.Item.FindControl("lblD7") as Label;
            var lblD8 = e.Item.FindControl("lblD8") as Label;
            var lblD9 = e.Item.FindControl("lblD9") as Label;
            var lblD10 = e.Item.FindControl("lblD10") as Label;
            var lblD11 = e.Item.FindControl("lblD11") as Label;
            var lblD12 = e.Item.FindControl("lblD12") as Label;
            var lblD13 = e.Item.FindControl("lblD13") as Label;
            var lblD14 = e.Item.FindControl("lblD14") as Label;
            var lblD15 = e.Item.FindControl("lblD15") as Label;
            var lblD16 = e.Item.FindControl("lblD16") as Label;
            var lblD17 = e.Item.FindControl("lblD17") as Label;
            var lblD18 = e.Item.FindControl("lblD18") as Label;
            var lblD19 = e.Item.FindControl("lblD19") as Label;
            var lblD20 = e.Item.FindControl("lblD20") as Label;
            var lblD21 = e.Item.FindControl("lblD21") as Label;
            var lblD22 = e.Item.FindControl("lblD22") as Label;
            var lblD23 = e.Item.FindControl("lblD23") as Label;
            var lblD24 = e.Item.FindControl("lblD24") as Label;
            var lblD25 = e.Item.FindControl("lblD25") as Label;
            var lblD26 = e.Item.FindControl("lblD26") as Label;
            var lblD27 = e.Item.FindControl("lblD27") as Label;
            var lblD28 = e.Item.FindControl("lblD28") as Label;
            var lblD29 = e.Item.FindControl("lblD29") as Label;
            var lblD30 = e.Item.FindControl("lblD30") as Label;



            var lblCkD1 = e.Item.FindControl("lblCkD1") as Label;
            var lblCkD2 = e.Item.FindControl("lblCkD2") as Label;
            var lblCkD3 = e.Item.FindControl("lblCkD3") as Label;
            var lblCkD4 = e.Item.FindControl("lblCkD4") as Label;
            var lblCkD5 = e.Item.FindControl("lblCkD5") as Label;
            var lblCkD6 = e.Item.FindControl("lblCkD6") as Label;
            var lblCkD7 = e.Item.FindControl("lblCkD7") as Label;
            var lblCkD8 = e.Item.FindControl("lblCkD8") as Label;
            var lblCkD9 = e.Item.FindControl("lblCkD9") as Label;
            var lblCkD10 = e.Item.FindControl("lblCkD10") as Label;
            var lblCkD11 = e.Item.FindControl("lblCkD11") as Label;
            var lblCkD12 = e.Item.FindControl("lblCkD12") as Label;
            var lblCkD13 = e.Item.FindControl("lblCkD13") as Label;
            var lblCkD14 = e.Item.FindControl("lblCkD14") as Label;
            var lblCkD15 = e.Item.FindControl("lblCkD15") as Label;
            var lblCkD16 = e.Item.FindControl("lblCkD16") as Label;
            var lblCkD17 = e.Item.FindControl("lblCkD17") as Label;
            var lblCkD18 = e.Item.FindControl("lblCkD18") as Label;
            var lblCkD19 = e.Item.FindControl("lblCkD19") as Label;
            var lblCkD20 = e.Item.FindControl("lblCkD20") as Label;
            var lblCkD21 = e.Item.FindControl("lblCkD21") as Label;
            var lblCkD22 = e.Item.FindControl("lblCkD22") as Label;
            var lblCkD23 = e.Item.FindControl("lblCkD23") as Label;
            var lblCkD24 = e.Item.FindControl("lblCkD24") as Label;
            var lblCkD25 = e.Item.FindControl("lblCkD25") as Label;
            var lblCkD26 = e.Item.FindControl("lblCkD26") as Label;
            var lblCkD27 = e.Item.FindControl("lblCkD27") as Label;
            var lblCkD28 = e.Item.FindControl("lblCkD28") as Label;
            var lblCkD29 = e.Item.FindControl("lblCkD29") as Label;
            var lblCkD30 = e.Item.FindControl("lblCkD30") as Label;


            var txtD1 = e.Item.FindControl("txtD1") as TextBox;
            var txtD2 = e.Item.FindControl("txtD2") as TextBox;
            var txtD3 = e.Item.FindControl("txtD3") as TextBox;
            var txtD4 = e.Item.FindControl("txtD4") as TextBox;
            var txtD5 = e.Item.FindControl("txtD5") as TextBox;
            var txtD6 = e.Item.FindControl("txtD6") as TextBox;
            var txtD7 = e.Item.FindControl("txtD7") as TextBox;
            var txtD8 = e.Item.FindControl("txtD8") as TextBox;
            var txtD9 = e.Item.FindControl("txtD9") as TextBox;
            var txtD10 = e.Item.FindControl("txtD10") as TextBox;
            var txtD11 = e.Item.FindControl("txtD11") as TextBox;
            var txtD12 = e.Item.FindControl("txtD12") as TextBox;
            var txtD13 = e.Item.FindControl("txtD13") as TextBox;
            var txtD14 = e.Item.FindControl("txtD14") as TextBox;
            var txtD15 = e.Item.FindControl("txtD15") as TextBox;
            var txtD16 = e.Item.FindControl("txtD16") as TextBox;
            var txtD17 = e.Item.FindControl("txtD17") as TextBox;
            var txtD18 = e.Item.FindControl("txtD18") as TextBox;
            var txtD19 = e.Item.FindControl("txtD19") as TextBox;
            var txtD20 = e.Item.FindControl("txtD20") as TextBox;
            var txtD21 = e.Item.FindControl("txtD21") as TextBox;
            var txtD22 = e.Item.FindControl("txtD22") as TextBox;
            var txtD23 = e.Item.FindControl("txtD23") as TextBox;
            var txtD24 = e.Item.FindControl("txtD24") as TextBox;
            var txtD25 = e.Item.FindControl("txtD25") as TextBox;
            var txtD26 = e.Item.FindControl("txtD26") as TextBox;
            var txtD27 = e.Item.FindControl("txtD27") as TextBox;
            var txtD28 = e.Item.FindControl("txtD28") as TextBox;
            var txtD29 = e.Item.FindControl("txtD29") as TextBox;
            var txtD30 = e.Item.FindControl("txtD30") as TextBox;

            if (lblD1.Text == "False" || lblCkD1.Text == "0") { txtD1.Enabled = false; }
            if (lblD2.Text == "False" || lblCkD2.Text == "0") { txtD2.Enabled = false; }
            if (lblD3.Text == "False" || lblCkD3.Text == "0") { txtD3.Enabled = false; }
            if (lblD4.Text == "False" || lblCkD4.Text == "0") { txtD4.Enabled = false; }
            if (lblD5.Text == "False" || lblCkD5.Text == "0") { txtD5.Enabled = false; }
            if (lblD6.Text == "False" || lblCkD6.Text == "0") { txtD6.Enabled = false; }
            if (lblD7.Text == "False" || lblCkD7.Text == "0") { txtD7.Enabled = false; }
            if (lblD8.Text == "False" || lblCkD8.Text == "0") { txtD8.Enabled = false; }
            if (lblD9.Text == "False" || lblCkD9.Text == "0") { txtD9.Enabled = false; }
            if (lblD10.Text == "False" || lblCkD10.Text == "0") { txtD10.Enabled = false; }
            if (lblD11.Text == "False" || lblCkD11.Text == "0") { txtD11.Enabled = false; }
            if (lblD12.Text == "False" || lblCkD12.Text == "0") { txtD12.Enabled = false; }
            if (lblD13.Text == "False" || lblCkD13.Text == "0") { txtD13.Enabled = false; }
            if (lblD14.Text == "False" || lblCkD14.Text == "0") { txtD14.Enabled = false; }
            if (lblD15.Text == "False" || lblCkD15.Text == "0") { txtD15.Enabled = false; }
            if (lblD16.Text == "False" || lblCkD16.Text == "0") { txtD16.Enabled = false; }
            if (lblD17.Text == "False" || lblCkD17.Text == "0") { txtD17.Enabled = false; }
            if (lblD18.Text == "False" || lblCkD18.Text == "0") { txtD18.Enabled = false; }
            if (lblD19.Text == "False" || lblCkD19.Text == "0") { txtD19.Enabled = false; }
            if (lblD20.Text == "False" || lblCkD20.Text == "0") { txtD20.Enabled = false; }
            if (lblD21.Text == "False" || lblCkD21.Text == "0") { txtD21.Enabled = false; }
            if (lblD22.Text == "False" || lblCkD22.Text == "0") { txtD22.Enabled = false; }
            if (lblD23.Text == "False" || lblCkD23.Text == "0") { txtD23.Enabled = false; }
            if (lblD24.Text == "False" || lblCkD24.Text == "0") { txtD24.Enabled = false; }
            if (lblD25.Text == "False" || lblCkD25.Text == "0") { txtD25.Enabled = false; }
            if (lblD26.Text == "False" || lblCkD26.Text == "0") { txtD26.Enabled = false; }
            if (lblD27.Text == "False" || lblCkD27.Text == "0") { txtD27.Enabled = false; }
            if (lblD28.Text == "False" || lblCkD28.Text == "0") { txtD28.Enabled = false; }
            if (lblD29.Text == "False" || lblCkD29.Text == "0") { txtD29.Enabled = false; }
            if (lblD30.Text == "False" || lblCkD30.Text == "0") { txtD30.Enabled = false; }




        }

    }

    protected void LvMonthlyChangesIncome_ItemDatabound(object sender, ListViewItemEventArgs e)
    {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            var lblI1 = e.Item.FindControl("lblI1") as Label;
            var lblI2 = e.Item.FindControl("lblI2") as Label;
            var lblI3 = e.Item.FindControl("lblI3") as Label;
            var lblI4 = e.Item.FindControl("lblI4") as Label;
            var lblI5 = e.Item.FindControl("lblI5") as Label;
            var lblI6 = e.Item.FindControl("lblI6") as Label;
            var lblI7 = e.Item.FindControl("lblI7") as Label;
            var lblI8 = e.Item.FindControl("lblI8") as Label;
            var lblI9 = e.Item.FindControl("lblI9") as Label;
            var lblI10 = e.Item.FindControl("lblI10") as Label;
            var lblI11 = e.Item.FindControl("lblI11") as Label;
            var lblI12 = e.Item.FindControl("lblI12") as Label;
            var lblI13 = e.Item.FindControl("lblI13") as Label;
            var lblI14 = e.Item.FindControl("lblI14") as Label;
            var lblI15 = e.Item.FindControl("lblI15") as Label;


            var lblCk1 = e.Item.FindControl("lblCk1") as Label;
            var lblCk2 = e.Item.FindControl("lblCk2") as Label;
            var lblCk3 = e.Item.FindControl("lblCk3") as Label;
            var lblCk4 = e.Item.FindControl("lblCk4") as Label;
            var lblCk5 = e.Item.FindControl("lblCk5") as Label;
            var lblCk6 = e.Item.FindControl("lblCk6") as Label;
            var lblCk7 = e.Item.FindControl("lblCk7") as Label;
            var lblCk8 = e.Item.FindControl("lblCk8") as Label;
            var lblCk9 = e.Item.FindControl("lblCk9") as Label;
            var lblCk10 = e.Item.FindControl("lblCk10") as Label;
            var lblCk11 = e.Item.FindControl("lblCk11") as Label;
            var lblCk12 = e.Item.FindControl("lblCk12") as Label;
            var lblCk13 = e.Item.FindControl("lblCk13") as Label;
            var lblCk14 = e.Item.FindControl("lblCk14") as Label;
            var lblCk15 = e.Item.FindControl("lblCk15") as Label;


            var txtI1 = e.Item.FindControl("txtI1") as TextBox;
            var txtI2 = e.Item.FindControl("txtI2") as TextBox;
            var txtI3 = e.Item.FindControl("txtI3") as TextBox;
            var txtI4 = e.Item.FindControl("txtI4") as TextBox;
            var txtI5 = e.Item.FindControl("txtI5") as TextBox;
            var txtI6 = e.Item.FindControl("txtI6") as TextBox;
            var txtI7 = e.Item.FindControl("txtI7") as TextBox;
            var txtI8 = e.Item.FindControl("txtI8") as TextBox;
            var txtI9 = e.Item.FindControl("txtI9") as TextBox;
            var txtI10 = e.Item.FindControl("txtI10") as TextBox;
            var txtI11 = e.Item.FindControl("txtI11") as TextBox;
            var txtI12 = e.Item.FindControl("txtI12") as TextBox;
            var txtI13 = e.Item.FindControl("txtI13") as TextBox;
            var txtI14 = e.Item.FindControl("txtI14") as TextBox;
            var txtI15 = e.Item.FindControl("txtI15") as TextBox;

            if (lblI1.Text == "False" || lblCk1.Text == "0")
            {
                txtI1.Enabled = false;
            }
            if (lblI2.Text == "False" || lblCk2.Text == "0")
            {
                txtI2.Enabled = false;
            }
            if (lblI3.Text == "False" || lblCk3.Text == "0")
            {
                txtI3.Enabled = false;
            }
            if (lblI4.Text == "False" || lblCk4.Text == "0")
            {
                txtI4.Enabled = false;
            }
            if (lblI5.Text == "False" || lblCk5.Text == "0")
            {
                txtI5.Enabled = false;
            }

            if (lblI6.Text == "False" || lblCk6.Text == "0")
            {
                txtI6.Enabled = false;
            }
            if (lblI7.Text == "False" || lblCk7.Text == "0")
            {
                txtI7.Enabled = false;
            }
            if (lblI8.Text == "False" || lblCk8.Text == "0")
            {
                txtI8.Enabled = false;
            }
            if (lblI9.Text == "False" || lblCk9.Text == "0")
            {
                txtI9.Enabled = false;
            }
            if (lblI10.Text == "False" || lblCk10.Text == "0")
            {
                txtI10.Enabled = false;
            }

            if (lblI11.Text == "False" || lblCk11.Text == "0")
            {
                txtI11.Enabled = false;
            }
            if (lblI12.Text == "False" || lblCk12.Text == "0")
            {
                txtI12.Enabled = false;
            }
            if (lblI13.Text == "False" || lblCk13.Text == "0")
            {
                txtI13.Enabled = false;
            }
            if (lblI14.Text == "False" || lblCk14.Text == "0")
            {
                txtI14.Enabled = false;
            }
            if (lblI15.Text == "False" || lblCk15.Text == "0")
            {
                txtI15.Enabled = false;
            }


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
        divbutton.Visible = false;

        objCommon.FillDropDownList(ddlSubPayHead, "PAYROLL_PAY_SUBPAYHEAD", "SUBHEADNO", "FULLNAME", "PAYHEAD='" + ddlPayhead.SelectedValue + "' AND PAYHEAD LIKE '%I%' ", "SUBHEADNO");

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlStaff.SelectedIndex = 0;
        //ddlDeptmon.SelectedIndex = 0;
        //ddlorderby.SelectedIndex = 0;
        //ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        divbutton.Visible = false;
        ClearGrid();
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDeptmon.SelectedIndex = 0;
        //ddlorderby.SelectedIndex = 0;
        //ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        divbutton.Visible = false;
        ClearGrid();
    }
    protected void ddlDeptmon_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlorderby.SelectedIndex = 0;
        //ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        divbutton.Visible = false;
        ClearGrid();

    }
    protected void ddlpayruleselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMonthlyChanges.Visible = false;
        divbutton.Visible = false;
        ClearGrid();
    }
    protected void ddlSubPayHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlCollege.SelectedIndex = 0;
        //ddlStaff.SelectedIndex = 0;
        //ddlDeptmon.SelectedIndex = 0;
        //ddlorderby.SelectedIndex = 0;
        //ddlpayruleselect.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        divbutton.Visible = false;
        ClearGrid();
    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMonthlyChanges.Visible = false;
        divbutton.Visible = false;
        ClearGrid();
    }
}