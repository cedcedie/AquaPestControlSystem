using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPestControlSystem.Models.DBEntities
{
    public class Technician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechnicianId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public long ContactNum { get; set; }

        public string Address { get; set; }

        [Required]
        public string Status {  get; set; }
    }
}
