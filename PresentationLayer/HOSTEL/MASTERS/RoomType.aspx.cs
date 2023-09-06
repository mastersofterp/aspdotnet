using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class HOSTEL_MASTERS_RoomType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                BindListView();
                PopulateDropDownList();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_RoomType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RoomType.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RoomType.aspx");
        }
    }
    #endregion Page Events

    private void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN HOSTEL NAME
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0 ", "HOSTEL_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RoomType.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {

            string TYNO = objCommon.LookUp("ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME='" + txtRoomtypeName.Text.Trim() + "'  and  HOSTEL_NO ='" + Convert.ToInt32(ddlHostel.SelectedValue) + "'");
            if (TYNO != null && TYNO != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RoomType.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int TYPE_NO)
    {
        bool flag = false;
        try
        {
            string TYNO = objCommon.LookUp("ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "HOSTEL_NO=" + ddlHostel.SelectedValue + " AND ROOMTYPE_NAME='" + txtRoomtypeName.Text.Trim() + "' and TYPE_NO != " + TYPE_NO.ToString() + " ");
            if (TYNO != null && TYNO != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RoomType.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {

            string CollegeCode = Session["colcode"].ToString();
            int HostelNo = Convert.ToInt32(ddlHostel.SelectedValue);
            string RoomtypeName = txtRoomtypeName.Text.Trim();
            //int typeno  = Convert.ToInt32(ViewState["TYPE_NO"].ToString());
            int typeno = 0;
            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);

                        return;

                    }
                    //Add Block
                    CustomStatus cs = (CustomStatus)AddRoomType(RoomtypeName, HostelNo, CollegeCode, typeno);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                        BindListView();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["TYPE_NO"] != null)
                    {
                         typeno  = Convert.ToInt32(ViewState["TYPE_NO"].ToString());
                        if (CheckDuplicateEntryUpdate(typeno) == true)
                        {
                            objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                            return;
                        }

                        //if (CheckDuplicateEntry() == true)
                        //{
                        //    objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                        //    ViewState["action"] = "add";
                        //    Clear();
                        //    return;

                        //}

                        CustomStatus cs = (CustomStatus)UpdateRoomType(RoomtypeName, HostelNo, typeno);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                            BindListView();
                        }
                      
                    }
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_RoomType.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int TYPENO = int.Parse(btnEdit.CommandArgument);

            ShowDetail(TYPENO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_RoomType.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public int AddRoomType(String RoomType, int hostelno, string collegecode, int typeno)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others) ;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;

            //Add Block info 
            objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_ROOMTYPE_NAME",RoomType);
            objParams[1] = new SqlParameter("@P_HOSTEL_NO",hostelno);
            objParams[2] = new SqlParameter("@P_COLLEGE_CODE",collegecode);
            objParams[3] = new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //OrganizationId  added by Shubham on 14072022
            objParams[4] = new SqlParameter("@P_TYPE_NO", typeno); 
            objParams[4].Direction = ParameterDirection.Output;

            if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOMTYPE_INSERT", objParams, false) != null)
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.AddBlock-> " + ex.ToString());
        }
        return retStatus;
    }

    public int UpdateRoomType(String RoomType, int hostelno, int typeno)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others) ;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;

            objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@P_ROOMTYPE_NAME", RoomType);
            objParams[1] = new SqlParameter("@P_HOSTEL_NO", hostelno);
            objParams[2] = new SqlParameter("@P_TYPE_NO", typeno);
            //objParams[2].Direction = ParameterDirection.Output;

            if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOMTYPE_UPDATE", objParams, false) != null)
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.AddBlock-> " + ex.ToString());
        }
        return retStatus;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACD_HOSTEL_ROOMTYPE_MASTER HM WITH(NOLOCK) INNER JOIN ACD_HOSTEL H WITH(NOLOCK) ON (HM.HOSTEL_NO=H.HOSTEL_NO)", "HM.TYPE_NO", "HM.ROOMTYPE_NAME, H.HOSTEL_NAME", "HM.TYPE_NO > 0 AND HM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "HM.TYPE_NO");

            if (ds != null)
            {
                if ((ds.Tables[0].Rows.Count > 0))
                {

                    lvBlock.DataSource = ds;
                    lvBlock.DataBind();
                }
                else
                {
                    lvBlock.DataSource = null;
                    lvBlock.DataBind();

                }
            }
            else
            {
                lvBlock.DataSource = null;
                lvBlock.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_RoomType.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int TYPENO)
    {

        SqlDataReader dr = this.GetRoomType(TYPENO);

        //Show Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["TYPE_NO"] = TYPENO.ToString();
                ddlHostel.SelectedValue = dr["HOSTEL_NO"].ToString();
                txtRoomtypeName.Text = dr["ROOMTYPE_NAME"] == null ? string.Empty : dr["ROOMTYPE_NAME"].ToString();

                if (dr["HOSTEL_NO"].ToString() != null)
                    ddlHostel.SelectedValue = dr["HOSTEL_NO"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    public SqlDataReader GetRoomType(int TYPENO)
    {
        SqlDataReader dr = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_TYPE_NO", TYPENO);

            dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_ROOMTYPE_GET_BY_ID", objParams);
        }
        catch (Exception ex)
        {
            return dr;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetBlockType-> " + ex.ToString());
        }
        return dr;
    }

    private void Clear()
    {
        txtRoomtypeName.Text = string.Empty;
        ddlHostel.SelectedIndex = 0;
        ViewState["action"] = "add";
        BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
}