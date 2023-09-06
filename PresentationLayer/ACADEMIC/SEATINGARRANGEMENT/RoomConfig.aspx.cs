//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SEATING PLAN                                                             
// PAGE NAME     : ROOM CONFIGURATION                                  
// CREATION DATE : 12-MAR-2012                                                      
// CREATED BY    : ABHIJIT DESHPANDE                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
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

public partial class ACADEMIC_EXAMINATION_RoomConfig : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
   //RoomConfigController objRC = new RoomConfigController();
     SeatingArrangementController objRC = new SeatingArrangementController();
     RoomConfig objRoom = new RoomConfig();
   // SeatingArrangementController objExamController = new SeatingArrangementController();
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
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    
                }
                PopulateDropDownList();
                ViewState["action"] = null;
                lblMsg.Text = "";
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
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
        if (ddlCollege.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updplRoom, "Please Select College/School", this.Page);
        }
        else if(ddlBlockNo.SelectedIndex==0)
        {

            objCommon.DisplayMessage(this.updplRoom, "Please Select Block.", this.Page);
        }
        else if (ddlRoom.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updplRoom, "Please Select Room", this.Page);
        }
        else if (txtRows.Text == "")
        {

            objCommon.DisplayMessage(this.updplRoom, "Please Enter Row", this.Page);
        }
        else if (txtColumns.Text == "")
        {
            objCommon.DisplayMessage(this.updplRoom, "Please Enter Column.", this.Page);
        }
        else
        {
            BindGridview();

        }
        
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
                //objRoom.Actual_Capacity = ViewState["action"].ToString() == "add" ? Convert.ToInt32(txtActual.Text) - minus : ViewState["action"].ToString() == "edit" ? Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim()) - minus : Convert.ToInt32(txtRows.Text.Trim()) * Convert.ToInt32(txtColumns.Text.Trim());
                //objRoom.Actual_Capacity = Convert.ToInt32(txtRoomCapacity.Text);
                objRoom.Actual_Capacity = Convert.ToInt32(txtActual.Text);
               
                objRoom.CollegeCode = Session["colcode"].ToString();

                if (ViewState["action"] != null)
                {
                    //Add/Update Room Config/Building Room
                    CustomStatus cs = (CustomStatus)objRC.AddRoomConfig(objRoom,Convert.ToInt32(ddlCollege.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updplRoom, "Block Configured Successfully!..", this.Page);
                        Clearall();
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updplRoom, "Block Updated Successfully!..", this.Page);
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        //Clearall();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updplRoom, "Error!..", this.Page);
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }

                  //  btnSave.Visible = false;
                   // btnCancel.Visible = false;
                    //txtId.Enabled = false;
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
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME AS COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_NAME ASC");
          
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
            SeatingArrangementController objRC = new SeatingArrangementController();
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
        ddlCollege.SelectedIndex = 0;
        ddlBlockNo.SelectedIndex = 0;
        txtRows.Text = string.Empty;
        txtColumns.Text = string.Empty;
        txtActual.Text = string.Empty;
        gvRoom.DataSource = null;
        gvRoom.DataBind();
        lvFactors.DataSource = null;
        lvFactors.DataBind();
        txtId.Text = string.Empty;
        txtRoomCapacity.Text = string.Empty;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }

    //The Gridview where Bind by using Row and Columns   
    protected bool BindGridview( )
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

                    //column[yIndex] = yIndex.ToString();

                    dt.Columns.Add(column);
                }


                for (int nIndex = 0; nIndex < Convert.ToInt32(txtRows.Text); nIndex++)
                {
                    row = dt.NewRow();
                    //row[nIndex] = nIndex.ToString();
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

                int iloop = 1;
                int columncount = Convert.ToInt32(txtColumns.Text);
                int rowcount = Convert.ToInt32(txtRows.Text);
                int benchid = 1;
                int error = 0;
                string[] ID = txtId.Text.Split(',');
                for (int i = 0; i < columncount; i++)
                {
                    for (int j = 0; j < rowcount; j++)
                    {
                      
                        //(gvRoom.Rows[j].Cells[i].Controls[0] as Label).Text = "<b>BENCH " + benchid + " :</b>&nbsp;&nbsp;&nbsp;&nbsp;Seat " + benchid + "";  // TO DISPLAY 1 BENCHES IN A CELL commented by akhilesh on [2019-03-28]
                        (gvRoom.Rows[j].Cells[i].Controls[0] as Label).Text = "Seat " + benchid + "";  // TO DISPLAY 1 BENCHES IN A CELL
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
                                    (gvRoom.Rows[j].Cells[i].Controls[0] as Label).BackColor = System.Drawing.Color.Yellow; //HIGHLIGHT THE DISABLE BENCHES
                        iloop = iloop + 1;
                        benchid++;
                    }
                    iloop = iloop + rowcount;
                }

                gvRoom.Visible = true;
                pnlroomConfig.Visible = true;
                pnlRoom.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = false;
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
                                    objCommon.DisplayMessage(this.updplRoom, "Bench ID " + str[i]  + " does not exist!", this.Page);
                                    txtId.Text = txtId.Text.Replace(str[i],"");
                                    error++;
                                }
                            }
                            else
                            {
                                //objCommon.DisplayMessage(this.updplRoom, "Bench ID not entered in correct format!", this.Page);
                                // txtId.Focus();
                                // 28/08/2012 Testing changes
                                if (txtId.Text.ToString().Contains(",,"))
                                txtId.Text = txtId.Text.Replace(",,", ",");
                                else
                                txtId.Text = txtId.Text.Replace(",", "");

                                i = 0;
                                str = txtId.Text.Split(',');
                                txtActual.Text = (Convert.ToInt16(txtRows.Text) * Convert.ToInt16(txtColumns.Text)).ToString();
                                // return false;
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
        SqlDataReader dr = objRC.GetRoomConfiguration(roomno);
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["ROOMNO"] = roomno.ToString();
                //ddlRoom.SelectedValue = dr["ROOM_NAME"] == null ? string.Empty : dr["ROOM_NAME"].ToString();
                txtRows.Text = dr["ROW_INDEX"] == DBNull.Value ? string.Empty : dr["ROW_INDEX"].ToString();
                txtColumns.Text = dr["COLUMN_INDEX"] == DBNull.Value ? string.Empty : dr["COLUMN_INDEX"].ToString();
                txtActual.Text = dr["ACTUALCAPACITY"] == DBNull.Value ? string.Empty : dr["ACTUALCAPACITY"].ToString();
                txtRoomCapacity.Text = dr["ROOMCAPACITY"] == DBNull.Value ? string.Empty : dr["ROOMCAPACITY"].ToString();
                txtId.Text = dr["DISABLED_IDS"] == DBNull.Value ? string.Empty : dr["DISABLED_IDS"].ToString();
                if(txtRoomCapacity.Text!= "")
                    GetFactors(Convert.ToInt16(txtRoomCapacity.Text));
                if (txtRows.Text == "" && txtColumns.Text == "")
                {
                    objCommon.DisplayMessage(this.updplRoom, "This Block is not Configured! Please Configure..", this.Page);
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
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10 ; j++)
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

    #region Not in used
    protected void gvRoom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region DropdownList
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
       // int collegecode = Convert.ToInt16(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
            //objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0 AND  COLLEGE_ID=" + ddlCollege.SelectedValue + "", "ROOMName");
       // objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
   
        //objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0 AND  COLLEGE_ID=" + ddlCollege.SelectedValue + "", "SEQUENCENO");
        //objCommon.FillDropDownList(ddlRoom, "ACD_ROOM R INNER JOIN ACD_DEPARTMENT D ON R.DEPTNO=D.DEPTNO", "R.ROOMNO","CONCAT(ROOMNAME,'-',DEPTCODE)", "R.ROOMNO > 0 AND  R.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "SEQUENCENO");
        objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND isnull(ACTIVESTATUS,0)=1 AND COLLEGE_CODE=" + Session["colcode"].ToString(), "BLOCKNAME ASC"); 
        //objCommon.FillDropDownList(ddlBlockNo, "ACD_FLOOR", "FLOORNO", "FLOORNAME", "FLOORNO > 0 AND COLLEGE_CODE=" + collegecode, "FLOORNO DESC"); 
    }
    protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlRoom, "ACD_ROOM R INNER JOIN ACD_BLOCK B ON B.BLOCKNO=R.BLOCKNO INNER JOIN ACD_DEPARTMENT D ON R.DEPTNO=D.DEPTNO", "R.ROOMNO", "CONCAT(ROOMNAME,'-',DEPTCODE) ROOMNAME", "R.ROOMNO > 0 AND isnull(R.ACTIVESTATUS,0)=1 AND R.BLOCKNO=" + ddlBlockNo.SelectedValue + "", "ROOMNAME ASC");
        //objCommon.FillDropDownList(ddlRoom, "ACD_ROOM R INNER JOIN ACD_FLOOR B ON B.FLOORNO=R.FLOORNO INNER JOIN ACD_DEPARTMENT D ON R.DEPTNO=D.DEPTNO", "R.ROOMNO", "CONCAT(ROOMNAME,'-',DEPTCODE)", "R.ROOMNO > 0 AND  R.FLOORNO=" + ddlBlockNo.SelectedValue + "", "ROOMNO");
   
    }
    protected void ddlBlockNo_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
    #endregion

#region
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

                //Creates a new label control and add it to the container.

                Label lbl = new Label();            //Allocates the new label object.


                lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                //lbl1.Text = _columnName;
                container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                //container.Controls.Add(lbl1);
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

#endregion

}