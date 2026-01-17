using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using EuroRunAPI.Authentification.Models;
using EuroRunAPI.Authentification.ViewModels;
using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Authentification.Helpers
{
    public static class MyAuthTokenExtension
    {
        public class LoginInfo
        {
            [JsonIgnore]
            public UserAccountGetVM userAccount => authentificationToken?.UserAccount;
            public AuthentificationTokenGetVM authentificationToken { get; set; }
            public bool isLoggedIn => userAccount != null;

            public LoginInfo(AuthentificationTokenGetVM AuthentificationToken)
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

            AuthentificationToken authToken = db.AuthentificationTokens.Include(x => x.UserAccount).ThenInclude(y => y.Role).
                SingleOrDefault(x => token != null && x.Value == token);

            return authToken;
        }

        public static AuthentificationTokenGetVM GetAuthTokenVM(this HttpContext httpContext)
        {
            AuthentificationToken authToken = httpContext.GetAuthToken();

            var userGet = new UserAccountGetVM
            {
                FirstName = authToken.UserAccount.FirstName,
                LastName = authToken.UserAccount.LastName,
                PhoneNumber = authToken.UserAccount.PhoneNumber,
                Email = authToken.UserAccount.Email,
                UserName = authToken.UserAccount.UserName,
                Picture = authToken.UserAccount.Picture != null ? Convert.ToBase64String(authToken.UserAccount.Picture) : null,
                Active = authToken.UserAccount.Active,
                Role = authToken.UserAccount.Role
            };

            var tokenGet = new AuthentificationTokenGetVM()
            {
                IpAddress = authToken.IpAddress,
                Value = authToken.Value,
                UserAccount = userGet,
                TimeOfLogin = authToken.TimeOfLogin
            };

            return tokenGet;
        }

        public static LoginInfo GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthTokenVM();

            return new LoginInfo(token);
        }

    }
}
