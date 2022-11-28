CREATE TABLE `driverOrders` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `SponsorID` int NOT NULL,
  `OrderStatus` varchar(30) DEFAULT NULL,
  `TotalPointVal` double NOT NULL,
  `OrderDate` datetime NOT NULL,
  `ProductList` json NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `order_driverFK` (`UserID`),
  KEY `order_sponsorFK` (`SponsorID`),
  CONSTRAINT `order_driverFK` FOREIGN KEY (`UserID`) REFERENCES `users` (`Id`),
  CONSTRAINT `order_sponsorFK` FOREIGN KEY (`SponsorID`) REFERENCES `sponsorOrgs` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci