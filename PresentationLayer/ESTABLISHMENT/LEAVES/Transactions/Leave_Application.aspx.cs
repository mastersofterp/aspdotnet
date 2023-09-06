//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : 
// PAGE NAME     :                                                     
// CREATION DATE : 
// CREATED BY    :                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using IITMS.UAIMS.NonAcadBusinessLogicLayer;
using BusinessLogicLayer.BusinessLogic;


public partial class ESTABLISHMENT_LEAVES_Transactions_Leave_Application : System.Web.UI.Page
{
    string date = "";
    int counter = 0;
    Common objCommon = new Common();
    static double Pending_count = 0;
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();
    
    
    string leave_code = string.Empty;
    static string leavename = string.Empty;
    static double balance_leave = 0;
    static double aditional_leave;
    static double actual_balance;
    static int rno = 0;
    static string leavename_compoff = string.Empty;

    static int letrno_static = 0, static_lno_edit = 0;
    static int month_start, month_end, month, year, count_prev_month, year_start, year_end = 0;
    DataTable dteng = new DataTable();
    DataTable dtLoad = new DataTable();

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



    protected void Page_Load(object sender, EventArgs e)
    {
        //txtFromdt.Attributes.Add("onblur", "return caldiff();");
        //txtTodt.Attributes.Add("onblur", "return caldiff();");

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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                pnlAdd.Visible = false;
                pnllist.Visible = true;
                pnlLeaveStatus.Visible = false;
                lnkRestrictedLeaves.Visible = true;
                FillEmployees();

                trhalfDay.Visible = false;
                trhalfjoinDay.Visible = false;

                ////----------------for checking 45 days CL credit-------------------------
                int useridno = Convert.ToInt32(Session["idno"]);
                Session["deptno"] = objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + useridno + " ");
                ViewState["COLLEGE_NO"] = objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + useridno + " ");
                if (useridno == 0)
                {
                    MessageBox("Sorry! User is not Authorised");
                    return;
                }
                int appointno = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAYMAS", "isnull(APPOINTNO,0) as APPOINTNO", "IDNO=" + useridno));
                //int appointno = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAYMAS", "isnull(APPOINTNO,0) as APPOINTNO", "IDNO=" + useridno));
                // DataSet dsAppoint = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "CL45_APPOINTMENT", "CL45_APPOINTMENT AS A", "" + appointno + " IN(SELECT SUBSTRING(CL45_APPOINTMENT,1,(len(CL45_APPOINTMENT)-1)) FROM PAYROLL_LEAVE_REF)", "");
                //SELECT * FROM PAYROLL_LEAVE_CL_APPOINTMENT WHERE COLLEGE_NO=1 AND APPOINTNO=2
                DataSet dsAppoint = objCommon.FillDropDown("PAYROLL_LEAVE_CL_APPOINTMENT", "APPOINTNO", "COLLEGE_NO", "COLLEGE_NO=" + Convert.ToInt32(ViewState["COLLEGE_NO"]) + " AND APPOINTNO=" + appointno + " ", "");

                if (dsAppoint.Tables[0].Rows.Count > 0)
                {
                    DataSet dsEno = objCommon.FillDropDown("PAYROLL_LEAVETRAN", "ENO", "IDNO", "IDNO=" + useridno + " AND LEAVENO=1 AND ST=2", "");
                    if (dsEno.Tables[0].Rows.Count > 0)
                    {
                        int eno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVETRAN", "ENO", "IDNO=" + useridno + " AND LEAVENO=1 AND ST=2 "));

                        DataSet dsDate = objCommon.FillDropDown("PAYROLL_LEAVETRAN", "LAST_ALLOTED_DATE", "IDNO", "IDNO=" + useridno + " AND ENO=" + eno + " AND LAST_ALLOTED_DATE IS NOT NULL", "");
                        if (dsDate.Tables[0].Rows.Count > 0)
                        {
                            pnllist.Visible = true;
                            lnkbut.Visible = true;
                            DateTime chklastaltdt = Convert.ToDateTime(objCommon.LookUp("PAYROLL_LEAVETRAN", "LAST_ALLOTED_DATE", "IDNO=" + useridno + " AND ENO=" + eno));//swati


                            DateTime currentdate = DateTime.Now;
                            //================================swati
                            //Code to get slots of 45
                            TimeSpan daysCL = currentdate - chklastaltdt;
                            int diffdays = daysCL.Days;
                            int increment_days = 0;
                            int count = 0;
                            if (diffdays > 45)
                            {
                                int slot = Convert.ToInt32(diffdays / 45);
                                for (int i = 1; i <= slot; i++)
                                {
                                    increment_days = increment_days + 45;//used to add this in DateAdd() function
                                    count++;//used to add this in Leave variable(to increment the leave)
                                }
                                Leaves objlv = new Leaves();
                                CustomStatus cs = new CustomStatus();
                                objlv.ENO = eno;
                                objlv.EMPNO = useridno;
                                objlv.NO_DAYS = increment_days;
                                objlv.LEAVE = count.ToString();
                                cs = (CustomStatus)objApp.UPDATE_CL45DAYS_LEAVES_ONLOAD(objlv);

                            }
                            //==================end
                            //if (diffdays > 45)
                            //{
                            //    Leaves objlv = new Leaves();
                            //    CustomStatus cs = new CustomStatus();
                            //    objlv.ENO = eno;
                            //    cs = (CustomStatus)objApp.UPDATE_CL45DAYS_LEAVES_ONLOAD(objlv);
                            //}
                        }
                        else
                        {
                            pnllist.Visible = false;
                            lnkbut.Visible = false;
                            MessageBox("Last Modified Date Not Updated!");
                            return;
                        }
                    }
                    else
                    {
                        //display message
                        // btnSave.Enabled = false;
                        MessageBox("Sorry! Record Not Exists");
                        pnllist.Visible = false;
                        lnkbut.Visible = false;
                        return;
                    }
                }
                //objCommon.FillDropDownList(ddlEmp, "payroll_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') as ENAME", "STATUSNO=4", "ENAME");


                ////--------------- for checking 45 days CL credit end----------------------

                //pnlAdd.Visible = false;
                //pnllist.Visible = true;
                //pnlLeaveStatus.Visible = false;

                BindListViewLeaveapplStatus();
                BindListViewLeaveStatus();
                //txtValid.Visible = false;
                //tdValid.Visible = false;
                //code to delete from temp table

                objLM.LNO = 0;
                objLM.TRANNO = 0;
                objLM.EMPNO = 0;
                objLM.LETRNO = 0;
                //CustomStatus csdelete = (CustomStatus)objApp.DeleteEngagedLecture(objLM);
                //======================
                if (lvLeaveinfo != null)
                {
                    foreach (ListViewDataItem dti in lvLeaveinfo.Items)
                    {
                        HiddenField hfbal = dti.FindControl("hfbal") as HiddenField;
                        Button btn = dti.FindControl("btnApply") as Button;
                        if (hfbal != null)
                        {
                            if ((Convert.ToDouble(hfbal.Value)) > 0)
                            {
                                btn.Enabled = true;
                            }
                            else
                            {
                                btn.Enabled = true;
                            }
                        }
                    }
                }

                dteng = setGridViewDataset(dteng, "dteng").Clone();
                ViewState["dteng"] = dteng;
                dtLoad = setGridDatatSetLoad(dtLoad, "dtLoad").Clone();
                ViewState["dtLoad"] = dtLoad;
                ViewState["SRNO"] = null;
            }


        }
        divMsg.InnerHtml = string.Empty;
        lnkRestrictedLeaves.Visible = true;


    }

    protected DataTable setGridViewDataset(DataTable dt, string tabName)
    {
        dt.TableName.Equals(tabName);
        dteng.Columns.Add("DATE");
        dteng.Columns.Add("TIME");
        dteng.Columns.Add("YEAR_SEM");
        dteng.Columns.Add("SUBJECT");
        // dteng.Columns.Add("THEORY_PRACTICAL");       
        dteng.Columns.Add("IDNO");
        dteng.Columns.Add("LNO");
        dteng.Columns.Add("FACULTY");
        dteng.Columns.Add("FACULTY_NAME");  // ENGDATA 
        dteng.Columns.Add("SEQNO");
        //dteng.Columns.Add("DESIGNATION");
        dteng.Columns["SEQNO"].AutoIncrement = true; dteng.Columns["SEQNO"].AutoIncrementSeed = 1; dteng.Columns["SEQNO"].AutoIncrementStep = 1;
        // dteng.Columns.Add("SEQNO").AutoIncrement = true; dteng.Columns.Add("SEQNO").AutoIncrementSeed = 1; dt.Columns["Id"].AutoIncrementStep = 1;
        return dt;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void BindListViewLeaveapplStatus()
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno));

            DataSet ds = objApp.GetLeaveApplStatus(Convert.ToInt32(Session["idno"]), stno);//Session["idno"]
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;

            }
            else
            {
                string status = ds.Tables[0].Rows[0]["Status"].ToString();
                //dpPager.Visible = true;

            }
            lvStatus.DataSource = ds.Tables[0];
            lvStatus.DataBind();
            lvStatus.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.BindListViewLeaveapplStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void BindListViewRestrictedHolidaysLeaveapplStatus()
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            // int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno));

            DataSet ds = objApp.GetLeaveApplStatusForRestrictedholidays(Convert.ToInt32(Session["idno"]));//Session["idno"]
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;

            }
            else
            {
                lvRestrictHolidays.DataSource = ds.Tables[0];
                lvRestrictHolidays.DataBind();
                lvRestrictHolidays.Visible = true;
                // string status = ds.Tables[0].Rows[0]["Status"].ToString();
                // dpPager.Visible = true;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.BindListViewLeaveapplStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void BindListViewLeaveStatus()
    {
        try
        {
            if (Convert.ToInt32(Session["idno"].ToString()) != 0)
            {
                int YR = DateTime.Now.Year;
                //int YR = 2011;
                //int MON = DateTime.Now.Month;
                DataSet ds = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), YR, 0);//Session["idno"]

                lvLeaveinfo.DataSource = ds.Tables[0];
                lvLeaveinfo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.BindListViewLeaveStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Int32 LETRNO = int.Parse(btnEdit.CommandArgument);
            Int32 lno = int.Parse(btnEdit.ToolTip);
            ViewState["letrno"] = LETRNO;
            letrno_static = LETRNO;
            ViewState["LNO"] = lno;
            Session["lno"] = lno;
            chklvapply.Visible = false;
            DataSet ds = new DataSet();
            string status;
            ds = objCommon.FillDropDown("payroll_LEAVE_APP_PASS_ENTRY", "*", "", "LETRNO=" + LETRNO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "A")
                {
                    btnSave.Enabled = false;
                    MessageBox("Leave is Already Approved by authority, You can not modify");
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            ds = objCommon.FillDropDown("payroll_LEAVE_APP_ENTRY", "*", "", "LETRNO=" + LETRNO, "");
            string statusnew = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (statusnew == "A" || statusnew == "T")
            {
                btnSave.Enabled = false;
                MessageBox("Leave is Already Approved by authority, You can not modify");
                return;
            }
            else if (statusnew == "R")
            {
                btnSave.Enabled = false;
                MessageBox("Leave is Already Rejected by authority, You can not modify");
                return;

            }
            else
            {
                btnSave.Enabled = true;
            }
            ds = objCommon.FillDropDown("PAYROLL_LEAVE_APP_PASS_ENTRY", "STATUS", "PANO", "LETRNO=" + LETRNO + " ", "PANO");
            int total = ds.Tables[0].Rows.Count;
            for (int i = 0; i < total; i++)
            {
                //Code to avoid modification of record if 1st authority has approved leave (in case of more than 1 authority)
                status = ds.Tables[0].Rows[i]["STATUS"].ToString();
                if (status == "F")
                {
                    MessageBox("Approval In Progress, Not Allow To Modify");
                    return;
                }
            }

            ShowDetailsFromAppl(Convert.ToInt32(Session["idno"].ToString()), LETRNO);
            //  static_lno_edit = Convert.ToInt32(ViewState["LEAVENO"]);
            //GetPAPath(Convert.ToInt32(Session["userno"]), lno);
            Boolean IsLeaveWisePassingPath = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0)as IsLeaveWisePassingPath", ""));

            ViewState["IsLeaveWisePassingPath"] = IsLeaveWisePassingPath;
            GetPAPath1();
            //------------------------lecture engaged info-------------

            objLM.TRANNO = 1;
            objLM.EMPNO = Convert.ToInt32(Session["idno"].ToString());
            objLM.LNO = lno;
            //-------------------------
            objLM.TRANNO = 0;

            objLM.EMPNO = objLM.EMPNO;
            objLM.LETRNO = LETRNO;

            //==============================
            objLM.TRANNO = 1;

            objLM.TRANNO = 0;
            //------------------------lecture engaged info-----end--------




            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnllist.Visible = false;
            pnlLeaveStatus.Visible = false;
            pnlLeaveRestrictHolidays.Visible = false;
            lnkRestrictedLeaves.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void ShowDetailsFromAppl(int IDNO, int LETRNo)
    {
        DataSet ds = null;
        try
        {


            ds = objApp.GetLeaveAppEntryByNOForEdit(IDNO, LETRNo);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["FileName"] = ds.Tables[0].Rows[0]["FileName"].ToString();
                ViewState["LNO"] = ds.Tables[0].Rows[0]["LNO"].ToString();
                txtLeavename.Text = ds.Tables[0].Rows[0]["LEAVENAME"].ToString();
                txtFromdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                txtTodt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                txtNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                ddlNoon.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["FNAN"]);
                txtReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                txtcharge.Text = ds.Tables[0].Rows[0]["CHARGE_HANDED"].ToString();
                txtLeaveType.Text = ds.Tables[0].Rows[0]["LType"].ToString();
                ViewState["SESSION_SRNO"] = ds.Tables[0].Rows[0]["SESSION_SRNO"].ToString();
                ViewState["SESSION_SERVICE_SRNO"] = ds.Tables[0].Rows[0]["SESSION_SERVICE_SRNO"].ToString();
                ViewState["ISCERTIFICATE"] = ds.Tables[0].Rows[0]["IsCertificate"].ToString();
                ViewState["LEAVE"] = ds.Tables[0].Rows[0]["Leave_ShortName"].ToString();

                ViewState["LEAVENO"] = ds.Tables[0].Rows[0]["LEAVENO"].ToString();
                //ALLOWED_DAYS,MAX_DAYS_TO_APPLY,MIN_DAYS_TO_APPLY
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsWithCertificate"].ToString()) == true)
                {
                    rblIsCertificate.SelectedValue = "0";
                    divIsCertificate.Visible = true;
                    CertificateUpload.Visible = true;
                    PanelCertificate.Visible = true;

                    //trCer.Visible = true;
                    //trml.Visible = true;
                    trotherlvs.Visible = true;
                }
                else
                {
                    //rblIsCertificate.SelectedValue = "1";
                    rblIsCertificate.SelectedValue = "0";
                    divIsCertificate.Visible = false;
                    CertificateUpload.Visible = false;
                    PanelCertificate.Visible = false;
                    //trCer.Visible = false;
                    trml.Visible = false;
                    trotherlvs.Visible = true;
                }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["FIT"]) == true)
                    ChkFit.Checked = true;
                else
                    ChkFit.Checked = false;

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["UNFIT"]) == true)
                    chkUFit.Checked = true;
                else
                    chkUFit.Checked = false;
                txtadd.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtLeavebal.Text = ds.Tables[0].Rows[0]["bal"].ToString();
                //ISFULLDAYLEAVE,FNAN
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["ISFULLDAYLEAVE"]) == true)//full day leave
                {
                    rblleavetype.SelectedValue = "0";
                    // ddlLeaveFNAN.Visible = false;
                    ddlLeaveFNAN.Enabled = false;
                }
                else
                {
                    //ddlLeaveFNAN.Visible = true;
                    ddlLeaveFNAN.Enabled = true;
                    rblleavetype.SelectedValue = "1";
                    trhalfDay.Visible = true;
                    trhalfjoinDay.Visible = true;
                }

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["FNAN"]) == true)
                    ddlLeaveFNAN.SelectedValue = "1";//forenoon
                else
                    ddlLeaveFNAN.SelectedValue = "0";//afternoon

                ViewState["ALLOWED_DAYS"] = ds.Tables[1].Rows[0]["ALLOWED_DAYS"].ToString();
                ViewState["MAX_DAYS_TO_APPLY"] = ds.Tables[1].Rows[0]["MAX_DAYS_TO_APPLY"].ToString();
                ViewState["MIN_DAYS_TO_APPLY"] = ds.Tables[1].Rows[0]["MIN_DAYS_TO_APPLY"].ToString();
                ViewState["MAX_DAYS_TO_PDL"] = ds.Tables[1].Rows[0]["MAX_DAYS_TO_PDL"].ToString();
                ViewState["MAX_DAYS_ALLOWED_TOPDL"] = ds.Tables[1].Rows[0]["MAX_DAYS_ALLOWED_TOPDL"].ToString();
                ViewState["MAX_DAYS_TO_PD"] = ds.Tables[1].Rows[0]["MAX_DAYS_TO_PD"].ToString();
                ViewState["MAX_DAYS_ALLOWED_TOPD"] = ds.Tables[1].Rows[0]["MAX_DAYS_ALLOWED_TOPD"].ToString();

                ViewState["IsClassArrangeRequired"] = ds.Tables[0].Rows[0]["IsClassArrangeRequired"].ToString();
                ViewState["isValidatedays"] = ds.Tables[0].Rows[0]["isValidatedays"].ToString();
                ViewState["IsCompOff"] = ds.Tables[0].Rows[0]["ISCOMPOFF"].ToString();
                ViewState["DaysforCertificate"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DaysforCertificate"]).ToString();
                ViewState["IsRequiredLoad"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsRequiredLoad"].ToString());
                ViewState["IsLeaveValid"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLeaveValid"].ToString());

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsClassArrangeRequired"].ToString().ToString()) == true)
                {
                    pnlEngaged.Visible = true;
                    PnlAddEngaged.Visible = true;
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lvEngagedInfo.DataSource = ds.Tables[2];
                        ViewState["dteng"] = ds.Tables[2];
                        lvEngagedInfo.DataBind();
                    }
                }
                else
                {
                    pnlEngaged.Visible = false;
                    PnlAddEngaged.Visible = false;
                }

                ViewState["STNO"] = objCommon.LookUp("Payroll_empmas", "stno", "IDNO=" + IDNO);
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsRequiredLoad"].ToString()) == true)
                {
                    pnlFacLoad.Visible = true;
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lvFacLoad.DataSource = ds.Tables[2];
                        ViewState["dtLoad"] = ds.Tables[2];
                        lvFacLoad.DataBind();

                        foreach (ListViewDataItem lvitem in lvFacLoad.Items)
                        {

                            DropDownList ddlFac = lvitem.FindControl("ddlFac") as DropDownList;
                            HiddenField hdnfacno = lvitem.FindControl("hdnidno") as HiddenField;
                            //objCommon.FillDropDownList(ddlFac, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "ISNULL(E.TITLE,'' ) + ' ' + isnull(E.FNAME,'') + ' ' + isnull(E.MNAME,'') + ' ' + isnull(E.LNAME,'') AS EMPNAME", "E.STNO=" + Convert.ToInt32(ViewState["STNO"]) + " AND E.IDNO!=" + Convert.ToInt32(Session["idno"]) + " AND P.PSTATUS='Y' ", "E.IDNO");

                            ddlFac.SelectedValue = hdnfacno.Value.ToString();
                        }
                    }
                }
                else
                {
                    pnlFacLoad.Visible = false;
                }

            }
            ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "COLLEGE_NO,SUBDEPTNO,STNO,COLLEGE_NO,STNO,STAFFNO,PHONENO,ISNULL(ALTERNATE_EMAILID,'') AS ALTERNATE_EMAILID", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SUBDEPTNO"] = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                ViewState["STNO"] = ds.Tables[0].Rows[0]["STNO"].ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ViewState["STAFFNO"] = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                lblmobile.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                lblemail.Text = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();
            }


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 LETRNO = int.Parse(btnDelete.CommandArgument);
            DataSet ds = new DataSet();
            //ds = objCommon.FillDropDown("payroll_LEAVE_APP_PASS_ENTRY", "*", "", "LETRNO=" + LETRNO, "");

            //string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            //if (status == "A")
            //{
            //    btnSave.Enabled = false;
            //    MessageBox("Leave is Already Approved by authority, You can not delete");
            //    return;
            //}
            //else
            //{
            //    btnSave.Enabled = true;
            //}
            ds = objCommon.FillDropDown("payroll_LEAVE_APP_ENTRY", "*", "", "LETRNO=" + LETRNO, "");
            string statusnew = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (statusnew == "A" || statusnew == "T")
            {
                btnSave.Enabled = false;
                MessageBox("Leave is Already Approved by authority, You can not delete");
                return;
            }
            else if (statusnew == "R")
            {
                btnSave.Enabled = false;
                MessageBox("Leave is Already Rejected by authority, You can not delete");
                return;

            }
            else
            {
                btnSave.Enabled = true;
            }

            //ds = objCommon.FillDropDown("PAYROLL_LEAVE_APP_PASS_ENTRY", "STATUS", "PANO", "LETRNO=" + LETRNO + " ", "PANO");
            //int total = ds.Tables[0].Rows.Count;
            //for (int i = 0; i < total; i++)
            //{
            //    //Code to avoid modification of record if 1st authority has approved leave (in case of more than 1 authority)
            //    status = ds.Tables[0].Rows[i]["STATUS"].ToString();
            //    if (status == "F")
            //    {
            //        MessageBox("Approval In Progress, Not Allow To Modify");
            //        return;
            //    }
            //}

            CustomStatus cs = (CustomStatus)objApp.DeleteLeaveAppEntry(LETRNO, Convert.ToInt32(Session["idno"]));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = "add";
                MessageBox("Record Deleted Successfully!");
                BindListViewLeaveapplStatus();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{

    //    BindListViewLeaveapplStatus();
    //}
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            //  letrno_static=letrno_static;
            letrno_static = 0;
            Button btnApply = sender as Button;
            //balance_leave = Convert.ToDouble(btnApply.ToolTip);
            int year = Convert.ToInt32(btnApply.ToolTip);
            lnkRestrictedLeaves.Visible = false;
            lnkbut.Visible = false;

            string Working_date = btnApply.CommandName;
            //Modifed By: Swati Ghate
            //Code: to bind rno with Commandname "DATE" for comp-off leave
            int start = Working_date.IndexOf('_');
            if (start > 0)
            {
                //Code: to check only for comp-off leave because for other leave no date exists so return start=-1
                start = start + 1;
                string rno = Working_date.Substring(start);
                ViewState["RNO"] = rno;
                start = start - 1;
                Working_date = Working_date.Substring(0, start);
                //select 'COMPENSATORY LEAVE ('+ CONVERT(varchar(10),WORKING_DATE,103) +')' from ESTB_COMP_OFF_REQUEST where RNO=55
                leavename_compoff = objCommon.LookUp("ESTB_COMP_OFF_REQUEST", " CONVERT(varchar(10), WORKING_DATE, 103) ", "RNO=" + Convert.ToInt32(rno) + "");
                leavename_compoff = "COMPENSATORY LEAVE(" + leavename_compoff + ")";
            }
            Session["Working_Date"] = Working_date;
            int LNO = int.Parse(btnApply.CommandArgument);
            int idno = Convert.ToInt32(Session["idno"]);
            Session["lno"] = LNO.ToString();
            string bal = btnApply.ToolTip.ToString();
            double balance = Convert.ToDouble(bal);
            chklvapply.Visible = false;


            int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + LNO + " "));

            ViewState["LEAVENO"] = leaveno.ToString();


            // DataSet ds_Session = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), DateTime.Now.Year, leaveno);//Session["idno"]
            //DataSet ds_Session = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), DateTime.Now.Year, LNO);//Session["idno"]  // Commeneted By Shrikant B on 04/01/2022
            DataSet ds_Session = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), year, LNO);//Session["idno"]
            if (ds_Session.Tables[0].Rows.Count > 0)
            {
                //SELECT FDT,TDT,* FROM PAYROLL_LEAVE_SESSION
                // ViewState["LEAVENO"] = ds_Session.Tables[0].Rows[0]["LNO"].ToString();
                // ViewState["LNO"] = ds_Session.Tables[0].Rows[0]["LNO_NEW"].ToString();
                ViewState["SESSION_SRNO"] = ds_Session.Tables[0].Rows[0]["SESSION_SRNO"].ToString();
                ViewState["ISCERTIFICATE"] = ds_Session.Tables[0].Rows[0]["ISCERTIFICATE"].ToString();
                if (ViewState["SESSION_SRNO"].ToString().Trim() == "0".ToString().Trim())
                {
                    //SERVICE BASE LEAVE
                    ViewState["SESSION_SERVICE_SRNO"] = ds_Session.Tables[0].Rows[0]["SESSION_SERVICE_SRNO"].ToString();


                    //--------------------------------------------//

                    month_start = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION_SERVICE", "MONTH(FDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SERVICE_SRNO"].ToString()) + ""));
                    month_end = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION_SERVICE", "MONTH(TDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SERVICE_SRNO"].ToString()) + ""));

                    year_start = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION_SERVICE", "YEAR(FDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SERVICE_SRNO"].ToString()) + ""));
                    year_end = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION_SERVICE", "YEAR(TDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SERVICE_SRNO"].ToString()) + ""));


                    //SERVICE_LIMIT ,MAX_SERVICE_COMPLETE
                    ViewState["SERVICE_LIMIT"] = ds_Session.Tables[0].Rows[0]["SERVICE_LIMIT"].ToString();
                    ViewState["MAX_SERVICE_COMPLETE"] = ds_Session.Tables[0].Rows[0]["MAX_SERVICE_COMPLETE"].ToString();
                    //===============
                    DateTime oldDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_EMPMAS", "DOJ", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));//09-12-2014

                    DateTime newDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                    // Difference in days, hours, and minutes.
                    TimeSpan ts = newDate - oldDate;
                    // Difference in days.
                    int differenceInDays = ts.Days;
                    int years = (int)(ts.Days / 365.25);
                    int months = ts.Days / 31;
                    if (years < Convert.ToInt32(Convert.ToInt32(ViewState["MAX_SERVICE_COMPLETE"].ToString())))
                    {
                        MessageBox("Sorry! Not Allow to Apply. Service should be complete by atleast " + Convert.ToInt32(ViewState["MAX_SERVICE_COMPLETE"].ToString()) + " Years");
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                    int ret = CheckLeaveLimit();   //To check service leave limit
                    if (ret == 1)
                    {
                        return;
                    }
                    //===================

                }
                else
                {

                    ViewState["SESSION_SERVICE_SRNO"] = "0";
                    month_start = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION", "MONTH(FDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SRNO"].ToString()) + ""));
                    month_end = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION", "MONTH(TDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SRNO"].ToString()) + ""));

                    year_start = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION", "YEAR(FDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SRNO"].ToString()) + ""));
                    year_end = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SESSION", "YEAR(TDT)", "SESSION_SRNO=" + Convert.ToInt32(ds_Session.Tables[0].Rows[0]["SESSION_SRNO"].ToString()) + ""));


                    // New Code Added By Shrikant B on 28/12/2021 //

                    rblleavetype.Items.Clear();
                    rblleavetype.Items.Insert(0, new ListItem("Full Day", "0"));
                    rblleavetype.Items.Insert(1, new ListItem("Half Day", "1"));
                    rblleavetype.Items[0].Selected = true;

                    double MinDay = Convert.ToDouble(ds_Session.Tables[0].Rows[0]["MIN_DAYS_TO_APPLY"].ToString());

                    if (MinDay >= 1)
                    {
                        rblleavetype.Items.RemoveAt(1);
                        rblleavetype.Items[0].Selected = true;
                    }
                    else
                    {
                        rblleavetype.Items.Clear();
                        rblleavetype.Items.Insert(0, new ListItem("Full Day", "0"));
                        rblleavetype.Items.Insert(1, new ListItem("Half Day", "1"));
                        rblleavetype.Items[0].Selected = true;
                    }
                    //Added for Service Complete condition for normal leave also leave is not servuce based
                    int MAX_SERVICE_COMPLETE = Convert.ToInt32(ds_Session.Tables[0].Rows[0]["MAX_SERVICE_COMPLETE"].ToString());

                    if (MAX_SERVICE_COMPLETE != 0)
                    {
                        DateTime oldDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_EMPMAS", "DOJ", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));//09-12-2014

                        DateTime newDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                        // Difference in days, hours, and minutes.
                        TimeSpan ts = newDate - oldDate;
                        // Difference in days.
                        int differenceInDays = ts.Days;
                        int years = (int)(ts.Days / 365.25);
                        int months = ts.Days / 31;
                        //if (years < Convert.ToInt32(Convert.ToInt32(ViewState["MAX_SERVICE_COMPLETE"].ToString())))
                        if (years < MAX_SERVICE_COMPLETE)
                        {
                            MessageBox("Sorry! Not Allow to Apply. Service should be complete by atleast " + MAX_SERVICE_COMPLETE + " Years");
                            btnSave.Enabled = false;
                            return;
                        }
                    }
                    //===end

                }
            }
            DataSet dsLeave = objCommon.FillDropDown("PAYROLL_LEAVE L INNER JOIN PAYROLL_LEAVE_NAME LV ON(LV.LVNO=L.LEAVENO)", "L.CAL,L.PERIOD,L.LEAVENO,L.LEAVE,isnull(L.ALLOWED_DAYS,0)as ALLOWED_DAYS,L.CAL_HOLIDAYS,isnull(LV.isValidatedays,0) as isValidatedays,isnull(LV.DaysforCertificate,0)as DaysforCertificate", "L.LEAVENO", "L.LNO=" + LNO + "", "");
            if (dsLeave.Tables[0].Rows.Count > 0)
            {
                leave_code = (dsLeave.Tables[0].Rows[0]["LEAVE"]).ToString();
                ViewState["LEAVE_CODE"] = leave_code;
                // leavename = (dsLeave.Tables[0].Rows[0]["LEAVENAME"]).ToString();
                ViewState["PERIOD"] = dsLeave.Tables[0].Rows[0]["PERIOD"].ToString();
                ViewState["LEAVENO"] = dsLeave.Tables[0].Rows[0]["LEAVENO"].ToString();
                ViewState["LEAVE"] = dsLeave.Tables[0].Rows[0]["LEAVE"].ToString();
                ViewState["LEAVENO"] = dsLeave.Tables[0].Rows[0]["LEAVENO"].ToString();
                ViewState["ALLOWED_DAYS"] = dsLeave.Tables[0].Rows[0]["ALLOWED_DAYS"].ToString();
                ViewState["CAL_HOLIDAYS"] = dsLeave.Tables[0].Rows[0]["CAL_HOLIDAYS"].ToString();
                ViewState["isValidatedays"] = Convert.ToBoolean(dsLeave.Tables[0].Rows[0]["isValidatedays"]).ToString();
                ViewState["DaysforCertificate"] = Convert.ToInt32(dsLeave.Tables[0].Rows[0]["DaysforCertificate"]).ToString();

                //trCommuted.Visible = true;

            }

            if (Convert.ToInt32(ViewState["RNO"]) > 0)
            {
                int retComp = CheckExistComoff();
                if (retComp == 1)
                {

                    return;
                }
            }

            Boolean IsLeaveWisePassingPath = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0)as IsLeaveWisePassingPath", ""));

            ViewState["IsLeaveWisePassingPath"] = IsLeaveWisePassingPath;

            // Session["LEAVENO"] = objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + Convert.ToInt32(Session["lno"]) + "");

            // Added BY Shrikant Bharne New code to LWP Apply Restrication Crescent//

            DataSet DsLWPLeave = objCommon.FillDropDown("Payroll_leave_ref", "isnull(LWP_NO,0) as LWP_NO", "isnull(ISLWPNOTALLOW,0) as ISLWPNOTALLOW, isnull(Leavevalue,'') as Leavevalue", "", "");
            if (DsLWPLeave.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToBoolean(DsLWPLeave.Tables[0].Rows[0]["ISLWPNOTALLOW"]) && Convert.ToInt32(DsLWPLeave.Tables[0].Rows[0]["LWP_NO"]) == Convert.ToInt32(ViewState["LEAVENO"]))
                {
                    int retLwp = CheckLWPApply(year);
                    if (retLwp == 1)
                    {
                        return;
                    }

                }
            }


            //-------------------------------------------------------------------//



            ShowDetails(LNO, year);

            pnlAdd.Visible = true;
            pnllist.Visible = false;
            ViewState["action"] = "add";
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnApply_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    public int CheckLeaveLimit()
    {

        int ret = 0;
        int empid = Convert.ToInt32(Session["idno"]);
        //int maternity_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "MATER_NO", "ISNULL(MATER_NO,0)>=0"));
        //int paternity_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "PATER_NO", "ISNULL(PATER_NO,0)>=0"));
        int leaveno = Convert.ToInt32(ViewState["LEAVENO"]);
        //MAX_SERVICE_COMPLETE
        DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "LEAVENO", "EMPNO", "EMPNO=" + empid + " AND STATUS NOT IN('R','D') AND LEAVENO=" + leaveno + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
            //SERVICE_LIMIT ,MAX_SERVICE_COMPLETE
            if (count >= Convert.ToInt32(ViewState["SERVICE_LIMIT"]))
            {
                MessageBox("Sorry! Not allow to apply. Limit is closed");
                ret = 1;
            }
        }
        //if (maternity_no == leaveno)
        //{
        //    DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "LEAVENO", "EMPNO", "EMPNO=" + empid + " AND STATUS NOT IN('R','D') AND LEAVENO=" + maternity_no + "", "");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
        //        //SERVICE_LIMIT ,MAX_SERVICE_COMPLETE
        //        if (count >= Convert.ToInt32(ViewState["SERVICE_LIMIT"]))
        //        {
        //            MessageBox("Sorry! Not allow to apply. Limit is closed");
        //            ret = 1;
        //        }
        //    }
        //}
        //else if (paternity_no == leaveno)
        //{
        //    DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "LEAVENO", "EMPNO", "EMPNO=" + empid + " AND STATUS NOT IN('R','D') AND LEAVENO=" + paternity_no + "", "");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
        //        //if (count >= 2)
        //        if (count >= Convert.ToInt32(ViewState["SERVICE_LIMIT"]))
        //        {
        //            MessageBox("Sorry! Not allow to apply. Limit is closed");
        //            ret = 1;
        //        }
        //    }
        //}
        return ret;
    }
    private void ShowDetails(Int32 LNO, int year)
    {
        DataSet ds1 = null;
        double balance = 0;

        bool IsCompOff = false; bool IsPayLeave = false;
        int YR = year;// DateTime.Now.Year;
        try
        {
            int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + LNO + " "));

            ViewState["LEAVENO"] = leaveno.ToString();

            DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO,STNO,COLLEGE_NO,STNO,STAFFNO,PHONENO,isnull(ALTERNATE_EMAILID,'') as ALTERNATE_EMAILID", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SUBDEPTNO"] = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                ViewState["STNO"] = ds.Tables[0].Rows[0]["STNO"].ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ViewState["STAFFNO"] = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();
                ViewState["PHONENO"] = ds.Tables[0].Rows[0]["PHONENO"].ToString();
            }

            lblmobile.Text = Convert.ToString(ViewState["PHONENO"]);
            lblemail.Text = Convert.ToString(ViewState["EMAILID"]);


            if (ViewState["PHONENO"].ToString() != string.Empty)
            {
                lblmobile.Text = Convert.ToString(ViewState["PHONENO"]);
            }
            else
            {
                lblmobile.Text = string.Empty;
            }
            if (ViewState["EMAILID"].ToString() != string.Empty)
            {
                lblemail.Text = Convert.ToString(ViewState["EMAILID"]);
            }
            else
            {
                lblemail.Text = string.Empty;
            }
            //select CAL,PERIOD,LEAVENO,LEAVE,isnull(ALLOWED_DAYS,0),CAL_HOLIDAYS,* from PAYROLL_LEAVE
            DataSet dsLeave = objCommon.FillDropDown("PAYROLL_LEAVE", "CAL,PERIOD,LEAVENO,LEAVE,isnull(ALLOWED_DAYS,0)as ALLOWED_DAYS,CAL_HOLIDAYS", "LEAVENO ,isnull(IsClassArrangeRequired,0)as IsClassArrangeRequired ,isnull(IsClassArrangeAcceptanceRequired,0) as IsClassArrangeAcceptanceRequired, ISNULL(IsRequiredLoad,0) AS IsRequiredLoad , ISNULL(IsLeaveValid,0) AS IsLeaveValid , ISNULL(LEAVEVALIDMONTH,0) AS LEAVEVALIDMONTH", "LNO=" + LNO + "", "");
            if (dsLeave.Tables[0].Rows.Count > 0)
            {
                ViewState["CAL"] = dsLeave.Tables[0].Rows[0]["CAL"].ToString();
                ViewState["PERIOD"] = dsLeave.Tables[0].Rows[0]["PERIOD"].ToString();
                ViewState["LEAVENO"] = dsLeave.Tables[0].Rows[0]["LEAVENO"].ToString();
                ViewState["LEAVE"] = dsLeave.Tables[0].Rows[0]["LEAVE"].ToString();
                ViewState["ALLOWED_DAYS"] = dsLeave.Tables[0].Rows[0]["ALLOWED_DAYS"].ToString();
                ViewState["CAL_HOLIDAYS"] = dsLeave.Tables[0].Rows[0]["CAL_HOLIDAYS"].ToString();
                ViewState["LEAVENO"] = dsLeave.Tables[0].Rows[0]["LEAVENO"].ToString();

                ViewState["IsClassArrangeRequired"] = dsLeave.Tables[0].Rows[0]["IsClassArrangeRequired"].ToString();
                ViewState["IsClassArrangeAcceptanceRequired"] = dsLeave.Tables[0].Rows[0]["IsClassArrangeAcceptanceRequired"].ToString();
                ViewState["IsRequiredLoad"] = dsLeave.Tables[0].Rows[0]["IsRequiredLoad"].ToString();
                //  ViewState["TimeforLeave"] = dsLeave.Tables[0].Rows[0]["TimeforLeave"].ToString();

                // Added  by Shrikant 
                //  DateTime checktime = DateTime.Parse(dsLeave.Tables[0].Rows[0]["TimeforLeave"].ToString());
                //  DateTime timeconfig = DateTime.Parse(checktime.ToString("HH:mm tt"));
                ////  DateTime checktime = DateTime.Parse ViewState["TimeforLeave"]

                //  DateTime timecheck = Convert.ToDateTime(DateTime.Now.ToString());
                //  DateTime timecheckconfig = DateTime.Parse(timecheck.ToString("HH:mm tt"));

                //  if (timecheckconfig > timeconfig)
                //  {
                //      MessageBox("you can not apply");
                //      return;
                //  }
                ViewState["IsLeaveValid"] = dsLeave.Tables[0].Rows[0]["IsLeaveValid"].ToString();

            }

            //  DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString());
            // CurrentDate.TimeOfDay.ToString("HH:mm tt");
            // CurrentDate.TimeOfDay.ToString();
            //  string time = DateTime.Now.ToString("HH:mm tt");

            //if (!(DateTime.Now.ToString("HH:mm tt") ==  ViewState["TimeforLeave"].ToString()))
            //{
            //    MessageBox("Not Allow to Apply Leave Time is more than ");
            //}          

            Boolean certificate = Convert.ToBoolean(objCommon.LookUp("PayROLL_Leave_Name", "isnull(IsCertificate,0)", "LVNO=" + leaveno));
            if (certificate == true)
            {
                CertificateUpload.Visible = true;
                divIsCertificate.Visible = true;
                PanelCertificate.Visible = true;
                if (rblIsCertificate.SelectedValue == "0")
                {
                    divCer.Visible = true;
                }
                else
                {
                    divCer.Visible = false;
                }
            }
            else
            {
                divIsCertificate.Visible = false;
                divCer.Visible = false;
                CertificateUpload.Visible = false;
                PanelCertificate.Visible = false;
            }

            Boolean IsClassArrangeRequired = Convert.ToBoolean(ViewState["IsClassArrangeRequired"]);
            Boolean IsClassArrangeAcceptanceRequired = Convert.ToBoolean(ViewState["IsClassArrangeAcceptanceRequired"]);
            //if (IsClassArrangeRequired == true || IsClassArrangeAcceptanceRequired == true)
            if (IsClassArrangeRequired == true )
            {
                pnlEngaged.Visible = true;
                //PnlAddEngaged.Visible = true;

            }
            else
            {
                pnlEngaged.Visible = false;
                //PnlAddEngaged.Visible = false;
            }

            //ds1 = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"]), YR, leaveno);//Session["idno"]
            //ds1 = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"]), DateTime.Now.Year, LNO);//Session["idno"]
            ds1 = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"]), YR, LNO);//Session["idno"]
            int last_row = 0;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds1.Tables[0].Rows.Count) > 1)
                {
                    int tot = Convert.ToInt32(ds1.Tables[0].Rows.Count);
                    int exist_rno = Convert.ToInt32(ViewState["RNO"]);
                    for (int i = 0; i < tot; i++)
                    {
                        string Working_date = ds1.Tables[0].Rows[i]["DATE"].ToString();
                        int start = Working_date.IndexOf('_');
                        if (start > 0)
                        {
                            //Code: to check only for comp-off leave because for other leave no date exists so return start=-1
                            start = start + 1;
                            string rno = Working_date.Substring(start);
                            if (Convert.ToInt32(rno) == exist_rno)
                            {
                                last_row = i;
                                break;
                            }
                        }
                    }
                }
                balance = Convert.ToDouble(ds1.Tables[0].Rows[last_row]["bal"].ToString());
                if (balance <= 0)
                {
                    // MessageBox("Sorry ! Insufficient Balance");
                    // btnSave.Enabled = false;
                    // return;
                }
                else
                {
                    btnSave.Enabled = true;
                }
                ViewState["SESSION_SRNO"] = ds1.Tables[0].Rows[last_row]["SESSION_SRNO"].ToString();
                ViewState["PERIOD_NO"] = ds1.Tables[0].Rows[last_row]["PERIOD_NO"].ToString();
                ViewState["LNO_NEW"] = ds1.Tables[0].Rows[last_row]["LNO_NEW"].ToString();

                //  ViewState["SESSION_SRNO"] = ds1.Tables[0].Rows[last_row]["SESSION_SRNO"].ToString();
                // ViewState["PERIOD_NO"] = ds1.Tables[0].Rows[last_row]["PERIOD_NO"].ToString();
                // ViewState["LNO_NEW"] = ds1.Tables[0].Rows[last_row]["LNO_NEW"].ToString();


                ViewState["MAX_DAYS_TO_APPLY"] = ds1.Tables[0].Rows[last_row]["MAX_DAYS_TO_APPLY"].ToString();
                ViewState["MIN_DAYS_TO_APPLY"] = ds1.Tables[0].Rows[last_row]["MIN_DAYS_TO_APPLY"].ToString();

                ViewState["MAX_DAYS_ALLOWED_TOPD"] = ds1.Tables[0].Rows[last_row]["MAX_DAYS_ALLOWED_TOPD"].ToString();
                ViewState["MAX_DAYS_ALLOWED_TOPDL"] = ds1.Tables[0].Rows[last_row]["MAX_DAYS_ALLOWED_TOPDL"].ToString();

                ViewState["MAX_DAYS_TO_PD"] = ds1.Tables[0].Rows[last_row]["MAX_DAYS_TO_PD"].ToString();
                ViewState["MAX_DAYS_TO_PDL"] = ds1.Tables[0].Rows[last_row]["MAX_DAYS_TO_PDL"].ToString();

                leavename = ViewState["LEAVE"].ToString();
                ViewState["LNO"] = LNO;
                txtLeavename.Text = ds1.Tables[0].Rows[last_row]["LEAVENAME"].ToString();

                ViewState["LEAVENAME"] = leavename.ToString().Trim();
                double bal = balance;
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    IsCompOff = Convert.ToBoolean(ds1.Tables[1].Rows[0]["IsCompOff"]);
                    ViewState["IsCompOff"] = IsCompOff;
                    ViewState["IsLWP"] = Convert.ToBoolean(ds1.Tables[1].Rows[0]["IsLWP"]);
                    ViewState["IsPayLeave"] = Convert.ToBoolean(ds1.Tables[1].Rows[0]["IsPayLeave"]);
                    IsPayLeave = Convert.ToBoolean(ds1.Tables[1].Rows[0]["IsPayLeave"]);
                    ViewState["Leave_ShortName"] = ds1.Tables[1].Rows[0]["Leave_ShortName"].ToString();
                    if (IsCompOff == true)
                    {
                        txtLeavename.Text = leavename_compoff;
                    }
                }
                if (IsCompOff == true)
                {
                    //bal
                    txtLeavebal.Text = bal.ToString();
                }
                else
                {
                    txtLeavebal.Text = ds1.Tables[0].Rows[last_row]["bal"].ToString();
                }


                if (IsPayLeave == true)
                {
                    trml.Visible = true;
                    trotherlvs.Visible = false;
                }
                else
                {
                    trml.Visible = false;
                    trotherlvs.Visible = true;
                }
                GetPAPath1();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        txtJoindt.Text = string.Empty;
        ddlNoon.SelectedIndex = 0;
        txtReason.Text = string.Empty;
        chkUFit.Checked = false;
        ChkFit.Checked = false;
        txtNodays.Text = string.Empty;
        txtadd.Text = string.Empty;
        txtcharge.Text = string.Empty;
        chklvapply.Checked = false;
        chklvapply.Visible = false;

        txtLeaveType.Text = string.Empty;

        rblleavetype.SelectedValue = "0";
        ddlLeaveFNAN.Enabled = false;
        ddlLeaveFNAN.SelectedValue = "0";
        rdbml.SelectedValue = "0";
        pnlLeaveRestrictHolidays.Visible = false;
        lnkRestrictedLeaves.Visible = true;

        rblIsCertificate.SelectedValue = "0";
        //Clear();
        pnlEngaged.Visible = false;
        PnlAddEngaged.Visible = false;
        lvEngagedInfo.DataSource = null;
        lvEngagedInfo.DataBind();
        dteng = (DataTable)ViewState["dteng"];
        dteng.Clear();
        lvFacLoad.DataSource = null;
        lvFacLoad.DataBind();
        pnlFacLoad.Visible = false;
        dtLoad = (DataTable)ViewState["dtLoad"];
        dtLoad.Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        ViewState["action"] = null;
        chklvapply.Checked = false;
        Clear();
        lnkbut.Visible = true;

        trhalfDay.Visible = false;
        trhalfjoinDay.Visible = false;


        rblleavetype.Items.Clear();
        rblleavetype.Items.Insert(0, new ListItem("Full Day", "0"));
        rblleavetype.Items.Insert(1, new ListItem("Half Day", "1"));
        rblleavetype.Items[0].Selected = true;


    }



    private void Clear()
    {
        try
        {
            //txtLeavename.Text = string.Empty;
            //txtLeavebal.Text = string.Empty;
            txtFromdt.Text = string.Empty;
            txtTodt.Text = string.Empty;
            txtJoindt.Text = string.Empty;
            ddlNoon.SelectedIndex = 0;
            txtReason.Text = string.Empty;
            chkUFit.Checked = false;
            ChkFit.Checked = false;
            txtNodays.Text = string.Empty;
            txtadd.Text = string.Empty;
            txtcharge.Text = string.Empty;
            chklvapply.Checked = false;
            chklvapply.Visible = false;

            txtLeaveType.Text = string.Empty;
            //code to delete from temp table

            objLM.LNO = 0;
            objLM.TRANNO = 0;
            objLM.EMPNO = 0;
            objLM.LETRNO = 0;

            ViewState["SESSION_SRNO"] = null; ViewState["PERIOD_NO"] = ViewState["SESSION_SERVICE_SRNO"] = ViewState["ISCERTIFICATE"] = null;
            ViewState["SUBDEPTNO"] = null;
            ViewState["STNO"] = null;
            ViewState["COLLEGE_NO"] = null;
            ViewState["STAFFNO"] = null;
            ViewState["LEAVENO"] = null;
            Session["LNO"] = null;
            ViewState["letrno"] = null;
            lnkbut.Visible = true;
            ViewState["MAX_DAYS_TO_APPLY"] = null;
            ViewState["MIN_DAYS_TO_APPLY"] = null;
            ViewState["SESSION_SERVICE_SRNO"] = null;
            ViewState["SERVICE_LIMIT"] = null;
            ViewState["MAX_SERVICE_COMPLETE"] = ViewState["FileName"] = ViewState["File"] = null;
            ViewState["TimeforLeave"] = null;

            ViewState["MAX_DAYS_TO_PDL"] = null;
            ViewState["MAX_DAYS_ALLOWED_TOPDL"] = null;
            ViewState["MAX_DAYS_TO_PD"] = null;
            ViewState["MAX_DAYS_ALLOWED_TOPD"] = null;
            ViewState["EMAILID"] = null;
            ViewState["PHONENO"] = null;
            ViewState["RNO"] = null;
            ViewState["isValidatedays"] = null;

            rblleavetype.SelectedValue = "0";
            ddlLeaveFNAN.Enabled = false;
            ddlLeaveFNAN.SelectedValue = "0";
            rdbml.SelectedValue = "0";
            pnlLeaveRestrictHolidays.Visible = false;
            lnkRestrictedLeaves.Visible = true;

            rblleavetype.Items.Clear();
            rblleavetype.Items.Insert(0, new ListItem("Full Day", "0"));
            rblleavetype.Items.Insert(1, new ListItem("Half Day", "1"));
            rblleavetype.Items[0].Selected = true;


            txtEngagedDate.Text = txtTime.Text = txtsubject.Text = txtyear.Text = string.Empty;
            pnlEngaged.Visible = false;
            PnlAddEngaged.Visible = false;
            lvEngagedInfo.DataSource = null;
            lvEngagedInfo.DataBind();
            dteng = (DataTable)ViewState["dteng"];
            dteng.Clear();
            ViewState["IsLeaveWisePassingPath"] = null;
            ViewState["IsCompOff"] = null;
            ViewState["LEAVE"] = null;
            ViewState["DaysforCertificate"] = null;
            lvFacLoad.DataSource = null;
            lvFacLoad.DataBind();
            pnlFacLoad.Visible = false;
            dtLoad = (DataTable)ViewState["dtLoad"];
            dtLoad.Clear();
            ViewState["IsRequiredLoad"] = null;
            ViewState["IsLeaveValid"] = null;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.clear ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
        //======================
    }
    protected void GetPAPath(int EmpNo, int Lno)
    {
        string paname1 = string.Empty; string paname2 = string.Empty; string paname3 = string.Empty; string paname4 = string.Empty; string paname5 = string.Empty;
        string uano1 = string.Empty; string uano2 = string.Empty; string uano3 = string.Empty; string uano4 = string.Empty; string uano5 = string.Empty;

        string path = string.Empty;
        string userno = Session["userno"].ToString();
        DataSet dsAuth = new DataSet();
        dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno, "");
        // string pano = dsAuth.Tables[0].Rows[0]["PANO"].ToString();

        DataSet dsdept = new DataSet();
        dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
        string dept = dsdept.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();

        DataSet dspath = new DataSet();
        dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "DEPTNO=" + dept, "");

        //for (int i = 0; i < dspath.Tables[0].Columns.Count; i++)
        //{
        //    if (dspath.Tables[0].Rows[0][i].ToString() == pano)
        //    {
        //        string colname = dspath.Tables[0].Columns[i].ColumnName;


        //    }
        //    string nextcol = dspath.Tables[0].Columns[i + 1].ColumnName;
        //}
        string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
        string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
        string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
        string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
        string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();
        DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
        uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
        paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();

        DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
        if (dsauth2.Tables[0].Rows.Count > 0)
        {

            uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
            paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();
        }
        DataSet dsauth3 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
        if (dsauth3.Tables[0].Rows.Count > 0)
        {
            uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
            paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();
        }
        DataSet dsauth4 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
        if (dsauth4.Tables[0].Rows.Count > 0)
        {
            uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
            paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();
        }
        DataSet dsauth5 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
        if (dsauth5.Tables[0].Rows.Count > 0)
        {

            uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
            paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();
        }
        //string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
        //string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
        //string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
        //string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
        //string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();
        //DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO="+pano1 , "");
        //string uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
        //string paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();

        //DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
        //string uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
        //string paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();

        //DataSet dsauth3 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
        //string uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
        //string paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();

        //DataSet dsauth4 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
        //string uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
        //string paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();

        //DataSet dsauth5 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
        //string uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
        //string paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();


        if (userno == uano1)
        {
            path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
        }
        else if (userno == uano2)
        {
            path = paname3 + "->" + paname4 + "->" + paname5;
        }
        else if (userno == uano3)
        {
            path = paname4 + "->" + paname5;
        }
        else if (userno == uano4)
        {
            path = paname5;
        }
        else if (userno == uano5)
        {
            path = paname5;
        }
        else
        {
            path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
        }
        txtPath.Text = path;

        ddlPath.Items.Clear();
        try
        {
            DataSet ds = null;
            ds = objApp.GetPAPath_EmpNO(EmpNo, Lno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPath.DataSource = ds;
                ddlPath.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlPath.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlPath.DataBind();
                ddlPath.SelectedIndex = 0;
            }


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();
            //DataSet dsAuth = new DataSet();
            int useridno = Convert.ToInt32(Session["idno"]);
            //int collegeno =Convert.ToInt32( objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + useridno + ""));
            int collegeno = Convert.ToInt32(ViewState["COLLEGE_NO"]);
            //dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno+"  AND COLLEGE_NO="+ collegeno +" ", "");
            //string pano = dsAuth.Tables[0].Rows[0]["PANO"].ToString();

            //DataSet dsdept = new DataSet();
            //dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
            //string dept = dsdept.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();

            DataSet dspath = new DataSet();
            //dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "DEPTNO=" + dept+" AND COLLEGE_NO="+ collegeno +" ", "");
            // dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " ", "");
            if (Convert.ToBoolean(ViewState["IsLeaveWisePassingPath"]) == true)
            {
                dspath = null;
                dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " AND Leavevalue=" + ViewState["LEAVENO"] + " ", "");
            }
            else
            {
                dspath = null;
                dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " AND isnull(Leavevalue,0) =" + 0 + " ", "");
            }
            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();


                string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
                string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
                string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
                string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
                string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();


                string uano1 = string.Empty;
                string uano2 = string.Empty;
                string uano3 = string.Empty;
                string uano4 = string.Empty;
                string uano5 = string.Empty;
                string paname1 = string.Empty;
                string paname2 = string.Empty;
                string paname3 = string.Empty;
                string paname4 = string.Empty;
                string paname5 = string.Empty;

                DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
                if (dsauth1.Tables[0].Rows.Count > 0)
                {
                    uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
                if (dsauth2.Tables[0].Rows.Count > 0)
                {
                    uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth3 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
                if (dsauth3.Tables[0].Rows.Count > 0)
                {
                    uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth4 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
                if (dsauth4.Tables[0].Rows.Count > 0)
                {
                    uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth5 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
                if (dsauth5.Tables[0].Rows.Count > 0)
                {
                    uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();
                }


                if (userno == uano1)
                {
                    path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano2)
                {
                    path = paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano3)
                {
                    path = paname4 + "->" + paname5;
                }
                else if (userno == uano4)
                {
                    path = paname5;
                }
                else if (userno == uano5)
                {
                    path = paname5;
                }
                else
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                txtPath.Text = path;

            }
            else
            {
                MessageBox("Sorry! Authority Not found");
                txtPath.Text = string.Empty;
                return;
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }

    //protected void checkAuthority()
    //{
    //    bool result = false;
    //    DataSet dsAuth = new DataSet();
    //    //dsAuth = objCommon.FillDropDown("PAYROLL_COLLEGE", "*", "", "CollegeName='" + txtCollege.Text + "'", "");


    //    int path1 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "PAN01", ""));
    //    int path2 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "PAN02", ""));
    //    int path3 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "PAN03", ""));
    //    int path4 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "PAN04", ""));
    //    int path5 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "PAN05", ""));

    //    int p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0;
    //    int auth1 = 0, auth2 = 0, auth3 = 0, auth4 = 0, auth5 = 0;


    //    if (path5 > 0)
    //    {
    //        p5 = path5;
    //        auth5 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + p5));
    //        //dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "", "PANO=" + p5, "");
    //        //return dsAuth;
    //    }
    //    else
    //    {
    //        if (path4 > 0)
    //        {
    //            p4 = path4;
    //            auth4 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + p4));
    //        }
    //        else
    //        {
    //            if (path3 > 0)
    //            {
    //                p3 = path3;
    //                auth3 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + p3));
    //            }
    //            else
    //            {
    //                if (path2 > 0)
    //                {
    //                    p2 = path2;
    //                    auth2 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + p2));
    //                }
    //                else
    //                {
    //                    if (path1 > 0)
    //                    {
    //                        p1 = path1;
    //                        auth1 = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + p1));
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    //if (Session["userno"] == auth1.ToString() || Session["userno"] == auth2.ToString() || Session["userno"] == auth3.ToString() || Session["userno"] == auth4.ToString() || Session["userno"] == auth5.ToString())
    //    //{


    //    //}

    //}


    private void SendSMS(int letrno)
    {
        //int pano = Convert.ToInt32(objCommon.LookUp("payroll_leave_app_pass_entry", "pano", "STATUS='P' AND letrno=" + letrno));
        //int ua_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "ua_no", "PANO=" + pano));
        //string Mobile_no = objCommon.LookUp("user_acc", "ua_mobile", "ua_no=" + ua_no);
        //string name = objCommon.LookUp("payroll_empmas", "fname+' '+mname+' '+ lname as name", "idno=" + Convert.ToInt32(Session["idno"]));
        string url = "http://smsnmms.co.in/sms.aspx";
        string uid = string.Empty; string pass = string.Empty; string mobno = string.Empty; string message, Mobile_no = string.Empty;
        //select issms,* from payroll_leave_ref
        bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
        if (isSMS == true)
        {
            DataSet dsReff = objCommon.FillDropDown("REFF", "SMSSVCID", "SMSSVCPWD", "", "");
            if (dsReff.Tables[0].Rows.Count > 0)
            {
                uid = dsReff.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = dsReff.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
            }
            objLM.LETRNO = letrno;
            DataSet ds = objApp.GetSMSInformation(objLM);

            if (ds.Tables[0].Rows.Count > 0)
            {


                string employeename = ds.Tables[1].Rows[0]["name"].ToString();

                double tot_days = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                string Joindt = ds.Tables[0].Rows[0]["Joindt"].ToString();
                string PHONENO = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                string FromDate = ds.Tables[1].Rows[0]["From_Date"].ToString();
                string ToDate = ds.Tables[1].Rows[0]["To_Date"].ToString();
                string AUTHORITY_NAME = ds.Tables[1].Rows[0]["AUTHORITY_NAME"].ToString();

                string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
                string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
                string leavenamestatus = string.Empty;

                if (leavestatus.Trim() != string.Empty || leavestatus != "")
                {
                    leavenamestatus = leavestatus + ' ' + leavename;
                }
                else
                {
                    leavenamestatus = leavename;
                }
                //Full day/half day [casual] Leave Application Submitted By ['ABC'] for [2] days & Joining Date is [Joindt]
                //   message = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;
                // message = leavestatus + leavename + " Application Submitted By " + name + ". Date From " + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + " & Joining Date is " + Joindt;
                message = "Hi " + AUTHORITY_NAME + "," + Environment.NewLine + employeename + " " + "has applied " + leavenamestatus + " from " + FromDate
                             + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                             + "Joining date will be " + Joindt;

                WebRequest request = HttpWebRequest.Create("" + url + "?ID=" + uid + "&PWD=" + pass + "&PHNO=" + PHONENO + "&TEXT=" + message + "");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd();
            }
        }
    }
    public void SendMail(int letrno)
    {
        try
        {
            string body = string.Empty;
            objLM.LETRNO = letrno;
            DataSet ds = objApp.GetSMSInformation(objLM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string fromEmailId = ds.Tables[0].Rows[0]["SenderEmailId"].ToString();
                string fromEmailPwd = ds.Tables[0].Rows[0]["SenderEmailPassword"].ToString();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    string Name = ds.Tables[1].Rows[0]["NAME"].ToString();
                    string AUTHORITY_NAME = ds.Tables[1].Rows[0]["AUTHORITY_NAME"].ToString();
                    //AUTHORITY_NAME
                    string FromDate = ds.Tables[1].Rows[0]["From_Date"].ToString();
                    string ToDate = ds.Tables[1].Rows[0]["To_Date"].ToString();

                    string employeename = ds.Tables[1].Rows[0]["name"].ToString();
                    string department = ds.Tables[1].Rows[0]["department"].ToString();
                    double tot_days = Convert.ToDouble(ds.Tables[1].Rows[0]["NO_OF_DAYS"].ToString());
                    string Joindt = ds.Tables[1].Rows[0]["Joindt"].ToString();
                    string toEmailId = ds.Tables[1].Rows[0]["EMAILID"].ToString();
                    string Sub = "Related To Leave Application";
                    //Full day/half day [casual] Leave Application Submitted By ['ABC']. Date from 1/1/2018 to 1/12018 & Joining Date is [Joindt]
                    //body = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;

                    //body = leavestatus + leavename + " Application Submitted By " + name + ". Date From " + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + " & Joining Date is " + Joindt;

                    //*Mail Subject-* Related To Leave Application
                    //*Mail body -*
                    //Leave Applicant Name : 
                    //Department :
                    //Leave Name :
                    //Number of Days :
                    //Application Date :

                    //Thanks and Regards
                    //Applicant Name

                    string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
                    string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
                    string leavenamestatus = string.Empty;

                    if (leavestatus.Trim() != string.Empty || leavestatus != "")
                    {
                        leavenamestatus = leavestatus + ' ' + leavename;
                    }
                    else
                    {
                        leavenamestatus = leavename;
                    }


                    //Applicant Name of department has applied leave leavename for Total No of days from date to todate on application date

                    body = "Hi " + AUTHORITY_NAME + "," + Environment.NewLine + Environment.NewLine + employeename + " of department " + department + " " + "has applied " + leavenamestatus + " for " + tot_days + " days from " + FromDate
                               + " " + "to " + ToDate + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " , " + Environment.NewLine + ""
                               + "Joining date will be " + Joindt + "." + Environment.NewLine + Environment.NewLine + "Thanks and Regards" + Environment.NewLine + employeename;

                    string ToEmail = ds.Tables[1].Rows[0]["EmailId"].ToString();

                    //SmtpClient smtpClient = new SmtpClient();
                    //MailMessage mailMessage = new MailMessage(fromEmailId, toEmailId, Sub, body);
                    //// MailMessage mailMessage = new MailMessage(fromEmailId, "vidisha.kamatkar@mastersofterp.co.in", Sub, body);
                    //smtpClient.Credentials = new System.Net.NetworkCredential(fromEmailId, fromEmailPwd);

                    //smtpClient.EnableSsl = true;
                    //smtpClient.Port = 587; // 587
                    //smtpClient.Host = "smtp.gmail.com";

                    //ServicePointManager.ServerCertificateValidationCallback =
                    //delegate(object s, X509Certificate certificate,
                    //X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    //{ return true; };

                    //smtpClient.Send(mailMessage);// .SendAsync(mailMessage, null);
                    // lblMsg.Text = "Email has been successfully sent..!!";
                    /*
                       text = "Hi " + multipleName + "," + Environment.NewLine + employeename + " " + "has applied a leave from " + FromDate
                                + " " + "to " + ToDate + " " + Environment.NewLine + "with reason " + "'" + txtReason.Text.Trim() + "'" + ","
                                + "Joining date will be " + JoiningDate;
                     * */
                    //string email_type, Link;
                    //DataSet dsMailconfig = getModuleConfig();
                    //if (dsMailconfig != null && dsMailconfig.Tables.Count > 0 && dsMailconfig.Tables[0].Rows.Count > 0)
                    //{
                    //    email_type = dsMailconfig.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                    //    Link = dsMailconfig.Tables[0].Rows[0]["LINK"].ToString();

                    //}




                    SendEmailCommon objSendEmail = new SendEmailCommon(); 
                    int status = 0;
                    try
                    {
                        //status = SendMailBYSendgrid(message, emailid, subject);
                        //status = sendEmail(message, emailid, subject);

                        //Task<int> task = Execute(body, toEmailId, Sub);
                        //status = task.Result;                       

                        status = objSendEmail.SendEmail(toEmailId, body, Sub);

                    }
                    catch (Exception ex3)
                    {
                        //lbl3.Text = "lbl3  " + "email status " + status + " " + ex3.ToString();
                    }
                }
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    public void insertdata()
    {
        try
        {
            ServiceBookController objServiceBook = new ServiceBookController();
            byte[] doc_image;
            int RNO;
            //if (Session["RNO"] == "")
            if (ViewState["RNO"] == "")
            {
                RNO = 0;
            }
            else
            {
                RNO = Convert.ToInt32(ViewState["RNO"]);
            }
            //this.Restrictleavetaken();
            Leaves objLeaves = new Leaves();

            int empidnocheck = Convert.ToInt32(Session["idno"]);
            //int stafftypeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + empidnocheck));
            int stafftypeno = Convert.ToInt32(ViewState["STNO"]);

            double nodays = 0;
            double noleave = 0;
            int lno = 0;
            double bal;
            DataSet dslv = null;
            int lno1 = 0;
            int idno = Convert.ToInt32(Session["idno"]);
            DateTime now, next;
            DateTime frmdt, dt;
            int records = 0;

            DataSet ds, ds1 = null;

            Boolean leavefnan = false;
            if (divIsCertificate.Visible == true)
            {
                if (rblIsCertificate.SelectedValue == "0")
                {
                    objLeaves.IsWithCertificate = true;
                }
                else
                {
                    objLeaves.IsWithCertificate = false;
                    bool flag = true;
                }
            }
            if (fuUploadImage.HasFile)
            {
                //objLeaves.FileName = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                string File = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                string ext1 = System.IO.Path.GetExtension(fuUploadImage.PostedFile.FileName);
                string Filename = Path.GetFileNameWithoutExtension(File);
                objLeaves.FileName = Regex.Replace(Filename, @"[^0-9a-zA-Z:,]+", "") + ext1;
            }

            //if (leavename == "HPL" || static_lno_edit == 3)
            if (ViewState["IsPayLeave"] != null)
            {
                if (Convert.ToInt32(ViewState["IsPayLeave"]) == 1)
                {
                    objLeaves.MLHPL = 0;
                    if (rdbml.SelectedValue == "0")
                        objLeaves.MLHPL = 1;//full pay
                    else if (rdbml.SelectedValue == "1")
                        objLeaves.MLHPL = 2;
                    if (ViewState["Certificate"] != null)
                    {
                        if (ViewState["Certificate"].ToString().Trim() == "Y".ToString().Trim())
                        {
                            if (ViewState["File"] != null)
                            {
                                //doc_image = ViewState["File"] as byte[];
                                //objLeaves.docimage = doc_image;
                            }
                            else
                            {
                                MessageBox("You have already avail 3 one day medical leave, Please Upload Medical Certificate");
                                return;
                            }
                        }
                    }
                }
            }

            if (Convert.ToBoolean(ViewState["ISCERTIFICATE"]) == true)
            {
                //if (fuUploadImage.HasFile == false)
                //{
                //    MessageBox("Please Upload Document");
                //    //btnSave.Enabled = false;
                //    return;
                //}
                //else
                //{

                //}
                if (Convert.ToDecimal(txtNodays.Text) >= Convert.ToDecimal(ViewState["DaysforCertificate"]) && fuUploadImage.HasFile == false)
                {
                    MessageBox("Sorry ! Please Select Document");
                    return;
                }
                else
                {
                }
            }



            //DateTime dt;
            date = Convert.ToDateTime(txtFromdt.Text).ToString("dd/MM/yyyy");
            dt = findfix(Convert.ToDateTime(txtFromdt.Text), -1);
            int PrefixDays = counter;
            string prefix;
            if (PrefixDays > 0)
                prefix = date.Remove(0, 11);
            else
                prefix = "";
            counter = 0;
            date = Convert.ToDateTime(txtTodt.Text).ToString("dd/MM/yyyy");
            dt = findfix(Convert.ToDateTime(txtTodt.Text), 1);
            int SuffixDays = counter;
            string suffix;
            if (SuffixDays > 0)
            {
                suffix = date.Remove(0, 11);
            }
            else
            {
                suffix = "";
            }
            ds1 = objApp.GetSuffixDays(Convert.ToString(txtTodt), SuffixDays);


            if (trml.Visible == true && rdbml.SelectedValue == "0")
            {


                if (rblIsCertificate.SelectedValue == "0" && rblIsCertificate.Visible == true)
                {
                    //if (chkUFit.Checked == false && ChkFit.Checked == false)
                    //{
                    //    MessageBox("Sorry ! Please Select Atleast One Certificate");
                    //    chkUFit.Focus();
                    //    return;
                    //}

                    if (fuUploadImage.HasFile == false)
                    {

                        if (ViewState["FileName"] != null)
                        {
                            objLeaves.FileName = ViewState["FileName"].ToString();

                        }
                        else
                        {
                            objLeaves.FileName = string.Empty;
                        }
                        MessageBox("Sorry ! Please Select Document");
                        return;
                    }
                }
            }


            Boolean fit = false;
            if (ChkFit.Checked) fit = true;
            Boolean Unfit = false;
            if (chkUFit.Checked) Unfit = true;
            Boolean noonbit = false;
            if (ddlNoon.SelectedValue == Convert.ToString(1))
                noonbit = true;

            objLeaves.EMPNO = Convert.ToInt32(Session["idno"]);//Session["idno"]
            objLeaves.APPDT = Convert.ToDateTime(DateTime.Now.Date);
            objLeaves.LType = txtLeaveType.Text;
            //objLeaves.DEPTNO = deptno;
            if (!txtFromdt.Text.Trim().Equals(string.Empty)) objLeaves.FROMDT = Convert.ToDateTime(txtFromdt.Text);
            if (!txtTodt.Text.Trim().Equals(string.Empty)) objLeaves.TODT = Convert.ToDateTime(txtTodt.Text);


            //objLeaves.NO_DAYS = Convert.ToDouble(txtNodays.Text);
            if (!txtJoindt.Text.Trim().Equals(string.Empty)) objLeaves.JOINDT = Convert.ToDateTime(txtJoindt.Text);
            objLeaves.FIT = fit;
            objLeaves.UNFIT = Unfit;
            objLeaves.FNAN = noonbit;

            objLeaves.REASON = Convert.ToString(txtReason.Text);
            objLeaves.ADDRESS = Convert.ToString(txtadd.Text);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeaves.PREFIX = PrefixDays;
            objLeaves.SUFFIX = SuffixDays;

            if (!txtcharge.Text.Trim().Equals(string.Empty)) objLeaves.CHARGE_HANDED = txtcharge.Text;

            if (txtPath.Text.Equals(string.Empty))
            {
                objLeaves.PAPNO = 0;
            }
            else
            {
                objLeaves.PAPNO = Convert.ToInt32(ViewState["papno"]);
                //Convert.ToInt32(ddlPath.SelectedValue);
            }

            //int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno));
            int stno = Convert.ToInt32(ViewState["STNO"]);
            //DataSet dsres = objCommon.FillDropDown("PAYROLL_LEAVE_RESTRICT_FOR_DAYS", "*", "LNO", "STNO=" + stno, "");

            double dayno = Convert.ToDouble(txtNodays.Text);


            objLeaves.LEAVENO = Convert.ToInt32(ViewState["LEAVENO"]);
            //added lwp check condition
            if (chklvapply.Checked == true)
            {
                int lwp_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "ISNULL(LWP_NO,0) AS LWP_NO", ""));
                if (lwp_no == 0)
                {
                    MessageBox("LWP leave not created. Please Contact to Administrator!");
                    return;
                }
                else
                {
                    objLeaves.LNO = lwp_no;
                    objLeaves.LEAVENO = lwp_no;
                }
            }
            else
            {
                objLeaves.LNO = Convert.ToInt32(ViewState["LNO"].ToString());
            }
            //int period = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "PERIOD", "LNO=" + objLeaves.LNO.ToString()));
            // int period = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "PERIOD", "LEAVENO=" + objLeaves.LNO.ToString() + " AND STNO=" + stafftypeno));
            int period = Convert.ToInt32(ViewState["PERIOD"]);
            objLeaves.PERIOD = Convert.ToInt32(period);
            objLeaves.ENTRY_STATUS = "LEAVE ENTRY";



            if (ViewState["SESSION_SERVICE_SRNO"] != "")
            {
                objLeaves.SESSION_SERVICE_SRNO = Convert.ToInt32(ViewState["SESSION_SERVICE_SRNO"]);
            }
            else
            {
                objLeaves.SESSION_SERVICE_SRNO = 0;
            }

            /////////////////////////////////////////////////////

            if (rblleavetype.SelectedValue == "1")//half day
            {
                objLeaves.ISFULLDAYLEAVE = false;
            }
            else
            {
                objLeaves.ISFULLDAYLEAVE = true;
            }
            if (trml.Visible == true && trotherlvs.Visible == false)
            {
                //objLeaves.MLHPL = 
                if (rdbml.SelectedValue == "0")//full pay
                {
                    objLeaves.MLHPL = 1;//full pay
                }
                else
                {
                    objLeaves.MLHPL = 2;//half pay
                }
            }
            if (ddlChargeHanded.SelectedIndex > 0)
            {
                objLeaves.CHIDNO = Convert.ToInt32(ddlChargeHanded.SelectedValue);
            }
            else
            {
                objLeaves.CHIDNO = 0;
            }
            //Boolean CLASS_ARRAN_STATUS = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE", "ISNULL(IsClassArrangeAcceptanceRequired,0) AS IsClassArrangeAcceptanceRequired", "LNO =" + Convert.ToInt32(ViewState["LNO"].ToString()) + ""));
            //objLeaves.CLASS_ARRAN_STATUS = Convert.ToString('P');
            //if (CLASS_ARRAN_STATUS == true)
            //{
            //    objLeaves.CLASS_ARRAN_STATUS = Convert.ToString('P');
            //}
            //else
            //{
            //    objLeaves.CLASS_ARRAN_STATUS = null;
            //}
            dteng = (DataTable)ViewState["dteng"];
            if (dteng.Rows.Count > 0)
            {
                objLeaves.CLASS_ARRAN_STATUS = Convert.ToString('P');
            }
            else
            {
                objLeaves.CLASS_ARRAN_STATUS = null;
            }


            Boolean IsRequiredLoad = Convert.ToBoolean(ViewState["IsRequiredLoad"]);
            if (IsRequiredLoad == true)
            {
                SaveLoadTable();
                dtLoad = (DataTable)ViewState["dtLoad"];
                if (dtLoad.Rows.Count > 0)
                {
                    objLeaves.LOAD_STATUS = Convert.ToString('P');
                }
                else
                {
                    objLeaves.LOAD_STATUS = null;

                }
            }
            else
            {
                dtLoad = (DataTable)ViewState["dtLoad"];
                objLeaves.LOAD_STATUS = null;
            }
            if (IsRequiredLoad == true && lvFacLoad.Items.Count > 0)
            {
                //if (lvFacLoad.Items.Count >0)
                if (dtLoad.Rows.Count <= 0)
                {
                    MessageBox("Please Fill Load Detail.");
                    return;
                }
                foreach (ListViewDataItem dataItem in lvFacLoad.Items)
                {
                    DropDownList ddlFac = dataItem.FindControl("ddlFac") as DropDownList;

                    HiddenField hdnidno = dataItem.FindControl("hdnidno") as HiddenField;
                    if (ddlFac.SelectedValue == "0")
                    {
                        MessageBox("Please Fill Load Detail.");
                        return;
                    }                  
                }
            }

            double cal = Convert.ToDouble(ViewState["CAL"]);
            double noofdays = Convert.ToDouble(txtNodays.Text);
            double no = 0;
            string noofday = string.Empty;
            if (cal == 0.5)
            {
                noofday = (cal).ToString();
                no = (noofdays / cal);
            }
            else
            {
                noofday = (noofdays * cal).ToString();
                no = (cal);
            }
            objLeaves.NO_DAYS = Convert.ToDouble(noofday);

            if (ddlLeaveFNAN.SelectedValue == Convert.ToString(1))
            {
                leavefnan = true;
            }
            objLeaves.LEAVEFNAN = leavefnan;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    objLeaves.SESSION_SRNO = Convert.ToInt32(ViewState["SESSION_SRNO"]);
                    //objLeaves.LEAVENO = Convert.ToInt32(ViewState["LEAVENO"]);
                    DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
                    string userno = Session["userno"].ToString();
                    DataSet dsAuth = new DataSet();
                    dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno, "");

                    objLeaves.STATUS = "P";
                    //double cal = Convert.ToDouble(objCommon.LookUp("PAYROLL_LEAVE", "CAL", "LNO=" + Convert.ToInt32(ViewState["LNO"].ToString())));
                    // double cal = Convert.ToDouble(objCommon.LookUp("PAYROLL_LEAVE", "CAL", "LEAVENO=" + Convert.ToInt32(ViewState["LNO"].ToString()) + " AND STNO=" + stafftypeno));
                    
                    //double cal = Convert.ToDouble(ViewState["CAL"]);
                    //double noofdays = Convert.ToDouble(txtNodays.Text);
                    objLeaves.SESSION_SERVICE_SRNO = Convert.ToInt32(ViewState["SESSION_SERVICE_SRNO"]);
                    objLeaves.SESSION_SRNO = Convert.ToInt32(ViewState["SESSION_SRNO"]);
                    //double no = 0;
                    //string noofday = string.Empty;
                    //if (cal == 0.5)
                    //{
                    //    noofday = (cal).ToString();
                    //    no = (noofdays / cal);
                    //}
                    //else
                    //{
                    //    noofday = (noofdays * cal).ToString();
                    //    no = (cal);
                    //}
                    //objLeaves.NO_DAYS = Convert.ToDouble(noofday);

                    //if (ddlLeaveFNAN.SelectedValue == Convert.ToString(1))
                    //{
                    //    leavefnan = true;
                    //}
                    //objLeaves.LEAVEFNAN = leavefnan;
                    //dteng = (DataTable)ViewState["dteng"];

                    //
                    int ret_letrno = Convert.ToInt32(objApp.AddAPP_ENTRY(objLeaves, prefix, suffix, RNO, dteng, dtLoad));
                    letrno_static = ret_letrno;
                    if (ret_letrno > 0)
                    {
                        if (rblIsCertificate.SelectedValue == "0" && rblIsCertificate.Visible == true)
                        {
                            if (fuUploadImage.HasFile)
                            {
                                //objLeaves.FileName = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                                string File = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                                string ext1 = System.IO.Path.GetExtension(fuUploadImage.PostedFile.FileName);
                                string Filename = Path.GetFileNameWithoutExtension(File);
                                objLeaves.FileName = Regex.Replace(Filename, @"[^0-9a-zA-Z:,]+", "") + ext1;
                                objServiceBook.upload_new_files("LEAVE_CERTIFICATE_DOCUMENT", idno, "LETRNO", "PAYROLL_LEAVE_APP_ENTRY", "LETRNO_", fuUploadImage);
                            }
                        }
                        pnlAdd.Visible = false;
                        pnllist.Visible = true;
                        ViewState["LNO"] = objLeaves.LNO.ToString();
                        //ViewState["LETRNO"] = objCommon.LookUp("payroll_leave_app_entry", "max(LETRNO)", "");
                        DisplayMessage("Record Saved Successfully");
                        objCommon.DisplayMessage("Record Saved Successfully", this);
                        ViewState["action"] = null;
                        bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
                        bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));
                        bool IsChargeHandedMail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(IsChargeHandedMail,0)as IsChargeHandedMail", ""));
                        Boolean Isclassarangement = Convert.ToBoolean(ViewState["IsClassArrangeRequired"]);
                        if (isSMS == true)
                        {
                            // SendSMS(ret_letrno);
                        }
                        else if (isEmail == true)
                        {
                            SendMail(ret_letrno);
                        }

                        if (IsChargeHandedMail == true)
                        {
                            if (Isclassarangement == true)
                            {
                                if (dteng.Rows.Count > 0)
                                {
                                    SendChargeHandoverMail(ret_letrno);
                                }
                            }

                            if (IsRequiredLoad == true)
                            {
                                if (dtLoad.Rows.Count > 0)
                                {
                                    SendChargeHandoverMail(ret_letrno);
                                }
                            }
                        }

                        Clear();
                    }
                    else
                    {
                        MessageBox("Leave Already Exists!");
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    objLeaves.NO_DAYS = dayno;
                    objLeaves.STATUS = "P";
                    objLeaves.LETRNO = Convert.ToInt32(ViewState["letrno"]);
                    objLeaves.ADDRESS = txtadd.Text;

                    if (fuUploadImage.HasFile)
                    {
                        //objLeaves.FileName = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                        string File = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                        string ext1 = System.IO.Path.GetExtension(fuUploadImage.PostedFile.FileName);
                        string Filename = Path.GetFileNameWithoutExtension(File);
                        objLeaves.FileName = Regex.Replace(Filename, @"[^0-9a-zA-Z:,]+", "") + ext1;
                        ViewState["FileName"] = objLeaves.FileName;
                    }
                    if (ViewState["FileName"] != null)
                    {
                        objLeaves.FileName = ViewState["FileName"].ToString();

                    }
                    else
                    {
                        objLeaves.FileName = string.Empty;
                    }
                    letrno_static = Convert.ToInt32(ViewState["letrno"]);
                    dteng = (DataTable)ViewState["dteng"];
                    CustomStatus cs = (CustomStatus)objApp.UpdateAppEntry(objLeaves, dteng, dtLoad);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        if (fuUploadImage.HasFile)
                        {
                            objServiceBook.update_upload("LEAVE_CERTIFICATE_DOCUMENT", letrno_static, ViewState["FileName"].ToString(), idno, "LETRNO_", fuUploadImage);
                        }
                        pnlAdd.Visible = false;
                        pnllist.Visible = true;
                        objCommon.DisplayMessage("Record Updated Successfully", this);
                        ViewState["LNO"] = objLeaves.LNO.ToString();

                        ViewState["action"] = null;
                        Clear();
                    }
                }
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Lname;
            string DATE = string.Empty;
            Leaves objLeaves = new Leaves();

            //if (static_lno_edit == 3)
            //bool IsPayLeave = false;
            //if (ViewState["IsPayLeave"] != null)
            //{
            //    if (Convert.ToBoolean(ViewState["IsPayLeave"]) == true)
            //    {
            //        IsPayLeave = true;
            //    }
            //    else
            //    {
            //        IsPayLeave = false;
            //    }
            //}
            //if (IsPayLeave == true)
            //{
            //    int empno = Convert.ToInt32(Session["idno"]);

            //    //select count(1) as TOTAL_APPLIED,EMPNO from payroll_leave_app_entry where empno=212 AND lno=3 and no_of_days=1 and status IN('P','A','T') AND 2015 BETWEEN YEAR(FROM_DATE) AND YEAR(TO_DATE) group BY EMPNO
            //    if ( txtFromdt.Text != string.Empty && (txtNodays.Text == "1".ToString().Trim() || txtNodays.Text == "1.00".ToString().Trim()))
            //    {
            //        //DateTime frm = Convert.ToDateTime(txtFromdt.Text);
            //        string frmdate = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
            //        DataSet ds = objCommon.FillDropDown("payroll_leave_app_entry", "LNO", "EMPNO", "empno=" + empno + " AND LNO=3 AND NO_OF_DAYS=1 AND STATUS IN('P','A','T') AND year('" + frmdate + "') BETWEEN YEAR(FROM_DATE) AND YEAR(TO_DATE)", "EMPNO");

            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
            //            if (count >= 3)
            //            {
            //                MessageBox("You have already avail 3 one day medical leave, So please upload Medical Certificate");
            //                ViewState["Certificate"] = "Y".ToString().Trim();
            //                return;
            //            }
            //            else
            //            {
            //                ViewState["Certificate"] = null;
            //            }
            //        }
            //        else
            //        {
            //            ViewState["Certificate"] = null;
            //        }
            //    }
            //    else
            //    {
            //        ViewState["Certificate"] = null;
            //    }

            //}

            if (txtPath.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Proper Path From Leave Passing Path Master", this);
            }
            else
            {
                if (txtLeavename.Text.Contains("("))
                {
                    string[] LeaveName = txtLeavename.Text.Split('(');

                    Lname = LeaveName[0].ToString();
                    string Lname1 = LeaveName[1].ToString();
                    DATE = (Lname1.TrimEnd(')'));
                }

                //string Lname2 = Lname1.Split(')').ToString();


                //DateTime Wdate = Convert.ToDateTime(Lname2);
                else
                {
                    Lname = txtLeavename.Text;
                }

                // int LeaveNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LEAVENAME='" + Lname + "'"));
                int LeaveNo = Convert.ToInt32(ViewState["LEAVENO"]);
                int comp_LeaveNo = 0;
                //DataSet dsComp = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "COMP_OFF", "COMP_OFF AS COMP_OFF1", "COMP_OFF=" + ViewState["LEAVENO"].ToString() + "", "");
                //if (dsComp.Tables[0].Rows.Count > 0)
                //{
                //    comp_LeaveNo = Convert.ToInt32(dsComp.Tables[0].Rows[0]["COMP_OFF"].ToString());

                //}
                //else
                //{
                //    //please make comp-off entry
                //}

                // if (LeaveNo == 4)
                //working hear----03-09-2018
                //if(comp_LeaveNo!=0)
                if (ViewState["IsCompOff"] != null)
                {
                    //if (Convert.ToInt32(ViewState["IsCompOff"]) == 1)
                    if (Convert.ToBoolean(ViewState["IsCompOff"]) == true)
                    {
                        string Todate = txtTodt.Text.ToString();
                        DateTime WDate = Convert.ToDateTime(txtFromdt.Text);
                        int idno = Convert.ToInt32(Session["idno"]);
                        string RNo = objCommon.LookUp("ESTB_COMP_OFF_REQUEST", "RNO", "idno=" + idno + " AND WORKING_DATE=convert(datetime,'" + Session["Working_Date"] + "',103)");

                        // string RNo = objCommon.LookUp("ESTB_COMP_OFF_REQUEST", "RNO", "CONVERT(NVARCHAR(30),WORKING_DATE,103) BETWEEN CONVERT(NVARCHAR(30),'" + txtFromdt.Text + "',103) AND CONVERT(NVARCHAR(30),'" + txtFromdt.Text + "',103)");
                        ViewState["RNO"] = RNo;
                        DateTime Nextmonth = WDate.AddMonths(+1);
                        int prvday = 0;
                        int nxtday = 0;
                        DateTime prv_date;
                        DateTime nxt_date;

                        string ExpiryDate = objCommon.LookUp("ESTB_COMP_OFF_REQUEST", "EXPIRY_DATE", "WORKING_DATE=convert(datetime,'" + DATE + "',103)AND EXPIRY_DATE>=convert(datetime,'" + Todate + "',103)");
                        if (ExpiryDate == "")
                        {
                            objCommon.DisplayMessage("Compansetary Leave Laps...", this.Page);
                        }
                        else
                        {
                            insertdata();
                        }
                    }
                    else
                    {
                        insertdata();
                    }
                }
                else
                {
                    insertdata();
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {

        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    public DateTime findfix(DateTime dt, double i)
    {
        // cnt = 0;
        //int count = 0;
        //int check = 1;
        //do
        //{
        //    dt = dt.AddDays(-1);
        //    int check = Convert.ToInt32(objCommon.LookUp("PAYROLL_HOLIDAYS", "COUNT(*)", "DT=" + dt).ToString());
        //    if (Convert.ToString(dt.DayOfWeek) != "Sunday" || Convert.ToString(dt.DayOfWeek) != "Saturday")
        //    cnt++;
        //}
        //while (check != 0);

        //if (Convert.ToString(dt.DayOfWeek) == "Sunday")
        //{
        //    count =2;

        //}
        //else if (Convert.ToString(dt.DayOfWeek) == "Saturday")
        //{

        //   count= 1;
        //}
        //else 
        //{
        //    count =0;
        //}

        //return count;
        dt = dt.AddDays(i);
        int check = Convert.ToInt32(objCommon.LookUp("payroll_holidays", "count(*)", "dt=CONVERT(DATETIME,'" + dt + "',103)"));
        string day = Convert.ToString(dt.DayOfWeek);
        if (check == 1)
        {
            date = date + ", " + dt.Date.ToString("dd/MM/yyyy");
            findfix(dt, i);
            counter = counter + 1;
        }
        else if (day == "Sunday")
        {
            date = date + ", " + dt.Date.ToString("dd/MM/yyyy");
            findfix(dt, i);
            counter = counter + 1;
        }
        //else if (day == "Saturday")
        //{
        //    date = date + ", " + dt.Date.ToString("dd/MM/yyyy");
        //    findfix(dt, i);
        //    counter = counter + 1;
        //}
        else
            day = (Convert.ToString(dt));

        return (dt);
    }
    //protected void lvLeaveinfo_ItemCommand(object sender, ListViewCommandEventArgs e)
    //{
    //    int j = Convert.ToInt16(e.Item.ID.ToString().Trim().Substring(4));

    //    Label levname = lvLeaveinfo.Items[j].FindControl("lblLname") as Label;
    //    txtLeavename.Text = levname.Text;
    //    Label lbl = lvLeaveinfo.Items[j].FindControl("lblbalance") as Label;
    //    txtLeavebal.Text = lbl.Text;

    //}
    protected void lnkbut_Click(object sender, EventArgs e)
    {
        BindListViewLeaveapplStatus();
        pnlLeaveStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        lnkbut.Visible = false;
        pnlLeaveRestrictHolidays.Visible = false;
        lnkRestrictedLeaves.Visible = false;
    }
    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
        pnlLeaveStatus.Visible = false;
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        lnkbut.Visible = true;
        pnlLeaveRestrictHolidays.Visible = false;

    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        Leaves objlv = new Leaves();
        Pending_count = 0;
        DataSet ds = null;
        try
        {
            txtLeaveType.Text = string.Empty;

            if (txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____" && txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____")
            {
                DateTime fromDate = Convert.ToDateTime(txtFromdt.Text.ToString());

                DateTime toDate = Convert.ToDateTime(txtTodt.Text.ToString());
                int empidno = Convert.ToInt32(Session["idno"]);
                int ret = CheckLeaveExists();
                if (ret == 1)
                {
                    return;
                }
                //=================================
                DataSet dsValidate = null;
                if (letrno_static != 0)
                {
                    dsValidate = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "SUM(NO_OF_DAYS) AS NO_OF_DAYS ", "EMPNO", "STATUS IN('P','F') AND LNO=" + Convert.ToInt32(ViewState["LNO"]) + " AND EMPNO=" + empidno + " and LETRNO<>" + Convert.ToInt32(letrno_static) + " AND SESSION_SRNO IN(SELECT SESSION_SRNO  FROM PAYROLL_LEAVE_SESSION S INNER JOIN PAYROLL_LEAVE L ON(L.LEAVENO=S.LEAVENO) where convert(date,getdate()) between S.FDT and S.TDT) GROUP BY EMPNO", "");
                }
                else
                {
                    dsValidate = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "SUM(NO_OF_DAYS) AS NO_OF_DAYS ", "EMPNO", "STATUS IN('P','F') AND LNO=" + Convert.ToInt32(ViewState["LNO"]) + " AND EMPNO=" + empidno + "  AND SESSION_SRNO IN(SELECT SESSION_SRNO  FROM PAYROLL_LEAVE_SESSION S INNER JOIN PAYROLL_LEAVE L ON(L.LEAVENO=S.LEAVENO) where convert(date,getdate()) between S.FDT and S.TDT) GROUP BY EMPNO", "");
                }


                if (dsValidate.Tables[0].Rows.Count > 0)
                {
                    Pending_count = Convert.ToDouble(dsValidate.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                }


                //============================

                if (toDate < fromDate)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtNodays.Text = string.Empty;
                    return;
                }
                else if (rblleavetype.SelectedValue == "1" && fromDate < toDate)
                {
                    MessageBox("Please select single day for Half Day Leave");
                    return;
                }
                else
                {
                    int days = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "ALLOWED_DAYS", "LNO=" + Convert.ToInt32(ViewState["LNO"])));
                    //int lno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LNO", "LNO=" + Convert.ToInt32(Session["lno"])));
                    int calholy = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "CAL_HOLIDAYS", "LNO=" + Convert.ToInt32(ViewState["LNO"])));



                    DateTime dt = Convert.ToDateTime(txtTodt.Text);
                    string todt = dt.ToShortDateString();


                    DateTime today = System.DateTime.Now.Date;
                    string todate = today.ToShortDateString();
                    DateTime dt2 = Convert.ToDateTime(todate);
                    DateTime sundaydt = dt2.AddDays(-1);

                    DateTime sub = today.AddDays(-days);
                    string validdate = sub.ToShortDateString();
                    DateTime frmdt = Convert.ToDateTime(txtFromdt.Text);

                    string fromdt = frmdt.ToShortDateString();
                    int frm = Convert.ToInt32(fromdt.Substring(0, 2));
                    int todaydt = Convert.ToInt32(todate.Substring(0, 2));


                    btnSave.Enabled = true;



                    //if (frmdt < sub)
                    //{

                    //    MessageBox("Not Allowed ,Please check To Date Todate is not more than before" + days + "day of today");
                    //    btnSave.Enabled = false;
                    //    return;

                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;
                    //}

                    //}
                    //////======================17 nov===st=========//////////////////////////////////////
                    //Modified By: swati ghate
                    //Date: 04-08-2014
                    //Reason: to Avoid existing leave entry
                    string from_date, to_Date;
                    int empid = Convert.ToInt32(Session["idno"]);
                    from_date = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
                    to_Date = Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd");

                    ///////////////////////////////////////////
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        if (txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____" && txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____")
                        {
                            DateTime fromDate1 = Convert.ToDateTime(txtFromdt.Text.ToString());

                            DateTime toDate1 = Convert.ToDateTime(txtTodt.Text.ToString());

                            if (toDate1 < fromDate1)
                            {
                                MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                                //txtTodt.Text = string.Empty;
                                txtNodays.Text = string.Empty;
                                txtJoindt.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                //DateInfo();
                            }
                        }
                    }
                    else
                    {

                    }

                    //////////////////////================17-nov=================///////////////////////////////
                    if (txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____" && txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____")
                    {
                        int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                        int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "college_no", "IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (ViewState["SESSION_SRNO"].ToString().Trim() != "0".ToString().Trim())
                        {
                            //select * from payroll_leave_session WHERE '2017-07-12' BETWEEN FDT AND TDT AND SESSION_SRNO=1
                            DataSet dsValidSession = objCommon.FillDropDown("PAYROLL_LEAVE_SESSION", "SESSION_SRNO", "SESSION_SRNO AS A", " '" + Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd") + "' BETWEEN FDT AND TDT AND SESSION_SRNO=" + Convert.ToInt32(ViewState["SESSION_SRNO"]) + "", "");
                            if (dsValidSession.Tables[0].Rows.Count <= 0)
                            {
                                MessageBox("Sorry ! Not allow to apply in this Session");
                                btnSave.Enabled = false;
                                return;

                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }

                        }
                        else
                        {
                            //service session wise
                            //select * from payroll_leave_session WHERE '2017-07-12' BETWEEN FDT AND TDT AND SESSION_SRNO=1
                            DataSet dsValidSession = objCommon.FillDropDown("payroll_leave_session_service", "SESSION_SRNO", "SESSION_SRNO AS A", " '" + Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd") + "' BETWEEN FDT AND TDT AND SESSION_SRNO=" + Convert.ToInt32(ViewState["SESSION_SERVICE_SRNO"]) + "", "");
                            if (dsValidSession.Tables[0].Rows.Count <= 0)
                            {
                                MessageBox("Sorry ! Not allow to apply in this Session");
                                btnSave.Enabled = false;
                                return;

                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }
                        }
                        // Added by Shrikant Bharne For Saturday working Leaves new. on 05/01/2022////////////////
                        DataSet dsdt = objApp.GetSaturdayWorkingDate(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), stno, empid);
                        double workdays = Convert.ToDouble(dsdt.Tables[0].Rows[0]["WORKINGDAYS"].ToString());
                        //Added  by Sonal Banode on 29-10-200
                        int offdays = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "ISNULL(OffDayNumber,0)AS OffDayNumber", ""));
                        //
                        //////////////////////////////////////////////////////////////////////
                        if (Convert.ToDateTime(txtFromdt.Text) == Convert.ToDateTime(txtTodt.Text))
                        {
                            // DataSet dsHoliday = objCommon.FillDropDown("payroll_holidays_vacation", "DATE", "STNO", "DATE='" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + "' and isnull(RESTRICT_STATUS,'N')='N' AND STNO IN(0," + stno + ")", "");
                            if (ViewState["LEAVE"] != null)
                            {
                                if (ViewState["LEAVE"].ToString() != "RH")
                                {
                                    DataSet dsHoliday = objCommon.FillDropDown("payroll_holidays_vacation", "DATE", "STNO", "DATE='" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + "' and isnull(RESTRICT_STATUS,'N')='N' AND STNO IN(0," + stno + ")" + "AND college_no =" + Convert.ToInt32(collegeno), "");
                                    if (dsHoliday.Tables[0].Rows.Count > 0 && workdays <= 0)
                                    {
                                        MessageBox("Sorry! Leave Not allowed on Holiday");
                                        btnSave.Enabled = false;
                                        clearcancel();
                                        return;
                                    }
                                    string dayname = Convert.ToDateTime(txtFromdt.Text).DayOfWeek.ToString();
                                    if (dayname == "Sunday" && workdays <= 0)
                                    {
                                        MessageBox("Sorry! Leave Not allowed on Sunday");
                                        btnSave.Enabled = false;
                                        return;
                                    }
                                    if (dayname == "Saturday" && workdays <= 0 && offdays == 6)
                                    {
                                        MessageBox("Sorry ! Leave not allowed on Saturday");
                                        btnSave.Enabled = false;
                                        return;
                                    }

                                    //if (dayname == "Monday" && Convert.ToBoolean(ViewState["isValidatedays"]) == true)
                                    //{
                                    //    MessageBox("Sorry ! Leave not allowed From Monday");
                                    //    btnSave.Enabled = false;
                                    //    return;
                                    //}

                                    //if (dayname == "Thursday" && Convert.ToBoolean(ViewState["isValidatedays"]) == true)
                                    //{
                                    //    MessageBox("Sorry ! Leave not allowed From Thursday");
                                    //    btnSave.Enabled = false;
                                    //    return;
                                    //}
                                    //if (dayname == "Friday" && Convert.ToBoolean(ViewState["isValidatedays"]) == true)
                                    //{
                                    //    MessageBox("Sorry ! Leave not allowed From Friday");
                                    //    btnSave.Enabled = false;
                                    //    return;
                                    //}

                                }
                                else if (ViewState["LEAVE"].ToString() == "RH")
                                {
                                    if (Convert.ToDateTime(txtFromdt.Text) == Convert.ToDateTime(txtTodt.Text))
                                    {
                                        DataSet dsHoliday1 = objCommon.FillDropDown("payroll_holidays_vacation", "DATE", "STNO", "DATE='" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + "' and isnull(RESTRICT_STATUS,'N')='Y'  AND STNO IN(0," + stno + ")" + "AND college_no =" + Convert.ToInt32(collegeno), "");
                                        if (dsHoliday1.Tables[0].Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox("This is not a Restricted Holiday!!Please Check");
                                            btnSave.Enabled = false;
                                            return;
                                        }
                                        string dayname1 = Convert.ToDateTime(txtFromdt.Text).DayOfWeek.ToString();
                                        if (dayname1 == "Sunday" && workdays <= 0)
                                        {
                                            MessageBox("Sorry! Leave Not allowed on Sunday");
                                            btnSave.Enabled = false;
                                            return;
                                        }
                                        if (dayname1 == "Saturday" && workdays <= 0 && offdays == 6)
                                        {
                                            MessageBox("Sorry! Leave Not allowed on Saturday");
                                            btnSave.Enabled = false;
                                            return;
                                        }
                                        //Added BY SHrikant Bharne  
                                        DataSet dsRH = objApp.GetRestricatedHoliday(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), stno);
                                        if (dsRH.Tables[0].Rows.Count > 0)
                                        {
                                            int valid = Convert.ToInt32(dsRH.Tables[0].Rows[0]["VALID"].ToString());

                                            if (valid == 0)
                                            {
                                                MessageBox("Please Check Restricted Holiday Date");
                                                txtTodt.Text = string.Empty;
                                            }
                                            else
                                            {

                                            }
                                        }
                                        ///////////////////////////
                                    }
                                    else
                                    {
                                        //MessageBox("This is not a Restricted Holiday!!Please Check");
                                        //btnSave.Enabled = false;
                                        //return;
                                        /// Added By Shrikant Bharne for Countinues to Take Restricated Holiday leave in countinetion if it is Restricated.
                                        DataSet dsRH = objApp.GetRestricatedHoliday(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), stno);
                                        if (dsRH.Tables[0].Rows.Count > 0)
                                        {
                                            int valid = Convert.ToInt32(dsRH.Tables[0].Rows[0]["VALID"].ToString());

                                            if (valid == 0)
                                            {
                                                MessageBox("Please Check Restricted Holiday Date");
                                                //txtTodt.Text = string.Empty;
                                                btnSave.Enabled = false;
                                                return;
                                            }
                                            else
                                            {
                                                btnSave.Enabled = true;
                                            }

                                        }
                                        else
                                        {
                                            btnSave.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            //string dayname = Convert.ToDateTime(txtFromdt.Text).DayOfWeek.ToString();
                            //if (dayname == "Monday" && Convert.ToBoolean(ViewState["isValidatedays"]) == true)
                            //{
                            //    MessageBox("Sorry ! Leave not allowed From Monday");
                            //    btnSave.Enabled = false;
                            //    return;
                            //}

                            //if (dayname == "Thursday" && Convert.ToBoolean(ViewState["isValidatedays"]) == true)
                            //{
                            //    MessageBox("Sorry ! Leave not allowed From Thursday");
                            //    btnSave.Enabled = false;
                            //    return;
                            //}
                            //if (dayname == "Friday" && Convert.ToBoolean(ViewState["isValidatedays"]) == true)
                            //{
                            //    MessageBox("Sorry ! Leave not allowed From Friday");
                            //    btnSave.Enabled = false;
                            //    return;
                            //}


                            if (ViewState["LEAVE"].ToString() == "RH")
                            {
                                /// Added By Shrikant Bharne for Countinues to Take Restricated Holiday leave in countinetion if it is Restricated.
                                DataSet dsRH = objApp.GetRestricatedHoliday(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), stno);
                                if (dsRH.Tables[0].Rows.Count > 0)
                                {
                                    int valid = Convert.ToInt32(dsRH.Tables[0].Rows[0]["VALID"].ToString());

                                    if (valid == 0)
                                    {
                                        MessageBox("Please Check Restricted Holiday Date");
                                        //txtTodt.Text = string.Empty;
                                        btnSave.Enabled = false;
                                        return;
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                    }

                                }
                                else
                                {
                                    btnSave.Enabled = true;
                                }
                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }


                        }
                        Boolean IsValidDate = Convert.ToBoolean(ViewState["isValidatedays"].ToString());
                        if (IsValidDate == true)
                        {
                            DateTime fdate = Convert.ToDateTime(txtTodt.Text);
                            int jflag = 0;
                            DateTime newdate = Convert.ToDateTime(txtTodt.Text).AddDays(1);

                            while (jflag != 1)
                            {
                                //newdate = newdate;
                                DateTime checkdate = newdate;
                                DataSet dsJ = objApp.GetIsValidLeaveDateDays(newdate, stno, collegeno);
                                if (dsJ.Tables[0].Rows.Count > 0 && dsJ.Tables[0].Rows[0]["VALID"].ToString() == "YES")
                                {
                                    //string joining = Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd");
                                    string joining = newdate.ToString("dd/MM/yyyy");
                                    fdate = Convert.ToDateTime(joining);
                                    //fdate = fdate.AddDays(1);
                                    newdate = newdate.AddDays(1);

                                    //txtFromdt.Text = fdate.ToString("dd/MM/yyyy");
                                    txtTodt.Text = newdate.ToString("dd/MM/yyyy");
                                    jflag = 0;
                                }
                                else
                                {
                                    txtTodt.Text = fdate.ToString("dd/MM/yyyy");
                                    jflag = 1;

                                }
                            }

                        }
                        //ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, stno, calholy, Convert.ToInt32(ViewState["COLLEGE_NO"]), Convert.ToInt32(ddlLeaveFNAN.SelectedValue));
                        ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, stno, calholy, Convert.ToInt32(ViewState["COLLEGE_NO"]), Convert.ToInt32(ddlLeaveFNAN.SelectedValue), empid);
                        DataSet ds2 = null;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            double totdays = Convert.ToDouble(ds.Tables[0].Rows[0]["TOT_DAYS"].ToString());
                            txtNodays.Text = totdays.ToString();
                            //=================================To Validate PD OR PDL===============================================
                            string currentdt = objCommon.LookUp("Payroll_Leave", "Top(1) CONVERT(VARCHAR(10), getdate(), 103) as currentdate", "");
                            DateTime CurrentDT = Convert.ToDateTime(currentdt);
                            DateTime FromDt = Convert.ToDateTime(from_date);
                            DateTime ToDt = Convert.ToDateTime(to_Date);
                            if (CurrentDT >= FromDt)//Post Dated Leave (PDL)
                            {
                                txtLeaveType.Text = "PDL";
                                if (ViewState["MAX_DAYS_TO_PDL"] != null)
                                {
                                    Double MAX_DAYS_TO_PDL = Convert.ToDouble(ViewState["MAX_DAYS_TO_PDL"]);
                                    if (MAX_DAYS_TO_PDL != 0)
                                    {
                                        if (totdays > MAX_DAYS_TO_PDL)
                                        {
                                            if (ViewState["MAX_DAYS_ALLOWED_TOPDL"] != null)
                                            {
                                                Double MAX_DAYS_ALLOWED_TOPDL = Convert.ToDouble(ViewState["MAX_DAYS_ALLOWED_TOPDL"]);
                                                if (MAX_DAYS_ALLOWED_TOPDL != 0)
                                                {
                                                    Double DiffCal = (CurrentDT - ToDt).TotalDays;
                                                    if (DiffCal > MAX_DAYS_ALLOWED_TOPDL)
                                                    {
                                                        //MessageBox("For EL No. of Days should be greater than or equal to Four");
                                                        MessageBox("For PDL more than " + MAX_DAYS_TO_PDL + " days, not allow to submit leave after " + MAX_DAYS_ALLOWED_TOPDL + " days.");
                                                        MessageBox("Sorry ! Maximum Days Can Apply is=> " + MAX_DAYS_TO_PDL);
                                                        btnSave.Enabled = false;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (CurrentDT < FromDt)//Pre Dated Leave (PD)
                            {
                                txtLeaveType.Text = "PD";
                                if (ViewState["MAX_DAYS_TO_PD"] != null)
                                {
                                    Double MAX_DAYS_TO_PD = Convert.ToDouble(ViewState["MAX_DAYS_TO_PD"]);
                                    if (MAX_DAYS_TO_PD != 0)
                                    {
                                        if (totdays > MAX_DAYS_TO_PD)
                                        {
                                            if (ViewState["MAX_DAYS_ALLOWED_TOPD"] != null)
                                            {
                                                Double MAX_DAYS_ALLOWED_TOPD = Convert.ToDouble(ViewState["MAX_DAYS_ALLOWED_TOPD"]);
                                                if (MAX_DAYS_ALLOWED_TOPD != 0)
                                                {
                                                    Double DiffCal = (FromDt - CurrentDT).TotalDays;
                                                    if (DiffCal < MAX_DAYS_ALLOWED_TOPD)
                                                    {
                                                        //MessageBox("For EL No. of Days should be greater than or equal to Four");
                                                        MessageBox("For Leaves more than " + MAX_DAYS_TO_PD + " days must be applied atleast " + MAX_DAYS_ALLOWED_TOPD + " days in advanced");
                                                        //MessageBox("Sorry !  Minimum Days Can Apply is=> " + MAX_DAYS_TO_PD);
                                                        btnSave.Enabled = false;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //else if (CurrentDT == FromDt)
                            //{
                            //    txtLeaveType.Text = "OL";
                            //}

                            //======================================CODE TO validate max & minimum leave to be taken=======================
                            if (ViewState["MAX_DAYS_TO_APPLY"] != null)
                            {
                                Double MAX_DAYS_TO_APPLY = Convert.ToDouble(ViewState["MAX_DAYS_TO_APPLY"]);
                                if (MAX_DAYS_TO_APPLY != 0)
                                {
                                    if (totdays > MAX_DAYS_TO_APPLY)
                                    {
                                        //MessageBox("For EL No. of Days should be greater than or equal to Four");
                                        MessageBox("Sorry ! Maximum Days Can Apply is=> " + MAX_DAYS_TO_APPLY);
                                        btnSave.Enabled = false;
                                        return;
                                    }
                                }
                            }
                            if (ViewState["MIN_DAYS_TO_APPLY"] != null)
                            {
                                Double MIN_DAYS_TO_APPLY = Convert.ToDouble(ViewState["MIN_DAYS_TO_APPLY"]);
                                if (MIN_DAYS_TO_APPLY != 0)
                                {
                                    if (totdays < MIN_DAYS_TO_APPLY)
                                    {
                                        //MessageBox("For EL No. of Days should be greater than or equal to Four");
                                        MessageBox("Sorry !  Minimum Days Can Apply is=> " + MIN_DAYS_TO_APPLY);
                                        btnSave.Enabled = false;
                                        return;
                                    }
                                }
                            }
                            //=============================================================
                            txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();

                            txtNodays.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);

                            //Added by vidisha
                            //18-09-2019
                            DateTime jdate = Convert.ToDateTime(txtJoindt.Text);
                            int jflag = 0;

                            while (jflag != 1)
                            {
                                //DataSet dsJ = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "top(1)EMPNO,APPDT,NO_OF_DAYS", "FROM_DATE,TO_DATE,JOINDT", "FROM_DATE='" + jdate.ToString("yyyy-MM-dd") + "' AND EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND status<>'D'", "");
                                DataSet dsJ = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "top(1)EMPNO,APPDT,NO_OF_DAYS", "FROM_DATE,TO_DATE,JOINDT", "FROM_DATE='" + jdate.ToString("yyyy-MM-dd") + "' AND EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND status not in ('D','P')", "");
                                if (dsJ.Tables[0].Rows.Count > 0)
                                {
                                    string joining = dsJ.Tables[0].Rows[0]["TO_DATE"].ToString();
                                    jdate = Convert.ToDateTime(joining);
                                    jdate = jdate.AddDays(1);
                                    jflag = 0;
                                }
                                else
                                {
                                    txtJoindt.Text = jdate.ToString("dd/MM/yyyy");
                                    jflag = 1;

                                }
                            }
                            //End

                            if (ViewState["LEAVENAME"] != null)
                            {
                                if (ViewState["LEAVENAME"].ToString().Trim() == "HPL".ToString().Trim())
                                {
                                    if (rdbml.SelectedValue == "0")
                                    {
                                        totdays = totdays * 2;
                                        txtNodays.Text = totdays.ToString();
                                    }
                                }
                            }

                            if (totdays == 0)
                            {
                                btnSave.Enabled = false;
                                MessageBox("Sorry! Total Days are Zero. Not Allow");
                                return;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }


                            //===================================================
                            double frmtodays = totdays;

                            Button btnApply = sender as Button;

                            string join = string.Empty;

                            objlv.JOINDT = Convert.ToDateTime(txtJoindt.Text);
                            objlv.FROMDT = Convert.ToDateTime(txtFromdt.Text); 
                            objlv.NO_DAYS = days;
                            objlv.STNO = stno;
                            objlv.COLLEGE_NO = Convert.ToInt32(ViewState["COLLEGE_NO"].ToString());
                            bool isAllowedBeforeApplication = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE", "isnull(IsAllowBeforeApplication,0) as IsAllowBeforeApplication", "LNO=" + Convert.ToInt32(Session["LNO"])));
                            objlv.IsAllowBeforeApplication = isAllowedBeforeApplication;
                            int allow_days = objApp.GetAllowDays(objlv);
                            DateTime CheckDate;
                            if (isAllowedBeforeApplication == true)
                            {
                                CheckDate = objlv.FROMDT.AddDays(-allow_days);
                            }
                            else
                            {
                                CheckDate = objlv.JOINDT.AddDays(allow_days);
                            }
                            if (today > CheckDate)//10>7
                            {
                                // not allow.
                                // MessageBox("Not Allowed ! Please check From Date Fromdate should not be more than before" + allow_days + " day of today");

                                if (days != 0)
                                {
                                    MessageBox("Not Allowed ! This application not allow to fill after " + CheckDate.ToShortDateString());
                                    btnSave.Enabled = false; //comment FOR SPECIAL CASE--18-MAY
                                }
                            }
                            Boolean IsRequiredLoad = Convert.ToBoolean(ViewState["IsRequiredLoad"]);
                            if (IsRequiredLoad == true)
                            {
                                pnlFacLoad.Visible = true;
                                
                                ShowEmployeeSchedule(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text));
                            }
                            else
                            {
                                pnlFacLoad.Visible = false;
                            }
                            //Added by Sonal Banode 10-04-2023
                            //if (ViewState["action"].ToString().Equals("add"))
                            //{
                                
                          

                            #region CL_VALIDATION1
                            //if (Convert.ToString(ViewState["LEAVE_CODE"]).Trim() == "CL".ToString().Trim() || ViewState["LEAVE_CODE"] == "cl")
                            //{
                            //    actual_balance = Convert.ToDouble(txtLeavebal.Text);
                            //    if (actual_balance > 0)
                            //    {
                            //        if (totdays > 5)
                            //        {
                            //            MessageBox("More than 5 Days not Allow");
                            //            btnSave.Enabled = false;
                            //            return;
                            //        }
                            //        if (actual_balance < totdays)
                            //        {
                            //            MessageBox("Leave Days Greater than Balance");
                            //            btnSave.Enabled = false;
                            //            return;
                            //        }

                            //    }
                            //    else
                            //    {
                            //        MessageBox("No Balance Leave");
                            //        btnSave.Enabled = false;
                            //        return;
                            //    }



                            //}
                            #endregion

                            //====================================================
                            Chkvalid();

                            if (ViewState["action"].ToString().Equals("edit") && letrno_static != 0)
                            {
                                if (Convert.ToBoolean(ViewState["IsLeaveValid"]) == true)
                                {
                                    DataSet dsValid = null;

                                    dsValid = objApp.GetLeaveValidCount(Convert.ToInt32(Session["idno"]), Convert.ToDateTime(txtFromdt.Text), Convert.ToInt32(ViewState["LNO"]), Convert.ToInt32(letrno_static));
                                    double LEAVEVALID = Convert.ToDouble(objCommon.LookUp("PAYROLL_LEAVE", "ISNULL(LEAVEVALIDMONTH,0) AS LEAVEVALIDMONTH", "LNO=" + Convert.ToInt32(ViewState["LNO"])));
                                    //Convert.ToDouble(dsValidate.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                                    //int LEAVEVALID = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "ISNULL(LEAVEVALIDMONTH,0) AS LEAVEVALIDMONTH", "LNO=" + Convert.ToInt32(ViewState["LNO"])));
                                    double CheckLEAVEVALID;

                                    if (dsValid.Tables[0].Rows.Count > 0)
                                    {
                                        double count = Convert.ToDouble(dsValid.Tables[0].Rows[0]["count"].ToString());

                                        CheckLEAVEVALID = Convert.ToDouble(count) + Convert.ToDouble(txtNodays.Text);

                                        if (CheckLEAVEVALID > LEAVEVALID)
                                        {
                                            MessageBox("You have reached the Limit for Leave of " + LEAVEVALID + " per month");
                                            btnSave.Enabled = false;
                                            return;
                                        }
                                        else
                                        {
                                            btnSave.Enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (Convert.ToBoolean(ViewState["IsLeaveValid"]) == true)
                                {
                                    DataSet dsValid = null;

                                    dsValid = objApp.GetLeaveValidCount(Convert.ToInt32(Session["idno"]), Convert.ToDateTime(txtFromdt.Text), Convert.ToInt32(ViewState["LNO"]), 0);
                                    double LEAVEVALID = Convert.ToDouble(objCommon.LookUp("PAYROLL_LEAVE", "ISNULL(LEAVEVALIDMONTH,0) AS LEAVEVALIDMONTH", "LNO=" + Convert.ToInt32(ViewState["LNO"])));
                                    double CheckLEAVEVALID;
                                    if (dsValid.Tables[0].Rows.Count > 0)
                                    {
                                        double count = Convert.ToDouble(dsValid.Tables[0].Rows[0]["count"].ToString());
                                        CheckLEAVEVALID = Convert.ToDouble(count) + Convert.ToDouble(txtNodays.Text);


                                        //if (count >= LEAVEVALID)
                                        if (CheckLEAVEVALID > LEAVEVALID)
                                        {
                                            MessageBox("You have reached the Limit for Leave of " + LEAVEVALID + " per month");
                                            btnSave.Enabled = false;
                                            return;
                                        }
                                        else
                                        {
                                            btnSave.Enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                    }
                                }
                            }

                            ///Newly Added by Shrikant B on 18-08-2023 for 
                            //===========================new code for Leave prefix and suffix==================================//
                            Boolean IsValidatedLeaveComb = Convert.ToBoolean(objCommon.LookUp("Payroll_leave_ref", "isnull(IsValidatedLeaveComb,0) as IsValidatedLeaveComb",""));

                            if (IsValidatedLeaveComb)
                            {
                                DataSet dsprefix = objCommon.FillDropDown("Payroll_leave_suffixprefix", "LVNO", "PrefixAllowed,SufixAllowed", "LVNO=" + Convert.ToInt32(ViewState["LEAVENO"]) + "", "");
                                {
                                    if (dsprefix.Tables[0].Rows.Count > 0)
                                    {
                                        string prefix = dsprefix.Tables[0].Rows[0]["PrefixAllowed"].ToString();
                                        string suffix = dsprefix.Tables[0].Rows[0]["SufixAllowed"].ToString();

                                        //string[] prefix = dsprefix.Tables[0].Rows[0]["PrefixAllowed"].ToString().Split(',');
                                        //string[] suffix = dsprefix.Tables[0].Rows[0]["SufixAllowed"].ToString().Split(',');



                                        if (txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____" && txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____")
                                        {

                                            DataSet dsprefixnew = objApp.ValidsuffixPrefix(Convert.ToInt32(Session["idno"]), Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), prefix, suffix);
                                            if (dsprefixnew.Tables[0].Rows.Count > 0)
                                            {

                                                int prefixcount = Convert.ToInt32(dsprefixnew.Tables[0].Rows[0]["PeifixCount"].ToString());
                                                int sufixcount = Convert.ToInt32(dsprefixnew.Tables[0].Rows[0]["SuffixCount"].ToString());
                                                if (prefixcount > 0)
                                                {
                                                    MessageBox("Not allow to take leave");
                                                    btnSave.Enabled = false;
                                                }
                                                else if (sufixcount > 0)
                                                {
                                                    MessageBox("Not allow to take leave");
                                                    btnSave.Enabled = false;
                                                }
                                                else
                                                {
                                                    btnSave.Enabled = true;
                                                }
                                            }
                                            else
                                            {
                                                btnSave.Enabled = true;
                                            }
                                        }

                                    }
                                }
                            }
                            //==========================end for Leave prefix and suffix========================================//
                            ///
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    /// <summary>
    /// Created by: swati ghate
    /// Created date: 08-09-2015
    /// This functio is used to check whether any leave is exists in between selected date
    /// </summary>
    /// <returns></returns>
    public int CheckLeaveExists()
    {
        Leaves objLM = new Leaves();
        int cntleaveapp;
        int ret = 0;
        string from_date, to_Date;
        int empid = Convert.ToInt32(Session["idno"]);
        from_date = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
        to_Date = Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd");

        ///////////////////////////////////////////
        objLM.FROMDT = Convert.ToDateTime(txtFromdt.Text);
        objLM.TODT = Convert.ToDateTime(txtTodt.Text);
        if (ViewState["letrno"] != null)
        {
            objLM.LETRNO = Convert.ToInt32(ViewState["letrno"]);
        }
        else
        {
            objLM.LETRNO = 0;
        }
        objLM.EMPNO = Convert.ToInt32(Session["idno"]);
        DataSet ds = objApp.CheckLeaveExists(objLM);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string IsLeaveExists = ds.Tables[0].Rows[0]["IsLeaveExists"].ToString();
            if (IsLeaveExists == "Y")
            {
                MessageBox("Leave Already Exists For Selected Date!");

                txtFromdt.Text = string.Empty;
                txtTodt.Text = string.Empty;
                txtJoindt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                btnSave.Enabled = false;
                ret = 1;
            }
            else
            {
                btnSave.Enabled = true;
                ret = 0;
            }
            return ret;

        }
        else
        {
            btnSave.Enabled = true;
            ret = 0;
        }

        return ret;
        ////////////////////////////////////////
    }
    protected void txtNodays_TextChanged(object sender, EventArgs e)
    {
        if (txtNodays.Text.ToString() != string.Empty)
        {
            Chkvalid();
        }
    }
    protected void Chkvalid()
    {
        int empno = Convert.ToInt32(Session["idno"]);
        if (ViewState["LEAVENAME"] != null)
        {
            if (ViewState["LEAVENAME"].ToString().Trim() == "HPL")
            {

                //select count(1) as TOTAL_APPLIED,EMPNO from payroll_leave_app_entry where empno=212 AND lno=3 and no_of_days=1 and status IN('P','A','T') AND 2015 BETWEEN YEAR(FROM_DATE) AND YEAR(TO_DATE) group BY EMPNO
                if (txtFromdt.Text != string.Empty && (txtNodays.Text == "1".ToString().Trim() || txtNodays.Text == "1.00".ToString().Trim()))
                {
                    //DateTime frm = Convert.ToDateTime(txtFromdt.Text);
                    string frmdate = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
                    DataSet ds = objCommon.FillDropDown("payroll_leave_app_entry", "LNO", "EMPNO", "empno=" + empno + " AND LEAVENO=3 AND NO_OF_DAYS=1 AND STATUS IN('P','A','T') AND year('" + frmdate + "') BETWEEN YEAR(FROM_DATE) AND YEAR(TO_DATE)", "EMPNO");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
                        if (count >= 3)
                        {
                            MessageBox("You have already avail 3 one day medical leave, So please upload Medical Certificate");
                            ViewState["Certificate"] = "Y".ToString().Trim();
                        }
                        else
                        {
                            ViewState["Certificate"] = null;
                        }
                    }
                    else
                    {
                        ViewState["Certificate"] = null;
                    }
                }
                else
                {
                    ViewState["Certificate"] = null;
                }
            }
        }

        //Added By Shrikant Bharne on 21-10-2022
        double CompPendingCount = 0;
        if (ViewState["IsCompOff"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCompOff"]) && ViewState["action"].ToString().Equals("add"))
            //if (Convert.ToInt32(ViewState["IsCompOff"]) == 1)
            {
                //DataSet ds = objCommon.FillDropDown("payroll_leave_app_entry", "LNO", "EMPNO", "empno=" + empno + " AND LEAVENO=3 AND NO_OF_DAYS=1 AND STATUS IN('P','A','T') AND year('" + frmdate + "') BETWEEN YEAR(FROM_DATE) AND YEAR(TO_DATE)", "EMPNO");
                DataSet ds = objCommon.FillDropDown("payroll_leave_app_entry", "NO_OF_DAYS", "", "empno=" + empno + "AND STATUS in('P','A','T') and RNO=" + Convert.ToInt32(ViewState["RNO"]), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CompPendingCount = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                }
                else
                {
                    CompPendingCount = 0;
                }
            }

        }

        ///////////////////////////////////////////////////

        double total_application_count = 0;
        double balance_leave = 0;
        double balance_validate = 0;
        balance_leave = Convert.ToDouble(txtLeavebal.Text);

        balance_validate = balance_leave;

        //if (leavename != "COMPL")
        //if (leavename != "CH")
        //if (Convert.ToInt32(ViewState["IsCompOff"]) == 0)
        if (!Convert.ToBoolean(ViewState["IsCompOff"]))
        {
            total_application_count = Pending_count + Convert.ToDouble(txtNodays.Text);
        }
        else
        {
            //total_application_count = Pending_count;
            total_application_count = CompPendingCount;
        }
        if (txtLeavebal.Text != string.Empty && txtNodays.Text != string.Empty)
        {
            if (chklvapply.Checked == true)
            {

            }
            else if (Convert.ToDouble(txtLeavebal.Text) <= 0)
            {
                objCommon.DisplayMessage("Leave Days Can not be 0 or leass than 0", this);

                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();
                btnSave.Visible = false;
            }
            else if (balance_validate < total_application_count)
            {
                MessageBox("Sorry! Total Days is more than Balance");
                //MessageBox("Total Leave application is more than balance");
                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();
                btnSave.Visible = false;
            }
            else if (balance_validate < Convert.ToDouble(txtNodays.Text))
            {
                MessageBox("Sorry! There is already Pending Leave Application");
                //MessageBox("Total Leave application is more than balance");
                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();
                btnSave.Visible = false;
                // chklvapply.Visible = true;
            }
            else if (Convert.ToDouble(txtLeavebal.Text) < Convert.ToDouble(txtNodays.Text))
            {
                objCommon.DisplayMessage("Leave Days Can not be greater than Balance Days", this);

                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();
                btnSave.Visible = false;
            }
            else if (Convert.ToBoolean(ViewState["IsCompOff"]) && balance_validate <= total_application_count)
            {
                MessageBox("Sorry! Total Days is more than Balance");
                //MessageBox("Total Leave application is more than balance");
                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();
                btnSave.Visible = false;
            }
            else
            {
                txtNodays.Focus();
                btnSave.Visible = true;
            }
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            //int idno = Convert.ToInt32(Session["idno"]);
            //int staffno =Convert.ToInt32(objCommon .LookUp ("PAYROLL_EMPMAS","STAFFNO","IDNO="+idno));
            //GetStudentIDs();
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;

            int idnorpt = Convert.ToInt32(Session["idno"]);
            //int stnorpt = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS","STNO","IDNO="+idnorpt));
            int stnorpt = Convert.ToInt32(ViewState["STNO"]);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(Session["idno"]) + "," + "@P_LNO=" + Convert.ToInt32(ViewState["LNO"].ToString()) + "," + "@P_LETRNO=" + Convert.ToInt32(ViewState["letrno"].ToString()) + "," + "@username=" + Session["userfullname"].ToString() + "," + "@P_STNO=" + stnorpt;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(Session["idno"]) + "," + "@P_LNO=" + Convert.ToInt32(ViewState["LNO"].ToString()) + "," + "@P_LETRNO=" + Convert.ToInt32(ViewState["LETRNO"].ToString()) + "," + "@username=" + Session["userfullname"].ToString() + "," + "@P_STNO=" + stnorpt + " ," + "@P_TRANNO=1" +", @P_IDNO="+ Convert.ToInt32(Session["idno"])  ;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRPT_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = objCommon.FillDropDown("PAYROLL_EMPMAS", "COLLEGE_NO,SUBDEPTNO,STNO,COLLEGE_NO,STNO,STAFFNO", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ViewState["STNO"] = ds1.Tables[0].Rows[0]["STNO"].ToString();

            }
            Button btnApply = sender as Button;
            int LNO = int.Parse(btnApply.CommandArgument);
            int letrno = int.Parse(btnApply.ToolTip.ToString());
            ViewState["letrno"] = letrno;
            ViewState["LNO"] = LNO;


            string status = objCommon.LookUp("PAYROLL_LEAVE_APP_ENTRY", "status", "LETRNO =" + letrno);

            // if (status == "A" || status == "T")
            // {
            //ShowReport("Application_form", "leave_app.rpt");
            ShowReport("Application_form", "leave_application.rpt");
            //string ds = objCommon.LookUp("PAYROLL_LEAVE_APP_PASS_ENTRY", "COUNT(*)as 'AP'", "LETRNO ='" + letrno + "' and STATUS='A'");
            //if (Convert.ToInt32(ds) > 0)
            //{

            //}
            //}
            //else
            //{
            //    MessageBox("Leave Not Approve Report not Available");
            //    return;
            //}


            // ShowReport("Application_form", "leave_app.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnApply_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    //public static bool IsDate(string strDate)
    //{
    //    DateTime outDate;
    //    return DateTime.TryParse(strDate, out outDate);
    //} 

    //protected DateTime checkday( DateTime jdt)  
    //{
    //    //string Day = Convert.ToDateTime(jdt).DayOfWeek.ToString();
    //    //if (Day.Equals("Saturday")) 
    //    //{
    //    //    jdt = jdt.AddDays(2);
    //    //}
    //    //if (Day.Equals("Sunday"))
    //    //{
    //    //    jdt = jdt.AddDays(1);
    //    //}

    //    DataSet dsh = objApp.RetrieveSingleHolydate(jdt);
    //    if (dsh.Tables[0].Rows.Count > 0)
    //    {
    //        string dbdt = dsh.Tables[0].Rows[0]["DT"].ToString();
    //        jdt = checkday(Convert.ToDateTime(dbdt).AddDays(1));
    //    }

    //    return jdt;
    //}

    //private DateTime TryParse(string myString)
    //{

    //    DateTime myDateTime = DateTime.MinValue;

    //    if (null != myString && 0 < myString.Length)
    //    {
    //        try
    //        {

    //            myDateTime = Convert.ToDateTime(myString);
    //        }
    //        catch (FormatException)
    //        {

    //        }
    //    }
    //    return myDateTime;
    //}

    //private bool IsDateTime(string myString)
    //{

    //    DateTime myDateTime = TryParse(myString);

    //    return (DateTime.MinValue != myDateTime);
    //}

    protected void btnreport_Click(object sender, EventArgs e)
    {
        //ShowReport("Application_Form", "leave_app.rpt");

        ShowReport("Application_Form", "leave_application.rpt");
    }


    //protected void btnLeaveRPT_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Button btnApply = sender as Button;   
    //        int LNO = int.Parse(btnApply.CommandArgument);
    //        int letrno = int.Parse(btnApply.ToolTip.ToString());
    //        ViewState["LETRNO"] = letrno;
    //        ViewState["LNO"] = LNO;
    //        ShowReport("Application_form", "JoinAfterLeaveReport.rpt");

    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnLeaveRPT_Click ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");

    //    }
    //}


    protected void rblleavetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblleavetype.SelectedValue == "1")//HALF DAY
        {
            ddlLeaveFNAN.Enabled = true;
            txtFromdt.Text = string.Empty;
            txtTodt.Text = string.Empty;
            txtJoindt.Text = string.Empty;
            txtNodays.Text = string.Empty;
            txtLeaveType.Text = string.Empty;
            trhalfDay.Visible = true;
            trhalfjoinDay.Visible = true;

        }
        else
        {
            //ddlLeaveFNAN.Visible = false;
            txtFromdt.Text = string.Empty;
            txtTodt.Text = string.Empty;
            txtJoindt.Text = string.Empty;
            txtNodays.Text = string.Empty;
            btnSave.Enabled = true;
            ceFromdt.Enabled = CeTodt.Enabled = true;
            txtFromdt.Enabled = txtTodt.Enabled = true;
            trhalfDay.Visible = false;
            ddlLeaveFNAN.Enabled = false;
            trhalfjoinDay.Visible = false;
        }
    }
    protected void ddlLeaveFNAN_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLeaveFNAN.SelectedValue == "1")
        {
            txtJoindt.Text = txtTodt.Text;
            ddlNoon.SelectedValue = "1";
        }

        //if (ddlLeaveFNAN.SelectedValue == "0")
        //{
        //    ddlNoon.SelectedValue = "0";
        //}
        else if (ddlLeaveFNAN.SelectedValue == "0")
        {
            txtJoindt.Text = txtFromdt.Text;
            ddlNoon.SelectedValue = "0";
        }
        else
        {
            if (txtTodt.Text != string.Empty)
            {

                DateTime joindate;
                joindate = Convert.ToDateTime(txtTodt.Text);

                txtJoindt.Text = joindate.AddDays(1).ToString();
                ddlNoon.SelectedValue = "0";
            }
            else
            {
                MessageBox("Please Select From Date and To Date");
            }

        }

    }
    protected void rdbml_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        txtJoindt.Text = string.Empty;
        txtNodays.Text = string.Empty;

    }


    protected void rblIsCertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblIsCertificate.SelectedValue == "0")
        {
            divCer.Visible = true;
        }
        else
        {
            divCer.Visible = false;
        }
    }
    protected void lnkRestrictedLeaves_Click(object sender, EventArgs e)
    {
        BindListViewRestrictedHolidaysLeaveapplStatus();
        pnlLeaveStatus.Visible = false;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        lnkbut.Visible = false;
        pnlLeaveRestrictHolidays.Visible = true;
        lnkRestrictedLeaves.Visible = false;
    }


    public int CheckExistComoff()
    {

        int ret = 0;
        int empid = Convert.ToInt32(Session["idno"]);
        //int maternity_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "MATER_NO", "ISNULL(MATER_NO,0)>=0"));
        //int paternity_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "PATER_NO", "ISNULL(PATER_NO,0)>=0"));
        int RNO = Convert.ToInt32(ViewState["RNO"]);
        //MAX_SERVICE_COMPLETE
        //DataSet dsValidate = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "SUM(NO_OF_DAYS) AS NO_OF_DAYS ", "EMPNO", "STATUS IN('A','P') AND LNO=" + Convert.ToInt32(Session["lno"]) + " AND RNO=" + Convert.ToInt32(ViewState["RNO"]) + " AND EMPNO=" + empidno + " GROUP BY EMPNO", "");
        DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "LEAVENO", "EMPNO", "EMPNO=" + empid + " AND STATUS NOT IN('R','D') AND RNO=" + RNO + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
            //SERVICE_LIMIT ,MAX_SERVICE_COMPLETE
            if (count > 0)
            {
                MessageBox("Leave Already Applied against this Comp off");
                lnkbut.Visible = true;
                ret = 1;
            }
        }

        return ret;
    }


    private void clearcancel()
    {
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        txtNodays.Text = string.Empty;
        txtJoindt.Text = string.Empty;
        txtReason.Text = string.Empty;
        txtadd.Text = string.Empty;
        txtcharge.Text = string.Empty;
    }

    private void FillEmployees()
    {
        int idno = Convert.ToInt32(Session["idno"]);
        objCommon.FillDropDownList(ddlChargeHanded, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME", "P.PSTATUS='Y' AND E.IDNO!=" + idno + "", "FNAME");

    }



    private bool CheckDuplicateCHName(DataTable dt, string value1, string value2, string value3, string value4, string value5, string value6)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DATE"].ToString() == value1 && dr["TIME"].ToString() == value2 && dr["YEAR_SEM"].ToString() == value3 && dr["SUBJECT"].ToString() == value4 && dr["FACULTY_NAME"].ToString() == value5 && dr["FACULTY"].ToString() == value6)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return retVal;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dteng = (DataTable)ViewState["dteng"];
        //if (ddlEmp.SelectedIndex == 0)
        if (txtTime.Text == string.Empty || txtyear.Text == string.Empty || txtsubject.Text == string.Empty || txtEngagedDate.Text == string.Empty || ddlChargeHanded.SelectedValue == string.Empty)
        {
            MessageBox("Please Fill All Class Arrangement Information");
            return;
        }
        else if (txtTime.Text != string.Empty && txtyear.Text != string.Empty && txtsubject.Text != string.Empty && txtEngagedDate.Text != string.Empty && ddlChargeHanded.SelectedValue != string.Empty)
        {
            //dteng = (DataTable)ViewState["dteng"];

            //if (CheckDuplicateCHName(dteng, ddlChargeHanded.SelectedItem.Text, txtEngagedDate.Text, txtsubject.Text, txtTime.Text, txtyear.Text,ddlChargeHanded.SelectedValue.ToString()))
            //{
            //    lvEngagedInfo.DataSource = dteng;
            //    lvEngagedInfo.DataBind();
            //    MessageBox("Record Already Exist!");
            //    return;
            //}


            if (txtFromdt.Text != string.Empty && txtTodt.Text != string.Empty)
            {
                DateTime frmdt = Convert.ToDateTime(txtFromdt.Text);
                DateTime todt = Convert.ToDateTime(txtTodt.Text);
                objLM.FROMDT = frmdt;
                objLM.TODT = todt;
                DateTime lecdt = Convert.ToDateTime(txtEngagedDate.Text);
                if (lecdt >= frmdt && lecdt <= todt)
                {
                    objLM.ENGAGED_DATE = txtEngagedDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtEngagedDate.Text.Trim());
                    if (txtTime.Text != string.Empty)
                    {
                        objLM.TIME = txtTime.Text;
                    }
                    else
                    {
                        objLM.TIME = string.Empty;
                    }
                    objLM.YEARSEM = txtyear.Text;
                    objLM.SUBJECT = txtsubject.Text;
                    objLM.FACULTYNO = Convert.ToInt32(ddlChargeHanded.SelectedValue);
                    //objLM.FACULTY_NAME = ddlEmp.SelectedItem.Text.ToString();     
                    objLM.FACULTY_NAME = Convert.ToString(ddlChargeHanded.SelectedItem);
                    //string DESIGNATION = txtdesig.Text.Trim();
                    objLM.TRANNO = 0;
                    objLM.EMPNO = Convert.ToInt32(Session["idno"]);
                    objLM.LNO = Convert.ToInt32(Session["lno"]);
                    objLM.LNO = Convert.ToInt32(ViewState["LNO"]);
                    DataRow dr = dteng.NewRow();

                    dr["DATE"] = Convert.ToDateTime(objLM.ENGAGED_DATE).ToString("yyyy/MM/dd");
                    dr["TIME"] = objLM.TIME;
                    dr["YEAR_SEM"] = objLM.YEARSEM;
                    dr["SUBJECT"] = objLM.SUBJECT;
                    dr["FACULTY_NAME"] = objLM.FACULTY_NAME;
                    dr["IDNO"] = objLM.EMPNO;
                    dr["LNO"] = objLM.LNO;
                    dr["FACULTY"] = objLM.FACULTYNO;
                    dteng.Columns["SEQNO"].AutoIncrementStep = 1;
                    //dr["DESIGNATION"] = DESIGNATION;
                    dteng.Rows.Add(dr);
                    dteng.AcceptChanges();
                    lvEngagedInfo.DataSource = dteng;
                    lvEngagedInfo.DataBind();
                    txtEngagedDate.Text = txtTime.Text = txtsubject.Text = txtyear.Text = string.Empty;
                    // txtdesig.Text = txtEmp.Text = string.Empty;
                    ddlChargeHanded.SelectedIndex = 0;
                    PnlAddEngaged.Visible = true;
                }
                else
                {
                    MessageBox(" Lecture Date must be in between From date & to date");
                    return;
                }
            }
            else
            {
                MessageBox("Please Enter Leave Details!");
                return;
            }
        }
        else
        {
            MessageBox("Please Fill Class Arrangement Information");
            return;
        }
    }
    protected void btnEditEng_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            string[] arg = new string[4];
            arg = btnEdit.CommandArgument.ToString().Split(';');
            string date = arg[0];
            string time = arg[1];
            string year = arg[2];
            string subject = arg[3];
            string FacultyName = arg[4];
            //string designation = arg[5];
            txtEngagedDate.Text = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            txtTime.Text = time;
            txtyear.Text = year;
            txtsubject.Text = subject;
            //ddlEmp.SelectedValue = FacultyName;
            //txtEmp.Text = FacultyName;
            ddlChargeHanded.SelectedValue = FacultyName;
            //txtdesig.Text = designation;
            dteng = (DataTable)ViewState["dteng"];
            for (int i = dteng.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dteng.Rows[i];
                if (dr["DATE"].ToString() == date && dr["TIME"].ToString() == txtTime.Text && dr["YEAR_SEM"].ToString() == txtyear.Text && dr["SUBJECT"].ToString() == txtsubject.Text && dr["FACULTY_NAME"].ToString() == ddlChargeHanded.SelectedValue)
                {
                    dr.Delete();
                }
            }
            dteng.AcceptChanges();
            if (dteng.Rows.Count > 0)
            {
                lvEngagedInfo.DataSource = dteng;
                lvEngagedInfo.DataBind();
            }
            else
            {
                lvEngagedInfo.DataSource = null;
                lvEngagedInfo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnEdit_click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDeleteLecture_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["dteng"] != null && ((DataTable)ViewState["dteng"]) != null)
            {
                DataTable dtM = (DataTable)ViewState["dteng"];
                dtM.Rows.Remove(this.GetEditableDatarow(dtM, btnDelete.CommandArgument));
                ViewState["dteng"] = dtM;
                if (dtM.Rows.Count > 0)
                {
                    lvEngagedInfo.DataSource = dtM;
                    lvEngagedInfo.DataBind();
                }
                else
                {
                    lvEngagedInfo.DataSource = null;
                    lvEngagedInfo.DataBind();
                }
                //lvEngagedInfo.DataSource = dtM;
                //lvEngagedInfo.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarow(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["SEQNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void txtFromdt_TextChanged(object sender, EventArgs e)
    {
        Boolean IsValidDate = Convert.ToBoolean(ViewState["isValidatedays"].ToString());
        if (IsValidDate == true)
        {
            int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "college_no", "IDNO=" + Convert.ToInt32(Session["idno"])));

            DateTime fdate = Convert.ToDateTime(txtFromdt.Text);
            int jflag = 0;
            DateTime newdate = Convert.ToDateTime(txtFromdt.Text).AddDays(-1);

            while (jflag != 1)
            {
                //newdate = newdate;
                DateTime checkdate = newdate;
                DataSet dsJ = objApp.GetIsValidLeaveDateDays(newdate, stno, collegeno);
                if (dsJ.Tables[0].Rows.Count > 0 && dsJ.Tables[0].Rows[0]["VALID"].ToString() == "YES")
                {
                    string joining = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
                    fdate = Convert.ToDateTime(joining);
                    //fdate = fdate.AddDays(-1);
                    newdate = newdate.AddDays(-1);

                    //txtFromdt.Text = fdate.ToString("dd/MM/yyyy");
                    txtFromdt.Text = newdate.ToString("dd/MM/yyyy");
                    jflag = 0;
                }
                else
                {
                    txtFromdt.Text = fdate.ToString("dd/MM/yyyy");
                    jflag = 1;

                }
            }
        }
        else
        {
            txtTodt.Text = string.Empty;
            txtNodays.Text = string.Empty;
            txtJoindt.Text = string.Empty;
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    //Added by Sonal Banode on 29-03-2023 
    protected void ShowEmployeeSchedule(DateTime FromDate, DateTime ToDate)
    {
        DataSet ds = null;
        try
        {
            ds = objApp.GetEmployeeScheduleList(Convert.ToInt32(Session["userno"]), FromDate, ToDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFacLoad.DataSource = ds.Tables[0];
                lvFacLoad.DataBind();
                lvFacLoad.Visible = true;
                pnlFacLoad.Visible = true;
            }
            else
            {
                lvFacLoad.DataSource = null;
                lvFacLoad.DataBind();
                lvFacLoad.Visible = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    protected void lvFacLoad_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        for (int i = 0; i <= lvFacLoad.Items.Count; i++)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlFac = (DropDownList)e.Item.FindControl("ddlFac");
                //HiddenField hdnidno = (HiddenField)e.Item.FindControl("hdnidno");
                objCommon.FillDropDownList(ddlFac, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO as FACULTY", "ISNULL(E.TITLE,'' ) + ' ' + isnull(E.FNAME,'') + ' ' + isnull(E.MNAME,'') + ' ' + isnull(E.LNAME,'') AS EMPNAME", "E.STNO=" + Convert.ToInt32(ViewState["STNO"]) + " AND E.IDNO!=" + Convert.ToInt32(Session["idno"]) + " AND P.PSTATUS='Y' ", "E.IDNO");
                //ddlFac.SelectedValue = hdnidno.Value; 
            }
        }
    }

    protected DataTable setGridDatatSetLoad(DataTable dt, string tabName)
    {
        dt.TableName.Equals(tabName);
        dtLoad.Columns.Add("TIMETABLE_DATE");
        dtLoad.Columns.Add("CLASS");
        dtLoad.Columns.Add("COURSE_NAME");
        dtLoad.Columns.Add("SLOTNAME");
        dtLoad.Columns.Add("FACULTY");
        dtLoad.Columns.Add("SRNO");
        dtLoad.Columns["SRNO"].AutoIncrement = true; dtLoad.Columns["SRNO"].AutoIncrementSeed = 1; dtLoad.Columns["SRNO"].AutoIncrementStep = 1;
        return dt;
    }

    protected void SaveLoadTable()
    {
        try
        {
            int maxVal = 0;
            dtLoad = (DataTable)ViewState["dtLoad"];
            dtLoad.Clear();
            Leaves objLeave = new Leaves();

            foreach (ListViewDataItem lvItem in lvFacLoad.Items)
            {
                DataRow dr = dtLoad.NewRow();
                if (dr != null)
                {
                    maxVal = Convert.ToInt32(dtLoad.AsEnumerable().Max(row => row["SRNO"]));
                }
                Label lbldate = lvItem.FindControl("lbldate") as Label;
                Label lblClass = lvItem.FindControl("lblClass") as Label;
                Label lblCourseName = lvItem.FindControl("lblCourseName") as Label;
                Label lblSlotName = lvItem.FindControl("lblSlotName") as Label;
                DropDownList ddlFac = lvItem.FindControl("ddlFac") as DropDownList;

                objLeave.DATE = Convert.ToDateTime(lbldate.Text);
                objLeave.CLASS = lblClass.Text;
                objLeave.COURSENAME = lblCourseName.Text;
                objLeave.SLOTNAME = lblSlotName.Text.ToString();
                if (ddlFac.SelectedIndex == 0)
                {
                    MessageBox("Please Select Faculty Name.");
                    return;
                    //objLeave.FACNO = 0;
                }
                else
                {
                    objLeave.FACNO = Convert.ToInt32(ddlFac.SelectedValue);
                }
                //DataRow dr = dtLoad.NewRow();
                dr["TIMETABLE_DATE"] = Convert.ToDateTime(objLeave.DATE).ToString("yyyy/MM/dd");
                dr["CLASS"] = objLeave.CLASS;
                dr["COURSE_NAME"] = objLeave.COURSENAME;
                dr["SLOTNAME"] = objLeave.SLOTNAME;
                dr["FACULTY"] = objLeave.FACNO;
                dr["SRNO"] = maxVal + 1;
                //dtLoad.Columns["SRNO"].AutoIncrementStep = 1;
                dtLoad.Rows.Add(dr);
                dtLoad.AcceptChanges();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
        }

        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    //--- Added By Shrikant Bharne on 24-04-2023 for Crescent LWP Leave Validation
    public int CheckLWPApply(int YEAR)
    {

        int ret = 0;
        int empid = Convert.ToInt32(Session["idno"]);
        double Balance=0;
        string LeaveName="";       
        int leaveno = Convert.ToInt32(ViewState["LEAVENO"]);
        DataSet ds = objApp.GetLeavesStatusForLWPApply(empid, YEAR, 0);
        if (ds.Tables[0].Rows.Count > 0)
        {           

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                Balance = Convert.ToDouble(ds.Tables[0].Rows[i]["bal"].ToString());
                LeaveName = ds.Tables[0].Rows[i]["LeaveName"].ToString();

                if (Balance > 0)
                {
                    MessageBox("You are not allow to Apply LWP/Loss of Pay leave as leave Balance available for leave " + LeaveName);
                    ret = 1;
                }
            }

            
        }       
        return ret;
    }


    public void SendChargeHandoverMail(int letrno)
    {
        try
        {
            string body = string.Empty;
            objLM.LETRNO = letrno;
            dteng = (DataTable)ViewState["dteng"];
            DataSet ds = objApp.GetChargeEmailInformation(objLM);
            //foreach (DataTable table in ds.Tables)
            //{

            //    foreach (DataRow dr in table.Rows)
            //    {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string fromEmailId = ds.Tables[0].Rows[0]["SenderEmailId"].ToString();
                string fromEmailPwd = ds.Tables[0].Rows[0]["SenderEmailPassword"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                    {
                        string Name = ds.Tables[1].Rows[i]["NAME"].ToString();
                        string ChargePersonName = ds.Tables[1].Rows[i]["ChargePerson_NAME"].ToString();
                        //AUTHORITY_NAME
                        string FromDate = ds.Tables[1].Rows[i]["From_Date"].ToString();
                        string ToDate = ds.Tables[1].Rows[i]["To_Date"].ToString();
                        string ChargeDate = ds.Tables[1].Rows[i]["ChargeDate"].ToString();

                        string employeename = ds.Tables[1].Rows[i]["NAME"].ToString();
                        string department = ds.Tables[1].Rows[i]["department"].ToString();
                        string Joindt = ds.Tables[1].Rows[i]["Joindt"].ToString();
                        string toEmailId = ds.Tables[1].Rows[i]["EMAILID"].ToString();
                        string Sub = "Notification for Charge Notification";
                       

                        string leavename = ds.Tables[1].Rows[i]["Leave_Name"].ToString();

                        //Applicant Name of department has applied leave leavename for Total No of days from date to todate on application date

                        body = "Dear Sir/Mam " + Environment.NewLine + ChargePersonName + "," + Environment.NewLine + Environment.NewLine + employeename + " of Department " + department + " " + "has applied " + leavename + " from " + FromDate
                                   + " " + "to " + ToDate + " on " + DateTime.Now.ToString("dd/MM/yyyy") + "  " + Environment.NewLine + ""
                                   + "and you have been assigned charge for date " + ChargeDate + "." + Environment.NewLine + Environment.NewLine + "Thanks and Regards" + Environment.NewLine + employeename;

                        string ToEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                        SendEmailCommon objSendEmail = new SendEmailCommon(); 
                        int status = 0;
                        try
                        {
                            //status = SendMailBYSendgrid(message, emailid, subject);
                            //status = sendEmail(message, emailid, subject);
                            //Task<int> task = Execute(body, toEmailId, Sub);
                            //status = task.Result;

                            status = objSendEmail.SendEmail(toEmailId, body, Sub);
                        }
                        catch (Exception ex3)
                        {
                            //lbl3.Text = "lbl3  " + "email status " + status + " " + ex3.ToString();
                        }
                    }
                }
            }
            //    }
            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

      
}

