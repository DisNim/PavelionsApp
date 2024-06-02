using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PavelionsApp.Classes;

namespace PavelionsApp.Pages
{
    public partial class ShoppingCenterPage : Page
    {
        public ShoppingCenterPage()
        {
            InitializeComponent();
        }
        private void loadPage(object sender, RoutedEventArgs e)
        {
            DataBaseConnection connection = new DataBaseConnection();
            connection.Connect();
            SqlDataReader reader = connection.GetScList($"'{searchBox.Text}'", GetSortType());
            loadData(reader);
            connection.Disconnect();
        }
        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (optionCombobbox.Visibility == Visibility.Visible)
            {
                optionCombobbox.Visibility = Visibility.Hidden;
            }
            else { optionCombobbox.Visibility = Visibility.Visible; }
        }
        private void Border_Click(object sender, RoutedEventArgs e)
        {
            Border border = (Border)sender;
            ShoppingCenterData.ShoppingCenterId =border.Name.Split('_').Last();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadPage(new CurrentShoppingCenterPage());
            mainWindow.windowTitle.Text = "Список павильонов";
        }
        private void loadData(SqlDataReader reader)
        {
            ShoppingCenterData.clearVal();
            ResourceDictionary resource = (ResourceDictionary)Application.Current.Resources;
            dataGrid.Children.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Border border = new Border();
                    border.CornerRadius = new CornerRadius(5);
                    border.BorderThickness = new Thickness(2);
                    border.Height = 300;
                    border.Width = 220;
                    border.Style = (Style)resource["ContentBorder"];
                    border.MouseLeftButtonDown += Border_Click;
                    border.BorderBrush = new SolidColorBrush(Color.FromRgb(69, 69, 80));
                    border.Margin = new Thickness(20, 30, 0, 0);
                    border.Name = $"Border_{reader.GetInt32(0)}";

                    StackPanel borderStack = new StackPanel();
                    borderStack.Orientation = Orientation.Vertical;

                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri($"/Assets/ShoppingCenterIcon/{reader.GetString(1)}.jpg", UriKind.Relative);
                    bi3.EndInit();
                    Image image = new Image();
                    image.Source = bi3;
                    image.Stretch = Stretch.Fill;
                    image.Height = 150;
                    image.Width = 200;
                    image.Margin = new Thickness(0, 5, 0, 0);
                    image.Name = $"Image";

                    TextBlock shoppingCenterName = new TextBlock();
                    shoppingCenterName.Foreground = Brushes.White;
                    shoppingCenterName.Margin = new Thickness(10, 5, 0, 0);
                    shoppingCenterName.FontSize = 17;
                    shoppingCenterName.Text = reader.GetString(1);
                    shoppingCenterName.Name = $"Name";

                    TextBlock shoppingCenterStatus = new TextBlock();
                    shoppingCenterStatus.Foreground = Brushes.White;
                    shoppingCenterStatus.FontSize = 10;
                    shoppingCenterStatus.Margin = new Thickness(20, 5, 0, 0);
                    shoppingCenterStatus.Text = $"Статус: {reader.GetString(2)}";
                    shoppingCenterStatus.Name = $"Status";

                    TextBlock shoppingCenterCountPav = new TextBlock();
                    shoppingCenterCountPav.Foreground = Brushes.White;
                    shoppingCenterCountPav.FontSize = 10;
                    shoppingCenterCountPav.Margin = new Thickness(20, 5, 0, 0);
                    shoppingCenterCountPav.Text = $"Кол. павильонов: {reader.GetInt32(3)}";
                    shoppingCenterCountPav.Name = $"Pav";

                    TextBlock shoppingCenterCity = new TextBlock();
                    shoppingCenterCity.Foreground = Brushes.White;
                    shoppingCenterCity.FontSize = 10;
                    shoppingCenterCity.Margin = new Thickness(20, 5, 0, 0);
                    shoppingCenterCity.Text = $"Город: {reader.GetString(4)}";
                    shoppingCenterCity.Name = $"City";

                    TextBlock shoppingCenterCast = new TextBlock();
                    shoppingCenterCast.Foreground = Brushes.White;
                    shoppingCenterCast.FontSize = 10;
                    shoppingCenterCast.Margin = new Thickness(20, 5, 0, 0);
                    shoppingCenterCast.Text = $"Стоимость посторойки: {reader.GetDecimal(5)}";
                    shoppingCenterCast.Name = $"Cast";

                    TextBlock shoppingCenterFloors = new TextBlock();
                    shoppingCenterFloors.Foreground = Brushes.White;
                    shoppingCenterFloors.FontSize = 10;
                    shoppingCenterFloors.Margin = new Thickness(20, 5, 0, 0);
                    shoppingCenterFloors.Text = $"Кол. этажей: {reader.GetInt32(6)}";
                    shoppingCenterFloors.Name = $"Floors";

                    TextBlock shoppingCenterAddedCast = new TextBlock();
                    shoppingCenterAddedCast.Foreground = Brushes.White;
                    shoppingCenterAddedCast.FontSize = 10;
                    shoppingCenterAddedCast.Margin = new Thickness(20, 5, 0, 0);
                    shoppingCenterAddedCast.Text = $"Коэфф. доб стоимости: {reader.GetDouble(7)}";
                    shoppingCenterAddedCast.Name = $"Added";

                    borderStack.Children.Add(image);
                    borderStack.Children.Add(shoppingCenterName);
                    borderStack.Children.Add(shoppingCenterStatus);
                    borderStack.Children.Add(shoppingCenterCountPav);
                    borderStack.Children.Add(shoppingCenterCity);
                    borderStack.Children.Add(shoppingCenterCast);
                    borderStack.Children.Add(shoppingCenterFloors);
                    borderStack.Children.Add(shoppingCenterAddedCast);

                    border.Child = borderStack;
                    dataGrid.Children.Add(border);
 
                }
                reader.Close();
            }
            Border addBorder = new Border();
            addBorder.Name = "AddedBorder";
            addBorder.Style = (Style)resource["ContentBorder"];
            addBorder.CornerRadius = new CornerRadius(5);
            addBorder.BorderThickness = new Thickness(2);
            addBorder.Height = 300;
            addBorder.Width = 220;
            addBorder.MouseLeftButtonDown += AddSc_Click;
            addBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(69, 69, 80));
            addBorder.Margin = new Thickness(20, 20, 0, 0);

            BitmapImage addImage = new BitmapImage();
            addImage.BeginInit();
            addImage.UriSource = new Uri("/Assets/MenuIcon/addIcon.png", UriKind.Relative);
            addImage.EndInit();
            Image adImage = new Image();
            adImage.Source = addImage;
            adImage.Height = 100;
            adImage.Width = 100;
            adImage.Stretch = Stretch.Uniform;

            addBorder.Child = adImage;
            dataGrid.Children.Add(addBorder);
        }
        private int GetSortType()
        {
            if (sortBySc.IsChecked == true)
            {
                sortByCity.IsChecked = false;
                return 2;
            }
            else if (sortByCity.IsChecked == true)
            {
                sortBySc.IsChecked = false;
                return 1;
            }
            else
            {
                return 0;
            }
        }


        private void AddSc_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            ShoppingCenterData.ShoppingCenterId = "0";
            mainWindow.windowTitle.Text = "Добавление ТЦ";
            mainWindow.LoadPage(new AddedEditScPage("Добавить", "2"));
        }  
        
        private void searchTextChanged(object sender, TextChangedEventArgs args)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();       
            SqlDataReader reader = dataBaseConnection.GetScList($"'{searchBox.Text}'", GetSortType());
            loadData(reader);
            dataBaseConnection.Disconnect();
        }
    }
}

