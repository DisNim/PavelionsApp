using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PavelionsApp.Classes;

namespace PavelionsApp.Pages
{
    public partial class AddEmployeesPage : Page
    {
        private StringBuilder _originalText = new StringBuilder();
        public AddEmployeesPage()
        {
            InitializeComponent();
        }
        private void TextIsEmpty(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(pas.Text))
                _originalText.Clear();
        }
        private void ChangePreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            _originalText.Append(e.Text);
            pas.Text += "*";
            pas.CaretIndex = pas.Text.Length;
        }
        private void btnGender_Click(object sender, RoutedEventArgs e)
        {

            string gender = "";
            if (men.IsChecked == true)
                gender = "М";
            if (wom.IsChecked == true)
                gender = "Ж";
            MessageBox.Show(_originalText.ToString());
            DataBaseConnection connection = new DataBaseConnection();
            try
            {
                connection.Connect();
                if (string.IsNullOrEmpty(employeesData.Text) || string.IsNullOrEmpty(login.Text) || string.IsNullOrEmpty(pas.Text) || string.IsNullOrEmpty(phone.Text) || string.IsNullOrEmpty(role.Text))
                {
                    errorTxt.Foreground = Brushes.Red;
                    errorTxt.Text = "Ошибка ввода. Проверьте введенные данные.";
                }
                else
                {
                    
                    connection.AddEmployees(employeesData.Text, login.Text, _originalText.ToString(), phone.Text, role.Text, gender);
                    errorTxt.Foreground = Brushes.Green;
                    errorTxt.Text = "Сотрудник добавлен.";
                }
            }
            catch (Exception ex)
            {
                errorTxt.Foreground = Brushes.Red;
                errorTxt.Text = "Ошибка ввода. Проверьте введенные данные.";
            }
            connection.Disconnect();
            
        }
    }
}
