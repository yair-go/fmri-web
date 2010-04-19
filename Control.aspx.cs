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

    protected void Page_Load(object sender, EventArgs e)
    {
        MatlabRunner m = (MatlabRunner)Application["MatlabRunner"];
        
        List<FmriRequest> reqList;

        reqList = m.GetQueueList();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            
            AddCells(r.Cells, req);

            tblQueue.Rows.Add(r);
        }

        reqList = m.GetDoneList();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            TableCell c;

            AddCells(r.Cells, req);

            c = new TableCell();
            c.Text = Convert.ToString(req.TimeExecuted);
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<a href=\"Results.aspx?id=" + req.AreaStringWithThresholdMD5 + "\">Results...</a>";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = req.Result;
            r.Cells.Add(c);
            

            tblDone.Rows.Add(r);
        }

        FmriRequest currReq = ((MatlabRunner)Application["MatlabRunner"]).CurrentRequest;
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
}
