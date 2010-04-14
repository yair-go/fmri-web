using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void debugString(string msg)
    {
        lblDebugReport.Text += msg + "<br />";
    }

    protected string isFilenameOK(string filename)
    {
        if (!filename.EndsWith(".mat"))
        {
            return " Filename must end with '.mat' suffix! ";
        }
        if (System.IO.File.Exists(FmriCommon.getSrcImageDir(Server) + filename))
        {
            return " Filename already exists on the server! ";
        }

        return null;
    }

    protected void txtServerFileName_TextChanged(object sender, EventArgs e)
    {
        lblSFNValidationMessage.Visible = true;

        string errMsg = isFilenameOK(txtServerFileName.Text);
        if (errMsg != null)
        {
            lblSFNValidationMessage.Text = errMsg;
            return;
        }

        lblSFNValidationMessage.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblSFNValidationMessage.Visible = true;

        string errMsg = isFilenameOK(txtServerFileName.Text);
        if (errMsg != null)
        {
            lblSFNValidationMessage.Text = errMsg;
            return;
        }

        lblSFNValidationMessage.Visible = false;



        debugString("Got " + uploadCtrl.FileBytes.Length + " bytes...");
        string fname = FmriCommon.getSrcImageDir(Server) + txtServerFileName.Text;
        try
        {
            FileStream fs = new FileStream(
                fname,
                FileMode.CreateNew);
            fs.Write(uploadCtrl.FileBytes, 0, uploadCtrl.FileBytes.Length);
            fs.Close();

            debugString("Written successfully into " + fname + ".");

            lblResult.Text = "File was successfully uploaded.";
            lblResult.ForeColor = System.Drawing.Color.Green;
            pnlResult.Visible = true;
        }
        catch (IOException ioe)
        {
            lblResult.Text = ioe.Message;
            lblResult.ForeColor = System.Drawing.Color.Red;
            pnlResult.Visible = true;
        }

    }
}
