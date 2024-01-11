using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public  class STR_PARTY_ITEM_ENTRY
   {
       #region Store PartyItemEntry

 private int   _PINO;
 private int _QTY;
 private int _PNO;
 private string _QUOTNO;
 private int _ITEM_NO;
 private string _ITEMDETAIL;
 private decimal _PRICE;
 private string _UNIT;
 private string _MANUFACTURE;
 private string _MODELNO;
 private string _TECHSPECH;
 private string _FLAG;
 private int _MDNO;
 private DateTime  _EDATE;
 private string _CURRENCY;
 private decimal _TAX_AMT;
 private decimal _TAX;
 private string _QUALITY_QTY_SPEC;
 private string _ITEM_REMARK;
 private int _IsTaxInclusive;

 private string _TECHSPEC;

 public int IsTaxInclusive
 {
     get {return _IsTaxInclusive ;}
     set { _IsTaxInclusive = value; }
 }

 public string TECHSPEC
 {
     get
     {
         return _TECHSPEC;
     }
     set
     {
         _TECHSPEC = value;
     }
 }

 public string QUALITY_QTY_SPEC
 {
     get
     {
         return _QUALITY_QTY_SPEC;
     }
     set
     {
         _QUALITY_QTY_SPEC = value;
     }
 }
 public string ITEM_REMARK
 {
     get
     {
         return _ITEM_REMARK;
     }
     set
     {
         _ITEM_REMARK = value;
     }
 }
 public decimal TAX_AMT
 {
     get {
         return _TAX_AMT;
        }
     set {
         _TAX_AMT = value;
         }
 }

 public decimal TAX
 {
     get
     {
         return _TAX;
     }
     set
     {
         _TAX = value;
     }
 }
 private decimal _TAXABLE_AMT;

 public decimal TAXABLE_AMT
 {
     get
     {
         return _TAXABLE_AMT;
     }
     set
     {
         _TAXABLE_AMT = value;
     }
 }
 private decimal _TOTAMOUNT;

 public decimal TOTAMOUNT
 {
     get { 
         return _TOTAMOUNT; 
     }
     set { 
         _TOTAMOUNT = value; 
         }
 } 

    
       //ProperTy

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
 public int QTY
 {
     get
     {
         return _QTY ;
     }
     set
     {
         if (_QTY != value)
         {
             _QTY = value;
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
 public string  QUOTNO
 {
     get
     {
         return _QUOTNO ;
     }
     set
     {
         if (_QUOTNO != value)
         {
             _QUOTNO = value;
         }
     }
 }
 public int  ITEM_NO
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
 public string ITEMDETAIL
 {
     get
     {
         return _ITEMDETAIL;
     }
     set
     {
         if (_ITEMDETAIL != value)
         {
             _ITEMDETAIL = value;
         }
     }
 }
 public decimal  PRICE
 {
     get
     {
         return _PRICE ;
     }
     set
     {
         if (_PRICE != value)
         {
             _PRICE = value;
         }
     }
 }
 public string  UNIT
 {
     get
     {
         return _UNIT ;
     }
     set
     {
         if (_UNIT != value)
         {
             _UNIT = value;
         }
     }
 }
 public string MANUFACTURE
 {
     get
     {
         return _MANUFACTURE ;
     }
     set
     {
         if (_MANUFACTURE != value)
         {
             _MANUFACTURE = value;
         }
     }
 }
 public string MODELNO
 {
     get
     {
         return _MODELNO ;
     }
     set
     {
         if (_MODELNO != value)
         {
             _MODELNO = value;
         }
     }
 }
 public string TECHSPECH
 {
     get
     {
         return _TECHSPECH ;
     }
     set
     {
         if (_TECHSPECH != value)
         {
             _TECHSPECH = value;
         }
     }
 }
 public string FLAG
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
 public DateTime  EDATE
 {
     get
     {
         return _EDATE;
     }
     set
     {
         if (_EDATE != value)
         {
             _EDATE = value;
         }
     }
 }
 public string CURRENCY
 {
     get
     {
         return _CURRENCY ;
     }
     set
     {
         if (_CURRENCY != value)
         {
             _CURRENCY = value;
         }
     }
 }

 private decimal _TAXAMOUNT;

 public decimal TAXAMOUNT
 {
     get { return _TAXAMOUNT; }
     set { _TAXAMOUNT = value; }
 }
 private decimal _DISCOUNT;

 public decimal DISCOUNT
 {
     get { return _DISCOUNT; }
     set { _DISCOUNT = value; }
 }
 private decimal _DISCOUNTAMOUNT;

 public decimal DISCOUNTAMOUNT
 {
     get { return _DISCOUNTAMOUNT; }
     set { _DISCOUNTAMOUNT = value; }
 }

       #endregion



   }
}
