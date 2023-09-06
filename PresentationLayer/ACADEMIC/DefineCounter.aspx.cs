//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DEFINE COUNTER
// CREATION DATE : 07-OCT-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class DefineCounter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    CounterController counterController = new CounterController();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddlCounterUser, "USER_ACC", "UA_NO", "UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + UA_FULLNAME COLLATE DATABASE_DEFAULT AS UA_FULLNAME", "UA_TYPE IN (1,7,5,8)", "UA_FULLNAME");
                    //this.objCommon.FillDropDownList(ddlCounterUser, "USER_ACC", "UA_NO", "UA_NAME", "UA_TYPE IN (1,7,5,8)", "UA_FULLNAME");

                    // bind receipt type check box list
                    RecieptTypeController recieptTypeController = new RecieptTypeController();
                    DataSet ds = recieptTypeController.GetRecieptTypes();
                    chkListReceiptTypes.DataSource = ds;
                    chkListReceiptTypes.DataTextField = "RECIEPT_TITLE";
                    chkListReceiptTypes.DataValueField = "RECIEPT_CODE";
                    chkListReceiptTypes.DataBind();

                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                }
                this.ShowAllCounters();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }   

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
        }
    }
    #endregion

    #region Actions

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Counter counter = this.BindDataFromControls();
            if (counter.ReceiptPermission.Length == 0)
            {
                objCommon.DisplayUserMessage(updplRoom, "Please select atleast one receipt permission for counter user.", this.Page);
              //  ShowMessage("Please select atleast one receipt permission for counter user.");
                return;
            }

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add Room
                if (ViewState["action"].ToString().Equals("add"))
                {
                    cs = (CustomStatus)counterController.AddCounter(counter);
                  
                }

                /// Update Room
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    counter.CounterNo = (GetViewStateItem("CounterNo") != string.Empty ? int.Parse(GetViewStateItem("CounterNo")) : 0);
                    cs = (CustomStatus)counterController.UpdateCounter(counter);
                  
                }
               
                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");
                else
                    this.ShowAllCounters();

                if (ViewState["action"].ToString().Equals("add"))
                {
                     objCommon.DisplayUserMessage(updplRoom, "Record Saved Successfully....", this.Page);
                    //this.ShowMessage("Record Saved Successfully.....");
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                     objCommon.DisplayUserMessage(updplRoom, "Record Updated Successfully....", this.Page);
                     
                   // this.ShowMessage("Record Updated Successfully....");
                }
            }
            this.ClearControlContents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int counterNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = counterController.GetCounterByNo(counterNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                BindDataToControls(ds.Tables[0].Rows[0]);

                ViewState["action"] = "edit";
                ViewState["CounterNo"] = ds.Tables[0].Rows[0]["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindDataToControls(DataRow dr)
    {
        try
        {
            if (dr["COUNTERNAME"].ToString() != null)
                txtCounterName.Text = dr["COUNTERNAME"].ToString();

            if (dr["PRINTNAME"].ToString() != null)
                txtPrintName.Text = dr["PRINTNAME"].ToString();

            if (dr["UA_NO"].ToString() != null &&
                ddlCounterUser.Items.FindByValue(dr["UA_NO"].ToString()) != null)
                ddlCounterUser.SelectedValue = dr["UA_NO"].ToString();

            if (dr["RECEIPT_PERMISSION"].ToString() != null && dr["RECEIPT_PERMISSION"].ToString().Length > 1)
            {
                string[] recType = dr["RECEIPT_PERMISSION"].ToString().Split(new char[]{','});
                foreach (ListItem item in chkListReceiptTypes.Items)
                {
                    foreach (string str in recType)
                    {
                        if (str.Trim() == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.BindDataToControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private Counter BindDataFromControls()
    {
        Counter counter = new Counter();
        try
        {
            counter.CounterName = txtCounterName.Text.Trim();
            counter.PrintName = txtPrintName.Text.Trim();
            
            if (ddlCounterUser.SelectedIndex > 0)
                counter.CounterUserId = Convert.ToInt32(ddlCounterUser.SelectedValue);
            
            foreach (ListItem item in chkListReceiptTypes.Items)
            {
                if (item.Selected)
                {
                    if (item.Value == "MFC" || item.Value == "MC")
                    {
                        counter.ReceiptPermission += "MFD";
                    }
                    else
                    {
                        //if (counter.ReceiptPermission.Length > 0) counter.ReceiptPermission += ",";
                        counter.ReceiptPermission += item.Value;
                    }
                }
            }
            counter.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.BindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
        return counter;
    }

    #endregion

    #region Private Methods
    private void ShowAllCounters()
    {
        try
        {
            DataSet ds = counterController.GetAllCounters();
            lvCounters.DataSource = ds;
            lvCounters.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.ShowAllRooms() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearControlContents()
    {
        txtCounterName.Text = "";
        txtPrintName.Text = "";
        ddlCounterUser.SelectedIndex = 0;
        ViewState["action"] = "add";
       // Response.Redirect(Request.Url.ToString());
    }



    private void ShowMessage(string message)
    {        
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    #endregion
}