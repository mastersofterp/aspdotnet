using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IOBPAYClient
            {
            }

            public class TokenReq
            {
                public String merchantid { get; set; }
                public String merchantsubid { get; set; }
                public String action { get; set; }
                public String feetype { get; set; }
                public String totalamt { get; set; }
                public String replyurl { get; set; }
            }

            public class TxnInitReq
            {
                public String merchantid { get; set; }
                public String merchantsubid { get; set; }
                public String action { get; set; }
                public String feetype { get; set; }
                public String totalamt { get; set; }
                public String tokenid { get; set; }
                public String merchanttxnid { get; set; }
                public String udf1 { get; set; }
                public String udf2 { get; set; }
                public String udf3 { get; set; }
            }

            public class DataExchange
            {
                public String merchantid { get; set; }
                public String merchantsubid { get; set; }
                public String action { get; set; }
                public String data { get; set; }
                public String hmac { get; set; }

            }

            public class TokenRes
            {
                public String merchantid { get; set; }
                public String merchantsubid { get; set; }
                public String action { get; set; }
                public String data { get; set; }
                public String hmac { get; set; }
                public String requestid { get; set; }
                public String errorcd { get; set; }
                public String errormsg { get; set; }

            }

            public class TokenResDecrypted
            {
                public String merchantid { get; set; }
                public String merchantsubid { get; set; }
                public String action { get; set; }
                public String tokenid { get; set; }
                public String validitydttime { get; set; }
                public String feetype { get; set; }
                public String totalamt { get; set; }
                public String replyurl { get; set; }

                public String tokenstatus { get; set; }

            }


            public class ResponseData
            {
                public string Data { get; set; }
                public string MerchantId { get; set; }
                public string RequestId { get; set; }
                public string Hmac { get; set; }
                public string Action { get; set; }
                public string MerchantSubId { get; set; }
            }

            public class TransactionData
            {
                public string txndt { get; set; }
                public string totalamt { get; set; }
                public string tokenid { get; set; }
                public string merchantid { get; set; }
                public string statustimestamp { get; set; }
                public string trackid { get; set; }
                public string txnstatus { get; set; }
                public string action { get; set; }
                public string feetype { get; set; }
                public string merchanttxnid { get; set; }
                public string merchantsubid { get; set; }
                public string timestamp { get; set; }
                //new added
                public string udf1 { get; set; }
                public string udf2 { get; set; }
                public string udf3 { get; set; }
            }
        
        }
    }
   
}
