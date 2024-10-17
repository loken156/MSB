/*namespace Application.MappingProfiles
{
    public class DeliveryMapper
    {
        public static Delivery MapOrderToDelivery(OrderEntity order)
        {
            return new Delivery
            {
                DoNumber = order.OrderID,                            // Mapping OrderID to DoNumber
                JobDate = order.DeliveryDate.ToString("yyyy-MM-dd"), // Mapping DeliveryDate
                Address = order.CustomerAddress,                     // Mapping CustomerAddress
                Country = order.CountryCode,                         // Mapping CountryCode to Country
                PostalCode = order.ZipCode,                          // Mapping ZipCode to PostalCode
                CustomerName = order.BuyerName,                      // Mapping BuyerName to CustomerName
                Contact = order.Email,                               // Mapping Email to Contact
                Phone = order.PhoneNumber,                           // Mapping PhoneNumber to Phone
                Zone = order.Region,                                 // Mapping Region to Zone
                Instructions = order.SpecialInstructions             // Mapping SpecialInstructions
            };
        }
    }
}*/