namespace MembershipCardSystem.TopUp.Model
 {
     public class TopUpResponse
     {
         public TopUpResponse(int amount, int balance)
         {
             Amount = amount;
             Balance = balance;
         }
 
         public int Balance { get; set; }
 
         public int Amount { get; set; }
     }
 }