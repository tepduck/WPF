using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBlog01.DB.Context
{
    public class DbContext : IDisposable
    {
        private static readonly Lazy<DbContext> Instance = new(() => new DbContext());
        string ConnectionStr = ConfigurationManager.ConnectionStrings["KKD_DBEntities1"].ConnectionString;
        public readonly SqlConnection connection;

        private readonly UserContext userContext;
        public static ObservableCollection<Articles> Articles;

        private DbContext()
        {
            userContext = UserContext.GetInstance();

            connection = new SqlConnection(ConnectionStr);
            Articles = new ObservableCollection<Articles>();
            GetArticles(); 
        }

        public static DbContext GetInstance()
        {
            return Instance.Value;
        }

        private void GetArticles()
        {
            try
            {
                Articles.Clear();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Articles";
                var result = command.ExecuteReaderAsync().Result;
                while (result.ReadAsync().Result)
                {
                    Articles.Add(new Articles()
                    {
                        artcle_id=result.GetInt32(result.GetOrdinal("artcle_id")),
                        category=result.GetString(result.GetOrdinal("category")),
                        title = result.GetString(result.GetOrdinal("title")),
                        descrpt =result.GetString(result.GetOrdinal("descrpt")),
                        author=result.GetString(result.GetOrdinal("author")),
                      //  comm_title=result.GetString(result.GetOrdinal("comm_title")),
                        rating=result.GetInt32(result.GetOrdinal("rating"))
                    });
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void AddArticle(Articles articles)
        {
            Articles.Add(articles);
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Insert into Articles (title, category, descrpt, author) values " +
                    "(@title, @category, @descrpt, @author)";
                command.Parameters.AddWithValue("@title", articles.title);
                command.Parameters.AddWithValue("@category", articles.category);
                command.Parameters.AddWithValue("@descrpt", articles.descrpt);
                command.Parameters.AddWithValue("@author", articles.author);
                command.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteArticle(Articles articles)
        {
            Articles.Remove(articles);
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Delete from Articles where title='@title'";
                command.Parameters.AddWithValue("@title", articles.title);
                command.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Dispose()
        {
            connection?.Close();
        }
    }
}
