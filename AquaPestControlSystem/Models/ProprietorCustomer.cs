using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPestControlSystem.Models
{
    public class ProprietorCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }  

        public string Name { get; set; }
        public string TargetPest { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string Town { get; set; }
        public string Status { get; set; }

    }
}
