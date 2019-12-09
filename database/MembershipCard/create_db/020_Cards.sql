USE [Membership_Card]
GO
CREATE TABLE [dbo].[Card](
                             [user_id] [int] IDENTITY(1,1) PRIMARY KEY,
                             [employee_id] [varchar](20) UNIQUE NOT NULL,
                             [first_name] [varchar](50) NOT NULL,
                             [second_name] [varchar](50) NOT NULL,
                             [mobile_number] [varchar](22) NOT NULL,
                             [card_id] [varchar](16) UNIQUE NOT NULL
)
GO


USE [Membership_Card]
GO

