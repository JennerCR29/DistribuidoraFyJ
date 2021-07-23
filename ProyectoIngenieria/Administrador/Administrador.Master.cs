using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["action"] != null)
            //{
            //    Response.Clear();
            //    Session.Abandon();
            //    Response.Write("Success");
            //    Response.End();
            //}
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/InicioSesion.aspx");
            Response.Clear();
            Session.Abandon();
            Session.Clear();
            Response.End();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
    }
}