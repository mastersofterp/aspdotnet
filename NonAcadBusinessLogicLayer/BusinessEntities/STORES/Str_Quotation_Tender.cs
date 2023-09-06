using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Str_Quotation_Tender
    {

        #region STORE_QUOTATIONENTRY

         private int  _QNO;
         private string  _QUOTNO;
         private string  _REFNO;
         private int  _MDNO;
         private string   _INDNO;
         private DateTime   _ODATE;
       private DateTime   _OTIME;
       private DateTime   _LDATE;
       private DateTime   _LTIME;
       private DateTime   _SDATE;
       private int   _BHALNO;
        private string  _MATTER;
       private string  _SUBJECT;
       private string  _TERMS;
       private string  _TOPSPECI;
       private double  _QUOTAMT;
       private int  _RTVALID;
       private string  _FLAG;
       private string _RTVALIDUNIT;
       //private string _AUTHORITY;
       private char _ISAPPROVE;
       private string _APPROVAL_REMARK;
        
        //QUOTENTRY PROPERTY
       //public string AUTHORITY
       //{
       //    get
       //    {
       //        return _AUTHORITY;
       //    }
       //    set
       //    {
       //        if (_AUTHORITY != value)
       //        {
       //            _AUTHORITY = value;
       //        }
       //    }
       //}
       public int QNO
       {
           get
           {
               return _QNO;
           }
           set
           {
               if (_QNO != value)
               {
                   _QNO = value;
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

         
        public char ISAPPROVE
        {
            get{return _ISAPPROVE;}
            set{
                if (_ISAPPROVE != value)
               {
                   _ISAPPROVE = value;
               }
            }
        }
        
        public string  APPROVAL_REMARK
         {
            get{return _APPROVAL_REMARK;}
            set{
                if (_APPROVAL_REMARK != value)
               {
                   _APPROVAL_REMARK = value;
               }
            }
        }

       public string REFNO
       {
           get
           {
               return _REFNO ;
           }
           set
           {
               if (_REFNO != value)
               {
                   _REFNO = value;
               }
           }
       }

       public int MDNO
       {
           get
           {
               return _MDNO ;
           }
           set
           {
               if (_MDNO != value)
               {
                   _MDNO = value;
               }
           }
       }
       public string  INDNO
       {
           get
           {
               return _INDNO;
           }
           set
           {
               if (_INDNO != value)
               {
                   _INDNO = value;
               }
           }
       }

       public DateTime  ODATE
       {
           get
           {
               return _ODATE ;
           }
           set
           {
               if (_ODATE  != value)
               {
                   _ODATE = value;
               }
           }
       }
       public DateTime OTIME
       {
           get
           {
               return _OTIME;
           }
           set
           {
               if (_OTIME != value)
               {
                   _OTIME = value;
               }
           }
       }
       public DateTime LDATE
       {
           get
           {
               return _LDATE ;
           }
           set
           {
               if (_LDATE != value)
               {
                   _LDATE = value;
               }
           }
       }
       public DateTime LTIME
       {
           get
           {
               return _LTIME;
           }
           set
           {
               if (_LTIME != value)
               {
                   _LTIME = value;
               }
           }
       }
       public DateTime SDATE
       {
           get
           {
               return _SDATE ;
           }
           set
           {
               if (_SDATE != value)
               {
                   _SDATE = value;
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
       public string MATTER
       {
           get
           {
               return _MATTER;
           }
           set
           {
               if (_MATTER != value)
               {
                   _MATTER = value;
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
       public string TERM
       {
           get
           {
               return _TERMS ;
           }
           set
           {
               if (_TERMS != value)
               {
                   _TERMS = value;
               }
           }
       }
       public string TOPSPECI
       {
           get
           {
               return _TOPSPECI;
           }
           set
           {
               if (_TOPSPECI != value)
               {
                   _TOPSPECI = value;
               }
           }
       }
       public double  QUOTAMT
       {
           get
           {
               return _QUOTAMT ;
           }
           set
           {
               if (_QUOTAMT != value)
               {
                   _QUOTAMT = value;
               }
           }
       }
       public int  RTVALID
       {
           get
           {
               return _RTVALID ;
           }
           set
           {
               if (_RTVALID != value)
               {
                   _RTVALID = value;
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
       public string RTVALIDUNIT
       {
           get
           {
               return _RTVALIDUNIT ;
           }
           set
           {
               if (_RTVALIDUNIT != value)
               {
                   _RTVALIDUNIT = value;
               }
           }
       }
        
        #endregion
        #region STORE_TENDER
      private int  _TNO;
      private string _TENDERNO;
     private string  _TDRNO;
     private DateTime  _LDATESALE;
     private DateTime  _LTIMESALE;
     private DateTime  _SUBMITDATE;
     private DateTime  _SUBMITTIME;
     private DateTime  _TDODATE;
     private DateTime  _TDOTIME;
     private double  _DDAMT;
     private double  _EMD;
     private decimal   _STAX;
     private double  _TDAMT;
     private double  _TOTAMT;
     private string  _SPECI;
     private string _IndNO;
     private string  _INFAVOUR;
     
    
      
        //Tender Property


     public int TNO
     {
         get
         {
             return _TNO;
         }
         set
         {
             if (_TNO != value)
             {
                 _TNO = value;
             }
         }
     }
     public string  TENDERNO
     {
         get
         {
             return _TENDERNO ;
         }
         set
         {
             if (_TENDERNO != value)
             {
                 _TENDERNO = value;
             }
         }
     }
     public string TDRNO
     {
         get
         {
             return _TDRNO ;
         }
         set
         {
             if (_TDRNO != value)
             {
                 _TDRNO = value;
             }
         }
     }
     public DateTime  LDATESALE
     {
         get
         {
             return _LDATESALE;
         }
         set
         {
             if (_LDATESALE != value)
             {
                 _LDATESALE = value;
             }
         }
     }
     public DateTime LTIMESALE
     {
         get
         {
             return _LTIMESALE;
         }
         set
         {
             if (_LTIMESALE != value)
             {
                 _LTIMESALE = value;
             }
         }
     }
     public DateTime SUBMITDATE
     {
         get
         {
             return _SUBMITDATE;
         }
         set
         {
             if (_SUBMITDATE != value)
             {
                 _SUBMITDATE = value;
             }
         }
     }
     public DateTime SUBMITTIME
     {
         get
         {
             return _SUBMITTIME;
         }
         set
         {
             if (_SUBMITTIME != value)
             {
                 _SUBMITTIME = value;
             }
         }
     }
     public DateTime TDODATE
     {
         get
         {
             return _TDODATE;
         }
         set
         {
             if (_TDODATE != value)
             {
                 _TDODATE = value;
             }
         }
     }
     public DateTime TDOTIME
     {
         get
         {
             return _TDOTIME;
         }
         set
         {
             if (_TDOTIME != value)
             {
                 _TDOTIME = value;
             }
         }
     }
     public double DDAMT
     {
         get
         {
             return _DDAMT;
         }
         set
         {
             if (_DDAMT != value)
             {
                 _DDAMT = value;
             }
         }
     }
     public double EMD
     {
         get
         {
             return _EMD;
         }
         set
         {
             if (_EMD != value)
             {
                 _EMD = value;
             }
         }
     }
     public decimal STAX
     {
         get
         {
             return _STAX;
         }
         set
         {
             if (_STAX != value)
             {
                 _STAX = value;
             }
         }
     }
     public double TDAMT
     {
         get
         {
             return _TDAMT;
         }
         set
         {
             if (_TDAMT != value)
             {
                 _TDAMT = value;
             }
         }
     }
     public double TOTAMT
     {
         get
         {
             return _TOTAMT;
         }
         set
         {
             if (_TOTAMT != value)
             {
                 _TOTAMT = value;
             }
         }
     }
     public string  SPECI
     {
         get
         {
             return _SPECI ;
         }
         set
         {
             if (_SPECI != value)
             {
                 _SPECI = value;
             }
         }
     }
         public string  INDENTNO
     {
         get
         {
             return _IndNO  ;
         }
         set
         {
             if (_IndNO != value)
             {
                 _IndNO = value;
             }
         }
     }
        #endregion
    }
}
