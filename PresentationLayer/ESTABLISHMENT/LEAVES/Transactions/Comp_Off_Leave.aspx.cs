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
using System.Globalization;


public partial class ESTABLISHMENT_LEAVES_Transactions_Comp_Off_Leave : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Leaves objLM = new Leaves();
    LeavesController objLC = new LeavesController();
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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();


                BindCompOffListView();
                CheckPageAuthorization();
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Comp_Off_Leave.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Comp_Off_Leave.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
          
            DateTime EXPIRY_DATE = Convert.ToDateTime(ViewState["EXPIRY_DATE"]);
            double NO_OF_DAYS = Convert.ToDouble(ViewState["NO_OF_DAYS"]);
            int idno = Convert.ToInt32(Session["idno"]);
            string INTIME = ViewState["INTIME"].ToString();
            string OUTTIME = ViewState["OUTTIME"].ToString();
            string WORKHR = ViewState["WORKHR"].ToString();
          
            //CustomStatus cs = (CustomStatus)objLC.AddCompOff(idno, Convert.ToDateTime(txtWDate.Text), EXPIRY_DATE, txtReason.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), INTIME, OUTTIME, WORKHR,'P');
            CustomStatus cs = (CustomStatus)objLC.AddCompOff(idno, Convert.ToDateTime(txtWDate.Text), EXPIRY_DATE, txtReason.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), INTIME, OUTTIME, WORKHR, 'P',1);
           
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Record Saved Successfully...", this.Page);
                BindCompOffListView();
                Clear();
            }
            /*
             int idno = Convert.ToInt32(Session["idno"]);
            int COMPOFF_MIN_HR_FULLDAY = Convert.ToInt32(objCommon.LookUp("payroll_leave_ref", "isnull(COMPOFF_MIN_HR_FULLDAY,0) as COMPOFF_MIN_HR_FULLDAY", ""));
            if (COMPOFF_MIN_HR_FULLDAY == 0)
            {
                COMPOFF_MIN_HR_FULLDAY = 6;
            }
            int Hour = 0;
            string timeDiff;
            DateTime WDate = Convert.ToDateTime(txtWDate.Text);
            DateTime Nextmonth = WDate.AddMonths(+1);
            DataSet dsOD = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "STATUS", "EMPNO", "EMPNO=" + Convert.ToInt32(Session["idno"]) + " and STATUS IN('A','T') AND '" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "' between from_date and to_date ", "");
            DataSet DS = objCommon.FillDropDown("EMP_BIOATTENDANCE_LOG ", "CONVERT(NVARCHAR(30),LogTime,108)AS TIME", "FunctionNo", "USERID=(SELECT RFIDNO FROM PAYROLL_EMPMAS WHERE IDNO=" + Session["idno"] + ") AND CONVERT(NVARCHAR(30),LogTime,103) BETWEEN CONVERT(NVARCHAR(30),'" + txtWDate.Text + "',103) AND CONVERT(NVARCHAR(30),'" + txtWDate.Text + "',103)", "");
            if (DS.Tables[0].Rows.Count > 0)
            {
              
                int rowCount = DS.Tables[0].Rows.Count;
                if (rowCount != 2)
                {
                   // DataSet dsOD = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "STATUS", "EMPNO", "EMPNO=" + Convert.ToInt32(Session["idno"]) + " and STATUS IN('A','T') AND '" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "' between from_date and to_date ", "");
                    if (dsOD.Tables[0].Rows.Count > 0)
                    {
                        string InTime = DS.Tables[0].Rows[0]["TIME"].ToString();
                        string OutTime = DS.Tables[0].Rows[1]["TIME"].ToString();

                        DateTime Current_Date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        DateTime dFrom;
                        DateTime dTo;
                        string sDateFrom = InTime;
                        string sDateTo = OutTime;
                        if (DateTime.TryParse(sDateFrom, out dFrom) && DateTime.TryParse(sDateTo, out dTo))
                        {
                            TimeSpan TS = dTo - dFrom;
                            int hour = TS.Hours;
                            int mins = TS.Minutes;
                            int secs = TS.Seconds;
                            timeDiff = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");
                            string[] hour_min_sec = timeDiff.Split(':');
                            Hour = Convert.ToInt32(hour_min_sec[0].ToString());
                        }
                        if (Hour >= COMPOFF_MIN_HR_FULLDAY)
                        {
                            CustomStatus cs = (CustomStatus)objLeave.AddCompOff(idno, Convert.ToDateTime(txtWDate.Text), Nextmonth, txtReason.Text, Current_Date, InTime, OutTime, Hour, 'P');
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage("Record Saved Successfully...", this.Page);
                                BindCompOffListView();
                                Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Working Hour Should Be Minimum " + COMPOFF_MIN_HR_FULLDAY + " hour...", this.Page);
                        }

                    }
                    else
                    {
                        string FunctionNo = DS.Tables[0].Rows[0]["FunctionNo"].ToString();
                        if (FunctionNo == "IN")
                        {
                            objCommon.DisplayMessage("OUT Time Not Exists!", this.Page);
                            return;
                        }
                        else
                        {
                            objCommon.DisplayMessage("IN Time Not Exists!", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    string InTime = DS.Tables[0].Rows[0]["TIME"].ToString();
                    string OutTime = DS.Tables[0].Rows[1]["TIME"].ToString();

                    DateTime Current_Date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                    DateTime dFrom;
                    DateTime dTo;
                    string sDateFrom = InTime;
                    string sDateTo = OutTime;
                    if (DateTime.TryParse(sDateFrom, out dFrom) && DateTime.TryParse(sDateTo, out dTo))
                    {
                        TimeSpan TS = dTo - dFrom;
                        int hour = TS.Hours;
                        int mins = TS.Minutes;
                        int secs = TS.Seconds;
                        timeDiff = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");
                        string[] hour_min_sec = timeDiff.Split(':');
                        Hour = Convert.ToInt32(hour_min_sec[0].ToString());
                    }
                    if (Hour >= COMPOFF_MIN_HR_FULLDAY)
                    {
                        CustomStatus cs = (CustomStatus)objLeave.AddCompOff(idno, Convert.ToDateTime(txtWDate.Text), Nextmonth, txtReason.Text, Current_Date, InTime, OutTime, Hour, 'P');
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Record Saved Successfully...", this.Page);
                            BindCompOffListView();
                            Clear();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Working Hour Should Be Minimum " + COMPOFF_MIN_HR_FULLDAY + " hour...", this.Page);
                    }
                }
                //==============

            }//DS end            
            else if (dsOD.Tables[0].Rows.Count > 0)
            {
                string InTime = DS.Tables[0].Rows[0]["TIME"].ToString();
                string OutTime = DS.Tables[0].Rows[1]["TIME"].ToString();

                DateTime Current_Date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                DateTime dFrom;
                DateTime dTo;
                string sDateFrom = InTime;
                string sDateTo = OutTime;
                if (DateTime.TryParse(sDateFrom, out dFrom) && DateTime.TryParse(sDateTo, out dTo))
                {
                    TimeSpan TS = dTo - dFrom;
                    int hour = TS.Hours;
                    int mins = TS.Minutes;
                    int secs = TS.Seconds;
                    timeDiff = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");
                    string[] hour_min_sec = timeDiff.Split(':');
                    Hour = Convert.ToInt32(hour_min_sec[0].ToString());
                }
                if (Hour >= COMPOFF_MIN_HR_FULLDAY)
                {
                    CustomStatus cs = (CustomStatus)objLeave.AddCompOff(idno, Convert.ToDateTime(txtWDate.Text), Nextmonth, txtReason.Text, Current_Date, InTime, OutTime, Hour, 'P');
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully...", this.Page);
                        BindCompOffListView();
                        Clear();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Working Hour Should Be Minimum " + COMPOFF_MIN_HR_FULLDAY + " hour...", this.Page);
                }

            }
            else
            {
                objCommon.DisplayMessage("No Punch/OD Record Found For Selected Date...", this.Page);
                btnSave.Visible = false;
            }
           */

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transaction_Comp_Off_Leave.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        //}
        //else
        //{
        //    objCommon.DisplayMessage("You are not Eligible for Compensatory Leave...", this.Page);
        //}


    }
    private void Clear()
    {
        txtWDate.Text = string.Empty;
        txtReason.Text = string.Empty;
        btnSave.Visible = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void BindCompOffListView()
    {
        try
        {
            DataSet ds = objLC.RetrieveAllCompOff(Convert.ToInt32(Session["Idno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                RptCompoff.DataSource = ds.Tables[0];
                RptCompoff.DataBind();
                DivNote.Visible = false;
                PanelList.Visible = RptCompoff.Visible = true;
            }
            else
            {
                DivNote.Visible = true;
                PanelList.Visible = RptCompoff.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transaction_Comp_Off_Leave.BindCompOffListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void txtWDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime DtFrom, DtTo,Test;
            if (DateTime.TryParseExact(txtWDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
            {
                bool IsValid = false; string message = string.Empty;
                int idno = Convert.ToInt32(Session["idno"]);
                int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno + ""));
                int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "isnull(WORKING_COLLEGE_NO,0) as WORKING_COLLEGE_NO", "IDNO=" + idno + ""));
                Boolean IS_SHIFT_MANAGMENT = Convert.ToBoolean(objCommon.LookUp("PAYROLL_EMPMAS", "isnull(IS_SHIFT_MANAGMENT,0)as IS_SHIFT_MANAGMENT", "IDNO=" + idno + ""));

                //if (IS_SHIFT_MANAGMENT == false)
                //{
                //    DataSet DsValid = objCommon.FillDropDown("PAYROLL_HOLIDAYS_VACATION", "case when count(1)>0 then '1' else'0' end as Valid", "''as test", "(DATE='" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "' AND STNO IN(0," + stno + ") ) OR DATENAME(DW,'" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "')='Sunday'", "");
                //    if (DsValid.Tables[0].Rows.Count > 0)
                //    {
                //        string valid = Convert.ToString(DsValid.Tables[0].Rows[0]["Valid"]);
                //        if (valid == "0")
                //        {
                //            objCommon.DisplayMessage("Not allow to Apply. Holiday/Sunday Not found", this.Page);
                //            btnSave.Visible = false;
                //            return;
                //        }
                //    }
                //}
                //else
                //{
                //    btnSave.Visible = true;
                //}
                DateTime WDate = Convert.ToDateTime(txtWDate.Text);
                #region withshiftmanagement
                // added by Shrikant Bharne on 13/01/2023
                if (IS_SHIFT_MANAGMENT == false)
                {
                    DataSet DsValid = objCommon.FillDropDown("PAYROLL_HOLIDAYS_VACATION", "case when count(1)>0 then '1' else'0' end as Valid", "''as test", "(DATE='" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "' AND STNO IN(0," + stno + ") AND COLLEGE_NO=" + collegeno + ") OR DATENAME(DW,'" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "')='Sunday'", "");
                    if (DsValid.Tables[0].Rows.Count > 0)
                    {
                        string valid = Convert.ToString(DsValid.Tables[0].Rows[0]["Valid"]);
                        if (valid == "0")
                        {
                            objCommon.DisplayMessage("Not allow to Apply. Holiday/Sunday Not found", this.Page);
                            btnSave.Visible = false;
                            return;
                        }
                    }
                }
                else if (IS_SHIFT_MANAGMENT == true)
                {
                    DataSet DsValid = objCommon.FillDropDown("PAYROLL_HOLIDAYS_VACATION", "case when count(1)>0 then '1' else'0' end as Valid", "''as test", "(DATE='" + Convert.ToDateTime(txtWDate.Text).ToString("yyyy-MM-dd") + "' AND STNO IN(0," + stno + ") AND COLLEGE_NO=" + collegeno + ") ", "");
                    if (DsValid.Tables[0].Rows.Count > 0)
                    {
                        string valid = Convert.ToString(DsValid.Tables[0].Rows[0]["Valid"]);
                        if (valid == "1")
                        {
                            //Holiday Found...Now to chack Alloted Shift other than 0th timing shift                        
                            DataSet DsShift = objCommon.FillDropDown("[dbo].[PAYROLL_SHIFTMANAGEMENT] MGT INNER JOIN payroll_shiftmaster SH ON(MGT.SHIFTNO=SH.SHIFTNO)", "ISNULL(SH.ISLEAVE,0)as IsLeave", "left(convert(time,SH.INTIME),8) AS SH_INTIME,left(convert(time,SH.OUTTIME),8) AS SH_OUTTIME", "MGT.ISACTIVE=1 AND MGT.EMPLOYEEIDNO=" + Convert.ToInt32(Session["idno"]) + " and convert(date,MGT.shiftdate)='" + Convert.ToDateTime(WDate).ToString("yyyy-MM-dd") + "'", "");
                            if (DsShift.Tables[0].Rows.Count > 0)
                            {
                                if (DsShift.Tables[0].Rows[0]["SH_INTIME"].ToString() == "00:00:00" && DsShift.Tables[0].Rows[0]["SH_OUTTIME"].ToString() == "00:00:00")
                                {
                                    objCommon.DisplayMessage("During Holiday, Working Shift Not Assigned. Contact To Shift Incharge", this.Page);
                                    btnSave.Visible = false;
                                    return;
                                }
                            }
                            else
                            {
                                //Holiday not found & SHift not assigned
                                objCommon.DisplayMessage("Shift Not Assigned. Contact To Shift Incharge", this.Page);
                                btnSave.Visible = false;
                                return;
                            }
                        }
                        else if (valid == "0")//Holiday Not FOund
                        {
                            //Holiday Not FOund
                            //objCommon.DisplayMessage("Not allow to Apply(Holiday/Comp-off related shift Not Found)", this.Page);
                            //return;

                            //Note:1. TO check Shift

                            DataSet DsShift = objCommon.FillDropDown("[dbo].[PAYROLL_SHIFTMANAGEMENT] MGT INNER JOIN payroll_shiftmaster SH ON(MGT.SHIFTNO=SH.SHIFTNO)", "left(convert(time,SH.INTIME),8)AS SH_INTIME,ISNULL(SH.ISLEAVE,0)as IsLeave,ISNULL(MGT.IsDayOff,0)as IsDayOff", "MGT.EMPLOYEEIDNO", "MGT.ISACTIVE=1 AND MGT.EMPLOYEEIDNO=" + Convert.ToInt32(Session["idno"]) + " and convert(date,MGT.shiftdate)='" + Convert.ToDateTime(WDate).ToString("yyyy-MM-dd") + "'", "");
                            if (DsShift.Tables[0].Rows.Count > 0)
                            {
                                bool IsDayOff = Convert.ToBoolean(DsShift.Tables[0].Rows[0]["IsDayOff"]);
                                string SH_Intime = DsShift.Tables[0].Rows[0]["SH_INTIME"].ToString();
                                if (IsDayOff == true && SH_Intime == "00:00:00")
                                {
                                    objCommon.DisplayMessage("Not allow to Apply(Working Shift Not Assigned Against DayOff). Contact To Shift Incharge", this.Page);
                                    btnSave.Visible = false;
                                    return;
                                }
                                else if (SH_Intime == "00:00:00")
                                {
                                    objCommon.DisplayMessage("Not allow to Apply(Working Shift Not Assigned Against DayOff). Contact To Shift Incharge", this.Page);
                                    btnSave.Visible = false;
                                    return;
                                }


                                bool isleave = Convert.ToBoolean(DsShift.Tables[0].Rows[0]["IsLeave"]);
                                if (IsDayOff == false)
                                {
                                    if (isleave == true)
                                    {
                                    }
                                    else if (isleave == false)//to check leave_master => iscomp-off flag
                                    {
                                        //Holiday not found & Comp-off shift not found                                
                                        objCommon.DisplayMessage("Not allow to Apply(Holiday/Comp-off related shift Not Found)", this.Page);
                                        btnSave.Visible = false;
                                        return;
                                    }
                                    //else if (SH_Intime != "00:00:00")
                                    //{
                                    //    objCommon.DisplayMessage("Not allow to Apply(DayOff Status Not Assinged)Contact To Shift Incharge", this.Page);
                                    //    btnSave.Visible = false;
                                    //    return;
                                    //}
                                }

                            }
                            else
                            {
                                //Holiday not found & SHift not assigned
                                objCommon.DisplayMessage("Shift Not Assigned. Contact To Shift Incharge", this.Page);
                                btnSave.Visible = false;
                                return;
                            }
                        }
                    }
                }

                #endregion 

                objLM.EMPNO = Convert.ToInt32(Session["IDNO"]);
                objLM.DATE = Convert.ToDateTime(txtWDate.Text);
                DataSet ds = objLC.GetCompOffValidate(objLM);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IsValid = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsValid"]);
                    message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                    if (IsValid == false)
                    {
                        // MessageBox("" + message + "");
                        objCommon.DisplayMessage("" + message + "", this.Page);
                        btnSave.Visible = false;
                        return;
                    }
                    else
                    {
                        btnSave.Visible = true;   //ViewState["EXPIRY_DATE"]  =ViewState["NO_OF_DAYS"]   =   ViewState["INTIME"] =  ViewState["OUTTIME"] = ViewState["WORKHR"] =      
                        ViewState["EXPIRY_DATE"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["EXPIRY_DATE"]);
                        ViewState["NO_OF_DAYS"] = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"]);
                        ViewState["INTIME"] = (ds.Tables[0].Rows[0]["INTIME"]).ToString();
                        ViewState["OUTTIME"] = (ds.Tables[0].Rows[0]["OUTTIME"]).ToString();
                        ViewState["WORKHR"] = (ds.Tables[0].Rows[0]["WORKHR"]).ToString();
                        ViewState["IsDoubleDuty"] = ds.Tables[0].Rows[0]["IsDoubleDuty"];

                    }
                }
            }
            else
            {
                txtWDate.Text = string.Empty;
                txtReason.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}
