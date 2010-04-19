using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MatlabRunner m = (MatlabRunner)Application["MatlabRunner"];
        
        List<FmriRequest> reqList;

        reqList = m.GetQueueList();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = req.ImageName;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "" + req.X1 + "-" + req.X2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "" + req.Y1 + "-" + req.Y2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "" + req.Z1 + "-" + req.Z2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Convert.ToString(req.Threshold);
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Convert.ToString(req.TimeSubmitted);
            r.Cells.Add(c);

            tblQueue.Rows.Add(r);
        }

        reqList = m.GetDoneList();
        foreach (FmriRequest req in reqList)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = req.ImageName;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "" + req.X1 + "-" + req.X2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "" + req.Y1 + "-" + req.Y2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "" + req.Z1 + "-" + req.Z2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Convert.ToString(req.Threshold);
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Convert.ToString(req.TimeSubmitted);
            r.Cells.Add(c);

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
    }
}
