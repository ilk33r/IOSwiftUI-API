START TRANSACTION;

CREATE TABLE `MemberFaceIDs` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `MemberID` int NOT NULL,
    `AuthenticationKey` varchar(140) CHARACTER SET utf8mb4 NOT NULL,
    `PairDate` datetime NOT NULL,
    CONSTRAINT `PK_MemberFaceIDs` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_MemberFaceIDs_Members_MemberID` FOREIGN KEY (`MemberID`) REFERENCES `Members` (`ID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$fhWCgya+Zow5wNtDmlVVC8tHJqv539jW79eDbVl8bvdXM9tME5HYkkJkl8HbZlsYQKQJQixfzzRXimI6j9yT5L/JkizkXvO8J5qJsRbAuPYIgJsvsaE/F9t7ABI0yEHW'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_MemberFaceIDs_MemberID` ON `MemberFaceIDs` (`MemberID`);

CREATE INDEX `IX_MemberFaceIDs_PairDate` ON `MemberFaceIDs` (`PairDate`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221231103238_Biometric', '6.0.2');

COMMIT;

