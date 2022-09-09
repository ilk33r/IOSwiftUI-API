START TRANSACTION;

ALTER TABLE `Members` ADD `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL;

CREATE TABLE `MemberImages` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `FileName` longtext CHARACTER SET utf8mb4 NULL,
    `CreateDate` datetime NOT NULL,
    `MemberID` int NULL,
    CONSTRAINT `PK_MemberImages` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_MemberImages_Members_MemberID` FOREIGN KEY (`MemberID`) REFERENCES `Members` (`ID`)
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$NNBIRS6vFuTDj0+LPwjxfUrepth9wjh/IRI38fpPd6dddKHm4K8jU8DU0ovzDel+F9r0E8f9l8z+xUcsPPNiWjKZsO1Ew6fk+qlkeNXvCL9pOTfKv/AcN589NjKPAg8N'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_MemberImages_CreateDate` ON `MemberImages` (`CreateDate`);

CREATE INDEX `IX_MemberImages_MemberID` ON `MemberImages` (`MemberID`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220909182214_MemberImages', '6.0.2');

COMMIT;

