using MeteoraBot.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace MeteoraBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainGrid.DataContext = MainViewModel.Instance;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            try
            {
                Process.GetCurrentProcess().Kill();
            }
            catch { }
        }
    }
}