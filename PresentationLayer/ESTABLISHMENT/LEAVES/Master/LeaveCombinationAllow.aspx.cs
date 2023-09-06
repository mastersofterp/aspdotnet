using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ESTABLISHMENT_LEAVES_Master_LeaveCombinationAllow : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
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
                Page.Title = Session["coll_name"].ToString();
                CheckPageAuthorization();
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillLeaveName();
                //FillLeave();
                //FillLeavesuffix();

                ViewState["action"] = "add";
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
                Response.Redirect("~/notauthorized.aspx?page=HolidayType.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HolidayType.aspx");
        }
    }

    private void FillLeaveName()
    {
        try
        {
            objCommon.FillDropDownList(ddlleaveshrtname, "Payroll_Leave_Name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void FillLeave()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("Payroll_Leave_Name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvlist.DataSource = ds.Tables[0];
                    lvlist.DataBind();

                    lvlist.Visible = true;
                    pnlListMain.Visible = true;
                    //Fill the treeview  
                    //tvLinks.ExpandAll();                   
                }
                else
                {
                    lvlist.DataSource = null;
                    lvlist.DataBind();
                    lvlist.Visible = false;
                    pnlListMain.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void FillLeavesuffix()
    {
        try
        {

            if (ddlleaveshrtname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("Payroll_Leave_Name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvlleavepre.DataSource = ds.Tables[0];
                        lvlleavepre.DataBind();

                        lvlleavepre.Visible = true;
                        pnllistprefix.Visible = true;
                        lvlist.Visible = true;
                        pnlListMain.Visible = true;
                        pnllist.Visible = true;

                    }
                    else
                    {
                        lvlleavepre.DataSource = null;
                        lvlleavepre.DataBind();
                        lvlleavepre.Visible = false;
                        pnllistprefix.Visible = false;
                        lvlist.Visible = false;
                        pnlListMain.Visible = false;
                        pnllist.Visible = false;

                    }

                }
            }
            else
            {
                lvlleavepre.DataSource = null;
                lvlleavepre.DataBind();
                lvlleavepre.Visible = false;
                pnllistprefix.Visible = false;
                lvlist.Visible = false;
                pnlListMain.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }




    protected void ddlleaveshrtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Payroll_leave_suffixprefix", "LVNO,SPFID", "CASE WHEN LEN(PrefixAllowed) > 0 THEN ISNULL(PrefixAllowed,'0') ELSE '0' END PrefixAllowed,CASE WHEN LEN(SufixAllowed) > 0 THEN ISNULL(SufixAllowed,'0') ELSE '0' END SufixAllowed", "LVNO=" + ddlleaveshrtname.SelectedValue, "LVNO");


            if (ds.Tables[0].Rows.Count > 0)
            {
                FillLeave();
                FillLeavesuffix();
                ViewState["SPFID"] = ds.Tables[0].Rows[0]["SPFID"].ToString();

                if (ViewState["SPFID"] != null)
                {
                    ViewState["action"] = "edit";
                }
                string[] suffix = ds.Tables[0].Rows[0]["SufixAllowed"].ToString().Split(',');
                string[] prefix = ds.Tables[0].Rows[0]["PrefixAllowed"].ToString().Split(',');

                for (int i = 0; i < prefix.Length; i++)
                {
                    foreach (ListViewDataItem Items in lvlist.Items)
                    {
                        CheckBox chkAccept = Items.FindControl("chkAccept") as CheckBox;
                        int chk = Convert.ToInt32(chkAccept.ToolTip.ToString());
                        if (Convert.ToInt32(prefix[i]) == chk)
                        {
                            chkAccept.Checked = true;

                        }
                    }
                }

                for (int i = 0; i < suffix.Length; i++)
                {
                    foreach (ListViewDataItem Items in lvlleavepre.Items)
                    {
                        CheckBox chkacceptsuffix = Items.FindControl("chkacceptsuffix") as CheckBox;
                        int chks = Convert.ToInt32(chkacceptsuffix.ToolTip.ToString());
                        if (Convert.ToInt32(suffix[i]) == chks)
                        {
                            chkacceptsuffix.Checked = true;
                        }
                    }
                }
            }
            else
            {
                ViewState["action"] = "add";
                FillLeave();
                FillLeavesuffix();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Leaves objleaves = new Leaves();
        try
        {
            objleaves.LEAVENO = Convert.ToInt32(ddlleaveshrtname.SelectedValue);
            objleaves.LEAVENAME = Convert.ToString(ddlleaveshrtname.SelectedItem.Text);
            objleaves.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
            objleaves.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString());
            objleaves.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            objleaves.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            string PrefixAllowed = "";
            string SufixAllowed = "";
            foreach (ListViewDataItem Items in lvlist.Items)
            {
                CheckBox chkAccept = Items.FindControl("chkAccept") as CheckBox;
                if (chkAccept.Checked == true)
                {
                    if (PrefixAllowed == "")
                    {
                        PrefixAllowed = chkAccept.ToolTip.ToString();
                    }
                    else
                    {
                        PrefixAllowed = PrefixAllowed + "," + chkAccept.ToolTip.ToString();
                    }
                }

            }
            foreach (ListViewDataItem Items in lvlleavepre.Items)
            {
                CheckBox chkacceptsuffix = Items.FindControl("chkacceptsuffix") as CheckBox;
                if (chkacceptsuffix.Checked == true)
                {
                    if (SufixAllowed == "")
                    {
                        SufixAllowed = chkacceptsuffix.ToolTip.ToString();
                    }
                    else
                    {
                        SufixAllowed = SufixAllowed + "," + chkacceptsuffix.ToolTip.ToString();
                    }
                }
            }
            if (PrefixAllowed == "")
            {
                PrefixAllowed = "0";
            }
            if (SufixAllowed == "")
            {
                SufixAllowed = "0";
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objLeave.AddLeavePrefixSuffix(objleaves, PrefixAllowed, SufixAllowed);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        Clear();
                        //ddlleaveshrtname.SelectedIndex = 0;
                        // checkuncheckleave();

                    }
                }
                else
                {
                    if (ViewState["SPFID"] != null)
                    {
                        objleaves.SPFID = Convert.ToInt32(ViewState["SPFID"].ToString());
                        CustomStatus cs = (CustomStatus)objLeave.UpdateLeavePrefixSuffix(objleaves, PrefixAllowed, SufixAllowed);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            Clear();
                            //ddlleaveshrtname.SelectedIndex = 0;
                            //checkuncheckleave();   


                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            pnllist.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void Clear()
    {
        ddlleaveshrtname.SelectedIndex = 0;
        lvlist.DataSource = null;
        lvlist.DataBind();

        lvlleavepre.DataSource = null;
        lvlleavepre.DataBind();
        ViewState["SPFID"] = null;
        pnllist.Visible = false;


    }
}