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
    public partial class AddUser : MetroWindow
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//доб. раздел
        {
            Model1 db = new Model1();

            var USER = db.USER;

            if (tbEmail.Text != "")
            {
                foreach (var t in USER)
                {
                    if (t.EMAIL.Equals(tbEmail.Text))
                    {
                        this.ShowMessageAsync("Error", "This user already exists");
                        return;
                    }
                }
                int idUser;
                if (db.USER.Count() != 0)
                    idUser = db.USER.Max(l => l.USERID) + 1;
                else
                    idUser = 1;
                var row = new USER
                {
                    USERID = idUser,
                    EMAIL = tbEmail.Text,
                    LNAME = tbLn.Text,
                    FNAME = tbFn.Text,
                    PASSWORD = tbPass.Text
                };
                try
                {
                    db.USER.Add(row);
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
