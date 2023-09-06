//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DEFINE COUNTER
// CREATION DATE : 24-APRIL-2014
// CREATED BY    : IFTAKHAR KHAN
// MODIFIED BY   : SNEHAL SAHARE
// MODIFIED DATE : 19-MAY-2014
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class HOSTEL_HostelMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelController objCHC = new HostelController();
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
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
                    //{
                    //    divhostel.Visible = false;
                    //}
                }
                objCommon.FillDropDownList( ddlDegree,"ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                DataSet dsDegree = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORYNO");
                ChkblstCategory.DataSource = dsDegree;
                ChkblstCategory.DataTextField = "CATEGORY";
                ChkblstCategory.DataValueField = "CATEGORYNO";
               ChkblstCategory .DataBind();
               DataSet dsYear = objCommon.FillDropDown("ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0 AND ACTIVESTATUS=1", "YEAR ");
               ChkBlstYear.DataSource = dsYear;
               ChkBlstYear.DataTextField = "YEARNAME";
               ChkBlstYear.DataValueField = "YEAR";
               ChkBlstYear.DataBind();

                
            }
            // Set form add as add on first time form load.
            //ViewState["add"] = "0";
            //ViewState["DELETE"] = 0; // further used to insert or update record 
            BindData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
        }
    }
    #endregion
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)      //MODIFICATION DONE BY SNEHAL SAHARE
    {
        string hostelno = string.Empty;

        string yearno = string.Empty;
        string categoryno = string.Empty;
        Boolean category = false;
        Boolean year = false;

        foreach (ListItem li in ChkblstCategory.Items)
        {
            if (li.Selected == true)
            {
                category = true;
                if (!categoryno.Equals(string.Empty))
                {
                    categoryno = categoryno + ',' + li.Value;
                }
                else
                {
                    categoryno = li.Value;
                }
            }
        }
        foreach (ListItem li in ChkBlstYear.Items)
        {
            if (li.Selected == true)
            {
                year = true;
                if (!yearno.Equals(string.Empty))
                {
                    yearno = yearno + ',' + li.Value;
                }
                else
                {
                    yearno = li.Value;
                }
            }
        }

        if (ViewState["hostelno"] == null)
        {
            hostelno = "0";
            if (hostelno == "0")
            {
         //     int Count = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL", "COUNT(1)", "HOSTEL_NAME LIKE '" + txtHostelName.Text.Trim() + "' AND HOSTEL_ADDRESS LIKE '" + txtAddress.Text.Trim() + "' AND HOSTEL_TYPE=" + rdoGender.SelectedValue));

                // Condition for only check same hostel name the existing hostel details by shubham on 27/09/2022
                int Count = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL", "COUNT(HOSTEL_NO)", "HOSTEL_NAME LIKE '" + txtHostelName.Text.Trim() + "'"));
                if (Count == 0)
                {

                    int output = objCHC.AddHostelDetail(Convert.ToInt32(ddlDegree.SelectedValue), yearno,categoryno,txtHostelName.Text, txtAddress.Text, Convert.ToInt32(rdoGender.SelectedValue), Convert.ToInt32(Session["colcode"]));
                    objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                    BindData();
                    clear();
                }
                else
                {
                    objCommon.DisplayMessage("Hostel Name Already Exist.", this.Page);
                    txtHostelName.Text = string.Empty;
                    txtAddress.Text = string.Empty;
                   
                    return;
                }
            }
        }
        else 
        {
            // Condition Comment for edit the existing hostel details by shubham on 27/09/2022
          //int Count = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL", "COUNT(1)", "HOSTEL_NAME LIKE '" + txtHostelName.Text.Trim() + "' AND HOSTEL_ADDRESS LIKE '" + txtAddress.Text.Trim() + "' AND HOSTEL_TYPE=" + rdoGender.SelectedValue));

            //Below if-else condition uncommented and added "HOSTEL_NO !=" +  Convert.ToInt32(ViewState["hostelno"]) by Saurabh L on 05/07/2023 Ticket No:45352 AND  BUGID:163298
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL", "COUNT(HOSTEL_NO)", "HOSTEL_NO !=" + Convert.ToInt32(ViewState["hostelno"]) + " AND   HOSTEL_NAME LIKE '" + txtHostelName.Text.Trim() + "'"));
            if (Count == 0)
            {
                int output = objCHC.UpdateHosteDetail(Convert.ToInt32(ddlDegree.SelectedValue), yearno, categoryno, txtHostelName.Text, txtAddress.Text, Convert.ToInt32(rdoGender.SelectedValue), Convert.ToInt32(ViewState["hostelno"]));

                //int output = objCHC.UpdateHosteDetail(txtHostelName.Text, txtAddress.Text, Convert.ToInt32(rdoGender.SelectedValue), Convert.ToInt32(ViewState["hostelno"]));

                objCommon.DisplayMessage("Data Updated Successfully.", this.Page);
                BindData();
                clear();
            }
            else
            {
                objCommon.DisplayMessage("Record Already Exist With Same Hostel Name.", this.Page);
                txtHostelName.Text = string.Empty;
                txtAddress.Text = string.Empty;

                return;

            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int hostelNo = Int32.Parse(editButton.CommandArgument);
            ViewState["hostelno"] = hostelNo;
            string Hostel_No =objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "HOSTEL_NO", "HOSTEL_NO=" + hostelNo);
            if (Hostel_No == "")
            {
                DataSet dt = objCommon.FillDropDown("ACD_HOSTEL", "HOSTEL_NO", "DEGREENO,YEARNO,CATEGORYNO,HOSTEL_NAME,HOSTEL_ADDRESS,HOSTEL_TYPE", "HOSTEL_NO=" + hostelNo, "");
                BindControl(dt.Tables[0].Rows[0]);
            }
            else
            {
                objCommon.DisplayMessage("Record is in Use. So it can not Edit or Update...", this.Page);
                ViewState["hostelno"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "MaleFemaleHostelEntry.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #region Private Methods
    private void BindControl(DataRow dr)
    {
        ddlDegree.SelectedValue = dr["DEGREENO"].ToString();      
        txtHostelName.Text = dr["HOSTEL_NAME"].ToString();
        txtAddress.Text = dr["HOSTEL_ADDRESS"].ToString();
        rdoGender.SelectedValue = dr["HOSTEL_TYPE"].ToString();
        string [] yearnolist = dr["YEARNO"].ToString().Split(',');
        string[] categorynolist = dr["CATEGORYNO"].ToString().Split(',');

        foreach (string yearno in yearnolist)
        {

            foreach (ListItem li in ChkBlstYear.Items)
            {


                if (li.Value ==yearno)
                {
                    li.Selected = true;
                }
            }
        }
        foreach (string categoryno in categorynolist)
        {

            foreach (ListItem li in ChkblstCategory.Items)
            {


                if (li.Value == categoryno)
                {
                    li.Selected = true;
                }
            }
        }



    }
    private void BindData()

    {
       // DataSet ds = objCommon.FillDropDown("ACD_HOSTEL", "HOSTEL_NO", "dbo.FN_DESC('DEGREENAME',DEGREENO)AS DEGREE,HOSTEL_NAME,HOSTEL_ADDRESS,(CASE HOSTEL_TYPE WHEN  1 THEN 'MALE'  WHEN 2 THEN 'FEMALE' END)HOSTEL_TYPE", "HOSTEL_NO >0", "");
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL", "HOSTEL_NO", "dbo.FN_DESC('DEGREENAME',DEGREENO)AS DEGREE,HOSTEL_NAME,HOSTEL_ADDRESS,(CASE HOSTEL_TYPE WHEN  1 THEN 'MALE'  WHEN 2 THEN 'FEMALE' END)HOSTEL_TYPE", "HOSTEL_NO >0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), ""); // OrganizationId added by Saurabh L on 23/05/2022
        lvSession.DataSource = ds;
        lvSession.DataBind();
        ds.Dispose();
    }
    #endregion
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int hostelNo = Int32.Parse(btnDelete.CommandArgument);
        int retstatus = objCHC.DeleteHostelDetail(hostelNo);
        if (retstatus == 3)
        {
            objCommon.DisplayMessage("Record Delete Successfully.", this.Page);
            BindData();
        }
        else
        {
            objCommon.DisplayMessage("Record Not Deleted Because Hostel is alloted.", this.Page);
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void clear()
    {
        ddlDegree.SelectedValue = "0";        
        txtAddress.Text = string.Empty;
        txtHostelName.Text = string.Empty;
        rdoGender.SelectedIndex = 0;
        ViewState["hostelno"] = null;
        foreach (ListItem li in ChkBlstYear.Items)
        {
            if (li.Selected == true)
            {
                li.Selected = false;
            }
        }
        foreach (ListItem li in ChkblstCategory.Items)
        {
            if (li.Selected == true)
            {
                li.Selected = false;
            }
        }
        foreach (ListItem li in rdoGender.Items)
        {
            if (li.Selected == true)
            {
                li.Selected = false;
            }
        }      
    }
}
