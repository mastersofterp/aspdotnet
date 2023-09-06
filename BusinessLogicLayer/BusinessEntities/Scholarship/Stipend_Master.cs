using System;
using System.Collections.Generic;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
  public   class Stipend_Master
    {
        #region private member
        private int _idNo = 0;
        private int _srNo = 0;
        private System.Nullable<System.DateTime> _sTFROMDT;
        private System.Nullable<System.DateTime>  _sTTODT;
        private int _sTIPEND_MONTH = 0;
        private decimal _mON_STIPEND_AMT = 0;
        private decimal _oPBAL = 0;
        private int _sPSTATUS = 0;


        private System.Nullable<System.DateTime> _sTFROMDT1;
        private System.Nullable<System.DateTime> _sTTODT1;
        private int _sTIPEND_MONTH1 = 0;
        private decimal _mON_STIPEND_AMT1 = 0;


        private System.Nullable<System.DateTime>  _sTFROMDT2;
        private System.Nullable<System.DateTime> _sTTODT2;
        private int _sTIPEND_MONTH2 = 0;
        private decimal _mON_STIPEND_AMT2 = 0;

       private System.Nullable<System.DateTime> _rEVISEDDATE; 
	   private decimal  _rEVISEDAMT = 0;
       private decimal _rADDITIONALAMT = 0;
       private System.Nullable<System.DateTime> _eFFECTIVEREVISEDDATE;
       private System.Nullable<System.DateTime> _aCCLOSEDATE;
       private string   _aCCLOSERMK = string.Empty;
       private string _idnostr = string.Empty;

       private int _sTOPTRANSACTIO = 0;
       private string _rEMARKS = string.Empty;
      
       #endregion


       #region public property field
       public int Id_No
       {
           get { return _idNo; }
           set { _idNo = value; }
       }
       public int Sr_No
       {
           get { return _srNo; }
           set { _srNo = value; }
       }
       public System.Nullable<System.DateTime> STFROMDT
       {
           get { return _sTFROMDT; }
           set
           {
               if(this._sTFROMDT != value)
               {
                   this._sTFROMDT = value;
               }
           }
          
       }
       public System.Nullable<System.DateTime> STTODT
       {
           get { return _sTTODT; }
           set {
               if(this._sTTODT != value)
               {
                   this._sTTODT = value;
               }
              }
       }
       public int STIPEND_MONTH
       {
           get { return _sTIPEND_MONTH; }
           set { _sTIPEND_MONTH = value; }
       }
      
       public decimal  MON_STIPEND_AMT
        {
           get { return _mON_STIPEND_AMT; }
           set { _mON_STIPEND_AMT = value; }
       }


       public System.Nullable<System.DateTime> STFROMDT1
       {
           get { return _sTFROMDT1; }
           set
           {
               if (this._sTFROMDT1 != value)
               {
                   this._sTFROMDT1 = value;
               }
           }
       }
       public System.Nullable<System.DateTime> STTODT1
       {
           get { return _sTTODT1; }
           set
           {
               if (this._sTTODT1 != value)
               {
                   this._sTTODT1 = value;
               }
           }
       }
       public int STIPEND_MONTH1
       {
           get { return _sTIPEND_MONTH1; }
           set { _sTIPEND_MONTH1 = value; }
       }
       public decimal MON_STIPEND_AMT1
       {
           get { return _mON_STIPEND_AMT1; }
           set { _mON_STIPEND_AMT1 = value; }
       }


       public System.Nullable<System.DateTime> STFROMDT2
       {
           get { return _sTFROMDT2; }
           set
           {
               if (this._sTFROMDT2 != value)
               {
                   this._sTFROMDT2 = value;
               }
           }
       }
       public System.Nullable<System.DateTime> STTODT2
       {
           get { return _sTTODT2; }
           set
           {
               if (this._sTTODT2 != value)
               {
                   this._sTTODT2 = value;
               }
           }
       }
       public int STIPEND_MONTH2
       {
           get { return _sTIPEND_MONTH2; }
           set { _sTIPEND_MONTH2 = value; }
       }
       public decimal MON_STIPEND_AMT2
       {
           get { return _mON_STIPEND_AMT2; }
           set { _mON_STIPEND_AMT2 = value; }
       }
       public decimal OPBAL
       {
           get { return _oPBAL; }
           set { _oPBAL = value; }
       }
       public int  SPSTATUS
       {
           get { return _sPSTATUS; }
           set { _sPSTATUS = value; }
       }
       public System.Nullable<System.DateTime> REVISEDDATE
       {
           get { return _rEVISEDDATE; }

           set
           {
               if (this._rEVISEDDATE != value)
               {
                   this._rEVISEDDATE = value;
               }
           }
       }
       public decimal  REVISEDAMT
       {
           get { return _rEVISEDAMT; }
           set { _rEVISEDAMT = value; }
       }
       public decimal ADDITIONALAMT
       {
           get { return _rADDITIONALAMT; }
           set { _rADDITIONALAMT = value; }
       }
       public System.Nullable<System.DateTime> EFFECTIVEREVISEDDATE
       {
           get { return _eFFECTIVEREVISEDDATE; }
           set
           {
               if (this._eFFECTIVEREVISEDDATE != value)
               {
                   this._eFFECTIVEREVISEDDATE = value;
               }
           }
       }
       public System.Nullable<System.DateTime> ACCLOSEDATE
       {
           get { return _aCCLOSEDATE; }
           set
           {
               if (this._aCCLOSEDATE != value)
               {
                   this._aCCLOSEDATE = value;
               }
           }
       }
       public string ACCLOSERMK
       {
           get { return _aCCLOSERMK; }
           set { _aCCLOSERMK = value; }
       }

       public string IDNOSTR
       {
           get { return _idnostr; }
           set { _idnostr = value; }
       }
       public string Remarks
       {
           get { return _rEMARKS; }
           set { _rEMARKS = value; }
       }
       public int StopTransaction
       {
           get { return _sTOPTRANSACTIO; }
           set { _sTOPTRANSACTIO = value; }
       }
       #endregion

    }
}
