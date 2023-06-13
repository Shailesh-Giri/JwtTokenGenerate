using Practice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Infrastructure.IRepository
{
    public interface ICategoryRepository
    {
        Task<int> Add(Category category);
        Task<int> Update(Category category);
        Task<int> Delete(int id);
        Task<Category> GetById(int id);
        Task<List<Category>> GetAll();
    }
}
