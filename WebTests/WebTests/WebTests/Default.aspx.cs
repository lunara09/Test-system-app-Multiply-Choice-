﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;


namespace WebTests
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                //Response.Redirect("Login.aspx");
            }
            else
            {
                lblUserName.Text = ", " +Session["login"].ToString();
            }
            //DateTime l = DateTime.Now;
            // Label1.Text = l.ToString();
            if (Session["Index"] != null) Session["Index"] = null;
            if (Session["finishTime"] != null) Session["finishTime"] = null;
            if (Session["Answerbad"] != null) Session["Answerbad"] = null;
            if (Session["Answergood"] != null) Session["Answergood"] = null;
            if (Session["Values"] != null) Session["Values"] = null;
            if (Session["answ_list"] != null) Session["answ_list"] = null;
            if (Session["quest_list"] != null) Session["quest_list"] = null;
            if (Session["chks"] != null) Session["chks"] = null;
            if (Session["AnswerAll"] != null) Session["AnswerAll"] = null;

        }
    }
}
