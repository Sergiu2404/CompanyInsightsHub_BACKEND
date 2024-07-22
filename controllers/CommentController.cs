using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos.comment;
using backend.mappers;
using backend.models;
using backend.repositories;
using backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<User> _userManager;
        private readonly IFinModelingPrepAPIService _finModelingPrepAPIService;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<User> userManager, IFinModelingPrepAPIService finModelingPrepAPIService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _finModelingPrepAPIService = finModelingPrepAPIService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {
            // perfom all the validations existent in the Entities
            if(!ModelState.IsValid) // ModelState is iinheriting from COntrollerBase
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetAllAsync(queryObject);
            var commentDTOs = comments.Select(comment => comment.ToCommentDTO());

            return Ok(commentDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentDTO commentDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            var comment = commentDTO.ToCommentFromCreate(stock.Id);
            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.DeleteAsync(id);
            if(comment == null)
            {
                return NotFound("Comment to delete does not exist");
            }

            return Ok(comment);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDTO updateCommentDTO)
        {
            var comment = await _commentRepository.UpdateAsync(id, updateCommentDTO.ToCommentFromUpdate()); //can not pass the commentDTO => create mapper for this

            if(comment == null)
            {
                NotFound("Comment not found when updating");
            }

            return Ok(comment);
        }
    }
}
