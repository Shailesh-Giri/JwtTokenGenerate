using Practice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Infrastructure.IRepository
{
    public interface IProductRepository
    {
        Task<int> Add(Product product);
        Task<int> Update(Product product);
        Task<int> Delete(int id);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
    }
}
