using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class FmriCommon
{
    public static string getSrcImageDir(HttpServerUtility Server)
    {
        return Server.MapPath(@"App_Data\Images") + "\\";
    }

    public static string getMatrixDir(HttpServerUtility Server)
    {
        return Server.MapPath(@"App_Data\Matrix") + "\\";
    }

    public static string getOutImageDir(HttpServerUtility Server)
    {
        return Server.MapPath(@"Results") + "\\";
    }

    public static string md5(string input)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }
}
