using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data.SqlClient;
using System.Data;
public partial class ACADEMIC_MASTERS_SubjectType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    CourseController objCc = new CourseController();
    Course objCe = new Course();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int istutorial = 0;
        Convert.ToInt32(ddlCondition.SelectedValue);
        objCe.theory_Pra = Convert.ToInt32(ddlCondition.SelectedValue);
        objCe.sec_batch = Convert.ToInt32(ddlAllotement.SelectedValue);
        //string subjname = txtSubjectType.Text.Trim();
        objCe.subjecttype = txtSubjectType.Text.Trim(); 
        if (chkTutorial.Checked == true)
        {
            istutorial = 1;
        }
        else
        {
            istutorial = 0;
        }
        if (hfdActive.Value == "true")
        {
            objCe.activestatus = "1";
        }
        else
        {
            objCe.activestatus = "0";
        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            int subid = Convert.ToInt32(ViewState["id"]);

            CustomStatus cs = (CustomStatus)objCc.UpdateSubjectTypeData(objCe, subid, istutorial);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                ClearData();
                BindListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ViewState["action"] = null;
            }

            else
            {
                BindListView();
                ClearData();
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);              
                ViewState["action"] = null;
            }
        }
        else
        {
            //Insert 
            CustomStatus cs = (CustomStatus)objCc.InsertSubjectTypeData(objCe, istutorial);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                BindListView();
                ClearData();
            }

            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearData();
            }
            BindListView();
        }
    }

    protected void BindListView()
    {
        try
        {

            DataSet ds = objCc.GetSubjectTypeList(objCe);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelSubjectType.Visible = true;
                lvSubjecttype.DataSource = ds.Tables[0];
                lvSubjecttype.DataBind();
            }
            else
            {
                PanelSubjectType.Visible = true;
                lvSubjecttype.DataSource = null;
                lvSubjecttype.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvSubjecttype.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

                Label Istutorial = dataitem.FindControl("lblistutorial") as Label;

                //string istutorial = (Istutorial.Text);
                //if (istutorial == "NA")
                //{
                   
                //}
                //else
                //{
                    
                //}

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "SubjectType.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();

    }
    public void ClearData()
    {
        txtSubjectType.Text = "";
        ddlAllotement.SelectedIndex = 0;
        ddlCondition.SelectedIndex = 0;
        chkTutorial.Checked = false;  
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SUBID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(SUBID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PbiConfiguration.btnEditWorkspace_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetail(int SUBID)
    {
        ddlAllotement.Items.Add(new ListItem("Section & Batch", "3"));
        DataSet ds = objCc.EditSubjectTypeData(SUBID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtSubjectType.Text = ds.Tables[0].Rows[0]["SUBNAME"].ToString();
            ddlCondition.SelectedValue = ds.Tables[0].Rows[0]["TH_PR"].ToString();
            if (ddlCondition.SelectedValue == "3")
            {
                ddlAllotement.Items.Clear();
                ddlAllotement.Items.Add(new ListItem("Please Select", "0"));
                ddlAllotement.Items.Add(new ListItem("Section & Batch", "3"));
            }
            else
            {
                ddlAllotement.Items.Clear();
                ddlAllotement.Items.Add(new ListItem("Please Select", "0"));
                ddlAllotement.Items.Add(new ListItem("Section", "1"));
                ddlAllotement.Items.Add(new ListItem("Batch", "2"));
                ddlAllotement.Items.Remove(new ListItem("Section & Batch", "3"));
            }
            
            ddlAllotement.SelectedValue = ds.Tables[0].Rows[0]["SEC_BATCH"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "ACTIVE")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(false);", true);
            }
            if (ds.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "YES")
            {
                chkTutorial.Checked = true;
            }
            else
            {
                chkTutorial.Checked = false;
            }
            //if (ddlCondition.SelectedValue == "1" || ddlCondition.SelectedValue == "3")
            //{
            //    divchk.Visible = true;
            //}
            //else
            //{
            //    divchk.Visible = false;
            //}

        }
    }
    protected void lvSubjecttype_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void ddlCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCondition.SelectedValue == "1" || ddlCondition.SelectedValue == "3")
        //{
        //    divchk.Visible = true;
        //    chkTutorial.Checked = false;            
        //}
        //else
        //{
        //    divchk.Visible = false;
        //    chkTutorial.Checked = false;
        //}
        if (ddlCondition.SelectedValue == "3")
        {
            ddlAllotement.Items.Clear();
            ddlAllotement.Items.Add(new ListItem("Please Select", "0"));
            ddlAllotement.Items.Add(new ListItem("Section & Batch", "3"));
        }
        else 
        {
            ddlAllotement.Items.Clear();
            ddlAllotement.Items.Add(new ListItem("Please Select", "0"));
            ddlAllotement.Items.Add(new ListItem("Section", "1"));
            ddlAllotement.Items.Add(new ListItem("Batch", "2"));
            ddlAllotement.Items.Remove(new ListItem("Section & Batch", "3"));
        }
    }
}

