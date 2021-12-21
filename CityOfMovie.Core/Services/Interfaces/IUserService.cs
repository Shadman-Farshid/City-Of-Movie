using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityOfMovie.Core.DTOs;
using CityOfMovie.Data.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace CityOfMovie.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Common 
        public bool IsUsernameExist(string username);
        public bool IsEmailExist(string email);
        public int AddUser(User user);
        public void UpdateUser(User user);
        public User LoginUser(LoginVm login);
        public bool ActiveUser(string activeCode);
        public User GetUserWithEmail(string email);
        public User GetUserWithActiveCode(string activeCode);
        public User GetUserByUsername(string userName);
        public User GetUserById(int id);
        #endregion

        #region User Panel
        UserPanelVm GetUserInformation(string username);
        UserPanelSidebarAvatarVm GetAvatar(string username);
        EditUserPanelVm editUserPanelInformation(string username);
        void UpdateUserInformations(int id, EditUserPanelVm userPanelVm);
        bool EditPassword(EditPasswordVm editPasswordVm, User user);
        #endregion

        #region Wallet
        public int UserWalletBalance(string username);
        public int Deposit(int userId, int amount);
        public List<walletBalanceHistoryVM> GetBalanceHistory(string username);
        public Wallet GetWalletByWalletId(int walletId);
        public void UpdateWallet(Wallet wallet);
        #endregion
    }
}
