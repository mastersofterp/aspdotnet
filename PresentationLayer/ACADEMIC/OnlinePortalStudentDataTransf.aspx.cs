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
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class ACADEMIC_OnlinePortalStudentDataTransf : System.Web.UI.Page
{

    Common objCommon = new Common();
    applicationReceivedController objnuc = new applicationReceivedController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
               // pnlDetails.Visible = false;
            }
            ddlAdmbatchPHD.Enabled = false;
            ddlBranchPhD.Enabled = false;
            ddlDegreePhD.Enabled = false;
            ddlSchoolPhD.Enabled = false;
            btnPrintPhD.Visible = false;

           // divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //private void BindPaymentType()
    //{
    //    objCommon.FillDropDownList(ddlpaymenttype, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO ASC");
    //}

    //private void BindDegree()
    //{
    //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_USER_BRANCH_PREF CB ON D.DEGREENO=CB.DEGREENO", "DISTINCT D.DEGREENO", " D.DEGREENAME", " CB.USERNO=" + ViewState["USERNO"], " DEGREENAME");
    //}

    //private void BindBranch()
    //{
    //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_USER_BRANCH_PREF CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "  CB.BRANCHNO <> 0 AND CB.USERNO=" + ViewState["USERNO"], " LONGNAME");

    //}

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ViewState["USERNO"] = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtApplicationID.Text.Trim() + "'");
    //        if (String.IsNullOrEmpty(ViewState["USERNO"].ToString()))
    //        {
    //            objCommon.DisplayMessage("Please Enter Correct Application ID.", this.Page);
    //            return;
    //        }


    //        //New changes on Provisional Admission form..Candidates will admitted without having allotmenmt status true for degree B-tech,B-tec+M-tech,B-tech+Mba and other degrees[19-July-2016].

    //        string UserNocheck = objCommon.LookUp("ACD_USER_PROFILE_STATUS US INNER JOIN ACD_USER_REGISTRATION R ON (US.USERNO = R.USERNO) INNER JOIN DOCUMENTENTRY_FILE NR ON (US.USERNO=NR.USERNO) INNER JOIN ACD_DCR_ONLINE F ON (F.IDNO=R.USERNO) LEFT JOIN ACD_BRANCH BR ON (BR.BRANCHNO=R.BRANCHNO)", "DISTINCT CONFIRM_STATUS", "R.USERNAME ='" + txtApplicationID.Text.Trim() + "' AND CONFIRM_STATUS=1 AND RECON=1 AND RECIEPT_CODE = 'PA' AND DOCUMENT_STATUS=1");
    //        if (String.IsNullOrEmpty(UserNocheck))
    //        {
    //            objCommon.DisplayMessage("Applicant Documents Are Not Verify Yet. Please Verify Documents First.", this.Page);
    //        }
    //        else
    //        {
    //            int IsExistsUserNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "ISNULL(CAN,0)=0 AND USERNO=" + Convert.ToInt32(ViewState["USERNO"].ToString())));

    //            if (IsExistsUserNo == 1)
    //            {
    //                BindBranch();
    //                BindDegree();
    //                lblStatus.Visible = true;
    //                lblStatus.Text = "Provisional Admission Is Done For Application ID " + txtApplicationID.Text.Trim() + "";
    //                lblStatus.ForeColor = System.Drawing.Color.Green;
    //                btnSubmit.Enabled = false;
    //                DataSet ds1 = objCommon.FillDropDown("ACD_USER_REGISTRATION R INNER JOIN ACD_STUDENT S ON (S.IDNO = R.USERNO)LEFT JOIN ACD_STUD_PHOTO_ONLINE_ADM UP ON (UP.IDNO = R.USERNO) LEFT JOIN ACD_STU_ADDRESS_ONLINE_ADM UA ON (UA.IDNO = R.USERNO)", "R.USERNO,S.STUDNAME AS STUDENTNAME", "DBO.FN_DESC('DEGREENAME',ISNULL(S.DEGREENO,0))DEGREE,DBO.FN_DESC('BRANCHLNAME',ISNULL(S.BRANCHNO,0))BRANCH,S.DEGREENO ,S.BRANCHNO,S.STUDENTMOBILE,S.EMAILID,UP.PHOTO, CONVERT(NVARCHAR,S.DOB,103)AS DOB, UA.LADDRESS,UA.LPINCODE", "R.USERNO=" + ViewState["USERNO"].ToString(), "USERNO");
    //                if (ds1.Tables[0].Rows.Count > 0)
    //                {
    //                    lblStudentname.Text = ds1.Tables[0].Rows[0]["STUDENTNAME"].ToString();
    //                    lblDateOfBirth.Text = ds1.Tables[0].Rows[0]["DOB"].ToString();
    //                    lblAddress.Text = ds1.Tables[0].Rows[0]["LADDRESS"].ToString();
    //                    lblEmail.Text = ds1.Tables[0].Rows[0]["EMAILID"].ToString();
    //                    lblMobile.Text = ds1.Tables[0].Rows[0]["MOBILENO"].ToString();
    //                    lblPinCode.Text = ds1.Tables[0].Rows[0]["LPINCODE"].ToString();

    //                    ddlDegree.SelectedValue = ds1.Tables[0].Rows[0]["DEGREENO"].ToString();
    //                    ddlBranch.SelectedValue = ds1.Tables[0].Rows[0]["BRANCHNO"].ToString();
    //                    ddlDegree.Enabled = false;
    //                    ddlBranch.Enabled = false;

    //                    if (ds1.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
    //                    {
    //                        imgPhoto.ImageUrl = "../showimage.aspx?id=" + ViewState["USERNO"].ToString() + "&type=ADMISSION";
    //                    }
    //                    else
    //                    {
    //                        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
    //                    }
    //                    pnlDetails.Visible = true;
    //                    string payno = Convert.ToString(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON(S.PTYPE=P.PAYTYPENO)", "PAYTYPENAME", "ISNULL(CAN,0)=0 AND USERNO=" + Convert.ToInt32(ViewState["USERNO"].ToString())));
    //                    if (payno != string.Empty)
    //                    {
    //                        BindPaymentType();
    //                        ddlpaymenttype.SelectedItem.Text = payno;
    //                    }
    //                    else
    //                    {
    //                        BindPaymentType();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                BindBranch();
    //                BindDegree();
    //                lblStatus.Visible = true;
    //                lblStatus.Text = "Provisional Admission Is Not Done For Application ID " + txtApplicationID.Text.Trim() + "";
    //                lblStatus.ForeColor = System.Drawing.Color.Red;
    //                int ProvCount = Convert.ToInt32(objCommon.LookUp("ACD_DCR_ONLINE", "COUNT(IDNO)", "RECIEPT_CODE = 'PA' AND IDNO =" + +Convert.ToInt32(ViewState["USERNO"].ToString())));
    //                if (ProvCount == 1)
    //                {
    //                    DataSet ds = objnuc.GetExstStudentDetailsByApplicationID(txtApplicationID.Text.Trim());
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        lblStudentname.Text = ds.Tables[0].Rows[0]["STUDENTNAME"].ToString();
    //                        lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
    //                        lblAddress.Text = ds.Tables[0].Rows[0]["LADDRESS"].ToString();
    //                        lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
    //                        lblMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
    //                        lblPinCode.Text = ds.Tables[0].Rows[0]["LPINCODE"].ToString();

    //                        ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
    //                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
    //                        ddlDegree.Enabled = false;
    //                        ddlBranch.Enabled = false;

    //                        if (ds.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
    //                        {
    //                            imgPhoto.ImageUrl = "../showimage.aspx?id=" + ViewState["USERNO"].ToString() + "&type=ADMISSION";
    //                        }
    //                        else
    //                        {
    //                            imgPhoto.ImageUrl = "~/images/nophoto.jpg";
    //                        }
    //                        pnlDetails.Visible = true;
    //                        string payno = Convert.ToString(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON(S.PTYPE=P.PAYTYPENO)", "PAYTYPENAME", "ISNULL(CAN,0)=0 AND USERNO=" + Convert.ToInt32(ViewState["USERNO"].ToString())));
    //                        if (payno != string.Empty)
    //                        {
    //                            BindPaymentType();
    //                            ddlpaymenttype.SelectedItem.Text = payno;
    //                        }
    //                        else
    //                        {
    //                            BindPaymentType();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION R INNER JOIN ACD_STUDENT_ONLINE_ADM S ON (S.IDNO = R.USERNO) LEFT JOIN ACD_STUD_PHOTO_ONLINE_ADM UP ON (UP.IDNO = R.USERNO) LEFT JOIN ACD_STU_ADDRESS_ONLINE_ADM UA ON (UA.IDNO = R.USERNO)", "R.USERNO,R.FIRSTNAME AS STUDENTNAME,R.ADM_TYPE", "R.MOBILENO,R.EMAILID,UP.PHOTO, CONVERT(NVARCHAR,S.DOB,103)AS DOB,UA.LADDRESS,UA.LPINCODE", "R.USERNO=" + ViewState["USERNO"].ToString(), "USERNO");
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        lblStudentname.Text = ds.Tables[0].Rows[0]["STUDENTNAME"].ToString();
    //                        lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
    //                        lblAddress.Text = ds.Tables[0].Rows[0]["LADDRESS"].ToString();
    //                        lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
    //                        lblMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
    //                        lblPinCode.Text = ds.Tables[0].Rows[0]["LPINCODE"].ToString();

    //                        if (ds.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
    //                        {
    //                            imgPhoto.ImageUrl = "../showimage.aspx?id=" + ViewState["USERNO"].ToString() + "&type=ADMISSION";
    //                        }
    //                        else
    //                        {
    //                            imgPhoto.ImageUrl = "~/images/nophoto.jpg";
    //                        }
    //                        pnlDetails.Visible = true;
    //                        string payno = Convert.ToString(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON(S.PTYPE=P.PAYTYPENO)", "PAYTYPENAME", "ISNULL(CAN,0)=0 AND USERNO=" + Convert.ToInt32(ViewState["USERNO"].ToString())));
    //                        if (payno != string.Empty)
    //                        {
    //                            BindPaymentType();
    //                            ddlpaymenttype.SelectedItem.Text = payno;
    //                        }
    //                        else
    //                        {
    //                            BindPaymentType();
    //                        }

    //                        if (ds.Tables[0].Rows[0]["ADM_TYPE"].ToString() == "1")
    //                        {
    //                            objCommon.FillDropDownList(ddlBranch, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_BRANCH B ON (BP.BRANCHNO = B.BRANCHNO)", "DISTINCT BP.BRANCHNO", "B.LONGNAME AS BRANCHNAME", "BP.USERNO =" + Convert.ToInt32(ViewState["USERNO"]) + "AND BP.ADM_TYPE = 1", "BRANCHNAME");
    //                            objCommon.FillDropDownList(ddlDegree, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_DEGREE D ON (BP.DEGREENO = D.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME AS DEGREENAME", "BP.USERNO =" + Convert.ToInt32(ViewState["USERNO"]) + "AND BP.ADM_TYPE = 1", "BRANCHNAME");

    //                        }
    //                        else
    //                        {
    //                            objCommon.FillDropDownList(ddlBranch, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_BRANCH B ON (BP.BRANCHNO = B.BRANCHNO)", "DISTINCT BP.BRANCHNO", "B.LONGNAME AS BRANCHNAME", "BP.USERNO =" + Convert.ToInt32(ViewState["USERNO"]), "BRANCHNAME");
    //                            objCommon.FillDropDownList(ddlDegree, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_DEGREE D ON (BP.DEGREENO = D.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME AS DEGREENAME", "BP.USERNO =" + Convert.ToInt32(ViewState["USERNO"]), "BRANCHNAME");

    //                        }
    //                    }
    //                }
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //private void ValidateExistingRecord()
    //{
    //    int IsExistsUserNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "ISNULL(CAN,0)=0 AND USERNO=" + Convert.ToInt32(ViewState["USERNO"].ToString())));

    //    if (IsExistsUserNo == 1)
    //    {
    //        lblStatus.Visible = true;
    //        lblStatus.Text = "Provisional Admission Is Done For Application ID " + txtApplicationID.Text.Trim() + "";
    //        lblStatus.ForeColor = System.Drawing.Color.Green;
    //        btnSubmit.Enabled = false;
    //    }
    //    else
    //    {
    //        lblStatus.Visible = true;
    //        lblStatus.Text = "Provisional Admission Is Not Done For Application ID " + txtApplicationID.Text.Trim() + "";
    //        lblStatus.ForeColor = System.Drawing.Color.Red;
    //    }
    //}

    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ViewState["CHKDEGREE"].ToString()) > 0)
    //    {
    //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT ISNULL(B.BRANCHNO,0)BRANCHNO", "B.LONGNAME", "CB.BRANCHNO>0 AND CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.ToString()), "LONGNAME");
    //    }
    //    else
    //    {
    //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_USER_BRANCH_PREF CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT ISNULL(B.BRANCHNO,0)BRANCHNO", " B.LONGNAME", "CB.BRANCHNO>0 AND CB.USERNO=" + ViewState["USERNO"].ToString() + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.ToString()), "LONGNAME");
    //    }
    //}

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.ToString());
    //}

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlDegree.SelectedIndex > 0 || ddlDegree.SelectedValue != "0")
    //        {
    //            if (ddlBranch.SelectedIndex > 0 || ddlBranch.SelectedValue != "0")
    //            {
    //                if (ddlpaymenttype.SelectedIndex > 0 || ddlpaymenttype.SelectedValue != "0")
    //                {
    //                    string transfer = objCommon.LookUp("ACD_USER_REGISTRATION", "DATA_TRANSFERED", "USERNO = " + ViewState["USERNO"].ToString() + "");
    //                    if (String.IsNullOrEmpty(transfer))
    //                    {
    //                        int userno = Convert.ToInt32(ViewState["USERNO"]);
    //                        int PTYPE = Convert.ToInt32(ddlpaymenttype.SelectedValue);
    //                        CustomStatus cs = new CustomStatus();

    //                        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0 && Convert.ToInt32(ddlBranch.SelectedValue) > 0)
    //                        {
    //                            int DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
    //                            int BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
    //                            cs = (CustomStatus)objnuc.TransferStudentDataToMain(userno, PTYPE, DEGREENO, BRANCHNO);
    //                            if (cs.Equals(CustomStatus.RecordSaved))
    //                            {
    //                                ValidateExistingRecord();
    //                                objCommon.DisplayMessage("Data Saved Successfully.", this.Page);
    //                            }
    //                            else
    //                            {
    //                                objCommon.DisplayMessage("Failed To Save Data.", this.Page);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            objCommon.DisplayMessage("Please select Degree, Branch .", this.Page);

    //                        }
    //                    }
    //                    else
    //                    {
    //                        objCommon.DisplayMessage("Student Already Provisionally Admitted.", this.Page);
    //                    }
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage("Please Select Payment Type.", this.Page);
    //                    ddlpaymenttype.Focus();
    //                }
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage("Please Select Branch.", this.Page);
    //                ddlBranch.Focus();
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Please Select Degree.", this.Page);
    //            ddlDegree.Focus();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    protected void btnShowPhD_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet dsGetPhDStud = null;
            byte[] imgData = null;
            ddlDegreePhD.Items.Clear();
            ddlDegreePhD.Items.Add(new ListItem("Please Select", "0"));
            dsGetPhDStud = objnuc.GetPhDStudentInfo_AdmConfirm(txtAppIdPhD.Text.Trim());
            if (dsGetPhDStud.Tables[0].Rows.Count > 0)
            {
                pnlPhD.Visible = true;
                lblStudNamePhD.Text = dsGetPhDStud.Tables[0].Rows[0]["STUDENTNAME"].ToString();
                lblDOBPhD.Text = dsGetPhDStud.Tables[0].Rows[0]["DOB"].ToString();
                lblAddPhD.Text = dsGetPhDStud.Tables[0].Rows[0]["ADDRESS"].ToString();
                lblPinPhD.Text = dsGetPhDStud.Tables[0].Rows[0]["PINCODE"].ToString();
                lblMobPhD.Text = dsGetPhDStud.Tables[0].Rows[0]["MOBILENO"].ToString();
                lblEmailPhD.Text = dsGetPhDStud.Tables[0].Rows[0]["EMAILID"].ToString();
                ViewState["USERNO_PHD"] = dsGetPhDStud.Tables[0].Rows[0]["USERNO"].ToString();
                if (dsGetPhDStud.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
                {
                    imgData = dsGetPhDStud.Tables[0].Rows[0]["PHOTO"] as byte[];
                    string base64String = Convert.ToBase64String(imgData);
                    imgPhotoPhD.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    imgPhotoPhD.ImageUrl = "~/images/nophoto.jpg";
                }

                DataSet dsAmount = objnuc.GetAmountDetails_ByUserno_ForPhD(Convert.ToInt32(ViewState["USERNO_PHD"]));
                if (dsAmount.Tables[0].Rows.Count > 0)
                {
                    lvFees.DataSource = dsAmount;
                    lvFees.DataBind();
                    lvFees.Visible = true;
                }
                else
                {
                    lvFees.DataSource = null;
                    lvFees.DataBind();
                    lvFees.Visible = false;
                }

                if (dsGetPhDStud.Tables[0].Rows[0]["STUDENT_EXISTS"].ToString().Equals("1"))
                {
                    btnSubmitPhD.Visible = false;
                    btnPrintPhD.Visible = true;
                    ddlPayTypePhD.Enabled = true;
                    objCommon.FillDropDownList(ddlAdmbatchPHD, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
                    objCommon.FillDropDownList(ddlSchoolPhD, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
                    ddlSchoolPhD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    ddlAdmbatchPHD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    if (dsGetPhDStud.Tables[1].Rows.Count > 0)
                    {
                        ddlDegreePhD.DataSource = dsGetPhDStud.Tables[1];
                        ddlDegreePhD.DataTextField = "DEGREENAME";
                        ddlDegreePhD.DataValueField = "DEGREENO";
                        ddlDegreePhD.DataBind();
                    }
                    ddlDegreePhD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["DEGREENO"].ToString();
                    objCommon.FillDropDownList(ddlBranchPhD, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO =" + Convert.ToInt32(ddlDegreePhD.SelectedValue), "B.LONGNAME");
                    ddlBranchPhD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    objCommon.FillDropDownList(ddlPayTypePhD, "ACD_PAYMENTTYPE P inner join acd_standard_fees S ON (S.PAYTYPENO=P.PAYTYPENO)", "DISTINCT S.PAYTYPENO", "PAYTYPENAME", "P.PAYTYPENO>0 and S.DEGREENO=" + dsGetPhDStud.Tables[0].Rows[0]["DEGREENO"].ToString() + "and S.BRANCHNO=" + dsGetPhDStud.Tables[0].Rows[0]["BRANCHNO"].ToString() + "and S.COLLEGE_ID=" + dsGetPhDStud.Tables[0].Rows[0]["COLLEGE_ID"].ToString() + "and S.batchno=" + dsGetPhDStud.Tables[0].Rows[0]["ADMBATCH"].ToString(), "");
                    ddlPayTypePhD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["PTYPE"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Provisional admission is already done.`)", true);
                    return;
                }
                else
                {
                    btnSubmitPhD.Visible = true;
                    btnPrintPhD.Visible = false;
                    ddlPayTypePhD.Enabled = true;
                    if (dsGetPhDStud.Tables[1].Rows.Count > 0)
                    {
                        ddlDegreePhD.DataSource = dsGetPhDStud.Tables[1];
                        ddlDegreePhD.DataTextField = "DEGREENAME";
                        ddlDegreePhD.DataValueField = "DEGREENO";
                        ddlDegreePhD.DataBind();
                        ddlDegreePhD.SelectedValue = dsGetPhDStud.Tables[1].Rows[0]["DEGREENO"].ToString();
                        objCommon.FillDropDownList(ddlAdmbatchPHD, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
                        objCommon.FillDropDownList(ddlSchoolPhD, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
                        ddlSchoolPhD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["COLLEGE_ID_APPLICATION"].ToString();
                        ddlAdmbatchPHD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["ADMBATCH_APPLICATION"].ToString();
                        objCommon.FillDropDownList(ddlBranchPhD, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO =" + Convert.ToInt32(dsGetPhDStud.Tables[1].Rows[0]["DEGREENO"].ToString()), "B.LONGNAME");
                        ddlBranchPhD.SelectedValue = dsGetPhDStud.Tables[0].Rows[0]["BRANCHNO_APPLICATION"].ToString();
                        objCommon.FillDropDownList(ddlPayTypePhD, "ACD_PAYMENTTYPE P inner join acd_standard_fees S ON (S.PAYTYPENO=P.PAYTYPENO)", "DISTINCT S.PAYTYPENO", "PAYTYPENAME", "P.PAYTYPENO>0 AND S.DEGREENO=" + dsGetPhDStud.Tables[1].Rows[0]["DEGREENO"].ToString() + "AND S.BRANCHNO=" + dsGetPhDStud.Tables[0].Rows[0]["BRANCHNO_APPLICATION"].ToString() + "AND S.COLLEGE_ID=" + dsGetPhDStud.Tables[0].Rows[0]["COLLEGE_ID_APPLICATION"].ToString() + "AND S.batchno=" + dsGetPhDStud.Tables[0].Rows[0]["ADMBATCH_APPLICATION"].ToString(), "");
                    }
                }
            }
            else
            {
                pnlPhD.Visible = false;
                dsGetPhDStud = null;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`No Student Found`)", true);
                return;                
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlAdmbatchPHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatchPHD.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSchoolPhD, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSchoolPhD_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ddlDegreePhD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegreePhD.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranchPhD, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO =" + Convert.ToInt32(ddlDegreePhD.SelectedValue), "B.LONGNAME");

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitPhD_Click(object sender, EventArgs e)
    {
        try
        {
            int userno = Convert.ToInt32(ViewState["USERNO_PHD"].ToString());
            int UANO = int.Parse(Session["userno"].ToString());
            int College_Id = Convert.ToInt32(ddlSchoolPhD.SelectedValue);
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "COLLEGE_ID=" + College_Id));
            int count = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "count(STD_FEE_NO)", "RECIEPT_CODE = 'TF'  and DEGREENO = " + Convert.ToInt32(ddlDegreePhD.SelectedValue) + " and BRANCHNO= " + Convert.ToInt32(ddlBranchPhD.SelectedValue) + "and PAYTYPENO=" + Convert.ToInt32(ddlPayTypePhD.SelectedValue) + "AND BATCHNO=" + Convert.ToInt32(ddlAdmbatchPHD.SelectedValue) + "AND COLLEGE_ID=" + College_Id));
            if (count < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`First define the standard fees for selected criteria!`)", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objnuc.TransferStudentDataToMain_PhD(userno, Convert.ToInt32(ddlPayTypePhD.SelectedValue), Convert.ToInt32(ddlDegreePhD.SelectedValue),
                Convert.ToInt32(ddlBranchPhD.SelectedValue), UANO);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Admission done successfully.`)", true);
                    ddlAdmbatchPHD.Enabled = false;
                    ddlBranchPhD.Enabled = false;
                    ddlDegreePhD.Enabled = false;
                    ddlSchoolPhD.Enabled = false;
                    ddlPayTypePhD.Enabled = false;
                    //btnPrintPhD.Visible = true;
                    btnSubmitPhD.Visible = false;
                    btnPrintPhD.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Admission not done successfully.`)", true);
                    ddlAdmbatchPHD.Enabled = false;
                    ddlBranchPhD.Enabled = false;
                    ddlDegreePhD.Enabled = false;
                    ddlSchoolPhD.Enabled = false;
                    //btnPrintPhD.Visible = true;
                    btnSubmitPhD.Visible = true;
                    btnPrintPhD.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancelPhD_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPhD.Visible = false;
            txtAppIdPhD.Text = string.Empty;
            lvFees.DataSource = null;
            lvFees.DataBind();
            ddlDegreePhD.Items.Clear();
            ddlBranchPhD.Items.Clear();
            ddlAdmbatchPHD.Items.Clear();
            ddlSchoolPhD.Items.Clear();
            ddlPayTypePhD.Items.Clear();
            ddlDegreePhD.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmbatchPHD.Items.Add(new ListItem("Please Select", "0"));
            ddlSchoolPhD.Items.Add(new ListItem("Please Select", "0"));
            ddlBranchPhD.Items.Add(new ListItem("Please Select", "0"));
            ddlPayTypePhD.Items.Add(new ListItem("Please Select", "0"));
            btnSubmitPhD.Visible = true;
            btnPrintPhD.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnPrintPhD_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Provisional Admission", "rptProvisionalAdmissionCard.rpt");
            btnPrintPhD.Visible = true; 
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + txtAppIdPhD.Text.ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePortalStudentDataTransf.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}