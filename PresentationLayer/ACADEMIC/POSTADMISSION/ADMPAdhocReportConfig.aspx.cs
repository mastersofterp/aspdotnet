using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS.UAIMS;
using mastersofterp_MAKAUAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class ACADEMIC_POSTADMISSION_ADMPAdhocReportConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPAdhocReportConfigController ARCC = new ADMPAdhocReportConfigController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                ViewState["action"] = "add";
                ViewState["ADHOCID"] = 0;
                PopulateDropDownList();
                BindListView_ReportConfig();
            }
            else
                FocusLost();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }

    }
    #region Common Methods For All DropDown
    public void BindALLDDL(ref DropDownList ddl, DataSet ds, string textField, string valueField)
    {
        try
        {
            ddl.Items.Clear();
            ddl.DataSource = ds;
            ddl.DataValueField = ds.Tables[0].Columns[valueField].ToString();
            ddl.DataTextField = ds.Tables[0].Columns[textField].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, "Please Select");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPAdhocReportConfig.BindALLDDL() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SYS.PROCEDURES", "OBJECT_ID", "NAME", "OBJECT_ID > 0", "NAME");
            //BindALLDDL(ref ddlProcName, ds, "NAME", "OBJECT_ID");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPAdhocReportConfig.PopulateDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    //protected void ddlProcName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlProcName.SelectedIndex > 0)
    //        {
    //            DataSet ds = ARCC.GetAllParamsList((ddlProcName.SelectedItem.Text.ToString().Trim()).ToString());
    //            DataTable dt = ds.Tables[0];
    //            BindListView_ProcParamsList(dt);

    //        }
    //        else
    //        {

    //            pnlProcParamsList.Visible = false;
    //            lvProcParamsList.DataSource = null;
    //            lvProcParamsList.DataBind();

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.ShowError(Page, "ADMPAdhocReportConfig.ddlProcName_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
    //    }

    //}

    private void BindListView_ProcParamsList(DataTable dt)
    {
        try
        {
            //DataSet ds = ARCC.GetAllParamsList((ddlProcName.SelectedItem.Text.ToString().Trim()).ToString());

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            if (dt != null && dt.Rows.Count > 0)
            {
                pnlProcParamsList.Visible = true;
                lvProcParamsList.DataSource = dt;
                lvProcParamsList.DataBind();

                //int nextRow, currentRow = 0;
                foreach (ListViewDataItem lv in lvProcParamsList.Items)
                {
                    //HiddenField hfdAdhocId = lv.FindControl("hfdAdhocId") as HiddenField;
                    Label lblParamName = lv.FindControl("lblParamName") as Label;
                    DropDownList ddlControlId = lv.FindControl("CONTROLID") as DropDownList;

                    //nextRow = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (currentRow == nextRow)
                    //    {
                    //        //if(ddlControlId.SelectedItem.Text.Equals(dt.Rows[i]["CONTROLID"].ToString()){
                    //        ddlControlId.Items.FindByText(dt.Rows[i]["CONTROLID"].ToString()).Selected = true;
                    //    }
                    //    nextRow++;
                    //}
                    //currentRow++;
                }

            }
            else
            {

                pnlProcParamsList.Visible = false;
                lvProcParamsList.DataSource = null;
                lvProcParamsList.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.BindListView_ProcParamsList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region XMLData Methods
    DataTable CreateDatatable_Params()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "ACD_ADMP_ADHOC_REPORT_CONFIGDETAIL";

            dt.Columns.Add("PROCEDUREPARAMNAME");
            dt.Columns.Add("PAGECONTROLID");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.CreateDatatable_Params() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable Add_Datatable_Params()
    {
        DataTable dt = CreateDatatable_Params();
        try
        {
            if (pnlProcParamsList.Visible == true)
            {
                int rowIndex = 0;
                foreach (var item in lvProcParamsList.Items)
                {
                    DataRow dRow = dt.NewRow();
                    Label box1 = (Label)lvProcParamsList.Items[rowIndex].FindControl("lblParamName");
                    DropDownList ddlControlId = (DropDownList)lvProcParamsList.Items[rowIndex].FindControl("CONTROLID");

                    if (ddlControlId.SelectedIndex > 0)
                    {
                        dRow["PROCEDUREPARAMNAME"] = box1.Text.Trim();
                        dRow["PAGECONTROLID"] = ddlControlId.SelectedItem.Text.Trim();

                        //dRow["PAGECONTROLID"] = ddlControlId.SelectedItem.Text.Trim().Length > 0 ? ddlControlId.SelectedItem.Text.Trim() : null;
                        rowIndex += 1;
                        dt.Rows.Add(dRow);
                    }
                }
            }
            else
            {
                dt = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.Add_Datatable_Params() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private string validateParamsData()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvProcParamsList.Items)
            {

                Label lbl1 = (Label)lvProcParamsList.Items[rowIndex].FindControl("lblParamName");
                DropDownList ddlControlId = (DropDownList)lvProcParamsList.Items[rowIndex].FindControl("CONTROLID");
                if (ddlControlId.SelectedIndex.Equals(0))
                {
                    // _validate = "Already Selected Control Name.";
                    _validate = "Please Select Control for Parameter: " + lbl1.Text.Trim();
                    //ddlControlId.SelectedItem.Text = "Please Select";
                    return _validate;
                }
                int rowcheckDuplicateEntryIndex = 0;
                foreach (var Control in lvProcParamsList.Items)
                {
                    if (rowcheckDuplicateEntryIndex == rowIndex)
                    {
                        continue;
                    }

                    
                    if (ddlControlId.SelectedItem.Text.Trim().Equals(((DropDownList)Control.FindControl("CONTROLID")).SelectedItem.Text.Trim()))
                    {
                        _validate = "Already Selected Control Name.";
                        //_validate = "Please Select Control Name";
                        //ddlControlId.SelectedItem.Text = "Please Select";
                        return _validate;
                    }
                    if (Convert.ToInt32(ddlControlId.SelectedValue) <= 0)
                    {

                        _validate = "Please Select Control for Parameter: " + lbl1.Text.Trim();
                        return _validate;
                    }
                    rowcheckDuplicateEntryIndex++;
                }
                rowIndex++;

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.validateParamsData() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }
    #endregion

    #region Buttons Submit & Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["BoardNo"] = "";
        ViewState["action"] = "add";
        ClearData();
        BindListView_ReportConfig();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds1 = objCommon.FillDropDown("SYS.PROCEDURES", "NAME", "", "", "");
        bool spExists = false;
        foreach (DataRow dr in ds1.Tables[0].Rows)
        {
            var proc = dr["NAME"].ToString();
            if (txtProcName.Text == proc)
            {
                spExists = true;
            }
        }
        if (spExists == true)
        {
            if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
            {
                string _validateParamData = validateParamsData();
                if (_validateParamData != string.Empty)
                {
                    objCommon.DisplayMessage(_validateParamData, this.Page);
                    return;
                }

                string xmlData = null;
                DataTable dt = Add_Datatable_Params();
                if (dt != null)
                {
                    DataSet ds = new DataSet();
                    ds.DataSetName = "ACD_ADMP_ADHOC_REPORT_CONFIGURATIONDETAILS";
                    ds.Tables.Add(dt);

                    xmlData = datasetToXML(ds, ds.DataSetName, ds.Tables[0].TableName);
                }

                int AdhocId = 0;
                string ReportName = txtReportName.Text.Trim();
                //string ProcName = ddlProcName.SelectedItem.Text.Trim();
                string ProcName = txtProcName.Text.Trim();
                string TabName = txtTabName.Text.Trim() == string.Empty ? null : txtTabName.Text.Trim();
                int UserNo = Convert.ToInt32(Session["userno"]);
                int activeStatus;
                if (hfdActiveStatus.Value == "true")
                {
                    activeStatus = 1;
                }
                else
                {
                    activeStatus = 0;
                }

                string ControlName = string.Empty;
                if (pnlProcParamsList.Visible == true)
                {
                    int rowIndex = 0;
                    foreach (var item in lvProcParamsList.Items)
                    {
                        DropDownList ddlControlId = (DropDownList)lvProcParamsList.Items[rowIndex].FindControl("CONTROLID");

                        if (ddlControlId.SelectedIndex > 0 && ddlControlId.SelectedItem.Text != "Please Select")
                        {
                            ControlName += ddlControlId.SelectedItem.Text.Trim().ToString() + ",";
                        }
                        rowIndex++;
                    }
                    if (ControlName.Contains(','))
                    {
                        ControlName = ControlName.Remove(ControlName.Length - 1);
                    }
                }
                else
                {
                    ControlName = null;
                }

                int ret = 0;
                string displaymsg = "";

                if (ViewState["action"].ToString().Equals("add"))
                {
                    ret = Convert.ToInt32(ARCC.InsertReportParamData(ReportName, ProcName, ControlName, TabName, xmlData, UserNo, activeStatus));
                    displaymsg = "Record added successfully.";
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    AdhocId = Convert.ToInt32(ViewState["ADHOCID"]);

                    ret = Convert.ToInt32(ARCC.UpdateReportParamData(ReportName, ProcName, ControlName, TabName, xmlData, UserNo, AdhocId, activeStatus));
                    displaymsg = "Record updated successfully.";

                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }
                objCommon.DisplayMessage(displaymsg, this.Page);
                BindListView_ReportConfig();
                ClearData();


            }
        }
        else
            objCommon.DisplayMessage("Procedure does not exists!", this.Page);
       

    }
    #endregion

    private void BindListView_ReportConfig()
    {
        try
        {

            DataSet ds = ARCC.GetAllReportList();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlReportConfig.Visible = true;
                lvReportConfig.DataSource = ds;
                lvReportConfig.DataBind();

            }
            else
            {

                pnlReportConfig.Visible = false;
                lvReportConfig.DataSource = null;
                lvReportConfig.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvReportConfig.Items)
            {
                Label Status = dataitem.FindControl("lblActiveStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss.Equals("InActive"))
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.BindListView_ProcParamsList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            DataSet ds;
            int AdhocId = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["ADHOCID"] = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["action"] = "edit";

           
            ReportcommonBindExistAndEditData(AdhocId);
            //ddlProcName.Enabled = false;
            txtProcName.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPAdhocReportConfig.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ReportcommonBindExistAndEditData(int AdhocId)
    {
        try
        {
            DataSet ds;
            ds = ARCC.GetSingleReportData(AdhocId);

            if (ds.Tables.Count == 2)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    txtReportName.Text = ds.Tables[0].Rows[0]["REPORTNAME"].ToString();
                    //ddlProcName.SelectedItem.Text = ds.Tables[0].Rows[0]["PROCEDURENAME"].ToString();
                    //ddlProcName.ClearSelection();
                    //ddlProcName.Items.FindByText(ds.Tables[0].Rows[0]["PROCEDURENAME"].ToString()).Selected = true;
                    txtProcName.Text = ds.Tables[0].Rows[0]["PROCEDURENAME"].ToString();
                    txtTabName.Text = ds.Tables[0].Rows[0]["FORM_TABLIST"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["FORM_TABLIST"].ToString();
                    string activeStatus = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
                    if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "Set_ActiveStatus(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "Set_ActiveStatus(false);", true);
                    }
                    if (txtProcName != null)
                    {
                        DataSet ds1 = ARCC.GetAllParamsList((txtProcName.Text.ToString().Trim()).ToString());
                        DataTable dt = ds1.Tables[0];
                        BindListView_ProcParamsList(dt);

                    }
                    else
                    {

                        pnlProcParamsList.Visible = false;
                        lvProcParamsList.DataSource = null;
                        lvProcParamsList.DataBind();

                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        //BindListView_ProcParamsList(ds.Tables[1]);
                        DataTable dt = ds.Tables[1];
                        int nextRow, currentRow = 0;
                        DataSet ds1 = ARCC.GetAllParamsList((txtProcName.Text.ToString().Trim()).ToString());
                        if (ds1 != null)
                        {
                            foreach (ListViewDataItem lv in lvProcParamsList.Items)
                            {

                                DropDownList ddlControlId = lv.FindControl("CONTROLID") as DropDownList;

                                nextRow = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (currentRow == nextRow)
                                    {
                                        ddlControlId.Items.FindByText(dt.Rows[i]["CONTROLID"].ToString()).Selected = true;
                                    }
                                    nextRow++;
                                }
                                currentRow++;
                            }
                        }
                        
                    }
                }
                else if (ViewState["action"].ToString().Equals("add"))
                {

                    BindListView_ProcParamsList(ds.Tables[1]);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.commonBindExistAndEditData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearData()
    {
        ViewState["action"] = "add";
        ViewState["ADHOCID"] = 0;

        //ddlProcName.Enabled = true;
        txtProcName.Enabled = true;
        txtReportName.Text = string.Empty;
        txtTabName.Text = string.Empty;

        //ddlProcName.SelectedItem.Text = "Please Select";
        //ddlProcName.ClearSelection();
        txtProcName.Text = string.Empty;
        BindListView_ProcParamsList(null);
    }

    #region DataSet To XML (solve issue of Null Column)
    private string datasetToXML(DataSet ds, string dsName, string dtName)
    {
        try
        {
            StringWriter sw = new StringWriter();

            //ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);
            DataTable dt = ds.Tables[0];
            sw.Write(@"<" + dsName + ">");
            foreach (DataRow row in dt.Rows)
            {
                sw.Write(@"<" + dtName + ">");
                foreach (DataColumn col in dt.Columns)
                {
                    sw.Write(@"<" + XmlConvert.EncodeName(col.ColumnName) + @">");
                    sw.Write(row[col]);
                    sw.Write(@"</" + XmlConvert.EncodeName(col.ColumnName) + @">");
                }
                sw.Write(@"</" + dtName + ">");
            }
            sw.Write(@"</" + dsName + ">");
            return sw.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
    #endregion


    public void FocusLost()
    {
        
            string targetControl = Request.Form["__EVENTTARGET"];
            if (targetControl == txtProcName.UniqueID)
            {
                // TextBox lost focus, perform desired actions here
                // You can access the TextBox value using txtExample.Text
                try
                {
                    if (txtProcName != null)
                    {
                        DataSet ds = ARCC.GetAllParamsList((txtProcName.Text.ToString().Trim()).ToString());
                        DataTable dt = ds.Tables[0];
                        BindListView_ProcParamsList(dt);

                    }
                    else
                    {

                        pnlProcParamsList.Visible = false;
                        lvProcParamsList.DataSource = null;
                        lvProcParamsList.DataBind();

                    }

                }
                catch (Exception ex)
                {
                    objCommon.ShowError(Page, "ADMPAdhocReportConfig.ddlProcName_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
                }

            }
       
       
    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
            {
                popup.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
            }
            else
                objCommon.DisplayMessage("Password does not match!", this.Page);
    }
  
}