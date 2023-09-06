//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Dept_User.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class Stores_Masters_Dept_User : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();
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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDept();
                this.FillApprovalLevel();
                this.FillUser();
                BindListViewDeptUser();
                ViewState["action"] = "add";
                //Set Report Parameters
               // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Department_User_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    private void BindListViewDeptUser()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllDeptUser(Convert.ToInt32(ddlDepartment.SelectedValue));
            lvDeptUser.DataSource = ds;
            lvDeptUser.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
        
                int ApprovalLevel = 0;
            
                //int duplicateCkeck1 = Convert.ToInt32(objCommon.LookUp("store_approval_level", "count(*)", "aplno=1"));
                //if (duplicateCkeck1 == 1)
                //{
                //    objCommon.DisplayMessage(UpdatePanel1, "You Can Not Create More Than One Central Store User.", this);
                //    return;
                //}
            
                if (ddlApprovalLevel.SelectedValue != "0")
                    ApprovalLevel = Convert.ToInt32(ddlApprovalLevel.SelectedValue);

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        
                        //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("store_departmentuser", " count(*)", "mdno=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "or aplno=" + Convert.ToInt32(ddlApprovalLevel.SelectedValue) + " and ua_no=" + Convert.ToInt32(ddlUser.SelectedValue)));
                        //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("store_departmentuser", " count(*)", "mdno=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and aplno=" + Convert.ToInt32(ddlApprovalLevel.SelectedValue) + " and ua_no=" + Convert.ToInt32(ddlUser.SelectedValue)));

                     // if (duplicateCkeck == 0)
                     // {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("store_departmentuser", " count(*)", "mdno=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and aplno=" + Convert.ToInt32(ddlApprovalLevel.SelectedValue) + " and ua_no=" + Convert.ToInt32(ddlUser.SelectedValue)));         //10/05/2022
                        if (duplicateCkeck == 0)
                        {
                            CustomStatus cs = (CustomStatus)objStrMaster.AddDeptUser(Convert.ToInt32(ddlDepartment.SelectedValue), ddlUser.SelectedItem.ToString(), Convert.ToInt32(ddlUser.SelectedValue), ApprovalLevel, Session["colcode"].ToString(), Session["userfullname"].ToString(), Convert.ToInt32(rdoBtnListIsApproval.SelectedValue));
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                                this.BindListViewDeptUser();
                                this.Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist", this);

                        }
                      //}
                      //else
                      //{
                      //    objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist", this);
                      
                      //}
                    }
                    else
                    {
                       
                       // int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("store_departmentuser", " count(*)", "mdno=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and ua_no=" + Convert.ToInt32(ddlUser.SelectedValue) + " and aplno=" + Convert.ToInt32(ddlApprovalLevel.SelectedValue) + " and duno <> " + Convert.ToInt32(ViewState["dpvNo"].ToString())));
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("store_departmentuser", " count(*)", "mdno=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and ua_no=" + Convert.ToInt32(ddlUser.SelectedValue) + " and aplno=" + Convert.ToInt32(ddlApprovalLevel.SelectedValue) + " and duno <> " + Convert.ToInt32(ViewState["dpvNo"].ToString())));
                        if (duplicateCkeck == 0)
                        {

                            CustomStatus cs = (CustomStatus)objStrMaster.UpdateDeptUser(Convert.ToInt32(ddlDepartment.SelectedValue), ddlUser.SelectedItem.ToString(), Convert.ToInt32(ddlUser.SelectedValue), ApprovalLevel, Session["colcode"].ToString(), Convert.ToInt32(ViewState["dpvNo"].ToString()), Session["userfullname"].ToString(), Convert.ToInt32(rdoBtnListIsApproval.SelectedValue));
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                                this.BindListViewDeptUser();
                                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Succesfully", this);
                                this.Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record Already Exists", this);
                        }
                        
                    }

                }
            }
        
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["dpvNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsDeptUser(Convert.ToInt32(ViewState["dpvNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowEditDetailsDeptUser(int dpuNo)
    {
        DataSet ds = null;

        try
        {
            //ds = objStrMaster.GetSingleRecordDeptUser(dpuNo);
            ds = objCommon.FillDropDown("STORE_DEPARTMENTUSER A INNER JOIN STORE_DEPARTMENT B ON (A.MDNO= B.MDNO )", "A.MDNO,MDNAME,UA_NO,APLNO,A.COLLEGE_CODE,A.ISAPPROVAL", "DUNO", "DUNO=" + dpuNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
                ddlApprovalLevel.SelectedValue = ds.Tables[0].Rows[0]["APLNO"].ToString();
                ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                rdoBtnListIsApproval.SelectedValue = ds.Tables[0].Rows[0]["ISAPPROVAL"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.ShowEditDetailsDeptRegister-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void FillApprovalLevel()
    {
        try  
        {
            objCommon.FillDropDownList(ddlApprovalLevel, "store_approval_level", "aplno", "aplt", "aplno>0 and ACTIVESTATUS=1", "aplt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.FillUser-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillUser()
    {
        try
        {
            objCommon.FillDropDownList(ddlUser, "user_acc", "ua_no", "ISNULL(UA_FULLNAME,'')+''", "(ua_type=1 OR ua_type=3 OR ua_type=4 OR UA_TYPE=5 OR ua_type=6 OR ua_type=7 OR UA_TYPE=8) and UA_STATUS=0 AND UA_FULLNAME!=''", "UA_FULLNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.FillUser-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void FillDept()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void butCancel_Click(object sender, EventArgs e)
    {
        //this.Clear();
        Response.Redirect(Request.Url.ToString());
    }
    
    protected void Clear()
    {
       
        ddlDepartment.SelectedValue = "0";
        ddlUser.SelectedValue = "0";
        ddlApprovalLevel.SelectedValue = "0";
        rdoBtnListIsApproval.SelectedValue = "0";
        ViewState["action"] = "add";
    }


    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewDeptUser();
    }

    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        ShowReport("Department_User_Report", "Department_User_Report.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + "," + "@UserName=" + Session["username"]+",@P_MDNO="+ Convert.ToInt32(ddlDepartment.SelectedValue);
           

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objStrMaster.GetAllDeptUser(Convert.ToInt32(ddlDepartment.SelectedValue));
        lvDeptUser.DataSource = ds;
        lvDeptUser.DataBind();
    }
}
