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
using HostelBusinessLogicLayer.BusinessLogic.Hostel;
using IITMS.SQLServer.SQLDAL;
public partial class HOSTEL_HostelAttendence : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAllotmentController objRAC = new RoomAllotmentController();
    HostelAttendanceController objHAC = new HostelAttendanceController();
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
                objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
                ddlSession.SelectedIndex = 1;

                //Below code Added by Saurabh L on 30/12/2022 Purpose: For Crescent get 
                string check = this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "BlockWise_Attendence", "OrganizationId=" + Session["OrgId"] + "");

                if (check == "" || check == "0")
                {
                    divAttenIncharge.Visible = false;
                    ViewState["AttnIncharge"] = 0;

                    if (Session["usertype"].ToString() == "1")
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
                    else
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
                }
                else
                {
                    
                    ViewState["AttnIncharge"] = 1;
                    ddlBlock.SelectedIndex = 0;

                    if (Session["usertype"].ToString() == "1")
                    {
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
                        divAttenIncharge.Visible = false;
                        btnReport.Visible = false;
                        btnSubmit.Visible = false;
                    }
                    else
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL_ATTENDANCE_INCHARGE AI INNER JOIN ACD_HOSTEL H ON (AI.HOSTEL_NO=H.HOSTEL_NO)", "DISTINCT H.HOSTEL_NO", "H.HOSTEL_NAME", " AI.INCHARGE_UANO=" + Convert.ToInt32(Session["userno"].ToString()), "H.HOSTEL_NO");

                   
                }

                


               
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelAttendence.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["AttnIncharge"]) == 1) //If condition added by Saurabh L on 07/01/2023 
        {
            if (Session["usertype"].ToString() == "1")
            {
                FillAdminInchargeListView();
            }
            else
            {
                if (ddlBlock.SelectedValue.ToString() == "0" || ddlBlock.SelectedValue.ToString() == "")
                {
                    objCommon.DisplayMessage("Please Select Block.", this.Page);
                }
                else
                {
                    FillListViewForAttnIncharge();
                }
            }
        }
        else
        {
            FillListView();
        }

    }

    private void FillListView()
    {
        
        DataSet ds = null;
        try
        {
        string check = objCommon.LookUp("ACD_HOSTEL_STUDENT_ATTENDANCE", "HOSTEL_SESSIONNO", "HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND ATT_DATE = CONVERT(DATETIME,'" + txtAttendanceDate.Text.Trim() + "',103)");
        
        if (check == "")
        {
            ds = objCommon.FillDropDown("ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT S ON (S.IDNO=A.RESIDENT_NO) INNER JOIN ACD_HOSTEL_RESIDENT_TYPE T ON (A.RESIDENT_TYPE_NO=T.RESIDENT_TYPE_NO) INNER JOIN ACD_HOSTEL_ROOM R ON (A.ROOM_NO=R.ROOM_NO)", "S.IDNO, S.REGNO", "S.STUDNAME,ENROLLNO,S.ROLLNO,A.ROOM_NO,ROOM_NAME ", " A.CAN=0 AND S.CAN=0 AND IS_STUDENT=1 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue), "S.IDNO");
            //ds = objCommon.FillDropDown("ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT S ON (S.IDNO=A.RESIDENT_NO) INNER JOIN ACD_HOSTEL_RESIDENT_TYPE T ON (A.RESIDENT_TYPE_NO=T.RESIDENT_TYPE_NO) INNER JOIN ACD_HOSTEL_ROOM R ON (A.ROOM_NO=R.ROOM_NO)", "S.IDNO", "S.STUDNAME,ENROLLNO,S.ROLLNO,A.ROOM_NO,ROOM_NAME ", " A.CAN=0 AND S.CAN=0 AND IS_STUDENT=1 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue), "ROOM_NO");
             if (ds.Tables[0].Rows.Count > 0)
             {
                lvDetails.Visible = true;
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
                //Fill Remark
                foreach (ListViewDataItem item in lvDetails.Items)
                {
                    CheckBox chk = item.FindControl("chkIdno") as CheckBox;
                    DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                    //objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "REMARKNO>0", "");
                    objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "ACTIVESTATUS=1", "REMARKNO");
                    chk.Checked = false;
                    chk.Enabled = false;
                }
             }
             else
             {
                 lvDetails.Visible = false;
                 lvDetails.DataSource = null;
                 lvDetails.DataBind();
                 objCommon.DisplayMessage("Record Not Found!!", this.Page);
             }
        }
        else
        {

            ds = objRAC.DailyAttendanceSelect(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), txtAttendanceDate.Text.Trim(), Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
             if (ds.Tables[0].Rows.Count > 0)
             {
                lvDetails.Visible = true;
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
                //Fill Remark
                int i = 0;
                foreach (ListViewDataItem item in lvDetails.Items)
                {
                    CheckBox chkIdno = item.FindControl("chkIdno") as CheckBox;
                    DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                    TextBox txtTime = item.FindControl("txtTime") as TextBox;
                    
                    //objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "REMARKNO>0", "");
                    objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "ACTIVESTATUS=1", "REMARKNO");
                    ddlRemark.SelectedValue = ds.Tables[0].Rows[i]["REMARKNO"].ToString();
                    
                   // if (ddlRemark.SelectedValue == "3")
                    if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "T") //Added by Saurabh l on 06/01/2023
                    {
                        chkIdno.Enabled = false;
                        //txtTime.Enabled = false;
                    }
                    //if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "A" || ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "L")
                    if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "A")
                    {
                        chkIdno.Checked = false;
                        chkIdno.Enabled = false; //Added By Himanshu tamrakar 17-01-2024
                    }
                    else
                    {
                        chkIdno.Checked = true;
                        chkIdno.Enabled = false; //Added By Himanshu tamrakar 17-01-2024
                    }
                    txtTime.Text = ds.Tables[0].Rows[i]["ATT_TIME"].ToString();
                    i++;
                }
             }
            else 
             {
                lvDetails.Visible = false;
                lvDetails.DataSource = null;
                lvDetails.DataBind();
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
             }
            }
        //lblRemark.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelAttendence.FillListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int hostel_sessionno = Convert.ToInt32(ddlSession.SelectedValue);
	        int hostelno=Convert.ToInt32(ddlHostel.SelectedValue);				
	        int ua_no=Convert.ToInt32(Session["userno"].ToString());			
	        string studids= string.Empty;				
	        string att_status= string.Empty;			
	        string remarknos= string.Empty;
            DateTime att_date = Convert.ToDateTime(txtAttendanceDate.Text);
	        string att_time	= string.Empty;
            string college_code = Session["colcode"].ToString();
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            int Count=0;
            //Below Code Added By Himanshu Tamrakar on date 17-01-2024
            foreach (ListViewDataItem item in lvDetails.Items)
            {
                DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                if (!(ddlRemark.SelectedValue).Equals("0"))
                {
                    Count++;
                }
            }
            if (Count == 0)
            {
                objCommon.DisplayMessage("Please Select Remark.", this);
                return;
            }
            //End 
           foreach (ListViewDataItem item in lvDetails.Items)
           {
               CheckBox chkIdno = item.FindControl("chkIdno") as CheckBox;
               DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
               TextBox txtTime = item.FindControl("txtTime") as TextBox;

               //if (chkIdno.Checked == true && txtTime.Enabled == true && txtTime.Text == "")

               // if (ddlRemark.SelectedValue == "3" && chkIdno.Checked == true && chkIdno.Enabled == false && txtTime.Text == "")
               // Below two condition Added by Saurabh L on 15/02/2023 Purpose: when select Late then cross check time format
               if ((ddlRemark.SelectedItem.Text == "Late" || ddlRemark.SelectedItem.Text == "LATE" || ddlRemark.SelectedItem.Text == "late") && txtTime.Text == "")
               {
                   objCommon.DisplayMessage( "Time is Mandatory for Late Remark", this.Page);
                   txtTime.Enabled = true ;
                   return;
               }

               if ((ddlRemark.SelectedItem.Text == "Late" || ddlRemark.SelectedItem.Text == "LATE" || ddlRemark.SelectedItem.Text == "late") && txtTime.Text != "")
                   {
                       string time = txtTime.Text.ToString();
                       string HH = time.Substring(0, 2);
                       string MM = time.Substring(3, 2);

                       if (Convert.ToInt32(HH) > 12)
                       {
                           objCommon.DisplayMessage("Please Enter 12-Hour clock format Time", this.Page);
                           txtTime.Enabled = true;
                           return;
                       }

                       if (Convert.ToInt32(MM) > 59)
                       {
                           objCommon.DisplayMessage("Please Enter 12-Hour clock format Time", this.Page);
                           txtTime.Enabled = true;
                           return;
                       }
                   }

               

               studids += chkIdno.ToolTip+",";

               //below code Added by Saurabh L on 06/01/2023
               string ddlremark = ddlRemark.SelectedItem.Text;

               if (ddlremark == "Present" || ddlremark == "PRESENT" || ddlremark == "present")
               {
                   att_status += "P,";
                   txtTime.Text = ""; //Added By himanshu tamrakar for bug : 170496
               }
               else if (ddlremark == "Absent" || ddlremark == "ABSENT" || ddlremark == "absent")
               {
                   att_status += "A,";
                   txtTime.Text = ""; //Added By himanshu tamrakar for bug : 170496
               }
               else if (ddlremark == "Late" || ddlremark == "LATE" || ddlremark == "late")    //Else if condition added for by default absent mark.
                   att_status += "T,";
               else
                   att_status += "A,";

               //--------------End by Saurabh L on 06/01/2023

               ////added by shubham on 27062022
               //if (ddlRemark.SelectedValue == "3")
               //    att_status += "T,";
               //else if(ddlRemark.SelectedValue=="2")
               //    att_status += "A,";
               //else
               //    att_status += "P,";
               ////---------------------------------------

               //if (chkIdno.Checked == true && txtTime.Text != "")
               //{
               //    att_status += "T,";
               //}
               //else
               //if (chkIdno.Checked)
               //{
               //    att_status += "P,";
               //}
               //else
               //    att_status += "A,";

               remarknos += ddlRemark.SelectedValue+",";
               att_time += txtTime.Text + ",";
           }
           studids =studids.Remove(studids.Length-1);
           att_status = att_status.Remove(att_status.Length - 1);
           remarknos = remarknos.Remove(remarknos.Length-1);
           att_time = att_time.Remove(att_time.Length-1);

            int output= 0;

            //if condition Added by Saurabh L on 04/01/2023 Purpose: For incharge report parameter 
              if (Convert.ToInt32(ViewState["AttnIncharge"]) == 1)
              {
                  if (ddlBlock.SelectedValue.ToString() == "0" || ddlBlock.SelectedValue.ToString() == "")
                  {
                      objCommon.DisplayMessage("Please Select Block.", this.Page);
                  }
                  else
                  {
                      int block = Convert.ToInt32(ddlBlock.SelectedValue);

                      output = objRAC.DailyAttendanceInsertByIncharge(hostel_sessionno, hostelno, ua_no, studids, att_status, remarknos, att_date, att_time, college_code, OrganizationId, block);
                  }
              }
              else
              {
                  output = objRAC.DailyAttendanceInsert(hostel_sessionno, hostelno, ua_no, studids, att_status, remarknos, att_date, att_time, college_code, OrganizationId);
              }

           if (output != -99)
           {
               objCommon.DisplayMessage("Record Saved Successfully!!", this.Page);
               FillListView();
           }
           else
               objCommon.DisplayMessage("Transaction Failed!!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelAttendence.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["AttnIncharge"]) == 1)
        {
            if (ddlBlock.SelectedValue.ToString() == "0" || ddlBlock.SelectedValue.ToString() == "")
            {
                objCommon.DisplayMessage("Please Select Block.", this.Page);
            }
            else
            {
                string check = objCommon.LookUp("ACD_HOSTEL_STUDENT_ATTENDANCE", "HOSTEL_SESSIONNO", "HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + "  AND BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue) + "  AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ATT_DATE = CONVERT(DATETIME,'" + txtAttendanceDate.Text.Trim() + "',103)");

                if (check == "")
                    objCommon.DisplayMessage("Record not found!!", this.Page);
                else
                    ShowReport("HostelAttendanceInchargeDaywise", "HostelAttnInchargeBlockwiseDaywise.rpt");
            }
        }
        else
        {
            string check = objCommon.LookUp("ACD_HOSTEL_STUDENT_ATTENDANCE", "HOSTEL_SESSIONNO", "HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND ATT_DATE = CONVERT(DATETIME,'" + txtAttendanceDate.Text.Trim() + "',103)");

            if (check == "")
                objCommon.DisplayMessage("Record not found!!", this.Page);
            else
                ShowReport("HostelAttendanceDaywise", "HostelAttendanceDaywise.rpt");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;

            if (Convert.ToInt32(ViewState["AttnIncharge"]) == 1)  // Added by Saurabh L on 05/01/2023 Purpose: For incharge report parameter 
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ",@P_ATT_DATE=" + txtAttendanceDate.Text.Trim() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + ",@P_BLOCKNO=" + Convert.ToInt32(ddlBlock.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString());
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ",@P_ATT_DATE=" + txtAttendanceDate.Text.Trim() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            }

           
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_HostelAttendence.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void btnAddRemark_Click(object sender, EventArgs e)
    //{
    //    int output = -99;
    //    output = objM.AddMaster("acd_hostel_attendance_remark", "remarkno,remark,college_code", "remarkno","'"+txtAddRemark.Text.Trim() + "',8");
    //    if (output != -99)
    //    {
    //        lblRemark.Text = "Record Saved Successfully";
    //        txtAddRemark.Text = string.Empty;
    //        //Fill Remark
    //        foreach (ListViewDataItem item in lvDetails.Items)
    //        {
    //            DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
    //            if(ddlRemark.SelectedIndex==0)
    //                objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "REMARKNO>0", "");
    //        }
    //    }
    //    else
    //        lblRemark.Text = "Transaction Failed!!";
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_HostelAttendence.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_HostelAttendence.aspx");
        }
    }

    //Added by Saurabh L on 30/12/20222 Purpose: 
    private void GetFloor(string floors, int BlockNo)
    {
        DataSet ds = objRAC.GetFloorNo(floors, BlockNo);

        if (ds != null && ds.Tables.Count > 0)
        {
            cblstFloor.DataTextField = "FLOOR_NAME";
            cblstFloor.DataValueField = "FLOOR_NO";
            cblstFloor.DataSource = ds.Tables[0];
            cblstFloor.DataBind();
        }
    }

    //Added by Saurabh L on 02/01/20223 Purpose: Attendance Incharge get data
    private void FillListViewForAttnIncharge()
    {

        DataSet ds = null;
        try
        {
            string check = objCommon.LookUp("ACD_HOSTEL_STUDENT_ATTENDANCE", "HOSTEL_SESSIONNO", "HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue) + "AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND  ATT_DATE = CONVERT(DATETIME,'" + txtAttendanceDate.Text.Trim() + "',103)");

            if (check == "")
            {
                ds = objCommon.FillDropDown("ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT S ON (S.IDNO=A.RESIDENT_NO) INNER JOIN ACD_HOSTEL_RESIDENT_TYPE T ON (A.RESIDENT_TYPE_NO=T.RESIDENT_TYPE_NO) INNER JOIN ACD_HOSTEL_ROOM R ON (A.ROOM_NO=R.ROOM_NO) INNER JOIN ACD_HOSTEL_BLOCK_MASTER B ON R.BLOCK_NO=B.BL_NO ", "S.IDNO, S.REGNO", "S.STUDNAME,ENROLLNO,S.ROLLNO,A.ROOM_NO,ROOM_NAME ", " A.CAN=0 AND S.CAN=0 AND IS_STUDENT=1 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.HBNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND R.BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue), "S.IDNO");
                

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvDetails.Visible = true;
                    lvDetails.DataSource = ds;
                    lvDetails.DataBind();
                    //Fill Remark
                    foreach (ListViewDataItem item in lvDetails.Items)
                    {
                        DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                        //objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "REMARKNO>0", "");
                        objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "ACTIVESTATUS=1", "REMARKNO");
                    }
                }
                else
                {
                    lvDetails.Visible = false;
                    lvDetails.DataSource = null;
                    lvDetails.DataBind();
                    objCommon.DisplayMessage("Record Not Found!!", this.Page);
                }
            }
            else
            {

                ds = objRAC.DailyAttendanceSelectInchargeBlockwise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), txtAttendanceDate.Text.Trim(), Convert.ToInt32(ddlBlock.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvDetails.Visible = true;
                    lvDetails.DataSource = ds;
                    lvDetails.DataBind();
                    //Fill Remark
                    int i = 0;
                    foreach (ListViewDataItem item in lvDetails.Items)
                    {
                        CheckBox chkIdno = item.FindControl("chkIdno") as CheckBox;
                        DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                        TextBox txtTime = item.FindControl("txtTime") as TextBox;

                        //objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "REMARKNO>0", "");
                        objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "ACTIVESTATUS=1", "REMARKNO");
                       
                        ddlRemark.SelectedValue = ds.Tables[0].Rows[i]["REMARKNO"].ToString();

                        if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "T")
                        {
                            chkIdno.Enabled = false;
                            chkIdno.Checked = true;
                            txtTime.Enabled = false;
                        }
                        //if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "A" || ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "L")
                        if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "A")
                            chkIdno.Checked = false;
                        else
                        {
                            chkIdno.Checked = true;
                            chkIdno.Enabled = false;
                        }

                        txtTime.Text = ds.Tables[0].Rows[i]["ATT_TIME"].ToString();
                        i++;
                    }
                }
                else
                {
                    lvDetails.Visible = false;
                    lvDetails.DataSource = null;
                    lvDetails.DataBind();
                    objCommon.DisplayMessage("Record Not Found!!", this.Page);
                }
            }
            //lblRemark.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelAttendence.FillListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    // added FillAdminInchargeListView() method by Saurabh L on 07/01/2023 Purpose: When In-charge Attendance Active for college then Admin user show all Incharge Submitted Attendance
    private void FillAdminInchargeListView()
    {
        lvDetails.Visible = false;
        DataSet ds = null;
        try
        {
            string check = objCommon.LookUp("ACD_HOSTEL_STUDENT_ATTENDANCE", "ATT_NO", "HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND ATT_DATE = CONVERT(DATETIME,'" + txtAttendanceDate.Text.Trim() + "',103)");

            if (check == "")
            {
                lvAdmin.Visible = false;
                lvAdmin.DataSource = null;
                lvAdmin.DataBind();
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
            }
            else
            {
                int Hostel_SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                int HostelNo = Convert.ToInt32(ddlHostel.SelectedValue);
                DateTime att_date = Convert.ToDateTime(txtAttendanceDate.Text);

                ds = objHAC.AllInchargeSubmittedAttnDatewise(Hostel_SessionNo, HostelNo, att_date);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvAdmin.Visible = true;
                    lvAdmin.DataSource = ds;
                    lvAdmin.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelAttendence.FillAdminInchargeListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }

    protected void txtAttendanceDate_TextChanged(object sender, EventArgs e)
    {
        string CurDate = DateTime.Now.ToString("dd/MM/yyyy");

        if (txtAttendanceDate.Text != string.Empty && CurDate != "")
        {
            if (Convert.ToDateTime(txtAttendanceDate.Text) <= Convert.ToDateTime(CurDate))
            {
            }
            else
            {
                objCommon.DisplayMessage("Attendance Date should be less OR equal to Current Date.", this.Page);
                txtAttendanceDate.Text = string.Empty;
                txtAttendanceDate.Focus();
                return;
            }
        }
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (Convert.ToInt32(ViewState["AttnIncharge"]) == 1)
        {
            string floorNos = this.objCommon.LookUp("ACD_HOSTEL_ATTENDANCE_INCHARGE", "FLOOR_NOS", "INCHARGE_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue));

            int Block_no = Convert.ToInt32(ddlBlock.SelectedValue);

            this.GetFloor(floorNos, Block_no);


        }
    }
    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["AttnIncharge"]) == 1)
        {
        objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_ATTENDANCE_INCHARGE AI INNER JOIN ACD_HOSTEL_BLOCK B ON (AI.BLOCK_NO = B.BLK_NO) INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "AI.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND AI.INCHARGE_UANO=" + Convert.ToInt32(Session["userno"].ToString()), "HB.BLOCK_NAME");
        }
    }
    protected void chkFloor_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cblstFloor.Items)
        {
            item.Selected = chkFloor.Checked;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            ImageButton ViewButton = sender as ImageButton;
            int att_no = Int32.Parse(ViewButton.CommandArgument);
            int BlockNo = 0;
            int User_No = 0;
            DataSet ds2 = objCommon.FillDropDown("ACD_HOSTEL_STUDENT_ATTENDANCE", "BLOCK_NO", "UA_NO", " ATT_NO=" + att_no, "ATT_NO");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                BlockNo = Convert.ToInt32(ds2.Tables[0].Rows[0]["BLOCK_NO"]);
                User_No = Convert.ToInt32(ds2.Tables[0].Rows[0]["UA_NO"]);
            }


            ds = objRAC.DailyAttendanceSelectInchargeBlockwise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), txtAttendanceDate.Text.Trim(), BlockNo, User_No, Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAdmin.Visible = false;
                lvDetails.Visible = true;
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
                //Fill Remark
                int i = 0;
                foreach (ListViewDataItem item in lvDetails.Items)
                {
                    CheckBox chkIdno = item.FindControl("chkIdno") as CheckBox;
                    DropDownList ddlRemark = item.FindControl("ddlRemark") as DropDownList;
                    TextBox txtTime = item.FindControl("txtTime") as TextBox;

                    //objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "REMARKNO>0", "");
                    objCommon.FillDropDownList(ddlRemark, "ACD_HOSTEL_ATTENDANCE_REMARK", "REMARKNO", "REMARK", "ACTIVESTATUS=1", "REMARKNO");

                    ddlRemark.SelectedValue = ds.Tables[0].Rows[i]["REMARKNO"].ToString();

                    if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "T")
                    {
                        chkIdno.Enabled = false;
                        txtTime.Enabled = false;
                    }
                    //if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "A" || ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "L")
                    if (ds.Tables[0].Rows[i]["ATT_STATUS"].ToString() == "A")
                        chkIdno.Checked = false;
                    else
                        chkIdno.Checked = true;
                    txtTime.Text = ds.Tables[0].Rows[i]["ATT_TIME"].ToString();
                    i++;
                }
            }
            else
            {
                lvDetails.Visible = false;
                lvDetails.DataSource = null;
                lvDetails.DataBind();
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelAttendence.btnView_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}
