using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


/// <summary>
/// Summary description for PageControlValidator
/// </summary>
/// 
namespace PageControlValidator
{

    public static class ValidateControls
    {
       
        public static string ValidateNumeric(string input)
        {
           
            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                return  "";
                
            }
            return "Please Enter Numeric Only ";
        }

        public static string ValidateAlpha(string input)
        {
            if (Regex.IsMatch(input, "^[a-zA-Z]+$"))
            {
                return "";

            }
            return "Please Enter Alphabets Only ";
           
        }

        public static string ValidateAlphaNumeric(string input)
        {
            if (Regex.IsMatch(input, "^[a-zA-Z0-9]+$"))
            {
                return "";

            }
            return "Please Enter Alphabets And Numeric Only ";
          
        }

        public static string ValidateAlphaNumericWithSpace(string input)
        {
            if (Regex.IsMatch(input, "^[a-zA-Z0-9 ]+$"))
            {
                return "";

            }
            return "Please Enter Alphabets And Numeric Only ";
           
        }

        public static string ValidateEmail(string input, int maxlength)
        {
            if (Regex.IsMatch(input, @"^([\w.\-]+)@(?:(?!(?:.*?[.]){3,})[a-zA-Z0-9\-]+(?:\.[a-zA-Z]{2,3}(?:\.[a-zA-Z]{2})?)?)$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
            }
            return "Please Enter Valid Email ID";
        }

        public static string ValidateMobile(string input, int maxlength)
        {
            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                if (input.Length == maxlength)
                {
                    return "";
                }
            }
            return "Please Enter Valid Mobile Number";
        }

        public static string ValidateTextBoxLength(string input, int maxlength)
        {
            if (input.Length <= maxlength)
            {
                return "";
            }
            return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
        }

        public static string ValidateAlphaWithLength(string input, int maxlength)
        {
            if (Regex.IsMatch(input, "^[a-zA-Z]+$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
                }
            }

            return "Please Enter Alphabets Only ";
        }

        public static string ValidateNumericWithLength(string input, int maxlength)
        {
           
            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
                }
            }

            return "Please Enter Numeric Only ";
        }

        public static string ValidateAlphaNumericWithLength(string input, int maxlength)
        {
            if (Regex.IsMatch(input, "^[a-zA-Z0-9]+$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
                }
            }
            return "Please Enter Alphabets And Numeric Only";
        }

        public static string ValidateAlphaWithLength(string input, int maxlength, bool allowspace)
        {
            if (Regex.IsMatch(input, @"/^[A-Za-z\s]*$/"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
                }
            }
            return "Please Enter Alphabets Only ";
        }


        // Added By Shrikant
        public static string ValidateAlphaWithLength(string input, bool isMandatory, int maxlength)
        {
            if (!isMandatory && string.IsNullOrWhiteSpace(input))
            {
                return ""; 
            }

            if (Regex.IsMatch(input, "^[a-zA-Z]+$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
                }
            }

            return "Please Enter Alphabets Only ";
        }

        public static string ValidateMobile(string input, bool isMandatory, int maxlength)
        {
            if (!isMandatory && string.IsNullOrWhiteSpace(input))
            {
                return ""; 
            }

            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                if (input.Length == maxlength)
                {
                    return "";
                }
            }
            return "Please Enter Valid Mobile Number";
        }

        public static string ValidateEmail(string input, bool isMandatory, int maxlength)
        {
            if (!isMandatory && string.IsNullOrWhiteSpace(input))
            {
                return ""; 
            }

            if (Regex.IsMatch(input, @"^([\w.\-]+)@(?:(?!(?:.*?[.]){3,})[a-zA-Z0-9\-]+(?:\.[a-zA-Z]{2,3}(?:\.[a-zA-Z]{2})?)?)$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
            }
            return "Please Enter Valid Email Id";
        }

        public static string ValidateAlphaNumericWithLength(string input, bool isMandatory, int maxlength)
        {
            if (string.IsNullOrEmpty(input) && !isMandatory)
            {
                return ""; 
            }

            if (Regex.IsMatch(input, "^[a-zA-Z0-9]+$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength;
                }
            }
            return "Please Enter Alphabets And Numeric Only";
        }

        public static string ValidateNumericWithLength(string input, bool isMandatory, int maxlength)
        {
            if (!isMandatory && string.IsNullOrWhiteSpace(input))
            {
                return ""; 
            }

            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                if (input.Length <= maxlength)
                {
                    return "";
                }
                else
                {
                    return "Entered Text Length Should Be Less Than Or Equal To " + maxlength + "";
                }
            }

            return "Please Enter Numeric Only ";
        }

        public static string ValidateAlphabetsWithSpaceAndLength(string input, bool isMandatory, int maxlength)
        {
            if (!isMandatory && string.IsNullOrWhiteSpace(input))
            {
                return ""; 
            }

            if (Regex.IsMatch(input, "^[a-zA-Z ]+$"))
            {
                return "";
            }

            return "Please Enter Alphabets Only ";
        }
    }
}