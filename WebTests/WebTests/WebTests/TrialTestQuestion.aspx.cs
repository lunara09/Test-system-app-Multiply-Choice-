using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Drawing.Design;
using WebTests.data;
using WebTests.MyClasses;

namespace WebTests
{
    [Serializable]
    public partial class TrialTestQuestion : System.Web.UI.Page
    {
        testdb2Entities obj = new testdb2Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblUserName.Text = Session["login"].ToString();
            }
            //int galka = 0;
            Session["galka"] = null;
            var testId = Convert.ToInt32(Request["testId"]);
            int first = obj.QUESTION.Where(h => h.TESTID == testId).Select(y => y.QUESTIONID).FirstOrDefault();
            var questId = Convert.ToInt32(Request["questId"]);
            if (questId == first || questId == 1) BackButton.Visible = false;
            Label1.Text = obj.TEST.Single(p => p.TESTID == testId).TESTTITLE;
            var text = obj.QUESTION.Where(h => h.TESTID == testId).FirstOrDefault();

            // перенести в админку на закрытие окна вопросов
            var qs = obj.QUESTION.Where(k => k.TESTID == testId).ToList();
            for (int iQ = 0; iQ < qs.Count(); iQ++)
            {
                qs[iQ].QUESTIONNUMBER = iQ + 1;
            }
            obj.SaveChanges();
            //

            if (text == null)
            {
                questId = obj.QUESTION.Where(h => h.TESTID == testId).Select(y => y.QUESTIONID).FirstOrDefault();
                text = obj.QUESTION.Where(h => h.TESTID == testId && h.QUESTIONID == questId).FirstOrDefault();
                if (text == null)
                {
                    string url = "NotFound.aspx";
                    Response.Redirect(url);
                }
            }
            Label2.ForeColor = System.Drawing.Color.Black;
            Label2.Font.Bold = true;
            Label2.Text = string.Concat("Question №  ", obj.QUESTION.Where(k=>k.QUESTIONID==questId).First().QUESTIONNUMBER.ToString(), ": ", text.QUESTIONTEXT);
            //  var answers = obj.Answers.Where(a => a.idpart == testId && a.id_questions == questId);
            var questions = obj.QUESTION.Where(q => q.TESTID == testId);
            var qas = obj.QA.Where(d => d.QUESTIONID == questId).ToList();
            //var Answ_lis = obj.ANSWER.Where(po => qas.Select(d => d.ANSWERID).Contains(po.ANSWERID));
            if (questId >= questions.Count()) NextButton.Visible = false;
            var i = 0;

            QuestionsPerPart.Items.Clear();
            foreach (var q in questions)
            {
                var item = new ListItem { Value = q.QUESTIONID.ToString(), Text = string.Concat("Question № ", q.QUESTIONNUMBER.ToString()) };
                if (q.QUESTIONID == questId)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
                QuestionsPerPart.Items.Add(item);
                if (Session["Values"] != null)
                {
                    var enums = Session["Values"] as List<ResultClass>;
                    var cur_flag = enums.Where(en => en.QuestionId == q.QUESTIONID);
                    if (cur_flag.Count() > 0)
                    {
                        if (cur_flag.First().Result)
                        {
                            QuestionsPerPart.Items[i].Attributes["style"] = "color:green";


                        }
                        else
                        {
                            QuestionsPerPart.Items[i].Attributes["style"] = "color:red";

                        }
                    }
                    else
                    {
                        QuestionsPerPart.Items[i].Attributes["style"] = "color:black";
                    }
                }
                i++;
            }


            i = 0;
            int f = 0;
            int l = 1;
            int ses = 0;
            //foreach (var c in Answ_lis)
            foreach (var c in qas)
            {
                //if (c.QUESTIONID == questId)
                //{
                    var chk = new RadioButton//создание чкбоксов
                    {
                        //BackColor = System.Drawing.Color.Yellow,
                        Text = obj.ANSWER.Where(k=>k.ANSWERID == c.ANSWERID).First().ANSWERTEXT,
                        ID = "chk" + f.ToString(),
                        GroupName = questId.ToString(),
                        // AutoPostBack = true,
                        Checked = false,
                    };
                    chk.Width = Request.Browser.ScreenPixelsWidth;
                    chk.Font.Size = 14;
                    chk.ForeColor = System.Drawing.Color.Black;
                    chk.Font.Name = "Calibri";
                    Panel2.Controls.Add(chk);
                    Panel2.Controls.Add(new LiteralControl("<br /><br />"));
                    f++;
                //}
                //if (c.CORRECTANSWER == true)
                //{
                //    if (Session["galka"] == null)
                //    {
                //        Panel2.Controls.Add(new LiteralControl("<table border='0'>"));

                //    }
                //    TextBox1.Visible = true;
                //    Label3.Visible = true;
                //    //Button1.Visible = true;
                //    Panel2.Controls.Add(new LiteralControl("<tr><td><span style='font-weight: bold; color: black;'>"));
                //    var llab = new Label
                //    {
                //        BackColor = System.Drawing.Color.Yellow,
                //        Text = obj.ANSWER.Where(k=>k.ANSWERID == c.ANSWERID).First().ANSWERTEXT,
                //        ID = "llab" + f.ToString(),

                //    };
                //    llab.Width = 300;
                //    llab.Font.Size = 14;
                //    llab.ForeColor = System.Drawing.Color.Black;
                //    llab.BorderColor = System.Drawing.Color.Black;
                //    llab.Font.Name = "Calibri";
                //    Panel2.Controls.Add(llab);
                //    Panel2.Controls.Add(new LiteralControl("</span></td>"));
                //    f++; Session["galka"] = ++galka;
                //    continue;
                //}
                // ???
                //if (c.CORRECTANSWER == false)
                //{
                //    if (Session["ss"] == null) { Session["ss"] = ses; }
                //    Panel2.Controls.Add(new LiteralControl("<td><span style='font-weight: bold; color: black;'>"));

                //    var liu = new Label
                //    {
                //        BackColor = System.Drawing.Color.Yellow,
                //        Text = "[" + l + "]" + obj.ANSWER.Where(d=>d.ANSWERID == c.ANSWERID).First().ANSWERTEXT,
                //        ID = "chk" + l.ToString(),

                //    };
                //    liu.Width = 300;
                //    liu.Font.Size = 14;
                //    liu.ForeColor = System.Drawing.Color.Black;
                //    liu.BorderColor = System.Drawing.Color.Black;
                //    liu.Font.Name = "Calibri";
                //    Panel2.Controls.Add(liu);
                //    l++;
                //    Panel2.Controls.Add(new LiteralControl("</span></td></tr>"));
                //}
            }
            Panel2.Controls.Add(new LiteralControl("</table>"));
        }


        protected void Answer(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();

            var testId = Convert.ToInt32(Request["testId"]);
            var QuestId = Convert.ToInt32(Request["questId"]);
            //выделенные пользователем ответы
            if (Session["ss"] == null)
            {
                var chks = new List<bool>();
                foreach (var chk in Panel2.Controls)
                {
                    if (chk is RadioButton)
                    {
                        if ((chk as RadioButton).Checked)
                        {
                            chks.Add(true);
                        }
                        else
                        {
                            chks.Add(false);
                        }
                    }

                }
                //правильные ответы
                var q = obj.QUESTION.Where(l => l.TESTID == testId && l.QUESTIONID == QuestId).First();
                List<bool> flags = new List<bool>();
                var qa = obj.QA.Where(a => a.QUESTIONID == QuestId).ToList();
                foreach (var item in qa)
                {
                    var t = item.CORRECTANSWER.Value;
                    flags.Add(t);
                }

                var i = 0;
                var counter = 0;
                foreach (var flag in flags)
                {
                    if (flag == true)
                    {
                        (FindControl("chk" + i.ToString()) as RadioButton).BackColor = System.Drawing.Color.SeaGreen;
                    }
                    else
                    {
                        (FindControl("chk" + i.ToString()) as RadioButton).BackColor = System.Drawing.Color.OrangeRed;
                    }
                    if (flag != chks[i])
                    {
                        counter++;
                    }
                    i++;
                }
                var result = false;
                if (counter > 0)
                {

                    Result.ForeColor = System.Drawing.Color.Red;
                    Result.Text = "Incorrect answer";

                }
                else
                {

                    Result.ForeColor = System.Drawing.Color.Green;
                    Result.Text = "Correct answer";
                    result = true;
                }


                var element = new ResultClass { QuestionId = QuestId, Result = result };
                List<ResultClass> element_list = new List<ResultClass>();
                element_list.Add(element);
                if (Session["Values"] == null)
                {
                    Session["Values"] = element_list;
                }
                else
                {
                    (Session["Values"] as List<ResultClass>).Add(element);
                }

            }
            //else
            //{
            //    var porydk = obj.ANSWER.Where(hj => hj.idpart == testId && hj.id_questions == QuestId && hj.flag == false);
            //    var pop = porydk.Count();
            //    var textb = TextBox1.Text;
            //    int p = 0;
            //    string[] split = textb.Split(new Char[] { ' ', ',', '.', ':' });
            //    var cou = split.Count();
            //    if (cou == pop)
            //    {
            //        int[] io = new int[cou];
            //        foreach (string s in split)
            //        {
            //            if (s.Trim() != "")
            //                io[p] = Convert.ToInt32(s);
            //            p++;
            //        }

            //        int t = 0;
            //        int counter = 0;

            //        foreach (var po in porydk)
            //        {
            //            if (io[t] != po)
            //            {
            //                counter++;
            //            }
            //            t++;
            //        }

            //        var result = false;
            //        if (counter > 0)
            //        {
            //            Result.ForeColor = System.Drawing.Color.Red;
            //            Result.Text = "Incorrect answer";
            //        }
            //        else
            //        {
            //            Result.ForeColor = System.Drawing.Color.Green;
            //            Result.Text = "Correct answer";
            //            result = true;
            //        }

            //        var element = new ResultClass { QuestionId = QuestId, Result = result };
            //        List<ResultClass> element_list = new List<ResultClass>();
            //        element_list.Add(element);
            //        if (Session["Values"] == null)
            //        {
            //            Session["Values"] = element_list;
            //        }
            //        else
            //        {
            //            (Session["Values"] as List<ResultClass>).Add(element);
            //        }
            //    }
            //    else
            //    {
            //        var result = false;
            //        Result.ForeColor = System.Drawing.Color.Red;
            //        Result.Text = "Incorrect answer";
            //        var element = new ResultClass { QuestionId = QuestId, Result = result };
            //        List<ResultClass> element_list = new List<ResultClass>();
            //        element_list.Add(element);
            //        if (Session["Values"] == null)
            //        {
            //            Session["Values"] = element_list;
            //        }
            //        else
            //        {
            //            (Session["Values"] as List<ResultClass>).Add(element);
            //        }
            //    }
            //    Session["ss"] = null;
            //    //ModalPopupExtender1.Show();
            //}

        }

        protected void Next(object sender, EventArgs e)
        {
            string testId = Request["testId"];
            string std = Request["questId"];
            int questId = Convert.ToInt32(std);
            questId++;
            string url = "TrialTestQuestion.aspx?testId=" + testId + "&questid=" + questId;
            Response.Redirect(url);
        }

        protected void Prev(object sender, EventArgs e)
        {
            string testId = Request["testId"];
            string std = Request["questId"];
            int questId = Convert.ToInt32(std);
            questId--;
            string url = "TrialTestQuestion.aspx?testId=" + testId + "&questid=" + questId;
            Response.Redirect(url);
        }

        //protected void Butizmpor_Click(object sender, EventArgs e)
        //{
        //    var testId = Convert.ToInt32(Request["testId"]);
        //    var questId = Convert.ToInt32(Request["questId"]);

        //    string url = "TrialTestQuestion.aspx?testId=" + testId + "&questid=" + questId;
        //    Response.Redirect(url);
        //}


    }
}