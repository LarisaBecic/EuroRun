import {LoginInfo} from "./login-info";

export class AuthentificationHelper {
  static setLoginInfo(x: LoginInfo | null) :void
  {
    if(x==null)
      x = new LoginInfo();
    sessionStorage.setItem("authentification-token", JSON.stringify(x));
  }

  static getLoginInfo():LoginInfo
  {
    // @ts-ignore
    let x:string = sessionStorage.getItem("authentification-token");
    if(x==="")
      return new LoginInfo();

    try {

      let loginInfo:LoginInfo = JSON.parse(x);
      if(loginInfo==null)
        return new LoginInfo();
      return loginInfo;
    }
    catch (e) {
      return new LoginInfo();
    }
  }
}
