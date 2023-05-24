using Microsoft.AspNetCore.SignalR;

namespace RecipeFoodApiProject.Service
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            //for example just return the user's username
            return connection.User?.Identity?.Name;
        }
    }
}
