using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data.SqlClient;




public partial class REPAIR_AND_MAINTENANCE_MASTER_Complaint_Department : System.Web.UI.Page
{
    Common objCommon = new Common();
    ComplaintController objCIC = new ComplaintController();
    Complaint objComplaintItem = new Complaint();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    CheckPageAuthorization();
                    ViewState["action"] = "add";
                    BindDepartmentName();
                }
                divMsg.InnerHtml = string.Empty;
                ViewState["flag"] = null;
                PopulateDropDownListDept();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

        }
    }

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

    protected void Repeater_Complainttype_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int MNO = Convert.ToInt32(e.CommandArgument);
            ViewState["MNO"] = MNO;
            ViewState["action"] = "edit";
            
            DataSet ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME, DEPTCODE,FLAG_SP", "DEPTID=" + MNO, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                objCommon.FillDropDownList(ddlDepartment, "COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", "", "DEPTNAME");
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTID"].ToString();
                txtDeptCode.Text = ds.Tables[0].Rows[0]["DEPTCODE"].ToString();
                if (ds.Tables[0].Rows[0]["FLAG_SP"].ToString() == "1")
                {
                    chkServiceProvider.Checked = true;
                }
                else
                {
                    chkServiceProvider.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        if (ViewState["action"].Equals("add"))
        {
            ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "*", " ", "DEPTNAME='" + ddlDepartment.SelectedItem.Text + "'", "");
        }
        else
        {
            ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "*", " ", "DEPTNAME='" + ddlDepartment.SelectedItem.Text + "' AND DEPTID !=" + Convert.ToInt32(ViewState["MNO"]), "");
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

    protected Boolean DeptCodeDuplicate()
    {
        DataSet ds = null;
        if (ViewState["action"].Equals("add"))
        {
            ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "*", " ", "DEPTCODE='" + txtDeptCode.Text + "'", "");
        }
        else
        {
            ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "*", " ", "DEPTCODE='" + txtDeptCode.Text + "' AND DEPTID !=" + Convert.ToInt32(ViewState["MNO"]), "");
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (DeptCodeDuplicate() == true)
            {
                objCommon.DisplayMessage(updComplaintDepartment, "This Department Code Already Exist.", this.Page);
                return;
            }

            if (ddlDepartment.SelectedIndex >0)
            {
                objComplaintItem.Department_name = ddlDepartment.SelectedItem.Text;
                objComplaintItem.Department_code = txtDeptCode.Text;

                if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        objCommon.DisplayMessage(updComplaintDepartment, "Record Already Exist.", this.Page);
                        funclear();
                        return;
                    }
                    if (chkServiceProvider.Checked == true)
                    {
                        objComplaintItem.Flag = 1;
                    }
                    else
                    {
                        objComplaintItem.Flag = 0;
                    }
                    //objComplaintItem.Flag = Convert.ToInt32(ViewState["flag"]);
                    objComplaintItem.Dept_Id = 0;                   
                    CustomStatus cs = (CustomStatus)objCIC.AddDepartmentTypeName(objComplaintItem);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindDepartmentName();
                        objCommon.DisplayMessage(updComplaintDepartment, "Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updComplaintDepartment, "Sorry!Try Again.", this.Page);
                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    if (funDuplicate() == true)
                    {
                        objCommon.DisplayMessage(updComplaintDepartment, "Record Already Exist.", this.Page);
                        funclear();
                        return;
                    }                   
                    if (chkServiceProvider.Checked == true)
                    {
                        objComplaintItem.Flag = 1;
                    }
                    else
                    {
                        objComplaintItem.Flag = 0;
                    }
                    objComplaintItem.Dept_Id = Convert.ToInt16(ViewState["MNO"]);

                    CustomStatus cs = (CustomStatus)objCIC.AddDepartmentTypeName(objComplaintItem);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindDepartmentName();
                        objCommon.DisplayMessage(updComplaintDepartment, "Record Updated Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updComplaintDepartment, "Sorry! Try Again.", this.Page);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(updComplaintDepartment, "Please Select Department.", this.Page);
            }
            funclear();

        }
        catch (Exception ex)
        {

        }
    }

    protected void funclear()
    {
        ViewState["action"] = "add";
        ddlDepartment.SelectedIndex = 0;
        txtDeptCode.Text = string.Empty;
        ViewState["MNO"] = null;
        chkServiceProvider.Checked = false;
        PopulateDropDownListDept();
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        funclear();
    }

    protected void BindDepartmentName()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME, DEPTCODE,FLAG_SP", "DEPTID>0 AND ISNULL(DEL_STATUS,0) =0", "DEPTID");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                Repeater_Complainttype.DataSource = ds;
                Repeater_Complainttype.DataBind();
            }
            else
            {
                Repeater_Complainttype.DataSource = null;
                Repeater_Complainttype.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ComplaintController objCC = new ComplaintController();
            ImageButton btnDelete = sender as ImageButton;
            int deptID = int.Parse(btnDelete.CommandArgument);
            ViewState["deptID"] = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("COMPLAINT_USER", "*", "", "DEPTID=" + deptID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Department Can Not Be Deleted.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objCC.DeleteDepartment(deptID);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    funclear();
                    BindDepartmentName();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Complaint_Complaint_Department.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkServiceProvider_CheckedChanged(object sender, EventArgs e)
    {
        if (chkServiceProvider.Checked == true)
        {
            ViewState["flag"] = 1;
        }
        else
        {
            ViewState["flag"] = 0;
        }
        
    }
    
    //Added by Nancy Sharma
    #region This is for fetching Department Name & Code from Academic 

    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   try
    //    {
    //        DataSet dsC = objCommon.FillDropDown("ACD_DEPARTMENT", "DEPTNO", "DEPTCODE", "DEPTNO=" +ddlDepartment.SelectedValue, "DEPTNAME");
    //        txtDeptCode.Text = dsC.Tables[0].Rows[0]["DEPTCODE"].ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Complaint_Complaint_Department.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    // }    
    
    private void PopulateDropDownListDept()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPT!=''", "SUBDEPT");
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDepartment.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaint_Complaint_Department.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

}
