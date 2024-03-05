using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory;

public class InMemoryProductDal : IProductDal
{
    List<Product> _products;
    public InMemoryProductDal()
    {
        _products = new List<Product>() {
            new Product{ProductId=1,CategoryId=1,ProductName="Glass",UnitPrice=15,UnitsInStock=15},
            new Product{ProductId=2,CategoryId=1,ProductName="Camera",UnitPrice=500,UnitsInStock=3},
            new Product{ProductId=3,CategoryId=2,ProductName="Telephone",UnitPrice=1500,UnitsInStock=2},
            new Product{ProductId=4,CategoryId=2,ProductName="Keyboard",UnitPrice=150,UnitsInStock=65},
            new Product{ProductId=5,CategoryId=2,ProductName="Mouse",UnitPrice=85,UnitsInStock=1},

        };
    }
    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Delete(Product product)
    {
        //LİNQ
        //SingleOrDefault foreach gibi tek tek dolaşıcak.p ise foreach de foreach(var p in _products) daki p'dir.
        //parametre olarak gelen ProductId yi p ye ata
        Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

        _products.Remove(productToDelete);
    }

    public Product Get(Expression<Func<Product, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetAll()
    {
       return _products;
    }

    public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetAllByCategories(int categoryId)
    {
        return _products.Where(p=>p.CategoryId== categoryId).ToList();
    }

    public void Update(Product product)
    {
        //Gönderilen ürün id'sine sahip olan listedeki ürünü bul
        Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
        productToUpdate.ProductName = product.ProductName;
        productToUpdate.UnitPrice = product.UnitPrice;
        productToUpdate.CategoryId = product.CategoryId;
        productToUpdate.UnitsInStock = product.UnitsInStock;
    }
}
