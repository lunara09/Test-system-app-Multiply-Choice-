using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;

namespace WebTests
{
    public partial class Primer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["chks"] != null)
            {
                var resultekzamen = 0;
                var testId = Convert.ToInt32(Request["testId"]);
                int countqest = 0;
                var Qest_list = (List<Questionss>)Session["quest_list"];
                int ab = Convert.ToInt32(Session["Answerbad"]);
                var ag = Convert.ToInt32(Session["Answergood"]);
                //var kolPravOtv = (int)(Session["sch"]);
                int aa = (int)Session["AnswerAll"];
                double procent = 0;
                if (Qest_list != null)
                {
                    countqest = Qest_list.Count();
                }
                if (countqest != 0)
                {
                    procent = ((double)ag / countqest) * 100;
                    procent = Math.Round(procent, 0);

                }
                if (procent >= 80)
                {

                    Label1.Text = "Exam Сдан";
                    resultekzamen = 1;

                }
                else
                {
                    Label1.Text = "Exam Несдан";
                }
                HyperLink1.NavigateUrl = "About.aspx?testId=" + testId;
                Session["resu"] = resultekzamen;
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

        }
    }
}