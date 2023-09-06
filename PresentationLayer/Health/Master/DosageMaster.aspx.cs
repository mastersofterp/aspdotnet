using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_Master_DosageMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HelMasterController objHelMaster = new HelMasterController();
    Health objHel = new Health();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help                 
                }
                //BindListView(0);
                ViewState["action"] = "add";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            objHel.DOSAGENAME = txtdname.Text;
            objHel.DOSAGEQUANTITY = txtquantity.Text;

            if (chkActive.Checked == true)
            {
                objHel.STATUS = 'Y';
            }
            else
            {
                objHel.STATUS = 'N';
            }
            objHel.IP_ADDRESS = IP;
            objHel.MAC_ADDRESS = ""; /// GetMacAddress(IP);
            objHel.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                if (ViewState["DNO"] != null)

                    objHel.DNO = Convert.ToInt32(ViewState["DNO"]);
                CustomStatus cs = (CustomStatus)objHelMaster.UpdateHelDosage(objHel);

                if (cs.Equals(CustomStatus.RecordUpdated))
                    MessageBox("Record Updated Successfully");
            }
            else if (ViewState["action"] != null && ViewState["action"].ToString().Equals("add"))
            {

                int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_DOSAGEMASTER", " COUNT(*)", "DNAME='" + txtdname.Text + "' AND DQTY='" + txtquantity.Text + "'"));
                if (duplicateCkeck == 0)
                {
                    CustomStatus cs = (CustomStatus)objHelMaster.AddHelDosage(objHel);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        MessageBox("Record Saved Successfully");
                }
                else
                {
                   MessageBox("Record Already Exists.");
                }
            }
            ClearControls();
            ViewState["action"] = "add";
            BindListView();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        this.ClearControls();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int DNO = int.Parse(btnEdit.CommandArgument);
            ShowDetail(DNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objHelMaster.GetDosageMasterDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvDosage.DataSource = ds.Tables[0];
                lvDosage.DataBind();
            }
            else
            {
                lvDosage.DataSource = null;
                lvDosage.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        txtdname.Text = string.Empty;
        txtquantity.Text = string.Empty;
        chkActive.Checked = true;
        ViewState["DNO"] = null;
    }

    private void ShowDetail(int DNO)
    {
        try
        {
            ViewState["DNO"] = DNO;
            DataSet ds = objHelMaster.GetDosageMasterByNo(DNO);
            if (ds.Tables[0].Rows.Count != null)
            {
                txtdname.Text = ds.Tables[0].Rows[0]["DNAME"].ToString();
                txtquantity.Text = ds.Tables[0].Rows[0]["DQTY"].ToString();
                //int dno = Convert.ToInt32(ds.Tables[0].Rows[0]["DNO"].ToString());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}