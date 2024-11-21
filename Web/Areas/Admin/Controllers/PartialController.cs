using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class PartialController : Controller
    {
        // GET: Admin/Partial
        public PartialViewResult Menu()
        {
            ArticleContext db = new ArticleContext();
            ViewBag.NewComments = db.ArticleComments.Where(x => x.IsApproved == false).Count();  
            if (Login.Login.ActiveUser != null)
            {
                ViewBag.User = Login.Login.ActiveUser.UserName;
            }        
            return PartialView("_Menu");
        }
    }
}