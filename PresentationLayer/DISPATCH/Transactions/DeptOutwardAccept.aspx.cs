//============================================================
// CREATED BY    : MRUNAL SINGH
// CREATION DATE : 22-10-2019
// DESCRIPTION   : TO ACCEPT/ REJECT DEPARTMENT OUTWARD FORMS
//============================================================

using System;
using System.Collections.Generic;
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
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class DISPATCH_Transactions_DeptOutwardAccept : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();
    CarrierMaster objCM = new CarrierMaster();

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
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    ViewState["action"] = null;
                    BindListViewOutwardDispatch();
                    //  objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
        }
    }



    private void BindListViewOutwardDispatch()
    {
        try
        {
            DataSet ds = null;
            ds = objIOtranc.GetDepartmentOutwards();

            if (ds.Tables[0].Rows.Count > 0)
            {
                IvOutwardDispatch.DataSource = ds;
                IvOutwardDispatch.DataBind();
                IvOutwardDispatch.Visible = true;
            }
            else
            {
                IvOutwardDispatch.DataSource = null;
                IvOutwardDispatch.DataBind();
                IvOutwardDispatch.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.BindListViewOutwardDispatch -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        IOTRAN objIOtran = new IOTRAN();
        try
        {
            string strIOtranno = string.Empty;
            string strStatus = string.Empty;
            int Count = 0;
            foreach (ListViewItem i in IvOutwardDispatch.Items)
            {
                CheckBox chkID = (CheckBox)i.FindControl("chkLetter");
                DropDownList ddlS = (DropDownList)i.FindControl("ddlStatus");
                if (chkID.Checked == true)
                {
                    Count++;
                    if (ddlS.SelectedIndex == 0)
                    {
                        objCommon.DisplayUserMessage(updActivity, "Please Select Accept/Reject.", this);
                        return;
                    }
                }
            }
            if (Count == 0)
            {
                objCommon.DisplayUserMessage(updActivity, "Please Select At Least One Letter.", this);
                return;
            }

            foreach (ListViewItem i in IvOutwardDispatch.Items)
            {
                CheckBox chkID = (CheckBox)i.FindControl("chkLetter");
                DropDownList ddlS = (DropDownList)i.FindControl("ddlStatus");

                if (chkID.Checked == true)
                {
                    if (ddlS.SelectedIndex > 0)
                    {
                        if (strIOtranno == "")
                        {
                            strIOtranno = chkID.ToolTip;
                            strStatus = ddlS.SelectedValue;
                        }
                        else
                        {
                            strIOtranno += "," + chkID.ToolTip;
                            strStatus += "," + ddlS.SelectedValue;
                        }
                    }
                    //else
                    //{
                    //    objCommon.DisplayUserMessage(updActivity, "Please Select Accept/Reject.", this);
                    //    return;
                    //}
                }
                //else
                //{
                //    objCommon.DisplayUserMessage(updActivity, "Please Check Atleast One Letter.", this);
                //    return;
                //}
            }

            if (strIOtranno != "")
            {

                objIOtran.IN_TO_USER = Convert.ToInt32(Session["userno"]);// accept/ reject by

                CustomStatus cs = (CustomStatus)objIOtranc.UpdateAcceptRejectOutward(objIOtran, strIOtranno, strStatus);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    Clear_Controls();
                    BindListViewOutwardDispatch();
                    ViewState["action"] = null;
                    Session["RecTbl"] = null;
                    objCommon.DisplayUserMessage(updActivity, "Record Updated Successfully.", this);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Clear_Controls()
    {
        // ddlDepartment.SelectedIndex = 0; 

        ViewState["IOTRANNO"] = null;
        ViewState["action"] = "edit";

    }





    protected void btnback_Click(object sender, EventArgs e)
    {

        divSubmit.Visible = false;
        divListview.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BindListViewOutwardDispatch();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }










    protected void btnDeleteRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int IOTRANNO = int.Parse(btnDelete.CommandArgument);
            ViewState["IOTRANNO"] = int.Parse(btnDelete.CommandArgument);

            CustomStatus cs = (CustomStatus)objIOtranc.DeleteDeptOutward(IOTRANNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear_Controls();
                BindListViewOutwardDispatch();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }





}