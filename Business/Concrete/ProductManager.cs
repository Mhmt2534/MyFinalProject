using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }

    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {
        //business codes
        //validation


        _productDal.Add(product);
        return new Result(true,Messages.ProductAdded);
    }

    public IDataResult<List<Product>> GetAll()
    {
        //business codes // iş kodları
        //Yetkisi varmı
        //if (DateTime.Now.Hour==14)
        //{
        //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        //}


        return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
    }

    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == id));
    }

    public IDataResult<Product> GetById(int id)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == id));
    }

    public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
    {
        if (DateTime.Now.Hour == 22)
        {
            return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
        }

        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetailDtos(),Messages.ProductsListed);
    }



}
