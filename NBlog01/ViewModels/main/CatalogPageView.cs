using NBlog01.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.ViewModels.main
{
    class CatalogPageView : INotifyPropertyChanged
    {
        private readonly DbContext _dbContext;

        private Articles _slectArticle;
        private ObservableCollection<Articles> _articles;

        public ObservableCollection<Articles> Articles
        {
            get => _articles;
            set
            {
                _articles = value;
                OnPropertyChanged(nameof(Articles));
            }
        }

        public Articles SelectArticle
        {
            get => _slectArticle;
            set
            {
                _slectArticle = value;
                OnPropertyChanged(nameof(SelectArticle));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
