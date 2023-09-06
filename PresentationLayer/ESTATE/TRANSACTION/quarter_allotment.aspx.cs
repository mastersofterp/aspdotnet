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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Globalization;


using System.Collections.Generic;
using System.Web.Services;
using System.IO;
using System.Web.Script.Services;
using System.Text;


public partial class ESTATE_Transaction_quarter_allotment : System.Web.UI.Page
{
    Common objcommon = new Common();
    QuarterAllotmentController objAllot = new QuarterAllotmentController();
    QuarterAllotment objAllotEntity = new QuarterAllotment();

    OnlineApp objOApp = new OnlineApp();

    public static int EMPID = 0;
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
                CheckPageAuthorization();
                ViewState["action"] = "add";               
                binddropdownlist();               
                BindApplicantRepeater();
                BindWaitingList();
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    protected void binddropdownlist()
    {
        try
        {           
             objcommon.FillDropDownList(ddlquarterno, "EST_QRT_MST", "IDNO", "QUARTER_NO", "IDNO  not in (SELECT Qrtno_id from EST_ADDMETER where QRT_STATUS is null)", "IDNO");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void binddropdownlistControl()
    {
       // objcommon.FillDropDownList(ddlEmetertype, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(1), "MTYPE_NO");
      //  objcommon.FillDropDownList(ddlmeterno, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.METER_NO", "A.M_ID>0", "A.M_ID");
    }

    //this is used to save  the  consumer information.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {          

            if (ddlquarterno.SelectedIndex < 0)
            {
                objcommon.DisplayMessage(this.updQtr, "Please Select Quarter Name.", this.Page);
                return;
            }            

            objAllotEntity.Name = Convert.ToInt32(ViewState["EMPID"]);
            objAllotEntity.EmployeeType = 1;
            objAllotEntity.Designation = Convert.ToInt32(lblDesig.ToolTip);
            objAllotEntity.Department = Convert.ToInt32(lblDepart.ToolTip);
            objAllotEntity.AllotOrderNo  = txtallotorderno.Text.Trim().ToString();
            objAllotEntity.OffceOrderDt = DateTime.ParseExact(txtorderdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(.Text.Trim());           

             if (txtOccuDate.Text == string.Empty)           
             {
                 objAllotEntity.occuDate = DateTime.MinValue;               
             }
             else
             {
                 objAllotEntity.occuDate = DateTime.ParseExact(txtOccuDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                
             }
            objAllotEntity.QID = Convert.ToInt32(ddlquarterno.SelectedValue);           
            objAllotEntity.QuarterNo = Convert.ToString(lblQrtTyp.ToolTip);
            
            if (Convert.ToDateTime(txtallotmentdate.Text) >= Convert.ToDateTime(txtorderdate.Text))
            {
                objAllotEntity.AllotmentDate = DateTime.ParseExact(txtallotmentdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(txtallotmentdate.Text.Trim());                 
            }
            else
            {
                objcommon.DisplayMessage(this.updQtr, "Allotment date can not be less than office order date.", this.Page);
                return;
            }            
            objAllotEntity.QuarterRent   = Convert.ToDouble(0);

            //if (Convert.ToDateTime(txtOccuDate.Text) >= Convert.ToDateTime(txtallotmentdate.Text))
            //{
            //    objAllotEntity.occuDate = DateTime.Parse(txtOccuDate.Text.Trim());
            //}
            //else
            //{
            //    objcommon.DisplayMessage(this.updQtr, "Occupied date can not be less than Allotment date.", this.Page);
            //    return;
            //}         

            if (ViewState["action"].Equals("add"))
            {
                //if (funDuplicate() == true)
                //{
                //    objcommon.DisplayMessage(updQtr, "Allot Order No. Already Exist.", this.Page);
                //    txtallotorderno.Focus();
                //    return;
                //}
                CustomStatus cs = (CustomStatus)objAllot.AddQuarterAllotment(objAllotEntity);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindApplicantRepeater();         
                  objcommon.DisplayMessage(this.updQtr, "Record Save Successfully.", this.Page);
                  funclear();
                }
                else
                {
                  objcommon.DisplayMessage(this.updQtr, "Sorry! Try agian.", this.Page);
                }
            }
            if (ViewState["action"].Equals("edit"))
            {             
                objAllotEntity.MId = 1;
                //if (funDuplicate() == true)
                //{
                //    objcommon.DisplayMessage(updQtr, "Allot Order No. Already Exist.", this.Page);
                //    txtallotorderno.Focus();
                //    return;
                //}
                CustomStatus cs = (CustomStatus)objAllot.AddQuarterAllotment(objAllotEntity);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindApplicantRepeater();         
                 objcommon.DisplayMessage(this.updQtr, "Record Updated Successfully.", this.Page);
                 funclear();                
                }
                else
                {
                 objcommon.DisplayMessage(this.updQtr,"Sorry! Try agian.", this.Page);
                }
                funclear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT", "*", " ", "QA_ID !=" + Convert.ToInt32(ViewState["QA_ID"]) + " AND ALLT_ORDER_NO='" + txtallotorderno.Text + "'", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 

    protected void GetConsumerAllotData(int EMPID)
    {
        try
        {
            DataSet ds = null;
            binddropdownlistControl();
            ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN  EST_ADDMETER  B  ON(A.QA_ID =B.QA_ID)", "A. EMPTYPE_ID,A.NAME_ID,A.DEPT_ID,A.DESIG_ID,A.ALLT_ORDER_NO,A.OFFCE_ORDER_DT,A.ALLOTMENT_DT,A.QRT_RENT,A.WMETER_STATUS, A.GAS_MID,A.CUSTOMER_ID,A.WATERMETERRENT,B.QRTTYPE_ID ,B.QRTNO_ID, B.EMETER_TYPE_ID, B.METER_NO", "A.QA_ID", "A.NAME_ID=" + EMPID + "AND A.QRT_STATUS IS NULL", "A.QA_ID");
 
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlquarterno.Items.Clear();
                objcommon.FillDropDownList(ddlquarterno, "EST_QRT_MST", "IDNO", "QUARTER_NO", "IDNO >0", "IDNO");
                // ddlquarterno.Enabled = false; 
               // ddldepartment.SelectedValue  = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();
               // ddldesignation.SelectedValue = ds.Tables[0].Rows[0]["DESIG_ID"].ToString();
                txtallotorderno.Text         = ds.Tables[0].Rows[0]["ALLT_ORDER_NO"].ToString();
                txtorderdate.Text            = ds.Tables[0].Rows[0]["OFFCE_ORDER_DT"].ToString();
                txtallotmentdate.Text        = ds.Tables[0].Rows[0]["ALLOTMENT_DT"].ToString();
                txtquarterRent.Text          = ds.Tables[0].Rows[0]["QRT_RENT"].ToString();
                //ddlquartertype.SelectedValue = ds.Tables[0].Rows[0]["QRTTYPE_ID"].ToString();
                ddlquarterno.SelectedValue = ds.Tables[0].Rows[0]["QRTNO_ID"].ToString();
                //ddlEmetertype.SelectedValue = ds.Tables[0].Rows[0]["EMETER_TYPE_ID"].ToString();
                //ddlmeterno.SelectedValue = ds.Tables[0].Rows[0]["METER_NO"].ToString();            
                ViewState["action"] = "edit";
            }
            else
            {
                   objcommon.FillDropDownList(ddlquarterno, "EST_QRT_MST", "IDNO", "QUARTER_NO", "IDNO  not in (SELECT Qrtno_id from EST_ADDMETER)", "IDNO");
                    ddlquarterno.Enabled = true;
                   // ddldepartment.ClearSelection();
                    //ddldesignation.ClearSelection();
                   // ddlcustomerNo.ClearSelection();
                    txtallotorderno.Text       = string.Empty;
                    txtorderdate.Text          = string.Empty;
                    txtallotmentdate.Text      = string.Empty;
                    txtquarterRent.Text        = string.Empty;
                    ViewState["action"] = "add";
                    clearSelection();
                    binddropdownlistControl();
            }    
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }   

    protected void BindApplicantRepeater()
    {
        try
        {
            DataSet ds = null;
            ds = objcommon.FillDropDown("EST_ONLINE_APPLICATION O INNER JOIN PAYROLL_EMPMAS E ON(O.EMPID = E.IDNO) INNER JOIN EST_QRT_TYPE Q ON (O.QRT_TYPE = Q.QNO) LEFT JOIN EST_QRT_ALLOTMENT QA ON (QA.NAME_ID = O.EMPID)", "O.APPID, (CASE WHEN O.TOTAL_MEMBERS = 0 THEN NULL ELSE O.TOTAL_MEMBERS END) as TOTAL_MEMBERS, O.REMARK, O.[STATUS], Q.QUARTER_TYPE, O.EMPID, O.QRT_TYPE AS QTNO", "isnull(E.Title,'')+''+isnull(E.FNAME,'')+''+isnull(E.MNAME,'')+''+isnull(E.LNAME,'') AS NAME, O.APP_DATE, O.STATUS", "QA.QRT_STATUS IS NULL", "O.STATUS");
            if (ds.Tables[0].Rows.Count > 0)
            {  
                RepApplicant.DataSource = ds;
                RepApplicant.DataBind();              
             }
             else
            {
                RepApplicant.DataSource = null;
                RepApplicant.DataBind();                  
            }      
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
      
    protected void funclear()
    {        
        txtallotmentdate.Text = string.Empty;
        txtallotorderno.Text = string.Empty;
        txtquarterRent.Text = string.Empty;
        txtorderdate.Text = string.Empty;
        txtOccuDate.Text = string.Empty;      
        ddlquarterno.ClearSelection();      
       
        ViewState["action"] = "add";
        clearSelection();
        btnSave.Visible = true;
        pnlDetails.Visible = false;
     
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        funclear();
    }
      
    protected void clearSelection()
    {       
        ddlquarterno.Items.Clear();      
    } 
   
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
       
        try
        {
            pnlDetails.Visible = true;
            ViewState["EMPID"] = Convert.ToInt32(btn.CommandArgument);
           // ViewState["action"] = "edit"; 
            ViewState["STATUS"] = Convert.ToInt32(btn.ToolTip);

            ShowDetails(Convert.ToInt32(ViewState["EMPID"]), Convert.ToInt32(ViewState["STATUS"])); 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // this method is used to show the details fetch from database.
    private void ShowDetails(int EMPID, int STATUS)
    {
        try
        {
            DataSet ds = objcommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DEG ON (E.SUBDESIGNO = DEG.SUBDESIGNO) INNER JOIN EST_ONLINE_APPLICATION O ON (E.IDNO = O.EMPID) INNER JOIN EST_QRT_TYPE QT ON (O.QRT_TYPE = QT.QNO) LEFT JOIN EST_QRT_ALLOTMENT QA ON (QA.NAME_ID =E.IDNO) LEFT JOIN EST_ADDMETER M ON (M.QA_ID = QA.QA_ID)", "E.IDNO, isnull(E.Title,'')+''+isnull(E.FNAME,'')+''+isnull(E.MNAME,'')+''+isnull(E.LNAME,'') AS NAME, E.SUBDESIGNO, E.SUBDEPTNO", "DPT.SUBDEPT, DEG.SUBDESIG, QT.QUARTER_TYPE, O.QRT_TYPE,QA.ALLT_ORDER_NO, QA.OFFCE_ORDER_DT, QA.ALLOTMENT_DT, QA.OCCUPIED_DATE, M.QRTNO_ID", "O.EMPID=" + EMPID, "");
            
            if (ds.Tables[0].Rows.Count > 0)
                {
                   ViewState["QRT_TYP_NO"] = ds.Tables[0].Rows[0]["QRT_TYPE"].ToString();
                   lblName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                   lblName.ToolTip = ds.Tables[0].Rows[0]["IDNO"].ToString();

                   lblDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                   lblDesig.ToolTip = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();

                   lblDepart.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                   lblDepart.ToolTip = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();

                   lblQrtTyp.Text = ds.Tables[0].Rows[0]["QUARTER_TYPE"].ToString();
                   lblQrtTyp.ToolTip = ds.Tables[0].Rows[0]["QRT_TYPE"].ToString();

                   if (STATUS == 1)
                   {
                       //objcommon.FillDropDownList(ddlquarterno, "EST_QRT_MST A LEFT JOIN EST_ADDMETER B ON A.IDNO = B.QRTNO_ID", "A.IDNO", "QUARTER_NO + ' --> ' + QRTNAME AS QRTNAME", "QNO=" + Convert.ToInt32(ViewState["QRT_TYP_NO"]), "A.IDNO");
                       DataSet dsQ = objAllot.GetQuarterList(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(ViewState["QRT_TYP_NO"]));
                       if (dsQ.Tables[0].Rows.Count > 0)
                       {
                           ddlquarterno.Items.Clear();
                           ddlquarterno.DataSource = dsQ;
                           ddlquarterno.DataTextField = "QRTNAME";
                           ddlquarterno.DataValueField = "IDNO";
                           ddlquarterno.DataBind();
                       }
                       else
                       {
                         
                       }
                   ddlquarterno.SelectedValue = ds.Tables[0].Rows[0]["QRTNO_ID"].ToString();
                      
                   }
                   else
                   {
                       objcommon.FillDropDownList(ddlquarterno, "EST_QRT_MST", "IDNO", "QUARTER_NO + ' --> ' + QRTNAME AS QRTNAME", "IDNO not in (SELECT Qrtno_id from EST_ADDMETER where QRT_STATUS is null) AND QNO=" + Convert.ToInt32(ViewState["QRT_TYP_NO"]), "IDNO");
                   }                
                  
                   txtallotorderno.Text = ds.Tables[0].Rows[0]["ALLT_ORDER_NO"].ToString();
                   txtorderdate.Text = ds.Tables[0].Rows[0]["OFFCE_ORDER_DT"].ToString();
                   txtallotmentdate.Text = ds.Tables[0].Rows[0]["ALLOTMENT_DT"].ToString();
                   txtOccuDate.Text = ds.Tables[0].Rows[0]["OCCUPIED_DATE"].ToString();
                }            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnReprt_Click(object sender, EventArgs e)
    {
        ShowWaitingDetails("Waiting List", "rptWaitingList.rpt");
        //ShowWaitingDetails(rdoReportType.SelectedValue, "rptWaitingList.rpt");
    }    

    private void ShowWaitingDetails(string exporttype, string rptFileName)
    {
        try
        {  
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));          
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";           
            url += "&filename=WaitingListReport.pdf";          
             url += "&path=~,Reports,ESTATE," + rptFileName;
             url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString(); 

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);         
          //  ScriptManager.RegisterClientScriptBlock(this.updQuarterReport, this.updQuarterReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    
    private void Export(string type)
    {
        try
        {
            string filename = string.Empty;
            string ContentType = string.Empty;
            if (type == "Excel")
            {
                filename = "WaitingList.xls";
                ContentType = "ms-excel";
            }
            
            string attachment = "attachment; filename=" + filename;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", attachment);
            Response.AppendHeader("Refresh", ".5; quarter_allotment.aspx");
            Response.Charset = "";
            Response.ContentType = "application/" + ContentType;
            StringWriter sw1 = new StringWriter();
            HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
            gvWaitingList.RenderControl(htw1);
            Response.Output.Write(sw1.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {   
        Export("Excel");
    }

    private void BindWaitingList()
    {
        try
        {
            DataSet ds = objcommon.FillDropDown("EST_ONLINE_APPLICATION O INNER JOIN PAYROLL_EMPMAS E ON (O.EMPID = E.IDNO) INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DEG ON (E.SUBDESIGNO = DEG.SUBDESIGNO) INNER JOIN EST_QRT_TYPE QT ON (O.QRT_TYPE = QT.QNO) INNER JOIN PAYROLL_STAFF S ON (S.STAFFNO = E.STAFFNO)", "ROW_NUMBER() OVER ( ORDER BY EMPID ) AS SrNo, isnull(E.Title,'')+''+isnull(E.FNAME,'')+''+isnull(E.MNAME,'')+''+isnull(E.LNAME,'') AS APPLICANT_NAME", " DEG.SUBDESIG AS DESIGNATION, DPT.SUBDEPT AS DEPARTMENT, O.APP_DATE AS APPLY_DATE,  QT.QUARTER_TYPE AS QUARTER_APPLIED, E.DOJ AS DATE_OF_JOINING", "O.STATUS=0", "E.DOJ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvWaitingList.DataSource = ds;
                gvWaitingList.DataBind();
                gvWaitingList.Visible = true;
                imgExcel.Visible = true;
            }
            else
            {
                gvWaitingList.DataSource = null;
                gvWaitingList.DataBind();
                imgExcel.Visible = false;
                gvWaitingList.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void gvWaitingList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int count = gvWaitingList.Rows.Count;
        foreach (TableCell tc in e.Row.Cells)
        {           
            tc.Text = Server.HtmlDecode(tc.Text);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
