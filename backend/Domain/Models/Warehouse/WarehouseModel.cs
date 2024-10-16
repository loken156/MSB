﻿using Domain.Models.Address;
using Domain.Models.Shelf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Warehouse
{
    public class WarehouseModel
    {
        [Key]
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;

        // Address relationship
        [ForeignKey("AddressId")]
        public Guid AddressId { get; set; }
        public virtual AddressModel Address { get; set; }

        // Shelves relationship
        //public virtual ICollection<ShelfModel>? Shelves { get; set; }
    }
}