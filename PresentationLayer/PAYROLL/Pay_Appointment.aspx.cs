//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayRoll_Appointment.ASPX                                                    
// CREATION DATE : 05-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_Pay_Appointment : System.Web.UI.Page
{   
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                pnlPayhead.Visible = true;
                PnlAppoint.Visible = true;
                
                //Binging least view of payhead,appoint
                BindListViewPayHead();
                BindListViewAppoint();
            }
        }

        if (ViewState["action"] == null)
            ViewState["action"] = "add";

    }

    private void BindListViewPayHead()
    {
        try
        { 
            DataSet ds = objpay.GetAllPayHead();
            if(ds.Tables[0].Rows.Count <= 0)
                pnlPayhead.Visible = false;                
            else
            {
                pnlPayhead.Visible = true;
                lvPayhead.DataSource = ds;
                lvPayhead.DataBind();
                ds.Dispose();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.BindListViewPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewAppoint()
    {
        try
        {  
            DataSet ds = objpay.GetAllAppoint();
            if (ds.Tables[0].Rows.Count <= 0)
                PnlAppoint.Visible = true;                
            else
            {
                PnlAppoint.Visible = true;
                lvAppoint.DataSource = ds;
                lvAppoint.DataBind();
                ds.Dispose();
            }
            Clear(); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.BindListViewAppoint-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        { 
            string colvalues;
            int checkcount=0;
            colvalues = string.Empty;
            
            if (ViewState["action"].ToString() == "edit")
            colvalues = colvalues +"Appoint='"+ txtAppointMent.Text +"',";            
           
             foreach (ListViewDataItem lvitem in lvPayhead.Items)
             {  
                    CheckBox chk = lvitem.FindControl("ChkAppointment") as CheckBox;
                    if (chk.Checked)
                    {
                        if (ViewState["action"].ToString() == "add")
                        {
                            checkcount = 1;
                        }

                       if(ViewState["action"].ToString()== "add") 
                        colvalues = colvalues + "1" + ",";
                       else
                        colvalues = colvalues + chk.ToolTip +"=1" + ",";
                    }
                    else
                    {
                        if (ViewState["action"].ToString() == "add") 
                          colvalues = colvalues + "0" + ",";
                        else
                          colvalues = colvalues + chk.ToolTip + "=0" + ",";
                    }
                }
                
                string colval=colvalues.Substring(0,colvalues.Length - 1);

                if(ViewState["action"].ToString() == "add")
                {
                    //Atleast one checkbox to be checked 
                    if (checkcount == 1)
                    {
                        //duplicate check 
                        Int32 dubcheckadd = Convert.ToInt32(objCommon.LookUp("payroll_appoint", "count(*)", "appoint='" + txtAppointMent.Text + "'"));
                        if (dubcheckadd <= 0)
                        {
                            CustomStatus cs = (CustomStatus)objpay.AddAppointment(txtAppointMent.Text, colval, Session["colcode"].ToString());
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                lblerror.Text = null;
                                lblmsg.Text = "Record Saved Successfully";
                            }
                        }
                        else
                        {
                            lblmsg.Text = null;
                            lblerror.Text = "Record already exists";
                        }
                    }
                    else
                    {
                        lblmsg.Text = null;
                        lblerror.Text = "Check at least one payhead";                    
                    }
                    
                }
                else if (ViewState["action"].ToString() == "edit")
                {
                    //duplicate check 
                    Int32 dubcheckedit = Convert.ToInt32(objCommon.LookUp("payroll_appoint", "count(*)", "appoint='" + txtAppointMent.Text + "' and  appointno <> " + Convert.ToInt32(ViewState["appointno"].ToString())));
                    if (dubcheckedit <= 0)
                    {
                        CustomStatus cs = (CustomStatus)objpay.UpdateAppoint(Convert.ToInt32(ViewState["appointno"].ToString()), colval);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            lblerror.Text = null;
                            lblmsg.Text = "Record Updated Successfully";
                        }
                    }
                    else
                    {

                        lblmsg.Text = null;
                        lblerror.Text = "Record already exists";
                    }
                
                }

              BindListViewAppoint();
              Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {           
            lblmsg.Text = null;
            ImageButton btnEdit = sender as ImageButton;
            int appointno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(appointno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int appointno)
    {
        try
        {
            SqlDataReader dr = objpay.GetAppointno(appointno);
            DataSet ds = objpay.GetAllPayHead();
            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["appointno"] = appointno.ToString();
                    txtAppointMent.Text = dr["APPOINT"].ToString();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count-1; i++)
                        {
                            CheckBox chk = lvPayhead.Items[i].FindControl("ChkAppointment") as CheckBox;

                            Boolean checktrue=Convert.ToBoolean(dr["" + ds.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString());

                            if(checktrue)
                            chk.Checked = true;
                            else
                            chk.Checked = false;
                        }
                    }
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
            objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void Clear()
    {
        try
        {        
            lblerror.Text = string.Empty;
            lblmsg.Text = string.Empty;
            txtAppointMent.Text = string.Empty;
            ViewState["action"] = null;
            CheckBox chkAll = lvPayhead.FindControl("chkAll") as CheckBox;
            chkAll.Checked = false;
            foreach (ListViewDataItem lvitem in lvPayhead.Items)
            {
                CheckBox chk = lvitem.FindControl("ChkAppointment") as CheckBox;
                chk.Checked = false;
            }
        }
         catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.Clear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }

}
