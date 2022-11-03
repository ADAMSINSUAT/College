using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FoodMenuAPITest.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodMenuAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITemporaryInvoiceTableRepository _repository;

        public GetOrderController(ITemporaryInvoiceTableRepository repository)
        {
            _repository = repository;
            //_configuration = configuration;
        }

        //public IActionResult Index()
        //{
        //    _repository.GetAllOrders();
        //}

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return Ok(_repository.GetAllOrders(id));
           return _repository.GetAllOrders(id);
        }
    }
}