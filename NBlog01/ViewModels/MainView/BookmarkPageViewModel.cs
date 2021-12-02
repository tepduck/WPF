using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Repositories;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.ViewModels.MainView
{
    public class BookmarkPageViewModel : ViewModel
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

        public BookmarkPageViewModel() 
        {
            uoW = new UnitOfWork(new ArticleContext());
            using (ArticleContext articleContext = new ArticleContext())
            {
                string username = UserContext.GetInstance().User.username;
                int id = UserContext.GetInstance().User.id;
                try
                {
                    var result = from ab in articleContext.ArticlesBookmarks
                                 where ab.UserId == id
                                 join a in articleContext.Articles
                                 on ab.ArticleId equals a.id
                                 select new { Articles = a };
                    Articles = new List<Article>();
                    foreach (var a in result)
                        Articles.Add(a.Articles);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
