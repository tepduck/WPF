using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Repositories;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.ViewModels.MainView
{
    public class SearchVM : ViewModel
    {
        public UnitOfWork uoW;

        private List<Article> articles;
        public List<Article> Articles
        {
            get => articles;
            set => Set(ref articles, value);
        }

        private Article selectedArticle;
        public Article SelectedArticle
        {
            get => selectedArticle;
            set => Set(ref selectedArticle, value);
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set => Set(ref searchText, value);
        }

        public SearchVM()
        {

            Search = new RelayCommands(obj => 
            {
                try
                {
                    uoW = new UnitOfWork(new ArticleContext());
                    Articles = uoW.Articles.GetArticles();
                    if (Articles.Count() == 0)
                        MessageBox.Show("нет новостей");
                    Articles = uoW.Articles.GetArticleByTitle(SearchText);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        public RelayCommands Search { get; set; }
    }
}
