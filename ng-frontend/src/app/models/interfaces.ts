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
  totalPoints: number;
}

export interface LoginAttempt {
  id: number;
  username: string;
  isLoginSuccessful: string;
}

export interface PointsChanges {
  pointId: number;
  sponsorId: number;
  userId: number;
  pointValue: number;
  reason: string;
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
  applicantName: string;
  sponsorName: string;
  description: string;
  requestedDate: any;
  responseDate: any;
  decisionReason: string;
  isActive: number; //0 or 1
}
