START TRANSACTION;

CREATE TABLE `MemberFaceIDs` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `MemberID` int NOT NULL,
    `AuthenticationKey` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
    `PairDate` datetime NOT NULL,
    CONSTRAINT `PK_MemberFaceIDs` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_MemberFaceIDs_Members_MemberID` FOREIGN KEY (`MemberID`) REFERENCES `Members` (`ID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$SOkE5oCTi5wWVy8kjHltpHUQkc825PSCKDF49eReWdcK/Iq9ZQP3buE3qjJGYkPta+id+IRdoaOUMq+8P0GlTclEqq/djAAUrjnfQj55LYeU9qsld6D5sV7H4MbYlCVK'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_MemberFaceIDs_MemberID` ON `MemberFaceIDs` (`MemberID`);

CREATE INDEX `IX_MemberFaceIDs_PairDate` ON `MemberFaceIDs` (`PairDate`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221231113758_Biometric', '6.0.2');

COMMIT;

