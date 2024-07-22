using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.dtos.stock
{
    public class UpdateStockRequestDTO
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol is too long, it must be of length <= 10")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(255, ErrorMessage = "Company name is too long, it must be of length <= 255")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 1000)]
        public decimal LastDividend { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Industry name is too long, it must be of length <= 255")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000)]
        public long MarketCap { get; set; }
    }
}