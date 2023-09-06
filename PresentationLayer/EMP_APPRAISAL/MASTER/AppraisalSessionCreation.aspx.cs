//=================================================================================
// MODULE NAME   : EMLOYEE APPRAISAL
// PAGE NAME     : AppraisalSessionCreation.aspx
// CREATION DATE : 6-03-2021
// CREATED BY    : TEJAS JAISWAL
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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


public partial class EMP_APPRAISAL_MASTER_AppraisalSessionCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    EmpSessionEnt objESEnt = new EmpSessionEnt();
    EmpSessionCon objESCon = new EmpSessionCon();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();


    #region Page load Functionality
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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["action"] = "add";
                BindListViewSessionEntry();
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
                Response.Redirect("~/notauthorized.aspx?page=empinfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=empinfo.aspx");
        }
    }

    #endregion


    #region Bind List Functionality

    private void BindListViewSessionEntry()
    {
        try
        {
            DataSet ds = objESCon.GetAllSessionEntry();
            lvAppraisalDetails.DataSource = ds;
            lvAppraisalDetails.DataBind();
            for (int i = 0; i < lvAppraisalDetails.Items.Count; i++)
            {
                Label lblActive = lvAppraisalDetails.Items[i].FindControl("lblActive") as Label;
                if (ds.Tables[0].Rows[i]["IS_ACTIVE"].ToString().Equals("ACTIVE"))
                {
                    lblActive.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblActive.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRIASAL_AppraisalSessionCreation.BindListViewLwpEntry()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion


    #region Edit Functionality

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SessionNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(SessionNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRIASAL_AppraisalSessionCreation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void ShowDetails(Int32 SessionNo)
    {

        DataSet ds = null;
        try
        {
            ds = objESCon.GetSingleSessionEntry(SessionNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SESSION_ID"] = SessionNo.ToString();
                txtSessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                txtSessionShortName.Text = ds.Tables[0].Rows[0]["SESSION_SHORTNAME"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                if (ds.Tables[0].Rows[0]["IS_SPECIAL"].ToString() == "S")
                {
                    rdbSpecialYes.Checked = true;
                    rdbSpecialNo.Checked = false;
                }
                else
                {
                    rdbSpecialNo.Checked = true;
                    rdbSpecialYes.Checked = false;
                }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString()) == false)
                {
                    rdbActiveNo.Checked = true;
                    rdbActiveYes.Checked = false;
                }
                else
                {
                    rdbActiveYes.Checked = true;
                    rdbActiveNo.Checked = false;
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRIASAL_AppraisalSessionCreation.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    #endregion


    #region Submit Functionality

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            objESEnt.SESSION_NAME = Convert.ToString(txtSessionName.Text);
            objESEnt.SESSION_SHORTNAME = Convert.ToString(txtSessionShortName.Text);

            if (Convert.ToDateTime(txtFromDate.Text) < System.DateTime.Now)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Start Date should be greater than Current date.", this.Page);
                txtFromDate.Text = "";
                return;

            }
            else
            {
                objESEnt.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
            }



            if (Convert.ToDateTime(txtToDate.Text) < System.DateTime.Now)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "End Date should be smaller than Current date.", this.Page);
                txtToDate.Text = "";
                return;


            }
            else
            {
                objESEnt.TODATE = Convert.ToDateTime(txtToDate.Text);
            }


            //  if (!txtFromDate.Text.Trim().Equals(string.Empty)) objESEnt.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
            // if (!txtToDate.Text.Trim().Equals(string.Empty)) objESEnt.TODATE = Convert.ToDateTime(txtToDate.Text); 
            objESEnt.IS_SPECIAL = rdbSpecialYes.Checked ? 'S' : 'N';
            objESEnt.ISACTIVE = rdbActiveYes.Checked ? true : false;
            objESEnt.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objESEnt.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objESEnt.SESSION_ID = 0;

                    DataSet ds = objCommon.FillDropDown("APPRAISAL_SESSION_MASTER", "*", "", "SESSION_NAME='" + txtSessionName.Text.TrimStart() + "' AND FROM_DATE='" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' AND TO_DATE='" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + "' ", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox("Sorry! Record Already Exist.");
                        Clear();
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objESCon.AddUpdSessionCreation(objESEnt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully.");
                        ViewState["action"] = null;

                    }
                }
                else
                {
                    if (ViewState["SESSION_ID"] != null)
                    {
                        objESEnt.SESSION_ID = Convert.ToInt32(ViewState["SESSION_ID"].ToString());

                        DataSet ds = objCommon.FillDropDown("APPRAISAL_SESSION_MASTER", "*", "", "SESSION_NAME='" + txtSessionName.Text + "' AND SESSION_ID != '" + objESEnt.SESSION_ID + "' AND FROM_DATE='" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' AND TO_DATE='" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + "' ", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox("Sorry! Record Already Exist");
                            Clear();
                            return;
                        }

                        CustomStatus CS = (CustomStatus)objESCon.AddUpdSessionCreation(objESEnt);
                        if (CS.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Updated Successfully");
                            ViewState["action"] = null;
                        }
                    }
                }
                Clear();
                BindListViewSessionEntry();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRIASAL_AppraisalSessionCreation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

    private void Clear()
    {

        txtSessionName.Text = string.Empty;
        txtSessionShortName.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        rdbSpecialNo.Checked = true;
        rdbActiveYes.Checked = true;
        rdbActiveNo.Checked = false;
        rdbSpecialYes.Checked = false;
        ViewState["action"] = "add";

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int sessionno = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objESCon.DeleteSessionEntry(sessionno);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                MessageBox("Record Deleted Successfully");
                BindListViewSessionEntry();
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRIASAL_AppraisalSessionCreation.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}