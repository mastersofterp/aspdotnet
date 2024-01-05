using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_MASTERS_CurrencyHeads : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Session["colcode"] = "1";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlfees.Visible = false;
            }

            PopulateDropDownList();
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN Receipt Type
            objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "PAYTYPENO");
            objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", string.Empty, "RECIEPT_TITLE");
            objCommon.FillDropDownList(ddlCurrency, "ACD_CURRENCY", "CUR_NO", "CUR_SHORT", "", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CurrencyHeads.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesHeadCurrencyDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesHeadCurrencyDefinition.aspx");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblerror.Text = string.Empty;
            lblmsg.Text = string.Empty;
            lvFeesHead.DataSource = null;
            lvFeesHead.DataBind();
            ddlRecType.SelectedIndex = 0;
            ddlPayType.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            divCurencyType.Visible = false;
            this.btnReport.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CurrencyHeads.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            FeesHeadController objFHC = new FeesHeadController();
            int count = 0;

            foreach (ListViewDataItem lvHead in lvFeesHead.Items)
            {
                Label txtHead = lvHead.FindControl("txtHead") as Label;
                TextBox txtLName = lvHead.FindControl("txtLName") as TextBox;
                TextBox txtSName = lvHead.FindControl("txtSName") as TextBox;
                //DropDownList ddlCurren = lvHead.FindControl("ddlCurrency") as DropDownList;
                string RecCode = objCommon.LookUp("ACD_FEE_TITLE", "RECIEPT_CODE", "FEE_TITLE_NO = " + Convert.ToInt32((txtHead.ToolTip)));

                CustomStatus cs = (CustomStatus)objFHC.UpdateCurrencyHead(Convert.ToInt32((txtHead.ToolTip)), RecCode, Convert.ToInt32(ddlPayType.SelectedValue), Convert.ToInt32(ddlCurrency.SelectedValue));
                //CustomStatus cs = (CustomStatus)objFHC.UpdateFeeHead(objFeesHead);

                if (cs.Equals(CustomStatus.RecordUpdated))
                    count++;
            }

            if (count == lvFeesHead.Items.Count)
            {
                //lblmsg.Text = "Record Saved Successfully";
                objCommon.DisplayMessage("Record Saved Successfully", this.Page);

                FeesHeadController objFC = new FeesHeadController();
                DataSet ds = objFC.GetCurrencyHeads(ddlRecType.SelectedValue, Convert.ToInt32(ddlPayType.SelectedValue));
                lvFeesHead.DataSource = ds;
                lvFeesHead.DataBind();
                //ShowReport("FeesHead", "rptFeesHeadCurrencyReport.rpt"); Commented by Rishabh -01082022- DISCUSSED with Umesh Sir and Manoj Sir.
            }
            else
            {
                //lblerror.Text = "Error...";
                objCommon.DisplayMessage("Error...", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CurrencyHeads.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("FeesHead", "rptFeesHeadCurrencyReport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_RECIEPTCODE=" + ddlRecType.SelectedValue + ",@P_PAYTYPENO=" + ddlPayType.SelectedValue + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListViewCurrencyHead()
    {
        try
        {
            FeesHeadController objFHC = new FeesHeadController();
            DataSet ds = objFHC.GetCurrencyHeads(ddlRecType.SelectedValue, Convert.ToInt32(ddlPayType.SelectedValue));
            //DataSet ds = objCommon.FillDropDown("ACD_FEE_TITLE A, ACD_CURRENCY C", "A.FEE_TITLE_NO,A.FEE_HEAD", "C.CUR_SHORT", string.Empty, "A.SRNO");
            if (ds.Tables[0].Rows.Count <= 0)
            {
                pnlfees.Visible = false;
                lblmsg.Text = "Sorry No Record Found..";
            }
            else
            {
                lblmsg.Text = null;
                pnlfees.Visible = true;
                hfdcount.Value = "0";
                lvFeesHead.DataSource = ds;
                lvFeesHead.DataBind();
                //pnlStudGrid.Visible = true;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CurrencyHeads.BindListViewCurrencyHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlRecType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblerror.Text = string.Empty;
            lblmsg.Text = string.Empty;
            lvFeesHead.DataSource = null;
            lvFeesHead.DataBind();
            ddlPayType.SelectedIndex = 0;
            this.btnReport.Enabled = false;
            divCurencyType.Visible = false;
            //this.btnReport.Enabled = true;
            //BindListViewCurrencyHead();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CurrencyHeads.ddlRecType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

    //protected void lvFeesHead_ItemDataBound(object sender, ListViewItemEventArgs data)
    //{
    //    int i;
    //    FeesHeadController objFHC = new FeesHeadController();
    //    DataSet ds = objFHC.GetCurrencyHeads(ddlRecType.SelectedValue, Convert.ToInt32(ddlPayType.SelectedValue));
    //    if (ds.Tables[0].Columns[6] == null || ds.Tables[0].Columns[6].Equals(0))
    //    {
    //        DropDownList ddlCurr = data.Item.FindControl("ddlCurrency") as DropDownList;
    //        objCommon.FillDropDownList(ddlCurr, "ACD_CURRENCY", "CUR_NO", "CUR_SHORT", string.Empty, "");
    //        //ddlCurr.SelectedValue = ds.Tables[0].Rows[0]["CUR_NO"].ToString();
    //    }
    //    else
    //    {
    //        i = Convert.ToInt32(hfdcount.Value);
    //        //int CountList = 0;
    //        DropDownList ddlCurr = data.Item.FindControl("ddlCurrency") as DropDownList;

    //        //foreach (ListViewDataItem lvHeads in lvFeesHead.Items)
    //        {
    //            //DropDownList ddlCurr = lvHeads.FindControl("ddlCurrency") as DropDownList;
    //            string recCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE = '" + ddlRecType.SelectedItem.Text.Trim().ToString() + "'");
    //            objCommon.FillDropDownList(ddlCurr, "ACD_CURRENCY", "CUR_NO", "CUR_SHORT", "", "");
    //            ddlCurr.SelectedValue = ds.Tables[0].Rows[i]["CUR_NO"].ToString();
    //            hfdcount.Value = (i + 1).ToString();
    //            //CountList++;
    //        }
    //    }
    //}
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRecType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select Receipt Type...", this.Page);
                lvFeesHead.DataSource = null;
                lvFeesHead.DataBind();
                return;
            }
            this.btnReport.Enabled = true;
            if (ddlPayType.SelectedIndex > 0)
            {
                divCurencyType.Visible = true;
                FeesHeadController ob = new FeesHeadController();
                DataSet ds = ob.GetCurrencyHeads(ddlRecType.SelectedValue, Convert.ToInt32(ddlPayType.SelectedValue));
                //DataSet ds = objCommon.FillDropDown("ACD_FEE_TITLE A, ACD_CURRENCY C", "A.FEE_TITLE_NO,A.FEE_HEAD", "C.CUR_SHORT", string.Empty, "A.SRNO");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count <= 0)
                {
                    ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["CUR_NO"].ToString();
                }
            }
            else
            {
                divCurencyType.Visible = false;
            }
            BindListViewCurrencyHead();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CurrencyHeads.ddlPayType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}
