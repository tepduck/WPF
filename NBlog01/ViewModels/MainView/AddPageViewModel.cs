using Microsoft.Win32;
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
    public class AddPageViewModel : ViewModel
    {
        public UnitOfWork uoW;
        private string title;
        private string category;
        private string description;
        private string comment;
        public string author;
        private string image;

        private List<Article> articles;
        public List<Article> Articles
        {
            get => articles;
            set => Set(ref articles, value);
        }

        public RelayCommands AddArticle { get; set; }
        public RelayCommands AddImage { get; set; }

        public AddPageViewModel()
        {
            CatalogViewModel catalog = new CatalogViewModel();

            AddArticle = new RelayCommands(o=>
            {
                if (TitleValidation(title) && CategoryValidation(category) && CommentValidation(comment))
                {
                    Article article = new Article(title, category, description, comment, image);

                    try
                    {
                        uoW = new UnitOfWork(new DB.EFContext.ArticleContext());
                        uoW.Articles.Add(article);
                        uoW.Save();
                        MessageBox.Show("Статья добавлена!");
                        catalog.Articles = uoW.Articles.GetArticles();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw;
                    }
                }
                else MessageBox.Show("Введите все данные");
            });

            AddImage = new RelayCommands(o => 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg; *.jpg)|*.png;*.jpeg; *.jpg";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(openFileDialog.ShowDialog()==true)
                    Image=openFileDialog.FileName;
            });
        }

        private void ClearFields()
        {
            Title = null;
            Category = null;
            Description = null;
            Comment = null;
        }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string Category
        {
            get => category;
            set => Set(ref category, value);
        }

        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        public string Comment
        {
            get => comment;
            set => Set(ref comment, value);
        }

        public string Author
        {
            get => author;
            set => Set(ref author, value);
        }

        public string Image
        {
            get => image;
            set => Set(ref image, value);
        }

        public bool TitleValidation(string title)
        {
            bool flag = true;
            if (title == null)
                flag = false;
            else
            {
                if (title.Length > 10)
                    flag = false;
            }
            return flag;
        } 

        public bool CategoryValidation(string category)
        {
            bool flag = true;
            if (category == null)
                flag = false;
            else
            {
                if (category.Length > 15)
                    flag = false;
            }
            return flag;
        }

        public bool CommentValidation(string comment)
        {
            bool flag = true;
            if (comment != null)
            {
                if (comment.Length > 30)
                    flag = false;
            }
            return flag;
        }
    }
}
