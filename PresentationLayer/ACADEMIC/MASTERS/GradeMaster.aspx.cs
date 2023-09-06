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
using System.Collections.Generic;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;
//using IITMS.UAIMS.BusinessLayer;
//using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_MASTERS_GradeMaster : System.Web.UI.Page
{
    #region Page Events
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
            //objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID>0", "SUBID");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GradeController objGC = new GradeController();
            Grade objGrade = new Grade();
            //objBatch.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            //objBatch.BatchName = txtBatchName.Text.Trim();
            //objBatch.CollegeCode = Session["colcode"].ToString();
            //objGrade.GradeType = Convert.ToInt32(txtGradeType.Text);
            objGrade.GradeName = txtGradeName.Text;
            objGrade.CollegeCode = Session["colcode"].ToString();

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Batch
                    CustomStatus cs = (CustomStatus)objGC.AddGrade(objGrade);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updGrade, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
                        Label1.Text = "Record already exist";
                    }
                }
                else
                {
                    //Edit
                    //if (ViewState["GRADE_TYPE"] != null)
                    //{
                    //    objGrade.GradeType = Convert.ToInt32(ViewState["GRADE_TYPE"].ToString());

                        CustomStatus cs = (CustomStatus)objGC.UpdateGrade(objGrade);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updGrade, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            Label1.Text = "Record already exist";
                        }
                    //}
                }

                BindListView();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void Clear()
    {
        txtGradeName.Text = string.Empty;
        //txtGradeType.Text = string.Empty;
        Label1.Text = string.Empty;

    }
    private void BindListView()
    {
        try
        {
            GradeController objGC = new GradeController();
            DataSet ds = objGC.GetAllGrade();
            lvGradeType.DataSource = ds;
            lvGradeType.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }
    private void ShowDetail(int gradeNo)
    {
        GradeController objGC = new GradeController();
        SqlDataReader dr = objGC.GetGradeTypeNo(gradeNo);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["batchno"] = gradeNo.ToString();
                //ddlSubjectType.Text = dr["SUBID"] == null ? string.Empty : dr["SUBID"].ToString();
                //txtBatchName.Text = dr["BATCHNAME"] == null ? string.Empty : dr["BATCHNAME"].ToString();
                //txtGradeType.Text = dr["GRADE_TYPE"] == null ? string.Empty : dr["GRADE_TYPE"].ToString();
                txtGradeName.Text = dr["GRADE_TYPE_NAME"] == null ? string.Empty : dr["GRADE_TYPE_NAME"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int gradeNo = int.Parse(btnEdit.CommandArgument);
            Label1.Text = string.Empty;

            ShowDetail(gradeNo);
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
}

