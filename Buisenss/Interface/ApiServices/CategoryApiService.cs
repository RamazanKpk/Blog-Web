using Buisenss.Interface.Abstract.ApiAbstract;
using DataAccess;
using DataModel.CategoryModels;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.ArticleModels;

namespace Buisenss.Interface.ApiServices
{
    public class CategoryApiService : IAllApiServices<CategoryListModel, Category, CategoryListModel>
    {
        ArticleContext db = new ArticleContext();
        public async Task CreateOrUpdate(Category category)
        {
            //var getCategoryList = await db.Categories.ToListAsync();
            //bool categoryExists = false;
            //foreach (Category item in getCategoryList) {
            //    db.Entry(category).State = EntityState.Modified;
            //    categoryExists = true;
            //    break;
            //}
            //if (!categoryExists)
            //{
            //    db.Categories.Add(category);
            //}
            await db.SaveChangesAsync();
        }

        public Task<List<CategoryListModel>> Details()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryListModel>> GetList()
        {
            var result = await db.Categories.Where(x => x.IsActive)
                .Select(c => new CategoryListModel()
                {
                    Id = c.CategoryId,
                    Name = c.CategoryName,
                    Articles = c.Articles.Where(x=> x.Category.CategoryId == c.CategoryId)
                    .Select(a => new ArticleListModel()
                    {
                        Id = a.ArticleId,
                        Date = a.ArticleDate,
                        CategoryName = c.CategoryName,
                        Title = a.ArticleTitle
                    }).ToList()
                }).ToListAsync();
            return result;
        }
    }
}
