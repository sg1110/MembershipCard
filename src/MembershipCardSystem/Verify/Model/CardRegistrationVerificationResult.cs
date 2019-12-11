namespace MembershipCardSystem.Verify.Model
{
    public class CardRegistrationStatusResult
    {
        public CardRegistrationStatusResult(string cardId)
        {
            CardId = cardId;
        }

        public string CardId { get; }
    }
}