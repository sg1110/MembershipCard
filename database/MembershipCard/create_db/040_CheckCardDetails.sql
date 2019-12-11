-- Stored procedure for checking card details

CREATE PROCEDURE CheckCardDetails
(@card_id varchar(16))
AS
BEGIN
    SET NOCOUNT ON

    Select * from dbo.Card WHERE card_id in (@card_id)

END
