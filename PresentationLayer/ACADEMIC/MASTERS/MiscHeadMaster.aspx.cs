//CREATED BY - IFTAKHAR KHAN
//DATED - 27-MARCH-2014
//MODIFIED ON - 1-APRIL-2014
//APPROVED BY - PIYUSH R
//PURPOSE - THIS PAGE IS USED TO ENTER MASTER ENTRY OF THE MISCELANEOUS HEAD


using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_MASTERS_MiscHeadMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    CounterController counterController = new CounterController();
    FeeCollectionController ObjFCC = new FeeCollectionController();
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

                    // Fill Dropdown lists                
                    //this.objCommon.FillDropDownList(ddlCounterUser, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (1, 4)", "UA_FULLNAME");
                    objCommon.FillDropDownList(ddlMisc, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "BELONGS_TO='M'", "");
                    RecieptTypeController recieptTypeController = new RecieptTypeController();
                    DataSet ds = recieptTypeController.GetRecieptTypes();
                    ViewState["add"] = "0";
                    ViewState["action"] = "add";
                }
                this.ShowAllCounters();
            }
        }
        catch (Exception ex)
        {
            throw;
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
    private void clear()
    {
        ddlMisc.SelectedIndex = 0;
        txtHeadCode.Text = string.Empty;
        txtDescription.Text = string.Empty;
        ViewState["Edit"] = null;
    }

    //THIS IS ENTITY CLASS 
    private MiscFees binddatafromControl()
    {
        MiscFees ObjE = new MiscFees();
        FeeCollectionController ObjFCC = new FeeCollectionController();
        try
        {

            ObjE.MiscCasHbook = Convert.ToInt32(ddlMisc.SelectedValue);
            ObjE.Misccode = (txtHeadCode.Text).ToString();
            ObjE.Mischead = txtDescription.Text;
            ObjE.CollegeCode = Convert.ToInt32(Session["colcode"]);
            ObjE.Userid = Session["userno"].ToString();
            ObjE.Ipaddress = Session["ipAddress"].ToString();
            ObjE.MiscSrno = Convert.ToInt32(ViewState["add"]);

        }
        catch (Exception ex)
        {
            throw;
        }
        return ObjE;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            MiscFees miscfees = this.binddatafromControl();
            string colname = objCommon.LookUp("mischead_master", "MISCHEADCODE", "MISCHEADCODE='" + txtHeadCode.Text.ToString() + "'");
            string desc = objCommon.LookUp("mischead_master", "MISCHEADCODE", "MISCHEAD='" + txtDescription.Text.ToString() + "'");
            if (ddlMisc.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select Cash Book", this.Page);
            }
            else if (txtHeadCode.Text == "")
            {
                objCommon.DisplayMessage("Please Enter Head Code", this.Page);
            }
            else if (txtDescription.Text == "")
            {
                objCommon.DisplayMessage("Please Enter Description", this.Page);
            }
            else if (ViewState["Edit"] == null)
            {
                if (colname == txtHeadCode.Text)
                {
                    objCommon.DisplayMessage("Head Code is already exists !!!", this.Page);
                    return;
                }
                else
                {
                    CustomStatus cs = new CustomStatus();
                    cs = (CustomStatus)ObjFCC.AddMiscFee(miscfees);

                    if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    {
                        this.ShowMessage("Unable to complete the operation.");
                    }
                    else
                    {
                        this.ShowAllCounters();
                        objCommon.DisplayMessage("Head Code Added Successfully", this.Page);
                        clear();
                    }
                }

            }
            else
            {

                CustomStatus cs = new CustomStatus();
                cs = (CustomStatus)ObjFCC.AddMiscFee(miscfees);
                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                {
                    this.ShowMessage("Unable to complete the operation.");
                }
                else
                {
                    this.ShowAllCounters();
                    objCommon.DisplayMessage("Head Code Updated Successfully", this.Page);
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }


    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //int counterNo = Int32.Parse(editButton.CommandArgument);
            ImageButton editButton = sender as ImageButton;
            int miscNo = Int32.Parse(editButton.CommandArgument);
            int testNo = Int32.Parse(editButton.CommandArgument);

            //DataSet ds = ObjFCC.GETMISCNO();
            ViewState["add"] = miscNo.ToString(); // HERE WE ADD MISCELLANEOUS NO INTO VIEWSTATE
            ViewState["add"] = testNo.ToString(); //   HERE WE ADD MISCELLANEOUS NO INTO VIEWSTATE""
            ViewState["Edit"] = "Edit";
            DataTable dt = objCommon.FillDropDown("MISCHEAD_MASTER", "MISCHEADCODE", "MISCHEAD,CBOOKSRNO", "MISCHEADSRNO = " + miscNo + " AND MISCHEADSRNO = " + testNo, "").Tables[0];
            //DataTable dt = objCommon.FillDropDown("MISCHEAD_MASTER", "MISCHEADCODE", "MISCHEAD,CBOOKSRNO", "MISCHEADSRNO = "+testNo, "").Tables[0];
            if (dt != null && dt.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                binddatatocontrol(dt.Rows[0]); // HERE WE BIND DATA 
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //THIS METHOD IS USED FOR EDIT BTN
    private void binddatatocontrol(DataRow dr)
    {
        try
        {
            //if (dr["MISCHEADSRNO"].ToString() != null &&
            //    ddlMisc.Items.FindByText(dr["MISCHEADSRNO"].ToString()) != null)
            //    ddlMisc.SelectedValue = dr["MISCHEADSRNO"].ToString();

            if (dr["MISCHEADCODE"].ToString() != null)
                txtHeadCode.Text = dr["MISCHEADCODE"].ToString();
            if (dr["MISCHEAD"].ToString() != null)
                txtDescription.Text = dr["MISCHEAD"].ToString();
            objCommon.FillDropDownList(ddlMisc, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "BELONGS_TO='M'", "");

            if (dr["CBOOKSRNO"].ToString() != null)
                ddlMisc.SelectedValue = dr["CBOOKSRNO"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //THIS METHOD IS USED TO BIND DATA INTO LISTVIEW 
    private void ShowAllCounters()
    {

        try
        {

            //DataSet ds = ObjFCC.GETMISCNO();
            DataTable dt = objCommon.FillDropDown("MISCHEAD_MASTER", "MISCHEADSRNO", "MISCHEADCODE,MISCHEAD ", "", "").Tables[0];
            lvCounters.DataSource = dt;
            lvCounters.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void BindListView()
    {
        DataSet ds = null;
        try
        {
            DataTable dt = objCommon.FillDropDown("MISCHEAD_MASTER M inner join ACD_RECIEPT_TYPE A ON(A.RCPTTYPENO = M.CBOOKSRNO)", "M.MISCHEADCODE", "M.MISCHEAD,M.CBOOKSRNO,M.MISCHEADSRNO", "M.CBOOKSRNO=" + ddlMisc.SelectedValue, "M.MISCHEADSRNO").Tables[0];
            lvCounters.DataSource = dt;
            lvCounters.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlMisc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}




