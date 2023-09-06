using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS. BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_AdmissionNotice : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objBC = new ConfigController();
    College objCol = new College();
    int NoticeID = 0;
    string Notice = string.Empty;
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
            if (Session["userno"] == null || Session["username"] == null ||
             Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                 //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
            BindListView();
            ViewState["action"] = "add";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Notice = txtNotice.Text;
            objCol.CollegeCode = Session["colcode"].ToString();

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //added on 07-02-2020 by Vaishali
                    int Count = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_NOTICE", "COUNT(*) COUNT", string.Empty));
                    if (Count > 0)
                    {
                        objCommon.DisplayMessage(this.updAdmissionNotice, "Only One Notice is allowed !!!! In case to create new notice, continue with || to the existing notice.", this.Page);
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objBC.AddNotice(objCol, Notice, Convert.ToInt32(Session["OrgId"]));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            //BindListView();
                            objCommon.DisplayMessage(this.updAdmissionNotice, "Record Saved Successfully!", this.Page);
                            Clear();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updAdmissionNotice, "Existing Record", this.Page);
                        }
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["noticeid"] != null)
                    {

                        Notice = txtNotice.Text;
                        NoticeID = Convert.ToInt32(ViewState["noticeid"]);
                        CustomStatus cs = (CustomStatus)objBC.UpdateNotice(Notice, NoticeID);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.updAdmissionNotice, "Record Updated Successfully!", this.Page);
                            Clear();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updAdmissionNotice, "Record already exist", this.Page);
                           
                        }
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
    private void Clear()
    {
        txtNotice.Text = string.Empty;
       // Label1.Text = string.Empty;
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objBC.GetAllNotices(); 
            lvAdmNotice.DataSource = ds;
            lvAdmNotice.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetail(int noticeid)
    {
        NoticeID = noticeid;
        DataSet ds = objBC.GetNoticeDetails(NoticeID);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ViewState["batchno"] = batchno.ToString();
                //ddlSubjectType.Text = dr["SUBID"] == null ? string.Empty : dr["SUBID"].ToString();
                txtNotice.Text = ds.Tables[0].Rows[0]["NOTICE"] == null ? string.Empty : ds.Tables[0].Rows[0]["NOTICE"].ToString();
            }
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int noticeid = int.Parse(btnEdit.CommandArgument);
            
            ShowDetail(noticeid);
            ViewState["action"] = "edit";
            ViewState["noticeid"] = noticeid;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
}