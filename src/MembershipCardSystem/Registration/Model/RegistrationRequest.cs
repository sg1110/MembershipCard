using System.ComponentModel.DataAnnotations;

namespace MembershipCardSystem.Registration.Model
{
    public class RegistrationRequest
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

        [Required]
        [StringLength(16)]
        public string CardId { get; set; }

        public RegistrationRequest(string employeeId, string firstName, string secondName, string mobileNumber, string cardId)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            SecondName = secondName;
            MobileNumber = mobileNumber;
            CardId = cardId;
        }
    }
    
}