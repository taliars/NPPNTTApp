using Microsoft.WindowsAPICodePack.Dialogs;
using NPPNTTApp.ModelView;
using System.Windows;

namespace NPPNTTApp.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
            viewModel.ClosingRequest += (s, e) => Close();
        }

        private void PopulateBtn_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("csv файлы", "*.csv"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                viewModel.Path = dialog.FileName;
            }
        }
    }
}
