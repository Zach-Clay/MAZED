CREATE TABLE `auditLogging` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `UserId` varchar(45) DEFAULT NULL,
  `SponsorId` varchar(45) DEFAULT NULL,
  `AppStatus` varchar(45) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `AppReason` varchar(45) DEFAULT NULL,
  `PointChange` int DEFAULT NULL,
  `PointReason` varchar(45) DEFAULT NULL,
  `PasswordReason` varchar(45) DEFAULT NULL,
  `LoginSF` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `SponsorIdFK_AuditLog` FOREIGN KEY (`Id`) REFERENCES `sponsorOrgs` (`Id`),
  CONSTRAINT `UserIdFK_AuditLog` FOREIGN KEY (`Id`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci