//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SEATING PLAN                                                             
// PAGE NAME     : ROOM CONFIGURATION                                  
// CREATION DATE : 12-MAR-2012                                                      
// CREATED BY    : ABHIJIT DESHPANDE  
// MODIFIED BY   : SACHIN A                                               
// MODIFIED DATE : 31/01/2023                                                                     
// MODIFIED DESC : SVCE CLIENT SEATING ARRANGEMENT INTEGRATE IN COMMON CODE && AS PER REQ MODIFICATION                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL; 
using DynamicAL_v2;

public partial class ACADEMIC_EXAMINATION_RoomConfig : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomConfigController objRC = new RoomConfigController();
    RoomConfig objRoom = new RoomConfig();
    DynamicControllerAL AL = new DynamicControllerAL();
    //int roomno=0;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                }
                PopulateDropDownList();
                ViewState["action"] = null;
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RoomConfig.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RoomConfig.aspx");
        }
    }
    #endregion

    #region Click Events

    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRoom.SelectedValue != "0")
            {
                int roomno = Convert.ToInt32(ddlRoom.SelectedValue);
                if (roomno > 0 && ddlRoom.SelectedIndex > 0)
                {
                    ShowDetails(roomno);
                }
                else
                {
                    lblMsg.Text = "ERROR!";
                    return;
                }
            }
            else
            {
                txtActual.Text = null;
                txtColumns.Text = null;
                txtId.Text = null;
                txtRoomCapacity.Text = null;
                txtRows.Text = null;
                lvFactors.DataSource = null;
                lvFactors.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.ddlRoom_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //CONFIGURE THE GRIDVIEW
    protected void btnConfigure_Click(object sender, EventArgs e)
    {
        BindGridview();
    }
    //SAVE RECORD
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (BindGridview())
            {
                int minus = 0;
                if (txtId.Text != "")
                {
                    for (int i = 0; i < txtId.Text.Split(',').GetLength(0); i++)
                    {
                        if (Convert.ToInt16(txtId.Text.Split(',')[i]) > Convert.ToInt16(txtRows.Text) * Convert.ToInt16(txtColumns.Text))
                        {
                            objCommon.DisplayMessage(this.updplRoom, "Bench ID " + txtId.Text.Split(',')[i] + " not in Configuration!!", this.Page);
                            return;
                        }
                    }
                    string[] str = txtId.Text.Split(',');

                    for (int i = 0; i < str.GetLength(0); i++)
                    {
                        if (str[i] != "0")
                        {
                            minus++;
                        }
                    }
                }
                objRoom.Room_No = Convert.ToInt32(ddlRoom.SelectedValue);
                objRoom.Room_name = ddlRoom.SelectedItem.Text;
                objRoom.Rows = Convert.ToInt32(txtRows.Text.Trim());
                objRoom.Columns = Convert.ToInt32(txtColumns.Text.Trim());
                objRoom.DisbStudId = txtId.Text.Trim();

                if (ViewState["action"].ToString() == "add")
                {
                    objRoom.Actual_Capacity = Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim()) - minus;
                }
                else if (ViewState["action"].ToString() == "edit")
                {
                    objRoom.Actual_Capacity = Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim()) - minus;
                }

                //objRoom.Actual_Capacity = ViewState["action"].ToString() == "add" ? Convert.ToInt32(txtActual.Text) - minus : ViewState["action"].ToString() == "edit" ? Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim()) - minus : Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim());

                objRoom.CollegeCode = Session["colcode"].ToString();

                if (ViewState["action"] != null)
                {
                    int Exam_Type = 2;
                    //if (rbInternal.Checked)
                    //    Exam_Type = 1;
                    //else
                    //    Exam_Type = 2;

                    string txtIDs = txtId.Text.Trim().Replace(',', '^');

                    string SP_Name = "PKG_ACD_ROOMCONFIG_CRUD";
                    string SP_Parameters = "@P_ROOM_NO ,@P_ROOM_NAME ,@P_ROWS ,@P_COLUMNS ,@P_ACTUAL_CAPACITY ,@P_DISABLED_ID ,@P_STATUS ,@P_EXAM_TYPE ,@P_OPERATION";
                    string Call_Values = "" + Convert.ToInt32(ddlRoom.SelectedValue) + "," + Convert.ToString(ddlRoom.SelectedItem.Text) + "," + Convert.ToInt32(txtRows.Text.Trim()) + "," + Convert.ToInt32(txtColumns.Text.Trim()) + "," + (Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim()) - minus) + "," + txtIDs + ",1," + Exam_Type + ",1";

                    string RetVal = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 2);


                    //Add/Update Room Config/Building Room
                    //CustomStatus cs = (CustomStatus)objRC.AddRoomConfig(objRoom);
                    if (RetVal=="1")
                    {
                        objCommon.DisplayMessage(this.updplRoom, "Record Saved Successfully!..", this.Page);
                        Clearall();
                    }
                    else if (RetVal=="2")
                    {
                        objCommon.DisplayMessage(this.updplRoom, "Record Updated Successfully!..", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updplRoom, "Error!..", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.btnSave_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ViewState["action"] = null;
    }

    #endregion

    #region Private Methods

    private void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN HOSTEL NAME
            objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "ROOMName");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Record should be Shown on the Listview
    private void BindListView()
    {
        try
        {
            RoomConfigController objRC = new RoomConfigController();
            DataSet ds = objRC.GetAllRooms();
            gvRoom.DataSource = ds;
            gvRoom.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clearall()
    {
        ddlRoom.SelectedIndex = 0;
        txtRows.Text = string.Empty;
        txtColumns.Text = string.Empty;
        txtActual.Text = string.Empty;
        gvRoom.DataSource = null;
        gvRoom.DataBind();
        txtId.Text = string.Empty;
        txtRoomCapacity.Text = string.Empty;
        btnSave.Visible = false;
        btnCancel.Visible = false;
       // pnlconfig.Visible = false;
    }

    //The Gridview where Bind by using Row and Columns   
    protected bool BindGridview()
    {
        try
        {
            //Create a Datatable and all the records
            DataTable dt = new DataTable("Room");
            DataColumn column;

            DataRow row;
            if (Convert.ToInt32(txtColumns.Text) * Convert.ToInt32(txtRows.Text) <= Convert.ToInt32(txtRoomCapacity.Text))
            {
                //Add Columns
                //==================
                for (int yIndex = 0; yIndex < Convert.ToInt32(txtColumns.Text); yIndex++)
                {
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "Column " + Convert.ToInt32(yIndex + 1).ToString();
                    dt.Columns.Add(column);
                }


                for (int nIndex = 0; nIndex < Convert.ToInt32(txtRows.Text); nIndex++)
                {
                    row = dt.NewRow();
                    dt.Rows.Add(row);
                }


                //Iterate through the columns of the datatable to set the data bound field dynamically.
                foreach (DataColumn col in dt.Columns)
                {
                    //Declare the bound field and allocate memory for the bound field.
                    TemplateField bfield = new TemplateField();
                    //Initalize the DataField value.
                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);

                    //Initialize the HeaderText field value.
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName);
                    //Add the newly created bound field to the GridView.
                    gvRoom.Columns.Add(bfield);
                }
                //Initialize the DataSource
                gvRoom.DataSource = dt;
                gvRoom.DataBind();

                //Added by Abhinay Lad [06-03-2020]
                string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

                int iloop = 1;
                int columncount = Convert.ToInt32(txtColumns.Text);
                int rowcount = Convert.ToInt32(txtRows.Text);
                int benchid = 1;
                int error = 0;
                string[] ID = txtId.Text.Split(',');

                rbExternal.Checked = true;     //Added by Sachin A dt on 25012023 because only external component seating arrangement Requirement in future internal component use 

                if (rbInternal.Checked)
                {
                    for (int i = 0; i < columncount; i++)
                    {
                        for (int j = 0; j < rowcount; j++)
                        {
                            (gvRoom.Rows[j].Cells[i].Controls[0] as Label).Text = "<b>Seat " + benchid + " <span style='color:black'>:</span>&nbsp;" + Alphabets[i] + "" + Convert.ToInt32(j + 1) + "</b>";  // TO DISPLAY 1 BENCHES IN A CELL
                            //(gvRoom.Rows[j].Cells[i].Controls[0] as Label).Width
                            (gvRoom.Rows[j].Cells[i].Controls[0] as Label).ToolTip = iloop.ToString() + (iloop + 1).ToString();
                            hdfbenchid.Value += iloop.ToString() + ',' + (iloop + 1).ToString() + ',';
                            for (int k = 0; k < txtId.Text.Split(',').GetLength(0) && txtId.Text.Split(',')[k] != ""; k++)
                                if (ID[k].Equals(benchid.ToString()))
                                    if ((gvRoom.Rows[j].Cells[i].Controls[0] as Label).BackColor == System.Drawing.Color.LightGray)
                                    {
                                        objCommon.DisplayMessage(this.updplRoom, "Repeated Bench ID!", this.Page);
                                        error++;
                                    }
                                    else
                                        (gvRoom.Rows[j].Cells[i].Controls[0] as Label).BackColor = System.Drawing.Color.LightGray; //HIGHLIGHT THE DISABLE BENCHES
                            iloop = iloop + 1;
                            benchid++;
                        }
                        iloop = iloop + rowcount;
                    }
                }
                else if (rbExternal.Checked)
                {
                    for (int i = 0; i < columncount; i++)
                    {
                        for (int j = 0; j < rowcount; j++)
                        {
                            (gvRoom.Rows[j].Cells[i].Controls[0] as Label).Text = "<b>Seat " + benchid + " <span style='color:black'>:</span>&nbsp;" + Alphabets[j] + "" + Convert.ToInt32(i + 1) + "</b>";  // TO DISPLAY 1 BENCHES IN A CELL
                            //(gvRoom.Rows[j].Cells[i].Controls[0] as Label).Width
                            (gvRoom.Rows[j].Cells[i].Controls[0] as Label).ToolTip = iloop.ToString() + (iloop + 1).ToString();
                            hdfbenchid.Value += iloop.ToString() + ',' + (iloop + 1).ToString() + ',';
                            for (int k = 0; k < txtId.Text.Split(',').GetLength(0) && txtId.Text.Split(',')[k] != ""; k++)
                                if (ID[k].Equals(benchid.ToString()))
                                    if ((gvRoom.Rows[j].Cells[i].Controls[0] as Label).BackColor == System.Drawing.Color.LightGray)
                                    {
                                        objCommon.DisplayMessage(this.updplRoom, "Repeated Bench ID!", this.Page);
                                        error++;
                                    }
                                    else
                                        (gvRoom.Rows[j].Cells[i].Controls[0] as Label).BackColor = System.Drawing.Color.LightGray; //HIGHLIGHT THE DISABLE BENCHES
                            iloop = iloop + 1;
                            benchid++;
                        }
                        iloop = iloop + rowcount;
                    }
                }

                gvRoom.Visible = true;
                pnlroomConfig.Visible = true;
                pnlRoom.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                txtId.Enabled = true;

                //TO GET ACTUAL CAPACITY 

                if (!string.IsNullOrEmpty(txtRows.Text) && !string.IsNullOrEmpty(txtColumns.Text))
                {
                    int minus = 0;
                    if (string.IsNullOrEmpty(txtId.Text))
                    {
                        minus = 0;
                    }
                    else
                    {
                        string[] str = txtId.Text.Split(',');

                        for (int i = 0; i < str.GetLength(0); i++)
                        {
                            if (str[i] != "")
                            {
                                if (str[i].Length <= 3)
                                {
                                    if (str[i] != "0" && Convert.ToInt16(str[i].ToString()) <= Convert.ToInt16(txtRows.Text) * Convert.ToInt16(txtColumns.Text))
                                    {
                                        minus++;
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updplRoom, "Bench ID " + str[i].ToString() + "  Not Found!", this.Page);

                                        txtId.Text = i > 0 ? txtId.Text.Replace("," + str[i].ToString(), "") : i == str.GetLength(0) - 1 ? txtId.Text.Replace(str[i].ToString(), "") : txtId.Text.Replace(str[i].ToString() + ",", "");
                                        i = i - 1;
                                        str = txtId.Text.Split(',');
                                        txtActual.Text = (Convert.ToInt16(txtRows.Text) * Convert.ToInt16(txtColumns.Text) - minus).ToString();
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updplRoom, "Bench ID " + str[i] + " does not exist!", this.Page);
                                    txtId.Text = txtId.Text.Replace(str[i], "");
                                    error++;
                                }
                            }
                            else
                            {
                                // 28/08/2012 Testing changes
                                if (txtId.Text.ToString().Contains(",,"))
                                    txtId.Text = txtId.Text.Replace(",,", ",");
                                else
                                    txtId.Text = txtId.Text.Replace(",", "");

                                i = 0;
                                str = txtId.Text.Split(',');
                                txtActual.Text = (Convert.ToInt16(txtRows.Text) * Convert.ToInt16(txtColumns.Text)).ToString();
                            }

                        }
                    }
                    txtActual.Text = (Convert.ToInt16(txtRows.Text) * Convert.ToInt16(txtColumns.Text) - minus).ToString(); // ROW*COL - UnUSED_BENCH
                    if (error > 0)
                        return false;
                    else
                        return true;
                }
                else
                    return false;
            }
            else
            {
                objCommon.DisplayMessage(this.updplRoom, "Actual Capacity exceeds Room Capacity", this.Page);
                btnSave.Visible = false;
                btnCancel.Visible = false;
                return false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.BindGridview() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            return false;
        }
    }

    //Show Room Config Details
    private void ShowDetails(int roomno)
    {
        int Exam_Type = 2;
        //if (rbInternal.Checked)
        //    Exam_Type = 1;
        //else
        //    Exam_Type = 2;
        string SP_Name = "PKG_ACD_GET_ROOM_CONFIG_CRESCENT";
        string SP_Parameters = " @P_ROOM_NO, @P_EXAM_TYPE";
        string Call_Values = "" + roomno + "," + Exam_Type + "";

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        DataTableReader dr = ds.Tables[0].CreateDataReader();
        
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["ROOMNO"] = roomno.ToString();
                txtRows.Text = dr["ROW_INDEX"] == DBNull.Value ? string.Empty : dr["ROW_INDEX"].ToString();
                txtColumns.Text = dr["COLUMN_INDEX"] == DBNull.Value ? string.Empty : dr["COLUMN_INDEX"].ToString();
                txtActual.Text = dr["ACTUALCAPACITY"] == DBNull.Value ? string.Empty : dr["ACTUALCAPACITY"].ToString();
                txtRoomCapacity.Text = dr["ROOMCAPACITY"] == DBNull.Value ? string.Empty : dr["ROOMCAPACITY"].ToString();
                txtId.Text = dr["DISABLED_IDS"] == DBNull.Value ? string.Empty : dr["DISABLED_IDS"].ToString();
                if (txtRoomCapacity.Text != "")
                    GetFactors(Convert.ToInt16(txtRoomCapacity.Text));
                if (txtRows.Text == "" && txtColumns.Text == "")
                {
                    objCommon.DisplayMessage(this.updplRoom, "This Room is not Configured! Please Configure..", this.Page);
                    ViewState["action"] = "add";
                    txtRows.Text = string.Empty;
                    txtColumns.Text = string.Empty;
                    txtId.Text = string.Empty;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    pnlRoom.Visible = false;
                }
                else
                {
                    ViewState["action"] = "edit";
                    BindGridview();
                }

            }
            else
            {
                txtRows.Text = string.Empty;
                txtColumns.Text = string.Empty;
                txtActual.Text = string.Empty;
                txtId.Text = string.Empty;
                objCommon.DisplayMessage(this.updplRoom, "This Room is not Configured! Please Configure..", this.Page);
                btnSave.Visible = false;
                btnCancel.Visible = false;
                pnlRoom.Visible = false;
            }

        }
    }

    private void GetFactors(int roomCap)
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("Rows"));
        dt.Columns.Add(new DataColumn("Col"));
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (i * j == roomCap)
                {
                    dr = dt.NewRow();
                    dr["Rows"] = i;
                    dr["Col"] = j;
                    dt.Rows.Add(dr);
                }
            }
        }
        if (dt != null && dt.Rows.Count > 0)
        {
            lvFactors.DataSource = dt;
            lvFactors.DataBind();
        }
        else
            objCommon.DisplayMessage(this.updplRoom, "No Factors Available!", this.Page);
    }
    #endregion
    protected void gvRoom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rbInternal_CheckedChanged(object sender, EventArgs e)
    {
        //DivInternal.Visible = true;
        //DivExternal.Visible = false;
        if (ddlRoom.SelectedValue != "0")
            ShowDetails(Convert.ToInt32(ddlRoom.SelectedValue));

        lblHead.Text = "Internal";

    }
    protected void rbExternal_CheckedChanged(object sender, EventArgs e)
    {
        //DivInternal.Visible = false;
        //DivExternal.Visible = true;
        if (ddlRoom.SelectedValue != "0")
            ShowDetails(Convert.ToInt32(ddlRoom.SelectedValue));

        lblHead.Text = "External";
    }
}

//Create Gridview Template

public class GridViewTemplate : ITemplate
{

    //A variable to hold the type of ListItemType.

    ListItemType _templateType;
    //A variable to hold the column name.
    string _columnName;
    //Constructor where we define the template type and column name.
    public GridViewTemplate(ListItemType type, string colname)
    {
        _templateType = type;
        _columnName = colname;
    }

    void ITemplate.InstantiateIn(System.Web.UI.Control container)
    {
        switch (_templateType)
        {
            case ListItemType.Header:
                Label lbl = new Label();            //Allocates the new label object.
                lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                break;
            case ListItemType.Item:
                Label lbl2 = new Label();
                lbl2.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                container.Controls.Add(lbl2);
                break;
            case ListItemType.EditItem:
                CheckBox cbchk = new CheckBox();
                break;
            case ListItemType.Footer:
                CheckBox chkColumn = new CheckBox();
                chkColumn.ID = "Chk" + _columnName;
                container.Controls.Add(chkColumn);
                break;
        }
    }

    void tb1_DataBinding(object sender, EventArgs e)
    {
        Label lbldata = (Label)sender;
        GridViewRow container = (GridViewRow)lbldata.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        if (dataValue != DBNull.Value)
        {
            lbldata.ToolTip = dataValue.ToString();

        }
    }
}
