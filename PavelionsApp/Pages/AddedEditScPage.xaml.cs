using System.Windows;
using System.Windows.Controls;
using PavelionsApp.Classes;

namespace PavelionsApp.Pages
{
    public partial class AddedEditScPage : Page
    {
        private string _mode;
        public AddedEditScPage(string btnText, string mode)
        {
            InitializeComponent();
            addBtn.Content = btnText;
            _mode = mode;

        }
        private void loadPage(object sender, RoutedEventArgs e)
        {
            if (_mode == "1")
            {
                scFloors.IsEnabled = false;
                scFloors.Visibility = Visibility.Hidden;
            }
            scName.Text = ShoppingCenterData.ShoppingCenterName;
            scAdded.Text = ShoppingCenterData.ShoppingCenterAddedCost;
            scCity.Text = ShoppingCenterData.ShoppingCenterCity;
            scStatus.Text = ShoppingCenterData.ShoppingCenterStatus;
            scCost.Text = ShoppingCenterData.ShoppingCenterCast;
            scFloors.Text = ShoppingCenterData.ShoppingCenterFloors;
        }
        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            string scAddedVal = "", scMoney = "";
            if (scAdded.Text.Contains(','))
                scAddedVal = scAdded.Text.Replace(',', '.');
            else
                scAddedVal = scAdded.Text;
            if (scCost.Text.Contains(','))
                scMoney = scCost.Text.Replace(",", ".");
            else
                scMoney = scCost.Text;
            try
            {
                dataBaseConnection.AddEditSc(ShoppingCenterData.ShoppingCenterId, scName.Text, scStatus.Text, scCity.Text,
                    scFloors.Text, _mode,
                    scAddedVal, scMoney);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.LoadPage(new ShoppingCenterPage());
            }
            catch (Exception ex) {
              
                errorTxt.Text = "Ошибка ввода. Проверьте введенные данные.";
            }
            dataBaseConnection.Disconnect();
        }
    }
}
