//======================================================================================
// PROJECT NAME  : RFC COMMON CODE                                                                
// MODULE NAME   : MASTER 'DEPARTMENT MASTER' 'DEGREE TYPE' 'DEGREE MASTER' 'BRANCH MASTER'                            
// CREATION DATE : 26-10-2021                                                         
// CREATED BY    : RISHABH BAJIRAO  
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE : 16-11-2021                                                                      
// MODIFIED DESC : Added New Column in degree master.                                                                     
//======================================================================================

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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class RFC_CONFIG_Masters_BasicMasterCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.DepartmentController objBC = new IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.DepartmentController();
    Department objDept = new Department();
    DegreeController objController = new DegreeController();
    Degree objDeg = new Degree();
    Branch ObjBranch = new Branch();
    BranchNameController objBc = new BranchNameController();

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
        if (!Page.IsPostBack)
        {
            TabName.Value = "tab_1";
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
                //Set the Page Title
                //Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {

                }
            }
            BindListViewDepMast();
            ViewState["action"] = "add";

            BindListViewDegTyp();
            ViewState["actiondegtyp"] = "add";

            BindListViewDegMaster();
            ViewState["actionDegMaster"] = "add";

            BindListViewBranchM();
            ViewState["actionbranch"] = "add";
            //Fill DropDown
            objCommon.FillDropDownList(ddlDegreeType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "ACTIVESTATUS=1", "UA_SECTION");
            objCommon.FillDropDownList(ddlKnowledgePartner, "ACD_KNOWLEDGE_PARTNER", "KNOWLEDGE_PARTNER_NO", "KNOWLEDGE_PARTNER", "ISNULL(ACTIVESTATUS,0)=1", "KNOWLEDGE_PARTNER");


        }
        else
        {
            TabName.Value = Request.Form[TabName.UniqueID];
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion Page Events

    #region Check Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_BasicMasterCreation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_BasicMasterCreation.aspx");
        }
    }
    #endregion Check Authorization

    #region Department_Master

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objDept.DepartmentName = txtDepartment.Text.Trim();
            objDept.DepartmentShortName = txtDeptShort.Text.Trim();
            if (hfdStat.Value == "true")
            {
                objDept.ActiveStatus = true;
            }
            else
            {
                objDept.ActiveStatus = false;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                //if (ViewState["action"].ToString().Equals("add"))
                //{
                if (ViewState["DEPTNO"] != null)
                {
                    objDept.DepartmentId = Convert.ToInt32(ViewState["DEPTNO"]);
                }
                CustomStatus cs = (CustomStatus)objBC.SaveDepartment(objDept);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    objCommon.DisplayUserMessage(this.updDepartment, "Record Saved Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    Clear();
                    objCommon.DisplayUserMessage(this.updDepartment, "Record Updated Successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayUserMessage(this.updDepartment, "Record already exist!", this.Page);
                }
                BindListViewDepMast();
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ShowDetailDept(editno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetailDept(int id)
    {
        DataSet ds = null;
        ds = objBC.GetDepartmentData(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["DEPTNO"] = id.ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0]["DEPTNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEPTNAME"].ToString();
                txtDeptShort.Text = ds.Tables[0].Rows[0]["DEPTCODE"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEPTCODE"].ToString();
                if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
    }

    private void Clear()
    {
        txtDepartment.Text = string.Empty;
        txtDeptShort.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["DEPTNO"] = null;
    }

    private void BindListViewDepMast()
    {
        try
        {
            DataSet ds = objBC.GetDepartmentData(0);
            lvDepartment.DataSource = ds;
            lvDepartment.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDepartment);//Set label -
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion Department_Master

    #region Degree_Type
    protected void btnSubmitDegType_Click(object sender, EventArgs e)
    {
        try
        {
            objDeg.DegreeTypeName = txtDegreeType.Text.Trim();
            if (hfdStatDegTyp.Value == "true")
            {
                objDeg.ActiveStatus = true;
            }
            else
            {
                objDeg.ActiveStatus = false;
            }

            // Check whether to add or update
            if (ViewState["actiondegtyp"] != null)
            {
                if (ViewState["actiondegtyp"].ToString().Equals("add"))
                {
                    if (ViewState["UA_SECTION"] != null)
                    {
                        objDeg.DegreeTypeID = Convert.ToInt32(ViewState["UA_SECTION"]);
                    }
                    CustomStatus cs = (CustomStatus)objController.SaveDegreeTypeData(objDeg);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(this.updDegreeType, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updDegreeType, "Record already exist", this.Page);
                    }

                }
                else
                {
                    //Edit
                    if (ViewState["UA_SECTION"] != null)
                    {
                        if (ViewState["UA_SECTION"] != null)
                        {
                            objDeg.DegreeTypeID = Convert.ToInt32(ViewState["UA_SECTION"]);
                        }
                        CustomStatus cs = (CustomStatus)objController.SaveDegreeTypeData(objDeg);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayUserMessage(this.updDegreeType, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(this.updDegreeType, "Record already exist", this.Page);
                        }
                    }
                }
                BindListViewDegTyp();
                ClearControls();
                hidTAB.Value = "#tab2";
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControls()
    {
        txtDegreeType.Text = string.Empty;
        ViewState["actiondegtyp"] = "add";
        ViewState["UA_SECTION"] = null;
        //hidTAB.Value = "#tab2";
    }

    private void BindListViewDegTyp()
    {
        try
        {
            DataSet ds = objController.GetDegreeTypeInfo(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlDegreeType.Visible = true;
                lvDegreeType.DataSource = ds;
                lvDegreeType.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDegreeType);//Set label -
            }
            else
            {
                pnlDegreeType.Visible = false;
                lvDegreeType.DataSource = null;
                lvDegreeType.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancelDegType_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    protected void btnEditDegType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hidTAB.Value = "#tab2";
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ShowDetailDegTyp(editno);
            ViewState["actiondegtyp"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetailDegTyp(int id)
    {
        DataSet ds = null;
        ds = objController.GetDegreeTypeInfo(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["UA_SECTION"] = id.ToString();
                txtDegreeType.Text = ds.Tables[0].Rows[0]["UA_SECTIONNAME"].ToString();

                if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatDegTyp(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatDegTyp(false);", true);
                }
            }
        }
    }
    #endregion Degree_Type

    #region Degree_Master

    private void ClearControlsDegMaster()
    {
        txtDegreeName.Text = string.Empty;
        txtDegreeShortName.Text = string.Empty;
        txtDegreeCode.Text = string.Empty;
        ddlDegreeType.SelectedIndex = 0;
        ViewState["actionDegMaster"] = "add";
        ViewState["DEGREENO"] = null;
    }

    private void BindListViewDegMaster()
    {
        try
        {
            DataSet ds = objController.GetDegreeInfo(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlDegreeMaster.Visible = true;
                lvDegreeMaster.DataSource = ds;
                lvDegreeMaster.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDegreeMaster);//Set label -
            }
            else
            {
                pnlDegreeMaster.Visible = false;
                lvDegreeMaster.DataSource = null;
                lvDegreeMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmitDegMaster_Click(object sender, EventArgs e)
    {
        try
        {
            objDeg.DegreeName = txtDegreeName.Text.Trim();
            objDeg.DegreeShort_Name = txtDegreeShortName.Text.Trim();
            objDeg.DegreeCode = txtDegreeCode.Text.Trim();
            objDeg.DegreeTypeID = Convert.ToInt32(ddlDegreeType.SelectedValue); //Added By Rishabh -  16/11/2021
            if (hfdStatDegMaster.Value == "true")
            {
                objDeg.ActiveStatus = true;
            }
            else
            {
                objDeg.ActiveStatus = false;
            }

            //Check whether to add or update
            if (ViewState["actionDegMaster"] != null)
            {
                if (ViewState["actionDegMaster"].ToString().Equals("add"))
                {
                    if (ViewState["DEGREENO"] != null)
                    {
                        objDeg.DegreeID = Convert.ToInt32(ViewState["DEGREENO"]);
                    }
                    CustomStatus cs = (CustomStatus)objController.SaveDegreeMasterData(objDeg);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(this.updDegreeMaster, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updDegreeMaster, "Record already exist", this.Page);
                    }

                }
                else
                {
                    //Edit  
                    if (ViewState["DEGREENO"] != null)
                    {
                        if (ViewState["DEGREENO"] != null)
                        {
                            objDeg.DegreeID = Convert.ToInt32(ViewState["DEGREENO"]);
                        }
                        CustomStatus cs = (CustomStatus)objController.SaveDegreeMasterData(objDeg);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayUserMessage(this.updDegreeMaster, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(this.updDegreeMaster, "Record already exist", this.Page);
                        }
                    }
                }
                BindListViewDegMaster();
                ClearControlsDegMaster();
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancelDegMaster_Click(object sender, EventArgs e)
    {
        ClearControlsDegMaster();
    }
    protected void btnEditDegMaster_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ShowDetailsDegMaster(editno);
            ViewState["actionDegMaster"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetailsDegMaster(int id)
    {
        DataSet ds = null;
        ds = objController.GetDegreeInfo(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["DEGREENO"] = id.ToString();

                txtDegreeName.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                txtDegreeShortName.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                txtDegreeCode.Text = ds.Tables[0].Rows[0]["DEGREE_CODE"].ToString();
                //objCommon.FillDropDownList(ddlDegreeType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "ACTIVESTATUS=1", "UA_SECTION");
                ddlDegreeType.SelectedValue = ds.Tables[0].Rows[0]["UA_SECTION"].ToString();

                if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatDegMaster(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatDegMaster(false);", true);
                }
            }
        }
    }
    #endregion Degree_Master

    #region Branch_Master

    private void BindListViewBranchM()
    {
        try
        {
            DataSet ds = objBc.GetBranchMasterData(0);
            lvBranch.DataSource = ds;
            lvBranch.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBranch);//Set label -
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearBranch()
    {
        txtBranchname.Text = string.Empty;
        txtBranchshortname.Text = string.Empty;
        ddlKnowledgePartner.SelectedIndex = 0;
        ViewState["actionbranch"] = "add";
        ViewState["BRANCHNO"] = null;
    }

    protected void btnSavebranch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBranchname.Text.Trim() == string.Empty)
            {
                objCommon.DisplayUserMessage(updMaster, "Please Enter Branch Name", this.Page);
            }
            else if (txtBranchshortname.Text.Trim() == string.Empty)
            {
                objCommon.DisplayUserMessage(updMaster, "Please Enter Branch ShortName", this.Page);
            }
            else
            {
                if (ddlKnowledgePartner.SelectedIndex > 0)
                {
                    ObjBranch.LongName = txtBranchname.Text.Trim() + '-' + ddlKnowledgePartner.SelectedItem.Text.Trim();
                }
                else
                {
                    ObjBranch.LongName = txtBranchname.Text.Trim();
                }

                ObjBranch.ShortName = txtBranchshortname.Text.Trim();
                ObjBranch.Branchname_Origral = txtBranchname.Text.Trim();
                ObjBranch.KpNo = Convert.ToInt32(ddlKnowledgePartner.SelectedValue);
                if (hfdbranch.Value == "true")
                {
                    ObjBranch.IsActive = true;
                }
                else
                {
                    ObjBranch.IsActive = false;
                }
                if (hdfIsCore.Value == "true")
                {
                    ObjBranch.Iscore = false;
                }
                else
                {
                    ObjBranch.Iscore = true;
                }
                //Check whether to add or update
                if (ViewState["actionbranch"] != null)
                {
                    //if (ViewState["actionbranch"].ToString().Equals("add"))
                    //{
                    if (ViewState["BRANCHNO"] != null)
                    {
                        ObjBranch.BranchNo = Convert.ToInt32(ViewState["BRANCHNO"]);
                    }
                    CustomStatus cs = (CustomStatus)objBc.SaveBranchMasterData(ObjBranch);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearBranch();
                        objCommon.DisplayUserMessage(this.updBranchMaster, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearBranch();
                        objCommon.DisplayUserMessage(this.updBranchMaster, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updBranchMaster, "Record Already exist", this.Page);
                        ClearBranch();
                    }
                    //}
                    //else
                    //{
                    //    //Edit
                    //    if (ViewState["BRANCHNO"] != null)
                    //    {
                    //        if (ViewState["BRANCHNO"] != null)
                    //        {

                    //            ObjBranch.BranchNo = Convert.ToInt32(ViewState["BRANCHNO"]);
                    //        }
                    //        CustomStatus cs = (CustomStatus)objBc.SaveBranchMasterData(ObjBranch);
                    //        if (cs.Equals(CustomStatus.RecordUpdated))
                    //        {
                    //            ClearBranch();
                    //            objCommon.DisplayUserMessage(this.updMaster, "Record Updated Successfully!", this.Page);
                    //        }
                    //        else
                    //        {
                    //            //objCommon.DisplayUserMessage(this.updBatch, "Existing Record", this.Page);
                    //            // lblname.Text = "Record already exist";

                    //            objCommon.DisplayUserMessage(this.updMaster, "Record Already Exist", this.Page);
                    //            ClearBranch();
                    //        }
                    //    }
                    //}
                    BindListViewBranchM();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnEditbranch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditbranch = sender as ImageButton;
            int branchno = int.Parse(btnEditbranch.CommandArgument);
            ShowDetailBranch(branchno);
            ViewState["actionbranch"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetailBranch(int id)
    {
        DataSet ds = null;
        ds = objBc.GetBranchMasterData(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BRANCHNO"] = id.ToString();
                txtBranchname.Text = ds.Tables[0].Rows[0]["BRANCHNAME_ORIGNAL"] == null ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNAME_ORIGNAL"].ToString();
                txtBranchshortname.Text = ds.Tables[0].Rows[0]["SHORTNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["SHORTNAME"].ToString();
                ddlKnowledgePartner.SelectedValue = ds.Tables[0].Rows[0]["KNOWLEDGE_PARTNER_NO"].ToString();
                if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatBranch(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatBranch(false);", true);
                }
                if (ds.Tables[0].Rows[0]["ISCORESTATUS"].ToString() == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatIsCore(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatIsCore(false);", true);
                }
            }
        }
    }

    protected void btnCancelbranch_Click(object sender, EventArgs e)
    {
        ClearBranch();
    }

    #endregion Branch_Master
}