using NBlog01.View.Windows;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.ViewModels.MainView
{
    public class WelcomeViewModel : ViewModel
    {
        private object _current;
        public object Current
        {
            get => _current;
            set => Set(ref _current, value);
        }
        public WelcomeViewModel()
        {
            SetAdminwindow = new RelayCommands(Enter);
        }

        public RelayCommands SetAdminwindow { get; set; }
        public void Enter(object obj)
        {
            AWindowDeleteArticle AWindow = new AWindowDeleteArticle();
            AWindow.Show();
        }
    }
}
