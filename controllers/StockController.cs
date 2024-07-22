using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.data;
using backend.dtos.stock;
using backend.mappers;
using backend.models;
using backend.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _databaseContext;
        private readonly IStockRepository _stockRepository;
        
        public StockController(ApplicationDbContext databaseContext, IStockRepository stockRepository)
        {
            _databaseContext = databaseContext;
            _stockRepository = stockRepository;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject){
            if(!ModelState.IsValid) // ModelState is inhheriting from COntrollerBase
            {
                return BadRequest(ModelState);
            }

            var stocks = await _stockRepository.GetAllAsync(queryObject);
            var stockDTO = stocks.Select(stock => stock.ToStockDTO()); 

            return Ok(stockDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) // ModelState is iinheriting from COntrollerBase
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockRequestDTO)
        {
            if(!ModelState.IsValid) // ModelState is iinheriting from COntrollerBase
            {
                return BadRequest(ModelState);
            }

            var stock = stockRequestDTO.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id}, stock.ToStockDTO());
        }

        // FromRoute - to get the id form the route / URL
        // FromBody - to get the object present in the boyd of the requiest
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            if(!ModelState.IsValid) // ModelState is iinheriting from COntrollerBase
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.UpdateAsync(id, updateDTO);
            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) // ModelState is iinheriting from COntrollerBase
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.DeleteAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}