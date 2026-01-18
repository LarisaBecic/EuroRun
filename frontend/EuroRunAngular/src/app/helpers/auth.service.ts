import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoginInfo, UserAccount } from './login-info';
import { AuthentificationHelper } from './authentification-helper';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private loginInfoSubject = new BehaviorSubject<LoginInfo | null>(
    AuthentificationHelper.getLoginInfo()
  );

  loginInfo$ = this.loginInfoSubject.asObservable();

  setLoginInfo(info: LoginInfo | null) {
    AuthentificationHelper.setLoginInfo(info);
    this.loginInfoSubject.next(info);
  }

  updateUserAccount(updatedUser: UserAccount) {
    const info = this.loginInfoSubject.value;
    if (!info?.authentificationToken) return;

    const updatedInfo: LoginInfo = {
      ...info,
      authentificationToken: {
        ...info.authentificationToken,
        userAccount: updatedUser
      }
    };
    this.loginInfoSubject.next(updatedInfo);

    AuthentificationHelper.setLoginInfo(updatedInfo);
  }



  getLoginInfo(): LoginInfo | null {
    return this.loginInfoSubject.value;
  }
}
