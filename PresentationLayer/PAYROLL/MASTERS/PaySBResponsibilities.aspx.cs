//==============================================
//CREATED BY: SONAL BANODE
//CREATED DATE:02-04-2024
//PURPOSE: TO MAINTAIN RECORD OF RESPONSIBILITIES
//MODIFIED BY:
//MODIFIED DATE:
//MODIFIED REASON:
//==============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;

public partial class PAYROLL_MASTERS_PaySBResponsibilities : System.Web.UI.Page
{
    #region declaration

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objResponsibility = new ServiceBookController();
    #endregion

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
                ViewState["action"] = "add";
                BindListViewResponsibility();

                CheckPageAuthorization();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Program_Type.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Program_Type.aspx");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (ViewState["action"] != null)
            {
                ServiceBook objSev = new ServiceBook();
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckPurpose();
                    if (result == true)
                    {
                        MessageBox("Record Already Exist");
                        return;
                    }
                    else
                    {
                        objSev.RESPONSIBILITY = txtResponsibilities.Text;
                        objSev.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                        objSev.CREATEDBY = Convert.ToInt32(Session["userno"]);
                        CustomStatus cs = (CustomStatus)objResponsibility.AddResponsibility(objSev);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            //pnlAdd.Visible = false;
                            //pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
                else
                {

                    if (ViewState["RNO"] != null)
                    {
                        objSev.RESPONSIBILITY = txtResponsibilities.Text;
                        objSev.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                        objSev.MODIFYBY = Convert.ToInt32(Session["userno"]);
                        objSev.RNO = Convert.ToInt32(ViewState["RNO"].ToString());
                        CustomStatus cs = (CustomStatus)objResponsibility.UpdateResponsibility(objSev);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            //pnlAdd.Visible = false;
                            //pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
                BindListViewResponsibility();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Int32 RNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(RNO);

            ViewState["action"] = "edit";
            //pnlAdd.Visible = true;
            //pnlList.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtResponsibilities.Text = string.Empty;
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
        dsPURPOSE = objCommon.FillDropDown("PAYROLL_RESPONSIBILITES", "*", "", "RESPONSIBILTIY='" + txtResponsibilities.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;
        }
        return result;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void BindListViewResponsibility()
    {
        try
        {
            DataSet ds = objResponsibility.GetAllResponsibility();
            lvResponsibilites.DataSource = ds.Tables[0];
            lvResponsibilites.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 RNO)
    {
        DataSet ds = null;
        try
        {
            ds = objResponsibility.GetSingleResponsibility(RNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["RNO"] = RNO;
                txtResponsibilities.Text = ds.Tables[0].Rows[0]["RESPONSIBILTIY"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtResponsibilities.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["RNO"] = null;
    }

}