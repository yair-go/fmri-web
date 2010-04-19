using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

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

	public MatlabRunner(HttpServerUtility Server)
	{
        m_server = Server;
        
        m_matlab = new EngMATLib.EngMATAccess();
        m_matlab.Evaluate("cd('" + m_server.MapPath("App_Data") + "');");
            
        m_queue = new Queue<FmriRequest>();
        m_doneList = new List<FmriRequest>();

        m_newItemEvent = new AutoResetEvent(false);
        m_stopEvent = new ManualResetEvent(false);
        m_waitables = new WaitHandle[] { m_newItemEvent, m_stopEvent };

        m_thread = new Thread(WorkLoop);
        m_thread.Start();
	}

    ~MatlabRunner()
    {
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

    public void handleRequest(FmriRequest req)
    {
        FmriCommon.LogToFile("MatlabRunner - handling request: {0}", req.AreaStringWithThreshold);
        
        try
        {
            string mtx_filename = FmriCommon.getMatrixDir(m_server) + req.AreaStringMD5 + ".mat";

            if (!System.IO.File.Exists(mtx_filename))
            {
                m_matlab.Evaluate("should_calc_corr_matrix = 1;");
                m_matlab.Evaluate("src_image_filename = '" + FmriCommon.getSrcImageDir(m_server) + req.ImageName + "';");
                object[] range = { req.X1, req.X2, req.Y1, req.Y2, req.Z1, req.Z2 };
                m_matlab.Evaluate(String.Format("x1={0};x2={1};y1={2};y2={3};z1={4};z2={5};", range));
                m_matlab.Evaluate("corr_matrix_out_filename = '" + mtx_filename + "';");
            }
            else
            {
                m_matlab.Evaluate("should_calc_corr_matrix = 0;");
                m_matlab.Evaluate("corr_matrix_in_filename = '" + mtx_filename + "';");
            }

            m_matlab.Evaluate("threshold = " + Convert.ToString(req.Threshold) + ";");

            string out_image_filename = FmriCommon.getOutImageDir(m_server) + req.AreaStringWithThresholdMD5 + ".png";
            m_matlab.Evaluate("corr_image_out_filename = '" + out_image_filename + "';");


            // Finished initializing variables. Run the MATLAB script now!
            req.executedNow();
            m_matlab.Evaluate("analyze;");
            req.Result = m_matlab.LastResult;
            
            FmriCommon.LogToFile("MatlabRunner - matlab done: {0}", req.Result);
           
        }
        catch (Exception e)
        {
            req.Result = e.Message;
            FmriCommon.LogToFile("MatlabRunner - exception caught: {0} [{1}]", e.Message,  e.StackTrace);
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
}
