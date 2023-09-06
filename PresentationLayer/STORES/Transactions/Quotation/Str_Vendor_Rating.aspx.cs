using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STORES_Transactions_Quotation_Str_Vendor_Rating : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStr = new StoreMasterController();
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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindVendor();

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



    private void BindVendor()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "STORE_PARTY_CATEGORY", "PCNO", "PCNAME", "", "PCNAME");

        }
        catch (Exception)
        {

            throw;
        }
    }





    private void VendorList()
    {

        if (ddlCategory.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Category", Page);
            return;
        }
        DataSet ds = new DataSet();

        ds = objStr.GetVendorDetails(Convert.ToInt32(ddlCategory.SelectedValue));
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlItemMaster.Visible = true;
                lvParty.DataSource = ds;
                lvParty.DataBind();
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Data Not Found", Page);
                pnlItemMaster.Visible = false;
                lvParty.DataSource = null;
                lvParty.DataBind();
            }
        }
        else
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "Data Not Found", Page);
            pnlItemMaster.Visible = false;
            lvParty.DataSource = null;
            lvParty.DataBind();
        }
    }


    protected void bntSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue == "0")
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Category", Page);
                return;
            }
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            int res = 0;
            int count = 0;
            foreach (ListViewItem lvi_Head in lvParty.Items)
            {
                CheckBox chk = lvi_Head.FindControl("ckhAll") as CheckBox;
                Label lbl = lvi_Head.FindControl("lblPname") as Label;
                DropDownList ddl = lvi_Head.FindControl("ddlRemark") as DropDownList;

                if (chk.Checked == true)
                {
                    int Id = Convert.ToInt32(chk.ToolTip);
                    if (Session["dt"] != null)
                        dt = (DataTable)Session["dt"];
                    if (!dt.Columns.Contains("PNO"))
                        dt.Columns.Add("PNO", typeof(int));
                    if (!dt.Columns.Contains("PNAME"))
                        dt.Columns.Add("PNAME");
                    if (!dt.Columns.Contains("RATING"))
                        dt.Columns.Add("RATING");

                    DataRow dr = dt.NewRow();

                    dr["PNO"] = Convert.ToInt32(Id);
                    dr["PNAME"] = Convert.ToString(lbl.Text);
                    dr["RATING"] = Convert.ToString(ddl.SelectedValue);
                    dt.Rows.Add(dr);
                    Session["dt"] = dt;
                    count++;

                    res = objStr.UpdateRatingStatus(Convert.ToInt32(Session["colcode"]), dt);
                    pnlItemMaster.Visible = true;
                }
                //if (count == 0)
                //{
                //    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Atleast One Vendor", Page);
                //}
            }


            if (res >= 0)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Data Updated Successfully", Page);
                Session["dt"] = null;
                Clear();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlCategory.SelectedValue = "0";
        pnlItemMaster.Visible = false;
    }

    protected void lvParty_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            DataRowView rowView = (DataRowView)dataItem.DataItem;
            DropDownList ddlRemark = (DropDownList)e.Item.FindControl("ddlRemark");
            if (rowView["VendorRating"] == DBNull.Value)
            {

                ddlRemark.SelectedValue = "Please Select";
            }
            else
            {
                ddlRemark.SelectedValue = Convert.ToString(rowView["VendorRating"]);
            }
        }
    }
    protected void ddlRemark_SelectedIndexChanged(object sender, EventArgs e)
    {
        VendorList();
    }
    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        ShowReport("VendorRating", "VendorRating_Report.rpt");
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@UserName=" + Session["username"] + "," + "@PCNO=" + ddlCategory.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


}