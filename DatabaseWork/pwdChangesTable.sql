CREATE TABLE `pwdChanges` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ChangedPwd` varchar(45) DEFAULT NULL,
  `OgPwd` varchar(45) DEFAULT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`,`UserId`),
  KEY `UserIdFK_idx` (`UserId`),
  CONSTRAINT `UserIdFK_PwdChanges` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci