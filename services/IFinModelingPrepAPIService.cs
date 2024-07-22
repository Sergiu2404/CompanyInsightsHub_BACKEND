using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface IFinModelingPrepAPIService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}