using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;

using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;

public partial class Query_Manager : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QueryManagerController objQMC = new QueryManagerController();
    static int DepartmentID;
    static int RequestTypeID;
    static int RequestCategoryID;
    static int RequestSubCategoryID;
    static int UserAllocationId;


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
            ///Check Session
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

            }
            Clear();
            BindListView();
            BindUserListView();
            BindDropdown();
            ViewState["action"] = "add";

           


            //   ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat1(true);", true);
            //string FilePath = @"D:\Abhinay Data\Currently Working\SVN_SVCE\PresentationLayer\DATA_TABLE\jss\DataTableButtonSetting.js";
            //string text = System.IO.File.ReadAllText(FilePath);
            //ScriptManager.RegisterStartupScript(this, GetType(), "script", text, true);
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AchievementMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AchievementMaster.aspx");
        }
    }
    #endregion


    protected void btnSubmitServiceDept_Click(object sender, EventArgs e)
    {
        
        QueryManager objQM = new QueryManager();
        QueryManagerController objQMC = new QueryManagerController();
        try
        {
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                objQM.QMDepartmentName = txtDepartmentName.Text;
                objQM.QMDepartmentShortname = txtShortssName.Text;
                if (hdnchkSDept.Value == "true")
                {
                    objQM.IsActive = true;
                }
                else
                {
                    objQM.IsActive = false;
                }
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckDept();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objQMC.AddServiceDepartment(objQM);
                        if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Achievement Details!", this.Page);
                        }

                    } 
                }
                else
                {
                    bool result = CheckDept();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        objQM.QMDepartmentID = DepartmentID;
                        CustomStatus cs = (CustomStatus)objQMC.UpdateServiceDepartment(objQM);

                        //if (cs.Equals(CustomStatus.DuplicateRecord))
                        //{
                        //    objCommon.DisplayMessage("Record Already Exist", this.Page);
                        //}
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {

                            objCommon.DisplayMessage(this.UpdatePanel1, "Record Updated Successfully!", this.Page);
                            ViewState["action"] = "add";

                            Clear();
                            //ViewState["action"] = null;

                            btnSubmitServiceDept.Text = "Submit";
                            btnSubmitServiceDept.CssClass = "btn btn-outline-info";

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UpdatePanel1, "Something Went Wrong!", this.Page);
                        }
                    
                    
                    }
                }

                BindListView();
                BindUserListView();
                BindDropdown();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnCancelServiceDept_Click(object sender, EventArgs e)
    {
        Clear();
        btnSubmitServiceDept.Text = "Submit";
    }

    protected void btnEditServiceDept_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DepartmentID = int.Parse(btnEdit.CommandArgument);
            Clear();
            DataSet ds = objQMC.GetDataBYID(DepartmentID);

            DepartmentID = int.Parse(ds.Tables[0].Rows[0]["QMDepartmentID"].ToString());
            txtDepartmentName.Text = (ds.Tables[0].Rows[0]["QMDepartmentName"].ToString());
            txtShortssName.Text = (ds.Tables[0].Rows[0]["QMDepartmentShortname"].ToString());
            if ((ds.Tables[0].Rows[0]["IsActive"].ToString()) == "True")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat1(true);", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat1(false);", true);

            }
            
            ViewState["action"] = "edit";

            btnSubmitServiceDept.Text = "UPDATE";
            btnSubmitServiceDept.CssClass = "btn btn-outline-info";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSubmitRequestType_Click(object sender, EventArgs e)
    {
        QueryManager objQM = new QueryManager();
        QueryManagerController objQMC = new QueryManagerController();
        try
        {
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                objQM.QMDepartmentID =Convert.ToInt32(ddlServiceDepartment.SelectedValue);
                objQM.QMRequestTypeName = txtTypeName.Text;
          
                if (hdnchkSDept.Value == "true")
                {
                    objQM.IsActive = true;
                }
                else
                {
                    objQM.IsActive = false;
                }
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckRequestType();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.updtab_2, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objQMC.AddRequestType(objQM);
                        if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this.updtab_2, "Record Already Exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updtab_2, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updtab_2, "Error", this.Page);
                        }
                    }
                    
                }
                else
                {
                    bool result = CheckRequestType();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.updtab_2, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        objQM.QMRequestTypeID = RequestTypeID;
                        CustomStatus cs = (CustomStatus)objQMC.UpdateRequestType(objQM);

                       
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.updtab_2, "Record Updated Successfully!", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                            //ViewState["action"] = null;
                            btnSubmitRequestType.Text = "Submit";
                            btnSubmitRequestType.CssClass = "btn btn-outline-info";

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updtab_2, "Something Went Wrong!", this.Page);
                        }
                    }

                }

                BindListView();
                BindUserListView();
                BindDropdown();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancelRequestType_Click(object sender, EventArgs e)
    {
        Clear();

        // 12-04-2022
        ddlServiceDepartment.Items.Clear();
        ddlServiceDepartment.Items.Add("Please Select");
        objCommon.FillDropDownList(ddlServiceDepartment, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");    //22
        btnSubmitRequestType.Text = "Submit";
    }
    protected void btnEditRequestType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            RequestTypeID = int.Parse(btnEdit.CommandArgument);
            Clear();

            DataSet ds = objQMC.GetDataBYID(RequestTypeID);

            RequestTypeID = int.Parse(ds.Tables[1].Rows[0]["QMRequestTypeID"].ToString());
            ddlServiceDepartment.SelectedValue = (ds.Tables[1].Rows[0]["QMDepartmentID"].ToString());
            ddlServiceDepartment.Enabled = false;  //12-04-2022
            txtTypeName.Text = (ds.Tables[1].Rows[0]["QMRequestTypeName"].ToString());
            if ((ds.Tables[1].Rows[0]["IsActive"].ToString()) == "True")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat1(true);", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat1(false);", true);

            }
           
            ViewState["action"] = "edit";

            btnSubmitRequestType.Text = "UPDATE";
            btnSubmitRequestType.CssClass = "btn btn-outline-info";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //tab3
    protected void btnSubmitRequestCategory_Click(object sender, EventArgs e)
    {

        QueryManager objQM = new QueryManager();
        QueryManagerController objQMC = new QueryManagerController();
        try
        {
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                objQM.QMDepartmentID = Convert.ToInt32(ddlSDept.SelectedValue);
                objQM.QMRequestCategoryName = txtRequestCategory.Text;
                objQM.QMRequestTypeID = Convert.ToInt32(ddlRequestType.SelectedValue);
                
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckRequestCategory();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.updTab3, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objQMC.AddRequestCategory(objQM);
                        if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this.updTab3, "Record Already Exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updTab3, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updTab3, "Error Adding Achievement Details!", this.Page);
                        }
                    }
                }
                else
                {
                    bool result = CheckRequestCategory();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.updTab3, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        objQM.QMRequestCategoryID = RequestCategoryID;
                        CustomStatus cs = (CustomStatus)objQMC.UpdateRequestCategory(objQM);

                        //if (cs.Equals(CustomStatus.DuplicateRecord))
                        //{
                        //    objCommon.DisplayMessage("Record Already Exist", this.Page);
                        //}
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {

                            objCommon.DisplayMessage(this.updTab3, "Record Updated Successfully!", this.Page);

                            ViewState["action"] = "add";
                            Clear();
                            //ViewState["action"] = null;

                            btnSubmitRequestCategory.Text = "Submit";
                            btnSubmitRequestCategory.CssClass = "btn btn-outline-info";

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updTab3, "Something Went Wrong!", this.Page);
                        }
                    }

                }

                BindListView();
                BindUserListView();
                BindDropdown();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

   
    protected void btnCancelRequestCategory_Click(object sender, EventArgs e)
    {
        Clear();
        // 12-04-2022 cancel btn clear dropdown list this 2 line 
        ddlRequestType.Items.Clear();
        ddlRequestType.Items.Add("Please Select");
        btnSubmitRequestCategory.Text = "Submit";
    }

    protected void btnEditCategoryType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            RequestCategoryID = int.Parse(btnEdit.CommandArgument);
            Clear();
            DataSet ds = objQMC.GetDataBYID(RequestCategoryID);
            RequestCategoryID = int.Parse(ds.Tables[2].Rows[0]["QMRequestCategoryID"].ToString());
            ddlSDept.SelectedValue = ds.Tables[2].Rows[0]["QMDepartmentID"].ToString();
            objCommon.FillDropDownList(ddlRequestType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1 + " And QSD.QMDepartmentID=" + ds.Tables[2].Rows[0]["QMDepartmentID"].ToString(), "QMRequestTypeName Asc");
            ddlRequestType.SelectedValue = ds.Tables[2].Rows[0]["QMRequestTypeID"].ToString();
            txtRequestCategory.Text = (ds.Tables[2].Rows[0]["QMRequestCategoryName"].ToString());
            //12-04-2022
            ddlSDept.Enabled = false;
            ddlRequestType.Enabled = false;
            

            ViewState["action"] = "edit";

            btnSubmitRequestCategory.Text = "UPDATE";
            btnSubmitRequestCategory.CssClass = "btn btn-outline-info";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void ShowDetail(int ID)
    {
       
        DataSet ds = objQMC.GetDataBYID(ID);        			
        
        RequestSubCategoryID = int.Parse(ds.Tables[3].Rows[0]["QMRequestSubCategoryID"].ToString());
        objCommon.FillDropDownList(ddlDept, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
        ddlDept.SelectedValue = ds.Tables[3].Rows[0]["QMDepartmentID"].ToString();
        objCommon.FillDropDownList(ddlReqstType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1 + " And QSD.QMDepartmentID=" + ds.Tables[3].Rows[0]["QMDepartmentID"].ToString(), "QMRequestTypeName Asc");
        ddlReqstType.SelectedValue= ds.Tables[3].Rows[0]["QMRequestTypeID"].ToString();
       // objCommon.FillDropDownList(ddlRequestCategory, "QM_RequestCategory QRC INNER JOIN QM_ServiceDepartment QSD ON(QRC.QMDepartmentID = QSD.QMDepartmentID) INNER jOIN QM_RequestType QRT ON (QRT.QMDepartmentID=QSD.QMDepartmentID)", "QMRequestCategoryID", "QMRequestCategoryName", "QRC.QMDepartmentID=" + ds.Tables[3].Rows[0]["QMDepartmentID"].ToString() + " AND QRC.QMRequestTypeID=" + ds.Tables[3].Rows[0]["QMRequestTypeID"].ToString(), "QMRequestCategoryName Asc");
        //add 08-04-2022
        objCommon.FillDropDownList(ddlRequestCategory, "QM_RequestCategory QRC", "QMRequestCategoryID", "QMRequestCategoryName", "QRC.QMDepartmentID=" + ds.Tables[3].Rows[0]["QMDepartmentID"].ToString() + " AND QRC.QMRequestTypeID=" + ds.Tables[3].Rows[0]["QMRequestTypeID"].ToString(), "QMRequestCategoryName Asc");
        ddlRequestCategory.SelectedValue = ds.Tables[3].Rows[0]["QMRequestCategoryID"].ToString();
        txtSubCategory.Text = ds.Tables[3].Rows[0]["QMRequestSubCategoryName"].ToString();
        txtInstruction.Text= ds.Tables[3].Rows[0]["GeneralInstruction"].ToString();
        // 12-04-2022 edit for fixed 
        ddlDept.Enabled=false;
        ddlReqstType.Enabled=false;
        ddlRequestCategory.Enabled = false;

        if((ds.Tables[3].Rows[0]["IsPaidService"].ToString()) == "True")
        {
            paidservice.Visible = true;
            
            chkYes.Checked=true;
            txtAmount.Visible = true; // Amol sawarkar 17-03-2022
            txtAmount.Text=ds.Tables[3].Rows[0]["PaidServiceAmount"].ToString();
            
        }
       else
        {
            paidservice.Visible = false;
            chkYes.Checked=false;
        }
           
        if((ds.Tables[3].Rows[0]["IsEmergencyService"].ToString()) == "True")
        {
            emergencyservice.Visible = true;
            emergencyservice1.Visible = true;
            chkYess.Checked=true;
            txtAmt.Text  =ds.Tables[3].Rows[0]["EmergencyServiceAmount"].ToString();
            txtHours.Text =ds.Tables[3].Rows[0]["EmergencyServiceHours"].ToString();
        }
        else
        {
            emergencyservice.Visible = false;
            emergencyservice1.Visible = false;
            chkYess.Checked=false;
        }

    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objQMC.GETDATA();
            lvServiceDept.DataSource = ds.Tables[0];
            lvServiceDept.DataBind();

            for (int i = 0; i < lvServiceDept.Items.Count; i++)
            {
                Label lblStatus = lvServiceDept.Items[i].FindControl("lablstatus") as Label;
                if (ds.Tables[0].Rows[i]["IsActive"].ToString() == "True")
                {
                    lblStatus.Text = "Active";
                    lblStatus.CssClass = "badge badge-success";
                    
                }
                else
                {
                    lblStatus.Text = "In-Active";
                    lblStatus.CssClass = "badge badge-danger";
                }
            }


            lvRequestType.DataSource = ds.Tables[1];
            lvRequestType.DataBind();

            for (int i = 0; i < lvRequestType.Items.Count; i++)
            {
                Label lblStatus = lvRequestType.Items[i].FindControl("lablstatus") as Label;
                if (ds.Tables[1].Rows[i]["IsActive"].ToString() == "True")
                {
                    lblStatus.Text = "Active";
                    lblStatus.CssClass = "badge badge-success";

                }
                else
                {
                    lblStatus.Text = "In-Active";
                    lblStatus.CssClass = "badge badge-danger";
                }
            }

            lvRequestCategory.DataSource = ds.Tables[2];
            lvRequestCategory.DataBind();

            lvsubcategory.DataSource = ds.Tables[3];
            lvsubcategory.DataBind();

        
        }
        catch (Exception ex)
        {

        }
    }
    private void BindUserListView()
    {
        try
        {
            DataSet ds = objQMC.GETUSERDATA();

            lvUser.DataSource = ds.Tables[0];
            lvUser.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    
    private void BindDropdown()
    {
        objCommon.FillDropDownList(ddlServiceDepartment, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
        objCommon.FillDropDownList(ddlSDept, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
        objCommon.FillDropDownList(ddlDept, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
        objCommon.FillDropDownList(ddlServiceDepart, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
        objCommon.FillDropDownList(ddlIncharge, "user_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2", "UA_FULLNAME Asc");
       
        
    }

    private void Clear()
    {

        txtDepartmentName.Text = string.Empty;
        txtShortssName.Text = string.Empty;
        txtAmt.Text = string.Empty;
        txtRequestCategory.Text = string.Empty;
        txtInstruction.Text = string.Empty;
        txtSubCategory.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtHours.Text = string.Empty;
        txtTypeName.Text = string.Empty;
        ddlDept.SelectedIndex = 0;
        //
        ddlIncharge.SelectedIndex = 0;
        ddlReqstType.SelectedIndex = 0;
        ddlRequestCategory.SelectedIndex = 0;
        ddlRequestType.SelectedIndex = 0;
        ddlServiceDepartment.SelectedIndex = 0;
        ddlServiceDepart.SelectedIndex = 0;
        ddlSDept.SelectedIndex = 0;
        //
        //ddlIncharge.SelectedValue = "0";
        //ddlReqstType.SelectedValue = "0";
        //ddlRequestCategory.SelectedValue = "0";
        //ddlRequestType.SelectedValue = "0";
        //ddlServiceDepartment.SelectedValue="0";
        //ddlServiceDepart.SelectedValue="0";
        //ddlSDept.SelectedValue = "0";
        //
        lstbxAddTeam.SelectedIndex = -1;
        lstbxRequestType.SelectedIndex = -1;
        chkYes.Checked = false;
        chkYess.Checked = false;
        ViewState["action"] = "add";
        objCommon.FillDropDownList(ddlReqstType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1 + " And QSD.QMDepartmentID=" + ddlDept.SelectedValue, "QMRequestTypeName Asc");
        //if (ddlDept.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlReqstType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1 + " And QSD.QMDepartmentID=" + ddlDept.SelectedValue, "QMRequestTypeName Asc");
        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlReqstType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1 , "QMRequestTypeName Asc");
        //}
            ddlServiceDepartment.Enabled = true;
        ddlSDept.Enabled = true;
        ddlRequestType.Enabled = true;
        ddlDept.Enabled = true;
        ddlReqstType.Enabled = true;
        ddlRequestCategory.Enabled = true;
        ddlServiceDepart.Enabled = true;
        //ddlIncharge.Enabled = true;
        //lstbxRequestType.Enabled = true;
        lstbxRequestType.Attributes.Remove("disabled");

    }

   
protected void ddlSDept_SelectedIndexChanged(object sender, EventArgs e)
{
    //objCommon.FillDropDownList(ddlRequestType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1+" And QSD.QMDepartmentID="+ddlSDept.SelectedValue, "QMRequestTypeName Asc");
    objCommon.FillDropDownList(ddlRequestType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QRT.IsActive=" + 1 + " And QSD.QMDepartmentID=" + ddlSDept.SelectedValue, "QMRequestTypeName Asc");
}


protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
{
    objCommon.FillDropDownList(ddlReqstType, "QM_RequestType QRT INNER JOIN QM_ServiceDepartment QSD ON(QRT.QMDepartmentID = QSD.QMDepartmentID) ", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND QSD.IsActive=" + 1 + " And QSD.QMDepartmentID=" + ddlDept.SelectedValue, "QMRequestTypeName Asc");
   
}


protected void ddlReqstType_SelectedIndexChanged(object sender, EventArgs e)
{
    objCommon.FillDropDownList(ddlRequestCategory, "QM_RequestCategory ", "QMRequestCategoryID", "QMRequestCategoryName", "QMDepartmentID=" + ddlDept.SelectedValue + " AND QMRequestTypeID=" + ddlReqstType.SelectedValue, "QMRequestCategoryName Asc");
    //QRC INNER JOIN QM_ServiceDepartment QSD ON(QRC.QMDepartmentID = QSD.QMDepartmentID) INNER jOIN QM_RequestType QRT ON (QRT.QMDepartmentID=QSD.QMDepartmentID)
}





protected void btnSubmitsubcategory_Click(object sender, EventArgs e)
{
    QueryManager objQM = new QueryManager();
        QueryManagerController objQMC = new QueryManagerController();
        try
        {
            //Check whether to add or update
            if (ViewState["action"] != null)
            {

                objQM.QMRequestSubCategoryName  = txtSubCategory.Text;
                objQM.QMRequestTypeID           = Convert.ToInt32(ddlReqstType.SelectedValue) ;
                objQM.QMRequestCategoryID       = Convert.ToInt32(ddlRequestCategory.SelectedValue) ;
                objQM.GeneralInstruction        = txtInstruction.Text;
                if(chkYes.Checked==true)
                {
                    objQM.IsPaidService = true;
                    objQM.PaidServiceAmount= double.Parse(txtAmount.Text);
                }
                else
                {
                    objQM.IsPaidService =false;                    
                }

                if(chkYess.Checked ==true)
                {
                    objQM.IsEmergencyService = true;
                    objQM.EmergencyServiceAmount = double.Parse(txtAmt.Text);
                    objQM.EmergencyServiceHours = Convert.ToInt32(txtHours.Text);
                }
                else
                {
                    objQM.IsEmergencyService = false;                    
                }


                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckRequestSubCategory();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.updtab_4, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objQMC.AddRequestSubCategory(objQM);
                        if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this.updtab_4, "Record Already Exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                           
                            
                            // Amol sawarkar 
                            paidservice.Visible = false;
                            chkYes.Checked = false;
                            emergencyservice.Visible = false;
                            emergencyservice1.Visible = false;
                            chkYess.Checked = false;
                            objCommon.DisplayMessage(this.updtab_4, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updtab_4, "Error Adding Achievement Details!", this.Page);
                        }
                    }
                }
                else
                {
                    bool result = CheckRequestSubCategory();
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        objCommon.DisplayMessage(this.updtab_4, "Record Already Exist", this.Page);
                        return;
                    }
                    else
                    {
                        objQM.QMRequestSubCategoryID = RequestSubCategoryID;
                        CustomStatus cs = (CustomStatus)objQMC.UpdateRequestSubCategory(objQM);

                        //if (cs.Equals(CustomStatus.DuplicateRecord))
                        //{
                        //    objCommon.DisplayMessage("Record Already Exist", this.Page);
                        //}
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {

                            objCommon.DisplayMessage(this.updtab_4, "Record Updated Successfully!", this.Page);

                            ViewState["action"] = "add";
                            Clear();
                            //ViewState["action"] = null;
                          
                            
                            // Amol sawarkar 09-03-2022
                            paidservice.Visible = false;
                            chkYes.Checked = false;
                            emergencyservice.Visible = false;
                            emergencyservice1.Visible = false;
                            chkYess.Checked = false;


                            btnSubmitsubcategory.Text = "Submit";
                            btnSubmitsubcategory.CssClass = "btn btn-outline-info";

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updtab_4, "Something Went Wrong!", this.Page);
                        }

                    }
                }

                BindListView();
                BindUserListView();
                BindDropdown();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
}
protected void btnCancelsubcategory_Click(object sender, EventArgs e)
{
    Clear();

    ddlReqstType.Items.Clear();
    ddlReqstType.Items.Add("Please Select");
    ddlRequestCategory.Items.Clear();
    ddlRequestCategory.Items.Add("Please Select");
    btnSubmitsubcategory.Text = "Submit";
}
protected void btnEditsubCategory_Click(object sender, ImageClickEventArgs e)
{
    try
    {
        ImageButton btnEdit = sender as ImageButton;
        RequestSubCategoryID = int.Parse(btnEdit.CommandArgument);
        Clear();
        ShowDetail(RequestSubCategoryID);

        ViewState["action"] = "edit";

        btnSubmitsubcategory.Text = "UPDATE";
        btnSubmitsubcategory.CssClass = "btn btn-outline-info";


       
    }
    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server UnAvailable");
    }

}
protected void chkYess_CheckedChanged(object sender, EventArgs e)
{
    if (chkYess.Checked == true)
    {
        emergencyservice.Visible = true;
        emergencyservice1.Visible = true;
    }
    else
    {
        emergencyservice.Visible = false;
        emergencyservice1.Visible = false;
    }
        


}
protected void chkYes_CheckedChanged(object sender, EventArgs e)
{
    if (chkYes.Checked == true)
    {
        paidservice.Visible = true;
        
    }
    else
        paidservice.Visible = false;

}


protected void btnSubmitUser_Click(object sender, EventArgs e)
{
    QueryManager objQM = new QueryManager();
    QueryManagerController objQMC = new QueryManagerController();
    try
    {
        //Check whether to add or update
        if (ViewState["action"] != null)
        {
          //  string values = ddlRequestType.SelectedValue.ToString();

            objQM.QMDepartmentID = Convert.ToInt32(ddlServiceDepart.SelectedValue);
            objQM.InchargeID = Convert.ToInt32(ddlIncharge.SelectedValue);
            string Member = "";
            for (int i = 0; i < lstbxAddTeam.Items.Count; i++)
            {
                if (lstbxAddTeam.Items[i].Selected)
                {
                    Member += lstbxAddTeam.Items[i].Value + ",";
                    //Leavevalue = Leavevalue + "," + chkleave.Items[i].Value;                   
                }
            }
            objQM.MemberID = Member.TrimEnd(',');

            string reqtype = "";
            for (int i = 0; i < lstbxRequestType.Items.Count; i++)
            {
                if (lstbxRequestType.Items[i].Selected)
                {
                    reqtype += lstbxRequestType.Items[i].Value + ",";
                    //Leavevalue = Leavevalue + "," + chkleave.Items[i].Value;                   
                }
            }
            objQM.RequestType = reqtype.TrimEnd(',');

            if (ViewState["action"].ToString().Equals("add"))
            {
                //bool result = CheckUser();
                //if (result == true)
                //{
                //    //objCommon.DisplayMessage("Record Already Exist", this);
                //    objCommon.DisplayMessage(this.updtab_5, "Record Already Exist", this.Page);
                //    return;
                //}
                //else
                //{
                    //Add Batch
                    CustomStatus cs = (CustomStatus)objQMC.AddUser(objQM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.updtab_5, "Record Already Exist", this.Page);
                        return;
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updtab_5, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updtab_5, "Error Adding Achievement Details!", this.Page);
                    }
               // }
            }
            else
            {
                //bool result = CheckUser();
                //if (result == true)
                //{
                //    //objCommon.DisplayMessage("Record Already Exist", this);
                //    objCommon.DisplayMessage(this.updtab_5, "Record Already Exist", this.Page);
                //    return;
                //}
                //else
                //{
                    objQM.QMUserAllocationID = UserAllocationId;
                    CustomStatus cs = (CustomStatus)objQMC.UpdateUser(objQM);

                    //if (cs.Equals(CustomStatus.DuplicateRecord))
                    //{
                    //    objCommon.DisplayMessage("Record Already Exist", this.Page);
                    //}
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage(this.updtab_5, "Record Updated Successfully!", this.Page);

                        ViewState["action"] = "add";
                        Clear();
                        //ViewState["action"] = null;

                        btnSubmitUser.Text = "Submit";
                        btnSubmitUser.CssClass = "btn btn-outline-info";

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updtab_5, "Something Went Wrong!", this.Page);
                    }
                //}
            }

            BindUserListView();

        }

    }
    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server UnAvailable");
    }

}
protected void btnCancelUser_Click(object sender, EventArgs e)
{
    Clear();
    btnSubmitUser.Text = "Submit";
    //12-04-2022 4line
    ddlIncharge.Items.Clear();
    objCommon.FillDropDownList(ddlIncharge, "user_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2", "UA_FULLNAME Asc");
    //ddlIncharge.Items.Add("Please Select");
    lstbxRequestType.SelectionMode = ListSelectionMode.Multiple;
    //BindUserListView();
    lstbxRequestType.Items.Clear();
}
protected void ddlServiceDepart_SelectedIndexChanged(object sender, EventArgs e)
{
    //add 2 line for clear 
    lstbxRequestType.Items.Clear();
    lstbxRequestType.SelectedIndex = -1;
    if (ddlServiceDepart.SelectedIndex > 0)
    {
        BindQM_RequestType(ddlServiceDepart.SelectedValue);
    }
}

protected void BindQM_RequestType( string ServiceDepartmentId)
{
    // add one line clear 
    lstbxRequestType.Items.Clear();
    DataSet ds = objCommon.FillDropDown("QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive=" + 1 + " And QMDepartmentID=" + ServiceDepartmentId, "QMRequestTypeName Asc");
    lstbxRequestType.DataSource = ds;
    lstbxRequestType.DataTextField = "QMRequestTypeName";
    lstbxRequestType.DataValueField = "QMRequestTypeID";
    lstbxRequestType.DataBind();  
}

protected void ddlIncharge_SelectedIndexChanged(object sender, EventArgs e)
{
    if (ddlIncharge.SelectedIndex > 0)
    {
        BindInchargeTeam(ddlIncharge.SelectedValue);
    }    
}

private void BindInchargeTeam( string InchargeId)
{
    DataSet ds = objCommon.FillDropDown("user_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO <>" + InchargeId, "UA_FULLNAME Asc");
    lstbxAddTeam.DataSource = ds;
    lstbxAddTeam.DataTextField = "UA_FULLNAME";
    lstbxAddTeam.DataValueField = "UA_NO";
    lstbxAddTeam.DataBind();
}
protected void btnEditUserAllocation_Click(object sender, ImageClickEventArgs e)
{
    try
    {
        ImageButton btnEdit = sender as ImageButton;
        UserAllocationId = int.Parse(btnEdit.CommandArgument);
        Clear();
        //ShowDetail(UserAllocationId);
        DataSet ds = objQMC.GetDataBYID(UserAllocationId);

        ddlServiceDepart.SelectedValue = ds.Tables[4].Rows[0]["QMDepartmentID"].ToString();
        ddlIncharge.SelectedValue = ds.Tables[4].Rows[0]["InchargeID"].ToString();
        BindQM_RequestType(ds.Tables[4].Rows[0]["QMDepartmentID"].ToString());

        // add 12-04-2021
        ddlServiceDepart.Enabled = false;
        //ddlIncharge.Enabled = false;
        //lstbxRequestType.di = false;
        lstbxRequestType.Attributes.Add("disabled", "disabled");

        BindInchargeTeam(ds.Tables[4].Rows[0]["InchargeID"].ToString());

        string[] reqtype = Convert.ToString(ds.Tables[4].Rows[0]["QMRequestTypeID"]).Split(',');

        foreach (string s in reqtype)
        {
            foreach (ListItem item in lstbxRequestType.Items)
            {
                if (s == item.Value)
                {
                    item.Selected = true;
                   // lstbxRequestType.SelectionMode = ListSelectionMode.Single;
                    break;
                }
            }
        }

        string[] Member = Convert.ToString(ds.Tables[4].Rows[0]["MemberID"]).Split(',');

        foreach (string s in Member)
        {
            foreach (ListItem item in lstbxAddTeam.Items)
            {
                if (s == item.Value)
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        ViewState["action"] = "edit";

        btnSubmitUser.Text = "UPDATE";
        btnSubmitUser.CssClass = "btn btn-outline-info";

    }
    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server UnAvailable");
    }
}



public bool CheckDept()
{
    bool result = false;
    DataSet ds = new DataSet();

    // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
    ds = objCommon.FillDropDown("QM_ServiceDepartment", "*", "", "QMDepartmentID <>" + DepartmentID +"AND (QMDepartmentName='" + txtDepartmentName.Text + "' OR QMDepartmentShortname='" + txtShortssName.Text + "')", "");
    if (ds.Tables[0].Rows.Count >0)
    {
        result = true;

    }
    return result;
}

public bool CheckRequestType()
{
    bool result = false;
    DataSet ds = new DataSet();

    // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");

    ds = objCommon.FillDropDown("QM_RequestType", "*", "", "QMRequestTypeID <>" + RequestTypeID + "AND QMDepartmentID='" + ddlServiceDepartment.SelectedValue + "' AND QMRequestTypeName='" + txtTypeName.Text + "'", "");
    if (ds.Tables[0].Rows.Count > 0)
    {
        result = true;

    }
    return result;
}


public bool CheckRequestCategory()
{
    bool result = false;
    DataSet ds = new DataSet();

    // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");

   ds = objCommon.FillDropDown("QM_RequestCategory", "*", "", "QMRequestCategoryID <>" + RequestCategoryID + "AND QMDepartmentID='" + ddlSDept.SelectedValue +"'AND QMRequestTypeID='" + ddlRequestType.SelectedValue + "' AND QMRequestCategoryName='" + txtRequestCategory.Text + "'", "");
   
    if (ds.Tables[0].Rows.Count > 0)
    {
        result = true;

    }
    return result;
}


public bool CheckRequestSubCategory()
{
    bool result = false;
    DataSet ds = new DataSet();

    // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");

    //QMRequestSubCategoryID <>" + RequestSubCategoryID + "AND 
    ds = objCommon.FillDropDown("QM_RequestSubCategory", "*", "", "QMRequestSubCategoryID <>" + RequestSubCategoryID + "AND QMRequestTypeID='" + ddlReqstType.SelectedValue + "'AND QMRequestCategoryID='" + ddlRequestCategory.SelectedValue + "' AND QMRequestSubCategoryName='" + txtSubCategory.Text + "'", "");
    if (ds.Tables[0].Rows.Count > 0)
    {
        result = true;

    }
    return result;
}

public bool CheckUser()
{
    bool result = false;
    DataSet ds = new DataSet();

    // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");

   ds = objCommon.FillDropDown("QM_UserAllocation", "*", "", "QMUserAllocationID <>" + UserAllocationId + "AND QMRequestTypeID='" + lstbxRequestType + "' AND QMDepartmentID='" + ddlIncharge.SelectedValue + "'", "");
  //  ds = objCommon.FillDropDown("QM_UserAllocation", "*", "", "QMDepartmentID='" + ddlSDept.SelectedValue + "'AND QMRequestTypeID in (SELECT VALUE FROM string.split('" + ddlRequestType.SelectedValue + "'))", "");
    if (ds.Tables[0].Rows.Count > 0)
    {
        result = true;

    }
    return result;
}



}