using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class DocumentType
            {
                private int _DOCID;
                private string _DOCUMENTTYPE;

                private int _DOCDIDDATA;
                private string _DOCTYPE;    
                private System.Nullable<System.DateTime> _DOCDATE;
                private string _DOCNO;
                private string _PROP_ADDRESS;
                private string _DISTRICT;
                private string _SURVEY_NO;
                private string _SUBDIVI_NO;
                private decimal _TOTAREA;
                private string _FILENAME;
                private string _FILEPTH;
                private int _ISBLOB;
                private string _EC_NO;
                private decimal _NORTH;
                private decimal _SOUTH;
                private decimal _EAST;
                private decimal _WEST;
                private string _OTHERDOCTYPE;
                private System.Nullable<System.DateTime> _FROM_DATE;
                private System.Nullable<System.DateTime> _TO_DATE;
                private DataTable _AttachTable;
                private int _UA_NO;

                public int DOCID
                {
                    get { return _DOCID; }
                    set { _DOCID = value; }
                }

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }

                public string DOCUMENTTYPE
                {
                    get { return _DOCUMENTTYPE; }
                    set { _DOCUMENTTYPE = value; }
                }

                public int DOCDIDDATA
                {
                    get { return _DOCDIDDATA; }
                    set { _DOCDIDDATA = value; }
                }

                public string DOCTYPE
                { 
                    get  { return _DOCTYPE; }
                    set  { _DOCTYPE = value; }
                 }

                 public System.Nullable<System.DateTime> DOCDATE
                { 
                    get  { return _DOCDATE; }
                    set  { _DOCDATE = value; }
                }

                public string DOCNO 
                {
                    get { return _DOCNO; }
                    set  {  _DOCNO = value; }
                }

                public string PROP_ADDRESS 
                {
                    get  { return _PROP_ADDRESS; }
                    set  { _PROP_ADDRESS =value; }
                }

                public string District
                { 
                    get { return _DISTRICT; }
                    set {  _DISTRICT =value; }
                }

                public string SURVEYNO 
                { 
                    get { return _SURVEY_NO; }
                    set { _SURVEY_NO =value; }
                }

                public string SUBDIVINO 
                {
                    get { return _SUBDIVI_NO; }
                    set {  _SUBDIVI_NO =value; }
                }

                public decimal TOTAREA
                { 
                    get { return _TOTAREA; }
                    set {  _TOTAREA=value; }
                }

                public decimal NORTH
                {
                    get { return _NORTH; }
                    set { _NORTH = value; }
                }
                public decimal SOUTH
                {
                    get { return _SOUTH; }
                    set { _SOUTH = value; }

                }
                public decimal EAST
                {
                    get { return _EAST; }
                    set { _EAST = value; }
                }

                public decimal WEST
                {
                    get { return _WEST; }
                    set { _WEST = value; }
                } 
                public string FILENAME 
                { 
                    get { return _FILENAME; }
                    set {  _FILENAME=value; } 
                }

                public string FILEPTH
                { 
                    get { return _FILEPTH; }
                    set { _FILEPTH=value; }
                }

                public string EC_NO
                {
                    get { return _EC_NO; }
                    set { _EC_NO = value; }
                }

                public System.Nullable<System.DateTime> TO_DATE
                {
                    get
                    {
                        return this._TO_DATE;
                    }
                    set
                    {
                        if ((this._TO_DATE != value))
                        {
                            this._TO_DATE = value;
                        }
                    }
                }
                public System.Nullable<System.DateTime> FROM_DATE
                {
                    get
                    {
                        return this._FROM_DATE;
                    }
                    set
                    {
                        if ((this._FROM_DATE != value))
                        {
                            this._FROM_DATE = value;
                        }
                    }
                }


                public int ISBLOB
                {
                    get
                    {
                        return this._ISBLOB;
                    }
                    set
                    {
                        if ((this._ISBLOB != value))
                        {
                            this._ISBLOB = value;
                        }
                    }
                }


                 public DataTable AttachTable
                {
                    get { return _AttachTable; }
                    set { _AttachTable = value; }
                }

                 public string OTHERDOCTYPE
                 {
                     get { return _OTHERDOCTYPE; }
                     set { _OTHERDOCTYPE = value; }
                 }
            }
        }
    }
}