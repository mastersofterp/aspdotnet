//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : COMPLAINT TYPE                                                       
// CREATION DATE : 15-April-2009                                                        
// CREATED BY    : SANJAY RATNAPARKHI                                                   
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


public partial class Estate_complaint_type : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ComplaintController objCTC = new ComplaintController();
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();

                //Show Department Name according username

                int deptid = objCTC.GetDeptName(Convert.ToInt32(Session["userno"].ToString()));
                ddlDepartmentName.Text = deptid.ToString();

                if (Session["usertype"].ToString() == "1")
                {
                    ddlDepartmentName.Enabled = true;
                    BindListViewComplaintType(0);
                }
                else
                {
                    ddlDepartmentName.Enabled = false;
                    BindListViewComplaintType(deptid);
                }

                //Bind the ListView with Notice

                ViewState["action"] = "add";
                //pnlAdd.Visible = false;
                //pnlList.Visible = true;
            }
        }
    }

    private void BindListViewComplaintType(int deptid)
    {
        try
        {
            ComplaintController objCtc = new ComplaintController();
            DataSet dsCT = objCtc.GetAllComplaintType(deptid); //Convert.ToInt32(ddlDepartmentName.Text));

            if (dsCT.Tables[0].Rows.Count > 0)
            {
                lvComplaintType.DataSource = dsCT;
                lvComplaintType.DataBind();
                lvComplaintType.Visible = true;
            }
            else
            {
                lvComplaintType.DataSource = null;
                lvComplaintType.DataBind();
                lvComplaintType.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_type.BindListViewComplaintType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Clear();
        ViewState["action"] = "add";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // ViewState["action"] = null;
        //txtComplaintType.Text = string.Empty;     
        Clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        txtComplaintType.Text = string.Empty;
    }



    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int typeid = int.Parse(btnEdit.CommandArgument);

            ShowDetail(typeid);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_type.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int typeid)
    {
        ComplaintController objCTC = new ComplaintController();
        SqlDataReader dr = objCTC.GetSingleComplaintType(typeid);

        //Show Complaint type Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["typeid"] = typeid.ToString();
                ddlDepartmentName.Text = dr["DEPTID"] == null ? string.Empty : dr["DEPTID"].ToString();
                txtComplaintType.Text = dr["TYPENAME"] == null ? string.Empty : dr["TYPENAME"].ToString();
                txtCatCode.Text = dr["TYPE_CODE"] == null ? string.Empty : dr["TYPE_CODE"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ComplaintController objCtc = new ComplaintController();
            Complaint objComplaintType = new Complaint();

            objComplaintType.Deptid = Convert.ToInt32(ddlDepartmentName.SelectedValue);
            objComplaintType.Type_Name = txtComplaintType.Text.Trim();
            objComplaintType.Type_Code = txtCatCode.Text.Trim();

            if (CategoryDuplicate() == true)
            {
                objCommon.DisplayMessage(UpdatePanel1, "This Category Type Already Exist.", this.Page);
                Clear();
                return;

            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Complaint Type Record
                    CustomStatus cs = (CustomStatus)objCtc.AddComplaintType(objComplaintType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["typeid"] != null)
                    {
                        objComplaintType.TypeId = Convert.ToInt32(ViewState["typeid"].ToString());

                        CustomStatus cs = (CustomStatus)objCtc.UpdateComplaintType(objComplaintType);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully.", this.Page);
                        }
                    }
                }
                BindListViewComplaintType(0);
            }
            //update record bind listview 

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_type.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_DEPARTMENT");
            ddlDepartmentName.DataSource = ds;
            ddlDepartmentName.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDepartmentName.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDepartmentName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_type.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Clear Control 
    private void Clear()
    {
        if (Session["usertype"].ToString() == "1")
        {
            BindListViewComplaintType(Convert.ToInt32(ddlDepartmentName.SelectedValue));
            // ddlDepartmentName.SelectedIndex = 0;
        }
        ddlDepartmentName.SelectedIndex = 0;
        txtComplaintType.Text = string.Empty;
        txtCatCode.Text = string.Empty;
        ViewState["action"] = "add";

    }

    protected void dpComplaintType_PreRender(object sender, EventArgs e)
    {
        ComplaintController objCC = new ComplaintController();
        BindListViewComplaintType(objCC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())));
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
                BindListViewComplaintType(Convert.ToInt32(ddlDepartmentName.Text));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_type.ddlDepartmentName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Added by Sonal Banode to avoid saving of duplicate records
    protected Boolean CategoryDuplicate()
    {
        DataSet ds = null;
        if (ViewState["action"].Equals("add"))
        {
            ds = objCommon.FillDropDown("COMPLAINT_TYPE", "*", " ", "TYPENAME='" + txtComplaintType.Text + "'", "");
        }
        else
        {
            ds = objCommon.FillDropDown("COMPLAINT_TYPE", "*", " ", "TYPENAME='" + txtComplaintType.Text + "' AND TYPEID !=" + Convert.ToInt32(ViewState["typeid"]), "");
        }
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
