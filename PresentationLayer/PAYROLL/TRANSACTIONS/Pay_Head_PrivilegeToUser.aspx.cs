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
using IITMS.SQLServer.SQLDAL;

public partial class PayRoll_Pay_Head_PrivilegeToUser : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    PayHeadPrivilegesController objPayHeadPrivilege = new PayHeadPrivilegesController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


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
              //  CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Head_PrivilegeToUser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Head_PrivilegeToUser.aspx");
        }
    }


    private void BindListViewPayHead(int uaNo)
    {
        try
        {
            DataSet ds = objPayHeadPrivilege.GetAllPayHead();
            DataSet dsEdit = objPayHeadPrivilege.EditPayHeadUser(uaNo);

            if (ds.Tables[0].Rows.Count <= 0)
                pnlPayhead.Visible = false;
            else
            {
                pnlPayhead.Visible = true;
                lvPayhead.DataSource = ds;
                lvPayhead.DataBind();

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    CheckBox ChkPayHead = lvPayhead.Items[i].FindControl("ChkPayHead") as CheckBox;


                    for (int j = 0; j <= dsEdit.Tables[0].Rows.Count - 1; j++)
                    {   
                        string payHead =ds.Tables[0].Rows[i]["PAYHEAD"].ToString();
                        string payHeadUser = dsEdit.Tables[0].Rows[j]["PAYHEAD"].ToString();

                        if (payHead == payHeadUser)
                        {
                            ChkPayHead.BackColor =  System.Drawing.Color.Gold;
                            ChkPayHead.Checked = true;
                        }
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

    //private void BindListViewUser(int uaNo)
    //{
    //    try
    //    {
    //        DataSet ds = objPayHeadPrivilege.GetPayHeadUser(uaNo);
    //        if (ds.Tables[0].Rows.Count <= 0)
    //            PnlUserHead.Visible = true;
    //        else
    //        {
    //            PnlUserHead.Visible = true;
    //            lvUser.DataSource = ds;
    //            lvUser.DataBind();
    //            ds.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_Pay_Head_PrivilegeToUser.BindListViewAppoint-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void FillUser()
    {   
        int linkno;
        DataSet ds;
        linkno=GetLinkNo();
        if (!(linkno == 0))
        {
            pnluser.Visible = true;
            try
            {
                //objCommon.FillDropDownList(ddlUserName,"user_acc","ua_no","ua_fullname","ua_acc like '%"+linkno+"%'","ua_fullname");
                ds = GetRolebasedEmployeeUserName(linkno);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlUserName.DataSource = ds;
                        ddlUserName.DataTextField = "ua_fullname";
                        ddlUserName.DataValueField = "UA_NO";
                        ddlUserName.DataBind();
                        //ddlpreviousvisitedyear.Items.Insert(0, new ListItem("--Select Year--", "0"));
                        ds.Dispose();
                    }
                }
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
        int linkno=0;

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


    // Bulk Employee Salary Process
    public DataSet GetRolebasedEmployeeUserName(int LinkNo)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_LINK_NO", LinkNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_ROLE_BASED_USERNAME", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIncrementEmployees-> " + ex.ToString());
        }
        return ds;
    }
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;

        if (!(ddlUserName.SelectedValue == "0"))
        {
            int uaNo = Convert.ToInt32(ddlUserName.SelectedValue);
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
            string colvalues;
            int checkcount = 0;
            int uaNo=Convert.ToInt32(ddlUserName.SelectedValue);
            colvalues = string.Empty;
            
            foreach (ListViewDataItem lvitem in lvPayhead.Items)
            {
                CheckBox chk = lvitem.FindControl("ChkPayHead") as CheckBox;

                if (chk.Checked)
                {
                    checkcount = 1;
                    colvalues = colvalues + chk.ToolTip + ",";
                }
               
            }

                //Atleast one checkbox to be checked 
                if (checkcount == 1)
                {
                    string colval = colvalues.Substring(0, colvalues.Length - 1);

                    //checkUser 

                    Int32 checkUser = Convert.ToInt32(objCommon.LookUp("PAYROLL_ACCESS_PAYHEAD", "count(*)", "ua_no=" + uaNo));
                    if (checkUser <= 0)
                    {
                        CustomStatus cs = (CustomStatus)objPayHeadPrivilege.AddUser(uaNo, colval, Session["colcode"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //lblerror.Text = null;
                            //lblmsg.Text = "Record Saved Successfully";
                            objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objPayHeadPrivilege.DeleteUser(uaNo);
                        if (cs.Equals(CustomStatus.RecordDeleted))
                        {
                            CustomStatus csadd = (CustomStatus)objPayHeadPrivilege.AddUser(uaNo, colval, Session["colcode"].ToString());
                            if (csadd.Equals(CustomStatus.RecordSaved))
                            {
                                //lblerror.Text = null;
                                //lblmsg.Text = "Record Saved Successfully";
                                objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                            }
                           
                         }
                    }
                }
                else
                {
                    //lblmsg.Text = null;
                    //lblerror.Text = "Check at least one payhead";
                    objCommon.DisplayMessage(UpdatePanel1, "Check at least one payhead", this);
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
        if (ddlUserName.SelectedIndex > 0)
        {
            ddlUserName.SelectedIndex = 0;
        }
        else
        {
            ddlUserName.SelectedIndex = 0;
        }
      //  ddlUserName.SelectedValue = "0";
        pnlPayhead.Visible = false;
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            
            CustomStatus cs = (CustomStatus)objPayHeadPrivilege.DeleteUser(Convert.ToInt32(ddlUserName.SelectedValue));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                //lblmsg.Text = "Record Deleted Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Deleted Successfully",this);
                BindListViewPayHead(Convert.ToInt32(ddlUserName.SelectedValue));
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
