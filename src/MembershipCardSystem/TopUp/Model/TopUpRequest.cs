using System.ComponentModel.DataAnnotations;

namespace MembershipCardSystem.TopUp.Model
{
    public class TopUpRequest
    {
        [Required]
        [StringLength(16)]
        public string CardId { get; }

        [Required]
        public int Amount { get; }
        
        public string Pin { get; set; }

//        public TopUpRequest(string pin)
//        {
//            Pin = pin;
//        }
    }
}