using System.Web.Security;
using RussianTeaClubSite.Infrastructure.Abstract;

namespace RussianTeaClubSite.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string userName, string password)
        {
            var result = FormsAuthentication.Authenticate(userName, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }

            return result;
        }
    }
}