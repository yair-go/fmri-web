using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : System.Web.UI.Page
{
    protected void findImage(string id, string id2)
    {
        if (!string.IsNullOrEmpty(id))
        {
            if (FmriCommon.isOutImageExists(id, Server))
            {
                imgResult.ImageUrl = "Results/" + id + ".png";
                pnlImage.Visible = true;
            }
            else
            {
                lblMsg.Text = "File does not exist yet. Please try again later.<br />";
                lblMsg.ForeColor = System.Drawing.Color.DarkRed;
                pnlImage.Visible = false;
            }
        }
        else
        {
            pnlImage.Visible = false;

        }

        if (!string.IsNullOrEmpty(id2))
        {
            string xls_filename = FmriCommon.getExcelDir(Server) + id2 + ".csv";

            if (System.IO.File.Exists(xls_filename))
            {
                lnkExcelFile.NavigateUrl = "Excel/" + id2 + ".csv";
                lnkZipFile.NavigateUrl = "Excel/" + id2 + ".zip";
                pnlExcelFile.Visible = true;
            }
            else
            {
                lblMsg.Text = "File does not exist yet. Please try again later.<br />";
                lblMsg.ForeColor = System.Drawing.Color.DarkRed;
                pnlExcelFile.Visible = false;
            }
        }
        else
        {
            pnlExcelFile.Visible = false;
        }

        if (pnlExcelFile.Visible == false || pnlImage.Visible == false)
        {
            pnlMessage.Visible = true;
        }
        else
        {
            pnlMessage.Visible = false;
        }

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        findImage(txtID.Text, txtExcelID.Text);   
    }
        

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string id = Request.Params["id"];
            string id2 = Request.Params["id2"];
            txtID.Text = id;
            txtExcelID.Text = id2;
            findImage(id, id2);
        }
    }
}
