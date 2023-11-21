using System.ComponentModel.DataAnnotations;

namespace Backend295.Model
{
    public class Priority
    {
        [Key]
        public int PriorityID { get; set; }

        [Required]
        public string PriorityType { get; set; }

        [Required]
        public int DaysToCompletion { get; set; }
    }
}
