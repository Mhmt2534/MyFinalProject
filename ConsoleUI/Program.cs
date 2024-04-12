using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

//SOLID
//BU YAPILAN O ÖZELLİĞİDİR. OPEN CLOSE PRİNCİPLE YANİ Yeni bir özellik eklenirse mevcuttaki hiç bir özellik ellenemez

//Data Transormaiton Object

ProductTest();
//CategoryTest();






static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal(),
        new CategoryManager(new EFCategoryDal()));

    var result=productManager.GetProductDetailDtos();

    if (result.Success)
    {
        Console.WriteLine(result.Message);
        foreach (var product in result.Data)
        {
            Console.WriteLine(product.ProductName + "/" + product.CategoryName);
        }
    }
    else
    {
        Console.WriteLine(result.Message);
    }

   
}

/*
static void CategoryTest()
{
    CategoryManager categoryManager = new(new EFCategoryDal());
    foreach (var category in categoryManager.GetAll())
    {
        Console.WriteLine(category.CategoryName);
    }
}
*/