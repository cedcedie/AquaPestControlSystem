using AquaPestControlSystem.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPestControlSystem.Models.DBEntities
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AppointmentId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string ContactNum { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PestProblem {  get; set; }

        public DateTime? Schedule {  get; set; }

        public string ImageFileName { get; set; }

        public string? CustomerId { get; set; }

        public Customer? customer { get; set; }

        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName;}
        }
    }
}
