//==========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   :  Appraisal Passing Authority               
// CREATION DATE : 10-03-2021
// CREATED BY    : TANU BALGOTE
// DESCRIPTION   :                        
// MODIFIED DATE :
// MODIFIED DESC :
//==========================================================================

using System;
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
using System.Data.SqlClient;


public partial class EmpAppraisal_AppraisalPassingAuthority : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AppraisalPassingAuthorityCon objPAuthority = new AppraisalPassingAuthorityCon();
    AppraisalPassingAuthorityEnt objEmpAuthority = new AppraisalPassingAuthorityEnt();

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
    //PAGE LOAD EVENT
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
                Page.Title = Session["coll_name"].ToString();
              //  CheckPageAuthorization();
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                FillCollege();
                FillUser();

                BindListViewPAuthority();
              }
            }
    }

    // It is used to check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    //THIS METHOD USE TO FILL DROPDOWN VALUE
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlCollegeGrid, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);

            removeItem = ddlCollegeGrid.Items.FindByValue("0");
            ddlCollegeGrid.Items.Remove(removeItem);
        }
    }

    //THIS IS ADD NEW AUTHORITY
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;

        ViewState["action"] = "add";
    }

    //THIS METHOD USE TO USER IN DROPDOWN
    private void FillUser()
    {
        try
        {

            if (ddlCollege.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (3,4,5,8,1) AND UA_STATUS=0 and UA_FULLNAME <> ''", "UA_FULLNAME");
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //THIS METHOD IS USED TO SAVE DATA
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
           
            objEmpAuthority.PANAME = Convert.ToString(txtPAuthority.Text);
            objEmpAuthority.UANO = Convert.ToInt32(ddlUser.SelectedValue);
            objEmpAuthority.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objEmpAuthority.COLLEGE_CODE = Session["colcode"].ToString();
            //objAppraisalEnt.IS_SPECIAL = rdbAnnual.Checked ? A : 'S
            objEmpAuthority.COMMON_AUTHORITY = Convert.ToBoolean(cbauthority.Checked ? 1 : 0);


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objEmpAuthority.PANO = 0;
                    //DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY", "UA_NO", "PANO", " UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STATUS = 0", "");
                   // if (cbauthority.Checked == false)
                   // {
                        DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY", "UA_NO", "PANO", " UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " " + " AND STATUS = 0", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox("Sorry! Record Already Exist");
                            return;
                        }
                  //  }
                    CustomStatus cs = (CustomStatus)objPAuthority.AddPassAuthority(objEmpAuthority);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewPAuthority();
                        ViewState["action"] = null;
                        Clear();


                    }
                }
                else
                {
                    if (ViewState["PANO"] != null)
                    {

                        objEmpAuthority.PANO = Convert.ToInt32(ViewState["PANO"].ToString());
                        //DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY", "UA_NO", "PANO", " UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND STATUS = 0", "");
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    MessageBox("Sorry! Record Already Exists");
                        //    return;
                        //}
                        CustomStatus cs = (CustomStatus)objPAuthority.UpdatePassAuthority(objEmpAuthority);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {

                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewPAuthority();
                            ViewState["action"] = null;
                            Clear();


                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //THIS METHOD IS USED TO SHOW MESSAGE 
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain            
    //    BindListViewPAuthority();
    //}
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillUser();
    }

    //THIS METHOD USED TO MODIFY THE DATA
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PANO = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY_PATH", "*", "", "( PAN01 = " + PANO + " OR PAN02 = " + PANO + " OR PAN03 = " + PANO + " OR PAN04 = " + PANO + " OR PAN05 = " + PANO + ")", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Can Not Be Update. It is in use.');", true);
                return;
            }
            else
            {

                ShowDetails(PANO);
                ViewState["action"] = "edit";
                pnlAdd.Visible = true;
                pnlList.Visible = false;
                btnSave.Text = "Update";
                btnCancel.Visible = false;
                ViewState["PANO"] = PANO;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    //THIS IS USED TO CLEAR CONTROLLS
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //THIS METHOD USED TO BACK TO MAIN PAGE
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    }

    //THIS METHOD USED TO CLEAR CONTROLS
    private void Clear()
    {
        txtPAuthority.Text = string.Empty;
        ddlUser.SelectedIndex = ddlCollege.SelectedIndex = 0;
        btnSave.Text = "Submit";
        btnCancel.Visible = true;
        ddlCollege.Focus();
        ViewState["action"] = "add";
        ViewState["PANO"] = null;
        cbauthority.Checked = false;

    }

    //THIS METHOD USED TO SHOW SUBMITED DETAIL (USE FOR MODIFICATION)
    private void ShowDetails(Int32 PANO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAuthority.GetSingPassAuthority(PANO);
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

                cbauthority.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["COMMON_AUTHORITY"]);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
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

            DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY_PATH", "*", "", "( PAN01 = " + PANO + " OR PAN02 = " + PANO + " OR PAN03 = " + PANO + " OR PAN04 = " + PANO + " OR PAN05 = " + PANO + ")", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Can Not Be Deleted. It is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objPAuthority.DeletePassAuthority(PANO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    ViewState["action"] = null;
                    BindListViewPAuthority();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlCollegeGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewPAuthority();
    }

    // THIS METHOD USED TO BIND THE DATA IN LIST VIEW
    protected void BindListViewPAuthority()
    {
        try
        {

            DataSet ds = objPAuthority.GetAllPassAuthority(Convert.ToInt32(ddlCollegeGrid.SelectedValue));


            lvPAuthority.DataSource = ds;
            lvPAuthority.DataBind();
            pnlList.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
   
}
