using System.ComponentModel.DataAnnotations;

namespace MembershipCardSystem.TopUp.Model
{
    public class TopUpRequest
    {
//        [Required]
//        [StringLength(16)]
//        public string CardId { get; }

        [Required]
        public int TopUpAmount { get; }
    }
}