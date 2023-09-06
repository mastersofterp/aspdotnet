using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public  class Str_Purchase_Order
    {

     private int  _PORDNO;
     private string _QUOTNO;
     private string _TENDERNO;
     private string _INDENTNO;
     private int _MDNO;
     private int _PNO;
     private int _PINO;
     private int _ITEM_NO;
     private char  _FLAG;
     private string _FOOTER1;
     private string _FOOTER2;
     private string _TERM1;
     private string _TERM2;
     private DateTime  _SDATE;
     private DateTime _TDATE;
     private string _REFNO;
     private int _BHALNO;
     private char _FLAGINV;
     private string _SUBJECT;
     private char _SDPER;
     private string _ENCL;
     private string _COPYTO;
     private string _HA;
     private string _SIGN;
     private string _REMARK;
     private string _TECHCLAR;
     private double  _CIFCHARGE;
     private string _CIFCHARGETEXT;
     private int _TRNO;
     private char _RELISHED;
     private char _AGREEMENT;
     private char _PENDING;
     private char _DEL;
     private char _RELIES;
     private char _CST;
     private char _ED;
     private char _FINAL;
     private int _AMENDNO;
     private char _ISTYPE;
     private char _BANKGTY;
     private double _AMOUNT;
     private string _BANK_REMARK;

     private double _HANDLING_CHARG;
     private double _TRANSPORT_CHARG;

     private int _INSURED;
       //----start---16-03-2023-----
     private string _NatureOfWork;
     private string _OurReferenceNo;
     //----end---16-03-2023-------
     private DataTable _DPO_ITEM_TBL;
     private DataTable _DPO_TAX_TBL;
     public DataTable DPO_ITEM_TBL
     {
         get
         {
             return _DPO_ITEM_TBL;
         }
         set
         {
             if (_DPO_ITEM_TBL != value)
             {
                 _DPO_ITEM_TBL = value;
             }
         }
     }
     public DataTable DPO_TAX_TBL
     {
         get
         {
             return _DPO_TAX_TBL;
         }
         set
         {
             if (_DPO_TAX_TBL != value)
             {
                 _DPO_TAX_TBL = value;
             }
         }
     }
     public double HANDLING_CHARG
     {
         get
         {
             return _HANDLING_CHARG;
         }
         set
         {
             if (_HANDLING_CHARG != value)
             {
                 _HANDLING_CHARG = value;
             }
         }
     }
     public double TRANSPORT_CHARG
     {
         get
         {
             return _TRANSPORT_CHARG;
         }
         set
         {
             if (_TRANSPORT_CHARG != value)
             {
                 _TRANSPORT_CHARG = value;
             }
         }
     }
     private string _DELIVERY_AT;
     private string _DELIVERY_SCHEDULE;
     private string _MODE_OF_DESPATCH;

     public string DELIVERY_AT
     {
         get
         {
             return _DELIVERY_AT;
         }
         set
         {
             if (_DELIVERY_AT != value)
             {
                 _DELIVERY_AT = value;
             }
         }
     }
     public string DELIVERY_SCHEDULE
     {
         get
         {
             return _DELIVERY_SCHEDULE;
         }
         set
         {
             if (_DELIVERY_SCHEDULE != value)
             {
                 _DELIVERY_SCHEDULE = value;
             }
         }
     }
     public string MODE_OF_DESPATCH
     {
         get
         {
             return _MODE_OF_DESPATCH;
         }
         set
         {
             if (_MODE_OF_DESPATCH != value)
             {
                 _MODE_OF_DESPATCH = value;
             }
         }
     }

     public int INSURED
     {
         get
         {
             return _INSURED;
         }
         set
         {
             if (_INSURED != value)
             {
                 _INSURED = value;
             }
         }


     }
     
     public char BANKGTY
     {
         get
         {
             return _BANKGTY;
         }
         set
         {
             if (_BANKGTY != value)
             {
                 _BANKGTY = value;
             }
         }


     }
     public double AMOUNT
     {
         get
         {
             return _AMOUNT;
         }
         set
         {
             if (_AMOUNT != value)
             {
                 _AMOUNT = value;
             }
         }
     }

     public string BANK_REMARK
     {
         get
         {
             return _BANK_REMARK;
         }
         set
         {
             if (_BANK_REMARK != value)
             {
                 _BANK_REMARK = value;
             }
         }
     }
       //Properties

     public int PORDNO
     {
         get
         {
             return _PORDNO;
         }
         set
         {
             if (_PORDNO != value)
             {
                 _PORDNO = value;
             }
         }
     }
     public string QUOTNO
     {
         get
         {
             return _QUOTNO;
         }
         set
         {
             if (_QUOTNO  != value)
             {
                 _QUOTNO = value;
             }
         }
     }
     public string TENDERNO
     {
         get
         {
             return _TENDERNO;
         }
         set
         {
             if (_TENDERNO != value)
             {
                 _TENDERNO = value;
             }
         }
     }
     public string INDENTNO
     {
         get
         {
             return _INDENTNO;
         }
         set
         {
             if (_INDENTNO != value)
             {
                 _INDENTNO = value;
             }
         }
     }
     public int MDNO
     {
         get
         {
             return _MDNO;
         }
         set
         {
             if (_MDNO != value)
             {
                 _MDNO = value;
             }
         }
     }
     public int PNO
     {
         get
         {
             return _PNO;
         }
         set
         {
             if (_PNO != value)
             {
                 _PNO = value;
             }
         }
     }
     public int PINO
     {
         get
         {
             return _PINO;
         }
         set
         {
             if (_PINO != value)
             {
                 _PINO = value;
             }
         }
     }
     public int ITEM_NO
     {
         get
         {
             return _ITEM_NO;
         }
         set
         {
             if (_ITEM_NO != value)
             {
                 _ITEM_NO = value;
             }
         }
     }
     public char FLAG
     {
         get
         {
             return _FLAG;
         }
            set
         {
             if (_FLAG != value)
             {
                 _FLAG = value;
             }
         }
         

     }
     public string FOOTER1
     {
         get
         {
             return _FOOTER1;
         }
         set
         {
             if (_FOOTER1  != value)
             {
                 _FOOTER1 = value;
             }
         }
     }
       //---------start----16-03-2023

     public string NatureOfWork
     {
         get
         {
             return _NatureOfWork;
         }
         set
         {
             if (_NatureOfWork != value)
             {
                 _NatureOfWork = value;
             }
         }
     }


     public string OurReferenceNo
     {
         get
         {
             return _OurReferenceNo;
         }
         set
         {
             if (_OurReferenceNo != value)
             {
                 _OurReferenceNo = value;
             }
         }
     }

       //----------end-------16-03-2023

     public string FOOTER2
     {
         get
         {
             return _FOOTER2;
         }
         set
         {
             if (_FOOTER2 != value)
             {
                 _FOOTER2 = value;
             }
         }
     }
     public string TERM1
     {
         get
         {
             return _TERM1;
         }
         set
         {
             if (_TERM1 != value)
             {
                 _TERM1 = value;
             }
         }
     }
     public string TERM2
     {
         get
         {
             return _TERM2;
         }
         set
         {
             if (_TERM2 != value)
             {
                 _TERM2 = value;
             }
         }
     }
     public DateTime  SDATE
     {
         get
         {
             return _SDATE;
         }
         set
         {
             if (_SDATE != value)
             {
                 _SDATE = value;
             }
         }
     }
     public DateTime TDATE
     {
         get
         {
             return _TDATE;
         }
         set
         {
             if (_TDATE != value)
             {
                 _TDATE = value;
             }
         }
     }
     public string REFNO
     {
         get
         {
             return _REFNO;
         }
         set
         {
             if (_REFNO != value)
             {
                 _REFNO = value;
             }
         }
     }
     public int BHALNO
     {
         get
         {
             return _BHALNO;
         }
         set
         {
             if (_BHALNO != value)
             {
                 _BHALNO = value;
             }
         }
     }
     public char FLAGINV
     {
         get
         {
             return _FLAGINV;
         }
         set
         {
             if (_FLAGINV != value)
             {
                 _FLAGINV = value;
             }
         }


     }
     public string SUBJECT
     {
         get
         {
             return _SUBJECT;
         }
         set
         {
             if (_SUBJECT != value)
             {
                 _SUBJECT = value;
             }
         }
     }
     public char SDPER
     {
         get
         {
             return _SDPER;
         }
         set
         {
             if (_SDPER != value)
             {
                 _SDPER = value;
             }
         }


     }
     public string ENCL
     {
         get
         {
             return _ENCL;
         }
         set
         {
             if (_ENCL != value)
             {
                 _ENCL = value;
             }
         }
     }
     public string COPYTO
     {
         get
         {
             return _COPYTO;
         }
         set
         {
             if (_COPYTO != value)
             {
                 _COPYTO = value;
             }
         }
     }
     public string HA
     {
         get
         {
             return _HA;
         }
         set
         {
             if (_HA != value)
             {
                 _HA = value;
             }
         }
     }
     public string SIGN
     {
         get
         {
             return _SIGN;
         }
         set
         {
             if (_SIGN != value)
             {
                 _SIGN = value;
             }
         }
     }
     public string REMARK
     {
         get
         {
             return _REMARK;
         }
         set
         {
             if (_REMARK != value)
             {
                 _REMARK = value;
             }
         }
     }
     public string TECHCLAR
     {
         get
         {
             return _TECHCLAR;
         }
         set
         {
             if (_TECHCLAR != value)
             {
                 _TECHCLAR = value;
             }
         }
     }
     public double CIFCHARGE
     {
         get
         {
             return _CIFCHARGE;
         }
         set
         {
             if (_CIFCHARGE != value)
             {
                 _CIFCHARGE = value;
             }
         }
     }
     public string CIFCHARGETEXT
     {
         get
         {
             return _CIFCHARGETEXT;
         }
         set
         {
             if (_CIFCHARGETEXT != value)
             {
                 _CIFCHARGETEXT = value;
             }
         }
     }
     public int TRNO
     {
         get
         {
             return _TRNO;
         }
         set
         {
             if (_TRNO != value)
             {
                 _TRNO = value;
             }
         }
     }
     public char RELISHED
     {
         get
         {
             return _RELISHED;
         }
         set
         {
             if (_RELISHED != value)
             {
                 _RELISHED = value;
             }
         }


     }
     public char AGREEMENT
     {
         get
         {
             return _AGREEMENT;
         }
         set
         {
             if (_AGREEMENT != value)
             {
                 _AGREEMENT = value;
             }
         }


     }
     public char PENDING
     {
         get
         {
             return _PENDING;
         }
         set
         {
             if (_PENDING != value)
             {
                 _PENDING = value;
             }
         }


     }
     public char DEL
     {
         get
         {
             return _DEL;
         }
         set
         {
             if (_DEL != value)
             {
                 _DEL = value;
             }
         }


     }
     public char RELIES
     {
         get
         {
             return _RELIES;
         }
         set
         {
             if (_RELIES != value)
             {
                 _RELIES = value;
             }
         }


     }
     public char CST
     {
         get
         {
             return _CST;
         }
         set
         {
             if (_CST != value)
             {
                 _CST = value;
             }
         }


     }
     public char ED
     {
         get
         {
             return _ED;
         }
         set
         {
             if (_ED != value)
             {
                 _ED = value;
             }
         }


     }
     public char FINAL
     {
         get
         {
             return _FINAL;
         }
         set
         {
             if (_FINAL != value)
             {
                 _FINAL = value;
             }
         }


     }
     public int AMENDNO
     {
         get
         {
             return _AMENDNO;
         }
         set
         {
             if (_AMENDNO != value)
             {
                 _AMENDNO = value;
             }
         }
     }
     public char ISTYPE
     {
         get
         {
             return _ISTYPE;
         }
         set
         {
             if (_ISTYPE != value)
             {
                 _ISTYPE = value;
             }
         }


     }
    }
}
