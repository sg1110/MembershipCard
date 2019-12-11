using System.ComponentModel.DataAnnotations;

namespace MembershipCardSystem.CheckCard.Model
{
    public class PresentCard
    {
        [Required]
        [StringLength(16)]
        public string CardId {get; set;}
        
        [Required]
        [StringLength(4)]
        public string Pin { get; set; }
    }
}