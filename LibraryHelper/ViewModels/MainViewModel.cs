namespace LibraryHelper.ViewModels
{
    using Caliburn.Micro;
    using Microsoft.Win32;
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
        private OpenFileDialog openFileDialog;

        public MainViewModel()
        {
            // INIT
            title = "Library Helper";
        }

        /// <summary>
        /// Initialize OpenFileDialog.
        /// </summary>
        private void InitFileDialog()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".pdf";
            openFileDialog.Filter = "PDF|*.pdf|Archive|*.zip;*.rar;*.7z";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        /// <summary>
        /// Restore initial value of Properties.
        /// </summary>
        private void CleanGui()
        {
            this.TextBefore = string.Empty;
            this.TextAfter = string.Empty;
            this.TextAuthor = string.Empty;
            this.TextIsbn = string.Empty;
            this.TextTitle = string.Empty;
            this.TextYear = string.Empty;
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

        /// <summary>
        /// Property for TextBlock with Title
        /// </summary>
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

        /// <summary>
        /// Property for TexoBlock with Author name.
        /// </summary>
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

        /// <summary>
        /// Property for TextBlock with Year of publication.
        /// </summary>
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

        /// <summary>
        /// Property for TextBlock with ISBN code.
        /// </summary>
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

        /// <summary>
        /// Property for enable/disable Rename button.
        /// </summary>
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

            this.InitFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result != null && result == true)
            {
                App.logger.Debug("File selected: \t" + openFileDialog.FileName);
                this.TextBefore = openFileDialog.SafeFileName;
                this.TextTitle = openFileDialog.SafeFileName;
            }
            else
            {
                this.CleanGui();
            }
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
