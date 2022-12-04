CREATE TABLE `userToSponsors` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `userId` int unsigned NOT NULL,
  `sponsorId` int unsigned NOT NULL,
  `userPoints` double NOT NULL DEFAULT '0',
  `sponsorTotal` int DEFAULT '0',
  `userType` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci