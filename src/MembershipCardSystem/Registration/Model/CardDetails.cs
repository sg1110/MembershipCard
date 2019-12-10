using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MembershipCardSystem.Registration.Model
{
    public class CardDetails
    {
        [Required]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string SecondName { get; set; }
        
        [Required]
        [StringLength(22)]
        public string MobileNumber { get; set; }

    }
    
}