//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : NO DUES FEES COLLECTION                  
// CREATION DATE : 20 APRIL 2013
// ADDED BY      : YAKIN UTANE                                       
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ACADEMIC_NoDueFeeCollection : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    int semesterno;
    int id;
    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    btnSubmit.Enabled = false;
                    // Fill Dropdown lists                

                    this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_NoDueFeeCollection.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NoDueFeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NoDueFeeCollection.aspx");
        }
    }
    #endregion
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["usertype"].ToString()) == 6)
        {
            this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
        }
        else
        {
            // this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " and DEPTNO =" + Convert.ToInt32(Session["userdeptno"].ToString()), "B.BRANCHNO");
            this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedValue == "1")
        //{
        //    semesterno = 8;
        //}
        //else if ( ddlDegree.SelectedValue == "4")
        //{
        //    semesterno = 6;
        //}
        //else if (ddlDegree.SelectedValue == "3")
        //{
        //    semesterno = 10;
        //}
        //else if (ddlDegree.SelectedValue == "2" || ddlDegree.SelectedValue == "5")
        //{
        //    semesterno = 4;
        //}
        divSelectedStudents.Visible = true;
        btnSubmit.Enabled = true;
        // DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND REGNO IS NOT NULL AND SEMESTERNO="+ semesterno , "REGNO");
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + ddlBranch.SelectedValue, string.Empty);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds;
            lvStudents.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (RepeaterItem dataitem in lvStudents.Items)
        {
            Label lblid = dataitem.FindControl("lblRegno") as Label;
            HiddenField hidreg = dataitem.FindControl("hidRegno") as HiddenField;
            HiddenField hidname = dataitem.FindControl("hidName") as HiddenField;
            TextBox txtduefee = dataitem.FindControl("txtDue") as TextBox;
            DropDownList ddlStatus = dataitem.FindControl("ddlStatus") as DropDownList;
            int a5 = ddlStatus.SelectedIndex;
            id = Convert.ToInt32(lblid.ToolTip);
            string regno = hidreg.Value;
            string name = hidname.Value.ToString();
            int session = Convert.ToInt32(Session["currentsession"].ToString());
            int uatype = Convert.ToInt32(Session["usertype"].ToString());
            int uano = Convert.ToInt32(Session["userno"].ToString());
            string ua_name = Session["userfullname"].ToString();
            int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "idno=" + id));

            string receiptno = this.GetNewReceiptNo();
            FeeDemand dcr = this.GetDcrCriteria();

            if (txtduefee.Text != "")
            {
                int a = feeController.AddNoDueFeeAmount(regno, Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), id, name, session, Convert.ToDecimal(txtduefee.Text), uatype, uano, ua_name, Convert.ToInt32(ddlStatus.SelectedValue), semesterno, receiptno);
                i++;
            }
        }
        if (i > 0)
        {
            objCommon.DisplayMessage("Due Amount Saved Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage("Fill Proper Information !!", this.Page);
        }
    }

    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "DF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteria()
    {
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "idno=" + id));
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "NDF";
            dcrCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            dcrCriteria.SemesterNo = semesterno;
            dcrCriteria.PaymentTypeNo = 1;
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDcrCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    protected void lvStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //decimal b =0;
        //    Label lblreg =e.Item.FindControl("lblRegno") as Label;
        //    HiddenField hid = e.Item.FindControl("hidid") as HiddenField;

        //    TextBox ddldue = e.Item.FindControl("txtDue") as TextBox;
        //    DropDownList ddlstatus = e.Item.FindControl("ddlStatus") as DropDownList;
        //    string uaname = Session["userfullname"].ToString();
        //    string reciept = string.Empty;
        //    if (uaname == "Library")
        //    {
        //       reciept = "LB";
        //    }
        //   else if (uaname == "N.S.S/N.C.C")
        //    {
        //       reciept = "NS";
        //    }
        //   else if (uaname == "Games")
        //    {
        //       reciept = "GM";
        //    }
        //   else if (uaname == "SC/ST DEPT.")
        //    {
        //       reciept = "SC";
        //    }
        //  else if (uaname == "Student_Section1")
        //    {
        //       reciept = "SS";
        //    }
        //  else if (uaname == "Scholarship Section")
        //    {
        //        reciept = "SCS";
        //    }
        //  else if (uaname == "T.P.O")
        //    {
        //        reciept = "TP";
        //    }
        //   else if (uaname == "Hostel Account")
        //    {
        //        reciept = "HA";
        //    }
        //    else if (uaname == "Hostel Warden")
        //    {
        //        reciept = "HW";
        //    }
        //    else
        //    {
        //        reciept ="DEP";
        //    }
        //    int id =Convert.ToInt32(lblreg.ToolTip);
        //    DataSet DS =objCommon.FillDropDown("ACD_DCR","TOT_AMT","RECON,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10","SESSIONNO= " +Convert.ToInt32(Session["currentsession"].ToString())+ " AND DEGREENO = "+ Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO = " + ddlBranch.SelectedValue + " AND IDNO=" +id+ " AND RECIEPT_CODE = '" +reciept +"'","IDNO");
        //    if(DS.Tables[0].Rows.Count>0)
        //    {
        //        b =Convert.ToDecimal(DS.Tables[0].Rows[0]["TOT_AMT"]);
        //        ddldue.Text=b.ToString();
        //    }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("NoDueFeeCollection", "rptNoduefee.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            if (ddlDegree.SelectedValue == "1")
            {
                semesterno = 8;
            }
            else if (ddlDegree.SelectedValue == "4")
            {
                semesterno = 6;
            }
            else if (ddlDegree.SelectedValue == "3")
            {
                semesterno = 10;
            }
            else if (ddlDegree.SelectedValue == "2" || ddlDegree.SelectedValue == "5")
            {
                semesterno = 4;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO="+ Convert.ToInt32(Session["currentsession"].ToString())+",@P_BRANCHNO="+Convert.ToInt32(ddlBranch.SelectedValue)+",@P_SEMESTERNO="+ semesterno+",@P_UATYPE="+ Convert.ToInt32(Session["usertype"].ToString()) +",@P_UANAME="+ Session["userfullname"].ToString()+",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"].ToString()) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + semesterno + ",@P_UATYPE=" + Convert.ToInt32(Session["usertype"].ToString()) + ",@P_UANAME=" + Session["userfullname"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_NoDueFeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
