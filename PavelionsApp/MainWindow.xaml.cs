using PavelionsApp.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PavelionsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void mouse_down(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        public void LoadPage(Page page)
        {
            if (MainFrame.CanGoBack)
            {
               MainFrame.NavigationService.RemoveBackEntry();
            }
            MainFrame.Navigate(page);
        }

        private void loadShoppingCenterList(object sender, RoutedEventArgs e)
        {
            windowTitle.Text = "Список ТЦ";
            LoadPage(new ShoppingCenterPage());
        }
        private void btnAddEmployees_Click(object sender, RoutedEventArgs e)
        {
            windowTitle.Text = "Добавление сотрудника";
            LoadPage(new AddEmployeesPage());
        }
        private void btnAddTenantry_Click(object sender, RoutedEventArgs e)
        {
            windowTitle.Text = "Добавление арендатора";
            LoadPage(new AddTenantryPage());
        }
    }
  
}