#pointTransaction Table 3 triggers

CREATE DEFINER=`admin`@`%` TRIGGER `pointTransaction_AFTER_INSERT` AFTER INSERT ON `pointTransaction` FOR EACH ROW BEGIN
-- call TEAM2_DB.totalUserPoints();
-- call TEAM2_DB.totalSponsorPoints();
	CALL TEAM2_DB.updateUserAndSponsorPoints(NEW.sponsorId, NEW.userId);
END

CREATE DEFINER=`admin`@`%` TRIGGER `pointTransaction_AFTER_UPDATE` AFTER UPDATE ON `pointTransaction` FOR EACH ROW BEGIN
-- call TEAM2_DB.totalUserPoints();
-- call TEAM2_DB.totalSponsorPoints();
	CALL TEAM2_DB.updateUserAndSponsorPoints(NEW.sponsorId, NEW.userId);
END

CREATE DEFINER=`admin`@`%` TRIGGER `pointTransaction_AFTER_DELETE` AFTER DELETE ON `pointTransaction` FOR EACH ROW BEGIN
	CALL TEAM2_DB.updateUserAndSponsorPoints(OLD.sponsorId, OLD.userId);
END