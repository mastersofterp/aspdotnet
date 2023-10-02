using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Registration_Grade_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
               
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                listViewBind();
            }
            //Populate the Drop Down Lists
            populateDropDown();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        ddlActivityName.Focus();
    }

    protected void clear()
    {
        ddlActivityName.SelectedIndex = 0;
        ddlGrade.ClearSelection();

        btnSave.Text = "Submit";

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            ExamController objSv = new ExamController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.Exam objSession = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Exam();

            objSession.ActivityNo = int.Parse(ddlActivityName.SelectedValue);
            if (objSession.ActivityNo == 0)
            {
                objCommon.DisplayMessage(updGrade, "Please Select Activity Name", this.Page);
                return;
            }

            int count = 0;
            string grade = string.Empty;

            foreach (ListItem Item in ddlGrade.Items)
            {
                if (Item.Selected)
                {
                    //grade += Item.Text + ",";
                    grade += (Item.Text).Split('-')[0] + ',';
                    count++;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updGrade, "Please Select Atleast One Grade", this.Page);
                return;
            }
            objSession.Grade = grade.Substring(0, grade.Length - 1);

            int status = (hfActive.Value == "true") ? 1 : 0;


            int Orgid = Convert.ToInt32(objCommon.LookUp("reff", "OrganizationId", ""));



            // edit

            if (btnSave.Text == "Update")
            {
                CustomStatus cs = (CustomStatus)objSv.AddGrade(objSession, Orgid, status);

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updGrade, "Record Update Sucessfully", this.Page);
                    listViewBind();
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objSv.AddGrade(objSession, Orgid, status);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updGrade, "Record Added Sucessfully", this.Page);
                    listViewBind();
                    clear();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updGrade, "Record Already Exist", this.Page);
                }
                else
                {
                    //msgLbl.Text = "Record already exist";
                    objCommon.DisplayMessage(this.updGrade, "Record Already Exist", this.Page);
                }
            }
            listViewBind();
            clear();
            btnSave.Text = "Submit";
            ddlActivityName.Focus();
        }
        catch (Exception ex)
        {
            throw ex;
        }
       
    }

    private string getorgId()
    {
        string orgId = objCommon.LookUp("reff", "OrganizationId", "");
        return orgId;
    }

    protected void populateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlActivityName, "ACD_COURSE_ACTIVITY_TYPE_MASTER", "CRS_ACT_TYPE_NO", "CRS_ACTIVITY_NAME", "ISNULL(ISACTIVE,0)=1", "");
            ddlActivityName.Focus();
            objCommon.FillListBox(ddlGrade, "ACD_GRADE_NEW", "GRADENO", "GRADE", "GRADENO>0 AND ACTIVESTATUS=1", "GRADENO");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void listViewBind()
    {
        try
        {
            ExamController objsession = new ExamController();
            DataSet ds = objsession.GetAllActivityName();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //pnlSession.Visible = true;
                lvSession.DataSource = ds;
                lvSession.DataBind();
            }

            else
            {
                //pnlSession.Visible = false;
                lvSession.DataSource = ds;
                lvSession.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
  
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ActivityNo = int.Parse(btnEdit.CommandArgument);
            Session["ACTIVITY_NO"] = int.Parse(btnEdit.CommandArgument);
            this.showDetails(ActivityNo);
            btnSave.Text = "Update";
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public void showDetails(int ActivityNo)
    {
        try
        {
            ExamController objsv = new ExamController();
            SqlDataReader dr = objsv.GetSingleSession(ActivityNo);

            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlActivityName.SelectedValue = (dr["ACTIVITY_NO"].ToString());
                    hfActive.Value = dr["ACTIVESTATUS"].ToString() == "0" ? "false" : "true";

                    if (dr["ACTIVESTATUS"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                    }

                    foreach (ListItem Item in ddlGrade.Items)
                    {
                        if (dr["GRADE"].ToString().Contains(Item.Text))
                        {
                            Item.Selected = true;

                        }
                    }
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlActivityName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGrade.ClearSelection();
    }
}


