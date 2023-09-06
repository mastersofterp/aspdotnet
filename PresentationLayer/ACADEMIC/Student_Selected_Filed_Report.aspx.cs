//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Student_Selected_Report.aspx
// CREATION DATE :                                                     
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE : 26-12-2015
// MODIFIED DESC : By Mohit Maske
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class Student_Selected_Filed_Report : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    StudentSelectFieldController objStudContrl = new StudentSelectFieldController();

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
            Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
            }
            this.FillSelectedFieldTable();
            pnlgrdSelectFieldReport.Visible = false;
            pnlFilters.Visible = false;
            imgbutExporttoexcel.Visible = false;
            imgbutExporttoWord.Visible = false;
            imgbutBack.Visible = false;
        }

    }
    // THIS ADDED FOR EXPORT OT EXCEL AND WORD
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    private void BindListViewReport(string TableNames, string Fields, string WhereCondition, string orderBy)
    {
        try
        {
            DataSet ds = objStudContrl.EmployeeSelectedFieldReport(TableNames, Fields, WhereCondition, orderBy);
            grdSelectFieldReport.DataSource = ds;
            grdSelectFieldReport.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillSelectedFieldTable()
    {
        try
        {
            objCommon.FillListBox(lstFieldsToSelect, "SELECTED_FIELD_TABLE", "SFTRXNO", "DISPLAY_FIELD_NAME", "FILTER='ACAD'", "SFTRXNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.FillSelectedFieldTable()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void imgNextFieldsToSelect_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < lstFieldsToSelect.Items.Count; i++)
        {
            if (lstFieldsToSelect.Items[i].Selected)
            {
                int Count_FieldsToSelect = 0;
                for (int j = 0; j < lstSelectedFields.Items.Count; j++)
                {
                    if (lstSelectedFields.Items[j].Value == lstFieldsToSelect.Items[i].Value)
                    {
                        Count_FieldsToSelect = 1;
                    }
                }
                if (Count_FieldsToSelect != 1)
                    objCommon.FillListBoxWithOutClear(lstSelectedFields, "SELECTED_FIELD_TABLE", "SFTRXNO", "DISPLAY_FIELD_NAME", "SFTRXNO=" + Convert.ToInt32(lstFieldsToSelect.Items[i].Value), "SFTRXNO");
                else
                    objCommon.DisplayMessage(updpnlMain, lstFieldsToSelect.Items[i].Text + " Already Exists", this);
            }
        }
    }

    protected void imgPrevFieldsToSelect_Click(object sender, ImageClickEventArgs e)
    {

        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (lstSelectedFields.Items[i].Selected)
            {
                lstSelectedFields.Items.RemoveAt(i);
            }

        }

    }

    protected void imgNextSelectedFields_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (lstSelectedFields.Items[i].Selected)
            {
                //lstOrderBy.Items.Add(lstSelectedFields.Items[i].Text);

                int Count_FieldsToSelect = 0;
                for (int j = 0; j < lstOrderBy.Items.Count; j++)
                {
                    if (lstOrderBy.Items[j].Value == lstSelectedFields.Items[i].Value)
                    {
                        Count_FieldsToSelect = 1;
                    }
                }

                if (Count_FieldsToSelect != 1)
                    objCommon.FillListBoxWithOutClear(lstOrderBy, "SELECTED_FIELD_TABLE", "SFTRXNO", "DISPLAY_FIELD_NAME", "SFTRXNO=" + Convert.ToInt32(lstSelectedFields.Items[i].Value), "SFTRXNO");
                else
                    objCommon.DisplayMessage(updpnlMain, lstSelectedFields.Items[i].Text + " Already Exists", this);
            }
        }
    }

    protected void imgPrevSelectedFields_Click(object sender, ImageClickEventArgs e)
    {


        for (int i = 0; i < lstOrderBy.Items.Count; i++)
        {
            if (lstOrderBy.Items[i].Selected)
            {

                lstOrderBy.Items.RemoveAt(i);

            }

        }


    }

    protected void imgUpOrderBy_Click(object sender, ImageClickEventArgs e)
    {
        lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex + 1, lstOrderBy.Items[lstOrderBy.SelectedIndex - 1]);
        lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex - 1);
    }

    protected void imgDownOrderBy_Click(object sender, ImageClickEventArgs e)
    {
        lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex, lstOrderBy.Items[lstOrderBy.SelectedIndex + 1]);
        lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex + 1);
    }

    protected void butCondition_Click(object sender, EventArgs e)
    {
        string SFTRXNO = string.Empty;
        pnlFilters.Visible = true;

        imgbutBack.Visible = true;

        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
            {
                DataSet ds = null;
                ds = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));

                if (ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString() != "N")
                {
                    if (ds.Tables[0].Rows[0]["COLUMNIDNO"].ToString() != "N" && ds.Tables[0].Rows[0]["COLUMNNAME"].ToString() != "N")
                    {
                        if (SFTRXNO.Length == 0)
                            SFTRXNO += ds.Tables[0].Rows[0]["SFTRXNO"].ToString();
                        else
                            SFTRXNO += "," + ds.Tables[0].Rows[0]["SFTRXNO"].ToString();
                    }
                }
            }
        }
        if (Convert.ToInt32(lstSelectedFields.Items.Count) > 0)
        {
            if (SFTRXNO.Length > 0)
                this.BindGridViewFilter(SFTRXNO);
            else
                objCommon.DisplayMessage(updpnlMain, "No Filters", this);
        }
        else
        {
            objCommon.DisplayMessage(updpnlMain, "Please Select Selected Fields", this);
        }
    }

    protected void butShowReport_Click(object sender, EventArgs e)
    {
        //tblButtons.Visible = true;
        string TableNames = string.Empty;
        string Fields = string.Empty;
        string WhereCondition = string.Empty;
        string orderBy = string.Empty;
        pnlgrdSelectFieldReport.Visible = true;
        pnlSelectionCriteria.Visible = false;

        TableNames = this.TableNames();
        Fields = this.Fields();
        WhereCondition = this.WhereCondition();
        orderBy = this.orderBy();
        this.BindListViewReport(TableNames, Fields, WhereCondition, orderBy);

        imgbutExporttoexcel.Visible = true;
        imgbutExporttoWord.Visible = true;
        imgbutBack.Visible = true;
    }

    private string TableNames()
    {
        string TableNames = string.Empty;
        DataSet ds;
        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
            {
                ds = null;
                ds = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));

                if (ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString() != "N")
                {
                    if (TableNames.Length == 0)
                        TableNames += ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString();
                    else
                        TableNames += "," + ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString();
                }
            }
        }
        String str = TableNames;
        String[] arr = str.Split(',');
        String penultimate = "";
        String ultimate = "";
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != ultimate)
            {
                penultimate = ultimate;
                //ultimate += arr[i];
                if (ultimate.Length == 0)
                    ultimate += arr[i];
                else
                    ultimate += "," + arr[i];
            }
        }
        return ultimate;
    }

    private string Fields()
    {
        string Fields = string.Empty;
        DataSet ds;
        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
            {
                ds = null;
                ds = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));

                if (ds.Tables[0].Rows[0]["FIELD_ADDED_ON_SQL"].ToString() != "N")
                {
                    if (Fields.Length == 0)
                        Fields += ds.Tables[0].Rows[0]["FIELD_ADDED_ON_SQL"].ToString() + ' ' + ds.Tables[0].Rows[0]["DISPLAY_FIELD_NAME"].ToString();
                    else
                        Fields += "," + ds.Tables[0].Rows[0]["FIELD_ADDED_ON_SQL"].ToString() + ' ' + ds.Tables[0].Rows[0]["DISPLAY_FIELD_NAME"].ToString();
                }
            }
        }
        return Fields;
    }

    private string WhereCondition()
    {
        string WhereCondition = string.Empty;
        string JoiningCondition = string.Empty;

        DataSet ds;

        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
            {
                ds = null;
                ds = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));
                if (ds.Tables[0].Rows[0]["joining"].ToString() != "N")
                {
                    if (ds.Tables[0].Rows[0]["joining"].ToString() != "N")
                    {
                        if (JoiningCondition.Length == 0)
                            JoiningCondition += ds.Tables[0].Rows[0]["joining"].ToString();
                        else
                            JoiningCondition += " and " + ds.Tables[0].Rows[0]["joining"].ToString();
                    }
                }
            }
        }
        if (grdvFilters.Rows.Count > 0)
        {
            for (int j = 0; j < grdvFilters.Rows.Count; j++)
            {
                DropDownList ddlFilter = (DropDownList)grdvFilters.Rows[j].FindControl("ddlFilters");
                TextBox txtFilter = (TextBox)grdvFilters.Rows[j].FindControl("txtFilter");
                DataSet ds1 = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(txtFilter.Text));

                if (ds1.Tables[0].Rows[0]["where_condition"].ToString() != "N")
                {
                    if (Convert.ToInt32(ddlFilter.SelectedValue) > 0)
                    {
                        if (WhereCondition.Length == 0)
                            WhereCondition += ds1.Tables[0].Rows[0]["where_condition"].ToString() + "=" + Convert.ToInt32(ddlFilter.SelectedValue);
                        else
                            WhereCondition += " and " + ds1.Tables[0].Rows[0]["where_condition"].ToString() + "=" + Convert.ToInt32(ddlFilter.SelectedValue);
                    }
                }
            }
        }
        if (WhereCondition.Length == 0)
            WhereCondition = JoiningCondition;
        else
            WhereCondition = JoiningCondition + " and " + WhereCondition;
        return WhereCondition;
    }

    private string orderBy()
    {
        string orderBy = string.Empty;

        DataSet ds;

        if (lstOrderBy.Items.Count > 0)
        {
            for (int i = 0; i < lstOrderBy.Items.Count; i++)
            {
                if (Convert.ToInt32(lstOrderBy.Items[i].Value) > 0)
                {
                    ds = null;
                    ds = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstOrderBy.Items[i].Value));


                    if (ds.Tables[0].Rows[0]["ORDERBY"].ToString() != "N")
                    {

                        if (orderBy.Length == 0)
                            orderBy += ds.Tables[0].Rows[0]["ORDERBY"].ToString();
                        else
                            orderBy += "," + ds.Tables[0].Rows[0]["ORDERBY"].ToString();
                    }

                }

            }
        }
        else
        {
            orderBy = null;
        }

        return orderBy;
    }

    private void BindGridViewFilter(string SFTRXNO)
    {
        DataSet ds = objStudContrl.GetSelectedSftrxno(SFTRXNO);
        grdvFilters.DataSource = ds;
        grdvFilters.DataBind();

        for (int i = 0; i < grdvFilters.Rows.Count; i++)
        {
            DropDownList ddlFilter = (DropDownList)grdvFilters.Rows[i].FindControl("ddlFilters");
            TextBox txtFilter = (TextBox)grdvFilters.Rows[i].FindControl("txtFilter");
            DataSet ds1 = objStudContrl.GetSingleSelectedFieldTable(Convert.ToInt32(txtFilter.Text));
            string COLUMNIDNO = ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString().Replace("DISTINCT","");//THIS CONDITION IS FOR WHERE CONDITION & ORDER BY
            //objCommon.FillDropDownList(ddlFilter, ds1.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString(), ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString(), ds1.Tables[0].Rows[0]["COLUMNNAME"].ToString(), ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString() + ">0", ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString());
            objCommon.FillDropDownList(ddlFilter, ds1.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString(), ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString(), ds1.Tables[0].Rows[0]["COLUMNNAME"].ToString(), COLUMNIDNO + ">0", COLUMNIDNO);

        }
    }

    private void Export(string type)
    {
        string filename = string.Empty;
        string ContentType = string.Empty;

        if (type == "Excel")
        {
            filename = "SelectedFieldReport.xls";
            ContentType = "ms-excel";
        }
        else if (type == "Word")
        {
            filename = "SelectedFieldReport.doc";
            ContentType = "vnd.word";
        }
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + ContentType;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grdSelectFieldReport.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void imgbutExporttoexcel_Click(object sender, ImageClickEventArgs e)
    {
        this.Export("Excel");
    }

    protected void imgbutExporttoWord_Click(object sender, ImageClickEventArgs e)
    {
        this.Export("Word");
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        pnlgrdSelectFieldReport.Visible = false;
        pnlSelectionCriteria.Visible = true;
        pnlFilters.Visible = false;
        imgbutExporttoexcel.Visible = false;
        imgbutExporttoWord.Visible = false;
        imgbutBack.Visible = false;
        Response.Redirect("~/ACADEMIC/Student_Selected_Filed_Report.aspx");
    }
}
