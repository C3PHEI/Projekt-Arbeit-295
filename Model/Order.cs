using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend295.Model
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        // Fremdschlüssel für Customer
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        public RegistrationUser Customer { get; set; }

        // Fremdschlüssel für Priority
        [ForeignKey("Priority")]
        public int PriorityID { get; set; }

        public Priority Priority { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime PickupDate { get; set; }
    }
}
