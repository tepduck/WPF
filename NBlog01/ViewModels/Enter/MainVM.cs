using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.View.Windows;
using NBlog01.ViewModels.AdminVM;
using NBlog01.ViewModels.Base;
using NBlog01.ViewModels.MainView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NBlog01.ViewModels.Enter
{
    class MainVM : ViewModel
    {
        private int _language;
        private readonly List<string> _languages = new List<string>();

        private object _current;
        public UserWelcome _welcome { get; set; }
        public WelcomeViewModel _adminWelcome { get; set; }
        public CatalogViewModel _mainNews { get; set; }
        public AddPageViewModel _addNews { get; set; }
        public SearchVM search { get; set; }
        public BookmarkPageViewModel _bookmarks { get; set; }

        public object Current
        {
            get => _current;
            set => Set(ref _current, value);
        }

        public MainVM()
        {
            SetAdminwindow = new RelayCommands(Enter);
            MinimizeWindow = new RelayCommands(MinimWin);
            MaximizeWindow = new RelayCommands(MaximWin);
            ResizeWindow = new RelayCommands(ResWin);
            CloseWindow = new RelayCommands(CloseWin);

            _language = 0;
            _languages.Add("Ru-ru");
            _languages.Add("En-en");
            ChangeLng = new RelayCommands(ChLng);

            _welcome = new UserWelcome();
            _adminWelcome = new WelcomeViewModel();
            _addNews = new AddPageViewModel();
            _bookmarks = new BookmarkPageViewModel();
            _mainNews = new CatalogViewModel();
            search = new SearchVM();
            if (UserContext.GetInstance().User.role == "user")
                Current = _welcome;
            if (UserContext.GetInstance().User.role == "admin")
                Current = _adminWelcome;

            SetHomePage = new RelayCommands(obj =>
            {
                if (UserContext.GetInstance().User.role == "user")
                    Current = _welcome;
                if (UserContext.GetInstance().User.role == "admin")
                    Current = _adminWelcome;
            });
        }

        public RelayCommands CloseWindow { get; set; }
        public RelayCommands MaximizeWindow { get; set; }
        public RelayCommands MinimizeWindow { get; set; }
        public RelayCommands ResizeWindow { get; set; }
        public RelayCommands ChangeLng { get; set; }
        public RelayCommands WelcomeView { get; set; }
        public RelayCommands AWelcomeView { get; set; }
        public RelayCommands CatalogView { get; set; }
        public RelayCommands AddView { get; set; }
        public RelayCommands BookmarksView { get; set; }
        public RelayCommands SetmainNewsPage => new RelayCommands(obj => Current = _mainNews);
        public RelayCommands SetHomePage { get; set; }
        public RelayCommands SetAddPage => new RelayCommands(obj => Current = _addNews);
        public RelayCommands SetSearchPage => new RelayCommands(obj => Current = search);
        public RelayCommands SetBookmarkPage => new RelayCommands(obj => Current = _bookmarks);
        public RelayCommands SetAdminwindow { get; set; }

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

        public void Enter(object obj)
        {
            AWindowDeleteArticle AWindow = new AWindowDeleteArticle();
            AWindow.Show();
        }
    }
}
