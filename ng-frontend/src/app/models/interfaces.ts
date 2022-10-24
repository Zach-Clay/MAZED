import * as internal from "stream";

export interface User {
  id: number;
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
  sponsorCount?: number;
}

export interface UserToSponsor {
  id: number;
  userId: number;
  sponsorId: number;
  userPoints: number;
  sponsorTotal?: number;
  userType: string;
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
  isSpecialTransaction?: number;
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

export interface Product {
  productId: number;
  sponsorId: number;
  orderId: number;
  pointValue: number;
  orderQuantity: number;
  availibility: boolean;
  description: string;
  image: number[];
  name: string;
  isBlackListed: boolean;
}
