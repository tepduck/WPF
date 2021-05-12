using NBlog01.DB;
using NBlog01.DB.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.ViewModels.MainView
{
    class CatalogViewModel : INotifyPropertyChanged
    {
        private readonly DbContext dbContext;

        private Articles _selectArticles;
        private ObservableCollection<Articles> _tempArticles;

        public CatalogViewModel()
        {
            dbContext = DbContext.GetInstance();
            TempArticles = new ObservableCollection<Articles>();
            GetArticles();
        }

        public ObservableCollection<Articles> TempArticles
        {
            get => _tempArticles;
            set
            {
                _tempArticles = value;
                OnPropertyChanged(nameof(TempArticles));
            }
        }

        public Articles Selectarticle
        {
            get => _selectArticles;
            set
            {
                _selectArticles = value;
                OnPropertyChanged(nameof(Selectarticle));
            }
        }

        public ObservableCollection<Articles> Articles
        {
            get => DbContext.Articles;
            set
            {
                DbContext.Articles = value;
                OnPropertyChanged(nameof(Articles));
            }
        }

        private void GetArticles()
        {
            TempArticles.Clear();
            foreach (var i in DbContext.Articles)
                TempArticles.Add(i);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
