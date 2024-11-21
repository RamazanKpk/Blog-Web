using Buisenss.Interface.Abstract.ApiAbstract;
using DataModel.CategoryModels;
using Entity.Concrate;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlogEducation.Controllers
{
    public class CategoryApiController : ApiController
    {
        private readonly IAllApiServices<CategoryListModel, Category, CategoryListModel> _allApiServices;

        public CategoryApiController(IAllApiServices<CategoryListModel, Category, CategoryListModel> allApiServices)
        {
            _allApiServices = allApiServices;
        }

        [HttpGet]
        [Route("api/category/categories")]
        public async Task<IHttpActionResult> GetCategory()
        {
            var result = await _allApiServices.GetList();
            return Json(result);
        }
    }
}
