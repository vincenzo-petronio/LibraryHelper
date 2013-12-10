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
using System.Threading.Tasks;

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

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="ss">Constructor Injection</param>
        public MainViewModel(ISearchService ss)
        {
            // INIT
            title = "Library Helper";

            // Guard Clause
            if (ss == null)
            {
                throw new ArgumentNullException("SearchService");
            }

            this.searchService = ss;
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
                Book b = await searchService.SearchForIsbnAsync(this.TextIsbn);
                if (b != null)
                {
                    App.logger.Info("Book found: \t" + b.Title);
                    this.TextAuthor = b.Author;
                    this.TextTitle = b.Title;
                    this.TextYear = b.Year;
                }
            }
        }
    }
}
