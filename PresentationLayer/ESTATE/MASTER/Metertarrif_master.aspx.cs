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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ESTATE_Master_Metertarrif_master : System.Web.UI.Page
{
     Common objCommon                              = new Common();
   
     MeterTarrif objMeterTarrif                    = new MeterTarrif();
     MeterTeriffController objMeterTarrifContoller = new MeterTeriffController();
    

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
                    CheckPageAuthorization();
                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlenergymetertype, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID="+Convert.ToInt16(1), "MTYPE_NO");
                    objCommon.FillDropDownList(ddlwatermetertype, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID="+Convert.ToInt16(2), "MTYPE_NO");
                    BindEnergyMeterTypeMaster();
                    BindWaterMeterTypeMaster();
                }
                divMsg.InnerHtml = string.Empty;
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
   
     #region  energy
 
    protected void btnenergysubmit_Click(object sender, EventArgs e)
    {
            try
            {


                //foreach (RepeaterItem item in Repeater_energymetercharges.Items)
                //{
                //    Label lblItem = item.FindControl("lblenergymetertype") as Label;
                //    Label lblfrom = item.FindControl("lblenergyFrom") as Label;
                //    Label lblTO = item.FindControl("lblenergyTo") as Label;
                //    if(lblItem.Text.Equals(ddlenergymetertype.SelectedItem.Text))
                //    {
                //        if (Convert.ToInt16(txtenergyfrom.Text)> Convert.ToInt16(lblfrom.Text) && Convert.ToInt16(txtenergyto.Text) < Convert.ToInt16(lblTO.Text))
                //        {

                //            objCommon.DisplayMessage(this.updpnltariif, "duplicate", this.Page);
                //            break;
                //        }
                //        else 

                        
                        
                //    }


                //}



                if (!string.IsNullOrEmpty(txtenergyfrom.Text) && !string.IsNullOrEmpty(txtenergyto.Text) && !string.IsNullOrEmpty(txtenergyrate.Text))
                {

                    objMeterTarrif.Metertype      = Convert.ToInt16(ddlenergymetertype.SelectedItem.Value);
                    objMeterTarrif.MeterunitFrom  = Convert.ToInt64(txtenergyfrom.Text);
                    objMeterTarrif.MeterunitTO    = Convert.ToInt64(txtenergyto.Text);
                    objMeterTarrif.MeterRate    = Convert.ToDecimal(txtenergyrate.Text);


                    if (ViewState["action"].Equals("add"))
                    {
                        objMeterTarrif.MID = 0;
                        CustomStatus cs = (CustomStatus)objMeterTarrifContoller.AddMeterTariff(objMeterTarrif);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindEnergyMeterTypeMaster();
                            objCommon.DisplayMessage(updpnltariif, "Record Save Successfully.", this.Page);
                        }
                        else
                        {
                          objCommon.DisplayMessage(updpnltariif, "Sorry!Try Again.", this.Page);

                        }
                    }


                    if (ViewState["action"].Equals("edit"))
                    {

                        objMeterTarrif.MID = Convert.ToInt16(ViewState["MID"]);


                        CustomStatus cs = (CustomStatus)objMeterTarrifContoller.AddMeterTariff(objMeterTarrif);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindEnergyMeterTypeMaster();
                            objCommon.DisplayMessage(updpnltariif, "Record Updated Successfully.", this.Page);
                        }
                        else
                        {
                           objCommon.DisplayMessage(updpnltariif, "Sorry!Try Again.", this.Page);

                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnltariif, "Please Enter material Type.", this.Page);
                    }

                    funclear();
                }
            }
            catch (Exception ex)
            {

            }      

    }

    protected void Repeater_energymetercharges_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int MNO = Convert.ToInt32(e.CommandArgument);
            ViewState["MID"] = MNO;
            ViewState["action"] = "edit";
            DataSet ds = objCommon.FillDropDown("EST_METERUNIT_MST A INNER JOIN EST_METERTYPE_MST B ON(A.MTYPE_NO =B.MTYPE_NO)", "A.[FROM],A.[RATE/UNIT],A.[TO],B.MTYPE_NO,A.ID", "A.MTYPE_NO", "A.ID=" + MNO, "A.ID");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlenergymetertype.SelectedValue = ds.Tables[0].Rows[0]["MTYPE_NO"].ToString();
                txtenergyfrom.Text = ds.Tables[0].Rows[0]["FROM"].ToString();
                txtenergyto.Text = ds.Tables[0].Rows[0]["TO"].ToString();
                txtenergyrate.Text = ds.Tables[0].Rows[0]["RATE/UNIT"].ToString();
              //  txtmaterialtype.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void BindEnergyMeterTypeMaster()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("EST_METERUNIT_MST A inner join dbo.EST_METERTYPE_MST b on (a.MTYPE_NO =b.MTYPE_NO) inner join dbo.EST_METER_MST c  on(b.id = c.id  and b.ID =1)", "a.ID,b.METER_TYPE,a.[FROM],a.[TO],a.[RATE/UNIT]", "A.MTYPE_NO", "A.ID>0", "a.[FROM]");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                fldtariff.Visible = true;
                Repeater_energymetercharges.DataSource = ds;
                Repeater_energymetercharges.DataBind();
            }
            else
            {
                fldtariff.Visible =false;
                Repeater_energymetercharges.DataSource = null;
                Repeater_energymetercharges.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected void funclear()
    {
        ddlenergymetertype.ClearSelection();
        txtenergyfrom.Text = string.Empty;
        txtenergyrate.Text = string.Empty;
        txtenergyto.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void btnenergyreset_Click(object sender, EventArgs e)
    {
        funclear();
    }
#endregion 
   
     #region water
    protected void BindWaterMeterTypeMaster()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("EST_METERUNIT_MST A inner join dbo.EST_METERTYPE_MST b on (a.MTYPE_NO =b.MTYPE_NO) inner join dbo.EST_METER_MST c  on(b.id = c.id  and b.ID =2)", "a.ID,b.METER_TYPE,a.[FROM],a.[TO],a.[RATE/UNIT]", "A.MTYPE_NO", "A.ID>0", "a.[FROM]");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                fldwater.Visible = true;
                rpt_watermetercharges.DataSource = ds;
                rpt_watermetercharges.DataBind();
            }
            else
            {
                fldwater.Visible =false;
                rpt_watermetercharges.DataSource = null;
                rpt_watermetercharges.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
   
    protected void btnwatersubmit_Click(object sender, EventArgs e)
    {
            try
            {
                if (!string.IsNullOrEmpty(txtwaterfrom.Text) && !string.IsNullOrEmpty(txtwaterto.Text) && !string.IsNullOrEmpty(txtwaterto.Text))
                {
                     objMeterTarrif.Metertype = Convert.ToInt16(ddlwatermetertype.SelectedItem.Value);
                     objMeterTarrif.MeterunitFrom = Convert.ToInt64(txtwaterfrom.Text);
                     objMeterTarrif.MeterunitTO = Convert.ToInt64(txtwaterto.Text);
                     objMeterTarrif.MeterRate = Convert.ToDecimal(txtwaterrate.Text);

                    if (ViewState["action"].Equals("add"))
                    {
                        objMeterTarrif.MID = 0;
                        CustomStatus cs = (CustomStatus)objMeterTarrifContoller.AddMeterTariff(objMeterTarrif);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindWaterMeterTypeMaster();
                           objCommon.DisplayMessage(updpnltariif, "Record Save Successfully.", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnltariif, "Sorry!Try Again.", this.Page);

                        }
                    }

                    if (ViewState["action"].Equals("edit"))
                    {
                        objMeterTarrif.MID = Convert.ToInt16(ViewState["MID"]);

                        CustomStatus cs = (CustomStatus)objMeterTarrifContoller.AddMeterTariff(objMeterTarrif);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindWaterMeterTypeMaster();
                            objCommon.DisplayMessage(updpnltariif, "Record Updated Successfully.", this.Page);
                        }
                        else
                        {
                           objCommon.DisplayMessage(updpnltariif, "Sorry!Try Again.", this.Page);
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnltariif, "Please Enter Meter Type.", this.Page);
                    }
                    funWaterclear();
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
            int MNO = Convert.ToInt32(e.CommandArgument);
            ViewState["MID"] = MNO;
            ViewState["action"] = "edit";
            ds = objCommon.FillDropDown("EST_METERUNIT_MST A INNER JOIN EST_METERTYPE_MST B ON(A.MTYPE_NO =B.MTYPE_NO)", "A.[FROM],A.[RATE/UNIT],A.[TO],B.MTYPE_NO,A.ID", "A.MTYPE_NO", "A.ID=" + MNO, "A.ID");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlwatermetertype.SelectedValue = ds.Tables[0].Rows[0]["MTYPE_NO"].ToString();
                txtwaterfrom.Text = ds.Tables[0].Rows[0]["FROM"].ToString();
                txtwaterto.Text = ds.Tables[0].Rows[0]["TO"].ToString();
                txtwaterrate.Text = ds.Tables[0].Rows[0]["RATE/UNIT"].ToString();
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
        txtwaterfrom.Text = string.Empty;
        txtwaterrate.Text = string.Empty;
        txtwaterto.Text = string.Empty;
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

            ScriptManager.RegisterClientScriptBlock(this.updpnltariif, this.updpnltariif.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnWaterReport_Click(object sender, EventArgs e)
    {
        ShowReport("Water Meter Tarrif Master", "rptWaterMeterTarrifMaster.rpt");
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Energy Meter tarrif Master", "rptEnergyMeterTarrifMaster.rpt");
    }
}   

