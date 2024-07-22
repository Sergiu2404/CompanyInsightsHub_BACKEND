using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.data;
using backend.dtos.stock;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _databaseContext; 
        public StockRepository(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;   
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _databaseContext.Stocks.AddAsync(stock);
            await _databaseContext.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _databaseContext.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if(stock == null)
            {
                return null;
            }

            _databaseContext.Stocks.Remove(stock);
            await _databaseContext.SaveChangesAsync();

            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _databaseContext.Stocks.Include(stock => stock.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(queryObject.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(stock => stock.Symbol.Contains(queryObject.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)) //ignoring some special chars
                {
                    stocks = 
                    queryObject.IsDescending ?
                         stocks.OrderByDescending(stock => stock.Symbol) 
                    :
                        stocks.OrderBy(stock => stock.Symbol); 

                    
                }
            }

            // PAGINATION: Skip(n): skips first n elems of the array, Take(n): takes the array without the first n elements
            // first time PageNumber is 1 => skip over 0 elems and take the first PageSize elements
            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _databaseContext.Stocks.Include(stock => stock.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _databaseContext.Stocks.FirstOrDefaultAsync(stock => stock.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _databaseContext.Stocks.AnyAsync(stock => stock.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO)
        {
            var existingStock = await _databaseContext.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if(existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockDTO.Symbol;
            existingStock.CompanyName = stockDTO.CompanyName;
            existingStock.Purchase = stockDTO.Purchase;
            existingStock.LastDividend = stockDTO.LastDividend;
            existingStock.Industry = stockDTO.Industry;
            existingStock.MarketCap = stockDTO.MarketCap;

            await _databaseContext.SaveChangesAsync();
            return existingStock;
        }
    }
}