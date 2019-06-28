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
    /// Логика взаимодействия для ShowTests.xaml
    /// </summary>
    public partial class ShowTests : MetroWindow
    {
        List<TEST> TESTCollection;

        Model1 db = new Model1();

        public ShowTests()
        {
            InitializeComponent();
            Load();
        }
        
        public void Load()
        {
            TESTCollection = new List<TEST>();
            var objs = db.TEST.ToList();
            foreach (var i in objs)
            {
                TESTCollection.Add(
                    new TEST
                    {
                        TESTTITLE = i.TESTTITLE,
                        TESTNOTE = i.TESTNOTE,
                        TESTCATERY = i.TESTCATERY,
                        TESTID = i.TESTID,
                    });
            }
            grid1.ItemsSource = TESTCollection;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            object id = ((System.Windows.Controls.Button)sender).CommandParameter;
            var form = new ShowQUESTION((int)id);
            form.ShowDialog();
            //    form.ShowsNavigationUI();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var form = new AddTest();
            form.ShowDialog();
            Load();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).CommandParameter);
            var pa = db.TEST.Where(d => d.TESTID == id).FirstOrDefault();
            try
            {
                db.TEST.Remove(pa);
                db.SaveChanges();

                this.ShowMessageAsync("Success", "Successful saved");
                Load();
            }
            catch (Exception er)
            {
                this.ShowMessageAsync("Error", er.Message + "Inner:" + er.InnerException);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowUsers showUsers = new ShowUsers();
            showUsers.ShowDialog();
            Load();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ShowResults showResults = new ShowResults();
            showResults.ShowDialog();
            Load();

        }
    }
}
