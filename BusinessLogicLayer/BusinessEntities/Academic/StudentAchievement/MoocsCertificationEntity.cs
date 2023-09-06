using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement
{
   public class MoocsCertificationEntity
   {
       #region Private Members
       private int _uno = 0;
       private int _idno = 0;
       private string _course_name = string.Empty;
       private int _moocs_certification_id = 0;
       private int _moocs_platform_id = 0;
       private string _institute_university = string.Empty;
       private DateTime _sdate = DateTime.Now;
       private DateTime _edate = DateTime.Now;
       private bool _fa_status = false;
       private decimal _amount = 0.0m;
       private string _file_name = string.Empty;
       private string _file_path = string.Empty;
       private int _acadamic_year_id = 0;
       private int _duration_id = 0;
       private string _IPADDRESS;
       private DateTime _Current_Date = DateTime.Now;
    
       #endregion
       public int uno
       {
           get { return _uno; }
           set { _uno = value; }
       }
       public string IPADDRESS
       {
           get { return _IPADDRESS; }
           set { _IPADDRESS = value; }
       }
       public int idno
       {
           get { return _idno; }
           set { _idno = value; }
       }
       public int moocs_certification_id
       {
           get { return _moocs_certification_id; }
           set { _moocs_certification_id = value; }
       }
       public int moocs_platform_id
       {
           get { return _moocs_platform_id; }
           set { _moocs_platform_id = value; }
       }
       public string institute_university
       {
           get { return _institute_university; }
           set { _institute_university = value; }
       }
       public DateTime SDate
       {
           get { return _sdate; }
           set { _sdate = value; }
       }

       public DateTime EDate
       {
           get { return _edate; }
           set { _edate = value; }
       }
       public bool fa_status
       {
           get { return _fa_status; }
           set { _fa_status = value; }
       }
       public string course_name
       {
           get { return _course_name; }
           set { _course_name = value; }
       }
       public string file_name
       {
           get { return _file_name; }
           set { _file_name = value; }
       }
       public string file_path
       {
           get { return _file_path; }
           set { _file_path = value; }
       }
       public decimal amount
       {
           get { return _amount; }
           set { _amount = value; }
       }
       public int duration_id
       {
           get { return _duration_id; }
           set { _duration_id = value; }
       }
       public int acadamic_year_id
       {
           get { return _acadamic_year_id; }
           set { _acadamic_year_id = value; }
       }
       public int OrganizationId
       {
           get;
           set;
       }
       public DateTime Current_Date
       {
           get { return _Current_Date; }
           set { _Current_Date = value; }
       }

   }
}
