using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using WebTests.data;

namespace WebTests
{
    [Serializable]
    public partial class Answerss
    {
        public int id { get; set; }
        public int QuestionId { get; set; }
        public List<Answerss> orderAnswera { get; set; }
        public string TextOtv { get; set; }
        public bool AnswerCorrect { get; set; }
        public int? Porydoc { get; set; }
    }
    [Serializable]
    public partial class Proverka
    {
        public string textO { get; set; }
        public bool pravda { get; set; }

    }
    [Serializable]
    public partial class FLAGLAR
    {
        public int IDvoprosa { get; set; }
        public List<bool> flagiotvetov { get; set; }
        public string textquestion { get; set; }
    }


    [Serializable]
    public partial class ResultClassMy
    {
        public int QuestionId { get; set; }
        public bool Result { get; set; }
    }

    [Serializable]
    public partial class Questionss
    {
        public int ID { get; set; }
        public List<Answerss> answerss { get; set; }
        public string QuestName { get; set; }
        public int? enumq { get; set; }

    }

    [Serializable]
    public partial class WebForm1 : System.Web.UI.Page
    {

        testdb2Entities obj = new testdb2Entities();

        protected void Page_Init(object sender, EventArgs e)
        {
            var testId = Convert.ToInt32(Request["testId"]);
            Session["testId"] = testId;
            if (Session["testId"] != null) testId = (int)Session["testId"];
            Label1.Text = obj.TEST.Single(p => p.TESTID == testId).TESTTITLE;
            if (Session["quest_list"] == null)
            {
                var questions = obj.QUESTION.Where(q => q.TESTID == testId);
                var sa = new List<int>();
                //int i = 0;
                foreach (var question in questions)
                {
                    sa.Add((int)question.QUESTIONID);
                    //i++;
                    //if (i == 2) break;
                }
                //var a15 = Operation.GetRandomValues(sa, 15);
                Answerss answerss = new Answerss();

                Questionss questionss = new Questionss();
                List<Answerss> answ_list = new List<Answerss>();
                List<Questionss> quest_list = new List<Questionss>();
                foreach (var itemSa in sa)
                {
                    var QuestionId = itemSa;
                    var QuestionText = obj.QUESTION.Single(h => h.QUESTIONID == QuestionId).QUESTIONTEXT;//текст вопроса                    
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
            Timer1.Tick += Timer1_Tick;
            int galka = 0;
            Session["galka"] = null;
            if (Session["Index"] == null)
                Session["Index"] = 0;
            if (Session["AnswerAll"] != null)
            {
                //  Buttonnaekz.Enabled = true;
            }
            /***------------------------------------------блок списка вопросов----------------------------------***/
            var questId = Convert.ToInt32(Request["questId"]);
            try
            {
                if (Session["quest_list"] != null)
                {
                    int r = 1;
                    int i = 0;
                    var Qest_list = (List<Questionss>)Session["quest_list"];
                    var Answ_list = (List<Answerss>)Session["answ_list"];
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
                            Text = string.Concat("Вопрос ", r)// t.ID.ToString() + "--"
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

                    /*-------------------------------------------блок выводa name вопроса-------------------------------------------*/
                    foreach (var item in Qest_list)
                    {
                        if (item.ID == questId)
                        {
                            LabelVoprosa.Text = "Вопрос: " + " " + item.QuestName;//вывод текста вопроса + item.ID +
                            LabelVoprosa.ForeColor = System.Drawing.Color.Black;
                            LabelVoprosa.Font.Bold = true;

                        }
                        continue;

                    }
                    /*------------------------------------------блок вывода ответов-------------------------------------------*/
                    int f = 0;
                    int l = 1;
                    int ses = 0;
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
                    foreach (var c in Answ_list)
                    {

                        if (c.QuestionId == questId && c.Porydoc == null)
                        {

                            foreach (var d in Perem_otv_list)
                            {
                                var chk = new CheckBox//создание чкбоксов
                                {
                                    BackColor = System.Drawing.Color.Yellow,
                                    Text = d,
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

                        if (c.AnswerCorrect == true && c.Porydoc != null && c.QuestionId == questId)
                        {
                            if (Session["galka"] == null)
                            {
                                Panel2.Controls.Add(new LiteralControl("<table border='1'>"));

                            }
                            TextBoxVariant.Visible = true;
                            Primer.Visible = true;
                            //ButtonPOrIzmen.Visible = true;
                            Panel2.Controls.Add(new LiteralControl("<tr><td><span style='font-weight: bold; color: black;'>"));
                            var llab = new Label
                            {
                                BackColor = System.Drawing.Color.Yellow,
                                Text = c.TextOtv,
                                ID = "llab" + f.ToString(),

                            };
                            llab.Width = 300;
                            llab.Font.Size = 14;
                            llab.ForeColor = System.Drawing.Color.Black;
                            llab.BorderColor = System.Drawing.Color.Black;
                            llab.Font.Name = "Calibri";
                            Panel2.Controls.Add(llab);
                            Panel2.Controls.Add(new LiteralControl("</span></td>"));
                            f++; Session["galka"] = ++galka;
                            // continue;
                        }

                        if (c.AnswerCorrect == false && c.Porydoc != null && c.QuestionId == questId)
                        {
                            if (Session["ss"] == null) { Session["ss"] = ses; }
                            Panel2.Controls.Add(new LiteralControl("<td><span style='font-weight: bold; color: black;'>"));

                            var labf = new Label//создание чeкбоксов
                            {
                                BackColor = System.Drawing.Color.Yellow,
                                Text = "[" + l + "]" + c.TextOtv,
                                ID = "chk" + l.ToString(),
                            };
                            labf.Width = 300;
                            labf.Font.Size = 14;
                            labf.ForeColor = System.Drawing.Color.Black;
                            labf.BorderColor = System.Drawing.Color.Black;
                            labf.Font.Name = "Calibri";
                            Panel2.Controls.Add(labf);
                            l++;
                            Panel2.Controls.Add(new LiteralControl("</span></td></tr>"));
                            Session["galka"] = ++galka;
                        }


                        if (Session["galka"] != null)
                        {
                            Panel2.Controls.Add(new LiteralControl("</table>"));
                            Session["galka"] = null;
                        }



                    }
                }
            }
            catch (Exception er)
            {
                string err = er.Message;
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
            FLAGLAR flagi = new FLAGLAR();
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
            /*...........................если обычный вопрос..................................................... */
            if (Session["ss"] == null)
            {

                var chks = new List<Proverka>();
                //  var text_otveta = new List<string>();
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
                // var texts = Answ_list.Where(s => s.QuestionId == questId).Select(s => s.TextOtv);

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

                //foreach (var flag in flags)
                //{ 
                //    if (flag == true)
                //    {
                //       // (FindControl("chk" + t.ToString()) as CheckBox).BackColor = System.Drawing.Color.AliceBlue;
                //    }
                //    else
                //    {
                //      //  (FindControl("chk" + t.ToString()) as CheckBox).BackColor = System.Drawing.Color.Yellow;
                //    }
                //    if (flag != chks[t])
                //    {
                //        counter++;
                //    }
                //    t++;

                //}                
                flagi = new FLAGLAR { IDvoprosa = questId, flagiotvetov = chks.Select(d => d.pravda).ToList(), textquestion = textquesta };
                List<FLAGLAR> Flag_list;
                if (Session["chks"] == null)
                {
                    Flag_list = new List<FLAGLAR>();
                }
                else
                {
                    Flag_list = (List<FLAGLAR>)Session["chks"];
                }
                Flag_list.Add(flagi);
                Session["chks"] = Flag_list;

                var result = false;

                if (counter > 0)
                {
                    // Result.ForeColor = System.Drawing.Color.Red;
                    //Result.Text = "Неправильный ответ";
                    ab++;
                    Session["Answerbad"] = ab;
                }
                else
                {
                    ag++;
                    Session["Answergood"] = ag;

                    // Result.ForeColor = System.Drawing.Color.Green;
                    // Result.Text = "Правильный ответ";

                    result = true;
                }

                aa = ag + ab;

                Session["AnswerAll"] = aa;

                var element = new ResultClassMy { QuestionId = questId, Result = result };

                List<ResultClassMy> element_list = new List<ResultClassMy>();
                element_list.Add(element);
                if (Session["Values"] == null)
                {
                    Session["Values"] = element_list;
                }
                else
                {
                    (Session["Values"] as List<ResultClassMy>).Add(element);
                }
            }
            /*................................необычный вопрос.....................................................................*/
            //else
            //{

            //    var porydk = obj.Answers.Where(hj => hj.idpart == testId && hj.id_questions == questId && hj.order != null && hj.flag == false).Select(hj => hj.order);
            //    var pop = porydk.Count();
            //    var textb = TextBoxVariant.Text;
            //    int p = 0;
            //    string[] split = textb.Split(new Char[] { ' ', ',', '.', ':' });
            //    var cou = split.Count();
            //    /*..........................если кол-во ответов совпадает с эталонным количеством...............................*/
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
            //        flagi = new FLAGLAR { IDvoprosa = questId, textquestion = textquesta };
            //        List<FLAGLAR> Flag_list;
            //        if (Session["chks"] == null)
            //        {
            //            Flag_list = new List<FLAGLAR>();

            //        }
            //        else
            //        {
            //            Flag_list = (List<FLAGLAR>)Session["chks"];
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
            //        var element = new ResultClassMy { QuestionId = questId, Result = result };
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
            //        var element = new ResultClassMy { QuestionId = questId, Result = result };
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
            if (aa == 15)
            {
                Labeltimer.Text = (finishTime - DateTime.Now).ToString(@"mm\:ss");
                if (Session["finishTime"] != null) Session["finishTime"] = null;
                Timer1.Enabled = false;

                //  Response.Redirect("Login.aspx?testId=" + testId);
            }
            try
            {
                questId = Qest_list.AsEnumerable().ElementAt(index).ID;
            }
            catch { }
            string url = "EkzQuest.aspx?testId=" + testId + "&questId=" + questId;

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
                //  Response.Redirect("Login.aspx?partd="+testId);
            }

            Labeltimer.Text = (finishTime - DateTime.Now).ToString(@"mm\:ss");
            UpdatePanel3.Update();
        }

        protected void Buttonnaekz_Click(object sender, EventArgs e)
        {
            var testId = Convert.ToInt32(Request["testId"]);
            Response.Redirect("Login.aspx?testId=" + testId);
        }

        protected void ButtonPOrIzmen_Click(object sender, EventArgs e)
        {
            var testId = Convert.ToInt32(Request["testId"]);
            var questId = Convert.ToInt32(Request["questId"]);
            string url = "EkzQuest.aspx?testId=" + testId + "&questId=" + questId;
            Response.Redirect(url);
        }

        protected void TextBoxVariant_TextChanged(object sender, EventArgs e)
        {
            AnswerButton.Enabled = false;
            if (TextBoxVariant.Text != null)
            {
                AnswerButton.Enabled = true;
            }
            UpdatePanel2.Update();
        }
    }
}