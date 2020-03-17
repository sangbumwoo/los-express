using System.Threading.Tasks;
using LosExpress.Models;

namespace LosExpress.Services
{
    public interface IExampleUsersService
    {
        Task<ExampleUser> GetUserByIdAsync(string userId);
    }
}