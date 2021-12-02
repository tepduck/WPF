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

namespace NBlog01.ViewModels.AdminVM
{
    public class DeleteArticleVM : ViewModel
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

        public DeleteArticleVM()
        {
            uoW = new UnitOfWork(new ArticleContext());
            articles = uoW.Articles.GetArticles();

            DeleteArticle = new RelayCommands(obj =>
            {
                try
                {
                    uoW.Articles.Delete(SelectedArticle);
                    uoW.Save();
                    MessageBox.Show("Статья удалена!");
                }
                catch 
                {
                    MessageBox.Show("статья уже удалена");
                }
            });
        }
        public RelayCommands DeleteArticle { get; set; }
    }
}
