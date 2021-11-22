using AutoMapper;
using Microsoft.EntityFrameworkCore;
using serverApp.Data;
using serverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductModel>> GetAllProduct()
        {
            
            var products =await _context.Products.Where(x=>x.Available == true).ToListAsync();

            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<ProductModel> GetProduct(int ID)
        {

            var product = await _context.Products.FindAsync(ID);

            return _mapper.Map<ProductModel>(product);
        }
        public async Task<int> AddProduct(ProductModel model)
        {
            var product = _mapper.Map<Products>(model);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
        public async Task<int> UpdateProductAsync(int Id,ProductModel model)
        {

            var product = _mapper.Map<Products>(model);
            product.Id = Id;
            _context.Products.Update(product);
           int response = await _context.SaveChangesAsync();
            return response;
        }
        public async Task<int> ProductRemoveAsync(int Id)
        {
            var product = new Products()
            {
                Id = Id
            };
            _context.Products.Remove(product);
            int response =await _context.SaveChangesAsync();
            return response;
        }
    }
}
