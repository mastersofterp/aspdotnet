//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : SECTION ALLOTMENT                                                     
// CREATION DATE : 27-Sept-2010                                                          
// CREATED BY    : NIRAJ D. PHALKE                                 
// MODIFIED DATE : 28/10/2021                                                         
// MODIFIED DESC : ADDED Switch for active status and Activity.                                                                     
//======================================================================================
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;



public partial class Academic_SessionCreate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                BindListView();
            }
            //Populate the Drop Down Lists
            PopulateDropDown();
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  
            objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND ActiveStatus=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Fill the Odd/Even DropDownList
            objCommon.FillDropDownList(ddlOddEven, "ACD_SESSION_TYPE", "STNO", "SESSIONTYPE", "ISNULL(ACTIVESTATUS,0)=1", "STNO");
            //Fill the Exam Status DropDownList
            objCommon.FillExamStatus(ddlStatus);

            //Added by Nehal on Dated-20/02/2023
            objCommon.FillDropDownList(ddlSessionCollege, "ACD_SESSION", "SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC");


        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void BindListView()
    {
        try
        {
            //Added by Nehal on Dated-20/02/2023
            SessionController objSC = new SessionController();
            DataSet ds = objSC.GetAllSession_Modified();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvSession.DataSource = ds;
                lvSession.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSession);//Set label - 
            }
            else
            {
                pnlSession.Visible = false;
                lvSession.DataSource = null;
                lvSession.DataBind();
            }
            DataSet dsM = objSC.GetAllSession();

            if (dsM != null && dsM.Tables[0].Rows.Count > 0)
            {
                pnlCollegeMap.Visible = true;
                lvCollegeMap.DataSource = dsM;
                lvCollegeMap.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCollegeMap);//Set label - 
            }
            else
            {
                pnlCollegeMap.Visible = false;
                lvCollegeMap.DataSource = null;
                lvCollegeMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //BindListView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage(this.UPDMASTER, "Please select Start Date End Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];
        //DateTime dateTime10 = Convert.ToDateTime(a);
        DateTime dtStartDate = DateTime.Parse(StartDate);
        string SDate = dtStartDate.ToString("yyyy/MM/dd");
        DateTime dtEndDate = DateTime.Parse(EndDate);
        string EDate = dtEndDate.ToString("yyyy/MM/dd");

        if (txtSeqNo.Text == string.Empty || txtSeqNo.Text == " ")
        {
            objCommon.DisplayMessage(this.UPDMASTER, "Please Enter Sequence Number & Sequence Number Should not be Blank", this.Page);
            return;
        }

        try
        {
            //if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            //{
            if (Convert.ToDateTime(EDate) <= Convert.ToDateTime(SDate))
            {
                objCommon.DisplayMessage(this.UPDMASTER, "End Date should be greater than Start Date", this.Page);
                return;
            }
            else
            {
                //Set all properties
                SessionController objSC = new SessionController();
                IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Session();
                objSession.Session_PName = txtSLongName.Text.Trim();
                objSession.Session_Name = txtSShortName.Text.Trim();
                objSession.Session_SDate = Convert.ToDateTime(SDate);
                objSession.Session_EDate = Convert.ToDateTime(EDate);
                objSession.Odd_Even = Convert.ToInt32(ddlOddEven.SelectedValue);
                objSession.ExamType = Convert.ToInt32(ddlStatus.SelectedValue);
                objSession.Sessname_hindi = txtSessName_Hindi.Text.Trim();

                //objSession.College_Id = Convert.ToInt32(ddlCollege.SelectedValue); //Added By Rishabh On 25/01/2022

                // add by maithili [19-08-2022]
                //modified
                //int count = 0;
                //string College_Id = string.Empty;

                //foreach (ListItem Item in ddlCollege.Items)
                //{
                //    if (Item.Selected)
                //    {
                //        College_Id += Item.Value + ",";
                //        count++;
                //    }
                //}
                //objSession.College_Id_str = College_Id.Substring(0, College_Id.Length - 1);

                //End 


                //Added By Rishabh on Dated 28/10/2021
                if (hfdActive.Value == "true")
                {
                    objSession.IsActive = true;
                }
                else
                {
                    objSession.IsActive = false;
                }

                objSession.academic_year = txtacadyear.Text.Trim();
                objSession.sequence_no = Convert.ToInt32(txtSeqNo.Text.Trim()); //Added by Vinay Mishra on Dated 16/06/2023

                objSession.College_code = Session["colcode"].ToString();

                if (hfdStart.Value == "true")
                {
                    objSession.Flock = true;
                }
                else
                {
                    objSession.Flock = false;
                }

                //Added Mahesh on Dated 29/07/2021
                // objSession.PROVISIONAL_CERTIFICATE_SESSION_NAME = txtProvisionalCertificateSessionName.Text;

                //Check for add or edit
                if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
                {
                    //Edit 
                    objSession.SessionNo = Convert.ToInt32(Session["sessionno"]);
                    CustomStatus cs = (CustomStatus)objSC.UpdateSession_Modified(objSession); //Added Nehal on Dated 20/02/2021
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearControls();
                        BindListView();
                        objCommon.DisplayMessage(this.UPDMASTER, "Record Updated Successfully", this.Page);
                    }
                }
                else
                {
                    //Add New
                    CustomStatus cs = (CustomStatus)objSC.AddSession_Modified(objSession); //Added Nehal on Dated 20/02/2021
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UPDMASTER, "Session Added Successfully", this.Page);
                        ClearControls();
                        BindListView();
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.UPDMASTER, "Record Already Exist", this.Page);
                    }
                    else
                    {
                        //msgLbl.Text = "Record already exist";
                        objCommon.DisplayMessage(this.UPDMASTER, "Record Already Exist", this.Page);
                    }
                    //else if (cs.Equals(CustomStatus.TransactionFailed))                    
                    //{
                    //    objCommon.DisplayMessage(this.updSession, "Transaction Failed", this.Page);
                    //}
                }
            }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updSession, "Server UnAvailable", this.Page);
            //    return;
            //}       
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SESSIONNO = int.Parse(btnEdit.CommandArgument);
            Session["sessionno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowDetails(SESSIONNO);
            txtSLongName.Focus();
            ddlCollege.Enabled = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowDetails(int Session_No)
    {
        try
        {
            //Added by Nehal on Dated-20/02/2023
            SessionController objSS = new SessionController();
            SqlDataReader dr = objSS.GetSingleSession_Modified(Session_No);
            if (dr != null)
            {
                if (dr.Read())
                {
                    //ddlCollege.Text = dr["COLLEGE_ID"].ToString();  // Added By Rishabh on 25/01/2022
                    //ddlCollege.SelectedIndex = Convert.ToInt32(dr.["COLLEGE_ID"].ToString());  // modify by maithili [19-08-2022]

                    txtSLongName.Text = dr["SESSION_PNAME"] == null ? string.Empty : dr["SESSION_PNAME"].ToString();
                    //chkFlock.Checked = Convert.ToBoolean (dr["FLOCK"]) == null ? false : Convert.ToBoolean(dr["FLOCK"]);
                    txtSShortName.Text = dr["SESSION_NAME"] == null ? string.Empty : dr["SESSION_NAME"].ToString();
                    txtStartDate.Text = dr["SESSION_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["SESSION_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtEndDate.Text = dr["SESSION_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["SESSION_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtSessName_Hindi.Text = dr["SESSNAME_HINDI"] == null ? string.Empty : dr["SESSNAME_HINDI"].ToString();
                    //hdnDate.Value = Convert.ToDateTime(dr["SESSION_STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(dr["SESSION_ENDDATE"].ToString()).ToString("MMM dd, yyyy");
                    hdnDate.Value = dr["SESSION_STDATE"] != DBNull.Value ? Convert.ToDateTime(dr["SESSION_STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(dr["SESSION_ENDDATE"].ToString()).ToString("MMM dd, yyyy") : Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy");

                    txtacadyear.Text = dr["ACADEMIC_YEAR"] == null ? string.Empty : dr["ACADEMIC_YEAR"].ToString();
                    txtSeqNo.Text = dr["SEQUENCE_NO"] == null ? string.Empty : dr["SEQUENCE_NO"].ToString();

                    if (dr["ODD_EVEN"] == null | dr["ODD_EVEN"].ToString().Equals(""))
                        ddlOddEven.SelectedIndex = 0;
                    else
                        ddlOddEven.Text = dr["ODD_EVEN"].ToString();

                    if (dr["EXAMTYPE"] == null | dr["EXAMTYPE"].ToString().Equals(""))
                        ddlStatus.SelectedIndex = 0;
                    else
                        ddlStatus.Text = dr["EXAMTYPE"].ToString();

                    //Added By Rishabh on Dated 28/10/2021
                    if (dr["IS_ACTIVE"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                    }

                    if (dr["FLOCK"].ToString() == "Start")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatStart(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatStart(false);", true);
                    }
                    //Added Mahesh On Dated 21-01-2021
                    //chkIsActive.Checked = dr["IS_ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(dr["IS_ACTIVE"]);
                    //txtProvisionalCertificateSessionName.Text = dr["PROVISIONAL_CERTIFICATE_SESSION_NAME"] == DBNull.Value ? "" : Convert.ToString(dr["PROVISIONAL_CERTIFICATE_SESSION_NAME"]);

                    ScriptManager.RegisterClientScriptBlock(UPDMASTER, UPDMASTER.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                }

            }
            if (dr != null) dr.Close();

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlOddEven.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtSLongName.Text = string.Empty;
        txtSShortName.Text = string.Empty;
        txtSeqNo.Text = string.Empty;
        //chkFlock.Checked = false;
        Session["action"] = null;
        txtacadyear.Text = string.Empty;
        txtProvisionalCertificateSessionName.Text = string.Empty;
        //ddlCollege.SelectedIndex = 0;
        //ddlCollege.Enabled = true;
        ddlCollege.ClearSelection(); // Modify by maithili [20-08-2022]
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("SessionMaster", "rptSessionMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UPDMASTER, this.UPDMASTER.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //protected void ddlCollege_ItemDataBound(object sender, ListViewItemEventArgs e) // Add by Maithili [19-08-2022]
    //{
    //    ListViewDataItem dataitem = (ListViewDataItem)e.Item;
    //}
    protected void btnCancel2_Click(object sender, EventArgs e) // Add by Nehal [20-02-2023]
    {
        ClearControls();
    }
    protected void btnSubmit2_Click(object sender, EventArgs e) // Add by Nehal [20-02-2023]
    {
        try
        {
            SessionController objSC = new SessionController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Session();

            int count = 0;
            string College_Id = string.Empty;

            foreach (ListItem Item in ddlCollege.Items)
            {
                if (Item.Selected)
                {
                    College_Id += Item.Value + ",";
                    count++;
                }
            }
            objSession.College_Id_str = College_Id.Substring(0, College_Id.Length - 1);

            int Sessionid = Convert.ToInt32(ddlSessionCollege.SelectedValue);

            CustomStatus cs = (CustomStatus)objSC.AddSessionMaster_Modified(objSession, Sessionid);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ClearControls();
                BindListView();
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Session Added Successfully", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Already Exist", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Already Exist", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnDeActive_Click(object sender, EventArgs e)
    {
        try
        {
             SessionController objSC = new SessionController();
            int sessionno = Convert.ToInt32((sender as Button).CommandArgument);
            string status = Convert.ToString((sender as Button).ToolTip);
            int activestatus = 0;
            if (status == "Active")
            {
                activestatus = 1;
            }
            else
            {
                activestatus = 0;
            }
            string ipAdress = Request.ServerVariables["REMOTE_ADDR"];
            CustomStatus cs = (CustomStatus)objSC.ActiveDeactiveSessionConfiguration(sessionno, Convert.ToInt32(Session["userno"]), ipAdress, activestatus);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Recored Update Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Already Exist", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
