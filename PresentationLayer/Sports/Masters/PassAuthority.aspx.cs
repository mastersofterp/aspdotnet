//=======================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS AND EVENT MANAGEMENT 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 26-04-2017
// DESCRIPTION   : USED TO CREATE PASSING AUTHORITY
//========================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Sports_Masters_PassAuthority : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    PassignAuthorityEnt objPAEnt = new PassignAuthorityEnt();
    PassingAuthorityController objPACon = new PassingAuthorityController();


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
                //Page Authorization
                this.CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                FillCollege();
                FillUser();
                BindListViewPAuthority();
                btnShowReport.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    protected void BindListViewPAuthority()
    {
        try
        {
            DataSet ds = objPACon.GetAllPassAuthority(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;

            }
            else
            {
                btnShowReport.Visible = false;

            }
            lvPAuthority.DataSource = ds;
            lvPAuthority.DataBind();
            pnlList.Visible = true;


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void Clear()
    {
        txtPAuthority.Text = string.Empty;
        ddlUser.SelectedIndex = ddlCollege.SelectedIndex = 0;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";
        divButtons.Visible = false;
        divButtonFooter.Visible = true;
    }

    private void FillUser()
    {
        try
        {
            if (ddlCollege.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlUser, "PAYROLL_EMPMAS E INNER JOIN USER_ACC U ON(U.UA_IDNO=E.IDNO)", "U.UA_NO", "U.UA_FULLNAME ", "U.UA_TYPE IN(4,8) ", "U.UA_FULLNAME");
                objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (4,8) ", "UA_FULLNAME");
                objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME", "", "UA_FULLNAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            objPAEnt.PANAME = Convert.ToString(txtPAuthority.Text);
            objPAEnt.UANO = Convert.ToInt32(ddlUser.SelectedValue);
            objPAEnt.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objPAEnt.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objPAEnt.PANO = 0;
                    DataSet ds = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANO", " UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STATUS = 0", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox("Sorry! Record Already Exists");
                        return;
                    }
                    CustomStatus cs = (CustomStatus)objPACon.AddUpdatePassAuthority(objPAEnt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        Clear();
                        BindListViewPAuthority();

                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        divButtons.Visible = true;
                        divButtonFooter.Visible = false;

                        ViewState["action"] = null;
                        MessageBox("Record Saved Successfully");
                    }
                }
                else
                {
                    if (ViewState["PANO"] != null)
                    {
                        objPAEnt.PANO = Convert.ToInt32(ViewState["PANO"].ToString());
                        DataSet ds = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANO", "PANAME='" + txtPAuthority.Text + "' and UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND PANO!=" + objPAEnt.PANO + "", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox("Sorry! Record Already Exists");
                            return;
                        }

                        CustomStatus cs = (CustomStatus)objPACon.AddUpdatePassAuthority(objPAEnt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            BindListViewPAuthority();

                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            divButtons.Visible = true;
                            divButtonFooter.Visible = false;
                            btnShowReport.Visible = false;
                            btnAdd.Visible = true;
                            ViewState["action"] = null;
                            MessageBox("Record Updated Successfully");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        divButtons.Visible = true;
        divButtonFooter.Visible = false;
        btnAdd.Visible = true;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PANO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PANO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            divButtonFooter.Visible = true;
            //Modified by Saahil Trivedi 23-02-2022
            btnAdd.Visible = false;
            btnShowReport.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int PANO = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("SPRT_APPROVAL_AUTHORITY_PATH", "*", "", "( PAN01 = " + PANO + " OR PAN02 = " + PANO + " OR PAN03 = " + PANO + " OR PAN04 = " + PANO + " OR PAN05 = " + PANO + ")", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Can Not Be Deleted. It is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objPACon.DeletePassingAuthority(PANO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindListViewPAuthority();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully.');", true);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowDetails(Int32 PANO)
    {
        DataSet ds = null;
        try
        {
            ds = objPACon.GetSingPassAuthority(PANO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PANO"] = PANO;
                string colno = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                if (colno != string.Empty)
                {
                    ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                }

                FillUser();
                string userno = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                if (userno != string.Empty)
                {
                    ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                }

                txtPAuthority.Text = ds.Tables[0].Rows[0]["PANAME"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("ApprovalAuthorityList", "ApprovalAuthority.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + ",@P_PANO=0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PassingAuthority.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillUser();
    }
}