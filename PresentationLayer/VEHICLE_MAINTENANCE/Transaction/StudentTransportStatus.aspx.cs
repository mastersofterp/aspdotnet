using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Linq;
using System.Data.SqlTypes;

public partial class VEHICLE_MAINTENANCE_Transaction_StudentTransportStatus : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController ObjCon = new VMController();
    VM ObjEnt = new VM();
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ds = (DataSet)ViewState["Table"];
            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                }

                BindData();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLEMANAGMENT.StudentTransportStaus.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindData()
    {
        try
        {
            ViewState["Table"] = ds = ObjCon.GetTablesStudentTransportStatus();
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLEMANAGMENT.StudentTransportStaus.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {

            DataTable NewDt = new DataTable();
            NewDt.Columns.Add("ENROLLNO", typeof(string));
            NewDt.Columns.Add("STUDNAME", typeof(string));
            NewDt.Columns.Add("TRANSPORT", typeof(string));
            NewDt.Columns.Add("S_CANCELED_DATE", typeof(DateTime));
            var result =
             from dt1 in ds.Tables[0].AsEnumerable()
             join dt2 in ds.Tables[1].AsEnumerable() on dt1.Field<int>("IDNO") equals dt2.Field<int>("S_IDNO")



             select NewDt.LoadDataRow(new object[]
                       {
                            dt1.Field<string>("ENROLLNO"),
                            dt1.Field<string>("STUDNAME"),
                            dt1.Field<string>("TRANSPORT"),
                            dt2.Field<DateTime>("S_CANCELED_DATE")
                       }, false);
            result.CopyToDataTable();

            LvStudent.DataSource = NewDt;
            LvStudent.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLEMANAGMENT.StudentTransportStaus.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void txtscearcheno_TextChanged(object sender, EventArgs e)
    {
        BindControl(txtscearcheno.Text);

    }
    public void BindControl(string Condition)
    {
        Condition = "ENROLLNO='" + Condition + "'";

        DataRow[] FOUND = ds.Tables[0].Select(Condition);
        if (FOUND.Length > 0)
        {
            Condition = string.Empty;
            ViewState["IDNO"] = null;
            ViewState["IDNO"] = FOUND[0]["IDNO"].ToString();
            lblBranch.Text = FOUND[0]["BRANCH"].ToString();
            lblDegree.Text = FOUND[0]["DEGREENAME"].ToString();
            lblsem.Text = FOUND[0]["SEMFULLNAME"].ToString();
            lblStatus.Text = FOUND[0]["TRANSPORT"].ToString();
            lblstudentname.Text = FOUND[0]["STUDNAME"].ToString();
            lblyear.Text = FOUND[0]["YEARNAME"].ToString();
            txtscearcheno.Text = FOUND[0]["ENROLLNO"].ToString();
            rdostatus.SelectedValue = FOUND[0]["TransportValue"].ToString();

        }
        else
        {
            MessageBox("Record Not Found");
            Clear();
        }
    }
    public void Clear()
    {
        lblBranch.Text =
        lblDegree.Text =
        lblsem.Text =
        lblStatus.Text =
        lblstudentname.Text =
        txtscearcheno.Text =
        lblyear.Text = string.Empty;
        rdostatus.SelectedValue = "0";
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["IDNO"] != null)
            {
                ObjEnt.S_IDNO = Convert.ToInt32(ViewState["IDNO"].ToString());
                ObjEnt.S_STATUS = Convert.ToInt32(rdostatus.SelectedValue);
                //ObjEnt.S_CANCELLED_STATUS = rdostatus.SelectedValue == "0" ? "C" : "A";
                if (rdostatus.SelectedValue == "0")
                {
                    ObjEnt.S_CANCELLED_STATUS ="C"; 
                }
                else if (rdostatus.SelectedValue == "1")
                {
                    ObjEnt.S_CANCELLED_STATUS ="A";  
                }
                else
                {
                    ObjEnt.S_CANCELLED_STATUS = "E";
                    ObjEnt.S_STATUS = 0;   // if transport is exempted then also Transport= 0 will update in student table.
                }
                ObjEnt.S_CREATED_BY = Convert.ToInt32(Session["userno"].ToString());
                string Msg = string.Empty;
                if (rdostatus.SelectedValue == "0")
                {
                    Msg = "Transport Cancelled";
                }
                else if (rdostatus.SelectedValue == "1")
                {
                    Msg = "Transport Applied";
                }
                else 
                {
                    Msg = "Transport Exempted";
                }
                CustomStatus cs = (CustomStatus)ObjCon.Ins_Upd_StudentTransportstaus(ObjEnt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    MessageBox(Msg + " Successfully.");
                }
                else
                {
                    MessageBox(Msg + " Successfully.");
                }
                BindData();
                Clear();
            }
            else
            {
                MessageBox("Please search student.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLEMANAGMENT.StudentTransportStaus.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = sender as ImageButton;
        Session["ENROLLNUMBER"] = img.CommandArgument.ToString();
        BindControl(Convert.ToString(Session["ENROLLNUMBER"]));
    }
}