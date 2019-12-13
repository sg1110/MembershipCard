using System.ComponentModel.DataAnnotations;

namespace MembershipCardSystem.LogIn.Model
{
    public class LogInRequest
    {
        public LogInRequest(string cardId, string cardPin)
        {
            CardId = cardId;
            CardPin = cardPin;
        }

        [Required] 
        [StringLength(16)]
        public string CardId { get;  }

        [Required]
        [StringLength(4)]
        public string CardPin { get;  }
    }
}