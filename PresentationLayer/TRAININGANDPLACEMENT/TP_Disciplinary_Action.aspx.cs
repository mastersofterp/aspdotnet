using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.IO;

public partial class TP_Disciplinary_Action : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objDisciplinary = new TPController();

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

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            BindDisciplinaryStudent();
            ViewState["action"] = "add";
            // divMsg.InnerHtml = string.Empty;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // string no = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO = " + txtRegno.Text.Trim());

        // string idn = objCommon.LookUp("acd_student", "IDNO", "REGNO=" + no);
        //// string regno = txtRegno.Text;

        //// Int32 reg = Convert.ToInt32(objCommon.LookUp("acd_student", "IDNO", "REGNO=" + regno));
        ////string REG1 =Convert.ToString(reg);
        //if (idn == "")
        // {
        //     objCommon.DisplayMessage(upPanel,"No student found having Reg. No. " + txtRegno.Text.Trim(), this.Page);
        //     return;
        // }
        if (txtFromdt.Text == string.Empty && txtTodt.Text == string.Empty)
        {
            objCommon.DisplayMessage(upPanel, "Please Enter From Date and To Date", this);
            txtFromdt.Focus();
            return;
        }
        if (txtFromdt.Text == string.Empty)
        {
            objCommon.DisplayMessage(upPanel, "Please Enter From Date", this);
            txtFromdt.Focus();
            return;
        }
        if (txtTodt.Text == string.Empty)
        {
            objCommon.DisplayMessage(upPanel, "Please Enter To Date", this);
            txtTodt.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text)) == 1)
        {
            objCommon.DisplayMessage(upPanel, "From Date Can Not Be Greater Than to Date ", this);
            txtFromdt.Focus();
            return;
        }

        //int LIGALREGNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "REGNO", "ENROLLNO='" + txtRegno.Text.Trim() + "'"));
        //string LIGALREGNO1 = Convert.ToString(LIGALREGNO);
        //if (LIGALREGNO1 == "")
        //{
        //    objCommon.DisplayMessage(upPanel, "Please Check Registration No.", this);
        //    return;
        //}
        //else
        //{


            // ViewState["action"] = "add";
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int Status = 0;
                    //if (chknlstatus.Checked)
                    //{
                    //    Status = 1;
                    //}
                    //else
                    //{
                    //    Status = 0;
                    //}

                    DateTime FROMDATE = Convert.ToDateTime(txtFromdt.Text);
                    DateTime TODATE = Convert.ToDateTime(txtTodt.Text);
                    //string FROMDATE = Convert.ToString(FROMDATE1);
                    //string TODATE = Convert.ToString(TODATE1);

                    string date1 = System.DateTime.Now.ToString("dd-MMM-yyyy");
                    DateTime dt2 = Convert.ToDateTime(FROMDATE);
                    DateTime dt3 = Convert.ToDateTime(TODATE);
                    DateTime dt1 = Convert.ToDateTime(date1);
                    if (dt2 <= dt1 && dt3 >= dt1)

                    //if (active == 1)
                    {
                        Status = 1;

                    }
                    else
                    {
                        Status = 0;
                    }




                    //  int REGNO = Convert.ToInt32(objCommon.LookUp("acd_student", "REGNO", "idno=" + Convert.ToInt32(Ugstudentidno)));
                    //  string IsAvail = objCommon.LookUp("ACD_TP_STUDENT_SELECTION_PROCESS", "COUNT(spno)cnt", "SCHEDULENO="+SchNo+" AND IDS LIKE '%" + Convert.ToInt16(chk.ToolTip.ToString()) + "%'");
                    // string ENROLLNO = txtRegno.Text;
                    //  string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtSearchText.Text.Trim() + "'");
                    // string ENROLLNO1 = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "ENROLLNO='" + txtRegno.Text.Trim() + "'");
                    string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "ENROLLNO='" + txtRegno.Text.Trim() + "'");
                    if (REGNO=="")
                    {
                        objCommon.DisplayMessage(upPanel, "Please Enter Correct Enrollment Number !", this);
                        return;
                    }
                    string RNO = Convert.ToString(REGNO);
                    string remark =Convert.ToString(txtRemark.Text.Trim());
                    CustomStatus cs = (CustomStatus)objDisciplinary.AddStudentForDisciplinaryAction(txtRegno.Text, RNO, Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), Status,remark);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(upPanel, "Record Save Successfully !", this);
                        BindDisciplinaryStudent();
                        Clear();
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upPanel, "Record Already Exists !", this.Page);
                        BindDisciplinaryStudent();
                        Clear();
                    }
                }
                else
                {
                    int Status = 0;
                    //if (chknlstatus.Checked)
                    //{
                    //    Status = 1;
                    //}
                    //else
                    //{
                    //    Status = 0;
                    //}

                    DateTime FROMDATE = Convert.ToDateTime(txtFromdt.Text);
                    DateTime TODATE = Convert.ToDateTime(txtTodt.Text);

                    string date1 = System.DateTime.Now.ToString("dd-MMM-yyyy");
                    DateTime dt2 = FROMDATE;
                    DateTime dt3 = TODATE;
                    DateTime dt1 = Convert.ToDateTime(date1);
                    if (dt2 <= dt1 && dt3 >= dt1)

                    //if (active == 1)
                    {
                        Status = 1;

                    }
                    else
                    {
                        Status = 0;
                    }

                    string REGNO = ViewState["REGNO"].ToString();
                    string remark =Convert.ToString(txtRemark.Text.Trim());
                    // string sENROLL = Convert.ToString(REGNO);
                    //string REGNO1 = objCommon.LookUp("ACD_STUDENT", "REGNO", "ENROLLNO='" + txtRegno.Text.Trim() + "'");
                    CustomStatus cs = (CustomStatus)objDisciplinary.UpdateDisciplinaryInfo(REGNO, Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), Status,remark);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindDisciplinaryStudent();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upPanel, "Record Updated Successfully.", this.Page);
                        txtRegno.Enabled = true;
                        Clear();
                    }
                    //if (cs.Equals(CustomStatus.RecordExist))
                    //{
                    //    objCommon.DisplayMessage(upPanel, "Record Already Exists.", this.Page);
                    //    Clear();
                    //}
                }
            }
      //  }
    }
       public string GetStatus(object status)
    {
        string str = status.ToString();
        if (str == "InActive")
           //if (status.ToString().Equals("Active"))
           return "<span style='color:Red;font-weight: bold;'>Inactive</span>";
        else
            return "<span style='color:Green;font-weight: bold;'>Active</span>";
    }

    private void BindDisciplinaryStudent()
    {
         DataSet ds = null;
         //foreach (ListViewDataItem dataitem in lvDisciplinaryStudent.Items)
         //  {
         //Label btnStatus = dataitem.FindControl("txtStatus") as Label;
        // Button btnRefund = dataitem.FindControl("btnRefund") as Button;
               //if (Convert.ToString(btnStatus.Equals) == 'Active')
               //     {
               //         btnApprove.Enabled = false;
               //         btnApprove.BackColor = Color.DarkOrange;
               //         btnApprove.BorderColor = Color.DarkOrange;

         ds = objDisciplinary.GetDisciplinaryActionStudent();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDisciplinaryStudent.DataSource = ds;
            lvDisciplinaryStudent.DataBind();
        }
        else
        {
            lvDisciplinaryStudent.DataSource = null;
            lvDisciplinaryStudent.DataBind();
         //   objCommon.DisplayMessage(upPanel, "Record Not Found !", this);
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;    
            string REGNO = btnEdit.CommandArgument;
            ViewState["REGNO"] = btnEdit.CommandArgument;
            ViewState["action"] = "edit";
            this.ShowDetails(REGNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(string REGNO)
    {
        try
        {
            DataSet ds = objDisciplinary.GetDisciplinaryRecordByREGNo(REGNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRegno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                txtRegno.Enabled = false;
                txtFromdt.Text = ds.Tables[0].Rows[0]["DISCIPLINARY_START_DATE"].ToString();
                txtTodt.Text = ds.Tables[0].Rows[0]["DISCIPLINARY_END_DATE"].ToString();
                string STATUS = ds.Tables[0].Rows[0]["STATUS"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();

               // int S = Convert.ToInt16(STATUS);
               //if(S==1)
               //{
               //    chknlstatus.Checked=true;
               //}
               //else
               //{
               //    chknlstatus.Checked=false;
               //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Clear()
    {
        txtRegno.Text = string.Empty;
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        txtRemark.Text = string.Empty;
       // chknlstatus.Checked = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}