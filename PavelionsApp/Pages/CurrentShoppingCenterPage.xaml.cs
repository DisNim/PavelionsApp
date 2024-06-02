using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PavelionsApp.Classes;

namespace PavelionsApp.Pages
{
    public partial class CurrentShoppingCenterPage : Page
    {
        public CurrentShoppingCenterPage()
        {
            InitializeComponent();
        }
        private void loadPage(object sender, RoutedEventArgs e)
        {
            loadPv();
        }
        private void loadPv()
        {
            stackPavelions.Children.Clear();
            ResourceDictionary resource = (ResourceDictionary)Application.Current.Resources;
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            SqlDataReader readPavelions = dataBaseConnection.GetScPavelions(ShoppingCenterData.ShoppingCenterId);
            int i = 0;
            if (readPavelions.HasRows)
            {
                while (readPavelions.Read())
                {
                    Border border = new Border();
                    border.CornerRadius = new CornerRadius(5);
                    border.BorderThickness = new Thickness(2);
                    border.Height = 300;
                    border.Width = 220;
                    border.Style = (Style)resource["ContentBorder"];
                    border.BorderBrush = new SolidColorBrush(Color.FromRgb(69, 69, 80));
                    border.Margin = new Thickness(20, 20, 0, 0);
                    border.Name = $"Border_{readPavelions.GetString(0)}";

                    StackPanel borderStack = new StackPanel();
                    borderStack.Orientation = Orientation.Vertical;

                    TextBlock idPavelion = new TextBlock();
                    idPavelion.Foreground = Brushes.White;
                    idPavelion.HorizontalAlignment = HorizontalAlignment.Center;
                    idPavelion.FontSize = 17;
                    idPavelion.Text = readPavelions.GetString(0);
                    idPavelion.Name = $"Name";

                    TextBlock pavelionsFloors = new TextBlock();
                    pavelionsFloors.Foreground = Brushes.White;
                    pavelionsFloors.FontSize = 10;
                    pavelionsFloors.Margin = new Thickness(20, 5, 0, 0);
                    pavelionsFloors.Text = $"Этаж: {readPavelions.GetInt32(1)}";
                    pavelionsFloors.Name = $"Floors";

                    TextBlock pavelionsStatus = new TextBlock();
                    pavelionsStatus.Foreground = Brushes.White;
                    pavelionsStatus.FontSize = 10;
                    pavelionsStatus.Margin = new Thickness(20, 5, 0, 0);
                    pavelionsStatus.Text = $"Статус: {readPavelions.GetString(2)}";
                    pavelionsStatus.Name = $"Status";

                    TextBlock pavelionsSquare = new TextBlock();
                    pavelionsSquare.Foreground = Brushes.White;
                    pavelionsSquare.FontSize = 10;
                    pavelionsSquare.Margin = new Thickness(20, 5, 0, 0);
                    pavelionsSquare.Text = $"Площадь: {readPavelions.GetInt32(3)}";
                    pavelionsSquare.Name = $"Square";

                    TextBlock pavelionsPerSquare = new TextBlock();
                    pavelionsPerSquare.Foreground = Brushes.White;
                    pavelionsPerSquare.FontSize = 10;
                    pavelionsPerSquare.Margin = new Thickness(20, 5, 0, 0);
                    pavelionsPerSquare.Text = $"Стоимость за кв/м: {readPavelions.GetDecimal(4)}";
                    pavelionsPerSquare.Name = $"Cast";

                    TextBlock pavelionsAdded = new TextBlock();
                    pavelionsAdded.Foreground = Brushes.White;
                    pavelionsAdded.FontSize = 10;
                    pavelionsAdded.Margin = new Thickness(20, 5, 0, 0);
                    pavelionsAdded.Text = $"Коэфф. доб стоимости: {readPavelions.GetDouble(5)}";
                    pavelionsAdded.Name = $"Floors";

                    Button btnDelete = new Button();
                    btnDelete.Content = "Удалить";
                    btnDelete.Style = (Style)resource["DeleteButton"];
                    btnDelete.Name = $"DeleteBtn_{readPavelions.GetString(0)}";
                    btnDelete.Click += btnDeletePav_Click;


                    Button btnRent = new Button();
                    btnRent.Content = "Арендовать";
                    btnRent.Style = (Style)resource["ControlButton"];
                    btnRent.Margin = new Thickness(0, 10, 0, 0);
                    btnRent.Name = $"RentBtn_{readPavelions.GetString(0)}";
                    btnRent.Click += btnNewRent_Click;
                    if (readPavelions.GetString(2) != "Свободен")
                        btnRent.IsEnabled = false;

                    borderStack.Children.Add(idPavelion);
                    borderStack.Children.Add(pavelionsFloors);
                    borderStack.Children.Add(pavelionsStatus);
                    borderStack.Children.Add(pavelionsSquare);
                    borderStack.Children.Add(pavelionsPerSquare);
                    borderStack.Children.Add(pavelionsAdded);
                    borderStack.Children.Add(btnDelete);
                    borderStack.Children.Add(btnRent);

                    border.Child = borderStack;
                    stackPavelions.Children.Add(border);
                    i++;
                }
                readPavelions.Close();
            }
            Border addBorder = new Border();
            addBorder.Name = "AddedBorder";
            addBorder.Style = (Style)resource["ContentBorder"];
            addBorder.CornerRadius = new CornerRadius(5);
            addBorder.BorderThickness = new Thickness(2);
            addBorder.Height = 300;
            addBorder.Width = 220;
            addBorder.MouseLeftButtonDown += addBorder_Click;
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
            stackPavelions.Children.Add(addBorder);
            dataBaseConnection.Disconnect();
        }

        private void btnNewRent_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            PavelionsData.PavelionsID = button.Name.Split('_').Last();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.windowTitle.Text = "Создание аренды";
            mainWindow.LoadPage(new NewRentPage());
        }
        private void btnDeletePav_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.Connect();
            dataBaseConnection.DeletePav(ShoppingCenterData.ShoppingCenterId, button.Name.Split('_').Last());
            dataBaseConnection.Disconnect();
            loadPv();
        }
        private void addBorder_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.windowTitle.Text = "Добавление павильона";
            mainWindow.LoadPage(new AddedPavPage());
        }
    }
}
