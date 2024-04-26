using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_EXAMINATION_ProvisionalCertificate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    ProvisionalCertificateEntity objPCEntity = new ProvisionalCertificateEntity();
    ProvisionalCertificateController objPCController = new ProvisionalCertificateController();

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (Session["usertype"].ToString().Equals("2")) 
                {
                    divDetails.Visible = true;
                    divRegistrationNo.Visible = false;
                    StudentDetail();
                }
                else
                {
                    divDetails.Visible = true;
                    divRegistrationNo.Visible = true;
                }
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        StudentDetail();
    }

    private void StudentDetail()
    {
        try
        {
            DataSet ds = null;
            if (Session["usertype"].ToString().Equals("2")) //Student 
            {
                //ds = objCommon.FillDropDown("ACD_TRRESULT TRRESULT WITH (NOLOCK) INNER JOIN ACD_STUDENT STUDENT WITH (NOLOCK) ON(STUDENT.IDNO=TRRESULT.IDNO)", "TRRESULT.REGNO,TRRESULT.STUDNAME,STUDENT.STUDENTMOBILE,STUDENT.EMAILID", "DGPA,TRRESULT.SESSIONNO", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"]) + ") AND ISNULL(DGPA,0)>0 AND TRRESULT.IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
                string SP_Name = "PKG_ACD_DEGREE_COMPLETE_STUDENT"; // added by shubham on 27/11/2023
                string SP_Parameters = "@P_IDNO";
                string Call_Values = "" + Convert.ToInt32(Session["idno"]) + "";
                ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            }
            else
            {

                Session["idno"] = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "REGNO='" + txtRegistrationNo.Text + "'");
                ds = objCommon.FillDropDown("ACD_TRRESULT TRRESULT WITH (NOLOCK) INNER JOIN ACD_STUDENT STUDENT WITH (NOLOCK) ON(STUDENT.IDNO=TRRESULT.IDNO)", "TRRESULT.REGNO,TRRESULT.STUDNAME,STUDENT.STUDENTMOBILE,STUDENT.EMAILID", "TRRESULT.DGPA,TRRESULT.SESSIONNO", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"]) + ") AND ISNULL(DGPA,0)>0 AND TRRESULT.IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");

                ViewState["idno"] = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "REGNO='" + txtRegistrationNo.Text + "'");
                String idno = ViewState["idno"].ToString();
                if (idno == "")
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Enter Valid !!!.", this.Page);
                    return;
                }
                //ds = objCommon.FillDropDown("ACD_TRRESULT TRRESULT WITH (NOLOCK) INNER JOIN ACD_STUDENT STUDENT WITH (NOLOCK) ON(STUDENT.IDNO=TRRESULT.IDNO)", "TRRESULT.REGNO,TRRESULT.STUDNAME,STUDENT.STUDENTMOBILE,STUDENT.EMAILID", "TRRESULT.DGPA,TRRESULT.SESSIONNO", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"]) + ") AND ISNULL(DGPA,0)>0 AND TRRESULT.IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
                //ds = objCommon.FillDropDown("ACD_TRRESULT TRRESULT WITH (NOLOCK) INNER JOIN ACD_STUDENT STUDENT WITH (NOLOCK) ON(STUDENT.IDNO=TRRESULT.IDNO)", "TRRESULT.REGNO,TRRESULT.STUDNAME,STUDENT.STUDENTMOBILE,STUDENT.EMAILID", "TRRESULT.DGPA,TRRESULT.SESSIONNO", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"]) + ") AND TRRESULT.IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
                string SP_Name = "PKG_ACD_DEGREE_COMPLETE_STUDENT"; // added by shubham on 27/11/2023
                string SP_Parameters = "@P_IDNO";
                string Call_Values = "" + Convert.ToInt32(idno) + "";
                ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            }

            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                if (CheckDueStatus(Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"])) == true && CheckPublishStatus(Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"])) == true)
                {
                    divStudDetails.Visible = true;
                    hdfSessionno.Value = ds.Tables[0].Rows[0]["SESSIONNO"] !=null ? Convert.ToString(ds.Tables[0].Rows[0]["SESSIONNO"]):"0";
                    lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"] != null ?  Convert.ToString(ds.Tables[0].Rows[0]["STUDNAME"]) : string.Empty;
                    lblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"] != null ?  Convert.ToString(ds.Tables[0].Rows[0]["REGNO"]) : string.Empty;
                    lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"] != null ?  Convert.ToString(ds.Tables[0].Rows[0]["STUDENTMOBILE"]) : string.Empty;
                    lblEmailId.Text = ds.Tables[0].Rows[0]["EMAILID"] != null ?  Convert.ToString(ds.Tables[0].Rows[0]["EMAILID"]) : string.Empty;
                    lblDGPA.Text = ds.Tables[0].Rows[0]["DGPA"] != null ?  Convert.ToString(ds.Tables[0].Rows[0]["DGPA"]) : string.Empty;
                    btnPrint.Visible = true;
                }
                else
                {
                    divStudDetails.Visible = false;
                    btnPrint.Visible = false;
                    return;
                }
            }
            else
            {
                divStudDetails.Visible = false;
                btnPrint.Visible = false;

                if (Session["usertype"].ToString().Equals("2"))
                {
                    objCommon.DisplayUserMessage(this.Page, "Registration No. " + txtRegistrationNo.Text + " is not Eligible to View the Provisional Certificate Detail Due to Not Clear All Semester.", this.Page);
                    Response.Redirect("~/notauthorized.aspx?page=ProvisionalCertificate.aspx");
                }
                else
                {
                    //objCommon.DisplayUserMessage(this.Page, "Registration No. " + txtRegistrationNo.Text + " is not Eligible to View the Provisionl Certificate Detail Due to DGPA is not Available.", this.Page);
                    //return;
                    objCommon.DisplayUserMessage(this.Page, "Registration No. " + txtRegistrationNo.Text + " is not Eligible to View the Provisional Certificate Detail Due to Not Clear All Semester.", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ProvisionalCertificate.StudentDetail()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool CheckDueStatus(int Sessionno)
    {
        int ExistDueClear = Convert.ToInt32(objCommon.LookUp("ACD_NODUES_STATUS WITH (NOLOCK)", "COUNT(IDNO) AS CNT", "ISNULL(NODUES_STATUS,0)=1 AND IDNO = " + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Sessionno + ""));
        if (ExistDueClear > 0)
        {
            return true;
        }
        else
        {
            objCommon.DisplayUserMessage(this.Page, "Please Contact Finance Office  for fees reconciliation.", this.Page);
            return false;
        }
    }

    private bool CheckPublishStatus(int Sessionno)
    {
        int ExistDueClear = Convert.ToInt32(objCommon.LookUp("RESULT_PUBLISH_DATA WITH (NOLOCK)", "COUNT(IDNO) AS CNT", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Sessionno + ""));
        if (ExistDueClear > 0)
        {
            return true;
        }
        else
        {
            objCommon.DisplayUserMessage(this.Page, "Result Publish not Yet Done, Kindly Contact to your Examination Section of MAKAUT.", this.Page);
            return false;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ProvisionalCertificate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ProvisionalCertificate.aspx");
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() != "2")
                Session["idno"] = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "regno='" + txtRegistrationNo.Text + "'");


            objPCEntity.IDNO = Convert.ToInt64(Session["idno"]);
            objPCEntity.SESSIONNO = Convert.ToInt32(hdfSessionno.Value);
            objPCEntity.CREATE_BY = Convert.ToInt64(Session["userno"]);
            objPCEntity.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

            int Result= Convert.ToInt32(objPCController.Insert_Provisional_Certificate_Log(objPCEntity));
            if (Result == 1)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=StudentResultDetails";
                url += "&path=~,Reports,Academic,rptProvisionalCertificate_New.rpt";

                url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                //sb.Append(@"window.open('" + url + "','','" + features + "');");

                //ScriptManager.RegisterClientScriptBlock(this.updUpdate, this.updUpdate.GetType(), "controlJSScript", sb.ToString(), true);
                ShowReport("ProvisionalCertificate", "rptProvisionalCertificateNew.rpt");

                ScriptManager.RegisterClientScriptBlock(this.updFacAllot, this.updFacAllot.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please try again !!!.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ProvisionalCertificate.btnPrint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //int clg_id = Convert.ToInt32(ViewState["college_id"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"]);
            //url += "&param=@P_IDNO=" + "45.1813";
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updFacAllot, updFacAllot.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


}