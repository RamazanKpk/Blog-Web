using Buisenss.Interface.Abstract;
using Buisenss.Interface.Abstract.AuthAbstract;
using Entity.Concrate;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Security;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAllService<User> _allService;
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly IArticleService _articleService;
        public LoginController(IUserService userService, IAllService<User> allService, HttpClient httpClient, ITokenService tokenService, IArticleService articleService)
        {
            _userService = userService;
            _allService = allService;
            _httpClient = httpClient;
            _tokenService = tokenService;
            _articleService = articleService;
        }
        public LoginController() { }
        public ActionResult login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.Message = TempData["Message"];
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> login(User user, string FormType, string NewUser, string NewPassword, string ReturnUrl)
        {
            if (FormType == "signup")
            {
                string resultMessage = string.Empty;
                var checkUser = await _userService.CheckUserName(NewUser);
                if (NewUser == checkUser.UserName)
                {
                    resultMessage = "This Username has been taken";
                }
                else
                {
                    var newUser = new User
                    {
                        UserName = NewUser,
                        EMail = user.EMail,
                        Password = NewPassword,
                        IsActive = true,
                        IsAdmin = false
                    };
                    await _allService.Add(newUser);
                    resultMessage = "Registration was successful, please login.";
                }
                TempData["Message"] = resultMessage;

                //if (isSuccess)
                //    return RedirectToAction("login");
                //else
                return View();
            }
            else
            {
                var logUser = await _userService.UserLog(user);
                if (logUser != null)
                {
                    //logUser.Articles.Add(new Article { });
                    //logUser.Articles = await _allService.GetList();
                    var sendUser = await SendUser(logUser);
                    bool isAdmin = logUser.IsAdmin;
                    Login.Login.ActiveUser = logUser;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    Session["User"] = user.UserName;
                    if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
                    {
                        return RedirectToAction("Index", "Article", new { area = "" });
                    }
                    //return Redirect(ReturnUrl);
                    return RedirectToAction("Index", "Article", new { area = "" });
                }

            }
            ViewBag.Mesaj = "User Not Found";
            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ResetPassword(string userName, string newPassword)
        {
            var user = await _userService.GetUserByName(userName);
            if (user != null)
            {
                user.Password = newPassword;
                await _allService.Update(user);
                TempData["Message"] = "Your password has been successfully updated, you can log in.";
                return RedirectToAction("login");
            }
            else
            {
                ViewBag.Error = "User Not Found";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Login.Login.SignOut();
            return RedirectToAction("login", "Login");
        }
        public async Task<User> SendUser(User user)
        {
            string apiUrl = ConfigurationManager.AppSettings["LogUserEndPoint"];
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var logUser = new User();

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage reponse = await client.PostAsync(apiUrl, content);
                if (reponse.IsSuccessStatusCode)
                {
                    string jsonData = await reponse.Content.ReadAsStringAsync();
                    logUser = JsonConvert.DeserializeObject<User>(jsonData);
                }
            }
            return logUser;
        }

    }
}