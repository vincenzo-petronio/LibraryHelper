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
        private string title = string.Empty;
        private string textBefore = string.Empty;
        private string textAfter = string.Empty;
        private string textTitle = string.Empty;
        private string textAuthor = string.Empty;
        private string textYear = string.Empty;
        private string textIsbn = string.Empty;
        private bool renameIsEnabled = false;

        public MainViewModel()
        {
            // INIT
            title = "Library Helper";
        }

        /// <summary>
        /// String property for Windows Title
        /// </summary>
        public string Title
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

        /// <summary>
        /// Property for TextBlock with initial filename
        /// </summary>
        public string TextBefore
        {
            get
            {
                return textBefore;
            }

            set
            {
                if (textBefore != value)
                {
                    textBefore = value;
                    NotifyOfPropertyChange(() => TextBefore);
                }
            }
        }

        /// <summary>
        /// Property for TextBlock with final filename (after rename).
        /// </summary>
        public string TextAfter
        {
            get
            {
                return textAfter;
            }

            set
            {
                if (textAfter != value)
                {
                    textAfter = value;
                    NotifyOfPropertyChange(() => TextAfter);
                }
            }
        }

        public string TextTitle
        {
            get
            {
                return textTitle;
            }

            set
            {
                if (textTitle != value)
                {
                    textTitle = value;
                    NotifyOfPropertyChange(() => TextTitle);
                }
            }
        }

        public string TextAuthor
        {
            get 
            { 
                return textAuthor; 
            }

            set 
            {
                if (textAuthor != value)
                {
                    textAuthor = value;
                    NotifyOfPropertyChange(() => TextAuthor);
                }
            }
        }

        public string TextYear
        {
            get
            {
                return textYear;
            }

            set
            {
                if (textYear != value)
                {
                    textYear = value;
                    NotifyOfPropertyChange(() => TextYear);
                }
            }
        }

        public string TextIsbn
        {
            get
            {
                return textIsbn;
            }

            set
            {
                if (textIsbn != value)
                {
                    textIsbn = value;
                    NotifyOfPropertyChange(() => TextIsbn);
                }
            }
        }

        public bool RenameIsEnabled
        {
            get 
            {
                return renameIsEnabled;
            }

            set
            {
                if (renameIsEnabled != value)
                {
                    renameIsEnabled = value;
                    NotifyOfPropertyChange(() => RenameIsEnabled);
                }
            }
        }

        /// <summary>
        /// Action for Click Event on Load button.
        /// </summary>
        public void LoadAction()
        {
            App.logger.Debug("LoadAction called...");
        }

        /// <summary>
        /// Action for Click Event on Rename button.
        /// </summary>
        public void RenameAction()
        {
            App.logger.Debug("RenameAction called...");
        }

        /// <summary>
        /// Action for Changed item Event on Combobox.
        /// </summary>
        public void ComboChangedAction()
        {
            App.logger.Debug("ComboChangedAction called...");
        }
    }
}
