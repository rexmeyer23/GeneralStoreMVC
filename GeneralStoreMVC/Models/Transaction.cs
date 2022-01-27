using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreMVC.Models
{
    public class Transaction
    {
        [Key]
        [Display(Name = "Trans ID")]
        public int TransactionID { get; set; }
        [Required]
        [ForeignKey((nameof(Customer)))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        [ForeignKey((nameof(Product)))]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int NumberOfItems { get; set; }
        //public decimal TotalCost
        //{
        //    get
        //    {
        //        return this.Product.Price * this.NumberOfItems;
        //    }
        //}
        public DateTime TransactionDate { get; set; }

    }
}