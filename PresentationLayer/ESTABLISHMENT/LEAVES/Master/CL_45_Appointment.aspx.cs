//============================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE MGT.
// PAGE NAME     : CL_45_Appointment.aspx                                                  
// CREATION DATE : 15-JUN-2015
// CREATED BY    : SWATI GHATE                                       
// MODIFIED DATE :
// MODIFIED DESC :
//============================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class CL_45_Appointment : System.Web.UI.Page
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
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                FillCollege();
                ShowDetails();


            }
            //
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");

    }
    private void ShowDetails()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_APPOINT", "APPOINTNO", "APPOINT", "APPOINTNO!=0", "APPOINT");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvList.Visible = true;
                lvList.DataSource = ds;
                lvList.DataBind();
                btnSave.Visible = true;
                //btnReject.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("Record not found", this.Page);
                lvList.DataSource = null;
                lvList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comp_Off_Leave_List.ShowDetails ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int cs = 0;

        string appointno = string.Empty;
        Leaves objLeaves = new Leaves();
        DataTable dtAppRecord = new DataTable();
        dtAppRecord.Columns.Add("APPOINTNO");
        int count = 0;
        foreach (ListViewDataItem item in lvList.Items)
        {
            CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked)
            {
                count = count + 1;
                // appointno += chkSelect.ToolTip+",";
                DataRow dr = dtAppRecord.NewRow();
                dr["APPOINTNO"] = chkSelect.ToolTip;
                dtAppRecord.Rows.Add(dr);
                dtAppRecord.AcceptChanges();
            }
        }

        // if (appointno == "" || appointno == string.Empty)
        if (count == 0)
        {
            objCommon.DisplayMessage("Please Select Atleast One Appointment...", this);
            return;
        }


        cs = objLeave.UpdateAppointment(Convert.ToInt32(ddlCollege.SelectedValue), dtAppRecord);
        if (cs == 1)
        {
            objCommon.DisplayMessage("Record Updated Successfully...", this.Page);
            ShowDetails();

            ViewState["action"] = null;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        foreach (ListViewDataItem items in lvList.Items)
        {
            //cbAl
            CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;

            if (chkSelect.Checked)
            {
                chkSelect.Checked = false;

            }
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowDetails();
        //SELECT CA.APPOINTNO,APP.APPOINT FROM PAYROLL_LEAVE_CL_APPOINTMENT CA INNER JOIN PAYROLL_APPOINT APP ON(APP.APPOINTNO=CA.APPOINTNO)
        DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_CL_APPOINTMENT CA INNER JOIN PAYROLL_APPOINT APP ON(APP.APPOINTNO=CA.APPOINTNO)", "CA.APPOINTNO,APP.APPOINT", "CA.COLLEGE_NO", "CA.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int checkedmark = 0;
            int count = Convert.ToInt32(ds.Tables[0].Rows.Count);

            for (int i = 0; i < lvList.Items.Count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    CheckBox chkSelect = lvList.Items[i].FindControl("chkSelect") as CheckBox;

                    int appointno = Convert.ToInt32(chkSelect.ToolTip.ToString());
                    int caAPPOINTNO = Convert.ToInt32(ds.Tables[0].Rows[j]["APPOINTNO"].ToString());

                    if (appointno == caAPPOINTNO)
                    {
                        chkSelect.Checked = true;
                        checkedmark = 1;
                    }

                }
            }
        }
        else
        {
            foreach (ListViewDataItem lvitem in lvList.Items)
            {
                CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;
                chkSelect.Checked = false;
            }
            //foreach (ListViewItem lvitem in lvEmployees.Items)
            //{
            //    CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;

            //    if (Convert.ToInt32(chkIdNo.ToolTip) == Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()))
            //    {
            //        chkIdNo.Checked = true;
            //        break;
            //    }

            //}
        }

    }
}
