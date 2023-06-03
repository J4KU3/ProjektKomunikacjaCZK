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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektKomunikacja
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region DataBaseConnection
        private List<Data.Employees> GetLogin()
        {
            using (var employees = new Data.ZarzadzanieFirmaDBEntities())
            {
                return employees.Employees.ToList();
            }
        }


        #endregion


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var Log = GetLogin().Find(x => x.Mail == Login.Text);
                string searchLogin="";
                string searchPass="";
                if (Log != null)
                {
                    searchLogin = Log.Mail;
                    searchPass = Log.Passsword;
                }
                
               
                    
                    if (Login.Text == searchLogin && Pasword.Text == searchPass && Login.Text != string.Empty && Pasword.Text != string.Empty)
                    {
                       
                        MessageBox.Show("NAreszcie");
                        ProgramMain.SelectedIndex = 1;


                    }
                    else
                    {
                        MessageBox.Show("niepoprawne dane lub puste pole");
                    }
               
                
                //  MessageBox.Show($"{Log.Password}, {Log.Mail}");




            }
            catch (Exception)
            {
                MessageBox.Show("złe dane");
              //  throw;
            }
        }
    }
}
