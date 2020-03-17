using System.Threading.Tasks;
using LosExpress.Models;

namespace LosExpress.Services
{
    public class PoiService : IPoiService
    {
        public Task<Poi> GetPoiByIdAsync(string id)
        {
            //throw new System.NotImplementedException();
            var poi = new Poi 
            { 
                Name = "test poi name" 
            };
            return Task.FromResult(poi);
        }
    }
}
