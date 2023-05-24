using Domain.Request;
using Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeProject.Interface
{
    public interface ICommentHub
    {
        // Task SendComment(int postId, int? parentId, string message);
        //CommentRequest
        Task SendOffersToUser(CommentResponse data);
    }
}
