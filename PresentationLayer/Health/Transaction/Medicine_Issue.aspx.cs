/// created by vaibhav m on date 14-apr-2016
/// form use to issue medicine to patient.
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
using System.Collections.Generic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_Transaction_Medicine_Issue : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StockMaster objStock = new StockMaster();
    Health objHel   = new Health();
    HealthTransactions objHelTran = new HealthTransactions();
    StockMaintnance objSController = new StockMaintnance();
    HealthTransactionController objHealthTransactionController = new HealthTransactionController();
    int patientNo = 0, ItemNO = 0,QtyGiven=0,doctorno=0,opdno=0;
    string itemno, qtygiven, qtyissue, qtybal, issuestatus, doses, qtyavail, item_name, it_no;
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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select ITEM_NO, ITEM_NAME AS ITEM_NAME from HEALTH_ITEM where ITEM_NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["ITEM_NO"].ToString() + "---------*" + sdr["ITEM_NAME"].ToString());                        
                    }                    
                }
                conn.Close();
                return ItemsName;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDoseName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DNO, DNAME AS DNAME from HEALTH_DOSAGEMASTER where DNAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["DNO"].ToString() + "---------*" + sdr["DNAME"].ToString());
                    }
                }
                conn.Close();
                return ItemsName;
            }
        }
    }        

    protected void btnSerch_Click(object sender, EventArgs e)
    {
        try
        {
           this.BindData();
            
        } 
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_StockMaintenance_InvoiceEntry->btnSave_Click" + ex.Message);
        }
    }

    /// <summary>
    /// CheckPageAuthorization() method checks whether the user is authorised to access this Page    
    /// </summary>
    /// 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
           foreach (GridViewRow row in lvMedicineissue.Rows)
               {
                  if (row.RowType == DataControlRowType.DataRow)
                     { 
                       CheckBox   chkrow = (CheckBox)row.FindControl("chkIssue");
                       TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                       TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
                      TextBox txtAvilQty = (TextBox)row.FindControl("txtAvailQty");
                      TextBox txtQtyMain = (TextBox)row.FindControl("txtQtyMain");
                       if (chkrow.Checked == false)
                        {
                           objCommon.DisplayMessage(this.updpnlMain,"For issue medicine click on check box is must", this.Page);
                           return;
                        }
                       if (txtItemname.Text == string.Empty)
                       {
                           objCommon.DisplayMessage(this.updpnlMain, "Medicine name should not be blank.", this.Page);
                           return;
                       }
                       if (txtissue.Text == string.Empty)
                       {
                           objCommon.DisplayMessage(this.updpnlMain, "Issue Quantity should not be blank.", this.Page);
                           return;
                       }
                              int issueQty = Convert.ToInt32(txtissue.Text);
                              int avaiQty = Convert.ToInt32(txtAvilQty.Text);
                              int mainQty = Convert.ToInt32(txtQtyMain.Text);

                              if (issueQty > avaiQty)
                              {
                                  objCommon.DisplayMessage(this.updpnlMain, "Issue Quantity should not be greater than Available Quantity.", this.Page);
                                  return;
                              }
                              if (issueQty > mainQty)
                              {
                                  objCommon.DisplayMessage(this.updpnlMain, "Issue Quantity should not be greater than Quantity.", this.Page);
                                  return;
                              }
                     }
                }                             
            
            objHelTran.PID = Convert.ToInt32(lblPatientNO.Text);
            objHelTran.OPDID = Convert.ToInt32(lblopdNO.Text);
            objHelTran.DRID = lblDocNo.Text == string.Empty ? 0 : Convert.ToInt32(lblDocNo.Text);

             DataTable MedicineTbl = new DataTable("MediTbl");
             MedicineTbl.Columns.Add("ITEM_NAME", typeof(string));
            MedicineTbl.Columns.Add("INO", typeof(int));
            MedicineTbl.Columns.Add("QTY", typeof(int));
            MedicineTbl.Columns.Add("DOSESNO", typeof(int));
            MedicineTbl.Columns.Add("QTY_ISSUE", typeof(int));
            MedicineTbl.Columns.Add("AVAILABLE_QTY", typeof(int));
            MedicineTbl.Columns.Add("ISSUE_STATUS", typeof(int));

            DataRow dr = null;
            foreach (GridViewRow row in lvMedicineissue.Rows)
               {
                  if (row.RowType == DataControlRowType.DataRow)
                     {                                       
                       
                      TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                      Label   ino = (Label)row.FindControl("lblino");
                      TextBox qty = (TextBox)row.FindControl("txtQtyMain");
                      Label   dno = (Label)row.FindControl("lblDno");                     
                      TextBox  txtissue = (TextBox)row.FindControl("txtIssueQty");
                      TextBox    txtAvailQty = (TextBox)row.FindControl("txtAvailQty");
                       CheckBox   chkrow = (CheckBox)row.FindControl("chkIssue");
                                           
               
                            dr = MedicineTbl.NewRow();
                            dr["ITEM_NAME"] = txtItemname.Text;
                            dr["INO"] = ino.Text;
                            dr["QTY"] = qty.Text;
                            dr["DOSESNO"] = dno.Text;
                            dr["QTY_ISSUE"] = txtissue.Text;
                            dr["AVAILABLE_QTY"] = txtAvailQty.Text;
                              if (chkrow.Checked == true)
                              {
                                   dr["ISSUE_STATUS"] = "1" ;
                              }
                              else
                              {
                                   dr["ISSUE_STATUS"] = "0" ;
                              }
                            MedicineTbl.Rows.Add(dr);                
               }
            }
            objHelTran.MEDICINE_ISSUE = MedicineTbl;
            


                   
                    CustomStatus cs = (CustomStatus)objHealthTransactionController.AddHealthMedicineIsuue(objHelTran);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        btnsave.Enabled = false; 
                        objCommon.DisplayMessage(this.updpnlMain, "Record Saved Successfully", this.Page);
                        BindData();

                        Button btnAdd = lvMedicineissue.FooterRow.FindControl("btnAdd") as Button;
                        btnAdd.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnlMain, "errro in saving data", this.Page);
                    }       
         

        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->btnSave_Click" + ex.Message);
        }
    }
   
    protected void chkIssue_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            ((TextBox)row.FindControl("txtIssueQty")).Enabled = true;
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->chkIssue_CheckedChanged" + ex.Message);
        }
    }

    protected void lvMedicineissue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {               
                TextBox txtItemname = e.Row.FindControl("txtItemname") as TextBox;
                TextBox txtQtyMain = e.Row.FindControl("txtQtyMain") as TextBox;
                TextBox txtDoses = e.Row.FindControl("txtDoses") as TextBox;
                TextBox txtAvailQty = e.Row.FindControl("txtAvailQty") as TextBox;
                TextBox txtBalQty = e.Row.FindControl("txtBalQty") as TextBox;
                TextBox txtIssueQty = e.Row.FindControl("txtIssueQty") as TextBox;
                Label lblQtyMain = e.Row.FindControl("lblQtyMain") as Label;
                CheckBox chkIssue = e.Row.FindControl("chkIssue") as CheckBox; 
            }        
        }    
    }
    
    protected void rdlSerch_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.clear();
    }
    
    public void clear()
    {
        txtPatientNo.Text = string.Empty;
        lblopdNO.Text = string.Empty;
        lblPatientNO.Text = string.Empty;
        lblPatientName.Text = string.Empty;
        lblPatientSex.Text = string.Empty;
        lblPatientAge.Text = string.Empty;
        lblPatientCompl.Text = string.Empty;
        lblTreatmentDate.Text = string.Empty;
        lblDocName.Text = string.Empty;
        lvMedicineissue.DataSource = null;
        lvMedicineissue.DataBind();
        pnlStudGrid.Visible = false;
    }
   
    protected void EditCustomer(object sender, GridViewEditEventArgs e)
    {        
        lvMedicineissue.EditIndex = e.NewEditIndex;        
        BindData();        
    }    
    
    private void BindData()
    {
        try
        {
            DataSet ds = objSController.GetPatientDetails(Convert.ToInt32(rdlSerch.SelectedValue), txtPatientNo.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblopdNO.Text = ds.Tables[0].Rows[0]["OPDNO"].ToString();
                lblPatientNO.Text = ds.Tables[0].Rows[0]["PID"].ToString();
                ViewState["opdno"] = ds.Tables[0].Rows[0]["OPDNO"].ToString();
                lblPatientName.Text = ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
                lblPatientSex.Text = ds.Tables[0].Rows[0]["SEX"].ToString();
                lblPatientAge.Text = ds.Tables[0].Rows[0]["AGE"].ToString();
                lblPatientCompl.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                lblTreatmentDate.Text = ds.Tables[0].Rows[0]["PRESCDT"].ToString();
                lblDocName.Text = ds.Tables[0].Rows[0]["DRNAME"].ToString();
                lblDocNo.Text = ds.Tables[0].Rows[0]["DRID"].ToString();
                lvMedicineissue.DataSource = ds;
                lvMedicineissue.DataBind();
                pnlStudGrid.Visible = true;
                btnsave.Visible = true;
                btnclear.Visible = true;
                DataTable firstTable = ds.Tables[0];
                ViewState["CurrentTable"] = firstTable;



                foreach (GridViewRow row in lvMedicineissue.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                        TextBox qty = (TextBox)row.FindControl("txtQtyMain");
                        TextBox dno = (TextBox)row.FindControl("txtDoses");
                        CheckBox chkrow = (CheckBox)row.FindControl("chkIssue");
                        TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
                        LinkButton lnkR = (LinkButton)row.FindControl("lnkRemove");
                        
                        if (chkrow.Checked == true)
                        {
                            txtItemname.Enabled = false;
                            qty.Enabled = false;
                            dno.Enabled = false;
                            chkrow.Enabled = false;
                            txtissue.Enabled = false;
                            lnkR.Visible = false;
                           // ModalPopupExtender1.Hide();
                            Button btnAdd = lvMedicineissue.FooterRow.FindControl("btnAdd") as Button;
                            btnAdd.Visible = false;
                            btnsave.Enabled = false;
                        }
                        else
                        {
                            txtItemname.Enabled = true;
                            qty.Enabled = true;
                            dno.Enabled = true;
                            chkrow.Enabled = true;
                            txtissue.Enabled = true;
                            lnkR.Visible = true;
                           // ModalPopupExtender1.Show();
                            Button btnAdd = lvMedicineissue.FooterRow.FindControl("btnAdd") as Button;
                            btnAdd.Visible = true;
                            btnsave.Enabled = true;
                        }

                    }
                }
                
            }
            else
            {
                lblopdNO.Text = string.Empty;
                lblPatientNO.Text = string.Empty;
                lblPatientName.Text = string.Empty;
                lblPatientSex.Text = string.Empty;
                lblPatientAge.Text = string.Empty;
                lblPatientCompl.Text = string.Empty;
                lblTreatmentDate.Text = string.Empty;
                lblDocName.Text = string.Empty;
                pnlStudGrid.Visible = false;
                btnsave.Visible = false;
                btnclear.Visible = false;
                lvMedicineissue.DataSource = null;
                lvMedicineissue.DataBind();
                DataTable firstTable = ds.Tables[0];
                ViewState["CurrentTable"] = firstTable;
                objCommon.DisplayMessage(updpnlMain, "No record found for current Selection", this.Page);
                return;

            }
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_StockMaintenance_InvoiceEntry->btnSave_Click" + ex.Message);
        }
    }
   
    //protected void AddNewCustomer(object sender, EventArgs e)
    //{
    //    //TextBox txtItemName = ((TextBox)lvMedicineissue.FooterRow.FindControl("txtItemName"));
    //    DropDownList ddlItemName = ((DropDownList)lvMedicineissue.FooterRow.FindControl("ddlItemName"));
    //    TextBox txtItemQty = ((TextBox)lvMedicineissue.FooterRow.FindControl("txtItemQty"));
    //    DropDownList ddlDosename = ((DropDownList)lvMedicineissue.FooterRow.FindControl("ddlDosename"));
    //    CheckBox chkIssue1 = ((CheckBox)lvMedicineissue.FooterRow.FindControl("chkIssue1"));
    //    TextBox txtItemIssueQty = ((TextBox)lvMedicineissue.FooterRow.FindControl("txtItemIssueQty"));
    //    TextBox txtItemAvailQty = ((TextBox)lvMedicineissue.FooterRow.FindControl("txtItemAvailQty"));
    //    if (chkIssue1.Checked)
    //    {
    //        patientNo = Convert.ToInt32(lblPatientNO.Text);
    //        opdno = Convert.ToInt32(lblopdNO.Text);
    //        doctorno = lblDocNo.Text == string.Empty ? 0 : Convert.ToInt32(lblDocNo.Text);

    //        itemno += ddlItemName.SelectedValue == "0" ? "0" : ddlItemName.SelectedValue + ",";
    //        qtygiven += txtItemQty.Text.Trim() == string.Empty ? "NULL" : txtItemQty.Text + ",";
    //        doses += ddlDosename.SelectedValue == "0" ? "0" : ddlDosename.SelectedValue + ",";
    //        qtyissue += txtItemIssueQty.Text.Trim() == string.Empty ? "NULL" : txtItemIssueQty.Text + ",";
    //        qtyavail += txtItemAvailQty.Text.Trim() == string.Empty ? "NULL" : txtItemAvailQty.Text + ",";
    //        item_name += ddlItemName.SelectedItem.Text == "0" ? "0" : ddlItemName.SelectedItem.Text + ",";
    //        if (chkIssue1.Checked)
    //        {

    //            issuestatus += "1" + ",";
    //        }
    //        else
    //        {
    //            issuestatus += "0" + ",";
    //        }
    //        CustomStatus cs = (CustomStatus)objHealthTransactionController.AddHealthMedicineIsuue_Gride(patientNo, doctorno, opdno, itemno, qtygiven, doses, qtyissue,issuestatus, item_name);
    //        if (cs.Equals(CustomStatus.RecordSaved))
    //        {
    //            btnsave.Enabled = false;
    //            objCommon.DisplayMessage(this.updpnlMain, "Record Saved Successfully", this.Page);
    //            BindData();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updpnlMain, "errro in saving data", this.Page);
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.updpnlMain, "For Issue Medicine Please check on checkbox", this.Page);
    //        return;
    //    }
        
    //}
   
    protected void AddNewCustomer(object sender, EventArgs e)
    {
        try
        {
            AddNewRowToGrid();
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->AddNewCustomer" + ex.Message);
        }
    }

    private void AddNewRowToGrid()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtItemname = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[1].FindControl("txtItemname");
                        Label lblino = (Label)lvMedicineissue.Rows[rowIndex].Cells[1].FindControl("lblino");
                        TextBox txtQtyMain = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[2].FindControl("txtQtyMain");
                        TextBox txtDoses = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[3].FindControl("txtDoses");
                        Label lbldNO = (Label)lvMedicineissue.Rows[rowIndex].Cells[3].FindControl("lblDno");
                        CheckBox chkIssue = (CheckBox)lvMedicineissue.Rows[rowIndex].Cells[4].FindControl("chkIssue");
                        TextBox txtIssueQty = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[5].FindControl("txtIssueQty");
                        TextBox txtAvailQty = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[6].FindControl("txtAvailQty");
                        LinkButton lnkR = (LinkButton)lvMedicineissue.Rows[rowIndex].Cells[7].FindControl("lnkRemove");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ITEMNAME"] = txtItemname.Text;
                        dtCurrentTable.Rows[i - 1]["INO"] = lblino.Text;
                        dtCurrentTable.Rows[i - 1]["QTY"] = txtQtyMain.Text;
                        dtCurrentTable.Rows[i - 1]["DNAME"] = txtDoses.Text;
                        dtCurrentTable.Rows[i - 1]["DOSES"] = lbldNO.Text;
                        if (chkIssue.Checked == true)
                        {
                            dtCurrentTable.Rows[i - 1]["ISSUE_STATUS"] = 1;
                        }
                        else
                        {
                            dtCurrentTable.Rows[i - 1]["ISSUE_STATUS"] = 0;
                        }
                        if (txtIssueQty.Text != string.Empty)
                        {
                            dtCurrentTable.Rows[i - 1]["QTY_ISSUE"] = txtIssueQty.Text;
                        }
                        else
                        {
                            dtCurrentTable.Rows[i - 1]["QTY_ISSUE"] = 0;
                        }
                        dtCurrentTable.Rows[i - 1]["ITEM_MAX_QTY"] = txtAvailQty.Text;                                            
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvMedicineissue.DataSource = dtCurrentTable;
                    lvMedicineissue.DataBind();


                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks
         // SetPreviousData();

            //foreach (GridViewRow row in lvMedicineissue.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
            //        TextBox qty = (TextBox)row.FindControl("txtQtyMain");
            //        TextBox dno = (TextBox)row.FindControl("txtDoses");
            //        CheckBox chkrow = (CheckBox)row.FindControl("chkIssue");
            //        TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
            //        LinkButton lnkR = (LinkButton)row.FindControl("lnkRemove");
            //        if (chkrow.Checked == true)
            //        {
            //            txtItemname.Enabled = false;
            //            qty.Enabled = false;
            //            dno.Enabled = false;
            //            chkrow.Enabled = false;
            //            txtissue.Enabled = false;
            //            lnkR.Enabled = false;
            //        }
            //        else
            //        {
            //            txtItemname.Enabled = true;
            //            qty.Enabled = true;
            //            dno.Enabled = true;
            //            chkrow.Enabled = true;
            //            txtissue.Enabled = true;
            //            lnkR.Enabled = true;
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->AddNewCustomer" + ex.Message);
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
                   
                    TextBox txtItemname = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[1].FindControl("txtItemname");
                    TextBox txtQtyMain = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[2].FindControl("txtQtyMain");
                    //CheckBox chkIssue = (CheckBox)lvMedicineissue.Rows[rowIndex].Cells[3].FindControl("chkIssue");
                    TextBox txtDoses = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[3].FindControl("txtDoses");
                    TextBox txtAvailQty = (TextBox)lvMedicineissue.Rows[rowIndex].Cells[6].FindControl("txtAvailQty");

                    txtItemname.Text = dt.Rows[i]["ITEMNAME"].ToString();
                    txtQtyMain.Text = dt.Rows[i]["QTY"].ToString();
                    //chkIssue.Checked =Convert.ToBoolean(dt.Rows[i]["Column3"].ToString());
                    txtDoses.Text = dt.Rows[i]["DNAME"].ToString();
                    txtAvailQty.Text = dt.Rows[i]["ITEM_MAX_QTY"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    protected void DeleteCustomer(object sender, EventArgs e)
    {
        try
        {
            
            LinkButton lnkRemove = (LinkButton)sender;
            int ino = Convert.ToInt32(lnkRemove.CommandArgument);

           // DataTable dt = (DataTable)ViewState["CurrentTable"]; 
             DataTable MedicineTbl = new DataTable("MediTbl");
             MedicineTbl.Columns.Add("ITEMNAME", typeof(string));
             MedicineTbl.Columns.Add("INO", typeof(int));
             MedicineTbl.Columns.Add("QTY", typeof(int));
             MedicineTbl.Columns.Add("DNAME", typeof(string));
             MedicineTbl.Columns.Add("DOSES", typeof(int));
             MedicineTbl.Columns.Add("QTY_ISSUE", typeof(int));
             MedicineTbl.Columns.Add("ITEM_MAX_QTY", typeof(int));
             MedicineTbl.Columns.Add("ISSUE_STATUS", typeof(int));
             MedicineTbl.Columns.Add("ITEM_NO", typeof(int));

            DataRow dr = null;
            foreach (GridViewRow row in lvMedicineissue.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
                    Label itemno = (Label)row.FindControl("lblino");
                    TextBox qty = (TextBox)row.FindControl("txtQtyMain");
                    Label dno = (Label)row.FindControl("lblDno");
                    TextBox txtDoses = (TextBox)row.FindControl("txtDoses");
                    TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
                    TextBox txtAvailQty = (TextBox)row.FindControl("txtAvailQty");
                    CheckBox chkrow = (CheckBox)row.FindControl("chkIssue");

                    dr = MedicineTbl.NewRow();
                    dr["ITEMNAME"] = txtItemname.Text;
                    dr["INO"] = itemno.Text;
                    dr["QTY"] = qty.Text;
                    dr["DNAME"] = txtDoses.Text;
                    dr["DOSES"] = dno.Text;
                    if (txtissue.Text != string.Empty)
                    {
                        dr["QTY_ISSUE"] = txtissue.Text;
                    }
                    else
                    {
                        dr["QTY_ISSUE"] = 0;
                    }
                    dr["ITEM_MAX_QTY"] = txtAvailQty.Text;
                    dr["ITEM_NO"] = itemno.Text;
                    if (chkrow.Checked == true)
                    {
                        dr["ISSUE_STATUS"] = "1";
                    }
                    else
                    {
                        dr["ISSUE_STATUS"] = "0";
                    }

                    MedicineTbl.Rows.Add(dr);
                }
            }

            CustomStatus cs = (CustomStatus)objHealthTransactionController.DeleteHealthMedicineIsuue(ino, Convert.ToInt32(ViewState["opdno"]));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(this.updpnlMain, "Record Deleted Successfully", this.Page);              

                MedicineTbl.Rows.Remove(this.GetEditableDatarow(MedicineTbl, lnkRemove.CommandArgument));

                ViewState["CurrentTable"] = MedicineTbl;
                lvMedicineissue.DataSource = MedicineTbl;
                lvMedicineissue.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlMain, "errro in deleting data", this.Page);
            }
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->DeleteCustomer" + ex.Message);
        }
    }


    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["INO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_PatientDetails.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lvMedicineissue.EditIndex = -1;
        BindData();
    }
    
    //protected void UpdateCustomer(object sender, GridViewUpdateEventArgs e)
    //{
    //    try
    //    {
          
    //       foreach (GridViewRow row in lvMedicineissue.Rows)
    //           {
    //              if (row.RowType == DataControlRowType.DataRow)
    //                 { 
    //                   CheckBox   chkrow = (CheckBox)row.FindControl("chkIssue");
    //                   TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
    //                   TextBox txtissue = (TextBox)row.FindControl("txtIssueQty");
    //                   if (chkrow.Checked == false)
    //                    {
    //                       objCommon.DisplayMessage(this.updpnlMain,"For issue item click on check box is must", this.Page);
    //                       return;
    //                    }
    //                   if (txtItemname.Text == string.Empty)
    //                   {
    //                       objCommon.DisplayMessage(this.updpnlMain, "Medicine name should not be blank.", this.Page);
    //                       return;
    //                   }
    //                   if (txtissue.Text == string.Empty)
    //                   {
    //                       objCommon.DisplayMessage(this.updpnlMain, "Issue Quantity should not be blank.", this.Page);
    //                       return;
    //                   }                  
    //                 }
    //            }
                              
            
    //        objHelTran.PID = Convert.ToInt32(lblPatientNO.Text);
    //        objHelTran.OPDID = Convert.ToInt32(lblopdNO.Text);
    //        objHelTran.DRID = lblDocNo.Text == string.Empty ? 0 : Convert.ToInt32(lblDocNo.Text);

    //         DataTable MedicineTbl = new DataTable("MediTbl");
    //         MedicineTbl.Columns.Add("ITEM_NAME", typeof(string));
    //        MedicineTbl.Columns.Add("INO", typeof(int));
    //        MedicineTbl.Columns.Add("QTY", typeof(int));
    //        MedicineTbl.Columns.Add("DOSESNO", typeof(int));
    //        MedicineTbl.Columns.Add("QTY_ISSUE", typeof(int));
    //        MedicineTbl.Columns.Add("AVAILABLE_QTY", typeof(int));
    //        MedicineTbl.Columns.Add("ISSUE_STATUS", typeof(int));

    //        DataRow dr = null;
    //        foreach (GridViewRow row in lvMedicineissue.Rows)
    //           {
    //              if (row.RowType == DataControlRowType.DataRow)
    //                 {                                       
                       
    //                  TextBox txtItemname = (TextBox)row.FindControl("txtItemname");
    //                  Label   ino = (Label)row.FindControl("lblino");
    //                  TextBox qty = (TextBox)row.FindControl("txtQtyMain");
    //                  Label   dno = (Label)row.FindControl("lblDno");                     
    //                  TextBox  txtissue = (TextBox)row.FindControl("txtIssueQty");
    //                  TextBox    txtAvailQty = (TextBox)row.FindControl("txtAvailQty");
    //                   CheckBox   chkrow = (CheckBox)row.FindControl("chkIssue");  
               
    //                        dr = MedicineTbl.NewRow();
    //                        dr["ITEM_NAME"] = txtItemname.Text;
    //                        dr["INO"] = ino.Text;
    //                        dr["QTY"] = qty.Text;
    //                        dr["DOSESNO"] = dno.Text;
    //                        dr["QTY_ISSUE"] = txtissue.Text;
    //                        dr["AVAILABLE_QTY"] = txtAvailQty.Text;
    //                          if (chkrow.Checked == true)
    //                          {
    //                               dr["ISSUE_STATUS"] = "1" ;
    //                          }
    //                          else
    //                          {
    //                               dr["ISSUE_STATUS"] = "0" ;
    //                          }
    //                        MedicineTbl.Rows.Add(dr);                
    //           }
    //        }
    //        objHelTran.MEDICINE_ISSUE = MedicineTbl;       
                   
    //                CustomStatus cs = (CustomStatus)objHealthTransactionController.AddHealthMedicineIsuue(objHelTran);
    //                if (cs.Equals(CustomStatus.RecordSaved))
    //                {
    //                    btnsave.Enabled = false;
    //                    objCommon.DisplayMessage(this.updpnlMain, "Record Saved Successfully", this.Page);
    //                    //  BindData();
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.updpnlMain, "errro in saving data", this.Page);


    //                }         
         

    //    }
    //    catch (Exception ex)
    //    {
    //        objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_Transaction_Medicine_Issue->btnSave_Click" + ex.Message);
    //    }          
       
    //}
   
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
         DropDownList ddlItemName = ((DropDownList)lvMedicineissue.FooterRow.FindControl("ddlItemName"));
         TextBox txtItemAvailQty = ((TextBox)lvMedicineissue.FooterRow.FindControl("txtItemAvailQty"));
         string bal_qty = objCommon.LookUp("HEALTH_ITEM", "ITEM_MAX_QTY", "ITEM_NO=" + Convert.ToInt32(ddlItemName.SelectedValue));
         txtItemAvailQty.Text = bal_qty.ToString();
    }

    protected void txtItemname_TextChanged(object sender, EventArgs e)
    {
        TextBox txtitem = sender as TextBox;
        GridViewRow item = (GridViewRow)(sender as Control).NamingContainer;
     
        GridViewRow row = (GridViewRow)txtitem.NamingContainer;
        int index = row.RowIndex;
         
        TextBox txtItemname = (TextBox)item.FindControl("txtItemname");
        Label lblino = (Label)item.FindControl("lblino");
        LinkButton lnkRemove = (LinkButton)item.FindControl("lnkRemove");
        TextBox txtAvailQuantity = (TextBox)item.FindControl("txtAvailQty");       

        string txtValue = ((TextBox)lvMedicineissue.Rows[index].FindControl("txtItemname")).Text;
        string[] Value = txtValue.Split('-');
        string [] ITEM_NAME = Value[9].Split('*');

        lblino.Text = Value[0].ToString();
        lnkRemove.CommandArgument = Value[0].ToString();
        txtItemname.Text = ITEM_NAME[1].ToString();       
        txtItemname.Focus();

        //string bal_qty = objCommon.LookUp("HEALTH_ITEM", "ITEM_MAX_QTY", "ITEM_NO=" + Convert.ToInt32(lblitemNo.Text));
        DataSet ds = objHealthTransactionController.GetTotal(Convert.ToInt32(lblino.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtAvailQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString(); //bal_qty.ToString();
        } 

        //foreach (GridViewRow row1 in lvMedicineissue.Rows)
        //{
        //    if (row1.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblitemNo = (Label)row1.FindControl("lblino");
        //        TextBox txtAvailQuantity = (TextBox)row1.FindControl("txtAvailQty");               
        //        string bal_qty = objCommon.LookUp("HEALTH_ITEM", "ITEM_MAX_QTY", "ITEM_NO=" + Convert.ToInt32(lblitemNo.Text));               
        //        txtAvailQuantity.Text = bal_qty.ToString();
        //    }
        //}

    }

    protected void txtDoses_TextChanged(object sender, EventArgs e)
    {
        TextBox txtDoses = sender as TextBox;
        GridViewRow doses = (GridViewRow)(sender as Control).NamingContainer;

        GridViewRow row = (GridViewRow)txtDoses.NamingContainer;
        int index = row.RowIndex;

        TextBox txtDosesName = (TextBox)doses.FindControl("txtDoses");
        Label lblDno = (Label)doses.FindControl("lblDno");

        string txtValue = ((TextBox)lvMedicineissue.Rows[index].FindControl("txtDoses")).Text;

        string[] Value = txtValue.Split('*');
        txtDoses.Text = Value[1].ToString();

        string[] DOSES_NAME = Value[0].Split('-');

        lblDno.Text = DOSES_NAME[0].ToString();        
        txtDoses.Focus();

    }
   
}