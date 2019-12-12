namespace MembershipCardSystem.DataStore.Model
{
    public class Card
    {
        public Card(string cardId, bool pin)
        {
            CardId = cardId;
            Pin = pin;
        }

        public string CardId { get;  }
        public bool Pin { get;  }
    }
}