//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Dept_Register.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Stores_Masters_Str_Dept_Register : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();
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
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillDept();
                BindListViewDeptRegister();
                ViewState["action"] = "add";
            }
            //Set Report Parameters
            objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Department_Register_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    private void BindListViewDeptRegister()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllDeptRegister();
            lvDepartmentRegister.DataSource = ds;
            lvDepartmentRegister.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Register.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPTREGISTER", " count(*)", " mdno = " + Convert.ToInt32(ddlDepartment.SelectedValue) + " and drsrno= " + Convert.ToInt32(txtSrNo.Text)+" and drname='"+txtRegisterName.Text+"' " ));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddDeptRegister(txtRegisterName.Text, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToDecimal(txtSrNo.Text), Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Save Successfully", this);
                            this.BindListViewDeptRegister();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist", this);
                    }
                }
                else
                {
                    if (ViewState["drNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPTREGISTER", " count(*)", " mdno =" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and drname='"+txtRegisterName.Text+ "' or drsrno=" + Convert.ToInt32(txtSrNo.Text) +"  and drNo<>" + Convert.ToInt32(ViewState["drNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateDeptRegister(txtRegisterName.Text, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToDecimal(txtSrNo.Text), Session["colcode"].ToString(), Convert.ToInt32(ViewState["drNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                objCommon.DisplayMessage(UpdatePanel1, "Record Update Successfully", this);
                                this.BindListViewDeptRegister();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist", this);
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Register.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["drNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsDeptRegister(Convert.ToInt32(ViewState["drNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Register.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowEditDetailsDeptRegister(int drNo)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordDeptRegister(drNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.SelectedValue=ds.Tables[0].Rows[0]["MDNO"].ToString();
                txtRegisterName.Text = ds.Tables[0].Rows[0]["DRNAME"].ToString();
                txtSrNo.Text =Convert.ToInt32(ds.Tables[0].Rows[0]["DRSRNO"]).ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Register.ShowEditDetailsDeptRegister-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void FillDept()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Register.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    

    protected void butCancel_Click(object sender, EventArgs e)
    {
        //this.Clear();
        Response.Redirect(Request.Url.ToString());
    }


    protected void Clear()
    {
        txtRegisterName.Text = string.Empty;
        txtSrNo.Text = string.Empty;
        ddlDepartment.SelectedValue = "0";
        ViewState["action"] = "add";
    }


    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewDeptRegister();
    }
}