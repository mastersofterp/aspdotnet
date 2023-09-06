using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_PHDCommitteeDesignation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhdC = new PhdController();
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
                //  CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }
            ViewState["action"] = "add";
            BindListViewCommittee();
            BindListViewDesignation();
            BindListViewMapping();
            objCommon.FillDropDownList(ddlCommittee, "ACD_PHD_COMMITTEE", "COMMITTEE_ID", "COMMITTEE_NAME", "ACTIVESTATUS=1", "COMMITTEE_ID");
            objCommon.FillListBox(lboDesignation, "ACD_PHDCOMMITTEE_DESIGNATION", "DESIG_ID", "DESIGNATION", "DESIG_ID>0", "DESIG_ID");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PHDCommitteeDesignation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PHDCommitteeDesignation.aspx");
        }
    }
    protected void BindListViewCommittee()
    {
        try
        {
            int id = 0;
            int mode = 2;
            DataSet ds = objPhdC.GetEditCommitteeData(id, mode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PnCommittee.Visible = true;
                lvCommittee.DataSource = ds;
                lvCommittee.DataBind();
            }
            else
            {
                PnCommittee.Visible = false;
                lvCommittee.DataSource = null;
                lvCommittee.DataBind();
               // objCommon.DisplayMessage(this, "No Data Found", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.BindListViewCommitte-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListViewDesignation()
    {
        try
        {
            int id = 0;
            int mode = 2;
            DataSet ds = objPhdC.GetEditCommitteeDesignationData(id, mode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PnDesignation.Visible = true;
                lvDesignation.DataSource = ds;
                lvDesignation.DataBind();
            }
            else
            {
                PnDesignation.Visible = false;
                lvDesignation.DataSource = null;
                lvDesignation.DataBind();
             //   objCommon.DisplayMessage(this, "No Data Found", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.BindListViewDesignation-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void BindListViewMapping()
    {
        try
        {
            int committeid = 0;
            int mode = 2;
            DataSet ds = objPhdC.GetEditCommitteeMappingData(committeid, mode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PnMapping.Visible = true;
                lvMapping.DataSource = ds;
                lvMapping.DataBind();
            }
            else
            {
                PnMapping.Visible = false;
                lvMapping.DataSource = null;
                lvMapping.DataBind();
                //  objCommon.DisplayMessage(this, "No Data Found", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.BindListViewMapping-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void ClearCommitteeData()
    {
        //modified on 16/03/2023
        txtCommitteeName.Text = "";
        chkMaincom.Checked = false;
        // Response.Redirect(Request.Url.ToString());
    }

    public void ClearDesignationData()
    {
        //Response.Redirect(Request.Url.ToString());
        txtDesignation.Text = "";
        if (chkExternal.Checked == true)
        {
            chkExternal.Checked = false;
        }
    }
    public void ClearMappingData()
    {
        //Response.Redirect(Request.Url.ToString());
        ddlCommittee.SelectedIndex = 0;
        //lboDesignation.Items.Clear();
        lboDesignation.ClearSelection();
    }

    protected void btnCommitteeCancel_Click(object sender, EventArgs e)
    {
        ClearCommitteeData();
    }
    protected void btnCommitteeSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //modified on 16/03/2023
            string status;
            int maincomittestatus;
            string committe = txtCommitteeName.Text.Trim();
            if (hfdActive.Value == "true")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }
            if (chkMaincom.Checked == true)
            {
                maincomittestatus = 1;
            }
            else
            {
                maincomittestatus = 0;
            }
            int mode;
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                int id = Convert.ToInt32(ViewState["id"]);
                mode = 2;
                CustomStatus cs = (CustomStatus)objPhdC.UpdateCommitteeData(id, committe, status, mode, maincomittestatus);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearCommitteeData();
                    BindListViewCommittee();
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                }
            }
            else
            {
                mode = 1;

                CustomStatus cs = (CustomStatus)objPhdC.InsertCommitteeData(0, committe, status, mode, maincomittestatus);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ClearCommitteeData();
                    BindListViewCommittee();

                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                }

                else
                {
                    objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                    ClearCommitteeData();
                }
                BindListViewCommittee();
                objCommon.FillDropDownList(ddlCommittee, "ACD_PHD_COMMITTEE", "COMMITTEE_ID", "COMMITTEE_NAME", "ACTIVESTATUS=1", "COMMITTEE_ID");

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCommitteeEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnCommitteeEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnCommitteeEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnCommitteeEdit.CommandArgument);
            int mode = 1;
            ShowDetailCommittee(ID, mode);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.btnCommitteeEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDetailCommittee(int ID, int mode)
    {
        //modified on 16/03/2023
        DataSet ds = objPhdC.GetEditCommitteeData(ID, mode);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtCommitteeName.Text = ds.Tables[0].Rows[0]["COMMITTEE_NAME"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCommittee(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCommittee(false);", true);
            }
            if (ds.Tables[0].Rows[0]["COMMITTEE_STATUS"].ToString() == "1")
            {
                chkMaincom.Checked = true;
            }
            else
            {
                chkMaincom.Checked = false;
            }
        }
    }
    protected void lvDesignation_ItemEditing(object sender, ListViewEditEventArgs e)
    {
    }

    protected void btnDesignationCancel_Click(object sender, EventArgs e)
    {
        ClearDesignationData();
    }
    protected void btnDesignationSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status;
            string designation = txtDesignation.Text.Trim();
            if (chkExternal.Checked == true)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            int mode;
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                int id = Convert.ToInt32(ViewState["id"]);
                mode = 2;
                CustomStatus cs = (CustomStatus)objPhdC.UpdateCommitteeDesignationData(id, designation, status, mode);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearDesignationData();
                    BindListViewDesignation();
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                }
            }
            else
            {
                mode = 1;

                CustomStatus cs = (CustomStatus)objPhdC.InsertCommitteeDesignationData(0, designation, status, mode);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ClearDesignationData();
                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    BindListViewDesignation();
                }

                else
                {
                    objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                    ClearDesignationData();
                }
                BindListViewDesignation();
                objCommon.FillListBox(lboDesignation, "ACD_PHDCOMMITTEE_DESIGNATION", "DESIG_ID", "DESIGNATION", "EXTERNALSTATUS=1", "DESIG_ID");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetailDesignation(int ID, int mode)
    {

        DataSet ds = objPhdC.GetEditCommitteeDesignationData(ID, mode);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtDesignation.Text = ds.Tables[0].Rows[0]["DESIGNATION"].ToString();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["EXTERNALSTATUS"].ToString()) == 1)
            {
                chkExternal.Checked = true;
            }
            else
            {
                chkExternal.Checked = false;
            }
        }
    }

    protected void btnDesignationEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            int mode = 1;
            ShowDetailDesignation(ID, mode);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.btnDesignationEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnMappingCancel_Click(object sender, EventArgs e)
    {
        ClearMappingData();
    }
    protected void btnMappingSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string status;
            int committe = Convert.ToInt32(ddlCommittee.SelectedValue);
            int count = 0;
            string Designation = string.Empty;
            foreach (ListItem Item in lboDesignation.Items)
            {
                if (Item.Selected)
                {
                    Designation += Item.Value + ",";
                    count++;
                }
            }
            Designation = Designation.Substring(0, Designation.Length - 1);
            if (hfdStart.Value == "true")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }
            int mode;

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                int id = Convert.ToInt32(ViewState["id"]);
                mode = 2;

                CustomStatus cs = (CustomStatus)objPhdC.UpdateCommitteeMappingData(id, committe, Designation, status, mode);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                    BindListViewMapping();
                    ClearMappingData();
                }
            }
            else
            {
                mode = 1;
                CustomStatus cs = (CustomStatus)objPhdC.InsertCommitteeMappingData(0, committe, Designation, status, mode);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    BindListViewMapping();
                    ClearMappingData();
                }

                else
                {
                    objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                    ClearMappingData();
                }
                BindListViewMapping();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowDetailMapping(int committeid, int mode)
    {
        DataSet ds = objPhdC.GetEditCommitteeMappingData(committeid, mode);

        // string Designation = string.Empty;

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            string did = string.Empty;
            //foreach(ListViewDataItem Item in lvMapping.Items)
            //{
            if (ds.Tables[0].Rows[0]["COMMITTEE_ID"] == null | ds.Tables[0].Rows[0]["COMMITTEE_ID"].ToString().Equals(""))
                ddlCommittee.SelectedIndex = 0;
            else
                ddlCommittee.Text = ds.Tables[0].Rows[0]["COMMITTEE_ID"].ToString();
            int COMMITTEE_ID = int.Parse(ds.Tables[0].Rows[0]["COMMITTEE_ID"].ToString());



            objCommon.FillListBox(lboDesignation, "ACD_PHDCOMMITTEE_DESIGNATION", "DESIG_ID", "DESIGNATION", "DESIG_ID>0" , "DESIG_ID");
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                did = ds.Tables[0].Rows[j]["DESIG_ID"].ToString();
                for (int i = 0; i < lboDesignation.Items.Count; i++)
                {
                    if (did.ToString() == lboDesignation.Items[i].Value)
                    {
                        lboDesignation.Items[i].Selected = true;
                    }
                }
            }

            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetMapping(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetMapping(false);", true);
            }
        }
    }
    protected void btnMappingEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnCommitteeEdit = sender as ImageButton;
            int committeid = Convert.ToInt32(btnCommitteeEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnCommitteeEdit.CommandArgument);
            int mode = 1;
            ShowDetailMapping(committeid, mode);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.btnMappingEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}