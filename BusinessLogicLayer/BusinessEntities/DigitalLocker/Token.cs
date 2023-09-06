using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Token
            {
                public string access_token
                {
                    get;
                    set;
                }
                public string token_type
                {
                    get;
                    set;
                }
                public string expires_in
                {
                    get;
                    set;
                }
                public string refresh_token
                {
                    get;
                    set;
                }
            }
            
            public class UserDetails
            {
                public string digilockerid
                {
                    get;
                    set;
                }
                public string name
                {
                    get;
                    set;
                }
                public string dob
                {
                    get;
                    set;
                }
                public string gender
                {
                    get;
                    set;
                }
            }
           
            public class Item
            {
                public string name
                {
                    get;
                    set;
                }
                public string type
                {
                    get;
                    set;
                }
                public string size
                {
                    get;
                    set;
                }
                public string date
                {
                    get;
                    set;
                }
                public string parent
                {
                    get;
                    set;
                }
                public List<string> mime
                {
                    get;
                    set;
                }
                public string uri
                {
                    get;
                    set;
                }
                public string doctype
                {
                    get;
                    set;
                }
                public string description
                {
                    get;
                    set;
                }
                public string issuerid
                {
                    get;
                    set;
                }
                public string issuer
                {
                    get;
                    set;
                }
            }

            public class Root
            {
                public List<Item> items
                {
                    get;
                    set;
                }
                public string resource
                {
                    get;
                    set;
                }
            }
            
            public class ContentDisposition
            {
                private static readonly Regex regex = new Regex(
                    "^([^;]+);(?:\\s*([^=]+)=((?<q>\"?)[^\"]*\\k<q>);?)*$",
                    RegexOptions.Compiled
                );

                private readonly string fileName;
                private readonly StringDictionary parameters;
                private readonly string type;

                public ContentDisposition(string s)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        throw new ArgumentNullException("s");
                    }
                    Match match = regex.Match(s);
                    if (!match.Success)
                    {
                        throw new FormatException("input is not a valid content-disposition string.");
                    }
                    var typeGroup = match.Groups[1];
                    var nameGroup = match.Groups[2];
                    var valueGroup = match.Groups[3];

                    int groupCount = match.Groups.Count;
                    int paramCount = nameGroup.Captures.Count;

                    this.type = typeGroup.Value;
                    this.parameters = new StringDictionary();

                    for (int i = 0; i < paramCount; i++)
                    {
                        string name = nameGroup.Captures[i].Value;
                        string value = valueGroup.Captures[i].Value;

                        if (name.Equals("filename", StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.fileName = value;
                        }
                        else
                        {
                            this.parameters.Add(name, value);
                        }
                    }
                }
                public string FileName
                {
                    get
                    {
                        return this.fileName;
                    }
                }
                public StringDictionary Parameters
                {
                    get
                    {
                        return this.parameters;
                    }
                }
                public string Type
                {
                    get
                    {
                        return this.type;
                    }
                }
            } 
        }
    }
}
