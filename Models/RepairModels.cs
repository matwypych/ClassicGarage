using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Repair")]
    public class RepairModels
    {
        public int ID { set; get; }

        [Required]
        public int CarId { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Description { set; get; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Cost")]
        public double Cost { set; get; }

        public virtual CarModels Car { get; set; }
    }
}