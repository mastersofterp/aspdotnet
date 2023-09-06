// ========================================================
// CREATE BY   : MRUNAL SINGH
// CREATE DATE : 06-JUN-2017
// DESCRIPTION : USED TO GIVE APPROVAL FOR MEETING AGENDAS
// MODIFY DATE :    
// =========================================================

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class MEETING_MANAGEMENT_TRANSACTION_AgendaApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController OBJmc = new MeetingController();

    public static int pk_agenda_id;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public int InsertUpdate = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_RELETIONMASTER RM INNER JOIN TBL_MM_MENBERDETAILS MD ON (RM.FK_MEMBER = MD.PK_CMEMBER) INNER JOIN Tbl_MM_COMITEE MC ON (RM.FK_COMMITEE = MC.ID)", "FK_COMMITEE", "MC.NAME", " MD.USERID != 0 AND MD.USERID = " + Convert.ToInt32(Session["idno"]), "");                                       
                } 
                //objMM.LOCK = 'N';
                //objMM.TABLE_ITEM = 'N';
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaApproval.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    // This method is used to bind the list View.
    private void BindlistView()
    {
        try
        {
            DataSet ds = OBJmc.GetAgendaToBind(Convert.ToInt32(ddlCommitee.SelectedValue), ddlpremeeting.SelectedItem.Text, Convert .ToInt32(Session["idno"]));         
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lvAgenda.DataSource = ds;
                    lvAgenda.DataBind();
                    pnlList.Visible = true;
                 //   pnlButtons.Visible = true;
                }
             
                lblMDate.Text = ds.Tables[0].Rows[0]["MEETINGDATE"].ToString();
                lblMVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
                lblMTime.Text = ds.Tables[0].Rows[0]["MEETINGTIME"].ToString();
                InsertUpdate = Convert.ToInt32(ds.Tables[0].Rows[0]["Exist"].ToString());

                if (InsertUpdate == 1)
                {
                    ViewState["action"] = "edit";
                }
                else
                {
                    ViewState["action"] = "add";
                }

                trDate.Visible = true;
                trTime.Visible = true;               
            }
            else
            {
                lvAgenda.DataSource = null;
                lvAgenda.DataBind();
                trDate.Visible = false;
                trTime.Visible = false;
                pnlList.Visible = false;
              //  pnlButtons.Visible = false;
                

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaApproval.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) , "");
        
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.Items.Add("Please Select");
                ddlpremeeting.SelectedItem.Value = "0";
                ddlpremeeting.DataTextField = "MEETING_CODE";
                ddlpremeeting.DataValueField = "MEETING_CODE";
                ddlpremeeting.DataSource = ds.Tables[0];
                ddlpremeeting.DataBind();
                ddlpremeeting.SelectedIndex = 0;
            }
            else
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.DataSource = null;
                ddlpremeeting.DataBind();
            }    
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objMM.APPROVAL_ID = 0;
            objMM.FK_MEETING_ID = Convert.ToInt32(ddlCommitee.SelectedValue);
            objMM.MEETING_CODE = ddlpremeeting.SelectedItem.Text;
            objMM.USERID = Convert.ToInt32(Session["idno"]);


            DataTable RemarkTbl = new DataTable("RemTbl");
            RemarkTbl.Columns.Add("PK_AGENDA", typeof(int));           
            RemarkTbl.Columns.Add("REMARK", typeof(string));

            DataRow dr = null;
            foreach (ListViewItem i in lvAgenda.Items)
            {
                HiddenField HdnAgendaNo = (HiddenField)i.FindControl("hdnPK_AGENDA");
                TextBox txtRemark = (TextBox)i.FindControl("txtRemark");
               
                dr = RemarkTbl.NewRow();
                dr["PK_AGENDA"] = HdnAgendaNo.Value;               
                dr["REMARK"] = txtRemark.Text;

                RemarkTbl.Rows.Add(dr);                
            }

            objMM.REMARK_TBL = RemarkTbl;


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)OBJmc.AddUpdateApprovalRemark(objMM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {                     
                        BindlistView();
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";                      
                        BindlistView();
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("edit"))
                    {

                        objMM.APPROVAL_ID = 1;
                        CustomStatus cs = (CustomStatus)OBJmc.AddUpdateApprovalRemark(objMM);                       
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            BindlistView();                           
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaApproval.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCommitee.SelectedIndex = 0;
        ddlpremeeting.SelectedIndex = 0;
    }
    protected void ddlpremeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindlistView();
    }
}