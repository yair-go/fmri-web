using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class FmriCommon
{
    private static System.IO.StreamWriter fs;

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

    public static bool isOutImageExists(string md5, HttpServerUtility Server)
    {
        return System.IO.File.Exists(FmriCommon.getOutImageDir(Server) + md5 + ".png");
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

    public static void LogToFile(string str)
    {
        string s = String.Format("{0}\t{1}", DateTime.Now, str);
        fs.WriteLine(s);
        fs.Flush();
    }

    public static void LogToFile(string str, object o1)
    {
        LogToFile(String.Format(str, o1));
    }

    public static void LogToFile(string str, object o1, object o2)
    {
        LogToFile(String.Format(str, o1, o2));
    }

    public static void LogToFile(string str, object o1, object o2, object o3)
    {
        object[] o = { o1, o2, o3 };
        LogToFile(String.Format(str, o));
    }

    public static void createLogStream(string path)
    {
        fs = new System.IO.StreamWriter(path, true);
    }

    public static MatlabRunner getMatlabRunner(HttpApplicationState Application, HttpServerUtility Server)
    {
        MatlabRunner m = null;
        m = (MatlabRunner)Application.Get("MatlabRunner");

        if( null == m )
        {
            m = new MatlabRunner(Server);
            Application.Set("MatlabRunner", m);
        }

        return m;
    }
}
