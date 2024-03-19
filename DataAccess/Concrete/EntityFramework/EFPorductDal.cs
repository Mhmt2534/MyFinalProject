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

}
