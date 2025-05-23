﻿/*
Скрипт развертывания для ProjectWeb

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ProjectWeb"
:setvar DefaultFilePrefix "ProjectWeb"
:setvar DefaultDataPath "/var/opt/mssql/data/"
:setvar DefaultLogPath "/var/opt/mssql/data/"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Идет изменение Таблица [dbo].[Products]…';


GO
UPDATE [dbo].[Products]
SET [Description] = ''
WHERE [Description] IS NULL;

UPDATE [dbo].[Products]
SET [ImageUrl] = ''
WHERE [ImageUrl] IS NULL;

ALTER TABLE [dbo].[Products] ALTER COLUMN [Description] NVARCHAR (MAX) NOT NULL;

ALTER TABLE [dbo].[Products] ALTER COLUMN [ImageUrl] NVARCHAR (255) NOT NULL;


GO
PRINT N'Обновление завершено.';


GO
