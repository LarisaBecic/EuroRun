import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoginInfo } from './login-info';
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

  getLoginInfo(): LoginInfo | null {
    return this.loginInfoSubject.value;
  }
}
