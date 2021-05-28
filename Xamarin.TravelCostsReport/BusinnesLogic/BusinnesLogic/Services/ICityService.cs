using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Dto;
using LiteDB;

namespace BusinnesLogic.Services
{
    public interface ICityService
    {
        Task<BsonValue> InsertAsync(CityDto item);
        Task<int> InsertItemsAsync(IEnumerable<CityDto> items);
        Task<bool> UpdateAsync(CityDto item);
        Task<bool> DeleteAsync(CityDto item);
        Task<CityDto> FindByIdAsync(int id);
        Task<IEnumerable<CityDto>> FindAllAsync();
        Task<int> DeleteAllAsync();
    }
}
