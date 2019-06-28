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
    /// Логика взаимодействия для AddTest.xaml
    /// </summary>
    public partial class AddTest : MetroWindow
    {
        public AddTest()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//доб. раздел
        {
            Model1 db = new Model1();

            var TEST = db.TEST;

            if (tbText.Text != "")
            {
                foreach (var t in TEST)
                {
                    if (t.TESTTITLE.Equals(tbText.Text))
                    {
                        this.ShowMessageAsync("Error", "This test already exists");
                        return;
                    }
                }
                int idTest;
                if (db.TEST.Count() != 0)
                    idTest = db.TEST.Max(l => l.TESTID) + 1;
                else
                    idTest = 1;
                var row = new TEST
                {
                    TESTTITLE = tbText.Text,
                    TESTNOTE = tbNote.Text,
                    TESTCATERY = tbCatery.Text,
                    TESTID = idTest
                };
                try
                {
                    db.TEST.Add(row);
                    db.SaveChanges();
                    this.ShowMessageAsync("Success", "Successful saved");
                    this.Close();
                }
                catch (Exception er)
                {
                    this.ShowMessageAsync("Error", er.Message + "Inner:" + er.InnerException);
                    return;
                }
            }
            else
            {
                this.ShowMessageAsync("Error", "Enter test text");
            }

        }
    }
}
