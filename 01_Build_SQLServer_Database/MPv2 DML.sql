USE MPv2;

SET DATEFORMAT DMY; --Damn Americans >:(

BULK INSERT [User]
FROM '{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\1_User.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

BULK INSERT [Practitioner]
FROM '{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\2_Practitioner.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

BULK INSERT [PractitionerType]
FROM '{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\3_PractitionerType.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

BULK INSERT [Day]
FROM '{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\CSV Data\5_Day.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

BULK INSERT [Availability]
FROM '{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\6_Availability.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

BULK INSERT [Address]
FROM '{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\7_Address.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

BULK INSERT [Appointment]
FROM 'E{insert the rest your filepath here}\MedicalPracticeV2\01_Build_SQLServer_Database\9_Appointment.csv'
WITH (
	FIRSTROW = 1,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);

SELECT * FROM [User];
SELECT * FROM [Practitioner];
SELECT * FROM [PractitionerType];
SELECT * FROM [Day];
SELECT * FROM [Availability];
SELECT * FROM [Address];
SELECT * FROM [Appointment];

--A place to test if the new DB returns the correct results.
--	This is will be also useful for making API calls in ASP.NET Core

--Practitioners
SELECT p.practitionerId, p.medicalRegistrationNo, u.* FROM 
Practitioner p 
INNER JOIN [User] u ON p.userId = u.userId;

--Patitents
SELECT u.* 
FROM [User] u 
LEFT JOIN Practitioner p ON u.userId = p.userId
WHERE p.practitionerId IS NULL;

--Availability of specific practitioners
SELECT d.dayName
FROM Availability a
INNER JOIN Day d ON a.dayId = d.dayId
INNER JOIN Practitioner p ON a.practitionerId = p.practitionerId
INNER JOIN [User] u ON p.userId = u.userId
WHERE CONCAT(u.title, ' ', u.fName, ' ', u.lName) = 'Mrs Leslie Gray';

--Specific address of a user
SELECT CONCAT(u.title, ' ', u.fName, ' ', u.lName) AS 'user', a.houseUnitLotNo, a.street, a.suburb, a.state, a.postcode
FROM [User] u
INNER JOIN Address a ON u.addressId = a.addressId
WHERE a.addressId = 3001;

-- Check to see if Appointment table returns correct values
--	Results are ugly, but work
SELECT CONCAT(u.fName, ' ', u.lName) AS 'patient', a.appId, a.appDate, a.appTime, CONCAT(u2.title, ' ', u2.fName, ' ', u2.lName) AS 'practitioner'
FROM Appointment a
INNER JOIN [User] u ON a.patientId = u.userId
INNER JOIN Practitioner p ON a.practitionerId = p.practitionerId
INNER JOIN [User] u2 ON p.userId = u2.userId;