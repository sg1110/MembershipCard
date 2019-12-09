USE [master]
GO

PRINT 'Checking for existence of MembershipCard database.';

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='Membership_Card')
    BEGIN
        PRINT 'No Membership_Card database found, creating Membership_Card database.';
        CREATE DATABASE [Membership_Card]
        GOTO Successful;
    END

Successful:
PRINT 'Membership_Card database created successfully';
PRINT 'Done';
