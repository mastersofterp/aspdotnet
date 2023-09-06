using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_ExamTypeMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamTypeController ExmCon = new ExamTypeController();

    String ActiveStatus;
    String ExamName;
    String CollegeCode;

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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }

            BindListView();
            ViewState["action"] = "add";
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

        #region Button Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                 ExamName = txtExamtype.Text.Trim();
                 CollegeCode = Session["colcode"].ToString();
                if (hfExamtype.Value == "true")
                {
                    ActiveStatus = "1";
                }
                else
                {
                    ActiveStatus = "0";
                }

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        CustomStatus cs = (CustomStatus)ExmCon.AddExamType(ExamName, CollegeCode, ActiveStatus);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updGrade, "Record Saved Successfully!", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this.updGrade, "Record Already Exists", this.Page);

                        }
                    }
                    else 
                    {
                        int Examno = Convert.ToInt32(ViewState["ExamTypeNo"].ToString());
                        CustomStatus cs = (CustomStatus)ExmCon.UpdateExamType(ExamName, ActiveStatus, CollegeCode, Examno);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //ViewState["action"] = null;
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updGrade, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updGrade, "Record Already Exists Use Different Name", this.Page);
                            
                        }
                    }
                }
                BindListView();
                Clear();
            }catch(Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ExamTypeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btnEdit = sender as ImageButton;
                int ExamTypeNo = int.Parse(btnEdit.CommandArgument);
                Label1.Text = string.Empty;

                ShowDetail(ExamTypeNo);
                ViewState["action"] = "edit";
                ViewState["ExamTypeNo"] = ExamTypeNo;

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ExamTypeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            ViewState["action"] = "add";
        }
        #endregion

        #region Methods
        protected void Clear()
        {
            txtExamtype.Text = string.Empty;
            Label1.Text = string.Empty;

        }

        private void ShowDetail(int ExamTypeNo)
        {

            SqlDataReader dr = ExmCon.GetExamTypeNo(ExamTypeNo);

            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["batchno"] = ExamTypeNo.ToString();
                    txtExamtype.Text = dr["EXAM_TYPE"] == null ? string.Empty : dr["EXAM_TYPE"].ToString();

                    if (dr["ACTIVESTATUS"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslot(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslot(false);", true);
                    }
                }
            }
            if (dr != null) dr.Close();
        }

        private void BindListView()
        {
            try
            {
                DataSet ds = ExmCon.GetAllExamType();
                lvExamtype.DataSource = ds.Tables[0];
                lvExamtype.DataBind();
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ExamTypeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        #endregion
}