using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
    ICategoryService _categoryService;
    public ProductManager(IProductDal productDal,ICategoryService categoryService)
    {
        _productDal = productDal;
        _categoryService = categoryService;
    }

    [SecuredOperation("product.add,admin")]
    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {

        IResult result= BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
            CheckIfProductNameOfSame(product.ProductName),
            CheckIfCategoryLimitExceded());

        if (result!=null)
        {
            return result;
        }


        _productDal.Add(product);
        return new Result(true, Messages.ProductAdded);
    }

    

    [ValidationAspect(typeof(ProductValidator))]
    public IResult Update(Product product)
    {
        if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success) 
        {
            _productDal.Update(product);
            return new Result(true, Messages.ProductAdded);
        }
        return new ErrorResult();
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

    //Bussines Particular

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
        if (result >= 10)
        {
            return new ErrorResult(Messages.CategroyIsMax);
        }
        return new SuccessResult();
    }

    private IResult CheckIfProductNameOfSame(string porductName)
    {
        var res=_productDal.GetAll(p=>p.ProductName == porductName).Any();

        if (res)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }
        return new SuccessResult();

    }

    private IResult CheckIfCategoryLimitExceded()
    {
        var result = _categoryService.GetAll();
        if (result.Data.Count>=15)
        {
            return new ErrorResult(Messages.CategoryIsMax);
        }
        return new SuccessResult();
    }


}
