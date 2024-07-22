using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.repositories
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(User user); 
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(User connectedUser, string symbol);
    }
}