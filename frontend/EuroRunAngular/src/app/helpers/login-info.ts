export class LoginInfo {
  authentificationToken?: AuthentificationToken|null=null;
  isLoggedIn: boolean=false;
}

export interface AuthentificationToken {
  value: string;
  userAccount: UserAccount;
  timeOfLogin: Date;
  IpAddress: string;
}

export interface UserAccount {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  userName: string;
  picture?: string;
  active: boolean;
  role: string;
}
