START TRANSACTION;

ALTER TABLE `Members` MODIFY COLUMN `BirthDate` DATETIME NOT NULL;

CREATE TABLE `MemberFollowings` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `FollowDate` datetime NOT NULL,
    `MemberID` int NOT NULL,
    `FollowingMemberID` int NOT NULL,
    CONSTRAINT `PK_MemberFollowings` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_MemberFollowings_Members_FollowingMemberID` FOREIGN KEY (`FollowingMemberID`) REFERENCES `Members` (`ID`) ON DELETE CASCADE,
    CONSTRAINT `FK_MemberFollowings_Members_MemberID` FOREIGN KEY (`MemberID`) REFERENCES `Members` (`ID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$Shh2fqbP8+LjYoxB6xEPecchSvGru3BlIHtN1xOgUsJX1S/3tsb+UgAom8Hx23deXS2vC7Sfgzf1GgWVv4d2VwNol0/XfistcLkcslw05ShPde7DILSxW8CoGlgqMFhg'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_MemberFollowings_FollowDate` ON `MemberFollowings` (`FollowDate`);

CREATE INDEX `IX_MemberFollowings_FollowingMemberID` ON `MemberFollowings` (`FollowingMemberID`);

CREATE INDEX `IX_MemberFollowings_MemberID` ON `MemberFollowings` (`MemberID`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220910201720_MemberFollowing', '6.0.2');

COMMIT;

