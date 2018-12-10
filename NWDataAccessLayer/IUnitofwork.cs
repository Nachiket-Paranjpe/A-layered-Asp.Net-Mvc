using System.Collections.Generic;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public interface IUnitofwork
    {
        //GenericRepository<Products> ProductRepository { get;}
        //GenericRepository<Orders> OrderRepository { get;}
        //GenericRepository<Customer> CustomerRepository { get;}
        IGenericRepository<T> Repository<T>() where T : class;
        void Save(bool savechanges);
        Task<Dictionary<string, string>> CheckandResolveConcurrencyIssuesAsync(bool concurrencyCheckclientWins);
        void Dispose();
    }
}
