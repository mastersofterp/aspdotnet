//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : Pay_Status.aspx                                                   
// CREATION DATE : 20 Aug 2012
// CREATED BY    : Mrunal Bansod                                       
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

public partial class PAYROLL_MASTERS_Pay_EmpStatus : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController  objStatus = new PayController();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                //Page Authorization
                CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;

                BindListViewStatus();
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
                objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmpStatus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmpStatus.aspx");
        }
    }
    protected void BindListViewStatus()
    {
        try
        {
            DataSet ds = objStatus.GetAllStatus();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
                dpPager.Visible = true;
            }
            lvWorkType.DataSource = ds;
            lvWorkType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListViewStatus();
    }

    private void Clear()
    {
        txtStatus.Text = string.Empty;

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;

        ViewState["action"] = "add";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Payroll objStat = new Payroll();
            objStat.STATUS = Convert.ToString(txtStatus.Text);

            objStat.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objStatus.AddStatus(objStat);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewStatus();
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["STATUSNO"] != null)
                    {
                        objStat.STATUSNO = Convert.ToInt32(ViewState["STATUSNO"].ToString());
                        CustomStatus cs = (CustomStatus)objStatus.UpdateStatus(objStat);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewStatus();
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int STATUSNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(STATUSNO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int STATUSNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objStatus.DeleteStatus(STATUSNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                BindListViewStatus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetails(Int32 STATUSNO)
    {
        DataSet ds = null;
        try
        {
            ds = objStatus.GetSingleStatus(STATUSNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["STATUSNO"] = STATUSNO;
                //txtStatus.Text = ds.Tables[0].Rows[0]["WORKTYPE"].ToString();
                txtStatus.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}
