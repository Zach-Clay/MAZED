CREATE TABLE `application` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `userId` int NOT NULL,
  `sponsorId` int NOT NULL,
  `approvalStatus` tinyint DEFAULT NULL,
  `applicantName` varchar(45) DEFAULT NULL,
  `sponsorName` varchar(45) DEFAULT NULL,
  `description` longtext NOT NULL,
  `requestedDate` date NOT NULL,
  `responseDate` date DEFAULT NULL,
  `decisionReason` longtext,
  `isActive` tinyint NOT NULL,
  PRIMARY KEY (`Id`,`userId`,`sponsorId`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci