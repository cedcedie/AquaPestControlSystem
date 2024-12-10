using AquaPestControlSystem.Models.DBEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AquaPestControlSystem.Models
{
    public class AppointmentViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AppointmentId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Contact Number")]
        public string ContactNum { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Pest Problem")]
        public string PestProblem { get; set; }

        [DisplayName("Schedule")]
        public DateTime Schedule { get; set; }

        
        public string CustomerId { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
    }
}
