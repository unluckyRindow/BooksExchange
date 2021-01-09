﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksDealersAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BooksDealersAPI.FrontendModels;


namespace BooksDealersAPI.Controllers
{
    [ApiController]
    [Route("api/trades")]
    public class TradesController : Controller
    {
        private readonly ITradeService _tradeService;


        public TradesController(
                ITradeService tradeService
            )
        {
            _tradeService = tradeService;
        }

        [HttpGet("user-trades/{id}")]
        public IActionResult UserTrades(int id)
        {
            return Ok(_tradeService.GetUserTrades(id));
        }

        [HttpGet("{id}")]
        public IActionResult Trade(int id)
        {
            return Ok(_tradeService.GetTrade(id));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrade([FromBody] TradeViewModel trade, int id)
        {
            bool updated = _tradeService.UpdateTrade(trade);
            if (updated)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddTrade([FromBody] TradeAddModel trade)
        {
            bool created = _tradeService.AddTrade(trade);
            if (created)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrade(int id)
        {
            bool deleted = _tradeService.DeleteTrade(id);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        }
        
    }
}
