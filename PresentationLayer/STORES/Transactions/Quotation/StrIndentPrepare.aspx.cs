//Created By: Chaitanya C. Bhure
//Date Of Creation:26-March-2010

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

public partial class STORES_Transactions_Quotation_StrIndentPrepare : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
    DataTable TmpIndent; //Temporary table For Indent.
    List<int> ReQ_TNoList;
    string SortField = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage.
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session.
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)           
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help.
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //lbluser.Text = Session["userfullname"].ToString();
                // lblDept.Text = Session["strdeptname"].ToString();
                Session["tmpindent"] = null;
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                this.BindReqListByDepartment();
                this.FillDept();
                this.GenerateIndentNo();
                this.BindIndentRefNo();
                this.InitialiseTempIndentTabl();
                btnRpt.Visible = false;

                // txtPName.Text = lbluser.Text;
                ddlDept.SelectedValue = Session["strdeptcode"].ToString();
                ViewState["ReqtrNO"] = null;

            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    //BindIndentRefNo For Report
    void BindIndentRefNo()
    {
        //objCommon.FillListBox(lstIndent, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "", "INDTRNO DESC");
        objCommon.FillListBox(lstIndent, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "INDTRNO DESC");
    }

    //Bind Req.List With grdReqList.
    void BindReqListByDepartment()
    {
        DataSet ds = null;
        try
        {

            //ds = objDeptReqController.GetDeptRequisionsAccepted();
            ds = objDeptReqController.GetMainStoreAndDeptStoreRequisions(Convert.ToInt32(Session["strdeptcode"]), ViewState["StoreUser"].ToString());

            grdReqList.DataSource = ds.Tables[0];
            grdReqList.EmptyDataText = "Sorry ! There Is No Any Requisition";
            grdReqList.DataBind();
            grdReqList.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Indent_Preparation.BindReqListByDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    //Bind Items For Selected REQ.
    void BindItemsForREQ(int reqTrNo)
    {
        DataSet ds = null;
        try
        {
            //ds = objDeptReqController.GetTranDetailsByReqNo(reqTrNo);
            ds = objDeptReqController.GetReqAcceptedItemsByReqNo(reqTrNo);
            // lvItemDetails.DataSource = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdItemList.DataSource = ds.Tables[0];
                grdItemList.DataBind();
            }
            else
            {
                grdItemList.DataSource = null;
                grdItemList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Indent_Preparation.BindItemsForREQ() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStore";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {      

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND APLNO=" + deptStoreUser);

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStore";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    //Show Items When User Select Perticular REQ.
    protected void grdReqList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int ReqtrNO = Convert.ToInt32(grdReqList.DataKeys[e.NewSelectedIndex].Value.ToString());
        ViewState["ReqtrNO"] = ReqtrNO;
        //InitialiseTempIndentTabl();
        BindItemsForREQ(ReqtrNO);
        //BindItemsForIndent();
        GenerateIndentNo();
        divRequisitionList.Visible = false;
        divItemDetails.Visible = true;
    }

    //Add Item To TmpIndent Table.
    protected void grdItemList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        TmpIndent = (DataTable)Session["tmpindent"];
        int Req_TNO = Convert.ToInt32(grdItemList.DataKeys[e.NewSelectedIndex].Value);
        DataRow dr = objDeptReqController.GetSingleTransDetailByReq_TRNO(Req_TNO).Tables[0].Rows[0];
        if (!TmpIndent.Rows.Contains(Req_TNO))
        {
            TmpIndent.ImportRow(dr);
            BindItemsForIndent();
            Session["tmpindent"] = TmpIndent;
        }
        else
        {
            objCommon.DisplayUserMessage(updatePanel1, "Item Already In Indent List", this.Page);

        }
    }
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in grdItemList.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chk") as CheckBox);
                CheckBox chkAll = (row.Cells[0].FindControl("chkall") as CheckBox);
                if (chkRow.Checked)
                {
                    count = 1;
                    TmpIndent = (DataTable)Session["tmpindent"];
                    int Req_TNO = Convert.ToInt32(grdItemList.DataKeys[row.RowIndex].Value);
                    Session["Req_TNO"] = Req_TNO;//Added By vijay 08-06-2020
                    DataRow dr = objDeptReqController.GetSingleTransDetailByReq_TRNO(Req_TNO).Tables[0].Rows[0];
                 

                   
                    if (!TmpIndent.Rows.Contains(Req_TNO))
                    {
                        TmpIndent.ImportRow(dr);
                        BindItemsForIndent();
                        Session["tmpindent"] = TmpIndent;
                        divItemDetails.Visible = true;
                        divItemList.Visible = true;
                        
                    }
                    else
                    {
                        objCommon.DisplayMessage("Item already exists", this.Page);
                        divItemList.Visible = true;
                    }
                    chkRow.Checked = false;
                }
            }
        }
        if (count == 0)
        {
            Showmessage("Please Select At Least One Item.");
            return;
        }
        DataRow dr1 = objDeptReqController.GetSingleTransDetailByReq_TRNO(Convert.ToInt32(Session["Req_TNO"].ToString())).Tables[0].Rows[0];
        if (dr1.Table.Rows.Count > 0)
        {
            Session["MDNO"] = dr1.Table.Rows[0]["MDNO"].ToString();
            //DataSet ds1 = objDeptReqController.GenrateIndNo(Convert.ToInt32(Session["MDNO"].ToString()));
            DataSet ds1 = objDeptReqController.GenrateIndNo(Convert.ToInt32(Session["MDNO"].ToString()), Convert.ToInt32(Session["OrgId"]));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtIndRefNo.Text = Convert.ToString(ds1.Tables[0].Rows[0]["INDNO"].ToString());
            }
        }
        
    }


    //initialise TmpIndent table. 
    void InitialiseTempIndentTabl()
    {
        TmpIndent = new DataTable();
        TmpIndent = objDeptReqController.GetTranDetailsByReqNo(0).Tables[0];
        //Set Primary Key Column For TmpIndent Table
        DataColumn[] Primary = { TmpIndent.Columns["REQ_TNO"] };
        TmpIndent.PrimaryKey = Primary;
        Session["tmpindent"] = TmpIndent;
    }


    protected void grdItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
        //string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        //string message = string.Format(prompt, Message);
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }



    //Bound Delete Function With IndentItem Gridview.
    protected void grdIndentItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    // we are using a html anchor and a hidden asp:button for the delete
        //    HtmlAnchor linkDelete = (HtmlAnchor)e.Row.FindControl("linkDelete");
        //    Button btnDelete = (Button)e.Row.FindControl("btnDelete");

        //    //for each delete link - the corresponding submit buttons id will be passed to delete call back as a hidden field
        //    string prompt = "$.prompt('Are you sure you want to delete the selected item?"
        //        + "<input type=\"hidden\" value=\"{0}\" name=\"hidID\" />'"
        //        + ", {{buttons: {{ Ok: true, Cancel: false }}, callback: confirmDeleteResult}} ); return false; ";
        //    btnDelete.Attributes["onclick"] = string.Format(prompt, btnDelete.ClientID);
        //}
    }

    //Delete Selected Row.
    protected void grdIndentItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int Req_Tno=Convert.ToInt32(grdIndentItemList.DataKeys[e.RowIndex].Value);
        //TmpIndent = (DataTable)Session["tmpindent"];
        //DataRow dr = TmpIndent.Rows.Find(Req_Tno );
        //if (ViewState["action"].Equals("edit"))
        //{
        //    ReQ_TNoList=(List<int>) Session["reqtno"] ;
        //    ReQ_TNoList.Add(Req_Tno);
        //    Session["reqtno"] = ReQ_TNoList;
        //}
        ////objDeptReqController.UpdateIndentNoForReqTran(Req_Tno, "null");
        //TmpIndent.Rows.Remove(dr);
        //BindItemsForIndent();
        //Session["tmpindent"] = TmpIndent;

        TmpIndent = (DataTable)Session["tmpindent"];
        int Req_TNO = Convert.ToInt32(grdIndentItemList.DataKeys[e.RowIndex].Value);
        DataView dv = TmpIndent.DefaultView;
        dv.RowFilter = "REQ_TNO<>" + Req_TNO;

        TmpIndent = dv.ToTable();
        DataColumn[] Primary = { TmpIndent.Columns["REQ_TNO"] };
        TmpIndent.PrimaryKey = Primary;
        BindItemsForIndent();
        Session["tmpindent"] = TmpIndent;
    }


    //Bind Selected Item For IndentItem Gridview.
    void BindItemsForIndent()
    {
        grdIndentItemList.DataSource = TmpIndent;
        grdIndentItemList.DataBind();
    }

    //Save Indent Info.
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (grdIndentItemList.Rows.Count > 0)
        {
            string Type = rdbtnList.SelectedValue;
            STR_INDENT objstrInd = new STR_INDENT();
            //Check For Action
            if (ViewState["action"].Equals("edit"))
                objstrInd.INDTRNO = Convert.ToInt32(ddlIndent.SelectedValue);

            //objstrInd.INDNO = "Ind/" + Session["strdeptcode"].ToString() + "/" + txtIndRefNo.Text;
            if (ddlIndent.SelectedIndex == 0)
            {
                objstrInd.INDNO = txtIndRefNo.Text;
            }
            else
            {
                objstrInd.INDNO = Convert.ToString(ddlIndent.SelectedItem);
            }
            objstrInd.INDSLIP_NO = "null";
            objstrInd.INDSLIP_DATE = Convert.ToDateTime(txtindDate.Text);
            objstrInd.NAME = txtPName.Text;
            objstrInd.REMARK = txtRemark.Text;
            objstrInd.REQTRNO = Convert.ToInt32(ViewState["ReqtrNO"].ToString());
            //save new indent.
            if (ViewState["action"].Equals("add"))
            {
                int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_REQ_TRAN", " count(*)", "indentno='" + objstrInd.INDNO + "'"));

                if (duplicateCkeck == 0)
                {
                    //CustomStatus cs = (CustomStatus)objDeptReqController.AddIndent(objstrInd, Session["colcode"].ToString(), Type, Convert.ToInt32(Session["strdeptcode"].ToString()));
                    CustomStatus cs = (CustomStatus)objDeptReqController.AddIndent(objstrInd, Session["colcode"].ToString(), Type, Convert.ToInt32(Session["strdeptcode"].ToString()));
                    if (cs == CustomStatus.RecordSaved)
                    {
                        for (int i = 0; i < grdIndentItemList.Rows.Count; i++)
                        {
                            TextBox txtIndQty = (TextBox)grdIndentItemList.Rows[i].FindControl("txtIndQty");
                            HiddenField hfItemNo = (HiddenField)grdIndentItemList.Rows[i].FindControl("hdfItemNo");
                            decimal indQty = Convert.ToDecimal(txtIndQty.Text);
                            int itemNo = Convert.ToInt32(hfItemNo.Value);

                            cs = (CustomStatus)objDeptReqController.UpdateIndentNoForReqTran(Convert.ToInt32(grdIndentItemList.DataKeys[i].Value), objstrInd.INDNO, indQty, itemNo);
                        }
                        if (cs == CustomStatus.RecordSaved || cs == CustomStatus.RecordUpdated)
                        {
                            //objCommon.DisplayUserMessage(updatePanel1, "Record save successfully", this.Page);
                            DisplayMessage("Record Saved Successfully.");
                            this.Clear();
                            BindReqListByDepartment();
                            BindIndentRefNo();
                        }

                    }
                }
                else
                {
                    //objCommon.DisplayUserMessage(updatePanel1, "Record already exist", this.Page);
                    DisplayMessage("Record already exist.");
                }
            }
            //update indent entry.
            else
            {
                ReQ_TNoList = (List<int>)Session["reqtno"];


                //Change ISINDENT Status For Item Which Are Modified

                foreach (int req_tno in ReQ_TNoList)
                {

                    //////for edit qty
                    objDeptReqController.UpdateIndentNoForReqTran(req_tno, "null", 0, 0);
                }
                //Change ISINDENT Status For Newly Added Items

                //CustomStatus cs = (CustomStatus)objDeptReqController.UpdateIndent(objstrInd, Session["colcode"].ToString(), Type, Convert.ToInt32(ddlDept.SelectedValue));

                int MDNO = Convert.ToInt32(ddlDept.SelectedValue);
                // CustomStatus cs = (CustomStatus)objDeptReqController.UpdateIndent(objstrInd, Session["colcode"].ToString(), Type, Convert.ToInt32(Session["strdeptcode"].ToString()));
                CustomStatus cs = (CustomStatus)objDeptReqController.UpdateIndent(objstrInd, Session["colcode"].ToString(), Type, Convert.ToInt32(Convert.ToInt32(ddlDept.SelectedValue)));

                if (cs == CustomStatus.RecordSaved || cs == CustomStatus.RecordUpdated)
                {
                    //Change ISINDENT Status For Newly Added Items
                    for (int i = 0; i < grdIndentItemList.Rows.Count; i++)
                    {
                        TextBox txtIndQty = (TextBox)grdIndentItemList.Rows[i].FindControl("txtIndQty");
                        HiddenField hfItemNo = (HiddenField)grdIndentItemList.Rows[i].FindControl("hdfItemNo");
                        decimal indQty = Convert.ToDecimal(txtIndQty.Text);
                        int itemNo = Convert.ToInt32(hfItemNo.Value);

                        cs = (CustomStatus)objDeptReqController.UpdateIndentNoForReqTran(Convert.ToInt32(grdIndentItemList.DataKeys[i].Value), objstrInd.INDNO, indQty, itemNo);
                    }
                    if (cs == CustomStatus.RecordSaved || cs == CustomStatus.RecordUpdated)
                    {
                        //objCommon.DisplayUserMessage(updatePanel1, "Record Updated Succesfully", this.Page);
                        DisplayMessage("Record Updated Succesfully.");
                        this.Clear();
                        BindIndentRefNo();
                    }

                }
            }
            grdReqList.Visible = true;
        }
        else
        {
            //objCommon.DisplayUserMessage(updatePanel1, "Sorry ! There are No Items For Indent", this.Page);
            DisplayMessage("Please Select Requisition Number.");

        }

    }

    //Fill Department Name t0 DropdownList.
    void FillDept()
    {
        try
        {
            if (ViewState["StoreUser"].ToString() == "MainStore")
            {
                objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "MDNO=1", "MDNAME");
                objCommon.FillDropDownList(ddlDepts, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStore")
            {
                objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "MDNAME");
                objCommon.FillDropDownList(ddlDepts, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "SDNAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillDept() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Clear Data.
    void Clear()
    {
        //txtindDate.Text = "";
        txtIndRefNo.Text = string.Empty;
        txtPName.Text = string.Empty;
        txtRemark.Text = string.Empty;
        rdbtnList.SelectedValue = "Q";
        ddlDept.SelectedValue = "0";
        InitialiseTempIndentTabl();
        grdIndentItemList.DataSource = null;
        grdIndentItemList.DataBind();
        grdItemList.DataSource = null;
        grdItemList.DataBind();
        ddlIndent.Visible = false;
        txtIndRefNo.Visible = true;
        txtindDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        ViewState["action"] = "add";
        this.GenerateIndentNo();
        Session["tmpindent"] = null;
        ViewState["ReqtrNO"] = null;
    }

    //To Modify Already Saved Indent.
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "edit";
        //objCommon.FillDropDownList(ddlIndent, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "", "INDTRNO DESC");INDNO not in (Select INDENTNO from STORE_PORDER ) AND
        objCommon.FillDropDownList(ddlIndent, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", " MDNO=" + Convert.ToInt32(Session["strdeptcode"]) + " AND INDNO NOT IN (Select INDENTNO from STORE_PORDER WHERE ISTYPE='D') and INDNO NOT IN (Select INDNO from STORE_QUOTENTRY ) and INDNO NOT IN (Select INDNO from STORE_TENDER )  ", "INDTRNO DESC");
        FillDept();
        ddlIndent.Visible = true;
        txtIndRefNo.Visible = false;
    }

    //Show Indent Details.
    void ShowIndentDeatails(int IndentTRNo)
    {
        DataSet ds = null;
        try
        {
            ds = objDeptReqController.GetSingleIndent(IndentTRNo);
            DataTable dt = ds.Tables[0];
            txtIndRefNo.Text = dt.Rows[0]["INDNO"].ToString().Split('/')[2];
            txtindDate.Text = Convert.ToDateTime(dt.Rows[0]["INDSLIP_DATE"].ToString()).ToString("dd/MM/yyyy");
            txtPName.Text = dt.Rows[0]["NAME"].ToString();
            txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
            rdbtnList.SelectedValue = dt.Rows[0]["TQSTATUS"].ToString();
            ddlDept.SelectedValue = dt.Rows[0]["MDNO"].ToString();
            ViewState["ReqtrNO"] = dt.Rows[0]["REQTRNO"].ToString();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, ex.Message);
        }
    }

    //Set Selected Indent Details.
    protected void ddlIndent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowIndentDeatails(Convert.ToInt32(ddlIndent.SelectedValue));
        BindItemsForIndentDuringMod();
        BindItemsForIndent();
        ReQ_TNoList = new List<int>();
        Session["reqtno"] = ReQ_TNoList;
        if (ViewState["action"].ToString() == "edit")
        {
            grdReqList.Visible = false;
        }
    }

    void BindItemsForIndentDuringMod()
    {
        InitialiseTempIndentTabl();
        DataSet ds = objCommon.FillDropDown("STORE_REQ_TRAN A, STORE_ITEM B,STORE_REQ_MAIN C,STORE_INDENT_MAIN D", "A.REQTRNO ,A.ITEM_NO,ISNULL(A.INDQTY ,0)REQ_QTY,ISNULL(A.RATE,0)RATE,B.ITEM_NAME,C.REQ_NO", "A.REQ_TNO", "A.ITEM_NO=B.ITEM_NO AND C.REQTRNO=A.REQTRNO AND D.INDNO=A.INDENTNO AND D.INDTRNO=" + Convert.ToInt32(ddlIndent.SelectedValue), "");
        // DataSet ds = objDeptReqController.GetItemsByIndentNo(Convert.ToInt32(ddlIndent.SelectedValue));
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TmpIndent.ImportRow(dr);
        }
    }

    //To Cancel Modification
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].Equals("edit"))
        {
            ReQ_TNoList = (List<int>)Session["reqtno"];

            ReQ_TNoList = null;
            ViewState["action"] = "add";

        }
        Clear();
        //txtindDate.Text = "";
    }


    protected void btnRpt_Click(object sender, EventArgs e)
    {
        //ShowReport("INDENT_REPORT", "Str_Indent_Report_New.rpt");
        if (objCommon.LookUp("STORE_CONFIG", "PARAMETER", "CONFIGDESC='INDENT REPORT ITEMWISE CLUB'").Trim() == "Y")
            ShowReport("INDENT_REPORT", "Str_Indent_Report_New_Club.rpt");
        else
            ShowReport("INDENT_REPORT", "Str_Indent_Report_New.rpt");

    }

    //To Show Indent report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@username=" + Session["userfullname"].ToString() + "," + "@INDTRNO=" + Convert.ToInt32(lstIndent.SelectedValue) + "," + "@P_INDTRNO=" + Convert.ToInt32(lstIndent.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void lstIndent_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRpt.Visible = true;
        //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Str_Indent_Report.rpt&param=@username=" + Session["userfullname"].ToString() + "," + "@INDTRNO=" + Convert.ToInt32(lstIndent.SelectedValue) + "," + "@P_INDTRNO=" + Convert.ToInt32(lstIndent.SelectedValue), "UAIMS");
    }


    //Sort Direction Property
    string SortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = "ASC";
            }
            return ViewState["SortDirection"].ToString();
        }
        set
        {
            string s = SortDirection;

            if (value == "flip")
            {
                s = s == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                s = value;
            }

            ViewState["SortDirection"] = s;
        }
    }

    public void AddSortImage()
    {
        if (SortField == "") return;


        // Search for the columm with the sort expression  
        for (int i = 0; i < grdReqList.Columns.Count; i++)
        {
            DataControlField dcf = grdReqList.Columns[i];
            if (dcf.SortExpression == SortField)
            {
                //// Add image to corresponding header row 
                //gvSamples.HeaderRow.Cells[i].Controls.Add(sortImage);
                break;
            }
        }
    }

    //Sort Gridview
    protected void grdReqList_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortField = e.SortExpression;
        SortDirection = "flip";
        DataSet ds = objDeptReqController.GetDeptRequisions();
        ds.Tables[0].DefaultView.Sort = SortField + " " + SortDirection;
        grdReqList.DataSource = ds.Tables[0];
        grdReqList.EmptyDataText = "Sorry ! There Is No Any Requistion";
        grdReqList.DataBind();
        grdReqList.Visible = true;
    }

    protected void grdReqList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReqList.PageIndex = e.NewPageIndex;
        BindReqListByDepartment();
    }

    void GenerateIndentNo()
    {
        //try 
        //{
        //    string dept= objCommon.LookUp("store_department","MDSNAME","MDNO="+ Session["strdeptcode"].ToString());
        //    string num = objCommon.LookUp("store_indent_main", "count(*)", "MDNO=" + Session["strdeptcode"].ToString());
        //    int IndNo= Convert.ToInt32(num)+1;
        //    DataSet ds = new DataSet();
        //    ds = objDeptReqController.GetDeptRequestByReqNo(reqTrNo);
        //    DataTable dt = ds.Tables[0];
        //    string ReqNo = dt.Rows[0]["REQ_NO"].ToString();
        //    string[] IndentRefNo = ReqNo.Split('/');
        //    txtIndRefNo.Text = IndentRefNo[0] + "/" + IndentRefNo[1] + "/" + dept+"/" + IndentRefNo[3]+"/"+ IndentRefNo[4]+"/INV"+Convert.ToString(IndNo);
        //}
        //catch (Exception ex)
        //{
        //    objUCommon.ShowError(Page, ex.Message);
        //}
        DataSet ds = new DataSet();
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        //ds = objDeptReqController.GenrateIndNo(mdno);
        ds = objDeptReqController.GenrateIndNo(mdno, Convert.ToInt32(Session["OrgId"]));   //10-03-2022
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtIndRefNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INDNO"].ToString());
        }

    }

    protected void ddlDepts_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind Req.List With grdReqList.
        if (chkDept.Checked)
        {
            BindGrdList();
        }

    }


    void BindGrdList()
    {
        DataSet ds = null;

        try
        {

            //if (this.CheckMainStoreUser())
            //{
                ds = objDeptReqController.GetDeptRequisions(Convert.ToInt32(ddlDepts.SelectedValue));
                grdReqList.DataSource = ds.Tables[0];
                grdReqList.EmptyDataText = "Sorry ! There Is No Any Requistion";
                grdReqList.DataBind();
                grdReqList.Visible = true;

            //}
            //else
            //{
                //lblerror.Text = "You can not access this page because your department is not Main Store";
            //}


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Indent_Preparation.BindReqListByDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    // code for filter Requisition By Date wise

    void BindGrdListByDatewise()
    {
        DataSet ds = null;

        try
        {

            //if (this.CheckMainStoreUser())
            //{
                ds = objDeptReqController.GetDeptRequisionsByDatewise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtLastDate.Text));
                grdReqList.DataSource = ds.Tables[0];
                grdReqList.EmptyDataText = "Sorry ! There Is No Any Requistion";
                grdReqList.DataBind();
                grdReqList.Visible = true;
            //}
            //else
            //{
            //    //lblerror.Text = "You can not access this page because your department is not Main Store";
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Indent_Preparation.BindReqListByDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void chkDept_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDept.Checked)

            ddlDepts.Visible = true;

        else
        {
            ddlDepts.Visible = false;
            BindReqListByDepartment();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //if (txtFromDate.Text != string.Empty || txtToDate.Text != string.Empty)
        if (txtStartDate.Text != string.Empty || txtLastDate.Text != string.Empty)
        {
            BindGrdListByDatewise();
        }
        else
        {
            DisplayMessage("Please select from date and to date.");
            return;
        }
    }

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDate.Checked)
        {
            txtStartDate.Visible = true;
            txtLastDate.Visible = true;
            ImaCalLastDate.Visible = true;
            ImaCalStartDate.Visible = true;
            btnSearch.Visible = true;
            divSD.Visible = true;
            divED.Visible = true;
            // startdate.Visible = true;
            //enddate.Visible = true;            
        }
        else
        {
            // startdate.Visible = false;
            //enddate.Visible = false;
            txtStartDate.Visible = false;
            txtLastDate.Visible = false;
            txtStartDate.Text = string.Empty;
            txtLastDate.Text = string.Empty;
            ImaCalLastDate.Visible = false;
            ImaCalStartDate.Visible = false;
            btnSearch.Visible = false;
            divSD.Visible = false;
            divED.Visible = false;
            BindReqListByDepartment();
        }
    }


    protected void btnSearchRep_Click(object sender, EventArgs e)
    {
       
        if (txtFromDate.Text != string.Empty || txtToDate.Text != string.Empty)
        {
            //if (!txtFromDate.Text.Equals(string.Empty))
            //{
            //    if (DateTime.Compare(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text)) == 1)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
            //        txtFromDate.Focus();
            //        return;
            //    }
            //}
            string DtFrom = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
            string DtTo = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");

            objCommon.FillListBox(lstIndent, "STORE_INDENT_MAIN", "INDTRNO", "INDNO", "INDSLIP_DATE between '" + DtFrom + "' and '" + DtTo + "'", "INDTRNO DESC");
        }
        else
        {
            DisplayMessage("Please select from date and to date.");
            return;
        }

    }


    protected void btnReportDate_Click(object sender, EventArgs e)
    {

    }


    #region Back/Next Buttons

    protected void btnReqListNext_Click(object sender, EventArgs e)
    {
        divRequisitionList.Visible = false;
        divItemDetails.Visible = true;
        divItemList.Visible = true;
        divIndentDetails.Visible = false;
        divIndentReport.Visible = false;
    }
    protected void btnDivIDBack_Click(object sender, EventArgs e)
    {
        divRequisitionList.Visible = true;
        divItemDetails.Visible = false;
        divItemList.Visible = false;
    }
    protected void btnDivIDNext_Click(object sender, EventArgs e)
    {
        divRequisitionList.Visible = false;
        divItemDetails.Visible = false;
        divItemList.Visible = false;
        divIndentDetails.Visible = true;
    }

    protected void btnDivIndentDBack_Click(object sender, EventArgs e)
    {
        divRequisitionList.Visible = false;
        divItemDetails.Visible = true;
        divItemList.Visible = true;
        divIndentDetails.Visible = false;
    }
    protected void btnDivIndentDNext_Click(object sender, EventArgs e)
    {
        divRequisitionList.Visible = false;
        divItemDetails.Visible = false;
        divItemList.Visible = false;
        divIndentDetails.Visible = false;
        divIndentReport.Visible = true;
    }


    protected void btnIndentReportBack_Click(object sender, EventArgs e)
    {
        divRequisitionList.Visible = false;
        divItemDetails.Visible = false;
        divItemList.Visible = false;
        divIndentDetails.Visible = true;
        divIndentReport.Visible = false;
    }
    #endregion

}