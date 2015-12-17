using System;
using System.ComponentModel.DataAnnotations;

namespace CodedHomes.Models
{
    public class Home : IAuditInfo
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public int? Bathrooms { get; set; }
        [Required]
        public int? Bedrooms { get; set; }
        [Required]
        public int? SquareFeet { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
