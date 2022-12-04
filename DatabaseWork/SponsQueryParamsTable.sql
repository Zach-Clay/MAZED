CREATE TABLE `SponsQueryParams` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `mediaType` varchar(45) DEFAULT NULL,
  `entities` varchar(45) DEFAULT NULL,
  `attributes` varchar(45) DEFAULT NULL,
  `sponsorID` int NOT NULL,
  `limit` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci