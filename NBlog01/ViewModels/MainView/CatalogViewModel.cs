using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Repositories;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.ViewModels.MainView
{
    public class CatalogViewModel : ViewModel
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

        private List<ArticlesBookmark> bookmark;
        public List<ArticlesBookmark> Bookmarks
        {
            get => bookmark;
            set => Set(ref bookmark, value);
        }

        public CatalogViewModel()
        {
            uoW = new UnitOfWork(new ArticleContext());
            Articles = uoW.Articles.GetArticles();

            AddtoBookmark = new RelayCommands(obj=>
            {
                int UserId = UserContext.GetInstance().User.id;
                Bookmark bookmark = new Bookmark();
                uoW.Bookmarks.Add(bookmark);
                ArticlesBookmark NewArticlesBookmark = new ArticlesBookmark(bookmark.id, SelectedArticle.id);
                ArticlesBookmark articlesBookmark = uoW.ArticlesBookmark.GetBookmarkByArticleId(SelectedArticle.id, UserId);
                if(articlesBookmark == null)
                {
                    articlesBookmark = new ArticlesBookmark();
                    if(uoW.ArticlesBookmark.GetUserId(articlesBookmark) == UserId)
                    {
                        uoW.ArticlesBookmark.Add(NewArticlesBookmark);
                        uoW.Save();
                        MessageBox.Show("закладка добавлена");
                    }
                }
                else MessageBox.Show("Пост уже добавлен в закладки");
            });
        }
        public RelayCommands AddtoBookmark { get; set; }
    }
}
