using NBlog01.DB;
using NBlog01.DB.Context;
using NBlog01.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.ViewModels.MainView
{
    class AddPageViewModel : INotifyPropertyChanged
    {
        private string title;
        private string category;
        private string description;
        private string author;
        private readonly DbContext dbContext;
        private ObservableCollection<Articles> _tempArticles;

        public AddPageViewModel()
        {
            dbContext = DbContext.GetInstance();
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Author
        {
            get => author;
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
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

        public ObservableCollection<Articles> TempArticles
        {
            get => _tempArticles;
            set
            {
                _tempArticles = value;
                OnPropertyChanged(nameof(TempArticles));
            }
        }

        public RelayCommands AddArticle => new(_ =>
         {
             try
             {
                 if (Title == null || Category == null || Description == null || Author == null)
                     MessageBox.Show("Неправильные данные");
                 dbContext.connection.Open();
                 var command = dbContext.connection.CreateCommand();
                 command.CommandText = "Insert into Articles (title, category, descrpt, author) values " +
                     "(@title, @category, @descrpt, @author)";
                 command.Parameters.AddWithValue("@title", Title);
                 command.Parameters.AddWithValue("@category", Category);
                 command.Parameters.AddWithValue("@descrpt", Description);
                 command.Parameters.AddWithValue("@author", Author);
                 command.ExecuteNonQueryAsync();
                 dbContext.connection.Close();
             }
             catch(Exception ex)
             {
                 dbContext.connection.Close();
                 MessageBox.Show(ex.Message);
             }
         });

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
