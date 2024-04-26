//=================================================================================
// PROJECT NAME  : COMMON CODE                                                          
// MODULE NAME   : ACADEMIC - Course Activity Master                                           
// CREATION DATE :                                                      
// CREATED BY    :                                            
// MODIFIED BY   :                                                     
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Text.RegularExpressions;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_MASTERS_GradeMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    Exam ObjE = new Exam();
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
                Session["usertype"] == null || Session["userfullname"] == null || Session["OrgId"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //this.CheckPageAuthorization(); 
                Page.Title = Session["coll_name"].ToString();  //Set the Page Title 
            }
            BindListView();
            ViewState["action"] = "add";
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label 
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
        }
    }
    #endregion

    #region btnclick Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GradeController objGC = new GradeController();

            string activityname = txtActivityName.Text.Replace(" ", " ");
            string activity = RemoveExtraSpaces(activityname);

            // string activity = txtActivityName.Text;
            int status = 0;
            if (hfActivity.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            //string ifexists = objCommon.LookUp("ACD_COURSE_ACTIVITY_TYPE_MASTER", "COUNT(1)", "ISACTIVE=" + status + " AND CRS_ACTIVITY_NAME='" + txtActivityName.Text + "'");
            //string ifexists = objCommon.LookUp("ACD_COURSE_ACTIVITY_TYPE_MASTER", "COUNT(1)", "CRS_ACTIVITY_NAME='" + txtActivityName.Text + "'");  
            //if (ifexists == "1")
            //{
            //    objCommon.DisplayMessage(this.Page, "Record already exists", this.Page);
            //    Clear();
            //    return;
            //} 

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objGC.AddCourseActivityMaster(Convert.ToInt32(ViewState["Id"]), activity, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record already exists", this.Page);
                        //Label1.Text = "Record already exist";
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objGC.UpdateCourseActivityMaster(Convert.ToInt32(ViewState["Id"]), activity, status);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ViewState["action"] = null;
                        Clear();
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record already exists", this.Page);
                    }
                }
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public string RemoveExtraSpaces(string input)
    {
        // Remove extra spaces using regular expression
        input = Regex.Replace(input, @"\s+", " ").Trim();
        return input;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["Id"] = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            //Label1.Text = string.Empty;
            int ID = int.Parse(btnEdit.CommandArgument);
            ShowDetail(ID);
            ViewState["Id"] = ID;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COURSE_ACTIVITY_TYPE_MASTER", "CRS_ACT_TYPE_NO,CRS_ACTIVITY_NO,CRS_ACTIVITY_NAME", "CASE WHEN ISNULL(ISACTIVE,0)=0 THEN 'De-Active' ELSE 'Active' END AS ISACTIVE ", "CRS_ACT_TYPE_NO>0", "CRS_ACT_TYPE_NO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvActivityType.DataSource = ds.Tables[0];
                lvActivityType.DataBind();
            }
            else
            {
                lvActivityType.DataSource = null;
                lvActivityType.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int ID)
    {

        DataSet ds = objCommon.FillDropDown("ACD_COURSE_ACTIVITY_TYPE_MASTER", "CRS_ACTIVITY_NAME", "CASE WHEN ISNULL(ISACTIVE,0)=0 THEN 'De-Active' ELSE 'Active' END AS ISACTIVE ", "CRS_ACT_TYPE_NO=" + ID, "CRS_ACT_TYPE_NO");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtActivityName.Text = ds.Tables[0].Rows[0]["CRS_ACTIVITY_NAME"].ToString() == null ? "0" : ds.Tables[0].Rows[0]["CRS_ACTIVITY_NAME"].ToString();
            if (ds.Tables[0].Rows[0]["ISACTIVE"].ToString() == "Active" || ds.Tables[0].Rows[0]["ISACTIVE"].ToString() == "ACTIVE")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslot(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslot(false);", true);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            return;
        }
    }

    protected void Clear()
    {
        txtActivityName.Text = string.Empty;
        hfActivity.Value = "false";
        txtActivityName.Focus();
    }
}

