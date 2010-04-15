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
    private AutoResetEvent m_newItemEvent;
    private ManualResetEvent m_stopEvent;
    private WaitHandle[] m_waitables;
    private Thread m_thread;
    private HttpServerUtility m_server;

	public MatlabRunner(HttpServerUtility Server)
	{
        m_server = Server;
        m_matlab = new EngMATLib.EngMATAccess();
        m_queue = new Queue<FmriRequest>();

        m_newItemEvent = new AutoResetEvent(false);
        m_stopEvent = new ManualResetEvent(false);
        m_waitables = new WaitHandle[] { m_newItemEvent, m_stopEvent };

        m_thread = new Thread(WorkLoop);
        m_thread.Start();
	}

    ~MatlabRunner()
    {
        if (null != m_matlab)
        {
            m_matlab.Close();
        }

        m_stopEvent.Set();
        m_thread.Join(30000);
    }

    private void WorkLoop()
    {
        do
        {
            int index = WaitHandle.WaitAny(m_waitables);
            if( m_waitables[index] == m_stopEvent )
            {
                break;
            }
            
            while(HasRequests() && !m_stopEvent.WaitOne(0))
            {
                handleRequest(DequeueRequest());
            }
        } while(true);
    }

    public void handleRequest(FmriRequest req)
    {
        m_matlab.Evaluate("boolean_mtx = [1 1 0 1 0; 1 0 0 0 0; 0 1 0 1 1; 0 1 1 1 0; 1 0 1 0 0];");
        string corr_matrix_out_filename = String.Format("corr_matrix_out_filename = '{0}{1}.mat';", FmriCommon.getMatrixDir(m_server), req.AreaStringMD5);
        m_matlab.Evaluate(corr_matrix_out_filename);
        m_matlab.Evaluate("save(corr_matrix_out_filename, 'boolean_mtx');");

        string corr_image_out_filename = String.Format("corr_image_out_filename = '{0}{1}.png';", FmriCommon.getOutImageDir(m_server), req.AreaStringWithThresholdMD5);
        m_matlab.Evaluate(corr_image_out_filename);
        m_matlab.Evaluate("imwrite(boolean_mtx, corr_image_out_filename, 'BitDepth', 1);");
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
