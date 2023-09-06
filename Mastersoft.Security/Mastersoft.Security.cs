using System;


namespace Mastersoft.Security.IITMS

{
    public static class SecurityThreads
    {

        //Check the multiple words not includes in Username and Passwords and also check the special
        //characters which are not allowed in Username and Password.
        // "-", the special character is removed from list because this character is matched with the Registration number (RegNo) field.
        // sanjay r new DLL build on dated 09 Aug 2023

        public static Boolean ValidInput(string Value)
        {
            bool ValueCount = true;
            string NotAllowed = @"><{}[]&()|*+%~\,;'=";
            try
            {
                for (var i = 0; i < Value.Length; i++)
                {

                    if (Value[i] == ' ' || Value[i] == '\r' || Value[i] == '\n' || i == Value.Length - 1)
                        ValueCount= false;
                    for (var j = 0; j < NotAllowed.Length; j++)
                    {
                        if (Value[i] == NotAllowed[j])
                        {
                            ValueCount = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ValueCount = false;
            }
            return ValueCount;
        }

        public static Boolean CheckSecurityInput(string userInput)
        {
            bool isSQLInjection = false;

            string[] sqlCheckList = {
                                                "--", ";--", ";",

                                                   "/*", "*/", "@@",

                                                   "alter", "begin",  "declare",

                                                   "varchar","nvarchar","nchar",

                                                   "create", "delete", "drop",  "insert", "select",

                                                   "cursor",  "exec",  "execute", "fetch", "kill",

                                                     "sysobjects", "syscolumns",

                                                    "table","update"

                                                    // "sys", "end","char", "cast",the keyword is removed from list because this keyword is matched with the username field.
                                                    // sanjay r new DLL build on dated 09 Aug 2023
                                            };

            string CheckString = userInput.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {
                if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    isSQLInjection = true;
                }
            }

            return isSQLInjection;
        }
    }
}
