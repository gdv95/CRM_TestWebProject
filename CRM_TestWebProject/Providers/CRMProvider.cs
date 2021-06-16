using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Query;

namespace CRM_TestWebProject
{
    public class CRMProvider
    {
        private static Uri organizationUri = new Uri("http://nskdccrm/alventa/xrmservices/2011/Organization.svc");
        private static Uri homeRealmUri = null;

        private static IOrganizationService getConnection()
        {
            // Учетные данные вошедшего в систему пользователя для авторизации в CRM
            ClientCredentials credentials = new ClientCredentials();
            credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            // Создание экземпляра класса OrganizationServiceProxy для реализации интерфейса IOrganizationService
            // и предоставления канала WCF с проверкой подлинности для службы организации.
            OrganizationServiceProxy orgProxy = new OrganizationServiceProxy(organizationUri, homeRealmUri, credentials, null);
            orgProxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.CloseTimeout = new TimeSpan(1, 0, 0);
            orgProxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.OpenTimeout = new TimeSpan(1, 0, 0);
            orgProxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
            orgProxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.SendTimeout = new TimeSpan(1, 0, 0);
            orgProxy.Timeout = new TimeSpan(1, 0, 0);

            IOrganizationService organizationService = orgProxy as IOrganizationService;

            return organizationService;
        }

        public static List<CRMUser> getActiveUsers()
        {
            QueryExpression systemUserQE = new QueryExpression
            {
                EntityName = "systemuser",
                ColumnSet = new ColumnSet("firstname", "lastname", "domainname"),
                Criteria =
                {
                    Conditions = {
                        new ConditionExpression {
                            AttributeName = "isdisabled",
                            Operator = ConditionOperator.Equal,
                            Values = { "0" }
                        }
                    }
                }
            };

            List<CRMUser> users = new List<CRMUser>();

            try
            {
                DataCollection<Entity> systemUserEC = getConnection().RetrieveMultiple(systemUserQE).Entities;
                foreach (Entity suE in systemUserEC)
                {
                    //Console.WriteLine(suE.Attributes["firstname"] + " " + suE.Attributes["lastname"] + " " + suE.Attributes["domainname"]);
                    users.Add(new CRMUser(suE.Attributes["firstname"].ToString(), suE.Attributes["lastname"].ToString(), suE.Attributes["domainname"].ToString().Substring(8)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return users;
        }

        public static List<string> getLoginsOfActiveUsers()
        {
            List<string> logins = new List<string>();

            foreach (CRMUser user in getActiveUsers())
            {
                logins.Add(user.Login);
            }

            return logins;
        }
    }
}