using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class DefineCreditLimit
            {
                #region Private members
                private string _term = string.Empty;
                private string _term_text = string.Empty;
                private int _session = 0;
                private int _degreeNo = 0;
                private int _student_type = 0;
                private int _adm_type = 0;
                private int _from_credit = 0;
                private int _to_credit = 0;
                private double _to_cgpa = 0.0;
                private double _from_cgpa = 0.0;
                private int _minRegCreditLimit = 0;
                private int _PubliishYesNo = 0;

                private int _additional_course = 0;
                private int _degree_type = 0;
                private int _min_schemeLimit = 0;
                private int _max_schemeLimit = 0;
                private int _recordNo = 0;
                private string _sessionName = string.Empty;
                private string branchnos = string.Empty;
                private string branchnosText = string.Empty;
                private int _elective_groupno = 0;
                private int _elective_choisefor = 0;

                private int _schemeNo = 0;
                private string _schemeName = string.Empty;

                private int _core_credit = 0;
                private int _elective_credit = 0;
                private int _global_credit = 0;
                private int _overload_credit = 0;
                private string _schemenos = string.Empty;
                private string _electivegroupnos = string.Empty;
                #endregion

                #region Public Property Fields

                public string Schemenos
                {
                    get { return _schemenos; }
                    set { _schemenos = value; }
                }
                public string Electivegroupnos
                {
                    get { return _electivegroupnos; }
                    set { _electivegroupnos = value; }
                }

                public int PUBLIISH_YES_NO
                {
                    get { return _PubliishYesNo; }
                    set { _PubliishYesNo = value; }
                }


                public string BRANCHNOS
                {
                    get { return branchnos; }
                    set { branchnos = value; }
                }
                public string BRANCHNOS_TEXT
                {
                    get { return branchnosText; }
                    set { branchnosText = value; }
                }


                public int MIN_REG_CREDIT_LIMIT
                {
                    get { return _minRegCreditLimit; }
                    set { _minRegCreditLimit = value; }
                }

                public string TERM
                {
                    get { return _term; }
                    set { _term = value; }
                }

                public string TERM_TEXT
                {
                    get { return _term_text; }
                    set { _term_text = value; }
                }

                public int RECORDNO
                {
                    get { return _recordNo; }
                    set { _recordNo = value; }
                }

                public int DEGREENO
                {
                    get { return _degreeNo; }
                    set { _degreeNo = value; }
                }

                public int SESSION
                {
                    get { return _session; }
                    set { _session = value; }
                }

                public int STUDENT_TYPE
                {
                    get { return _student_type; }
                    set { _student_type = value; }
                }
                public int FROM_CREDIT
                {
                    get { return _from_credit; }
                    set { _from_credit = value; }
                }
                public int TO_CREDIT
                {
                    get { return _to_credit; }
                    set { _to_credit = value; }
                }

                public int ADM_TYPE
                {
                    get { return _adm_type; }
                    set { _adm_type = value; }
                }

                public double TO_CGPA
                {
                    get { return _to_cgpa; }
                    set { _to_cgpa = value; }
                }



                public double FROM_CGPA
                {
                    get { return _from_cgpa; }
                    set { _from_cgpa = value; }
                }

                public int ADDITIONAL_COURSE
                {
                    get { return _additional_course; }
                    set { _additional_course = value; }
                }
                public int DEGREE_TYPE
                {
                    get { return _degree_type; }
                    set { _degree_type = value; }
                }
                public int MIN_SCHEMELIMIT
                {
                    get { return _min_schemeLimit; }
                    set { _min_schemeLimit = value; }
                }

                public int MAX_SCHEMELIMIT
                {
                    get { return _max_schemeLimit; }
                    set { _max_schemeLimit = value; }
                }

                public string SESSIONNAME
                {
                    get { return _sessionName; }
                    set { _sessionName = value; }
                }
                public int ELECTIVE_GROUPNO { get { return _elective_groupno; } set { _elective_groupno = value; } }
                public int ELECTIVE_CHOISEFOR { get { return _elective_choisefor; } set { _elective_choisefor = value; } }

                public int SCHEMENO { get { return _schemeNo; } set { _schemeNo = value; } }
                public string SCHEMENAME { get { return _schemeName; } set { _schemeName = value; } }

                public int Core_credit
                { get { return _core_credit; } set { _core_credit = value; } }
                public int Elective_credit
                { get { return _elective_credit; } set { _elective_credit = value; } }
                public int Global_credit
                { get { return _global_credit; } set { _global_credit = value; } }
                public int Overload_credit
                {
                    get { return _overload_credit; }
                    set { _overload_credit = value; }
                }

                #endregion

            }
        }
    }
}