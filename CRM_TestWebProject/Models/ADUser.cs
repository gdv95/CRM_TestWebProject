using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_TestWebProject
{
    public class ADUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public DateTime? LastLogon { get; set; }

        public ADUser(string firstName, string lastName, string login, DateTime? lastLogon)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Login = login ?? throw new ArgumentNullException(nameof(login));
            LastLogon = lastLogon;
        }
    }
}