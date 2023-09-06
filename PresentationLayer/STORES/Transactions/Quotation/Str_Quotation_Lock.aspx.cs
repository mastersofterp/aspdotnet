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
using IITMS.NITPRM;

public partial class STORES_Transactions_Quotation_Str_Quotation_Lock : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
    Str_Quotation_Tender_Controller objQuotTender = new Str_Quotation_Tender_Controller();
    PurchaseComiteeController objPurchComitee = new PurchaseComiteeController();

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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

               // Tabs.ActiveTabIndex = 0;
                this.BindGrdQuotList();
                this.BindGrdTenderList();
                //objCommon.FillDropDownList(ddlUserNo, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE in (1,3,4)", "");
            }
        }
        String hiddenFieldValue = hidLastTab.Value;
        System.Text.StringBuilder js = new System.Text.StringBuilder();
        js.Append("<script type='text/javascript'>");
        js.Append("var previouslySelectedTab = ");
        js.Append(hiddenFieldValue);
        js.Append(";</script>");
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "acttab", js.ToString());
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
  private void BindGrdQuotList()
  {
      grdQuotList.DataSource = objQuotTender.GetAllQuotationEntry(Convert.ToInt32(Session["strdeptcode"].ToString()));
      grdQuotList.DataBind();
  }

  private void BindGrdTenderList()
  {
      grdTenderList.DataSource = objQuotTender.GetAllTender();
      grdTenderList.DataBind();
  }

  protected void btnUnLock_Click(object sender, EventArgs e)
  {
      try
      {
          int chkcount = 0;
          for (int i = 0; i < grdQuotList.Rows.Count; i++)
          {
              CheckBox chk = (CheckBox)grdQuotList.Rows[i].FindControl("chkqno");
              if (chk.Checked)
              {
                  chkcount++;
                  CustomStatus cssubmit = (CustomStatus)objQuotTender.ChangeLockForQuot(Convert.ToInt32(chk.ToolTip), '0');
                  //this.BindGrdQuotList();
                  //objCommon.DisplayUserMessage(updatepanel1, "Lock Change Successfully", this.Page);
                  DisplayMessage("Lock Changed Successfully.");
              }
          }
          if (chkcount <= 0)
          {
              // objCommon.DisplayUserMessage(updatepanel1, "Please select Quotation or Tender", this.Page);
              DisplayMessage("Please select Quotation or Tender.");
          }

          this.BindGrdQuotList();
      }
      catch (Exception Ex)
      {
          if (Convert.ToBoolean(Session["error"]) == true)
              objUaimsCommon.ShowError(Page, "Str_QuotORTnderLock.aspx.btnUnLock_Click() --> " + Ex.Message + " " + Ex.StackTrace);
          else
              objUaimsCommon.ShowError(Page, "Server Unavailable.");
      }
  }

  protected void chkqno_CheckedChanged(object sender, EventArgs e)
  {
      CheckBox chkqno = (CheckBox)sender;
      if (chkqno.Checked == true)
      {
          ViewState["QNO"] = chkqno.ToolTip;
          //mdlopenPage.Show();
          //FillComitee();
      }
  }


  protected void btnLock_Click(object sender, EventArgs e)
  {

      try
      {
          int chkcount = 0;
          for (int i = 0; i < grdQuotList.Rows.Count; i++)
          {
              CheckBox chk = (CheckBox)grdQuotList.Rows[i].FindControl("chkqno");
              if (chk.Checked)
              {
                  chkcount++;
                  CustomStatus cssubmit = (CustomStatus)objQuotTender.ChangeLockForQuot(Convert.ToInt32(chk.ToolTip), '1');
                  DisplayMessage("Lock Changed Successfully.");
                  //this.BindGrdQuotList();
                  // objCommon.DisplayUserMessage(updatepanel1, "Lock Change Successfully", this.Page);
              }
          }
          if (chkcount <= 0)
          {
              DisplayMessage("Please select Quotation or Tender.");
              // objCommon.DisplayUserMessage(updatepanel1, "Please select Quotation or Tender", this.Page);
              return;
          }

          this.BindGrdQuotList();
      }
      catch (Exception Ex)
      {
          if (Convert.ToBoolean(Session["error"]) == true)
              objUaimsCommon.ShowError(Page, "Str_QuotORTnderLock.aspx.btLock_Click() --> " + Ex.Message + " " + Ex.StackTrace);
          else
              objUaimsCommon.ShowError(Page, "Server Unavailable.");
      }
  }
  protected void btnTUnLock_Click(object sender, EventArgs e)
  {
      try
      {
          int ChkLock = 0;
          for (int i = 0; i < grdTenderList.Rows.Count; i++)
          {
              CheckBox chk = (CheckBox)grdTenderList.Rows[i].FindControl("chkqno");
              if (chk.Checked)
              {
                  ChkLock += 1;
                  CustomStatus cssubmit = (CustomStatus)objQuotTender.ChangeLockForTender(Convert.ToInt32(chk.ToolTip), '0');
                  objCommon.DisplayUserMessage(updatepanel2, "Lock Changed Successfully", this.Page);
                  //this.BindGrdTenderList();
              }
          }
          if (ChkLock <= 0)
          {
              objCommon.DisplayUserMessage(updatepanel2, "Please select Quotation or Tender", this.Page);
          }



          this.BindGrdTenderList();
      }
      catch (Exception Ex)
      {
          if (Convert.ToBoolean(Session["error"]) == true)
              objUaimsCommon.ShowError(Page, "Str_QuotORTnderLock.aspx.btnTUnLock_Click() --> " + Ex.Message + " " + Ex.StackTrace);
          else
              objUaimsCommon.ShowError(Page, "Server Unavailable.");
      }
  }
  protected void btnTLock_Click(object sender, EventArgs e)
  {
      try
      {
          int chkCount = 0;
          for (int i = 0; i < grdTenderList.Rows.Count; i++)
          {
              CheckBox chk = (CheckBox)grdTenderList.Rows[i].FindControl("chkqno");
              if (chk.Checked)
              {
                  chkCount++;
                  CustomStatus cssubmit = (CustomStatus)objQuotTender.ChangeLockForTender(Convert.ToInt32(chk.ToolTip), '1');
                  objCommon.DisplayUserMessage(updatepanel2, "Lock Changed Successfully", this.Page);
                  //this.BindGrdTenderList();
              }
          }
          if (chkCount <= 0)
          {
              objCommon.DisplayUserMessage(updatepanel2, "Please Select Quotation or Tender", this.Page);
          }
          this.BindGrdTenderList();
      }
      catch (Exception Ex)
      {
          if (Convert.ToBoolean(Session["error"]) == true)
              objUaimsCommon.ShowError(Page, "Str_QuotORTnderLock.aspx.btTLock_Click() --> " + Ex.Message + " " + Ex.StackTrace);
          else
              objUaimsCommon.ShowError(Page, "Server Unavailable.");
      }
  }

  void DisplayMessage(string Message)
  {
      ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
  }


  private void BindGrdTenderListLocked()
  {
      grdTenderList.DataSource = objQuotTender.GetAllTenderEntryLocked();
      grdTenderList.DataBind();
  }

  protected void ddlLockUnlock_SelectedIndexChanged(object sender, EventArgs e)
  {

      if (ddlLockUnlock.Text == "All")
      {
          this.BindGrdQuotList();
      }
      else if (ddlLockUnlock.Text == "Locked")
      {

      }
      //Tabs.ActiveTabIndex = 0;
  }

  protected void ddlLockUnlockTender_SelectedIndexChanged(object sender, EventArgs e)
  {

      if (ddlLockUnlockTender.Text == "All")
      {
          this.BindGrdTenderList();
      }
      else if (ddlLockUnlockTender.Text == "Locked")
      {
          this.BindGrdTenderListLocked();
      }
      // Tabs.ActiveTabIndex = 1;
  }

 

  protected void imgClose_Click(object sender, ImageClickEventArgs e)
  {
      for (int i = 0; i < grdQuotList.Rows.Count; i++)
      {
          CheckBox chkqno = (CheckBox)grdQuotList.Rows[i].FindControl("chkqno");
          if (chkqno.ToolTip == ViewState["QNO"].ToString())
          {
              chkqno.Checked = false;
          }
      }

      ViewState["QNO"] = null;

  }

    
}


    