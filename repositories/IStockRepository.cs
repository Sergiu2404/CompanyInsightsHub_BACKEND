using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos.stock;
using backend.models;

namespace backend.repositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id); //need of ? bc firstOrDef can return null also
        Task<Stock?> GetBySymbolAsync(string symbol); 
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}