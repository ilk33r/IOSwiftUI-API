START TRANSACTION;

ALTER TABLE `MemberImages` ADD `DirectSale` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `MemberImages` ADD `Height` int NOT NULL DEFAULT 0;

ALTER TABLE `MemberImages` ADD `IsDraft` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `MemberImages` ADD `Price` double NOT NULL DEFAULT 0.0;

ALTER TABLE `MemberImages` ADD `PriceCurrency` int NOT NULL DEFAULT 0;

ALTER TABLE `MemberImages` ADD `SaleAmount` int NOT NULL DEFAULT 0;

ALTER TABLE `MemberImages` ADD `Width` int NOT NULL DEFAULT 0;

CREATE INDEX `IX_MemberImages_IsDraft` ON `MemberImages` (`IsDraft`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240204153232_ImageMetadata', '7.0.8');

COMMIT;

