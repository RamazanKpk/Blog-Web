using Buisenss.Interface.Abstract;
using DataAccess;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        ArticleContext db = new ArticleContext();
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public HomeController() { }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Login.Login.SignOut();
            return RedirectToAction("login", "Login", new {area =""});
        }
    }
}