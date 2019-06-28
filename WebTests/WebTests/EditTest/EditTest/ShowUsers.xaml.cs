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
    public partial class ShowUsers : MetroWindow
    {
        List<USER> USERCollection;

        Model1 db = new Model1();

        public ShowUsers()
        {
            InitializeComponent();
            Load();
        }
        
        public void Load()
        {
            USERCollection = new List<USER>();
            var objs = db.USER.ToList();
            foreach (var i in objs)
            {
                USERCollection.Add(
                    new USER
                    {
                        USERID = i.USERID,
                        EMAIL = i.EMAIL,
                        LNAME = i.LNAME,
                        FNAME = i.FNAME,
                        PASSWORD = i.PASSWORD
                    });
            }
            grid1.ItemsSource = USERCollection;
        }


        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    object id = ((System.Windows.Controls.Button)sender).CommandParameter;
        //    var form = new ShowQUESTION((int)id);
        //    form.ShowDialog();
        //}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var form = new AddUser();
            form.ShowDialog();
            Load();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).CommandParameter);
            var u = db.USER.Where(d => d.USERID == id).FirstOrDefault();
            try
            {
                db.USER.Remove(u);
                db.SaveChanges();

                this.ShowMessageAsync("Success", "Successful saved");
                Load();
            }
            catch (Exception er)
            {
                this.ShowMessageAsync("Error", er.Message + "Inner:" + er.InnerException);
            }
        }

    }
}
