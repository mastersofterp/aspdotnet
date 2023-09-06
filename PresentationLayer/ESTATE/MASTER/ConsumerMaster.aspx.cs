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


public partial class ESTATE_Master_ConsumerMaster : System.Web.UI.Page
{

    Common objcommon = new Common();
    ConsumerMasterEntity objMasEnt = new ConsumerMasterEntity();
    ConsumerMasterController objMasCon = new ConsumerMasterController();

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
                ViewState["action"] = "add";
                //BindMaterialMaster();        
                binddropdownlist();
                rdoActive.Checked = true;
            }
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

    //bind the  dropdownlist 
    protected void binddropdownlist()
    {
        try
        {
            objcommon.FillDropDownList(dllconsumertype, "EST_CONSUMERTYPE_MASTER", "IDNO", "CONSUMERTYPE_NAME", "IDNO>0", "IDNO");
            objcommon.FillDropDownList(ddldepartment,   "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            objcommon.FillDropDownList(ddldessignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIGNO");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //submit button changes
    protected void btnsumit_Click(object sender, EventArgs e)
    {
        try
        {
             objMasEnt.Consumertype  = Convert.ToInt16(dllconsumertype.SelectedItem.Value);
             objMasEnt.Title  = ddltitle.SelectedValue == "0" ? "" : ddltitle.SelectedItem.Text;
             objMasEnt.Firstname  = txtfirstname.Text.Trim();
             objMasEnt.Middlename = txtmiddlename.Text.Trim();
             objMasEnt.Lastname   = txtlastname.Text.Trim();
           
             if(rdoMale.Checked.Equals(true))
             {
                 objMasEnt.Gender  ='M';
             }
             else 
             {
                 objMasEnt.Gender ='F';
             }
             if(rdoMarried.Checked.Equals(true ))
             {
               objMasEnt.Martial  ='M';
             }
             else 
             {
                 objMasEnt.Martial = 'U';
             }

             //if (Convert.ToDateTime(txtdateofbirth.Text) < System.DateTime.Now)
             //{
             //    objMasEnt.Dateofbirth = DateTime.Parse(txtdateofbirth.Text);
             //}
             //else
             //{
             //    objcommon.DisplayMessage(this.updpnlge, "Date of Birth can not be greater than Todays date.", this.Page);
             //    txtdateofbirth.Text = "";
             //}


             if (txtdateofbirth.Text != string.Empty)
             {
                 if (Convert.ToDateTime(txtdateofbirth.Text) < System.DateTime.Now)
                 {
                     objMasEnt.Dateofbirth = Convert.ToDateTime(txtdateofbirth.Text);
                 }
                 else
                 {
                     objcommon.DisplayMessage(this.updpnlge, "Date of Birth can not be greater than Todays date.", this.Page);
                     txtdateofbirth.Text = "";
                 }
             }
             else
             {
                 objMasEnt.Dateofbirth = DateTime.MinValue;
             }


             if (txtdatejoing.Text != string.Empty)
             {
                 if (Convert.ToDateTime(txtdateofbirth.Text) < Convert.ToDateTime(txtdatejoing.Text))
                 {
                     objMasEnt.Dateofjoining = Convert.ToDateTime(txtdatejoing.Text);
                 }
                 else
                 {
                     objcommon.DisplayMessage(this.updpnlge, "Date of Joining can not be greater than Birth date.", this.Page);
                     txtdateofbirth.Text = "";
                     return;
                 }
             }
             else
             {
                 objMasEnt.Dateofjoining = DateTime.MinValue;
             }





             objMasEnt.Loccaladdress     =    txtlocaladdress.Text.Trim();
             objMasEnt.PermanentAddress  =    txtpermanentaddress.Text.Trim();
             objMasEnt.Phonenumber       =    txtphonenumber.Text.Trim();
             objMasEnt.PANnumber         =    txtpannumber.Text.Trim();
             objMasEnt.Emailaddress      =    txtempemail.Text.Trim();
             objMasEnt.Department        =    Convert.ToInt16(ddldepartment.SelectedItem.Value);
             objMasEnt.Dessignation      =    Convert.ToInt16(ddldessignation.SelectedItem.Value);
             objMasEnt.COLLEGECODE       =    Session["colcode"].ToString();
             objMasEnt.Consumerfullname  =    txtfirstname.Text + " " + txtmiddlename.Text + " " + txtlastname.Text;
             objMasEnt.Title = ddltitle.SelectedItem.Text;

             if (rdoActive.Checked.Equals(true))
             {
                 objMasEnt.Checkstatus = 'A';
             }
             else
             {
                 objMasEnt.Checkstatus = 'D';
             }
             
            if (ViewState["action"].ToString().Equals("add"))
            {                
                 objMasEnt.ConsumerIDNO  =0;
                 CustomStatus cs = (CustomStatus)objMasCon.AddconsumerEntry(objMasEnt);
                 if (cs.Equals(CustomStatus.RecordSaved))
                 {
                     objcommon.DisplayMessage(this.updpnlge, "Record Save Successfully.", this.Page);
                     ClearSelection();
                 }
                 else
                 {
                     objcommon.DisplayMessage(this.updpnlge, "Sorry!Try Again.", this.Page);
                 }
            }
            if(ViewState["action"].ToString().Equals("edit"))
            {
                objMasEnt.ConsumerIDNO = Convert.ToInt16(ViewState["IDNO"]);
                CustomStatus cs = (CustomStatus)objMasCon.AddconsumerEntry(objMasEnt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {  
                    objcommon.DisplayMessage(this.updpnlge, "Record Update Successfully.", this.Page);
                    ClearSelection();
                }
                else
                {
                    objcommon.DisplayMessage(this.updpnlge, "Sorry!Try Again.", this.Page);
                }
                 //CustomStatus cs =
            }   
        }
        catch (Exception ex)
        {
            Console.WriteLine (ex.ToString());
        }
    }
    protected void imgserch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {            
          int idno = 0;
          // idno = Convert.ToInt32(objcommon.GetIDNo(txtSearch));
          if (!string.IsNullOrEmpty(hfInvNo.Value))
          {
            idno = Convert.ToInt32(hfInvNo.Value);         
            ViewState["IDNO"] = idno; 
                  DataTableReader dr = objMasCon.GetemployeeInformation(idno);
                  if (dr != null)
                  {
                      ViewState["action"] = "edit";
                      if (dr.Read())
                      {
                          dllconsumertype.SelectedValue = dr["CONSUMERTYPE"].Equals(DBNull.Value) ? "0" : dr["CONSUMERTYPE"].ToString();
                          ddltitle.SelectedItem.Text = dr["TITLE"] == null ? string.Empty : dr["TITLE"].ToString();
                          txtfirstname.Text = dr["FNAME"] == null ? string.Empty : dr["FNAME"].ToString();
                          txtmiddlename.Text = dr["MNAME"] == null ? string.Empty : dr["MNAME"].ToString();
                          txtlastname.Text = dr["LNAME"] == null ? string.Empty : dr["LNAME"].ToString();

                          if (dr["SEX"].ToString().Trim() == "F")
                          {
                              rdoMale.Checked = false;
                              rdoFemale.Checked = true;
                          }
                          else if (dr["SEX"].ToString().Trim() == "M")
                          {
                              rdoFemale.Checked = false;
                              rdoMale.Checked = true;
                          }
                          if (dr["MartialStatus"].ToString().Trim() == "U")
                          {
                              rdoSingle.Checked = true;
                              rdoMarried.Checked = false;
                          }
                          else
                          {
                              rdoMarried.Checked = true;
                              rdoSingle.Checked = false;
                          }

                          if (dr["ChkStatus"].ToString().Trim() == "A")
                          {
                              rdoActive.Checked = true;
                              rdoDeactive.Checked = false;
                          }
                          else
                          {
                              rdoActive.Checked = false;
                              rdoDeactive.Checked = true;
                          }

                          txtdateofbirth.Text = dr["DOB"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["DOB"]).ToString("dd/MM/yyyy");
                          txtdatejoing.Text = dr["DOJ"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["DOJ"]).ToString("dd/MM/yyyy");

                          txtlocaladdress.Text = dr["RESADD1"] == null ? string.Empty : dr["RESADD1"].ToString();
                          txtpermanentaddress.Text = dr["TOWNADD1"] == null ? string.Empty : dr["TOWNADD1"].ToString();

                          txtempemail.Text = dr["EMAILID"] == null ? string.Empty : dr["EMAILID"].ToString();
                          txtphonenumber.Text = dr["PHONENO"] == null ? string.Empty : dr["PHONENO"].ToString();
                          txtpannumber.Text = dr["PAN_NO"] == null ? string.Empty : dr["PAN_NO"].ToString();

                          ddldepartment.SelectedValue = dr["SUBDEPTNO"].Equals(DBNull.Value) ? "0" : dr["SUBDEPTNO"].ToString();
                          ddldessignation.SelectedValue = dr["SUBDESIGNO"].Equals(DBNull.Value) ? "0" : dr["SUBDESIGNO"].ToString();
                          if (!string.IsNullOrEmpty(dr["EMP_ID"].ToString()))
                          {
                              btnsumit.Visible = false;
                              btnreset.Visible = false;
                              btnImport.Visible = false;
                          }
                          else
                          {
                              btnsumit.Visible = true;
                              btnreset.Visible = true;
                             // btnImport.Visible = true;
                          }
                      }
                  }
              }
              else
              {
                //  objcommon.DisplayMessage(this.updpnlge, "Please select Consumer.", this.Page);

              }
          }
          catch(Exception exe)
          {
              Console.WriteLine(exe.ToString());
          }

    }

    protected void ClearSelection()
    {
        ddltitle.SelectedItem.Text = "Please Select";
        ViewState["action"] = "add";
        ddldessignation.ClearSelection();
        ddldepartment.ClearSelection();
        dllconsumertype.ClearSelection();
        txtSearch.Text = string.Empty;
        txtdatejoing.Text = string.Empty;
        txtdateofbirth.Text = string.Empty;
        txtempemail.Text = string.Empty;
        txtfirstname.Text = string.Empty;
        txtlastname.Text = string.Empty;
        txtlocaladdress.Text = string.Empty;
        txtmiddlename.Text = string.Empty;
        txtpannumber.Text = string.Empty;
        txtpermanentaddress.Text = string.Empty;
        txtphonenumber.Text = string.Empty;
        ViewState["IDNO"] = 0;
        rdoFemale.Checked = false;
        rdoMale.Checked = true;
        rdoSingle.Checked = true;
        rdoMarried.Checked = false;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        ClearSelection();

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
       int status =objMasCon.ImportDataFromPayRoll();
        if (status>0)
        {
            objcommon.DisplayMessage(this.updpnlge, "Data Import Successfully.", this.Page);
        }
        else
        {
            objcommon.DisplayMessage(this.updpnlge, "Data Import Fail.", this.Page);
        }

    }
    protected void imgbtnclearname_Click(object sender, ImageClickEventArgs e)
    {
        ClearSelection();
        btnsumit.Visible = true;
        btnreset.Visible = true;
        btnImport.Visible =true;
    }
}
