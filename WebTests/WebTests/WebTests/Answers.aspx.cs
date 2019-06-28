using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Text.RegularExpressions;
using WebTests.data;

namespace WebTests
{
    public partial class Answers1 : System.Web.UI.Page
    {
        [Serializable]
        public partial class FLAGLARsew
        {
            public int IDvoprosa { get; set; }
            public List<bool> flagiotvetov { get; set; }
            public string textquestion { get; set; }
            public List<int> order { get; set; }
        }
        testdb2Entities obj = new testdb2Entities();
        protected void Page_Init(object sender, EventArgs e)
        {
            var testId = Convert.ToInt32(Request["testId"]);
            Label1.Text = obj.TEST.Single(p => p.TESTID == testId).TESTTITLE;
            if (Session["quest_list"] == null)
            {
                var questions = obj.QUESTION.Where(q => q.TESTID == testId);
                var sa = new List<int>();
                foreach (var question in questions)
                {
                    sa.Add((int)question.QUESTIONID);
                }
                //var a15 = Operation.GetRandomValues(sa, 15);   //вопрос рандомятся
                Answerss answerss = new Answerss();
                Questionss questionss = new Questionss();
                List<Answerss> answ_list = new List<Answerss>();
                List<Questionss> quest_list = new List<Questionss>();
                //foreach (var itemQ in a15)
                foreach (var itemQ in sa)
                {
                    var QuestionId = itemQ;
                    var QuestionText = obj.QUESTION.Single(h => h.TESTID == testId).QUESTIONTEXT; //текст вопроса
                    var qa = obj.QA.Where(l => l.QUESTIONID == QuestionId).ToList(); // Id всех ответов на вопрос
                    var ans = obj.ANSWER.Where(a => qa.Select(l => l.ANSWERID).Contains(a.ANSWERID)); //ответы на вопрос
                    List<QA> qas = new List<QA>();
                    foreach (var item in ans)
                    {
                        bool? t = obj.QA.Where(l => l.ANSWERID == item.ANSWERID && l.QUESTIONID == QuestionId).First().CORRECTANSWER;
                        QA qa1 = new QA()
                        {
                            QUESTIONID = QuestionId,
                            ANSWERID = item.ANSWERID,
                            CORRECTANSWER = t,
                            ANSWER = new ANSWER()
                            {
                                ANSWERID = item.ANSWERID,
                                ANSWERTEXT = item.ANSWERTEXT,
                                ANSWERNOTE = item.ANSWERNOTE,

                            }
                        };
                        qas.Add(qa1);
                    }
                    foreach (var es in qas)//запись ответов
                    {
                        answerss = new Answerss { TextOtv = es.ANSWER.ANSWERTEXT, AnswerCorrect = es.CORRECTANSWER.Value, QuestionId = es.QUESTIONID.Value };//тип  новый
                        answ_list.Add(answerss);
                    };
                    questionss = new Questionss { ID = QuestionId, QuestName = QuestionText, answerss = answ_list/*, enumq = enumqs */};//тип"вопросы".                    
                    quest_list.Add(questionss);
                }
                Session["answ_list"] = answ_list;
                Session["quest_list"] = quest_list;
                Session["answerss"] = answerss;
            }
        }
        /*........................................................................................................*/
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Index"] == null)
                Session["Index"] = 0;
            int galka = 0;
            Session["galka"] = null;

            /***------------------------------------------блок списка вопросов----------------------------------***/
            var questId = Convert.ToInt32(Request["questId"]);
            if (Session["quest_list"] != null)
            {
                int r = 1;
                int i = 0;
                var Qest_list = (List<Questionss>)Session["quest_list"];
                var Answ_list = (List<Answerss>)Session["answ_list"];
                var Flag_list = (List<FLAGLARsew>)Session["chks"];
                var countqest = Qest_list.Count();
                //для первого вопроса
                if (questId == 0)
                {
                    foreach (var td in Qest_list)
                    {
                        if (questId != td.ID)
                        {
                            questId = td.ID;
                        }
                        break;
                    }
                }
                //загрузка ответов из сессии
                QuestionsPerPart.Items.Clear();
                foreach (var t in Qest_list)
                {
                    var item = new ListItem
                    {
                        Value = t.ID.ToString(),
                        Text = string.Concat("Вопрос ", r)//t.ID.ToString() + "--
                    };
                    if (t.ID == questId)
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
                        var enums = Session["Values"] as List<ResultClassMy>;
                        var cur_flag = enums.Where(en => en.QuestionId == t.ID);
                        if (cur_flag.Count() > 0)
                        {
                            if (cur_flag.First().Result) { QuestionsPerPart.Items[i].Attributes["style"] = "color:green"; }   //ответ верный 
                            else { QuestionsPerPart.Items[i].Attributes["style"] = "color:red"; }//ответ не правильный                                             

                        }
                        else
                        {
                            QuestionsPerPart.Items[i].Attributes["style"] = "color:black";//нет ответа
                        }
                    }
                    i++; r++;

                }
                i = 0;



                /*------------------------------------------------------------------------------------------------------*/

                /*-------------------------------------------блок выводa названия вопроса-------------------------------------------*/
                foreach (var item in Qest_list)
                {
                    if (item.ID == questId)
                    {
                        LabelVoprosa.Text = item.QuestName;//вывод текста вопроса + item.ID"Вопрос: " + " " +
                        LabelVoprosa.ForeColor = System.Drawing.Color.Black;
                        LabelVoprosa.Font.Bold = true;

                    }
                    continue;

                }
                /*------------------------------------------блок вывода ответов-------------------------------------------*/
                int f = 0;
                int max = 0;
                List<string> _otv_list;
                List<string> Perem_otv_list;
                string otv;


                if (Session["quiestid"] == null)
                {
                    Session["ff"] = questId;
                }
                if ((int)Session["ff"] != questId)
                {
                    Session["Perem_otv_list"] = null;
                    Session["quiestid"] = null;
                }
                if (Session["Perem_otv_list"] == null && Session["quiestid"] == null)
                {
                    _otv_list = new List<string>();
                    Perem_otv_list = new List<string>();
                    foreach (var c in Answ_list)
                    {
                        if (c.QuestionId == questId && c.Porydoc == null)
                        {
                            otv = c.TextOtv;
                            _otv_list.Add(otv);
                        }
                    }
                    Perem_otv_list = Operation.PermOtveti(_otv_list);
                    Session["quiestid"] = questId;
                    Session["ff"] = questId;
                    Session["Perem_otv_list"] = Perem_otv_list;
                }
                else
                {
                    Perem_otv_list = (List<string>)Session["Perem_otv_list"];
                }

                if (Flag_list == null)/* ...если ответ не получен еще ни один*/
                {

                    int maxlengh = 0;
                    int l = 1;
                    int ses = 0;

                    foreach (var n in Answ_list)
                    {
                        if (n.QuestionId == questId && n.Porydoc == null)
                        {
                            foreach (var d in Perem_otv_list)
                            {
                                var chk = new CheckBox//создание чкбоксов
                                {
                                    BackColor = System.Drawing.Color.Yellow,
                                    Text = d,//n.TextOtv,
                                    ID = "chk" + f.ToString(),
                                    AutoPostBack = true,
                                    Checked = false,
                                };
                                chk.Width = Request.Browser.ScreenPixelsWidth;
                                chk.Font.Size = 14;
                                chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                                chk.ForeColor = System.Drawing.Color.Black;
                                chk.Font.Name = "Calibri";
                                Panel2.Controls.Add(chk);
                                Panel2.Controls.Add(new LiteralControl("<br /><br />"));
                                f++;
                            }
                            break;
                        }
                        else if (n.AnswerCorrect == true && n.Porydoc != null && n.QuestionId == questId)
                        {
                            if (Session["galka"] == null)
                            {
                                Panel2.Controls.Add(new LiteralControl("<table border='1'>"));
                            }
                            AnswerButton.Enabled = true;
                            Label3.Visible = true;
                            maxlengh++;
                            TextBox1.Visible = true;
                            TextBox1.Focus();
                            Label2.Visible = true;
                            //Button1.Visible = true;
                            Panel2.Controls.Add(new LiteralControl("<tr><td><span style='font-weight: bold; color: black;'>"));
                            var llab = new Label
                            {
                                BackColor = System.Drawing.Color.Yellow,
                                Text = n.TextOtv,
                                ID = "llab" + f.ToString(),
                            };
                            llab.Width = 300;
                            llab.Font.Size = 14;
                            llab.ForeColor = System.Drawing.Color.Black;
                            llab.BorderColor = System.Drawing.Color.Black;
                            llab.Font.Name = "Calibri";
                            Panel2.Controls.Add(llab);
                            Panel2.Controls.Add(new LiteralControl("</span></td>"));
                            f++;
                            Session["galka"] = ++galka;
                            //continue;
                        }
                        else if (n.AnswerCorrect == false && n.Porydoc != null && n.QuestionId == questId)
                        {
                            if (Session["ss"] == null) { Session["ss"] = ses; }
                            Panel2.Controls.Add(new LiteralControl("<td><span style='font-weight: bold; color: black;'>"));
                            var labfs = new Label
                            {
                                BackColor = System.Drawing.Color.Yellow,
                                Text = "[" + l + "]" + n.TextOtv,
                                ID = "labfs" + l.ToString(),
                            };
                            labfs.Width = 400;
                            labfs.Font.Size = 14;
                            labfs.ForeColor = System.Drawing.Color.Black;
                            labfs.BorderColor = System.Drawing.Color.Black;
                            labfs.Font.Name = "Calibri";
                            Panel2.Controls.Add(labfs);
                            l++;
                            Panel2.Controls.Add(new LiteralControl("</span></td></tr>"));
                            max = maxlengh + (maxlengh - 1);
                            //Session["maxl"] = max;
                            TextBox1.MaxLength = max;
                            TextBox1.Visible = true;
                            switch (maxlengh)
                            {
                                case 2:
                                    RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                    break;
                                case 3:
                                    RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                    break;
                                case 4:
                                    RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                    break;
                                case 5:
                                    RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                    break;
                            }
                        }
                        Session["galka"] = ++galka;
                    }
                    if (Session["galka"] != null)
                    {
                        Panel2.Controls.Add(new LiteralControl("</table>"));
                        Session["galka"] = null;
                    }
                }
                else                        /*..........................................если ответs хотя бы один плучен......................*/
                {
                    int contin = 0;
                    int maxlengh = 0;
                    foreach (var hj in Flag_list)
                    {
                        if (hj.IDvoprosa == questId)
                        {
                            contin++;
                        }
                        if (contin > 1)                         //это повторяется вопрос, если истинно
                        {
                            break;
                        }
                    }
                    if (contin == 0)         //вопрос не повторялся 
                    {
                        int l = 1;
                        int ses = 0;
                        foreach (var n in Answ_list)
                        {
                            if (n.QuestionId == questId && n.Porydoc == null)
                            {
                                var chk = new CheckBox                                                       //создание чкбоксов
                                {
                                    BackColor = System.Drawing.Color.Yellow,
                                    Text = n.TextOtv,
                                    ID = "chk" + f.ToString(),
                                    AutoPostBack = true,
                                    Checked = false,
                                };
                                chk.Width = Request.Browser.ScreenPixelsWidth;
                                chk.Font.Size = 14;
                                chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                                chk.ForeColor = System.Drawing.Color.Black;
                                chk.Font.Name = "Calibri";
                                Panel2.Controls.Add(chk);
                                Panel2.Controls.Add(new LiteralControl("<br /><br />"));
                                f++;
                            }

                            else if (n.AnswerCorrect == true && n.Porydoc != null && n.QuestionId == questId)
                            {

                                maxlengh++;
                                if (Session["galka"] == null)
                                {
                                    Panel2.Controls.Add(new LiteralControl("<table border='1'>"));

                                }
                                TextBox1.Visible = true;
                                AnswerButton.Enabled = true;
                                Label2.Visible = true;
                                TextBox1.Focus();
                                Label3.Visible = true;
                                Panel2.Controls.Add(new LiteralControl("<tr><td><span style='font-weight: bold; color: black;'>"));
                                var llab = new Label
                                {
                                    BackColor = System.Drawing.Color.Yellow,
                                    Text = n.TextOtv,
                                    ID = "llab" + f.ToString(),

                                };
                                llab.Width = 300;
                                llab.Font.Size = 14;
                                llab.ForeColor = System.Drawing.Color.Black;
                                llab.BorderColor = System.Drawing.Color.Black;
                                llab.Font.Name = "Calibri";
                                Panel2.Controls.Add(llab);
                                Panel2.Controls.Add(new LiteralControl("</span></td>"));
                                //Session["maxlengh"] = maxlengh;
                                f++;
                                Session["galka"] = ++galka;

                            }
                            if (n.AnswerCorrect == false && n.Porydoc != null && n.QuestionId == questId)
                            {
                                if (Session["ss"] == null) { Session["ss"] = ses; }
                                Panel2.Controls.Add(new LiteralControl("<td><span style='font-weight: bold; color: black;'>"));

                                var labf = new Label
                                {
                                    BackColor = System.Drawing.Color.Yellow,
                                    Text = "[" + l + "]" + n.TextOtv,
                                    ID = "labf" + l.ToString(),

                                };
                                labf.Width = 400;
                                labf.Font.Size = 14;
                                labf.ForeColor = System.Drawing.Color.Black;
                                labf.BorderColor = System.Drawing.Color.Black;
                                labf.Font.Name = "Calibri";
                                Panel2.Controls.Add(labf);
                                l++;
                                Panel2.Controls.Add(new LiteralControl("</span></td></tr>"));
                                max = maxlengh + (maxlengh - 1);
                                TextBox1.MaxLength = max;
                                // Session["maxl"] = max;
                                TextBox1.Visible = true;
                                switch (maxlengh)
                                {
                                    case 2:
                                        RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                        break;
                                    case 3:
                                        RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                        break;
                                    case 4:
                                        RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                        break;
                                    case 5:
                                        RegularExpressionValidator1.ValidationExpression = "[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "],[1-" + maxlengh + "]";
                                        break;
                                }
                            }
                            Session["galka"] = ++galka;

                        }
                        if (Session["galka"] != null)
                        {
                            Panel2.Controls.Add(new LiteralControl("</table>"));
                            Session["galka"] = null;
                        }
                    }

                    //если  отвечал на вопрос
                    else
                    {

                        int tx = 0;
                        var txt = new Label
                        {
                            BackColor = System.Drawing.Color.Red,
                            Text = "Вы ответили на этот вопрос.Перейдите к вопросу помеченному черным цветом",
                            ID = "txt" + tx.ToString()
                        };
                        txt.Width = Request.Browser.ScreenPixelsWidth;
                        // txt.Height = 40;
                        txt.Font.Size = 14;
                        txt.ForeColor = System.Drawing.Color.White;
                        txt.Font.Name = "Calibri";
                        Panel2.Controls.Add(txt);

                    }

                }
            }
        }


        void chk_CheckedChanged(object sender, EventArgs e)
        {
            var chks = new List<bool>();
            AnswerButton.Enabled = false;
            foreach (var chk in Panel2.Controls)
            {
                if (chk is CheckBox)
                {
                    if ((chk as CheckBox).Checked)
                    {
                        AnswerButton.Enabled = true;
                        break;
                    }
                }
            }
            UpdatePanel2.Update();
        }

        /*-------------------------------кнопка: "ответить"------------------------------------------------------*/


        protected void Ans(object sender, EventArgs e)
        {


            int schetchik = 0;
            if (Session["sch"] != null)
            {
                schetchik = (int)Session["sch"];
            }

            FLAGLARsew flagi = new FLAGLARsew();

            ResultClassMy element = new ResultClassMy();
            int ag = 0;
            int ab = 0;
            int aa = 0;
            if (Session["Answergood"] != null)
            {
                ag = (int)Session["Answergood"];
            }
            if (Session["Answerbad"] != null)
            {
                ab = (int)Session["Answerbad"];
            }
            if (Session["AnswerAll"] != null)
            {
                aa = (int)Session["AnswerAll"];
            }
            var testId = Convert.ToInt32(Request["testId"]);
            var questId = Convert.ToInt32(Request["questId"]);
            var Qest_list = Session["quest_list"] as List<Questionss>;//список вопросов по разделу
            var Answ_list = Session["answ_list"] as List<Answerss>;//список ответов по разделу
            var answerss = Session["answerss"];
            string textquesta = "";
            if (questId == 0)
            {
                foreach (var td in Qest_list)
                {
                    if (td.ID != questId)
                    {
                        questId = td.ID;

                    }
                    break;
                }
            }
            foreach (var g in Qest_list)
            {
                if (g.ID == questId)
                    textquesta = g.QuestName;
                else continue;
            }
            /*......................если  обычный вопрос...................................*/
            //if (Session["ss"] == null)
            {
                var chks = new List<Proverka>();
                foreach (var chk in Panel2.Controls)
                {
                    if (chk is CheckBox)
                    {
                        if ((chk as CheckBox).Checked)
                        {
                            Proverka row = new Proverka
                            {
                                pravda = true,
                                textO = (chk as CheckBox).Text

                            };
                            chks.Add(row);
                            //chks.Add(true);
                            //text_otveta.Add((chk as CheckBox).Text);
                        }
                        else
                        {
                            Proverka row = new Proverka
                            {
                                pravda = false,
                                textO = (chk as CheckBox).Text

                            };
                            chks.Add(row);
                            //chks.Add(false);
                            //text_otveta.Add((chk as CheckBox).Text);

                        }
                    }
                }
                var t = 0;
                var counter = 0;
                var flags = Answ_list.Where(s => s.QuestionId == questId).Select(s => s);

                foreach (var tx in chks)
                {

                    foreach (var tw in flags)
                    {
                        if (tx.textO.Equals(tw.TextOtv) & tx.pravda == tw.AnswerCorrect)
                        {

                        }
                        else if (tx.textO.Equals(tw.TextOtv) & tx.pravda != tw.AnswerCorrect)
                        {
                            counter++;
                        }
                    }
                }
                Session["sch"] = schetchik;


                flagi = new FLAGLARsew { IDvoprosa = questId, flagiotvetov = chks.Select(d => d.pravda).ToList(), textquestion = textquesta };
                List<FLAGLARsew> Flag_list;

                if (Session["chks"] == null)
                {
                    Flag_list = new List<FLAGLARsew>();

                }
                else
                {
                    Flag_list = (List<FLAGLARsew>)Session["chks"];

                }
                Flag_list.Add(flagi);
                Session["chks"] = Flag_list;
                var result = false;

                if (counter > 0)
                {
                    ab++;
                    Session["Answerbad"] = ab;//неправильно
                }
                else
                {
                    ag++;
                    Session["Answergood"] = ag;//правильно
                    result = true;
                }

                aa = ag + ab;//кол-во ответов

                Session["AnswerAll"] = aa;
                element = new ResultClassMy { QuestionId = questId, Result = result };
                List<ResultClassMy> element_list;
                if (Session["Values"] == null)
                {
                    element_list = new List<ResultClassMy>();

                }
                else
                {
                    element_list = (List<ResultClassMy>)Session["Values"];
                    // (Session["Values"] as List<ResultClassMy>).Add(element);
                }
                element_list.Add(element);
                Session["Values"] = element_list;
            }
            /*.............................не обычный вопрос.........................................*/
            //else
            //{
            //    var poro = new List<int>();
            //    var porydk = obj.ANSWER.Where(hj => hj.idpart == testId && hj.id_questions == questId && hj.order != null && hj.flag == false).Select(hj => hj.order);
            //    var pop = porydk.Count();
            //    var textb = TextBox1.Text;
            //    int p = 0;
            //    string[] split = textb.Split(new Char[] { ' ', ',', '.' });
            //    var cou = split.Count();
            //    /*..........................если кол-во ответов совпадает с эталонным количеством...............................*/
            //    if (cou == pop)
            //    {
            //        int[] io = new int[cou];
            //        foreach (string s in split)
            //        {
            //            if (s.Trim() != "")
            //                io[p] = Convert.ToInt32(s);
            //            poro.Add(io[p]);
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
            //        flagi = new FLAGLARsew { IDvoprosa = questId, order = poro, textquestion = textquesta };
            //        List<FLAGLARsew> Flag_list;
            //        if (Session["chks"] == null)
            //        {
            //            Flag_list = new List<FLAGLARsew>();
            //        }
            //        else
            //        {
            //            Flag_list = (List<FLAGLARsew>)Session["chks"];
            //        }
            //        Flag_list.Add(flagi);
            //        Session["chks"] = Flag_list;
            //        var result = false;
            //        if (counter > 0)
            //        {
            //            ab++;
            //            Session["Answerbad"] = ab;
            //        }
            //        else
            //        {
            //            ag++;
            //            Session["Answergood"] = ag;
            //            result = true;
            //        }
            //        aa = ag + ab;
            //        Session["AnswerAll"] = aa;
            //        element = new ResultClassMy { QuestionId = questId, Result = result };
            //        List<ResultClassMy> element_list = new List<ResultClassMy>();
            //        element_list.Add(element);
            //        if (Session["Values"] == null)
            //        {
            //            Session["Values"] = element_list;
            //        }
            //        else
            //        {
            //            (Session["Values"] as List<ResultClassMy>).Add(element);
            //        }
            //    }
            //    /*..................если перебор или недобор...........................................*/
            //    else
            //    {
            //        var result = false;
            //        element = new ResultClassMy { QuestionId = questId, Result = result };
            //        List<ResultClassMy> element_list = new List<ResultClassMy>();
            //        element_list.Add(element);
            //        if (Session["Values"] == null)
            //        {
            //            Session["Values"] = element_list;
            //        }
            //        else
            //        {
            //            (Session["Values"] as List<ResultClassMy>).Add(element);
            //        }
            //    }
            //    Session["ss"] = null;
            //}
            int index = (int)Session["Index"];
            Session["Index"] = ++index;
            if (aa == 15)                //переход при ответах
            {
                Response.Redirect("Primer.aspx?testId=" + testId);
            }

            questId = Qest_list.AsEnumerable().ElementAt(index).ID;
            string url = "Answers.aspx?testId=" + testId + "&questId=" + questId;

            Response.Redirect(url);

        }

        private DateTime finishTime
        {
            get
            {
                if (Session["finishTime"] == null) Session["finishTime"] = DateTime.Now.AddMinutes(30);
                return (DateTime)Session["finishTime"];
            }
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, finishTime) > 0)
            {
                var testId = Convert.ToInt32(Request["testId"]);
                Timer1.Enabled = false;
                Response.Redirect("Primer.aspx?testId=" + testId);
            }

            Labeltimer.Text = (finishTime - DateTime.Now).ToString(@"mm\:ss");
        }



    }
}


