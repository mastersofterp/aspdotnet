using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Faculty_DisciplineMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    CourseController objCc = new CourseController();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void BindListView()
    {
        try
        {

            DataSet ds = objCc.GetFacultyDisciplineList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelFaculty.Visible = true;
                lvFacultyMaster.DataSource = ds.Tables[0];
                lvFacultyMaster.DataBind();
            }
            else
            {
                PanelFaculty.Visible = true;
                lvFacultyMaster.DataSource = null;
                lvFacultyMaster.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvFacultyMaster.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.ToolTip);

                if (Statuss == "0")
                {
                  //  Status.CssClass = "badge badge-danger";
                    Status.Text = "INACTIVE";
                }
                else
                {
                    //Status.CssClass = "badge badge-success";
                    Status.Text = "ACTIVE";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Faculty_DisciplineMaster.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ClearData()
    {
        txtFaculty.Text = "";
     
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        string status;
        string faculty_name = txtFaculty.Text.Trim();
        if (hfdActive.Value == "true")
        {
            status = "1";
        }
        else
        {
            status = "0";
        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            int id = Convert.ToInt32(ViewState["id"]);

            CustomStatus cs = (CustomStatus)objCc.UpdateFacultyDisciplineData(id, faculty_name, status);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearData();
                BindListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ViewState["action"] = null;
            }
        }
        else
        {
            //Edit 
            CustomStatus cs = (CustomStatus)objCc.InsertFacultyDisciplineData(faculty_name, status);
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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(ID);
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

    private void ShowDetail(int ID)
    {

        DataSet ds = objCc.EditFacultyDiscplineData(ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtFaculty.Text = ds.Tables[0].Rows[0]["FACULTY_NAME"].ToString();          
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(false);", true);
            }


        }
    }
    protected void lvFacultyMaster_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
}