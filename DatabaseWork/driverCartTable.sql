CREATE TABLE `driverCart` (
  `id` int NOT NULL AUTO_INCREMENT,
  `userId` int NOT NULL,
  `sponsorId` int NOT NULL,
  `pointValue` double NOT NULL,
  `productId` int NOT NULL,
  `cartTotal` int DEFAULT NULL,
  `driverCartcol` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=111 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci