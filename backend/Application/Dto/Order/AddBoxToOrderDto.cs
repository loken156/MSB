namespace Application.Dto.Order
{
    public class AddBoxToOrderDto
    {
        public Guid BoxId { get; set; }
        public Guid OrderId { get; set; } //KANSKE SKA BORT JAG E TRÖTT SÅ JAG LÅTER DEN VARA HÄR FÖR NU
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string? UserNotes { get; set; }
        public string Size { get; set; }
    }
}