CREATE TABLE `pointTransaction` (
  `pointId` int NOT NULL AUTO_INCREMENT,
  `sponsorId` int NOT NULL,
  `userId` int NOT NULL,
  `pointValue` double NOT NULL,
  `reason` varchar(45) DEFAULT NULL,
  `modDate` datetime NOT NULL,
  `isSpecialTransaction` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`pointId`)
) ENGINE=InnoDB AUTO_INCREMENT=368 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci