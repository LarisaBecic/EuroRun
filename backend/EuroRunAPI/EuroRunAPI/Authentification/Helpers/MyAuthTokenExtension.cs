using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using EuroRunAPI.Authentification.Models;
using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Authentification.Helpers
{
    public static class MyAuthTokenExtension
    {
        public class LoginInfo
        {
            [JsonIgnore]
            public UserAccount userAccount => authentificationToken?.UserAccount;
            public AuthentificationToken authentificationToken { get; set; }
            public bool isLoggedIn => userAccount != null;

            public LoginInfo(AuthentificationToken AuthentificationToken)
            {
                this.authentificationToken = AuthentificationToken;
            }
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authentification-token"];
            return token;
        }

        public static AuthentificationToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            AuthentificationToken UserAccount = db.AuthentificationTokens.Include(x => x.UserAccount).
                SingleOrDefault(x => token != null && x.Value == token);

            return UserAccount;
        }

        public static LoginInfo GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInfo(token);
        }

    }
}
