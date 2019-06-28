using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EditTest.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EditTest
{
    /// <summary>
    /// Логика взаимодействия для ShowResults.xaml
    /// </summary>
    public partial class ShowResults : MetroWindow
    {

        List<USERANSWERS> UaCollection;

        Model1 db = new Model1();

        public ShowResults()
        {
            InitializeComponent();
            Load();
        }

        public class Result
        {
            public string TESTTITLE { get; set; }
            public DateTime TESTDATE { get; set; }
            public string EMAIL { get; set; }
            public string FNAME { get; set; }
            public string LNAME { get; set; }
            public double PERCENT { get; set; }
        }

        public void Load()
        {
            UaCollection = new List<USERANSWERS>();
            var objs = db.USERANSWERS.ToList();
            foreach (var i in objs)
            {
                UaCollection.Add(
                    new USERANSWERS
                    {
                        USERTESTID = i.USERTESTID,
                        QA_ID = i.QA_ID
                    });
            }
            foreach (var itemUa in UaCollection)
            {
                var uts = db.USERTEST.Where(l => l.USERTESTID == itemUa.USERTESTID).ToList();
                foreach (var itemUt in uts)
                {
                    itemUa.USERTEST = new USERTEST()
                    {
                        USERTESTID = itemUt.USERTESTID,
                        USERID = itemUt.USERID,
                        TESTID = itemUt.TESTID,
                        TESTDATE = itemUt.TESTDATE
                    };
                    var u = db.USER.Where(l => l.USERID == itemUt.USERID).First();
                    itemUa.USERTEST.USER = new USER()
                    {
                        USERID = u.USERID,
                        LNAME = u.LNAME,
                        FNAME = u.LNAME,
                        EMAIL = u.EMAIL
                    };
                    var t = db.TEST.Where(l => l.TESTID == itemUt.TESTID).First();
                    itemUa.USERTEST.TEST = new TEST()
                    {
                        TESTID = t.TESTID,
                        TESTTITLE = t.TESTTITLE
                    };
                }
                var qas = db.QA.Where(l => l.QA_ID == itemUa.QA_ID).ToList();
                foreach (var itemQa in qas)
                {
                    itemUa.QA = new QA()
                    {
                        QA_ID = itemQa.QA_ID,
                        QUESTIONID = itemQa.QUESTIONID,
                        ANSWERID = itemQa.ANSWERID,
                        CORRECTANSWER = itemQa.CORRECTANSWER
                    };
                    var q = db.QUESTION.Where(l => l.QUESTIONID == itemQa.QUESTIONID).First();
                    itemUa.QA.QUESTION = new QUESTION()
                    {
                        QUESTIONID = q.QUESTIONID,
                        QUESTIONTEXT = q.QUESTIONTEXT
                    };
                    var a = db.ANSWER.Where(l => l.ANSWERID == itemQa.ANSWERID).First();
                    itemUa.QA.ANSWER = new ANSWER()
                    {
                        ANSWERID = a.ANSWERID,
                        ANSWERTEXT = a.ANSWERTEXT
                    };
                }
            }
            grid1.ItemsSource = UaCollection;

            List<Result> rs = new List<Result>();
            var temp = UaCollection.GroupBy(l => l.USERTESTID);
            foreach (var item in temp)
            {
                Result res = new Result();
                res.TESTTITLE = item.First().USERTEST.TEST.TESTTITLE;
                res.TESTDATE = item.First().USERTEST.TESTDATE.Value;
                res.EMAIL = item.First().USERTEST.USER.EMAIL;
                res.FNAME = item.First().USERTEST.USER.FNAME;
                res.LNAME = item.First().USERTEST.USER.LNAME;
                
                double nCa = 0;
                double nA = 0;
                foreach (var itemitem in item)
                {
                    nA++;
                    nCa += itemitem.QA.CORRECTANSWER == true ? 1 : 0;    
                }
                res.PERCENT = nCa / nA * 100;
                rs.Add(res);
                //res
                //res.TESTTITLE = item.USERTEST.TEST.TESTTITLE;
                //res.TESTDATE = item.USERTEST.TESTDATE.Value;
                //res.EMAIL = item.USERTEST.USER.EMAIL;
                //res.FNAME = item.USERTEST.USER.FNAME;
                //res.LNAME = item.USERTEST.USER.LNAME;
            }
            //var a = UaCollection.Where(l=>l.USERTESTID)
            //foreach (var item in ))
            //{

            //}
            gridShortRes.ItemsSource = rs;

        }
    }
}