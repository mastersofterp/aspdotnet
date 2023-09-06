using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections;

using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Masters
            {
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                
                public string[,] captions = new string[4, 9];
               
                public string _tablename = string.Empty;

                public Masters()
                {
                    GetCaptions(captions);
                }

                public Masters(string tablename)
                {
                    _tablename = tablename;
                    GetCaptions(captions);
                }

                private void GetCaptions(string[,] captions)
                {
                    switch (_tablename.ToLower())
                    {
                        #region PAYROLL

                        //PAYROLL
                        case "payroll_staff":    //payroll
                            captions[0, 0] = "Staff";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "Staff";                           //FieldName
                            captions[0, 3] = "StaffNo";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "StaffNo,Staff,retirement_age,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Staff";                   //Report Title

                            captions[1, 0] = "Retirement Age";                  //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "retirement_age";                  //FieldName
                            captions[1, 3] = "StaffNo";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "StaffNo,Staff,retirement_age,College_code";      //FieldNames
                            captions[1, 6] = "number";                          //Validation Type
                            captions[1, 7] = "2";                               //Max length
                            captions[1, 8] = "List of Staff";                   //Report Title
                            break;

                        case "payroll_employeetype":    //payroll
                            captions[0, 0] = "Employee Type";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "employeetype";                           //FieldName
                            captions[0, 3] = "emptypeno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "emptypeno,employeetype,retirement_age,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Staff";                   //Report Title

                            captions[1, 0] = "Retirement Age";                  //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "retirement_age";                  //FieldName
                            captions[1, 3] = "emptypeno";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "emptypeno,employeetype,retirement_age,College_code";      //FieldNames
                            captions[1, 6] = "number";                          //Validation Type
                            captions[1, 7] = "2";                               //Max length
                            captions[1, 8] = "List of Employee Type";                   //Report Title
                            break;

                        case "payroll_caste":       //payroll
                            captions[0, 0] = "Caste";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "caste";                           //FieldName
                            captions[0, 3] = "casteno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "casteno,caste,college_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Caste";                   //Report Title
                            break;

                        case "payroll_form12bamaster":       //payroll
                            captions[0, 0] = "Nature of perquisites";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "PERQUISITES";                           //FieldName
                            captions[0, 3] = "FORM12BAID";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "FORM12BAID,PERQUISITES,college_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Perquisites";                   //Report Title
                            break;

                      case "payroll_employee_classification":               //payroll
                            captions[0, 0] = "Employee Classification";            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "clname";                          //FieldName
                            captions[0, 3] = "clno";                            //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "clno,clname,college_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Employee Classification";                   //Report Title
                            break; 


                        case "payroll_nationality":  //payroll
                            captions[0, 0] = "Nationality";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "nationalitynm";                     //FieldName
                            captions[0, 3] = "nationalityno";                   //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "nationalityno,nationalitynm,college_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Nationality";             //Report Title
                            break;
                        case "payroll_loantype":  //payroll
                            captions[0, 0] = "Loan Type";                       //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "loanname";                        //FieldName
                            captions[0, 3] = "loanno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "loanno,loanname,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Loan Type";             //Report Title
                            break;
                        case "payroll_leave":  //payroll
                            captions[0, 0] = "Leave Type";                       //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "leavename";                        //FieldName
                            captions[0, 3] = "lno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "lno,leavename,college_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Leave Type";             //Report Title
                            break;
                        case "payroll_photo_type":  //payroll
                            captions[0, 0] = "Image Name";                          //Label
                            captions[0, 1] = "textbox";                             //Control
                            captions[0, 2] = "photo_type";                          //FieldName
                            captions[0, 3] = "photo_typeid";                        //ID
                            captions[0, 4] = "1";                                   //No of Fields
                            captions[0, 5] = "photo_typeid,photo_type,College_code";//FieldNames
                            captions[0, 6] = "string2";                              //Validation Type
                            captions[0, 7] = "100";                                 //Max length
                            captions[0, 8] = "List of Image Name";                  //Report Title
                            break; 
                        case "payroll_nominitype":  //payroll  
                            captions[0, 0] = "Nominee Type";                      //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "nominitype";                      //FieldName
                            captions[0, 3] = "ntno";                            //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "ntno,nominitype,College_code";    //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Nominee Type";              //Report Title
                            break;  
                        case "payroll_category":   //payroll
                            captions[0, 0] = "Category";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "Category";                        //FieldName
                            captions[0, 3] = "CategoryNo";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "CategoryNo,Category,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Category";                //Report Title
                            break;
                        case "payroll_religion":   //payroll
                            captions[0, 0] = "Religion";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "Religion";                        //FieldName
                            captions[0, 3] = "Religionno";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "ReligionNo,Religion,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Religion";                //Report Title
                            break;
                        case "payroll_supplimentary_head":   //payroll
                            captions[0, 0] = "Supplementary Head";               //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "suplhead";                        //FieldName
                            captions[0, 3] = "suplhno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "suplhno,suplhead,College_code";    //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Supplementary Head";       //Report Title
                            break;
                        case "payroll_city":       //payroll
                            captions[0, 0] = "City";                        //Label
                            captions[0, 1] = "textbox";                     //Control
                            captions[0, 2] = "city";                        //FieldName
                            captions[0, 3] = "cityno";                      //ID
                            captions[0, 4] = "1";                           //No of Fields
                            captions[0, 5] = "cityno,city,College_code";    //FieldNames
                            captions[0, 6] = "string";                      //Validation Type
                            captions[0, 7] = "25";                          //Max length
                            captions[0, 8] = "List of City";                //Report Title
                            break;
                        case "payroll_title":     //payroll
                            captions[0, 0] = "Title";                        //Label
                            captions[0, 1] = "textbox";                      //Control
                            captions[0, 2] = "title";                        //FieldName
                            captions[0, 3] = "titleno";                      //ID
                            captions[0, 4] = "1";                            //No of Fields
                            captions[0, 5] = "titleno,title,College_code";   //FieldNames
                            captions[0, 6] = "string2";                       //Validation Type
                            captions[0, 7] = "50";                           //Max length
                            captions[0, 8] = "List of Title";                //Report Title
                            break;
                        case "payroll_stafftype":   //payroll
                            captions[0, 0] = "Staff Type";                      //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "stafftype";                       //FieldName
                            captions[0, 3] = "stno";                            //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "stno,stafftype,College_code";     //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Staff Type";              //Report Title
                            break;
                        case "payroll_rule":       //payroll
                            captions[0, 0] = "Pay Rule";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "payrule";                         //FieldName
                            captions[0, 3] = "ruleno";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "ruleno,payrule,rulename,College_code";     //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "30";                              //Max length
                            captions[0, 8] = "List of Pay Rule";                //Report Title

                            captions[1, 0] = "Rule Name";                       //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "rulename";                        //FieldName
                            captions[1, 3] = "ruleno";                          //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "ruleno,payrule,rulename,College_code";     //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "30";                              //Max length
                            captions[1, 8] = "List of Pay Rule";                //Report Title
                            break;

                        case "payroll_typetran":      //payroll
                            captions[0, 0] = "Transaction Type";                //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "typetran";                        //FieldName
                            captions[0, 3] = "typetranno";                      //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "typetranno,typetran,shortname,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Transaction Type";        //Report Title

                            captions[1, 0] = "Short Name";                      //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "ShortName";                       //FieldName
                            captions[1, 3] = "typetranno";                      //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "typetranno,typetran,shortname,College_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "25";                              //Max length
                            captions[1, 8] = "List of Transaction Type";        //Report Title
                            break;

                        case "payroll_subdept":       //payroll
                            captions[0, 0] = "Sub Department";                 //Label
                            captions[0, 1] = "textbox";                        //Control
                            captions[0, 2] = "subdept";                        //FieldName
                            captions[0, 3] = "subdeptno";                      //ID
                            captions[0, 4] = "2";                              //No of Fields
                            captions[0, 5] = "subdeptno,subdept,subsdept,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                             //Max length
                            captions[0, 8] = "List of Department";             //Report Title

                            captions[1, 0] = "Short Name";                     //Label
                            captions[1, 1] = "textbox";                        //Control
                            captions[1, 2] = "subsdept";                       //FieldName
                            captions[1, 3] = "subdeptno";                      //ID
                            captions[1, 4] = "2";                              //No of Fields
                            captions[1, 5] = "subdeptno,subdept,subsdept,College_code";//FieldNames
                            captions[1, 6] = "string2";                         //Validation Type
                            captions[1, 7] = "50";                             //Max length 
                            captions[1, 8] = "List of Department";             //Report Title
                            break;

                        case "payroll_pgcoursedepartment":       //payroll
                            captions[0, 0] = "PG Course Department";                 //Label
                            captions[0, 1] = "textbox";                        //Control
                            captions[0, 2] = "pgsubdept";                        //FieldName
                            captions[0, 3] = "pgsubdeptno";                      //ID
                            captions[0, 4] = "2";                              //No of Fields
                            captions[0, 5] = "pgsubdeptno,pgsubdept,pgsubsdept,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                             //Max length
                            captions[0, 8] = "List of Department";             //Report Title

                            captions[1, 0] = "Short Name";                     //Label
                            captions[1, 1] = "textbox";                        //Control
                            captions[1, 2] = "pgsubsdept";                       //FieldName
                            captions[1, 3] = "pgsubdeptno";                      //ID
                            captions[1, 4] = "2";                              //No of Fields
                            captions[1, 5] = "pgsubdeptno,pgsubdept,pgsubsdept,College_code";//FieldNames
                            captions[1, 6] = "string2";                         //Validation Type
                            captions[1, 7] = "50";                             //Max length 
                            captions[1, 8] = "List of Department";             //Report Title
                            break;

                        case "payroll_subdesig":    //payroll
                            captions[0, 0] = "Sub Designation";                 //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "subdesig";                        //FieldName
                            captions[0, 3] = "subdesigno";                      //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "subdesigno,subdesig,subsdesig,seqno,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Designation";             //Report Title
                                                                                    
                            captions[1, 0] = "Short Name";                      //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "subsdesig";                       //FieldName
                            captions[1, 3] = "subdesigno";                      //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "subdesigno,subdesig,subsdesig,seqno,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Designation";             //Report Title
                            
                            captions[2, 0] = "Sequence Number";                      //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "seqno";                       //FieldName
                            captions[2, 3] = "subdesigno";                      //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "subdesigno,subdesig,subsdesig,seqno,College_code";//FieldNames
                            captions[2, 6] = "number";                          //Validation Type
                            captions[2, 7] = "50";                              //Max length
                            captions[2, 8] = "List of Designation";             //Report Title

                            break;



                        case "payroll_bank":      //payroll
                            captions[0, 0] = "Bank Code";                       //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "bankcode";                        //FieldName
                            captions[0, 3] = "bankno";                          //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "bankno,bankcode,bankname,bankaddr,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Bank";                    //Report Title

                            captions[1, 0] = "Bank Name";                       //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "bankname";                        //FieldName
                            captions[1, 3] = "bankno";                          //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "bankno,bankcode,bankname,bankaddr,College_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Bank";                    //Report Title

                            captions[2, 0] = "Bank Address";                    //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "bankaddr";                        //FieldName
                            captions[2, 3] = "bankno";                          //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "bankno,bankcode,bankname,bankaddr,College_code";//FieldNames
                            captions[2, 6] = "string";                          //Validation Type
                            captions[2, 7] = "50";                              //Max length
                            captions[2, 8] = "List of Bank";                    //Report Title

                            //captions[3, 0] = "Account No.";                     //Label
                            //captions[3, 1] = "textbox";                         //Control
                            //captions[3, 2] = "bankaccno";                       //FieldName
                            //captions[3, 3] = "bankno";                          //ID
                            //captions[3, 4] = "4";                               //No of Fields
                            //captions[3, 5] = "bankno,bankcode,bankname,bankaddr,bankaccno,College_code";//FieldNames
                            //captions[3, 6] = "";                          //Validation Type
                            //captions[3, 7] = "10";                              //Max length
                            break;

                        case "payroll_pf_loan_type":      //payroll
                            captions[0, 0] = "Name";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "name";                            //FieldName
                            captions[0, 3] = "pfloantypeno";                          //ID
                            captions[0, 4] = "4";                               //No of Fields
                            captions[0, 5] = "pfloantypeno,name,shortname,deducted,amt,app_for,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of PF Loan";                 //Report Title

                            captions[1, 0] = "Short Name";                      //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "shortname";                       //FieldName
                            captions[1, 3] = "pfloantypeno";                          //ID
                            captions[1, 4] = "4";                               //No of Fields
                            captions[1, 5] = "pfloantypeno,name,shortname,deducted,amt,app_for,College_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "25";                              //Max length
                            captions[1, 8] = "List of GPF/CPF";                 //Report Title

                            captions[2, 0] = "Deducted";                        //Label
                            captions[2, 1] = "checkbox";                        //Control
                            captions[2, 2] = "deducted";                        //FieldName
                            captions[2, 3] = "pfloantypeno";                          //ID
                            captions[2, 4] = "4";                               //No of Fields
                            captions[2, 5] = "pfloantypeno,name,shortname,deducted,amt,app_for,College_code";//FieldNames
                            captions[2, 6] = "number";                          //Validation Type
                            captions[2, 7] = "";                                //Max length
                            captions[2, 8] = "List of GPF/CPF";                 //Report Title

                            captions[3, 0] = "Amount";                          //Label
                            captions[3, 1] = "textbox";                         //Control
                            captions[3, 2] = "amt";                             //FieldName
                            captions[3, 3] = "pfloantypeno";                          //ID
                            captions[3, 4] = "4";                               //No of Fields
                            captions[3, 5] = "pfloantypeno,name,shortname,deducted,amt,app_for,College_code";//FieldNames
                            captions[3, 6] = "double";                          //Validation Type
                            captions[3, 7] = "10";                              //Max length
                            captions[3, 8] = "List of GPF/CPF";                 //Report Title
                            break;

                        case "payroll_headacc":  //payroll
                            captions[0, 0] = "Account Head";                    //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "headac";                          //FieldName
                            captions[0, 3] = "ano";                             //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "ano,headac,accno,staff,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Account Head";            //Report Title

                            captions[1, 0] = "Account No";                      //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "accno";                           //FieldName
                            captions[1, 3] = "ano";                             //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "ano,headac,accno,staff,College_code";//FieldNames
                            captions[1, 6] = "numeric";                         //Validation Type
                            captions[1, 7] = "20";                              //Max length
                            captions[1, 8] = "List of Account Head";            //Report Title

                            captions[2, 0] = "Staff";                           //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "staff";                           //FieldName
                            captions[2, 3] = "ano";                             //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "ano,headac,accno,staff,College_code";//FieldNames
                            captions[2, 6] = "string";                          //Validation Type
                            captions[2, 7] = "100";                             //Max length
                            captions[2, 8] = "List of Account Head";            //Report Title
                            break;

                        case "payroll_designature":  //payroll
                            captions[0, 0] = "Designature";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "designature";                     //FieldName
                            captions[0, 3] = "designatureno";                   //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "designatureno,designature,status,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Designature";             //Report Title

                            captions[1, 0] = "Status";                          //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "status";                          //FieldName
                            captions[1, 3] = "designatureno";                   //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "designatureno,designature,status,College_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "1";                               //Max length
                            captions[1, 8] = "List of Designature";             //Report Title
                            break;

                        case "payroll_quarter_type":  //payroll
                            captions[0, 0] = "Quarter";                         //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "qrttype";                         //FieldName
                            captions[0, 3] = "qrttypeno";                       //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "qrttypeno,qrttype,qarea,qrent,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "15";                              //Max length
                            captions[0, 8] = "List of Quarter Type";            //Report Title

                            captions[1, 0] = "Area";                            //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "qarea";                           //FieldName
                            captions[1, 3] = "qrttypeno";                       //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "qrttypeno,qrttype,qarea,qrent,College_code";//FieldNames
                            captions[1, 6] = "string2";                                //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Quarter Type";            //Report Title

                            captions[2, 0] = "Rent";                            //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "qrent";                           //FieldName
                            captions[2, 3] = "qrttypeno";                       //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "qrttypeno,qrttype,qarea,qrent,College_code";//FieldNames
                            captions[2, 6] = "double";                          //Validation Type
                            captions[2, 7] = "0";                               //Max length
                            captions[2, 8] = "List of Quarter Type";            //Report Title

                            break;

                        case "payroll_quartermas":  //payroll
                            captions[0, 0] = "Quarter";                         //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "qrtname";                         //FieldName
                            captions[0, 3] = "qrtno";                           //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "qrtno,qrtname,qrttypeno,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Quarter";                 //Report Title

                            captions[1, 0] = "Quarter Type";                        //Label
                            captions[1, 1] = "dropdown";                            //Control
                            captions[1, 2] = "qrttypeno";                           //FieldName
                            captions[1, 3] = "qrtno";                               //ID
                            captions[1, 4] = "2";                                   //No of Fields
                            captions[1, 5] = "qrtno,qrtname,qrttypeno,College_code";//FieldNames
                            captions[1, 6] = "number";                              //Validation Type
                            captions[1, 7] = "1";                                   //Max length
                            captions[1, 8] = "List of Quarter";                     //Report Title
                            break;

                        case "payroll_qualilevel":  //payroll
                            captions[0, 0] = "Qualification Level";                     //Label
                            captions[0, 1] = "textbox";                                 //Control
                            captions[0, 2] = "qualilevelname";                          //FieldName
                            captions[0, 3] = "qualilevelno";                            //ID
                            captions[0, 4] = "1";                                       //No of Fields
                            captions[0, 5] = "qualilevelno,qualilevelname,College_code";//FieldNames
                            captions[0, 6] = "string2";                                 //Validation Type
                            captions[0, 7] = "50";                                      //Max length
                            captions[0, 8] = "List of Qualification Level";             //Report Title
                            break;

                        #endregion

                        #region pension

                        //pension
                        case "pension_staff":    //pension
                            captions[0, 0] = "Staff";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "Staff";                           //FieldName
                            captions[0, 3] = "StaffNo";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "StaffNo,Staff,retirement_age,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Staff";                   //Report Title

                            captions[1, 0] = "Retirement Age";                  //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "retirement_age";                  //FieldName
                            captions[1, 3] = "StaffNo";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "StaffNo,Staff,retirement_age,College_code";      //FieldNames
                            captions[1, 6] = "number";                          //Validation Type
                            captions[1, 7] = "2";                               //Max length
                            captions[1, 8] = "List of Staff";                   //Report Title
                            break;



                        case "pension_designature":  //pension
                            captions[0, 0] = "Designature";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "designature";                     //FieldName
                            captions[0, 3] = "designatureno";                   //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "designatureno,designature,status,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Designature";             //Report Title

                            captions[1, 0] = "Status";                          //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "status";                          //FieldName
                            captions[1, 3] = "designatureno";                   //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "designatureno,designature,status,College_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "1";                               //Max length
                            captions[1, 8] = "List of Designature";             //Report Title
                            break;

                        case "pension_subdesig":    //pension
                            captions[0, 0] = "Sub Designation";                 //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "subdesig";                        //FieldName
                            captions[0, 3] = "subdesigno";                      //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "subdesigno,subdesig,subsdesig,seqno,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Designation";             //Report Title

                            captions[1, 0] = "Short Name";                      //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "subsdesig";                       //FieldName
                            captions[1, 3] = "subdesigno";                      //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "subdesigno,subdesig,subsdesig,seqno,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Designation";             //Report Title

                            captions[2, 0] = "Sequence Number";                      //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "seqno";                       //FieldName
                            captions[2, 3] = "subdesigno";                      //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "subdesigno,subdesig,subsdesig,seqno,College_code";//FieldNames
                            captions[2, 6] = "number";                          //Validation Type
                            captions[2, 7] = "50";                              //Max length
                            captions[2, 8] = "List of Designation";             //Report Title
                            break;


                        case "pension_rule":       //pension
                            captions[0, 0] = "Pay Rule";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "payrule";                         //FieldName
                            captions[0, 3] = "ruleno";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "ruleno,payrule,rulename,College_code";     //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "30";                              //Max length
                            captions[0, 8] = "List of Pay Rule";                //Report Title

                            captions[1, 0] = "Rule Name";                       //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "rulename";                        //FieldName
                            captions[1, 3] = "ruleno";                          //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "ruleno,payrule,rulename,College_code";     //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "30";                              //Max length
                            captions[1, 8] = "List of Pay Rule";                //Report Title
                            break;


                        case "pension_stafftype":   //pension
                            captions[0, 0] = "Staff Type";                      //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "stafftype";                       //FieldName
                            captions[0, 3] = "stno";                            //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "stno,stafftype,College_code";     //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Staff Type";              //Report Title
                            break;


                        case "pension_supplimentary_head":   //pension
                            captions[0, 0] = "Supplementary Head";               //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "suplhead";                        //FieldName
                            captions[0, 3] = "suplhno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "suplhno,suplhead,College_code";    //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Supplementary Head";       //Report Title
                            break;


                        case "pension_subdept":       //pension
                            captions[0, 0] = "Sub Department";                 //Label
                            captions[0, 1] = "textbox";                        //Control
                            captions[0, 2] = "subdept";                        //FieldName
                            captions[0, 3] = "subdeptno";                      //ID
                            captions[0, 4] = "2";                              //No of Fields
                            captions[0, 5] = "subdeptno,subdept,subsdept,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                             //Max length
                            captions[0, 8] = "List of Department";             //Report Title

                            captions[1, 0] = "Short Name";                     //Label
                            captions[1, 1] = "textbox";                        //Control
                            captions[1, 2] = "subsdept";                       //FieldName
                            captions[1, 3] = "subdeptno";                      //ID
                            captions[1, 4] = "2";                              //No of Fields
                            captions[1, 5] = "subdeptno,subdept,subsdept,College_code";//FieldNames
                            captions[1, 6] = "string2";                         //Validation Type
                            captions[1, 7] = "50";                             //Max length 
                            captions[1, 8] = "List of Department";             //Report Title
                            break;

                        #endregion

                        #region GPF
                        case "payroll_pf_mast":  //gpf
                            captions[0, 0] = "Short Name";                              //Label
                            captions[0, 1] = "textbox";                                 //Control
                            captions[0, 2] = "shortname";                               //FieldName
                            captions[0, 3] = "pfno";                                   //ID
                            captions[0, 4] = "2";                                       //No of Fields
                            captions[0, 5] = "pfno,shortname,fullname,College_code";   //FieldNames
                            captions[0, 6] = "string2";                                 //Validation Type
                            captions[0, 7] = "20";                                      //Max length
                            captions[0, 8] = "List of PF";                              //Report Title

                            captions[1, 0] = "Full Name";                               //Label
                            captions[1, 1] = "textbox";                                 //Control
                            captions[1, 2] = "fullname";                                //FieldName
                            captions[1, 3] = "pfno";                                   //ID
                            captions[1, 4] = "2";                                       //No of Fields
                            captions[1, 5] = "pfno,shortname,fullname,College_code";   //FieldNames
                            captions[1, 6] = "string2";                                 //Validation Type
                            captions[1, 7] = "50";                                      //Max length
                            captions[1, 8] = "List of PF";                              //Report Title
                            break;
                        #endregion

                        #region ACADEMIC
                        case "acd_course_level":  //academic
                            captions[0, 0] = "Level Name";                      //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "level_desc";                      //FieldName
                            captions[0, 3] = "levelno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "levelno,level_desc,college_code"; //FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Course Level";            //Report Title
                            break;


                       

                        case "acd_state":  //academic
                            captions[0, 0] = "State";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "statename";                        //FieldName
                            captions[0, 3] = "stateno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "stateno,statename,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of State";                    //Report Title
                            break;

                        case "acd_course_category":  //academic
                            captions[0, 0] = "Course Category";                           //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "categoryname";                       //FieldName
                            captions[0, 3] = "categoryno";                         //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "categoryno,categoryname,shortname,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "400";                              //Max length
                            captions[0, 8] = "List of Course Category";                   //Report Title

                            captions[1, 0] = "Code";                             //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "shortname";                             //FieldName
                            captions[1, 3] = "categoryno";                         //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "categoryno,categoryname,shortname,College_code";   //FieldNames
                            captions[1, 6] = "string";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Course Category";                   //Report Title
                            break;

                        case "acd_idtype":  //academic
                            captions[0, 0] = "Id Type";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "idtypedescription";                        //FieldName
                            captions[0, 3] = "idtypeno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "idtypeno,idtypedescription,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of Id Type";                    //Report Title
                            break;

                        //case "acd_idtype":  //academic

                        //    captions[1, 0] = "Id Type";                            //Label
                        //    captions[1, 1] = "textbox";                          //Control
                        //    captions[1, 2] = "idtypedescription";                        //FieldName
                        //    captions[1, 3] = "idtypeno";                          //ID
                        //    captions[1, 4] = "2";                                //No of Fields
                        //    captions[1, 5] = "idtypeno,idtypecode,idtypedescription,College_code";   //FieldNames
                        //    captions[1, 6] = "string2";                           //Validation Type
                        //    captions[1, 7] = "35";                               //Max length
                        //    captions[1, 8] = "List of Id Type";                    //Report Title

                        //    captions[0, 0] = "Id Code";                            //Label
                        //    captions[0, 1] = "textbox";                          //Control
                        //    captions[0, 2] = "idtypecode";                        //FieldName
                        //    captions[0, 3] = "idtypeno";                          //ID
                        //    captions[0, 4] = "2";                                //No of Fields
                        //    captions[0, 5] = "idtypeno,idtypecode,idtypedescription,College_code";   //FieldNames
                        //    captions[0, 6] = "string2";                           //Validation Type
                        //    captions[0, 7] = "2";                               //Max length
                        //    captions[0, 8] = "List of Id Type";                    //Report Title

                        //    break;

                        case "acd_batch":                                         //academic
                            captions[0, 0] = "Batch";                              //Label
                            captions[0, 1] = "textbox";                              //control
                            captions[0, 2] = "batchname";                             //fieldName
                            captions[0, 3] = "batchno";                                //ID
                            captions[0, 4] = "1";                                      //No of Fields
                            captions[0, 5] = "batchno,batchname,college_code";   //FieldNames
                            captions[0, 6] = "string2";                                      //Validation Type
                            captions[0, 7] = "20";                                      //Max Length
                            captions[0, 8] = "List Of Batch";                         //Report Title
                            break;

                        case "acd_subexam_name":  //academic
                            captions[0, 0] = "Sub Exam Name";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "subexamname";                        //FieldName
                            captions[0, 3] = "subexamno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "subexamno,subexamname,fldname,formula,examno,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of Sub Exam Name";                    //Report Title
                            break;

                        //caption[0,7] Modified By Dileep K
                        case "acd_department":  //academic
                            captions[0, 0] = "Department Name";                 //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "deptname";                        //FieldName
                            captions[0, 3] = "deptno";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "deptno,deptname,deptcode,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "64";                              //Max length
                            captions[0, 8] = "List of Department";              //Report Title

                            captions[1, 0] = "Department Code";                 //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "deptcode";                        //FieldName
                            captions[1, 3] = "deptno";                          //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "deptno,deptname,deptcode,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "5";                               //Max length
                            captions[1, 8] = "List of Department";              //Report Title
                            break;

                        case "acd_bank":  //academic
                            captions[0, 0] = "Bank Name";                       //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "bankname";                        //FieldName
                            captions[0, 3] = "bankno";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "bankno,bankname,bankcode,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Bank";                    //Report Title

                            captions[1, 0] = "Bank Code";                       //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "bankcode";                        //FieldName
                            captions[1, 3] = "bankno";                          //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "bankno,bankname,bankcode,College_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Bank";                    //Report Title

                            //captions[2, 0] = "Bank Address";                    //Label
                            //captions[2, 1] = "textbox";                         //Control
                            //captions[2, 2] = "bankaddr";                        //FieldName
                            //captions[2, 3] = "bankno";                          //ID
                            //captions[2, 4] = "3";                               //No of Fields
                            //captions[2, 5] = "bankno,bankname,bankcode,bankaddr,College_code";//FieldNames
                            //captions[2, 6] = "string";                          //Validation Type
                            //captions[2, 7] = "50";                              //Max length
                            //captions[2, 8] = "List of Bank";                    //Report Title
                            break;

                        //case "acd_level":  //academic
                        //    captions[0, 0] = "Level Name";                      //Label
                        //    captions[0, 1] = "textbox";                         //Control
                        //    captions[0, 2] = "levelname";                       //FieldName
                        //    captions[0, 3] = "levelno";                         //ID
                        //    captions[0, 4] = "1";                               //No of Fields
                        //    captions[0, 5] = "levelno,levelname,College_code";  //FieldNames
                        //    captions[0, 6] = "string";                          //Validation Type
                        //    captions[0, 7] = "35";                              //Max length
                        //    captions[0, 8] = "List of Level Name";              //Report Title
                        //    break;

                        case "acd_city":  //academic
                            captions[0, 0] = "City";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "city";                            //FieldName
                            captions[0, 3] = "cityno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "cityno,city,College_code";        //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of City";                    //Report Title
                            break;

                        case "acd_specialisation":  //academic
                            captions[0, 0] = "Specialization";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "specialisationname";                            //FieldName
                            captions[0, 3] = "specialisationno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "specialisationno,specialisationname,College_code";        //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of Specialization";                    //Report Title
                            break;

                        case "acd_admbatch":  //academic
                            captions[0, 0] = "Admission Batch";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "batchname";                            //FieldName
                            captions[0, 3] = "batchno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "batchno,batchname,College_code";        //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "30";                              //Max length
                            captions[0, 8] = "List of Admission Batch";                    //Report Title
                            break;

                        case "acd_physical_handicapped":  //academic
                            captions[0, 0] = "Handicap Name";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "handicap_name";                        //FieldName
                            captions[0, 3] = "handicap_no";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "handicap_no,handicap_name,college_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "200";                               //Max length
                            captions[0, 8] = "List of Physical Handicap";                     //Report Title
                            break;

                        case "acd_district":  //academic
                            captions[0, 0] = "District";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "districtname";                    //FieldName
                            captions[0, 3] = "districtno";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "districtno,districtname,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of District";                //Report Title
                            break;

                        case "acd_bloodgrp":  //academic
                            captions[0, 0] = "Blood group";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "bloodgrpname";                    //FieldName
                            captions[0, 3] = "bloodgrpno";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "bloodgrpno,bloodgrpname,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Blood Group";             //Report Title
                            break;

                        case "acd_category":  //academic
                            captions[0, 0] = "Category";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "category";                        //FieldName
                            captions[0, 3] = "categoryno";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "categoryno,category,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Category";                //Report Title
                            break;

                        case "acd_religion":  //academic
                            captions[0, 0] = "Religion";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "religion";                        //FieldName
                            captions[0, 3] = "religionno";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "religionno,religion,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Religion";                //Report Title
                            break;

                        case "acd_nationality":  //academic
                            captions[0, 0] = "Nationality";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "nationality";                     //FieldName
                            captions[0, 3] = "nationalityno";                   //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "nationalityno,nationality,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Nationality";             //Report Title
                            break;

                        case "acd_mtongue":  //academic
                            captions[0, 0] = "Mother Tongue";                   //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "mtongue";                         //FieldName
                            captions[0, 3] = "mtongueno";                       //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "mtongueno,mtongue,College_code";  //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "20";                              //Max length
                            captions[0, 8] = "List of Mother Tongue";           //Report Title
                            break;

                        case "acd_occupation":  //academic
                            captions[0, 0] = "Occupation";                      //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "occname";                         //FieldName
                            captions[0, 3] = "occupation";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "occupation,occname,College_code"; //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of Occupation";              //Report Title
                            break;

                        //added on 16-05-2020 by Vaishali for Announcement

                        case "acd_announcement_type":  //academic
                            captions[0, 0] = "Announcement_Type";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "announce_type";                        //FieldName
                            captions[0, 3] = "typeid";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "typeid,announce_type,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Announcement Type";                    //Report Title
                            break;

                        case "acd_paymenttype":  //academic
                            captions[0, 0] = "Payment Type";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "paytypename";                      //FieldName
                            captions[0, 3] = "paytypeno";                        //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "paytypeno,paytypename,remark,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Payment Types";             //Report Title

                            captions[1, 0] = "Remark to be Shown on Fee Receipt";                     //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "remark";                      //FieldName
                            captions[1, 3] = "paytypeno";                        //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "paytypeno,paytypename,remark,College_code";   //FieldNames
                            captions[1, 6] = "string2";                           //Validation Type
                            captions[1, 7] = "400";                               //Max length
                            captions[1, 8] = "List of Payment Types";             //Report Title
                            break;

                        case "acd_degree":  //academic
                            captions[0, 0] = "Degree Name";                           //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "degreename";                       //FieldName
                            captions[0, 3] = "degreeno";                         //ID
                            captions[0, 4] = "3";                                //No of Fields
                            captions[0, 5] = "degreeno,degreename,code,DEGREE_CODE,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "400";                              //Max length
                            captions[0, 8] = "List of Degree";                   //Report Title

                            captions[1, 0] = "Degree Short Name";                 //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "code";                             //FieldName
                            captions[1, 3] = "degreeno";                         //ID
                            captions[1, 4] = "3";                                //No of Fields
                            captions[1, 5] = "degreeno,degreename,code,DEGREE_CODE,College_code";   //FieldNames
                            captions[1, 6] = "string2";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Degree";                   //Report Title

                            captions[2, 0] = "Degree Code";                       //Label
                            captions[2, 1] = "textbox";                          //Control
                            captions[2, 2] = "DEGREE_CODE";                             //FieldName
                            captions[2, 3] = "degreeno";                         //ID
                            captions[2, 4] = "3";                                //No of Fields
                            captions[2, 5] = "degreeno,degreename,code,DEGREE_CODE,College_code";   //FieldNames
                            captions[2, 6] = "string2";                           //Validation Type
                            captions[2, 7] = "16";                               //Max length
                            captions[2, 8] = "List of Degree";                   //Report Title

                            break;  
                        case "acd_caste":  //academic
                            captions[0, 0] = "Caste";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "caste";                            //FieldName
                            captions[0, 3] = "casteno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "casteno,caste,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Caste";                    //Report Title
                            break;

                        case "acd_qualexm":  //academic
                            captions[0, 0] = "Qualifying Exam";                  //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "qualiexmname";                     //FieldName
                            captions[0, 3] = "qualifyno";                        //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "qualifyno,qualiexmname,College_code"; //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "25";                               //Max length
                            captions[0, 8] = "List of Qualifying Exam";          //Report Title
                            break;

                        case "acd_year":  //academic
                            captions[0, 0] = "Year";                             //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "yearname";                         //FieldName
                            captions[0, 3] = "year";                             //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "year,yearname,College_code";       //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                                //Max length
                            captions[0, 8] = "List of Class Year";               //Report Title
                            break;

                        case "acd_section":  //academic
                            captions[0, 0] = "Section";                          //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "sectionname";                      //FieldName
                            captions[0, 3] = "sectionno";                        //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "sectionno,sectionname,College_code";//FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "25";                               //Max length
                            captions[0, 8] = "List of Sections";                 //Report Title
                            break;


                        case "acd_academic_roommaster":  //academic
                            captions[0, 0] = "Room Name";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "Roomname";                      //FieldName
                            captions[0, 3] = "Roomno";                        //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "Roomno,Roomname,intake,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Rooms";             //Report Title


                            captions[1, 0] = "Room Intake";                     //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "Intake";                      //FieldName
                            captions[1, 3] = "Roomno";                        //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "Roomno,Roomname,intake,College_code";   //FieldNames
                            captions[1, 6] = "string2";                           //Validation Type
                            captions[1, 7] = "50";                               //Max length
                            captions[1, 8] = "List of Rooms";             //Report Title
                            break;


                    

                        //case "acd_academic_roommaster":  //academic
                        //     captions[0, 0] = "Room";                          //Label
                        //    captions[0, 1] = "textbox";                          //Control
                        //    captions[0, 2] = "Roomname";                      //FieldName
                        //    captions[0, 3] = "Roomno";                        //ID
                        //    captions[0, 4] = "1";                                //No of Fields
                        //    captions[0, 5] = "Roomno,Roomname,College_code";//FieldNames
                        //    captions[0, 6] = "string";                           //Validation Type
                        //    captions[0, 7] = "25";                               //Max length
                        //    captions[0, 8] = "List of Rooms";                 //Report Title
                        //    break;

                        case "acd_semester":  //academic
                            captions[0, 0] = "Semester Name";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "semestername";                        //FieldName
                            captions[0, 3] = "semesterno";                          //ID
                            captions[0, 4] = "3";                                //No of Fields
                            captions[0, 5] = "Semesterno,Semestername,Semfullname,odd_even,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of Semester";                    //Report Title

                            captions[1, 0] = "Semester Full Name";                            //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "Semfullname";                        //FieldName
                            captions[1, 3] = "semesterno";                          //ID
                            captions[1, 4] = "3";                                //No of Fields
                            captions[1, 5] = "Semesterno,Semestername,Semfullname,odd_even,College_code";   //FieldNames
                            captions[1, 6] = "string";                           //Validation Type
                            captions[1, 7] = "35";                               //Max length
                            captions[1, 8] = "List of Semester";                    //Report Title                            

                            captions[2, 0] = "ODD/EVEN";                            //Label
                            captions[2, 1] = "textbox";                          //Control
                            captions[2, 2] = "odd_even";                        //FieldName
                            captions[2, 3] = "semesterno";                          //ID
                            captions[2, 4] = "3";                                //No of Fields
                            captions[2, 5] = "Semesterno,Semestername,Semfullname,odd_even,College_code";   //FieldNames
                            captions[2, 6] = "string2";                           //Validation Type
                            captions[2, 7] = "2";                               //Max length
                            captions[2, 8] = "List of Semester";                    //Report Title
                         break;

                        case "acd_subjecttype":  //academic
                         captions[0, 0] = "Subject Type";                     //Label
                         captions[0, 1] = "textbox";                          //Control
                         captions[0, 2] = "subname";                          //FieldName
                         captions[0, 3] = "subid";                            //ID
                         captions[0, 4] = "1";                                //No of Fields
                         captions[0, 5] = "subid,subname,College_code";   //FieldNames
                         captions[0, 6] = "string";                           //Validation Type
                         captions[0, 7] = "40";                               //Max length
                         captions[0, 8] = "List of Subject Types";            //Report Title
                         break;

                        case "acd_scholorshiptype":  //ScholorshipType
                            captions[0, 0] = "Scholarship";                      //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "scholorshipname";                  //FieldName
                            captions[0, 3] = "scholorshiptypeno";                //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "scholorshiptypeno,scholorshipname,scholorshipcode,College_code";   //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Scholarships";             //Report Title

                            captions[1, 0] = "Code";                             //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "scholorshipcode";                  //FieldName
                            captions[1, 3] = "scholorshiptypeno";                //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "scholorshiptypeno,scholorshipname,scholorshipcode,College_code";   //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "5";                                //Max length
                            captions[1, 8] = "List of Scholarships";             //Report Title
                            break;

                        case "acd_admission_round":  //academic
                            captions[0, 0] = "Admission Round Name";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "roundname";                        //FieldName
                            captions[0, 3] = "admroundno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "admroundno,roundname,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "20";                               //Max length
                            captions[0, 8] = "List of Admission Round";                    //Report Title
                            break;
                        case "acd_certificate_master":  //academic
                            captions[0, 0] = "Certificate Name";                 //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "cert_name";                        //FieldName
                            captions[0, 3] = "cert_no";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "cert_no,cert_name,cert_short_name,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Certificates";              //Report Title

                            captions[1, 0] = "Short Name";                 //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "cert_short_name";                        //FieldName
                            captions[1, 3] = "cert_no";                          //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "cert_no,cert_name,cert_short_name,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Certificates";              //Report Title
                            break;
                        

                        case "acd_group":  //academic
                            captions[0, 0] = "Group Name";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "groupname";                        //FieldName
                            captions[0, 3] = "groupno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "groupno,groupname,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "100";                               //Max length
                            captions[0, 8] = "List of Elective Name";                    //Report Title
                            break;

                        case "acd_electgroup":  //academic
                            captions[0, 0] = "Elective";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "groupname";                            //FieldName
                            captions[0, 3] = "groupno";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "groupno,groupname,choicefor,College_code";        //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of Elective";                    //Report Title

                            captions[1, 0] = "Choice For";                            //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "choicefor";                            //FieldName
                            captions[1, 3] = "groupno";                          //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "groupno,groupname,choicefor,College_code";        //FieldNames
                            captions[1, 6] = "numeric";                          //Validation Type
                            captions[1, 7] = "5";                              //Max length
                            captions[1, 8] = "List of Elective";    
                        

                            break;

                        case "acd_schemetype":  //academic
                            captions[0, 0] = "Scheme";                           //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "schemetype";                       //FieldName
                            captions[0, 3] = "schemetypeno";                         //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "schemetypeno,schemetype,schemetype_code,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "100";                              //Max length
                            captions[0, 8] = "List of Scheme";                   //Report Title

                            captions[1, 0] = "Code";                             //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "schemetype_code";                             //FieldName
                            captions[1, 3] = "schemetypeno";                         //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "schemetypeno,schemetype,schemetype_code,College_code";   //FieldNames
                            captions[1, 6] = "string";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Scheme";                   //Report Title
                            break;

                        case "acd_event":  //academic
                            captions[0, 0] = "Event Name";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "eventname";                            //FieldName
                            captions[0, 3] = "eventno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "eventno,eventname,College_code";        //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of Event";                    //Report Title
                            break;

                        case "acd_quota":  //academic
                            captions[0, 0] = "Quota Name";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "quota";                            //FieldName
                            captions[0, 3] = "quotano";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "quotano,quota,College_code";        //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of Quota";                    //Report Title
                            break;

                        case "acd_document_list":  //academic
                            captions[0, 0] = "Document Name";                            //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "documentname";                            //FieldName
                            captions[0, 3] = "documentno";                          //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "documentno,documentname,College_code";        //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "100";                              //Max length
                            captions[0, 8] = "List of Documents";                    //Report Title
                            break;

                        case "acd_floor":  //academic
                            captions[0, 0] = "Floor";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "floorname";                          //FieldName
                            captions[0, 3] = "floorno";                            //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "floorno,floorname,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "100";                               //Max length
                            captions[0, 8] = "List of Floors";            //Report Title
                            break;

                        case "acd_eventcateogry":  //academic
                            captions[0, 0] = "Event Category Name";             //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "EVENTCATENAME";                   //FieldName
                            captions[0, 3] = "EVENTCATENO";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "EVENTCATENO,EVENTCATENAME,COLLEGE_CODE"; //FieldNames
                            captions[0, 6] = "string";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Event Category Name";            //Report Title
                            break;

                        case "acd_convocation_master":
                            captions[0, 0] = "Convocation Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "convocation_name";                        //FieldName
                            captions[0, 3] = "conv_no";                     //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "conv_no,convocation_name,college_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "250";                              //Max length
                            captions[0, 8] = "List of Convocation";                 //Report Title
                            break;

                        case "acd_withheld_reason":  //withheld info
                            captions[0, 0] = "Withheld Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "Withheld_name";                        //FieldName
                            captions[0, 3] = "withheldno";                     //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "withheldno,withheld_name,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "250";                              //Max length
                            captions[0, 8] = "List of WithHeld";                 //Report Title

                            break;

                       case "acd_checkers":  //academic
                            captions[0, 0] = "Checkername";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "checkername";                            //FieldName
                            captions[0, 3] = "checkerno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "checkerno,checkername,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Checkers";                    //Report Title
                            break;

                       case "acd_exam_pattern":  //academic
                            captions[0, 0] = "Exam Pattern";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "pattern_name";                            //FieldName
                            captions[0, 3] = "patternno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "patternno,pattern_name,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Exam Pattern";                    //Report Title
                            break;

                        //case "acd_phd_supervisor":  //academic
                        //    captions[0, 0] = "Supervisor";                          //Label
                        //    captions[0, 1] = "textbox";                          //Control
                        //    captions[0, 2] = "supervisorname";                      //FieldName
                        //    captions[0, 3] = "supervisorno";                        //ID
                        //    captions[0, 4] = "1";                                //No of Fields
                        //    captions[0, 5] = "supervisorno,supervisorname,College_code";//FieldNames
                        //    captions[0, 6] = "string";                           //Validation Type
                        //    captions[0, 7] = "25";                               //Max length
                        //    captions[0, 8] = "List of Supervisor Name";                 //Report Title
                        //    break;

                       case "acd_currency":  //academic
                            captions[0, 0] = "Currency Name";                           //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "cur_name";                       //FieldName
                            captions[0, 3] = "cur_no";                         //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "Cur_no,Cur_name,Cur_short,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "100";                              //Max length
                            captions[0, 8] = "List of Currency";                   //Report Title

                            captions[1, 0] = "Short Name";                             //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "Cur_Short";                             //FieldName
                            captions[1, 3] = "Cur_No";                         //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "Cur_no,Cur_name,Cur_short,College_code";   //FieldNames
                            captions[1, 6] = "string";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Currency";
                            break;
                       case "acd_branch":  //academic
                            captions[0, 0] = "Branch Name";                 //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "LONGNAME";                        //FieldName
                            captions[0, 3] = "BRANCHNO";                          //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "BRANCHNO,LONGNAME,SHORTNAME,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Branch";              //Report Title

                            captions[1, 0] = "Branch Short Name";                 //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "SHORTNAME";                        //FieldName
                            captions[1, 3] = "Branchno";                          //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "BRANCHNO,LONGNAME,SHORTNAME,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Branch";              //Report Title
                            break;

                       case "acd_qualilevel":  //academic
                            captions[0, 0] = "Qualification Level";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "qualilevelname";                            //FieldName
                            captions[0, 3] = "qualilevelno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "qualilevelno,qualilevelname,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Qualification Level";                    //Report Title
                            break;

                       case "acd_document_type":  //academic
                            captions[0, 0] = "Document Type";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "documentname";                            //FieldName
                            captions[0, 3] = "documenttypeno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "documenttypeno,documentname,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Document Type";                    //Report Title
                            break;

                      case "acd_ua_section":  //academic
                            captions[0, 0] = "Section Name";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "UA_SECTIONNAME";                        //FieldName
                            captions[0, 3] = "UA_SECTION";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "UA_SECTION,UA_SECTIONNAME,COLLEGE_CODE";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of Degree Type";                    //Report Title
                            break;


                      case "acd_exam_center":  //academic
                            captions[0, 0] = "Exam Center";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "CENTER_NAME";                        //FieldName
                            captions[0, 3] = "CENTER_ID";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "CENTER_ID,CENTER_NAME,COLLEGE_CODE";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of centers";                    //Report Title
                            break;

                      case "acd_fees_master":  //academic
                            captions[0, 0] = "Fee Item Name";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "FeeItemName";                      //FieldName
                            captions[0, 3] = "FeeItemId";                        //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "FeeItemId,FeeItemName,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Fee Items";             //Report Title
                            break;

                      case "acd_application_master":  //withheld info
                            captions[0, 0] = "Application Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "apname";                        //FieldName
                            captions[0, 3] = "apno";                     //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "apno,apname,amount,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Application Name";     //Report Title
                            captions[1, 0] = "Amount";                             //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "amount";                             //FieldName
                            captions[1, 3] = "apno";                         //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "apno,apname,amount,College_code";   //FieldNames
                            captions[1, 6] = "numeric";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Application Name";
                            break;

                      case "acd_reciept_head_master":  //academic
                            captions[0, 0] = "Receipt Short Name";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "rhshort";                      //FieldName
                            captions[0, 3] = "recieptheadno";                        //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "recieptheadno,rhshort,rhname,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of Receipt Master Head";             //Report Title

                            captions[1, 0] = "List of Receipt Master Head";                     //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "rhname";                      //FieldName
                            captions[1, 3] = "recieptheadno";                        //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "recieptheadno,rhshort,rhname,College_code";   //FieldNames
                            captions[1, 6] = "string2";                           //Validation Type
                            captions[1, 7] = "400";                               //Max length
                            captions[1, 8] = "List of Receipt Master Head";             //Report Title
                            break;

                      case "acd_gst_head_master":  //academic
                            captions[0, 0] = "GST Name";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "GSTNAME";                      //FieldName
                            captions[0, 3] = "GSTHEADNO";                        //ID
                            captions[0, 4] = "2";                                //No of Fields
                            captions[0, 5] = "GSTHEADNO,GSTNAME,GSTPERCENTAGE,College_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                               //Max length
                            captions[0, 8] = "List of GST Master Head";             //Report Title

                            captions[1, 0] = "Percentage";          //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "GSTPERCENTAGE";                      //FieldName
                            captions[1, 3] = "GSTHEADNO";                        //ID
                            captions[1, 4] = "2";                                //No of Fields
                            captions[1, 5] = "GSTHEADNO,GSTNAME,GSTPERCENTAGE,College_code";   //FieldNames
                            captions[1, 6] = "numeric";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of GST Master Head";             //Report Title
                            break;
                      case "acd_srcategory":  //academic
                            captions[0, 0] = "SR Category";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "srcategory";                        //FieldName
                            captions[0, 3] = "srcategoryno";                      //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "srcategoryno,srcategory,college_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of SR Category";                //Report Title
                            break;

                      //added by:Dileep
                      //Date:18/10/2019
                      case "acd_authority": //academic
                            captions[0, 0] = "Authority";                                   //Label
                            captions[0, 1] = "textbox";                                     //control
                            captions[0, 2] = "authorityname";                               //Field Name
                            captions[0, 3] = "authorityno";                                 //ID
                            captions[0, 4] = "1";                                           //No  of Fields
                            captions[0, 5] = "authorityno,authorityname,College_code";      //FieldNames
                            captions[0, 6] = "string";                                      //Validation Type
                            captions[0, 7] = "50";                                          //Max Length
                            captions[0, 8] = "List of Authority";                           //Report Title
                            break;

                      // Added by Swapnil
                      case "acd_feedback_parameter":  //Feedback
                            captions[0, 0] = "Feedback Type";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "fpname";                        //FieldName
                            captions[0, 3] = "fpno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "fpno,fpname,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of Feedback Parameter";                    //Report Title
                            break;

                      // Added by Deepali
                      case "acd_hostel_type":  //Hostel Type
                            captions[0, 0] = "Hostel Type";                //Label
                            captions[0, 1] = "textbox";                      //Control
                            captions[0, 2] = "hostel_type_name";                  //FieldName
                            captions[0, 3] = "hostel_type_no";                     //ID
                            captions[0, 4] = "1";                            //No of Fields
                            captions[0, 5] = "hostel_type_no,hostel_type_name,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Hostel Type";                 //Report Title
                            break;

                      // Added by Deepali
                      case "acd_transport":  //Transport
                            captions[0, 0] = "Transport";                //Label
                            captions[0, 1] = "textbox";                      //Control
                            captions[0, 2] = "transport_name";                  //FieldName
                            captions[0, 3] = "transport_no";                     //ID
                            captions[0, 4] = "1";                            //No of Fields
                            captions[0, 5] = "transport_no,transport_name,College_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Transport";                 //Report Title
                            break;

                      // Added by Deepali
                      case "acd_board":  //academic
                            captions[0, 0] = "Board";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "board";                            //FieldName
                            captions[0, 3] = "boardno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "boardno,board,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "100";                               //Max length
                            captions[0, 8] = "List of Boards";                    //Report Title
                            break; // Added by Deepali

                      //Added By Nikhil Lambe on 29042020
                      case "att_wieghtage":                                           //academic
                            captions[0, 0] = "Attendance";                              //Label
                            captions[0, 1] = "textbox";                                 //Control
                            captions[0, 2] = "attendance";                              //fieldname
                            captions[0, 3] = "ATT_WT_NO";                                    //ID
                            captions[0, 4] = "2";                                       //No of fields
                            captions[0, 5] = "ATT_WT_NO,attendance,weightage,college_code";  //fieldsnames
                            captions[0, 6] = "string2";                                    //Validation Type
                            captions[0, 7] = "200";                                       //Max length
                            captions[0, 8] = "List of Attendance Weightage";             //Report Title

                            captions[1, 0] = "Weightage";                              //Label
                            captions[1, 1] = "textbox";                                 //Control
                            captions[1, 2] = "weightage";                              //fieldname
                            captions[1, 3] = "ATT_WT_NO";                                    //ID
                            captions[1, 4] = "2";                                       //No of fields
                            captions[1, 5] = "ATT_WT_NO,attendance,weightage,college_code";  //fieldsnames
                            captions[1, 6] = "numeric";                                    //Validation Type
                            captions[1, 7] = "10";                                       //Max length
                            captions[1, 8] = "List of Attendance Weightage";             //Report Title
                            break;

                      //Added By Nikhil Lambe for affiliated college master

                      case "acd_affiliated_auth_master":  //academic
                            captions[0, 0] = "Author";                             //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "auth_name";                         //FieldName
                            captions[0, 3] = "AUTH_ID";                             //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "auth_id,auth_name,college_code";       //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                                //Max length
                            captions[0, 8] = "List of Authority";               //Report Title
                            break;
                      //end
                      //Added By Nikhil Lambe for event type master
                      case "acd_event_type":  //academic
                            captions[0, 0] = "Event Type";                             //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "event_type";                         //FieldName
                            captions[0, 3] = "event_id";                             //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "event_id,event_type,college_code";       //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                                //Max length
                            captions[0, 8] = "List of Event Types";               //Report Title
                            break;
                      //end
                      //Added By Nikhil Lambe for Participants type master
                      case "acd_participant_type":  //academic
                            captions[0, 0] = "Participant Type";                             //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "participant_type";                         //FieldName
                            captions[0, 3] = "participant_id";                             //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "participant_id,participant_type,college_code";       //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "50";                                //Max length
                            captions[0, 8] = "List of Participants Types";               //Report Title
                            break;
                      //end

                      // Added by Nikhil V. Lambe for affiliated degree master
                      case "acd_affiliated_degree":  //Affiliated
                            captions[0, 0] = "Affiliated Degree Name";                           //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "degreename";                       //FieldName
                            captions[0, 3] = "degreeno";                         //ID
                            captions[0, 4] = "3";                                //No of Fields
                            captions[0, 5] = "degreeno,degreename,code,degree_code,college_code";   //FieldNames
                            captions[0, 6] = "string2";                           //Validation Type
                            captions[0, 7] = "400";                              //Max length
                            captions[0, 8] = "List of Affiliated Degree";                   //Report Title

                            captions[1, 0] = "Degree Short Name";                 //Label
                            captions[1, 1] = "textbox";                          //Control
                            captions[1, 2] = "code";                             //FieldName
                            captions[1, 3] = "degreeno";                         //ID
                            captions[1, 4] = "3";                                //No of Fields
                            captions[1, 5] = "degreeno,degreename,code,degree_code,college_code";   //FieldNames
                            captions[1, 6] = "string2";                           //Validation Type
                            captions[1, 7] = "10";                               //Max length
                            captions[1, 8] = "List of Affiliated Degree";                   //Report Title

                            captions[2, 0] = "Degree Code";                       //Label
                            captions[2, 1] = "textbox";                          //Control
                            captions[2, 2] = "degree_code";                             //FieldName
                            captions[2, 3] = "degreeno";                         //ID
                            captions[2, 4] = "3";                                //No of Fields
                            captions[2, 5] = "degreeno,degreename,code,degree_code,college_code";   //FieldNames
                            captions[2, 6] = "string2";                           //Validation Type
                            captions[2, 7] = "16";                               //Max length
                            captions[2, 8] = "List of Affiliated Degree";                   //Report Title
                            break;

                      //Added By Swapnil P for SMS Template
                      case "acd_sms_template_type":  //academic
                            captions[0, 0] = "SMS Template Type";                             //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "template_type";                         //FieldName
                            captions[0, 3] = "template_id";                             //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "template_id,template_type,college_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "50";                                //Max length
                            captions[0, 8] = "List of SMS Template Type";               //Report Title
                            break;
                      //end
                     
                        #endregion

                        #region HOSTEL

                        case "acd_hostel":  //hostel info
                            captions[0, 0] = "Hostel Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "hostel_name";                        //FieldName
                            captions[0, 3] = "hostel_no";                     //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "hostel_no,hostel_name,hostel_address,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Hostel";                 //Report Title

                            captions[1, 0] = "Hostel Address";                  //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "hostel_address";                  //FieldName
                            captions[1, 3] = "hostel_no";                        //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "hostel_no,hostel_name,hostel_address,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Hostel";                  //Report Title
                            break;
                        case "acd_hostel_mess":                                 //Hostel Mess
                            captions[0, 0] = "Mess Name";                       //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "mess_name";                       //FieldName
                            captions[0, 3] = "mess_no";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "mess_no,mess_name,college_code";  //FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Hostel Mess";             //Report Title
                            break;
                        case "acd_hostel_asset_type":  //Asset Type
                            captions[0, 0] = "Asset Type";                //Label
                            captions[0, 1] = "textbox";                      //Control
                            captions[0, 2] = "asset_type_name";                  //FieldName
                            captions[0, 3] = "asset_type_no";                     //ID
                            captions[0, 4] = "1";                            //No of Fields
                            captions[0, 5] = "asset_type_no,asset_type_name,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Asset Type";                 //Report Title
                            break;
                        case "acd_hostel_quota":  //hostel quota info
                            captions[0, 0] = "Quota Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "quota_name";                        //FieldName
                            captions[0, 3] = "quota_no";                     //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "quota_no,quota_name,quota_per,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Hostel Quota";                 //Report Title

                            captions[1, 0] = "Quota Percernt";                  //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "quota_per";                  //FieldName
                            captions[1, 3] = "quota_no";                        //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "quota_no,quota_name,quota_per,College_code";//FieldNames
                            captions[1, 6] = "numeric";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Hostel Quota";                  //Report Title
                            break;

                        case "acd_hostel_attendance_remark":  //Asset Type
                            captions[0, 0] = "Remark";                //Label
                            captions[0, 1] = "textbox";                      //Control
                            captions[0, 2] = "remark";                  //FieldName
                            captions[0, 3] = "remarkno";                     //ID
                            captions[0, 4] = "1";                            //No of Fields
                            captions[0, 5] = "remarkno,remark,college_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "1000";                              //Max length
                            captions[0, 8] = "List of Attendance Remark";                 //Report Title
                            break;
                        case "acd_hostel_certificate_remark":               //Certifcate Type
                            captions[0, 0] = "Certificate Remark";                //Label
                            captions[0, 1] = "textbox";                      //Control
                            captions[0, 2] = "cert_remark";                  //FieldName
                            captions[0, 3] = "crno";                        //ID
                            captions[0, 4] = "1";                            //No of Fields
                            captions[0, 5] = "crno,cert_remark,college_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "1000";                              //Max length
                            captions[0, 8] = "List of Certificate Remark";                 //Report Title
                            break;

                        case "acd_hostel_floor":  //floor 
                            captions[0, 0] = "Floor Name";                 //Label
                            captions[0, 1] = "textbox";                    //Control
                            captions[0, 2] = "floor_name";                 //FieldName
                            captions[0, 3] = "floor_no";                   //ID
                            captions[0, 4] = "2";                         //No of Fields
                            captions[0, 5] = "floor_no,floor_name,code,College_code";//FieldNames
                            captions[0, 6] = "string2";                   //Validation Type
                            captions[0, 7] = "50";                        //Max length
                            captions[0, 8] = "List of Floor";             //Report Title

                            captions[1, 0] = "Floor Code";               //Label
                            captions[1, 1] = "textbox";                  //Control
                            captions[1, 2] = "code";                     //FieldName
                            captions[1, 3] = "floor_no";                 //ID
                            captions[1, 4] = "2";                        //No of Fields
                            captions[1, 5] = "floor_no,floor_name,code,College_code";//FieldNames
                            captions[1, 6] = "string2";                  //Validation Type
                            captions[1, 7] = "50";                       //Max length
                            captions[1, 8] = "List of Floor";            //Report Title
                            break;

                        case "acd_ufm_master":  //academic
                            captions[0, 0] = "Category";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "ufm_name";                        //FieldName
                            captions[0, 3] = "ufmno";                      //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "ufmno,ufm_name,ufm_desc,ufm_punishment,college_code";//FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                             //Max length
                            captions[0, 8] = "List of UFM Copy Case Category";                //Report Title

                            captions[1, 0] = "Category Desc";                        //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "ufm_desc";                        //FieldName
                            captions[1, 3] = "ufmno";                      //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "ufmno,ufm_name,ufm_desc,ufm_punishment,college_code";//FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "2000";                             //Max length
                            captions[1, 8] = "List of UFM Copy Case Category";//Report Title

                            captions[2, 0] = "Category Punishment";                        //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "ufm_punishment";                        //FieldName
                            captions[2, 3] = "ufmno";                      //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "ufmno,ufm_name,ufm_desc,ufm_punishment,college_code";//FieldNames
                            captions[2, 6] = "string";                          //Validation Type
                            captions[2, 7] = "1000";                             //Max length
                            captions[2, 8] = "List of UFM Copy Case Category"; //Report Title
                            break;

                        #endregion

                        #region Traning And Placement
                        //Traning And Placement                            
                        case "acd_tp_jobtype": //Job Type   
                            captions[0, 0] = "Job Type";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "jobtype";                         //FieldName
                            captions[0, 3] = "jobno";                           //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "jobno,jobtype,College_code";      //FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Job Type";                //Report Title
                            break;

                        case "acd_tp_compcategory": //Company Category 
                            captions[0, 0] = "Category";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "catname";                         //FieldName
                            captions[0, 3] = "catno";                           //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "catno,catname,college_code";      //FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Company Category";                //Report Title
                            break;
                        case "acd_tp_selectiontype": //Selection Type
                            captions[0, 0] = "Selection Type ";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "selectname";                         //FieldName
                            captions[0, 3] = "selectno";                           //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "selectno,selectname,college_code";      //FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Selection Type";                //Report Title
                            break;

                        case "acd_tp_class": // Class Master
                            captions[0, 0] = "Class ";                        //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "CLASSNAME";                         //FieldName
                            captions[0, 3] = "CLASSNO";                           //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "CLASSNO,CLASSNAME,college_code";      //FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Class";                //Report Title
                            break;
                        #endregion
                       
                        #region Staff Development Cell
                        case "sdc_remark":  //Remark
                            captions[0, 0] = "Remark Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "remark";                        //FieldName
                            captions[0, 3] = "remarkno";                     //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "remarkno,remark,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "200";                              //Max length
                            captions[0, 8] = "List of Remark";                 //Report Title                            
                            break;

                        case "sdc_participant_level":  //Level
                            captions[0, 0] = "Level Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "levelname";                        //FieldName
                            captions[0, 3] = "levelno";                     //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "levelno,levelname,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "100";                              //Max length
                            captions[0, 8] = "List of Participant Level";                 //Report Title                            
                            break;

                        case "sdc_session": //Session
                            captions[0, 0] = "Session Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "session_name";                        //FieldName
                            captions[0, 3] = "session_no";                     //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "session_no,session_name,start_date,end_date,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "session";                 //Report Title

                            captions[1, 0] = "Start Date";                     //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "start_date";                        //FieldName
                            captions[1, 3] = "session_no";                     //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "session_no,session_name,start_date,end_date,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "100";                              //Max length
                            captions[1, 8] = "session";                 //Report Title

                            captions[2, 0] = "end Date";                     //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "end_date";                        //FieldName
                            captions[2, 3] = "session_no";                     //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "session_no,session_name,start_date,end_date,College_code";//FieldNames
                            captions[2, 6] = "string2";                          //Validation Type
                            captions[2, 7] = "100";                              //Max length
                            captions[2, 8] = "session";                 //Report Title
                            break;

                        case "sdc_heads": //Heads
                            captions[0, 0] = "Head Name";                     //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "headname";                        //FieldName
                            captions[0, 3] = "hno";                     //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "hno,headname,head_shortname,head_longname,College_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "5";                              //Max length
                            captions[0, 8] = "Fee Heads";                 //Report Title      

                            captions[1, 0] = "Head Short Name";                     //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "head_shortname";                        //FieldName
                            captions[1, 3] = "hno";                     //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "hno,headname,head_shortname,head_longname,College_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "15";                              //Max length
                            captions[1, 8] = "Fee Heads";                 //Report Title   

                            captions[2, 0] = "Head Long Name";                     //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "head_longname";                        //FieldName
                            captions[2, 3] = "hno";                     //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "hno,headname,head_shortname,head_longname,College_code";//FieldNames
                            captions[2, 6] = "string2";                          //Validation Type
                            captions[2, 7] = "100";                              //Max length
                            captions[2, 8] = "Fee Heads";                 //Report Title   
                            break;

                        #endregion

                        #region STORE
                        case "store_city":       //store
                            captions[0, 0] = "City";                        //Label
                            captions[0, 1] = "textbox";                     //Control
                            captions[0, 2] = "city";                        //FieldName
                            captions[0, 3] = "cityno";                      //ID
                            captions[0, 4] = "1";                           //No of Fields
                            captions[0, 5] = "cityno,city,College_code";    //FieldNames
                            captions[0, 6] = "string";                      //Validation Type
                            captions[0, 7] = "25";                          //Max length
                            captions[0, 8] = "List of City";                //Report Title
                            break;
                        case "store_state":  //store
                            captions[0, 0] = "State";                            //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "statename";                        //FieldName
                            captions[0, 3] = "stateno";                          //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "stateno,statename,College_code";   //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "35";                               //Max length
                            captions[0, 8] = "List of State";                    //Report Title
                            break;
                        case "store_budgethead":  //store
                            captions[0, 0] = "Budget Head";                     //Label
                            captions[0, 1] = "textbox";                          //Control
                            captions[0, 2] = "bhname";                          //FieldName
                            captions[0, 3] = "bhno";                            //ID
                            captions[0, 4] = "1";                                //No of Fields
                            captions[0, 5] = "bhno,bhname,College_code";       //FieldNames
                            captions[0, 6] = "string";                           //Validation Type
                            captions[0, 7] = "100";                               //Max length
                            captions[0, 8] = "Budget Head";                     //Report Title
                            break;
                        case "store_approval_level":  //store
                            captions[0, 0] = "Approval Level";                                     //Label
                            captions[0, 1] = "textbox";                                            //Control
                            captions[0, 2] = "aplt";                                      //FieldName
                            captions[0, 3] = "aplno";                                  //ID
                            captions[0, 4] = "1";                                                  //No of Fields
                            captions[0, 5] = "aplno,aplt,College_code";       //FieldNames
                            captions[0, 6] = "string";                                             //Validation Type
                            captions[0, 7] = "100";                                                 //Max length
                            captions[0, 8] = "Approval Level";                                     //Report Title
                            break;
                        case "store_financial_year":  //store
                            captions[0, 0] = "Financial Year";                                     //Label
                            captions[0, 1] = "textbox";                                            //Control
                            captions[0, 2] = "fname";                                      //FieldName
                            captions[0, 3] = "fno";                                  //ID
                            captions[0, 4] = "1";                                                  //No of Fields
                            captions[0, 5] = "fno,fname,College_code";       //FieldNames
                            captions[0, 6] = "string2";                                             //Validation Type
                            captions[0, 7] = "100";                                                 //Max length
                            captions[0, 8] = "Financial Year ";                                     //Report Title
                            break;
                        #endregion       

                        #region Dispatch(Inward Outward)
                        case "admn_io_post_type":
                            captions[0, 0] = "Post Type";
                            captions[0, 1] = "textbox";
                            captions[0, 2] = "POSTTYPENAME";
                            captions[0, 3] = "POSTTYPENO";
                            captions[0, 4] = "1";
                            captions[0, 5] = "POSTTYPENO,POSTTYPENAME,college_code";
                            captions[0, 6] = "string2";
                            captions[0, 7] = "25";
                            captions[0, 8] = "List of Post Type";
                            break;
                        case "admn_io_department":       //payroll
                            captions[0, 0] = "Department Name";                 //Label
                            captions[0, 1] = "textbox";                        //Control
                            captions[0, 2] = "departmentname";                        //FieldName
                            captions[0, 3] = "deptno";                      //ID
                            captions[0, 4] = "2";                              //No of Fields
                            captions[0, 5] = "deptno,departmentname,deptcode,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "50";                             //Max length
                            captions[0, 8] = "List of Department";             //Report Title

                            captions[1, 0] = "Department Code";                     //Label
                            captions[1, 1] = "textbox";                        //Control
                            captions[1, 2] = "deptcode";                       //FieldName
                            captions[1, 3] = "deptno";                      //ID
                            captions[1, 4] = "2";                              //No of Fields
                            captions[1, 5] = "deptno,departmentname,deptcode,College_code";//FieldNames
                            captions[1, 6] = "string2";                         //Validation Type
                            captions[1, 7] = "5";                             //Max length 
                            captions[1, 8] = "List of Department";             //Report Title


                            // Created By Swati---------------------************
                            // Date:11-03-14
                            captions[2, 0] = "deptcode"; // 1st Field Name    
                            captions[2, 1] = "departmentname";//2nd FieldName
                            // ---------------------    **************            
                            break;
                        case "admn_io_letter_type":
                            captions[0, 0] = "Letter Type";
                            captions[0, 1] = "textbox";
                            captions[0, 2] = "lettertype";
                            captions[0, 3] = "lettertypeno";
                            captions[0, 4] = "2";                              //No of Fields
                            captions[0, 5] = "lettertypeno,lettertype,lettertypecode,College_code";//FieldNames
                            captions[0, 6] = "string2";                         //Validation Type
                            captions[0, 7] = "25";                             //Max length
                            captions[0, 8] = "List of Letter Type";             //Report Title

                            captions[1, 0] = "Letter Type Code";                     //Label
                            captions[1, 1] = "textbox";                        //Control
                            captions[1, 2] = "lettertypecode";                       //FieldName
                            captions[1, 3] = "lettertypeno";                      //ID
                            captions[1, 4] = "2";                              //No of Fields
                            captions[1, 5] = "lettertypeno,lettertype,lettertypecode,College_code";//FieldNames
                            captions[1, 6] = "string2";                         //Validation Type
                            captions[1, 7] = "5";                             //Max length 
                            captions[1, 8] = "List of Letter Type";             //Report Title


                            // Created By Swati---------------------************
                            // Date:11-03-14
                            captions[2, 0] = "lettertypecode"; // 1st Field Name    
                            captions[2, 1] = "lettertype";//2nd FieldName
                            //---------------------************

                            break;

                        #endregion
                       
                        #region CEP
                        case "cep_program_type":
                            captions[0, 0] = "Program Type ";
                            captions[0, 1] = "textbox";
                            captions[0, 2] = "PROGRAMTYPENAME";
                            captions[0, 3] = "PRGTYPENO";
                            captions[0, 4] = "1";
                            captions[0, 5] = "PRGTYPENO,PROGRAMTYPENAME,college_code";
                            captions[0, 6] = "string2";
                            captions[0, 7] = "50";
                            captions[0, 8] = "List of Program Types";
                            break;
                        #endregion

                        #region LIBMAN

                        case "lib_author":    //LIBMAN
                            captions[0, 0] = "Author Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "authorname";                           //FieldName
                            captions[0, 3] = "authorno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "authorno,authorname,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "225";                              //Max length
                            captions[0, 8] = "List of Author";                   //Report Title
                            break;

                        case "lib_institute":    //LIBMAN
                            captions[0, 0] = "Insitute Code";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "inscode";                           //FieldName
                            captions[0, 3] = "insno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "insno,inscode,insname,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "8";                              //Max length
                            captions[0, 8] = "List of Institute";                   //Report Title

                            captions[1, 0] = "Institute Name";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "insname";                           //FieldName
                            captions[1, 3] = "insno";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "insno,inscode,insname,College_code";      //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "80";                              //Max length
                            captions[1, 8] = "List of Institute";                   //Report Title
                            break;

                        case "lib_branch":    //LIBMAN
                            captions[0, 0] = "BRANCH CODE";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "branchcode";                           //FieldName
                            captions[0, 3] = "brno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "brno,branchcode,branchname,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "15";                              //Max length
                            captions[0, 8] = "List of Branch";                   //Report Title

                            captions[1, 0] = "Branch Name";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "branchname";                           //FieldName
                            captions[1, 3] = "BRNO";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "brno,branchcode,branchname,College_code";      //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "50";                              //Max length
                            captions[1, 8] = "List of Branch/Faculty";                   //Report Title
                            break;

                        case "lib_year":    //LIBMAN
                            captions[0, 0] = "Year Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "yearname";                           //FieldName
                            captions[0, 3] = "yrno";                         //ID
                            captions[0, 4] = "3";                               //No of Fields
                            captions[0, 5] = "yrno,yearname,yearfullname,odd_even,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "10";                              //Max length
                            captions[0, 8] = "List of Year/Class";                   //Report Title

                            captions[1, 0] = "Year Full Name";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "yearfullname";                           //FieldName
                            captions[1, 3] = "yrno";                         //ID
                            captions[1, 4] = "3";                               //No of Fields
                            captions[1, 5] = "yrno,yearname,yearfullname,odd_even,College_code";      //FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "10";                              //Max length
                            captions[1, 8] = "List of Year/Class";                   //Report Title

                            captions[2, 0] = "ODD EVEN";                           //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "odd_even";                           //FieldName
                            captions[2, 3] = "yrno";                         //ID
                            captions[2, 4] = "3";                               //No of Fields
                            captions[2, 5] = "yrno,yearname,yearfullname,odd_even,College_code";      //FieldNames
                            captions[2, 6] = "string";                          //Validation Type
                            captions[2, 7] = "10";                              //Max length
                            captions[2, 8] = "List of Year/Class";                   //Report Title
                            break;

                        case "lib_book_type":    //LIBMAN
                            captions[0, 0] = "Document Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "btypename";                           //FieldName
                            captions[0, 3] = "btypeno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "btypeno,btypename,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of Documents";                   //Report Title
                            break;

                        case "lib_belongtype":    //LIBMAN
                            captions[0, 0] = "Belong Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "belongname";                           //FieldName
                            captions[0, 3] = "belongno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "belongno,belongname,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                              //Max length
                            captions[0, 8] = "List of Belongings";                   //Report Title
                            break;


                        case "lib_category":    //LIBMAN
                            captions[0, 0] = "Category";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "category";                           //FieldName
                            captions[0, 3] = "categoryno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "categoryno,category,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "5";                              //Max length
                            captions[0, 8] = "List of Category";                   //Report Title
                            break;

                        case "lib_continent":    //LIBMAN
                            captions[0, 0] = "Continent Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "contname";                           //FieldName
                            captions[0, 3] = "contno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "contno,contname,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "20";                              //Max length
                            captions[0, 8] = "List of Continents";                   //Report Title
                            break;

                        case "lib_bindingtype":    //LIBMAN
                            captions[0, 0] = "Binding Type";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "typebind";                           //FieldName
                            captions[0, 3] = "typeno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "typeno,typebind,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "Binding Type";                   //Report Title
                            break;

                        case "lib_department":    //LIBMAN
                            captions[0, 0] = "Department Code";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "deptcode";                           //FieldName
                            captions[0, 3] = "deptno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "deptno,deptcode,deptname,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "15";                              //Max length
                            captions[0, 8] = "List of Departments";                   //Report Title

                            captions[1, 0] = "Department Name";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "deptname";                           //FieldName
                            captions[1, 3] = "deptno";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "deptno,deptcode,deptname,College_code";      //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "100";                              //Max length
                            captions[1, 8] = "List of Departments";                   //Report Title
                            break;

                        case "lib_main_subject":    //LIBMAN
                            captions[0, 0] = "Main Subject Code";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "msubcode";                           //FieldName
                            captions[0, 3] = "msubno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "msubno,msubcode,msubname,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "10";                              //Max length
                            captions[0, 8] = "List of Main Subjects";                   //Report Title

                            captions[1, 0] = "Main Subject Name";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "msubname";                           //FieldName
                            captions[1, 3] = "msubno";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "msubno,msubcode,msubname,College_code";      //FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "100";                              //Max length
                            captions[1, 8] = "List of Main Subjects";                   //Report Title
                            break;

                        case "lib_book_rack":    //LIBMAN
                            captions[0, 0] = "Rack Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "rack_name";                           //FieldName
                            captions[0, 3] = "rno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "rno,rack_name,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "List of Rack Names";                   //Report Title
                            break;

                        case "lib_reason":    //LIBMAN
                            captions[0, 0] = "Reason";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "reason";                           //FieldName
                            captions[0, 3] = "reasonno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "reasonno,reason,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "250";                              //Max length
                            captions[0, 8] = "List of Reasons";                   //Report Title
                            break;

                        case "lib_bindsection":    //LIBMAN
                            captions[0, 0] = "Section Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "bindsec";                           //FieldName
                            captions[0, 3] = "bsno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "bsno,bindsec,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "25";                              //Max length
                            captions[0, 8] = "Binding Section";                   //Report Title
                            break;

                        case "lib_series":    //LIBMAN
                            captions[0, 0] = "Series Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "sname";                           //FieldName
                            captions[0, 3] = "sno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "sno,sname,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Series";                   //Report Title
                            break;

                        #endregion

                        #region HEALTH

                        case "health_bloodgroup":                                  //HEALTH
                            captions[0, 0] = "Blood Group";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "bloodgr";                           //FieldName
                            captions[0, 3] = "bloodgrno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "bloodgrno,bloodgr,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Blood Groups";                   //Report Title
                            break;


                        case "health_dosagemaster":                                //HEALTH
                            captions[0, 0] = "Dosage Name";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "dname";                           //FieldName
                            captions[0, 3] = "dno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "dno,dname,dqty,College_code";      //FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "50";                              //Max length
                            captions[0, 8] = "List of Dosage";                   //Report Title

                            captions[1, 0] = "Dosage Quantity";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "dqty";                           //FieldName
                            captions[1, 3] = "dno";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "dno,dname,dqty,College_code";      //FieldNames
                            captions[1, 6] = "decimal";                          //Validation Type
                            captions[1, 7] = "10";                              //Max length
                            captions[1, 8] = "List of Dosage";                   //Report Title
                            break;

                        case "health_mgroup":                                  //HEALTH
                            captions[0, 0] = "MGroup";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "mgname";                           //FieldName
                            captions[0, 3] = "mgid";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "mgid,mgname,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "30";                              //Max length
                            captions[0, 8] = "List of MGroups";                   //Report Title
                            break;


                        case "health_patienttype":                                  //HEALTH
                            captions[0, 0] = "Patient Type";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "typename";                           //FieldName
                            captions[0, 3] = "typeno";                         //ID
                            captions[0, 4] = "1";                               //No of Fields
                            captions[0, 5] = "typeno,typename,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "30";                              //Max length
                            captions[0, 8] = "List of Patient Type";                   //Report Title
                            break;

                        case "health_sgroup":                                      //HEALTH
                            captions[0, 0] = "SGroup";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "sgname";                           //FieldName
                            captions[0, 3] = "sgid";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "sgid,sgname,mgid,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "100";                              //Max length
                            captions[0, 8] = "List of SGroup";                   //Report Title

                            captions[1, 0] = "SGroup";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "mgid";                           //FieldName
                            captions[1, 3] = "sgid";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "sgid,sgname,mgid,College_code";      //FieldNames
                            captions[1, 6] = "int";                          //Validation Type
                            captions[1, 7] = "9";                              //Max length
                            captions[1, 8] = "List of SGroup";                   //Report Title
                            break;

                        case "health_medtype":                                  //HEALTH
                            captions[0, 0] = "Medicine Type";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "med_type";                           //FieldName
                            captions[0, 3] = "med_no";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "med_no,med_type,fix_qty,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "20";                              //Max length
                            captions[0, 8] = "List of Patient Type";                   //Report Title

                            captions[1, 0] = "Medicine Type";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "fix_qty";                           //FieldName
                            captions[1, 3] = "med_no";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "med_no,med_type,fix_qty,College_code";      //FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "2";                              //Max length
                            captions[1, 8] = "List of Medicine Type";                   //Report Title
                            break;

                        #endregion

                        #region Vehicle Maintenance
                        case "vehicle_workshop":      //payroll
                            captions[0, 0] = "Workshop Name";                       //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "workshop_name";                        //FieldName
                            captions[0, 3] = "wsno";                          //ID
                            captions[0, 4] = "4";                               //No of Fields
                            captions[0, 5] = "wsno,workshop_name,add1,phone,personname,college_code";//FieldNames
                            captions[0, 6] = "string2";                          //Validation Type
                            captions[0, 7] = "45";                              //Max length
                            captions[0, 8] = "List of Workshop";                    //Report Title

                            captions[1, 0] = "Address";                       //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "add1";                        //FieldName
                            captions[1, 3] = "wsno";                          //ID
                            captions[1, 4] = "4";                               //No of Fields
                            captions[1, 5] = "wsno,workshop_name,add1,phone,personname,college_code";//FieldNames
                            captions[1, 6] = "string2";                          //Validation Type
                            captions[1, 7] = "120";                              //Max length
                            captions[1, 8] = "List of Workshop";                    //Report Title


                            captions[2, 0] = "Phone";                     //Label
                            captions[2, 1] = "textbox";                         //Control
                            captions[2, 2] = "phone";                       //FieldName
                            captions[2, 3] = "wsno";                          //ID
                            captions[2, 4] = "4";                               //No of Fields
                            captions[2, 5] = "wsno,workshop_name,add1,phone,personname,College_code";//FieldNames
                            captions[2, 6] = "number";
                            captions[2, 7] = "10";                          //Validation Type
                            captions[2, 8] = "List of Workshop";                              //Max length

                            captions[3, 0] = "Person Name.";                     //Label
                            captions[3, 1] = "textbox";                         //Control
                            captions[3, 2] = "personname";                       //FieldName
                            captions[3, 3] = "wsno";                          //ID
                            captions[3, 4] = "4";                               //No of Fields
                            captions[3, 5] = "wsno,workshop_name,add1,phone,personname,College_code";//FieldNames
                            captions[3, 6] = "string2";
                            captions[3, 7] = "45";                          //Validation Type
                            captions[3, 8] = "List of Workshop";
                            break;

                        case "vehicle_unit":
                            captions[0, 0] = "Unit Name";
                            captions[0, 1] = "textbox";
                            captions[0, 2] = "unit_name";
                            captions[0, 3] = "unit_id";
                            captions[0, 4] = "1";
                            captions[0, 5] = "unit_id,unit_name,college_code";
                            captions[0, 6] = "string2";
                            captions[0, 7] = "20";
                            captions[0, 8] = "List Of Unit Name";

                            break;
                        #endregion

                        #region Accounts

                        case "acc_acchead":                                      //ACCOUNTS
                            captions[0, 0] = "Account Head Code";                           //Label
                            captions[0, 1] = "textbox";                         //Control
                            captions[0, 2] = "account_head_code";                           //FieldName
                            captions[0, 3] = "acchno";                         //ID
                            captions[0, 4] = "2";                               //No of Fields
                            captions[0, 5] = "acchno,account_head_code,account_head_name,College_code";      //FieldNames
                            captions[0, 6] = "string";                          //Validation Type
                            captions[0, 7] = "15";                              //Max length
                            captions[0, 8] = "List of Account Heads";                   //Report Title

                            captions[1, 0] = "Account Head Name";                           //Label
                            captions[1, 1] = "textbox";                         //Control
                            captions[1, 2] = "account_head_name";                           //FieldName
                            captions[1, 3] = "acchno";                         //ID
                            captions[1, 4] = "2";                               //No of Fields
                            captions[1, 5] = "acchno,account_head_code,account_head_name,College_code";      //FieldNames
                            captions[1, 6] = "string";                          //Validation Type
                            captions[1, 7] = "100";                              //Max length
                            captions[1, 8] = "List of Account Heads";                   //Report Title
                            break;

                        #endregion

                    }
                }

                public HtmlTable CreateTableHTML()
                {
                    HtmlTable tbl = new HtmlTable();
                    tbl.ID = "tblMaster";
                    tbl.Width = "70%";

                    try
                    {
                        //create rows
                        for(int i = 0; i < int.Parse(captions[0, 4]); i++)
                        {
                            HtmlTableRow row = new HtmlTableRow();

                            HtmlTableCell cell1 = new HtmlTableCell();
                            Label lbl = new Label();
                            lbl.Text = "*";
                           // lbl.Attributes.Add("style", "color:Red;");
                            cell1.InnerHtml = "<span style='color:red; font-weight: 800;'>" + lbl.Text + "</span>" + " " + captions[i, 0] + " : ";
                            //cell1.InnerText = lbl.Text + " " + captions[i, 0] + " :";
                            //cell1.Attributes.Add("align", "right");
                            //cell1.Attributes.Add("padding", "10px");
                            //cell1.Attributes.Add("width", "30%");
                            cell1.Attributes.Add("style", "display: block;font-weight:bold;");
                            row.Controls.Add(cell1);

                            if (captions[i, 1].Equals("textbox"))
                            {
                                HtmlTableCell cell2 = new HtmlTableCell();
                               
                                cell2.Attributes.Add("class", "form_left_text");
                                cell2.Attributes.Add("class", "form-group");
                                cell2.Attributes.Add("style", "display: block");

                                TextBox txt = new TextBox();
                                txt.ID = "txtColumn" + i.ToString();
                                txt.Width = Unit.Pixel(150);
                                txt.ToolTip = "Enter " + captions[i, 0];

                                RequiredFieldValidator rfv = new RequiredFieldValidator();
                                rfv.ID = "rfv" + i.ToString();
                                rfv.Display = ValidatorDisplay.None;
                                rfv.ControlToValidate = "txtColumn" + i.ToString();
                                rfv.ErrorMessage = "Please Enter " + captions[i, 0]; 
                                rfv.ValidationGroup = "submit";
                                rfv.SetFocusOnError = true;
                                cell2.Controls.Add(rfv);

                                if (captions[i, 6].Equals("double"))
                                {
                                    CompareValidator cv = new CompareValidator();
                                    cv.ID = "cv" + i.ToString();
                                    cv.Display = ValidatorDisplay.None;
                                    cv.ControlToValidate = "txtColumn" + i.ToString();
                                    cv.ErrorMessage = "Please Enter Numbers Only for " + captions[i, 0];
                                    cv.Operator = ValidationCompareOperator.DataTypeCheck;
                                    cv.Type = ValidationDataType.Double;
                                    cv.ValidationGroup = "submit";
                                    cv.SetFocusOnError = true;
                                    cell2.Controls.Add(cv);
                                }
                                else if (captions[i, 6].Equals("string"))
                                {
                                    txt.MaxLength = int.Parse(captions[i, 7]);
                                    //Validation Not Required for :
                                    //- Blood Group
                                    //- Batch
                                    if (!_tablename.Equals("acd_bloodgrp") && !_tablename.Equals("acd_admbatch"))
                                    {
                                        RegularExpressionValidator rev = new RegularExpressionValidator();
                                        rev.ID = "rev" + i.ToString();
                                        rev.Display = ValidatorDisplay.None;
                                        rev.ControlToValidate = "txtColumn" + i.ToString();
                                        rev.ErrorMessage = "Please Enter Alphabets Only";
                                        rev.ValidationExpression = "^[a-zA-Z. ]+$";
                                        rev.ValidationGroup = "submit";
                                        rev.SetFocusOnError = true;
                                        cell2.Controls.Add(rev);
                                    }
                                }
                                else if (captions[i, 6].Equals("string2"))
                                {
                                    txt.MaxLength = int.Parse(captions[i, 7]);                                    
                                }
                                else if (captions[i, 6].Equals("numeric"))
                                {
                                    CompareValidator cv = new CompareValidator();
                                    cv.ID = "cv" + i.ToString();
                                    cv.Display = ValidatorDisplay.None;
                                    cv.ControlToValidate = "txtColumn" + i.ToString();
                                    cv.ErrorMessage = "Please Enter Numbers Only for " + captions[i, 0];
                                    cv.Operator = ValidationCompareOperator.DataTypeCheck;
                                    cv.Type = ValidationDataType.Integer;
                                    cv.SetFocusOnError = true;
                                    cv.ValidationGroup = "submit";
                                    cell2.Controls.Add(cv);
                                }

                                cell2.Controls.Add(txt);
                                row.Controls.Add(cell2);
                            }
                            else if (captions[i, 1].Equals("checkbox"))
                            {
                                HtmlTableCell cell2 = new HtmlTableCell();
                                cell2.Attributes.Add("class", "form_left_text");

                                CheckBox chk = new CheckBox();
                                chk.ID = "chkColumn" + i.ToString();
                                cell2.Controls.Add(chk);                                
                                row.Controls.Add(cell2);
                            }

                            tbl.Controls.Add(row);
                        }

                    }
                    catch (Exception ex)
                    {
                        //if (Convert.ToBoolean(Session["error"]) == true)
                        //    objUCommon.ShowError(Page, "Academic_MarkEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
                        //else
                        //    objUCommon.ShowError(Page, "Server Unavailable.");
                    }

                    return tbl;

                }

                public GridView CreateGridView(string where)
                {
                    SQLHelper objSH = new SQLHelper(_uaims_constr);

                    DataSet ds = AllMasters(_tablename.ToLower(),captions[0, 3] + ">0", captions[0, 3]);

                    GridView gv = new GridView();
                    gv.ID = "gvmaster";
                    gv.Width = Unit.Percentage(100);
                    gv.HeaderStyle.CssClass = "table table-bordered table-hovered";
                    gv.AutoGenerateColumns = false;

                    //Edit Button
                    ButtonField gvbut = new ButtonField();                    
                    gvbut.ButtonType = ButtonType.Image;
                    gvbut.ImageUrl = "~/images/edit.gif";
                    gvbut.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvbut.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                      

                    gvbut.HeaderText = "Action";
                    gv.Columns.Add(gvbut);
                    gv.Columns[0].HeaderStyle.Width = Unit.Percentage(5);

                    BoundField bfid = new BoundField();
                    bfid.HeaderText = "ID";
                    bfid.DataField = captions[0, 3];
                    bfid.HeaderStyle.Width = Unit.Percentage(5);
                    bfid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    bfid.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    gv.Columns.Add(bfid);

                    for (int i = 0; i < int.Parse(captions[0, 4]); i++)
                    {
                        BoundField bf = new BoundField();
                        bf.HeaderText = captions[i, 0];
                        bf.DataField = captions[i, 2];
                        gv.Columns.Add(bf);
                    }

                    //Added by Danish
                    gv.DataSource = ds;
                    gv.DataBind();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        gv.Rows[i].Cells[0].ToolTip = "Edit/Update";
                    }
                    //Added by Danish

                    //Added by Swapnil dated on 14092021 for master gridview table make thead and tbody seprate
                    if (gv.Rows.Count > 0)
                    {
                        //Adds THEAD and TBODY Section.
                        gv.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //Adds TH element in Header Row.  
                        gv.UseAccessibleHeader = true;


                    }
                    //End

                    return gv;
                }

                #region AllMasters

                public int  AddMaster(string tablename, string columnnames, string columnid, string columnvalues)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Masters
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TABLENAME", tablename);
                        objParams[1] = new SqlParameter("@P_COLUMNID", columnid );
                        objParams[2] = new SqlParameter("@P_COLUMNNAMES", columnnames );
                        objParams[3] = new SqlParameter("@P_COLUMNVALUES", columnvalues );
                        objParams[4] = new SqlParameter("@V_ID", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_MASTERS", objParams, true);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_MASTERS", objParams, true) != null)
                        if(ret !=null && ret.ToString()!="-1001")

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                         //Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = 0;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AddMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMaster(string tablename, string columnid, string columnnames)
                {
                   
                    int retStatus = 0;
                    try
                    {
                        
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        ////Add New Faq
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ID", columnid);
                        objParams[1] = new SqlParameter("@P_TABLENAME", tablename);
                        objParams[2] = new SqlParameter("@P_COLUMNNAMES", columnnames);
                        objParams[3] = new SqlParameter("@V_ID", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_UPD_MASTERS", objParams, true);
                        if (ret != null && ret.ToString() != "-1001")

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            
                    }
                    catch (Exception ex)
                    {
                       
                        retStatus = 0;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.UpdateMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet AllMasters(string tablename, string where, string orderby)
                {

                    DataSet ds = null;
                    try 
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TABLENAME", tablename);
                        
                        if (!where.Equals(string.Empty))
                            objParams[1] = new SqlParameter("@P_WHERE", where);
                        else
                            objParams[1] = new SqlParameter("@P_WHERE", DBNull.Value);

                        if (!orderby.Equals(string.Empty))
                            objParams[2] = new SqlParameter("@P_ORDERBY", orderby);
                        else
                            objParams[2] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MASTERS_SP_ALL_MASTERS", objParams);
                            
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AllMasters-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Quarters

                public DataSet AllQuarters()
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MASTERS_SP_ALL_PAY_QUARTERS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AllQuarters-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddQuarter(string qtrname, int qtrtypeno, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Quarter
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_QTRNAME", qtrname);
                        objParams[1] = new SqlParameter("@P_QRTTYPENO", qtrtypeno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[3] = new SqlParameter("@P_QTRNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_PAY_QUARTERS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AddQuarter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateQuarter(int qtrno, string qtrname, int qtrtypeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Quarter
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_QTRNO", qtrno);
                        objParams[1] = new SqlParameter("@P_QTRNAME", qtrname);
                        objParams[2] = new SqlParameter("@P_QRTTYPENO", qtrtypeno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_UPD_PAY_QUARTERS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.UpdateQuarter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Qualification

                public DataSet AllQualifications()
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MASTERS_SP_ALL_PAY_QUALIFICATION", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AllQualification-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddQualification(string qualiname, int qualilevelno, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Qualification
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_QUALI", qualiname);
                        objParams[1] = new SqlParameter("@P_QUALILEVELNO", qualilevelno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[3] = new SqlParameter("@P_QUALINO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_PAY_QUALIFICATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AddQualification-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateQualification(int qualino, string qualiname, int qualilevelno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Qualification
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_QUALINO", qualino);
                        objParams[1] = new SqlParameter("@P_QUALI", qualiname);
                        objParams[2] = new SqlParameter("@P_QUALILEVELNO", qualilevelno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_UPD_PAY_QUALIFICATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.UpdateQualification-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
            }
        }
    }
}