#region Using

using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;



#endregion

/// <summary>
/// Summary description for QueryStringModule
/// </summary>
public class QueryStringModule : IHttpModule
{

    #region IHttpModule Members

    public void Dispose()
    {
        // Nothing to dispose
    }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    #endregion

    private const string PARAMETER_NAME = "enc=";
    private const string ENCRYPTION_KEY = "key";
    private const string PATH_PARAMETER = "iitms";


    void context_BeginRequest(object sender, EventArgs e)
    {
        try
        {
            string decryptedQuery = string.Empty;
            string query1 = string.Empty;
            HttpContext context = HttpContext.Current;

            if (!context.Request.Url.OriginalString.ToUpper().Contains("ITLE/MATHEDITOR.ASPX"))
            {
                //if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
                if ((context.Request.Url.OriginalString.Contains("iitms") && context.Request.RawUrl.Contains("?")) || context.Request.Url.OriginalString.Contains("aspx") || context.Request.Url.OriginalString.Contains("iitms"))
                //Server.URLEncode(string) 
                //if (context.Request.Url.PathAndQuery.Contains("PresentationLayer") && context.Request.RawUrl.Contains("?"))
                {
                    string query = ExtractQuery(context.Request.RawUrl);
                    if (context.Request.HttpMethod == "POST")
                    {
                        if (query.Contains("pageno=") || query.Contains("IsReset="))
                        {
                            query1 = Encrypt(query);
                            query = query1.Substring(query1.LastIndexOf("?") + 1);
                        }
                        else if (query.Contains(".aspx") || query.Contains("co") || query.Contains("in") || query.Contains("%"))
                        {
                            query = PARAMETER_NAME;
                        }
                    }
                    string path = GetVirtualPath();

                    if (query.StartsWith(PARAMETER_NAME, StringComparison.OrdinalIgnoreCase) || query.Contains(PATH_PARAMETER))
                    {
                        // Decrypts the query string and rewrites the path.
                        string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
                        //string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
                        if ((!rawQuery.Contains("iitms")) && (rawQuery != ""))
                        {
                            decryptedQuery = Decrypt(rawQuery);
                        }

                        if (decryptedQuery == "/")
                        {

                        }
                        //if (rawQuery.Contains("in") &&  rawQuery.Contains("/"))  //
                        //{
                        //    decryptedQuery = "";
                        //}

                        string rawpath = path.Replace(PATH_PARAMETER, string.Empty);

                        string decryptpath = Decrypt(rawpath).Trim();
                        if (decryptpath.Contains("iitms"))
                        {
                            decryptpath = decryptpath.Replace(PATH_PARAMETER, string.Empty);
                            decryptpath = Decrypt(decryptpath).Trim();
                        }
                        //string pathurl = HttpContext.Current.Request.RawUrl.Replace("%20", "+");
                        string pathurl = HttpContext.Current.Request.RawUrl;
                        pathurl = pathurl.Substring(0, pathurl.IndexOf("iitms"));
                        string newurl = pathurl + "" + decryptpath;
                        context.RewritePath(newurl, string.Empty, decryptedQuery);

                    }
                    else if (context.Request.HttpMethod == "GET")
                    {
                        if (path == "")
                        {
                            path = "default.aspx";
                        }
                        string encryptedQuery = Encrypt(query);
                        string path123 = Encrypt1(path);
                        if (path.Contains("iitms"))
                        {
                            path123 = path;
                        }
                        context.Response.Redirect(path123 + encryptedQuery);
                    }
                    //else
                    //{
                    //    string rawpath = path.Replace(PATH_PARAMETER, string.Empty);
                    //    string decryptpath = Decrypt(rawpath);

                    //    string pathurl = HttpContext.Current.Request.RawUrl;
                    //    pathurl = pathurl.Substring(0, pathurl.IndexOf("aspx"));
                    //    string newurl = pathurl + "" + decryptpath;
                    //    context.RewritePath(newurl);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            //HttpContext.Current.Response.End();

            //throw new ExceptionTranslater(ex);
        }
    }

    /// <summary>
    /// Parses the current URL and extracts the virtual path without query string.
    /// </summary>
    /// <returns>The virtual path of the current URL.</returns>
    private static string GetVirtualPath()
    {
        string path = HttpContext.Current.Request.RawUrl;
        string pathstring = string.Empty;
        if (path.Contains("?"))
        {
            path = path.Substring(0, path.IndexOf("?"));
        }
        pathstring = path;
        pathstring = pathstring.Substring(pathstring.LastIndexOf("/") + 1);
        if (pathstring.Contains(".aspx"))
        {
            path = path.Substring(path.LastIndexOf("/") + 1);
        }
        else
        {
            if (path.Contains("iitms"))
            {
                path = path.Substring(path.LastIndexOf("iitms"));

            }
            else

                path = path.Substring(path.LastIndexOf("/") + 1);
            //path = "a" + path; 


        }

        // path = path.Substring(path.LastIndexOf("/") + 1);
        //if ("0" == path)
        //{
        //    return string.Empty;
        //}
        //else        
        return path;
    }

    /// <summary>
    /// Parses a URL and returns the query string.
    /// </summary>
    /// <param name="url">The URL to parse.</param>
    /// <returns>The query string without the question mark.</returns>
    private static string ExtractQuery(string url)
    {
        //url.Contains("%20")

        string url1 = url.Replace("%20", "+");
        int index = url1.IndexOf("?") + 1;
        //int index = url.LastIndexOf("/") + 1;     
        return url1.Substring(index);
    }

    #region Encryption/decryption

    /// <summary>
    /// The salt value used to strengthen the encryption.
    /// </summary>
    private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());

    /// <summary>
    /// Encrypts any string using the Rijndael algorithm.
    /// </summary>
    /// <param name="inputText">The string to encrypt.</param>
    /// <returns>A Base64 encrypted string.</returns>
    public static string Encrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();

                    return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }

    public static string Encrypt1(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);

        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();

                    return PATH_PARAMETER + Convert.ToBase64String(memoryStream.ToArray());
                    //return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// Decrypts a previously encrypted string.
    /// </summary>
    /// <param name="inputText">The encrypted string to decrypt.</param>
    /// <returns>A decrypted string.</returns>
    public static string Decrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] encryptedData = Convert.FromBase64String(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

        using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                }
            }
        }
    }

    public static string Decrypt2(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        //byte[] encryptedData = Convert.FromBase64String(inputText);
        byte[] encryptedData = Convert.FromBase64String(inputText.Replace(" ", "+"));
        // byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        //  byte[] plainText = UTF8Encoding.UTF8.GetBytes(inputText);
        // byte[] plainText = UTF8Encoding.UTF8.GetBytes(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);


        using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);

                    //cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                    //cryptoStream.FlushFinalBlock();
                    //return System.Text.Encoding.Default.GetString(memoryStream.ToArray()); ;

                    //byte[] plainText = new byte[inputText.Length];
                    //int plainByteCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    //string plainText1 = Encoding.UTF8.GetString(plainText, 0, plainByteCount);

                    //return plainText1;
                    //var test= Encoding.Unicode.GetString(plainText);
                    //return test;

                }

            }
        }

        //byte[] sourceByte = System.Text.Encoding.UTF8.GetBytes(inputText);
        //MemoryStream memdata = new MemoryStream(sourceByte);
        // DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        //CryptoStream cs = new CryptoStream(memdata,
        //des.CreateDecryptor(des.Key, des.IV), CryptoStreamMode.Read);
        //byte[] trival = new byte[memdata.Capacity];
        //cs.Read(trival, 0, trival.Length);
        //cs.Close();
        //memdata.Close();
        //return new UTF8Encoding().GetString(trival);

    }
    #endregion
}



////using System;
////using System.Web;
////using System.Web.Security;
////using System.Web.SessionState;
////using System.Diagnostics;

////// This code demonstrates how to make session state available in HttpModule,
////// regradless of requested resource.
////// author: Tomasz Jastrzebski

////public class MyHttpModule : IHttpModule
////{
////   public void Init(HttpApplication application)
////   {
////      application.PostAcquireRequestState += new EventHandler(Application_PostAcquireRequestState);
////      application.PostMapRequestHandler += new EventHandler(Application_PostMapRequestHandler);
////   }

////   void Application_PostMapRequestHandler(object source, EventArgs e)
////   {
////      HttpApplication app = (HttpApplication)source;

////      if (app.Context.Handler is IReadOnlySessionState || app.Context.Handler is IRequiresSessionState) {
////         // no need to replace the current handler
////         return;
////      }

////      // swap the current handler
////      app.Context.Handler = new MyHttpHandler(app.Context.Handler);
////   }

////   void Application_PostAcquireRequestState(object source, EventArgs e)
////   {
////      HttpApplication app = (HttpApplication)source;

////      MyHttpHandler resourceHttpHandler = HttpContext.Current.Handler as MyHttpHandler;

////      if (resourceHttpHandler != null) {
////         // set the original handler back
////         HttpContext.Current.Handler = resourceHttpHandler.OriginalHandler;
////      }

////      // -> at this point session state should be available

////      Debug.Assert(app.Session != null, "it did not work :(");
////   }

////   public void Dispose()
////   {

////   }

////   // a temp handler used to force the SessionStateModule to load session state
////   public class MyHttpHandler : IHttpHandler, IRequiresSessionState
////   {
////      internal readonly IHttpHandler OriginalHandler;

////      public MyHttpHandler(IHttpHandler originalHandler)
////      {
////         OriginalHandler = originalHandler;
////      }

////      public void ProcessRequest(HttpContext context)
////      {
////         // do not worry, ProcessRequest() will not be called, but let's be safe
////         throw new InvalidOperationException("MyHttpHandler cannot process requests.");
////      }

////      public bool IsReusable
////      {
////         // IsReusable must be set to false since class has a member!
////         get { return false; }
////      }
////   }
////}

