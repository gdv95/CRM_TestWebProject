using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace CRM_TestWebProject
{
    public class ADProvider
    {
        private const string sAMAccountName = "sAMAccountName";
        private static string domainPath = "LDAP://OU=Альвента,DC=alventa,DC=ru";
        private static DirectoryEntry searchRoot = new DirectoryEntry(domainPath);

        private static SearchResultCollection getConnection()
        {
            using (DirectorySearcher search = new DirectorySearcher(searchRoot))
            {
                search.Filter = "(&(objectClass=user)(objectCategory=person))";

                search.PropertiesToLoad.Add(sAMAccountName);  // Логин
                search.PropertiesToLoad.Add("sn");          // Фамилия
                search.PropertiesToLoad.Add("givenName");   // Имя
                search.PropertiesToLoad.Add("userAccountControl"); // Состояние учетной записи в домене
                search.PropertiesToLoad.Add("lastLogon");     // Дата и время последнего входа в домен
                search.PropertiesToLoad.Add("lastLogonTimestamp");     // Дата и время последнего входа в домен 2

                return search.FindAll();
            }
        }

        public static ADUser getActiveUser(string login)
        {
            throw new NotImplementedException();
        }

        public static List<ADUser> getActiveUsers()
        {
            SearchResultCollection resultCol = getConnection();

            List<ADUser> users = new List<ADUser>();

            if (resultCol != null)
            {
                foreach (SearchResult adUser in resultCol)
                {
                    bool accountDisable = ((int)adUser.Properties["userAccountControl"][0] & 0x0002) == 2;

                    string accountName = adUser.Properties.Contains(sAMAccountName)
                                                        ? adUser.Properties[sAMAccountName][0].ToString()
                                                         : string.Empty;

                    string surname = adUser.Properties.Contains("sn")
                                                         ? adUser.Properties["sn"][0].ToString()
                                                         : string.Empty;

                    string name = adUser.Properties.Contains("givenName")
                                                         ? adUser.Properties["givenName"][0].ToString()
                                                         : string.Empty;

                    // Здесь можно добавить еще поля...

                    DateTime? lastLogon = adUser.Properties.Contains("lastLogon")
                        ? (DateTime?)DateTime.FromFileTime((long)adUser.Properties["lastLogon"][0])
                        : null;

                    DateTime? lastLogonTimestamp = adUser.Properties.Contains("lastLogonTimestamp")
                        ? (DateTime?)DateTime.FromFileTime((long)adUser.Properties["lastLogonTimestamp"][0])
                        : null;


                    DateTime? lastLogonReal = new List<DateTime?>() { lastLogon, lastLogonTimestamp }.OrderByDescending(t => t).FirstOrDefault();

                    if (accountDisable == false)
                    {
                        users.Add(new ADUser(name, surname, accountName, lastLogonReal));
                    }
                }
            }

            return users;
        }

        public static List<string> getLoginsOfActiveUsers()
        {
            List<string> logins = new List<string>();

            foreach (ADUser user in getActiveUsers())
            {
                logins.Add(user.Login);
            }

            return logins;
        }
    }
}