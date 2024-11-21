using Buisenss.Interface.Abstract;
using DataAccess;
using DataModel.ArticleModels;
using Entity.Concrate;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace Buisenss.Interface.Service
{
    public class ArticleService : IAllService<Article>, IArticleService
    {
        ArticleContext db = new ArticleContext();
        ArticleImage image = new ArticleImage();
        public async Task Add(Article article)
        {
            db.Articles.Add(article);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int ArticleId)
        {
            var article = await db.Articles.FindAsync(ArticleId);
            if (article != null)
            {
                db.Articles.Remove(article);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Article> Details(int ArticleId)
        {
            return await db.Articles.Where(x => x.ArticleId == ArticleId).FirstOrDefaultAsync();
        }

        public async Task<Article> GetById(int? ArticleId)
        {
            return await db.Articles.FindAsync(ArticleId);
        }

        public async Task Update(Article article)
        {
            db.Entry(article).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task<IPagedList<Article>> GetFilterListOrAllList(string Search, int Page)
        {
            var commentList = await db.Articles.ToListAsync();
            var query = from x in commentList
                        select x;
            if (!String.IsNullOrEmpty(Search))
            {
                query = commentList.Where(x => x.ArticleTitle.Contains(Search) ||
                x.Category.CategoryName.Contains(Search));
            }
            return query.OrderByDescending(x => x.ArticleDate).ThenBy(x => x.ArticleId).ToPagedList(Page, 5);

        }
        public async Task AddImage(HttpPostedFileBase imageName, int Id)
        {

            if (imageName.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Entity/Images/"),
                    Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageName.FileName));
                image.ArticleId = Id;
                image.ImageName = Path.GetFileName(imageName.FileName);
                db.ArticleImages.Add(image);
                await db.SaveChangesAsync();
            }
        }
        public List<Category> CategoryList()
        {
            return db.Categories.ToList();
        }
        public async Task Delete(int[] imge_Id)
        {
            foreach (int imgeID in imge_Id)
            {
                //Delete images on ArticleImage Table
                ArticleImage articleImage = await db.ArticleImages.FindAsync(imge_Id);
                if (articleImage != null)
                {
                    db.ArticleImages.Remove(articleImage);

                    //Delete İmages in images Folder
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entity/Images/", articleImage.ImageName);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }

            }
            await db.SaveChangesAsync();
        }
        public async Task<List<ArticleImage>> GetImageList(int Id)
        {
            return await db.ArticleImages.Where(image => image.ArticleId == Id).ToListAsync();

        }

        public async Task<List<Article>> GetList()
        {
            var result = await db.Articles.ToListAsync();
            return result;
        }
    }
}
