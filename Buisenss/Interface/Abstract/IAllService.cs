using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Concrate;
using PagedList;

namespace Buisenss.Interface.Abstract
{
    public interface IAllService<T>
    {

        Task Add(T entity);
        Task Delete(int Id);
        Task<List<T>> GetList();
        Task<T> GetById(int? Id);
        Task Update(T entity);
        Task<IPagedList<T>> GetFilterListOrAllList(string Search, int Page);
    }
}
