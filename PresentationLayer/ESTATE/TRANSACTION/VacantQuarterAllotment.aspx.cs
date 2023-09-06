//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : ESATE
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 12-SEP-2016
// DESCRIPTION   : ONLINE APPLICATION FORM TO APPLY FOR QUARTER.
//=========================================================================
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

using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;

public partial class ESTATE_Transaction_VacantQuarterAllotment : System.Web.UI.Page
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
                Session["ApplicantType"] = rdbApplicantType.SelectedValue;

                
            }
            objcommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0", "SUBDEPTNO");
            
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
            objcommon.FillDropDownList(ddlQrtType, "EST_QRT_TYPE", "QNO", "QUARTER_TYPE", "", "QNO");
            objcommon.FillDropDownList(ddlQrtName, "EST_QRT_MST", "IDNO", "QUARTER_NO+ '-' + QRTNAME as QRTNAME", "IDNO  not in (SELECT Qrtno_id from EST_ADDMETER where QRT_STATUS is null)", "IDNO");
            objcommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "E.PFILENO + ' - ' + isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.IDNO NOT IN (SELECT NAME_ID FROM EST_QRT_ALLOTMENT WHERE EMPTYPE_ID <> 2 AND QRT_STATUS IS  NULL) AND E.IDNO NOT IN (SELECT EMPID FROM EST_ONLINE_APPLICATION  WHERE STATUS=0) AND P.PSTATUS='Y'", "E.PFILENO");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }   

    //this is used to save  the  consumer information.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlQrtName.SelectedIndex < 0)
            {
                objcommon.DisplayMessage(this.updQtr, "Please Select Quarter Name.", this.Page);
                return;
            }
            objAllotEntity.Name = Convert.ToInt32(ddlEmployee.SelectedValue); //Convert.ToInt32(ViewState["EMPID"]);
            objAllotEntity.Designation = hdnDesig.Value == "" ? 0 : Convert.ToInt32(hdnDesig.Value); //Convert.ToInt32(lblDesig.ToolTip);
            objAllotEntity.Department = hdndept.Value == "" ? 0 : Convert.ToInt32(hdndept.Value); //Convert.ToInt32(lblDepart.ToolTip);
           
            objAllotEntity.AllotOrderNo = txtallotorderno.Text.Trim().ToString();
            objAllotEntity.OffceOrderDt = DateTime.ParseExact(txtorderdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(.Text.Trim());           
            
            if (txtOccuDate.Text == string.Empty)
            {
                objAllotEntity.occuDate = DateTime.MinValue;
            }
            else
            {
                objAllotEntity.occuDate = DateTime.ParseExact(txtOccuDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (rdbApplicantType.SelectedValue == "2")
            {
                objAllotEntity.EmployeeType = 2;
            }
            else
            {
                objAllotEntity.EmployeeType = 1;
            }
            objAllotEntity.QID = Convert.ToInt32(ddlQrtName.SelectedValue);
            objAllotEntity.QuarterNo = ddlQrtType.SelectedValue;

            if (Convert.ToDateTime(txtallotmentdate.Text) >= Convert.ToDateTime(txtorderdate.Text))
            {
                objAllotEntity.AllotmentDate = DateTime.ParseExact(txtallotmentdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(txtallotmentdate.Text.Trim());                 
            }
            else
            {
                objcommon.DisplayMessage(this.updQtr, "Allotment date can not be less than office order date.", this.Page);
                return;
            }
            objAllotEntity.QuarterRent = Convert.ToDouble(0);
            objAllotEntity.TOTAL_MEMBERS = txtNoMembers.Text == string.Empty ? 0 : Convert.ToInt32(txtNoMembers.Text);

            objAllotEntity.Remark = txtRemark.Text.Trim();                             // Added on 21/03/2022
            

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
                objAllotEntity.QA_ID = Convert.ToInt32(ViewState["QA_ID"]);
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
                    objcommon.DisplayMessage(this.updQtr, "Sorry! Try agian.", this.Page);
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
            ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN  EST_ADDMETER  B  ON(A.QA_ID =B.QA_ID)", "A. EMPTYPE_ID,A.NAME_ID,A.DEPT_ID,A.DESIG_ID,A.ALLT_ORDER_NO,A.OFFCE_ORDER_DT,A.ALLOTMENT_DT,A.QRT_RENT,A.WMETER_STATUS, A.GAS_MID,A.CUSTOMER_ID,A.WATERMETERRENT,B.QRTTYPE_ID ,B.QRTNO_ID, B.EMETER_TYPE_ID, B.METER_NO", "A.QA_ID", "A.NAME_ID=" + EMPID + "AND A.QRT_STATUS IS NULL", "A.QA_ID");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlQrtName.Items.Clear();
                objcommon.FillDropDownList(ddlQrtName, "EST_QRT_MST", "IDNO", "QUARTER_NO", "IDNO >0", "IDNO");              
                txtallotorderno.Text = ds.Tables[0].Rows[0]["ALLT_ORDER_NO"].ToString();
                txtorderdate.Text = ds.Tables[0].Rows[0]["OFFCE_ORDER_DT"].ToString();
                txtallotmentdate.Text = ds.Tables[0].Rows[0]["ALLOTMENT_DT"].ToString();
                txtquarterRent.Text = ds.Tables[0].Rows[0]["QRT_RENT"].ToString();               
                ddlQrtName.SelectedValue = ds.Tables[0].Rows[0]["QRTNO_ID"].ToString();
               
                ViewState["action"] = "edit";
            }
            else
            {
                objcommon.FillDropDownList(ddlQrtName, "EST_QRT_MST", "IDNO", "QUARTER_NO", "IDNO  not in (SELECT Qrtno_id from EST_ADDMETER)", "IDNO");
                ddlQrtName.Enabled = true;                      
                txtallotorderno.Text = string.Empty;
                txtorderdate.Text = string.Empty;
                txtallotmentdate.Text = string.Empty;
                txtquarterRent.Text = string.Empty;
                ViewState["action"] = "add";                               
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
            //ds = objcommon.FillDropDown("EST_QRT_MST", "IDNO, QUARTER_NO", "QUARTER_NO+' -- '+ QRTNAME AS QRTNAME", "IDNO  NOT IN (SELECT QRTNO_ID  FROM EST_ADDMETER WHERE QRT_STATUS IS NULL)", "QUARTER_NO");
            ds = objcommon.FillDropDown("EST_QRT_MST A  INNER JOIN EST_QRT_TYPE B ON(A.QNO=B.QNO) INNER JOIN EST_BLOCK_MASTER BM ON (A.BLOCKID = BM.BLOCKID)", "A.QNO, BM.BLOCKNAME, A.QUARTER_NO,A.QRTNAME", "B.QUARTER_TYPE,A.IDNO", "IDNO  NOT IN (SELECT QRTNO_ID  FROM EST_ADDMETER WHERE QRT_STATUS IS NULL)", "A.QNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RepApplicant.DataSource = ds;
                RepApplicant.DataBind();
                RepApplicant.Visible = true;
            }
            else
            {
                RepApplicant.DataSource = null;
                RepApplicant.DataBind();
                RepApplicant.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void BindAllottedList()
    {
        try
        {
            DataSet ds = null;
            //(CASE WHEN CONSUMERTYPE=1 THEN PE.TITLE ELSE C.TITLE END) + ' ' + (CASE WHEN CONSUMERTYPE=1 THEN PE.FNAME ELSE C.CONSUMERFULLNAME END) AS CONSUMERFULLNAME,
            //ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN EST_ADDMETER E ON (A.QA_ID=E.QA_ID) INNER JOIN EST_QRT_MST QM ON (E.QRTNO_ID = QM.IDNO) INNER JOIN EST_BLOCK_MASTER B ON (B.BLOCKID = QM.BLOCKID) INNER JOIN EST_QRT_TYPE QT ON (QT.QNO = QT.QNO AND QT.QNO = E.QRTTYPE_ID) LEFT JOIN EST_CONSUMER_MST C ON(C.IDNO = A.NAME_ID AND C.CONSUMERTYPE = A.EMPTYPE_ID AND C.ChkStatus='A') LEFT JOIN PAYROLL_EMPMAS PE ON (A.NAME_ID = PE.IDNO AND A.EMPTYPE_ID <> 2)", "A.QA_ID,A.NAME_ID, A.EMPTYPE_ID, QT.QUARTER_TYPE, QM.QUARTER_NO+ ' - '+ QM.QRTNAME AS QRTNAME , 'ALLOTTED' AS [STATUS]", "ISNULL(C.TITLE,PE.TITLE) + ' ' + ISNULL(C.CONSUMERFULLNAME,PE.FNAME) AS NAME, B.BLOCKNAME ", "A.QRT_STATUS IS  NULL", "A.QA_ID");
            ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN EST_ADDMETER E ON (A.QA_ID=E.QA_ID) INNER JOIN EST_QRT_MST QM ON (E.QRTNO_ID = QM.IDNO) INNER JOIN EST_BLOCK_MASTER B ON (B.BLOCKID = QM.BLOCKID) INNER JOIN EST_QRT_TYPE QT ON (QT.QNO = QT.QNO AND QT.QNO = E.QRTTYPE_ID) LEFT JOIN EST_CONSUMER_MST C ON(C.IDNO = A.NAME_ID AND C.CONSUMERTYPE = A.EMPTYPE_ID AND C.ChkStatus='A') LEFT JOIN PAYROLL_EMPMAS PE ON (A.NAME_ID = PE.IDNO AND A.EMPTYPE_ID <> 2)", "A.QA_ID,A.NAME_ID, A.EMPTYPE_ID, QT.QUARTER_TYPE, QM.QUARTER_NO+ ' - '+ QM.QRTNAME AS QRTNAME , 'ALLOTTED' AS [STATUS]", "ISNULL(PE.TITLE,C.TITLE) + ' ' + ISNULL(PE.FNAME,C.CONSUMERFULLNAME) AS NAME, B.BLOCKNAME ", "A.QRT_STATUS IS  NULL", "A.QA_ID");
            
            //ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN EST_ADDMETER E ON (A.QA_ID=E.QA_ID) INNER JOIN EST_QRT_MST QM ON (E.QRTNO_ID = QM.IDNO) INNER JOIN EST_BLOCK_MASTER B ON (B.BLOCKID = QM.BLOCKID) INNER JOIN EST_QRT_TYPE QT ON (QT.QNO = QT.QNO AND QT.QNO = E.QRTTYPE_ID) LEFT JOIN EST_CONSUMER_MST C ON(C.IDNO = A.NAME_ID AND C.CONSUMERTYPE = A.EMPTYPE_ID AND C.ChkStatus='A') LEFT JOIN PAYROLL_EMPMAS PE ON (A.NAME_ID = PE.IDNO AND A.EMPTYPE_ID <> 2)", "A.QA_ID,A.NAME_ID, A.EMPTYPE_ID, QT.QUARTER_TYPE, QM.QUARTER_NO+ ' - '+ QM.QRTNAME AS QRTNAME , 'ALLOTTED' AS [STATUS]", "(CASE WHEN CONSUMERTYPE=1 THEN PE.TITLE ELSE C.TITLE END) + ' ' + (CASE WHEN CONSUMERTYPE=1 THEN PE.FNAME ELSE C.CONSUMERFULLNAME END) AS NAME, B.BLOCKNAME ", "A.QRT_STATUS IS  NULL", "A.QA_ID");      // changes by Vish on 15032022

            // ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN EST_ADDMETER E ON (A.QA_ID=E.QA_ID) INNER JOIN EST_QRT_MST QM ON (E.QRTNO_ID = QM.IDNO) INNER JOIN EST_BLOCK_MASTER B ON (B.BLOCKID = QM.BLOCKID) INNER JOIN EST_QRT_TYPE QT ON (QT.QNO = QT.QNO AND QT.QNO = E.QRTTYPE_ID) LEFT JOIN EST_CONSUMER_MST C ON(C.IDNO = A.NAME_ID AND C.CONSUMERTYPE = A.EMPTYPE_ID AND C.ChkStatus='A') LEFT JOIN PAYROLL_EMPMAS PE ON (A.NAME_ID = PE.IDNO AND A.EMPTYPE_ID <> 2 and PE.SUBDEPTNO = C.SUBDEPTNO)", "A.QA_ID,A.NAME_ID, A.EMPTYPE_ID, QT.QUARTER_TYPE, QM.QUARTER_NO+ ' - '+ QM.QRTNAME AS QRTNAME , 'ALLOTTED' AS [STATUS]", "(CASE WHEN CONSUMERTYPE=1 THEN PE.TITLE ELSE C.TITLE END) + ' ' + (CASE WHEN CONSUMERTYPE=1 THEN PE.FNAME ELSE C.CONSUMERFULLNAME END) AS NAME, B.BLOCKNAME ", "A.QRT_STATUS IS  NULL", "A.QA_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                repAllotted.DataSource = ds;
                repAllotted.DataBind();
                repAllotted.Visible = true;
            }
            else
            {
                repAllotted.DataSource = null;
                repAllotted.DataBind();
                repAllotted.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // this method is used to find the file name.
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetEmployeeName(string prefixText)
    {
        List<string> EmployeeName = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            QuarterAllotmentController objAllot = new QuarterAllotmentController();
            ds = objAllot.FillEmployeeName(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                EmployeeName.Add(ds.Tables[0].Rows[i]["IDNO"].ToString() + "---------*" + ds.Tables[0].Rows[i]["NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return EmployeeName;
    }

    protected void funclear()
    {
        txtallotmentdate.Text = string.Empty;
        txtallotorderno.Text = string.Empty;
        txtquarterRent.Text = string.Empty;
        txtorderdate.Text = string.Empty;
        txtOccuDate.Text = string.Empty;
        ViewState["action"] = "add";       
        btnSave.Visible = true;
        pnlDetails.Visible = false;

        ddlQrtName.SelectedIndex = 0;
        ddlQrtType.SelectedIndex = 0;
        rdbApplicantType.SelectedValue = "0";
        lblDepart.Text = string.Empty;
        lblDesig.Text = string.Empty;
        ddlEmployee.SelectedIndex = 0;
        txtNoMembers.Text =  string.Empty;
        
        objcommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "PFILENO + ' - ' + isnull(Title,'')+''+isnull(FNAME,'')+''+isnull(MNAME,'')+''+isnull(LNAME,'')  AS NAME", "IDNO NOT IN (SELECT NAME_ID FROM EST_QRT_ALLOTMENT WHERE EMPTYPE_ID <> 2) AND IDNO NOT IN (SELECT EMPID FROM EST_ONLINE_APPLICATION )", "IDNO");
        repAllotted.Visible = false;
        if (chkList.Checked == true)
        {
            repAllotted.Visible = true;
            RepApplicant.Visible = false;
        }
        else
        {
            repAllotted.Visible = false;
            RepApplicant.Visible = true;
        }
        ViewState["QA_ID"] = null;
        ViewState["EMPTYPE_ID"] = null;
        Session["ApplicantType"] = rdbApplicantType.SelectedValue;
        txtEmployee.Text = string.Empty;
        hfEmployeeId.Value = "0";
        txtRemark.Text = string.Empty;                                // Added on 21/03/2022

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        funclear();
    }   

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        try
        {
            pnlDetails.Visible = true;
            ViewState["IDNO"] = Convert.ToInt32(btn.CommandArgument);
            ShowDetails(Convert.ToInt32(ViewState["IDNO"]));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        try
        {
            pnlDetails.Visible = true;
            ViewState["QA_ID"] = Convert.ToInt32(btn.CommandArgument);
            ViewState["EMPTYPE_ID"] = Convert.ToInt32(btn.CommandName);
            ViewState["action"] = "edit";
            ShowAllottedDetails(Convert.ToInt32(ViewState["QA_ID"]), Convert.ToInt32(ViewState["EMPTYPE_ID"]));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // this method is used to show the details fetch from database.
    private void ShowAllottedDetails(int QA_ID, int EMPTYPE_ID)
    {
        try
        {
            DataSet ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT A INNER JOIN EST_ADDMETER E ON(A.QA_ID = E.QA_ID) INNER JOIN PAYROLL_SUBDEPT D ON (A.DEPT_ID = D.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DE ON (A.DESIG_ID = DE.SUBDESIGNO)", "E.QRTTYPE_ID, E.QRTNO_ID, A.NAME_ID, A.EMPTYPE_ID, A.DESIG_ID, DE.SUBDESIG, A.DEPT_ID,D.SUBDEPT", "A.ALLT_ORDER_NO, A.ALLOTMENT_DT, A.OCCUPIED_DATE, A.OFFCE_ORDER_DT,A.Remark, A.TOTAL_MEMBERS", "A.QA_ID=" + QA_ID + " AND A.QRT_STATUS IS NULL AND A.EMPTYPE_ID= " + EMPTYPE_ID, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlQrtType.SelectedValue = ds.Tables[0].Rows[0]["QRTTYPE_ID"].ToString();                
                objcommon.FillDropDownList(ddlQrtName, "EST_QRT_MST", "IDNO", "QUARTER_NO+ '-' + QRTNAME as QRTNAME ", "", "IDNO");
                ddlQrtName.SelectedValue = ds.Tables[0].Rows[0]["QRTNO_ID"].ToString();
                
                if (EMPTYPE_ID == 1)
                {
                    objcommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "PFILENO + ' - ' + isnull(Title,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'')  AS NAME", "", "IDNO");
                   
                    rdbApplicantType.SelectedValue = "0";
                }
                else
                {
                    objcommon.FillDropDownList(ddlEmployee, "EST_CONSUMER_MST", "IDNO", "CONSUMERFULLNAME AS NAME", "", "IDNO");
                    rdbApplicantType.SelectedValue ="2";
                }

                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["NAME_ID"].ToString();

                if (EMPTYPE_ID == 1)
                {
                    string APPID = objcommon.LookUp("EST_ONLINE_APPLICATION", "APPID", "EMPID=" + Convert.ToInt32(ddlEmployee.SelectedValue));
                    if (APPID != "")
                    {
                        rdbApplicantType.SelectedValue = "1";
                    }
                    else
                    {
                        rdbApplicantType.SelectedValue = "0";
                    }
                }
               

                lblDepart.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                hdndept.Value = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();
                lblDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                hdnDesig.Value = ds.Tables[0].Rows[0]["DESIG_ID"].ToString();

                txtallotorderno.Text = ds.Tables[0].Rows[0]["ALLT_ORDER_NO"].ToString();
                txtorderdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OFFCE_ORDER_DT"]).ToString("dd/MM/yyyy");
                txtallotmentdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ALLOTMENT_DT"]).ToString("dd/MM/yyyy");
                if (ds.Tables[0].Rows[0]["OCCUPIED_DATE"].ToString() == "")
                {
                    txtOccuDate.Text = ds.Tables[0].Rows[0]["OCCUPIED_DATE"].ToString();
                }
                else
                {
                    txtOccuDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OCCUPIED_DATE"]).ToString("dd/MM/yyyy");
                }

                if (ds.Tables[0].Rows[0]["TOTAL_MEMBERS"].ToString() == "0")
                {
                    txtNoMembers.Text = string.Empty;
                }
                else
                {
                    txtNoMembers.Text = ds.Tables[0].Rows[0]["TOTAL_MEMBERS"].ToString();
                }
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();                                  // Added on 21/03/2022
                ddlQrtType.Enabled = false;
                ddlQrtName.Enabled = false;
                ddlEmployee.Enabled = false;
                rdbApplicantType.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // this method is used to show the details fetch from database.
    private void ShowDetails(int IDNO)
    {
        try
        {
            DataSet ds = objcommon.FillDropDown("EST_QRT_MST QM INNER JOIN  EST_QRT_TYPE QT ON (QM.QNO = QT.QNO)", "QM.QUARTER_NO, QM.IDNO", "QT.QNO", "QM.IDNO=" + IDNO, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlQrtType.SelectedValue = ds.Tables[0].Rows[0]["QNO"].ToString();
                ddlQrtName.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();

                ////objcommon.FillDropDownList(ddlquarterno, "EST_QRT_MST A LEFT JOIN EST_ADDMETER B ON A.IDNO = B.QRTNO_ID", "A.IDNO", "QUARTER_NO + ' --> ' + QRTNAME AS QRTNAME", "QNO=" + Convert.ToInt32(ViewState["QRT_TYP_NO"]), "A.IDNO");
                //DataSet dsQ = objAllot.GetQuarterList(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(ViewState["QRT_TYP_NO"]));
                //if (dsQ.Tables[0].Rows.Count > 0)
                //{
                //    ddlquarterno.Items.Clear();
                //    ddlquarterno.DataSource = dsQ;
                //    ddlquarterno.DataTextField = "QRTNAME";
                //    ddlquarterno.DataValueField = "IDNO";
                //    ddlquarterno.DataBind();
                //}
                //else
                //{
                //}
                //ddlquarterno.SelectedValue = ds.Tables[0].Rows[0]["QRTNO_ID"].ToString();
                //}
                //else
                //{
                //    objcommon.FillDropDownList(ddlQrtName, "EST_QRT_MST", "IDNO", "QUARTER_NO + ' --> ' + QRTNAME AS QRTNAME", "IDNO not in (SELECT Qrtno_id from EST_ADDMETER where QRT_STATUS is null) AND QNO=" + Convert.ToInt32(ViewState["QRT_TYP_NO"]), "IDNO");
                //}
                //txtallotorderno.Text = ds.Tables[0].Rows[0]["ALLT_ORDER_NO"].ToString();
                //txtorderdate.Text = ds.Tables[0].Rows[0]["OFFCE_ORDER_DT"].ToString();
                //txtallotmentdate.Text = ds.Tables[0].Rows[0]["ALLOTMENT_DT"].ToString();
                //txtOccuDate.Text = ds.Tables[0].Rows[0]["OCCUPIED_DATE"].ToString();

                ddlQrtType.Enabled = true;
                ddlQrtName.Enabled = true;
                ddlEmployee.Enabled = true;
                rdbApplicantType.Enabled = true;
                txtRemark.Enabled = true;
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

    protected void rdbApplicantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
              
            if (rdbApplicantType.SelectedValue == "0")
            {
               
                objcommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "PFILENO + ' - ' + isnull(Title,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'')  AS NAME", "IDNO NOT IN (SELECT NAME_ID FROM EST_QRT_ALLOTMENT WHERE EMPTYPE_ID <> 2 AND QRT_STATUS IS NULL) AND IDNO NOT IN (SELECT EMPID FROM EST_ONLINE_APPLICATION )", "PFILENO");
                Session["ApplicantType"] = rdbApplicantType.SelectedValue;
                txtEmployee.Text = string.Empty;
                hfEmployeeId.Value = "0";
                //rdbApplicantType.SelectedValue = "0";
                rdbApplicantType.Focus();
            }
            else if (rdbApplicantType.SelectedValue == "1")
            {
               
                objcommon.FillDropDownList(ddlEmployee, "EST_ONLINE_APPLICATION O INNER JOIN PAYROLL_EMPMAS E ON (O.EMPID = E.IDNO)", "O.EMPID AS IDNO", "E.PFILENO + ' - ' + isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "O.STATUS=0", "E.PFILENO");
                Session["ApplicantType"] = rdbApplicantType.SelectedValue;
                txtEmployee.Text = string.Empty;
                hfEmployeeId.Value = "0";
                //rdbApplicantType.SelectedValue = "1";
                rdbApplicantType.Focus();
                
            }
            else
            {
                
               //rdbApplicantType.SelectedValue = "2";
               objcommon.FillDropDownList(ddlEmployee, "EST_CONSUMER_MST", "IDNO", "CONSUMERFULLNAME AS NAME", "IDNO NOT IN (SELECT NAME_ID FROM EST_QRT_ALLOTMENT WHERE EMPTYPE_ID <> 1)", "IDNO");
               Session["ApplicantType"] = rdbApplicantType.SelectedValue;
               txtEmployee.Text = string.Empty;
               hfEmployeeId.Value = "0";
               rdbApplicantType.Focus();
            
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
  
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
             if (rdbApplicantType.SelectedValue == "2")
             {
                 ds = objcommon.FillDropDown("EST_CONSUMER_MST C INNER JOIN PAYROLL_SUBDEPT DPT ON (C.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DEG ON (C.SUBDESIGNO = DEG.SUBDESIGNO)", "C.IDNO, C.SUBDEPTNO, DEG.SUBDESIGNO", "DPT.SUBDEPT, DEG.SUBDESIG", "C.IDNO=" + Convert.ToInt32(ddlEmployee.SelectedValue), "");
             }
             else 
             {
                 string Total_Members = objcommon.LookUp("EST_ONLINE_APPLICATION", "TOTAL_MEMBERS", "EMPID=" + Convert.ToInt32(ddlEmployee.SelectedValue));
                 if (Total_Members != "")
                 {                    
                     txtNoMembers.Text = Total_Members;
                 }               
                 ds = objcommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DEG ON (E.SUBDESIGNO = DEG.SUBDESIGNO)", "E.SUBDESIGNO, E.SUBDEPTNO", "DPT.SUBDEPT, DEG.SUBDESIG", "E.IDNO=" + Convert.ToInt32(ddlEmployee.SelectedValue), "");                 
             }               

             if (ds.Tables[0].Rows.Count > 0)
                {
                    lblDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                    hdnDesig.Value = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();

                    lblDepart.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                    hdndept.Value = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void chkList_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkList.Checked == true)
            {
                BindAllottedList();
                RepApplicant.Visible = false;
                pnlDetails.Visible = false;
                funclear();
            }
            else
            {
                BindApplicantRepeater();
                repAllotted.Visible = false;
                pnlDetails.Visible = false;
                funclear();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected void txtEmployee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmployee.SelectedIndex > 0)
            {
                //ddlEmployee_SelectedIndexChanged(null, new EventArgs());
                DataSet ds = null;
                if (rdbApplicantType.SelectedValue == "2")
                {
                    ds = objcommon.FillDropDown("EST_CONSUMER_MST C INNER JOIN PAYROLL_SUBDEPT DPT ON (C.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DEG ON (C.SUBDESIGNO = DEG.SUBDESIGNO)", "C.IDNO, C.SUBDEPTNO, DEG.SUBDESIGNO", "DPT.SUBDEPT, DEG.SUBDESIG", "C.IDNO=" + Convert.ToInt32(ddlEmployee.SelectedValue), "");
                }
                else
                {
                    string Total_Members = objcommon.LookUp("EST_ONLINE_APPLICATION", "TOTAL_MEMBERS", "EMPID=" + Convert.ToInt32(ddlEmployee.SelectedValue));
                    if (Total_Members != "")
                    {
                        txtNoMembers.Text = Total_Members;
                    }
                    ds = objcommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DEG ON (E.SUBDESIGNO = DEG.SUBDESIGNO)", "E.SUBDESIGNO, E.SUBDEPTNO", "DPT.SUBDEPT, DEG.SUBDESIG", "E.IDNO=" + Convert.ToInt32(ddlEmployee.SelectedValue), "");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                    hdnDesig.Value = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();

                    lblDepart.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                    hdndept.Value = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                }
            }
        }
         catch (Exception ex)
         {
             Console.WriteLine(ex.ToString());
         }
          
    }


    // Added on 21/03/2022

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        objcommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO) INNER JOIN Payroll_subdept SU ON (SU.SUBDEPTNO =E.SUBDEPTNO)", "E.IDNO", "ISNULL(FNAME,'') + ' ' + ISNULL(MNAME,'') + ' ' + ISNULL(LNAME,'')NAME", "PSTATUS='Y' AND SU.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue), "");
        
    }


}