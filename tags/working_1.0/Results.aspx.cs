using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : System.Web.UI.Page
{
    protected void findImage(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            if (FmriCommon.isOutImageExists(id, Server))
            {
                imgResult.ImageUrl = "Results/" + id + ".png";
                pnlImage.Visible = true;
                pnlMessage.Visible = false;
            }
            else
            {
                lblMsg.Text = "File does not exist yet. Please try again later.<br />";
                lblMsg.ForeColor = System.Drawing.Color.DarkRed;
                pnlImage.Visible = false;
                pnlMessage.Visible = true;
            }
        }
        else
        {
            pnlImage.Visible = false;
            pnlMessage.Visible = true;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        findImage(txtID.Text);   
    }
        

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string id = Request.Params["id"];
            txtID.Text = id;
            findImage(id);        
        }
    }
}
