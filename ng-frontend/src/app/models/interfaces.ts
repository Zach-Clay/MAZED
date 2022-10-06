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
