
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ESTABLISHMENT;

public partial class ESTABLISHMENT_LEAVES_Transactions_ELEnCashment_Apply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EnCashmentControllar objEnCash = new EnCashmentControllar();
    Leaves objLeaveMaster = new Leaves();
  
    string ipAddress = string.Empty;

    #region Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                Page.Title = Session["coll_name"].ToString();
                pnlAdd.Visible = true;
                ViewState["action"] = "add";
                PanelList.Visible = true;
                txtApplyDt.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ipAddress = Request.ServerVariables["REMOTE_HOST"];
                Session["ipAddress"] = ipAddress;
               BindListView();
            }
           
        }
    }

    #endregion

    #region Page Event

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtApplyDt.Text) > DateTime.Now)
        {
            objCommon.DisplayMessage(updAll, "Date greater than current date", this.Page);
            return;
        }
        else
        {
            objLeaveMaster.APPDT = Convert.ToDateTime(txtApplyDt.Text);
            objLeaveMaster.NO_DAYS = Convert.ToDouble(txtEnCashDays.Text);
            objLeaveMaster.EMPNO = Convert.ToInt32(Session["idno"].ToString());
            objLeaveMaster.IPADDRESS = Convert.ToString(Session["ipAddress"].ToString());
            objLeaveMaster.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objLeaveMaster.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            //objLeaveMaster.TRANNO = Convert.ToInt32(Session["userno"]);
            objLeaveMaster.IsActive = Convert.ToBoolean(chkIsActive.Checked);
            objLeaveMaster.LNO      = Convert.ToInt32(ViewState["LNO"].ToString());  
            objLeaveMaster.LEAVENO  = Convert.ToInt32(ViewState["LEAVENO"].ToString());  
            objLeaveMaster.PERIOD     =  Convert.ToInt32(  ViewState["PERIOD"].ToString());
            objLeaveMaster.SESSION_SRNO= Convert.ToInt32(ViewState["SESSION_SRNO"].ToString());   

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objLeaveMaster.TRANNO = Convert.ToInt32(ViewState["SRNO"]);

                CustomStatus cs = (CustomStatus)objEnCash.AddUpdateElEnCashment(objLeaveMaster);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                  //  objCommon.DisplayMessage(updAll, "Record Updated Successfully");
                    objCommon.DisplayMessage(updAll, "Record Updated Successfully", this.Page);
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(updAll, "Sorry! Record Already Exist", this.Page);

                }
                Clear();
            }
            else
            {
                CustomStatus cs = (CustomStatus)objEnCash.AddUpdateElEnCashment(objLeaveMaster);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updAll, "Record Saved Successfully", this.Page);
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(updAll, "Sorry! Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage("Record Not Saved", this.Page);
                }
            }
            Clear();
            BindListView();

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int SRNO = int.Parse(btnEdit.CommandArgument);
        showDetails(SRNO);
        ViewState["SRNO"] = SRNO;
        ViewState["action"] = "edit".ToString().Trim();
    }

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    ImageButton btnDelete = sender as ImageButton;
    //    int SRNO = int.Parse(btnDelete.CommandArgument);
    //    objLeaveMaster.TRANNO = SRNO;
    //    string status=Convert.ToString( objCommon.LookUp("PAYROLL_LEAVE_EL_ENCASH", "STATUS", "SRNO=" + SRNO + ""));
    //    if (status == "A")
    //    {
    //        MessageBox("Already Approved. Not Allow To Delete ");
    //        return;
    //    }
    //    //if (Convert.ToInt32(objEnCash.DeleteEncashData(objLeaveMaster)) == Convert.ToInt32(CustomStatus.RecordDeleted))
    //    int ret = Convert.ToInt32(objEnCash.DeleteEncashData(objLeaveMaster));
    //    if(ret==Convert.ToInt32(CustomStatus.RecordDeleted))
    //    {
    //        objCommon.DisplayMessage(updAll, "Record Deleted Successfully", this.Page);
    //        BindListView();
    //    }

      
    //}

    #endregion

    # region Private Method

    private void Clear()
    {
        //txtApplyDt.Text = DateTime.Now.ToString();
        txtEnCashDays.Text = string.Empty;
    }

    private void showDetails(int SR_NO)
    {
        DataSet ds = objEnCash.GetSingleEncashmentData(SR_NO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A")
            {
                MessageBox("Already Approved. Not Allow To Edit ");
                return;
            }
            txtApplyDt.Text= ds.Tables[0].Rows[0]["APPDT"].ToString();
            txtEnCashDays.Text = ds.Tables[0].Rows[0]["TOTAL_DAYS"].ToString();
            bool isactive =Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString());
            if (isactive == true)
            {
                chkIsActive.Checked = true;
            }
            else
            {
                chkIsActive.Checked = false;
            }

        }

    }

    private void BindListView()
    {
        lblYear.Text = System.DateTime.Now.Year.ToString();
        lblYearPending.Text = System.DateTime.Now.Year.ToString();
        LeavesController objApp = new LeavesController();
        try
        {
           int idno= Convert.ToInt32(Session["idno"]);
            DataSet ds = objEnCash.GetAllEncashmentData(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEnCashment.DataSource = ds;
                lvEnCashment.DataBind();
                DivNote.Visible = false;
            }
            else
            {
                lvEnCashment.DataSource = null;
                lvEnCashment.DataBind();
                DivNote.Visible = true;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                txtTotDays.Text = ds.Tables[1].Rows[0]["total_days"].ToString();
            }
            //txtPendingEncashment
            if (ds.Tables[2].Rows.Count > 0)
            {
                txtPendingEncashment.Text = ds.Tables[2].Rows[0]["PendingEncashment"].ToString();
            }

            ds = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), System.DateTime.Now.Year, 0);//Session["idno"]
            txtELBalance.Text = string.Empty;
            string leave_name = "EL"; int lno = 0; double Pending_count = 0,bal=0;
            if (ds.Tables[0].Rows.Count > 0)
            {
            
             
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (leave_name == ds.Tables[0].Rows[i]["LEAVE"].ToString())
                    {
                        bal = Convert.ToDouble(ds.Tables[0].Rows[i]["BAL"].ToString());
                       // txtELBalance.Text = ds.Tables[0].Rows[i]["BAL"].ToString();
                        lno =Convert.ToInt32( ds.Tables[0].Rows[i]["LNO_new"].ToString());
                        
                        ViewState["LNO"] = lno.ToString();
                        ViewState["LEAVENO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["LNO"].ToString());
                        ViewState["PERIOD"] = Convert.ToInt32(ds.Tables[0].Rows[i]["PERIOD_NO"].ToString());
                        ViewState["SESSION_SRNO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["SESSION_SRNO"].ToString());
                        
                        break;
                    }
                }
            }
            if (bal<=0)
            {
                MessageBox("Sorry ! EL Balance Not Found!");
            }
            else
            {

                DataSet dsValidate = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "SUM(NO_OF_DAYS) AS NO_OF_DAYS ", "EMPNO", "STATUS IN('A','P','F') AND LNO=" + Convert.ToInt32(lno) + " AND ORDTRNO IS  NULL AND EMPNO=" + Convert.ToInt32(Session["idno"]) + "  AND SESSION_SRNO IN(SELECT SESSION_SRNO  FROM PAYROLL_LEAVE_SESSION S INNER JOIN PAYROLL_LEAVE L ON(L.LEAVENO=S.LEAVENO) where convert(date,getdate()) between S.FDT and S.TDT) GROUP BY EMPNO", "");
                if (dsValidate.Tables[0].Rows.Count > 0)
                {
                    Pending_count = Convert.ToDouble(dsValidate.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                    bal = bal - Pending_count;
                    //txtPending.Text = Pending_count.ToString();
                    //txtELBalance.Text = bal.ToString();
                  //  divPending.Visible = true;
                }
                txtELBalance.Text = bal.ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_ELEnCashment.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    #endregion
}