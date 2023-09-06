//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Head_PrivilegeToUser.ASPX                                                    
// CREATION DATE : 24-JULY-2009                                                        
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

public partial class PAYROLL_MASTERS_Pay_NoduesAuthorityDetail : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    PayHeadPrivilegesController objPayHeadPrivilege = new PayHeadPrivilegesController();

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
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                FillUser();
                pnlButton.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_NoduesAuthorityDetail.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_NoduesAuthorityDetail.aspx");
        }
    }

    private void BindListViewPayHead(int uaNo)
    {
        try
        {
            DataSet ds = objPayHeadPrivilege.GetAllAuthoName(uaNo);
         

            if (ds.Tables[0].Rows.Count <= 0)
                pnlPayhead.Visible = false;
            else
            {
                pnlPayhead.Visible = true;
                lvAutho.DataSource = ds;
                lvAutho.DataBind();

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    CheckBox ChkPayHead = lvAutho.Items[i].FindControl("ChkPayHead") as CheckBox;

                    bool payHead =Convert.ToBoolean(ds.Tables[0].Rows[i]["ISACTIVE"].ToString());
                    if (payHead == true)
                    {
                        ChkPayHead.BackColor = System.Drawing.Color.Gold;
                        ChkPayHead.Checked = true;
                    }
                   

                }



            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.BindListViewPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  

    protected void FillUser()
    {
        int linkno;

        linkno = GetLinkNo();

        if (!(linkno == 0))
        {
            pnluser.Visible = true;

            try
            {

                objCommon.FillDropDownList(ddlAuthoName, "PAYROLL_NODUES_AUTHORITY_TYPE", "AUTHO_TYP_ID", "AUTHORITY_TYP_NAME", "", "AUTHO_TYP_ID");
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.FillUser-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        else
        {
            pnluser.Visible = false;

            lblerror.Text = "Please Assign Module and link in Pay Ref Table";

        }
    }

    protected int GetLinkNo()
    {
        int linkno = 0;

        try
        {
            linkno = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAY_REF", "isnull(link_no,0)", ""));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.GetLinkNo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

        return linkno;
    }

    protected void ddlAuthoName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;

        if (!(ddlAuthoName.SelectedValue == "0"))
        {
            int uaNo = Convert.ToInt32(ddlAuthoName.SelectedValue);
            pnlPayhead.Visible = true;
            //PnlUserHead.Visible = true;
            pnlButton.Visible = true;
            BindListViewPayHead(uaNo);
            //BindListViewUser(uaNo);
        }
        else
        {
            pnlPayhead.Visible = false;
            // PnlUserHead.Visible = false;
            pnlButton.Visible = false;
        }
    }

    //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            int uaNo = Convert.ToInt32(ddlAuthoName.SelectedValue);
            DataTable CellNumTbl = new DataTable("AuthoTbl");

            CellNumTbl.Columns.Add("AUTHO_ID", typeof(int));
            CellNumTbl.Columns.Add("IDNO", typeof(int));
            CellNumTbl.Columns.Add("ISACTIVE", typeof(int));


            DataRow dr = null;

            foreach (ListViewDataItem i in lvAutho.Items)
            {
                int pay = 0;
                CheckBox ChkPay = (CheckBox)i.FindControl("ChkPayHead");


                if (ChkPay.Checked == true) { pay = 1; } else { pay = 0; }
                dr = CellNumTbl.NewRow();

                dr["AUTHO_ID"] = Convert.ToInt32(ddlAuthoName.SelectedValue);
                dr["IDNO"] = Convert.ToInt32(ChkPay.ToolTip);
                dr["ISACTIVE"] = pay;
              

                CellNumTbl.Rows.Add(dr);

            }

            CustomStatus cs = (CustomStatus)objPayHeadPrivilege.AddAuthoDetails(CellNumTbl);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Saved Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
            }


            BindListViewPayHead(uaNo);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAuthoName.SelectedValue = "0";
        pnlPayhead.Visible = false;
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            CustomStatus cs = (CustomStatus)objPayHeadPrivilege.DeleteUser(Convert.ToInt32(ddlAuthoName.SelectedValue));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                //lblmsg.Text = "Record Deleted Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Deleted Successfully", this);
                BindListViewPayHead(Convert.ToInt32(ddlAuthoName.SelectedValue));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}