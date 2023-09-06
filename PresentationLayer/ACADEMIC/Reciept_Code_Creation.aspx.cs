using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Reciept_Code_Creation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
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

                BindListView();
            }
            ViewState["action"] = "add";

        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objSC.GetAllRecieptCode();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvRecieptCode.DataSource = ds;
                lvRecieptCode.DataBind();
            }
            else
            {
                pnlSession.Visible = false;
                lvRecieptCode.DataSource = null;
                lvRecieptCode.DataBind();
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
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int IsActive = 0;
        string rcname = txtReceiptName.Text.ToString();
        string recieptcode = txtRecieptCode.Text.ToString();
        if (hfdActive.Value == "true")
        {
            IsActive = 1;
        }
        else
        {
            IsActive = 0;

        }
        CustomStatus cs = (CustomStatus)objSC.AddReceiptCode(rcname, recieptcode, IsActive);
        if (cs.Equals(CustomStatus.RecordExist))
              {
                  objCommon.DisplayMessage(this.updSession, "Record Already Exist", this.Page);
              }
               else if (cs.Equals(CustomStatus.RecordSaved))
               {
                   objCommon.DisplayMessage(this.updSession, "Record Saved Successfully!", this.Page);
               }
               else
               {
                   objCommon.DisplayMessage(this.updSession, "Error Adding Slot Name!", this.Page);
               }
             this.ClearControls();
             BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();
    }
    private void ClearControls()
    {
        txtReceiptName.Text = string.Empty;
        txtRecieptCode.Text = string.Empty;
    }
}