using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buisenss.Interface.Abstract.ApiAbstract
{
    public interface IAllApiServices<T, A, C>
    {
        Task<List<T>> GetList();
        Task CreateOrUpdate(A entity);
        Task<List<C>> Details();
    }
}
