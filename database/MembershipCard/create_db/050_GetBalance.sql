--Create procedure for retrieving balance
CREATE PROCEDURE GetBalance
(@card_id varchar(16))
AS
BEGIN
    SET NOCOUNT ON
    Select balance from dbo.card WHERE card_id in (@card_id)
END