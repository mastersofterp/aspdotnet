//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIRS & MAINTAINENCE                                               
// PAGE NAME     : COMPLAINT WORKOUT                                                    
// CREATION DATE : 17-April-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;

 public partial class Estate_Complaint_Workout : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public string path = string.Empty;
    public string dbPath = string.Empty;
    string complaintno = string.Empty;


    protected void bPage_PreInit(object sender, EventArgs e)
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
                //Check browser and set table width
                //if (Request.Browser.Browser.ToLower().Equals("opera"))
                //    tblMain.Width = "100%";
                //else if (Request.Browser.Browser.ToLower().Equals("ie"))
                //    tblMain.Width = "100%";
                //else
                //    tblMain.Width = "100%";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //Bind the ListView with Domain
                BindListViewComplaintDetails(Convert.ToInt32(Session["userno"].ToString()));
                //Enabled listview panal
                pnlList.Visible = true;               
                pnlworkoutdetails.Visible = false;
                pnlworkout.Visible = false;
                Pnlbutton.Visible = false;
                PnlAddDetails.Visible = false;
                txtWorkoutDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }               
        }
        else
        {
            if (!(txtWorkoutDate.Text == null || txtWorkoutDate.Text == "" || txtWorkoutDate.Text == string.Empty))
                CalendarExtender1.SelectedDate = Convert.ToDateTime(txtWorkoutDate.Text);
        }
    }
   
    private void BindListViewComplaintDetails(Int32 userno)
    {
        try
        {
            ComplaintController objCC = new ComplaintController();
            DataSet dsca = objCC.GetAllCompaintAllotmentDetails(userno);
            lvComplaintDetails.DataSource = dsca;
            lvComplaintDetails.DataBind();            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Workout.BindListViewComplaintDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewWorkoutDetails(Int32 userno,Int32 compno)
    {
        try
        {
            ComplaintController objCC = new ComplaintController();
            DataSet dscw = objCC.GetAllWorkoutDetails(userno,compno);
            lvWorkoutDetails.DataSource = dscw;
            lvWorkoutDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Workout.BindListViewWorkoutDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["compl"] = null;
            Clear();  
            Int32 empid;
            ComplaintController objcc=new ComplaintController();

            ImageButton btnEdit = sender as ImageButton;
            int complaintno = int.Parse(btnEdit.CommandArgument);            

            ViewState["complaintno"] = complaintno.ToString();
            ViewState["compl"] = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINTNO", "COMPLAINTID=" + ViewState["complaintno"].ToString());//ADDED  BY VIJAY ON 24-02-2020
            ShowDetails(complaintno);
            lblCompNo.Text = Convert.ToString(complaintno);
            lblEmployee.Text = Session["userfullname"].ToString();
            //txtcomplaintsdt.Text = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINT", "COMPLAINTNO="+complaintno);
            txtcomplaintsdt.Text = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINT", "COMPLAINTID=" + complaintno);
            //empid = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ALLOTMENT","EMPID","COMPLAINTID=" + complaintno));
            empid = Convert.ToInt32(objCommon.LookUp("CELL_COMPLAINT_ALLOTMENT", "EMPID", "COMPLAINTID=" + complaintno));

            lblEmployee.Text = objCommon.LookUp("USER_ACC","UA_FULLNAME", "UA_NO=" + empid);

            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN CELL_COMPLAINT_ALLOTMENT CCA ON (CR.COMPLAINTID = CCA.COMPLAINTID) INNER JOIN COMPLAINT_USER CU ON (CU.C_EMPNO = CCA.USERNO) INNER JOIN USER_ACC U ON (U.UA_NO = CCA.USERNO)", "CR.COMPLAINTID, CR.COMPLAINTEE_ADDRESS, CR.COMPLAINTEE_PHONENO, U.UA_FULLNAME", "", "CR.COMPLAINTID=" + complaintno, "");
            if (ds.Tables[0].Rows.Count > 0)
            { 
                lblAllotterName.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                lblLocation.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_ADDRESS"].ToString();
                lblContactNo.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_PHONENO"].ToString();
            }
                



            BindListViewWorkoutDetails(empid, complaintno);
            pnlworkout.Visible = true;
            Pnlbutton.Visible = true;
            pnlworkoutdetails.Visible = true;           
            pnlList.Visible = false;
            chkAddItem.Checked = false;
            rdocomp2.Checked = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Workout.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }    
    }
    
    private void ShowDetails(int compno)
    {
        ComplaintController objcc = new ComplaintController();
        SqlDataReader dr = objcc.Getworkout(compno);
      
        //to show created user details     
        if (dr != null)
        {
            if (dr.Read())
            {

                CalendarExtender1.SelectedDate = Convert.ToDateTime(dr["WORKDATE"].ToString());
                txtDetails.Text = dr["WORKOUT"].ToString();
                string compstatus = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINTSTATUS", "COMPLAINTID=" + compno);//ADDED  BY VIJAY ON 24-02-2020
                if (compstatus == "I")
                {
                    rdocomp1.Checked = true;
                    rdocomp2.Checked = false;
                }
                else if(compstatus == "C")
                {
                    rdocomp2.Checked = true;
                }
            }
        }

        if (dr != null) dr.Close();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int32 empid;
        //empid = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ALLOTMENT", "EMPID", "COMPLAINTID=" + Convert.ToInt32(lblCompNo.Text)));
        empid = Convert.ToInt32(objCommon.LookUp("CELL_COMPLAINT_ALLOTMENT", "EMPID", "COMPLAINTID=" + lblCompNo.Text));//ADDED  BY VIJAY ON 25-02-2020

        try
        {
            ComplaintController objCC = new ComplaintController();
            Complaint objWorkedOut = new Complaint();
            //objWorkedOut.Deptid = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ALLOTMENT", "DEPTID", "COMPLAINTID=" + Convert.ToInt32(lblCompNo.Text)));
            objWorkedOut.Deptid = Convert.ToInt32(objCommon.LookUp("CELL_COMPLAINT_ALLOTMENT", "DEPTID", "COMPLAINTID=" + Convert.ToInt32(lblCompNo.Text))); //ADDED  BY VIJAY ON 25-02-2020
            objWorkedOut.ComplaintId = Convert.ToInt32(lblCompNo.Text);
            objWorkedOut.ComplaintId = Convert.ToInt32(lblCompNo.Text);
            objWorkedOut.WorkDate = Convert.ToDateTime(txtWorkoutDate.Text);
            objWorkedOut.WorkOut = txtDetails.Text;
            objWorkedOut.EmpId = empid;            
            objWorkedOut.ItemId =Convert.ToInt32(ddlRMItemName.SelectedValue);
            objWorkedOut.ItemName = ddlRMItemName.SelectedItem.ToString();
            objWorkedOut.TypeId = Convert.ToInt32(ddlRMItemType.SelectedValue);
            objWorkedOut.Type_Name = ddlRMItemType.SelectedItem.ToString();
            objWorkedOut.ItemUnit = txtItemUnit.Text;
            
            if (txtQtyIssued.Text == null || txtQtyIssued.Text == string.Empty || txtQtyIssued.Text == "")
                objWorkedOut.QtyIssued = 0;
            else
                objWorkedOut.QtyIssued = Convert.ToInt32(txtQtyIssued.Text);     
            
            if (rdocomp1.Checked == true)
                objWorkedOut.C_Status = "I";          
            else
                objWorkedOut.C_Status = "C";

            int stock = checkstock(Convert.ToInt32(ddlRMItemName.SelectedValue));
            if (stock == 1 || stock == -1)
            {
                CustomStatus cs = (CustomStatus)objCC.AddComplaintWorkOut(objWorkedOut);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    string emialID = string.Empty;
                    string messBody = string.Empty;

                    if (objWorkedOut.C_Status.Equals("C"))
                    {
                        DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR inner join user_acc UA on(cr.UA_NO =ua.UA_NO)inner join PAYROLL_EMPMAS PM on(pm.IDNO =ua.UA_IDNO)", "ua.UA_IDNO,cr.COMPLAINT", "cr.COMPLAINTNO,pm.EMAILID", "CR.COMPLAINTNO=" + Convert.ToInt16(ViewState["complaintno"].ToString()), "CR.COMPLAINTNO");

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            emialID = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                            messBody = "Your" + " " + "Service Request No. :-" + "  " + ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString() + "  " + "Service:-" + ds.Tables[0].Rows[0]["complaint"].ToString() + "  " + "has been completed";

                            MailSender objSend = new MailSender();
                         //   objSend.sendingmail(emialID, messBody);
                        }
                    }
                    Clear();
                  //  LableErr.Text = "Record Saved Successfully.";
                    objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                    BindListViewComplaintDetails(Convert.ToInt32(Session["userno"].ToString()));
                }
            }           
            else if(stock == 0)      
            {
                LableErr.Visible = true;
                LableErr.Text = ddlRMItemName.SelectedItem.ToString()+ " Are Out of stock"  ;
            }               
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Workout.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }       
        BindListViewWorkoutDetails(empid, Convert.ToInt32(lblCompNo.Text));      
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();    
    }
    
    public Int32 checkstock(int itemno)
    {
        Int32 result=0;
        if (itemno == -1)
        {
            result= -1;
        }
        else if (!(itemno == -1))
        {
            Int32 minstock = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ITEMMASTER", "MINSTOCK", "ITEMID=" + itemno));
            Int32 currstock = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ITEMMASTER", "CURRSTOCK", "ITEMID=" + itemno));
            Int32 qutissued = Convert.ToInt32(txtQtyIssued.Text);
            Int32 checkstock;
            checkstock = currstock - qutissued;
            if (checkstock > minstock)
            {
                result= 1;
            }
            else             
            {
                result = 0; 
            }            
        }
        return result;
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlworkout.Visible = false;
        PnlAddDetails.Visible = false;
        Pnlbutton.Visible = false;
        pnlworkoutdetails.Visible = false;
        pnlList.Visible = true;
       
        //BindListViewComplaintDetails(Convert.ToInt32(Session["userno"].ToString()));
    }
    
    protected void ddlRMItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClrItemName(Convert.ToInt32(ddlRMItemType.SelectedValue));      
    }

    protected void ddlRMItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        cleartxtItemUnitFill(Convert.ToInt32(ddlRMItemName.SelectedValue));         
    }

    private void FillItemType()
    {     
         try 
         {
             Int32 deptid;
             deptid = Convert.ToInt32(objCommon.LookUp("COMPLAINT_USER", "DISTINCT C_DEPTNO","C_UANO=" + Session["userno"].ToString()));
             ComplaintController objCC = new ComplaintController();
             DataSet ds = objCC.GetAllComplaintType(deptid);             
             ddlRMItemType.DataSource = ds;
             ddlRMItemType.DataValueField = ds.Tables[0].Columns["TYPEID"].ToString();
             ddlRMItemType.DataTextField = ds.Tables[0].Columns["TYPENAME"].ToString();
             ddlRMItemType.DataBind();
             ddlRMItemType.SelectedIndex = -1;

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Estate_Complaint_Workout.FillItemType-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }

    private void FillItemName(Int32 itemid)
     {
         try
         {          
             ComplaintController objCC = new ComplaintController();
             DataSet ds = objCC.GetItemNameByComplaintType(itemid);

             ddlRMItemName.DataSource = ds;
             ddlRMItemName.DataValueField = ds.Tables[0].Columns["ITEMID"].ToString();
             ddlRMItemName.DataTextField = ds.Tables[0].Columns["ITEMNAME"].ToString();
             ddlRMItemName.DataBind();
             ddlRMItemName.SelectedIndex = -1;

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Estate_Complaint_Workout.FillItemName-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }

    private void ClrItemName(Int32 itemid)
    {
        ddlRMItemName.Items.Clear();
        ddlRMItemName.Items.Add("Please Select");
        ddlRMItemName.SelectedItem.Value = "-1";
        FillItemName(itemid);
        txtWorkoutDate.Text = "";
    }

    private void ClrItemType()
    {
        ddlRMItemType.Items.Clear();
        ddlRMItemType.Items.Add("Please Select");
        ddlRMItemType.SelectedItem.Value = "-1";
        FillItemType();
    }
    
    private void cleartxtItemUnitFill(Int32 itemid)
    {         
        txtItemUnit.Text = string.Empty;
        ComplaintController objcc = new ComplaintController();
        SqlDataReader dr = objcc.GetItemUnits(itemid);

        if(dr != null)       
            if(dr.Read()) txtItemUnit.Text = Convert.ToString(dr["ITEMUNIT"]);       
        dr.Close();       
    }
 
     // clear method
    private void Clear()
    {
        txtDetails.Text = string.Empty;
        LableErr.Text = string.Empty;
        ddlRMItemName.SelectedIndex = 0;
        ddlRMItemType.SelectedIndex = 0;
        txtItemUnit.Text = string.Empty;
        txtQtyIssued.Text = string.Empty;
        txtWorkoutDate.Text   = string.Empty;
       // txtcomplaintsdt.Text  = string.Empty;
        rdocomp1.Checked = true;
       // rdocomp2.Checked = false;
       
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
     {
         //Bind the ListView with Domain
         BindListViewComplaintDetails(Convert.ToInt32(Session["userno"].ToString()));
     }

    protected void dpPagerWorkout_PreRender(object sender, EventArgs e)
     {
        
         //Bind the ListView with Domain
         Int32 compno;

         if (ViewState["complaintno"] != null)
             compno = Convert.ToInt32(ViewState["complaintno"].ToString());
         else
             compno = 0;
         Int32 empid = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ALLOTMENT", "EMPID", "COMPLAINTID=" + compno));
         BindListViewWorkoutDetails(empid, compno);
     }

    protected void chkAddItem_CheckedChanged(object sender, EventArgs e)
    {
        ClrItemType();  

        LableErr.Text = "";

        if(chkAddItem.Checked==true)         
        PnlAddDetails.Visible = true;
        else if(chkAddItem.Checked==false)
        PnlAddDetails.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }

    }
}

