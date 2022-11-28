CREATE DEFINER=`admin`@`%` PROCEDURE `updateUserAndSponsorPoints`(IN sponsorId INT, userId INT)
BEGIN

	-- Declare intermediate variables
	DECLARE totalUsersPoints INT DEFAULT 0;
    DECLARE totalSponsorsPoints INT DEFAULT 0;

	-- Get total of the users points
	SELECT SUM(p.pointValue)
	INTO totalUsersPoints
	FROM pointTransaction as p
	WHERE p.sponsorId=sponsorId AND p.userId=userId;
        
    -- Update total user points
	SET SQL_SAFE_UPDATES = 0;
    UPDATE userToSponsors as u
    SET u.userPoints = totalUsersPoints
    WHERE u.userId=userId AND u.sponsorId=sponsorId;
	SET SQL_SAFE_UPDATES = 1;
    
    -- Get total of the sponsors points
    SELECT SUM(p.pointValue)
    INTO totalSponsorsPoints
    FROM pointTransaction as p
    WHERE p.sponsorId=sponsorId;
        
     -- Update total sponsor points
	SET SQL_SAFE_UPDATES = 0;
    UPDATE userToSponsors as u
    SET u.sponsorTotal = totalSponsorsPoints
    WHERE u.sponsorId=sponsorId;
	SET SQL_SAFE_UPDATES = 1;
    
END