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
        private DateTime date;
        
        public MainWindow()
        {
            InitializeComponent();
            DataLoaded.ItemsSource = GetClients();
            ChangeTabCommand = new RelayCommand<string>(ChangeTab);
            date = DateTime.UtcNow;
            OrderD.Text = $"{date.Day}/{date.Month}/{date.Year}";
            DataContext = this;

        }
        #region DataBaseConnection
        //pracownicy
        private List<Data.Employees> GetLogin()
        {
            using (var employees = new Data.ZarzadzanieFirmaDBEntities())
            {
                return employees.Employees.ToList();
            }
        }
        //klienci
        private List<Data.Clients> GetClients()
        {
            using (var clients = new Data.ZarzadzanieFirmaDBEntities())
            {
                return clients.Clients.ToList();
            }
        }
        //zamówienia
        private List<Data.Orders> GetOrders()
        {
            using (var orders = new Data.ZarzadzanieFirmaDBEntities())
            {
                return orders.Orders.ToList();
            }
        }
        //reklamacje
        private List<Data.Complaints> GetComplaints()
        {
            using (var complaints = new Data.ZarzadzanieFirmaDBEntities())
            {
                return complaints.Complaints.ToList();
            }
        }
        #endregion
        #region CRUD
        public void AddNewClients(Data.Clients newClients)
        {
            using (var Clients = new Data.ZarzadzanieFirmaDBEntities())
            {

                Clients.Clients.Add(newClients);
                Clients.SaveChanges();
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

        private void ZappiszDaneClienta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FN.Text != null && LN.Text != null && PH.Text != null && HAN != null && CID != null && ZC.Text != null && CT.Text != null && ST.Text!=null )
                {
                    Data.Clients clients = new Data.Clients();
                    clients.CFName = FN.Text;
                    clients.CLName = LN.Text;
                    clients.Phone = PH.Text;
                    clients.ApartmentNumber = HAN.Text;
                    clients.ClientID = int.Parse(CID.Text);
                    clients.Zipcode = ZC.Text;
                    clients.City = CT.Text;
                    clients.Street = ST.Text;
                    AddNewClients(clients);
                    MessageBox.Show("klient dodany do bazy");
                    FN.Text = string.Empty;
                    LN.Text = string.Empty;
                    PH.Text = string.Empty;
                    HAN.Text = string.Empty;
                    CID.Text = string.Empty;
                    ZC.Text = string.Empty;
                    CT.Text = string.Empty;
                    ST.Text = string.Empty;


                }
                else
                {
                    MessageBox.Show("ktores z pol jest puste");
                }



            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
