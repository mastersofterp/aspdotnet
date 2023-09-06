using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrievanceRedressal_Transaction_GrievanceDetailsSearch : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();
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
                    // this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    Session["RecTblGrCell"] = null;
                    ViewState["SRNO"] = null;
                    Session["RecTblGrCellRecord"] = null;
                    // BindlistView();
                    objCommon.FillDropDownList(ddlGrievanceType, "GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "", "GRIV_ID");
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() == "") || (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() != ""))
            {
                MessageBox("Select From Date and To Date");
            }
            else
            {
                BindlistView();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    private void BindlistView()
    {
        try
        {
            string fDate = null;
            string tDate = null;

            if (txtFromDate.Text == "" || txtFromDate.Text == string.Empty)
            {
                fDate = null;
            }

            else
            {
                fDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            }


            if (txtToDate.Text == "" || txtToDate.Text == string.Empty)
            {
                tDate = null;
            }

            else
            {
                tDate = Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
            }

            DataSet ds = objGrivC.GetGrievanceApplicationDetails(Convert.ToInt32(ddlGrievanceType.SelectedValue), fDate, tDate);


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvshowRedressalCell.DataSource = ds;
                lvshowRedressalCell.DataBind();
                lvshowRedressalCell.Visible = true;
            }
            else
            {
                lvshowRedressalCell.DataSource = null;
                lvshowRedressalCell.DataBind();
                lvshowRedressalCell.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.BindlistView -> " + ex.Message + " " + ex.StackTrace);
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
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlGrievanceType.SelectedIndex = 0;
        lvshowRedressalCell.Visible = false;
        lvshowRedressalCell.DataSource = null;
        lstGrvReply.Visible = false;
        lstGrvReply.DataSource = null;
        lblGrievance.Text = string.Empty;

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int GAID = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objGrivC.GetGrievanceApplicationReply(GAID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                
                lblGrievance.Text = "Grievance Topic : " + ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
                lstGrvReply.DataSource = ds;
                lstGrvReply.DataBind();
                lstGrvReply.Visible = true;
            }
            else
            {
                lstGrvReply.DataSource = null;
                lstGrvReply.DataBind();
                lstGrvReply.Visible = true;
            }
        }
        catch (Exception ex)
        {

            //throw;
        }
    }
}