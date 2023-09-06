using System;
using System.Collections;

namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class Str_Budget
    {

        #region budget

        private System.Nullable<int> _BUDNO;
		
		private System.Nullable<int> _HNO;
		
		private int _DCODE;
		
		private System.Nullable<decimal> _AMT;
		
		private System.Nullable<System.DateTime> _SDATE;
		
		private System.Nullable<System.DateTime> _EDATE;
		
		private System.Nullable<decimal> _BALAMT;
		
		private string _COLLEGE_CODE;

        #endregion

        #region budgetHead

        private string _HEADNAME;

        private System.Nullable<decimal> _AMOUNT;       

        private System.Nullable<System.DateTime> _ENDDATE;

        private string _NATURE;

        private string _SCHEME;

        private string _COORDINATOR;
       

        #endregion

        //[Column(Storage="_BUDNO", DbType="Int")]
		public System.Nullable<int> BUDNO
		{
			get
			{
				return this._BUDNO;
			}
			set
			{
				if ((this._BUDNO != value))
				{
					this._BUDNO = value;
				}
			}
		}
		
		//[Column(Storage="_HNO", DbType="Int")]
		public System.Nullable<int> HNO
		{
			get
			{
				return this._HNO;
			}
			set
			{
				if ((this._HNO != value))
				{
					this._HNO = value;
				}
			}
		}
		
		//[Column(Storage="_DCODE", DbType="NVarChar(12)")]
		public int DCODE
		{
			get
			{
				return this._DCODE;
			}
			set
			{
				if ((this._DCODE != value))
				{
					this._DCODE = value;
				}
			}
		}
		
		//[Column(Storage="_AMT", DbType="Decimal(14,2)")]
		public System.Nullable<decimal> AMT
		{
			get
			{
				return this._AMT;
			}
			set
			{
				if ((this._AMT != value))
				{
					this._AMT = value;
				}
			}
		}
		
		//[Column(Storage="_SDATE", DbType="DateTime")]
		public System.Nullable<System.DateTime> SDATE
		{
			get
			{
				return this._SDATE;
			}
			set
			{
				if ((this._SDATE != value))
				{
					this._SDATE = value;
				}
			}
		}
		
		//[Column(Storage="_EDATE", DbType="DateTime")]
		public System.Nullable<System.DateTime> EDATE
		{
			get
			{
				return this._EDATE;
			}
			set
			{
				if ((this._EDATE != value))
				{
					this._EDATE = value;
				}
			}
		}
		
		//[Column(Storage="_BALAMT", DbType="Decimal(14,2)")]
		public System.Nullable<decimal> BALAMT
		{
			get
			{
				return this._BALAMT;
			}
			set
			{
				if ((this._BALAMT != value))
				{
					this._BALAMT = value;
				}
			}
		}
		
		//[Column(Storage="_COLLEGE_CODE", DbType="NVarChar(15)")]
		public string COLLEGE_CODE
		{
			get
			{
				return this._COLLEGE_CODE;
			}
			set
			{
				if ((this._COLLEGE_CODE != value))
				{
					this._COLLEGE_CODE = value;
				}
			}
		}


        //[Column(Storage = "_HEADNAME", DbType = "NVarChar(50)")]
        public string HEADNAME
        {
            get
            {
                return this._HEADNAME;
            }
            set
            {
                if ((this._HEADNAME != value))
                {
                    this._HEADNAME = value;
                }
            }
        }

        //[Column(Storage = "_AMOUNT", DbType = "Decimal(11,2)")]
        public System.Nullable<decimal> AMOUNT
        {
            get
            {
                return this._AMOUNT;
            }
            set
            {
                if ((this._AMOUNT != value))
                {
                    this._AMOUNT = value;
                }
            }
        }

       
        //[Column(Storage = "_ENDDATE", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ENDDATE
        {
            get
            {
                return this._ENDDATE;
            }
            set
            {
                if ((this._ENDDATE != value))
                {
                    this._ENDDATE = value;
                }
            }
        }

       // [Column(Storage = "_NATURE", DbType = "NVarChar(100)")]
        public string NATURE
        {
            get
            {
                return this._NATURE;
            }
            set
            {
                if ((this._NATURE != value))
                {
                    this._NATURE = value;
                }
            }
        }

        //[Column(Storage = "_SCHEME", DbType = "NVarChar(100)")]
        public string SCHEME
        {
            get
            {
                return this._SCHEME;
            }
            set
            {
                if ((this._SCHEME != value))
                {
                    this._SCHEME = value;
                }
            }
        }

        //[Column(Storage = "_COORDINATOR", DbType = "NVarChar(50)")]
        public string COORDINATOR
        {
            get
            {
                return this._COORDINATOR;
            }
            set
            {
                if ((this._COORDINATOR != value))
                {
                    this._COORDINATOR = value;
                }
            }
        }
		
    }
}
