using web_api.Data;
using web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace web_api.Services
{
    public class ProductService
    {
        private readonly UserDbContext _context;

        public ProductService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Reviews).ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProduct(int id, Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Details = updatedProduct.Details;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
