using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely Coupled (geveş bağımlılık)
        //IoC Container (Inversion of Control) -- Bir kutu içine new PorductManager() gibi referans koyalım
        //kim ithiyaç duyarsa ona verelim

        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Swagger hazır dökümasyon
            //Dependency chain
            var res=_productService.GetAll();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var res=_productService.GetById(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("geybycategoryid")]
        public IActionResult GetByCategoryId(int id)
        {
            var res=_productService.GetAllByCategoryId(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }



        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            /*
            int deneme= _productService.GetAllByCategoryId(product.CategoryId).Data.Count;

            if (deneme>=10)
            {
                return BadRequest("Max sayıya ulaşıldı");
            }
            //Bu kısım aslında iş kuralı ama ben deneme amaçlı buraya yazdım. 
            */


            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
