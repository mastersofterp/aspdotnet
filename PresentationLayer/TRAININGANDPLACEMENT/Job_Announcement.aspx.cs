using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class Job_Announcement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    BlobController objBlob = new BlobController();
    Panel panelfordropdown;

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
        try
        {
            if (!Page.IsPostBack)
            {
                FileUploadCompany.Enabled = false;
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";

                    //objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "COMPREQUEST=2", "COMPNAME"); //15-11-2022
                    objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "COMPREQUEST=2", "COMPNAME");
                    objCommon.FillDropDownList(ddlJobType, "ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "STATUS=1", "JOBTYPE");
                    objCommon.FillDropDownList(ddlJobRole, "ACD_TP_JOB_ROLE A inner join ACD_TP_JOBTYPE B on (A.JOBNO = B.JOBNO)", "A.ROLENO", "concat(B.JOBTYPE,' - ',A.JOBROLETYPE)", "A.STATUS=1", "JOBROLETYPE");
                    //objCommon.FillDropDownList(ddlPlacement, "ACD_TP_PLACEMENT_STATUS", "STATUS_NO", "PLACED_STATUS", "", "PLACED_STATUS");
                    objCommon.FillDropDownList(ddlCurrency, "ACD_CURRENCY", "CUR_NO", "CUR_NAME", "STATUS=1", "CUR_NAME");
                    objCommon.FillDropDownList(ddlInterval, "ACD_TP_INTERVALS", "INTNO", "INTERVALS", "STATUS=1", "INTERVALS");
                    //objCommon.FillDropDownList(ddlRound1, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "STATUS=1", "SELECTNAME");
                    objCommon.FillListBox(lstbxCity, "ACD_CITY ", "CITYNO", "CITY ", "CITYNO >0", "CITYNO ");

                    //objCommon.FillDropDownList(ddlRound2, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", " SELECTNO NOT IN (" + Convert.ToInt32(ddlRound1.SelectedValue) + ")", "SELECTNAME");
                    //objCommon.FillListBox(lstbxCareerAreas, "ACD_TP_WORK_AREA ", "WORKAREANO", "WORKAREANAME ", "", "WORKAREANAME ");
                    objCommon.FillListBox(lstbxSemester, "ACD_SEMESTER ", "SEMESTERNO", "SEMESTERNAME ", "SEMESTERNO>0", "SEMESTERNO");
                    objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
                    //objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTIONNAME");
                    objCommon.FillDropDownList(ddlRound, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNO NOT IN (0) and STATUS=1", "SELECTNAME");
                    objCommon.FillDropDownList(ddlPlacement, "ACD_TP_PLACEMENT_STATUS", "STATUS_NO", "PLACED_STATUS", "STATUS=1", "STATUS_NO");
                    //  objCommon.FillListBox(lstbxProgramName, "ACD_BRANCH A inner join ACD_DEGREE B on (A.COLLEGE_CODE=B.COLLEGE_CODE) ", "DEGREENO", "CONCAT(LONGNAME,' ',DEGREENAME) as PROGRAME  ", "", "PROGRAME");
                    btnSpecifyRange.Text = "SPECIFY RANGE";
                    //  Session["announcement"] = null;
                    BlobDetails();//Added by Parag

                }
                BindListViewAddCompany();
                pnlmin.Visible = false;
                pnlmax.Visible = false;
                Session["announcement"] = null;
                Session["TblRound"] = null;
                ViewState["ACOMSCHNO"] = null;
                txtSSC.Text = "0.00";
                txtHSC.Text = "0.00";
                txtDiploma.Text = "0.00";
                txtUG.Text = "0.00";
                txtPG.Text = "0.00";

                upnlAnnouncedFor.Visible = true;
                divbtn.Visible = true;
                lvJobAnnouncement.Visible = true;
                Createtable_RoundDet();
                // placeH.Visible = true;
            }

            //Literal lit;
            //        panelfordropdown = new Panel();
            //        panelfordropdown.ID = "pnldropdown";
            //        panelfordropdown.Width = 400;
            //        this.pnlround1.Controls.Add(panelfordropdown);

            //        lit = new Literal();
            //        lit.Text = "<br>";
            //        this.pnlround1.Controls.Add(lit);
            //        LinkButton lnkb = new LinkButton();
            //        lnkb.ID = "btnplus";
            //        lnkb.Text = "click to add dropdown";
            //        lnkb.Click += new System.EventHandler(clicktoaddddl);
            //        this.pnlround1.Controls.Add(lnkb);


            //        if (IsPostBack)
            //        {
            //            Controlcreate("DynamicText", "Dropdown");
            //        }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void Controlcreate(string p1, string p2)
    //{
    //   // throw new NotImplementedException();
    //    string[] control = Request.Form.ToString().Split('&');
    //    int Count = FindControlCount(p1);
    //    if (Count > 0)
    //    {
    //        Literal lit;
    //        for (int k = 0; k < Count; k++)
    //        {
    //            for (int i = 0; i < control.Length; i++)
    //            {
    //                if (Controls[i].Contains(p1 + "-" + k.ToString()))
    //                {
    //                    string contrlname=Controls[i].Split('')
    //                }
    //            }
    //        }
    //    }
    //}

    //private void clicktoaddddl(object sender, EventArgs e)
    //{
    //    //throw new NotImplementedException();
    //    Button btn = (Button)sender;
    //    if (btn.ID == "lnkb")
    //    {

    //        int count = FindControlCount("DynamicText");
    //        DropDownList ddl = new DropDownList();
    //        ddl.ID = "DynamicText-" + Convert.ToString(count + 1);
    //        panelfordropdown.Controls.Add(ddl);
    //        Literal liit = new Literal();
    //        liit.Text = "<br/>";
    //        panelfordropdown.Controls.Add(liit);

    //    }
    //}

    //private int FindControlCount(string p)
    //{
    //    //throw new NotImplementedException();
    //    string str = Request.Form.ToString();
    //    return ((str.Length - str.Replace(p, "").Length) / p.Length);

    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt;
        // DataTable dtr = (DataTable)Session["announcement"];

        if (Session["announcement"] == null)
        {
            //objCommon.DisplayMessage("Please Add Announcement for Details !", this.Page);
        }
        //string StartEndDate = hdnDate.Value;
        //string[] dates = new string[] { };
        //if ((StartEndDate) == "")//GetDocs()
        //{
        //    objCommon.DisplayMessage( "Please select FROM Date TO Date !", this.Page);
        //    return;
        //}
        //else
        //{
        //    StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
        //    //string[]
        //    dates = StartEndDate.Split('-');
        //}
        //foreach(ListViewDataItem dataitem in lvannouncefor.Items)
        //{
        //  //  ListView lvdetail = dataitem.FindControl("lvannouncefor") as ListView;
        if (this.lvRoundDetail.Items.Count == 0)
        {
            objCommon.DisplayMessage("Please select Round Details !", this.Page);
            return;
        }

        if (this.lvannouncefor.Items.Count == 0)
        {
            objCommon.DisplayMessage("Please select Announce for Details !", this.Page);
            return;
        }
        //}
        //   string StartDate = dates[0];//Jul 15, 2021
        // string EndDate = dates[1];
        //DateTime dateTime10 = Convert.ToDateTime(a);
        DateTime dtStartDate = DateTime.Parse(txtSchFromDate.Text);
        string SDate = dtStartDate.ToString("yyyy/MM/dd");
        DateTime dtEndDate = DateTime.Parse(txtSchToDate.Text);
        string EDate = dtEndDate.ToString("yyyy/MM/dd");
        DateTime dtLastDate = DateTime.Parse(txtLastDate.Text);
        string LDate = dtLastDate.ToString("yyyy/MM/dd");
        try
        {
            if (SDate != string.Empty && EDate != string.Empty)
            {
                if (Convert.ToDateTime(SDate) > Convert.ToDateTime(EDate))
                {
                    objCommon.DisplayMessage(this.upnlsalary, "Schedule TO DATE should be greater than Schedule FROM Date", this.Page);
                    return;
                }
            }

            if (Convert.ToDateTime(LDate) > Convert.ToDateTime(SDate))
            {
                objCommon.DisplayMessage(this.upnlsalary, "Scheduled Last Date should not be greater than Schedule From Date and To Date", this.Page);
                return;
            }
            // else
            {

                objTP.COMPID = Convert.ToInt32(ddlCompanyName.SelectedValue);
                objTP.JOBTYPE = Convert.ToInt32(ddlJobType.SelectedValue);
                objTP.JobRole = Convert.ToInt32(ddlJobRole.SelectedValue);
                objTP.PlacementMode = Convert.ToInt32(ddlPlacement.SelectedValue);

                string City = "";

                foreach (ListItem items in lstbxCity.Items)
                {
                    if (items.Selected == true)
                    {
                        //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        City += items.Value + ',';
                    }
                }
                objTP.CITY = City.Remove(City.Length - 1);

                if (chkAnywhere.Checked)
                {
                    objTP.anywhereinsrilanka = 1;
                }
                else if (chkAnywhere.Checked == false)
                {
                    objTP.anywhereinsrilanka = 0;
                }
                objTP.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

                objTP.INTERVIEWFROM = Convert.ToDateTime(SDate);
                objTP.INTERVIEWTO = Convert.ToDateTime(EDate);
                objTP.Venue = txtVenue.Text;


                //if (Convert.ToDateTime(txtLastDate.Text) < (Convert.ToDateTime(EDate)))
                //{
                objTP.LASTDATE = Convert.ToDateTime(txtLastDate.Text);
                //}
                // else {
                //  objCommon.DisplayMessage(this.upnlsalary, "Last Date to Apply is Less Than Schedule To Date.", this.Page);
                //   return;
                //  }

                objTP.JobDiscription = "" + Convert.ToString(hfdTemplate.Value).Replace(",", "^") + "";

                objTP.CRITERIA = "" + Convert.ToString(hfdEligibility.Value).Replace(",", "^") + "";

                //added by AMIT PANDEY
                objTP.SSCPER = Convert.ToDecimal(txtSSC.Text);
                if (txtHSC.Text == "0.00" && txtDiploma.Text != "0.00")
                {
                    objTP.HSCPER = Convert.ToDecimal(0.0);
                    objTP.DIPLOMAPER = Convert.ToDecimal(txtDiploma.Text);
                }
                else if (txtHSC.Text != "0.00" && txtDiploma.Text == "0.00")
                {
                    objTP.HSCPER = Convert.ToDecimal(txtHSC.Text);
                    objTP.DIPLOMAPER = Convert.ToDecimal(0.0);
                }
                if (txtHSC.Text == string.Empty && txtDiploma.Text == string.Empty)
                {
                    objCommon.DisplayMessage("Please Enter HSC Percentage or Diploma Percentage", this.Page);
                }

                objTP.UGPER = Convert.ToDecimal(txtUG.Text);
                if (txtPG.Text == string.Empty)
                {
                    objTP.PGPER = Convert.ToDecimal(0.0);
                }
                else
                {
                    objTP.PGPER = Convert.ToDecimal(txtPG.Text);
                }

                //end

                objTP.Amount = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtAmount.Text);
                objTP.MinAmount = string.IsNullOrEmpty(txtMinAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtMinAmount.Text);
                objTP.MaxAmount = string.IsNullOrEmpty(txtMaxAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtMaxAmount.Text);
                objTP.SalDetails = txtAddDetails.Text;
                objTP.Currency = Convert.ToInt32(ddlCurrency.SelectedValue);
                objTP.Interval = Convert.ToInt32(ddlInterval.SelectedValue);
                string r1 = null, r2 = null, r3 = null, r4 = null;

                //r1= ddlRound1.SelectedValue;

                // r2=ddlRound2.SelectedValue;

                //     r3 = ddlRound3.SelectedValue;

                //    r4 = ddlRound4.SelectedValue;


                objTP.SELECTNO = r1 + "," + r2 + "," + r3 + "," + r4;

                // objTP.SELECTNO = r1 + "," + r2 + "," + r3 + "," + r4;
                objTP.RoundDiscription = "" + Convert.ToString(hfDescription.Value).Replace(",", "^") + "";
                //dtround = Session["TblRound"];

                DataTable btround = (DataTable)ViewState["Round"];
                objTP.TP_ROUND_TBL = btround;
                //int faculty = 0;
                //string  Studylevel = string.Empty;
                //string Programno = string.Empty;
                //string semno = string.Empty;

                //if (Session["ProjTbl"] != null && ((DataTable)Session["ProjTbl"]) != null)
                //{

                // dt = (DataTable)Session["announcement"];
                DataTable btannounce = (DataTable)Session["announcement"];
                objTP.TP_ANNOUNCE_FOR_TBL = btannounce;

                string studentIds = "";
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    // isProj = true;
                //    int faculty = Convert.ToInt32(dt.Rows[i]["Faculty"].ToString());
                //    string Studylevel = dt.Rows[i]["Study Level"].ToString();
                //    string Programno = dt.Rows[i]["Program Name"].ToString();
                //    string semno = dt.Rows[i]["SEMESTERNO"].ToString();


                //    //studentIds += Convert.ToString(IDNO.Value) + "$";

                //    objTP.Faculty = faculty;
                //    objTP.StudyLevel = Studylevel;
                //    objTP.Program = Programno;
                //    objTP.Semester = semno;
                //    ViewState["ProgramName"] = dt.Rows[i]["Program Name"].ToString();
                //}




                //string ss = "r";
                //DataTable dta = new DataTable();
                //SqlDataAdapter sda = new SqlDataAdapter();
                //sda.SelectCommand = cm;
                //sda.Fill(dt);
                //if (dt != null)
                //{s
                //    DataTable table = new DataTable();
                //    table = dt;
                //    ViewState["Row"] = table;
                //    //lv1.DataSource = ViewState["Row"];
                //    lv1.DataSource = dt;
                //    lv1.DataBind();
                //    lv1.Visible = true;
                //}

                //    DataTable dt = new DataTable();
                //    DataTable dtt = new DataTable();
                //    dtt = dt;
                //    DataSet ds = objCompany.GetIdEditJobAnnouncemnt(Compid);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //string Program = lstbxProgramName.SelectedValue;
                //string Program = ViewState["ProgramName"].ToString();
                //    if (Program!=string.Empty)
                //    {
                //    string[] subs = Program.Split(',');
                //    ViewState["Branchno"] = Convert.ToInt32(subs[0]);
                //    ViewState["Degreeno"] = Convert.ToInt32(subs[1]);
                //   // ViewState["Branchno"] =  0 ;
                //  //  ViewState["Degreeno"] =  0 ;
                //objTP.BRANCHNO=Convert.ToInt32(ViewState["Branchno"]);
                //objTP.DEGREE = Convert.ToInt32(ViewState["Degreeno"]);
                //    }


                //Blob File Upload Code
                string file = string.Empty;
                if (FileUploadCompany.HasFile)
                {
                    if (FileTypeValid(System.IO.Path.GetExtension(FileUploadCompany.FileName)))
                    {
                        if (FileUploadCompany.HasFile)
                        {
                            if (FileUploadCompany.FileContent.Length >= 1024 * 500)
                            {

                                objCommon.DisplayMessage(this.upnlsalary, "File Size Should Not Be Greater Than 500 kb", this.Page);
                                FileUploadCompany.Dispose();
                                FileUploadCompany.Focus();
                                return;
                            }
                        }

                        if (lblBlobConnectiontring.Text == "")
                        {
                            objTP.ISBLOB = 0;
                        }
                        else
                        {
                            objTP.ISBLOB = 1;
                        }
                        if (objTP.ISBLOB == 1)
                        {
                            string filename = string.Empty;
                            string FilePath = string.Empty;
                            // string IdNo = _idnoEmp.ToString();
                            if (FileUploadCompany.HasFile)
                            {
                                string contentType = contentType = FileUploadCompany.PostedFile.ContentType;
                                string ext = System.IO.Path.GetExtension(FileUploadCompany.PostedFile.FileName);
                                string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                                string[] split = FileUploadCompany.FileName.Split('.');
                                string firstfilename = string.Join(".", split.Take(split.Length - 1));
                                string lastfilename = split.Last();
                                file = firstfilename + "_" + time + "." + lastfilename;
                                if (FileUploadCompany.FileContent.Length <= 1024 * 10000)
                                {
                                    string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                    string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                    bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                    if (result == true)
                                    {

                                        int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, firstfilename + "_" + time + "." + lastfilename, FileUploadCompany);
                                        if (retval == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                            return;
                                        }
                                        else
                                        {
                                            objTP.FileName = file;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.pdf]", this.Page);
                        FileUploadCompany.Focus();
                    }

                }
                else
                {
                    objTP.FileName = "";
                }


                //


                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        int org = Convert.ToInt32(Session["OrgId"]);
                        CustomStatus cs = (CustomStatus)objCompany.AddJobAnnouncement(objTP, org);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                            ViewState["action"] = "add";
                            //Response.Redirect(Request.Url.ToString());
                            Clear();
                            BindListViewAddCompany();

                        }
                    }
                    else
                    {
                        if (ViewState["ACOMSCHNO"] != null)
                        {
                            objTP.SCHEDULENO = 0;
                            objTP.ACOMSCHNO = Convert.ToInt32(ViewState["ACOMSCHNO"]);
                            //int org = Convert.ToInt32(Session["OrgId"]);
                            // objTP.SCHEDULENO = Convert.ToInt32(ViewState["COMPID"]);
                            int org = Convert.ToInt32(Session["OrgId"]);
                            CustomStatus CS = (CustomStatus)objCompany.UpdJobAnnouncement(objTP, org);
                            if (CS.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage(Page, "Record Updated Successfully.", this.Page);
                                ViewState["action"] = null;
                                Clear();
                                BindListViewAddCompany();
                            }
                            ViewState["ACOMSCHNO"] = null;
                        }
                    }

                    Session["announcement"] = null;
                }
                //}
                //pnlr2.Visible = false;
                // pnlr3.Visible = false;
                // pnlr4.Visible = false;
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






    protected void chkNoSalary_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNoSalary.Checked == true)
        {
            txtAmount.Enabled = false;
            btnSpecifyRange.Enabled = false;
            txtMinAmount.Enabled = false;
            txtMaxAmount.Enabled = false;
            txtAddDetails.Enabled = false;
            ddlCurrency.Enabled = false;
            ddlInterval.Enabled = false;
        }
    }
    protected void chkSalaryAnnLater_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSalaryAnnLater.Checked == true)
        {
            txtAmount.Enabled = false;
            btnSpecifyRange.Enabled = false;
            txtMinAmount.Enabled = false;
            txtMaxAmount.Enabled = false;
            txtAddDetails.Enabled = false;
            ddlCurrency.Enabled = false;
            ddlInterval.Enabled = false;
        }
    }

    protected void btnSpecifyRange_Click(object sender, EventArgs e)
    {

        if (btnSpecifyRange.Text == "SPECIFY RANGE")
        {
            pnlamount.Visible = false;
            txtAmount.Visible = false;
            lblAmount.Visible = false;
            txtMinAmount.Visible = true;
            txtMaxAmount.Visible = true;
            lblMinAmount.Visible = true;
            lblMaxAmount.Visible = true;
            pnlmin.Visible = true;
            pnlmax.Visible = true;
            btnSpecifyRange.Text = "SPECIFY AMOUNT";
        }
        else if (btnSpecifyRange.Text == "SPECIFY AMOUNT")
        {
            pnlmin.Visible = false;
            pnlmax.Visible = false;
            txtMinAmount.Visible = false;
            txtMaxAmount.Visible = false;
            lblMinAmount.Visible = false;
            lblMaxAmount.Visible = false;
            txtAmount.Visible = true;
            lblAmount.Visible = true;
            pnlamount.Visible = true;
            btnSpecifyRange.Text = "SPECIFY RANGE";
        }

    }

    private void BindListViewAddCompany()
    {
        try
        {
            DataSet ds = objCompany.JobAnnouncementBindLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvJobAnnouncement.DataSource = ds;
                lvJobAnnouncement.DataBind();
                ViewState["TSno"] = Convert.ToInt32(ds.Tables[0].Rows.Count);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditJobAnnouncement_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            ViewState["ACOMSCHNO"] = int.Parse(btnEdit.CommandArgument);
            int ACOMSCHNO = Convert.ToInt32(ViewState["ACOMSCHNO"]);
            //int Compid = int.Parse(btnEdit.CommandArgument);
            ViewState["ACOMSCHNO"] = ACOMSCHNO;
            ViewState["action"] = "edit";
            //int Count = Convert.ToInt32(objCommon.LookUp("ACD_TP_COMPSCHEDULE A INNER JOIN ACD_TP_REGISTER B ON (A.SCHEDULENO=B.SCHEDULENO)", "Count(1)", "A.ACOMSCHNO='" + ACOMSCHNO + "'"));
            //if(Count==0)
            //{
            //    this.ShowDetailsJobAnnouncement(ACOMSCHNO);
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Student Use This Schedule .Record Can Not Be Modify. !", this.Page);
            //    return;
            //}
            this.ShowDetailsJobAnnouncement(ACOMSCHNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetailsJobAnnouncement(int ACOMSCHNO)
    {
        try
        {
            char delimiterChars = ',';
            char delimiter = ',';
            DataSet ds = objCompany.GetIdEditJobAnnouncemnt(ACOMSCHNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCompanyName.SelectedValue = ds.Tables[0].Rows[0]["COMPID"].ToString();
                ddlCompanyName.Enabled = false;
                ddlJobType.SelectedValue = ds.Tables[0].Rows[0]["JOBTYPE"].ToString();
                objCommon.FillDropDownList(ddlJobRole, "ACD_TP_JOB_ROLE", "ROLENO", "JOBROLETYPE", "STATUS=1", "JOBROLETYPE");
                ddlJobRole.SelectedValue = ds.Tables[0].Rows[0]["JOBROLE"].ToString();
                //ddlPlacement.SelectedValue = ds.Tables[0].Rows[0]["SELECTNO"].ToString();
                ddlPlacement.SelectedValue = (ds.Tables[0].Rows[0]["PLACEMENT_MODE"] == null) ? string.Empty : ds.Tables[0].Rows[0]["PLACEMENT_MODE"].ToString();
                // ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["Faculty"].ToString();
                //   objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B on (A.UA_SECTION=B.UGPGOT)", "DISTINCT (UA_SECTION)", "A.UA_SECTIONNAME", "B.COLLEGE_ID=" + ddlFaculty.SelectedValue, "(UA_SECTION)");
                // ddlStudyLevel.SelectedValue = ds.Tables[0].Rows[0]["Study Level"].ToString();
                //ddlStudyLevel.SelectedValue = "1";
                //ddlPlacement.SelectedValue = ds.Tables[0].Rows[0]["SELECTNO"].ToString();
                string city = ds.Tables[0].Rows[0]["CITY"].ToString();

                string[] utype = city.Split(delimiterChars);
                string[] DeptTypes = city.Split(delimiter);

                for (int j = 0; j < utype.Length; j++)
                {
                    for (int i = 0; i < lstbxCity.Items.Count; i++)
                    {
                        if (utype[j] == lstbxCity.Items[i].Value)
                        {
                            lstbxCity.Items[i].Selected = true;
                        }
                    }
                }

                //string programe = ds.Tables[0].Rows[0]["Program Name"].ToString();

                ////
                //StringBuilder ProgramNames = new StringBuilder();
                //foreach (ListItem items in lstbxProgramName.Items)
                //{
                //    if (items.Selected == true)
                //    {
                //        //ProgramNames += items.Value + ',';
                //        ProgramNames.Append(items.Value);
                //        ProgramNames.Append(",");
                //    }
                //}

                //string programes = ds.Tables[0].Rows[0]["Program Name"].ToString();
                //objCommon.FillListBox(lstbxProgramName, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) ", "CONCAT(B.BRANCHNO,',',A.DEGREENO)", "CONCAT(LONGNAME,'- ',DEGREENAME) as PROGRAME", "A.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "(A.DEGREENO)");  //15-11-2022
                //string[] progs = programes.Split(',');
                //foreach (string s in progs)
                //{
                //    foreach (ListItem items in lstbxProgramName.Items)
                //    {
                //        if (items.Value == s)
                //        {
                //            items.Selected = true;
                //            break;
                //        }
                //    }
                //}
                //------------------START

                //for (int j = 0; j < progs.Length; j++)
                //{
                //    for (int i = 0; i < lstbxProgramName.Items.Count; i++)
                //    {

                //        //lstbxProgramName.Items.Add(new ListItem(progs[0], progs[1]));
                //        if (progs[j] == lstbxProgramName.Items[i].Value)
                //        {
                //            lstbxProgramName.Items[i].Selected = true;
                //        }
                //    }
                //}

                //  lstbxProgramName.Items.Add(programes);

                //------------------END
                //lstbxProgramName
                ////



                // string[] ptype = programe.Split(delimiterChars);
                // string[] progTypes = programe.Split(delimiter);

                //for (int j = 0; j < ptype.Length; j++)
                //{
                //    for (int i = 0; i < lstbxProgramName.Items.Count; i++)
                //    {
                //        if (ptype[j] == lstbxProgramName.Items[i].Value)
                //        {
                //            lstbxProgramName.Items[i].Selected = true;
                //        }
                //    }
                //}



                //   string sem = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                //  string[] stype = sem.Split(delimiterChars);
                //   string[] semTypes = sem.Split(delimiter);

                //for (int j = 0; j < stype.Length; j++)
                //{
                //    for (int i = 0; i < lstbxSemester.Items.Count; i++)
                //    {
                //        if (stype[j] == lstbxSemester.Items[i].Value)
                //        {
                //            lstbxSemester.Items[i].Selected = true;
                //        }
                //    }
                //}

                string chk = ds.Tables[0].Rows[0]["ANYWHEREINSRILANKA"].ToString();
                if (chk == "1")
                {
                    chkAnywhere.Checked = true;
                    //lstbxCity.Enabled = false;
                }
                else if (chk == "0")
                {
                    chkAnywhere.Checked = false;
                }

                txtVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
                //hdnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWFROM"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWTO"].ToString()).ToString("MMM dd, yyyy");

                txtSchFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWFROM"].ToString()).ToString("yyyy-MM-dd");
                txtSchToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWTO"].ToString()).ToString("yyyy-MM-dd");

                //hdnDate.Value = ds.Tables[0].Rows[0]["INTERVIEWFROM"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWFROM"].ToString()).ToString("dd MMM, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWTO"].ToString()).ToString("dd MMM, yyyy") : Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(DateTime.Now).ToString("dd MMM, yyyy");

                //added by AMIT PANDEY
                txtSSC.Text = ds.Tables[0].Rows[0]["SSCPER"].ToString();
                txtHSC.Text = ds.Tables[0].Rows[0]["HSCPER"].ToString();
                txtDiploma.Text = ds.Tables[0].Rows[0]["DIPLOMAPER"].ToString();
                txtUG.Text = ds.Tables[0].Rows[0]["UGPER"].ToString();
                txtPG.Text = ds.Tables[0].Rows[0]["PGPER"].ToString();

                templateEditor.Text = ds.Tables[0].Rows[0]["JOBDISCRIPTION"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                txtMinAmount.Text = ds.Tables[0].Rows[0]["MINAMOUNT"].ToString();
                txtMaxAmount.Text = ds.Tables[0].Rows[0]["MAXAMOUNT"].ToString();
                ddlInterval.SelectedValue = ds.Tables[0].Rows[0]["INTERVAL"].ToString();
                txtAddDetails.Text = ds.Tables[0].Rows[0]["ADDDETAILS"].ToString();
                ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["CUR_NO"].ToString();
                // ddlRound1.SelectedValue =
                string rounds = ds.Tables[0].Rows[0]["SELECTNO"].ToString();
                // string r1 = rounds.Split(new[] { ',' }, 2)[0];


                //string a[]=rounds.Split(",");
                //string[] subs = rounds.Split(',');
                //int a = Convert.ToInt32(subs[0]);
                //int b = Convert.ToInt32(subs[1]);
                //int c = Convert.ToInt32(subs[2]);
                //int d = Convert.ToInt32(subs[3]);

                // ddlRound1.SelectedValue = a.ToString();
                // objCommon.FillDropDownList(ddlRound2, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "", "SELECTNAME");            
                // ddlRound2.SelectedValue = b.ToString();
                // pnlr2.Visible = true;
                // objCommon.FillDropDownList(ddlRound3, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "", "SELECTNAME");
                // ddlRound3.SelectedValue = c.ToString();
                // pnlr3.Visible = true;
                // objCommon.FillDropDownList(ddlRound4, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "", "SELECTNAME");
                // ddlRound4.SelectedValue = d.ToString();
                // pnlr4.Visible = true;
                txtDescription.Text = ds.Tables[0].Rows[0]["ROUNDS_DISCRIPTION"].ToString();
                txtEligibility.Text = ds.Tables[0].Rows[0]["CRITERIA"].ToString();
                txtLastDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LASTDATE"].ToString()).ToString("yyyy-MM-dd");//yyyy-MM-dd
                //  ddlStudyLevel.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["StudyLevel"].ToString());
                //   lstbxProgramName.SelectedValue = ds.Tables[0].Rows[0]["Program Name"].ToString();


                lvannouncefor.Visible = true;
                lvannouncefor.DataSource = ds.Tables[2];
                Session["announcement"] = ds.Tables[2];
                lvannouncefor.DataBind();

                lvRoundDetail.DataSource = ds.Tables[1];
                lvRoundDetail.DataBind();
                ViewState["Round"] = ds.Tables[1];

                FileUploadCompany.Enabled = true;
                //string fileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    lblfileName.Visible = true;
                //    lblfileName.Text = fileName;
                //}

                //ScriptManager.RegisterClientScriptBlock(upnlMain, upnlMain.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                //ScriptManager.RegisterClientScriptBlock(upnlMain, upnlMain.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);

                //btnr2.Visible = true;
                // btnr1.Visible = false;
                //  btnr1.Visible = true;
                //btnr3.Visible = true;
                // btnr4.Visible = true;

            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkAnywhere_CheckedChanged(object sender, EventArgs e)
    {
        lstbxCity.Enabled = false;
    }
    private DataTable CreateTable_AnnounceFor()
    {
        DataTable dta = new DataTable();
        dta.Columns.Add(new DataColumn("SCHEDULENO", typeof(int)));
        dta.Columns.Add(new DataColumn("Faculty Name", typeof(string)));
        dta.Columns.Add(new DataColumn("Faculty", typeof(string)));
        dta.Columns.Add(new DataColumn("Study Level", typeof(string)));
        dta.Columns.Add(new DataColumn("Program Name", typeof(string)));
        dta.Columns.Add(new DataColumn("SEMESTERNO", typeof(string)));
        dta.Columns.Add(new DataColumn("BRANCH", typeof(string)));
        dta.Columns.Add(new DataColumn("DEGREE", typeof(string)));

        dta.Columns.Add(new DataColumn("ProgramFullName", typeof(string)));
        dta.Columns.Add(new DataColumn("Semester", typeof(string)));
        return dta;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            FileUploadCompany.Enabled = true;
            if (Session["announcement"] != null && ((DataTable)Session["announcement"]) != null)
            {


                string programname = "";
                foreach (ListViewDataItem dataitem in lvannouncefor.Items)
                {
                    Label Label2 = dataitem.FindControl("lblfName") as Label;
                    Label Label3 = dataitem.FindControl("lblslevel") as Label;
                    Label Label4 = dataitem.FindControl("lblpname") as Label;
                    Label Label5 = dataitem.FindControl("lblsno") as Label;


                    string s1 = Label2.Text;
                    string s2 = Label3.Text;
                    string s3 = Label4.Text;
                    string s4 = Label5.Text;

                    string program1 = "";
                    string Branchno1 = "";
                    string Degreeno1 = "";

                    foreach (ListItem items in lstbxProgramName.Items)
                    {
                        if (items.Selected == true)
                        {
                            //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            program1 += items.Value + ',';
                            programname += items.Text + ',';
                            if (program1 != string.Empty)
                            {
                                string[] subs = items.Value.Split(',');
                                ViewState["Branchno"] = Convert.ToInt32(subs[0]);
                                ViewState["Degreeno"] = Convert.ToInt32(subs[1]);

                                Branchno1 += ViewState["Branchno"].ToString() + ',';
                                Degreeno1 += ViewState["Degreeno"].ToString() + ',';
                            }
                        }
                    }
                    program1 = program1.Remove(program1.Length - 1);
                    programname = programname.Remove(programname.Length - 1);

                    if (s1 == ddlFaculty.SelectedItem.Text.Trim() && s2 == ddlStudyLevel.SelectedItem.Text.Trim() && s3 == program1) // && s4 == lstbxSemester.SelectedValue
                    {

                        objCommon.DisplayMessage(this.Page, "This Announce already Exist!", this.Page);  // shaikh juned 17-10-2023
                        return;
                    }
                }

                //if (this.lvannouncefor.Items.Count > 0)
                //{
                //    objCommon.DisplayMessage(this.Page, "Please Remove Already Added Job Announcement.And Add New !", this.Page);
                //    return;
                //}

                DataTable dt = (DataTable)Session["announcement"];



                DataRow dr = dt.NewRow();
                dr["SCHEDULENO"] = Convert.ToInt32(ViewState["TSno"]) + 1;
                dr["Faculty Name"] = ddlFaculty.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddlFaculty.SelectedItem.Text.Trim());
                dr["Faculty"] = ddlFaculty.Text.Trim() == null ? string.Empty : Convert.ToString(ddlFaculty.Text.Trim());
                dr["Study Level"] = ddlStudyLevel.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddlStudyLevel.SelectedItem.Text.Trim());
                dr["Program Name"] = lstbxProgramName.SelectedItem == null ? string.Empty : Convert.ToString(lstbxProgramName.SelectedItem.Value);

                string program = "";
                string Branchno = "";
                string Degreeno = "";
                string semester = "";
                foreach (ListItem items in lstbxProgramName.Items)
                {
                    if (items.Selected == true)
                    {
                        //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        program += items.Value + ',';
                        programname += items.Text + ',';
                        if (program != string.Empty)
                        {
                            string[] subs = items.Value.Split(',');
                            ViewState["Branchno"] = Convert.ToInt32(subs[0]);
                            ViewState["Degreeno"] = Convert.ToInt32(subs[1]);

                            Branchno += ViewState["Branchno"].ToString() + ',';
                            Degreeno += ViewState["Degreeno"].ToString() + ',';
                        }
                    }
                }
                program = program.Remove(program.Length - 1);
                Branchno = Branchno.Remove(Branchno.Length - 1);
                Degreeno = Degreeno.Remove(Degreeno.Length - 1);
                programname = programname.Remove(programname.Length - 1);

                dr["Program Name"] = program;
                dr["BRANCH"] = Branchno;
                dr["DEGREE"] = Degreeno;
                dr["ProgramFullName"] = programname;
                // dr["Program Name"] = lstbxProgramName.SelectedItem == null ? string.Empty : Convert.ToString(lstbxProgramName.SelectedItem.Value);
                string sem = "";

                foreach (ListItem items in lstbxSemester.Items)
                {
                    if (items.Selected == true)
                    {
                        //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        sem += items.Value + ',';
                        semester += items.Text + ',';
                    }
                }
                sem = sem.Remove(sem.Length - 1);
                semester = semester.Remove(semester.Length - 1);

                dr["SEMESTERNO"] = sem;
                dr["Semester"] = semester;
                //dr["SEMESTERNO"] = lstbxSemester.SelectedItem.Text.Trim() == null ? string.Empty : sem;

                dt.Rows.Add(dr);
                Session["announcement"] = dt;
                lvannouncefor.DataSource = dt;
                lvannouncefor.DataBind();
                // clear_Project();
                lvannouncefor.Visible = true;
                ViewState["PSno"] = Convert.ToInt32(ViewState["PSno"]) + 1;
                ViewState["ProgramName"] = lstbxProgramName.SelectedValue;
                ddlFaculty.SelectedValue = "0";
                ddlStudyLevel.SelectedValue = "0";
                lstbxProgramName.Items.Clear();
                lstbxSemester.Items.Clear();

                lstbxProgramName.SelectedValue = "0";
                lstbxSemester.SelectedValue = "0";
            }
            else
            {
                DataTable dt = this.CreateTable_AnnounceFor();
                DataRow dr = dt.NewRow();


                dr["SCHEDULENO"] = Convert.ToInt32(ViewState["TSno"]) + 1;
                dr["Faculty Name"] = ddlFaculty.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddlFaculty.SelectedItem.Text.Trim());
                dr["Faculty"] = ddlFaculty.Text.Trim() == null ? string.Empty : Convert.ToString(ddlFaculty.Text.Trim());
                dr["Study Level"] = ddlStudyLevel.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddlStudyLevel.SelectedItem.Text.Trim());
                dr["Program Name"] = lstbxProgramName.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(lstbxProgramName.SelectedItem.Text.Trim());
                // dr["SEMESTERNO"] = lstbxSemester.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(lstbxSemester.SelectedItem.Text.Trim());


                string program = "";
                string Branchno = "";
                string Degreeno = "";
                string programname = "";
                string semester = "";
                foreach (ListItem items in lstbxProgramName.Items)
                {
                    if (items.Selected == true)
                    {
                        //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        program += items.Value + ',';
                        programname += items.Text + ',';
                        if (program != string.Empty)
                        {
                            string[] subs = items.Value.Split(',');
                            ViewState["Branchno"] = Convert.ToInt32(subs[0]);
                            ViewState["Degreeno"] = Convert.ToInt32(subs[1]);
                            Branchno += ViewState["Branchno"].ToString() + ',';
                            Degreeno += ViewState["Degreeno"].ToString() + ',';
                        }
                    }
                }
                if (program.Length > 0)
                {
                    program = program.Remove(program.Length - 1);
                    programname = programname.Remove(programname.Length - 1);
                }
                Branchno = Branchno.Remove(Branchno.Length - 1);
                Degreeno = Degreeno.Remove(Degreeno.Length - 1);




                dr["Program Name"] = program;
                dr["BRANCH"] = Branchno;
                dr["DEGREE"] = Degreeno;
                dr["ProgramFullName"] = programname;

                //dr["Program Name"] = lstbxProgramName.SelectedItem == null ? string.Empty : Convert.ToString(lstbxProgramName.SelectedItem.Value);

                string sem = "";

                foreach (ListItem items in lstbxSemester.Items)
                {
                    if (items.Selected == true)
                    {
                        //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        sem += items.Value + ',';
                        semester += items.Text + ',';
                    }
                }
                if (sem.Length > 0)
                {
                    sem = sem.Remove(sem.Length - 1);
                    semester = semester.Remove(semester.Length - 1);
                }

                dr["SEMESTERNO"] = sem;
                dr["Semester"] = semester;

                //dr["SEMESTERNO"] = lstbxSemester.SelectedItem.Text.Trim() == null ? string.Empty : sem;


                ViewState["PSno"] = Convert.ToInt32(ViewState["PSno"]) + 1;

                dt.Rows.Add(dr);
                Session["announcement"] = dt;
                lvannouncefor.DataSource = dt;
                lvannouncefor.DataBind();
                lvannouncefor.Visible = true;
                ViewState["ProgramName"] = lstbxProgramName.SelectedValue;
                ddlFaculty.SelectedValue = "0";
                ddlStudyLevel.SelectedValue = "0";
                lstbxProgramName.Items.Clear();
                lstbxSemester.Items.Clear();

                lstbxProgramName.SelectedValue = "0";
                lstbxSemester.SelectedValue = "0";



            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "TP_Reg_form.btnAddJLoc_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void announcefor()
    //{
    //    DataSet dsProject = objCommon.FillDropDown("ACD_TP_STU_PROJ_DETAIL", "TITLE,DURATION,PROJ_DETAIL", "SEQNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]), "");
    //    if (Convert.ToInt32(dsProject.Tables[0].Rows.Count) > 0)
    //    {
    //        lvProjDetail.DataSource = dsProject.Tables[0];
    //        lvProjDetail.DataBind();
    //        Session["ProjTbl"] = dsProject.Tables[0];
    //        ViewState["PSno"] = Convert.ToInt32(dsProject.Tables[0].Rows.Count);
    //    }    
    //}
    protected void btnDelPDetail_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelPDetail = sender as ImageButton;
            if (Session["announcement"] != null && ((DataTable)Session["announcement"]) != null)
            {
                DataTable dt = (DataTable)Session["announcement"];
                dt.Rows.Remove(GetEditableDataRow_ProjDet(dt, btnDelPDetail.CommandArgument));
                Session["announcement"] = dt;
                lvannouncefor.DataSource = dt;
                lvannouncefor.DataBind();
                objCommon.DisplayMessage(upnlAnnouncedFor, "Announcement Detete Successfully!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stud_Search_tp.btnDelPDetail_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDataRow_ProjDet(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SCHEDULENO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stud_Search_tp.GetEditableDataRow_ProjDet --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }





    protected void btnr1_Click(object sender, EventArgs e)
    {
        //if (ddlRound1.SelectedIndex == 0)
        //{
        //    objCommon.DisplayMessage(Page, "Please Select Round.", Page);
        //}
        //else
        //{
        //    // int index = pnlround1.Controls.OfType<DropDownList>().ToList().Count + 1;
        //    // this.Createddl2("ddldyn" + index);
        //    pnlr2.Visible = true;
        //    objCommon.FillDropDownList(ddlRound2, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", " SELECTNO NOT IN (" + Convert.ToInt32(ddlRound1.SelectedValue) + ")", "SELECTNAME");
        //    btnr1.Visible = false;
        //}
        //objCommon.FillDropDownList(ddlRound2, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", " ", "SELECTNAME");


        //string Count = objCommon.LookUp("ACD_TP_SELECTIONTYPE", "Count(SELECTNO)", "");
        //for (int i = 0; i < Convert.ToInt32(Count); i++)      //specifying the condition in for loop for creating no. of textboxes
        //{

        //    // TextBox tb = new TextBox();   //Create the object of TexBox Class
        //    DropDownList ddl = new DropDownList();
        //    ddl.ID = i.ToString();                // assign the loop value to textbox object for dynamically adding Textbox ID
        //    createDynamicDropDownList(ddl.ID);      // adding the controls

        //}




    }
    protected void btnr2_Click(object sender, EventArgs e)
    {
        //if (ddlRound2.SelectedIndex == 0)
        //{
        //    objCommon.DisplayMessage(Page, "Please Select Round.", Page);
        //}
        //else
        //{
        //    pnlr3.Visible = true;
        //    int index = pnlRound2.Controls.OfType<DropDownList>().ToList().Count + 1;
        //    //this.Createddl3("ddldyn" + index);
        //    objCommon.FillDropDownList(ddlRound3, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNO NOT IN (" + Convert.ToInt32(ddlRound1.SelectedValue) + "," + Convert.ToInt32(ddlRound2.SelectedValue) + ")", "SELECTNAME");
        //    btnr2.Visible = false;
        //}
    }
    protected void btnr3_Click(object sender, EventArgs e)
    {
        //if (ddlRound3.SelectedIndex == 0)
        //{
        //    objCommon.DisplayMessage(Page, "Please Select Round.", Page);
        //}
        //else
        //{
        //    pnlr4.Visible = true;
        //    //  int index = pnlSort.Controls.OfType<DropDownList>().ToList().Count + 1;
        //    // this.Createddl("ddldyn" + index);
        //    objCommon.FillDropDownList(ddlRound4, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNO NOT IN (" + Convert.ToInt32(ddlRound1.SelectedValue) + "," + Convert.ToInt32(ddlRound2.SelectedValue) + "," + Convert.ToInt32(ddlRound3.SelectedValue) + ")", "SELECTNAME");
        //    btnr3.Visible = false;
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ddlCompanyName.Enabled = true;
    }
    private void Clear()
    {
        ddlCompanyName.SelectedIndex = 0;
        ddlJobType.SelectedIndex = 0;
        ddlJobRole.SelectedIndex = 0;
        ddlCurrency.SelectedIndex = 0;
        ddlInterval.SelectedIndex = 0;
        ddlPlacement.SelectedIndex = 0;
        //  ddlRound1.SelectedIndex = 0;
        //  ddlRound2.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        txtAddDetails.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtEligibility.Text = string.Empty;
        txtLastDate.Text = string.Empty;
        txtMaxAmount.Text = string.Empty;
        txtMinAmount.Text = string.Empty;
        txtVenue.Text = string.Empty;
        txtEligibility.Text = string.Empty;
        txtSSC.Text = "0.00";
        txtHSC.Text = "0.00";
        txtDiploma.Text = "0.00";
        txtUG.Text = "0.00";
        txtPG.Text = "0.00";
        lstbxCity.ClearSelection();
        lstbxProgramName.ClearSelection();
        lstbxSemester.ClearSelection();
        chkAnywhere.Checked = false;
        lvannouncefor.DataSource = null;
        lvannouncefor.DataBind();
        lvannouncefor.Visible = false;
        //lvannouncefor.Items.Clear();
        // lvannouncefor.Visible = false;
        // pnlr2.Visible = false;
        // pnlr3.Visible = false;
        // pnlr4.Visible = false;
        // pnlr5.Visible = false;
        // pnlr6.Visible = false;
        templateEditor.Text = string.Empty;
        ddlRound.SelectedValue = "0";
        txtRoundSrNo.Text = string.Empty;
        lvRoundDetail.DataSource = null;
        lvRoundDetail.DataBind();
        txtSchFromDate.Text = string.Empty;
        txtSchToDate.Text = string.Empty;
        ViewState["Round"] = null;
        ViewState["action"] = "add";
        Session["announcement"] = null;
        ViewState["ACOMSCHNO"] = null;
        ddlCompanyName.Enabled = true;
    }
    //public void Createddl(string id)
    //{
    //    DropDownList ddl = new DropDownList();
    //    ddl.ID = id;

    //    pnlSort.Controls.Add(ddl);

    //    Literal lt = new Literal();
    //    lt.Text = "<br />";
    //    pnlSort.Controls.Add(lt);
    //    pnlSort.Attributes.Add("style", "display:block");
    //}
    //public void Createddl2(string id)
    //{
    //    DropDownList ddl = new DropDownList();
    //    ddl.ID = id;

    //    pnlround1.Controls.Add(ddl);

    //    Literal lt = new Literal();
    //    lt.Text = "<br />";
    //    pnlround1.Controls.Add(lt);
    //    pnlround1.Attributes.Add("style", "display:block");
    //}
    //public void Createddl3(string id)
    //{
    //    DropDownList ddl = new DropDownList();
    //    ddl.ID = id;

    //    pnlRound2.Controls.Add(ddl);

    //    Literal lt = new Literal();
    //    lt.Text = "<br />";
    //    pnlRound2.Controls.Add(lt);
    //    pnlRound2.Attributes.Add("style", "display:block");
    //}


    //protected void btnaadd_Click(object sender, EventArgs e)
    //{
    //    int index = pnlSort.Controls.OfType<DropDownList>().ToList().Count + 1;
    //    this.Createddl("ddldyn" + index);
    //}
    protected void chksalary_CheckedChanged(object sender, EventArgs e)
    {
        txtAmount.Enabled = true;
        btnSpecifyRange.Enabled = true;
        txtMinAmount.Enabled = true;
        txtMaxAmount.Enabled = true;
        txtAddDetails.Enabled = true;
        ddlCurrency.Enabled = true;
        ddlInterval.Enabled = true;
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B on (A.UA_SECTION=B.UGPGOT)", "DISTINCT (UA_SECTION)", "A.UA_SECTIONNAME", "B.COLLEGE_ID=" + ddlFaculty.SelectedValue, "(UA_SECTION)");
            FileUploadCompany.Enabled = false;
        }
    }

    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStudyLevel.SelectedIndex > 0)
        {

            //objCommon.FillListBox(lstbxProgramName, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) ", "DISTINCT (A.DEGREENO)", "CONCAT(LONGNAME,'-',DEGREENAME) as PROGRAME", "A.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "(A.DEGREENO)");
            // objCommon.FillListBox(lstbxProgramName, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B on (A.DEGREENO=B.DEGREENO)", "DISTINCT (A.DEGREENO)", "A.DEGREENAME", "B.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + " AND B.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "(A.DEGREENO)");
            objCommon.FillListBox(lstbxProgramName, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) ", "CONCAT(B.BRANCHNO,',',A.DEGREENO)", "CONCAT(LONGNAME,'- ',DEGREENAME) as PROGRAME", "A.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "(A.DEGREENO)");  //15-11-2022
            //   objCommon.FillListBox(lstbxProgramName, "ACD_AFFILIATED_UNIVERSITY", "AFFILIATED_NO", "AFFILIATED_LONGNAME", "", "AFFILIATED_NO");  //15-11-2022
            objCommon.FillListBox(lstbxSemester, "ACD_SEMESTER ", "SEMESTERNO", "SEMESTERNAME ", "SEMESTERNO>0", "SEMESTERNO ");
            FileUploadCompany.Enabled = false;
        }
    }
    protected void btnr4_Click(object sender, EventArgs e)
    {
        //if (ddlRound4.SelectedIndex == 0)
        //{
        //    objCommon.DisplayMessage(Page, "Please Select Round.", Page);
        //}
        //else
        //{
        //    pnlr5.Visible = true;
        //    objCommon.FillDropDownList(ddlr5, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNO NOT IN (" + Convert.ToInt32(ddlRound1.SelectedValue) + "," + Convert.ToInt32(ddlRound2.SelectedValue) + "," + Convert.ToInt32(ddlRound3.SelectedValue) + "," + Convert.ToInt32(ddlRound4.SelectedValue) + ")", "SELECTNAME");
        //    btnr4.Visible = false;
        //}
    }
    protected void btnr5_Click(object sender, EventArgs e)
    {

        //if (ddlr5.SelectedIndex == 0)
        //{
        //    objCommon.DisplayMessage(Page, "Please Select Round.", Page);
        //}
        //else
        //{

        //    pnlr6.Visible = true;
        //    objCommon.FillDropDownList(ddlr6, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNO NOT IN (" + Convert.ToInt32(ddlRound1.SelectedValue) + "," + Convert.ToInt32(ddlRound2.SelectedValue) + "," + Convert.ToInt32(ddlRound3.SelectedValue) + "," + Convert.ToInt32(ddlRound4.SelectedValue) + "," + Convert.ToInt32(ddlr5.SelectedValue) + ")", "SELECTNAME");
        //    btnr5.Visible = false;
        //}
    }


    private void createDynamicDropDownList(String _ddlId)
    {
        HtmlGenericControl tr = new HtmlGenericControl("tr");
        HtmlGenericControl td1 = new HtmlGenericControl("td");
        Label lbl = new Label();
        lbl.ID = "ddl" + _ddlId.Replace(" ", "").ToLower();
        td1.Controls.Add(lbl);
        tr.Controls.Add(td1);
        //placeH.Controls.Add(tr);
        lbl.Text = _ddlId;
        HtmlGenericControl td2 = new HtmlGenericControl("td");
        DropDownList ddl = new DropDownList();
        ddl.ID = _ddlId.Replace(" ", "").ToLower();
        // ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        // ddl.AutoPostBack = true;

        td2.Controls.Add(ddl);
        tr.Controls.Add(td2);
        // placeH.Controls.Add(tr);

        // DropDownList ddl1 = (DropDownList)placeH.FindControl("DropDownList");
        // lblValue.Text = ddl1.SelectedValue;


        /// ddl.ID = _ddlId.Replace(" ", "").ToLower();
        ddl.Items.Insert(0, "Please Select");
        objCommon.FillDropDownList(ddl, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNO NOT IN (0)", "SELECTNAME");

        //  ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        //ddl.AutoPostBack = true;
    }


    //public void ddl_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var ddl = (DropDownList)sender;
    //    if (ddl.SelectedIndex > 0)
    //    {
    //        lblValue.Text = ddl.SelectedValue;
    //    }
    //}


    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < 4; i++)      //specifying the condition in for loop for creating no. of textboxes
    //    {

    //       // TextBox tb = new TextBox();   //Create the object of TexBox Class
    //        DropDownList ddl = new DropDownList();
    //        ddl.ID = i.ToString();                // assign the loop value to textbox object for dynamically adding Textbox ID
    //        Form.Controls.Add(tb);        // adding the controls

    //    }
    //}

    //---------

    protected void btnRoundAdd_Click(object sender, EventArgs e)
    {

        //DataTable AccessionRegisterTbl = new DataTable("AccessionRegisterTbl");

        //AccessionRegisterTbl.Columns.Add("AccessionRegisterId", typeof(int));

        //DataRow dr = null;

        ////For Accession Register Update

        //foreach (RepeaterItem i in RepVERIBOOK.Items)
        //{
        //    Label lblAccRegId;
        //    CheckBox chkselect = (CheckBox)i.FindControl("chkSelect");
        //    if (chkselect.Checked == true)
        //    {
        //        lblAccRegId = (Label)i.FindControl("lblAccRegId");

        //        dr = AccessionRegisterTbl.NewRow();

        //        dr["AccessionRegisterId"] = lblAccRegId.Text;

        //        AccessionRegisterTbl.Rows.Add(dr);
        //        Count = Count + 1;
        //    }


        //}

        //ObjEL.AccessNoTbl = AccessionRegisterTbl;

        DataTable dt = new DataTable();
        lvRoundDetail.Visible = true;
        if (ViewState["Round"] != null)
        {
            dt = (DataTable)ViewState["Round"];
        }
        else
        {
            Createtable_RoundDet();
            dt = (DataTable)ViewState["Round"];
        }

        //int count = (from row in dt.AsEnumerable()
        //             where row.Field<string>("Title").Equals(Convert.ToString(lblTitle.Text)) 
        //             select row).Count();

        //if (count <= 0)
        //{
        foreach (ListViewDataItem dataitem in lvRoundDetail.Items)
        {
            Label Label1 = dataitem.FindControl("Label1") as Label;
            string SrNo = Label1.Text;
            if (SrNo == txtRoundSrNo.Text)
            {
                objCommon.DisplayMessage("Should Not Enter Same Round Serial Number !", this.Page);
                return;
            }
        }


        int maxVals = 0;
        DataRow dr = dt.NewRow();
        dr["SEQNO"] = maxVals + 1;
        dr["ROUNDS"] = ddlRound.SelectedItem.Text == null ? string.Empty : Convert.ToString(ddlRound.SelectedItem.Text);
        dr["ROUNDSID"] = Convert.ToInt32(ddlRound.SelectedValue) == null ? 0 : Convert.ToInt32(ddlRound.SelectedValue);
        dr["ROUNDS_SRNO"] = Convert.ToInt32(txtRoundSrNo.Text) == null ? 0 : Convert.ToInt32(txtRoundSrNo.Text);


        dt.Rows.Add(dr);


        lvRoundDetail.DataSource = dt;
        lvRoundDetail.DataBind();
        objCommon.DisplayMessage("Round Is Added !", this.Page);


        //}
        //else
        //{
        //    Showmessage("Selected entry already in the list.");
        //    txtAccNo.Focus();
        //    return;
        //}


        upnlAnnouncedFor.Visible = true;
        divbtn.Visible = true;
        lvJobAnnouncement.Visible = true;
        ddlRound.SelectedValue = "0";
        txtRoundSrNo.Text = string.Empty;


        //if (Session["TblRound"] != null && ((DataTable)Session["TblRound"]) != null)
        //{


        //    DataTable dt = (DataTable)Session["TblRound"];
        //    DataRow dr = dt.NewRow();
        //    dr["SEQNO"] = Convert.ToInt32(Session["SRNO"]) + 1;
        //    dr["ROUNDS"] = ddlRound.SelectedItem.Text == null ? string.Empty : Convert.ToString(ddlRound.SelectedItem.Text);
        //    //  dr["ROUNDS_SRNO"] = Convert.ToInt32(ddlRound.SelectedValue) == null ? 0  : Convert.ToInt32(ddlRound.SelectedValue);
        //    dr["ROUNDS_SRNO"] = Convert.ToInt32(txtRoundSrNo.Text) == null ? 0 : Convert.ToInt32(txtRoundSrNo.Text);

        //    dt.Rows.Add(dr);
        //    Session["TblRound"]=dt; 
        //    lvRoundDetail.DataSource = dt;
        //    lvRoundDetail.DataBind();


        //}
        //else
        //{
        //    int maxVals = 0;
        //    DataTable dt = this.Createtable_RoundDet();
        //    DataRow dr = dt.NewRow();

        //    dr["SEQNO"] = maxVals + 1; 
        //    dr["ROUNDS"] = ddlRound.SelectedItem.Text == null ? string.Empty : Convert.ToString(ddlRound.SelectedItem.Text);
        //    dr["ROUNDSID"] = Convert.ToInt32(ddlRound.SelectedValue) == null ? 0 : Convert.ToInt32(ddlRound.SelectedValue);
        //    dr["ROUNDS_SRNO"] = Convert.ToInt32(txtRoundSrNo.Text) == null ? 0 : Convert.ToInt32(txtRoundSrNo.Text);

        //    dt.Rows.Add(dr);
        //    //Session["RoundTbl"] = dt;
        //   // ViewState["RoundTbl"] = dt;
        //    Session["TblRound"]=dt;
        //    lvRoundDetail.DataSource = dt;
        //    lvRoundDetail.DataBind();

        //  //  ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
        //    //ViewState["SRNO"] = maxVals + 1;
        //    Session["SRNO"] = maxVals + 1;
        //}


    }
    protected void btnCancelRound_Click(object sender, EventArgs e)
    {

    }
    //protected void btnDelRoundDetail_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelRoundDetail = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["TblRound"] != null && ((DataTable)Session["TblRound"]) != null)
    //        {
    //             dt = (DataTable)Session["TblRound"];

    //                DataRow dr = this.GetEditableDataRow_RoundDet(dt, btnDelRoundDetail.CommandArgument);
    //             dt.Rows.Remove(dr);
    //             Session["TblRound"] = dt;
    //            lvRoundDetail.DataSource = dt;
    //            lvRoundDetail.DataBind();

    //        }
    //        upnlAnnouncedFor.Visible = true;
    //        divbtn.Visible = true;
    //        lvJobAnnouncement.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Stud_Search_tp.btnDelPDetail_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}




    protected void btnDelRoundDetail_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            ImageButton btnDelete = sender as ImageButton;
            int srno = Convert.ToInt32(btnDelete.CommandArgument);
            if (ViewState["Round"] != null)
            {
                dt = (DataTable)ViewState["Round"];
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (srno - 1 == i)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }

            }

            lvRoundDetail.DataSource = dt;
            lvRoundDetail.DataBind();
            objCommon.DisplayMessage(this.Page, "Round Delete Successfully  !", this.Page);
            ViewState["Round"] = dt;

            //  objCommon.DisplayMessage("Round Delete Successfully  !", this.Page);

        }
        catch (Exception ex)
        {


            return;
        }
    }


    private void Createtable_RoundDet()
    {
        DataTable dtround = new DataTable();
        dtround.Columns.Add(new DataColumn("SEQNO", typeof(int)));
        dtround.Columns.Add(new DataColumn("ROUNDS", typeof(string)));
        dtround.Columns.Add(new DataColumn("ROUNDSID", typeof(int)));
        dtround.Columns.Add(new DataColumn("ROUNDS_SRNO", typeof(int)));


        ViewState["Round"] = dtround;
    }

    private DataRow GetEditableDataRow_RoundDet(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SEQNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stud_Search_tp.GetEditableDataRow_ProjDet --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }


    //Added by Parag//29-02-2024

    protected void imgdownloadComDetails_Click(object sender, ImageClickEventArgs e)
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

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNametandpdoctest";
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

    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".pdf", ".PDF" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    //Added by Parag//29-02-2024

}