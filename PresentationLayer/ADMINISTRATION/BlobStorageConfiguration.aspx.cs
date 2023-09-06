using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_BlobStorageConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int BlobID;
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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                BindListView();
            }
            ViewState["action"] = "add";
            objCommon.FillDropDownList(ddlActivity, "ACD_BLOB_STORAGE_ACTIVITY", "ACTIVITYNO", "ACTIVITY_NAME", "ACTIVITYNO>0 AND ACTIVE_STATUS=1", "ACTIVITYNO");
            objCommon.FillDropDownList(ddlInstances, "ACD_INSTANCES", "INSTANCENO", "INSTANCE_NAME", "", "");
        }
    }

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetAllBlobConfiguration();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlBlobConfig.Visible = true;
                lvBlobConfig.DataSource = ds;
                lvBlobConfig.DataBind();
            }
            else
            {
                pnlBlobConfig.Visible = false;
                lvBlobConfig.DataSource = null;
                lvBlobConfig.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BlobStorageConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BlobStorageConfiguration.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int IsActive = 0;
        try
        {
            StudentController objSC = new StudentController();
            string accountname = txtAccountName.Text.ToString();
            string secretkey = txtSecretKey.Text.ToString();
            string containername = txtContainerName.Text.ToString();
            int activity = Convert.ToInt32(ddlActivity.SelectedValue);
            int instances = Convert.ToInt32(ddlInstances.SelectedValue);
            string blobstoragePath = txtBlobStoragePath.Text.ToString();

            if (hfdStat.Value == "true")
            {
                IsActive = 1;
            }
            else
            {
                IsActive = 0;

            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Batch
                    CustomStatus cs = (CustomStatus)objSC.AddBlobConfig(accountname, secretkey, containername, instances, activity, IsActive, blobstoragePath); // Modified by Vinay Mishra on 26/06/2023 - blobstoragePath
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.updBlobConfig, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        this.ClearControls();
                        objCommon.DisplayMessage(this.updBlobConfig, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updBlobConfig, "Error Adding Blob Configuration!", this.Page);
                    }
                }
                else
                {
                    int blobid = Convert.ToInt32(ViewState["BlobID"]);
                    CustomStatus cs = (CustomStatus)objSC.UpdateBlobConfig(accountname, secretkey, containername, instances, activity, IsActive, blobid, blobstoragePath); // Modified by Vinay Mishra on 26/06/2023 - blobstoragePath

                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.updBlobConfig, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updBlobConfig, "Record Updated Successfully!", this.Page);
                        this.ClearControls();
                        btnSave.Text = "Submit";

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updBlobConfig, "Error Adding Blob Configuration!", this.Page);
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControls()
    {
        txtAccountName.Text = string.Empty;
        txtSecretKey.Text = string.Empty;
        txtContainerName.Text = string.Empty;
        ddlActivity.SelectedIndex = 0;
        ddlInstances.SelectedIndex = 0;

        ddlActivity.Items.Clear();
        ddlInstances.Items.Clear();
        ddlActivity.Items.Add(new ListItem("Please Select", "-1"));
        ddlInstances.Items.Add(new ListItem("Please Select", "-1"));
        objCommon.FillDropDownList(ddlActivity, "ACD_BLOB_STORAGE_ACTIVITY", "ACTIVITYNO", "ACTIVITY_NAME", "ACTIVITYNO>0 AND ACTIVE_STATUS=1", "ACTIVITYNO");
        objCommon.FillDropDownList(ddlInstances, "ACD_INSTANCES", "INSTANCENO", "INSTANCE_NAME", "", "");
        txtBlobStoragePath.Text = string.Empty;
        btnSave.Text = "Submit";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ShowDetail(int feedbackNo)
    {
        StudentController objSC = new StudentController();
        SqlDataReader dr = objSC.GetBlobConfigByID(BlobID);

        if (dr != null)
        {
            if (dr.Read())
            {
                txtAccountName.Text = dr["ACCOUNT_NAME"] == null ? string.Empty : dr["ACCOUNT_NAME"].ToString();
                txtSecretKey.Text = dr["SECRET_KEY"] == null ? string.Empty : dr["SECRET_KEY"].ToString();
                txtContainerName.Text = dr["CONTAINER_NAME"] == null ? string.Empty : dr["CONTAINER_NAME"].ToString();


                if (dr["ACTIVITY_NAME"] == null | dr["ACTIVITY_NAME"].ToString().Equals(""))
                    ddlActivity.SelectedIndex = 0;
                else
                    //ddlActivity.SelectedItem.Text = dr["ACTIVITY_NAME"].ToString();
                    objCommon.FillDropDownList(ddlActivity, "ACD_BLOB_STORAGE_ACTIVITY", "ACTIVITYNO", "ACTIVITY_NAME", "ACTIVITYNO>0 AND ACTIVE_STATUS=1", "ACTIVITYNO");
                    ddlActivity.SelectedValue = dr["ACTIVITYNO"].ToString();

                if (dr["INSTANCE_NAME"] == null | dr["INSTANCE_NAME"].ToString().Equals(""))
                    ddlInstances.SelectedIndex = 0;
                else
                    //ddlInstances.SelectedItem.Text = dr["INSTANCES"].ToString();
                    objCommon.FillDropDownList(ddlInstances, "ACD_INSTANCES", "INSTANCENO", "INSTANCE_NAME", "", "");
                    ddlInstances.SelectedValue = dr["INSTANCENO"].ToString();

                if (dr["ACTIVE_STATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }

                txtBlobStoragePath.Text = dr["BLOB_STORAGE_PATH"] == null ? string.Empty : dr["BLOB_STORAGE_PATH"].ToString(); // Added by Vinay Mishra on 26/06/2023
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            BlobID = int.Parse(btnEdit.CommandArgument);
            ViewState["BlobID"] = BlobID;
            ShowDetail(BlobID);
            ViewState["action"] = "edit";
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}