using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework;

public class EFPorductDal : IProductDal
{
    public void Add(Product entity)
    {
        //usin içine yazılan nesneler using bitince çöpe atılır.çünkü Context nesnei pahalı
        //IDisposable pattern implementation of c#
        using (NorthwindContext context=new NorthwindContext())
        {
            var addedEntity = context.Entry(entity); //bağlamdaki verilen entity nesnesini all
            addedEntity.State=EntityState.Added;//durumu EntityState. ile ayarlarız
            context.SaveChanges();
        }
    }

    public void Delete(Product entity)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            var deletedEntity = context.Entry(entity); 
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }

    public void Update(Product entity)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            var updatedEntity = context.Entry(entity); //bağlamdaki verilen entity nesnesini all
            updatedEntity.State = EntityState.Modified;//durumu EntityState. ile ayarlarız
            context.SaveChanges();
        }
    }

    public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
    {
        using (NorthwindContext context=new NorthwindContext())
        {
            return filter == null ? context.Set<Product>().ToList() 
                : context.Set<Product>().Where(filter).ToList();
        }
    }

    public Product Get(Expression<Func<Product, bool>> filter)
    {
        using (NorthwindContext context=new NorthwindContext())
        {
            return context.Set<Product>().SingleOrDefault(filter);
        }
    }
}
