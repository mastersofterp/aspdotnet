//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:15-01-2016
//PURPOSE:EMPLOYEE ABSENT REPORT
//==============================================
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
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ESTABLISHMENT_LEAVES_Transactions_Estb_AbsentReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DataTable dtLDTimeInt = new DataTable();
    BioMetricsController objBioMetric = new BioMetricsController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (!Page.IsPostBack)
        {
            CheckPageAuthorization();
            //fills employee drop down list
            //FillEmployee();
            //fill department dropdownlist
            FillCollege();
            FillDepartmentStaff();//fill department and staff
            //creating structure of table
            dtLDTimeInt.TableName.Equals("LDTimeInterval");
            dtLDTimeInt.Columns.Add("IDNO");
            dtLDTimeInt.Columns.Add("USERID");
            dtLDTimeInt.Columns.Add("USERNAME");
            dtLDTimeInt.Columns.Add("DATE");
            dtLDTimeInt.Columns.Add("SHIFTINTIME");
            dtLDTimeInt.Columns.Add("INTIME");
            dtLDTimeInt.Columns.Add("OUTTIME");
            dtLDTimeInt.Columns.Add("HOURS");
            dtLDTimeInt.Columns.Add("LEAVETYPE");
            dtLDTimeInt.Columns.Add("STAFF_NAME");
            dtLDTimeInt.Columns.Add("SHIFTOUTTIME");

            ViewState["data"] = dtLDTimeInt;
           // txtFdate.Text = DateTime.Now.ToString();
           // txtDate.Text = DateTime.Now.ToString();


            if (ua_type != 1)
            {
                trEmp.Visible = true;
                //lblEmployee.Visible = true;
                //ddlEmployee.Visible = true;
                trdept.Visible = tr1.Visible = trcollege.Visible = false;
                trsearchtype.Visible = false;
                rblSelect.SelectedValue = "1";
                this.FillEmployeeIdno();
                //ddlEmployee.SelectedIndex = 1;
                btnCancel.Visible = false;
                trIn.Visible = trOut.Visible = trTimeFormat.Visible = trReportType.Visible = false;

            }
            else
            {
                trIn.Visible = trReportType.Visible = false;
                trdept.Visible = tr1.Visible = trcollege.Visible = true;
            }

            //Sachin 25 May 2017
            if (ua_type == 1 && Request.QueryString["EmployeeId"] != null)
            {
                imgBtnBack.Visible = true;
                trEmp.Visible = true;
                //lblEmployee.Visible = true;
                //ddlEmployee.Visible = true;
                trdept.Visible = tr1.Visible = false;
                trsearchtype.Visible = false;
                rblSelect.SelectedValue = "1";
                this.FillEmployeeIdno();
                //ddlEmployee.SelectedIndex = 1;
                btnCancel.Visible = false;
                trIn.Visible = trOut.Visible = trTimeFormat.Visible = trReportType.Visible = false;

            }

            if (Request.QueryString["EmployeeId"] != null)
            {
                imgBtnBack.Visible = true;
            }



        }
        else
        {
            //clearGrd();
            dtLDTimeInt = (DataTable)ViewState["data"];
            dtLDTimeInt.Clear();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "CODE");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    //reset the grid
    protected void clearGrd()
    {
        lblHead.Visible = false;
        //btnExport.Enabled = false;
        pnlGridview.Visible = false;
    }
    protected void clear()
    {
        //txtDate.Text = string.Empty;
        //ddldept.SelectedIndex = 0;
        rblSelect.SelectedValue = "0";

        //ddlEmployee.SelectedIndex = 0;
    }



    protected void FillEmployee()
    {
        //created dataset object
        int Emptype=0;
        DataSet ds = new DataSet();
        //ds = objBioMetric.GetEmployee(Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
        if(rblemptype.SelectedValue == "0")
        {
            Emptype=0;
        }
        else
        {
            Emptype=1;
        }

        ds = objBioMetric.GetEmployeeUser(Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Emptype);
        //to bind data source to Employee dropdown list
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlEmployee.Items.Clear();

            ddlEmployee.DataSource = ds;
            ddlEmployee.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlEmployee.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlEmployee.DataBind();
            ddlEmployee.SelectedItem.Text = "Please Select";



        }
    }



    // Sachin 25 May 2017

    protected void FillEmployeeIdno()
    {
        int IDNO = 0;
        // string employeeid = Request.QueryString["EmployeeId"].ToString().Trim();
        if (Request.QueryString["EmployeeId"] == null)//07-12-2016
        {
            IDNO = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            IDNO = Convert.ToInt32(Request.QueryString["EmployeeId"]);
        }


        //if (Request.QueryString["EmployeeId"].ToString() != null)
        //{
        //    IDNO = Convert.ToInt32(Request.QueryString["EmployeeId"]);
        //}
        //else
        //{
        //    IDNO = Convert.ToInt32(Session["idno"]);
        //}      


        int ua_type = Convert.ToInt32(Session["usertype"]);

        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME", "IDNO=" + IDNO, "");
        ddlEmployee.SelectedValue = IDNO.ToString();
        DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO", "stno,college_no", "IDNO=" + IDNO + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int subdeptno = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBDEPTNO"]);
            int STAFFNO = Convert.ToInt32(ds.Tables[0].Rows[0]["stno"]);
            int college_no = Convert.ToInt32(ds.Tables[0].Rows[0]["college_no"]);
            ddldept.SelectedValue = subdeptno.ToString();

            ddlStaff.SelectedValue = STAFFNO.ToString();
            ddlCollege.SelectedValue = college_no.ToString();

        }
    }


    //protected void FillEmployeeIdno()
    //{
    //    int IDNO = Convert.ToInt32(Session["idno"]);
    //    int ua_type = Convert.ToInt32(Session["usertype"]);

    //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME", "IDNO=" + IDNO, "");
    //    ddlEmployee.SelectedValue = IDNO.ToString();
    //    DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO", "STAFFNO", "IDNO=" + IDNO + "", "");
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        int subdeptno = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBDEPTNO"]);
    //        int STAFFNO = Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"]);
    //        ddldept.SelectedValue = subdeptno.ToString();
    //        ddlStaff.SelectedValue = STAFFNO.ToString();
    //    }
    //}



    private void FillDepartmentStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
            // objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0", "STAFFTYPE");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Enable/disable the employee drop down list
    protected void rblSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlEmployee.SelectedValue = "0";
        // ddlEmployee.SelectedItem.Text = "Please Select";
        //txtDate.Text = string.Empty;
        if (ddlCollege.SelectedIndex >= 0)
        {
            if (rblSelect.SelectedValue == "1")
            {
                ddlEmployee.Visible = true;
                //lblEmployee.Visible = true;
                //lbl.Visible = true;
                trEmp.Visible = true;
                FillEmployee();                
            }
            else if (rblSelect.SelectedValue == "0")
            {

                ddlEmployee.Visible = false;
                //lblEmployee.Visible = false;
                // lbl.Visible = false;
                trEmp.Visible = false;               
            }
        }
        else
        {
        }
    }

    //Shows the login logout details
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string time_from, time_to, in_out = string.Empty;

        if (rblTime.SelectedValue == "0")
        {
            if (txtInTimeFrom.Text != string.Empty && txtInTimeTo.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter End Intime Range", this);
                return;
            }
            else if (txtInTimeTo.Text != string.Empty && txtInTimeFrom.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Start Intime Range", this);
                return;
            }
            else
            {
                time_from = txtInTimeFrom.Text;
                time_to = txtInTimeTo.Text;
                in_out = "IN";
            }
        }
        else if (rblTime.SelectedValue == "1")
        {
            if (txtOutTimeFrom.Text != string.Empty && txtOutTimeTo.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter End Outtime Range", this);
                return;
            }
            else if (txtOutTimeFrom.Text != string.Empty && txtOutTimeTo.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Start Outtime Range", this);
                return;
            }
            else
            {
                time_from = txtOutTimeFrom.Text;
                time_to = txtOutTimeTo.Text;
                in_out = "OUT";
            }
        }
        else
        {
            time_from = "N";
            time_to = "N";
            in_out = "N";
        }


        DateTime fdate = Convert.ToDateTime(txtFdate.Text);
        DateTime tdate = Convert.ToDateTime(txtDate.Text);

        //int monthDiff = Math.Abs((fdate.Month - tdate.Month) + 12 * (fdate.Year - tdate.Year));
        //if (monthDiff > 1)
        //{
        //   // objCommon.DisplayUserMessage(UpdatePanel1, "Month Difference Can Not Be Greater Than One", this);
        //    //ShowMessage("Month Difference Can Not Be Greater Than One");
        //   // return;
        //}
        //else
        //{
        if (rblSelect.SelectedValue == "1" && ddlEmployee.SelectedIndex == 0)
        {
            // ShowMessage("Please Select Employee");
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee", this);
            return;
        }
        else
        {

            int IDNO = Convert.ToInt32(Session["idno"]);
            int ua_type = Convert.ToInt32(Session["usertype"]);

            //Check whether entered date must not greater than todays date
            if (Convert.ToDateTime(txtDate.Text) <= System.DateTime.Now)
            {
                string toDate = Convert.ToDateTime(txtDate.Text).AddMonths(1).ToString();
                toDate = Convert.ToDateTime(toDate).AddDays(-1).ToString();

                //set the month last date if month is not completed yet
                if (Convert.ToDateTime(toDate) >= System.DateTime.Now)
                    toDate = (System.DateTime.Now).ToString("dd/MM/yyyy");

                // string frmdate = Convert.ToDateTime(txtDate.Text).AddDays(-30).ToString();
                string frmdate = Convert.ToDateTime(txtDate.Text).ToString();
                //txtDate.Text = Convert.ToString(frmdate);

                //txtTodate.Text = Convert.ToDateTime(toDate).AddMonths(-1).ToString();
                DataSet ds = null;
                ds = objCommon.FillDropDown("EMP_BIOATTENDANCE_LOG", "userid", "userid", "CONVERT(DATE,LogTime) between '" + fdate.ToString("yyyy-MM-dd") + "' AND '" + tdate.ToString("yyyy-MM-dd") + "'", "");
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Sorry ! Record Not Exists", this);
                    return;
                }

                //if (rblSelect.SelectedValue == "0")
                //{
                //    //All Employee
                //    string empval = "0";
                //    //ddlEmployee.SelectedValue = empval;
                //    //if (ua_type != 1 && IDNO != 22)
                //    ds = objBioMetric.GetLoginDetails(Convert.ToDateTime(txtFdate.Text), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddldept.SelectedValue), time_from, time_to, in_out);
                //}
                int idno = 0;
                if (rblSelect.SelectedValue == "1")
                {
                    idno = Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                ds = objBioMetric.GetLoginDetails(Convert.ToDateTime(txtFdate.Text), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(idno), Convert.ToInt32(ddldept.SelectedValue), time_from, time_to, in_out, Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
                //lvEmpList.DataSource = ds;
                //lvEmpList.DataBind();
                //lvEmpList.Visible = true;
                int i;
                int rowCount = ds.Tables[0].Rows.Count;

                //for (i = 0; i < rowCount; i++)
                //{
                //    string id = Convert.ToString(ds.Tables[0].Rows[i]["IDNO"].ToString());

                //    ////calculate the login details of particulat employee in a month by time interval
                //    //while (id == Convert.ToString(ds.Tables[0].Rows[i]["IDNO"]))
                //    //{
                //    //DateTime fdate=Convert.ToDateTime(txtDate.Text);
                //    DataRow dr = dtLDTimeInt.NewRow();
                //    dr["IDNO"] = id;
                //    dr["USERNAME"] = ds.Tables[0].Rows[i]["USERNAME"];
                //    dr["DATE"] = ds.Tables[0].Rows[i]["ENTDATE"];
                //    dr["INTIME"] = ds.Tables[0].Rows[i]["INTIME"];
                //    dr["OUTTIME"] = ds.Tables[0].Rows[i]["OUTTIME"];
                //    if (ds.Tables[0].Rows[i]["INTIME"].ToString() != string.Empty && ds.Tables[0].Rows[i]["OUTTIME"].ToString() != string.Empty)
                //        dr["HOURS"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["OUTTIME"]) - Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]);
                //    else
                //        dr["HOURS"] = "-";
                //    dr["LEAVETYPE"] = ds.Tables[0].Rows[i]["LEAVETYPE"];
                //    dr["SHIFTINTIME"] = ds.Tables[0].Rows[i]["SHIFTINTIME"];
                //    dr["SHIFTOUTTIME"] = ds.Tables[0].Rows[i]["SHIFTOUTTIME"];
                //    dr["USERID"] = ds.Tables[0].Rows[i]["USERID"];
                //    dr["STAFF_NAME"] = ds.Tables[0].Rows[i]["STAFF_NAME"];
                //    dtLDTimeInt.Rows.Add(dr);
                //    dtLDTimeInt.AcceptChanges();

                //}
                //gvLoginDetails.DataSource = dtLDTimeInt;
                Rep_Info.DataSource = ds.Tables[0];
                Rep_Info.DataBind();
                pnlGridview.Visible = true;
                Rep_Info.Visible = true;

                //btnExport.Enabled = true;
                //lblHead.Text = "<b><u>" + "Employee Login/Logout Details for starting from " + txtDate.Text + " to " + Convert.ToDateTime(frmdate).ToString("dd/MM/yyyy") + "</b></u>";
                lblHead.Text = "<b><u>" + "Employee Login/Logout Details for starting from " + Convert.ToDateTime(txtFdate.Text).ToString("dd/MM/yyyy") + " to " + txtDate.Text + "</b></u>";
                lblHead.Visible = true;
                // clear();
            }
            else
            {
                //   ShowMessage("You have entered date beyond todays date. Please enter valid date.");
                objCommon.DisplayUserMessage(UpdatePanel1, "You have entered date beyond todays date. Please enter valid date", this);
            }

            System.Threading.Thread.Sleep(5000);

        }
        //}
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    this.Export();
    //}

    //export grid view to excel file
    private void Export()
    {
        //Response.Clear();
        //Response.AddHeader("content-disposition",
        //string.Format("attachment; filename={0}", lblHead.Text.Remove(0, 6) + ".xls"));
        //Response.Charset = "";
        //Response.ContentType = "application/vnd";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //gvLoginDetails.RenderControl(htw);
        //gvLoginDetails.Style.Add("Font-family", "Border");
        //Response.Write(sw.ToString());
        //Response.End();

        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=LoginDetails.xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.xls";
        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        ////gvLoginDetails.RenderControl(htmlWrite);
        //lvEmpList.RenderControl(htmlWrite);
        //Response.Write(stringWrite.ToString());
        //Response.End();
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int IDNO = Convert.ToInt32(Session["idno"]);
        //int ua_type = Convert.ToInt32(Session["usertype"]);

        //if (ua_type != 1 && IDNO != 22)
        //{
        //    this.FillEmployeeIdno();
        //}
        //else
        //{

        this.FillEmployee();

        //}
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {


            DateTime fdate = Convert.ToDateTime(txtFdate.Text);
            DateTime tdate = Convert.ToDateTime(txtDate.Text);

            //int monthDiff = Math.Abs((fdate.Month - tdate.Month) + 12 * (fdate.Year - tdate.Year));

            //if (monthDiff > 1)
            //{
            //    objCommon.DisplayUserMessage(UpdatePanel1, "Month Difference Can Not Be Greater Than One", this);
            //    //ShowMessage("Month Difference Can Not Be Greater Than One");
            //    return;
            //}
            //else
            //{
            DataSet ds = objCommon.FillDropDown("[dbo].[EMP_BIOATTENDANCE_LOG_COLLEGE]", "userid", "userid", "CONVERT(DATE,LogTime) between '" + fdate.ToString("yyyy-MM-dd") + "' AND '" + tdate.ToString("yyyy-MM-dd") + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (chkGraph.Checked == false && rblemptype.SelectedValue=="0")
                {
                    if (ddldept.SelectedIndex > 0)
                    {

                        //ShowReport("LoginDetail", "ESTB_LoginDetail_Report.rpt");
                        ShowReport("LoginDetailAll", "ESTB_Absent_Report1.rpt");

                    }
                    else
                    {

                        ShowReport("LoginDetailAll", "ESTB_Absent_Report1.rpt");

                    }
                }
                else if (chkGraph.Checked == true)
                {
                    if (rblTime.SelectedValue == "0")//Intime
                    {
                        // if (txtInTimeFrom.Text != string.Empty && txtInTimeTo.Text != string.Empty)
                        if (rblGraph.SelectedValue == "0")
                        {
                            if (txtInTimeFrom.Text != string.Empty && txtInTimeTo.Text != string.Empty)
                            {
                                if (ddldept.SelectedIndex > 0)
                                {
                                    ShowReport("LoginDetail", "ESTB_LoginDetail_Graph_Format1_dept.rpt");

                                }
                                else
                                {
                                    ShowReport("LoginDetail", "ESTB_LoginDetail_Graph_Format1.rpt");
                                }
                            }
                            else
                            {
                                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter In Time Range", this);
                                return;

                            }
                        }
                        else if (rblGraph.SelectedValue == "1")
                        {
                            ShowReport_Format2("LoginDetail", "ESTB_LoginDetail_Graph_Format2.rpt");
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Graph Format Type", this);
                            return;
                        }
                    }
                    else if (rblTime.SelectedValue == "1")//Outtime
                    {
                        //if (txtOutTimeFrom.Text != string.Empty && txtOutTimeTo.Text != string.Empty)
                        if (rblGraph.SelectedValue == "0")
                        {
                            if (txtOutTimeFrom.Text != string.Empty && txtOutTimeTo.Text != string.Empty)
                            {
                                if (ddldept.SelectedIndex > 0)
                                {
                                    ShowReport("LoginDetail", "ESTB_LoginDetail_Graph_Format1_dept.rpt");
                                }
                                else
                                {

                                    ShowReport("LoginDetail", "ESTB_LoginDetail_Graph_Format1.rpt");
                                }

                            }
                            else
                            {
                                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Out Time Range", this);
                                return;

                            }
                        }
                        else if (rblGraph.SelectedValue == "1")
                        {
                            ShowReport_Format2("LoginDetail", "ESTB_LoginDetail_Graph_Format2.rpt");
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Graph Format Type", this);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Select In Or Out Time Format", this);
                        return;
                    }
                    // 

                }
                else if (chkGraph.Checked == false  && rblemptype.SelectedValue == "1")
                {
                    ShowReportshift("LoginDetailAll", "ESTB_AbsentShiftEmp_Report1.rpt");
                }
                else
                {
                    // ShowReport("LoginDetail", "ESTB_LoginDetail_Graph_Format1.rpt");
                }

            }
            else
            {
                // ShowMessage("Sorry ! Record Not Exists");
                objCommon.DisplayUserMessage(UpdatePanel1, "Sorry ! Record Not Exists", this);
            }

            // }
            ////ESTB_LoginDetail_All_Report.rpt
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Login_details_Time_Interval.aspx.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //------------------------------------------------------------
            string time_from, time_to, in_out = string.Empty;

            // if (rblTime.SelectedValue == "0" && chkGraph.Checked ==false)
            if (rblTime.SelectedValue == "0")
            {
                if (txtInTimeFrom.Text != string.Empty && txtInTimeTo.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter End Intime Range", this);
                    return;
                }
                else if (txtInTimeTo.Text != string.Empty && txtInTimeFrom.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Start Intime Range", this);
                    return;
                }
                else
                {
                    time_from = txtInTimeFrom.Text;
                    time_to = txtInTimeTo.Text;
                    in_out = "IN";
                }
            }
            //else if (rblTime.SelectedValue == "1" && chkGraph.Checked == false)
            else if (rblTime.SelectedValue == "1")
            {
                if (txtOutTimeFrom.Text != string.Empty && txtOutTimeTo.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter End Outtime Range", this);
                    return;
                }
                else if (txtOutTimeTo.Text != string.Empty && txtOutTimeFrom.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Start Outtime Range", this);
                    return;
                }
                else
                {
                    time_from = txtOutTimeFrom.Text;
                    time_to = txtOutTimeTo.Text;
                    in_out = "OUT";
                }
            }
            else if (chkGraph.Checked == false)
            {
                time_from = "N";
                time_to = "N";
                in_out = "N";
            }
            else
            {
                if (rblTime.SelectedValue == "0" && chkGraph.Checked == true)
                {
                    in_out = "IN";
                    time_from = "N";
                    time_to = "N";
                }
                else if (rblTime.SelectedValue == "1" && chkGraph.Checked == true)
                {
                    in_out = "OUT";
                    time_from = "N";
                    time_to = "N";
                }
                else
                {
                    time_from = "N";
                    time_to = "N";
                    in_out = "N";
                }



            }


            //------------------------------------------------------



            //establishment//")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("biometrics")));//BIOMETRICS
            //int url1 = Request.Url.ToString().ToLower().IndexOf("establishment");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            int idno = 0;
            if (rblSelect.SelectedValue == "0")
            {
                idno = 0;
            }
            else
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    idno = Convert.ToInt32(ddlEmployee.SelectedValue);

                }
                else
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee", this);
                    return;
                }
            }

            url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy/MM/dd") +
                ",@P_TODATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy/MM/dd") + ",@P_IDNO=" + idno + ",@P_DEPTNO="
                + Convert.ToInt32(ddldept.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()
                + ",@P_TIME_FROM=" + time_from + ",@P_TIME_TO=" + time_to + ",@P_INOUT=" + in_out + 
                ",@P_STNO=" + Convert.ToInt32(ddlStaff.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " ";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Login_details_Time_Interval.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport_Format2(string reportTitle, string rptFileName)
    {
        try
        {
            //------------------------------------------------------------
            string time_from, time_to, in_out = string.Empty;
            if (rblTime.SelectedValue == "0" && chkGraph.Checked == true)
            {
                in_out = "IN";
                time_from = "N";
                time_to = "N";
            }
            else if (rblTime.SelectedValue == "1" && chkGraph.Checked == true)
            {
                in_out = "OUT";
                time_from = "N";
                time_to = "N";
            }
            else
            {
                time_from = "N";
                time_to = "N";
                in_out = "N";
            }






            //------------------------------------------------------




            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("BioMetrics")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            int idno = 0;
            if (rblSelect.SelectedValue == "0")
            {
                idno = 0;
            }
            else
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    idno = Convert.ToInt32(ddlEmployee.SelectedValue);

                }
                else
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee", this);
                    return;
                }
            }

            //url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy/MM/dd") + ",@P_IDNO=" + idno + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TIME_FROM=" + time_from + ",@P_TIME_TO=" + time_to + ",@P_INOUT=" + in_out + " ";
            url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy/MM/dd") + ",@P_IDNO=" + idno + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_INOUT=" + in_out + " ";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Login_details_Time_Interval.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Login_details_Time_Interval.aspx?pageno=1314");
        //ddlStaffType.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        rblemptype.SelectedIndex = 0;
        rblSelect.SelectedIndex = 0;
        txtFdate.Text = string.Empty;
        txtDate.Text = string.Empty;
        ddlEmployee.SelectedIndex = 0;
        trEmp.Visible = false;
        
    }

    private void ShowReportshift(string reportTitle, string rptFileName)
    {
        try
        {
            //------------------------------------------------------------
            string time_from, time_to, in_out = string.Empty;

            // if (rblTime.SelectedValue == "0" && chkGraph.Checked ==false)
            if (rblTime.SelectedValue == "0")
            {
                if (txtInTimeFrom.Text != string.Empty && txtInTimeTo.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter End Intime Range", this);
                    return;
                }
                else if (txtInTimeTo.Text != string.Empty && txtInTimeFrom.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Start Intime Range", this);
                    return;
                }
                else
                {
                    time_from = txtInTimeFrom.Text;
                    time_to = txtInTimeTo.Text;
                    in_out = "IN";
                }
            }
            //else if (rblTime.SelectedValue == "1" && chkGraph.Checked == false)
            else if (rblTime.SelectedValue == "1")
            {
                if (txtOutTimeFrom.Text != string.Empty && txtOutTimeTo.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter End Outtime Range", this);
                    return;
                }
                else if (txtOutTimeTo.Text != string.Empty && txtOutTimeFrom.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Start Outtime Range", this);
                    return;
                }
                else
                {
                    time_from = txtOutTimeFrom.Text;
                    time_to = txtOutTimeTo.Text;
                    in_out = "OUT";
                }
            }
            else if (chkGraph.Checked == false)
            {
                time_from = "N";
                time_to = "N";
                in_out = "N";
            }
            else
            {
                if (rblTime.SelectedValue == "0" && chkGraph.Checked == true)
                {
                    in_out = "IN";
                    time_from = "N";
                    time_to = "N";
                }
                else if (rblTime.SelectedValue == "1" && chkGraph.Checked == true)
                {
                    in_out = "OUT";
                    time_from = "N";
                    time_to = "N";
                }
                else
                {
                    time_from = "N";
                    time_to = "N";
                    in_out = "N";
                }



            }


            //------------------------------------------------------



            //establishment//")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("biometrics")));//BIOMETRICS
            //int url1 = Request.Url.ToString().ToLower().IndexOf("establishment");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            int idno = 0;
            if (rblSelect.SelectedValue == "0")
            {
                idno = 0;
            }
            else
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    idno = Convert.ToInt32(ddlEmployee.SelectedValue);

                }
                else
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee", this);
                    return;
                }
            }

            url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy/MM/dd") +
                ",@P_TO_DATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy/MM/dd") + ",@P_DEPTNO="
                + Convert.ToInt32(ddldept.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()
                + ",@P_COUNT=" + 0 + ",@P_STNO=" + Convert.ToInt32(ddlStaff.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " ";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Login_details_Time_Interval.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtFdate_TextChanged(object sender, EventArgs e)
    {
        Rep_Info.DataSource = null;
        Rep_Info.DataBind();
        lblHead.Text = string.Empty;
        pnlGridview.Visible = false;
        txtDate.Focus();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        //lvEmpList.Visible = false;
        {
            if (Convert.ToDateTime(txtDate.Text) < Convert.ToDateTime(txtFdate.Text))
            {
                MessageBox("To Date must be greater than From date");
                txtDate.Text = string.Empty;
            }

            Rep_Info.DataSource = null;
            Rep_Info.DataBind();
            lblHead.Text = string.Empty;
            pnlGridview.Visible = false;
            txtInTimeFrom.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void txtInTimeFrom_TextChanged(object sender, EventArgs e)
    {
        Rep_Info.DataSource = null;
        Rep_Info.DataBind();
        lblHead.Text = string.Empty;
        pnlGridview.Visible = false;
        txtInTimeTo.Focus();
    }
    protected void txtInTimeTo_TextChanged(object sender, EventArgs e)
    {
        Rep_Info.DataSource = null;
        Rep_Info.DataBind();
        lblHead.Text = string.Empty;
        pnlGridview.Visible = false;
        btnShow.Focus();
    }

    protected void rblTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTime.SelectedValue == "0")
        {
            trIn.Visible = false;
            trOut.Visible = false;
        }
        else if (rblTime.SelectedValue == "1")
        {
            trIn.Visible = false;
            trOut.Visible = false;
        }
    }
    protected void txtOutTimeFrom_TextChanged(object sender, EventArgs e)
    {
        Rep_Info.DataSource = null;
        Rep_Info.DataBind();
        lblHead.Text = string.Empty;
        pnlGridview.Visible = false;
        btnShow.Focus();

    }
    protected void txtOutTimeTo_TextChanged(object sender, EventArgs e)
    {
        Rep_Info.DataSource = null;
        Rep_Info.DataBind();
        lblHead.Text = string.Empty;
        pnlGridview.Visible = false;
        btnShow.Focus();
    }


    protected void chkGraph_CheckedChanged(object sender, EventArgs e)
    {
        if (chkGraph.Checked == true)
        {
            trFormat.Visible = true;
        }
        else
        {
            trFormat.Visible = false;

        }

    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);


        if (Convert.ToDateTime(txtDate.Text) <= System.DateTime.Now)
        {

            DataSet ds = null;
            if (rblSelect.SelectedValue == "0")
            {
                ds = objBioMetric.GetLoginDetailsForExcel(Convert.ToDateTime(txtFdate.Text), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(IDNO), Convert.ToInt32(ddldept.SelectedValue), "N", "N", "N", Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
            }
            else
            {
                ds = objBioMetric.GetLoginDetailsForExcel(Convert.ToDateTime(txtFdate.Text), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(IDNO), Convert.ToInt32(ddldept.SelectedValue), "N", "N", "N", Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=LoginDetails.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
        }



    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void rblemptype_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillEmployee();
    }
}