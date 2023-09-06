using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
using System.Configuration;
using System.Data;

public partial class RFC_CONFIG_Masters_DefineSpecialization : System.Web.UI.Page
{
    CourseController objCc = new CourseController();
    Course objCe = new Course();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.DepartmentController objBC = new IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.DepartmentController();
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
                Session["actionsp"] = "addsp";
            }

            BindListViewSpecialisation();
            objCommon.FillDropDownList(ddlSpecialisation, "ACD_SPECIALISATION", "SPECIALISATIONNO", "SPECIALISATION_NAME", "SPECIALISATIONNO >0", "SPECIALISATIONNO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO >0", "DEGREENO");
            objCommon.FillDropDownList(ddlKnowledgepartner, "ACD_KNOWLEDGE_PARTNER", "KNOWLEDGE_PARTNER_NO", "KNOWLEDGE_PARTNER", "KNOWLEDGE_PARTNER_NO >0", "KNOWLEDGE_PARTNER");
       // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN  ACD_COLLEGE_DEGREE_BRANCH DB ON (B.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0", "B.LONGNAME");
    }

       
    }


    protected void btnSpSubmit_Click(object sender, EventArgs e)
    {
        objCe.degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        objCe.branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        //int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        objCe.specialisationno = Convert.ToInt32(ddlSpecialisation.SelectedValue);
        objCe.Knowledge_partner =Convert.ToInt32(ddlKnowledgepartner.SelectedValue);
        objCe.intake = Convert.ToInt32(txtIntake.Text);


        if (hfSpstatus.Value == "true")
        {
            objCe.status = true;
        }
        else
        {
            objCe.status = false;
        }
        objCe.OrgId = Convert.ToInt32(Session["OrgId"]);

        if (Session["actionsp"].ToString().Equals("addsp"))
        {

            CustomStatus cs = (CustomStatus)objCc.InsertSpecialisationData(objCe);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListViewSpecialisation();
               // ClearEventPraticipationData();
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully..", this.Page);
                clear();
                //ViewState["actionsp"] = "addsp";
                //return;
            }
             //CustomStatus css = (CustomStatus)objCc.InsertSpecialisationData(objCe);
            if (cs.Equals(CustomStatus.RecordExist)) 
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exists..", this.Page);
            }

        }

        else
        {
            objCe.Special_Map_No = Convert.ToInt32(Session["std"]);
            CustomStatus cs = (CustomStatus)objCc.InsertSpecialisationData(objCe);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.Page, "Record Updated sucessfully..", this.Page);
            }

             BindListViewSpecialisation();
            ClearEventPraticipationData();
            //return;
            clear();
            //ViewState["actionsp"] = "editsp";
            Session["actionsp"] = "addsp";
        }



    }
   
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND B.ACTIVESTATUS=1", "LONGNAME");

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int specialisationno = Convert.ToInt32(btnEdit.CommandArgument);
            Session["std"] = specialisationno;
            ShowDetailSpecialization(specialisationno);
            Session["actionsp"] = "editsp";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StudentInformation.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }




    private void ClearEventPraticipationData()
    {
        Session["actionsp"] = "addsp";
    }

    private void BindListViewSpecialisation()
    {
        try
        {
            DataSet ds = objCc.GetSpecialisationList(objCe);
            lvSpecialization.DataSource = ds;
            lvSpecialization.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSpecialization);//Set label -
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private void ShowDetailSpecialization(int specialisationno)
    {
        DataSet ds = objCc.EditSpecialisation(specialisationno);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //objCommon.FillDropDownList(ddlBranch, " ACD_BRANCH" , "DISTINCT BRANCHNO", "LONGNAME", "BRANCHNO>0 ", "LONGNAME");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND B.ACTIVESTATUS=1", "LONGNAME");

            ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            objCommon.FillDropDownList(ddlSpecialisation, "ACD_SPECIALISATION", "SPECIALISATIONNO", "SPECIALISATION_NAME", "SPECIALISATIONNO >0", "SPECIALISATIONNO");

            ddlSpecialisation.SelectedValue = ds.Tables[0].Rows[0]["SPECIALISATIONNO"].ToString();
            
            objCommon.FillDropDownList(ddlKnowledgepartner, "ACD_KNOWLEDGE_PARTNER", "KNOWLEDGE_PARTNER_NO", "KNOWLEDGE_PARTNER", "KNOWLEDGE_PARTNER_NO >0", "KNOWLEDGE_PARTNER_NO");
            ddlKnowledgepartner.SelectedValue = ds.Tables[0].Rows[0]["KNOWLEDGE_PARTNER_NO"].ToString();
            txtIntake.Text = ds.Tables[0].Rows[0]["INTAKE"].ToString();

            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "True")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "setdefinesp(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setdefinesp(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setdefinesp(false);", true);
            }
        } 
    }

    public void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlKnowledgepartner.SelectedIndex = 0;
        ddlSpecialisation.SelectedIndex = 0;
        txtIntake.Text = string.Empty;

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
    }
}