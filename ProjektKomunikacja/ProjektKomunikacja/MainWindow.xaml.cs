using GalaSoft.MvvmLight.Command;
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
            DataLoaded.ItemsSource = GetLogin();
            ChangeTabCommand = new RelayCommand<string>(ChangeTab);
            DataContext = this;
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


        //Login
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
                       
                        MessageBox.Show("Zalogowano");
                        ProgramMain.SelectedIndex = 1;
                       
                         Pasword.Text = string.Empty;

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

        #region allMenu Buttons
       

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Zmiana Strony 
        public ICommand ChangeTabCommand { get; }
        public void ChangeTab(string tabnum)
        {
            if (int.TryParse(tabnum, out int index))
            {
                ProgramMain.SelectedIndex = index;
            }
        }


        #endregion


    }
}
