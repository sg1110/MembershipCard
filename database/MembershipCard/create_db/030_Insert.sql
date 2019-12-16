-- Creates stored procedure for inserting customer details

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveCardDetailInformation]
(@employee_id VARCHAR(20),
 @first_name VARCHAR(50),
 @second_name VARCHAR(50),
 @mobile_number VARCHAR(22),
 @card_id VARCHAR(16))
AS
BEGIN
    SET NOCOUNT ON



    INSERT INTO [dbo].[Card]
    (employee_id, first_name, second_name, mobile_number, card_id)
    VALUES
    (@employee_id, @first_name, @second_name, @mobile_number, @card_id )

END
GO
