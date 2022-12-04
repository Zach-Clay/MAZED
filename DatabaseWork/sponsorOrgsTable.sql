CREATE TABLE `sponsorOrgs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrgName` varchar(30) NOT NULL,
  `OrgDescription` longtext NOT NULL,
  `CatalogueID` int NOT NULL,
  `dollarToPoint` double NOT NULL DEFAULT '0.01',
  `isBlacklisted` tinyint NOT NULL,
  `DailyPointAmount` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci