using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityOfMovie.Core.Convertors;
using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Generators;
using CityOfMovie.Core.Security;
using CityOfMovie.Core.Senders;
using CityOfMovie.Core.Services.Interfaces;
using CityOfMovie.Data.Context;
using CityOfMovie.Data.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace CityOfMovie.Core.Services
{
    public class UserService : IUserService
    {
        private CityOfMovieContext _context;
        private IViewRenderService _renderView;

        public UserService(CityOfMovieContext context, IViewRenderService renderView)
        {
            _context = context;
            _renderView = renderView;
        }

        public bool ActiveUser(string activeCode)
        {
            var user = _context.Users.FirstOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            else
            {
                user.IsActive = true;
                user.ActiveCode = ActiveCodeGeneratore.Generator();
                _context.SaveChanges();
                return true;
            }
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public int Deposit(int userId, int amount)
        {
            Wallet wallet = new Wallet()
            {
                UserId = userId,
                CreateDate = DateTime.Now,
                Amount = amount,
                Description = "واریز به حساب",
                IsPay = false,
                TypeId = 1,
            };
            _context.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public bool EditPassword(EditPasswordVm editPasswordVm, User user)
        {
            var userPass = HashPasswordGenerator.EncodePassword(editPasswordVm.OldPassword);
            var curentUser = _context.Users.FirstOrDefault(u => u.Password == userPass);
            if (curentUser == null)
            {
                return false;
            }
            user.Password = HashPasswordGenerator.EncodePassword(editPasswordVm.Password);
            UpdateUser(user);
            return true;
        }

        public EditUserPanelVm editUserPanelInformation(string username)
        {
            var user = GetUserByUsername(username);
            EditUserPanelVm editUserPanelInformation = new EditUserPanelVm()
            {
                Email = user.Email,
                AvatarName = user.UserAvatar,
                UserName = user.UserName,
                UserId = user.UserId,
            };
            return editUserPanelInformation;
        }

        public UserPanelSidebarAvatarVm GetAvatar(string username)
        {
            var user = GetUserByUsername(username);
            UserPanelSidebarAvatarVm userPanelSidebarAvatar = new UserPanelSidebarAvatarVm()
            {
                Avatar = user.UserAvatar
            };
            return userPanelSidebarAvatar;
        }

        public List<walletBalanceHistoryVM> GetBalanceHistory(string username)
        {
            var userId = GetUserByUsername(username).UserId;
            return _context.Wallets.Where(w=>w.UserId == userId && w.IsPay==true ).Select(w=> new walletBalanceHistoryVM()
            {
                Amount = w.Amount,
                CreateDate = w.CreateDate,
                Description = w.Description,
                TypeId = w.TypeId,
            }).ToList();
           
            
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public UserPanelVm GetUserInformation(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            UserPanelVm userPanelVm = new UserPanelVm()
            {
                Email = user.Email,
                UserName = user.UserName,
                RegisterDate = user.RegisterDate,
                Wallet = UserWalletBalance(username),

            };
            return userPanelVm;
        }

        public User GetUserWithActiveCode(string activeCode)
        {
            return _context.Users.FirstOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserWithEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _context.Wallets.Find(walletId);
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameExist(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public User LoginUser(LoginVm login)
        {
            string password = HashPasswordGenerator.EncodePassword(login.Password);
            return _context.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == password);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void UpdateUserInformations(int id, EditUserPanelVm userPanelVm)
        {
            var user = GetUserById(id);
            var stringPath = "";
            if (userPanelVm.Avatar != null)
            {
                if (userPanelVm.AvatarName != "Default.webp")
                {
                    stringPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", user.UserAvatar);
                    if (File.Exists(stringPath))
                    {
                        File.Delete(stringPath);
                    }
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(userPanelVm.Avatar.FileName);
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", imageName);
                using (var filestream = new FileStream(SavePath, FileMode.Create))
                {
                    userPanelVm.Avatar.CopyTo(filestream);
                }
                user.UserAvatar = imageName;

            }
            user.UserName = userPanelVm.UserName;
            if (user.Email != userPanelVm.Email)
            {

                user.IsActive = false;
                string emailBody = _renderView.RenderToStringAsync("_ActiveEmail", user);
                SendEmail.Send(userPanelVm.Email, "فعال سازی حساب کاربری", emailBody);

            }
            user.Email = userPanelVm.Email;
            UpdateUser(user);

        }

        public void UpdateWallet(Wallet wallet)
        {
           _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public int UserWalletBalance(string username)
        {
            var user = GetUserByUsername(username);
            var deposit = _context.Wallets
                .Where(w => w.UserId == user.UserId && w.TypeId == 1 && w.IsPay == true)
                .Select(w => w.Amount);
            var withdrawal = _context.Wallets
                .Where(w => w.UserId == user.UserId && w.TypeId == 2 && w.IsPay == true).Select(w => w.Amount);
            var UserWalletBalance = deposit.Sum() - withdrawal.Sum();
            return UserWalletBalance;
        }
    }
}
