using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IQuestionbank
           {
              #region Private Members

               private string _TOPIC;
                                              
              private int _QUESTIONNO;
              private char _OBJECTIVE_DESCRIPTIVE;

              
              private char _TYPE;



              private string _QUEIMGNAME;

              private string _ANS1IMGNAME;

              private string _ANS2IMGNAME;

              private string _ANS3IMGNAME;

              private string _ANS4IMGNAME;

              private string _ANS5IMGNAME;

              

              private string _ANS6IMGNAME;

              

              private int _COURSENO;

              private string _QUESTIONTEXT;


              private byte[] _QUESTIONIMAGE;

              private string _ANS1TEXT;

              private byte[] _ANS1IMAGE;

              private string _ANS2TEXT;

              private byte[] _ANS2IMAGE;

              private string _ANS3TEXT;

              private byte[] _ANS3IMAGE;

              private string _ANS4TEXT;

              private byte[] _ANS4IMAGE;

              private string _ANS5TEXT;

              

              private byte[] _ANS5IMAGE;



              private string _ANS6TEXT;

              

              private byte[] _ANS6IMAGE;

              

              //private int _CORRECTANS;

              private string  _CORRECTANS;
              private int _TEST_NO;


              private string _COLLEGE_CODE;

              //private DateTime _CREATEDDATE;

              private int _UA_NO;

              private int _IDNO;

              

              private string _FTBANS;

            //  private int _SELECTED;

              private string _SELECTED;
                 
              private string _REMARKS;

              private string _AUTHOR;

              private int _MARKS_FOR_QUESTION;

              private string _DESCRIPTIVE_ANSWER=string.Empty;

              private string _TEST_TYPE;



              private int _QUESTION_MARKS;

              

              
              private int _MARKS_OBTAINED;

              

              #endregion

              #region Public Memebers

              public string ANS2IMGNAME
              {
                  get { return _ANS2IMGNAME; }
                  set { _ANS2IMGNAME = value; }
              }

              public string ANS1IMGNAME
              {
                  get { return _ANS1IMGNAME; }
                  set { _ANS1IMGNAME = value; }
              }
              public string QUEIMGNAME
              {
                  get { return _QUEIMGNAME; }
                  set { _QUEIMGNAME = value; }
              }
              public string ANS3IMGNAME
              {
                  get { return _ANS3IMGNAME; }
                  set { _ANS3IMGNAME = value; }
              }
              public char TYPE
              {
                  get { return _TYPE; }
                  set { _TYPE = value; }
              }
              public string ANS4IMGNAME
              {
                  get { return _ANS4IMGNAME; }
                  set { _ANS4IMGNAME = value; }
              }

              public string ANS5IMGNAME
              {
                  get { return _ANS5IMGNAME; }
                  set { _ANS5IMGNAME = value; }
              }

              public string ANS6IMGNAME
              {
                  get { return _ANS6IMGNAME; }
                  set { _ANS6IMGNAME = value; }
              }


              public char OBJECTIVE_DESCRIPTIVE
              {
                  get { return _OBJECTIVE_DESCRIPTIVE; }
                  set { _OBJECTIVE_DESCRIPTIVE = value; }
              }


              public string TOPIC
              {
                  get { return _TOPIC; }
                  set { _TOPIC = value; }
              }



              public int QUESTIONNO

                {
                    get 
                    { 
                        return _QUESTIONNO;
                    }

                    set 
                    {
                        _QUESTIONNO = value;
                    }
                }
              public int TEST_NO
              {
                  get { return _TEST_NO; }
                  set { _TEST_NO = value; }
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

                public string QUESTIONTEXT
                {
                    get 
                    { 
                        return _QUESTIONTEXT; 
                    }
                    set 
                    { 
                        _QUESTIONTEXT = value;
                    }
                }

                public byte[] QUESTIONIMAGE
                {
                    get 
                    { 
                        return _QUESTIONIMAGE;
                    }
                    set 
                    {
                        _QUESTIONIMAGE = value; 
                    }

                }

                public string ANS1TEXT
                {
                    get 
                    {
                        return _ANS1TEXT;
                    }
                    set 
                    {
                        _ANS1TEXT = value;
                    }
                }

                public byte[] ANS1IMAGE
                {
                    get 
                    { 
                        return _ANS1IMAGE; 
                    }
                    set 
                    { 
                        _ANS1IMAGE = value;
                    }
                }

                public string ANS2TEXT
                {
                    get 
                    { 
                        return _ANS2TEXT; 
                    }
                    set 
                    { 
                        _ANS2TEXT = value; 
                    }
                }

                public byte[] ANS2IMAGE
                {
                    get 
                    { 
                        return _ANS2IMAGE;
                    }
                    set 
                    { 
                        _ANS2IMAGE = value; 
                    }
                }

                public string ANS3TEXT
                {
                    get 
                    { 
                        return _ANS3TEXT;
                    }
                    set 
                    { 
                        _ANS3TEXT = value; 
                    }
                }

                public byte[] ANS3IMAGE
                {
                    get 
                    {
                        return _ANS3IMAGE;
                    }
                    set 
                    { 
                        _ANS3IMAGE = value; 
                    }
                }

                public string ANS4TEXT
                {
                    get 
                    { 
                        return _ANS4TEXT;
                    }
                    set 
                    { 
                        _ANS4TEXT = value;
                    }
                }

               
                public byte[] ANS4IMAGE
                {
                    get 
                    {
                        return _ANS4IMAGE; 
                    }
                    set 
                    {
                        _ANS4IMAGE = value;
                    }
                }

                public string ANS5TEXT
                {
                    get { return _ANS5TEXT; }
                    set { _ANS5TEXT = value; }
                }


                public byte[] ANS5IMAGE
                {
                    get { return _ANS5IMAGE; }
                    set { _ANS5IMAGE = value; }
                }


                public string ANS6TEXT
                {
                    get { return _ANS6TEXT; }
                    set { _ANS6TEXT = value; }
                }

                public byte[] ANS6IMAGE
                {
                    get { return _ANS6IMAGE; }
                    set { _ANS6IMAGE = value; }
                }


                public string  CORRECTANS
                {
                    get
                    {
                        return _CORRECTANS;
                    }
                    set
                    {
                        _CORRECTANS = value;
                    }
                }

                //public int CORRECTANS
                //{
                //    get 
                //    {
                //        return _CORRECTANS;
                //    }
                //    set 
                //    { 
                //        _CORRECTANS = value; 
                //    }
                //}

                public string COLLEGE_CODE
                {
                    get 
                    { 
                        return _COLLEGE_CODE; 
                    }
                    set 
                    { 
                        _COLLEGE_CODE = value; 
                    }
                }
                //public DateTime CREATEDDATE
                //{
                //    get 
                //    { 
                //        return _CREATEDDATE;
                //    }
                //    set 
                //    { 
                //        _CREATEDDATE = value; 
                //    }
                //}

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


                public string FTBANS
                {
                    get
                    {
                        return _FTBANS;
                    }
                    set
                    {
                        _FTBANS = value;
                    }
                }

                //public int SELECTED
                //{
                //    get
                //    {
                //        return _SELECTED;
                //    }
                //    set
                //    {
                //        _SELECTED = value;
                //    }
                //}

                public string  SELECTED
                {
                    get
                    {
                        return _SELECTED;
                    }
                    set
                    {
                        _SELECTED = value;
                    }
                }

                public string REMARKS
                {
                    get
                    {
                        return _REMARKS;
                    }
                    set
                    {
                        _REMARKS = value;
                    }
                }

                public string AUTHOR
                {
                    get
                    {
                        return _AUTHOR;
                    }
                    set
                    {
                        _AUTHOR = value;
                    }
                }


                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }


                public int MARKS_FOR_QUESTION
                {
                    get { return _MARKS_FOR_QUESTION; }
                    set { _MARKS_FOR_QUESTION = value; }
                }

                public string DESCRIPTIVE_ANSWER
                {
                    get { return _DESCRIPTIVE_ANSWER; }
                    set { _DESCRIPTIVE_ANSWER = value; }
                }

                public string TEST_TYPE
                {
                    get { return _TEST_TYPE; }
                    set { _TEST_TYPE = value; }
                }

                public int QUESTION_MARKS
                {
                    get { return _QUESTION_MARKS; }
                    set { _QUESTION_MARKS = value; }
                }

                public int MARKS_OBTAINED
                {
                    get { return _MARKS_OBTAINED; }
                    set { _MARKS_OBTAINED = value; }
                }




              #endregion
    }
        }
    }
}
