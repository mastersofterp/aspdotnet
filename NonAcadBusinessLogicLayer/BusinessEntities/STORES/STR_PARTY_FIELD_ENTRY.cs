using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public  class STR_PARTY_FIELD_ENTRY
    {

          private int  _PFNO;
          private string _QUOTNO;
          private int  _PNO;
          private int _FNO;
          private string _FTYPE;
          private string _INFO;
          private decimal  _AMT;
          private char _DEPENDSON;
         private decimal _PERCENTAGE;

       //pROPERY

         private DataTable _VENDOR_TAX_TBL;


         public int IsBlob { get; set; }   
         public string VENDRQUOT_FILE_NAME { get; set; }
         public DataTable VENDRQUOT_UPLOAD_FILE_TBL { get; set; }


         public DataTable VENDOR_TAX_TBL
         {
             get
             {
                 return _VENDOR_TAX_TBL;
             }
             set
             {
                 _VENDOR_TAX_TBL = value;
             }
         }

         public int PFNO
         {
             get
             {
                 return _PFNO;
             }
             set
             {
                 if (_PFNO != value)
                 {
                     _PFNO = value;
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
                 if (_QUOTNO != value)
                 {
                     _QUOTNO = value;
                 }
             }
         }
         public int  PNO
         {
             get
             {
                 return _PFNO ;
             }
             set
             {
                 if (_PFNO != value)
                 {
                     _PFNO = value;
                 }
             }
         }
         public int FNO
         {
             get
             {
                 return _FNO;
             }
             set
             {
                 if (_FNO != value)
                 {
                     _FNO = value;
                 }
             }
         }
         public string FTYPE
         {
             get
             {
                 return _FTYPE;
             }
             set
             {
                 if (_FTYPE != value)
                 {
                     _FTYPE = value;
                 }
             }
         }
         public string INFO
         {
             get
             {
                 return _INFO;
             }
             set
             {
                 if (_INFO != value)
                 {
                     _INFO = value;
                 }
             }
         }
         public decimal AMT
         {
             get
             {
                 return _AMT;
             }
             set
             {
                 if (_AMT != value)
                 {
                     _AMT = value;
                 }
             }
         }
         public char DEPENDSON
         {
             get
             {
                 return _DEPENDSON;
             }
             set
             {
                 if (_DEPENDSON != value)
                 {
                     _DEPENDSON = value;
                 }
             }
         }
         public decimal PERCENTAGE
         {
             get
             {
                 return _PERCENTAGE;
             }
             set
             {
                 if (_PERCENTAGE != value)
                 {
                     _PERCENTAGE = value;
                 }
             }
         }
    }
}
