using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        public class Sports_Invoice_Item
        {
            public int INVINO { get; set; }
            public int INVTRNO { get; set; }
            public int ITEM_NO { get; set; }
            public double QTY { get; set; }
            public double RATE { get; set; }
            public double AMT { get; set; }
            public double TOTQTY { get; set; }
            public string BATCH_NO { get; set; }
            public string EXPIRY_DATE { get; set; }
            public string ITEM_NAME { get; set; }
            public string MFG_DATE { get; set; }
            public int UNIT { get; set; }
        }
    }

}
