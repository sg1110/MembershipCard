namespace MembershipCardSystem.DataStore.Model
{
    public class User
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}