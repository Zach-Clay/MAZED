CREATE TABLE `product` (
  `productId` int NOT NULL AUTO_INCREMENT,
  `sponsorId` int NOT NULL,
  `orderId` int DEFAULT NULL,
  `trackId` int NOT NULL,
  `itemCost` int DEFAULT NULL,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB AUTO_INCREMENT=81 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci