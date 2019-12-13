namespace MembershipCardSystem.TopUp.Model
 {
     public class TopUpResponse
     {
         public TopUpResponse(string topUpAmount, string balance)
         {
             TopUpAmount = topUpAmount;
             Balance = balance;
         }
 
         public string Balance { get; }
 
         public string TopUpAmount { get; }
     }
 }