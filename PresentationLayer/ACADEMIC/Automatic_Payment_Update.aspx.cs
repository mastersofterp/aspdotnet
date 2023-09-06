using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;

public partial class ACADEMIC_Automatic_Payment_Update : System.Web.UI.Page
{
    SqlConnection SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    FeeCollectionController objFees = new FeeCollectionController();
    Common objCommon = new Common();
    int SuccessCounter = 0;
    int FailedCounter = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblSuccessStatus.Text = "Success Transaction :-" + SuccessCounter.ToString();
        lblFailedStatus.Text = "Failed Transaction :-" + FailedCounter.ToString();
    }

    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    public int SubmitData(string Order_ID, int IDNO, string IsValid, string Receipt_Code, string TransactionDate, string TransactionID, string PayStatus, string Message,string TranDateTime)
    {
        int retStatus1 = 0;
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSqlhelper = new SQLHelper(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            SqlParameter[] sqlparam = null;
            {
                sqlparam = new SqlParameter[9];
                sqlparam[0] = new SqlParameter("@P_ORDER_ID", Order_ID);
                sqlparam[1] = new SqlParameter("@P_IDNO", IDNO);
                sqlparam[2] = new SqlParameter("@P_ISVALID", IsValid);
                sqlparam[3] = new SqlParameter("@P_RECEIPT_CODE", Receipt_Code);
                sqlparam[4] = new SqlParameter("@P_TRANSACTION_DATE", TransactionDate);
                sqlparam[5] = new SqlParameter("@P_TRANSID", TransactionID);
                sqlparam[6] = new SqlParameter("@P_PAY_STATUS", PayStatus);
                sqlparam[7] = new SqlParameter("@P_MESSAGE", Message);                 
                sqlparam[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                sqlparam[8].Direction = ParameterDirection.Output;
                string idcat = sqlparam[8].Direction.ToString();
            };
            object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_AUTO_UPDATED_TRANSACTION", sqlparam, true);
            return retStatus1 = Convert.ToInt32(studid);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SubmitData() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    public int UpdateFailData(string Order_ID, int IDNO, string Message)
    {
        int retStatus1 = 0;
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSqlhelper = new SQLHelper(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            SqlParameter[] sqlparam = null;
            {
                sqlparam = new SqlParameter[4];
                sqlparam[0] = new SqlParameter("@P_ORDER_ID", Order_ID);
                sqlparam[1] = new SqlParameter("@P_IDNO", IDNO);
                sqlparam[2] = new SqlParameter("@P_MESSAGE", Message);
                sqlparam[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                sqlparam[3].Direction = ParameterDirection.Output;
                
            };
            object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_FAIL_AUTO_TRANSACTION", sqlparam, true);
            return retStatus1 = Convert.ToInt32(studid);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateFailData() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    public DataSet GetAllDCRTempTrasaction()
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSqlhelper = new SQLHelper(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            SqlParameter[] sqlParams = new SqlParameter[0];

            ds = objSqlhelper.ExecuteDataSetSP("PKG_GET_ACD_DCR_TEMP_RECORD_FOR_AUTOSCHEDULAR", sqlParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetAllDCRTempTrasaction() --> " + ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    public DataSet GetAllTransationLog()
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSqlhelper = new SQLHelper(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            SqlParameter[] sqlParams = new SqlParameter[0];

            ds = objSqlhelper.ExecuteDataSetSP("PKG_ACAD_GET_ONLINE_FEE_TRANSATION_LOG_REPORT", sqlParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetAllTransationLog() --> " + ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet dsAllTransactions = new DataSet();

      
        //SqlDataAdapter sda = new SqlDataAdapter("SELECT ORDER_ID,IDNO,MESSAGE,RECON,SUBSTRING([MESSAGE],0,CHARINDEX('|',[MESSAGE])) MID,(SELECT TOP 1 CHECKSUM_KEY FROM ACD_PG_CONFIGURATION WHERE MERCHANT_ID = SUBSTRING([MESSAGE],0,CHARINDEX('|',[MESSAGE]))) CHECKSUM_KEY FROM ACD_DCR_TEMP WHERE ORDER_ID IS NOT NULL AND ORDER_ID NOT IN (SELECT ORDER_ID FROM ACD_DCR WHERE ORDER_ID IS NOT NULL) AND MESSAGE IS NOT NULL AND (SUBSTRING([MESSAGE],0,CHARINDEX('|',[MESSAGE])) IS NOT NULL AND SUBSTRING([MESSAGE],0,CHARINDEX('|',[MESSAGE])) != '') AND (SELECT TOP 1 CHECKSUM_KEY FROM ACD_PG_CONFIGURATION WHERE MERCHANT_ID = SUBSTRING([MESSAGE],0,CHARINDEX('|',[MESSAGE]))) IS NOT NULL", SqlConnection);

        //sda.Fill(dsAllTransactions);


        dsAllTransactions = GetAllDCRTempTrasaction();

       

        //New Code on 2022 Sep 03
        if (dsAllTransactions != null && dsAllTransactions.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsAllTransactions.Tables[0].Rows.Count; i++)
            {
                string TranMesasge = "";
                string order_id = dsAllTransactions.Tables[0].Rows[i]["ORDER_ID"].ToString();
                int IDNO = Convert.ToInt32(dsAllTransactions.Tables[0].Rows[i]["IDNO"].ToString());
                string TranDate = "";
                string TranDateTime = "";
                string TranID = "";
                //string order_id = "3408522872008";
                String dataNew = "";
                //New Added
                string MID = dsAllTransactions.Tables[0].Rows[i]["MID"].ToString();
                string Secretkey = dsAllTransactions.Tables[0].Rows[i]["CHECKSUM_KEY"].ToString();
                //New Added End

                //Original Code
                //String data = Convert.ToString("0122|BSABREHFEE|" + order_id + "|" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                //String commonkey = Convert.ToString("7QOrIqImgcxkoD8J2RuCbMSI8DM6srl6");
                //Original Code END

                String data = Convert.ToString("0122|" + MID + "|" + order_id + "|" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                String commonkey = Secretkey.ToString();


                String hash = String.Empty;
                hash = GetHMACSHA256(data, commonkey);
                data = data + "|" + hash.ToUpper();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 1000;
                WebRequest request = WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController");
                byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + data);
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
                //lblStatus.Text = result.ToString();

                dataNew = result;
                TranMesasge = result.ToString();

                List<String> listStrLineElements;
                listStrLineElements = dataNew.Split('|').ToList();
                string status = listStrLineElements[15].ToString();
                TranDateTime = listStrLineElements[14].ToString();
                string refundstatus = listStrLineElements[27].ToString();
                TranDate = TranDateTime.Split(' ')[0];
                TranID = listStrLineElements[3].ToString();
                if (status == "0300" && refundstatus == "NA")
                {
                    SuccessCounter++;
                    int resultOut = SubmitData(order_id, IDNO, "1", "", TranDate, TranID, "1", TranMesasge, TranDateTime);
                }
                else
                {
                    FailedCounter++;
                    int resultOut = UpdateFailData(order_id, IDNO, TranMesasge);
                }
            }
            lblSuccessStatus.Text = "Success Transaction :-" + SuccessCounter.ToString();
            lblFailedStatus.Text = "Failed Transaction :-" + FailedCounter.ToString();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Record for Auto Schedular", this.Page);
        }


    }
    protected void btnStatusReport_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = GetAllTransationLog();
           // ds.Tables[0].TableName = "Publish Date Report";

            if (ds.Tables[0].Rows.Count < 1)
            {
                ds.Tables[0].Rows.Add("No Record Found");
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OnlineFeesTransactionLogReport.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch
        {

        }
    }
}