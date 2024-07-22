using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos.stock;
using backend.mappers;
using backend.models;
using Newtonsoft.Json;

namespace backend.services
{
    public class FinModelingPrepAPIService : IFinModelingPrepAPIService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        public FinModelingPrepAPIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try{
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FinModelPrepKey"]}");

                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];

                    if(stock != null)
                    {
                        return stock.ToStockFromFMPStockDTO();
                    }

                    return null;
                }

                return null;
            }catch(Exception exception)
            {
                Console.WriteLine(exception); // create a logging system
                return null;
            }
        }
    }
}