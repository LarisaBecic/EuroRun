import {AuthentificationHelper} from "./helpers/authentification-helper";
import {AuthentificationToken} from "./helpers/login-info";

export class Config{
  static api = "https://localhost:7249";

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
