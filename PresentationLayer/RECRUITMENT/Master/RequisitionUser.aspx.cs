using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class RECRUITMENT_Master_RequisitionUser : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RequisitionController objReq = new RequisitionController();
    Requisition objRequisition = new Requisition();

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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                CheckPageAuthorization();
                FillDepartment();
                BindReqUser();
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
                Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
        }
    }
    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_CODE =" + Convert.ToInt32(Session["colcode"]) + " ", "DEPT.SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillDepartment ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillUser()
    {
        try
        {
            objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPost();
        FillUser();
    }

    private void FillPost()
    {
        try
        {
            int deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
            DataSet ds = objReq.GetPosts(deptno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstPost.DataSource = ds;
                lstPost.DataTextField = "sPostCode";
                lstPost.DataValueField = "nId";
                lstPost.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillPost ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        ddlDepartment.SelectedIndex = 0;
        ddlUser.SelectedIndex = 0;
        ddlPostCategory.SelectedIndex = 0;
        lstPost.ClearSelection();
        lstPost.Items.Clear();
        BindReqUser();
        ddlDepartment.Enabled = true;
        ddlUser.Enabled = true;
        //ViewState["action"] = null;
        ViewState["REQUNO"] = null;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int REQUNO = 0;
            string status1 = Request.Form["switch"];
            if(ViewState["REQUNO"] != null)
            {
              REQUNO = Convert.ToInt32(ViewState["REQUNO"].ToString());
            }
            objRequisition.REQUNO = REQUNO;
            objRequisition.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objRequisition.USERNO = Convert.ToInt32(ddlUser.SelectedValue);
            objRequisition.POSTCATNO = Convert.ToInt32(ddlPostCategory.SelectedValue);
            objRequisition.COLLEGE_CODE = Session["colcode"].ToString();
            objRequisition.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objRequisition.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            if (!string.IsNullOrEmpty(status1) && status1 == "on")
            {
                objRequisition.IsActive = true;
            }
            else
            {
                objRequisition.IsActive = false;
            }

            StringBuilder selectedValues = new StringBuilder();

            // Iterate through the selected items in the list box
            foreach (ListItem item in lstPost.Items)
            {
                // Append the selected item followed by a comma
                if (item.Selected)
                {
                    selectedValues.Append(item.Value);
                    selectedValues.Append(",");
                }
            }

            // Remove the trailing comma if there are selected items
            if (selectedValues.Length > 0)
            {
                selectedValues.Length--; // Remove the last comma
            }

            // Store the result in a variable
            if (selectedValues.Length > 0)
            {
                objRequisition.POSTNO = selectedValues.ToString();
            }
            else
            {
                MessageBox("Please Select Atleast One Allowed Post!");
                return;
            }

            //-----------------------//----------------------------//

            StringBuilder selectedItems = new StringBuilder();

            // Iterate through the selected items in the list box
            foreach (ListItem item in lstPost.Items)
            {
                // Append the selected item followed by a comma
                if (item.Selected)
                {
                    selectedItems.Append(item.ToString());
                    selectedItems.Append(",");
                }
            }

            // Remove the trailing comma if there are selected items
            if (selectedItems.Length > 0)
            {
                selectedItems.Length--; // Remove the last comma
            }

            // Store the result in a variable
            objRequisition.POST = selectedItems.ToString();

            bool result = CheckReqUser();

            if (result == true)
            {
                MessageBox("Record Already Exist!");
                //Clear();
                return;
            }

            CustomStatus cs = (CustomStatus)objReq.AddUpdateReqUserData(objRequisition, 0);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["action"] = null;
                Clear();
                MessageBox("Record Saved Successfully!");
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ViewState["action"] = null;
                Clear();
                MessageBox("Record Updated Successfully!");
            }
            BindReqUser();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int REQUNO = int.Parse(btnEdit.CommandArgument);
        ShowDetails(REQUNO);
        ddlDepartment.Enabled = false;
        ddlUser.Enabled = false;
    }
    private void ShowDetails(Int32 REQUNO)
    {
        DataSet ds = new DataSet();
        objRequisition.REQUNO = REQUNO;
        string status1 = Request.Form["switch"];
        try
        {
            ds = objReq.GetAllRequisitionUser(objRequisition);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["REQUNO"] = REQUNO;
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                FillUser();
                ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                ddlPostCategory.SelectedValue = ds.Tables[0].Rows[0]["POSTCAT_NO"].ToString();
                Boolean Isactive = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISACTIVE"].ToString());
                if (Isactive == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatRecUser(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatRecUser(false);", true);
                }
                string postno = ds.Tables[0].Rows[0]["POST_NO"].ToString();
                string[] valuesArray = postno.Split(',');
                FillPost();
                // Bind the values to the ListBox
                foreach (string value in valuesArray)
                {
                    ListItem item = lstPost.Items.FindByValue(value);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    private void BindReqUser()
    {
        objRequisition.REQUNO = 0;
        try
        {
            DataSet ds = objReq.GetAllRequisitionUser(objRequisition);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRecUser.DataSource = ds;
                lvRecUser.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public bool CheckReqUser()
    {
        bool result = false;
        DataSet ds = new DataSet();

        ds = objCommon.FillDropDown("REC_REQUISITION_USER", "*", "", "DEPTNO= '" + Convert.ToInt32(ddlDepartment.SelectedValue) + "' and UA_NO='" + Convert.ToInt32(ddlUser.SelectedValue) + "' AND ISACTIVE = 1 AND REQUNO != '" + Convert.ToInt32(ViewState["REQUNO"]) + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }



        return result;
    }
}