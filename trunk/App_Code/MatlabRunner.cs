using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MatlabRunner
/// </summary>
public class MatlabRunner
{
    private EngMATLib.EngMATAccess m_matlab;

	public MatlabRunner()
	{
        m_matlab = new EngMATLib.EngMATAccess();
        m_matlab.Evaluate("a = 5");
        string l = m_matlab.LastResult;
	}

    ~MatlabRunner()
    {
        if (null != m_matlab)
        {
            m_matlab.Close();
        }
    }

    public void PostRequest(FmriRequest req, HttpServerUtility Server)
    {
        string mtxFileNameCommand = String.Format("matrixOutFileName = '{0}{1}';", FmriCommon.getMatrixDir(Server), req.AreaStringMD5);
        m_matlab.Evaluate(mtxFileNameCommand);
        m_matlab.Evaluate("A = [1 2 3; 4 5 6]; save(matrixOutFileName);");
    }
}
