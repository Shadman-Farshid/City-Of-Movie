using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityOfMovie.Web.Controllers
{
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
             _userService = userService;
        }

        [Authorize]
        [Route("Wallet")]
        public IActionResult Index()
        {
            ViewBag.balanceHistory = _userService.GetBalanceHistory(User.Identity.Name);
            return View();
        }

        [HttpPost]
        [Route("Wallet/Deposit")]
        public IActionResult Index(WalletDepositVM walletDepositVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = _userService.GetUserByUsername(User.Identity.Name).UserId;
           int walletId =  _userService.Deposit(userId, walletDepositVM.Amount);
            #region Onlin Payment
            var payment = new ZarinpalSandbox.Payment(walletDepositVM.Amount);
            var response = payment.PaymentRequest("شارژ کیف پول", "https://localhost:7227/OnlinePayment/"+ walletId, "shadman.test1@gmail.com", "09365950606");
            if(response.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + response.Result.Authority);
            }
            #endregion
            return Redirect("Wallet");
        }
        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = _userService.GetWalletByWalletId(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }
            return View();
        }


    }
}
