START TRANSACTION;

ALTER TABLE `Members` ADD `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL;

CREATE TABLE `MemberImages` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `FileName` longtext CHARACTER SET utf8mb4 NULL,
    `CreateDate` datetime NOT NULL,
    `ImagesID` int NULL,
    CONSTRAINT `PK_MemberImages` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_MemberImages_Members_ImagesID` FOREIGN KEY (`ImagesID`) REFERENCES `Members` (`ID`)
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$WitkW0dOGZqh3h9w+T/hpmGauOnpg3/MecP2JLC4cIa3Pm1B+8Fh+IPC9NNt+CrZ0ekPRjxXvpU6XPmty3tydFcOe9Qo4h/OblYPGRnl7g01LA4O1wIBIEGl+8J5k1Ep'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_MemberImages_CreateDate` ON `MemberImages` (`CreateDate`);

CREATE INDEX `IX_MemberImages_ImagesID` ON `MemberImages` (`ImagesID`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220908161607_MemberImages', '6.0.2');

COMMIT;

