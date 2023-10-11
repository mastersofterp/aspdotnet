//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : HOSTEL IDENTITY CARD FOR STUDENT                                     
// CREATION DATE : 07-DEC-2010                                                          
// CREATED BY    : GAURAV SONI                                                      
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


using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Hostel_Report_StudentHostelIdentityCard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAssetController roomassetcon = new RoomAssetController(); // added by sonali on date 08/02/2023
    
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
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=StudentHostelIdentityCard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentHostelIdentityCard.aspx");
        }
    }
    #endregion

    #region Form Events
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController studCont = new StudentController();
            RoomAssetController roomasset = new RoomAssetController();
            DataSet ds;
            int hostelSessionNo = 0;
            int hostelNo = 0;
            int blockno = 0;
            int floorNo = 0;
            if (Session["listStudentHostelIdentityCard"] == null || ((DataTable)Session["listStudentHostelIdentityCard"] == null))
            {
                hostelSessionNo = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                hostelNo = Convert.ToInt32(ddlHostelNo.SelectedValue);
                blockno = Convert.ToInt32(ddlBlockNo.SelectedValue);
                floorNo = Convert.ToInt32(ddlFloor.SelectedValue);
                ds = roomasset.GetStudentSearchForHostelIdentityCard(hostelSessionNo, hostelNo, blockno, floorNo);
                if (ds != null && ds.Tables.Count > 0)
                {
                    lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataBind();
                    hftot.Value = ds.Tables[0].Rows.Count.ToString();
                }
                Session["listStudentHostelIdentityCard"] = ds.Tables[0];
            }
            else
            {
                hostelSessionNo = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                hostelNo = Convert.ToInt32(ddlHostelNo.SelectedValue);
                blockno = Convert.ToInt32(ddlBlockNo.SelectedValue);
                floorNo = Convert.ToInt32(ddlFloor.SelectedValue);
                ds = roomasset.GetStudentSearchForHostelIdentityCard(hostelSessionNo, hostelNo, blockno, floorNo);
                if (ds != null && ds.Tables.Count > 0)
                {
                    lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataBind();
                    hftot.Value = ds.Tables[0].Rows.Count.ToString();
                }
                Session["listStudentHostelIdentityCard"] = ds.Tables[0];
            }
            txtTotStud.Text = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void btnBackReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReport(GetStudentIDs(), "Hostel_Identity_Card_Report_Back", "StudentHostelIdentityCardBack.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.btnBackReport_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
               
                if (cbRow.Checked == true)
                {
                    HiddenField hidIdNo = dataitem.FindControl("hidIdNo") as HiddenField;
                    int idno = Convert.ToInt32(hidIdNo.Value);
                    int hostelno = Convert.ToInt32(ddlHostelNo.SelectedValue);
                    int sessionno = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                    string username = Session["username"].ToString();
                    string ipaddress = Session["ipAddress"].ToString();

                    CustomStatus cs = (CustomStatus)roomassetcon.AddUpdateIdCardStudData(idno, hostelno, sessionno, username, ipaddress);
                    
                }
            }
            ShowReport(GetStudentIDs(), "Hostel_Identity_Card_Report_Front", "StudentHostelIdentityCardFront.rpt");
            btnShow_Click(sender, e);
     
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController studCont = new StudentController();
            DataTable dt;
            if (Session["listStudentHostelIdentityCard"] != null && (DataTable)Session["listStudentHostelIdentityCard"] != null)
            {
                dt = (DataTable)Session["listStudentHostelIdentityCard"];
                string searchText = txtSearchText.Text.Trim();
                string searchBy = ("enrollmentno");
                DataTableReader dtr = studCont.RetrieveStudentDetails(searchText, searchBy).Tables[0].CreateDataReader();
                if (dtr != null && dtr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["NAME"] = dtr["NAME"];
                    dr["ENROLLMENTNO"] = dtr["ENROLLMENTNO"];
                    dr["SHORTNAME"] = dtr["SHORTNAME"];
                    dr["YEARNAME"] = dtr["YEARNAME"];
                    dt.Rows.Add(dr);
                    Session["listStudentHostelIdentityCard"] = dt;
                    lvStudentRecords.DataSource = dt;
                    lvStudentRecords.DataBind();
                    lblEnrollmentNo.Text = string.Empty;
                }
                else
                {
                    lblEnrollmentNo.Text = "Enrollment No. does not match.";
                }
                dtr.Close();
            }
            else
            {
                string searchText = txtSearchText.Text.Trim();
                string searchBy = ("enrollmentno");
                DataTableReader dtr = studCont.RetrieveStudentDetails(searchText, searchBy).Tables[0].CreateDataReader();
                dt = this.GetDataTable();
                DataRow dr = dt.NewRow();
                if (dtr.Read())
                {
                    dr["IDNO"] = dtr["IDNO"];
                    dr["NAME"] = dtr["NAME"];
                    dr["ENROLLMENTNO"] = dtr["ENROLLMENTNO"];
                    dr["SHORTNAME"] = dtr["SHORTNAME"];
                    dr["YEARNAME"] = dtr["YEARNAME"];
                    dt.Rows.Add(dr);
                    Session["qualifyTbl"] = dt;
                    lvStudentRecords.DataSource = dt;
                    lvStudentRecords.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.btnAdd_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
    
    #region Private Methods

    private DataTable GetDataTable()
    {
        DataTable objStudentIdCard = new DataTable();
        objStudentIdCard.Columns.Add(new DataColumn("IDNO", typeof(int)));
        objStudentIdCard.Columns.Add(new DataColumn("NAME", typeof(string)));
        objStudentIdCard.Columns.Add(new DataColumn("ENROLLMENTNO", typeof(string)));
        objStudentIdCard.Columns.Add(new DataColumn("SHORTNAME", typeof(string)));
        objStudentIdCard.Columns.Add(new DataColumn("YEARNAME", typeof(string)));
        return objStudentIdCard;
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        int count = 0;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    count++;
                }

            }

            txtTotStud.Text = count.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            //url += "&param=Hostelsession=" + (DateTime.Today.Year).ToString() + "-" + (DateTime.Today.Year + 1).ToString() + ",@P_IDNO=" + param;
            url += "&param=academicsession=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL DROPDOWN HOSTEL SESSION NO.

            //objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
            objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND IS_SHOW=1", "FLOCK DESC");

            //FILL DROPDOWN HOSTEL NO.
            if (Session["usertype"].ToString() == "1")
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
            else
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    } 
    #endregion
    protected void ddlHostelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlHostelNo.SelectedIndex > 0)
            {
                ddlBlockNo.Items.Clear();
                objCommon.FillDropDownList(ddlBlockNo, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostelNo.SelectedValue), "HB.BLOCK_NAME");
                ddlBlockNo.Focus();
            }
            else
            {
                ddlHostelNo.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ddlHostelNo_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlockNo.SelectedIndex > 0)
            {
                ddlFloor.Items.Clear();
                objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostelNo.SelectedValue + " AND BLK_NO=" + ddlBlockNo.SelectedValue, "FLOOR_NO");
                ddlFloor.Focus();
            }
            else
            {
                ddlBlockNo.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ddlBlockNo_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
