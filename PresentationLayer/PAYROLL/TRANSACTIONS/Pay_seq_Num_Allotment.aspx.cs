//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_seq_Num_Allotment.aspx                                                  
// CREATION DATE : 13-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_Pay_seq_Num_Allotment : System.Web.UI.Page
{
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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                pnlSelect.Visible = true;
                pnlSeqNum.Visible = false;
                btnCancel.Visible = false;
                btnSave.Visible = false;
                FillDropdown();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_seq_Num_Allotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_seq_Num_Allotment.aspx");
        }
    }

    private void BindListViewList(Int32 staffno)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {
                pnlSeqNum.Visible = true;
                string orderby;

                if (ddlorderby.SelectedValue == "0")
                {
                    orderby = "IDNO";
                }
                else
                {
                    if (ddlorderby.SelectedValue == "1")
                        orderby = "IDNO";
                    else
                        orderby = "SEQ_NO";

                }
                pnlSeqNum.Visible = true;
                int deptno = Convert.ToInt32(ddldept.SelectedValue);
                int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
                int EmpTypeNo = Convert.ToInt32(ddlEmpType.SelectedValue);
                DataSet ds = objpay.GetEmpByStaffno(staffno, ddlorderby.SelectedItem.Text, deptno, collegeno, EmpTypeNo);             
                lvSeqNum.DataSource = ds;
                lvSeqNum.DataBind();
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    btnCancel.Visible = false;
                    btnSave.Visible = false;

                    objCommon.DisplayMessage(UpdatePanel1, "No Record found for Selection", this);
                }
                else
                {
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }
              
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_seq_Num_Allotment.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page,"Server UnAvailable");
        }
    }


    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.EnableDisableButton();
        this.enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.EnableDisableButton();
        this.enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void enablelistview(int index)
    {
        if (!(Convert.ToInt32(index) == 0))
        {

            BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()));
        }
        else
        {
            pnlSeqNum.Visible = false;
        }
    
    }


    
    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            
                      
            foreach (ListViewDataItem lvitem in lvSeqNum.Items)
            {
                TextBox txt = lvitem.FindControl("TxtSqeNo") as TextBox;
                
                CustomStatus cs = (CustomStatus)objpay.UpdateEmpmasPaymasSeqNo(Convert.ToInt32(txt.Text), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
            }
            
            
            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
               objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
            }

            enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    
   protected void btnCancel_Click(object sender, EventArgs e)
   {

       foreach (ListViewDataItem lvitem in lvSeqNum.Items)
       {
           TextBox txt = lvitem.FindControl("TxtSqeNo") as TextBox;
           txt.Text = string.Empty;
       }
       ddlStaff.SelectedIndex = 0;
       ddlCollege.SelectedIndex = 0;
       ddlorderby.SelectedIndex = 0;
       ddldept.SelectedIndex = 0;
       ddlEmpType.SelectedIndex = 0; //23-09-2022
       lblerror.Text = string.Empty;
       lblmsg.Text = string.Empty;
       pnlSeqNum.Visible = false;
       
    }

    protected void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlEmpType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO > 0", "EMPTYPENO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void EnableDisableButton()
    {
        if (ddlStaff.SelectedValue == "0")
        {
            btnCancel.Visible = false;
            btnSave.Visible = false;
        }
        else
        {
            btnCancel.Visible = true;
            btnSave.Visible = true;
        }
    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.EnableDisableButton();
        this.enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    
    {
        this.EnableDisableButton();
        this.enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.EnableDisableButton();
        this.enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }
}
