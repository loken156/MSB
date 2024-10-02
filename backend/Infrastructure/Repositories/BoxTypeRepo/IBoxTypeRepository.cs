
using Domain.Models.BoxType;

namespace Infrastructure.Repositories.BoxTypeRepo
{
    public interface IBoxTypeRepository
    {
        Task<BoxTypeModel> AddBoxTypeAsync(BoxTypeModel boxType);
        Task<IEnumerable<BoxTypeModel>> GetAllBoxTypesAsync();
        Task<BoxTypeModel> UpdateBoxTypeAsync(BoxTypeModel box);
        Task DeleteBoxTypeAsync(int BoxTypeId);
        Task<BoxTypeModel> GetBoxTypeByIdAsync(int boxTypeId);
        Task<List<string>> GetAllBoxSizesAsync();
    }
}