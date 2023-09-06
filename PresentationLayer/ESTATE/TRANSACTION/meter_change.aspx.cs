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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;


public partial class ESTATE_Transaction_meter_change : System.Web.UI.Page
{
    Common objcommon = new Common();
    // QuarterAllotmentController objAllot = new QuarterAllotmentController();
    MeterChange     objMeterEn  = new MeterChange();
    MeterChangeCont objMeterCon = new MeterChangeCont();

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
                CheckPageAuthorization();
                txtselectdt.Text = Convert.ToString(System.DateTime.Now).Substring(0, 10);
                bindDropDownList();
                ViewState["QA_ID"] = null;
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    //objCommon.FillDropDownList(ddlvacatorName, "EST_CONSUMER_MST", "IDNO", "(Title+' '+FNAME+' '+MNAME+' '+ LNAME)AS NAME", "CONSUMERTYPE=" + ddlVacatortype.SelectedItem.Value, "IDNO");
    protected void bindDropDownList()
    {
        try
        {
              objcommon.FillDropDownList(ddlmeterType,    "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(1), "MTYPE_NO");
              objcommon.FillDropDownList(ddloldMeterId,   "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.METER_NO", "A.M_ID>0", "A.M_ID");
              objcommon.FillDropDownList(ddlNewMeterType, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(1), "MTYPE_NO");
           

           
              //objcommon.FillDropDownList(ddlWaterOldMeterId, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2)", "A.M_ID", "A.METER_NO", "A.M_ID>0", "A.M_ID");
              //objcommon.FillDropDownList(ddlWaterMeterType, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(2), "MTYPE_NO");
              //objcommon.FillDropDownList(ddlNewWaterMeterType, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(2), "MTYPE_NO");
       
              objcommon.FillDropDownList(ddlConsumerType, "EST_CONSUMERTYPE_MASTER", "IDNO", "CONSUMERTYPE_NAME", "IDNO>0", "IDNO");
              // objcommon.FillDropDownList(ddloccupantName , "EST_MATERIAL_MST", "MNO", "MNAME", "MNO>0", "MNO");

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void ddlConsumerType_SelectedIndexChanged(object sender, EventArgs e)
    {       
        //objcommon.FillDropDownList(ddloccupantName, "EST_CONSUMER_MST", "IDNO", "FNAME AS NAME", "ChkStatus='A' AND CONSUMERTYPE=" + ddlConsumerType.SelectedItem.Value + "and IDNO IN(SELECT NAME_ID from EST_QRT_ALLOTMENT where QRT_STATUS is null)", "IDNO");
        objcommon.FillDropDownList(ddloccupantName, "EST_QRT_ALLOTMENT Q INNER JOIN EST_QRT_OCCUPANT O ON (Q.QA_ID = O.QA_ID) LEFT JOIN EST_CONSUMER_MST C  ON(C.IDNO = Q.NAME_ID AND C.CONSUMERTYPE = Q.EMPTYPE_ID AND C.CHKSTATUS='A') LEFT JOIN PAYROLL_EMPMAS E ON (Q.NAME_ID = E.IDNO AND Q.EMPTYPE_ID <> 2)", "Q.NAME_ID AS IDNO", "ISNULL(C.TITLE +' '+C.CONSUMERFULLNAME, E.PFILENO + ' - ' + E.TITLE + ' ' + E.FNAME) AS NAME", "Q.EMPTYPE_ID =" + ddlConsumerType.SelectedValue + "AND Q.QRT_STATUS IS NULL", "Q.NAME_ID");
    }

    protected void ddloccupantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtselectdt.Text = Convert.ToString(System.DateTime.Now).Substring(0,10);
        DataSet ds = null;
        ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT", "QA_ID", "NAME_ID", "NAME_ID=" + ddloccupantName.SelectedItem.Value + " AND EMPTYPE_ID =" + ddlConsumerType.SelectedValue , "QA_ID");
        if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["QA_ID"].ToString()))
        {
            ViewState["QA_ID"] = ds.Tables[0].Rows[0]["QA_ID"].ToString();
            ds = objMeterCon.getMeterChangeInfo(Convert.ToInt16(ds.Tables[0].Rows[0]["QA_ID"].ToString()));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtQuatertype.Text     = ds.Tables[0].Rows[0]["QUARTER_TYPE"].ToString();
                txtqaterno.Text       =  ds.Tables[0].Rows[0]["QUARTER_NO"].ToString();
                hdnquatertypeno.Value = ds.Tables[0].Rows[0]["QNO"].ToString();
                hdnquaterno.Value     = ds.Tables[0].Rows[0]["IDNO"].ToString();
              
                ddloldMeterId.SelectedValue = ds.Tables[0].Rows[0]["ENERGYMETERNO"].ToString();
                ddlmeterType.SelectedValue = ds.Tables[0].Rows[0]["ENERGY"].ToString();
                //ddlWaterOldMeterId.SelectedValue = ds.Tables[0].Rows[0]["WATERMETERNO"].ToString();
                //ddlWaterMeterType.SelectedValue = ds.Tables[0].Rows[0]["WATER"].ToString();
                txtPreMonthReading.Text = ds.Tables[0].Rows[0]["OLD_R"].ToString();

                // ddlWaterOldMeterId.SelectedItem.Text = ds.Tables[0].Rows[0]["QUARTER_TYPE"].ToString();
                // ddlWaterMeterType.SelectedItem.Text  = ds.Tables[0].Rows[0]["WATER"].ToString();
               // txtPreMonthReading_water.Text = ds.Tables[0].Rows[0]["WATER_OLD_R"].ToString();
                // objcommon.FillDropDownList(ddlWaterOldMeterId, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(2), "MTYPE_NO");
            }
            else
            {
                objcommon.DisplayMessage(this,"Meter Not Availble OR Quater is Vacant.", this.Page);
            }            
        }
    }

    protected void btnMeterChange_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkEbillModification.Checked.Equals(true) ) //|| chkWaterBillModi.Checked.Equals(true))
            {
                objMeterEn.CONSUMERTYPE_ID = Convert.ToInt16(ddlConsumerType.SelectedItem.Value);
                objMeterEn.NAME_ID = Convert.ToInt16(ddloccupantName.SelectedItem.Value);
                objMeterEn.QUATERTYPE = txtQuatertype.Text.Trim();
                objMeterEn.QUATERNO = txtqaterno.Text.Trim();
                objMeterEn.M_CHANGE_DATE =   DateTime.Parse(txtselectdt.Text.Trim());
                objMeterEn.QUATERTYPENO  =   Convert.ToInt32(hdnquatertypeno.Value);
                objMeterEn.QUATERNOID    =   Convert.ToInt32(hdnquaterno.Value);
                objMeterEn.QA_ID =  Convert.ToInt32(ViewState["QA_ID"]);

                if (chkEbillModification.Checked.Equals(true))
                {
                    objMeterEn.OLD_EN_MID = Convert.ToInt16(ddloldMeterId.SelectedItem.Value);
                    objMeterEn.OLD_EN_MTYPE = Convert.ToInt16(ddlmeterType.SelectedItem.Value);
                    objMeterEn.PREV_EN_MONTH_R = Convert.ToInt16(txtPreMonthReading.Text.Trim());
                    objMeterEn.CURRENT_EN_MONTH_R = Convert.ToInt16(txtClosingReading.Text.Trim());
                    objMeterEn.NEW_EN_MID = Convert.ToInt16(ddlMeterId.SelectedItem.Value);
                    objMeterEn.NEW_EN_MTYPE = Convert.ToInt16(ddlNewMeterType.SelectedItem.Value);
                    objMeterEn.NEW_EN_MSTART_R = Convert.ToInt64(txtMeterStartReading.Text.Trim());
                    objMeterEn.DIFF_PREV_EN_METER_R = Convert.ToInt64(txtDifference.Text.Trim());
                }
                else
                {
                    objMeterEn.OLD_EN_MID = 0;
                    objMeterEn.OLD_EN_MTYPE = 0;
                    objMeterEn.PREV_EN_MONTH_R = 0;
                    objMeterEn.CURRENT_EN_MONTH_R = 0;
                    objMeterEn.NEW_EN_MID = 0;
                    objMeterEn.NEW_EN_MTYPE = 0;
                    objMeterEn.NEW_EN_MSTART_R = 0;
                    objMeterEn.DIFF_PREV_EN_METER_R = 0;
                }
                if (chkEbillModification.Checked.Equals(false))
                {
                    //objMeterEn.OLD_WA_MID = Convert.ToInt16(ddlWaterOldMeterId.SelectedItem.Value);
                    //objMeterEn.OLD_WA_MTYPE = Convert.ToInt16(ddlWaterMeterType.SelectedItem.Value);
                    //objMeterEn.PREV_WA_MONTH_R = Convert.ToInt16(txtPreMonthReading_water.Text.Trim());
                    //objMeterEn.CURRENT_WA_MONTH_R = Convert.ToInt16(txtWaterMeterStartReading.Text.Trim());
                    //objMeterEn.NEW_WA_MTYPE = Convert.ToInt16(ddlNewWaterMeterType.SelectedItem.Value);
                    //objMeterEn.NEW_WA_MID = Convert.ToInt16(ddlWaterMeterId.SelectedItem.Value);
                    //objMeterEn.NEW_WA_MSTART_R = Convert.ToInt64(txtWaterMeterStartReading.Text.Trim());
                    //objMeterEn.DIFF_PREV_WA_METER_R = Convert.ToInt64(txtWaterMeterDifference.Text.Trim());
                }
                else
                {
                    objMeterEn.OLD_WA_MID = 0;
                    objMeterEn.OLD_WA_MTYPE = 0;
                    objMeterEn.PREV_WA_MONTH_R = 0;
                    objMeterEn.CURRENT_WA_MONTH_R = 0;
                    objMeterEn.NEW_WA_MTYPE = 0;
                    objMeterEn.NEW_WA_MID = 0;
                    objMeterEn.NEW_WA_MSTART_R = 0;
                    objMeterEn.DIFF_PREV_WA_METER_R = 0;
                }
                CustomStatus cs = (CustomStatus)objMeterCon.MeterChangeEntry(objMeterEn);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objcommon.DisplayMessage(this, "Record Save Successfully.", this.Page);
                }
                else
                {
                    objcommon.DisplayMessage(this, "sorry,Transaction  Fail.", this.Page);
                }
            }
            else
            {
                objcommon.DisplayMessage(this, "Please Check the Check box for Meter Change.", this.Page);
            }
            clearselection();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void ddlNewMeterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objcommon.FillDropDownList(ddlMeterId, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.METER_NO", "B.MTYPE_NO =" + Convert.ToInt16(ddlNewMeterType.SelectedItem.Value)+"AND A.M_ID NOT IN( SELECT METER_NO  from EST_ADDMETER  where QRT_STATUS is null)", "A.M_ID");
        }
        catch
        {
        }
    }
    
    protected void clearselection()
    {
        txtselectdt.Text = Convert.ToString(System.DateTime.Now);
        ddlConsumerType.ClearSelection();
        ddlMeterId.ClearSelection();
        ddlmeterType.ClearSelection();
        ddlNewMeterType.ClearSelection();        
        ddloldMeterId.ClearSelection();      
        ddloccupantName.ClearSelection();
        txtClosingReading.Text = string.Empty;
        txtDifference.Text = string.Empty;
        txtMeterStartReading.Text = string.Empty;
        txtPreMonthReading.Text = string.Empty;       
        txtqaterno.Text = string.Empty;
        txtQuatertype.Text = string.Empty;
        txtselectdt.Text = string.Empty;       
        ViewState["action"] = "add";
        ViewState["QA_ID"] = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearselection();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddloccupantName.SelectedIndex > 0)
        {
            ShowReport("Meter Change Report", "rptMeterChange.rpt");
        }
        else
        {
            objcommon.DisplayMessage(this, "Please Select Resident Name.", this.Page);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_NAME_ID=" + ddloccupantName.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
