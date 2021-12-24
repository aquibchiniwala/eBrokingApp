using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Presentation.Models;
using Repository;
using Repository.Entity;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService service;

        public TradeController(ITradeService service)
        {
            this.service = service;
        }

        // GET: api/Trade
        [HttpGet]
        public ActionResult<Trader> GetPortfolio()
        {
            return service.GetTraderDetails();
        }

        // POST: api/Trade
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("AddFunds")]
        public ActionResult AddFunds([FromBody] double amount)
        {
            service.AddFunds(amount);
            return Ok(service.GetTraderDetails());
        }

        [HttpPost("Buy")]
        public ActionResult Buy([FromBody] Transaction transaction)
        {
            return Ok(service.BuyTransaction(transaction.EquityName, transaction.Quantity, transaction.TransactionDateTime));
        }

        [HttpPost("Sell")]
        public ActionResult Sell([FromBody] Transaction transaction)
        {
            return Ok(service.SellTransaction(transaction.EquityName, transaction.Quantity, transaction.TransactionDateTime));
        }
    }
}
