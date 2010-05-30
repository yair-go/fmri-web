using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Control : System.Web.UI.Page
{
    private void AddCells(TableCellCollection cells, FmriRequest req)
    {
        TableCell c;

        c = new TableCell();
        c.Text = req.ImageName;
        cells.Add(c);

        c = new TableCell();
        c.Text = "" + req.X1 + "-" + req.X2;
        cells.Add(c);

        c = new TableCell();
        c.Text = "" + req.Y1 + "-" + req.Y2;
        cells.Add(c);

        c = new TableCell();
        c.Text = "" + req.Z1 + "-" + req.Z2;
        cells.Add(c);

        c = new TableCell();
        c.Text = Convert.ToString(req.Threshold);
        cells.Add(c);

        c = new TableCell();
        c.Text = "" + req.T1 + "-" + req.T2;
        cells.Add(c);

        c = new TableCell();
        c.Text = "" + req.CS1 + "-" + req.CS2;
        cells.Add(c);

        c = new TableCell();
        c.Text = req.IPAddress;
        cells.Add(c);
        
        c = new TableCell();
        c.Text = Convert.ToString(req.TimeSubmitted);
        cells.Add(c);
    }

    protected void handleDone(List<FmriRequest> reqList, Table dest, string status, Color bg, Color fg)
    {
        reqList.Reverse();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            r.Height = 40;
            
            TableCell c;

            c = new TableCell();
            c.Text = status;
            c.BackColor = bg;
            c.ForeColor = fg;
            r.Cells.Add(c);
            
            AddCells(r.Cells, req);

            c = new TableCell();
            c.Text = Convert.ToString(req.TimeExecuted);
            r.Cells.Add(c);

            c = new TableCell();
            if (req.Result.Contains("Out of memory"))
            {
                req.Result = "Out of memory.";
            }
            if (req.Result.Trim() != "OK")
            {
                c.ForeColor = Color.Red;
            }
            c.Text = req.Result;
            r.Cells.Add(c);
            
            if (req.Result.Trim() == "OK")
            {
                c = new TableCell();
                c.Text = "<a href=\"Results.aspx?id=" + req.AreaStringWithThresholdMD5 + "&id2=" + req.AreaStringMD5 + "\" class=\"small orange awesome\">Results</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"Excel/" + req.AreaStringMD5 + ".csv\"\" class=\"small awesome\">Excel</a>&nbsp;&nbsp;<a href=\"Excel/" + req.AreaStringMD5 + ".zip\"\" class=\"small awesome\">Zipped</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"Cliques/" + req.AreaStringWithThresholdMD5 + ".txt\"\" class=\"small awesome\">Cliques</a>";
                r.Cells.Add(c);
            }


            dest.Rows.Add(r);
        }
        reqList.Reverse();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MatlabRunner m = FmriCommon.getMatlabRunner(Application, Server);
        
        List<FmriRequest> reqList;

        reqList = m.GetQueueList();
        reqList.Reverse();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            r.Height = 40;
            
            TableCell c;

            c = new TableCell();
            c.Text = "Queue";
            c.BackColor = Color.Orange;
            r.Cells.Add(c);
            
            AddCells(r.Cells, req);

            tblReq.Rows.Add(r);
        }
        reqList.Reverse();

        
        FmriRequest currReq = m.CurrentRequest;
        if (currReq != null)
        {
            TableRow r = new TableRow();
            r.Height = 40;
            
            TableCell c;

            c = new TableCell();
            c.Text = "In Progress";
            c.BackColor = Color.DarkGreen;
            c.ForeColor = Color.White;
            r.Cells.Add(c);

            AddCells(r.Cells, currReq);

            c = new TableCell();
            c.Text = Convert.ToString(currReq.TimeExecuted);
            r.Cells.Add(c);

            tblReq.Rows.Add(r);
        }

        handleDone(m.GetDoneList(), tblReq, "Finished", Color.DarkSlateBlue, Color.White);
        handleDone((List<FmriRequest>)Application["ReqHist"], tblReq, "History", Color.LightGray, Color.Black);       


    }
    protected void lnkSvnUpdate_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("svn", "update \"" + Server.MapPath("") + "\"").WaitForExit();
        Response.Redirect("Control.aspx");
    }
}
