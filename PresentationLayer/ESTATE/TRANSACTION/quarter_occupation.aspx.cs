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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Transaction_quarter_occupation : System.Web.UI.Page
{

    Common objcommon = new Common();
    QuarterOccupantController objOcupant = new QuarterOccupantController();
    Quarter_Occupant objOcuEnt = new Quarter_Occupant();
    static  DataTable Material = new DataTable();

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
                CheckPageAuthorization();
                ViewState["action"] ="add";
                //BindMaterialMaster();
                //BindReapeater();
                //ViewState["DS"] = null;
                CreateDataTable();
                binddropdownlist();
                btnSaveMaterial.Visible = true;
                ddlquarterType.Enabled = false;
                ddlquarterNo.Enabled = false;
                ViewState["QA_ID"] = null;
            }
           //Session["Presc"] = null;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    protected void binddropdownlist()
    {
        objcommon.FillDropDownList(ddlEmptype, "EST_CONSUMERTYPE_MASTER", "IDNO", "CONSUMERTYPE_NAME", "IDNO>0", "IDNO");         
        // objcommon.FillDropDownList(ddlquarterType, "EST_QRT_TYPE", "QNO", "QUARTER_TYPE", "QNO>0", "QNO");         
        //  objcommon.FillDropDownList(ddlquarterNo, "EST_QRT_MST", "QNO", "QUARTER_NO", "QNO>0", "QNO");
        objcommon.FillDropDownList(ddlMaterial, "EST_MATERIAL_MST", "MNO", "MNAME", "MNO>0","MNO");
    }
    
    protected void Repeater_meterialtype_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       try{    

            int recordno = Convert.ToInt32(e.CommandArgument);
            //ViewState["recordno"] = "recordno";      

            if (e.CommandName == "delete")
            {
                //DataSet ds = (DataSet)ViewState["DS"];
                DataSet ds = (DataSet)ViewState["DS"];
                DataTable dt = ds.Tables[0];
                DataView dv  = new DataView(dt);
                dv.RowFilter = "IDNO <>" + recordno;
                Repeater_meterialtype.DataSource = dv.ToTable();
                Repeater_meterialtype.DataBind();
                Repeater_meterialtype.Visible = true;
                //Session["Presc"] = null;
                //Session["Presc"] = dv.ToTable();
                DataSet dsr = new DataSet();
                dsr.Tables.Add(dv.ToTable().Copy());
                ViewState["DS"] = dsr;
                if (Repeater_meterialtype.Items.Count == 0)
                {
                    Repeater_meterialtype.DataSource = null;
                    Repeater_meterialtype.DataBind();
                    Repeater_meterialtype.Visible = false;
                }
                //Repeater_meterialtype.DataSource = Session["Presc"];
                //Repeater_meterialtype.DataBind();
                //Session["Presc"] = dv.ToTable();
                // objCom.DisplayMessage(updPnl, "Item Deleted", this);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }

    protected void ddlEmptype_SelectedIndexChanged(object sender, EventArgs e)
    {       
        //objcommon.FillDropDownList(ddlEmpName, "EST_CONSUMER_MST", "IDNO", "FNAME AS NAME", "ChkStatus='A' AND CONSUMERTYPE=" + ddlEmptype.SelectedItem.Value + "and IDNO IN(SELECT NAME_ID from EST_QRT_ALLOTMENT where QRT_STATUS is null)", "IDNO");
        DataSet ds = objOcupant.FillOccupantName(Convert.ToInt32(ddlEmptype.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlEmpName.Items.Clear();
            ddlEmpName.Items.Add("Please Select");
            ddlEmpName.DataTextField = "NAME";
            ddlEmpName.DataValueField = "QA_ID";
            ddlEmpName.DataSource = ds;
            ddlEmpName.DataBind();
        }
    }
    
    protected void btnOccupantSave_Click(object sender, EventArgs e)
    {
        if (ddlquarterType.SelectedIndex < 0)
        {
            objcommon.DisplayMessage(this, "Please Select Occupant Name.", this.Page);
            return;
        }

        if (Repeater_meterialtype.Items.Count == 0)
        {
            objcommon.DisplayMessage(this, "Quarter is Not Alloted to this Consumer.", this.Page);
            return;
        }

        string materialName = string.Empty;
        string qty = string.Empty;              
        objOcuEnt.EmpID          = Convert.ToInt16(ddlEmptype.SelectedItem.Value);
        objOcuEnt.OccupantName = Convert.ToInt32(ViewState["NAME_ID"]);//Convert.ToInt16(ddlEmpName.SelectedItem.Value);
        objOcuEnt.OfficeOrder_Dt = Convert.ToDateTime(txtoffceOrderDt.Text);
        objOcuEnt.Allotment_Dt   = Convert.ToDateTime(txtAllotDT.Text);
        objOcuEnt.QrtType_Id     = Convert.ToInt16(ddlquarterType.SelectedItem.Value);
        objOcuEnt.Qrt_No         = Convert.ToInt16(ddlquarterNo.SelectedItem.Value);   
        //objOcuEnt.OccupantName   = Convert.ToInt16(ddlEmpName.SelectedItem.Value);
        objOcuEnt.QA_ID = Convert.ToInt32(ViewState["QA_ID"]);
    

            foreach (RepeaterItem item in Repeater_meterialtype.Items)
            {               
                Label lblmaterial = item.FindControl("lblmaterialtype") as Label;             
                Label lblQty = item.FindControl("lblqty") as Label;

                materialName += lblmaterial.ToolTip+ ",";                 
                qty += lblQty.Text + ",";                
            }            
      
            objOcuEnt.Material = materialName;
            objOcuEnt.Quantity  = qty;  
            if (ViewState["action"].Equals("add"))
            {
                objOcuEnt.MNO = 0;
                CustomStatus cs = (CustomStatus)objOcupant.AddQuarterOccupant(objOcuEnt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objcommon.DisplayMessage(this, "Record Save Successfully.", this.Page);
                    clearselection();
                }
                else
                {
                    objcommon.DisplayMessage(this, "Sorry!Try again.", this.Page);
                }
            }
           if (ViewState["action"].Equals("edit"))    
           {
            objOcuEnt.MNO = 1;
            CustomStatus cs = (CustomStatus)objOcupant.AddQuarterOccupant(objOcuEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objcommon.DisplayMessage(this, "Record Updated Successfully.", this.Page);
                clearselection();
            }
            else
            {
                objcommon.DisplayMessage(this, "Sorry!Try again.", this.Page);
            }
        }
    }

    protected void btnSaveMaterial_Click(object sender, EventArgs e)
    {
        //DataSet ds = null;
        // ds = objcommon.FillDropDown("EST_ADDMATERIAL A INNER JOIN EST_MATERIAL_MST B on(A.MNO =B.MNO)", "B.MNAME,A.MNO,A.QNT", "A.IDNO", "QCCU_ID=" + Convert.ToInt16(ds.Tables[0].Rows[0]["QCCU_ID"].ToString()) + "and QRT_STATUS is null", "A.IDNO");

        string materialName = string.Empty;
        DataSet ds = (DataSet)ViewState["DS"];
        DataTable dtTable = ds.Tables[0];
        //(DataTable)Session["Presc"];
        DataRow row  = dtTable.NewRow();
        foreach (RepeaterItem item in Repeater_meterialtype.Items)
            {               
                Label lblmaterial = item.FindControl("lblmaterialtype") as Label;
                //Label lblSubItem = item.FindControl("lblSubitemName") as Label;
                //Label lblUnitPrice = item.FindControl("lblUnitPrice") as Label;
                Label lblQty = item.FindControl("lblqty") as Label;

                materialName += lblmaterial.ToolTip+ ",";
                   //subitemnos += lblSubItem.ToolTip + ",";
               
            }
        string[] split = materialName.Split(',');
        string str=ddlMaterial.SelectedValue;
        bool f=false;
        foreach (string str1 in split)
        {
            if (str1 == str)
            {
                objcommon.DisplayMessage(this, "Record Already Exist.", this.Page);
                f=true;
                break;
            }
          
        }
           if (f == false)
           {
            row["IDNO"] = Convert.ToInt16(ddlMaterial.SelectedValue);
            row["MNAME"] = Convert.ToString(ddlMaterial.SelectedItem.Text);
            row["MNO"] = Convert.ToString(ddlMaterial.SelectedValue);
            row["QNT"] = txtQuantity.Text; 
            dtTable.Rows.Add(row);
            Session["Presc"] = dtTable;
            ViewState["DS"] = ds;
           }
            
        if (dtTable.Rows.Count > 0)
        {
            Repeater_meterialtype.DataSource = dtTable;
            Repeater_meterialtype.DataBind();
            Repeater_meterialtype.Visible = true;
            ddlMaterial.ClearSelection();
            txtQuantity.Text = string.Empty;
        }
        else
        {
            Repeater_meterialtype.Visible = false;
        }  

    }
 
    protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindReapeater(Material);
            GetMaterialInfo();        
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void GetMaterialInfo()
    {
        try
        {
            DataSet ds = null;
           // ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT", "QA_ID", "NAME_ID", "NAME_ID=" + ddlEmpName.SelectedItem.Value+"and QRT_STATUS is null", "QA_ID");
            ds = objcommon.FillDropDown("EST_QRT_ALLOTMENT", "QA_ID", "NAME_ID", "QA_ID=" + ddlEmpName.SelectedItem.Value + "and QRT_STATUS is null", "");
            
            if (ds!=null&& ds.Tables[0].Rows.Count > 0)
            {
                int Q_ID = 0;
                Q_ID = Convert.ToInt16(ds.Tables[0].Rows[0]["QA_ID"].ToString());
                ViewState["QA_ID"] = Q_ID.ToString();
                ViewState["NAME_ID"] = Convert.ToInt16(ds.Tables[0].Rows[0]["NAME_ID"].ToString());

                DataSet dsAllot = objcommon.FillDropDown("EST_ADDMETER A INNER JOIN EST_QRT_MST C  ON (A.QRTNO_ID =C.IDNO)INNER JOIN EST_QRT_TYPE D ON (D.QNO = A.QRTTYPE_ID)INNER JOIN EST_QRT_ALLOTMENT E ON(A.QA_ID =E.QA_ID)", "top 1 C.IDNO AS QRTNO,  C.QUARTER_NO, D.QNO AS QRTYPENO ,D.QUARTER_TYPE", " cast(E.OFFCE_ORDER_DT as date)As OFF_DT , cast(E.ALLOTMENT_DT as date) as ALLOT_DT", "A.QA_ID="+Q_ID+"and a.QRT_STATUS is null", "A.QA_ID");

                if (dsAllot != null && dsAllot.Tables[0].Rows.Count > 0)
                {                    
                    txtoffceOrderDt.Text = dsAllot.Tables[0].Rows[0]["OFF_DT"].ToString().Substring(0, 10);
                    txtAllotDT.Text = dsAllot.Tables[0].Rows[0]["ALLOT_DT"].ToString().Substring(0, 10); 
                    
                    ddlquarterType.DataTextField = dsAllot.Tables[0].Columns["QUARTER_TYPE"].ToString();
                    ddlquarterType.DataValueField = dsAllot.Tables[0].Columns["QRTYPENO"].ToString();
                    ddlquarterType.DataSource = dsAllot;
                    ddlquarterType.DataBind();
                    
                    ddlquarterNo.DataTextField ="QUARTER_NO";
                    ddlquarterNo.DataValueField = "QRTNO";
                    ddlquarterNo.DataSource = dsAllot;
                    ddlquarterNo.DataBind();
                      
                }
                else
                { //clear selection

                }
            }
            else
            { clearselection(); }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }  
    }

    protected void BindReapeater(DataTable dt)
    {
        try
        {
            DataSet ds = null;
            ds = objcommon.FillDropDown("EST_QRT_OCCUPANT", "QCCU_ID", "NAME_ID", "QA_ID=" + ddlEmpName.SelectedItem.Value + "and QRT_STATUS is null", "QCCU_ID");
            if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["QCCU_ID"].ToString()))
            {
                ds = objcommon.FillDropDown("EST_ADDMATERIAL A INNER JOIN EST_MATERIAL_MST B on(A.MNO =B.MNO)", "B.MNAME,A.MNO,A.QNT", "A.IDNO", "QCCU_ID=" + Convert.ToInt16(ds.Tables[0].Rows[0]["QCCU_ID"].ToString()) + "and QRT_STATUS is null", "A.IDNO");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    // btnAddmetr.Visible = true;
                    ViewState["DS"] = ds;
                    Repeater_meterialtype.DataSource = ds;
                    Repeater_meterialtype.DataBind();
                    //Repeater_meterialtype.DataSource = dt;
                    //Repeater_meterialtype.DataBind();
                   // btnSaveMaterial.Visible = true;
                    //btnOccupantSave.Visible = false;
                    // btnOccupantCancel.Visible = false;
                    ViewState["action"] = "edit";                   
                }
                else
                {
                    Repeater_meterialtype.DataSource = null;
                    Repeater_meterialtype.DataBind();
                    ViewState["action"] = "edit";
                    ViewState["DS"] = null;
                    ViewState["DS"] = ds;
                   // btnSaveMaterial.Visible = false;
                    btnOccupantSave.Visible = true;
                    //btnOccupantCancel.Visible = true;
                }
            }
            else
            {
                ViewState["DS"] = null;
                ViewState["DS"] = ds;
                Repeater_meterialtype.DataSource = null;
                Repeater_meterialtype.DataBind();  
      
                ViewState["DS"] = CreateDataTable();
                //btnSaveMaterial.Visible = false;
                btnOccupantSave.Visible = true;
                btnOccupantCancel.Visible = true;
                Repeater_meterialtype.Visible = false;
                ViewState["action"] = "add";
                //clearselection();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
            //Repeater_meterialtype.DataSource = dt;
            //Repeater_meterialtype.DataBind();        
    }

    protected void clearselection()
    {
        try
        {
            ViewState["action"] = "add";
            //DataTable dt = new DataTable();
            //dt = (DataTable)Session["Presc"];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows.RemoveAt(i);
            //}
            //Session["Presc"] = dt;
            Repeater_meterialtype.DataSource = null;
            Repeater_meterialtype.DataBind();

            ddlEmptype.ClearSelection();
            ddlEmpName.ClearSelection();
            txtoffceOrderDt.Text = string.Empty;
            txtAllotDT.Text = string.Empty;
            ddlEmpName.ClearSelection();
            ddlquarterType.Items.Clear();
            ddlquarterNo.Items.Clear();
            Repeater_meterialtype.DataSource = null;
            Repeater_meterialtype.DataBind();
            ddlMaterial.ClearSelection();
            txtQuantity.Text = string.Empty;
            btnOccupantSave.Visible = true;
            ViewState["QA_ID"] = null;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }        
        // btnOccupantCancel.Visible = true;
    }

    protected void clearmaterial()
    {
        ddlMaterial.ClearSelection();
        txtQuantity.Text = string.Empty;
    }

    protected void btnOccupantCancel_Click(object sender, EventArgs e)
    {
        clearselection();
    }

    //this is code for create table and save all record in it.......
    private DataSet CreateDataTable()
    {
        //Create a Datatable and all the records
        DataTable dt = new DataTable();
        DataColumn column;

        //Add Header Columns
        //==================
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "IDNO";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);

        //Add Header Columns
        //==================
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "MNAME";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "MNO";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "QNT";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);
        DataSet ds = new DataSet();

        ds.Tables.Add(dt);
       
        //add column to table
       // dt.Columns.Add(column);
        
        return ds;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Assets Occupation", "rptAssetsOccupation.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updOccupantMaterial, this.updOccupantMaterial.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
