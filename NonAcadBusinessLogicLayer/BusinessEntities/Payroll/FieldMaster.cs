using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class FieldMaster
            {
                private System.Nullable<int> _FIELD_NO;
        		
		        private string _FIELD_NAME;
        		
		        private System.Nullable<char> _FIELD_TYPE;
        		
		        private System.Nullable<int> _SERIAL_NO;
        		
		        private System.Nullable<char> _IND_FOR;
        		
		        private System.Nullable<bool> _CALC_ON_BASICAMT_YN;
        		
		        private string _COLLEGE_CODE;
                		
    	        //[Column(Storage="_FIELD_NO", DbType="Int")]
		        public System.Nullable<int> FIELD_NO
		        {
			        get
			        {
				        return this._FIELD_NO;
			        }
			        set
			        {
				        if ((this._FIELD_NO != value))
				        {
					        this._FIELD_NO = value;
				        }
			        }
		        }
        		
		        //[Column(Storage="_FIELD_NAME", DbType="NVarChar(50)")]
		        public string FIELD_NAME
		        {
			        get
			        {
				        return this._FIELD_NAME;
			        }
			        set
			        {
				        if ((this._FIELD_NAME != value))
				        {
					        this._FIELD_NAME = value;
				        }
			        }
		        }
        		
		        //[Column(Storage="_FIELD_TYPE", DbType="NVarChar(1)")]
		        public System.Nullable<char> FIELD_TYPE
		        {
			        get
			        {
				        return this._FIELD_TYPE;
			        }
			        set
			        {
				        if ((this._FIELD_TYPE != value))
				        {
					        this._FIELD_TYPE = value;
				        }
			        }
		        }
        		
		        //[Column(Storage="_SERIAL_NO", DbType="Int")]
		        public System.Nullable<int> SERIAL_NO
		        {
			        get
			        {
				        return this._SERIAL_NO;
			        }
			        set
			        {
				        if ((this._SERIAL_NO != value))
				        {
					        this._SERIAL_NO = value;
				        }
			        }
		        }
        		
		        //[Column(Storage="_IND_FOR", DbType="NVarChar(1)")]
		        public System.Nullable<char> IND_FOR
		        {
			        get
			        {
				        return this._IND_FOR;
			        }
			        set
			        {
				        if ((this._IND_FOR != value))
				        {
					        this._IND_FOR = value;
				        }
			        }
		        }
        		
		        //[Column(Storage="_CALC_ON_BASICAMT_YN", DbType="Bit")]
		        public System.Nullable<bool> CALC_ON_BASICAMT_YN
		        {
			        get
			        {
				        return this._CALC_ON_BASICAMT_YN;
			        }
			        set
			        {
				        if ((this._CALC_ON_BASICAMT_YN != value))
				        {
					        this._CALC_ON_BASICAMT_YN = value;
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
            }
        }
    }
}
