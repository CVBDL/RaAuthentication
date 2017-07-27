using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace RAAuthenticationLib
{
    public class Authentication
    {
        static public Task<bool> CheckAuthenticateAsync(string userName, string password, string domainName)
        {
            return Task.Run(() => {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domainName, userName, password))
                {
                    return pc.ValidateCredentials(userName, password, ContextOptions.Negotiate);
                }
            });
        }
        
        static public Task<UserDetail> GetUserEmailFromADAsync(string userName, string password, string domainName)
        {
            return Task.Run(() => {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domainName, userName, password))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(pc, userName);
                    if (null == user)
                    {
                        return null;
                    }

                    UserDetail detail = new UserDetail
                    {
                        DisplayName = user.DisplayName,
                        EmailAddress = user.EmailAddress,
                        EmployeeID = user.EmployeeId,
                        Name = user.Name
                    };
                    return detail;
                }
            });
        }
    }
}
