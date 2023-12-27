using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_EXAMINATION_LateFees_New_Examination : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    LateFeeController lateFeeController = new LateFeeController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // sssssssssSet MasterPage
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    // Load drop down lists
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");
                    //  this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "");
                    //BindCheckList_ForDegree();
                    this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");


                    //objCommon.FillDropDownList(ddlReceiptType, "ACD_EXAM_TYPE WITH (NOLOCK)", "EXAM_TYPENO", "EXAM_TYPE", "EXAM_TYPENO>=0", "");



                    //this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "");
                    ViewState["action"] = "add";
                    chkDegree.Enabled = false;
                    SetInitialRow();
                    FillLateFeesDetails();
                    this.objCommon.FillListBox(lstSemester, "ACd_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "ShowReactivationControls();", true);
            divMsg.InnerHtml = string.Empty;

            //btnLock.Enabled = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        //foreach (GridViewRow row in gvLateFees.Rows)
        //{
        //    var textbox = row.FindControl("txtDayNoFrom") as TextBox;

        //    textbox.Text = "1";
        //}
        gvLateFees.DataSource = dt;
        gvLateFees.DataBind();
    }
    private void AddNewRowToGrid()
    {
        //int rowIndex = 0;
        //if (ViewState["CurrentTable"] != null)
        //{
        //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //    DataRow drCurrentRow = null;
        //    if (dtCurrentTable.Rows.Count > 0)
        //    {
        //        DataTable dtNewTable = new DataTable();
        //        dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        //        dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
        //        dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
        //        dtNewTable.Columns.Add(new DataColumn("Column3", typeof(string)));
        //        drCurrentRow = dtNewTable.NewRow();

        //        drCurrentRow["RowNumber"] = 1;
        //        drCurrentRow["Column1"] = string.Empty;
        //        drCurrentRow["Column2"] = string.Empty;
        //        drCurrentRow["Column3"] = string.Empty;

        //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
        //        {
        //            TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
        //            TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
        //            TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

        //            if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty && box3.Text.Trim() != string.Empty)
        //            {
        //                drCurrentRow = dtCurrentTable.NewRow();
        //                drCurrentRow["RowNumber"] = i + 1;
        //                drCurrentRow["Column1"] = box1.Text;
        //                drCurrentRow["Column2"] = box2.Text;
        //                drCurrentRow["Column3"] = box3.Text;
        //                rowIndex++;
        //                dtNewTable.Rows.Add(drCurrentRow);
        //            }
        //            else
        //            {
        //                objCommon.DisplayUserMessage(UpdatePanel1, "Late Fee Details Cannot Be Blank.", this.Page);
        //                return;
        //            }
        //        }
        //        //dtCurrentTable.Rows.Add(drCurrentRow);
        //        ViewState["CurrentTable"] = dtCurrentTable;
        //        gvLateFees.DataSource = dtCurrentTable;
        //        gvLateFees.DataBind();
        //    }
        //}
        //else
        //{
        //    Response.Write("ViewState is null");
        //}
        //SetPreviousData();



        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 5)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Column3", typeof(string)));
                drCurrentRow = dtNewTable.NewRow();

                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Column1"] = string.Empty;
                drCurrentRow["Column2"] = string.Empty;
                drCurrentRow["Column3"] = string.Empty;

                dtNewTable.Rows.Add(drCurrentRow);
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                    HiddenField hdnval = (HiddenField)gvLateFees.Rows[rowIndex].Cells[1].FindControl("hdnval");

                    TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                    TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");
                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Column1"] = box1.Text;
                        drCurrentRow["Column2"] = box2.Text;
                        drCurrentRow["Column3"] = box3.Text;
                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(pnlFeeTable, "Late Fee Details Cannot Be Blank.", this.Page);
                        return;
                    }
                }

                ViewState["CurrentTable"] = dtNewTable;
                gvLateFees.DataSource = dtNewTable;
                gvLateFees.DataBind();
            }
            else
            {
                objCommon.DisplayUserMessage(pnlFeeTable, "Maximum Options Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void BindCheckList_ForDegree(int clgID)
    {
        DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE CD INNER JOIN ACD_DEGREE D ON CD.DEGREENO=D.DEGREENO", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0 and CD.COLLEGE_ID=" + clgID + " AND D.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "DEGREENAME");
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            chkDegree.Enabled = true;
            chkDegrees.DataSource = ds;
            chkDegrees.DataTextField = ds.Tables[0].Columns["DEGREENAME"].ToString();
            chkDegrees.DataValueField = ds.Tables[0].Columns["DEGREENO"].ToString();
            chkDegrees.DataBind();
        }
        else
        {
            chkDegrees.DataSource = null;
            chkDegrees.DataBind();
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // foreach (DataRow drr in dt.Rows)
                    // {

                    TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                    TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                    TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

                    box1.Text = dt.Rows[i]["Column1"].ToString();//rowIndex
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();

                    rowIndex++;
                }
            }
            // ViewState["CurrentTable"] = dt;
        }
    }
    private void FillLateFeesDetails()
    {
        DataSet ds;
        //ds = objCommon.FillDropDown("ACD_LATE_FEE_EXAM LF INNER JOIN ACD_DEGREE D ON (LF.DEGREENO=D.DEGREENO) INNER JOIN ACD_MASTER_LATE_FEE_EXAM MLF ON(MLF.LATE_FEE_NO=LF.LATE_FEE_NO)", "DISTINCT LF.DEGREENO,DEGREENAME,LF.LATE_FEE_NO", "RECEIPT_TYPE,LAST_DATE", "LF.LATE_FEE_NO>0", string.Empty);

        ds = lateFeeController.GET__LATE_FEES_DETAILS_EXAM(Convert.ToInt32(ddlSession.SelectedValue));
        if (ds.Tables.Count > 0 && ds.Tables != null)
        {
            lvLateFeesDEtails.DataSource = ds;
            lvLateFeesDEtails.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvLateFeesDEtails);//Set label 
            //   pnllst.Visible = truessssssssss;
            ddlReceiptType.Focus();
        }
        else
        {
            lvLateFeesDEtails.DataSource = null;
            lvLateFeesDEtails.DataBind();
        }
    }
    private void ShowDetails()
    {
        string semester = String.Empty;
        try
        {
            chkDegrees.SelectedIndex = -1;
            int a = Convert.ToInt32(ViewState["Late_Fee_NO"]);
            DataSet ds = lateFeeController.GET__LATE_FEES_DETAILS_FOR_EDIT_EXAM(Convert.ToInt32(ViewState["Late_Fee_NO"]));


            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtToDate.Text = ds.Tables[0].Rows[0]["LAST_DATE"] == null ? string.Empty : ds.Tables[0].Rows[0]["LAST_DATE"].ToString();
                    string sem = ds.Tables[0].Rows[0]["SEMESTERNOS"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNOS"].ToString();
                    // chkDegrees.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == null ? "0" : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            for (int i = 0; i < chkDegrees.Items.Count; i++)
                            {
                                if (chkDegrees.Items[i].Value == ds.Tables[0].Rows[j]["DEGREENO"].ToString())
                                {
                                    chkDegrees.Items[i].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                    string[] semno = sem.Split(',');
                    for (int i = 0; i < semno.Length; i++)
                    {
                        for (int j = 0; j < lstSemester.Items.Count; j++)
                        {
                            if (semno[i] == lstSemester.Items[j].Value)
                            {
                                lstSemester.Items[j].Selected = true;
                            }
                        }
                    }
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    //    {
                    //        for (int i = 0; i < lstSemester.Items.Count; i++)
                    //        {
                    //            string x = ds.Tables[0].Rows[j]["SEMESTERNOS"].ToString();
                    //            string y = lstSemester.Items[i].Value;
                    //               if (lstSemester.Items[i].Value == ds.Tables[0].Rows[j]["SEMESTERNOS"].ToString())
                    //         {
                    //                lstSemester.Items[i].Selected = true;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}



                    ddlReceiptType.SelectedValue = ds.Tables[0].Rows[0]["RECEIPT_TYPE"] == null ? "0" : ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString();
                    ddlReceiptType_SelectedIndexChanged(new object(), new EventArgs());
                    ddlFeeItems.SelectedValue = (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FEE_HEAD"].ToString()) || ds.Tables[0].Rows[0]["FEE_HEAD"] == null) ? "0" : ds.Tables[0].Rows[0]["FEE_HEAD"].ToString();

                    if (ds.Tables[0].Rows[0]["READMISSION_FLAG"] == null || Convert.ToInt32(ds.Tables[0].Rows[0]["READMISSION_FLAG"]) == 0)
                    {
                        chkReactivationFee.Checked = false;
                        txtReactivationfees.Text = string.Empty;
                    }
                    else
                    {
                        chkReactivationFee.Checked = true;
                        txtReactivationfees.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["READMISSION_FEE"].ToString()) ? ds.Tables[0].Rows[0]["READMISSION_FEE"].ToString() : string.Empty;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "ShowReactivationControls();", true);

                    if (ds.Tables[0].Rows[0]["FIXED_AMT_FLAG"] == null || Convert.ToInt32(ds.Tables[0].Rows[0]["FIXED_AMT_FLAG"]) == 0)
                        chkFixedAmtFlag.Checked = false;
                    else
                        chkFixedAmtFlag.Checked = true;

                    EnableControls();
                    DataTable dtCurrentTable = new DataTable();
                    DataRow drCurrentRow = null;
                    dtCurrentTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column3", typeof(string)));
                    #region semester
                    //semester = ds["SEMESTER"].ToString();
                    //string[] sem = semester.Split(delimiterChars);
                    //count = 0;
                    //vcount = 0;
                    //for (int j = 0; j < sem.Length; j++)
                    //{
                    //    for (int i = 0; i < chkSemesterList.Items.Count; i++)
                    //    {
                    //        if (sem[j] == chkSemesterList.Items[i].Value)
                    //        {
                    //            chkSemesterList.Items[i].Selected = true;
                    //        }
                    //    }
                    //}
                    //foreach (ListItem Item in chkSemesterList.Items)
                    //{
                    //    vcount++;
                    //    if (Item.Selected == true)
                    //    {
                    //        count++;
                    //    }
                    //}
                    //if (count == vcount)
                    //{
                    //    chkSemester.Checked = true;
                    //}
                    #endregion

                   // DataSet dst = objCommon.FillDropDown("ACD_MASTER_LATE_FEE_EXAM ML WITH (NOLOCK) INNER JOIN ACD_LATE_FEE_EXAM LF WITH (NOLOCK) ON(ML.LATE_FEE_NO=LF.LATE_FEE_NO)", "DISTINCT DAY_NO_FROM", "DAY_NO_TO,AMOUNT", "COMMON_NO=" + Convert.ToInt32(ViewState["Late_Fee_NO"]), string.Empty);
                    DataSet dst = objCommon.FillDropDown("ACD_MASTER_LATE_FEE_EXAM ML WITH (NOLOCK) INNER JOIN ACD_LATE_FEE_EXAM LF WITH (NOLOCK) ON(ML.LATE_FEE_NO=LF.LATE_FEE_NO)", "DISTINCT DAY_NO_FROM", "DAY_NO_TO,AMOUNT", " ML.LATE_FEE_NO=" + Convert.ToInt32(ViewState["Late_Fee_NO"]), string.Empty);


                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;//Convert.ToInt32(dst.Tables[0].Rows[i]["ROWNUMBER"].ToString());
                        drCurrentRow["Column1"] = dst.Tables[0].Rows[i]["DAY_NO_FROM"];
                        drCurrentRow["Column2"] = dst.Tables[0].Rows[i]["DAY_NO_TO"];
                        drCurrentRow["Column3"] = dst.Tables[0].Rows[i]["AMOUNT"];
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;
                    gvLateFees.DataSource = dtCurrentTable;
                    gvLateFees.DataBind();
                    BindDataonEdit();
                }
            }
            if (ds != null) ds.Dispose();

            //Set Previous Data on Postbacks
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindDataonEdit()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                    TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                    TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();

                    rowIndex++;
                }
            }
            //ViewState["CurrentTable"] = dt;
        }
    }
    private void ClearDetails()
    {
        //ddlSession.SelectedIndex = -1; 
        ddlReceiptType.SelectedIndex = -1; ddlFeeItems.SelectedIndex = -1;
        txtToDate.Text = string.Empty;
        chkDegrees.SelectedIndex = -1;
        chkReactivationFee.Checked = false;
        chkFixedAmtFlag.Checked = false;
        txtReactivationfees.Text = string.Empty;
        //  ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "ShowReactivationControls();", true);
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLateFeesDetails();
    }
    protected void gvLateFees_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemove");
            if (lb != null)
            {
                //if (dt.Rows.Count > 1)
                //{
                //    if (e.Row.RowIndex == dt.Rows.Count - 1)
                //    {
                //        lb.Visible = false;
                //    }
                //}
                //else
                //{
                //    lb.Visible = false;
                //}
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    //protected void btnChargeLateFee_Click(object sender, EventArgs e)
    //{

    //}
    //protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //     //AND FEE_LONGNAME <> '' AND FEE_LONGNAME IS NOT NULL 
    //    try
    //    {
    //        if (ddlReceiptType.SelectedIndex > 0)
    //        {
    //            this.objCommon.FillDropDownList(ddlFeeItems, "ACD_FEE_TITLE", "FEE_HEAD", "FEE_LONGNAME", "RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "'AND FEE_LONGNAME <> '' AND FEE_LONGNAME IS NOT NULL ", "SRNO");
    //            ddlFeeItems.Enabled = true;
    //        }
    //        else
    //        {
    //            ddlFeeItems.SelectedIndex = 0;
    //            ddlFeeItems.Enabled = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Late_Fee_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["Late_Fee_NO"] = Late_Fee_NO;
            ViewState["action"] = "edit";
            this.ShowDetails();
            //btnSubmit.Text = "Edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void EnableControls()
    {
        //chkDegrees.Enabled = false;
        //ddlReceiptType.Enabled = false;
        ddlFeeItems.Enabled = false;
        //txtToDate.Enabled = false;
    }
    private void ClearControl()
    {
        ViewState["action"] = "add";
        SetInitialRow();
    }
    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex+1;

        int rowID = Convert.ToInt32(gvRow.RowIndex);
        //if (ViewState["CurrentTable"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["CurrentTable"];
        //    if (dt.Rows.Count > 1)
        //    {
        //        if (gvRow.RowIndex < dt.Rows.Count - 1)
        //        {
        //            dt.Rows.Remove(dt.Rows[rowID]);
        //        }
        //    }

        //    ViewState["CurrentTable"] = dt;
        //    gvLateFees.DataSource = dt;
        //    gvLateFees.DataBind();
        //}


        DataTable dt = ViewState["CurrentTable"] as DataTable;
        dt.Rows[rowID].Delete();
        ViewState["CurrentTable"] = dt;
        gvLateFees.DataSource = dt;
        gvLateFees.DataBind();

        SetPreviousData();
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int Late_Fee_NO = int.Parse(btnDelete.CommandArgument);

        int del = lateFeeController.Delete_LateFeesDetails_EXAM(Late_Fee_NO,Convert.ToInt32(Session["userno"]));
        if (del > 0) 
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Late Fees Details Deleted!", this.Page);
            ClearDetails();
        }
        else
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Failed To Delete Late Fees Details!", this.Page);
        }
        FillLateFeesDetails();
        ClearControl();
    }
    protected void gvLateFees_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //lnkRemove_Click(new object(), new EventArgs());
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = ViewState["CurrentTable"] as DataTable;
        dt.Rows[index].Delete();
        ViewState["CurrentTable"] = dt;
        gvLateFees.DataSource = dt;
        gvLateFees.DataBind();
        SetPreviousData();

    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////if (ddlReceiptType.SelectedIndex >= 0)
            ////{
            ////    //this.objCommon.FillDropDownList(ddlFeeItems, "ACD_FEE_TITLE WITH (NOLOCK)", "FEE_HEAD", "FEE_LONGNAME", "RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "'AND FEE_LONGNAME <> '' AND FEE_LONGNAME IS NOT NULL ", "SRNO");
            ////    //ddlFeeItems.Enabled = true;
            ////  //  ddlFeeItems.Focus();
            ////}
            ////else
            ////{
            ////    ddlFeeItems.SelectedIndex = 0;
            ////    ddlFeeItems.Enabled = false;
            ////}
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void btnChargeLateFee_Click(object sender, EventArgs e)
    //{

    //}
    protected void btnChargeLateFee_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Please Select School/Institute.", this.Page);

            return;
        }

        if (ddlSession.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Please select Session.", this.Page);

            return;
        }
        else if (ddlReceiptType.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Please select ReceiptType.", this.Page);

            return;
        }
        //else if (ddlFeeItems.SelectedValue == "0")
        //{
        //    objCommon.DisplayUserMessage(pnlFeeTable, "Please select Fee Item.", this.Page);

        //    return;
        //}
        string DAYfrom = string.Empty; string DAYto = string.Empty; string TOTfees = string.Empty; string hdnvalue = string.Empty;

        string DayFrom = string.Empty; string DayTo = string.Empty; string TotFees = string.Empty; string hdnID = string.Empty;

        string DegreeId = string.Empty;
        string semesternos = string.Empty;

        foreach (ListItem item in chkDegrees.Items)
        {
            if (item.Selected == true)
                DegreeId = DegreeId + item.Value.ToString() + ",";
        }

        if (DegreeId != "")
            DegreeId = DegreeId.TrimEnd(',');
        else
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Please select at least one Degree.", this.Page);
            //objCommon.DisplayMessage("Please select at least one Degree.", this.Page);
            return;
        }
        foreach (ListItem item in lstSemester.Items)
        {
            if (item.Selected == true)
                semesternos = semesternos + item.Value.ToString() + ",";
        }

        if (semesternos != "")
            semesternos = semesternos.TrimEnd(',');
        else
        {
            objCommon.DisplayUserMessage(pnlFeeTable, "Please select at least one semester.", this.Page);
            //objCommon.DisplayMessage("Please select at least one Degree.", this.Page);
            return;
        }


        int FixedAmtFlag = chkFixedAmtFlag.Checked ? 1 : 0;
        int ReAdmissionFlag = chkReactivationFee.Checked ? 1 : 0;
        double ReAdmAmt = (chkReactivationFee.Checked && !string.IsNullOrEmpty(txtReactivationfees.Text)) ? Convert.ToDouble(txtReactivationfees.Text.Trim()) : 0;

        if (ViewState["Late_Fee_NO"] == null)
        {
            #region SAVE LATE FEES


            //if (DegreeId != string.Empty)
            //{
            int result = lateFeeController.Insert_New_Late_Fees_Details_EXAM(DegreeId, semesternos, ddlReceiptType.SelectedValue, Convert.ToDateTime(txtToDate.Text), "",
                                Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["college_id"]), ReAdmissionFlag, ReAdmAmt);  //, txtLateFeeAmount.Text
            if (result > 0)
            {
                if (DegreeId != string.Empty)
                {
                    string strValue = DegreeId;
                    string[] strArray = strValue.Split(',');
                    foreach (object obj in strArray)
                    {
                        int Late_Fees_NO = Convert.ToInt32(objCommon.LookUp("ACD_LATE_FEE_EXAM WITH (NOLOCK)", "LATE_FEE_NO", "DEGREENO=" + obj + " AND RECEIPT_TYPE='" + ddlReceiptType.SelectedValue + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                        int rowIndex = 0;
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                            if (dtCurrentTable.Rows.Count > 0)
                            {
                                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                                {
                                    TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                                    HiddenField hdnval = (HiddenField)gvLateFees.Rows[rowIndex].Cells[1].FindControl("hdnval");

                                    TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                                    TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

                                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty && box3.Text.Trim() != string.Empty)
                                    {
                                        DAYfrom += box1.Text.Trim() + ",";
                                        hdnvalue += hdnval.Value.Trim() + ",";
                                        DAYto += box2.Text.Trim() + ",";
                                        TOTfees += box3.Text.Trim() + ",";
                                        rowIndex++;
                                    }
                                    else
                                    {
                                        objCommon.DisplayUserMessage(pnlFeeTable, "Late Fee Details Cannot Be Blank.", this.Page);
                                        return;
                                    }
                                }
                            }
                        }
                        DayFrom = DAYfrom.TrimEnd(',');
                        DayTo = DAYto.TrimEnd(',');
                        TotFees = TOTfees.TrimEnd(',');
                        hdnID = hdnvalue.TrimEnd(',');

                        //Add New
                        CustomStatus cs = (CustomStatus)lateFeeController.Add_LateFees_Master_Details_EXAM(Late_Fees_NO, DayFrom, DayTo, TotFees, FixedAmtFlag);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayUserMessage(pnlFeeTable, "Late Fees Details Save Successfully!", this.Page);
                            DayFrom = DayTo = TotFees = hdnID = string.Empty;
                            DAYfrom = DAYto = TOTfees = hdnvalue = string.Empty;
                        }
                        else if (cs.Equals(CustomStatus.TransactionFailed))
                        {
                            objCommon.DisplayUserMessage(pnlFeeTable, "Transaction Failed", this.Page);
                        }
                    }
                }
                else
                {   //objCommon.DisplayUserMessage(pnlFeeTable, "Failed To Save Late Fees Details!", this.Page);
                    //return;

                    int Late_Fees_NO = Convert.ToInt32(objCommon.LookUp("ACD_LATE_FEE_EXAM WITH (NOLOCK)", "LATE_FEE_NO", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND RECEIPT_TYPE='" + ddlReceiptType.SelectedValue + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                    int rowIndex = 0;
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                            {
                                TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                                HiddenField hdnval = (HiddenField)gvLateFees.Rows[rowIndex].Cells[1].FindControl("hdnval");

                                TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                                TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

                                if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty && box3.Text.Trim() != string.Empty)
                                {
                                    DAYfrom += box1.Text.Trim() + ",";
                                    hdnvalue += hdnval.Value.Trim() + ",";
                                    DAYto += box2.Text.Trim() + ",";
                                    TOTfees += box3.Text.Trim() + ",";
                                    rowIndex++;
                                }
                                else
                                {
                                    objCommon.DisplayUserMessage(pnlFeeTable, "Late Fee Details Cannot Be Blank.", this.Page);
                                    return;
                                }
                            }
                        }
                    }
                    DayFrom = DAYfrom.TrimEnd(',');
                    DayTo = DAYto.TrimEnd(',');
                    TotFees = TOTfees.TrimEnd(',');
                    hdnID = hdnvalue.TrimEnd(',');

                    //Add New
                    //CustomStatus cs = (CustomStatus)lateFeeController.Add_LateFees_Master_Details(Late_Fees_NO, DayFrom, DayTo, TotFees, FixedAmtFlag);
                    CustomStatus cs = (CustomStatus)lateFeeController.Add_LateFees_Master_Details_EXAM(Late_Fees_NO, DayFrom, DayTo, TotFees, FixedAmtFlag);
                   
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(pnlFeeTable, "Late Fees Details Save Successfully!", this.Page);
                        DayFrom = DayTo = TotFees = hdnID = string.Empty;
                        DAYfrom = DAYto = TOTfees = hdnvalue = string.Empty;
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayUserMessage(pnlFeeTable, "Transaction Failed", this.Page);
                    }
                }
            }

            //else
            //{
            //    objCommon.DisplayUserMessage(pnlFeeTable, "Please Select Atleast one Degree To Charge Late Fee!", this.Page);
            //    return;
            //}
            #endregion
        }
        else
        {
            #region UPDATE LATE FEES

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Edit 
                CustomStatus cs = (CustomStatus)lateFeeController.UpdateLate_New_FeesDetails_EXAM(Convert.ToDateTime(txtToDate.Text), DegreeId,
                    Convert.ToInt32(ViewState["Late_Fee_NO"]), ddlFeeItems.SelectedValue, ddlReceiptType.SelectedValue,
                    Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["college_id"]), ReAdmissionFlag, ReAdmAmt,semesternos);//Late_Fees_NO, hdnID, s,DayFrom, DayTo, TotFees,
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    if (!string.IsNullOrEmpty(DegreeId))
                    {
                        string strValue = DegreeId;
                        string[] strArray = strValue.Split(',');

                        foreach (object obj in strArray)
                        {
                            int Late_Fees_NO = Convert.ToInt32(objCommon.LookUp("ACD_LATE_FEE_EXAM WITH (NOLOCK)", "LATE_FEE_NO", "DEGREENO=" + obj + " AND RECEIPT_TYPE='" + ddlReceiptType.SelectedValue + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                            int rowIndex = 0;
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                                    {
                                        TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                                        HiddenField hdnval = (HiddenField)gvLateFees.Rows[rowIndex].Cells[1].FindControl("hdnval");

                                        TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                                        TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

                                        if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty && box3.Text.Trim() != string.Empty)
                                        {
                                            DAYfrom += box1.Text.Trim() + ",";
                                            hdnvalue += hdnval.Value.Trim() + ",";
                                            DAYto += box2.Text.Trim() + ",";
                                            TOTfees += box3.Text.Trim() + ",";
                                            rowIndex++;
                                        }
                                        else
                                        {
                                            objCommon.DisplayUserMessage(pnlFeeTable, "Late Fee Details Cannot Be Blank.", this.Page);
                                            return;
                                        }
                                    }
                                }
                            }
                            DayFrom = DAYfrom.TrimEnd(',');
                            DayTo = DAYto.TrimEnd(',');
                            TotFees = TOTfees.TrimEnd(',');
                            hdnID = hdnvalue.TrimEnd(',');

                            CustomStatus cs1 = (CustomStatus)lateFeeController.Add_LateFees_Master_Details_EXAM(Late_Fees_NO, DayFrom, DayTo, TotFees, FixedAmtFlag);
                            if (cs1.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayUserMessage(pnlFeeTable, "Late Fees Details Updated sucessfully!", this.Page);
                                DayFrom = DayTo = TotFees = hdnID = string.Empty;
                                DAYfrom = DAYto = TOTfees = hdnvalue = string.Empty;
                            }
                            else if (cs1.Equals(CustomStatus.TransactionFailed))
                            {
                                objCommon.DisplayUserMessage(pnlFeeTable, "Transaction Failed", this.Page);
                            }
                        }
                    }
                    else
                    {
                        int Late_Fees_NO = Convert.ToInt32(objCommon.LookUp("ACD_LATE_FEE_EXAM WITH (NOLOCK)", "LATE_FEE_NO", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND RECEIPT_TYPE='" + ddlReceiptType.SelectedValue + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
                        int rowIndex = 0;
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                            if (dtCurrentTable.Rows.Count > 0)
                            {
                                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                                {
                                    TextBox box1 = (TextBox)gvLateFees.Rows[rowIndex].Cells[1].FindControl("txtDayNoFrom");
                                    HiddenField hdnval = (HiddenField)gvLateFees.Rows[rowIndex].Cells[1].FindControl("hdnval");

                                    TextBox box2 = (TextBox)gvLateFees.Rows[rowIndex].Cells[2].FindControl("txtDayNoTo");
                                    TextBox box3 = (TextBox)gvLateFees.Rows[rowIndex].Cells[3].FindControl("txtFees");

                                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty && box3.Text.Trim() != string.Empty)
                                    {
                                        DAYfrom += box1.Text.Trim() + ",";
                                        hdnvalue += hdnval.Value.Trim() + ",";
                                        DAYto += box2.Text.Trim() + ",";
                                        TOTfees += box3.Text.Trim() + ",";
                                        rowIndex++;
                                    }
                                    else
                                    {
                                        objCommon.DisplayUserMessage(pnlFeeTable, "Late Fee Details Cannot Be Blank.", this.Page);
                                        return;
                                    }
                                }
                            }
                        }
                        DayFrom = DAYfrom.TrimEnd(',');
                        DayTo = DAYto.TrimEnd(',');
                        TotFees = TOTfees.TrimEnd(',');
                        hdnID = hdnvalue.TrimEnd(',');

                        CustomStatus cs1 = (CustomStatus)lateFeeController.Add_LateFees_Master_Details_EXAM(Late_Fees_NO, DayFrom, DayTo, TotFees, FixedAmtFlag);
                        if (cs1.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayUserMessage(pnlFeeTable, "Late Fees Details Updated sucessfully!", this.Page);
                            DayFrom = DayTo = TotFees = hdnID = string.Empty;
                            DAYfrom = DAYto = TOTfees = hdnvalue = string.Empty;
                        }
                        else if (cs1.Equals(CustomStatus.TransactionFailed))
                        {
                            objCommon.DisplayUserMessage(pnlFeeTable, "Transaction Failed", this.Page);
                        }
                    }
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayUserMessage(pnlFeeTable, "Transaction Failed", this.Page);
                }
            }
            #endregion
        }

        FillLateFeesDetails();
        ClearControl();
        ClearDetails();
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                // FILL DROPDOWN  ddlSession_SelectedIndexChanged
            }





            // BindCheckList_ForDegree(Convert.ToInt32(ddlCollege.SelectedValue));
            BindCheckList_ForDegree(Convert.ToInt32(ViewState["college_id"]));
            // this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            ddlSession.Focus();
            ClearDetails();
        }
        else
        {
            chkDegrees.Items.Clear();
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            chkDegree.Enabled = false;
            chkDegree.Checked = false;
        }
    }
    protected void chkDegrees_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lstSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        // txtValuationFee.Text = "0";
        // txtValuationMaxFee.Text = "0";
        //
        // pnlCopySession.Visible = false;
        // btnCopyData.Visible = false;
        //
        // ddlCsession.SelectedIndex = 0;
        //
        //  ScriptManager.RegisterClientScriptBlock(pnlFeeTable, pnlFeeTable.GetType(), "Src", "ShowDropDown();", true);
    }
}