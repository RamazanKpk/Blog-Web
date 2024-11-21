using Buisenss.Interface.Abstract;
using DataAccess;
using Entity.Concrate;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Buisenss.Interface.Service
{
    public class CategoryService: IAllService<Category>
    {
        ArticleContext db = new ArticleContext();
        public async Task Add(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int Id)
        {
            var category = await db.Categories.FindAsync(Id);
            if (category != null)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<Category>> GetList()
        {
            return await db.Categories.ToListAsync();            
        }
        public async Task Update(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task<Category> GetById(int? Id)
        {
            return await db.Categories.FindAsync(Id);
        }

        public async Task<IPagedList<Category>> GetFilterListOrAllList(string Search, int Page)
        {
            var categoryList = await db.Categories.ToListAsync();
            var query = from x in categoryList
                        select x;
            if (!String.IsNullOrEmpty(Search))
            {
                query = categoryList.Where(x => x.CategoryName.Contains(Search));
            }
            return query.OrderByDescending(x => x.CategoryId).ToPagedList(Page, 10);
        }
    }
}
