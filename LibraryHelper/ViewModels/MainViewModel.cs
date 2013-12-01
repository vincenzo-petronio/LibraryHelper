namespace LibraryHelper.ViewModels
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ViewModel for MainView
    /// </summary>
    public class MainViewModel : PropertyChangedBase
    {
        private String title;

        public MainViewModel()
        {
            // INIT
            title = "Library Helper";
        }

        public String Title
        {
            get
            {
                return title;
            }

            set
            {
                if (title != value)
                {
                    title = value;
                    NotifyOfPropertyChange(() => Title);
                }
            }
        }

        public void LoadAction()
        {
            App.logger.Debug("LoadAction called...");
        }
    }
}
