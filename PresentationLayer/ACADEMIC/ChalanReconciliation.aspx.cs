//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : RECEIPT RECONCILIATION
// CREATION DATE : 20-AUG-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;


public partial class Academic_ChalanReconciliation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ChalanReconciliationController crController = new ChalanReconciliationController();

    #region Page Events

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

                DataSet dssearch = objCommon.FillDropDown("ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME,SRNO,IS_FEE_RELATED", "ID > 0 AND IS_FEE_RELATED=1 UNION ALL SELECT ID,CRITERIANAME,SRNO,IS_FEE_RELATED FROM ACD_SEARCH_CRITERIA WHERE ID = 2", "SRNO");
                ViewState["dssearch"] = dssearch;
                ddlSearch.DataSource = dssearch;
                ddlSearch.DataTextField = dssearch.Tables[0].Columns[1].ToString();
                ddlSearch.DataValueField = dssearch.Tables[0].Columns[0].ToString();
                ddlSearch.DataBind();

                for (int i = 0; i < dssearch.Tables[0].Rows.Count; i++)
                {
                    ddlSearch.Items[i + 1].Attributes.Add("title", dssearch.Tables[0].Rows[i][3].ToString());
                }
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by reconcile chalan or delete chalan buttons
                /// if yes then call corresponding methods
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "ReconcileChalan")
                        this.ReconcileChalan();
                    else if (Request.Params["__EVENTTARGET"].ToString() == "DeleteChalan")
                        this.DeleteChalan();
                }
                if (Convert.ToInt32(ddlSearch.SelectedItem.Value) == 14)
                {
                    lblDateFormat.Visible = true;
                }
                else
                {
                    lblDateFormat.Visible = false;
                }
            }
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
                Response.Redirect("~/notauthorized.aspx?page=ChalanReconciliation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ChalanReconciliation.aspx");
        }
    }
    #endregion

    #region SearchPannel

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divReconDel.Visible = false;
            BtnSubmit.Visible = false;
            pnlChallan.Visible = false;
            lvChallan.DataSource = null;
            lvChallan.DataBind();
       // pnlstatus.Visible = false;
        divpanel.Visible = true;
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
                if (ViewState["dssearch"] != null)
                {
                    DataSet dsDDL = ViewState["dssearch"] as DataSet;
                    for (int i = 0; i < dsDDL.Tables[0].Rows.Count; i++)
                    {
                        ddlSearch.Items[i + 1].Attributes.Add("title", dsDDL.Tables[0].Rows[i][3].ToString());
                        if (ddlSearch.SelectedValue == dsDDL.Tables[0].Rows[i][0].ToString())
                        {
                            ddlSearch.ToolTip = dsDDL.Tables[0].Rows[i][3].ToString();
                            return;
                        }

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lvSearchResults.Visible = false;
            lvSearchResults.DataSource = null;
            divRemark.Visible = false;
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }
        if (ddlSearch.ToolTip == "1")// Is fee related fields
        {
            string fieldName = string.Empty;
            string searchText = value;
            string errorMsg = string.Empty;

            if (ddlSearch.SelectedItem.Text == "Challan/Receipt No")
            {
                fieldName = "REC_NO";
                errorMsg = "having Receipt no. : " + value;
                ddlDropdown.Visible = false;
                divpanel.Visible = false;
                ddlSearch.ClearSelection();
            }

            else if (ddlSearch.SelectedItem.Text == "Challan Date")
            {
                fieldName = "REC_DT";
                errorMsg = "having Challan Date. : " + value;
                txtSearch.Text = string.Empty;
                ddlDropdown.Visible = false;
                ddlSearch.ClearSelection();
                divpanel.Visible = false;
            }

            else if (ddlSearch.SelectedItem.Text == "DD/Cheque No")
            {
                fieldName = "DDNO";
                errorMsg = "having DD/Cheque No. : " + value;
                txtSearch.Text = string.Empty;
                ddlDropdown.Visible = false;
                ddlSearch.ClearSelection();
                divpanel.Visible = false;
            }
            else if (ddlSearch.SelectedItem.Text == "NEFT/RTGS")
                {
                fieldName = "NEFT/RTGS";
                errorMsg = "having NEFT/RTG No. : " + value;
                txtSearch.Text = string.Empty;
                ddlDropdown.Visible = false;
                ddlSearch.ClearSelection();
                divpanel.Visible = false;
                }
            else if (ddlSearch.SelectedItem.Text == "NAME")
                {
                fieldName = "NAME";
                errorMsg = "having NAME. : " + value;
                txtSearch.Text = string.Empty;
                ddlDropdown.Visible = false;
                ddlSearch.ClearSelection();
                divpanel.Visible = false;
                }


            FeeCollectionController feeController = new FeeCollectionController();
            //DataSet ds = feeController.FindReceipts(fieldName, value);
            DataSet ds = crController.FindChalan(fieldName, value);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvSearchResults.DataSource = ds;
                lvSearchResults.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSearchResults);//Set label - 
                lvSearchResults.Visible = true;
                btnDelete.Disabled = false;
                btnReconcile.Disabled = false;
                divRemark.Visible = true;
                btnReconcile.Visible = true;
                btnDelete.Visible = true;
                BtnSubmit.Visible = false;
                pnlChallan.Visible = false;
                divReconDel.Visible = false;
                lvChallan.DataSource = null;
                lvChallan.DataBind();
            }
            else
            {
                objCommon.DisplayUserMessage(pnlFeeTable, "No Chalan found " + errorMsg, this.Page);
                lvSearchResults.Visible = false;
                btnReconcile.Disabled = true;
                btnDelete.Disabled = true;
                divRemark.Visible = false;
                BtnSubmit.Visible = false;
                pnlChallan.Visible = false;
                divReconDel.Visible = false;
                lvChallan.DataSource = null;
                lvChallan.DataBind();
            }
            txtRemark.Text = string.Empty;
        }
        else
        {
            bindlist(ddlSearch.SelectedItem.Text, value);
            //lvStudent.DataSource = null;
            //lvStudent.DataBind();
        }
        ddlDropdown.ClearSelection();
        //txtSearch.Text = string.Empty;

    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            FeeCollectionController feeController = new FeeCollectionController();
            string fieldName = string.Empty;
            string errorMsg = string.Empty;

            fieldName = "REGNO";
            errorMsg = "having Registration No. : " + txtSearch.Text;

            DataSet ds = crController.FindChalan(fieldName, txtSearch.Text);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvSearchResults.DataSource = ds;
                lvSearchResults.DataBind();
                lvSearchResults.Visible = true;
                btnDelete.Disabled = false;
                btnReconcile.Disabled = false;
                divRemark.Visible = true;
                lvStudent.Visible = false;
                btnReconcile.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                lvSearchResults.Visible = false;
                btnReconcile.Disabled = true;
                btnDelete.Disabled = true;
                divRemark.Visible = false;
                lvStudent.Visible = true;
                objCommon.DisplayUserMessage(pnlFeeTable, "No Chalan found " + errorMsg, this.Page);
            }
            txtRemark.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    private void ReconcileChalan()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            if (this.GetRecieptData(ref dcr))
            {
                if (crController.ReconcileChalan(dcr))
                {
                    string enroll = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(dcr.StudentId));
                    this.ShowMessage("Chalan reconciled successfully.");
                    HideControles();
                    divRemark.Visible = false;
                    ddlSearch.SelectedIndex = 0;
                    lblNoRecords.Visible = false;
                    btnReconcile.Visible = false;
                    btnDelete.Visible = false;
                }
                else
                {
                    this.ShowMessage("Unable to complete the operation.");
                    HideControles();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void DeleteChalan()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            if (this.GetRecieptData(ref dcr))
            {
                string ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                if (crController.DeleteChalan(dcr, ipaddress))
                {
                    this.ShowMessage("Chalan has been deleted.");
                    HideControles();
                    divRemark.Visible = false;
                    ddlSearch.SelectedIndex = 0;
                    lblNoRecords.Visible = false;
                    btnReconcile.Visible = false;
                    btnDelete.Visible = false;
                }
                else
                {
                    this.ShowMessage("Unable to complete the operation.");
                    HideControles();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool GetRecieptData(ref DailyCollectionRegister dcr)
    {
        try
        {
            foreach (ListViewDataItem item in lvSearchResults.Items)
            {
                string strDcrNo = (item.FindControl("hidDcrNo") as HiddenField).Value;
                int semesterno = Convert.ToInt32((item.FindControl("hidDcrSemesterNo") as HiddenField).Value);
                /// "Receipts" is a redio button list name. Request.Form["Receipts"] contains
                /// the value of selected radio button. it is a replacement of radio.checked.
                if (strDcrNo == Request.Form["Receipts"].ToString())
                {
                    dcr.DcrNo = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

                    string strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                    dcr.StudentId = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);

                    dcr.ReceiptDate = DateTime.Today;
                    dcr.UserNo = int.Parse(Session["userno"].ToString());
                    dcr.SemesterNo = semesterno;
                    if (Request.Params["__EVENTTARGET"].ToString() == "ReconcileChalan")
                        dcr.Remark = "This chalan has been reconciled by " + Session["userfullname"].ToString() + ". ";
                    else if (Request.Params["__EVENTTARGET"].ToString() == "DeleteChalan")
                        dcr.Remark = "This receipt has been deleted by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";

                    dcr.Remark += txtRemark.Text.Trim();
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return false;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert(\"" + msg + "\"); </script>";
    }


    private void HideControles()
    {
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        lvSearchResults.Visible = false;
        btnReconcile.Disabled = true;
        btnDelete.Disabled = true;
        txtRemark.Text = string.Empty;
        //txtSearchText.Text = string.Empty;
    }
    protected void rdoDDNo_CheckedChanged(object sender, EventArgs e)
    {
        //txtSearchText.Text = "";
        //lblDateFormat.Visible = false;
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        lvSearchResults.Visible = false;
        txtRemark.Text = "";
        btnDelete.Disabled = true;
        btnReconcile.Disabled = true;
        divRemark.Visible = false;
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;

            pnltextbox.Visible = false;
            txtSearch.Visible = false;
            pnlDropdown.Visible = false;
            DataSet dsfee = crController.BindPendingChallan_Excel();

            if (dsfee.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsfee;
                GV.DataBind();
                string attachment = "attachment; filename=PendingChallanReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
        }
        catch 
        {
            throw;
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        BindListView();
        divpanel.Visible = false;
        ddlSearch.SelectedIndex = 0;
        ddlSearch.ClearSelection();
    }

    private void BindListView()
    {
        try
        {
            ChalanReconciliationController crController = new ChalanReconciliationController();
            DataSet ds = crController.BindPendingChallanNew(Convert.ToInt32(ddlchalanstatus.SelectedValue.ToString()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                divReconDel.Visible = true;
                BtnSubmit.Visible = true;
                btnDelete.Disabled = true;
                btnReconcile.Disabled = true;
                pnlChallan.Visible = true;
                lvChallan.DataSource = ds;
                lvChallan.DataBind();
                lvChallan.Visible = true;
                lvSearchResults.Visible = false;
                divRemark.Visible = false;
                btnExcelReport.Visible = true;
                pnltextbox.Visible = false;
                txtSearch.Visible = false;
                pnlDropdown.Visible = false;
            }
            else
            {
                divReconDel.Visible = false;
                BtnSubmit.Visible = false;
                lvChallan.Visible = false;
                lvChallan.DataSource = null;
                lvChallan.DataBind();
                btnExcelReport.Visible = false;
                objCommon.DisplayUserMessage(pnlFeeTable, "Data Not Found...!", this);
                ddlchalanstatus.SelectedIndex = 0;
                txtSearch.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.ViewChalan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void imgbtnPrevDoc_Click(object sender, ImageClickEventArgs e)
    {
        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var ImageName = img;
        ImageButton lnkView = (ImageButton)(sender);
        string urlpath = System.Configuration.ConfigurationManager.AppSettings["paymentslip"].ToString();
        iframeView.Src = urlpath + ImageName;
        mpeViewDocument.Show();
    }

    protected void ddlchalanstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divReconDel.Visible = false;
        BtnSubmit.Visible = false;
        pnlChallan.Visible = false;
        lvChallan.DataSource = null;
        lvChallan.DataBind();
        ddlSearch.SelectedIndex = 0;
        lvSearchResults.Visible = false;
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        btnExcelReport.Visible = false;
        divRemark.Visible = false;
        btnReconcile.Visible = false;
        btnDelete.Visible = false;
        txtSearch.Text = string.Empty;
        pnltextbox.Visible = false;
        pnlDropdown.Visible = false;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ChalanReconciliationController chalan = new ChalanReconciliationController();
            int count = 0;
            CustomStatus cs = 0;
            string dcrno = ""; string semesterno = "";
            string idno = ""; 
            foreach (ListViewDataItem dataitem in lvChallan.Items)
            {
                CheckBox chkBoxX = dataitem.FindControl("chkadm") as CheckBox;
                Label lbldcr = dataitem.FindControl("lbldcr") as Label;
                Label lblsemesterno = dataitem.FindControl("lblsemesterno") as Label;
                if (chkBoxX.Checked == true && chkBoxX.Enabled == true)
                {
                    count++;
                    idno += chkBoxX.ToolTip + ',';
                    dcrno += lbldcr.Text.ToString() + ',';
                    semesterno += lblsemesterno.Text.ToString() + ',';
                   
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select At Least One Student !!", this.Page);
                return;
            }
            idno = idno.TrimEnd(',');
            dcrno = dcrno.TrimEnd(',');
            semesterno = semesterno.TrimEnd(',');
            int uano =Convert.ToInt32(Session["userno"].ToString());
            string remark = txtBulkRemark.Text;
            string ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            cs = (CustomStatus)chalan.AddChallaneReconcilation(idno, semesterno, dcrno, Convert.ToInt32(Convert.ToInt32(Session["OrgId"])), uano, remark, ipaddress);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
               // BindListView();
                txtBulkRemark.Text = "";
                //ddlchalanstatus.SelectedIndex = 0;
                BindListView();

            }
            else
            {
                objCommon.DisplayMessage(this, "Error in Saving", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
    }
protected void lvChallan_ItemDataBound(object sender, ListViewItemEventArgs e)
{
    if ((e.Item.ItemType == ListViewItemType.DataItem))
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        DataRow dr = ((DataRowView)dataItem.DataItem).Row;
        string Script = "";
        Script += "var arrOfHiddenColumns = [];";

        if (ddlchalanstatus.SelectedValue == "1" || ddlchalanstatus.SelectedValue == "3")
        {
            divReconDel.Visible = false;
            Script += "$('#divsessionlist th:nth-child(1)').hide();$('#divsessionlist td:nth-child(1)').hide();";
            Script += "arrOfHiddenColumns.push(2);";
        } 
        ViewState["ScriptTbl"] = Script;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Src", Script, true);
    }
}
protected void btndeletechallan_Click(object sender, EventArgs e)
    {
    //BindListView();
    }
}