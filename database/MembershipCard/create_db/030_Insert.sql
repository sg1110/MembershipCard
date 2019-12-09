INSERT INTO [dbo].[Card]
(employee_id, first_name, mobile_number, card_id)
VALUES
('ID2', 'TEST', '1234567890', (SELECT SUBSTRING(CONVERT(varchar(40), NEWID()),0,17)) )
GO
