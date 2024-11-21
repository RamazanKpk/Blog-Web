using Buisenss.Interface.Abstract;
using Entity.Concrate;
using PagedList;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        private readonly IAllService<Category> _allService;
        public CategoryController(IAllService<Category> allService)
        {
            _allService = allService;
        }
        public async Task<ActionResult> Index(string Search = null, int Page= 1)
        {
            var result = await _allService.GetFilterListOrAllList(Search, Page);
            return View(result);
        }
        public async Task<ActionResult> Delete(int Id)
        {
            await _allService.Delete(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            var category = await _allService.GetById(Id);
            return View(category);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Category category)
        {
            await _allService.Update(category);
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            await _allService.Add(category);
            return RedirectToAction("Index");
        }
    }
}