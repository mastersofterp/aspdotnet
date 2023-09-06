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

public partial class ESTATE_Master_MeterNumber_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    MeterNumber_Master objMeterType = new MeterNumber_Master();
    MeterNumberMasterController objMeterTypecontroller = new MeterNumberMasterController();

    protected void Page_Load(object sender, EventArgs e)
     {
        try
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
                    //ViewState["action"] = "add";
                    //BindEnergyMeterMaster();
                    //BindWaterMeterMaster();
                    CheckPageAuthorization();
                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlmetertype, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(1), "MTYPE_NO");
                    objCommon.FillDropDownList(ddlwatermetertype, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(2), "MTYPE_NO");
                    BindEnergyMeterMaster();
                    BindWaterMeterMaster();
                }
                divMsg.InnerHtml = string.Empty;
                //objCommon.FillDropDownList(ddlmetertype, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "", "MTYPE_NO");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

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

    #region Energy
    
    protected void BindEnergyMeterMaster()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.METER_NO,A.EMETER_MULTI,A.RENT,B.METER_TYPE", "A.MTYPE_NO>0", "A.MTYPE_NO");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                fldenergy.Visible = true;
                Repeater_energymetercharges.DataSource = ds;
                Repeater_energymetercharges.DataBind();
            }
            else
            {
                fldenergy.Visible = false;
                Repeater_energymetercharges.DataSource = null;
                Repeater_energymetercharges.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_METERNO_MST", "*", " ", "METER_NO='" + txtmeternumber.Text + "' and MTYPE_NO="+ddlmetertype.SelectedValue.ToString(), "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    protected void btnsubmit_Click1(object sender, EventArgs e)
    {

    
        try
        {
            if (!string.IsNullOrEmpty(ddlmetertype.SelectedValue) && !string.IsNullOrEmpty(txtmeternumber.Text))
            {
                objMeterType.MeterTypeNo    = Convert.ToInt16(ddlmetertype.SelectedItem.Value);
                objMeterType.MeterNo        = txtmeternumber.Text.Trim();
                objMeterType.EmeterMultiple = 0;
                objMeterType.Rent           = 0;

                if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        objCommon.DisplayMessage(updPnl, "Record Already Exist.", this.Page);
                        funclear();
                        return;
                    }
                    //int A = 0;
                    objMeterType.MType = 0;
                        //Convert.ToInt32(ddlmetertype.SelectedValue);
                    CustomStatus cs = (CustomStatus)objMeterTypecontroller.AddMeterNumber(objMeterType );
                        //AddMeterNumber(objMeterType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindEnergyMeterMaster();
                        objCommon.DisplayMessage(updPnl, "Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updPnl, "Sorry! Try Again.", this.Page);

                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    //if (funDuplicate() == true)
                    //{
                    //    DataSet ds = new DataSet();
                    //    ds = objCommon.FillDropDown("EST_METERNO_MST", "*", " ", "METER_NO='" + txtmeternumber.Text + "' and MTYPE_NO=" + ddlmetertype.SelectedValue.ToString(), "");
                    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        //if (txtmeternumber.Text != ds.Tables[0].Rows[0]["METER_NO"].ToString().Trim())
                    //        //{
                    //        objCommon.DisplayMessage(updPnl, "Record Already Exist.", this.Page);
                    //        txtmeternumber.Focus();
                    //        funclear();
                    //        return;
                    //        //}
                    //    }
                    //}
                    objMeterType.MType            = Convert.ToInt16(ViewState["MType"]);
                    CustomStatus cs               = (CustomStatus)objMeterTypecontroller.AddMeterNumber(objMeterType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindEnergyMeterMaster();
                        objCommon.DisplayMessage(updPnl, "Record Updated Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updPnl, "Sorry! Try Again.", this.Page);

                    }
                }
                funclear();
            }
            //else
            //{
            //    objCommon.DisplayMessage(updPnl, "Please Select Meter Type!", this.Page);
            //}
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void Repeater_energymetercharges_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataSet ds = null;
        try
        {
            int MTNo = Convert.ToInt32(e.CommandArgument);
            ViewState["MType"] = MTNo;
            ViewState["action"] = "edit";
            ds = objCommon.FillDropDown("EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO = B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.MTYPE_NO,A.METER_NO,A.EMETER_MULTI,A.RENT,B.METER_TYPE", "A.M_ID=" + MTNo, "A.MTYPE_NO");

           // DataSet ds = objCommon.FillDropDown("EST_QRT_TYPE", "QUARTER_TYPE", "RENT", "QNO=" + QNO, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlmetertype.SelectedValue        = ds.Tables[0].Rows[0]["MTYPE_NO"].ToString();
                txtmeternumber.Text               = ds.Tables[0].Rows[0]["METER_NO"].ToString();
                txtelectricmetermultiple.Text     = ds.Tables[0].Rows[0]["EMETER_MULTI"].ToString();
                txtrent.Text                      = ds.Tables[0].Rows[0]["RENT"].ToString();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected void funclear()
    {
        ddlmetertype.ClearSelection();
        txtelectricmetermultiple.Text = string.Empty;
        txtmeternumber.Text = string.Empty;
        txtrent.Text = string.Empty;
        ViewState["action"] = "add";
    }


    protected void btnreset_Click1(object sender, EventArgs e)
    {
        funclear();
    }
#endregion 

    #region Water

    protected Boolean funWDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_METERNO_MST", "*", " ", "METER_NO='" + txtwatermeternumber.Text + "' and MTYPE_NO=" + ddlwatermetertype.SelectedValue.ToString(), "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

     protected void btnwatersubmit_Click1(object sender, EventArgs e)
    {

        try
        {
            if (!string.IsNullOrEmpty(txtwatermeternumber.Text) )
            {
                objMeterType.MeterTypeNo     = Convert.ToInt16(ddlwatermetertype.SelectedItem.Value);
                objMeterType.MeterNo         = txtwatermeternumber.Text.Trim();
                objMeterType.Rent = 0;

                if (ViewState["action"].Equals("add"))
                {
                    if (funWDuplicate() == true)
                    {
                        objCommon.DisplayMessage(updPnl, "Record Already Exist.", this.Page);
                        funWaterclear();
                        return;
                    }
                    //int A = 0;
                    objMeterType.MType = 0;
                        //Convert.ToInt32(ddlwatermetertype.SelectedValue);
                    CustomStatus cs          = (CustomStatus)objMeterTypecontroller.AddMeterNumber(objMeterType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindWaterMeterMaster();
                        objCommon.DisplayMessage(updPnl, "Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updPnl, "Sorry! Try Again.", this.Page);

                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    if (funWDuplicate() == true)
                    {
                        DataSet ds = new DataSet();
                        ds = objCommon.FillDropDown("EST_METERNO_MST", "*", " ", "METER_NO='" + txtwatermeternumber.Text + "' and MTYPE_NO=" + ddlwatermetertype.SelectedValue.ToString(), "");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            //if (txtwatermeternumber.Text != ds.Tables[0].Rows[0]["METER_NO"].ToString().Trim())
                            //{
                            objCommon.DisplayMessage(updPnl, "Record Already Exist.", this.Page);
                            txtwatermeternumber.Focus();
                            funclear();
                            return;
                            //}
                        }
                    }
                    objMeterType.MType              = Convert.ToInt16(ViewState["MType"]);
                    CustomStatus cs                 = (CustomStatus)objMeterTypecontroller.AddMeterNumber(objMeterType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindWaterMeterMaster();
                        objCommon.DisplayMessage(updPnl, "Record Updated Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updPnl, "Sorry! Try Again.", this.Page);

                    }
                }
                funWaterclear();
            }
            else
            {
                objCommon.DisplayMessage(updPnl, "Please Select Meter Type.", this.Page);
            }
            
        }
        catch (Exception ex)
        {

        }

    }
   
    protected void rpt_watermetercharges_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        DataSet ds = null;
        try
        {
            int MTNo          = Convert.ToInt32(e.CommandArgument);
            ViewState["MType"] = MTNo;
            ViewState["action"]      = "edit";
            //ds = objCommon.FillDropDown("EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO)", "A.MTYPE_NO", "A.METER_NO,A.EMETER_MULTI,A.RENT,B.METER_TYPE", "A.MTYPE_NO>0", "A.MTYPE_NO");
            ds=objCommon.FillDropDown("EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2)", "A.M_ID", "A.MTYPE_NO,A.METER_NO,A.EMETER_MULTI,A.RENT,B.METER_TYPE", "A.M_ID=" + MTNo, "A.MTYPE_NO");

            // DataSet ds = objCommon.FillDropDown("EST_QRT_TYPE", "QUARTER_TYPE", "RENT", "QNO=" + QNO, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlwatermetertype.SelectedValue     = ds.Tables[0].Rows[0]["MTYPE_NO"].ToString();
                txtwatermeternumber.Text            = ds.Tables[0].Rows[0]["METER_NO"].ToString();
                txtwaterrent.Text                   = ds.Tables[0].Rows[0]["RENT"].ToString();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
   
    protected void BindWaterMeterMaster()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2)", "A.M_ID", "A.METER_NO,A.EMETER_MULTI,A.RENT,B.METER_TYPE", "A.MTYPE_NO>0", "A.MTYPE_NO");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                fldwater.Visible = true;
                rpt_watermetercharges.DataSource = ds;
                rpt_watermetercharges.DataBind();
            }
            else
            {
                fldwater.Visible = false;
                rpt_watermetercharges.DataSource = null;
                rpt_watermetercharges.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void funWaterclear()
    {
        ddlwatermetertype.ClearSelection();
        txtwatermeternumber.Text = string.Empty;
        txtwaterrent.Text = string.Empty;
        ViewState["action"] = "add";

    }

    protected void btnwaterreset_Click(object sender, EventArgs e)
    {
        funWaterclear();
    }

    #endregion

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updEnergy, this.updEnergy.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Energy Meter Number Master", "rptEnergyMeterNumberMaster.rpt");
    }

    protected void btnwaterReport_Click(object sender, EventArgs e)
    {
        ShowReport("Water Meter Number Master", "rptWaterMeterNumberMaster.rpt");
    }   
}
