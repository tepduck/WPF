using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NBlog01.ViewModels.Enter
{
    class MainVM : INotifyPropertyChanged
    {
        private int _language;
        private readonly List<string> _languages = new List<string>();

        private Page _current;
        private readonly Page _welcome;
        private readonly Page _mainNews;
        private readonly Page _addNews;
        private readonly Page _searchNews;
        private readonly Page _bookmarks;

        public Page Current
        {
            get => _current;
            set
            {
                _current = value;
                OnPropertyChanged("Current");
            }
        }
        public MainVM()
        {
            MinimizeWindow = new RelayCommands(MinimWin);
            MaximizeWindow = new RelayCommands(MaximWin);
            ResizeWindow = new RelayCommands(ResWin);
            CloseWindow = new RelayCommands(CloseWin);

            _language = 0;
            _languages.Add("Ru-ru");
            _languages.Add("En-en");
            ChangeLng = new RelayCommands(ChLng);

            _welcome = new View.Welcome();
            _addNews = new View.AddPage();
            _searchNews = new View.SearchPage();
            _mainNews = new View.CatalogPage();//неоределенные данные
            _bookmarks = new View.BookmarkPage();
            Current = _welcome;
        }

        public RelayCommands CloseWindow { get; set; }
        public RelayCommands MaximizeWindow { get; set; }
        public RelayCommands MinimizeWindow { get; set; }
        public RelayCommands ResizeWindow { get; set; }
        public RelayCommands ChangeLng { get; set; }
        public RelayCommands SetmainNewsPage => new RelayCommands(obj => Current = _mainNews);
        public RelayCommands SetHomePage => new RelayCommands(obj => Current = _welcome);
        public RelayCommands SetAddPage => new RelayCommands(obj => Current = _addNews);
        public RelayCommands SetSearchPage => new RelayCommands(obj => Current = _searchNews);
        public RelayCommands SetBookmarkPage => new RelayCommands(obj => Current = _bookmarks);

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        public void MinimWin(object obj)
        {
            Window win = obj as Window;
            win.WindowState = WindowState.Minimized;
        }

        public void ResWin(object obj)
        {
            Window win = obj as Window;
            win.WindowState = WindowState.Normal;
            win.Height = 600;
            win.Width = 900;
        }

        public void MaximWin(object obj)
        {
            Window win = obj as Window;
            win.WindowState = WindowState.Maximized;
        }

        public void ChLng(object obj)
        {
            _language = _language == 1 ? 0 : 1;
            var uri = new Uri("Localization/" + _languages[_language] + ".xaml", UriKind.Relative);
            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
