START TRANSACTION;

ALTER TABLE `Members` DROP COLUMN `OneTimeCode`;

CREATE TABLE `OneTimeCodes` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `PhoneNumber` varchar(16) CHARACTER SET utf8mb4 NOT NULL,
    `OneTimeCode` varchar(6) CHARACTER SET utf8mb4 NOT NULL,
    `CreateDate` DATETIME NOT NULL,
    `ValidateDate` DATETIME NOT NULL,
    `IsValidated` tinyint(1) NOT NULL,
    CONSTRAINT `PK_OneTimeCodes` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$AkOQUURPH3Ikqa+3Mby5gzH3jeg0vCxTPQrbJ6Kkk5qysT9c9Wnb3ehdIH5Y7mVe4ipcxUeU5rWNn0Nh+PqHXU2yrBFkhbhbtW12RHy4VYfITx9d7BYlQj5DBY8/SdiS'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE UNIQUE INDEX `IX_OneTimeCodes_PhoneNumber` ON `OneTimeCodes` (`PhoneNumber`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220912195209_OTP', '6.0.2');

COMMIT;

