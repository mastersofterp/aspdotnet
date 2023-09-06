using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
   public  class Video
   {
       #region Private Fields

       private string _TITLE = string.Empty;
       private string _PUBLISHER_NAME = string.Empty;
       private string _FILENAME = string.Empty;
       private int _COURSENO = 0;
       private string _DESCRIPTION = string.Empty;
       private string _OLDFILENAME = string.Empty;
       private string _TYPE = string.Empty;
       private int _UANO = 0;
       private int _SR_NO = 0;
       private int _IDNO = 0;
       private string _BODY = string.Empty;
       private DateTime _DATETIME;
       private int _COLLEGE_CODE;

      
       private DateTime _TIME;

       
      

       #endregion

       public DateTime DATETIME
       {
           get { return _DATETIME; }
           set { _DATETIME = value; }
       }
       public DateTime TIME
       {
           get { return _TIME; }
           set { _TIME = value; }
       }

       public int COLLEGE_CODE
       {
           get { return _COLLEGE_CODE; }
           set { _COLLEGE_CODE = value; }
       }
       

       public string BODY
       {
           get { return _BODY; }
           set { _BODY = value; }
       }

     
       public int IDNO
       {
           get { return _IDNO; }
           set { _IDNO = value; }
       }
       public string TYPE
       {
           get { return _TYPE; }
           set { _TYPE = value; }
       }
       public string OLDFILENAME
       {
           get { return _OLDFILENAME; }
           set { _OLDFILENAME = value; }
       }
       public int SR_NO
       {
           get { return _SR_NO; }
           set { _SR_NO = value; }
       }
       public string TITLE
       {
           get { return _TITLE; }
           set { _TITLE = value; }
       }
       public string FILENAME
       {
           get { return _FILENAME; }
           set { _FILENAME = value; }
       }
       public string PUBLISHER_NAME
       {
           get { return _PUBLISHER_NAME; }
           set { _PUBLISHER_NAME = value; }
       }
       public int COURSENO
       {
           get { return _COURSENO; }
           set { _COURSENO = value; }
       }
       public int UANO
       {
           get { return _UANO; }
           set { _UANO = value; }
       }
       public string DESCRIPTION
       {
           get { return _DESCRIPTION; }
           set { _DESCRIPTION = value; }
       }
       
      
     
   }
}
