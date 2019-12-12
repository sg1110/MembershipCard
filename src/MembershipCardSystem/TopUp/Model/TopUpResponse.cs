namespace MembershipCardSystem.TopUp.Model
 {
     public class TopUpResponse
     {
         public TopUpResponse(int topUpAmount, int balance)
         {
             TopUpAmount = topUpAmount;
             Balance = balance;
         }
 
         public int Balance { get; }
 
         public int TopUpAmount { get; }
     }
 }