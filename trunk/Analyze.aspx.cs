using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Analyze: System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DirectoryInfo di = new DirectoryInfo(FmriCommon.getSrcImageDir(Server));
            FileInfo[] lstFI = di.GetFiles("*.mat");
            foreach (FileInfo f in lstFI)
            {
                lstImageName.Items.Add(f.Name);
            }
        }
    }

    private void debugString(string msg)
    {
        lblDebugReport.Text += msg + "<br />";
    }

    private void execute(string imageName,
        int x1, int x2,
        int y1, int y2,
        int z1, int z2,
        double threshold)
    {
        debugString("Running on image: " + imageName);
        debugString("X=[" + x1 + "," + x2 + "]");
        debugString("Y=[" + y1 + "," + y2 + "]");
        debugString("Z=[" + z1 + "," + z2 + "]");
        debugString("Threshold: " + threshold);

        FmriRequest req = new FmriRequest(imageName, x1, x2, y1, y2, z1, z2, threshold);
        refStr.Value = req.AreaStringWithThresholdMD5;

        debugString("AreaString: " + req.AreaString + " (" + req.AreaStringMD5 + ")");
        debugString("AreaStringWithThreshold: " + req.AreaStringWithThreshold + " (" + req.AreaStringWithThresholdMD5 + ")");

        if (FmriCommon.isOutImageExists(req.AreaStringWithThresholdMD5, Server))
        {
            debugString("Filename: \"" + FmriCommon.getOutImageDir(Server) + req.AreaStringWithThresholdMD5 + ".png" + "\" exists. good.");
            lblRef.ForeColor = System.Drawing.Color.Green;
            lblRef.Text = "Your request is completed. See results page!";
        }
        else
        {
            MatlabRunner m = FmriCommon.getMatlabRunner(Application, Server);
            m.EnqueueRequest(req);
            debugString("Posted to queue.");
            lblRef.ForeColor = System.Drawing.Color.Blue;
            lblRef.Text = "Your request was posted to the queue. Try the results page later.";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String imageName = lstImageName.SelectedItem.Text;
        int x1 = Convert.ToInt32(txtX1.Text);
        int x2 = Convert.ToInt32(txtX2.Text);
        int y1 = Convert.ToInt32(txtY1.Text);
        int y2 = Convert.ToInt32(txtY2.Text);
        int z1 = Convert.ToInt32(txtZ1.Text);
        int z2 = Convert.ToInt32(txtZ2.Text);
        double threshold = Convert.ToDouble(txtThreshold.Text);

        execute(imageName, x1, x2, y1, y2, z1, z2, threshold);
        pnlRefArea.Visible = true;
    }

    protected void btnGoToResults_Click(object sender, EventArgs e)
    {
        Response.Redirect("Results.aspx?id=" + refStr.Value);
    }
}
