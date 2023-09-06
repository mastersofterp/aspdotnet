//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : PRIORITY WORK MASTER                                                       
// CREATION DATE : 10-OCT-2017                                                        
// CREATED BY    : MRUNAL SINGH                                                   
// MODIFIED DATE :
// MODIFIED DESC :
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Complaints_MASTER_ComplaintStaff : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Complaint objCEnt = new Complaint();
    ComplaintController objCCon = new ComplaintController();

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
               
                ViewState["action"] = "add";
                objCommon.FillDropDownList(ddlDepartmentName, "COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", "", "DEPTNAME");


                int deptid = objCCon.GetDeptName(Convert.ToInt32(Session["userno"].ToString()));
                ddlDepartmentName.Text = deptid.ToString();

                if (Session["usertype"].ToString() == "1")
                {
                    ddlDepartmentName.Enabled = true;
                }
                else
                {
                    ddlDepartmentName.Enabled = false;
                }

                objCommon.FillDropDownList(ddlComplaintNatureType, "COMPLAINT_TYPE", "TYPEID", "TYPENAME", "DEPTID =" + Convert.ToInt32(ddlDepartmentName.SelectedValue), "");
                BindListView();
            }
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_STAFF S INNER JOIN COMPLAINT_TYPE T ON (S.COMPLAINT_NATURE_ID = T.TYPEID)  INNER JOIN COMPLAINT_DEPARTMENT D ON (S.DEPTNO = D.DEPTID)", "S.STAFFID, S.DEPTNO, S.COMPLAINT_NATURE_ID, S.STAFF_NAME, S.MOBILENO, S.EMAIL_ID", "T.TYPENAME, D.DEPTNAME", "S.DEPTNO =" + Convert.ToInt32(ddlDepartmentName.SelectedValue) + " OR " + Convert.ToInt32(ddlDepartmentName.SelectedValue) + " = 0", "S.STAFFID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStaff.DataSource = ds;
                lvStaff.DataBind();
                lvStaff.Visible = true;
            }
            else
            {
                lvStaff.DataSource = null;
                lvStaff.DataBind();
                lvStaff.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_ComplaintStaff.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            int StaffId = int.Parse(btnEdit.CommandArgument);
            ViewState["STAFFID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetail(StaffId);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_ComplaintStaff.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int StaffId)
    {
        DataSet ds = objCommon.FillDropDown("COMPLAINT_STAFF", "STAFFID, DEPTNO, COMPLAINT_NATURE_ID", "STAFF_NAME,	MOBILENO, EMAIL_ID", "STAFFID = " + StaffId, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["STAFFID"] = ds.Tables[0].Rows[0]["STAFFID"].ToString();
            ddlDepartmentName.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
            objCommon.FillDropDownList(ddlComplaintNatureType, "COMPLAINT_TYPE", "TYPEID", "TYPENAME", "DEPTID =" + Convert.ToInt32(ddlDepartmentName.SelectedValue), "");              
               
            ddlComplaintNatureType.SelectedValue = ds.Tables[0].Rows[0]["COMPLAINT_NATURE_ID"].ToString();
            txtEngStaffName.Text = ds.Tables[0].Rows[0]["STAFF_NAME"].ToString();
            txtMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCEnt.STAFFID = 0;
            objCEnt.DEPTNO =  Convert.ToInt32(ddlDepartmentName.SelectedValue);
            objCEnt.COMPLAINT_NATURE_ID = Convert.ToInt32(ddlComplaintNatureType.SelectedValue);
            objCEnt.STAFF_NAME = txtEngStaffName.Text.Trim();
            objCEnt.MOBILENO = txtMobileNo.Text.Trim();
            objCEnt.EMAIL_ID = txtEmail.Text.Trim() == string.Empty ? string.Empty : txtEmail.Text.Trim();
            objCEnt.USERNO = Convert.ToInt32(Session["userno"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objCCon.AddUpdateStaff(objCEnt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        BindListView();
                        objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["STAFFID"] != null)
                    {
                        objCEnt.STAFFID = Convert.ToInt32(ViewState["STAFFID"].ToString());

                        CustomStatus cs = (CustomStatus)objCCon.AddUpdateStaff(objCEnt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            Clear();
                            BindListView();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully.", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_ComplaintStaff.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //Clear Control 
    private void Clear()
    {
        //if (Session["usertype"].ToString() == "1")
        //{
        //    ddlDepartmentName.SelectedIndex = 0;
        //}
        BindListView();
        ddlComplaintNatureType.SelectedIndex = 0;
        txtEngStaffName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["STAFFID"] = null;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void ddlDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartmentName.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlComplaintNatureType, "COMPLAINT_TYPE", "TYPEID", "TYPENAME", "DEPTID =" + Convert.ToInt32(ddlDepartmentName.SelectedValue), "");              
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ComplaintStaff.ddlDepartmentName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}