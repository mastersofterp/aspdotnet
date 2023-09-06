using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_ONLINEFEECOLLECTION_EazyPaySettlment : System.Web.UI.Page
{
    Common objCommon = new Common();
    FeeCollectionController objFees = new FeeCollectionController();
    EazyPaySettmentController ObjEazyPay = new EazyPaySettmentController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // BindSettlementReport();
        }
    }

    private void BindSettlementReport()
    {
        try
        {
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

            DataSet ds = new DataSet();
           // DS= objCommon.FillDropDown('ACD_DCR_ONLINE','ORDER_ID','');
            //ds = objCommon.FillDropDown("ACD_DCR_ONLINE", "ORDER_ID", "", "cast(REC_DT as date) between" + " " + FromDate + " " + "AND" + " " + ToDate + "", "");
            ds = ObjEazyPay.GetOrderId(FromDate, ToDate);
            for (int i=0; i<ds.Tables[0].Rows.Count;i++)
           //for (int i = 0; i < 1; i++)
            {
            //return;
            //Button myButton = (Button)sender;
            String ORDERID = ds.Tables[0].Rows[i]["ORDER_ID"].ToString();

            DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree_DAIICT(Convert.ToInt32(Session["OrgId"]), 1, 1, 0, 0);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                string TranDate=null;
                string  SettlmentDate=null;
                string Ezpaytranid = "";

              string merchantid = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();

                string ezpaytranid = "";
                string amount = "";

                WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify?");


                //byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("ezpaytranid=&amount=&paymentmode=&merchantid=368293&trandate=&pgreferenceno=742571254-440");

                byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + "ezpaytranid=&amount=&paymentmode=&merchantid=" + merchantid + "&trandate=&pgreferenceno=" + ORDERID + "");

                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();
                Console.Write(buffer.Length);
                Console.Write(request.ContentLength);
                WebResponse myResponse = request.GetResponse();
                string result = "";
                StreamReader reader = new StreamReader(myResponse.GetResponseStream());

                result = reader.ReadToEnd();
                //Label1.Text = result;
                Console.WriteLine(result);

                 List<String> listStrLineElements;
                listStrLineElements = result.Split('&').ToList();
                string status = listStrLineElements[0].ToString();
                //string refundstatus = listStrLineElements[27].ToString();
                Ezpaytranid = "850753181-7";
               // Ezpaytranid = listStrLineElements[1].Substring(listStrLineElements[1].IndexOf('=') + 1);
                TranDate = listStrLineElements[3].Substring(listStrLineElements[1].IndexOf('=') + 1);
                SettlmentDate = listStrLineElements[5].Substring(listStrLineElements[2].IndexOf('=') + 1);



                int a = ObjEazyPay.InsertSettelmentDate(Ezpaytranid, Convert.ToDateTime(TranDate), Convert.ToDateTime(SettlmentDate));

               
           }


            }

        }

        catch (Exception exp)
        {
            //Response.Write("Exception " + exp);

        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
         BindSettlementReport(); 
         DataSet ds=new DataSet();
         DateTime FromDate= Convert.ToDateTime(txtFromDate.Text);
         DateTime ToDate= Convert.ToDateTime(txtToDate.Text);

         ds = ObjEazyPay.GetSettemlmentReport(FromDate, ToDate);
         if (ds.Tables[0].Rows.Count > 0)
         {
             pnllvSh.Visible = true;
             lvReport.Visible = true;
             lvReport.DataSource = ds;
             lvReport.DataBind();
             Session["SettlementInfo"]=ds;
         }
         else
         {
             pnllvSh.Visible = false;
             lvReport.Visible = false;
             lvReport.DataSource = null;
             lvReport.DataBind();
         }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  
  
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        ////GridView GV = new GridView();
        ////string ContentType = string.Empty;

        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "SettelmentReport.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //lvReport.GridLines = GridLines.Both;
        //lvReport.HeaderStyle.Font.Bold = true;
        lvReport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

        //if (ds.Tables[0].Rows.Count > 0)
        //{
            //GV.DataSource = Session["SettlementInfo"];
            //GV.DataBind();
            //string attachment = "attachment; filename=StudentAttendance.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/vnd.MS-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //GV.RenderControl(htw);
            //Response.Write(sw.ToString());
        //}
        //else
        //{
        //    objCommon.DisplayMessage(upAttendance, "No Recored Found.", this.Page);
        //    return;
        //}

    }
}