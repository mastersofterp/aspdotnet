//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 25-SEP-2015                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.FileMovement;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class FileMovementTracking_Master_SectionMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FileMovement objFMov = new FileMovement();
    FileMovementController objFController = new FileMovementController();

    #region PageLoad Events
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
                    ViewState["SECTION_ID"] = null;
                    BindListViewSection();

                    BindRoleNameList();

                    //objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE=7", "UA_NO");
                    //objCommon.FillDropDownList(ddlReceiverHead, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", " E.IDNO", "E.FNAME", "PSTATUS='Y'", "E.IDNO");
                    //objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2) AND UA_DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
                   // objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2)", "UA_FULLNAME");                    
                    objCommon.FillDropDownList(ddlReceiverHead, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2)", "UA_FULLNAME");
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");
                  
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_SectionMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region User-Define Methods
    // This method is used to check page authorization.
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
    // this method is used to clear the controls.
    private void Clear()
    {
        txtSecName.Text = string.Empty;
        ViewState["SECTION_ID"] = null;
       // ddlReceiver.SelectedIndex = 0;
        ddlReceiver.Items.Clear();
        ddlReceiver.Items.Insert(0, new ListItem("Please Select", "Please Select"));        
        ddlReceiverHead.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        
        foreach (ListViewItem i in lvRoles.Items)
        {
            CheckBox chkRoleId = (CheckBox)i.FindControl("chkAccept");
            chkRoleId.Checked = false;
        }


    }
    // this method is used to display the entry list.
    private void BindListViewSection()
    {
        try
        {
           // DataSet ds = objCommon.FillDropDown("FILE_SECTIONMASTER S INNER JOIN User_Acc E ON (S.RECEIVER_ID = E.UA_NO) LEFT JOIN User_Acc P ON (S.RECEVING_HEAD_ID = P.UA_NO) INNER JOIN ACD_DEPARTMENT D ON (S.DEPTNO = D.DEPTNO)", "S.SECTION_ID, S.SECTION_NAME", "E.UA_FULLNAME AS RECEIVER_NAME, P.UA_FULLNAME AS RECEIVER_HEAD_NAME, POST_FROM_DATE, POST_TO_DATE, D.DEPTNAME", "", "SECTION_ID");
            DataSet ds = objFController.GetSectionUserList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSection.DataSource = ds;
                lvSection.DataBind();
            }
            else
            {
                lvSection.DataSource = null;
                lvSection.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_SectionMaster.BindListViewSection-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this method is used to bind Role Name list.
    private void BindRoleNameList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("FILE_ROLE_MASTER", "ROLE_ID", "ROLENAME", "", "ROLE_ID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRoles.DataSource = ds;
                lvRoles.DataBind();
            }
            else
            {
                lvRoles.DataSource = null;
                lvRoles.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_SectionMaster.BindRoleNameList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // this method is used to show the details fetch from database.
    private void ShowDetails(int section_no)
    {
        try
        {
           // DataSet ds = objCommon.FillDropDown("FILE_SECTIONMASTER", "*", "", "POST_TO_DATE IS NULL AND SECTION_ID=" + section_no, "");
            DataSet ds = objFController.GetSectionUserDetails(section_no);
            //to show created user details
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["SECTION_ID"] = ds.Tables[0].Rows[0]["SECTION_ID"].ToString();
                    txtSecName.Text = ds.Tables[0].Rows[0]["SECTION_NAME"].ToString();                                      
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                   // objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2) AND UA_DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
                    objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2) AND UA_DEPTNO='" + Convert.ToInt32(ddlDepartment.SelectedValue)+"'", "UA_FULLNAME");  //  16-03-2022 gayatri
                   ddlReceiver.SelectedValue = ds.Tables[0].Rows[0]["RECEIVER_ID"].ToString(); 
                    ddlReceiverHead.SelectedValue = ds.Tables[0].Rows[0]["RECEVING_HEAD_ID"].ToString();
                    if (ds.Tables[1].Rows.Count > 0)
                    {    
                         for (int j=0 ; j < ds.Tables[1].Rows.Count ; j++ )  
                         {
                          int roleId =  Convert.ToInt32(ds.Tables[1].Rows[j]["SECTION_USER_ROLE"].ToString());

                            foreach (ListViewItem i in lvRoles.Items)
                            {
                               CheckBox chkRoleId = (CheckBox)i.FindControl("chkAccept");

                               if (chkRoleId.ToolTip == roleId.ToString())
                               {
                                   chkRoleId.Checked = true;
                               }
                            }
                         } 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_SectionMaster.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion


    #region Page Actions 
    // this button is used to insert and update the section name.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string userRoles = string.Empty;
            if (lvRoles.Items.Count > 0)
            {
                foreach (ListViewItem i in lvRoles.Items)            
                {
                    CheckBox chkRoleId  = (CheckBox)i.FindControl("chkAccept");
                    if (chkRoleId.Checked == true)
                    {
                        if (userRoles.Trim() == "")
                        {
                            userRoles = chkRoleId.ToolTip;
                        }
                        else
                        {
                            userRoles += "," + chkRoleId.ToolTip;                          
                        }
                    }
                }
            }
            if (userRoles.Trim() == "")
            {
                objCommon.DisplayMessage( "Please Select At Least One Role.", this.Page);
                //objCommon.DisplayMessage(this.updActivity, "Please Select At Least One Role.", this.Page); 15/11/2021
                return;
            }


            objFMov.SECTIONNAME = txtSecName.Text;
            objFMov.RECEIVER_ID = Convert.ToInt32(ddlReceiver.SelectedValue);
            objFMov.RECEIVER_HEAD_ID = Convert.ToInt32(ddlReceiverHead.SelectedValue);
            objFMov.EMPDEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objFMov.USER_ROLES = userRoles;
            if (ViewState["SECTION_ID"] == null)
            {
                CustomStatus cs = (CustomStatus)objFController.AddUpdateSectionName(objFMov);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    //objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page); 15/11/2021
                    objCommon.DisplayMessage( "Record Already Exist.", this.Page);
                    return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListViewSection();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);                   
                }
            }
            else
            {
                objFMov.SECTIONNO = Convert.ToInt32(ViewState["SECTION_ID"].ToString());
                CustomStatus cs = (CustomStatus)objFController.AddUpdateSectionName(objFMov);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    //objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);15/11/2021
                    objCommon.DisplayMessage("Record Already Exist.", this.Page);
                    return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListViewSection();
                  
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);                   
                }             
               
            }
            BindListViewSection();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_SectionMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    // this button is used to brings you in modify mode.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int section_no = int.Parse(btnEdit.CommandArgument);
            ViewState["SECTION_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(section_no);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_SectionMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Section Details", "rptSectionMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true); 15/11/2021

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }  

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
               // objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2) AND UA_DEPTNO='" + Convert.ToString(ddlDepartment.SelectedValue), "UA_FULLNAME");      //15-03-2022  gayatri
               
                objCommon.FillDropDownList(ddlReceiver, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2) AND UA_DEPTNO='" + Convert.ToString(ddlDepartment.SelectedValue)+"'", "UA_FULLNAME");      //15-03-2022 gayatri
                objCommon.FillDropDownList(ddlReceiverHead, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2) AND UA_DEPTNO='"  + Convert.ToInt32(ddlDepartment.SelectedValue)+"'", "UA_FULLNAME");  //15-03-2022  gayatri
               
               
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }


    protected void ddlReceiver_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
        {
            if (ddlReceiver.SelectedIndex > 0)
            {
                txtSecName.Text = ddlReceiver.SelectedItem.Text;
            }
        }
         catch (Exception ex)
         {
             Console.WriteLine(ex.ToString());
         }
    }

    #endregion
}