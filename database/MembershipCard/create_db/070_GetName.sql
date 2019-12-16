--Creates stored procedure for getting name
CREATE PROCEDURE GetName
(@card_id varchar(16))
AS
BEGIN
    SET NOCOUNT ON
    Select first_name from dbo.card WHERE card_id in (@card_id)
END