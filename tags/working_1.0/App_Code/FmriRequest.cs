using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FmriRequest
/// </summary>
public class FmriRequest
{
    private string m_imageName;
    private int m_x1, m_x2, m_y1, m_y2, m_z1, m_z2;
    private double m_threshold;
    private DateTime m_timeSubmitted, m_timeExecuted;
    private string m_result;


    public FmriRequest()
    {
        //
    }

    public FmriRequest(string imageName, int x1, int x2, int y1, int y2, int z1, int z2, double threshold)
    {
        ImageName = imageName;
        X1 = x1;
        X2 = x2;
        Y1 = y1;
        Y2 = y2;
        Z1 = z1;
        Z2 = z2;
        Threshold = threshold;
        m_timeSubmitted = DateTime.Now;
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
    
    public double Threshold
    {
        get { return m_threshold; }
        set { m_threshold = value; }
    }

    public string AreaString
    {
        get { return String.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}", ImageName, X1, X2, Y1, Y2, Z1, Z2); }
    }

    public string AreaStringMD5
    {
        get { return FmriCommon.md5(AreaString); }
    }

    public string AreaStringWithThreshold
    {
        get { return String.Format("{0}_{1}", AreaString, Threshold); }
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



}
