using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public  class Str_Invoice_Item
    {
       public int INVINO { get; set; }
       public int INVTRNO { get; set; }
       public int ITEM_NO { get; set; }
       public double  QTY { get; set; }
       public double  RATE { get; set; }
       public double  AMT { get; set; }
       public double INV_QTY { get; set; }
       public int PORDNO { get; set; }

       public double TAX { get; set; }      
       public double DISCOUNT_AMT { get; set; }      
       public double DISCOUNT_PERCENT { get; set; }      
       public double TAX_PER { get; set; }   
       
       public string MAKE {get; set;}
       public string MANUFACTURING_YEAR {get; set;}
       public DateTime DATE_OF_PURCHASE {get; set;}
       public DateTime GUARANTY_FROM_DATE {get; set;}
       public DateTime GUARANTY_TO_DATE {get; set;}
       public string MAINTAINED_BY {get; set;}
       public string CALIBRATION_FREQUENCY { get; set; }
    }
}
