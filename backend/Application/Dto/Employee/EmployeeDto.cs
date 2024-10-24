using Domain.Models.Warehouse;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Employee
{
    public class EmployeeDto
    {

        [Required] public string Email { get; set; } = string.Empty;
        [Required] 
        [DefaultValue("123Abc!")]
        public string Password { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string PhoneNumber { get; set; } = string.Empty;
        [Required] public List<string> Roles { get; set; } = new List<string>();
        [Required] public string Department { get; set; } = string.Empty;
        [Required] public string Position { get; set; } = string.Empty;
        [Required] public DateTime HireDate { get; set; }
        [Required] public Guid WarehouseId { get; set; }
        //public WarehouseModel Warehouse { get; set; }

    }
}