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
using System.Globalization;

public partial class ESTATE_Transaction_quarter_vacant : System.Web.UI.Page
{
    Common objCommon = new Common();
    QuarterVacant objQuarterVacant = new QuarterVacant();
    QuarterVacantCont objQrtVacantCont = new QuarterVacantCont();
    static DataTable Material = new DataTable();

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
                CreateDataTable();
                ViewState["action"] = "add";
                //BindMaterialMaster();
                binddropdownlist();
                //btnSubmit.Visible = false;
                //btnAddmetr.Visible = false;
                //fdsMaterial.Visible = false;
                //btnSubmit.Visible = false;
                //fdsMaterial.Visible = false;
            }
         }
        divMsg.InnerHtml = string.Empty;
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

    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_QRT_VACANT", "*", " ", "VACATION_ORDERNO ='" + txtvacationOrdNo.Text + "'", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string materialName = string.Empty;
        DataSet ds = (DataSet)ViewState["DS"];
        DataTable dtTable = ds.Tables[0];
        //(DataTable)Session["Presc"];
        DataRow row = dtTable.NewRow();
        foreach (RepeaterItem item in rpt_shortMaterials.Items)
        {
            Label lblmaterial = item.FindControl("lblmaterialName") as Label;
            Label lblSubItem = item.FindControl("lblallotQty") as Label;
            Label lblUnitPrice = item.FindControl("lblshorQty") as Label;
            Label lblQty = item.FindControl("lblfine") as Label;

            materialName += lblmaterial.ToolTip + ",";
            //subitemnos += lblSubItem.ToolTip + ",";
        }
        string[] split = materialName.Split(',');
        string str = ddlMaterial.SelectedValue;
        bool f = false;
        foreach (string str1 in split)
        {
            if (str1 == str)
            {
                objCommon.DisplayMessage(this, "Record Already Exist.", this.Page);
                f = true;
                break;
            }
        }
        if (f == false)
        {
            row["IDNO"] = Convert.ToInt16(ddlMaterial.SelectedValue);
            row["MNAME"] = Convert.ToString(ddlMaterial.SelectedItem.Text);
            row["MATERIAL_ID"] = Convert.ToString(ddlMaterial.SelectedValue);
            row["ALLOT_QTY"] = txtallotedQty.Text;
            row["SHORT_QTY"] = txtshortQty.Text;
            row["FINE"] = txtfine.Text;
            dtTable.Rows.Add(row);
            Session["Presc"] = dtTable;
            ViewState["DS"] = ds;
        }

        if (dtTable.Rows.Count > 0)
        {
            rpt_shortMaterials.DataSource = dtTable;
            rpt_shortMaterials.DataBind();
            rpt_shortMaterials.Visible = true;
            ddlMaterial.ClearSelection();
            txtallotedQty.Text = string.Empty;
            txtfine.Text = string.Empty;
            txtfineRemark.Text = string.Empty;
            txtshortQty.Text = string.Empty;
        }
        else
        {
            rpt_shortMaterials.Visible = false;
        }
    }

    protected void clearselection()
    {
        ddlquarterType.SelectedIndex = 0;
        ddlvacatorName.SelectedIndex = 0;
        ddlquarterNo.SelectedIndex = 0;
        ddlquarterType.SelectedIndex = 0;         
        txtallotedQty.Text = string.Empty;
        txtfine.Text = string.Empty;
        txtfineRemark.Text = string.Empty;
        txtshortQty.Text = string.Empty;
        txtvacationOrdNo.Text = string.Empty;
        txtDtOfVacant.Text = string.Empty;
        txtoffceOrderDate.Text = string.Empty;
        chlsubmsnMaterial.Checked = false;
        DivchlsubmsnMaterial.Visible = false;
        fdsMaterial.Visible = false;
        rpt_shortMaterials.DataSource = null;
        rpt_shortMaterials.DataBind();
        fldshortmat.Visible = false;    
        ddlMaterial.ClearSelection();
        ViewState["action"] = "add";
        ddlVacatortype.SelectedIndex = 0;
    }

    protected void clearSelectionOnVacator()
    {
        txtallotedQty.Text = string.Empty;
        txtfine.Text = string.Empty;
        txtfineRemark.Text = string.Empty;
        txtshortQty.Text = string.Empty;
        txtvacationOrdNo.Text = string.Empty;
        txtDtOfVacant.Text = string.Empty;
        txtoffceOrderDate.Text = string.Empty;
        chlsubmsnMaterial.Checked = false;
        DivchlsubmsnMaterial.Visible = false;
        fdsMaterial.Visible = false;
       
        ddlMaterial.ClearSelection();
        ddlquarterNo.ClearSelection();
        ddlquarterType.ClearSelection();
        rpt_shortMaterials.DataSource = null;
        rpt_shortMaterials.DataBind();
        fldshortmat.Visible = false;
        // btnSave.Visible = true ;
        btnCancel.Visible = true ;
        ddlVacatortype.ClearSelection();
        ddlvacatorName.ClearSelection();
    }

    protected void binddropdownlist()
    {
        try
        {
            objCommon.FillDropDownList(ddlVacatortype, "EST_CONSUMERTYPE_MASTER", "IDNO", "CONSUMERTYPE_NAME", "IDNO>0", "IDNO");
            objCommon.FillDropDownList(ddlMaterial, "EST_MATERIAL_MST", "MNO", "MNAME", "MNO>0", "MNO");
            objCommon.FillDropDownList(ddlquarterType, "EST_QRT_TYPE", "QNO", "QUARTER_TYPE", "QNO>0", "QNO");
            objCommon.FillDropDownList(ddlquarterNo, "EST_QRT_MST", "IDNO", "QUARTER_NO", "IDNO>0", "IDNO");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected void ddlVacatortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        // (CASE WHEN CONSUMERTYPE=1 THEN PE.TITLE ELSE C.TITLE END) + ' ' + (CASE WHEN CONSUMERTYPE=1 THEN PE.FNAME ELSE C.CONSUMERFULLNAME END) AS NAME
        // objCommon.FillDropDownList(ddlvacatorName, "EST_QRT_ALLOTMENT Q LEFT JOIN EST_CONSUMER_MST C  ON(C.IDNO = Q.NAME_ID AND C.CONSUMERTYPE = Q.EMPTYPE_ID AND C.CHKSTATUS='A') LEFT JOIN PAYROLL_EMPMAS E ON (Q.NAME_ID = E.IDNO AND Q.EMPTYPE_ID <> 2)", "Q.NAME_ID AS IDNO", "ISNULL(C.TITLE +' '+C.CONSUMERFULLNAME, E.PFILENO + ' - ' + E.TITLE + ' ' + E.FNAME) AS NAME", "Q.EMPTYPE_ID =" + ddlVacatortype.SelectedValue + "AND Q.QRT_STATUS IS NULL", "Q.NAME_ID");
        objCommon.FillDropDownList(ddlvacatorName, "EST_QRT_ALLOTMENT Q LEFT JOIN EST_CONSUMER_MST C  ON(C.IDNO = Q.NAME_ID AND C.CONSUMERTYPE = Q.EMPTYPE_ID AND C.CHKSTATUS='A') LEFT JOIN PAYROLL_EMPMAS E ON (Q.NAME_ID = E.IDNO AND Q.EMPTYPE_ID <> 2)", "Q.NAME_ID AS IDNO", "(CASE WHEN CONSUMERTYPE=1 THEN E.TITLE ELSE C.TITLE END) + ' ' + (CASE WHEN CONSUMERTYPE=1 THEN E.FNAME ELSE C.CONSUMERFULLNAME END) AS NAME", "Q.EMPTYPE_ID =" + ddlVacatortype.SelectedValue + "AND Q.QRT_STATUS IS NULL", "Q.NAME_ID");                // Changes by Vish on 15032022
    }

    protected void ddlvacatorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            // (CASE WHEN CONSUMERTYPE=1 THEN PE.TITLE ELSE C.TITLE END) + ' ' + (CASE WHEN CONSUMERTYPE=1 THEN PE.FNAME ELSE C.CONSUMERFULLNAME END) AS NAME
           // ds = objCommon.FillDropDown("EST_QRT_OCCUPANT", "OFFCE_ORDER_DT,ALLOT_DT,QTYPE_ID,QNO_ID", "QCCU_ID", "NAME_ID=" + Convert.ToInt32(ddlvacatorName.SelectedValue) + " AND EMPTYPE_ID=" + Convert.ToInt32(ddlVacatortype.SelectedValue), "QCCU_ID");
            ds = objCommon.FillDropDown("EST_QRT_ALLOTMENT Q INNER JOIN EST_ADDMETER A ON (Q.QA_ID = A.QA_ID) ", "Q.OFFCE_ORDER_DT, Q.ALLOTMENT_DT, A.QRTTYPE_ID, A.QRTNO_ID", "Q.QA_ID", "Q.NAME_ID=" + Convert.ToInt32(ddlvacatorName.SelectedValue) + " AND A.QRT_STATUS IS NULL AND Q.EMPTYPE_ID=" + Convert.ToInt32(ddlVacatortype.SelectedValue), "Q.QA_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlquarterType.SelectedValue = ds.Tables[0].Rows[0]["QRTTYPE_ID"].ToString();
                ddlquarterNo.SelectedValue = ds.Tables[0].Rows[0]["QRTNO_ID"].ToString();
                ddlquarterType.Enabled = false;
                ddlquarterNo.Enabled = false;
                objCommon.FillDropDownList(ddlMaterial, "EST_ADDMATERIAL A INNER JOIN EST_MATERIAL_MST B  ON (A.MNO=B.MNO)", "A.MNO", "B.MNAME", "A.QCCU_ID=" + ds.Tables[0].Rows[0]["QCCU_ID"].ToString(), "A.MNO");
                GetVacatorInfo(Convert.ToInt16(ddlvacatorName.SelectedValue), Convert.ToInt32(ddlVacatortype.SelectedValue));
                BindReapeater(Material);
            }
            else
            {
                clearSelectionOnVacator();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void GetVacatorInfo(int VactorID, int EMPTYPE_ID)
    {
        try
        {
             DataSet ds = null;
             ds = objCommon.FillDropDown("EST_QRT_VACANT", "VACATION_ORDERNO,DATE_OF_VACATION,OFFCE_ORDER_DT", "NAME_ID", "NAME_ID=" + VactorID + " AND EMPTYPE_ID=" + EMPTYPE_ID, "NAME_ID");
             if (ds.Tables[0].Rows.Count > 0)
             {
                 txtvacationOrdNo.Text = ds.Tables[0].Rows[0]["VACATION_ORDERNO"].ToString();
                 txtDtOfVacant.Text = ds.Tables[0].Rows[0]["DATE_OF_VACATION"].ToString();
                 txtoffceOrderDate.Text = ds.Tables[0].Rows[0]["OFFCE_ORDER_DT"].ToString();
                 BindReapeater(Material);
             }
             else
             {
                 ddlMaterial.ClearSelection();
                 txtallotedQty.Text = string.Empty;
                 txtfine.Text = string.Empty;
                 txtfineRemark.Text = string.Empty;
                 txtshortQty.Text = string.Empty;
                 rpt_shortMaterials.DataSource = null;
                 rpt_shortMaterials.DataBind();
                 btnSave.Visible   = true;
                 btnCancel.Visible = true;
             }       
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally 
        {
         
        }
    }

    protected void chlsubmsnMaterial_CheckedChanged(object sender, EventArgs e)
    {
        //chlsubmsnMaterial.Enabled = false;

        if (chlsubmsnMaterial.Checked.Equals(true))
        {
            fdsMaterial.Visible = true;
            //pnlshortage.Visible = false;
            //fldshortmat.Visible = false;
            //btnSubmit.Visible = false;
        }
        else
        {
            chlsubmsnMaterial.Checked = false;
            DivchlsubmsnMaterial.Visible = false;
            fdsMaterial.Visible = false;
        }

    }

    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_QRT_OCCUPANT", "QCCU_ID", "NAME_ID", "NAME_ID=" + ddlvacatorName.SelectedValue + " AND EMPTYPE_ID=" + Convert.ToInt32(ddlVacatortype.SelectedValue), "QCCU_ID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds = objCommon.FillDropDown("EST_ADDMATERIAL", "QNT", "MNO", "MNO=" + ddlMaterial.SelectedValue + "and QCCU_ID=" + ds.Tables[0].Rows[0]["QCCU_ID"].ToString(), "MNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtallotedQty.Text = ds.Tables[0].Rows[0]["QNT"].ToString();
                }
                else
                {
                    txtallotedQty.Text = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    
    protected void rpt_shortMaterials_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            int recordno = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Delete")
            {
                //DataSet ds = (DataSet)ViewState["DS"];
                DataSet ds = (DataSet)ViewState["DS"];
                DataTable dt = ds.Tables[0];
                DataView dv = new DataView(dt);
                dv.RowFilter = "IDNO <>" + recordno;
                rpt_shortMaterials.DataSource = dv.ToTable();
                rpt_shortMaterials.DataBind();
                rpt_shortMaterials.Visible = true;
                //Session["Presc"] = null;
                //Session["Presc"] = dv.ToTable();
                DataSet dsr = new DataSet();
                dsr.Tables.Add(dv.ToTable().Copy());
                ViewState["DS"] = dsr;
                if (rpt_shortMaterials.Items.Count == 0)
                {
                    rpt_shortMaterials.DataSource = null;
                    rpt_shortMaterials.DataBind();
                    rpt_shortMaterials.Visible = false;
                    fldshortmat.Visible = false;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            objQuarterVacant.EmpID = Convert.ToInt16(ddlVacatortype.SelectedItem.Value);
            objQuarterVacant.NameOfVacator = Convert.ToInt16(ddlvacatorName.SelectedItem.Value);
            objQuarterVacant.QuarterType = Convert.ToInt16(ddlquarterType.SelectedItem.Value);
            objQuarterVacant.QuarterNo = Convert.ToInt16(ddlquarterNo.SelectedItem.Value);            
            objQuarterVacant.VacationDT = DateTime.Parse(txtDtOfVacant.Text.Trim());

            objQuarterVacant.VacationOrderNo = txtvacationOrdNo.Text == string.Empty ? "" : txtvacationOrdNo.Text;
            if (txtoffceOrderDate.Text == string.Empty)
            {
                objQuarterVacant.OffceOrdetDt = DateTime.MinValue;
            }
            else
            {
                objQuarterVacant.OffceOrdetDt = DateTime.ParseExact(txtoffceOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); // DateTime.Parse(txtoffceOrderDate.Text.Trim()); 
            }

            if (chlsubmsnMaterial.Checked.Equals(true))
            {
                string materialName = string.Empty;
                string allotQty = string.Empty;
                string shortQty = string.Empty;
                string fine = string.Empty;
                foreach (RepeaterItem item in rpt_shortMaterials.Items)
                {

                    Label lblmaterial = item.FindControl("lblmaterialName") as Label;
                    Label lblallotqty= item.FindControl("lblallotQty") as Label;
                    Label lblshortqty = item.FindControl("lblshorQty") as Label;
                    Label lblFine = item.FindControl("lblfine") as Label;


                    materialName += lblmaterial.ToolTip +",";
                    //subitemnos += lblSubItem.ToolTip + ",";
                    allotQty += lblallotqty.Text + ",";
                    shortQty += lblshortqty.Text + ",";
                    fine += lblFine.Text + ",";

                }
                //string temp = materialName.Substring(0, materialName.LastIndexOf(","));

                objQuarterVacant.MaterialId = materialName.Substring(0, materialName.LastIndexOf(","));
                objQuarterVacant.AlloterQTY = allotQty.Substring(0, allotQty.LastIndexOf(","));
                objQuarterVacant.ShortQTY = shortQty.Substring(0, shortQty.LastIndexOf(","));
                objQuarterVacant.Fine = fine.Substring(0, fine.LastIndexOf(","));

                 //objQuarterVacant.Fine_Remark =materialName.Substring(0, materialName.LastIndexOf(","));
                    //objQuarterVacant.MaterialId  = materialName;
                    //objQuarterVacant.AlloterQTY  = allotQty;
                    //objQuarterVacant.ShortQTY    = shortQty;
                    //objQuarterVacant.Fine        = fine;
                    //objQuarterVacant.Fine_Remark =               
                //objQuarterVacant.MaterialId = Convert.ToInt16(ddlMaterial.SelectedItem.Value).Equals(string.Empty) ? 0: Convert.ToInt16(ddlMaterial.SelectedItem.Value);
                //objQuarterVacant.AlloterQTY = Convert.ToInt16(txtallotedQty.Text.Trim()).Equals(string.Empty) ? 0 : Convert.ToInt16(txtallotedQty.Text.Trim());
                //objQuarterVacant.ShortQTY = Convert.ToInt16(txtshortQty.Text.Trim()).Equals(string.Empty) ? 0 : Convert.ToInt16(txtshortQty.Text.Trim());
                //objQuarterVacant.Fine = Convert.ToDouble(txtfine.Text.Trim()).Equals(string.Empty) ? 0 : Convert.ToDouble(txtfine.Text.Trim());
                objQuarterVacant.Fine_Remark = txtfineRemark.Text.Trim().Equals(string.Empty) ? null : txtfineRemark.Text.Trim();

            }
            else
            {
                objQuarterVacant.MaterialId = string.Empty;
                objQuarterVacant.AlloterQTY = string.Empty;
                objQuarterVacant.ShortQTY = string.Empty;
                objQuarterVacant.Fine = string.Empty;
                objQuarterVacant.Fine_Remark = string.Empty;
            }
            if (ViewState["action"].Equals("add"))
            {
                if (funDuplicate() == true)
                {
                    objCommon.DisplayMessage(updpnl, "Vacation Order No. Already Exist.", this.Page);
                    txtvacationOrdNo.Text = "";
                    return;
                }
                objQuarterVacant.MNO = 0;
                CustomStatus cs = (CustomStatus)objQrtVacantCont.AddQuarterVacant(objQuarterVacant);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this.updpnl, "Record Save Successfully.", this.Page);
                    clearSelectionOnVacator();
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnl, "Sorry!Try again.", this.Page);
                }
            }
            if (ViewState["action"].Equals("edit"))
            {
                if (funDuplicate() == true)
                {
                    objCommon.DisplayMessage(updpnl, "Vacation Order No. Already Exist.", this.Page);
                    txtvacationOrdNo.Text = "";
                    return;
                }
                objQuarterVacant.MNO = 1;
                CustomStatus cs = (CustomStatus)objQrtVacantCont.AddQuarterVacant(objQuarterVacant);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updpnl, "Record Updated Successfully.", this.Page);
                    clearSelectionOnVacator();
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnl, "Sorry!Try again.", this.Page);
                }               
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void BindReapeater(DataTable dt)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_QRT_VACANT", "QV_ID", "NAME_ID", "NAME_ID=" + Convert.ToInt32(ddlvacatorName.SelectedValue) + " AND EMPTYPE_ID=" + Convert.ToInt32(ddlVacatortype.SelectedValue), "QV_ID");
            if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["QV_ID"].ToString()))
            {
                ds = objCommon.FillDropDown("EST_ADDSHORT_MATERIAL A INNER JOIN EST_MATERIAL_MST B on(A.MATERIAL_ID =B.MNO)", "A.MATERIAL_ID,B.MNAME,A.ALLOT_QTY,A.SHORT_QTY,A.FINE", "A.IDNO", "QV_ID=" + Convert.ToInt16(ds.Tables[0].Rows[0]["QV_ID"].ToString()), "A.IDNO");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    // btnAddmetr.Visible = true;
                    ViewState["DS"] = ds;
                    rpt_shortMaterials.DataSource = ds;
                    rpt_shortMaterials.DataBind();
                    fldshortmat.Visible = true;
                    //btnSubmit.Visible = true;
                    //btnClear.Visible = true;
                    chlsubmsnMaterial.Checked = true;
                    DivchlsubmsnMaterial.Visible = true;
                    fdsMaterial.Visible = true;
                    //btnSave.Visible = false;
                    //btnCancel.Visible = false;
                    ViewState["action"] = "edit";
                }
                else
                {
                    rpt_shortMaterials.DataSource = null;
                    rpt_shortMaterials.DataBind();
                    //btnSubmit.Visible = false;
                    //fdsMaterial.Visible = false;
                    //btnSave.Visible = true;
                    //btnCancel.Visible = true;
                    //btnClear.Visible =false;
                    ViewState["action"] = "edit";
                    ViewState["DS"] = null;
                    ViewState["DS"] = ds;
                }
            }
            else
            {
                rpt_shortMaterials.DataSource = null;
                rpt_shortMaterials.DataBind();
                ViewState["DS"] = null;
                ViewState["DS"] = ds;
                ViewState["DS"] = CreateDataTable();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMaterial.ClearSelection();
        txtallotedQty.Text = string.Empty;
        txtfine.Text = string.Empty;
        txtfineRemark.Text = string.Empty;
        txtshortQty.Text = string.Empty;
        rpt_shortMaterials.DataSource = null;
        rpt_shortMaterials.DataBind();
    }

    //protected void btnprint_Click(object sender, EventArgs e)
    //{
    //    showReport("Print Vacant History", "rptQuarterVacant.rpt");
    //}
    //protected void showReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        if (ddlvacatorName.SelectedValue.Equals(0))
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=" + reportTitle;
    //            url += "&path=~,Reports,ESTATE," + rptFileName;
    //            url += "&param=@p_college_code=" + Session["colcode"].ToString() + ",@P_EMP_ID=" + Convert.ToInt16(ddlvacatorName.SelectedValue);

    //            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //            divMsg.InnerHtml += " </script>";

    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //            sb.Append(@"window.open('" + url + "','','" + features + "');");

    //            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
    //        }
           
         
    //    }
    //    catch(Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());

    //    }



    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearselection();
    }

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
        column.ColumnName = "MATERIAL_ID";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);



        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ALLOT_QTY";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "SHORT_QTY";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "FINE";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);
        DataSet ds = new DataSet();
        DataTable dtTable = new DataTable();

        ds.Tables.Add(dt);



        //add column to table
        // dt.Columns.Add(column);

        return ds;
    }
}

