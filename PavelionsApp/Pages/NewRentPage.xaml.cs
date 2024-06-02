using System.Windows;
using System.Windows.Controls;
using PavelionsApp.Classes;
namespace PavelionsApp.Pages
{
    public partial class NewRentPage : Page
    {
        public NewRentPage()
        {
            InitializeComponent();
        }
        private void btnNewRent_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            try
            {
                dataBaseConnection.AddRent(tenantryName.Text, employeesData.Text, PavelionsData.PavelionsID,
                    ShoppingCenterData.ShoppingCenterId, startDate.dtPicker.Text, endDate.dtPicker.Text);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.LoadPage(new CurrentShoppingCenterPage());
            }
            catch (Exception ex)
            {
                errorTxt.Text = "Возникла ошибка при заполнение. Проверьте заполненые поля.";
            }
            dataBaseConnection.Disconnect();
        }

        private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.LoadPage(new RentPage());
        }
    }
}
