DROP SCHEMA IF EXISTS TEAM2_DB;
CREATE SCHEMA TEAM2_DB;
use TEAM2_DB;

CREATE TABLE  users(
Id	int NOT NULL,
-- sponsor ID can be null b/c admins
SponsorID	int,
Username varchar(30) NOT NULL,
UserFName	varchar(30) NOT NULL,
UserLName	varchar(30) NOT NULL,
UserType varchar(30) NOT NULL,
UserAddress varchar(30),
UserEmail varchar(30),
UserPhoneNum varchar(30),
UserPronouns varchar(30),
UserPwd varchar(30) NOT NULL, 
PRIMARY KEY(Id)
);

CREATE TABLE  sponsorOrgs(
Id	int NOT NULL,
OrgName varchar(30) NOT NULL,
OrgDescription varchar(30) NOT NULL,
CatalogueID int NOT NULL,
PRIMARY KEY(Id)
);

CREATE TABLE driverOrders(
Id int NOT NULL,
UserID int NOT NULL,
SponsorID int NOT NULL,
OrderStatus varchar(30) NOT NULL,
TotalPointVal int NOT NULL,
OrderDate DateTime NOT NULL,
PRIMARY KEY(Id)
);

ALTER TABLE users MODIFY Id INT NOT NULL AUTO_INCREMENT;
ALTER TABLE sponsorOrgs MODIFY Id INT NOT NULL AUTO_INCREMENT;
ALTER TABLE driverOrders MODIFY Id INT NOT NULL AUTO_INCREMENT;
ALTER TABLE users ADD CONSTRAINT users_sponsorOrgFK FOREIGN KEY(SponsorID) REFERENCES sponsorOrgs(Id);
-- ALTER TABLE sponsorOrg ADD CONSTRAINT sponosor_catlogue FOREIGN KEY(CatalogueID) REFERENCES catalogue(Id);
ALTER TABLE driverOrders ADD CONSTRAINT order_driverFK FOREIGN KEY(UserID) REFERENCES users(Id);
ALTER TABLE driverOrders ADD CONSTRAINT order_sponsorFK FOREIGN KEY(SponsorID) REFERENCES sponsorOrgs(Id);
