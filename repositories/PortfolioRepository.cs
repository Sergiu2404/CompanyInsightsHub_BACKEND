using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.data;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _databaseContext;

        public PortfolioRepository(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _databaseContext.Portfolios.AddAsync(portfolio);
            await _databaseContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(User connectedUser, string symbol)
        {
            var portfolio = await _databaseContext.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.UserId == connectedUser.Id && portfolio.Stock.Symbol.ToLower() == symbol.ToLower());

            if(portfolio == null)
            {
                return null;
            }

            _databaseContext.Portfolios.Remove(portfolio);
            await _databaseContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(User connectedUser)
        {
            return await _databaseContext.Portfolios
            .Where(user => user.UserId == connectedUser.Id)
            .Select(stock => new Stock{
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDividend = stock.Stock.LastDividend,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }

    }
}