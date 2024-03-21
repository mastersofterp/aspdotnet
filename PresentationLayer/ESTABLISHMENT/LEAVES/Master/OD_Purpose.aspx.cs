//======================================================================================
// PROJECT NAME  : UAIMS                                                               
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     :                                                 
// CREATION DATE :  5 April 2012                                                     
// CREATED BY    :  Mrunal Bansod      
// Modified By   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//=======================================================================================
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



public partial class ESTABLISHMENT_LEAVES_Master_OD_Purpose : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaves = new LeavesController();

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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                pnlAdd.Visible = false;
                pnlList.Visible = true;
                ViewState["action"] = "add";
                BindListViewPURPOSE();

                //Added by Saket Singh on 14-Dec-2016
                LnkAdd.Visible = true;
                btnShowReport.Visible = true;
                btnSubmit.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=OD_Purpose.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OD_Purpose.aspx");
        }
    }


    #region Actions

    //Add and Update the Data
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            bool result = CheckPurpose();

            //Add/Update
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {

                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        return;
                    }
                    CustomStatus cs = (CustomStatus)objLeaves.AddODPurpose(txtPurpose.Text.Trim(), Session["colcode"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //objCommon.DisplayMessage("Record Saved Successfully", this);
                        BindListViewPURPOSE();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        MessageBox("Record Saved Successfully");
                        Clear();

                        //Added by Saket Singh on 14-Dec-2016
                        LnkAdd.Visible = true;
                        btnShowReport.Visible = true;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        btnBack.Visible = false;

                    }

                }
                else
                {
                    if (ViewState["lbPurposetno"] != null)
                    {
                        if (result == true)
                        {
                            DataSet ds = new DataSet();
                            ds = objCommon.FillDropDown("PAYROLL_OD_PURPOSE", "*", "", "PURPOSENO=" + ViewState["lbPurposetno"].ToString().Trim() + "", "");
                            if (txtPurpose.Text.ToUpper() != ds.Tables[0].Rows[0]["PURPOSE"].ToString().Trim().ToUpper())
                            {
                                objCommon.DisplayMessage("Record Already Exist", this);
                                txtPurpose.Focus();
                                return;
                            }
                        }
                        CustomStatus cs = (CustomStatus)objLeaves.UpdateODPurpose(Convert.ToInt32(ViewState["lbPurposetno"]), txtPurpose.Text.Trim());
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //objCommon.DisplayMessage("Record Updated Successfully", this);
                            BindListViewPURPOSE();
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;

                           // objCommon.DisplayMessage("Record Updated Successfully", this);
                            MessageBox("Record Updated Successfully");
                            Clear();
                            LnkAdd.Visible = true;
                            btnShowReport.Visible = true;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            //Added by Saket Singh on 14-Dec-2016
                            ////LnkAdd.Visible = true;
                            ////btnShowReport.Visible = true;
                            ////btnSubmit.Visible = false;
                            ////btnCancel.Visible = false;
                            ////btnBack.Visible = false;

                        }

                    }
                }
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //to Retrive Data by Clicking Edit Emage Button
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        try
        {

            ImageButton btnEdit = sender as ImageButton;
            ViewState["lbPurposetno"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblPURPOSE = lst.FindControl("lblPURPOSE") as Label;

            txtPurpose.Text = lblPURPOSE.Text;
            ViewState["action"] = "edit";

            //Added by Saket Singh on 14-12-2016
            pnlAdd.Visible = true; 
            pnlList.Visible = false;           
            LnkAdd.Visible = false;
            btnShowReport.Visible = false;
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    // Created By : Saket Singh
    // Date : 14/12/2016
    // Purpose : To delete records
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 PNo = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objLeaves.DeleteODPurpose(PNo);
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

        BindListViewPURPOSE();
    }

    //To navigate Paging List View.
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewPURPOSE();
    }

    //Check user rights and generate the report
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("PURPOSE", "ESTB_ODPurpose.rpt");
    }

    //clear values
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Clear();
        txtPurpose.Text = string.Empty;
    }

    #endregion

    #region Methods



    //Retrive data in listview
    private void BindListViewPURPOSE()
    {
        try
        {

            DataSet dsPURPOSE = objLeaves.AllODPurpose();



            if (dsPURPOSE.Tables[0].Rows.Count > 0)
            {
                lvPURPOSE.DataSource = dsPURPOSE;
                lvPURPOSE.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //To Check newly entered PURPOSE already Exist or not
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_OD_PURPOSE", "*", "", "PURPOSE='" + txtPurpose.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            string url=Request.Url.ToString().Substring(0,(Request.Url.ToString().IndexOf("Establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            // url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@P_COLLEGE_CODE=0," + "@username=" + Session["userfullname"].ToString();
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["college_nos"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowReport->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //Generate the report


    //Clear textbox value
    private void Clear()
    {
        txtPurpose.Text = string.Empty;
        ViewState["action"] = "add";
        pnlAdd.Visible = false; pnlList.Visible = true;
    }

    //function to popup messageBox
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #endregion


    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = true;
        pnlList.Visible = false;

        //Added by Saket Singh on 14-12-2016
        LnkAdd.Visible = false;
        btnShowReport.Visible = false;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        //pnlAdd.Visible = false;
        //pnlList.Visible = true;

        //Added by Saket Singh on 14-12-2016
       
        btnShowReport.Visible = true;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        LnkAdd.Visible = true;
    }
}
