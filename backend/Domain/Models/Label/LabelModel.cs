namespace Domain.Models.Label
{
    public class LabelModel
    {
        public string LabelName { get; set; } = "Magnificient Store Buddies";
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string QRCode { get; set; }
        public string Barcode { get; set; }
    }
}