using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;


public partial class ACADEMIC_SubModuleMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
            }
            //ddlModule.SelectedValue = "0";
            PopulatUserRightsList();
            PopulateDropDownList();
            ViewState["SMID"] = null;
            ViewState["action"] = null;
            //BindListView();
            //BindAll();
        }
        //BindAll();
        //BindListView();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SubModuleMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SubModuleMaster.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlModule, "ACC_SECTION", "AS_NO", "AS_TITLE", "AS_NO > 0", "AS_NO");         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SubModuleMaster.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");

            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int MID = 0, SMID = 0;
            string submodule = string.Empty;

            MID = Convert.ToInt32(ddlModule.SelectedValue);
            submodule = txtSubModule.Text;

            int count = 0;
            string user_types = string.Empty;
            for (int i = 0; i < chkUserRightsList.Items.Count; i++)
            {
                if (chkUserRightsList.Items[i].Selected)
                {
                    user_types += chkUserRightsList.Items[i].Value + ",";
                    count++;
                }
            }
            user_types = user_types.TrimEnd(',');

            //if (count == 0)
            if (user_types == "" ||ddlModule.SelectedIndex == 0 || txtSubModule.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updDetails, "Please Select Module, Sub-Module, atleast one user type !", this.Page);
                return;
            }
           
            string SM_ID = "-100";
            SM_ID = objCommon.LookUp("ACD_SUBMODULE_MASTER", "SMID", "MID=" + Convert.ToInt32(ddlModule.SelectedValue) +" AND SUB_MODULE_NAME = '"+ txtSubModule.Text +"'");

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].Equals("edit"))
                {
                    SMID = Convert.ToInt32(ViewState["SMID"].ToString());

                    CustomStatus cs = (CustomStatus)objSC.UpdateSubModuleDetails(SMID,MID,submodule,user_types);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updDetails, "Sub-Module Updated Successfully !", this.Page);
                        BindListView();
                        Clear();
                    }
                    ViewState["action"] = null;
                    ViewState["SMID"] = null;
                }
            }
            else
            {
                if (SM_ID == "")
                {
                    int subID = objSC.InsertSubModuleDetails(MID, submodule, user_types);
                    if (subID != -99)
                    {
                        objCommon.DisplayMessage(this.updDetails, "Sub-Module Created Successfully !", this.Page);
                        ViewState["SMID"] = subID.ToString();
                        BindListView();
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updDetails, "Failed to Save !", this.Page);
                    }
                }
                else
                    objCommon.DisplayMessage(this.updDetails, "Already Exists!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_submodulemaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulatUserRightsList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID<>0", "USERTYPEID");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkUserRightsList.DataTextField = "USERDESC";
                    chkUserRightsList.DataValueField = "USERTYPEID";
                    chkUserRightsList.ToolTip = "USERTYPEID";
                    chkUserRightsList.DataSource = ds.Tables[0];
                    chkUserRightsList.DataBind();
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

    public void BindListView()
    {
        try
        {
            DataSet ds = null;
            ds = objSC.GetSubModuleDetails(Convert.ToInt32(ddlModule.SelectedValue));
            //value = ddlModule.SelectedValue;
            //ds = objSC.GetSubModuleDetails(Convert.ToInt32("0"));

            if(ds!=null && ds.Tables[0].Rows.Count>0)
            {
                lvDetails.Visible = true;
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
            }
            else
            {
                lvDetails.DataSource = null;
                lvDetails.DataBind();
            }
        }
        catch(Exception ex)
        {
             if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_submodulemaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindAll()
    {
        try
        {
            DataSet ds = null;
            ds = objSC.GetAllSubModuleDetails();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDetails.Visible = true;
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
            }
            else
            {
                lvDetails.DataSource = null;
                lvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_submodulemaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void Clear()
    {
        ddlModule.SelectedIndex = 0;
        txtSubModule.Text = string.Empty;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowDetail(int smid)
    {
        SqlDataReader dr = objSC.GetSingleSubModuleDetails(smid);
        string UserTypes = string.Empty;
        char delimiterChars = ',';

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["SMID"] = smid.ToString();
                ddlModule.SelectedValue = dr["MID"] == DBNull.Value ? "0" : dr["MID"].ToString();
                txtSubModule.Text = dr["SUB_MODULE_NAME"] == DBNull.Value ? string.Empty : dr["SUB_MODULE_NAME"].ToString();
                PopulatUserRightsList();
                UserTypes = dr["User_Types"] == DBNull.Value ? string.Empty : dr["User_Types"].ToString(); 
                if (UserTypes != "")
                {
                    string[] utype = UserTypes.Split(delimiterChars);
                    for (int j = 0; j < utype.Length; j++)
                    {
                        for (int i = 0; i < chkUserRightsList.Items.Count; i++)
                        {
                            if (utype[j] == chkUserRightsList.Items[i].Value)
                            {
                                chkUserRightsList.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = sender as ImageButton;
        int smid = int.Parse(img.CommandArgument);
        ViewState["SMID"] = smid;
        ShowDetail(smid);
        ViewState["action"] = "edit";
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSubModule.Text = string.Empty;
        chkUserRights.Checked = false;
        chkUserRightsList.ClearSelection();
        BindListView();
    }
}