using Microsoft.AspNetCore.Cors;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPestControlSystem.Models
{
    public class CustomerViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Contact Num")]
        public long ContactNum { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Name")]
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; } 
        }
    }
}
