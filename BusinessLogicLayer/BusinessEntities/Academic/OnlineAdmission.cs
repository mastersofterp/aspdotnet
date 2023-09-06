// Added by Pooja Sandel on 04/02/2022


using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class OnlineAdmission
            {
                #region Private Members
                private int _DEGREE_NO;
                private int _DEGREE_TYPE;
                private int _SUBDEGREE_TYPE;
                private string _DEGREE;
                private int _DEGREE_CODE;
                private int _CREATED_BY;
                private DateTime _CREATED_DATE;
                private string _IP_ADDRESS;
                private string _SPECIALIZATION;
                # endregion

                #region public members
                public string SPECIALIZATION
                {
                    get { return _SPECIALIZATION; }
                    set { _SPECIALIZATION = value; }
                }
                public int DEGREE_NO
                {
                    get { return _DEGREE_NO; }
                    set { _DEGREE_NO = value; }
                }

                public int DEGREE_TYPE
                {
                    get { return _DEGREE_TYPE; }
                    set { _DEGREE_TYPE = value; }
                }

                public int SUBDEGREE_TYPE
                {
                    get { return _SUBDEGREE_TYPE; }
                    set { _SUBDEGREE_TYPE = value; }
                }


                public string DEGREE
                {
                    get { return _DEGREE; }
                    set { _DEGREE = value; }
                }

                public int DEGREE_CODE
                {
                    get { return _DEGREE_CODE; }
                    set { _DEGREE_CODE = value; }
                }

                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }


                public DateTime CREATED_DATE
                {
                    get { return _CREATED_DATE; }
                    set { _CREATED_DATE = value; }
                }


                public string IP_ADDRESS
                {
                    get { return _IP_ADDRESS; }
                    set { _IP_ADDRESS = value; }
                }

                #endregion
            }
        }
    }
}