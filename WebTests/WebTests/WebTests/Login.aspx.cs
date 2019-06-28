using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using WebTests.data;
using System.Configuration;

namespace WebTests
{
    public partial class Login : System.Web.UI.Page
    {
        testdb2Entities eup = new testdb2Entities();
        SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["testdb2Entities"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            // TextBoxfio.Focus();
            // var testId = Convert.ToInt32(Request["testId"]);          
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

        //protected void TextBoxfio_TextChanged(object sender, EventArgs e)
        //{
        //}

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //  var testId = Convert.ToInt32(Request["testId"]);
            //try
            //{
            sql.Open();
            //}
            //catch
            //{}

            var login = TextBoxFIOSDAushego.Text;
            var pass = TextBoxDolSdaush.Text;
            //SqlCommand command = new SqlCommand("SELECT count(*) FROM [testdb2].[dbo].[USER] where EMAIL=" + "'" + login + "'" + " and PASSWORD=" + "'" + pass + "'", sql);
            SqlCommand command = new SqlCommand("SELECT USERID FROM [testdb2].[dbo].[USER] where EMAIL=" + "'" + login + "'" + " and PASSWORD=" + "'" + pass + "'", sql);
            SqlDataReader reader = command.ExecuteReader();
            //int count1 = 0;
            int loginId = -1;
            if (reader.HasRows != false)
            //if (reader.Read())
            {
                //count1 = 1;
                reader.Read();
                loginId = (int)reader[0];
            }

            sql.Close();
            if (loginId == -1)
            {
                Label1.Visible = true;
                Label1.Text = "Password or Username entered incorrectly";
            }
            else
            {
                Session["login"] = login;
                Session["loginId"] = loginId;
                Session["loginSd"] = TextBoxFIOSDAushego.Text;
                Session["dolgSd"] = TextBoxDolSdaush.Text;
                //Session["otdSd"] = TextBoxOtdSdaush.Text;
                // Response.Redirect("Answers.aspx?testId="+testId);
                Response.Redirect("Default.aspx");
            }

        }

    }
}
