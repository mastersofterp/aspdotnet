//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : SHOW  VACANT ROOMS HOSTEL ROOM                                                  
// CREATION DATE : 19-APRIL-2017                                                       
// CREATED BY    : DIGESH PATEL                                                      
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Hostel_ShowHostelRooms_Master : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelController objhostel = new HostelController();

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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Fill DropDown List
                PopulateDropDownList();
                FillDetail();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_ShowHostelRooms_Master.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HoselVacantRooms.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HoselVacantRooms.aspx");
        }
    }


    #endregion

    #region Form Ecents
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            string strDegreeNo = string.Empty;
            string strGender = string.Empty;
            string strSemesternos = string.Empty;
            string fmiler = "0";
            int blockNo = 0;

            //Comment by shubham as per Discussion with Pankaj sir
            //if (ddlstudtype.SelectedIndex > 0)
            //{
            //    fmiler = ddlstudtype.SelectedValue;
            //}

            int count = 0;

            foreach (ListItem items in cblstDegree.Items)
            {
                if (items.Selected == true)
                {
                    strDegreeNo += items.Value + ",";
                    count++;
                }
            }
            strDegreeNo = strDegreeNo.TrimEnd(',');

            foreach (ListItem items1 in cblgenderd.Items)
            {
                if (items1.Selected == true)
                {
                    strGender += items1.Value + ",";
                    count++;
                }
            }
            strGender = strGender.TrimEnd(',');

            foreach (ListItem items2 in cblstSemester.Items)
            {
                if (items2.Selected == true)
                {
                    strSemesternos += items2.Value + ",";
                    count++;
                }
            }
            strSemesternos = strSemesternos.TrimEnd(',');
            blockNo = Convert.ToInt16(ddlBlock.SelectedValue);

            ds = objhostel.Show_hostelroom(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlHostelNo.SelectedValue), strSemesternos, strDegreeNo, strGender, fmiler, blockNo); // blockNo added by Saurabh L on 02 march 2023

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    pnlStudent.Visible = true;
                    btnsubmit.Enabled = true;
                    btnsubmit.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage("No Record Found!", this.Page);
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    pnlStudent.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("Error!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlStudent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page

            // Response.Redirect(Request.Url.ToString());
            ddlHostelNo.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            GetDegree();
            GetSemester();
            cblgenderd.ClearSelection();
            chkDegree.Checked = false;
            chkSem.Checked = false;
            ddlBlock.SelectedIndex = 0; // Added BUG-ID 163734 
            btnsubmit.Visible = false;  // Added BUG-ID 163734 
            //chkfamiler.Checked = false;
            //ddlstudtype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlHostelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudent.Visible = false;
            
            // Below code added by Saurabh L on 02 March 2023
            if (ddlHostelNo.SelectedIndex > 0)
            {
                ddlBlock.Enabled = true;

                this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostelNo.SelectedValue), "HB.BLOCK_NAME");

            }
            else
            {
                ddlBlock.Enabled = false;
            }
            //---------- End by Saurabh L on 02 March 2023
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_ShowHostelRooms_Master.ddlHostelNo_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string roomsNO = string.Empty;
        int fmiler = 0;
        int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);


        //if (ddlstudtype.SelectedIndex > 0)
        //{
        //    fmiler = Convert.ToInt16(ddlstudtype.SelectedValue);
        //}

        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkSelect") as CheckBox;
            Label lblroomname = item.FindControl("lblroomname") as Label;

            if (chk.Checked)
            {
                if (roomsNO.Length == 0) roomsNO = lblroomname.ToolTip.ToString();
                else
                    roomsNO += "," + lblroomname.ToolTip.ToString();
            }
        }
        if (roomsNO.Length > 0)
        {
            foreach (ListItem items_gender in cblgenderd.Items)
            {
                if (items_gender.Selected == true)
                {
                    foreach (ListItem items_degree in cblstDegree.Items)
                    {
                        if (items_degree.Selected == true)
                        {
                            foreach (ListItem items_semester in cblstSemester.Items)
                            {
                                if (items_semester.Selected == true)
                                {
                                    CustomStatus cs = (CustomStatus)objhostel.Insertshow_hostelroom(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlHostelNo.SelectedValue), Convert.ToUInt16(items_semester.Value), Convert.ToInt16(items_degree.Value), items_gender.Value, roomsNO, fmiler, OrganizationId);
                                    if (cs.Equals(CustomStatus.RecordSaved))
                                    {
                                        objCommon.DisplayMessage("Save Successfully!!!", this.Page);
                                    }

                                }
                               
                            }
                            objCommon.DisplayMessage("Please Select Semester !!!", this.Page);
                        }
                    }
                    objCommon.DisplayMessage("Please Select Degree !!!", this.Page);
                }

            }
            objCommon.DisplayMessage("Please Select Gender !!!", this.Page);
           // cleareall();
        }
        else
        {
            objCommon.DisplayMessage("Please Select Room !!!", this.Page);
        }

        FillDetail();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string reportTitle = "Hoste Room";
        string rptFileName = "rptHosteShowRoom.rpt";
        string strDegreeNo = string.Empty;
        string strGender = string.Empty;
        string strSemesternos = string.Empty;

        int count = 0;

        foreach (ListItem items in cblstDegree.Items)
        {
            if (items.Selected == true)
            {
                strDegreeNo += items.Value + "$";
                count++;
            }
        }
        strDegreeNo = strDegreeNo.TrimEnd('$');

        foreach (ListItem items1 in cblgenderd.Items)
        {
            if (items1.Selected == true)
            {
                strGender += items1.Value + "$";
                count++;
            }
        }
        strGender = strGender.TrimEnd('$');

        foreach (ListItem items2 in cblstSemester.Items)
        {
            if (items2.Selected == true)
            {
                strSemesternos += items2.Value + "$";
                count++;
            }
        }
        strSemesternos = strSemesternos.TrimEnd('$');

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTELSESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_HBNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_DEGREENO=" + strDegreeNo + ",@P_GENDER=" + strGender + ",@P_SEMESTERNO=" + strSemesternos + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Private Methods

    //Below code commented by Saurabh L on 02 March 2023 Purpose: Not in use
    //protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlBlockNo.SelectedIndex > 0)
    //        {
    //            this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostelNo.SelectedValue + " AND BLK_NO=" + ddlBlockNo.SelectedValue, "FLOOR_NO");
    //            ddlFloor.Focus();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.ddlBlockNo_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void PopulateDropDownList()
    {
        try
        {
            //CHANGED IN CONDITION BY SHUBHAM BARKE ON 23.03.22
            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO >0 AND HOSTEL_SESSION_NO >8 ", "HOSTEL_SESSION_NO desc");
            ddlSession.SelectedIndex = 1;

            //FILL DROPDOWN HOSTEL NO
            //CHANGED IN COLUMN NAME AND CONDITION BY SHUBHAM BARKE ON 23.03.22
            objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
            //objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HBNO", "HOSTEL_NAME", "HBNO > 0", "HOSTEL_NAME");

            //FILL DROPDOWN BRANCH
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "BRANCHNO");

            //FILL DROPDOWN RESIDENT TYPE NO
            objCommon.FillDropDownList(ddlResidentTypeNo, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "RESIDENT_TYPE_NO>0", "RESIDENT_TYPE_NO");
            GetDegree();
            GetSemester();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetDegree()
    {
        DataSet ds = objCommon.FillDropDown("ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstDegree.DataTextField = "DEGREENAME";
            cblstDegree.DataValueField = "DEGREENO";
            cblstDegree.DataSource = ds.Tables[0];
            cblstDegree.DataBind();
        }
    }
    private void cleareall()
    {
        ddlHostelNo.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        GetDegree();
        GetSemester();
        cblgenderd.ClearSelection();
        //ddlstudtype.SelectedIndex = 0; 
    }
    private void GetSemester()
    {
        DataSet ds = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstSemester.DataTextField = "SEMESTERNAME";
            cblstSemester.DataValueField = "SEMESTERNO";
            cblstSemester.DataSource = ds.Tables[0];
            cblstSemester.DataBind();
        }
    }
    private void FillDetail()
    {
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_SHOWROOM_MASTER", "RECORD_NO", "FAMILER, STARTED, ('DEGREE->'+DBO.FN_DESC('DEGREENAME',DEGREENO)+' | SEM->'+DBO.FN_DESC('SEMESTER',SEMESTERNO)+' | HOSTEL->'+ DBO.FN_DESC('HOSTELNAME',HBNO)+' | GENDER->' + GENDER)DETAIL ", "HOSTEL_SESSION_NO=" + ddlSession.SelectedValue, "HBNO,DEGREENO");
        if (ds != null && ds.Tables.Count > 0)
        {
            lvdetail.DataSource = ds.Tables[0];
            lvdetail.DataBind();
        }
    }
    #endregion
    protected void btnsub_Click(object sender, EventArgs e)
    {
        try
        {
            int started = 0;
            int recno = 0;
            int familer = 0;
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            CustomStatus cs = new CustomStatus();
            foreach (ListViewDataItem item in lvdetail.Items)
            {
                CheckBox chkstarted = item.FindControl("chkstarted") as CheckBox;
                Label lblroomname = item.FindControl("lblroomname") as Label;
                HiddenField hidrecno = item.FindControl("hidrecno") as HiddenField;
               // DropDownList ddlfamiler = item.FindControl("ddlstudtype") as DropDownList;
                if (chkstarted.Checked)
                {
                    started = 1;
                }
                else
                {
                    started = 0;
                }
                recno = Convert.ToInt16(hidrecno.Value);
                //familer = Convert.ToInt16(ddlfamiler.SelectedValue);
                cs = (CustomStatus)objhostel.Updateshow_hostelroom(recno, started, familer, OrganizationId);
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Save Successfully!!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }

    }
    //protected void lvdetail_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //      string  x = string.Empty; ;

    //      if (e.Item.ItemType == ListViewItemType.DataItem)
    //      {
    //          DropDownList ddlstudtype = (DropDownList)e.Item.FindControl("ddlstudtype");
    //          HiddenField hidfamiler = (HiddenField)e.Item.FindControl("hidfamiler") as HiddenField;
    //          x = hidfamiler.Value;
    //           ddlstudtype.SelectedValue = x.Length > 0 ? x : "0";
 
  
    //      }
    //}

    //added as per Requirement added on 01-09-22 by Shubham
    protected void chkDegree_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cblstDegree.Items)
        {
            item.Selected = chkDegree.Checked;
        }
    }

    //added as per Requirement added on 01-09-22 by Shubham
    protected void chkSem_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cblstSemester.Items)
        {
            item.Selected = chkSem.Checked;
        }
    }
}
