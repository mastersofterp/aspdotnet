using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class BioMetrics
            {

                #region Private members
                //[Table(Name="HOLIDAY_MASTER")]	
                private DateTime _HOLIDAYDATE;
                private string _HOLIDAYNAME;
                private int _HOLIDAYDAYS;
                private int _HOLIDAYNO;

            

                //[TABLE(namespace="NGAC_TOUR_MASTER"]
                private string _USERID;
                private string _EMPNAME;
                private DateTime _TOURFROMDT;
                private DateTime _TOURTODT;
                private int _TOURDAYS;
                private string _CLIENT;
                private int _TOURNO;

               
                #endregion

                #region public members

                public DateTime HOLIDAYDATE
                {
                    get
                    {
                        return this._HOLIDAYDATE;
                    }
                    set
                    {
                        if ((this._HOLIDAYDATE) != value)
                        {
                            this._HOLIDAYDATE = value;
                        }
                    }
                }
                public string HOLIDAYNAME
                {
                    get
                    {
                        return (this._HOLIDAYNAME);
                    }
                    set
                    {
                        if ((this._HOLIDAYNAME) != value)
                        {
                            this._HOLIDAYNAME = value;
                        }
                    }

                }
                public int HOLIDAYDAYS
                {
                    get
                    {
                        return _HOLIDAYDAYS;
                    }
                    set
                    {
                        _HOLIDAYDAYS = value;

                    }

                }

                public string USERID
                {
                    get
                    {
                        return (this._USERID);
                    }
                    set
                    {
                        if ((this._USERID) != value)
                        {
                            this._USERID = value;
                        }
                    }
                }
                public string EMPNAME
                {
                    get { return _EMPNAME; }
                    set { _EMPNAME = value; }
                }
                public DateTime TOURFROMDT
                {
                    get { return _TOURFROMDT; }
                    set { _TOURFROMDT = value; }
                }
                public DateTime TOURTODT
                {
                    get { return _TOURTODT; }
                    set { _TOURTODT = value; }
                }
                public int TOURDAYS
                {
                    get { return _TOURDAYS; }
                    set { _TOURDAYS = value; }
                }
                public string CLIENT
                {
                    get { return _CLIENT; }
                    set { _CLIENT = value; }

                }
                public int HOLIDAYNO
                {
                    get { return _HOLIDAYNO; }
                    set { _HOLIDAYNO = value; }
                }
                public int TOURNO
                {
                    get { return _TOURNO; }
                    set { _TOURNO = value; }
                }
                #endregion


            } //end class BioMetrics
        }//end namespace  BusinessLogicLayer.BusinessEntities 
    }//end namespace NITPRM
}//end namespace IITMS
