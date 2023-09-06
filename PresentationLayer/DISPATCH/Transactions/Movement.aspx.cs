//--MODIFIED BY: MRUNAL SINGH
//-- MODIFIED DATE : 01-12-2014
//==========================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Dispatch_Transactions_Movement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();

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
                    ViewState["action"] = "add";
                    txtMovenmentDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    BindLetter();
                    objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                    BindUser();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.Page_Load --> " + ex.Message + " " + ex.StackTrace);
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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void lvLetterDetails_bound(object sender, ListViewItemEventArgs e)
    {
        Label lblSt = ((ListViewDataItem)e.Item).FindControl("lblStatus") as Label;
        CheckBox chk = ((ListViewDataItem)e.Item).FindControl("chkLNo") as CheckBox;
        if (lblSt.Text == "MOVE")
        {
            chk.Enabled = false;
        }

    }
    // MRUNAL SINGH
    //Purpose: To bind PendingLette in listview
    private void BindLetter()
    {
        try
        {
            //DataSet ds = objIOtranc.GetLetter();
            DataSet ds = objIOtranc.GetPendingLetter();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                lvlinks.Visible = true;
            }
            else
            {
                lvlinks.DataSource = null;
                lvlinks.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.BindLetter --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void BindUser()
    {
        try
        {
            DataSet ds = objIOtranc.GetUserByBranchNo(Convert.ToInt32(ddlDepartment.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                reUser.DataSource = ds;
                reUser.DataBind();
            }
            else
            {
                reUser.DataSource = null;
                reUser.DataBind();
            }
        }
        catch (Exception ex)
        {
            if
                (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.BindUser -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void lbtnCentralRefNo_Click(object sender, EventArgs e)
    {
        LinkButton btnIO = sender as LinkButton;

        string INO = Convert.ToString(btnIO.Text);
        string IOTRA = objCommon.LookUp("ADMN_IO_TRAN", "IOTRANNO", "CENTRALREFERENCENO='" + Convert.ToString(INO) + "'");
        //ViewState["INO"] = int.Parse(btnIO.CommandArgument);
        this.BindUser1(INO);
        ViewState["IONO"] = IOTRA;
        ViewState["action"] = "edit";
    }

    protected void lvUser_Bound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkSel = ((ListViewDataItem)e.Item).FindControl("chkId") as CheckBox;
        HiddenField hfApplied = e.Item.FindControl("hfApplied") as HiddenField;
        if (hfApplied.Value == "1")
            chkSel.Checked = true;
        else
            chkSel.Checked = false;

    }



    protected void BindUser1(string IOTranNO)
    {
        DataSet ds = objIOtranc.GetUserByIONo(Convert.ToString(IOTranNO));
        reUser.DataSource = ds;
        reUser.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IOTRAN objIOtran = new IOTRAN();
        try
        {
            if (reUser != null)
            {
                string IdNo = string.Empty;
                string LtrNo = string.Empty;
                string CenRefNo = string.Empty;


                foreach (RepeaterItem dti in reUser.Items)
                {

                    CheckBox chkSel = dti.FindControl("chkId") as CheckBox;
                    // CheckBox chkSel = dti.FindControl("chkLNo") as CheckBox;

                    //HiddenField hdfI =dti.FindControl("hfCenNo") (HiddenField)Items ;
                    if (chkSel.Checked)
                    {

                        if (IdNo.Equals(string.Empty))
                        {
                            IdNo = chkSel.ToolTip;
                            //CenRefNo = hdfI.Value;
                        }
                        else
                        {
                            IdNo = IdNo + "," + chkSel.ToolTip;
                            //CenRefNo = CenRefNo + "," + hdfI.Value;
                        }
                    }
                }
                if (IdNo.Equals(string.Empty))
                {
                    //objCommon.DisplayMessage(this.updActivity, "Please Select Atleast One User.", this.Page);
                    objCommon.DisplayMessage("Please Select Atleast One User.", this.Page);
                    return;
                }


                if (lvlinks != null)
                {

                    foreach (RepeaterItem dtie in lvlinks.Items)
                    {
                        CheckBox chbx = dtie.FindControl("chkLNo") as CheckBox;
                        if (chbx.Checked)
                            if (LtrNo.Equals(string.Empty))
                            {
                                LtrNo = chbx.ToolTip;
                            }
                            else
                            {
                                LtrNo = LtrNo + "," + chbx.ToolTip;
                            }
                    }

                }

                objIOtran.MOV_DATE = txtMovenmentDT.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtMovenmentDT.Text);
                objIOtran.TODEPT = Convert.ToInt32(ddlDepartment.SelectedValue);
                objIOtran.REMARK = txtRemark.Text.ToString().Trim();

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        if (LtrNo.Equals(string.Empty))
                        {
                            //objCommon.DisplayMessage(this.updActivity, "Select Letter.", this.Page);
                            objCommon.DisplayMessage("Select Letter.", this.Page);
                            return;
                        }
                        CustomStatus cs = (CustomStatus)objIOtranc.AddMovement(objIOtran, IdNo, LtrNo, txtPeon.Text.Trim());
                        if (Convert.ToInt32(cs) != -99)
                        {
                            // Response.Redirect(Request.Url.ToString());
                            ViewState["action"] = null;
                            //BindLetter();
                            /////
                            objCommon.DisplayMessage("Letter Moved To User", this.Page);
                           // objCommon.DisplayMessage(this.updActivity, "Letter Moved To User", this.Page);
                            clear();
                            BindUser();
                            BindLetter();
                        }
                    }
                    else
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            string ltrNo = Convert.ToString(ViewState["IONO"]);
                            CustomStatus cs = (CustomStatus)objIOtranc.UpdateMovement(objIOtran, IdNo, ltrNo);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                Response.Redirect(Request.Url.ToString());
                                ViewState["action"] = null;
                               // objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                                objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                            }

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlDepartment_SelectedIndexChange(object sender, EventArgs e)
    {
        BindUser();
        ViewState["action"] = "add";
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        BindUser();
        BindLetter();
    }
    private void clear()
    {
        ddlDepartment.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        txtPeon.Text = string.Empty;

        //foreach (Repeater item in lvlinks.Items)
        //{
        //    CheckBox chkIDNO = item.FindControl("chkLNo") as CheckBox;
        //    chkIDNO.Checked = false;
        //}
        //foreach (Repeater item in reUser.Items)
        //{
        //    CheckBox chkIDNO = item.FindControl("chkId") as CheckBox;
        //    chkIDNO.Checked = false;
        //}
        BindLetter();
    }
    //protected void lvLetterDetails_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        DataSet ds = objIOtranc.GetLetter();

        if (ds.Tables[0].Rows.Count > 0)
        {
            //lvLetterDetails.DataSource = ds;
            //lvLetterDetails.DataBind();
            lvlinks.DataSource = ds;
            lvlinks.DataBind();

            //lvLetterDetails.Visible = true;
        }
        else
        {

            //lvLetterDetails.DataSource = null;
            //lvLetterDetails.DataBind();
            lvlinks.DataSource = null;
            lvlinks.DataBind();
        }
    }
    protected void lbtnCentralRefNo_Click1(object sender, EventArgs e)
    {

    }
    //Created By: Swati GHate
    //Purpose: facility to do movement directly
    protected void lbtnGoToMovement_Click(object sender, EventArgs e)
    {
        Response.Redirect("Movement.aspx");
    }
    protected void txtrefNo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {

    }
}