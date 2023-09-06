//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ServiceBook_Report.aspx
// CREATION DATE : 29-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
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


public partial class PAYROLL_REPORT_Pay_Selected_Filed_Report : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objpayContrl = new PayController(); 
   
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
                CheckPageAuthorization();
            }

           // this.BindListSelectedFieldTable();

            this.FillSelectedFieldTable();

            //grdSelectFieldReport.Visible = false;

            pnlgrdSelectFieldReport.Visible = false;
            pnlFilters.Visible = false;
           // pnlExportButtons.Visible = false;
         
        }
        
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_Selected_Filed_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Selected_Filed_Report.aspx");
        }
    }
       
    private void BindListViewReport(string TableNames, string Fields, string WhereCondition, string orderBy)
    {
        try
        {
            DataSet ds = objpayContrl.EmployeeSelectedFieldReport(TableNames, Fields, WhereCondition, orderBy);
            grdSelectFieldReport.DataSource = ds;
            grdSelectFieldReport.DataBind();

            //grdSelectFieldReport.Visible = true;
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
            objCommon.FillListBox(lstFieldsToSelect, "SELECTED_FIELD_TABLE", "SFTRXNO", "DISPLAY_FIELD_NAME", "FILTER='PAY'", "SFTRXNO");
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
                    objCommon.DisplayMessage(updpnlMain, lstFieldsToSelect.Items[i].Text + " Already Exists",this);
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
                    objCommon.FillListBoxWithOutClear(lstOrderBy,"SELECTED_FIELD_TABLE", "SFTRXNO", "DISPLAY_FIELD_NAME", "SFTRXNO=" + Convert.ToInt32(lstSelectedFields.Items[i].Value), "SFTRXNO"); 
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
        try
        {
            if (lstOrderBy.SelectedIndex > 0)
            {
                lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex + 1, lstOrderBy.Items[lstOrderBy.SelectedIndex - 1]);
                lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex - 1);
            }
            else
            {
                // MessageBox.Show("Already have it");
               // Response.Write("Item Does not exist");

            }

        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {

        }
        

    }
    protected void imgDownOrderBy_Click(object sender, ImageClickEventArgs e)
    {
        //  if (!lstOrderBy.Items.Contains("ABC".ToLower()))
        ////  if (lstOrderBy.SelectedIndex > 0)
        //  {
        //      lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex, lstOrderBy.Items[lstOrderBy.SelectedIndex + 1]);
        //      lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex + 1);
        //  }
        try
        {
            int Count_FieldsToSelect = 0;
            for (int i = 1; i < lstOrderBy.Items.Count; i++)
            {
                Count_FieldsToSelect++;

            }

            if (lstOrderBy.SelectedIndex == Convert.ToInt32(Count_FieldsToSelect.ToString()))
            {

            }
            else
            {

                lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex, lstOrderBy.Items[lstOrderBy.SelectedIndex + 1]);
                lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex + 1);

                //lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex, lstOrderBy.Items[lstOrderBy.SelectedIndex]);
                //lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex + 1);
            }

        }
        catch (Exception)
        {

            //throw;
        }
        finally
        {

        }
    }
    
    //protected void imgDownOrderBy_Click(object sender, ImageClickEventArgs e)
    //{
    //  //  if (!lstOrderBy.Items.Contains("ABC".ToLower()))
    //  ////  if (lstOrderBy.SelectedIndex > 0)
    //  //  {
    //  //      lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex, lstOrderBy.Items[lstOrderBy.SelectedIndex + 1]);
    //  //      lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex + 1);
    //  //  }
    //    try
    //    {
    //        lstOrderBy.Items.Insert(lstOrderBy.SelectedIndex, lstOrderBy.Items[lstOrderBy.SelectedIndex + 1]);
    //        lstOrderBy.Items.RemoveAt(lstOrderBy.SelectedIndex + 1);
    //    }
    //    catch (Exception)
    //    {
            
    //        throw;
    //    }
    //}

    protected void butCondition_Click(object sender, EventArgs e)
    {
        string SFTRXNO = string.Empty;
        pnlFilters.Visible = true;
         
        //COMMENTED ON 30-08-2010

        //for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        //{
        //    if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
        //    {
        //        DataSet ds = null;               
        //        ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));

        //        if (ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString() != "N")
        //        {
        //            if (ds.Tables[0].Rows[0]["COLUMNIDNO"].ToString() != "N" && ds.Tables[0].Rows[0]["COLUMNNAME"].ToString() != "N")
        //            {

        //                if(SFTRXNO.Length ==0)
        //                  SFTRXNO += ds.Tables[0].Rows[0]["SFTRXNO"].ToString();
        //                else
        //                  SFTRXNO += ","+ds.Tables[0].Rows[0]["SFTRXNO"].ToString();
 
        //            }

        //        }

        //    }

        //}


        for (int i = 0; i < lstFieldsToSelect.Items.Count; i++)
        {
            if (Convert.ToInt32(lstFieldsToSelect.Items[i].Value) > 0)
            {
                DataSet ds = null;
                ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstFieldsToSelect.Items[i].Value));

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


        if (Convert.ToInt32(lstFieldsToSelect.Items.Count) > 0)
        {
            if (SFTRXNO.Length >0)
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

        imgbutExporttoexcel.Visible = true;   
        imgbutExporttoWord.Visible = true;
        imgbutBack.Visible = true;
        string TableNames=string.Empty;
        string Fields = string.Empty;
        string WhereCondition = string.Empty;
        string orderBy = string.Empty;
        pnlgrdSelectFieldReport.Visible = true;
        pnlSelectionCriteria.Visible = false;
        //pnlExportButtons.Visible = true;
        TableNames=this.TableNames();
        Fields = this.Fields();
        WhereCondition = this.WhereCondition();
        orderBy = this.orderBy();
        this.BindListViewReport(TableNames, Fields, WhereCondition, orderBy);
        //objpayContrl.EmployeeSelectedFieldReport(TableNames, Fields, WhereCondition, orderBy);      
    }
    
    private string TableNames()
    {
        string TableNames = string.Empty;
        DataSet ds;

        //switch ()
        //{
        //    case "payroll_staff":
        //        break;
        //}

        //lstFieldsToSelect

        /* COMMENTED ON 30-08-2010 */

        //for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        //{
        //    if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
        //    {
        //        ds = null;
        //        ds=objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));

        //        if (ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString() != "N")
        //        {
        //            if(TableNames.Length ==0)     
        //                TableNames += ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString();
        //            else
        //                TableNames += "," + ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString();
                
        //        }

        //    }

        //}
        
        for (int i = 0; i < lstFieldsToSelect.Items.Count; i++)
        {
            if (Convert.ToInt32(lstFieldsToSelect.Items[i].Value) > 0)
            {
                ds = null;
                ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstFieldsToSelect.Items[i].Value));

                if (ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString() != "N")
                {
                    if (TableNames.Length == 0)
                        TableNames += ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString();
                    else
                        TableNames += "," + ds.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString();
                }

            }

        }
        
        return TableNames;
    }

    private string Fields()
    {
        string Fields = string.Empty;

        DataSet ds;
        for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        {
            if (Convert.ToInt32(lstSelectedFields.Items[i].Value) >= 0)
            {
                ds = null;
                ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));

                

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

        //COMMENTED ON 30-08-2010
        //for (int i = 0; i < lstSelectedFields.Items.Count; i++)
        //{
        //    if (Convert.ToInt32(lstSelectedFields.Items[i].Value) > 0)
        //    {
        //        ds = null;
        //        ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstSelectedFields.Items[i].Value));


        //        if(ds.Tables[0].Rows[0]["joining"].ToString() != "N")
        //        {
                                                      

        //            if (ds.Tables[0].Rows[0]["joining"].ToString() != "N")
        //            {
        //                if (JoiningCondition.Length == 0)
        //                    JoiningCondition += ds.Tables[0].Rows[0]["joining"].ToString();
        //                else
        //                    JoiningCondition +=" and " + ds.Tables[0].Rows[0]["joining"].ToString();
        //            }

        //        }

        //    }         
            
        //}

        for (int i = 0; i < lstFieldsToSelect.Items.Count; i++)
        {
            if (Convert.ToInt32(lstFieldsToSelect.Items[i].Value) > 0)
            {
                ds = null;
                ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstFieldsToSelect.Items[i].Value));


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
                DataSet ds1 = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(txtFilter.Text));

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
            WhereCondition = WhereCondition + "  (PSTATUS ='" + ddlstatus.SelectedValue + "' OR '" + ddlstatus.SelectedValue + "'='')";
        else
            WhereCondition = WhereCondition + " AND (PSTATUS ='" + ddlstatus.SelectedValue + "' OR '" + ddlstatus.SelectedValue + "'='')";


 
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
                if (Convert.ToInt32(lstOrderBy.Items[i].Value) >= 0)
                {
                    ds = null;
                    ds = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(lstOrderBy.Items[i].Value));


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
      DataSet ds=objpayContrl.GetSelectedSftrxno(SFTRXNO);
      grdvFilters.DataSource = ds;
      grdvFilters.DataBind();

      for (int i = 0; i<grdvFilters.Rows.Count; i++)
      {
          DropDownList ddlFilter = (DropDownList)grdvFilters.Rows[i].FindControl("ddlFilters");
          TextBox txtFilter = (TextBox)grdvFilters.Rows[i].FindControl("txtFilter");
          DataSet ds1 = objpayContrl.GetSingleSelectedFieldTable(Convert.ToInt32(txtFilter.Text));
          objCommon.FillDropDownList(ddlFilter, ds1.Tables[0].Rows[0]["EXTRA_TABLE_NAME"].ToString(), ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString(), ds1.Tables[0].Rows[0]["COLUMNNAME"].ToString(), ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString()+">0", ds1.Tables[0].Rows[0]["COLUMNIDNO"].ToString());

      }
       
    }

    //protected void butExporttoexcel_Click(object sender, EventArgs e)
    //{        
        
    //    this.Export("Excel");               
    //}
    
    //protected void butExporttoWord_Click(object sender, EventArgs e)
    //{
        
    //    this.Export("Word"); 

    //}
    
    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    pnlgrdSelectFieldReport.Visible = false;
    //    pnlSelectionCriteria.Visible = true;
    //    //pnlExportButtons.Visible = false;
    //    pnlFilters.Visible = false;
    //}
       
    private void Export(string type)
    {

        string filename=string.Empty;
        string ContentType=string.Empty;

        if (type == "Excel")
        {
            filename = "SelectedFieldReport.xls";
            ContentType="ms-excel";
        }
        else if (type == "Word")
        {
           filename = "SelectedFieldReport.doc";
           ContentType = "vnd.word";        
        }

        string attachment = "attachment; filename=" + filename;
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/"+ContentType;
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //grdSelectFieldReport.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();

        //string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", attachment);
       // Response.AppendHeader("Refresh", ".5; PayBankStatementReport.aspx");
        //Response.Charset = "";
        Response.ContentType = "application/" + ContentType;
        StringWriter sw1 = new StringWriter();
        HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
        grdSelectFieldReport.RenderControl(htw1);
        Response.Output.Write(sw1.ToString());
        //Response.End();
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();



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
        imgbutExporttoexcel.Visible = false;  
        imgbutExporttoWord.Visible = false;
        imgbutBack.Visible = false;
        pnlgrdSelectFieldReport.Visible = false;
        pnlSelectionCriteria.Visible = true;
        //pnlExportButtons.Visible = false;
        pnlFilters.Visible = false;
    }
}   
