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

public partial class ESTATE_Transaction_EnergyMeterReading : System.Web.UI.Page
{

    Common objcommon = new Common();
    EnergyMeterReading objMeReEn = new EnergyMeterReading();
    EnergyMeterReadingCont objMeReCon = new EnergyMeterReadingCont();

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
                trshowbutton.Visible = false;
                energyReading.Visible = false;
                //txtDate.Text = Convert.ToString(System.DateTime.Now);
                // txtselectdt.Text = Convert.ToString(System.DateTime.Now);
                //  bindDropDownList();
                ViewState["IDNO"] = "0";
            }
            objcommon.FillDropDownList(ddlBlock, "EST_BLOCK_MASTER", "BLOCKID", "BLOCKNAME", "BLOCKID>0", "BLOCKID");
        }
        divMsg.InnerHtml = string.Empty;
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

    protected void btnreset_Click(object sender, EventArgs e)
    {
        ViewState["IDNO"] = "0";
        txtDate.Text = string.Empty;
        ddlBlock.SelectedIndex = 0;
        energyReading.Visible = false;
        trshowbutton.Visible = false;
    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //  BindListView();
    //}

    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void BindListView()
    {
        try
        {
            string eDate = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
            objMeReEn.BLOCKID = Convert.ToInt32(ddlBlock.SelectedValue);
            string Status = string.Empty;
            string ReadingDate = string.Empty;
            string StatusAfter = string.Empty;

            DataSet dsStatusAfter = objMeReCon.GetLockUnlockStatus(eDate);
            if (dsStatusAfter != null && dsStatusAfter.Tables[0].Rows.Count > 0)
            {
                // string StatusAfter = objcommon.LookUp("EST_LOCK_UNLOCK_STATUS", "STATUS", "YEAR='" + Convert.ToDateTime(txtDate.Text).Year + "' and MONTH='" + Convert.ToDateTime(txtDate.Text).Month + "'-1");
                StatusAfter = dsStatusAfter.Tables[0].Rows[0]["STATUS"].ToString();
            }


            //  DataSet dsStatus = objcommon.FillDropDown("EST_ENERGY_METER_READING", "TOP 1 E_status as STATUS", "IDNO, MONTH_R", "DATEPART(YYYY,MONTH_R) =" + Convert.ToDateTime(txtDate.Text).Year + " and Month_No='" + Convert.ToDateTime(txtDate.Text).Month + "'-1", "IDNO DESC");
            DataSet dsStatus = objcommon.FillDropDown("EST_ENERGY_METER_READING", "TOP 1 E_status as STATUS", "IDNO, MONTH_R", "DATEPART(YYYY,MONTH_R) =" + Convert.ToDateTime(txtDate.Text).Year + " and Month_No = DATEPART(MM, DATEADD(MM, -1,'" + eDate + "'))", "IDNO DESC");

            if (dsStatus != null && dsStatus.Tables[0].Rows.Count > 0)
            {
                Status = dsStatus.Tables[0].Rows[0]["STATUS"].ToString();
                ReadingDate = Convert.ToDateTime(dsStatus.Tables[0].Rows[0]["MONTH_R"]).ToString("yyyy-MM-dd");
            }

            DataSet ds = null;
            if (Status.Trim() == "")
            {
                ds = objMeReCon.GetEnfoEnergyMeterRead(eDate, objMeReEn);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmitData.Enabled = true;
                    lvenergymeterreading.DataSource = ds;
                    lvenergymeterreading.DataBind();
                    energyReading.Visible = true;
                    trshowbutton.Visible = true;
                }
            }
            else if (Status.Trim() == "L".Trim())
            {
                ds = objMeReCon.GetEnfoEnergyMeterRead(eDate, objMeReEn);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmitData.Enabled = true;
                    lvenergymeterreading.DataSource = ds;
                    lvenergymeterreading.DataBind();
                    energyReading.Visible = true;
                    trshowbutton.Visible = true;
                }
                else
                {
                    energyReading.Visible = false;
                    trshowbutton.Visible = false;
                    lvenergymeterreading.DataSource = null;
                    lvenergymeterreading.DataBind();
                    objcommon.DisplayMessage(this.updpnl, "Meter Reading is Locked for Selected Month.", this.Page);

                }
            }
            else if (Convert.ToDateTime(txtDate.Text).Month > Convert.ToDateTime(ReadingDate).Month && Status.Trim() == "A".Trim())
            {
                energyReading.Visible = false;
                trshowbutton.Visible = false;
                lvenergymeterreading.DataSource = null;
                lvenergymeterreading.DataBind();
                objcommon.DisplayMessage(this.updpnl, "Previous month reading is not lock.", this.Page);
                return;
            }
            else if (Convert.ToDateTime(txtDate.Text).Month == Convert.ToDateTime(ReadingDate).Month && Status.Trim() == "A".Trim())
            {
                ds = objMeReCon.GetEnfoEnergyMeterRead(eDate, objMeReEn);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmitData.Enabled = true;
                    lvenergymeterreading.DataSource = ds;
                    lvenergymeterreading.DataBind();
                    energyReading.Visible = true;
                    trshowbutton.Visible = true;
                }
                else
                {
                    energyReading.Visible = false;
                    trshowbutton.Visible = false;
                    lvenergymeterreading.DataSource = null;
                    lvenergymeterreading.DataBind();
                    objcommon.DisplayMessage(this.updpnl, "Record Not Found For Selected Block.", this.Page);
                    return;
                }
            }
            else if (StatusAfter.Trim() == "A".Trim())
            {
                ds = objMeReCon.GetEnfoEnergyMeterRead(eDate, objMeReEn);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmitData.Enabled = true;
                    lvenergymeterreading.DataSource = ds;
                    lvenergymeterreading.DataBind();
                    energyReading.Visible = true;
                    trshowbutton.Visible = true;
                }
                else
                {
                    energyReading.Visible = false;
                    trshowbutton.Visible = false;
                    lvenergymeterreading.DataSource = null;
                    lvenergymeterreading.DataBind();
                    objcommon.DisplayMessage(this.updpnl, "Meter Reading is Locked for Selected Month.", this.Page);

                }

            }
            else
            {
                energyReading.Visible = false;
                trshowbutton.Visible = false;
                lvenergymeterreading.DataSource = null;
                lvenergymeterreading.DataBind();
                objcommon.DisplayMessage(this.updpnl, "Meter Reading is Locked for Selected Month.", this.Page);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //this is used to submit data
    protected void btnSubmitData_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem item in lvenergymeterreading.Items)
            {
                TextBox txt = item.FindControl("txtcurrentReading") as TextBox;
                TextBox Oldtxt = item.FindControl("txtoldreading") as TextBox;
                Label lblsrno = item.FindControl("lblsrno") as Label;
                CheckBox chk = item.FindControl("chkIDNO") as CheckBox;
                TextBox txtReadingDate = item.FindControl("txtReadingDate") as TextBox;
                TextBox total = item.FindControl("txttotalReading") as TextBox;
                //HiddenField hdnQAID = item.FindControl("hdnQaId") as HiddenField;

                //if (!string.IsNullOrEmpty(txt.Text))
                //{
                //if (chk.Checked.Equals(true))
                //{
                objMeReEn.IDNO += lblsrno.ToolTip + ",";
                objMeReEn.NameId += (item.FindControl("lblname") as Label).Text + ",";
                objMeReEn.QtrNo += (item.FindControl("hdnquarterno") as HiddenField).Value + ",";
                objMeReEn.MeterNo += (item.FindControl("hdnenergymeterno") as HiddenField).Value + ",";
                if (Convert.ToInt32(Oldtxt.Text) > Convert.ToInt32(txt.Text))
                {
                    objcommon.DisplayMessage(this.updpnl, "Old Reading should be less than Current Reading.", this.Page);
                }
                else
                {
                    objMeReEn.OldReading += (item.FindControl("txtoldreading") as TextBox).Text == string.Empty ? "0," : (item.FindControl("txtoldreading") as TextBox).Text + ",";
                    objMeReEn.CurrentReading += (item.FindControl("txtcurrentReading") as TextBox).Text == string.Empty ? "0," : (item.FindControl("txtcurrentReading") as TextBox).Text + ",";
                }
                objMeReEn.AdjUnit += (item.FindControl("txtadjUnits") as TextBox).Text == string.Empty ? "0," : (item.FindControl("txtadjUnits") as TextBox).Text + ",";
                objMeReEn.Total += (item.FindControl("txttotalReading") as TextBox).Text == string.Empty ? "0," : (item.FindControl("txttotalReading") as TextBox).Text + ",";
                objMeReEn.ConStatus += (item.FindControl("txtstatus") as TextBox).Text + ",";
                objMeReEn.QA_ID += (item.FindControl("hdnQaId") as HiddenField).Value == string.Empty ? "" : (item.FindControl("hdnQaId") as HiddenField).Value + ",";
                objMeReEn.EMP_CODE += (item.FindControl("hdnEmpCode") as HiddenField).Value == string.Empty ? "0," : (item.FindControl("hdnEmpCode") as HiddenField).Value + ",";
                objMeReEn.ReadingDate += (item.FindControl("txtReadingDate") as TextBox).Text == string.Empty ? "1900-12-31," : Convert.ToDateTime((item.FindControl("txtReadingDate") as TextBox).Text).ToString("yyyy-MM-dd") + ",";
                // }

                //}

            }
           
            objMeReEn.Monthreading = DateTime.Parse(txtDate.Text);
            objMeReEn.BLOCKID = Convert.ToInt32(ddlBlock.SelectedValue);
            if (Convert.ToInt16(ViewState["IDNO"]) == 1)
            {
                objMeReEn.CheckIDNO = 1;
            }
            else
            {
                objMeReEn.CheckIDNO = 0;
            }
            CustomStatus cs = (CustomStatus)objMeReCon.InsertMonthlyReading(objMeReEn);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objcommon.DisplayMessage(this.updpnl, "Record Save Successfully.", this.Page);
            }

            else
            {
                objcommon.DisplayMessage(this.updpnl, "Sorry!Transaction Fail.", this.Page);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    if(!string.IsNullOrEmpty(txtDate.Text))
    //    {
    //        BindListViewUpdate();

    //        ViewState["IDNO"] ="1";
    //    }
    //    else
    //    {
    //        objcommon.DisplayMessage(this.updpnl, "Please check Reding entry", this.Page);
    //    }
    // }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["IDNO"] = "0";
        txtDate.Text = string.Empty;
        energyReading.Visible = false;
        trshowbutton.Visible = false;
    }

    protected void BindListViewUpdate()
    {
        try
        {
            DataSet ds = null;
            ds = objMeReCon.GetEnfoEnergyMeterReadUpdate();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvenergymeterreading.DataSource = ds;
                lvenergymeterreading.DataBind();
                energyReading.Visible = true;
                trshowbutton.Visible = true;
            }
            else
            {
                energyReading.Visible = false;
                trshowbutton.Visible = false;
                lvenergymeterreading.DataSource = null;
                lvenergymeterreading.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = string.Empty;
            // url = System.Configuration.ConfigurationManager.AppSettings["clientReportPath"].ToString();

            url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString() + ",@P_DATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");

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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("Monthly Energy Meter Billing", "rptEnergyMeterReading.rpt");
    }
}
