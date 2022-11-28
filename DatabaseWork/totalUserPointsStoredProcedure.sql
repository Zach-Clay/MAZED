CREATE DEFINER=`admin`@`%` PROCEDURE `totalUserPoints`()
BEGIN
SET SQL_SAFE_UPDATES = 0;
UPDATE userToSponsors u
INNER JOIN (
	SELECT pointTransaction.userId, pointTransaction.sponsorId, SUM(pointTransaction.pointValue) as total
	FROM pointTransaction
	GROUP BY pointTransaction.userId
)pointTransaction ON u.sponsorId = pointTransaction.sponsorId and u.userId = pointTransaction.userId
SET  u.userPoints = pointTransaction.total
WHERE u.userId = pointTransaction.userId;
SET SQL_SAFE_UPDATES = 1;
END