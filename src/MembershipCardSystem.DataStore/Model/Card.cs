using System.Collections.Generic;

namespace MembershipCardSystem.DataStore.Model
{
    public class Card
    {
        public Card(string cardId, string pin)
        {
            CardId = cardId;
            Pin = pin;
        }

        public string CardId { get;  }
        public string Pin { get;  }
    }
}