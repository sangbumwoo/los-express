using System.Threading.Tasks;
using LosExpress.Models;

namespace LosExpress.Services
{
    public interface IPoiService
    {
        Task<Poi> GetPoiByIdAsync(string id);
    }
}