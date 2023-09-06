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
using IITMS.SQLServer.SQLDAL;
public partial class HOSTEL_StaffInformation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GuestInfo objGI = new GuestInfo();
    GuestInfoController objGIC = new GuestInfoController();

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
        try
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO");
                objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
                ViewState["action"] = "add";
            }
           
            //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
            FillListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StaffInformation.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_StaffInformation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_StaffInformation.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    { try
        {
        int output = -99;
        objGI.GuestName = txtName.Text.Trim();
        objGI.GuestAddress = txtAddress.Text.Trim();
        objGI.ContactNo = txtContact.Text.Trim();
        objGI.Purpose = txtDesignation.Text.Trim();
        objGI.CollegeCode = Session["colcode"].ToString();
        objGI.Sessionno=Convert.ToInt32(ddlSession.SelectedValue);
        objGI.Hostelno=Convert.ToInt32(ddlHostel.SelectedValue);
        objGI.EmailId = txtEmail.Text.Trim();
        objGI.OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        objGI.Ua_no = Convert.ToInt32(Session["userno"].ToString());

        if (ViewState["action"].ToString() == "add")
        {
            if (CheckDuplicateEntry() == true)
            {
                objCommon.DisplayMessage("Record Already Exist.", this.Page);
                return;

            }
            output = objGIC.AddStaffInfo(objGI);
        }
        else
        {
           int stno= Convert.ToInt32(ViewState["STNO"].ToString());
           if (CheckDuplicateEntryUpdate(stno) == true)
           {
               objCommon.DisplayMessage("Record Already Exist.", this.Page);
               return;

           }
           output = objGIC.UpdateStaffInfo(objGI, stno);
           ViewState["action"] = "add";
        }
        if (output != -99)
        {
            objCommon.DisplayMessage("Record Saved Successfully!!", this.Page);
            FillListView();
            ClearControlContents();
        }
        else
            objCommon.DisplayMessage("Transaction Failed!!", this.Page);
        }
    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "HOSTEL_StaffInformation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server Unavailable");
    }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void FillListView()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_HOSTEL_STAFF_INFO SI LEFT OUTER JOIN ACD_HOSTEL_SESSION HS ON (SI.SESSIONNO=HS.HOSTEL_SESSION_NO)LEFT OUTER JOIN ACD_HOSTEL H ON (SI.HOSTELNO=H.HOSTEL_NO)", "STNO", "EMP_NAME,DESIGNATION,CONTACTNO,EMAILID,EMP_ADDRESS,SESSION_NAME,HOSTEL_NAME,SI.SESSIONNO,SI.HOSTELNO", "(SI.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (SI.HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " OR " + Convert.ToInt32(ddlHostel.SelectedValue) + "=0)", "STNO DESC");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStaff.DataSource = ds;
            lvStaff.DataBind();
        }
        else
        {
            lvStaff.DataSource = null;
            lvStaff.DataBind();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            ImageButton editButton = sender as ImageButton;
            int stno = Int32.Parse(editButton.CommandArgument);

            ds = objCommon.FillDropDown("ACD_HOSTEL_STAFF_INFO SI LEFT OUTER JOIN ACD_HOSTEL_SESSION HS ON (SI.SESSIONNO=HS.HOSTEL_SESSION_NO)LEFT OUTER JOIN ACD_HOSTEL H ON (SI.HOSTELNO=H.HOSTEL_NO)", "STNO", "EMP_NAME,DESIGNATION,CONTACTNO,EMAILID,EMP_ADDRESS,SESSION_NAME,HOSTEL_NAME,SI.SESSIONNO,SI.HOSTELNO", "STNO="+stno, "STNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["EMP_NAME"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["EMP_ADDRESS"].ToString();
                txtContact.Text = ds.Tables[0].Rows[0]["CONTACTNO"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["DESIGNATION"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                ddlHostel.SelectedValue = ds.Tables[0].Rows[0]["HOSTELNO"].ToString();
                ViewState["STNO"] = stno.ToString();
                ViewState["action"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StaffInformation.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    //protected void dpStaff_PreRender(object sender, EventArgs e)
    //{
    //    FillListView();
    //}

    private void ClearControlContents()
    {
        ClearControlsRecursive(Page);
    }
    private void ClearControlsRecursive(Control root)
    {
        if (root is TextBox)
        {
            ((TextBox)root).Text = string.Empty;
        }
        if (root is DropDownList)
        {
            ((DropDownList)root).SelectedIndex = 0;
        }
        foreach (Control child in root.Controls)
        {
            ClearControlsRecursive(child);
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillListView();
    }

    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillListView();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    { 
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Staff Information";
            url += "&path=~,Reports,Hostel,StaffInfoHostel.rpt";
            //url += "&param=username=" + Session["username"].ToString() + ",monthname=" + ddlMonth.SelectedValue + " - " + (DateTime.Now.Year).ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_HOSTELNO="+Convert.ToInt32(ddlHostel.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','Staff Information','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StaffInformation.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }


    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string empID = objCommon.LookUp("ACD_HOSTEL_STAFF_INFO", "STNO", "(EMP_NAME = '" + txtName.Text + "' AND CONTACTNO='" + txtContact.Text + "' AND EMAILID='" + txtEmail.Text + "') OR (EMP_NAME = '" + txtName.Text + "' AND EMAILID='" + txtEmail.Text + "') OR (EMP_NAME = '" + txtName.Text + "' AND CONTACTNO='" + txtContact.Text + "')");
            if (empID != null && empID != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int stno)
    {
        bool flag = false;
        try
        {
            string empID = objCommon.LookUp("ACD_HOSTEL_STAFF_INFO", "STNO", "(STNO !=" + stno + " ) and ((EMP_NAME = '" + txtName.Text + "' AND CONTACTNO='" + txtContact.Text + "' AND EMAILID='" + txtEmail.Text + "') OR (EMP_NAME = '" + txtName.Text + "' AND EMAILID='" + txtEmail.Text + "') OR (EMP_NAME = '" + txtName.Text + "' AND CONTACTNO='" + txtContact.Text + "'))");
            if (empID != null && empID != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
}
