using Microsoft.EntityFrameworkCore;
using Practice.Domain.Context;
using Practice.Domain.Entities;
using Practice.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int id)
        {
            Category category =  await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<int> Update(Category category)
        {
            int entry = 0;
            Category oldcCategory = await _context.Categories.FindAsync(category.CategoryId);
            if(oldcCategory != null)
            {
                oldcCategory.CategoryName = category.CategoryName;
                entry = await _context.SaveChangesAsync();
            }

            return entry;
        }
    }
}
