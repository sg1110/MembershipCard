--Creates stored procedure for saving new balance
CREATE PROCEDURE SaveNewBalance
(@card_id varchar(16),
 @balance DECIMAL(10,2))
AS
BEGIN
    SET NOCOUNT ON

    UPDATE Card
    SET balance = @balance
    WHERE card_id = @card_id;

END


