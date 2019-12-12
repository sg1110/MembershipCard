-- DB insert query for inserting test data (emoloyee_id and card_id must be unique)
-- No PIN
INSERT INTO [dbo].[Card]
(employee_id, first_name, second_name, mobile_number, card_id)
VALUES
('ID2', 'TEST','mctest', '1234567890', '1234567890123456' )
GO

--With Pin
INSERT INTO [dbo].[Card]
(employee_id, first_name, second_name, mobile_number, card_id, pin)
VALUES
('ID1', 'TEST','mctest', '1234567890', '1234567890123455', '1234' )
GO

--With pin and money
INSERT INTO [dbo].[testing]
(employee_id, first_name, second_name, mobile_number, card_id, pin, balance)
VALUES
('Iadfaf1', 'TEST','mctest', '1234567890', '2dsf2', '1234' , 1)
GO

-- Create stored procedure for inserting customer details

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
