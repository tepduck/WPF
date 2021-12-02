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

namespace NBlog01.ViewModels.AdminVM
{
    public class DeleteUserVM : ViewModel
    {
        public UnitOfWork uoW;

        private List<User> users;
        public List<User> Users
        {
            get => users;
            set => Set(ref users, value);
        }

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set => Set(ref selectedUser, value);
        }

        public DeleteUserVM()
        {
            uoW = new UnitOfWork(new ArticleContext());
            users = uoW.Users.GetUsers();

            DeleteUser = new RelayCommands(obj =>
            {
                try
                {
                    if (SelectedUser.id != UserContext.GetInstance().User.id)
                    {
                        uoW.Users.Delete(SelectedUser);
                        uoW.Save();
                        MessageBox.Show("Пользователь удален!");
                    }
                    else MessageBox.Show("Вы не можете удалить самого себя!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("пользователь уже удален");
                }
            });
        }
        public RelayCommands DeleteUser { get; set; }
    }
}
