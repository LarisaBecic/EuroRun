import {AuthentificationHelper} from "./helpers/authentification-helper";
import {AuthentificationToken} from "./helpers/login-info";

export class Config{
  static api = "http://10.0.0.7:7249"; //localhost rules: https://localhost..

  static http_options = function () {
    // @ts-ignore
    let authentificationToken:AuthentificationToken = AuthentificationHelper.getLoginInfo().authentificationToken;
    let myToken = "";

    if(authentificationToken!=null)
      myToken=authentificationToken.value;
    return{
      headers: {
        'authentification-token': myToken,
      }
    };
  }
}
