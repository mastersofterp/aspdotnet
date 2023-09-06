//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : HOSTEL STUDENT FINE REPORT                     
// CREATION DATE : 13 MARCH 2013                                                     
// CREATED BY    : YAKIN UTANE                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
public partial class HOSTEL_REPORT_HostelFineReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MessBillController objMbc = new MessBillController();
    #region Page Events
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
                        //lblHelp.Value = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelFineReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelFineReport.aspx");
        }
    }
    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL DROPDOWN HOSTEL SESSION NO.
           // objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0", "HOSTEL_SESSION_NO desc");
            objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
            if (Session["usertype"].ToString() == "1")
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
            else
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT A LEFT OUTER JOIN ACD_HOSTEL_APPLY_STUDENT SA ON(SA.IDNO=A.IDNO) INNER JOIN ACD_HOSTEL_ROOM_ALLOTMENT B ON(A.IDNO=B.RESIDENT_NO) INNER JOIN ACD_HOSTEL_SESSION AH ON	(B.HOSTEL_SESSION_NO=AH.HOSTEL_SESSION_NO) INNER JOIN ACD_HOSTEL H ON(B.HOSTEL_NO=H.HOSTEL_NO)INNER JOIN ACD_HOSTEL_ROOM HR ON(HR.ROOM_NO=B.ROOM_NO) INNER JOIN ACD_HOSTEL_RESIDENT_TYPE RT ON (RT.RESIDENT_TYPE_NO=B.RESIDENT_TYPE_NO)", "A.IDNO", "A.ROLLNO,A.STUDNAME,B.ROOM_NO,HR.ROOM_NAME,H.HOSTEL_NAME,AH.SESSION_NAME,DBO.FN_DESC('DEGREENAME',A.DEGREENO) AS DEGREE,DBO.FN_DESC('BRANCHSNAME',A.BRANCHNO) AS BRANCH ,DBO.FN_DESC('SEMESTER',A.SEMESTERNO)AS SEMESTERNAME", "B.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + "  AND B.HOSTEL_NO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + " AND A.CAN=0 AND B.CAN=0", "A.IDNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.DataSource = ds;
            lvDetails.DataBind();
        }
        else
        {
            lvDetails.DataSource = null;
            lvDetails.DataBind();
            objCommon.DisplayMessage("Record Not Found", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlHostelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvDetails.DataSource = null;
        lvDetails.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ret = 0;
        int studentIds = 0;
        string remark=string.Empty;
        decimal fine=0;
        int OrganizationId = 0;
        foreach (ListViewDataItem item in lvDetails.Items)
        {
            TextBox txtfine = item.FindControl("txtFine") as TextBox;
            TextBox txtremark = item.FindControl("txtRemark") as TextBox;
            //CheckBox checkid = item.FindControl("chkIdno") as CheckBox;

            HiddenField hid = item.FindControl("hidIdNo") as HiddenField;
            int checkid = Convert.ToInt32(hid.Value);
            if (txtfine.Text == "" || txtremark.Text == "")
            {
                objCommon.DisplayMessage("please fill the information", this.Page);
            }
            else
            {
                remark = txtremark.Text;
                fine = Convert.ToDecimal(txtfine.Text);
                studentIds = Convert.ToInt32(checkid);
                OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                ret = objMbc.HostelFineInsert(Convert.ToInt32(ddlHostelSessionNo.SelectedValue), Convert.ToInt32(studentIds), Convert.ToInt32(ddlHostelNo.SelectedValue), fine, remark, Convert.ToInt32(Session["userno"].ToString()), Request.ServerVariables["REMOTE_ADDR"].ToString(), OrganizationId);
                if (ret != -99)
                {
                    objCommon.DisplayMessage("Record Saved Successfully!!", this.Page);
                    txtfine.Enabled = false;
                    txtremark.Enabled = false;
                }
                else
                {
                    objCommon.DisplayMessage("Transaction Failed!!", this.Page);
                }
            }
        }
     }
    protected void lvDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataSet ds = new DataSet();
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        TextBox txtfine = dataitem.FindControl("txtFine") as TextBox;
        TextBox txtremark = dataitem.FindControl("txtRemark") as TextBox;
        HiddenField hid = dataitem.FindControl("hidIdNo") as HiddenField;
        int checkid = Convert.ToInt32(hid.Value);
        int idno= Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_STUD_FINE","COUNT(IDNO)","SESSION_NO="+Convert.ToInt32(ddlHostelSessionNo.SelectedValue)+ " AND HOSTEL_NO="+Convert.ToInt32(ddlHostelNo.SelectedValue)+"AND IDNO="+Convert.ToInt32(checkid)));
        if (idno > 0)
        {   
            ds= objCommon.FillDropDown("ACD_HOSTEL_STUD_FINE","FINE_AMOUNT","REMARK","SESSION_NO="+Convert.ToInt32(ddlHostelSessionNo.SelectedValue)+ " AND HOSTEL_NO="+Convert.ToInt32(ddlHostelNo.SelectedValue)+"AND IDNO="+Convert.ToInt32(checkid),string.Empty);
            txtfine.Text = ds.Tables[0].Rows[0]["FINE_AMOUNT"].ToString();
            txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
           // checkid.Enabled = false;
            txtfine.Enabled = false;
            txtremark.Enabled = false;
        }
    }

    protected void btnEditFine_Click(object sender, EventArgs e)
    {
        ((sender as ImageButton).Parent.FindControl("txtFine") as TextBox).Enabled = true;
        ((sender as ImageButton).Parent.FindControl("txtRemark") as TextBox).Enabled = true;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Hostel Fine Report", "rptHostelFineReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HOSTEL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HOSTEL," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
 
    
}

