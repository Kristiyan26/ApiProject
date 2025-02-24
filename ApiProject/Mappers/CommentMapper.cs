using ApiProject.DTOs.Comment;
using ApiProject.Models;

namespace ApiProject.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreateOn = commentModel.CreateOn,
                StockId = commentModel.StockId,
                Username = commentModel.User.UserName


            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {

                Title = commentDto.Title,
                Content = commentDto.Content,
                CreateOn=DateTime.Now,
                StockId= stockId
                
            };
        }
        public static Comment ToCommentFromUpdateDto(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };

        }
    }
}
