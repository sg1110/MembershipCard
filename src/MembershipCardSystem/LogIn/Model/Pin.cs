namespace MembershipCardSystem.LogIn.Model
{
    public class Pin 
    {
        public Pin (string cachedPin)
        {
            CachedPin = cachedPin;
        }

        public string CachedPin { get; set; }
    }
}

