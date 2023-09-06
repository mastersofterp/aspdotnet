//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : ACTIVITY CLASS 
// CREATION DATE : 15-JUN-2009                                                        
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI
// MODIFIED DATE : 07-OCT-2009 (NIRAJ D. PHALKE)
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Activity
    {
        #region Private Fields
        int _activityNo = 0;        
        string _activityCode = string.Empty;
        string _activity_name = string.Empty;        
        string _userTypes = string.Empty;
        string _page_links = string.Empty;
        string _prereq_act_no = string.Empty;
        int _exam_no = 0;
        int _subexam_no = 0;
        bool _ActiveStatus = false; //ADDED BY RISHABH ON 30/10/2021
        #endregion
        string _activityTempalte = string.Empty; //added on 06-04-2020 by Vaishali for Notification



        //added on 06-04-2020 by Vaishali for Notification
        public string ActivityTemplate
        {
            get { return _activityTempalte; }
            set { _activityTempalte = value; }
        }


        public bool ActiveStatus //ADDED BY RISHABH ON 30/10/2021
        {
            get
            {
                return _ActiveStatus;
            }
            set
            {
                _ActiveStatus = value;
            }
        }
        #region Public Properties
        public int ActivityNo
        {
            get { return _activityNo; }
            set { _activityNo = value; }
        }
        public string ActivityCode
        {
            get { return _activityCode; }
            set { _activityCode = value; }
        }
        public string ActivityName
        {
            get { return _activity_name; }
            set { _activity_name = value; }
        }
        public string UserTypes
        {
            get { return _userTypes; }
            set { _userTypes = value; }
        }
        public string Page_links
        {
            get { return _page_links; }
            set { _page_links = value; }
        }
        public string Prereq_Act_No
        {
            get { return _prereq_act_no; }
            set { _prereq_act_no = value; }
        }
        public int Exam_No
        {
            get { return _exam_no; }
            set { _exam_no = value; }
        }
        public int SubExam_No
        {
            get { return _subexam_no; }
            set { _subexam_no = value; }
        }
        #endregion 


        
        int Assign_flag = 0; //Added by Injamam 28-2-23

        public int AssignFlag          //Added by Injamam 28-2-23 
        {
            get { return Assign_flag; }
            set { Assign_flag = value; }
        }
    }
}