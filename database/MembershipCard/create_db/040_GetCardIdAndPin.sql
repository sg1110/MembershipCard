-- Stored procedure for retrieving card pin and card id

    CREATE PROCEDURE GetCardIdAndPin
    (@card_id varchar(16))
    AS
    BEGIN
        SET NOCOUNT ON

        Select pin, card_id from dbo.Card WHERE card_id in (@card_id)

    END
