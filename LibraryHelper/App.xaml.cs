using NLog;
using System.Windows;

namespace LibraryHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            logger.Info("App starting...");
            InitializeComponent();
        }
    }
}
