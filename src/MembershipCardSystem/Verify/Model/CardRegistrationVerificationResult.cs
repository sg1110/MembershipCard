namespace MembershipCardSystem.Verify.Model
{
    public class CardRegistrationStatusResult
    {
        public CardRegistrationStatusResult(string cardId, bool pinPresent)
        {
            CardId = cardId;
            PinPresent = pinPresent;
        }

        public bool PinPresent { get;}

        public string CardId { get; }
    }
}