export interface User {
  id: number;
  sponsorId: number;
  username: string;
  userFname: string;
  userLname: string;
  userType: string;
  userAddress: string;
  userEmail: string;
  userPwd: string;
  userPhoneNum: string;
  isBlacklisted: number;
  pointNotifications: number;
  orderNotifications: number;
  issueNotifications: number;
}

export interface LoginAttempt {
  id: number;
  username: string;
  isLoginSuccessful: string;
}

export interface PointsChanges {
  PointId: number;
  SponsorId: number;
  UserId: number;
  PointValue: number;
  Reason: string;
}

export interface SponsorOrg {
  id: number;
  orgName: string;
  orgDescription: string;
  catalogueId: number;
  dollarToPoint: number;
  isBlacklisted: number;
}

export interface Application {
  id: number;
  userId: number;
  sponsorId: number;
  approvalStatus: number; //0 or 1
  description: string;
  requestedDate: string;
  responseDate: string;
  isActive: number; //0 or 1
}