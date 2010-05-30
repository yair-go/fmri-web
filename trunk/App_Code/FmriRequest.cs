using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class FmriRequest
{
    private string m_imageName;
    private int m_x1, m_x2, m_y1, m_y2, m_z1, m_z2;
    private double m_threshold;
    private int m_t1, m_t2, m_cs1, m_cs2;
    private DateTime m_timeSubmitted, m_timeExecuted;
    private string m_result;
    private string m_ipAddress;
    private string m_cliquesResult;

    public FmriRequest(string imageName, int x1, int x2, int y1, int y2, int z1, int z2, double threshold,
        int t1, int t2, int cs1, int cs2, string ipAddress)
    {
        ImageName = imageName;
        X1 = x1;
        X2 = x2;
        Y1 = y1;
        Y2 = y2;
        Z1 = z1;
        Z2 = z2;
        T1 = t1;
        T2 = t2;
        CS1 = cs1;
        CS2 = cs2;
        Threshold = threshold;
        m_timeSubmitted = DateTime.Now;
        IPAddress = m_ipAddress;
    }

    public string ImageName
    {
        get { return m_imageName; }
        set { m_imageName = value; }
    }

    public int X1
    {
        get { return m_x1; }
        set { m_x1 = value; }
    }

    public int X2
    {
        get { return m_x2; }
        set { m_x2 = value; }
    }

    public int Y1
    {
        get { return m_y1; }
        set { m_y1 = value; }
    }

    public int Y2
    {
        get { return m_y2; }
        set { m_y2 = value; }
    }

    public int Z1
    {
        get { return m_z1; }
        set { m_z1 = value; }
    }

    public int Z2
    {
        get { return m_z2; }
        set { m_z2 = value; }
    }

    public string IPAddress
    {
        get { return m_ipAddress; }
        set { m_ipAddress = value; }
    }

    public double Threshold
    {
        get { return m_threshold; }
        set { m_threshold = value; }
    }

    public int T1
    {
        get { return m_t1; }
        set { m_t1 = value; }
    }

    public int T2
    {
        get { return m_t2; }
        set { m_t2 = value; }
    }

    public int CS1
    {
        get { return m_cs1; }
        set { m_cs1 = value; }
    }

    public int CS2
    {
        get { return m_cs2; }
        set { m_cs2 = value; }
    }
    
    public string AreaString
    {
        get { return String.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}_{8}", ImageName, X1, X2, Y1, Y2, Z1, Z2, T1, T2); }
    }

    public string AreaStringMD5
    {
        get { return FmriCommon.md5(AreaString); }
    }

    public string AreaStringWithThreshold
    {
        get { return String.Format("{0}_{1}_{2}_{3}", AreaString, Threshold, CS1, CS2); }
    }

    public string AreaStringWithThresholdMD5
    {
        get { return FmriCommon.md5(AreaStringWithThreshold); }
    }

    public DateTime TimeSubmitted
    {
        get { return m_timeSubmitted; }
    }

    public void executedNow()
    {
        m_timeExecuted = DateTime.Now;
    }

    public DateTime TimeExecuted
    {
        get { return m_timeExecuted; }
    }

    public string Result
    {
        get { return m_result; }
        set { m_result = value; }
    }

    public string CliquesResult
    {
        get { return m_cliquesResult; }
        set { m_cliquesResult = value; }
    }



}
