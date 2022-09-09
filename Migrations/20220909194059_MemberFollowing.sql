START TRANSACTION;

CREATE TABLE `MemberFollowings` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `FollowDate` datetime NOT NULL,
    `MemberID` int NULL,
    CONSTRAINT `PK_MemberFollowings` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_MemberFollowings_Members_MemberID` FOREIGN KEY (`MemberID`) REFERENCES `Members` (`ID`)
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$PfNx26e7O+9S260DpUIh6TqZPbJYS51BBa6qIJg45z0OCjdBJPVoVrBQy0SXV79nj1MbP+EAvqqHCaUym96yc1jHhZLt4eRNMycuhROzxf4jeID6MgHeipLxOa+xv6kp'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_MemberFollowings_FollowDate` ON `MemberFollowings` (`FollowDate`);

CREATE INDEX `IX_MemberFollowings_MemberID` ON `MemberFollowings` (`MemberID`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220909194059_MemberFollowing', '6.0.2');

COMMIT;

