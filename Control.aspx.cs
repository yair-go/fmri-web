using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        c.Text = Convert.ToString(req.TimeSubmitted);
        cells.Add(c);
    }

    protected void handleDone(List<FmriRequest> reqList, Table dest)
    {
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            TableCell c;

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
                c.ForeColor = System.Drawing.Color.Red;
            }
            c.Text = req.Result;
            r.Cells.Add(c);
            
            if (req.Result.Trim() == "OK")
            {
                c = new TableCell();
                c.Text = "<a href=\"Results.aspx?id=" + req.AreaStringWithThresholdMD5 + "&id2=" + req.AreaStringMD5 + "\">Results...</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"Excel/" + req.AreaStringMD5 + ".csv\">Excel</a>, <a href=\"Excel/" + req.AreaStringMD5 + ".zip\">Zipped</a>";
                r.Cells.Add(c);    
            }


            dest.Rows.Add(r);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MatlabRunner m = FmriCommon.getMatlabRunner(Application, Server);
        
        List<FmriRequest> reqList;

        reqList = m.GetQueueList();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            
            AddCells(r.Cells, req);

            tblQueue.Rows.Add(r);
        }

        handleDone(m.GetDoneList(), tblDone);
        handleDone( (List<FmriRequest>)Application["ReqHist"], tblHistory);       

        FmriRequest currReq = m.CurrentRequest;
        if (currReq != null)
        {
           TableRow r = new TableRow();
           TableCell c;

           AddCells(r.Cells, currReq);

           c = new TableCell();
           c.Text = Convert.ToString(currReq.TimeExecuted);
           r.Cells.Add(c);

           tblCurrent.Rows.Add(r);
        }
        else
        {
           pnlCurrent.Visible = false;
        }

    }
    protected void lnkSvnUpdate_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("svn", "update \"" + Server.MapPath("") + "\"").WaitForExit();
        Response.Redirect("Control.aspx");
    }
}
