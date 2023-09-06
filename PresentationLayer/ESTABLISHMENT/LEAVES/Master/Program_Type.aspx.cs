//==============================================
//CREATED BY: GARIMA BISANEY
//CREATED DATE:20-11-2014
//PURPOSE: TO MAINTAIN RECORD OF PROGRAM/EVENT TYPES
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

public partial class ESTABLISHMENT_LEAVES_Master_Program_Type : System.Web.UI.Page
{


    #region declaration

    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objProgramType = new LeavesController();
    #endregion
    #region page event
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
                BindListViewProgramType();

                LnkAdd.Visible = true;
                btnReport.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
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
    #endregion

    #region clickevent
    //button add click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";

        LnkAdd.Visible = false;
        btnReport.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }

    /// <summary>
    /// Reason: To save & update the record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            bool result = CheckPurpose();

                if (ViewState["action"] != null)
                {
                    Leaves objPType = new Leaves();
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        if (result == true)
                        {
                            //objCommon.DisplayMessage("Record Already Exist", this);
                            MessageBox("Record Already Exist");
                            return;
                        }
                        else
                        {
                            // code to save
                            objPType.PROGRAM_TYPE = txtProgramType.Text;
                            objPType.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                            CustomStatus cs = (CustomStatus)objProgramType.AddProgramType(objPType);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                // MessageBox("Record Saved Successfully");
                                // objCommon.DisplayMessage("Record Saved Successfully", this.Page);
                                MessageBox("Record Saved Successfully");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                            }
                        }
                    }
                    else
                    {

                        //code to update
                        if (ViewState["PNO"] != null)
                        {
                            objPType.PROGRAM_TYPE = txtProgramType.Text;
                            objPType.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                            objPType.PROGRAM_NO = Convert.ToInt32(ViewState["PNO"].ToString());
                            CustomStatus cs = (CustomStatus)objProgramType.UpdateProgramType(objPType);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Updated Successfully");
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                            }
                        }
                    }
                    //code to bind the listview
                    BindListViewProgramType();
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

    //Edit Click
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Int32 PNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            LnkAdd.Visible = false;
            btnReport.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //DELETE BUTTON CLICK
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 PNo = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objProgramType.DeleteProgramType(PNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
            }
            ViewState["action"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        BindListViewProgramType();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Clear();
        txtProgramType.Text = string.Empty;
        //ViewState["action"] = "add";
        //ViewState["PNO"] = null;
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Program Type Entry", "ESTB_EventProgramType.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    #endregion


    #region methods
    //code to Bind the List View
    protected void BindListViewProgramType()
    {
        try
        {
            DataSet ds = objProgramType.GetAllProgramType();
            lvProgramType.DataSource = ds.Tables[0];
            lvProgramType.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //code to display data on th form
    private void ShowDetails(Int32 PNO)
    {
        DataSet ds = null;
        try
        {
            ds = objProgramType.GetSingleProgramType(PNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PNO"] = PNO;
                txtProgramType.Text = ds.Tables[0].Rows[0]["PROGRAM_TYPE"].ToString();
                
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
    
    
   
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void Clear()
    {
        txtProgramType.Text = string.Empty;
        LnkAdd.Visible = true;
        btnReport.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        ViewState["action"] = "add";
        ViewState["PNO"] = null;
    }

   

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Program_Type.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        Clear();
    }


    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("Program_type", "*", "", "PROGRAM_TYPE='" + txtProgramType.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
}
