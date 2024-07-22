using backend.dtos.comment;
using backend.models;

namespace backend.mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDTO commentDTO, int stockId)
        {
            return new Comment
            {
                Title = commentDTO.Title,
                Content = commentDTO.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDTO updateCommentDTO)
        {
            return new Comment
            {
                Title = updateCommentDTO.Title,
                Content = updateCommentDTO.Content
            };
        }
    }
}
