

//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Degree MASTER                             
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : RISHABH
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.RFC_CONFIG
        {
            public class Degree
            {
                #region private members

                private int _degreetype_id = 0;
                private int _organizationId;

                private string _degreeTypeName;
                private string _degreeCode;
                // Added by Rishabh on 07-10-2021
                private int _Degree_id = 0;
                private int _degreeTypeID;
                private string _degree_Name;
                private string _degreeShort_Name;
                //End

                #endregion


                #region public members
                //Added by rishabh - 23/10/2021
                public string DegreeCode
                {
                    get
                    {
                        return _degreeCode;
                    }
                    set
                    {
                        _degreeCode = value;
                    }
                }
                public int DegreeTypeID
                {
                    get { return _degreetype_id; }
                    set { _degreetype_id = value; }
                }

                public int Organization
                {
                    get { return _organizationId; }
                    set { _organizationId = value; }
                }

                public string DegreeTypeName
                {
                    get { return _degreeTypeName; }
                    set { _degreeTypeName = value; }
                }

                public bool ActiveStatus
                {
                    get;
                    set;
                }
                // Added by Rishabh on 07-10-2021
                public int DegreeID
                {
                    get
                    {
                        return _Degree_id;
                    }
                    set
                    {
                        _Degree_id = value;
                    }
                }

                public int Degree_Type_ID
                {
                    get
                    {
                        return _degreeTypeID;
                    }
                    set
                    {
                        _degreeTypeID = value;
                    }
                }

                public string DegreeName
                {
                    get
                    {
                        return _degree_Name;
                    }
                    set
                    {
                        _degree_Name = value;
                    }
                }

                public string DegreeShort_Name
                {
                    get
                    {
                        return _degreeShort_Name;
                    }
                    set
                    {
                        _degreeShort_Name = value;
                    }
                }
                //End
                #endregion
            }
        }
    }
}
