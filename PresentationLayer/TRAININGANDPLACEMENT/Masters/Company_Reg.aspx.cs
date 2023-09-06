//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : TRAINING AND PLACEMENT
// PAGE NAME     : COMPANY REGISTRATION
// CREATION DATE : 06-AUG-2019
// CREATED BY    : SWAPNIL PRACHAND
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================



using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class TRAININGANDPLACEMENT_Masters_Company_Reg : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,TPController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible=true;
                
                FillCity();
                FillCompCat();
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");
            }
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int COMID = int.Parse(btnEdit.CommandArgument);
            Clear();
            ShowDetails(COMID);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void BindCompany()
    {
        DataSet ds = null;
        try
        {
            //ds = objCompany.GetAllCompany(radlStatus.SelectedValue.ToString());
            ds = objCompany.GetAllCompany("B");
            if (ds.Tables[0].Rows.Count <= 0)
            {
                dpPager.Visible = false;
                btnShowReport.Visible = false;
            }
            else
            {
                dpPager.Visible = true;
                //btnShowReport.Visible = true;
            }
            lvCompany.DataSource = ds.Tables[0];
            lvCompany.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.BindCompany ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindCompany();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["action"] = "add";
            Clear();
            lvTo.DataSource = null;
            lvTo.DataBind();
            pnlAdd.Visible = true;
            pnlList.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company_Reg.btnAdd_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillCompCat()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "ACD_TP_COMPCATEGORY", "CATNO", "CATNAME", "", "CATNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company_Reg.FillCompCat ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillCity()
    {
        try
        {
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "", "CITY");
            ddlCity.Items.Insert(ddlCity.Items.Count, "Other");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company_Reg.FillCity ->" + ex.Message + " " + ex.StackTrace);
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
        txtCompany.Text = string.Empty;
        txtShortname.Text = string.Empty;
        txtDirector.Text = string.Empty;
        txtCompAdd.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        txtemailid.Text = string.Empty;
        txtFaxNo.Text = string.Empty;
        txtWebSite.Text = string.Empty;
        txtSalRange.Text = string.Empty;
        txtContPerson.Text = string.Empty;
        txtContAddress.Text = string.Empty;
        txtContDesignation.Text = string.Empty;
        txtContAddress.Text = string.Empty;
        txtContPhone.Text = string.Empty;
        txtContMailId.Text = string.Empty;
        txtOthInfo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlCategory.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        chkInplant.Checked = false;
        txtIPName.Text = string.Empty;
        txtIPDesignation.Text = string.Empty;
        txtIPAddress.Text = string.Empty;
        txtIPContNo.Text = string.Empty;
        txtIPEmail.Text = string.Empty;
        txtPConactNo.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
    }
    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedIndex == ddlCity.Items.Count - 1)
        {
            txtbox.Text = string.Empty;
            ddlCity.Visible = false;
            txtbox.Visible = true;

        }
    }
    protected void txtbox_TextChanged(object sender, EventArgs e)
    {
        int index = 0;
        if (!this.ddlCity.Items.Contains(new ListItem(this.txtbox.Text.Trim())))
        {
            this.ddlCity.Items.Add(this.txtbox.Text.Trim());

        }
        for (int i = 0; i < this.ddlCity.Items.Count; i++)
        {
            if (ddlCity.Items[i].Text == this.txtbox.Text)
            {
                index = i;
            }
        }
        this.ddlCity.SelectedIndex = index;
        this.ddlCity.Visible = true;
        this.txtbox.Visible = false;
    }
    private DataTable CreateTabel()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("BRANCHNAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("BRANCHNO", typeof(int)));
        // dtRe.Columns.Add(new DataColumn("STATUS", typeof(string)));
        return dtRe;
    }
    protected void btnAddIP_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["BRANCHNAME"] = Convert.ToString(ddlBranch.SelectedItem);
                dr["BRANCHNO"] = Convert.ToInt32(ddlBranch.SelectedValue);
                //dr["STATUS"] = "T";

                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {

                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["BRANCHNAME"] = Convert.ToString(ddlBranch.SelectedItem);

                dr["BRANCHNO"] = Convert.ToInt32(ddlBranch.SelectedValue);
                // dr["STATUS"] = "T";

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnAddIP_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ClearRec()
    {
        ddlBranch.SelectedIndex = 0;
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["RecTbl"] = dt;
                lvTo.DataSource = dt;
                lvTo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string brno = string.Empty;
            string brno1 = string.Empty;
            //int brno = 0;
            //int brno1 = 0;

            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = (DataTable)Session["RecTbl"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (brno.Trim().Equals(string.Empty))
                    {
                        brno = dr["BRANCHNO"].ToString();
                        brno1 = dr["BRANCHNO"].ToString();
                    }
                    else
                    {
                        //string brno9 =(Convert.ToString(brno) + ',' + (dr["BRANCHNO"]));
                        brno = Convert.ToString(brno) + ',' + dr["BRANCHNO"].ToString();
                    }
                }
                if (chkInplant.Checked)
                {
                    if (txtIPName.Text.Trim().Equals(string.Empty))
                    {
                        objCommon.DisplayMessage("Inplant Training Contact Person Name can not be Blank", this.Page);
                        txtIPName.Focus();
                        return;
                    }
                }
                string pwd = Common.EncryptPassword(Convert.ToString(txtShortname.Text));
                TrainingPlacement objTP = new TrainingPlacement();

                objTP.COMPNAME = Convert.ToString(txtCompany.Text);
                objTP.COMPCATNO = Convert.ToInt32(ddlCategory.SelectedValue);
                objTP.COMPCODE = Convert.ToString(txtShortname.Text);
                objTP.COMPDIRECTOR = Convert.ToString(txtDirector.Text);
                objTP.COMPADD = Convert.ToString(txtCompAdd.Text);

                if (!(ddlCity.SelectedValue.Equals(ddlCity.SelectedItem.ToString())))
                    objTP.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
                else
                    objTP.CITYNO = 0;

                objTP.CITY = Convert.ToString(ddlCity.SelectedItem);
                objTP.PINCODE = Convert.ToString(txtPincode.Text);
                objTP.PHONENO = Convert.ToString(txtPhoneNo.Text);
                objTP.FAXNO = Convert.ToString(txtFaxNo.Text);
                objTP.EMAILID = Convert.ToString(txtemailid.Text);
                objTP.WEBSITE = Convert.ToString(txtWebSite.Text);
                objTP.SALRANGE = Convert.ToString(txtSalRange.Text);
                objTP.CONTPERSON = Convert.ToString(txtContPerson.Text);
                objTP.CONTDESIGNATION = Convert.ToString(txtContDesignation.Text);
                objTP.CONTADDRESS = Convert.ToString(txtContAddress.Text);
                objTP.CONTPHONE = Convert.ToString(txtContPhone.Text);
                objTP.CONTMAILID = Convert.ToString(txtContMailId.Text);
                objTP.OTHERINFO = Convert.ToString(txtOthInfo.Text);
                objTP.REMARK = Convert.ToString(txtRemark.Text);
                objTP.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                objTP.COMPPWD = pwd;
                //objTP.COMPSTATUS = Convert.ToChar(ddlStatus.SelectedValue);
                objTP.COMPSTATUS = Convert.ToChar('Y');
                objTP.INPLANT = chkInplant.Checked ? Convert.ToChar('Y') : Convert.ToChar('N');
                if (chkInplant.Checked)
                {
                    objTP.IPCONTPERSON = txtIPName.Text.Trim().Equals(string.Empty) ? string.Empty : txtIPName.Text.Trim();
                    objTP.IPCONTDESIGNATION = txtIPDesignation.Text.Trim().Equals(string.Empty) ? string.Empty : txtIPDesignation.Text.Trim();
                    objTP.IPCONTADDRESS = txtIPAddress.Text.Trim().Equals(string.Empty) ? string.Empty : txtIPAddress.Text.Trim();
                    objTP.IPCONTPHONE = txtIPContNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtIPContNo.Text.Trim();
                    objTP.IPCONTMAILID = txtIPEmail.Text.Trim().Equals(string.Empty) ? string.Empty : txtIPEmail.Text.Trim();
                    objTP.PLACEMENTCONTNO = txtPConactNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtPConactNo.Text.Trim();
                }
                else
                {
                    objTP.IPCONTPERSON = string.Empty;
                    objTP.IPCONTDESIGNATION = string.Empty;
                    objTP.IPCONTADDRESS = string.Empty;
                    objTP.IPCONTPHONE = string.Empty;
                    objTP.IPCONTMAILID = string.Empty;
                    objTP.PLACEMENTCONTNO = string.Empty;
                }
                //objTP.BRANCHNO = brno1;
                objTP.BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        CustomStatus cs = (CustomStatus)objCompany.AddCompany(objTP, brno);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                            Response.Redirect(Request.Url.ToString());
                        }
                    }
                    else
                    {
                        if (ViewState["COMPID"] != null)
                        {
                            objTP.COMPID = Convert.ToInt32(ViewState["COMPID"]);
                            CustomStatus CS = (CustomStatus)objCompany.UpdateCompany(objTP, brno);
                            if (CS.Equals(CustomStatus.RecordUpdated))
                            {
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Session["RecTbl"] = null;
                                Clear();
                                Response.Redirect(Request.Url.ToString());
                            }
                        }
                    }
                }

            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(Int32 COMPID)
    {
        DataSet ds = null;
        try
        {
            ds = objCompany.GetCompanyById(COMPID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["COMPID"] = COMPID;

                txtCompany.Text = ds.Tables[0].Rows[0]["COMPNAME"].ToString();
                txtShortname.Text = ds.Tables[0].Rows[0]["COMPCODE"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["COMPCATNO"].ToString();
                txtDirector.Text = ds.Tables[0].Rows[0]["COMPDIRECTOR"].ToString();
                txtCompAdd.Text = ds.Tables[0].Rows[0]["COMPADD"].ToString();

                if (!(ds.Tables[0].Rows[0]["CITYNO"].ToString().Equals("0")))
                    ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                else
                {
                    ddlCity.Items.Add(ds.Tables[0].Rows[0]["CITY"].ToString());
                    ddlCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
                }
                txtPincode.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                txtFaxNo.Text = ds.Tables[0].Rows[0]["FAXNO"].ToString();
                txtemailid.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtWebSite.Text = ds.Tables[0].Rows[0]["WEBSITE"].ToString();
                txtSalRange.Text = ds.Tables[0].Rows[0]["SALRANGE"].ToString();
                txtContPerson.Text = ds.Tables[0].Rows[0]["CONTPERSON"].ToString();
                txtContDesignation.Text = ds.Tables[0].Rows[0]["CONTDESIGNATION"].ToString();
                txtContAddress.Text = ds.Tables[0].Rows[0]["CONTADDRESS"].ToString();
                txtContPhone.Text = ds.Tables[0].Rows[0]["CONTPHONE"].ToString();
                txtContMailId.Text = ds.Tables[0].Rows[0]["CONTMAILID"].ToString();
                txtOthInfo.Text = ds.Tables[0].Rows[0]["OTHERINFO"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                //ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS"].ToString();
                chkInplant.Checked = ds.Tables[0].Rows[0]["INPLANT"].ToString().Equals("Y") ? true : false;
                if (ds.Tables[0].Rows[0]["INPLANT"].ToString().Equals("Y"))
                {
                    txtIPName.Text = ds.Tables[1].Rows[0]["CONTPERSON"].ToString();
                    txtIPDesignation.Text = ds.Tables[1].Rows[0]["CONTDESIGNATION"].ToString();
                    txtIPAddress.Text = ds.Tables[1].Rows[0]["CONTADDRESS"].ToString();
                    txtIPContNo.Text = ds.Tables[1].Rows[0]["CONTPHONE"].ToString();
                    txtIPEmail.Text = ds.Tables[1].Rows[0]["CONTMAILID"].ToString();
                    txtPConactNo.Text = ds.Tables[1].Rows[0]["PLACEMENTCONTNO"].ToString();
                }
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();

                DataSet dsR = objCompany.GetBranchIP(COMPID);
                if (Convert.ToInt32(dsR.Tables[0].Rows.Count) > 0)
                {
                    lvTo.DataSource = dsR;
                    lvTo.DataBind();
                    Session["RecTbl"] = dsR.Tables[0];
                    ViewState["SRNO"] = Convert.ToInt32(dsR.Tables[0].Rows.Count);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void radlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompany();
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("CompanyList", "TPCompanyList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString() + ",@P_STATUS=" + radlStatus.SelectedValue.ToString();         
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString() + ",@P_STATUS='B'";


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    
}
