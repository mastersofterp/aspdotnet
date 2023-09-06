//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Grievance Redressal                           
// CREATION DATE : 27-july-2019                                                        
// CREATED BY    : NANCY SHARMA                                   
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
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

public partial class GrievanceRedressal_Master_GrievanceType : System.Web.UI.Page
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
            DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME,GT_CODE", "", "GRIV_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGrievanceType.DataSource = ds;
                lvGrievanceType.DataBind();                
            }
            else
            {
                lvGrievanceType.DataSource = null;
                lvGrievanceType.DataBind();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceType.BindlistView -> " + ex.Message + " " + ex.StackTrace);
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
             objGrivE.GRIEVANCE_TYPE = Convert.ToString(txtGrievance.Text);
             objGrivE.UANO =Convert.ToInt16(Session["userno"]);
             objGrivE.GRIEVANCE_TYPE_CODE = Convert.ToString(txtGRCode.Text);
             if (ViewState["action"] != null)
             {
                 if (txtGrievance.Text == string.Empty)
                 {
                     objCommon.DisplayMessage(this.updActivity, "Please Enter Grievance Type.", this.Page);
                     return;
                 }
                 else
                 {
                     if (ViewState["action"].ToString().Equals("add"))
                     {
                         DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "GT_NAME='" + objGrivE.GRIEVANCE_TYPE + "'", "");
                         if (ds.Tables[0].Rows.Count > 0)
                         {
                             objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                             return;
                         }
                         else
                         {
                             objGrivE.GRIEVANCE_TYPE_ID = 0;
                             CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGrievanceType(objGrivE);
                             if (cs.Equals(CustomStatus.RecordSaved))
                             {
                                 BindlistView();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                                 Clear();
                             }
                         }
                     }
                     else
                     {
                         objGrivE.GRIEVANCE_TYPE_ID = Convert.ToInt32(ViewState["GRIV_ID"]);

                         DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "GT_NAME ='" + objGrivE.GRIEVANCE_TYPE + "' AND GRIV_ID !=" + Convert.ToInt32(ViewState["GRIV_ID"]), "");
                         if (ds.Tables[0].Rows.Count > 0)
                         {
                             objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                             return;
                         }
                         else
                         {
                             CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGrievanceType(objGrivE);
                             if (cs.Equals(CustomStatus.RecordSaved))
                             {
                                 BindlistView();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
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
                 objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceType.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
             int GRIEVANCE_TYPE_ID = int.Parse(btnEdit.CommandArgument);
             ViewState["GRIV_ID"] = int.Parse(btnEdit.CommandArgument);
             ViewState["action"] = "edit";

             ShowDetails(GRIEVANCE_TYPE_ID);

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceType.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }

     private void ShowDetails(int GRIEVANCE_TYPE_ID)
     {
         try
         {
             DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_TYPE", "*", "", "GRIV_ID=" + Convert.ToInt32(ViewState["GRIV_ID"]) + "", "GRIV_ID");
             if (ds.Tables[0].Rows.Count > 0)
             {
                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     txtGrievance.Text = Convert.ToString(dr["GT_NAME"]);
                     txtGRCode.Text = Convert.ToString(dr["GT_CODE"]);
                 }
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceType.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
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
         txtGrievance.Text = string.Empty;
         txtGRCode.Text = string.Empty;
         ViewState["action"] = "add";
         ViewState["GRIV_ID"] = null;
     }


    #endregion




}