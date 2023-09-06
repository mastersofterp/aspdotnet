using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Data.SqlClient;

public partial class ACADEMIC_ONLINEFEESCOLLECTION_MISC_Fine_Master : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    MiscFees misc = new MiscFees();
    FeeCollectionController feeController = new FeeCollectionController();
    #endregion

    #region Methods
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
        try
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                      //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method
                    PopulateDropDown();
                    //txtAccountName.Visible = false;
                    //txtaddress.Visible = false;
                    //ddlBankName.Visible = false;
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, ".Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateDropDown()
    {
       // objCommon.FillDropDownList(ddlCashBook, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RECIEPT_CODE='MF'", "");
        objCommon.FillDropDownList(ddlCashBook, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "BELONGS_TO='M'", "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StipendMasterNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StipendMasterNew.aspx");
        }
    }

    //private void BindListView()
    //{
    //    try
    //    {
    //        DataSet ds = feeController.GetStudentForBankDeatils(objMaster); ;
    //        lvmiscCollection.DataSource = ds;
    //        lvmiscCollection.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "STIPEND_BankMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.DisplayMessage("Server UnAvailable", this.Page);
    //    }
    //}
    #endregion

    #region Event
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlCashBook.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Please Select Cash Book", this.Page);
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("MISCHEAD_MASTER", "MISCHEADCODE,MISCHEAD", "MISCHEADSRNO,AMOUNT", "CBOOKSRNO LIKE '" + ddlCashBook.SelectedValue + "%'", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvmiscCollection.DataSource = ds;
                lvmiscCollection.DataBind();
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvmiscCollection.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0)
        {
            //objCommon.DisplayMessage("Please Select atleast one Student for bank details", this);
            objCommon.DisplayMessage(this.updBank, "Please Select atleast one Fees Head", this.Page);
            return;
        }

        ViewState["studcount"] = count;
        count = 0;
        try
        {
            foreach (ListViewDataItem dataitem in lvmiscCollection.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
                if (cbRow.Checked == true)
                {
                    HiddenField hfRow = (dataitem.FindControl("hidIdNo")) as HiddenField;
                    misc.Id = hfRow.Value;
                    TextBox txtAmount = (dataitem.FindControl("txtAmount")) as TextBox;
                    misc.Amount = txtAmount.Text;

                    //insert Bank Master
                    CustomStatus cs = (CustomStatus)feeController.UpdateFessCollectionMaster(misc);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        //BindListView();
                        objCommon.DisplayMessage(this.updBank, "Process Done Successfully !!!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Error !!!", this);
                    }
                    count++;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STIPEND_BankMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);

        }

    }

    //protected void lvmiscCollection_ItemDataBound_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
        //try
        //{
        //    DropDownList ddlBankName = e.Item.FindControl("ddlBankName") as DropDownList;
        //    DataSet ds = objCommon.FillDropDown("ACD_BANK", "BANKNO", "BANKNAME", "BANKNO > 0", "BANKNAME");
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataTableReader dtr = ds.Tables[0].CreateDataReader();
        //        while (dtr.Read())
        //        {
        //            ddlBankName.Items.Add(new ListItem(dtr["BANKNAME"].ToString(), dtr["BANKNO"].ToString()));
        //        }
        //    }
        //    ddlBankName.SelectedValue = ddlBankName.ToolTip;
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objCommon.ShowError(Page, "STIPEND_BankMaster.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objCommon.ShowError(Page, "Server UnAvailable");
    //    //}
    //}
    #endregion
}