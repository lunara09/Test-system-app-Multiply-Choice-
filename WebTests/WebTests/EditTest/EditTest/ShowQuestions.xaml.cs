using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для ShowQUESTION.xaml
    /// </summary>
    public partial class ShowQUESTION : MetroWindow
    {
        int _IdTest;
        private ViewModel _vm;
        int _IdQuestion;
        Model1 db = new Model1();
        List<TextBox> tbs = null;
        List<RadioButton> chbs = null;
        TextBox tbQuestion, tbQuestionNote;

        public ShowQUESTION(int id)
        {
            _vm = new ViewModel();
            this.DataContext = _vm;
            _IdTest = id;

            string filename = db.TEST.Where(d => d.TESTID == _IdTest).Select(d => d.TESTTITLE).FirstOrDefault();
            _vm.AppTitle = string.IsNullOrEmpty(filename) ? "Not defined" : filename;
            SplashScreen sp = new SplashScreen("load.jpg");
            sp.Show(true);
            InitializeComponent();
            SetLbQuestions();
        }

        private void ShowQuestionAndAnswers(IList<Object> q)
        {
            tbs = new List<TextBox>();
            chbs = new List<RadioButton>();

            var add = q;//e.AddedItems
            if(add.Count > 0)
            {
                var sel = q[0] as QUESTION;
                _IdQuestion = sel.QUESTIONID;
                stP.Children.Clear();
                StackPanel stphead = new StackPanel();
                stphead.Orientation = Orientation.Horizontal;
                Label lblQ = new Label { Content = "Question text", VerticalAlignment = VerticalAlignment.Bottom, FontSize = 14, Margin = new Thickness(0, 0, 0, 5) };
                tbQuestion = new TextBox { Text = sel.QUESTIONTEXT, FontSize = 14, Width = stP.ActualWidth / 2 -200, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10), Background = Brushes.Aqua };
                Label lblQN = new Label { Content = "Question note", VerticalAlignment = VerticalAlignment.Bottom, FontSize = 14, Margin = new Thickness(0, 0, 0, 5) };
                tbQuestionNote = new TextBox { Text = sel.QUESTIONNOTE, FontSize = 14, Width = stP.ActualWidth /2 - 200, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10), Background = Brushes.Aqua };
                stphead.Children.Add(lblQ);
                stphead.Children.Add(tbQuestion);
                stphead.Children.Add(lblQN);
                stphead.Children.Add(tbQuestionNote);

                //Label labNumber = new Label { Content = "№ " };
                Grid DynamicGrid = new Grid();
                DynamicGrid.Width = 400;
                DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
                DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
                DynamicGrid.ShowGridLines = true;
                DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
                ColumnDefinition gridCol1 = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(gridCol1);
                RowDefinition gridRow1 = new RowDefinition();
                gridRow1.Height = new GridLength(45);

                //DynamicGrid.Children.Add(labNumber);

                stP.Children.Add(DynamicGrid);
                stP.Children.Add(stphead);
                foreach (var t in sel.QA)
                {
                    StackPanel stp2 = new StackPanel();
                    stp2.Orientation = Orientation.Horizontal;
                    RadioButton chb = new RadioButton { Margin = new Thickness(20) };
                    chbs.Add(chb);
                    if (t.CORRECTANSWER.Value)
                    {
                        chb.IsChecked = true;
                    }
                    else
                    {
                        chb.IsChecked = false;
                    }
                    stp2.Children.Add(chb);

                    TextBox tb = new TextBox { Text = t.ANSWER.ANSWERTEXT, MaxLength = t.ANSWER.ANSWERTEXT.Length, TextWrapping = TextWrapping.Wrap, Width = stP.ActualWidth - 250, Margin = new Thickness(5), Background = Brushes.Yellow, FontSize = 14 };
                    Button btnDelete = new Button { Content = "Remove", Margin = new Thickness(10), CommandParameter = t.ANSWERID };
                    btnDelete.Click += BtnDelAns_Click;
                    tbs.Add(tb);

                    stp2.Children.Add(tb);
                    stp2.Children.Add(btnDelete);
                    stP.Children.Add(stp2);
                }
                Button bansw = new Button { Content = "Add answer", Margin = new Thickness(20), Width = 150 };
                bansw.Click += BtnAddAnsw_Click;
                stP.Children.Add(bansw);
                if (tbQuestion.Text != "")
                {
                    Button bdelete = new Button { Content = "Remove question", Margin = new Thickness(20), Width = 150, CommandParameter = _IdQuestion };
                    bdelete.Click += BtnDelQ_Click;
                    stP.Children.Add(bdelete);
                }

                Button b = new Button { Content = "Save", Margin = new Thickness(20), Width = 100 };
                b.Click += BtnSaveAllQuestion_Click;
                stP.Children.Add(b);
            }
        }

        void BtnDelQ_Click(object sender, RoutedEventArgs e)
        {
            Button delbut = (Button)sender;
            StackPanel sp = (StackPanel)delbut.Parent;
            int id = Convert.ToInt32(((Button)sender).CommandParameter);
            var q = db.QUESTION.Where(d => d.QUESTIONID == id && d.TESTID == _IdTest);
            QA qa = db.QA.Where(d => d.QUESTIONID == id).FirstOrDefault();
            List<USERANSWERS> ua = null;
            if (qa != null)
                 ua = db.USERANSWERS.Where(d => d.QA_ID == qa.QA_ID).ToList();
            if (q.Count() != 0)
            {
                try
                {
                    if (ua != null)
                    {
                        db.USERANSWERS.RemoveRange(ua);
                    }
                    if (qa != null)
                    {
                        db.QA.Remove(qa);
                    }
                    db.QUESTION.RemoveRange(q);
                    db.SaveChanges();

                }
                catch (Exception er)
                {
                    this.ShowMessageAsync("Error", er.Message + "Inner:" + er.InnerException);
                    return;

                }
                
                this.ShowMessageAsync("Success", "Successful saved");
                stP.Children.Remove(sp);
                SetLbQuestions();

            }
        }

        void SetLbQuestions()
        {
            LbQuestions.ItemsSource = null;
            var obj = db.QUESTION.Where(d => d.TESTID == _IdTest).OrderBy(f => f.QUESTIONID);
            List<QUESTION> listQuest = new List<QUESTION>();

            foreach (var f in obj)
            {
                QUESTION row = new QUESTION();
                row.QA = new List<QA>();
                row.TEST = new TEST();
                row.QUESTIONTEXT = f.QUESTIONTEXT;
                row.QUESTIONID = f.QUESTIONID;
                row.TEST.TESTID = _IdTest;
                var qa = db.QA.Where(l => l.QUESTIONID == f.QUESTIONID);
                foreach (var item in qa)
                {
                    var a = db.ANSWER.Where(l => l.ANSWERID == item.ANSWERID).First();
                    QA qaNew = new QA
                    {
                        ANSWERID = item.ANSWERID,
                        CORRECTANSWER = item.CORRECTANSWER,
                        QUESTIONID = row.QUESTIONID,
                        ANSWER = a
                    };
                    row.QA.Add(qaNew);
                }
                listQuest.Add(row);
            }
            LbQuestions.ItemsSource = listQuest;

        }

        void BtnDelAns_Click(object sender, RoutedEventArgs e)
        {
            Button BtnDel = (Button)sender;
            int idAns = Convert.ToInt32(BtnDel.CommandParameter);
            StackPanel sp = (StackPanel)BtnDel.Parent;
            var qa = db.QA.Where(l => l.ANSWERID == idAns);
            var a = db.ANSWER.Where(s => s.ANSWERID == idAns);
            if (a.Count() > 0)
            {
                try
                {
                    db.QA.RemoveRange(qa);
                    db.ANSWER.RemoveRange(a);
                    db.SaveChanges();

                }
                catch (Exception er)
                {
                    this.ShowMessageAsync("Error", er.Message + "Inner:" + er.InnerException);
                    return;
                }
            }
            foreach (UIElement elem in sp.Children)
            {
                if (elem is RadioButton)
                {
                    chbs.Remove((RadioButton)elem);
                }
                else if (elem is TextBox)
                {
                    tbs.Remove((TextBox)elem);
                }
            }
            stP.Children.Remove(sp);
            
            this.ShowMessageAsync("Success", "Successful saved");
            SetLbQuestions();
        }

        void BtnAddAnsw_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stp2 = new StackPanel();
            stp2.Orientation = Orientation.Horizontal;
            RadioButton chb = new RadioButton { Margin = new Thickness(20), GroupName="1" };
            chbs.Add(chb);
            chb.IsChecked = false;
            stp2.Children.Add(chb);
            TextBox tb = new TextBox { Text = "", TextWrapping = TextWrapping.Wrap, Width = stP.ActualWidth - 200, Margin = new Thickness(5), Background = Brushes.Yellow, FontSize = 14 };
            Button btnDelete = new Button { Content = "Remove", Margin = new Thickness(10) };
            btnDelete.Click += BtnDelAns_Click;
            tbs.Add(tb);
            stp2.Children.Add(tb);
            stp2.Children.Add(btnDelete);
            stP.Children.Insert(stP.Children.Count - 2, stp2);
        }

        private void listQuest_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ShowQuestionAndAnswers(e.AddedItems as IList<Object>);
        }

        void BtnSaveAllQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (tbQuestion.Text == "")
            {
                this.ShowMessageAsync("Error", "Enter text question");
                return;
            }
            int i = 0;

            if (tbs.Count() == 0)
            {
                this.ShowMessageAsync("Error", "Add ANSWER");
                return;
            }
            var questRepeat = db.QUESTION.Where(l => l.QUESTIONID == _IdQuestion).ToList();
            if (questRepeat.Count() > 0)
            {
                questRepeat.FirstOrDefault().QUESTIONTEXT = tbQuestion.Text;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception er)
                {
                    this.ShowMessageAsync("Error", "Error " + er.Message + "Inner:" + er.InnerException);
                    return;
                }
                //this.ShowMessageAsync("Success", "Successful saved");
            }
            else
            {
                try
                {
                    QUESTION row = new QUESTION
                    {
                        QUESTIONID = db.QUESTION.Count() == 0 ? 1 : db.QUESTION.Max(l => l.QUESTIONID) + 1,
                        TESTID = _IdTest,
                        QUESTIONTEXT = tbQuestion.Text,
                        QUESTIONNOTE = tbQuestionNote.Text
                    };
                    db.QUESTION.Add(row);
                    db.SaveChanges();
                    _IdQuestion = db.QUESTION.Where(l=>l.TESTID == _IdTest && l.QUESTIONTEXT == row.QUESTIONTEXT).First().QUESTIONID;
                }
                catch (Exception er)
                {
                    this.ShowMessageAsync("Error", "A data-entry error in the database of QUESTION " + er.Message + "Inner:" + er.InnerException);
                    return;
                }
            }

            foreach (TextBox tbAnswer in tbs)
            {
                var t = db.ANSWER.Where(l => l.ANSWERTEXT == tbAnswer.Text).FirstOrDefault();
                List<QA> repeats = new List<QA>();
                if (t != null)
                {
                    repeats = db.QA.Where(l => l.QUESTIONID == _IdQuestion && l.ANSWERID == t.ANSWERID).ToList();
                }
                //this.ShowMessageAsync("", chbs[i].IsChecked.ToString() + tb.Text);
                if (tbAnswer.Text == "")
                {
                    this.ShowMessageAsync("Error", "Enter answer");
                    return;
                }

                if (repeats.Count() == 0)//нет повтора
                {
                    try
                    {
                        ANSWER rowA = new ANSWER
                        {
                            ANSWERTEXT = tbAnswer.Text,
                            ANSWERID = db.ANSWER.Count() == 0 ? 1 : db.ANSWER.Max(l => l.ANSWERID) + 1
                        };
                        db.ANSWER.Add(rowA);
                        db.SaveChanges();

                        QA qa = new QA()
                        {
                            CORRECTANSWER = chbs[i].IsChecked,
                            QUESTIONID = _IdQuestion,
                            ANSWERID = db.ANSWER.Where(l => l.ANSWERTEXT == tbAnswer.Text).First().ANSWERID,
                            QA_ID = db.QA.Count() == 0 ? 1 : db.QA.Max(l => l.QA_ID) + 1
                        };
                        db.QA.Add(qa);
                        db.SaveChanges();

                        //CompletingQUESTION();
                    }
                    catch (Exception er)
                    {
                        this.ShowMessageAsync("Error", "Error " + er.Message + "Inner:" + er.InnerException);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        var a = db.ANSWER.Where(l => l.ANSWERID == t.ANSWERID).First();
                        a.ANSWERTEXT = tbAnswer.Text;
                        db.SaveChanges();

                        var qa = db.QA.Where(l => l.QUESTIONID == _IdQuestion && l.ANSWERID == t.ANSWERID).First();
                        qa.CORRECTANSWER = chbs[i].IsChecked;
                        db.SaveChanges();
                    }
                    catch (Exception er)
                    {
                        this.ShowMessageAsync("Error", er.Message + "Inner:" + er.InnerException);
                        return;
                    }


                }
                i++;
            }
            this.ShowMessageAsync("Success", "Successful saved");
            SetLbQuestions();
        }

        private void GridSplitter_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            if (tbs != null)
            {
                foreach (TextBox tb in tbs)
                {
                    tb.Width = stP.ActualWidth - 200;
                }
                tbQuestion.Width = stP.ActualWidth - 200;
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LbQuestions.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowQuestionAndAnswers(new List<Object> { new QUESTION { TESTID = _IdTest, QUESTIONID = 0, QUESTIONTEXT = "", QUESTIONNOTE = "",  QA = new List<QA>() } });
        }
        public class ViewModel : INotifyPropertyChanged
        {
            private string _appTitle = "MyApp";

            public string AppTitle
            {
                get { return _appTitle; }
                set
                {
                    if (_appTitle == value) return;

                    _appTitle = value;
                    this.OnPropertyChanged("AppTitle");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
