//============================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : VIDISHA KAMATKAR
// CREATED DATE  : 19-APR-2021
// DESCRIPTION   : USE TO CREATE CATEGORY NAMES.
// MODIFY BY     : MRUNAL SINGH
// MODIFY DATE   : 22-07-2021
// DESCRRIPTION  : TO DEFINE COLLEGEWISE SESSIONWISE CATEGORY FOR TRANSPORT FEES.
//==============================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Master_CategoryMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    BindlistView();
                    objVM.IPADDRESS = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (objVM.IPADDRESS == null || objVM.IPADDRESS == "")
                        objVM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    objVM.MACADDRESS = nics[0].GetPhysicalAddress().ToString();
                  
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleTypeMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    private void BindlistView()
    {
        try
        {
           // DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER	C INNER JOIN ACD_COLLEGE_MASTER CM ON (C.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_SESSION_MASTER SM ON (C.SESSIONNO = SM.SESSIONNO)", "C.VCID, C.CATEGORYNAME, C.AMOUNT", "CM.COLLEGE_NAME, SM.SESSION_NAME", "C.IsActive=1 And C.EndTime='9999-12-31'", "C.VCID");
           // DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER", "VCID, CATEGORYNAME", "AMOUNT", "IsActive=1 And EndTime='9999-12-31'", "VCID");
            DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER", "VCID, CATEGORYNAME", "AMOUNT", "", "VCID");
           
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvVCategory.DataSource = ds;
                lvVCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_CategoryMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.CATEGORYNAME = txtCategoryName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtCategoryName.Text.Trim());
            objVM.AMOUNT = txtAmount.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtAmount.Text.Trim());
            objVM.UANO = Convert.ToInt32(Session["userno"]);
            objVM.ModifiedDate = DateTime.Now;
            objVM.CollegeNo = 0; 
            objVM.COLLEGE_ID =  Convert.ToInt32(ddlCollege.SelectedValue);
            objVM.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
           
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objVM.IsActive = 1;
                    DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER", "VCID", "CategoryName", "CategoryName='"+txtCategoryName.Text+"'", "");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateVCMaster(objVM);
                        //if (cs.Equals(CustomStatus.RecordExist))
                        //{
                        //    Clear();
                        //    objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        //    return;
                        //}
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                           return;
                    }
                }
                else
                {
                    if (ViewState["VCID"] != null)
                    {
                        objVM.IsActive = 1;
                        objVM.VCID = Convert.ToInt32(ViewState["VCID"].ToString());

                        DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER", "VCID", "CategoryName", "CategoryName='" + txtCategoryName.Text + "' and VCID!= '" + Convert.ToInt32(ViewState["VCID"].ToString()) + "'", "");
                        if (ds.Tables[0].Rows.Count == 0)
                        {

                            CustomStatus cs = (CustomStatus)objVMC.AddUpdateVCMaster(objVM);
                            //if (cs.Equals(CustomStatus.RecordExist))
                            //{
                            //    Clear();
                            //    objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            //    return;
                            //}
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";

                                objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                                Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                                return;
                        }

                    }
                }
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_CategoryMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int VCID = int.Parse(btnEdit.CommandArgument);
            ViewState["VCID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(VCID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_CategoryMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int VCID)
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER", "*", "", "IsActive=1 And EndTime='9999-12-31' AND VCID=" + VCID, "VCID");
            DataSet ds = objCommon.FillDropDown("VEHICLE_CATEGORYMASTER", "*", "", " VCID=" + VCID, "VCID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCategoryName.Text = ds.Tables[0].Rows[0]["CATEGORYNAME"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
               // ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO");
            
                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_CategoryMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtCategoryName.Text = string.Empty;
        txtAmount.Text = string.Empty;       
        ViewState["VCID"] = null;
        ViewState["action"] = "add";
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO");
            ddlSession.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_CategoryMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}