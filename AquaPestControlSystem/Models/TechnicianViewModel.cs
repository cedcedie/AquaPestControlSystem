using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AquaPestControlSystem.Models
{
    public class TechnicianViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechnicianId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }

        [DisplayName("Contact Number")]
        public long ContactNum { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Name")]
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
    }
}
