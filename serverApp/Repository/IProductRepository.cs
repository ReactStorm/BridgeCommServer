using serverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProduct();
        Task<ProductModel> GetProduct(int ID);
        Task<int> AddProduct(ProductModel model);
        Task<int> UpdateProductAsync(int Id, ProductModel model);
        Task<int> ProductRemoveAsync(int Id);
    }
}
