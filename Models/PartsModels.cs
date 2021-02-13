using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Parts")]
    public class PartsModels
    {
        public int ID { set; get; }

        [Required]
        public int? CarId { set; get; }

        [Required]
        public string Brand { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public int PartNumber { set; get; }

        public int? Owner { set; get; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Purchase price")]
        public double PurchasePrice { set; get; }

        
        [DataType(DataType.Currency)]
        [Display(Name = "Sell price")]
        public double? SellPrice { set; get; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
              ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase date")]
        public DateTime PurchaseDate { set; get; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
              ApplyFormatInEditMode = true)]
        [Display(Name = "Sale date")]
        public DateTime? SaleDate { set; get; }

        public virtual CarModels Car { get; set; }

    }
}