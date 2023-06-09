﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result=_productService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("getbycategoryid")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetAllByCategoryId(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}