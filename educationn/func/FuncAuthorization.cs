using educationn.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educationn.func
{
    internal class FuncAuthorization
    {
        public static ObservableCollection<Employee> employees {  get; set; }
        public static Employee AuthorizationEmpl(string login, string password)
        {
            employees = new ObservableCollection<Employee>(DBConnection.Uchebka1Entities.Employee.ToList());
            var users = employees.Where(user  => user.Login == login && user.Password == password).FirstOrDefault();
            if (users != null)
            {
                return users;
            }
            else
            {
                return users;
            }
        }
    }
}
