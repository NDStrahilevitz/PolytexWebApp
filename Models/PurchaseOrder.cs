using System;
using System.ComponentModel.DataAnnotations;
using PolytexWebApp.CustomAttributes;

namespace PolytexWebApp.Models
{
    public class PurchaseOrder
    {
        public enum Status{
            [Display(Name = "Draft")]
            Draft=1, 
            [Display(Name = "Saved and sent for confirmation")]
            SentForConfirmation=2, 
            [Display(Name = "Confirmed")]
            Confirmed=3
        }
        //primary key
        [Required]
        public ulong poNumber{get;set;}
        [Required]
        public uint poReference{get;set;}
        [Required]
        [StringLength(64, ErrorMessage="Length can't be more than 64")]
        public string companyName{get;set;}
        [Required]
        [StringLength(64, ErrorMessage="Length can't be more than 64")]
        public string contactName{get;set;}
        [Required]
        [EmailAddress]
        [StringLength(64, ErrorMessage="Length can't be more than 64")]
        public string contactEmail{get;set;}
        [Required]
        [Phone]
        [StringLength(64, ErrorMessage="Length can't be more than 64")]
        public string contactPhone{get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime orderDate{get;set;}
        [Required]
        [DataType(DataType.Date)]
        [LargerThanToday]
        public DateTime deliveryDate{get;set;}
        [Required]
        public Status orderStatus{get;set;}
    }
}