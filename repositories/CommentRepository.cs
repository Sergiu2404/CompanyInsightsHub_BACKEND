using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.data;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _databaseContext;

        public CommentRepository(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _databaseContext.Comments.AddAsync(comment);
            await _databaseContext.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _databaseContext.Comments.FirstOrDefaultAsync(comment => comment.Id == id);

            if(comment == null)
            {
                return null;
            }
            _databaseContext.Comments.Remove(comment);
            await _databaseContext.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _databaseContext.Comments.AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(comment => comment.Stock.Symbol == queryObject.Symbol);
            }
            
            if(queryObject.IsDescending == true)
            {
                comments = comments.OrderByDescending(comment => comment.CreatedOn);
            }


            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _databaseContext.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var existingComment = await _databaseContext.Comments.FindAsync(id);
            if(existingComment == null)
            {
                return existingComment;
            }

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;

            await _databaseContext.SaveChangesAsync();

            return existingComment;
        }
    }
}