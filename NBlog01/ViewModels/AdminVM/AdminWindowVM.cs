using NBlog01.ViewModels.Base;
using NBlog01.ViewModels.MainView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NBlog01.ViewModels.AdminVM
{
    public class AdminWindowVM : ViewModel
    {
        private object _current;
        private UserWelcome _welcome;
        private readonly DeleteUserVM _deleteUser;
        private readonly DeleteArticleVM _deleteArticle;

        public object Current
        {
            get => _current;
            set => Set(ref _current, value);
        }
        public AdminWindowVM()
        {
            MinimizeWindow = new RelayCommands(MinimWin);
            MaximizeWindow = new RelayCommands(MaximWin);
            ResizeWindow = new RelayCommands(ResWin);
            CloseWindow = new RelayCommands(CloseWin);

            _welcome = new UserWelcome();
            _deleteArticle = new DeleteArticleVM();
            _deleteUser = new DeleteUserVM();
            Current = _welcome;
        }
        public RelayCommands CloseWindow { get; set; }
        public RelayCommands MaximizeWindow { get; set; }
        public RelayCommands MinimizeWindow { get; set; }
        public RelayCommands ResizeWindow { get; set; }
        public RelayCommands SetHomePage => new RelayCommands(obj => Current = _welcome);
        public RelayCommands SetDeleteUserPage => new RelayCommands(obj => Current = _deleteUser);
        public RelayCommands SetDeleteArticlePage => new RelayCommands(obj => Current = _deleteArticle);

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
    }
}
