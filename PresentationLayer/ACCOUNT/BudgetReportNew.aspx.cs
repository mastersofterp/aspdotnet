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
using System.IO;

public partial class ACCOUNT_Default : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenterController objCostCenterController = new CostCenterController();
    GridView gvBudgetReport = new GridView();//Added by vijay on 20-08-2020 for excel report for budget





    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                getCollegeName();
            }
            txtFrmDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
            txtUptoDate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
        }

    }
    protected void btndb_Click(object sender, EventArgs e)
    {

    }
    protected void getCollegeName()
    {
        DataSet ds = objCommon.FillDropDown("REFF", "CollegeName", "College_address", "College_code=" + Session["colcode"].ToString(), "");
        if (ds != null)
        {
            Session["CollegeName"] = ds.Tables[0].Rows[0]["CollegeName"].ToString();
            string s = txtFrmDate.Text + txtUptoDate.Text;
            Session["clgAddress"] = ds.Tables[0].Rows[0]["College_address"].ToString().Replace(",", "") + s;
        }
    }
    //protected void btnRpt_Click(object sender, EventArgs e)
    //{
    //try
    //{
    //    if (txtFrmDate.Text == "" || txtUptoDate.Text == "" || txtFrmDate.Text == null || txtUptoDate.Text == null)
    //    {
    //        objCommon.DisplayMessage(UPBudget, "Please select Date.", this.Page);
    //        return;
    //    }
    //    #region
    //    if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //    {
    //        objCommon.DisplayMessage(UPBudget, "Upto Date Should Be In The Financial Year Range. ", this);
    //        txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //        txtFrmDate.Focus();
    //        return;
    //    }

    //    if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //    {
    //        objCommon.DisplayMessage(UPBudget, "From Date Should Be In The Financial Year Range. ", this);
    //        txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //        txtFromDate.Focus();
    //        return;
    //    }

    //    if (DateTime.Compare(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txttodate.Text)) == 1)
    //    {
    //        objCommon.DisplayMessage(UPBudgetApprove, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //        txttodate.Focus();
    //        return;
    //    }
    //    #endregion



    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Budget_Head_Report.btnRpt_Click()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_Report_type=" + Session["Report_type"].ToString() + ",@P_clgAddress=" + Session["clgAddress"].ToString() + ",@P_Username=" + Session["UserName"].ToString() + ",@P_CollegeName=" + Session["CollegeName"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text.ToString()).ToString("yyyy-MMM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtUptoDate.Text.ToString()).ToString("yyyy-MMM-dd");
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UPBudget, UPBudget.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BUDGETREPORT.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void rdbApplied_CheckedChanged(object sender, EventArgs e)
    {


    }
    protected void rdbApproved_CheckedChanged(object sender, EventArgs e)
    {


    }

    protected void btnrpt_Click(object sender, EventArgs e)
    {
        Session["Report_type"] = null;
        if (rdbApplied.Checked == true)
        {
            Session["Report_type"] = "Applied Budget";
            Session["reportname"] = "Applied_BudgetReportNew.rpt";
        }
        else if (rdbApproved.Checked == true)
        {
            Session["Report_type"] = "Approved Budget";
            Session["reportname"] = "Approved_BudgetReportNew.rpt";
        }
        string s = txtFrmDate.Text.Substring(6) + "-" + txtUptoDate.Text.Substring(6);
        Session["Report_type"] = Session["Report_type"] + "   " + s;
        ShowReport("Budget_Report", Session["reportName"].ToString());
        Session["Report_type"] = string.Empty;
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {

        string Isapproved=string.Empty;
        string ReportHead = string.Empty;
        if(rdbApplied.Checked)
        {
            Isapproved="P";
            ReportHead = "Applied Budget";
        }
        if(rdbApproved.Checked)
        {
              Isapproved="A";
              ReportHead = "Approved Budget";
        }
        DataSet ds = new DataSet();
        ds = objCostCenterController.GETAPPLIEDBUDGET(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text), Isapproved);
        //gvBudgetReport.RowDataBound += new GridViewRowEventHandler(gvBudgetReport_ro);
        DataTable dt = new DataTable();
        dt = SetColumns();
        dt = AddTable(ds, dt);
        gvBudgetReport.DataSource = dt;
        gvBudgetReport.DataBind();
        int ColumnCount = dt.Columns.Count;

        string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
        string attachment = "attachment; filename=" + ReportHead + ".xls";

        AddHeader(ColumnCount, ReportHead);


        gvBudgetReport.FooterStyle.Font.Bold = true;
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", attachment);
        Response.AppendHeader("Refresh", ".5; BudgetReportNew.aspx");
        Response.Charset = "";
        Response.ContentType = "application/" + ContentType;
        StringWriter sw1 = new StringWriter();
        HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
        gvBudgetReport.RenderControl(htw1);
        Response.Output.Write(sw1.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();




    }

    private void AddEmptyRow(DataTable dt, DataTable dtEcxcel, string GrandTotal, int RowPos)
    {


        //var result = dt.AsEnumerable().Where(x => Convert.ToInt32(x["SRNO"])).Sum(x => Convert.ToDouble(x["Administration"]));
        //    double result = dt.AsEnumerable().Where(x => x.Field<string>("Parent_id") == ds.Tables[0].Rows[i]["PARENT_ID"].ToString()).Sum(x => Convert.ToDouble(x.Field<string>("Administration")));
        DataRow dr = dtEcxcel.NewRow(); //Create New Row

        dr["BudgetHead"] = GrandTotal.ToUpper();
        dr["Administration"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Administration"]));
        dr["Computer Sience"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Computer Sience"]));
        dr["Centre of Robotics and 3D printing"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Centre of Robotics and 3D printing"]));
        dr["Department of Forensic Science"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Forensic Science"]));
        dr["Department of Media Science"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Media Science"]));
        dr["Registrar"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Registrar"]));
        dr["Workshop"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Workshop"]));
        dr["Controller of Examinations"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Controller of Examinations"]));
        dr["Engineering Section"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Engineering Section"]));
        dr["Finance Department"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Finance Department"]));
        dr["Finance officer"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Finance officer"]));
        dr["Heritage Cell"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Heritage Cell"]));
        dr["Information Scientist"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Information Scientist"]));
        dr["Inspector of Colleges"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Inspector of Colleges"]));
        dr["Department of Computer Science& Engineering"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Computer Science& Engineering"]));
        dr["Vice-Chancellor"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Vice-Chancellor"]));
        dr["Centre for Linguistics"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Centre for Linguistics"]));
        dr["Department Material Science and Technology"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department Material Science and Technology"]));
        dr["Department of Bio-informatics"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Bio-informatics"]));
        dr["Department of Bio-technology"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Bio-technology"]));        
        dr["Department of Environment Sciences"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Environment Sciences"]));
        dr["Department of Food Science and Technology"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Food Science and Technology"]));
        dr["Department of Hospital Management"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Hospital Management"]));
        dr["Department of Industrial Engineering and Management"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Industrial Engineering and Management"]));
        dr["Department of Information Technology"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Information Technology"]));
        dr["Department of Management Science"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Management Science"]));
        dr["Department of Management Sciences"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Management Sciences"]));
        dr["Department of Microelectronics & VLSI"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Microelectronics & VLSI"]));
        dr["Department of Natural and Applied Science"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Natural and Applied Science"]));
        dr["Department of Renewable Energy"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Renewable Energy"]));
        dr["Department of Travel and Tourism"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department of Travel and Tourism"]));
        dr["Department Pharmaceutical Science and Technology"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Department Pharmaceutical Science and Technology"]));
        dr["Waste Utilize"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Waste Utilize"]));
        dr["Information Technology"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Information Technology"]));
        dr["Training & Placement"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Training & Placement"]));
        dr["TBI & Industry Institute Partnership Cell"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TBI & Industry Institute Partnership Cell"]));
        dr["Internal Quality Assurance Cell"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Internal Quality Assurance Cell"]));
        dr["Social Science"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Social Science"]));
        dr["Publication Division"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Publication Division"]));
        dr["IIPC"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["IIPC"]));
        dr["MED"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["MED"]));
        dr["PHD"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["PHD"]));      
        dr["Other Administrative Expenses department"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Other Administrative Expenses department"]));
        dr["Total"] = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TOTAL"]));
        dtEcxcel.Rows.InsertAt(dr, RowPos);


    }

    private DataTable AddTable(DataSet ds, DataTable dt)
    {
        DataRow Row1;
        for (int i = 0; i < 2; i++)
        {

            Row1 = dt.NewRow();
            Row1[i] = "";
            dt.Rows.InsertAt(Row1, i);
        }

        int Rowcount = 2;
        for (int Bud = 1; Bud < 3; Bud++)
        {
            Rowcount = dt.Rows.Count;
            DataRow[] ParentRow = ds.Tables[0].Select("BUDGET_PRAPROSAL_ID=" + Bud.ToString());
            if (ParentRow.Length > 0)
            {

                DataTable dtParentId = ParentRow.CopyToDataTable();
                dt.Rows[Rowcount - 2]["BudgetHead"] = dtParentId.Rows[0]["BUDGET_PRAPOSAL_HEAD"].ToString();
                dt.Rows[Rowcount - 2].AcceptChanges();
                for (int j = 0; j < dtParentId.Rows.Count; j++)
                {
                    DataRow[] datarow = ds.Tables[1].Select("BUDGET_PRAPROSAL_ID=" + Bud.ToString() + "AND  PARENT_ID=" + dtParentId.Rows[j]["PARENT_ID"].ToString());
                    DataTable dttemp = datarow.CopyToDataTable();

                    Rowcount = dt.Rows.Count;
                    dt.Rows[Rowcount - 1]["BudgetHead"] = "  " + dttemp.Rows[0]["PARENT_HEAD"].ToString().ToUpper();
                    dt.Rows[Rowcount - 1].AcceptChanges();
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        #region IsertRow
                        DataRow Row;
                        Row = dt.NewRow();
                        Row["CODE"] = dttemp.Rows[i]["CODE"].ToString();
                        Row["BudgetHead"] = "        " + dttemp.Rows[i]["CHILD_HEAD"].ToString();
                        //Row["Parent_id"] = dttemp.Rows[i]["PARENT_ID"].ToString();
                        Row["Administration"] = dttemp.Rows[i]["Administration"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Administration"].ToString();
                        Row["Computer Sience"] = dttemp.Rows[i]["Computer Sience"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Computer Sience"].ToString();
                        Row["Centre of Robotics and 3D printing"] = dttemp.Rows[i]["Centre of Robotics and 3D printing"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Centre of Robotics and 3D printing"].ToString();
                        Row["Department of Forensic Science"] = dttemp.Rows[i]["Department of Forensic Science"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Forensic Science"].ToString();
                        Row["Department of Media Science"] = dttemp.Rows[i]["Department of Media Science"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Media Science"].ToString();
                        Row["Registrar"] = dttemp.Rows[i]["Registrar"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Registrar"].ToString();
                        Row["Workshop"] = dttemp.Rows[i]["Workshop"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Workshop"].ToString();
                        Row["Controller of Examinations"] = dttemp.Rows[i]["Controller of Examinations"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Controller of Examinations"].ToString();
                        Row["Engineering Section"] = dttemp.Rows[i]["Engineering Section"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Engineering Section"].ToString();
                        Row["Finance Department"] = dttemp.Rows[i]["Finance Department"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Finance Department"].ToString();
                        Row["Finance officer"] = dttemp.Rows[i]["Finance officer"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Finance officer"].ToString();
                        Row["Heritage Cell"] = dttemp.Rows[i]["Heritage Cell"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Heritage Cell"].ToString();
                        Row["Information Scientist"] = dttemp.Rows[i]["Information Scientist"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Information Scientist"].ToString();
                        Row["Inspector of Colleges"] = dttemp.Rows[i]["Inspector of Colleges"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Inspector of Colleges"].ToString();
                        Row["Department of Computer Science& Engineering"] = dttemp.Rows[i]["Department of Computer Science& Engineering"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Computer Science& Engineering"].ToString();
                        Row["Vice-Chancellor"] = dttemp.Rows[i]["Vice-Chancellor"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Vice-Chancellor"].ToString();
                        Row["Centre for Linguistics"] = dttemp.Rows[i]["Centre for Linguistics"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Centre for Linguistics"].ToString();
                        Row["Department Material Science and Technology"] = dttemp.Rows[i]["Department Material Science and Technology"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department Material Science and Technology"].ToString();
                        Row["Department of Bio-informatics"] = dttemp.Rows[i]["Department of Bio-informatics"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Bio-informatics"].ToString();
                        Row["Department of Bio-technology"] = dttemp.Rows[i]["Department of Bio-technology"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Bio-technology"].ToString();
                        Row["Department of Environment Sciences"] = dttemp.Rows[i]["Department of Environment Sciences"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Environment Sciences"].ToString();
                        Row["Department of Food Science and Technology"] = dttemp.Rows[i]["Department of Food Science and Technology"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Food Science and Technology"].ToString();
                        Row["Department of Hospital Management"] = dttemp.Rows[i]["Department of Hospital Management"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Hospital Management"].ToString();
                        Row["Department of Industrial Engineering and Management"] = dttemp.Rows[i]["Department of Industrial Engineering and Management"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Industrial Engineering and Management"].ToString();
                        Row["Department of Information Technology"] = dttemp.Rows[i]["Department of Information Technology"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Information Technology"].ToString();
                        Row["Department of Management Science"] = dttemp.Rows[i]["Department of Management Science"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Management Science"].ToString();
                        Row["Department of Management Sciences"] = dttemp.Rows[i]["Department of Management Sciences"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Management Sciences"].ToString();
                        Row["Department of Microelectronics & VLSI"] = dttemp.Rows[i]["Department of Microelectronics & VLSI"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Microelectronics & VLSI"].ToString();
                        Row["Department of Natural and Applied Science"] = dttemp.Rows[i]["Department of Natural and Applied Science"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Natural and Applied Science"].ToString();
                        Row["Department of Renewable Energy"] = dttemp.Rows[i]["Department of Renewable Energy"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Renewable Energy"].ToString();
                        Row["Department of Travel and Tourism"] = dttemp.Rows[i]["Department of Travel and Tourism"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department of Travel and Tourism"].ToString();
                        Row["Department Pharmaceutical Science and Technology"] = dttemp.Rows[i]["Department Pharmaceutical Science and Technology"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Department Pharmaceutical Science and Technology"].ToString();
                        Row["Waste Utilize"] = dttemp.Rows[i]["Waste Utilize"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Waste Utilize"].ToString();
                        Row["Information Technology"] = dttemp.Rows[i]["Information Technology"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Information Technology"].ToString();
                        Row["Training & Placement"] = dttemp.Rows[i]["Training & Placement"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Training & Placement"].ToString();
                        Row["TBI & Industry Institute Partnership Cell"] = dttemp.Rows[i]["TBI & Industry Institute Partnership Cell"].ToString() == "" ? "0.00" : dttemp.Rows[i]["TBI & Industry Institute Partnership Cell"].ToString();
                        Row["Internal Quality Assurance Cell"] = dttemp.Rows[i]["Internal Quality Assurance Cell"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Internal Quality Assurance Cell"].ToString();
                        Row["Social Science"] = dttemp.Rows[i]["Social Science"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Social Science"].ToString();
                        Row["Publication Division"] = dttemp.Rows[i]["Publication Division"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Publication Division"].ToString();
                        Row["IIPC"] = dttemp.Rows[i]["IIPC"].ToString() == "" ? "0.00" : dttemp.Rows[i]["IIPC"].ToString();
                        Row["MED"] = dttemp.Rows[i]["MED"].ToString() == "" ? "0.00" : dttemp.Rows[i]["MED"].ToString();
                        Row["PHD"] = dttemp.Rows[i]["PHD"].ToString() == "" ? "0.00" : dttemp.Rows[i]["PHD"].ToString();
                        Row["Other Administrative Expenses department"] = dttemp.Rows[i]["Other Administrative Expenses department"].ToString() == "" ? "0.00" : dttemp.Rows[i]["Other Administrative Expenses department"].ToString();
                        Row["Total"] = dttemp.Rows[i]["TOTAL"].ToString() == "" ? "0.00" : dttemp.Rows[i]["TOTAL"].ToString(); ;
                        dt.Rows.InsertAt(Row, Rowcount + i);
                        #endregion
                    }


                    for (int e = 1; e < 4; e++)
                    {
                        #region AddTotal
                        if (e == 1)
                        {

                            AddEmptyRow(dttemp, dt, "TOTAL", dt.Rows.Count + 1);
                        }
                        else
                        {
                            int dtcount = dt.Rows.Count;
                            Row1 = dt.NewRow();
                            Row1[e] = "";
                            dt.Rows.InsertAt(Row1, dtcount + e);
                        }
                        #endregion
                    }


                }

                DataRow[] datarow1 = ds.Tables[1].Select("BUDGET_PRAPROSAL_ID=" + Bud.ToString());

                DataTable dttemp1 = datarow1.CopyToDataTable();
                AddEmptyRow(dttemp1, dt, "GRAND TOTAL", dt.Rows.Count - 2);
                if (Bud == 1)
                {
                    for (int e = 1; e < 3; e++)
                    {
                        int dtcount = dt.Rows.Count;
                        Row1 = dt.NewRow();
                        Row1[e] = "";
                        dt.Rows.InsertAt(Row1, dtcount + e);
                    }
                }
            }

        }

        return dt;
    }

    protected DataTable SetColumns()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CODE", typeof(string));
        dt.Columns.Add("BudgetHead", typeof(string));
        //dt.Columns.Add("Parent_id", typeof(string));
        dt.Columns.Add("Administration", typeof(string));
        dt.Columns.Add("Computer Sience", typeof(string));
        dt.Columns.Add("Centre of Robotics and 3D printing", typeof(string));
        dt.Columns.Add("Department of Forensic Science", typeof(string));
        dt.Columns.Add("Department of Media Science", typeof(string));
        dt.Columns.Add("Registrar", typeof(string));
        dt.Columns.Add("Workshop", typeof(string));
        dt.Columns.Add("Controller of Examinations", typeof(string));
        dt.Columns.Add("Engineering Section", typeof(string));
        dt.Columns.Add("Finance Department", typeof(string));
        dt.Columns.Add("Finance officer", typeof(string));
        dt.Columns.Add("Heritage Cell", typeof(string));
        dt.Columns.Add("Information Scientist", typeof(string));
        dt.Columns.Add("Inspector of Colleges", typeof(string));
        dt.Columns.Add("Department of Computer Science& Engineering", typeof(string));
        dt.Columns.Add("Vice-Chancellor", typeof(string));
        dt.Columns.Add("Centre for Linguistics", typeof(string));
        dt.Columns.Add("Department Material Science and Technology", typeof(string));
        dt.Columns.Add("Department of Bio-informatics", typeof(string));
        dt.Columns.Add("Department of Bio-technology", typeof(string));       
        dt.Columns.Add("Department of Environment Sciences", typeof(string));
        dt.Columns.Add("Department of Food Science and Technology", typeof(string));
        dt.Columns.Add("Department of Hospital Management", typeof(string));
        dt.Columns.Add("Department of Industrial Engineering and Management", typeof(string));
        dt.Columns.Add("Department of Information Technology", typeof(string));
        dt.Columns.Add("Department of Management Science", typeof(string));
        dt.Columns.Add("Department of Management Sciences", typeof(string));
        dt.Columns.Add("Department of Microelectronics & VLSI", typeof(string));
        dt.Columns.Add("Department of Natural and Applied Science", typeof(string));
        dt.Columns.Add("Department of Renewable Energy", typeof(string));
        dt.Columns.Add("Department of Travel and Tourism", typeof(string));
        dt.Columns.Add("Department Pharmaceutical Science and Technology", typeof(string));
        dt.Columns.Add("Waste Utilize", typeof(string));
        dt.Columns.Add("Information Technology", typeof(string));
        dt.Columns.Add("Training & Placement", typeof(string));
        dt.Columns.Add("TBI & Industry Institute Partnership Cell", typeof(string));
        dt.Columns.Add("Internal Quality Assurance Cell", typeof(string));
        dt.Columns.Add("Social Science", typeof(string));
        dt.Columns.Add("Publication Division", typeof(string));
        dt.Columns.Add("IIPC", typeof(string));
        dt.Columns.Add("MED", typeof(string));
        dt.Columns.Add("PHD", typeof(string));       
        dt.Columns.Add("Other Administrative Expenses department", typeof(string));
        dt.Columns.Add("Total", typeof(string));

        return dt;
    }

    private void AddHeader(int colspan,string ReportHead)
    {

        DataSet ds1 = objCommon.FillDropDown("reff", "CollegeName", "College_address", "College_code=" + Session["colcode"].ToString(), "");

        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = ds1.Tables[0].Rows[0]["CollegeName"].ToString().ToUpper();
        HeaderCell.ColumnSpan = colspan;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.ForeColor = System.Drawing.Color.Black;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        gvBudgetReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = ds1.Tables[0].Rows[0]["College_address"].ToString().ToUpper();
        HeaderCell1.ColumnSpan = colspan;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        gvBudgetReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = "Budget 2020-2021";
        HeaderCell2.ColumnSpan = colspan;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        gvBudgetReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "";
        HeaderCell3.ColumnSpan = colspan;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        gvBudgetReport.Controls[0].Controls.AddAt(3, HeaderGridRow3);

        GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell4 = new TableCell();
        HeaderCell4 = new TableCell();
        HeaderCell4.Text = ReportHead;
        HeaderCell4.ColumnSpan = colspan;
        HeaderCell4.BackColor = System.Drawing.Color.White;
        HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow4.Cells.Add(HeaderCell4);
        gvBudgetReport.Controls[0].Controls.AddAt(4, HeaderGridRow4);

        gvBudgetReport.FooterStyle.Font.Bold = true;
        gvBudgetReport.FooterStyle.Font.Size = 12;
    }
}