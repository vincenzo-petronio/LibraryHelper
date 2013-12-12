namespace LibraryHelper.ViewModels
{
    using Caliburn.Micro;
    using LibraryHelper.Models;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// ViewModel for MainView
    /// </summary>
    public class MainViewModel : PropertyChangedBase
    {
        private ISearchService searchService;
        private string title = string.Empty;
        private string textBefore = string.Empty;
        private string textAfter = string.Empty;
        private string textTitle = string.Empty;
        private string textAuthor = string.Empty;
        private string textYear = string.Empty;
        private string textIsbn = string.Empty;
        private string selectedTextPublisher = string.Empty;
        private OpenFileDialog openFileDialog;
        private bool isRightGridEnabled = false;
        private bool isProgressEnabled = false;
        private bool isSearchButtonEnabled = true;
        private DispatcherTimer dTimer;

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="ss">Constructor Injection</param>
        public MainViewModel(ISearchService ss)
        {
            // INIT
            title = "Library Helper";
            dTimer = new DispatcherTimer();
            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 2); // 2 sec

            // Guard Clause
            if (ss == null)
            {
                throw new ArgumentNullException("SearchService");
            }

            this.searchService = ss;
        }

        /// <summary>
        /// Handler for DispatcherTimer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dTimer_Tick(object sender, EventArgs e)
        {
            // GoodReads Developer Terms of Service:
            // 1. Not request any method more than once a second.
            App.logger.Debug("TimerTick");
            dTimer.Stop();
            IsSearchButtonEnabled = true;
        }

        /// <summary>
        /// Initialize OpenFileDialog.
        /// </summary>
        private void InitFileDialog()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".pdf";
            openFileDialog.Filter = "PDF|*.pdf|Archive|*.zip;*.rar;*.7z";
            openFileDialog.RestoreDirectory = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        /// <summary>
        /// Restore initial value of Properties.
        /// </summary>
        private void CleanGui()
        {
            this.TextBefore = string.Empty;
            this.TextAuthor = string.Empty;
            this.TextIsbn = string.Empty;
            this.TextTitle = string.Empty;
            this.TextYear = string.Empty;
            this.TextAfter = string.Empty;
        }

        /// <summary>
        /// Bool property for visibility of left grid of window.
        /// </summary>
        public bool IsRightGridEnabled
        {
            get
            {
                return isRightGridEnabled;
            }

            set
            {
                if (isRightGridEnabled != value)
                {
                    isRightGridEnabled = value;
                    NotifyOfPropertyChange(() => IsRightGridEnabled);
                }
            }
        }

        /// <summary>
        /// Boolean property for ProgressBar 
        /// </summary>
        public bool IsProgressEnabled
        {
            get
            {
                return isProgressEnabled;
            }

            set
            {
                if (isProgressEnabled != value)
                {
                    isProgressEnabled = value;
                    NotifyOfPropertyChange(() => IsProgressEnabled);
                }
            }
        }

        /// <summary>
        /// Boolean property for Search Button.
        /// </summary>
        public bool IsSearchButtonEnabled
        {
            get
            {
                return isSearchButtonEnabled;
            }

            set
            {
                if (isSearchButtonEnabled != value)
                {
                    isSearchButtonEnabled = value;
                    NotifyOfPropertyChange(() => IsSearchButtonEnabled);
                }
            }
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
                    TextAfter = GetFinalFilename();
                }
            }
        }

        /// <summary>
        /// Property for TextBlock with Author name.
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
                    TextAfter = GetFinalFilename();
                }
            }
        }

        /// <summary>
        /// Property for ComboBox with Publisher name.
        /// </summary>
        public string SelectedTextPublisher
        {
            get
            {
                return selectedTextPublisher;
            }

            set 
            {
                if (selectedTextPublisher != value)
                {
                    selectedTextPublisher = value;
                    NotifyOfPropertyChange(() => SelectedTextPublisher);
                    TextAfter = GetFinalFilename();
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
                    TextAfter = GetFinalFilename();
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
                    TextAfter = GetFinalFilename();
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
                IsRightGridEnabled = true;
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                this.TextBefore = openFileDialog.FileName;
                this.TextTitle = fileNameNoExtension;
            }
            else
            {
                this.CleanGui();
                IsRightGridEnabled = false;
            }
        }

        /// <summary>
        /// Action for Click Event on Rename button.
        /// </summary>
        public void RenameAction()
        {
            App.logger.Debug("RenameAction called...");
            if (this.openFileDialog == null) return;

            if (!string.IsNullOrEmpty(this.TextTitle) && !string.IsNullOrEmpty(this.TextAuthor) &&
                !string.IsNullOrEmpty(this.SelectedTextPublisher) && 
                !string.IsNullOrEmpty(this.TextYear) && !string.IsNullOrEmpty(this.TextIsbn))
            {
                // Selected filename (full path)
                string selectedFilename = openFileDialog.FileName;
                App.logger.Debug("FileName: {0}", selectedFilename);

                // Selected filename merged to new Filename
                string mergedFilename = Path.Combine(Path.GetDirectoryName(openFileDialog.FileName), this.GetFinalFilename());
                App.logger.Debug("FileName merged: {0}", mergedFilename);

                // Replace
                try
                {
                    File.Move(selectedFilename, mergedFilename);
                    App.logger.Info("File was renamed successfully!");
                }
                catch (Exception e)
                {
                    App.logger.Error(e.Message);
                }
                finally
                {
                    CleanGui();
                }
            }
            else
            {
                // TODO 
                App.logger.Info("Not all text fields are filled!");
            }
        }

        /// <summary>
        /// Return string for filename based on filled text fields.
        /// </summary>
        /// <returns>string filename</returns>
        private string GetFinalFilename()
        {
            // Pattern new filename
            string newFilename =
                this.TextTitle
                + "._." +
                this.TextAuthor
                + "._." +
                this.SelectedTextPublisher
                + "._." +
                this.TextYear
                + "._." +
                this.TextIsbn
                + 
                Path.GetExtension(openFileDialog.FileName);
            return PurgeFilename(newFilename);
        }

        /// <summary>
        /// Purge filename from unwanted chars.
        /// </summary>
        /// <returns>string</returns>
        private string PurgeFilename(string filenameBefore)
        {
            string filenameAfter = string.Empty;
            filenameAfter = filenameBefore.Replace(" ", ".");
            return filenameAfter;
        }

        /// <summary>
        /// Action for Changed item Event on Combobox.
        /// </summary>
        public void TextPublisherSelectionChanged(object item)
        {
            App.logger.Debug("TextPublisherSelectionChanged called... {0}", (string)item);
            this.SelectedTextPublisher = item as string;
        }

        /// <summary>
        /// Action for Click Event on Search ISBN button.
        /// </summary>
        public async void SearchIsbn()
        {
            App.logger.Debug("SearchIsbn called...");
            if (!string.IsNullOrEmpty(TextIsbn))
            {
                IsProgressEnabled = true;
                IsSearchButtonEnabled = false;
                dTimer.Start();

                Book b = await searchService.SearchForIsbnAsync(this.TextIsbn);
                if (b != null)
                {
                    App.logger.Info("Book found: \t" + b.Title);
                    this.TextAuthor = b.Author;
                    this.TextTitle = b.Title;
                    this.TextYear = b.Year;
                    IsProgressEnabled = false;
                }
            }
        }

        /// <summary>
        /// Validator for Isbn TextBox.
        /// </summary>
        /// <param name="e">TextCompositionEventArgs</param>
        public void IsbnValidation(TextCompositionEventArgs e)
        {
            App.logger.Debug("IsbnValidation called...");
            if (e == null) return;
            e.Handled = !isOnlyIsbn(e.Text);
        }

        /// <summary>
        /// Validator for numbers only TextBox
        /// </summary>
        /// <param name="e">TextCompositionEventArgs</param>
        public void NumberValidation(TextCompositionEventArgs e)
        {
            App.logger.Debug("NumberValidation called...");
            if (e == null) return;
            e.Handled = !isOnlyNumber(e.Text);
        }

        private bool isOnlyNumber(string s)
        {
            // Accept only numeric values
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(s);
        }

        private bool isOnlyIsbn(string s)
        {
            // Accept numeric values and chars x X
            Regex regex = new Regex("[^0-9xX-]+");
            return !regex.IsMatch(s);
        }
    }
}
