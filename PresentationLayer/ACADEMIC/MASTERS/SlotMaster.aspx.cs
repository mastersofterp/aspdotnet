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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_MASTERS_SlotMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAC = new AcdAttendanceController();
    static int slotNo;

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
                Response.Redirect("~/notauthorized.aspx?page=SlotMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SlotMaster.aspx");
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Active_Status = 0;

            if (hdnActive.Value == "true")
            {
                Active_Status = 1;
            }
            else
            {
                Active_Status = 0;
            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Batch

                    CustomStatus cs = (CustomStatus)objAC.AddSlotMaster(txtSlotName.Text, Session["colcode"].ToString(), Active_Status);
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updSlot, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(updSlot, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updSlot, "Error Adding Slot Name!", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objAC.UpdateSlotMaster(slotNo, txtSlotName.Text, Session["colcode"].ToString(), Active_Status);

                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updSlot, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage(this.updSlot, "Record Updated Successfully!", this.Page);

                        Clear();
                        //ViewState["action"] = null;
                        btnSave.Text = "Submit";
                        //btnSave.CssClass = "btn btn-success";
                        txtSlotName.Focus();

                        //Response.Redirect(Request.Url.ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updSlot, "Error Adding Slot Name!", this.Page);
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
    protected void Clear()
    {
        txtSlotName.Text = string.Empty;
        //Label1.Text = string.Empty;
        ViewState["action"] = "add";
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objAC.GetAllSlots();
            lvSlots.DataSource = ds;
            lvSlots.DataBind();

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
        btnSave.Text = "Submit";
        txtSlotName.Focus();
    }
    private void ShowDetail(int feedbackNo)
    {
        SqlDataReader dr = objAC.GetSlotNo(slotNo);

        if (dr != null)
        {
            if (dr.Read())
            {
                // ViewState["slotno"] = feedbackNo.ToString();
                if (dr["ACTIVESTATUS"].ToString() == "1")
                    ScriptManager.RegisterStartupScript(this, GetType(), "act", "$('[id*=chkActive]').prop('checked', true);", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "act1", "$('[id*=chkActive]').prop('checked', false);", true);

                    txtSlotName.Text = dr["SLOTTYPE_NAME"] == null ? string.Empty : dr["SLOTTYPE_NAME"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        //string MyScript = "$(document).ready(function () { var table = $('#example').DataTable();table.button(0).disable();});";
        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey",MyScript, true);
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            slotNo = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;

            ShowDetail(slotNo);
            ViewState["action"] = "edit";

            btnSave.Text = "Update";
            txtSlotName.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

