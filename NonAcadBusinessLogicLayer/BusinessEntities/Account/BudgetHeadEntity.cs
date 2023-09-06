using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class BudgetHeadEntity
            {
                private int _BUDGET_NO = 0;
                private string _BUDGET_CODE = string.Empty;
                private string _BUDGET_HEAD = string.Empty;
                private int _PARENT_ID = 0;
                private string _SERIAL_NO = string.Empty;
                private int _BUDGET_PRAPOSAL = 0;
                private int _COLLEGE_CODE = 0;
                private int _CREATED_BY = 0;
                private string _REC_NONREC = string.Empty;
                private int _PARTY_NO = 0;
                //BUDGET_CODE
                //BUDGET_HEAD
                //PARENT_ID
                //SERIAL_NO
                //BUDGET_PRAPOSAL
                //COLLEGE_CODE
                //CREATED_BY
                //CREATED_DATE
                //MODIFIED_BY
                //MODIFIED_DATE


                public int BUDGET_NO
                {
                    get { return _BUDGET_NO; }
                    set { _BUDGET_NO = value; }
                }
                public string BUDGET_CODE
                {
                    get { return _BUDGET_CODE; }
                    set { _BUDGET_CODE = value; }
                }
                public string BUDGET_HEAD
                {
                    get { return _BUDGET_HEAD; }
                    set { _BUDGET_HEAD = value; }
                }
                public int PARENT_ID
                {
                    get { return _PARENT_ID; }
                    set { _PARENT_ID = value; }
                }
                public string SERIAL_NO
                {
                    get { return _SERIAL_NO; }
                    set { _SERIAL_NO = value; }
                }
                public int BUDGET_PRAPOSAL
                {
                    get { return _BUDGET_PRAPOSAL; }
                    set { _BUDGET_PRAPOSAL = value; }
                }
                public int COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }
                public int PARTYNO
                {
                    get { return _PARTY_NO; }
                    set { _PARTY_NO = value; }
                }
                public string RECURRING
                {
                    get { return _REC_NONREC; }
                    set { _REC_NONREC = value; }
                }


            }
        }
    }
}