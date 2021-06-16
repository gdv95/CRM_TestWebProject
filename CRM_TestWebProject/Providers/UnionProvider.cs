using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_TestWebProject
{
    public class UnionProvider
    {
        public static List<CRMUser> getUsers()
        {
            var logins = CRMProvider.getLoginsOfActiveUsers().Except(ADProvider.getLoginsOfActiveUsers());
            List<CRMUser> CRMUsers = CRMProvider.getActiveUsers();
            List<CRMUser> users = new List<CRMUser>();

            foreach (string login in logins)
            {
                foreach (CRMUser user in CRMUsers)
                {
                    if (login.Equals(user.Login))
                    {
                        users.Add(user);
                    }
                }
            }

            return users;
        }
    }
}