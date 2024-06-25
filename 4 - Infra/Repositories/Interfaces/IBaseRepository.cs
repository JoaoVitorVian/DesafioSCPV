using CadastroPostosVacinacao.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Delete(long id);
        Task<T> Get(long id);
        Task<List<T>> GetAll();
    }
}
