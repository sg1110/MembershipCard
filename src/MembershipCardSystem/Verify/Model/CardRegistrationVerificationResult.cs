namespace MembershipCardSystem.Verify.Model
{
    public class CardRegistrationVerificationResult
    {
        public CardRegistrationVerificationResult(string cardId, bool pinPresent)
        {
            CardId = cardId;
            PinPresent = pinPresent;
        }

        public bool PinPresent { get;}

        public string CardId { get; }
    }
}