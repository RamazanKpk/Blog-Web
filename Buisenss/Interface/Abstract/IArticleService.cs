using Entity.Concrate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Buisenss.Interface.Abstract
{
    public interface IArticleService
    {
        Task<Article> Details(int Id);
        Task AddImage(HttpPostedFileBase imageName, int Id);
        Task Delete(int[] imge_Id);
        Task<List<ArticleImage>> GetImageList(int Id = 0);
        List<Category> CategoryList();
    }

}
