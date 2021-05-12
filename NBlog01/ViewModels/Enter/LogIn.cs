using NBlog01.DB;
using NBlog01.Models.Authentication;
using NBlog01.View.Windows;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.ViewModels.Enter
{
    class LogIn : INotifyPropertyChanged
    {
        private string _username="";
        private string _password="";
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private int _language;
        private readonly List<string> _languages = new List<string>();

        public LogIn()
        {
            MinimizeWindow = new RelayCommands(MinimWin);
            CloseWindow = new RelayCommands(CloseWin);
            EnterWindow = new RelayCommands(Enter);
         //   Regestry = new RelayCommands(CreateAccount);

            _language = 0;
            _languages.Add("Ru-ru");
            _languages.Add("En-en");
            ChangeLng = new RelayCommands(ChLng);
        }

        public RelayCommands Regestry { get; set; }
        public RelayCommands CloseWindow { get; set; }
        public RelayCommands MinimizeWindow { get; set; }
        public RelayCommands EnterWindow { get; set; }
        public RelayCommands ChangeLng { get; set; }

        public void CloseWin(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Close();
                }
            }
        }

        public void MinimWin(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).WindowState=WindowState.Minimized;
                }
            }
        }

        public void Enter(object obj)
        {
            Main news_w = new Main();
            news_w.Show();
            Window win = obj as Window;
            win.Close();
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
