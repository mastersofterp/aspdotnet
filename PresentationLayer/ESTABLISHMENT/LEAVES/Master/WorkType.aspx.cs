//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : Pay_HolidayType.aspx                                                   
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

public partial class ESTABLISHMENT_LEAVES_Master_WorkType : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objHoliType = new LeavesController();

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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;

                BindListViewWorkType();
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
            }

        }
    }

    protected void BindListViewWorkType()
    {
        try
        {
            DataSet ds = objHoliType.GetAllWorkType();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                dpPager.Visible = false;
            }
            else
            {
               // btnShowReport.Visible = true;
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
        BindListViewWorkType();
    }

    private void Clear()
    {
        txtWorkType.Text = string.Empty;

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
            Leaves objLeaves = new Leaves();
            objLeaves.WORKTYPE = Convert.ToString(txtWorkType.Text);

            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objHoliType.AddWorkType(objLeaves);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewWorkType();
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["WTNO"] != null)
                    {
                        objLeaves.WTNO = Convert.ToInt32(ViewState["WTNO"].ToString());
                        CustomStatus cs = (CustomStatus)objHoliType.UpdateWorkType(objLeaves);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewWorkType();
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
            int HTNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(HTNO);
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
            int WTNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objHoliType.DeleteWorkType(WTNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                BindListViewWorkType();
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

    private void ShowDetails(Int32 WTNO)
    {
        DataSet ds = null;
        try
        {
            ds = objHoliType.GetSingWorkType(WTNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["WTNO"] = WTNO;
                txtWorkType.Text = ds.Tables[0].Rows[0]["WORKTYPE"].ToString();

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
