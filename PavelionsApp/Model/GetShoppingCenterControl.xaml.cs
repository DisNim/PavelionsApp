using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PavelionsApp.Classes;
using PavelionsApp.Pages;

namespace PavelionsApp.Model
{
    public partial class GetShoppingCenterControl : UserControl
    {
        public GetShoppingCenterControl()
        {
            InitializeComponent();
        }
        private void loadComponent(object sender, RoutedEventArgs e)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            SqlDataReader reader = dataBaseConnection.GetScInfo(ShoppingCenterData.ShoppingCenterId);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri($"/Assets/ShoppingCenterIcon/{reader.GetString(0)}.jpg", UriKind.Relative);
                    bi3.EndInit();
                    image.Source = bi3;
                    image.Stretch = Stretch.Uniform;

                    ScName.Text = $"Название тц: {reader.GetString(0)}";
                    ScCity.Text = $"Город: {reader.GetString(1)}";
                    ScCountPav.Text = $"Кол. павильонов: {reader.GetInt32(2)}";
                    ScStatus.Text = $"Статус: {reader.GetString(3)}";
                    ScCast.Text = $"Стоимость тц: {reader.GetDecimal(4)}";
                    ScFloors.Text = $"Кол. этажей: {reader.GetInt32(5)}";
                    ScAdded.Text = $"Коэф. доп стоимости: {reader.GetDouble(6)}";
                }
            }
            reader.Close();
            dataBaseConnection.Disconnect();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            dataBaseConnection.DeleteSc(ShoppingCenterData.ShoppingCenterId);
            dataBaseConnection.Disconnect();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Navigate(new ShoppingCenterPage());
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ShoppingCenterData.ShoppingCenterAddedCost = ScAdded.Text.Split(':',StringSplitOptions.TrimEntries).Last();
            ShoppingCenterData.ShoppingCenterStatus = ScStatus.Text.Split(':', StringSplitOptions.TrimEntries).Last();
            ShoppingCenterData.ShoppingCenterCast = ScCast.Text.Split(':', StringSplitOptions.TrimEntries).Last();
            ShoppingCenterData.ShoppingCenterName = ScName.Text.Split(':', StringSplitOptions.TrimEntries).Last();
            ShoppingCenterData.ShoppingCenterFloors = ScFloors.Text.Split(':', StringSplitOptions.TrimEntries).Last();
            ShoppingCenterData.ShoppingCenterCity = ScCity.Text.Split(':', StringSplitOptions.TrimEntries).Last();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.windowTitle.Text = "Изменение ТЦ";
            mainWindow.MainFrame.Navigate(new AddedEditScPage("Изменить", "1"));
        }
    }
}
