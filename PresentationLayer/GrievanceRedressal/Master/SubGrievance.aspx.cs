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

public partial class GrievanceRedressal_Master_SubGrievance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();

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
                    
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    FillDropDown();
                    BindlistView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_SubGrievance.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlGriv, "GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "GRIV_ID>0", "GRIV_ID");
        }
         catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_SubGrievance.FillDropDown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objGrivE.GRIEVANCE_TYPE_ID = Convert.ToInt32(ddlGriv.SelectedValue);
            objGrivE.UANO = Convert.ToInt16(Session["userno"]);
            objGrivE.GRIV_SUB_TYPE = Convert.ToString(txtSubGriv.Text);
            
            if (ViewState["action"] != null)
            {
                if (txtSubGriv.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Enter Grievance Type.", this.Page);
                    return;
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        DataSet ds = objCommon.FillDropDown("GRIV_SUB_GRIEVANCE_TYPE", "SUB_ID", "SUBGTNAME", "SUBGTNAME='" + objGrivE.GRIV_SUB_TYPE + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                            return;
                        }
                        else
                        {
                            objGrivE.GRIV_SUB_ID = 0;
                            CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSUBGrievanceType(objGrivE);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                MessageBox("Record Saved Successfully.");
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        objGrivE.GRIV_SUB_ID = Convert.ToInt32(ViewState["SUB_ID"]);

                        DataSet ds = objCommon.FillDropDown("GRIV_SUB_GRIEVANCE_TYPE", "SUB_ID", "SUBGTNAME", "SUBGTNAME ='" + objGrivE.GRIV_SUB_TYPE + "' AND SUB_ID !=" + Convert.ToInt32(ViewState["SUB_ID"]), "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSUBGrievanceType(objGrivE);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                MessageBox("Record Updated Successfully.");
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //msgcomp.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int SUB_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["SUB_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";

            ShowDetails(SUB_ID);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_GrievanceType.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int SUB_ID)
    {
        DataSet ds = null;
         try
         {
             ds = objGrivC.GetSingleSubType(SUB_ID);
           
             if (ds.Tables[0].Rows.Count > 0)
             {
                 ViewState["SUB_ID"] = SUB_ID.ToString();
                 ddlGriv.SelectedValue = ds.Tables[0].Rows[0]["GRIV_ID"].ToString();
                 txtSubGriv.Text = ds.Tables[0].Rows[0]["SUBGTNAME"].ToString();
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

    private void Clear()
    {
        txtSubGriv.Text = string.Empty;
        ddlGriv.SelectedValue = "0";
        ViewState["action"] = "add";
        ViewState["SUB_ID"] = null;
    }

    private void BindlistView()
    {
        try
        {
            DataSet ds = objGrivC.GetAllSubGriv();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSubGrivType.DataSource = ds;
                lvSubGrivType.DataBind();
            }
            else
            {
                lvSubGrivType.DataSource = null;
                lvSubGrivType.DataBind();
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

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

}