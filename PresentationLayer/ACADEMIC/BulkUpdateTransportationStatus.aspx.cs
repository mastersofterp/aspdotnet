using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_BulkUpdateTransportationStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    QrCodeController objQrC = new QrCodeController();
    int prev_status;

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
                    pnltextbox.Visible = false;
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                int utyp = Convert.ToInt32(Session["usertype"].ToString());
                if (utyp.Equals(1))
                {
                    btnLock.Visible = false;
                    btnUnlock.Visible = false;
                }
                else
                {
                    btnLock.Visible = false;
                    btnUnlock.Visible = false;
                }

            }
            divMsg.InnerHtml = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0 AND ActiveStatus=1", "college_id");
            // objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            // FILL DROPDOWN YEAR
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
            // ddlYear.SelectedValue = "1";
            this.objCommon.FillDropDownList(ddlSearchPanel, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
            ddlSearchPanel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
        divhostel.Visible = true;
        divtransport.Visible = true;
            this.BindListView();
            //this.Bindlvstudlist();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds;
            ds = studCont.GetStudentListForTransportationStatus(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();

                foreach (ListViewDataItem item in lvStudentRecords.Items)
                {
                    HiddenField TRANSTATUSNO = item.FindControl("hdTRANSTATUSNO") as HiddenField;
                    CheckBox chkAccept = item.FindControl("chkReport") as CheckBox;

                    int Number = Convert.ToInt32(TRANSTATUSNO.Value);
                    if (Number !=0)
                    {
                        chkAccept.Checked = true;
                    }
                    else
                    {
                        chkAccept.Checked = false;
                    }
                }

                lvStudentRecords.Visible = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label - 
            }

            else
            {
                objCommon.DisplayMessage(updtime, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void Bindlvstudlist()
        {
        try
        {
        pnlLV.Visible = false;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;

        //string college = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + (Session["IDNO"]));
        //string Admabatch = objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + (Session["IDNO"]));
        //string Degree = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + (Session["IDNO"]));
        //string BRANCH = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + (Session["IDNO"]));
        //string Year = objCommon.LookUp("ACD_STUDENT", "YEAR", "IDNO=" + (Session["IDNO"]));
        //string SEMESTER = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + (Session["IDNO"])); 
        btnsubmit1.Visible = true;
        lblNoRecords.Visible = false;
            DataSet ds;
            ds = studCont.GetStudentListForSingleStudentHosteller(Convert.ToInt32(Session["IDNO"]));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvStudlist.DataSource = ds;
                lvStudlist.DataBind();
               

                foreach (ListViewDataItem item in lvStudlist.Items)
                {
                    HiddenField TRANSTATUSNO = item.FindControl("hdTRANSTATUSNO1") as HiddenField;
                    CheckBox chkAccept = item.FindControl("chkReport1") as CheckBox;

                    int Number = Convert.ToInt32(TRANSTATUSNO.Value);
                    if (Number !=0)
                    {
                        chkAccept.Checked = true;
                    }
                    else
                    {
                        chkAccept.Checked = false;
                    }
                }
                btnsubmit1.Visible = true;
                lvStudlist.Visible = true;
                hftot1.Value = lvStudlist.Items.Count.ToString();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label - 
            }

            else
            {
                objCommon.DisplayMessage(updtime, "Record Not Found!!", this.Page);
                lvStudlist.DataSource = null;
                lvStudlist.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
       

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            Session["listIdCard"] = null;
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + ddlYear.SelectedValue + ",@P_PREV_STATUS=" + prev_status + ",@P_USER_FUll_NAME=" + Session["userfullname"];
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_PREV_STATUS=" + prev_status + ",@P_DATEOFISSUE=" + txtDateofissue.Text.ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                string studentIds = string.Empty;
                foreach (ListViewDataItem lvItem in lvStudentRecords.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkReport") as CheckBox;
                    if (chkBox.Checked == true)
                    {
                        studentIds += chkBox.ToolTip + ",";
                        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip) + ""));

                    }
                }

                int chkg = studCont.BulkInsAdmitCardLog(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), studentIds, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]));

                ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");

            }
            else
            {
                objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
        objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
        objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
        objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
        objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();

            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;

                //DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport");
                //DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller");
                //DropDownList ddlHostellerType = (DropDownList)itm.FindControl("ddlHostellerType");


                //objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;
                
                if (chk.Checked.Equals(true) && chk.Enabled.Equals(true))
                {

                    objS.IdNo = Convert.ToInt32(hdnf.Value);

                    if (Convert.ToInt32(ddlhosteller.SelectedValue) == -1 && Convert.ToInt32(ddtransportstatus.SelectedValue) == -1)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Transporation Status or Hosteller Status", this.Page);
                        return;
                    }
                    if (Convert.ToInt32(ddlhosteller.SelectedValue) > -1 || Convert.ToInt32(ddlHosteltypes.SelectedValue) > 0 || Convert.ToInt32(ddltransportStatus.SelectedValue) < 1)
                    {
                       
                        //objS.Hosteler = Convert.ToBoolean(1);
                        //objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        //flag = true;
                    objS.Hosteler = Convert.ToInt32(ddlhosteller.SelectedValue);
                    objS.HostelSts = Convert.ToInt32(ddlHosteltypes.SelectedValue);
                    objS.HostelSts = Convert.ToInt32(ddlHosteltypes.SelectedValue);
                        flag = true;
                    }
                

                    //if (Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) < 1|| Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) < 1)
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Please Select Transporatation Status and Hosteller Status.", this.Page);
                    //    return;
                    //}
                    if (Convert.ToInt32(ddtransportstatus.SelectedValue) > -1)
                    {
                        //objS.Transportation = 1;
                    objS.Transportation = Convert.ToInt32(ddtransportstatus.SelectedValue);
                    objS.TransportSts = Convert.ToInt32(ddtransportstatus.SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                    }



                    if (Convert.ToInt32(ddlhosteller.SelectedValue) > -1)
                    {
                        
                        //objS.Hosteler = Convert.ToBoolean(1);
                        //objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        //flag = true;
                    objS.Hosteler = Convert.ToInt32(ddlhosteller.SelectedValue);
                    objS.HostelSts = Convert.ToInt32(ddlHosteltypes.SelectedValue);
                        flag = true;
                    }
                    
                    else
                    {
                        objS.Hosteler = 0;
                        objS.HostelSts = 0;
                        flag = true;
                        //objS.Hosteler = Convert.ToBoolean(0);
                        //objS.HostelSts = 0;
                        //flag = true;
                    }

                    //hostelertype
                    if (Convert.ToInt32(ddlhosteller.SelectedValue) == 1)
                    {
                    if (Convert.ToInt32(ddlHosteltypes.SelectedValue) < 1)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select Hosteller Type.", this.Page);
                            return;
                        }
                   }

                    if (Convert.ToInt32(ddlHosteltypes.SelectedValue) > 0)
                        {
                            //objS.Transportation = 1;
                        objS.HostelSts = Convert.ToInt32(ddlHosteltypes.SelectedValue);

                            flag = true;
                            ddlHostellerType.Enabled = true;

                        }
                    
                    else
                    {
                        //objS.Transportation = 0;
                        objS.HostelSts = 0;
                        flag = true;
                    }
                    //

                    //if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > 0)
                    // {
                    //objS.Hosteler = Convert.ToBoolean(1);
                    //if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 1)
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(1);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    //else if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 2)
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(1);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    //else if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 3)
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(1);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    //else
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(0);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    // }
                    // else
                    //{
                    //objS.Hosteler = Convert.ToBoolean(0);
                    // objS.HostelSts = 0;
                    //flag = true;
                    // }
                    BindListView();
                    // objCommon.DisplayMessage(this.updtime, "Please Select No Dues Status.", this.Page);

                    if (flag.Equals(true))
                    {
                        output = studCont.AddTransportStatusForStudent(objS);
                    }
                }
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(Page, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(Page, "Student Status Alloted Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    
    protected void lvStudentRecords_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
               

                DropDownList ddlHostellerType = (DropDownList)e.Item.FindControl("ddlHostellerType");
                objCommon.FillDropDownList(ddlHostellerType, "ACD_HOSTEL_TYPE", "HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO>0 ", "HOSTEL_TYPE_NO");
               

                ddlHostellerType.Enabled = false;
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                HiddenField hdfn = e.Item.FindControl("hidIdNo") as HiddenField;
                CheckBox chkdis = e.Item.FindControl("chkReport") as CheckBox;


                int idno = Convert.ToInt32(hdfn.Value);

                int HostellerType = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S LEFT JOIN ACD_TRANSPORTATION_BULK_UPDATE_STATUS TS ON S.IDNO=TS.IDNO AND S.SEMESTERNO=TS.SEMESTERNO ", "ISNULL(TS.HOSTEL_STATUS,0) AS STUD_HOSTEL_STATUS", "S.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.[YEAR]=" + Convert.ToInt32(ddlYear.SelectedValue) + " AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND S.IDNO=" + idno));
                if (HostellerType != null)
                {
                    ddlHostellerType.SelectedValue = HostellerType.ToString();
                }



                string exist1 = objCommon.LookUp("ACD_TRANSPORTATION_BULK_UPDATE_STATUS", "COUNT(1)", "IDNO='" + idno + "'");

                //if ((dr["STUD_TRANSPORTATION"].Equals(1)))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(1);

                //}
                //else if ((dr["STUD_TRANSPORTATION"].Equals(0)))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(0);

                //}
                ////else if ((dr["STUD_TRANSPORTATION"].Equals(-1) && dr["STUD_HOSTELER"].Equals(-1)))
                ////{
                ////    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                ////}             
                //else
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(-1);
                //}

                //if (dr["STUD_HOSTELER"].Equals(1))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(1);
                //}
                //else if (dr["STUD_HOSTELER"].Equals(0))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(0);
                //}
                ////else if ((dr["STUD_TRANSPORTATION"].Equals(0) && dr["STUD_HOSTELER"].Equals(0)))
                ////{
                ////    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                ////}
                ////else if (dr["STUD_HOSTEL_STATUS"].Equals(0) || )
                ////{
                ////    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                ////}
                //else
                //{
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(-1);
                //}

                //if (dr["LOCK"].Equals(1))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((CheckBox)e.Item.FindControl("chkReport")).Enabled = false;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = false;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = false;
                //}
                //else if (dr["UNLOCK"].Equals(0))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((CheckBox)e.Item.FindControl("chkReport")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                //}



                if (Convert.ToInt32(exist1) > 0)
                {
                    //if (dr["STUD_TRANSPORTATION"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(1);

                    //}
                    //else if ((dr["STUD_TRANSPORTATION"].Equals(0)))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(0);

                    //}
                    //else if (dr["TRANS_STATUS"].Equals(0) && dr["STUD_TRANS_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //}
                    //else if (dr["TRANS_STATUS"].Equals(0) || dr["STUD_TRANS_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;

                    //}
                    //else
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(-1);
                    //}

                    //if (dr["STUD_HOSTELER"].Equals(Convert.ToBoolean(1)))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(1);
                    //    //objCommon.FillDropDownList(ddlHosteller, "ACD_HOSTEL_FESS_TYPE", "HID", "HOSTELTYPE", "HID>0 ", "HID");
                    //}
                    //else if (dr["STUD_HOSTELER"].Equals(Convert.ToBoolean(0)))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(0);
                        
                    //}
                    
                    //if (dr["HOSTEL_STATUS"].Equals(1) && dr["STUD_HOSTEL_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(1);
                    //}
                    //if (dr["HOSTEL_STATUS"].Equals(2) && dr["STUD_HOSTEL_STATUS"].Equals(2))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(2);
                    //}
                    //if (dr["HOSTEL_STATUS"].Equals(3) && dr["STUD_HOSTEL_STATUS"].Equals(3))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(3);
                    //}
                    //if (dr["HOSTEL_STATUS"].Equals(4) && dr["STUD_HOSTEL_STATUS"].Equals(4))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(4);
                    //}

                    //else if (dr["HOSTEL_STATUS"].Equals(0) || dr["STUD_HOSTEL_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(0);


                    //}
                        
                    //else
                    //{
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(-1);
                    //}

                    if (dr["LOCK"].Equals(1))
                    {
                        ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                        ((CheckBox)e.Item.FindControl("chkReport")).Enabled = false;
                        ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = false;
                        ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = false;
                    }
                    else if (dr["UNLOCK"].Equals(0))
                    {
                        ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                        ((CheckBox)e.Item.FindControl("chkReport")).Enabled = true;
                        ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                        ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                    }

                }
                else
                {
                    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(-1);
                    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(-1);

                    //if (dr["STUD_TRANS_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(1);

                    //}
                    //else if (dr["STUD_TRANS_STATUS"].Equals(2))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(2);

                    //}
                    //else if (dr["STUD_TRANS_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;

                    //}
                    //else
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(0);
                    //}

                    //if (dr["STUD_HOSTEL_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(1);
                    //}
                    //else if (dr["STUD_HOSTEL_STATUS"].Equals(2))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(2);
                    //}
                    //else if (dr["STUD_HOSTEL_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;

                    //}
                    //else
                    //{

                    //}
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlAdmbatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlhostelerstatus.SelectedIndex = 0;
        ddltransportStatus.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void rbRegEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    public void ClearForBulkStudent()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;
        Panel1.Visible = false;
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();


            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;

                DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport");
                DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller");


                objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;

                if (chk.Checked.Equals(true) && chk.Enabled.Equals(true))
                {
                    objS.Lock = 1;
                    objS.UnLock = 0;
                    if (Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) > -1)
                    {
                        //objS.Transportation = 1;
                        objS.Transportation = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        objS.TransportSts = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                    }

                    if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > -1)
                    {
                        //objS.Hosteler = Convert.ToBoolean(1);
                        //objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        //flag = true;
                        if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 1)
                        {
                            //objS.Hosteler = Convert.ToBoolean(1);
                            objS.Hosteler = 0;
                            objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                            flag = true;
                        }
                      
                        else
                        {
                            //objS.Hosteler = Convert.ToBoolean(0);
                            objS.Hosteler = 0;
                            objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                            flag = true;
                        }
                    }
                    else
                    {
                        //objS.Hosteler = Convert.ToBoolean(0);
                        objS.Hosteler = 0;
                        objS.HostelSts = 0;
                        flag = true;
                    }
                    BindListView();
                    if (flag.Equals(true))
                    {
                        output = studCont.AddLockTransportStatusForStudent(objS);
                    }
                }
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student Status Lock Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();


            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;

                DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport");
                DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller");


                objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;

                if (chk.Checked.Equals(true) && chk.Enabled.Equals(false))
                {
                    objS.Lock = 0;
                    objS.UnLock = 1;
                    if (Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue) > -1)
                    {
                        //objS.Transportation = 1;
                        objS.Transportation = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        objS.TransportSts = Convert.ToInt32((itm.FindControl("ddlTransport") as DropDownList).SelectedValue);
                        flag = true;
                    }
                    else
                    {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                    }

                    if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > -1)
                    {
                        //objS.Hosteler = Convert.ToBoolean(1);
                        //objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        //flag = true;
                        if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 1)
                        {
                            //objS.Hosteler = Convert.ToBoolean(1);
                            objS.Hosteler = 0;
                            objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                            flag = true;
                        }
                        else
                        {
                            objS.Hosteler = 0;
                            //objS.Hosteler = Convert.ToBoolean(0);
                            objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                            flag = true;
                        }
                    }
                    else
                    {
                         objS.Hosteler = 0;
                        //objS.Hosteler = Convert.ToBoolean(0);
                        objS.HostelSts = 0;
                        flag = true;
                    }

                   
                    BindListView();

                    if (flag.Equals(true))
                    {
                        output = studCont.AddLockTransportStatusForStudent(objS);
                    }
                }
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student Status Unlock Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlHosteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvStudentRecords.Items)
        {
            DropDownList ddlTransport = (DropDownList)item.FindControl("ddlTransport");
            DropDownList ddlhosteller = (DropDownList)item.FindControl("ddlHosteller");
            DropDownList ddlHostellerType = (DropDownList)item.FindControl("ddlHostellerType");
            if (ddlhosteller.SelectedItem.Text == "Yes")
            {
                ddlHostellerType.Enabled = true;             
                ddlTransport.Enabled = true;
                ddlTransport.SelectedValue = "-1";
                ddlTransport.SelectedValue = "0";
               
            }
            else if (ddlhosteller.SelectedItem.Text == "No")
            {
                ddlHostellerType.Enabled = false;
                ddlHostellerType.SelectedValue = "0";
                ddlTransport.Enabled = true;
                ddlTransport.SelectedValue = "1";
            }
        }
    }
    protected void ddlTransport_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvStudentRecords.Items)
        {
            DropDownList ddlhosteller = (DropDownList)item.FindControl("ddlHosteller");
            DropDownList ddlTransport = (DropDownList)item.FindControl("ddlTransport");
            DropDownList ddlHostellerType = (DropDownList)item.FindControl("ddlHostellerType");
            //if (ddlTransport.SelectedItem.Text == "No" || ddlTransport.SelectedItem.Text == "Please select")
            //{
            //    ddlhosteller.Enabled = true;
            //}
            //else
            //{
            //    ddlhosteller.Enabled = true;
            //}
            if (ddlTransport.SelectedItem.Text == "Yes")
            {
                ddlHostellerType.Enabled = false;
                ddlHostellerType.SelectedValue = "0";
                ddlhosteller.Enabled = true;
                ddlhosteller.SelectedValue = "-1";
                ddlhosteller.SelectedValue = "0";

            }
            else if (ddlTransport.SelectedItem.Text == "No")
            {
                ddlHostellerType.Enabled = true;
                ddlhosteller.Enabled = true;
                ddlhosteller.SelectedValue = "1";
            }
          
        }
    }

    protected void ddlSearchPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            
            if (ddlSearchPanel.SelectedIndex > 0)
                {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearchPanel.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                        {
                        pnltextbox.Visible = false;
                        txtSearchPanel.Visible = false;
                        pnlDropdown.Visible = true;
                        divpanel.Attributes.Add("style", "display:block");
                        divDropDown.Attributes.Add("style", "display:block");
                        //divSearchPanel.Attributes.Add("style", "display:block");
                       // divStudInfo.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:none");
                        //divtxt.Visible = false;
                        lblDropdown.Text = ddlSearchPanel.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                        }
                    else
                        {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearchPanel.Visible = true;
                        // pnlDropdown.Visible = false;
                        divDropDown.Attributes.Add("style", "display:none");
                      //  divStudInfo.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:block");
                        divpanel.Attributes.Add("style", "display:block");
                        }
                    }
                }
            else
                {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
                divpanel.Attributes.Add("style", "display:none");

                }
          //  ClearSelection();
            }
        catch
            {
            throw;
            }
        }
    protected void btnSearchPanel_Click(object sender, EventArgs e)
        {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            {
            value = ddlDropdown.SelectedValue;
            }
        else
            {
            value = txtSearchPanel.Text;
            }
        bindlist(ddlSearchPanel.SelectedItem.Text, value);
        //ddlDropdown.ClearSelection();
        //txtSearchPanel.Text = string.Empty;
        //pnltextbox.Visible = false;
        //pnlDropdown.Visible = false;
        //divpanel.Attributes.Add("style", "display:none");
        }

    private void bindlist(string category, string searchtext)
        {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsFeeCollection(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
            {
            pnlLV.Visible = true;
            lvStudentPanel.Visible = true;
            lvStudentPanel.DataSource = ds;
            lvStudentPanel.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentPanel);//Set label - 
            }
        else
            {
            ddlSearchPanel.ClearSelection();
            lblNoRecords.Text = "Total Records : 0";
            //objCommon.DisplayMessage("","");
            objCommon.DisplayUserMessage(this.Page, "Record Not Found,Kindly Check for Demand is Created or Not.", this.Page);
            lvStudentPanel.Visible = false;
            lvStudentPanel.DataSource = null;
            lvStudentPanel.DataBind();
            }
        }

    protected void btnClosePanel_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());

        }
    protected void lnkIdPanel_Click(object sender, EventArgs e)
        {
        try
            {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            //if (Request.Url.ToString().IndexOf("&id=") > 0)
            //    url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            //else
            //    url = Request.Url.ToString();

            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            Label lblSem = lnk.Parent.FindControl("lblSemester") as Label;
            Label lblreceipttype = lnk.Parent.FindControl("lblReceipttype") as Label;
            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["IDNO"] = Convert.ToInt32(lnk.CommandArgument);
           // hdnID.Value = idno.ToString();
            txtSearchPanel.Text = lblenrollno.Text;
            ViewState["semesterno"] = lblSem.ToolTip;
            ViewState["ReceiptType"] = lblreceipttype.ToolTip;
            Bindlvstudlist();
            lvStudlist.Visible = true;
            pnlLV.Visible = false;
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lvStudentRecords.Visible = false;
            }
        catch (Exception ex)
            {
            //if (Convert.ToBoolean(Session["error"]) == true)
                //objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.ddlReceiptType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
           // else
               // objUaimsCommon.ShowError(Page, "Server Unavailable.");
           }
        }
    protected void lvStudlist_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        try
            {

            if ((e.Item.ItemType == ListViewItemType.DataItem))
                {


                DropDownList ddlHostellerType = (DropDownList)e.Item.FindControl("ddlHostellerType1");
                objCommon.FillDropDownList(ddlHostellerType, "ACD_HOSTEL_TYPE", "HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO>0 ", "HOSTEL_TYPE_NO");


                //ddlHostellerType.Enabled = false;
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                HiddenField hdfn = e.Item.FindControl("hidIdNo1") as HiddenField;
                CheckBox chkdis = e.Item.FindControl("chkReport1") as CheckBox;


                int idno = Convert.ToInt32(hdfn.Value);

                int HostellerType = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S LEFT JOIN ACD_TRANSPORTATION_BULK_UPDATE_STATUS TS ON S.IDNO=TS.IDNO AND S.SEMESTERNO=TS.SEMESTERNO ", "ISNULL(TS.HOSTEL_STATUS,0) AS STUD_HOSTEL_STATUS",  "S.IDNO=" + idno));
                if (HostellerType != null)
                    {
                    ddlHostellerType.SelectedValue = HostellerType.ToString();
                    }



                string exist1 = objCommon.LookUp("ACD_TRANSPORTATION_BULK_UPDATE_STATUS", "COUNT(1)", "IDNO='" + idno + "'");

                //if ((dr["STUD_TRANSPORTATION"].Equals(1)))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(1);

                //}
                //else if ((dr["STUD_TRANSPORTATION"].Equals(0)))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(0);

                //}
                ////else if ((dr["STUD_TRANSPORTATION"].Equals(-1) && dr["STUD_HOSTELER"].Equals(-1)))
                ////{
                ////    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                ////}             
                //else
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(-1);
                //}

                //if (dr["STUD_HOSTELER"].Equals(1))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(1);
                //}
                //else if (dr["STUD_HOSTELER"].Equals(0))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(0);
                //}
                ////else if ((dr["STUD_TRANSPORTATION"].Equals(0) && dr["STUD_HOSTELER"].Equals(0)))
                ////{
                ////    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                ////}
                ////else if (dr["STUD_HOSTEL_STATUS"].Equals(0) || )
                ////{
                ////    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                ////}
                //else
                //{
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(-1);
                //}

                //if (dr["LOCK"].Equals(1))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((CheckBox)e.Item.FindControl("chkReport")).Enabled = false;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = false;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = false;
                //}
                //else if (dr["UNLOCK"].Equals(0))
                //{
                //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                //    ((CheckBox)e.Item.FindControl("chkReport")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                //}



                if (Convert.ToInt32(exist1) > 0)
                    {
                    //if (dr["STUD_TRANSPORTATION"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(1);

                    //}
                    //else if ((dr["STUD_TRANSPORTATION"].Equals(0)))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(0);

                    //}
                    //else if (dr["TRANS_STATUS"].Equals(0) && dr["STUD_TRANS_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //}
                    //else if (dr["TRANS_STATUS"].Equals(0) || dr["STUD_TRANS_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;

                    //}
                    //else
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(-1);
                    //}

                    //if (dr["STUD_HOSTELER"].Equals(Convert.ToBoolean(1)))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(1);
                    //    //objCommon.FillDropDownList(ddlHosteller, "ACD_HOSTEL_FESS_TYPE", "HID", "HOSTELTYPE", "HID>0 ", "HID");
                    //}
                    //else if (dr["STUD_HOSTELER"].Equals(Convert.ToBoolean(0)))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(0);

                    //}

                    //if (dr["HOSTEL_STATUS"].Equals(1) && dr["STUD_HOSTEL_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(1);
                    //}
                    //if (dr["HOSTEL_STATUS"].Equals(2) && dr["STUD_HOSTEL_STATUS"].Equals(2))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(2);
                    //}
                    //if (dr["HOSTEL_STATUS"].Equals(3) && dr["STUD_HOSTEL_STATUS"].Equals(3))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(3);
                    //}
                    //if (dr["HOSTEL_STATUS"].Equals(4) && dr["STUD_HOSTEL_STATUS"].Equals(4))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(4);
                    //}

                    //else if (dr["HOSTEL_STATUS"].Equals(0) || dr["STUD_HOSTEL_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //    ((DropDownList)e.Item.FindControl("ddlHostellerType")).SelectedValue = Convert.ToString(0);


                    //}

                    //else
                    //{
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(-1);
                    //}

                    //if (dr["LOCK"].Equals(1))
                    //    {
                    //    ((CheckBox)e.Item.FindControl("chkReport1")).Checked = true;
                    //    ((CheckBox)e.Item.FindControl("chkReport1")).Enabled = false;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport1")).Enabled = false;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller1")).Enabled = false;
                    //    }
                    //else if (dr["UNLOCK"].Equals(0))
                    //    {
                    //    ((CheckBox)e.Item.FindControl("chkReport1")).Checked = true;
                    //    ((CheckBox)e.Item.FindControl("chkReport1")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport1")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller1")).Enabled = true;
                    //    }

                    }
                else
                    {
                    ((DropDownList)e.Item.FindControl("ddlHosteller1")).Enabled = true;
                    ((DropDownList)e.Item.FindControl("ddlHosteller1")).SelectedValue = Convert.ToString(-1);
                    ((CheckBox)e.Item.FindControl("chkReport1")).Checked = false;
                    ((DropDownList)e.Item.FindControl("ddlTransport1")).Enabled = true;
                    ((DropDownList)e.Item.FindControl("ddlTransport1")).SelectedValue = Convert.ToString(-1);

                    //if (dr["STUD_TRANS_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(1);

                    //}
                    //else if (dr["STUD_TRANS_STATUS"].Equals(2))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(2);

                    //}
                    //else if (dr["STUD_TRANS_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;

                    //}
                    //else
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).Enabled = true;
                    //    ((DropDownList)e.Item.FindControl("ddlTransport")).SelectedValue = Convert.ToString(0);
                    //}

                    //if (dr["STUD_HOSTEL_STATUS"].Equals(1))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(1);
                    //}
                    //else if (dr["STUD_HOSTEL_STATUS"].Equals(2))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                    //    ((DropDownList)e.Item.FindControl("ddlHosteller")).SelectedValue = Convert.ToString(2);
                    //}
                    //else if (dr["STUD_HOSTEL_STATUS"].Equals(0))
                    //{
                    //    ((CheckBox)e.Item.FindControl("chkReport")).Checked = true;

                    //}
                    //else
                    //{

                    //}
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }
    protected void ddlHosteller1_SelectedIndexChanged(object sender, EventArgs e)
        {
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            DropDownList ddlTransport = (DropDownList)item.FindControl("ddlTransport1");
            DropDownList ddlhosteller = (DropDownList)item.FindControl("ddlHosteller1");
            DropDownList ddlHostellerType = (DropDownList)item.FindControl("ddlHostellerType1");
            if (ddlhosteller.SelectedItem.Text == "Yes")
                {
                ddlHostellerType.Enabled = true;
                ddlTransport.Enabled = true;
                ddlTransport.SelectedValue = "-1";
                ddlTransport.SelectedValue = "0";

                }
            else if (ddlhosteller.SelectedItem.Text == "No")
                {
                ddlHostellerType.Enabled = false;
                ddlHostellerType.SelectedValue = "0";
                ddlTransport.Enabled = true;
                ddlTransport.SelectedValue = "1";
                }
            }
        }
    protected void ddlTransport1_SelectedIndexChanged(object sender, EventArgs e)
        {
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            DropDownList ddlhosteller = (DropDownList)item.FindControl("ddlHosteller1");
            DropDownList ddlTransport = (DropDownList)item.FindControl("ddlTransport1");
            DropDownList ddlHostellerType = (DropDownList)item.FindControl("ddlHostellerType1");
            //if (ddlTransport.SelectedItem.Text == "No" || ddlTransport.SelectedItem.Text == "Please select")
            //{
            //    ddlhosteller.Enabled = true;
            //}
            //else
            //{
            //    ddlhosteller.Enabled = true;
            //}
            if (ddlTransport.SelectedItem.Text == "Yes")
                {
                ddlHostellerType.Enabled = false;
                ddlHostellerType.SelectedValue = "0";
                ddlhosteller.Enabled = true;
                ddlhosteller.SelectedValue = "-1";
                ddlhosteller.SelectedValue = "0";

                }
            else if (ddlTransport.SelectedItem.Text == "No")
                {
                ddlHostellerType.Enabled = true;
                ddlhosteller.Enabled = true;
                ddlhosteller.SelectedValue = "1";
                }

            }
        }
    protected void btnsubmit1_Click(object sender, EventArgs e)
        {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
            {
            //objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            //objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            //objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            //objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            //objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            //objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();

            foreach (ListViewDataItem itm in lvStudlist.Items)
                {
                CheckBox chk = itm.FindControl("chkReport1") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo1") as HiddenField;

                DropDownList dbtran = (DropDownList)itm.FindControl("ddlTransport1");
                DropDownList dbHostel = (DropDownList)itm.FindControl("ddlHosteller1");
                DropDownList ddlHostellerType = (DropDownList)itm.FindControl("ddlHostellerType1");


                //objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = Convert.ToInt32(Session["IDNO"]);
                string college = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + (Session["IDNO"]));
                objS.College_ID = Convert.ToInt32(college);
                string Admabatch = objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + (Session["IDNO"]));
                objS.AdmBatch = Convert.ToInt32(Admabatch);
                string Degree = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + (Session["IDNO"]));
                objS.DegreeNo = Convert.ToInt32(Degree);
                string BRANCH = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + (Session["IDNO"]));
                objS.BranchNo = Convert.ToInt32(BRANCH);
                string Year = objCommon.LookUp("ACD_STUDENT", "YEAR", "IDNO=" + (Session["IDNO"]));
                objS.Year = Convert.ToInt32(Year);
                string SEMESTER = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + (Session["IDNO"]));
                objS.SemesterNo = Convert.ToInt32(SEMESTER);



                if (chk.Checked.Equals(true) && chk.Enabled.Equals(true))
                    {

                    objS.IdNo = Convert.ToInt32(hdnf.Value);
                    if (Convert.ToInt32(dbHostel.SelectedValue) == -1 && Convert.ToInt32(dbtran.SelectedValue) == -1)
                        {
                        objCommon.DisplayMessage(this.Page, "Please Select Transporation Status or Hosteller Status", this.Page);
                        return;
                        }
                    if (Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue) > -1 || Convert.ToInt32((itm.FindControl("ddlHostellerType1") as DropDownList).SelectedValue) > 0 || Convert.ToInt32((itm.FindControl("ddlTransport1") as DropDownList).SelectedValue) < 1)
                        {

                        //objS.Hosteler = Convert.ToBoolean(1);
                        //objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        //flag = true;
                        objS.Hosteler = Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue);
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue);
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHostellerType1") as DropDownList).SelectedValue);
                        flag = true;
                        }

                    if (Convert.ToInt32((itm.FindControl("ddlTransport1") as DropDownList).SelectedValue) > -1)
                        {
                        //objS.Transportation = 1;
                        objS.Transportation = Convert.ToInt32((itm.FindControl("ddlTransport1") as DropDownList).SelectedValue);
                        objS.TransportSts = Convert.ToInt32((itm.FindControl("ddlTransport1") as DropDownList).SelectedValue);
                        flag = true;
                        }
                    else
                        {
                        objS.Transportation = 0;
                        objS.TransportSts = 0;
                        flag = true;
                        }



                    if (Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue) > -1)
                        {

                        //objS.Hosteler = Convert.ToBoolean(1);
                        //objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                        //flag = true;
                        objS.Hosteler = Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue);
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue);
                        flag = true;
                        }

                    else
                        {
                        objS.Hosteler = 0;
                        objS.HostelSts = 0;
                        flag = true;
                        //objS.Hosteler = Convert.ToBoolean(0);
                        //objS.HostelSts = 0;
                        //flag = true;
                        }

                    //hostelertype
                    if (Convert.ToInt32((itm.FindControl("ddlHosteller1") as DropDownList).SelectedValue) == 1)
                        {
                        if (Convert.ToInt32((itm.FindControl("ddlHostellerType1") as DropDownList).SelectedValue) < 1)
                            {
                            objCommon.DisplayMessage(this.Page, "Please Select Hosteller Type.", this.Page);
                            return;
                            }
                        }

                    if (Convert.ToInt32((itm.FindControl("ddlHostellerType1") as DropDownList).SelectedValue) > 0)
                        {
                        //objS.Transportation = 1;
                        objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHostellerType1") as DropDownList).SelectedValue);

                        flag = true;
                        ddlHostellerType.Enabled = true;

                        }

                    else
                        {
                        //objS.Transportation = 0;
                        objS.HostelSts = 0;
                        flag = true;
                        }
                    //

                    //if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) > 0)
                    // {
                    //objS.Hosteler = Convert.ToBoolean(1);
                    //if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 1)
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(1);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    //else if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 2)
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(1);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    //else if (Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue) == 3)
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(1);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    //else
                    //{
                    //    objS.Hosteler = Convert.ToBoolean(0);
                    //    objS.HostelSts = Convert.ToInt32((itm.FindControl("ddlHosteller") as DropDownList).SelectedValue);
                    //    flag = true;
                    //}
                    // }
                    // else
                    //{
                    //objS.Hosteler = Convert.ToBoolean(0);
                    // objS.HostelSts = 0;
                    //flag = true;
                    // }
                   // BindListView();
                    Bindlvstudlist();
                    // objCommon.DisplayMessage(this.updtime, "Please Select No Dues Status.", this.Page);

                    if (flag.Equals(true))
                        {
                        output = studCont.AddTransportStatusForStudent(objS);
                        }
                    }
                if (chk.Checked)
                    {
                    count++;
                    }
                }

            if (count == 0)
                {
                objCommon.DisplayMessage(Page, "Please select at least one student", this.Page);
                }
            else if (flag.Equals(true))
                {
                objCommon.DisplayMessage(Page, "Student Status Alloted Successfully", this.Page);
               // BindListView();
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void ddlhosteller_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlhosteller.SelectedValue == "1")
            {
            divhostelerType.Visible = true;
            }
        objCommon.FillDropDownList(ddlHosteltypes, "ACD_HOSTEL_TYPE", "HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO>0 ", "HOSTEL_TYPE_NO");
        }
}