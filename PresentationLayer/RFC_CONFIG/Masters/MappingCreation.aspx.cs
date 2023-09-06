//======================================================================================
// PROJECT NAME  : RFC_common                                                                
// MODULE NAME   : MAPPING CREATION                        
// CREATION DATE : 14-10-2021                                                       
// CREATED BY    : RISHABH BAJIRAO   
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
public partial class RFC_CONFIG_Masters_MappingCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingCreationController objMapp = new MappingCreationController();
    Mapping objM = new Mapping();
    DegreeController objDeg = new DegreeController();
    OrganizationController objOrg = new OrganizationController();
    #region Page Events
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

            }
            //Populate the Drop Down Lists
            PopulateDropDownDeptMap();
            BindListViewDeptMap();
            BindMappingViewDeptMap(0);
            BindListViewDegree();
            BindMappingViewDegMap(0);
            //ViewState["ColgDepid"] = "addcolg";
            BindMappingViewBranchDeg(Convert.ToInt32(ddlCollegeBranchMap.SelectedValue), Convert.ToInt32(ddlDegreeBrMap.SelectedValue), 0);
            BindListViewBranchMap();
            //BindMappingViewBranchDeg(0);
        }
        else
        {
            TabName.Value = Request.Form[TabName.UniqueID];

        }
    }
    #endregion Page Events

    #region Check Authorization
    #endregion Check Authorization

    #region Bind Dropdownlists
    private void PopulateDropDownDeptMap()
    {
        // objCommon.FillDropDownList(ddlCollegeIdDepMap, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ActiveStatus=1", "COLLEGE_ID");
        // objCommon.FillDropDownList(ddlCollegeList, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ActiveStatus=1", "COLLEGE_ID");
        //// objCommon.FillDropDownList(ddlDegreeBrMap, "TBL_ACD_DEGREEMASTER", "DegreeId", "DegreeName", "ActiveStatus=1", "DegreeId");

        // /*************************************** Added By Dileep Kare on 26.10.2021 for College,Degree and Branch Mapping ***************************************************/
        // objCommon.FillDropDownList(ddlCollegeBranchMap, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ActiveStatus=1", "COLLEGE_ID");
        // objCommon.FillDropDownList(ddlBranchMap, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "ACTIVESTATUS=1", "BRANCHNO");
        // //objCommon.FillDropDownList(ddlDegreeBrMap, "TBL_ACD_DEGREEMASTER", "DegreeId", "DegreeName", "ActiveStatus=1", "DegreeId");
        // objCommon.FillDropDownList(ddlProgrammetypeBranchMap, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "ActiveStatus=1", "UA_SECTION");
        // objCommon.FillDropDownList(ddlCollegeTypeBranchMap, "tblConfigInstitutionTypeMaster", "InstitutionTypeId", "InstitutionTypeName", "ActiveStatus=1", "InstitutionTypeId");


        //DataSet ds = objMapp.GetDataToFillDropDownlist();
        DataSet ds1 = objOrg.GetDataToFillDropDownlist(Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
        if (ds1 != null && ds1.Tables.Count > 0)
        {
            if (ds1.Tables[6].Rows.Count > 0)
            {
                ddlBranchMap.DataTextField = ds1.Tables[6].Columns["LONGNAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlBranchMap.DataValueField = ds1.Tables[6].Columns["BRANCHNO"].ToString();
                ddlBranchMap.DataSource = ds1.Tables[6];      //assigning datasource to the dropdownlist  
                ddlBranchMap.DataBind();  //binding dropdownlist 
            }
            if (ds1.Tables[7].Rows.Count > 0)
            {
                ddlProgrammetypeBranchMap.DataTextField = ds1.Tables[7].Columns["UA_SECTIONNAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlProgrammetypeBranchMap.DataValueField = ds1.Tables[7].Columns["UA_SECTION"].ToString();
                ddlProgrammetypeBranchMap.DataSource = ds1.Tables[7];      //assigning datasource to the dropdownlist  
                ddlProgrammetypeBranchMap.DataBind();  //binding dropdownlist 
            }
            if (ds1.Tables[8].Rows.Count > 0)
            {
                ddlCoreBranch.DataTextField = ds1.Tables[8].Columns["LONGNAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlCoreBranch.DataValueField = ds1.Tables[8].Columns["BRANCHNO"].ToString();
                ddlCoreBranch.DataSource = ds1.Tables[8];      //assigning datasource to the dropdownlist  
                ddlCoreBranch.DataBind();  //binding dropdownlist 
            }
            if (ds1.Tables[9].Rows.Count > 0)
            {
                ddlFcultyno.DataTextField = ds1.Tables[9].Columns["FACULTY_NAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlFcultyno.DataValueField = ds1.Tables[9].Columns["FACULTY_NO"].ToString();
                ddlFcultyno.DataSource = ds1.Tables[9];      //assigning datasource to the dropdownlist  
                ddlFcultyno.DataBind();  //binding dropdownlist 
            }
        }
        if (ds1 != null && ds1.Tables.Count > 0)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlCollegeTypeBranchMap.DataTextField = ds1.Tables[0].Columns["InstitutionTypeName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlCollegeTypeBranchMap.DataValueField = ds1.Tables[0].Columns["InstitutionTypeId"].ToString();
                ddlCollegeTypeBranchMap.DataSource = ds1.Tables[0];      //assigning datasource to the dropdownlist  
                ddlCollegeTypeBranchMap.DataBind();  //binding dropdownlist 
            }

            if (ds1.Tables[5].Rows.Count > 0)
            {
                ddlCollegeIdDepMap.DataTextField = ds1.Tables[5].Columns["COLLEGE_NAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlCollegeIdDepMap.DataValueField = ds1.Tables[5].Columns["COLLEGE_ID"].ToString();
                ddlCollegeIdDepMap.DataSource = ds1.Tables[5];      //assigning datasource to the dropdownlist  
                ddlCollegeIdDepMap.DataBind();  //binding dropdownlist 

                ddlCollegeList.DataTextField = ds1.Tables[5].Columns["COLLEGE_NAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlCollegeList.DataValueField = ds1.Tables[5].Columns["COLLEGE_ID"].ToString();
                ddlCollegeList.DataSource = ds1.Tables[5];      //assigning datasource to the dropdownlist  
                ddlCollegeList.DataBind();  //binding dropdownlist 

                ddlCollegeBranchMap.DataTextField = ds1.Tables[5].Columns["COLLEGE_NAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlCollegeBranchMap.DataValueField = ds1.Tables[5].Columns["COLLEGE_ID"].ToString();
                ddlCollegeBranchMap.DataSource = ds1.Tables[5];      //assigning datasource to the dropdownlist  
                ddlCollegeBranchMap.DataBind();  //binding dropdownlist 
            }
        }
    }
    #endregion Bind Dropdownlists

    #region College_Dept_Mapping
    #region Dropdownlist Events
    protected void ddlCollegeIdDepMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeIdDepMap.SelectedIndex > 0)
        {
            BindMappingViewDeptMap(Convert.ToInt32(ddlCollegeIdDepMap.SelectedValue));
        }
        else
        {
            BindMappingViewDeptMap(0);
        }
    }
    #endregion Dropdownlist Events
    #region Button Click Events
    protected void btnSubmitDepMapp_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string depids = "";
            string depnames = "";
            objM.CollegeId = Convert.ToInt32(ddlCollegeIdDepMap.SelectedValue);
            objM.College_Name = ddlCollegeIdDepMap.SelectedItem.Text; //Added By Rishabh on 24/11/2021
            string clgName = ddlCollegeIdDepMap.SelectedItem.Text;
            //if (hfdStatDepart.Value == "true")
            //{
            //    objM.ActiveStatus = true;
            //}
            //else
            //{
            //    objM.ActiveStatus = false;
            //}
            foreach (ListViewDataItem dataitem in lvDepMapping.Items)
            {
                CheckBox chkBox = (dataitem.FindControl("chkDepart")) as CheckBox;
                Label lblDepName = (dataitem.FindControl("lblDepName")) as Label;
                if (chkBox.Checked)
                {
                    if (depids == "")
                    {
                        depids = chkBox.ToolTip;
                    }
                    else
                    {
                        depids = depids + "," + chkBox.ToolTip;
                    }
                    if (depnames == "")
                    {
                        depnames = clgName + " - " + lblDepName.Text;
                    }
                    else
                    {
                        depnames = depnames + "," + clgName + " - " + lblDepName.Text;
                    }
                }
            }
            if (depids != "")
            {
                CustomStatus cs = (CustomStatus)objMapp.SaveCollegeDepartMapping(objM, depids, depnames);

                msg = "Record Added Successfully.";
                objM.DepartmentId = 0;
            }
            objCommon.DisplayMessage(this.updDepartmentMapp, msg, this.Page);
            BindMappingViewDeptMap(0);
            ClearAllFieldsDepMapp();
            ClearCheckboxDepMapp();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.btnSubmitDepMapp_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnEditDepMap_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearCheckboxDepMapp();
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["ColgDepid"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionColgDepid"] = "edit";
            this.ShowDetailsDepMapping(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.btnEditDepMap_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancelDepMap_Click(object sender, EventArgs e)
    {
        ClearAllFieldsDepMapp();
        ClearCheckboxDepMapp();
        BindMappingViewDeptMap(0);
    }
    #endregion Button Click Events
    #region Methods
    private void ClearAllFieldsDepMapp()
    {
        ddlCollegeIdDepMap.SelectedIndex = 0;
    }
    private void ClearCheckboxDepMapp()
    {
        CheckBox headchk = (lvDepMapping.FindControl("cbDep") as CheckBox);
        if (headchk.Checked)
        {
            headchk.Checked = false;
        }
        foreach (ListViewItem item in lvDepMapping.Items)
        {
            if (item.ItemType == ListViewItemType.DataItem)
            {
                foreach (Control ctr in item.Controls)
                {
                    var tt = ctr;
                    CheckBox chk = (CheckBox)ctr.FindControl("chkDepart");
                    CheckBox chkHead = (CheckBox)ctr.FindControl("cbDep");

                    if (chk.Checked)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
    }
    private void ShowDetailsDepMapping(int id)
    {
        try
        {
            DataSet ds = null;
            ds = objMapp.GetMappedDepartmentData(id);
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCollegeIdDepMap.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    int i = 0;
                    foreach (ListViewDataItem item in lvDepMapping.Items)
                    {
                        CheckBox chkBox = (item.FindControl("chkDepart")) as CheckBox;
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (ds.Tables[0].Rows[j]["DEPTNO"].ToString() == chkBox.ToolTip)
                                chkBox.Checked = true;
                            //chkBox.Checked = false;
                        }

                    }
                    //if (ds.Tables[0].Rows[0]["ActiveStatus"].ToString() == "Active")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatDepart(true);", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatDepart(false);", true);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.ShowDetailsDepMapping-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void BindMappingViewDeptMap(int depID)
    {
        try
        {
            DataSet ds = objMapp.GetMappedDepartmentData(depID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvMappedDepartment.DataSource = ds;
                lvMappedDepartment.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvMappedDepartment);//Set label -
                ClearCheckboxDepMapp();
                    //ddlCollegeIdDepMap.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                if (depID > 0)
                    {
                    int i = 0;
                    foreach (ListViewDataItem item in lvDepMapping.Items)
                        {
                        CheckBox chkBox = (item.FindControl("chkDepart")) as CheckBox;
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                            if (ds.Tables[0].Rows[j]["DEPTNO"].ToString() == chkBox.ToolTip)
                                chkBox.Checked = true;
                            //chkBox.Checked = false;
                            }

                        }
                    }
            }
            else
            {
                lvMappedDepartment.DataSource = null;
                lvMappedDepartment.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.BindMappingViewDeptMap-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    private void BindListViewDeptMap()
    {
        try
        {
            DataSet ds = objMapp.GetDepartmentList(0);
            //DataTable products = ds.Tables[0];
            //DataRow result = (from row in products.AsEnumerable()

            //                  where row.Field<int>("DEPTNO") == 1

            //                  select row).SingleOrDefault();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDepMapping.DataSource = ds;
                lvDepMapping.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDepMapping);//Set label -
                hdncount1.Value = lvDepMapping.Items.Count.ToString();
            }
            else
            {
                lvDepMapping.DataSource = null;
                lvDepMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.BindListViewDeptMap-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Methods
    #endregion College_Dept_Mapping

    #region College_Degree_Mapping

    #region Button Click Events
    protected void lnkSaveDegreeMap_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string degids = "";
            string degnames = "";
            objM.CollegeDepartmentId = Convert.ToInt32(ddlCollegeList.SelectedValue);
            objM.College_Name = ddlCollegeList.SelectedItem.Text; //Added By Rishabh on 24/11/2021
            string clgDeptName = ddlCollegeList.SelectedItem.Text;

            foreach (ListViewDataItem dataitem in lstDegree.Items)
            {
                CheckBox chkBox = (dataitem.FindControl("chkDegree")) as CheckBox;
                Label lblDegName = (dataitem.FindControl("lblDegName")) as Label;
                if (chkBox.Checked)
                {
                    if (degids == "")
                    {
                        degids = chkBox.ToolTip;
                    }
                    else
                    {
                        degids = degids + "," + chkBox.ToolTip;
                    }
                    if (degnames == "")
                    {
                        degnames = clgDeptName + " - " + lblDegName.Text;
                    }
                    else
                    {
                        degnames = degnames + "," + clgDeptName + " - " + lblDegName.Text;
                    }
                }
            }
            if (degids != "")
            {
                CustomStatus cs = (CustomStatus)objMapp.SaveDegreeDeptMapping(objM, degids, degnames);

                msg = "Record Added Successfully.";
                objM.CollegeDepartmentId = 0;
            }
            objCommon.DisplayMessage(this.updDepartmentMapp, msg, this.Page);
            BindMappingViewDegMap(0);
            ClearControlsDegMap();
            ClearCheckboxDegMap();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.btnSubmitDepMapp_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnCancelDegreeMap_Click(object sender, EventArgs e)
    {
        ClearControlsDegMap();
        ClearCheckboxDegMap();
        BindMappingViewDegMap(0);
    }
    protected void btnEditDegMap_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearCheckboxDegMap();
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["ColgDepid"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionColgDepid"] = "edit";
            this.ShowDetailsDegreeMapping(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.btnEditDegMap_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Button Click Events

    #region Methods
    private void ClearControlsDegMap()
    {
        ddlCollegeList.SelectedIndex = 0;
    }
    private void ClearCheckboxDegMap()
    {
        CheckBox headchk = (lstDegree.FindControl("cbDegHead") as CheckBox);

        if (headchk.Checked)
        {
            headchk.Checked = false;
        }
        foreach (ListViewItem item in lstDegree.Items)
        {
            if (item.ItemType == ListViewItemType.DataItem)
            {
                foreach (Control ctr in item.Controls)
                {
                    var tt = ctr;
                    CheckBox chk = (CheckBox)ctr.FindControl("chkDegree");
                    //CheckBox chkHead = (CheckBox)ctr.FindControl("chkDegree");

                    if (chk.Checked)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
    }
    private void BindMappingViewDegMap(int depID)
    {
        try
        {
            DataSet ds = objMapp.GetDegMappingData(depID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lstDegreeMap.DataSource = ds;
                lstDegreeMap.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lstDegreeMap);//Set label -
                ClearCheckboxDegMap();
                //ddlCollegeIdDepMap.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                if (depID > 0)
                    {
                    foreach (ListViewDataItem item in lstDegree.Items)
                        {
                        CheckBox chkBox = (item.FindControl("chkDegree")) as CheckBox;
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                            if (ds.Tables[0].Rows[j]["DEGREENO"].ToString() == chkBox.ToolTip)
                                chkBox.Checked = true;
                            //chkBox.Checked = false;
                            }
                        }
                    }
            }
            else
            {
                lstDegreeMap.DataSource = null;
                lstDegreeMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.BindMappingViewDegMap-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlCollegeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeList.SelectedIndex > 0)
        {
            BindMappingViewDegMap(Convert.ToInt32(ddlCollegeList.SelectedValue));
        }
        else
        {
            BindMappingViewDegMap(0);
        }
    }

    private void BindListViewDegree()
    {
        try
        {
            DataSet ds = objMapp.GetDegreeInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lstDegree.DataSource = ds;
                lstDegree.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lstDegree);//Set label -
                hdnDegree.Value = lstDegree.Items.Count.ToString();
            }
            else
            {
                lstDegree.DataSource = null;
                lstDegree.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.BindListViewDeptMap-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
    private void ShowDetailsDegreeMapping(int id)
    {
        try
        {
            DataSet ds = null;
            ds = objMapp.GetDegMappingData(id);
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCollegeList.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    int i = 0;
                    foreach (ListViewDataItem item in lstDegree.Items)
                    {
                        CheckBox chkBox = (item.FindControl("chkDegree")) as CheckBox;
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (ds.Tables[0].Rows[j]["DEGREENO"].ToString() == chkBox.ToolTip)
                                chkBox.Checked = true;
                            //chkBox.Checked = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.ShowDetailsDegreeMapping-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Methods
    #endregion College_Degree_Mapping

    #region College_Degree_Branch_Mapping

    protected void ddlDegreeBrMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegreeBrMap.SelectedIndex > 0)
        {
            BindMappingViewBranchDeg(Convert.ToInt32(ddlCollegeBranchMap.SelectedValue), Convert.ToInt32(ddlDegreeBrMap.SelectedValue), 0);
        }
        else
        {
            BindMappingViewBranchDeg(Convert.ToInt32(ddlCollegeBranchMap.SelectedValue), 0, 0);
        }
    }

    private void BindListViewBranchMap()
    {
        try
        {
            DataSet ds = objMapp.GetBranchList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvBranchList.DataSource = ds;
                lvBranchList.DataBind();
                hdncountBr.Value = lvBranchList.Items.Count.ToString();
            }
            else
            {
                lvBranchList.DataSource = null;
                lvBranchList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.BindListViewBranchMap-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindMappingViewBranchDeg(int college_id, int degreeno, int degID)
    {
        try
        {
            DataSet ds = objMapp.GetMappedBranchData(college_id, degreeno, degID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvBranchMapping.DataSource = ds;
                lvBranchMapping.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBranchMapping);//Set label -
            }
            else
            {
                lvBranchMapping.DataSource = null;
                lvBranchMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.BindMappingViewBranchDeg-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnSubmitBranchMap_Click(object sender, EventArgs e)
    {
        try
        {
            string msg1 = "";
            string branchid = "";
            string branchname = "";
            objM.CollegeId = Convert.ToInt32(ddlCollegeBranchMap.SelectedValue);
            objM.College_Name = ddlCollegeBranchMap.SelectedItem.Text; //Added By Rishabh on 21/11/2021
            objM.DegreeDepartmentId = Convert.ToInt32(ddlDegreeBrMap.SelectedValue);
            objM.DepartmentId = Convert.ToInt32(ddlDeptBranchMap.SelectedValue);
            objM.BranchId = Convert.ToInt32(ddlBranchMap.SelectedValue);
            objM.Duration = txtDurationBranchMap.Text == string.Empty ? 0 : Convert.ToInt32(txtDurationBranchMap.Text);
            objM.ShortName = txtBranchShortName.Text == string.Empty ? string.Empty : txtBranchShortName.Text;
            objM.Branch_Code = txtBranchCode.Text == string.Empty ? string.Empty : txtBranchCode.Text;
            objM.College_Code = txtCollegeCodeBranchMap.Text == string.Empty ? string.Empty : txtCollegeCodeBranchMap.Text;
            objM.College_Type = ddlCollegeTypeBranchMap.SelectedIndex > 0 ? Convert.ToInt32(ddlCollegeTypeBranchMap.SelectedValue) : 0;
            objM.Programme_Type = ddlProgrammetypeBranchMap.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammetypeBranchMap.SelectedValue) : 0;
            objM.Intake_I = txtIntake1.Text == string.Empty ? 0 : Convert.ToInt32(txtIntake1.Text);
            objM.Intake_II = txtIntake2.Text == string.Empty ? 0 : Convert.ToInt32(txtIntake2.Text);
            objM.Intake_III = txtIntake3.Text == string.Empty ? 0 : Convert.ToInt32(txtIntake3.Text);
            objM.Intake_IV = txtIntake4.Text == string.Empty ? 0 : Convert.ToInt32(txtIntake4.Text);
            objM.Intake_V = txtIntake5.Text == string.Empty ? 0 : Convert.ToInt32(txtIntake5.Text);
            
            objM.CoreBranchNo = Convert.ToInt32(ddlCoreBranch.SelectedValue);
            int FacultyNo = Convert.ToInt32(ddlFcultyno.SelectedValue);
            if (chkEng.Checked)
                objM.Eng_Status = 1;
            else
                objM.Eng_Status = 0;

            if (chkIsSpecialisation.Checked)
                objM.IsSpecialisation = 1;
            else
                objM.IsSpecialisation = 0;

            string degreeName = ddlDegreeBrMap.SelectedItem.Text;

            //foreach (ListViewDataItem dataitem in lvBranchList.Items)
            //{
            //    CheckBox chkBox = (dataitem.FindControl("chkBranch")) as CheckBox;
            //    Label lblDepName = (dataitem.FindControl("lblBranch")) as Label;
            //    if (chkBox.Checked)
            //    {
            //        if (branchid == "")
            //        {
            //            branchid = chkBox.ToolTip;
            //        }
            //        else
            //        {
            //            branchid = branchid + "," + chkBox.ToolTip;
            //        }
            //        if (branchname == "")
            //        {
            //            branchname = degreeName + " - " + lblDepName.Text;
            //        }
            //        else
            //        {
            //            branchname = branchname + "," + degreeName + " - " + lblDepName.Text;
            //        }
            //    }
            //}
            //if (branchid != "")
            //{
            //TabName.Value = "tab_3";
            CustomStatus cs = (CustomStatus)objMapp.SaveDegreeBranchMapping(objM,FacultyNo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                msg1 = "Record Added Successfully.";
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                msg1 = "Record Updated Successfully.";
            }
            objM.DegreeDepartmentId = 0;
            //}
            objCommon.DisplayMessage(this.updDepartmentMapp, msg1, this.Page);
            BindMappingViewBranchDeg(Convert.ToInt32(ddlCollegeBranchMap.SelectedValue), Convert.ToInt32(ddlDegreeBrMap.SelectedValue), 0);
            // ClearCheckboxDBranchMapp();
            ClearAllFieldsBranchMapp();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.btnSubmitBranchMap_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnCancelBranchMap_Click(object sender, EventArgs e)
    {
        // ClearCheckboxDBranchMapp();
        ClearAllFieldsBranchMapp();
        BindMappingViewBranchDeg(0,0,0);

    }
    protected void btnEditBranchMap_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ClearCheckboxDBranchMapp();
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["DegBranchid"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionDegBranchid"] = "edit";
            this.ShowDetailsBranchMapp(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.btnEditBranchMap_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetailsBranchMapp(int id)
    {
        try
        {
            DataSet ds = null;
            ds = objMapp.GetMappedBranchData(0, 0, id);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlCollegeBranchMap.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                objCommon.FillDropDownList(ddlDegreeBrMap, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON D.DEGREENO =CD.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "D.ACTIVESTATUS=1 AND COLLEGE_ID=" + ddlCollegeBranchMap.SelectedValue, "D.DEGREENO");
                ddlDegreeBrMap.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                objCommon.FillDropDownList(ddlDeptBranchMap, "ACD_DEPARTMENT DP INNER JOIN  ACD_COLLEGE_DEPT CDP ON DP.DEPTNO=CDP.DEPTNO", "DISTINCT DP.DEPTNO", "DEPTNAME", "DP.ACTIVESTATUS=1 AND COLLEGE_ID=" + ddlCollegeBranchMap.SelectedValue, "DP.DEPTNO");
                ddlDeptBranchMap.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                ddlBranchMap.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ddlFcultyno.SelectedValue = ds.Tables[0].Rows[0]["FACULTY_DISCIPLINENO"].ToString();
                txtDurationBranchMap.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();
                txtBranchShortName.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                txtBranchCode.Text = ds.Tables[0].Rows[0]["BRANCH_CODE"].ToString();
                txtCollegeCodeBranchMap.Text = ds.Tables[0].Rows[0]["SCHOOL_COLLEGE_CODE"].ToString();
                ddlCollegeTypeBranchMap.SelectedValue = ds.Tables[0].Rows[0]["INSTITUTE_TYPE"].ToString();
                ddlProgrammetypeBranchMap.SelectedValue = ds.Tables[0].Rows[0]["UGPGOT"].ToString();
                txtIntake1.Text = ds.Tables[0].Rows[0]["INTAKE1"].ToString();
                txtIntake2.Text = ds.Tables[0].Rows[0]["INTAKE2"].ToString();
                txtIntake3.Text = ds.Tables[0].Rows[0]["INTAKE3"].ToString();
                txtIntake4.Text = ds.Tables[0].Rows[0]["INTAKE4"].ToString();
                txtIntake5.Text = ds.Tables[0].Rows[0]["INTAKE5"].ToString();
                objCommon.FillDropDownList(ddlCoreBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "ACTIVESTATUS=1 AND ISNULL(ISCORE,0)=0", "BRANCHNO");
                ddlCoreBranch.SelectedValue = ds.Tables[0].Rows[0]["COREBRANCHNO"].ToString();

                if (ds.Tables[0].Rows[0]["SPECIALISATION"].ToString() == "1")
                {
                    chkIsSpecialisation.Checked = true;
                    divcorebranch.Visible = true;
                }
                else
                {
                    chkIsSpecialisation.Checked = false;
                    divcorebranch.Visible = false;
                }

                if (ds.Tables[0].Rows[0]["ENG_STATUS"].ToString() == "1")
                    chkEng.Checked = true;
                else
                    chkEng.Checked = false;

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlDegreeBrMap.SelectedValue = ds.Tables[0].Rows[0]["DegreeDepartmentId"] == null ? string.Empty : ds.Tables[0].Rows[0]["DegreeDepartmentId"].ToString();
                //    int i = 0;
                //    foreach (ListViewDataItem item in lvBranchList.Items)
                //    {
                //        CheckBox chkBox = (item.FindControl("chkBranch")) as CheckBox;
                //        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                //        {
                //            if (ds.Tables[0].Rows[j]["BranchId"].ToString() == chkBox.ToolTip)
                //            {
                //                chkBox.Checked = true;
                //            }
                //            //else
                //            //{
                //            //    chkBox.Checked = false;
                //            //}
                //        }

                //    }
                //}

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_MappingCreation.ShowDetailsBranchMapp-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearAllFieldsBranchMapp()
    {
        ddlDegreeBrMap.SelectedIndex = 0;
        ddlCollegeBranchMap.SelectedIndex = -1;
        ddlCollegeTypeBranchMap.SelectedIndex = -1;
        ddlDeptBranchMap.SelectedIndex = -1;
        ddlBranchMap.SelectedIndex = -1;
        ddlProgrammetypeBranchMap.SelectedIndex = -1;
        ddlCoreBranch.SelectedIndex = -1;
        ddlFcultyno.SelectedIndex = -1;

        txtDurationBranchMap.Text = string.Empty;
        txtBranchCode.Text = string.Empty;
        txtBranchShortName.Text = string.Empty;
        txtCollegeCodeBranchMap.Text = string.Empty;
        txtIntake1.Text = string.Empty;
        txtIntake2.Text = string.Empty;
        txtIntake3.Text = string.Empty;
        txtIntake4.Text = string.Empty;
        txtIntake5.Text = string.Empty;
        chkEng.Checked = false;
        chkIsSpecialisation.Checked = false;
    }

    private void ClearCheckboxDBranchMapp()
    {
        CheckBox headchk = (lvBranchList.FindControl("cbBranchHead") as CheckBox);

        if (headchk.Checked)
        {
            headchk.Checked = false;
        }
        foreach (ListViewItem item in lvBranchList.Items)
        {
            if (item.ItemType == ListViewItemType.DataItem)
            {
                foreach (Control ctr in item.Controls)
                {
                    var tt = ctr;
                    CheckBox chk = (CheckBox)ctr.FindControl("chkBranch");
                    CheckBox chkHead = (CheckBox)ctr.FindControl("cbBranchHead");

                    if (chk.Checked)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
    }
    protected void ddlCollegeBranchMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegreeBrMap, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON D.DEGREENO =CD.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "D.ACTIVESTATUS=1 AND COLLEGE_ID=0" + ddlCollegeBranchMap.SelectedValue, "D.DEGREENO");
        objCommon.FillDropDownList(ddlDeptBranchMap, "ACD_DEPARTMENT DP INNER JOIN  ACD_COLLEGE_DEPT CDP ON DP.DEPTNO=CDP.DEPTNO", "DISTINCT DP.DEPTNO", "DEPTNAME", "DP.ACTIVESTATUS=1 AND COLLEGE_ID=0" + ddlCollegeBranchMap.SelectedValue, "DP.DEPTNO");
        BindMappingViewBranchDeg(Convert.ToInt32(ddlCollegeBranchMap.SelectedValue), Convert.ToInt32(ddlDegreeBrMap.SelectedValue), 0);
    }
    #endregion College_Degree_Branch_Mapping


    
    protected void chkIsSpecialisation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsSpecialisation.Checked == true)
        {
            divcorebranch.Visible = true;
        }
        else
        {
            divcorebranch.Visible = false;
        }
    }
}