using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WebTests.data;
using WebTests.MyClasses;

namespace WebTests
{
    public partial class UserResult : System.Web.UI.Page
    {
        testdb2Entities obj = new testdb2Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            var testId = Convert.ToInt32(Request["testId"]);
            int loginId = Convert.ToInt32(Session["loginId"]);
            string date = DateTime.Now.ToString();
            DateTime dateNow = DateTime.Now;

            List<USERANSWERS> uas = new List<USERANSWERS>();

            if (Session["answ_list"] != null && Session["quest_list"] != null)
            {
                var enums = Session["Values"] as List<ResultClassMy>;
                var listQ = (List<Questionss>)Session["quest_list"];
                var listA = (List<Answerss>)Session["answ_list"];
                var listIsChecked = (List<FLAGLAR>)Session["chks"];
                int CountQ = 0;
                var CountA = 0;
                if (listIsChecked != null)
                {
                    CountA = listIsChecked.Count();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
                if (listQ != null)
                {
                    CountQ = listQ.Count();
                }
                else { Response.Redirect("Default.aspx"); }
                int ab = Convert.ToInt32(Session["Answerbad"]);
                var ag = Convert.ToInt32(Session["Answergood"]);
                int aa = (int)Session["AnswerAll"];
                double goodAnswerPrecent = 0;
                if (CountQ != 0)
                {
                    goodAnswerPrecent = ((double)ag / CountQ) * 100;
                    goodAnswerPrecent = Math.Round(goodAnswerPrecent, 0);
                    LabelProcent.Text = "Percentage of correct answers: " + goodAnswerPrecent.ToString() + "%";
                }
                if (goodAnswerPrecent >= 80)
                {
                    Label1.Text = " Result: Passed";
                    Session["Answergood"] = null;
                    Session["Answerbad"] = null;
                }
                else
                {
                    Label1.Text = "Result: Not passed";
                    Session["Answergood"] = null;
                    Session["Answerbad"] = null;
                }

                USER u = obj.USER.Where(l => l.USERID == loginId).First();
                LabelFn.Text = "First name:  " + u.FNAME;
                LabelLn.Text = "Last name:  " + u.LNAME;
                int qа = 1;
                LabelTest.Text = obj.TEST.Single(p => p.TESTID == testId).TESTTITLE;
                LabelDate.Text = DateTime.Now.ToString();
                Panel1.Controls.Add(new LiteralControl("<table border='0'>"));

                //try
                {
                    USERTEST ut = new USERTEST()
                    {
                        USERID = loginId,
                        TESTID = testId,
                        TESTDATE = dateNow,
                        USERTESTID = obj.USERTEST.Count() == 0 ? 1 : obj.USERTEST.Max(l => l.USERTESTID) + 1
                    };
                    obj.USERTEST.Add(ut);
                    obj.SaveChanges();
                    //int utId = obj.USERTEST.Where(l => l.USERID == loginId && l.TESTID == testId && l.TESTDATE == ut.).First().USERTESTID;
                    int utId = ut.USERTESTID;
                    foreach (var item in listIsChecked)
                    {
                        USERANSWERS ua = new USERANSWERS();
                        ua.USERTESTID = utId;
                        ua.QA_ID = obj.QA.Where(l => l.QUESTIONID == item.IdQuestion && l.ANSWERID == item.IdUserAnswer).First().QA_ID;
                        ua.USERANSWERSID = obj.USERANSWERS.Count() == 0 ? 1 : obj.USERANSWERS.Max(l => l.USERANSWERSID) + 1;
                        uas.Add(ua);
                        obj.USERANSWERS.Add(ua);
                        obj.SaveChanges();
                    }
                    //obj.USERANSWERS.AddRange(uas);
                    
                }
                //catch
                //{

                //}


                if (CountA == CountQ)
                {
                    foreach (var question in listQ)
                    {
                        var result = enums.Single(en => en.QuestionId == question.ID).Result;
                        var header = result == true ? "Question: " + qа.ToString() + "|,Test " + testId + ", Correct answer" :
                        "Question: " + qа.ToString() + "|,Test: " + testId + ", Incorrect answer";
                        var index = 0;
                        Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                        Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                        Panel1.Controls.Add(new LiteralControl("<tr><td>Correct answer</td><td>User Answer</td><td></td></tr>"));
                        var answers = question.answerss.Select(q => new { correctAnswer = q.IsAnswerCorrect, Text = q.AnswerText, idvopros = q.QuestionId });
                        foreach (var answer in answers)
                        {
                            if (answer.idvopros == question.ID)
                            {
                                Panel1.Controls.Add(new LiteralControl("<tr>"));
                                if (answer.correctAnswer == true)
                                {
                                    Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));

                                }
                                else
                                {
                                    Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));

                                }

                                var it_answered = listIsChecked.Where(l => l.IdQuestion == question.ID).Select(l => l.UserAnswerText);

                                foreach (var hb in it_answered)
                                {
                                    if (hb[index] == true)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>(*)</td>"));
                                    }
                                    else
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>()</td>"));//Ответ ()
                                    }
                                    break;
                                }

                                foreach (var idg in listIsChecked)
                                {
                                    if (idg.IdQuestion == question.ID)
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
                    Panel1.Controls.Add(new LiteralControl("</table>"));
                }
                // 
                else
                {
                    int[] arrAnswerdQuestions = new int[CountQ];
                    int[] arrAllQ = new int[CountQ];
                    int sc = 0;
                    int qew = 0;
                    foreach (var hj in listIsChecked)
                    {
                        arrAnswerdQuestions[sc] = hj.IdQuestion;
                        sc++;
                    }
                    int idquestionsa = 1;
                    int indexd = 0;
                    int FIN = 0;
                    foreach (var quest in listQ)
                    {
                        arrAllQ[qew] = quest.ID;
                        qew++;

                    }
                    foreach (var question in listQ)
                    {
                        if (arrAllQ[indexd] == arrAnswerdQuestions[FIN])
                        {
                            var result = enums.Single(en => en.QuestionId == arrAnswerdQuestions[FIN]).Result;
                            var header = result == true ? "Question: " + idquestionsa.ToString() + "|,Test " + testId + ", Correct answer" :
                           "Question: " + idquestionsa.ToString() + "|,Test: " + testId + ", Incorrect answer";


                            var index = 0;
                            Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                            Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                            Panel1.Controls.Add(new LiteralControl("<tr><td>Correct answer</td><td>Ответ</td><td></td></tr>"));
                            var answers = question.answerss.Select(q => new { correctAnswer = q.IsAnswerCorrect, Text = q.AnswerText, idvopros = q.QuestionId });

                            foreach (var answer in answers)
                            {
                                if (answer.idvopros == question.ID)
                                {
                                    Panel1.Controls.Add(new LiteralControl("<tr>"));
                                    if (answer.correctAnswer == true)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));//Correct answer (*)

                                    }
                                    else
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));//Correct answer()

                                    }
                                    var it_answered = listIsChecked.Where(l => l.IdQuestion == question.ID).Select(l => l.UserAnswerText);
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

                                    foreach (var idg in listIsChecked)
                                    {
                                        if (idg.IdQuestion == question.ID)
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
                            if (indexd < CountQ - 1)
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
                        while (arrAllQ[indexd] != arrAnswerdQuestions[FIN])  //поиск вопроса
                        {
                            FIN++;
                            if (arrAnswerdQuestions[FIN] == 0) //Question не найден  списке отвеченных
                            {
                                var header = "Question: " + idquestionsa.ToString() + "|,Test " + testId + ", No answer selected";
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td>Correct answer</td><td>Answer</td><td></td></tr>"));
                                var answers = question.answerss.Select(q => new { correctAnswer = q.IsAnswerCorrect, Text = q.AnswerText, idvopros = q.QuestionId });
                                foreach (var answer in answers)
                                {
                                    if (answer.idvopros == question.ID)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<tr>"));
                                        if (answer.correctAnswer == true)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));

                                        }
                                        else
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));

                                        }
                                        Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>-</td>"));
                                        Panel1.Controls.Add(new LiteralControl("<td style='height: 50px; vertical-align: top; width: 90%; margin: 0;'> " + answer.Text + "</span></td>"));
                                        Panel1.Controls.Add(new LiteralControl("</tr>"));
                                    }
                                    else
                                        continue;
                                }
                                idquestionsa++;
                                FIN = 0;
                                if (indexd < CountQ - 1)
                                {
                                    indexd++;
                                }
                                else
                                {
                                    break;
                                }
                                break;

                            }
                            // question found in the list of answered
                            if (arrAllQ[indexd] == arrAnswerdQuestions[FIN])
                            {
                                var result = enums.Single(en => en.QuestionId == arrAllQ[indexd]).Result;
                                var header = result == true ? "Question: " + idquestionsa.ToString() + "|,Test " + testId + ", Correct answer" :
                               "Question: " + idquestionsa.ToString() + "|,Test: " + testId + ", Incorrect answer";

                                var index = 0;
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span style='font-weight: bold; color: black;'>" + header + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td colspan='6'><span>" + question.QuestName + "</span></td></tr>"));
                                Panel1.Controls.Add(new LiteralControl("<tr><td>Correct answer</td><td>Ответ</td><td></td></tr>"));
                                var answers = question.answerss.Select(q => new { correctAnswer = q.IsAnswerCorrect, Text = q.AnswerText, idvopros = q.QuestionId });
                                foreach (var answer in answers)
                                {
                                    if (answer.idvopros == question.ID)
                                    {
                                        Panel1.Controls.Add(new LiteralControl("<tr>"));
                                        if (answer.correctAnswer == true)
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>(*)</td> "));//Correct answer (*)

                                        }
                                        else
                                        {
                                            Panel1.Controls.Add(new LiteralControl("<td style='vertical-align:top'>( )</td>"));//Incorrect answer()

                                        }

                                        var it_answered = listIsChecked.Where(l => l.IdQuestion == question.ID).Select(l => l.UserAnswerText);

                                        foreach (var hb in it_answered)
                                        {
                                            if (hb[index] == true)
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>(*)</td>"));// User correct answer(*)
                                            }
                                            else
                                            {
                                                Panel1.Controls.Add(new LiteralControl("<td style='height:50px;vertical-align:top;width:10%;margin:0;'>()</td>"));//User incorrect answer()
                                            }
                                            break;
                                        }

                                        foreach (var idg in listIsChecked)
                                        {
                                            if (idg.IdQuestion == question.ID)
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
                                if (indexd < CountQ - 1)
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
                Panel1.Controls.Add(new LiteralControl("</table>"));
            }
            var resultat = Session["resu"];
            
            Session["resu"] = null;
            if (Session["Index"] != null) Session["Index"] = null;
            if (Session["finishTime"] != null) Session["finishTime"] = null;
            if (Session["Answerbad"] != null) Session["Answerbad"] = null;
            if (Session["Answergood"] != null) Session["Answergood"] = null;
            if (Session["Values"] != null) Session["Values"] = null;
            if (Session["answ_list"] != null) Session["answ_list"] = null;
            if (Session["quest_list"] != null) Session["quest_list"] = null;
            if (Session["chks"] != null) Session["chks"] = null;
            if (Session["AnswerAll"] != null) Session["AnswerAll"] = null;
            //Session["login"] = null;
            //Session["loginSd"] = null;
            //Session["dolgSd"] = null;
            //Session["otdSd"] = null;
        }
    }
}