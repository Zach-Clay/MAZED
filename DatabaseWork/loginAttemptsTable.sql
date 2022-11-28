CREATE TABLE `loginAttempts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `attemptedDate` datetime NOT NULL,
  `isLoginSuccessful` tinyint NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=159 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci