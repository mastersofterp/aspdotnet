using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITMS.UAIMS;
using System.Data;

/// <summary>
/// Summary description for Common
/// </summary>
/// Created By : Yograj Chaple
/// Created On : 27-07-2022
/// *************************************************************************************************
/// Modified On Modified By            Purpose
/// *************************************************************************************************
/// 27-047-2022 Yograj C                Added process for Parameter from ACD_PARAMETER
/// *************************************************************************************************


namespace CommonComponent
{
    public static class GenerateRandomPassword
    {
        public static string GenearteSixLengthPassword()
        {
            string allowedChars = "";
            // allowedChars += "Slit@123";
            //allowedChars += "s,l,i,t";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();

            for (int i = 0; i < 6; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }

        public static string GenearteFourLengthPassword()
        {
            string allowedChars = "";
            // allowedChars += "Slit@123";
            //allowedChars += "s,l,i,t";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }

        //Added by Yograj on 28-01-2023
        public static string GenearteOTP(int otplength)
        {
            string allowedChars = "";
            // allowedChars += "Slit@123";
            //allowedChars += "s,l,i,t";
            allowedChars += "0,1,2,3,4,5,6,7,8,9";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string otpString = "";
            string temp = "";
            Random rand = new Random();

            for (int i = 0; i < otplength; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                otpString += temp;
            }
            return otpString;
        }
    }

    public static class Parameters
    {

        public static string ALLOW_LOGOUT_ON_CHANGEPASSWORD { get { return "ALLOW_LOGOUT_ON_CHANGEPASSWORD"; } }
        public static string ALLOW_ANTIRAGGING_MANDATORY { get { return "ALLOW_ANTIRAGGING_MANDATORY"; } }
        public static string ALLOW_PHOTO_SIGN_MANDATORY_ON_STUD_PROFILE { get { return "ALLOW_PHOTO_SIGN_MANDATORY_ON_STUD_PROFILE"; } }
        public static string ALLOW_LOAD_DISTRICT_ON_STATE_SELECTION_ONLY { get { return "ALLOW_LOAD_DISTRICT_ON_STATE_SELECTION_ONLY"; } }
        public static string ALLOW_LOAD_UPLOAD_DOCUMENT_IN_STUD_INFO_FROM_DOCUMENT_STATUS { get { return "ALLOW_LOAD_UPLOAD_DOCUMENT_IN_STUD_INFO_FROM_DOCUMENT_STATUS"; } }
        public static string ALLOW_STUD_INFO_VALIDATE_10TH_QUALIFICATION { get { return "ALLOW_STUD_INFO_VALIDATE_10TH_QUALIFICATION"; } }
        public static string ALLOW_STUD_INFO_VALIDATED_12TH_QUALIFICATION { get { return "ALLOW_STUD_INFO_VALIDATED_12TH_QUALIFICATION"; } }

        public static string ParameterValue(string ParamName)
        {
            Common objCommon = new Common();
            string paramvalue = string.Empty;
            paramvalue = objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME = '" + ParamName + "' ");
            return paramvalue;
        }
    }

    #region  "WhatsApp Configuration"
    // Added By : Yograj Chaple
    // Added On : 28-01-2023
    // Purpose  : To get WhatsApp Configuration

    public static class WhatsAppConfiguration
    {
        public static string WHATSAPP_CONFIGID { get { return "WHATSAPP_CONFIGID"; } }
        public static string ACCOUNT_SID { get { return "ACCOUNT_SID"; } }
        public static string API_KEY { get { return "API_KEY"; } }
        public static string API_URL { get { return "API_URL"; } }
        public static string FROM_MOBILENO { get { return "FROM_MOBILENO"; } }

        public static DataSet GetWhatsAppConfiguration()
        {
            Common objCommon = new Common();
            DataSet whatsappconfigurationdata = new DataSet();
            whatsappconfigurationdata = objCommon.FillDropDown("ACD_WHATSAPP_CONFIGURATION", "WHATSAPP_CONFIGID", "ACCOUNT_SID, API_KEY, API_URL, FROM_MOBILENO", "", "");
            return whatsappconfigurationdata;
        }
    }
}
    #endregion
