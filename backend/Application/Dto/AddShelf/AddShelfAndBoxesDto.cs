using Application.Dto.Box;

namespace Application.Dto.AddShelf
{
    public class AddShelfAndBoxesDto
    {
        public AddShelfDto NewShelf { get; set; }
        public Guid WarehouseId { get; set; }
        public IEnumerable<BoxDto> Boxes { get; set; }
    }
}