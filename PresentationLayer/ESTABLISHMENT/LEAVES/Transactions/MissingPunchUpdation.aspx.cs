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
using System.Globalization;

public partial class ESTABLISHMENT_LEAVES_Transactions_MissingPunchUpdation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Leaves objLM = new Leaves();
    LeavesController objLC = new LeavesController();

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


                BindMissPunchListView();
                CheckPageAuthorization();
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
                Response.Redirect("~/notauthorized.aspx?page=Comp_Off_Leave.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Comp_Off_Leave.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(ViewState["ValidTime"]) == true)
            {
                Boolean chkv = CheckValid();
                if (chkv == true)
                {
                    objLM.DATE = Convert.ToDateTime(txtWDate.Text);
                    objLM.REASON = txtReason.Text.ToString();
                    objLM.IDNO = Convert.ToInt32(Session["idno"]);
                    objLM.INTIME = txtInTime.Text.ToString();
                    objLM.OUTTIME = txtOutTime.Text.ToString();
                    objLM.CreatedBy = Convert.ToInt32(Session["userno"]);
                    objLM.ModifiedBy = Convert.ToInt32(Session["userno"]);
                    objLM.COLLEGE_CODE = Session["colcode"].ToString();
                    int MissPunchNo = 0;

                    CustomStatus cs = (CustomStatus)objLC.AddMissPunchUpdation(objLM, MissPunchNo);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully...", this.Page);
                        BindMissPunchListView();
                        Clear();
                    }
                }
            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transaction_MissingPunchUpdation.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        txtWDate.Text = string.Empty;
        txtReason.Text = string.Empty;
        txtInTime.Text = string.Empty;
        txtOutTime.Text = string.Empty;
        btnSave.Enabled = true;
        btnSave.Visible = true;
    }
    protected void BindMissPunchListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ESTB_MISSING_PUNCH_UPD_REQ", "WRKDATE,IN_TIME,OUT_TIME,REASON,APR_STATUS", "", "EMPNO = " + Convert.ToInt32(Session["Idno"]) + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RptMissPunch.DataSource = ds.Tables[0];
                RptMissPunch.DataBind();
                DivNote.Visible = false;
                PanelList.Visible = RptMissPunch.Visible = true;
            }
            else
            {
                DivNote.Visible = true;
                PanelList.Visible = RptMissPunch.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transaction_Comp_Off_Leave.BindCompOffListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtOutTime_TextChanged(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        //{
        //    if (txtInTime.Text.ToString() != string.Empty && txtOutTime.Text.ToString() != string.Empty)
        //    {
        //        DateTime intime = Convert.ToDateTime(txtInTime.Text);
        //        DateTime outtime = Convert.ToDateTime(txtOutTime.Text);

        //        if (outtime <= intime)
        //        {
        //            objCommon.DisplayMessage("Please Enter Time in 24 hours format...", this.Page);
        //            btnSave.Enabled = false;
        //        }
        //        else
        //        {
        //            btnSave.Enabled = true;
        //        }
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Please Enter Valid Time!!", this.Page);
        //}
        //btnSave.Enabled = true; 

        if (!string.IsNullOrEmpty(txtInTime.Text) && !string.IsNullOrEmpty(txtOutTime.Text))
        {
            DateTime intime, outtime;

            if (DateTime.TryParse(txtInTime.Text, out intime) && DateTime.TryParse(txtOutTime.Text, out outtime))
            {
                if (outtime <= intime)
                {
                    objCommon.DisplayMessage("Please Enter Time in 24 hours format...", this.Page);
                    //btnSave.Enabled = false;
                    ViewState["ValidTime"] = false;
                    return;
                }
                else
                {
                    ViewState["ValidTime"] = true;
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Valid Time!!", this.Page);
                //btnSave.Enabled = false;
                ViewState["ValidTime"] = false;
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Valid Time!!", this.Page);
            //btnSave.Enabled = false;
            ViewState["ValidTime"] = false;
            return;
        }
    }
    protected void txtInTime_TextChanged(object sender, EventArgs e)
    {
        txtOutTime.Text = string.Empty;
    }

    private Boolean CheckValid()
    {
        string WrkDate = txtWDate.Text;
        DateTime WorkDate = DateTime.Parse(WrkDate);
        string WRKDATE = WorkDate.ToString("yyyy/MM/dd");
        string rec = objCommon.LookUp("ESTB_MISSING_PUNCH_UPD_REQ", "MPNO", "WRKDATE= '" + WRKDATE + "' And EMPNO = '" + Convert.ToInt32(Session["idno"]) + "'");
        if (rec != string.Empty)
        {
            objCommon.DisplayMessage("Record Already Exist!", this.Page);
            return false;
        }
        return true;
    }
}