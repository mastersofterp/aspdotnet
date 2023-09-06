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

public partial class ESTATE_Transaction_WaterMeterReading : System.Web.UI.Page
{

    Common objcommon = new Common();
    WaterMeterReading objMeReEn = new WaterMeterReading();
    WaterMeterReadingCont objMeReCon = new WaterMeterReadingCont();

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
                waterReading.Visible = false;
                txtDate.Text = Convert.ToString(System.DateTime.Now);
                // txtselectdt.Text = Convert.ToString(System.DateTime.Now);
                ViewState["IDNO"] = "0";
                pnlbutton.Visible = false;

            }
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

    protected void lvmanualAtt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        pnlbutton.Visible = true;
        lvwatermeterreading.Visible = true;
        BindListView();
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds = null;
            ds = objMeReCon.GetEnfoWaterMeterRead(txtDate.Text);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvwatermeterreading.DataSource = ds;
                lvwatermeterreading.DataBind();
                waterReading.Visible = true;
                pnlbutton.Visible = true;
            }
            else
            {
                waterReading.Visible = false;
                lvwatermeterreading.DataSource = null;
                lvwatermeterreading.DataBind();
                pnlbutton.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //this is uswed to submit data
    protected void btnSubmitData_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem item in lvwatermeterreading.Items)
            {
                TextBox txt = item.FindControl("txtcurrentReading") as TextBox;
                Label lblsrno = item.FindControl("lblsrno") as Label;
                CheckBox chk = item.FindControl("chkIDNO") as CheckBox;

                //if (!string.IsNullOrEmpty(txt.Text))
                //{
                //    if (chk.Checked.Equals(true))
                //    {
                objMeReEn.IDNO += lblsrno.ToolTip + ",";
                objMeReEn.NameId += (item.FindControl("lblname") as Label).Text + ",";
                objMeReEn.QtrNo += (item.FindControl("hdnquarterno") as HiddenField).Value + ",";
                objMeReEn.MeterNo += (item.FindControl("hdnwatermeterno") as HiddenField).Value + ",";
                objMeReEn.OldReading += (item.FindControl("txtoldreading") as TextBox).Text + ",";
                objMeReEn.CurrentReading += (item.FindControl("txtcurrentReading") as TextBox).Text + ",";
                objMeReEn.AdjUnit += (item.FindControl("txtadjUnits") as TextBox).Text + ",";
                objMeReEn.Total += (item.FindControl("txttotalReading") as TextBox).Text + ",";
                objMeReEn.ConStatus += (item.FindControl("txtstatus") as TextBox).Text + ",";
                //    }

                //}
            }
            objMeReEn.Monthreading = DateTime.Parse(txtDate.Text);

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
                objcommon.DisplayMessage(this.updpnl, "Record Save Sucessfully!", this.Page);
            }

            else
            {
                objcommon.DisplayMessage(this.updpnl, "Sorry!Transaction Fail", this.Page);

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
        {
            lvwatermeterreading.Visible = true;

            BindListViewUpdate();
            ViewState["IDNO"] = "1";

        }
        else
        {
            objcommon.DisplayMessage(this.updpnl, "Please check Reding entry", this.Page);
        }
    }

    protected void BindListViewUpdate()
    {
        try
        {
            DataSet ds = null;
            ds = objMeReCon.GetEnfoWaterMeterReadUpdate();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvwatermeterreading.DataSource = ds;
                lvwatermeterreading.DataBind();
                waterReading.Visible = true;
                pnlbutton.Visible = true;
            }
            else
            {
                waterReading.Visible = false;
                lvwatermeterreading.DataSource = null;
                lvwatermeterreading.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        waterReading.Visible = false;
        pnlbutton.Visible = false;
        lvwatermeterreading.DataSource = null;
        lvwatermeterreading.DataBind();
        lvwatermeterreading.Visible = false;
        txtDate.Text = string.Empty;
        ViewState["IDNO"] = "0";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlbutton.Visible = false;
        ViewState["IDNO"] = "0";
        clearselection();
    }

    protected void clearselection()
    {
        lvwatermeterreading.DataSource = null;
        lvwatermeterreading.DataBind();
        lvwatermeterreading.Visible = false;
        txtDate.Text = string.Empty;
        waterReading.Visible = false;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("Monthly Billing Entry", "rptWaterMeterReading.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString() + ",@P_date=" + txtDate.Text;

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
