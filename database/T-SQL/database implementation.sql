
				-- T-SQL --
		 -- START DATE < 2024/6/25 > --
	   -- BY Murtadha Muhammed Muhammed --


USE DebtManagementSystemDB

------------------ START COMPANIES TABLE --------------------

CREATE FUNCTION COMPANIES_FUN_IsCompanyCodeExist(@companyCode nvarchar(14))
	RETURNS INT
	AS
BEGIN

	DECLARE @isExist INT = 0;
	SELECT @isExist = 1 FROM Companies WHERE CompanyCode = @companyCode;
	RETURN @isExist;

END

 --COMPLETED

CREATE FUNCTION COMPANIES_FUN_IsCompanyCodeExistWithOutCurrentCompany(@companyId int, @companyCode nvarchar(14))
	RETURNS INT
	AS
BEGIN

	DECLARE @isExist INT = 0;
	SELECT @isExist = 1 FROM Companies WHERE CompanyCode = @companyCode AND CompanyId != @companyId;
	RETURN @isExist;

END

 --COMPLETED

CREATE FUNCTION COMPANIES_FUN_IsCompanyPhoneExist(@companyPhone nvarchar(14))
	RETURNS INT
	AS
BEGIN

	DECLARE @isExist INT = 0;
	SELECT @isExist = 1 FROM Companies WHERE Phone1 = @companyPhone OR Phone2 = @companyPhone;
	RETURN @isExist;

END

--COMPLETED


CREATE FUNCTION COMPANIES_FUN_IsCompanyPhoneExistWithOutCurrentCompany(@companyId int, @companyPhone nvarchar(14))
	RETURNS INT
	AS
BEGIN

	DECLARE @isExist INT = 0;
	SELECT @isExist = 1 FROM Companies WHERE Phone1 = @companyPhone AND CompanyId != @companyId;
	RETURN @isExist;

END

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompanySubscriptionRemainingDays(@endDate date)
	RETURNS INT
	AS
BEGIN

	DECLARE @diff INT = 0;
	SET @diff = DATEDIFF(DAY, GETDATE(), @endDate);
	
	IF (@diff < 0)
		SET @diff = 0;

	RETURN @diff;

END

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompanies()
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,
		
		CASE
			WHEN SubscriptionStatus = 1 THEN 'Active'
			ELSE 'Inactive'
		END AS SubscriptionStatus,
		
		SubscriptionStartDate,
		SubscriptionEndDate,
		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate)) 
			AS RemainingSubscriptionDays,
		
		Description,

		CASE
			WHEN IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		ByAdmin,
		Action
	
	FROM Companies c
  
);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompaniesByName(@companyName nvarchar(50))
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,
	
		CASE
			WHEN SubscriptionStatus = 1 THEN 'Active'
			ELSE 'Inactive'
		END AS SubscriptionStatus,
	
		SubscriptionStartDate,
		SubscriptionEndDate,
		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate)) 
			AS RemainingSubscriptionDays,
	
		Description,

		CASE
			WHEN IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		ByAdmin,
		Action

	FROM Companies c
	WHERE CompanyName = @companyName

);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompanyByCode(@companyCode nvarchar(14))
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,

		CASE
			WHEN SubscriptionStatus = 1 THEN 'Active'
			ELSE 'Inactive'
		END AS SubscriptionStatus,

		SubscriptionStartDate,
		SubscriptionEndDate,
		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate)) 
			AS RemainingSubscriptionDays,

		Description,

		CASE
			WHEN IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		ByAdmin,
		Action

	FROM Companies c
	WHERE CompanyCode = @companyCode

);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompanyById(@companyId int)
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,
		SubscriptionStatus,
		SubscriptionStartDate,
		SubscriptionEndDate,

		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate)) 
			AS RemainingSubscriptionDays,

		Description,
		IsPaid,
		ByAdmin,
		Action

	FROM Companies c
	WHERE CompanyId = @companyId

);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetActiveCompanies()
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,

		CASE
			WHEN SubscriptionStatus = 1 THEN 'Active'
			ELSE 'Inactive'
		END AS SubscriptionStatus,

		SubscriptionStartDate,
		SubscriptionEndDate,
		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate)) 
			AS RemainingSubscriptionDays,

		Description,

		CASE
			WHEN IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		ByAdmin,
		Action

	FROM Companies c
	WHERE SubscriptionStatus = 1

);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetInActiveCompanies()
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,

		CASE
			WHEN SubscriptionStatus = 1 THEN 'Active'
			ELSE 'Inactive'
		END AS SubscriptionStatus,

		SubscriptionStartDate,
		SubscriptionEndDate,
		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate)) 
			AS RemainingSubscriptionDays,

		Description,

		CASE
			WHEN IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		ByAdmin,
		Action

	FROM Companies c
	WHERE SubscriptionStatus = 0

);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompaniesByAdmin(@adminId int)
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
		CompanyId,
		ManagerFullName,
		CompanyName,
		CompanyCode,
		CompanyImage,
		Phone1,
		Phone2,
		Address,
		SubscriptionFee,
		Currency,
		RegistrationDate,

		CASE
			WHEN SubscriptionStatus = 1 THEN 'Active'
			ELSE 'Inactive'
		END AS SubscriptionStatus,

		SubscriptionStartDate,
		SubscriptionEndDate,
		([dbo].[COMPANIES_FUN_GetCompanySubscriptionRemainingDays] (c.SubscriptionEndDate))
			AS RemainingSubscriptionDays,

		Description,

		CASE
			WHEN IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		ByAdmin,
		Action

	FROM Companies c
	WHERE ByAdmin = @adminId

);

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_IsSubscriptionActive (@companyId INT)
	RETURNS BIT
	AS
BEGIN

	DECLARE @isActive BIT = 0;
	SELECT @isActive = SubscriptionStatus FROM Companies WHERE CompanyId = @companyId;
	
	RETURN @isActive;

END

CREATE PROCEDURE COMPANIES_SP_SetCompanyAsActive
	@companyId int,
	@adminId int
	AS
BEGIN
	
	UPDATE Companies
	SET SubscriptionStatus = 1,
	ByAdmin = @adminId,
	Action = 'Change SubscriptionStatus'
	WHERE CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE COMPANIES_SP_SetCompanyAsInActive
	@companyId int,
	@adminId int
	AS
BEGIN
	
	UPDATE Companies
	SET SubscriptionStatus = 0,
	ByAdmin = @adminId,
	Action = 'Change SubscriptionStatus'
	WHERE CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompaniesCount()
	RETURNS INT
	AS
BEGIN
	
	DECLARE @count INT = 0;
	SELECT @count = COUNT(CompanyId) FROM Companies;
	RETURN @count;

END

 --COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetActiveCompaniesCount()
	RETURNS INT
	AS
BEGIN
	
	DECLARE @count INT = 0;
	SELECT @count = COUNT(CompanyId) FROM Companies WHERE SubscriptionStatus = 1;
	RETURN @count;

END

 --COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetInActiveCompaniesCount()
	RETURNS INT
	AS
BEGIN
	
	DECLARE @count INT = 0;
	SELECT @count = COUNT(CompanyId) FROM Companies WHERE SubscriptionStatus = 0;
	RETURN @count;

END

 --COMPLETED

CREATE FUNCTION COMPANIES_FUN_GetCompaniesCountByAdmin(@adminId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @count INT = 0;
	SELECT @count = COUNT(CompanyId) FROM Companies WHERE ByAdmin = @adminId;
	RETURN @count;

END

--COMPLETED

CREATE PROCEDURE COMPANIES_SP_SetPaidStatusAsPaid
	@companyId INT,
	@adminId INT
	AS
BEGIN
	
	UPDATE Companies
	SET IsPaid = 1,
	ByAdmin = @adminId,
	Action = 'Change IsPaid'
	WHERE CompanyId = @companyId;
	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE COMPANIES_SP_SetPaidStatusAsUnPaid
	@companyId INT,
	@adminId INT
	AS
BEGIN
	
	UPDATE Companies
	SET IsPaid = 0,
	ByAdmin = @adminId,
	Action = 'Change IsPaid'
	WHERE CompanyId = @companyId;
	RETURN @@ROWCOUNT;
	
END

--COMPLETED

CREATE PROCEDURE COMPANIES_SP_NewCompany
	@managerFullName nvarchar(50),
	@companyName nvarchar(50),
	@companyCode nvarchar(14),
	@companyImage varbinary(max) NULL,
	@phone1 nvarchar(14),
	@phone2 nvarchar(14) NULL,
	@address nvarchar(150),
	@subscriptionFee money,
	@currency nvarchar(14),
	@subscriptionStatus bit,
	@subscriptionStartDate date,
	@subscriptionEndDate date,
	@description nvarchar(250) NULL,
	@isPaid bit,
	@byAdmin int
	AS
BEGIN

	DECLARE @isCodeExist BIT = 0;
	SELECT @isCodeExist = [dbo].[COMPANIES_FUN_IsCompanyCodeExist] (@companyCode);
	IF (@isCodeExist = 1)
		RETURN 0

	DECLARE @isPhoneExist BIT = 0;
	SELECT @isPhoneExist = [dbo].[COMPANIES_FUN_IsCompanyPhoneExist] (@phone1);
	IF (@isPhoneExist = 1)
		RETURN 0;
	
	SELECT @isPhoneExist = [dbo].[COMPANIES_FUN_IsCompanyPhoneExist] (@phone2);
	IF (@isPhoneExist = 1)
	    RETURN 0;

	IF (@phone1 = @phone2)
		RETURN 0;

	INSERT INTO Companies
	(
		ManagerFullName, CompanyName, CompanyCode, CompanyImage, Phone1, Phone2,
		Address, SubscriptionFee, Currency, RegistrationDate, SubscriptionStatus, SubscriptionStartDate, 
		SubscriptionEndDate, Description, IsPaid, ByAdmin, Action
	)
	VALUES
	(
		@managerFullName, @companyName, @companyCode, @companyImage, @phone1, @phone2,
		@address, @subscriptionFee, @currency, GETDATE(), @subscriptionStatus, @subscriptionStartDate,
		@subscriptionEndDate, @description, @isPaid, @byAdmin, 'New Record'
	);

	RETURN SCOPE_IDENTITY();

END

--COMPLETED

CREATE PROCEDURE COMPANIES_SP_UpdateCompany
	@companyId int,
	@managerFullName nvarchar(50),
	@companyName nvarchar(50),
	@companyImage varbinary(max) NULL,
	@phone1 nvarchar(14),
	@phone2 nvarchar(14) NULL,
	@address nvarchar(150),
	@subscriptionFee money,
	@currency nvarchar(14),
	@subscriptionStatus bit,
	@subscriptionStartDate date,
	@subscriptionEndDate date,
	@description nvarchar(250) NULL,
	@isPaid bit,
	@byAdmin int
	AS
BEGIN

	DECLARE @isPhoneExist BIT = 0;
	SELECT @isPhoneExist = [dbo].[COMPANIES_FUN_IsCompanyPhoneExistWithOutCurrentCompany] (@companyId, @phone1);
	IF (@isPhoneExist = 1)
		RETURN 0;
	
	SELECT @isPhoneExist = [dbo].[COMPANIES_FUN_IsCompanyPhoneExistWithOutCurrentCompany] (@companyId, @phone2);
	IF (@isPhoneExist = 1)
	    RETURN 0;

	IF (@phone1 = @phone2)
		RETURN 0;
	
	UPDATE Companies
	SET ManagerFullName = @managerFullName,
	CompanyName = @companyName,
	CompanyImage = @companyImage,
	Phone1 = @phone1,
	Phone2 = @phone2,
	Address = @address,
	SubscriptionFee = @subscriptionFee,
	Currency = @currency,
	SubscriptionStatus = @subscriptionStatus,
	SubscriptionStartDate = @subscriptionStartDate,
	SubscriptionEndDate = @subscriptionEndDate,
	Description = @description,
	IsPaid = @isPaid,
	ByAdmin = @byAdmin,
	Action = 'Update Record'
	WHERE CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED     

CREATE PROCEDURE COMPANIES_SP_DeleteCompany
	@companyId INT
	AS
BEGIN

	DECLARE @rowsAffected INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY

		DELETE FROM Settings WHERE CompanyId = @companyId;
		DELETE FROM PaidRecords WHERE CompanyId = @companyId;
		DELETE FROM DebtRecords_Products WHERE CompanyId = @companyId;
		DELETE FROM DebtRecords WHERE CompanyId = @companyId;
		DELETE FROM Products WHERE CompanyId = @companyId;
		DELETE FROM SalesUnits WHERE CompanyId = @companyId;
		DELETE FROM Categories WHERE CompanyId = @companyId;
		
		DELETE FROM Customers FROM People INNER JOIN Customers ON
		People.PersonId = Customers.PersonId AND People.CompanyId = @companyId;
		
		DELETE FROM Users FROM People INNER JOIN Users ON
		People.PersonId = Users.PersonId AND People.CompanyId = @companyId;
		
		DELETE FROM People WHERE CompanyId = @companyId;
		DELETE FROM Companies WHERE CompanyId = @companyId;
		
		SELECT @rowsAffected = @@ROWCOUNT;		
		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
		RETURN -1;
	END CATCH

		RETURN @rowsAffected;

END

--COMPLETED

CREATE TRIGGER COMPANIES_TRG_AfterInsertNewCompany ON Companies
	AFTER INSERT
	AS
BEGIN

	DECLARE @newUserId INT = 0;
    DECLARE @fullName NVARCHAR(50);
    DECLARE @phone1 NVARCHAR(14);
    DECLARE @phone2 NVARCHAR(14);
    DECLARE @companyId INT;
    DECLARE @userName NVARCHAR(14);
    DECLARE @password NVARCHAR(100) = 'admin';
    DECLARE @permissions BIGINT = -2;
    DECLARE @newSettingId INT;
    DECLARE @companyName NVARCHAR(50);
    DECLARE @currency NVARCHAR(14);

    -- احصل على القيم من الصف المدخل
    SELECT 
        @fullName = ManagerFullName,
        @phone1 = Phone1,
        @phone2 = Phone2,
        @companyId = CompanyId,
        @userName = CompanyCode,
        @companyName = CompanyName,
		@currency = Currency
    FROM INSERTED;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		DECLARE @personId int = 0;
		EXECUTE @personId = [dbo].[PEOPLE_SP_NewPerson] 
			@fullName,
			@phone1,
			@phone2,
			NULL,
			@companyId,
			NULL;

		IF (@personId <= 0)
			THROW 50001, 'ERROR', 1;

		EXECUTE @newSettingId = [dbo].[SETTINGS_SP_NewSettings] 
			@companyName, @currency, @companyId

		INSERT INTO Users (UserName, Password, Permissions, Image, PersonId, IsActive, IsDeleted)
		VALUES
		(
			@userName,
			@password,
			@permissions,
			NULL,
			@personId,
			1,
			0
		);

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

END

--COMPLETED

------------------ END COMPANIES TABLE --------------------

------------------ START SETTINGS TABLE --------------------


CREATE PROCEDURE SETTINGS_SP_NewSettings
	@companyName nvarchar(50),
	@currency nvarchar(14),
	@companyId int
	AS
BEGIN

	DECLARE @isCompanyIdExist BIT = 0;
	SELECT @isCompanyIdExist = 1 FROM Settings WHERE CompanyId = @companyId;
	IF (@isCompanyIdExist = 1)
		RETURN 0;

	INSERT INTO Settings(CompanyName, Currency, CompanyId)
	VALUES 
	(
		@companyName,
		@currency,
		@companyId
	);

	RETURN SCOPE_IDENTITY();

END

--COMPLETED

CREATE PROCEDURE SETTINGS_SP_UpdateSettings
	@companyName nvarchar(50),
	@description nvarchar(max),
	@logo varbinary(max),
	@currency nvarchar(14),
	@paymentRequestMessage nvarchar(250),
	@companyId int
	AS
BEGIN

	UPDATE Settings
	SET CompanyName = @companyName,
	Description = @description,
	Logo = @logo,
	Currency = @currency,
	PaymentRequestMessage = @paymentRequestMessage
	WHERE CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE SETTINGS_SP_DeleteSettings
	@companyId int
	AS
BEGIN

	DELETE FROM Settings WHERE CompanyId = @companyId;
	RETURN @@ROWCOUNT;
	
END

--COMPLETED

CREATE FUNCTION SETTINGS_FUN_GetSettings (@companyId int)
	RETURNS TABLE
	AS
RETURN
(
	SELECT * FROM Settings WHERE CompanyId = @companyId
)

--COMPLETED


------------------ END SETTINGS TABLE --------------------

------------------ START PEOPLE TABLE --------------------

CREATE FUNCTION PEOPLE_FUN_IsPhoneExist(@phone nvarchar(14), @companyId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @_isExist bit = 0;
	SELECT @_isExist = 1 FROM People WHERE (Phone1 = @phone OR Phone2 = @phone) AND CompanyId = @companyId;

	RETURN @_isExist;

END

 --COMPLETED

CREATE FUNCTION PEOPLE_FUN_IsPhoneExistWithOutCurrentPerson(@personId int, @phone nvarchar(14), @companyId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @_isExist bit = 0;
	SELECT @_isExist = 1 FROM People WHERE (Phone1 = @phone OR Phone2 = @phone) AND CompanyId = @companyId AND PersonId != @personId;
	RETURN @_isExist

END

 --COMPLETED

CREATE PROCEDURE PEOPLE_SP_NewPerson
	@fullName NVARCHAR(50),
	@phone1 NVARCHAR(14),
	@phone2 NVARCHAR(14),
	@telegramId NVARCHAR(25) NULL,
	@companyId INT,
	@byUser INT
	AS
BEGIN

	DECLARE @isPhoneExist bit = 0;
	SELECT @isPhoneExist = [dbo].[PEOPLE_FUN_IsPhoneExist] (@phone1, @companyId);

	IF (@isPhoneExist = 1)
		RETURN 0;

	SELECT @isPhoneExist = [dbo].[PEOPLE_FUN_IsPhoneExist] (@phone2, @companyId);

	IF (@isPhoneExist = 1)
		RETURN 0;

	IF (@phone1 = @phone2)
		RETURN 0;

	INSERT INTO People (FullName, Phone1, Phone2, TelegramId, CompanyId, ByUser)
	VALUES
	(
		@fullName, @phone1, @phone2, @telegramId, @companyId, @byUser
	);

	RETURN SCOPE_IDENTITY();

END

 --COMPLETED

CREATE PROCEDURE PEOPLE_SP_DeletePerson
	@personId INT,
	@companyId INT
	AS
BEGIN

	DELETE FROM People WHERE PersonId = @personId AND CompanyId = @companyId;
	RETURN @@ROWCOUNT;

END

 --COMPLETED

CREATE PROCEDURE PEOPLE_SP_UpdatePerson
	@personId INT,
	@fullName NVARCHAR(50),
	@phone1 NVARCHAR(14),
	@phone2 NVARCHAR(14),
	@telegramId NVARCHAR(25),
	@companyId INT,
	@byUser INT
	AS
BEGIN

	DECLARE @isPhoneExist bit = 0;

	SELECT @isPhoneExist = [dbo].[PEOPLE_FUN_IsPhoneExistWithOutCurrentPerson] (@personId, @phone1, @companyId);
	IF (@isPhoneExist = 1)
		RETURN 0

	SELECT @isPhoneExist = [dbo].[PEOPLE_FUN_IsPhoneExistWithOutCurrentPerson] (@personId, @phone2, @companyId);
	IF (@isPhoneExist = 1)
		RETURN 0

	IF (@phone1 = @phone2)
		RETURN 0;

	UPDATE People
	SET FullName = @fullName,
		Phone1 = @phone1,
		Phone2 = @phone2,
		TelegramID = @telegramId,
		ByUser = @byUser
	WHERE PersonId = @personId AND CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED

------------------ END PEOPLE TABLE --------------------

------------------ START USERS TABLE --------------------

CREATE FUNCTION USERS_FUN_GetUserByLoginInfo (@userName nvarchar(14), @password nvarchar(100))
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
	Users.UserId,
	People.FullName,
	Users.UserName,
	Users.Password,
	People.Phone1,
	People.Phone2,
	People.TelegramID,
	Users.Permissions,
	Users.Image,
	IsActive,
	ByUser,
	People.CompanyId

	FROM Users INNER JOIN
	People ON
	Users.PersonId = People.PersonId AND Users.IsDeleted = 0 AND UserName = @userName AND Password = @password

)

CREATE FUNCTION USERS_FUN_GetUserById (@userId int, @companyId int)
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
	Users.UserId,
	People.FullName,
	Users.UserName,
	People.Phone1,
	People.Phone2,
	People.TelegramID,
	Users.Permissions,
	Users.Image,
	IsActive,
	ByUser,
	CompanyId

	FROM Users INNER JOIN
	People ON
	Users.PersonId = People.PersonId AND People.CompanyId = @companyId AND Users.IsDeleted = 0 AND UserId = @userId

)

--COMPLETED

CREATE FUNCTION USERS_FUN_GetUsers (@companyId int)
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
	Users.UserId,
	People.FullName,
	Users.UserName,
	People.Phone1,
	People.Phone2,
	People.TelegramID,
	Users.Permissions,
	Users.Image,
	
	CASE
		WHEN Users.IsActive = 1 THEN 'Active'
		ELSE 'InActive'
	END AS IsActive,

	(
		SELECT Users.UserName FROM Users WHERE People.ByUser = Users.UserId
	) AS ByUser

	FROM Users INNER JOIN
	People ON
	Users.PersonId = People.PersonId AND People.CompanyId = @companyId AND Users.IsDeleted = 0

)

--COMPLETED

CREATE FUNCTION USERS_FUN_GetActiveUsers (@companyId int)
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
	Users.UserId,
	People.FullName,
	Users.UserName,
	People.Phone1,
	People.Phone2,
	People.TelegramID,
	Users.Permissions,
	Users.Image,
	
	CASE
		WHEN Users.IsActive = 1 THEN 'Active'
		ELSE 'InActive'
	END AS IsActive,
	(
		SELECT Users.UserName FROM Users WHERE People.ByUser = Users.UserId
	) AS ByUser

	FROM Users INNER JOIN
	People ON
	Users.PersonId = People.PersonId AND People.CompanyId = @companyId AND Users.IsDeleted = 0 AND Users.IsActive = 1

)

--COMPLETED

CREATE FUNCTION USERS_FUN_GetInActiveUsers (@companyId int)
	RETURNS TABLE
	AS
RETURN 
(

	SELECT 
	Users.UserId,
	People.FullName,
	Users.UserName,
	People.Phone1,
	People.Phone2,
	People.TelegramID,
	Users.Permissions,
	Users.Image,
	
	CASE
		WHEN Users.IsActive = 1 THEN 'Active'
		ELSE 'InActive'
	END AS IsActive,
	(
		SELECT Users.UserName FROM Users WHERE People.ByUser = Users.UserId
	) AS ByUser

	FROM Users INNER JOIN
	People ON
	Users.PersonId = People.PersonId AND People.CompanyId = @companyId AND Users.IsDeleted = 0 AND Users.IsActive = 0

)

--COMPLETED

CREATE FUNCTION USERS_FUN_IsUserNameExist(@userName nvarchar(14), @companyId int)
	RETURNS BIT
	AS
BEGIN

	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM Users INNER JOIN People ON 
		Users.PersonId = People.PersonId AND Users.UserName = @userName AND People.CompanyId = @companyId;

	RETURN @isExist;

END

--COMPLETED

CREATE FUNCTION USERS_FUN_IsUserNameExistWithOutCurrentUser(@userId int, @userName nvarchar(14), @companyId int)
	RETURNS BIT
	AS
BEGIN

	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM Users INNER JOIN People ON 
		Users.PersonId = People.PersonId AND Users.UserName = @userName AND People.CompanyId = @companyId AND Users.UserId != @userId;

	RETURN @isExist;

END

--COMPLETED

CREATE FUNCTION USERS_FUN_GetUsersCount(@companyId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @count int = 0;
	SELECT @count = COUNT(Users.UserId) FROM Users INNER JOIN People ON
	Users.PersonId = People.PersonId AND People.CompanyId = @companyId AND Users.IsDeleted = 0
	RETURN @count;

END

--COMPLETED

CREATE PROCEDURE USERS_SP_SetUserAsActive
	@companyId int,
	@userId int
	AS
BEGIN
	
	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	UPDATE Users
	SET IsActive = 1
	FROM Users WHERE Users.UserId = @userId AND 
	(
		SELECT CompanyId FROM People INNER JOIN Users ON People.PersonId = Users.PersonId AND Users.UserId = @userId
	)
	= @companyId;
	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE USERS_SP_SetUserAsInActive
	@companyId int,
	@userId int
	AS
BEGIN
	
	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	UPDATE Users
	SET IsActive = 0
	FROM Users WHERE Users.UserId = @userId AND 
	(
		SELECT CompanyId FROM People INNER JOIN Users ON People.PersonId = Users.PersonId AND Users.UserId = @userId
	)
	= @companyId;
	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE USERS_SP_NewUser
	@fullName nvarchar(50),
	@phone1 nvarchar(14),
	@phone2 nvarchar(14),
	@telegramId nvarchar(25),
	@companyId int,
	@byUser int,
	@userName nvarchar(14),
	@password nvarchar(100),
	@permissions bigint,
	@image varbinary(max)
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @userPermissions INT = 0;
	SELECT @userPermissions = Permissions FROM Users INNER JOIN People ON
	People.PersonId = Users.PersonId AND Users.UserId = @byUser AND People.CompanyId = @companyId;

	IF (@userPermissions >= 0)
		SELECT @permissions = @userPermissions & @permissions;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		DECLARE @personId int = 0;
		EXECUTE @personId = [dbo].[PEOPLE_SP_NewPerson] 
			@fullName,
			@phone1,
			@phone2,
			@telegramId,
			@companyId,
			@byUser;

		IF (@personId <= 0)
			THROW 50001, 'ERROR', 1;

		DECLARE @isUserNameExist BIT = 0;
		SELECT @isUserNameExist = [dbo].[USERS_FUN_IsUserNameExist] (@userName, @companyId);

		IF (@isUserNameExist = 1)
			THROW 50001, 'ERROR', 2;

		INSERT INTO Users (UserName, Password, Permissions, Image, PersonId, IsActive, IsDeleted)
		VALUES
		(
			@userName,
			@password,
			@permissions,
			@image,
			@personId,
			1,
			0
		);

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

	RETURN SCOPE_IDENTITY()

END

--COMPLETED

CREATE PROCEDURE USERS_SP_UpdateUser
	@userId int,
	@fullName nvarchar(50),
	@phone1 nvarchar(14),
	@phone2 nvarchar(14),
	@telegramId nvarchar(25),
	@companyId int,
	@byUser int,
	@userName nvarchar(14),
	@permissions bigint,
	@image varbinary(max),
	@isActive bit
	AS
BEGIN
	
	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @userPermissions INT = 0;
	SELECT @userPermissions = Permissions FROM Users INNER JOIN People ON
	People.PersonId = Users.PersonId AND Users.UserId = @byUser AND People.CompanyId = @companyId;

	IF (@userPermissions >= 0)
		SELECT @permissions = @userPermissions & @permissions;

	DECLARE @affectedRows INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		DECLARE @personId int = 0;
		SELECT @personId = PersonId FROM Users WHERE Users.UserId = @userId;
		
		DECLARE @rowsAffected int = 0;
		EXECUTE @rowsAffected = [dbo].[PEOPLE_SP_UpdatePerson] 
			@personId,
			@fullName,
			@phone1,
			@phone2,
			@telegramId,
			@companyId,
			@byUser

		IF (@rowsAffected <= 0)
			THROW 50001, 'ERROR', 3;

		DECLARE @isUserNameExist BIT = 0;
		SELECT @isUserNameExist = [dbo].[USERS_FUN_IsUserNameExistWithOutCurrentUser] (@userId, @userName, @companyId);

		IF (@isUserNameExist = 1)
			THROW 50001, 'ERROR', 4;

		UPDATE Users
		SET UserName = @userName,
		Permissions = @permissions,
		Image = @image,
		IsActive = @isActive
		WHERE UserId = @userId;

		SET @affectedRows = @@ROWCOUNT;

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

	RETURN @affectedRows;

END

--COMPLETED

CREATE PROCEDURE USERS_SP_UpdateCurrentUser
	@userId int,
	@fullName nvarchar(50),
	@phone1 nvarchar(14),
	@phone2 nvarchar(14),
	@telegramId nvarchar(25),
	@companyId int,
	@byUser int,
	@userName nvarchar(14),
	@password nvarchar(100),
	@image varbinary(max),
	@isActive bit
	AS
BEGIN
	
	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;


	DECLARE @affectedRows INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		DECLARE @personId int = 0;
		SELECT @personId = PersonId FROM Users WHERE Users.UserId = @userId;
		
		DECLARE @rowsAffected int = 0;
		EXECUTE @rowsAffected = [dbo].[PEOPLE_SP_UpdatePerson] 
			@personId,
			@fullName,
			@phone1,
			@phone2,
			@telegramId,
			@companyId,
			@byUser

		IF (@rowsAffected <= 0)
			THROW 50001, 'ERROR', 3;

		DECLARE @isUserNameExist BIT = 0;
		SELECT @isUserNameExist = [dbo].[USERS_FUN_IsUserNameExistWithOutCurrentUser] (@userId, @userName, @companyId);

		IF (@isUserNameExist = 1)
			THROW 50001, 'ERROR', 4;
			
		UPDATE Users
		SET UserName = @userName,
		Password = @password,
		Image = @image,
		IsActive = @isActive
		WHERE UserId = @userId AND
		(
			SELECT CompanyId FROM People INNER JOIN Users ON People.PersonId = Users.PersonId AND People.CompanyId = @companyId AND
			Users.UserId = @userId
		) = @companyId

		SET @affectedRows = @@ROWCOUNT;

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

	RETURN @affectedRows;

END

--COMPLETED

CREATE PROCEDURE USERS_SP_DeleteUser
	@userId int,
	@companyId int,
	@byUser int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @userPermissions INT = 0;
	SELECT @userPermissions = Permissions FROM Users INNER JOIN People ON
	People.PersonId = Users.PersonId AND Users.UserId = @byUser AND People.CompanyId = @companyId;

	DECLARE @deletedUserPermissions INT = 0;
	SELECT @deletedUserPermissions = Permissions FROM Users INNER JOIN People ON
	People.PersonId = Users.PersonId AND Users.UserId = @userId AND People.CompanyId = @companyId;

	IF (@deletedUserPermissions = -2)
		RETURN 0;

	IF (@userPermissions = @deletedUserPermissions)
		RETURN 0;

	IF (@userPermissions >= 0 AND @deletedUserPermissions = -1)
		RETURN 0;

	DECLARE @rowsAffected INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		UPDATE Users
		SET IsDeleted = 1
		WHERE UserId = @userId AND 
		(
			SELECT CompanyId FROM People INNER JOIN Users ON
			People.PersonId = Users.PersonId AND Users.UserId = @userId
		)
		= @companyId;

		SET @rowsAffected = @@ROWCOUNT;

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

	RETURN @rowsAffected;

END

--COMPLETED

------------------ END USERS TABLE --------------------

------------------ START CATEGORIES TABLE --------------------

CREATE FUNCTION CATEGORIES_FUN_GetCategories (@companyId int)
	RETURNS TABLE
	AS
RETURN
(

	SELECT 
		CategoryId,
		CategoryName,
		CategoryImage,
		(
			SELECT Users.UserName FROM Users WHERE c.ByUser = Users.UserId
		) AS ByUser,
		(
			SELECT COUNT(Products.ProductId) FROM Products WHERE Products.CategoryId = c.CategoryId AND Products.CompanyId = @companyId
		) AS Items
	FROM Categories c WHERE CompanyId = @companyId

)

--COMPLETED

CREATE FUNCTION CATEGORIES_FUN_GetCategoryById (@categoryId int, @companyId int)
	RETURNS TABLE
	AS
RETURN
(

	SELECT 
		CategoryId,
		CategoryName,
		CategoryImage,
		ByUser,
		CompanyId
	FROM Categories c WHERE CompanyId = @companyId AND CategoryId = @categoryId

)

--COMPLETED

CREATE FUNCTION CATEGORIES_FUN_IsCategoryExist(@categoryName nvarchar(50), @companyId int)
	RETURNS BIT
	AS
BEGIN
	
	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM Categories WHERE CategoryName = @categoryName AND CompanyId = @companyId;
	RETURN @isExist;

END

--COMPLETED

CREATE FUNCTION CATEGORIES_FUN_IsCategoryExistWithOutCurrentCategory(@categoryId int, @categoryName nvarchar(50), @companyId int)
	RETURNS BIT
	AS
BEGIN

	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM Categories WHERE CategoryName = @categoryName AND CompanyId = @companyId AND CategoryId != @categoryId;
	RETURN @isExist;

END

--COMPLETED

CREATE PROCEDURE CATEGORIES_SP_NewCategory
	@categoryName nvarchar(50),
	@categoryImage varbinary(max),
	@companyId int,
	@byUser int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isCategoryExist BIT = 0;
	SELECT @isCategoryExist = [dbo].[CATEGORIES_FUN_IsCategoryExist] (@categoryName, @companyId);

	IF (@isCategoryExist = 1)
		RETURN 0;

	INSERT INTO Categories (CategoryName, CategoryImage, ByUser, CompanyId)
	VALUES
	(
		@categoryName,
		@categoryImage,
		@byUser,
		@companyId
	);

	RETURN SCOPE_IDENTITY();

END

--COMPLETED

CREATE PROCEDURE CATEGORIES_SP_UpdateCategory
	@categoryId int,
	@categoryName nvarchar(50),
	@categoryImage varbinary(max),
	@companyId int,
	@byUser int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isCategoryExist BIT = 0;
	SELECT @isCategoryExist = [dbo].[CATEGORIES_FUN_IsCategoryExistWithOutCurrentCategory] (@categoryId, @categoryName, @companyId);

	IF (@isCategoryExist = 1)
		RETURN 0;

	UPDATE Categories
	SET 
		CategoryName = @categoryName,
		CategoryImage = @categoryImage,
		ByUser = @byUser
	WHERE CategoryId = @categoryId AND CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE FUNCTION CATEGORIES_FUN_GetCategoriesCount(@companyId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @count INT = 0;
	SELECT @count = COUNT(CategoryId) FROM Categories WHERE CompanyId = @companyId;
	RETURN @count;

END

--COMPLETED

CREATE FUNCTION CATEGORIES_FUN_IsCategoryHasRelations(@categoryId int, @companyId int)
	RETURNS BIT
	AS
BEGIN
	
	DECLARE @hasRelations BIT = 0;
	SELECT @hasRelations = COUNT(*) FROM Products WHERE CategoryId = @categoryId AND CompanyId = @companyId;
	
	IF (@hasRelations > 0)
		RETURN 1;

	RETURN 0;

END

--COMPLETED

CREATE PROCEDURE CATEGORIES_SP_DeleteCategory
	@categoryId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isCategoryHasRelations BIT = 0;
	SELECT @isCategoryHasRelations = [dbo].[CATEGORIES_FUN_IsCategoryHasRelations] (@categoryId, @companyId);

	IF (@isCategoryHasRelations = 1)
		RETURN 0;

	DELETE FROM Categories WHERE CategoryId = @categoryId AND CompanyId = @companyId;
	
	RETURN @@ROWCOUNT;

END

--COMPLETED

------------------ END CATEGORIES TABLE -------------------


------------------ START SALESUNITS TABLE --------------------

CREATE FUNCTION SALESUNITS_FUN_GetSalesUnits (@companyId int)
	RETURNS TABLE
	AS
RETURN
(

	SELECT 
		UnitId,
		UnitName,
		(
			SELECT Users.UserName FROM Users WHERE s.ByUser = Users.UserId
		) AS ByUser
	FROM SalesUnits s WHERE CompanyId = @companyId

)

--COMPLETED

CREATE FUNCTION SALESUNITS_FUN_GetSaleUnitById (@unitId int, @companyId int)
	RETURNS TABLE
	AS
RETURN
(

		SELECT 
			UnitId,
			UnitName,
			ByUser,
			CompanyId
		FROM SalesUnits s WHERE CompanyId = @companyId AND s.UnitId = @unitId

)

--COMPLETED

CREATE FUNCTION SALESUNITS_FUN_IsSaleUnitExist(@unitName nvarchar(50), @companyId int)
	RETURNS BIT
	AS
BEGIN
	
	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM SalesUnits WHERE UnitName = @unitName AND CompanyId = @companyId;
	RETURN @isExist;

END

--COMPLETED

CREATE FUNCTION SALESUNITS_FUN_IsSaleUnitExistWithOutCurrentUnit(@unitId int, @unitName nvarchar(50), @companyId int)
	RETURNS BIT
	AS
BEGIN

	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM SalesUnits WHERE UnitName = @unitName AND CompanyId = @companyId AND UnitId != @unitId;
	RETURN @isExist;

END

--COMPLETED

CREATE PROCEDURE SALESUNITS_SP_NewSaleUnit
	@unitName nvarchar(50),
	@byUser int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isUnitExist BIT = 0;
	SELECT @isUnitExist = [dbo].[SALESUNITS_FUN_IsSaleUnitExist] (@unitName, @companyId)

	IF (@isUnitExist = 1)
		RETURN 0;

	INSERT INTO SalesUnits (UnitName, ByUser, CompanyId)
	VALUES
	(
		@unitName,
		@byUser,
		@companyId
	)

	RETURN SCOPE_IDENTITY();

END

--COMPLETED

CREATE PROCEDURE SALESUNITS_SP_UpdateSaleUnit
	@unitId int,
	@unitName nvarchar(50),
	@byUser int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isUnitExist BIT = 0;
	SELECT @isUnitExist = [dbo].[SALESUNITS_FUN_IsSaleUnitExistWithOutCurrentUnit] (@unitId, @unitName, @companyId)

	IF (@isUnitExist = 1)
		RETURN 0;

	UPDATE SalesUnits
	SET UnitName = @unitName,
	ByUser = @byUser
	WHERE UnitId = @unitId AND CompanyId = @companyId

	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE FUNCTION SALESUNITS_FUN_GetSalesUnitsCount(@companyId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @count INT = 0;
	SELECT @count = COUNT(UnitId) FROM SalesUnits WHERE CompanyId = @companyId;
	RETURN @count;

END

--COMPLETED

CREATE FUNCTION SALESUNITS_FUN_IsSaleUnitHasRelations(@unitId int, @companyId int)
	RETURNS BIT
	AS
BEGIN
	
	DECLARE @hasRelations BIT = 0;
	SELECT @hasRelations = COUNT(*) FROM Products WHERE UnitId = @unitId AND CompanyId = @companyId;
	
	IF (@hasRelations > 0)
		RETURN 1;

	RETURN 0;

END

--COMPLETED

CREATE PROCEDURE SALESUNITS_SP_DeleteSaleUnit
	@unitId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isUnitHasRelations BIT = 0;
	SELECT @isUnitHasRelations = [dbo].[SALESUNITS_FUN_IsSaleUnitHasRelations] (@unitId, @companyId)

	IF (@isUnitHasRelations = 1)
		RETURN 0;

	DELETE FROM SalesUnits WHERE UnitId = @unitId AND CompanyId = @companyId;
	
	RETURN @@ROWCOUNT;

END

--COMPLETED

------------------ END SALESUNITS TABLE -------------------

------------------ START CUSTOMERS TABLE -------------------

CREATE FUNCTION CUSTOMERS_FUN_GetCustomers (@companyId INT)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	Customers.CustomerId,
	P.FullName,
	P.Phone1,
	P.Phone2,
	Customers.CustomerCode,
	Customers.Address,
	
	CASE 
		WHEN Customers.CustomerStatus = 1 THEN 'Active'
		ELSE 'InActive'
	END AS CustomerStatus,

	P.TelegramID,

	(
		SELECT UserName FROM Users WHERE UserId = P.ByUser
	) AS ByUser

	FROM People P INNER JOIN Customers ON
	P.PersonId = Customers.PersonId
	WHERE P.CompanyId = @companyId

)

--COMPLETED

CREATE FUNCTION CUSTOMERS_FUN_GetActiveCustomers (@companyId INT)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	Customers.CustomerId,
	P.FullName,
	P.Phone1,
	P.Phone2,
	Customers.CustomerCode,
	Customers.Address,
	
	CASE 
		WHEN Customers.CustomerStatus = 1 THEN 'Active'
		ELSE 'InActive'
	END AS CustomerStatus,

	P.TelegramID,

	(
		SELECT UserName FROM Users WHERE UserId = P.ByUser
	) AS ByUser

	FROM People P INNER JOIN Customers ON
	P.PersonId = Customers.PersonId
	WHERE P.CompanyId = @companyId AND Customers.CustomerStatus = 1

)

--COMPLETED

CREATE FUNCTION CUSTOMERS_FUN_GetInActiveCustomers (@companyId INT)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	Customers.CustomerId,
	P.FullName,
	P.Phone1,
	P.Phone2,
	Customers.CustomerCode,
	Customers.Address,
	
	CASE 
		WHEN Customers.CustomerStatus = 1 THEN 'Active'
		ELSE 'InActive'
	END AS CustomerStatus,

	P.TelegramID,

	(
		SELECT UserName FROM Users WHERE UserId = P.ByUser
	) AS ByUser

	FROM People P INNER JOIN Customers ON
	P.PersonId = Customers.PersonId
	WHERE P.CompanyId = @companyId AND Customers.CustomerStatus = 0

)

--COMPLETED

CREATE FUNCTION CUSTOMERS_FUN_GetCustomerById (@customerId INT, @companyId INT)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	Customers.CustomerId,
	P.FullName,
	P.Phone1,
	P.Phone2,
	Customers.CustomerCode,
	Customers.Address,
	CustomerStatus,
	P.TelegramID,
	ByUser,
	CompanyId

	FROM People P INNER JOIN Customers ON
	P.PersonId = Customers.PersonId
	WHERE P.CompanyId = @companyId AND Customers.CustomerId = @customerId

)

--COMPLETED

--CREATE FUNCTION CUSTOMERS_FUN_GetCustomerByCode (@customerCode NVARCHAR(14), @companyId INT)
--	RETURNS TABLE
--	AS
--RETURN
--(
	
--	SELECT 
--	Customers.CustomerId,
--	P.FullName,
--	P.Phone1,
--	P.Phone2,
--	Customers.CustomerCode,
--	Customers.Address,
	
--	CASE 
--		WHEN Customers.CustomerStatus = 1 THEN 'Active'
--		ELSE 'InActive'
--	END AS CustomerStatus,

--	P.TelegramID,

--	(
--		SELECT UserName FROM Users WHERE UserId = P.ByUser
--	) AS ByUser

--	FROM People P INNER JOIN Customers ON
--	P.PersonId = Customers.PersonId
--	WHERE P.CompanyId = @companyId AND Customers.CustomerCode = @customerCode

--)

--COMPLETED - DELETED.

CREATE PROCEDURE CUSTOMERS_SP_SetCustomerAsActive
	@customerId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	UPDATE Customers
	SET CustomerStatus = 1
	WHERE Customers.CustomerId = @customerId AND 
	(
		SELECT CompanyId FROM People INNER JOIN Customers ON People.PersonId = Customers.PersonId AND Customers.CustomerId = @customerId
	)
	= @companyId;
	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE CUSTOMERS_SP_SetCustomerAsInActive
	@customerId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)

		RETURN 0;
	UPDATE Customers
	SET CustomerStatus = 0
	WHERE Customers.CustomerId = @customerId AND 
	(
		SELECT CompanyId FROM People INNER JOIN Customers ON People.PersonId = Customers.PersonId AND Customers.CustomerId = @customerId
	)
	= @companyId;
	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE FUNCTION CUSTOMERS_FUN_IsCustomerCodeExist (@customerCode NVARCHAR(14))
	RETURNS BIT
	AS
BEGIN

	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM Customers WHERE CustomerCode = @customerCode

	RETURN @isExist;

END

--COMPLETED

CREATE FUNCTION CUSTOMERS_FUN_GetCustomersCount (@companyId INT)
	RETURNS INT
	AS
BEGIN

	DECLARE @count INT = 0;
	SELECT @count = COUNT(*) FROM Customers INNER JOIN People ON
	People.PersonId = Customers.PersonId AND People.CompanyId = @companyId

	RETURN @count

END

--COMPLETED

CREATE FUNCTION CUSTOMERS_FUN_IsCustomerHasRelations (@customerId INT, @companyId INT)
	RETURNS BIT
	AS
BEGIN

	DECLARE @hasRelations BIT = 0;
	SELECT @hasRelations = COUNT(*) FROM DebtRecords WHERE CustomerId = @customerId AND CompanyId = @companyId;
	
	IF (@hasRelations > 0)
		RETURN 1;

	RETURN 0;

END

--COMPLETED

CREATE PROCEDURE CUSTOMERS_SP_NewCustomer
	@customerCode NVARCHAR(14),
	@address NVARCHAR(100),
	@fullName NVARCHAR(50),
	@phone1 NVARCHAR(14),
	@phone2 NVARCHAR(14),
	@telegramId NVARCHAR(25),
	@companyId INT,
	@byUser INT
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		DECLARE @personId int = 0;
		EXECUTE @personId = [dbo].[PEOPLE_SP_NewPerson] 
			@fullName,
			@phone1,
			@phone2,
			@telegramId,
			@companyId,
			@byUser;

		IF (@personId <= 0)
			THROW 50001, 'ERROR', 1;

		DECLARE @isCustomerCodeExist BIT = 0;
		SELECT @isCustomerCodeExist = [dbo].[CUSTOMERS_FUN_IsCustomerCodeExist] (@customerCode)

		IF (@isCustomerCodeExist = 1)
			THROW 50001, 'ERROR', 2;

		INSERT INTO Customers (CustomerCode, Address, CustomerStatus, PersonId)
		VALUES
		(
			@customerCode,
			@address,
			1,
			@personId
		)

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

	RETURN SCOPE_IDENTITY()

END

--COMPLETED

CREATE PROCEDURE CUSTOMERS_SP_UpdateCustomer
	@customerId int,
	@address nvarchar(100),
	@customerStatus bit,
	@fullName nvarchar(50),
	@phone1 nvarchar(14),
	@phone2 nvarchar(14),
	@telegramId nvarchar(25),
	@companyId int,
	@byUser int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @affectedRows INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY
		
		DECLARE @personId int = 0;
		SELECT @personId = PersonId FROM Customers WHERE Customers.CustomerId = @customerId;
		
		DECLARE @rowsAffected int = 0;
		EXECUTE @rowsAffected = [dbo].[PEOPLE_SP_UpdatePerson] 
			@personId,
			@fullName,
			@phone1,
			@phone2,
			@telegramId,
			@companyId,
			@byUser

		IF (@rowsAffected <= 0)
			THROW 50001, 'ERROR', 3;

		UPDATE Customers
		SET Address = @address,
		CustomerStatus = @customerStatus
		WHERE CustomerId = @customerId ;

		SET @affectedRows = @@ROWCOUNT;

		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH

	RETURN @affectedRows;

END

--COMPLETED

CREATE PROCEDURE CUSTOMERS_SP_DeleteCustomer
	@customerId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isCustomerHasRelations BIT = 0;
	SELECT @isCustomerHasRelations = [dbo].[CUSTOMERS_FUN_IsCustomerHasRelations] (@customerId, @companyId)

	IF (@isCustomerHasRelations = 1)
		RETURN 0;

	DECLARE @rowsAffected INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY
	
		DECLARE @personId INT = 0;
		SELECT @personId = PersonId FROM Customers WHERE CustomerId = @customerId;

		DELETE FROM Customers WHERE CustomerId = @customerId AND 
		(
			SELECT CompanyId FROM People WHERE PersonId = Customers.PersonId
		)
		= @companyId;

		EXECUTE @rowsAffected = [dbo].[PEOPLE_SP_DeletePerson] 
			@personId,
			@companyId
	
		COMMIT;

	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH
	
	RETURN @rowsAffected;

END

--COMPLETED
------------------ END CUSTOMERS TABLE -------------------

------------------ START PRODUCTS TABLE -------------------

CREATE FUNCTION PRODUCTS_FUN_GetProducts (@companyId int)
RETURNS TABLE
	AS
RETURN
(
	
	SELECT
		Products.ProductId,
		Products.ProductName,
		Products.ProductCode,
		Products.ProductPrice,
		Products.ProductImage,
		Categories.CategoryName,
		SalesUnits.UnitName,
		Users.UserName
	FROM Products INNER JOIN Categories ON Products.CategoryId = Categories.CategoryId INNER JOIN 
	SalesUnits ON Products.UnitId = SalesUnits.UnitId INNER JOIN Users ON Products.ByUser = Users.UserId
	WHERE Products.CompanyId = @companyId

)

--COMPLETED

CREATE FUNCTION PRODUCTS_FUN_GetProductById (@productId int, @companyId int)
RETURNS TABLE
	AS
RETURN
(
	
	SELECT
		Products.ProductId,
		Products.ProductName,
		Products.ProductCode,
		Products.ProductPrice,
		Products.ProductImage,
		Categories.CategoryId,
		SalesUnits.UnitId,
		Products.ByUser,
		Products.CompanyId
	FROM Products INNER JOIN Categories ON Products.CategoryId = Categories.CategoryId INNER JOIN 
	SalesUnits ON Products.UnitId = SalesUnits.UnitId INNER JOIN Users ON Products.ByUser = Users.UserId
	WHERE Products.CompanyId = @companyId AND Products.ProductId = @productId

)

--COMPLETED

CREATE FUNCTION PRODUCTS_FUN_GetProductByCode (@productCode nvarchar(14), @companyId int)
RETURNS TABLE
	AS
RETURN
(
	
	SELECT
		Products.ProductId,
		Products.ProductName,
		Products.ProductCode,
		Products.ProductPrice,
		Products.ProductImage,
		Categories.CategoryId,
		SalesUnits.UnitId,
		Products.ByUser,
		Products.CompanyId
	FROM Products INNER JOIN Categories ON Products.CategoryId = Categories.CategoryId INNER JOIN 
	SalesUnits ON Products.UnitId = SalesUnits.UnitId INNER JOIN Users ON Products.ByUser = Users.UserId
	WHERE Products.CompanyId = @companyId AND Products.ProductCode = @productCode

)

--COMPLETED

CREATE FUNCTION PRODUCTS_FUN_GetProductsByCategoryId (@categoryId int, @companyId int)
RETURNS TABLE
	AS
RETURN
(
	
	SELECT
		Products.ProductId,
		Products.ProductName,
		Products.ProductCode,
		Products.ProductPrice,
		Products.ProductImage,
		Categories.CategoryName,
		SalesUnits.UnitName,
		Users.UserName
	FROM Products INNER JOIN Categories ON Products.CategoryId = Categories.CategoryId INNER JOIN 
	SalesUnits ON Products.UnitId = SalesUnits.UnitId INNER JOIN Users ON Products.ByUser = Users.UserId
	WHERE Products.CompanyId = @companyId AND Products.CategoryId = @categoryId

)

--COMPLETED

CREATE FUNCTION PRODUCTS_FUN_IsProductCodeExist(@productCode nvarchar(14))
	RETURNS BIT
	AS
BEGIN

	DECLARE @isExist BIT = 0;
	SELECT @isExist = 1 FROM Products WHERE ProductCode = @productCode;
	
	RETURN @isExist;
	
END

--COMPLETED

CREATE FUNCTION PRODUCTS_FUN_GetProductsCount(@companyId int)
	RETURNS INT
	AS
BEGIN
	
	DECLARE @count INT = 0;
	SELECT @count = COUNT(*) FROM Products WHERE CompanyId = @companyId;

	RETURN @count;

END

--COMPLETED

CREATE FUNCTION PRODUCTS_FUN_IsProductHasRelations (@productId int, @companyId int)
	RETURNS BIT
	AS
BEGIN

	DECLARE @hasRelations INT = 0;
	SELECT @hasRelations = COUNT(*) FROM DebtRecords_Products WHERE ProductId = @productId AND CompanyId = @companyId;
	
	IF (@hasRelations > 0)
		RETURN 1;

	RETURN 0;

END

--COMPLETED

CREATE PROCEDURE PRODUCTS_SP_NewProduct
	@productName nvarchar(100),
	@productCode nvarchar(14),
	@productPrice money,
	@productImage varbinary(max),
	@categoryId int,
	@unitId int,
	@byUser int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isCodeExist BIT = 0;
	SELECT @isCodeExist = [dbo].[PRODUCTS_FUN_IsProductCodeExist] (@productCode);

	IF (@isCodeExist = 1)
		RETURN 0;

	INSERT INTO Products (ProductName, ProductCode, ProductPrice, ProductImage, CategoryId, UnitId, ByUser, CompanyId)
		VALUES
		(
			@productName,
			@productCode,
			@productPrice,
			@productImage,
			@categoryId,
			@unitId,
			@byUser,
			@companyId
		)

		RETURN SCOPE_IDENTITY();

END

--COMPLETED

CREATE PROCEDURE PRODUCTS_SP_UpdateProduct
	@productId int,
	@productName nvarchar(100),
	@productPrice money,
	@productImage varbinary(max),
	@categoryId int,
	@unitId int,
	@byUser int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	UPDATE Products
	SET 
	ProductName = @productName,
	ProductPrice = @productPrice,
	ProductImage = @productImage,
	CategoryId = @categoryId, 
	UnitId = @unitId,
	ByUser = @byUser
	WHERE ProductId = @productId AND CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

--COMPLETED

CREATE PROCEDURE PRODUCTS_SP_DeleteProduct
	@productId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isHasRelations BIT = 0;
	SELECT @isHasRelations = [dbo].[PRODUCTS_FUN_IsProductHasRelations] (@productId, @companyId);

	IF (@isHasRelations = 1)
		RETURN 0;

	DELETE FROM Products WHERE ProductId = @productId AND CompanyId = @companyId;

	RETURN @@ROWCOUNT;

END

------------------ END PRODUCTS TABLE -------------------

------------------ START DEBTRECORDS TABLE -------------------

CREATE FUNCTION DEBTRECORDS_FUN_GetDebtRecords (@companyId int)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	d.DebtRecordId,
	c.CustomerId,
	p.FullName,
	d.TotalPrice,
	d.RemainingAmount,
	
	CASE
		WHEN d.IsPaid = 1 THEN 'Paid'
		ELSE 'UnPaid'
	END AS IsPaid,

	d.RegistrationDate,
	d.Description,
	
	(
		SELECT Users.UserName FROM Users WHERE d.ByUser = Users.UserId
	) AS ByUser

	FROM DebtRecords d INNER JOIN Customers c ON d.CustomerId = c.CustomerId INNER JOIN
	People p ON c.PersonId = p.PersonId 
	WHERE d.CompanyId = @companyId

)

--COMPLETED

CREATE FUNCTION DEBTRECORDS_FUN_GetPaidDebtRecords (@companyId int)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	d.DebtRecordId,
	c.CustomerId,
	p.FullName,
	d.TotalPrice,
	d.RemainingAmount,
	
	CASE
		WHEN d.IsPaid = 1 THEN 'Paid'
		ELSE 'UnPaid'
	END AS IsPaid,

	d.RegistrationDate,
	d.Description,
	
	(
		SELECT Users.UserName FROM Users WHERE d.ByUser = Users.UserId
	) AS ByUser

	FROM DebtRecords d INNER JOIN Customers c ON d.CustomerId = c.CustomerId INNER JOIN
	People p ON c.PersonId = p.PersonId 
	WHERE d.CompanyId = @companyId AND d.IsPaid = 1

)

--COMPLETED

CREATE FUNCTION DEBTRECORDS_FUN_GetUnPaidDebtRecords (@companyId int)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	d.DebtRecordId,
	c.CustomerId,
	p.FullName,
	d.TotalPrice,
	d.RemainingAmount,
	
	CASE
		WHEN d.IsPaid = 1 THEN 'Paid'
		ELSE 'UnPaid'
	END AS IsPaid,

	d.RegistrationDate,
	d.Description,
	
	(
		SELECT Users.UserName FROM Users WHERE d.ByUser = Users.UserId
	) AS ByUser

	FROM DebtRecords d INNER JOIN Customers c ON d.CustomerId = c.CustomerId INNER JOIN
	People p ON c.PersonId = p.PersonId 
	WHERE d.CompanyId = @companyId AND d.IsPaid = 0

)

--COMPLETED

CREATE FUNCTION DEBTRECORDS_FUN_GetDebtRecordById (@debtRecordId int, @companyId int)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	d.DebtRecordId,
	c.CustomerId,
	d.TotalPrice,
	d.RemainingAmount,
	IsPaid,
	d.RegistrationDate,
	d.Description,
	d.ByUser,
	d.CompanyId

	FROM DebtRecords d INNER JOIN Customers c ON d.CustomerId = c.CustomerId INNER JOIN
	People p ON c.PersonId = p.PersonId 
	WHERE d.CompanyId = @companyId AND d.DebtRecordId = @debtRecordId

)

--COMPLETED

CREATE FUNCTION DEBTRECORDS_FUN_GetDebtRecordsCount(@companyId int)
	RETURNS INT
	AS
BEGIN

	DECLARE @count INT = 0;
	SELECT @count = COUNT(*) FROM DebtRecords WHERE CompanyId = @companyId;

	RETURN @count;

END

--COMPLETED

CREATE FUNCTION DEBTRECORDS_FUN_GetTotalDebt(@companyId int)
	RETURNS MONEY
	AS
BEGIN

	DECLARE @totalDebt MONEY = COALESCE
	(
		(
			SELECT SUM (RemainingAmount) FROM DebtRecords 
			WHERE CompanyId = @companyId AND IsPaid = 0
		), 0
	);

	RETURN @totalDebt;

END

--COMPLETED

CREATE FUNCTION DEBTRECORDS_FUN_IsDebtRecordsHasRelations(@debtRecordId int, @companyId int)
	RETURNS BIT
	AS
BEGIN
	
	DECLARE @hasRelations BIT = 0;

	SELECT @hasRelations = 1 FROM PaidRecords WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;
	IF (@hasRelations = 1)
		RETURN 1;
	
	RETURN 0;

END

--COMPLETED

CREATE PROCEDURE DEBTRECORDS_SP_DeleteDebtRecord
	@debtRecordId int,
	@companyId int
	AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @isHasRelations BIT = 0;
	SELECT @isHasRelations = [dbo].[DEBTRECORDS_FUN_IsDebtRecordsHasRelations] (@debtRecordId, @companyId);

	IF (@isHasRelations = 1)
		RETURN 0;

	DECLARE @rowsAffected INT = 0;

	BEGIN TRANSACTION;
	BEGIN TRY

		DELETE FROM DebtRecords_Products WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;
		DELETE FROM DebtRecords WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

		SET @rowsAffected = @@ROWCOUNT;
		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		RETURN 0;
	END CATCH

	RETURN @rowsAffected;

END

--COMPLETED

CREATE TYPE DebtRecords_Products_Type AS TABLE 
(
    ProductId INT,
    Quantity INT
);

--COMPLETED

CREATE PROCEDURE DEBTRECORDS_SP_NewDebtRecord
    @customerId INT,
    @description NVARCHAR(250),
    @byUser INT,
    @companyId INT,
    @debtRecordsProducts DebtRecords_Products_Type READONLY
AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	IF NOT EXISTS (SELECT 1 FROM @debtRecordsProducts)
        RETURN 0; -- إنهاء الإجراء إذا كان الجدول المؤقت فارغًا

	DECLARE @customerStatus BIT = 0;
	SELECT @customerStatus = CustomerStatus FROM Customers WHERE CustomerId = @customerId;
	
	IF (@customerStatus = 0)
		RETURN 0;

    DECLARE @NewDebtRecordId INT;

    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @totalPrice MONEY = 0;
        SELECT @totalPrice = SUM(p.ProductPrice * dp.Quantity) FROM @debtRecordsProducts dp
        INNER JOIN Products p ON dp.ProductId = p.ProductId AND p.CompanyId = @companyId;

        INSERT INTO DebtRecords (CustomerId, TotalPrice, RemainingAmount, IsPaid, RegistrationDate, Description, ByUser, CompanyId)
        VALUES
        (
            @customerId,
            @totalPrice,
            @totalPrice,
            0,
            GETDATE(),
            @description,
            @byUser,
            @companyId
        );

        SET @NewDebtRecordId = SCOPE_IDENTITY();

        DECLARE @currency NVARCHAR(14);
        SELECT @currency = Currency FROM Settings WHERE CompanyId = @companyId;

        -- إدخال البيانات في الجدول الوسيط DebtRecords_Products
        INSERT INTO DebtRecords_Products (DebtRecordId, ProductId, Quantity, ProductPrice, TotalPrice, Currency, CompanyId)
        SELECT 
            @NewDebtRecordId, 
            dp.ProductId,
            dp.Quantity, 
            p.ProductPrice,
            p.ProductPrice * dp.Quantity,
            @currency,
            @companyId
        FROM @debtRecordsProducts dp
        INNER JOIN Products p ON dp.ProductId = p.ProductId AND p.CompanyId = @companyId;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
		RETURN 0;
    END CATCH

    RETURN @NewDebtRecordId;

END;

--COMPLETED

CREATE PROCEDURE DEPTRECORDS_SP_UpdateDebtRecord
    @debtRecordId INT,
    @customerId INT,
    @description NVARCHAR(250),
    @byUser INT,
    @companyId INT,
    @debtRecordsProductsToUpdate DebtRecords_Products_Type READONLY
AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	IF NOT EXISTS (SELECT 1 FROM @debtRecordsProductsToUpdate)
        RETURN 0; -- إنهاء الإجراء إذا كان الجدول المؤقت فارغًا

    DECLARE @paidStatus BIT = 0;
    SELECT @paidStatus = IsPaid FROM DebtRecords WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    IF @paidStatus = 1
        RETURN 0;

    DECLARE @rowsAffected INT = 0;
    DECLARE @totalPrice MONEY = 0;
    DECLARE @currency NVARCHAR(14);

    SELECT @currency = Currency FROM Settings WHERE CompanyId = @companyId;

    BEGIN TRANSACTION;
    BEGIN TRY

        -- حساب السعر الإجمالي للمنتجات الجديدة
        SELECT @totalPrice = SUM(p.ProductPrice * dp.Quantity)
        FROM @debtRecordsProductsToUpdate dp
        INNER JOIN Products p ON dp.ProductId = p.ProductId AND p.CompanyId = @companyId;

        -- حساب المبلغ المدفوع والمتبقي
        DECLARE @paymentAmount MONEY = COALESCE((SELECT SUM(PaymentAmount) FROM PaidRecords WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId), 0);
        DECLARE @remainingAmount MONEY = @totalPrice - @paymentAmount;

        -- تحديث بيانات جدول DebtRecords
        UPDATE DebtRecords
        SET
            CustomerId = @customerId,
            TotalPrice = @totalPrice,
            RemainingAmount = @remainingAmount,
            Description = @description,
            ByUser = @byUser
        WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

        SET @rowsAffected = @@ROWCOUNT;

        -- حذف الصفوف القديمة من جدول DebtRecords_Products
        DELETE FROM DebtRecords_Products
        WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

        -- إدخال البيانات الجديدة في جدول DebtRecords_Products
        INSERT INTO DebtRecords_Products (DebtRecordId, ProductId, Quantity, ProductPrice, TotalPrice, Currency, CompanyId)
        SELECT 
            @debtRecordId, 
            dp.ProductId,
            dp.Quantity, 
            p.ProductPrice,
            p.ProductPrice * dp.Quantity,
            @currency,
            @companyId
        FROM @debtRecordsProductsToUpdate dp
        INNER JOIN Products p ON dp.ProductId = p.ProductId AND p.CompanyId = @companyId;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        RETURN 0;
    END CATCH

    RETURN @rowsAffected;
END;

--COMPLETED

------------------ END DEBTRECORDS TABLE -------------------

------------------ START DEBTRECORDS_PRODUCTS TABLE -------------------

CREATE FUNCTION DEBTRECORDS_PRODUCTS_FUN_GetByDebtRecordsId (@debtRecordsId int, @companyId int)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT 
	dp.Debt_Product_Id,
	People.FullName,
	p.ProductName,
	dp.ProductPrice,
	SalesUnits.UnitName,
	dp.Quantity,
	dp.TotalPrice,
	dp.Currency,
	
	(
		SELECT Users.UserName FROM Users WHERE Users.UserId = d.ByUser
	) AS ByUser,
	
	CASE
		WHEN d.IsPaid = 1 THEN 'Paid'
		ELSE 'UnPaid'
	END AS IsPaid,

	d.RegistrationDate
	FROM DebtRecords_Products dp INNER JOIN DebtRecords d ON
	dp.DebtRecordId = d.DebtRecordId INNER JOIN 
	Products p ON dp.ProductId = p.ProductId INNER JOIN
	Customers c ON d.CustomerId = c.CustomerId INNER JOIN
	People ON c.PersonId = People.PersonId INNER JOIN SalesUnits ON
	SalesUnits.UnitId = P.UnitId

	WHERE dp.DebtRecordId = @debtRecordsId AND dp.CompanyId = @companyId

)

--COMPLETED

------------------ END DEBTRECORDS_PRODUCTS TABLE -------------------

------------------ START PAIDRECORDS TABLE -------------------

CREATE FUNCTION PAIDRECORDS_FUN_HasPreviousPayments (@debtRecordId INT, @companyId INT)
	RETURNS BIT
	AS
BEGIN
    DECLARE @hasPayments BIT;

    -- التحقق مما إذا كان هناك أي دفعات سابقة لسجل الدين المحدد
    IF EXISTS
		(
			SELECT 1 FROM PaidRecords 
			WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId
		)
        SET @hasPayments = 1;
    ELSE
        SET @hasPayments = 0;

    RETURN @hasPayments;

END;

--COMPLETED

CREATE FUNCTION PAIDRECORDS_FUN_HasPreviousPaymentsWithOutCurrentPayment (@paidRecordId INT, @debtRecordId INT, @companyId INT)
	RETURNS BIT
	AS
BEGIN
    DECLARE @hasPayments BIT;

    -- التحقق مما إذا كان هناك أي دفعات سابقة لسجل الدين المحدد
    IF EXISTS
		(
			SELECT 1 FROM PaidRecords 
			WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId AND PaidRecordId != @paidRecordId
		)
        SET @hasPayments = 1;
    ELSE
        SET @hasPayments = 0;

    RETURN @hasPayments;

END;

--COMPLETED

CREATE FUNCTION PAIDRECORDS_FUN_GetPaidRecordsCountByDebtRecordId (@debtRecordId INT, @companyId INT)
	RETURNS INT
	AS
BEGIN

    DECLARE @paidRecordsCount INT;

     --حساب عدد السجلات في جدول PaidRecords لسجل الدين المحدد
    SELECT @paidRecordsCount = COUNT(*)
    FROM PaidRecords
    WHERE DebtRecordId = @debtRecordId
      AND CompanyId = @companyId;

    RETURN @paidRecordsCount;

END;

--COMPLETED

CREATE FUNCTION PAIDRECORDS_FUN_GetPaidRecords (@companyId INT)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT
		paid.PaidRecordId,
		debt.DebtRecordId,
		c.CustomerId,
		p.FullName AS CustomerName, 
		debt.TotalPrice,
		debt.RemainingAmount,

		CASE 
			WHEN debt.IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		CASE 
			WHEN paid.PaymentTypes = 1 THEN 'Full Payment'
			ELSE 'Partial Payment'
		END AS PaymentType,

		paid.PaymentAmount,
		paid.RegistrationDate,
		paid.Description,
		
		(
			SELECT Users.UserName FROM Users WHERE paid.ByUser = Users.UserId
		) AS ByUser

	FROM PaidRecords paid INNER JOIN DebtRecords debt ON 
	paid.DebtRecordId = debt.DebtRecordId INNER JOIN Customers c ON
	debt.CustomerId = c.CustomerId INNER JOIN People p ON
	c.PersonId = p.PersonId

	WHERE paid.CompanyId = @companyId

)

--COMPLETED

CREATE FUNCTION PAIDRECORDS_FUN_GetPaidRecordsByDebtRecordId (@debtRecordId INT, @companyId INT)
	RETURNS TABLE
	AS
RETURN
(
	
	SELECT
		paid.PaidRecordId,
		debt.DebtRecordId,
		c.CustomerId,
		p.FullName AS CustomerName, 
		debt.TotalPrice,
		debt.RemainingAmount,

		CASE 
			WHEN debt.IsPaid = 1 THEN 'Paid'
			ELSE 'UnPaid'
		END AS IsPaid,

		CASE 
			WHEN paid.PaymentTypes = 1 THEN 'Full Payment'
			ELSE 'Partial Payment'
		END AS PaymentType,

		paid.PaymentAmount,
		paid.RegistrationDate,
		paid.Description,
		
		(
			SELECT Users.UserName FROM Users WHERE paid.ByUser = Users.UserId
		) AS ByUser

	FROM PaidRecords paid INNER JOIN DebtRecords debt ON 
	paid.DebtRecordId = debt.DebtRecordId INNER JOIN Customers c ON
	debt.CustomerId = c.CustomerId INNER JOIN People p ON
	c.PersonId = p.PersonId

	WHERE paid.CompanyId = @companyId AND paid.DebtRecordId = @debtRecordId

)

--COMPLETED

CREATE PROCEDURE PAIDRECORDS_SP_NewPaid
    @debtRecordId INT,
    @paymentAmount MONEY,
	@description NVARCHAR(250),
	@byUser INT,
    @companyId INT
AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

	DECLARE @paymentType BIT = 0;

	DECLARE @totalPrice MONEY = 0;
	SELECT @totalPrice = TotalPrice FROM DebtRecords WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

	DECLARE @remainingAmount MONEY = 0;
	SELECT @remainingAmount = RemainingAmount FROM DebtRecords WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;


	IF (@paymentAmount > @remainingAmount OR @paymentAmount <= 0)
		RETURN 0;

	DECLARE @isHasPreviousPayments BIT = [dbo].[PAIDRECORDS_FUN_HasPreviousPayments] (@debtRecordId, @companyId);

	IF (@isHasPreviousPayments = 0)
	BEGIN
		
		IF (@paymentAmount = @totalPrice)
			SET @paymentType = 1;
		ELSE
			SET @paymentType = 0;

	END
	ELSE
		SET @paymentType = 0;

	INSERT INTO PaidRecords (DebtRecordId, PaymentTypes, PaymentAmount, RegistrationDate, Description, ByUser, CompanyId)
	VALUES
	(
		@debtRecordId,
		@paymentType,
		@paymentAmount,
		GETDATE(),
		@description,
		@byUser,
		@companyId
	)

    RETURN SCOPE_IDENTITY();

END;

--COMPLETED

CREATE TRIGGER PAIDRECORDS_TRG_UpdateDebtRecordsOnPaymentInsert ON PaidRecords
	AFTER INSERT
	AS
BEGIN

    DECLARE @debtRecordId INT;
    DECLARE @companyId INT;
    DECLARE @totalPaid MONEY;
    DECLARE @totalPrice MONEY;
    DECLARE @remainingAmount MONEY;

    -- الحصول على معرف الدين ومعرف الشركة من السجلات المدخلة
    SELECT TOP 1 
        @debtRecordId = i.DebtRecordId,
        @companyId = i.CompanyId
    FROM inserted i;

    -- حساب إجمالي المدفوعات الجديدة للسجل الدين المحدد
    SELECT @totalPaid = SUM(PaymentAmount)
    FROM PaidRecords
    WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    -- الحصول على السعر الإجمالي للسجل الدين المحدد
    SELECT @totalPrice = TotalPrice
    FROM DebtRecords
    WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    -- حساب المبلغ المتبقي الجديد
    SET @remainingAmount = @totalPrice - @totalPaid;

    -- تحديث المبلغ المتبقي في جدول DebtRecords
    UPDATE DebtRecords
    SET RemainingAmount = @remainingAmount
    WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    -- إذا كان المبلغ المتبقي صفر، تحديث حالة الدفع إلى "مدفوع"
    IF @remainingAmount = 0
        UPDATE DebtRecords
        SET IsPaid = 1
        WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    ELSE
        UPDATE DebtRecords
        SET IsPaid = 0
        WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

END;

--COMPLETED

CREATE FUNCTION PAIDRECORDS_FUN_GetTotalPaymentAmountWithOutCurrentPayment (@paidRecordId int, @debtRecordId int,  @companyId int)
	RETURNS MONEY
	AS
BEGIN

	DECLARE @totalAmount MONEY = 0;
	SELECT @totalAmount = COALESCE(
    (
		SELECT SUM(PaymentAmount) 
		FROM PaidRecords 
		WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId AND PaidRecordId != @paidRecordId),
		0
	);

	RETURN @totalAmount;

END

--COMPLETED

CREATE PROCEDURE PAIDRECORDS_SP_UpdatePaidRecord
    @paidRecordId INT,
    @paymentAmount MONEY,
    @description NVARCHAR(250),
    @byUser INT,
    @companyId INT
AS
BEGIN

	DECLARE @isSubscriptionActive BIT = 0;
	SELECT @isSubscriptionActive = [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId);

	IF (@isSubscriptionActive = 0)
		RETURN 0;

    DECLARE @paymentType BIT = 0;
    DECLARE @debtRecordId INT =  0;
    DECLARE @totalPrice MONEY = 0;
    DECLARE @isHasPreviousPayments BIT = 0;

    -- الحصول على معرف الدين والسعر الإجمالي
    SELECT @debtRecordId = DebtRecordId, @totalPrice = TotalPrice
    FROM DebtRecords 
    WHERE DebtRecordId = (SELECT DebtRecordId FROM PaidRecords WHERE PaidRecordId = @paidRecordId AND CompanyId = @companyId);


	DECLARE @remainingAmount MONEY = [dbo].[PAIDRECORDS_FUN_GetTotalPaymentAmountWithOutCurrentPayment] (@paidRecordId, @debtRecordId, @companyId);

    -- التحقق من صحة مبلغ الدفعة
    IF (@paymentAmount > (@totalPrice - @remainingAmount) OR @paymentAmount <= 0)
        RETURN 0;

    -- التحقق من وجود دفعات سابقة باستثناء الدفعة الحالية
    SELECT @isHasPreviousPayments = [dbo].[PAIDRECORDS_FUN_HasPreviousPaymentsWithOutCurrentPayment] (@paidRecordId, @debtRecordId, @companyId);

    -- تحديد نوع الدفعة
    IF (@isHasPreviousPayments = 0)
    BEGIN
        IF (@paymentAmount = @totalPrice)
            SET @paymentType = 1;
        ELSE
            SET @paymentType = 0;
    END
    ELSE
        SET @paymentType = 0;

    -- تحديث بيانات الدفع في جدول PaidRecords
    UPDATE PaidRecords
    SET
        PaymentAmount = @paymentAmount,
        PaymentTypes = @paymentType,
        Description = @description,
        ByUser = @byUser
    WHERE PaidRecordId = @paidRecordId AND CompanyId = @companyId;

    -- إرجاع عدد الصفوف المتأثرة
    RETURN @@ROWCOUNT;

END;

--COMPLETED

CREATE TRIGGER PAIDRECORDS_TRG_UpdateDebtRecordsOnPaymentUpdate ON PaidRecords
	AFTER UPDATE
	AS
BEGIN

    DECLARE @debtRecordId INT;
    DECLARE @companyId INT;
    DECLARE @totalPaid MONEY;
    DECLARE @totalPrice MONEY;
    DECLARE @remainingAmount MONEY;

    -- الحصول على معرف الدين ومعرف الشركة من السجلات المعدلة
    SELECT TOP 1 
        @debtRecordId = i.DebtRecordId,
        @companyId = i.CompanyId
    FROM inserted i;

    -- حساب إجمالي المدفوعات الجديدة للسجل الدين المحدد
    SELECT @totalPaid = SUM(PaymentAmount)
    FROM PaidRecords
    WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    -- الحصول على السعر الإجمالي للسجل الدين المحدد
    SELECT @totalPrice = TotalPrice
    FROM DebtRecords
    WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    -- حساب المبلغ المتبقي الجديد
    SET @remainingAmount = @totalPrice - @totalPaid;

    -- تحديث المبلغ المتبقي في جدول DebtRecords
    UPDATE DebtRecords
    SET RemainingAmount = @remainingAmount
    WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    -- إذا كان المبلغ المتبقي صفر، تحديث حالة الدفع إلى "مدفوع"
    IF @remainingAmount = 0
        UPDATE DebtRecords
        SET IsPaid = 1
        WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

    ELSE
        UPDATE DebtRecords
        SET IsPaid = 0
        WHERE DebtRecordId = @debtRecordId AND CompanyId = @companyId;

END;

----COMPLETED

------------------ END PAIDRECORDS TABLE -------------------

------------------ START ERRORS TABLE -------------------

CREATE PROCEDURE ERRORS_SP_NewError
    @errorMessage nvarchar(250),
	@source nvarchar(50),
	@class nvarchar(50),
	@method nvarchar(50),
	@stackTrace nvarchar(250),
	@companyId int,
	@action nvarchar(50),
	@params nvarchar(500)
AS
BEGIN

	INSERT INTO Errors (ErrorDate, ErrorMessage, Source, Class, Method, StackTrace, CompanyId, Action, Params)
	VALUES
	(
		GETDATE(),
		@errorMessage,
		@source,
		@class,
		@method,
		@stackTrace,
		@companyId,
		@action,
		@params
	)

    RETURN SCOPE_IDENTITY();

END;

--COMPLETED

------------------ END ERRORS TABLE -------------------

--COMPLETED IN 2024-7-4