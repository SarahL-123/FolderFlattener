using System.Configuration;
using System.Data;
using System.Windows;

namespace FolderFlattenerWpfUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Dependency injection
            var flatteningStratFactory = new FolderFlattener.Implementations.FlatteningStrategyFactory();

            // start first window
            var window = new MainWindow();
            var windowVM = new MainWindowVM(flatteningStratFactory);
            window.MainWindowVM = windowVM;

            window.Show();


        }
    }

}
