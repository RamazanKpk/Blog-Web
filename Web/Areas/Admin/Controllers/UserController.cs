using Buisenss.Interface.Abstract;
using Entity.Concrate;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IAllService<User> _allService;
        public UserController(IAllService<User> allService)
        {
            _allService = allService;
        }
        public UserController() { }
        // GET: User
        public async Task<ActionResult> Index()
        {
            var users = await _allService.GetList();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _allService.Add(user);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? Id)
        {
            var user = await _allService.GetById(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentPassword = user.Password;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    user.Password = ViewBag.CurrentPassword;
                }
                _allService.Update(user);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                _allService.Delete(Id);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int Id)
        {
            if (ModelState.IsValid)
            {
                _allService.GetById(Id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}