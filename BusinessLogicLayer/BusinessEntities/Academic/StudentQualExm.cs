using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
                public class StudentQualExm
                {
                    #region Private Member
                    private string _YEAR_OF_EXAM = string.Empty;
                    private string _REGNO = string.Empty;
                    private int _QUALIFYNO = 0;
                    private string _QEXMROLLNO = string.Empty;
                    private int _STATERANK = 0;
                    private int _ALLINDIARANK = 0;
                    private decimal _PERCENTAGE = 0.00m;
                    private decimal _PERCENTILE = 0.00m;                                           
                    private string _SCHOOL_COLLEGE_NAME = string.Empty;
                    private string _BOARD = string.Empty;
                    private string _GRADE = string.Empty;
                    private decimal _MARKOBTAINED = 0;
                    private int _OUTOFMARK = 0;
                    private int _MAXMARKS = 0;
                    private int _HSCPCM = 0;
                    private decimal _HSCPCMperct = 0; //Added By Rishabh on 12/02/2022
                    private int _HSCPCMMAX = 0;
                    private int _HSCBIO = 0;
                    private int _HSCBIOMAX = 0;
                    private int _ENG = 0;
                    private int _ENGMAX = 0;
                    private int _HSCENGMAX = 0;
                    private int _MATHS = 0;
                    private int _MATHSMAX = 0;
                    private int _HSCCHE = 0;
                    private int HSCCHEMAX = 0;
                    private int HSCPHY = 0;
                    private int HSCPHYMAX = 0;
                    private string _ATTEMPT = string.Empty;
                    private string _RES_TOPIC = string.Empty;
                    private string _SUPERVISOR_NAME1 = string.Empty;
                    private string _SUPERVISOR_NAME2 = string.Empty;
                    private string _COLLEGE_CODE = string.Empty;
                    private int _MERITNO = 0;
                    private string _YEAR_OF_EXAMHSSC = string.Empty;
                    private string _QEXMROLLNOHSSC = string.Empty;
                    private string _yearOfExamSsc = string.Empty;
                    private string _schoolCollegeNameSsc = string.Empty;
                    private string _QEXMROLLNOSSC = string.Empty;
                    private decimal _marksObtainedSsc = 0;    //Changed by Yograj C on 03-09-2022 int->decimal
                    private int _outOfMarksSsc = 0;
                    private string _boardSsc = string.Empty;
                    private string _gradeSsc = string.Empty;
                    private decimal _percentageSsc = 0.0m;
                    private decimal _percentileSsc = 0.0m;
                    private string _attemptSsc = string.Empty;
                    private int _qualifynoth = 0;
                    private string _yearOfExamOth = string.Empty;                                     
                    private string _schoolCollegeNameOth = string.Empty;
                    private string _boardOth = string.Empty;
                    private string _gradeOth = string.Empty;
                    private string _attemptOth = string.Empty;
                    private int _marksObtainedOth = 0;
                    private int _outOfMarksOth = 0;
                    private decimal _perOth = 0.0m;
                    private decimal _percentileOth = 0.0m;
                    private string _resTopicOth = string.Empty;
                    private string _supervisorName1 = string.Empty;
                    private string _supervisorName2 = string.Empty;
                    private string _qexmRollNo = string.Empty;
                    private string _qexmStatus = string.Empty;
                    private string _qualexmName = string.Empty;
                    private string _collegeCode = string.Empty;
                    private int _qualexmId = 0;
                    private int _degreeno = 0;
                    string _sscmedium = string.Empty;
                    string _hsscmedium = string.Empty;
                    private string _SSCRollNo = string.Empty;
                    private string _SSCYear = string.Empty;
                    private string _colg_address_SSC = string.Empty;
                    private string _colg_address_HSSC = string.Empty;
                    private decimal _percentileHSsc = 0.0m;

                    //ADDED BY DEEPALI ON 08/07/2021
                    private string _colg_address_Diploma = string.Empty;
                    private string _attemptDiploma = string.Empty;
                    private string _gradeDiploma = string.Empty;
                    private decimal _percentileDiploma = 0.0m;
                    private string _QEXMROLLNODiploma = string.Empty;
                    private decimal _percentageDiploma = 0.0m;
                    private int _outOfMarksDiploma = 0;
                    private decimal _marksObtainedDiploma = 0;
                    string _DiplomaMedium = string.Empty;
                    private string _yearOfExamDiploma = string.Empty;
                    private string _boardDiploma = string.Empty;
                    private string _schoolCollegeNameDiploma = string.Empty;

                    #endregion
                   

                  
                    #region Property Fields
                    public decimal PercentileHSsc
                    {
                        get { return _percentileHSsc; }
                        set { _percentileHSsc = value; }
                    }

                    public string colg_address_SSC
                    {
                        get { return _colg_address_SSC; }
                        set { _colg_address_SSC = value; }
                    }
                    public string colg_address_HSSC
                    {
                        get { return _colg_address_HSSC; }
                        set { _colg_address_HSSC = value; }
                    }


                    public string SSCRollNo
                    {
                        get { return _SSCRollNo; }
                        set { _SSCRollNo = value; }
                    }


                    public string SSCYear
                    {
                        get { return _SSCYear; }
                        set { _SSCYear = value; }
                    }

                    public string YearOfExamOth
                    {
                        get { return _yearOfExamOth; }
                        set { _yearOfExamOth = value; }
                    }
                    public string SupervisorName2
                    {
                        get { return _supervisorName2; }
                        set { _supervisorName2 = value; }
                    }

                    public string SupervisorName1
                    {
                        get { return _supervisorName1; }
                        set { _supervisorName1 = value; }
                    }

                    public string ResTopicOth
                    {
                        get { return _resTopicOth; }
                        set { _resTopicOth = value; }
                    }

                    public decimal PercentileOth
                    {
                        get { return _percentileOth; }
                        set { _percentileOth = value; }
                    }

                    public decimal PerOth
                    {
                        get { return _perOth; }
                        set { _perOth = value; }
                    }


                    public int OutOfMarksOth
                    {
                        get { return _outOfMarksOth; }
                        set { _outOfMarksOth = value; }
                    }

                    public int MarksObtainedOth
                    {
                        get { return _marksObtainedOth; }
                        set { _marksObtainedOth = value; }
                    }

                    public string AttemptOth
                    {
                        get { return _attemptOth; }
                        set { _attemptOth = value; }
                    }


                    public string GradeOth
                    {
                        get { return _gradeOth; }
                        set { _gradeOth = value; }
                    }

                    public string BoardOth
                    {
                        get { return _boardOth; }
                        set { _boardOth = value; }
                    }

                    public string SchoolCollegeNameOth
                    {
                        get { return _schoolCollegeNameOth; }
                        set { _schoolCollegeNameOth = value; }
                    }


                   

                    public int Qualifynoth
                    {
                        get { return _qualifynoth; }
                        set { _qualifynoth = value; }
                    }
                    
                    public string AttemptSsc
                    {
                        get { return _attemptSsc; }
                        set { _attemptSsc = value; }
                    }

                    public decimal PercentileSsc
                    {
                        get { return _percentileSsc; }
                        set { _percentileSsc = value; }
                    }

                    public decimal PercentageSsc
                    {
                        get { return _percentageSsc; }
                        set { _percentageSsc = value; }
                    }

                    public string GradeSsc
                    {
                        get { return _gradeSsc; }
                        set { _gradeSsc = value; }
                    }

                    public string BoardSsc
                    {
                        get { return _boardSsc; }
                        set { _boardSsc = value; }
                    }

                    public int OutOfMarksSsc
                    {
                        get { return _outOfMarksSsc; }
                        set { _outOfMarksSsc = value; }
                    }

                    //Changed by Yograj C on 03-09-2022 int->decimal
                    public decimal MarksObtainedSsc
                    {
                        get { return _marksObtainedSsc; }
                        set { _marksObtainedSsc = value; }
                    }

                    public string SchoolCollegeNameSsc
                    {
                        get { return _schoolCollegeNameSsc; }
                        set { _schoolCollegeNameSsc = value; }
                    }

                    public string YearOfExamSsc
                    {
                        get { return _yearOfExamSsc; }
                        set { _yearOfExamSsc = value; }
                    }
                    public string QEXMROLLNOHSSC
                    {
                        get { return _QEXMROLLNOHSSC; }
                        set { _QEXMROLLNOHSSC = value; }
                    }
                    public string YEAR_OF_EXAMHSSC
                    {
                        get { return _YEAR_OF_EXAMHSSC; }
                        set { _YEAR_OF_EXAMHSSC = value; }
                    }
                    public int QUALIFYNO
                    {
                        get { return _QUALIFYNO; }
                        set { _QUALIFYNO = value; }
                    }

                    public string QEXMROLLNO
                    {
                        get { return _QEXMROLLNO; }
                        set { _QEXMROLLNO = value; }
                    }

                    public int ENG
                    {
                        get { return _ENG; }
                        set { _ENG = value; }
                    }
                    public int ENGMAX
                    {
                        get { return _ENGMAX; }
                        set { _ENGMAX = value; }
                    }
                    public decimal PERCENTILE
                    {
                        get { return _PERCENTILE; }
                        set { _PERCENTILE = value; }
                    }   
                    public string YEAR_OF_EXAM
                    {
                        get { return _YEAR_OF_EXAM; }
                        set { _YEAR_OF_EXAM = value; }
                    }
                    public int STATERANK
                    {
                        get { return _STATERANK; }
                        set { _STATERANK = value; }
                    }
                    public int ALLINDIARANK
                    {
                        get { return _ALLINDIARANK; }
                        set { _ALLINDIARANK = value; }
                    }
                    public decimal PERCENTAGE
                    {
                        get { return _PERCENTAGE; }
                        set { _PERCENTAGE = value; }
                    }
                    public string REGNO
                    {
                        get { return _REGNO; }
                        set { _REGNO = value; }
                    }
                   
                    public string SCHOOL_COLLEGE_NAME
                    {
                        get { return _SCHOOL_COLLEGE_NAME; }
                        set { _SCHOOL_COLLEGE_NAME = value; }
                    }
                    public string BOARD
                    {
                        get { return _BOARD; }
                        set { _BOARD = value; }
                    }
                    public string GRADE
                    {
                        get { return _GRADE; }
                        set { _GRADE = value; }
                    }
                    public string ATTEMPT
                    {
                        get { return _ATTEMPT; }
                        set { _ATTEMPT = value; }
                    }
                    public string RES_TOPIC
                    {
                        get { return _RES_TOPIC; }
                        set { _RES_TOPIC = value; }
                    }
                    public string SUPERVISOR_NAME1
                    {
                        get { return _SUPERVISOR_NAME1; }
                        set { _SUPERVISOR_NAME1 = value; }
                    }
                    public string SUPERVISOR_NAME2
                    {
                        get { return _SUPERVISOR_NAME2; }
                        set { _SUPERVISOR_NAME2 = value; }
                    }
                    public string COLLEGE_CODE
                    {
                        get { return _COLLEGE_CODE; }
                        set { _COLLEGE_CODE = value; }
                    }
                    public int HSCPHYMAX1
                    {
                        get { return HSCPHYMAX; }
                        set { HSCPHYMAX = value; }
                    }
                    public int HSCPHY1
                    {
                        get { return HSCPHY; }
                        set { HSCPHY = value; }
                    }

                    public int HSCCHEMAX1
                    {
                        get { return HSCCHEMAX; }
                        set { HSCCHEMAX = value; }
                    }
                    public int HSCCHE
                    {
                        get { return _HSCCHE; }
                        set { _HSCCHE = value; }
                    }
                    public int MATHSMAX
                    {
                        get { return _MATHSMAX; }
                        set { _MATHSMAX = value; }
                    }
                    public int MATHS
                    {
                        get { return _MATHS; }
                        set { _MATHS = value; }
                    }
                    public int HSCENGMAX
                    {
                        get { return _HSCENGMAX; }
                        set { _HSCENGMAX = value; }
                    }
                    

                    public int HSCBIOMAX
                    {
                        get { return _HSCBIOMAX; }
                        set { _HSCBIOMAX = value; }
                    }
                    public int HSCBIO
                    {
                        get { return _HSCBIO; }
                        set { _HSCBIO = value; }
                    }
                    public int HSCPCMMAX
                    {
                        get { return _HSCPCMMAX; }
                        set { _HSCPCMMAX = value; }
                    }

                    public int HSCPCM
                    {
                        get { return _HSCPCM; }
                        set { _HSCPCM = value; }
                    }
                    public decimal HSCPCMPercentage //Rishabh 12/02/2022
                    {
                        get { return _HSCPCMperct; }
                        set { _HSCPCMperct = value; }
                    }
                    public int MAXMARKS
                    {
                        get { return _MAXMARKS; }
                        set { _MAXMARKS = value; }
                    }
                    public int OUTOFMARK
                    {
                        get { return _OUTOFMARK; }
                        set { _OUTOFMARK = value; }
                    }

                    public decimal MARKOBTAINED
                    {
                        get { return _MARKOBTAINED; }
                        set { _MARKOBTAINED = value; }
                    }
                    public int MERITNO
                    {
                        get { return _MERITNO; }
                        set { _MERITNO = value; }
                    }
                    public string QEXMROLLNOSSC
                    {
                        get { return _QEXMROLLNOSSC; }
                        set { _QEXMROLLNOSSC = value; }
                    }
                    public string QexmRollNo
                    {
                        get { return _qexmRollNo; }
                        set { _qexmRollNo = value; }
                    }
                    public string QexmStatus
                    {
                        get { return _qexmStatus; }
                        set { _qexmStatus = value; }
                    }
                    public string QualexmName
                    {
                        get { return _qualexmName; }
                        set { _qualexmName = value; }
                    }
                    public string CollegeCode
                    {
                        get { return _collegeCode; }
                        set { _collegeCode = value; }
                    }
                    public int QualexmId
                    {
                        get { return _qualexmId; }
                        set { _qualexmId = value; }
                    }
                    public int DegreeNo
                    {
                        get { return _degreeno; }
                        set { _degreeno = value; }
                    }

                    public string SSC_medium
                    {
                        get { return _sscmedium; }
                        set { _sscmedium = value; }
                    }
                    public string HSSC_medium
                    {
                        get { return _hsscmedium; }
                        set { _hsscmedium = value; }
                    }
                    public string colg_address_Diploma
                    {
                        get { return _colg_address_Diploma; }
                        set { _colg_address_Diploma = value; }
                    }

                    public string AttemptDiploma
                    {
                        get { return _attemptDiploma; }
                        set { _attemptDiploma = value; }
                    }

                    public string GradeDiploma
                    {
                        get { return _gradeDiploma; }
                        set { _gradeDiploma = value; }
                    }

                    public decimal PercentileDiploma
                    {
                        get { return _percentileDiploma; }
                        set { _percentileDiploma = value; }
                    }

                    public string QEXMROLLNODiploma
                    {
                        get { return _QEXMROLLNODiploma; }
                        set { _QEXMROLLNODiploma = value; }
                    }

                    public decimal PercentageDiploma
                    {
                        get { return _percentageDiploma; }
                        set { _percentageDiploma = value; }
                    }

                    public int OutOfMarksDiploma
                    {
                        get { return _outOfMarksDiploma; }
                        set { _outOfMarksDiploma = value; }
                    }

                    public decimal MarksObtainedDiploma
                    {
                        get { return _marksObtainedDiploma; }
                        set { _marksObtainedDiploma = value; }
                    }

                    public string Diploma_medium
                    {
                        get { return _DiplomaMedium; }
                        set { _DiplomaMedium = value; }
                    }

                    public string YearOfExamDiploma
                    {
                        get { return _yearOfExamDiploma; }
                        set { _yearOfExamDiploma = value; }
                    }

                    public string BoardDiploma
                    {
                        get { return _boardDiploma; }
                        set { _boardDiploma = value; }
                    }

                    public string SchoolCollegeNameDiploma
                    {
                        get { return _schoolCollegeNameDiploma; }
                        set { _schoolCollegeNameDiploma = value; }
                    }

                    #endregion
                }

            }

        }
    }
