using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.extensions;
using backend.models;
using backend.repositories;
using backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFinModelingPrepAPIService _finModelingPrepAPIService;

        public PortfolioController(UserManager<User> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository, IFinModelingPrepAPIService finModelingPrepAPIService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _finModelingPrepAPIService = finModelingPrepAPIService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var connectedUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(connectedUser);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var connectedUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _finModelingPrepAPIService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("Stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }

            if(stock == null)
            {
                return BadRequest("Stock with given symbol not found");
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(connectedUser);

            if(userPortfolio.Any(stock => stock.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Cannot add same stock to portfolio");
            }

            var portfolio = new Portfolio{
                StockId = stock.Id,
                UserId = connectedUser.Id
            };

            await _portfolioRepository.CreateAsync(portfolio);

            if(portfolio == null)
            {
                return StatusCode(500, "Could not create portfolio");
            }
            else{
                return Created();
            }

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var connectedUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(connectedUser);
            var filterStocks = userPortfolio.Where(stock => stock.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filterStocks.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolio(connectedUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }

            return Ok();
        }
        
    }
}