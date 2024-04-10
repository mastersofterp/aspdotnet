
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Sb_Admin_Responsibilities.aspx                                               
// CREATION DATE : 13-07-2018                                                    
// CREATED BY    : Saket Singh                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ESTABLISHMENT_ServiceBook_OtherMiscelnew : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    ServiceBook objSevBook = new ServiceBook();
    public int _idnoEmp;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string empno = ViewState["idno"].ToString();

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
            ViewState["RecTbl"] = null;
            ViewState["BRecTbl"] = null;
            ViewState["PRecTbl"] = null;
            ViewState["IdRecTbl"] = null;
            ViewState["ThRecTbl"] = null;
        }
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        BindListViewMisOtherDetail();
        GetConfigForEditAndApprove();
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    private void BindListViewMisOtherDetail()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllMISOTHERDetail(_idnoEmp);
            lvother.DataSource = ds;
            lvother.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.BindListViewAdminResponsiblities-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtbond.Text = txtFromDate.Text = txtToDate.Text = txtphdgui.Text = txtphdawd.Text = txtHindex.Text = string.Empty;
        txtIndexDt.Text = txtindexVal.Text = string.Empty; txtunderGuidence.Text = string.Empty; txtAwarded.Text = string.Empty;
        ddlIndexFac.SelectedValue = "0";

        pnlBond.Visible = false;
        lvBondList.DataSource = null;
        lvBondList.DataBind();

        pnllIndexFact.Visible = false;
        lvIndexFact.DataSource = null;
        lvIndexFact.DataBind();

        panelphd.Visible = false;
        lvphd.DataSource = null;
        lvphd.DataBind();

        ViewState["RecTbl"] = null;
        //ViewState["BRecTbl"] = null;
        ViewState["PRecTbl"] = null;

        ViewState["action"] = "add";
        txtCandidate.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtGrant.Text = string.Empty;
        txtNo.Text = string.Empty;
        txtPatent.Text = string.Empty;
        txtUniversity.Text = string.Empty;
        txtResearch.Text = string.Empty;
        txtResGuide.Text = string.Empty;
        txtRegNum.Text = string.Empty;
        txtThesis.Text = string.Empty;

        pnlIDList.Visible = false;
        lvIDList.DataSource = null;
        lvIDList.DataBind();

        pnlThesisList.Visible = false;
        lvThesis.DataSource = null;
        lvThesis.DataBind();
        ViewState["IdRecTbl"] = null;
        ViewState["ThRecTbl"] = null;

        txtWeb.Text = string.Empty;
        txtScopus.Text = string.Empty;
        txtOrchid.Text = string.Empty;
        txtSupervisor.Text = string.Empty;
        txtGoogleScId.Text = string.Empty;
        txtThesisTitle.Text = string.Empty;
        txtThesisUniversity.Text = string.Empty;
        txtMonth.Text = string.Empty;
        txtThesisYear.Text = string.Empty;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }


    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            objSevBook.IDNO = _idnoEmp;

            if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
            {
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
            //objSevBook.HIndex = txtHindex.Text;
            //objSevBook.Bond = txtbond.Text;          
            //objSevBook.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
            //objSevBook.TODATE = Convert.ToDateTime(txtToDate.Text);

            //objSevBook.PHDGUIDED = txtphdgui.Text.Trim().Equals(string.Empty) ? 0 : (txtphdgui.Text).ToString();
            //objSevBook.PHDAWARD = txtphdawd.Text.Trim().Equals(string.Empty) ? 0 : txtphdawd.Text;

            //if (txtphdgui.Text != string.Empty)
            //{
            //    objSevBook.PHDGUIDED = txtphdgui.Text;
            //}
            //else
            //{
            //    objSevBook.PHDGUIDED = string.Empty;
            //}

            //if (txtphdawd.Text != string.Empty)
            //{
            //    objSevBook.PHDAWARD = txtphdawd.Text;
            //}
            //else
            //{
            //    objSevBook.PHDAWARD = string.Empty;
            //}

            //if (txtCandidate.Text != string.Empty)
            //{
            //    objSevBook.NAME = txtCandidate.Text;
            //}
            //else
            //{
            //    objSevBook.NAME = string.Empty;
            //}



            //if (txtDate.Text != String.Empty)
            //{
            //    objSevBook.DATE = Convert.ToDateTime(txtDate.Text);
            //}
            //else
            //{
            //    objSevBook.DATE = null;
            //}

            //if (txtDate.Text.Trim() == string.Empty)
            //{
            //    objSevBook.DATE = Convert.ToDateTime("9999/12/31"); // DateTime.MinValue;
            //}
            //else
            //{
            //    objSevBook.DATE = Convert.ToDateTime(txtDate.Text);
            //}






            //if (txtUniversity.Text != string.Empty)
            //{
            //    objSevBook.UNIVERSITY_NAME = txtUniversity.Text;
            //}
            //else
            //{
            //    objSevBook.UNIVERSITY_NAME = string.Empty;
            //}

            //if (txtResearch.Text != string.Empty)
            //{
            //    objSevBook.RESEARCHNAME = txtResearch.Text;
            //}
            //else
            //{
            //    objSevBook.RESEARCHNAME = string.Empty;
            //}

            //if (txtResGuide.Text != string.Empty)
            //{
            //    objSevBook.GUIDENAME = txtResGuide.Text;
            //}
            //else
            //{
            //    objSevBook.GUIDENAME = string.Empty;
            //}

            //if (txtNo.Text != string.Empty)
            //{
            //    objSevBook.PUBLICATIONPHDNO = txtNo.Text;
            //}
            //else
            //{
            //    objSevBook.PUBLICATIONPHDNO = string.Empty;
            //}

            //if (txtGrant.Text != string.Empty)
            //{
            //    objSevBook.PHDGRANT = txtGrant.Text;
            //}
            //else
            //{
            //    objSevBook.PHDGRANT = string.Empty;
            //}

            //if (txtPatent.Text != string.Empty)
            //{
            //    objSevBook.PHDPATENT = txtPatent.Text;
            //}
            //else
            //{
            //    objSevBook.PHDPATENT = string.Empty;
            //}

            //if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            //{
            //    MessageBox("To Date Should be Greater than  or equal to From Date");
            //    txtToDate.Text = string.Empty;
            //    txtToDate.Focus();
            //    return;
            //}

            if (txtunderGuidence.Text != string.Empty)
            {
                objSevBook.PHDGUIDED = Convert.ToInt32(txtunderGuidence.Text);
            }
            else
            {
                objSevBook.PHDGUIDED = 0;
            }

            if (txtAwarded.Text != string.Empty)
            {
                objSevBook.PHDAWARD = Convert.ToInt32(txtAwarded.Text);
            }
            else
            {
                objSevBook.PHDAWARD = 0;
            }

            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();

            if (flupld.HasFile)
            {
                // objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
                if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
                {
                    objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
                }
                else
                {
                    MessageBox("Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]");
                    flupld.Focus();
                    return;
                }

            }
            else
            {
                if (ViewState["attachment"] != null)
                {
                    objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
                }
                else
                {
                    objSevBook.ATTACHMENTS = string.Empty;
                }

            }



            #region datatable
            DataTable dtIF = null, dtBD = null, dtPH = null;
            DataTable dtId = null, dtTh = null;
            if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
            {
                if (((DataTable)ViewState["RecTbl"]).Rows.Count > 0)
                {
                    dtIF = (DataTable)ViewState["RecTbl"];
                }
                else
                {
                    //dtIF = this.CreateTable();
                    // MessageBox("Please add Miscellaneous Detail!");
                    //return;
                }
            }
            else
            {
                //dtIF = this.CreateTable();
                //MessageBox("Please add Miscellaneous Detail!");
                //return;
            }


            if (ViewState["BRecTbl"] != null && ((DataTable)ViewState["BRecTbl"]) != null)
            {
                if (((DataTable)ViewState["BRecTbl"]).Rows.Count > 0)
                {
                    dtBD = (DataTable)ViewState["BRecTbl"];
                }
                else
                {
                    //dtBD = this.CreateTableBond();
                }
            }
            else
            {
                //dtBD = this.CreateTableBond();
                //MessageBox("Please add Bond Detail!");
                //return;
            }


            //phd details


            if (ViewState["PRecTbl"] != null && ((DataTable)ViewState["PRecTbl"]) != null)
            {
                if (((DataTable)ViewState["PRecTbl"]).Rows.Count > 0)
                {
                    dtPH = (DataTable)ViewState["PRecTbl"];
                }
                else
                {
                    //dtPH = this.CreateTablePhd();
                }
            }
            else
            {
                //dtPH = this.CreateTablePhd();
                //MessageBox("Please add Phd Detail!");
                //return;
            }

            //
            //Added by Sonal

            if (ViewState["IdRecTbl"] != null && ((DataTable)ViewState["IdRecTbl"]) != null)
            {
                if (((DataTable)ViewState["IdRecTbl"]).Rows.Count > 0)
                {
                    dtId = (DataTable)ViewState["IdRecTbl"];
                }
                else
                {
                    //dtId = this.CreateTableIDDetail();
                }
            }
            else
            {
                //dtId = this.CreateTableIDDetail();
                //MessageBox("Please add ID Details!");
                //return;
            }

            if (ViewState["ThRecTbl"] != null && ((DataTable)ViewState["ThRecTbl"]) != null)
            {
                if (((DataTable)ViewState["ThRecTbl"]).Rows.Count > 0)
                {
                    dtTh = (DataTable)ViewState["ThRecTbl"];
                }
                else
                {
                    //dtTh = this.CreateTableThesisTitle();
                }
            }
            else
            {
                //dtTh = this.CreateTableThesisTitle();
                //MessageBox("Please add Thesis Detail!");
                //return;
            }
            //
            if (txtunderGuidence.Text == string.Empty && txtAwarded.Text == string.Empty && dtIF == null && dtBD == null && dtPH == null && dtId == null && dtTh == null)
            {
                //if (((ViewState["RecTbl"] == null && ((DataTable)ViewState["RecTbl"]) == null)) && ((ViewState["BRecTbl"] == null && ((DataTable)ViewState["BRecTbl"]) == null)) && ((ViewState["PRecTbl"] == null && ((DataTable)ViewState["PRecTbl"]) == null)) && ((ViewState["IdRecTbl"] == null && ((DataTable)ViewState["IdRecTbl"]) == null)) && ((ViewState["ThRecTbl"] == null && ((DataTable)ViewState["ThRecTbl"]) == null)))
                //{

                //dtIF = this.CreateTable();
                //dtBD = this.CreateTableBond();
                //dtPH = this.CreateTablePhd();
                //dtId = this.CreateTableIDDetail();
                //dtTh = this.CreateTableThesisTitle();
                MessageBox("Please add Atleast One Detail!");
                return;

                //}
            }

            #endregion



            if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    // CustomStatus cs = (CustomStatus)objServiceBook.AddMiscellaneousOtherdetail(objSevBook);

                    CustomStatus cs = (CustomStatus)objServiceBook.AddMiscellaneousOtherdetail(objSevBook, dtIF, dtBD, dtPH, dtId, dtTh);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        string MOSNO = objCommon.LookUp("PAYROLL_SB_MIS_OTHER", "MAX(MOSNO)", "");
                        AddDocuments(Convert.ToInt32(MOSNO));
                        //objServiceBook.upload_new_files("Miscellaneous_Detail", _idnoEmp, "MOSNO", "PAYROLL_SB_MIS_OTHER", "MISOTHER_", flupld);
                        //this.Clear();
                        Clear();
                        this.BindListViewMisOtherDetail();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exist");
                        Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["MOSNO"] != null)
                    {
                        objSevBook.MOSNO = Convert.ToInt32(ViewState["MOSNO"].ToString());
                        //CustomStatus cs = (CustomStatus)objServiceBook.UpdateMiscellaneousdetails(objSevBook);
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateMiscellaneousdetails(objSevBook, dtIF, dtBD, dtPH, dtId, dtTh);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            AddDocuments(objSevBook.MOSNO);
                            // objServiceBook.update_upload("Miscellaneous_Detail", objSevBook.MOSNO, ViewState["attachment"].ToString(), _idnoEmp, "MISOTHER_", flupld);
                            ViewState["action"] = "add";
                            Clear();
                            this.BindListViewMisOtherDetail();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                            DeletePath();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OtherMisceDetail.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int MOSNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(MOSNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetails(int MOSNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleMisdetailEmployee(MOSNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["MOSNO"] = MOSNO.ToString();
                txtHindex.Text = ds.Tables[0].Rows[0]["H_Index"].ToString();
                txtbond.Text = ds.Tables[0].Rows[0]["Bond"].ToString();
                //txtphdgui.Text = ds.Tables[0].Rows[0]["PHDGUIDED"].ToString();
                //txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                //txtToDate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                //txtphdgui.Text = ds.Tables[0].Rows[0]["PHDGUIDED"].ToString();
                //txtphdawd.Text = ds.Tables[0].Rows[0]["PHDAWARD"].ToString();
                //txtCandidate.Text = ds.Tables[0].Rows[0]["CANDIDATENAME"].ToString();
                //txtDate.Text = ds.Tables[0].Rows[0]["REGDATE"].ToString();
                //txtGrant.Text = ds.Tables[0].Rows[0]["UNIVERSITYNAME"].ToString();
                //txtNo.Text = ds.Tables[0].Rows[0]["RESEARCHNAME"].ToString();
                //txtPatent.Text = ds.Tables[0].Rows[0]["GUIDENAME"].ToString();
                //txtUniversity.Text = ds.Tables[0].Rows[0]["PUBLICATIONPHDNO"].ToString();
                //txtResearch.Text = ds.Tables[0].Rows[0]["PHDGRANT"].ToString();
                //txtResGuide.Text = ds.Tables[0].Rows[0]["PHDPATENT"].ToString();

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["PHDGUIDED"].ToString()) != 0)
                {
                    txtunderGuidence.Text = ds.Tables[0].Rows[0]["PHDGUIDED"].ToString();
                }
                else
                {
                    txtunderGuidence.Text = "0";
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["PHDAWARD"].ToString()) != 0)
                {
                    txtAwarded.Text = ds.Tables[0].Rows[0]["PHDAWARD"].ToString();
                }
                else
                {
                    txtAwarded.Text = "0";
                }

                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }


            if (Convert.ToInt32(ds.Tables[1].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[1].Rows.Count;
                DataTable dtM = this.CreateTable();
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();

                    dr["SRNO"] = ds.Tables[1].Rows[i]["MOSIFID"].ToString();
                    dr["INDEXFACTOR"] = ds.Tables[1].Rows[i]["INDEXFACTOR"].ToString();
                    dr["INDEXVALUE"] = ds.Tables[1].Rows[i]["INDEXVALUE"].ToString();
                    dr["DATE"] = ds.Tables[1].Rows[i]["DATE"].ToString();

                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["RecTbl"] = dtM;
                    ViewState["SRNO"] = ds.Tables[1].Rows[i]["MOSIFID"].ToString();
                }
                lvIndexFact.DataSource = (DataTable)ViewState["RecTbl"];
                lvIndexFact.DataBind();
                pnllIndexFact.Visible = true;
            }
            else
            {
                pnllIndexFact.Visible = false;
                lvIndexFact.DataSource = null;
                lvIndexFact.DataBind();
            }



            if (Convert.ToInt32(ds.Tables[2].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[2].Rows.Count;
                DataTable dtM = this.CreateTableBond();
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();

                    dr["SBNO"] = ds.Tables[2].Rows[i]["MOSBID"].ToString();
                    dr["BOND"] = ds.Tables[2].Rows[i]["BOND"].ToString();
                    dr["FROMDATE"] = ds.Tables[2].Rows[i]["FROMDATE"].ToString();
                    dr["TODATE"] = ds.Tables[2].Rows[i]["TODATE"].ToString();
                    dr["FILENAME"] = ds.Tables[2].Rows[i]["FILENAME"].ToString();
                    dr["FILEPATH"] = ds.Tables[2].Rows[i]["FILEPATH"].ToString();

                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["BRecTbl"] = dtM;
                    ViewState["SBNO"] = ds.Tables[2].Rows[i]["MOSBID"].ToString();
                }
                lvBondList.DataSource = (DataTable)ViewState["BRecTbl"];
                lvBondList.DataBind();
                pnlBond.Visible = true;
            }
            else
            {
                pnlBond.Visible = false;
                lvBondList.DataSource = null;
                lvBondList.DataBind();
            }

            //phd details


            if (Convert.ToInt32(ds.Tables[3].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[3].Rows.Count;
                DataTable dtM = this.CreateTablePhd();
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();

                    dr["SPNO"] = ds.Tables[3].Rows[i]["MOSPHDID"].ToString();
                    dr["PHDGUIDED"] = ds.Tables[3].Rows[i]["PHDGUIDED"].ToString();
                    dr["PHDAWARD"] = ds.Tables[3].Rows[i]["PHDAWARD"].ToString();
                    dr["CANDIDATENAME"] = ds.Tables[3].Rows[i]["CANDIDATENAME"].ToString();
                    dr["REGDATE"] = ds.Tables[3].Rows[i]["REGDATE"].ToString();
                    dr["UNIVERSITYNAME"] = ds.Tables[3].Rows[i]["UNIVERSITYNAME"].ToString();
                    dr["RESEARCHNAME"] = ds.Tables[3].Rows[i]["RESEARCHNAME"].ToString();
                    dr["GUIDENAME"] = ds.Tables[3].Rows[i]["GUIDENAME"].ToString();
                    dr["PUBLICATIONPHDNO"] = ds.Tables[3].Rows[i]["PUBLICATIONPHDNO"].ToString();
                    dr["PHDGRANT"] = ds.Tables[3].Rows[i]["PHDGRANT"].ToString();
                    dr["PHDPATENT"] = ds.Tables[3].Rows[i]["PHDPATENT"].ToString();
                    dr["REGISTRATIONNO"] = ds.Tables[3].Rows[i]["REGISTRATIONNO"].ToString();
                    dr["PHDSTATUS"] = ds.Tables[3].Rows[i]["PHDSTATUS"].ToString();
                    dr["YEAR"] = ds.Tables[3].Rows[i]["YEAR"].ToString();
                    dr["THESISTITLE"] = ds.Tables[3].Rows[i]["THESISTITLE"].ToString();

                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["PRecTbl"] = dtM;
                    ViewState["SPNO"] = ds.Tables[3].Rows[i]["MOSPHDID"].ToString();
                }
                lvphd.DataSource = (DataTable)ViewState["PRecTbl"];
                lvphd.DataBind();
                panelphd.Visible = true;
            }
            else
            {
                panelphd.Visible = false;
                lvphd.DataSource = null;
                lvphd.DataBind();
            }

            //phd details
            if (Convert.ToInt32(ds.Tables[4].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[4].Rows.Count;
                DataTable dtM = this.CreateTableIDDetail();
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();

                    dr["SIDNO"] = ds.Tables[4].Rows[i]["MOSID"].ToString();
                    dr["WEB"] = ds.Tables[4].Rows[i]["WEBID"].ToString();
                    dr["SCOPUS"] = ds.Tables[4].Rows[i]["SCOPUSID"].ToString();
                    dr["ORCHID"] = ds.Tables[4].Rows[i]["ORCHIDID"].ToString();
                    dr["RESEARCH"] = ds.Tables[4].Rows[i]["SUPERVISORID"].ToString();
                    dr["GOOGLESCID"] = ds.Tables[4].Rows[i]["GOOGLESCID"].ToString();

                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["IdRecTbl"] = dtM;
                    ViewState["SIDNO"] = ds.Tables[4].Rows[i]["MOSID"].ToString();
                }
                lvIDList.DataSource = (DataTable)ViewState["IdRecTbl"];
                lvIDList.DataBind();
                pnlIDList.Visible = true;
            }
            else
            {
                pnlIDList.Visible = false;
                lvIDList.DataSource = null;
                lvIDList.DataBind();
            }

            if (Convert.ToInt32(ds.Tables[5].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[5].Rows.Count;
                DataTable dtM = this.CreateTableThesisTitle();
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();

                    dr["THIDNO"] = ds.Tables[5].Rows[i]["THID"].ToString();
                    dr["THESISTITLE"] = ds.Tables[5].Rows[i]["THESISTITLE"].ToString();
                    dr["THESISUNIVERSITY"] = ds.Tables[5].Rows[i]["UNIVERSITY"].ToString();
                    dr["MONTH"] = ds.Tables[5].Rows[i]["MONTH"].ToString();
                    dr["YEAR"] = Convert.ToInt32(ds.Tables[5].Rows[i]["YEAR"].ToString());

                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["ThRecTbl"] = dtM;
                    ViewState["THIDNO"] = ds.Tables[5].Rows[i]["THID"].ToString();
                }
                lvThesis.DataSource = (DataTable)ViewState["ThRecTbl"];
                lvThesis.DataBind();
                pnlThesisList.Visible = true;
            }
            else
            {
                pnlThesisList.Visible = false;
                lvThesis.DataSource = null;
                lvThesis.DataBind();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Clear();
        GetConfigForEditAndApprove();
    }

    public string GetFileNamePath(object filename, object MOSNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/Miscellaneous_Detail/" + idno.ToString() + "/MISOTHER_" + MOSNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lblFamilymsg.Text = string.Empty;
            ImageButton btnDel = sender as ImageButton;
            int MOSNO = int.Parse(btnDel.CommandArgument);
             DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_MIS_OTHER", "*", "", "MOSNO=" + MOSNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved You Cannot Edit.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Edit.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeletemiseInfo(MOSNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewMisOtherDetail();
                    Clear();
                    //lblFamilymsg.Text = "Record Deleted Successfully";
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    #region Index Factor
    private DataTable CreateTable()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("INDEXFACTOR", typeof(string)));
        dtRe.Columns.Add(new DataColumn("INDEXVALUE", typeof(string)));
        dtRe.Columns.Add(new DataColumn("DATE", typeof(DateTime)));
        return dtRe;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnllIndexFact.Visible = true;
            if (ViewState["dtaction"] == null)
            {
                if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
                {
                    DataTable dtShorDesc = (DataTable)ViewState["RecTbl"];
                    DataRow dr = dtShorDesc.NewRow();
                    if (CheckDuplicateName(dtShorDesc, Convert.ToDateTime(txtIndexDt.Text.Trim()), txtindexVal.Text, ddlIndexFac.SelectedItem.Text))
                    {
                        ClearRec();
                        MessageBox("Record Already Exist!");
                        return;
                    }
                    dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]);
                    dr["INDEXFACTOR"] = ddlIndexFac.SelectedItem.Text;
                    dr["INDEXVALUE"] = txtindexVal.Text.Trim();
                    string tourdate = txtIndexDt.Text.Trim() == null ? string.Empty : Convert.ToString(txtIndexDt.Text.Trim());
                    dr["DATE"] = Convert.ToDateTime(tourdate).ToString("dd/MM/yyyy");

                    dtShorDesc.Rows.Add(dr);
                    ViewState["RecTbl"] = dtShorDesc;
                    lvIndexFact.DataSource = dtShorDesc;
                    lvIndexFact.DataBind();
                    ClearRec();
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                }
                else
                {
                    DataTable dtShorDesc = this.CreateTable();
                    DataRow dr = dtShorDesc.NewRow();
                    dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dr["INDEXFACTOR"] = ddlIndexFac.SelectedItem.Text;
                    dr["INDEXVALUE"] = txtindexVal.Text.Trim();
                    string tourdate = txtIndexDt.Text.Trim() == null ? string.Empty : Convert.ToString(txtIndexDt.Text.Trim());
                    dr["DATE"] = Convert.ToDateTime(tourdate).ToString("dd/MM/yyyy"); ;

                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dtShorDesc.Rows.Add(dr);
                    dtShorDesc.DefaultView.Sort = "DATE ASC";
                    ClearRec();
                    ViewState["RecTbl"] = dtShorDesc;
                    lvIndexFact.DataSource = dtShorDesc;
                    lvIndexFact.DataBind();
                }
            }
            else
            {
                if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
                {
                    DataTable dtShorDesc = (DataTable)ViewState["RecTbl"];
                    DataRow dr = dtShorDesc.NewRow();

                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_SRNO_SHORTDESC"]);

                    dr["INDEXFACTOR"] = ddlIndexFac.SelectedItem.Text;
                    dr["INDEXVALUE"] = txtindexVal.Text.Trim();
                    string tourdate = txtIndexDt.Text.Trim() == null ? string.Empty : Convert.ToString(txtIndexDt.Text.Trim());
                    dr["DATE"] = tourdate;

                    dtShorDesc.Rows.Add(dr);

                    ViewState["RecTbl"] = dtShorDesc;
                    lvIndexFact.DataSource = dtShorDesc;
                    lvIndexFact.DataBind();
                    ClearRec();
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ClearRec()
    {
        txtIndexDt.Text = string.Empty;
        ddlIndexFac.SelectedValue = "0";
        txtindexVal.Text = string.Empty;
        ViewState["dtaction"] = null;
        ViewState["EDIT_SRNO_SHORTDESC"] = null;
    }
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{

    //}
    private DataRow GetEditableDatarow(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
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

        }
        return datRow;
    }
    protected void btnEditTr_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["EDIT_SRNO_SHORTDESC"] = string.Empty;
        ImageButton btnEditShortDesc = sender as ImageButton;
        DataTable dtShorDesc;
        if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
        {
            dtShorDesc = ((DataTable)ViewState["RecTbl"]);
            ViewState["EDIT_SRNO_SHORTDESC"] = btnEditShortDesc.CommandArgument;

            DataRow dr = this.GetEditableDatarowShorDesc(dtShorDesc, btnEditShortDesc.CommandArgument);
            ddlIndexFac.SelectedValue = dr["INDEXFACTOR"].ToString();
            txtindexVal.Text = dr["INDEXVALUE"].ToString();
            txtIndexDt.Text = dr["DATE"].ToString();
            dtShorDesc.Rows.Remove(dr);
            ViewState["RecTbl"] = dtShorDesc;
            lvIndexFact.DataSource = dtShorDesc;
            lvIndexFact.DataBind();
            ViewState["dtaction"] = "edit";
        }
    }
    private bool CheckDuplicateName(DataTable dt, DateTime value, string value1, string value2)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToDateTime(dr["DATE"].ToString()) == value && dr["INDEXVALUE"].ToString() == value1 && dr["INDEXFACTOR"].ToString() == value2)
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
    private DataRow GetEditableDatarowShorDesc(DataTable dt, string value)
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

        }
        return datRow;
    }
    protected void btnIndexDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
            {
                DataTable dtM = (DataTable)ViewState["RecTbl"];
                dtM.Rows.Remove(this.GetEditableDatarow(dtM, btnDelete.CommandArgument));
                ViewState["RecTbl"] = dtM;
                MessageBox("Record Deleted Successfully");
                if (dtM.Rows.Count > 0)
                {
                    lvIndexFact.DataSource = dtM;
                    lvIndexFact.DataBind();
                    pnllIndexFact.Visible = true;
                }
                else
                {
                    lvIndexFact.DataSource = null;
                    lvIndexFact.DataBind();
                    pnllIndexFact.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Bond Details
    private DataTable CreateTableBond()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SBNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("BOND", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FROMDATE", typeof(DateTime)));
        dtRe.Columns.Add(new DataColumn("TODATE", typeof(DateTime)));
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }
    protected void btnAddBond_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (flupld.HasFile)
            {

                if (flupld.FileContent.Length >= 1024 * 10000)
                {
                    MessageBox("File Size Should Not Be Greater Than 10 Mb");
                    flupld.Dispose();
                    flupld.Focus();
                    return;
                }
            }
            pnlBond.Visible = true;
            if (ViewState["dtbaction"] == null)
            {
                if (ViewState["BRecTbl"] != null && ((DataTable)ViewState["BRecTbl"]) != null)
                {
                    DataTable dtShorDesc = (DataTable)ViewState["BRecTbl"];
                    DataRow dr = dtShorDesc.NewRow();
                    if (CheckDuplicateBondName(dtShorDesc, Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()), txtbond.Text))
                    {
                        ClearRec();
                        MessageBox("Record Already Exist!");
                        return;
                    }
                    dr["SBNO"] = Convert.ToInt32(ViewState["SBNO"]);
                    dr["BOND"] = txtbond.Text;
                    dr["FROMDATE"] = txtFromDate.Text.Trim();
                    string date = txtToDate.Text.Trim() == null ? string.Empty : Convert.ToString(txtToDate.Text.Trim());
                    dr["TODATE"] = date;

                    if (flupld.HasFile)
                    {
                        if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
                        {
                            //if (flupld.FileContent.Length >= 1024 * 10000)
                            //{
                            //    MessageBox("File Size Should Not Be Greater Than 10 Mb");
                            //    flupld.Dispose();
                            //    flupld.Focus();
                            //    return;
                            //}

                            objSevBook.ATTACHMENTS = "Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_";
                            ViewState["attachment"] = "Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_";
                            string uploadPath = "TEMP_MISCELLANEOUS/" + _idnoEmp;
                            string uploadFile = Convert.ToString(flupld.PostedFile.FileName.ToString());
                            dr["FILENAME"] = uploadFile;
                            dr["FILEPATH"] = uploadPath;
                            SaveAttachment(uploadPath, uploadFile);
                        }
                        else
                        {
                            MessageBox("Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]");
                            flupld.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dr["FILENAME"] = string.Empty;
                        dr["FILEPATH"] = string.Empty;
                    }

                    dtShorDesc.Rows.Add(dr);
                    ViewState["BRecTbl"] = dtShorDesc;
                    lvBondList.DataSource = dtShorDesc;
                    lvBondList.DataBind();
                    ClearBondRec();
                    ViewState["SBNO"] = Convert.ToInt32(ViewState["SBNO"]) + 1;
                }
                else
                {
                    DataTable dtShorDesc = this.CreateTableBond();
                    DataRow dr = dtShorDesc.NewRow();
                    dr["SBNO"] = Convert.ToInt32(ViewState["SBNO"]) + 1;
                    dr["BOND"] = txtbond.Text;
                    dr["FROMDATE"] = txtFromDate.Text.Trim();
                    string date = txtToDate.Text.Trim() == null ? string.Empty : Convert.ToString(txtToDate.Text.Trim());
                    dr["TODATE"] = date;


                    if (flupld.HasFile)
                    {
                        if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
                        {
                            //dr["FILENAME"] = Convert.ToString(flupld.PostedFile.FileName.ToString());                           
                            objSevBook.ATTACHMENTS = "Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_";
                            ViewState["attachment"] = "Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_";
                            string uploadPath = "TEMP_MISCELLANEOUS/" + _idnoEmp;
                            string uploadFile = Convert.ToString(flupld.PostedFile.FileName.ToString());
                            dr["FILENAME"] = uploadFile;
                            dr["FILEPATH"] = uploadPath;
                            SaveAttachment(uploadPath, uploadFile);
                        }
                        else
                        {
                            MessageBox("Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]");
                            flupld.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dr["FILENAME"] = string.Empty;
                        dr["FILEPATH"] = string.Empty;
                    }


                    ViewState["SBNO"] = Convert.ToInt32(ViewState["SBNO"]) + 1;
                    dtShorDesc.Rows.Add(dr);
                    ClearBondRec();
                    ViewState["BRecTbl"] = dtShorDesc;
                    lvBondList.DataSource = dtShorDesc;
                    lvBondList.DataBind();
                }
            }
            else
            {
                if (ViewState["BRecTbl"] != null && ((DataTable)ViewState["BRecTbl"]) != null)
                {
                    DataTable dtShorDesc = (DataTable)ViewState["BRecTbl"];
                    DataRow dr = dtShorDesc.NewRow();

                    dr["SBNO"] = Convert.ToInt32(ViewState["EDIT_SBNO_SHORTDESC"]);

                    dr["BOND"] = txtbond.Text;
                    dr["FROMDATE"] = txtFromDate.Text.Trim();
                    string date = txtToDate.Text.Trim() == null ? string.Empty : Convert.ToString(txtToDate.Text.Trim());
                    dr["TODATE"] = date;

                    if (flupld.HasFile)
                    {
                        if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
                        {
                            objSevBook.ATTACHMENTS = "Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_";
                            ViewState["attachment"] = "Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_";
                            string uploadPath = "TEMP_MISCELLANEOUS/" + _idnoEmp;
                            string uploadFile = Convert.ToString(flupld.PostedFile.FileName.ToString());
                            dr["FILENAME"] = uploadFile;
                            dr["FILEPATH"] = uploadPath;
                            SaveAttachment(uploadPath, uploadFile);
                        }
                        else
                        {
                            MessageBox("Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]");
                            flupld.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dr["FILENAME"] = string.Empty;
                        dr["FILEPATH"] = string.Empty;
                    }

                    dtShorDesc.Rows.Add(dr);

                    ViewState["BRecTbl"] = dtShorDesc;
                    lvBondList.DataSource = dtShorDesc;
                    lvBondList.DataBind();
                    ClearBondRec();
                    ViewState["SBNO"] = Convert.ToInt32(ViewState["SBNO"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnEditBond_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["EDIT_SBNO_SHORTDESC"] = string.Empty;
        ImageButton btnEditShortDesc = sender as ImageButton;
        DataTable dtShorDesc;
        if (ViewState["BRecTbl"] != null && ((DataTable)ViewState["BRecTbl"]) != null)
        {
            dtShorDesc = ((DataTable)ViewState["BRecTbl"]);
            ViewState["EDIT_SBNO_SHORTDESC"] = btnEditShortDesc.CommandArgument;

            DataRow dr = this.GetEditableDatarowBond(dtShorDesc, btnEditShortDesc.CommandArgument);
            txtbond.Text = dr["BOND"].ToString();
            txtFromDate.Text = dr["FROMDATE"].ToString();
            txtToDate.Text = dr["TODATE"].ToString();
            dtShorDesc.Rows.Remove(dr);
            ViewState["BRecTbl"] = dtShorDesc;
            lvIndexFact.DataSource = dtShorDesc;
            lvIndexFact.DataBind();
            ViewState["dtbaction"] = "edit";
        }
    }
    protected void ClearBondRec()
    {
        txtbond.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        ViewState["dtbaction"] = null;
        ViewState["EDIT_SBNO_SHORTDESC"] = null;
    }
    private bool CheckDuplicateBondName(DataTable dt, DateTime value, DateTime value2, string value1)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToDateTime(dr["FROMDATE"].ToString()) == value && Convert.ToDateTime(dr["TODATE"].ToString()) == value2 && dr["BOND"].ToString() == value1)
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

    private void SaveAttachment(string Path, string uploadFile)
    {
        string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/" + Path);
        if (!System.IO.Directory.Exists(uploadPath))
        {
            System.IO.Directory.CreateDirectory(uploadPath);
        }
        //Upload the File
        if (!flupld.PostedFile.FileName.Equals(string.Empty))
        {
            flupld.PostedFile.SaveAs(uploadPath + "//" + uploadFile);
        }
    }

    protected void btnBondDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string filepath = btnDelete.AlternateText;
            string filename = btnDelete.ToolTip;

            if (ViewState["BRecTbl"] != null && ((DataTable)ViewState["BRecTbl"]) != null)
            {
                DataTable dt = (DataTable)ViewState["BRecTbl"];
                dt.Rows.Remove(this.GetEditableDatarowBond(dt, btnDelete.CommandArgument));
                ViewState["BRecTbl"] = dt;
                lvBondList.DataSource = dt;
                lvBondList.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);

                if (ViewState["DELETE_BOND"] != null && ((DataTable)ViewState["DELETE_BOND"]) != null)
                {
                    DataTable dtD = (DataTable)ViewState["DELETE_BOND"];
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = filepath;
                    dr["FILENAME"] = filename;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BOND"] = dtD;
                }
                else
                {
                    DataTable dtD = this.CreateTableAttach();
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = filepath;
                    dr["FILENAME"] = filename;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BOND"] = dtD;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private DataTable CreateTableAttach()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }

    private DataRow GetEditableDatarowBond(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["SBNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return datRow;
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
                    //DELETING THE FILE
                    File.Delete(filepath + "\\" + filename);
                }
                i++;
            }
            ViewState["DELETE_BILLS"] = null;
        }
    }
    //protected void lnkDownload_Click(object sender, System.EventArgs e)
    //{
    //    LinkButton btn = sender as LinkButton;
    //    DownloadFile(btn.CommandArgument, btn.ToolTip);
    //}

    //public void DownloadFile(string path, string fileName)
    //{
    //    try
    //    {
    //        string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/" + path);

    //        FileStream sourceFile = new FileStream((uploadPath + "\\" + fileName), FileMode.Open);
    //        long fileSize = sourceFile.Length;
    //        byte[] getContent = new byte[(int)fileSize];
    //        sourceFile.Read(getContent, 0, (int)sourceFile.Length);
    //        sourceFile.Close();
    //        sourceFile.Dispose();

    //        Response.ClearContent();
    //        Response.Clear();
    //        Response.BinaryWrite(getContent);
    //        Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
    //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

    //        HttpContext.Current.Response.Flush();
    //        HttpContext.Current.Response.SuppressContent = true;
    //        HttpContext.Current.ApplicationInstance.CompleteRequest();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Clear();
    //        Response.ContentType = "text/html";
    //        Response.Write("Unable to download the attachment.");
    //    }
    //}

    //private string GetResponseType(string fileExtension)
    //{
    //    switch (fileExtension.ToLower())
    //    {
    //        case ".doc":
    //            return "application/vnd.ms-word";
    //            break;

    //        case ".docx":
    //            return "application/vnd.ms-word";
    //            break;

    //        case ".xls":
    //            return "application/ms-excel";
    //            break;

    //        case ".xlsx":
    //            return "application/ms-excel";
    //            break;

    //        case ".pdf":
    //            return "application/pdf";
    //            break;

    //        case ".ppt":
    //            return "application/vnd.ms-powerpoint";
    //            break;

    //        case ".txt":
    //            return "text/plain";
    //            break;

    //        case "":
    //            return "";
    //            break;

    //        default:
    //            return "";
    //            break;
    //    }
    //}

    public string GetFileNamePath(object filename, object filepath)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/" + filepath + "/" + filename);
        else
            return "";
    }

    private void AddDocuments(int MOSNO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/Miscellaneous_Detail/" + _idnoEmp + "/MISOTHER_");
            sourcePath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/TEMP_MISCELLANEOUS/" + _idnoEmp);

            targetPath = uploadPath + MOSNO;

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

    #endregion


    #region PHD Details

    private DataTable CreateTablePhd()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SPNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("PHDGUIDED", typeof(string)));
        dtRe.Columns.Add(new DataColumn("PHDAWARD", typeof(string)));
        dtRe.Columns.Add(new DataColumn("CANDIDATENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("REGDATE", typeof(DateTime)));
        dtRe.Columns.Add(new DataColumn("UNIVERSITYNAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("RESEARCHNAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("GUIDENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("PUBLICATIONPHDNO", typeof(string)));
        dtRe.Columns.Add(new DataColumn("PHDGRANT", typeof(string)));
        dtRe.Columns.Add(new DataColumn("PHDPATENT", typeof(string)));
        dtRe.Columns.Add(new DataColumn("REGISTRATIONNO", typeof(string)));
        dtRe.Columns.Add(new DataColumn("PHDSTATUS", typeof(string)));
        dtRe.Columns.Add(new DataColumn("YEAR", typeof(int)));
        dtRe.Columns.Add(new DataColumn("THESISTITLE", typeof(string)));
        return dtRe;
    }

    protected void btnPhd_Click(object sender, EventArgs e)
    {
        try
        {
            panelphd.Visible = true;
            if (ViewState["dtpaction"] == null)
            {
                if (ViewState["PRecTbl"] != null && ((DataTable)ViewState["PRecTbl"]) != null)
                {
                    DataTable dtShorDesc = (DataTable)ViewState["PRecTbl"];
                    DataRow dr = dtShorDesc.NewRow();
                    if (CheckDuplicatePhd(dtShorDesc, Convert.ToDateTime(txtDate.Text.Trim()), txtphdgui.Text, txtphdawd.Text))
                    {
                        ClearPhd();
                        MessageBox("Record Already Exist!");
                        return;
                    }
                    dr["SPNO"] = Convert.ToInt32(ViewState["SPNO"]);
                    dr["PHDGUIDED"] = txtphdgui.Text.Trim();
                    dr["PHDAWARD"] = txtphdawd.Text.Trim();
                    dr["CANDIDATENAME"] = txtCandidate.Text.Trim();
                    string tourdate = txtDate.Text.Trim() == null ? string.Empty : Convert.ToString(txtDate.Text.Trim());
                    dr["REGDATE"] = Convert.ToDateTime(tourdate).ToString("dd/MM/yyyy");
                    dr["UNIVERSITYNAME"] = txtUniversity.Text.Trim();
                    dr["RESEARCHNAME"] = txtResearch.Text.Trim();
                    dr["GUIDENAME"] = txtResGuide.Text.Trim();
                    dr["PUBLICATIONPHDNO"] = txtNo.Text.Trim();
                    dr["PHDGRANT"] = txtGrant.Text.Trim();
                    dr["PHDPATENT"] = txtPatent.Text.Trim();
                    dr["REGISTRATIONNO"] = txtRegNum.Text.Trim();
                    dr["PHDSTATUS"] = rdbStatus.SelectedValue.ToString();
                    //int year =  == null ? string.Empty : Convert.ToString(txtYear.Text.Trim());
                    if (txtYear.Text != string.Empty)
                    {
                        dr["YEAR"] = txtYear.Text.Trim();
                    }
                    else
                    {
                        dr["YEAR"] = DBNull.Value;
                    }
                    dr["THESISTITLE"] = txtThesis.Text.Trim();

                    dtShorDesc.Rows.Add(dr);
                    ViewState["PRecTbl"] = dtShorDesc;
                    lvphd.DataSource = dtShorDesc;
                    lvphd.DataBind();
                    ClearPhd();
                    ViewState["SPNO"] = Convert.ToInt32(ViewState["SPNO"]) + 1;
                }
                else
                {
                    DataTable dtShorDesc = this.CreateTablePhd();
                    DataRow dr = dtShorDesc.NewRow();
                    dr["SPNO"] = Convert.ToInt32(ViewState["SPNO"]) + 1;
                    dr["PHDGUIDED"] = txtphdgui.Text.Trim();
                    dr["PHDAWARD"] = txtphdawd.Text.Trim();
                    dr["CANDIDATENAME"] = txtCandidate.Text.Trim();
                    string tourdate = txtDate.Text.Trim() == null ? string.Empty : Convert.ToString(txtDate.Text.Trim());
                    dr["REGDATE"] = Convert.ToDateTime(tourdate).ToString("dd/MM/yyyy");
                    dr["UNIVERSITYNAME"] = txtUniversity.Text.Trim();
                    dr["RESEARCHNAME"] = txtResearch.Text.Trim();
                    dr["GUIDENAME"] = txtResGuide.Text.Trim();
                    dr["PUBLICATIONPHDNO"] = txtNo.Text.Trim();
                    dr["PHDGRANT"] = txtGrant.Text.Trim();
                    dr["PHDPATENT"] = txtPatent.Text.Trim();
                    dr["REGISTRATIONNO"] = txtRegNum.Text.Trim();
                    dr["PHDSTATUS"] = rdbStatus.SelectedValue.ToString();
                    if (txtYear.Text != string.Empty)
                    {
                        dr["YEAR"] = txtYear.Text.Trim();
                    }
                    else
                    {
                        dr["YEAR"] = DBNull.Value;
                    }
                    dr["THESISTITLE"] = txtThesis.Text.Trim();

                    ViewState["SPNO"] = Convert.ToInt32(ViewState["SPNO"]) + 1;
                    dtShorDesc.Rows.Add(dr);
                    dtShorDesc.DefaultView.Sort = "REGDATE ASC";
                    ClearPhd();
                    ViewState["PRecTbl"] = dtShorDesc;
                    lvphd.DataSource = dtShorDesc;
                    lvphd.DataBind();
                }
            }
            else
            {
                if (ViewState["PRecTbl"] != null && ((DataTable)ViewState["PRecTbl"]) != null)
                {
                    DataTable dtShorDesc = (DataTable)ViewState["PRecTbl"];
                    DataRow dr = dtShorDesc.NewRow();

                    dr["SPNO"] = Convert.ToInt32(ViewState["EDIT_SPNO_SHORTDESC"]);
                    dr["PHDGUIDED"] = txtphdgui.Text.Trim();
                    dr["PHDAWARD"] = txtphdawd.Text.Trim();
                    dr["CANDIDATENAME"] = txtCandidate.Text.Trim();
                    string tourdate = txtDate.Text.Trim() == null ? string.Empty : Convert.ToString(txtDate.Text.Trim());
                    dr["REGDATE"] = Convert.ToDateTime(tourdate).ToString("dd/MM/yyyy");
                    dr["UNIVERSITYNAME"] = txtUniversity.Text.Trim();
                    dr["RESEARCHNAME"] = txtResearch.Text.Trim();
                    dr["GUIDENAME"] = txtResGuide.Text.Trim();
                    dr["PUBLICATIONPHDNO"] = txtNo.Text.Trim();
                    dr["PHDGRANT"] = txtGrant.Text.Trim();
                    dr["PHDPATENT"] = txtPatent.Text.Trim();
                    dr["REGISTRATIONNO"] = txtRegNum.Text.Trim();
                    dr["PHDSTATUS"] = rdbStatus.SelectedValue.ToString();
                    if (txtYear.Text != string.Empty)
                    {
                        dr["YEAR"] = txtYear.Text.Trim();
                    }
                    else
                    {
                        dr["YEAR"] = DBNull.Value;
                    }
                    dr["THESISTITLE"] = txtThesis.Text.Trim();

                    dtShorDesc.Rows.Add(dr);

                    ViewState["PRecTbl"] = dtShorDesc;
                    lvphd.DataSource = dtShorDesc;
                    lvphd.DataBind();
                    ClearPhd();
                    ViewState["SPNO"] = Convert.ToInt32(ViewState["SPNO"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ClearPhd()
    {
        txtphdgui.Text = string.Empty;
        txtphdawd.Text = string.Empty;
        txtCandidate.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtUniversity.Text = string.Empty;
        txtResearch.Text = string.Empty;
        txtResGuide.Text = string.Empty;
        txtNo.Text = string.Empty;
        txtGrant.Text = string.Empty;
        txtPatent.Text = string.Empty;
        txtThesis.Text = string.Empty;
        txtYear.Text = string.Empty;
        txtRegNum.Text = string.Empty;
        rdbStatus.SelectedValue = "O";
        divyear.Visible = false;

        ViewState["dtpaction"] = null;
        ViewState["EDIT_SPNO_SHORTDESC"] = null;
    }

    private bool CheckDuplicatePhd(DataTable dt, DateTime value, string value1, string value2)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToDateTime(dr["REGDATE"].ToString()) == value && dr["PHDGUIDED"].ToString() == value1 && dr["PHDAWARD"].ToString() == value2)
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


    #endregion

    protected void btnPhdDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["PRecTbl"] != null && ((DataTable)ViewState["PRecTbl"]) != null)
            {
                DataTable dtM = (DataTable)ViewState["PRecTbl"];
                dtM.Rows.Remove(this.GetEditableDatarowPhd(dtM, btnDelete.CommandArgument));
                ViewState["PRecTbl"] = dtM;
                MessageBox("Record Deleted Successfully");
                if (dtM.Rows.Count > 0)
                {
                    lvphd.DataSource = dtM;
                    lvphd.DataBind();
                    panelphd.Visible = true;
                }
                else
                {
                    lvphd.DataSource = null;
                    lvphd.DataBind();
                    panelphd.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    private DataRow GetEditableDatarowPhd(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["SPNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return datRow;
    }
    protected void rdbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbStatus.SelectedValue == "O")
        {
            divyear.Visible = false;

        }
        else
        {
            divyear.Visible = true;

        }
    }

    #region ID Details

    private DataTable CreateTableIDDetail()
    {
        DataTable dtId = new DataTable();
        dtId.Columns.Add(new DataColumn("SIDNO", typeof(int)));
        dtId.Columns.Add(new DataColumn("WEB", typeof(string)));
        dtId.Columns.Add(new DataColumn("SCOPUS", typeof(string)));
        dtId.Columns.Add(new DataColumn("ORCHID", typeof(string)));
        dtId.Columns.Add(new DataColumn("RESEARCH", typeof(string)));
        dtId.Columns.Add(new DataColumn("GOOGLESCID", typeof(string)));
        return dtId;
    }

    private bool CheckDuplicateIdDetail(DataTable dt, string value, string value1, string value2, string value3, string value4)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["WEB"].ToString() == value && dr["SCOPUS"].ToString() == value1 && dr["ORCHID"].ToString() == value2 && dr["RESEARCH"].ToString() == value3 && dr["GOOGLESCID"].ToString() == value4)
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

    protected void btnIDAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlIDList.Visible = true;
            if (ViewState["dtIDaction"] == null)
            {
                if (ViewState["IdRecTbl"] != null && ((DataTable)ViewState["IdRecTbl"]) != null)
                {
                    DataTable dtIdDet = (DataTable)ViewState["IdRecTbl"];
                    DataRow dr = dtIdDet.NewRow();
                    if (CheckDuplicateIdDetail(dtIdDet, txtWeb.Text.Trim(), txtScopus.Text, txtOrchid.Text, txtSupervisor.Text, txtGoogleScId.Text))
                    {
                        ClearIDDetails();
                        MessageBox("Record Already Exist!");
                        return;
                    }
                    dr["SIDNO"] = Convert.ToInt32(ViewState["SIDNO"]);
                    dr["WEB"] = txtWeb.Text.Trim();
                    dr["SCOPUS"] = txtScopus.Text.Trim();
                    if (txtOrchid.Text == string.Empty)
                    {
                        dr["ORCHID"] = string.Empty;
                    }
                    else
                    {
                        dr["ORCHID"] = txtOrchid.Text.Trim();
                    }
                    if (txtSupervisor.Text == string.Empty)
                    {
                        dr["RESEARCH"] = string.Empty;
                    }
                    else
                    {
                        dr["RESEARCH"] = txtSupervisor.Text.Trim();
                    }
                    if (txtGoogleScId.Text == string.Empty)
                    {
                        dr["GOOGLESCID"] = string.Empty;
                    }
                    else
                    {
                        dr["GOOGLESCID"] = txtGoogleScId.Text.Trim();
                    }

                    dtIdDet.Rows.Add(dr);
                    ViewState["IdRecTbl"] = dtIdDet;
                    lvIDList.DataSource = dtIdDet;
                    lvIDList.DataBind();
                    ClearIDDetails();
                    ViewState["SIDNO"] = Convert.ToInt32(ViewState["SIDNO"]) + 1;
                    //dtIdDet = (DataTable)ViewState["IdRecTbl"];
                    //dtIdDet.Clear();
                }
                else
                {
                    DataTable dtIdDet = this.CreateTableIDDetail();
                    DataRow dr = dtIdDet.NewRow();
                    dr["SIDNO"] = Convert.ToInt32(ViewState["SIDNO"]) + 1;
                    dr["WEB"] = txtWeb.Text.Trim();
                    dr["SCOPUS"] = txtScopus.Text.Trim();
                    if (txtOrchid.Text == string.Empty)
                    {
                        dr["ORCHID"] = string.Empty;
                    }
                    else
                    {
                        dr["ORCHID"] = txtOrchid.Text.Trim();
                    }
                    if (txtSupervisor.Text == string.Empty)
                    {
                        dr["RESEARCH"] = string.Empty;
                    }
                    else
                    {
                        dr["RESEARCH"] = txtSupervisor.Text.Trim();
                    }
                    if (txtGoogleScId.Text == string.Empty)
                    {
                        dr["GOOGLESCID"] = string.Empty;
                    }
                    else
                    {
                        dr["GOOGLESCID"] = txtGoogleScId.Text.Trim();
                    }

                    ViewState["SIDNO"] = Convert.ToInt32(ViewState["SIDNO"]) + 1;
                    dtIdDet.Rows.Add(dr);
                    //dtIdDet.DefaultView.Sort = "DATE ASC";
                    ClearIDDetails();
                    ViewState["IdRecTbl"] = dtIdDet;
                    lvIDList.DataSource = dtIdDet;
                    lvIDList.DataBind();
                    //dtIdDet = (DataTable)ViewState["IdRecTbl"];
                    //dtIdDet.Clear();
                }
            }
            else
            {
                if (ViewState["IdRecTbl"] != null && ((DataTable)ViewState["IdRecTbl"]) != null)
                {
                    DataTable dtIdDet = (DataTable)ViewState["IdRecTbl"];
                    DataRow dr = dtIdDet.NewRow();

                    dr["SIDNO"] = Convert.ToInt32(ViewState["EDIT_SIDNO_SHORTDESC"]);

                    dr["WEB"] = txtWeb.Text.Trim();
                    dr["SCOPUS"] = txtScopus.Text.Trim();
                    if (txtOrchid.Text == string.Empty)
                    {
                        dr["ORCHID"] = string.Empty;
                    }
                    else
                    {
                        dr["ORCHID"] = txtOrchid.Text.Trim();
                    }
                    if (txtSupervisor.Text == string.Empty)
                    {
                        dr["RESEARCH"] = string.Empty;
                    }
                    else
                    {
                        dr["RESEARCH"] = txtSupervisor.Text.Trim();
                    }
                    if (txtGoogleScId.Text == string.Empty)
                    {
                        dr["GOOGLESCID"] = string.Empty;
                    }
                    else
                    {
                        dr["GOOGLESCID"] = txtGoogleScId.Text.Trim();
                    }

                    dtIdDet.Rows.Add(dr);

                    ViewState["IdRecTbl"] = dtIdDet;
                    lvIDList.DataSource = dtIdDet;
                    lvIDList.DataBind();
                    ClearIDDetails();
                    ViewState["SIDNO"] = Convert.ToInt32(ViewState["SIDNO"]) + 1;
                    //dtIdDet = (DataTable)ViewState["IdRecTbl"];
                    //dtIdDet.Clear();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ClearIDDetails()
    {
        txtWeb.Text = string.Empty;
        txtScopus.Text = string.Empty;
        txtOrchid.Text = string.Empty;
        txtSupervisor.Text = string.Empty;
        txtGoogleScId.Text = string.Empty;
        ViewState["dtIDaction"] = null;
        ViewState["EDIT_SIDNO_SHORTDESC"] = null;
    }

    protected void btnIDDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnIDDelete = sender as ImageButton;
            if (ViewState["IdRecTbl"] != null && ((DataTable)ViewState["IdRecTbl"]) != null)
            {
                DataTable dtId = (DataTable)ViewState["IdRecTbl"];
                dtId.Rows.Remove(this.GetEditableDatarowforIdDetail(dtId, btnIDDelete.CommandArgument));
                ViewState["IdRecTbl"] = dtId;
                MessageBox("Record Deleted Successfully");
                if (dtId.Rows.Count > 0)
                {
                    lvIDList.DataSource = dtId;
                    lvIDList.DataBind();
                    pnlIDList.Visible = true;
                }
                else
                {
                    lvIDList.DataSource = null;
                    lvIDList.DataBind();
                    pnlIDList.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private DataRow GetEditableDatarowforIdDetail(DataTable dtId, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtId.Rows)
            {
                if (dr["SIDNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return datRow;
    }
    #endregion

    #region Thesis Evaluated

    private DataTable CreateTableThesisTitle()
    {
        DataTable dtTh = new DataTable();
        dtTh.Columns.Add(new DataColumn("THIDNO", typeof(int)));
        dtTh.Columns.Add(new DataColumn("THESISTITLE", typeof(string)));
        dtTh.Columns.Add(new DataColumn("THESISUNIVERSITY", typeof(string)));
        dtTh.Columns.Add(new DataColumn("MONTH", typeof(string)));
        dtTh.Columns.Add(new DataColumn("YEAR", typeof(int)));
        return dtTh;
    }

    protected void btnThesis_Click(object sender, EventArgs e)
    {
        try
        {
            pnlThesisList.Visible = true;
            if (ViewState["dtThesisaction"] == null)
            {
                if (ViewState["ThRecTbl"] != null && ((DataTable)ViewState["ThRecTbl"]) != null)
                {
                    DataTable dtTh = (DataTable)ViewState["ThRecTbl"];
                    DataRow dr = dtTh.NewRow();
                    if (CheckDuplicateThesisDetail(dtTh, txtThesisTitle.Text.Trim(), txtThesisUniversity.Text, txtMonth.Text, txtThesisYear.Text))
                    {
                        ClearThesisDetails();
                        MessageBox("Record Already Exist!");
                        return;
                    }
                    dr["THIDNO"] = Convert.ToInt32(ViewState["THIDNO"]);
                    dr["THESISTITLE"] = txtThesisTitle.Text.Trim();
                    dr["THESISUNIVERSITY"] = txtThesisUniversity.Text.Trim();
                    if (txtMonth.Text == string.Empty)
                    {
                        dr["MONTH"] = string.Empty;
                    }
                    else
                    {
                        dr["MONTH"] = txtMonth.Text.Trim();
                    }
                    if (txtThesisYear.Text == string.Empty)
                    {
                        dr["YEAR"] = DBNull.Value;
                    }
                    else
                    {
                        if (txtThesisYear.Text.Length < 4)
                        {
                            MessageBox("Please Enter 4 Digit Year!");
                            txtThesisYear.Text = string.Empty;
                            txtThesisYear.Focus();
                            return;
                        }
                        else
                        {
                            dr["YEAR"] = txtThesisYear.Text.Trim();
                        }
                    }

                    dtTh.Rows.Add(dr);
                    ViewState["ThRecTbl"] = dtTh;
                    lvThesis.DataSource = dtTh;
                    lvThesis.DataBind();
                    ClearThesisDetails();
                    ViewState["THIDNO"] = Convert.ToInt32(ViewState["THIDNO"]) + 1;
                    //dtIdDet = (DataTable)ViewState["IdRecTbl"];
                    //dtIdDet.Clear();
                }
                else
                {
                    DataTable dtTh = this.CreateTableThesisTitle();
                    DataRow dr = dtTh.NewRow();
                    dr["THIDNO"] = Convert.ToInt32(ViewState["THIDNO"]) + 1;
                    dr["THESISTITLE"] = txtThesisTitle.Text.Trim();
                    dr["THESISUNIVERSITY"] = txtThesisUniversity.Text.Trim();
                    if (txtMonth.Text == string.Empty)
                    {
                        dr["MONTH"] = string.Empty;
                    }
                    else
                    {
                        dr["MONTH"] = txtMonth.Text.Trim();
                    }
                    if (txtThesisYear.Text == string.Empty)
                    {
                        dr["YEAR"] = DBNull.Value;
                    }
                    else
                    {
                        if (txtThesisYear.Text.Length < 4)
                        {
                            MessageBox("Please Enter 4 Digit Year!");
                            txtThesisYear.Text = string.Empty;
                            txtThesisYear.Focus();
                            return;
                        }
                        else
                        {
                            dr["YEAR"] = txtThesisYear.Text.Trim();
                        }
                    }

                    ViewState["THIDNO"] = Convert.ToInt32(ViewState["THIDNO"]) + 1;
                    dtTh.Rows.Add(dr);
                    //dtIdDet.DefaultView.Sort = "DATE ASC";
                    ClearThesisDetails();
                    ViewState["ThRecTbl"] = dtTh;
                    lvThesis.DataSource = dtTh;
                    lvThesis.DataBind();
                }
            }
            else
            {
                if (ViewState["ThRecTbl"] != null && ((DataTable)ViewState["ThRecTbl"]) != null)
                {
                    DataTable dtTh = (DataTable)ViewState["ThRecTbl"];
                    DataRow dr = dtTh.NewRow();

                    dr["THIDNO"] = Convert.ToInt32(ViewState["EDIT_THIDNO_SHORTDESC"]);

                    dr["THESISTITLE"] = txtThesisTitle.Text.Trim();
                    dr["THESISUNIVERSITY"] = txtThesisUniversity.Text.Trim();
                    if (txtMonth.Text == string.Empty)
                    {
                        dr["MONTH"] = string.Empty;
                    }
                    else
                    {
                        dr["MONTH"] = txtMonth.Text.Trim();
                    }
                    if (txtThesisYear.Text == string.Empty)
                    {
                        dr["YEAR"] = DBNull.Value;
                    }
                    else
                    {
                        if (txtThesisYear.Text.Length < 4)
                        {
                            MessageBox("Please Enter 4 Digit Year!");
                            txtThesisYear.Text = string.Empty;
                            txtThesisYear.Focus();
                            return;
                        }
                        else
                        {
                            dr["YEAR"] = txtThesisYear.Text.Trim();
                        }
                    }

                    dtTh.Rows.Add(dr);

                    ViewState["ThRecTbl"] = dtTh;
                    lvThesis.DataSource = dtTh;
                    lvThesis.DataBind();
                    ClearThesisDetails();
                    ViewState["THIDNO"] = Convert.ToInt32(ViewState["THIDNO"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private bool CheckDuplicateThesisDetail(DataTable dt, string value, string value1, string value2, string value3)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["THESISTITLE"].ToString() == value && dr["THESISUNIVERSITY"].ToString() == value1 && dr["MONTH"].ToString() == value2 && dr["YEAR"].ToString() == value3)
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

    protected void ClearThesisDetails()
    {
        txtThesisTitle.Text = string.Empty;
        txtThesisUniversity.Text = string.Empty;
        txtMonth.Text = string.Empty;
        txtThesisYear.Text = string.Empty;
        ViewState["dtThesisaction"] = null;
        ViewState["EDIT_THIDNO_SHORTDESC"] = null;
    }

    protected void btnThesisDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnThesisDelete = sender as ImageButton;
            if (ViewState["ThRecTbl"] != null && ((DataTable)ViewState["ThRecTbl"]) != null)
            {
                DataTable dtTh = (DataTable)ViewState["ThRecTbl"];
                dtTh.Rows.Remove(this.GetEditableDatarowforThesis(dtTh, btnThesisDelete.CommandArgument));
                ViewState["ThRecTbl"] = dtTh;
                MessageBox("Record Deleted Successfully");
                if (dtTh.Rows.Count > 0)
                {
                    lvThesis.DataSource = dtTh;
                    lvThesis.DataBind();
                    pnlThesisList.Visible = true;
                }
                else
                {
                    lvThesis.DataSource = null;
                    lvThesis.DataBind();
                    pnlThesisList.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private DataRow GetEditableDatarowforThesis(DataTable dtTh, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtTh.Rows)
            {
                if (dr["THIDNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return datRow;
    }

    #endregion
    protected void btnEditPhdDetails_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["EDIT_SPNO_SHORTDESC"] = string.Empty;
        ImageButton btnEditShortDesc = sender as ImageButton;
        DataTable dtShorDesc;
        if (ViewState["PRecTbl"] != null && ((DataTable)ViewState["PRecTbl"]) != null)
        {
            dtShorDesc = ((DataTable)ViewState["PRecTbl"]);
            ViewState["EDIT_SPNO_SHORTDESC"] = btnEditShortDesc.CommandArgument;

            DataRow dr = this.GetEditableDatarowPhd(dtShorDesc, btnEditShortDesc.CommandArgument);
            txtphdgui.Text = dr["PHDGUIDED"].ToString();
            txtphdawd.Text = dr["PHDAWARD"].ToString();
            txtCandidate.Text = dr["CANDIDATENAME"].ToString();
            txtDate.Text = dr["REGDATE"].ToString();
            txtUniversity.Text = dr["UNIVERSITYNAME"].ToString();
            txtResearch.Text = dr["RESEARCHNAME"].ToString();
            txtResGuide.Text = dr["GUIDENAME"].ToString();
            txtNo.Text = dr["PUBLICATIONPHDNO"].ToString();
            txtGrant.Text = dr["PHDGRANT"].ToString();
            txtPatent.Text = dr["PHDPATENT"].ToString();
            txtRegNum.Text = dr["REGISTRATIONNO"].ToString();
            rdbStatus.Text = dr["PHDSTATUS"].ToString();
            txtYear.Text = dr["YEAR"].ToString();
            txtThesis.Text = dr["THESISTITLE"].ToString();
            dtShorDesc.Rows.Remove(dr);
            ViewState["PRecTbl"] = dtShorDesc;
            lvphd.DataSource = dtShorDesc;
            lvphd.DataBind();
            ViewState["dtaction"] = "edit";
        }
    }
    protected void btnEditIdDetails_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["EDIT_SIDNO_SHORTDESC"] = string.Empty;
        ImageButton btnEditShortDesc = sender as ImageButton;
        DataTable dtShorDesc;
        if (ViewState["IdRecTbl"] != null && ((DataTable)ViewState["IdRecTbl"]) != null)
        {
            dtShorDesc = ((DataTable)ViewState["IdRecTbl"]);
            ViewState["EDIT_SIDNO_SHORTDESC"] = btnEditShortDesc.CommandArgument;

            DataRow dr = this.GetEditableDatarowforIdDetail(dtShorDesc, btnEditShortDesc.CommandArgument);
            txtWeb.Text = dr["WEB"].ToString();
            txtScopus.Text = dr["SCOPUS"].ToString();
            txtOrchid.Text = dr["ORCHID"].ToString();
            txtSupervisor.Text = dr["RESEARCH"].ToString();
            txtGoogleScId.Text = dr["GOOGLESCID"].ToString();
            dtShorDesc.Rows.Remove(dr);
            ViewState["IdRecTbl"] = dtShorDesc;
            lvIDList.DataSource = dtShorDesc;
            lvIDList.DataBind();
            ViewState["dtaction"] = "edit";
        }
    }
    protected void btnEditThesisDetails_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["EDIT_THIDNO_SHORTDESC"] = string.Empty;
        ImageButton btnEditShortDesc = sender as ImageButton;
        DataTable dtShorDesc;
        if (ViewState["ThRecTbl"] != null && ((DataTable)ViewState["ThRecTbl"]) != null)
        {
            dtShorDesc = ((DataTable)ViewState["ThRecTbl"]);
            ViewState["EDIT_THIDNO_SHORTDESC"] = btnEditShortDesc.CommandArgument;

            DataRow dr = this.GetEditableDatarowforThesis(dtShorDesc, btnEditShortDesc.CommandArgument);
            txtThesisTitle.Text = dr["THESISTITLE"].ToString();
            txtThesisUniversity.Text = dr["THESISUNIVERSITY"].ToString();
            txtMonth.Text = dr["MONTH"].ToString();
            txtThesisYear.Text = dr["YEAR"].ToString();
            dtShorDesc.Rows.Remove(dr);
            ViewState["ThRecTbl"] = dtShorDesc;
            lvThesis.DataSource = dtShorDesc;
            lvThesis.DataBind();
            ViewState["dtaction"] = "edit";
        }
    }

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Miscellaneous Detail";
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