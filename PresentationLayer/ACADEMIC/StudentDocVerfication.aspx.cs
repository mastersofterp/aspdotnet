using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;


public partial class ACADEMIC_StudentDocVerfication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                    this.CheckPageAuthorization();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //  string sessionno = objCommon.LookUp("ACD_ADMCONFIGURATION", "SESSIONNO", string.Empty);
                    //if (string.IsNullOrEmpty(sessionno))
                    //{
                    //    objCommon.DisplayMessage("Please Set the Admission Session from Reference Page!!", this.Page);
                    //    return;
                    //}
                    //else
                    //{


                    //objCommon.FillDropDownList(ddlSession, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    //ddlSession.Items.RemoveAt(0);


                    //objCommon.FillDropDownList(ddlWing, "ACD_HOSTEL_WING", "WINGNO", "WINGNAME", "WINGNO!=0", "WINGNO");
                    //objCommon.FillDropDownList(ddlRoomno, "ACD_HOSTEL_ROOM_ALLOT", "RNO", "ROOMNAME", "RNO!=0", "RNO");
                    //objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_FLOOR", "FLOOR_NO", "FLOOR_NAME", "FLOOR_NO!=0", "FLOOR_NO");

                    //}

                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocVerfication.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentDocVerfication.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string userno = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtAppID.Text.ToString() + "'");


            //New changes on Documents verification form..Documents are verify without having allotmenmt status true for degree B-tech,B-tec+M-tech,B-tech+Mba and other degrees[19-July-2016].

            if (userno != string.Empty)
            {

                #region CODE SHOW OTHER ALL DEGREE DETAILS WITHOUT HAVING ALLOTMENT STATUS TRUE

                 int status = Convert.ToInt32(objCommon.LookUp("ACD_USER_PROFILE_STATUS", "ISNULL(CONFIRM_STATUS,0) AS CONFIRM_STATUS", "USERNO='" + userno.ToString() + "'"));
                 if (status == 1)
                 {
                     ViewState["newuser"] = userno;


                     ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR left JOIN DOCUMENTENTRY_FILE NR ON(NR.USERNO=UR.USERNO) LEFT OUTER JOIN ACD_STUDENT_ENTRENCE_DETAILS ED ON(UR.USERNO=ED.USERNO) LEFT OUTER JOIN ACD_QUALEXM Q ON(Q.QUALIFYNO=ED.ENTR_EXAM_NO)left JOIN ACD_USER_PROFILE_STATUS PS ON(PS.USERNO=UR.USERNO) left JOIN ACD_FEES_LOG F ON(F.USERNO=UR.USERNO)", "UR.USERNO,USERNAME,UR.FIRSTNAME,LASTNAME,ED.REGNO,ED.VERIFY_MARKS,ED.ENTR_EXAM_NO,Q.QUALIEXMNAME", "NR.DOCNO,NR.DOCUMENT_STATUS,EMAILID,MOBILENO", "  UR.USERNAME='" + txtAppID.Text + "'", "UR.DEGREENO DESC");

                     if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                     {
                         fldstudent.Visible = true;

                         lblAppID.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                         lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                         lblEmail_id.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                         lblMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                         //  lblFatherName.Text = ds.Tables[0].Rows[0]["LASTNAME"].ToString();


                         if (ds.Tables[0].Rows[0]["REGNO"].ToString() == string.Empty &&
                             ds.Tables[0].Rows[0]["VERIFY_MARKS"].ToString() == string.Empty &&
                             ds.Tables[0].Rows[0]["QUALIEXMNAME"].ToString() == string.Empty)
                         {
                             TREntranceDetails.Visible = false;

                             // lblDegreeName.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();

                         }
                         else
                         {
                             TREntranceDetails.Visible = true;

                             lblJEERollNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                             lblJtotal.Text = ds.Tables[0].Rows[0]["VERIFY_MARKS"].ToString();
                             lblexamname.Text = ds.Tables[0].Rows[0]["QUALIEXMNAME"].ToString();
                             // lblDegreeName.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();

                         }
                         DataSet dsdocument = objCommon.FillDropDown("ACD_DOCUMENT_LIST DT LEFT JOIN DOCUMENTENTRY_FILE D ON (DT.DOCUMENTNO=D.DOCUMENTTYPENO AND USERNO=" + ViewState["newuser"] + ")", "ROW_NUMBER() over(order by DT.DOCUMENTNO) AS SRNO,DOCUMENTNAME", "DT.DOCUMENTNO,ISNULL(DOCUMENT_STATUS,0) DOCUMENT_STATUS", "DT.DOCUMENTNO>0", string.Empty);
                         if (dsdocument.Tables[0] != null && dsdocument.Tables[0].Rows.Count > 0)
                         {
                             lvDocumentList.Visible = true;
                             lvDocumentList.DataSource = dsdocument.Tables[0];
                             lvDocumentList.DataBind();
                         }
                         else
                         {
                             lvDocumentList.Visible = false;
                             lvDocumentList.DataSource = null;
                             lvDocumentList.DataBind();
                         }

                         string Doc_list = ds.Tables[0].Rows[0]["DOCUMENT_LIST"].ToString();
                         string[] words = Doc_list.Split('$');
                         foreach (string word in words)
                         {
                             foreach (ListViewDataItem lvItem in lvDocumentList.Items)
                             {
                                 CheckBox chkBox = lvItem.FindControl("chkSelect") as CheckBox;

                                 if (chkBox.ToolTip.Trim() == word)
                                 {
                                     chkBox.Enabled = false;
                                     chkBox.Checked = true;
                                     chkBox.BackColor = System.Drawing.Color.SeaGreen;
                                 }
                             }
                         }

                         //if (ds.Tables[0].Rows[0]["DOCUMENT_STATUS"].ToString() == "1")
                         //{
                         //    lblNote.Text = "Applicant's Document Verification Done Sucessfully";
                         //    lblNote.ForeColor = System.Drawing.Color.Green;
                         //}
                         //else
                         //{
                         //    lblNote.Text = "Documents Are Not Verify Yet.";
                         //    lblNote.ForeColor = System.Drawing.Color.Red;
                         //}
                     }
                     else
                     {
                         //objCommon.DisplayMessage("Record Not Found", this.Page);
                         objCommon.DisplayMessage("Either Application ID Not found or Application Fees not paid.", this.Page);

                     }
                 }
                 else
                 {
                     objCommon.DisplayMessage("Either Application ID Not found or Application not confirm. ", this.Page);
                     txtAppID.Text = string.Empty;
                     txtAppID.Focus();
                 }
                #endregion
                   
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Correct Application ID.", Page);
                txtAppID.Text=string.Empty;
                txtAppID.Focus();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status;
            int COUNTONE = 0;
            int COUNTTWO = 0;
            string str = string.Empty;
            string documentnos = string.Empty;
           // string document_status = string.Empty;
            foreach (ListViewDataItem lvItem in lvDocumentList.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkSelect") as CheckBox;

                if (chkBox.Checked == true)
                {
                    documentnos += chkBox.ToolTip + "$";
                   // document_status += 1 + "$";
                }

                if (chkBox.Enabled == false)
                {
                    COUNTTWO++;
                }
            }

            if (lvDocumentList.Items.Count == COUNTTWO)
            {
                objCommon.DisplayMessage("All documents are already verified for Applicant ID:  " + txtAppID.Text + "", this.Page);
            }

            //else if (string.IsNullOrEmpty(documentnos))
            //    objCommon.DisplayMessage("Please Select Document to verify.", this.Page);
            else
            {
                //status = objSC.InsertJoiningDetails(1, Convert.ToInt32(ViewState["newuser"]), Convert.ToInt32(ddlSession.SelectedValue), documentnos);
                status = objSC.InsertJoiningDetails(Convert.ToInt32(ViewState["newuser"]), documentnos,string.Empty,string.Empty,string.Empty);
              
                if (status == 1)
                {
                    objCommon.DisplayMessage("Applicants Document Verification done successfully", this.Page);
                    btnShow_Click(sender, e);
                }
                else
                {
                    objCommon.DisplayMessage("Error in submitting the data", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPdfreport_Click(object sender, EventArgs e)
    {
        //this.ShowReport("Document_Verified_Applicant", "rptDocverifiedapplicant.rpt", 1);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName, int report_type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (report_type == 1)
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue);
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=0";
            else
              //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + Convert.ToInt32(ViewState["newuser"]) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue);
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + Convert.ToInt32(ViewState["newuser"]) + ",@P_ADMBATCH=0";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSingleReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("Document_Verified_Report", "rptStudentdocReport.rpt", 2);
    }
}

