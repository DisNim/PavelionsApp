using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PavelionsApp.Classes;

namespace PavelionsApp.Pages
{
    public partial class AddTenantryPage : Page
    {
        public AddTenantryPage()
        {
            InitializeComponent();
        }
        private void btnAddTenantry_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            try
            {
                dataBaseConnection.AddTenantry(tntName.Text, tntPhone.Text, tntAddress.Text);
                if (string.IsNullOrEmpty(tntName.Text) || string.IsNullOrEmpty(tntAddress.Text) || string.IsNullOrEmpty(tntPhone.Text))
                {
                    errorTxt.Foreground = Brushes.Red;
                    errorTxt.Text = "Ошибка ввода. Проверьте введенные данные.";

                }
                else
                {
                    errorTxt.Foreground = Brushes.Green;
                    errorTxt.Text = "Арендатор добавлен.";
                }
            }
            catch (Exception ex)
            {
                errorTxt.Foreground = Brushes.Red;
                errorTxt.Text = "Ошибка ввода. Проверьте введенные данные.";
            }
            dataBaseConnection.Disconnect();
        }
    }
}
