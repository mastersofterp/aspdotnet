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

public partial class PAYROLL_TRANSACTIONS_Pay_AuthorityTypeEmployeeDetail : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();
    ChangeInMasterFileController ObjChangeMstFile = new ChangeInMasterFileController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // CheckRef();

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
               
                pnlSelect.Visible = true;
               pnlMonthlyChanges.Visible = false;

                //FillPayHead(Convert.ToInt32(Session["userno"].ToString()));
                FillStaff();


            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }

    }

    protected void lvMonthlyChanges_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {


            DropDownList ddlAuthoType = (DropDownList)e.Item.FindControl("ddlAuthoType");
            HiddenField hdnAuthoTypeId = (HiddenField)e.Item.FindControl("hdnAuthoTypeId");
            HiddenField hdnIDNO = (HiddenField)e.Item.FindControl("hdnIDNO");
            HiddenField hdnAuthoTypeStatus = (HiddenField)e.Item.FindControl("hdnAuthoTypeStatus");
            
            //Select AUTHO_TYP_ID,	AUTHORITY_TYP_NAME from [dbo].[PAYROLL_NODUES_AUTHORITY_TYPE]
            objCommon.FillDropDownList(ddlAuthoType, "PAYROLL_NODUES_AUTHORITY_TYPE", "AUTHO_TYP_ID", "AUTHORITY_TYP_NAME", "AUTHO_TYP_ID>0", "AUTHO_TYP_ID");
            ddlAuthoType.SelectedValue = hdnAuthoTypeId.Value;
             //ddleditfield.Enabled = false;
            if (Convert.ToInt32(hdnAuthoTypeStatus.Value) > 0)
            {
                ddlAuthoType.Enabled = false;
            }
            else
            {
                ddlAuthoType.Enabled = true;
            }
              
        }
    }


    private void BindListViewList(int staffNo, int collegeNo)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {

                pnlMonthlyChanges.Visible = true;
                DataSet ds = ObjChangeMstFile.GetEmployeesForNoDues(Convert.ToInt32(ddlStaff.SelectedValue), collegeNo);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    lvMonthlyChanges.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    lvMonthlyChanges.Visible = true;
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }

                lvMonthlyChanges.DataSource = ds;
                lvMonthlyChanges.DataBind();

               
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
            {

                if (lvMonthlyChanges.Items.Count>0)
                {
                    
                    DropDownList ddlAuthoType = lvitem.FindControl("ddlAuthoType") as DropDownList;
                   
                    HiddenField hdnIDNO = lvitem.FindControl("hdnIDNO") as HiddenField;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdateNoDuesAuthoType(Convert.ToInt32(ddlAuthoType.SelectedValue),Convert.ToInt32(hdnIDNO.Value) );
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }
               
            }
            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage("Record Updated Successfully", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
       

    }

   

    protected void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");          
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;
        BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
    }


    
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            message = message.Replace("'", "\'");
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListViewList( Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

        }
        catch (Exception ex)
        {
        }
    }
  
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            BindListViewList( Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
        }
        catch (Exception ex)
        {
        }
    }
}