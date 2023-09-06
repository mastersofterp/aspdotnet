using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;

public partial class GrievanceRedressal_Master_GrievanceRedressalCommittee : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();

    #region
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
                    BindlistView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceType.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region
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
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

     private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("GRIV_GR_COMMITTEE_TYPE", "GRCT_ID", "GR_COMMITTEE", "", "GRCT_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCommittee.DataSource = ds;
                lvCommittee.DataBind();                
            }
            else
            {
                lvCommittee.DataSource = null;
                lvCommittee.DataBind();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceRedressalCommittee.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region

     protected void btnSubmit_Click(object sender, EventArgs e)
     {
         try
         {
             objGrivE.COMMITTEE_TYPE = Convert.ToString(txtCommitteeType.Text).Trim() ;
             objGrivE.DEPT_FLAG=Convert.ToInt32(chkCommitteeTypeDept.Checked);           
             
             objGrivE.UANO =Convert.ToInt16(Session["userno"]);
             if (ViewState["action"] != null)
             {
                 if (txtCommitteeType.Text == string.Empty)
                 {
                     objCommon.DisplayMessage(this.updRedressalCommittee, "Please Enter Committee Type.", this.Page);
                     return;
                 }
                 else
                 {
                     if (ViewState["action"].ToString().Equals("add"))
                     {
                         DataSet ds = objCommon.FillDropDown("GRIV_GR_COMMITTEE_TYPE", "GRCT_ID", "GR_COMMITTEE", "GR_COMMITTEE='" + objGrivE.COMMITTEE_TYPE + "'", "");
                         if (ds.Tables[0].Rows.Count > 0)
                         {
                             objCommon.DisplayMessage(this.updRedressalCommittee, "Record Already Exist", this.Page);
                             return;
                         }
                         
                         else
                         {
                             objGrivE.COMMITTEE_TYPE_ID = 0;
                             CustomStatus cs = (CustomStatus)objGrivC.AddUpdateRedressalCommitteeType(objGrivE);
                             if (cs.Equals(CustomStatus.RecordSaved))
                             {
                                 BindlistView();                                
                                 objCommon.DisplayMessage(this.updRedressalCommittee, "Record Saved Successfully.", this.Page);
                                 Clear();
                             }
                         }
                     }
                     else
                     {
                         objGrivE.COMMITTEE_TYPE_ID = Convert.ToInt32(ViewState["GRCT_ID"]);

                         DataSet ds = objCommon.FillDropDown("GRIV_GR_COMMITTEE_TYPE", "GRCT_ID", "GR_COMMITTEE", "GR_COMMITTEE ='" + objGrivE.COMMITTEE_TYPE + "' AND GRCT_ID !=" + Convert.ToInt32(ViewState["GRCT_ID"]), "");
                         if (ds.Tables[0].Rows.Count > 0)
                         {
                             objCommon.DisplayMessage(this.updRedressalCommittee, "Record Already Exist.", this.Page);
                             return;
                         }
                         else
                         {
                             CustomStatus cs = (CustomStatus)objGrivC.AddUpdateRedressalCommitteeType(objGrivE);
                             if (cs.Equals(CustomStatus.RecordSaved))
                             {
                                 BindlistView();                                 
                                 objCommon.DisplayMessage(this.updRedressalCommittee, "Record Updated Successfully.", this.Page);
                                 Clear();
                             }
                         }
                     }
                 }
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceRedressalCommittee.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }

     }

     protected void btnEdit_Click(object sender, EventArgs e)
     {
         try
         {
             //msgcomp.Visible = true;
             ImageButton btnEdit = sender as ImageButton;
             int COMMITTEE_TYPE_ID = int.Parse(btnEdit.CommandArgument);
             ViewState["GRCT_ID"] = int.Parse(btnEdit.CommandArgument);
             ViewState["action"] = "edit";
             ShowDetails(COMMITTEE_TYPE_ID);

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceRedressalCommittee.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }

     private void ShowDetails(int COMMITTEE_TYPE_ID)
     {
         try
         {
             DataSet ds = objCommon.FillDropDown("GRIV_GR_COMMITTEE_TYPE", "GRCT_ID,GR_COMMITTEE", "ISNULL(DEPT_FLAG,0) as DEPT_FLAG ,SRNO", "GRCT_ID=" + Convert.ToInt32(ViewState["GRCT_ID"]) + "", "GRCT_ID");
             if (ds.Tables[0].Rows.Count > 0)
             {
                 txtCommitteeType.Text = ds.Tables[0].Rows[0]["GR_COMMITTEE"].ToString();
                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["DEPT_FLAG"]) == 1)
                 {
                     chkCommitteeTypeDept.Checked = true;
                 }
                 else
                 {
                    chkCommitteeTypeDept.Checked = false;
                 }                
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceRedressalCommittee.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }

     protected void btnCancel_Click(object sender, EventArgs e)
     {
         Clear();
     }

     private void Clear()
     {
         txtCommitteeType.Text = string.Empty;
         ViewState["action"] = "add";
         ViewState["GRCT_ID"] = null;
         chkCommitteeTypeDept.Checked = false;         
     }


    #endregion








}