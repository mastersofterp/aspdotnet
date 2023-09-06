using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class CopyQuestionToNextSessionEnt
            {
                private char _OBJECTIVE_DESCRIPTIVE;
                private int _COURSENO;
                private int _UA_NO;
                private string _TOPIC;
                private DataTable _QuestionTbl_TRAN = null;


                public DataTable QuestionTbl_TRAN
                {
                    get { return _QuestionTbl_TRAN; }
                    set { _QuestionTbl_TRAN = value; }
                }


                public char OBJECTIVE_DESCRIPTIVE
                {
                    get { return _OBJECTIVE_DESCRIPTIVE; }
                    set { _OBJECTIVE_DESCRIPTIVE = value; }
                }
                public int COURSENO
                {
                    get
                    {
                        return _COURSENO;
                    }
                    set
                    {

                        _COURSENO = value;
                    }
                }

                public int UA_NO
                {
                    get
                    {
                        return _UA_NO;
                    }
                    set
                    {
                        _UA_NO = value;
                    }
                }

                public string TOPIC
                {
                    get
                    {
                        return _TOPIC;
                    }
                    set
                    {
                        _TOPIC = value;
                    }
                }

            }
        }
    }
}
