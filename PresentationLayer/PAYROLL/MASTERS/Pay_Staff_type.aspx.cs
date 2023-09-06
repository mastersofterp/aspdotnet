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
//using Google.API.Translate;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.IO;

public partial class PAYROLL_MASTERS_Pay_Staff_type : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    DeptDesigMaster objDeptDesig = new DeptDesigMaster();
    string UsrStatus = string.Empty;

    #region Page Load

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
            Form.Attributes.Add("onLoad()", "msg()");
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ViewState["action"] = "add";
                BindListView();
            }
        }
        //ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "onLoad();", true);

    }
    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PayDepartment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayDepartment.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objDeptDesig.GetStaffType();

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvstaff.DataSource = ds;
                lvstaff.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    private void Clear()
    {
         txtStaffType.Text = "";
         chkActive.Checked = false;
         chkIsTeaching.Checked = false;
        ViewState["lblFNO"] = null;
        ViewState["action"] = "add";
    }

    #endregion

    #region Page Events

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //Update();
        PayMaster objPay = new PayMaster();
        objPay.STAFFTYPE = txtStaffType.Text.Trim();
        if (chkActive.Checked)
        {
            objPay.ACTIVESTATUS = "1";
        }
        else
        {
            objPay.ACTIVESTATUS = "0";
        }
        
        objPay.IsTeaching = chkIsTeaching.Checked ? true : false;


        if (ViewState["action"] != null)
        {
            bool result = CheckPurpose();
            if (ViewState["action"].ToString().Equals("add"))
            {
                // bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    objPay.STAFFNO = 0;
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddStaffType(objPay);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        //BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
            else
            {
                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    ViewState["action"] = "add";
                    objPay.STAFFNO = Convert.ToInt32(ViewState["STNO"].ToString().Trim());
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddStaffType(objPay);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }

        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["STNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblstaffname = lst.FindControl("lblstaffname") as Label;
            Label lblisactive = lst.FindControl("lblisactive") as Label;
            Label lblisteaching = lst.FindControl("lblisteaching") as Label;
            txtStaffType.Text = lblstaffname.Text.Trim();
            if (lblisactive.Text=="Active")
            
            {
                chkActive.Checked=true;
            }
            else
            {
                chkActive.Checked = false;
            }

            if (lblisteaching.Text == "False")
            {
                chkIsTeaching.Checked = false;
            }
            else
            {
                chkIsTeaching.Checked = true;
            }
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    #endregion
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
        // AMOL SAWARKAR  CONDITION OR ORIGINAL CHNAGE (AND)
        // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
        dsPURPOSE = objCommon.FillDropDown("PAYROLL_STAFFTYPE", "*", "", "STAFFTYPE='" + txtStaffType.Text + "'and STNO!='" + Convert.ToInt32(ViewState["STNO"]) + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

}
