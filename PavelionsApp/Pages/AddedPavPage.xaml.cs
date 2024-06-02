using PavelionsApp.Classes;
using System.Windows;
using System.Windows.Controls;

namespace PavelionsApp.Pages
{
    public partial class AddedPavPage : Page
    {
        public AddedPavPage()
        {
            InitializeComponent();
        }
        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            string pvMoney = "";
            string pvAddedVal = "";
            if (pvCost.Text.Contains(','))
                pvMoney = pvCost.Text.Replace(',', '.');
            else pvMoney = pvCost.Text;
            if (pvAdded.Text.Contains(","))
                pvAddedVal = pvAdded.Text.Replace(',', '.');
            else pvAddedVal = pvAdded.Text;
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            try
            {
                dataBaseConnection.AddPav(pvId.Text, ShoppingCenterData.ShoppingCenterId, pvSquare.Text, pvMoney, pvFloor.Text, pvAddedVal);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.LoadPage(new CurrentShoppingCenterPage());
            }
            catch (Exception ex)
            {
                errorTxt.Text = "Ошибка ввода. Проверьте введенные данные.";
            }
                dataBaseConnection.Disconnect();
            }
    }
}
