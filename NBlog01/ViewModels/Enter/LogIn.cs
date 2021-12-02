using NBlog01.Authentication;
using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Repositories;
using NBlog01.View.Windows;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.ViewModels.Enter
{
    class LogIn : ViewModel
    {
        private string _username;
        private string _password;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged("");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("");
            }
        }

        public IAuth auth { get; }
        public UnitOfWork unitOfWork { get; }

        public string Error { get; set; }

        private int _language;
        private readonly List<string> _languages = new List<string>();

        public LogIn()
        {
            unitOfWork = new UnitOfWork(new ArticleContext());
            auth = new Auth(unitOfWork);

            MinimizeWindow = new RelayCommands(MinimWin);
            CloseWin = new RelayCommands(obj=>CloseWindow());

            _language = 0;
            _languages.Add("Ru-ru");
            _languages.Add("En-en");
            ChangeLng = new RelayCommands(ChLng);
            LogInCommand = new RelayCommands(obj =>
            {
                if(UsernameValidation(Username) && PasswordValidation(Password))
                {
                    try
                    {
                        UserContext.GetInstance().User = auth.Login(Username, Password);

                        Main main = new Main()
                        {
                            DataContext = new MainVM()
                        };
                        main.Show();
                        CloseWindow();
                    }
                    catch
                    {
                        MessageBox.Show("Проверьте данные");
                    }
                }
                else MessageBox.Show("Проверьте данные");
            });
            Regestry = new RelayCommands(obj =>
            {
                UnitOfWork unitOfWork = new UnitOfWork(new ArticleContext());
                IAuth auth = new Auth(unitOfWork);
                if(UsernameValidation(Username) && PasswordValidation(Password))
                {
                    try
                    {
                        IAuth.Result result = auth.Register(Username, Password);

                        if (result == IAuth.Result.Success)
                            MessageBox.Show("пользователь зарегистрирован!");
                        if (result == IAuth.Result.LoginProblem)
                            MessageBox.Show("Проблема с логином!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else MessageBox.Show("Проверьте данные");
            });
        }

        public RelayCommands LogInCommand { get; set; }
        public RelayCommands Regestry { get; set; }
        public RelayCommands MinimizeWindow { get; set; }
        public RelayCommands CloseWin { get; set; }
        public RelayCommands ChangeLng { get; set; }

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
        
        public void ChLng(object obj)
        {
            _language = _language == 1 ? 0 : 1;
            var uri = new Uri("Localization/" + _languages[_language] + ".xaml", UriKind.Relative);
            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Close();
                }
            }
        }

        public bool UsernameValidation(string username)
        {
            bool flag = true;
            if (username == null || username.Length > 15)
                flag = false;
            return flag;
        }

        public bool PasswordValidation(string password)
        {
            bool flag = true;
            if (password == null || password.Length > 15)
                flag = false;
            return flag;
        }
    }
}
