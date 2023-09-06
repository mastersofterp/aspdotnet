//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : Hostel Incharge Entry                                                         
// CREATION DATE : 21-DEC-2022                                                          
// CREATED BY    : SONALI BHOR
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================


using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using HostelBusinessLogicLayer.BusinessLogic.Hostel;

public partial class HOSTEL_MASTERS_HostelInchargeEntry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelAttendanceController HAcontroller = new HostelAttendanceController();

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
                // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Session["colcode"] = "1";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

              int check = Convert.ToInt32(this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "BlockWise_Attendence", "OrganizationId="+Session["OrgId"]+""));
              if (check != 1)
              {
                  objCommon.DisplayMessage(this.UpdAtten, "Sorry You Have Not Access of this Page !", this.Page);                 
                  return;
              }



            }
            // hostelname
            PopulateDropDownList();
           BindListView();
            ViewState["action"] = "add";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BlockInfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BlockInfo.aspx");
        }
    }
    #endregion

    #region private methods

    protected void PopulateDropDownList()
    {
        try
        {

            objCommon.FillDropDownList(ddlHostelName, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NO");
            objCommon.FillDropDownList(ddlIncharge, "user_acc", "UA_NO", "UA_NAME", "UA_TYPE not in (2)", "UA_NO");
            this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO>0", "HB.BLOCK_NAME");
            // GetBlock();
             GetFloor();
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            int inchargeid = 0;
            DataSet ds = HAcontroller.GetAllIncharge(inchargeid);
            lvIncharge.DataSource = ds;
            lvIncharge.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Masters_HostelSession.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetBlock()
    {
        DataSet ds = this.objCommon.FillDropDown("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO>0", "HB.BLOCK_NAME");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstBlock.DataTextField = "BLOCK_NAME";
            cblstBlock.DataValueField = "BLK_NO";
            cblstBlock.DataSource = ds.Tables[0];
            cblstBlock.DataBind();
        }
    }

    private void GetBlockByHostel()
    {
        DataSet ds = this.objCommon.FillDropDown("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO>0 AND B.HOSTEL_NO="+ Convert.ToInt32(ddlHostelName.SelectedValue)+"", "HB.BLOCK_NAME");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstBlock.DataTextField = "BLOCK_NAME";
            cblstBlock.DataValueField = "BLK_NO";
            cblstBlock.DataSource = ds.Tables[0];
            cblstBlock.DataBind();
        }
    }

    private void GetFloor()
    {
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_FLOOR F", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "FLOOR_NO>0 and ACTIVESTATUS=1", "FLOOR_NO");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstFloor.DataTextField = "FLOOR_NAME";
            cblstFloor.DataValueField = "FLOOR_NO";
            cblstFloor.DataSource = ds.Tables[0];
            cblstFloor.DataBind();
        }
    }


    private void GetFloorByHostel()
    {
        //string vari = string.Empty;
        //foreach (ListItem item in cblstBlock.Items)
        //{
        //    if (item.Selected)
        //    {
        //        vari += item.Value + ",";              
        //    }                 
        //}
        //vari = vari.TrimEnd(',');

        //DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLK_NO IN (" + vari + ")", "FLOOR_NO");

        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLK_NO="+ddlBlock.SelectedValue+"", "FLOOR_NO");
                if (ds != null && ds.Tables.Count > 0)
                {
                    cblstFloor.DataTextField = "FLOOR_NAME";
                    cblstFloor.DataValueField = "FLOOR_NO";
                    cblstFloor.DataSource = ds.Tables[0];
                    cblstFloor.DataBind();
                }
       
    }

    private void clearAll()
    {
        ddlHostelName.SelectedIndex = 0;
        ddlIncharge.SelectedIndex = 0;
        GetBlock();
        GetFloor();
        chkBlock.Checked = false;
        chkFloor.Checked = false;
        cblstBlock.ClearSelection();
        cblstFloor.ClearSelection();
        ddlBlock.SelectedIndex = 0;
        ViewState["action"] = "add";
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {

            string inchargeno = objCommon.LookUp("ACD_HOSTEL_ATTENDANCE_INCHARGE", "INCHARGE_ID", "(INCHARGE_UANO='" + ddlIncharge.SelectedValue + "' AND HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLOCK_NO="+ ddlBlock.SelectedValue +"  AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])+" ) OR (HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLOCK_NO="+ ddlBlock.SelectedValue +"  AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])+")");
            if (inchargeno != null && inchargeno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Room.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int inchargeid)
    {
        bool flag = false;
        try
        {
            //string inchargeno = objCommon.LookUp("ACD_HOSTEL_ATTENDANCE_INCHARGE", "INCHARGE_ID", "INCHARGE_ID != " + inchargeid + " AND INCHARGE_UANO='" + ddlIncharge.SelectedValue + "' AND HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLOCK_NO=" + ddlBlock.SelectedValue );

            string inchargeno = objCommon.LookUp("ACD_HOSTEL_ATTENDANCE_INCHARGE", "INCHARGE_ID", "(INCHARGE_ID != " + inchargeid + ") AND ((INCHARGE_UANO='" + ddlIncharge.SelectedValue + "' AND HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLOCK_NO=" + ddlBlock.SelectedValue + "  AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " ) OR (HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND BLOCK_NO=" + ddlBlock.SelectedValue + "  AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "))");

            if (inchargeno != null && inchargeno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Room.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }


    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    #endregion
    protected void chkBlock_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cblstBlock.Items)
        {       
            item.Selected = chkBlock.Checked ;
        }
        this.GetFloorByHostel();
    }
    protected void chkFloor_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cblstFloor.Items)
        {
             item.Selected = chkFloor.Checked ;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int inchargeno = 0;
            int hostelno = Convert.ToInt32(ddlHostelName.SelectedValue);
            int blockno = Convert.ToInt32(ddlBlock.SelectedValue);
            string floornos = string.Empty;
            int inchargeuano = Convert.ToInt32(ddlIncharge.SelectedValue);
            string ipaddress = Session["ipAddress"].ToString();
            int userno = Convert.ToInt32(Session["userno"].ToString());
            int collegecode = Convert.ToInt32(Session["colcode"].ToString());
            int orgid = Convert.ToInt32(Session["OrgId"].ToString());

          
            foreach (ListItem item in cblstFloor.Items)
            {
                if (item.Selected)
                {
                    floornos += item.Value + ",";
                }
            }
            floornos = floornos.TrimEnd(',');
           
            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add Hostel Session
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage(this.UpdAtten,"Entry for this Selection Already Done,You can Edit the existing Entry!", this.Page);
                        return;
                    }

                    cs = (CustomStatus)HAcontroller.AddUpdateHostelAttendanceIncharge(inchargeno, hostelno, blockno, floornos, inchargeuano, ipaddress, userno, collegecode, orgid);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this.UpdAtten,"Record Saved Successfully!!!.", this.Page);
                        clearAll();
                    }
                }

                /// Update Asset
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    inchargeno = (GetViewStateItem("INCHARGE_ID") != string.Empty ? int.Parse(GetViewStateItem("INCHARGE_ID")) : 0);
                    if (CheckDuplicateEntryUpdate(inchargeno) == true)
                    {
                        objCommon.DisplayMessage(this.UpdAtten, "Entry for this Selection Already Done!", this.Page);
                        return;
                    }
                    cs = (CustomStatus)HAcontroller.AddUpdateHostelAttendanceIncharge(inchargeno, hostelno, blockno, floornos, inchargeuano, ipaddress, userno, collegecode, orgid);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UpdAtten,"Record Updated Successfully!!!.", this.Page);
                        clearAll();
                    }
                }

                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.objCommon.DisplayMessage(this.UpdAtten,"Unable to complete the operation.", this.Page);
                else
                    this.BindListView();
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.clearAll();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton editbutton = sender as ImageButton;
            int inchargeno = Convert.ToInt32(editbutton.CommandArgument);

            DataSet ds = HAcontroller.GetAllIncharge(inchargeno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                ddlHostelName.SelectedValue = dr["HOSTEL_NO"] == null ? string.Empty : dr["HOSTEL_NO"].ToString();
                ddlHostelName_SelectedIndexChanged(sender,e);
                ddlBlock.SelectedValue = dr["BLOCK_NO"] == null ? string.Empty : dr["BLOCK_NO"].ToString();
                ddlIncharge.SelectedValue = dr["INCHARGE_UANO"] == null ? string.Empty : dr["INCHARGE_UANO"].ToString();
                GetFloorByHostel();
            
                ViewState["action"] = "edit";
                ViewState["INCHARGE_ID"] = dr["INCHARGE_ID"].ToString();
               // ViewState["FLOORS"] = dr["FLOOR_NOS"].ToString();
                
                string[] floornolist = dr["FLOOR_NOS"].ToString().Split(',');

                foreach (string floorno in floornolist)
                {
                    foreach (ListItem li in cblstFloor.Items)
                    {
                        if (li.Value == floorno)
                        {
                            li.Selected = true;
                        }
                    }

                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnEdit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }
    protected void ddlHostelName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHostelName.SelectedIndex > 0)
        {
           // this.GetBlockByHostel();

            this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO>0 AND B.HOSTEL_NO=" + Convert.ToInt32(ddlHostelName.SelectedValue) + "", "HB.BLOCK_NAME");
        }
        else
        {
            objCommon.DisplayMessage(this.UpdAtten,"Please Select Hostel Name.", this.Page);
        }
    }
    protected void cblstBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ( Convert.ToInt32(cblstBlock.SelectedValue) > 0)
        {
            GetFloorByHostel();
        }
        else
        {
            objCommon.DisplayMessage(this.UpdAtten,"Please Select Block Name.", this.Page);
        }
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex > 0)
        {
            GetFloorByHostel();
        }
        else
        {
            objCommon.DisplayMessage(this.UpdAtten,"Please Select Block Name.", this.Page);
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Hostel_Attendance_Incharge", "HostelAttendanceInchargeDataReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_RoomAllotmentStatus.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int hostelno = Convert.ToInt32(ddlHostelName.SelectedValue);

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_NO=" + hostelno + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}