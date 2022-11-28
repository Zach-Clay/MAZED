CREATE DEFINER=`admin`@`%` PROCEDURE `totalSponsorPoints`()
BEGIN
SET SQL_SAFE_UPDATES = 0;
UPDATE userToSponsors u
INNER JOIN (
	SELECT pointTransaction.sponsorId, SUM(pointTransaction.pointValue) as total
	FROM pointTransaction
	GROUP BY pointTransaction.sponsorId
)pointTransaction ON u.sponsorId = pointTransaction.sponsorId
SET  u.sponsorTotal = pointTransaction.total;
SET SQL_SAFE_UPDATES = 1;
END