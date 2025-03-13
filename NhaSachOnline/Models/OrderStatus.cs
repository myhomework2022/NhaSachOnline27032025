﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaSachOnline.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int ID { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        [MaxLength(20)]
        public string? StatusName { get; set; }
    }
}
