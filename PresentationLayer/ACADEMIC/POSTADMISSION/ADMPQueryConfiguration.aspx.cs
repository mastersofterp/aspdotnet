using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_POSTADMISSION_ADMPQueryConfiguration : System.Web.UI.Page
{
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    QueryController objquery = new QueryController();
    Common objCommon = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ViewState["action"] = "add";
                BindQueryCategory();
                //BindDropDown();
                //BindUserAllocation();
            }
        }
    }

    //private void ToggleTab(string TabName)
    //{
    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('" + TabName + "');</script>", false);
    //}

    //protected void BindDropDown()
    //{

    //    objCommon.FillDropDownList(ddlQCategory, "ACD_ADMP_QueryCategory", "QCATEGORYID", "QUERY_CATEGORY", "QCATEGORYID>0", "QCATEGORYID");
    //    objCommon.FillDropDownList(ddlIncharge, "User_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE = 1", "UA_NO");
    //    objCommon.FillListBox(lstbxTeamMember, "User_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE =" + 3 + "", "UA_NO");
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtQueryCategory.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Query Category", this.Page);
                return;
            }
            //int USERNO = Convert.ToInt32(Session["userno"].ToString());
            int COLLEGE_CODE = Convert.ToInt32(Session["colcode"].ToString());
            string QueryCategory = txtQueryCategory.Text.Trim();
            int Status = 0;
            int QCategoryId = 0;
            if (hfdActive.Value == "true")
            {
                Status = 1;
            }
            else
            {
                Status = 0;
            }

            if (ViewState["action"].ToString() == "add")
            {
                QCategoryId = 0;
            }
            else
            {
                QCategoryId = Convert.ToInt32(ViewState["QCATEGORYID"]);
            }
            int status = objquery.InsertQueryCategoryData(QCategoryId, QueryCategory, COLLEGE_CODE, Status);
            if (status == 1)
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Sucessfully !", this.Page);
                BindQueryCategory();
                ClearControl();
                
            }
            else if (status == 2)
            {
                objCommon.DisplayMessage(this.Page, "Record Updated Sucessfully!", this.Page);
                BindQueryCategory();
                ClearControl();
              
            }
            else if (status == -1001)
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exist !", this.Page);
                BindQueryCategory();
                ClearControl();

            }
            else if (status == 4)
            {
                objCommon.DisplayMessage(this.Page, "You Can not Change Status As Query Category is Already Used in process!", this.Page);
                BindQueryCategory();
                ClearControl();

            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPQueryConfiguration.aspx.BindQueryCategory() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void ClearControl()
    {
        txtQueryCategory.Text = string.Empty;
        ViewState["action"] = "add";
        //ToggleTab("tab_1");
    }

    public void BindQueryCategory()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_QUERY_CATEGORY", "CATEGORYNO", "QUERY_CATEGORY_NAME,ACTIVE_STATUS =(isnull(ACTIVE_STATUS,0))", "", "CATEGORYNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvQueryCategory.DataSource = ds;
                lvQueryCategory.DataBind();
            }
            else
            {
                lvQueryCategory.DataSource = null;
                lvQueryCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPQueryConfiguration.BindQueryCategory() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int QCATEGORYID = int.Parse(btnEdit.CommandArgument);
        ViewState["QCATEGORYID"] = int.Parse(btnEdit.CommandArgument);
        ShowDetails(QCATEGORYID);
        ViewState["action"] = "edit";
    }
    private void ShowDetails(int QCATEGORYID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_QUERY_CATEGORY", "ACTIVE_STATUS =(isnull(ACTIVE_STATUS,0))", "CATEGORYNO,QUERY_CATEGORY_NAME", "CATEGORYNO =" + QCATEGORYID + "", "CATEGORYNO");

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString()) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
                txtQueryCategory.Text = ds.Tables[0].Rows[0]["QUERY_CATEGORY_NAME"].ToString();
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPQueryConfiguration.BindQueryCategory() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
       
    }
    //protected void btncancel2_Click(object sender, EventArgs e)
    //{
    //    ClearControl2();
       
    //}
    //protected void ClearControl2()
    // {
        
    //     ddlIncharge.SelectedIndex = 0;
    //     ddlQCategory.SelectedIndex = 0;
    //     lstbxTeamMember.ClearSelection();
    //     ViewState["action"] = "add";
    //     ToggleTab("tab_2");
        
    // }
    //protected void btnsubmit2_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int USERNO = Convert.ToInt32(Session["userno"].ToString());
    //        int QMUSERALLOCATIONID = 0;
    //        int INCHARGEID = Convert.ToInt32(ddlIncharge.SelectedValue);
    //        int QCategoryId = Convert.ToInt32(ddlQCategory.SelectedValue);
    //        string memberid = "";
           
    //        foreach (ListItem item in lstbxTeamMember.Items)
    //        {
    //            if (item.Selected == true)
    //            {
    //                memberid += item.Value + ',';
    //            }
    //        }
    //        if (!string.IsNullOrEmpty(memberid))
    //        {
    //            memberid = memberid.Substring(0, memberid.Length - 1);
    //        }
    //        else
    //        {
    //            memberid = "0";

    //        }
         
    //        string MEMBERID = memberid;
    //        if (ViewState["action"].ToString() == "add")
    //        {
    //            QMUSERALLOCATIONID = 0;
    //        }
    //        else
    //        {
    //            QMUSERALLOCATIONID = Convert.ToInt32(ViewState["QMUSERALLOCATIONID"]);
    //        }
          
    //        int status = objquery.InsertUserAllocationData(QMUSERALLOCATIONID, QCategoryId, INCHARGEID, MEMBERID, USERNO);
           
            
    //        if (status == 1)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Record Saved Sucessfully !", this.Page);
    //            BindUserAllocation();
    //            ClearControl2();
               
    //        }
    //        else if (status == 2)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Record Updated Sucessfully!", this.Page);
    //            BindUserAllocation();
    //            ClearControl2();
                
    //        }
    //        else if (status == -1001)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Record Already Exist!", this.Page);
    //            BindUserAllocation();
    //            ClearControl2();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ADMPQueryConfiguration.aspx.BindUserAllocation() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //public void BindUserAllocation()
    //{
    //    try
    //    {
    //        DataSet ds = objquery.GetUserAllocationList();
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvUserAllocation.DataSource = ds;
    //            lvUserAllocation.DataBind();
    //        }
    //        else
    //        {
    //            lvUserAllocation.DataSource = null;
    //            lvUserAllocation.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ADMPQueryConfiguration.BindUserAllocation() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnEdit1_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton btnEdit = sender as ImageButton;
    //    int userallocationid = int.Parse(btnEdit.CommandArgument);
    //    ViewState["QMUSERALLOCATIONID"] = int.Parse(btnEdit.CommandArgument);
    //    ShowDetails1(userallocationid);
    //    ViewState["action"] = "edit";
    //    ToggleTab("tab_2");
    //}
    //private void ShowDetails1(int userallocationid)
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_ADMP_QMUserAllocation", "QMUSERALLOCATIONID", "QCATEGORYID, INCHARGEID,MEMBERID", "QMUSERALLOCATIONID =" + userallocationid + "", "QMUSERALLOCATIONID");
    //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //                ddlIncharge.SelectedValue = ds.Tables[0].Rows[0]["INCHARGEID"].ToString();
    //                ddlQCategory.SelectedValue = ds.Tables[0].Rows[0]["QCATEGORYID"].ToString();
    //                char delimiterChars = ',';
    //                string MEMBERID = ds.Tables[0].Rows[0]["MEMBERID"].ToString();
    //                string[] mem = MEMBERID.Split(delimiterChars);
    //                for (int j = 0; j < mem.Length; j++)
    //                {
    //                    for (int i = 0; i < lstbxTeamMember.Items.Count; i++)
    //                    {
    //                        if (mem[j].Trim() == lstbxTeamMember.Items[i].Value.Trim())
    //                        {
    //                            lstbxTeamMember.Items[i].Selected = true;
    //                        }
    //                    }
    //                }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ADMPQueryConfiguration.BindUserAllocation() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
}