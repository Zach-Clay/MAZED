CREATE DEFINER=`admin`@`%` PROCEDURE `totalCart`(IN userId INT, sponsorId INT)
BEGIN
DECLARE cartTotal INT DEFAULT 0;

SELECT SUM(c.pointValue)
INTO cartTotal
FROM driverCart as c
WHERE c.userId=userId AND c.sponsorId=sponsorId;

SET SQL_SAFE_UPDATES = 0;
UPDATE driverCart as c
SET c.cartTotal = cartTotal
WHERE c.userId=userId AND c.sponsorId=sponsorId;
SET SQL_SAFE_UPDATES = 1;
END