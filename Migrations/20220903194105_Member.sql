START TRANSACTION;

CREATE TABLE `Members` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `UserName` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UserToken` varchar(48) CHARACTER SET utf8mb4 NULL,
    `TokenDate` datetime NOT NULL,
    `RegisterDate` datetime NOT NULL,
    `BirthDate` datetime NOT NULL,
    `Email` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Surname` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `LocationName` varchar(128) CHARACTER SET utf8mb4 NULL,
    `LocationLatitude` double NOT NULL,
    `LocationLongitude` double NOT NULL,
    `ProfilePictureFileName` longtext CHARACTER SET utf8mb4 NULL,
    `UserStatus` int NOT NULL,
    CONSTRAINT `PK_Members` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$5m0ylN3bS94ONPbhf2wqwkh+nOS7HtVbO4Ip31fsffh0ecnUcFve95a1S4rDn63YT9brhvx7rOOyyrDChFw7AKVXkh4fXmvMCBLD3zNRMKovOiR97qokvFxo/9ZDgHMM'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE UNIQUE INDEX `IX_Members_Email` ON `Members` (`Email`);

CREATE INDEX `IX_Members_Name_Surname` ON `Members` (`Name`, `Surname`);

CREATE UNIQUE INDEX `IX_Members_UserName` ON `Members` (`UserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220903194105_Member', '6.0.2');

COMMIT;

