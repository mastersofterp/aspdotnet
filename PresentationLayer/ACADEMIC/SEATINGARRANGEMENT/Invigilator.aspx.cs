//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : INVIGILATOR MANAGEMENT[TO UPDATE INVIGILATOR STATUS OR ADD NEW ENTRY]
// CREATION DATE : 16-MAR-2012
// CREATED BY    : PRIYANKA KABADE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_MASTERS_Invigilator : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExmController  = new ExamController();

    #region Page Events
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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindInvigilators();
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
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
        }
    }

    #endregion

    # region Click Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ua_nos = string.Empty;
            string status = string.Empty;
            foreach (ListViewDataItem dataItem in lvFaculty.Items)
            {
                CheckBox chkrow = dataItem.FindControl("chkUa_no") as CheckBox;
                Label lblUa_no = dataItem.FindControl("lblFaculty") as Label;
                ua_nos += lblUa_no.ToolTip.Trim() + "$";
                status += (chkrow.Checked.ToString().ToLowerInvariant() == "true" ? "1" : "0") + "$"; // WHEN 1( CHECKBOX SELECTED) THEN APPLICABLE ,WHEN 0 THEN NOT APPLICABLE
            }

            CustomStatus cs = (CustomStatus)objExmController.Add_Update_Exam_Invigilator_Status(ua_nos, status, Session["colcode"].ToString());
            if (!cs.Equals(CustomStatus.Error))
                objCommon.DisplayMessage("Invigilators Status Updated Successfully!", this.Page);
            else
                objCommon.DisplayMessage("Error Adding Staff!", this.Page);
            this.BindInvigilators();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_Invigilator.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region User Method

    private void BindInvigilators()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("USER_ACC U LEFT OUTER JOIN ACD_EXAM_INVIGILATOR E ON (U.UA_NO = E.UA_NO)", "U.UA_NO", "U.UA_FULLNAME COLLATE DATABASE_DEFAULT + '  ('+ U.UA_NAME COLLATE DATABASE_DEFAULT +') ' AS UA_FULLNAME,ISNULL(E.STATUS,0)as STATUS", "UA_TYPE NOT IN(1,2,6,8) AND U.UA_NO != 0", " UA_NAME");
            int Count = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvFaculty.DataSource = ds;
                    lvFaculty.DataBind();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        Count += Convert.ToInt16((ds.Tables[0].Rows[i]["status"].ToString().ToLower() == "1" ? ds.Tables[0].Rows[i]["status"].ToString() : "0" ));
                    txtTotFaculty.Text = Count.ToString();
                }
                else
                {
                    lvFaculty.DataSource = null;
                    lvFaculty.DataBind();
                }
            }
            else
            {
                lvFaculty.DataSource = null;
                lvFaculty.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_Invigilator.LoadInvigilators() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
}

