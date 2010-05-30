using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Summary description for MatlabRunner
/// </summary>
public class MatlabRunner
{
    private EngMATLib.EngMATAccess m_matlab;
    private Queue<FmriRequest> m_queue;
    private List<FmriRequest> m_doneList;
    private AutoResetEvent m_newItemEvent;
    private ManualResetEvent m_stopEvent;
    private WaitHandle[] m_waitables;
    private Thread m_thread;
    private HttpServerUtility m_server;
    private HttpApplicationState m_app;
    private FmriRequest m_currentRequest;

	public MatlabRunner(HttpServerUtility Server, HttpApplicationState Application)
	{
        m_server = Server;
        m_app = Application;
        
        m_matlab = new EngMATLib.EngMATAccess();
        m_matlab.Evaluate("cd('" + m_server.MapPath("App_Data") + "');");
            
        m_queue = new Queue<FmriRequest>();
        m_doneList = new List<FmriRequest>();

        m_newItemEvent = new AutoResetEvent(false);
        m_stopEvent = new ManualResetEvent(false);
        m_waitables = new WaitHandle[] { m_newItemEvent, m_stopEvent };

        m_app["ReqHist"] = readFromHistory();

        m_thread = new Thread(WorkLoop);
        m_thread.Start();
	}

    ~MatlabRunner()
    {
        writeToHistory();

        FmriCommon.LogToFile("MatlabRunner - dtor started.");
        
        if (null != m_matlab)
        {
            m_matlab.Close();
        }

        m_stopEvent.Set();
        m_thread.Join(30000);
        FmriCommon.LogToFile("MatlabRunner - dtor ends.");
        
    }

    private void WorkLoop()
    {
        FmriCommon.LogToFile("MatlabRunner - worker thread started.");

        try
        {
            do
            {
                int index = WaitHandle.WaitAny(m_waitables);
                if (m_waitables[index] == m_stopEvent)
                {
                    break;
                }

                while (HasRequests() && !m_stopEvent.WaitOne(0))
                {
                    handleRequest(DequeueRequest());
                }
            } while (true);
        }
        catch (ThreadAbortException e)
        {
            FmriCommon.LogToFile("MatlabRunner - worker thread aborted!");
        }
    }

    public void RunMatlabAndLog(string cmd)
    {
        m_matlab.Evaluate(cmd);
        string res = m_matlab.LastResult;
        if (res.Trim() != "")
            FmriCommon.LogToFile("MATLAB: " + res.Trim());
    }

    public void handleRequest(FmriRequest req)
    {
        FmriCommon.LogToFile("MatlabRunner - handling request: {0}", req.AreaStringWithThreshold);
        m_currentRequest = req;

        string mtx_filename = FmriCommon.getMatrixDir(m_server) + req.AreaStringMD5 + ".mat";
        string xls_filename = FmriCommon.getExcelDir(m_server) + req.AreaStringMD5 + ".csv";
        string zip_filename = FmriCommon.getExcelDir(m_server) + req.AreaStringMD5 + ".zip";
        
        try
        {
            
            if (!System.IO.File.Exists(mtx_filename))
            {
                RunMatlabAndLog("should_calc_corr_matrix = 1;");
                RunMatlabAndLog("src_image_filename = '" + FmriCommon.getSrcImageDir(m_server) + req.ImageName + "';");
                object[] range = { req.X1, req.X2, req.Y1, req.Y2, req.Z1, req.Z2, req.T1, req.T2 };
                RunMatlabAndLog(String.Format("x1={0};x2={1};y1={2};y2={3};z1={4};z2={5};t1={6};t2={7};", range));
                RunMatlabAndLog("corr_matrix_out_filename = '" + mtx_filename + "';");
            }
            else
            {
                RunMatlabAndLog("should_calc_corr_matrix = 0;");
                RunMatlabAndLog("corr_matrix_in_filename = '" + mtx_filename + "';");
            }

            if (!System.IO.File.Exists(xls_filename))
            {
                RunMatlabAndLog("should_write_xls = 1;");
                RunMatlabAndLog("xls_out_filename = '" + xls_filename + "';");
                RunMatlabAndLog("zip_out_filename = '" + zip_filename + "';");
            }
            else
            {
                RunMatlabAndLog("should_write_xls = 0;");
            }

            RunMatlabAndLog("threshold = " + Convert.ToString(req.Threshold) + ";");

            string out_image_filename = FmriCommon.getOutImageDir(m_server) + req.AreaStringWithThresholdMD5 + ".png";
            RunMatlabAndLog("corr_image_out_filename = '" + out_image_filename + "';");

            // Finished initializing variables. Run the MATLAB script now!
            req.executedNow();
            RunMatlabAndLog("analyze;");
            req.Result = m_matlab.LastResult;
        }
        catch (Exception e)
        {
            req.Result = e.Message;
            FmriCommon.LogToFile("MatlabRunner - exception caught: {0} [{1}]", e.Message, e.StackTrace);
        }
        finally
        {
            m_currentRequest = null;
        }

        try
        {
            string clique_filename = FmriCommon.getCliquesDir(m_server) + req.AreaStringWithThresholdMD5 + ".txt";
            string jar_filename = FmriCommon.getJarFilename(m_server);

            object[] commandline = { jar_filename, xls_filename, req.Threshold, req.CS1, req.CS2, clique_filename };
            System.Diagnostics.ProcessStartInfo psinfo = new System.Diagnostics.ProcessStartInfo(
                "java",
                String.Format("-jar {0} {1} {2} {3} {4} {5}", commandline));
            psinfo.RedirectStandardError = true;
            psinfo.RedirectStandardOutput = true;
            psinfo.UseShellExecute = false;
            System.Diagnostics.Process java = System.Diagnostics.Process.Start(psinfo);
            FmriCommon.LogToFile("JAVA: started, PID=" + java.Id);
            java.WaitForExit();

            FmriCommon.LogToFile("JAVA: finished, ExitCode=" + java.ExitCode);
            string stdout = java.StandardOutput.ReadToEnd().Trim();
            string stderr = java.StandardError.ReadToEnd().Trim();
            
            if( stdout != "" && stderr != "" )
            {
                req.CliquesResult = stdout + ";\n" + stderr;
            }
            else if(stdout != "")
            {
                req.CliquesResult = stdout;
            }
            else
            {
                req.CliquesResult = stderr;
            }
            FmriCommon.LogToFile("JAVA: " + req.CliquesResult);            
        }
        catch (Exception e)
        {
            req.CliquesResult = e.Message;
            FmriCommon.LogToFile("MatlabRunner - exception caught (cliques): {0} [{1}]", e.Message, e.StackTrace);
        }
        
        lock (m_doneList)
        {
            m_doneList.Add(req);
        }

        FmriCommon.LogToFile("MatlabRunner - request finished.");
    }

    public List<FmriRequest> GetDoneList()
    {
        FmriRequest[] doneArray;
        lock (m_doneList)
        {
            doneArray = new FmriRequest[m_doneList.Count];
            m_doneList.CopyTo(doneArray);
        }
        return new List<FmriRequest>(doneArray);
    }

    public List<FmriRequest> GetQueueList()
    {
        FmriRequest[] queueArray;
        lock (m_queue)
        {
            queueArray = new FmriRequest[m_queue.Count];
            m_queue.CopyTo(queueArray, 0);
        }
        return new List<FmriRequest>(queueArray);
    }
    
    public void EnqueueRequest(FmriRequest req)
    {
        lock (m_queue)
        {
            m_queue.Enqueue(req);
        }
        m_newItemEvent.Set();
    }

    private bool HasRequests()
    {
        lock (m_queue)
        {
            return m_queue.Count > 0;
        }
    }

    private FmriRequest DequeueRequest()
    {
        lock (m_queue)
        {
            return m_queue.Dequeue();
        }
    }

    public FmriRequest CurrentRequest
    {
        get { return m_currentRequest; }
    }

    public List<FmriRequest> readFromHistory()
    {
        List<FmriRequest> ret = new List<FmriRequest>();

        try
        {
            FileStream fstream = new FileStream(m_server.MapPath("App_Data\\history.bin"), FileMode.Open);
            IFormatter formatter = new BinaryFormatter();

            while (fstream.Position + 4 < fstream.Length)
            {
                object o = null;
                try
                {
                    o = formatter.Deserialize(fstream);
                    ret.Add((FmriRequest)o);
                }
                catch (InvalidCastException e)
                {
                    FmriCommon.LogToFile("Exception in readFromHistory ({0}): {1} [{2}]", o.ToString(), e.Message, e.StackTrace);
                }
            }

            fstream.Close();
        }
        catch (System.IO.FileNotFoundException e)
        {

        }
        catch (Exception e)
        {
            FmriCommon.LogToFile("Exception in readFromHistory: {0} [{1}]", e.Message, e.StackTrace);
        }


        return ret;
    }

    public void writeToHistory()
    {
        try
        {
            FileStream fstream = new FileStream(m_server.MapPath("App_Data\\history.bin"), FileMode.Append);
            IFormatter formatter = new BinaryFormatter();

            foreach (FmriRequest req in GetDoneList())
            {
                formatter.Serialize(fstream, req);
            }

            fstream.Close();
        }
        catch (Exception e)
        {
            FmriCommon.LogToFile("Exception in writeToHistory {0} [{1}]", e.Message, e.StackTrace);
        }
    }
}
