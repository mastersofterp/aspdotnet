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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using Google.API.Translate;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.IO;

public partial class PAYROLL_MASTERS_DivisionMastert : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    DeptDesigMaster objDeptDesig = new DeptDesigMaster();
    string UsrStatus = string.Empty;




    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Check Session
            Form.Attributes.Add("onLoad()", "msg()");
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ViewState["action"] = "add";
                BindListView();
                // Populate DropDownList
                PopulateDropdown();
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "onLoad();", true);
        


    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        //Update();
        PayMaster objPay = new PayMaster();
        objPay.DIVNAME = txtDeptName.Text.Trim();
        objPay.DIVCODE = txtCode.Text.Trim();
        objPay.SUBDEPTNO = Convert.ToInt32(ddlSubDepartment.SelectedValue);
        if (ViewState["action"] != null)
        {
            bool result = CheckPurpose();
            if (ViewState["action"].ToString().Equals("add"))
            {
                // bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    objPay.DIVIDNO = 0;
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddDivision(objPay);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
            else
            {
                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    ViewState["action"] = "add";
                    objPay.DIVIDNO = Convert.ToInt32(ViewState["lblFNO"].ToString().Trim());
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddDivision(objPay);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }

        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }


    private void PopulateDropdown()
    {
        try
        {

            objCommon.FillDropDownList(ddlSubDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPTNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.PopulateDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void Clear()
    {
        ddlSubDepartment.SelectedIndex = 0 ;
        txtDeptName.Text = "";
        txtCode.Text = "";


        ViewState["lblFNO"] = null;
        ViewState["action"] = "add";
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
       
       // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
        dsPURPOSE = objCommon.FillDropDown("PAYROLL_DIVISION", "*", "", "SUBDEPT='" + ddlSubDepartment.SelectedValue + "' AND DIVNAME='" + txtDeptName.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }


    private void BindListView()
    {
        try
        {
            DataSet ds = objDeptDesig.GetDivision();

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvDepartment.DataSource = ds;
                lvDepartment.DataBind();
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblFNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label LBLDEPTNAME = lst.FindControl("lbldeptname") as Label;
            Label LBLDEPTNO = lst.FindControl("lbldeptno") as Label;
            Label LBLDIVNAME = lst.FindControl("lblDivName") as Label;
            Label LBLDIVCODE = lst.FindControl("lblDivCode") as Label;

            
            ddlSubDepartment.SelectedValue = LBLDEPTNO.Text;
            txtDeptName.Text = LBLDIVNAME.Text.Trim();
            txtCode.Text = LBLDIVCODE.Text.Trim();
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void DataPager1_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
}