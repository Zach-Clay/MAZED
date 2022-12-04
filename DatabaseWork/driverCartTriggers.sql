#driverCart Table's triggers

CREATE DEFINER=`admin`@`%` TRIGGER `driverCart_AFTER_INSERT` AFTER INSERT ON `driverCart` FOR EACH ROW BEGIN
-- call totalCart(NEW.sponsorId, NEW.userId); 
END

CREATE DEFINER=`admin`@`%` TRIGGER `driverCart_AFTER_UPDATE` AFTER UPDATE ON `driverCart` FOR EACH ROW BEGIN
-- call totalCart(NEW.sponsorId, NEW.userId); 
END

CREATE DEFINER=`admin`@`%` TRIGGER `driverCart_AFTER_DELETE` AFTER DELETE ON `driverCart` FOR EACH ROW BEGIN
-- call totalCart(OLD.sponsorId, OLD.userId); 
END