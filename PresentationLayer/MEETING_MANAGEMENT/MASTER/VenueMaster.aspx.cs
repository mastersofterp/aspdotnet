using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MEETING_MANAGEMENT_MASTER_VenueMaster : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";

                }
                BindlistView();
                //msgcomp.Visible = false;
            }
            else
            {
                //msgcomp.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DesigMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("tbl_mm_Venue", "*", "", "STATUS=0", "PK_VENUEID");
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvDesig.DataSource = ds;
                lvDesig.DataBind();
                lvDesig.Visible = true;
            }
            else
            {

                lvDesig.DataSource = null;
                lvDesig.DataBind();
                lvDesig.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objMM.VENUE = Convert.ToString(txtVenue.Text);

            if (ViewState["action"] != null)
            {
                if (txtVenue.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Enter Data", this.Page);
                    return;
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        DataSet ds = objCommon.FillDropDown("tbl_mm_Venue", "PK_VENUEID", "VENUE", "STATUS = 0 AND VENUE='" + objMM.VENUE + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                            txtVenue.Text = string.Empty;
                            return;
                        }
                        else
                        {
                            objMM.PK_COMMITEEDES = 0;
                            CustomStatus cs = (CustomStatus)objMC.AddUpdate_Venue(objMM);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        objMM.PK_COMMITEEDES = Convert.ToInt32(ViewState["DesigNo"]);
                        // DataSet ds = objCommon.FillDropDown(" TBL_MM_COMITEE", "*", "", "NAME ='" + objMM.NAME + "' AND CODE='" + objMM.CODE + "' ", "");
                        DataSet ds = objCommon.FillDropDown("tbl_mm_CommiteeDesig", "PK_COMMITEEDES", "DESIGNATION", "STATUS = 0 AND DESIGNATION ='" + objMM.DESIGNAME + "' AND PK_COMMITEEDES !=" + Convert.ToInt32(ViewState["DesigNo"]), "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            txtVenue.Text = string.Empty;
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objMC.AddUpdate_Venue(objMM);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                                Clear();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int VenueID = int.Parse(btnDelete.CommandArgument);
            ViewState["DESIGNATION_ID"] = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "*", "", "VENUEID=" + VenueID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Venue can not delete, it is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objMC.DeleteVenue(VenueID);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindlistView();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "MEETING_MANAGEMENT_MASTER_DesigMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //msgcomp.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int DesigNo = int.Parse(btnEdit.CommandArgument);
            ViewState["DesigNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";

            ShowDetails(DesigNo);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetails(int DesigNo)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("tbl_mm_Venue", "*", "", "PK_VENUEID=" + Convert.ToInt32(ViewState["DesigNo"]) + "", "PK_VENUEID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtVenue.Text = Convert.ToString(dr["VENUE"]);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtVenue.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["DesigNo"] = null;
    }
}