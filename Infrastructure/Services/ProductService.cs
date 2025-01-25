using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services;


public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
   public void AddProduct(Product product)
    {
        productRepository.AddProduct( product);
    }

    public void DeleteProduct(Product product)
    {
        productRepository.DeleteProduct( product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await productRepository.GetBrandsAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await productRepository.GetProductByIdAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand,
        string? type, string? sort)
    {
         return await productRepository.GetProductsAsync(brand, type, sort);
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
       return await productRepository.GetTypesAsync();
    }

    public bool ProductExists(int id)
    {
        return productRepository.ProductExists(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await productRepository.SaveChangesAsync();
    }

    public void UpdateProduct(Product product)
    {
        productRepository.UpdateProduct(product);
    }
}
