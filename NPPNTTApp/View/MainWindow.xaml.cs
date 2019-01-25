using NPPNTTApp.ModelView;
using System.Windows;

namespace NPPNTTApp.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel();
            DataContext = viewModel;
            viewModel.ClosingRequest += (s, e) => Close();
        }
    }
}
