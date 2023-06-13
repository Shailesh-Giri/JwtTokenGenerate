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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.Include("Category").ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include("Category").Where(x => x.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<int> Update(Product product)
        {
            int entry = 0;
            Product oldProduct = await _context.Products.FindAsync(product.ProductId);
            if(oldProduct != null)
            {
                oldProduct.ProductName = product.ProductName;
                oldProduct.ProductDescription = product.ProductDescription;
                oldProduct.ProductPrice = product.ProductPrice;
                oldProduct.CategoryId = product.CategoryId;
                entry = await _context.SaveChangesAsync();
            }

            return entry;
        }
    }
}
