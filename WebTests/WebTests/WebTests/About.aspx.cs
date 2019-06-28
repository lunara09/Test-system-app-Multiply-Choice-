using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WebTests.data;

namespace WebTests
{
    public partial class About : System.Web.UI.Page
    {
        //jurnal_rabEntities jurnal = new jurnal_rabEntities();
        testdb2Entities obt = new testdb2Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            var testId = Convert.ToInt32(Request["testId"]);
            if (Session["answ_list"] != null && Session["quest_list"] != null)
            {
                List<int> List_Porydkov;
                List<int> Non_list;
                string[] tr;
                int elmenporydka = 0;
                var textporudka = "";
                int indexpo = 0;
                int indexponon = 0;
                int textpore = 0;
                int noorder = 0;
                int[] hbc;
                int otvetindex = 0;
                var enums = Session["Values"] as List<ResultClassMy>;
                var Qest_list = (List<Questionss>)Session["quest_list"];
                var Answ_list = (List<Answerss>)Session["answ_list"];
                var Flag_list = (List<WebTests.Answers1.FLAGLARsew>)Session["chks"];
                //  var login = (string)Session["login"];
                int countqest = 0;
                var countans = 0;
                if (Flag_list != null)
                {
                    countans = Flag_list.Count();
                }
                else { Response.Redirect("Default.aspx"); }
                if (Qest_list != null)
                {
                    countqest = Qest_list.Count();
                }
                else { Response.Redirect("Default.aspx"); }
                int ab = Convert.ToInt32(Session["Answerbad"]);
                var ag = Convert.ToInt32(Session["Answergood"]);
                int aa = (int)Session["AnswerAll"];
                double procent = 0;
                if (countqest != 0)
                {
                    procent = ((double)ag / countqest) * 100;
                    procent = Math.Round(procent, 0);
                    LabelProcent.Text = "Процент правильных ответов: " + procent.ToString() + "%";
                }
                if (procent >= 80)
                {
                    Label1.Text = " Результат : Сдал";
                    Session["Answergood"] = null;
                    Session["Answerbad"] = null;
                }
                else
                {
                    Label1.Text = "Результат : Не сдал ";

                    Session["Answergood"] = null;
                    Session["Answerbad"] = null;
                }


                //  var person = jurnal.login.Single(j => j.login1 == login);
                string nameSd = (string)Session["loginSd"];
                var namedol = (string)Session["dolgSd"];
                //jurnal.dol.Single(d => d.id == person.dol).dol1;
                var nameotd = (string)Session["otdSd"];
                //jurnal.otd.Single(o => o.id == person.otdel).otd1;
                //  LabeChlenComis.Text = "Член комисии:_____________________________________ ";// +login;
                Labelfio.Text = "ФИО сдающего:  " + nameSd;
                Session["person"] = nameSd;
                Labeltod.Text = "Отдел:  " + nameotd;
                Labeldol.Text = "Должность:  " + namedol;
                int qа = 1;
                LabelPart.Text = "Раздел тестирования:  " + obt.TEST.Single(p => p.TESTID == testId).TESTTITLE;
                Labeldate.Text = "_________________";// DateTime.Now.ToString();
                LabelFoterSday.Text = "Подпись сдающего   ______________________   ";
                LabelFoterKomis.Text = "Член комиссии      ______________________   ";
                Panel1.Controls.Add(new LiteralControl("<table border='0'>"));

                if (countans == countqest)         //кол-во отвеч.вопросов и кол-во сформирово.вопросов
                {
                    foreach (var question in Qest_list)
                    {

                        if (question.enumq == null)  //вопрос обычный
                        {
                            var result = enums.Single(en => en.QuestionId == question.ID).Result;
                            var header = result == true ? "Вопрос: " + qа.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Правильный ответ" :
                            "Вопрос: " + qа.ToString() + "|,Раздел: " + testId + ",Вопрос: " + question.ID.ToString() + "  Неправильный ответ";
                            var index = 0;
                            Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                            Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                            Panel1.Controls.Add(new LiteralControl("<tr><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                            var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId });
                            foreach (var answer in answers)
                            {
                                if (answer.idvopros == question.ID)
                                {
                                    Panel1.Controls.Add(new LiteralControl("<tr>"));
                                    if (answer.correctAnswer == true)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));//Эталон (*)

                                    }
                                    else
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));

                                    }

                                    var it_answered = Flag_list.Where(l => l.IDvoprosa == question.ID).Select(l => l.flagiotvetov);

                                    foreach (var hb in it_answered)
                                    {
                                        if (hb[index] == true)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>(*)</td>"));// ОТвет(*)
                                        }
                                        else
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>()</td>"));//Ответ ()
                                        }
                                        break;
                                    }

                                    foreach (var idg in Flag_list)
                                    {
                                        if (idg.IDvoprosa == question.ID)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='height: 50px; vertical-align: top; width: 90%; margin: 0;'> " + answer.Text + "</span></td>"));
                                            Panel1.Controls.Add(new LiteralControl("</tr>"));
                                            break;
                                        }
                                        else continue;
                                    }
                                    index++;
                                }
                                else
                                    continue;
                            }
                            qа++;
                        }
                        /*...................................Если вопрос необычный ситуация со всеми ответами...............................................*/
                        else
                        {
                            var result = enums.Single(en => en.QuestionId == question.ID).Result;
                            var header = result == true ? "Вопрос: " + qа.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Правильный ответ" :
                            "Вопрос: " + qа.ToString() + "|,Раздел: " + testId + ",Вопрос: " + question.ID.ToString() + "  Неправильный ответ";

                            Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                            Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                            Panel1.Controls.Add(new LiteralControl("<tr><td>Термин</td><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                            var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId, porydk = q.Porydoc });

                            foreach (var answer in answers)
                            {
                                if (answer.idvopros == question.ID)
                                {
                                    if (Session["lispor"] == null)
                                    {

                                        Non_list = new List<int>();
                                        List_Porydkov = new List<int>();
                                        foreach (var lispor in answers)
                                        {
                                            if (lispor.porydk < 10 && lispor.idvopros == question.ID)
                                            {
                                                noorder = (int)lispor.porydk;
                                                elmenporydka = (int)lispor.porydk;
                                                List_Porydkov.Add(elmenporydka);
                                                Non_list.Add(noorder);
                                            }
                                        }
                                        hbc = new int[List_Porydkov.Count()];
                                        tr = new string[List_Porydkov.Count()];
                                        List_Porydkov.Sort();
                                        Session["lispor"] = List_Porydkov;
                                        Session["nonlist"] = Non_list;
                                        Session["tr"] = tr;
                                        Session["hbc"] = hbc;
                                    }
                                    else
                                    {
                                        List_Porydkov = (List<int>)Session["lispor"];
                                        Non_list = (List<int>)Session["nonlist"]; tr = (string[])Session["tr"];
                                        hbc = (int[])Session["hbc"];
                                    }
                                    if (Session["str"] == null)
                                    {
                                        foreach (var listd in answers)
                                        {
                                            if (listd.porydk < 10 && listd.idvopros == question.ID)
                                            {
                                                tr[textpore] = listd.Text;
                                                textpore++;
                                            }
                                        }
                                        textpore = 0;
                                        Session["str"] = tr;
                                        var it_answered = Flag_list.Single(l => l.IDvoprosa == question.ID).order;//как ответил

                                        foreach (var hb in it_answered)
                                        {
                                            hbc[otvetindex] = hb;
                                            otvetindex++;
                                        }
                                        otvetindex = 0;
                                    }

                                    if (Session["term"] == null)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<tr>"));
                                    }
                                    if (answer.correctAnswer == true && answer.porydk > 10) //если это термин
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:20%;margin:0;'>" + answer.Text + "</td> "));//текст термина
                                        int term = 1;
                                        Session["term"] = term;
                                        continue;

                                    }
                                    else
                                    {
                                        while (Non_list[indexponon] != List_Porydkov[indexpo])
                                        {
                                            indexponon++;
                                            textpore++;
                                        }
                                        if (Non_list[indexponon] == List_Porydkov[indexpo])
                                        {
                                            textporudka = tr[textpore];
                                            textpore = 0;
                                            indexponon = 0;

                                        }

                                        Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));//текст эталона
                                        Session["term"] = null;


                                    }

                                    while (hbc[otvetindex] != List_Porydkov[indexpo])
                                    {
                                        otvetindex++;
                                        textpore++;
                                    }
                                    if (hbc[otvetindex] == List_Porydkov[indexpo])
                                    {
                                        textporudka = tr[textpore];
                                        textpore = 0;
                                        otvetindex = 0;

                                        Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));// ОТвет(*)

                                    }
                                    indexpo++;
                                }
                                else
                                    continue;
                            }
                            Session["lispor"] = null;
                            Session["nonlist"] = null;
                            Session["str"] = null; Session["hbc"] = null;
                            qа++; indexpo = 0;
                        }
                        //}
                    }
                    Panel1.Controls.Add(new LiteralControl("</table>"));
                }

                /*................когда нету ответов на все вопросы ..................................................................*/

                else
                {
                    int[] Fcontin = new int[countqest];
                    int[] Qconec = new int[countqest];
                    int sc = 0;
                    int qew = 0;
                    foreach (var hj in Flag_list)
                    {
                        Fcontin[sc] = hj.IDvoprosa;  //массив отвеченных вопросов

                        sc++;
                    }
                    int idquestionsa = 1;
                    int indexd = 0;
                    int FIN = 0;
                    foreach (var quest in Qest_list)
                    {
                        Qconec[qew] = quest.ID;    //массив сформировавшихся вопросов
                        qew++;

                    }
                    foreach (var question in Qest_list)   //поиск вопросов
                    {
                        if (Qconec[indexd] == Fcontin[FIN])//когда отвеч.вопрос совпадает с сформировавшимся
                        {
                            if (question.enumq == null) //обычный вопрос
                            {
                                var result = enums.Single(en => en.QuestionId == Fcontin[FIN]).Result;
                                var header = result == true ? "Вопрос: " + idquestionsa.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Правильный ответ" :
                               "Вопрос: " + idquestionsa.ToString() + "|,Раздел: " + testId + ",Вопрос: " + question.ID.ToString() + "  Неправильный ответ";


                                var index = 0;
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                                var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId });

                                foreach (var answer in answers)
                                {
                                    if (answer.idvopros == question.ID)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<tr>"));
                                        if (answer.correctAnswer == true)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));//Эталон (*)

                                        }
                                        else
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));//Эталон()

                                        }
                                        var it_answered = Flag_list.Where(l => l.IDvoprosa == question.ID).Select(l => l.flagiotvetov);
                                        foreach (var hb in it_answered)
                                        {
                                            if (hb[index] == true)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>(*)</td>"));// ОТвет(*)
                                            }
                                            else
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>()</td>"));//Ответ ()
                                            }
                                            break;
                                        }

                                        foreach (var idg in Flag_list)
                                        {
                                            if (idg.IDvoprosa == question.ID)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='height: 50px; vertical-align: top; width: 90%; margin: 0;'> " + answer.Text + "</span></td>"));
                                                Panel1.Controls.Add(new LiteralControl("</tr>"));
                                                break;
                                            }
                                            else continue;
                                        }
                                        index++;
                                    }
                                    else
                                        continue;
                                }
                                idquestionsa++;
                                if (indexd < countqest - 1)
                                {
                                    indexd++;
                                }
                                else
                                {
                                    break;
                                }
                                FIN++;
                                continue;
                            }
                            else  //необычный вопрос
                            {
                                var result = enums.Single(en => en.QuestionId == Fcontin[FIN]).Result;
                                var header = result == true ? "Вопрос: " + idquestionsa.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Правильный ответ" :
                                "Вопрос: " + idquestionsa.ToString() + "|,Раздел: " + testId + ",Вопрос: " + question.ID.ToString() + "  Неправильный ответ";

                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td>Термин</td><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                                var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId, porydk = q.Porydoc });

                                foreach (var answer in answers)
                                {
                                    if (answer.idvopros == question.ID)
                                    {
                                        if (Session["lispor"] == null)
                                        {

                                            Non_list = new List<int>();
                                            List_Porydkov = new List<int>();
                                            foreach (var lispor in answers)
                                            {
                                                if (lispor.porydk < 10 && lispor.idvopros == question.ID)
                                                {
                                                    noorder = (int)lispor.porydk;
                                                    elmenporydka = (int)lispor.porydk;
                                                    List_Porydkov.Add(elmenporydka);
                                                    Non_list.Add(noorder);

                                                }
                                            }
                                            hbc = new int[List_Porydkov.Count()];
                                            tr = new string[List_Porydkov.Count()];
                                            List_Porydkov.Sort();
                                            Session["lispor"] = List_Porydkov;
                                            Session["nonlist"] = Non_list;
                                            Session["tr"] = tr;
                                            Session["hbc"] = hbc;
                                        }
                                        else
                                        {
                                            List_Porydkov = (List<int>)Session["lispor"];
                                            Non_list = (List<int>)Session["nonlist"]; tr = (string[])Session["tr"];
                                            hbc = (int[])Session["hbc"];
                                        }
                                        if (Session["str"] == null)
                                        {
                                            foreach (var listd in answers)
                                            {
                                                if (listd.porydk < 10 && listd.idvopros == question.ID)
                                                {
                                                    tr[textpore] = listd.Text;
                                                    textpore++;
                                                }
                                            }
                                            textpore = 0;
                                            Session["str"] = tr;
                                            var it_answered = Flag_list.Single(l => l.IDvoprosa == question.ID).order;//как ответил

                                            foreach (var hb in it_answered)
                                            {
                                                hbc[otvetindex] = hb;
                                                otvetindex++;
                                            }
                                            otvetindex = 0;
                                        }
                                        if (Session["term"] == null)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<tr>"));
                                        }
                                        if (answer.correctAnswer == true && answer.porydk > 10) //если это термин
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:20%;margin:0;'>" + answer.Text + "</td> "));//текст термина
                                            int term = 1;
                                            Session["term"] = term;
                                            continue;

                                        }
                                        else
                                        {
                                            while (Non_list[indexponon] != List_Porydkov[indexpo])
                                            {
                                                indexponon++;
                                                textpore++;
                                            }
                                            if (Non_list[indexponon] == List_Porydkov[indexpo])
                                            {
                                                textporudka = tr[textpore];
                                                textpore = 0;
                                                indexponon = 0;
                                            }
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));//текст эталона
                                            Session["term"] = null;
                                        }
                                        while (hbc[otvetindex] != List_Porydkov[indexpo])
                                        {
                                            otvetindex++;
                                            textpore++;
                                        }
                                        if (hbc[otvetindex] == List_Porydkov[indexpo])
                                        {
                                            textporudka = tr[textpore];
                                            textpore = 0;
                                            otvetindex = 0;
                                            Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));// ОТвет(*)

                                        }
                                        indexpo++;
                                    }
                                    else continue;
                                }
                                Session["lispor"] = null;
                                Session["nonlist"] = null;
                                Session["str"] = null;
                                Session["hbc"] = null;
                                indexpo = 0;
                            }
                            if (indexd < countqest - 1)
                            {
                                indexd++;
                            }
                            else
                            {
                                break;
                            }
                            FIN++;
                            continue;
                        }
                        while (Qconec[indexd] != Fcontin[FIN])  //поиск вопроса
                        {
                            FIN++;
                            if (Fcontin[FIN] == 0) //вопрос не найден  списке отвеченных
                            {
                                if (question.enumq == null)  //обычный вопрос 
                                {
                                    var header = "Вопрос: " + idquestionsa.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Ответ не выбран";
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                                    var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId });
                                    foreach (var answer in answers)
                                    {
                                        if (answer.idvopros == question.ID)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<tr>"));
                                            if (answer.correctAnswer == true)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));//Эталон (*)

                                            }
                                            else
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));//Эталон()

                                            }
                                            Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>-</td>"));// ОТвет(*)
                                            Panel1.Controls.Add(new LiteralControl("<td style='height: 50px; vertical-align: top; width: 90%; margin: 0;'> " + answer.Text + "</span></td>"));
                                            Panel1.Controls.Add(new LiteralControl("</tr>"));
                                        }
                                        else
                                            continue;
                                    }
                                    idquestionsa++;
                                    FIN = 0;
                                    if (indexd < countqest - 1)
                                    {
                                        indexd++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    break;
                                }

                                else //необычный вопрос когда неотвечен вопрос
                                {

                                    var header = "Вопрос: " + idquestionsa.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Ответ не выбран";
                                    // var index = 0;
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                    // Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td>Термин</td><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                                    var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId, porydk = q.Porydoc });
                                    foreach (var answer in answers)
                                    {
                                        if (answer.idvopros == question.ID)
                                        {
                                            if (Session["lispor"] == null)
                                            {

                                                Non_list = new List<int>();
                                                List_Porydkov = new List<int>();
                                                foreach (var lispor in answers)
                                                {
                                                    if (lispor.porydk < 10 && lispor.idvopros == question.ID)
                                                    {
                                                        noorder = (int)lispor.porydk;
                                                        elmenporydka = (int)lispor.porydk;
                                                        List_Porydkov.Add(elmenporydka);
                                                        Non_list.Add(noorder);

                                                    }
                                                }
                                                hbc = new int[List_Porydkov.Count()];
                                                tr = new string[List_Porydkov.Count()];
                                                List_Porydkov.Sort();
                                                Session["lispor"] = List_Porydkov;
                                                Session["nonlist"] = Non_list;
                                                Session["tr"] = tr;
                                                Session["hbc"] = hbc;
                                            }
                                            else
                                            {
                                                List_Porydkov = (List<int>)Session["lispor"];
                                                Non_list = (List<int>)Session["nonlist"]; tr = (string[])Session["tr"];
                                                hbc = (int[])Session["hbc"];
                                            }
                                            if (Session["str"] == null)
                                            {
                                                foreach (var listd in answers)
                                                {
                                                    if (listd.porydk < 10 && listd.idvopros == question.ID)
                                                    {
                                                        tr[textpore] = listd.Text;
                                                        textpore++;
                                                    }
                                                }
                                                textpore = 0;
                                                Session["str"] = tr;
                                                otvetindex = 0;
                                            }
                                            if (Session["term"] == null)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<tr>"));
                                            }
                                            if (answer.correctAnswer == true && answer.porydk > 10) //если это термин
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:20%;margin:0;'>" + answer.Text + "</td> "));//текст термина
                                                int term = 1;
                                                Session["term"] = term;
                                                continue;
                                            }
                                            else
                                            {
                                                while (Non_list[indexponon] != List_Porydkov[indexpo])
                                                {
                                                    indexponon++;
                                                    textpore++;
                                                }
                                                if (Non_list[indexponon] == List_Porydkov[indexpo])
                                                {
                                                    textporudka = tr[textpore];
                                                    textpore = 0;
                                                    indexponon = 0;
                                                }
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));//текст эталона
                                                Session["term"] = null;
                                            }
                                            indexpo++;
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:40%;margin:0;'>-</td>"));//текст 
                                            Session["term"] = null;
                                        }
                                        else
                                            continue;
                                    }
                                    Session["lispor"] = null;
                                    Session["nonlist"] = null;
                                    Session["str"] = null; Session["hbc"] = null;
                                    idquestionsa++; indexpo = 0;
                                    FIN = 0;
                                    if (indexd < countqest - 1)
                                    {
                                        indexd++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    break;
                                }

                            }
                            if (Qconec[indexd] == Fcontin[FIN])//вопрос найден в списке отвеченных
                            {
                                if (question.enumq == null)
                                {
                                    var result = enums.Single(en => en.QuestionId == Qconec[indexd]).Result;
                                    var header = result == true ? "Вопрос: " + idquestionsa.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Правильный ответ" :
                                   "Вопрос: " + idquestionsa.ToString() + "|,Раздел: " + testId + ",Вопрос: " + question.ID.ToString() + "  Неправильный ответ";

                                    var index = 0;
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                                    var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId });
                                    foreach (var answer in answers)
                                    {
                                        if (answer.idvopros == question.ID)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<tr>"));
                                            if (answer.correctAnswer == true)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));//Эталон (*)

                                            }
                                            else
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));//Эталон()

                                            }

                                            var it_answered = Flag_list.Where(l => l.IDvoprosa == question.ID).Select(l => l.flagiotvetov);

                                            foreach (var hb in it_answered)
                                            {
                                                if (hb[index] == true)
                                                {
                                                    Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>(*)</td>"));// ОТвет(*)
                                                }
                                                else
                                                {
                                                    Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>()</td>"));//Ответ ()
                                                }
                                                break;
                                            }

                                            foreach (var idg in Flag_list)
                                            {
                                                if (idg.IDvoprosa == question.ID)
                                                {
                                                    Panel1.Controls.Add(new LiteralControl("<td style='height: 50px; vertical-align: top; width: 90%; margin: 0;'> " + answer.Text + "</span></td>"));
                                                    Panel1.Controls.Add(new LiteralControl("</tr>"));
                                                    break;
                                                }
                                                else continue;
                                            }
                                            index++;
                                        }
                                        else
                                            continue;
                                    }
                                    idquestionsa++;
                                    FIN = 0;
                                    if (indexd < countqest - 1)
                                    {
                                        indexd++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else //необычный вопрос когда вопрос нашелся
                                {
                                    var result = enums.Single(en => en.QuestionId == Qconec[indexd]).Result;
                                    var header = result == true ? "Вопрос: " + idquestionsa.ToString() + "|,Раздел " + testId + ",Вопрос: " + question.ID.ToString() + "  Правильный ответ" :
                                    "Вопрос: " + idquestionsa.ToString() + "|,Раздел: " + testId + ",Вопрос: " + question.ID.ToString() + "  Неправильный ответ";
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                    Panel1.Controls.Add(new LiteralControl("<tr><td>Термин</td><td>Эталон</td><td>Ответ</td><td></td></tr>"));
                                    var answers = question.answerss.Select(q => new { correctAnswer = q.AnswerCorrect, Text = q.TextOtv, idvopros = q.QuestionId, porydk = q.Porydoc });
                                    foreach (var answer in answers)
                                    {
                                        if (answer.idvopros == question.ID)
                                        {
                                            if (Session["lispor"] == null)
                                            {
                                                Non_list = new List<int>();
                                                List_Porydkov = new List<int>();
                                                foreach (var lispor in answers)
                                                {
                                                    if (lispor.porydk < 10 && lispor.idvopros == question.ID)
                                                    {
                                                        noorder = (int)lispor.porydk;
                                                        elmenporydka = (int)lispor.porydk;
                                                        List_Porydkov.Add(elmenporydka);
                                                        Non_list.Add(noorder);
                                                    }
                                                }
                                                hbc = new int[List_Porydkov.Count()];
                                                tr = new string[List_Porydkov.Count()];
                                                List_Porydkov.Sort();
                                                Session["lispor"] = List_Porydkov;
                                                Session["nonlist"] = Non_list;
                                                Session["tr"] = tr;
                                                Session["hbc"] = hbc;
                                            }
                                            else
                                            {
                                                List_Porydkov = (List<int>)Session["lispor"];
                                                Non_list = (List<int>)Session["nonlist"]; tr = (string[])Session["tr"];
                                                hbc = (int[])Session["hbc"];
                                            }
                                            if (Session["str"] == null)
                                            {
                                                foreach (var listd in answers)
                                                {
                                                    if (listd.porydk < 10 && listd.idvopros == question.ID)
                                                    {
                                                        tr[textpore] = listd.Text;
                                                        textpore++;
                                                    }
                                                }
                                                textpore = 0;
                                                Session["str"] = tr;
                                                otvetindex = 0;
                                                var it_answered = Flag_list.Single(l => l.IDvoprosa == question.ID).order;//как ответил

                                                foreach (var hb in it_answered)
                                                {
                                                    hbc[otvetindex] = hb;
                                                    otvetindex++;
                                                }
                                                otvetindex = 0;

                                            }
                                            if (Session["term"] == null)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<tr>"));
                                            }
                                            if (answer.correctAnswer == true && answer.porydk > 10) //если это термин
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:20%;margin:0;'>" + answer.Text + "</td> "));//текст термина
                                                int term = 1;
                                                Session["term"] = term;
                                                continue;
                                            }
                                            else
                                            {
                                                while (Non_list[indexponon] != List_Porydkov[indexpo])
                                                {
                                                    indexponon++;
                                                    textpore++;
                                                }
                                                if (Non_list[indexponon] == List_Porydkov[indexpo])
                                                {
                                                    textporudka = tr[textpore];
                                                    textpore = 0;
                                                    indexponon = 0;
                                                }
                                                Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));//текст эталона
                                                Session["term"] = null;
                                            }
                                            while (hbc[otvetindex] != List_Porydkov[indexpo])
                                            {
                                                otvetindex++;
                                                textpore++;
                                            }
                                            if (hbc[otvetindex] == List_Porydkov[indexpo])
                                            {
                                                textporudka = tr[textpore];
                                                textpore = 0;
                                                otvetindex = 0;
                                                Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:40%;margin:0;'>" + textporudka + "</td>"));// ОТвет(*)

                                            }
                                            indexpo++;
                                        }
                                        else
                                            continue;
                                    }
                                    Session["lispor"] = null;
                                    Session["nonlist"] = null;
                                    Session["str"] = null; Session["hbc"] = null;
                                    idquestionsa++; indexpo = 0;
                                    FIN = 0;
                                    if (indexd < countqest - 1)
                                    {
                                        indexd++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }


                        }

                    }
                }
                Panel1.Controls.Add(new LiteralControl("</table>"));
            }
            var resultat = Session["resu"];
            string persofn = (string)Session["person"];
            string date = DateTime.Now.ToString();
            string insert = "INSERT into dbo.Protocols (login,result,date,testId) Values ('" + persofn + "'," + resultat + ",'" + date + "'," + testId + ")";
            string source = @"Data Source=eup;Initial Catalog = testdb;Persist Security Info=true;multipleactiveresultsets=True;User ID=sa;Password=контрольсмп;";
            SqlConnection connect = new SqlConnection(source);
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(insert, connect);
                int scalrreturned = cmd.ExecuteNonQuery();
                // SqlDataReader reader = cmd.ExecuteReader();
                connect.Close();
            }
            catch { }
            Session["resu"] = null;
            Session["person"] = null;
            if (Session["Index"] != null) Session["Index"] = null;
            if (Session["finishTime"] != null) Session["finishTime"] = null;
            if (Session["Answerbad"] != null) Session["Answerbad"] = null;
            if (Session["Answergood"] != null) Session["Answergood"] = null;
            if (Session["Values"] != null) Session["Values"] = null;
            if (Session["answ_list"] != null) Session["answ_list"] = null;
            if (Session["quest_list"] != null) Session["quest_list"] = null;
            if (Session["chks"] != null) Session["chks"] = null;
            if (Session["AnswerAll"] != null) Session["AnswerAll"] = null;
            Session["login"] = null;
            Session["loginSd"] = null;
            Session["dolgSd"] = null;
            Session["otdSd"] = null;
        }
    }
}