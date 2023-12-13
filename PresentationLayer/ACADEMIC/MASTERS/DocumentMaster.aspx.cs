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
               
            }
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "DEGREENO");
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO IN(1,2) AND ISNULL(ACTIVESTATUS,0) = 1", "IDTYPENO");
            //objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0", "NATIONALITY ASC");
            //int param_value = Convert.ToInt32(objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME='ALLOW_ADMISSION_CATEGORY_ON_DOCUMENT_LIST_MASTER'"));     // Added by Shrikant Waghmare on 15-11-2023

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
            ViewState["action"] = "add";
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
        try
         {
            DocumentControllerAcad objDC = new DocumentControllerAcad();
            DocumentAcad objDocument = new DocumentAcad();
            int nationalityno = 0;
            //bool IsMandatory = false;

            objDocument.Degree = Convert.ToInt32(ddlDegree.SelectedValue);
            objDocument.Documentname = txtDocumentName.Text;
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
           // nationalityno=Convert.ToInt32(ddlNationality.SelectedValue);
            //IsMandatory = (rdoMandetory.Checked ? true : false);
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
                    string documentno = objCommon.LookUp("ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME='" + txtDocumentName.Text.Trim() + "' and DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND ID_TYPE=" + Convert.ToInt32(ddlAdmType.SelectedValue) + "");
                    string srno = objCommon.LookUp("ACD_DOCUMENT_LIST", "COUNT(DOCUMENTNO)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR_NO="+ Convert.ToInt32(txtDocumentSrNo.Text) + "");
                   if (documentno != null && documentno != string.Empty)
                   {
                       objCommon.DisplayMessage(this.updDocument, "Record Already Exist", this.Page);
                       return;
                   }
                   if (srno != null && srno != string.Empty && srno != "0")
                   {
                       objCommon.DisplayMessage(this.updDocument, "Sr. No. Already Exist", this.Page);
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
                    else
                    {
                        //objCommon.DisplayMessage(this.updDocument, "Existing Record", this.Page);
                        //Label1.Text = "Record already exist";
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["documentno"] != null)
                    {
                        objDocument.Documentno = Convert.ToInt32(ViewState["documentno"].ToString());
                        //string srno = objCommon.LookUp("ACD_DOCUMENT_LIST", "SR_NO", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "");
                        string srno = objCommon.LookUp("ACD_DOCUMENT_LIST", "COUNT(DOCUMENTNO)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR_NO=" + Convert.ToInt32(txtDocumentSrNo.Text) + "");
                        //if (srno != null && srno != string.Empty && srno!="0")
                        //{
                        //    objCommon.DisplayMessage(this.updDocument, "Sr. No. Already Exist", this.Page);
                        //    return;
                        //}
                        CustomStatus cs = (CustomStatus)objDC.UpdateDocument(objDocument, category, nationality, AdmCategory);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updDocument, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            //Label1.Text = "Record already exist";
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
        DataSet ds = objCommon.FillDropDown("ACD_PARAMETER", "PARAM_VALUE", string.Empty, "PARAM_NAME='ALLOW_ADMISSION_CATEGORY_ON_DOCUMENT_LIST_MASTER'", string.Empty);     

        for (int i = 0; i < chkNationalityList.Items.Count; i++)
        {
            if (chkNationalityList.Items[i].Selected)
            {
                nationality += chkNationalityList.Items[i].Value.ToString() + ",";
            }
        }

        if (!string.IsNullOrEmpty(nationality))
        {
            nationality = nationality.Substring(0, nationality.Length - 1);
        }
        else
        {
            objCommon.DisplayMessage(this.updDocument, "Please select atleast one Nationality!", this.Page);
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
        return true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int documentno = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;

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
        DocumentControllerAcad objDC = new DocumentControllerAcad();
        SqlDataReader dr = objDC.GetDocumentByNo(documentno);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["documentno"] = documentno.ToString();
                ddlDegree.Text = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                ddlAdmType.Text = dr["ID_TYPE"] == null ? string.Empty : dr["ID_TYPE"].ToString();
                txtDocumentName.Text = dr["DOCUMENTNAME"] == null ? string.Empty : dr["DOCUMENTNAME"].ToString();
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

                PopulatNationalityList();
                nationality = dr["NATIONALITYNO"].ToString();
                string[] deg1 = nationality.Split(delimiterChars);
                for (int j = 0; j < deg1.Length; j++)
                {
                    for (int i = 0; i < chkNationalityList.Items.Count; i++)
                    {
                        if (deg1[j] == chkNationalityList.Items[i].Value)
                        {
                            chkNationalityList.Items[i].Selected = true;
                        }
                    }
                }

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

                //if (dr["MANDATORY"].ToString().ToLower().Equals("true"))
                //{
                //    rdoMandetory.Checked = true;
                //    rdoNonMadatory.Checked = false;
                //}
                //else
                //{
                //    rdoMandetory.Checked = false;
                //    rdoNonMadatory.Checked = true;
                //}
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        txtDocumentName.Text = string.Empty;
        txtDocumentSrNo.Text = string.Empty;
        //chkstatus.Checked = false;
        ViewState["action"] = "add";
       // ddlNationality.SelectedIndex = 0;
        chkNationalityList.ClearSelection();
        chkCategoryList.ClearSelection();
        chkAdmCategoryList.ClearSelection();
        //rdoMandetory.Checked = false;
        //rdoNonMadatory.Checked = true;
       
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
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

        //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    divMsg.InnerHtml += " </script>";
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}

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
}
