using System.Threading.Tasks;
using LosExpress.Models;

namespace LosExpress.Services
{
    public class ExampleUsersService : IExampleUsersService
    {
        public Task<ExampleUser> GetUserByIdAsync(string userId)
        {
            var exampleUser = new ExampleUser
            {
                Name = "Test User",
                Id = userId
            };
            return Task.FromResult(exampleUser);
        }
    }
}
