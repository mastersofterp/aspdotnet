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

public partial class DISCIPLINARY_Disciplinary_Action : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                FillDropDown();
                Session["trainTbl"] = null;
                //lvStudInvol.DataSource = null;
                //lvStudInvol.DataBind();
                ShowBlankLV();
            }
          
        }
    }

    void ShowBlankLV()
    {
        DataTable dt = CreateTable();
        DataRow dr = dt.NewRow();      
        dt.Rows.Add(dr);

        lvStudInvol.DataSource = dt;    
        lvStudInvol.DataBind();

        ImageButton imgbtDelete = (ImageButton)lvStudInvol.Items[0].FindControl("btnDelete");
        imgbtDelete.Visible = false;

        Label lblName = (Label)lvStudInvol.Items[0].FindControl("lblStudName");
        lblName.Text = "No Record Found.";
        lblName.ForeColor = System.Drawing.Color.Red;
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlEventCat, "ACD_EVENTCATEOGRY", "COLLEGE_CODE", "EVENTCATENAME", "", "EVENTCATENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Acd_SeatNumber_Allotment.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("acd_student a,acd_semester b,acd_branch c", "IDNO,studname,a.regno,a.semesterno,a.branchno", "b.semestername,c.longname", "regno='" + txtStudInvol.Text + " 'and a.semesterno = b.semesterno and a.branchno = c.branchno", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Session["trainTbl"] != null && ((DataTable)Session["trainTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["trainTbl"];
                int count = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["IDNO"].ToString() == ds.Tables[0].Rows[0]["IDNO"].ToString())
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    objCommon.DisplayMessage("Student already exists..!!", this.Page);
                }
                else
                {                    
                    DataRow dr = dt.NewRow();

                    dr["IDNO"] = ds.Tables[0].Rows[0]["IDNO"];
                    dr["studname"] = ds.Tables[0].Rows[0]["studname"];
                    dr["semestername"] = ds.Tables[0].Rows[0]["semestername"];
                    dr["longname"] = ds.Tables[0].Rows[0]["longname"];
                    dr["semesterno"] = ds.Tables[0].Rows[0]["semesterno"];
                    dr["branchno"] = ds.Tables[0].Rows[0]["branchno"];
                    dr["regno"] = ds.Tables[0].Rows[0]["regno"];
                    dt.Rows.Add(dr);

                    Session["trainTbl"] = dt;
                    lvStudInvol.DataSource = dt;
                    lvStudInvol.DataBind();
                    ViewState["TSno"] = Convert.ToInt32(ViewState["TSno"]) + 1;
                }
            }
            else
            {
                //DataSet ds = objCommon.FillDropDown("acd_student a,acd_semester b,acd_branch c", "IDNO,studname,a.regno,a.semesterno,a.branchno", "b.semestername,c.longname", "regno='" + txtStudInvol.Text + "'and a.semesterno = b.semesterno and a.branchno = c.branchno", "");
                DataTable dt = this.CreateTable();
                DataRow dr = dt.NewRow();



                dr["IDNO"] = ds.Tables[0].Rows[0]["IDNO"];
                dr["studname"] = ds.Tables[0].Rows[0]["studname"];
                dr["semestername"] = ds.Tables[0].Rows[0]["semestername"];
                dr["longname"] = ds.Tables[0].Rows[0]["longname"];
                dr["semesterno"] = ds.Tables[0].Rows[0]["semesterno"];
                dr["branchno"] = ds.Tables[0].Rows[0]["branchno"];
                dr["regno"] = ds.Tables[0].Rows[0]["regno"];
                ViewState["TSno"] = Convert.ToInt32(ViewState["TSno"]) + 1;

                dt.Rows.Add(dr);
                Session["trainTbl"] = dt;
                lvStudInvol.DataSource = dt;
                lvStudInvol.DataBind();
            }
        }
        else
        {
            if (Session["trainTbl"] != null && ((DataTable)Session["trainTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["trainTbl"];
                lvStudInvol.DataSource = dt;
                lvStudInvol.DataBind();
                objCommon.DisplayMessage("No Record Found..!!", this.Page);
            }
            else
            {
                lvStudInvol.DataSource = null;
                lvStudInvol.DataBind();
                ShowBlankLV();
                objCommon.DisplayMessage("No Record Found..!!", this.Page);
            }
        }       

    }
    private DataTable CreateTable()
    {
        DataTable dtTrain = new DataTable();
        dtTrain.Columns.Add(new DataColumn("IDNO", typeof(int)));
        dtTrain.Columns.Add(new DataColumn("studname", typeof(string)));
        dtTrain.Columns.Add(new DataColumn("semestername", typeof(string)));
        dtTrain.Columns.Add(new DataColumn("longname", typeof(string)));
        dtTrain.Columns.Add(new DataColumn("semesterno", typeof(int)));
        dtTrain.Columns.Add(new DataColumn("branchno", typeof(int)));
        dtTrain.Columns.Add(new DataColumn("regno", typeof(string)));   
        return dtTrain;
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {      
        Disciplinary_Action objDiscAction = new Disciplinary_Action();
        Disciplinary_ActionController objDiscActionCon = new Disciplinary_ActionController();

        objDiscAction.EDATE =Convert.ToDateTime(txtEventDate.Text);
        objDiscAction.ETITLE = txtEventTitle.Text;
        objDiscAction.EDESC = txtEventDesc.Text;
        objDiscAction.ECAT = Convert.ToInt32(ddlEventCat.SelectedValue);
        objDiscAction.PUNISH = txtActTaken.Text;
        objDiscAction.AUTHO = txtAuthInvol.Text;
        objDiscAction.UNO = Convert.ToInt32(Session["userno"]);
        objDiscAction.ENTRYDATE = System.DateTime.Now;
        objDiscAction.CCODE = Convert.ToInt32(Session["colcode"]);

        CustomStatus cs = (CustomStatus)objDiscActionCon.AddEvent(objDiscAction);
        if (cs.Equals(CustomStatus.RecordSaved))
        {            
              if (Session["trainTbl"] != null && ((DataTable)Session["trainTbl"]) != null)
              {
                  DataTable dt;
                  dt = (DataTable)Session["trainTbl"];

                  foreach (DataRow dr in dt.Rows)
                  {
                      objDiscAction.STUDID =Convert.ToInt32(dr["IDNO"]);
                      objDiscAction.BRANCHNO = Convert.ToInt32(dr["branchno"]);
                      objDiscAction.SEMNO = Convert.ToInt32(dr["semesterno"]);
                      objDiscAction.CCODE = Convert.ToInt32(Session["colcode"]);
                      CustomStatus cs1 = (CustomStatus)objDiscActionCon.AddStudAction(objDiscAction);
                      if (cs1.Equals(CustomStatus.RecordSaved))
                      {
                          objCommon.DisplayMessage("Record Saved Successfully..!!", this.Page);
                      }
                  }
              }
        }
        Clear();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["trainTbl"] != null && ((DataTable)Session["trainTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["trainTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["trainTbl"] = dt;
                if (dt.Rows.Count > 0)
                {
                    lvStudInvol.DataSource = dt;
                    lvStudInvol.DataBind();
                }
                else
                {
                    ShowBlankLV();
                }
            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "DISCIPLINARY_Disciplinary_Action.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["IDNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    void Clear()
    {
        txtEventDate.Text = string.Empty;
        txtActTaken.Text = string.Empty;
        txtAuthInvol.Text = string.Empty;
        txtEventDesc.Text = string.Empty;
        txtEventTitle.Text = string.Empty;
        txtStudInvol.Text = string.Empty;
        ddlEventCat.SelectedIndex = 0;
        lvStudInvol.DataSource = null;
        lvStudInvol.DataBind();
        Session["trainTbl"] = null;
        ShowBlankLV();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Punished_Student_Report", "rptDisciplinary_Action_Taken.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISCIPLINARY_Disciplinary_Action.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("DISCIPLINARY")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISCIPLINARY_Disciplinary_Action.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
