//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Applicant data verify
// CREATION DATE : 19-MAY-2014
// CREATED BY    : RENUKA ADULKAR
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient  ;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using ICSharpCode.SharpZipLib.Zip;
using DynamicAL_v2;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Academic_UserInfoVerify : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    DailyFeeCollectionController objdfcc = new DailyFeeCollectionController();
    OnlineAdmissionController objOAC = new OnlineAdmissionController();
    Student objstud = new Student();



    DynamicControllerAL AL = new DynamicControllerAL();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();


    string FileName = string.Empty;
    public string Docpath = string.Empty;
    string DirPath = string.Empty;
    public int count = 0;
    public string studid = string.Empty;
    // public string docname = string.Empty;
    public string enrollno = string.Empty;
    public string fname = string.Empty;
    public int i = 0;
    public string btndocname = string.Empty;

    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();


    public string docname = string.Empty;
    #region page

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
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                //  this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //fill sessions

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", string.Empty, "SESSIONNO DESC");
                //ddlSession.Items.RemoveAt(0);

                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMISSION_CONFIG AG INNER JOIN ACD_ADMBATCH AB ON AG.ADMBATCH=AB.BATCHNO", "DISTINCT ADMBATCH", "BATCHNAME", "ADMBATCH>0", "ADMBATCH DESC");
                ddlAdmBatch.SelectedIndex = 1;
                //ddlAdmBatch.Items.RemoveAt(0);

                //objCommon.FillDropDownList(ddlAdmcat, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS='E'", string.Empty);


                //string verifycount = objCommon.LookUp("ACD_USER_DETAILSMODIFY_LOG", "count(distinct userno)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                // lblVerifycount.ForeColor = System.Drawing.Color.Green;
                //lblVerifycount.Text = verifycount;

                //}


            }

        }
        divMsg.InnerHtml = string.Empty;


    }

    #endregion

    #region User Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            // if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) //== false)

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UserInfoVerify.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UserInfoVerify.aspx");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {

        btnSubmit_Click(sender, e);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;



            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_QUALIFYNO=" + ddlAdmcat.SelectedValue + ",@P_APPID=" + txtAppID.Text.Trim();


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updMarks, this.updMarks.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    #endregion

    #region button

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            DataSet dsfee = objdfcc.GetUserFeesPaidData(Convert.ToInt32(ddlAdmBatch.SelectedValue.ToString()), Convert.ToInt32(rdoStatus.SelectedValue), Convert.ToInt32(ddlProgramType.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            // ViewState["code"] = ddlProgramType.SelectedValue;  //COMMENT BY JAY    
            ViewState["programmeType"] = ddlProgramType.SelectedValue;
            if (dsfee.Tables[0] != null && dsfee.Tables[0].Rows.Count > 0)
            {
                btnVerify.Enabled = true;
                divDetails.Visible = true;
                lvApplicantdata.Visible = true;
                divApplicantDetail.Visible = false;
                lvApplicantdata.DataSource = dsfee.Tables[0];
                lvApplicantdata.DataBind();
                lvApplicantdata.Visible = true;
                foreach (ListViewDataItem lvItem in lvApplicantdata.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkVerify") as CheckBox;
                    // TextBox txtJEETotal = (TextBox)lvItem.FindControl("txtJEETotal");
                    HiddenField hdnUserno = lvItem.FindControl("hiduserno") as HiddenField;
                    ViewState["Userno"] = hdnUserno.Value;

                    if (chkBox.ToolTip == "1")
                    {
                        chkBox.Enabled = false;
                        chkBox.Checked = true;
                        //txtJEETotal.Enabled = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found.", this.Page);
                btnVerify.Enabled = false;
                divApplicantDetail.Visible = false;
                divDetails.Visible = false;
                lvApplicantdata.Visible = false;
                lvApplicantdata.DataSource = null;
                lvApplicantdata.DataBind();
                return;
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {
        //DataSet dsstatus = objCommon.FillDropDown("ACD_USER_COUNTERS  A INNER JOIN ACD_USER_REGISTRATION B ON(A.USERNO=B.USERNO)INNER JOIN ACD_IDTYPE C ON(C.IDTYPENO=B.ADMCAT)", "ROW_NUMBER() OVER(ORDER BY TOKENNO) AS SRNO, COUNTER1_DATE,TOKENNO,B.FIRSTNAME +' ' +B.LASTNAME NAME", "C.IDTYPEDESCRIPTION", "C1_STATUS=" + 1, "TOKENNO");
        //if (dsstatus.Tables[0] != null && dsstatus.Tables[0].Rows.Count > 0)
        //{
        //    btnexport.Visible = true;
        //    pnlstatus.Visible = true;
        //    lvC1Status.DataSource = dsstatus;
        //    lvC1Status.DataBind();
        //}
        //else
        //{
        //    objCommon.DisplayMessage("There is no user pass from counter1", this.Page);
        //}
    }

    protected void btnexport_Click(object sender, EventArgs e)
    {
        this.Export();
    }

    private void Export()
    {
        //string attachment = "attachment; filename=" + "Verification.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/" + "ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //int sessionNo = 0;
        //sessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        //DataSet dsfee = objdfcc.GetUserJEEVerifed(Convert.ToInt32(ddlSession.SelectedValue.ToString()), Convert.ToInt32(ddlAdmcat.SelectedValue.ToString()), txtAppID.Text.Trim());
        //DataGrid dg = new DataGrid();
        //if (dsfee.Tables.Count > 0)
        //{
        //    dg.DataSource = dsfee.Tables[0];
        //    dg.DataBind();
        //}
        //dg.HeaderStyle.Font.Bold = true;
        //dg.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {

            string studentIds = string.Empty;
            string jeetotal = string.Empty;
            int appid = 0;

            int countone = 0; int counttwo = 0;

            foreach (ListViewDataItem lvItem in lvApplicantdata.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkVerify") as CheckBox;
                HiddenField chkhdn = lvItem.FindControl("hiduserno") as HiddenField;
                //TextBox txttotal = lvItem.FindControl("txtJEETotal") as TextBox;

                if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    studentIds = chkhdn.Value;
                    //jeetotal = txttotal.Text;
                    appid = 1;
                }
                countone++;
                if (chkBox.Enabled == false && chkBox.Enabled == false)
                {
                    counttwo++;
                }
            }
            if (string.IsNullOrEmpty(studentIds) && (counttwo != countone))
            {
                objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
            //else if (counttwo == countone)
            //{
            //    if (txtAppID.Text == string.Empty)
            //        objCommon.DisplayMessage("Marks are Already Verified For Selected Admission Batch " + ddlSession.SelectedItem.Text + " of Admission Category " + ddlAdmcat.SelectedItem.Text + "!", this.Page);
            //    else
            //        objCommon.DisplayMessage("Marks are Already Verified For Selected Admission Batch " + ddlSession.SelectedItem.Text + " of Admission Category " + ddlAdmcat.SelectedItem.Text + " of Applicantion ID " + txtAppID.Text.Trim() + "!", this.Page);
            //}
            else
            {
                CustomStatus cs = new CustomStatus();

                foreach (ListViewDataItem lvItem in lvApplicantdata.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkVerify") as CheckBox;
                    HiddenField chkhdn = lvItem.FindControl("hiduserno") as HiddenField;
                    //TextBox txttotal = lvItem.FindControl("txtJEETotal") as TextBox;

                    if (chkBox.Checked == true && chkBox.Enabled == true)
                    {
                        studentIds = chkhdn.Value;
                        //jeetotal = txttotal.Text;
                        appid = 1;
                    }

                    //cs = (CustomStatus)objdfcc.GetUserjeeinsertData(Convert.ToInt32(studentIds), Convert.ToInt32(Session["userno"]), Convert.ToString(ViewState["ipAddress"]));
                    cs = (CustomStatus)objdfcc.InsertUserVerifyStatus(Convert.ToInt32(studentIds), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());

                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Documents Verified Successfully.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage("Error To Verified Marks.", this.Page);
                    return;
                }
                btnSubmit_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divDetails.Visible = false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divbtn.Visible = true;
            divRemark.Visible = true;
            btnsYes.Visible = true;
            btnsNo.Visible = true;
            string isUploaded = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int UserNo = int.Parse(btnEdit.CommandArgument);
            Session["studuserno"] = UserNo;
            //ListViewItem lvi = (ListViewItem)((DropDownList)sender).NamingContainer;
            //string ddl=lvi.FindControl("ddlCourse") as DropDownList;

            DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR LEFT JOIN ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "EMAILID", "FIRSTNAME,MOBILENO,DOB,USERNAME,N.APPLICATION_NO", " USERNO = " + UserNo, string.Empty);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                lblMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                lblApplicationId.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                lblInteratedApplicationId.Text = ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString();
            }


            DataSet dsDoc = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO", "DOCNAME,FILENAME,PATH,USERNO", " USERNO = " + UserNo, string.Empty);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsDoc.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    isUploaded += dr["DOCNO"].ToString() + ", ";
                }

                string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
                ViewState["UploadedString"] = realUploaded;

                lvApplicantDocDetails.DataSource = dsDoc;
                lvApplicantDocDetails.DataBind();
            }
            else
            {
                lvApplicantDocDetails.DataSource = null;
                lvApplicantDocDetails.DataBind();
            }
            if (ViewState["programmeType"].Equals("1"))
            {
                //COMMENT BY JAY    
                //if (ViewState["code"].ToString().Equals("U32"))
                //{
                //    DataSet dsExmU32 = objSC.GetUserExamninationDataForU32(UserNo, ViewState["code"].ToString());
                //    lvU32.DataSource = dsExmU32;
                //    lvU32.DataBind();
                //    pnlU32.Visible = true;
                //    lvU32.Visible = true;
                //    pnlMarks.Visible = false;
                //    pnlPG.Visible = false;
                //    foreach (ListViewDataItem lv in lvU32.Items)
                //    {
                //        TextBox lblPercentU32 = lv.FindControl("lblPercentU32") as TextBox;
                //        TextBox  txtObtMarksU32= lv.FindControl("txtObtMarksU32") as TextBox;
                //        TextBox txtMaxMarksU32 = lv.FindControl("txtMaxMarksU32") as TextBox;
                //        HiddenField hdnQualifyU32 = lv.FindControl("hdnQualifyU32") as HiddenField;
                //        if (hdnQualifyU32.Value.Equals("139"))
                //        {
                //            txtObtMarksU32.Enabled = false;
                //            txtMaxMarksU32.Enabled = false;
                //            lblPercentU32.Enabled = true;
                //        }
                //        else
                //        {
                //            txtObtMarksU32.Enabled = true;
                //            txtMaxMarksU32.Enabled = true;
                //            lblPercentU32.Enabled = false;
                //        }
                //    }
                //    //divApplicantDetail.Visible = true;
                //    //divDetails.Visible = false;
                //    //btnVerify.Enabled = false;
                //    //divRemark.Visible = true;
                //}
                //else
                //{
                DataSet dsExm = objSC.GetUserExamninationData(UserNo, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue)); //JAY 
                ViewState["dsExm"] = dsExm;
                if (dsExm.Tables[0].Rows.Count > 0)
                {
                    lvApplicantMarksDetail.DataSource = dsExm;
                    lvApplicantMarksDetail.DataBind();
                    pnlMarks.Visible = true;
                    lvApplicantMarksDetail.Visible = true;
                    pnlPG.Visible = false;
                    pnlU32.Visible = false;

                    foreach (ListViewDataItem lv in lvApplicantMarksDetail.Items)
                    {
                        DropDownList ddl = lv.FindControl("ddlCourse") as DropDownList;
                        objCommon.FillDropDownList(ddl, "ACD_COMPULSORY_COURSES", "COURSENO", "COURSENAME", "COURSENO > 0", "COURSENAME ASC");
                        HiddenField hdn = lv.FindControl("hdnCourse") as HiddenField;
                        HiddenField hdnQ = lv.FindControl("hdnQualify") as HiddenField;


                        // Label lblCourse = lv.FindControl("lblComplCourse") as Label;
                        TextBox txtOther = lv.FindControl("txtOther") as TextBox;
                        //HiddenField hdnOther = lv.FindControl("hdnOther") as HiddenField;
                        Label lblOther = lv.FindControl("lblOtherSub") as Label;
                        if (Convert.ToInt32(hdnQ.Value) == 101 || Convert.ToInt32(hdnQ.Value) == 102 || Convert.ToInt32(hdnQ.Value) == 103 || Convert.ToInt32(hdnQ.Value) == 104)
                        {
                            if ((int.Parse(hdn.Value)) == 48)
                            {
                                txtOther.Visible = true;
                                txtOther.Text = lblOther.Text.ToString();
                                lblOther.Visible = false;

                            }
                            else
                            {
                                txtOther.Visible = false;
                                lblOther.Visible = true;
                            }
                            ddl.Visible = true;
                            ddl.SelectedValue = hdn.Value;
                            //  lblCourse.Visible = false;
                        }
                        else
                        {
                            ddl.Visible = false;
                            // lblCourse.Visible = true;
                            txtOther.Visible = false;
                        }
                    }
                }
                else
                {
                    lvApplicantMarksDetail.DataSource = null;
                    lvApplicantMarksDetail.DataBind();
                }
            }
            //}
            else
            {
                DataSet dsExmPG = objSC.GetUserExamninationDataForPG(UserNo, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
                if (dsExmPG.Tables[0].Rows.Count > 0)
                {
                    lvPG.DataSource = dsExmPG;
                    lvPG.DataBind();
                    pnlPG.Visible = true;
                    lvPG.Visible = true;
                    pnlMarks.Visible = false;
                    pnlU32.Visible = false;
                }
                else
                {
                    lvPG.DataSource = null;
                    lvPG.DataBind();
                    pnlPG.Visible = false;
                    lvPG.Visible = false;
                    pnlMarks.Visible = false;
                    pnlU32.Visible = false;
                }
            }
            DataSet dsRemark = objCommon.FillDropDown("ACD_USER_VERIFY", "*", "", "USERNO=" + UserNo + " AND DEGREENO =" + ddlBranch.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + "", "");
            if (dsRemark.Tables[0].Rows.Count > 0)
            {
                if (!dsRemark.Tables[0].Rows[0]["REMARK"].ToString().Equals(string.Empty))
                {
                    txtRemarkSingle.Text = dsRemark.Tables[0].Rows[0]["REMARK"].ToString();
                }
                else
                {
                    txtRemarkSingle.Text = string.Empty;
                }
                if (dsRemark.Tables[0].Rows[0]["IS_CANCEL"].ToString().Equals("1"))
                {
                    chkCancel.Checked = true;
                    divCancelRemark.Visible = true;
                    txtCancelRemark.Text = dsRemark.Tables[0].Rows[0]["CANCEL_REMARK"].ToString();
                }
                else
                {
                    chkCancel.Checked = false;
                    divCancelRemark.Visible = false;
                }
            }
            else
            {
                txtRemarkSingle.Text = string.Empty;
            }
            foreach (ListViewItem Item in lvApplicantDocDetails.Items)
            {

                ImageButton imgbtnPrevDoc = Item.FindControl("imgbtnPrevDoc") as ImageButton;
            }
            divApplicantDetail.Visible = true;
            divDetails.Visible = false;
            btnVerify.Enabled = false;
            divRemark.Visible = true;
            divbtn.Visible = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnsYes_Click(object sender, EventArgs e)
    {
        try
        {

            CustomStatus cs = new CustomStatus();
            int cancelcount = 0;
            int records = 0;
            int updated = 0;
            int userno = 0;
            decimal oldObtMarks = 0;
            decimal oldMaxMarks = 0;
            decimal oldPercent = 0;
            string remarkSingle = string.Empty;
            string remarkCancel = string.Empty;
            int compCourse = 0;
            string otherSub = string.Empty;
            int cancel = 0;
            if (chkCancel.Checked)
            {
                cancelcount++;
                if (txtCancelRemark.Text.Equals(string.Empty))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Cancel Remark.", this.Page);
                    txtCancelRemark.Focus();
                    return;
                }
                cancel = 1;
                remarkCancel = txtCancelRemark.Text.TrimEnd();
            }
            if (!txtRemarkSingle.Text.Equals(string.Empty))
            {
                remarkSingle = txtRemarkSingle.Text.TrimEnd();
            }


            if (cancelcount == 0)
            {
                cs = (CustomStatus)objSC.Update_Document_Verification_Status(Convert.ToInt32(Session["studuserno"]), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue),
                 Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), remarkSingle, cancel, remarkCancel);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this.Page, "Documents Verified Successfully.", this.Page);
                    clear();

                }
            }
            else if (cancelcount == 1)
            {
                objCommon.DisplayMessage(this.Page, "Verification Cancel Successfully.", this.Page);
                // Response.Redirect(Request.Url.ToString());
                clear();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Applicant Information not found!", this.Page);
            }

        }
        // Response.Redirect(Request.Url.ToString());
        //}
        //}
        catch (Exception ex)
        {
            throw;
        }



    }
    protected void btnsNo_Click(object sender, EventArgs e)
    {
        divCancelRemark.Visible = false;
        txtCancelRemark.Text = string.Empty;
        chkCancel.Checked = false;
        btnSubmit_Click(sender, e);
        pnlPG.Visible = false;
        pnlU32.Visible = false;
        lvPG.Visible = false;
        lvU32.Visible = false;
        lvPG.DataSource = null;
        lvPG.DataBind();
        lvU32.DataSource = null;
        lvU32.DataBind();
    }
    protected void imgbtnPrevDoc_Click(object sender, ImageClickEventArgs e)
    {

        //ImageButton btnview = sender as ImageButton;
        //int docno = (btnview.ToolTip != string.Empty ? int.Parse(btnview.ToolTip) : 0);

        //DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "PATH +'\'+FILENAME PATH", "FILENAME", "USERNO=" + Session["studuserno"] + " AND DOCNO = " + Convert.ToInt32(docno), "");

        //string filepath = string.Empty;
        //string filename = string.Empty;
        //int fileCount = 0;
        //foreach (DataRow dr1 in ds.Tables[0].Rows)
        //{
        //    filepath = dr1["PATH"].ToString();
        //    filename = dr1["FILENAME"].ToString();
        //}
        //iframeView.Attributes.Add("src", filepath);

        //ImageButton lnkView = (ImageButton)(sender);
        //string path = lnkView.CommandArgument;
        //string fileName = lnkView.ToolTip;
        //string destFile;

        //int userno = Convert.ToInt32(Session["studuserno"]);
        //string username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Convert.ToInt32(userno));
        //string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
        //path = path.Replace(fileName, "");
        //if (!(Directory.Exists(MapPath("~/ONLINEADMISSION_VERIFY_DOCUMENT/"))))
        //{
        //    Directory.CreateDirectory(MapPath("~/ONLINEADMISSION_VERIFY_DOCUMENT/"));
        //}
        //Directory.CreateDirectory(MapPath("~/ONLINEADMISSION_VERIFY_DOCUMENT/"));
        //string[] files = System.IO.Directory.GetFiles(path,fileName);
        //string uploadPath = HttpContext.Current.Server.MapPath("~/ONLINEADMISSION_VERIFY_DOCUMENT/");
        //// Copy the files and overwrite destination files if they already exist.
        //foreach (string s in files)
        //{
        //    // Use static Path methods to extract only the file name from the path.
        //    fileName = System.IO.Path.GetFileName(s);
        //    destFile = System.IO.Path.Combine(uploadPath, fileName);
        //    System.IO.File.Copy(s, destFile, true);
        //}

        //string filepath = urlpath + "/" + lnkView.ToolTip;
        //iframeView.Attributes.Add("src", filepath);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDocumentPopup", "ShowDocumentPopup();", true);
        //mpeViewDocument.Show();


        string Url = string.Empty;
        string directoryPath = string.Empty;

        string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
        string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var ImageName = img;
        if (img == null || img == "")
        {

            // objCommon.DisplayMessage(this, "Image not Found...", this);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            //ltEmbed.Text = "Image Not Found....!";
            //BindDocument();

        }
        else
        {

            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

            var blob = blobContainer.GetBlockBlobReference(ImageName);

            string filePath = directoryPath + "\\" + ImageName;
            //"D:\\RohitMore\\Code\\CPU_KOTA_OA_CODE\\CPUK_OA_06_06_2022\\CPU-OA_SVN-19-05-2022\\PresentationLayer\\ONLINEIMAGESUPLOAD\\" + ImageName;
            //directoryPath + "\\" + ImageName;


            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }

            blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            // ltEmbed.Text = string.Format(embed, ResolveUrl("~/ONLINEIMAGESUPLOAD/" + ImageName));


            ImageButton lnkView = (ImageButton)(sender);
            string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
            iframeView.Src = urlpath + ImageName;
            mpeViewDocument.Show();

        }





    }
    //#region Unwanted code 
    protected void lvApplicantDocDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            //if ((e.Item.ItemType == ListViewItemType.DataItem))
            //{
            //    ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            //    DataRow dr = ((DataRowView)dataItem.DataItem).Row;
            //    string compareString = ViewState["UploadedString"].ToString();
            //    string stringToCompare = dr["DOCNO"].ToString();
            //    docname = dr["FILENAME"].ToString();
            //    int consist = 0;
            //    consist = objCommon.LookUp("DOCUMENTENTRY_FILE", "DOCNO", "USERNO='" + Convert.ToInt32(Session["studuserno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("DOCUMENTENTRY_FILE", "DOCNO", "USERNO='" + Convert.ToInt32(Session["studuserno"]) + "'"));
            //    if (compareString.Contains(stringToCompare) && consist != 0)
            //    {
            //        string documentname = ((ImageButton)e.Item.FindControl("imgbtnPrevDoc")).CommandName;
            //        DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "PATH +'\\'+FILENAME PATH", "FILENAME", "USERNO=" + Session["studuserno"] + " AND DOCNO = " + Convert.ToInt32(dr["DOCNO"].ToString()), "");
            //        string filepath = string.Empty;
            //        int fileCount = 0;
            //        foreach (DataRow dr1 in ds.Tables[0].Rows)
            //        {
            //            filepath = dr1["PATH"].ToString();

            //            //if (System.IO.File.Exists(filePath))
            //            //{
            //            //    fileCount++;
            //            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + compressedFileName.Replace(" ", "_") + ".zip");
            //            //    Response.ContentType = "application/zip";
            //            //}
            //        }
            //        //string file = filepath + documentname;
            //        FileInfo myfile = new FileInfo(filepath);
            //        // Checking if file exists
            //        if (myfile.Exists)
            //        {
            //            ((ImageButton)e.Item.FindControl("imgbtnPrevDoc")).Visible = true;
            //            ((Label)e.Item.FindControl("lblPreview")).Visible = false;

            //        }
            //        else
            //        {
            //            ((ImageButton)e.Item.FindControl("imgbtnPrevDoc")).Visible = false;
            //            ((Label)e.Item.FindControl("lblPreview")).Visible = true;

            //            ((Label)e.Item.FindControl("lblPreview")).Text = "Preview not available";
            //        }
            //    }
            //    else
            //    {

            //        ((ImageButton)e.Item.FindControl("imgbtnPrevDoc")).Visible = false;
            //        ((Label)e.Item.FindControl("lblPreview")).Visible = true;
            //        ((Label)e.Item.FindControl("lblPreview")).Text = "Preview not available";
            //        ((Label)e.Item.FindControl("lblPreview")).ForeColor = System.Drawing.Color.Red;
            //        ((Label)e.Item.FindControl("lblPreview")).Font.Bold = true;
            //    }
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //#endregion Unwanted Code

    protected void txtMaxMarks_TextChanged(object sender, EventArgs e)
    {
        //ListViewDataItem lvi = lvApplicantMarksDetail.NamingContainer as ListViewDataItem;

        ListViewItem lvi = (ListViewItem)((TextBox)sender).NamingContainer;
        TextBox tb1 = (TextBox)lvi.FindControl("txtObtMarks");
        TextBox tb2 = (TextBox)lvi.FindControl("txtMaxMarks");
        double per = 0.00;
        double ObtMarks = Convert.ToDouble(tb1.Text);
        double MaxMarks = Convert.ToDouble(tb2.Text);

        per = (ObtMarks / MaxMarks) * 100; //percentage calculation.
        per = Math.Round(per, 3);
        Label tb3 = (Label)lvi.FindControl("lblPercent");
        tb3.Text = per.ToString();

    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        Task<int> task = Execute(txtMailMsg.Text, lblEmail.Text, txtMailSubject.Text);
        int status = task.Result;
        if (status == 1)
        {
            objCommon.DisplayUserMessage(updMarks, "Mail send successfully..", this.Page);
            txtMailSubject.Text = string.Empty;
            txtMailMsg.Text = string.Empty;
        }

    }
    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ALIAH");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }
    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlProgramType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "UGPGOT = " + ddlProgramType.SelectedValue, "DEGREENO");
                ddlDegree.Focus();
            }
            else
            {
                ddlDegree.Items.Clear();
                ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            }

            //HideListview();
        }
        catch (Exception ex)
        {
            throw;

        }
    }
    protected void txtObtMarks_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).Parent;
        TextBox tb1 = (TextBox)lvi.FindControl("txtMaxMarks");
        Label tb3 = (Label)lvi.FindControl("lblPercent");
        tb1.Text = string.Empty;
        tb3.Text = string.Empty;
        tb1.Focus();
    }
    protected void chkCancel_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkCancel.Checked)
            {
                divCancelRemark.Visible = true;
            }
            else
            {
                divCancelRemark.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;

        }
    }
    //protected void lvApplicantMarksDetail_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    DataSet ds =(DataSet) ViewState["dsExm"];
    //    DropDownList ddl =new DropDownList() ;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        //for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
    //        //{
    //            //if (ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("101") || ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("102") || ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("103") || ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("104"))
    //            //{

    //                //if (e.Item.ItemType == ListViewItemType.DataItem)
    //                //{
    //                    ddl = e.Item.FindControl("ddlCourse") as DropDownList;
    //                    objCommon.FillDropDownList(ddl, "ACD_COMPULSORY_COURSES", "COURSENO", "COURSENAME", "COURSENO > 0", "COURSENAME ASC");
    //                    //ddl.SelectedValue = ds.Tables[0].Rows[i]["COLUMNID"].ToString();
    //                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
    //                    while (dtr.Read())
    //                    {
    //                        //ddl.Items.Add(new ListItem(dtr["COLUMNNAME"].ToString(), dtr["COLUMNID"].ToString()));
    //                    //    ddl.SelectedValue = dtr["COLUMNID"].ToString();
    //                    //    //string a=ddl.ToolTip;
    //                    }
    //                   // ddl.SelectedValue = ddl.ToolTip;
    //                    //ddl.SelectedItem.Text = ds.Tables[0].Rows[i]["COURSENAME"].ToString();
    //                //}
    //                //ddl.Visible = true;
    //            //}
    //            //else
    //            //{
    //            //   // ddl.Visible = false;
    //            //}
    //        //}
    //    }


    //}
    protected void txtObtMarksU32_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).Parent;
        TextBox txtMaxMarksU32 = (TextBox)lvi.FindControl("txtMaxMarksU32");
        TextBox lblPercentU32 = (TextBox)lvi.FindControl("lblPercentU32");
        txtMaxMarksU32.Text = string.Empty;
        lblPercentU32.Text = string.Empty;
        txtMaxMarksU32.Focus();

    }
    protected void txtMaxMarksU32_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).NamingContainer;
        TextBox tb1 = (TextBox)lvi.FindControl("txtObtMarksU32");
        TextBox tb2 = (TextBox)lvi.FindControl("txtMaxMarksU32");

        double per = 0.00;
        double ObtMarks = Convert.ToDouble(tb1.Text);
        double MaxMarks = Convert.ToDouble(tb2.Text);

        per = (ObtMarks / MaxMarks) * 100; //percentage calculation.
        per = Math.Round(per, 3);
        TextBox tb3 = (TextBox)lvi.FindControl("lblPercentU32");
        tb3.Text = per.ToString();
        calculateAvg();
    }
    protected void txtObtMarksPG_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).Parent;
        TextBox txtMaxMarksPG = (TextBox)lvi.FindControl("txtMaxMarksPG");
        Label lblPercentPG = (Label)lvi.FindControl("lblPercentPG");
        txtMaxMarksPG.Text = string.Empty;
        lblPercentPG.Text = string.Empty;
        txtMaxMarksPG.Focus();
    }
    protected void txtMaxMarksPG_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).NamingContainer;
        TextBox tb1 = (TextBox)lvi.FindControl("txtObtMarksPG");
        TextBox tb2 = (TextBox)lvi.FindControl("txtMaxMarksPG");
        double per = 0.00;
        double ObtMarks = Convert.ToDouble(tb1.Text);
        double MaxMarks = Convert.ToDouble(tb2.Text);

        per = (ObtMarks / MaxMarks) * 100; //percentage calculation.
        per = Math.Round(per, 3);
        Label tb3 = (Label)lvi.FindControl("lblPercentPG");
        tb3.Text = per.ToString();
    }
    protected void HideListview()
    {
        pnlDocument.Visible = false;
        pnlList.Visible = false;
        pnlMarks.Visible = false;
        pnlPG.Visible = false;
        pnlU32.Visible = false;
        lvApplicantdata.Visible = false;
        lvApplicantDocDetails.Visible = false;
        lvApplicantMarksDetail.Visible = false;
        lvPG.Visible = false;
        lvU32.Visible = false;
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //HideListview();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void txtOutofMarksDegree_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).Parent;
        TextBox txtMaxMarksDegree = (TextBox)lvi.FindControl("txtOutofMarksDegree");
        Label lblPercentDegree = (Label)lvi.FindControl("lblPercentDegree");
        txtMaxMarksDegree.Text = string.Empty;
        lblPercentDegree.Text = string.Empty;
        txtMaxMarksDegree.Focus();
    }
    protected void txtObtainedMarksDegree_TextChanged(object sender, EventArgs e)
    {
        ListViewItem lvi = (ListViewItem)((TextBox)sender).NamingContainer;
        TextBox tb1 = (TextBox)lvi.FindControl("txtObtainedMarksDegree");
        TextBox tb2 = (TextBox)lvi.FindControl("txtOutofMarksDegree");
        double per = 0.00;
        double ObtMarks = Convert.ToDouble(tb1.Text);
        double MaxMarks = Convert.ToDouble(tb2.Text);

        per = (ObtMarks / MaxMarks) * 100; //percentage calculation.
        per = Math.Round(per, 3);
        Label tb3 = (Label)lvi.FindControl("lblPercentDegree");
        tb3.Text = per.ToString();
    }
    protected void calculateAvg()
    {
        decimal totalAvg = 0;
        decimal Avg = 0;
        foreach (ListViewDataItem lv in lvU32.Items)
        {
            HiddenField hdnQualifyU32 = lv.FindControl("hdnQualifyU32") as HiddenField;
            TextBox lblPercentU32 = (TextBox)lv.FindControl("lblPercentU32");
            Label lblAvgPercent = lv.FindControl("lblAvgPercent") as Label;
            HiddenField hdn12subType = lv.FindControl("hdn12subType") as HiddenField;
            //Label lblAvgPercent = lv.FindControl("lblAvgPercent") as Label;
            if (hdnQualifyU32.Value.Equals("140"))
            {
                decimal calculate = Convert.ToDecimal(lblPercentU32.Text.ToString());
                totalAvg = totalAvg + calculate;
            }
            if (hdnQualifyU32.Value.Equals("140") && hdn12subType.Value.Equals("5"))
            {
                Avg = totalAvg / 5;
                lblAvgPercent.Text = Avg.ToString();
            }
        }



    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 ", "B.LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            }

            lvApplicantdata.DataSource = null;
            lvApplicantdata.DataBind();
            lvApplicantdata.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    public void clear()
    {

        lvApplicantdata.Visible = false;
        lvApplicantdata.DataSource = null;
        lvApplicantdata.DataBind();
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlProgramType.SelectedIndex = 0;
        divApplicantDetail.Visible = false;
    }




    //ADDED BY POOJA for Tab1

    protected void rdoadmissionno_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (rdoadmissionno.SelectedValue == "1")
        {
            PanelNPFadmissionno.Visible = true;
            Paneladmissionno.Visible = false;

        }
        else
        {
            PanelERPadmissionno.Visible = true;
            Paneladmissionno.Visible = false;
        }
    }

    public byte[] ResizePhoto(byte[] bytes)
    {
        byte[] image = null;
        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(bytes);

        System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(myMemStream);

        int imageHeight = imageToBeResized.Height;
        int imageWidth = imageToBeResized.Width;
        int maxHeight = 240;
        int maxWidth = 320;
        imageHeight = (imageHeight * maxWidth) / imageWidth;
        imageWidth = maxWidth;

        if (imageHeight > maxHeight)
        {
            imageWidth = (imageWidth * maxHeight) / imageHeight;
            imageHeight = maxHeight;
        }

        // Saving image to smaller size and converting in byte[]
        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
        System.IO.MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        stream.Position = 0;
        image = new byte[stream.Length + 1];
        stream.Read(image, 0, image.Length);
        return image;
    }

    //for npf
    private void bindlist()
    {

        StudentController objSC = new StudentController();
        divnpfbtn.Visible = true;
        divnpfRemark.Visible = true;
        btnnpfsYes.Visible = true;
        btnnpfno.Visible = false;
        string isUploaded = string.Empty;
        string npfregno = txtREGNo.Text.ToString();
        if (npfregno == string.Empty)
        {
            objCommon.DisplayMessage("Enter Application No", this.Page);
            return;
        }
        //DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "EMAIL_ADDRESS", "FIRST_NAME,MOBILE_NUMBER,USERNAME,DATE_OF_BIRTH,N.APPLICATION_NO", "N.APPLICATION_NO = "+npfregno, "");
        DataSet ds = objSC.GetStudentInfoUsingApplicationno(npfregno);
        // DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR LEFT JOIN ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "EMAILID", "FIRSTNAME,MOBILENO,DOB,USERNAME,N.APPLICATION_NO", " USERNO = " + UserNo, string.Empty);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtnpfdob.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
            lblnpfemail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblnpfname.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
            lblnpfmobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
            lblNPFApplicationId.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            lblERPApplicationId.Text = ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString();

            // DataSet ds1 = objCommon.FillDropDown("ACD_USER_REGISTRATION UR LEFT JOIN ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "EMAILID", "FIRSTNAME,MOBILENO,DOB,USERNAME,N.APPLICATION_NO", " USERNO = " + npfregno, string.Empty);


            int docno = 0;
            int userno = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + lblNPFApplicationId.Text + "'"));
            using (WebClient webClient = new WebClient())
            {
                DataSet ds1 = objCommon.FillDropDown("ACD_USER_REGISTRATION UR inner JOIN ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "ur.USERNO", "N.FILE_UPLOAD_PASSPORT_SIZE_PHOTOGRAPH[PHOTOGRAPH],N.FILE_UPLOAD_SIGNATURE[SIGNATURE],USERNAME,N.APPLICATION_NO", " N.APPLICATION_NO = '" + npfregno + "'", string.Empty);

                ds1.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                string path = dt.Rows[0]["PHOTOGRAPH"].ToString();
                string SIGNPATH = dt.Rows[0]["SIGNATURE"].ToString();
                byte[] data = webClient.DownloadData(path);
                byte[] signdata = webClient.DownloadData(SIGNPATH);

               
                objstud.IdNo = userno;

                //Photo Conversion
                int bytes = data.Length;
                double kilobytes = bytes / 1024.0;
                if (kilobytes > 150)
                {
                    data = ImageCompression.CompressImage(data, 150);
                    //data = ResizePhoto(data);
                    int fileSize2 = data.Length;
                }
                //END

                //Sign Conversion
                int bytessign = signdata.Length;
                double kilobytessign = bytes / 1024.0;
                if (kilobytessign > 150)
                {
                    signdata = ImageCompression.CompressImage(signdata, 150);
                    //signdata = ResizePhoto(signdata);
                    int fileSize2 = signdata.Length;
                }
                //END

                CustomStatus cs;
                objstud.StudPhoto = data;
                cs = (CustomStatus)objOAC.UpdateStudPhotoFromNpfPhotos(objstud, 100);
                objstud.StudPhoto = null;
                objstud.StudPhoto = signdata;
                cs = (CustomStatus)objOAC.UpdateStudPhotoFromNpfPhotos(objstud, 1);
                //if (cs.Equals(CustomStatus.RecordUpdated))
                //{
                //    //objCommon.DisplayMessage(this.Page, "Photo uploaded Successfully!!", this.Page);

                //}
            }



            //string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
            //ViewState["UploadedString"] = realUploaded;
            // DataTable dt = ds.Tables[0];
            //foreach (DataRow dr in dt.Rows)
            //{
            //    isUploaded += dr["NPF_ID"].ToString() + ", ";
            //}

            //string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
            //ViewState["UploadedString"] = realUploaded;

            Lvnpfpreview.DataSource = ds;
            Lvnpfpreview.DataBind();
        }
        else
        {
            Lvnpfpreview.DataSource = null;
            Lvnpfpreview.DataBind();
        }

        divnpfApplicantDetail.Visible = true;
        divnpfRemark.Visible = true;
        divnpfbtn.Visible = true;

    }
    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        bindlist();
    }

    //for erpno
    private void bindlistbyerpno()
    {

        StudentController objSC = new StudentController();
        dverpbtn.Visible = true;
        diverpremark.Visible = true;
        btberpyes.Visible = true;
        btnerpno.Visible = false;
        string isUploaded = string.Empty;
        string erpregno = txterpregno.Text.ToString();
        if (erpregno == string.Empty)
        {
            objCommon.DisplayMessage("Enter Application No", this.Page);
            return;
        }
        //DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION N LEFT JOIN ACD_USER_REGISTRATION UR ON(N.MOBILE_NUMBER=UR.MOBILENO)", "EMAIL_ADDRESS", "FIRST_NAME,MOBILE_NUMBER,USERNAME,DATE_OF_BIRTH,N.APPLICATION_NO", "N.APPLICATION_NO = "+npfregno, "");
        DataSet ds = objSC.GetStudentInfoUsingERPApplicationno(erpregno);
        // DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR LEFT JOIN ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "EMAILID", "FIRSTNAME,MOBILENO,DOB,USERNAME,N.APPLICATION_NO", " USERNO = " + UserNo, string.Empty);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txterpdob.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
            lblerpemail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblerpname.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
            lblerpmobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
            lblerpid.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            lblerpapplicationno.Text = ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString();
            //}
            //{
            int docno = 0;
            int userno = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + erpregno + "'"));
            using (WebClient webClient = new WebClient())
            {
                DataSet ds1 = objCommon.FillDropDown("ACD_USER_REGISTRATION UR inner JOIN ACD_NPF_DATA N ON(UR.MOBILENO=N.MOBILE_NUMBER)", "ur.USERNO", "N.FILE_UPLOAD_PASSPORT_SIZE_PHOTOGRAPH[PHOTOGRAPH],N.FILE_UPLOAD_SIGNATURE[SIGNATURE],USERNAME,N.APPLICATION_NO", " USERNAME = '" + erpregno + "'", string.Empty);

                ds1.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                string path = dt.Rows[0]["PHOTOGRAPH"].ToString();
                string SIGNPATH = dt.Rows[0]["SIGNATURE"].ToString();
                byte[] data = webClient.DownloadData(path);

                objstud.IdNo = userno;
                objstud.StudPhoto = data;
                CustomStatus cs = (CustomStatus)objOAC.UpdateStudPhotoFromNpfPhotos(objstud, docno);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //objCommon.DisplayMessage(this.Page, "Photo uploaded Successfully!!", this.Page);

                }


            }

            // }


            Lverppreview.DataSource = ds;
            Lverppreview.DataBind();
        }
        else
        {
            Lverppreview.DataSource = null;
            Lverppreview.DataBind();
        }

        divnpfApplicantDetail.Visible = false;
        diverpapplicationDetails.Visible = true;
        divnpfRemark.Visible = true;
        divnpfbtn.Visible = true;


    }
    protected void btnsearchERP_Click(object sender, System.EventArgs e)
    {
        bindlistbyerpno();
    }

    protected void btnerpcancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnnpfcancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnnpfsYes_Click(object sender, System.EventArgs e)
    {
        try
        {

            CustomStatus cs = new CustomStatus();
            int cancelcount = 0;
            string remarkSingle = string.Empty;
            string remarkCancel = string.Empty;
            int cancel = 0;
            int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + lblNPFApplicationId.Text + "'"));
            if (chknpfverify.Checked)
            {
                cancelcount++;
                if (txtnpfcancel.Text.Equals(string.Empty))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Cancel Remark.", this.Page);
                    txtnpfcancel.Focus();
                    return;
                }
                cancel = 1;
                remarkCancel = txtnpfcancel.Text.TrimEnd();
            }
            if (!txtnpfremark.Text.Equals(string.Empty))
            {
                remarkSingle = txtnpfremark.Text.TrimEnd();
            }


            if (cancelcount == 0)
            {


                cs = (CustomStatus)objSC.Update_Cancel_Document_Verification_Status(USERNO,
                 Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), remarkSingle, cancel, remarkCancel);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this.Page, "Documents Verified Successfully.", this.Page);
                    clear();

                }
            }
            else if (cancelcount == 1)
            {
                cs = (CustomStatus)objSC.Update_Cancel_Document_Verification_Status(USERNO,
                Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), remarkSingle, cancel, remarkCancel);
                objCommon.DisplayMessage(this.Page, "Verification Cancel Successfully.", this.Page);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    // Response.Redirect(Request.Url.ToString());
                    clear();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Applicant Information not found!", this.Page);
            }

        }
        // Response.Redirect(Request.Url.ToString());
        //}
        //}
        catch (Exception ex)
        {
            throw;
        }


    }
    protected void chknpfverify_CheckedChanged(object sender, System.EventArgs e)
    {
        try
        {
            if (chknpfverify.Checked)
            {
                dvnpfcancel.Visible = true;
            }
            else
            {
                dvnpfcancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;

        }
    }
    protected void chkerpremark_CheckedChanged(object sender, System.EventArgs e)
    {

        try
        {
            if (chkerpremark.Checked)
            {
                dverpcancel.Visible = true;
            }
            else
            {
                dverpcancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;

        }
    }
    protected void btberpyes_Click(object sender, System.EventArgs e)
    {
        try
        {

            CustomStatus cs = new CustomStatus();
            int cancelcount = 0;
            string remarkSingle = string.Empty;
            string remarkCancel = string.Empty;
            int cancel = 0;
            int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + lblerpid.Text + "'"));
            if (chkerpremark.Checked)
            {
                cancelcount++;
                if (txterpcancel.Text.Equals(string.Empty))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Cancel Remark.", this.Page);
                    txterpcancel.Focus();
                    return;
                }
                cancel = 1;
                remarkCancel = txterpcancel.Text.TrimEnd();
            }
            if (!txterpremark.Text.Equals(string.Empty))
            {
                remarkSingle = txterpremark.Text.TrimEnd();
            }


            if (cancelcount == 0)
            {


                cs = (CustomStatus)objSC.Update_Cancel_Document_Verification_Status(USERNO,
                 Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), remarkSingle, cancel, remarkCancel);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this.Page, "Documents Verified Successfully.", this.Page);
                    clear();

                }
            }
            else if (cancelcount == 1)
            {
                cs = (CustomStatus)objSC.Update_Cancel_Document_Verification_Status(USERNO,
                Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), remarkSingle, cancel, remarkCancel);
                objCommon.DisplayMessage(this.Page, "Verification Cancel Successfully.", this.Page);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    // Response.Redirect(Request.Url.ToString());
                    clear();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Applicant Information not found!", this.Page);
            }

        }
        // Response.Redirect(Request.Url.ToString());
        //}
        //}
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void lbdownloaderp_Click(object sender, System.EventArgs e)
    {
        try
        {

            LinkButton lbdownloaderp = sender as LinkButton;
            int docno = Convert.ToInt32(lbdownloaderp.CommandArgument);
            ViewState["DOCUMENTNO"] = docno;
            DownloadERPDocument();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QualificationDetails.btnDownload_SSS_Certificate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void lbnpfdownload_Click(object sender, System.EventArgs e)
    {
        try
        {
            LinkButton lbnpfdownload = sender as LinkButton;

            int docno = Convert.ToInt32(lbnpfdownload.CommandArgument);
            ViewState["DOCUMENTNO"] = docno;

            DownloadNPFDocument();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QualificationDetails.DownloadCertificate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void DownloadNPFDocument()
    {
        try
        {

            string npfregno = txtREGNo.Text.ToString();
            int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + lblNPFApplicationId.Text + "'"));

            //   DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE f inner join ACD_USER_REGISTRATION ur on(ur.USERNO=f.USERNO)", "DOCNO", "ur.userno,DOCNAME,FILENAME,PATH", "ur.userno = " + USERNO + "", string.Empty);
            DataSet ds = objSC.GetStudentPhotoInfoByNPFapplicationno(Convert.ToInt32(ViewState["DOCUMENTNO"]), npfregno);
            //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_LAST_QUALIFICATION", "UNO", "CERTIFICATE", "UNO=" + userno + "AND QUALIFYNO=" + qualifyno + "", "UNO");
            string FileName = ds.Tables[0].Rows[0]["FILENAME"].ToString();
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);

            string img = ds.Tables[0].Rows[0]["FILENAME"].ToString();
            string extension = Path.GetExtension(img.ToString());
            var ImageName = img;
            if (img != null || img != "")
            {
                if (extension == ".pdf")
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "image/jpeg";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    //Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QualificationDetails.DownloadCertificate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }


    private void DownloadERPDocument()
    {
        try
        {
            int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + lblerpid.Text + "'"));
            string erpRegno = txterpregno.Text.ToString();
            //DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE f inner join ACD_USER_REGISTRATION ur on(ur.USERNO=f.USERNO)", "DOCNO", "ur.userno,DOCNAME,FILENAME,PATH", "ur.userno = " + USERNO + "", string.Empty);
            DataSet ds = objSC.GetStudentPhotoInfoByNPFapplicationno_ERP(Convert.ToInt32(ViewState["DOCUMENTNO"]), erpRegno);
            //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_LAST_QUALIFICATION", "UNO", "CERTIFICATE", "UNO=" + userno + "AND QUALIFYNO=" + qualifyno + "", "UNO");
            string FileName = ds.Tables[0].Rows[0]["FILENAME"].ToString();
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);

            string img = ds.Tables[0].Rows[0]["FILENAME"].ToString();
            string extension = Path.GetExtension(img.ToString());
            var ImageName = img;
            if (img != null || img != "")
            {
                if (extension == ".pdf")
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "image/jpeg";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    //Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QualificationDetails.DownloadCertificate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void imgbtnerpPrevDoc_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;

        string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
        string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var ImageName = img;
        if (img == null || img == "")
        {

            // objCommon.DisplayMessage(this, "Image not Found...", this);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            //ltEmbed.Text = "Image Not Found....!";
            //BindDocument();

        }
        else
        {

            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

            var blob = blobContainer.GetBlockBlobReference(ImageName);

            string filePath = directoryPath + "\\" + ImageName;
            //"D:\\RohitMore\\Code\\CPU_KOTA_OA_CODE\\CPUK_OA_06_06_2022\\CPU-OA_SVN-19-05-2022\\PresentationLayer\\ONLINEIMAGESUPLOAD\\" + ImageName;
            //directoryPath + "\\" + ImageName;


            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }

            blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            // ltEmbed.Text = string.Format(embed, ResolveUrl("~/ONLINEIMAGESUPLOAD/" + ImageName));


            ImageButton lnkView = (ImageButton)(sender);
            string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
            //iframeViewerp.Src = urlpath + ImageName;
            //mpeViewerpdocument.Show();
            iframeView.Src = urlpath + ImageName;
            mpeViewDocument.Show();
        }

    }
    protected void imgbtnpfPrevDoc_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;

        string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
        string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var ImageName = img;
        if (img == null || img == "")
        {

            // objCommon.DisplayMessage(this, "Image not Found...", this);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            //ltEmbed.Text = "Image Not Found....!";
            //BindDocument();

        }
        else
        {

            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

            var blob = blobContainer.GetBlockBlobReference(ImageName);

            string filePath = directoryPath + "\\" + ImageName;
            //"D:\\RohitMore\\Code\\CPU_KOTA_OA_CODE\\CPUK_OA_06_06_2022\\CPU-OA_SVN-19-05-2022\\PresentationLayer\\ONLINEIMAGESUPLOAD\\" + ImageName;
            //directoryPath + "\\" + ImageName;


            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }

            blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            // ltEmbed.Text = string.Format(embed, ResolveUrl("~/ONLINEIMAGESUPLOAD/" + ImageName));


            ImageButton lnkView = (ImageButton)(sender);
            string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
            //iframeViewerp.Src = urlpath + ImageName;
            //mpeViewerpdocument.Show();
            iframeView.Src = urlpath + ImageName;
            mpeViewDocument.Show();

        }
    }



    public int Blob_Upload(string ConStr, string ContainerName, string filename, byte[] data)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        //string Ext = Path.GetExtension(FU.FileName);
        string FileName = filename;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(data))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    protected void btnfetch_Click1(object sender, System.EventArgs e)
    {

        int docno = int.Parse((sender as Button).CommandArgument);
        ViewState["DOCUMENTNO"] = docno;
        string npfregno = txtREGNo.Text.ToString();

        {
            int userno = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + lblNPFApplicationId.Text + "'"));
            DataSet ds = objSC.GetStudentPhotoInfoByNPFapplicationno(docno, npfregno);
            ds.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
            //ds.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
            //    DataTable dt = (ds.Tables[0].DefaultView).ToTable();
            DataTable dt = ds.Tables[0];
            //string documentno = dt.Rows[0]["DOCUMENTNO"].ToString();
            string file = dt.Rows[0]["LINKS"].ToString();

            //string file = dt.Rows[0]["DOCUMENTS"].ToString();
            string username = dt.Rows[0]["USERNAME"].ToString();
            string ext = string.Empty;
                //file.Split('.').Last();
            ext = Path.GetExtension(new Uri(file).AbsolutePath);
            byte[] data;
            using (WebClient webClient = new WebClient())
            {
                data = webClient.DownloadData(file);

            }
            string datetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").ToString();
            string filename = username + "_" + userno + "_" + docno + "_" + datetime + "." + ext;
            //byte[] bytes = System.IO.File.ReadAllBytes(localPath);
            Blob_Upload(blob_ConStr, blob_ContainerName, filename, data);
            //objCommon.DisplayMessage(this.upduserinfo, documentno.ToString(), this.Page);
            int result = objSC.UpdateDocumentnpf(userno, Convert.ToInt32(docno), filename);
            if (result == 1)
            {
                objCommon.DisplayMessage(this.Page, "Document uploaded Successfully!!", this.Page);

            }
            bindlist();
        }




    }
    protected void btnfetcherp_Click(object sender, System.EventArgs e)
    {

        int docno = int.Parse((sender as Button).CommandArgument);
        ViewState["DOCUMENTNO"] = docno;
        string erpregno = txterpregno.Text.ToString();

        {
            int userno = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + erpregno + "'"));
            DataSet ds = objSC.GetStudentPhotoInfoByNPFapplicationno_ERP(docno, erpregno);
            ds.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
            //ds.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
            // DataTable dt = (ds.Tables[0].DefaultView).ToTable();
            DataTable dt = ds.Tables[0];
            //string documentno = dt.Rows[0]["DOCUMENTNO"].ToString();
            string file = dt.Rows[0]["LINKS"].ToString();

            //string file = dt.Rows[0]["DOCUMENTS"].ToString();
            string username = dt.Rows[0]["USERNAME"].ToString();
            string ext = string.Empty;
            ext = Path.GetExtension(new Uri(file).AbsolutePath);
                //file.Split('.').Last();
            byte[] data;
            using (WebClient webClient = new WebClient())
            {
                data = webClient.DownloadData(file);

            }
            string filename = username + "_" + userno + "_" + docno + "." + ext;
            //byte[] bytes = System.IO.File.ReadAllBytes(localPath);
            Blob_Upload(blob_ConStr, blob_ContainerName, filename, data);
            //objCommon.DisplayMessage(this.upduserinfo, documentno.ToString(), this.Page);
            int result = objSC.UpdateDocumentnpf(userno, Convert.ToInt32(docno), filename);
            if (result == 1)
            {
                objCommon.DisplayMessage(this.Page, "Document uploaded Successfully!!", this.Page);

            }
            bindlistbyerpno();
        }
    }


    protected void btncancelreset_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btncancelerp_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}