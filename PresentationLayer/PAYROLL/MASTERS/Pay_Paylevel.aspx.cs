//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayLevel.ASPX                                                    
// CREATION DATE : 13-02-2019                                                        
// CREATED BY    : Prashant Wankar                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_MASTERS_Pay_Paylevel : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string Message = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
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
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                BindPaylevel();
                FillDropdown();
                ViewState["Update"] = "Insert";
                BindCellNo();
            }
        }
    }

    protected void BindCellNo()
    {
        if (ViewState["Update"].ToString() == "Update")
        {
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), Convert.ToInt32(ViewState["Paylevelno"].ToString()));
        }
        else
        {
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), 0);
        }

    }

    private void BindPaylevel()
    {
        try
        {

            DataSet ds = GetPaylevelDataAll(0, "GetData");

            if (ds.Tables[0].Rows.Count > 0)
            {
                LstEdit.DataSource = ds;
                LstEdit.DataBind();
            }
            else
            {
                LstEdit.DataSource = null;
                LstEdit.DataBind();

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void BindListViewList(int staffno, int payeleveno)
    {
        try
        {
            //if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            //{

            pnlAttendance.Visible = true;
            DataSet ds = GetCellInformation(staffno, payeleveno);
            if (ds.Tables[0].Rows.Count <= 0)
            {

                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
            else
            {

                btnSave.Visible = true;
                btnCancel.Visible = true;
            }

            lvCEllno.DataSource = ds;
            lvCEllno.DataBind();
            // }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmsg.Text = null;
            ImageButton btnEdit = sender as ImageButton;
            int appointno = int.Parse(btnEdit.CommandArgument);
            ViewState["Update"] = "Update";
            ShowDetails(appointno);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int paylevelNo)
    {
        try
        {
            SqlDataReader dr = GetPaylevelData(paylevelNo, "GetDataById");

            if (dr != null)
            {
                if (dr.Read())
                {   //Paylevel_No,Staff_No,Paylevel_Name,Paylevel_Srno,UA_NO,Record_Date ,STAFF,STAFFNO
                    ViewState["Paylevelno"] = paylevelNo.ToString();
                    txtpayLevel.Text = dr["Paylevel_Name"].ToString();
                    txtPaylevelSrno.Text = dr["Paylevel_Srno"].ToString();
                    ddlStaff.SelectedValue = dr["STAFFNO"].ToString();
                    txtTAFarmula.Text = dr["TA_Formula"].ToString();
                    BindCellNo();
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            long res = 0;

            int payLevelno = 0;
            int Ua_no = 0; int staff_no = 0;
            string PayLevelname = string.Empty;
            string paylevelsrno = string.Empty;
            string TaFormula = string.Empty;

            Ua_no = Convert.ToInt32(Session["userno"]);
            staff_no = Convert.ToInt32(ddlStaff.SelectedValue);
            PayLevelname = txtpayLevel.Text.Trim();
            paylevelsrno = txtPaylevelSrno.Text.Trim();
            TaFormula = txtTAFarmula.Text.Trim();

            if (ViewState["Update"].ToString() == "Update")
            {
                payLevelno = Convert.ToInt32(ViewState["Paylevelno"].ToString());
            }
            else
            {
                payLevelno = 0;
            }


            DataTable CellNumTbl = new DataTable("CellNumTbl");

            CellNumTbl.Columns.Add("CellNo", typeof(int));
            CellNumTbl.Columns.Add("PayLevelAmount", typeof(decimal));


            DataRow dr = null;

            //For Accession Register insert

            foreach (ListViewItem i in lvCEllno.Items)
            {
                Label lblCellno = (Label)i.FindControl("lblCellno");
                TextBox txtPayLevelAmount = (TextBox)i.FindControl("txtPayLevelAmount");

                if (txtPayLevelAmount.Text == string.Empty) { txtPayLevelAmount.Text = "0"; }
                dr = CellNumTbl.NewRow();

                dr["CellNo"] = Convert.ToInt32(lblCellno.Text);
                dr["PayLevelAmount"] = Convert.ToDecimal(txtPayLevelAmount.Text);

                CellNumTbl.Rows.Add(dr);
            }



            res = InsertPayLevel(payLevelno, Ua_no, staff_no, PayLevelname, paylevelsrno, CellNumTbl, TaFormula, ref Message);

            if (res == -99)
            {
                Showmessage("Error Occured !!");
                return;
            }
            if (res == 1)
            {
                Showmessage("Record Saved Successfully !!");
                btnCancel_Click(sender, e);
                return;
            }
            if (res == 2)
            {
                Showmessage("Record Updated Successfully !!");
                btnCancel_Click(sender, e);
                return;
            }
            if (res == 3)
            {
                Showmessage("Record Already Exists !!");
                return;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Update"] = "Insert";
            BindPaylevel();
            BindCellNo();
            ddlStaff.SelectedIndex = 0;
            lblerror.Text = string.Empty;
            lblmsg.Text = string.Empty;

            txtTAFarmula.Text = "";
            txtpayLevel.Text = "";
            txtPaylevelSrno.Text = "";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Prashant Wankar Dated : 13-02-2019

    public DataSet GetCellInformation(int staffNo, int PaylevelNo)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@V_Staff_No", staffNo);
            objParams[1] = new SqlParameter("@V_Paylevel_No", PaylevelNo);

            ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_CELL_NUMBER_INFO", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetAttendanceOfEmployee-> " + ex.ToString());
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    public DataSet GetPaylevelDataAll(int PaylevelNo, string Commandtype)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@V_Paylevel_No", PaylevelNo);
            objParams[1] = new SqlParameter("@V_CommandType", Commandtype);

            ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_PAY_LEVEL_INFO", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetAttendanceOfEmployee-> " + ex.ToString());
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    public SqlDataReader GetPaylevelData(int PaylevelNo, string Commandtype)
    {
        SqlDataReader dr = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@V_Paylevel_No", PaylevelNo);
            objParams[1] = new SqlParameter("@V_CommandType", Commandtype);

            dr = objSQLHelper.ExecuteReaderSP("PAYROLL_GET_PAY_LEVEL_INFO", objParams);
        }
        catch (Exception ex)
        {
            return dr;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAppointno-> " + ex.ToString());
        }
        return dr;
    }

    public long InsertPayLevel(int payLevelno, int Ua_no, int staff_no, string PayLevelname, string paylevelsrno, DataTable dttbl, string TaFormula, ref string Message)
    {
        long pkid = 0;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[8];

            objParams[0] = new SqlParameter("@P_PAYLEVEL_NO", payLevelno);

            objParams[1] = new SqlParameter("@P_UA_NO", Ua_no);
            objParams[2] = new SqlParameter("@P_STAFF_NO", staff_no);
            objParams[3] = new SqlParameter("@P_PAY_LEVEL_NAME", PayLevelname);
            objParams[4] = new SqlParameter("@P_PAY_LEVEL_SRNO", paylevelsrno);
            objParams[5] = new SqlParameter("@P_Tbl_var", dttbl);
            objParams[6] = new SqlParameter("@P_TAFoarmula", TaFormula);

            objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[7].Direction = ParameterDirection.Output;

            object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_PAYROLL_PAYLEVEL_INSERT]", objParams, true);
            if (ret != null)
            {
                if (ret.ToString().Equals("-99"))
                    Message = "Transaction Failed!";
                else
                    pkid = Convert.ToInt64(ret.ToString());
            }
            else
            {
                pkid = -99;
                Message = "Transaction Failed!";
            }
        }
        catch (Exception ee)
        {
            pkid = -99;
        }
        return pkid;
    }

}