using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

//SOLID
//BU YAPILAN O ÖZELLİĞİDİR. OPEN CLOSE PRİNCİPLE YANİ Yeni bir özellik eklenirse mevcuttaki hiç bir özellik ellenemez


ProductManager productManager = new ProductManager(new EFPorductDal());

foreach (var product in productManager.GetByUnitPrice(100,200))
{
    Console.WriteLine(product.ProductName);
}