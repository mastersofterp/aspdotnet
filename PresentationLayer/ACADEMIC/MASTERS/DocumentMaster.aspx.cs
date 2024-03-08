using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Created By  : 
Created On  : 
Purpose     :  
Version     : 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Version     Modified On     Modified By       Purpose
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
1.0.1       27-02-2024      Anurag Baghele    [53807]-Bind Document Name to Dropdown and save button take the value of Country category insist of nationlity
------------------------------------------- ------------------------------------------------------------------------------------------------------------------------------
1.0.2       28-02-2024      Anurag Baghele    [53807]-Added region Document Name for tab 1 and created ToggleTab method
------------------------------------------- ------------------------------------------------------------------------------------------------------------------------------
1.0.3       08-03-2024      Anurag Baghele    [53807]-Change the massage and remove null condition
------------------------------------------- ------------------------------------------------------------------------------------------------------------------------------
*/

public partial class ACADEMIC_MASTERS_DocumentMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objdfc = new StudentController();
    string category = string.Empty;
    string nationality = string.Empty;
    string AdmCategory = string.Empty;

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
                    this.CheckPageAuthorization();
                    Page.Title = Session["coll_name"].ToString();
                }

                objCommon.FillDropDownList(ddlDocumentName, "ACD_DOCUMENT_NAME", "ID", "Docname", "ID>0 AND ISNULL(ACTIVE_STATUS,0) = 1", "ID"); //<1.0.1>
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "DEGREENO");
                objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO IN(1,2) AND ISNULL(ACTIVESTATUS,0) = 1", "IDTYPENO");

                DataSet ds = objCommon.FillDropDown("ACD_PARAMETER", "PARAM_VALUE", string.Empty, "PARAM_NAME='ALLOW_ADMISSION_CATEGORY_ON_DOCUMENT_LIST_MASTER'", string.Empty);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string param_value = ds.Tables[0].Rows[0]["PARAM_VALUE"].ToString();

                    if (param_value != "1")
                    {
                        divAdmCategory.Visible = false;
                        chkAdmCategoryList.ClearSelection();
                    }
                    else if (param_value == "1")
                    {
                        divAdmCategory.Visible = true;
                        divCasteCategory.Visible = false;
                        chkCategoryList.ClearSelection();
                    }
                }
                else
                {
                    chkCategory.Visible = true;
                    divAdmCategory.Visible = false;
                    chkAdmCategoryList.ClearSelection();
                }


                PopulatAdmCategoryList();
                PopulatCategoryList();
                PopulatNationalityList();
                BindListView();
                BindListView_Tab1();
                ViewState["action"] = "add";
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
        }
        catch
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DocumentMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DocumentMaster.aspx");
        }
    }
    #endregion

    private void ToggleTab(string TabName)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('" + TabName + "');</script>", false);
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    #region Form Events

    private void PopulatCategoryList()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO<>0", "CATEGORY");
            DataSet ds = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO<>0 AND ISNULL(ACTIVESTATUS,0)=1", "CATEGORY");  //Added by ACTIVESTATUS sachin 27-07-2022

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkCategoryList.DataTextField = "CATEGORY";
                    chkCategoryList.DataValueField = "CATEGORYNO";
                    chkCategoryList.ToolTip = "CATEGORYNO";
                    chkCategoryList.DataSource = ds.Tables[0];
                    chkCategoryList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_SessionActivityDefinition.PopulateDegreeList --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulatNationalityList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO<>0", "NATIONALITY");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkNationalityList.DataTextField = "NATIONALITY";
                    chkNationalityList.DataValueField = "NATIONALITYNO";
                    chkNationalityList.ToolTip = "NATIONALITYNO";
                    chkNationalityList.DataSource = ds.Tables[0];
                    chkNationalityList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_SessionActivityDefinition.PopulateDegreeList --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulatAdmCategoryList()          // Added By Shrikant Waghmare on 15-11-2023
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO<>0", "CATEGORY");
            DataSet ds = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO<>0 AND ISNULL(ACTIVESTATUS,0)=1", "CATEGORY");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkAdmCategoryList.DataTextField = "CATEGORY";
                    chkAdmCategoryList.DataValueField = "CATEGORYNO";
                    chkAdmCategoryList.ToolTip = "CATEGORYNO";
                    chkAdmCategoryList.DataSource = ds.Tables[0];
                    chkAdmCategoryList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_SessionActivityDefinition.PopulateDegreeList --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "A.SHORTNAME");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ToggleTab("tab_2");
        try
        {
            DocumentControllerAcad objDC = new DocumentControllerAcad();
            DocumentAcad objDocument = new DocumentAcad();
            int nationalityno = 0;

            objDocument.Degree = Convert.ToInt32(ddlDegree.SelectedValue);
            objDocument.Documentname = ddlDocumentName.SelectedItem.Text;//<1.0.1>
            objDocument.DocNo = Convert.ToInt32(ddlDocumentName.SelectedValue);
            objDocument.DocumentSrno = Convert.ToInt32(txtDocumentSrNo.Text);
            objDocument.Idtype = Convert.ToInt32(ddlAdmType.Text);
            objDocument.CollegeCode = Convert.ToInt32(Session["colcode"]);

            //Added By Rishabh ON 29/10/2021
            if (hfdActive.Value == "true")
            {
                objDocument.chkstatus = 1;
            }
            else
            {
                objDocument.chkstatus = 0;
            }

            bool flag = getChk();

            if (flag == false)
            {
                return;
            }

            if (hfdMandatory.Value == "true")
            {
                objDocument.MandtStatus = 1;
            }
            else
            {
                objDocument.MandtStatus = 0;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string documentno = objCommon.LookUp("ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME='" + ddlDocumentName.SelectedItem.Text.Trim() + "' and DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND ID_TYPE=" + Convert.ToInt32(ddlAdmType.SelectedValue) + "");
                    string srno = objCommon.LookUp("ACD_DOCUMENT_LIST", "COUNT(DOCUMENTNO)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR_NO=" + Convert.ToInt32(txtDocumentSrNo.Text) + "");

                    if (documentno != null && documentno != string.Empty)
                    {
                        objCommon.DisplayMessage(this.updDocument, "Record Already Exist", this.Page);
                        return;
                    }
                    if (srno != null && srno != string.Empty && srno != "0")
                    {
                        objCommon.DisplayMessage(this.updDocument, "Document Sr. No. Already Exist", this.Page); //<1.0.3>
                        return;
                    }
                    //Add Batch
                    CustomStatus cs = (CustomStatus)objDC.AddDocument(objDocument, category, nationality, AdmCategory);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updDocument, "Record Saved Successfully!", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["documentno"] != null)
                    {
                        objDocument.Documentno = Convert.ToInt32(ViewState["documentno"].ToString());
                        string srno = objCommon.LookUp("ACD_DOCUMENT_LIST", "COUNT(DOCUMENTNO)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR_NO=" + Convert.ToInt32(txtDocumentSrNo.Text) + "");
                        CustomStatus cs = (CustomStatus)objDC.UpdateDocument(objDocument, category, nationality, AdmCategory);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updDocument, "Record Updated Successfully!", this.Page);
                        }
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    bool getChk()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_PARAMETER", "PARAM_VALUE", string.Empty, "PARAM_NAME='ALLOW_ADMISSION_CATEGORY_ON_DOCUMENT_LIST_MASTER'", string.Empty);

            for (int i = 0; i < chkCountryCategoryList.Items.Count; i++)
            {
                if (chkCountryCategoryList.Items[i].Selected)
                {
                    nationality += chkCountryCategoryList.Items[i].Value.ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(nationality))
            {
                nationality = nationality.Substring(0, nationality.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updDocument, "Please select atleast one Country Category!", this.Page);
                return false;
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string param_value = ds.Tables[0].Rows[0]["PARAM_VALUE"].ToString();

                if (param_value == "1")
                {

                    for (int i = 0; i < chkAdmCategoryList.Items.Count; i++)
                    {
                        if (chkAdmCategoryList.Items[i].Selected)
                        {
                            AdmCategory += chkAdmCategoryList.Items[i].Value.ToString() + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(AdmCategory))
                    {
                        AdmCategory = AdmCategory.Substring(0, AdmCategory.Length - 1);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updDocument, "Please select atleast one Admission Category!", this.Page);
                        return false;
                    }
                }
                else
                {
                    for (int i = 0; i < chkCategoryList.Items.Count; i++)
                    {
                        if (chkCategoryList.Items[i].Selected)
                        {
                            category += chkCategoryList.Items[i].Value.ToString() + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(category))
                    {
                        category = category.Substring(0, category.Length - 1);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updDocument, "Please select atleast one Category!", this.Page);
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < chkCategoryList.Items.Count; i++)
                {
                    if (chkCategoryList.Items[i].Selected)
                    {
                        category += chkCategoryList.Items[i].Value.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(category))
                {
                    category = category.Substring(0, category.Length - 1);
                }
                else
                {
                    objCommon.DisplayMessage(this.updDocument, "Please select atleast one Category!", this.Page);
                    return false;
                }
            }
        }
        catch
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
        }
        return true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //ViewState["action"] = null; //<1.0.3>
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ToggleTab("tab_2");
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int documentno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(documentno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Private Methods

    private void ShowDetail(int documentno)
    {
        try
        {
            DocumentControllerAcad objDC = new DocumentControllerAcad();
            SqlDataReader dr = objDC.GetDocumentByNo(documentno);

            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["documentno"] = documentno.ToString();
                    ddlDegree.Text = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                    ddlAdmType.Text = dr["ID_TYPE"] == null ? string.Empty : dr["ID_TYPE"].ToString();
                    ddlDocumentName.SelectedValue = dr["DOC_NO"] == null ? string.Empty : dr["DOC_NO"].ToString();
                    txtDocumentSrNo.Text = dr["SR_NO"] == null ? string.Empty : dr["SR_NO"].ToString();

                    //Added By Rishabh ON 29/10/2021
                    if (dr["ACTIVE_STATUS"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                    }

                    if (dr["MANDATORY"].ToString() == "Mandatory")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatMandat(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatMandat(false);", true);
                    }

                    char delimiterChars = ',';
                    PopulatCategoryList();
                    category = dr["CATEGORYNO"].ToString();
                    string[] deg = category.Split(delimiterChars);
                    for (int j = 0; j < deg.Length; j++)
                    {
                        for (int i = 0; i < chkCategoryList.Items.Count; i++)
                        {
                            if (deg[j] == chkCategoryList.Items[i].Value)
                            {
                                chkCategoryList.Items[i].Selected = true;
                            }
                        }
                    }

                    //PopulatNationalityList();
                    //<1.0.1>
                    nationality = dr["NATIONALITYNO"].ToString();
                    chkCountryCategoryList.ClearSelection();
                    string[] deg1 = nationality.Split(delimiterChars);
                    for (int j = 0; j < deg1.Length; j++)
                    {
                        for (int i = 0; i < chkCountryCategoryList.Items.Count; i++)
                        {
                            if (deg1[j] == chkCountryCategoryList.Items[i].Value)
                            {
                                chkCountryCategoryList.Items[i].Selected = true;
                            }
                        }
                    }
                    //</1.0.1>

                    PopulatAdmCategoryList();
                    AdmCategory = dr["ADMCATEGORYNO"].ToString();
                    string[] admCat = AdmCategory.Split(delimiterChars);
                    for (int j = 0; j < admCat.Length; j++)
                    {
                        for (int i = 0; i < chkAdmCategoryList.Items.Count; i++)
                        {
                            if (admCat[j] == chkAdmCategoryList.Items[i].Value)
                            {
                                chkAdmCategoryList.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
            if (dr != null) dr.Close();
        }
        catch
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    private void Clear()
    {
        ToggleTab("tab_2");
        ddlDocumentName.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        txtDocumentSrNo.Text = string.Empty;
        ViewState["action"] = "add";
        chkNationalityList.ClearSelection();
        chkCategoryList.ClearSelection();
        chkAdmCategoryList.ClearSelection();
        chkCountryCategoryList.ClearSelection();
    }

    private void BindListView()
    {
        try
        {
            DocumentControllerAcad objDC = new DocumentControllerAcad();
            DataSet ds = objDC.GetAllDocument();
            lvBatchName.DataSource = ds;
            lvBatchName.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBatchName);//Set label 

            foreach (ListViewItem item in lvBatchName.Items)
            {
                Label lblactinestatus = item.FindControl("lblactinestatus") as Label;
                if (lblactinestatus.Text == "1")
                {
                    lblactinestatus.Text = "Active";
                    lblactinestatus.Style.Add("color", "Green");
                }
                else
                {
                    lblactinestatus.Text = "DeActive";
                    lblactinestatus.Style.Add("color", "Red");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Masters_BatchMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("DocumentMaster", "rptDocumentMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updDocument, this.updDocument.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Document Name
    //<1.0.2>
    protected void btnSubmit_tab1_Click(object sender, EventArgs e)
    {
        try
        {
            DocumentControllerAcad objDC = new DocumentControllerAcad();
            DocumentAcad objDocument = new DocumentAcad();

            objDocument.Documentname = txtDocumentName_tab1.Text.Trim();

            if (hfdActive_tab1.Value == "true")
            {
                objDocument.chkstatus = 1;
            }
            else
            {
                objDocument.chkstatus = 0;
            }

            DataSet ds = objCommon.FillDropDown("ACD_DOCUMENT_LIST", "DOCUMENTNAME", string.Empty, "", string.Empty);

            bool documentFound = false;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (txtDocumentName_tab1.Text.Trim() == row["DOCUMENTNAME"].ToString())
                    {
                        documentFound = true;

                        if (hfdActive_tab1.Value == "false")
                        {
                            objCommon.DisplayMessage(this.updDocument, "Document status cannot be set to Inactive because the document is already mapped.", this.Page);
                            return;
                        }
                    }
                }
            }

            if (hdfDoc_Id.Value != string.Empty)
            {
                objDocument.DocId = Convert.ToInt32(hdfDoc_Id.Value);
            }
            else
            {
                objDocument.DocId = 0;
            }

            int cs = objDC.InsUpdDocumentName(objDocument);

            if (cs == 1)
            {
                objCommon.DisplayMessage(this.updDocument, "Record Saved Successfully!", this.Page);
                objCommon.FillDropDownList(ddlDocumentName, "ACD_DOCUMENT_NAME", "ID", "Docname", "ID>0 AND ISNULL(ACTIVE_STATUS,0) = 1", "ID"); //Added By Anurag Baghele on 29-02-2024
                clear_tab1();
            }
            else if (cs == 2)
            {
                objCommon.DisplayMessage(this.updDocument, "Record Updated Successfully!", this.Page);
                objCommon.FillDropDownList(ddlDocumentName, "ACD_DOCUMENT_NAME", "ID", "Docname", "ID>0 AND ISNULL(ACTIVE_STATUS,0) = 1", "ID"); //Added By Anurag Baghele on 29-02-2024
                clear_tab1();
            }
            else if (cs == 2627)
            {
                objCommon.DisplayMessage(this.updDocument, "Record Already Exist", this.Page);
                BindListView_Tab1();
                hdfDoc_Id.Value = string.Empty;
                hfdActive_tab1.Value = "";
            }
        }
        catch
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    private void BindListView_Tab1()
    {
        try
        {
            DocumentControllerAcad objDC = new DocumentControllerAcad();
            DataSet ds = objDC.GetAllDocumentName();
            lvDoc.DataSource = ds;
            lvDoc.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDoc);//Set label 

            foreach (ListViewItem item in lvDoc.Items)
            {
                Label lblactinestatus_tab1 = item.FindControl("lblactinestatus_tab1") as Label;
                if (lblactinestatus_tab1.Text == "1")
                {
                    lblactinestatus_tab1.Text = "Active";
                    lblactinestatus_tab1.Style.Add("color", "Green");
                }
                else
                {
                    lblactinestatus_tab1.Text = "DeActive";
                    lblactinestatus_tab1.Style.Add("color", "Red");
                }
            }

        }
        catch
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnEdit_tab1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Doc_Id = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("ACD_DOCUMENT_NAME", "Docname", "ACTIVE_STATUS", "ID=" + Doc_Id + "", "");

            hdfDoc_Id.Value = Doc_Id.ToString();

            txtDocumentName_tab1.Text = ds.Tables[0].Rows[0]["Docname"].ToString();

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVE_STATUS"]) == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive_Tab1(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive_Tab1(false);", true);
            }
        }
        catch
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnCancel_tab1_Click(object sender, EventArgs e)
    {
        clear_tab1();
    }

    protected void clear_tab1()
    {
        txtDocumentName_tab1.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive_Tab1(true);", true);
        BindListView_Tab1();
        hdfDoc_Id.Value = string.Empty;
        hfdActive_tab1.Value = "";
    }
    //</1.0.2>
    #endregion
}
