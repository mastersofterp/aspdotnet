using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
using System.Text;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS.NITPRM;
using System.Web.UI.HtmlControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class TRAININGANDPLACEMENT_TP_Admin_Lock_Unlock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TPTraining objTPT = new TPTraining();
    TrainingPlacement objTP = new TrainingPlacement();
    BlobController objBlob = new BlobController();

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
                    this.CheckPageAuthorization(); 

                    Page.Title = Session["coll_name"].ToString();


                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";

                    objCommon.FillDropDownList(ddlStudent, "ACD_TP_STUDENT_REGISTRATION A INNER JOIN ACD_STUDENT B ON (A.IDNO=B.IDNO)", "A.IDNO", "CONCAT(B.STUDNAME, '_', B.REGNO) AS full_name", "REGSTATUS = 'Y'", "");
                  
                }


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TP_Reg_Approval.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TP_Reg_Approval.aspx");

        }
    }

    protected bool GetStatus(object value)
    {
        if (value == null) return false;
        int intValue = (int)value;
        return intValue == 1;
    }
    protected void Show_Click(object sender, EventArgs e)
    {
        if (ddlStudent.SelectedValue=="0")
        {
            objCommon.DisplayMessage(this.Page, "Please select TP Student.", this.Page);
            return;
        }
        DataSet DS = objCommon.FillDropDown("ACD_TP_STUDENT_REGISTRATION A INNER JOIN ACD_STUDENT B ON (A.IDNO=B.IDNO)", "A.IDNO,A.REGNO", "B.STUDNAME,ISNULL(EXAM_LOCK_UNLOCK,0) EXAM_LOCK_UNLOCK,isnull(WORK_EXP_LOCK_UNLOCK,0) WORK_EXP_LOCK_UNLOCK,ISNULL(TECH_SKIL_LOCK_UNLOCK,0) TECH_SKIL_LOCK_UNLOCK,ISNULL(PROJECT_LOCK_UNLOCK,0) PROJECT_LOCK_UNLOCK,ISNULL(CERTIFICATION_LOCK_UNLOCK,0) CERTIFICATION_LOCK_UNLOCK ,ISNULL(LANGUAGE_LOCK_UNLOCK,0) LANGUAGE_LOCK_UNLOCK,ISNULL(AWARD_LOCK_UNLOCK,0) AWARD_LOCK_UNLOCK,ISNULL(COMPETITION_LOCK_UNLOCK,0) COMPETITION_LOCK_UNLOCK,ISNULL(TRAINING_LOCK_UNLOCK,0) TRAINING_LOCK_UNLOCK,ISNULL(TEST_SCORE_LOCK_UNLOCK,0) TEST_SCORE_LOCK_UNLOCK,ISNULL(BUILD_RESUME_LOCK_UNLOCK,0) BUILD_RESUME_LOCK_UNLOCK", "A.IDNO='" + Convert.ToInt32(ddlStudent.SelectedValue) + "'", "");
        lvlockunlock.DataSource = DS;
        lvlockunlock.DataBind();
        lvlockunlock.Visible = true;

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            TPStudent objtpstudent = new TPStudent();
            TPScheNewController objTPSchont = new TPScheNewController();
            foreach(ListViewDataItem lvitem in lvlockunlock.Items)
            {
            HiddenField hdidno= lvitem.FindControl("hdidno") as HiddenField;
            CheckBox Exmchk = lvitem.FindControl("Exmchk") as CheckBox;
            CheckBox WorkExpchk = lvitem.FindControl("WorkExpchk") as CheckBox;
            CheckBox TechSkilchk = lvitem.FindControl("TechSkilchk") as CheckBox;
            CheckBox Projectchk = lvitem.FindControl("Projectchk") as CheckBox;
            CheckBox certificationschk = lvitem.FindControl("certificationschk") as CheckBox;
            CheckBox langchk = lvitem.FindControl("langchk") as CheckBox;
            CheckBox awrdchk = lvitem.FindControl("awrdchk") as CheckBox;
            CheckBox comptchk = lvitem.FindControl("comptchk") as CheckBox;
            CheckBox trngchk = lvitem.FindControl("trngchk") as CheckBox;
            CheckBox tessctchk = lvitem.FindControl("tessctchk") as CheckBox;
            CheckBox BildReschk = lvitem.FindControl("BildReschk") as CheckBox;
            objtpstudent.IdNo = Convert.ToInt32(hdidno.Value);
                if (Exmchk.Checked == true)
                {
                    objtpstudent.EXAM_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.EXAM_LOCK_UNLOCK = 0;
                }
                if (WorkExpchk.Checked == true)
                {
                    objtpstudent.WORK_EXP_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.WORK_EXP_LOCK_UNLOCK = 0;
                }
                if (TechSkilchk.Checked == true)
                {
                    objtpstudent.TECH_SKIL_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.TECH_SKIL_LOCK_UNLOCK = 0;
                }
                if (Projectchk.Checked == true)
                {
                    objtpstudent.PROJECT_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.PROJECT_LOCK_UNLOCK = 0;
                }
                if (certificationschk.Checked == true)
                {
                    objtpstudent.CERTIFICATION_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.CERTIFICATION_LOCK_UNLOCK = 0;
                }
                if (langchk.Checked == true)
                {
                    objtpstudent.LANGUAGE_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.LANGUAGE_LOCK_UNLOCK = 0;
                }
                if (awrdchk.Checked == true)
                {
                    objtpstudent.AWARD_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.AWARD_LOCK_UNLOCK = 0;
                }
                if (comptchk.Checked == true)
                {
                    objtpstudent.COMPETITION_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.COMPETITION_LOCK_UNLOCK = 0;
                }
                if (trngchk.Checked == true)
                {
                    objtpstudent.TRAINING_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.TRAINING_LOCK_UNLOCK = 0;
                }
                if (tessctchk.Checked == true)
                {
                    objtpstudent.TEST_SCORE_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.TEST_SCORE_LOCK_UNLOCK = 0;
                }
                if (BildReschk.Checked == true)
                {
                    objtpstudent.BUILD_RESUME_LOCK_UNLOCK = 1;
                }
                else
                {
                    objtpstudent.BUILD_RESUME_LOCK_UNLOCK = 0;
                }
                count++;
                CustomStatus cs = (CustomStatus)objTPSchont.UpdateStudentLockUnlock(objtpstudent);


                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Records Update Successfully..!", this.Page);
                    ddlStudent.SelectedValue = "0";
                    lvlockunlock.DataSource = null;
                    lvlockunlock.DataBind();
                    lvlockunlock.Visible = false;

                }
            }

            if(count==0)
            {
                objCommon.DisplayMessage(this.Page, "Please show TP Student.", this.Page);
                return;
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_TP_Admin_Lock_Unlock.btnSubmit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        ddlStudent.SelectedValue = "0";
        lvlockunlock.DataSource = null;
        lvlockunlock.DataBind();
        lvlockunlock.Visible = false;

    }
}