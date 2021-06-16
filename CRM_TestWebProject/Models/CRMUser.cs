using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_TestWebProject
{
    public class CRMUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }

        public CRMUser(string firstName, string lastName, string login)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Login = login ?? throw new ArgumentNullException(nameof(login));
        }
    }
}