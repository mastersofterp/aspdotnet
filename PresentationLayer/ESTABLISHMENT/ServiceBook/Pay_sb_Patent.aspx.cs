using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using System.IO;
using IITMS.UAIMS;
using System.Configuration;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class ESTABLISHMENT_ServiceBook_Pay_sb_Patent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;


    DataTable dteng = new DataTable();
    //DataTable dt = new DataTable();
    public string path = string.Empty;
    public string Docpath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/");
    public static string RETPATH = "";
    BlobController objBlob = new BlobController();

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
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
            dteng = setGridViewDataset(dteng, "dteng").Clone();
            ViewState["dteng"] = dteng;
            DeleteDirecPath(Docpath + "TEMP_PATENTFILES\\" + _idnoEmp + "\\APP_0");

            FillIPRCategory();
            FillIssuingAgency();
        }


        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue); 
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindListViewPatent();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
        BlobDetails();
        GetConfigForEditAndApprove();
    }

    private void FillIPRCategory()
    {
        try
        {
            objCommon.FillDropDownList(ddlIPRCategory, "PAYROLL_SB_IPRCategory", "IPRNO", "IPR_NAME", "", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_Patent.FillIPRCategory ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillIssuingAgency()
    {
        try
        {
            objCommon.FillDropDownList(ddlIssuingAgency, "PAYROLL_SB_IPRIssuingAgency", "IPRNOAGNO", "IPRIssuing_Agency", "", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_Patent.FillIssuingAgency ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected DataTable setGridViewDataset(DataTable dt, string tabName)
    {
        dt.TableName.Equals(tabName);
        dteng.Columns.Add("Name");
        dteng.Columns.Add("Address");
        dteng.Columns.Add("Role");
        dteng.Columns.Add("SEQNO");
        dteng.Columns["SEQNO"].AutoIncrement = true; dteng.Columns["SEQNO"].AutoIncrementSeed = 1; dteng.Columns["SEQNO"].AutoIncrementStep = 1;
        return dt;
    }

    private void BindListViewPatent()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllStaffPatent(_idnoEmp);
            lvAchiveInfo.DataSource = ds;
            lvAchiveInfo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.BindListViewFamilyInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            DataTable dt = null;
            //CreateTable();
            if (ViewState["dteng"] != null)
            {
                dt = (DataTable)ViewState["dteng"];
            }


            DataTable dtF = null;
            if (ViewState["FILE1"] != null)
            {
                dtF = (DataTable)ViewState["FILE1"];
            }

            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.PatentTitle = txtPatent.Text;
            objSevBook.ApplicantName = txtApplicantname.Text;
            objSevBook.SubjectOfPatent = txtsubject.Text;
            objSevBook.PATENTNO = txtFileNo.Text.Trim();

            //if (txtFileNo.Text != string.Empty)
            //{
            //    objSevBook.FILENO = Convert.ToInt32(txtFileNo.Text);
            //}
            //else
            //{
            //    objSevBook.FILENO = 0;
            //}
            //if (txtAppNo.Text != string.Empty)
            //{
            //    objSevBook.APPLICATION_NO = Convert.ToInt32(txtAppNo.Text);
            //}
            //else
            //{
            //    objSevBook.APPLICATION_NO = 0;
            //}

            objSevBook.APPLICATION_NUMBER = txtAppNo.Text;

            objSevBook.STATUS_DATE = Convert.ToDateTime(txtStatusDate.Text);


            if (ddlrole.SelectedIndex > 0)
            {
                objSevBook.ROLE = Convert.ToInt32(ddlrole.SelectedValue);
            }
            else
            {
                objSevBook.ROLE = 0;
            }

            if (txtotherrole.Text != string.Empty)
            {
                objSevBook.OtherRole = txtotherrole.Text;
            }
            else
            {
                objSevBook.OtherRole = string.Empty;
            }
            if (ddlCategory.SelectedIndex > 0)
            {
                objSevBook.PatentCategory = Convert.ToInt32(ddlCategory.SelectedValue);
            }
            else
            {
                objSevBook.PatentCategory = 0;
            }

            //objSevBook.PatentStatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlstatus.SelectedIndex > 0)
            {
                objSevBook.PatentStatus = Convert.ToInt32(ddlstatus.SelectedValue);
            }
            else
            {
                objSevBook.PatentStatus = 0;
            }

            if (ddlwithdrawn.SelectedIndex > 0)
            {
                objSevBook.Withdrawn = Convert.ToInt32(ddlwithdrawn.SelectedValue);
            }
            else
            {
                objSevBook.Withdrawn = 0;
            }

            //objSevBook.FROMDATE = Convert.ToDateTime(txtDate.Text);

            if (txtDate.Text.Trim() == string.Empty)
            {
                objSevBook.FROMDATE = Convert.ToDateTime("9999/12/31"); // DateTime.MinValue;
            }
            else
            {
                objSevBook.FROMDATE = Convert.ToDateTime(txtDate.Text);
            }


            if (txtNumber.Text != string.Empty)
            {
                objSevBook.NO_GUIDED = Convert.ToInt32(txtNumber.Text);
            }
            else
            {
                objSevBook.NO_GUIDED = 0;
            }
            //Changes done for Blob
            if (lblBlobConnectiontring.Text == "")
            {
                objSevBook.ISBLOB = 0;
            }
            else
            {
                objSevBook.ISBLOB = 1;
            }

            
                objSevBook.IPRNO = Convert.ToInt32(ddlIPRCategory.SelectedValue);
            //}
            //else
            //{
            //    objSevBook.IPRNO = 0;
            //}
            //if (ddlIssuingAgency.SelectedIndex > 0)
            //{
                objSevBook.IPRNOAGNO = Convert.ToInt32(ddlIssuingAgency.SelectedValue);
            //}
            //else
            //{
            //    objSevBook.IPRNOAGNO = 0;
            //}
            


            dteng = (DataTable)ViewState["dteng"];
            if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    objSevBook.PUBTRXNO = 0;
                    CustomStatus cs = (CustomStatus)objServiceBook.AddPatent(objSevBook, dteng, dtF);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string PCNO = objCommon.LookUp("PAYROLL_SB_Patent", "MAX(PCNO)", "");
                                AddDocuments(Convert.ToInt32(PCNO));
                            }
                        }
                        //objServiceBook.upload_new_files("PATENT", _idnoEmp, "PUBTRXNO", "PAYROLL_SB_Patent", "PAT_", flPUB);
                        this.Clear();
                        this.BindListViewPatent();
                        DeletePath();
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exits ");
                        this.Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["PCNO"] != null)
                    {
                        objSevBook.PCNO = Convert.ToInt32(ViewState["PCNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdatePatent(objSevBook, dteng, dtF);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    AddDocuments(Convert.ToInt32(objSevBook.PCNO));
                                }
                            }
                            //objServiceBook.update_upload("PATENT", Convert.ToInt32(ViewState["PUBTRXNO"].ToString()), ViewState["attachment"].ToString(), _idnoEmp, "PAT_", flPUB);
                            // objServiceBook.update_upload("Accomplishment_INFO", objSevBook.ACNO, ViewState["attachment"].ToString(), _idnoEmp, "ACI_", flupld);
                            //objServiceBook.update_upload("Accomplishment_INFO", objSevBook.ACNO, ViewState["attachment"].ToString(), _idnoEmp, "ACI_", flupld);
                            ViewState["action"] = "add";
                            //lblFamilymsg.Text = "Record Updated Successfully";
                            this.Clear();
                            this.BindListViewPatent();
                            DeletePath();
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordFound))
                        {
                            MessageBox("Record Already Exits ");
                            this.Clear();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["FILE1"] = null;
            ImageButton btnEdit = sender as ImageButton;
            int PCNO = int.Parse(btnEdit.CommandArgument);
            //FillIPRCategory();
            //FillIssuingAgency();

            ShowDetails(PCNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int PCNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePatentOfEmployee(PCNO);
            //To show employee family details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["PCNO"] = PCNO.ToString();

                txtPatent.Text = ds.Tables[0].Rows[0]["Title_Patent"].ToString();
                txtApplicantname.Text = ds.Tables[0].Rows[0]["Applicant_Name"].ToString();
                ddlrole.SelectedValue = ds.Tables[0].Rows[0]["Role"].ToString();
                txtotherrole.Text = ds.Tables[0].Rows[0]["Other_Role"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
                ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                ddlwithdrawn.SelectedValue = ds.Tables[0].Rows[0]["Withdrawn"].ToString();


                txtFileNo.Text = ds.Tables[0].Rows[0]["FILENO"].ToString();
                txtStatusDate.Text = ds.Tables[0].Rows[0]["STATUS_DATE"].ToString();
                txtAppNo.Text = ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString();


                txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                txtNumber.Text = ds.Tables[0].Rows[0]["No_Of_Mem"].ToString();
                txtsubject.Text = ds.Tables[0].Rows[0]["Subject_Of_Patent"].ToString();
                //ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                // ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

                ddlIPRCategory.SelectedValue = ds.Tables[0].Rows[0]["IPRNO"].ToString();
                ddlIssuingAgency.SelectedValue = ds.Tables[0].Rows[0]["IPRNOAGNO"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {


                lvEnclosures.DataSource = ds.Tables[1];
                lvEnclosures.DataBind();
                pnlEnclosure.Visible = true;
                ViewState["dteng"] = ds.Tables[1];

                // ViewState["PCNO"] = PCNO.ToString();
                ViewState["SEQNO"] = ds.Tables[1].Rows[(ds.Tables[1].Rows.Count) - 1]["SEQNO"].ToString();

            }
            else
            {
                pnlEnclosure.Visible = true;
            }

            if (Convert.ToInt32(ds.Tables[2].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[2].Rows.Count;
                CreateTable();
                DataTable dtM = (DataTable)ViewState["FILE1"];
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();
                    dr["FUID"] = ds.Tables[2].Rows[i]["FUID"].ToString();
                    dr["FILEPATH"] = Docpath + "PATENT" + ViewState["idno"] + "\\APP_" + PCNO;
                    dr["GETFILE"] = ds.Tables[2].Rows[i]["GETFILE"].ToString();
                    dr["DisplayFileName"] = ds.Tables[2].Rows[i]["DisplayFileName"].ToString();
                    dr["IDNO"] = ds.Tables[2].Rows[i]["IDNO"].ToString();
                    dr["FOLDER"] = "PATENT";
                    dr["APPID"] = PCNO.ToString();
                    dr["FILENAME"] = ds.Tables[2].Rows[i]["FILENAME"].ToString();
                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["FILE1"] = dtM;
                    ViewState["FUID"] = ds.Tables[2].Rows[i]["FUID"].ToString();
                }
                //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
                //LVFiles.DataBind();
                this.BindListView_Attachments(dtM);
                pnlfiles.Visible = true;
            }
            else
            {
                pnlfiles.Visible = false;
                LVFiles.DataSource = null;
                LVFiles.DataBind();
            }
            if (Convert.ToBoolean(ViewState["IsApprovalRequire"]) == true)
            {
                string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                if (STATUS == "A")
                {
                    MessageBox("Your Details Are Approved You Cannot Edit.");
                    btnSubmit.Enabled = false;
                    return;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
                GetConfigForEditAndApprove();
            }
            else
            {
                btnSubmit.Enabled = true;
                GetConfigForEditAndApprove();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int PCNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_Patent", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "PCNO=" + PCNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved You Cannot Delete.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeletePatentDetails(PCNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {

                    BindListViewPatent();
                    ViewState["action"] = "add";
                    Clear();
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        DeleteDirecPath(Docpath + "TEMP_PATENTFILES\\" + _idnoEmp + "\\APP_0");
        GetConfigForEditAndApprove();
    }

    private void Clear()
    {
        // txtname.Text = txtadd.Text = txtDateOftalk.Text = txtToDate.Text = txtConsultancy.Text = txtAmount.Text = txtnaturework.Text = txtDescription.Text = string.Empty;
        txtPatent.Text = txtApplicantname.Text = txtotherrole.Text = txtDate.Text = txtNumber.Text = txtName.Text = txtAddress.Text = string.Empty;

        ddlrolenew.SelectedValue = "0";
        ddlrole.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        ddlstatus.SelectedValue = "0";
        ddlwithdrawn.SelectedValue = "0";
        txtStatusDate.Text = txtFileNo.Text = txtAppNo.Text = string.Empty;
        ViewState["action"] = "add";

        lvEnclosures.DataSource = null;
        lvEnclosures.DataBind();
        //  pnlfiles.Visible = false;
        ViewState["dteng"] = null;
        txtsubject.Text = string.Empty;
        //ViewState["attachment"] = null;

        ddlIPRCategory.SelectedValue = "0";
        ddlIssuingAgency.SelectedValue = "0";

        LVFiles.DataSource = null;
        LVFiles.DataBind();
        pnlfiles.Visible = false;
        ViewState["FILE1"] = null;
        ViewState["DESTINATION_PATH"] = null;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #region PatentDetails
    //protected DataTable CreateTblEnclosure()
    //{
    //    DataTable dtER = new DataTable();
    //    dtER.Columns.Add(new DataColumn("SRNO", typeof(int)));
    //    dtER.Columns.Add(new DataColumn("NAME", typeof(string)));
    //    dtER.Columns.Add(new DataColumn("Address", typeof(string)));
    //    dtER.Columns.Add(new DataColumn("Role", typeof(string)));
    //    return dtER;
    //}
    protected void ClearER()
    {
        txtName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlrolenew.SelectedIndex = 0;
    }
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        pnlEnclosure.Visible = true;

    //            DataTable dtE = (DataTable)Session["RecEnTbl"];
    //            DataRow dr = dtE.NewRow();
    //            dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

    //            dr["NAME"] = txtName.Text.Trim() == null ? string.Empty : Convert.ToString(txtName.Text.Trim());
    //            dr["Address"] = txtAddress.Text.Trim() == null ? string.Empty : Convert.ToString(txtAddress.Text.Trim());

    //            dr["Role"] = ddlrolenew.SelectedValue;
    //            //if (ddlrolenew.SelectedIndex > 0)
    //            //{
    //            //    dr["Role"] = Convert.ToInt32(ddlrolenew.SelectedValue);
    //            //}
    //            //else
    //            //{
    //            //    dr["Role"] = 0;
    //            //}

    //            dtE.Rows.Add(dr);
    //            Session["RecEnTbl"] = dtE;
    //            lvEnclosures.DataSource = dtE;
    //            lvEnclosures.DataBind();
    //            pnlEnclosure.Visible = true;
    //            ClearER();
    //            ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

    //        //else
    //        //{
    //        //    DataTable dtE = this.CreateTblEnclosure();
    //        //    DataRow dr = dtE.NewRow();
    //        //    dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

    //        //    dr["NAME"] = txtName.Text.Trim() == null ? string.Empty : Convert.ToString(txtName.Text.Trim());
    //        //    dr["Address"] = txtAddress.Text.Trim() == null ? string.Empty : Convert.ToString(txtAddress.Text.Trim());
    //        //    if (ddlrolenew.SelectedIndex > 0)
    //        //    {
    //        //        dr["Role"] = Convert.ToInt32(ddlrolenew.SelectedValue);
    //        //    }
    //        //    else
    //        //    {
    //        //        dr["Role"] = 0;
    //        //    }

    //        //    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
    //        //    dtE.Rows.Add(dr);
    //        //    ClearER();
    //        //    Session["RecEnTbl"] = dtE;
    //        //    lvEnclosures.DataSource = dtE;
    //        //    lvEnclosures.DataBind();
    //        //    pnlEnclosure.Visible = true;
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "IO_OutwardDispatch.btnAddTo_Click -->" + ex.Message + "" + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ViewState["dteng"] != null)
        {
            dteng = (DataTable)ViewState["dteng"];
        }
        else
        {
            dteng = setGridViewDataset(dteng, "dteng").Clone();
            ViewState["dteng"] = dteng;
        }

        if (CheckDuplicateBondName(dteng, txtName.Text.Trim(), txtAddress.Text.Trim(), ddlrolenew.SelectedValue))
        {
            txtName.Text = txtAddress.Text = string.Empty;
            ddlrolenew.SelectedIndex = 0;
            MessageBox("Record Already Exist!");
            return;
        }

        DataRow dr = dteng.NewRow();
        //dteng.Columns["SEQNO"].AutoIncrementStep = 1;
        dr["Name"] = txtName.Text.ToString();
        dr["Address"] = txtAddress.Text.ToString();

        if (ddlrolenew.SelectedIndex > 0)
        {
            dr["Role"] = Convert.ToString(ddlrolenew.SelectedValue);
        }
        else
        {
            dr["Role"] = string.Empty;
        }
        // dr["SEQNO"] = dteng.Columns["SEQNO"].AutoIncrementStep = 1;
        // dr["SEQNO"] = Convert.ToString(Convert.ToInt32(dteng.Columns["SEQNO"]) + 1);

        dr["SEQNO"] = Convert.ToInt32(ViewState["SEQNO"]) + 1;

        dteng.Rows.Add(dr);
        dteng.AcceptChanges();
        lvEnclosures.DataSource = dteng;
        lvEnclosures.DataBind();
        pnlEnclosure.Visible = true;
        txtName.Text = txtAddress.Text = string.Empty;
        ddlrolenew.SelectedIndex = 0;
        // ddlEmp.SelectedIndex = 0;              
        ViewState["SEQNO"] = Convert.ToInt32(ViewState["SEQNO"]) + 1;

    }
    protected void btnDeleteEN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["dteng"] != null && ((DataTable)ViewState["dteng"]) != null)
            {
                DataTable dtM = (DataTable)ViewState["dteng"];
                dtM.Rows.Remove(this.GetEditableDatarow(dtM, btnDelete.CommandArgument));
                ViewState["dteng"] = dtM;
                if (dtM.Rows.Count > 0)
                {
                    lvEnclosures.DataSource = dtM;
                    lvEnclosures.DataBind();
                }
                else
                {
                    lvEnclosures.DataSource = null;
                    lvEnclosures.DataBind();
                }
                //lvEngagedInfo.DataSource = dtM;
                //lvEngagedInfo.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckDuplicateBondName(DataTable dt, string value, string value2, string value1)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Name"].ToString() == value && dr["Address"].ToString() == value2 && dr["Role"].ToString() == value1)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return retVal;
    }

    private DataRow GetEditableDatarow(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["SEQNO"].ToString() == value)
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



    private DataRow GetEditDatarow(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["ERNO"].ToString() == value)
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
    #endregion

    #region Upload File
    private int Addfieldstotbl(string filename)
    {
        if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "PATENT" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "PAT_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_PATENTFILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = ViewState["FILE1"];
            //LVFiles.DataBind();
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            this.BindListView_Attachments(dt);
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "PATENT" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "PAT_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_PATENTFILES";
            dr["APPID"] = 0;
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
            //LVFiles.DataBind();
            pnlfiles.Visible = true;
            this.BindListView_Attachments(dt);
        }
        return Convert.ToInt32(ViewState["FUID"]);
    }
    private void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn("FUID", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FILEPATH", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("GETFILE", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("IDNO", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FOLDER", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("APPID", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FILENAME", typeof(string));
        dt.Columns.Add(dc);

        ViewState["FILE1"] = dt;
    }
    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".DOC", ".doc", ".JPEG", ".jpeg", ".DOCX" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    public string GetFileNamePath(object filename, object SCNO, object idno, object folder, object AppID)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/PAT_" + SCNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    private void AddDocuments(int SCNO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            int idno = _idnoEmp;

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\APP_" + SCNO;

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
            }
            DeleteDirectory(sourcePath);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.AddDocuments-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            //Delete all files from the Directory
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            //Delete all child Directories
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            //Delete a Directory
            Directory.Delete(path);
        }
    }
    protected void btnDelFile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            int appid = Convert.ToInt32(btnDelete.AlternateText);
            if (appid != 0)
            {
                path = Docpath + "PATENT" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["PCNO"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_PATENTFILES" + "\\" + idno + "\\APP_" + appid;
            }

            if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
            {
                DataTable dt = (DataTable)ViewState["FILE1"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, fname));
                ViewState["FILE1"] = dt;
                BindListView_Attachments(dt);
                //LVFiles.DataSource = dt;
                //LVFiles.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);

                if (ViewState["DELETE_BILLS"] != null && ((DataTable)ViewState["DELETE_BILLS"]) != null)
                {
                    DataTable dtD = (DataTable)ViewState["DELETE_BILLS"];
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = path;
                    dr["FILENAME"] = fname;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BILLS"] = dtD;
                }
                else
                {
                    DataTable dtD = this.CreateTableBill();
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = path;
                    dr["FILENAME"] = fname;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BILLS"] = dtD;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private DataTable CreateTableBill()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }
    private DataRow GetEditableDatarowBill(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["GETFILE"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return datRow;
    }
    private void DeletePath()
    {
        if (ViewState["DELETE_BILLS"] != null && ((DataTable)ViewState["DELETE_BILLS"]) != null)
        {
            int i = 0;
            DataTable DtDel = (DataTable)ViewState["DELETE_BILLS"];
            foreach (DataRow Dr in DtDel.Rows)
            {
                string filename = DtDel.Rows[i]["FILENAME"].ToString();
                string filepath = DtDel.Rows[i]["FILEPATH"].ToString();

                if (File.Exists(filepath + "\\" + filename))
                {
                    File.Delete(filepath + "\\" + filename);
                }
                i++;
            }
            ViewState["DELETE_BILLS"] = null;
        }
    }
    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ServiceBook objSevBook = new ServiceBook();
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileContent.Length >= 1024 * 10000)
                        {

                            MessageBox("File Size Should Not Be Greater Than 10 Mb");
                            FileUpload1.Dispose();
                            FileUpload1.Focus();
                            return;
                        }
                    }
                    if (Session["serviceIdNo"] != null && Convert.ToInt32(Session["serviceIdNo"]) != 0)
                    {
                        idno = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
                    }
                    else
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    string FileName = FileUpload1.FileName;
                    if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["FILE1"];
                        for (int i = 0; i < dtM.Rows.Count; i++)
                        {
                            if (dtM.Rows[i]["DisplayFileName"].ToString() == FileName)
                            {
                                MessageBox("File Already Exist!");
                                return;
                            }
                        }
                    }

                    string file = Docpath + "TEMP_PATENTFILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "PATENT\\" + idno;
                    ViewState["DESTINATION_PATH"] = PATH;
                    if (lblBlobConnectiontring.Text == "")
                    {
                        objSevBook.ISBLOB = 0;
                    }
                    else
                    {
                        objSevBook.ISBLOB = 1;
                    }
                    if (objSevBook.ISBLOB == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        string IdNo = _idnoEmp.ToString();
                        if (FileUpload1.HasFile)
                        {
                            string contentType = contentType = FileUpload1.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                            //HttpPostedFile file = flupld.PostedFile;
                            //filename = objSevBook.IDNO + "_familyinfo" + ext;
                            //string name = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = IdNo + "_patent_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (FileUpload1.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_patent_" + time, FileUpload1);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    int stcno = Addfieldstotbl(filename);
                                    //BindListView_Attachments();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                string filename = FileUpload1.FileName;
                                int stcno = Addfieldstotbl(filename);
                                path = file + "\\PAT_" + stcno + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                                FileUpload1.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]", this.Page);
                    FileUpload1.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnUploadDoc_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void DeleteDirecPath(string FilePath)
    {
        if (System.IO.Directory.Exists(FilePath))
        {
            try
            {
                System.IO.Directory.Delete(FilePath, true);
            }

            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    #endregion

    #region Blob
    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameEmployee";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            LVFiles.DataSource = dt;
            LVFiles.DataBind();


            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = LVFiles.FindControl("divBlobDownload");
                Control ctrHead1 = LVFiles.FindControl("divattachblob");
                Control ctrhead2 = LVFiles.FindControl("divattach");
                Control ctrHead3 = LVFiles.FindControl("divDownload");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;
                ctrHead3.Visible = false;

                foreach (ListViewItem lvRow in LVFiles.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    Control download = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;
                    download.Visible = false;
                }
            }
            else
            {

                Control ctrHeader = LVFiles.FindControl("divBlobDownload");
                Control ctrHead1 = LVFiles.FindControl("divattachblob");
                Control ctrhead2 = LVFiles.FindControl("divattach");
                Control ctrHead3 = LVFiles.FindControl("divDownload");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;
                ctrHead3.Visible = true;

                foreach (ListViewItem lvRow in LVFiles.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    Control download = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;
                    attachblob.Visible = false;
                    ckattach.Visible = true;
                    download.Visible = true;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Patent";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.GetConfigForEditAndApprove-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    #endregion
}