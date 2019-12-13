using System.ComponentModel.DataAnnotations;

namespace MembershipCardSystem.TopUp.Model
{
    public class TopUpRequest
    {
        public TopUpRequest(string topUpAmount)
        {
            TopUpAmount = topUpAmount;
        }
        
        [Required]
        [StringLength(10)]
        public string TopUpAmount { get; }
    }
}